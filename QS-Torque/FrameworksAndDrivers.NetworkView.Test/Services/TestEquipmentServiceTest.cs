using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Common.Types.Enums;
using DtoTypes;
using NUnit.Framework;
using Server.Core.Entities;
using Server.TestHelper.Factories;
using Server.UseCases.UseCases;
using TestEquipmentService;
using TestHelper.Checker;
using TestEquipment = Server.Core.Entities.TestEquipment;
using TestEquipmentDiff = Server.Core.Diffs.TestEquipmentDiff;
using TestEquipmentModel = Server.Core.Entities.TestEquipmentModel;
using TestEquipmentModelDiff = Server.Core.Diffs.TestEquipmentModelDiff;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    public class TestEquipmentUseCaseMock : ITestEquipmentUseCase
    {
        public List<TestEquipment> GetTestEquipmentsByIdsReturnValue { get; set; } = new List<TestEquipment>();
        public List<TestEquipmentId> GetTestEquipmentsByIdsParameter { get; set; }
        public bool UpdateTestEquipmentsWithHistoryParameterWithModelUpdate { get; set; }
        public List<TestEquipmentDiff> UpdateTestEquipmentsWithHistoryParameterDiffs { get; set; }
        public List<TestEquipmentModel> LoadTestEquipmentModelsReturnValue { get; set; } = new List<TestEquipmentModel>();
        public bool LoadTestEquipmentModelsCalled { get; set; }
        public List<TestEquipmentModelDiff> UpdateTestEquipmentModelsWithHistoryParameterDiff { get; set; }
        public List<TestEquipment> InsertTestEquipmentsWithHistoryReturnValue { get; set; } = new List<TestEquipment>();
        public bool InsertTestEquipmentsWithHistoryParameterReturnList { get; set; }
        public List<TestEquipmentDiff> InsertTestEquipmentsWithHistoryParameterDiff { get; set; }
        public bool IsInventoryNumberUniqueReturnValue { get; set; }
        public TestEquipmentInventoryNumber IsInventoryNumberUniqueParameter { get; set; }
        public bool IsSerialNumberUniqueReturnValue { get; set; }
        public TestEquipmentSerialNumber IsSerialNumberUniqueParameter { get; set; }
        public bool IsTestEquipmentModelNameUniqueReturnValue { get; set; }
        public TestEquipmentModelName IsTestEquipmentModelNameUniqueParameter { get; set; }
        public List<TestEquipmentType> LoadAvailableTestEquipmentTypesReturnValue { get; set; } = new List<TestEquipmentType>();
        public bool LoadAvailableTestEquipmentTypesCalled { get; set; }

        public List<TestEquipment> GetTestEquipmentsByIds(List<TestEquipmentId> ids)
        {
            GetTestEquipmentsByIdsParameter = ids;
            return GetTestEquipmentsByIdsReturnValue;
        }

        public void UpdateTestEquipmentsWithHistory(List<TestEquipmentDiff> diffs, bool withModelUpdate)
        {
            UpdateTestEquipmentsWithHistoryParameterDiffs = diffs;
            UpdateTestEquipmentsWithHistoryParameterWithModelUpdate = withModelUpdate;
        }

        public List<TestEquipmentModel> LoadTestEquipmentModels()
        {
            LoadTestEquipmentModelsCalled = true;
            return LoadTestEquipmentModelsReturnValue;
        }

        public void UpdateTestEquipmentModelsWithHistory(List<TestEquipmentModelDiff> diff)
        {
            UpdateTestEquipmentModelsWithHistoryParameterDiff = diff;
        }

        public List<TestEquipment> InsertTestEquipmentsWithHistory(List<TestEquipmentDiff> diffs, bool returnList)
        {
            InsertTestEquipmentsWithHistoryParameterDiff = diffs;
            InsertTestEquipmentsWithHistoryParameterReturnList = returnList;
            return InsertTestEquipmentsWithHistoryReturnValue;
        }

        public bool IsInventoryNumberUnique(TestEquipmentInventoryNumber inventoryNumber)
        {
            IsInventoryNumberUniqueParameter = inventoryNumber;
            return IsInventoryNumberUniqueReturnValue;
        }

        public bool IsSerialNumberUnique(TestEquipmentSerialNumber serialNumber)
        {
            IsSerialNumberUniqueParameter = serialNumber;
            return IsSerialNumberUniqueReturnValue;
        }

        public bool IsTestEquipmentModelNameUnique(TestEquipmentModelName name)
        {
            IsTestEquipmentModelNameUniqueParameter = name;
            return IsTestEquipmentModelNameUniqueReturnValue;
        }

        public List<TestEquipmentType> LoadAvailableTestEquipmentTypes()
        {
            LoadAvailableTestEquipmentTypesCalled = true;
            return LoadAvailableTestEquipmentTypesReturnValue;
        }
    }

    public class TestEquipmentServiceTest
    {
        private static IEnumerable<(List<TestEquipmentModel>, List<TestEquipment>)> TestEquipmentModelData =
            new List<(List<TestEquipmentModel>, List<TestEquipment>)>()
            {
                (
                    new List<TestEquipmentModel>()
                    {
                        CreateTestEquipmentModel.Randomized(9878),
                        CreateTestEquipmentModel.Randomized(1324),
                    },
                    new List<TestEquipment>()
                    {
                        CreateTestEquipment.Randomized(7777),
                    }
                 ),
                (
                    new List<TestEquipmentModel>()
                    {
                        CreateTestEquipmentModel.Randomized(111),
                    },
                    new List<TestEquipment>()
                    {
                        CreateTestEquipment.Randomized(333),
                        CreateTestEquipment.Randomized(2345),
                    }
                )
            };

        [TestCaseSource(nameof(TestEquipmentModelData))]
        public void LoadTestEquipmentModelsReturnCorrectValue((List<TestEquipmentModel> models, List<TestEquipment> testEquipments) data)
        {
            foreach (var model in data.models)
            {
                model.TestEquipments = data.testEquipments;
            }

            var useCase = new TestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TestEquipmentService(null, useCase);
            useCase.LoadTestEquipmentModelsReturnValue = data.models;
            var result = service.LoadTestEquipmentModels(new NoParams(),  null).Result;

            CheckerFunctions.CollectionAssertAreEquivalent(data.models, result.TestEquipmentModels, EqualityChecker.CompareTestEquipmentModelWithTestEquipmentModel);

            var counter = 0;
            foreach (var entity in data.models)
            {
                CheckerFunctions.CollectionAssertAreEquivalent(entity.TestEquipments.ToList(),
                    result.TestEquipmentModels[counter].TestEquipments.TestEquipments,
                    EqualityChecker.CompareTestEquipmentWithTestEquipmentDto);
                counter++;
            }
        }

        [Test]
        public void LoadTestEquipmentModelsCallsUseCase()
        {
            var useCase = new TestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TestEquipmentService(null, useCase);

            service.LoadTestEquipmentModels(new NoParams(), null);

            Assert.IsTrue(useCase.LoadTestEquipmentModelsCalled);
        }

        private static IEnumerable<ListOfLongs> GetTestEquipmentsByIdsCallsUseCaseData =
            new List<ListOfLongs>()
            {
                new ListOfLongs()
                {
                    Values =
                    {
                        new Long() {Value = 1},
                        new Long() {Value = 1113},
                        new Long() {Value = 14},
                        new Long() {Value = 13},
                    }
                },
                new ListOfLongs()
                {
                    Values =
                    {
                        new Long() {Value = 41},
                        new Long() {Value = 24},
                    }
                }
            };

        [TestCaseSource(nameof(GetTestEquipmentsByIdsCallsUseCaseData))]
        public void GetTestEquipmentsByIdsCallsUseCase(ListOfLongs ids)
        {
            var useCase = new TestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TestEquipmentService(null, useCase);

            service.GetTestEquipmentsByIds(ids, null);

            Assert.AreEqual(ids.Values.Select(x => x.Value).ToList(), useCase.GetTestEquipmentsByIdsParameter.Select(x => x.ToLong()));
        }

        private static IEnumerable<List<TestEquipment>> TestEquipmentData =
            new List<List<TestEquipment>>()
            {
                new List<TestEquipment>()
                {
                    CreateTestEquipment.Randomized(9878),
                    CreateTestEquipment.Randomized(1324),
                },
                new List<TestEquipment>()
                {
                    CreateTestEquipment.Randomized(7777),
                }
            };

        [TestCaseSource(nameof(TestEquipmentData))]
        public void GetTestEquipmentsByIdsReturnCorrectValue(List<TestEquipment> entityList)
        {
            var useCase = new TestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TestEquipmentService(null, useCase);
            useCase.GetTestEquipmentsByIdsReturnValue = entityList;
            var result = service.GetTestEquipmentsByIds(new ListOfLongs(), null).Result;
            CheckerFunctions.CollectionAssertAreEquivalent(entityList, result.TestEquipments, EqualityChecker.CompareTestEquipmentWithTestEquipmentDto);
        }

        static IEnumerable<(ListOfTestEquipmentDiffs, bool)> InsertUpdateTestEquipmentWithHistoryData = new List<(ListOfTestEquipmentDiffs, bool)>
        {
            (
                new ListOfTestEquipmentDiffs()
                {
                    Diffs =
                    {
                        new DtoTypes.TestEquipmentDiff()
                        {
                            UserId = 1,
                            Comment = new NullableString(){IsNull = false, Value = "4365678"},
                            OldTestEquipment = DtoFactory.CreateTestEquipmentDtoRandomized(45646),
                            NewTestEquipment = DtoFactory.CreateTestEquipmentDtoRandomized(32423),
                        },
                        new DtoTypes.TestEquipmentDiff()
                        {
                            UserId = 14,
                            Comment = new NullableString(){IsNull = false, Value = "345647"},
                            OldTestEquipment = DtoFactory.CreateTestEquipmentDtoRandomized(111),
                            NewTestEquipment = DtoFactory.CreateTestEquipmentDtoRandomized(2222),
                        },
                    }
                },
                true
             ),
            (
                new ListOfTestEquipmentDiffs()
                {
                    Diffs =
                    {
                        new DtoTypes.TestEquipmentDiff()
                        {
                            UserId = 451,
                            Comment = new NullableString(){IsNull = false, Value = "43547558"},
                            OldTestEquipment = DtoFactory.CreateTestEquipmentDtoRandomized(4567),
                            NewTestEquipment = DtoFactory.CreateTestEquipmentDtoRandomized(12),
                        },
                        new DtoTypes.TestEquipmentDiff()
                        {
                            UserId = 23,
                            Comment = new NullableString(){IsNull = false, Value = "43546"},
                            OldTestEquipment = DtoFactory.CreateTestEquipmentDtoRandomized(124324),
                            NewTestEquipment = DtoFactory.CreateTestEquipmentDtoRandomized(5674),
                        },
                    }
                },
                false
            )
        };

        [TestCaseSource(nameof(InsertUpdateTestEquipmentWithHistoryData))]
        public void UpdateTestEquipmentsWithHistoryCallsUseCase((ListOfTestEquipmentDiffs diffs, bool withModelUpdate) data)
        {
            var useCase = new TestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TestEquipmentService(null, useCase);

            var request = new UpdateTestEquipmentsWithHistoryRequest()
            {
                TestEquipmentDiffs = data.diffs,
                WithTestEquipmentModelUpdate = data.withModelUpdate
            };

            service.UpdateTestEquipmentsWithHistory(request, null);

            var comparer = new Func<DtoTypes.TestEquipmentDiff, TestEquipmentDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment.Value == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareTestEquipmentWithTestEquipmentDto(diff.GetOldTestEquipment(), dtoDiff.OldTestEquipment) &&
                EqualityChecker.CompareTestEquipmentWithTestEquipmentDto(diff.GetNewTestEquipment(), dtoDiff.NewTestEquipment)
            );
            
            Assert.AreEqual(data.withModelUpdate, useCase.UpdateTestEquipmentsWithHistoryParameterWithModelUpdate);

            CheckerFunctions.CollectionAssertAreEquivalent(data.diffs.Diffs, useCase.UpdateTestEquipmentsWithHistoryParameterDiffs, comparer);
        }

        private static IEnumerable<ListOfTestEquipmentModelDiffs> TestEquipmentModelDtoDiffList =
            new List<ListOfTestEquipmentModelDiffs>()
            {
                new ListOfTestEquipmentModelDiffs()
                {
                    Diffs =
                    {
                        new DtoTypes.TestEquipmentModelDiff()
                        {
                            UserId = 1,
                            Comment = new NullableString(){IsNull = false, Value = "234"},
                            OldTestEquipmentModel = DtoFactory.CreateTestEquipmentModelDtoRandomized(32435),
                            NewTestEquipmentModel = DtoFactory.CreateTestEquipmentModelDtoRandomized(56768)
                        },
                        new DtoTypes.TestEquipmentModelDiff()
                        {
                            UserId = 1567,
                            Comment = new NullableString(){IsNull = false, Value = "45657"},
                            OldTestEquipmentModel = DtoFactory.CreateTestEquipmentModelDtoRandomized(342),
                            NewTestEquipmentModel = DtoFactory.CreateTestEquipmentModelDtoRandomized(222)
                        }
                    }
                },
                new ListOfTestEquipmentModelDiffs()
                {
                    Diffs =
                    {
                        new DtoTypes.TestEquipmentModelDiff()
                        {
                            UserId = 1,
                            Comment = new NullableString(){IsNull = false, Value = "aaaa"},
                            OldTestEquipmentModel = DtoFactory.CreateTestEquipmentModelDtoRandomized(77554),
                            NewTestEquipmentModel = DtoFactory.CreateTestEquipmentModelDtoRandomized(5633768)
                        }
                    }
                }
            };

        [TestCaseSource(nameof(TestEquipmentModelDtoDiffList))]
        public void UpdateTestEquipmentModelsWithHistoryCallsUseCase(ListOfTestEquipmentModelDiffs diffs)
        {
            var useCase = new TestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TestEquipmentService(null, useCase);

            var request = new UpdateTestEquipmentModelsWithHistoryRequest()
            {
                TestEquipmentModelDiffs = diffs,
            };

            service.UpdateTestEquipmentModelsWithHistory(request, null);

            var comparer = new Func<DtoTypes.TestEquipmentModelDiff, TestEquipmentModelDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment.Value == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareTestEquipmentModelWithTestEquipmentModel(diff.GetOldTestEquipmentModel(), dtoDiff.OldTestEquipmentModel) &&
                EqualityChecker.CompareTestEquipmentModelWithTestEquipmentModel(diff.GetNewTestEquipmentModel(), dtoDiff.NewTestEquipmentModel)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(diffs.Diffs, useCase.UpdateTestEquipmentModelsWithHistoryParameterDiff, comparer);
        }

        [TestCaseSource(nameof(InsertUpdateTestEquipmentWithHistoryData))]
        public void InsertTestEquipmentsWithHistoryCallsUseCase((ListOfTestEquipmentDiffs testEquipmentDiffs, bool returnList) data)
        {
            var useCase = new TestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TestEquipmentService(null, useCase);

            var request = new InsertTestEquipmentsWithHistoryRequest()
            {
                Diffs = data.testEquipmentDiffs,
                ReturnList = data.returnList
            };

            service.InsertTestEquipmentsWithHistory(request, null);

            Assert.AreEqual(data.returnList, useCase.InsertTestEquipmentsWithHistoryParameterReturnList);

            var comparer = new Func<DtoTypes.TestEquipmentDiff, TestEquipmentDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment.Value == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareTestEquipmentWithTestEquipmentDto(diff.GetOldTestEquipment(), dtoDiff.OldTestEquipment) &&
                EqualityChecker.CompareTestEquipmentWithTestEquipmentDto(diff.GetNewTestEquipment(), dtoDiff.NewTestEquipment)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.testEquipmentDiffs.Diffs, useCase.InsertTestEquipmentsWithHistoryParameterDiff, comparer);
        }

        [TestCaseSource(nameof(TestEquipmentData))]
        public void InsertTestEquipmentsWithHistoryReturnsCorrectValue(List<TestEquipment> testEquipments)
        {
            var useCase = new TestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TestEquipmentService(null, useCase);

            useCase.InsertTestEquipmentsWithHistoryReturnValue = testEquipments;

            var request = new InsertTestEquipmentsWithHistoryRequest()
            {
                Diffs = new ListOfTestEquipmentDiffs()
            };

            var result = service.InsertTestEquipmentsWithHistory(request, null).Result;

            var comparer = new Func<TestEquipment, DtoTypes.TestEquipment, bool>((testEquipment, dtoTestEquipment) =>
                EqualityChecker.CompareTestEquipmentWithTestEquipmentDto(testEquipment, dtoTestEquipment)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(testEquipments, result.TestEquipments, comparer);
        }

        [TestCase("1234")]
        [TestCase("8435345 123")]
        public void IsInventoryNumberUniqueCallsUseCase(string inventoryNumber)
        {
            var useCase = new TestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TestEquipmentService(null, useCase);

            service.IsTestEquipmentInventoryNumberUnique(new BasicTypes.String() { Value = inventoryNumber }, null);

            Assert.AreEqual(inventoryNumber, useCase.IsInventoryNumberUniqueParameter.ToDefaultString());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsInventoryNumberUniqueReturnsCorrectValue(bool isUnique)
        {
            var useCase = new TestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TestEquipmentService(null, useCase);
            useCase.IsInventoryNumberUniqueReturnValue = isUnique;

            var result = service.IsTestEquipmentInventoryNumberUnique(new BasicTypes.String() { Value = "" }, null);

            Assert.AreEqual(isUnique, result.Result.Value);
        }

        [TestCase("1234")]
        [TestCase("8435345 123")]
        public void IsSerialNumberUniqueCallsUseCase(string serialNumber)
        {
            var useCase = new TestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TestEquipmentService(null, useCase);

            service.IsTestEquipmentSerialNumberUnique(new BasicTypes.String() { Value = serialNumber }, null);

            Assert.AreEqual(serialNumber, useCase.IsSerialNumberUniqueParameter.ToDefaultString());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsTestEquipmentSerialNumberUniqueReturnsCorrectValue(bool isUnique)
        {
            var useCase = new TestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TestEquipmentService(null, useCase);
            useCase.IsSerialNumberUniqueReturnValue = isUnique;

            var result = service.IsTestEquipmentSerialNumberUnique(new BasicTypes.String() { Value = "" }, null);

            Assert.AreEqual(isUnique, result.Result.Value);
        }

        [TestCase("1234")]
        [TestCase("8435345 123")]
        public void IsTestEquipmentModelNameUniqueCallsUseCase(string name)
        {
            var useCase = new TestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TestEquipmentService(null, useCase);

            service.IsTestEquipmentModelNameUnique(new BasicTypes.String() { Value = name }, null);

            Assert.AreEqual(name, useCase.IsTestEquipmentModelNameUniqueParameter.ToDefaultString());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsTestEquipmentModelNameUniqueReturnsCorrectValue(bool isUnique)
        {
            var useCase = new TestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TestEquipmentService(null, useCase);
            useCase.IsTestEquipmentModelNameUniqueReturnValue = isUnique;

            var result = service.IsTestEquipmentModelNameUnique(new BasicTypes.String() { Value = "" }, null);

            Assert.AreEqual(isUnique, result.Result.Value);
        }

        [Test]
        public void LoadAvailableTestEquipmentTypesCallsUseCase()
        {
            var useCase = new TestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TestEquipmentService(null, useCase);
            service.LoadAvailableTestEquipmentTypes(new NoParams(), null);
            Assert.IsTrue(useCase.LoadAvailableTestEquipmentTypesCalled);
        }

        private static IEnumerable<List<TestEquipmentType>> TestEquipmentTypeDatas = new List<List<TestEquipmentType>>()
        {
            new List<TestEquipmentType>()
            {
                TestEquipmentType.Wrench,
                TestEquipmentType.Bench,
            },
            new List<TestEquipmentType>()
            {
                TestEquipmentType.TestTool
            }
        };

        [TestCaseSource(nameof(TestEquipmentTypeDatas))]
        public void LoadAvailableTestEquipmentTypesReturnsCorrectValue(List<TestEquipmentType> datas)
        {
            var useCase = new TestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TestEquipmentService(null, useCase);
            useCase.LoadAvailableTestEquipmentTypesReturnValue = datas;
            var result = service.LoadAvailableTestEquipmentTypes(new NoParams(), null);

            var comparer =
                new Func<TestEquipmentType, BasicTypes.Long, bool>((type, lType) =>
                    type == (TestEquipmentType) lType.Value);

            CheckerFunctions.CollectionAssertAreEquivalent(datas, result.Result.Values, comparer);

        }
    }
}
