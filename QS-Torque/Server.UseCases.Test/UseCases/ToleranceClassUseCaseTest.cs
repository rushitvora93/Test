using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using Server.UseCases.UseCases;

namespace Server.UseCases.Test.UseCases
{
    public class ToleranceClassDataAccessMock : IToleranceClassDataAccess
    {
        public enum ToleranceClassDataAccessFunction
        {
            InsertToleranceClassesWithHistory,
            UpdateToleranceClassesWithHistory,
            Commit
        }

        public void Commit()
        {
            CalledFunctions.Add(ToleranceClassDataAccessFunction.Commit);
        }

        public List<ToleranceClass> LoadToleranceClasses()
        {
            LoadToleranceClassesCalled = true;
            return LoadToleranceClassesReturnValue;
        }

        public List<LocationReferenceLink> GetToleranceClassLocationLinks(ToleranceClassId classId)
        {
            GetToleranceClassLocationLinksParameter = classId;
            return GetToleranceClassLocationLinksReturnValue;
        }

        public List<ToleranceClass> InsertToleranceClassesWithHistory(List<ToleranceClassDiff> toleranceClassDiffs, bool returnList)
        {
            CalledFunctions.Add(ToleranceClassDataAccessFunction.InsertToleranceClassesWithHistory);
            InsertToleranceClassesWithHistoryParameterToleranceClassDiffs = toleranceClassDiffs;
            InsertToleranceClassesWithHistoryParameterReturnList = returnList;
            return InsertToleranceClassesWithHistoryReturnValue;
        }

        public List<ToleranceClass> UpdateToleranceClassesWithHistory(List<ToleranceClassDiff> toleranceClassDiffs)
        {
            CalledFunctions.Add(ToleranceClassDataAccessFunction.UpdateToleranceClassesWithHistory);
            UpdateToleranceClassesWithHistoryParameterToleranceClassDiffs = toleranceClassDiffs;
            return UpdateToleranceClassesWithHistoryReturnValue;
        }

        public List<LocationToolAssignmentId> GetToleranceClassLocationToolAssignmentLinks(ToleranceClassId toleranceClassId)
        {
            GetToleranceClassLocationToolAssignmentLinksParameter = toleranceClassId;
            return GetToleranceClassLocationToolAssignmentLinksReturnValue;
        }

        public Dictionary<long, ToleranceClass> GetToleranceClassesFromHistoryForIds(List<Tuple<long, long, DateTime>> idsWithToleranceClassData)
        {
            GetToleranceClassesFromHistoryForIdsParameter = idsWithToleranceClassData;
            return GetToleranceClassesFromHistoryForIdsReturnValue;
        }

        public List<ToleranceClass> LoadToleranceClassesReturnValue { get; set; }
        public bool LoadToleranceClassesCalled { get; set; }
        public List<LocationReferenceLink> GetToleranceClassLocationLinksReturnValue { get; set; }
        public ToleranceClassId GetToleranceClassLocationLinksParameter { get; set; }
        public List<ToleranceClass> InsertToleranceClassesWithHistoryReturnValue { get; set; }
        public bool InsertToleranceClassesWithHistoryParameterReturnList { get; set; }
        public List<ToleranceClassDiff> InsertToleranceClassesWithHistoryParameterToleranceClassDiffs { get; set; }
        public List<ToleranceClass> UpdateToleranceClassesWithHistoryReturnValue { get; set; }
        public List<ToleranceClassDiff> UpdateToleranceClassesWithHistoryParameterToleranceClassDiffs { get; set; }
        public List<LocationToolAssignmentId> GetToleranceClassLocationToolAssignmentLinksReturnValue { get; set; }
        public ToleranceClassId GetToleranceClassLocationToolAssignmentLinksParameter { get; set; }
        public Dictionary<long, ToleranceClass> GetToleranceClassesFromHistoryForIdsReturnValue { get; set; }
        public List<Tuple<long, long, DateTime>> GetToleranceClassesFromHistoryForIdsParameter { get; set; }
        public List<ToleranceClassDataAccessFunction> CalledFunctions { get; set; } = new List<ToleranceClassDataAccessFunction>();
    }

    public class ToleranceClassUseCaseTest
    {
        [Test]
        public void LoadToleranceClassesCallsDataAccess()
        {
            var dataAccess = new ToleranceClassDataAccessMock();
            var useCase = new ToleranceClassUseCase(dataAccess);

            useCase.LoadToleranceClasses();

            Assert.IsTrue(dataAccess.LoadToleranceClassesCalled);
        }

        [Test]
        public void LoadToleranceClassesReturnsCorrectValue()
        {
            var dataAccess = new ToleranceClassDataAccessMock();
            var useCase = new ToleranceClassUseCase(dataAccess);

            var toleranceClasses = new List<ToleranceClass>();
            dataAccess.LoadToleranceClassesReturnValue = toleranceClasses;

            Assert.AreSame(toleranceClasses, useCase.LoadToleranceClasses());
        }

        [TestCase(10)]
        [TestCase(20)]
        public void GetToleranceClassLocationLinksCallsDataAccess(long classId)
        {
            var dataAccess = new ToleranceClassDataAccessMock();
            var useCase = new ToleranceClassUseCase(dataAccess);

            useCase.GetToleranceClassLocationLinks(new ToleranceClassId(classId));

            Assert.AreEqual(classId, dataAccess.GetToleranceClassLocationLinksParameter.ToLong());
        }

