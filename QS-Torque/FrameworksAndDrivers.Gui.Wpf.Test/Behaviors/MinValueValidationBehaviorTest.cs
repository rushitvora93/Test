using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using Microsoft.Xaml.Behaviors;
using NUnit.Framework;
using System;
using System.Threading;
using System.Windows.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Behaviors
{
    class MinValueValidationBehaviorTest
    {
        TextBox _textBox;
        MinValueValidationBehavior _numericValidationBehavior;
        MinValueValidationBehavior _floatingPointValidationBehavior;
        const string MinValueNumeric = "50";
        const string MinValueFloatingPoint = "48.7";

        [SetUp]
        public void MinValueValidationBehaviorSetUp()
        {
            _textBox = new TextBox();
            _numericValidationBehavior = new MinValueValidationBehavior() { MinValue = MinValueNumeric, ValueType = ValidationBehavior.ValidationType.Numeric };
            _floatingPointValidationBehavior = new MinValueValidationBehavior() { MinValue = MinValueFloatingPoint, ValueType = ValidationBehavior.ValidationType.FloatingPoint };

            var textBoxBehaviors = Interaction.GetBehaviors(_textBox);
            textBoxBehaviors.Add(_numericValidationBehavior);
            textBoxBehaviors.Add(_floatingPointValidationBehavior);
        }


        [Test, RequiresThread(ApartmentState.STA)]
        public void ValueTypeTextTest()
        {
            var behavior = new MaxValueValidationBehavior();
            Assert.Catch<InvalidOperationException>(() => { behavior.ValueType = ValidationBehavior.ValidationType.Text; });
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterWhiteSpaceTest()
        {
            _textBox.Text = "   \n";

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterNullTest()
        {
            _textBox.Text = null;

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterValidNumberOverLimitTest()
        {
            _textBox.Text = "80";

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterValidNumberUnterLimitTest()
        {
            _textBox.Text = "20";

            Assert.IsTrue(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterValidNumberOnLimitTest()
        {
            _textBox.Text = MinValueNumeric;

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterNeagtiveNumberOverLimitTest()
        {
            _numericValidationBehavior.MinValue = "-80";
            _textBox.Text = "-60";

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterNeagtiveNumberUnderLimitTest()
        {
            _numericValidationBehavior.MinValue = "-50";
            _textBox.Text = "-70";

            Assert.IsTrue(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterNeagtiveNumberOnLimitTest()
        {
            _numericValidationBehavior.MinValue = "-70";
            _textBox.Text = "-70";

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ChangeIncorrectToCorrectNumberTest()
        {
            _textBox.Text = "20";
            Assert.IsTrue(_numericValidationBehavior.IsWarningShown);

            _textBox.Text = "80";
            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterValidFloatingPointOverLimitTest()
        {
            _textBox.Text = "100,7";

            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterValidFloatingPointUnderLimitTest()
        {
            _textBox.Text = "0.003";

            Assert.IsTrue(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterValidFloatingPointOnLimitTest()
        {
            _textBox.Text = MinValueFloatingPoint;

            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterNeagtiveFloatingPointOverLimitTest()
        {
            _floatingPointValidationBehavior.MinValue = "-80.85";
            _textBox.Text = "-60.65";

            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterNeagtiveFloatingPointUnderLimitTest()
        {
            _floatingPointValidationBehavior.MinValue = "-50.014";
            _textBox.Text = "-70.32";

            Assert.IsTrue(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterNeagtiveFloatingPointOnLimitTest()
        {
            _floatingPointValidationBehavior.MinValue = "-70.98";
            _textBox.Text = "-70.98";

            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ChangeIncorrectToCorrectFloatingPointTest()
        {
            _textBox.Text = "10.2";
            Assert.IsTrue(_floatingPointValidationBehavior.IsWarningShown);

            _textBox.Text = "60,3";
            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterInvalidInputTest()
        {
            _textBox.Text = "jhfdksl";

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void BehaviorValidatesIfInvalidMinValueChanged()
        {
            _textBox.Text = "100";
            _numericValidationBehavior.MinValue = "110";
            _floatingPointValidationBehavior.MinValue = "110";

            Assert.IsTrue(_numericValidationBehavior.IsWarningShown);
            Assert.IsTrue(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void BehaviorUsesAttachedMinValueIfUseAttachedMinValueIsTrue()
        {
            _numericValidationBehavior.MinValue = "100";
            _floatingPointValidationBehavior.MinValue = "100";
            _numericValidationBehavior.UseAttachedMinValue = true;
            _floatingPointValidationBehavior.UseAttachedMinValue = true;

            MinValueValidationBehavior.SetMinValueAttached(_textBox, "150");
            _textBox.Text = "130";

            Assert.IsTrue(_numericValidationBehavior.IsWarningShown);
            Assert.IsTrue(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void BehaviorValidatesNotIfIsValidationDisabledIsTrue()
        {
            _numericValidationBehavior.MinValue = "130";
            _floatingPointValidationBehavior.MinValue = "130";

            MinValueValidationBehavior.SetIsValidationDisabled(_textBox, true);
            _textBox.Text = "100";

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
        }
    }
}
