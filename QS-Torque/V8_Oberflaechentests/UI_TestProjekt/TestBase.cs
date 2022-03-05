using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

using UI_TestProjekt.Helper;

namespace UI_TestProjekt
{
    [TestClass]
    public abstract class TestBase
    {
        // IP:Port auf dem der WinAppDriver läuft
        protected string WindowsApplicationDriverUrl = TestConfiguration.GetWindowsApplicationDriverUrl();

        //Pfad oder AppId UWP der zu Testenden Anwendung
        protected string WpfAppId = TestConfiguration.WpfAppId();

        //Pfad zum Working directory
        protected string WorkingDir = TestConfiguration.WorkingDir();

        //relativer Pfad zum WinAppDriver
        protected string RelWinAppDriverPath = TestConfiguration.RelWinAppDriverPath();

        protected static Process WinappProcess { get; set; }
        protected static WindowsDriver<WindowsElement> LoginSession { get; set; }
        protected static WindowsDriver<WindowsElement> QstSession { get; set; }
        protected static WindowsDriver<WindowsElement> DesktSession { get; set; }

        //protected static DesktopSession desktopSession;
        private readonly StringBuilder output = new StringBuilder();

        protected static readonly string testLogPath = $"C:\\temp\\QSTV8_GUITestFehlerScreenshots\\TestLog_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log";

        protected static readonly CultureInfo currentCulture = CultureInfo.InvariantCulture;
        protected static readonly string numberFormatThreeDecimals = "0.000";
        protected static readonly string numberFormatNoDecimals = "0";

        private static readonly bool showWinAppDriverWindow = false;

        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void Setup(TestContext context)
        {
            /*var config = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false)
            .Build();
            TestConfiguration.WindowsApplicationDriverUrl = (string)config.GetValue(typeof(string), "WindowsApplicationDriverUrl");
            RelWinAppDriverPath = (string)config.GetValue(typeof(string), "RelWinAppDriverPath");
            WpfAppId = (string)config.GetValue(typeof(string), "StartUpPath");
            WorkingDir = (string)config.GetValue(typeof(string), "WorkingDir");
            */
            TestHelper.TestLogPath = testLogPath;
            //winappProcess = TestHelper.StartWinAppDriver(RelWinAppDriverPath, output);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            //TestHelper.StopWinAppDriver(winappProcess, RelWinAppDriverPath, output);
        }


        [TestInitialize]
        public void InitializeDesktSession()
        {
            File.AppendAllText(testLogPath, string.Format("{0}: Start Testfall:{1}{2}", DateTime.Now.ToString(), TestContext.TestName, Environment.NewLine));

            //DesktopSession erstellen
            if (DesktSession == null)
            {
                DesktSession = TestHelper.CreateNewDesktSession(WindowsApplicationDriverUrl);
            }
            Assert.IsNotNull(DesktSession, "Desktop not found!");
        }

        [TestCleanup]
        public void LogoutAndClearSessions()
        {
            File.AppendAllText(testLogPath, string.Format("{0}: Ende Testfall:{1}{2}", DateTime.Now.ToString(), TestContext.TestName, Environment.NewLine));
            
            if (QstSession != null)
            {
                if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
                {
                    MakeScreenshots(QstSession);
                }

                try
                {
                    var logoutBtn = QstSession.FindElementByAccessibilityId(AiStringHelper.GlobalToolbar.LogOut);
                    if (logoutBtn != null)
                    {
                        logoutBtn.Click();
                    }
                }
                catch (WebDriverException)
                {
                    //WebDriverException fangen
                }
            }
            Thread.Sleep(500);

            TestHelper.CloseSessions(LoginSession);
            if (QstSession != null)
            {
                TestHelper.CloseSessions(QstSession);
            }

            //Möglicherweise noch offene QST-Fenster killen um nachfolgende Tests nicht zu behindern
            Process[] QSTStartups = Process.GetProcessesByName("StartUp");
            foreach (var QSTStartup in QSTStartups)
            {
                QSTStartup.Kill();
            }
            
            Thread.Sleep(1000);
            File.AppendAllText(testLogPath, string.Format("{0}: Ende Testfall:{1} nach Sleep{2}", DateTime.Now.ToString(), TestContext.TestName, Environment.NewLine));
        }
        public TestContext TestContext { get; set; }        

