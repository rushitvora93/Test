using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace UI_TestProjekt.Helper
{
    public static class TestHelper
    {
        public static string TestLogPath { get; set; } = $"C:\\temp\\QSTV8_GUITestFehlerScreenshots\\TestLog.log";

        public static void CloseSessions(WindowsDriver<WindowsElement> session)
        {
            // Switch to any window handle returned in the list
            // session.SwitchTo().Window(allWindowHandles[0]);
            if (session != null)
            {
                //alle Sessions schließen
                foreach (var windowHandle in session.WindowHandles)
                {
                    try
                    {
                        session.SwitchTo().Window(windowHandle);
                        session.CloseApp();
                        session.Quit();
                    }
                    catch (WebDriverException e)
                    {
                        if (e.Message == "No active session with ID window" ||
                            e.Message == "No active session with ID window_handles")
                        {
                            // Bei den ExceptionsMessages nichts machen
                        }
                        else { throw; }
                    }
                }
            }
        }

        /* Slash und Backslash werden mit dieser Methode nicht konvertiert, stattdessen "SendKeysWithBackslash" verwenden
         * Bei Sendkeys wird aktuell nur die US-Tastatur unterstützt
         * Bei Eingabe mit deutschter Tastatur werden Auszugsweise folgende Zeichen ersetzt
         * "z" -> "y";
         * "y" -> "z";
         * "/" -> "-"; 
         * "=" -> "?"; 
         * "?" -> "´"; 
         * "`" -> "^";
         * "'" -> "ä";
         * ";" -> "ö";
         * "[" -> "ü";
         * "-" -> "ß";
         * "\" -> "#",
         * "]" -> "+";
         * 
         * "°!§%&/()=?`*'_:;ÄÖÜ^1234567890ß´+#äö-.,üµ~\}][{²³<>|#  /*-+" -> "°!§%&-()?´^*ä_:öÄÖÜ^1234567890ß´+#äöß.,üµ~#}+ü{²³<>|#  -*ß+"
         * 
         * Workaround für z.B. Slash: String slash = Keys.Alt + Keys.NumberPad4 + Keys.NumberPad7 + Keys.Alt;
         * Workaround für z.B. BackSlash: String slash = Keys.Alt + Keys.NumberPad9 + Keys.NumberPad2 + Keys.Alt;
         * 
         * Methode ersetzt aktuell y und z und -
         * Bekannte nicht unterstützte Zeichen sind aktuell "=", "'" "`", ";", "]", "["
         * Außer durch Workaround über ALT siehe Slash, Backslash
         */
        private static string GetStringforSendKeys(string sendString)
        {
            if (sendString == null)
            {
                return " ";
            }
            return sendString
                .Replace("y", "~~~~~°")
                .Replace("z", "y")
                .Replace("-", "/")
                .Replace("~~~~~°", "z");
        }

        //Sendet Strings auch mit Backslash(\) und Slash(/)
        //Wenn onlyBackslash = true ersetzt nur Backslash(\) und nicht Slash(/)
        public static void SendKeysWithBackslash(WindowsDriver<WindowsElement> session, AppiumWebElement elementToSend, string sendString, bool onlyBackslash = false)
        {
            //Nur senden wenn string vorhanden ist da sonst actions.SendKeys Fehler wirft: "The key value must not be null or empty (Parameter 'keysToSend')"
            if (string.IsNullOrEmpty(sendString))
            {
                return;
            }
            string splitString = @"/";
            if (onlyBackslash)
            {
                splitString = @"\";
            }
            List<string> sendStrings = new List<string>(sendString.Split(splitString));
            int i = 0;
            foreach (var item in sendStrings)
            {
                Actions actions = new Actions(session);
                if (onlyBackslash)
                {
                    actions.SendKeys(elementToSend, GetStringforSendKeys(item));
                }
                else
                {
                    SendKeysWithBackslash(session, elementToSend, item, true);
                }
                if ((i + 1) < sendStrings.Count)
                {
                    actions.KeyDown(Keys.Alt);
                    if (onlyBackslash)
                    {
                        actions.SendKeys(Keys.NumberPad9);
                        actions.SendKeys(Keys.NumberPad2);
                    }
                    else
                    {
                        actions.SendKeys(Keys.NumberPad4);
                        actions.SendKeys(Keys.NumberPad7);
                    }
                    actions.KeyUp(Keys.Alt);
                }
                actions.Build().Perform();
                i++;
            }
        }

        public static void SendKeysConverted(AppiumWebElement element, string sendString)
        {
            string convertedString = GetStringforSendKeys(sendString);
            int maxStringSize = 100;
            int stringLength = convertedString.Length;

            //Logik für sehr lange Strings (100+), da es sonst bei der Eingabe zu Timeouts kommen kann
            for (int i = 0; i < stringLength; i += maxStringSize)
            {
                if (i + maxStringSize > stringLength)
                {
                    maxStringSize = stringLength - i;
                }
                element.SendKeys(convertedString.Substring(i, maxStringSize));
            }
        }

        //Obsolet
        public static string GetConvertedString(string sendString)
        {
            return sendString
                .Replace("y", "~~~~~°")
                .Replace("-", "ß")
                .Replace("/", "-")
                .Replace("?", "´")
                .Replace("=", "?")
                .Replace("`", "^")
                .Replace("'", "ä")
                .Replace(";", "ö")
                .Replace(@"\", "#")
                .Replace("]", "+")
                .Replace("[", "ü")
                .Replace("z", "y")
                .Replace("~~~~~°", "z");
        }

        /// <summary>
        /// Try to findElement if not found return null
        /// </summary>
        /// <param name="accessabilityId"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static AppiumWebElement TryFindElementBy(string accessabilityId, AppiumWebElement parent, AiStringHelper.By by = AiStringHelper.By.AI)
        {
            AppiumWebElement element = null;
            if (parent == null)
            {
                return element;
            }
            try
            {
                switch (by)
                {
                    case AiStringHelper.By.AI: element = parent.FindElementByAccessibilityId(accessabilityId); break;
                    case AiStringHelper.By.Class: element = parent.FindElementByClassName(accessabilityId); break;
                    case AiStringHelper.By.Name: element = parent.FindElementByName(accessabilityId); break;
                    case AiStringHelper.By.XPath: element = parent.FindElementByXPath(accessabilityId); break;
                }
            }
            catch (WebDriverException e)
            {
                if (e.Message == "An element could not be located on the page using the given search parameters." &&
                    e.Source == "WebDriver")
                {
                    return null;
                }
            }
            return element;
        }
        /// <summary>
        /// Try to findElement if not found return null
        /// </summary>
        /// <param name="accessabilityId"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static AppiumWebElement TryFindElementBy(string accessabilityId, WindowsDriver<WindowsElement> driver, AiStringHelper.By by = AiStringHelper.By.AI)
        {
            AppiumWebElement element = null;
            if (driver == null)
            {
                return element;
            }
            try
            {
                switch (by)
                {
                    case AiStringHelper.By.AI: element = driver.FindElementByAccessibilityId(accessabilityId); break;
                    case AiStringHelper.By.Name: element = driver.FindElementByName(accessabilityId); break;
                    case AiStringHelper.By.XPath: element = driver.FindElementByXPath(accessabilityId); break;
                }
            }
            catch (WebDriverException e)
            {
                if (e.Message == "An element could not be located on the page using the given search parameters." &&
                    e.Source == "WebDriver")
                {
                    return null;
                }
            }
            return element;
        }

        /// <summary>
        /// Try to findElement if not found return null
        /// </summary>
        /// <param name="accessabilityId"></param>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static AppiumWebElement TryFindElementByAccessabilityId(string accessabilityId, WindowsDriver<WindowsElement> driver)
        {
            AppiumWebElement element = null;
            if (driver == null)
            {
                return element;
            }
            try
            {
                element = driver.FindElementByAccessibilityId(accessabilityId);
            }
            catch (WebDriverException e)
            {
                if (e.Message == "An element could not be located on the page using the given search parameters." &&
                    e.Source == "WebDriver")
                {
                    return null;
                }
            }
            return element;
        }

        /// <summary>
        /// Get a new Session for Window with given AutomationId
        /// </summary>
        /// <param name="accessabilityId"></param>
        /// <returns>The new Session for the Window</returns>
        public static WindowsDriver<WindowsElement> GetWindowSession(WindowsDriver<WindowsElement> desktSession, string accessabilityId, string windowsApplicationDriverUrl, int timeoutInMilliSeconds = 10000)
        {
            //desktopSession.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //var window = WaitForPresence(desktopSession, 10000, accessabilityId);
            WindowsElement window =
                FindElementOnDesktopByAiWithWait(desktSession, accessabilityId);
            //WaitForElementExistsIsEnabledIsVisibleByAutomationId(desktSession, accessabilityId, 5000, 500);
            //WaitForElementExistsIsEnabledIsVisibleByAutomationIds(desktSession, accessabilityId, 5000, 500);

            var windowTopLevelWindowHandle = window.GetAttribute("NativeWindowHandle");
            windowTopLevelWindowHandle = (int.Parse(windowTopLevelWindowHandle)).ToString("x"); // Convert to Hex

            // Create session by attaching to top level window
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("appTopLevelWindow", windowTopLevelWindowHandle);
            WindowsDriver<WindowsElement> windowSession = new WindowsDriver<WindowsElement>(new Uri(windowsApplicationDriverUrl), appiumOptions, TimeSpan.FromMilliseconds(timeoutInMilliSeconds));
            return windowSession;
        }

        public static WindowsElement WaitForPresenceByXpath(WindowsDriver<WindowsElement> driver, int timeLimitInSeconds, String xPath)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeLimitInSeconds));
            WindowsElement elementToBeDisplayed = null;
            Boolean found = wait.Until<Boolean>(condition =>
            {
                try
                {
                    elementToBeDisplayed = driver.FindElementByXPath(xPath);
                    if (elementToBeDisplayed != null)
                    {
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            });
            if (found)
            {
                return elementToBeDisplayed;
            }
            return elementToBeDisplayed;
        }

        public static WindowsDriver<WindowsElement> CreateNewDesktSession(string windowsApplicationDriverUrl)
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", "Root");
            //appiumOptions.AddAdditionalCapability("ms:experimental-webdriver", true);
            return new WindowsDriver<WindowsElement>(new Uri(windowsApplicationDriverUrl), appiumOptions, TimeSpan.FromMilliseconds(60000));
        }


        public static AppiumWebElement FindElementByAbsoluteXPathWithWait(WindowsDriver<WindowsElement> desktSession, string xPath, int nTryCount = 10)
        {
            WindowsElement uiTarget = null;
            while (nTryCount-- > 0)
            {
                try
                {
                    uiTarget = WaitForPresenceByXpath(desktSession, 5, xPath);
                }
                catch
                {
                    //catch Exception when Element is not found and then Retry
                }

                if (uiTarget != null)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(200);
                }
            }
            return uiTarget;
        }

        public static WindowsElement FindElementOnDesktopByAiWithWait(WindowsDriver<WindowsElement> desktSession, string automationId, int nTryCount = 5)
        {
            return FindElementWithWait(automationId, desktSession, nTryCount);
        }

        public static WindowsElement FindElementByAiWithWaitQSTShortTimeSession(WindowsDriver<WindowsElement> desktSession, string automationId, string windowsApplicationDriverUrl, int nTryCount = 3, int timeLimitInSeconds = 5, int sleepIntervalInMilliseconds = 300)
        {
            WindowsElement window =
                FindElementOnDesktopByAiWithWait(desktSession, AiStringHelper.MainWindow.Window);

            var windowTopLevelWindowHandle = window.GetAttribute("NativeWindowHandle");
            windowTopLevelWindowHandle = (int.Parse(windowTopLevelWindowHandle)).ToString("x"); // Convert to Hex
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("appTopLevelWindow", windowTopLevelWindowHandle);
            WindowsDriver<WindowsElement> shortTimeSession = new WindowsDriver<WindowsElement>(new Uri(windowsApplicationDriverUrl), appiumOptions, TimeSpan.FromMilliseconds(1000));

            return FindElementWithWait(automationId, shortTimeSession, nTryCount, timeLimitInSeconds, sleepIntervalInMilliseconds);
        }

        public static WindowsElement FindElementWithWait(string searchString, WindowsDriver<WindowsElement> driver, int nTryCount = 3, int timeLimitInSeconds = 5, int sleepIntervalInMilliseconds = 300, AiStringHelper.By by = AiStringHelper.By.AI)
        {
            WindowsElement uiTarget = null;
            while (nTryCount-- > 0)
            {
                try
                {
                    uiTarget = FindElementBy(driver, searchString, timeLimitInSeconds, sleepIntervalInMilliseconds, by);
                }
                catch
                {
                    //catch Exception when Element is not found and then Retry
                }

                if (uiTarget != null)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(200);
                }
            }

            return uiTarget;
        }

        public static AppiumWebElement FindElementByAiWithWaitFromParent(AppiumWebElement parent, string automationId, WindowsDriver<WindowsElement> driver, int nTryCount = 5, int timeLimitInSeconds = 5, int sleepIntervalInMilliseconds = 300)
        {
            AppiumWebElement uiTarget = null;
            while (nTryCount-- > 0)
            {
                try
                {
                    uiTarget = FindElementByAiFromParent(parent, automationId, driver, timeLimitInSeconds, sleepIntervalInMilliseconds);
                }
                catch
                {
                    //catch Exception when Element is not found and then Retry
                }

                if (uiTarget != null)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(200);
                }
            }

            return uiTarget;
        }

        //Find Element from Driver with Wait
        public static WindowsElement FindElementBy(WindowsDriver<WindowsElement> driver, string searchString, int timeLimitInSeconds = 5, int sleepIntervalInMilliseconds = 300, AiStringHelper.By by = AiStringHelper.By.AI)
        {
            SystemClock clock = new SystemClock();
            WebDriverWait wait = new WebDriverWait(clock, driver, TimeSpan.FromSeconds(timeLimitInSeconds), TimeSpan.FromMilliseconds(sleepIntervalInMilliseconds));
            WindowsElement elementToBeDisplayed = null;
            Boolean found = wait.Until<Boolean>(condition =>
            {
                try
                {
                    switch (by)
                    {
                        case AiStringHelper.By.AI: elementToBeDisplayed = driver.FindElementByAccessibilityId(searchString); break;
                        case AiStringHelper.By.Name: elementToBeDisplayed = driver.FindElementByName(searchString); break;
                    }

                    if (elementToBeDisplayed != null)
                    {
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            });
            if (found)
            {
                return elementToBeDisplayed;
            }
            return elementToBeDisplayed;
        }

        //Find Element from ParentElement with Wait
        public static AppiumWebElement FindElementByAiFromParent(AppiumWebElement parent, string automationId, WindowsDriver<WindowsElement> driver, int timeLimitInSeconds, int sleepIntervalInMilliseconds)
        {
            SystemClock clock = new SystemClock();
            WebDriverWait wait = new WebDriverWait(clock, driver, TimeSpan.FromSeconds(timeLimitInSeconds), TimeSpan.FromMilliseconds(sleepIntervalInMilliseconds));
            AppiumWebElement elementToBeDisplayed = null;
            Boolean found = wait.Until<Boolean>(condition =>
            {
                try
                {
                    elementToBeDisplayed = parent.FindElementByAccessibilityId(automationId);
                    if (elementToBeDisplayed != null)
                    {
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            });
            if (found)
            {
                return elementToBeDisplayed;
            }
            return elementToBeDisplayed;
        }

        public static bool WaitForElementToBeEnabledAndDisplayed(WindowsDriver<WindowsElement> driver, int timeLimitInSeconds, int sleepIntervalInMilliseconds, AppiumWebElement elementToBeDisplayed)
        {
            SystemClock clock = new SystemClock();
            WebDriverWait wait = new WebDriverWait(clock, driver, TimeSpan.FromSeconds(timeLimitInSeconds), TimeSpan.FromMilliseconds(sleepIntervalInMilliseconds));
            bool displayed = false;
            try
            {
                displayed = wait.Until<Boolean>(condition =>
                {
                    if (elementToBeDisplayed != null)
                    {
                        if (elementToBeDisplayed.Enabled && elementToBeDisplayed.Displayed)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            return displayed;
        }

        public static bool WaitForElementToBeEnabledAndDisplayed(WindowsDriver<WindowsElement> driver, AppiumWebElement elementToBeDisplayed)
        {
            return WaitForElementToBeEnabledAndDisplayed(driver, 5, 300, elementToBeDisplayed);
        }

        public static bool WaitForElementToBeDisplayed(WindowsDriver<WindowsElement> driver, AppiumWebElement elementToBeDisplayed, int timeLimitInSeconds = 5, int sleepIntervalInMilliseconds = 300)
        {
            SystemClock clock = new SystemClock();
            WebDriverWait wait = new WebDriverWait(clock, driver, TimeSpan.FromSeconds(timeLimitInSeconds), TimeSpan.FromMilliseconds(sleepIntervalInMilliseconds));
            bool displayed = false;
            try
            {
                displayed = wait.Until<Boolean>(condition =>
                {
                    if (elementToBeDisplayed != null)
                    {
                        if (elementToBeDisplayed.Displayed)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            return displayed;
        }

        public static bool WaitForElementToggleToBeOpened(WindowsDriver<WindowsElement> driver, AppiumWebElement elementToBeToggleOpened, int timeLimitInSeconds = 5, int sleepIntervalInMilliseconds = 300)
        {
            SystemClock clock = new SystemClock();
            WebDriverWait wait = new WebDriverWait(clock, driver, TimeSpan.FromSeconds(timeLimitInSeconds), TimeSpan.FromMilliseconds(sleepIntervalInMilliseconds));
            Boolean toggleOpened = wait.Until<Boolean>(condition =>
            {
                if (elementToBeToggleOpened != null)
                {
                    if (elementToBeToggleOpened.GetAttribute("Toggle.ToggleState") == "1")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            });
            return toggleOpened;
        }
        public static void MakeScreenshots(WindowsDriver<WindowsElement> session, string screenshotPath)
        {
            //evtl kann man den Window Status irgendwie abfragen
            //bis dahin TryCatch
            try
            {
                session.GetScreenshot().SaveAsFile(screenshotPath);
            }
            catch (Exception) 
            {
                //Es kann passieren dass QST vorher geschlossen wurde und dadurch z.B. die Exception fliegt "Currently selected window has been closed"
            }
            /*catch (WebDriverException e)
            {
                if(e.Message!="Currently selected window has been closed")
                {
                    throw;
                }
            }*/
        }
        public static void ClickWithWait(AppiumWebElement elementToClick, WindowsDriver<WindowsElement> driver)
        {
            WaitForElementToBeEnabledAndDisplayed(driver, 3, 300, elementToClick);
            elementToClick.Click();
        }

        // Sucht das gewählte Element in Listbox und berücksichtigt evtl. vorhandenen vertikalen Scrollbalken
        public static AppiumWebElement FindElementInListbox(string accessabilityId, AppiumWebElement listbox, AiStringHelper.By by = AiStringHelper.By.AI)
        {
            string scrollable = listbox.GetAttribute("Scroll.VerticallyScrollable");
            AppiumWebElement searchedElement = TryFindElementBy(accessabilityId, listbox, by);
            if ((searchedElement == null || searchedElement.Location.Y > listbox.Size.Height) && scrollable == true.ToString())
            {
                listbox.SendKeys(Keys.Home);
                string scrollpercent = listbox.GetAttribute("Scroll.VerticalScrollPercent");
                while (scrollpercent != 100.ToString() && (searchedElement == null || searchedElement.Location.Y > listbox.Size.Height))
                {
                    var items = listbox.FindElementsByClassName("ListBoxItem");
                    items[items.Count - 1].Click();
                    listbox.SendKeys(Keys.PageDown);
                    scrollpercent = listbox.GetAttribute("Scroll.VerticalScrollPercent");
                    searchedElement = TryFindElementBy(accessabilityId, listbox);
                }
            }
            return searchedElement;
        }

        public static void AssertGridRow(WindowsDriver<WindowsElement> qstSession, AppiumWebElement parent, string aiGridString, string aiGridHeaderString, string aiGridRowPrefix, string identString, string headerText, string expectedValue, bool isCheckbox = false)
        {
            Assert.IsNotNull(parent, string.Format("Grid-Parent:{0} not found", parent));
            var grid = FindElementByAiWithWaitFromParent(parent, aiGridString, qstSession);
            Assert.IsNotNull(grid, string.Format("Grid:{0} not found", aiGridString));
            var listViewScrollAreaLeft = TryFindElementBy(AiStringHelper.GeneralStrings.ScrollViewerAreaLeft, grid);

            if (listViewScrollAreaLeft == null)
            {
                var headerrowItems = grid.FindElementsByClassName("GridHeaderCellControl");
                int i = 0;
                bool found = false;
                foreach (var item in headerrowItems)
                {
                    if (item.FindElementByClassName("TextBlock").Text == headerText)
                    {
                        found = true;
                        break;
                    }
                    i++;
                }
                if (found)
                {
                    var givenManufacturerRow = grid.FindElementByAccessibilityId(aiGridRowPrefix + identString);

                    var givenManufacturerRowCells = givenManufacturerRow.FindElementsByClassName("GridCell");
                    Assert.IsNotNull(givenManufacturerRowCells[i].FindElementByClassName("TextBlock"), string.Format("{0} not found", headerText));
                    Assert.AreEqual(expectedValue, givenManufacturerRowCells[i].FindElementByClassName("TextBlock").Text, string.Format("{0}, Wrong Value", headerText));
                }
                else
                {
                    Assert.IsTrue(found, string.Format("Header not found:{0}", headerText));
                }
            }
            else
            {
                var headerrowItems = grid.FindElementsByAccessibilityId(aiGridHeaderString);

                var scrollBarRightArrow = grid.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.ScrollViewerRightBtn);
                var ListViewScrollAreaRight = grid.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.ScrollViewerAreaRight);
                var scrollbar = grid.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.ScrollViewerHorizontalScrollbar);

                bool found = false;
                int i;

                Stopwatch watch = new Stopwatch();
                watch.Reset();
                watch.Start();

                do
                {
                    i = 0;
                    foreach (var item in headerrowItems)
                    {
                        if (item.FindElementByClassName("TextBlock").Text == headerText)
                        {
                            found = true;
                            break;
                        }
                        i++;
                    }
                    if (found)
                    {
                        break;
                    }
                    if (ListViewScrollAreaRight.Size.Width == 0)
                    {
                        var scrollBarLeftArrow = grid.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.ScrollViewerLeftBtn);
                        Actions actions = new Actions(qstSession);
                        actions.ClickAndHold(scrollBarLeftArrow);
                        actions.Build().Perform();

                        Stopwatch watchScrollLeft = new Stopwatch();
                        watchScrollLeft.Start();

                        while (listViewScrollAreaLeft.Size.Width > 0 && watchScrollLeft.ElapsedMilliseconds < 5000)
                        {
                            Thread.Sleep(100);
                        }
                        watchScrollLeft.Stop();

                        actions = new Actions(qstSession);
                        actions.Release();
                        actions.Build().Perform();

                        Assert.AreEqual(0, listViewScrollAreaLeft.Size.Width, "Konnte nicht ganz nach links scrollen");
                    }
                    else
                    {
                        //Workaround wenn gleichzeitig noch win10 Toastnotifications aufploppen und statt dem ScrollPfeil der Toast geklickt wurde
                        int leftListViewSizeBefore = listViewScrollAreaLeft.Size.Width;
                        do
                        {
                            scrollBarRightArrow.Click();
                        }
                        while (leftListViewSizeBefore == listViewScrollAreaLeft.Size.Width);
                    }

                    headerrowItems = grid.FindElementsByClassName("GridHeaderCellControl");
                } while (!found && watch.ElapsedMilliseconds < 20000);
                watch.Stop();

                var givenRow = grid.FindElementByAccessibilityId(aiGridRowPrefix + identString);
                var givenRowCells = givenRow.FindElementsByClassName("GridCell");

                Assert.AreEqual(headerrowItems.Count, givenRowCells.Count, "HeaderRow und ChildRow haben unterschiedliche Anzahl an Elemente");

                if (!isCheckbox)
                {
                    Assert.IsNotNull(givenRowCells[i].FindElementByClassName("TextBlock"), string.Format("{0} not found", headerText));
                    Assert.AreEqual(expectedValue, givenRowCells[i].FindElementByClassName("TextBlock").Text, string.Format("{0}, Wrong Value", headerText));
                }
                else
                {
                    Assert.IsNotNull(givenRowCells[i].FindElementByClassName("CheckBox"), string.Format("{0} not found", headerText));
                    Assert.AreEqual(expectedValue, givenRowCells[i].FindElementByClassName("CheckBox").Selected.ToString(), string.Format("{0}, Wrong Value", headerText));
                }
            }
        }
        /// <summary>
        /// Wait for Text-Attribut to have the expected String
        /// </summary>
        /// <param name="element">element with text</param>
        /// <param name="text">text to check for</param>
        public static void WaitforElementTextLoaded(AppiumWebElement element, string text)
        {
            for (int i = 10; i > 0; i--)
            {
                if (element.Text == text) break;
                Thread.Sleep(200);
            }
        }
        /// <summary>
        /// Auslesen des ausgewählten Texts in einer Combobox (Workaround) speziell für 1024 x 768 Auflösung
        /// Workaround dafür, wenn Combobox zwar geladen aber immer noch außerhalb des Sichtbereichs liegt (hinter Taskleiste)
        /// </summary>
        /// <param name="comboBox"></param>
        /// <returns></returns>
        public static string GetSelectedComboboxStringWithScrolling(WindowsDriver<WindowsElement> session, AppiumWebElement comboBox, AppiumWebElement scrollViewer)
        {
            var verticalScrollbar = TryFindElementBy(AiStringHelper.GeneralStrings.ScrollViewerVerticalScrollbar, scrollViewer);
            var scrollBarDownArrow = TryFindElementBy(AiStringHelper.GeneralStrings.ScrollViewerDownBtn, verticalScrollbar);
            var ListViewScrollAreaBottom = TryFindElementBy(AiStringHelper.GeneralStrings.ScrollViewerAreaDown, verticalScrollbar);
            var ListViewScrollAreaTop = TryFindElementBy(AiStringHelper.GeneralStrings.ScrollViewerAreaUp, verticalScrollbar);

            if (verticalScrollbar != null && verticalScrollbar.Displayed && comboBox.Location.Y > 700)
            {
                do
                {
                    scrollViewer.SendKeys(Keys.PageUp);
                }
                while (ListViewScrollAreaTop.Size.Height > 0);

                do
                {
                    scrollViewer.SendKeys(Keys.PageDown);
                    //Scroll Down
                    //scrollBarDownArrow.Click();
                } while (comboBox.Location.Y > 700 && ListViewScrollAreaBottom.Size.Height > 0);
            }

            //Speziell für 1024 x 768 Auflösung
            Assert.IsFalse(comboBox.Location.Y > 700 && ListViewScrollAreaBottom.Size.Height == 0, "komplett gescrolled aber Combobox nicht gefunden");
            Thread.Sleep(200);

            return GetSelectedComboboxString(session, comboBox);
        }
        public static string GetSelectedComboboxString(WindowsDriver<WindowsElement> session, AppiumWebElement comboBox)
        {
            //ComboBox mit Klick öffnen damit die Liste in Baum geladen wird
            comboBox.SendKeys(Keys.Cancel);
            comboBox.Click();
            //bool comboDisplayed = comboBox.Displayed;
            //bool comboEnabled = comboBox.Displayed;
            //string comboAttr = comboBox.GetAttribute("Disabled");
            WindowsElement activeElement = null;
            //Das aktive Element aus der Liste finden
            if (session.SwitchTo().ActiveElement() != null)
            {
                activeElement = session.SwitchTo().ActiveElement() as WindowsElement;
            }
            else
            {
                Assert.IsNotNull(activeElement, string.Format("Active ComboboxElement von Combobox={0} is null", comboBox.Text));
            }
            //Das untergeordnete Element mit dem eigentlichen Text finden
            var activeToolModelTypeTextBlock = activeElement.FindElementByClassName("TextBlock");

            string selectedText = "";
            if (activeToolModelTypeTextBlock != null)
            {
                selectedText = activeToolModelTypeTextBlock.Text;
            }

            //ComboBox wieder schließen ohne Einstellung zu ändern
            comboBox.SendKeys(Keys.Escape);

            return selectedText;
        }

        public static void ClickComboBoxEntryWithScrolling(WindowsDriver<WindowsElement> driver, string entryString, AppiumWebElement comboBox, WindowsElement parentView, string aiScrollViewerParent, string aiScrollviewer)
        {
            var scrollViewerParent = parentView.FindElementByAccessibilityId(aiScrollViewerParent);
            var scrollViewer = scrollViewerParent.FindElementByAccessibilityId(aiScrollviewer);
            ClickComboBoxEntryWithScrolling(driver, entryString, comboBox, scrollViewer);
        }
        public static void ClickComboBoxEntryWithScrolling(WindowsDriver<WindowsElement> driver, string entryString, AppiumWebElement comboBox, AppiumWebElement scrollViewer)
        {
            var verticalScrollbar = TryFindElementBy(AiStringHelper.GeneralStrings.ScrollViewerVerticalScrollbar, scrollViewer);
            var scrollBarDownArrow = TryFindElementBy(AiStringHelper.GeneralStrings.ScrollViewerDownBtn, verticalScrollbar);
            var ListViewScrollAreaBottom = TryFindElementBy(AiStringHelper.GeneralStrings.ScrollViewerAreaDown, verticalScrollbar);
            var ListViewScrollAreaTop = TryFindElementBy(AiStringHelper.GeneralStrings.ScrollViewerAreaUp, verticalScrollbar);

            if (verticalScrollbar != null && verticalScrollbar.Displayed && comboBox.Location.Y > 700)
            {
                do
                {
                    scrollViewer.SendKeys(Keys.PageUp);
                }
                while (ListViewScrollAreaTop.Size.Height > 0);

                do
                {
                    scrollViewer.SendKeys(Keys.PageDown);
                    //Scroll Down
                    //scrollBarDownArrow.Click();
                } while (comboBox.Location.Y > 700 && ListViewScrollAreaBottom.Size.Height > 0);
            }

            Assert.IsFalse(comboBox.Location.Y > 700 && ListViewScrollAreaBottom.Size.Height == 0, "komplett gescrolled aber Combobox nicht gefunden");
            Thread.Sleep(200);

            ClickComboBoxEntry(driver, entryString, comboBox);
        }
        public static void ClickComboBoxEntry(WindowsDriver<WindowsElement> driver, string entryString, AppiumWebElement comboBox)
        {
            comboBox.SendKeys(Keys.Cancel);
            comboBox.Click();
            IReadOnlyCollection<AppiumWebElement> comboBoxItems = comboBox.FindElementsByClassName("ListBoxItem");
            foreach (AppiumWebElement item in comboBoxItems)
            {
                var textblock = item.FindElementByClassName("TextBlock");
                if (textblock != null && textblock.Text == entryString)
                {
                    WaitForElementToBeEnabledAndDisplayed(driver, item);
                    item.Click();
                    return;
                }
            }
            Assert.IsTrue(false, string.Format("ListboxEntry: \"{0}\" not found!", entryString));
        }

        public static string GetBoolValueAsVerifyString(bool boolToConvert, bool textAsActive = false)
        {
            if (textAsActive)
            {
                if (boolToConvert) { return "Active"; }
                else { return "Not active"; }
            }
            if (boolToConvert) { return "Yes"; }
            else { return "No"; }
        }
        public static void SendDate(DateTime dateTimeToSend, WindowsElement inputDate)
        {
            inputDate.SendKeys(Keys.ArrowLeft);
            inputDate.SendKeys(Keys.ArrowLeft);
            inputDate.SendKeys(Keys.ArrowLeft);
            inputDate.SendKeys(dateTimeToSend.Month.ToString("d2"));
            inputDate.SendKeys(dateTimeToSend.Day.ToString("d"));
            if (dateTimeToSend.Day < 10)
            {
                inputDate.SendKeys(Keys.Space);
            }
            inputDate.SendKeys(dateTimeToSend.Year.ToString("d4"));
        }
        //Ohne Uhrzeit
        public static string GetDateString(DateTime dateTime)
        {
            return dateTime.ToString(CultureInfo.CreateSpecificCulture("en-us").DateTimeFormat.ShortDatePattern, CultureInfo.CreateSpecificCulture("en-us"));
        }

        public static string GetTimeString(DateTime datetime)
        {
            return datetime.ToLongTimeString();
        }
        /// <summary>
        /// Liefert den Knoten anhand des durch eine StringList gelieferten Pfades zurück 
        /// </summary>
        /// <param name="qstSession"></param>
        /// <param name="folderList">Liste der BaumElemente</param>
        /// <returns></returns>
        public static AppiumWebElement GetNode(WindowsDriver<WindowsElement> qstSession, AppiumWebElement treeViewRootNode, List<string> folderList)
        {
            //TODO evtl wait einbauen
            Assert.IsNotNull(folderList, "Übergebene OrdnerListe ist leer, Fehler im Test?");

            AppiumWebElement currentNode = treeViewRootNode;
            if (treeViewRootNode != null)
            {
                if (folderList.Count == 1 && treeViewRootNode.Text == folderList[0])
                {                    
                    return currentNode;
                }
            }
            else
            {
                File.AppendAllText(TestLogPath, string.Format("{0}:treeViewRootNode = null{1}", DateTime.Now.ToString(), Environment.NewLine));
                return null;
            }

            for (int i = 0; i < folderList.Count; i++)
            {
                var treeviewRootNodeExpander = FindElementByAiWithWaitFromParent(currentNode, AiStringHelper.GeneralStrings.PartExpander, qstSession, 2, 1);
                if (treeviewRootNodeExpander == null)
                {
                    File.AppendAllText(TestLogPath, string.Format("{0}:treeviewRootNodeExpander = null{1}", DateTime.Now.ToString(), Environment.NewLine));
                    return null;
                }

                var treeviewRootNodeSubExpander = TryFindElementBy(AiStringHelper.GeneralStrings.Expander, currentNode);
                if (treeviewRootNodeSubExpander == null)
                {
                    File.AppendAllText(TestLogPath, string.Format("{0}: treeviewRootNodeSubExpander ist null{1}", DateTime.Now, Environment.NewLine));
                    return null;
                }

                WaitForElementToBeEnabledAndDisplayed(qstSession, treeviewRootNodeSubExpander);
                if (treeviewRootNodeSubExpander.GetAttribute("Toggle.ToggleState") == "0")
                {
                    treeviewRootNodeSubExpander.Click();
                    Thread.Sleep(500);
                    //treeviewRootNodeSubExpander neu auslesen weil sich der ToggleState nicht immer aktualisiert
                    treeviewRootNodeSubExpander = TryFindElementBy(AiStringHelper.GeneralStrings.Expander, currentNode);
                }
                WaitForElementToggleToBeOpened(qstSession, treeviewRootNodeSubExpander);

                ReadOnlyCollection<AppiumWebElement> treeViewItemAdvs = currentNode.FindElementsByClassName("TreeViewItemAdv");
                //ReadOnlyCollection<AppiumWebElement> treeViewItemAdvs = currentNode.FindElementsByXPath("*/*[@ClassName=\"TreeViewItemAdv\"]");

                AppiumWebElement subElementNode = null;
                foreach (var item in treeViewItemAdvs)
                {
                    subElementNode = TryFindElementBy(folderList[i], item, AiStringHelper.By.Name);
                    if (subElementNode != null)
                    {
                        currentNode = subElementNode;
                        break;
                    }
                }

                if (subElementNode == null)
                {
                    File.AppendAllText(TestLogPath, string.Format("{0}:subElementNode: i{1} = null{2}", DateTime.Now.ToString(), i, Environment.NewLine));
                    return null;
                }
            }
            WaitForElementToBeEnabledAndDisplayed(qstSession, currentNode);

            /*try
            {
                var screenshotPath = $"C:\\temp\\QSTV8_GUITestFehlerScreenshots\\GetNode_TreeViewItemAdvCollection{DateTime.Now:yyyy-MM-dd_HH-mm-ss.fffff}.png";
                qstSession.GetScreenshot().SaveAsFile(screenshotPath);
            }
            catch (WebDriverException e)
            {
                if (e.Message != "Currently selected window has been closed")
                {
                    throw;
                }
            }*/

            return currentNode;
        }

        public static void AssertGridRowWithHorizontalScrolling(WindowsDriver<WindowsElement> qstSession, AppiumWebElement parent, string aiGridString, string aiGridHeaderString, string aiGridRowPrefix, string identString, string headerText, string expectedValue, bool isCheckbox = false)
        {
            Assert.IsNotNull(parent, string.Format("Grid-Parent:{0} not found", parent));
            var grid = FindElementByAiWithWaitFromParent(parent, aiGridString, qstSession);
            Assert.IsNotNull(grid, string.Format("Grid:{0} not found", aiGridString));

            var listViewScrollAreaTop = TryFindElementBy(AiStringHelper.GeneralStrings.ScrollViewerAreaUp, grid);

            if (listViewScrollAreaTop == null)
            {
                var headerrowItems = grid.FindElementsByClassName("GridHeaderCellControl");
                int i = 0;
                bool found = false;
                foreach (var item in headerrowItems)
                {
                    if (item.FindElementByClassName("TextBlock").Text == headerText)
                    {
                        found = true;
                        break;
                    }
                    i++;
                }
                if (found)
                {
                    var givenRow = grid.FindElementByAccessibilityId(aiGridRowPrefix + identString);

                    var givenRowCells = givenRow.FindElementsByClassName("GridCell");
                    Assert.IsNotNull(givenRowCells[i].FindElementByClassName("TextBlock"), string.Format("{0} not found", headerText));
                    Assert.AreEqual(expectedValue, givenRowCells[i].FindElementByClassName("TextBlock").Text, string.Format("{0}, Wrong Value", headerText));
                }
                else
                {
                    Assert.IsTrue(found, string.Format("Header not found:{0}", headerText));
                }
            }
            else
            {
                var headerrowItems = grid.FindElementsByAccessibilityId(aiGridHeaderString);

                var scrollBarDownArrow = grid.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.ScrollViewerDownBtn);
                var ListViewScrollAreaBottom = grid.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.ScrollViewerAreaDown);
                var scrollbar = grid.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.ScrollViewerVerticalScrollbar);

                bool headerFound = false;
                bool found = false;
                int i;

                Stopwatch watch = new Stopwatch();
                watch.Reset();
                watch.Start();

                i = 0;
                foreach (var item in headerrowItems)
                {
                    if (item.FindElementByClassName("TextBlock").Text == headerText)
                    {
                        headerFound = true;
                        break;
                    }
                    i++;
                }
                Assert.IsTrue(headerFound, "Header wurde nicht gefunden");

                AppiumWebElement givenRow;
                do
                {
                    givenRow = TryFindElementBy(aiGridRowPrefix + identString, grid);
                    if (givenRow != null)
                    {
                        found = true;
                        break;
                    }
                    if (ListViewScrollAreaBottom.Size.Height == 0)
                    {
                        var scrollBarUpArrow = grid.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.ScrollViewerUpBtn);
                        Actions actions = new Actions(qstSession);
                        actions.ClickAndHold(scrollBarUpArrow);
                        actions.Build().Perform();

                        Stopwatch watchScrollTop = new Stopwatch();
                        watchScrollTop.Start();

                        while (listViewScrollAreaTop.Size.Height > 0 && watchScrollTop.ElapsedMilliseconds < 5000)
                        {
                            Thread.Sleep(100);
                        }
                        watchScrollTop.Stop();

                        actions = new Actions(qstSession);
                        actions.Release();
                        actions.Build().Perform();

                        Assert.AreEqual(0, listViewScrollAreaTop.Size.Height, "Konnte nicht ganz nach oben scrollen");
                    }
                    else
                    {
                        Stopwatch watchScrollBottom = new Stopwatch();
                        watchScrollBottom.Start();
                        //Workaround wenn gleichzeitig noch win10 Toastnotifications aufploppen und statt dem ScrollPfeil der Toast geklickt wurde
                        int topListViewSizeBefore = listViewScrollAreaTop.Size.Height;
                        do
                        {
                            scrollBarDownArrow.Click();
                        }
                        while (topListViewSizeBefore == listViewScrollAreaTop.Size.Height && watchScrollBottom.ElapsedMilliseconds < 10000);
                    }

                } while (!found && watch.ElapsedMilliseconds < 20000);
                watch.Stop();

                var givenRowCells = givenRow.FindElementsByClassName("GridCell");

                Assert.AreEqual(headerrowItems.Count, givenRowCells.Count, "HeaderRow und ChildRow haben unterschiedliche Anzahl an Elemente");

                if (!isCheckbox)
                {
                    Assert.IsNotNull(givenRowCells[i].FindElementByClassName("TextBlock"), string.Format("{0} not found", headerText));
                    Assert.AreEqual(expectedValue, givenRowCells[i].FindElementByClassName("TextBlock").Text, string.Format("{0}, Wrong Value", headerText));
                }
                else
                {
                    Assert.IsNotNull(givenRowCells[i].FindElementByClassName("CheckBox"), string.Format("{0} not found", headerText));
                    Assert.AreEqual(expectedValue, givenRowCells[i].FindElementByClassName("CheckBox").Selected.ToString(), string.Format("{0}, Wrong Value", headerText));
                }
            }
        }
        public static AppiumWebElement GetDataGridRowWithHorizontalScrolling(WindowsDriver<WindowsElement> qstSession, AppiumWebElement parent, string aiGridString, string aiGridHeaderString, string aiGridRowPrefix, string identString, string headerText)
        {
            Assert.IsNotNull(parent, string.Format("Grid-Parent:{0} not found", parent));
            var grid = FindElementByAiWithWaitFromParent(parent, aiGridString, qstSession);
            Assert.IsNotNull(grid, string.Format("Grid:{0} not found", aiGridString));

            var listViewScrollAreaTop = TryFindElementBy(AiStringHelper.GeneralStrings.ScrollViewerAreaUp, grid);

            if (listViewScrollAreaTop == null)
            {
                var headerrowItems = grid.FindElementsByClassName("GridHeaderCellControl");
                int i = 0;
                bool found = false;
                foreach (var item in headerrowItems)
                {
                    if (item.FindElementByClassName("TextBlock").Text == headerText)
                    {
                        found = true;
                        break;
                    }
                    i++;
                }
                if (found)
                {
                    var givenRow = TryFindElementBy(aiGridRowPrefix + identString, grid);
                    if (givenRow != null)
                    {
                        var givenRowCells = givenRow.FindElementsByClassName("GridCell");
                        Assert.IsNotNull(givenRowCells[i].FindElementByClassName("TextBlock"), string.Format("{0} not found", headerText));
                    }
                    return givenRow;
                }
                else
                {
                    Assert.IsTrue(found, string.Format("Header not found:{0}", headerText));
                    return null;
                }
            }
            else
            {
                var headerrowItems = grid.FindElementsByAccessibilityId(aiGridHeaderString);

                var scrollBarDownArrow = grid.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.ScrollViewerDownBtn);
                var ListViewScrollAreaBottom = grid.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.ScrollViewerAreaDown);
                var scrollbar = grid.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.ScrollViewerVerticalScrollbar);

                bool headerFound = false;
                bool found = false;
                int i;

                Stopwatch watch = new Stopwatch();
                watch.Reset();
                watch.Start();

                i = 0;
                foreach (var item in headerrowItems)
                {
                    if (item.FindElementByClassName("TextBlock").Text == headerText)
                    {
                        headerFound = true;
                        break;
                    }
                    i++;
                }
                Assert.IsTrue(headerFound, string.Format("Header not found:{0}", headerText));

                AppiumWebElement givenRow;
                bool alreadyScrolledUpOrStartedAtTop = listViewScrollAreaTop.Size.Height == 0;
                do
                {
                    givenRow = TryFindElementBy(aiGridRowPrefix + identString, grid);
                    if (givenRow != null)
                    {
                        found = true;
                        break;
                    }
                    if (ListViewScrollAreaBottom.Size.Height == 0)
                    {
                        if (alreadyScrolledUpOrStartedAtTop)
                        {
                            break;
                        }
                        else
                        {
                            alreadyScrolledUpOrStartedAtTop = true;
                        }
                        var scrollBarUpArrow = grid.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.ScrollViewerUpBtn);
                        Actions actions = new Actions(qstSession);
                        actions.ClickAndHold(scrollBarUpArrow);
                        actions.Build().Perform();

                        Stopwatch watchScrollTop = new Stopwatch();
                        watchScrollTop.Start();

                        while (listViewScrollAreaTop.Size.Height > 0 && watchScrollTop.ElapsedMilliseconds < 5000)
                        {
                            Thread.Sleep(100);
                        }
                        watchScrollTop.Stop();

                        actions = new Actions(qstSession);
                        actions.Release();
                        actions.Build().Perform();

                        Assert.AreEqual(0, listViewScrollAreaTop.Size.Height, "Konnte nicht ganz nach oben scrollen");
                    }
                    else
                    {
                        Stopwatch watchScrollBottom = new Stopwatch();
                        watchScrollBottom.Start();
                        //Workaround wenn gleichzeitig noch win10 Toastnotifications aufploppen und statt dem ScrollPfeil der Toast geklickt wurde
                        int topListViewSizeBefore = listViewScrollAreaTop.Size.Height;
                        do
                        {
                            scrollBarDownArrow.Click();
                        }
                        while (topListViewSizeBefore == listViewScrollAreaTop.Size.Height && watchScrollBottom.ElapsedMilliseconds < 10000);
                    }

                } while (!found && watch.ElapsedMilliseconds < 20000);
                watch.Stop();

                if (givenRow != null)
                {
                    var givenRowCells = givenRow.FindElementsByClassName("GridCell");
                    Assert.AreEqual(headerrowItems.Count, givenRowCells.Count, "HeaderRow und ChildRow haben unterschiedliche Anzahl an Elemente");
                }

                return givenRow;
            }
        }
        public static void SetCheckbox(AppiumWebElement checkbox, bool ValueToSet)
        {
            if (checkbox.Selected && !ValueToSet
                || !checkbox.Selected && ValueToSet)
            {
                checkbox.SendKeys(Keys.Cancel);
                checkbox.Click();
            }
        }
        public static void SetTimeTimePicker(WindowsDriver<WindowsElement> session, AppiumWebElement dropDownBtnFromTimePicker, DateTime timeToSet)
        {
            var actions = new Actions(session);
            actions.Click(dropDownBtnFromTimePicker);
            actions.SendKeys(Keys.Tab);
            actions.SendKeys(timeToSet.ToString("HH"));
            actions.SendKeys(Keys.Tab);
            actions.SendKeys(timeToSet.ToString("mm"));
            actions.SendKeys(Keys.Enter);
            actions.Build().Perform();
        }
        public static void ScrollUp(AppiumWebElement scrollviewer)
        {
            if (scrollviewer != null)
            {
                var scrollViewerAreaUp = TryFindElementBy(AiStringHelper.GeneralStrings.ScrollViewerAreaUp, scrollviewer);
                if (scrollViewerAreaUp != null)
                {
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    while (scrollViewerAreaUp.Size.Height > 0 && watch.ElapsedMilliseconds < 5000)
                    {
                        scrollviewer.SendKeys(Keys.PageUp);
                    }
                    if (scrollViewerAreaUp.Size.Height > 0)
                    {
                        Assert.IsTrue(false, "Konnte nicht ganz nach oben scrollen");
                    }
                }
            }
        }

        public static string GetToolTipText(WindowsDriver<WindowsElement> session, AppiumWebElement elementToHover, int offsetX = 0, int offsetY = 0)
        {
            Actions actions = new Actions(session);
            actions.MoveToElement(elementToHover);
            actions.MoveByOffset(offsetX, offsetY);
            actions.Build().Perform();
            Thread.Sleep(500);
            var parentWindowHandle = session.CurrentWindowHandle;
            var windowHandles = session.WindowHandles;
            foreach (var windowHandle in windowHandles)
            {
                if (windowHandle != parentWindowHandle)
                {
                    session.SwitchTo().Window(windowHandle);
                    break;
                }
            }
            WindowsElement mouseHoverElements = (WindowsElement)session.FindElementByClassName("Popup").FindElementByClassName("ToolTip");
            var tooltipText = mouseHoverElements.Text;
            session.SwitchTo().Window(parentWindowHandle);
            return tooltipText;
        }
        public static void AssertToolTipText(WindowsDriver<WindowsElement> session, AppiumWebElement elementToAssert, string expectedMessage, int offsetX = 0, int offsetY = 0)
        {
            string toolTipText = GetToolTipText(session, elementToAssert, offsetX, offsetY);
            Assert.AreEqual(expectedMessage, toolTipText);
        }
        public static void ScrollToElementWithOffset(WindowsDriver<WindowsElement> session, AppiumWebElement element, int offsetX = 0, int offsetY = -30)
        {
            element.SendKeys(Keys.Escape);
            Actions actions = new Actions(session);
            actions.MoveToElement(element, offsetX, offsetY);
            actions.Perform();
        }
        public static void SetFloatingPointTextBox(WindowsDriver<WindowsElement> driver, AppiumWebElement elementToSet, double numberToSet, string numberFormat, CultureInfo currentCulture)
        {
            elementToSet.Clear();
            elementToSet.SendKeys(Keys.ArrowRight);
            elementToSet.SendKeys(numberToSet.ToString(numberFormat, currentCulture));
        }
        public static void SetDatePicker(WindowsDriver<WindowsElement> driver, AppiumWebElement elementToSet, DateTime dateTimeToSet, CultureInfo currentCulture)
        {
            var datePickerTextBox = elementToSet.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.DatePickerTextbox);
            //Clear funktioniert nur auf der Textbox (PART_TextBox) nicht direkt auf dem DatePicker
            datePickerTextBox.Clear();
            SendKeysWithBackslash(driver, datePickerTextBox, dateTimeToSet.ToString("M/d/yyyy", currentCulture));
        }
        public static string GetBoolIcon(bool check)
        {
            if (check) 
            {
                return ValidationStringHelper.GeneralValidationStrings.Ok;
            }
            return ValidationStringHelper.GeneralValidationStrings.Nok;
        }
        public static void KillToasts(WindowsDriver<WindowsElement> desktSession)
        {
            //AppiumWebElement toastNotification = TestHelper.TryFindElementByAccessabilityId(AiStringHelper.ToastNotification.Toast, desktSession);
            AppiumWebElement toastNoticationParent = TryFindElementBy(AiStringHelper.ToastNotification.ToastParentWindowName, desktSession, AiStringHelper.By.Name);
            while (toastNoticationParent != null)
            {
                Actions actions = new Actions(desktSession);
                actions.KeyDown(Keys.Alt);
                actions.SendKeys(toastNoticationParent, Keys.F4);
                actions.KeyUp(Keys.Alt);
                actions.Build().Perform();
                Thread.Sleep(2000);
                //toastNotification = TestHelper.TryFindElementByAccessabilityId(AiStringHelper.ToastNotification.Toast, desktSession);
                toastNoticationParent = TryFindElementBy(AiStringHelper.ToastNotification.ToastParentWindowName, desktSession, AiStringHelper.By.Name);
            }
        }

        public static void AssertAndCloseToastNotification(WindowsDriver<WindowsElement> desktSession, string toastText, string toastSender = ValidationStringHelper.ToastNotificationStrings.SenderQST)
        {
            Thread.Sleep(1200);
            var toastnotification = FindElementWithWait(AiStringHelper.ToastNotification.Toast, desktSession);
            if (toastnotification != null)
            {
                var sender = toastnotification.FindElementByAccessibilityId(AiStringHelper.ToastNotification.SenderName);
                Assert.AreEqual(toastSender, sender.Text);
                var text = toastnotification.FindElementByAccessibilityId(AiStringHelper.ToastNotification.Text);
                Assert.AreEqual(toastText, text.Text);
                var closeBtn = toastnotification.FindElementByAccessibilityId(AiStringHelper.ToastNotification.CloseButton);
                
                Actions actions = new Actions(desktSession);
                //actions.Click hat scheinbar Probleme auf anderen Bildschirm zu wechseln bei unterschiedlichen Auflösungen
                //MoveToElement() funktioniert aber darum zuerst MoveToElement() und dann Click()
                actions.MoveToElement(closeBtn);
                actions.Click(closeBtn);
                actions.Build().Perform();
            }
        }
        public static bool CheckAndCloseToastNotification(WindowsDriver<WindowsElement> desktSession, string toastText, string toastSender = ValidationStringHelper.ToastNotificationStrings.SenderQST)
        {
            var toastnotification = FindElementWithWait(AiStringHelper.ToastNotification.Toast, desktSession);
            if (toastnotification != null)
            {
                var sender = toastnotification.FindElementByAccessibilityId(AiStringHelper.ToastNotification.SenderName);
                var text = toastnotification.FindElementByAccessibilityId(AiStringHelper.ToastNotification.Text);
                var closeBtn = toastnotification.FindElementByAccessibilityId(AiStringHelper.ToastNotification.CloseButton);
                if ( toastSender != sender.Text || toastText != text.Text )
                {
                    return false;
                }
                else
                {
                    closeBtn.Click();
                    return true;
                }
            }
            return false;
        }
        public static string BuildXpath(List<List<string>> elements)
        {
            StringBuilder sb = new StringBuilder("*");
            foreach (var element in elements)
            {
                sb.Append('/');
                if(!string.IsNullOrWhiteSpace(element[0]))
                {
                    sb.Append(element[0]);
                }
                else
                {
                    sb.Append('*');
                }
                sb.Append("[@");
                sb.Append(element[1]);
                sb.Append("=\"");
                sb.Append(element[2]);
                sb.Append("\"]");
            }
            return sb.ToString();
        }
    }
}