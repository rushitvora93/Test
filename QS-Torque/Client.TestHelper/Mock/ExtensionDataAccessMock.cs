using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Core.Diffs;

namespace TestHelper.Mock
{
    public class ExtensionDataAccessMock : IExtensionDataAccess
    {
        public List<Extension> LoadExtensionsData;
        public bool LoadExtensionsThrowsException;
        public bool LoadExtensionsCalled;
        public bool ThrowsLoadReferencedLocationsException;
        public bool LoadReferencedLocationsCalled;
        public ExtensionId LoadReferencedLocationsParameter;
        public List<LocationReferenceLink> LoadReferencedLocationsReturnValue;
        public Extension AddExtensionReturnValue;
        public User AddExtensionParameterUser;
        public Extension AddExtensionParameterExtension;
        public bool AddExtensionThrowsException;
        public bool IsInventoryNumberUniqueReturnValue;
        public ExtensionInventoryNumber IsInventoryNumberUniqueParameter;
        public ExtensionDiff SaveExtensionParameterDiff;
        public bool SaveExtensionThrowsError;
        public User RemoveExtensionUser;
        public bool RemoveExtensionThrowsError;

        public Extension RemoveExtensionExtension;

        public List<Extension> LoadExtensions()
        {
            if (LoadExtensionsThrowsException)
            {
                throw new Exception();
            }
            LoadExtensionsCalled = true;
            return LoadExtensionsData;
        }

        public List<LocationReferenceLink> LoadReferencedLocations(ExtensionId id)
        {
            if (ThrowsLoadReferencedLocationsException)
            {
                throw new Exception();
            }
            LoadReferencedLocationsCalled = true;
            LoadReferencedLocationsParameter = id;
            return LoadReferencedLocationsReturnValue;
        }

        public Extension AddExtension(Extension extension, User byUser)
        {
            if (AddExtensionThrowsException)
            {
                throw new Exception();
            }
            AddExtensionParameterExtension = extension;
            AddExtensionParameterUser = byUser;
            return AddExtensionReturnValue;
        }

        public void SaveExtension(ExtensionDiff extensionDiff)
        {
            if (SaveExtensionThrowsError)
            {
                throw new Exception();
            }

            SaveExtensionParameterDiff = extensionDiff;
        }

        public bool IsInventoryNumberUnique(ExtensionInventoryNumber inventoryNumber)
        {
            IsInventoryNumberUniqueParameter = inventoryNumber;
            return IsInventoryNumberUniqueReturnValue;
        }

        public void RemoveExtension(Extension extension, User user)
        {
            if (RemoveExtensionThrowsError)
            {
                throw new Exception();
            }
            RemoveExtensionExtension = extension;
            RemoveExtensionUser = user;
        }
    }
}
