using FrameworksAndDrivers.Gui.Wpf.View.Themes;
using System.Windows.Controls;
using System.Windows.Media;
using State;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public class ShowRequieredFieldBase<T> : BehaviorBase<T> where T : Control
    {
        private const string RequiredFieldBrushKey = "RequiredFieldBrush";
        private const string DefaultRequiredFieldColor = "#FFFF0000";
        
        #region Properties
        private Brush _requiredBorderBrush;
        #endregion


        #region Methods
        protected void ShowControlAsRequired()
        {
            if (this.AssociatedObject == null)
            {
                return;
            }

            this.AssociatedObject.BorderBrush = _requiredBorderBrush;
        }

        protected void ShowControlAsNormal()
        {
            if (this.AssociatedObject == null || this.AssociatedObject.BorderBrush != _requiredBorderBrush)
            {
                return;
            }

            this.AssociatedObject.BorderBrush = null;
        }

        protected override void OnSetup()
        {
            base.OnSetup();
            (Application.Current.Resources[ResourceKeys.ApplicationThemeDictionaryKey] as ThemeDictionary).ThemeChanged += ThemeDictionaryOnThemeChanged;
        }

        protected override void OnCleanup()
        {
            (Application.Current.Resources[ResourceKeys.ApplicationThemeDictionaryKey] as ThemeDictionary).ThemeChanged -= ThemeDictionaryOnThemeChanged;
            base.OnCleanup();
        }

        #endregion


        #region Event-Handler
        private void ThemeDictionaryOnThemeChanged(object sender, Theme e)
        {
            var themeDictionary = sender as ThemeDictionary;
            _requiredBorderBrush = themeDictionary[RequiredFieldBrushKey] as Brush ?? new SolidColorBrush((Color)ColorConverter.ConvertFromString(DefaultRequiredFieldColor));
        }
        #endregion


        public ShowRequieredFieldBase()
        {
            // Get default Border
            var themeDictionary = Application.Current.Resources[ResourceKeys.ApplicationThemeDictionaryKey] as ThemeDictionary;
            _requiredBorderBrush = themeDictionary[RequiredFieldBrushKey] as Brush ?? new SolidColorBrush((Color)ColorConverter.ConvertFromString(DefaultRequiredFieldColor));
        }
    }
}
