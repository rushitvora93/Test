using System.Windows;
using System.Windows.Media;
using Syncfusion.Windows.Shared;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public class ShowChangedUpDownOnComparisonBehavior : ShowChangesBase<UpDown>
    {
        public Brush NormalBorderBrush { get; set; }

        private static readonly DependencyProperty CompareValueProperty =
            DependencyProperty.Register(nameof(CompareValue), typeof(int), typeof(ShowChangedUpDownOnComparisonBehavior), new PropertyMetadata(new PropertyChangedCallback(CompareValueChangedCallback)));
        public int CompareValue
        {
            get { return (int)GetValue(CompareValueProperty); }
            set { SetValue(CompareValueProperty, value); }
        }
        private static void CompareValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ShowChangedUpDownOnComparisonBehavior).CheckIfValueHasChanged();
        }


        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.ValueChanged += AssociatedObject_ValueChanged;
            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.ValueChanged -= AssociatedObject_ValueChanged;
            this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
            base.OnDetaching();
        }

        private void AssociatedObject_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckIfValueHasChanged();
        }


        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.AssociatedObject.BorderBrush != null && (this.AssociatedObject.BorderBrush as SolidColorBrush).Color.ToString() != DefaultChangedFieldColor)
            {
                NormalBorderBrush = (sender as UpDown).BorderBrush;
            }
            else
            {
                NormalBorderBrush = SystemColors.ControlDarkBrush;
            }
            CheckIfValueHasChanged();
        }

        private void CheckIfValueHasChanged()
        {
            if(!this.AssociatedObject?.IsLoaded ?? true)
            {
                return;
            }

            if(this.AssociatedObject.Value == CompareValue)
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
