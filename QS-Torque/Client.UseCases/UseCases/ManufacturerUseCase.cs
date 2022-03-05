using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.ReferenceLink;
using log4net;

namespace Core.UseCases
{

	public interface IManufacturerGui
    {
        void ShowManufacturer(List<Manufacturer> manufacturer);
        void AddManufacturer(Manufacturer manufacturer);
        void ShowErrorMessage();
        void ShowComment(Manufacturer manufacturer, string comment);
        void ShowCommentError();
        void SaveManufacturer(Manufacturer manufacturer);
        void RemoveManufacturer(Manufacturer manufacturer);
        void ShowReferencedToolModels(List<ToolModelReferenceLink> toolModelReferenceLinks);
        void ShowReferencesError();
        void ShowRemoveManufacturerPreventingReferences(List<ToolModelReferenceLink> references);
    }

    public interface IManufacturerData
    {
        List<Manufacturer> LoadManufacturer();
        Manufacturer AddManufacturer(Manufacturer manufacturer, User byUser);
        void RemoveManufacturer(Manufacturer manufacturer, User byUser);
        Manufacturer SaveManufacturer(ManufacturerDiff manufacturer);
        string LoadManufacturerForComment(Manufacturer manufacturer);
        List<ToolModelReferenceLink> LoadToolModelReferenceLinksForManufacturerId(long manufacturerId);
    }

    public interface IManufacturerUseCase
    {
        void LoadManufacturer();
        void LoadCommentForManufacturer(Manufacturer manufacturer);
        void AddManufacturer(Manufacturer manufacturer);
        void RemoveManufacturer(Manufacturer manufacturer, IManufacturerGui active);
        void SaveManufacturer(Manufacturer oldManufacturer, Manufacturer newManufacturer);
        void LoadReferencedToolModels(long manufacturerId);
    }

    public class ManufacturerUseCase : IManufacturerUseCase
    {
        private static  readonly ILog Log = LogManager.GetLogger(typeof(ManufacturerUseCase)); 

        private IManufacturerGui _guiInterface;
        private IManufacturerData _dataInterface;
		private ISessionInformationUserGetter _userGetter;
        private readonly INotificationManager _notificationManager;

        public ManufacturerUseCase(IManufacturerGui guiInterface, IManufacturerData dataInterface, ISessionInformationUserGetter userGetter, INotificationManager notificationManager)
        {
            _guiInterface = guiInterface;
            _dataInterface = dataInterface;
			_userGetter = userGetter;
            _notificationManager = notificationManager;
        }

        public void LoadManufacturer()
        {
            Log.Info("LoadManfucaturer started");
            try
            {
                var manufacturer = _dataInterface.LoadManufacturer();
                Log.Debug($"ShowManfacturer call with List of Manufactuer Size of {manufacturer?.Count}");
                _guiInterface.ShowManufacturer(manufacturer);
            }
            catch (Exception exception)
            {
                Log.Error("Error while loading Manufacturer failed with Exception",exception);
                _guiInterface.ShowErrorMessage();
            }
            Log.Info("LoadManfucaturer ended");
        }

        public void LoadCommentForManufacturer(Manufacturer manufacturer)
        {
            Log.Info("LoadCommentForManufacturer started");
            Log.Debug($"LoadCommentForManufacturer called with Manufacturer Name:{manufacturer?.Name} and Id:{manufacturer?.Id}");
            try
            {
               var comment = _dataInterface.LoadManufacturerForComment(manufacturer);
               Log.Debug($"ShowComment call with Manufacturer Name:{manufacturer?.Name} and Id:{manufacturer?.Id} and Comment {comment}");
               _guiInterface.ShowComment(manufacturer, comment);
            }
            catch (Exception exception)
            {
                Log.Error($"Error while Loading Comment for Manufacturer Name:{manufacturer?.Name} and Id:{manufacturer?.Id}", exception);
                _guiInterface.ShowCommentError();
            }
            Log.Info("LoadCommentForManufacturer ended");
        }

