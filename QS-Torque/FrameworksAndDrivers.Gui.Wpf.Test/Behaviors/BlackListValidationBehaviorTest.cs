using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using Microsoft.Xaml.Behaviors;
using NUnit.Framework;
using System.Threading;
using System.Windows.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Behaviors
{
    class BlackListValidationBehaviorTest
    {
        TextBox _textBox;
        BlackListValidationBehavior _validationBehavior;
        const string BlackList = "abc123";

        [SetUp]
        public void BlackListValidationBehaviorSetUp()
        {
            _validationBehavior = new BlackListValidationBehavior() { BlackList = BlackList };
            _textBox = new TextBox();

            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);
            textBoxBehaviors.Add(_validationBehavior);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterValidText()
        {
            _textBox.Text = "kop987";

            Assert.IsFalse(_validationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterInvalidText()
        {
            _textBox.Text = "b";

            Assert.IsTrue(_validationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterWhiteSpace()
        {
            _textBox.Text = "   \n";

            Assert.IsFalse(_validationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterInvalidTextAfterValid()
        {
            _textBox.Text = "kop987";
            Assert.IsFalse(_validationBehavior.IsWarningShown);

            _textBox.Text = "cba";
            Assert.IsTrue(_validationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ChangeValidToInvalid()
        {
            _textBox.Text = "kop987";
            Assert.IsFalse(_validationBehavior.IsWarningShown);

            _textBox.Text = "kop1987";
            Assert.IsTrue(_validationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ChangeInvalidToValid()
        {
            _textBox.Text = "kop1987";
            Assert.IsTrue(_validationBehavior.IsWarningShown);

            _textBox.Text = "kop987";
            Assert.IsFalse(_validationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ChangeInvalidToInvalid()
        {
            _textBox.Text = "852963";
            Assert.IsTrue(_validationBehavior.IsWarningShown);

            _textBox.Text = "qayedctgb";
            Assert.IsTrue(_validationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ChangeValidToValid()
        {
            _textBox.Text = "456789";
            Assert.IsFalse(_validationBehavior.IsWarningShown);

            _textBox.Text = "wsxrfvzhn";
            Assert.IsFalse(_validationBehavior.IsWarningShown);
        }
    }
}
