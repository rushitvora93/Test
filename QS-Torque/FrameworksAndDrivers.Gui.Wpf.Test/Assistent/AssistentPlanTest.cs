using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Threading;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Assistent
{
    class AssistentPlanTest
    {
        [Test]
        public void CreateAssistentPlanWithAssistentItemTest()
        {
            var item = new Model.AssistentItemModel<string>(Model.AssistentItemType.Text, "", "", "", (o, i) => { });

            var plan = new AssistentPlan<string>(item);

            Assert.AreEqual(plan.AssistentItem, item);
        }

        [Test]
        public void AddNextWithAssistentItemAndOneSubPlan()
        {
            var subPlan = new AssistentPlan<string>(new AssistentItemModel<string>(AssistentItemType.Text, "", "", "", (o, i) => { }));
            var plan = new AssistentPlan<string>(new List<AssistentPlan>() { subPlan }, null);
            var list = new List<AssistentPlan>();

            plan.AddNext(list);

            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], subPlan);
        }

        [Test]
        public void AddNextWithNoChildTest()
        {
            var plan = new ParentAssistentPlan(new List<ParentAssistentPlan>());
            var list = new List<AssistentPlan>();

            plan.AddNext(list);

            Assert.AreEqual(list.Count, 0);
        }

        [Test]
        public void AddNextWithOneSubPlan()
        {
            var subPlan = new AssistentPlan<string>(new AssistentItemModel<string>(AssistentItemType.Text, "", "", "", (o, i) => { }));
            var plan = new ParentAssistentPlan(new List<ParentAssistentPlan>() { subPlan });
            var list = new List<AssistentPlan>();

            plan.AddNext(list);

            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], subPlan);
        }

        [Test]
        public void AddNextWithOneSublayer()
        {
            var subPlan0 = new AssistentPlan<string>(new AssistentItemModel<string>(AssistentItemType.Text, "", "", "", (o, i) => { }));
            var subPlan1 = new AssistentPlan<string>(new AssistentItemModel<string>(AssistentItemType.Text, "", "", "", (o, i) => { }));
            var subPlan2 = new AssistentPlan<string>(new AssistentItemModel<string>(AssistentItemType.Text, "", "", "", (o, i) => { }));
            var subPlan3 = new AssistentPlan<string>(new AssistentItemModel<string>(AssistentItemType.Text, "", "", "", (o, i) => { }));

            var plan = new ParentAssistentPlan(new List<ParentAssistentPlan>()
                                              {
                                                  subPlan0,
                                                  subPlan1,
                                                  subPlan2,
                                                  subPlan3
                                              });

            var list = new List<AssistentPlan>();
            plan.AddNext(list);

            Assert.AreEqual(list.Count, 4);
            Assert.AreEqual(list[0], subPlan0);
            Assert.AreEqual(list[1], subPlan1);
            Assert.AreEqual(list[2], subPlan2);
            Assert.AreEqual(list[3], subPlan3);
        }

        [Test]
        public void AddNextWithTwoSublayers()
        {
            var subPlan10 = new AssistentPlan<string>(new AssistentItemModel<string>(AssistentItemType.Text, "", "", "", (o, i) => { }));
            var subPlan11 = new AssistentPlan<string>(new AssistentItemModel<string>(AssistentItemType.Text, "", "", "", (o, i) => { }));

            var subPlan20 = new AssistentPlan<string>(new AssistentItemModel<string>(AssistentItemType.Text, "", "", "", (o, i) => { }));
            var subPlan21 = new AssistentPlan<string>(new AssistentItemModel<string>(AssistentItemType.Text, "", "", "", (o, i) => { }));
            var subPlan22 = new AssistentPlan<string>(new AssistentItemModel<string>(AssistentItemType.Text, "", "", "", (o, i) => { }));

            var subPlan0 = new AssistentPlan<string>(new AssistentItemModel<string>(AssistentItemType.Text, "", "", "", (o, i) => { }));
            var subPlan1 = new AssistentPlan<string>(new List<AssistentPlan>() { subPlan10, subPlan11 }, new AssistentItemModel<string>(AssistentItemType.Text, "", "", "", (o, i) => { }));
            var subPlan2 = new AssistentPlan<string>(new List<AssistentPlan>() { subPlan20, subPlan21, subPlan22 }, new AssistentItemModel<string>(AssistentItemType.Text, "", "", "", (o, i) => { }));
            var subPlan3 = new AssistentPlan<string>(new AssistentItemModel<string>(AssistentItemType.Text, "", "", "", (o, i) => { }));

            var plan = new ParentAssistentPlan(new List<ParentAssistentPlan>()
                                              {
                                                  subPlan0,
                                                  subPlan1,
                                                  subPlan2,
                                                  subPlan3
                                              });

            var list = new List<AssistentPlan>();
            plan.AddNext(list);

            Assert.AreEqual(list.Count, 9);
            Assert.AreEqual(list[0], subPlan0);
            Assert.AreEqual(list[1], subPlan1);
            Assert.AreEqual(list[2], subPlan10);
            Assert.AreEqual(list[3], subPlan11);
            Assert.AreEqual(list[4], subPlan2);
            Assert.AreEqual(list[5], subPlan20);
            Assert.AreEqual(list[6], subPlan21);
            Assert.AreEqual(list[7], subPlan22);
            Assert.AreEqual(list[8], subPlan3);
        }

        [Test]
        public void CreateListAssistentPlanWithoutSubPlans()
        {
            var model = new ListAssistentItemModel<string>(Dispatcher.CurrentDispatcher, "", "", "", (o, i) => { }, "", x => x, () => { });
            var plan = new ListAssistentPlan<string>(model);

            Assert.AreEqual(plan.AssistentItem, model);
        }
    }
}
