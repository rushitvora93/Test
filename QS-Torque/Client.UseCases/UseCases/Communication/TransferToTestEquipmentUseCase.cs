using System;
using System.Collections.Generic;
using System.Linq;
using Client.Core.Entities;
using Client.UseCases.UseCases;
using Common.Types.Enums;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using Core.UseCases.Communication.DataGate;

namespace Core.UseCases.Communication
{
    public class LocationToolAssignmentForTransfer
    {
        public LocationToolAssignmentId LocationToolAssignmentId;
        public LocationNumber LocationNumber;
        public LocationId LocationId;
        public LocationDescription LocationDescription;
        public string LocationFreeFieldCategory;
        public bool LocationFreeFieldDocumentation;
        public HelperTableEntityId ToolUsageId;
        public ToolUsageDescription ToolUsageDescription;
        public ToolId ToolId;
        public string ToolSerialNumber;
        public string ToolInventoryNumber;
        public int SampleNumber { get; set; }
        public Interval TestInterval { get; set; }
        public DateTime? LastTestDate { get; set; }
        public DateTime? NextTestDate { get; set; }
        public Shift? NextTestDateShift { get; set; }
    }

    public class ProcessControlForTransfer
    {
        public LocationNumber LocationNumber;
        public LocationId LocationId;
        public LocationDescription LocationDescription;
        public ProcessControlConditionId ProcessControlConditionId;
        public ProcessControlTechId ProcessControlTechId;
        public Torque SetPointTorque;
        public Torque MinimumTorque;
        public Torque MaximumTorque;
        public TestMethod TestMethod;
        public DateTime? LastTestDate;
        public DateTime? NextTestDate;
        public Interval TestInterval;
        public int SampleNumber;
        public Shift? NextTestDateShift;
    }

    public class TestEquipmentTestResult
    {
        public LocationNumber LocationNumber { get; set; }
        public LocationDescription LocationDescription { get; set; }
        public string ToolInventoryNumber { get; set; }
        public string ToolSerialNumber { get; set; }

        public double NominalValue => LocationToolAssignment.TestParameters.ControlledBy == LocationControlledBy.Torque
            ? ResultFromDataGate.Nom1
            : ResultFromDataGate.Nom2;

        public double LowerToleranceLimit =>
            LocationToolAssignment.TestParameters.ControlledBy == LocationControlledBy.Torque
                ? ResultFromDataGate.Min1
                : ResultFromDataGate.Min2;

        public double UpperToleranceLimit =>
            LocationToolAssignment.TestParameters.ControlledBy == LocationControlledBy.Torque
                ? ResultFromDataGate.Max1
                : ResultFromDataGate.Max2;

        public int SampleCount => ResultFromDataGate.Values.Count;
        public DateTime TestTimestamp => ResultFromDataGate.Values.Select(element => element.Timestamp).Min();
        public List<double> Values
        {
            get
            {
                return LocationToolAssignment.TestParameters.ControlledBy == LocationControlledBy.Torque
                    ? ResultFromDataGate.Values.Select(x => x.Value1).ToList()
                    : ResultFromDataGate.Values.Select(x => x.Value2).ToList();
            }
        }

        private double? _average;

        public double Average
        {
            get
            {
                if (_average is null)
                {
                    _average = Statistic.GetAverage(Values.ToArray());
                }

                return _average.Value;
            }
        }

        private double? _cm;

        public double Cm
        {
            get
            {
                if (_cm is null)
                {
                    _cm = Statistic.GetC(Values.ToArray(), UpperToleranceLimit, LowerToleranceLimit);
                }

                return _cm.Value;
            }
        }

        private double? _cmk;

        public double Cmk
        {
            get
            {
                if (_cmk is null)
                {
                    _cmk = Statistic.GetCk(Values.ToArray(), UpperToleranceLimit, LowerToleranceLimit);
                }

                return _cmk.Value;
            }
        }


        private double? _standardDeviation;

        public double? StandardDeviation
        {
            get
            {
                if (_standardDeviation is null)
                {
                    try
                    {
                        _standardDeviation = Statistic.GetStandardDeviation(Values.ToArray());
                    }
                    catch (ArgumentException)
                    {
                        _standardDeviation = null;
                    }
                }

                return _standardDeviation;
            }
        }

