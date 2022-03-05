using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using Microsoft.Xaml.Behaviors;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Behaviors
{
    class BehaviorBindingTest
    {
        private class TextBoxBehaviorMock : Behavior<TextBox>
        {

        }

        BehaviorBinding _behaviorBinding;
        TextBox _textBox;
        TextBoxBehaviorMock _behavior1;
        TextBoxBehaviorMock _behavior2;

        [SetUp]
        public void BehaviorBindingSetUp()
        {
            _behaviorBinding = new BehaviorBinding();
            _textBox = new TextBox();
            _behavior1 = new TextBoxBehaviorMock();
            _behavior2 = new TextBoxBehaviorMock();

            _behaviorBinding.Attach(_textBox);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void BindNoBehaviorsTest()
        {
            _behaviorBinding.Behaviors = new List<Behavior>();

            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);

            Assert.AreEqual(0, textBoxBehaviors.Count);
        }
        
        [Test, RequiresThread(ApartmentState.STA)]
        public void BindOneBehaviorTest()
        {
            _behaviorBinding.Behaviors = new List<Behavior>() { _behavior1 };

            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);

            Assert.AreEqual(1, textBoxBehaviors.Count);
            Assert.AreEqual(_behavior1, textBoxBehaviors[0]);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void BindTwoBehaviorsTest()
        {
            _behaviorBinding.Behaviors = new List<Behavior>() { _behavior1, _behavior2 };

            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);

            Assert.AreEqual(2, textBoxBehaviors.Count);
            Assert.AreEqual(_behavior1, textBoxBehaviors[0]);
            Assert.AreEqual(_behavior2, textBoxBehaviors[1]);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void RemoveOneBehaviorTest()
        {
            _behaviorBinding.Behaviors = new List<Behavior>() { _behavior1, _behavior2 };

            _behaviorBinding.Behaviors = new List<Behavior>() { _behavior2 };

            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);

            Assert.AreEqual(1, textBoxBehaviors.Count);
            Assert.AreEqual(_behavior2, textBoxBehaviors[0]);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void RemoveBehaviorsWithEmptyListTest()
        {
            _behaviorBinding.Behaviors = new List<Behavior>() { _behavior1, _behavior2 };

            _behaviorBinding.Behaviors = new List<Behavior>();

            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);

            Assert.AreEqual(0, textBoxBehaviors.Count);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void BindNullAgainTest()
        {
            // _behaviorBinding.Behaviors is null by default
            _behaviorBinding.Behaviors = new List<Behavior>() { _behavior1, _behavior2 };

            _behaviorBinding.Behaviors = null;

            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);

            Assert.AreEqual(0, textBoxBehaviors.Count);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void AddBehaviorWithPreAttachedBehaviorTest()
        {
            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);
            textBoxBehaviors.Add(_behavior1);
            
            _behaviorBinding.Behaviors = new List<Behavior>() { _behavior2 };

            Assert.AreEqual(2, textBoxBehaviors.Count);
            Assert.AreEqual(_behavior1, textBoxBehaviors[0]);
            Assert.AreEqual(_behavior2, textBoxBehaviors[1]);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void RemoveBehaviorWithPreAttachedBehaviorTest()
        {
            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);
            textBoxBehaviors.Add(_behavior1);

            _behaviorBinding.Behaviors = new List<Behavior>() { _behavior2 };
            _behaviorBinding.Behaviors = new List<Behavior>();

            Assert.AreEqual(1, textBoxBehaviors.Count);
            Assert.AreEqual(_behavior1, textBoxBehaviors[0]);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void AddAlreadyExistingBehaviorTest()
        {
            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);
            textBoxBehaviors.Add(_behavior1);

            _behaviorBinding.Behaviors = new List<Behavior>() { _behavior1 };

            Assert.AreEqual(1, textBoxBehaviors.Count);
            Assert.AreEqual(_behavior1, textBoxBehaviors[0]);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void RemoveNotExistingBehaviorTest()
        {
            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);
            textBoxBehaviors.Add(_behavior2);

            _behaviorBinding.Behaviors = new List<Behavior>() { _behavior1 };

            Assert.AreEqual(2, textBoxBehaviors.Count);
            Assert.AreEqual(_behavior2, textBoxBehaviors[0]);
            Assert.AreEqual(_behavior1, textBoxBehaviors[1]);
        }
    }
}
