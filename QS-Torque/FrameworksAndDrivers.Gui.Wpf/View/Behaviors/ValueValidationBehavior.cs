using System;
using System.Globalization;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public class ValueValidationBehavior : ValidationBehavior
    {
        public ValidationType ValueType { get; set; } = ValidationType.Text;
        
        private static readonly DependencyProperty ForbiddenValueProperty =
            DependencyProperty.Register("ForbiddenValue", typeof(string), typeof(ValueValidationBehavior), new PropertyMetadata(ForbiddenValueChangedCallback));
        public string ForbiddenValue
        {
            get { return (string)GetValue(ForbiddenValueProperty); }
            set { SetValue(ForbiddenValueProperty, value); }
        }
        private static void ForbiddenValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ValueValidationBehavior)?.ValidateValue();
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
            if (this.AssociatedObject == null || string.IsNullOrWhiteSpace(this.AssociatedObject.Text))
            {
                return;
            }

            switch (ValueType)
            {
                case ValidationType.Numeric:
                    try
                    {
                        long number = long.Parse(this.AssociatedObject.Text, NumberStyles.Number, CultureInfo.InvariantCulture);
                        if (number == long.Parse(ForbiddenValue, NumberStyles.Number, CultureInfo.InvariantCulture))
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
                        if (floatingPoint == double.Parse(ForbiddenValue, CultureInfo.InvariantCulture))
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
                case ValidationType.Text:
                    string text = this.AssociatedObject.Text;
                    if (text == ForbiddenValue)
                    {
                        this.ShowWarning();
                    }
                    else
                    {
                        this.HideWarning();
                    }
                    break;
                default: throw new InvalidOperationException("You have to define a ValueType to validate the Value of the TextBox");
            }
        }
    }
}
