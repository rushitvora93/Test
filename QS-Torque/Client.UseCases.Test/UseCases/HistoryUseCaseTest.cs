using Client.Core.ChangesGenerators;
using Client.Core.Enums;
using Client.TestHelper.Mock;
using Client.UseCases.UseCases;
using Core.Diffs;
using Core.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHelper.Mock;

namespace Client.UseCases.Test.UseCases
{
    public class HistoryErrorHandlerMock : IHistoryErrorHandler
    {
        public bool LocationErrorCalled { get; set; }

        public void LocationHistoryError()
        {
            LocationErrorCalled = true;
        }
    }

    public class HistoryDataMock : IHistoryData
    {
        public LocationId LoadLocationDiffsParameter { get; set; }
        public List<LocationDiff> LoadLocationDiffsReturnValue { get; set; }
        public bool LoadLocationDiffsThrowsError { get; set; }

        public List<LocationDiff> LoadLocationDiffsFor(LocationId id)
        {
            if(LoadLocationDiffsThrowsError)
            {
                throw new Exception();
            }

            LoadLocationDiffsParameter = id;
            return LoadLocationDiffsReturnValue;
        }
    }

    public class HistoryGuiMock : IHistoryGui
    {
        public List<ValueChangesContainer> LoadLocationDiffsParameter { get; set; }

        public void LoadLocationChanges(List<ValueChangesContainer> diffs)
        {
            LoadLocationDiffsParameter = diffs;
        }
    }

    class LocationChangesGeneratorMock : ILocationChangesGenerator
    {
        public List<LocationDiff> GetLocationChangesParameters { get; set; } = new List<LocationDiff>();
        public IEnumerable<SingleValueChange> GetLocationChangesReturnValue { get; set; }

        public IEnumerable<SingleValueChange> GetLocationChanges(LocationDiff diff)
        {
            GetLocationChangesParameters.Add(diff);
            return GetLocationChangesReturnValue;
        }
    }

    public class HistoryUseCaseTest
    {
        [Test]
        public void LoadLocationHistoryPassesFromDataToGui()
        {
            var environment = CreateUseCaseEnvironment();
            var now = DateTime.Now.Ticks;
            var diffs = new List<LocationDiff>()
            {
                new LocationDiff() { TimeStamp = new DateTime(now), Type = DiffType.Insert, User = new User(), Comment = new HistoryComment("")},
                new LocationDiff() { TimeStamp = new DateTime(now + 1), Type = DiffType.Update, User = new User(), Comment = new HistoryComment("")}
            };
            environment.data.LoadLocationDiffsReturnValue = diffs;
            var generator = new LocationChangesGeneratorMock() 
            { 
                GetLocationChangesReturnValue = new List<SingleValueChange>()
                {
                    new SingleValueChange(),
                    new SingleValueChange()
                }
            };
            environment.useCase.LoadLocationHistory(new LocationId(now), null, generator);

            Assert.AreEqual(now, environment.data.LoadLocationDiffsParameter.ToLong());
            Assert.AreSame(diffs[0], generator.GetLocationChangesParameters[0]);
            Assert.AreSame(diffs[1], generator.GetLocationChangesParameters[1]);
            Assert.AreEqual(diffs[0].TimeStamp, environment.gui.LoadLocationDiffsParameter[0].Timestamp);
            Assert.AreEqual(diffs[0].Type, environment.gui.LoadLocationDiffsParameter[0].Type);
            Assert.AreSame(diffs[0].User, environment.gui.LoadLocationDiffsParameter[0].User);
            Assert.AreSame(diffs[0].Comment, environment.gui.LoadLocationDiffsParameter[0].Comment);
            Assert.AreSame(generator.GetLocationChangesReturnValue.ToList()[0], environment.gui.LoadLocationDiffsParameter[0].Changes[0]);
            Assert.AreSame(generator.GetLocationChangesReturnValue.ToList()[1], environment.gui.LoadLocationDiffsParameter[0].Changes[1]);
            Assert.AreEqual(diffs[1].TimeStamp, environment.gui.LoadLocationDiffsParameter[1].Timestamp);
            Assert.AreEqual(diffs[1].Type, environment.gui.LoadLocationDiffsParameter[1].Type);
            Assert.AreSame(diffs[1].User, environment.gui.LoadLocationDiffsParameter[1].User);
            Assert.AreSame(diffs[1].Comment, environment.gui.LoadLocationDiffsParameter[1].Comment);
            Assert.AreSame(generator.GetLocationChangesReturnValue.ToList()[0], environment.gui.LoadLocationDiffsParameter[1].Changes[0]);
            Assert.AreSame(generator.GetLocationChangesReturnValue.ToList()[1], environment.gui.LoadLocationDiffsParameter[1].Changes[1]);
        }

        [Test]
        public void LoadLocationHistoryHandlesError()
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.LoadLocationDiffsThrowsError = true;
            environment.useCase.LoadLocationHistoryFor(new LocationId(0), null, environment.errorHandler);
            Assert.IsTrue(environment.errorHandler.LocationErrorCalled);
        }


        static Environment CreateUseCaseEnvironment()
        {
            var environment = new Environment();
            environment.gui = new HistoryGuiMock();
            environment.data = new HistoryDataMock();
            environment.errorHandler = new HistoryErrorHandlerMock();
            environment.useCase = new HistoryUseCase(environment.data,
                environment.gui,
                new MockLocationDisplayFormatter());
            return environment;
        }

        struct Environment
        {
            public HistoryUseCase useCase;
            public HistoryGuiMock gui;
            public HistoryDataMock data;
            public HistoryErrorHandlerMock errorHandler;
        }
    }
}
