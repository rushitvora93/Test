using FrameworksAndDrivers.Gui.Wpf.Model;
using NUnit.Framework;
using InterfaceAdapters;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Models
{
    class DisplayMemberModelTest
    {
        private class DummyClass : BindableBase
        {
            public string Value;

            public string ValueProperty
            {
                get => Value;
                set => Set(ref Value, value);
            }
        }


        [Test]
        public void ConstructorDisplayMemberTest()
        {
            var item = new DummyClass() { Value = "Dummy 123" };
            var model = new DisplayMemberModel<DummyClass>(item, x => x.Value);

            Assert.AreEqual(item, model.Item);
            Assert.AreEqual("Dummy 123", model.DisplayMember);
        }

        [Test]
        public void SetItemTest()
        {
            var model = new DisplayMemberModel<DummyClass>(x => x.Value);
            model.Item = new DummyClass() { Value = "321 ymmuD" };

            Assert.AreEqual("321 ymmuD", model.DisplayMember);
        }

        [Test]
        public void UpdateDisplayMemberTest()
        {
            var model = new DisplayMemberModel<DummyClass>(new DummyClass() { Value = "" }, x => x.Value);
            model.Item.Value = "9876543210";

            model.UpdateDisplayMember();

            Assert.AreEqual("9876543210", model.DisplayMember);
        }

        [Test]
        public void NotifyPropertyChangedTest()
        {
            var model = new DisplayMemberModel<DummyClass>(new DummyClass() { Value = "hgur8439iowsldxcvk" }, x => x.Value);

            model.Item.ValueProperty = "nhgztu78r9e0o4pwlsd,fcm";

            Assert.AreEqual("nhgztu78r9e0o4pwlsd,fcm", model.DisplayMember);
        }

        [Test]
        public void LocalizedDisplayMemberTest()
        {
            var localization = new NullLocalizationWrapper();
            var item = new DummyClass() { Value = "bngztrieolsd.xcv," };
            var model = new LocalizedDisplayMemberModel<DummyClass>(item, x => x.Value, localization);

            item.Value = "bnhgfrujdksxlcklmvscmvy";
            Assert.AreEqual("bngztrieolsd.xcv,", model.DisplayMember);
            model.LanguageUpdate();
            Assert.AreEqual("bnhgfrujdksxlcklmvscmvy", model.DisplayMember);
        }

        [Test]
        public void GetItemReturnsItem()
        {
            var dummy = new DummyClass();
            var model = new DisplayMemberModel<DummyClass>(dummy, x => x.Value);

            Assert.AreEqual(dummy, model.GetItem());
        }
    }
}
