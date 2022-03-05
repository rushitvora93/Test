using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Client.Core;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.TreeStructure;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using FrameworksAndDrivers.Localization;
using InterfaceAdapters.Communication;
using InterfaceAdapters.Localization;
using Syncfusion.Windows.Tools.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaktionslogik für TransferToTestEquipmentView.xaml
    /// </summary>
    public partial class TransferToTestEquipmentView : UserControl, IGetUpdatedByLanguageChanges, IDisposable
    {
        private readonly TransferToTestEquipmentViewModel _viewModel;
        private readonly LocalizationWrapper _localization;
        private readonly ExtensionTreeStructure<TestEquipmentHumbleModel, TestEquipmentModelHumbleModel> _testEquipmentTreeStructure;
        private readonly TreeStructure<TestEquipmentModelHumbleModel> _testEquipmentModelTreeStructur;

        private TreeViewItemAdv _selectedTreeItem;
        public TreeViewItemAdv SelectedTreeItem
        {
            get => _selectedTreeItem;
            set
            {
                _selectedTreeItem = value;
                if (value is StructureTreeViewItemAdv<TestEquipmentHumbleModel> testEquipment)
                {
                    _viewModel.SelectedTestEquipment = testEquipment.DisplayMember.Item;
                }
                else
                {
                    _viewModel.SelectedTestEquipment = null;
                }
            }
        }

        public TransferToTestEquipmentView(TransferToTestEquipmentViewModel viewModel, LocalizationWrapper localization, ITestEquipmentDisplayFormatter testEquipmentFormatter)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _localization = localization;
            DataContext = viewModel;
            viewModel.MessageBoxRequest += MessageBoxRequestHandler;
            viewModel.SetDispatcher(Dispatcher);
            DataGrid.LocalizationWrapper = localization;
            DataGridProcess.LocalizationWrapper = localization; 
            localization.Subscribe(DataGrid);
            localization.Subscribe(DataGridProcess);
            _localization.Subscribe(this);

            _testEquipmentModelTreeStructur = new TreeStructure<TestEquipmentModelHumbleModel>(viewModel.TestEquipmentModels,
                new RootLevel<TestEquipmentModelHumbleModel>(() => localization.Strings.GetParticularString("TestEquipment", "Test equipments"), localization)
                {
                    SubLevel = new InternalLevel<TestEquipmentModelHumbleModel, TestEquipmentTypeModel>(x => x.TestEquipmentType,
                        x => new DisplayMemberModel<TestEquipmentTypeModel>(x, y => y.TranslatedName),
                        (x1, x2) => x1?.Equals(x2) ?? x2 == null)
                    {
                        SubLevel = new LeafLevel<TestEquipmentModelHumbleModel>(x => new DisplayMemberModel<TestEquipmentModelHumbleModel>(x, y => y.TestEquipmentModelName))
                    }

                },
                new List<string>()
                {
                    nameof(TestEquipmentModelHumbleModel.TestEquipmentType),
                },
                TestEquipmentTree,
                false);

            _testEquipmentTreeStructure = new ExtensionTreeStructure<TestEquipmentHumbleModel, TestEquipmentModelHumbleModel>(viewModel.TestEquipments,
                new LeafLevel<TestEquipmentHumbleModel>(model => new DisplayMemberModel<TestEquipmentHumbleModel>(model, tm => testEquipmentFormatter.Format(tm.Entity))),
                _testEquipmentModelTreeStructur,
                testEquipment => testEquipment.Model,
                new List<string>() { nameof(TestEquipmentHumbleModel.Model) },
                TestEquipmentTree,
                false);

            TestEquipmentTree.ItemsSource = _testEquipmentModelTreeStructur.Source;
        }

        private void MessageBoxRequestHandler(object sender, EventArgs.MessageBoxEventArgs e)
        {
            Dispatcher.Invoke(()=> e.Show(Window.GetWindow(this)));
        }
        
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            (_testEquipmentModelTreeStructur.SourceLevel as RootLevel<TestEquipmentModelHumbleModel>).CollapseRoot();
        }

        public void LanguageUpdate()
        {
            SwitchButtonTop.LeftButton.Content =
                _localization.Strings.GetParticularString("Transfer To Test Equipment", "Tool testing");

            SwitchButtonTop.RightButton.Content =
                _localization.Strings.GetParticularString("Transfer To Test Equipment", "Process testing");

            SwitchButtonBottom.LeftButton.Content =
                _localization.Strings.GetParticularString("Transfer To Test Equipment", "Tool monitoring");

            SwitchButtonBottom.RightButton.Content =
                _localization.Strings.GetParticularString("Transfer To Test Equipment", "MCA");
        }

        public void Dispose()
        {
            _viewModel.MessageBoxRequest -= MessageBoxRequestHandler;
            SelectedTreeItem = null;
            DataContext = null;
        }
    }
}
