using Client.Core.Diffs;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface IShiftManagementClient
    {
        DtoTypes.ShiftManagement GetShiftManagement();
        void SaveShiftManagement(DtoTypes.ShiftManagementDiff diff);
    }


    public class ShiftManagementDataAccess : IShiftManagementData
    {
        private readonly IClientFactory _clientFactory;
        private readonly Mapper _mapper = new Mapper();

        public ShiftManagementDataAccess(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private IShiftManagementClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetShiftManagementClient();
        }

        public ShiftManagement LoadShiftManagement()
        {
            return _mapper.DirectPropertyMapping(GetClient().GetShiftManagement());
        }

        public void SaveShiftManagement(ShiftManagementDiff diff)
        {
            GetClient().SaveShiftManagement(_mapper.DirectPropertyMapping(diff));
        }
    }
}
