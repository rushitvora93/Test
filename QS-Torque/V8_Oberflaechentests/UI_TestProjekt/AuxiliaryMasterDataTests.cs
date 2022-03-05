using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using UI_TestProjekt.Helper;
using UI_TestProjekt.TestModel;

namespace UI_TestProjekt
{
    [TestClass]
    public class AuxiliaryMasterDataTests : TestBase
    {
        [TestMethod]
        [TestCategory("AuxiliaryMasterData")]
        public void TestManufacturer()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            //Auf Herstellerseite wechseln
            var globalTree = QstSession.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);

            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);
            var auxiliaryButton = globalTree.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.AuxiliaryMasterData);

            NavigateToAuxiliarySubmenu(QstSession, globalTree, auxiliaryButton, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.Manufacturer);

            //Cleanup falls noch Überreste von früheren Tests vorhanden sind

            //SendKeys sendet Zeichen anhand von US-Tastatur darum z statt y
            Manufacturer manufacturer1 = Testdata.GetManufacturer1();
            Manufacturer manufacturer2 = Testdata.GetManufacturer2();

            Manufacturer changedManufacturer1 = Testdata.GetManufacturerChanged1();
            Manufacturer changedManufacturer2 = Testdata.GetManufacturerChanged2();
            Thread.Sleep(200);
            var manuView = QstSession.FindElementByAccessibilityId(AiStringHelper.Manufacturer.View);
            var manuListbox = manuView.FindElementByAccessibilityId(AiStringHelper.Manufacturer.ManufacturerListBox);

            AppiumWebElement deleteListEntry = TestHelper.FindElementInListbox(manufacturer1.Name, manuListbox);
            DeleteManufacturer(manuView, deleteListEntry);

            deleteListEntry = TestHelper.FindElementInListbox(manufacturer2.Name, manuListbox);
            DeleteManufacturer(manuView, deleteListEntry);

            deleteListEntry = TestHelper.FindElementInListbox(changedManufacturer1.Name, manuListbox);
            DeleteManufacturer(manuView, deleteListEntry);

            deleteListEntry = TestHelper.FindElementInListbox(changedManufacturer2.Name, manuListbox);
            DeleteManufacturer(manuView, deleteListEntry);

            // Start eigentlicher Test
            // 2 Hersteller anlegen
            ManufacturerUIHelper manuUiHelper = GetManuUiHelperWithElements(manuView, QstSession);

            CreateManufacturer(QstSession, manufacturer1, manuUiHelper);
            manuView = QstSession.FindElementByAccessibilityId(AiStringHelper.Manufacturer.View);
            var saveManuBtn = manuView.FindElementByAccessibilityId(AiStringHelper.Manufacturer.SaveManufacturer);
            saveManuBtn.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

            CreateManufacturer(QstSession, manufacturer2, manuUiHelper);
            saveManuBtn.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

            // Daten prüfen
            var manu1ListEntry = TestHelper.FindElementInListbox(manufacturer1.Name, manuListbox);
            manu1ListEntry.Click();
            AssertSingleManufacturer(manuUiHelper, manufacturer1);

            var manu2ListEntry = TestHelper.FindElementInListbox(manufacturer2.Name, manuListbox);
            manu2ListEntry.Click();
            AssertSingleManufacturer(manuUiHelper, manufacturer2);

            Actions actions = new Actions(QstSession);
            actions.Click(manu1ListEntry);
            actions.KeyDown(Keys.Control);
            actions.Click(manu2ListEntry);
            actions.KeyUp(Keys.Control);
            actions.Build();
            actions.Perform();

            //neue Hersteller in Grid prüfen
            AssertListManufacturer(QstSession, manuView, manufacturer1);
            AssertListManufacturer(QstSession, manuView, manufacturer2);

            //Hersteller ändern
            manu1ListEntry.Click();
            manuUiHelper = GetManuUiHelperWithElements(manuView, QstSession);
            UpdateManufacturer(QstSession, changedManufacturer1, manuUiHelper);
            //Änderung speichern nach Rückfrage 
            manu2ListEntry.Click();
            var confirmBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
            confirmBtn.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

            UpdateManufacturer(QstSession, changedManufacturer2, manuUiHelper);
            saveManuBtn.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

            // Daten prüfen
            manu1ListEntry = TestHelper.FindElementInListbox(changedManufacturer1.Name, manuListbox);
            manu1ListEntry.Click();
            AssertSingleManufacturer(manuUiHelper, changedManufacturer1);

            manu2ListEntry = TestHelper.FindElementInListbox(changedManufacturer2.Name, manuListbox);
            manu2ListEntry.Click();
            AssertSingleManufacturer(manuUiHelper, changedManufacturer2);

            actions = new Actions(QstSession);
            actions.Click(manu1ListEntry);
            actions.KeyDown(Keys.Control);
            actions.Click(manu2ListEntry);
            actions.KeyUp(Keys.Control);
            actions.Build();
            actions.Perform();

            //geänderte Hersteller in Grid prüfen
            AssertListManufacturer(QstSession, manuView, changedManufacturer1);
            AssertListManufacturer(QstSession, manuView, changedManufacturer2);

            //geänderte Hersteller Löschen
            DeleteManufacturer(manuView, manu1ListEntry);
            DeleteManufacturer(manuView, manu2ListEntry);

            //Prüfen ob Hersteller noch in der Liste sind
            var manu1Entry = TestHelper.FindElementInListbox(changedManufacturer1.Name, manuListbox);
            Assert.IsNull(manu1Entry, "{0} wurde nicht gelöscht", changedManufacturer1.Name);

            var manu2Entry = TestHelper.FindElementInListbox(changedManufacturer2.Name, manuListbox);
            Assert.IsNull(manu2Entry, "{0} wurde nicht gelöscht", changedManufacturer2.Name);

            //Herstellerseite neu Öffnen und Liste nochmal prüfen
            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);

            NavigateToAuxiliarySubmenu(QstSession, globalTree, auxiliaryButton, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.Manufacturer);

            //Nach Neuladen der Seite Controls neu befüllen
            manuView = QstSession.FindElementByAccessibilityId(AiStringHelper.Manufacturer.View);
            manuListbox = manuView.FindElementByAccessibilityId(AiStringHelper.Manufacturer.ManufacturerListBox);

            manu1Entry = TestHelper.FindElementInListbox(changedManufacturer1.Name, manuListbox);
            Assert.IsNull(manu1Entry, "{0} wurde nicht gelöscht", changedManufacturer1.Name);

            manu2Entry = TestHelper.FindElementInListbox(changedManufacturer2.Name, manuListbox);
            Assert.IsNull(manu2Entry, "{0} wurde nicht gelöscht", changedManufacturer2.Name);
        }

        [TestMethod]
        [TestCategory("AuxiliaryMasterData")]
        public void TestToleranceClass()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            //Auf Seite wechseln
            var globalTree = QstSession.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);

            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);
            var auxiliaryButton = globalTree.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.AuxiliaryMasterData);

            NavigateToAuxiliarySubmenu(QstSession, globalTree, auxiliaryButton, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ToleranceClass);

            //Cleanup falls noch Überreste von früheren Tests vorhanden sind
            ToleranceClass toleranceClass1 = Testdata.GetToleranceClass1();
            ToleranceClass toleranceClass2 = Testdata.GetToleranceClass2();
            ToleranceClass toleranceClass3 = Testdata.GetToleranceClass3();
            ToleranceClass toleranceClass4 = Testdata.GetToleranceClass4();
            ToleranceClass toleranceClass1Changed = Testdata.GetToleranceClassChanged1();
            ToleranceClass toleranceClass2Changed = Testdata.GetToleranceClassChanged2();

            var toleranceClassView = TestHelper.FindElementWithWait(AiStringHelper.ToleranceClass.View, QstSession);

            DeleteToleranceclass(QstSession, toleranceClassView, toleranceClass1);
            DeleteToleranceclass(QstSession, toleranceClassView, toleranceClass2);
            DeleteToleranceclass(QstSession, toleranceClassView, toleranceClass3);
            DeleteToleranceclass(QstSession, toleranceClassView, toleranceClass4);
            DeleteToleranceclass(QstSession, toleranceClassView, toleranceClass1Changed);
            DeleteToleranceclass(QstSession, toleranceClassView, toleranceClass2Changed);

            //Toleranzklassen anlegen
            var toleranceClassName = toleranceClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.Name);

            CreateToleranceClass(toleranceClass1, QstSession);
            CreateToleranceClass(toleranceClass2, QstSession);
            CreateToleranceClass(toleranceClass3, QstSession);
            CreateToleranceClass(toleranceClass4, QstSession);

            //Daten prüfen
            var toleranceClassListbox = toleranceClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.ListBox);
            var toleranceClass1ListEntry = TestHelper.FindElementInListbox(toleranceClass1.Name, toleranceClassListbox, AiStringHelper.By.Name);
            toleranceClass1ListEntry.Click();
            TestHelper.WaitforElementTextLoaded(toleranceClassName, toleranceClass1.Name);
            AssertToleranceClass(QstSession, toleranceClass1);

            var toleranceClass2ListEntry = TestHelper.FindElementInListbox(toleranceClass2.Name, toleranceClassListbox, AiStringHelper.By.Name);
            toleranceClass2ListEntry.Click();
            TestHelper.WaitforElementTextLoaded(toleranceClassName, toleranceClass2.Name);
            AssertToleranceClass(QstSession, toleranceClass2);

            var toleranceClass3ListEntry = TestHelper.FindElementInListbox(toleranceClass3.Name, toleranceClassListbox, AiStringHelper.By.Name);
            toleranceClass3ListEntry.Click();
            TestHelper.WaitforElementTextLoaded(toleranceClassName, toleranceClass3.Name);
            AssertToleranceClass(QstSession, toleranceClass3);

            var toleranceClass4ListEntry = TestHelper.FindElementInListbox(toleranceClass4.Name, toleranceClassListbox, AiStringHelper.By.Name);
            toleranceClass4ListEntry.Click();
            TestHelper.WaitforElementTextLoaded(toleranceClassName, toleranceClass4.Name);
            AssertToleranceClass(QstSession, toleranceClass4);

            //Toleranzklassen ändern
            toleranceClass1ListEntry.Click();
            UpdateToleranceClass(QstSession, toleranceClass1Changed);

            toleranceClass2ListEntry.Click();
            UpdateToleranceClass(QstSession, toleranceClass2Changed);

            //geänderte Daten prüfen
            var toleranceClass1ChangedListEntry = TestHelper.FindElementInListbox(toleranceClass1Changed.Name, toleranceClassListbox, AiStringHelper.By.Name);
            toleranceClass1ChangedListEntry.Click();
            TestHelper.WaitforElementTextLoaded(toleranceClassName, toleranceClass1Changed.Name);
            AssertToleranceClass(QstSession, toleranceClass1Changed);

            var toleranceClass2ChangedListEntry = TestHelper.FindElementInListbox(toleranceClass2Changed.Name, toleranceClassListbox, AiStringHelper.By.Name);
            toleranceClass2ChangedListEntry.Click();
            TestHelper.WaitforElementTextLoaded(toleranceClassName, toleranceClass2Changed.Name);
            AssertToleranceClass(QstSession, toleranceClass2Changed);

            //Toleranzklassen Löschen
            toleranceClassView = TestHelper.FindElementWithWait(AiStringHelper.ToleranceClass.View, QstSession);
            DeleteToleranceclass(QstSession, toleranceClassView, toleranceClass1Changed);
            DeleteToleranceclass(QstSession, toleranceClassView, toleranceClass2Changed);
            DeleteToleranceclass(QstSession, toleranceClassView, toleranceClass3);
            DeleteToleranceclass(QstSession, toleranceClassView, toleranceClass4);

            //Prüfen ob Hilfstabelleneintrag noch in der Liste sind
            toleranceClass1ListEntry = TestHelper.FindElementInListbox(toleranceClass1.Name, toleranceClassListbox, AiStringHelper.By.Name);
            Assert.IsNull(toleranceClass1ListEntry, string.Format("{0} wurde nicht gelöscht", toleranceClass1.Name));

            toleranceClass2ListEntry = TestHelper.FindElementInListbox(toleranceClass2.Name, toleranceClassListbox, AiStringHelper.By.Name);
            Assert.IsNull(toleranceClass2ListEntry, string.Format("{0} wurde nicht gelöscht", toleranceClass2.Name));

            toleranceClass3ListEntry = TestHelper.FindElementInListbox(toleranceClass3.Name, toleranceClassListbox, AiStringHelper.By.Name);
            Assert.IsNull(toleranceClass3ListEntry, string.Format("{0} wurde nicht gelöscht", toleranceClass3.Name));

            toleranceClass4ListEntry = TestHelper.FindElementInListbox(toleranceClass4.Name, toleranceClassListbox, AiStringHelper.By.Name);
            Assert.IsNull(toleranceClass4ListEntry, string.Format("{0} wurde nicht gelöscht", toleranceClass4.Name));

            toleranceClass1ChangedListEntry = TestHelper.FindElementInListbox(toleranceClass1Changed.Name, toleranceClassListbox, AiStringHelper.By.Name);
            Assert.IsNull(toleranceClass1ChangedListEntry, string.Format("{0} wurde nicht gelöscht", toleranceClass1Changed.Name));

            toleranceClass2ChangedListEntry = TestHelper.FindElementInListbox(toleranceClass2Changed.Name, toleranceClassListbox, AiStringHelper.By.Name);
            Assert.IsNull(toleranceClass2ChangedListEntry, string.Format("{0} wurde nicht gelöscht", toleranceClass2Changed.Name));

            //Hilfstabelleneintrag neu Öffnen und Liste nochmal prüfen
            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);
            NavigateToAuxiliarySubmenu(QstSession, globalTree, auxiliaryButton, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ToleranceClass);

            toleranceClassView = TestHelper.FindElementWithWait(AiStringHelper.ToleranceClass.View, QstSession);
            toleranceClassListbox = toleranceClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.ListBox);

            //Prüfen ob Hilfstabelleneintrag noch in der Liste sind
            toleranceClass1ListEntry = TestHelper.FindElementInListbox(toleranceClass1.Name, toleranceClassListbox, AiStringHelper.By.Name);
            Assert.IsNull(toleranceClass1ListEntry, string.Format("{0} wurde nicht gelöscht", toleranceClass1.Name));

            toleranceClass2ListEntry = TestHelper.FindElementInListbox(toleranceClass2.Name, toleranceClassListbox, AiStringHelper.By.Name);
            Assert.IsNull(toleranceClass2ListEntry, string.Format("{0} wurde nicht gelöscht", toleranceClass2.Name));

            toleranceClass3ListEntry = TestHelper.FindElementInListbox(toleranceClass3.Name, toleranceClassListbox, AiStringHelper.By.Name);
            Assert.IsNull(toleranceClass3ListEntry, string.Format("{0} wurde nicht gelöscht", toleranceClass3.Name));

            toleranceClass4ListEntry = TestHelper.FindElementInListbox(toleranceClass4.Name, toleranceClassListbox, AiStringHelper.By.Name);
            Assert.IsNull(toleranceClass4ListEntry, string.Format("{0} wurde nicht gelöscht", toleranceClass4.Name));

            toleranceClass1ChangedListEntry = TestHelper.FindElementInListbox(toleranceClass1Changed.Name, toleranceClassListbox, AiStringHelper.By.Name);
            Assert.IsNull(toleranceClass1ChangedListEntry, string.Format("{0} wurde nicht gelöscht", toleranceClass1Changed.Name));

            toleranceClass2ChangedListEntry = TestHelper.FindElementInListbox(toleranceClass2Changed.Name, toleranceClassListbox, AiStringHelper.By.Name);
            Assert.IsNull(toleranceClass2ChangedListEntry, string.Format("{0} wurde nicht gelöscht", toleranceClass2Changed.Name));
        }

        [TestMethod]
        [TestCategory("AuxiliaryMasterData")]
        public void TestShutOff()
        {
            var helperMenuName = AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ShutOff;
            string helper1 = Testdata.ShutOff1;
            string helper2 = Testdata.ShutOff2;
            string addHelper1 = Testdata.ShutOffAddHelper1;
            string changedHelper2 = Testdata.ShutOffChangedHelper2;

            TestAuxiliaryMasterData(helperMenuName, helper1, helper2, addHelper1, changedHelper2);
        }

        [TestMethod]
        [TestCategory("AuxiliaryMasterData")]
        public void TestSwitchOff()
        {
            var helperMenuName = AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.SwitchOff;
            string helper1 = Testdata.SwitchOff1;
            string helper2 = Testdata.SwitchOff2;
            string addHelper1 = Testdata.SwitchOffAddHelper1;
            string changedHelper2 = Testdata.SwitchOffChangedHelper2;

            TestAuxiliaryMasterData(helperMenuName, helper1, helper2, addHelper1, changedHelper2);
        }

        [TestMethod]
        [TestCategory("AuxiliaryMasterData")]
        public void TestDriveSize()
        {
            var helperMenuName = AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.DriveSize;
            string helper1 = Testdata.DriveSize1;
            string helper2 = Testdata.DriveSize2;
            string addHelper1 = Testdata.DriveSizeAddHelper1;
            string changedHelper2 = Testdata.DriveSizeChangedHelper2;

            TestAuxiliaryMasterData(helperMenuName, helper1, helper2, addHelper1, changedHelper2);
        }

        [TestMethod]
        [TestCategory("AuxiliaryMasterData")]
        public void TestDriveType()
        {
            var helperMenuName = AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.DriveType;
            string helper1 = Testdata.DriveType1;
            string helper2 = Testdata.DriveType2;
            string addHelper1 = Testdata.DriveTypeAddHelper1;
            string changedHelper2 = Testdata.DriveTypeChangedHelper2;

            TestAuxiliaryMasterData(helperMenuName, helper1, helper2, addHelper1, changedHelper2);
        }

        [TestMethod]
        [TestCategory("AuxiliaryMasterData")]
        public void TestToolType()
        {
            var helperMenuName = AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ToolType;
            string helper1 = Testdata.ToolType1;
            string helper2 = Testdata.ToolType2;
            string addHelper1 = Testdata.ToolTypeAddHelper1;
            string changedHelper2 = Testdata.ToolTypeChangedHelper2;

            TestAuxiliaryMasterData(helperMenuName, helper1, helper2, addHelper1, changedHelper2);
        }

        [TestMethod]
        [TestCategory("AuxiliaryMasterData")]
        public void TestConstructionType()
        {
            var helperMenuName = AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ConstructionType;
            string helper1 = Testdata.ConstructionType1;
            string helper2 = Testdata.ConstructionType2;
            string addHelper1 = Testdata.ConstructionTypeAddHelper1;
            string changedHelper2 = Testdata.ConstructionTypeChangedHelper2;

            TestAuxiliaryMasterData(helperMenuName, helper1, helper2, addHelper1, changedHelper2);
        }

        [TestMethod]
        [TestCategory("AuxiliaryMasterData")]
        public void TestStatus()
        {
            var helperMenuName = AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.Status;
            string helper1 = Testdata.Status1;
            string helper2 = Testdata.Status2;
            string addHelper1 = Testdata.StatusAddHelper1;
            string changedHelper2 = Testdata.StatusChangedHelper2;

            TestAuxiliaryMasterData(helperMenuName, helper1, helper2, addHelper1, changedHelper2);
        }

        [TestMethod]
        [TestCategory("AuxiliaryMasterData")]
        public void TestReasonToolChange()
        {
            var helperMenuName = AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ReasonToolChange;
            string helper1 = Testdata.ReasonToolChange1;
            string helper2 = Testdata.ReasonToolChange2;
            string addHelper1 = Testdata.ReasonToolChangeAddHelper1;
            string changedHelper2 = Testdata.ReasonToolChangeChangedHelper2;

            TestAuxiliaryMasterData(helperMenuName, helper1, helper2, addHelper1, changedHelper2);
        }

        [TestMethod]
        [TestCategory("AuxiliaryMasterData")]
        public void TestConfigurableFieldTool()
        {
            var helperMenuName = AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ConfigurableFieldTool;
            string helper1 = Testdata.ConfigFieldTool1;
            string helper2 = Testdata.ConfigFieldTool2;
            string addHelper1 = Testdata.ConfigFieldToolAddHelper1;
            string changedHelper2 = Testdata.ConfigFieldToolChangedHelper2;

            TestAuxiliaryMasterData(helperMenuName, helper1, helper2, addHelper1, changedHelper2);
        }

        [TestMethod]
        [TestCategory("AuxiliaryMasterData")]
        public void TestCostCenter()
        {
            var helperMenuName = AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.CostCenter;
            string helper1 = Testdata.CostCenter1;
            string helper2 = Testdata.CostCenter2;
            string addHelper1 = Testdata.CostCenterAddHelper1;
            string changedHelper2 = Testdata.CostCenterChangedHelper2;

            TestAuxiliaryMasterData(helperMenuName, helper1, helper2, addHelper1, changedHelper2);
        }

        [TestMethod]
        [TestCategory("AuxiliaryMasterData")]
        public void TestToolUsage()
        {
            var helperMenuName = AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ToolUsage;
            string helper1 = Testdata.ToolUsage1;
            string helper2 = Testdata.ToolUsage2;
            string addHelper1 = Testdata.ToolUsageAddHelper1;
            string changedHelper2 = Testdata.ToolUsageChangedHelper2;

            TestAuxiliaryMasterData(helperMenuName, helper1, helper2, addHelper1, changedHelper2);
        }



        //Einfacher Testfall Anlegen/Ändern/Löschen für Hilfstabellen (ohne Berücksichtigung der Referenztabellen
        private static void TestAuxiliaryMasterData(string helperMenuName, string helper1, string helper2, string addHelper1, string changedHelper2)
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            //Auf Seite wechseln
            var globalTree = QstSession.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);

            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);
            var auxiliaryButton = globalTree.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.AuxiliaryMasterData);

            NavigateToAuxiliarySubmenu(QstSession, globalTree, auxiliaryButton, helperMenuName);

            //Cleanup falls noch Überreste von früheren Tests vorhanden sind


            var helperView = TestHelper.FindElementWithWait(AiStringHelper.HelperTable.View, QstSession);
            var helperListbox = helperView.FindElementByAccessibilityId(AiStringHelper.HelperTable.HelperListBox);

            AppiumWebElement deleteListEntry = TestHelper.FindElementInListbox(helper1, helperListbox);
            DeleteHelper(helperView, deleteListEntry);

            deleteListEntry = TestHelper.FindElementInListbox(helper2, helperListbox);
            DeleteHelper(helperView, deleteListEntry);

            deleteListEntry = TestHelper.FindElementInListbox(helper1 + addHelper1, helperListbox);
            DeleteHelper(helperView, deleteListEntry);

            deleteListEntry = TestHelper.FindElementInListbox(changedHelper2, helperListbox);
            DeleteHelper(helperView, deleteListEntry);

            //Hilfstabelleneintrag anlegen
            var addHelperBtn = helperView.FindElementByAccessibilityId(AiStringHelper.HelperTable.AddHelper);
            var saveHelperBtn = helperView.FindElementByAccessibilityId(AiStringHelper.HelperTable.SaveHelper);

            var helperName = helperView.FindElementByAccessibilityId(AiStringHelper.HelperTable.HelperInput);

            addHelperBtn.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            //Thread.Sleep(500); weil helperName auch enabled ist wenn noch alter Eintrag ausgewählt ist
            //Liste prüfen ob neuer Eintrag hinzugekomment ist funktioniert auch nicht da bei langer Liste nicht alle Einträge im dom sind
            Thread.Sleep(500);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, 5, 300, helperName);
            helperName.Clear();
            TestHelper.SendKeysConverted(helperName, helper1);

            saveHelperBtn.Click();
            var confirmBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
            confirmBtn.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

            addHelperBtn.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            //Thread.Sleep(500); weil helperName auch enabled ist wenn noch alter Eintrag ausgewählt ist
            //Liste prüfen ob neuer Eintrag hinzugekomment ist funktioniert auch nicht da bei langer Liste nicht alle Einträge im dom sind
            Thread.Sleep(500);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, 5, 300, helperName);
            helperName.Clear();
            TestHelper.SendKeysConverted(helperName, helper2);
            saveHelperBtn.Click();

            confirmBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
            confirmBtn.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

            //Daten prüfen
            var helper1ListEntry = TestHelper.FindElementInListbox(helper1, helperListbox);
            helper1ListEntry.Click();

            //Warten bis Daten geladen sind
            TestHelper.WaitforElementTextLoaded(helperName, helper1);

            Assert.AreEqual(helper1, helperName.Text);

            var helper2ListEntry = helperListbox.FindElementByAccessibilityId(helper2);
            helper2ListEntry.Click();

            //Warten bis Daten geladen sind
            TestHelper.WaitforElementTextLoaded(helperName, helper2);

            Assert.AreEqual(helper2, helperName.Text);

            //Hilfstabelleneintrag ändern
            helper1ListEntry.Click();
            TestHelper.WaitforElementTextLoaded(helperName, helper1);

            TestHelper.SendKeysConverted(helperName, Keys.End + addHelper1);
            helper2ListEntry.Click();
            confirmBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
            confirmBtn.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

            TestHelper.WaitforElementTextLoaded(helperName, helper2);
            helperName.Clear();
            TestHelper.SendKeysConverted(helperName, changedHelper2);
            saveHelperBtn.Click();

            confirmBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
            confirmBtn.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

            // geänderte Daten prüfen
            helperListbox = helperView.FindElementByAccessibilityId(AiStringHelper.HelperTable.HelperListBox);
            helper1ListEntry = TestHelper.FindElementInListbox(helper1 + addHelper1, helperListbox);
            helper1ListEntry.Click();

            //Warten bis Daten geladen sind
            TestHelper.WaitforElementTextLoaded(helperName, helper1 + addHelper1);

            Assert.AreEqual(helper1 + addHelper1, helperName.Text);

            helper2ListEntry = helperListbox.FindElementByAccessibilityId(changedHelper2);
            helper2ListEntry.Click();

            //Warten bis Daten geladen sind
            TestHelper.WaitforElementTextLoaded(helperName, changedHelper2);

            Assert.AreEqual(changedHelper2, helperName.Text);


            //Hilfstabelleneintrag Löschen
            helperListbox = helperView.FindElementByAccessibilityId(AiStringHelper.HelperTable.HelperListBox);
            helper1ListEntry = TestHelper.FindElementInListbox(helper1 + addHelper1, helperListbox);
            helper1ListEntry.Click();

            //Warten bis Daten geladen sind
            TestHelper.WaitforElementTextLoaded(helperName, helper1 + addHelper1);
            Assert.AreEqual(helper1 + addHelper1, helperName.Text);

            var delhelperBtn = helperView.FindElementByAccessibilityId(AiStringHelper.HelperTable.DeleteHelper);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, 5, 300, delhelperBtn);
            delhelperBtn.Click();

            confirmBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
            confirmBtn.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

            helperListbox = helperView.FindElementByAccessibilityId(AiStringHelper.HelperTable.HelperListBox);
            helper2ListEntry = TestHelper.FindElementInListbox(changedHelper2, helperListbox);
            helper2ListEntry.Click();

            //Warten bis Daten geladen sind
            TestHelper.WaitforElementTextLoaded(helperName, changedHelper2);
            Assert.AreEqual(changedHelper2, helperName.Text);

            helper2ListEntry.Click();
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, 5, 300, delhelperBtn);
            delhelperBtn.Click();

            //neuen Bestätigungsbutton finden
            confirmBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
            confirmBtn.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

            //Prüfen ob Hilfstabelleneintrag noch in der Liste sind
            helper1ListEntry = TestHelper.FindElementInListbox(helper1 + addHelper1, helperListbox);
            Assert.IsNull(helper1ListEntry, string.Format("{0} wurde nicht gelöscht", helper1 + addHelper1));

            helper2ListEntry = TestHelper.FindElementInListbox(changedHelper2, helperListbox);
            Assert.IsNull(helper2ListEntry, string.Format("{0} wurde nicht gelöscht", changedHelper2));

            //Hilfstabelleneintrag neu Öffnen und Liste nochmal prüfen
            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);

            NavigateToAuxiliarySubmenu(QstSession, globalTree, auxiliaryButton, helperMenuName);

            helperView = QstSession.FindElementByAccessibilityId(AiStringHelper.HelperTable.View);
            helperListbox = helperView.FindElementByAccessibilityId(AiStringHelper.HelperTable.HelperListBox);

            //Prüfen ob Hilfstabelleneintrag noch in der Liste sind
            helper1ListEntry = TestHelper.FindElementInListbox(helper1 + addHelper1, helperListbox);
            Assert.IsNull(helper1ListEntry, string.Format("{0} wurde nicht gelöscht", helper1 + addHelper1));

            helper2ListEntry = TestHelper.FindElementInListbox(changedHelper2, helperListbox);
            Assert.IsNull(helper2ListEntry, string.Format("{0} wurde nicht gelöscht", changedHelper2));
        }
        public static void AddHelper(string name, string strHelperTableName)
        {
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            //Auf Helperseite wechseln
            var globalTree = QstSession.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);

            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);
            var auxiliaryButton = globalTree.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.AuxiliaryMasterData);

            NavigateToAuxiliarySubmenu(QstSession, globalTree, auxiliaryButton, strHelperTableName);

            Thread.Sleep(200);
            CreateAndAssertHelper(name);
        }
        public static void AddHelpers(List<string> names, string strHelperTableName)
        {
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            //Auf Helperseite wechseln
            var globalTree = QstSession.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);
            
            NavigateToAuxiliarySubmenu(QstSession, globalTree, strHelperTableName);
            Thread.Sleep(200);

            foreach (var name in names)
            {
                CreateAndAssertHelper(name);
            }
        }
        private static void CreateAndAssertHelper(string name)
        {
            var helperView = QstSession.FindElementByAccessibilityId(AiStringHelper.HelperTable.View);
            var helperListbox = helperView.FindElementByAccessibilityId(AiStringHelper.HelperTable.HelperListBox);

            var addHelperBtn = helperView.FindElementByAccessibilityId(AiStringHelper.HelperTable.AddHelper);
            var saveHelperBtn = helperView.FindElementByAccessibilityId(AiStringHelper.HelperTable.SaveHelper);
            var helperName = helperView.FindElementByAccessibilityId(AiStringHelper.HelperTable.HelperInput);

            AppiumWebElement addEntry = TestHelper.FindElementInListbox(name, helperListbox);
            if (addEntry == null)
            {
                //Helper anlegen
                addHelperBtn.Click();
                TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
                TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, helperName);
                helperName.Clear();
                TestHelper.SendKeysConverted(helperName, name);
                saveHelperBtn.Click();

                var confirmBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
                confirmBtn.Click();
                TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

                // Daten prüfen
                var helperListEntry = TestHelper.FindElementInListbox(name, helperListbox);
                helperListEntry.Click();

                //Warten bis Daten geladen sind
                TestHelper.WaitforElementTextLoaded(helperName, name);

                Assert.AreEqual(name, helperName.Text);
            }
        }
        public static AppiumWebElement FindOrCreateHelperListEntryInAssistWindow(string name, WindowsDriver<WindowsElement> assistantSession, WindowsElement listInput)
        {
            var listEntry = TestHelper.TryFindElementBy(name, listInput);
            if (listEntry == null)
            {
                var jumpHelperTable = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.JumpHelperTable);
                jumpHelperTable.Click();
                Thread.Sleep(500);
                var helperTableView = DesktSession.FindElementByAccessibilityId(AiStringHelper.HelperTable.View);
                var helperListbox = helperTableView.FindElementByAccessibilityId(AiStringHelper.HelperTable.HelperListBox);

                // Helper anlegen
                var addHelperBtn = helperTableView.FindElementByAccessibilityId(AiStringHelper.HelperTable.AddHelper);
                var saveHelperBtn = helperTableView.FindElementByAccessibilityId(AiStringHelper.HelperTable.SaveHelper);

                var helperName = helperTableView.FindElementByAccessibilityId(AiStringHelper.HelperTable.HelperInput);

                addHelperBtn.Click();
                TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

                TestHelper.WaitForElementToBeEnabledAndDisplayed(assistantSession, helperName);
                helperName.Clear();
                TestHelper.SendKeysConverted(helperName, name);

                saveHelperBtn.Click();
                var confirmBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathAssistantConfirmButton);
                confirmBtn.Click();
                TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

                // Daten prüfen
                var helperListEntry = TestHelper.FindElementInListbox(name, helperListbox);
                helperListEntry.Click();

                //Warten bis Daten geladen sind
                TestHelper.WaitforElementTextLoaded(helperName, name);

                Assert.AreEqual(name, helperName.Text);
                var childWindows = assistantSession.FindElementsByClassName("Window");
                WindowsElement helperTableWindow = null;
                foreach (var window in childWindows)
                {
                    //Suche aus den zwei AssistentenFenster dass ohne Titel raus
                    //Ist nicht hübsch funktioniert aber
                    if (window.Text == "")
                    {
                        helperTableWindow = window;
                    }
                }
                //helperTable-Window wieder in den Vordergrund bringen
                assistantSession.SwitchTo().Window(assistantSession.WindowHandles.First());

                var helperTableTitleBar = helperTableWindow.FindElementByAccessibilityId("TitleBar");

                //AutomationId wird zwar in Inspect angezeigt aber der Button wird nicht gefunden 
                var helperTableCloseButton = TestHelper.TryFindElementBy("Close", helperTableTitleBar);
                helperTableCloseButton = TestHelper.TryFindElementBy("Schließen", helperTableTitleBar, AiStringHelper.By.Name);
                //Fallback falls Englisch
                if (helperTableCloseButton == null)
                {
                    helperTableCloseButton = TestHelper.TryFindElementBy("Close", helperTableTitleBar, AiStringHelper.By.Name);
                }
                Assert.IsNotNull(helperTableCloseButton, "Schließenbutton nicht gefunden");

                helperTableCloseButton.Click();

                listInput = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.InputList);
                listEntry = listInput.FindElementByAccessibilityId(name);
            }
            return listEntry;
        }
        public static void DeleteHelper(string name, string strHelperTableName)
        {
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            //Auf Helperseite wechseln
            var globalTree = QstSession.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);

            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);
            var auxiliaryButton = globalTree.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.AuxiliaryMasterData);

            NavigateToAuxiliarySubmenu(QstSession, globalTree, auxiliaryButton, strHelperTableName);
            Thread.Sleep(200);

            DeleteAndAssertHelper(name);
        }
        public static void DeleteHelpers(List<string> names, string strHelperTableName)
        {
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            //Auf Helperseite wechseln
            var globalTree = QstSession.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);

            NavigateToAuxiliarySubmenu(QstSession, globalTree, strHelperTableName);
            Thread.Sleep(200);

            foreach (var name in names)
            {
                DeleteAndAssertHelper(name);
            }
        }

        private static void DeleteAndAssertHelper(string name)
        {
            var helperView = QstSession.FindElementByAccessibilityId(AiStringHelper.HelperTable.View);
            var helperListbox = helperView.FindElementByAccessibilityId(AiStringHelper.HelperTable.HelperListBox);

            AppiumWebElement deleteEntry = TestHelper.FindElementInListbox(name, helperListbox);
            if (deleteEntry != null)
            {
                //Helper löschen
                DeleteHelper(helperView, deleteEntry);

                // Daten prüfen
                deleteEntry = TestHelper.FindElementInListbox(name, helperListbox);
                Assert.IsNull(deleteEntry, "Hilfstabelleneintrag sollte gelöscht sein");
            }
        }
        private static void DeleteHelper(WindowsElement helperView, AppiumWebElement toDeleteListEntry)
        {
            if (toDeleteListEntry != null)
            {
                toDeleteListEntry.Click();

                //Hilfstabelleneintrag Löschen
                toDeleteListEntry.Click();
                var delHelperBtn = helperView.FindElementByAccessibilityId(AiStringHelper.HelperTable.DeleteHelper);
                delHelperBtn.Click();

                var clearConfirmBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
                clearConfirmBtn.Click();
                TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            }
        }
        public static void AddToleranceClass(ToleranceClass tolClass, string strToleranceClass, WindowsDriver<WindowsElement> session)
        {
            //Auf Helperseite wechseln
            var globalTree = session.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);

            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);
            var auxiliaryButton = globalTree.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.AuxiliaryMasterData);

            NavigateToAuxiliarySubmenu(session, globalTree, auxiliaryButton, strToleranceClass);

            Thread.Sleep(200);

            var toleranceView = TestHelper.FindElementWithWait(AiStringHelper.ToleranceClass.View, session);
            var helperListbox = toleranceView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.ListBox);

            AppiumWebElement addEntry = TestHelper.FindElementInListbox(tolClass.Name, helperListbox, AiStringHelper.By.Name);

            if (addEntry == null)
            {
                CreateToleranceClass(tolClass, session);
            }
        }
        public static void CreateToleranceClass(ToleranceClass tolClass, WindowsDriver<WindowsElement> session)
        {
            var tolClassView = TestHelper.FindElementWithWait(AiStringHelper.ToleranceClass.View, session);
            var tolClassListbox = tolClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.ListBox);

            AppiumWebElement addEntry = TestHelper.FindElementInListbox(tolClass.Name, tolClassListbox);
            if (addEntry == null)
            {
                var addTolClassBtn = tolClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.AddToleranceClass);
                var saveTolClassBtn = tolClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SaveToleranceClass);

                // Toleranzklasse anlegen
                addTolClassBtn.Click();
                TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

                var name = tolClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.Name);
                TestHelper.WaitForElementToBeEnabledAndDisplayed(session, 5, 300, name);
                name.Clear();
                TestHelper.SendKeysConverted(name, tolClass.Name);

                var rdBtnRelative = tolClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.RdBtnRelative);
                var rdBtnAbsolute = tolClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.RdBtnAbsolute);

                if (tolClass.IsRelative)
                {
                    rdBtnRelative.Click();
                }
                else
                {
                    rdBtnAbsolute.Click();
                }

                var symmetricLimits = tolClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.SymmetricalLimits);
                if ((tolClass.IsSymmetrical && !symmetricLimits.Selected)
                    || (!tolClass.IsSymmetrical && symmetricLimits.Selected))
                {
                    symmetricLimits.Click();
                }

                //IsSymmetrical funktioniert aktuell nur über Action zum testen
                //Kann Wert in SymmetricUpDown (syncfusion:UpDown) Element nicht direkt setzen
                //Bei LowerUpDown und UpperUpDown funktionert es
                //Bei LowerUpDown und UpperUpDown sind noch jeweils eine Textbox und die zwei auf- und up-Buttons untergeordnet 
                //bei SymmetricUpDown fehlen diese Elemente (warum auch immer???)
                if (tolClass.IsSymmetrical)
                {
                    var lowerUpperLimit = tolClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.LowerUpperLimit);
                    lowerUpperLimit.Click();
                    Actions actions = new Actions(session);
                    actions.SendKeys(tolClass.Lower.ToString(numberFormatThreeDecimals, currentCulture));
                    actions.Build().Perform();
                    //var lowerUpperLimitTextBox = lowerUpperLimit.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
                    //lowerUpperLimitTextBox.SendKeys(tolClass.Lower.ToString(numberFormatThreeDecimals, currentCulture));
                }
                else
                {
                    var lowerLimit = tolClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.LowerLimit);
                    var lowerLimitTextBox = lowerLimit.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
                    lowerLimitTextBox.SendKeys(tolClass.Lower.ToString(numberFormatThreeDecimals, currentCulture));
                    var upperLimit = tolClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.UpperLimit);
                    var upperLimitTextBox = upperLimit.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
                    upperLimitTextBox.SendKeys(tolClass.Upper.ToString(numberFormatThreeDecimals, currentCulture));
                }

                saveTolClassBtn.Click();
                //Kein Confirm-Button bei Toleranzklasse
                //var confirmBtn = FindElementByAbsoluteXPathWithWait(AiStringHelper.GeneralStrings.XPathAssistantConfirmButton);
                //confirmBtn.Click();
                TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
                Thread.Sleep(500);

                // Daten prüfen
                tolClassListbox = tolClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.ListBox);
                var tolClassListEntry = TestHelper.FindElementInListbox(tolClass.Name, tolClassListbox, AiStringHelper.By.Name);
                tolClassListEntry.Click();

                //Warten bis Daten geladen sind
                TestHelper.WaitforElementTextLoaded(name, tolClass.Name);

                rdBtnRelative = tolClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.RdBtnRelative);
                rdBtnAbsolute = tolClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.RdBtnAbsolute);
                symmetricLimits = tolClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.SymmetricalLimits);
                //Assert.AreEqual(tolClass.Name, name.Text);
                //Assert.AreEqual(tolClass.IsRelative, rdBtnRelative.Selected);
                //Assert.AreEqual(!tolClass.IsRelative, rdBtnAbsolute.Selected);
                //Assert.AreEqual(tolClass.IsSymmetrical, symmetricLimits.Selected);
                if (tolClass.IsSymmetrical)
                {
                    var lowerUpperLimit = tolClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.LowerUpperLimit);
                    //Assert.AreEqual(tolClass.Lower.ToString(numberFormatThreeDecimals, currentCulture), Convert.ToDouble(lowerUpperLimit.Text).ToString(numberFormatThreeDecimals, currentCulture));
                }
                else
                {
                    var lowerLimit = tolClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.LowerLimit);
                    var lowerLimitTextBox = lowerLimit.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
                    //Assert.AreEqual(tolClass.Lower.ToString(numberFormatThreeDecimals, currentCulture), lowerLimitTextBox.Text);
                    var upperLimit = tolClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.UpperLimit);
                    var upperLimitTextBox = upperLimit.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
                    //Assert.AreEqual(tolClass.Upper.ToString(numberFormatThreeDecimals, currentCulture), upperLimitTextBox.Text);
                }
            }
        }
        private static void AssertToleranceClass(WindowsDriver<WindowsElement> QstSession, ToleranceClass tolClass)
        {
            var toleranceClassScrollViewer = TestHelper.FindElementWithWait(AiStringHelper.ToleranceClass.SingleToleranceClass.ScrollViewer, QstSession);
            var tolClassName = toleranceClassScrollViewer.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.Name);
            var rdBtnRelative = toleranceClassScrollViewer.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.RdBtnRelative);
            var rdBtnAbsolute = toleranceClassScrollViewer.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.RdBtnAbsolute);
            var symmetricLimits = toleranceClassScrollViewer.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.SymmetricalLimits);

            Assert.AreEqual(tolClass.Name, tolClassName.Text);
            Assert.AreEqual(tolClass.IsRelative, rdBtnRelative.Selected);
            Assert.AreEqual(!tolClass.IsRelative, rdBtnAbsolute.Selected);
            Assert.AreEqual(tolClass.IsSymmetrical, symmetricLimits.Selected);
            if (tolClass.IsSymmetrical)
            {
                var lowerUpperLimit = toleranceClassScrollViewer.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.LowerUpperLimit);
                //Assert.AreEqual(tolClass.Lower.ToString(numberFormatThreeDecimals, currentCulture), Convert.ToDouble(lowerUpperLimit.Text).ToString(numberFormatThreeDecimals, currentCulture));
            }
            else
            {
                var lowerLimit = toleranceClassScrollViewer.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.LowerLimit);
                var lowerLimitTextBox = lowerLimit.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
                //Assert.AreEqual(tolClass.Lower.ToString(numberFormatThreeDecimals, currentCulture), lowerLimitTextBox.Text);
                var upperLimit = toleranceClassScrollViewer.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.UpperLimit);
                var upperLimitTextBox = upperLimit.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
                //Assert.AreEqual(tolClass.Upper.ToString(numberFormatThreeDecimals, currentCulture), upperLimitTextBox.Text);
            }
        }
        private static void UpdateToleranceClass(WindowsDriver<WindowsElement> QstSession, ToleranceClass tolClass)
        {
            var toleranceClassScrollViewer = TestHelper.FindElementWithWait(AiStringHelper.ToleranceClass.SingleToleranceClass.ScrollViewer, QstSession);
            var tolClassName = toleranceClassScrollViewer.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.Name);
            var rdBtnRelative = toleranceClassScrollViewer.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.RdBtnRelative);
            var rdBtnAbsolute = toleranceClassScrollViewer.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.RdBtnAbsolute);
            var symmetricLimits = toleranceClassScrollViewer.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.SymmetricalLimits);

            tolClassName.Clear();
            TestHelper.SendKeysConverted(tolClassName, tolClass.Name);

            if (tolClass.IsRelative)
            {
                rdBtnRelative.Click();
            }
            else
            {
                rdBtnAbsolute.Click();
            }

            //IsSymmetrical funktioniert aktuell nur über Action zum testen
            //Kann Wert in SymmetricUpDown (syncfusion:UpDown) Element nicht direkt setzen
            //Bei LowerUpDown und UpperUpDown funktionert es
            //Bei LowerUpDown und UpperUpDown sind noch jeweils eine Textbox und die zwei auf- und up-Buttons untergeordnet 
            //bei SymmetricUpDown fehlen diese Elemente (warum auch immer???)
            if (tolClass.IsSymmetrical)
            {
                if (!symmetricLimits.Selected)
                {
                    symmetricLimits.Click();
                }

                var lowerUpperLimit = TestHelper.FindElementByAiWithWaitFromParent(toleranceClassScrollViewer, AiStringHelper.ToleranceClass.SingleToleranceClass.LowerUpperLimit, QstSession);
                TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, 5, 300, lowerUpperLimit);
                lowerUpperLimit.Click();
                Actions actions = new Actions(QstSession);
                actions.SendKeys(tolClass.Lower.ToString(numberFormatThreeDecimals, currentCulture));
                actions.Build().Perform();
                //var lowerUpperLimitTextBox = lowerUpperLimit.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
                //lowerUpperLimitTextBox.SendKeys(tolClass.Lower.ToString(numberFormatThreeDecimals, currentCulture));
            }
            else
            {
                if (symmetricLimits.Selected)
                {
                    symmetricLimits.Click();
                }

                var lowerLimit = TestHelper.FindElementByAiWithWaitFromParent(toleranceClassScrollViewer, AiStringHelper.ToleranceClass.SingleToleranceClass.LowerLimit, QstSession);
                var lowerLimitTextBox = lowerLimit.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
                TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, 5, 300, lowerLimitTextBox);
                lowerLimitTextBox.SendKeys(tolClass.Lower.ToString(numberFormatThreeDecimals, currentCulture));
                var upperLimit = toleranceClassScrollViewer.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SingleToleranceClass.UpperLimit);
                var upperLimitTextBox = upperLimit.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
                TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, 5, 300, upperLimitTextBox);
                upperLimitTextBox.SendKeys(tolClass.Upper.ToString(numberFormatThreeDecimals, currentCulture));
            }

            var saveTolClass = QstSession.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.SaveToleranceClass);
            saveTolClass.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            //TODO evtl ConfirmWindow noch einbauen wenn das kommen sollte
        }
        private static void DeleteToleranceclass(WindowsDriver<WindowsElement> QstSession, AppiumWebElement toleranceClassView, ToleranceClass tolClass)
        {
            var toleranceClassListbox = TestHelper.FindElementByAiWithWaitFromParent(toleranceClassView, AiStringHelper.ToleranceClass.ListBox, QstSession);
            var toleranceClassListEntry = TestHelper.FindElementInListbox(tolClass.Name, toleranceClassListbox, AiStringHelper.By.Name);

            if (toleranceClassListEntry != null)
            {
                toleranceClassListEntry.Click();
                var btnDeleteToleranceClass = toleranceClassView.FindElementByAccessibilityId(AiStringHelper.ToleranceClass.DeleteToleranceClass);
                TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, 5, 300, btnDeleteToleranceClass);
                btnDeleteToleranceClass.Click();

                var confirmDeleteBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
                confirmDeleteBtn.Click();
                TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            }
        }
        public static AppiumWebElement FindOrCreateToleranceClass(ToleranceClass tolClass, WindowsDriver<WindowsElement> assistantSession, WindowsElement listInput)
        {
            var listEntry = TestHelper.TryFindElementBy(tolClass.Name, listInput);
            if (listEntry == null)
            {
                var jumpHelperTable = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.JumpHelperTable);
                jumpHelperTable.Click();

                CreateToleranceClass(tolClass, assistantSession);

                var childWindows = assistantSession.FindElementsByClassName("Window");
                WindowsElement helperTableWindow = null;
                foreach (var window in childWindows)
                {
                    //Suche aus den zwei AssistentenFenster dass ohne Titel raus
                    //Ist nicht hübsch funktioniert aber
                    if (window.Text == "")
                    {
                        helperTableWindow = window;
                    }
                }
                //helperTable-Window wieder in den Vordergrund bringen
                assistantSession.SwitchTo().Window(assistantSession.WindowHandles.First());

                var helperTableTitleBar = helperTableWindow.FindElementByAccessibilityId("TitleBar");

                //AutomationId wird zwar in Inspect angezeigt aber der Button wird nicht gefunden 
                //var helperTableCloseButton = TryFindElementByAccessabilityId("Close", helperTableTitleBar);
                var helperTableCloseButton = TestHelper.TryFindElementBy("Schließen", helperTableTitleBar, AiStringHelper.By.Name);
                //Fallback falls Englisch
                if (helperTableCloseButton == null)
                {
                    helperTableCloseButton = TestHelper.TryFindElementBy("Close", helperTableTitleBar, AiStringHelper.By.Name);
                }
                Assert.IsNotNull(helperTableCloseButton, "Schließenbutton nicht gefunden");

                helperTableCloseButton.Click();

                listInput = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.InputList);
                listEntry = listInput.FindElementByAccessibilityId(tolClass.Name);
            }
            return listEntry;
        }

        private static ManufacturerUIHelper GetManuUiHelperWithElements(WindowsElement manuView, WindowsDriver<WindowsElement> QstSession)
        {
            AppiumWebElement manuSMScrollViewer = TestHelper.FindElementByAiWithWaitFromParent(manuView, AiStringHelper.Manufacturer.SingleManufacturer.SingleManuScrollViewer, QstSession);
            AppiumWebElement manuName = manuSMScrollViewer.FindElementByAccessibilityId(AiStringHelper.Manufacturer.SingleManufacturer.Name);

            AppiumWebElement manuPerson = manuSMScrollViewer.FindElementByAccessibilityId(AiStringHelper.Manufacturer.SingleManufacturer.ContactPerson);
            AppiumWebElement manuPhoneNumber = manuSMScrollViewer.FindElementByAccessibilityId(AiStringHelper.Manufacturer.SingleManufacturer.PhoneNumber);
            AppiumWebElement manuFax = manuSMScrollViewer.FindElementByAccessibilityId(AiStringHelper.Manufacturer.SingleManufacturer.Fax);
            AppiumWebElement manuStreet = manuSMScrollViewer.FindElementByAccessibilityId(AiStringHelper.Manufacturer.SingleManufacturer.Street);
            AppiumWebElement manuHouseNumber = manuSMScrollViewer.FindElementByAccessibilityId(AiStringHelper.Manufacturer.SingleManufacturer.HouseNumber);
            AppiumWebElement manuPlz = manuSMScrollViewer.FindElementByAccessibilityId(AiStringHelper.Manufacturer.SingleManufacturer.ZipCode);
            AppiumWebElement manuCity = manuSMScrollViewer.FindElementByAccessibilityId(AiStringHelper.Manufacturer.SingleManufacturer.City);
            AppiumWebElement manuCountry = manuSMScrollViewer.FindElementByAccessibilityId(AiStringHelper.Manufacturer.SingleManufacturer.Country);
            AppiumWebElement manuComment = manuSMScrollViewer.FindElementByAccessibilityId(AiStringHelper.Manufacturer.SingleManufacturer.Comment);

            ManufacturerUIHelper manuUiHelper = new ManufacturerUIHelper(manuView, manuSMScrollViewer, manuName, manuPerson, manuPhoneNumber, manuFax, manuStreet, manuHouseNumber, manuPlz, manuCity, manuCountry, manuComment);
            return manuUiHelper;
        }
        /// <summary>
        /// Drückt auf den Hinzufügen-Button und füllt die Felder mit den übergebenen Strings aus
        /// Achtung Sendkeys benutzt englische Tastatur wenn man in der Oberfläche ein y haben will muss man z schreiben
        /// </summary>
        /// <param name="manuView">die Übergeordnete View des Herstellers</param>
        /// <param name="name"></param>
        /// <param name="person"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="fax"></param>
        /// <param name="street"></param>
        /// <param name="houseNumber"></param>
        /// <param name="plz"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="comment"></param>
        /// <param name="manufacturerUIHelper"></param>
        private static void CreateManufacturer(WindowsDriver<WindowsElement> session, Manufacturer manufacturer, ManufacturerUIHelper manufacturerUIHelper)
        {
            var addManuBtn = session.FindElementByAccessibilityId(AiStringHelper.Manufacturer.AddManufacturer);
            addManuBtn.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            Thread.Sleep(200);
            UpdateManufacturer(session, manufacturer, manufacturerUIHelper);
        }
        private static void UpdateManufacturer(WindowsDriver<WindowsElement> QstSession, Manufacturer manufacturer, ManufacturerUIHelper manufacturerUIHelper)
        {
            var manuNameIsEnabledAndDisplayed = TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, 5, 300, manufacturerUIHelper.ManuName);
            Assert.IsTrue(manuNameIsEnabledAndDisplayed);
            manufacturerUIHelper.ManuName.Clear();
            TestHelper.SendKeysConverted(manufacturerUIHelper.ManuName, manufacturer.Name);
            manufacturerUIHelper.ManuPerson.Clear();
            TestHelper.SendKeysConverted(manufacturerUIHelper.ManuPerson, manufacturer.ContactPerson);
            manufacturerUIHelper.ManuPhoneNumber.Clear();
            TestHelper.SendKeysConverted(manufacturerUIHelper.ManuPhoneNumber, manufacturer.PhoneNumber);
            manufacturerUIHelper.ManuFax.Clear();
            TestHelper.SendKeysConverted(manufacturerUIHelper.ManuFax, manufacturer.Fax);
            manufacturerUIHelper.ManuStreet.Clear();
            TestHelper.SendKeysConverted(manufacturerUIHelper.ManuStreet, manufacturer.Street);
            manufacturerUIHelper.ManuHouseNumber.Clear();
            TestHelper.SendKeysConverted(manufacturerUIHelper.ManuHouseNumber, manufacturer.HouseNumber);
            manufacturerUIHelper.ManuPlz.Clear();
            TestHelper.SendKeysConverted(manufacturerUIHelper.ManuPlz, manufacturer.ZipCode);
            manufacturerUIHelper.ManuCity.Clear();
            TestHelper.SendKeysConverted(manufacturerUIHelper.ManuCity, manufacturer.City);
            manufacturerUIHelper.ManuCountry.Clear();
            TestHelper.SendKeysConverted(manufacturerUIHelper.ManuCountry, manufacturer.Country);
            manufacturerUIHelper.ManuComment.Clear();
            TestHelper.SendKeysConverted(manufacturerUIHelper.ManuComment, manufacturer.Comment);
        }
        private static void AssertListManufacturer(WindowsDriver<WindowsElement> QstSession, AppiumWebElement manufacturerView, Manufacturer manufacturer)
        {
            TestHelper.AssertGridRow(QstSession, manufacturerView, AiStringHelper.Manufacturer.ManufacturerGrid, AiStringHelper.Manufacturer.ManufacturerGridHeader, AiStringHelper.Manufacturer.ManufacturerGridRowPrefix, manufacturer.Name, Manufacturer.ManufacturerListHeaderStrings.Name, manufacturer.Name);
            TestHelper.AssertGridRow(QstSession, manufacturerView, AiStringHelper.Manufacturer.ManufacturerGrid, AiStringHelper.Manufacturer.ManufacturerGridHeader, AiStringHelper.Manufacturer.ManufacturerGridRowPrefix, manufacturer.Name, Manufacturer.ManufacturerListHeaderStrings.ContactPerson, manufacturer.ContactPerson);
            TestHelper.AssertGridRow(QstSession, manufacturerView, AiStringHelper.Manufacturer.ManufacturerGrid, AiStringHelper.Manufacturer.ManufacturerGridHeader, AiStringHelper.Manufacturer.ManufacturerGridRowPrefix, manufacturer.Name, Manufacturer.ManufacturerListHeaderStrings.PhoneNumber, manufacturer.PhoneNumber);
            TestHelper.AssertGridRow(QstSession, manufacturerView, AiStringHelper.Manufacturer.ManufacturerGrid, AiStringHelper.Manufacturer.ManufacturerGridHeader, AiStringHelper.Manufacturer.ManufacturerGridRowPrefix, manufacturer.Name, Manufacturer.ManufacturerListHeaderStrings.Fax, manufacturer.Fax);
            TestHelper.AssertGridRow(QstSession, manufacturerView, AiStringHelper.Manufacturer.ManufacturerGrid, AiStringHelper.Manufacturer.ManufacturerGridHeader, AiStringHelper.Manufacturer.ManufacturerGridRowPrefix, manufacturer.Name, Manufacturer.ManufacturerListHeaderStrings.Street, manufacturer.Street);
            TestHelper.AssertGridRow(QstSession, manufacturerView, AiStringHelper.Manufacturer.ManufacturerGrid, AiStringHelper.Manufacturer.ManufacturerGridHeader, AiStringHelper.Manufacturer.ManufacturerGridRowPrefix, manufacturer.Name, Manufacturer.ManufacturerListHeaderStrings.HouseNumber, manufacturer.HouseNumber);
            TestHelper.AssertGridRow(QstSession, manufacturerView, AiStringHelper.Manufacturer.ManufacturerGrid, AiStringHelper.Manufacturer.ManufacturerGridHeader, AiStringHelper.Manufacturer.ManufacturerGridRowPrefix, manufacturer.Name, Manufacturer.ManufacturerListHeaderStrings.ZipCode, manufacturer.ZipCode);
            TestHelper.AssertGridRow(QstSession, manufacturerView, AiStringHelper.Manufacturer.ManufacturerGrid, AiStringHelper.Manufacturer.ManufacturerGridHeader, AiStringHelper.Manufacturer.ManufacturerGridRowPrefix, manufacturer.Name, Manufacturer.ManufacturerListHeaderStrings.City, manufacturer.City);
            TestHelper.AssertGridRow(QstSession, manufacturerView, AiStringHelper.Manufacturer.ManufacturerGrid, AiStringHelper.Manufacturer.ManufacturerGridHeader, AiStringHelper.Manufacturer.ManufacturerGridRowPrefix, manufacturer.Name, Manufacturer.ManufacturerListHeaderStrings.Country, manufacturer.Country);
        }
        private static void AssertSingleManufacturer(ManufacturerUIHelper manuUiHelper, Manufacturer manufacturer)
        {
            //Warten bis Daten geladen sind
            TestHelper.WaitforElementTextLoaded(manuUiHelper.ManuName, manufacturer.Name);

            Assert.AreEqual(manufacturer.Name, manuUiHelper.ManuName.Text);
            Assert.AreEqual(manufacturer.ContactPerson, manuUiHelper.ManuPerson.Text);
            Assert.AreEqual(manufacturer.PhoneNumber, manuUiHelper.ManuPhoneNumber.Text);
            Assert.AreEqual(manufacturer.Fax, manuUiHelper.ManuFax.Text);
            Assert.AreEqual(manufacturer.Street, manuUiHelper.ManuStreet.Text);
            Assert.AreEqual(manufacturer.HouseNumber, manuUiHelper.ManuHouseNumber.Text);
            Assert.AreEqual(manufacturer.ZipCode, manuUiHelper.ManuPlz.Text);
            Assert.AreEqual(manufacturer.City, manuUiHelper.ManuCity.Text);
            Assert.AreEqual(manufacturer.Country, manuUiHelper.ManuCountry.Text);
            //TODO Einkommentieren wenn Kommentar angezeigt wird
            //Assert.AreEqual(TestHelper.getConvertedString(manufacturer1.Comment), manuComment.Text);
        }
        private static void DeleteManufacturer(AppiumWebElement manuView, AppiumWebElement toDeleteListEntry)
        {
            if (toDeleteListEntry != null)
            {
                //Hersteller Löschen
                toDeleteListEntry.Click();
                Thread.Sleep(200);
                var delManuBtn = manuView.FindElementByAccessibilityId(AiStringHelper.Manufacturer.DeleteManufacturer);
                delManuBtn.Click();

                var confirmBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
                confirmBtn.Click();
                TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            }
        }
    }
}