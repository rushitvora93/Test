using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using Server.UseCases.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Server.TestHelper.Factories;
using Server.TestHelper.Mocks;
using State;
using TestHelper.Checker;

namespace Server.UseCases.Test.UseCases
{
    public class ToolDataAccessMock : IToolDataAccess
    {
        public enum ToolDataAccessFunction
        {
            InsertToolsWithHistory,
            UpdateToolsWithHistory,
            Commit
        }
        public List<ToolDataAccessFunction> CalledFunctions { get; set; } = new List<ToolDataAccessFunction>();
        public List<LocationToolAssignmentReferenceLink> GetLocationToolAssignmentLinkForToolReturnValue { get; set; }
        public ToolId GetLocationToolAssignmentLinkForToolParameter { get; set; }
        public Tool GetToolByIdReturnValue { get; set; }
        public ToolId GetToolByIdParameter { get; set; }
        public List<Tool> InsertToolsWithHistoryReturnValue { get; set; }
        public bool InsertToolsWithHistoryParameterReturnList { get; set; }
        public List<ToolDiff> InsertToolsWithHistoryParameterDiff { get; set; }
        public bool IsInventoryNumberUniqueReturnValue { get; set; }
        public string IsInventoryNumberUniqueParameter { get; set; }
        public bool IsSerialNumberUniqueReturnValue { get; set; }
        public string IsSerialNumberUniqueParameter { get; set; }
        public List<Tool> LoadToolsReturnValue { get; set; }
        public int LoadToolsParameterSize { get; set; }
        public int LoadToolsParameterIndex { get; set; }
        public List<Tool> UpdateToolsWithHistoryReturnValue { get; set; }
        public List<ToolDiff> UpdateToolsWithHistoryParameter { get; set; }
        public List<Tool> LoadToolsForModelReturnValue { get; set; } = new List<Tool>();
        public ToolModelId LoadToolsForModelParameter { get; set; }
        public List<ToolModel> LoadModelsWithAtLeasOneToolReturnValue { get; set; }
        public bool LoadModelsWithAtLeasOneToolCalled { get; set; }

        public void Commit()
        {
            CalledFunctions.Add(ToolDataAccessFunction.Commit);
        }

        public List<LocationToolAssignmentReferenceLink> GetLocationToolAssignmentLinkForTool(ToolId id)
        {
            GetLocationToolAssignmentLinkForToolParameter = id;
            return GetLocationToolAssignmentLinkForToolReturnValue;
        }

        public Tool GetToolById(ToolId toolId)
        {
            GetToolByIdParameter = toolId;
            return GetToolByIdReturnValue;
        }

        public List<Tool> InsertToolsWithHistory(List<ToolDiff> toolDiff, bool returnList)
        {
            CalledFunctions.Add(ToolDataAccessFunction.InsertToolsWithHistory);
            InsertToolsWithHistoryParameterDiff = toolDiff;
            InsertToolsWithHistoryParameterReturnList = returnList;
            return InsertToolsWithHistoryReturnValue;
        }

        public bool IsInventoryNumberUnique(string inventoryNumber)
        {
            IsInventoryNumberUniqueParameter = inventoryNumber;
            return IsInventoryNumberUniqueReturnValue;
        }

        public List<Tool> LoadToolsForModel(ToolModelId toolModelId)
        {
            LoadToolsForModelParameter = toolModelId;
            return LoadToolsForModelReturnValue;
        }

        public List<ToolModel> LoadModelsWithAtLeasOneTool()
        {
            LoadModelsWithAtLeasOneToolCalled = true;
            return LoadModelsWithAtLeasOneToolReturnValue;
        }

        public bool IsSerialNumberUnique(string serialNumber)
        {
            IsSerialNumberUniqueParameter = serialNumber;
            return IsSerialNumberUniqueReturnValue;
        }

        public List<Tool> LoadTools(int index, int size)
        {
            LoadToolsParameterIndex = index;
            LoadToolsParameterSize = size;
            return LoadToolsReturnValue;
        }

