using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using System;

namespace TestHelper.Mock
{
    public class ClearShownChangesMock : IClearShownChanges
    {
        public event EventHandler ClearShownChanges;

        public void InvokeClearShownChanges()
        {
            ClearShownChanges?.Invoke(this, null);
        }
    }
}