        private void MakeScreenshots(WindowsDriver<WindowsElement> qstSession)
        {
            var screenshotPath = $"C:\\temp\\QSTV8_GUITestFehlerScreenshots\\{TestContext.TestName}_QST_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
            TestHelper.MakeScreenshots(qstSession, screenshotPath);

            var screenshotPathDesktop = $"C:\\temp\\QSTV8_GUITestFehlerScreenshots\\{TestContext.TestName}_Desktop_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
            TestHelper.MakeScreenshots(DesktSession, screenshotPathDesktop);
        }        

        protected static void ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames menu, AppiumWebElement globalTree)
        {
            string menuAi = "";
            string treeViewItem = "";
            switch (menu)
            {
                case AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData: 
                    menuAi = AiStringHelper.MegaMainSubmodule.MasterData;
                    treeViewItem = AiStringHelper.MegaMainSubmodule.TreeViewItemMasterData;
                    break;
                default: 
                    Assert.IsTrue(false, "Menüpunkt noch nicht implementiert"); 
                    break;
            }
            var expandButton = TestHelper.TryFindElementBy(AiStringHelper.MegaMainSubmodule.ExpandButton, globalTree);

            if (expandButton != null && expandButton.Enabled && expandButton.Displayed)
            {
                var menuButton = globalTree.FindElementByAccessibilityId(menuAi);

                Assert.IsNotNull(menuButton, "ExpandMainMenuButton ist null");
                Assert.IsTrue(menuButton.Displayed, string.Format("Gesuchter ExpandMainMenuButton ist nicht sichtbar: {0}", menu));

                if (menuButton.Displayed)
                {
                    menuButton.Click();
                }
            }
            else
            {
                var treeViewItemMasterData = globalTree.FindElementByAccessibilityId(treeViewItem);
                var masterDataExpander = treeViewItemMasterData.FindElementByAccessibilityId("Expander");
                if (masterDataExpander.GetAttribute("Toggle.ToggleState") == "0")
                {
                    treeViewItemMasterData.Click();
                }
            }
        }
        protected static void NavigateToMainMenu(WindowsDriver<WindowsElement> qstSession, string aiMenuBtn)
        {
            switch (aiMenuBtn)
            {
                case AiStringHelper.MegaMainSubmodule.MeasurementPoint:
                    NavigateToMainMenu(qstSession, AiStringHelper.MegaMainSubmodule.MeasurementPointContainer, aiMenuBtn); break;
                case AiStringHelper.MegaMainSubmodule.ToolModel:
                    NavigateToMainMenu(qstSession, AiStringHelper.MegaMainSubmodule.ToolModelContainer, aiMenuBtn); break;
                case AiStringHelper.MegaMainSubmodule.Tool:
                    NavigateToMainMenu(qstSession, AiStringHelper.MegaMainSubmodule.ToolContainer, aiMenuBtn); break;
                case AiStringHelper.MegaMainSubmodule.MpToolAllocation:
                    NavigateToMainMenu(qstSession, AiStringHelper.MegaMainSubmodule.MpToolAllocationContainer, aiMenuBtn); break;
                case AiStringHelper.MegaMainSubmodule.TestEquipment:
                    NavigateToMainMenu(qstSession, AiStringHelper.MegaMainSubmodule.TestEquipmentContainer, aiMenuBtn); break;
                case AiStringHelper.MegaMainSubmodule.TestPlanningMasterData:
                    NavigateToMainMenu(qstSession, AiStringHelper.MegaMainSubmodule.TestPlanningMasterDataContainer, aiMenuBtn); break;
                case AiStringHelper.MegaMainSubmodule.ProcessControl:
                    NavigateToMainMenu(qstSession, AiStringHelper.MegaMainSubmodule.ProcessControlContainer, aiMenuBtn); break;
                default: Assert.IsTrue(false, "String für Menübutton noch nicht implementiert"); break;
            }
        }
        protected static void NavigateToMainMenu(WindowsDriver<WindowsElement> qstSession, string aiContainer, string aiMenuBtn)
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", "Root");
            var test = new WindowsDriver<WindowsElement>(new Uri(TestConfiguration.GetWindowsApplicationDriverUrl()), appiumOptions, TimeSpan.FromMilliseconds(3000));
            var assistantView = TestHelper.TryFindElementByAccessabilityId(AiStringHelper.Assistant.View, test);
            Assert.IsNull(assistantView, "AssistentFenster ist beim Seitenwechsel noch geöffnet");

