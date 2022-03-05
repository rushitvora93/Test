using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using FrameworksAndDrivers.Gui.Wpf.View.Themes;
using NUnit.Framework;
using State;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Behaviors
{
    class ShowRequiredTextBoxBehaviorTest
    {
        private ShowRequiredTextBoxBehavior _behavior;
        private TextBox _textBox;
        private ThemeDictionary _themeDictionary;
        

        [SetUp]
        public void ShowRequiredTextBoxBehaviorSetUp()
        {
            BehaviorSetUpFixture.TestApplication.Resources.Remove(ResourceKeys.ApplicationThemeDictionaryKey);

            var themeUris = new Dictionary<Theme, Uri>
            {
                { Theme.Normal, new Uri("/FrameworksAndDrivers.Gui.Wpf;component/View/Themes/QstColors.xaml", UriKind.RelativeOrAbsolute) },
                { Theme.Dark, new Uri("/FrameworksAndDrivers.Gui.Wpf;component/View/Themes/DarkModeColors.xaml", UriKind.RelativeOrAbsolute) }
            };
            _themeDictionary = new ThemeDictionary(themeUris);
            BehaviorSetUpFixture.TestApplication.Resources.Add(ResourceKeys.ApplicationThemeDictionaryKey, _themeDictionary);

            _textBox = new TextBox();
            _behavior = new ShowRequiredTextBoxBehavior();
            _behavior.Attach(_textBox);
        }

        
        [Test, RequiresThread(ApartmentState.STA)]
        public void OnCreateTest()
        {
            Assert.IsNull(_textBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void TextBoxNotRequiredTest()
        {
            _textBox.Text = "hfdsjküplfkogji";

            Assert.IsNull(_textBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void TextBoxRequiredTest()
        {
            _textBox.Text = "hfdsjküplfkogji";
            _textBox.Text = "";

            Assert.AreEqual(_themeDictionary[ResourceKeys.RequiredFieldBrushKey], _textBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void DisabledTextBoxNotRequiredTest()
        {
            _textBox.IsEnabled = false;
            _textBox.Text = "hfdsjküplfkogji";

            Assert.IsNull(_textBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void DisabledTextBoxRequiredTest()
        {
            _textBox.IsEnabled = false;
            _textBox.Text = "hfdsjküplfkogji";
            _textBox.Text = "";

            Assert.IsNull(_textBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ThemeChangedTextBoxRequiredTest()
        {
            _themeDictionary.ChangeTheme(Theme.Dark);
            _textBox.Text = "hfdsjküplfkogji";
            _textBox.Text = "";

            Assert.AreEqual(_themeDictionary[ResourceKeys.RequiredFieldBrushKey] as SolidColorBrush, _textBox.BorderBrush as SolidColorBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ThemeChangedTextBoxNotRequiredTest()
        {
            _themeDictionary.ChangeTheme(Theme.Dark);
            _textBox.Text = "hfdsjküplfkogji";

            Assert.IsNull(_textBox.BorderBrush);
        }
    }
}
