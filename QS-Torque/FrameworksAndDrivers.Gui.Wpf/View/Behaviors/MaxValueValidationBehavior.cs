using System;
using System.Globalization;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public class MaxValueValidationBehavior : ValidationBehavior
    {
        public bool UseAttachedMaxValue { get; set; }

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
        
        private static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(string), typeof(MaxValueValidationBehavior), new PropertyMetadata(new PropertyChangedCallback(MaxValueChangedCallback)));
        public string MaxValue
        {
            get { return (string)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }
        private static void MaxValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MaxValueValidationBehavior)?.ValidateValue();
        }

        private static readonly DependencyProperty MaxValueAttachedProperty =
            DependencyProperty.RegisterAttached("MaxValueAttached", typeof(string), typeof(MaxValueValidationBehavior));
        public static string GetMaxValueAttached(DependencyObject obj)
        {
            return (string) obj.GetValue(MaxValueAttachedProperty);
        }
        public static void SetMaxValueAttached(DependencyObject obj, string value)
        {
            obj.SetValue(MaxValueAttachedProperty, value);
        }

        private static readonly DependencyProperty IsValidationDisabledProperty =
            DependencyProperty.RegisterAttached("IsValidationDisabled", typeof(bool), typeof(MaxValueValidationBehavior));

        public static bool GetIsValidationDisabled(DependencyObject obj)
        {
            return (bool) obj.GetValue(IsValidationDisabledProperty);
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

        protected override void CloneCore(Freezable sourceFreezable)
        {
            /*
             * If a Behavior should be settable with the Behavior Setter it as to override this method
             * Otherwise specific attributes would vanish while the behavior is set
             * 
             * See this for example
             */

            base.CloneCore(sourceFreezable);

            MaxValue = (sourceFreezable as MaxValueValidationBehavior).MaxValue;
            ValueType = (sourceFreezable as MaxValueValidationBehavior).ValueType;
        }

        private void AssociatedObject_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ValidateValue();
        }

        private void ValidateValue()
        {
            if (this.AssociatedObject == null || string.IsNullOrWhiteSpace(this.AssociatedObject.Text) || GetIsValidationDisabled(this.AssociatedObject))
            {
                return;
            }

            var maxValue = UseAttachedMaxValue ? GetMaxValueAttached(this.AssociatedObject) : MaxValue;

            switch (ValueType)
            {
                case ValidationType.Numeric:
                    try
                    {
                        long number = long.Parse(this.AssociatedObject.Text, NumberStyles.Number, CultureInfo.InvariantCulture);
                        if (number > long.Parse(maxValue, NumberStyles.Number, CultureInfo.InvariantCulture))
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
                        if (floatingPoint > double.Parse(maxValue, CultureInfo.InvariantCulture))
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
