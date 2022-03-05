using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BasicTypes;
using Client.TestHelper.Mock;
using Core.Entities;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Google.Protobuf;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;
using ToolService;
using LocationToolAssignmentReferenceLink = DtoTypes.LocationToolAssignmentReferenceLink;
using String = BasicTypes.String;
using Tool = DtoTypes.Tool;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    public class ToolClientMock : IToolClient
    {
        public LoadToolsRequest LoadToolsParameter { get; set; }
        public ListOfTools LoadToolsReturnValue { get; set; } = new ListOfTools();
        public Long GetToolByIdParameter { get; set; }
        public Tool GetToolByIdReturnValue { get; set; } = new Tool();
        public ListOfTools InsertToolsWithHistoryReturnValue { get; set; } = new ListOfTools();
        public InsertToolsWithHistoryRequest InsertToolsWithHistoryParameter { get; set; }
        public ListOfTools UpdateToolsWithHistoryReturnValue { get; set; } = new ListOfTools();
        public UpdateToolsWithHistoryRequest UpdateToolsWithHistoryParameter { get; set; }
        public ListOfLocationToolAssignmentReferenceLink GetLocationToolAssignmentLinkForToolReturnValue { get; set; } = new ListOfLocationToolAssignmentReferenceLink();
        public Long GetLocationToolAssignmentLinkForToolParameter { get; set; }
        public Bool IsSerialNumberUniqueReturnValue { get; set; } = new Bool();
        public String IsSerialNumberUniqueParameter { get; set; }
        public Bool IsInventoryNumberUniqueReturnValue { get; set; } = new Bool();
        public String IsInventoryNumberUniqueParameter { get; set; }
        public String GetToolCommentReturnValue { get; set; } = new String();
        public Long GetToolCommentParameter { get; set; }
        public LoadPictureForToolResponse LoadPictureForToolReturnValue { get; set; } = new LoadPictureForToolResponse();
        public LoadPictureForToolRequest LoadPictureForToolParameter { get; set; }
        public ListOfTools LoadToolsForModelReturnValue { get; set; } = new ListOfTools();
        public Long LoadToolsForModelParameter { get; set; }
        public ListOfToolModel LoadModelsWithAtLeasOneToolReturnValue { get; set; } = new ListOfToolModel();
        public bool LoadModelsWithAtLeasOneToolCalled { get; set; }

        public bool LoadDeletedModelsWithAtLeasOneToolCalled { get; set; }
        public ListOfToolModel LoadDeletedModelsWithAtLeasOneToolReturnValue { get; set; } = new ListOfToolModel();

        public ListOfTools LoadTools(LoadToolsRequest request)
        {
            LoadToolsParameter = request;
            return LoadToolsReturnValue;
        }

        public Tool GetToolById(Long toolId)
        {
            GetToolByIdParameter = toolId;
            return GetToolByIdReturnValue;
        }

        public ListOfTools InsertToolsWithHistory(InsertToolsWithHistoryRequest request)
        {
            InsertToolsWithHistoryParameter = request;
            return InsertToolsWithHistoryReturnValue;
        }

        public ListOfTools UpdateToolsWithHistory(UpdateToolsWithHistoryRequest request)
        {
            UpdateToolsWithHistoryParameter = request;
            return UpdateToolsWithHistoryReturnValue;
        }

        public ListOfLocationToolAssignmentReferenceLink GetLocationToolAssignmentLinkForTool(Long locationToolAssignmentId)
        {
            GetLocationToolAssignmentLinkForToolParameter = locationToolAssignmentId;
            return GetLocationToolAssignmentLinkForToolReturnValue;
        }

        public Bool IsSerialNumberUnique(String serialNumber)
        {
            IsSerialNumberUniqueParameter = serialNumber;
            return IsSerialNumberUniqueReturnValue;
        }

        public Bool IsInventoryNumberUnique(String inventoryNumber)
        {
            IsInventoryNumberUniqueParameter = inventoryNumber;
            return IsInventoryNumberUniqueReturnValue;
        }

        public String GetToolComment(Long toolId)
        {
            GetToolCommentParameter = toolId;
            return GetToolCommentReturnValue;
        }

        public LoadPictureForToolResponse LoadPictureForTool(LoadPictureForToolRequest request)
        {
            LoadPictureForToolParameter = request;
            return LoadPictureForToolReturnValue;
        }

        public ListOfTools LoadToolsForModel(Long modelId)
        {
            LoadToolsForModelParameter = modelId;
            return LoadToolsForModelReturnValue;
        }

        public ListOfToolModel LoadModelsWithAtLeasOneTool()
        {
            LoadModelsWithAtLeasOneToolCalled = true;
            return LoadModelsWithAtLeasOneToolReturnValue;
        }

        public ListOfToolModel LoadDeletedModelsWithAtLeastOneTool()
        {
            LoadDeletedModelsWithAtLeasOneToolCalled = true;
            return LoadDeletedModelsWithAtLeasOneToolReturnValue;
        }
    }

    public class ToolDataAccessTest
    {
        [Test]
        public void AddToolWithoutUserThrowsArgumentException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.AddTool(null, null); });
        }

        static IEnumerable<(Core.Entities.Tool, Core.Entities.User)> AddAndRemoveToolData = new List<(Core.Entities.Tool, Core.Entities.User)>()
        {
            (
                CreateTool.Parameterized(1, "1234", "2344576", CreateToolModel.WithId(1), "abcd", "A", "B",
                    "C", new ConfigurableField() {ListId = new HelperTableEntityId(1), Value = new HelperTableDescription("99")},
                    new CostCenter() {ListId = new HelperTableEntityId(99), Value = new HelperTableDescription("Cost Center")},
                    new Core.Entities.Status(){ListId = new HelperTableEntityId(35), Value = new StatusDescription("Status 834")}),
                CreateUser.IdOnly(2)
            ),
            (
                CreateTool.Parameterized(67, "inv", "ser", CreateToolModel.WithId(31), "acc", "C1", "C2",
                    "C3", new ConfigurableField() {ListId = new HelperTableEntityId(14), Value = new HelperTableDescription("43")},
                    new CostCenter() {ListId = new HelperTableEntityId(11), Value = new HelperTableDescription("Cost Center AV")},
                    new Core.Entities.Status(){ListId = new HelperTableEntityId(1223), Value = new StatusDescription("Status X")}),
                CreateUser.IdOnly(32)
            )
        };

        [TestCaseSource(nameof(AddAndRemoveToolData))]
        public void AddToolCallsClient((Core.Entities.Tool tool, Core.Entities.User user) data)
        {
            var environment = new Environment();
            environment.mocks.toolClient.InsertToolsWithHistoryReturnValue = new ListOfTools() { Tools = { new Tool(){Id = data.tool.Id.ToLong()} } };

            environment.dataAccess.AddTool(data.tool, data.user);

            var clientParam = environment.mocks.toolClient.InsertToolsWithHistoryParameter;
            var clientDiff = clientParam.ToolDiffs.ToolDiffs.First();

            Assert.AreEqual(1, clientParam.ToolDiffs.ToolDiffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.AreEqual("", clientDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareToolWithToolDto(data.tool, clientDiff.NewTool));
            Assert.IsNull(clientDiff.OldTool);
            Assert.IsTrue(clientParam.ReturnList);
        }


        [TestCase(1)]
        [TestCase(99)]
        public void AddToolReturnsCorrectValue(long toolId)
        {
            var environment = new Environment();
            environment.mocks.toolClient.InsertToolsWithHistoryReturnValue = new ListOfTools() {Tools = { new Tool(){Id = toolId } }};
            var tool = CreateTool.Anonymous();
            var result = environment.dataAccess.AddTool(tool, CreateUser.Anonymous());

            Assert.AreSame(tool, result);
            Assert.AreEqual(toolId, result.Id.ToLong());
        }

        [Test]
        public void AddToolReturnsNullThrowsException()
        {
            var environment = new Environment();
            Assert.Throws<NullReferenceException>(() =>
            {
                environment.dataAccess.AddTool(new Core.Entities.Tool(), CreateUser.Anonymous());
            });
        }

        [Test]
        public void RemoveToolWithoutUserThrowsArgumentException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.RemoveTool(null, null); });
        }

        [TestCaseSource(nameof(AddAndRemoveToolData))]
        public void RemoveToolCallsClient((Core.Entities.Tool tool, Core.Entities.User user) data)
        {
            var environment = new Environment();
            environment.mocks.toolClient.UpdateToolsWithHistoryReturnValue =
                new ListOfTools() { Tools = { new Tool() } };

            environment.dataAccess.RemoveTool(data.tool, data.user);

            var clientParam = environment.mocks.toolClient.UpdateToolsWithHistoryParameter;
            var clientDiff = clientParam.ToolDiffs.ToolDiffs.First();

            Assert.AreEqual(1, clientParam.ToolDiffs.ToolDiffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.AreEqual("", clientDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareToolWithToolDto(data.tool, clientDiff.NewTool));
            Assert.IsTrue(EqualityChecker.CompareToolWithToolDto(data.tool, clientDiff.OldTool));
            Assert.AreEqual(true, clientDiff.OldTool.Alive);
            Assert.AreEqual(false, clientDiff.NewTool.Alive);
        }

        [Test]
        public void UpdateToolWithMismatchingIdsThrowsArgumentException()
        {
            var environment = new Environment();
            var diff = new Core.UseCases.ToolDiff(CreateUser.Anonymous(), new HistoryComment(""),
                CreateTool.WithId(1), CreateTool.WithId(2) );

            Assert.Throws<ArgumentException>(() => { environment.dataAccess.UpdateTool(diff); });
        }

        static IEnumerable<(Core.Entities.Tool, Core.Entities.Tool, Core.Entities.User, string)> SaveToolData =
            new List<(Core.Entities.Tool, Core.Entities.Tool, Core.Entities.User, string)>()
        {
            (
                CreateTool.Parameterized(1, "1234", "2344576", CreateToolModel.WithId(1), "abcd", "A", "B",
                    "C", new ConfigurableField() {ListId = new HelperTableEntityId(1), Value = new HelperTableDescription("99")},
                    new CostCenter() {ListId = new HelperTableEntityId(99), Value = new HelperTableDescription("Cost Center")},
                    new Core.Entities.Status(){ListId = new HelperTableEntityId(35), Value = new StatusDescription("Status 834")}),
                CreateTool.Parameterized(1, "inv", "ser", CreateToolModel.WithId(31), "acc", "C1", "C2",
                    "C3", new ConfigurableField() {ListId = new HelperTableEntityId(14), Value = new HelperTableDescription("43")},
                    new CostCenter() {ListId = new HelperTableEntityId(11), Value = new HelperTableDescription("Cost Center AV")},
                    new Core.Entities.Status(){ListId = new HelperTableEntityId(1223), Value = new StatusDescription("Status X")}),
                CreateUser.IdOnly(2),
                "Kommentar fehlt!"
            ),
            (
                CreateTool.Parameterized(13, "abc", "def", CreateToolModel.WithId(21), "AC", "A", "B",
                    "C", new ConfigurableField() {ListId = new HelperTableEntityId(1), Value = new HelperTableDescription("99")},
                    new CostCenter() {ListId = new HelperTableEntityId(11), Value = new HelperTableDescription("Cost-Center")},
                    new Core.Entities.Status(){ListId = new HelperTableEntityId(35), Value = new StatusDescription("Status 834")}),
                CreateTool.Parameterized(13, "inv", "ser", CreateToolModel.WithId(31), "acc", "C1", "C2",
                    "C3", new ConfigurableField() {ListId = new HelperTableEntityId(143), Value = new HelperTableDescription("43")},
                    new CostCenter() {ListId = new HelperTableEntityId(131), Value = new HelperTableDescription("Cost Center AV")},
                    new Core.Entities.Status(){ListId = new HelperTableEntityId(9432), Value = new StatusDescription("I.O.")}),
                CreateUser.IdOnly(20),
                "Werkzeug funktioniert"
            )
        };

        [TestCaseSource(nameof(SaveToolData))]
        public void UpdateToolCallsClient((Core.Entities.Tool oldTool, Core.Entities.Tool newTool, Core.Entities.User user, string comment) data)
        {
            var environment = new Environment();
            var diff = new Core.UseCases.ToolDiff(data.user, new HistoryComment(data.comment),
                data.oldTool, data.newTool);
            environment.dataAccess.UpdateTool(diff);

            var clientParam = environment.mocks.toolClient.UpdateToolsWithHistoryParameter;
            var clientDiff = clientParam.ToolDiffs.ToolDiffs.First();

            Assert.AreEqual(1, clientParam.ToolDiffs.ToolDiffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.IsTrue(EqualityChecker.CompareToolWithToolDto(data.newTool, clientDiff.NewTool));
            Assert.IsTrue(EqualityChecker.CompareToolWithToolDto(data.oldTool, clientDiff.OldTool));
            Assert.AreEqual(data.comment, clientDiff.Comment);
            Assert.AreEqual(true, clientDiff.NewTool.Alive);
            Assert.AreEqual(true, clientDiff.NewTool.Alive);
        }

        [TestCase("12345")]
        [TestCase("abcd")]
        public void IsSerialNumberUniqueCallsClient(string number)
        {
            var environment = new Environment();
            environment.dataAccess.IsSerialNumberUnique(number);

            Assert.AreEqual(number, environment.mocks.toolClient.IsSerialNumberUniqueParameter.Value);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsSerialNumberUniqueReturnsCorrectValue(bool isUnique)
        {
            var environment = new Environment();
            environment.mocks.toolClient.IsSerialNumberUniqueReturnValue = new Bool() { Value = isUnique };
            environment.dataAccess.IsSerialNumberUnique("");

            Assert.AreEqual(isUnique, environment.dataAccess.IsSerialNumberUnique(""));
        }

        [TestCase("12345")]
        [TestCase("abcd")]
        public void IsInventoryNumberUniqueCallsClient(string number)
        {
            var environment = new Environment();
            environment.dataAccess.IsInventoryNumberUnique(number);

            Assert.AreEqual(number, environment.mocks.toolClient.IsInventoryNumberUniqueParameter.Value);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsInventoryNumberUniqueReturnsCorrectValue(bool isUnique)
        {
            var environment = new Environment();
            environment.mocks.toolClient.IsInventoryNumberUniqueReturnValue = new Bool() { Value = isUnique };

            Assert.AreEqual(isUnique, environment.dataAccess.IsInventoryNumberUnique(""));
        }

        [TestCase(1)]
        [TestCase(100)]
        public void LoadLocationToolAssignmentLinksForToolIdCallsClient(long toolId)
        {
            var environment = new Environment();
            environment.dataAccess.LoadLocationToolAssignmentLinksForToolId(new ToolId(toolId));
            Assert.AreEqual(toolId, environment.mocks.toolClient.GetLocationToolAssignmentLinkForToolParameter.Value);
        }

        static IEnumerable<ListOfLocationToolAssignmentReferenceLink> LoadLocationToolAssignmentLinksForToolIdData = 
            new List<ListOfLocationToolAssignmentReferenceLink>()
        {
            new ListOfLocationToolAssignmentReferenceLink()
            {
                Links =
                {
                    CreateLocationToolAssignmentReferenceLink(1, 12,"12345",
                        "SST 1", 1, "12345", "546567"),
                    CreateLocationToolAssignmentReferenceLink(12, 1232,"xxx1",
                        "SST 121", 134, "abcdef", "0983527")

                }
            },
            new ListOfLocationToolAssignmentReferenceLink()
            {
                Links =
                {
                    CreateLocationToolAssignmentReferenceLink(122, 345,"Airbag 1",
                        "Airbag 1", 13, "X324", "D658")
                }
            }
        };

        [TestCaseSource(nameof(LoadLocationToolAssignmentLinksForToolIdData))]
        public void LoadLocationToolAssignmentLinksForToolIdReturnsCorrectValue(ListOfLocationToolAssignmentReferenceLink locationToolLinks)
        {
            var environment = new Environment();
            environment.mocks.toolClient.GetLocationToolAssignmentLinkForToolReturnValue = locationToolLinks;
            var result = environment.dataAccess.LoadLocationToolAssignmentLinksForToolId(new ToolId(1));

            var comparer = new Func<LocationToolAssignmentReferenceLink, Core.Entities.ReferenceLink.LocationToolAssignmentReferenceLink, bool>(
                (dtoLocationToolLink, locationToolLink) =>
                    locationToolLink.Id.ToLong() == dtoLocationToolLink.Id &&
                    locationToolLink.LocationId.ToLong() == dtoLocationToolLink.LocationId &&
                    locationToolLink.LocationName.ToDefaultString() == dtoLocationToolLink.LocationName &&
                    locationToolLink.LocationNumber.ToDefaultString() == dtoLocationToolLink.LocationNumber &&
                    locationToolLink.ToolId.ToLong() == dtoLocationToolLink.ToolId &&
                    locationToolLink.ToolInventoryNumber == dtoLocationToolLink.ToolInventoryNumber &&
                    locationToolLink.ToolSerialNumber == dtoLocationToolLink.ToolSerialNumber
            );

            CheckerFunctions.CollectionAssertAreEquivalent(locationToolLinks.Links, result, comparer);
        }

        [TestCase(145)]
        [TestCase(678)]
        public void LoadCommentCallsClient(long toolId)
        {
            var environment = new Environment();
            environment.mocks.toolClient.GetToolCommentReturnValue = new String();
            environment.dataAccess.LoadComment(CreateTool.WithId(toolId));

            Assert.AreEqual(toolId, environment.mocks.toolClient.GetToolCommentParameter.Value);
        }

        [TestCase("Kommentar 7364")]
        [TestCase("Hersteller")]
        public void LoadCommentReturnsCorrectValue(string comment)
        {
            var environment = new Environment();
            environment.mocks.toolClient.GetToolCommentReturnValue = new String() { Value = comment };

            var result = environment.dataAccess.LoadComment(CreateTool.Anonymous());

            Assert.AreEqual(comment, result);
        }

        [Test]
        public void LoadPictureForToolWithoutToolThrowsArgumentNullException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentNullException>(() => { environment.dataAccess.LoadPictureForTool(null); });
        }

        [TestCase(1)]
        [TestCase(13)]
        public void LoadPictureForToolCallsClient(long toolId)
        {
            var environment = new Environment();
            environment.dataAccess.LoadPictureForTool(CreateTool.WithId(toolId));

            Assert.AreEqual(toolId, environment.mocks.toolClient.LoadPictureForToolParameter.ToolId);
            Assert.AreEqual(0, environment.mocks.toolClient.LoadPictureForToolParameter.FileType);
        }

        static IEnumerable<(byte[], DtoTypes.Picture)> LoadPictureForToolReturnsCorrectValueData = new List<(byte[], DtoTypes.Picture)>()
        {
            (
                new byte[] {1,2,56,34,67,76},
                new DtoTypes.Picture()
                {
                    Id = 1,
                    FileName = new NullableString() {IsNull = false, Value = "Test1"},
                    FileType = 0,
                    Nodeid = 12,
                    Nodeseqid = 123
                }
            ),
            (
                new byte[] {13,42,56,33,22,16},
                new DtoTypes.Picture()
                {
                    Id = 14,
                    FileName = new NullableString() {IsNull = false, Value = "Path"},
                    FileType = 10,
                    Nodeid = 12222,
                    Nodeseqid = 12
                }
            )
        };

        [TestCaseSource(nameof(LoadPictureForToolReturnsCorrectValueData))]
        public void LoadPictureForToolReturnsCorrectValue((byte[] pict, DtoTypes.Picture pictDto) data)
        {
            var environment = new Environment();

            var picture = new Core.Entities.Picture { ImageStream = new MemoryStream(data.pict) };
            environment.mocks.pictureFromZipLoader.LoadPictureFromZipBytesReturnValue = picture;
            environment.mocks.toolClient.LoadPictureForToolReturnValue.Picture = data.pictDto;
            var result = environment.dataAccess.LoadPictureForTool(CreateTool.Anonymous());
            Assert.AreSame(picture, result);
            Assert.AreEqual(data.pictDto.Id, result.SeqId);
            Assert.AreEqual(data.pictDto.FileName.Value, result.FileName);
            Assert.AreEqual(data.pictDto.FileType, result.FileType);
            Assert.AreEqual(data.pictDto.Nodeid, result.NodeId);
            Assert.AreEqual(data.pictDto.Nodeseqid, result.NodeSeqId);
            Assert.AreEqual(data.pict, ((MemoryStream)result.ImageStream).ToArray());
        }

        static IEnumerable<byte[]> LoadPictureForToolCallsZipLoaderData = new List<byte[]>()
        {
            new byte[] {1,2,56,34,67,76},
            new byte[] {13,42,56,33,22,16}
        };

        [TestCaseSource(nameof(LoadPictureForToolCallsZipLoaderData))]
        public void LoadPictureForLocationCallsZipLoader(byte[] pict)
        {
            var environment = new Environment();
            var picture = new DtoTypes.Picture()
            {
                Image = ByteString.CopyFrom(pict)
            };

            environment.mocks.toolClient.LoadPictureForToolReturnValue.Picture = picture;

            environment.dataAccess.LoadPictureForTool(CreateTool.Anonymous());

            Assert.AreEqual(pict, environment.mocks.pictureFromZipLoader.LoadPictureFromZipBytesParameter);
        }

        [TestCase(1)]
        [TestCase(7)]
        public void LoadToolsForModelCallsClient(long modelId)
        {
            var environment = new Environment();
            environment.dataAccess.LoadToolsForModel(CreateToolModel.WithId(modelId));

            Assert.AreEqual(modelId, environment.mocks.toolClient.LoadToolsForModelParameter.Value);
        }

        static IEnumerable<ListOfTools> LoadToolsForModelReturnsCorrectValueData = new List<ListOfTools>()
        {
            new ListOfTools()
            {
                Tools =
                {
                    DtoFactory.CreateToolDto(1, "serial 123", "inventory 123", true,
                        "abcX", "F11", "F12", "F13", "233a",
                        new DtoTypes.Status(){Id = 341, Description="CX", Alive = false},
                        new DtoTypes.HelperTableEntity(){ListId = 134,Value="C11",Alive = true,NodeId = 15},
                        new DtoTypes.HelperTableEntity(){ListId = 131, Value="K1213",Alive = true, NodeId = 1},
                        DtoFactory.CreateToolModelDtoRandomized(934567)),
                    DtoFactory.CreateToolDto(12, "serial ABC", "inventory XYZ", false,
                        "abc", "F1", "F2", "F3", "a",
                        new DtoTypes.Status(){Id = 1, Description="S", Alive = true},
                        new DtoTypes.HelperTableEntity(){ListId = 1,Value="C",Alive = true,NodeId = 5},
                        new DtoTypes.HelperTableEntity(){ListId = 11, Value="K",Alive = false, NodeId = 15},
                        DtoFactory.CreateToolModelDtoRandomized(4687268))
                }
            },
            new ListOfTools()
            {
                Tools =
                {
                    DtoFactory.CreateToolDto(199, "serial ABC", "inventory DEF", true,
                        "HI", "JK", "LM", "NO", "PQ",
                        new DtoTypes.Status(){Id = 666, Description="C$X", Alive = false},
                        new DtoTypes.HelperTableEntity(){ListId = 999, Value = "656", Alive = true, NodeId = 15},
                        new DtoTypes.HelperTableEntity(){ListId = 1312, Value="999",Alive = true, NodeId = 1},
                        DtoFactory.CreateToolModelDtoRandomized(6798524))
                }
            }
        };


        [TestCaseSource(nameof(LoadToolsForModelReturnsCorrectValueData))]
        public void LoadToolsForModelReturnsCorrectValue(ListOfTools datas)
        {
            var environment = new Environment();
            environment.mocks.toolClient.LoadToolsForModelReturnValue = datas;
            var result = environment.dataAccess.LoadToolsForModel(CreateToolModel.Anonymous());

            var comparer =
                new Func<DtoTypes.Tool, Core.Entities.Tool, bool>((dto, entity) =>
                    EqualityChecker.CompareToolWithToolDto(entity, dto));

            CheckerFunctions.CollectionAssertAreEquivalent(datas.Tools, result, comparer);
        }

        [Test]
        public void LoadModelsWithAtLeasOneToolCallsClient()
        {
            var environment = new Environment();
            environment.dataAccess.LoadModelsWithAtLeasOneTool();

            Assert.IsTrue(environment.mocks.toolClient.LoadModelsWithAtLeasOneToolCalled);
        }

        private static IEnumerable<ListOfToolModel> LoadModelsWithAtLeasOneToolReturnsCorrectValueData =
            new List<ListOfToolModel>()
            {
                new ListOfToolModel()
                {
                    ToolModels =
                    {
                        DtoFactory.CreateToolModelDtoRandomized(3789456),
                        DtoFactory.CreateToolModelDtoRandomized(198734),
                    }
                },
                new ListOfToolModel()
                {
                    ToolModels =
                    {
                        DtoFactory.CreateToolModelDtoRandomized(2689487)
                    }
                }
            };

        [TestCaseSource(nameof(LoadModelsWithAtLeasOneToolReturnsCorrectValueData))]
        public void LoadModelsWithAtLeasOneToolReturnsCorrectValue(ListOfToolModel datas)
        {
            var environment = new Environment();
            environment.mocks.toolClient.LoadModelsWithAtLeasOneToolReturnValue = datas;
            var result = environment.dataAccess.LoadModelsWithAtLeasOneTool();

            var comparer =
                new Func<DtoTypes.ToolModel, Core.Entities.ToolModel, bool>((dto, entity) =>
                    EqualityChecker.CompareToolModelWithToolModelDto(entity, dto));

            CheckerFunctions.CollectionAssertAreEquivalent(datas.ToolModels, result, comparer);
        }


        private static LocationToolAssignmentReferenceLink CreateLocationToolAssignmentReferenceLink(long id, long locationId, 
            string locationNumber, string locationName, long toolId, string serialNumber, string inventoryNumber)
        {
            return new LocationToolAssignmentReferenceLink()
            {
                Id = id,
                LocationId = locationId,
                LocationNumber = locationNumber,
                LocationName = locationName,
                ToolId = toolId,
                ToolSerialNumber = serialNumber,
                ToolInventoryNumber = inventoryNumber
            };
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    clientFactory = new ClientFactoryMock();
                    channelWrapper = new ChannelWrapperMock();
                    toolClient = new ToolClientMock();
                    pictureFromZipLoader = new PictureFromZipLoaderMock();
                    channelWrapper.GetToolClientReturnValue = toolClient;
                    clientFactory.AuthenticationChannel = channelWrapper;
                }
                public ClientFactoryMock clientFactory;
                public ChannelWrapperMock channelWrapper;
                public ToolClientMock toolClient;
                public PictureFromZipLoaderMock pictureFromZipLoader;
            }

            public Environment()
            {
                mocks = new Mocks();
                dataAccess = new ToolDataAccess(mocks.clientFactory, new MockLocationToolAssignmentDisplayFormatter(), mocks.pictureFromZipLoader);
            }

            public readonly Mocks mocks;
            public readonly ToolDataAccess dataAccess;
        }
    }
}
