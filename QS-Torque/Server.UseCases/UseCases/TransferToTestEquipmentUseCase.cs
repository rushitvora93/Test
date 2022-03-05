using System;
using System.Collections.Generic;
using System.Linq;
using Server.Core;
using Server.Core.Entities;
using Server.Core.Enums;

namespace Server.UseCases.UseCases
{
    public interface ITransferToTestEquipmentDataAccess
    {
        List<LocationToolAssignmentForTransfer> LoadLocationToolAssignmentsForTransfer(TestType testType);
        List<ProcessControlForTransfer> LoadProcessControlDataForTransfer();
    }

    public interface ITransferToTestEquipmentUseCase
    {
        List<LocationToolAssignmentForTransfer> LoadLocationToolAssignmentsForTransfer(TestType testType);
        List<ProcessControlForTransfer> LoadProcessControlDataForTransfer();
        void InsertClassicChkTests(Dictionary<ClassicChkTest, DateTime> testsAndLocalTimestamps);
        void InsertClassicMfuTests(Dictionary<ClassicMfuTest, DateTime> testsAndLocalTimestamps);
    }

    public class TransferToTestEquipmentUseCase : ITransferToTestEquipmentUseCase
    {
        private readonly ITransferToTestEquipmentDataAccess _dataAccess;
        private readonly IClassicChkTestData _classicChkTestData;
        private readonly IClassicMfuTestData _classicMfuTestData;
        private readonly IShiftManagementData _shiftManagementData;

        public TransferToTestEquipmentUseCase(ITransferToTestEquipmentDataAccess dataAccess, IClassicChkTestData classicChkTestData, IClassicMfuTestData classicMfuTestData, IShiftManagementData shiftManagementData)
        {
            _dataAccess = dataAccess;
            _classicChkTestData = classicChkTestData;
            _classicMfuTestData = classicMfuTestData;
            _shiftManagementData = shiftManagementData;
        }
        public List<LocationToolAssignmentForTransfer> LoadLocationToolAssignmentsForTransfer(TestType testType)
        {
            return _dataAccess.LoadLocationToolAssignmentsForTransfer(testType);
        }

        public List<ProcessControlForTransfer> LoadProcessControlDataForTransfer()
        {
            return _dataAccess.LoadProcessControlDataForTransfer();
        }

        public void InsertClassicChkTests(Dictionary<ClassicChkTest, DateTime> testsAndLocalTimestamps)
        {
            var shiftCalculator = new ShiftCalculator(_shiftManagementData.GetShiftManagement());
            foreach (var pair in testsAndLocalTimestamps)
            {
                pair.Key.Shift = shiftCalculator.GetShiftForDateTime(pair.Value);
            }

            _classicChkTestData.InsertClassicChkTests(testsAndLocalTimestamps.Keys.ToList());
            _classicChkTestData.Commit();
        }

        public void InsertClassicMfuTests(Dictionary<ClassicMfuTest, DateTime> testsAndLocalTimestamps)
        {
            var shiftCalculator = new ShiftCalculator(_shiftManagementData.GetShiftManagement());
            foreach (var pair in testsAndLocalTimestamps)
            {
                pair.Key.Shift = shiftCalculator.GetShiftForDateTime(pair.Value);
            }

            _classicMfuTestData.InsertClassicMfuTests(testsAndLocalTimestamps.Keys.ToList());
            _classicMfuTestData.Commit();
        }
    }
}
