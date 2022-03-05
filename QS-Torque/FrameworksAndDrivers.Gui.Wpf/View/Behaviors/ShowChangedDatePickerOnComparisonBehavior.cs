using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Syncfusion.Windows.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public class ShowChangedDatePickerOnComparisonBehavior : ShowChangesBase<DatePicker>
    {
        public Brush NormalBorderBrush { get; set; }

        private static readonly DependencyProperty CompareValueProperty =
            DependencyProperty.Register(nameof(CompareValue), typeof(DateTime), typeof(ShowChangedDatePickerOnComparisonBehavior), new PropertyMetadata(new PropertyChangedCallback(CompareValueChangedCallback)));
        public DateTime CompareValue
        {
            get { return (DateTime)GetValue(CompareValueProperty); }
            set { SetValue(CompareValueProperty, value); }
        }
        private static void CompareValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ShowChangedDatePickerOnComparisonBehavior).CheckIfValueHasChanged();
        }


        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SelectedDateChanged += AssociatedObject_SelectedDateChanged;
            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        
        protected override void OnDetaching()
        {
            this.AssociatedObject.SelectedDateChanged -= AssociatedObject_SelectedDateChanged;
            base.OnDetaching();
        }

        private void AssociatedObject_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckIfValueHasChanged();
        }


        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            NormalBorderBrush = (sender as DatePicker).BorderBrush;
            CheckIfValueHasChanged();
        }

        private void CheckIfValueHasChanged()
        {
            if(!this.AssociatedObject?.IsLoaded ?? true)
            {
                return;
            }

            if(this.AssociatedObject.SelectedDate == null || this.AssociatedObject.SelectedDate.ToDateTime().Date == CompareValue.Date)
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
