using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using Microsoft.Xaml.Behaviors;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Behaviors
{
    class EventToCommandBehaviorTest
    {
        TextBox _textBox;
        EventToCommandBehavior _eventToCommandBehavior;
        RelayCommand _command;
        object _eventArgs;
        bool _wasEventCalld;

        private void CommandExecute(object obj)
        {
            _eventArgs = obj;
            _wasEventCalld = true;
        }


        [SetUp]
        public void EventToCommandBehaviorSetUp()
        {
            _textBox = new TextBox();
            _command = new RelayCommand(CommandExecute, arg => true);

            _eventToCommandBehavior = new EventToCommandBehavior() { Event = "TextChanged", Command = _command, PassArguments = true };

            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);
            textBoxBehaviors.Add(_eventToCommandBehavior);
        }


        [Test, RequiresThread(ApartmentState.STA)]
        public void CallEventWithoutEventNameIsNullTest()
        {
            _eventToCommandBehavior.Event = null;

            _textBox.Text = "123";

            Assert.IsFalse(_wasEventCalld);
            Assert.IsNull(_eventArgs);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void CallEventWithoutEventNameIsEmptyTest()
        {
            _eventToCommandBehavior.Event = "";

            _textBox.Text = "123";

            Assert.IsFalse(_wasEventCalld);
            Assert.IsNull(_eventArgs);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void CallEventWithoutEventNameIsWhiteSpaceTest()
        {
            Assert.Catch<ArgumentException>(() => { _eventToCommandBehavior.Event = "   \n"; });
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void CallEventWithoutEventNameIsInvalidTest()
        {
            Assert.Catch<ArgumentException>(() => { _eventToCommandBehavior.Event = "hsfdkoaplfkospl"; });
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void CallEventWithoutCommandTest()
        {
            _eventToCommandBehavior.Command = null;

            _textBox.Text = "123";

            Assert.IsFalse(_wasEventCalld);
            Assert.IsNull(_eventArgs);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void CallEventWithValidAndNotPassArgumentsBehaviorTest()
        {
            _eventToCommandBehavior.Event = "";

            _textBox.Text = "123";

            Assert.IsFalse(_wasEventCalld);
            Assert.IsNull(_eventArgs);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void CallEventWithValidAndPassArgumentsBehaviorTest()
        {
            _textBox.Text = "123";

            Assert.IsTrue(_wasEventCalld);
            Assert.IsNotNull(_eventArgs);
            Assert.IsTrue(_eventArgs is TextChangedEventArgs);
            Assert.AreEqual(1, (_eventArgs as TextChangedEventArgs).Changes.Count);
            Assert.AreEqual(0, (_eventArgs as TextChangedEventArgs).Changes.First().Offset);
            Assert.AreEqual(3, (_eventArgs as TextChangedEventArgs).Changes.First().AddedLength);
            Assert.AreEqual(0, (_eventArgs as TextChangedEventArgs).Changes.First().RemovedLength);
        }
    }
}
