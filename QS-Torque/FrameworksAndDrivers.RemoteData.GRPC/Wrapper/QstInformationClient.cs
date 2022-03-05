using BasicTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using String = BasicTypes.String;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class QstInformationClient : IQstInformationClient
    {
        private readonly QstInformationService.QstInformationService.QstInformationServiceClient _qstInformationClient;

        public QstInformationClient(QstInformationService.QstInformationService.QstInformationServiceClient qstInformationClient)
        {
            _qstInformationClient = qstInformationClient;
        }

        public String LoadServerVersion()
        {
            return _qstInformationClient.LoadServerVersion(new NoParams());
        }
    }
}
