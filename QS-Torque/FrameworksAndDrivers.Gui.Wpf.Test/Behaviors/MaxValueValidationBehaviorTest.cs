using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using Microsoft.Xaml.Behaviors;
using NUnit.Framework;
using System;
using System.Threading;
using System.Windows.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Behaviors
{
    class MaxValueValidationBehaviorTest
    {
        TextBox _textBox;
        MaxValueValidationBehavior _numericValidationBehavior;
        MaxValueValidationBehavior _floatingPointValidationBehavior;
        const string MaxValueNumeric = "50";
        const string MaxValueFloatingPoint = "48.7";

        [SetUp]
        public void MaxValueValidationBehaviorSetUp()
        {
            _textBox = new TextBox();
            _numericValidationBehavior = new MaxValueValidationBehavior() { MaxValue = MaxValueNumeric, ValueType = ValidationBehavior.ValidationType.Numeric };
            _floatingPointValidationBehavior = new MaxValueValidationBehavior() { MaxValue = MaxValueFloatingPoint, ValueType = ValidationBehavior.ValidationType.FloatingPoint };

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
        public void EnterValidNumberUnderLimitTest()
        {
            _textBox.Text = "20";

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterValidNumberOverLimitTest()
        {
            _textBox.Text = "80";

            Assert.IsTrue(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterValidNumberOnLimitTest()
        {
            _textBox.Text = MaxValueNumeric;

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterNeagtiveNumberUnderLimitTest()
        {
            _numericValidationBehavior.MaxValue = "-60";
            _textBox.Text = "-80";

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterNeagtiveNumberOverLimitTest()
        {
            _numericValidationBehavior.MaxValue = "-70";
            _textBox.Text = "-50";

            Assert.IsTrue(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterNeagtiveNumberOnLimitTest()
        {
            _numericValidationBehavior.MaxValue = "-70";
            _textBox.Text = "-70";

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ChangeIncorrectToCorrectNumberTest()
        {
            _textBox.Text = "80";
            Assert.IsTrue(_numericValidationBehavior.IsWarningShown);

            _textBox.Text = "20";
            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterValidFloatingPointUnderLimitTest()
        {
            _textBox.Text = "20.6";

            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterValidFloatingPointOverLimitTest()
        {
            _textBox.Text = "80,9";

            Assert.IsTrue(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterValidFloatingPointOnLimitTest()
        {
            _textBox.Text = MaxValueFloatingPoint;

            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterNeagtiveFloatingPointUnderLimitTest()
        {
            _floatingPointValidationBehavior.MaxValue = "-60.65";
            _textBox.Text = "-80.85";

            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterNeagtiveFloatingPointOverLimitTest()
        {
            _floatingPointValidationBehavior.MaxValue = "-70.32";
            _textBox.Text = "-50.014";

            Assert.IsTrue(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void EnterNeagtiveFloatingPointOnLimitTest()
        {
            _floatingPointValidationBehavior.MaxValue = "-70.98";
            _textBox.Text = "-70.98";

            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ChangeIncorrectToCorrectFloatingPointTest()
        {
            _textBox.Text = "80.2";
            Assert.IsTrue(_floatingPointValidationBehavior.IsWarningShown);

            _textBox.Text = "20.3";
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
        public void BehaviorValidatesIfInvalidMaxValueChanged()
        {
            _textBox.Text = "20";
            _numericValidationBehavior.MaxValue = "10";
            _floatingPointValidationBehavior.MaxValue = "10";

            Assert.IsTrue(_numericValidationBehavior.IsWarningShown);
            Assert.IsTrue(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void BehaviorUsesAttachedMaxValueIfUseAttachedMaxValueIsTrue()
        {
            _numericValidationBehavior.MaxValue = "150";
            _floatingPointValidationBehavior.MaxValue = "150";
            _numericValidationBehavior.UseAttachedMaxValue = true;
            _floatingPointValidationBehavior.UseAttachedMaxValue = true;

            MaxValueValidationBehavior.SetMaxValueAttached(_textBox, "100");
            _textBox.Text = "130";

            Assert.IsTrue(_numericValidationBehavior.IsWarningShown);
            Assert.IsTrue(_floatingPointValidationBehavior.IsWarningShown);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void BehaviorValidatesNotIfIsValidationDisabledIsTrue()
        {
            _numericValidationBehavior.MaxValue = "100";
            _floatingPointValidationBehavior.MaxValue = "100";

            MaxValueValidationBehavior.SetIsValidationDisabled(_textBox, true);
            _textBox.Text = "130";

            Assert.IsFalse(_numericValidationBehavior.IsWarningShown);
            Assert.IsFalse(_floatingPointValidationBehavior.IsWarningShown);
        }
    }
}
