using Core.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.ReferenceLink;

namespace Core.UseCases
{
    public interface IHelperTableReadOnlyErrorGui<T>
    {
        void ShowErrorMessage();
    }

    public interface IHelperTableGui<T>
    {
        void ShowItems(List<T> items);
        void Add(T newItem);
        void Save(T savedItem); // rename to update?
        void Remove(T removeItem);
    }

    public interface IHelperTableShowReferencesGui
    {
        void ShowReferencesError();
        void ShowReferencedToolModels(List<ToolModelReferenceLink> toolModelReferenceLinks);
        void ShowToolReferenceLinks(List<ToolReferenceLink> toolReferenceLinks);
        void ShowReferencedLocationToolAssignments(List<LocationToolAssignment> assignments);
    }

    public interface IHelperTableErrorGui<T>: IHelperTableReadOnlyErrorGui<T>
    {
        void ShowEntryAlreadyExists(T newItem);
        void ShowRemoveHelperTableItemPreventingReferences(
            List<ToolModelReferenceLink> toolModels,
            List<ToolReferenceLink> tools,
            List<LocationToolAssignment> locationToolAssignments);
    }

    public interface IHelperTableData<T>
    {
        bool HasToolModelAsReference { get; }
        bool HasToolAsReference { get; }
        bool HasLocationToolAssignmentAsReference { get; }

        List<T> LoadItems();
		HelperTableEntityId AddItem(T item, User byUser);
        void RemoveItem(T item, User byUser);
        void SaveItem(T oldItem, T changedItem, User byUser);
        
        List<ToolModelReferenceLink> LoadReferencedToolModels(HelperTableEntityId id);
        List<ToolReferenceLink> LoadToolReferenceLinks(HelperTableEntityId id);
        List<LocationToolAssignmentId> LoadReferencedLocationToolAssignmentIds(HelperTableEntityId id);
    }

    public class EntryAlreadyExists : Exception
    {
        public EntryAlreadyExists(string exceptionText, Exception innerException) : base(exceptionText, innerException) { }
        public EntryAlreadyExists(string exceptionText) : base(exceptionText) { }
    }

    public interface IHelperTableUseCase<T> where T : HelperTableEntity
    {
        void LoadItems(IHelperTableReadOnlyErrorGui<T> errorGui);
        void AddItem(T newItem, IHelperTableErrorGui<T> errorGui);
        void RemoveItem(T removedItem, IHelperTableErrorGui<T> errorGui);
        void SaveItem(T oldItem, T changedItem, IHelperTableErrorGui<T> errorGui);
        void LoadReferences(HelperTableEntityId id, IHelperTableShowReferencesGui errorGui);
    }

