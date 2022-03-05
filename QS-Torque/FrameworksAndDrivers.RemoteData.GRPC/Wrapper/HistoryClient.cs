using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoTypes;
using BasicTypes;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class HistoryClient : IHistoryClient
    {
        private readonly HistoryService.Histories.HistoriesClient _historyClient;

        public HistoryClient(HistoryService.Histories.HistoriesClient historyClient)
        {
            _historyClient = historyClient;
        }

        public ListOfLocationDiff LoadLocationDiffsFor(Long param)
        {
            return _historyClient.LoadLocationDiffsFor(param);
        }
    }
}
