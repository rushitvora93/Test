using System;
using System.Windows;
using System.Windows.Media;
using Syncfusion.Windows.Controls.Input;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public class ShowChangedTimePickerOnComparisonBehavior : ShowChangesBase<SfTimePicker>
    {
        public Brush NormalBorderBrush { get; set; }

        private object _currentTimePickerValue;

        private static readonly DependencyProperty CompareValueProperty =
            DependencyProperty.Register(nameof(CompareValue), typeof(object), typeof(ShowChangedTimePickerOnComparisonBehavior), new PropertyMetadata(new PropertyChangedCallback(CompareValueChangedCallback)));
        public object CompareValue
        {
            get { return (object)GetValue(CompareValueProperty); }
            set { SetValue(CompareValueProperty, value); }
        }
        private static void CompareValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ShowChangedTimePickerOnComparisonBehavior).CheckIfValueHasChanged();
        }


        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.ValueChanged += AssociatedObject_ValueChanged;
            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
            this.AssociatedObject.IsEnabledChanged += AssociatedObject_IsEnabledChanged;
        }
        
        protected override void OnDetaching()
        {
            this.AssociatedObject.ValueChanged -= AssociatedObject_ValueChanged;
            this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
            this.AssociatedObject.IsEnabledChanged -= AssociatedObject_IsEnabledChanged;
            base.OnDetaching();
        }

        private void AssociatedObject_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            _currentTimePickerValue = e.NewValue;
            CheckIfValueHasChanged();
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if ((this.AssociatedObject.BorderBrush as SolidColorBrush).Color.ToString() != DefaultChangedFieldColor)
            {
                NormalBorderBrush = (sender as SfTimePicker).BorderBrush;
            }
            CheckIfValueHasChanged();
        }

        private void AssociatedObject_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((!this.AssociatedObject?.IsLoaded ?? true))
            {
                return;
            }
            
            if (this.AssociatedObject.IsEnabled)
            {
                this.AssociatedObject.BorderBrush = new SolidColorBrush()
                {
                    Color = (this.AssociatedObject.BorderBrush as SolidColorBrush).Color,
                    Opacity = 1
                };
            }
            else
            {
                this.AssociatedObject.BorderBrush = new SolidColorBrush()
                {
                    Color = (this.AssociatedObject.BorderBrush as SolidColorBrush).Color,
                    Opacity = 0.4
                };
            }
        }

        private void CheckIfValueHasChanged()
        {
            if ((!this.AssociatedObject?.IsLoaded ?? true) || _currentTimePickerValue == null)
            {
                return;
            }
            
            if (_currentTimePickerValue.Equals(CompareValue))
            {
                this.AssociatedObject.BorderBrush = NormalBorderBrush;
            }
            else if(_currentTimePickerValue is string s && CompareValue is TimeSpan timespan && TimeSpan.Parse(s).Equals(timespan))
            {
                this.AssociatedObject.BorderBrush = NormalBorderBrush;
            }
            else if (_currentTimePickerValue is DateTime dateTime && CompareValue is TimeSpan timeSpan && dateTime.TimeOfDay.Equals(timeSpan))
            {
                this.AssociatedObject.BorderBrush = NormalBorderBrush;
            }
            else
            {
                ShowControlAsChanged();
            }
        }
    }
}
