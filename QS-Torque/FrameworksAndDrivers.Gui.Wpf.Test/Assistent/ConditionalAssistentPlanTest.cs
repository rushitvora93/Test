using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Assistent
{
    class ConditionalAssistentPlanTest
    {
        [TestCase("987")]
        [TestCase("uriefnwvjior")]
        public void AddNextConditionTrueTest(string requiredValue)
        {
            var item = new Model.AssistentItemModel<string>(Model.AssistentItemType.Text, "", "", "", (o, i) => { });
            var parentPlan = new AssistentPlan<string>(item);

            var subPlan1 = new AssistentPlan<string>(new AssistentItemModel<string>(AssistentItemType.Text, "", "", "", (o, i) => { }));
            var subPlan2 = new AssistentPlan<string>(new AssistentItemModel<string>(AssistentItemType.Text, "", "", "", (o, i) => { }));

            Func<bool> condition = () => parentPlan.AssistentItem.EnteredValue == requiredValue;
            var condPlan = new ConditionalAssistentPlan(new List<ParentAssistentPlan>() { subPlan1, subPlan2 }, condition);

            item.EnteredValue = requiredValue;
            var list = new List<AssistentPlan>();
            condPlan.AddNext(list);

            Assert.AreEqual(list.Count, 2);
            Assert.AreEqual(list[0], subPlan1);
            Assert.AreEqual(list[1], subPlan2);
        }

        [TestCase("987")]
        [TestCase("uriefnwvjior")]
        public void AddNextConditionFalseTest(string requiredValue)
        {
            var item = new Model.AssistentItemModel<string>(Model.AssistentItemType.Text, "", "", "", (o, i) => { });
            var parentPlan = new AssistentPlan<string>(item);

            var subPlan1 = new AssistentPlan<string>(null);
            var subPlan2 = new AssistentPlan<string>(null);

            Func<bool> condition = () => parentPlan.AssistentItem.EnteredValue == requiredValue;
            var condPlan = new ConditionalAssistentPlan(new List<ParentAssistentPlan>() { subPlan1, subPlan2 }, condition);

            item.EnteredValue = "";
            var list = new List<AssistentPlan>();
            condPlan.AddNext(list);

            Assert.AreEqual(list.Count, 0);
        }
    }
}