        public MeaUnit ControlUnit()
        {
            return LocationToolAssignment.TestParameters.ControlledBy == LocationControlledBy.Torque
                ? (MeaUnit) ResultFromDataGate.Unit1Id
                : (MeaUnit) ResultFromDataGate.Unit2Id;
        }

        public TestResult TestResult { get; set; }
        public DataGateResult ResultFromDataGate { get; set; }
        public LocationToolAssignment LocationToolAssignment { get; set; }
        public string LocationTreePath { get; set; }
    }

    public class TransmissionStatus
    {
        public TestEquipmentSerialNumber serialNumber;
        public bool transmissionFailed;
        public string message;
    }

    public class DataGateResultValue
    {
        public DateTime Timestamp;
        public double Value1;
        public double Value2;
    }

    public class DataGateResult
    {
        public long LocationToolAssignmentId;
        public long Unit1Id;
        public double Nom1;
        public double Min1;
        public double Max1;
        public long Unit2Id;
        public double Nom2;
        public double Min2;
        public double Max2;
        public List<DataGateResultValue> Values;
    }

    public class DataGateResults
    {
        public List<DataGateResult> Results;
    }

    public interface ITransferToTestEquipmentGui
    {
        void ShowNoTestEquipmentSelectedError();
        void ShowLocationToolAssignmentForTransferList(List<LocationToolAssignmentForTransfer> locationToolAssignments, TestType testType);
        void ShowCommunicationProgramNotFoundError();
        void AskToCancelLastTransfer(Action onCancelLastTransfer);
        void ShowMismatchingSerialNumber();
        void ShowTransmissionError(string message);
        void ShowReadResults(List<TestEquipmentTestResult> results);
        void ShowProcessControlForTransferList(List<ProcessControlForTransfer> processData);
        void ShowLoadProcessControlDataError();
        void ShowNoRouteSelectedError();
    }

    public interface ITransferToTestEquipmentDataAccess
    {
        List<LocationToolAssignmentForTransfer> LoadLocationToolAssignmentsForTransfer(TestType testType);
        List<TestEquipmentTestResult> FillWithLocationToolAssignmentsData(DataGateResults results);
        void SaveTestEquipmentTestResult(TestEquipment testEquipment, List<TestEquipmentTestResult> testEquipmentTestResults, (double cm, double cmk) cmCmk, User user);
        List<ProcessControlForTransfer> LoadProcessControlDataForTransfer();
    }

    public interface IDataGateDataAccess
    {
        void TransferToTestEquipment(
            SemanticModel dataGateSemanticModel, 
            TestEquipment testEquipment,
            Action<TransmissionStatus> withReceivedStatus);
        bool LastTransferFinished();
        void CancelTransfer();
        DataGateResults GetResults(TestEquipment testEquipment);
    }

    public class CommunicationProgramNotFoundException: Exception
    {
        public CommunicationProgramNotFoundException(Exception inner)
            : base("CommunicationProgramNotFound", inner)
        {
        }
    }

    public interface ICommunicationProgramController
    {
        void Start(TestEquipment testEquipment);
    }

    public interface ITransferToTestEquipmentUseCase
    {
        void ShowLocationToolAssignments(TestType testType);
        void SubmitToTestEquipment(
            TestEquipment testEquipment,
            List<LocationToolAssignmentForTransfer> locationToolAssignments,
			TestType testType);
        void SubmitToTestEquipment(
			TestEquipment testEquipment,
			List<ProcessControlForTransfer> processControlForTransfers);
        void ReadFromTestEquipment(TestEquipment testEquipment);
        void ShowProcessControlData();
    }

