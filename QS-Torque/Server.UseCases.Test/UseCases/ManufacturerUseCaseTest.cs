using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using Server.TestHelper.Factories;
using Server.TestHelper.Mocks;
using Server.UseCases.UseCases;
using State;
using TestHelper.Checker;

namespace Server.UseCases.Test.UseCases
{
    public class ManufacturerDataAccessMock : IManufacturerDataAccess
    {
        public enum ManufacturerDataAccessFunction
        {
            InsertManufacturersWithHistory,
            UpdateManufacturersWithHistory,
            Commit
        }

        public void Commit()
        {
            CalledFunctions.Add(ManufacturerDataAccessFunction.Commit);
        }

        public List<Manufacturer> LoadManufacturers()
        {
            LoadManufacturersCalled = true;
            return LoadManufacturersReturnValue;
        }

        public List<ToolModelReferenceLink> GetManufacturerModelLinks(ManufacturerId manufacturerId)
        {
            GetManufacturerModelLinksParameter = manufacturerId;
            return GetManufacturerModelLinksReturnValue;
        }

        public List<Manufacturer> InsertManufacturersWithHistory(List<ManufacturerDiff> manufacturerDiffs, bool returnList)
        {
            CalledFunctions.Add(ManufacturerDataAccessFunction.InsertManufacturersWithHistory);
            InsertManufacturersWithHistoryParameterDiffs = manufacturerDiffs;
            InsertManufacturersWithHistoryParameterReturnList = returnList;
            return InsertManufacturersWithHistoryReturnValue;
        }

        public List<Manufacturer> UpdateManufacturersWithHistory(List<ManufacturerDiff> manufacturerDiffs)
        {
            CalledFunctions.Add(ManufacturerDataAccessFunction.UpdateManufacturersWithHistory);
            UpdateManufacturersWithHistoryParameter = manufacturerDiffs;
            return UpdateManufacturersWithHistoryReturnValue;
        }

        public List<Manufacturer> UpdateManufacturersWithHistoryReturnValue { get; set; }
        public List<ManufacturerDiff> UpdateManufacturersWithHistoryParameter { get; set; }
        public List<Manufacturer> InsertManufacturersWithHistoryReturnValue { get; set; }
        public bool InsertManufacturersWithHistoryParameterReturnList { get; set; }
        public List<ManufacturerDiff> InsertManufacturersWithHistoryParameterDiffs { get; set; }
        public ManufacturerId GetManufacturerModelLinksParameter { get; set; }
        public List<ToolModelReferenceLink> GetManufacturerModelLinksReturnValue { get; set; }
        public List<Manufacturer> LoadManufacturersReturnValue { get; set; }
        public bool LoadManufacturersCalled { get; set; }
        public List<ManufacturerDataAccessFunction> CalledFunctions { get; set; } = new List<ManufacturerDataAccessFunction>();
    }

    public class ManufacturerUseCaseTest
    {
        [Test]
        public void LoadManufacturersCallsDataAccessLoadManufacturers()
        {
            var environment = new Environment();

            environment.useCase.LoadManufacturers();

            Assert.IsTrue(environment.mocks.manufacturerDataAcces.LoadManufacturersCalled);
        }

        [Test]
        public void LoadManufacturersReturnsCorrectValue()
        {
            var environment = new Environment();

            var manufacturers = new List<Manufacturer>();
            environment.mocks.manufacturerDataAcces.LoadManufacturersReturnValue = manufacturers;

            var result = environment.useCase.LoadManufacturers();

            Assert.AreSame(manufacturers, result);
        }

        [TestCase(10)]
        [TestCase(20)]
        public void GetManufacturerCommentCallsDataAccessGetManufacturerComment(long nodeseqid)
        {
            var environment = new Environment();

            environment.useCase.GetManufacturerComment(new ManufacturerId(nodeseqid));

            Assert.AreEqual(NodeId.Manufacturer, environment.mocks.commentDataAccess.GetQstCommentByNodeIdAndNodeSeqIdParameterNodeId);
            Assert.AreEqual(nodeseqid, environment.mocks.commentDataAccess.GetQstCommentByNodeIdAndNodeSeqIdParameterNodeSeqId);
        }

        [TestCase("Kommentar A")]
        [TestCase("blub2020")]
        public void GetManufacturerCommentReturnsCorrectValue(string comment)
        {
            var environment = new Environment();
            environment.mocks.commentDataAccess.GetQstCommentByNodeIdAndNodeSeqIdReturnValue = new HistoryComment(comment);

            environment.useCase.GetManufacturerComment(new ManufacturerId(1));

            Assert.AreEqual(comment, environment.mocks.commentDataAccess.GetQstCommentByNodeIdAndNodeSeqIdReturnValue.ToDefaultString());
        }

        [Test]
        public void GetManufacturerCommentReturnsEmptyStringIfCommentIsNull()
        {
            var environment = new Environment();
            environment.mocks.commentDataAccess.GetQstCommentByNodeIdAndNodeSeqIdReturnValue = null;

            Assert.AreEqual("",environment.useCase.GetManufacturerComment(new ManufacturerId(1)));
        }

        [TestCase(10)]
        [TestCase(20)]
        public void GetManufacturerModelLinksCallsDataAccessGetManufacturerModelLinks(long manufacturerId)
        {
            var environment = new Environment();

            environment.useCase.GetManufacturerModelLinks(new ManufacturerId(manufacturerId));

            Assert.AreEqual(manufacturerId, environment.mocks.manufacturerDataAcces.GetManufacturerModelLinksParameter.ToLong());
        }

