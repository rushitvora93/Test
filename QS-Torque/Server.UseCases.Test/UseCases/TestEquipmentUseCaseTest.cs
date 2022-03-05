using System.Collections.Generic;
using System.Linq;
using Common.Types.Enums;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.TestHelper.Factories;
using Server.UseCases.UseCases;

namespace Server.UseCases.Test.UseCases
{
    public enum TestEquipmentFunction
    {
        InsertTestEquipmentsWithHistory,
        UpdateTestEquipmentsWithHistory,
        UpdateTestEquipmentModelsWithHistoryParameter,
        CommitTestEquipment,
        CommitTestEquipmentModel,

    }
    public class TestEquipmentDataAccessMock : ITestEquipmentDataAccess
    {
        public List<TestEquipment> GetTestEquipmentsByIdsReturnValue { get; set; }
        public List<TestEquipmentId> GetTestEquipmentsByIdsParameter { get; set; }
        public List<TestEquipmentDiff> UpdateTestEquipmentsWithHistoryParameter { get; set; }
        public List<TestEquipmentFunction> CalledFunctions { get; set; } = new List<TestEquipmentFunction>();
        public List<TestEquipment> InsertTestEquipmentsWithHistoryReturnValue { get; set; }
        public bool InsertTestEquipmentsWithHistoryParameterReturnList { get; set; }
        public List<TestEquipmentDiff> InsertTestEquipmentsWithHistoryParameterDiff { get; set; }
        public bool IsSerialNumberUniqueReturnValue { get; set; }
        public TestEquipmentSerialNumber IsSerialNumberUniqueParameter { get; set; }
        public bool IsInventoryNumberUniqueReturnValue { get; set; }
        public TestEquipmentInventoryNumber IsInventoryNumberUniqueParameter { get; set; }
        public List<TestEquipmentType> LoadAvailableTestEquipmentTypesReturnValue { get; set; }
        public bool LoadAvailableTestEquipmentTypesCalled { get; set; }

        public List<TestEquipment> GetTestEquipmentsByIds(List<TestEquipmentId> ids)
        {
            GetTestEquipmentsByIdsParameter = ids;
            return GetTestEquipmentsByIdsReturnValue;
        }

        public List<TestEquipment> InsertTestEquipmentsWithHistory(List<TestEquipmentDiff> diffs, bool returnList)
        {
            CalledFunctions.Add(TestEquipmentFunction.InsertTestEquipmentsWithHistory);
            InsertTestEquipmentsWithHistoryParameterDiff = diffs;
            InsertTestEquipmentsWithHistoryParameterReturnList = returnList;
            return InsertTestEquipmentsWithHistoryReturnValue;
        }

        public void UpdateTestEquipmentsWithHistory(List<TestEquipmentDiff> testEquipmentDiffs)
        {
            CalledFunctions.Add(TestEquipmentFunction.UpdateTestEquipmentsWithHistory);
            UpdateTestEquipmentsWithHistoryParameter = testEquipmentDiffs;
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

        public void Commit()
        {
            CalledFunctions.Add(TestEquipmentFunction.CommitTestEquipment);
        }

        public List<TestEquipmentType> LoadAvailableTestEquipmentTypes()
        {
            LoadAvailableTestEquipmentTypesCalled = true;
            return LoadAvailableTestEquipmentTypesReturnValue;
        }
    }

    public class TestEquipmentModelDataAccessMock : ITestEquipmentModelDataAccess
    {
        private readonly TestEquipmentDataAccessMock _testEquipmentDataAccess;

        public TestEquipmentModelDataAccessMock(TestEquipmentDataAccessMock testEquipmentDataAccess)
        {
            _testEquipmentDataAccess = testEquipmentDataAccess;
        }

        public List<TestEquipmentModelDiff> UpdateTestEquipmentModelsWithHistoryParameter { get; set; }
        public List<TestEquipmentModel> LoadTestEquipmentModelsReturnValue { get; set; }
        public bool LoadTestEquipmentModelsCalled { get; set; }
        public List<TestEquipmentFunction> CalledFunctions { get; set; } = new List<TestEquipmentFunction>();
        public bool IsTestEquipmentModelNameUniqueReturnValue { get; set; }
        public TestEquipmentModelName IsTestEquipmentModelNameUniqueParameter { get; set; }

        public List<TestEquipmentModel> LoadTestEquipmentModels()
        {
            LoadTestEquipmentModelsCalled = true;
            return LoadTestEquipmentModelsReturnValue;
        }

