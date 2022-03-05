using System.Collections.Generic;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;

namespace InterfaceAdapters
{
    public class ToleranceClassInterfaceAdapter : InterfaceAdapter<IToleranceClassGui>, IToleranceClassGui
    {
        public void ShowToleranceClasses(List<ToleranceClass> toleranceClasses)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowToleranceClasses(toleranceClasses));
        }

        public void ShowToleranceClassesError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowToleranceClassesError());
        }

        public void RemoveToleranceClass(ToleranceClass toleranceClass)
        {
            InvokeActionOnGuiInterfaces(gui => gui.RemoveToleranceClass(toleranceClass));
        }

        public void RemoveToleranceClassError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.RemoveToleranceClassError());
        }

        public void AddToleranceClass(ToleranceClass toleranceClass)
        {
            InvokeActionOnGuiInterfaces(gui => gui.AddToleranceClass(toleranceClass));
        }

        public void AddToleranceClassError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.AddToleranceClassError());
        }

        public void UpdateToleranceClass(ToleranceClass toleranceClass)
        {
            InvokeActionOnGuiInterfaces(gui => gui.UpdateToleranceClass(toleranceClass));
        }

        public void SaveToleranceClassError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.SaveToleranceClassError());
        }

        public void ShowReferencedLocations(List<LocationReferenceLink> locationReferenceLinks)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowReferencedLocations(locationReferenceLinks));
        }

        public void ShowReferencesError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowReferencesError());
        }

        public void ShowReferencedLocationToolAssignments(List<LocationToolAssignment> assignments)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowReferencedLocationToolAssignments(assignments));
        }

        public void ShowRemoveToleranceClassPreventingReferences(List<LocationReferenceLink> referencedLocations, List<LocationToolAssignment> referencedLocationToolAssignments)
        {
            //Intentionally empty gets called by active Parameter in RemoveToleranceClass
        }
    }
}