        public List<Tool> UpdateToolsWithHistory(List<ToolDiff> toolDiff)
        {
            CalledFunctions.Add(ToolDataAccessFunction.UpdateToolsWithHistory);
            UpdateToolsWithHistoryParameter = toolDiff;
            return UpdateToolsWithHistoryReturnValue;
        }
    }

    public class ToolUseCaseTest
    {
        [TestCase(1, 5)]
        [TestCase(132, 435)]
        public void LoadToolsCallsDataAccess(int index, int size)
        {
            var environment = new Environment();

            environment.useCase.LoadTools(index, size);

            Assert.AreEqual(index, environment.mocks.toolDataAccess.LoadToolsParameterIndex);
            Assert.AreEqual(size, environment.mocks.toolDataAccess.LoadToolsParameterSize);
        }

        [Test]
        public void LoadToolsReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new List<Tool>();
            environment.mocks.toolDataAccess.LoadToolsReturnValue = entities;

            Assert.AreSame(entities, environment.useCase.LoadTools(1, 1));
        }

        [TestCase(1)]
        [TestCase(132)]
        public void GetToolByIdCallsDataAccess(long id)
        {
            var environment = new Environment();

            environment.useCase.GetToolById(new ToolId(id));

            Assert.AreEqual(id, environment.mocks.toolDataAccess.GetToolByIdParameter.ToLong());
        }

        [Test]
        public void GetToolByIdReturnsCorrectValue()
        {
            var environment = new Environment(); 

            var entity = new Tool();
            environment.mocks.toolDataAccess.GetToolByIdReturnValue = entity;

            Assert.AreSame(entity, environment.useCase.GetToolById(new ToolId(1)));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InsertToolsWithHistoryCallsDataAccess(bool returnList)
        {
            var environment = new Environment();

            var diffs = new List<ToolDiff>();
            environment.useCase.InsertToolsWithHistory(diffs, returnList);

            Assert.AreSame(diffs, environment.mocks.toolDataAccess.InsertToolsWithHistoryParameterDiff);
            Assert.AreEqual(returnList, environment.mocks.toolDataAccess.InsertToolsWithHistoryParameterReturnList);
        }

        [Test]
        public void InsertToolsWithHistoryCallsCommitAfterWork()
        {
            var environment = new Environment();

            environment.useCase.InsertToolsWithHistory(new List<ToolDiff>(), false);

            Assert.AreEqual(ToolDataAccessMock.ToolDataAccessFunction.Commit, environment.mocks.toolDataAccess.CalledFunctions.Last());
        }

        [Test]
        public void InsertToolsWithHistoryReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new List<Tool>();
            environment.mocks.toolDataAccess.InsertToolsWithHistoryReturnValue = entities;

            var returnValue = environment.useCase.InsertToolsWithHistory(null, true);

            Assert.AreSame(entities, returnValue);
        }

        [Test]
        public void UpdateToolsWithHistoryCallsDataAccess()
        {
            var environment = new Environment();

            var diffs = new List<ToolDiff>();
            environment.useCase.UpdateToolsWithHistory(diffs);

            Assert.AreSame(diffs, environment.mocks.toolDataAccess.UpdateToolsWithHistoryParameter);
        }

        static IEnumerable<List<(long, string, long)>> UpdateToolWithHistoryCallsInsertQstCommentsData = new List<List<(long, string, long)>>()
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

        [TestCaseSource(nameof(UpdateToolWithHistoryCallsInsertQstCommentsData))]
        public void UpdateToolWithHistoryCallsInsertQstComments(List<(long userId, string comment, long toolId)> datas)
        {
            var environment = new Environment();

            var toolDiffs = new List<ToolDiff>();
            foreach (var data in datas)
            {
                toolDiffs.Add(new ToolDiff(CreateUser.IdOnly(data.userId), new HistoryComment(""),
                    CreateTool.WithId(data.toolId),
                    CreateTool.IdAndCommentOnly(data.toolId, data.comment)));
            }

            environment.useCase.UpdateToolsWithHistory(toolDiffs);

            var comparer = new Func<(long userId, string comment, long toolId), QstComment, bool>((data, parameter) =>
                data.userId == parameter.UserId.ToLong() &&
                data.comment == parameter.Comment.ToDefaultString() &&
                (long)NodeId.Tool == parameter.NodeId &&
                data.toolId == parameter.NodeSeqid);

            CheckerFunctions.CollectionAssertAreEquivalent(datas, environment.mocks.qstCommentDataAccessMock.InsertQstCommentsParameter, comparer);
        }

