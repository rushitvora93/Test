using System;
using System.Globalization;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public class MinValueValidationBehavior : ValidationBehavior
    {
        public bool UseAttachedMinValue { get; set; }

        private ValidationType _valueType;
        public ValidationType ValueType
        {
            get => _valueType;
            set
            {
                if(value == ValidationType.Numeric || value == ValidationType.FloatingPoint)
                {
                    _valueType = value;
                }
                else
                {
                    throw new InvalidOperationException("You have to define a ValueType (Numeric or FloatingPoint) to validate the Value of the TextBox");
                }
            }
        }

        private static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(string), typeof(MinValueValidationBehavior), new PropertyMetadata(MinValueChangedCallback));
        public string MinValue
        {
            get { return (string)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }
        private static void MinValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MinValueValidationBehavior)?.ValidateValue();
        }

        private static readonly DependencyProperty MinValueAttachedProperty =
            DependencyProperty.RegisterAttached("MinValueAttached", typeof(string), typeof(MinValueValidationBehavior));
        public static string GetMinValueAttached(DependencyObject obj)
        {
            return (string)obj.GetValue(MinValueAttachedProperty);
        }
        public static void SetMinValueAttached(DependencyObject obj, string value)
        {
            obj.SetValue(MinValueAttachedProperty, value);
        }

        private static readonly DependencyProperty IsValidationDisabledProperty =
            DependencyProperty.RegisterAttached("IsValidationDisabled", typeof(bool), typeof(MinValueValidationBehavior));
        public static bool GetIsValidationDisabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsValidationDisabledProperty);
        }
        public static void SetIsValidationDisabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsValidationDisabledProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
            base.OnDetaching();
        }

        private void AssociatedObject_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ValidateValue();
        }

        private void ValidateValue()
        {
            if (this.AssociatedObject == null ||  string.IsNullOrWhiteSpace(this.AssociatedObject.Text) || GetIsValidationDisabled(this.AssociatedObject))
            {
                return;
            }

            string minValue = UseAttachedMinValue ? GetMinValueAttached(this.AssociatedObject) : MinValue;

            switch (ValueType)
            {
                case ValidationType.Numeric:
                    try
                    {
                        long number = long.Parse(this.AssociatedObject.Text, NumberStyles.Number, CultureInfo.InvariantCulture);
                        if (number < long.Parse(minValue, NumberStyles.Number, CultureInfo.InvariantCulture))
                        {
                            this.ShowWarning();
                        }
                        else
                        {
                            this.HideWarning();
                        }
                    }
                    catch (Exception) { }
                    break;
                case ValidationType.FloatingPoint:
                    try
                    {
                        double floatingPoint = double.Parse(this.AssociatedObject.Text, CultureInfo.InvariantCulture);
                        if (floatingPoint < double.Parse(minValue, CultureInfo.InvariantCulture))
                        {
                            this.ShowWarning();
                        }
                        else
                        {
                            this.HideWarning();
                        }
                    }
                    catch (Exception) { }
                    break;
                default: throw new InvalidOperationException("You have to define a ValueType (Numeric or FloatingPoint) to validate the Value of the TextBox");
            }
        }
    }
}
