using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.View.Themes;
using State;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public class ShowChangedComboBoxBehavior : ShowChangesBase<ComboBox>
    {
        protected override void OnSetup()
        {
            base.OnSetup();
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
            AssociatedObject.Unloaded += AssociatedObjectOnUnloaded;
            (Application.Current.Resources[ResourceKeys.ApplicationThemeDictionaryKey] as ThemeDictionary).ThemeChanged += ThemeDictionaryOnThemeChanged;
            BaseValue = this.AssociatedObject.SelectedItem;
        }

        private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckIfSelectionIsChanged();
        }

        private void OnClearShownChanges(object sender, System.EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                // Reset BaseValue
                BaseValue = this.AssociatedObject.SelectedItem;
            });
        }

        private void ThemeDictionaryOnThemeChanged(object sender, Theme e)
        {
            CheckIfSelectionIsChanged();
        }

        private void AssociatedObjectOnUnloaded(object sender, RoutedEventArgs e)
        {
            OnDetaching();
        }

        protected override void OnCleanup()
        {
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
            AssociatedObject.Unloaded -= AssociatedObjectOnUnloaded;
            (Application.Current.Resources[ResourceKeys.ApplicationThemeDictionaryKey] as ThemeDictionary).ThemeChanged -= ThemeDictionaryOnThemeChanged;
            base.OnCleanup();
        }

        protected override void ClearShownChangesParentChanged(IClearShownChanges oldValue, IClearShownChanges newValue)
        {
            if (oldValue != null)
            {
                oldValue.ClearShownChanges -= OnClearShownChanges;
            }

            if (newValue != null)
            {
                newValue.ClearShownChanges += OnClearShownChanges;
            }

            base.ClearShownChangesParentChanged(oldValue, newValue);
        }

        private void CheckIfSelectionIsChanged()
        {
            if (!this.AssociatedObject.SelectedItem?.Equals(BaseValue) ?? false)
            {
                ShowControlAsChanged();
            }
            else
            {
                ShowControlAsNormal();
            }
        }
    }

    public class ShowChangedComboBoxOnComparisonBehavior : ShowChangesBase<ComboBox>
    {
        public Brush NormalBorderBrush { get; set; }

        private static readonly DependencyProperty CompareValueProperty =
            DependencyProperty.Register(nameof(CompareValue), typeof(object), typeof(ShowChangedComboBoxOnComparisonBehavior), new PropertyMetadata(new PropertyChangedCallback(CompareValueChangedCallback)));
        public object CompareValue
        {
            get { return (object)GetValue(CompareValueProperty); }
            set { SetValue(CompareValueProperty, value); }
        }
        private static void CompareValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ShowChangedComboBoxOnComparisonBehavior).CheckIfValueHasChanged();
        }


        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
            this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
            base.OnDetaching();
        }

        private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckIfValueHasChanged();
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.AssociatedObject.BorderBrush != null && (this.AssociatedObject.BorderBrush as SolidColorBrush).Color.ToString() != DefaultChangedFieldColor)
            {
                NormalBorderBrush = (sender as ComboBox).BorderBrush;
            }
            else
            {
                NormalBorderBrush = SystemColors.ControlDarkBrush;
            }
            CheckIfValueHasChanged();
        }

        protected virtual void CheckIfValueHasChanged()
        {
            if (!this.AssociatedObject?.IsLoaded ?? true)
            {
                return;
            }

            if (this.AssociatedObject.SelectedItem?.Equals(CompareValue) ?? true)
            {
                this.AssociatedObject.BorderBrush = NormalBorderBrush;
            }
            else
            {
                ShowControlAsChanged();
            }
        }
    }

    public class ShowTableComboBoxChangedBehaviour : ShowChangedComboBoxOnComparisonBehavior
    {
        private static readonly DependencyProperty CompareFunctionProperty =
            DependencyProperty.Register(nameof(CompareFunction), typeof(Func<long, object, object, bool>), typeof(ShowTableComboBoxChangedBehaviour), new PropertyMetadata(new PropertyChangedCallback(CompareFunctionChangedCallback)));
        /// <summary>
        /// Parameter 1: Id of corresponding entity
        /// Parameter 2: The current value in the ComboBox
        /// Parameter 3: Additional parameter to determine the result
        /// Returns: true if the value has changed
        /// </summary>
        public Func<long, object, object, bool> CompareFunction
        {
            get { return (Func<long, object, object, bool>)GetValue(CompareFunctionProperty); }
            set { SetValue(CompareFunctionProperty, value); }
        }
        private static void CompareFunctionChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ShowTableComboBoxChangedBehaviour).CheckIfValueHasChanged();
        }

        private static readonly DependencyProperty CompareQstIdProperty =
            DependencyProperty.Register(nameof(CompareQstId), typeof(long), typeof(ShowTableComboBoxChangedBehaviour), new PropertyMetadata(new PropertyChangedCallback(ComapareQstIdChangedCallback)));
        public long CompareQstId
        {
            get { return (long)GetValue(CompareQstIdProperty); }
            set { SetValue(CompareQstIdProperty, value); }
        }
        private static void ComapareQstIdChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ShowTableComboBoxChangedBehaviour).CheckIfValueHasChanged();
        }

        private static readonly DependencyProperty CompareParameterProperty =
            DependencyProperty.Register(nameof(CompareParameter), typeof(object), typeof(ShowTableComboBoxChangedBehaviour), new PropertyMetadata(new PropertyChangedCallback(CompareParameterChangedCallback)));
        public object CompareParameter
        {
            get { return GetValue(CompareParameterProperty); }
            set { SetValue(CompareParameterProperty, value); }
        }
        private static void CompareParameterChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ShowTableComboBoxChangedBehaviour).CheckIfValueHasChanged();
        }

        protected override void CheckIfValueHasChanged()
        {
            if (!this.AssociatedObject?.IsLoaded ?? true)
            {
                return;
            }

            if(this.AssociatedObject.SelectedItem == null)
            {
                this.AssociatedObject.BorderBrush = NormalBorderBrush;
                return;
            }

            if (!CompareFunction?.Invoke(CompareQstId, this.AssociatedObject.SelectedItem, CompareParameter) ?? true)
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
