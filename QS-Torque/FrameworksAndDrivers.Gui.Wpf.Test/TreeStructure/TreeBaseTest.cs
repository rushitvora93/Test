using System;
using System.Collections.ObjectModel;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.TreeStructure;
using InterfaceAdapters;
using NUnit.Framework;
using Syncfusion.Windows.Tools.Controls;
using TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.TreeStructure
{
    class TreeBaseTest
    {
        private class TestClass : BindableBase
        {
            public string Text { get; set; }
            public string LevelText;

            public string LevelTextProperty
            {
                get => LevelText;
                set => Set(ref LevelText, value);
            }
        }

        private class TreeBaseMock : TreeBase<TestClass>
        {
            public override ObservableCollection<TreeViewItemAdv> Source => throw new NotImplementedException();
            
            protected override void ProtectedAdd(TestClass item)
            {
                AddToCache(item, new StructureTreeViewItemAdv<TestClass>(new DisplayMemberModel<TestClass>(item, x => x.Text))
                {
                    Level = new ITreeLevelMock<TestClass>()
                });
            }

            public void AddToCache(TestClass key, StructureTreeViewItemAdv<TestClass> value)
            {
                _cache.Add(key, value);
            }

            public TreeBaseMock(ObservableCollection<TestClass> list) : base(list, false, null, false) { }
        }

        TreeBaseMock _tree;
        ObservableCollection<TestClass> _itemList;


        [SetUp]
        public void TreeStructureSetUp()
        {
            _itemList = new ObservableCollection<TestClass>();
            _tree = new TreeBaseMock(_itemList);
        }


        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void AddAddsItemToItemsList()
        {
            var item = new TestClass() { Text = "ghrueidlc,bh" };

            _tree.Add(item);

            Assert.IsTrue(_itemList.Contains(item));
        }

        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void RemoveRemovesItemToItemsList()
        {
            var item = new TestClass() { Text = "ghrueidlc,bh" };
            _itemList.Add(item);

            _tree.Remove(item);

            Assert.IsFalse(_itemList.Contains(item));
        }

        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void ExpandToItemTest()
        {
            var item = new TestClass() { Text = "ghrueidlc,bh" };
            var parentParentParentTvi = new StructureTreeViewItemAdv();
            var parentParentTvi = new StructureTreeViewItemAdv() { TreeParent = parentParentParentTvi };
            var parentTvi = new StructureTreeViewItemAdv() { TreeParent = parentParentTvi };
            var tvi = new StructureTreeViewItemAdv<TestClass>(new Model.DisplayMemberModel<TestClass>(item, x => x.Text)) { TreeParent = parentTvi };
            
            _tree.AddToCache(item, tvi);
            _tree.ExpandToItem(item);

            Assert.IsTrue(parentTvi.IsExpanded);
            Assert.IsTrue(parentParentTvi.IsExpanded);
            Assert.IsTrue(parentParentParentTvi.IsExpanded);
        }

        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void CollapsedToItemTest()
        {
            var item = new TestClass() { Text = "ghrueidlc,bh" };
            var parentParentParentTvi = new StructureTreeViewItemAdv() { IsExpanded = true };
            var parentParentTvi = new StructureTreeViewItemAdv() { TreeParent = parentParentParentTvi, IsExpanded = true };
            var parentTvi = new StructureTreeViewItemAdv() { TreeParent = parentParentTvi, IsExpanded = true };
            var tvi = new StructureTreeViewItemAdv<TestClass>(new Model.DisplayMemberModel<TestClass>(item, x => x.Text)) { TreeParent = parentTvi };

            _tree.AddToCache(item, tvi);
            _tree.CollapseToItem(item);

            Assert.IsFalse(parentTvi.IsExpanded);
            Assert.IsFalse(parentParentTvi.IsExpanded);
            Assert.IsFalse(parentParentParentTvi.IsExpanded);
        }

        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void SelectItemTest()
        {
            var item = new TestClass() { Text = "ghrueidlc,bh" };
            var parentParentParentTvi = new StructureTreeViewItemAdv();
            var parentParentTvi = new StructureTreeViewItemAdv() { TreeParent = parentParentParentTvi };
            var parentTvi = new StructureTreeViewItemAdv() { TreeParent = parentParentTvi };
            var tvi = new StructureTreeViewItemAdv<TestClass>(new Model.DisplayMemberModel<TestClass>(item, x => x.Text)) { TreeParent = parentTvi };

            _tree.AddToCache(item, tvi);
            _tree.Select(item);

            Assert.IsTrue(tvi.IsSelected);
            Assert.IsTrue(parentTvi.IsExpanded);
            Assert.IsTrue(parentParentTvi.IsExpanded);
            Assert.IsTrue(parentParentParentTvi.IsExpanded);
        }

        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void RemoveFromItemSourceTest()
        {
            var item = new TestClass() { Text = "nbhgrjieuokdlckvm" };
            _itemList.Add(item);
            var tvi = _tree.GetContainerForItem(item) as StructureTreeViewItemAdv<TestClass>;

            _itemList.Remove(item);

            Assert.AreEqual(item, (tvi).DisplayMember.Item);
            Assert.IsFalse(_itemList.Contains(item));
        }
    }
}
