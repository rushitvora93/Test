using Client.Core;
using Client.UseCases.UseCases;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.TestHelper.Mock
{
    public class HistoryUseCaseMock : IHistoryUseCase
    {
        public LocationId LoadLocationHistoryLocationId { get; set; }
        public IHistoryErrorHandler LoadLocationHistoryErrorHandler { get; set; }

        public void LoadLocationHistoryFor(LocationId id, ICatalogProxy localization, IHistoryErrorHandler errorHandler)
        {
            LoadLocationHistoryLocationId = id;
            LoadLocationHistoryErrorHandler = errorHandler;
        }
    }
}