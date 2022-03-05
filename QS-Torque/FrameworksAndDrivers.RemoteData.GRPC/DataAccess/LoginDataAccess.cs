using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security;
using System.Threading.Tasks;
using AuthenticationService;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Authentication;
using State;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface ILoginClient
    {
        Task Ping();
        Task<AuthenticationResponse> Login(AuthenticationRequest request);
        Task<ListOfGroups> GetQstGroupByUserName(QstGroupByUserNameRequest request);
    }

    public class LoginDataAccess : ILoginData
    {
        private readonly IClientFactory _clientFactory;
        private readonly IChannelCreator _channelCreator;
        private ServerConnection _serverConnection;

        public LoginDataAccess(IClientFactory clientFactory, IChannelCreator channelCreator)
        {
            _clientFactory = clientFactory;
            _channelCreator = channelCreator;
        }

        private ILoginClient GetClient()
        {
            return _clientFactory.AnonymousChannel.GetLoginClient();
        }

        public async Task<List<Group>> LoadGroupsForUserNameAsync(string username)
        {
            var listOfGroupsResponse = await GetClient().GetQstGroupByUserName(new QstGroupByUserNameRequest()
            {
                Username = username
            });

            var listOfGroups = listOfGroupsResponse.Groups;
            var groups = new List<Group>();
            foreach (var group in listOfGroups)
            {
                groups.Add(new Group(){Id = new GroupId(group.GroupId),GroupName = group.GroupName});
            }

            return groups;
        }

        public async Task SetupServerConnectionAsync(ServerConnection serverConnection)
        {
            _serverConnection = serverConnection;
            _clientFactory.AnonymousChannel = _channelCreator.CreateChannel(serverConnection);
            await GetClient().Ping();
            SessionInformation.ServerName = serverConnection.ServerName;
        }

        public async Task LoginAsync(string userName, SecureString password, Group group)
        {
            var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            var localfqdn = string.Format("{0}.{1}", ipProperties.HostName, ipProperties.DomainName);
            if (localfqdn.EndsWith("."))
            {
                localfqdn = localfqdn.Remove(localfqdn.Length - 1, 1);
            }
            var authenticationResponse = await GetClient().Login(new AuthenticationRequest()
            {
                Username = userName,
                Password = new System.Net.NetworkCredential(string.Empty, password).Password,
                GroupId = group?.Id.ToLong() ?? 0,
                Pcfqdn = localfqdn
            });

            var token = authenticationResponse.Token.Base64;
            if (token == "")
            {
                throw new LoginFailedException(null, "Login Failed");
            }
            var authInformation = new AuthenticationInformation(token);
            SessionInformation.CurrentUser = new User
            {
                UserId = new UserId(authInformation.UserId),
                UserName = authInformation.UserName,
                Group = new Group()
                {
                    Id = new GroupId(authInformation.GroupId),
                    GroupName = authInformation.GroupName
                }
            };
            _clientFactory.AuthenticationChannel = _channelCreator.CreateChannel(_serverConnection, token);
        }
    }
}
