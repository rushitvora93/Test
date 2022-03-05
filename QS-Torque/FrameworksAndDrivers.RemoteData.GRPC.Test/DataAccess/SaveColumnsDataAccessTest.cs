using System.Collections.Generic;
using System.Linq;
using Client.TestHelper.Mock;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using State;
using TestHelper.Factories;
using TestHelper.Mock;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    public class SaveColumnsDataAccessTest
    {
        [Test]
        public void SaveColumnWidthsWithNoUserDontCallsClient()
        {
            var environment = new Environment();
            SessionInformation.CurrentUser = null;
            environment.dataAccess.SaveColumnWidths("", new List<(string, double)>());
            Assert.IsFalse(environment.mocks.setupClient.InsertOrUpdateQstSetupsCalled);
        }

        private static IEnumerable<(string, List<(string, double)>, List<string>, List<string>, long, List<long>)>
            SaveColumnWidthsCallsClientData =
                new List<(string, List<(string, double)>, List<string>, List<string>, long, List<long>)>()
                {
                    (
                        "gridA",
                        new List<(string, double)>()
                        {
                            ("col1", 12),
                            ("col2", 45)
                        },
                        new List<string>()
                        {
                            $"gridAcol1",
                            $"gridAcol2"
                        },
                        new List<string>()
                        {
                            "12",
                            "45"
                        },
                        9,
                        new List<long>()
                        {
                             9,
                             9
                        }
                    ),
                    (
                        "test1",
                        new List<(string, double)>()
                        {
                            ("colt", 99),
                        },
                        new List<string>()
                        {
                            "test1colt",
                        },
                        new List<string>()
                        {
                            "99",
                        },
                        19,
                        new List<long>()
                        {
                            19
                        }
                    )
                };

        [TestCaseSource(nameof(SaveColumnWidthsCallsClientData))]
        public void SaveColumnWidthsCallsClient((string gridName, List<(string, double)> columns, List<string> names, List<string> values, long user, List<long> users) data)
        {
            var environment = new Environment();

            SessionInformation.CurrentUser = CreateUser.IdOnly(data.user);

            environment.dataAccess.SaveColumnWidths(data.gridName, data.columns);

            var names = new List<string>();
            var values = new List<string>();
            var users = new List<long>();

            foreach (var setupList in environment.mocks.setupClient.InsertOrUpdateQstSetupsParameter.SetupList)
            {
                names.Add(setupList.Name);
                values.Add(setupList.Value);
                users.Add(setupList.UserId);
            }

            Assert.AreEqual(false, environment.mocks.setupClient.InsertOrUpdateQstSetupsParameter.ReturnList);
            Assert.AreEqual(data.names, names);
            Assert.AreEqual(data.values, values);
            Assert.AreEqual(data.users, users);
        }

        static IEnumerable<(string, List<string>, long)> LoadColumnWidthsCallsClientData = new List<(string, List<string>, long)>()
        {
            (
                "grid X",
                 new List<string>()
                 {
                     "col Z",
                     "col A",
                 }, 
                 5
             ),
            (
                "grid 111",
                new List<string>()
                {
                    "col 3",
                    "col 5",
                },
                9
            )

        };
        [TestCaseSource(nameof(LoadColumnWidthsCallsClientData))]
        public void LoadColumnWidthsCallsClient((string gridName, List<string> columns, long user) data)
        {
            var environment = new Environment();

            SessionInformation.CurrentUser = CreateUser.IdOnly(data.user);

            environment.dataAccess.LoadColumnWidths(data.gridName, data.columns);

            Assert.AreEqual(data.gridName, environment.mocks.setupClient.GetColumnWidthsForGridParameter.GridName);
            Assert.AreEqual(data.columns, environment.mocks.setupClient.GetColumnWidthsForGridParameter.Columns.ToList());
            Assert.AreEqual(data.user, environment.mocks.setupClient.GetColumnWidthsForGridParameter.UserId);
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    clientFactory = new ClientFactoryMock();
                    channelWrapper = new ChannelWrapperMock();
                    setupClient = new SetupClientMock();
                    channelWrapper.GetSetupClientReturnValue = setupClient;
                    clientFactory.AuthenticationChannel = channelWrapper;
                }
                public ClientFactoryMock clientFactory;
                public ChannelWrapperMock channelWrapper;
                public SetupClientMock setupClient;
            }

            public Environment()
            {
                mocks = new Mocks();
                dataAccess = new SaveColumnsDataAccess(mocks.clientFactory);
            }

            public readonly Mocks mocks;
            public readonly SaveColumnsDataAccess dataAccess;
        }
    }
}
