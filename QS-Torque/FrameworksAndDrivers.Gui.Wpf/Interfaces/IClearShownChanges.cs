using System;

namespace FrameworksAndDrivers.Gui.Wpf.Interfaces
{
    /// <summary>
    /// Forces a ViewModel to implement an event to clear the shown changes in the view
    /// </summary>
    public interface IClearShownChanges
    {
        /// <summary>
        /// All shown changes in the gui/view (e. g. blue border of TextBox) are reverted
        /// </summary>
        event EventHandler ClearShownChanges;
    }
}
