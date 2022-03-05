using System.Collections.Generic;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;

namespace StartUp
{
    public class ChangeToolStateForLocationGuiProxy : IChangeToolStateForLocationGui
    {
        public IChangeToolStateForLocationGui Real { get; set; }
        public void ShowLocationsForTools(Dictionary<Tool, List<LocationReferenceLink>> locationsForTools)
        {
            Real.ShowLocationsForTools(locationsForTools);
        }

        public void ShowErrorForLoadLocationsForTools()
        {
            Real.ShowErrorForLoadLocationsForTools();
        }

        public void ShowErrorForSaveToolStates()
        {
            Real.ShowErrorForSaveToolStates();
        }
    }
}