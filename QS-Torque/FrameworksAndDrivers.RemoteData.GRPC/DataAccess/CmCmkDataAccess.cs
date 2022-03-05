using System.Globalization;
using System.Linq;
using Core.UseCases;
using SetupService;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public class CmCmkDataAccess : ICmCmkDataAccess
    {
        private readonly IClientFactory _clientFactory;

        public CmCmkDataAccess(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private ISetupClient GetSetupClient()
        {
            return _clientFactory.AuthenticationChannel.GetSetupClient();
        }

        public (double cm, double cmk) LoadCmCmk()
        {
            var cmRequest = new GetQstSetupsByUserIdAndNameRequest()
            {
                UserId = 0,
                Name = "Cm"
            };

            var cmkRequest = new GetQstSetupsByUserIdAndNameRequest()
            {
                UserId = 0,
                Name = "Cmk"
            };
            var cm = GetSetupClient().GetQstSetupsByUserIdAndName(cmRequest).SetupList.FirstOrDefault();
            var cmk = GetSetupClient().GetQstSetupsByUserIdAndName(cmkRequest).SetupList.FirstOrDefault();

            const double defaultCm = 1.67;
            const double defaultCmk = 1.33;

            return
            (
                cm != null
                    ? double.Parse(cm.Value.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture)
                    : defaultCm,
                cmk != null
                    ? double.Parse(cmk.Value.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture)
                    : defaultCmk
            );
        }
    }
}
