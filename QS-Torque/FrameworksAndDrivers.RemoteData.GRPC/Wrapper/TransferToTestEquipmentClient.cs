using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using TransferToTestEquipmentService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class TransferToTestEquipmentClient : ITransferToTestEquipmentClient
    {
        private readonly TransferToTestEquipmentService.TransferToTestEquipmentService.TransferToTestEquipmentServiceClient _client;

        public TransferToTestEquipmentClient(TransferToTestEquipmentService.TransferToTestEquipmentService.TransferToTestEquipmentServiceClient client)
        {
            _client = client;
        }

        public ListOfLocationToolAssignmentForTransfer LoadLocationToolAssignmentsForTransfer(Long testType)
        {
            return _client.LoadLocationToolAssignmentsForTransfer(testType);
        }

        public ListOfProcessControlDataForTransfer LoadProcessControlDataForTransfer()
        {
            return _client.LoadProcessControlDataForTransfer(new NoParams());
        }

        public void InsertClassicChkTests(ListOfClassicChkTestWithLocalTimestamp tests)
        {
            _client.InsertClassicChkTests(tests);
        }

        public void InsertClassicMfuTests(ListOfClassicMfuTestWithLocalTimestamp tests)
        {
            _client.InsertClassicMfuTests(tests);
        }
    }
}
