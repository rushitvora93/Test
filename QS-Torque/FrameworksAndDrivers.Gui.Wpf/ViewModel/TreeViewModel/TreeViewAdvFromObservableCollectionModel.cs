using FrameworksAndDrivers.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using InterfaceAdapters;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel.TreeViewModel
{
    public class HeaderData
    {
        public String Header;

        public override bool Equals(object obj)
        {
            if (obj is HeaderData other)
            {
                return other.Header.Equals(Header);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Header.GetHashCode();
        }

        public HeaderData(string header)
        {
            Header = header;
        }
    }

    public class TranslateableHeaderData : HeaderData
    {
        public TranslateableHeaderData(string context, string header) : base(header)
        {
            Context = context;
        }        
        public string Context { get; set; }        
    }

    public class TreeObservableCollection : ObservableCollection<TreeViewItemAdvModel>
    {
        public TreeViewItemAdvModel Parent { get; set; }
        public TreeObservableCollection(TreeViewItemAdvModel parent)
        {
            Parent = parent;
        }
        protected override void InsertItem(int index, TreeViewItemAdvModel item)
        {
            item.Parent = this.Parent;
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, TreeViewItemAdvModel item)
        {
            item.Parent = this.Parent;
            base.SetItem(index, item);
        }
    }

    abstract public class TreeViewItemAdvModel : BindableBase
    {
        public TreeObservableCollection SubItems { get; set; }

        private string _header;
        public string Header
        {
            get => _header;
            set => Set(ref _header, value);
        }
        public bool IsCheckable { get; set; }

        public TreeViewItemAdvModel Parent { get; set; }

        public HeaderData HeaderData { get; set; }

        public TreeViewItemAdvModel(HeaderData headerData = null)
        {
            SubItems = new TreeObservableCollection(this);
            HeaderData = headerData;
            if (HeaderData != null)
            {
                Header = headerData.Header;
            }
        }

        public List<HeaderData> GetHeaderList(bool includethisitem)
        {
            List<HeaderData> erg = new List<HeaderData>();
            if (includethisitem)
            {
                erg.Add(this.HeaderData);
            }
            var parent = Parent;
            while (parent != null && parent.HeaderData != null)
            {
                erg.Add(parent.HeaderData);
                parent = parent.Parent;
            }
            erg.Reverse();
            return erg;
        }
    }

    public class TreeViewItemAdvModelHeader : TreeViewItemAdvModel
    {
        public TreeViewItemAdvModelHeader(HeaderData headerData) : base(headerData)
        {
        }
    }

    public class TreeViewItemAdvModelTranslateableHeader : TreeViewItemAdvModelHeader
    {
        public TreeViewItemAdvModelTranslateableHeader(TranslateableHeaderData headerData) : base(headerData)
        {

        }
    }

    public class TreeViewItemAdvModelEntry<T> : TreeViewItemAdvModel
    {
        public TreeViewItemAdvModelEntry(T entry)
        {
            Entry = entry;
        }
        public T Entry { get; set; }
    }

    public interface ITreeModelAdvFromObserverablecolleciontModelCache<T>
    {
        Dictionary<List<HeaderData>, TreeViewItemAdvModelHeader> ListToLowestPathModel { get; set; }
        Dictionary<T, TreeViewItemAdvModelEntry<T>> TToLowestPathmodel { get; set; }

        List<TreeViewItemAdvModelTranslateableHeader> TranslateableTreeItems { get; set; }
    }

    public sealed class ListComparer : EqualityComparer<List<HeaderData>>
    {
        public override bool Equals(List<HeaderData> x, List<HeaderData> y)
          => StructuralComparisons.StructuralEqualityComparer.Equals(x?.ToArray(), y?.ToArray());

        public override int GetHashCode(List<HeaderData> x)
          => StructuralComparisons.StructuralEqualityComparer.GetHashCode(x?.ToArray());
    }


    public class TreeModelAdvFromObserverablecolleciontModelCache<T> : ITreeModelAdvFromObserverablecolleciontModelCache<T>

    {
        public Dictionary<List<HeaderData>, TreeViewItemAdvModelHeader> ListToLowestPathModel { get; set; }
        public Dictionary<T, TreeViewItemAdvModelEntry<T>> TToLowestPathmodel { get; set; }
        public List<TreeViewItemAdvModelTranslateableHeader> TranslateableTreeItems { get; set; }

        public TreeModelAdvFromObserverablecolleciontModelCache()
        {
            ListToLowestPathModel = new Dictionary<List<HeaderData>, TreeViewItemAdvModelHeader>(new ListComparer());
            TToLowestPathmodel = new Dictionary<T, TreeViewItemAdvModelEntry<T>>();
            TranslateableTreeItems = new List<TreeViewItemAdvModelTranslateableHeader>();
        }
    }

    public class TreeViewAdvFromObservableCollectionModel<T> where T : BindableBase
    {
        private LocalizationWrapper _localizationWrapper { get; set; }
        private TreeViewItemAdvModelHeader _root { get; set; }
        private Func<T, List<HeaderData>> _getPathHeaderDataList { get; set; }
        private Func<T, string> _getItemString { get; set; }
        private ITreeModelAdvFromObserverablecolleciontModelCache<T> _cache { get; set; }
        public TreeObservableCollection Source { get { return _root.SubItems; } set { _root.SubItems = value; } }

        private TreeViewItemAdvModelHeader GetTreeViewItemAdvModelHeader(HeaderData headerData)
        {
            if (headerData is TranslateableHeaderData transHeaderData)
            {
                return new TreeViewItemAdvModelTranslateableHeader(transHeaderData);
            }
            return new TreeViewItemAdvModelHeader(headerData);
        }

        private TreeViewItemAdvModelHeader AddTreeViewItemsFromHeaderDataList(List<HeaderData> pathList)
        {
            var start = _root;
            for (int i = 0; i < pathList.Count; ++i)
            {
                TreeViewItemAdvModelHeader tvitem = null;
                var currentRange = pathList.GetRange(0, i + 1);
                if (_cache.ListToLowestPathModel.ContainsKey(currentRange))
                {
                    start = _cache.ListToLowestPathModel[currentRange];
                }
                else
                {
                    tvitem = GetTreeViewItemAdvModelHeader(pathList[i]);
                    if (tvitem is TreeViewItemAdvModelTranslateableHeader transHeader)
                    {
                        _cache.TranslateableTreeItems.Add(transHeader);
                    }
                    start.SubItems.Add(tvitem);
                    start = tvitem;
                    _cache.ListToLowestPathModel.Add(currentRange, tvitem);
                }
            }
            return start;
        }
        private void HandleAdd(IList<T> items)
        {
            foreach (var item in items)
            {                
                var pathlist = _getPathHeaderDataList(item);
                var node = AddTreeViewItemsFromHeaderDataList(pathlist);
                var entry = new TreeViewItemAdvModelEntry<T>(item) { Header = _getItemString(item) };
                node.SubItems.Add(entry);
                _cache.TToLowestPathmodel.Add(item, entry);
                item.PropertyChanged += NotifyChangedEventHandler;
            }
        }

        private void RemoveEmptyTreeViewItemsFromBottom(TreeViewItemAdvModel start)
        {
            var parent = start;
            while (parent != null && parent.SubItems.Count == 0)
            {
                var tparent = parent.Parent;
                if (tparent != null)
                {
                    _cache.ListToLowestPathModel.Remove(parent.GetHeaderList(true));
                    tparent.SubItems.Remove(parent);
                    if (parent is TreeViewItemAdvModelTranslateableHeader trsparent)
                    {
                        _cache.TranslateableTreeItems.Remove(trsparent);
                    }
                }
                parent = tparent;
            }
        }

        private void HandleRemove(IList<T> items)
        {
            foreach (var item in items)
            {
                if (_cache.TToLowestPathmodel.ContainsKey(item))
                {
                    var tvitem = _cache.TToLowestPathmodel[item];
                    if (tvitem.Parent != null)
                    {                        
                        if (tvitem.Parent.SubItems.Count < 2)
                        {
                            var parent = tvitem.Parent;
                            parent.SubItems.Clear();
                            RemoveEmptyTreeViewItemsFromBottom(parent);
                        }
                        else
                        {
                            tvitem.Parent.SubItems.Remove(tvitem);
                        }
                        _cache.TToLowestPathmodel.Remove(item);
                        item.PropertyChanged -= NotifyChangedEventHandler;
                    }                    
                }
            }
        }

        private void HandleClear()
        {
            Source.Clear();
            _cache.ListToLowestPathModel.Clear();
            _cache.TToLowestPathmodel.Clear();
            _cache.TranslateableTreeItems.Clear();
        }

        private void HandleReplace(IList<T> olditems, IList<T> newitems)
        {
            HandleRemove(olditems);
            HandleAdd(newitems);            
        }

        private void NotifyCollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(sender is ObservableCollection<T> collection)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        if (e.NewItems != null)
                        {
                            HandleAdd(e.NewItems.Cast<T>().ToList());
                        }
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        if (e.OldItems != null)
                        {
                            HandleRemove(e.OldItems.Cast<T>().ToList());
                        }
                        break;

                    case NotifyCollectionChangedAction.Replace:
                        if (e.NewItems != null && e.OldItems != null)
                        {
                            HandleReplace(e.OldItems.Cast<T>().ToList(), e.NewItems.Cast<T>().ToList());
                        }
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        {
                            HandleClear();
                        }
                        break;
                }
            }
        }

        private void NotifyChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            if (sender is T item)
            {
                TreeViewItemAdvModelEntry<T> tvitem = null;
                if (_cache.TToLowestPathmodel.TryGetValue(item, out tvitem))
                {
                    var newlist = _getPathHeaderDataList(item);
                    var oldlist = tvitem.Parent.GetHeaderList(true);
                    var comparer = new ListComparer();
                    if (!comparer.Equals(oldlist,newlist) && tvitem.Parent != null)
                    {
                        var parent = tvitem.Parent;
                        parent.SubItems.Remove(tvitem);
                        RemoveEmptyTreeViewItemsFromBottom(parent);
                        var node = AddTreeViewItemsFromHeaderDataList(newlist);
                        node.SubItems.Add(tvitem);
                    }
                    tvitem.Header = _getItemString(item);
                }
            }
        }        

        public TreeViewAdvFromObservableCollectionModel(ObservableCollection<T> collection, Func<T, List<HeaderData> > getpathheaderdatalist, Func<T,String> getitemstring, LocalizationWrapper localizationWrapper, ITreeModelAdvFromObserverablecolleciontModelCache<T> cache = null)
        {
            _cache = cache;
            if (cache == null)
            {
                _cache = new TreeModelAdvFromObserverablecolleciontModelCache<T>();
            }
            collection.CollectionChanged += NotifyCollectionChangedEventHandler;
            _root = new TreeViewItemAdvModelHeader(null);
            _getPathHeaderDataList = getpathheaderdatalist;
            _getItemString = getitemstring;
            _localizationWrapper = localizationWrapper;
        }

        public void UpdateTranslation()
        {
            foreach(var tvitem in _cache.TranslateableTreeItems)
            {
                if (tvitem.HeaderData is TranslateableHeaderData hd)
                {
                    tvitem.Header = _localizationWrapper.Strings.GetParticularString(hd.Context, hd.Header);
                }
            } 
        }
    }
}
