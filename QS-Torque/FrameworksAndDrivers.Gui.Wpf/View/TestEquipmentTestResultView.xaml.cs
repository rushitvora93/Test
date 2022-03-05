using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using InterfaceAdapters.Localization;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaktionslogik für TestEquipmentTestResultView.xaml
    /// </summary>
    public partial class TestEquipmentTestResultView
    {
        public TestEquipmentTestResultView(TestEquipmentTestResultViewModel viewModel, ILocalizationWrapper localization)
        {
            InitializeComponent();
            DataContext = viewModel;
            ResultDataGrid.LocalizationWrapper = localization;
            localization.Subscribe(ResultDataGrid);
        }
    }
}
