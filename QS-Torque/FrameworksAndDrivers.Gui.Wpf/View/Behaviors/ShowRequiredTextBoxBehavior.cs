using FrameworksAndDrivers.Gui.Wpf.View.Themes;
using State;
using System.Windows;
using System.Windows.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public class ShowRequiredTextBoxBehavior : ShowRequieredFieldBase<TextBox>
    {
        protected override void OnSetup()
        {
            base.OnSetup();
            this.AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;
            this.AssociatedObject.Unloaded += AssociatedObjectOnUnloaded;
            (Application.Current.Resources[ResourceKeys.ApplicationThemeDictionaryKey] as ThemeDictionary).ThemeChanged += ThemeDictionaryOnThemeChanged;
        }

        protected override void OnCleanup()
        {
            this.AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
            this.AssociatedObject.Unloaded -= AssociatedObjectOnUnloaded;
            (Application.Current.Resources[ResourceKeys.ApplicationThemeDictionaryKey] as ThemeDictionary).ThemeChanged -= ThemeDictionaryOnThemeChanged;
            base.OnCleanup();
        }

        private void ThemeDictionaryOnThemeChanged(object sender, Theme e)
        {
            CheckIfTextIsRequired();
        }

        private void AssociatedObjectOnTextChanged(object sender, TextChangedEventArgs e)
        {
            CheckIfTextIsRequired();
        }

        private void AssociatedObjectOnUnloaded(object sender, RoutedEventArgs e)
        {
            OnDetaching();
        }

        private void CheckIfTextIsRequired()
        {
            if (!this.AssociatedObject.IsEnabled)
            {
                ShowControlAsNormal();
                return;
            }

            if (this.AssociatedObject.Text == string.Empty || this.AssociatedObject.Text == null)
            {
                ShowControlAsRequired();
            }
            else
            {
                ShowControlAsNormal();
            }
        }
    }
}
