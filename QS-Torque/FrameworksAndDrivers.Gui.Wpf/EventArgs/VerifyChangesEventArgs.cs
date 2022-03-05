using System.Collections.Generic;
using System.Windows;
using FrameworksAndDrivers.Gui.Wpf.Model;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.EventArgs
{
    public class VerifyChangesEventArgs
    {
        public List<SingleValueChangeModel> ChangedValues { get; private set; }
        public string Comment { get; set; } = "";
        public MessageBoxResult Result { get; set; }


        public VerifyChangesEventArgs(List<SingleValueChangeModel> changedValues)
        {
            ChangedValues = changedValues;
        }
    }
}
