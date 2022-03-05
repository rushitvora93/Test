using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Grpc.Core;
using ExtensionService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    class ExtensionClient : IExtensionClient
    {
        private readonly Extensions.ExtensionsClient _extensionClient;

        public ExtensionClient(Extensions.ExtensionsClient extensionClient)
        {
            _extensionClient = extensionClient;
        }

        public ListOfLocationLink GetExtensionLocationLinks(LongRequest extensionId)
        {
            return _extensionClient.GetExtensionLocationLinks(extensionId, new CallOptions());
        }

        public ListOfExtensions InsertExtensions(InsertExtensionsRequest request)
        {
            return _extensionClient.InsertExtensions(request, new CallOptions());
        }

        public void UpdateExtensions(UpdateExtensionsRequest request)
        {
            _extensionClient.UpdateExtensions(request, new CallOptions());
        }

        public Bool IsExtensionInventoryNumberUnique(String inventoryNumber)
        {
            return _extensionClient.IsExtensionInventoryNumberUnique(inventoryNumber, new CallOptions());
        }

        public ListOfExtensions LoadExtensions()
        {
            return _extensionClient.LoadExtensions(new NoParams(), new CallOptions());
        }

        public ListOfExtensions LoadDeletedExtensions()
        {
            return _extensionClient.LoadDeletedExtensions(new NoParams(), new CallOptions());
        }
    }
}
