using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.ReferenceLink;
using log4net;

namespace Core.UseCases
{

    public interface IToleranceClassGui
    {
        void ShowToleranceClasses(List<ToleranceClass> toleranceClasses);
        void ShowToleranceClassesError();
        void RemoveToleranceClass(ToleranceClass toleranceClass);
        void RemoveToleranceClassError();
        void AddToleranceClass(ToleranceClass toleranceClass);
        void AddToleranceClassError();
        void UpdateToleranceClass(ToleranceClass toleranceClass);
        void SaveToleranceClassError();
        void ShowReferencedLocations(List<LocationReferenceLink> locationReferenceLinks);
        void ShowReferencesError();
        void ShowReferencedLocationToolAssignments(List<LocationToolAssignment> assignments);

        void ShowRemoveToleranceClassPreventingReferences(List<LocationReferenceLink> referencedLocations, List<LocationToolAssignment> referencedLocationToolAssignments);
    }

    public interface IToleranceClassData
    {
        List<ToleranceClass> LoadToleranceClasses();
        void RemoveToleranceClass(ToleranceClassDiff toleranceClass);
        ToleranceClass AddToleranceClass(ToleranceClass toleranceClass, User byUser);
        ToleranceClass SaveToleranceClass(ToleranceClassDiff toleranceClass);
        List<LocationReferenceLink> LoadReferencedLocations(ToleranceClassId id);
        List<LocationToolAssignmentId> LoadReferencedLocationToolAssignments(ToleranceClassId id);
        Dictionary<long, ToleranceClass> GetToleranceClassFromHistoryForIds(List<Tuple<long, long, DateTime>> idsWithClassData);
    }

    public interface IToleranceClassUseCase
    {
        void LoadToleranceClasses();
        void AddToleranceClass(ToleranceClass toleranceClass);
        void RemoveToleranceClass(ToleranceClass toleranceClass, IToleranceClassGui active);
        void SaveToleranceClass(ToleranceClass newToleranceClass, ToleranceClass oldToleranceClass);
        void LoadReferencedLocations(ToleranceClassId id);
        void LoadReferencedLocationToolAssignments(ToleranceClassId id);
    }


    public class ToleranceClassUseCase : IToleranceClassUseCase
    {
        private IToleranceClassGui _guiInterface;
        private IToleranceClassData _dataInterface;
        private ISessionInformationUserGetter _userGetter;
        private ILocationToolAssignmentData _locationToolData;
        private readonly INotificationManager _notificationManager;

        private static readonly ILog Log = LogManager.GetLogger(typeof(ToleranceClassUseCase));

        public ToleranceClassUseCase(IToleranceClassGui guiInterface, IToleranceClassData dataInterface, ISessionInformationUserGetter userGetter, ILocationToolAssignmentData locationToolData, INotificationManager notificationManager)
        {
            _guiInterface = guiInterface;
            _dataInterface = dataInterface;
            _userGetter = userGetter;
            _locationToolData = locationToolData;
            _notificationManager = notificationManager;
        }

        public virtual void LoadToleranceClasses()
        {
            Log.Info("LoadToleranceClasses started");
            try
            {
                var toleranceClasses = _dataInterface.LoadToleranceClasses();
                Log.Debug($"LoadToleranceClasses call with List of Tolerance classes with size of {toleranceClasses?.Count}");
                _guiInterface.ShowToleranceClasses(toleranceClasses);
            }
            catch (Exception exception)
            {
                Log.Error("Error while Loading Tolerance classes failed with Exception", exception);
                _guiInterface.ShowToleranceClassesError();
            }
            Log.Info("LoadToleranceClasses ended");
        }

        public virtual void AddToleranceClass(ToleranceClass toleranceClass)
        {
            Log.Info("AddToleranceClass started");
            Log.Debug($"AddToleranceClass called with Tolerance class: {LogToleranceClass(toleranceClass)}");
            try
            {
                var addedToleranceClass = _dataInterface.AddToleranceClass(toleranceClass, _userGetter.GetCurrentUser());
                Log.Debug($"AddToleranceClass call with Tolerance class: {LogToleranceClass(toleranceClass)}");
                _guiInterface.AddToleranceClass(addedToleranceClass);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception exception)
            {
                Log.Error($"Error while adding Tolerance class: {LogToleranceClass(toleranceClass)}", exception);
                _guiInterface.AddToleranceClassError();
            }
            Log.Info("AddToleranceClass ended");
        }

