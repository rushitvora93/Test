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
    class ShowChangedTextBoxBehaviorTest
    {
        private ClearShownChangesMock _clearShownChanges;
        private ShowChangedTextBoxBehavior _behavior;
        private TextBox _textBox;
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
            _textBox = new TextBox() { Text = DefaultText };
            _behavior = new ShowChangedTextBoxBehavior();
            _behavior.ClearShownChangesParent = _clearShownChanges;
            _behavior.Attach(_textBox);
        }


        [Test, RequiresThread(ApartmentState.STA)]
        public void OnCreateTest()
        {
            Assert.IsNull(_textBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowNotChangedTextBoxOnTextChangedTest()
        {
            _textBox.Text = "bfhsdjkaöl";
            _textBox.Text = DefaultText;

            Assert.IsNull(_textBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowChangedTextBoxOnTextChangedTest()
        {
            _textBox.Text = "bfhsdjkaöl";

            Assert.AreEqual(_themeDictionary[ResourceKeys.ChangedFieldBrushKey], _textBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowNotChangedTextBoxOnThemeChangedTest()
        {
            _textBox.Text = "sdfgjmk,.ktz";
            _textBox.Text = DefaultText;

            _themeDictionary.ChangeTheme(Theme.Dark);

            Assert.IsNull(_textBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowChangedTextBoxOnThemeChangedTest()
        {
            _textBox.Text = "09zu7ghfjdk";

            _themeDictionary.ChangeTheme(Theme.Dark);

            Assert.AreEqual(_themeDictionary[ResourceKeys.ChangedFieldBrushKey], _textBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ClearShownChangesAfterNoChangeTest()
        {
            _textBox.Text = "sdfgjmk,.ktz";
            _textBox.Text = DefaultText;

            _clearShownChanges.InvokeClearShownChanges();

            Assert.IsNull(_textBox.BorderBrush);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ClearShownChangesAfterChangeTest()
        {
            _textBox.Text = "96i3kelfpfdwqtz";

            _clearShownChanges.InvokeClearShownChanges();

            Assert.IsNull(_textBox.BorderBrush);
        }
    }
}