    public class TransferToTestEquipmentUseCase: ITransferToTestEquipmentUseCase
    {
        public TransferToTestEquipmentUseCase(
            ITransferToTestEquipmentDataAccess dataAccess,
            ILocationToolAssignmentData locationToolAssignmentData,
            IDataGateDataAccess dataGateDataAccess,
            ICmCmkDataAccess cmCmkDataAccess,
            ITimeDataAccess timeDataAccess,
            ITransferToTestEquipmentGui gui,
            ISemanticModelFactory semanticModelFactory,
            ISemanticModelRewriterBuilder dataGateRewriterBuilder,
            ICommunicationProgramController communicationProgramController,
            ISessionInformationUserGetter userGetter,
            ITestDateCalculationUseCase testDateCalculationUseCase,
            INotificationManager notificationManager,
            ILocationUseCase locationUseCase,
            ITreePathBuilder treePathBuilder,
            ILocationData locationData,
            IProcessControlData processControlData)
        {
            _dataAccess = dataAccess;
            _locationToolAssignmentData = locationToolAssignmentData;
            _dataGateDataAccess = dataGateDataAccess;
            _gui = gui;
            _semanticModelFactory = semanticModelFactory;
            _dataGateRewriterBuilder = dataGateRewriterBuilder;
            _cmCmkDataAccess = cmCmkDataAccess;
            _timeDataAccess = timeDataAccess;
            _communicationProgramController = communicationProgramController;
            _userGetter = userGetter;
            _testDateCalculationUseCase = testDateCalculationUseCase;
            _notificationManager = notificationManager;
            _locationUseCase = locationUseCase;
            _treePathBuilder = treePathBuilder;
            _locationData = locationData;
            _processControlData = processControlData;
        }

        public void ShowLocationToolAssignments(TestType testType)
        {
            var assigments2Transfer = _dataAccess.LoadLocationToolAssignmentsForTransfer(testType);
            _gui.ShowLocationToolAssignmentForTransferList(assigments2Transfer, testType);
        }

        public void ShowProcessControlData()
        {
            try
            {
                var processData = _dataAccess.LoadProcessControlDataForTransfer();
                _gui.ShowProcessControlForTransferList(processData);
            }
            catch (Exception)
            {
                _gui.ShowLoadProcessControlDataError();
            }
           
        }

        public void SubmitToTestEquipment(TestEquipment testEquipment, List<LocationToolAssignmentForTransfer> locationToolAssignments, TestType testType)
        {
            if (testEquipment == null)
            {
                _gui.ShowNoTestEquipmentSelectedError();
                return;
            }

            if (locationToolAssignments == null || locationToolAssignments.Count == 0)
            {
                _gui.ShowNoRouteSelectedError();
                return;
            }

            WithCommunicationProgram(
                testEquipment,
                () =>
                {
                    if (!_dataGateDataAccess.LastTransferFinished())
                    {
                        _gui.AskToCancelLastTransfer(() =>
                        {
                            _dataGateDataAccess.CancelTransfer();
                            ProgramTestEquipment(testEquipment, locationToolAssignments, testType);
                        });
                        return;
                    }

                    ProgramTestEquipment(testEquipment, locationToolAssignments, testType);
                });
        }

        public void SubmitToTestEquipment(TestEquipment testEquipment, List<ProcessControlForTransfer> processControlForTransfers)
        {
            if (testEquipment == null)
            {
                _gui.ShowNoTestEquipmentSelectedError();
                return;
            }

            if (processControlForTransfers == null || processControlForTransfers.Count == 0)
            {
                _gui.ShowNoRouteSelectedError();
                return;
            }

            WithCommunicationProgram(
                testEquipment,
                () =>
                {
                    if (!_dataGateDataAccess.LastTransferFinished()) // <-- move this into with communication program?
                    {
                        _gui.AskToCancelLastTransfer(() =>
                        {
                            _dataGateDataAccess.CancelTransfer();
                            ProgramTestEquipment(testEquipment, processControlForTransfers);
                        });
                        return;
                    }
                    ProgramTestEquipment(testEquipment, processControlForTransfers);
                });
        }

