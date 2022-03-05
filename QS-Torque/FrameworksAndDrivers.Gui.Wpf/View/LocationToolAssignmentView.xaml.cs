using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.TreeStructure;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using FrameworksAndDrivers.Localization;
using log4net;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Core;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;
using ToolModel = InterfaceAdapters.Models.ToolModel;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for LocationToolAssignmentView.xaml
    /// </summary>
    public partial class LocationToolAssignmentView : UserControl, IDisposable, ICanClose, IGetUpdatedByLanguageChanges
    {
        private LocationToolAssignmentViewModel _viewModel;
        private LocalizationWrapper _localization;

        private ExtensionTreeStructure<LocationModel, long> _locationTreeStructure;
        private ElementTree<LocationDirectoryHumbleModel> _locationDirectoryTreeStructure;

        private ExpandableLeafLevel<ToolModelModel> _expandableModelLevel;
        private ExtensionTreeStructure<ToolModel, ToolModelModel> _toolTreeStructure;
        private TreeStructure<ToolModelModel> _modelTreeStructure;

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
                    _viewModel.SelectedLocation = null;
                }
            }
        }

        private StructureTreeViewItemAdv _selectedToolTreeViewItemAdv;
        private IToolDisplayFormatter _toolDisplayFormatter;
        private ILocationDisplayFormatter _locationDisplayFormatter;
        private readonly ILog _log = LogManager.GetLogger(typeof(LocationToolAssignmentView));
        public StructureTreeViewItemAdv SelectedToolTreeViewItemAdv
        {
            get => _selectedToolTreeViewItemAdv;
            set
            {
                _selectedToolTreeViewItemAdv = value;
                
                if (value is StructureTreeViewItemAdv<ToolModel> structureTreeView)
                {
                    _viewModel.SelectedTool = structureTreeView.DisplayMember.Item;
                }
                else
                {
                    _viewModel.SelectedTool = null;
                }
            }
        }


        public LocationToolAssignmentView(LocationToolAssignmentViewModel viewModel, LocalizationWrapper localization, IToolDisplayFormatter toolDisplayFormatter, ILocationDisplayFormatter locationDisplayFormatter)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            _viewModel = viewModel;
            _localization = localization;
            _localization.Subscribe(this);

            _toolDisplayFormatter = toolDisplayFormatter;
            _locationDisplayFormatter = locationDisplayFormatter;
            viewModel.SetGuiDispatcher(this.Dispatcher);

            viewModel.InitializeLocationTreeRequest += ViewModel_InitializeLocationTreeRequest;
            viewModel.ShowDialogRequest += ViewModel_ShowDialogRequest;
            viewModel.MessageBoxRequest += ViewModel_MessageBoxRequest;
            viewModel.RequestChangesVerification += ViewModel_RequestChangesVerification;
            viewModel.LocationSelectionRequest += ViewModel_LocationSelectionRequest;
            viewModel.ToolSelectionRequest += ViewModel_ToolSelectionRequest;
            PropertyChangedEventManager.AddHandler(viewModel, ViewModel_SelectedLocationToolAssignmentModelChanged, nameof(viewModel.SelectedLocationToolAssignmentModel));
            InitToolTree();
        }

        private void ViewModel_ToolSelectionRequest(object sender, ToolModel e)
        {
            if (e is null)
            {
                return;
            }
            Dispatcher.Invoke(() =>
            {
                try
                {
                    _toolTreeStructure.Select(e);
                }
                catch (Exception exception)
                {
                    LogManager.GetLogger(typeof(LocationToolAssignmentView)).Error("ToolModel was not found", exception);
                }
            });
        }

        private void ViewModel_LocationSelectionRequest(object sender, LocationModel e)
        {
            Dispatcher.Invoke(() =>
            {
                _locationTreeStructure.Select(e);
            });
        }

        private void ViewModel_RequestChangesVerification(object sender, EventArgs.VerifyChangesEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var dialog = new VerifyChangesView(e.ChangedValues);
                dialog.ShowDialog();
                e.Comment = dialog.Comment;
                e.Result = dialog.Result;
            });
        }

        private void ViewModel_SelectedLocationToolAssignmentModelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_viewModel.SelectedLocationToolAssignmentModel?.TestTechnique is null)
            {
                TestConditionsTabItem.IsEnabled = false;
                if (TestConditionsTabItem.IsSelected == true)
                {
                    TabControl.SelectedIndex = 0;
                }
            }
            else
            {
                TestConditionsTabItem.IsEnabled = true;
            }
        }

        private void ViewModel_MessageBoxRequest(object sender, EventArgs.MessageBoxEventArgs e)
        {
            e.Show();
        }

        private void InitToolTree()
        {
            _expandableModelLevel = new ExpandableLeafLevel<ToolModelModel>(toolModelModel => new DisplayMemberModel<ToolModelModel>(toolModelModel, tmm => tmm.Description));
            _expandableModelLevel.TreeViewItemExpanded += ExpandableModelLevel_TreeViewItemExpanded;

            _modelTreeStructure = new TreeStructure<ToolModelModel>(_viewModel.AllToolModelModels,
                new RootLevel<ToolModelModel>(() => _localization.Strings.GetParticularString("Tool", "Tools"), _localization)
                {
                    SubLevel = new InternalLevel<ToolModelModel, AbstractToolTypeModel>(toolModelModel => toolModelModel.ModelType,
                                                                                        toolType => new DisplayMemberModel<AbstractToolTypeModel>(toolType, y => y.TranslatedName),
                                                                                        (leftToolType, rightToolType) => leftToolType?.Equals(rightToolType) ?? rightToolType == null)
                    {
                        SubLevel = new InternalLevel<ToolModelModel, ManufacturerModel>(toolModelModel => toolModelModel.Manufacturer,
                                                                                        manufacturerModel => new DisplayMemberModel<ManufacturerModel>(manufacturerModel, y => y.Name),
                                                                                        (leftManufacturerModel, rightManufacturerModel) =>
                                                                                        {
                                                                                            if (leftManufacturerModel == null && rightManufacturerModel == null)
                                                                                            {
                                                                                                return true;
                                                                                            }
                                                                                            if (leftManufacturerModel == null || rightManufacturerModel == null)
                                                                                            {
                                                                                                return false;
                                                                                            }
                                                                                            return leftManufacturerModel?.Equals(rightManufacturerModel) ?? rightManufacturerModel == null;
                                                                                        })
                        {
                            SubLevel = _expandableModelLevel
                        }
                    }
                },
                new List<string>()
                {
                    nameof(ToolModelModel.ModelType),
                    nameof(ToolModelModel.Manufacturer)
                },
                ToolTreeView);

            _toolTreeStructure = new ExtensionTreeStructure<ToolModel, ToolModelModel>(_viewModel.AllToolModels,
                new LeafLevel<ToolModel>(toolModel => new DisplayMemberModel<ToolModel>(toolModel, tm => _toolDisplayFormatter.Format(tm.Entity))),
                _modelTreeStructure,
                toolModel => toolModel.ToolModelModel,
                new List<string>() { nameof(InterfaceAdapters.Models.ToolModel.ToolModelModel) },
                ToolTreeView,
                false);

            ToolTreeView.ItemsSource = _toolTreeStructure.Source;
        }

        private void ExpandableModelLevel_TreeViewItemExpanded(object sender, AlwaysExpandableTreeViewItemAdv<ToolModelModel> e)
        {
            if (!e.HasAlreadyExpanded)
            {
                _viewModel.ToolModelExpanded(e?.DisplayMember?.Item);
                e.HasAlreadyExpanded = true;
            }
        }

        private void ViewModel_InitializeLocationTreeRequest(object sender, System.EventArgs e)
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

        private void ViewModel_ShowDialogRequest(object sender, IAssistentView e)
        {
            e.ShowDialog();
        }

        public void Dispose()
        {
            _viewModel.InitializeLocationTreeRequest -= ViewModel_InitializeLocationTreeRequest;
            _viewModel = null;
            DataContext = null;
        }

        public bool CanClose()
        {
            if (DataContext is ICanClose canClose)
            {
                return canClose.CanClose();
            }

            return true;
        }

        public void LanguageUpdate()
        {
            var help = StartDatePickerMonitoring.SelectedDate;
            StartDatePickerMonitoring.SelectedDate = null;
            StartDatePickerMonitoring.SelectedDate = help;

            help = StartDatePickerMca.SelectedDate;
            StartDatePickerMca.SelectedDate = null;
            StartDatePickerMca.SelectedDate = help;
        }
    }
}
