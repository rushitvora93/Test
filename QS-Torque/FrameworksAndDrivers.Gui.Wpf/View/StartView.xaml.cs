using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView : UserControl
    {
        public StartView()
        {
            InitializeComponent();

            var logo = new Image()
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Resources;component/Images/QST_Logo.png"))
            };

            // fancy fade in :D
            var storyboard = new Storyboard();
            var fadeInAnimation = new DoubleAnimation() {From = 0.0, To = 1.0, Duration = TimeSpan.FromSeconds(2.0), EasingFunction = new QuadraticEase{EasingMode = EasingMode.EaseIn}};
            Storyboard.SetTarget(fadeInAnimation, logo);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(fadeInAnimation);
            //Resources.Add("Storyboard", storyboard);
            logo.Loaded += (sender, args) => storyboard.Begin();
            Content = logo;
        }
    }
}
