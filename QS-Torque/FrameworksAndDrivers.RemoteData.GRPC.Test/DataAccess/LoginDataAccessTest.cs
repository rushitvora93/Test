using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService;
using Client.TestHelper.Mock;
using Common.UseCases.Interfaces;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Authentication;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using State;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    public class LoginDataAccessTest
    {
        public class LoginClientMock : ILoginClient
        {
            public Task Ping()
            {
                PingCalled = true;
                return Task.CompletedTask;
            }

            public async Task<AuthenticationResponse> Login(AuthenticationRequest request)
            {
                await Task.CompletedTask;
                LoginRequestParameter = request;
                return LoginReturnValue;
            }

            public async Task<ListOfGroups> GetQstGroupByUserName(QstGroupByUserNameRequest request)
            {
                await Task.CompletedTask;
                GetQstGroupByUserNameRequestParameter = request;
                return GetQstGroupByUserNameReturnValue;
            }

            public ListOfGroups GetQstGroupByUserNameReturnValue { get; set; } = new ListOfGroups();
            public QstGroupByUserNameRequest GetQstGroupByUserNameRequestParameter { get; set; }
            public AuthenticationResponse LoginReturnValue { get; set; }
            public AuthenticationRequest LoginRequestParameter { get; set; }
            public bool PingCalled { get; set; }
        }

        [Test]
        public void SetupServerConnectionAsyncCreatesChannel()
        {
            var environment = new Environment();
            
            environment.mocks.channelCreator.CreateChannelReturnValue = environment.mocks.channelWrapper;

            environment.dataAccess.SetupServerConnectionAsync(new ServerConnection()).Wait();

            Assert.AreSame(environment.mocks.channelWrapper, environment.mocks.clientFactory.AnonymousChannel);
        }

        [Test]
        public void SetupServerConnectionAsyncCallsCreateChannelFromServerconnections()
        {
            var environment = new Environment();
            environment.mocks.channelCreator.CreateChannelReturnValue = environment.mocks.channelWrapper;

            var serverConnection = new ServerConnection();

            environment.dataAccess.SetupServerConnectionAsync(serverConnection).Wait();

            Assert.AreSame(serverConnection, environment.mocks.channelCreator.CreateChannelServerConnectionParameter);
        }

        [Test]
        public void SetupServerConnectionAsyncCallsPing()
        {
            var environment = new Environment();

            environment.mocks.channelCreator.CreateChannelReturnValue = environment.mocks.channelWrapper;

            environment.dataAccess.SetupServerConnectionAsync(new ServerConnection()).Wait();

            Assert.IsTrue(environment.mocks.loginClient.PingCalled);
        }

        [TestCase("localhost")]
        [TestCase("praktifix")]
        public void SetupServerConnectionAsyncSetSessionInformation(string serverName)
        {
            var environment = new Environment();

            environment.mocks.channelCreator.CreateChannelReturnValue = environment.mocks.channelWrapper;

            environment.dataAccess.SetupServerConnectionAsync(new ServerConnection() {ServerName = serverName}).Wait();

            Assert.AreEqual(serverName, SessionInformation.ServerName);
        }

        [TestCase("blub", "43543646", 23)]
        [TestCase("test", "abcdefg", 99)]

        public void LoginAsyncCallsLogin(string userName, string password, long groupId)
        {
            var environment = new Environment();

            var tokenGenerator = new JWTBearerGenerator(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("43785vtrzitrzi5687509789506ß7896786")));

            var token = tokenGenerator.GenerateToken(GetAuthenticationUserFromUser(CreateUser.Anonymous()),"");

            environment.mocks.loginClient.LoginReturnValue = new AuthenticationResponse()
            {
                Token = new Token()
                {
                    Base64 = token
                }
            };

            environment.mocks.channelCreator.CreateChannelReturnValue = environment.mocks.channelWrapper;
            environment.dataAccess.SetupServerConnectionAsync(new ServerConnection()).Wait();

            var pass = new SecureString();
            foreach (var character in password)
            {
                pass.AppendChar(character);
            }

            environment.dataAccess.LoginAsync(userName, pass, new Group(){Id = new GroupId(groupId)}).Wait();

            Assert.AreEqual(userName,environment.mocks.loginClient.LoginRequestParameter.Username);
            Assert.AreEqual(groupId, environment.mocks.loginClient.LoginRequestParameter.GroupId);
            Assert.AreEqual(password, environment.mocks.loginClient.LoginRequestParameter.Password);
            var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            var localfqdn = string.Format("{0}.{1}", ipProperties.HostName, ipProperties.DomainName);
            if (localfqdn.EndsWith("."))
            {
                localfqdn = localfqdn.Remove(localfqdn.Length - 1, 1);
            }
            Assert.AreEqual(localfqdn, environment.mocks.loginClient.LoginRequestParameter.Pcfqdn);
        }

        [TestCase(10,"CSP",99, "Admin")]
        [TestCase(99, "Test123", 99, "Global")]
        public void LoginSetsSessionInformation(long userId, string userName, long groupId, string groupName)
        {
            var tokenGenerator = new JWTBearerGenerator(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("43785vtrzitrzi5687509789506ß7896786")));
            var user = CreateUser.Parametrized(userId, userName, CreateGroup.Parametrized(groupId, groupName));
            var token = tokenGenerator.GenerateToken(GetAuthenticationUserFromUser(user), "");

            var environment = new Environment();
            
            environment.mocks.loginClient.LoginReturnValue = new AuthenticationResponse(){Token = new Token()
            {
                Base64 = token
            }};

            environment.mocks.channelCreator.CreateChannelReturnValue = environment.mocks.channelWrapper;
            environment.dataAccess.SetupServerConnectionAsync(new ServerConnection()).Wait();

            environment.dataAccess.LoginAsync(user.UserName, new SecureString(), user.Group).Wait();

            Assert.IsTrue(
                (SessionInformation.CurrentUser.UserId.ToLong(), SessionInformation.CurrentUser.UserName,
                    SessionInformation.CurrentUser.Group.Id.ToLong(), SessionInformation.CurrentUser.Group.GroupName)
                .Equals((user.UserId.ToLong(), user.UserName, user.Group.Id.ToLong(), user.Group.GroupName)));
        }

        [Test]
        public void LoginAsyncCreatesChannel()
        {
            var environment = new Environment();

            environment.mocks.channelCreator.CreateChannelReturnValue = environment.mocks.channelWrapper;

            var tokenGenerator = new JWTBearerGenerator(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("43785vtrzitrzi5687509789506ß7896786")));
            
            var token = tokenGenerator.GenerateToken(GetAuthenticationUserFromUser(CreateUser.Anonymous()),"");

            environment.mocks.loginClient.LoginReturnValue = new AuthenticationResponse()
            {
                Token = new Token()
                {
                    Base64 = token
                }
            };

            environment.mocks.channelCreator.CreateChannelReturnValue = environment.mocks.channelWrapper;
            environment.dataAccess.SetupServerConnectionAsync(new ServerConnection()).Wait();

            environment.dataAccess.LoginAsync("", new SecureString(), CreateGroup.Anonymous()).Wait();

            Assert.AreSame(environment.mocks.channelWrapper, environment.mocks.clientFactory.AuthenticationChannel);
        }

        [TestCase("localhost", 1)]
        [TestCase("praktifix", 99)]
        public void LoginAsyncCallsGenerateChannel(string servername, long userId )
        {
            var environment = new Environment();

            environment.mocks.channelCreator.CreateChannelReturnValue = environment.mocks.channelWrapper;

            var tokenGenerator = new JWTBearerGenerator(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("43785vtrzitrzi5687509789506ß7896786")));

            var token = tokenGenerator.GenerateToken(GetAuthenticationUserFromUser(CreateUser.IdOnly(userId)),"");

            environment.mocks.loginClient.LoginReturnValue = new AuthenticationResponse()
            {
                Token = new Token()
                {
                    Base64 = token
                }
            };

            var serverconnection = new ServerConnection()
            {
                ServerName = servername
            };

            environment.mocks.channelCreator.CreateChannelReturnValue = environment.mocks.channelWrapper;
            environment.dataAccess.SetupServerConnectionAsync(serverconnection).Wait();

            environment.dataAccess.LoginAsync("", new SecureString(), CreateGroup.Anonymous()).Wait();

            Assert.AreEqual(token, environment.mocks.channelCreator.CreateChannelTokenParameter);
            Assert.AreEqual(serverconnection.ServerName, environment.mocks.channelCreator.CreateChannelServerConnectionParameter.ServerName);
        }

        [Test]
        public void LoggingInWithEmptyTokenInResponseFailsLogin()
        {
            var environment = new Environment();
            environment.mocks.loginClient.LoginReturnValue = new AuthenticationResponse{Token = new Token{Base64 = ""} };
            environment.mocks.channelCreator.CreateChannelReturnValue = environment.mocks.channelWrapper;
            environment.dataAccess.SetupServerConnectionAsync(new ServerConnection()).Wait();
            Assert.CatchAsync<LoginFailedException>(
                async () =>
                {
                    await environment.dataAccess.LoginAsync("",new SecureString(), CreateGroup.Anonymous());
                });
        }

        [TestCase("Frank")]
        [TestCase("Hans")]
        public void LoadGroupsForUserNameAsyncCallsClientCorrect(string userName)
        {
            var environment = new Environment();
            environment.mocks.channelCreator.CreateChannelReturnValue = environment.mocks.channelWrapper;
            environment.dataAccess.SetupServerConnectionAsync(new ServerConnection()).Wait();

            environment.dataAccess.LoadGroupsForUserNameAsync(userName).Wait();

            Assert.AreEqual(userName, environment.mocks.loginClient.GetQstGroupByUserNameRequestParameter.Username);
        }

        static IEnumerable<ListOfGroups> LoadGroupsForUserNameAsyncData = new List<ListOfGroups>()
        {
            new ListOfGroups()
            {
                Groups =
                {
                    new DtoTypes.Group(){GroupId = 1, GroupName= "Admin9837"},
                    new DtoTypes.Group(){GroupId = 55, GroupName= "BLUB 984325"},
                }
            },
            new ListOfGroups()
            {
                Groups =
                {
                    new DtoTypes.Group(){GroupId = 82, GroupName= "gfdzh 568zu756"},
                }
            }
        };

        [TestCaseSource(nameof(LoadGroupsForUserNameAsyncData))]
        public void LoadGroupsForUserNameAsyncReturnsCorrectValue(ListOfGroups listOfGroups)
        {
            var environment = new Environment();
            environment.mocks.channelCreator.CreateChannelReturnValue = environment.mocks.channelWrapper;

            environment.mocks.loginClient.GetQstGroupByUserNameReturnValue = listOfGroups;

            environment.dataAccess.SetupServerConnectionAsync(new ServerConnection()).Wait();

            var result = environment.dataAccess.LoadGroupsForUserNameAsync("");
            result.Wait();

            var comparer = new Func<DtoTypes.Group, Group,bool>((dtoGroup, group) => dtoGroup.GroupId == group.Id.ToLong() && dtoGroup.GroupName == group.GroupName);
            CheckerFunctions.CollectionAssertAreEquivalent(listOfGroups.Groups, result.Result, comparer);
        }


        private static AuthenticationUser GetAuthenticationUserFromUser(User user)
        {
            var authenticationUser = new AuthenticationUser()
            {
                UserId = user.UserId.ToLong(),
                UserName = user.UserName,
                GroupId = user.Group.Id.ToLong(),
                GroupName = user.Group.GroupName
            };
            return authenticationUser;
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    clientFactory = new ClientFactoryMock();
                    channelCreator = new ChannelCreatorMock();
                    channelWrapper = new ChannelWrapperMock();
                    loginClient = new LoginClientMock();
                    channelWrapper.GetLoginClientReturnValue = loginClient;
                }
                public ClientFactoryMock clientFactory;
                public ChannelCreatorMock channelCreator;
                public ChannelWrapperMock channelWrapper;
                public LoginClientMock loginClient;
            }

            public Environment()
            {
                mocks = new Mocks();
                dataAccess = new LoginDataAccess(mocks.clientFactory, mocks.channelCreator);
            }

            public readonly Mocks mocks;
            public readonly LoginDataAccess dataAccess;
        }
    }
}
