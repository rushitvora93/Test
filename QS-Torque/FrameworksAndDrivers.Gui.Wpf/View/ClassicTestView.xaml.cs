using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using System;
using System.Windows.Controls;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.TreeStructure;
using System.Windows.Media.Imaging;
using FrameworksAndDrivers.Gui.Wpf.Model;
using System.Collections.Generic;
using Core;
using InterfaceAdapters;
using InterfaceAdapters.Models;
using Syncfusion.Windows.Tools.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for ManufacturerView.xaml
    /// </summary>
    public partial class ClassicTestView : UserControl, ICanClose, IDisposable
    {
        #region Event-Handler
        private void ClassicTestView_MessageBoxRequest(object sender, MessageBoxEventArgs e)
        {            
            e.Show();
        }
        #endregion

        private ExtensionTreeStructure<LocationModel, long> _locationTreeStructure;
        private ILocationDisplayFormatter _locationDisplayFormatter;
        private ElementTree<LocationDirectoryHumbleModel> _locationDirectoryTreeStructure;
        private ClassicTestViewModel _viewModel;

        public ClassicTestView(ClassicTestViewModel classicTestViewModel, ILocationDisplayFormatter locationDisplayFormatter)
		{
			InitializeComponent();
            _locationDisplayFormatter = locationDisplayFormatter;
            DataContext = classicTestViewModel;
            classicTestViewModel.SetGuiDispatcher(this.Dispatcher);
            classicTestViewModel.InitializeLocationTreeRequest += ViewModel_InitializeLocationTreeRequest;
            _viewModel = classicTestViewModel;
            _viewModel.MessageBoxRequest += ClassicTestView_MessageBoxRequest;
            MfuGrid.SelectedItems.CollectionChanged += MfuSelectedItems_CollectionChanged;
            ChkGrid.SelectedItems.CollectionChanged += ChkSelectedItems_CollectionChanged;
            PfuGrid.SelectedItems.CollectionChanged += PfuSelectedItems_CollectionChanged;
            CtlGrid.SelectedItems.CollectionChanged += CtlSelectedItems_CollectionChanged;
        }

        private void ChkSelectedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        _viewModel.selectedChks.Add((ChkHeaderClassicTestHumbleModel)item);
                    }
                    MfuGrid.SelectedItems.Clear();
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        _viewModel.selectedChks.Remove((ChkHeaderClassicTestHumbleModel)item);
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    _viewModel.selectedChks.Clear();
                    break;
            }
            _viewModel.EvaluateDataComand.InvalidateRequerySuggested();
        }

        private void MfuSelectedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        _viewModel.selectedMfus.Add((MfuHeaderClassicTestHumbleModel)item);
                    }
                    ChkGrid.SelectedItems.Clear();
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        _viewModel.selectedMfus.Remove((MfuHeaderClassicTestHumbleModel)item);
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    _viewModel.selectedMfus.Clear();
                    break;

            }
            _viewModel.EvaluateDataComand.InvalidateRequerySuggested();
        }

        private void CtlSelectedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        _viewModel.selectedCtls.Add((ProcessHeaderClassicTestHumbleModel)item);
                    }
                    PfuGrid.SelectedItems.Clear();
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        _viewModel.selectedCtls.Remove((ProcessHeaderClassicTestHumbleModel)item);
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    _viewModel.selectedCtls.Clear();
                    break;
            }
            _viewModel.EvaluateDataComand.InvalidateRequerySuggested();
        }

        private void PfuSelectedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        _viewModel.selectedPfus.Add((ProcessHeaderClassicTestHumbleModel)item);
                    }
                    CtlGrid.SelectedItems.Clear();
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        _viewModel.selectedPfus.Remove((ProcessHeaderClassicTestHumbleModel)item);
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    _viewModel.selectedPfus.Clear();
                    break;

            }
            _viewModel.EvaluateDataComand.InvalidateRequerySuggested();
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

        private TreeViewItemAdv _selectedTreeItem;
        public TreeViewItemAdv SelectedTreeItem
        {
            get => _selectedTreeItem;
            set
            {
                _selectedTreeItem = value;

                if (value is StructureTreeViewItemAdv<LocationModel> locationDisplayMember)
                {
                    _viewModel.SelectedLocation = locationDisplayMember.DisplayMember.Item;
                }
                else 
                {
                    _viewModel.SelectedLocation = null;
                }
            }
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

        public void Dispose()
        {
            MfuGrid.SelectedItems.CollectionChanged -= MfuSelectedItems_CollectionChanged;
            ChkGrid.SelectedItems.CollectionChanged -= ChkSelectedItems_CollectionChanged;
            PfuGrid.SelectedItems.CollectionChanged -= PfuSelectedItems_CollectionChanged;
            CtlGrid.SelectedItems.CollectionChanged -= CtlSelectedItems_CollectionChanged;
            _viewModel.MessageBoxRequest -= ClassicTestView_MessageBoxRequest;
            _viewModel.InitializeLocationTreeRequest -= ViewModel_InitializeLocationTreeRequest;
            _viewModel = null;
            DataContext = null;
        }
    }
}
