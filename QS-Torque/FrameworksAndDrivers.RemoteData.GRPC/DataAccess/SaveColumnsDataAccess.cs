using System.Collections.Generic;
using Core.UseCases;
using SetupService;
using State;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public class SaveColumnsDataAccess : ISaveColumnsData
    {
        private readonly IClientFactory _clientFactory;

        public SaveColumnsDataAccess(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        private ISetupClient GetSetupClient()
        {
            return _clientFactory.AuthenticationChannel.GetSetupClient();
        }

        public void SaveColumnWidths(string gridName, List<(string, double)> columns)
        {
            if (SessionInformation.CurrentUser == null)
            {
                return;
            }

            var request = new InsertOrUpdateQstSetupsRequest()
            {
                ReturnList = false
            };

            foreach (var (columnName, width) in columns)
            {
                request.SetupList.Add(new DtoTypes.QstSetup()
                {
                    UserId = SessionInformation.CurrentUser.UserId.ToLong(),
                    Name = $"{gridName}{columnName}",
                    Value = $"{width}"
                });
            }
            GetSetupClient().InsertOrUpdateQstSetups(request);
        }

        public List<(string, double)> LoadColumnWidths(string gridName, List<string> columns)
        {
            var request = new GetColumnWidthsForGridRequest()
            {
                UserId = SessionInformation.CurrentUser.UserId.ToLong(),
                GridName = gridName,
                Columns = { columns }
            };
            var setupList = GetSetupClient().GetColumnWidthsForGrid(request);

            var result = new List<(string, double)>();
            foreach (var setup in setupList.SetupList)
            {
                result.Add((setup.Name.Replace(gridName, ""), double.Parse(setup.Value)));

            }

            return result;
        }
    }
}
