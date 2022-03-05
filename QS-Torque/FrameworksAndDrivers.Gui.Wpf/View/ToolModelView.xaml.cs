using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.TreeStructure;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using FrameworksAndDrivers.Localization;
using Syncfusion.Windows.Tools.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using FrameworksAndDrivers.Gui.Wpf.View.Dialogs;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for ToolModelView.xaml
    /// </summary>
    public partial class ToolModelView : UserControl, ICanClose
    {


        private TreeStructure<ToolModelModel> _treeStructure;
        private ToolModelViewModel _viewModel;
        private LocalizationWrapper _localization;

        private ToolModelModel _ignoreNextAddToSelectionOf;
        private StructureTreeViewItemAdv _lastSelectedTvi;

        
        #region Interface Implementations
        public bool CanClose()
        {
            if (DataContext is ICanClose canClose)
            {
                return canClose.CanClose();
            }
            return true;
        }
        #endregion


        #region Event-Handler
        private void ViewModel_MessageBoxRequest(object sender, MessageBoxEventArgs e)
        {
            e.Show();
        }

        private void ViewModel_ShowDialogRequest(object sender, Window e)
        {
            e.ShowDialog();
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

        private static MessageBoxResult lastResult;
        private void TreeViewAdv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(_lastSelectedTvi != TreeViewAdv.SelectedTreeItem && TreeViewAdv.SelectedTreeItem != null && TreeViewAdv.SelectedTreeItem as StructureTreeViewItemAdv<ToolModelModel> == null)
            {
                _viewModel.SelectedToolModels.Clear();
                TreeViewAdv.ClearSelection();
                _viewModel.IsListViewVisible = false;
                return;
            }

            foreach (var removed in e.RemovedItems)
            {
                var item = (removed as StructureTreeViewItemAdv<ToolModelModel>)?.DisplayMember?.Item;
                if (_viewModel.SelectedToolModels.Contains(item) && item != null)
                {
                    _viewModel.SelectedToolModels.Remove(item);
                }
            }

            foreach (var added in e.AddedItems)
            {
                var item = (added as StructureTreeViewItemAdv<ToolModelModel>)?.DisplayMember?.Item;
                if (!_viewModel.SelectedToolModels.Contains(item) && item != null)
                {
                    if (item == _ignoreNextAddToSelectionOf)
                    {
                        _ignoreNextAddToSelectionOf = null;
                    }
                    else
                    {
                        _viewModel.SelectedToolModels.Add(item);
                    }
                }
            }

            if (TreeViewAdv.SelectedTreeItem as StructureTreeViewItemAdv<ToolModelModel> != null)
            {
                if ((TreeViewAdv.SelectedTreeItem as StructureTreeViewItemAdv<ToolModelModel>)?.DisplayMember?.Item != _viewModel.SelectedToolModel)
                {
                    if (lastResult == MessageBoxResult.Cancel)
                    {
                        TreeViewAdv.SelectedTreeItem = _treeStructure.GetContainerForItem(_viewModel.SelectedToolModel);
                        lastResult = MessageBoxResult.None;
                    }
                    else
                    {
                        if (_viewModel.SaveToolModelCommand.CanExecute(null))
                        {
                            var result = MessageBox.Show(_localization.Strings.GetString("Do you want to save your changes? This change will affect the references."),
                                                            "",
                                                            MessageBoxButton.YesNoCancel,
                                                            MessageBoxImage.Question);

                            switch (result)
                            {
                                case MessageBoxResult.Yes:
                                    _viewModel.SaveToolModelCommand.Invoke(null);
                                    _viewModel.SelectedToolModelAfterDiffDialog = (TreeViewAdv.SelectedItem as StructureTreeViewItemAdv<ToolModelModel>)?.DisplayMember?.Item;
                                    break;
                                case MessageBoxResult.No:
                                    _viewModel.UndoChanges();
                                    _viewModel.SelectedToolModel = (TreeViewAdv.SelectedItem as StructureTreeViewItemAdv<ToolModelModel>)?.DisplayMember?.Item;
                                    break;
                                case MessageBoxResult.Cancel:
                                    _ignoreNextAddToSelectionOf = (TreeViewAdv.SelectedTreeItem as StructureTreeViewItemAdv<ToolModelModel>)?.DisplayMember?.Item;
                                    TreeViewAdv.ClearSelection();
                                    _treeStructure.Select(_viewModel.SelectedToolModel);
                                    lastResult = MessageBoxResult.Cancel;
                                    break;
                            }
                        }
                        else
                        {
                            _viewModel.SelectedToolModel = (TreeViewAdv.SelectedItem as StructureTreeViewItemAdv<ToolModelModel>)?.DisplayMember?.Item;
                        } 
                    }
                } 
            }
            
            _viewModel.IsListViewVisible = _viewModel.SelectedToolModels.Count > 1;
            _lastSelectedTvi = TreeViewAdv.SelectedItem as StructureTreeViewItemAdv;
        }

        private void ViewModel_SelectionRequest(object sender, ToolModelModel e)
        {
            TreeViewAdv.ClearSelection();

            if (e != null)
            {
                _treeStructure.Select(e);
                TreeViewAdv.BringIntoView(_treeStructure.GetContainerForItem(e));
                TreeViewAdv.AddNodeToSelectedItems(_treeStructure.GetContainerForItem(e));
            }

        }
        #endregion


        public ToolModelView(ToolModelViewModel viewModel, LocalizationWrapper localizationWrapper)
		{
			InitializeComponent();
			DataContext = viewModel;
            _viewModel = viewModel;
            _localization = localizationWrapper;
            viewModel.SetDispatcher(Dispatcher);
			viewModel.MessageBoxRequest += ViewModel_MessageBoxRequest;
            viewModel.ShowDialogRequest += ViewModel_ShowDialogRequest;
            viewModel.RequestVerifyChangesView += ViewModel_RequestVerifyChangesView;
            viewModel.ReferencesDialogRequest += ViewModelOnReferencesDialogRequest;
            viewModel.SelectionRequest += ViewModel_SelectionRequest;
            viewModel.SetColumnWidths += ViewModelOnSetColumnWidths;
            viewModel.UpdateColumnData += ViewModel_UpdateColumnData;
            DataGrid.LocalizationWrapper = localizationWrapper;
            localizationWrapper.Subscribe(DataGrid);

            _treeStructure = new TreeStructure<ToolModelModel>(viewModel.AllToolModelModels,
                new RootLevel<ToolModelModel>(() => localizationWrapper.Strings.GetParticularString("ToolModel", "Tool models"), localizationWrapper)
                {
                    SubLevel = new InternalLevel<ToolModelModel, AbstractToolTypeModel>(x => x.ModelType,
                                                                                        x => new DisplayMemberModel<AbstractToolTypeModel>(x, y => y.TranslatedName),
                                                                                        (x1, x2) => x1?.Equals(x2) ?? x2 == null)
                    {
                        SubLevel = new InternalLevel<ToolModelModel, ManufacturerModel>(x => x.Manufacturer,
                                                                                        x => new DisplayMemberModel<ManufacturerModel>(x, y => y.Name),
                                                                                        (x1, x2) =>
                                                                                        {
                                                                                            if(x1 == null && x2 == null)
                                                                                            {
                                                                                                return true;
                                                                                            }
                                                                                            if(x1 == null || x2 == null)
                                                                                            {
                                                                                                return false;
                                                                                            }
                                                                                            return x1?.Equals(x2) ?? x2 == null;
                                                                                        })
                        {
                            SubLevel = new LeafLevel<ToolModelModel>(x => new DisplayMemberModel<ToolModelModel>(x, y => y.Description))
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
            TreeViewAdv.ItemsSource = _treeStructure.Source;
        }

        private void ViewModel_UpdateColumnData(object sender, System.EventArgs e)
        {
            List<(string mappingName, double width)> columnData = new List<(string mappingName, double width)>();
            foreach (var dataGridColumn in DataGrid.Columns)
            {
                columnData.Add((dataGridColumn.MappingName, dataGridColumn.ActualWidth));
            }

            _viewModel.ColumnData = columnData;
        }

        private void ViewModelOnSetColumnWidths(object sender, List<(string mappingName, double columnWidth)> e)
        {
            foreach (var (mappingName, columnWidth) in e)
            {
                var column = DataGrid.Columns.FirstOrDefault(x => x.MappingName == mappingName);
                if (column != null)
                {
                    column.Width = columnWidth;
                }
            }
        }

        private void ViewModelOnReferencesDialogRequest(object sender, ReferenceList e)
        {
            var dialog = new ViewReferencesDialog();
            dialog.ShowDialog(Window.GetWindow(this), new List<ReferenceList>{e});
        }
    }
}
