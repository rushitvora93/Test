using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Client.TestHelper.Factories;
using Client.TestHelper.Mock;
using Common.Types.Enums;
using Core.Entities;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using TestEquipmentService;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;
using String = BasicTypes.String;
using TestEquipment = DtoTypes.TestEquipment;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    public class TestEquipmentClientMock : ITestEquipmentClient
    {
        public ListOfTestEquipment GetTestEquipmentsByIdsReturnValue { get; set; } = new ListOfTestEquipment(){TestEquipments = { new DtoTypes.TestEquipment()}};
        public ListOfLongs GetTestEquipmentsByIdsParameter { get; set; }
        public UpdateTestEquipmentsWithHistoryRequest UpdateTestEquipmentsWithHistoryParameter { get; set; }
        public UpdateTestEquipmentModelsWithHistoryRequest UpdateTestEquipmentModelsWithHistoryParameter { get; set; }
        public InsertTestEquipmentsWithHistoryRequest InsertTestEquipmentsWithHistoryParameter { get; set; }
        public ListOfTestEquipment InsertTestEquipmentsWithHistoryReturnValue { get; set; } = new ListOfTestEquipment();
        public Bool IsTestEquipmentModelNameUniqueReturnValue { get; set; } = new Bool();
        public String IsTestEquipmentModelNameUniqueParameter { get; set; }
        public String IsTestEquipmentSerialNumberUniqueParameter { get; set; }
        public Bool IsTestEquipmentSerialNumberUniqueReturnValue { get; set; } = new Bool();
        public String IsTestEquipmentInventoryNumberUniqueParameter { get; set; }
        public Bool IsTestEquipmentInventoryNumberUniqueReturnValue { get; set; } = new Bool();
        public ListOfLongs LoadAvailableTestEquipmentTypesReturnValue { get; set; } = new ListOfLongs();
        public bool LoadAvailableTestEquipmentTypesCalled { get; set; }

        public ListOfTestEquipmentModel LoadTestEquipmentModelsReturnValue { get; set; } =
            new ListOfTestEquipmentModel()
            {
                TestEquipmentModels =
                {
                    new DtoTypes.TestEquipmentModel()
                        {TestEquipments = new ListOfTestEquipment() {TestEquipments = {new TestEquipment()}}}
                }
            };

        public bool LoadTestEquipmentModelsCalled { get; set; }

        public ListOfTestEquipmentModel LoadTestEquipmentModels()
        {
            LoadTestEquipmentModelsCalled = true;
            return LoadTestEquipmentModelsReturnValue;
        }

        public ListOfTestEquipment GetTestEquipmentsByIds(ListOfLongs ids)
        {
            GetTestEquipmentsByIdsParameter = ids;
            return GetTestEquipmentsByIdsReturnValue;
        }

        public ListOfTestEquipment InsertTestEquipmentsWithHistory(InsertTestEquipmentsWithHistoryRequest request)
        {
            InsertTestEquipmentsWithHistoryParameter = request;
            return InsertTestEquipmentsWithHistoryReturnValue;
        }

        public void UpdateTestEquipmentsWithHistory(UpdateTestEquipmentsWithHistoryRequest request)
        {
            UpdateTestEquipmentsWithHistoryParameter = request;
        }

        public void UpdateTestEquipmentModelsWithHistory(UpdateTestEquipmentModelsWithHistoryRequest request)
        {
            UpdateTestEquipmentModelsWithHistoryParameter = request;
        }

        public Bool IsTestEquipmentInventoryNumberUnique(String inventoryNumber)
        {
            IsTestEquipmentInventoryNumberUniqueParameter = inventoryNumber;
            return IsTestEquipmentInventoryNumberUniqueReturnValue;
        }

        public Bool IsTestEquipmentSerialNumberUnique(String serialNumber)
        {
            IsTestEquipmentSerialNumberUniqueParameter = serialNumber;
            return IsTestEquipmentSerialNumberUniqueReturnValue;
        }

        public Bool IsTestEquipmentModelNameUnique(String name)
        {
            IsTestEquipmentModelNameUniqueParameter = name;
            return IsTestEquipmentModelNameUniqueReturnValue;
        }

        public ListOfLongs LoadAvailableTestEquipmentTypes()
        {
            LoadAvailableTestEquipmentTypesCalled = true;
            return LoadAvailableTestEquipmentTypesReturnValue;
        }
    }

    public class TestEquipmentDataAccessTest
    {

        [Test]
        public void LoadTestEquipmentModelsCallsClient()
        {
            var environment = new Environment();

            environment.dataAccess.LoadTestEquipmentModels();

            Assert.IsTrue(environment.mocks.testEquipmentClient.LoadTestEquipmentModelsCalled);
        }

        static IEnumerable<(ListOfTestEquipmentModel, ListOfTestEquipment)> LoadTestEquipmentModelsReturnsCorrectValueData = new List<(ListOfTestEquipmentModel, ListOfTestEquipment)>()
        {
            (
                new ListOfTestEquipmentModel()
                {
                    TestEquipmentModels =
                    {
                        DtoFactory.CreateTestEquipmentModelRandomized(213235)
                    }
                },
                new ListOfTestEquipment()
                {
                    TestEquipments =
                    {
                        DtoFactory.CreateTestEquipmentRandomized(21124),
                        DtoFactory.CreateTestEquipmentRandomized(221),
                        DtoFactory.CreateTestEquipmentRandomized(46574),
                    }
                }
             ),
            (
                new ListOfTestEquipmentModel()
                {
                    TestEquipmentModels =
                    {
                        DtoFactory.CreateTestEquipmentModelRandomized(1234),
                    }
                },
                new ListOfTestEquipment()
                {
                    TestEquipments =
                    {
                        DtoFactory.CreateTestEquipmentRandomized(13245677),
                        DtoFactory.CreateTestEquipmentRandomized(123)
                    }
                }
            ),
        };

        [TestCaseSource(nameof(LoadTestEquipmentModelsReturnsCorrectValueData))]
        public void LoadTestEquipmentModelsReturnsCorrectValue((ListOfTestEquipmentModel testEquipmentModels, ListOfTestEquipment testEquipments) data)
        {
            foreach (var model in data.testEquipmentModels.TestEquipmentModels)
            {
                model.TestEquipments = data.testEquipments;
                foreach (var testEquipment in model.TestEquipments.TestEquipments)
                {
                    testEquipment.TestEquipmentModel = model;
                }
            }

            var environment = new Environment();
            environment.mocks.testEquipmentClient.LoadTestEquipmentModelsReturnValue = data.testEquipmentModels;

            var result = environment.dataAccess.LoadTestEquipmentModels();

            var comparer = new Func<DtoTypes.TestEquipmentModel, Core.Entities.TestEquipmentModel, bool>((dto, entity) =>
                EqualityChecker.CompareTestEquipmentModelWithTestEquipmentModelDto(entity, dto)
            );
            CheckerFunctions.CollectionAssertAreEquivalent(data.testEquipmentModels.TestEquipmentModels, result, comparer);

            var counter = 0;
            foreach (var modelDto in data.testEquipmentModels.TestEquipmentModels)
            {
                var comparerTestEquipment = new Func<DtoTypes.TestEquipment, Core.Entities.TestEquipment, bool>((dto, entity) =>
                    EqualityChecker.CompareTestEquipmentWithTestEquipmentDto(entity, dto)
                );

                CheckerFunctions.CollectionAssertAreEquivalent(modelDto.TestEquipments.TestEquipments,
                    result[counter].TestEquipments.ToList(), comparerTestEquipment);
                counter++;
            }
        }

        [Test]
        public void SaveTestEquipmentWithMismatchingIdsThrowsArgumentException()
        {
            var environment = new Environment();
            var testEquipmentDiff = new Core.Diffs.TestEquipmentDiff(CreateTestEquipment.WithId(1), CreateTestEquipment.WithId(2), CreateUser.IdOnly(4));
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.SaveTestEquipment(testEquipmentDiff); });
        }

        [Test]
        public void SaveUnchangedTestEquipmentDontCallClient()
        {
            var environment = new Environment();
            var testEquipmentDiff = new Core.Diffs.TestEquipmentDiff(CreateTestEquipment.WithId(1), CreateTestEquipment.WithId(1), CreateUser.IdOnly(4));
            environment.dataAccess.SaveTestEquipment(testEquipmentDiff);
            Assert.IsNull(environment.mocks.testEquipmentClient.UpdateTestEquipmentsWithHistoryParameter);
        }

        static IEnumerable<(Core.Entities.TestEquipment, Core.Entities.TestEquipment, Core.Entities.User, string comment)> SaveTestEquipmentCallsClientData = new List<(Core.Entities.TestEquipment, Core.Entities.TestEquipment, Core.Entities.User, string)>()
        {
            (
                CreateTestEquipment.RandomizedWithId(4335456, 1),
                CreateTestEquipment.RandomizedWithId(5758, 1),
                CreateUser.IdOnly(1), 
                "ertet"
            ),
            (
                CreateTestEquipment.RandomizedWithId(33345, 2),
                CreateTestEquipment.RandomizedWithId(778,2),
                CreateUser.IdOnly(2), 
                "3242345435"
            ),
        };

        [TestCaseSource(nameof(SaveTestEquipmentCallsClientData))]
        public void SaveTestEquipmentCallsClient((Core.Entities.TestEquipment oldTestEquipment, Core.Entities.TestEquipment newTestEquipment, Core.Entities.User user, string comment) data)
        {
            var environment = new Environment();
            var testEquipmentDiff = new Core.Diffs.TestEquipmentDiff(data.oldTestEquipment, data.newTestEquipment,data.user, new HistoryComment(data.comment));
            environment.dataAccess.SaveTestEquipment(testEquipmentDiff);

            var clientParam = environment.mocks.testEquipmentClient.UpdateTestEquipmentsWithHistoryParameter;
            var clientDiff = clientParam.TestEquipmentDiffs.Diffs.First();

            Assert.AreEqual(1, clientParam.TestEquipmentDiffs.Diffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.IsFalse(clientDiff.Comment.IsNull);
            Assert.AreEqual(data.comment,clientDiff.Comment.Value);
            Assert.IsTrue(EqualityChecker.CompareTestEquipmentWithTestEquipmentDto(data.oldTestEquipment, clientDiff.OldTestEquipment));
            Assert.IsTrue(EqualityChecker.CompareTestEquipmentWithTestEquipmentDto(data.newTestEquipment, clientDiff.NewTestEquipment));
            Assert.IsTrue(clientDiff.OldTestEquipment.Alive);
            Assert.IsTrue(clientDiff.NewTestEquipment.Alive);
            if (clientDiff.OldTestEquipment?.TestEquipmentModel != null)
            {
                Assert.IsTrue(clientDiff.OldTestEquipment.TestEquipmentModel.Alive);
            }

            if (clientDiff.NewTestEquipment?.TestEquipmentModel != null)
            {
                Assert.IsTrue(clientDiff.NewTestEquipment.TestEquipmentModel.Alive);
            }
            Assert.IsTrue(clientParam.WithTestEquipmentModelUpdate);
        }

        [Test]
        public void RemoveTestEquipmentWithoutUserThrowsArgumentException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.RemoveTestEquipment(CreateTestEquipment.WithId(1), null); });
        }

        static IEnumerable<(Core.Entities.TestEquipment, Core.Entities.User)> AddAndRemoveTestEquipmentData = new List<(Core.Entities.TestEquipment, Core.Entities.User)>()
        {
            (
                CreateTestEquipment.Randomized(12343),
                CreateUser.IdOnly(2)
            ),
            (
                CreateTestEquipment.Randomized(678678),
                CreateUser.IdOnly(32)
            )
        };

        [TestCaseSource(nameof(AddAndRemoveTestEquipmentData))]
        public void RemoveTestEquipmentCallsClient((Core.Entities.TestEquipment testEquipment, Core.Entities.User user) data)
        {
            var environment = new Environment();

            environment.dataAccess.RemoveTestEquipment(data.testEquipment, data.user);

            var clientParam = environment.mocks.testEquipmentClient.UpdateTestEquipmentsWithHistoryParameter;
            var clientDiff = clientParam.TestEquipmentDiffs.Diffs.First();

            Assert.AreEqual(1, clientParam.TestEquipmentDiffs.Diffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.AreEqual("", clientDiff.Comment.Value);
            Assert.IsTrue(EqualityChecker.CompareTestEquipmentWithTestEquipmentDto(data.testEquipment, clientDiff.NewTestEquipment));
            Assert.IsTrue(EqualityChecker.CompareTestEquipmentWithTestEquipmentDto(data.testEquipment, clientDiff.OldTestEquipment));
            Assert.AreEqual(true, clientDiff.OldTestEquipment.Alive);
            Assert.AreEqual(false, clientDiff.NewTestEquipment.Alive);
        }

        [Test]
        public void SaveTestEquipmentModelWithMismatchingIdsThrowsArgumentException()
        {
            var environment = new Environment();
            var diff = new Client.Core.Diffs.TestEquipmentModelDiff(CreateTestEquipmentModel.WithId(1), CreateTestEquipmentModel.WithId(2), CreateUser.IdOnly(4));
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.SaveTestEquipmentModel(diff); });
        }

        [Test]
        public void SaveUnchangedTestEquipmentModelDontCallClient()
        {
            var environment = new Environment();
            var testEquipmentModel = CreateTestEquipmentModel.WithId(1);
            var testEquipmentDiff = new Client.Core.Diffs.TestEquipmentModelDiff(testEquipmentModel.CopyDeep(), testEquipmentModel.CopyDeep(), CreateUser.IdOnly(4));
            environment.dataAccess.SaveTestEquipmentModel(testEquipmentDiff);
            Assert.IsNull(environment.mocks.testEquipmentClient.UpdateTestEquipmentModelsWithHistoryParameter);
        }


        static IEnumerable<(Core.Entities.TestEquipmentModel, Core.Entities.TestEquipmentModel, Core.Entities.User, string)> SaveTestEquipmentModelCallsClientData = new List<(Core.Entities.TestEquipmentModel, Core.Entities.TestEquipmentModel, Core.Entities.User, string)>()
        {
            (
                CreateTestEquipmentModel.Randomized(3253467),
                CreateTestEquipmentModel.Randomized(345345),
                CreateUser.IdOnly(1),
                "32435"
            ),
            (
                CreateTestEquipmentModel.Randomized(23455),
                CreateTestEquipmentModel.Randomized(12445),
                CreateUser.IdOnly(15),
                "86768786"
            )
        };

        [TestCaseSource(nameof(SaveTestEquipmentModelCallsClientData))]
        public void SaveTestEquipmentModelCallsClient((Core.Entities.TestEquipmentModel oldTestEquipmentModel, Core.Entities.TestEquipmentModel newTestEquipmentModel, Core.Entities.User user, string comment) data)
        {
            var environment = new Environment();
            data.oldTestEquipmentModel.Id = data.newTestEquipmentModel.Id;
            var testEquipmentDiff = new Client.Core.Diffs.TestEquipmentModelDiff(data.oldTestEquipmentModel,
                data.newTestEquipmentModel, data.user, new HistoryComment(data.comment));
            environment.dataAccess.SaveTestEquipmentModel(testEquipmentDiff);

            var clientParam = environment.mocks.testEquipmentClient.UpdateTestEquipmentModelsWithHistoryParameter;
            var clientDiff = clientParam.TestEquipmentModelDiffs.Diffs.First();

            Assert.AreEqual(1, clientParam.TestEquipmentModelDiffs.Diffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.IsFalse(clientDiff.Comment.IsNull);
            Assert.AreEqual(data.comment, clientDiff.Comment.Value);
            Assert.IsTrue(EqualityChecker.CompareTestEquipmentModelWithTestEquipmentModelDto(data.oldTestEquipmentModel, clientDiff.OldTestEquipmentModel));
            Assert.IsTrue(EqualityChecker.CompareTestEquipmentModelWithTestEquipmentModelDto(data.newTestEquipmentModel, clientDiff.NewTestEquipmentModel));
            Assert.IsTrue(clientDiff.OldTestEquipmentModel.Alive);
            Assert.IsTrue(clientDiff.NewTestEquipmentModel.Alive);
        }

        [Test]
        public void AddingTestEquipmentWithoutUserThrowsArgumentException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.AddTestEquipment(null, null); });
        }


        [TestCaseSource(nameof(AddAndRemoveTestEquipmentData))]
        public void AddTestEquipmentCallsClient((Core.Entities.TestEquipment testEquipment, Core.Entities.User user) data)
        {
            var environment = new Environment();
            environment.mocks.testEquipmentClient.InsertTestEquipmentsWithHistoryReturnValue = new ListOfTestEquipment() { TestEquipments = { new DtoTypes.TestEquipment() } };

            environment.dataAccess.AddTestEquipment(data.testEquipment, data.user);

            var clientParam = environment.mocks.testEquipmentClient.InsertTestEquipmentsWithHistoryParameter;
            var clientDiff = clientParam.Diffs.Diffs.First();

            Assert.AreEqual(1, clientParam.Diffs.Diffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.AreEqual("", clientDiff.Comment.Value);
            Assert.IsTrue(EqualityChecker.CompareTestEquipmentWithTestEquipmentDto(data.testEquipment, clientDiff.NewTestEquipment));
            Assert.IsNull(clientDiff.OldTestEquipment);
            Assert.IsTrue(clientParam.ReturnList);
        }

        static IEnumerable<DtoTypes.TestEquipment> TestEquipmentDtoData = new List<DtoTypes.TestEquipment>()
        {
            DtoFactory.CreateTestEquipmentRandomized(435345),
            DtoFactory.CreateTestEquipmentRandomized(324)
        };

        [TestCaseSource(nameof(TestEquipmentDtoData))]
        public void AddTestEquipmentReturnsCorrectValue(DtoTypes.TestEquipment testEquipmentDto)
        {
            var environment = new Environment();
            environment.mocks.testEquipmentClient.InsertTestEquipmentsWithHistoryReturnValue = new ListOfTestEquipment() { TestEquipments = { testEquipmentDto } };
            var result = environment.dataAccess.AddTestEquipment(CreateTestEquipment.Anonymous(), CreateUser.Anonymous());

            Assert.IsTrue(EqualityChecker.CompareTestEquipmentWithTestEquipmentDto(result, testEquipmentDto));
        }

        [Test]
        public void AddTestEquipmentReturnsNullThrowsException()
        {
            var environment = new Environment();

            Assert.Throws<NullReferenceException>(() =>
            {
                environment.dataAccess.AddTestEquipment(CreateTestEquipment.Anonymous(), CreateUser.Anonymous());
            });
        }

        [TestCase("name 1")]
        [TestCase("abcd")]
        public void IsTestEquipmentModelNameUniqueCallsClient(string name)
        {
            var environment = new Environment();

            environment.dataAccess.IsTestEquipmentModelNameUnique(new TestEquipmentModelName(name));

            Assert.AreEqual(name,environment.mocks.testEquipmentClient.IsTestEquipmentModelNameUniqueParameter.Value);
        }

        [TestCase("inv 1")]
        [TestCase("abcd")]
        public void IsInventoryNumberUniqueCallsClient(string inventoryNumber)
        {
            var environment = new Environment();

            environment.dataAccess.IsInventoryNumberUnique(new TestEquipmentInventoryNumber(inventoryNumber));

            Assert.AreEqual(inventoryNumber, environment.mocks.testEquipmentClient.IsTestEquipmentInventoryNumberUniqueParameter.Value);
        }

        [TestCase("ser 1")]
        [TestCase("abcd")]
        public void IsSerialNumberUniqueCallsClient(string serialNumber)
        {
            var environment = new Environment();

            environment.dataAccess.IsSerialNumberUnique(new TestEquipmentSerialNumber(serialNumber));

            Assert.AreEqual(serialNumber, environment.mocks.testEquipmentClient.IsTestEquipmentSerialNumberUniqueParameter.Value);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsSerialNumberUniqueReturnsCorrectValue(bool isUnique)
        {
            var environment = new Environment();
            environment.mocks.testEquipmentClient.IsTestEquipmentSerialNumberUniqueReturnValue = new Bool() { Value = isUnique };

            Assert.AreEqual(isUnique, environment.dataAccess.IsSerialNumberUnique(new TestEquipmentSerialNumber("")));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsInventoryNumberUniqueReturnsCorrectValue(bool isUnique)
        {
            var environment = new Environment();
            environment.mocks.testEquipmentClient.IsTestEquipmentInventoryNumberUniqueReturnValue = new Bool() { Value = isUnique };

            Assert.AreEqual(isUnique, environment.dataAccess.IsInventoryNumberUnique(new TestEquipmentInventoryNumber("")));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsTestEquipmentModelNameUniqueReturnsCorrectValue(bool isUnique)
        {
            var environment = new Environment();
            environment.mocks.testEquipmentClient.IsTestEquipmentModelNameUniqueReturnValue = new Bool() { Value = isUnique };

            Assert.AreEqual(isUnique, environment.dataAccess.IsTestEquipmentModelNameUnique(new TestEquipmentModelName("")));
        }

        [Test]
        public void LoadAvailableTestEquipmentTypesCallsClient()
        {
            var environment = new Environment();

            environment.dataAccess.LoadAvailableTestEquipmentTypes();

            Assert.IsTrue(environment.mocks.testEquipmentClient.LoadAvailableTestEquipmentTypesCalled);
        }

        static IEnumerable<ListOfLongs> AvailableTestEquipmentTypes = new List<ListOfLongs>()
        {
            new ListOfLongs()
            {
                Values =
                {
                    new Long{Value = 1},
                    new Long{Value = 2},
                    new Long{Value = 3},
                }
            },
            new ListOfLongs()
            {
                Values =
                {
                    new Long{Value = 5},
                    new Long{Value = 7},
                    new Long{Value = 9},
                }
            }
        };

        [TestCaseSource(nameof(AvailableTestEquipmentTypes))]
        public void LoadAvailableTestEquipmentTypesReturnsCorrectValue(ListOfLongs longs)
        {
            var environment = new Environment();
            environment.mocks.testEquipmentClient.LoadAvailableTestEquipmentTypesReturnValue = longs;
            var result = environment.dataAccess.LoadAvailableTestEquipmentTypes();

            var comparer = new Func<TestEquipmentType, Long, bool>((val, valDto) =>
                val == (TestEquipmentType)valDto.Value
            );

            CheckerFunctions.CollectionAssertAreEquivalent(result, longs.Values, comparer);
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    clientFactory = new ClientFactoryMock();
                    channelWrapper = new ChannelWrapperMock();
                    testEquipmentClient = new TestEquipmentClientMock();
                    channelWrapper.GetTestEquipmentClientReturnValue = testEquipmentClient;
                    clientFactory.AuthenticationChannel = channelWrapper;
                }
                public ClientFactoryMock clientFactory;
                public ChannelWrapperMock channelWrapper;
                public TestEquipmentClientMock testEquipmentClient;
                
            }

            public Environment()
            {
                mocks = new Mocks();
                dataAccess = new TestEquipmentDataAccess(mocks.clientFactory);
            }

            public readonly Mocks mocks;
            public readonly TestEquipmentDataAccess dataAccess;
        }
    }
}