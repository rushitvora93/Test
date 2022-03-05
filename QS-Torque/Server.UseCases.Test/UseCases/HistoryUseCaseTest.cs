using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.UseCases.Test.UseCases
{
    public class HistoryDataMock : IHistoryData
    {
        public LocationId LoadLocationDiffsForParameter { get; set; }
        public List<LocationDiff> LoadLocationDiffsReturnValue { get; set; }

        public List<LocationDiff> LoadLocationDiffsFor(LocationId id)
        {
            LoadLocationDiffsForParameter = id;
            return LoadLocationDiffsReturnValue;
        }
    }

    public class HistoryUseCaseTest
    {
        [TestCase(1)]
        [TestCase(11)]
        public void LoadLoctaionDiffsTest(long idVal)
        {
            var environment = CreateUseCaseEnvironment();
            environment.data.LoadLocationDiffsReturnValue = new List<LocationDiff>();
            var id = new LocationId(idVal);
            var result = environment.useCase.LoadLocationDiffsFor(id);
            Assert.AreSame(environment.data.LoadLocationDiffsForParameter, id);
            Assert.AreSame(environment.data.LoadLocationDiffsReturnValue, result);
        }


        private static (HistoryUseCase useCase, HistoryDataMock data) CreateUseCaseEnvironment()
        {
            var data = new HistoryDataMock();
            var useCase = new HistoryUseCase(data);
            return (useCase, data);
        }
    }
}
