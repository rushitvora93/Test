using System.Collections.Generic;
using Core.UseCases;

namespace InterfaceAdapters
{
    public class SaveColumnsInterfaceAdapter : InterfaceAdapter<ISaveColumnsGui>, ISaveColumnsGui
    {
        public void UpdateColumnWidths(string gridName, List<(string, double)> columns)
        {
            InvokeActionOnGuiInterfaces(gui => gui.UpdateColumnWidths(gridName, columns));
        }
        
        public void ShowSaveColumnError(string gridName)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowSaveColumnError(gridName));
        }

        public void ShowColumnWidths(string gridName, List<(string, double)> columns)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowColumnWidths(gridName, columns));
        }

        public void ShowLoadColumnWidthsError(string gridName)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowLoadColumnWidthsError(gridName));
        }
    }
}
