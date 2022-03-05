using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InterfaceAdapters;
using Syncfusion.Windows.Tools.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.TreeStructure
{
    public class ExtensionTreeStructure<T, U> : TreeStructure<T> where T : BindableBase
    {
        private Func<T, U> _getItemForSubTreeStructure;
        public new ITreeLevel<T> SourceLevel { get; private set; }
        public TreeBase<U> SubTree { get; private set; }

        public override ObservableCollection<TreeViewItemAdv> Source
        {
            get => SubTree.Source;
        }

        #region Methods
        protected override void ProtectedAdd(T item)
        {
            var itemForSubTree = _getItemForSubTreeStructure(item);
            var containerOfSubTree = SubTree.GetContainerForItem(itemForSubTree);

            if (containerOfSubTree == null)
            {
                SubTree.Add(itemForSubTree);
            }
            
            var tvi = SourceLevel.Add(item, SubTree.GetContainerForItem(itemForSubTree) as StructureTreeViewItemAdv);
            _cache.Add(item, tvi);
        }
        #endregion


        public ExtensionTreeStructure(ObservableCollection<T> list, ITreeLevel<T> sourceLevel, TreeBase<U> subTree, Func<T, U> getItemForSubTreeStructure, List<string> namesOfLevelAttributes, TreeViewAdv treeViewAdvControl, bool allowEmptyNodes, bool isTreeSorted = true)
            : base(list, null, namesOfLevelAttributes, treeViewAdvControl, allowEmptyNodes, isTreeSorted)
        {
            SourceLevel = sourceLevel;
            SubTree = subTree;
            _getItemForSubTreeStructure = getItemForSubTreeStructure;
        }
    }
}
