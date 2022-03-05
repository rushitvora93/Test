using System.Collections.Generic;
using BasicTypes;
using NUnit.Framework;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using TestDateCalculationService;
using TestHelper.Checker;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    class TestDateCalculationUseCaseMock : ITestDateCalculationUseCase
    {
        public List<LocationToolAssignmentId> CalculateToolTestDateForParameter { get; set; }
        public bool CalculateToolTestDateForAllLocationToolAssignmentsCalled { get; set; }
        public TestLevelSetId CalculateToolTestDateForTestLevelSetParameter { get; set; }
        public List<ProcessControlConditionId> CalculateProcessControlDateForParameter { get; set; }
        public bool CalculateProcessControlDateForAllLocationToolAssignmentsCalled { get; set; }
        public TestLevelSetId CalculateProcessControlDateForTestLevelSetParameter { get; set; }

        public void CalculateToolTestDateFor(List<LocationToolAssignmentId> ids)
        {
            CalculateToolTestDateForParameter = ids;
        }

        public void CalculateToolTestDateForTestLevelSet(TestLevelSetId id)
        {
            CalculateToolTestDateForTestLevelSetParameter = id;
        }

        public void CalculateToolTestDateForAllLocationToolAssignments()
        {
            CalculateToolTestDateForAllLocationToolAssignmentsCalled = true;
        }

        public void CalculateProcessControlDateFor(List<ProcessControlConditionId> ids)
        {
            CalculateProcessControlDateForParameter = ids;
        }

        public void CalculateProcessControlDateForTestLevelSet(TestLevelSetId id)
        {
            CalculateProcessControlDateForTestLevelSetParameter = id;
        }

        public void CalculateProcessControlDateForAllProcessControlConditions()
        {
            CalculateProcessControlDateForAllLocationToolAssignmentsCalled = true;
        }
    }


    public class TestDateCalculationServiceTest
    {
        [TestCase(5, 9, 12)]
        [TestCase(56)]
        [TestCase(5, 65, 98, 132, 465465)]
        public void CalculateToolTestDateForPassesDataToUseCase(params long[] ids)
        {
            var tuple = CreateServiceTuple();
            var serverParam = new ListOfLocationToolAssignmentIds();
            foreach (var id in ids)
            {
                serverParam.Values.Add(id);
            }
            tuple.service.CalculateToolTestDateFor(serverParam, null);
            CheckerFunctions.CollectionAssertAreEquivalent(ids, tuple.useCase.CalculateToolTestDateForParameter, (x, y) => x == y.ToLong());
        }
        
        [Test]
        public void CalculateToolTestDateForAllLocationToolAssignmentsPassesDataToUseCase()
        {
            var tuple = CreateServiceTuple();
            tuple.service.CalculateToolTestDateForAllLocationToolAssignments(new NoParams(), null);
            Assert.IsTrue(tuple.useCase.CalculateToolTestDateForAllLocationToolAssignmentsCalled);
        }

        [TestCase(5)]
        [TestCase(56)]
        [TestCase(9)]
        public void CalculateToolTestDateForTestLevelSetPassesDataToUseCase(long id)
        {
            var tuple = CreateServiceTuple();
            tuple.service.CalculateToolTestDateForTestLevelSet(new Long() { Value = id }, null);
            Assert.AreEqual(id, tuple.useCase.CalculateToolTestDateForTestLevelSetParameter.ToLong());
        }

        [TestCase(5, 9, 12)]
        [TestCase(56)]
        [TestCase(5, 65, 98, 132, 465465)]
        public void CalculateProcessControlDateForPassesDataToUseCase(params long[] ids)
        {
            var tuple = CreateServiceTuple();
            var serverParam = new ListOfProcessControlConditionIds();
            foreach (var id in ids)
            {
                serverParam.Values.Add(id);
            }
            tuple.service.CalculateProcessControlDateFor(serverParam, null);
            CheckerFunctions.CollectionAssertAreEquivalent(ids, tuple.useCase.CalculateProcessControlDateForParameter, (x, y) => x == y.ToLong());
        }

        [Test]
        public void CalculateProcessControlDateForAllLocationToolAssignmentsPassesDataToUseCase()
        {
            var tuple = CreateServiceTuple();
            tuple.service.CalculateProcessControlDateForAllProcessControlConditions(new NoParams(), null);
            Assert.IsTrue(tuple.useCase.CalculateProcessControlDateForAllLocationToolAssignmentsCalled);
        }

        [TestCase(5)]
        [TestCase(56)]
        [TestCase(9)]
        public void CalculateProcessControlDateForTestLevelSetPassesDataToUseCase(long id)
        {
            var tuple = CreateServiceTuple();
            tuple.service.CalculateProcessControlDateForTestLevelSet(new Long() { Value = id }, null);
            Assert.AreEqual(id, tuple.useCase.CalculateProcessControlDateForTestLevelSetParameter.ToLong());
        }


        private static (NetworkView.Services.TestDateCalculationService service, TestDateCalculationUseCaseMock useCase) CreateServiceTuple()
        {
            var useCase = new TestDateCalculationUseCaseMock();
            var service = new NetworkView.Services.TestDateCalculationService(useCase, null);
            return (service, useCase);
        }
    }
}
