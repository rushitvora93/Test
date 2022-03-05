using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Core.Diffs;
using Core.Entities;
using Core.Entities.ReferenceLink;
using log4net;

namespace Core.UseCases
{
    public interface IExtensionGui
    {
        void ShowExtensions(List<Extension> extensions);

        void ShowReferencedLocations(List<LocationReferenceLink> locationReferenceLinks);
        void AddExtension(Extension addedExtension);
        void UpdateExtension(Extension updatedExtension);
        void RemoveExtension(Extension extension);
    }

    public interface IExtensionDependencyGui
    {
        void ShowRemoveExtensionPreventingReferences(List<LocationReferenceLink> references);
    }

    public interface IExtensionErrorGui
    {
        void ShowErrorMessageLoadingExentsions();
        void ShowErrorMessageLoadingReferencedLocations();
        void ShowProblemSavingExtension();
        void ExtensionAlreadyExists();
        void ShowProblemRemoveExtension();
    }

    public interface IExtensionDataAccess
    {
        List<Extension> LoadExtensions();
        List<LocationReferenceLink> LoadReferencedLocations(ExtensionId id);
        Extension AddExtension(Extension extension, User byUser);
        void SaveExtension(Client.Core.Diffs.ExtensionDiff extensionDiff);
        bool IsInventoryNumberUnique(ExtensionInventoryNumber inventoryNumber);
        void RemoveExtension(Extension extension, User user);
    }

    public interface IExtensionUseCase
    {
        void ShowExtensions(IExtensionErrorGui loadingError);
        void ShowReferencedLocations(IExtensionErrorGui loadingError, ExtensionId id);
        void AddExtension(Extension extension, IExtensionErrorGui errorHandler);
        bool IsInventoryNumberUnique(ExtensionInventoryNumber inventoryNumber);
        void SaveExtension(ExtensionDiff diff, IExtensionErrorGui errorHandler,
            IExtensionSaveGuiShower saveGuiShower);
        void UpdateExtension(ExtensionDiff diff, IExtensionErrorGui errorHandler);
        void RemoveExtension(Extension extension, IExtensionErrorGui errorHandler, IExtensionDependencyGui dependencyGui);
    }

    public interface IExtensionSaveGuiShower
    {
        void SaveExtension(ExtensionDiff diff, Action saveAction);
    }

    public class ExtensionUseCase : IExtensionUseCase
    {
        private IExtensionGui _guiInterface;
        private IExtensionDataAccess _dataInterface;
        private ISessionInformationUserGetter _userGetter;
        private readonly INotificationManager _notificationManager;

        private static readonly ILog Log = LogManager.GetLogger(typeof(ExtensionUseCase));

        
        public ExtensionUseCase(IExtensionGui guiInterface, IExtensionDataAccess dataInterface, ISessionInformationUserGetter userGetter, INotificationManager notificationManager)
        {
            _guiInterface = guiInterface;
            _dataInterface = dataInterface;
            _userGetter = userGetter;
            _notificationManager = notificationManager;
        }

        public virtual void ShowExtensions(IExtensionErrorGui loadingError)
        {
            Log.Info("LoadExtensions started");
            try
            {
                var extensions = _dataInterface.LoadExtensions();
                Log.Debug($"LoadExtensions call with List of Extensions with size of {extensions?.Count}");
                //_guiInterface.ShowExtensions(extensions);
            }
            catch (Exception exception)
            {
                Log.Error("Error while Loading Extensions failed with Exception", exception);
                loadingError.ShowErrorMessageLoadingExentsions();
            }
            Log.Info("LoadExtensions ended");
        }
        public virtual void ShowReferencedLocations(IExtensionErrorGui loadingError, ExtensionId id)
        {
            try
            {
                _guiInterface.ShowReferencedLocations(_dataInterface.LoadReferencedLocations(id));
            }
            catch (Exception exception)
            {
                Log.Error($"Error while loading the referenced locations for a Extension with Id {id.ToLong()}", exception);
                loadingError.ShowErrorMessageLoadingReferencedLocations();
            }
        }

