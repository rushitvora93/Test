using System;

namespace FrameworksAndDrivers.Gui.Wpf.EventArgs
{
    public class FileDialogEventArgs
    {
        public Action<string> ResultAction { get; private set; }

        public FileDialogEventArgs(Action<string> resultAction)
        {
            ResultAction = resultAction;
        }
    }
}
