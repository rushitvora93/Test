using FrameworksAndDrivers.RemoteData.GRPC;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Grpc.Net.Client;

namespace Client.TestHelper.Mock
{
    public class ChannelWrapperMock : IChannelWrapper
    {
        public GrpcChannel GrpcChannel { get; set; }

        public ILoginClient GetLoginClient()
        {
            return GetLoginClientReturnValue;
        }

        public IManufacturerClient GetManufacturerClient()
        {
            return GetManufacturerClientReturnValue;
        }

        public IStatusClient GetStatusClient()
        {
            return GetStatusClientReturnValue;
        }

        public IToleranceClassClient GetToleranceClassClient()
        {
            return GetToleranceClassClientReturnValue;
        }

        public IWorkingCalendarClient GetWorkingCalendarClient()
        {
            return GetWorkingCalendarClientReturnValue;
        }

        public IShiftManagementClient GetShiftManagementClient()
        {
            return GetShiftManagementClientReturnValue;
        }

        public ITestLevelSetClient GetTestLevelSetClient()
        {
            return GetTestLevelSetClientReturnValue;
        }

        public ITestLevelSetAssignmentClient GetTestLevelSetAssignmentClient()
        {
            return GetTestLevelSetAssignmentClientReturnValue;
        }

        public ISetupClient GetSetupClient()
        {
            return GetSetupClientReturnValue;
        }

        public IHelperTableClient GetHelperTableClient()
        {
            return GetHelperTableClientReturnValue;
        }

        public IToolUsageClient GetToolUsageClient()
        {
            return GetToolUsageClientReturnValue;
        }

        public ILocationClient GetLocationClient()
        {
            return GetLocationClientReturnValue;
        }

        public IToolClient GetToolClient()
        {
            return GetToolClientReturnValue;
        }

        public IToolModelClient GetToolModelClient()
        {
            return GetToolModelClientReturnValue;
        }

        public IClassicTestClient GetClassicTestClient()
        {
            return GetClassicTestClientReturnValue;
        }

        public ITransferToTestEquipmentClient GetTransferToTestEquipmentClient()
        {
            return GetTransferToTestEquipmentClientReturnValue;
        }

        public IQstInformationClient GetQstInformationClient()
        {
            return GetQstInformationClientReturnValue;
        }

        public ITestEquipmentClient GetTestEquipmentClient()
        {
            return GetTestEquipmentClientReturnValue;
        }

        public IProcessControlClient GetProcessControlClient()
        {
            return GetProcessControlClientReturnValue;
        }

        public ITestDateCalculationClient GetTestDateCalculationClient()
        {
            return GetTestDateCalculationClientReturnValue;
        }

        public ILocationToolAssignmentClient GetLocationToolAssignmentClient()
        {
            return GetLocationToolAssignmentClientReturnValue;
        }

        public void Dispose()
        {
            DisposeWasCalled = true;
        }

        public IExtensionClient GetExtensionClient()
        {
            return GetExtensionClientReturnValue;
        }

        public IHistoryClient GetHistoryClient()
        {
            return GetHistoryClientReturnValue;
        }

        public IManufacturerClient GetManufacturerClientReturnValue { get; set; }
        public IStatusClient GetStatusClientReturnValue { get; set; }
        public ILoginClient GetLoginClientReturnValue { get; set; }
        public IWorkingCalendarClient GetWorkingCalendarClientReturnValue { get; set; }
        public IShiftManagementClient GetShiftManagementClientReturnValue { get; set; }
        public ITestLevelSetClient GetTestLevelSetClientReturnValue { get; set; }
        public IToleranceClassClient GetToleranceClassClientReturnValue { get; set; }
        public ITestLevelSetAssignmentClient GetTestLevelSetAssignmentClientReturnValue { get; set; }
        public ISetupClient GetSetupClientReturnValue { get; set; }
        public ITestDateCalculationClient GetTestDateCalculationClientReturnValue { get; set; }
        public ILocationToolAssignmentClient GetLocationToolAssignmentClientReturnValue { get; set; }
        public IHelperTableClient GetHelperTableClientReturnValue { get; set; }
        public IToolUsageClient GetToolUsageClientReturnValue { get; set; }
        public ILocationClient GetLocationClientReturnValue { get; set; }
        public IToolClient GetToolClientReturnValue { get; set; }
        public IClassicTestClient GetClassicTestClientReturnValue { get; set; }
        public ITransferToTestEquipmentClient GetTransferToTestEquipmentClientReturnValue { get; set; }
        public IQstInformationClient GetQstInformationClientReturnValue { get; set; }
        public ITestEquipmentClient GetTestEquipmentClientReturnValue { get; set; }
        public IToolModelClient GetToolModelClientReturnValue { get; set; }
        public IProcessControlClient GetProcessControlClientReturnValue { get; set; }
        public IExtensionClient GetExtensionClientReturnValue { get; set; }
        public IHistoryClient GetHistoryClientReturnValue { get; set; }
        public bool DisposeWasCalled = false;
    }
}
