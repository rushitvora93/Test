using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using UI_TestProjekt.Helper;

namespace UI_TestProjekt
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für Login
    /// </summary>
    [TestClass]
    public class Login : TestBase
    {
        [TestMethod]
        [TestCategory("Login")]
        public void TestLanguage()
        {
            LoginSession = GetLoginSession(false);
            var loginWindow = TestHelper.FindElementWithWait(AiStringHelper.Login.Window, LoginSession);
            var language = TestHelper.FindElementByAiWithWaitFromParent(loginWindow, AiStringHelper.Login.Language, LoginSession);
            TestHelper.ClickComboBoxEntry(LoginSession, ValidationStringHelper.GeneralValidationStrings.LanguageGer, language);
            var txtServer = TestHelper.FindElementWithWait(AiStringHelper.Login.TxtServer, LoginSession);
            Assert.AreEqual(ValidationStringHelper.LoginValidationStrings.txtServerGer, txtServer.Text, "Falscher Text wird angezeigt");

            //var titleBar = loginWindow.FindElementByName("Titlebar");
            var close = TestHelper.TryFindElementBy("Schließen", loginWindow, AiStringHelper.By.Name);
            //Fallback falls Englisch
            if (close == null)
            {
                close = TestHelper.TryFindElementBy("Close", loginWindow, AiStringHelper.By.Name);
            }
            close.Click();

            LoginSession = GetLoginSession(false);
            loginWindow = TestHelper.FindElementWithWait(AiStringHelper.Login.Window, LoginSession);
            language = TestHelper.FindElementByAiWithWaitFromParent(loginWindow, AiStringHelper.Login.Language, LoginSession);
            txtServer = TestHelper.FindElementWithWait(AiStringHelper.Login.TxtServer, LoginSession);
            Assert.AreEqual(ValidationStringHelper.GeneralValidationStrings.LanguageGerShort, language.Text, "Falsche Sprache ausgewählt");
            Assert.AreEqual(ValidationStringHelper.LoginValidationStrings.txtServerGer, txtServer.Text, "Falscher Text wird angezeigt");

            TestHelper.ClickComboBoxEntry(LoginSession, ValidationStringHelper.GeneralValidationStrings.LanguageEn, language);
            Assert.AreEqual(ValidationStringHelper.LoginValidationStrings.txtServerEn, txtServer.Text, "Falscher Text wird angezeigt");
            close = TestHelper.TryFindElementBy("Schließen", loginWindow, AiStringHelper.By.Name);
            //Fallback falls Englisch
            if (close == null)
            {
                close = TestHelper.TryFindElementBy("Close", loginWindow, AiStringHelper.By.Name);
            }
            close.Click();

            LoginSession = GetLoginSession(false);
            loginWindow = TestHelper.FindElementWithWait(AiStringHelper.Login.Window, LoginSession);
            language = TestHelper.FindElementByAiWithWaitFromParent(loginWindow, AiStringHelper.Login.Language, LoginSession);
            Assert.AreEqual(ValidationStringHelper.GeneralValidationStrings.LanguageEnShort, language.Text, "Falsche Sprache ausgewählt");
            txtServer = TestHelper.FindElementWithWait(AiStringHelper.Login.TxtServer, LoginSession);
            Assert.AreEqual(ValidationStringHelper.LoginValidationStrings.txtServerEn, txtServer.Text, "Falscher Text wird angezeigt");
        }

        [TestMethod]
        [TestCategory("Login")]
        public void TestLanguageQST()
        {
            LoginAsCSP();
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            var language = TestHelper.FindElementWithWait(AiStringHelper.GlobalToolbar.Language, QstSession);
            TestHelper.ClickComboBoxEntry(LoginSession, ValidationStringHelper.GeneralValidationStrings.LanguageGer, language);
            var settings = TestHelper.FindElementWithWait(AiStringHelper.GlobalToolbar.Settings, QstSession);
            settings.Click();
            var aboutQST = TestHelper.FindElementWithWait(AiStringHelper.GlobalToolbar.AboutQST, QstSession);
            Assert.AreEqual(ValidationStringHelper.GlobalToolBarValidationStrings.AboutQSTGer, aboutQST.Text, "Falscher Text wird angezeigt");
            LogoutAndClearSessions();

            LoginAsCSP();
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            language = TestHelper.FindElementWithWait(AiStringHelper.GlobalToolbar.Language, QstSession);
            Assert.AreEqual(ValidationStringHelper.GeneralValidationStrings.LanguageGerShort, language.Text, "Falsche Sprache ausgewählt");

            settings = TestHelper.FindElementWithWait(AiStringHelper.GlobalToolbar.Settings, QstSession);
            settings.Click();
            aboutQST = TestHelper.FindElementWithWait(AiStringHelper.GlobalToolbar.AboutQST, QstSession);
            Assert.AreEqual(ValidationStringHelper.GlobalToolBarValidationStrings.AboutQSTGer, aboutQST.Text, "Falscher Text wird angezeigt");

            TestHelper.ClickComboBoxEntry(LoginSession, ValidationStringHelper.GeneralValidationStrings.LanguageEn, language);
            settings.Click();
            Assert.AreEqual(ValidationStringHelper.GlobalToolBarValidationStrings.AboutQSTEn, aboutQST.Text, "Falscher Text wird angezeigt");
            LogoutAndClearSessions();

            LoginAsCSP();
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            language = TestHelper.FindElementWithWait(AiStringHelper.GlobalToolbar.Language, QstSession);
            Assert.AreEqual(ValidationStringHelper.GeneralValidationStrings.LanguageEnShort, language.Text, "Falsche Sprache ausgewählt");

            settings = TestHelper.FindElementWithWait(AiStringHelper.GlobalToolbar.Settings, QstSession);
            settings.Click();
            aboutQST = TestHelper.FindElementWithWait(AiStringHelper.GlobalToolbar.AboutQST, QstSession);
            Assert.AreEqual(ValidationStringHelper.GlobalToolBarValidationStrings.AboutQSTEn, aboutQST.Text, "Falscher Text wird angezeigt");
        }

        [TestMethod]
        [TestCategory("Login")]
        public void TestPinMainMenu()
        {
            LoginAsCSP();
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            AppiumWebElement expandedButton;
            var pinButton = TestHelper.TryFindElementBy(AiStringHelper.MegaMainSubmodule.PinButton, QstSession);
            if (pinButton == null)
            {
                expandedButton = TestHelper.TryFindElementBy(AiStringHelper.MegaMainSubmodule.ExpandedButton, QstSession);
                Assert.IsNull(expandedButton, "Baum ist aufgeklappt aber Pin-Button wurde nicht gefunden");

                var expandButton = TestHelper.TryFindElementBy(AiStringHelper.MegaMainSubmodule.ExpandButton, QstSession);
                expandButton.Click();
                pinButton = TestHelper.FindElementWithWait(AiStringHelper.MegaMainSubmodule.PinButton, QstSession);
                Assert.IsNotNull(pinButton, "Nach Aufklappen wurde der Pin-Button immer noch nicht gefunden");
            }
            var pinButtonStatus = pinButton.GetAttribute("Name");
            
            //anpinnen
            if(pinButtonStatus == "False")
            {
                pinButton.Click();
            }
            expandedButton = TestHelper.TryFindElementBy(AiStringHelper.MegaMainSubmodule.ExpandedButton, QstSession);
            Assert.IsFalse(expandedButton.Enabled);
            
            var logout = QstSession.FindElementByAccessibilityId(AiStringHelper.GlobalToolbar.LogOut);
            logout.Click();
            LoginAsCSP(true);
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            pinButton = TestHelper.TryFindElementBy(AiStringHelper.MegaMainSubmodule.PinButton, QstSession);
            pinButtonStatus = pinButton.GetAttribute("Name");
            Assert.AreEqual("True", pinButtonStatus);

            expandedButton = TestHelper.TryFindElementBy(AiStringHelper.MegaMainSubmodule.ExpandedButton, QstSession);
            Assert.IsFalse(expandedButton.Enabled);

            pinButton.Click();
            pinButtonStatus = pinButton.GetAttribute("Name");
            Assert.AreEqual("False", pinButtonStatus);

            expandedButton = TestHelper.TryFindElementBy(AiStringHelper.MegaMainSubmodule.ExpandedButton, QstSession);
            Assert.IsTrue(expandedButton.Enabled);

            logout = QstSession.FindElementByAccessibilityId(AiStringHelper.GlobalToolbar.LogOut);
            logout.Click();
            LoginAsCSP(true);
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            pinButton = TestHelper.TryFindElementBy(AiStringHelper.MegaMainSubmodule.PinButton, QstSession);
            pinButtonStatus = pinButton.GetAttribute("Name");
            Assert.AreEqual("False", pinButtonStatus);

            expandedButton = TestHelper.TryFindElementBy(AiStringHelper.MegaMainSubmodule.ExpandedButton, QstSession);
            Assert.IsTrue(expandedButton.Enabled);
        }
    }
}