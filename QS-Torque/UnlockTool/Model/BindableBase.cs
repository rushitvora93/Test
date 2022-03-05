using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FrameworksAndDrivers.Gui.Wpf
{
    /// <summary>
    /// Base class for ViewModels
    /// Adds the ability to use get => Set(ref variable, value)
    /// instead of get { variable = value; RaisePropertyChanged(propertyName)}
    /// </summary>
    public class BindableBase : INotifyPropertyChanged
    {
        protected void Set<T>(ref T target, T value, [CallerMemberName] string propertyName = "")
        {
            target = value;
            RaisePropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void PropertyChangedChaining(object sender, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
    }
}
