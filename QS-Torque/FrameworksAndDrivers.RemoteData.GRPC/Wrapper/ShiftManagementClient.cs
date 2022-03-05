using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Grpc.Core;
using ShiftManagementService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class ShiftManagementClient : IShiftManagementClient
    {
        private readonly ShiftManagements.ShiftManagementsClient _shiftManagementClient;

        public ShiftManagementClient(ShiftManagements.ShiftManagementsClient shiftManagementClient)
        {
            _shiftManagementClient = shiftManagementClient;
        }

        public ShiftManagement GetShiftManagement()
        {
            return _shiftManagementClient.GetShiftManagement(new NoParams(), new CallOptions());
        }

        public void SaveShiftManagement(ShiftManagementDiff diff)
        {
            _shiftManagementClient.SaveShiftManagement(diff);
        }
    }
}
