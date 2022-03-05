using Client.Core.Enums;
using Client.UseCases.UseCases;
using Core.Diffs;
using Core.Entities;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface IHistoryClient
    {
        DtoTypes.ListOfLocationDiff LoadLocationDiffsFor(BasicTypes.Long param);
    }

    public class HistoryDataAccess : IHistoryData
    {
        private readonly IClientFactory _clientFactory;

        public HistoryDataAccess(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private IHistoryClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetHistoryClient();
        }

        public List<LocationDiff> LoadLocationDiffsFor(LocationId id)
        {
            var diffs = GetClient().LoadLocationDiffsFor(new BasicTypes.Long() { Value = id.ToLong() });
            var mapper = new Mapper();
            return diffs.LocationDiffs.Select(x => mapper.DirectPropertyMapping(x)).ToList();
        }
    }
}