        private void ProgramTestEquipment(TestEquipment testEquipment, List<ProcessControlForTransfer> processControlForTransfers)
        {
            var locations = _locationData.LoadLocationsByIds(processControlForTransfers.Select(processControl => processControl.LocationId).ToList());
            var processControls = new List<ProcessControlCondition>();
            if (locations != null)
            {
                locations.ForEach(
                    location => processControls.Add(
                        _processControlData.LoadProcessControlConditionForLocation(location)));
            }
            var semanticModel =
                _semanticModelFactory.Convert(
                    testEquipment,
                    locations,
                    processControls,
                    _timeDataAccess.LocalNow());
            _dataGateRewriterBuilder.Build(testEquipment).Apply(ref semanticModel);
            _dataGateDataAccess.TransferToTestEquipment(semanticModel, testEquipment, status =>
            {
                if (IsTransmissionStatusIo(status, testEquipment))
                {
                    _notificationManager.SendSuccessNotification();
                }
            });
        }

        private void WithCommunicationProgram(TestEquipment testEquipment, Action action)
        {
            try
            {
                _communicationProgramController.Start(testEquipment);
                action();
            }
            catch (CommunicationProgramNotFoundException)
            {
                _gui.ShowCommunicationProgramNotFoundError();
            }
        }

        private void ProgramTestEquipment(TestEquipment testEquipment, List<LocationToolAssignmentForTransfer> locationToolAssignments, TestType testType)
        {
            var route =
                _locationToolAssignmentData.GetLocationToolAssignmentsByIds(
                    locationToolAssignments.Select(
                        (locationToolAssignment) => locationToolAssignment.LocationToolAssignmentId).ToList());

            _locationUseCase.LoadTreePathForLocations(route?.Select(x => x.AssignedLocation).ToList());

            var semanticModel = _semanticModelFactory.Convert(
                testEquipment, 
                route,
                _cmCmkDataAccess.LoadCmCmk(),
                _timeDataAccess.LocalNow(), testType);
            var rewriter = _dataGateRewriterBuilder.Build(testEquipment);
            rewriter.Apply(ref semanticModel);
            _dataGateDataAccess.TransferToTestEquipment(semanticModel, testEquipment, status =>
            {
                if (IsTransmissionStatusIo(status, testEquipment))
                {
                    _notificationManager.SendSuccessNotification();
                }
            });
        }

        public void ReadFromTestEquipment(TestEquipment testEquipment)
        {
            if (testEquipment == null)
            {
                _gui.ShowNoTestEquipmentSelectedError();
                return;
            }

            WithCommunicationProgram(testEquipment, () =>
            {
                if (!_dataGateDataAccess.LastTransferFinished())
                {
                    _gui.AskToCancelLastTransfer(() =>
                    {
                        _dataGateDataAccess.CancelTransfer(); 
                        ReadTestEquipment(testEquipment);
                    });
                    return;
                }
                ReadTestEquipment(testEquipment);
            });
        }

        private void SetTreePathForTestResults(List<TestEquipmentTestResult> testEquipmentTestResults)
        {
            if (testEquipmentTestResults == null)
            {
                return;
            }

            _locationUseCase.LoadTreePathForLocations(testEquipmentTestResults?.Select(x => x.LocationToolAssignment?.AssignedLocation).ToList());

            foreach (var result in testEquipmentTestResults)
            {
                result.LocationTreePath = _treePathBuilder.GetMaskedTreePathWithBase64(result.LocationToolAssignment?.AssignedLocation);
            }
        }

        private void ReadTestEquipment(TestEquipment testEquipment)
        {
            _dataGateDataAccess.TransferToTestEquipment(
                _semanticModelFactory.ReadCommand(
                    testEquipment,
                    _timeDataAccess.LocalNow()),
                testEquipment,
                transmissionStatus =>
                {
                    if (IsTransmissionStatusIo(transmissionStatus, testEquipment))
                    {
                        var testEquipmentTestResults =
                            _dataAccess.FillWithLocationToolAssignmentsData(_dataGateDataAccess.GetResults(testEquipment));

                        SetTreePathForTestResults(testEquipmentTestResults);

                        var cmCmk = _cmCmkDataAccess.LoadCmCmk();
                        testEquipmentTestResults?.ForEach(testEquipmentTestResult =>
                        {
                            testEquipmentTestResult.TestResult = CalculateTestResult(testEquipmentTestResult, cmCmk);
                        });

                        _dataAccess.SaveTestEquipmentTestResult(testEquipment, testEquipmentTestResults, cmCmk, _userGetter.GetCurrentUser());
                            
                        if (testEquipmentTestResults != null && FeatureToggles.FeatureToggles.TestDateCalculation)
                        {
                            var ids = testEquipmentTestResults.Select(x => x.LocationToolAssignment.Id).ToList();
                            _testDateCalculationUseCase.CalculateToolTestDateFor(ids); 
                        }

                        _notificationManager.SendSuccessNotification();
                        _gui.ShowReadResults(testEquipmentTestResults);
                        _dataGateDataAccess.TransferToTestEquipment(
                            _semanticModelFactory.ClearCommand(
                                testEquipment,
                                _timeDataAccess.LocalNow()),
                            testEquipment,
                            status => { });
                    }
                });
        }

