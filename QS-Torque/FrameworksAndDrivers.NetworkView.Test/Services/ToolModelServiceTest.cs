using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Core.Entities;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using Server.TestHelper.Factories;
using Server.UseCases.UseCases;
using TestHelper.Checker;
using ToolModelService;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    class ToolModelServiceTest
    {
        private static List<List<ToolModel>> _gettingAllToolModelsTestData =
            new List<List<ToolModel>>
            {
                new List<ToolModel>
                {
                    CreateToolModel.Randomized(1)
                },
                new List<ToolModel>
                {
                    CreateToolModel.Randomized(2)
                },
                new List<ToolModel>
                {
                    CreateToolModel.Randomized(3),
                    CreateToolModel.Randomized(4),
                    CreateToolModel.Randomized(5),
                }
            };

        [TestCaseSource(nameof(_gettingAllToolModelsTestData))]
        public void GettingAllToolModelsReturnsDataFromUseCase(List<ToolModel> toolModels)
        {
            var useCase = new ToolModelUseCaseMock();
            var networkView = new NetworkView.Services.ToolModelService(useCase);
            useCase.GetAllToolModelsReturn = toolModels;

            var comparer =
                new Func<ToolModel, DtoTypes.ToolModel, bool>(
                    (toolModel, dtoToolModel) =>
                        EqualityChecker.CompareToolModelDtoWithToolModel(dtoToolModel, toolModel));

            CheckerFunctions.CollectionAssertAreEquivalent(
                toolModels,
                networkView.GetAllToolModels(new NoParams(), null).Result.ToolModels,
                comparer);
        }

        [TestCaseSource(nameof(_gettingAllToolModelsTestData))]
        public void GettingAllDeletedToolModelsReturnsDataFromUseCase(List<ToolModel> toolModels)
        {
            var useCase = new ToolModelUseCaseMock();
            var networkView = new NetworkView.Services.ToolModelService(useCase);
            useCase.GetAllDeletedToolModelsReturn= toolModels;

            var comparer =
                new Func<ToolModel, DtoTypes.ToolModel, bool>(
                    (toolModel, dtoToolModel) =>
                        EqualityChecker.CompareToolModelDtoWithToolModel(dtoToolModel, toolModel));

            CheckerFunctions.CollectionAssertAreEquivalent(
                toolModels,
                networkView.LoadDeletedToolModels(new NoParams(), null).Result.ToolModels,
                comparer);
        }

        private static List<ListOfToolModelDiff> _insertingToolModelsTestData = new List<ListOfToolModelDiff>
        {
            new ListOfToolModelDiff
            {
                ToolModelDiffs =
                {
                    new DtoTypes.ToolModelDiff
                    {
                        UserId = 345678,
                        Comment = "asdfjaklakveiaof",
                        OldToolModel = DtoFactory.CreateToolModelDtoRandomized(1),
                        NewToolModel = DtoFactory.CreateToolModelDtoRandomized(3)
                    }
                }
            },
            new ListOfToolModelDiff
            {
                ToolModelDiffs =
                {
                    new DtoTypes.ToolModelDiff
                    {
                        UserId = 784395,
                        Comment = "ogzhjgvokgh",
                        OldToolModel = DtoFactory.CreateToolModelDtoRandomized(2),
                        NewToolModel = DtoFactory.CreateToolModelDtoRandomized(4)
                    }
                }
            },
            new ListOfToolModelDiff
            {
                ToolModelDiffs =
                {
                    new DtoTypes.ToolModelDiff
                    {
                        UserId = 35498,
                        Comment = "jhnvdsafka",
                        OldToolModel = DtoFactory.CreateToolModelDtoRandomized(5),
                        NewToolModel = DtoFactory.CreateToolModelDtoRandomized(6)
                    },
                    new DtoTypes.ToolModelDiff
                    {
                        UserId = 70209,
                        Comment = "hjölkhgös",
                        OldToolModel = DtoFactory.CreateToolModelDtoRandomized(7),
                        NewToolModel = DtoFactory.CreateToolModelDtoRandomized(8)
                    },
                    new DtoTypes.ToolModelDiff
                    {
                        UserId = 0923845,
                        Comment = "hgwhougroe",
                        OldToolModel = DtoFactory.CreateToolModelDtoRandomized(9),
                        NewToolModel = DtoFactory.CreateToolModelDtoRandomized(10)
                    },
                    new DtoTypes.ToolModelDiff
                    {
                        UserId = 5666,
                        Comment = "nviioan",
                        OldToolModel = DtoFactory.CreateToolModelDtoRandomized(11),
                        NewToolModel = DtoFactory.CreateToolModelDtoRandomized(12)
                    },
                }
            },
        };

        [TestCaseSource(nameof(_insertingToolModelsTestData))]
        public void InsertingToolModelForwardsCorrectUserToUseCase(ListOfToolModelDiff toolModelDiffDtos)
        {
            var useCase = new ToolModelUseCaseMock();
            var networkView = new NetworkView.Services.ToolModelService(useCase);

            networkView.InsertToolModel(toolModelDiffDtos, null);

            CollectionAssert.AreEquivalent(
                toolModelDiffDtos.ToolModelDiffs.Select(diff => diff.UserId),
                useCase.LastInsertToolModelsParameterToolModelDiffs.Select(diff => diff.GetUser().UserId.ToLong()));
        }

        [TestCaseSource(nameof(_insertingToolModelsTestData))]
        public void InsertingToolModelForwardsCorrectCommentToUseCase(ListOfToolModelDiff toolModelDiffDtos)
        {
            var useCase = new ToolModelUseCaseMock();
            var networkView = new NetworkView.Services.ToolModelService(useCase);

            networkView.InsertToolModel(toolModelDiffDtos, null);

            CollectionAssert.AreEquivalent(
                toolModelDiffDtos.ToolModelDiffs.Select(diff => diff.Comment),
                useCase.LastInsertToolModelsParameterToolModelDiffs.Select(diff=>diff.GetComment().ToDefaultString()));
        }

        [TestCaseSource(nameof(_insertingToolModelsTestData))]
        public void InsertingToolModelForwardsCorrectOldToolModelToUseCase(ListOfToolModelDiff toolModelDiffDtos)
        {
            var useCase = new ToolModelUseCaseMock();
            var networkView = new NetworkView.Services.ToolModelService(useCase);

            networkView.InsertToolModel(toolModelDiffDtos, null);

            CheckerFunctions.CollectionAssertAreEquivalent(
                toolModelDiffDtos.ToolModelDiffs.Select(diff => diff.OldToolModel).ToList(),
                useCase.LastInsertToolModelsParameterToolModelDiffs.Select(diff => diff.GetOld()).ToList(),
                EqualityChecker.CompareToolModelDtoWithToolModel);
        }

        [TestCaseSource(nameof(_insertingToolModelsTestData))]
        public void InsertingToolModelForwardsCorrectNewToolModelToUseCase(ListOfToolModelDiff toolModelDiffDtos)
        {
            var useCase = new ToolModelUseCaseMock();
            var networkView = new NetworkView.Services.ToolModelService(useCase);

            networkView.InsertToolModel(toolModelDiffDtos, null);

            CheckerFunctions.CollectionAssertAreEquivalent(
                toolModelDiffDtos.ToolModelDiffs.Select(diff => diff.NewToolModel).ToList(),
                useCase.LastInsertToolModelsParameterToolModelDiffs.Select(diff => diff.GetNew()).ToList(),
                EqualityChecker.CompareToolModelDtoWithToolModel);
        }

        private static List<List<ToolModel>> _insertingToolModelsReturnData = new List<List<ToolModel>>
        {
            new List<ToolModel>
            {
                CreateToolModel.Randomized(6)
            },
            new List<ToolModel>
            {
                CreateToolModel.Randomized(7)
            },
            new List<ToolModel>
            {
                CreateToolModel.Randomized(8),
                CreateToolModel.Randomized(9),
                CreateToolModel.Randomized(10),
                CreateToolModel.Randomized(11),
            },
        };

        [TestCaseSource(nameof(_insertingToolModelsReturnData))]
        public void InsertingToolModelReturnsResultFromUseCase(List<ToolModel> toolModels)
        {
            var useCase = new ToolModelUseCaseMock();
            var networkView = new NetworkView.Services.ToolModelService(useCase);
            useCase.NextInsertToolModelsReturn = toolModels;

            var result = networkView.InsertToolModel(new ListOfToolModelDiff(), null);

            CheckerFunctions.CollectionAssertAreEquivalent(
                toolModels,
                result.Result.ToolModels,
                (expected, actual) => 
                    EqualityChecker.CompareToolModelDtoWithToolModel(actual, expected));
        }

        private static List<ListOfToolModelDiff> _updatingToolModelsTestData = new List<ListOfToolModelDiff>
        {
            new ListOfToolModelDiff
            {
                ToolModelDiffs =
                {
                    new DtoTypes.ToolModelDiff
                    {
                        UserId = 54567,
                        Comment = "hfdsljaka",
                        OldToolModel = DtoFactory.CreateToolModelDtoRandomized(13),
                        NewToolModel = DtoFactory.CreateToolModelDtoRandomized(14)
                    }
                }
            },
            new ListOfToolModelDiff
            {
                ToolModelDiffs =
                {
                    new DtoTypes.ToolModelDiff
                    {
                        UserId = 7826,
                        Comment = "znvshl",
                        OldToolModel = DtoFactory.CreateToolModelDtoRandomized(15),
                        NewToolModel = DtoFactory.CreateToolModelDtoRandomized(16)
                    }
                }
            },
            new ListOfToolModelDiff
            {
                ToolModelDiffs =
                {
                    new DtoTypes.ToolModelDiff
                    {
                        UserId = 547689,
                        Comment = "hfdsljaka",
                        OldToolModel = DtoFactory.CreateToolModelDtoRandomized(17),
                        NewToolModel = DtoFactory.CreateToolModelDtoRandomized(18)
                    },
                    new DtoTypes.ToolModelDiff
                    {
                        UserId = 72456573,
                        Comment = "gfdm",
                        OldToolModel = DtoFactory.CreateToolModelDtoRandomized(19),
                        NewToolModel = DtoFactory.CreateToolModelDtoRandomized(20)
                    },
                    new DtoTypes.ToolModelDiff
                    {
                        UserId = 27354599,
                        Comment = "kuömkfsa",
                        OldToolModel = DtoFactory.CreateToolModelDtoRandomized(21),
                        NewToolModel = DtoFactory.CreateToolModelDtoRandomized(22)
                    },
                    new DtoTypes.ToolModelDiff
                    {
                        UserId = 43264798,
                        Comment = "zuiui,kjh",
                        OldToolModel = DtoFactory.CreateToolModelDtoRandomized(23),
                        NewToolModel = DtoFactory.CreateToolModelDtoRandomized(24)
                    }
                }
            }
        };

        [TestCaseSource(nameof(_updatingToolModelsTestData))]
        public void UpdatingToolModelPassesUserToUseCase(ListOfToolModelDiff toolModelDiffDtos)
        {
            var useCase = new ToolModelUseCaseMock();
            var networkView = new NetworkView.Services.ToolModelService(useCase);

            networkView.UpdateToolModel(toolModelDiffDtos, null);

            CollectionAssert.AreEquivalent(
                toolModelDiffDtos.ToolModelDiffs.Select(diff => diff.UserId),
                useCase.LastUpdateToolModelsParameterToolModelDiffs.Select(diff => diff.GetUser().UserId.ToLong()));
        }

        [TestCaseSource(nameof(_updatingToolModelsTestData))]
        public void UpdatingToolModelPassesCommentToUseCase(ListOfToolModelDiff toolModelDiffDtos)
        {
            var useCase = new ToolModelUseCaseMock();
            var networkView = new NetworkView.Services.ToolModelService(useCase);

            networkView.UpdateToolModel(toolModelDiffDtos, null);

            CollectionAssert.AreEquivalent(
                toolModelDiffDtos.ToolModelDiffs.Select(diff => diff.Comment),
                useCase.LastUpdateToolModelsParameterToolModelDiffs.Select(diff => diff.GetComment().ToDefaultString()));
        }

        [TestCaseSource(nameof(_updatingToolModelsTestData))]
        public void UpdatingToolModelPassesOldToolModelToUseCase(ListOfToolModelDiff toolModelDiffDtos)
        {
            var useCase = new ToolModelUseCaseMock();
            var networkView = new NetworkView.Services.ToolModelService(useCase);

            networkView.UpdateToolModel(toolModelDiffDtos, null);

            CheckerFunctions.CollectionAssertAreEquivalent(
                toolModelDiffDtos.ToolModelDiffs.Select(diff => diff.OldToolModel).ToList(),
                useCase.LastUpdateToolModelsParameterToolModelDiffs.Select(diff => diff.GetOld()).ToList(),
                EqualityChecker.CompareToolModelDtoWithToolModel);
        }

        [TestCaseSource(nameof(_updatingToolModelsTestData))]
        public void UpdatingToolModelPassesNewToolModelToUseCase(ListOfToolModelDiff toolModelDiffDtos)
        {
            var useCase = new ToolModelUseCaseMock();
            var networkView = new NetworkView.Services.ToolModelService(useCase);

            networkView.UpdateToolModel(toolModelDiffDtos, null);

            CheckerFunctions.CollectionAssertAreEquivalent(
                toolModelDiffDtos.ToolModelDiffs.Select(diff => diff.NewToolModel).ToList(),
                useCase.LastUpdateToolModelsParameterToolModelDiffs.Select(diff => diff.GetNew()).ToList(),
                EqualityChecker.CompareToolModelDtoWithToolModel);
        }

        private static List<List<ToolModel>> _updatingToolModelsReturnData = new List<List<ToolModel>>
        {
            new List<ToolModel>
            {
                CreateToolModel.Randomized(6)
            },
            new List<ToolModel>
            {
                CreateToolModel.Randomized(7)
            },
            new List<ToolModel>
            {
                CreateToolModel.Randomized(8),
                CreateToolModel.Randomized(9),
                CreateToolModel.Randomized(10),
                CreateToolModel.Randomized(11),
            },
        };

        [TestCaseSource(nameof(_updatingToolModelsReturnData))]
        public void UpdatingToolModelReturnsResultFromUseCase(List<ToolModel> toolModels)
        {
            var useCase = new ToolModelUseCaseMock();
            var networkView = new NetworkView.Services.ToolModelService(useCase);
            useCase.NextUpdateToolModelsReturn = toolModels;

            var result = networkView.UpdateToolModel(new ListOfToolModelDiff(), null);

            CheckerFunctions.CollectionAssertAreEquivalent(
                toolModels,
                result.Result.ToolModels,
                (expected, actual) =>
                    EqualityChecker.CompareToolModelDtoWithToolModel(actual, expected));
        }

        [TestCase(2657)]
        [TestCase(46758)]
        [TestCase(458068)]
        [TestCase(87)]
        public void GetingReferencedToolsPassesToolModelIdToUseCase(long toolModelId)
        {
            var useCase = new ToolModelUseCaseMock();
            var networkView = new NetworkView.Services.ToolModelService(useCase);

            networkView.GetReferencedToolLinks(new BasicTypes.Long{Value=toolModelId}, null);

            Assert.AreEqual(toolModelId, useCase.LastGetReerencedToolLinksParameterToolModelId.ToLong());
        }

        private static List<List<ToolReferenceLink>> _gettingReferencedToolsReturnData = new List<List<ToolReferenceLink>>
        {
            new List<ToolReferenceLink>
            {
                CreateToolReferenceLinkRandomized(647382)
            },
            new List<ToolReferenceLink>
            {
                CreateToolReferenceLinkRandomized(196512)
            },
            new List<ToolReferenceLink>
            {
                CreateToolReferenceLinkRandomized(75834975),
                CreateToolReferenceLinkRandomized(988268+9),
                CreateToolReferenceLinkRandomized(42284),
            },
        };

        [TestCaseSource(nameof(_gettingReferencedToolsReturnData))]
        public void GettingReferencedToolsReturnsReferences(List<ToolReferenceLink> referenceLinks)
        {
            var useCase = new ToolModelUseCaseMock();
            var networkView = new NetworkView.Services.ToolModelService(useCase);
            useCase.NextGetReferencedToolLinksReturn = referenceLinks;

            var result = 
                networkView.GetReferencedToolLinks(new BasicTypes.Long {Value = 1}, null);

            CollectionAssert.AreEquivalent(
                referenceLinks.Select(
                    reference => (reference.Id.ToLong(), reference.InventoryNumber, reference.SerialNumber)),
                result.Result.ToolReferenceLinks.Select(
                    resultReference => (resultReference.Id, resultReference.InventoryNumber, resultReference.SerialNumber)));
        }

        private class ToolModelUseCaseMock : IToolModelUseCase
        {
            public List<ToolModel> GetAllToolModels()
            {
                return GetAllToolModelsReturn;
            }

            public List<ToolModel> InsertToolModels(List<ToolModelDiff> toolModelDiffs)
            {
                LastInsertToolModelsParameterToolModelDiffs = toolModelDiffs;
                return NextInsertToolModelsReturn;
            }

            public List<ToolModel> UpdateToolModels(List<ToolModelDiff> toolModelDiffs)
            {
                LastUpdateToolModelsParameterToolModelDiffs = toolModelDiffs;
                return NextUpdateToolModelsReturn;
            }

            public List<ToolReferenceLink> GetReferencedToolLinks(ToolModelId toolModelId)
            {
                LastGetReerencedToolLinksParameterToolModelId = toolModelId;
                return NextGetReferencedToolLinksReturn;
            }

            public List<ToolModel> LoadDeletedToolModels()
            {
                return GetAllDeletedToolModelsReturn;
            }

            public List<ToolModel> GetAllToolModelsReturn;
            public List<ToolModel> GetAllDeletedToolModelsReturn;
            public List<ToolModelDiff> LastInsertToolModelsParameterToolModelDiffs;
            public List<ToolModel> NextInsertToolModelsReturn = new List<ToolModel>();
            public List<ToolModelDiff> LastUpdateToolModelsParameterToolModelDiffs;
            public List<ToolModel> NextUpdateToolModelsReturn = new List<ToolModel>();
            public ToolModelId LastGetReerencedToolLinksParameterToolModelId;
            public List<ToolReferenceLink> NextGetReferencedToolLinksReturn =new List<ToolReferenceLink>();
        }

        private static ToolReferenceLink CreateToolReferenceLinkParametrized(long id, string inventoryNumber, string serialNumber)
        {
            return new ToolReferenceLink(new QstIdentifier(id), inventoryNumber, serialNumber);
        }

        private static ToolReferenceLink CreateToolReferenceLinkRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return CreateToolReferenceLinkParametrized(
                randomizer.Next(),
                CreateString.Randomized(randomizer.Next(0, 50), randomizer.Next()),
                CreateString.Randomized(randomizer.Next(0, 50), randomizer.Next()));
        }
    }
}
