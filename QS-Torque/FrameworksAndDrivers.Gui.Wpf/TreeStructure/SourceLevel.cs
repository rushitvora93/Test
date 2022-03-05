using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.ObjectModel;
using InterfaceAdapters.Localization;

namespace FrameworksAndDrivers.Gui.Wpf.TreeStructure
{
    public interface ISourceLevel<T> : ITreeLevel
    {
        ObservableCollection<TreeViewItemAdv> TreeViewItems { get; }

        StructureTreeViewItemAdv<T> Add(T item);
    }

    /// <summary>
    /// Level with only one static node at the beginning
    /// </summary>
    public class RootLevel<T> : ISourceLevel<T>, IGetUpdatedByLanguageChanges
    {
        private StructureTreeViewItemAdv _rootNode;
        private Func<string> _getHeader;

        public ITreeLevel<T> SubLevel { get; set; }
        public ObservableCollection<TreeViewItemAdv> TreeViewItems { get; private set; }

        public StructureTreeViewItemAdv<T> Add(T item)
        {
            return SubLevel.Add(item, _rootNode);
        }

        public void Remove(StructureTreeViewItemAdv item)
        {
            // Do nothing
        }

        public void LanguageUpdate()
        {
            _rootNode.Header = _getHeader();
        }

        public void CollapseRoot()
        {
            _rootNode.IsExpanded = false;
        }

        public RootLevel(Func<string> getHeader, ILocalizationWrapper localization)
        {
            _getHeader = getHeader;
            _rootNode = new StructureTreeViewItemAdv()
            {
                Header = _getHeader(),
                Level = this
            };
            TreeViewItems = new ObservableCollection<TreeViewItemAdv>();
            TreeViewItems.Add(_rootNode);
            localization.Subscribe(this);
        }
    }
}
