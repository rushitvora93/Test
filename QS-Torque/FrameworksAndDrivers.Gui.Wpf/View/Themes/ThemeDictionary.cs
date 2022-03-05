using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.View.Themes
{
    /// <summary>
    /// ResourceDicitionary, that contains the Resources for the current Theme (Colors, Brushes)
    /// </summary>
    public class ThemeDictionary : ResourceDictionary
    {
        #region Properties
        private readonly Dictionary<Theme, Uri> _themeUris;
        public Theme CurrentTheme;
        #endregion
        
        #region Events
        public event EventHandler<Theme> ThemeChanged;
        #endregion


        #region Methods
        public void ChangeTheme(Theme theme)
        {
            if(CurrentTheme == theme)
            {
                return;
            }

            CurrentTheme = theme;
            LoadDictionaryForTheme(theme);
            ThemeChanged?.Invoke(this, theme);
        }

        private void LoadDictionaryForTheme(Theme theme)
        {
            // Wire to the ResourceDictionaries with the Resources for the new Theme
            this.Source = _themeUris[theme];
        }
        #endregion


        public ThemeDictionary(Dictionary<Theme, Uri> themeUris)
        {
            _themeUris = themeUris;
            
            if (themeUris.Count > 0)
            {
                LoadDictionaryForTheme(_themeUris.First().Key); 
            }
        }

        [Obsolete("This Constructor should not be used, but it is used to create an instance of ThemeDictionary in the DesignTimeResources")]
        public ThemeDictionary() { }
    }
}
