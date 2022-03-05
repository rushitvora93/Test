using Server.Core.Diffs;
using Server.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.UseCases.UseCases
{
    public interface IHistoryData
    {
        List<LocationDiff> LoadLocationDiffsFor(LocationId id);
    }

    public interface IHistoryUseCase
    {
        List<LocationDiff> LoadLocationDiffsFor(LocationId id);
    }

    public class HistoryUseCase : IHistoryUseCase
    {
        private IHistoryData _data;

        public HistoryUseCase(IHistoryData data)
        {
            _data = data;
        }

        public List<LocationDiff> LoadLocationDiffsFor(LocationId id)
        {
            return _data.LoadLocationDiffsFor(id);
        }
    }
}
