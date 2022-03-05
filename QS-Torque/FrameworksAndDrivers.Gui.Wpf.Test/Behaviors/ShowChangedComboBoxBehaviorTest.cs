using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using FrameworksAndDrivers.Gui.Wpf.View.Themes;
using NUnit.Framework;
using State;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Behaviors
{
    class ShowChangedComboBoxBehaviorTest
    {
        private ClearShownChangesMock _clearShownChanges;
        private ShowChangedComboBoxBehavior _behavior;
        private ComboBox _comboBox;
        private ThemeDictionary _themeDictionary;

        private const string DefaultText = "hu8t549r0epodlvc,mk";


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

            _comboBox = new ComboBox();
            _comboBox.Items.Add("bhztu7epodölv,bnjhzut");
            _comboBox.Items.Add("bnghturiodlfcv,mbnhgut");
            _comboBox.SelectedIndex = 0;

            _behavior = new ShowChangedComboBoxBehavior();
            _behavior.ClearShownChangesParent = _clearShownChanges;
            _behavior.Attach(_comboBox);
        }


        [Test, RequiresThread(ApartmentState.STA)]
        public void OnCreateTest()
        {
            Assert.IsNull(_comboBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowNotChangedTextBoxOnTextChangedTest()
        {
            _comboBox.SelectedIndex = 1;
            _comboBox.SelectedIndex = 0;

            Assert.IsNull(_comboBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowChangedTextBoxOnTextChangedTest()
        {
            _comboBox.SelectedIndex = 1;

            Assert.AreEqual(_themeDictionary[ResourceKeys.ChangedFieldBrushKey], _comboBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowNotChangedTextBoxOnThemeChangedTest()
        {
            _comboBox.SelectedIndex = 1;
            _comboBox.SelectedIndex = 0;

            _themeDictionary.ChangeTheme(Theme.Dark);

            Assert.IsNull(_comboBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowChangedTextBoxOnThemeChangedTest()
        {
            _comboBox.SelectedIndex = 1;

            _themeDictionary.ChangeTheme(Theme.Dark);

            Assert.AreEqual(_themeDictionary[ResourceKeys.ChangedFieldBrushKey], _comboBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ClearShownChangesAfterNoChangeTest()
        {
            _comboBox.SelectedIndex = 1;
            _comboBox.SelectedIndex = 0;

            _clearShownChanges.InvokeClearShownChanges();

            Assert.IsNull(_comboBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ClearShownChangesAfterChangeTest()
        {
            _comboBox.SelectedIndex = 1;

            _clearShownChanges.InvokeClearShownChanges();

            Assert.IsNull(_comboBox.BorderBrush);
        }
    }
}
