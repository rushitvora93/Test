using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.RemoteData.GRPC;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.Extensions.Configuration;
using State;

namespace ServerIntegrationTests
{
    public class Configuration
    {
        public string CaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ushort ServerPort { get; set; }
        public string ServerName { get; set; }
    }

    public class TestSetup
    {
        public IClientFactory ClientFactory { get; set; }
        public User TestUser { get; set; }

        public TestSetup()
        {
            var configurationLoader = new ConfigurationLoader();
            ClientFactory = SetupServerConnection(configurationLoader.Configurations.First());
        }

        private ClientFactory SetupServerConnection(Configuration configuration)
        {
            var channelCreator = new ChannelCreator();
            var serverConnection = new ServerConnection
            {
                HostName = configuration.ServerName,
                ServerName = configuration.CaseName,
                Port = configuration.ServerPort
            };
            var clientFactory = new ClientFactory();
            var loginDataAccess = new LoginDataAccess(clientFactory, channelCreator);
            loginDataAccess.SetupServerConnectionAsync(serverConnection).Wait();

            var groups = loginDataAccess.LoadGroupsForUserNameAsync(configuration.UserName);
            groups.Wait();
            Group group = null;
            if (groups.Result.Count > 0)
            {
                group = groups.Result.First();
            }
            var login = loginDataAccess.LoginAsync(configuration.UserName, new NetworkCredential("", configuration.Password).SecurePassword, group);
            login.Wait();
            TestUser = SessionInformation.CurrentUser;

            return clientFactory;
        }
    }

    public class ConfigurationLoader
    {
        public ConfigurationLoader()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testcasesconfig.json", optional: false, reloadOnChange: false)
                .Build();
            foreach (var testcase in configuration.GetChildren())
            {

                var configitem = new Configuration(){
                    CaseName = testcase.Key,
                    UserName = (string)testcase.GetValue(typeof(string), "username"),
                    Password = (string)testcase.GetValue(typeof(string), "password"),
                    ServerPort = (ushort)testcase.GetValue(typeof(ushort), "serverport"),
                    ServerName = (string)testcase.GetValue(typeof(string), "servername")
                };
                if (configitem.UserName.ToUpper() == "CSP")
                {
                    configitem.Password = DateTime.Now.Month.ToString() + "TSQ";
                }

                Configurations.Add(configitem);
            }
        }

        public List<Configuration> Configurations { get; set; } = new List<Configuration>();


    }
}
