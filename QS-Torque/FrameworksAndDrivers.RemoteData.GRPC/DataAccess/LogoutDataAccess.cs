using Core.UseCases;
using State;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public class LogoutDataAccess: ILogoutData
    {
        public LogoutDataAccess(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public void Logout()
        {
            _clientFactory.AuthenticationChannel.Dispose();
            _clientFactory.AuthenticationChannel = null;
            _clientFactory.AnonymousChannel.Dispose();
            _clientFactory.AnonymousChannel = null;
            SessionInformation.CurrentUser = null;
        }

        private readonly IClientFactory _clientFactory;
    }
}
