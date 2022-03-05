using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using NUnit.Framework;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Behaviors
{
    class ConditionValidationBehaviorTest
    {
        [Test, RequiresThread(ApartmentState.STA)]
        public void BehaviorDoesntShowWarningIfConditionIsFalse()
        {
            var textBox = new TextBox();
            var behavior = new ConditionValidationBehavior();
            behavior.Condition = x => false;
            behavior.Attach(textBox);

            textBox.Text = "t7r890op";

            Assert.IsFalse(behavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void BehaviorDoesShowWarningIfConditionIsTrue()
        {
            var textBox = new TextBox();
            var behavior = new ConditionValidationBehavior();
            behavior.Condition = x => true;
            behavior.Attach(textBox);

            textBox.Text = "fhdee9zw08uu9";

            Assert.IsTrue(behavior.IsWarningShown);
        }
    }

    class ConditionsValidationBehaviorTest
    {
        [Test, RequiresThread(ApartmentState.STA)]
        public void BehaviorDoesntShowWarningIfAllConditionsAreFalse()
        {
            var textBox = new TextBox();
            var behavior = new ConditionsValidationBehavior()
            {
                Conditions = new List<ConditionValidationBehavior>()
                {
                    new ConditionValidationBehavior() {Condition = x => false},
                    new ConditionValidationBehavior() {Condition = x => false}
                }
            };

            behavior.Attach(textBox);
            textBox.Text = "t7r890op";

            Assert.IsFalse(behavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void BehaviorDoesShowWarningIfOneConditionIsTrue()
        {
            var textBox = new TextBox();
            var behavior = new ConditionsValidationBehavior()
            {
                Conditions = new List<ConditionValidationBehavior>()
                {
                    new ConditionValidationBehavior() {Condition = x => false},
                    new ConditionValidationBehavior() {Condition = x => true}
                }
            };

            behavior.Attach(textBox);
            textBox.Text = "fhdee9zw08uu9";

            Assert.IsTrue(behavior.IsWarningShown);
        }

        [TestCase("FALSCH!")]
        [TestCase("FEHLER!"), RequiresThread(ApartmentState.STA)]
        public void BehaviorShowsCorrectWarningIfOneConditionIsFalse(string warning)
        {
            var textBox = new TextBox();
            var behavior = new ConditionsValidationBehavior()
            {
                Conditions = new List<ConditionValidationBehavior>()
                {
                    new ConditionValidationBehavior() {Condition = x => true, WarningText = warning},
                    new ConditionValidationBehavior() {Condition = x => false, WarningText = ""}
                }
            };

            behavior.Attach(textBox);
            textBox.Text = "fhdee9zw08uu9";

            Assert.AreEqual(warning, behavior.WarningText);
        }
    }
}
