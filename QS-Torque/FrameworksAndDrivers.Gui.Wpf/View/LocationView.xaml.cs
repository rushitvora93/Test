using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View.Dialogs;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using FrameworksAndDrivers.Localization;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Core;
using FrameworksAndDrivers.Gui.Wpf.TreeStructure;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for LocationView.xaml
    /// </summary>
    public partial class LocationView : UserControl, ICanClose, IDisposable
    {
        private LocationViewModel _viewModel;
        private LocalizationWrapper _localization;

        private ExtensionTreeStructure<LocationModel, long> _locationTreeStructure;
        private ElementTree<LocationDirectoryHumbleModel> _locationDirectoryTreeStructure;
        private static readonly double CutOpacity = 0.4;

        private TreeViewItemAdv _selectedTreeItem;
        private ILocationDisplayFormatter _locationDisplayFormatter;

        public TreeViewItemAdv SelectedTreeItem
        {
            get => _selectedTreeItem;
            set
            {
                _selectedTreeItem = value;
                if (value is StructureTreeViewItemAdv<LocationModel> locationDisplayMember)
                {
                    _viewModel.SelectedLocation = locationDisplayMember.DisplayMember.Item;
                    _viewModel.SelectedDirectoryHumble = null;
                }
                else if(value is StructureTreeViewItemAdv<LocationDirectoryHumbleModel> directoryDisplayMember)
                {
                    _viewModel.SelectedLocation = null;
                    _viewModel.SelectedDirectoryHumble = directoryDisplayMember.DisplayMember.Item;
                }
            }
        }


        public LocationView(LocationViewModel viewModel, LocalizationWrapper localization, ILocationDisplayFormatter locationDisplayFormatter)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _localization = localization;
            _locationDisplayFormatter = locationDisplayFormatter;
            viewModel.SetDispatcher(this.Dispatcher);
            this.DataContext = viewModel;
            viewModel.ShowDialogRequest += ViewModel_ShowDialogRequest;
            viewModel.MessageBoxRequest += ViewModel_MessageBoxRequest;
            viewModel.SelectionRequest += ViewModel_SelectionRequest;
            viewModel.FolderSelectionRequest += ViewModel_FolderSelectionRequest;
            viewModel.LocationDirectoryNameRequest += LocationFolderNameRequest;
            viewModel.RequestChangesVerification += ViewModel_RequestChangesVerification;
            viewModel.InitializeLocationTreeRequest += ViewModel_InitializeLocationTreeRequest;
            viewModel.CutLocation += ViewModel_CutLocation;
            viewModel.CutLocationDirectory += ViewModel_CutLocationDirectory;
            viewModel.ResetCutMarking += ViewModel_ResetCutMarking;
        }

        private void ViewModel_ResetCutMarking(object sender, System.EventArgs e)
        {
            ResetOpacity();
        }

        private void ViewModel_CutLocationDirectory(object sender, LocationDirectoryHumbleModel e)
        {
            ResetOpacity();
            _locationDirectoryTreeStructure.GetContainerForItem(e).Opacity = CutOpacity;
        }

        private void ViewModel_CutLocation(object sender, LocationModel e)
        {
            ResetOpacity();
            _locationTreeStructure.GetContainerForItem(e).Opacity = CutOpacity;
        }

        private void ResetOpacity()
        {
            _locationDirectoryTreeStructure.ResetOpacity();
            _locationTreeStructure.ResetOpacity();
        }

        private void ViewModel_InitializeLocationTreeRequest(object sender, System.EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                _locationDirectoryTreeStructure = new ElementTree<LocationDirectoryHumbleModel>(_viewModel.LocationTree,
                    x => x.Name,
                    LocationTreeView,
                    new BitmapImage(new Uri("pack://application:,,,/Resources;component/Icons/folder.png")),
                FeatureToggles.FeatureToggles.Pikachu_532_SortLocationTree,
                new List<string>{nameof(LocationDirectoryHumbleModel.ParentId)});

                var directoryToDirectoryIdTreeConverter = new TreeConverter<long, LocationDirectoryHumbleModel>(
                    _locationDirectoryTreeStructure,
                    (id, dir) => dir.Id == id,
                    id => new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = id});

                _locationTreeStructure = new ExtensionTreeStructure<LocationModel, long>(
                    _viewModel.LocationTree.LocationModels,
                    new LeafLevel<LocationModel>(x =>
                            new DisplayMemberModel<LocationModel>(x, y => _locationDisplayFormatter.Format(y.Entity)),
                        new BitmapImage(new Uri("pack://application:,,,/Resources;component/Icons/screw2.png"))),
                    directoryToDirectoryIdTreeConverter,
                    x => x.ParentId,
                    new List<string>() {nameof(LocationModel.ParentId)},
                    LocationTreeView,
                    true,
                    FeatureToggles.FeatureToggles.Pikachu_532_SortLocationTree);

                LocationTreeView.ItemsSource = _locationDirectoryTreeStructure.Source;
            });
        }

        private void ViewModel_FolderSelectionRequest(object sender, LocationDirectoryHumbleModel e)
        {
            Dispatcher.Invoke(() =>
            {
                _locationDirectoryTreeStructure.Select(e);
            });
        }

        private void T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() => { LocationTreeView.SortTreeView(); });
        }

        public bool CanClose()
        {
            return _viewModel.CanClose();
        }
        private void ViewModel_RequestChangesVerification(object sender, VerifyChangesEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var dialog = new VerifyChangesView(e.ChangedValues);
                dialog.ShowDialog();
                e.Comment = dialog.Comment;
                e.Result = dialog.Result;
            });
        }

        private void LocationFolderNameRequest(object sender, (Action<MessageBoxResult, string>, Predicate<string>) e)
        {
            var dialog = new LocationDirectoryNameRequestDialog(e.Item1, e.Item2,
                _localization.Strings.GetParticularString("LocationViewModel",
                    "A directory with the same name is already in this folder!"),
                _localization.Strings.GetParticularString("LocationViewModel", "Directory with same name exists"));
            dialog.Owner = Window.GetWindow(this);
            dialog.ShowDialog();
        }

        private void ViewModel_SelectionRequest(object sender, LocationModel e)
        {
            Dispatcher.Invoke(() =>
            {
                _locationTreeStructure.Select(e);
            });
        }

        private void ViewModel_MessageBoxRequest(object sender, MessageBoxEventArgs e)
        {
            e.Show();
        }

        private void ViewModel_ShowDialogRequest(object sender, ICanShowDialog e)
        {
            e.ShowDialog();
        }


        public void Dispose()
        {
            _viewModel.ShowDialogRequest -= ViewModel_ShowDialogRequest;
            _viewModel.MessageBoxRequest -= ViewModel_MessageBoxRequest;
            _viewModel.SelectionRequest -= ViewModel_SelectionRequest;
            _viewModel.FolderSelectionRequest -= ViewModel_FolderSelectionRequest;
            _viewModel.LocationDirectoryNameRequest -= LocationFolderNameRequest;
            _viewModel.RequestChangesVerification -= ViewModel_RequestChangesVerification;
            _viewModel.InitializeLocationTreeRequest -= ViewModel_InitializeLocationTreeRequest;
            _viewModel.CutLocation -= ViewModel_CutLocation;
            _viewModel.CutLocationDirectory -= ViewModel_CutLocationDirectory;
            _viewModel = null;
            DataContext = null;
        }
    }
}