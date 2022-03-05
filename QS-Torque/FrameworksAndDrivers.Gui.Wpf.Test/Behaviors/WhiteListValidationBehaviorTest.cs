using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using Microsoft.Xaml.Behaviors;
using NUnit.Framework;
using System.Threading;
using System.Windows.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Behaviors
{
    class WhiteListValidationBehaviorTest
    {
        TextBox _textBox;
        WhiteListValidationBehavior _validationBehavior;
        const string WhiteList = "abc123";

        [SetUp]
        public void WhiteListValidationBehaviorSetUp()
        {
            _validationBehavior = new WhiteListValidationBehavior() { WhiteList = WhiteList };
            _textBox = new TextBox();

            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);
            textBoxBehaviors.Add(_validationBehavior);
        }


        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterValidText()
        {
            _textBox.Text = "bc";

            Assert.IsFalse(_validationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterInvalidText()
        {
            _textBox.Text = "olkjh";

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
            _textBox.Text = "321";
            Assert.IsFalse(_validationBehavior.IsWarningShown);

            _textBox.Text = "987apqws";
            Assert.IsTrue(_validationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ChangeValidToInvalid()
        {
            _textBox.Text = "12ba";
            Assert.IsFalse(_validationBehavior.IsWarningShown);

            _textBox.Text = "124bah";
            Assert.IsTrue(_validationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ChangeInvalidToValid()
        {
            _textBox.Text = "931qcb";
            Assert.IsTrue(_validationBehavior.IsWarningShown);

            _textBox.Text = "231acb";
            Assert.IsFalse(_validationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ChangeInvalidToInvalid()
        {
            _textBox.Text = "9632147";
            Assert.IsTrue(_validationBehavior.IsWarningShown);

            _textBox.Text = "qayxcfgbvc";
            Assert.IsTrue(_validationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ChangeValidToValid()
        {
            _textBox.Text = "123abc";
            Assert.IsFalse(_validationBehavior.IsWarningShown);

            _textBox.Text = "bca321";
            Assert.IsFalse(_validationBehavior.IsWarningShown);
        }
    }
}
