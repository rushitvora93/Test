using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using UI_TestProjekt.Helper;
using UI_TestProjekt.TestModel;
using static UI_TestProjekt.ToolModelTests;
using static UI_TestProjekt.AuxiliaryMasterDataTests;

namespace UI_TestProjekt
{
    [TestClass]
    public class ToolTests : TestBase
    {
        [TestMethod]        
        [TestCategory("MasterData")]
        public void TestTool()
        {
            LoginAsCSP();
            AppiumWebElement globalTree;
            AppiumWebElement toolMenu;
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            ToolModel cspToolModel = Testdata.GetToolToolModel1();
            ToolModel scsToolModel = Testdata.GetToolToolModel2();
            ToolModel cspToolModelChanged = Testdata.GetToolToolModelChanged1();

            Tool cspTool = Testdata.GetTool1();
            Tool scsTool = Testdata.GetTool2();
            Tool cspToolChanged = Testdata.GetToolChanged1();
            Tool scsToolChanged = Testdata.GetToolChanged2();

            //Auf Werkzeugseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            //evtl. übriggebliebene Tools löschen falls vorhanden
            var toolView = QstSession.FindElementByAccessibilityId(AiStringHelper.Tool.View);
            var btnDeleteTool = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.DeleteTool);

            DeleteTool(QstSession, cspTool, btnDeleteTool);
            DeleteTool(QstSession, scsTool, btnDeleteTool);
            DeleteTool(QstSession, cspToolChanged, btnDeleteTool);
            DeleteTool(QstSession, scsToolChanged, btnDeleteTool);

            //CreateHelperEntrys for Tools
            //for Tool1
            AddHelper(cspTool.ConfigurableField, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ConfigurableFieldTool);
            AddHelper(cspTool.CostCenter, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.CostCenter);

            //for Tool2
            AddHelper(scsTool.ConfigurableField, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ConfigurableFieldTool);
            AddHelper(scsTool.CostCenter, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.CostCenter);

            //for Tool3
            AddHelper(cspToolChanged.ConfigurableField, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ConfigurableFieldTool);
            AddHelper(cspToolChanged.CostCenter, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.CostCenter);

            //for Tool4
            AddHelper(scsToolChanged.ConfigurableField, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ConfigurableFieldTool);
            AddHelper(scsToolChanged.CostCenter, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.CostCenter);

            //Auf Werkzeugmodellseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //Alte Modelle entfernen falls vorhanden
            var toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, cspToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, scsToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, cspToolModelChanged, btnDeleteModel);

            //Create Toolmodel
            CreateToolModel(QstSession, cspToolModel);
            CreateToolModel(QstSession, scsToolModel);
            CreateToolModel(QstSession, cspToolModelChanged);

