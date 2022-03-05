using FrameworksAndDrivers.Gui.Wpf.TreeStructure;
using NUnit.Framework;
using System;
using InterfaceAdapters;
using TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.TreeStructure
{
    class InternalLevelTest
    {
        private class TestClass : BindableBase
        {
            public string Text { get; set; }
            public string LevelString { get; set; }
        }
        

        private InternalLevel<TestClass, string> _internalLevel;
        private ITreeLevelMock<TestClass> _subLevel;
        private TestClass _item;
        private StructureTreeViewItemAdv _parent;


        [SetUp]
        public void InternalLevelSetUp()
        {
            _item = new TestClass()
            {
                Text = "ghztu7eodlcvmn",
                LevelString = "bnhtzu489eodlcvk"
            };
            _subLevel = new ITreeLevelMock<TestClass>();
            _parent = new StructureTreeViewItemAdv();

            _internalLevel = new InternalLevel<TestClass, string>(x => x.LevelString,
                                                                  x => new Model.DisplayMemberModel<string>(x, o => o),
                                                                  (x1, x2) => x1.Equals(x2))
            {
                SubLevel = _subLevel
            };
        }


        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void AddWithoutSubLevelTest()
        {
            _internalLevel.SubLevel = null;

            Assert.Catch<InvalidOperationException>(() =>
            {
                _internalLevel.Add(_item, _parent);
            });
        }

        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void AddItemAndNewLevel()
        {
            _internalLevel.Add(_item, _parent);

            Assert.IsTrue(_subLevel.WasAddInvoked);
            Assert.AreEqual(_item, _subLevel.AddParameterItem);
            Assert.AreEqual(_parent, _subLevel.AddParameterParent.TreeParent);
            Assert.AreEqual(_internalLevel, _subLevel.AddParameterParent.Level);
            Assert.IsTrue(_parent.Items.Contains(_subLevel.AddParameterParent));
        }

        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void AddItemButNoNewLevel()
        {
            _internalLevel.Add(_item, _parent);

            var parentNodeForSecondItem = _subLevel.AddParameterParent;
            _subLevel = new ITreeLevelMock<TestClass>();
            _internalLevel.SubLevel = _subLevel;

            var item2 = new TestClass() { Text = "vhncmx,.öe0rij", LevelString = _item.LevelString };
            _internalLevel.Add(item2, _parent);

            Assert.IsTrue(_subLevel.WasAddInvoked);
            Assert.AreEqual(item2, _subLevel.AddParameterItem);
            Assert.AreEqual(parentNodeForSecondItem, _subLevel.AddParameterParent);
        }
    }
}
