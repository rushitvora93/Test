using FrameworksAndDrivers.Localization;
using System.Windows.Controls;
using InterfaceAdapters.Localization;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using InterfaceAdapters.Models;
using FrameworksAndDrivers.Gui.Wpf.TreeStructure;
using FrameworksAndDrivers.Gui.Wpf.Model;
using Syncfusion.Windows.Tools.Controls;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using System;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using Core.Entities;
using Core;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for ProcessControlView.xaml
    /// </summary>
    public partial class ProcessControlView : UserControl, ICanClose, IDisposable, IGetUpdatedByLanguageChanges
    {
        private ProcessControlViewModel _viewModel;

        private LocalizationWrapper _localization;

        private ExtensionTreeStructure<LocationModel, long> _locationTreeStructure;
        private ElementTree<LocationDirectoryHumbleModel> _locationDirectoryTreeStructure;

        private ILocationDisplayFormatter _locationDisplayFormatter;

        private TreeViewItemAdv _selectedLocationTreeItem;
        public TreeViewItemAdv SelectedLocationTreeItem
        {
            get => _selectedLocationTreeItem;
            set
            {
                _selectedLocationTreeItem = value;

                if (value is StructureTreeViewItemAdv<LocationModel> locationDisplayMember)
                {
                    _viewModel.SelectedLocation = locationDisplayMember.DisplayMember.Item;
                }
                else
                {
                    if (_viewModel.SelectedLocation != null)
                    {
                        _viewModel.SelectedLocation = null;
                    }
                }
            }
        }

        public ProcessControlView(ProcessControlViewModel viewModel, LocalizationWrapper localization, ILocationDisplayFormatter locationDisplayFormatter)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _localization = localization;
            _localization.Subscribe(this);
            _locationDisplayFormatter = locationDisplayFormatter;
            viewModel.SetDispatcher(this.Dispatcher);
            viewModel.MessageBoxRequest += ViewModel_MessageBoxRequest;
            viewModel.InitializeLocationTreeRequest += ViewModel_InitializeLocationTreeRequest;
            viewModel.ShowDialogRequest += ViewModel_ShowDialogRequest;
            viewModel.RequestVerifyChangesView += ViewModel_RequestVerifyChangesView;
            viewModel.SelectionRequestLocation += ViewModel_SelectionRequestLocation;
            this.DataContext = viewModel;
            TestConditionsTabItem.IsSelected = true;
        }

        private void ViewModel_SelectionRequestLocation(object sender, LocationModel e)
        {
            if (e == null)
            {
                return;
            }
            Dispatcher?.Invoke(() =>
            {
                LocationTreeView.ClearSelection();

                _locationTreeStructure.Select(e);

                LocationTreeView.BringIntoView(_locationTreeStructure.GetContainerForItem(e));
                LocationTreeView.AddNodeToSelectedItems(_locationTreeStructure.GetContainerForItem(e));

            });
        }

        private void ViewModel_RequestVerifyChangesView(object sender, VerifyChangesEventArgs e)
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

        private void ViewModel_ShowDialogRequest(object sender, IAssistentView e)
        {
            e.ShowDialog();
        }

        public void LanguageUpdate()
        {
            var help = AuditRulesDatePicker.SelectedDate;
            AuditRulesDatePicker.SelectedDate = null;
            AuditRulesDatePicker.SelectedDate = help;
        }

        private void ViewModel_InitializeLocationTreeRequest(object sender, LocationTreeModel e)
        {
            Dispatcher.Invoke(() =>
            {
                _locationDirectoryTreeStructure = new ElementTree<LocationDirectoryHumbleModel>(_viewModel.LocationTree,
                    x => x.Name,
                    LocationTreeView,
                    new BitmapImage(new Uri("pack://application:,,,/Resources;component/Icons/folder.png")),
                    FeatureToggles.FeatureToggles.Pikachu_532_SortLocationTree);

                var directoryToDirectoryIdTreeConverter = new TreeConverter<long, LocationDirectoryHumbleModel>(
                    _locationDirectoryTreeStructure,
                    (id, dir) => dir.Id == id,
                    id => new LocationDirectoryHumbleModel(new LocationDirectory()) { Id = id });

                _locationTreeStructure = new ExtensionTreeStructure<LocationModel, long>(
                    _viewModel.LocationTree.LocationModels,
                    new LeafLevel<LocationModel>(x =>
                            new DisplayMemberModel<LocationModel>(x, y => _locationDisplayFormatter.Format(y.Entity)),
                        new BitmapImage(new Uri("pack://application:,,,/Resources;component/Icons/screw2.png"))),
                    directoryToDirectoryIdTreeConverter,
                    x => x.ParentId,
                    new List<string>() { nameof(LocationModel.ParentId) },
                    LocationTreeView,
                    true,
                    FeatureToggles.FeatureToggles.Pikachu_532_SortLocationTree);

                LocationTreeView.ItemsSource = _locationDirectoryTreeStructure.Source;
            });
        }

        private void ViewModel_MessageBoxRequest(object sender, MessageBoxEventArgs e)
        {
            e.Show();
        }
        public bool CanClose()
        {
            if (DataContext is ICanClose canClose)
            {
                return canClose.CanClose();
            }
            else
            {
                return true;
            }
        }
        public void Dispose()
        {
            _viewModel.MessageBoxRequest -= ViewModel_MessageBoxRequest;
            _viewModel.InitializeLocationTreeRequest -= ViewModel_InitializeLocationTreeRequest;
            _viewModel.ShowDialogRequest -= ViewModel_ShowDialogRequest;
            _viewModel.SelectionRequestLocation -= ViewModel_SelectionRequestLocation;
            _viewModel.Dispose();
            _viewModel = null;
            DataContext = null;
        }
    }
}
