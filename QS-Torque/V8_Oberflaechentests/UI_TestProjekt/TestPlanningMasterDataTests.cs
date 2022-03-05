using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using UI_TestProjekt.Helper;
using UI_TestProjekt.TestModel;

namespace UI_TestProjekt
{
    [TestClass]
    public class TestPlanningMasterDataTests : TestBase
    {
        [TestMethod]
        [TestCategory("MasterData")]
        public void TestTestLevelSets()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestPlanningMasterData);

            var testPlanningMasterDataView = TestHelper.FindElementWithWait(AiStringHelper.TestPlanningMasterData.View, QstSession);

            var testLevelSetXTimes = Testdata.GetTestLevelSetXTimes();
            var testLevelSetEveryX = Testdata.GetTestLevelSetEveryX();
            var testLevelSetXTimesChanged = Testdata.GetTestLevelSetXTimesChanged();
            var testLevelSetEveryXChanged = Testdata.GetTestLevelSetEveryXChanged();

            DeleteTestLevelSet(QstSession, testLevelSetXTimes);
            DeleteTestLevelSet(QstSession, testLevelSetEveryX);
            DeleteTestLevelSet(QstSession, testLevelSetXTimesChanged);
            DeleteTestLevelSet(QstSession, testLevelSetEveryXChanged);

            //TestLevelSets anlegen
            CreateTestLevelSet(QstSession, testLevelSetXTimes);
            VerifyAndApplyTestLevelChanges(new TestLevelSet(), testLevelSetXTimes, new int[] {1, 1, 4, 5}, testLevelSetXTimes.Name+" created");

            CreateTestLevelSet(QstSession, testLevelSetEveryX);
            VerifyAndApplyTestLevelChanges(new TestLevelSet(), testLevelSetEveryX, new int[] {1, 4, 0, 3}, testLevelSetEveryX.Name + " created");

