using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using FrameworksAndDrivers.Gui.Wpf.Model;
using InterfaceAdapters;
using InterfaceAdapters.Models;
using Syncfusion.Windows.Tools.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.TreeStructure
{
    public class ElementTree<T> : TreeBase<T>
    {
        private ObservableCollection<TreeViewItemAdv> _source;
        public override ObservableCollection<TreeViewItemAdv> Source => _source;

        private IElementTreeSource<T> _elementTreeSource;
        private Func<T, string> _getDisplayMember;
        private ImageSource _leftItemImage;

        public ElementTree(IElementTreeSource<T> elementTreeSource, Func<T, string> getDisplayMember, TreeViewAdv treeViewAdvControl, ImageSource leftItemImage = null, bool isTreeSorted = true, List<string> namesOfTreeChangingAttributes = null) : base(elementTreeSource.TreeElements, true, treeViewAdvControl, isTreeSorted, namesOfTreeChangingAttributes)
        {
            _source = new ObservableCollection<TreeViewItemAdv>();
            _elementTreeSource = elementTreeSource;
            _getDisplayMember = getDisplayMember;
            _leftItemImage = leftItemImage;

            if (elementTreeSource.TreeElements.Count > 0)
            {
                InitializeTree();
            }
        }

        protected override void ProtectedAdd(T item)
        {
            var parents = _elementTreeSource.GetTreeEdgesFor(new List<T>() {item});

            if (!parents.ContainsKey(item))
            {
                throw new ArgumentException($"The parent for {_getDisplayMember(item)} is not set yet");
            }

            var tvi = new StructureTreeViewItemAdv<T>(new DisplayMemberModel<T>(item, _getDisplayMember))
            {
                IsEditable = false,
                LeftImageSource = _leftItemImage
            };

            _cache.Add(item, tvi);
            if (parents[item] == null)
            {
                this.Source.Add(tvi);
            }
            else
            {
                _cache[parents[item]].Items.Add(tvi);
                tvi.TreeParent = _cache[parents[item]];
            }
        }

        protected override void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = (T)sender;
            var tvi = _cache[item];

            tvi.Remove(!_allowEmptyNodes);

            var parents = _elementTreeSource.GetTreeEdgesFor(new List<T>() { item });

            if (!parents.ContainsKey(item))
            {
                throw new ArgumentException($"The parent for {_getDisplayMember(item)} is not set yet");
            }

            if (parents[item] == null)
            {
                this.Source.Add(tvi);
            }
            else
            {
                _cache[parents[item]].Items.Add(tvi);
                tvi.TreeParent = _cache[parents[item]];
            }
        }

        private void InitializeTree()
        {
            var edges = _elementTreeSource.GetTreeEdgesFor(_elementTreeSource.TreeElements);
            var elements = _elementTreeSource.GetElementsTopologicalSorted(_elementTreeSource.TreeElements, edges);

            foreach (var e in elements)
            {
                if (e as BindableBase != null)
                {
                    (e as BindableBase).PropertyChanged += Item_PropertyChanged;
                }

                if (!edges.ContainsKey(e) || edges[e] == null)
                {
                    var tvi = new StructureTreeViewItemAdv<T>(new DisplayMemberModel<T>(e, _getDisplayMember))
                    {
                        IsEditable = false,
                        LeftImageSource = _leftItemImage
                    };
                    _cache.Add(e, tvi);
                    _source.Add(tvi);
                }
                else
                {
                    var tvi = new StructureTreeViewItemAdv<T>(new DisplayMemberModel<T>(e, _getDisplayMember))
                    {
                        IsEditable = false,
                        LeftImageSource = _leftItemImage
                    };
                    _cache.Add(e, tvi);
                    
                    if (edges.ContainsKey(e))
                    {
                        _cache[edges[e]].Items.Add(tvi);
                        tvi.TreeParent = _cache[edges[e]];
                    }
                }
            }
        }
    }
}