            //Auf Werkzeugseite wechseln
            globalTree = QstSession.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);
            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);
            toolMenu = globalTree.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.Tool);
            toolMenu.Click();

            // Create Tools
            CreateTool(QstSession, cspTool);
            CreateTool(QstSession, scsTool);

            //TODO evtl entfernen
            globalTree = QstSession.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);
            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);
            toolMenu = globalTree.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.Tool);
            toolMenu.Click();
            toolView = QstSession.FindElementByAccessibilityId(AiStringHelper.Tool.View);

            //Check Tool
            AssertTool(QstSession, cspTool);
            AssertTool(QstSession, scsTool);

            //Change Tools
            //Aufpassen wegen https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-105 beim Ändern von WKZModell in WKZ
            var cspToolNode = GetToolNode(QstSession, cspTool.ToolModel.ToolModelType, cspTool.ToolModel.Manufacturer, cspTool.ToolModel.Description, cspTool.GetTreeString());
            cspToolNode.Click();
            UpdateTool(QstSession, cspToolChanged);
            VerifyAndApplyToolChanges(cspTool, cspToolChanged, 11, "changecomment cspTool!");

            var scsToolModelNode = GetToolNode(QstSession, scsTool.ToolModel.ToolModelType, scsTool.ToolModel.Manufacturer, scsTool.ToolModel.Description, scsTool.GetTreeString());
            scsToolModelNode.Click();
            UpdateTool(QstSession, scsToolChanged);
            VerifyAndApplyToolChanges(scsTool, scsToolChanged, 10, "changecomment scsToolmodel!");

            //Check toolmodel Changes
            AssertTool(QstSession, cspToolChanged);
            AssertTool(QstSession, scsToolChanged);

            //Delete toolmodel
            btnDeleteTool = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.DeleteTool);
            DeleteTool(QstSession, cspToolChanged, btnDeleteTool);

            DeleteTool(QstSession, scsToolChanged, btnDeleteTool);

            //Check deletion
            globalTree = QstSession.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);
            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);
            toolMenu = globalTree.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.Tool);
            toolMenu.Click();

            var cspChangedNode = GetToolNode(QstSession, cspToolChanged.ToolModel.ToolModelType, cspToolChanged.ToolModel.Manufacturer, cspToolChanged.ToolModel.Description, cspToolChanged.GetTreeString());
            Assert.IsNull(cspChangedNode);

            var scsChangedNode = GetToolNode(QstSession, scsToolChanged.ToolModel.ToolModelType, scsToolChanged.ToolModel.Manufacturer, scsToolChanged.ToolModel.Description, cspToolChanged.GetTreeString());
            Assert.IsNull(scsChangedNode);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestToolLongData()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            Tool invalidTool = Testdata.GetToolLongInvalidData();
            Tool validTool = Testdata.GetToolLongInvalidDataValid();
            Tool invalidToolForChange = Testdata.GetToolLongInvalidDataForChange();
            Tool validToolForChange = Testdata.GetToolLongInvalidDataForChangeValid();

            //Auf Werkzeugseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            //evtl. übriggebliebene Tools löschen falls vorhanden
            var toolView = QstSession.FindElementByAccessibilityId(AiStringHelper.Tool.View);
            var btnDeleteTool = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.DeleteTool);

            DeleteTool(QstSession, invalidTool, btnDeleteTool);
            DeleteTool(QstSession, validTool, btnDeleteTool);
            DeleteTool(QstSession, invalidToolForChange, btnDeleteTool);
            DeleteTool(QstSession, validToolForChange, btnDeleteTool);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            var toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);
            //DeleteToolModel(QstSession, validTool.ToolModel, btnDeleteModel);

            //Auf Werkzeugmodellseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            CreateToolModel(QstSession, validTool.ToolModel);

            //Auf Werkzeugseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            // Create Tools
            CreateTool(QstSession, validTool, false, null, false, true, invalidTool);

            //Check Tool
            AssertTool(QstSession, validTool);
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);
            AssertTool(QstSession, validTool);

            //Change Tools
            //Aufpassen wegen https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-105 beim Ändern von WKZModell in WKZ
            toolView = QstSession.FindElementByAccessibilityId(AiStringHelper.Tool.View);
            var treeViewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolView, AiStringHelper.Tool.ToolTreeViewRoot, QstSession);
            var validToolNode = TestHelper.GetNode(QstSession, treeViewRootNode, validTool.GetParentListWithTool());
            validToolNode.Click();
            UpdateTool(QstSession, invalidToolForChange);
            VerifyAndApplyToolChanges(validTool, validToolForChange, 6, "Update Long");

            //Check toolmodel Changes
            AssertTool(QstSession, validToolForChange);
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);
            AssertTool(QstSession, validToolForChange);

            //Delete toolmodel
            toolView = QstSession.FindElementByAccessibilityId(AiStringHelper.Tool.View);
            btnDeleteTool = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.DeleteTool);
            DeleteTool(QstSession, validToolForChange, btnDeleteTool);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);
            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);
            DeleteToolModel(QstSession, validTool.ToolModel, btnDeleteModel);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestToolWithTemplate()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            Tool toolTemplate = Testdata.GetToolTemplateForTemplateTest();
            Tool tool = Testdata.GetToolForTemplateTest();

            //evtl. übriggebliebene Tools löschen falls vorhanden
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);
            var toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            var btnDeleteTool = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.DeleteTool);

            DeleteTool(QstSession, toolTemplate, btnDeleteTool);
            DeleteTool(QstSession, tool, btnDeleteTool);

            //evtl.übriggebliebene Toolmodel löschen falls vorhanden
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);
            var toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);
            DeleteToolModel(QstSession, tool.ToolModel, btnDeleteModel);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);
            CreateToolModel(QstSession, tool.ToolModel);

            //Create Tool
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);
            CreateTool(QstSession, toolTemplate);
            AssertTool(QstSession, toolTemplate);

            CreateTool(QstSession, tool, true, toolTemplate);
            AssertTool(QstSession, tool);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);
            AssertTool(QstSession, tool);
            AssertTool(QstSession, toolTemplate);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            btnDeleteTool = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.DeleteTool);
            DeleteTool(QstSession, tool, btnDeleteTool);
            DeleteTool(QstSession, toolTemplate, btnDeleteTool);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);
            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);
            DeleteToolModel(QstSession, tool.ToolModel, btnDeleteModel);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestToolDuplicateIds()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            Tool toolDuplicateId = Testdata.GetToolForDuplicateIdsTest();

            //Auf Werkzeugseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            //evtl. übriggebliebenes Tools löschen falls vorhanden
            var toolView = QstSession.FindElementByAccessibilityId(AiStringHelper.Tool.View);
            var btnDeleteTool = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.DeleteTool);

            DeleteTool(QstSession, toolDuplicateId, btnDeleteTool);

            //Altes Modell entfernen falls vorhanden
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            var toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, toolDuplicateId.ToolModel, btnDeleteModel);

            //Create Toolmodel
            CreateToolModel(QstSession, toolDuplicateId.ToolModel);

            //Auf Toolseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            //evtl. übriggebliebene Tools löschen falls vorhanden
            toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            btnDeleteTool = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.DeleteTool);

            DeleteTool(QstSession, toolDuplicateId, btnDeleteTool);

            //Auf Toolseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            //Create Tool
            CreateTool(QstSession, toolDuplicateId);

            toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            var addToolBtn = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.AddTool);

            addToolBtn.Click();
            Thread.Sleep(500);
            var assistantSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.Assistant.View, TestConfiguration.GetWindowsApplicationDriverUrl());
            var assistantNextBtn = TestHelper.FindElementWithWait(AiStringHelper.Assistant.Next, assistantSession);
            var textInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputText, assistantSession);

            textInput.Clear();
            TestHelper.SendKeysWithBackslash(assistantSession, textInput, toolDuplicateId.SerialNumber);
            assistantNextBtn.Click();
            CheckAndCloseValidationWindow(assistantSession, ValidationStringHelper.ToolValidationStrings.SerialNumberUnique);
            TestHelper.SendKeysWithBackslash(assistantSession, textInput, "2");
            assistantNextBtn.Click();

            textInput.Clear();
            TestHelper.SendKeysWithBackslash(assistantSession, textInput, toolDuplicateId.InventoryNumber);
            assistantNextBtn.Click();
            CheckAndCloseValidationWindow(assistantSession, ValidationStringHelper.ToolValidationStrings.InventoryNumberUnique);

            var cancel = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.Cancel);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(assistantSession, cancel);
            cancel.Click();

            //Delete Tool
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            btnDeleteTool = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.DeleteTool);
            DeleteTool(QstSession, toolDuplicateId, btnDeleteTool);

            //Delete Model
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, toolDuplicateId.ToolModel, btnDeleteModel);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestToolOnChangeSwitchTool()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            Tool toolChangeSite1 = Testdata.GetToolChangeSite1();
            Tool toolChangeSite1Changed = Testdata.GetToolChangeSite1Changed();
            Tool toolChangeSite2 = Testdata.GetToolChangeSite2();

            AddHelper(toolChangeSite1Changed.CostCenter, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.CostCenter);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            //evtl. übriggebliebene Tools löschen falls vorhanden
            var toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            var btnDelete = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.DeleteTool);

            DeleteTool(QstSession, toolChangeSite1, btnDelete);
            DeleteTool(QstSession, toolChangeSite1Changed, btnDelete);
            DeleteTool(QstSession, toolChangeSite2, btnDelete);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //evtl. übriggebliebene Toolmodel löschen falls vorhanden
            var toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, toolChangeSite1.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, toolChangeSite1Changed.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, toolChangeSite2.ToolModel, btnDeleteModel);

            //Create ToolModel
            CreateToolModel(QstSession, toolChangeSite1.ToolModel);
            CreateToolModel(QstSession, toolChangeSite1Changed.ToolModel);
            CreateToolModel(QstSession, toolChangeSite2.ToolModel);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            //Create ToolModel
            CreateTool(QstSession, toolChangeSite1);
            CreateTool(QstSession, toolChangeSite2);

            toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            var toolTreeView = TestHelper.FindElementByAiWithWaitFromParent(toolView, AiStringHelper.Tool.ToolTreeView, QstSession);
            var toolTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolTreeView, AiStringHelper.Tool.ToolTreeViewRoot, QstSession);

            var toolChangeSite1Node = TestHelper.GetNode(QstSession, toolTreeviewRootNode, toolChangeSite1.GetParentListWithTool());
            toolChangeSite1Node.Click();

            //Cancel
            UpdateTool(QstSession, toolChangeSite1Changed, false);

            var toolChangeSite2Node = TestHelper.GetNode(QstSession, toolTreeviewRootNode, toolChangeSite2.GetParentListWithTool());
            toolChangeSite2Node.Click();

            ViewVerifyChangesAndClickButton(toolChangeSite1, toolChangeSite1Changed, 5, HistoButton.Cancel);
            AssertTool(QstSession, toolChangeSite1Changed);

            //Reset
            UpdateTool(QstSession, toolChangeSite1Changed, false);

            toolChangeSite2Node = TestHelper.GetNode(QstSession, toolTreeviewRootNode, toolChangeSite2.GetParentListWithTool());
            toolChangeSite2Node.Click();

            ViewVerifyChangesAndClickButton(toolChangeSite1, toolChangeSite1Changed, 5, HistoButton.Reset);

            //TODO Evtl. Seitenwechsel löschen wenn Bug https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-138 gefixed ist
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            toolTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolView, AiStringHelper.Tool.ToolTreeViewRoot, QstSession);

            toolChangeSite1Node = TestHelper.GetNode(QstSession, toolTreeviewRootNode, toolChangeSite1.GetParentListWithTool());
            toolChangeSite1Node.Click();

            AssertTool(QstSession, toolChangeSite1);

            //Apply
            UpdateTool(QstSession, toolChangeSite1Changed, false);

            toolChangeSite2Node = TestHelper.GetNode(QstSession, toolTreeviewRootNode, toolChangeSite2.GetParentListWithTool());
            toolChangeSite2Node.Click();

            ViewVerifyChangesAndClickButton(toolChangeSite1, toolChangeSite1Changed, 5, HistoButton.Apply);

            //TODO Evtl. Seitenwechsel löschen wenn Bug https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-158 gefixed ist
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);
            toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            toolTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolView, AiStringHelper.Tool.ToolTreeViewRoot, QstSession);

            var toolChangeSite1ChangedNode = TestHelper.GetNode(QstSession, toolTreeviewRootNode, toolChangeSite1Changed.GetParentListWithTool());
            toolChangeSite1ChangedNode.Click();

            AssertTool(QstSession, toolChangeSite1Changed);

            //Delete 
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            btnDelete = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.DeleteTool);

            DeleteTool(QstSession, toolChangeSite2, btnDelete);
            DeleteTool(QstSession, toolChangeSite1Changed, btnDelete);
            DeleteTool(QstSession, toolChangeSite1, btnDelete);


            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);
            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, toolChangeSite2.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, toolChangeSite1Changed.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, toolChangeSite1.ToolModel, btnDeleteModel);
        }

        private static void ViewVerifyChangesAndClickButton(Tool toolChangeSite1, Tool toolChangeSite1Changed, int numberOfExpectedChanges, HistoButton button)
        {
            var verifySession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.VerifyChanges.View, TestConfiguration.GetWindowsApplicationDriverUrl());

            //Verify-Fenster in den Vordergrund bringen
            verifySession.SwitchTo().Window(QstSession.WindowHandles.First());

            var viewVerifyChanges = TestHelper.FindElementWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");

            var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyToolChangesInVerifyView(toolChangeSite1, toolChangeSite1Changed, listViewChanges, numberOfExpectedChanges);

            switch (button)
            {
                case HistoButton.Apply:
                    var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
                    TestHelper.WaitForElementToBeEnabledAndDisplayed(DesktSession, btnApply);
                    btnApply.Click();
                    TestHelper.AssertAndCloseToastNotification(DesktSession,ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
                    break;
                case HistoButton.Reset:
                    var btnReset = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Reset);
                    TestHelper.WaitForElementToBeEnabledAndDisplayed(DesktSession, btnReset);
                    btnReset.Click();
                    break;
                case HistoButton.Cancel:
                    var btnCancel = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Cancel);
                    TestHelper.WaitForElementToBeEnabledAndDisplayed(DesktSession, btnCancel);
                    btnCancel.Click();
                    break;
                default:
                    Assert.IsTrue(false, "VerifyButton nicht gefunden");
                    break;
            }
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestToolOnChangeChangeSite()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            Tool toolChangeSite1 = Testdata.GetToolChangeSite1();
            Tool toolChangeSite1Changed = Testdata.GetToolChangeSite1Changed();
            Tool toolChangeSite2 = Testdata.GetToolChangeSite2();

            AddHelper(toolChangeSite1Changed.CostCenter, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.CostCenter);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            //evtl. übriggebliebene Tools löschen falls vorhanden
            var toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            var btnDelete = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.DeleteTool);

            DeleteTool(QstSession, toolChangeSite1, btnDelete);
            DeleteTool(QstSession, toolChangeSite1Changed, btnDelete);
            DeleteTool(QstSession, toolChangeSite2, btnDelete);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //evtl. übriggebliebene Toolmodel löschen falls vorhanden
            var toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, toolChangeSite1.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, toolChangeSite1Changed.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, toolChangeSite2.ToolModel, btnDeleteModel);

            //Create ToolModel
            CreateToolModel(QstSession, toolChangeSite1.ToolModel);
            CreateToolModel(QstSession, toolChangeSite1Changed.ToolModel);
            CreateToolModel(QstSession, toolChangeSite2.ToolModel);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            //Create ToolModel
            CreateTool(QstSession, toolChangeSite1);
            CreateTool(QstSession, toolChangeSite2);

            toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            var toolTreeView = TestHelper.FindElementByAiWithWaitFromParent(toolView, AiStringHelper.Tool.ToolTreeView, QstSession);
            var toolTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolTreeView, AiStringHelper.Tool.ToolTreeViewRoot, QstSession);

            var toolChangeSite1Node = TestHelper.GetNode(QstSession, toolTreeviewRootNode, toolChangeSite1.GetParentListWithTool());
            toolChangeSite1Node.Click();

            //Cancel
            UpdateTool(QstSession, toolChangeSite1Changed, false);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            ViewVerifyChangesAndClickButton(toolChangeSite1, toolChangeSite1Changed, 5, HistoButton.Cancel);

            AssertTool(QstSession, toolChangeSite1Changed);

            //Reset
            UpdateTool(QstSession, toolChangeSite1Changed, false);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            ViewVerifyChangesAndClickButton(toolChangeSite1, toolChangeSite1Changed, 5, HistoButton.Reset);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);
            toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            toolTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolView, AiStringHelper.Tool.ToolTreeViewRoot, QstSession);

            toolChangeSite1Node = TestHelper.GetNode(QstSession, toolTreeviewRootNode, toolChangeSite1.GetParentListWithTool());
            toolChangeSite1Node.Click();

            AssertTool(QstSession, toolChangeSite1);

            //Apply
            UpdateTool(QstSession, toolChangeSite1Changed, false);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            ViewVerifyChangesAndClickButton(toolChangeSite1, toolChangeSite1Changed, 5, HistoButton.Apply);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);
            toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            toolTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolView, AiStringHelper.Tool.ToolTreeViewRoot, QstSession);

            var toolChangeSite1ChangedNode = TestHelper.GetNode(QstSession, toolTreeviewRootNode, toolChangeSite1Changed.GetParentListWithTool());
            toolChangeSite1ChangedNode.Click();

            AssertTool(QstSession, toolChangeSite1Changed);

            //Delete 
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            btnDelete = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.DeleteTool);

            DeleteTool(QstSession, toolChangeSite2, btnDelete);
            DeleteTool(QstSession, toolChangeSite1Changed, btnDelete);
            DeleteTool(QstSession, toolChangeSite1, btnDelete);


            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);
            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, toolChangeSite2.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, toolChangeSite1Changed.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, toolChangeSite1.ToolModel, btnDeleteModel);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestToolOnChangeLogout()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            Tool toolChangeSite1 = Testdata.GetToolChangeSite1();
            Tool toolChangeSite1Changed = Testdata.GetToolChangeSite1Changed();
            Tool toolChangeSite2 = Testdata.GetToolChangeSite2();

            AddHelper(toolChangeSite1Changed.CostCenter, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.CostCenter);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            //evtl. übriggebliebene Tools löschen falls vorhanden
            var toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            var btnDelete = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.DeleteTool);

            DeleteTool(QstSession, toolChangeSite1, btnDelete);
            DeleteTool(QstSession, toolChangeSite1Changed, btnDelete);
            DeleteTool(QstSession, toolChangeSite2, btnDelete);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //evtl. übriggebliebene Toolmodel löschen falls vorhanden
            var toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, toolChangeSite1.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, toolChangeSite1Changed.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, toolChangeSite2.ToolModel, btnDeleteModel);

            //Create ToolModel
            CreateToolModel(QstSession, toolChangeSite1.ToolModel);
            CreateToolModel(QstSession, toolChangeSite1Changed.ToolModel);
            CreateToolModel(QstSession, toolChangeSite2.ToolModel);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            //Create ToolModel
            CreateTool(QstSession, toolChangeSite1);
            CreateTool(QstSession, toolChangeSite2);

            toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            var toolTreeView = TestHelper.FindElementByAiWithWaitFromParent(toolView, AiStringHelper.Tool.ToolTreeView, QstSession);
            var toolTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolTreeView, AiStringHelper.Tool.ToolTreeViewRoot, QstSession);

            var toolChangeSite1Node = TestHelper.GetNode(QstSession, toolTreeviewRootNode, toolChangeSite1.GetParentListWithTool());
            toolChangeSite1Node.Click();

            //Cancel
            UpdateTool(QstSession, toolChangeSite1Changed, false);

            var logout = QstSession.FindElementByAccessibilityId(AiStringHelper.GlobalToolbar.LogOut);
            logout.Click();

            ViewVerifyChangesAndClickButton(toolChangeSite1, toolChangeSite1Changed, 5, HistoButton.Cancel);
            AssertTool(QstSession, toolChangeSite1Changed);

            //Reset
            UpdateTool(QstSession, toolChangeSite1Changed, false);

            logout = QstSession.FindElementByAccessibilityId(AiStringHelper.GlobalToolbar.LogOut);
            logout.Click();

            ViewVerifyChangesAndClickButton(toolChangeSite1, toolChangeSite1Changed, 5, HistoButton.Reset);

            LoginAsCSP(true);
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);
            toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            toolTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolView, AiStringHelper.Tool.ToolTreeViewRoot, QstSession);

            toolChangeSite1Node = TestHelper.GetNode(QstSession, toolTreeviewRootNode, toolChangeSite1.GetParentListWithTool());
            toolChangeSite1Node.Click();

            AssertTool(QstSession, toolChangeSite1);

            //Apply
            UpdateTool(QstSession, toolChangeSite1Changed, false);

            logout = QstSession.FindElementByAccessibilityId(AiStringHelper.GlobalToolbar.LogOut);
            logout.Click();

            ViewVerifyChangesAndClickButton(toolChangeSite1, toolChangeSite1Changed, 5, HistoButton.Apply);

            LoginAsCSP(true);
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);
            toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            toolTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolView, AiStringHelper.Tool.ToolTreeViewRoot, QstSession);

            var toolChangeSite1ChangedNode = TestHelper.GetNode(QstSession, toolTreeviewRootNode, toolChangeSite1Changed.GetParentListWithTool());
            toolChangeSite1ChangedNode.Click();

            AssertTool(QstSession, toolChangeSite1Changed);

            //Delete 
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            btnDelete = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.DeleteTool);

            DeleteTool(QstSession, toolChangeSite2, btnDelete);
            DeleteTool(QstSession, toolChangeSite1Changed, btnDelete);
            DeleteTool(QstSession, toolChangeSite1, btnDelete);


            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);
            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, toolChangeSite2.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, toolChangeSite1Changed.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, toolChangeSite1.ToolModel, btnDeleteModel);
        }


        //evtl durch "TestHelper.GetNode" austauschen
        private static AppiumWebElement GetToolNode(WindowsDriver<WindowsElement> QstSession, string modelType, string manufacturer, string model, string tool)
        {
            var toolTreeviewRootNode = QstSession.FindElementByAccessibilityId(AiStringHelper.Tool.ToolTreeViewRoot);
            var toolTreeviewRootNodeExpander = TestHelper.TryFindElementBy("PART_Expander", toolTreeviewRootNode);
            if (toolTreeviewRootNodeExpander == null)
            {
                return null;
            }

            var toolTreeviewRootNodeSubExpander = toolTreeviewRootNodeExpander.FindElementByAccessibilityId("Expander");
            if (toolTreeviewRootNodeSubExpander.GetAttribute("Toggle.ToggleState") == "0")
            {
                toolTreeviewRootNodeSubExpander.Click();
            }
            IReadOnlyCollection<AppiumWebElement> treeViewItemAdvs1Lvl = toolTreeviewRootNode.FindElementsByClassName("TreeViewItemAdv");

            AppiumWebElement modelTypeNode = null;
            foreach (var item in treeViewItemAdvs1Lvl)
            {
                modelTypeNode = TestHelper.TryFindElementBy(modelType, item, AiStringHelper.By.Name);
                if (modelTypeNode != null)
                {
                    break;
                }
            }

            if (modelTypeNode == null)
            {
                return null;
            }

            var modelTypeNodeExpander = modelTypeNode.FindElementByAccessibilityId("PART_Expander");
            var modelTypeNodeSubExpander = modelTypeNodeExpander.FindElementByAccessibilityId("Expander");
            if (modelTypeNodeSubExpander.GetAttribute("Toggle.ToggleState") == "0")
            {
                modelTypeNodeSubExpander.Click();
            }

            IReadOnlyCollection<AppiumWebElement> treeViewItemAdvs2Lvl = modelTypeNode.FindElementsByClassName("TreeViewItemAdv");

            AppiumWebElement manufacturerNode = null;
            foreach (var item in treeViewItemAdvs2Lvl)
            {
                manufacturerNode = TestHelper.TryFindElementBy(manufacturer, item, AiStringHelper.By.Name);
                //item.FindElementByName(manufacturer);
                if (manufacturerNode != null)
                {
                    break;
                }
            }

            if (manufacturerNode == null)
            {
                return null;
            }

            var manuNodeExpander = manufacturerNode.FindElementByAccessibilityId("PART_Expander");
            var manuNodeSubExpander = manuNodeExpander.FindElementByAccessibilityId("Expander");
            if (manuNodeSubExpander.GetAttribute("Toggle.ToggleState") == "0")
            {
                manuNodeSubExpander.Click();
            }

            IReadOnlyCollection<AppiumWebElement> treeViewItemAdvs3Lvl = manufacturerNode.FindElementsByClassName("TreeViewItemAdv");

            AppiumWebElement wkzModelNode = null;
            foreach (var item in treeViewItemAdvs3Lvl)
            {
                wkzModelNode = TestHelper.TryFindElementBy(model, item, AiStringHelper.By.Name);
                if (wkzModelNode != null)
                {
                    break;
                }
            }

            if (wkzModelNode == null)
            {
                return null;
            }

            var wkzModelNodeExpander = wkzModelNode.FindElementByAccessibilityId("PART_Expander");
            var wkzModelNodeSubExpander = wkzModelNodeExpander.FindElementByAccessibilityId("Expander");
            if (wkzModelNodeSubExpander.GetAttribute("Toggle.ToggleState") == "0")
            {
                wkzModelNodeSubExpander.Click();
            }

            IReadOnlyCollection<AppiumWebElement> treeViewItemAdvs4Lvl = wkzModelNode.FindElementsByClassName("TreeViewItemAdv");

            AppiumWebElement wkzNode = null;
            foreach (var item in treeViewItemAdvs4Lvl)
            {
                wkzNode = TestHelper.TryFindElementBy(tool, item, AiStringHelper.By.Name);
                if (wkzNode != null)
                {
                    break;
                }
            }

            if (wkzNode == null)
            {
                return null;
            }

            return wkzNode;
        }
        public static void DeleteTool(WindowsDriver<WindowsElement> QstSession, Tool tool, AppiumWebElement btnDeleteTool)
        {
            var node = GetToolNode(QstSession, tool.ToolModel.ToolModelType, tool.ToolModel.Manufacturer, tool.ToolModel.Description, tool.GetTreeString());
            if (node != null)
            {
                node.Click();
                Thread.Sleep(200);
                btnDeleteTool.Click();
                Thread.Sleep(200);
                var confirmDeleteBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
                confirmDeleteBtn.Click();
                TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            }
        }
        public static void CreateTool(
            WindowsDriver<WindowsElement> QstSession,
            Tool tool,
            bool withTemplateCheck = false,
            Tool template = null,
            bool withCheckValidationErrors = false,
            bool withLongValues = false,
            Tool invalidTool = null)
        {
            Assert.IsNotNull(QstSession, "QstSession in CreateTool ist null");
            var toolView = TestHelper.FindElementWithWait(AiStringHelper.Tool.View, QstSession);
            var addToolBtn = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.AddTool);
            var toolTreeViewRoot = TestHelper.FindElementByAiWithWaitFromParent(toolView, AiStringHelper.Tool.ToolTreeViewRoot, QstSession);
            var toolNode = TestHelper.GetNode(QstSession, toolTreeViewRoot, tool.GetParentListWithTool());
            if (toolNode != null)
            {
                return;
            }

            addToolBtn.Click();
            Thread.Sleep(500);
            var assistantSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.Assistant.View, TestConfiguration.GetWindowsApplicationDriverUrl());
            var textInput = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.InputText);
            if (withTemplateCheck)
            {
                Assert.AreEqual(template.SerialNumber, textInput.Text);
            }

            textInput.Clear();
            if (withLongValues)
            {
                TestHelper.SendKeysConverted(textInput, invalidTool.SerialNumber);
            }
            else
            {
                TestHelper.SendKeysConverted(textInput, tool.SerialNumber);
            }
            AssertAssistantListEntry(assistantSession, tool.SerialNumber, AssistantStringHelper.ToolStrings.SerialNumber);
            var assistantNextBtn = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.Next);
            assistantNextBtn.Click();

            textInput = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.InputText);
            if (withTemplateCheck)
            {
                Assert.AreEqual(template.InventoryNumber, textInput.Text);
            }

            textInput.Clear();
            if (withLongValues)
            {
                TestHelper.SendKeysConverted(textInput, invalidTool.InventoryNumber);
            }
            else
            {
                TestHelper.SendKeysConverted(textInput, tool.InventoryNumber);
            }
            AssertAssistantListEntry(assistantSession, tool.InventoryNumber, AssistantStringHelper.ToolStrings.InventoryNumber);
            assistantNextBtn = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.Next);
            assistantNextBtn.Click();

            var listInput = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.InputList);
            if (withTemplateCheck)
            {
                var templateToolModelEntry = TestHelper.FindElementInListbox(template.ToolModel.Description, listInput);
                Assert.IsTrue(templateToolModelEntry.Selected);
            }
            var modelEntry = listInput.FindElementByAccessibilityId(tool.ToolModel.Description);
            modelEntry.Click();
            AssertAssistantListEntry(assistantSession, tool.ToolModel.Description, AssistantStringHelper.ToolStrings.ToolModel);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                var templateStatusEntry = TestHelper.FindElementInListbox(template.Status, listInput);
                Assert.IsTrue(templateStatusEntry.Selected);
            }
            var toolStatusEntry = FindOrCreateHelperListEntryInAssistWindow(tool.Status, assistantSession, listInput);
            toolStatusEntry.Click();
            AssertAssistantListEntry(assistantSession, tool.Status, AssistantStringHelper.ToolStrings.Status);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                Assert.AreEqual(template.Accessory, textInput.Text);
            }

            textInput.Clear();
            TestHelper.SendKeysConverted(textInput, tool.Accessory);
            AssertAssistantListEntry(assistantSession, tool.Accessory, AssistantStringHelper.ToolStrings.Accessory);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                var templateConfigFieldEntry = TestHelper.FindElementInListbox(template.ConfigurableField, listInput);
                Assert.IsTrue(templateConfigFieldEntry.Selected);
            }
            var configFieldEntry = FindOrCreateHelperListEntryInAssistWindow(tool.ConfigurableField, assistantSession, listInput);
            configFieldEntry.Click();
            AssertAssistantListEntry(assistantSession, tool.ConfigurableField, AssistantStringHelper.ToolStrings.ConfigurableField);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                var templateCostCenterEntry = TestHelper.FindElementInListbox(template.CostCenter, listInput);
                Assert.IsTrue(templateCostCenterEntry.Selected);
            }
            var costCenterEntry = FindOrCreateHelperListEntryInAssistWindow(tool.CostCenter, assistantSession, listInput);
            costCenterEntry.Click();
            AssertAssistantListEntry(assistantSession, tool.CostCenter, AssistantStringHelper.ToolStrings.CostCenter);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                Assert.AreEqual(template.ConfigurableField1, textInput.Text);
            }

            textInput.Clear();
            if (withLongValues)
            {
                TestHelper.SendKeysConverted(textInput, invalidTool.ConfigurableField1);
            }
            else
            {
                TestHelper.SendKeysConverted(textInput, tool.ConfigurableField1);
            }
            AssertAssistantListEntry(assistantSession, tool.ConfigurableField1, AssistantStringHelper.ToolStrings.ConfigurableField1);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                Assert.AreEqual(template.ConfigurableField2, textInput.Text);
            }

            textInput.Clear();
            if (withLongValues)
            {
                TestHelper.SendKeysConverted(textInput, invalidTool.ConfigurableField2);
            }
            else
            {
                TestHelper.SendKeysConverted(textInput, tool.ConfigurableField2);
            }
            AssertAssistantListEntry(assistantSession, tool.ConfigurableField2, AssistantStringHelper.ToolStrings.ConfigurableField2);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                Assert.AreEqual(template.ConfigurableField3, textInput.Text);
            }

            textInput.Clear();
            if (withLongValues)
            {
                TestHelper.SendKeysConverted(textInput, invalidTool.ConfigurableField3);
            }
            else
            {
                TestHelper.SendKeysConverted(textInput, tool.ConfigurableField3);
            }
            AssertAssistantListEntry(assistantSession, tool.ConfigurableField3, AssistantStringHelper.ToolStrings.ConfigurableField3);
            assistantNextBtn.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
        }
        private static void AssertTool(WindowsDriver<WindowsElement> QstSession, Tool tool)
        {
            /*var globalTree = QstSession.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);
            ExpandMainMenuWhenNotOpened("MasterData", globalTree);
            var toolModelMenu = globalTree.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.ToolModel);
            toolModelMenu.Click();*/

            AppiumWebElement toolNode = GetToolNode(QstSession, tool.ToolModel.ToolModelType, tool.ToolModel.Manufacturer, tool.ToolModel.Description, tool.GetTreeString());
            toolNode.Click();

            var toolView = QstSession.FindElementByAccessibilityId(AiStringHelper.Tool.View);
            var toolCmCmkTab = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.TabCmCmk);
            var toolParamTab = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ParamTab);
            var toolAdditionalInformationTab = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.TabAdditionalInformation);

            toolParamTab.Click();
            var scrollViewer = toolParamTab.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ParamTabScrollViewer);
            scrollViewer.SendKeys(Keys.PageUp);

            var inventoryNr = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.InventoryNumber);
            Assert.AreEqual(tool.InventoryNumber, inventoryNr.Text);

            var serialNr = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.SerialNumber);
            Assert.AreEqual(tool.SerialNumber, serialNr.Text);

            var toolModel = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ToolModel);
            string selectedToolModelText = GetToolComboboxString(toolView, toolModel, QstSession);
            Assert.AreEqual(tool.ToolModel.Description, selectedToolModelText);

            var status = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ToolStatus);
            string selectedStatus = GetToolComboboxString(toolView, status, QstSession);
            Assert.AreEqual(tool.Status, selectedStatus);

            var accessory = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.Accessory);
            Assert.AreEqual(tool.Accessory, accessory.Text);

            var CostCenter = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ToolCostCenter);
            string selectedCostCenter = GetToolComboboxString(toolView, CostCenter, QstSession);
            Assert.AreEqual(tool.CostCenter, selectedCostCenter);

            var configurableField = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ConfigurableField);
            string selectedConfigurableField = GetToolComboboxString(toolView, configurableField, QstSession);
            Assert.AreEqual(tool.ConfigurableField, selectedConfigurableField);

            //ToolModelFelder
            var modelManufacturer = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ModelManufacturer);
            Assert.AreEqual(tool.ToolModel.Manufacturer, modelManufacturer.Text);

            var modelType = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ModelType);
            Assert.AreEqual(tool.ToolModel.ToolModelType, modelType.Text);

            //TODO einkommentieren wenn Ladebug https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-113 gefixed ist
            /*
            var modelConstructionType = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ModelConstructionType);
            Assert.AreEqual(tool.ToolModel.ConstructionType, modelConstructionType.Text);

            var modelSwitchOff = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ModelSwitchOff);
            Assert.AreEqual(tool.ToolModel.SwitchOff, modelSwitchOff.Text);

            var modelShutOff = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ModelShutOff);
            Assert.AreEqual(tool.ToolModel.ShutOff, modelShutOff.Text);

            var modelDriveSize = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ModelDriveSize);
            Assert.AreEqual(tool.ToolModel.DriveSize, modelDriveSize.Text);

            var modelDriveType = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ModelDriveType);
            Assert.AreEqual(tool.ToolModel.DriveType, modelDriveType.Text);
            */

            var modelLowerPowerLimit = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ModelLowerPowLimit);
            Assert.AreEqual(tool.ToolModel.MinPow.ToString(numberFormatThreeDecimals, currentCulture), modelLowerPowerLimit.Text);

            var modelUpperPowerLimit = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ModelUpperPowLimit);
            Assert.AreEqual(tool.ToolModel.MaxPow.ToString(numberFormatThreeDecimals, currentCulture), modelUpperPowerLimit.Text);

            //Feld für Luftdruck ist nur bei "Pulse drivern", "Pulse driver Shut Off" und "General" vorhanden
            var modelAirPressure = TestHelper.TryFindElementBy(AiStringHelper.Tool.SingleTool.ModelAirPressure, toolView);
            if (tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriver ||
                tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriverShutOff ||
                tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General)
            {
                Assert.AreEqual(tool.ToolModel.AirPressure.ToString(numberFormatThreeDecimals, currentCulture), modelAirPressure.Text);
            }
            else
            {
                Assert.IsNull(modelAirPressure);
            }

            var modelWeight = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ModelWeight);
            Assert.AreEqual(tool.ToolModel.Weight.ToString(numberFormatThreeDecimals, currentCulture), modelWeight.Text);

            //Feld für maximale Umdrehungen ist nur bei "Pulse drivern", "Pulse driver Shut Off", "General" und EC Drivern vorhanden
            var modelMaxRotSpeed = TestHelper.TryFindElementBy(AiStringHelper.Tool.SingleTool.ModelMaxRotSpeed, toolView);
            if (tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriver ||
                tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriverShutOff ||
                tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General ||
                tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.EcDriver)
            {
                Assert.AreEqual(tool.ToolModel.MaxRotSpeed.ToString(numberFormatThreeDecimals, currentCulture), modelMaxRotSpeed.Text);
            }
            else
            {
                Assert.IsNull(modelMaxRotSpeed);
            }

            //Feld für Luftverbrauch ist nur bei "Pulse drivern", "Pulse driver Shut Off" und "General" vorhanden
            var modelAirConsumption = TestHelper.TryFindElementBy(AiStringHelper.Tool.SingleTool.ModelAirConsumption, toolView);
            if (tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriver ||
                tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriverShutOff ||
                tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General)
            {
                Assert.AreEqual(tool.ToolModel.AirConsumption.ToString(numberFormatThreeDecimals, currentCulture), modelAirConsumption.Text);
            }
            else
            {
                Assert.IsNull(modelAirConsumption);
            }

            //Feld für Batteriespannung ist nur bei "General" und "Ec Drivern" vorhanden
            var modelBattVolt = TestHelper.TryFindElementBy(AiStringHelper.Tool.SingleTool.ModelBatteryVoltage, toolView);
            if (tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General ||
                tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.EcDriver)
            {
                Assert.AreEqual(tool.ToolModel.BattVoltage.ToString(numberFormatThreeDecimals, currentCulture), modelBattVolt.Text);
            }
            else
            {
                Assert.IsNull(modelBattVolt);
            }


            toolCmCmkTab.Click();
            var cmCmkLimit = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.CmCmkLimit);
            string selectedCmCmkLimit = TestHelper.GetSelectedComboboxString(QstSession, cmCmkLimit);
            Assert.AreEqual(tool.CmCmkLimit, selectedCmCmkLimit);


            toolAdditionalInformationTab.Click();
            var configurableField1 = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ConfigurableField1);
            Assert.AreEqual(tool.ConfigurableField1, configurableField1.Text);

            var configurableField2 = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ConfigurableField2);
            Assert.AreEqual(tool.ConfigurableField2, configurableField2.Text);

            var configurableField3 = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ConfigurableField3);
            Assert.AreEqual(tool.ConfigurableField3, configurableField3.Text);
        }
        private static string GetToolComboboxString(WindowsElement toolView, AppiumWebElement comboBox, WindowsDriver<WindowsElement> driver)
        {
            var toolParamTab = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ParamTab);
            var scrollViewer = toolParamTab.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ParamTabScrollViewer);

            return TestHelper.GetSelectedComboboxStringWithScrolling(driver, comboBox, scrollViewer);
        }
        private static void ClickToolComboBoxEntryWithScrolling(WindowsDriver<WindowsElement> QstSession, string entryString, AppiumWebElement comboBox, WindowsElement toolView)
        {
            var toolParamTab = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ParamTab);
            var scrollViewer = toolParamTab.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ParamTabScrollViewer);
            TestHelper.ClickComboBoxEntryWithScrolling(QstSession, entryString, comboBox, scrollViewer);
        }
        private static void UpdateTool(
            WindowsDriver<WindowsElement> QstSession,
            Tool tool,
            bool save = true)
        {
            var toolView = QstSession.FindElementByAccessibilityId(AiStringHelper.Tool.View);
            var saveToolBtn = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SaveTool);
            var toolParamTab = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ParamTab);
            var toolCmCmkTab = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.TabCmCmk);
            var toolAdditionalInformationTab = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.TabAdditionalInformation);

            toolParamTab.Click();
            var scrollViewer = toolParamTab.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ParamTabScrollViewer);
            scrollViewer.SendKeys(Keys.PageUp);

            var fieldInventoryNr = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.InventoryNumber);
            fieldInventoryNr.Clear();
            TestHelper.SendKeysConverted(fieldInventoryNr, tool.InventoryNumber);

            var fieldSerialNr = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.SerialNumber);
            fieldSerialNr.Clear();
            TestHelper.SendKeysConverted(fieldSerialNr, tool.SerialNumber);

            var comboBoxModel = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ToolModel);
            ClickToolComboBoxEntryWithScrolling(QstSession, tool.ToolModel.Description, comboBoxModel, toolView);

            var comboBoxStatus = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ToolStatus);
            ClickToolComboBoxEntryWithScrolling(QstSession, tool.Status, comboBoxStatus, toolView);

            var fieldAccessory = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.Accessory);
            fieldAccessory.Clear();
            TestHelper.SendKeysConverted(fieldAccessory, tool.Accessory);

            var comboBoxCostCenter = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ToolCostCenter);
            ClickToolComboBoxEntryWithScrolling(QstSession, tool.CostCenter, comboBoxCostCenter, toolView);

            var comboBoxConfigurableField = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ConfigurableField);
            ClickToolComboBoxEntryWithScrolling(QstSession, tool.ConfigurableField, comboBoxConfigurableField, toolView);

            var fieldComment = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.Comment);
            fieldComment.Clear();
            TestHelper.SendKeysConverted(fieldComment, tool.Comment);

            //TODO CM/CMK ergänzen wenn Implementiert

            toolAdditionalInformationTab.Click();

            var fieldConfigurableField1 = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ConfigurableField1);
            fieldConfigurableField1.Clear();
            TestHelper.SendKeysConverted(fieldConfigurableField1, tool.ConfigurableField1);

            var fieldConfigurableField2 = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ConfigurableField2);
            fieldConfigurableField2.Clear();
            TestHelper.SendKeysConverted(fieldConfigurableField2, tool.ConfigurableField2);

            var fieldConfigurableField3 = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.SingleTool.ConfigurableField3);
            fieldConfigurableField3.Clear();
            TestHelper.SendKeysConverted(fieldConfigurableField3, tool.ConfigurableField3);

            if (save)
            {
                saveToolBtn.Click();
            }
        }
        private static void VerifyToolChangesInVerifyView(Tool tool, Tool toolChanged, AppiumWebElement listViewChanges, int numberOfExpectedChanges)
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
                    case "Inventory number":
                        Assert.AreEqual(tool.InventoryNumber, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolChanged.InventoryNumber, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Serial number":
                        Assert.AreEqual(tool.SerialNumber, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolChanged.SerialNumber, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Tool model":
                        Assert.AreEqual(tool.ToolModel.Description, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolChanged.ToolModel.Description, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Status":
                        Assert.AreEqual(tool.Status, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolChanged.Status, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Cost center":
                        Assert.AreEqual(tool.CostCenter, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolChanged.CostCenter, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Configurable field":
                        Assert.AreEqual(tool.ConfigurableField, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolChanged.ConfigurableField, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Accessory":
                        Assert.AreEqual(tool.Accessory, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolChanged.Accessory, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Comment":
                        //TODO Einkommentieren wenn Kommentar Implementiert ist
                        /*Assert.AreEqual(tool.Comment, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolChanged.Comment, item.FindElementsByClassName("TextBlock")[3].Text);*/
                        break;
                    case "Additional configurable Field1":
                        Assert.AreEqual(tool.ConfigurableField1, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolChanged.ConfigurableField1, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Additional configurable Field2":
                        Assert.AreEqual(tool.ConfigurableField2, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolChanged.ConfigurableField2, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Additional configurable Field3":
                        Assert.AreEqual(tool.ConfigurableField3, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolChanged.ConfigurableField3, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    default:
                        Assert.IsTrue(false, string.Format("Case '{0}' not implemented", changeTypeText));
                        break;
                }

                i++;
            }
        }
        private static void VerifyAndApplyToolChanges(Tool tool, Tool toolChanged, int numberOfChanges, string changeComment)
        {
            var viewVerifyChanges = DesktSession.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.View);

            var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyToolChangesInVerifyView(tool, toolChanged, listViewChanges, numberOfChanges);
            var textBoxComment = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Comment);
            TestHelper.SendKeysConverted(textBoxComment, changeComment);

            var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            btnApply.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
        }
    }
}
