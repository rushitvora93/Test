using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Client.Core;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
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
    /// Interaktionslogik für TestEquipmentView.xaml
    /// </summary>
    public partial class TestEquipmentView : ICanClose, IDisposable, IGetUpdatedByLanguageChanges
    {
        private readonly TestEquipmentViewModel _viewModel;
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
                    _viewModel.SelectedTestEquipmentModel = null;
                    _viewModel.SelectedTestEquipmentType = null;
                    if (_viewModel.SelectedTestEquipmentType == null && _viewModel.SelectedTestEquipmentModel == null)
                    {
                        _viewModel.SelectedTestEquipment = testEquipment.DisplayMember.Item;
                        if (!_viewModel.SelectedTestEquipment.FeaturesVisible)
                        {
                            TestEquipmentTabItem.IsSelected = true;
                        }
                    }
                }
                else if (value is StructureTreeViewItemAdv<TestEquipmentModelHumbleModel> testEquipmentModel)
                {
                    _viewModel.SelectedTestEquipmentType = null;
                    _viewModel.SelectedTestEquipment = null;
                    if (_viewModel.SelectedTestEquipment == null && _viewModel.SelectedTestEquipmentType == null)
                    {
                        _viewModel.SelectedTestEquipmentModel = testEquipmentModel.DisplayMember.Item;
                    }
                }
                else if (value is StructureTreeViewItemAdv<TestEquipmentTypeModel> testEquipmentType)
                {
                    _viewModel.SelectedTestEquipmentModel = null;
                    _viewModel.SelectedTestEquipment = null;
                    if (_viewModel.SelectedTestEquipment == null && _viewModel.SelectedTestEquipmentModel == null)
                    {
                        _viewModel.SelectedTestEquipmentType = testEquipmentType.DisplayMember.Item;
                    }
                }
                else if(value != null)
                {
                    _viewModel.SelectedTestEquipmentModel = null;
                    _viewModel.SelectedTestEquipment = null;
                    _viewModel.SelectedTestEquipmentType = null;
                }
            }
        }

        public TestEquipmentView(TestEquipmentViewModel viewModel, LocalizationWrapper localization, ITestEquipmentDisplayFormatter testEquipmentFormatter)
        {
            InitializeComponent();
            _viewModel = viewModel;
            viewModel.SetDispatcher(Dispatcher);
            _localization = localization;
            this.DataContext = viewModel;
            _localization.Subscribe(this);
            _viewModel.MessageBoxRequest += _viewModel_MessageBoxRequest;
            _viewModel.FileDialogRequest += _viewModel_FileDialogRequest;
            _viewModel.SelectionRequestTestEquipment += _viewModel_SelectionRequestTestEquipment;
            _viewModel.SelectionRequestTestEquipmentModel += _viewModel_SelectionRequestTestEquipmentModel;
            _viewModel.RequestVerifyChangesView += _viewModel_RequestVerifyChangesView;
            _viewModel.ShowDialogRequest += _viewModel_ShowDialogRequest;

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
                true);


            _testEquipmentTreeStructure = new ExtensionTreeStructure<TestEquipmentHumbleModel, TestEquipmentModelHumbleModel>(viewModel.TestEquipments,
                new LeafLevel<TestEquipmentHumbleModel>(model => new DisplayMemberModel<TestEquipmentHumbleModel>(model, tm => testEquipmentFormatter.Format(tm.Entity))),
                _testEquipmentModelTreeStructur,
                testEquipment => testEquipment.Model,
                new List<string>() { nameof(TestEquipmentHumbleModel.Model) },
                TestEquipmentTree,
                true);

            TestEquipmentTree.ItemsSource = _testEquipmentModelTreeStructur.Source;
        }

        public void LanguageUpdate()
        {
            var help = LastCalibrationDatePicker.SelectedDate;
            LastCalibrationDatePicker.SelectedDate = null;
            LastCalibrationDatePicker.SelectedDate = help;
        }

        private void _viewModel_ShowDialogRequest(object sender, ICanShowDialog e)
        {
            e.ShowDialog();
        }

        private void _viewModel_SelectionRequestTestEquipmentModel(object sender, TestEquipmentModelHumbleModel e)
        {
            if (e == null)
            {
                return;
            }

            Dispatcher?.Invoke(() =>
            {
                TestEquipmentTree.ClearSelection();

                _testEquipmentModelTreeStructur.Select(e);
                TestEquipmentTree.BringIntoView(_testEquipmentModelTreeStructur.GetContainerForItem(e));
                TestEquipmentTree.AddNodeToSelectedItems(_testEquipmentModelTreeStructur.GetContainerForItem(e));
            });
        }

        private void _viewModel_SelectionRequestTestEquipment(object sender, TestEquipmentHumbleModel e)
        {
            if (e == null)
            {
                return;
            }
            Dispatcher?.Invoke(() =>
            {
                TestEquipmentTree.ClearSelection();

                _testEquipmentTreeStructure.Select(e);
                
                TestEquipmentTree.BringIntoView(_testEquipmentTreeStructure.GetContainerForItem(e));
                TestEquipmentTree.AddNodeToSelectedItems(_testEquipmentTreeStructure.GetContainerForItem(e));
                
            });
        }

        private void _viewModel_RequestVerifyChangesView(object sender, VerifyChangesEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                var view = new VerifyChangesView(e.ChangedValues);
                view.ShowDialog();

                // Set Comment
                e.Comment = (view.DataContext as VerifyChangesViewModel).Comment;

                // Set Result
                e.Result = view.Result;
            });
        }

        private void _viewModel_FileDialogRequest(object sender, FileDialogEventArgs e)
        {
            using(OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.CheckFileExists = true;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    e.ResultAction(openFileDialog.FileName);
                }
            }
        }

        private void _viewModel_MessageBoxRequest(object sender, EventArgs.MessageBoxEventArgs e)
        {
            e.Show();
        }

        public bool CanClose()
        {
            return _viewModel.CanClose();
        }

        public void Dispose()
        {
            _viewModel.MessageBoxRequest -= _viewModel_MessageBoxRequest;
            _viewModel.FileDialogRequest -= _viewModel_FileDialogRequest;
            _viewModel.SelectionRequestTestEquipment -= _viewModel_SelectionRequestTestEquipment;
            _viewModel.SelectionRequestTestEquipmentModel -= _viewModel_SelectionRequestTestEquipmentModel;
            _viewModel.RequestVerifyChangesView -= _viewModel_RequestVerifyChangesView;
            _viewModel.ShowDialogRequest -= _viewModel_ShowDialogRequest;
            DataContext = null;
        }
    }
}
