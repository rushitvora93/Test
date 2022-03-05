using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Client.TestHelper.Factories;
using Client.TestHelper.Mock;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;
using ToolModelService;
using IToolModelClient = FrameworksAndDrivers.RemoteData.GRPC.DataAccess.IToolModelClient;
using ToolModel = Core.Entities.ToolModel;
using User = Core.Entities.User;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    class ToolModelDataAccessTest
    {
        private static List<ListOfToolModel> _loadToolModelsData = new List<ListOfToolModel>
        {
            new ListOfToolModel
            {
                ToolModels =
                {
                    DtoFactory.CreateToolModelDtoRandomized(78964165)
                }
            },
            new ListOfToolModel
            {
                ToolModels =
                {
                    DtoFactory.CreateToolModelDtoRandomized(687756276)
                }
            },
            new ListOfToolModel
            {
                ToolModels =
                {
                    DtoFactory.CreateToolModelDtoRandomized(707062),
                    DtoFactory.CreateToolModelDtoRandomized(30684720),
                    DtoFactory.CreateToolModelDtoRandomized(7676),
                    DtoFactory.CreateToolModelDtoRandomized(578737997),
                }
            }
        };

        [TestCaseSource(nameof(_loadToolModelsData))]
        public void LoadingToolModelsGetsToolModelsFromClient(ListOfToolModel toolModelDtos)
        {
            var environment = new Environment();
            environment.Mocks.toolModelClient.NextGetAllToolModelsReturn = toolModelDtos;

            var result = environment.Data.LoadToolModels();

            CheckerFunctions.CollectionAssertAreEquivalent(
                toolModelDtos.ToolModels,
                result,
                (expected, result) =>
                    EqualityChecker.CompareToolModelWithToolModelDto(result, expected));
        }

        [TestCaseSource(nameof(_loadToolModelsData))]
        public void LoadingDeletedToolModelsGetsToolModelsFromClient(ListOfToolModel toolModelDtos)
        {
            var environment = new Environment();
            environment.Mocks.toolModelClient.AllDeletedToolModels = toolModelDtos;

            var result = environment.Data.LoadDeletedToolModels();

            CheckerFunctions.CollectionAssertAreEquivalent(
                toolModelDtos.ToolModels,
                result,
                (expected, result) =>
                    EqualityChecker.CompareToolModelWithToolModelDto(result, expected));
        }

        public class RemoveToolModelData
        {
            public User User;
            public List<ToolModel> ToolModels;
        }

        private static List<RemoveToolModelData> _removeToolModelsData = new List<RemoveToolModelData>
        {
            new RemoveToolModelData
            {
                User = CreateUser.IdOnly(6),
                ToolModels =  new List<ToolModel>
                {
                    CreateToolModel.Randomized(78924)
                }
            },
            new RemoveToolModelData
            {
                User = CreateUser.IdOnly(75398),
                ToolModels =  new List<ToolModel>
                {
                    CreateToolModel.Randomized(63759)
                }
            },
            new RemoveToolModelData
            {
                User = CreateUser.IdOnly(43547),
                ToolModels =  new List<ToolModel>
                {
                    CreateToolModel.Randomized(879234),
                    CreateToolModel.Randomized(1850),
                    CreateToolModel.Randomized(5107593),
                }
            }
        };

        [TestCaseSource(nameof(_removeToolModelsData))]
        public void RemovingToolModelsForwardsUserToClient(RemoveToolModelData removeToolModelData)
        {
            var environment = new Environment();

            environment.Data.RemoveToolModels(removeToolModelData.ToolModels, removeToolModelData.User);

            foreach (var diff in environment.Mocks.toolModelClient.LastUpdateToolModelsParameterToolModelDiffs.ToolModelDiffs)
            {
                Assert.AreEqual(removeToolModelData.User.UserId.ToLong(), diff.UserId);
            }
        }

        [TestCaseSource(nameof(_removeToolModelsData))]
        public void RemovingToolModelsFillsOldDiffFields(RemoveToolModelData removeToolModelData)
        {
            var environment = new Environment();

            environment.Data.RemoveToolModels(removeToolModelData.ToolModels, removeToolModelData.User);

            CheckerFunctions.CollectionAssertAreEquivalent(
                removeToolModelData.ToolModels,
                environment.Mocks.toolModelClient.LastUpdateToolModelsParameterToolModelDiffs.ToolModelDiffs.Select(diff =>  diff.OldToolModel).ToList(),
                EqualityChecker.CompareToolModelWithToolModelDto);
        }

        [TestCaseSource(nameof(_removeToolModelsData))]
        public void RemovingToolModelsFillsNewDiffFields(RemoveToolModelData removeToolModelData)
        {
            var environment = new Environment();

            environment.Data.RemoveToolModels(removeToolModelData.ToolModels, removeToolModelData.User);
            
            CheckerFunctions.CollectionAssertAreEquivalent(
                removeToolModelData.ToolModels,
                environment.Mocks.toolModelClient.LastUpdateToolModelsParameterToolModelDiffs.ToolModelDiffs.Select(diff => diff.NewToolModel).ToList(),
                EqualityChecker.CompareToolModelWithToolModelDto);
        }

        [TestCaseSource(nameof(_removeToolModelsData))]
        public void RemovingToolModelsSetsAliveStatsCorrectly(RemoveToolModelData removeToolModelData)
        {
            var environment = new Environment();

            environment.Data.RemoveToolModels(removeToolModelData.ToolModels, removeToolModelData.User);

            foreach (var diff in environment.Mocks.toolModelClient.LastUpdateToolModelsParameterToolModelDiffs.ToolModelDiffs)
            {
                Assert.AreEqual(true, diff.OldToolModel.Alive);
                Assert.AreEqual(false, diff.NewToolModel.Alive);
            }
        }

        private static List<Core.UseCases.ToolModelDiff> _updateToolModelsData = new List<Core.UseCases.ToolModelDiff>
        {
            new Core.UseCases.ToolModelDiff
            {
                User = CreateUser.IdOnly(8),
                Comment = CreateHistoryComment.Randomized(2634782),
                OldToolModel = CreateToolModel.Randomized(7493),
                NewToolModel = CreateToolModel.Randomized(25472)
            },
            new Core.UseCases.ToolModelDiff
            {
                User = CreateUser.IdOnly(348795),
                Comment = CreateHistoryComment.Randomized(324657),
                OldToolModel = CreateToolModel.Randomized(93659),
                NewToolModel = CreateToolModel.Randomized(643271)
            },
        };

        [TestCaseSource(nameof(_updateToolModelsData))]
        public void UpdatingToolModelsPassesUserToClient(Core.UseCases.ToolModelDiff toolModelDiff)
        {
            var environment = new Environment();

            environment.Data.UpdateToolModel(toolModelDiff);

            Assert.AreEqual(
                toolModelDiff.User.UserId.ToLong(),
                environment.Mocks.toolModelClient.LastUpdateToolModelsParameterToolModelDiffs.ToolModelDiffs[0].UserId);
        }

        [TestCaseSource(nameof(_updateToolModelsData))]
        public void UpdatingToolModelsPassesCommentToClient(Core.UseCases.ToolModelDiff toolModelDiff)
        {
            var environment = new Environment();

            environment.Data.UpdateToolModel(toolModelDiff);

            Assert.AreEqual(
                toolModelDiff.Comment.ToDefaultString(),
                environment.Mocks.toolModelClient.LastUpdateToolModelsParameterToolModelDiffs.ToolModelDiffs[0].Comment);
        }

        [TestCaseSource(nameof(_updateToolModelsData))]
        public void UpdatingToolModelsPassesOldToolModelToClient(Core.UseCases.ToolModelDiff toolModelDiff)
        {
            var environment = new Environment();

            environment.Data.UpdateToolModel(toolModelDiff);

            Assert.IsTrue(
                EqualityChecker.CompareToolModelWithToolModelDto(
                    toolModelDiff.OldToolModel,
                    environment.Mocks.toolModelClient.LastUpdateToolModelsParameterToolModelDiffs.ToolModelDiffs[0].OldToolModel));
        }

        [TestCaseSource(nameof(_updateToolModelsData))]
        public void UpdatingToolModelsPassesNewToolModelToClient(Core.UseCases.ToolModelDiff toolModelDiff)
        {
            var environment = new Environment();

            environment.Data.UpdateToolModel(toolModelDiff);

            Assert.IsTrue(
                EqualityChecker.CompareToolModelWithToolModelDto(
                    toolModelDiff.NewToolModel,
                    environment.Mocks.toolModelClient.LastUpdateToolModelsParameterToolModelDiffs.ToolModelDiffs[0].NewToolModel));
        }

        [TestCaseSource(nameof(_updateToolModelsData))]
        public void UpdatingToolModelsSetsAliveTrueInOldAndNew(Core.UseCases.ToolModelDiff toolModelDiff)
        {
            var environment = new Environment();

            environment.Data.UpdateToolModel(toolModelDiff);

            Assert.IsTrue(environment.Mocks.toolModelClient.LastUpdateToolModelsParameterToolModelDiffs.ToolModelDiffs[0].OldToolModel.Alive);
            Assert.IsTrue(environment.Mocks.toolModelClient.LastUpdateToolModelsParameterToolModelDiffs.ToolModelDiffs[0].NewToolModel.Alive);
        }

        [TestCaseSource(nameof(_updateToolModelsData))]
        public void UpdatingToolModelsReturnsNewFromDiff(Core.UseCases.ToolModelDiff toolModelDiff)
        {
            var environment = new Environment();
            var result = environment.Data.UpdateToolModel(toolModelDiff);
            Assert.AreSame(toolModelDiff.NewToolModel, result);
        }

        public class AddingToolModelData
        {
            public User User;
            public ToolModel ToolModel;
        }

        private static List<AddingToolModelData> _addingToolModelData = new List<AddingToolModelData>
        {
            new AddingToolModelData
            {
                User = CreateUser.IdOnly(3),
                ToolModel = CreateToolModel.Randomized(46327864)
            },
            new AddingToolModelData
            {
                User = CreateUser.IdOnly(7843),
                ToolModel = CreateToolModel.Randomized(7943827)
            }
        };

        [TestCaseSource(nameof(_addingToolModelData))]
        public void AddingToolModelPassesUserToClient(AddingToolModelData toolModelData)
        {
            var environment = new Environment();

            environment.Data.AddToolModel(toolModelData.ToolModel, toolModelData.User);

            Assert.AreEqual(
                toolModelData.User.UserId.ToLong(), 
                environment.Mocks.toolModelClient.LastAddToolModelParameterToolModelDiffs.ToolModelDiffs[0].UserId);
        }

        [TestCaseSource(nameof(_addingToolModelData))]
        public void AddingToolModelPassesNewToolModelToClient(AddingToolModelData toolModelData)
        {
            var environment = new Environment();

            environment.Data.AddToolModel(toolModelData.ToolModel, toolModelData.User);

            Assert.IsTrue(
                EqualityChecker.CompareToolModelWithToolModelDto(
                    toolModelData.ToolModel,
                    environment.Mocks.toolModelClient.LastAddToolModelParameterToolModelDiffs.ToolModelDiffs[0].NewToolModel));
        }

        private static List<ListOfToolModel> _addingResultData = new List<ListOfToolModel>
        {
            new ListOfToolModel
            {
                ToolModels =
                {
                    DtoFactory.CreateToolModelDtoRandomized(358),
                }
            },
            new ListOfToolModel
            {
                ToolModels =
                {
                    DtoFactory.CreateToolModelDtoRandomized(789453270),
                    DtoFactory.CreateToolModelDtoRandomized(879432698)
                }
            }
        };

        [TestCaseSource(nameof(_addingResultData))]
        public void AddingToolModelReturnsNewToolModel(ListOfToolModel resultFromClient)
        {
            var environment = new Environment();
            environment.Mocks.toolModelClient.NextAddToolModelReturn = resultFromClient;

            var result = environment.Data.AddToolModel(CreateToolModel.Anonymous(), CreateUser.Anonymous());

            Assert.IsTrue(EqualityChecker.CompareToolModelWithToolModelDto(result, resultFromClient.ToolModels[0]));
        }

        [TestCase(8)]
        [TestCase(37489)]
        public void GettingToolLinkReferencesPassesToolModelIdToClient(long toolModelId)
        {
            var environment = new Environment();

            environment.Data.LoadReferencedTools(toolModelId);

            Assert.AreEqual(toolModelId, environment.Mocks.toolModelClient.LastGetReferencedToolLinksParameterToolModelId.Value);
        }

        private static List<ListOfToolReferenceLink> _gettingToolLinkReferencesReturn =
            new List<ListOfToolReferenceLink>
            {
                new ListOfToolReferenceLink
                {
                    ToolReferenceLinks =
                    {
                        DtoFactory.CreateToolReferenceLinkDtoRandomized(345)
                    }
                },
                new ListOfToolReferenceLink
                {
                    ToolReferenceLinks =
                    {
                        DtoFactory.CreateToolReferenceLinkDtoRandomized(93745)
                    }
                },
                new ListOfToolReferenceLink
                {
                    ToolReferenceLinks =
                    {
                        DtoFactory.CreateToolReferenceLinkDtoRandomized(65374),
                        DtoFactory.CreateToolReferenceLinkDtoRandomized(8760),
                        DtoFactory.CreateToolReferenceLinkDtoRandomized(257464),
                    }
                }
            };

        [TestCaseSource(nameof(_gettingToolLinkReferencesReturn))]
        public void GettingToolLinkReferencesReturnsReferencedToolLinks(ListOfToolReferenceLink toolReferenceLinkReturn)
        {
            var environment = new Environment();
            environment.Mocks.toolModelClient.NextGetReferencedToolLinksReturn = toolReferenceLinkReturn;

            var result = environment.Data.LoadReferencedTools(0);

            CollectionAssert.AreEquivalent(
                toolReferenceLinkReturn.ToolReferenceLinks.Select(
                    toolRefLink => (toolRefLink.Id, toolRefLink.InventoryNumber, toolRefLink.SerialNumber)),
                result.Select(
                    toolRefLink => (toolRefLink.Id.ToLong(), toolRefLink.InventoryNumber, toolRefLink.SerialNumber)));
        }

        private class ToolModelClientMock : IToolModelClient
        {
            public ListOfToolModel GetAllToolModels()
            {
                return NextGetAllToolModelsReturn;
            }

            public ListOfToolModel UpdateToolModels(ListOfToolModelDiff toolModelDiffs)
            {
                LastUpdateToolModelsParameterToolModelDiffs = toolModelDiffs;
                return NextUpdateToolModelsReturn;
            }

            public ListOfToolModel AddToolModel(ListOfToolModelDiff toolModelDiffs)
            {
                LastAddToolModelParameterToolModelDiffs = toolModelDiffs;
                return NextAddToolModelReturn;
            }

            public ListOfToolReferenceLink GetReferencedToolLinks(Long toolModelId)
            {
                LastGetReferencedToolLinksParameterToolModelId = toolModelId;
                return NextGetReferencedToolLinksReturn;
            }

            public ListOfToolModel LoadDeletedToolModels()
            {
                return AllDeletedToolModels;
            }

            public DtoTypes.ListOfToolModel NextGetAllToolModelsReturn;
            public ListOfToolModelDiff LastUpdateToolModelsParameterToolModelDiffs;
            public ListOfToolModel NextUpdateToolModelsReturn = new ListOfToolModel();
            public ListOfToolModelDiff LastAddToolModelParameterToolModelDiffs;
            public ListOfToolModel NextAddToolModelReturn = new ListOfToolModel();
            public Long LastGetReferencedToolLinksParameterToolModelId;
            public ListOfToolReferenceLink NextGetReferencedToolLinksReturn = new ListOfToolReferenceLink();
            public DtoTypes.ListOfToolModel AllDeletedToolModels;
        }

        private class Environment
        {
            public class Mock
            {
                public Mock()
                {
                    clientFactory = new ClientFactoryMock();
                    channelWrapper = new ChannelWrapperMock();
                    toolModelClient = new ToolModelClientMock();
                    channelWrapper.GetToolModelClientReturnValue = toolModelClient;
                    clientFactory.AuthenticationChannel = channelWrapper;
                }

                public ToolModelClientMock toolModelClient;
                public ClientFactoryMock clientFactory;
                public ChannelWrapperMock channelWrapper;
            }

            public Environment()
            {
                Mocks = new Mock();
                Data = new ToolModelDataAccess(Mocks.clientFactory, null);
            }

            public ToolModelDataAccess Data;
            public Mock Mocks;
        }
    }
}
