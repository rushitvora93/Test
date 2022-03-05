using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.View.Dialogs
{
    public class ReferenceList
    {
        public string Header { get; set; }
        public List<string> References;
    }

    public class ReferenceItem
    {
        public string Header { get; set; }
        public string Value { get; set; }
    }

    /// <summary>
    /// Interaction logic for ViewReferencesDialog.xaml
    /// </summary>
    public partial class ViewReferencesDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<ReferenceItem> _referenceList;
        public ObservableCollection<ReferenceItem> ReferenceList
        {
            get { return _referenceList; }
            set
            {
                _referenceList = value;
                RaisePropertyChanged();
            }
        }
        
        public ViewReferencesDialog()
        {
            InitializeComponent();
            _referenceList = new ObservableCollection<ReferenceItem>();
            DataContext = this;
        }

        /// <summary>
        /// Opens the Window as an Dialog
        /// </summary>
        /// <param name="owner">Owner Window if null opens as normal window</param>
        public void ShowDialog(Window owner, List<ReferenceList> referenceLists)
        {
            var observableCollection = new ObservableCollection<ReferenceItem>();
            foreach (var referenceList in referenceLists)
            {
                if (referenceList.References.Count > 0)
                {
                    referenceList.References.ForEach(x => observableCollection.Add(new ReferenceItem { Header = referenceList.Header, Value = x }));
                }
            }
            this.ReferenceList = observableCollection;
            this.Owner = owner;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            base.ShowDialog();
        }
        
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
