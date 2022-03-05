using FrameworksAndDrivers.Gui.Wpf.View.Themes;
using State;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FrameworksAndDrivers.Gui.Wpf.View.Controls
{
    /// <summary>
    /// Image that converts the containing picture for the current Theme (for DarkMode: black icon -> white)
    /// </summary>
    public class ThemeImage : Image
    {
        #region Properties
        /// <summary>
        /// Prevents that the image changes the color with the DarkMode
        /// </summary>
        public bool SwitchBlackWhiteWithDarkMode { get; set; } = true;
        #endregion

        public ThemeImage()
        {
            // Set default values
            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.HighQuality);

            // Add Event-Handler
            this.Loaded += This_Loaded;
            Unloaded += ThemeImage_Unloaded;
        }

        #region Methods
        /// <summary>
        /// Change drak/black colors to bright ones
        /// </summary>
        private BitmapSource BlackToWhite(BitmapSource source)
        {
            // Calculate stride of source
            int stride = (source.PixelWidth * source.Format.BitsPerPixel + 7) / 8;

            // Create data array to hold source pixel data
            int length = stride * source.PixelHeight;
            byte[] data = new byte[length];

            // Copy source image pixels to the data array
            source.CopyPixels(data, stride, 0);
            
            for (int i = 0; i < length; i += 4)
            {
                // Inverts all colors where R/G/B <= 50 (= black or dark gray)
                if (data[i] <= 50 && data[i + 1] <= 50 && data[i + 2] <= 50)
                {
                    data[i]     = (byte)(255 - data[i]);     //R
                    data[i + 1] = (byte)(255 - data[i + 1]); //G
                    data[i + 2] = (byte)(255 - data[i + 2]); //B 
                }
            }

            // Create a new BitmapSource from the inverted pixel buffer
            return BitmapSource.Create(
                source.PixelWidth, source.PixelHeight,
                source.DpiX, source.DpiY, source.Format,
                null, data, stride);
        }

        /// <summary>
        /// Change bright/white colors to dark ones
        /// </summary>
        private BitmapSource WhiteToBlack(BitmapSource source)
        {
            // Calculate stride of source
            int stride = (source.PixelWidth * source.Format.BitsPerPixel + 7) / 8;

            // Create data array to hold source pixel data
            int length = stride * source.PixelHeight;
            byte[] data = new byte[length];

            // Copy source image pixels to the data array
            source.CopyPixels(data, stride, 0);
            
            for (int i = 0; i < length; i += 4)
            {
                // Inverts all colors where R/G/B >= 205 (= white or very light gray)
                if (data[i] >= 205 && data[i + 1] >= 205 && data[i + 2] >= 205)
                {
                    data[i]     = (byte)(255 - data[i]);     //R
                    data[i + 1] = (byte)(255 - data[i + 1]); //G
                    data[i + 2] = (byte)(255 - data[i + 2]); //B 
                }
            }

            // Create a new BitmapSource from the inverted pixel buffer
            return BitmapSource.Create(
                source.PixelWidth, source.PixelHeight,
                source.DpiX, source.DpiY, source.Format,
                null, data, stride);
        }
        #endregion


        #region Event-Handler
        private void ThemeDictionary_ThemeChanged(object sender, Theme e)
        {
            // Execute only if the image should switch color with the darkmode
            if(!SwitchBlackWhiteWithDarkMode)
            {
                return;
            }

            switch(e)
            {
                case Theme.Normal:
                    this.Source = WhiteToBlack(this.Source as BitmapSource);
                    break;
                case Theme.Dark:
                    this.Source = BlackToWhite(this.Source as BitmapSource);
                    break;
            }
        }

        private void This_Loaded(object sender, RoutedEventArgs e)
        {
            if (SwitchBlackWhiteWithDarkMode)
            {
                return;
            }

            var themeDictionary = Application.Current.Resources[ResourceKeys.ApplicationThemeDictionaryKey] as ThemeDictionary;
            if (themeDictionary == null)
            {
                return;
            }
            themeDictionary.ThemeChanged += ThemeDictionary_ThemeChanged;

            // Switch colors per default if Theme is DarkMode
            if (themeDictionary.CurrentTheme == Theme.Dark)
            {
                this.Source = BlackToWhite(this.Source as BitmapSource);
            }
        }

        private void ThemeImage_Unloaded(object sender, RoutedEventArgs e)
        {
            var themeDictionary = Application.Current.Resources[ResourceKeys.ApplicationThemeDictionaryKey] as ThemeDictionary;
            if (themeDictionary == null)
            {
                return;
            }
            themeDictionary.ThemeChanged -= ThemeDictionary_ThemeChanged;
            Unloaded -= ThemeImage_Unloaded;
        }
        #endregion
    }
}
