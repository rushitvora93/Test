using BasicTypes;
using ClassicTestService;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class ClassicTestClient : IClassicTestClient
    {
        private readonly ClassicTestService.ClassicTestService.ClassicTestServiceClient _classicTestClient;

        public ClassicTestClient(ClassicTestService.ClassicTestService.ClassicTestServiceClient classicTestClient)
        {
            _classicTestClient = classicTestClient;
        }

        public ListOfClassicTestPowToolData GetToolsFromLocationTests(Long locationId)
        {
            return _classicTestClient.GetToolsFromLocationTests(locationId);
        }

        public ListOfClassicMfuTest GetClassicMfuHeaderFromTool(GetClassicHeaderFromToolRequest request)
        {
            return _classicTestClient.GetClassicMfuHeaderFromTool(request);
        }

        public ListOfClassicChkTest GetClassicChkHeaderFromTool(GetClassicHeaderFromToolRequest request)
        {
            return _classicTestClient.GetClassicChkHeaderFromTool(request);
        }

        public ListOfClassicChkTestValue GetValuesFromClassicChkHeader(ListOfLongs globalHistoryIds)
        {
            return _classicTestClient.GetValuesFromClassicChkHeader(globalHistoryIds);
        }

        public ListOfClassicMfuTestValue GetValuesFromClassicMfuHeader(ListOfLongs globalHistoryIds)
        {
            return _classicTestClient.GetValuesFromClassicMfuHeader(globalHistoryIds);
        }

        public ListOfClassicProcessTest GetClassicProcessHeaderFromLocation(Long locationId)
        {
            return _classicTestClient.GetClassicProcessHeaderFromLocation(locationId);
        }

        public ListOfClassicProcessTestValue GetValuesFromClassicProcessHeader(ListOfLongs globalHistoryIds)
        {
            return _classicTestClient.GetValuesFromClassicProcessHeader(globalHistoryIds);
        }
    }
}
