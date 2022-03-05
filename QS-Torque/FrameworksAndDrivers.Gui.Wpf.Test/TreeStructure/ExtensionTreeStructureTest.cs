using FrameworksAndDrivers.Gui.Wpf.TreeStructure;
using NUnit.Framework;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InterfaceAdapters;
using TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.TreeStructure
{
    class ExtensionTreeStructureTest
    {
        private class TestClass : BindableBase
        {
            public string Text { get; set; }
            public SubStructureClass SubStructureClass { get; set; }
        }

        private class SubStructureClass : BindableBase
        {
            public string SubStructureString { get; set; }
        }

        private class TreeStructureMock : TreeStructure<SubStructureClass>
        {
            public bool WasAddInvoked { get; private set; }
            public string AddParameterItem { get; private set; }

            public override ObservableCollection<TreeViewItemAdv> Source => throw new NotImplementedException();

            protected override void ProtectedAdd(SubStructureClass item)
            {
                WasAddInvoked = true;
                _cache.Add(item, new StructureTreeViewItemAdv<SubStructureClass>(new Model.DisplayMemberModel<SubStructureClass>(item, x => x.SubStructureString)));
            }

            public void AddToCache(SubStructureClass item, StructureTreeViewItemAdv<SubStructureClass> tvi)
            {
                _cache.Add(item, tvi);
            }

            public TreeStructureMock(ObservableCollection<SubStructureClass> list, List<string> namesOfLevelAttributes, TreeViewAdv treeViewAdvControl) : base(list, null, namesOfLevelAttributes, treeViewAdvControl)
            {
                
            }
        }

        ExtensionTreeStructure<TestClass, SubStructureClass> _extensionTreeStructure;
        TreeStructureMock _subTree;
        ITreeLevelMock<TestClass> _sourceLevel;
        ObservableCollection<TestClass> _itemList;


        [SetUp]
        public void ExtensionTreeStructureSetUp()
        {
            _itemList = new ObservableCollection<TestClass>();
            _subTree = new TreeStructureMock(new ObservableCollection<SubStructureClass>(), new List<string>(), null);
            _sourceLevel = new ITreeLevelMock<TestClass>();
            _extensionTreeStructure = new ExtensionTreeStructure<TestClass, SubStructureClass>(_itemList, _sourceLevel, _subTree, x => x.SubStructureClass, new List<string>() { nameof(TestClass.SubStructureClass) }, null, false);
        }


        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void ConstructorTest()
        {
            Assert.AreEqual(_sourceLevel, _extensionTreeStructure.SourceLevel);
            Assert.AreEqual(_itemList, _extensionTreeStructure.ItemList);
        }

        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void AddItemWithExistingSubTreeItemTest()
        {
            var item = new TestClass() { Text = "nhgjufkdc,", SubStructureClass = new SubStructureClass() { SubStructureString = "nhjuirelkd,kcmv" } };
            var tvi = new StructureTreeViewItemAdv<SubStructureClass>(new Model.DisplayMemberModel<SubStructureClass>(item.SubStructureClass, x => x.SubStructureString));
            
            _extensionTreeStructure.Add(item);

            Assert.IsTrue(_sourceLevel.WasAddInvoked);
            Assert.AreEqual(item, _sourceLevel.AddParameterItem);
            Assert.AreEqual(tvi.DisplayMember.Item, (_sourceLevel.AddParameterParent as StructureTreeViewItemAdv<SubStructureClass>).DisplayMember.Item);
        }

        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void AddItemWithoutExistingSubTreeItemTest()
        {
            var item = new TestClass() { Text = "nhgjufkdc,", SubStructureClass = new SubStructureClass() { SubStructureString = "nhjuirelkd,kcmv" } };

            _extensionTreeStructure.Add(item);

            Assert.IsTrue(_subTree.WasAddInvoked);
            Assert.IsTrue(_sourceLevel.WasAddInvoked);
            Assert.AreEqual(item, _sourceLevel.AddParameterItem);
            Assert.AreEqual(_subTree.GetContainerForItem(item.SubStructureClass), _sourceLevel.AddParameterParent);
        }
    }
}
