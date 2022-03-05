using FrameworksAndDrivers.Gui.Wpf.TreeStructure;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InterfaceAdapters;
using TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.TreeStructure
{
    class TreeStructureTest
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

        TreeStructure<TestClass> _treeStructure;
        RootLevel<TestClass> _rootLevel;
        ITreeLevelMock<TestClass> _subLevel;
        ObservableCollection<TestClass> _itemList;


        [SetUp]
        public void TreeStructureSetUp()
        {
            _itemList = new ObservableCollection<TestClass>();
            _subLevel = new ITreeLevelMock<TestClass>();
            _rootLevel = new RootLevel<TestClass>(() => "", new NullLocalizationWrapper())
            {
                SubLevel = _subLevel
            };
            _treeStructure = new TreeStructure<TestClass>(_itemList, _rootLevel, new List<string>() { nameof(TestClass.LevelTextProperty) }, null);
        }


        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void SourceTest()
        {
            Assert.AreEqual(_rootLevel.TreeViewItems, _treeStructure.Source);
        }
        
        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void AddToItemListTest()
        {
            var item = new TestClass() { Text = "nbhgrjieuokdlckvm" };
            _subLevel.AddReturnValue = new StructureTreeViewItemAdv<TestClass>(new Model.DisplayMemberModel<TestClass>(x => x.Text));
            _itemList.Add(item);

            Assert.AreEqual(item, _subLevel.AddParameterItem);
            Assert.AreEqual(1, _itemList.Count(x => x == item));
        }

        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void AddTest()
        {
            var item = new TestClass() { Text = "nbhgrjieuokdlckvm" };
            _subLevel.AddReturnValue = new StructureTreeViewItemAdv<TestClass>(new Model.DisplayMemberModel<TestClass>(x => x.Text));
            _treeStructure.Add(item);

            Assert.AreEqual(item, _subLevel.AddParameterItem);
            Assert.AreEqual(1, _itemList.Count(x => x == item));
        }

        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void RemoveTest()
        {
            var item = new TestClass() { Text = "nbhgrjieuokdlckvm" };
            _subLevel.AddReturnValue = new StructureTreeViewItemAdv<TestClass>(new Model.DisplayMemberModel<TestClass>(x => x.Text))
            {
                Level = _subLevel
            };
            _treeStructure.Add(item);
            _subLevel.AddReturnValue.DisplayMember.Item = _subLevel.AddParameterItem;
            _subLevel.AddReturnValue.TreeParent = _subLevel.AddParameterParent;

            _treeStructure.Remove(item);

            Assert.AreEqual(item, (_subLevel.RemoveParameterTvi as StructureTreeViewItemAdv<TestClass>).DisplayMember.Item);
            Assert.IsFalse(_itemList.Contains(item));
        }

        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void ChangeNotSelectedItemTest()
        {
            var item = new TestClass() { Text = "nbhgrjieuokdlckvm", LevelText = "jreiofpsdölv,bki" };
            var parentTvi = new StructureTreeViewItemAdv();
            var tvi = new StructureTreeViewItemAdv<TestClass>(new Model.DisplayMemberModel<TestClass>(x => x.Text))
            {
                Level = _subLevel,
                TreeParent = parentTvi
            };

            _subLevel.AddReturnValue = tvi;
            _itemList.Add(item);

            _subLevel = new ITreeLevelMock<TestClass>();
            _rootLevel.SubLevel = _subLevel;
            tvi.Level = _subLevel;

            item.LevelTextProperty = "nghfuidkcvnm56526";

            Assert.IsTrue(_subLevel.WasRemoveInvoked);
            Assert.AreEqual(tvi, _subLevel.RemoveParameterTvi);
            Assert.IsTrue(_subLevel.WasAddInvoked);
            Assert.AreEqual(item, _subLevel.AddParameterItem);
        }

        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void ChangeSelectedItemTest()
        {
            var item1 = new TestClass() { Text = "nbhgrjieuokdlckvm", LevelText = "Levelitem 1" };
            var levelTvi1 = new StructureTreeViewItemAdv() { IsExpanded = true };
            var tvi1 = new StructureTreeViewItemAdv<TestClass>(new Model.DisplayMemberModel<TestClass>(x => x.Text))
            {
                Level = _subLevel,
                TreeParent = levelTvi1,
                IsSelected = true
            };

            _subLevel.AddReturnValue = tvi1;
            _itemList.Add(item1);

            var item2 = new TestClass() { Text = "bvfgrtzeuwiskxcmvbn", LevelText = "Levelitem 2" };
            var levelTvi2 = new StructureTreeViewItemAdv();
            var tvi2 = new StructureTreeViewItemAdv<TestClass>(new Model.DisplayMemberModel<TestClass>(x => x.Text))
            {
                Level = _subLevel,
                TreeParent = levelTvi2
            };

            _subLevel.AddReturnValue = tvi2;
            _itemList.Add(item2);

            _subLevel = new ITreeLevelMock<TestClass>();
            _rootLevel.SubLevel = _subLevel;
            tvi1.Level = _subLevel;
            tvi2.Level = _subLevel;
            var newTvi = new StructureTreeViewItemAdv<TestClass>(new Model.DisplayMemberModel<TestClass>(item1, x => x.Text))
            {
                TreeParent = levelTvi2
            };
            _subLevel.AddReturnValue = newTvi;

            item1.LevelTextProperty = item2.LevelText;

            Assert.IsTrue(_subLevel.WasRemoveInvoked);
            Assert.AreEqual(tvi1, _subLevel.RemoveParameterTvi);
            Assert.IsTrue(_subLevel.WasAddInvoked);
            Assert.AreEqual(item1, _subLevel.AddParameterItem);
            Assert.IsFalse(levelTvi1.IsExpanded);
            Assert.IsTrue(levelTvi2.IsExpanded);
            Assert.IsTrue(newTvi.IsSelected);
        }
    }
}