            //Assert TestLevelSets
            var testLevelSetTab = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTab);
            var testLevelListBox = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.ListBox);
            var testLevelSetXTimesNode = TestHelper.FindElementInListbox(testLevelSetXTimes.Name, testLevelListBox, AiStringHelper.By.Name);
            Assert.IsNotNull(testLevelSetXTimesNode);
            testLevelSetXTimesNode.Click();
            AssertTestLevelSet(QstSession, testLevelSetXTimes);

            var testLevelSetEveryXNode = TestHelper.FindElementInListbox(testLevelSetEveryX.Name, testLevelListBox, AiStringHelper.By.Name);
            Assert.IsNotNull(testLevelSetEveryXNode);
            testLevelSetEveryXNode.Click();
            AssertTestLevelSet(QstSession, testLevelSetEveryX);

            //Update TestLevelSets
            testLevelSetXTimesNode = TestHelper.FindElementInListbox(testLevelSetXTimes.Name, testLevelListBox, AiStringHelper.By.Name);
            Assert.IsNotNull(testLevelSetXTimesNode);
            testLevelSetXTimesNode.Click();
            UpdateTestLevelSet(QstSession, testLevelSetXTimesChanged);

            var saveTestLevelSet = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.Save);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, saveTestLevelSet);
            saveTestLevelSet.Click();
            VerifyAndApplyTestLevelChanges(testLevelSetXTimes, testLevelSetXTimesChanged, new int[] { 1, 4, 1, 1 }, testLevelSetXTimesChanged.Name + " aktualisiert");

            testLevelSetEveryXNode = TestHelper.FindElementInListbox(testLevelSetEveryX.Name, testLevelListBox, AiStringHelper.By.Name);
            Assert.IsNotNull(testLevelSetEveryXNode);
            testLevelSetEveryXNode.Click();
            UpdateTestLevelSet(QstSession, testLevelSetEveryXChanged);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, saveTestLevelSet);
            saveTestLevelSet.Click();
            VerifyAndApplyTestLevelChanges(testLevelSetEveryX, testLevelSetEveryXChanged, new int[] { 1, 2, 4, 1 }, testLevelSetEveryXChanged.Name + " aktualisiert");

            //Assert Changes
            testLevelSetTab = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTab);
            testLevelListBox = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.ListBox);
            var testLevelSetXTimesNodeChanged = TestHelper.FindElementInListbox(testLevelSetXTimesChanged.Name, testLevelListBox, AiStringHelper.By.Name);
            Assert.IsNotNull(testLevelSetXTimesNodeChanged);
            testLevelSetXTimesNodeChanged.Click();
            AssertTestLevelSet(QstSession, testLevelSetXTimesChanged);

            var testLevelSetEveryXNodeChanged = TestHelper.FindElementInListbox(testLevelSetEveryXChanged.Name, testLevelListBox, AiStringHelper.By.Name);
            Assert.IsNotNull(testLevelSetEveryXNodeChanged);
            testLevelSetEveryXNodeChanged.Click();
            AssertTestLevelSet(QstSession, testLevelSetEveryXChanged);

            //Delete Testlevelsets
            DeleteTestLevelSet(QstSession, testLevelSetXTimesChanged);
            DeleteTestLevelSet(QstSession, testLevelSetEveryXChanged);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestPlanningMasterData);
            testPlanningMasterDataView = TestHelper.FindElementWithWait(AiStringHelper.TestPlanningMasterData.View, QstSession);
            testLevelSetTab = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTab);
            testLevelListBox = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.ListBox);

            testLevelSetXTimesNode = TestHelper.FindElementInListbox(testLevelSetXTimes.Name, testLevelListBox, AiStringHelper.By.Name);
            Assert.IsNull(testLevelSetXTimesNode);
            testLevelSetEveryXNode = TestHelper.FindElementInListbox(testLevelSetEveryX.Name, testLevelListBox, AiStringHelper.By.Name);
            Assert.IsNull(testLevelSetEveryXNode);
            testLevelSetXTimesNodeChanged = TestHelper.FindElementInListbox(testLevelSetXTimesChanged.Name, testLevelListBox, AiStringHelper.By.Name);
            Assert.IsNull(testLevelSetXTimesNodeChanged);
            testLevelSetEveryXNodeChanged = TestHelper.FindElementInListbox(testLevelSetEveryXChanged.Name, testLevelListBox, AiStringHelper.By.Name);
            Assert.IsNull(testLevelSetEveryXNodeChanged);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestWorkingCalendar()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestPlanningMasterData);
            var testPlanningMasterDataView = TestHelper.FindElementWithWait(AiStringHelper.TestPlanningMasterData.View, QstSession);
            var workingCalendarTab = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTab);
            workingCalendarTab.Click();
            var workfreeDaySaturday = workingCalendarTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SaturdayWorkfree);
            var workfreeDaySunday = workingCalendarTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SundayWorkfree);

            var firstChristmasDaySingle = Testdata.GetWorkingCalendarEntrySingleChristmasHoliday();
            var firstChristmasDay = Testdata.GetWorkingCalendarEntryChristmasHoliday();
            var secondChristmasDay = Testdata.GetWorkingCalendarEntry2ndChristmasHoliday();
            var extraShiftHalloween = Testdata.GetWorkingCalendarEntryExtraShiftHalloween();
            var companyVacation = Testdata.GetWorkingCalendarEntrySingleCompanyVacation();

            if (!workfreeDaySaturday.Selected)
            {
                workfreeDaySaturday.Click();
            }
            if (!workfreeDaySunday.Selected)
            {
                workfreeDaySunday.Click();
            }

            DeleteWorkingCalendarEntry(QstSession, firstChristmasDaySingle, true);
            DeleteWorkingCalendarEntry(QstSession, firstChristmasDay, true);
            DeleteWorkingCalendarEntry(QstSession, secondChristmasDay, true);
            DeleteWorkingCalendarEntry(QstSession, extraShiftHalloween, true);
            DeleteWorkingCalendarEntry(QstSession, companyVacation, true);
            
            CreateWorkingCalendarEntry(QstSession, firstChristmasDaySingle);
            AssertWorkingCalendarEntry(QstSession, firstChristmasDaySingle);

            CreateWorkingCalendarEntry(QstSession, firstChristmasDay, true);
            AssertWorkingCalendarEntry(QstSession, firstChristmasDay);

            CreateWorkingCalendarEntry(QstSession, secondChristmasDay);
            AssertWorkingCalendarEntry(QstSession, secondChristmasDay);

            CreateWorkingCalendarEntry(QstSession, extraShiftHalloween);
            AssertWorkingCalendarEntry(QstSession, extraShiftHalloween);

            CreateWorkingCalendarEntry(QstSession, companyVacation);
            AssertWorkingCalendarEntry(QstSession, companyVacation);

            DeleteWorkingCalendarEntry(QstSession, firstChristmasDaySingle);
            DeleteWorkingCalendarEntry(QstSession, firstChristmasDay);
            DeleteWorkingCalendarEntry(QstSession, secondChristmasDay);
            DeleteWorkingCalendarEntry(QstSession, extraShiftHalloween);
            DeleteWorkingCalendarEntry(QstSession, companyVacation);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestPlanningMasterData);
            testPlanningMasterDataView = TestHelper.FindElementWithWait(AiStringHelper.TestPlanningMasterData.View, QstSession);
            workingCalendarTab = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTab);
            workingCalendarTab.Click();

            var firstChristmasDaySingleRow = GetWorkingCalendarRow(QstSession, firstChristmasDaySingle);
            Assert.IsNull(firstChristmasDaySingleRow, "{0} am {1} sollte gelöscht sein)", firstChristmasDaySingle.Description, firstChristmasDaySingle.GetCalenderDateRowString());
            var firstChristmasDayRow = GetWorkingCalendarRow(QstSession, firstChristmasDay);
            Assert.IsNull(firstChristmasDayRow, "{0} am {1} sollte gelöscht sein)", firstChristmasDay.Description, firstChristmasDay.GetCalenderDateRowString());
            var secondChristmasDayRow = GetWorkingCalendarRow(QstSession, secondChristmasDay);
            Assert.IsNull(secondChristmasDayRow, "{0} am {1} sollte gelöscht sein)", secondChristmasDay.Description, secondChristmasDay.GetCalenderDateRowString());
            var extraShiftHalloweenRow = GetWorkingCalendarRow(QstSession, extraShiftHalloween);
            Assert.IsNull(extraShiftHalloweenRow, "{0} am {1} sollte gelöscht sein)", extraShiftHalloween.Description, extraShiftHalloween.GetCalenderDateRowString());
            var companyVacationRow = GetWorkingCalendarRow(QstSession, companyVacation);
            Assert.IsNull(companyVacationRow, "{0} am {1} sollte gelöscht sein)", companyVacation.Description, companyVacation.GetCalenderDateRowString());
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestWorkingCalendarWeekendWorkfree()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestPlanningMasterData);
            var testPlanningMasterDataView = TestHelper.FindElementWithWait(AiStringHelper.TestPlanningMasterData.View, QstSession);
            var workingCalendarTab = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTab);
            NavigateToTestPlanningMasterDataTab(QstSession, AiStringHelper.TestPlanningMasterData.WorkingCalendarTab);

            var workfreeDaySaturday = workingCalendarTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SaturdayWorkfree);
            var workfreeDaySunday = workingCalendarTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SundayWorkfree);

            var holidaySaturday = Testdata.GetWorkingCalendarEntryHolidaySaturday();
            var holidaySunday = Testdata.GetWorkingCalendarEntryHolidaySunday();
            var holidaySaturdayAnnually = Testdata.GetWorkingCalendarEntryHolidaySaturdayAnnually();
            var holidaySundayAnnually = Testdata.GetWorkingCalendarEntryHolidaySundayAnnually();
            var extraShiftSaturday = Testdata.GetWorkingCalendarEntryExtraShiftSaturday();
            var extraShiftSunday = Testdata.GetWorkingCalendarEntryExtraShiftSunday();

            if (workfreeDaySaturday.Selected)
            {
                workfreeDaySaturday.Click();
                AssertProcessToolTestNotification();
            }
            if (workfreeDaySunday.Selected)
            {
                workfreeDaySunday.Click();
                AssertProcessToolTestNotification();
            }

            DeleteWorkingCalendarEntry(QstSession, holidaySaturday, true);
            DeleteWorkingCalendarEntry(QstSession, holidaySunday, true);
            DeleteWorkingCalendarEntry(QstSession, holidaySaturdayAnnually, true);
            DeleteWorkingCalendarEntry(QstSession, holidaySundayAnnually, true);

            if (!workfreeDaySaturday.Selected)
            {
                workfreeDaySaturday.Click();
                AssertProcessToolTestNotification();
            }
            if (!workfreeDaySunday.Selected)
            {
                workfreeDaySunday.Click();
                AssertProcessToolTestNotification();
            }

            DeleteWorkingCalendarEntry(QstSession, extraShiftSaturday, true);
            DeleteWorkingCalendarEntry(QstSession, extraShiftSunday, true);

            CreateWorkingCalendarEntry(QstSession, holidaySaturday, false, true);
            CreateWorkingCalendarEntry(QstSession, holidaySunday, false, true);

            var holidaySaturdayRow = GetWorkingCalendarRow(QstSession, holidaySaturday);
            Assert.IsNull(holidaySaturdayRow, "{0} am {1} sollte nicht angelegt werden", holidaySaturday.Description, holidaySaturday.GetCalenderDateRowString());

            var holidaySundayRow = GetWorkingCalendarRow(QstSession, holidaySunday);
            Assert.IsNull(holidaySundayRow, "{0} am {1} sollte nicht angelegt werden", holidaySunday.Description, holidaySunday.GetCalenderDateRowString());

            CreateWorkingCalendarEntry(QstSession, holidaySaturdayAnnually, false, true);
            var holidaySaturdayAnnuallyRow = GetWorkingCalendarRow(QstSession, holidaySaturdayAnnually);
            Assert.IsNotNull(holidaySaturdayAnnuallyRow, "{0} am {1} sollte angelegt werden (ist Jährlich)", holidaySunday.Description, holidaySunday.GetCalenderDateRowString());

            CreateWorkingCalendarEntry(QstSession, holidaySundayAnnually, false, true);
            var holidaySundayAnnuallyRow = GetWorkingCalendarRow(QstSession, holidaySundayAnnually);
            Assert.IsNotNull(holidaySundayAnnuallyRow, "{0} am {1} sollte angelegt werden (ist Jährlich)", holidaySunday.Description, holidaySunday.GetCalenderDateRowString());

            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, workfreeDaySaturday);
            workfreeDaySaturday.Click();
            AssertProcessToolTestNotification();

            CreateWorkingCalendarEntry(QstSession, extraShiftSaturday, false, true);
            var extraShiftSaturdayRow = GetWorkingCalendarRow(QstSession, extraShiftSaturday);
            Assert.IsNull(extraShiftSaturdayRow, "{0} am {1} sollte nicht angelegt werden)", extraShiftSaturday.Description, extraShiftSaturday.GetCalenderDateRowString());

            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, workfreeDaySunday);
            workfreeDaySunday.Click();
            AssertProcessToolTestNotification();

            CreateWorkingCalendarEntry(QstSession, extraShiftSunday, false, true);
            var extraShiftSundayRow = GetWorkingCalendarRow(QstSession, extraShiftSunday);
            Assert.IsNull(extraShiftSundayRow, "{0} am {1} sollte nicht angelegt werden)", extraShiftSunday.Description, extraShiftSunday.GetCalenderDateRowString());

            DeleteWorkingCalendarEntry(QstSession, holidaySaturdayAnnually);
            DeleteWorkingCalendarEntry(QstSession, holidaySundayAnnually);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestShiftManagement()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestPlanningMasterData);
            NavigateToTestPlanningMasterDataTab(QstSession, AiStringHelper.TestPlanningMasterData.ShiftManagementTab);

            ShiftManagementUiHelper shiftManagementUiHelper = GetShiftManagementUiHelper(QstSession);

            ShiftManagement defaultShiftManagement = Testdata.GetShiftManagementDefault();
            ShiftManagement singleShiftManagement = Testdata.GetShiftManagementSingleShift();
            //Werte von DefaultShift übernehmen, da dessen Zeiten noch als Alt-Werte gespeichert sind
            singleShiftManagement.SecondShiftFromTime = defaultShiftManagement.SecondShiftFromTime;
            singleShiftManagement.SecondShiftToTime = defaultShiftManagement.SecondShiftToTime;
            ShiftManagement dualShiftManagement = Testdata.GetShiftManagementDualShift();

            UpdateShiftManagement(QstSession, defaultShiftManagement, shiftManagementUiHelper);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, shiftManagementUiHelper.save);
            shiftManagementUiHelper.save.Click();
            var viewVerifyChanges = TestHelper.TryFindElementByAccessabilityId(AiStringHelper.VerifyChanges.View, DesktSession);
            AppiumWebElement btnApply;
            if (viewVerifyChanges != null)
            {
                btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
                btnApply.Click();
                TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
                AssertProcessToolTestNotification();
            }

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestPlanningMasterData);
            NavigateToTestPlanningMasterDataTab(QstSession, AiStringHelper.TestPlanningMasterData.ShiftManagementTab);
            shiftManagementUiHelper = GetShiftManagementUiHelper(QstSession, shiftManagementUiHelper);
            AssertShiftManagement(QstSession, defaultShiftManagement, shiftManagementUiHelper);

            UpdateShiftManagement(QstSession, singleShiftManagement, shiftManagementUiHelper);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, shiftManagementUiHelper.save);
            shiftManagementUiHelper.save.Click();
            viewVerifyChanges = DesktSession.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.View);
            var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyShiftManagementChangesInVerifyView(QstSession, defaultShiftManagement, singleShiftManagement, listViewChanges, 6);
            viewVerifyChanges = TestHelper.FindElementWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            btnApply.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            AssertProcessToolTestNotification();

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestPlanningMasterData);
            NavigateToTestPlanningMasterDataTab(QstSession, AiStringHelper.TestPlanningMasterData.ShiftManagementTab);
            shiftManagementUiHelper = GetShiftManagementUiHelper(QstSession, shiftManagementUiHelper);
            AssertShiftManagement(QstSession, singleShiftManagement, shiftManagementUiHelper);

            UpdateShiftManagement(QstSession, dualShiftManagement, shiftManagementUiHelper);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, shiftManagementUiHelper.save);
            shiftManagementUiHelper.save.Click();
            viewVerifyChanges = DesktSession.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.View);
            listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyShiftManagementChangesInVerifyView(QstSession, singleShiftManagement, dualShiftManagement, listViewChanges, 7);
            viewVerifyChanges = TestHelper.FindElementWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            btnApply.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            AssertProcessToolTestNotification();

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestPlanningMasterData);
            NavigateToTestPlanningMasterDataTab(QstSession, AiStringHelper.TestPlanningMasterData.ShiftManagementTab);
            shiftManagementUiHelper = GetShiftManagementUiHelper(QstSession, shiftManagementUiHelper);
            AssertShiftManagement(QstSession, dualShiftManagement, shiftManagementUiHelper);
        }


        private static void AssertTestLevelSet(WindowsDriver<WindowsElement> QstSession, TestLevelSet testLevelSet)
        {
            var testPlanningMasterDataView = TestHelper.FindElementWithWait(AiStringHelper.TestPlanningMasterData.View, QstSession);
            var testLevelSetTab = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTab);

            var name = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.Name);
            Assert.AreEqual(testLevelSet.Name, name.Text);

            var interval1 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.Interval1);
            var interval1TextBox = interval1.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
            Assert.AreEqual(testLevelSet.Interval1.ToString(), interval1TextBox.Text);

            var intervalType1 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.IntervalType1);
            Assert.AreEqual(testLevelSet.IntervalType1, TestHelper.GetSelectedComboboxString(QstSession, intervalType1));

            var sampleNumber1 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.SampleNumber1);
            var sampleNumber1TextBox = sampleNumber1.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
            sampleNumber1TextBox.SendKeys(testLevelSet.SampleNumber1.ToString());
            Assert.AreEqual(testLevelSet.SampleNumber1.ToString(), sampleNumber1TextBox.Text);

            var considerWorkingCalendar1 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.ConsiderWorkingCalendar1);
            if (testLevelSet.ConsiderWorkingCalendar1)
            {
                Assert.IsTrue(considerWorkingCalendar1.Selected);
            }
            else
            {
                Assert.IsFalse(considerWorkingCalendar1.Selected);
            }

            var isActive2 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.TestLevelSet2Active);
            if (testLevelSet.IsActive2)
            {
                Assert.IsTrue(isActive2.Selected);
            }
            else
            {
                Assert.IsFalse(isActive2.Selected);
            }

            var interval2 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.Interval2);
            var interval2TextBox = interval2.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
            var intervalType2 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.IntervalType2);
            var sampleNumber2 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.SampleNumber2);
            var sampleNumber2TextBox = sampleNumber2.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
            var considerWorkingCalendar2 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.ConsiderWorkingCalendar2);

            if (testLevelSet.IsActive2)
            {
                Assert.AreEqual(testLevelSet.Interval2.ToString(), interval2TextBox.Text);
                Assert.AreEqual(testLevelSet.IntervalType2, TestHelper.GetSelectedComboboxString(QstSession, intervalType2));
                Assert.AreEqual(testLevelSet.SampleNumber2.ToString(), sampleNumber2TextBox.Text);

                if (testLevelSet.ConsiderWorkingCalendar2)
                {
                    Assert.IsTrue(considerWorkingCalendar2.Selected);
                }
                else
                {
                    Assert.IsFalse(considerWorkingCalendar2.Selected);
                }
            }
            else
            {
                Assert.IsFalse(interval2.Enabled);
                Assert.IsFalse(interval2TextBox.Enabled);
                Assert.IsFalse(intervalType2.Enabled);
                Assert.IsFalse(sampleNumber2.Enabled);
                Assert.IsFalse(sampleNumber2TextBox.Enabled);
                Assert.IsFalse(considerWorkingCalendar2.Enabled);
            }

            var isActive3 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.TestLevelSet3Active);
            if (testLevelSet.IsActive3)
            {
                Assert.IsTrue(isActive3.Selected);
            }
            else
            {
                Assert.IsFalse(isActive3.Selected);
            }

            var interval3 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.Interval3);
            var interval3TextBox = interval3.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
            var intervalType3 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.IntervalType3);
            var sampleNumber3 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.SampleNumber3);
            var sampleNumber3TextBox = sampleNumber3.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
            var considerWorkingCalendar3 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.ConsiderWorkingCalendar3);
            if (testLevelSet.IsActive3)
            {
                Assert.AreEqual(testLevelSet.Interval3.ToString(), interval3TextBox.Text);
                Assert.AreEqual(testLevelSet.IntervalType3, TestHelper.GetSelectedComboboxString(QstSession, intervalType3));
                Assert.AreEqual(testLevelSet.SampleNumber3.ToString(), sampleNumber3TextBox.Text);
                if (testLevelSet.ConsiderWorkingCalendar3)
                {
                    Assert.IsTrue(considerWorkingCalendar3.Selected);
                }
                else
                {
                    Assert.IsFalse(considerWorkingCalendar3.Selected);
                }
            }
            else
            {
                Assert.IsFalse(interval3.Enabled);
                Assert.IsFalse(interval3TextBox.Enabled);
                Assert.IsFalse(intervalType3.Enabled);
                Assert.IsFalse(sampleNumber3.Enabled);
                Assert.IsFalse(sampleNumber3TextBox.Enabled);
                Assert.IsFalse(considerWorkingCalendar3.Enabled);
            }
        }
        private static void UpdateTestLevelSet(WindowsDriver<WindowsElement> QstSession, TestLevelSet testLevelSet)
        {
            var testPlanningMasterDataView = TestHelper.FindElementWithWait(AiStringHelper.TestPlanningMasterData.View, QstSession);
            var testLevelSetTab = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTab);
            //Warten bis der Default Text "Test level set X" von QST gesetzt wird
            Thread.Sleep(300);
            var name = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.Name);
            name.Clear();
            TestHelper.SendKeysConverted(name, testLevelSet.Name);

            var interval1 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.Interval1);
            var interval1TextBox = interval1.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
            interval1TextBox.SendKeys(testLevelSet.Interval1.ToString());

            var intervalType1 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.IntervalType1);
            TestHelper.ClickComboBoxEntry(QstSession, testLevelSet.IntervalType1, intervalType1);

            var sampleNumber1 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.SampleNumber1);
            var sampleNumber1TextBox = sampleNumber1.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
            sampleNumber1TextBox.SendKeys(testLevelSet.SampleNumber1.ToString());

            var considerWorkingCalendar1 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.ConsiderWorkingCalendar1);
            TestHelper.SetCheckbox(considerWorkingCalendar1, testLevelSet.ConsiderWorkingCalendar1);

            var isActive2 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.TestLevelSet2Active);
            TestHelper.SetCheckbox(isActive2, testLevelSet.IsActive2);

            var interval2 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.Interval2);
            var interval2TextBox = interval2.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
            var intervalType2 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.IntervalType2);
            var sampleNumber2 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.SampleNumber2);
            var sampleNumber2TextBox = sampleNumber2.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
            var considerWorkingCalendar2 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.ConsiderWorkingCalendar2);

            if (testLevelSet.IsActive2)
            {
                TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, interval2TextBox);
                interval2TextBox.SendKeys(testLevelSet.Interval2.ToString());
                TestHelper.ClickComboBoxEntry(QstSession, testLevelSet.IntervalType2, intervalType2);
                sampleNumber2TextBox.SendKeys(testLevelSet.SampleNumber2.ToString());
                TestHelper.SetCheckbox(considerWorkingCalendar2, testLevelSet.ConsiderWorkingCalendar2);
            }
            else
            {
                Assert.IsFalse(interval2.Enabled);
                Assert.IsFalse(interval2TextBox.Enabled);
                Assert.IsFalse(intervalType2.Enabled);
                Assert.IsFalse(sampleNumber2.Enabled);
                Assert.IsFalse(sampleNumber2TextBox.Enabled);
                Assert.IsFalse(considerWorkingCalendar2.Enabled);
            }

            var isActive3 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.TestLevelSet3Active);
            TestHelper.SetCheckbox(isActive3, testLevelSet.IsActive3);

            var interval3 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.Interval3);
            var interval3TextBox = interval3.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
            var intervalType3 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.IntervalType3);
            var sampleNumber3 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.SampleNumber3);
            var sampleNumber3TextBox = sampleNumber3.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
            var considerWorkingCalendar3 = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.ConsiderWorkingCalendar3);
            if (testLevelSet.IsActive3)
            {
                TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, interval3TextBox);
                interval3TextBox.SendKeys(testLevelSet.Interval3.ToString());
                TestHelper.ClickComboBoxEntry(QstSession, testLevelSet.IntervalType3, intervalType3);
                sampleNumber3TextBox.SendKeys(testLevelSet.SampleNumber3.ToString());
                TestHelper.SetCheckbox(considerWorkingCalendar3, testLevelSet.ConsiderWorkingCalendar3);
            }
            else
            {
                Assert.IsFalse(interval3.Enabled);
                Assert.IsFalse(interval3TextBox.Enabled);
                Assert.IsFalse(intervalType3.Enabled);
                Assert.IsFalse(sampleNumber3.Enabled);
                Assert.IsFalse(sampleNumber3TextBox.Enabled);
                Assert.IsFalse(considerWorkingCalendar3.Enabled);
            }
        }
        public static void CreateTestLevelSet(WindowsDriver<WindowsElement> QstSession, TestLevelSet testLevelSet)
        {
            var testPlanningMasterDataView = TestHelper.FindElementWithWait(AiStringHelper.TestPlanningMasterData.View, QstSession);
            var testLevelSetTab = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTab);
            var testLevelListBox = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.ListBox);
            var testLevelSetNode = TestHelper.FindElementInListbox(testLevelSet.Name, testLevelListBox, AiStringHelper.By.Name);
            if (testLevelSetNode != null)
            {
                testLevelSetNode.Click();
            }
            else
            {
                var addTestLevelSet = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.Add);
                TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, addTestLevelSet);
                addTestLevelSet.Click();
            }
            UpdateTestLevelSet(QstSession, testLevelSet);

            var saveTestLevelSet = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.Save);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, saveTestLevelSet);
            saveTestLevelSet.Click();
        }
        public static void DeleteTestLevelSet(WindowsDriver<WindowsElement> QstSession, TestLevelSet testLevelSet)
        {
            var testPlanningMasterDataView = TestHelper.FindElementWithWait(AiStringHelper.TestPlanningMasterData.View, QstSession);
            var deleteTestLevelSet = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.Delete);
            var testLevelSetTab = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTab);
            var testLevelListBox = testLevelSetTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.TestLevelSetsTabElements.ListBox);
            var testLevelSetNode = TestHelper.FindElementInListbox(testLevelSet.Name, testLevelListBox);

            if (testLevelSetNode != null)
            {
                testLevelSetNode.Click();
                TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, deleteTestLevelSet);
                deleteTestLevelSet.Click();

                var confirmBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
                confirmBtn.Click();
            }
        }
        /// <summary>
        /// Prüft die Änderungen eines TestLevelSets im VerifyView 
        /// </summary>
        /// <param name="testLevelSet"></param>
        /// <param name="testLevelSetChanged"></param>
        /// <param name="listViewChanges"></param>
        /// <param name="numberOfExpectedChanges">Int-Array[4] mit den Änderungszahlen zu name (1), Testlevel(1-3)</param>
        /// <param name="checkName">Check ob alter Testlevelsetname überprüft werden soll. Default false</param>
        private static void VerifyTestLevelSetInVerifyView(TestLevelSet testLevelSet, TestLevelSet testLevelSetChanged, AppiumWebElement listViewChanges, int[] numberOfExpectedChanges)
        {
            var groupChanges = listViewChanges.FindElementsByClassName("GroupItem");

            bool calledVerifyName = false;
            bool calledVerifyTestLevel1 = false;
            bool calledVerifyTestLevel2 = false;
            bool calledVerifyTestLevel3 = false;

            foreach (var groupChange in groupChanges)
            {
                if (groupChange.Text == testLevelSetChanged.Name)
                {
                    var changes = groupChange.FindElementsByClassName("ListViewItem");
                    VerifyTestLevelSetNameInVerifyView(testLevelSet, testLevelSetChanged, changes, numberOfExpectedChanges[0]);
                    calledVerifyName = true;
                }
                else if (groupChange.Text == testLevelSetChanged.Name + " - Test level 1")
                {
                    var changes = groupChange.FindElementsByClassName("ListViewItem");
                    VerifyTestLevelSetTestLevel1InVerifyView(testLevelSet, testLevelSetChanged, changes, numberOfExpectedChanges[1]);
                    calledVerifyTestLevel1 = true;
                }
                else if (groupChange.Text == testLevelSetChanged.Name + " - Test level 2")
                {
                    var changes = groupChange.FindElementsByClassName("ListViewItem");
                    VerifyTestLevelSetTestLevel2InVerifyView(testLevelSet, testLevelSetChanged, changes, numberOfExpectedChanges[2]);
                    calledVerifyTestLevel2 = true;
                }
                else if (groupChange.Text == testLevelSetChanged.Name + " - Test level 3")
                {
                    var changes = groupChange.FindElementsByClassName("ListViewItem");
                    VerifyTestLevelSetTestLevel3InVerifyView(testLevelSet, testLevelSetChanged, changes, numberOfExpectedChanges[3]);
                    calledVerifyTestLevel3 = true;
                }
                else
                {
                    Assert.IsTrue(false, string.Format("Case '{0}' not implemented", groupChange.Text));
                }
            }
            if(numberOfExpectedChanges[0] > 0)
            {
                Assert.IsTrue(calledVerifyName, "VerifyName hätte aufgerufen werden sollen");
            }
            if (numberOfExpectedChanges[1] > 0)
            {
                Assert.IsTrue(calledVerifyTestLevel1, "VerifyTestLevel1 hätte aufgerufen werden sollen");
            }
            if (numberOfExpectedChanges[2] > 0)
            {
                Assert.IsTrue(calledVerifyTestLevel2, "VerifyTestLevel2 hätte aufgerufen werden sollen");
            }
            if (numberOfExpectedChanges[3] > 0)
            {
                Assert.IsTrue(calledVerifyTestLevel3, "VerifyTestLevel3 hätte aufgerufen werden sollen");
            }
        }
        private static void VerifyTestLevelSetNameInVerifyView(TestLevelSet testLevelSet, TestLevelSet testLevelSetChanged, IReadOnlyCollection<AppiumWebElement> changes, int numberOfExpectedChanges, bool checkNameOld = false)
        {

            Assert.AreEqual(numberOfExpectedChanges, changes.Count);
            foreach (AppiumWebElement item in changes)
            {
                string changeTypeText = item.FindElementsByClassName("TextBlock")[1].Text;
                switch (changeTypeText)
                {
                    case "Name":
                        if (checkNameOld)
                        {
                            Assert.AreEqual(testLevelSet.Name, item.FindElementsByClassName("TextBlock")[2].Text);
                        }
                        Assert.AreEqual(testLevelSetChanged.Name, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    default:
                        Assert.IsTrue(false, string.Format("Case '{0}' not implemented", changeTypeText));
                        break;
                }
            }
        }
        private static void VerifyTestLevelSetTestLevel1InVerifyView(TestLevelSet testLevelSet, TestLevelSet testLevelSetChanged, IReadOnlyCollection<AppiumWebElement> changes, int numberOfExpectedChangesTestLevel1)
        {
            Assert.AreEqual(numberOfExpectedChangesTestLevel1, changes.Count);
            foreach (AppiumWebElement item in changes)
            {
                string changeTypeText = item.FindElementsByClassName("TextBlock")[1].Text;
                switch (changeTypeText)
                {
                    case "Interval value":
                        Assert.AreEqual(testLevelSet.Interval1.ToString(), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(testLevelSetChanged.Interval1.ToString(), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Interval type":
                        Assert.AreEqual(testLevelSet.IntervalType1, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(testLevelSetChanged.IntervalType1, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Sample number":
                        Assert.AreEqual(testLevelSet.SampleNumber1.ToString(), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(testLevelSetChanged.SampleNumber1.ToString(), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    //TODO Ändern wenn calendar richtig geschrieben wird
                    case "Consider working calender":
                        Assert.AreEqual(TestHelper.GetBoolIcon(testLevelSet.ConsiderWorkingCalendar1), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolIcon(testLevelSetChanged.ConsiderWorkingCalendar1), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    default:
                        Assert.IsTrue(false, string.Format("Case '{0}' not implemented", changeTypeText));
                        break;
                }
            }
        }
        private static void VerifyTestLevelSetTestLevel2InVerifyView(TestLevelSet testLevelSet, TestLevelSet testLevelSetChanged, IReadOnlyCollection<AppiumWebElement> changes, int numberOfExpectedChangesTestLevel2)
        {
            Assert.AreEqual(numberOfExpectedChangesTestLevel2, changes.Count);
            foreach (AppiumWebElement item in changes)
            {
                string changeTypeText = item.FindElementsByClassName("TextBlock")[1].Text;
                switch (changeTypeText)
                {
                    case "Interval value":
                        if (testLevelSet.IsActive2)
                        {
                            Assert.AreEqual(testLevelSet.Interval2.ToString(), item.FindElementsByClassName("TextBlock")[2].Text);
                        }
                        if (testLevelSetChanged.IsActive2)
                        {
                            Assert.AreEqual(testLevelSetChanged.Interval2.ToString(), item.FindElementsByClassName("TextBlock")[3].Text);
                        }
                        break;
                    case "Interval type":
                        if (testLevelSet.IsActive2)
                        {
                            Assert.AreEqual(testLevelSet.IntervalType2, item.FindElementsByClassName("TextBlock")[2].Text);
                        }
                        if (testLevelSetChanged.IsActive2)
                        {
                            Assert.AreEqual(testLevelSetChanged.IntervalType2, item.FindElementsByClassName("TextBlock")[3].Text);
                        }
                        break;
                    case "Sample number":
                        if (testLevelSet.IsActive2)
                        {
                            Assert.AreEqual(testLevelSet.SampleNumber2.ToString(), item.FindElementsByClassName("TextBlock")[2].Text);
                        }
                        if (testLevelSetChanged.IsActive2)
                        {
                            Assert.AreEqual(testLevelSetChanged.SampleNumber2.ToString(), item.FindElementsByClassName("TextBlock")[3].Text);
                        }
                        break;
                    //TODO Ändern wenn calendar richtig geschrieben wird
                    case "Consider working calender":
                        if (testLevelSet.IsActive2)
                        {
                            Assert.AreEqual(TestHelper.GetBoolIcon(testLevelSet.ConsiderWorkingCalendar2), item.FindElementsByClassName("TextBlock")[2].Text);
                        }
                        if (testLevelSetChanged.IsActive2)
                        {
                            Assert.AreEqual(TestHelper.GetBoolIcon(testLevelSetChanged.ConsiderWorkingCalendar2), item.FindElementsByClassName("TextBlock")[3].Text);
                        }
                        break;
                    case "Is active":
                        Assert.AreEqual(TestHelper.GetBoolIcon(testLevelSet.IsActive2), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolIcon(testLevelSetChanged.IsActive2), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    default:
                        Assert.IsTrue(false, string.Format("Case '{0}' not implemented", changeTypeText));
                        break;
                }
            }
        }
        private static void VerifyTestLevelSetTestLevel3InVerifyView(TestLevelSet testLevelSet, TestLevelSet testLevelSetChanged, IReadOnlyCollection<AppiumWebElement> changes, int numberOfExpectedChangesTestLevel3)
        {
            Assert.AreEqual(numberOfExpectedChangesTestLevel3, changes.Count);
            foreach (AppiumWebElement item in changes)
            {
                string changeTypeText = item.FindElementsByClassName("TextBlock")[1].Text;
                switch (changeTypeText)
                {
                    case "Interval value":
                        if (testLevelSet.IsActive3)
                        {
                            Assert.AreEqual(testLevelSet.Interval3.ToString(), item.FindElementsByClassName("TextBlock")[2].Text);
                        }
                        if (testLevelSetChanged.IsActive3)
                        {
                            Assert.AreEqual(testLevelSetChanged.Interval3.ToString(), item.FindElementsByClassName("TextBlock")[3].Text);
                        }
                        break;
                    case "Interval type":
                        if (testLevelSet.IsActive3)
                        {
                            Assert.AreEqual(testLevelSet.IntervalType3, item.FindElementsByClassName("TextBlock")[2].Text);
                        }
                        if (testLevelSetChanged.IsActive3)
                        {
                            Assert.AreEqual(testLevelSetChanged.IntervalType3, item.FindElementsByClassName("TextBlock")[3].Text);
                        }
                        break;
                    case "Sample number":
                        if (testLevelSet.IsActive3)
                        {
                            Assert.AreEqual(testLevelSet.SampleNumber3.ToString(), item.FindElementsByClassName("TextBlock")[2].Text);
                        }
                        if (testLevelSetChanged.IsActive3)
                        {
                            Assert.AreEqual(testLevelSetChanged.SampleNumber3.ToString(), item.FindElementsByClassName("TextBlock")[3].Text);
                        }
                        break;
                    //TODO Ändern wenn calendar richtig geschrieben wird
                    case "Consider working calender":
                        if (testLevelSet.IsActive3)
                        {
                            Assert.AreEqual(TestHelper.GetBoolIcon(testLevelSet.ConsiderWorkingCalendar3), item.FindElementsByClassName("TextBlock")[2].Text);
                        }
                        if (testLevelSetChanged.IsActive3)
                        {
                            Assert.AreEqual(TestHelper.GetBoolIcon(testLevelSetChanged.ConsiderWorkingCalendar3), item.FindElementsByClassName("TextBlock")[3].Text);
                        }
                        break;
                    case "Is active":
                        Assert.AreEqual(TestHelper.GetBoolIcon(testLevelSet.IsActive3), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolIcon(testLevelSetChanged.IsActive3), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    default:
                        Assert.IsTrue(false, string.Format("Case '{0}' not implemented", changeTypeText));
                        break;
                }
            }
        }
        
        public static void VerifyAndApplyTestLevelChanges(TestLevelSet testLevelSet, TestLevelSet testLevelSetChanged, int[] numberOfChanges, string changeComment)
        {
            var viewVerifyChanges = DesktSession.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.View);
            var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyTestLevelSetInVerifyView(testLevelSet, testLevelSetChanged, listViewChanges, numberOfChanges);
            var textBoxComment = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Comment);
            TestHelper.SendKeysConverted(textBoxComment, changeComment);

            var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            btnApply.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            AssertProcessToolTestNotification();
        }

        private static void DeleteWorkingCalendarEntry(WindowsDriver<WindowsElement> QstSession, CalendarEntry calendarEntry, bool DeleteAnnuallyAndSingleEntry = false)
        {
            var testPlanningMasterDataView = TestHelper.FindElementWithWait(AiStringHelper.TestPlanningMasterData.View, QstSession);
            var workingCalendarTab = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTab);
            var workfreeDaySaturday = workingCalendarTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SaturdayWorkfree);
            var workfreeDaySunday = workingCalendarTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SundayWorkfree);

            AppiumWebElement selectedRow = null;

            if (DeleteAnnuallyAndSingleEntry)
            {
                calendarEntry.IsAnnually = !calendarEntry.IsAnnually;
                DeleteWorkingCalendarEntry(QstSession, calendarEntry, false);
                calendarEntry.IsAnnually = !calendarEntry.IsAnnually;
            }

            if (calendarEntry.IsAnnually)
            {
                var annuallyGridString = AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.AnnuallyEntriesGrid;
                var annuallyGridHeader = AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.AnnuallyGridHeader;
                var annuallyGridRowPrefix = AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.AnnuallyGridRowPrefix;

                selectedRow = TestHelper.GetDataGridRowWithHorizontalScrolling(QstSession, workingCalendarTab, annuallyGridString, annuallyGridHeader, annuallyGridRowPrefix, calendarEntry.GetCalenderDateRowString(), CalendarEntry.CalendarEntryListHeaderStrings.CalendarDate);
            }
            else
            {
                bool calendarEntryIsWeekday = calendarEntry.CalendarEntryIsWeekday();
                bool calendarEntryIsValidSaturday = calendarEntry.CalendarEntryIsValidSaturday(workfreeDaySaturday.Selected);
                bool calendarEntryIsValidSunday = calendarEntry.CalendarEntryIsValidSunday(workfreeDaySunday.Selected);

                if (calendarEntryIsWeekday || calendarEntryIsValidSaturday || calendarEntryIsValidSunday)
                {
                    var singleEntriesGridString = AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SingleEntriesGrid;
                    var singleEntriesGridHeader = AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SingleEntriesGridHeader;
                    var singleEntriesGridRowPrefix = AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SingleEntriesGridRowPrefix;

                    selectedRow = TestHelper.GetDataGridRowWithHorizontalScrolling(QstSession, workingCalendarTab, singleEntriesGridString, singleEntriesGridHeader, singleEntriesGridRowPrefix, calendarEntry.GetCalenderDateRowString(), CalendarEntry.CalendarEntryListHeaderStrings.CalendarDate);
                }
            }
            if (selectedRow != null)
            {
                selectedRow.Click();

                var deleteCalendarEntry = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.RemoveEntry);
                TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, deleteCalendarEntry);
                deleteCalendarEntry.Click();

                var confirmBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
                confirmBtn.Click();

                AssertProcessToolTestNotification();
            }
        }
        private static void AssertWorkingCalendarEntry(WindowsDriver<WindowsElement> QstSession, CalendarEntry calendarEntry)
        {
            var testPlanningMasterDataView = TestHelper.FindElementWithWait(AiStringHelper.TestPlanningMasterData.View, QstSession);
            var workingCalendarTab = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTab);
            var workfreeDaySaturday = workingCalendarTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SaturdayWorkfree);
            var workfreeDaySunday = workingCalendarTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SundayWorkfree);

            if (calendarEntry.IsAnnually)
            {
                var annuallyGridString = AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.AnnuallyEntriesGrid;
                var annuallyGridHeader = AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.AnnuallyGridHeader;
                var annuallyGridRowPrefix = AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.AnnuallyGridRowPrefix;

                TestHelper.AssertGridRowWithHorizontalScrolling(QstSession, workingCalendarTab, annuallyGridString, annuallyGridHeader, annuallyGridRowPrefix, calendarEntry.GetCalenderDateRowString(), CalendarEntry.CalendarEntryListHeaderStrings.CalendarDate, calendarEntry.GetCalenderDateRowString());
                TestHelper.AssertGridRowWithHorizontalScrolling(QstSession, workingCalendarTab, annuallyGridString, annuallyGridHeader, annuallyGridRowPrefix, calendarEntry.GetCalenderDateRowString(), CalendarEntry.CalendarEntryListHeaderStrings.Description, calendarEntry.Description);
                TestHelper.AssertGridRowWithHorizontalScrolling(QstSession, workingCalendarTab, annuallyGridString, annuallyGridHeader, annuallyGridRowPrefix, calendarEntry.GetCalenderDateRowString(), CalendarEntry.CalendarEntryListHeaderStrings.Type, calendarEntry.getDayTypeString());
            }
            else
            {
                bool calendarEntryIsWeekday = calendarEntry.CalendarEntryIsWeekday();
                bool calendarEntryIsValidSaturday = calendarEntry.CalendarEntryIsValidSaturday(workfreeDaySaturday.Selected);
                bool calendarEntryIsValidSunday = calendarEntry.CalendarEntryIsValidSunday(workfreeDaySunday.Selected);

                if (calendarEntryIsWeekday || calendarEntryIsValidSaturday || calendarEntryIsValidSunday)
                {
                    var singleEntriesGridString = AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SingleEntriesGrid;
                    var singleEntriesGridHeader = AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SingleEntriesGridHeader;
                    var singleEntriesGridRowPrefix = AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SingleEntriesGridRowPrefix;

                    TestHelper.AssertGridRowWithHorizontalScrolling(QstSession, workingCalendarTab, singleEntriesGridString, singleEntriesGridHeader, singleEntriesGridRowPrefix, calendarEntry.GetCalenderDateRowString(), CalendarEntry.CalendarEntryListHeaderStrings.CalendarDate, calendarEntry.GetCalenderDateRowString());
                    TestHelper.AssertGridRowWithHorizontalScrolling(QstSession, workingCalendarTab, singleEntriesGridString, singleEntriesGridHeader, singleEntriesGridRowPrefix, calendarEntry.GetCalenderDateRowString(), CalendarEntry.CalendarEntryListHeaderStrings.Description, calendarEntry.Description);
                    TestHelper.AssertGridRowWithHorizontalScrolling(QstSession, workingCalendarTab, singleEntriesGridString, singleEntriesGridHeader, singleEntriesGridRowPrefix, calendarEntry.GetCalenderDateRowString(), CalendarEntry.CalendarEntryListHeaderStrings.Type, calendarEntry.getDayTypeString());
                }
            }
        }
        private static void CreateWorkingCalendarEntry(WindowsDriver<WindowsElement> QstSession, CalendarEntry calendarEntry, bool entryOverwrites = false, bool checkWeekend = false)
        {
            WindowsElement testPlanningMasterDataView = TestHelper.FindElementWithWait(AiStringHelper.TestPlanningMasterData.View, QstSession);
            var addCalendarEntry = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.AddCalendarEntry);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, addCalendarEntry);
            addCalendarEntry.Click();
            Thread.Sleep(500);

            var assistantSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.Assistant.View, TestConfiguration.GetWindowsApplicationDriverUrl());
            var dateInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputDate, assistantSession);
            assistantSession.SwitchTo().Window(assistantSession.WindowHandles.First());
            TestHelper.SendDate(calendarEntry.CalendarDay, dateInput);
            AssertAssistantListEntry(assistantSession, TestHelper.GetDateString(calendarEntry.CalendarDay), AssistantStringHelper.WorkingCalendar.Date);
            
            var assistantNextBtn = TestHelper.FindElementWithWait(AiStringHelper.Assistant.Next, assistantSession);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(assistantSession, assistantNextBtn);
            assistantNextBtn.Click();

            if (entryOverwrites)
            {
                var okBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathOkButton, 1);
                Assert.IsNotNull(okBtn, "OK-Button zum Überschreiben wurde nicht gefunden");
                okBtn.Click();
            }

            var textInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputText, assistantSession);
            textInput.Clear();
            TestHelper.SendKeysConverted(textInput, calendarEntry.Description);
            AssertAssistantListEntry(assistantSession, calendarEntry.Description, AssistantStringHelper.WorkingCalendar.Description);
            assistantNextBtn.Click();

            var listInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputList, assistantSession);
            var repetitionListEntry = TestHelper.FindElementInListbox(calendarEntry.getRepetitionString(), listInput, AiStringHelper.By.Name);
            repetitionListEntry.Click();
            AssertAssistantListEntry(assistantSession, calendarEntry.getRepetitionString(), AssistantStringHelper.WorkingCalendar.Repetition);
            assistantNextBtn.Click();

            listInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputList, assistantSession);
            var typeListEntry = TestHelper.FindElementInListbox(calendarEntry.getDayTypeString(), listInput, AiStringHelper.By.Name);
            typeListEntry.Click();
            AssertAssistantListEntry(assistantSession, calendarEntry.getDayTypeString(), AssistantStringHelper.WorkingCalendar.Type);
            assistantNextBtn.Click();

            if (checkWeekend)
            {
                var workingCalendarTab = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTab);
                var workfreeDaySaturday = workingCalendarTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SaturdayWorkfree);
                var workfreeDaySunday = workingCalendarTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SundayWorkfree);

                if (!calendarEntry.IsAnnually && !calendarEntry.CalendarEntryIsWeekday() && (!calendarEntry.CalendarEntryIsValidSaturday(workfreeDaySaturday.Selected) || !calendarEntry.CalendarEntryIsValidSunday(workfreeDaySunday.Selected)))
                {
                    var okBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathOkButton, 1);
                    Assert.IsNotNull(okBtn, "OK-Button wegen Wochenende wurde nicht gefunden");
                    okBtn.Click();
                    var cancelBtn = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.Cancel);
                    cancelBtn.Click();

                    return;
                }
            }
            AssertProcessToolTestNotification();
            //wegen Bug werden Toast-Notification beim Überschreiben eines Eintrags 2mal aufgerufen 
            if (entryOverwrites)
            {
                AssertProcessToolTestNotification();
            }
        }
    
        private static AppiumWebElement GetWorkingCalendarRow(WindowsDriver<WindowsElement> QstSession, CalendarEntry calendarEntry)
        {
            var testPlanningMasterDataView = TestHelper.FindElementWithWait(AiStringHelper.TestPlanningMasterData.View, QstSession);
            var workingCalendarTab = testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.WorkingCalendarTab);

            if (calendarEntry.IsAnnually)
            {
                return TestHelper.GetDataGridRowWithHorizontalScrolling(QstSession, workingCalendarTab, AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.AnnuallyEntriesGrid, AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.AnnuallyGridHeader, AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.AnnuallyGridRowPrefix, calendarEntry.GetCalenderDateRowString(), CalendarEntry.CalendarEntryListHeaderStrings.CalendarDate);
            }
            else
            {
                return TestHelper.GetDataGridRowWithHorizontalScrolling(QstSession, workingCalendarTab, AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SingleEntriesGrid, AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SingleEntriesGridHeader, AiStringHelper.TestPlanningMasterData.WorkingCalendarTabElements.SingleEntriesGridRowPrefix, calendarEntry.GetCalenderDateRowString(), CalendarEntry.CalendarEntryListHeaderStrings.CalendarDate);
            }
        }
        public static AppiumWebElement GetShiftManagementTimePickerElements(WindowsDriver<WindowsElement> QstSession, ShiftManagement.ShiftManagementTimePickerElements option, ShiftManagementUiHelper shiftManagementUiHelper)
        {
            var shiftManagementTab = shiftManagementUiHelper.shiftManagementTab;

            var firstShiftFromText = shiftManagementUiHelper.firstShiftFromText;
            var firstShiftToText = shiftManagementUiHelper.firstShiftToText;
            var secondShiftFromText = shiftManagementUiHelper.secondShiftFromText;
            var secondShiftToText = shiftManagementUiHelper.secondShiftToText;
            var thirdShiftFromText = shiftManagementUiHelper.thirdShiftFromText;
            var thirdShiftToText = shiftManagementUiHelper.thirdShiftToText;
            var changeOfDayText = shiftManagementUiHelper.changeOfDayText;

            IReadOnlyCollection<AppiumWebElement> timepickerElements;

            if (option == ShiftManagement.ShiftManagementTimePickerElements.FirstShiftFromDropdown ||
                option == ShiftManagement.ShiftManagementTimePickerElements.FirstShiftToDropdown ||
                option == ShiftManagement.ShiftManagementTimePickerElements.SecondShiftFromDropdown ||
                option == ShiftManagement.ShiftManagementTimePickerElements.SecondShiftToDropdown ||
                option == ShiftManagement.ShiftManagementTimePickerElements.ThirdShiftFromDropdown ||
                option == ShiftManagement.ShiftManagementTimePickerElements.ThirdShiftToDropdown ||
                option == ShiftManagement.ShiftManagementTimePickerElements.ChangeDayDropdown)
            {
                //Alle TimePickerDropdowns finden über PART_DropDownButton
                timepickerElements = shiftManagementTab.FindElementsByAccessibilityId("PART_DropDownButton");
            }
            else
            {
                //Alle Bearbeiten-TextBlöcke zu den TimePickern finden über PART_TextBlock
                timepickerElements = shiftManagementTab.FindElementsByAccessibilityId("PART_TextBlock");
            }

            var tolerance = 10;
            List<AppiumWebElement> foundElements = new List<AppiumWebElement>();

            switch (option)
            {
                case ShiftManagement.ShiftManagementTimePickerElements.FirstShiftFromDropdown:
                    return FindElementWithSameHeight(timepickerElements, firstShiftFromText, firstShiftToText, tolerance, true);
                case ShiftManagement.ShiftManagementTimePickerElements.FirstShiftToDropdown:
                    return FindElementWithSameHeight(timepickerElements, firstShiftFromText, firstShiftToText, tolerance, false);
                case ShiftManagement.ShiftManagementTimePickerElements.SecondShiftFromDropdown:
                    return FindElementWithSameHeight(timepickerElements, secondShiftFromText, secondShiftToText, tolerance, true);
                case ShiftManagement.ShiftManagementTimePickerElements.SecondShiftToDropdown:
                    return FindElementWithSameHeight(timepickerElements, secondShiftFromText, secondShiftToText, tolerance, false);
                case ShiftManagement.ShiftManagementTimePickerElements.ThirdShiftFromDropdown:
                    return FindElementWithSameHeight(timepickerElements, thirdShiftFromText, thirdShiftToText, tolerance, true);
                case ShiftManagement.ShiftManagementTimePickerElements.ThirdShiftToDropdown:
                    return FindElementWithSameHeight(timepickerElements, thirdShiftFromText, thirdShiftToText, tolerance, false);
                case ShiftManagement.ShiftManagementTimePickerElements.ChangeDayDropdown:

                    foreach (var item in timepickerElements)
                    {
                        if (Math.Abs(item.Location.Y - changeOfDayText.Location.Y) < tolerance)
                        {
                            foundElements.Add(item);
                        }
                    }
                    Assert.AreEqual(1, foundElements.Count, string.Format("Es wurden {0} DropDownButton gefunden, es sollte einer sein", foundElements.Count));
                    return foundElements[0];
                case ShiftManagement.ShiftManagementTimePickerElements.FirstShiftFromTextBox:
                    return FindElementWithSameHeight(timepickerElements, firstShiftFromText, firstShiftToText, tolerance, true);
                case ShiftManagement.ShiftManagementTimePickerElements.FirstShiftToTextBox:
                    return FindElementWithSameHeight(timepickerElements, firstShiftFromText, firstShiftToText, tolerance, false);
                case ShiftManagement.ShiftManagementTimePickerElements.SecondShiftFromTextBox:
                    return FindElementWithSameHeight(timepickerElements, secondShiftFromText, secondShiftToText, tolerance, true);
                case ShiftManagement.ShiftManagementTimePickerElements.SecondShiftToTextBox:
                    return FindElementWithSameHeight(timepickerElements, secondShiftFromText, secondShiftToText, tolerance, false);
                case ShiftManagement.ShiftManagementTimePickerElements.ThirdShiftFromTextBox:
                    return FindElementWithSameHeight(timepickerElements, thirdShiftFromText, thirdShiftToText, tolerance, true);
                case ShiftManagement.ShiftManagementTimePickerElements.ThirdShiftToTextBox:
                    return FindElementWithSameHeight(timepickerElements, thirdShiftFromText, thirdShiftToText, tolerance, false);
                case ShiftManagement.ShiftManagementTimePickerElements.ChangeDayTextBox:
                    foreach (var item in timepickerElements)
                    {
                        if (Math.Abs(item.Location.Y - changeOfDayText.Location.Y) < tolerance)
                        {
                            foundElements.Add(item);
                        }
                    }
                    Assert.AreEqual(1, foundElements.Count, string.Format("Es wurden {0} DropDownButton gefunden, es sollte einer sein", foundElements.Count));
                    return foundElements[0];
                default:
                    Assert.IsTrue(false, "ShiftManagementTimeOption nicht vorhanden");
                    break;
            }
            return null;
        }
        public static AppiumWebElement FindElementWithSameHeight(IReadOnlyCollection<AppiumWebElement> givenList, AppiumWebElement element1, AppiumWebElement element2, int tolerance, bool isBetween)
        {
            var foundElements = new List<AppiumWebElement>();
            if (isBetween)
            {
                foreach (var item in givenList)
                {
                    if (Math.Abs(item.Location.Y - element1.Location.Y) < tolerance && item.Location.X < element2.Location.X)
                    {
                        foundElements.Add(item);
                        break;
                    }
                }
                Assert.AreEqual(1, foundElements.Count, string.Format("Es wurden {0} Elemente gefunden, es sollte einer sein", foundElements.Count));
                return foundElements[0];
            }
            else
            {
                foreach (var item in givenList)
                {
                    if (Math.Abs(item.Location.Y - element2.Location.Y) < tolerance && item.Location.X > element2.Location.X)
                    {
                        foundElements.Add(item);
                    }
                }
                Assert.AreEqual(1, foundElements.Count, string.Format("Es wurden {0} Elemente gefunden, es sollte einer sein", foundElements.Count));
                return foundElements[0];
            }
        }
        private static void NavigateToTestPlanningMasterDataTab(WindowsDriver<WindowsElement> QstSession, string tabAiString)
        {
            var testPlanningMasterDataView = TestHelper.FindElementWithWait(AiStringHelper.TestPlanningMasterData.View, QstSession);
            var TestPlanningMasterDataTab = testPlanningMasterDataView.FindElementByAccessibilityId(tabAiString);
            TestPlanningMasterDataTab.Click();
        }
        private static ShiftManagementUiHelper GetShiftManagementUiHelper(WindowsDriver<WindowsElement> QstSession)
        {
            return GetShiftManagementUiHelper(QstSession, new ShiftManagementUiHelper());
        }
        private static ShiftManagementUiHelper GetShiftManagementUiHelper(WindowsDriver<WindowsElement> QstSession, ShiftManagementUiHelper shiftManagementUiHelper)
        {
            if (shiftManagementUiHelper == null)
            {
                shiftManagementUiHelper = new ShiftManagementUiHelper();
            }
            shiftManagementUiHelper.testPlanningMasterDataView = TestHelper.FindElementWithWait(AiStringHelper.TestPlanningMasterData.View, QstSession);
            shiftManagementUiHelper.shiftManagementTab = shiftManagementUiHelper.testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.ShiftManagementTab);
            shiftManagementUiHelper.save = shiftManagementUiHelper.testPlanningMasterDataView.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.ShiftManagementTabElements.Save);


            shiftManagementUiHelper.firstShiftFromText = shiftManagementUiHelper.shiftManagementTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.ShiftManagementTabElements.FirstShiftFromText);
            shiftManagementUiHelper.firstShiftToText = shiftManagementUiHelper.shiftManagementTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.ShiftManagementTabElements.FirstShiftToText);
            shiftManagementUiHelper.secondShiftFromText = shiftManagementUiHelper.shiftManagementTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.ShiftManagementTabElements.SecondShiftFromText);
            shiftManagementUiHelper.secondShiftToText = shiftManagementUiHelper.shiftManagementTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.ShiftManagementTabElements.SecondShiftToText);
            shiftManagementUiHelper.thirdShiftFromText = shiftManagementUiHelper.shiftManagementTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.ShiftManagementTabElements.ThirdShiftFromText);
            shiftManagementUiHelper.thirdShiftToText = shiftManagementUiHelper.shiftManagementTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.ShiftManagementTabElements.ThirdShiftToText);
            shiftManagementUiHelper.changeOfDayText = shiftManagementUiHelper.shiftManagementTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.ShiftManagementTabElements.ChangeOfDayText);

            shiftManagementUiHelper.firstShiftFromDropdown = GetShiftManagementTimePickerElements(QstSession, ShiftManagement.ShiftManagementTimePickerElements.FirstShiftFromDropdown, shiftManagementUiHelper);
            shiftManagementUiHelper.firstShiftFromTextBox = GetShiftManagementTimePickerElements(QstSession, ShiftManagement.ShiftManagementTimePickerElements.FirstShiftFromTextBox, shiftManagementUiHelper);
            shiftManagementUiHelper.firstShiftToDropdown = GetShiftManagementTimePickerElements(QstSession, ShiftManagement.ShiftManagementTimePickerElements.FirstShiftToDropdown, shiftManagementUiHelper);
            shiftManagementUiHelper.firstShiftToTextBox = GetShiftManagementTimePickerElements(QstSession, ShiftManagement.ShiftManagementTimePickerElements.FirstShiftToTextBox, shiftManagementUiHelper);

            shiftManagementUiHelper.secondShiftActive = shiftManagementUiHelper.shiftManagementTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.ShiftManagementTabElements.SecondShiftActive);
            shiftManagementUiHelper.secondShiftFromDropdown = GetShiftManagementTimePickerElements(QstSession, ShiftManagement.ShiftManagementTimePickerElements.SecondShiftFromDropdown, shiftManagementUiHelper);
            shiftManagementUiHelper.secondShiftFromTextBox = GetShiftManagementTimePickerElements(QstSession, ShiftManagement.ShiftManagementTimePickerElements.SecondShiftFromTextBox, shiftManagementUiHelper);
            shiftManagementUiHelper.secondShiftToDropdown = GetShiftManagementTimePickerElements(QstSession, ShiftManagement.ShiftManagementTimePickerElements.SecondShiftToDropdown, shiftManagementUiHelper);
            shiftManagementUiHelper.secondShiftToTextBox = GetShiftManagementTimePickerElements(QstSession, ShiftManagement.ShiftManagementTimePickerElements.SecondShiftToTextBox, shiftManagementUiHelper);

            shiftManagementUiHelper.thirdShiftActive = shiftManagementUiHelper.shiftManagementTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.ShiftManagementTabElements.ThirdShiftActive);
            shiftManagementUiHelper.thirdShiftFromDropdown = GetShiftManagementTimePickerElements(QstSession, ShiftManagement.ShiftManagementTimePickerElements.ThirdShiftFromDropdown, shiftManagementUiHelper);
            shiftManagementUiHelper.thirdShiftFromTextBox = GetShiftManagementTimePickerElements(QstSession, ShiftManagement.ShiftManagementTimePickerElements.ThirdShiftFromTextBox, shiftManagementUiHelper);
            shiftManagementUiHelper.thirdShiftToDropdown = GetShiftManagementTimePickerElements(QstSession, ShiftManagement.ShiftManagementTimePickerElements.ThirdShiftToDropdown, shiftManagementUiHelper);
            shiftManagementUiHelper.thirdShiftToTextBox = GetShiftManagementTimePickerElements(QstSession, ShiftManagement.ShiftManagementTimePickerElements.ThirdShiftToTextBox, shiftManagementUiHelper);

            shiftManagementUiHelper.changeDayDropdown = GetShiftManagementTimePickerElements(QstSession, ShiftManagement.ShiftManagementTimePickerElements.ChangeDayDropdown, shiftManagementUiHelper);
            shiftManagementUiHelper.changeDayTextBox = GetShiftManagementTimePickerElements(QstSession, ShiftManagement.ShiftManagementTimePickerElements.ChangeDayTextBox, shiftManagementUiHelper);

            shiftManagementUiHelper.firstDayOfWeek = shiftManagementUiHelper.shiftManagementTab.FindElementByAccessibilityId(AiStringHelper.TestPlanningMasterData.ShiftManagementTabElements.FirstDayOfWeek);
            return shiftManagementUiHelper;
        }
        private static void AssertShiftManagement(WindowsDriver<WindowsElement> QstSession, ShiftManagement shiftManagement, ShiftManagementUiHelper shiftManagementUiHelper)
        {
            Assert.AreEqual(shiftManagement.FirstShiftFromTime.ToString("HH:mm"), shiftManagementUiHelper.firstShiftFromTextBox.Text, string.Format("Eingestellte Zeit ist falsch. Ist: {0}, Soll: {1}", shiftManagementUiHelper.firstShiftFromTextBox.Text, shiftManagement.FirstShiftFromTime.ToString("HH:mm")));
            Assert.AreEqual(shiftManagement.FirstShiftToTime.ToString("HH:mm"), shiftManagementUiHelper.firstShiftToTextBox.Text, string.Format("Eingestellte Zeit ist falsch. Ist: {0}, Soll: {1}", shiftManagementUiHelper.firstShiftToTextBox.Text, shiftManagement.FirstShiftToTime.ToString("HH:mm")));

            Assert.AreEqual(shiftManagement.IsSecondShiftActive, shiftManagementUiHelper.secondShiftActive.Selected, string.Format("Schicht 2 Aktiv: {0}", shiftManagementUiHelper.secondShiftActive.Selected));
            if (shiftManagement.IsSecondShiftActive)
            {
                Assert.AreEqual(shiftManagement.SecondShiftFromTime.ToString("HH:mm"), shiftManagementUiHelper.secondShiftFromTextBox.Text, string.Format("Eingestellte Zeit ist falsch. Ist: {0}, Soll: {1}", shiftManagementUiHelper.secondShiftFromTextBox.Text, shiftManagement.SecondShiftFromTime.ToString("HH:mm")));
                Assert.AreEqual(shiftManagement.SecondShiftToTime.ToString("HH:mm"), shiftManagementUiHelper.secondShiftToTextBox.Text, string.Format("Eingestellte Zeit ist falsch. Ist: {0}, Soll: {1}", shiftManagementUiHelper.secondShiftToTextBox.Text, shiftManagement.SecondShiftToTime.ToString("HH:mm")));
            }
            else
            {
                Assert.IsFalse(shiftManagementUiHelper.secondShiftFromDropdown.Enabled);
                Assert.IsFalse(shiftManagementUiHelper.secondShiftFromTextBox.Enabled);
                Assert.IsFalse(shiftManagementUiHelper.secondShiftToDropdown.Enabled);
                Assert.IsFalse(shiftManagementUiHelper.secondShiftToTextBox.Enabled);
            }

            Assert.AreEqual(shiftManagement.IsThirdShiftActive, shiftManagementUiHelper.thirdShiftActive.Selected, string.Format("Schicht 3 Aktiv: {0}", shiftManagementUiHelper.thirdShiftActive.Selected));
            if (shiftManagement.IsThirdShiftActive)
            {
                Assert.AreEqual(shiftManagement.ThirdShiftFromTime.ToString("HH:mm"), shiftManagementUiHelper.thirdShiftFromTextBox.Text, string.Format("Eingestellte Zeit ist falsch. Ist: {0}, Soll: {1}", shiftManagementUiHelper.thirdShiftFromTextBox.Text, shiftManagement.ThirdShiftFromTime.ToString("HH:mm")));
                Assert.AreEqual(shiftManagement.ThirdShiftFromTime.ToString("HH:mm"), shiftManagementUiHelper.thirdShiftFromTextBox.Text, string.Format("Eingestellte Zeit ist falsch. Ist: {0}, Soll: {1}", shiftManagementUiHelper.thirdShiftFromTextBox.Text, shiftManagement.ThirdShiftFromTime.ToString("HH:mm")));
            }
            else
            {
                Assert.IsFalse(shiftManagementUiHelper.thirdShiftFromDropdown.Enabled);
                Assert.IsFalse(shiftManagementUiHelper.thirdShiftFromTextBox.Enabled);
                Assert.IsFalse(shiftManagementUiHelper.thirdShiftToDropdown.Enabled);
                Assert.IsFalse(shiftManagementUiHelper.thirdShiftToTextBox.Enabled);
            }

            Assert.AreEqual(shiftManagement.ChangeDayTime.ToString("HH:mm"), shiftManagementUiHelper.changeDayTextBox.Text, string.Format("Eingestellte Zeit ist falsch. Ist: {0}, Soll: {1}", shiftManagementUiHelper.changeDayTextBox.Text, shiftManagement.ChangeDayTime.ToString("HH:mm")));
            Assert.AreEqual(shiftManagement.FirstDayOfWeekDay.ToString(), TestHelper.GetSelectedComboboxString(QstSession, shiftManagementUiHelper.firstDayOfWeek));
        }
        private static void UpdateShiftManagement(WindowsDriver<WindowsElement> QstSession, ShiftManagement shiftManagement, ShiftManagementUiHelper shiftManagementUiHelper)
        {
            TestHelper.SetTimeTimePicker(QstSession, shiftManagementUiHelper.firstShiftFromDropdown, shiftManagement.FirstShiftFromTime);
            TestHelper.SetTimeTimePicker(QstSession, shiftManagementUiHelper.firstShiftToDropdown, shiftManagement.FirstShiftToTime);

            TestHelper.SetCheckbox(shiftManagementUiHelper.secondShiftActive, shiftManagement.IsSecondShiftActive);
            if (shiftManagement.IsSecondShiftActive)
            {
                TestHelper.SetTimeTimePicker(QstSession, shiftManagementUiHelper.secondShiftFromDropdown, shiftManagement.SecondShiftFromTime);
                TestHelper.SetTimeTimePicker(QstSession, shiftManagementUiHelper.secondShiftToDropdown, shiftManagement.SecondShiftToTime);
            }

            TestHelper.SetCheckbox(shiftManagementUiHelper.thirdShiftActive, shiftManagement.IsThirdShiftActive);
            if (shiftManagement.IsThirdShiftActive)
            {
                TestHelper.SetTimeTimePicker(QstSession, shiftManagementUiHelper.thirdShiftFromDropdown, shiftManagement.ThirdShiftFromTime);
                TestHelper.SetTimeTimePicker(QstSession, shiftManagementUiHelper.thirdShiftToDropdown, shiftManagement.ThirdShiftToTime);
            }

            TestHelper.SetTimeTimePicker(QstSession, shiftManagementUiHelper.changeDayDropdown, shiftManagement.ChangeDayTime);

            TestHelper.ClickComboBoxEntry(QstSession, shiftManagement.FirstDayOfWeekDay.ToString(), shiftManagementUiHelper.firstDayOfWeek);
        }
        private static void VerifyShiftManagementChangesInVerifyView(WindowsDriver<WindowsElement> driver, ShiftManagement pc, ShiftManagement pcChanged, AppiumWebElement listViewChanges, int numberOfExpectedChanges)
        {
            var groupChanges = listViewChanges.FindElementByClassName("GroupItem");
            var changes = groupChanges.FindElementsByClassName("ListViewItem");
            int i = 1;
            Assert.AreEqual(numberOfExpectedChanges, changes.Count);
            foreach (AppiumWebElement item in changes)
            {
                string changeTypeText = item.FindElementsByClassName("TextBlock")[1].Text;
                switch (changeTypeText)
                {
                    //TODO correct cases and finish ShiftManagement
                    case "Start of first shift":
                        Assert.AreEqual(TestHelper.GetTimeString(pc.FirstShiftFromTime), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetTimeString(pcChanged.FirstShiftFromTime), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "End of first shift":
                        Assert.AreEqual(TestHelper.GetTimeString(pc.FirstShiftToTime), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetTimeString(pcChanged.FirstShiftToTime), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Start of second shift":
                        Assert.AreEqual(TestHelper.GetTimeString(pc.SecondShiftFromTime), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetTimeString(pcChanged.SecondShiftFromTime), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "End of second shift":
                        Assert.AreEqual(TestHelper.GetTimeString(pc.SecondShiftToTime), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetTimeString(pcChanged.SecondShiftToTime), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Start of third shift":
                        Assert.AreEqual(TestHelper.GetTimeString(pc.ThirdShiftFromTime), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetTimeString(pcChanged.ThirdShiftFromTime), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "End of third shift":
                        Assert.AreEqual(TestHelper.GetTimeString(pc.ThirdShiftToTime), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetTimeString(pcChanged.ThirdShiftToTime), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Is second shift active":
                        Assert.AreEqual(pc.GetSecondShiftIsActiveString(), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.GetSecondShiftIsActiveString(), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Is third shift active":
                        Assert.AreEqual(pc.GetThirdShiftIsActiveString(), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.GetThirdShiftIsActiveString(), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Change day":
                        Assert.AreEqual(TestHelper.GetTimeString(pc.ChangeDayTime), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetTimeString(pcChanged.ChangeDayTime), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "First day of the week":
                        Assert.AreEqual(pc.FirstDayOfWeekDay.ToString(), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.FirstDayOfWeekDay.ToString(), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    default:
                        Assert.IsTrue(false, string.Format("Case '{0}' not implemented", changeTypeText));
                        break;
                }
                i++;
            }
        }
        private static void AssertProcessToolTestNotification()
        {
            var calcProcessTestNotificationFound = false;
            var calcToolTestNotificationFound = false;
            Thread.Sleep(1200);
            var toastnotification = TestHelper.FindElementWithWait(AiStringHelper.ToastNotification.Toast, DesktSession);
            if (toastnotification != null)
            {
                var sender = toastnotification.FindElementByAccessibilityId(AiStringHelper.ToastNotification.SenderName);
                var text = toastnotification.FindElementByAccessibilityId(AiStringHelper.ToastNotification.Text);
                var closeBtn = toastnotification.FindElementByAccessibilityId(AiStringHelper.ToastNotification.CloseButton);
                Assert.AreEqual(ValidationStringHelper.ToastNotificationStrings.SenderQST, sender.Text);

                calcProcessTestNotificationFound = ValidationStringHelper.ToastNotificationStrings.ProcessTestsCalcSuccess == text.Text;
                calcToolTestNotificationFound = ValidationStringHelper.ToastNotificationStrings.ToolTestsCalcSuccess == text.Text;

                Assert.IsTrue(calcProcessTestNotificationFound || calcToolTestNotificationFound);
                closeBtn.Click();
            }
            Thread.Sleep(1200);
            toastnotification = TestHelper.FindElementWithWait(AiStringHelper.ToastNotification.Toast, DesktSession);
            if (toastnotification != null)
            {
                var sender = toastnotification.FindElementByAccessibilityId(AiStringHelper.ToastNotification.SenderName);
                var text = toastnotification.FindElementByAccessibilityId(AiStringHelper.ToastNotification.Text);
                var closeBtn = toastnotification.FindElementByAccessibilityId(AiStringHelper.ToastNotification.CloseButton);
                Assert.AreEqual(ValidationStringHelper.ToastNotificationStrings.SenderQST, sender.Text);

                if (calcProcessTestNotificationFound)
                {
                    Assert.AreEqual(ValidationStringHelper.ToastNotificationStrings.ToolTestsCalcSuccess, text.Text);
                }
                else
                {
                    if (calcToolTestNotificationFound)
                    {
                        Assert.AreEqual(ValidationStringHelper.ToastNotificationStrings.ProcessTestsCalcSuccess, text.Text);
                    }
                    else
                    {
                        Assert.IsTrue(false, "Falscher Toast-Text wird angezeigt");
                    }
                }
                closeBtn.Click();
            }
        }
    }
}