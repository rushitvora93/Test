using FrameworksAndDrivers.Gui.Wpf.TreeStructure;
using NUnit.Framework;
using System.Threading;
using InterfaceAdapters;
using TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.TreeStructure
{
    class LeafLevelTest
    {
        private class TestClass : BindableBase
        {
            public string Text { get; set; }
        }

        private LeafLevel<TestClass> _leafLevel;
        private ExpandableLeafLevel<TestClass> _expandableLeafLevel;
        private TestClass _item;


        [SetUp]
        public void LeafLevelSetUp()
        {
            _leafLevel = new LeafLevel<TestClass>(x => new Model.DisplayMemberModel<TestClass>(x, o => o.Text));
            _expandableLeafLevel = new ExpandableLeafLevel<TestClass>(x => new Model.DisplayMemberModel<TestClass>(x, o => o.Text));
            _item = new TestClass() { Text = "hgtz5u7r89eopsdlc,vmbn" };
        }

        
        [Test, RequiresThread(ApartmentState.STA)]
        public void AddTest()
        {
            StructureTreeViewItemAdv<TestClass> parent = new StructureTreeViewItemAdv<TestClass>(new Model.DisplayMemberModel<TestClass>(x => x.Text));

            var tvi = _leafLevel.Add(_item, parent);

            Assert.AreEqual(_item.Text, tvi.Header);
            Assert.AreEqual(parent, tvi.TreeParent);
            Assert.AreEqual(_leafLevel, tvi.Level);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ExpandableLeafLevelAddTest()
        {
            StructureTreeViewItemAdv<TestClass> parent = new StructureTreeViewItemAdv<TestClass>(new Model.DisplayMemberModel<TestClass>(x => x.Text));

            var tvi = _expandableLeafLevel.Add(_item, parent);

            Assert.AreEqual(typeof(AlwaysExpandableTreeViewItemAdv<TestClass>), tvi.GetType());
            Assert.AreEqual(_item.Text, tvi.Header);
            Assert.AreEqual(parent, tvi.TreeParent);
            Assert.AreEqual(_expandableLeafLevel, tvi.Level);
        }

        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void ExpandableLeafLevelRemoveItemFromParentWithMoreItems()
        {
            var parentLevel = new ITreeLevelMock<TestClass>();

            var parentTvi = new StructureTreeViewItemAdv();
            parentTvi.Level = parentLevel;
            parentTvi.Items.Add(new StructureTreeViewItemAdv());

            var tvi = _expandableLeafLevel.Add(_item, parentTvi);

            _leafLevel.Remove(tvi);

            Assert.AreEqual(1, parentTvi.Items.Count);
            Assert.IsFalse(parentLevel.WasRemoveInvoked);
        }
    }
}
