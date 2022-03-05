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
    class ShowNormalComboBoxBehaviorTest
    {
        private ClearShownChangesMock _clearShownChanges;
        private ShowNormalComboBoxBehavior _behavior;
        private ComboBox _comboBox;
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

            _comboBox = new ComboBox() { BorderBrush = _defaultBorder };
            _comboBox.Items.Add("gfdjköl");
            _comboBox.Items.Add("njuhipol");
            _comboBox.SelectedIndex = 0;

            _behavior = new ShowNormalComboBoxBehavior() { NormalBorderBrush = _defaultBorder };
            _behavior.ClearShownChangesParent = _clearShownChanges;
            _behavior.Attach(_comboBox);
        }


        [Test, RequiresThread(ApartmentState.STA)]
        public void OnCreateTest()
        {
            Assert.AreEqual(_defaultBorder, _comboBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowNotNormalTextBoxOnSelectionChangedTest()
        {
            var brush = new SolidColorBrush(Colors.Tomato);
            _comboBox.BorderBrush = brush;

            _comboBox.SelectedIndex = 1;

            Assert.AreEqual(brush, _comboBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowNormalTextBoxOnSelectionChangedTest()
        {
            _comboBox.BorderBrush = null;

            _comboBox.SelectedIndex = 1;

            Assert.AreEqual(_defaultBorder, _comboBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowNotNormalTextBoxOnThemeChangedTest()
        {
            var brush = new SolidColorBrush(Colors.Tomato);
            _comboBox.BorderBrush = brush;

            _themeDictionary.ChangeTheme(Theme.Dark);

            Assert.AreEqual(brush, _comboBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowNormalTextBoxOnThemeChangedTest()
        {
            _comboBox.BorderBrush = null;

            _themeDictionary.ChangeTheme(Theme.Dark);

            Assert.AreEqual(_defaultBorder, _comboBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowNotNormalTextBoxOnClearShownChangesTest()
        {
            var brush = new SolidColorBrush(Colors.Tomato);
            _comboBox.BorderBrush = brush;

            _clearShownChanges.InvokeClearShownChanges();

            Assert.AreEqual(brush, _comboBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowNormalTextBoxOnClearShownChangesTest()
        {
            _comboBox.BorderBrush = null;

            _clearShownChanges.InvokeClearShownChanges();

            Assert.AreEqual(_defaultBorder, _comboBox.BorderBrush);
        }
    }
}
