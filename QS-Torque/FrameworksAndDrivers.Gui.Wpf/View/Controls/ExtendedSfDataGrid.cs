using System.ComponentModel;
using System.Runtime.CompilerServices;
using InterfaceAdapters.Localization;
using Syncfusion.UI.Xaml.Grid;

namespace FrameworksAndDrivers.Gui.Wpf.View.Controls
{
    public class ExtendedSfDataGrid : SfDataGrid, INotifyPropertyChanged, IGetUpdatedByLanguageChanges
    {

        private ILocalizationWrapper _localizationWrapper;
        public ILocalizationWrapper LocalizationWrapper
        {
            private get => _localizationWrapper;
            set
            {
                _localizationWrapper = value;
                LanguageUpdate();
            }
        }

        private string _sortAscendingLocalization;

        public string SortAscendingLocalization
        {
            get { return _sortAscendingLocalization; }
            set
            {
                _sortAscendingLocalization = value;
                RaiseNotifyPropertyChanged();
            }
        }

        private string _sortDescendingLocalization;

        public string SortDescendingLocalization
        {
            get { return _sortDescendingLocalization; }
            set
            {
                _sortDescendingLocalization = value; 
                RaiseNotifyPropertyChanged();
            }
        }

        private string _clearFilterLocalization;

        public string ClearFilterLocalization
        {
            get { return _clearFilterLocalization; }
            set
            {
                _clearFilterLocalization = value;
                RaiseNotifyPropertyChanged();
            }
        }

        private string _okLocalization;

        public string OkLocalization
        {
            get => _okLocalization;
            set
            {
                _okLocalization = value;
                RaiseNotifyPropertyChanged();
            }
        }

        private string _cancelLocalization;

        public string CancelLocalization
        {
            get { return _cancelLocalization; }
            set
            {
                _cancelLocalization = value;
                RaiseNotifyPropertyChanged();
            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaiseNotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LanguageUpdate()
        {
            SortAscendingLocalization = LocalizationWrapper.Strings.GetString("Sort ascending");
            SortDescendingLocalization = LocalizationWrapper.Strings.GetString("Sort descending");
            ClearFilterLocalization = LocalizationWrapper.Strings.GetString("clear filter");
            OkLocalization = LocalizationWrapper.Strings.GetString("ok");
            CancelLocalization = LocalizationWrapper.Strings.GetString("Cancel");
        }
    }
}
