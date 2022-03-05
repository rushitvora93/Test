using Core.Entities;
using Core.UseCases;
using System;
using System.Collections.Generic;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels.Mock
{
    public class ChangeToolStateForLocationUseCaseMock : IChangeToolStateForLocationUseCase
    {
        public int SetNewToolStatesCall { get; set; }

        public ChangeToolStateForLocationUseCaseMock(ILocationToolAssignmentData locationToolAssignmentData, IToolData toolData, IChangeToolStateForLocationGui gui)
        {
        }


        public void SetNewToolStates(List<ToolDiff> tools)
        {
            SetNewToolStatesCall++;
        }

        public void LoadLocationsForTools(List<Tool> lists)
        {
            throw new NotImplementedException();
        }
    }
}