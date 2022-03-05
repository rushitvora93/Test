using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public class ShowChangedToggleButtonOnComparisonBehavior : ShowChangesBase<ToggleButton>
    {
        public Brush NormalBorderBrush { get; set; }

        private static readonly DependencyProperty CompareValueProperty =
            DependencyProperty.Register(nameof(CompareValue), typeof(bool), typeof(ShowChangedToggleButtonOnComparisonBehavior), new PropertyMetadata(new PropertyChangedCallback(CompareValueChangedCallback)));
        public bool CompareValue
        {
            get { return (bool)GetValue(CompareValueProperty); }
            set { SetValue(CompareValueProperty, value); }
        }
        private static void CompareValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ShowChangedToggleButtonOnComparisonBehavior).CheckIfValueHasChanged();
        }


        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.Checked += AssociatedObject_Checked;
            this.AssociatedObject.Unchecked += AssociatedObject_Unchecked;
            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.Checked -= AssociatedObject_Checked;
            this.AssociatedObject.Unchecked -= AssociatedObject_Unchecked;
            this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
            base.OnDetaching();
        }

        private void AssociatedObject_Checked(object sender, RoutedEventArgs e)
        {
            CheckIfValueHasChanged();
        }

        private void AssociatedObject_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckIfValueHasChanged();
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.AssociatedObject.BorderBrush != null && (this.AssociatedObject.BorderBrush as SolidColorBrush).Color.ToString() != DefaultChangedFieldColor)
            {
                NormalBorderBrush = (sender as ToggleButton).BorderBrush;
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

            if(this.AssociatedObject.IsChecked == CompareValue)
            {
                this.AssociatedObject.BorderBrush = NormalBorderBrush;
                this.AssociatedObject.BorderThickness = new Thickness(1);
            }
            else
            {
                ShowControlAsChanged();
                this.AssociatedObject.BorderThickness = new Thickness(2);
            }
        }
    }
}
