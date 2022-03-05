using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Localization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.View.Dialogs
{
    /// <summary>
    /// Interaction logic for LocationToolAssignmentDetails.xaml
    /// </summary>
    public partial class LocationToolAssignmentToolDetailsView : Window
    {

        public ObservableCollection<ToolModelWithToolUsage> Tools { get; set; }
        public LocationToolAssignmentToolDetailsView(LocalizationWrapper wrapper, List<ToolModelWithToolUsage> tools)
        {
            InitializeComponent();
            DataContext = this;
            DataGrid.LocalizationWrapper = wrapper;
            wrapper.Subscribe(DataGrid);
            Tools = new ObservableCollection<ToolModelWithToolUsage>(tools);
        }
    }
}
