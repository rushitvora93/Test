using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Grpc.Core;
using ManufacturerService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class ManufacturerClient : IManufacturerClient
    {
        private readonly Manufacturers.ManufacturersClient _manufacturerClient;

        public ManufacturerClient(Manufacturers.ManufacturersClient manufacturerClient)
        {
            _manufacturerClient = manufacturerClient;
        }

        public ListOfManufacturers LoadManufacturers()
        {
            return _manufacturerClient.LoadManufacturers(new NoParams(), new CallOptions());
        }

        public StringResponse GetManufacturerComment(LongRequest longRequest)
        {
            return _manufacturerClient.GetManufacturerComment(longRequest, new CallOptions());
        }

        public ListOfModelLink GetManufacturerModelLinks(LongRequest longRequest)
        {
            return _manufacturerClient.GetManufacturerModelLinks(longRequest, new CallOptions());
        }

        public ListOfManufacturers InsertManufacturerWithHistory(InsertManufacturerWithHistoryRequest request)
        {
            return _manufacturerClient.InsertManufacturerWithHistory(request, new CallOptions());
        }

        public ListOfManufacturers UpdateManufacturerWithHistory(UpdateManufacturerWithHistoryRequest request)
        {
            return _manufacturerClient.UpdateManufacturerWithHistory(request, new CallOptions());
        }
    }
}
