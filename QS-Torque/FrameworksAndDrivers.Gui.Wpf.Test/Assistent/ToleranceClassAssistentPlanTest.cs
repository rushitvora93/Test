using System.Collections.Generic;
using System.Windows.Threading;
using Core;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using NUnit.Framework;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Assistent
{
    class ToleranceClassAssistentPlanTest
    {
        private class ToleranceClassUseCaseMock : ToleranceClassUseCase
        {
            public bool WasLoadToleranceClassCalled = false;

            public override void LoadToleranceClasses()
            {
                WasLoadToleranceClassCalled = true;
            }

            public ToleranceClassUseCaseMock(IToleranceClassGui guiInterface, IToleranceClassData dataInterface, ISessionInformationUserGetter userGetter, INotificationManager notificationManager) : base(guiInterface, dataInterface, userGetter, null, notificationManager)
            {
            }
        }

        [Test]
        public void InitializeCallsLoadToleranceClassesOnUseCase()
        {
            var useCase = new ToleranceClassUseCaseMock(null, null, null, null);
            var plan = new ToleranceClassAssistentPlan(useCase, null);

            plan.Initialize();

            Assert.IsTrue(useCase.WasLoadToleranceClassCalled);
        }

        [Test]
        public void ShowToleranceClassesFillsItemsInAssistentItem()
        {
            var toleranceClass0 = CreateParametrizedToleranceClass(1, "zguhtrjnm6+");
            var toleranceClass1 = CreateParametrizedToleranceClass(2, "s6e46sth5s 1");
            var toleranceClass2 = CreateParametrizedToleranceClass(3, "zrs3f123sf3g21sf3");

            var item = CreateAssistenItem();
            var plan = CreateAssistentPlan(item);

            plan.ShowToleranceClasses(new List<ToleranceClass>() { toleranceClass0, toleranceClass1, toleranceClass2 });

            Assert.AreEqual(item.ItemsCollectionView.Count, 3);
            Assert.AreEqual(((item.ItemsCollectionView.GetItemAt(0) as DisplayMemberModel<ToleranceClass>).Item as ToleranceClass).Name, toleranceClass0.Name);
            Assert.AreEqual(((item.ItemsCollectionView.GetItemAt(1) as DisplayMemberModel<ToleranceClass>).Item as ToleranceClass).Name, toleranceClass1.Name);
            Assert.AreEqual(((item.ItemsCollectionView.GetItemAt(2) as DisplayMemberModel<ToleranceClass>).Item as ToleranceClass).Name, toleranceClass2.Name);
        }

        [Test]
        public void RemoveToleranceClassRemovesItemFromAssistentItem()
        {
            var toleranceClass0 = CreateParametrizedToleranceClass(1, "tz6h69jt4z5md12g");
            var toleranceClass1 = CreateParametrizedToleranceClass(2, "t4zh545jtn1zrd3gf");
            var toleranceClass2 = CreateParametrizedToleranceClass(3, "t65z5kfjdzfth1");

            var item = CreateAssistenItem(new List<ToleranceClass>() { toleranceClass0, toleranceClass1, toleranceClass2 });
            var plan = CreateAssistentPlan(item);

            var removeToleranceClass = new ToleranceClass() { Id = toleranceClass1.Id, Name = toleranceClass1.Name };
            plan.RemoveToleranceClass(removeToleranceClass);

            Assert.AreEqual(item.ItemsCollectionView.Count, 2);
            Assert.AreEqual(((item.ItemsCollectionView.GetItemAt(0) as DisplayMemberModel<ToleranceClass>).Item as ToleranceClass).Name, toleranceClass0.Name);
            Assert.AreEqual(((item.ItemsCollectionView.GetItemAt(1) as DisplayMemberModel<ToleranceClass>).Item as ToleranceClass).Name, toleranceClass2.Name);
        }

        [Test]
        public void AddToleranceClassAddsItemToAssistenItem()
        {
            var toleranceClass0 = CreateParametrizedToleranceClass(1, "ztevd");
            var toleranceClass1 = CreateParametrizedToleranceClass(2, "4596tz5r5t4zj13gf2");
            var toleranceClass2 = CreateParametrizedToleranceClass(3, "poiuzhuioplkij");

            var item = CreateAssistenItem(new List<ToleranceClass>() { toleranceClass0, toleranceClass1, toleranceClass2 });
            var plan = CreateAssistentPlan(item);

            var newToleranceClass = CreateParametrizedToleranceClass(24,"43qz89fvihcjn");
            plan.AddToleranceClass(newToleranceClass);

            Assert.AreEqual(item.ItemsCollectionView.Count, 4);
            Assert.AreEqual(((item.ItemsCollectionView.GetItemAt(3) as DisplayMemberModel<ToleranceClass>).Item as ToleranceClass).Name, newToleranceClass.Name);
        }

        [Test]
        public void UpdateToleranceClassUpdatesItemInAssistentItem()
        {
            var toleranceClass0 = CreateParametrizedToleranceClass(1, "ztevd");
            var toleranceClass1 = CreateParametrizedToleranceClass(2, "4596tz5r5t4zj13gf2");
            var toleranceClass2 = CreateParametrizedToleranceClass(3, "poiuzhuioplkij");

            var item = CreateAssistenItem(new List<ToleranceClass>() { toleranceClass0, toleranceClass1, toleranceClass2 });
            var plan = CreateAssistentPlan(item);

            var toleranceClass = CreateParametrizedToleranceClass(toleranceClass2.Id.ToLong(), "Changed Name");

            plan.UpdateToleranceClass(toleranceClass);

            Assert.AreEqual(item.ItemsCollectionView.Count, 3);
            Assert.AreEqual(((item.ItemsCollectionView.GetItemAt(2) as DisplayMemberModel<ToleranceClass>).Item as ToleranceClass).Id, toleranceClass.Id);
            Assert.AreEqual(((item.ItemsCollectionView.GetItemAt(2) as DisplayMemberModel<ToleranceClass>).Item as ToleranceClass).Name, toleranceClass.Name);
        }


        private static ToleranceClass CreateParametrizedToleranceClass(long id, string name)
        {
            return new ToleranceClass() { Id = new ToleranceClassId(id), Name = name };
        }

        private ListAssistentItemModel<ToleranceClass> CreateAssistenItem()
        {
            var item = new ListAssistentItemModel<ToleranceClass>(Dispatcher.CurrentDispatcher, "", "", null, (o, i) => { }, "", x => x.Name, () => { });
            item.ItemsCollectionView.SortDescriptions.Clear();
            return item;
        }

        private ListAssistentItemModel<ToleranceClass> CreateAssistenItem(List<ToleranceClass> deaultToleranceClasses)
        {
            var item = new ListAssistentItemModel<ToleranceClass>(Dispatcher.CurrentDispatcher, deaultToleranceClasses, "", "", null, (o, i) => { }, "", x => x.Name, () => { });
            item.ItemsCollectionView.SortDescriptions.Clear();
            return item;
        }

        private ToleranceClassAssistentPlan CreateAssistentPlan(ListAssistentItemModel<ToleranceClass> item)
        {
            return new ToleranceClassAssistentPlan(null, item);
        }
    }
}