        public void AddManufacturer(Manufacturer manufacturer)
        {
            Log.Info("AddManufacturer started");
            Log.Debug($"AddManufacturer called with Manufacturer Name:{manufacturer?.Name} and Id:{manufacturer?.Id}");
            try
            {
                var manu = _dataInterface.AddManufacturer(manufacturer, _userGetter.GetCurrentUser());
                Log.Debug($"AddManufacturer call with Manufacturer Name:{manufacturer?.Name} and Id:{manufacturer?.Id}");
                _guiInterface.AddManufacturer(manu);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception exception)
            {
                Log.Error($"Error while adding Manufacturer {manufacturer.Name}", exception);
                _guiInterface.ShowErrorMessage();
            }
            Log.Info("AddManufacturer ended");
        }

        public void RemoveManufacturer(Manufacturer manufacturer, IManufacturerGui active)
        {
            Log.Info("RemoveManufacturer started");
            Log.Debug($"RemoveManufacturer called with Manufacturer Name:{manufacturer?.Name} and Id:{manufacturer?.Id}");
            try
            {
                var references = _dataInterface.LoadToolModelReferenceLinksForManufacturerId(manufacturer.Id.ToLong());
                if (references != null && references.Count > 0)
                {
                    active.ShowRemoveManufacturerPreventingReferences(references);
                    return;
                }
				_dataInterface.RemoveManufacturer(manufacturer, _userGetter.GetCurrentUser());
                _guiInterface.RemoveManufacturer(manufacturer);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception exception)
            {
                Log.Error($"Error while removing Manufacturer Name:{manufacturer?.Name} and Id:{manufacturer?.Id}", exception);
                _guiInterface.ShowErrorMessage();
            }
            Log.Info("Removemanufacturer ended");
        }

        public void SaveManufacturer(Manufacturer oldManufacturer, Manufacturer newManufacturer)
        {
            Log.Info("SaveManufacturer started");
            Log.Debug($"SaveManufacturer called with Manufacturer Name:{newManufacturer?.Name} and Id:{newManufacturer?.Id}");
            try
            {
                var updateManufacturer = _dataInterface.SaveManufacturer(new ManufacturerDiff(_userGetter.GetCurrentUser(), new HistoryComment(""), oldManufacturer, newManufacturer));
                Log.Debug($"Gui.SaveManufacturer call with Manufacturer Name:{newManufacturer?.Name} and Id:{newManufacturer?.Id}");
                _guiInterface.SaveManufacturer(updateManufacturer);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception exception)
            {
                Log.Error($"Error while Saving Manufacturer Name:{newManufacturer?.Name} and Id:{newManufacturer?.Id}",
                    exception);
                _guiInterface.ShowErrorMessage();
            }
            Log.Info("SaveManufacturer ended");
        }

        public void LoadReferencedToolModels(long manufacturerId)
        {
            Log.Info("LoadReferencedToolModels started");
            try
            {
                var referencedToolModels = _dataInterface.LoadToolModelReferenceLinksForManufacturerId(manufacturerId);
                Log.Debug($"{referencedToolModels.Count} referenced ToolModels loaded");
                _guiInterface.ShowReferencedToolModels(referencedToolModels);
            }
            catch (Exception exception)
            {
                Log.Error($"Error while loading the ToolModels", exception);
                _guiInterface.ShowReferencesError();
            }
            Log.Info("LoadReferencedToolModels ended");
        }
    }

    public class ManufacturerHumbleAsyncRunner: IManufacturerUseCase
    {
        public ManufacturerHumbleAsyncRunner(IManufacturerUseCase real)
        {
            _real = real;
        }

        public void LoadManufacturer()
        {
            Task.Run(() => { _real.LoadManufacturer(); });
        }

        public void LoadCommentForManufacturer(Manufacturer manufacturer)
        {
            Task.Run(() => { _real.LoadCommentForManufacturer(manufacturer); });
        }

        public void AddManufacturer(Manufacturer manufacturer)
        {
            Task.Run(() => { _real.AddManufacturer(manufacturer); });
        }

        public void RemoveManufacturer(Manufacturer manufacturer, IManufacturerGui active)
        {
            Task.Run(() => { _real.RemoveManufacturer(manufacturer, active); });
        }

        public void SaveManufacturer(Manufacturer oldManufacturer, Manufacturer newManufacturer)
        {
            Task.Run(() => { _real.SaveManufacturer(oldManufacturer, newManufacturer); });
        }

        public void LoadReferencedToolModels(long manufacturerId)
        {
            Task.Run(() => { _real.LoadReferencedToolModels(manufacturerId); });
        }

        private IManufacturerUseCase _real;
    }
}