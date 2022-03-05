using FrameworksAndDrivers.Gui.Wpf.TreeStructure;

namespace TestHelper.Mock
{
    public class ITreeLevelMock<T> : ITreeLevel<T>
    {
        public bool WasAddInvoked { get; private set; }
        public StructureTreeViewItemAdv<T> AddReturnValue;
        public T AddParameterItem { get; private set; }
        public StructureTreeViewItemAdv AddParameterParent { get; private set; }
        
        public bool WasRemoveInvoked { get; private set; }
        public StructureTreeViewItemAdv RemoveParameterTvi { get; private set; }

        public StructureTreeViewItemAdv<T> Add(T item, StructureTreeViewItemAdv parent)
        {
            WasAddInvoked = true;
            AddParameterItem = item;
            AddParameterParent = parent;
            return AddReturnValue;
        }

        public void Remove(StructureTreeViewItemAdv tvi)
        {
            WasRemoveInvoked = true;
            RemoveParameterTvi = tvi;
        }
    }
}
