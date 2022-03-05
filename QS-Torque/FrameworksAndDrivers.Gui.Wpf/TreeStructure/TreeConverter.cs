using System;
using System.Collections.ObjectModel;
using System.Linq;
using Syncfusion.Windows.Tools.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.TreeStructure
{
    public class TreeConverter<T, U> : TreeBase<T>
    {
        public TreeBase<U> SubTree { get; private set; }

        public override ObservableCollection<TreeViewItemAdv> Source => SubTree.Source;

        private Func<T, U, bool> _equalityComparer;
        private Func<T, U> _mapForSubTree;

        public TreeConverter(TreeBase<U> subTree, Func<T, U, bool> equalityComparer, Func<T, U> mapForSubTree) : base(null, true, null, false)
        {
            SubTree = subTree;
            _equalityComparer = equalityComparer;
            _mapForSubTree = mapForSubTree;
        }

        public override void Add(T item)
        {
            SubTree.Add(_mapForSubTree(item));
        }

        public override void Remove(T item)
        {
            SubTree.Remove(SearchForItemInSubTree(item));
        }

        public override TreeViewItemAdv GetContainerForItem(T item)
        {
            return SubTree.GetContainerForItem(SearchForItemInSubTree(item));
        }

        public override void ExpandToItem(T item)
        {
            SubTree.ExpandToItem(SearchForItemInSubTree(item));
        }

        public override void CollapseToItem(T item)
        {
            SubTree.CollapseToItem(SearchForItemInSubTree(item));
        }

        public override void Select(T item)
        {
            SubTree.Select(SearchForItemInSubTree(item));
        }

        private U SearchForItemInSubTree(T key)
        {
            return SubTree.ItemList.FirstOrDefault(x => _equalityComparer(key, x));
        }

        protected override void ProtectedAdd(T item)
        {
            // Do nothing
        }
    }
}