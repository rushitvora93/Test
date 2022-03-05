using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Client.Core.Entities;
using Common.Types.Enums;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using Core.UseCases;
using Core.UseCases.Communication;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using TransferToTestEquipmentService;
using DateTime = System.DateTime;
using TestEquipment = Core.Entities.TestEquipment;
using User = Core.Entities.User;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface ITransferToTestEquipmentClient
    {
        ListOfLocationToolAssignmentForTransfer LoadLocationToolAssignmentsForTransfer(Long testType);
        ListOfProcessControlDataForTransfer LoadProcessControlDataForTransfer();
        void InsertClassicChkTests(ListOfClassicChkTestWithLocalTimestamp tests);
        void InsertClassicMfuTests(ListOfClassicMfuTestWithLocalTimestamp tests);
    }

    public class TransferToTestEquipmentDataAccess : ITransferToTestEquipmentDataAccess
    {
        private readonly IClientFactory _clientFactory;
        private readonly ITimeDataAccess _timeDataAccess;
        private ILocationToolAssignmentData _locationToolAssignments;

        public TransferToTestEquipmentDataAccess(IClientFactory clientFactory, ITimeDataAccess timeDataAccess, ILocationToolAssignmentData locationToolAssignments)
        {
            _clientFactory = clientFactory;
            _timeDataAccess = timeDataAccess;
            _locationToolAssignments = locationToolAssignments;
        }

        private ITransferToTestEquipmentClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetTransferToTestEquipmentClient();
        }

        public List<Core.UseCases.Communication.LocationToolAssignmentForTransfer> LoadLocationToolAssignmentsForTransfer(TestType testType)
        {
            var results = GetClient().LoadLocationToolAssignmentsForTransfer(new Long() {Value = (long)testType});
            if (results == null)
            {
                return null;
            }
            var assigner = new Assigner();
            var entities = new List<Core.UseCases.Communication.LocationToolAssignmentForTransfer>();
            foreach (var dto in results.Values)
            {
                var data = new Core.UseCases.Communication.LocationToolAssignmentForTransfer
                {
                    LocationToolAssignmentId = new LocationToolAssignmentId(dto.LocationToolAssignmentId),
                    ToolUsageId = new HelperTableEntityId(dto.ToolUsageId),
                    ToolUsageDescription = new ToolUsageDescription(dto.ToolUsageDescription),
                    LocationId = new LocationId(dto.LocationId),
                    LocationNumber = new LocationNumber(dto.LocationNumber),
                    LocationDescription = new LocationDescription(dto.LocationDescription),
                    LocationFreeFieldCategory = dto.LocationFreeFieldCategory,
                    LocationFreeFieldDocumentation = dto.LocationFreeFieldDocumentation,
                    ToolId = new ToolId(dto.ToolId),
                    ToolInventoryNumber = dto.ToolInventoryNumber,
                    ToolSerialNumber = dto.ToolSerialNumber,
                    SampleNumber = dto.SampleNumber
                };

                if (dto.NextTestDateShift.IsNull)
                {
                    data.NextTestDateShift = null;
                }
                else
                {
                    data.NextTestDateShift = (Shift)dto.NextTestDateShift.Value;
                }

                if (!dto.NextTestDate.IsNull)
                {
                    DateTime? localNextDate = null;
                    assigner.Assign((value) => { localNextDate = value; }, dto.NextTestDate);
                    data.NextTestDate = _timeDataAccess.ConvertToLocalDate(localNextDate);
                }

                if (!dto.LastTestDate.IsNull)
                {
                    DateTime? localLastDate = null;
                    assigner.Assign((value) => { localLastDate = value; }, dto.LastTestDate);
                    data.LastTestDate = _timeDataAccess.ConvertToLocalDate(localLastDate);
                }

                if (dto.TestInterval != null)
                {
                    assigner.Assign((value) => { data.TestInterval = value; }, dto.TestInterval);
                }

                entities.Add(data);
            }
            return entities;
        }

        public List<TestEquipmentTestResult> FillWithLocationToolAssignmentsData(DataGateResults results)
        {
            var locationToolAssignments =
                _locationToolAssignments.GetLocationToolAssignmentsByIds(
                    results.Results.Select(result => new LocationToolAssignmentId(result.LocationToolAssignmentId)).ToList());
            if (locationToolAssignments == null || locationToolAssignments.Count == 0)
            {
                return new List<TestEquipmentTestResult>();
            }
            return results.Results.Select(element =>
            {
                var assignment = locationToolAssignments.Find(ltassignment =>
                    ltassignment.Id.Equals(new LocationToolAssignmentId(element.LocationToolAssignmentId)));
                return new TestEquipmentTestResult
                {
                    LocationNumber = assignment?.AssignedLocation.Number,
                    LocationDescription = assignment?.AssignedLocation.Description,
                    ToolInventoryNumber = assignment?.AssignedTool.InventoryNumber?.ToDefaultString(),
                    ToolSerialNumber = assignment?.AssignedTool.SerialNumber?.ToDefaultString(),
                    TestResult = new TestResult(666),
                    ResultFromDataGate = element,
                    LocationToolAssignment = assignment
                };
            }).ToList();
        }


        public void SaveTestEquipmentTestResult(TestEquipment testEquipment, List<TestEquipmentTestResult> testEquipmentTestResults, (double cm, double cmk) cmCmk, User user)
        {
            var chkTests = testEquipmentTestResults.Where(IsTestEquipmentTestResultFromChk).ToList();
            if (chkTests.Count > 0)
            {
                InsertClassicChkTest(testEquipment, chkTests, user);
            }

            var mfuTests = testEquipmentTestResults.Where(x => !IsTestEquipmentTestResultFromChk(x)).ToList();
            if (mfuTests.Count > 0)
            {
                InsertClassicMfuTest(testEquipment, mfuTests, user, cmCmk);
            }
        }

        public List<ProcessControlForTransfer> LoadProcessControlDataForTransfer()
        {
            var results = GetClient().LoadProcessControlDataForTransfer();
            if (results == null)
            {
                return null;
            }

            var assigner = new Assigner();
            var entities = new List<Core.UseCases.Communication.ProcessControlForTransfer>();
            foreach (var dto in results.Values)
            {
                var data = new Core.UseCases.Communication.ProcessControlForTransfer
                {
                    LocationId = new LocationId(dto.LocationId),
                    LocationDescription = new LocationDescription(dto.LocationDescription),
                    LocationNumber = new LocationNumber(dto.LocationNumber),
                    ProcessControlConditionId = new ProcessControlConditionId(dto.ProcessControlConditionId),
                    ProcessControlTechId = new ProcessControlTechId(dto.ProcessControlTechId),
                    TestMethod = (TestMethod)dto.TestMethod,
                    MaximumTorque = dto.MaximumTorque.IsNull ? null : Torque.FromNm(dto.MaximumTorque.Value),
                    MinimumTorque = dto.MinimumTorque.IsNull ? null : Torque.FromNm(dto.MinimumTorque.Value),
                    SetPointTorque = dto.SetPointTorque.IsNull ? null : Torque.FromNm(dto.SetPointTorque.Value),
                    SampleNumber = dto.SampleNumber,
                    
                };

                if (dto.NextTestDateShift.IsNull)
                {
                    data.NextTestDateShift = null;
                }
                else
                {
                    data.NextTestDateShift = (Shift)dto.NextTestDateShift.Value;
                }

                if (!dto.NextTestDate.IsNull)
                {
                    DateTime? localNextDate = null;
                    assigner.Assign((value) => { localNextDate = value; }, dto.NextTestDate);
                    data.NextTestDate = _timeDataAccess.ConvertToLocalDate(localNextDate);
                }

                if (!dto.LastTestDate.IsNull)
                {
                    DateTime? localLastTestDate = null;
                    assigner.Assign((value) => { localLastTestDate = value; }, dto.LastTestDate);
                    data.LastTestDate = _timeDataAccess.ConvertToLocalDate(localLastTestDate);
                }

                if (dto.TestInterval != null)
                {
                    assigner.Assign((value) => { data.TestInterval = value; }, dto.TestInterval);
                }

                entities.Add(data);
            }

            return entities;
        }

        private void InsertClassicMfuTest(TestEquipment testEquipment,List<TestEquipmentTestResult> testEquipmentTestResults, User user, (double cm, double cmk) cmCmk)
        {
            var assigner = new Assigner();
            var tests = new ListOfClassicMfuTestWithLocalTimestamp();
            foreach (var result in testEquipmentTestResults)
            {
                var classicTest = new DtoTypes.ClassicMfuTest()
                {
                    NumberOfTests = result.SampleCount,
                    ToolId = result.LocationToolAssignment.AssignedTool.Id.ToLong(),
                    Cm = result.Cm,
                    Cmk = result.Cmk,
                    LimitCm = cmCmk.cm,
                    LimitCmk = cmCmk.cmk,
                    TestValueMinimum = result.Values.Min(),
                    TestValueMaximum = result.Values.Max(),
                    Average = result.Average,
                    StandardDeviation = new NullableDouble() { IsNull = result.StandardDeviation == null, Value = result.StandardDeviation.GetValueOrDefault(0) },
                    Result = result.TestResult.LongValue,
                    LowerLimitUnit1 = result.ResultFromDataGate.Min1,
                    NominalValueUnit1 = result.ResultFromDataGate.Nom1,
                    UpperLimitUnit1 = result.ResultFromDataGate.Max1,
                    Unit1Id = result.ResultFromDataGate.Unit1Id,
                    LowerLimitUnit2 = result.ResultFromDataGate.Min2,
                    NominalValueUnit2 = result.ResultFromDataGate.Nom2,
                    UpperLimitUnit2 = result.ResultFromDataGate.Max2,
                    Unit2Id = result.ResultFromDataGate.Unit2Id,
                    ToleranceClassUnit1 = result.LocationToolAssignment.TestParameters.ToleranceClassTorque.Id.ToLong(),
                    ToleranceClassUnit2 = result.LocationToolAssignment.TestParameters.ToleranceClassAngle.Id.ToLong(),
                    ControlledByUnitId = (long)result.ControlUnit(),
                    User = new DtoTypes.User() { UserId = user.UserId.ToLong() },
                    TestEquipment = new DtoTypes.TestEquipment() { Id = testEquipment.Id.ToLong() },
                    ThresholdTorque = result.LocationToolAssignment.TestParameters.ThresholdTorque.Degree,
                    SensorSerialNumber = new NullableString() { IsNull = true },
                    LocationToolAssignmentId = result.LocationToolAssignment.Id.ToLong(),
                    TestLocation = new DtoTypes.ClassicTestLocation()
                    {
                        LocationId = result.LocationToolAssignment.AssignedLocation.Id.ToLong(),
                        LocationDirectoryId = result.LocationToolAssignment.AssignedLocation.ParentDirectoryId.ToLong(),
                        TreePath = new NullableString() { IsNull = false, Value = result.LocationTreePath }
                    }
                };

                assigner.Assign((value) => { classicTest.Timestamp = value; }, _timeDataAccess.ConvertToUtc(result.TestTimestamp));

                classicTest.TestValues = new ListOfClassicMfuTestValue();
                var pos = 0;
                foreach (var value in result.ResultFromDataGate.Values)
                {
                    classicTest.TestValues.ClassicMfuTestValues.Add(new DtoTypes.ClassicMfuTestValue()
                    {
                        Position = pos,
                        ValueUnit1 = value.Value1,
                        ValueUnit2 = value.Value2,
                    });
                    pos++;
                }

                var classicTestWithLocalTimestamp = new ClassicMfuTestWithLocalTimestamp()
                {
                    ClassicMfuTest = classicTest
                };
                assigner.Assign((value) => { classicTestWithLocalTimestamp.LocalTimestamp = value; }, result.TestTimestamp);

                tests.ClassicMfuTests.Add(classicTestWithLocalTimestamp);
            }

            GetClient().InsertClassicMfuTests(tests);
        }

        private void InsertClassicChkTest(TestEquipment testEquipment, List<TestEquipmentTestResult> testEquipmentTestResults, User user)
        {
            var assigner = new Assigner();
            var tests = new ListOfClassicChkTestWithLocalTimestamp();
            foreach (var result in testEquipmentTestResults)
            {
                var classicTest = new DtoTypes.ClassicChkTest()
                {
                    NumberOfTests = result.SampleCount,
                    ToolId = result.LocationToolAssignment.AssignedTool.Id.ToLong(),
                    TestValueMinimum = result.Values.Min(),
                    TestValueMaximum = result.Values.Max(),
                    Average = result.Average,
                    StandardDeviation = new NullableDouble() { IsNull = result.StandardDeviation == null, Value = result.StandardDeviation.GetValueOrDefault(0) },
                    Result = result.TestResult.LongValue,
                    LowerLimitUnit1 = result.ResultFromDataGate.Min1,
                    NominalValueUnit1 = result.ResultFromDataGate.Nom1,
                    UpperLimitUnit1 = result.ResultFromDataGate.Max1,
                    Unit1Id = result.ResultFromDataGate.Unit1Id,
                    LowerLimitUnit2 = result.ResultFromDataGate.Min2,
                    NominalValueUnit2 = result.ResultFromDataGate.Nom2,
                    UpperLimitUnit2 = result.ResultFromDataGate.Max2,
                    Unit2Id = result.ResultFromDataGate.Unit2Id,
                    ToleranceClassUnit1 = result.LocationToolAssignment.TestParameters.ToleranceClassTorque.Id.ToLong(),
                    ToleranceClassUnit2 = result.LocationToolAssignment.TestParameters.ToleranceClassAngle.Id.ToLong(),
                    ControlledByUnitId = (long)result.ControlUnit(),
                    User = new DtoTypes.User() { UserId = user.UserId.ToLong() },
                    TestEquipment = new DtoTypes.TestEquipment() { Id = testEquipment.Id.ToLong() },
                    ThresholdTorque = result.LocationToolAssignment.TestParameters.ThresholdTorque.Degree,
                    SensorSerialNumber = new NullableString() { IsNull = true },
                    LocationToolAssignmentId = result.LocationToolAssignment.Id.ToLong(),
                    TestLocation = new DtoTypes.ClassicTestLocation()
                    {
                        LocationId = result.LocationToolAssignment.AssignedLocation.Id.ToLong(),
                        LocationDirectoryId = result.LocationToolAssignment.AssignedLocation.ParentDirectoryId.ToLong(),
                        TreePath = new NullableString() { IsNull = false, Value = result.LocationTreePath }
                    }
                };

                assigner.Assign((value) => { classicTest.Timestamp = value; }, _timeDataAccess.ConvertToUtc(result.TestTimestamp));

                classicTest.TestValues = new ListOfClassicChkTestValue();
                var pos = 0;
                foreach (var value in result.ResultFromDataGate.Values)
                {
                    classicTest.TestValues.ClassicChkTestValues.Add(new DtoTypes.ClassicChkTestValue()
                    {
                        Position = pos,
                        ValueUnit1 = value.Value1,
                        ValueUnit2 = value.Value2,
                    });
                    pos++;
                }

                var classicTestWithLocalTimestamp = new ClassicChkTestWithLocalTimestamp()
                {
                    ClassicChkTest = classicTest
                };
                assigner.Assign((value) => { classicTestWithLocalTimestamp.LocalTimestamp = value; }, result.TestTimestamp);

                tests.ClassicChkTests.Add(classicTestWithLocalTimestamp);
            }

            GetClient().InsertClassicChkTests(tests);
        }

        private bool IsTestEquipmentTestResultFromChk(TestEquipmentTestResult testEquipmentTestResult)
        {
            return testEquipmentTestResult.SampleCount <= 10;
        }
    }
}
