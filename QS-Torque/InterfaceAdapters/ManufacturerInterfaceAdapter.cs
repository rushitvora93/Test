using Core.Entities;
using Core.UseCases;
using System.Collections.Generic;
using Core.Entities.ReferenceLink;

namespace InterfaceAdapters
{
    public class ManufacturerInterfaceAdapter : InterfaceAdapter<IManufacturerGui>, IManufacturerGui
    {
        public void AddManufacturer(Manufacturer manufacturer)
        {
            InvokeActionOnGuiInterfaces(gui => gui.AddManufacturer(manufacturer));
        }

        public void RemoveManufacturer(Manufacturer manufacturer)
        {
            InvokeActionOnGuiInterfaces(gui => gui.RemoveManufacturer(manufacturer));
        }

        public void SaveManufacturer(Manufacturer manufacturer)
        {
            InvokeActionOnGuiInterfaces(gui => gui.SaveManufacturer(manufacturer));
        }

        public void ShowComment(Manufacturer manufacturer, string comment)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowComment(manufacturer, comment));
        }

        public void ShowCommentError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowCommentError());
        }

        public void ShowErrorMessage()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowErrorMessage());
        }

        public void ShowManufacturer(List<Manufacturer> manufacturer)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowManufacturer(manufacturer));
        }

        public void ShowReferencedToolModels(List<ToolModelReferenceLink> toolModels)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowReferencedToolModels(toolModels));
        }

        public void ShowReferencesError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowReferencesError());
        }

        public void ShowRemoveManufacturerPreventingReferences(List<ToolModelReferenceLink> references)
        {
            //Intentionally empty gets called by active parameter in 
        }
    }
}
