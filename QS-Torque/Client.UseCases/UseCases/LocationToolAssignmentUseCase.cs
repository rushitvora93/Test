using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.UseCases.UseCases;
using Core.Diffs;
using Core.Entities;
using Core.Entities.ReferenceLink;
using log4net;

namespace Core.UseCases
{
    public interface ILocationToolAssignmentErrorHandler
    {
        void ShowLocationToolAssignmentError();
    }

    public interface ILocationToolAssignmentDiffShower
    {
        void ShowDiffDialog(List<LocationToolAssignmentDiff> diff, Action saveAction);
    }

    public interface ILocationToolAssignmentData
    {
        List<LocationToolAssignment> LoadLocationToolAssignments();
        List<LocationToolAssignment> GetLocationToolAssignmentsByIds(List<LocationToolAssignmentId> ids);
        void AssignToolToLocation(LocationToolAssignment assignment, User user);
        List<LocationToolAssignment> LoadAssignedToolsForLocation(LocationId locationId);
        void AddTestConditions(LocationToolAssignment assignment, User user);
        List<ToolUsage> LoadUnusedToolUsagesForLocation(LocationId locationId);
        void RemoveLocationToolAssignment(LocationToolAssignment assignment, User user);
        void RestoreLocationToolAssignment(LocationToolAssignment assignment, User user);
        List<LocationReferenceLink> LoadLocationReferenceLinksForTool(ToolId toolId);
        void UpdateLocationToolAssignment(List<LocationToolAssignmentDiff> diff);
    }

    public interface ILocationToolAssignmentGui
    {
        void LoadLocationToolAssignments(List<LocationToolAssignment> locationToolAssignments);
        void ShowLocationToolAssignmentError();
        void AssignToolToLocation(LocationToolAssignment assignment);
        void AssignToolToLocationError();
        void ShowAssignedToolsForLocation(List<LocationToolAssignment> assignments);
        void LoadAssignedToolsForLocationError();
        void AddTestConditions(LocationToolAssignment assignment);
        void AddTestConditionsError();
        void ShowUnusedToolUsagesForLocation(List<ToolUsage> toolUsages, LocationId locationId);
        void LoadUnusedToolUsagesForLocationError();
        void RemoveLocationToolAssignment(LocationToolAssignment assignment);
        void RemoveLocationToolAssignmentError();
        void ShowLocationReferenceLinksForTool(List<LocationReferenceLink> locationReferenceLinks);
        void UpdateLocationToolAssignment(List<LocationToolAssignment> updatedLocationToolAssignment);
        void UpdateLocationToolAssignmentError();
    }

    public interface ILocationToolAssignmentUseCase
    {
        void LoadLocationToolAssignments();
        void AssignToolToLocation(LocationToolAssignment assignment);
        void LoadToolAssignmentsForLocation(Location location);
        void AddTestConditions(LocationToolAssignment assignment);
        void LoadUnusedToolUsagesForLocation(LocationId locationid);
        void RemoveLocationToolAssignment(LocationToolAssignment assignment);
        void LoadLocationReferencesForTool(ToolId toolId);
        void UpdateLocationToolAssignment(List<LocationToolAssignmentDiff> diff, ILocationToolAssignmentErrorHandler errorHandler = null, ILocationToolAssignmentDiffShower diffShower = null);
    }

    public class LocationToolAssignmentUseCase : ILocationToolAssignmentUseCase
    {
        private ILocationToolAssignmentData _data;
        private ILocationToolAssignmentGui _gui;
        private ISessionInformationUserGetter _userGetter;
        private readonly INotificationManager _notificationManager;
        private readonly ITestDateCalculationUseCase _testDateCalculationUseCase;
        private static readonly ILog _log = LogManager.GetLogger(typeof(LocationToolAssignment));


        public LocationToolAssignmentUseCase(ILocationToolAssignmentData data, ILocationToolAssignmentGui gui, ISessionInformationUserGetter userGetter, INotificationManager notificationManager, ITestDateCalculationUseCase testDateCalculationUseCase)
        {
            _data = data;
            _gui = gui;
            _userGetter = userGetter;
            _notificationManager = notificationManager;
            _testDateCalculationUseCase = testDateCalculationUseCase;
        }


        public void LoadLocationToolAssignments()
        {
            try
            {
                _gui.LoadLocationToolAssignments(_data.LoadLocationToolAssignments());
            }
            catch (Exception e)
            {
                _log.Error("Error in LoadLocationToolAssignments", e);
                _gui.ShowLocationToolAssignmentError();
            }
        }

        public void AssignToolToLocation(LocationToolAssignment assignment)
        {
            try
            {
                _data.AssignToolToLocation(assignment, _userGetter.GetCurrentUser());
                _gui.AssignToolToLocation(assignment);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception ex)
            {
                _log.Error("Error while assigning a tool to a location", ex);
                _gui.AssignToolToLocationError();
            }
        }

        public virtual void LoadToolAssignmentsForLocation(Location location)
        {
            try
            {
                var assignments = _data.LoadAssignedToolsForLocation(location.Id);
                assignments.ForEach(x => location.UpdateWith(x.AssignedLocation));
                _gui.ShowAssignedToolsForLocation(assignments);
            }
            catch (Exception ex)
            {
                _log.Error("Error while loading the assigned tools of a location", ex);
                _gui.LoadAssignedToolsForLocationError();
            }
        }

