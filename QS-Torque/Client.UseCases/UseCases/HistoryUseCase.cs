using Client.Core;
using Client.Core.ChangesGenerators;
using Core;
using Core.Diffs;
using Core.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.UseCases.UseCases
{
    public interface IHistoryErrorHandler
    {
        void LocationHistoryError();
    }

    public interface IHistoryData
    {
        List<LocationDiff> LoadLocationDiffsFor(LocationId id);
    }

    public interface IHistoryGui
    {
        void LoadLocationChanges(List<ValueChangesContainer> diffs);
    }

    public interface IHistoryUseCase
    {
        void LoadLocationHistoryFor(LocationId id, ICatalogProxy localization, IHistoryErrorHandler errorHandler);
    }


    public class HistoryUseCase : IHistoryUseCase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(HistoryUseCase));

        private IHistoryData _data;
        private IHistoryGui _gui;
        private ILocationDisplayFormatter _locationDisplayFormatter;

        public HistoryUseCase(IHistoryData data, IHistoryGui gui, ILocationDisplayFormatter locationDisplayFormatter)
        {
            _data = data;
            _gui = gui;
            _locationDisplayFormatter = locationDisplayFormatter;
        }

        public void LoadLocationHistoryFor(LocationId id, ICatalogProxy localization, IHistoryErrorHandler errorHandler) { LoadLocationHistory(id, errorHandler, new LocationChangesGenerator(localization, _locationDisplayFormatter)); }
        public void LoadLocationHistory(LocationId id, IHistoryErrorHandler errorHandler, ILocationChangesGenerator changesGenerator)
        {
            try
            {
                var diffs = _data.LoadLocationDiffsFor(id);
                var realGenerator = changesGenerator;
                var containers = diffs.Select(x => new ValueChangesContainer()
                {
                    Timestamp = x.TimeStamp,
                    User = x.User,
                    Type = x.Type,
                    Comment = x.Comment,
                    Changes = realGenerator.GetLocationChanges(x).ToList()
                }).ToList();
                _gui.LoadLocationChanges(containers);
            }
            catch (Exception e)
            {
                _log.Error("Error in LoadLocationHistory", e);
                errorHandler.LocationHistoryError();
            }
        }
    }

    public class HistoryUseCaseHumbleAsyncRunner : IHistoryUseCase
    {
        private IHistoryUseCase _real;

        public HistoryUseCaseHumbleAsyncRunner(IHistoryUseCase real)
        {
            _real = real;
        }

        public void LoadLocationHistoryFor(LocationId id, ICatalogProxy localization, IHistoryErrorHandler errorHandler)
        {
            Task.Run(() => _real.LoadLocationHistoryFor(id, localization, errorHandler));
        }
    }
}
