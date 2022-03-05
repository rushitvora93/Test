using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using Microsoft.Xaml.Behaviors;
using NUnit.Framework;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Behaviors
{
    class BehaviorSetterTest
    {
        private class TextBoxBehaviorMock : Behavior<TextBox>
        {
            public string TestString { get; set; }

            protected override void CloneCore(Freezable sourceFreezable)
            {
                base.CloneCore(sourceFreezable);

                TestString = (sourceFreezable as TextBoxBehaviorMock).TestString;
            }
        }

        TextBox _textBox;
        TextBoxBehaviorMock _behavior1;
        TextBoxBehaviorMock _behavior2;

        [SetUp]
        public void BehaviorSeterSetUp()
        {
            _textBox = new TextBox();
            _behavior1 = new TextBoxBehaviorMock() { TestString = "b37895430ipowekvcmlnj" };
            _behavior2 = new TextBoxBehaviorMock() { TestString = "huzgvnjsöü3ß0968zugjm" };
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void SetNullTest()
        {
            BehaviorSetter.SetBehavior(_textBox, null);

            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);

            Assert.AreEqual(0, textBoxBehaviors.Count);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void SetOneBehaviorTest()
        {
            BehaviorSetter.SetBehavior(_textBox, _behavior1);

            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);

            Assert.AreEqual(1, textBoxBehaviors.Count);
            Assert.AreEqual(_behavior1.GetType(), textBoxBehaviors[0].GetType());
            Assert.AreEqual(_behavior1.TestString, (textBoxBehaviors[0] as TextBoxBehaviorMock).TestString);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void SetTwoBehaviorTest()
        {
            BehaviorSetter.SetBehavior(_textBox, _behavior1);
            BehaviorSetter.SetBehavior(_textBox, _behavior2);

            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);

            Assert.AreEqual(2, textBoxBehaviors.Count);
            Assert.AreEqual(_behavior1.GetType(), textBoxBehaviors[0].GetType());
            Assert.AreEqual(_behavior1.TestString, (textBoxBehaviors[0] as TextBoxBehaviorMock).TestString);
            Assert.AreEqual(_behavior2.GetType(), textBoxBehaviors[1].GetType());
            Assert.AreEqual(_behavior2.TestString, (textBoxBehaviors[1] as TextBoxBehaviorMock).TestString);
        }
    }
}