        public void UpdateTestEquipmentModelsWithHistory(List<TestEquipmentModelDiff> testEquipmentModelDiffs)
        {
            _testEquipmentDataAccess.CalledFunctions.Add(TestEquipmentFunction.UpdateTestEquipmentModelsWithHistoryParameter);
            CalledFunctions.Add(TestEquipmentFunction.UpdateTestEquipmentModelsWithHistoryParameter);
            UpdateTestEquipmentModelsWithHistoryParameter = testEquipmentModelDiffs;
        }

        public bool IsTestEquipmentModelNameUnique(TestEquipmentModelName name)
        {
            IsTestEquipmentModelNameUniqueParameter = name;
            return IsTestEquipmentModelNameUniqueReturnValue;
        }

        public void Commit()
        {
            CalledFunctions.Add(TestEquipmentFunction.CommitTestEquipmentModel);
        }
    }

    public class TestEquipmentUseCaseTest
    {
        [Test]
        public void LoadTestEquipmentModelsCallsDataAccess()
        {
            var environment = new Environment();
            environment.useCase.LoadTestEquipmentModels();

            Assert.IsTrue(environment.mocks.testEquipmentModelDataAccess.LoadTestEquipmentModelsCalled);
        }

        [Test]
        public void LoadTestEquipmentModelsReturnsCorrectValue()
        {
            var environment = new Environment();
            var testEquipmentModels = new List<TestEquipmentModel>();
            environment.mocks.testEquipmentModelDataAccess.LoadTestEquipmentModelsReturnValue = testEquipmentModels;
            var result = environment.useCase.LoadTestEquipmentModels();

            Assert.AreSame(testEquipmentModels, result);
        }

        [Test]
        public void GetTestEquipmentsByIdsCallsDataAccess()
        {
            var environment = new Environment();
            var ids = new List<TestEquipmentId>();
            environment.useCase.GetTestEquipmentsByIds(ids);

            Assert.AreSame(ids, environment.mocks.testEquipmentDataAccess.GetTestEquipmentsByIdsParameter);
        }

        [Test]
        public void GetTestEquipmentsByIdsReturnsCorrectValue()
        {
            var environment = new Environment();
            var testEquipments = new List<TestEquipment>();
            environment.mocks.testEquipmentDataAccess.GetTestEquipmentsByIdsReturnValue = testEquipments;
            var result = environment.useCase.GetTestEquipmentsByIds(new List<TestEquipmentId>());

            Assert.AreSame(testEquipments, result);
        }

        [Test]
        public void UpdateTestEquipmentsWithHistoryCallsTestEquipmentDataAccess()
        {
            var environment = new Environment();
            var testEquipments = new List<TestEquipmentDiff>();
            environment.useCase.UpdateTestEquipmentsWithHistory(testEquipments, true);

            Assert.AreSame(testEquipments, environment.mocks.testEquipmentDataAccess.UpdateTestEquipmentsWithHistoryParameter);
        }


        private static IEnumerable<List<TestEquipmentDiff>>
            UpdateTestEquipmentsWithHistoryCallsTestEquipmentModelDataAccessData = new List<List<TestEquipmentDiff>>()
            {
                new List<TestEquipmentDiff>()
                {
                    new TestEquipmentDiff(CreateUser.IdOnly(1), new HistoryComment(""),CreateTestEquipment.Randomized(345345), CreateTestEquipment.Randomized(45524)),
                    new TestEquipmentDiff(CreateUser.IdOnly(7), new HistoryComment("56757"),CreateTestEquipment.Randomized(6789), CreateTestEquipment.Randomized(324))
                }
            };

        [TestCaseSource(nameof(UpdateTestEquipmentsWithHistoryCallsTestEquipmentModelDataAccessData))]
        public void UpdateTestEquipmentsWithHistoryCallsTestEquipmentModelDataAccess(List<TestEquipmentDiff> diffs)
        {
            var environment = new Environment();
            environment.useCase.UpdateTestEquipmentsWithHistory(diffs, true);
            var modelDiffs = diffs.Select(x => new Server.Core.Diffs.TestEquipmentModelDiff(x.GetUser(), x.GetComment(),
                x.GetOldTestEquipment().TestEquipmentModel, x.GetNewTestEquipment().TestEquipmentModel)).ToList();
            var i = 0;
            foreach (var modelDiff in modelDiffs)
            {
                var param = environment.mocks.testEquipmentModelDataAccess.UpdateTestEquipmentModelsWithHistoryParameter[i];
                Assert.AreEqual(modelDiff.GetUser(), param.GetUser());
                Assert.AreEqual(modelDiff.GetComment(), param.GetComment());
                Assert.AreEqual(modelDiff.GetNewTestEquipmentModel(), param.GetNewTestEquipmentModel());
                Assert.AreEqual(modelDiff.GetOldTestEquipmentModel(), param.GetOldTestEquipmentModel());
                i++;
            }
        }