        public void AddExtension(Extension extension, IExtensionErrorGui errorHandler)
        {
            try
            {
                Log.Info("AddExtension started");
                var addedExtension = _dataInterface.AddExtension(extension, _userGetter?.GetCurrentUser());
                _guiInterface.AddExtension(addedExtension);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception exception)
            {
                Log.Error("Error in AddExtension", exception);
                errorHandler.ShowProblemSavingExtension();
            }
            Log.Info("AddExtension ended");
        }

        public void SaveExtension(ExtensionDiff diff, IExtensionErrorGui errorHandler,
            IExtensionSaveGuiShower saveGuiShower)
        {
            saveGuiShower.SaveExtension(diff, () =>
            {
                UpdateExtension(diff, errorHandler);
            });
        }

        public void UpdateExtension(ExtensionDiff diff, IExtensionErrorGui errorHandler)
        {
            try
            {
                Log.Info("SaveExtension started");

                if (!diff.OldExtension.InventoryNumber.Equals(diff.NewExtension.InventoryNumber) && 
                    !_dataInterface.IsInventoryNumberUnique(diff.NewExtension.InventoryNumber))
                {
                    errorHandler.ExtensionAlreadyExists();
                    return;
                }

                diff.User = _userGetter?.GetCurrentUser();
                _dataInterface.SaveExtension(diff);
                _guiInterface.UpdateExtension(diff.NewExtension);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception e)
            {
                errorHandler.ShowProblemSavingExtension();
                Log.Error("Error in SaveExtension", e);
            }
        }

        public bool IsInventoryNumberUnique(ExtensionInventoryNumber inventoryNumber)
        {
            return _dataInterface.IsInventoryNumberUnique(inventoryNumber);
        }

        public void RemoveExtension(Extension extension, IExtensionErrorGui errorHandler, IExtensionDependencyGui dependencyGui)
        {
            try
            {
                Log.Info("RemoveExtension started");

                var references = _dataInterface.LoadReferencedLocations(extension.Id);
                if (references != null && references.Count > 0)
                {
                    dependencyGui.ShowRemoveExtensionPreventingReferences(references);
                    return;
                }

                _dataInterface.RemoveExtension(extension, _userGetter?.GetCurrentUser());
                _guiInterface.RemoveExtension(extension);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception e)
            {
                errorHandler.ShowProblemRemoveExtension();
                Log.Error("Error in RemoveExtension", e);
            }
        }
    }
    
    public class ExtensionHumbleAsyncRunner : IExtensionUseCase
    {
        private IExtensionUseCase _real;

        public ExtensionHumbleAsyncRunner(IExtensionUseCase real)
        {
            _real = real;
        }

        public void ShowExtensions(IExtensionErrorGui loadingError)
        {
            Task.Run(() => _real.ShowExtensions(loadingError));
        }

        public void ShowReferencedLocations(IExtensionErrorGui loadingError, ExtensionId id)
        {

            Task.Run(() => _real.ShowReferencedLocations(loadingError, id));
        }

        public void AddExtension(Extension extension, IExtensionErrorGui errorHandler)
        {
            Task.Run(() => _real.AddExtension(extension, errorHandler));
        }

        public bool IsInventoryNumberUnique(ExtensionInventoryNumber inventoryNumber)
        {
            return _real.IsInventoryNumberUnique(inventoryNumber);
        }

        public void SaveExtension(ExtensionDiff diff, IExtensionErrorGui errorHandler, IExtensionSaveGuiShower saveGuiShower)
        {
            Task.Run(() => _real.SaveExtension(diff, errorHandler, saveGuiShower));
        }

        public void UpdateExtension(ExtensionDiff diff, IExtensionErrorGui errorHandler)
        {
            Task.Run(() => _real.UpdateExtension(diff, errorHandler));
        }

        public void RemoveExtension(Extension extension, IExtensionErrorGui errorHandler, IExtensionDependencyGui dependencyGui)
        {
            Task.Run(() => _real.RemoveExtension(extension, errorHandler, dependencyGui));
        }
    }
}
