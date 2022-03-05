using FrameworksAndDrivers.Gui.Wpf.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Threading;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Models
{
    class AssistentItemModelTest
    {
        private class DummyClass
        {
            public string Value { get; set; }
        }
        

        [Test]
        public void OpenHelperTableTest()
        {
            bool helperTableOpened = false;
            var model = new ListAssistentItemModel<string>(Dispatcher.CurrentDispatcher,
                                                           new List<string>(),
                                                           "",
                                                           "",
                                                           null,
                                                           (o, i) => { },
                                                           "",
                                                           x => { return ""; },
                                                           () => { helperTableOpened = true; });
            model.OpenHelperTableCommand.Invoke(null);

            Assert.AreEqual(helperTableOpened, true);
        }

        [Test]
        public void ConstructorTest()
        {
            var dummy1 = new DummyClass() { Value = "Value 123" };
            var dummy2 = new DummyClass() { Value = "Value 456" };

            var model = new ListAssistentItemModel<DummyClass>(Dispatcher.CurrentDispatcher,
                                                               new List<DummyClass>() { dummy1, dummy2 },
                                                               "Description",
                                                               "AttributeName",
                                                               null,
                                                               (o, i) => { },
                                                               "HelperTableName",
                                                               x => x.Value,
                                                               () => { });

            Assert.AreEqual(model.ItemsCollectionView.Count, 2);
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(0) as DisplayMemberModel<DummyClass>).Item, dummy1);
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(1) as DisplayMemberModel<DummyClass>).Item, dummy2);
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(0) as DisplayMemberModel<DummyClass>).DisplayMember, "Value 123");
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(1) as DisplayMemberModel<DummyClass>).DisplayMember, "Value 456");
        }

        [Test]
        public void UpdateListItemsTest()
        {
            var model = new ListAssistentItemModel<DummyClass>(Dispatcher.CurrentDispatcher,
                                                               "Description",
                                                               "AttributeName",
                                                               null,
                                                               (o, i) => { },
                                                               "HelperTableName",
                                                               x => x.Value,
                                                               () => { });

            var dummy1 = new DummyClass() { Value = "Value 123" };
            var dummy2 = new DummyClass() { Value = "Value 456" };

            model.RefillListItems(new List<DummyClass>() { dummy1, dummy2 });

            Assert.AreEqual(model.ItemsCollectionView.Count, 2);
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(0) as DisplayMemberModel<DummyClass>).Item, dummy1);
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(1) as DisplayMemberModel<DummyClass>).Item, dummy2);
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(0) as DisplayMemberModel<DummyClass>).DisplayMember, "Value 123");
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(1) as DisplayMemberModel<DummyClass>).DisplayMember, "Value 456");
        }

        [Test]
        public void AddListItemsTest()
        {
            var model = new ListAssistentItemModel<DummyClass>(Dispatcher.CurrentDispatcher,
                                                               "Description",
                                                               "AttributeName",
                                                               null,
                                                               (o, i) => { },
                                                               "HelperTableName",
                                                               x => x.Value,
                                                               () => { });

            var dummy1 = new DummyClass() { Value = "Value 123" };
            var dummy2 = new DummyClass() { Value = "Value 456" };
            var dummy3 = new DummyClass() { Value = "Value 789" };

            model.RefillListItems(new List<DummyClass>() { dummy1, dummy2 });
            model.AddListItem(dummy3);

            Assert.AreEqual(model.ItemsCollectionView.Count, 3);
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(0) as DisplayMemberModel<DummyClass>).Item, dummy1);
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(1) as DisplayMemberModel<DummyClass>).Item, dummy2);
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(2) as DisplayMemberModel<DummyClass>).Item, dummy3);
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(0) as DisplayMemberModel<DummyClass>).DisplayMember, "Value 123");
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(1) as DisplayMemberModel<DummyClass>).DisplayMember, "Value 456");
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(2) as DisplayMemberModel<DummyClass>).DisplayMember, "Value 789");
        }

        [Test]
        public void InsertListItemsTest()
        {
            var model = new ListAssistentItemModel<DummyClass>(Dispatcher.CurrentDispatcher,
                                                               "Description",
                                                               "AttributeName",
                                                               null,
                                                               (o, i) => { },
                                                               "HelperTableName",
                                                               x => x.Value,
                                                               () => { });
            model.ItemsCollectionView.SortDescriptions.Clear();

            var dummy1 = new DummyClass() { Value = "Value 123" };
            var dummy2 = new DummyClass() { Value = "Value 456" };
            var dummy3 = new DummyClass() { Value = "Value 789" };

            model.RefillListItems(new List<DummyClass>() { dummy1, dummy2 });
            model.InsertListItem(1, dummy3);

            Assert.AreEqual(model.ItemsCollectionView.Count, 3);
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(0) as DisplayMemberModel<DummyClass>).Item, dummy1);
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(1) as DisplayMemberModel<DummyClass>).Item, dummy3);
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(2) as DisplayMemberModel<DummyClass>).Item, dummy2);
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(0) as DisplayMemberModel<DummyClass>).DisplayMember, "Value 123");
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(1) as DisplayMemberModel<DummyClass>).DisplayMember, "Value 789");
            Assert.AreEqual((model.ItemsCollectionView.GetItemAt(2) as DisplayMemberModel<DummyClass>).DisplayMember, "Value 456");
        }

        [Test]
        public void UniqueAssistentItemModelIsUniqueTest()
        {
            var list = new List<string>() { "a", "b", "c" };
            var model = new UniqueAssistentItemModel<string>(x => { return !list.Contains(x); },
                                                             AssistentItemType.Text,
                                                             "", 
                                                             "",
                                                             "",
                                                             (o, i) => { },
                                                             "");
            model.EnteredValue = "d";

            // No error
            Assert.IsFalse(model.ErrorCheck(model));
        }

        [Test]
        public void UniqueAssistentItemModelIsNotUniqueTest()
        {
            var list = new List<string>() { "a", "b", "c" };
            var model = new UniqueAssistentItemModel<string>(x => { return !list.Contains(x); },
                                                             AssistentItemType.Text,
                                                             "",
                                                             "",
                                                             "",
                                                             (o, i) => { },
                                                             "");
            model.EnteredValue = "b";

            // Error
            Assert.IsTrue(model.ErrorCheck(model));
        }
    }
}
