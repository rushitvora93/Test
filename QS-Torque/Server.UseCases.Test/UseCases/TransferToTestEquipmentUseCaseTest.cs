using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Server.Core;
using Server.Core.Entities;
using Server.Core.Enums;
using Server.UseCases.UseCases;

namespace Server.UseCases.Test.UseCases
{
    public class TransferToTestEquipmentDataAccessMock : ITransferToTestEquipmentDataAccess
    {
        public List<LocationToolAssignmentForTransfer> LoadLocationToolAssignmentsForTransferReturnValue { get; set; } = new List<LocationToolAssignmentForTransfer>();
        public TestType LoadLocationToolAssignmentsForTransferParameter { get; set; }
        public bool LoadProcessControlDataForTransferCalled { get; set; }
        public List<ProcessControlForTransfer> LoadProcessControlDataForTransferReturnValue { get; set; }

        public List<LocationToolAssignmentForTransfer> LoadLocationToolAssignmentsForTransfer(TestType testType)
        {
            LoadLocationToolAssignmentsForTransferParameter = testType;
            return LoadLocationToolAssignmentsForTransferReturnValue;
        }

        public List<ProcessControlForTransfer> LoadProcessControlDataForTransfer()
        {
            LoadProcessControlDataForTransferCalled = true;
            return LoadProcessControlDataForTransferReturnValue;
        }
    }

    class TransferToTestEquipmentUseCaseTest
    {
        [TestCase(TestType.Chk)]
        [TestCase(TestType.Mfu)]
        public void LoadLocationToolAssignmentsForTransferCallsDataAccess(TestType testType)
        {
            var environment = new Environment();

            environment.useCase.LoadLocationToolAssignmentsForTransfer(testType);

            Assert.AreEqual(testType, environment.mocks.dataAccess.LoadLocationToolAssignmentsForTransferParameter);
        }

        [Test]
        public void LoadLocationToolAssignmentsForTransferReturnsCorrectValue()
        {
            var environment = new Environment();

            var entities = new List<LocationToolAssignmentForTransfer>();
            environment.mocks.dataAccess.LoadLocationToolAssignmentsForTransferReturnValue = entities;

            Assert.AreSame(entities, environment.useCase.LoadLocationToolAssignmentsForTransfer(TestType.Chk));
        }

        [Test]
        public void InsertClassicChkTestsCallsDataAccess()
        {
            var environment = new Environment();
            var data = new Dictionary<ClassicChkTest, DateTime>()
            {
                { new ClassicChkTest(), new DateTime() },
                { new ClassicChkTest(), new DateTime() }
            };
            environment.useCase.InsertClassicChkTests(data);
            
            Assert.AreEqual(2, environment.mocks.dataAccessChk.InsertClassicChkTestsParameter.Count);
            Assert.AreSame(data.Keys.ToList()[0], environment.mocks.dataAccessChk.InsertClassicChkTestsParameter[0]);
            Assert.AreSame(data.Keys.ToList()[1], environment.mocks.dataAccessChk.InsertClassicChkTestsParameter[1]);
        }

        [Test]
        public void InsertClassicChkTestsCallsCommitAfterInsert()
        {
            var environment = new Environment();
            var data = new Dictionary<ClassicChkTest, DateTime>();
            environment.useCase.InsertClassicChkTests(data);

            Assert.AreEqual(ClassicChkTestDataMock.ClassicChkTestDataFunction.Commit, environment.mocks.dataAccessChk.CalledFunctions.Last());
        }

        [Test]
        public void InsertClassicChkTestFillsShift()
        {
            var environment = new Environment();
            environment.mocks.shiftManagementData.GetShiftManagementReturnValue = new ShiftManagement()
            {
                FirstShiftStart = TimeSpan.FromHours(6),
                FirstShiftEnd = TimeSpan.FromHours(14),
                SecondShiftStart = TimeSpan.FromHours(14),
                SecondShiftEnd = TimeSpan.FromHours(22),
                ThirdShiftStart = TimeSpan.FromHours(22),
                ThirdShiftEnd = TimeSpan.FromHours(6),
                IsSecondShiftActive = true,
                IsThirdShiftActive = true
            };
            var data = new Dictionary<ClassicChkTest, DateTime>()
            {
                { new ClassicChkTest(), new DateTime(2021, 4, 8, 3, 0, 0) },
                { new ClassicChkTest(), new DateTime(2021, 4, 8, 20, 0, 0) },
                { new ClassicChkTest(), new DateTime(2021, 4, 8, 8, 0, 0) }
            };
            environment.useCase.InsertClassicChkTests(data);

            Assert.AreEqual(Shift.ThirdShiftOfDay, data.Keys.ToList()[0].Shift);
            Assert.AreEqual(Shift.SecondShiftOfDay, data.Keys.ToList()[1].Shift);
            Assert.AreEqual(Shift.FirstShiftOfDay, data.Keys.ToList()[2].Shift);
        }

