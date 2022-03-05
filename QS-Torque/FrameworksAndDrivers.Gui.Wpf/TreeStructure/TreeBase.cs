using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using InterfaceAdapters;
using Syncfusion.Windows.Tools;
using Syncfusion.Windows.Tools.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.TreeStructure
{
    public abstract class TreeBase<T>
    {
        public abstract ObservableCollection<TreeViewItemAdv> Source { get; }
        protected bool _allowEmptyNodes;
        private bool _isTreeSorted;
        private List<string> _namesOfTreeChangingAttributes;

        public ObservableCollection<T> ItemList { get; set; }
        protected Dictionary<T, StructureTreeViewItemAdv<T>> _cache;
        protected TreeViewAdv _treeViewAdvControl;
        

        protected abstract void ProtectedAdd(T item);


        #region Methods
        public virtual void Add(T item)
        {
            ItemList.Add(item);
        }

        public virtual void Remove(T item)
        {
            ItemList.Remove(item);
        }

        private void PrivateRemove(T item)
        {
            if (item as BindableBase != null)
            {
                (item as BindableBase).PropertyChanged -= Item_PropertyChanged; 
            }

            if (_cache.ContainsKey(item))
            {
                _cache[item].Remove(!_allowEmptyNodes); 
            }
            _cache.Remove(item);
        }

        public virtual TreeViewItemAdv GetContainerForItem(T item)
        {
            if (!_cache.Keys.Contains(item))
            {
                return null;
            }

            return _cache[item];
        }

        public virtual void Select(T item)
        {
            ExpandToItem(item);
            _cache[item].IsSelected = true;
        }

        public virtual void ExpandToItem(T item)
        {
            var tvi = _cache[item].TreeParent;

            while (tvi != null)
            {
                tvi.IsExpanded = true;
                tvi = tvi.TreeParent;
            }
        }

        public void ResetOpacity()
        {
            foreach (var structureTreeViewItemAdv in _cache)
            {
                structureTreeViewItemAdv.Value.Opacity = 1.0;
            }
        }

        public virtual void CollapseToItem(T item)
        {
            var tvi = _cache[item].TreeParent;

            while (tvi != null)
            {
                tvi.IsExpanded = false;
                tvi = tvi.TreeParent;
            }
        }

        protected void BringIntoView(StructureTreeViewItemAdv<T> e)
        {
            _treeViewAdvControl?.BringIntoView(e);
        }
        #endregion


        #region Event-Handler

        private void ItemList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (T i in e.NewItems)
                    {
                        ProtectedAdd(i);
                        if (i as BindableBase != null)
                        {
                            (i as BindableBase).PropertyChanged += Item_PropertyChanged;
                        }
                        if (_isTreeSorted)
                        {
                            _treeViewAdvControl?.SortTreeView();
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (T i in e.OldItems)
                    {
                        PrivateRemove(i);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    foreach (T i in e.OldItems)
                    {
                        PrivateRemove(i);
                    }
                    foreach (T i in e.NewItems)
                    {
                        ProtectedAdd(i);
                        if (i as BindableBase != null)
                        {
                            (i as BindableBase).PropertyChanged += Item_PropertyChanged;
                        }
                    }
                    if (_isTreeSorted)
                    {
                        _treeViewAdvControl.SortTreeView();
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    foreach (var i in _cache.Keys.ToList())
                    {
                        PrivateRemove(i);
                    }
                    break;
            }
        }

        protected virtual void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_namesOfTreeChangingAttributes?.Contains(e.PropertyName) ?? false)
            {
                var item = (T)sender;
                bool wasItemSelected = _cache[item].IsSelected;

                if (wasItemSelected)
                {
                    CollapseToItem(item);
                }

                Remove(item);
                Add(item);

                if (wasItemSelected)
                {
                    Select(item);
                }

                BringIntoView(_cache[item]);
            }
        }
        #endregion


        public TreeBase(ObservableCollection<T> list, bool allowEmptyNodes, TreeViewAdv treeViewAdvControl, bool isTreeSorted, List<string> namesOfTreeChangingAttributes = null)
        {
            _allowEmptyNodes = allowEmptyNodes;
            _cache = new Dictionary<T, StructureTreeViewItemAdv<T>>();
            _treeViewAdvControl = treeViewAdvControl;
            _isTreeSorted = isTreeSorted;
            _namesOfTreeChangingAttributes = namesOfTreeChangingAttributes;

            if (_treeViewAdvControl != null)
            {
                _treeViewAdvControl.Sorting = isTreeSorted ? SortDirection.Ascending : SortDirection.None;
            }

            if (list != null)
            {
                ItemList = list;
                ItemList.CollectionChanged += ItemList_CollectionChanged; 
            }
        }
    }
}