        [TestCaseSource(nameof(UpdateTestEquipmentsWithHistoryCallsTestEquipmentModelDataAccessData))]
        public void UpdateTestEquipmentsWithHistoryWithoutUpdateModelDontCallTestEquipmentModelDataAccess(List<TestEquipmentDiff> diffs)
        {
            var environment = new Environment();
            environment.useCase.UpdateTestEquipmentsWithHistory(diffs, false);

            Assert.IsNull(environment.mocks.testEquipmentModelDataAccess.UpdateTestEquipmentModelsWithHistoryParameter);
        }

        [Test]
        public void UpdateTestEquipmentsWithHistoryWithSameModelDontCallTestEquipmentModelDataAccess()
        {
            var environment = new Environment();
            
            var model = CreateTestEquipment.Randomized(345345);
            var diff = new TestEquipmentDiff(CreateUser.IdOnly(1), new HistoryComment(""), model, model);
            environment.useCase.UpdateTestEquipmentsWithHistory(new List<TestEquipmentDiff>(){ diff }, true);

            Assert.AreEqual(0, environment.mocks.testEquipmentModelDataAccess.UpdateTestEquipmentModelsWithHistoryParameter.Count);
        }

        [TestCaseSource(nameof(UpdateTestEquipmentsWithHistoryCallsTestEquipmentModelDataAccessData))]
        public void UpdateTestEquipmentsWithHistoryCallsCommitAfterWork(List<TestEquipmentDiff> diffs)
        {
            var environment = new Environment();

            environment.useCase.UpdateTestEquipmentsWithHistory(diffs, true);

            Assert.AreEqual(TestEquipmentFunction.CommitTestEquipment, environment.mocks.testEquipmentDataAccess.CalledFunctions.Last());
        }

        [Test]
        public void UpdateTestEquipmentModelsWithHistoryCallsDataAccess()
        {
            var environment = new Environment();
            var diffs = new List<TestEquipmentModelDiff>();
            environment.useCase.UpdateTestEquipmentModelsWithHistory(diffs);

            Assert.AreSame(diffs, environment.mocks.testEquipmentModelDataAccess.UpdateTestEquipmentModelsWithHistoryParameter);
        }

