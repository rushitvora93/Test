using Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.ReferenceLink;
using log4net;

namespace Core.UseCases
{
    public interface IChangeToolStateForLocationGui
    {
        void ShowLocationsForTools(Dictionary<Tool, List<LocationReferenceLink>> locationsForTools);
        void ShowErrorForLoadLocationsForTools();

        void ShowErrorForSaveToolStates();
    }

    public interface IChangeToolStateForLocationUseCase
    {
        void LoadLocationsForTools(List<Tool> lists);
        void SetNewToolStates(List<ToolDiff> tools);
    }

    public class ChangeToolStateForLocationUseCase : IChangeToolStateForLocationUseCase
    {
        private ILocationToolAssignmentData _locationToolAssignmentData;
        private IToolData _toolData;
        private IChangeToolStateForLocationGui _gui;
        private readonly ISessionInformationUserGetter _userGetter;
        private readonly ILog log = LogManager.GetLogger(typeof(ChangeToolStateForLocationUseCase));

        public ChangeToolStateForLocationUseCase(ILocationToolAssignmentData locationToolAssignmentData, IToolData toolData, 
            IChangeToolStateForLocationGui gui, ISessionInformationUserGetter userGetter)
        {
            _locationToolAssignmentData = locationToolAssignmentData;
            _toolData = toolData;
            _gui = gui;
            _userGetter = userGetter;
        }

        public void LoadLocationsForTools(List<Tool> lists)
        {
            if (lists is null)
            {
                throw new ArgumentNullException(nameof(lists), "List cannot be null");
            }

            try
            {
                Dictionary<Tool, List<LocationReferenceLink>> locationsReferenceLinksForTools =
                    new Dictionary<Tool, List<LocationReferenceLink>>();
                foreach (Tool tool in lists)
                {
                    List<LocationReferenceLink> locationReferenceLinks =
                        _locationToolAssignmentData.LoadLocationReferenceLinksForTool(tool.Id);

                    locationsReferenceLinksForTools.Add(tool,
                        locationReferenceLinks ?? new List<LocationReferenceLink>());
                }

                _gui.ShowLocationsForTools(locationsReferenceLinksForTools);

            }
            catch (Exception exception)
            {
                log.Error("Exception in LoadLocationsForTools", exception);
                _gui.ShowErrorForLoadLocationsForTools();
            }
        }

        public void SetNewToolStates(List<ToolDiff> tools)
        {
            try
            {
                tools?.ForEach(x => x.User = _userGetter.GetCurrentUser());
                tools?.ForEach(x => _toolData.UpdateTool(x));
            }
            catch (Exception  exception)
            {
                log.Error("Exception in SetNewToolStates", exception);
                _gui.ShowErrorForSaveToolStates();
            }
        }
    }

    public class ChangeToolStateForLocationUseCaseHumbleAsyncRunner : IChangeToolStateForLocationUseCase
    {
        private IChangeToolStateForLocationUseCase _real;

        public ChangeToolStateForLocationUseCaseHumbleAsyncRunner(IChangeToolStateForLocationUseCase real)
        {
            _real = real;
        }
        public void LoadLocationsForTools(List<Tool> lists)
        {
            Task.Run(() => _real.LoadLocationsForTools(lists));
        }

        public void SetNewToolStates(List<ToolDiff> tools)
        {
            Task.Run(() => _real.SetNewToolStates(tools));
        }
    }
}