        [Test]
        public void UpdateToolsWithHistoryCallsCommitAfterWork()
        {
            var environment = new Environment();

            environment.useCase.UpdateToolsWithHistory(new List<ToolDiff>());

            Assert.AreEqual(ToolDataAccessMock.ToolDataAccessFunction.Commit, environment.mocks.toolDataAccess.CalledFunctions.Last());
        }

        [Test]
        public void UpdateToolsWithHistoryReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new List<Tool>();
            environment.mocks.toolDataAccess.UpdateToolsWithHistoryReturnValue = entities;

            var returnValue = environment.useCase.UpdateToolsWithHistory(null);

            Assert.AreSame(entities, returnValue);
        }

        [TestCase(1)]
        [TestCase(132)]
        public void GetLocationToolAssignmentLinkForToolCallsDataAccess(long id)
        {
            var environment = new Environment();

            environment.useCase.GetLocationToolAssignmentLinkForTool(new ToolId(id));

            Assert.AreEqual(id, environment.mocks.toolDataAccess.GetLocationToolAssignmentLinkForToolParameter.ToLong());
        }

        [Test]
        public void GetLocationToolAssignmentLinkForToolReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new List<LocationToolAssignmentReferenceLink>();
            environment.mocks.toolDataAccess.GetLocationToolAssignmentLinkForToolReturnValue = entities;

            Assert.AreSame(entities, environment.useCase.GetLocationToolAssignmentLinkForTool(new ToolId(1)));
        }

        [TestCase("234345")]
        [TestCase("abcdef")]
        public void IsInventoryNumberUniqueCallsDataAccess(string userId)
        {
            var environment = new Environment();

            environment.useCase.IsInventoryNumberUnique(userId);

            Assert.AreEqual(userId, environment.mocks.toolDataAccess.IsInventoryNumberUniqueParameter);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void IsInventoryNumberUniqueReturnsCorrectValue(bool isUnique)
        {
            var environment = new Environment();

            environment.mocks.toolDataAccess.IsInventoryNumberUniqueReturnValue = isUnique;

            Assert.AreEqual(isUnique, environment.useCase.IsInventoryNumberUnique(""));
        }

        [TestCase("asadad")]
        [TestCase("4354576")]
        public void IsSerialNumberUniqueCallsDataAccess(string userId)
        {
            var environment = new Environment();

            environment.useCase.IsSerialNumberUnique(userId);

            Assert.AreEqual(userId, environment.mocks.toolDataAccess.IsSerialNumberUniqueParameter);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void IsSerialNumberUniqueReturnsCorrectValue(bool isUnique)
        {
            var environment = new Environment();

            environment.mocks.toolDataAccess.IsSerialNumberUniqueReturnValue = isUnique;

            Assert.AreEqual(isUnique, environment.useCase.IsSerialNumberUnique(""));
        }

        [TestCase(10)]
        [TestCase(20)]
        public void GetToolCommentCallsDataAccess(long nodeseqid)
        {
            var environment = new Environment();

            environment.useCase.GetToolComment(new ToolId(nodeseqid));

            Assert.AreEqual(NodeId.Tool, environment.mocks.qstCommentDataAccessMock.GetQstCommentByNodeIdAndNodeSeqIdParameterNodeId);
            Assert.AreEqual(nodeseqid, environment.mocks.qstCommentDataAccessMock.GetQstCommentByNodeIdAndNodeSeqIdParameterNodeSeqId);
        }

        [TestCase("Kommentar A")]
        [TestCase("blub2020")]
        public void GetToolCommentReturnsCorrectValue(string comment)
        {
            var environment = new Environment();
            environment.mocks.qstCommentDataAccessMock.GetQstCommentByNodeIdAndNodeSeqIdReturnValue = new HistoryComment(comment);

            environment.useCase.GetToolComment(new ToolId(1));

            Assert.AreEqual(comment, environment.mocks.qstCommentDataAccessMock.GetQstCommentByNodeIdAndNodeSeqIdReturnValue.ToDefaultString());
        }

        [Test]
        public void GetToolCommentReturnsEmptyStringIfCommentIsNull()
        {
            var environment = new Environment();
            environment.mocks.qstCommentDataAccessMock.GetQstCommentByNodeIdAndNodeSeqIdReturnValue = null;

            Assert.AreEqual("", environment.useCase.GetToolComment(new ToolId(1)));
        }

        [TestCase(1, 6)]
        [TestCase(14, 56)]
        public void LoadPictureForToolCallsDataAccess(long locationId, int fileType)
        {
            var environment = new Environment();
            environment.useCase.LoadPictureForTool(new ToolId(locationId), fileType);

            Assert.AreEqual(NodeId.Location, environment.mocks.pictureDataAccessMock.GetQstPictureParameterNodeId);
            Assert.AreEqual(locationId, environment.mocks.pictureDataAccessMock.GetQstPictureParameterFileTypeNodeSeqId);
            Assert.AreEqual(fileType, environment.mocks.pictureDataAccessMock.GetQstPictureParameterFileType);
        }

        [Test]
        public void LoadPictureForToolReturnsCorrectValue()
        {
            var environment = new Environment();

            var picture = new Picture();
            environment.mocks.pictureDataAccessMock.GetQstPictureReturnValue = picture;

            Assert.AreSame(picture, environment.useCase.LoadPictureForTool(new ToolId(1), 1));
        }

        [TestCase(16)]
        [TestCase(14)]
        public void LoadToolsForModelCallsDataAccess(long toolModelId)
        {
            var environment = new Environment();
            environment.useCase.LoadToolsForModel(new ToolModelId(toolModelId));

            Assert.AreEqual(toolModelId, environment.mocks.toolDataAccess.LoadToolsForModelParameter.ToLong());
        }

        [Test]
        public void LoadToolsForModelReturnsCorrectValue()
        {
            var environment = new Environment();

            var tools = new List<Tool>();
            environment.mocks.toolDataAccess.LoadToolsForModelReturnValue = tools;

            Assert.AreSame(tools, environment.useCase.LoadToolsForModel(new ToolModelId(1)));
        }

        [Test]
        public void LoadModelsWithAtLeasOneToolCallsDataAccess()
        {
            var environment = new Environment();

            environment.useCase.LoadModelsWithAtLeasOneTool();

            Assert.IsTrue(environment.mocks.toolDataAccess.LoadModelsWithAtLeasOneToolCalled);
        }

        [Test]
        public void LoadModelsWithAtLeasOneToolReturnsCorrectValue()
        {
            var environment = new Environment();

            var entity = new List<ToolModel>();
            environment.mocks.toolDataAccess.LoadModelsWithAtLeasOneToolReturnValue = entity;

            Assert.AreSame(entity, environment.useCase.LoadModelsWithAtLeasOneTool());
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    toolDataAccess = new ToolDataAccessMock();
                    qstCommentDataAccessMock = new QstCommentDataAccessMock();
                    pictureDataAccessMock = new PictureDataAccessMock();
                }

                public readonly ToolDataAccessMock toolDataAccess;
                public readonly QstCommentDataAccessMock qstCommentDataAccessMock;
                public readonly PictureDataAccessMock pictureDataAccessMock;
            }

            public Environment()
            {
                mocks = new Mocks();
                useCase = new ToolUseCase(mocks.toolDataAccess, mocks.qstCommentDataAccessMock, mocks.pictureDataAccessMock);
            }

            public readonly Mocks mocks;
            public readonly ToolUseCase useCase;
        }
    }
}