            AppiumWebElement globalTree = TestHelper.FindElementWithWait(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector, qstSession);
            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);

            //Timeout damit die Eintrags-Container gefunden werden, unklar warum beim Buildserver withWait nicht greift
            Thread.Sleep(500);
            var Container = TestHelper.FindElementByAiWithWaitFromParent(globalTree, aiContainer, qstSession);
            var menuBtn = TestHelper.FindElementByAiWithWaitFromParent(globalTree, aiMenuBtn, qstSession);

            Assert.IsNotNull(Container, string.Format("Container: {0} wurde nicht gefunden", aiContainer));
            Assert.IsNotNull(menuBtn, string.Format("menuBtn: {0} wurde nicht gefunden", aiMenuBtn));

            bool needsExtraKlickOnBtn = Container.Size.Width > 2 * menuBtn.Size.Width;
            Container.Click();
            //Klick auf Container wird benötigt damit auf den Button gescrollt wird
            //Da der eigentliche Button allerdings kleiner ist kann es sein dass dieser nicht getroffen wird und nochmal extra gedrückt werden muss
            if (needsExtraKlickOnBtn)
            {
                menuBtn.Click();
            }
        }
        protected static void NavigateToAuxiliarySubmenu(WindowsDriver<WindowsElement> driver, WindowsElement globalTree, AppiumWebElement auxiliaryButton, string submenu)
        {
            bool submenuButtonFound = false;
            AppiumWebElement submenuTreeitem = null;

            try
            {
                submenuTreeitem = globalTree.FindElementByAccessibilityId(submenu);
            }
            catch (WebDriverException)
            {
                //WebDriverException fangen
            }
            if (submenuTreeitem != null && submenuTreeitem.Enabled)
            {
                //erster Klick um Element in Sichtbereich zu bringen (automatisches Scrollen auf Element funktioniert nur bei Click nicht bei Action.Click
                submenuTreeitem.Click();

                //kleiner Offset da er bei kurzen Menüpunkttexten wie z.B. Status sonst neben den Text klickt und das Menü nicht aufruft
                Actions actions = new Actions(driver);
                actions.MoveToElement(submenuTreeitem, 60, 5);
                actions.Click();
                actions.Build();
                actions.Perform();
                submenuButtonFound = true;
            }
            else
            {
                auxiliaryButton.Click();
                try
                {
                    submenuTreeitem = globalTree.FindElementByAccessibilityId(submenu);
                }
                catch (Exception)
                {
                    throw;
                }
                if (submenuTreeitem != null && submenuTreeitem.Enabled)
                {
                    //erster Klick um Element in Sichtbereich zu bringen (automatisches Scrollen auf Element funktioniert nur bei Click nicht bei Action.Click
                    submenuTreeitem.Click();

                    //kleiner Offset da er bei kurzen Menüpunkttexten wie z.B. Status sonst neben den Text klickt und das Menü nicht aufruft
                    Actions actions = new Actions(driver);
                    actions.MoveToElement(submenuTreeitem, 60, 5);
                    actions.Click();
                    actions.Build();
                    actions.Perform();
                    submenuButtonFound = true;
                }
            }
            Assert.IsNotNull(auxiliaryButton, "AuxiliaryButton ist null");
            Assert.IsTrue(submenuButtonFound, string.Format("Button für Untermenü nicht gefunden: {0}", submenu));
            Thread.Sleep(200);
        }
        protected static void NavigateToAuxiliarySubmenu(WindowsDriver<WindowsElement> session, WindowsElement globalTree, string aiSubmenu)
        {
            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);
            var auxiliaryButton = globalTree.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.AuxiliaryMasterData);
            NavigateToAuxiliarySubmenu(session, globalTree, auxiliaryButton, aiSubmenu);
        }
        protected static void LoginAsCSP(bool relogin = false)
        {
            string cspUser = "csp";
            string sMonth = DateTime.Now.ToString("%M");
            string cspPw = sMonth + "TSQ";
            Login(cspUser, cspPw, relogin);
        }
        protected static void LoginAsQST(bool relogin = false)
        {
            string qstUser = "qst";
            string qstPw = "QST";
            Login(qstUser, qstPw, relogin);
        }
        private static void Login(string user, string pw, bool relogin = false)
        {
            LoginSession = GetLoginSession(relogin);
            Assert.IsNotNull(LoginSession, "LoginWindow not found!");

            //evtl. mehr Zeit geben abhängig wie schnell server reagiert
            Thread.Sleep(500);
            var server = LoginSession.FindElementByAccessibilityId(AiStringHelper.Login.Server);
            var userName = LoginSession.FindElementByAccessibilityId(AiStringHelper.Login.Username);
            var password = LoginSession.FindElementByAccessibilityId(AiStringHelper.Login.Password);
            var logIn = LoginSession.FindElementByAccessibilityId(AiStringHelper.Login.BtnLogIn);

            Assert.IsNotNull(server, "Server-Dropdown nicht gefunden");
            Assert.IsNotNull(userName, "Eingabefeld für den Benutzernamen nicht gefunden");
            Assert.IsNotNull(password, "Eingabefeld für das Passwort nicht gefunden");
            Assert.IsNotNull(logIn, "Login-Button nicht gefunden");

            //Auf Serverauswahl klicken um zu sehen ob Interaktion mit QST funktioniert und Serverauswahl wieder schließen
            server.Click();
            Thread.Sleep(1000);
            Actions actions = new Actions(LoginSession);
            actions.SendKeys(Keys.Escape);
            actions.Build();
            actions.Perform();

            //TODO Server auswählen
            //TestHelper.ClickComboBoxEntry("Testserver", server);

            //mit CSP Einloggen
            TestHelper.SendKeysConverted(userName, user);
            TestHelper.SendKeysConverted(password, pw);
            Thread.Sleep(800);
            logIn.Click();

            //Session für Hauptfenster erstellen
            
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());
            //WaitForPresence(mainQstWindowSession, 5, AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);
            var contentPresenter = QstSession.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);

            Assert.IsTrue(contentPresenter != null && contentPresenter.Displayed);
        }

        protected static WindowsDriver<WindowsElement> GetLoginSession(bool relogin)
        {
            WindowsDriver<WindowsElement> localLoginSession;
            if (!relogin)
            {
                string absoluteWpfAppId = Directory.GetCurrentDirectory() + TestConfiguration.WpfAppId();
                //string absoluteWpfAppId = WpfAppId;

                string absoluteWorkingDirectory = Directory.GetCurrentDirectory() + TestConfiguration.WorkingDir();
                //string absoluteWorkingDirectory = Path.GetDirectoryName(absoluteWpfAppId); // Alternativ Working Directory aus AppPfad auslesen

                //verschiedene Optionen setzen
                var appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", absoluteWpfAppId);
                appiumOptions.AddAdditionalCapability("appWorkingDir", absoluteWorkingDirectory); // Setzen damit Übersetzung gefunden wird
                appiumOptions.AddAdditionalCapability("platformName", "Windows");
                appiumOptions.AddAdditionalCapability("platformVersion", "10");

                //Testweise ausprobieren wegen "Failed to locate opened application window"
                //Maximum für ms:waitForAppLaunch ist 50 sekunden!!!
                appiumOptions.AddAdditionalCapability("ms:waitForAppLaunch", "15");
                //appiumOptions.AddAdditionalCapability("fullReset", true);
                //appiumOptions.AddAdditionalCapability("forcequit", "true");

                //Session für Login-Fenster starten
                localLoginSession = new WindowsDriver<WindowsElement>(new Uri(TestConfiguration.GetWindowsApplicationDriverUrl()), appiumOptions);
            }
            else
            {
                localLoginSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.Login.Window, TestConfiguration.GetWindowsApplicationDriverUrl());
            }

            return localLoginSession;
        }

        protected static string GetValueFromAssistantList(WindowsDriver<WindowsElement> assistantSession, string fieldName)
        {
            var listInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.NavigationList, assistantSession);
            var value = listInput.FindElementByXPath(string.Format("*/*[@ClassName=\"ListViewItem\"]/*[@ClassName=\"ContentPresenter\"]/*[@ClassName=\"TextBlock\"][@Name=\"{0}\"]/../following-sibling::Custom[1]/*[@ClassName=\"TextBlock\"]", fieldName));
            //TODO Trim Entfernen 
            return value.Text.Trim();
        }
        protected static void AssertAssistantListEntry(WindowsDriver<WindowsElement> assistantSession, string expectedValue, string fieldName, string unit = "", bool isCheckbox = false)
        {
            string currentValue = GetValueFromAssistantList(assistantSession, fieldName);
            if (!string.IsNullOrWhiteSpace(unit))
            {
                Assert.AreEqual(expectedValue + " " + unit, currentValue);
            }
            /*if (fieldName == AssistantStringHelper.MpStrings.SetPointTorque ||
                fieldName == AssistantStringHelper.MpStrings.MinimumTorque ||
                fieldName == AssistantStringHelper.MpStrings.MaximumTorque ||
                fieldName == AssistantStringHelper.MpStrings.ThresholdTorque)
            {
            }
            else if(fieldName == AssistantStringHelper.MpStrings.SetPointAngle ||
                fieldName == AssistantStringHelper.MpStrings.MinimumAngle ||
                fieldName == AssistantStringHelper.MpStrings.MaximumAngle)
            {
                Assert.AreEqual(expectedValue + " °", currentValue);
            }
            */
            else if (isCheckbox)
            {
                Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(bool.Parse(expectedValue)), currentValue);
            }
            else
            {
                Assert.AreEqual(expectedValue, currentValue);
            }
        }
        //Schließt das offene Fenster und validiert die Nachricht
        //Der gewünschte Schließbutton kann mit übergeben werden standardmäßig ist es der OkBtn
        protected static void CheckAndCloseValidationWindow(WindowsDriver<WindowsElement> driver, string expectedMessage, string closeButtonAi = AiStringHelper.GeneralStrings.OkBtn)
        {
            Thread.Sleep(500);
            var messageWindow = driver.FindElementByClassName("#32770");
            Assert.IsNotNull(messageWindow, "MessageWindow nicht gefunden");
            var message = messageWindow.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.MessageText);
            Assert.AreEqual(expectedMessage, message.Text);
            var image = messageWindow.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.Image);
            Assert.IsNotNull(image, "Icon nicht gefunden / Falsches Icon");
            var okButton = messageWindow.FindElementByAccessibilityId(closeButtonAi);
            okButton.Click();
        }

        //Startet den WinAppDriver innerhalb des Tests falls benötigt
        public static Process StartWinAppDriver(string relWinAppDriverPath, StringBuilder output)
        {
            string absolutePathToWinappdriver = Directory.GetCurrentDirectory() + relWinAppDriverPath;

            //info.RedirectStandardError = true;
            Process localWinappProcess = new Process();
            localWinappProcess.StartInfo.FileName = absolutePathToWinappdriver;

            if (showWinAppDriverWindow)
            {
                // UseShellExecute wird benötigt für die Anzeige bei false läuft windowsdriver im hintergrund
                // Muss false sein damit man den StandardOutput umlenken kann
                localWinappProcess.StartInfo.UseShellExecute = true;
            }
            else
            {
                localWinappProcess.StartInfo.UseShellExecute = false;
                localWinappProcess.StartInfo.RedirectStandardOutput = true;
                localWinappProcess.StartInfo.RedirectStandardInput = true;
                //Umleitung der Ausgabe in Logfile falls winappdriver über Test gestartet wird
                localWinappProcess.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    //NUL entfernen
                    string line = e.Data.Replace("\x00", "");
                    if (!string.IsNullOrEmpty(line))
                    {
                        output.AppendLine(line);
                    }
                });
            }


            //WinAppDriver starten
            localWinappProcess.Start();

            if (!showWinAppDriverWindow)
            {
                // Asynchronously read the standard output of the spawned process. 
                // This raises OutputDataReceived events for each line of output.
                localWinappProcess.BeginOutputReadLine();
            }

            return localWinappProcess;
        }

        //Stoppt den WinAppDriver innerhalb des Tests falls benötigt
        public static void StopWinAppDriver(Process winappProcess, string relWinAppDriverPath, StringBuilder output)
        {
            // WinAppDriver schließen
            if (winappProcess != null)
            {
                if (!showWinAppDriverWindow)
                {
                    // Write the redirected output -> LOG
                    File.WriteAllText(Path.GetDirectoryName(Directory.GetCurrentDirectory() + relWinAppDriverPath) + @"/wad.log", output.ToString());
                }
                winappProcess.CloseMainWindow();
                //winappProcess.WaitForExit();
                //winappProcess.CloseMainWindow();
                winappProcess.Close();
                winappProcess.Dispose();
                //winappProcess.Kill();
            }
        }
    }
}