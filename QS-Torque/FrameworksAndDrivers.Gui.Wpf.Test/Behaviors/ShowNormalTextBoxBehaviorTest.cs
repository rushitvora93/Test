using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using FrameworksAndDrivers.Gui.Wpf.View.Themes;
using NUnit.Framework;
using State;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Behaviors
{
    class ShowNormalTextBoxBehaviorTest
    {
        private ClearShownChangesMock _clearShownChanges;
        private ShowNormalTextBoxBehavior _behavior;
        private TextBox _textBox;
        private Brush _defaultBorder;
        private ThemeDictionary _themeDictionary;


        [SetUp]
        public void ShowNormalTextBoxBehaviorSetUp()
        {
            BehaviorSetUpFixture.TestApplication.Resources.Remove(ResourceKeys.ApplicationThemeDictionaryKey);

            var themeUris = new Dictionary<Theme, Uri>
            {
                { Theme.Normal, new Uri("/FrameworksAndDrivers.Gui.Wpf;component/View/Themes/QstColors.xaml", UriKind.RelativeOrAbsolute) },
                { Theme.Dark, new Uri("/FrameworksAndDrivers.Gui.Wpf;component/View/Themes/DarkModeColors.xaml", UriKind.RelativeOrAbsolute) }
            };
            _themeDictionary = new ThemeDictionary(themeUris);
            BehaviorSetUpFixture.TestApplication.Resources.Add(ResourceKeys.ApplicationThemeDictionaryKey, _themeDictionary);

            _clearShownChanges = new ClearShownChangesMock();
            _defaultBorder = new SolidColorBrush(Colors.Bisque);
            _textBox = new TextBox() { BorderBrush = _defaultBorder };
            _behavior = new ShowNormalTextBoxBehavior() { NormalBorderBrush = _defaultBorder };
            _behavior.ClearShownChangesParent = _clearShownChanges;
            _behavior.Attach(_textBox);
        }


        [Test, RequiresThread(ApartmentState.STA)]
        public void OnCreateTest()
        {
            Assert.AreEqual(_defaultBorder, _textBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowNotNormalTextBoxOnTextChangedTest()
        {
            var brush = new SolidColorBrush(Colors.Tomato);
            _textBox.BorderBrush = brush;

            _textBox.Text = "bfhsdjkaöl";

            Assert.AreEqual(brush, _textBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowNormalTextBoxOnTextChangedTest()
        {
            _textBox.BorderBrush = null;

            _textBox.Text = "bfhsdjkaöl";

            Assert.AreEqual(_defaultBorder, _textBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowNotNormalTextBoxOnThemeChangedTest()
        {
            var brush = new SolidColorBrush(Colors.Tomato);
            _textBox.BorderBrush = brush;

            _themeDictionary.ChangeTheme(Theme.Dark);

            Assert.AreEqual(brush, _textBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowNormalTextBoxOnThemeChangedTest()
        {
            _textBox.BorderBrush = null;

            _themeDictionary.ChangeTheme(Theme.Dark);

            Assert.AreEqual(_defaultBorder, _textBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowNotNormalTextBoxOnClearShownChangesTest()
        {
            var brush = new SolidColorBrush(Colors.Tomato);
            _textBox.BorderBrush = brush;

            _clearShownChanges.InvokeClearShownChanges();

            Assert.AreEqual(brush, _textBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowNormalTextBoxOnClearShownChangesTest()
        {
            _textBox.BorderBrush = null;

            _clearShownChanges.InvokeClearShownChanges();

            Assert.AreEqual(_defaultBorder, _textBox.BorderBrush);
        }
    }
}
