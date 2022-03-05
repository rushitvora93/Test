using Syncfusion.Windows.Tools.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InterfaceAdapters;

namespace FrameworksAndDrivers.Gui.Wpf.TreeStructure
{
    public class TreeStructure<T> : TreeBase<T> where T : BindableBase
    {

        public virtual ISourceLevel<T> SourceLevel { get; private set; }

        public override ObservableCollection<TreeViewItemAdv> Source => SourceLevel.TreeViewItems;
        

        #region Methods
        protected override void ProtectedAdd(T item)
        {
            var tvi = SourceLevel.Add(item);
            _cache.Add(item, tvi);
        }
        #endregion


        public TreeStructure(ObservableCollection<T> list, ISourceLevel<T> sourceLevel, List<string> namesOfTreeChangingAttributes, TreeViewAdv treeViewAdvControl, bool allowEmptyNodes = true, bool isTreeSorted = true) : base(list, allowEmptyNodes, treeViewAdvControl, isTreeSorted, namesOfTreeChangingAttributes)
        {
            SourceLevel = sourceLevel;
        }
    }
}
