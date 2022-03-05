using System;
using System.Collections.Generic;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;

namespace Server.UseCases.UseCases
{
    public interface IExtensionUseCase
    {
        List<Extension> LoadExtensions();
        List<LocationReferenceLink> GetExtensionLocationLinks(ExtensionId extensionId);
        List<Extension> InsertExtensions(List<ExtensionDiff> diffs, bool returnList);
        void UpdateExtensions(List<ExtensionDiff> extensionDiffs);
        bool IsInventoryNumberUnique(ExtensionInventoryNumber inventoryNumber);
        List<Extension> LoadDeletedExtensions();
    }

    public interface IExtensionDataAccess
    {
        void Commit();
        List<Extension> LoadExtensions();
        List<LocationReferenceLink> GetExtensionLocationLinks(ExtensionId extensionId);
        List<Extension> InsertExtensions(List<ExtensionDiff> diffs, bool returnList);
        void UpdateExtensions(List<ExtensionDiff> extensionDiffs);
        bool IsInventoryNumberUnique(ExtensionInventoryNumber inventoryNumber);
        List<Extension> LoadDeletedExtensions();
    }
    public class ExtensionUseCase : IExtensionUseCase
    {
        private readonly IExtensionDataAccess _extensionDataAccess;

        public ExtensionUseCase(IExtensionDataAccess extensionDataAccess)
        {
            _extensionDataAccess = extensionDataAccess;
        }

        public List<Extension> LoadExtensions()
        {
            return _extensionDataAccess.LoadExtensions();
        }

        public List<LocationReferenceLink> GetExtensionLocationLinks(ExtensionId extensionId)
        {
            return _extensionDataAccess.GetExtensionLocationLinks(extensionId);
        }

        public List<Extension> InsertExtensions(List<ExtensionDiff> diffs, bool returnList)
        {
            var result = _extensionDataAccess.InsertExtensions(diffs, returnList);
            _extensionDataAccess.Commit();
            return result;
        }

        public void UpdateExtensions(List<ExtensionDiff> extensionDiffs)
        {
            _extensionDataAccess.UpdateExtensions(extensionDiffs);
            _extensionDataAccess.Commit();
        }

        public bool IsInventoryNumberUnique(ExtensionInventoryNumber inventoryNumber)
        {
            return _extensionDataAccess.IsInventoryNumberUnique(inventoryNumber);
        }

        public List<Extension> LoadDeletedExtensions()
        {
            return _extensionDataAccess.LoadDeletedExtensions();
        }
    }
}