        public virtual void RemoveToleranceClass(ToleranceClass toleranceClass, IToleranceClassGui active)
        {
            Log.Info("RemoveToleranceClass started");
            Log.Debug($"RemoveToleranceClass called with Tolerance class: {LogToleranceClass(toleranceClass)}");
            try
            {
                bool hasReferences = false;
                List<LocationReferenceLink> locationReferences = null;
                List<LocationToolAssignment> locationToolAssignments = null;

                locationReferences = _dataInterface.LoadReferencedLocations(toleranceClass.Id);
                if (locationReferences != null && locationReferences.Count > 0)
                {
                    hasReferences = true;
                }
                    
                var assignmentIds = _dataInterface.LoadReferencedLocationToolAssignments(toleranceClass.Id);

                if (assignmentIds != null && assignmentIds.Count > 0)
                {
                    hasReferences = true;
                    locationToolAssignments = _locationToolData.GetLocationToolAssignmentsByIds(assignmentIds);
                }

                if (hasReferences)
                {
                    active.ShowRemoveToleranceClassPreventingReferences(locationReferences, locationToolAssignments);
                    return;
                }

                _dataInterface.RemoveToleranceClass(new ToleranceClassDiff(_userGetter.GetCurrentUser(), new HistoryComment(""), toleranceClass, toleranceClass));
                _guiInterface.RemoveToleranceClass(toleranceClass);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception exception)
            {
                Log.Error(
                    $"Error while removing Tolerance class: {LogToleranceClass(toleranceClass)}",
                    exception);
                _guiInterface.RemoveToleranceClassError();
            }

            Log.Info("RemoveToleranceClass ended");
        }

        public virtual void SaveToleranceClass(ToleranceClass newToleranceClass, ToleranceClass oldToleranceClass)
        {
            Log.Info("SaveToleranceClass started");
            Log.Debug($"SaveToleranceClass called with Tolerance class: {LogToleranceClass(newToleranceClass)}");
            try
            {
                var updateToleranceClass = _dataInterface.SaveToleranceClass(new ToleranceClassDiff(_userGetter.GetCurrentUser(), new HistoryComment(""), oldToleranceClass, newToleranceClass));
                _guiInterface.UpdateToleranceClass(updateToleranceClass);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception exception)
            {
                Log.Error($"Error while saving Tolerance class: {LogToleranceClass(newToleranceClass)}",exception);
                _guiInterface.SaveToleranceClassError();
            }
        }

        public virtual void LoadReferencedLocations(ToleranceClassId id)
        {
            try
            {
                _guiInterface.ShowReferencedLocations(_dataInterface.LoadReferencedLocations(id));
            }
            catch (Exception exception)
            {
                Log.Error($"Error while loading the referenced locations for a ToleranceClass with Id {id.ToLong()}", exception);
                _guiInterface.ShowReferencesError();
            }
        }

        public virtual void LoadReferencedLocationToolAssignments(ToleranceClassId id)
        {
            try
            {
                var resultIds = _dataInterface.LoadReferencedLocationToolAssignments(id);
                var resultAssignments = _locationToolData.GetLocationToolAssignmentsByIds(resultIds);
                _guiInterface.ShowReferencedLocationToolAssignments(resultAssignments);
            }
            catch (Exception exception)
            {
                Log.Error($"Error while loading the referenced location tool assignments for a ToleranceClass with Id {id.ToLong()}", exception);
                _guiInterface.ShowReferencesError();
            }
        }

        private string LogToleranceClass(ToleranceClass toleranceClass)
        {
            return $"{nameof(toleranceClass.Id)}: {toleranceClass.Id}, " +
                   $"{nameof(toleranceClass.Name)}: {toleranceClass.Name}, " +
                   $"{nameof(toleranceClass.Relative)}: {toleranceClass.Relative}, " +
                   $"{nameof(toleranceClass.LowerLimit)}: {toleranceClass.LowerLimit}, " +
                   $"{nameof(toleranceClass.UpperLimit)}: {toleranceClass.UpperLimit}";
        }
    }


    public class ToleranceClassHumbleAsyncRunner : IToleranceClassUseCase
    {
        private IToleranceClassUseCase _real;

        public ToleranceClassHumbleAsyncRunner(IToleranceClassUseCase real)
        {
            _real = real;
        }


        public void AddToleranceClass(ToleranceClass toleranceClass)
        {
            Task.Run(() => _real.AddToleranceClass(toleranceClass));
        }

        public void LoadReferencedLocations(ToleranceClassId id)
        {
            Task.Run(() => _real.LoadReferencedLocations(id));
        }

        public void LoadReferencedLocationToolAssignments(ToleranceClassId id)
        {
            Task.Run(() => _real.LoadReferencedLocationToolAssignments(id));
        }

        public void LoadToleranceClasses()
        {
            Task.Run(() => _real.LoadToleranceClasses());
        }

        public void RemoveToleranceClass(ToleranceClass toleranceClass, IToleranceClassGui active)
        {
            Task.Run(() => _real.RemoveToleranceClass(toleranceClass, active));
        }

        public void SaveToleranceClass(ToleranceClass newToleranceClass, ToleranceClass oldToleranceClass)
        {
            Task.Run(() => _real.SaveToleranceClass(newToleranceClass, oldToleranceClass));
        }
    }
}