using System;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

using UI_TestProjekt.Helper;

namespace UI_TestProjekt
{
    [TestClass]
    public class UnlockToolTests : TestBase
    {
        //Pfad oder AppId UWP der zu Testenden Anwendung
        private new static readonly string WpfAppId = TestConfiguration.UnlockToolPath();

        //WinAppDriver Prozess
        //private static Process winappProcess = null;

        //Sessions für Login und QST-Hauptfenster
        private static WindowsDriver<WindowsElement> unlockSession;

        private static readonly StringBuilder output = new StringBuilder();

        [TestInitialize]
        public void LoginAndInitializeSessions()
        {
            string absoluteWpfAppId = Directory.GetCurrentDirectory() + WpfAppId;

            //verschiedene Optionen setzen hier nur Pfad zur Anwendung
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", WpfAppId); 
            appiumOptions.AddAdditionalCapability("app", absoluteWpfAppId);
            appiumOptions.AddAdditionalCapability("platformName", "Windows");
            appiumOptions.AddAdditionalCapability("platformVersion", "10");

            //Session für Unlock-Tool starten
            QstSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);

            Thread.Sleep(500);
        }

        [TestMethod]
        [TestCategory("UITests_UnlockTool")]
        public void TestBtnLoadUnlockRequest()
        {
            var btnLoadUnlockRequest = QstSession.FindElementByAccessibilityId(AiStringHelper.UnlockTool.LoadUnlockRequest);
            var btnGenerateUnlockResponse = QstSession.FindElementByAccessibilityId(AiStringHelper.UnlockTool.GenerateUnlockResponse);
            var btnLoadUnlockResponse = QstSession.FindElementByAccessibilityId(AiStringHelper.UnlockTool.LoadUnlockResponse);
            Assert.IsNotNull(btnLoadUnlockRequest);
            Assert.IsNotNull(btnGenerateUnlockResponse);
            Assert.IsNotNull(btnLoadUnlockResponse);
            //Klick auf Laden Unlockrequest
            btnLoadUnlockRequest.Click();

            var closeButton = DesktSession.FindElementByXPath(
            "/Pane[@ClassName=\"#32769\"][@Name=\"Desktop 1\"]/Window[@ClassName=\"Window\"][@Name=\"MainWindow\"]/Window[@ClassName=\"#32770\"]/TitleBar[@AutomationId=\"TitleBar\"]/Button[@AutomationId=\"Close\"]");

            Assert.IsNotNull(closeButton);
            closeButton.Click();
        }
    }
}