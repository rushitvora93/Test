using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.View.Themes;
using State;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public abstract class ShowChangedTextBoxBaseBehavior : ShowChangesBase<TextBox>
    {
        private void NumericTextBox_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckIfTextIsChanged();
        }

        protected override void OnSetup()
        {
            base.OnSetup();

            if (AssociatedObject is Syncfusion.Windows.Shared.DoubleTextBox)
            {
                (AssociatedObject as Syncfusion.Windows.Shared.DoubleTextBox).ValueChanged += NumericTextBox_ValueChanged;
                (AssociatedObject as Syncfusion.Windows.Shared.DoubleTextBox).IsVisibleChanged += AssociatedObject_IsVisibleChanged;
            }
            else if (AssociatedObject is Syncfusion.Windows.Shared.IntegerTextBox)
            {
                (AssociatedObject as Syncfusion.Windows.Shared.IntegerTextBox).ValueChanged += NumericTextBox_ValueChanged;
                (AssociatedObject as Syncfusion.Windows.Shared.IntegerTextBox).IsVisibleChanged += AssociatedObject_IsVisibleChanged;
            }
            else
            {
                AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;
                AssociatedObject.IsVisibleChanged += AssociatedObject_IsVisibleChanged;
            }

            AssociatedObject.Unloaded += AssociatedObjectOnUnloaded;
            (Application.Current.Resources[ResourceKeys.ApplicationThemeDictionaryKey] as ThemeDictionary).ThemeChanged += ThemeDictionaryOnThemeChanged;
            BaseValue = this.AssociatedObject.Text;
        }

        private void AssociatedObject_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CheckIfTextIsChanged();
        }

        private void AssociatedObjectOnUnloaded(object sender, RoutedEventArgs e)
        {
            OnDetaching();
        }

        private void AssociatedObjectOnTextChanged(object sender, TextChangedEventArgs e)
        {
            CheckIfTextIsChanged();
        }

        private void ThemeDictionaryOnThemeChanged(object sender, Theme e)
        {
            CheckIfTextIsChanged();
        }

        protected override void OnCleanup()
        {
            if (AssociatedObject is Syncfusion.Windows.Shared.DoubleTextBox)
            {
                (AssociatedObject as Syncfusion.Windows.Shared.DoubleTextBox).ValueChanged -= NumericTextBox_ValueChanged;
                (AssociatedObject as Syncfusion.Windows.Shared.DoubleTextBox).IsVisibleChanged -= AssociatedObject_IsVisibleChanged;
            }
            else if (AssociatedObject is Syncfusion.Windows.Shared.IntegerTextBox)
            {
                (AssociatedObject as Syncfusion.Windows.Shared.IntegerTextBox).ValueChanged -= NumericTextBox_ValueChanged;
                (AssociatedObject as Syncfusion.Windows.Shared.IntegerTextBox).IsVisibleChanged -= AssociatedObject_IsVisibleChanged;
            }
            else
            {
                AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
                AssociatedObject.IsVisibleChanged -= AssociatedObject_IsVisibleChanged;
            }

            AssociatedObject.Unloaded -= AssociatedObjectOnUnloaded;
            (Application.Current.Resources[ResourceKeys.ApplicationThemeDictionaryKey] as ThemeDictionary).ThemeChanged -= ThemeDictionaryOnThemeChanged;
            base.OnCleanup();
        }

        protected abstract void CheckIfTextIsChanged();
    }

    public class ShowChangedTextBoxBehavior : ShowChangedTextBoxBaseBehavior
    {
        private void OnClearShownChanges(object sender, System.EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                // Reset BaseValue
                BaseValue = this.AssociatedObject.Text;
            });
        }

        protected override void ClearShownChangesParentChanged(IClearShownChanges oldValue, IClearShownChanges newValue)
        {
            if(oldValue != null)
            {
                oldValue.ClearShownChanges -= OnClearShownChanges;
            }

            if(newValue != null)
            {
                newValue.ClearShownChanges += OnClearShownChanges;
            }

            base.ClearShownChangesParentChanged(oldValue, newValue);
        }

        protected override void CheckIfTextIsChanged()
        {
            if (this.AssociatedObject.Text != (string)BaseValue)
            {
                ShowControlAsChanged();
            }
            else
            {
                ShowControlAsNormal();
            }
        }
    }

    public class ShowChangedFromComparisonTextBoxBehavior : ShowChangedTextBoxBaseBehavior
    {
        public Brush NormalBorderBrush { get; set; }

        private static readonly DependencyProperty CompareToProperty =
            DependencyProperty.Register(
                nameof(CompareTo),
                typeof(object),
                typeof(ShowChangedFromComparisonTextBoxBehavior),
                new PropertyMetadata((target, args) =>
                {
                    (target as ShowChangedFromComparisonTextBoxBehavior).CheckIfTextIsChanged();
                }));

        public object CompareTo
        {
            get => GetValue(CompareToProperty);
            set => SetValue(CompareToProperty, value);
        }

        protected override void CheckIfTextIsChanged()
        {
            if (!AssociatedObject.IsLoaded)
            {
                return;
            }

            if (CompareTo == null)
            {
                AssociatedObject.BorderBrush = NormalBorderBrush;
                return;
            }

            if (CompareTo.ToString() != AssociatedObject.Text)
            {
                ShowControlAsChanged();
            }
            else
            {
                AssociatedObject.BorderBrush = NormalBorderBrush;
            }
        }

        protected override void OnSetup()
        {
            base.OnSetup();
            if (AssociatedObject != null)
            {
                AssociatedObject.Loaded += AssociatedObjectOnLoaded;
            }
        }

        protected override void OnCleanup()
        {
            base.OnCleanup();
            AssociatedObject.Loaded -= AssociatedObjectOnLoaded;
        }

        private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs args)
        {
            if (this.AssociatedObject.BorderBrush != null && (this.AssociatedObject.BorderBrush as SolidColorBrush).Color.ToString() != DefaultChangedFieldColor)
            {
                NormalBorderBrush = (sender as TextBox).BorderBrush;
            }
            else
            {
                NormalBorderBrush = SystemColors.ControlDarkBrush;
            }
            CheckIfTextIsChanged();
        }
    }

    public class ShowChangedFromComparisonDoubleTextBoxBehavior : ShowChangedTextBoxBaseBehavior
    {
        public Brush NormalBorderBrush { get; set; }

        private static readonly DependencyProperty CompareToProperty =
            DependencyProperty.Register(
                nameof(CompareTo),
                typeof(object),
                typeof(ShowChangedFromComparisonDoubleTextBoxBehavior),
                new PropertyMetadata((target, args) =>
                {
                    (target as ShowChangedFromComparisonDoubleTextBoxBehavior).CheckIfTextIsChanged();
                }));

        public object CompareTo
        {
            get => GetValue(CompareToProperty);
            set => SetValue(CompareToProperty, value);
        }

        protected override void CheckIfTextIsChanged()
        {
            if (!AssociatedObject.IsLoaded)
            {
                return;
            }

            if (CompareTo == null)
            {
                AssociatedObject.BorderBrush = NormalBorderBrush;
                return;
            }

            double.TryParse(
                CompareTo.ToString(),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var compareTo);

            double.TryParse(
                AssociatedObject.Text,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var associatedValue);

            if (compareTo != associatedValue)
            {
                ShowControlAsChanged();
            }
            else
            {
                AssociatedObject.BorderBrush = NormalBorderBrush;
            }
        }

        protected override void OnSetup()
        {
            base.OnSetup();
            if (AssociatedObject != null)
            {
                AssociatedObject.Loaded += AssociatedObjectOnLoaded;
            }
        }

        protected override void OnCleanup()
        {
            base.OnCleanup();
            AssociatedObject.Loaded -= AssociatedObjectOnLoaded;
        }

        private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs args)
        {
            if (this.AssociatedObject.BorderBrush == null || this.AssociatedObject.BorderBrush is not SolidColorBrush)
            {
                NormalBorderBrush = SystemColors.ControlDarkBrush;
            }
            else if ((this.AssociatedObject.BorderBrush as SolidColorBrush).Color.ToString() != DefaultChangedFieldColor)
            {
                NormalBorderBrush = (sender as TextBox).BorderBrush;
            }
            CheckIfTextIsChanged();
        }
    }
}
