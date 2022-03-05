using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;

using UI_TestProjekt.Helper;
using UI_TestProjekt.TestModel;

namespace UI_TestProjekt
{
    [TestClass]
    public class TestEquipmentTests : TestBase
    {
        [TestMethod]
        [TestCategory("MasterData")]
        public void TestTestEquipmentModelReadOnly()
        {
            LoginAsQST();

            //Session für Hauptfenster erstellen
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            var quantecModel = Testdata.GetTestEquipmentModel1();
            var STA6000Model = Testdata.GetTestEquipmentModel2();

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestEquipment);
            var testEquipmentView = TestHelper.FindElementWithWait(AiStringHelper.TestEquipment.View, QstSession);

            SelectTestequipmentModelNode(QstSession, quantecModel, testEquipmentView);
            AssertTestequipmentModelReadOnly(QstSession);

            SelectTestequipmentModelNode(QstSession, STA6000Model, testEquipmentView);
            AssertTestequipmentModelReadOnly(QstSession);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestTestEquipmentModel()
        {
            LoginAsCSP();
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestEquipment);
            var testequipmentQuantec = Testdata.GetTestEquipment1();
            var testequipmentSTA6000 = Testdata.GetTestEquipment2();

            var testequipmentModelQuantecChanged = Testdata.GetTestEquipmentModel3();
            var testequipmentModelSta6000ChangedNew = Testdata.GetTestEquipmentModel5();
            var testequipmentModelSTA6000Changed = Testdata.GetTestEquipmentModel6();

            DeleteTestequipment(QstSession, testequipmentQuantec);
            DeleteTestequipment(QstSession, testequipmentSTA6000);

            //Zuerst definierten Zustand herstellen
            var testEquipmentView = TestHelper.FindElementWithWait(AiStringHelper.TestEquipment.View, QstSession);

            UpdateTestEquipmentModel(QstSession, testequipmentQuantec.Model);
            var save = testEquipmentView.FindElementByAccessibilityId(AiStringHelper.TestEquipment.Save);
            if (TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, save))
            {
                save.Click();
            }

            var viewVerifyChanges = TestHelper.TryFindElementByAccessabilityId((AiStringHelper.VerifyChanges.View), DesktSession);
            if (viewVerifyChanges != null)
            {
                var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
                btnApply.Click();
                TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            }


