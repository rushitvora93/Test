using System.Threading.Tasks;
using BasicTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Grpc.Core;
using AuthenticationService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class LoginClient : ILoginClient
    {
        private readonly AuthenticationService.Authentication.AuthenticationClient _authenticationClient;

        public LoginClient(AuthenticationService.Authentication.AuthenticationClient authenticationClient)
        {
            _authenticationClient = authenticationClient;
        }

        public Task Ping()
        {
            _authenticationClient.PingAsync(new NoParams());
            return Task.CompletedTask;
        }

        public Task<AuthenticationResponse> Login(AuthenticationRequest request)
        {
            return _authenticationClient.LoginAsync(request, new CallOptions()).ResponseAsync;
        }

        public Task<ListOfGroups> GetQstGroupByUserName(QstGroupByUserNameRequest request)
        {
            return _authenticationClient.GetQstGroupByUserNameAsync(request, new CallOptions()).ResponseAsync;
        }
    }
}