        public void AddTestConditions(LocationToolAssignment assignment)
        {
            try
            {
                _data.AddTestConditions(assignment, _userGetter.GetCurrentUser());
                _gui.AddTestConditions(assignment);
                _notificationManager.SendSuccessNotification();

                if(FeatureToggles.FeatureToggles.TestDateCalculation)
                {
                    _testDateCalculationUseCase.CalculateToolTestDateFor(new List<LocationToolAssignmentId>(){ assignment.Id });
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error while adding test conditions to location tool assignment", ex);
                _gui.AddTestConditionsError();
            }
        }

        public void LoadUnusedToolUsagesForLocation(LocationId locationId)
        {
            try
            {
                var toolUsages = _data.LoadUnusedToolUsagesForLocation(locationId);
                _gui.ShowUnusedToolUsagesForLocation(toolUsages, locationId);
            }
            catch (Exception ex)
            {
                _log.Error("Error while loading unused tool usages for the Location with Id " + locationId.ToLong(), ex);
                _gui.LoadUnusedToolUsagesForLocationError();
            }
        }

        public void RemoveLocationToolAssignment(LocationToolAssignment assignment)
        {
            try
            {
                _data.RemoveLocationToolAssignment(assignment, _userGetter.GetCurrentUser());
                _gui.RemoveLocationToolAssignment(assignment);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception ex)
            {
                _log.Error("Error occured while removing a LocationToolAssignment", ex);
                _gui.RemoveLocationToolAssignmentError();
            }
        }

        public void LoadLocationReferencesForTool(ToolId toolId)
        {
            try
            {
                List<LocationReferenceLink> locationReferenceLinks = _data.LoadLocationReferenceLinksForTool(toolId);
                _gui.ShowLocationReferenceLinksForTool(locationReferenceLinks);
            }
            catch (Exception exception)
            {
                _log.Error("Error while loading LocationReferenceLinks", exception);
                //TODO: error handling
            }
            
        }

        public void UpdateLocationToolAssignment(List<LocationToolAssignmentDiff> diffs, ILocationToolAssignmentErrorHandler errorHandler = null, ILocationToolAssignmentDiffShower diffShower = null)
        {
            if (diffs is null || diffs.Count == 0)
            {
                throw new ArgumentNullException(nameof(diffs), "LocationToolAssignmentDiff cant be null or empty");
            }
            try
            {
                diffs.ForEach(x => x.User = _userGetter.GetCurrentUser());

                if (diffShower != null)
                {
                    diffShower.ShowDiffDialog(diffs, () =>
                    {
                        _data.UpdateLocationToolAssignment(diffs);
                        _gui.UpdateLocationToolAssignment(diffs.Select(x => x.NewLocationToolAssignment).ToList());
                        _notificationManager.SendSuccessNotification(diffs.Count);

                        if (FeatureToggles.FeatureToggles.TestDateCalculation)
                        {
                            _testDateCalculationUseCase.CalculateToolTestDateFor(diffs.Select(x => x.NewLocationToolAssignment.Id).ToList());
                            LoadLocationToolAssignments();
                        }
                    });
                }
                else
                {
                    _data.UpdateLocationToolAssignment(diffs);
                    _gui.UpdateLocationToolAssignment(diffs.Select(x => x.NewLocationToolAssignment).ToList());
                    _notificationManager.SendSuccessNotification(diffs.Count);

                    if (FeatureToggles.FeatureToggles.TestDateCalculation)
                    {
                        _testDateCalculationUseCase.CalculateToolTestDateFor(diffs.Select(x => x.NewLocationToolAssignment.Id).ToList());
                        LoadLocationToolAssignments();
                    } 
                }
            }
            catch (Exception exception)
            {
                _log.Error("Error while updating LocationToolAssignment", exception);
                if(errorHandler != null)
                {
                    errorHandler.ShowLocationToolAssignmentError();
                }
                else
                {
                    _gui.UpdateLocationToolAssignmentError(); 
                }
            }
        }
    }

    public class LocationToolAssignmentUseCaseHumbleAsyncRunner : ILocationToolAssignmentUseCase
    {
        private ILocationToolAssignmentUseCase _real;
        public LocationToolAssignmentUseCaseHumbleAsyncRunner(ILocationToolAssignmentUseCase real)
        {
            _real = real;
        }

        public void LoadLocationToolAssignments()
        {
            Task.Run(() => _real.LoadLocationToolAssignments());
        }

        public void AssignToolToLocation(LocationToolAssignment assignment)
        {
            Task.Run(() => _real.AssignToolToLocation(assignment));
        }

        public void LoadToolAssignmentsForLocation(Location location)
        {
            Task.Run(() => _real.LoadToolAssignmentsForLocation(location));
        } 

        public void AddTestConditions(LocationToolAssignment assignment)
        {
            Task.Run(() => _real.AddTestConditions(assignment));
        }

        public void LoadUnusedToolUsagesForLocation(LocationId locationid)
        {
            Task.Run(() => _real.LoadUnusedToolUsagesForLocation(locationid));
        }

        public void RemoveLocationToolAssignment(LocationToolAssignment assignment)
        {
            Task.Run(() => _real.RemoveLocationToolAssignment(assignment));
        }

        public void LoadLocationReferencesForTool(ToolId toolId)
        {
            Task.Run(() => _real.LoadLocationReferencesForTool(toolId));
        }

        public void UpdateLocationToolAssignment(List<LocationToolAssignmentDiff> diff, ILocationToolAssignmentErrorHandler errorHandler = null, ILocationToolAssignmentDiffShower diffShower = null)
        {
            Task.Run(() => _real.UpdateLocationToolAssignment(diff, errorHandler, diffShower));
        }
    }
}