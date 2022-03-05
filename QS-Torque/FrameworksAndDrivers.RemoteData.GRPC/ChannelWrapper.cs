using System;
using ExtensionService;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using FrameworksAndDrivers.RemoteData.GRPC.Wrapper;
using Grpc.Net.Client;
using LocationToolAssignmentService;
using ManufacturerService;
using ShiftManagementService;
using TestDateCalculationService;
using TestLevelSetAssignmentService;
using TestLevelSetService;
using ToleranceClassService;
using ToolModelService;
using WorkingCalendarService;

namespace FrameworksAndDrivers.RemoteData.GRPC
{
    public interface IChannelWrapper: IDisposable
    {
        GrpcChannel GrpcChannel { get; set; }
        ILoginClient GetLoginClient();
        IManufacturerClient GetManufacturerClient();
        IStatusClient GetStatusClient();
        IToleranceClassClient GetToleranceClassClient();
        IWorkingCalendarClient GetWorkingCalendarClient();
        IShiftManagementClient GetShiftManagementClient();
        ITestLevelSetClient GetTestLevelSetClient();
        ITestLevelSetAssignmentClient GetTestLevelSetAssignmentClient();
        ITestDateCalculationClient GetTestDateCalculationClient();
        ILocationToolAssignmentClient GetLocationToolAssignmentClient();
        ISetupClient GetSetupClient();
        IHelperTableClient GetHelperTableClient();
        IToolUsageClient GetToolUsageClient();
        ILocationClient GetLocationClient();
        IToolClient GetToolClient();
        IToolModelClient GetToolModelClient();
        IClassicTestClient GetClassicTestClient();
        ITransferToTestEquipmentClient GetTransferToTestEquipmentClient();
        IQstInformationClient GetQstInformationClient();
        ITestEquipmentClient GetTestEquipmentClient();
        IProcessControlClient GetProcessControlClient();
        IExtensionClient GetExtensionClient();
        IHistoryClient GetHistoryClient();
    }

    public class ChannelWrapper : IChannelWrapper
    {
        public GrpcChannel GrpcChannel { get; set; }

        public ILoginClient GetLoginClient()
        {
            return new LoginClient(new AuthenticationService.Authentication.AuthenticationClient(GrpcChannel));
        }

        public IManufacturerClient GetManufacturerClient()
        {
            return new ManufacturerClient(new Manufacturers.ManufacturersClient(GrpcChannel));
        }

        public IStatusClient GetStatusClient()
        {
            return new StatusClient(new StatusService.StatusService.StatusServiceClient(GrpcChannel));
        }

        public IToleranceClassClient GetToleranceClassClient()
        {
            return new ToleranceClassClient(new ToleranceClasses.ToleranceClassesClient(GrpcChannel));
        }

        public IWorkingCalendarClient GetWorkingCalendarClient()
        {
            return new WorkingCalendarClient(new WorkingCalendars.WorkingCalendarsClient(GrpcChannel));
        }

        public IShiftManagementClient GetShiftManagementClient()
        {
            return new ShiftManagementClient(new ShiftManagements.ShiftManagementsClient(GrpcChannel));
        }

        public ITestLevelSetClient GetTestLevelSetClient()
        {
            return new TestLevelSetClient(new TestLevelSets.TestLevelSetsClient(GrpcChannel));
        }

        public ITestLevelSetAssignmentClient GetTestLevelSetAssignmentClient()
        {
            return new TestLevelSetAssignmentClient(new TestLevelSetAssignments.TestLevelSetAssignmentsClient(GrpcChannel));
        }

        public ISetupClient GetSetupClient()
        {
            return new SetupClient(new SetupService.SetupService.SetupServiceClient(GrpcChannel));
        }

        public IHelperTableClient GetHelperTableClient()
        {
            return new HelperTableClient(new HelperTableService.HelperTableService.HelperTableServiceClient(GrpcChannel));
        }

        public IToolUsageClient GetToolUsageClient()
        {
            return new ToolUsageClient(new ToolUsageService.ToolUsageService.ToolUsageServiceClient(GrpcChannel));
        }

        public ILocationClient GetLocationClient()
        {
            return new LocationClient(new LocationService.LocationService.LocationServiceClient(GrpcChannel));
        }

        public IToolClient GetToolClient()
        {
            return new ToolClient(new ToolService.ToolService.ToolServiceClient(GrpcChannel));
        }

        public IToolModelClient GetToolModelClient()
        {
            return new ToolModelClient(new ToolModels.ToolModelsClient(GrpcChannel));
        }

        public IClassicTestClient GetClassicTestClient()
        {
            return new ClassicTestClient(new ClassicTestService.ClassicTestService.ClassicTestServiceClient(GrpcChannel));
        }

        public ITransferToTestEquipmentClient GetTransferToTestEquipmentClient()
        {
            return new TransferToTestEquipmentClient(new TransferToTestEquipmentService.TransferToTestEquipmentService.TransferToTestEquipmentServiceClient(GrpcChannel));
        }

        public IQstInformationClient GetQstInformationClient()
        {
            return new QstInformationClient(new QstInformationService.QstInformationService.QstInformationServiceClient(GrpcChannel));
        }

        public ITestEquipmentClient GetTestEquipmentClient()
        {
            return new TestEquipmentClient(new TestEquipmentService.TestEquipmentService.TestEquipmentServiceClient(GrpcChannel));
        }

        public IProcessControlClient GetProcessControlClient()
        {
            return new ProcessControlClient(new ProcessControlService.ProcessControlService.ProcessControlServiceClient(GrpcChannel));
        }

        public ITestDateCalculationClient GetTestDateCalculationClient()
        {
            return new TestDateCalculationClient(new TestDateCalculations.TestDateCalculationsClient(GrpcChannel));
        }

        public ILocationToolAssignmentClient GetLocationToolAssignmentClient()
        {
            return new LocationToolAssignmentClient(new LocationToolAssignments.LocationToolAssignmentsClient(GrpcChannel));
        }

        public IExtensionClient GetExtensionClient()
        {
            return new ExtensionClient(new Extensions.ExtensionsClient(GrpcChannel));
        }

        public IHistoryClient GetHistoryClient()
        {
            return new HistoryClient(new HistoryService.Histories.HistoriesClient(GrpcChannel));
        }

        public void Dispose()
        {
            GrpcChannel?.Dispose();
        }
    }
}
