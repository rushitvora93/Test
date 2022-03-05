using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using Microsoft.Xaml.Behaviors;
using NUnit.Framework;
using System.Threading;
using System.Windows.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Behaviors
{
    class ValueValidationBehaviorTest
    {
        TextBox _textBox;
        ValueValidationBehavior _numericValidationBehavior;
        ValueValidationBehavior _floatingPointValidationBehavior;
        ValueValidationBehavior _textValidationBehavior;
        const string ForbiddenValueNumeric = "50";
        const string ForbiddenValueFloatingPoint = "48.7";
        const string ForbiddenValueText = "fdsajklö";

        [SetUp]
        public void ValueValidationBehaviorSetUp()
        {
            _textBox = new TextBox();
            _numericValidationBehavior = new ValueValidationBehavior() { ForbiddenValue = ForbiddenValueNumeric, ValueType = ValidationBehavior.ValidationType.Numeric };
            _floatingPointValidationBehavior = new ValueValidationBehavior() { ForbiddenValue = ForbiddenValueFloatingPoint, ValueType = ValidationBehavior.ValidationType.FloatingPoint };
            _textValidationBehavior = new ValueValidationBehavior() { ForbiddenValue = ForbiddenValueText, ValueType = ValidationBehavior.ValidationType.Text };

            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);
            textBoxBehaviors.Add(_numericValidationBehavior);
            textBoxBehaviors.Add(_floatingPointValidationBehavior);
            textBoxBehaviors.Add(_textValidationBehavior);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterWhiteSpaceTest()
        {
            _textBox.Text = "   \n";

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
            Assert.IsFalse(_textValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterNullTest()
        {
            _textBox.Text = null;

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
            Assert.IsFalse(_textValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterAllowedValidNumberTest()
        {
            _textBox.Text = "20";

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterForbiddenNumberTest()
        {
            _textBox.Text = ForbiddenValueNumeric;

            Assert.IsTrue(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterAllowedNegativeNumberTest()
        {
            _numericValidationBehavior.ForbiddenValue = "-90";
            _textBox.Text = "-20";

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterForbiddenNegativeNumberTest()
        {
            _numericValidationBehavior.ForbiddenValue = "-90";
            _textBox.Text = "-90";

            Assert.IsTrue(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterAllowedValidFloatingPointTest()
        {
            _textBox.Text = "20.90";

            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterForbiddenFloatingPointTest()
        {
            _textBox.Text = ForbiddenValueFloatingPoint;

            Assert.IsTrue(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterAllowedNegativeFloatingPointTest()
        {
            _floatingPointValidationBehavior.ForbiddenValue = "-90.3";
            _textBox.Text = "-20.7";

            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterForbiddenNegativeFloatingPointTest()
        {
            _floatingPointValidationBehavior.ForbiddenValue = "-90.3";
            _textBox.Text = "-90.3";

            Assert.IsTrue(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterAllowedValidTextTest()
        {
            _textBox.Text = "ghdxvnksfpoij";

            Assert.IsFalse(_textValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterForbiddenTextTest()
        {
            _textBox.Text = ForbiddenValueText;

            Assert.IsTrue(_textValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterInvalidInputTest()
        {
            _textBox.Text = "bfhdnmkjh";

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void BehaviorValidatesIfInvalidForbiddenValueChanged()
        {
            _textBox.Text = "30";
            _numericValidationBehavior.ForbiddenValue = "30";
            _floatingPointValidationBehavior.ForbiddenValue = "30";
            _textValidationBehavior.ForbiddenValue = "30";

            Assert.IsTrue(_numericValidationBehavior.IsWarningShown);
            Assert.IsTrue(_floatingPointValidationBehavior.IsWarningShown);
            Assert.IsTrue(_textValidationBehavior.IsWarningShown);
        }
    }
}
