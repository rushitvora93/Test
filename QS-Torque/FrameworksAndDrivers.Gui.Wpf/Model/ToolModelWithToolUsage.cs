using Core.Entities;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.Model
{
    public class ToolModelWithToolUsage
    {
        public InterfaceAdapters.Models.ToolModel ToolModel { get; set; }
        public HelperTableItemModel<ToolUsage, string> ToolUsage { get; set; }

        public ToolModelWithToolUsage(InterfaceAdapters.Models.ToolModel toolModel, HelperTableItemModel<ToolUsage, string> toolUsage)
        {
            ToolModel = toolModel;
            ToolUsage = toolUsage;
        }
    }
}