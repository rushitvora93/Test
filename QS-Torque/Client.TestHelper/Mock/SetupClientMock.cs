using System.Collections.Generic;
using FrameworksAndDrivers.RemoteData.GRPC;
using SetupService;

namespace Client.TestHelper.Mock
{
    public class SetupClientMock : ISetupClient
    {
        private int _getQstSetupsByUserIdAndNameCallCounter;
        public ListOfQstSetup GetQstSetupsByUserIdAndName(GetQstSetupsByUserIdAndNameRequest request)
        {
            GetQstSetupsByUserIdAndNameParameters.Add(request);
            return GetQstSetupsByUserIdAndNameReturnValues[_getQstSetupsByUserIdAndNameCallCounter++];
        }

        public ListOfQstSetup GetColumnWidthsForGrid(GetColumnWidthsForGridRequest request)
        {
            GetColumnWidthsForGridParameter = request;
            return GetColumnWidthsForGridReturnValue;
        }

        public ListOfQstSetup InsertOrUpdateQstSetups(InsertOrUpdateQstSetupsRequest request)
        {
            InsertOrUpdateQstSetupsCalled = true;
            InsertOrUpdateQstSetupsParameter = request;
            return null;
        }

        public InsertOrUpdateQstSetupsRequest InsertOrUpdateQstSetupsParameter { get; set; }
        public bool InsertOrUpdateQstSetupsCalled { get; set; }
        public ListOfQstSetup GetColumnWidthsForGridReturnValue { get; set; } = new ListOfQstSetup();
        public GetColumnWidthsForGridRequest GetColumnWidthsForGridParameter { get; set; }
        public List<ListOfQstSetup> GetQstSetupsByUserIdAndNameReturnValues { get; set; } = new List<ListOfQstSetup>();
        public List<GetQstSetupsByUserIdAndNameRequest> GetQstSetupsByUserIdAndNameParameters { get; set; } = new List<GetQstSetupsByUserIdAndNameRequest>();
    }
}