        [Test]
        public void GetToleranceClassLocationLinksReturnsCorrectValue()
        {
            var dataAccess = new ToleranceClassDataAccessMock();
            var useCase = new ToleranceClassUseCase(dataAccess);

            var locationReferenceLinks = new List<LocationReferenceLink>();
            dataAccess.GetToleranceClassLocationLinksReturnValue = locationReferenceLinks;

            var result = useCase.GetToleranceClassLocationLinks(new ToleranceClassId(1));

            Assert.AreSame(locationReferenceLinks, result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InsertToleranceClassesWithHistoryCallsDataAccess(bool returnList)
        {
            var dataAccess = new ToleranceClassDataAccessMock();
            var useCase = new ToleranceClassUseCase(dataAccess);

            var diffs = new List<ToleranceClassDiff>();
            useCase.InsertToleranceClassesWithHistory(diffs, returnList);

            Assert.AreSame(diffs, dataAccess.InsertToleranceClassesWithHistoryParameterToleranceClassDiffs);
            Assert.AreEqual(returnList, dataAccess.InsertToleranceClassesWithHistoryParameterReturnList);
        }

        [Test]
        public void InsertToleranceClassesWithHistoryCallsCommitAfterWork()
        {
            var dataAccess = new ToleranceClassDataAccessMock();
            var useCase = new ToleranceClassUseCase(dataAccess);

            useCase.InsertToleranceClassesWithHistory(new List<ToleranceClassDiff>(), false);

            Assert.AreEqual(ToleranceClassDataAccessMock.ToleranceClassDataAccessFunction.Commit, dataAccess.CalledFunctions.Last());
        }

        [Test]
        public void InsertToleranceClassesWithHistoryReturnsCorrectValue()
        {
            var dataAccess = new ToleranceClassDataAccessMock();
            var useCase = new ToleranceClassUseCase(dataAccess);

            var toleranceClasses = new List<ToleranceClass>();
            dataAccess.InsertToleranceClassesWithHistoryReturnValue = toleranceClasses;

            var returnValue = useCase.InsertToleranceClassesWithHistory(null, true);

            Assert.AreSame(toleranceClasses, returnValue);
        }

        [Test]
        public void UpdateToleranceClassesWithHistoryCallsDataAccess()
        {
            var dataAccess = new ToleranceClassDataAccessMock();
            var useCase = new ToleranceClassUseCase(dataAccess);

            var diffs = new List<ToleranceClassDiff>();
            useCase.UpdateToleranceClassesWithHistory(diffs);

            Assert.AreSame(diffs, dataAccess.UpdateToleranceClassesWithHistoryParameterToleranceClassDiffs);
        }

        [Test]
        public void UpdateToleranceClassesWithHistoryCallsCommitAfterWork()
        {
            var dataAccess = new ToleranceClassDataAccessMock();
            var useCase = new ToleranceClassUseCase(dataAccess);

            useCase.UpdateToleranceClassesWithHistory(new List<ToleranceClassDiff>());

            Assert.AreEqual(ToleranceClassDataAccessMock.ToleranceClassDataAccessFunction.Commit, dataAccess.CalledFunctions.Last());
        }

        [Test]
        public void UpdateToleranceClassesWithHistoryReturnsCorrectValue()
        {
            var dataAccess = new ToleranceClassDataAccessMock();
            var useCase = new ToleranceClassUseCase(dataAccess);

            var toleranceClasses = new List<ToleranceClass>();
            dataAccess.UpdateToleranceClassesWithHistoryReturnValue = toleranceClasses;

            var returnValue = useCase.UpdateToleranceClassesWithHistory(null);

            Assert.AreSame(toleranceClasses, returnValue);
        }

        [TestCase(10)]
        [TestCase(20)]
        public void GetToleranceClassToolLinksCallsDataAccess(long classId)
        {
            var dataAccess = new ToleranceClassDataAccessMock();
            var useCase = new ToleranceClassUseCase(dataAccess);

            useCase.GetToleranceClassLocationToolAssignmentLinks(new ToleranceClassId(classId));

            Assert.AreEqual(classId, dataAccess.GetToleranceClassLocationToolAssignmentLinksParameter.ToLong());
        }

        [Test]
        public void GetToleranceClassToolLinksReturnsCorrectValue()
        {
            var dataAccess = new ToleranceClassDataAccessMock();
            var useCase = new ToleranceClassUseCase(dataAccess);

            var toolReferenceLinks = new List<LocationToolAssignmentId>();
            dataAccess.GetToleranceClassLocationToolAssignmentLinksReturnValue = toolReferenceLinks;

            var result = useCase.GetToleranceClassLocationToolAssignmentLinks(new ToleranceClassId(1));

            Assert.AreSame(toolReferenceLinks, result);
        }

        [Test]
        public void GetToleranceClassesFromHistoryForIdsCallsDataAccess()
        {
            var dataAccess = new ToleranceClassDataAccessMock();
            var useCase = new ToleranceClassUseCase(dataAccess);

            var data = new List<Tuple<long, long, DateTime>>();
            useCase.GetToleranceClassesFromHistoryForIds(data);

            Assert.AreSame(data, dataAccess.GetToleranceClassesFromHistoryForIdsParameter);
        }

        [Test]
        public void GetToleranceClassesFromHistoryForIdsReturnsCorrectValue()
        {
            var dataAccess = new ToleranceClassDataAccessMock();
            var useCase = new ToleranceClassUseCase(dataAccess);

            var data = new Dictionary<long, ToleranceClass>();
            dataAccess.GetToleranceClassesFromHistoryForIdsReturnValue = data;

            var result = useCase.GetToleranceClassesFromHistoryForIds(new List<Tuple<long, long, DateTime>>());

            Assert.AreSame(data, result);
        }
    }
}