    public class HelperTableUseCase<T> : IHelperTableUseCase<T> where T : HelperTableEntity
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(HelperTableUseCase<T>));

        private ILocationToolAssignmentData _locationToolAssignmentData;
        private IHelperTableData<T> _dataInterface;
        private IHelperTableGui<T> _guiInterface;
		private ISessionInformationUserGetter _userGetter;
        private readonly INotificationManager _notificationManager;


        public void LoadItems(IHelperTableReadOnlyErrorGui<T> errorGui)
        {
            try
            {
                _guiInterface.ShowItems(_dataInterface.LoadItems());
            }
            catch (Exception e)
            {
                Log.Error(e);
                errorGui.ShowErrorMessage();
            }
        }

        public void AddItem(T newItem, IHelperTableErrorGui<T> errorGui)
        {
            try
            {
                var id = _dataInterface.AddItem(newItem, _userGetter.GetCurrentUser());
                newItem.ListId = id;
                _guiInterface.Add(newItem);
                _notificationManager.SendSuccessNotification();
            }
            catch (EntryAlreadyExists entryAlreadyExists)
            {
                Log.Error($"Entry already exists ListId:{newItem?.ListId?.ToLong()} of type {typeof(T)}", entryAlreadyExists);
                errorGui.ShowEntryAlreadyExists(newItem);
            }
            catch (Exception exception)
            {
                Log.Error($"Error while Adding helperTableItem with ListId:{newItem?.ListId?.ToLong()} of type {typeof(T)}", exception);
                errorGui.ShowErrorMessage();
            }
        }

        public void RemoveItem(T removedItem, IHelperTableErrorGui<T> errorGui)
        {
            try
            {
                var hasReferences = false;
                List<ToolModelReferenceLink> toolModelReferenceLinks = new List<ToolModelReferenceLink>();
                if (_dataInterface.HasToolModelAsReference)
                {
                    toolModelReferenceLinks = _dataInterface.LoadReferencedToolModels(removedItem.ListId);
                    if (toolModelReferenceLinks != null && toolModelReferenceLinks.Count > 0)
                    {
                        hasReferences = true;
                    }
                }

                var toolReferences = new List<ToolReferenceLink>();
                if (_dataInterface.HasToolAsReference)
                {
                    toolReferences = _dataInterface.LoadToolReferenceLinks(removedItem.ListId);
                    if (toolReferences != null && toolReferences.Count > 0)
                    {
                        hasReferences = true;
                    }
                }

                var locationToolReferences = new List<LocationToolAssignment>();
                if (_dataInterface.HasLocationToolAssignmentAsReference)
                {
                    var referencedLocationToolAssignmentIds = _dataInterface.LoadReferencedLocationToolAssignmentIds(removedItem.ListId);
                    locationToolReferences = _locationToolAssignmentData.GetLocationToolAssignmentsByIds(referencedLocationToolAssignmentIds);
                    if (locationToolReferences != null && locationToolReferences.Count > 0)
                    {
                        hasReferences = true;
                    }
                }

                if (hasReferences)
                {
                    errorGui.ShowRemoveHelperTableItemPreventingReferences(toolModelReferenceLinks, toolReferences, locationToolReferences);
                    return;
                }
                _dataInterface.RemoveItem(removedItem, _userGetter.GetCurrentUser());
                _guiInterface.Remove(removedItem);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception exception)
            {
                Log.Error($"Error while Removing HelperTableItem with ListId:{removedItem?.ListId?.ToLong()} and type {typeof(T)}", exception);
                errorGui.ShowErrorMessage();
            }
        }

        public void SaveItem(T oldItem, T changedItem, IHelperTableErrorGui<T> errorGui)
        {
            try
            {
                _dataInterface.SaveItem(oldItem, changedItem, _userGetter.GetCurrentUser());
                _guiInterface.Save(changedItem);
                _notificationManager.SendSuccessNotification();
            }
            catch (EntryAlreadyExists entryAlreadyExists)
            {
                Log.Error($"Entry already Exists for HelperTableItem with old Item ListId:{oldItem?.ListId?.ToLong()} and type {typeof(T)}", entryAlreadyExists);
                errorGui.ShowEntryAlreadyExists(changedItem);
            }
            catch (Exception exception)
            {
                Log.Error($"Error while Saving HelperTableItem with old Item ListId:{oldItem?.ListId?.ToLong()} and type {typeof(T)}", exception);
                errorGui.ShowErrorMessage();
            }
        }

        public void LoadReferences(HelperTableEntityId id, IHelperTableShowReferencesGui showReferencesGui)
        {
            if (_dataInterface.HasToolModelAsReference)
            {
                Log.Info("LoadReferencedToolModels started");
                try
                {
                    var referncedToolModels = _dataInterface.LoadReferencedToolModels(id);
                    Log.Debug($"{referncedToolModels.Count} referenced ToolModels loaded");
                    showReferencesGui.ShowReferencedToolModels(referncedToolModels);
                }
                catch (Exception exception)
                {
                    Log.Error($"Error while loading the ToolModels", exception);
                    showReferencesGui.ShowReferencesError();
                }

                Log.Info("LoadReferencedToolModels ended");
            }

            if (_dataInterface.HasToolAsReference)
            {
                try
                {
                    showReferencesGui.ShowToolReferenceLinks(_dataInterface.LoadToolReferenceLinks(id));
                }
                catch (Exception exception)
                {
                    Log.Error($"Error while loading the Tools", exception);
                    showReferencesGui.ShowReferencesError();
                }
            }

            if (_dataInterface.HasLocationToolAssignmentAsReference)
            {
                try
                {
                    var locationtoolAssignmentIds = _dataInterface.LoadReferencedLocationToolAssignmentIds(id);
                    var assignments =
                        _locationToolAssignmentData.GetLocationToolAssignmentsByIds(locationtoolAssignmentIds);
                    showReferencesGui.ShowReferencedLocationToolAssignments(assignments);
                }
                catch (Exception exception)
                {
                    Log.Error($"Error while loading the LocationToolAssignments", exception);
                    showReferencesGui.ShowReferencesError();
                }
            }
        }

        public HelperTableUseCase(
            IHelperTableData<T> dataInterface,
            IHelperTableGui<T> guiInterface,
            ISessionInformationUserGetter userGetter,
            INotificationManager notificationManager,
            ILocationToolAssignmentData locationToolAssignmentData = null)
        {
            _dataInterface = dataInterface;
            _guiInterface = guiInterface;
            _locationToolAssignmentData = locationToolAssignmentData;
            _userGetter = userGetter;
            _notificationManager = notificationManager;
        }
    }

    public class HelperTableUseCaseHumbleAsyncRunner<T> : IHelperTableUseCase<T> where T : HelperTableEntity
    {
        public HelperTableUseCaseHumbleAsyncRunner(IHelperTableUseCase<T> real)
        {
            _real = real;
        }

        public void LoadItems(IHelperTableReadOnlyErrorGui<T> errorGui)
        {
            Task.Run(() => _real.LoadItems(errorGui));
        }

        public void AddItem(T newItem, IHelperTableErrorGui<T> errorGui)
        {
            Task.Run(() => _real.AddItem(newItem, errorGui));
        }

        public void RemoveItem(T removedItem, IHelperTableErrorGui<T> errorGui)
        {
            Task.Run(() => _real.RemoveItem(removedItem, errorGui));
        }

        public void SaveItem(T oldItem, T changedItem, IHelperTableErrorGui<T> errorGui)
        {
            Task.Run(() => _real.SaveItem(oldItem, changedItem, errorGui));
        }

        public void LoadReferences(HelperTableEntityId id, IHelperTableShowReferencesGui errorGui)
        {
            Task.Run(() => _real.LoadReferences(id, errorGui));
        }

        private readonly IHelperTableUseCase<T> _real;
    }
}
