using Microsoft.Xaml.Behaviors;
using State;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public class ValidationBehavior : Behavior<TextBox>
    {
        public enum ValidationType
        {
            Numeric, 
            FloatingPoint,
            Text
        }
        
        private static readonly DependencyProperty WarningTextProperty =
            DependencyProperty.Register("WarningText", typeof(string), typeof(ValidationBehavior));
        public string WarningText
        {
            get { return (string)GetValue(WarningTextProperty); }
            set { SetValue(WarningTextProperty, value); }
        }

        private static readonly DependencyProperty WarningTextAttachedProperty =
            DependencyProperty.RegisterAttached("WarningTextAttached", typeof(string), typeof(ValidationBehavior));
        public static string GetWarningTextAttached(DependencyObject obj)
        {
            return (string)obj.GetValue(WarningTextAttachedProperty);
        }
        public static void SetWarningTextAttached(DependencyObject obj, string value)
        {
            obj.SetValue(WarningTextAttachedProperty, value);
        }

        public bool IsWarningShown { get; private set; }
        public bool UseAttachedWarningText { get; set; }

        private DispatcherTimer _timer;
        private Popup _currentPopup;


        #region Overrides
        
        protected override void OnDetaching()
        {
            base.OnDetaching();
            HideWarning();
        }

        #endregion


        #region Methods

        protected void ShowWarning()
        {
            if(IsWarningShown)
            {
                return;
            }
            
            var popup = new Popup()
            {
                PlacementTarget = this.AssociatedObject,
                Placement = PlacementMode.Bottom
            };

            var textBlock = new TextBlock()
            {
                Text = UseAttachedWarningText ? GetWarningTextAttached(this.AssociatedObject) : WarningText,
                Background = Application.Current?.Resources[ResourceKeys.WindowBackgroundBrushKey] as SolidColorBrush,
                Foreground = Application.Current?.Resources[ResourceKeys.ForegroundBrushKey] as SolidColorBrush,
                Margin = new Thickness(5)
            };

            popup.Child = new Border()
            {
                BorderBrush = Application.Current?.Resources[ResourceKeys.WarningBrushKey] as SolidColorBrush,
                BorderThickness = new Thickness(2),
                Background = Application.Current?.Resources[ResourceKeys.WindowBackgroundBrushKey] as SolidColorBrush,
                Child = textBlock
            };

            popup.IsOpen = true;
            IsWarningShown = true;
            _currentPopup = popup;

            _timer = new DispatcherTimer();
            _timer.Tick += (_, _) =>
            {
                _timer.Stop();
                Dispatcher.Invoke(() => { HideWarning(); });
            };
            _timer.Interval = TimeSpan.FromMilliseconds(5000);
            _timer.Start();
        }

        protected void HideWarning()
        {
            if(_currentPopup == null || !IsWarningShown)
            {
                return;
            }

            _currentPopup.IsOpen = false;
            _currentPopup.PlacementTarget = null;
            _timer.Stop();
            _timer = null;
            IsWarningShown = false;
            _currentPopup = null;
        }

        #endregion
    }
}