        private TestResult CalculateTestResult(TestEquipmentTestResult testEquipmentTestResult, (double cm, double cmk) cmCmk)
        {
            return IsTestEquipmentTestResultFromChk(testEquipmentTestResult) 
                ? TestResultForChk(testEquipmentTestResult) : 
                TestResultForMfu(testEquipmentTestResult, cmCmk);
        }

        private bool IsTestEquipmentTestResultFromChk(TestEquipmentTestResult testEquipmentTestResult)
        {
            return testEquipmentTestResult.SampleCount <= 10;
        }

        private TestResult TestResultForMfu(TestEquipmentTestResult testEquipmentTestResult,
            (double cm, double cmk) cmCmk)
        {
            var values = testEquipmentTestResult.ResultFromDataGate.Values.Select(x => x.Value1)
                .ToArray();
            var lowerLimit = testEquipmentTestResult.LowerToleranceLimit;
            var upperLimit = testEquipmentTestResult.UpperToleranceLimit;
            var calculatedCm = Statistic.GetC(values, upperLimit, lowerLimit);
            var calculatedCmk = Statistic.GetCk(values, upperLimit, lowerLimit);
            if (calculatedCm >= cmCmk.cm && calculatedCmk >= cmCmk.cmk)
            {
                return new TestResult(0);
            }
            return new TestResult(1);
        }

        private TestResult TestResultForChk(TestEquipmentTestResult testEquipmentTestResult)
        {
            bool areValuesOutsideToleranceLimits = Statistic.AreValuesOutsideToleranceLimits(
                testEquipmentTestResult.ResultFromDataGate.Values.Select(x => x.Value1).ToArray(),
                testEquipmentTestResult.UpperToleranceLimit,
                testEquipmentTestResult.LowerToleranceLimit);
            return new TestResult(areValuesOutsideToleranceLimits ? 1 : 0);
        }

        private bool IsTransmissionStatusIo(TransmissionStatus transmissionStatus, TestEquipment testEquipment)
        {
            if (transmissionStatus.transmissionFailed)
            {
                _gui.ShowTransmissionError(transmissionStatus.message);
                return false;
            }

            if (!transmissionStatus.serialNumber.Equals(testEquipment.SerialNumber))
            {
                _gui.ShowMismatchingSerialNumber();
                return false;
            }

            return true;
        }

        private ITransferToTestEquipmentDataAccess _dataAccess;
        private ILocationToolAssignmentData _locationToolAssignmentData;
        private IDataGateDataAccess _dataGateDataAccess;
        private ICmCmkDataAccess _cmCmkDataAccess;
        private ITimeDataAccess _timeDataAccess;
        private ITransferToTestEquipmentGui _gui;
        private ISemanticModelFactory _semanticModelFactory;
        private ISemanticModelRewriterBuilder _dataGateRewriterBuilder;
        private ICommunicationProgramController _communicationProgramController;
        private ISessionInformationUserGetter _userGetter;
        private ITestDateCalculationUseCase _testDateCalculationUseCase;
        private INotificationManager _notificationManager;
        private readonly ILocationUseCase _locationUseCase;
        private readonly ITreePathBuilder _treePathBuilder;
        private readonly ILocationData _locationData;
        private readonly IProcessControlData _processControlData;
    }
}