        [Test]
        public void InsertClassicMfuTestsCallsDataAccess()
        {
            var environment = new Environment();
            var data = new Dictionary<ClassicMfuTest, DateTime>()
            {
                { new ClassicMfuTest(), new DateTime() },
                { new ClassicMfuTest(), new DateTime() }
            };
            environment.useCase.InsertClassicMfuTests(data);

            Assert.AreEqual(2, environment.mocks.dataAccessMfu.InsertClassicMfuTestsParameter.Count);
            Assert.AreSame(data.Keys.ToList()[0], environment.mocks.dataAccessMfu.InsertClassicMfuTestsParameter[0]);
            Assert.AreSame(data.Keys.ToList()[1], environment.mocks.dataAccessMfu.InsertClassicMfuTestsParameter[1]);
        }

        [Test]
        public void InsertClassicMfuTestsCallsCommitAfterInsert()
        {
            var environment = new Environment();
            var data = new Dictionary<ClassicMfuTest, DateTime>();
            environment.useCase.InsertClassicMfuTests(data);

            Assert.AreEqual(ClassicMfuTestDataMock.ClassicMfuTestDataFunction.Commit, environment.mocks.dataAccessMfu.CalledFunctions.Last());
        }

        [Test]
        public void InsertClassicMfuTestFillsShift()
        {
            var environment = new Environment();
            environment.mocks.shiftManagementData.GetShiftManagementReturnValue = new ShiftManagement()
            {
                FirstShiftStart = TimeSpan.FromHours(6),
                FirstShiftEnd = TimeSpan.FromHours(14),
                SecondShiftStart = TimeSpan.FromHours(14),
                SecondShiftEnd = TimeSpan.FromHours(22),
                ThirdShiftStart = TimeSpan.FromHours(22),
                ThirdShiftEnd = TimeSpan.FromHours(6),
                IsSecondShiftActive = true,
                IsThirdShiftActive = true
            };
            var data = new Dictionary<ClassicMfuTest, DateTime>()
            {
                { new ClassicMfuTest(), new DateTime(2021, 4, 8, 3, 0, 0) },
                { new ClassicMfuTest(), new DateTime(2021, 4, 8, 20, 0, 0) },
                { new ClassicMfuTest(), new DateTime(2021, 4, 8, 8, 0, 0) }
            };
            environment.useCase.InsertClassicMfuTests(data);

            Assert.AreEqual(Shift.ThirdShiftOfDay, data.Keys.ToList()[0].Shift);
            Assert.AreEqual(Shift.SecondShiftOfDay, data.Keys.ToList()[1].Shift);
            Assert.AreEqual(Shift.FirstShiftOfDay, data.Keys.ToList()[2].Shift);
        }

        [Test]
        public void LoadProcessControlDataForTransferCallsDataAccess()
        {
            var environment = new Environment();
            environment.useCase.LoadProcessControlDataForTransfer();
            Assert.IsTrue(environment.mocks.dataAccess.LoadProcessControlDataForTransferCalled);
        }

        [Test]
        public void LoadProcessControlDataForTransferReturnsCorrectValue()
        {
            var environment = new Environment();
            var data = new List<ProcessControlForTransfer>();
            environment.mocks.dataAccess.LoadProcessControlDataForTransferReturnValue = data;
            environment.useCase.LoadProcessControlDataForTransfer();
            Assert.AreSame(data, environment.mocks.dataAccess.LoadProcessControlDataForTransferReturnValue);
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    dataAccess = new TransferToTestEquipmentDataAccessMock();
                    dataAccessChk = new ClassicChkTestDataMock();
                    dataAccessMfu = new ClassicMfuTestDataMock();
                    shiftManagementData = new ShiftManagementDataMock() { GetShiftManagementReturnValue = new ShiftManagement()};
                }

                public readonly TransferToTestEquipmentDataAccessMock dataAccess;
                public readonly ClassicChkTestDataMock dataAccessChk;
                public readonly ClassicMfuTestDataMock dataAccessMfu;
                public readonly ShiftManagementDataMock shiftManagementData;
            }

            public Environment()
            {
                mocks = new Mocks();
                useCase = new TransferToTestEquipmentUseCase(mocks.dataAccess, mocks.dataAccessChk, mocks.dataAccessMfu, mocks.shiftManagementData);
            }

            public readonly Mocks mocks;
            public readonly TransferToTestEquipmentUseCase useCase;
        }
    }
}
