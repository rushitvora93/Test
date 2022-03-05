using FrameworksAndDrivers.Gui.Wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using InterfaceAdapters;

namespace FrameworksAndDrivers.Gui.Wpf.TreeStructure
{
    public interface ITreeLevel
    {
        void Remove(StructureTreeViewItemAdv tvi);
    }

    public interface ITreeLevel<T> : ITreeLevel
    {
        StructureTreeViewItemAdv<T> Add(T item, StructureTreeViewItemAdv parent);
    }

    
    /// <typeparam name="T">Type of the objects that are structured int the tree</typeparam>
    /// <typeparam name="U">Type of the object, by which the Items are structured in this level</typeparam>
    public class InternalLevel<T, U> : ITreeLevel<T> where T : BindableBase
    {
        private Func<T, U> _getLevelAttribute;
        private Func<U, U, bool> _areLevelAttributesEqual;
        private Func<U, DisplayMemberModel<U>> _getDisplayMemberForLevelItem;
        private List<StructureTreeViewItemAdv<U>> _internalNodes;

        public ITreeLevel<T> SubLevel { get; set; }


        public StructureTreeViewItemAdv<T> Add(T item, StructureTreeViewItemAdv parent)
        {
            var internalNodesForParent = _internalNodes.Where(x => x.TreeParent == parent);

            var internalNodeForItem = internalNodesForParent.FirstOrDefault(x => _areLevelAttributesEqual(_getLevelAttribute(item), x.DisplayMember.Item));

            if (SubLevel == null)
            {
                throw new InvalidOperationException("A InternalTreeLevel has to have a SubLevel");
            }
            else if (internalNodeForItem != null)
            {
                // Add to level below
                return SubLevel.Add(item, internalNodeForItem);
            }
            else
            {
                // Create new internal node in this level
                var levelAttribute = _getLevelAttribute(item);

                var node = new StructureTreeViewItemAdv<U>(levelAttribute != null ? _getDisplayMemberForLevelItem(levelAttribute) : new DisplayMemberModel<U>(x => ""));
                node.TreeParent = parent;
                node.Level = this;
                parent.Items.Add(node);
                _internalNodes.Add(node);
                parent.SortTreeViewItem();

                // Add item below the new node
                return SubLevel.Add(item, node);
            }
        }

        public void Remove(StructureTreeViewItemAdv tvi)
        {
            _internalNodes.Remove(tvi as StructureTreeViewItemAdv<U>);
        }


        public InternalLevel(Func<T, U> getLevelAttribute, Func<U, DisplayMemberModel<U>> getDisplayMemberForLevelItem, Func<U, U, bool> areLevelAttributesEqual)
        {
            _internalNodes = new List<StructureTreeViewItemAdv<U>>();
            _getLevelAttribute = getLevelAttribute;
            _getDisplayMemberForLevelItem = getDisplayMemberForLevelItem;
            _areLevelAttributesEqual = areLevelAttributesEqual;
        }
    }


    public class LeafLevel<T> : ITreeLevel<T> where T : BindableBase
    {
        private Func<T, DisplayMemberModel<T>> _getDisplayMemberForLeafItem;
        private ImageSource _leftItemImage;


        public StructureTreeViewItemAdv<T> Add(T item, StructureTreeViewItemAdv parent)
        {
            // Add to this level
            var tvi = new StructureTreeViewItemAdv<T>(_getDisplayMemberForLeafItem(item), _leftItemImage);
            tvi.TreeParent = parent;
            tvi.Level = this;
            parent.Items.Add(tvi);
            parent.SortTreeViewItem();
            return tvi;
        }

        public void Remove(StructureTreeViewItemAdv tvi)
        {
            tvi.TreeParent.Items.Remove(tvi);
        }


        public LeafLevel(Func<T, DisplayMemberModel<T>> getDisplayMemberForLeafItem, ImageSource leftItemImage = null)
        {
            _getDisplayMemberForLeafItem = getDisplayMemberForLeafItem;
            _leftItemImage = leftItemImage;
        }
    }

    public class ExpandableLeafLevel<T> : ITreeLevel<T> where T : BindableBase
    {
        private Func<T, DisplayMemberModel<T>> _getDisplayMemberForLeafItem;

        public event EventHandler<AlwaysExpandableTreeViewItemAdv<T>> TreeViewItemExpanded;

        private bool _removeItemsCascadeIfTheyGetEmpty;


        public StructureTreeViewItemAdv<T> Add(T item, StructureTreeViewItemAdv parent)
        {
            // Add to this level
            var tvi = new AlwaysExpandableTreeViewItemAdv<T>(_getDisplayMemberForLeafItem(item), _removeItemsCascadeIfTheyGetEmpty);
            tvi.TreeParent = parent;
            tvi.Level = this;
            parent.Items.Add(tvi);
            parent.SortTreeViewItem();
            tvi.Expanded += ExpandableLeafItem_Expanded;
            return tvi;
        }
        
        public void Remove(StructureTreeViewItemAdv tvi)
        {
            tvi.TreeParent.Items.Remove(tvi);
            tvi.Expanded -= ExpandableLeafItem_Expanded;
        }

        private void ExpandableLeafItem_Expanded(object sender, System.Windows.RoutedEventArgs e)
        {
            TreeViewItemExpanded?.Invoke(this, sender as AlwaysExpandableTreeViewItemAdv<T>);
        }


        public ExpandableLeafLevel(Func<T, DisplayMemberModel<T>> getDisplayMemberForLeafItem, bool removeItemsCascadeIfTheyGetEmpty = false)
        {
            _getDisplayMemberForLeafItem = getDisplayMemberForLeafItem;
            _removeItemsCascadeIfTheyGetEmpty = removeItemsCascadeIfTheyGetEmpty;
        }
    }
}
