using BasicTypes;
using Client.TestHelper.Mock;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using ProcessControlService;
using System;
using System.Collections.Generic;
using System.Linq;
using Client.Core.Diffs;
using Client.TestHelper.Factories;
using Core.Entities;
using TestHelper.Factories;
using TestHelper.Mock;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    public class ProcessControlClientMock : IProcessControlClient
    {
        public Long LoadProcessControlConditionForLocationParameter { get; set; }
        public LoadProcessControlConditionForLocationResponse LoadProcessControlConditionForLocationReturnValue { get; set; }
        public ListOfProcessControlCondition InsertProcessControlConditionsWithHistoryReturnValue { get; set; } 
        public InsertProcessControlConditionsWithHistoryRequest InsertProcessControlConditionsWithHistoryParameter { get; set; }
        public UpdateProcessControlConditionsWithHistoryRequest UpdateProcessControlConditionsWithHistoryParameter { get; set; }
        public bool LoadProcessControlConditionsCalled { get; set; }
        public ListOfProcessControlConditions LoadProcessControlConditionsReturnValue { get; set; }

        public LoadProcessControlConditionForLocationResponse LoadProcessControlConditionForLocation(Long locationId)
        {
            LoadProcessControlConditionForLocationParameter = locationId;
            return LoadProcessControlConditionForLocationReturnValue;
        }

        public ListOfProcessControlCondition InsertProcessControlConditionsWithHistory(
            InsertProcessControlConditionsWithHistoryRequest request)
        {
            InsertProcessControlConditionsWithHistoryParameter = request;
            return InsertProcessControlConditionsWithHistoryReturnValue;
        }

        public void UpdateProcessControlConditionsWithHistory(UpdateProcessControlConditionsWithHistoryRequest updateRequest)
        {
            UpdateProcessControlConditionsWithHistoryParameter = updateRequest;
        }

        public ListOfProcessControlConditions LoadProcessControlConditions(NoParams request)
        {
            LoadProcessControlConditionsCalled = true;
            return LoadProcessControlConditionsReturnValue;
        }
    }

    public class ProcessControlDataAccessTest
    {
        [Test]
        public void LoadProcessControlConditionForLocationThrowsArgumentExceptionWithNoLocation()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.LoadProcessControlConditionForLocation(null); });
        }

        [TestCase(1)]
        [TestCase(5)]
        public void LoadProcessControlConditionForLocationCallsClient(long id)
        {
            var environment = new Environment();
            var location = CreateLocation.IdOnly(id);
            environment.dataAccess.LoadProcessControlConditionForLocation(location);

            Assert.AreEqual(id, environment.mocks.processControlClient.LoadProcessControlConditionForLocationParameter.Value);
        }

        static IEnumerable<DtoTypes.ProcessControlCondition> ProcessControlDtos = 
            new List<DtoTypes.ProcessControlCondition>()
            {
                DtoFactory.CreateProcessControlConditionRandomized(32424),
                DtoFactory.CreateProcessControlConditionRandomized(325),
                null
            };

        [TestCaseSource(nameof(ProcessControlDtos))]
        public void LoadProcessControlConditionForLocationReturnsCorrectValue(DtoTypes.ProcessControlCondition dto)
        {
            var environment = new Environment();
            environment.mocks.processControlClient.LoadProcessControlConditionForLocationReturnValue =
                new LoadProcessControlConditionForLocationResponse() {Value = dto};
            var result = environment.dataAccess.LoadProcessControlConditionForLocation(CreateLocation.Anonymous());

            Assert.IsTrue(EqualityChecker.CompareProcessControlConditionToDto(result, dto));
        }

        [Test]
        public void AddProcessControlConditionWithoutUserThrowsArgumentException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.AddProcessControlCondition(null, null); });
        }

        static IEnumerable<(Client.Core.Entities.ProcessControlCondition, Core.Entities.User)> AddRemoveProcessControlConditionData = new List<(Client.Core.Entities.ProcessControlCondition, Core.Entities.User)>()
        {
            (
                CreateProcessControlCondition.Randomized(12343),
                CreateUser.IdOnly(2)
            ),
            (
                CreateProcessControlCondition.Randomized(678678),
                CreateUser.IdOnly(32)
            )
        };

        [TestCaseSource(nameof(AddRemoveProcessControlConditionData))]
        public void AddProcessControlConditionCallsClient((Client.Core.Entities.ProcessControlCondition processControlCondition, Core.Entities.User user) data)
        {
            var environment = new Environment();
            environment.mocks.processControlClient.InsertProcessControlConditionsWithHistoryReturnValue = new ListOfProcessControlCondition() { ProcessControlConditions = { new DtoTypes.ProcessControlCondition() } };

            environment.dataAccess.AddProcessControlCondition(data.processControlCondition, data.user);

            var clientParam = environment.mocks.processControlClient.InsertProcessControlConditionsWithHistoryParameter;
            var clientDiff = clientParam.Diffs.ConditionDiff.First();

            Assert.AreEqual(1, clientParam.Diffs.ConditionDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.AreEqual("", clientDiff.Comment.Value);
            Assert.IsTrue(EqualityChecker.CompareProcessControlConditionToDto(data.processControlCondition, clientDiff.NewCondition));

            Assert.IsNull(clientDiff.OldCondition);
            Assert.IsTrue(clientParam.ReturnList);
        }

        static IEnumerable<DtoTypes.ProcessControlCondition> ProcessControlConditionDtoData = new List<DtoTypes.ProcessControlCondition>()
        {
            DtoFactory.CreateProcessControlConditionRandomized(435345),
            DtoFactory.CreateProcessControlConditionRandomized(324)
        };

        [TestCaseSource(nameof(ProcessControlConditionDtoData))]
        public void AddProcessControlConditionReturnsCorrectValue(DtoTypes.ProcessControlCondition processControlConditionDto)
        {
            var environment = new Environment();
            environment.mocks.processControlClient.InsertProcessControlConditionsWithHistoryReturnValue = new ListOfProcessControlCondition() { ProcessControlConditions = { processControlConditionDto } };
            var result = environment.dataAccess.AddProcessControlCondition(CreateProcessControlCondition.Randomized(43534), CreateUser.Anonymous());

            Assert.IsTrue(EqualityChecker.CompareProcessControlConditionToDto(result, processControlConditionDto));
        }

        [Test]
        public void AddProcessControlConditionReturnsNullThrowsException()
        {
            var environment = new Environment();

            Assert.Throws<NullReferenceException>(() =>
            {
                environment.dataAccess.AddProcessControlCondition(CreateProcessControlCondition.Randomized(435467), CreateUser.Anonymous());
            });
        }
        
        [Test]
        public void SaveProcessControlConditionWithMismatchingIdsThrowsArgumentException()
        {
            var environment = new Environment();
            var processControlConditionDiff = new Client.Core.Diffs.ProcessControlConditionDiff(CreateUser.IdOnly(4), null, CreateProcessControlCondition.WithId(1), CreateProcessControlCondition.WithId(2));
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.SaveProcessControlCondition(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { processControlConditionDiff }); });
        }

        [Test]
        public void SaveUnchangedProcessControlConditionDontCallClient()
        {
            var environment = new Environment();
            var processControlConditionDiff = new Client.Core.Diffs.ProcessControlConditionDiff(CreateUser.IdOnly(4), null, CreateProcessControlCondition.WithId(1), CreateProcessControlCondition.WithId(1));
            environment.dataAccess.SaveProcessControlCondition(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { processControlConditionDiff });
            Assert.IsNull(environment.mocks.processControlClient.UpdateProcessControlConditionsWithHistoryParameter);
        }

        [Test]
        public void RemoveProcessControlConditionWithoutUserThrowsArgumentException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.RemoveProcessControlCondition(CreateProcessControlCondition.WithId(1), null); });
        }


        [TestCaseSource(nameof(AddRemoveProcessControlConditionData))]
        public void RemoveProcessControlConditionCallsClient((Client.Core.Entities.ProcessControlCondition processControlCondition, Core.Entities.User user) data)
        {
            var environment = new Environment();

            environment.dataAccess.RemoveProcessControlCondition(data.processControlCondition, data.user);

            var clientParam = environment.mocks.processControlClient.UpdateProcessControlConditionsWithHistoryParameter;
            var clientDiff = clientParam.ConditionDiffs.ConditionDiff.First();

            Assert.AreEqual(1, clientParam.ConditionDiffs.ConditionDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.AreEqual("", clientDiff.Comment.Value);
            Assert.IsTrue(EqualityChecker.CompareProcessControlConditionToDto(data.processControlCondition, clientDiff.NewCondition));
            Assert.IsTrue(EqualityChecker.CompareProcessControlConditionToDto(data.processControlCondition, clientDiff.OldCondition));
            Assert.AreEqual(true, clientDiff.OldCondition.Alive);
            Assert.AreEqual(false, clientDiff.NewCondition.Alive);
            Assert.AreEqual(true, clientDiff.OldCondition.ProcessControlTech.QstProcessControlTech.Alive);
            Assert.AreEqual(false, clientDiff.NewCondition.ProcessControlTech.QstProcessControlTech.Alive);
        }

        static IEnumerable<(Client.Core.Entities.ProcessControlCondition, Client.Core.Entities.ProcessControlCondition, Core.Entities.User, string comment)> SaveProcessControlConditionCallsClientData = new List<(Client.Core.Entities.ProcessControlCondition, Client.Core.Entities.ProcessControlCondition, Core.Entities.User, string)>()
        {
            (
                CreateProcessControlCondition.RandomizedWithId(4335456, 1),
                CreateProcessControlCondition.RandomizedWithId(5758, 1),
                CreateUser.IdOnly(1),
                "ertet"
            ),
            (
                CreateProcessControlCondition.RandomizedWithId(33345, 2),
                CreateProcessControlCondition.RandomizedWithId(778,2),
                CreateUser.IdOnly(2),
                "3242345435"
            ),
        };

        [TestCaseSource(nameof(SaveProcessControlConditionCallsClientData))]
        public void SaveProcessControlConditionCallsClient((Client.Core.Entities.ProcessControlCondition oldProcessControlCondition, Client.Core.Entities.ProcessControlCondition newProcessControlCondition, Core.Entities.User user, string comment) data)
        {
            var environment = new Environment();
            var processControlConditionDiff = new Client.Core.Diffs.ProcessControlConditionDiff(data.user, new HistoryComment(data.comment), data.oldProcessControlCondition, data.newProcessControlCondition);
            environment.dataAccess.SaveProcessControlCondition(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { processControlConditionDiff });

            var clientParam = environment.mocks.processControlClient.UpdateProcessControlConditionsWithHistoryParameter;
            var clientDiff = clientParam.ConditionDiffs.ConditionDiff.First();

            Assert.AreEqual(1, clientParam.ConditionDiffs.ConditionDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.IsFalse(clientDiff.Comment.IsNull);
            Assert.AreEqual(data.comment, clientDiff.Comment.Value);
            Assert.IsTrue(EqualityChecker.CompareProcessControlConditionToDto(data.oldProcessControlCondition, clientDiff.OldCondition));
            Assert.IsTrue(EqualityChecker.CompareProcessControlConditionToDto(data.newProcessControlCondition, clientDiff.NewCondition));
            Assert.IsTrue(clientDiff.OldCondition.Alive);
            Assert.IsTrue(clientDiff.NewCondition.Alive);
            if (clientDiff.OldCondition?.ProcessControlTech.QstProcessControlTech != null)
            {
                Assert.IsTrue(clientDiff.OldCondition.ProcessControlTech.QstProcessControlTech.Alive);
            }

            if (clientDiff.NewCondition?.ProcessControlTech != null)
            {
                Assert.IsTrue(clientDiff.NewCondition.ProcessControlTech.QstProcessControlTech.Alive);
            }
        }

        [Test]
        public void LoadProcessControlConditionsCallsClient()
        {
            var environment = new Environment();
            environment.mocks.processControlClient.LoadProcessControlConditionsReturnValue = new ListOfProcessControlConditions();
            environment.dataAccess.LoadProcessControlConditions();

            Assert.IsTrue(environment.mocks.processControlClient.LoadProcessControlConditionsCalled);
        }

        static IEnumerable<List<DtoTypes.ProcessControlCondition>> ProcessControlDtoLists =
            new List<List<DtoTypes.ProcessControlCondition>>()
            {
                new List<ProcessControlCondition>()
                {
                    DtoFactory.CreateProcessControlConditionRandomized(32424),
                    DtoFactory.CreateProcessControlConditionRandomized(325)
                },
                new List<ProcessControlCondition>()
                {
                    DtoFactory.CreateProcessControlConditionRandomized(741),
                    DtoFactory.CreateProcessControlConditionRandomized(258)
                }
            };

        [TestCaseSource(nameof(ProcessControlDtoLists))]
        public void LoadProcessControlConditionsReturnsCorrectValue(List<DtoTypes.ProcessControlCondition> dtos)
        {
            var environment = new Environment();
            environment.mocks.processControlClient.LoadProcessControlConditionsReturnValue = new ListOfProcessControlConditions();
            dtos.ForEach(x => environment.mocks.processControlClient.LoadProcessControlConditionsReturnValue.Conditions.Add(x));
            var result = environment.dataAccess.LoadProcessControlConditions();

            Assert.IsTrue(EqualityChecker.CompareProcessControlConditionToDto(result[0], dtos[0]));
            Assert.IsTrue(EqualityChecker.CompareProcessControlConditionToDto(result[1], dtos[1]));
        }


        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    clientFactory = new ClientFactoryMock();
                    channelWrapper = new ChannelWrapperMock();
                    processControlClient = new ProcessControlClientMock();
                    channelWrapper.GetProcessControlClientReturnValue = processControlClient;
                    clientFactory.AuthenticationChannel = channelWrapper;
                }
                public ClientFactoryMock clientFactory;
                public ChannelWrapperMock channelWrapper;
                public ProcessControlClientMock processControlClient;

            }

            public Environment()
            {
                mocks = new Mocks();
                dataAccess = new ProcessControlDataAccess(mocks.clientFactory);
            }

            public readonly Mocks mocks;
            public readonly ProcessControlDataAccess dataAccess;
        }
    }
}
