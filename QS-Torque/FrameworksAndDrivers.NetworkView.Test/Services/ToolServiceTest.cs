using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Core.Entities;
using DtoTypes;
using LocationService;
using NUnit.Framework;
using Server.Core.Entities;
using Server.TestHelper.Factories;
using Server.UseCases.UseCases;
using TestHelper.Checker;
using ToolService;
using LocationToolAssignmentReferenceLink = Server.Core.Entities.ReferenceLink.LocationToolAssignmentReferenceLink;
using Picture = Server.Core.Entities.Picture;
using Status = Server.Core.Entities.Status;
using Tool = Server.Core.Entities.Tool;
using ToolDiff = Server.Core.Diffs.ToolDiff;
using ToolModel = Server.Core.Entities.ToolModel;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    public class ToolUseCaseMock : IToolUseCase
    {
        public int LoadToolsParameterIndex { get; set; }
        public int LoadToolsParameterSize { get; set; }
        public List<Tool> LoadToolsReturnValue { get; set; } = new List<Tool>();
        public ToolId GetToolByIdParameter { get; set; }
        public Tool GetToolByIdReturnValue { get; set; } = new Tool();
        public List<ToolDiff> InsertToolsWithHistoryParameterDiff { get; set; }
        public bool InsertToolsWithHistoryParameterReturnList { get; set; }
        public List<Tool> InsertToolsWithHistoryReturnValue { get; set; } = new List<Tool>();
        public List<ToolDiff> UpdateToolsWithHistoryParameter { get; set; }
        public List<Tool> UpdateToolsWithHistoryReturnValue { get; set; } = new List<Tool>();
        public ToolId GetLocationToolAssignmentLinkForToolParameter { get; set; }
        public List<LocationToolAssignmentReferenceLink> GetLocationToolAssignmentLinkForToolReturnValue { get; set; } = new List<LocationToolAssignmentReferenceLink>();
        public string IsSerialNumberUniqueParameter { get; set; }
        public bool IsSerialNumberUniqueReturnValue { get; set; }
        public string IsInventoryNumberUniqueParameter { get; set; }
        public bool IsInventoryNumberUniqueReturnValue { get; set; }
        public string GetToolCommentReturnValue { get; set; }
        public ToolId GetToolCommentParameter { get; set; }
        public Picture LoadPictureForToolReturnValue { get; set; } = new Picture();
        public int LoadPictureForToolParameterFileType { get; set; }
        public ToolId LoadPictureForToolParameterToolId { get; set; }
        public List<Tool> LoadToolsForModelReturnValue { get; set; } = new List<Tool>();
        public ToolModelId LoadToolsForModelParameter { get; set; }
        public List<ToolModel> LoadModelsWithAtLeasOneToolReturnValue { get; set; } = new List<ToolModel>();
        
        public bool LoadModelsWithAtLeasOneToolCalled { get; set; }
        public bool LoadDeletedModelsWithAtLeasOneToolCalled { get; set; }
        public List<ToolModel> LoadDeletedModelsWithAtLeasOneToolReturnValue { get; set; } = new List<ToolModel>();

        public List<Tool> LoadTools(int index, int size)
        {
            LoadToolsParameterIndex = index;
            LoadToolsParameterSize = size;
            return LoadToolsReturnValue;
        }

        public Tool GetToolById(ToolId toolId)
        {
            GetToolByIdParameter = toolId;
            return GetToolByIdReturnValue;
        }

        public List<Tool> InsertToolsWithHistory(List<ToolDiff> toolDiff, bool returnList)
        {
            InsertToolsWithHistoryParameterDiff = toolDiff;
            InsertToolsWithHistoryParameterReturnList = returnList;
            return InsertToolsWithHistoryReturnValue;
        }

        public List<Tool> UpdateToolsWithHistory(List<ToolDiff> toolDiff)
        {
            UpdateToolsWithHistoryParameter = toolDiff;
            return UpdateToolsWithHistoryReturnValue;
        }

        public List<LocationToolAssignmentReferenceLink> GetLocationToolAssignmentLinkForTool(ToolId id)
        {
            GetLocationToolAssignmentLinkForToolParameter = id;
            return GetLocationToolAssignmentLinkForToolReturnValue;
        }

        public bool IsSerialNumberUnique(string serialNumber)
        {
            IsSerialNumberUniqueParameter = serialNumber;
            return IsSerialNumberUniqueReturnValue;
        }

        public bool IsInventoryNumberUnique(string inventoryNumber)
        {
            IsInventoryNumberUniqueParameter = inventoryNumber;
            return IsInventoryNumberUniqueReturnValue;
        }

        public string GetToolComment(ToolId toolId)
        {
            GetToolCommentParameter = toolId;
            return GetToolCommentReturnValue;
        }

        public Picture LoadPictureForTool(ToolId toolId, int fileType)
        {
            LoadPictureForToolParameterToolId = toolId;
            LoadPictureForToolParameterFileType = fileType;
            return LoadPictureForToolReturnValue;
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

        public List<ToolModel> LoadDeletedModelsWithAtLeasOneTool()
        {
            LoadDeletedModelsWithAtLeasOneToolCalled = true;
            return LoadDeletedModelsWithAtLeasOneToolReturnValue;
        }
    }

    public class ToolServiceTest
    {
        [TestCase(1, 7)]
        [TestCase(13, 17)]
        public void LoadToolsCallsUseCase(int index, int size)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);

            var request = new LoadToolsRequest()
            {
                Index = index,
                Size = size
            };

            service.LoadTools(request, null);

            Assert.AreEqual(index, useCase.LoadToolsParameterIndex);
            Assert.AreEqual(size, useCase.LoadToolsParameterSize);
        }

        private static IEnumerable<List<Tool>> toolData = new List<List<Tool>>()
        {
            new List<Tool>()
            {
                Server.TestHelper.Factories.CreateTool.Parameterized(
                    1,
                    "test 1", 
                    "435634",
                    true,
                    CreateToolModel.Randomized(7584392),
                    "a",
                    "b",
                    "c", 
                    "d",
                    new ConfigurableField() {ListId = new HelperTableEntityId(1), Value = new HelperTableEntityValue("f")},
                    new CostCenter(){ListId = new HelperTableEntityId(1), Value = new HelperTableEntityValue("XXX")},
                    new Status(){Id = new StatusId(11), Value = new StatusDescription("V")}),
                Server.TestHelper.Factories.CreateTool.Parameterized(
                    4, 
                    "2233 1", 
                    "abc", 
                    false,
                    CreateToolModel.Randomized(475893), 
                    "Accesory", 
                    "A1", 
                    "A2", 
                    "A3",
                    new ConfigurableField() {ListId = new HelperTableEntityId(14), Value = new HelperTableEntityValue("Conf")},
                    new CostCenter(){ListId = new HelperTableEntityId(14), Value = new HelperTableEntityValue("Cost")},
                    new Status(){Id = new StatusId(87), Value = new StatusDescription("Status")})

            },
            new List<Tool>()
            {
                Server.TestHelper.Factories.CreateTool.Parameterized(
                    134, 
                    "Wkz.1", 
                    "XXX", 
                    true,
                    CreateToolModel.Randomized(74829),
                    "Z", 
                    "1", 
                    "2", 
                    "3",
                    new ConfigurableField() {ListId = new HelperTableEntityId(1), Value = new HelperTableEntityValue("C")},
                    new CostCenter(){ListId = new HelperTableEntityId(14), Value = new HelperTableEntityValue("123")},
                    new Status(){Id = new StatusId(115), Value = new StatusDescription("VD")})
            }
        };

        [TestCaseSource(nameof(toolData))]
        public void LoadToolsReturnsCorrectValue(List<Tool> tools)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);
            useCase.LoadToolsReturnValue = tools;

            var result = service.LoadTools(new LoadToolsRequest(), null);

            var comparer = new Func<Tool, DtoTypes.Tool, bool>((tool, toolDto) =>
                EqualityChecker.CompareToolDtoWithTool(toolDto, tool)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(tools, result.Result.Tools, comparer);
        }

        [TestCase(1)]
        [TestCase(13)]
        public void GetToolByIdCallsUseCase(long toolId)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);
            useCase.GetToolByIdReturnValue = GetToolByIdData.ToList().First();

            service.GetToolById(new Long() {Value = toolId}, null);

            Assert.AreEqual(toolId, useCase.GetToolByIdParameter.ToLong());
        }

        private static IEnumerable<Tool> GetToolByIdData = new List<Tool>()
        {
            Server.TestHelper.Factories.CreateTool.Parameterized(1, "test 1", "435634", true,
                CreateToolModel.Randomized(435637), "a", "b", "c", "d",
                new ConfigurableField() {ListId = new HelperTableEntityId(1), Value = new HelperTableEntityValue("f")},
                new CostCenter(){ListId = new HelperTableEntityId(1), Value = new HelperTableEntityValue("XXX")},
                new Status(){Id = new StatusId(11), Value = new StatusDescription("V")}),
            Server.TestHelper.Factories.CreateTool.Parameterized(4, "2233 1", "abc", false,
                CreateToolModel.Randomized(96392), "Accesory", "A1", "A2", "A3",
                new ConfigurableField() {ListId = new HelperTableEntityId(14), Value = new HelperTableEntityValue("Conf")},
                new CostCenter(){ListId = new HelperTableEntityId(14), Value = new HelperTableEntityValue("Cost")},
                new Status(){Id = new StatusId(87), Value = new StatusDescription("Status")})
        };

        [TestCaseSource(nameof(GetToolByIdData))]
        public void GetToolByIdReturnsCorrectValue(Tool tool)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);
            useCase.GetToolByIdReturnValue = tool;

            var result = service.GetToolById(new Long(), null);

            Assert.IsTrue(EqualityChecker.CompareToolDtoWithTool(result.Result, tool));
        }

        static IEnumerable<(ListOfToolDiffs, bool)> InsertUpdateToolWithHistoryData = new List<(ListOfToolDiffs, bool)>
        {
            (
                new ListOfToolDiffs()
                {
                    ToolDiffs =
                    {
                        new DtoTypes.ToolDiff()
                        {
                            UserId = 1,
                            Comment = "",
                            OldTool = DtoFactory.CreateToolDto(1, "serial 123", "inventory 123", true,
                                "abcX", "F11", "F12", "F13", "233a",
                                new DtoTypes.Status(){Id = 341, Description="CX", Alive = false},
                                new DtoTypes.HelperTableEntity(){ListId = 134,Value="C11",Alive = true,NodeId = 15},
                                new DtoTypes.HelperTableEntity(){ListId = 131, Value="K1213",Alive = true, NodeId = 1},
                                DtoFactory.CreateToolModelDtoRandomized(567)),
                            NewTool = DtoFactory.CreateToolDto(1, "serial ABC", "inventory XYZ", false,
                                "abc", "F1", "F2", "F3", "a",
                                new DtoTypes.Status(){Id = 1, Description="S", Alive = true},
                                new DtoTypes.HelperTableEntity(){ListId = 1,Value="C",Alive = true,NodeId = 5},
                                new DtoTypes.HelperTableEntity(){ListId = 11, Value="K",Alive = false, NodeId = 15},
                                DtoFactory.CreateToolModelDtoRandomized(324895))
                        },
                        new DtoTypes.ToolDiff()
                        {
                            UserId = 2,
                            Comment = "ABCDEFG",
                            OldTool =  DtoFactory.CreateToolDto(99, "serial ABC", "inventory XYZ", false,
                                "abc", "F1", "F2", "F3", "a",
                                new DtoTypes.Status(){Id = 1, Description="S", Alive = true},
                                new DtoTypes.HelperTableEntity(){ListId = 1,Value="C",Alive = true,NodeId = 5},
                                new DtoTypes.HelperTableEntity(){ListId = 11, Value="K",Alive = false, NodeId = 15},
                                DtoFactory.CreateToolModelDtoRandomized(1238)),
                            NewTool = DtoFactory.CreateToolDto(99, "serial 123", "inventory 123", true,
                                "abcX", "F11", "F12", "F13", "233a",
                                new DtoTypes.Status(){Id = 341, Description="CX", Alive = false},
                                new DtoTypes.HelperTableEntity(){ListId = 134,Value="C11",Alive = true,NodeId = 15},
                                new DtoTypes.HelperTableEntity(){ListId = 131, Value="K1213",Alive = true, NodeId = 1},
                                DtoFactory.CreateToolModelDtoRandomized(468648))
                        }
                    }
                },
                true
             ),
             (
                new ListOfToolDiffs()
                {
                     ToolDiffs = 
                    {
                        new DtoTypes.ToolDiff()
                        {
                            UserId = 1,
                            Comment = "",
                            OldTool =   DtoFactory.CreateToolDto(22, "XXX ABC", "35346457 XYZ", false,
                                "abc", "F9", "A2", "D3", "a",
                                new DtoTypes.Status(){Id = 21, Description="S", Alive = true},
                                new DtoTypes.HelperTableEntity(){ListId = 4,Value="$C",Alive = true,NodeId = 53},
                                new DtoTypes.HelperTableEntity(){ListId = 112, Value="F",Alive = true, NodeId = 153},
                                DtoFactory.CreateToolModelDtoRandomized(479814)),
                            NewTool =  DtoFactory.CreateToolDto(22, "serial ABC", "inventory XYZ", false,
                                "abc", "F1", "F2", "F3", "a",
                                new DtoTypes.Status(){Id = 1, Description="S", Alive = true},
                                new DtoTypes.HelperTableEntity(){ListId = 1,Value="C",Alive = true,NodeId = 5},
                                new DtoTypes.HelperTableEntity(){ListId = 11, Value="K",Alive = false, NodeId = 15},
                                DtoFactory.CreateToolModelDtoRandomized(9434)),
                        }
                    }
                },
                false
            )
        };


        [TestCaseSource(nameof(InsertUpdateToolWithHistoryData))]
        public void InsertToolsWithHistoryCallsUseCase((ListOfToolDiffs toolDiffs, bool returnList) data)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);

            var request = new InsertToolsWithHistoryRequest()
            {
                ToolDiffs = data.toolDiffs,
                ReturnList = data.returnList
            };

            service.InsertToolsWithHistory(request, null);

            Assert.AreEqual(data.returnList, useCase.InsertToolsWithHistoryParameterReturnList);

            var comparer = new Func<DtoTypes.ToolDiff, ToolDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareToolDtoWithTool(dtoDiff.OldTool, diff.GetOldTool()) &&
                EqualityChecker.CompareToolDtoWithTool(dtoDiff.NewTool, diff.GetNewTool())
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.toolDiffs.ToolDiffs, useCase.InsertToolsWithHistoryParameterDiff, comparer);
        }

        [TestCaseSource(nameof(toolData))]
        public void InsertToolsWithHistoryReturnsCorrectValue(List<Tool> tools)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);

            useCase.InsertToolsWithHistoryReturnValue = tools;

            var request = new InsertToolsWithHistoryRequest()
            {
                ToolDiffs = new ListOfToolDiffs()
            };

            var result = service.InsertToolsWithHistory(request, null).Result;

            var comparer = new Func<Tool, DtoTypes.Tool, bool>((tool, dtoTool) =>
                EqualityChecker.CompareToolDtoWithTool(dtoTool, tool)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(tools, result.Tools, comparer);
        }

        [TestCaseSource(nameof(InsertUpdateToolWithHistoryData))]
        public void UpdateToolsWithHistoryCallsUseCase((ListOfToolDiffs toolDiffs, bool returnList) data)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);

            var request = new UpdateToolsWithHistoryRequest()
            {
                ToolDiffs = data.toolDiffs
            };

            service.UpdateToolsWithHistory(request, null);

            var comparer = new Func<DtoTypes.ToolDiff, ToolDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareToolDtoWithTool(dtoDiff.OldTool, diff.GetOldTool()) &&
                EqualityChecker.CompareToolDtoWithTool(dtoDiff.NewTool, diff.GetNewTool())
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.toolDiffs.ToolDiffs,
                useCase.UpdateToolsWithHistoryParameter, comparer);
        }

        [TestCaseSource(nameof(toolData))]
        public void UpdateToolsWithHistoryReturnsCorrectValue(List<Tool> tools)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);

            useCase.UpdateToolsWithHistoryReturnValue = tools;

            var request = new UpdateToolsWithHistoryRequest()
            {
                ToolDiffs = new ListOfToolDiffs()
            };

            var result = service.UpdateToolsWithHistory(request, null).Result;

            var comparer = new Func<Tool, DtoTypes.Tool, bool>((tool, dtoTool) =>
                EqualityChecker.CompareToolDtoWithTool(dtoTool, tool)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(tools, result.Tools, comparer);
        }

        [TestCase(1)]
        [TestCase(99)]
        public void GetLocationToolAssignmentLinkForToolCallsUseCase(long toolId)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);

            service.GetLocationToolAssignmentLinkForTool(new Long() { Value = toolId }, null);

            Assert.AreEqual(toolId, useCase.GetLocationToolAssignmentLinkForToolParameter.ToLong());
        }

        private static IEnumerable<List<LocationToolAssignmentReferenceLink>> locationToolLinkData = new List<List<LocationToolAssignmentReferenceLink>>()
        {
            new List<LocationToolAssignmentReferenceLink>()
            {
                new LocationToolAssignmentReferenceLink(new QstIdentifier(1), 
                    new LocationDescription("21435"), new LocationNumber("435456"),"tool 1234",
                    "0349564", new LocationId(12), new ToolId(12)),
                new LocationToolAssignmentReferenceLink(new QstIdentifier(2),
                    new LocationDescription("Test 1"), new LocationNumber("abcdef"),"tool 9999",
                    "6663736", new LocationId(1112), new ToolId(102)),
            },
            new List<LocationToolAssignmentReferenceLink>()
            {
                new LocationToolAssignmentReferenceLink(new QstIdentifier(5),
                    new LocationDescription("AAA"), new LocationNumber("BBB"),"tool 55555",
                    "1234567", new LocationId(1111), new ToolId(92)),
            }
        };

        [TestCaseSource(nameof(locationToolLinkData))]
        public void GetLocationToolAssignmentLinkForToolReturnsCorrectValue(List<LocationToolAssignmentReferenceLink> locationToolReferenceLink)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);
            useCase.GetLocationToolAssignmentLinkForToolReturnValue = locationToolReferenceLink;

            var result = service.GetLocationToolAssignmentLinkForTool(new Long(), null);

            var comparer = new Func<LocationToolAssignmentReferenceLink, DtoTypes.LocationToolAssignmentReferenceLink, bool>((locationToolLink, dtoLocationToolLink) =>
                locationToolLink.Id.ToLong() == dtoLocationToolLink.Id &&
                locationToolLink.LocationId.ToLong() == dtoLocationToolLink.LocationId &&
                locationToolLink.LocationName.ToDefaultString() == dtoLocationToolLink.LocationName &&
                locationToolLink.LocationNumber.ToDefaultString() == dtoLocationToolLink.LocationNumber &&
                locationToolLink.ToolId.ToLong() == dtoLocationToolLink.ToolId &&
                locationToolLink.ToolInventoryNumber == dtoLocationToolLink.ToolInventoryNumber &&
                locationToolLink.ToolSerialNumber == dtoLocationToolLink.ToolSerialNumber
            );

            CheckerFunctions.CollectionAssertAreEquivalent(locationToolReferenceLink, result.Result.Links, comparer);
        }

        [TestCase("1234")]
        [TestCase("Tool 123")]
        public void IsInventoryNumberUniqueCallsUseCase(string inventoryNumber)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);

            service.IsInventoryNumberUnique(new BasicTypes.String() { Value = inventoryNumber }, null);

            Assert.AreEqual(inventoryNumber, useCase.IsInventoryNumberUniqueParameter);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsInventoryNumberUniqueReturnsCorrectValue(bool isUnique)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);
            useCase.IsInventoryNumberUniqueReturnValue = isUnique;

            var result = service.IsInventoryNumberUnique(new BasicTypes.String() { Value = "" }, null);

            Assert.AreEqual(isUnique, result.Result.Value);
        }

        [TestCase("Tool 23445346")]
        [TestCase("123")]
        public void IsSerialNumberUniqueCallsUseCase(string serialNumber)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);

            service.IsSerialNumberUnique(new BasicTypes.String() { Value = serialNumber }, null);

            Assert.AreEqual(serialNumber, useCase.IsSerialNumberUniqueParameter);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsSerialNumberUniqueReturnsCorrectValue(bool isUnique)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);
            useCase.IsSerialNumberUniqueReturnValue = isUnique;

            var result = service.IsSerialNumberUnique(new BasicTypes.String() { Value = "" }, null);

            Assert.AreEqual(isUnique, result.Result.Value);
        }

        [TestCase(10)]
        [TestCase(99)]
        public void GetToolCommentCallsUseCase(long toolId)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);
            useCase.GetToolCommentReturnValue = "";

            service.GetToolComment(new Long() { Value = toolId }, null);

            Assert.AreEqual(toolId, useCase.GetToolCommentParameter.ToLong());
        }

        [TestCase("blub2020")]
        [TestCase("Testkommentar")]
        public void GetToolCommentReturnsCorrectValue(string comment)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);
            useCase.GetToolCommentReturnValue = comment;

            var result = service.GetToolComment(new Long() { Value = 1 }, null);

            Assert.AreEqual(comment, result.Result.Value);
        }

        [TestCase(12, 45)]
        [TestCase(132, 4445)]
        public void LoadPictureForToolCallsUseCase(long toolId, int fileType)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);
            useCase.LoadPictureForToolReturnValue = new Picture()
            {
                FileName = ""
            };

            var request = new LoadPictureForToolRequest()
            {
                ToolId = toolId,
                FileType = fileType,
            };
            service.LoadPictureForTool(request, null);

            Assert.AreEqual(toolId, useCase.LoadPictureForToolParameterToolId.ToLong());
            Assert.AreEqual(fileType, useCase.LoadPictureForToolParameterFileType);
        }

        private static IEnumerable<Picture> LoadPictureForToolReturnsCorrectValueData =
            new List<Picture>()
            {
                new Picture()
                {
                    NodeId = 12,
                    FileName = "234536",
                    FileType = 1,
                    NodeSeqId = 2,
                    SeqId = 1,
                    PictureBytes = new byte[]{12,23,5,6,7}
                },
                new Picture()
                {
                    NodeId = 88,
                    FileName = "path",
                    FileType = 12,
                    NodeSeqId = 32,
                    SeqId = 14,
                    PictureBytes = new byte[]{1,2,3,5,6,12,23,5,6,7}
                }
            };

        [TestCaseSource(nameof(LoadPictureForToolReturnsCorrectValueData))]
        public void LoadPictureForToolReturnsCorrectValue(Picture picture)
        {
            var useCase = new LocationUseCaseMock();
            var service = new NetworkView.Services.LocationService(null, useCase);
            useCase.LoadPictureForLocationReturnValue = picture;

            var result = service.LoadPictureForLocation(new LoadPictureForLocationRequest(), null);

            Assert.AreEqual(picture.SeqId, result.Result.Picture.Id);
            Assert.AreEqual(picture.NodeId, result.Result.Picture.Nodeid);
            Assert.AreEqual(picture.FileName, result.Result.Picture.FileName.Value);
            Assert.AreEqual(picture.FileType, result.Result.Picture.FileType);
            Assert.AreEqual(picture.NodeSeqId, result.Result.Picture.Nodeseqid);
            Assert.AreEqual(picture.PictureBytes, result.Result.Picture.Image.ToByteArray());
        }

        [TestCase(1)]
        [TestCase(13)]
        public void LoadToolsForModelCallsUseCase(int modelId)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);

            var request = new Long()
            {
                Value = modelId
            };

            service.LoadToolsForModel(request, null);

            Assert.AreEqual(modelId, useCase.LoadToolsForModelParameter.ToLong());
        }


        [TestCaseSource(nameof(toolData))]
        public void LoadToolsForModelReturnsCorrectValue(List<Tool> tools)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);
            useCase.LoadToolsForModelReturnValue = tools;

            var result = service.LoadToolsForModel(new Long(), null);

            var comparer = new Func<Tool, DtoTypes.Tool, bool>((tool, toolDto) =>
                EqualityChecker.CompareToolDtoWithTool(toolDto, tool)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(tools, result.Result.Tools, comparer);
        }

        [Test]
        public void LoadModelsWithAtLeasOneToolCallsUseCase()
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);

            service.LoadModelsWithAtLeasOneTool(new NoParams(), null);

            Assert.IsTrue(useCase.LoadModelsWithAtLeasOneToolCalled);
        }

        private static IEnumerable<List<ToolModel>> LoadModelsWithAtLeasOneToolReturnsCorrectValueData =
            new List<List<ToolModel>>()
            {
                new List<ToolModel>()
                {
                    CreateToolModel.Randomized(55623),
                    CreateToolModel.Randomized(19541654)
                },
                new List<ToolModel>()
                {
                    CreateToolModel.Randomized(22824)
                }
            };


        [TestCaseSource(nameof(LoadModelsWithAtLeasOneToolReturnsCorrectValueData))]
        public void LoadModelsWithAtLeasOneToolReturnsCorrectValue(List<ToolModel> data)
        {
            var useCase = new ToolUseCaseMock();
            var service = new NetworkView.Services.ToolService(null, useCase);
            useCase.LoadModelsWithAtLeasOneToolReturnValue = data;

            var result = service.LoadModelsWithAtLeasOneTool(new NoParams(), null);

            var comparer = new Func<ToolModel, DtoTypes.ToolModel, bool>((model, modelDto) =>
                EqualityChecker.CompareToolModelDtoWithToolModel(modelDto, model)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data, result.Result.ToolModels, comparer);
        }
    }
}
