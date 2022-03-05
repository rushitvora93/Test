using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.TreeStructure;
using NUnit.Framework;
using System.Threading;
using InterfaceAdapters;

namespace FrameworksAndDrivers.Gui.Wpf.Test.TreeStructure
{
    class StructureTreeViewItemAdvTest
    {
        private class TestClass : BindableBase
        {
            public string Text;
            public string TextProperty
            {
                get => Text;
                set => Set(ref Text, value);
            }
        }

        private class TreeLevelMock : ITreeLevel
        {
            public StructureTreeViewItemAdv RemoveParameter { get; private set; }
            public bool WasRemoveInvoked { get; private set; }

            public void Remove(StructureTreeViewItemAdv tvi)
            {
                RemoveParameter = tvi;
                WasRemoveInvoked = true;
            }
        }


        StructureTreeViewItemAdv<TestClass> _parentTvi;
        TreeLevelMock _level;
        TreeLevelMock _parentLevel;
        StructureTreeViewItemAdv<TestClass> _tvi;
        DisplayMemberModel<TestClass> _displayMember;
        TestClass _item;

        AlwaysExpandableTreeViewItemAdv<TestClass> _alwaysExpandableTvi;

        [SetUp]
        public void StructureTreeViewItemAdvSetUp()
        {
            _item = new TestClass() { Text = "hgdkwo34958tzghfj" };
            _displayMember = new DisplayMemberModel<TestClass>(_item, x => x.Text);
            _level = new TreeLevelMock();
            _parentLevel = new TreeLevelMock();
            _parentTvi = new StructureTreeViewItemAdv<TestClass>(new DisplayMemberModel<TestClass>(x => "")) { Level = _parentLevel };
            _tvi = new StructureTreeViewItemAdv<TestClass>(_displayMember) { TreeParent = _parentTvi, Level = _level };

            _parentTvi.Items.Add(_tvi);

            _alwaysExpandableTvi = new AlwaysExpandableTreeViewItemAdv<TestClass>(new DisplayMemberModel<TestClass>(x => x.Text));
        }


        [Test, RequiresThread(ApartmentState.STA)]
        public void OnCreateTest()
        {
            Assert.AreEqual(_item.Text, _tvi.Header);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void DisplayMemberChanged()
        {
            _item.Text = "sfdjikopf09u8thkm";
            _displayMember.UpdateDisplayMember();

            Assert.AreEqual(_item.Text, _tvi.Header);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void TextPropertyChanged()
        {
            _item.TextProperty = "6798zugjcmslrotiuh";

            Assert.AreEqual(_item.Text, _tvi.Header);
        }


        [Test, RequiresThread(ApartmentState.STA)]
        public void OnCreateAlwaysExpandableTest()
        {
            Assert.AreEqual(1, _alwaysExpandableTvi.Items.Count);
            Assert.IsFalse(_alwaysExpandableTvi.HasItemsExceptDummy());
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void AddItemToAlwaysExpandableTest()
        {
            var item = new TestClass() { Text = "nhgturieoldf,vm" };

            _alwaysExpandableTvi.Items.Add(item);

            Assert.AreEqual(1, _alwaysExpandableTvi.Items.Count);
            Assert.AreEqual(item, _alwaysExpandableTvi.Items[0]);
            Assert.IsTrue(_alwaysExpandableTvi.HasItemsExceptDummy());
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void RemoveItemToAlwaysExpandableTest()
        {
            var item = new TestClass() { Text = "nhgturieoldf,vm" };
            _alwaysExpandableTvi.Items.Add(item);

            _alwaysExpandableTvi.Items.Remove(item);

            Assert.AreEqual(1, _alwaysExpandableTvi.Items.Count);
            Assert.AreNotEqual(item, _alwaysExpandableTvi.Items[0]);
            Assert.IsFalse(_alwaysExpandableTvi.HasItemsExceptDummy());
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ResetAlwaysExpandableTest()
        {
            var item = new TestClass() { Text = "nhgturieoldf,vm" };
            _alwaysExpandableTvi.Items.Add(item);

            _alwaysExpandableTvi.Items.Clear();

            Assert.AreEqual(1, _alwaysExpandableTvi.Items.Count);
            Assert.AreNotEqual(item, _alwaysExpandableTvi.Items[0]);
            Assert.IsFalse(_alwaysExpandableTvi.HasItemsExceptDummy());
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void RemoveInvokesRemoveOnLevel()
        {
            _tvi.Remove(true);

            Assert.IsTrue(_level.WasRemoveInvoked);
            Assert.AreEqual(_tvi, _level.RemoveParameter);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void RemoveInvokesNotRemoveOnParentIfRemoveCascadeIsFalse()
        {
            _tvi.Remove(false);

            Assert.IsFalse(_parentLevel.WasRemoveInvoked);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void RemoveInvokesRemoveOnParentIfParentItemsEmpty()
        {
            _tvi.Remove(true);

            Assert.IsTrue(_parentLevel.WasRemoveInvoked);
            Assert.AreEqual(_parentTvi, _parentLevel.RemoveParameter);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void RemoveInvokesRemoveOnParentIfParentItemsNotEmpty()
        {
            _parentTvi.Items.Add(new StructureTreeViewItemAdv<TestClass>(new DisplayMemberModel<TestClass>(x => "")));
            _tvi.Remove(true);

            Assert.IsFalse(_parentLevel.WasRemoveInvoked);
            Assert.IsNull(_parentLevel.RemoveParameter);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void RemovesTviFromParentIems()
        {
            _tvi.Remove(true);

            Assert.IsFalse(_parentTvi.Items.Contains(_tvi));
        }
    }
}