            UpdateTestEquipmentModel(QstSession, testequipmentSTA6000.Model);
            save = testEquipmentView.FindElementByAccessibilityId(AiStringHelper.TestEquipment.Save);
            if (TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, save))
            {
                save.Click();
            }

            viewVerifyChanges = TestHelper.TryFindElementByAccessabilityId((AiStringHelper.VerifyChanges.View), DesktSession);
            if (viewVerifyChanges != null)
            {
                var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
                if (TestHelper.WaitForElementToBeEnabledAndDisplayed(DesktSession, btnApply))
                {
                    btnApply.Click();
                    TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
                }
            }

            CreateTestequipment(QstSession, testequipmentQuantec);
            CreateTestequipment(QstSession, testequipmentSTA6000);

            UpdateTestEquipmentModel(QstSession, testequipmentModelQuantecChanged);
            save = testEquipmentView.FindElementByAccessibilityId(AiStringHelper.TestEquipment.Save);
            save.Click();

            viewVerifyChanges = TestHelper.TryFindElementByAccessabilityId((AiStringHelper.VerifyChanges.View), DesktSession);
            if (viewVerifyChanges != null)
            {
                var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
                VerifyTestequipmentModelChangesInVerifyView(testequipmentQuantec.Model, testequipmentModelQuantecChanged, listViewChanges, 21);

                var textBoxComment = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Comment);
                TestHelper.SendKeysConverted(textBoxComment, "Model1 ändern");

                var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
                if (TestHelper.WaitForElementToBeEnabledAndDisplayed(DesktSession, btnApply))
                {
                    btnApply.Click();
                    TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
                }
            }
            testequipmentQuantec.Model = testequipmentModelQuantecChanged;

            UpdateTestEquipmentModel(QstSession, testequipmentModelSTA6000Changed);
            save = testEquipmentView.FindElementByAccessibilityId(AiStringHelper.TestEquipment.Save);
            save.Click();

            viewVerifyChanges = TestHelper.TryFindElementByAccessabilityId((AiStringHelper.VerifyChanges.View), DesktSession);
            if (viewVerifyChanges != null)
            {
                var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
                VerifyTestequipmentModelChangesInVerifyView(testequipmentSTA6000.Model, testequipmentModelSTA6000Changed, listViewChanges, 11);

                var textBoxComment = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Comment);
                TestHelper.SendKeysConverted(textBoxComment, "Model2 ändern");

                var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
                if (TestHelper.WaitForElementToBeEnabledAndDisplayed(DesktSession, btnApply))
                {
                    btnApply.Click();
                    TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
                }
            }
            testequipmentSTA6000.Model = testequipmentModelSTA6000Changed;

            var teTreeViewRootNode = TestHelper.FindElementByAiWithWaitFromParent(testEquipmentView, AiStringHelper.TestEquipment.TestEquipmentTreeViewRoot, QstSession);
            //Assert Model
            SelectTestequipmentModelNode(QstSession, testequipmentModelQuantecChanged, testEquipmentView);
            AssertTestequipmentModel(QstSession, testequipmentModelQuantecChanged);

            SelectTestequipmentModelNode(QstSession, testequipmentModelSTA6000Changed, testEquipmentView);
            AssertTestequipmentModel(QstSession, testequipmentModelSTA6000Changed);

            //AssertTestequipmentFeatures
            var te1Node = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipmentQuantec.GetParentListWithTestequipment());
            te1Node.Click();
            AssertTestequipmentFeaturesForModel(QstSession, testequipmentQuantec);

            var te2Node = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipmentSTA6000.GetParentListWithTestequipment());
            te2Node.Click();
            AssertTestequipmentFeaturesForModel(QstSession, testequipmentSTA6000);

            UpdateTestEquipmentModel(QstSession, testequipmentModelSta6000ChangedNew);
            save = testEquipmentView.FindElementByAccessibilityId(AiStringHelper.TestEquipment.Save);
            save.Click();

            viewVerifyChanges = TestHelper.TryFindElementByAccessabilityId((AiStringHelper.VerifyChanges.View), DesktSession);
            if (viewVerifyChanges != null)
            {
                var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
                VerifyTestequipmentModelChangesInVerifyView(testequipmentModelSTA6000Changed, testequipmentModelSta6000ChangedNew, listViewChanges, 14);

                var textBoxComment = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Comment);
                TestHelper.SendKeysConverted(textBoxComment, "Model3 ändern");

                var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
                if (TestHelper.WaitForElementToBeEnabledAndDisplayed(DesktSession, btnApply))
                {
                    btnApply.Click();
                    TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
                }
            }
            testequipmentSTA6000.Model = testequipmentModelSta6000ChangedNew;

            //AssertTestequipmentFeatures
            teTreeViewRootNode = TestHelper.FindElementByAiWithWaitFromParent(testEquipmentView, AiStringHelper.TestEquipment.TestEquipmentTreeViewRoot, QstSession);

            te2Node = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipmentSTA6000.GetParentListWithTestequipment());
            te2Node.Click();
            AssertTestequipmentFeaturesForModel(QstSession, testequipmentSTA6000);

            //Assert ModelNew
            SelectTestequipmentModelNode(QstSession, testequipmentModelSta6000ChangedNew, testEquipmentView);
            AssertTestequipmentModel(QstSession, testequipmentModelSta6000ChangedNew);

            DeleteTestequipment(QstSession, testequipmentQuantec);
            DeleteTestequipment(QstSession, testequipmentSTA6000);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestTestEquipment()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            var testequipment1 = Testdata.GetTestEquipment1();
            var testequipment2 = Testdata.GetTestEquipment2();

            var testequipmentChanged1 = Testdata.GetTestEquipmentChanged1();
            var testequipmentChanged2 = Testdata.GetTestEquipmentChanged2();

            //TODO Status anlegen für TeTest
            //AddHelper("teStatus1", AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.Status);
            //AddHelper("teStatus2", AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.Status);

            //Auf Prüfgeräteseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestEquipment);

            DeleteTestequipment(QstSession, testequipment1);
            DeleteTestequipment(QstSession, testequipment2);
            DeleteTestequipment(QstSession, testequipmentChanged1);
            DeleteTestequipment(QstSession, testequipmentChanged2);

            var testEquipmentView = TestHelper.FindElementWithWait(AiStringHelper.TestEquipment.View, QstSession);
            var teTreeViewRootNode = TestHelper.FindElementByAiWithWaitFromParent(testEquipmentView, AiStringHelper.TestEquipment.TestEquipmentTreeViewRoot, QstSession);

            //Bei Model nur Speichern ohne Check, Check bei Model = Extra Testfall
            UpdateTestEquipmentModel(QstSession, testequipment1.Model);
            var save = testEquipmentView.FindElementByAccessibilityId(AiStringHelper.TestEquipment.Save);
            if (TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, save))
            {
                save.Click();
            }

            var viewVerifyChanges = TestHelper.TryFindElementByAccessabilityId((AiStringHelper.VerifyChanges.View), DesktSession);
            if (viewVerifyChanges != null)
            {
                var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
                if (TestHelper.WaitForElementToBeEnabledAndDisplayed(DesktSession, btnApply))
                {
                    btnApply.Click();
                }
            }
            UpdateTestEquipmentModel(QstSession, testequipment2.Model);
            save = testEquipmentView.FindElementByAccessibilityId(AiStringHelper.TestEquipment.Save);
            if (TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, save))
            {
                save.Click();
            }

            viewVerifyChanges = TestHelper.TryFindElementByAccessabilityId((AiStringHelper.VerifyChanges.View), DesktSession);
            if (viewVerifyChanges != null)
            {
                var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
                if (TestHelper.WaitForElementToBeEnabledAndDisplayed(DesktSession, btnApply))
                {
                    btnApply.Click();
                }
            }

            //Create Testequipment
            CreateTestequipment(QstSession, testequipment1);
            CreateTestequipment(QstSession, testequipment2);

            var te1Node = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipment1.GetParentListWithTestequipment());
            te1Node.Click();
            AssertTestequipment(QstSession, testequipment1, false);

            var te2Node = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipment2.GetParentListWithTestequipment());
            te2Node.Click();
            AssertTestequipment(QstSession, testequipment2, false);

            te1Node = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipment1.GetParentListWithTestequipment());
            te1Node.Click();
            SetTestequipmentFeatures(testEquipmentView, new Testequipment(), testequipment1);

            save = testEquipmentView.FindElementByAccessibilityId(AiStringHelper.TestEquipment.Save);
            if (TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, save))
            {
                save.Click();
            }

            viewVerifyChanges = TestHelper.TryFindElementByAccessabilityId((AiStringHelper.VerifyChanges.View), DesktSession);
            if (viewVerifyChanges != null)
            {
                var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
                VerifyTestequipmentChangesInVerifyView(new Testequipment(), testequipment1, listViewChanges, 14);

                var textBoxComment = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Comment);
                TestHelper.SendKeysConverted(textBoxComment, "Features von erstem Prüfgerät setzen");

                var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
                if (TestHelper.WaitForElementToBeEnabledAndDisplayed(DesktSession, btnApply))
                {
                    btnApply.Click();
                    TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
                }
            }

            te2Node = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipment2.GetParentListWithTestequipment());
            te2Node.Click();
            SetTestequipmentFeatures(testEquipmentView, new Testequipment(), testequipment2);

            save = testEquipmentView.FindElementByAccessibilityId(AiStringHelper.TestEquipment.Save);
            if (TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, save))
            {
                save.Click();
            }

            viewVerifyChanges = TestHelper.TryFindElementByAccessabilityId((AiStringHelper.VerifyChanges.View), DesktSession);
            if (viewVerifyChanges != null)
            {
                var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
                VerifyTestequipmentChangesInVerifyView(new Testequipment(), testequipment2, listViewChanges, 0);

                var textBoxComment = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Comment);
                TestHelper.SendKeysConverted(textBoxComment, "Features von zweitem Prüfgerät setzen");

                var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
                if (TestHelper.WaitForElementToBeEnabledAndDisplayed(DesktSession, btnApply))
                {
                    btnApply.Click();
                    TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
                }
            }

            te1Node = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipment1.GetParentListWithTestequipment());
            te1Node.Click();
            AssertTestequipment(QstSession, testequipment1);

            te2Node = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipment2.GetParentListWithTestequipment());
            te2Node.Click();
            AssertTestequipment(QstSession, testequipment2);

            te1Node = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipment1.GetParentListWithTestequipment());
            te1Node.Click();

            //UpdateTestEquipment
            UpdateTestEquipment(QstSession, testEquipmentView, testequipment1, testequipmentChanged1);

            save = testEquipmentView.FindElementByAccessibilityId(AiStringHelper.TestEquipment.Save);
            if (TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, save))
            {
                save.Click();
            }

            viewVerifyChanges = TestHelper.TryFindElementByAccessabilityId((AiStringHelper.VerifyChanges.View), DesktSession);
            if (viewVerifyChanges != null)
            {
                var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
                VerifyTestequipmentChangesInVerifyView(testequipment1, testequipmentChanged1, listViewChanges, 24);

                var textBoxComment = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Comment);
                TestHelper.SendKeysConverted(textBoxComment, "Erstes Prüfgerät ändern");

                var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
                if (TestHelper.WaitForElementToBeEnabledAndDisplayed(DesktSession, btnApply))
                {
                    btnApply.Click();
                    TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
                }
            }

            te2Node = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipment2.GetParentListWithTestequipment());
            te2Node.Click();

            //UpdateTestEquipment
            UpdateTestEquipment(QstSession, testEquipmentView, testequipment2, testequipmentChanged2);

            save = testEquipmentView.FindElementByAccessibilityId(AiStringHelper.TestEquipment.Save);
            if (TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, save))
            {
                save.Click();
            }

            viewVerifyChanges = TestHelper.TryFindElementByAccessabilityId((AiStringHelper.VerifyChanges.View), DesktSession);
            if (viewVerifyChanges != null)
            {
                var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
                VerifyTestequipmentChangesInVerifyView(testequipment2, testequipmentChanged2, listViewChanges, 6);

                var textBoxComment = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Comment);
                TestHelper.SendKeysConverted(textBoxComment, "Zweites Prüfgerät ändern");

                var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
                if (TestHelper.WaitForElementToBeEnabledAndDisplayed(DesktSession, btnApply))
                {
                    btnApply.Click();
                    TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
                }
            }

            //AssertChanges
            var te1ChangedNode = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipmentChanged1.GetParentListWithTestequipment());
            te1ChangedNode.Click();
            AssertTestequipment(QstSession, testequipmentChanged1);

            var te2ChangedNode = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipmentChanged2.GetParentListWithTestequipment());
            te2ChangedNode.Click();
            AssertTestequipment(QstSession, testequipmentChanged2);

            DeleteTestequipment(QstSession, testequipmentChanged1);
            DeleteTestequipment(QstSession, testequipmentChanged2);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestEquipment);

            testEquipmentView = TestHelper.FindElementWithWait(AiStringHelper.TestEquipment.View, QstSession);
            teTreeViewRootNode = TestHelper.FindElementByAiWithWaitFromParent(testEquipmentView, AiStringHelper.TestEquipment.TestEquipmentTreeViewRoot, QstSession);

            te1Node = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipment1.GetParentListWithTestequipment());

            Assert.IsNull(te1Node);
            te2Node = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipment1.GetParentListWithTestequipment());
            Assert.IsNull(te2Node);
            te1ChangedNode = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipment1.GetParentListWithTestequipment());
            Assert.IsNull(te1ChangedNode);
            te2ChangedNode = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipment2.GetParentListWithTestequipment());
            Assert.IsNull(te2ChangedNode);
        }

        private static void CreateTestequipment(WindowsDriver<WindowsElement> QstSession, Testequipment testEquipment)
        {
            var testquipmentView = TestHelper.FindElementWithWait(AiStringHelper.TestEquipment.View, QstSession);
            var add = testquipmentView.FindElementByAccessibilityId(AiStringHelper.TestEquipment.Add);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, add);
            add.Click();
            Thread.Sleep(200);
            var assistantSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.Assistant.View, TestConfiguration.GetWindowsApplicationDriverUrl());
            var textInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputText, assistantSession);
            textInput.Clear();
            TestHelper.SendKeysConverted(textInput, testEquipment.SerialNumber);
            var assistantNextBtn = TestHelper.FindElementWithWait(AiStringHelper.Assistant.Next, assistantSession);
            AssertAssistantListEntry(assistantSession, testEquipment.SerialNumber, AssistantStringHelper.Testequipment.SerialNumber);
            assistantNextBtn.Click();

            textInput.Clear();
            TestHelper.SendKeysConverted(textInput, testEquipment.InventoryNumber);
            AssertAssistantListEntry(assistantSession, testEquipment.InventoryNumber, AssistantStringHelper.Testequipment.InventoryNumber);
            assistantNextBtn.Click();

            //Timeout damit das listInput gefunden wird, unklar warum beim Buildserver withWait nicht greift
            Thread.Sleep(500);
            var listInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputList, assistantSession);
            var type = listInput.FindElementByAccessibilityId(testEquipment.Model.Type);
            type.Click();
            AssertAssistantListEntry(assistantSession, testEquipment.Model.Type, AssistantStringHelper.Testequipment.TestequipmentType);
            assistantNextBtn.Click();

            var model = listInput.FindElementByAccessibilityId(testEquipment.Model.Name);
            model.Click();
            AssertAssistantListEntry(assistantSession, testEquipment.Model.Name, AssistantStringHelper.Testequipment.TestequipmentModel);
            assistantNextBtn.Click();

            //TODO Einbauen wenn Status im Assistenten implementiert wurde
            /*var status = FindOrCreateListEntry(testEquipment.Status, assistantSession, listInput);
            status.Click();
            assistantNextBtn.Click();
            */

            var inputDate = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputDate, assistantSession);
            TestHelper.SendDate(testEquipment.LastCalibrationDate, inputDate);
            AssertAssistantListEntry(assistantSession, TestHelper.GetDateString(testEquipment.LastCalibrationDate), AssistantStringHelper.Testequipment.LastCalibrationDate);
            assistantNextBtn.Click();

            var inputInteger = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputInteger, assistantSession);
            inputInteger.SendKeys(testEquipment.Interval.ToString());
            AssertAssistantListEntry(assistantSession, testEquipment.Interval.ToString(currentCulture), AssistantStringHelper.Testequipment.Interval, "Tage");
            assistantNextBtn.Click();

            textInput.Clear();
            TestHelper.SendKeysConverted(textInput, testEquipment.CalibrationNorm);
            AssertAssistantListEntry(assistantSession, testEquipment.CalibrationNorm, AssistantStringHelper.Testequipment.CalibrationNorm);
            assistantNextBtn.Click();

            textInput.Clear();
            TestHelper.SendKeysConverted(textInput, testEquipment.FirmwareVersion);
            AssertAssistantListEntry(assistantSession, testEquipment.FirmwareVersion, AssistantStringHelper.Testequipment.FirmwareVersion);
            assistantNextBtn.Click();

            if (testEquipment.Model.Type == TestequipmentModel.TestequipmentType.TestWrench.ToString())
            {
                var inputFloatingPoint = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputFloatingPoint, assistantSession);
                inputFloatingPoint.SendKeys(testEquipment.MinCapacity.ToString());
                AssertAssistantListEntry(assistantSession, testEquipment.MinCapacity.ToString(currentCulture), AssistantStringHelper.Testequipment.CapacityMinimum, AssistantStringHelper.UnitStrings.Nm);
                assistantNextBtn.Click();

                inputFloatingPoint.SendKeys(testEquipment.MaxCapacity.ToString());
                AssertAssistantListEntry(assistantSession, testEquipment.MaxCapacity.ToString(currentCulture), AssistantStringHelper.Testequipment.CapacityMaximum, AssistantStringHelper.UnitStrings.Nm);
                assistantNextBtn.Click();
            }

            if (testEquipment.Model.UseProcess)
            {
                var inputBoolean = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputBoolean, assistantSession);
                if (inputBoolean.Selected && !testEquipment.UseProcess || !inputBoolean.Selected && testEquipment.UseProcess)
                {
                    inputBoolean.Click();
                }
                AssertAssistantListEntry(assistantSession, testEquipment.UseProcess.ToString(), AssistantStringHelper.Testequipment.UseProcess, "", true);
                assistantNextBtn.Click();
            }

            if (testEquipment.Model.UseRotating)
            {
                var inputBoolean = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputBoolean, assistantSession);
                if (inputBoolean.Selected && !testEquipment.UseRotating || !inputBoolean.Selected && testEquipment.UseRotating)
                {
                    inputBoolean.Click();
                }
                AssertAssistantListEntry(assistantSession, testEquipment.UseRotating.ToString(), AssistantStringHelper.Testequipment.UseRotating, "", true);
                assistantNextBtn.Click();
            }
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
        }
        private static void AssertTestequipment(WindowsDriver<WindowsElement> QstSession, Testequipment testEquipment, bool withFeatures = true)
        {
            var testEquipmentView = TestHelper.FindElementWithWait(AiStringHelper.TestEquipment.View, QstSession);

            var testequipmentTab = testEquipmentView.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TeTab);
            testequipmentTab.Click();

            var serialnumber = testequipmentTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.SerialNumber);
            Assert.AreEqual(testEquipment.SerialNumber, serialnumber.Text);

            var inventoryNumber = testequipmentTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.InventoryNumber);
            Assert.AreEqual(testEquipment.InventoryNumber, inventoryNumber.Text);

            var model = testequipmentTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.TestEquipmentModel);
            Assert.AreEqual(testEquipment.Model.Name, TestHelper.GetSelectedComboboxString(QstSession, model));

            var manufacturer = testequipmentTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.Manufacturer);
            Assert.AreEqual(testEquipment.Model.GetManufacturer(), manufacturer.Text);

            var dataGateVersion = testequipmentTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.DataGateVersion);
            Assert.AreEqual(testEquipment.Model.DataGateVersion, dataGateVersion.GetAttribute("Name"));

            //TODO Status entkommentieren wenn eingebaut
            /*var status = testequipmentTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.Status);
            Assert.AreEqual(testEquipment.Status, GetSelectedComboboxString(status));*/

            var firmwareVersion = testequipmentTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.FirmwareVersion);
            Assert.AreEqual(testEquipment.FirmwareVersion, firmwareVersion.Text);

            //Datum steckt im Datepicker
            var lastCalibration = testequipmentTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.LastCalibration);
            //Format von lastCalibration.Text "11/26/2020 12:00:00 AM"
            Assert.AreEqual(testEquipment.LastCalibrationDate.ToString("M/d/yyyy", currentCulture), lastCalibration.Text.Split(" ")[0]);

            testEquipmentView = TestHelper.FindElementWithWait(AiStringHelper.TestEquipment.View, QstSession);
            testequipmentTab = testEquipmentView.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TeTab);
            var calibrationInterval = testequipmentTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.CalibrationInterval);
            Assert.AreEqual(testEquipment.Interval.ToString(), calibrationInterval.Text);

            //TODO Einheitliches Datumsformat???
            var nextCalibration = testequipmentTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.NextCalibration);
            Assert.AreEqual(testEquipment.LastCalibrationDate.AddDays(testEquipment.Interval).ToString("dd/MM/yyyy", currentCulture), nextCalibration.Text);

            var calibrationNorm = testequipmentTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.CalibrationNorm);
            Assert.AreEqual(testEquipment.CalibrationNorm, calibrationNorm.Text);

            if (testEquipment.Model.Type == TestequipmentModel.TestequipmentType.TestWrench)
            {
                var minCapacity = testequipmentTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.MinCapacity);
                Assert.AreEqual(testEquipment.MinCapacity.ToString(numberFormatThreeDecimals, currentCulture), minCapacity.Text);

                var maxCapacity = testequipmentTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.MaxCapacity);
                Assert.AreEqual(testEquipment.MaxCapacity.ToString(numberFormatThreeDecimals, currentCulture), maxCapacity.Text);
            }
            if (testEquipment.Model.UseProcess)
            {
                var useProcess = testequipmentTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.UseForProcess);
                Assert.AreEqual(testEquipment.UseProcess, useProcess.Selected);
            }

            if (testEquipment.Model.UseRotating)
            {
                var useRotating = testequipmentTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.UseForRotating);
                Assert.AreEqual(testEquipment.UseRotating, useRotating.Selected);
            }

            if (withFeatures)
            {
                //Wenn mindestens eins im TestequipmentModel ausgewählt FeatureTab prüfen
                if (testEquipment.ModelHasFeaturesEnabled())
                {
                    var featureTab = testEquipmentView.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TeFeaturesTab);
                    featureTab.Click();

                    var transferUser = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferUser, featureTab);
                    if (testEquipment.Model.TransferUser)
                    {
                        Assert.AreEqual(testEquipment.TransferUser, transferUser.Selected);
                    }
                    else
                    {
                        Assert.IsNull(transferUser);
                    }

                    var transferAdapter = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferAdapter, featureTab);
                    if (testEquipment.Model.TransferAdapter)
                    {
                        Assert.AreEqual(testEquipment.TransferAdapter, transferAdapter.Selected);
                    }
                    else
                    {
                        Assert.IsNull(transferAdapter);
                    }

                    var transferTransducer = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferTransducer, featureTab);
                    if (testEquipment.Model.TransferTransducer)
                    {
                        Assert.AreEqual(testEquipment.TransferTransducer, transferTransducer.Selected);
                    }
                    else
                    {
                        Assert.IsNull(transferTransducer);
                    }

                    var transferAttributes = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferAttributes, featureTab);
                    if (testEquipment.Model.TransferAttributes)
                    {
                        Assert.AreEqual(testEquipment.TransferAttributes, transferAttributes.Selected);
                    }
                    else
                    {
                        Assert.IsNull(transferAttributes);
                    }

                    var transferPicture = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferPictures, featureTab);
                    if (testEquipment.Model.TransferPictures)
                    {
                        Assert.AreEqual(testEquipment.TransferPictures, transferPicture.Selected);
                    }
                    else
                    {
                        Assert.IsNull(transferPicture);
                    }

                    var transferNewLimits = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferNewLimits, featureTab);
                    if (testEquipment.Model.TransferNewLimits)
                    {
                        Assert.AreEqual(testEquipment.TransferNewLimits, transferNewLimits.Selected);
                    }
                    else
                    {
                        Assert.IsNull(transferNewLimits);
                    }

                    var transferCurves = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferCurves, featureTab);
                    if (testEquipment.Model.TransferCurves)
                    {
                        string transferCurvesSelected = TestHelper.GetSelectedComboboxString(QstSession, transferCurves);
                        Assert.AreEqual(testEquipment.TransferCurves, transferCurvesSelected);
                    }
                    else
                    {
                        Assert.IsNull(transferCurves);
                    }

                    var askForIdent = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.AskForIdent, featureTab);
                    if (testEquipment.Model.AskForIdent)
                    {
                        Assert.AreEqual(testEquipment.GetAskForIdentComboBoxString(), askForIdent.Text);
                    }
                    else
                    {
                        Assert.IsNull(askForIdent);
                    }

                    var askForSign = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.AskForSign, featureTab);
                    if (testEquipment.Model.AskForSign)
                    {
                        Assert.AreEqual(testEquipment.AskForSign, askForSign.Selected);
                    }
                    else
                    {
                        Assert.IsNull(askForSign);
                    }

                    var useErrorCodes = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.UseErrorCodes, featureTab);
                    if (testEquipment.Model.UseErrorCodes)
                    {
                        Assert.AreEqual(testEquipment.UseErrorCodes, useErrorCodes.Selected);
                    }
                    else
                    {
                        Assert.IsNull(useErrorCodes);
                    }

                    var performLooseCheck = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.PerformLooseCheck, featureTab);
                    if (testEquipment.Model.PerformLooseCheck)
                    {
                        Assert.AreEqual(testEquipment.PerformLooseCheck, performLooseCheck.Selected);
                    }
                    else
                    {
                        Assert.IsNull(performLooseCheck);
                    }

                    var mpCanBeDeleted = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.MpCanBeDeleted, featureTab);
                    if (testEquipment.Model.MpCanBeDeleted)
                    {
                        Assert.AreEqual(testEquipment.MpCanBeDeleted, mpCanBeDeleted.Selected);
                    }
                    else
                    {
                        Assert.IsNull(mpCanBeDeleted);
                    }

                    var confirmMp = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.ConfirmMp, featureTab);
                    if (testEquipment.Model.ConfirmMp)
                    {
                        string confirmMpSelected = TestHelper.GetSelectedComboboxString(QstSession, confirmMp);
                        Assert.AreEqual(testEquipment.ConfirmMp, confirmMpSelected);
                    }
                    else
                    {
                        Assert.IsNull(confirmMp);
                    }

                    var standardMethodsCanBeUsed = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.StandardMethodsCanBeUsed, featureTab);
                    if (testEquipment.Model.StandardMethodsCanBeUsed)
                    {
                        Assert.AreEqual(testEquipment.StandardMethodsCanBeUsed, standardMethodsCanBeUsed.Selected);
                    }
                    else
                    {
                        Assert.IsNull(standardMethodsCanBeUsed);
                    }
                }
            }
        }
        private static void VerifyTestequipmentChangesInVerifyView(Testequipment testequipment, Testequipment testequipmentChanged, AppiumWebElement listViewChanges, int numberOfExpectedChanges)
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
                    case "Serial number":
                        Assert.AreEqual(testequipment.SerialNumber, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(testequipmentChanged.SerialNumber, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Inventory number":
                        Assert.AreEqual(testequipment.InventoryNumber, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(testequipmentChanged.InventoryNumber, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Firmware version":
                        Assert.AreEqual(testequipment.FirmwareVersion, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(testequipmentChanged.FirmwareVersion, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Capacity minimum":
                        Assert.AreEqual(testequipment.MinCapacity.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(testequipmentChanged.MinCapacity.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Capacity maximum":
                        Assert.AreEqual(testequipment.MaxCapacity.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(testequipmentChanged.MaxCapacity.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Last calibration":
                        Assert.AreEqual(TestHelper.GetDateString(testequipment.LastCalibrationDate), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetDateString(testequipmentChanged.LastCalibrationDate), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Calibration interval":
                        Assert.AreEqual(testequipment.Interval.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(testequipmentChanged.Interval.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Calibration norm":
                        Assert.AreEqual(testequipment.CalibrationNorm, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(testequipmentChanged.CalibrationNorm, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "UseForCtl":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipment.UseProcess), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipmentChanged.UseProcess), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "UseForRot":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipment.UseRotating), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipmentChanged.UseRotating), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;

                    case "Transfer user":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipment.TransferUser), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipmentChanged.TransferUser), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Transfer adapter":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipment.TransferAdapter), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipmentChanged.TransferAdapter), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Transfer transducer":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipment.TransferTransducer), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipmentChanged.TransferTransducer), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Transfer attributes":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipment.TransferAttributes), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipmentChanged.TransferAttributes), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Transfer measurement point picture":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipment.TransferPictures), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipmentChanged.TransferPictures), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Transfer new limits":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipment.TransferNewLimits), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipmentChanged.TransferNewLimits), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Transfer curves":
                        Assert.AreEqual(testequipment.TransferCurves, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(testequipmentChanged.TransferCurves, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Ask for ident":
                        Assert.AreEqual(testequipment.AskForIdent, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(testequipmentChanged.AskForIdent, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Ask for sign":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipment.AskForSign), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipmentChanged.AskForSign), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Use error codes":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipment.UseErrorCodes), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipmentChanged.UseErrorCodes), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Perform loose check":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipment.PerformLooseCheck), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipmentChanged.PerformLooseCheck), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Measurements can be deleted":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipment.MpCanBeDeleted), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipmentChanged.MpCanBeDeleted), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Confirm measurements":
                        Assert.AreEqual(testequipment.ConfirmMp, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(testequipmentChanged.ConfirmMp, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "QST Standard Methods can be used":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipment.StandardMethodsCanBeUsed), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(testequipmentChanged.StandardMethodsCanBeUsed), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    default:
                        Assert.IsTrue(false, string.Format("Case '{0}' not implemented", changeTypeText));
                        break;
                }
                i++;
            }
        }
        private static void DeleteTestequipment(WindowsDriver<WindowsElement> QstSession, Testequipment te)
        {
            var teTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.TestEquipment.TestEquipmentTreeViewRoot, QstSession);

            var node = TestHelper.GetNode(QstSession, teTreeviewRootNode, te.GetParentListWithTestequipment());
            if (node != null)
            {
                node.Click();
                var delete = QstSession.FindElementByAccessibilityId(AiStringHelper.TestEquipment.Delete);
                //Warten bis Testequipment geladen wurde und der Deletebutton klickbar ist
                Assert.IsTrue(TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, 2, 300, delete), "Löschenbutton ist nicht sichtbar");
                delete.Click();

                var confirmBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
                Assert.IsTrue(TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, 2, 300, confirmBtn), "Bestätigungsbutton ist nicht sichtbar");
                confirmBtn.Click();
                TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            }
        }
        private static void UpdateTestEquipment(WindowsDriver<WindowsElement> QstSession, WindowsElement testEquipmentView, Testequipment testequipment, Testequipment testequipmentChanged)
        {
            var testequipmentTab = testEquipmentView.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TeTab);
            testequipmentTab.Click();

            var scrollviewer = TestHelper.FindElementByAiWithWaitFromParent(testequipmentTab, AiStringHelper.TestEquipment.TestEquipmentTabElements.ScrollViewer, QstSession);

            var serialnumber = scrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.SerialNumber);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, serialnumber);
            serialnumber.Clear();
            TestHelper.SendKeysConverted(serialnumber, testequipmentChanged.SerialNumber);

            var inventoryNumber = scrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.InventoryNumber);
            inventoryNumber.Clear();
            TestHelper.SendKeysConverted(inventoryNumber, testequipmentChanged.InventoryNumber);

            //TODO entkommentieren wenn Status implementiert ist
            //var status = scrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.Status);
            //status.Clear();
            //TestHelper.ClickComboBoxEntry(testequipmentChanged1.Status, status);

            var firmwareVersion = scrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.FirmwareVersion);
            firmwareVersion.Clear();
            TestHelper.SendKeysConverted(firmwareVersion, testequipmentChanged.FirmwareVersion);

            var lastCalibration = scrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.LastCalibration);
            var lastCalibrationTextBox = lastCalibration.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.DatePickerTextbox);
            //Clear funktioniert nur auf der Textbox (PART_TextBox) nicht direkt auf dem DatePicker
            lastCalibrationTextBox.Clear();
            TestHelper.SendKeysWithBackslash(QstSession, lastCalibrationTextBox, testequipmentChanged.LastCalibrationDate.ToString("MM/dd/yyyy", currentCulture));

            var calibrationInterval = scrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.CalibrationInterval);
            var calibrationIntervalTextBox = calibrationInterval.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.SyncFusionUpDownTextbox);
            calibrationIntervalTextBox.Clear();
            TestHelper.SendKeysConverted(calibrationIntervalTextBox, testequipmentChanged.Interval.ToString());

            var calibration = scrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.CalibrationNorm);
            calibration.Clear();
            TestHelper.SendKeysConverted(calibration, testequipmentChanged.CalibrationNorm);

            if (testequipment.Model.Type == TestequipmentModel.TestequipmentType.TestWrench.ToString())
            {
                var minCapacity = scrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.MinCapacity);
                minCapacity.Clear();
                minCapacity.SendKeys(Keys.ArrowRight);
                TestHelper.SendKeysConverted(minCapacity, testequipmentChanged.MinCapacity.ToString(numberFormatThreeDecimals, currentCulture));

                var maxCapacity = scrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.MaxCapacity);
                maxCapacity.Clear();
                maxCapacity.SendKeys(Keys.ArrowRight);
                TestHelper.SendKeysConverted(maxCapacity, testequipmentChanged.MaxCapacity.ToString(numberFormatThreeDecimals, currentCulture));
            }

            if (testequipment.Model.UseProcess)
            {
                var useProcess = scrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.UseForProcess);
                if (useProcess.Selected && !testequipmentChanged.UseProcess
                    || !useProcess.Selected && testequipmentChanged.UseProcess)
                {
                    useProcess.SendKeys(Keys.Escape);
                    useProcess.Click();
                }
            }
            if (testequipment.Model.UseRotating)
            {
                var useRotating = scrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentTabElements.UseForRotating);
                if (useRotating.Selected && !testequipmentChanged.UseProcess
                    || !useRotating.Selected && testequipmentChanged.UseProcess)
                {
                    useRotating.SendKeys(Keys.Escape);
                    useRotating.Click();
                }
            }

            SetTestequipmentFeatures(testEquipmentView, testequipment, testequipmentChanged);
        }

        private static void SetTestequipmentFeatures(WindowsElement testEquipmentView, Testequipment testequipment, Testequipment testequipmentChanged)
        {
            var teFeatureTab = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TeFeaturesTab, testEquipmentView);

            if (testequipmentChanged.ModelHasFeaturesEnabled())
            {
                teFeatureTab.Click();

                var transferUser = teFeatureTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferUser);
                if (transferUser.Selected && !testequipmentChanged.TransferUser
                    || !transferUser.Selected && testequipmentChanged.TransferUser)
                {
                    transferUser.SendKeys(Keys.Escape);
                    transferUser.Click();
                }

                var transferAdapter = teFeatureTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferAdapter);
                if (transferAdapter.Selected && !testequipmentChanged.TransferAdapter
                    || !transferAdapter.Selected && testequipmentChanged.TransferAdapter)
                {
                    transferAdapter.SendKeys(Keys.Escape);
                    transferAdapter.Click();
                }

                var transferTransducer = teFeatureTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferTransducer);
                if (transferTransducer.Selected && !testequipmentChanged.TransferTransducer
                    || !transferTransducer.Selected && testequipmentChanged.TransferTransducer)
                {
                    transferTransducer.SendKeys(Keys.Escape);
                    transferTransducer.Click();
                }

                var transferAttributes = teFeatureTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferAttributes);
                if (transferAttributes.Selected && !testequipmentChanged.TransferAttributes
                    || !transferAttributes.Selected && testequipmentChanged.TransferAttributes)
                {
                    transferAttributes.SendKeys(Keys.Escape);
                    transferAttributes.Click();
                }

                var transferPictures = teFeatureTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferPictures);
                if (transferPictures.Selected && !testequipmentChanged.TransferPictures
                    || !transferPictures.Selected && testequipmentChanged.TransferPictures)
                {
                    transferPictures.SendKeys(Keys.Escape);
                    transferPictures.Click();
                }

                var transferNewLimits = teFeatureTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferNewLimits);
                if (transferNewLimits.Selected && !testequipmentChanged.TransferNewLimits
                    || !transferNewLimits.Selected && testequipmentChanged.TransferNewLimits)
                {
                    transferNewLimits.SendKeys(Keys.Escape);
                    transferNewLimits.Click();
                }

                var curves = teFeatureTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferCurves);
                TestHelper.ClickComboBoxEntry(QstSession, testequipmentChanged.TransferCurves, curves);


                var askForIdent = teFeatureTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.AskForIdent);
                TestHelper.ClickComboBoxEntry(QstSession, testequipmentChanged.AskForIdent, askForIdent);

                var askForSign = teFeatureTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.AskForSign);
                if (askForSign.Selected && !testequipmentChanged.AskForSign
                    || !askForSign.Selected && testequipmentChanged.AskForSign)
                {
                    askForSign.SendKeys(Keys.Escape);
                    askForSign.Click();
                }

                var useErrorCodes = teFeatureTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.UseErrorCodes);
                if (useErrorCodes.Selected && !testequipmentChanged.UseErrorCodes
                    || !useErrorCodes.Selected && testequipmentChanged.UseErrorCodes)
                {
                    useErrorCodes.SendKeys(Keys.Escape);
                    useErrorCodes.Click();
                }

                var performLooseCheck = teFeatureTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.PerformLooseCheck);
                if (performLooseCheck.Selected && !testequipmentChanged.PerformLooseCheck
                    || !performLooseCheck.Selected && testequipmentChanged.PerformLooseCheck)
                {
                    performLooseCheck.SendKeys(Keys.Escape);
                    performLooseCheck.Click();
                }

                var mpCanBeDeleted = teFeatureTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.MpCanBeDeleted);
                if (mpCanBeDeleted.Selected && !testequipmentChanged.MpCanBeDeleted
                    || !mpCanBeDeleted.Selected && testequipmentChanged.MpCanBeDeleted)
                {
                    mpCanBeDeleted.SendKeys(Keys.Escape);
                    mpCanBeDeleted.Click();
                }

                var confirmMp = teFeatureTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.ConfirmMp);
                TestHelper.ClickComboBoxEntry(QstSession, testequipmentChanged.ConfirmMp, confirmMp);

                var standardMethodsCanBeUsed = teFeatureTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.StandardMethodsCanBeUsed);
                if (standardMethodsCanBeUsed.Selected && !testequipmentChanged.StandardMethodsCanBeUsed
                    || !standardMethodsCanBeUsed.Selected && testequipmentChanged.StandardMethodsCanBeUsed)
                {
                    standardMethodsCanBeUsed.SendKeys(Keys.Escape);
                    standardMethodsCanBeUsed.Click();
                }
            }
            else
            {
                Assert.IsNull(teFeatureTab);
            }
        }
        private static void VerifyTestequipmentModelChangesInVerifyView(TestequipmentModel teModel, TestequipmentModel teModelChanged, AppiumWebElement listViewChanges, int numberOfExpectedChanges)
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
                    case "DataGate Version":
                        Assert.AreEqual(teModel.DataGateVersion, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(teModelChanged.DataGateVersion, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Use for process":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModel.UseProcess), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModelChanged.UseProcess), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Use for rotating":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModel.UseRotating), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModelChanged.UseRotating), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Transfer user":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModel.TransferUser), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModelChanged.TransferUser), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Transfer adapter":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModel.TransferAdapter), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModelChanged.TransferAdapter), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Transfer transducer":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModel.TransferTransducer), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModelChanged.TransferTransducer), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Transfer attributes":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModel.TransferAttributes), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModelChanged.TransferAttributes), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Transfer measurement point picture":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModel.TransferPictures), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModelChanged.TransferPictures), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Transfer new limits":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModel.TransferNewLimits), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModelChanged.TransferNewLimits), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Transfer curves":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModel.TransferCurves), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModelChanged.TransferCurves), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Ask for ident":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModel.AskForIdent), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModelChanged.AskForIdent), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Ask for sign":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModel.AskForSign), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModelChanged.AskForSign), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Use error codes":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModel.UseErrorCodes), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModelChanged.UseErrorCodes), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Perform loose check":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModel.PerformLooseCheck), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModelChanged.PerformLooseCheck), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Measurements can be deleted":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModel.MpCanBeDeleted), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModelChanged.MpCanBeDeleted), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Confirm measurements":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModel.ConfirmMp), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModelChanged.ConfirmMp), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "QST Standard Methods can be used":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModel.StandardMethodsCanBeUsed), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(teModelChanged.StandardMethodsCanBeUsed), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Path to test equipment driver":
                        Assert.AreEqual(teModel.TestequipmentDriver, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(teModelChanged.TestequipmentDriver, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Path to status file":
                        Assert.AreEqual(teModel.StatusFile, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(teModelChanged.StatusFile, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Path from QST to test equipment file":
                        Assert.AreEqual(teModel.QstToTestequipment, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(teModelChanged.QstToTestequipment, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Path from test equipment to QST file":
                        Assert.AreEqual(teModel.TestequipmentToQst, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(teModelChanged.TestequipmentToQst, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    default:
                        Assert.IsTrue(false, string.Format("Case '{0}' not implemented", changeTypeText));
                        break;
                }
                i++;
            }
        }
        private static void UpdateTestEquipmentModel(WindowsDriver<WindowsElement> QstSession, TestequipmentModel testequipmentModel)
        {
            var testEquipmentView = TestHelper.FindElementWithWait(AiStringHelper.TestEquipment.View, QstSession);

            SelectTestequipmentModelNode(QstSession, testequipmentModel, testEquipmentView);

            var testEquipmentModelTab = TestHelper.FindElementWithWait(AiStringHelper.TestEquipment.TeModelTab, QstSession);
            var scrollviewer = TestHelper.FindElementByAiWithWaitFromParent(testEquipmentModelTab, AiStringHelper.TestEquipment.TestEquipmentModelTabElements.ScrollViewer, QstSession);
            TestHelper.ScrollUp(scrollviewer);

            var modelName = testEquipmentView.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.Name);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, modelName);
            //Name bleibt gleich

            var modelDgVersion = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.DataGateVersion);
            TestHelper.ClickComboBoxEntry(QstSession, testequipmentModel.DataGateVersion, modelDgVersion);

            var modelUseForProcess = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.UseForProcess);
            if (modelUseForProcess.Selected && !testequipmentModel.UseProcess
                || !modelUseForProcess.Selected && testequipmentModel.UseProcess)
            {
                modelUseForProcess.SendKeys(Keys.Escape);
                modelUseForProcess.Click();
            }

            var modelUseForRotating = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.UseForRotating);
            if (modelUseForRotating.Selected && !testequipmentModel.UseRotating
                || !modelUseForRotating.Selected && testequipmentModel.UseRotating)
            {
                modelUseForRotating.SendKeys(Keys.Escape);
                modelUseForRotating.Click();
            }

            var modelTransferUser = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferUser);
            if (modelTransferUser.Selected && !testequipmentModel.TransferUser
                || !modelTransferUser.Selected && testequipmentModel.TransferUser)
            {
                modelTransferUser.SendKeys(Keys.Escape);
                modelTransferUser.Click();
            }

            var modelTransferAdapter = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferAdapter);
            if (modelTransferAdapter.Selected && !testequipmentModel.TransferAdapter
                || !modelTransferAdapter.Selected && testequipmentModel.TransferAdapter)
            {
                modelTransferAdapter.SendKeys(Keys.Escape);
                modelTransferAdapter.Click();
            }

            var modelTransferTransducer = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferTransducer);
            if (modelTransferTransducer.Selected && !testequipmentModel.TransferTransducer
                || !modelTransferTransducer.Selected && testequipmentModel.TransferTransducer)
            {
                modelTransferTransducer.SendKeys(Keys.Escape);
                modelTransferTransducer.Click();
            }

            var modelTransferAttributes = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferAttributes);
            if (modelTransferAttributes.Selected && !testequipmentModel.TransferAttributes
                || !modelTransferAttributes.Selected && testequipmentModel.TransferAttributes)
            {
                modelTransferAttributes.SendKeys(Keys.Escape);
                modelTransferAttributes.Click();
            }

            var modelTransferPictures = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferPictures);
            if (modelTransferPictures.Selected && !testequipmentModel.TransferPictures
                || !modelTransferPictures.Selected && testequipmentModel.TransferPictures)
            {
                modelTransferPictures.SendKeys(Keys.Escape);
                modelTransferPictures.Click();
            }

            var modelTransferNewLimits = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferNewLimits);
            if (modelTransferNewLimits.Selected && !testequipmentModel.TransferNewLimits
                || !modelTransferNewLimits.Selected && testequipmentModel.TransferNewLimits)
            {
                modelTransferNewLimits.SendKeys(Keys.Escape);
                modelTransferNewLimits.Click();
            }

            var modelTransferCurves = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferCurves);
            if (modelTransferCurves.Selected && !testequipmentModel.TransferCurves
                || !modelTransferCurves.Selected && testequipmentModel.TransferCurves)
            {
                modelTransferCurves.SendKeys(Keys.Escape);
                modelTransferCurves.Click();
            }

            var modelAskForIdent = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.AskForIdent);
            if (modelAskForIdent.Selected && !testequipmentModel.AskForIdent
                || !modelAskForIdent.Selected && testequipmentModel.AskForIdent)
            {
                modelAskForIdent.SendKeys(Keys.Escape);
                modelAskForIdent.Click();
            }

            var modelAskForSign = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.AskForSign);
            if (modelAskForSign.Selected && !testequipmentModel.AskForSign
                || !modelAskForSign.Selected && testequipmentModel.AskForSign)
            {
                modelAskForSign.SendKeys(Keys.Escape);
                modelAskForSign.Click();
            }

            var modelUseErrorCodes = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.UseErrorCodes);
            if (modelUseErrorCodes.Selected && !testequipmentModel.UseErrorCodes
                || !modelUseErrorCodes.Selected && testequipmentModel.UseErrorCodes)
            {
                modelUseErrorCodes.SendKeys(Keys.Escape);
                modelUseErrorCodes.Click();
            }

            var modelPerformLooseCheck = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.PerformLooseCheck);
            if (modelPerformLooseCheck.Selected && !testequipmentModel.PerformLooseCheck
                || !modelPerformLooseCheck.Selected && testequipmentModel.PerformLooseCheck)
            {
                modelPerformLooseCheck.SendKeys(Keys.Escape);
                modelPerformLooseCheck.Click();
            }

            var modelMpCanBeDeleted = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.MpCanBeDeleted);
            if (modelMpCanBeDeleted.Selected && !testequipmentModel.MpCanBeDeleted
                || !modelMpCanBeDeleted.Selected && testequipmentModel.MpCanBeDeleted)
            {
                modelMpCanBeDeleted.SendKeys(Keys.Escape);
                modelMpCanBeDeleted.Click();
            }

            var modelConfirmMp = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.ConfirmMp);
            if (modelConfirmMp.Selected && !testequipmentModel.ConfirmMp
                || !modelConfirmMp.Selected && testequipmentModel.ConfirmMp)
            {
                modelConfirmMp.SendKeys(Keys.Escape);
                modelConfirmMp.Click();
            }

            var modelStandardMethodsCanBeUsed = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.StandardMethodsCanBeUsed);
            if (modelStandardMethodsCanBeUsed.Selected && !testequipmentModel.StandardMethodsCanBeUsed
                || !modelStandardMethodsCanBeUsed.Selected && testequipmentModel.StandardMethodsCanBeUsed)
            {
                modelStandardMethodsCanBeUsed.SendKeys(Keys.Escape);
                modelStandardMethodsCanBeUsed.Click();
            }

            var modelTeDriver = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TeDriver);
            if (modelTeDriver.Text != testequipmentModel.TestequipmentDriver)
            {
                modelTeDriver.Clear();
                TestHelper.SendKeysWithBackslash(QstSession, modelTeDriver, testequipmentModel.TestequipmentDriver);
            }

            var modelStatusFile = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.StatusFile);
            if (modelStatusFile.Text != testequipmentModel.StatusFile)
            {
                modelStatusFile.Clear();
                TestHelper.SendKeysWithBackslash(QstSession, modelStatusFile, testequipmentModel.StatusFile);
            }

            var modelQstToTest = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.QstToTeFile);
            if (modelQstToTest.Text != testequipmentModel.QstToTestequipment)
            {
                modelQstToTest.Clear();
                TestHelper.SendKeysWithBackslash(QstSession, modelQstToTest, testequipmentModel.QstToTestequipment);
            }

            var modelTestToQst = testEquipmentModelTab.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TeToQstFile);
            if (modelTestToQst.Text != testequipmentModel.TestequipmentToQst)
            {
                modelTestToQst.Clear();
                TestHelper.SendKeysWithBackslash(QstSession, modelTestToQst, testequipmentModel.TestequipmentToQst);
            }
        }
        private static void SelectTestequipmentModelNode(WindowsDriver<WindowsElement> QstSession, TestequipmentModel testequipmentModel, AppiumWebElement testEquipmentView)
        {
            var teTreeViewRootNode = TestHelper.FindElementByAiWithWaitFromParent(testEquipmentView, AiStringHelper.TestEquipment.TestEquipmentTreeViewRoot, QstSession);

            var modelNode = TestHelper.GetNode(QstSession, teTreeViewRootNode, testequipmentModel.GetParentListWithTestequipmentModel());
            var nameNode = modelNode.FindElementByXPath(string.Format("*/*[@ClassName =\"TextBlock\"][@Name=\"{0}\"]", testequipmentModel.Name));
            Assert.IsNotNull(nameNode, string.Format("TestequipmentModelNameElement wurde nicht gefunden: {0}", testequipmentModel.Name));

            nameNode.Click();
        }
        //Nur prüfen ob Feature-Elemente vorhanden sind anhand Model
        private static void AssertTestequipmentFeaturesForModel(WindowsDriver<WindowsElement> QstSession, Testequipment testequipment)
        {
            var testEquipmentView = TestHelper.FindElementWithWait(AiStringHelper.TestEquipment.View, QstSession);

            var featureTab = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TeFeaturesTab, testEquipmentView);
            if (testequipment.ModelHasFeaturesEnabled())
            {
                featureTab.Click();

                var transferUser = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferUser, featureTab);
                if (testequipment.Model.TransferUser)
                {
                    Assert.IsNotNull(transferUser);
                }
                else
                {
                    Assert.IsNull(transferUser);
                }

                var transferAdapter = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferAdapter, featureTab);
                if (testequipment.Model.TransferAdapter)
                {
                    Assert.IsNotNull(transferAdapter);
                }
                else
                {
                    Assert.IsNull(transferAdapter);
                }

                var transferTransducer = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferTransducer, featureTab);
                if (testequipment.Model.TransferTransducer)
                {
                    Assert.IsNotNull(transferTransducer);
                }
                else
                {
                    Assert.IsNull(transferTransducer);
                }

                var transferAttributes = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferAttributes, featureTab);
                if (testequipment.Model.TransferAttributes)
                {
                    Assert.IsNotNull(transferAttributes);
                }
                else
                {
                    Assert.IsNull(transferAttributes);
                }

                var transferPicture = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferPictures, featureTab);
                if (testequipment.Model.TransferPictures)
                {
                    Assert.IsNotNull(transferPicture);
                }
                else
                {
                    Assert.IsNull(transferPicture);
                }

                var transferNewLimits = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferNewLimits, featureTab);
                if (testequipment.Model.TransferNewLimits)
                {
                    Assert.IsNotNull(transferNewLimits);
                }
                else
                {
                    Assert.IsNull(transferNewLimits);
                }

                var transferCurves = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.TransferCurves, featureTab);
                if (testequipment.Model.TransferCurves)
                {
                    Assert.IsNotNull(transferCurves);
                }
                else
                {
                    Assert.IsNull(transferCurves);
                }

                var askForIdent = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.AskForIdent, featureTab);
                if (testequipment.Model.AskForIdent)
                {
                    Assert.IsNotNull(askForIdent.Text);
                }
                else
                {
                    Assert.IsNull(askForIdent);
                }

                var askForSign = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.AskForSign, featureTab);
                if (testequipment.Model.AskForSign)
                {
                    Assert.IsNotNull(askForSign);
                }
                else
                {
                    Assert.IsNull(askForSign);
                }

                var useErrorCodes = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.UseErrorCodes, featureTab);
                if (testequipment.Model.UseErrorCodes)
                {
                    Assert.IsNotNull(useErrorCodes);
                }
                else
                {
                    Assert.IsNull(useErrorCodes);
                }

                var performLooseCheck = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.PerformLooseCheck, featureTab);
                if (testequipment.Model.PerformLooseCheck)
                {
                    Assert.IsNotNull(performLooseCheck);
                }
                else
                {
                    Assert.IsNull(performLooseCheck);
                }

                var mpCanBeDeleted = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.MpCanBeDeleted, featureTab);
                if (testequipment.Model.MpCanBeDeleted)
                {
                    Assert.IsNotNull(mpCanBeDeleted);
                }
                else
                {
                    Assert.IsNull(mpCanBeDeleted);
                }

                var confirmMp = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.ConfirmMp, featureTab);
                if (testequipment.Model.ConfirmMp)
                {
                    Assert.IsNotNull(confirmMp);
                }
                else
                {
                    Assert.IsNull(confirmMp);
                }

                var standardMethodsCanBeUsed = TestHelper.TryFindElementBy(AiStringHelper.TestEquipment.TestEquipmentFeaturesTabElements.StandardMethodsCanBeUsed, featureTab);
                if (testequipment.Model.StandardMethodsCanBeUsed)
                {
                    Assert.IsNotNull(standardMethodsCanBeUsed);
                }
                else
                {
                    Assert.IsNull(standardMethodsCanBeUsed);
                }
            }
            else
            {
                Assert.IsNull(featureTab, "FeatureTab sollte ausgeblendet sein");
            }
        }
        private static void AssertTestequipmentModel(WindowsDriver<WindowsElement> QstSession, TestequipmentModel testequipmentModel)
        {
            var testEquipmentModelTab = TestHelper.FindElementWithWait(AiStringHelper.TestEquipment.TeModelTab, QstSession);
            var modelTabScrollviewer = TestHelper.FindElementByAiWithWaitFromParent(testEquipmentModelTab, AiStringHelper.TestEquipment.TestEquipmentModelTabElements.ScrollViewer, QstSession);
            if (modelTabScrollviewer != null)
            {
                modelTabScrollviewer.SendKeys(Keys.PageUp);
            }
            var name = TestHelper.FindElementByAiWithWaitFromParent(modelTabScrollviewer, AiStringHelper.TestEquipment.TestEquipmentModelTabElements.Name, QstSession);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, name);
            Assert.AreEqual(testequipmentModel.Name, name.Text);

            var dataGateVersion = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.DataGateVersion);
            Assert.AreEqual(testequipmentModel.DataGateVersion, TestHelper.GetSelectedComboboxString(QstSession, dataGateVersion));

            var useProcess = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.UseForProcess);
            Assert.AreEqual(testequipmentModel.UseProcess, useProcess.Selected);

            var useRotating = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.UseForRotating);
            Assert.AreEqual(testequipmentModel.UseRotating, useRotating.Selected);

            var transferUser = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferUser);
            Assert.AreEqual(testequipmentModel.TransferUser, transferUser.Selected);

            var transferAdapter = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferAdapter);
            Assert.AreEqual(testequipmentModel.TransferAdapter, transferAdapter.Selected);

            var transferTransducer = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferTransducer);
            Assert.AreEqual(testequipmentModel.TransferTransducer, transferTransducer.Selected);

            var transferAttributes = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferAttributes);
            Assert.AreEqual(testequipmentModel.TransferAttributes, transferAttributes.Selected);

            var transferPictures = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferPictures);
            Assert.AreEqual(testequipmentModel.TransferPictures, transferPictures.Selected);

            var transferNewLimits = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferNewLimits);
            Assert.AreEqual(testequipmentModel.TransferNewLimits, transferNewLimits.Selected);

            var transferCurves = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferCurves);
            Assert.AreEqual(testequipmentModel.TransferCurves, transferCurves.Selected);

            var askForIdent = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.AskForIdent);
            Assert.AreEqual(testequipmentModel.AskForIdent, askForIdent.Selected);

            var askForSign = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.AskForSign);
            Assert.AreEqual(testequipmentModel.AskForSign, askForSign.Selected);

            var useErrorCodes = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.UseErrorCodes);
            Assert.AreEqual(testequipmentModel.UseErrorCodes, useErrorCodes.Selected);

            var performLooseCheck = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.PerformLooseCheck);
            Assert.AreEqual(testequipmentModel.PerformLooseCheck, performLooseCheck.Selected);

            var mpCanBeDeleted = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.MpCanBeDeleted);
            Assert.AreEqual(testequipmentModel.MpCanBeDeleted, mpCanBeDeleted.Selected);

            var confirmMp = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.ConfirmMp);
            Assert.AreEqual(testequipmentModel.ConfirmMp, confirmMp.Selected);

            var standardMethodsCanBeUsed = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.StandardMethodsCanBeUsed);
            Assert.AreEqual(testequipmentModel.StandardMethodsCanBeUsed, standardMethodsCanBeUsed.Selected);


            var testequipmentDriver = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TeDriver);
            Assert.AreEqual(testequipmentModel.TestequipmentDriver, testequipmentDriver.Text);

            var statusFile = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.StatusFile);
            Assert.AreEqual(testequipmentModel.StatusFile, statusFile.Text);

            var qstToTestequipment = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.QstToTeFile);
            Assert.AreEqual(testequipmentModel.QstToTestequipment, qstToTestequipment.Text);

            var testequipmentToQST = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TeToQstFile);
            Assert.AreEqual(testequipmentModel.TestequipmentToQst, testequipmentToQST.Text);
        }
        private static void AssertTestequipmentModelReadOnly(WindowsDriver<WindowsElement> QstSession)
        {
            var testEquipmentModelTab = TestHelper.FindElementWithWait(AiStringHelper.TestEquipment.TeModelTab, QstSession);
            var modelTabScrollviewer = TestHelper.FindElementByAiWithWaitFromParent(testEquipmentModelTab, AiStringHelper.TestEquipment.TestEquipmentModelTabElements.ScrollViewer, QstSession);
            if (modelTabScrollviewer != null)
            {
                modelTabScrollviewer.SendKeys(Keys.PageUp);
            }

            var name = TestHelper.FindElementByAiWithWaitFromParent(modelTabScrollviewer, AiStringHelper.TestEquipment.TestEquipmentModelTabElements.Name, QstSession);
            TestHelper.WaitForElementToBeDisplayed(QstSession, name);
            Assert.IsFalse(name.Enabled);

            var dataGateVersion = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.DataGateVersion);
            Assert.IsFalse(dataGateVersion.Enabled);

            var useProcess = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.UseForProcess);
            Assert.IsFalse(useProcess.Enabled);

            var useRotating = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.UseForRotating);
            Assert.IsFalse(useRotating.Enabled);

            var transferUser = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferUser);
            Assert.IsFalse(transferUser.Enabled);

            var transferAdapter = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferAdapter);
            Assert.IsFalse(transferAdapter.Enabled);

            var transferTransducer = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferTransducer);
            Assert.IsFalse(transferTransducer.Enabled);

            var transferAttributes = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferAttributes);
            Assert.IsFalse(transferAttributes.Enabled);

            var transferPictures = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferPictures);
            Assert.IsFalse(transferPictures.Enabled);

            var transferNewLimits = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferNewLimits);
            Assert.IsFalse(transferNewLimits.Enabled);

            var transferCurves = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TransferCurves);
            Assert.IsFalse(transferCurves.Enabled);

            var askForIdent = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.AskForIdent);
            Assert.IsFalse(askForIdent.Enabled);

            var askForSign = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.AskForSign);
            Assert.IsFalse(askForSign.Enabled);

            var useErrorCodes = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.UseErrorCodes);
            Assert.IsFalse(useErrorCodes.Enabled);

            var performLooseCheck = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.PerformLooseCheck);
            Assert.IsFalse(performLooseCheck.Enabled);

            var mpCanBeDeleted = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.MpCanBeDeleted);
            Assert.IsFalse(mpCanBeDeleted.Enabled);

            var confirmMp = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.ConfirmMp);
            Assert.IsFalse(confirmMp.Enabled);

            var standardMethodsCanBeUsed = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.StandardMethodsCanBeUsed);
            Assert.IsFalse(standardMethodsCanBeUsed.Enabled);


            var testequipmentDriver = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TeDriver);
            Assert.IsFalse(testequipmentDriver.Enabled);

            var statusFile = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.StatusFile);
            Assert.IsFalse(statusFile.Enabled);

            var qstToTestequipment = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.QstToTeFile);
            Assert.IsFalse(qstToTestequipment.Enabled);

            var testequipmentToQST = modelTabScrollviewer.FindElementByAccessibilityId(AiStringHelper.TestEquipment.TestEquipmentModelTabElements.TeToQstFile);
            Assert.IsFalse(testequipmentToQST.Enabled);
        }
    }
}