        [Test]
        public void UpdateTestEquipmentModelsWithHistoryCallsCommitAfterWork()
        {
            var environment = new Environment();
            var diffs = new List<TestEquipmentModelDiff>();
            environment.useCase.UpdateTestEquipmentModelsWithHistory(diffs);

            Assert.AreEqual(TestEquipmentFunction.CommitTestEquipmentModel, environment.mocks.testEquipmentModelDataAccess.CalledFunctions.Last());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InsertTestEquipmentsWithHistoryCallsDataAccess(bool returnList)
        {
            var environment = new Environment();
            var diffs = new List<TestEquipmentDiff>();
            environment.useCase.InsertTestEquipmentsWithHistory(diffs, returnList);

            Assert.AreSame(diffs, environment.mocks.testEquipmentDataAccess.InsertTestEquipmentsWithHistoryParameterDiff);
            Assert.AreEqual(returnList, environment.mocks.testEquipmentDataAccess.InsertTestEquipmentsWithHistoryParameterReturnList);
        }

        [Test]
        public void InsertTestEquipmentsWithHistoryCallsCommitAfterWork()
        {
            var environment = new Environment();
            
            environment.useCase.InsertTestEquipmentsWithHistory(new List<TestEquipmentDiff>(), true);

            Assert.AreEqual(TestEquipmentFunction.CommitTestEquipment, environment.mocks.testEquipmentDataAccess.CalledFunctions.Last());
        }

        [Test]
        public void InsertTestEquipmentsWithHistoryReturnsCorrectValue()
        {
            var environment = new Environment();
            var testEquipments = new List<TestEquipment>();
            environment.mocks.testEquipmentDataAccess.InsertTestEquipmentsWithHistoryReturnValue = testEquipments;
            var result = environment.useCase.InsertTestEquipmentsWithHistory(new List<TestEquipmentDiff>(), true);

            Assert.AreSame(testEquipments, result);
        }

        [TestCase("234345")]
        [TestCase("abcdef")]
        public void IsInventoryNumberUniqueCallsDataAccess(string inventoryNumber)
        {
            var environment = new Environment();

            environment.useCase.IsInventoryNumberUnique(new TestEquipmentInventoryNumber(inventoryNumber));

            Assert.AreEqual(inventoryNumber, environment.mocks.testEquipmentDataAccess.IsInventoryNumberUniqueParameter.ToDefaultString());
        }

        [TestCase(false)]
        [TestCase(true)]
        public void IsInventoryNumberUniqueReturnsCorrectValue(bool isUnique)
        {
            var environment = new Environment();

            environment.mocks.testEquipmentDataAccess.IsInventoryNumberUniqueReturnValue = isUnique;

            Assert.AreEqual(isUnique, environment.useCase.IsInventoryNumberUnique(new TestEquipmentInventoryNumber("")));
        }

        [TestCase("234345")]
        [TestCase("abcdef")]
        public void IsSerialNumberUniqueCallsDataAccess(string serialNumber)
        {
            var environment = new Environment();

            environment.useCase.IsSerialNumberUnique(new TestEquipmentSerialNumber(serialNumber));

            Assert.AreEqual(serialNumber, environment.mocks.testEquipmentDataAccess.IsSerialNumberUniqueParameter.ToDefaultString());
        }

        [TestCase(false)]
        [TestCase(true)]
        public void IsSerialNumberUniqueReturnsCorrectValue(bool isUnique)
        {
            var environment = new Environment();

            environment.mocks.testEquipmentDataAccess.IsSerialNumberUniqueReturnValue = isUnique;

            Assert.AreEqual(isUnique, environment.useCase.IsSerialNumberUnique(new TestEquipmentSerialNumber("")));
        }

        [TestCase("234345")]
        [TestCase("abcdef")]
        public void IsTestEquipmentModelNameUniqueCallsDataAccess(string name)
        {
            var environment = new Environment();

            environment.useCase.IsTestEquipmentModelNameUnique(new TestEquipmentModelName(name));

            Assert.AreEqual(name, environment.mocks.testEquipmentModelDataAccess.IsTestEquipmentModelNameUniqueParameter.ToDefaultString());
        }

        [TestCase(false)]
        [TestCase(true)]
        public void IsTestEquipmentModelNameUniqueReturnsCorrectValue(bool isUnique)
        {
            var environment = new Environment();

            environment.mocks.testEquipmentModelDataAccess.IsTestEquipmentModelNameUniqueReturnValue = isUnique;

            Assert.AreEqual(isUnique, environment.useCase.IsTestEquipmentModelNameUnique(new TestEquipmentModelName("")));
        }

        [Test]
        public void LoadAvailableTestEquipmentTypesCallsDataAccess()
        {
            var environment = new Environment();
            environment.useCase.LoadAvailableTestEquipmentTypes();

            Assert.IsTrue(environment.mocks.testEquipmentDataAccess.LoadAvailableTestEquipmentTypesCalled);
        }

        [Test]
        public void LoadAvailableTestEquipmentTypesReturnsCorrectValue()
        {
            var environment = new Environment();
            var testEquipmentTypes = new List<TestEquipmentType>();
            environment.mocks.testEquipmentDataAccess.LoadAvailableTestEquipmentTypesReturnValue = testEquipmentTypes;
            var result = environment.useCase.LoadAvailableTestEquipmentTypes();

            Assert.AreSame(testEquipmentTypes, result);
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    testEquipmentDataAccess = new TestEquipmentDataAccessMock();
                    testEquipmentModelDataAccess = new TestEquipmentModelDataAccessMock(testEquipmentDataAccess);
                }

                public readonly TestEquipmentDataAccessMock testEquipmentDataAccess;
                public readonly TestEquipmentModelDataAccessMock testEquipmentModelDataAccess;
            }

            public Environment()
            {
                mocks = new Mocks();
                useCase = new TestEquipmentUseCase(mocks.testEquipmentDataAccess, mocks.testEquipmentModelDataAccess);
            }

            public readonly Mocks mocks;
            public readonly TestEquipmentUseCase useCase;
        }
    }
}