        [Test]
        public void GetManufacturerModelLinksReturnsCorrectValue()
        {
            var environment = new Environment();

            var modelLinks = new List<ToolModelReferenceLink>();
            environment.mocks.manufacturerDataAcces.GetManufacturerModelLinksReturnValue = modelLinks;

            var result = environment.useCase.GetManufacturerModelLinks(new ManufacturerId(1));

            Assert.AreSame(modelLinks, result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InsertManufacturersWithHistoryCallsDataAccessInsertManufacturersWithHistory(bool returnList)
        {
            var environment = new Environment();

            var manufacturerDiffs = new List<ManufacturerDiff>();
            environment.useCase.InsertManufacturersWithHistory(manufacturerDiffs, returnList);

            Assert.AreSame(manufacturerDiffs, environment.mocks.manufacturerDataAcces.InsertManufacturersWithHistoryParameterDiffs);
            Assert.AreEqual(returnList, environment.mocks.manufacturerDataAcces.InsertManufacturersWithHistoryParameterReturnList);
        }

        [Test]
        public void InsertManufacturersWithHistoryWithHistoryCallsCommitAfterWork()
        {
            var environment = new Environment();
            
            environment.useCase.InsertManufacturersWithHistory(new List<ManufacturerDiff>(), false);

            Assert.AreEqual(ManufacturerDataAccessMock.ManufacturerDataAccessFunction.Commit, environment.mocks.manufacturerDataAcces.CalledFunctions.Last());
        }

        [Test]
        public void InsertManufacturersWithHistoryReturnsCorrectValue()
        {
            var environment = new Environment();
            
            var manufacturers = new List<Manufacturer>();
            environment.mocks.manufacturerDataAcces.InsertManufacturersWithHistoryReturnValue = manufacturers;

            var returnValue = environment.useCase.InsertManufacturersWithHistory(null, true);

            Assert.AreSame(manufacturers, returnValue);
        }

        [Test]
        public void UpdateManufacturersWithHistoryCallsDataAccessUpdateManufacturersWithHistory()
        {
            var environment = new Environment();
            
            var manufacturerDiffs = new List<ManufacturerDiff>();
            environment.useCase.UpdateManufacturersWithHistory(manufacturerDiffs);

            Assert.AreSame(manufacturerDiffs, environment.mocks.manufacturerDataAcces.UpdateManufacturersWithHistoryParameter);
        }

        [Test]
        public void UpdateManufacturersWithHistoryReturnsCorrectValue()
        {
            var environment = new Environment();

            var manufacturers = new List<Manufacturer>();
            environment.mocks.manufacturerDataAcces.UpdateManufacturersWithHistoryReturnValue = manufacturers;

            var returnValue = environment.useCase.UpdateManufacturersWithHistory(null);

            Assert.AreSame(manufacturers, returnValue);
        }

        static IEnumerable<List<(long, string, long)>> UpdateManufacturersWithHistoryCallsInsertQstCommentsData = new List<List<(long, string, long)>>()
        {
            new List<(long, string, long)>()
            {
                (1, "Kommentar1", 88),
                (2, "Kommentar2", 77)
            },
            new List<(long, string, long)>()
            {
                (3, "Kommentar3", 55)
            }
        };

        [TestCaseSource(nameof(UpdateManufacturersWithHistoryCallsInsertQstCommentsData))]
        public void UpdateManufacturersWithHistoryCallsInsertQstComments(List<(long userId, string comment, long manuId)> datas)
        {
            var environment = new Environment();
            
            var manufacturerDiffs = new List<ManufacturerDiff>();
            foreach (var data in datas)
            {
                manufacturerDiffs.Add(new ManufacturerDiff(CreateUser.IdOnly(data.userId), new HistoryComment(""), 
                    CreateManufacturer.IdOnly(data.manuId),
                    CreateManufacturer.IdAndCommentOnly(data.manuId, data.comment)));
            }

            environment.useCase.UpdateManufacturersWithHistory(manufacturerDiffs);

            var comparer = new Func<(long userId, string comment, long manuid), QstComment, bool>((data, parameter) => 
                data.userId == parameter.UserId.ToLong() && 
                data.comment == parameter.Comment.ToDefaultString() &&
                (long)NodeId.Manufacturer == parameter.NodeId &&
                data.manuid == parameter.NodeSeqid);

            CheckerFunctions.CollectionAssertAreEquivalent(datas, environment.mocks.commentDataAccess.InsertQstCommentsParameter, comparer);
        }

        [Test]
        public void UpdateManufacturersWithHistoryCallsCommitAfterWork()
        {
            var environment = new Environment();

            environment.useCase.UpdateManufacturersWithHistory(new List<ManufacturerDiff>());

            Assert.AreEqual(ManufacturerDataAccessMock.ManufacturerDataAccessFunction.Commit, environment.mocks.manufacturerDataAcces.CalledFunctions.Last());
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                   manufacturerDataAcces = new ManufacturerDataAccessMock();
                   commentDataAccess = new QstCommentDataAccessMock();
                }
                public ManufacturerDataAccessMock manufacturerDataAcces;
                public QstCommentDataAccessMock commentDataAccess;
            }

            public Environment()
            {
                mocks = new Mocks();
                useCase = new ManufacturerUseCase(mocks.manufacturerDataAcces, mocks.commentDataAccess);
            }

            public readonly Mocks mocks;
            public readonly ManufacturerUseCase useCase;
        }
    }
}
