using Core.UseCases;
using String = BasicTypes.String;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{

    public interface IQstInformationClient
    {
        String LoadServerVersion();
    }

    public class QstInformationDataAccess : IQstInformationData
    {
        private readonly IClientFactory _clientFactory;

        public QstInformationDataAccess(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private IQstInformationClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetQstInformationClient();
        }

        public string LoadServerVersion()
        {
            return GetClient().LoadServerVersion().Value;
        }
    }
}
