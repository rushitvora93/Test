﻿using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.View.Themes;
using State;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public class ShowNormalComboBoxBehavior : BehaviorBase<ComboBox>
    {
        public Brush NormalBorderBrush { get; set; }

        private static readonly DependencyProperty ClearShownChangesParentProperty =
            DependencyProperty.Register("ClearShownChangesParent", typeof(IClearShownChanges), typeof(ShowNormalComboBoxBehavior), new PropertyMetadata(new PropertyChangedCallback(ClearShownChangesParentChangeCallback)));
        public IClearShownChanges ClearShownChangesParent
        {
            get { return (IClearShownChanges)GetValue(ClearShownChangesParentProperty); }
            set { SetValue(ClearShownChangesParentProperty, value); }
        }
        private static void ClearShownChangesParentChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                (e.OldValue as IClearShownChanges).ClearShownChanges -= (d as ShowNormalComboBoxBehavior).OnClearShownChanges;
            }

            if (e.NewValue != null)
            {
                (e.NewValue as IClearShownChanges).ClearShownChanges += (d as ShowNormalComboBoxBehavior).OnClearShownChanges;
            }
        }

        protected override void OnSetup()
        {
            base.OnSetup();
            if (AssociatedObject != null)
            {
                AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
                AssociatedObject.Unloaded += AssociatedObjectOnUnloaded;
                AssociatedObject.Loaded += AssociatedObjectOnLoaded;
            }
            (Application.Current.Resources[ResourceKeys.ApplicationThemeDictionaryKey] as ThemeDictionary).ThemeChanged += ThemeDictionaryOnThemeChanged;
        }

        private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckForEmptyBorderBrush();
        }

        private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs e)
        {
            NormalBorderBrush = AssociatedObject.BorderBrush;
        }

        private void ThemeDictionaryOnThemeChanged(object sender, Theme e)
        {
            CheckForEmptyBorderBrush();
        }

        private void OnClearShownChanges(object sender, System.EventArgs e)
        {
            Dispatcher.Invoke(() => CheckForEmptyBorderBrush());
        }

        private void AssociatedObjectOnUnloaded(object sender, RoutedEventArgs e)
        {
            OnDetaching();
        }

        protected override void OnCleanup()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
                AssociatedObject.Unloaded -= AssociatedObjectOnUnloaded;
                AssociatedObject.Loaded -= AssociatedObjectOnLoaded;
            }
            (Application.Current.Resources[ResourceKeys.ApplicationThemeDictionaryKey] as ThemeDictionary).ThemeChanged -= ThemeDictionaryOnThemeChanged;
            base.OnCleanup();
        }

        private void CheckForEmptyBorderBrush()
        {
            if (AssociatedObject.BorderBrush == null)
            {
                AssociatedObject.BorderBrush = NormalBorderBrush;
            }
        }
    }
}
