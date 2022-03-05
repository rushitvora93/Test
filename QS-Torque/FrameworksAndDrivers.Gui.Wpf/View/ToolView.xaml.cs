using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.TreeStructure;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using FrameworksAndDrivers.Localization;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Core;
using FrameworksAndDrivers.Gui.Wpf.View.Dialogs;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for ToolView.xaml
    /// </summary>
    public partial class ToolView : UserControl, ICanClose
    {
        private ExtensionTreeStructure<ToolModel, ToolModelModel> _toolTreeStructure;
        private TreeStructure<ToolModelModel> _modelTreeStructure;
        private ToolViewModel _viewModel;
        private ExpandableLeafLevel<ToolModelModel> _expandableModelLevel;
        private LocalizationWrapper _localization;
        private IToolDisplayFormatter _toolDisplayFormatter;

        private StructureTreeViewItemAdv _selectedStructureTreeViewItemAdv;
        

        public StructureTreeViewItemAdv SelectedStructureTreeViewItemAdv
        {
            get => _selectedStructureTreeViewItemAdv;
            set
            {
                _selectedStructureTreeViewItemAdv = value;

                if (!(_selectedStructureTreeViewItemAdv is StructureTreeViewItemAdv<ToolModel> structureTreeView))
                {
                    _viewModel.SelectedTool = null;
                    return;
                }
                _viewModel.SelectedTool = structureTreeView.DisplayMember.Item;
            }
        }

        #region Event-Handler
        private void ViewModel_MessageBoxRequest(object sender, MessageBoxEventArgs e)
        {
            e.Show();
        }

        private void ViewModel_ShowDialogRequest(object sender, System.Windows.Window e)
        {
            e.ShowDialog();
        }

        private void ViewModel_SelectionRequest(object sender, ToolModel e)
        {
            _toolTreeStructure.Select(e);
            TreeViewAdv.BringIntoView(_toolTreeStructure.GetContainerForItem(e));
        }

        private void ViewModel_RequestVerifyChangesView(object sender, VerifyChangesEventArgs e)
        {
            Dispatcher?.Invoke(() =>
            {
                var view = new VerifyChangesView(e.ChangedValues);
                view.ShowDialog();

                e.Comment = (view.DataContext as VerifyChangesViewModel)?.Comment;
                e.Result = view.Result;
            });
        }

        private void ExpandableModelLevel_TreeViewItemExpanded(object sender, AlwaysExpandableTreeViewItemAdv<ToolModelModel> e)
        {
            if (!e.HasAlreadyExpanded)
            {
                _viewModel.ToolModelExpanded(e?.DisplayMember?.Item);
                e.HasAlreadyExpanded = true;
            }
        }
        #endregion

        public ToolView(ToolViewModel viewModel, LocalizationWrapper localizationWrapper, IToolDisplayFormatter toolDisplayFormatter)
		{
			InitializeComponent();
            _localization = localizationWrapper;
			DataContext = viewModel;
            _viewModel = viewModel;
            _toolDisplayFormatter = toolDisplayFormatter;
			viewModel.SetDispatcher(Dispatcher);
			viewModel.MessageBoxRequest += ViewModel_MessageBoxRequest;
            viewModel.ShowDialogRequest += ViewModel_ShowDialogRequest;
            viewModel.RequestVerifyChangesView += ViewModel_RequestVerifyChangesView;
            viewModel.SelectionRequest += ViewModel_SelectionRequest;
            viewModel.ReferencesDialogRequest += ViewModelOnReferencesDialogRequest;

            _expandableModelLevel = new ExpandableLeafLevel<ToolModelModel>(toolModelModel => new DisplayMemberModel<ToolModelModel>(toolModelModel, tmm => tmm.Description), true);
            _expandableModelLevel.TreeViewItemExpanded += ExpandableModelLevel_TreeViewItemExpanded;

            _modelTreeStructure = new TreeStructure<ToolModelModel>(viewModel.AllToolModelModels,
                new RootLevel<ToolModelModel>(() => localizationWrapper.Strings.GetParticularString("Tool", "Tools"), localizationWrapper)
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
                TreeViewAdv,
                false);
            
            _toolTreeStructure = new ExtensionTreeStructure<ToolModel, ToolModelModel>(viewModel.AllToolModels,
                new LeafLevel<ToolModel>(toolModel => new DisplayMemberModel<ToolModel>(toolModel, tm => _toolDisplayFormatter.Format(tm.Entity))),
                _modelTreeStructure,
                toolModel => toolModel.ToolModelModel,
                new List<string>() { nameof(ToolModel.ToolModelModel) },
                TreeViewAdv,
                false);

            TreeViewAdv.ItemsSource = _toolTreeStructure.Source;
        }

        private void ViewModelOnReferencesDialogRequest(object sender, ReferenceList e)
        {
            var dialog = new ViewReferencesDialog();
            dialog.ShowDialog(Window.GetWindow(this), new List<ReferenceList>{e});
        }

        public bool CanClose()
        {
            if (DataContext is ICanClose canClose)
            {
                var closable = canClose.CanClose();
                if (closable)
                {
                    _expandableModelLevel.TreeViewItemExpanded -= ExpandableModelLevel_TreeViewItemExpanded;
                    TreeViewAdv.ClearSelection();
                }
                return closable;
            }
            _expandableModelLevel.TreeViewItemExpanded -= ExpandableModelLevel_TreeViewItemExpanded;
            return true;
        }
    }
}
