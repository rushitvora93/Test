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
using static UI_TestProjekt.AuxiliaryMasterDataTests;

namespace UI_TestProjekt
{
    [TestClass]
    public class ToolModelTests : TestBase
    {
        [TestMethod]
        [TestCategory("MasterData")]
        public void TestToolModel()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            AppiumWebElement globalTree;
            AppiumWebElement toolModelMenu;

            ToolModel cspToolModel = Testdata.GetToolModel1();
            ToolModel scsToolModel = Testdata.GetToolModel2();
            ToolModel acToolModel = Testdata.GetToolModel3();
            ToolModel gedoreToolModel = Testdata.GetToolModel4();
            ToolModel schatzToolModel = Testdata.GetToolModel5();

            ToolModel cspToolModelChanged = Testdata.GetToolModelChanged1();
            ToolModel scsToolModelChanged = Testdata.GetToolModelChanged2();
            ToolModel acToolModelChanged = Testdata.GetToolModelChanged3();
            ToolModel gedoreToolModelChanged = Testdata.GetToolModelChanged4();
            ToolModel schatzToolModelChanged = Testdata.GetToolModelChanged5();

            //notwendige Einträge in Hilfstabellen anlegen
            //TODO später direkt in Toolmodel anlegen wenn Referenzbuttons implementiert wurden
            AddHelper(cspToolModelChanged.ConstructionType, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ConstructionType);

            //notwendige Einträge in Hilfstabellen anlegen
            //TODO später direkt in Toolmodel anlegen wenn Referenzbuttons implementiert wurden
            AddHelper(scsToolModelChanged.ToolType, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ToolType);
            AddHelper(scsToolModelChanged.DriveSize, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.DriveSize);
            AddHelper(scsToolModelChanged.DriveType, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.DriveType);


            //Auf Werkzeugmodellseite wechseln
            globalTree = QstSession.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);
            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);
            toolModelMenu = globalTree.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.ToolModel);
            toolModelMenu.Click();

            //evtl. übriggebliebene Toolmodel löschen falls vorhanden
            var toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, cspToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, scsToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, acToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, gedoreToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, schatzToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, cspToolModelChanged, btnDeleteModel);
            DeleteToolModel(QstSession, scsToolModelChanged, btnDeleteModel);
            DeleteToolModel(QstSession, acToolModelChanged, btnDeleteModel);
            DeleteToolModel(QstSession, gedoreToolModelChanged, btnDeleteModel);
            DeleteToolModel(QstSession, schatzToolModelChanged, btnDeleteModel);

            //Auf Werkzeugmodellseite wechseln
            globalTree = QstSession.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);
            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);
            toolModelMenu = globalTree.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.ToolModel);
            toolModelMenu.Click();

            //Create Toolmodel
            CreateToolModel(QstSession, cspToolModel);
            CreateToolModel(QstSession, scsToolModel);
            CreateToolModel(QstSession, acToolModel);
            CreateToolModel(QstSession, gedoreToolModel);
            CreateToolModel(QstSession, schatzToolModel);

            //Erneut auf Werkzeugmodellseite wechseln bis Bug QSTBV8-100 gelöst ist, da sonst beim Update statt SCS SALTUS ausgewählt wird
            //TODO evtl entfernen
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var toolModelTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.ToolModelTreeViewRoot, QstSession, 5, 10);

            //Check Toolmodel
            AssertToolModel(QstSession, cspToolModel);
            AssertToolModel(QstSession, scsToolModel);
            AssertToolModel(QstSession, acToolModel);
            AssertToolModel(QstSession, gedoreToolModel);
            AssertToolModel(QstSession, schatzToolModel);

            //AssertList
            var cspNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, cspToolModel.GetParentListWithToolModel());
            var scsNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, scsToolModel.GetParentListWithToolModel());
            var acNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, acToolModel.GetParentListWithToolModel());
            var gedoreNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, gedoreToolModel.GetParentListWithToolModel());
            var schatzNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, schatzToolModel.GetParentListWithToolModel());
            Assert.IsNotNull(cspNode, "CSP-Node konnte für Grid-Vergleich nicht gefunden werden");
            Assert.IsNotNull(scsNode, "SCS-Node konnte für Grid-Vergleich nicht gefunden werden");
            Assert.IsNotNull(acNode, "AtlasCopco-Node konnte für Grid-Vergleich nicht gefunden werden");
            Assert.IsNotNull(gedoreNode, "Gedore-Node konnte für Grid-Vergleich nicht gefunden werden");
            Assert.IsNotNull(schatzNode, "Schatz-Node konnte für Grid-Vergleich nicht gefunden werden");

            SelectToolModelNodesforGrid(QstSession, new List<AppiumWebElement>() { cspNode, scsNode, acNode, gedoreNode, schatzNode });

            //Neues Toolmodel in Grid prüfen
            AssertListToolModel(QstSession, toolModelView, cspToolModel);
            AssertListToolModel(QstSession, toolModelView, scsToolModel);

            //Change Toolmodels
            var cspToolModelNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, cspToolModel.GetParentListWithToolModel());
            cspToolModelNode.Click();
            UpdateToolModel(QstSession, cspToolModelChanged);
            VerifyAndApplyToolModelChanges(cspToolModel, cspToolModelChanged, 13, "changecomment cspToolmodel!");

            var scsToolModelNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, scsToolModel.GetParentListWithToolModel());
            scsToolModelNode.Click();
            UpdateToolModel(QstSession, scsToolModelChanged);
            VerifyAndApplyToolModelChanges(scsToolModel, scsToolModelChanged, 13, "changecomment scsToolmodel!");

            var acToolModelNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, acToolModel.GetParentListWithToolModel());
            acToolModelNode.Click();
            UpdateToolModel(QstSession, acToolModelChanged);
            VerifyAndApplyToolModelChanges(acToolModel, acToolModelChanged, 10, "changecomment acToolmodel!");

            var gedoreToolModelNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, gedoreToolModel.GetParentListWithToolModel());
            gedoreToolModelNode.Click();
            UpdateToolModel(QstSession, gedoreToolModelChanged);
            VerifyAndApplyToolModelChanges(gedoreToolModel, gedoreToolModelChanged, 11, "changecomment gedoreToolmodel!");

            var schatzToolModelNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, schatzToolModel.GetParentListWithToolModel());
            schatzToolModelNode.Click();
            UpdateToolModel(QstSession, schatzToolModelChanged);
            VerifyAndApplyToolModelChanges(schatzToolModel, schatzToolModelChanged, 9, "changecomment schatzToolmodel!");

            //TODO Auch mit Seitenwechsel testen wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-114 gefixed ist
            /*
            //Auf Werkzeugmodellseite wechseln
            globalTree = QstSession.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);
            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);
            toolModelMenu = globalTree.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.ToolModel);
            toolModelMenu.Click();*/

            //Check toolmodel Changes
            AssertToolModel(QstSession, cspToolModelChanged);
            AssertToolModel(QstSession, scsToolModelChanged);
            AssertToolModel(QstSession, acToolModelChanged);
            AssertToolModel(QstSession, gedoreToolModelChanged);
            AssertToolModel(QstSession, schatzToolModelChanged);

            //AssertList
            var cspChangedNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, cspToolModelChanged.GetParentListWithToolModel());
            var scsChangedNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, scsToolModelChanged.GetParentListWithToolModel());
            var acChangedNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, acToolModelChanged.GetParentListWithToolModel());
            var gedoreChangedNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, gedoreToolModelChanged.GetParentListWithToolModel());
            var schatzChangedNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, schatzToolModelChanged.GetParentListWithToolModel());
            Assert.IsNotNull(cspChangedNode, "CSP-ChangedNode konnte für Grid-Vergleich nicht gefunden werden");
            Assert.IsNotNull(scsChangedNode, "SCS-ChangedNode konnte für Grid-Vergleich nicht gefunden werden");
            Assert.IsNotNull(acChangedNode, "AtlasCopco-ChangedNode konnte für Grid-Vergleich nicht gefunden werden");
            Assert.IsNotNull(gedoreChangedNode, "Gedore-ChangedNode konnte für Grid-Vergleich nicht gefunden werden");
            Assert.IsNotNull(schatzChangedNode, "Schatz-ChangedNode konnte für Grid-Vergleich nicht gefunden werden");

            SelectToolModelNodesforGrid(QstSession, new List<AppiumWebElement>() { cspChangedNode, scsChangedNode, acChangedNode, gedoreChangedNode, schatzChangedNode });

            AssertListToolModel(QstSession, toolModelView, cspToolModelChanged);
            AssertListToolModel(QstSession, toolModelView, scsToolModelChanged);
            AssertListToolModel(QstSession, toolModelView, acToolModelChanged);
            AssertListToolModel(QstSession, toolModelView, gedoreToolModelChanged);
            AssertListToolModel(QstSession, toolModelView, schatzToolModelChanged);

            //Delete toolmodel
            btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);
            DeleteToolModel(QstSession, cspToolModelChanged, btnDeleteModel);
            DeleteToolModel(QstSession, scsToolModelChanged, btnDeleteModel);
            DeleteToolModel(QstSession, acToolModelChanged, btnDeleteModel);
            DeleteToolModel(QstSession, gedoreToolModelChanged, btnDeleteModel);
            DeleteToolModel(QstSession, schatzToolModelChanged, btnDeleteModel);

            //Check deletion
            globalTree = QstSession.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);
            ExpandMainMenuWhenNotOpened(AiStringHelper.MegaMainSubmodule.MainSelectorTreenames.MasterData, globalTree);
            toolModelMenu = globalTree.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.ToolModel);
            toolModelMenu.Click();

            toolModelTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.ToolModelTreeViewRoot, QstSession, 5, 10);
            cspChangedNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, cspToolModelChanged.GetParentListWithToolModel());
            Assert.IsNull(cspChangedNode);
            scsChangedNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, scsToolModelChanged.GetParentListWithToolModel());
            Assert.IsNull(scsChangedNode);
            acChangedNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, acToolModelChanged.GetParentListWithToolModel());
            Assert.IsNull(acChangedNode);
            gedoreChangedNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, gedoreToolModelChanged.GetParentListWithToolModel());
            Assert.IsNull(gedoreChangedNode);
            schatzChangedNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, schatzToolModelChanged.GetParentListWithToolModel());
            Assert.IsNull(schatzChangedNode);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestToolModelLongData()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            ToolModel invalidToolModel = Testdata.GetToolModelLongInvalidData();
            ToolModel validToolModel = Testdata.GetToolModelLongInvalidDataValid();
            ToolModel invalidToolModelForChange = Testdata.GetToolModelLongInvalidDataForChange();
            ToolModel validToolModelForChange = Testdata.GetToolModelLongInvalidDataForChangeValid();

            //Auf Werkzeugmodellseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //evtl. übriggebliebene Toolmodel löschen falls vorhanden
            var toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, invalidToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, validToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, invalidToolModelForChange, btnDeleteModel);
            DeleteToolModel(QstSession, validToolModelForChange, btnDeleteModel);

            //Auf Werkzeugmodellseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //Create Toolmodel
            CreateToolModel(QstSession, validToolModel, false, null, false, true, invalidToolModel);

            //TODO Seitenwechsel ausbauen wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-100 gefixed ist
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            AssertToolModel(QstSession, validToolModel);

            var toolModelTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.ToolModelTreeViewRoot, QstSession, 5, 10);
            var validToolModelNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, validToolModel.GetParentListWithToolModel());
            validToolModelNode.Click();
            UpdateToolModel(QstSession, invalidToolModelForChange);
            VerifyAndApplyToolModelChanges(validToolModel, validToolModelForChange, 1, "changecomment validToolmodel!");

            AssertToolModel(QstSession, validToolModelForChange);
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            AssertToolModel(QstSession, validToolModelForChange);

            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);
            DeleteToolModel(QstSession, validToolModelForChange, btnDeleteModel);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestToolModelWithTemplate()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            ToolModel toolModelTemplate = Testdata.GetToolModelTemplateForTemplateTest();
            ToolModel toolModel = Testdata.GetToolModelForTemplateTest();

            //Auf Werkzeugmodellseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //evtl. übriggebliebene Toolmodel löschen falls vorhanden
            var toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, toolModelTemplate, btnDeleteModel);
            DeleteToolModel(QstSession, toolModel, btnDeleteModel);

            //Auf Werkzeugmodellseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //Create Toolmodel
            CreateToolModel(QstSession, toolModelTemplate);
            AssertToolModel(QstSession, toolModelTemplate);

            CreateToolModel(QstSession, toolModel, true, toolModelTemplate);
            AssertToolModel(QstSession, toolModel);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);
            AssertToolModel(QstSession, toolModel);
            AssertToolModel(QstSession, toolModelTemplate);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);
            DeleteToolModel(QstSession, toolModel, btnDeleteModel);
            DeleteToolModel(QstSession, toolModelTemplate, btnDeleteModel);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestToolModelDuplicateDescription()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            ToolModel toolmodelDuplicateId = Testdata.GetToolModelDuplicateDescription();

            //Auf Toolmodelseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //evtl. übriggebliebene Tms löschen falls vorhanden
            var tmView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var btnDelete = tmView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, toolmodelDuplicateId, btnDelete);

            //Auf Tmseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //Create Tm
            CreateToolModel(QstSession, toolmodelDuplicateId);

            tmView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);

            var addTmBtn = tmView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.AddToolModel);

            addTmBtn.Click();
            Thread.Sleep(500);
            var assistantSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.Assistant.View, WindowsApplicationDriverUrl);
            var assistantNextBtn = TestHelper.FindElementWithWait(AiStringHelper.Assistant.Next, assistantSession);
            var textInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputText, assistantSession);

            textInput.Clear();
            TestHelper.SendKeysWithBackslash(assistantSession, textInput, toolmodelDuplicateId.Description);
            assistantNextBtn.Click();
            CheckAndCloseValidationWindow(assistantSession, ValidationStringHelper.GeneralValidationStrings.FieldRequiredUnique);
            var cancel = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.Cancel);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(assistantSession, cancel);
            cancel.Click();

            //Delete Toolmodel
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            tmView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            btnDelete = tmView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);
            DeleteToolModel(QstSession, toolmodelDuplicateId, btnDelete);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestToolModelOnChangeSwitchToolModel()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            ToolModel toolModelChangeSite1 = Testdata.GetToolModelChangeSite1();
            ToolModel toolModelChangeSite1Changed = Testdata.GetToolModelChangeSite1Changed();
            ToolModel toolModelChangeSite2 = Testdata.GetToolModelChangeSite2();

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //evtl. übriggebliebene Toolmodel löschen falls vorhanden
            var toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var btnDelete = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, toolModelChangeSite1, btnDelete);
            DeleteToolModel(QstSession, toolModelChangeSite1Changed, btnDelete);
            DeleteToolModel(QstSession, toolModelChangeSite2, btnDelete);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //Create ToolModel
            CreateToolModel(QstSession, toolModelChangeSite1);
            CreateToolModel(QstSession, toolModelChangeSite2);

            //TODO Seitenwechsel ausbauen wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-100 gefixed ist
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var toolModelTreeView = TestHelper.FindElementByAiWithWaitFromParent(toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelTreeView, QstSession);
            var toolModelTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolModelTreeView, AiStringHelper.ToolModel.ToolModelView.ToolModelTreeViewRoot, QstSession);

            var toolModelChangeSite1Node = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, toolModelChangeSite1.GetParentListWithToolModel());
            toolModelChangeSite1Node.Click();

            //Cancel
            UpdateToolModel(QstSession, toolModelChangeSite1Changed, false);

            var toolModelChangeSite2Node = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, toolModelChangeSite2.GetParentListWithToolModel());
            toolModelChangeSite2Node.Click();

            //var viewVerifyChanges = TestHelper.FindElementByAiWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            //Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");


            //TODO Ändern wenn Verify-Fenster statt Kombination aus einfachem Nachfragefenster + Verify-Fenster aufgerufen wird
            //var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            //VerifyToolModelChangesInVerifyView(toolModelChangeSite1, toolModelChangeSite1Changed, listViewChanges, 4);
            //var btnCancel = DesktSession.FindElementByXPath(AiStringHelper.GeneralStrings.YesBtn) viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Cancel);
            //TestHelper.WaitForElementToBeEnabledAndDisplayed(DesktSession, btnCancel);
            //btnCancel.Click();

            CheckAndCloseValidationWindow(QstSession, ValidationStringHelper.GeneralValidationStrings.DoYouWantToSaveChanges, AiStringHelper.GeneralStrings.CancelBtn);

            AssertToolModel(QstSession, toolModelChangeSite1Changed);

            //Reset
            UpdateToolModel(QstSession, toolModelChangeSite1Changed, false);

            toolModelChangeSite2Node = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, toolModelChangeSite2.GetParentListWithToolModel());
            toolModelChangeSite2Node.Click();

            CheckAndCloseValidationWindow(QstSession, ValidationStringHelper.GeneralValidationStrings.DoYouWantToSaveChanges, AiStringHelper.GeneralStrings.NoBtn);

            //viewVerifyChanges = TestHelper.FindElementByAiWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            //Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");
            //listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            //VerifyToolModelChangesInVerifyView(toolModelChangeSite1, toolModelChangeSite1Changed, listViewChanges, 4);

            //var btnReset = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Reset);
            //btnReset.Click();

            //TODO Evtl. Seitenwechsel löschen wenn Bug https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-138 gefixed ist
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);
            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            toolModelTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelTreeViewRoot, QstSession);

            toolModelChangeSite1Node = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, toolModelChangeSite1.GetParentListWithToolModel());
            toolModelChangeSite1Node.Click();

            AssertToolModel(QstSession, toolModelChangeSite1);

            //Apply
            UpdateToolModel(QstSession, toolModelChangeSite1Changed, false);

            toolModelChangeSite2Node = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, toolModelChangeSite2.GetParentListWithToolModel());
            toolModelChangeSite2Node.Click();

            //TODO Evtl. CloseValidationwindow löschen, wenn nur noch Verify-Window erscheint wie bei Messpunkt und WKZ
            CheckAndCloseValidationWindow(QstSession, ValidationStringHelper.GeneralValidationStrings.DoYouWantToSaveChanges, AiStringHelper.GeneralStrings.YesBtn);

            var viewVerifyChanges = TestHelper.FindElementWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");
            var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyToolModelChangesInVerifyView(toolModelChangeSite1, toolModelChangeSite1Changed, listViewChanges, 4);

            var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            btnApply.Click();

            var toolModelChangeSite1ChangedNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, toolModelChangeSite1Changed.GetParentListWithToolModel());
            toolModelChangeSite1ChangedNode.Click();

            AssertToolModel(QstSession, toolModelChangeSite1Changed);

            //Delete 
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            btnDelete = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, toolModelChangeSite2, btnDelete);
            DeleteToolModel(QstSession, toolModelChangeSite1Changed, btnDelete);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestToolModelOnChangeChangeSite()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            ToolModel toolModelChangeSite1 = Testdata.GetToolModelChangeSite1();
            ToolModel toolModelChangeSite1Changed = Testdata.GetToolModelChangeSite1Changed();
            ToolModel toolModelChangeSite2 = Testdata.GetToolModelChangeSite2();

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //evtl. übriggebliebene Toolmodel löschen falls vorhanden
            var toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var btnDelete = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, toolModelChangeSite1, btnDelete);
            DeleteToolModel(QstSession, toolModelChangeSite1Changed, btnDelete);
            DeleteToolModel(QstSession, toolModelChangeSite2, btnDelete);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //Create ToolModel
            CreateToolModel(QstSession, toolModelChangeSite1);
            CreateToolModel(QstSession, toolModelChangeSite2);

            //TODO Seitenwechsel ausbauen wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-100 gefixed ist
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var toolModelTreeView = TestHelper.FindElementByAiWithWaitFromParent(toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelTreeView, QstSession);
            var toolModelTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolModelTreeView, AiStringHelper.ToolModel.ToolModelView.ToolModelTreeViewRoot, QstSession);

            var toolModelChangeSite1Node = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, toolModelChangeSite1.GetParentListWithToolModel());
            toolModelChangeSite1Node.Click();

            //Cancel
            UpdateToolModel(QstSession, toolModelChangeSite1Changed, false);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //var viewVerifyChanges = TestHelper.FindElementByAiWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            //Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");


            //TODO Ändern wenn Verify-Fenster statt einfachem Nachfragefenster aufgerufen wird
            //var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            //VerifyToolModelChangesInVerifyView(toolModelChangeSite1, toolModelChangeSite1Changed, listViewChanges, 4);
            //var btnCancel = DesktSession.FindElementByXPath(AiStringHelper.GeneralStrings.YesBtn) viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Cancel);
            //TestHelper.WaitForElementToBeEnabledAndDisplayed(DesktSession, btnCancel);
            //btnCancel.Click();

            CheckAndCloseValidationWindow(QstSession, ValidationStringHelper.GeneralValidationStrings.DoYouWantToSaveChanges, AiStringHelper.GeneralStrings.CancelBtn);

            AssertToolModel(QstSession, toolModelChangeSite1Changed);

            //Reset
            UpdateToolModel(QstSession, toolModelChangeSite1Changed, false);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            CheckAndCloseValidationWindow(QstSession, ValidationStringHelper.GeneralValidationStrings.DoYouWantToSaveChanges, AiStringHelper.GeneralStrings.NoBtn);

            //viewVerifyChanges = TestHelper.FindElementByAiWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            //Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");
            //listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            //VerifyToolModelChangesInVerifyView(toolModelChangeSite1, toolModelChangeSite1Changed, listViewChanges, 4);

            //var btnReset = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Reset);
            //btnReset.Click();

            //TODO Evtl. Seitenwechsel löschen wenn Bug https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-138 gefixed ist
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);
            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            toolModelTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelTreeViewRoot, QstSession);

            toolModelChangeSite1Node = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, toolModelChangeSite1.GetParentListWithToolModel());
            toolModelChangeSite1Node.Click();

            AssertToolModel(QstSession, toolModelChangeSite1);

            //Apply
            UpdateToolModel(QstSession, toolModelChangeSite1Changed, false);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //TODO Evtl. CloseValidationwindow löschen, wenn nur noch Verify-Window erscheint wie bei Messpunkt und WKZ
            CheckAndCloseValidationWindow(QstSession, ValidationStringHelper.GeneralValidationStrings.DoYouWantToSaveChanges, AiStringHelper.GeneralStrings.YesBtn);

            var viewVerifyChanges = TestHelper.FindElementWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");
            var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyToolModelChangesInVerifyView(toolModelChangeSite1, toolModelChangeSite1Changed, listViewChanges, 4);

            var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            btnApply.Click();

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            toolModelTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelTreeViewRoot, QstSession);
            var toolModelChangeSite1ChangedNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, toolModelChangeSite1Changed.GetParentListWithToolModel());
            toolModelChangeSite1ChangedNode.Click();

            AssertToolModel(QstSession, toolModelChangeSite1Changed);

            //Delete 
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            btnDelete = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, toolModelChangeSite2, btnDelete);
            DeleteToolModel(QstSession, toolModelChangeSite1Changed, btnDelete);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestToolModelOnChangeLogout()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            ToolModel toolModelChangeSite1 = Testdata.GetToolModelChangeSite1();
            ToolModel toolModelChangeSite1Changed = Testdata.GetToolModelChangeSite1Changed();
            ToolModel toolModelChangeSite2 = Testdata.GetToolModelChangeSite2();

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //evtl. übriggebliebene Toolmodel löschen falls vorhanden
            var toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var btnDelete = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, toolModelChangeSite1, btnDelete);
            DeleteToolModel(QstSession, toolModelChangeSite1Changed, btnDelete);
            DeleteToolModel(QstSession, toolModelChangeSite2, btnDelete);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //Create ToolModel
            CreateToolModel(QstSession, toolModelChangeSite1);
            CreateToolModel(QstSession, toolModelChangeSite2);

            //TODO Seitenwechsel ausbauen wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-100 gefixed ist
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var toolModelTreeView = TestHelper.FindElementByAiWithWaitFromParent(toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelTreeView, QstSession);
            var toolModelTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolModelTreeView, AiStringHelper.ToolModel.ToolModelView.ToolModelTreeViewRoot, QstSession);

            var toolModelChangeSite1Node = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, toolModelChangeSite1.GetParentListWithToolModel());
            toolModelChangeSite1Node.Click();

            //Cancel
            UpdateToolModel(QstSession, toolModelChangeSite1Changed, false);

            var logout = QstSession.FindElementByAccessibilityId(AiStringHelper.GlobalToolbar.LogOut);
            logout.Click();

            //var viewVerifyChanges = TestHelper.FindElementByAiWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            //Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");


            //TODO Ändern wenn Verify-Fenster statt einfachem Nachfragefenster aufgerufen wird
            //var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            //VerifyToolModelChangesInVerifyView(toolModelChangeSite1, toolModelChangeSite1Changed, listViewChanges, 4);
            //var btnCancel = DesktSession.FindElementByXPath(AiStringHelper.GeneralStrings.YesBtn) viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Cancel);
            //TestHelper.WaitForElementToBeEnabledAndDisplayed(DesktSession, btnCancel);
            //btnCancel.Click();

            CheckAndCloseValidationWindow(QstSession, ValidationStringHelper.GeneralValidationStrings.DoYouWantToSaveChanges, AiStringHelper.GeneralStrings.CancelBtn);

            AssertToolModel(QstSession, toolModelChangeSite1Changed);

            //Reset
            UpdateToolModel(QstSession, toolModelChangeSite1Changed, false);

            logout = QstSession.FindElementByAccessibilityId(AiStringHelper.GlobalToolbar.LogOut);
            logout.Click();

            CheckAndCloseValidationWindow(QstSession, ValidationStringHelper.GeneralValidationStrings.DoYouWantToSaveChanges, AiStringHelper.GeneralStrings.NoBtn);
            //TODO 2.es Nachfragefenster schließen entfernen wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-155 fertig ist
            CheckAndCloseValidationWindow(QstSession, ValidationStringHelper.GeneralValidationStrings.DoYouWantToSaveChanges, AiStringHelper.GeneralStrings.NoBtn);

            //viewVerifyChanges = TestHelper.FindElementByAiWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            //Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");
            //listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            //VerifyToolModelChangesInVerifyView(toolModelChangeSite1, toolModelChangeSite1Changed, listViewChanges, 4);

            //var btnReset = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Reset);
            //btnReset.Click();

            //TODO Evtl. Seitenwechsel löschen wenn Bug https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-138 gefixed ist

            LoginAsCSP(true);
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);
            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            toolModelTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelTreeViewRoot, QstSession);

            toolModelChangeSite1Node = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, toolModelChangeSite1.GetParentListWithToolModel());
            toolModelChangeSite1Node.Click();

            AssertToolModel(QstSession, toolModelChangeSite1);

            //Apply
            UpdateToolModel(QstSession, toolModelChangeSite1Changed, false);

            logout = QstSession.FindElementByAccessibilityId(AiStringHelper.GlobalToolbar.LogOut);
            logout.Click();

            //TODO Evtl. CloseValidationwindow löschen, wenn nur noch Verify-Window erscheint wie bei Messpunkt und WKZ
            CheckAndCloseValidationWindow(QstSession, ValidationStringHelper.GeneralValidationStrings.DoYouWantToSaveChanges, AiStringHelper.GeneralStrings.YesBtn);

            var viewVerifyChanges = TestHelper.FindElementWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");
            var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyToolModelChangesInVerifyView(toolModelChangeSite1, toolModelChangeSite1Changed, listViewChanges, 4);

            var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            btnApply.Click();

            LoginAsCSP(true);
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            toolModelTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelTreeViewRoot, QstSession);
            var toolModelChangeSite1ChangedNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, toolModelChangeSite1Changed.GetParentListWithToolModel());
            toolModelChangeSite1ChangedNode.Click();

            AssertToolModel(QstSession, toolModelChangeSite1Changed);

            //Delete 
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            btnDelete = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, toolModelChangeSite2, btnDelete);
            DeleteToolModel(QstSession, toolModelChangeSite1Changed, btnDelete);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestToolModelUndoChanges()
        {
            LoginAsCSP();
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            ToolModel toolModelForUpdate = Testdata.GetToolModelForUndoChanges();
            ToolModel toolModelUpdate = Testdata.GetToolModelForUndoChangesUpdate();

            var btnDelete = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel, QstSession);
            DeleteToolModel(QstSession, toolModelForUpdate, btnDelete);
            DeleteToolModel(QstSession, toolModelUpdate, btnDelete);

            //Hilfseinträge fürs updaten anlegen
            AddHelper(toolModelUpdate.ToolType, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ToolType);
            AddHelper(toolModelUpdate.SwitchOff, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.SwitchOff);
            AddHelper(toolModelUpdate.ShutOff, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ShutOff);
            AddHelper(toolModelUpdate.DriveSize, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.DriveSize);
            AddHelper(toolModelUpdate.DriveType, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.DriveType);
            AddHelper(toolModelUpdate.ConstructionType, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ConstructionType);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);
            CreateToolModel(QstSession, toolModelForUpdate);

            //TODO entfernen wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-100 gefixed ist
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            var toolModelTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.ToolModelTreeViewRoot, QstSession, 5, 10);
            AppiumWebElement toolModelNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, toolModelForUpdate.GetParentListWithToolModel());
            toolModelNode.Click();

            UpdateToolModel(QstSession, toolModelUpdate, false);
            var save = QstSession.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SaveToolModel);
            Assert.AreEqual(true, save.Enabled, "Speichern-Button sollte aktiv sein");

            UpdateToolModel(QstSession, toolModelForUpdate, false);
            save = QstSession.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SaveToolModel);
            Assert.AreEqual(false, save.Enabled, "Speichern-Button sollte NICHT aktiv sein");

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);
            var viewVerifyChanges = TestHelper.TryFindElementByAccessabilityId(AiStringHelper.VerifyChanges.View, DesktSession);
            Assert.IsNull(viewVerifyChanges);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);
            AssertToolModel(QstSession, toolModelForUpdate);

            btnDelete = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel, QstSession);
            DeleteToolModel(QstSession, toolModelForUpdate, btnDelete);

            //Hilfseinträge wieder löschen
            DeleteHelpers(new List<string> { toolModelForUpdate.ToolType, toolModelUpdate.ToolType } , AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ToolType);
            DeleteHelpers(new List<string> { toolModelForUpdate.SwitchOff, toolModelUpdate.SwitchOff }, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.SwitchOff);
            DeleteHelpers(new List<string> { toolModelForUpdate.ShutOff, toolModelUpdate.ShutOff }, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ShutOff);
            DeleteHelpers(new List<string> { toolModelForUpdate.DriveSize, toolModelUpdate.DriveSize }, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.DriveSize);
            DeleteHelpers(new List<string> { toolModelForUpdate.DriveType, toolModelUpdate.DriveType }, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.DriveType);
            DeleteHelpers(new List<string> { toolModelForUpdate.ConstructionType, toolModelUpdate.ConstructionType }, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ConstructionType);
        }


        public static void CreateToolModel(
            WindowsDriver<WindowsElement> QstSession,
            ToolModel toolModel,
            bool withTemplateCheck = false,
            ToolModel template = null,
            bool withCheckValidationErrors = false,
            bool withLongValues = false,
            ToolModel invalidToolModel = null)
        {
            Assert.IsNotNull(QstSession, "QstSession in CreateToolModel ist null");
            var toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var addToolModelBtn = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.AddToolModel);
            var toolModelTreeViewRoot = TestHelper.FindElementByAiWithWaitFromParent(toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelTreeViewRoot, QstSession);
            var toolModelNode = TestHelper.GetNode(QstSession, toolModelTreeViewRoot, toolModel.GetParentListWithToolModel());
            if (toolModelNode != null)
            {
                return;
            }

            addToolModelBtn.Click();
            Thread.Sleep(500);
            var assistantSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.Assistant.View, TestConfiguration.GetWindowsApplicationDriverUrl());

            var textInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputText, assistantSession);
            if (withTemplateCheck)
            {
                AssertAssistantListEntry(assistantSession, template.Description, AssistantStringHelper.ToolModelStrings.Description);
            }

            textInput.Clear();
            if (withLongValues)
            {
                TestHelper.SendKeysWithBackslash(assistantSession, textInput, invalidToolModel.Description);
            }
            else
            {
                TestHelper.SendKeysConverted(textInput, toolModel.Description);
            }
            AssertAssistantListEntry(assistantSession, toolModel.Description, AssistantStringHelper.ToolModelStrings.Description);
            var assistantNextBtn = TestHelper.FindElementWithWait(AiStringHelper.Assistant.Next, assistantSession);
            assistantNextBtn.Click();

            var listInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputList, assistantSession);
            if (withTemplateCheck)
            {
                AssertAssistantListEntry(assistantSession, template.Manufacturer, AssistantStringHelper.ToolModelStrings.Manufacturer);
            }
            var manuEntry = TestHelper.FindElementByAiWithWaitFromParent(listInput, toolModel.Manufacturer, assistantSession);
            //var manuEntry = listInput.FindElementByAccessibilityId(toolModel.Manufacturer);
            manuEntry.Click();
            AssertAssistantListEntry(assistantSession, toolModel.Manufacturer, AssistantStringHelper.ToolModelStrings.Manufacturer);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                AssertAssistantListEntry(assistantSession, template.ToolModelType, AssistantStringHelper.ToolModelStrings.ToolModelType);
            }
            var toolModelTypeEntry = listInput.FindElementByAccessibilityId(toolModel.ToolModelType);
            toolModelTypeEntry.Click();
            AssertAssistantListEntry(assistantSession, toolModel.ToolModelType, AssistantStringHelper.ToolModelStrings.ToolModelType);
            assistantNextBtn.Click();

            if (toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.ClickWrench
                || toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.ProductionWrench
                || toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.MdWrench)
            {
                if (toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.ClickWrench)
                {
                    var beamTypeTorqueWrenchWithScaleEntry = listInput.FindElementByAccessibilityId(ToolModel.ToolModelClassStrings.ClickWrenchClass.BeamTypeTorqueWrenchWithScale);
                    var driverFixSetEntry = listInput.FindElementByAccessibilityId(ToolModel.ToolModelClassStrings.ClickWrenchClass.DriverFixSet);
                    var driverScaleEntry = listInput.FindElementByAccessibilityId(ToolModel.ToolModelClassStrings.ClickWrenchClass.DriverScale);
                    var driverWithoutScaleEntry = listInput.FindElementByAccessibilityId(ToolModel.ToolModelClassStrings.ClickWrenchClass.DriverWithoutScale);
                    var wrenchConfScaleEntry = listInput.FindElementByAccessibilityId(ToolModel.ToolModelClassStrings.ClickWrenchClass.WrenchConfScale);
                    var wrenchFixSetEntry = listInput.FindElementByAccessibilityId(ToolModel.ToolModelClassStrings.ClickWrenchClass.WrenchFixSet);
                    var wrenchWithoutScaleEntry = listInput.FindElementByAccessibilityId(ToolModel.ToolModelClassStrings.ClickWrenchClass.WrenchWithoutScale);
                    Assert.IsNotNull(beamTypeTorqueWrenchWithScaleEntry);
                    Assert.IsNotNull(driverFixSetEntry);
                    Assert.IsNotNull(driverScaleEntry);
                    Assert.IsNotNull(driverWithoutScaleEntry);
                    Assert.IsNotNull(wrenchConfScaleEntry);
                    Assert.IsNotNull(wrenchFixSetEntry);
                    Assert.IsNotNull(wrenchWithoutScaleEntry);
                }
                //Klasse für Prod-Schlüsseln/Anz. MD Schlüsseln zusammengefasst
                else if (toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.ProductionWrench || toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.MdWrench)
                {
                    var beamTypeTorqueWrenchEntry = listInput.FindElementByAccessibilityId(ToolModel.ToolModelClassStrings.Md_ProductionWrenchClass.BeamTypeTorqueWrench);
                    var driverElectronicEntry = listInput.FindElementByAccessibilityId(ToolModel.ToolModelClassStrings.Md_ProductionWrenchClass.DriverElectronic);
                    var driverWithDialIndicatorEntry = listInput.FindElementByAccessibilityId(ToolModel.ToolModelClassStrings.Md_ProductionWrenchClass.DriverWithDialIndicator);
                    var wrenchElectronicEntry = listInput.FindElementByAccessibilityId(ToolModel.ToolModelClassStrings.Md_ProductionWrenchClass.WrenchElectronic);
                    var wrenchWithDialIndicatorEntry = listInput.FindElementByAccessibilityId(ToolModel.ToolModelClassStrings.Md_ProductionWrenchClass.WrenchWithDialIndicator);
                    Assert.IsNotNull(beamTypeTorqueWrenchEntry);
                    Assert.IsNotNull(driverElectronicEntry);
                    Assert.IsNotNull(driverWithDialIndicatorEntry);
                    Assert.IsNotNull(wrenchElectronicEntry);
                    Assert.IsNotNull(wrenchWithDialIndicatorEntry);
                }
                if (withTemplateCheck)
                {
                    AssertAssistantListEntry(assistantSession, template.ToolModelClass, AssistantStringHelper.ToolModelStrings.ToolModelClass);
                }
                var classEntry = listInput.FindElementByAccessibilityId(toolModel.ToolModelClass);
                classEntry.Click();
                AssertAssistantListEntry(assistantSession, toolModel.ToolModelClass, AssistantStringHelper.ToolModelStrings.ToolModelClass);
                assistantNextBtn.Click();
            }

            var floatingPointInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputFloatingPoint, assistantSession);
            if (toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General || toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriver || toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriverShutOff)
            {
                if (withTemplateCheck)
                {
                    AssertAssistantListEntry(assistantSession, template.AirPressure.ToString(currentCulture), AssistantStringHelper.ToolModelStrings.AirPressure, AssistantStringHelper.UnitStrings.Bar);
                }
                floatingPointInput.SendKeys(toolModel.AirPressure.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, toolModel.AirPressure.ToString(currentCulture), AssistantStringHelper.ToolModelStrings.AirPressure, AssistantStringHelper.UnitStrings.Bar);
                assistantNextBtn.Click();

                if (withTemplateCheck)
                {
                    AssertAssistantListEntry(assistantSession, template.AirConsumption.ToString(currentCulture), AssistantStringHelper.ToolModelStrings.AirConsumption);
                }
                floatingPointInput.SendKeys(toolModel.AirConsumption.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, toolModel.AirConsumption.ToString(currentCulture), AssistantStringHelper.ToolModelStrings.AirConsumption);
                assistantNextBtn.Click();
            }

            if (toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General || toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.EcDriver)
            {
                if (withTemplateCheck)
                {
                    AssertAssistantListEntry(assistantSession, template.BattVoltage.ToString(currentCulture), AssistantStringHelper.ToolModelStrings.BatteryVoltage, AssistantStringHelper.UnitStrings.Volt);
                }
                floatingPointInput.SendKeys(toolModel.BattVoltage.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, toolModel.BattVoltage.ToString(currentCulture), AssistantStringHelper.ToolModelStrings.BatteryVoltage, AssistantStringHelper.UnitStrings.Volt);
                assistantNextBtn.Click();
            }

            if (toolModel.ToolModelType != ToolModel.ToolModelTypeStrings.ClickWrench && toolModel.ToolModelType != ToolModel.ToolModelTypeStrings.ProductionWrench && toolModel.ToolModelType != ToolModel.ToolModelTypeStrings.MdWrench)
            {
                if (withTemplateCheck)
                {
                    AssertAssistantListEntry(assistantSession, template.MaxRotSpeed.ToString(currentCulture), AssistantStringHelper.ToolModelStrings.MaxRotSpeed, AssistantStringHelper.UnitStrings.RevolutionPerMin);
                }
                var integerInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputInteger, assistantSession);
                integerInput.SendKeys(toolModel.MaxRotSpeed.ToString());
                AssertAssistantListEntry(assistantSession, toolModel.MaxRotSpeed.ToString(), AssistantStringHelper.ToolModelStrings.MaxRotSpeed, AssistantStringHelper.UnitStrings.RevolutionPerMin);
                assistantNextBtn.Click();
            }

            if (withTemplateCheck)
            {
                AssertAssistantListEntry(assistantSession, template.MinPow.ToString(currentCulture), AssistantStringHelper.ToolModelStrings.MinPower, AssistantStringHelper.UnitStrings.Nm);
            }
            floatingPointInput.SendKeys(toolModel.MinPow.ToString(numberFormatThreeDecimals, currentCulture));
            AssertAssistantListEntry(assistantSession, toolModel.MinPow.ToString(currentCulture), AssistantStringHelper.ToolModelStrings.MinPower, AssistantStringHelper.UnitStrings.Nm);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                AssertAssistantListEntry(assistantSession, template.MaxPow.ToString(currentCulture), AssistantStringHelper.ToolModelStrings.MaxPower, AssistantStringHelper.UnitStrings.Nm);
            }
            floatingPointInput.SendKeys(toolModel.MaxPow.ToString(numberFormatThreeDecimals, currentCulture));
            AssertAssistantListEntry(assistantSession, toolModel.MaxPow.ToString(currentCulture), AssistantStringHelper.ToolModelStrings.MaxPower, AssistantStringHelper.UnitStrings.Nm);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                AssertAssistantListEntry(assistantSession, template.ToolType, AssistantStringHelper.ToolModelStrings.ToolType);
            }
            var toolTypeEntry = FindOrCreateHelperListEntryInAssistWindow(toolModel.ToolType, assistantSession, listInput);
            toolTypeEntry.Click();
            AssertAssistantListEntry(assistantSession, toolModel.ToolModelType, AssistantStringHelper.ToolModelStrings.ToolModelType);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                AssertAssistantListEntry(assistantSession, template.Weight.ToString(currentCulture), AssistantStringHelper.ToolModelStrings.Weight, AssistantStringHelper.UnitStrings.Kg);
            }
            floatingPointInput.SendKeys(toolModel.Weight.ToString(numberFormatThreeDecimals, currentCulture));
            AssertAssistantListEntry(assistantSession, toolModel.Weight.ToString(currentCulture), AssistantStringHelper.ToolModelStrings.Weight, AssistantStringHelper.UnitStrings.Kg);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                AssertAssistantListEntry(assistantSession, template.SwitchOff, AssistantStringHelper.ToolModelStrings.SwitchOff);
            }
            var switchOffEntry = FindOrCreateHelperListEntryInAssistWindow(toolModel.SwitchOff, assistantSession, listInput);
            switchOffEntry.Click();
            AssertAssistantListEntry(assistantSession, toolModel.SwitchOff, AssistantStringHelper.ToolModelStrings.SwitchOff);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                AssertAssistantListEntry(assistantSession, template.ShutOff, AssistantStringHelper.ToolModelStrings.ShutOff);
            }
            var shutOffEntry = FindOrCreateHelperListEntryInAssistWindow(toolModel.ShutOff, assistantSession, listInput);
            shutOffEntry.Click();
            AssertAssistantListEntry(assistantSession, toolModel.ShutOff, AssistantStringHelper.ToolModelStrings.ShutOff);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                AssertAssistantListEntry(assistantSession, template.DriveSize, AssistantStringHelper.ToolModelStrings.DriveSize);
            }
            var driveSizeEntry = FindOrCreateHelperListEntryInAssistWindow(toolModel.DriveSize, assistantSession, listInput);
            driveSizeEntry.Click();
            AssertAssistantListEntry(assistantSession, toolModel.DriveSize, AssistantStringHelper.ToolModelStrings.DriveSize);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                AssertAssistantListEntry(assistantSession, template.DriveType, AssistantStringHelper.ToolModelStrings.DriveType);
            }
            var driveTypeEntry = FindOrCreateHelperListEntryInAssistWindow(toolModel.DriveType, assistantSession, listInput);
            driveTypeEntry.Click();
            AssertAssistantListEntry(assistantSession, toolModel.DriveType, AssistantStringHelper.ToolModelStrings.DriveType);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                AssertAssistantListEntry(assistantSession, template.ConstructionType, AssistantStringHelper.ToolModelStrings.ConstructionType);
            }
            var constructionTypeEntry = FindOrCreateHelperListEntryInAssistWindow(toolModel.ConstructionType, assistantSession, listInput);
            constructionTypeEntry.Click();
            AssertAssistantListEntry(assistantSession, toolModel.ConstructionType, AssistantStringHelper.ToolModelStrings.ConstructionType);
            assistantNextBtn.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
        }
        private static void AssertToolModel(WindowsDriver<WindowsElement> QstSession, ToolModel toolModel)
        {
            /*var globalTree = QstSession.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.MegaMainSubmoduleSelector);
            ExpandMainMenuWhenNotOpened("MasterData", globalTree);
            var toolModelMenu = globalTree.FindElementByAccessibilityId(AiStringHelper.MegaMainSubmodule.ToolModel);
            toolModelMenu.Click();*/
            var toolModelTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.ToolModelTreeViewRoot, QstSession, 5, 10);
            AppiumWebElement toolModelNode = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, toolModel.GetParentListWithToolModel());
            toolModelNode.Click();

            var toolModelView = QstSession.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.View);
            var toolModelParamTab = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.ParamTab);
            var scrollViewer = toolModelParamTab.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.ParamTabScrollViewer);
            scrollViewer.SendKeys(Keys.PageUp);

            var description = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMDescription);
            Assert.AreEqual(toolModel.Description, description.Text);

            //Feld für Klasse ist nur bei "click Wrench", "md Wrench" und "production wrench" vorhanden
            var toolModelClass = TestHelper.TryFindElementBy(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMClass, toolModelView);
            if (toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.ClickWrench ||
                toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.MdWrench ||
                toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.ProductionWrench)
            {
                string selectedToolModelClass = GetToolModelComboboxString(toolModelView, toolModelClass, QstSession);
                Assert.AreEqual(toolModel.ToolModelClass, selectedToolModelClass);
            }
            else
            {
                Assert.IsNull(toolModelClass);
            }

            var toolModelType = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMToolModelType);
            string selectedToolModelType = GetToolModelComboboxString(toolModelView, toolModelType, QstSession);
            Assert.AreEqual(toolModel.ToolModelType, selectedToolModelType);

            var manufacturer = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMManu);
            string selectedManufacturerText = GetToolModelComboboxString(toolModelView, manufacturer, QstSession);
            Assert.AreEqual(toolModel.Manufacturer, selectedManufacturerText);

            var lowerLimit = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMLowLim);
            Assert.AreEqual(toolModel.MinPow.ToString(numberFormatThreeDecimals, currentCulture), lowerLimit.Text);

            var upperLimit = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMUpLim);
            Assert.AreEqual(toolModel.MaxPow.ToString(numberFormatThreeDecimals, currentCulture), upperLimit.Text);

            //Feld für Luftdruck ist nur bei "Pulse drivern", "Pulse driver Shut Off" und "General" vorhanden
            var airPressure = TestHelper.TryFindElementBy(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMAirPressure, toolModelView);
            if (toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriver ||
                toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriverShutOff ||
                toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General)
            {
                Assert.AreEqual(toolModel.AirPressure.ToString(numberFormatThreeDecimals, currentCulture), airPressure.Text);
            }
            else
            {
                Assert.IsNull(airPressure);
            }

            var toolType = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMToolType);
            string selectedToolTypeText = GetToolModelComboboxString(toolModelView, toolType, QstSession);
            Assert.AreEqual(toolModel.ToolType, selectedToolTypeText);

            var weight = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMWeight);
            Assert.AreEqual(toolModel.Weight.ToString(numberFormatThreeDecimals, currentCulture), weight.Text);

            //Feld für maximale Umdrehungen ist nur bei "Pulse drivern", "Pulse driver Shut Off", "General" und EC Drivern vorhanden
            var maxRotSpeed = TestHelper.TryFindElementBy(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMMaxRot, toolModelView);
            if (toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriver ||
                toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriverShutOff ||
                toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General ||
                toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.EcDriver)
            {
                Assert.AreEqual(toolModel.MaxRotSpeed.ToString(numberFormatThreeDecimals, currentCulture), maxRotSpeed.Text);
            }
            else
            {
                Assert.IsNull(maxRotSpeed);
            }

            //Feld für Batteriespannung ist nur bei "General" und "Ec Drivern" vorhanden
            var battVoltage = TestHelper.TryFindElementBy(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMBattVolt, toolModelView);
            if (toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General ||
                toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.EcDriver)
            {
                Assert.AreEqual(toolModel.BattVoltage.ToString(numberFormatThreeDecimals, currentCulture), battVoltage.Text);
            }
            else
            {
                Assert.IsNull(battVoltage);
            }


            //Feld für Luftverbrauch ist nur bei "Pulse drivern", "Pulse driver Shut Off" und "General" vorhanden
            var airConsumption = TestHelper.TryFindElementBy(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMAirConsumpt, toolModelView);
            if (toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriver ||
                toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriverShutOff ||
                toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General)
            {
                Assert.AreEqual(toolModel.AirConsumption.ToString(numberFormatThreeDecimals, currentCulture), airConsumption.Text);
            }
            else
            {
                Assert.IsNull(airConsumption);
            }

            var switchOff = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMSwitchOff);
            string selectedSwitchOffText = GetToolModelComboboxString(toolModelView, switchOff, QstSession);
            Assert.AreEqual(toolModel.SwitchOff, selectedSwitchOffText);

            var driveSize = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMDriveSize);
            string selectedDriveSizeText = GetToolModelComboboxString(toolModelView, driveSize, QstSession);
            Assert.AreEqual(toolModel.DriveSize, selectedDriveSizeText);

            var shutOff = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMShutOff);
            string selectedShutOffText = GetToolModelComboboxString(toolModelView, shutOff, QstSession);
            Assert.AreEqual(toolModel.ShutOff, selectedShutOffText);

            var driveType = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMDriveType);
            string selectedDriveTypeText = GetToolModelComboboxString(toolModelView, driveType, QstSession);
            Assert.AreEqual(toolModel.DriveType, selectedDriveTypeText);

            var constructionType = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMConstrType);
            string selectedConstructionTypeText = GetToolModelComboboxString(toolModelView, constructionType, QstSession);
            Assert.AreEqual(toolModel.ConstructionType, selectedConstructionTypeText);
        }
        private static void AssertListToolModel(WindowsDriver<WindowsElement> QstSession, AppiumWebElement toolModelView, ToolModel toolModel)
        {
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.Description, toolModel.Description);
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.ToolModelType, toolModel.ToolModelType);
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.Class, toolModel.ToolModelClass);
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.Manufacturer, toolModel.Manufacturer);
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.LowPowLim, toolModel.MinPow.ToString(numberFormatThreeDecimals, currentCulture));
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.UpperPowLim, toolModel.MaxPow.ToString(numberFormatThreeDecimals, currentCulture));
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.AirPressure, toolModel.AirPressure.ToString(numberFormatThreeDecimals, currentCulture));
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.ToolType, toolModel.ToolType);
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.Weight, toolModel.Weight.ToString(numberFormatThreeDecimals, currentCulture));
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.BatteryVolt, toolModel.BattVoltage.ToString(numberFormatThreeDecimals, currentCulture));
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.MaxRotSpeed, toolModel.MaxRotSpeed.ToString(numberFormatThreeDecimals, currentCulture));
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.AirConsumption, toolModel.AirConsumption.ToString(numberFormatThreeDecimals, currentCulture));
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.SwitchOff, toolModel.SwitchOff);
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.DriveSize, toolModel.DriveSize);
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.ShutOff, toolModel.ShutOff);
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.DriveType, toolModel.DriveType);
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.ConstructionType, toolModel.ConstructionType);

            //TODO Change when implemented
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.LimitCM, 1.670.ToString(numberFormatThreeDecimals, currentCulture));
            TestHelper.AssertGridRow(QstSession, toolModelView, AiStringHelper.ToolModel.ToolModelView.ToolModelGrid, AiStringHelper.ToolModel.ToolModelView.ToolModelGridHeader, AiStringHelper.ToolModel.ToolModelView.ToolModelGridRowPrefix, toolModel.Description, ToolModel.ToolModelListHeaderStrings.LimitCMK, 1.670.ToString(numberFormatThreeDecimals, currentCulture));
        }
        private static void VerifyToolModelChangesInVerifyView(ToolModel toolModel, ToolModel toolModelChanged, AppiumWebElement listViewChanges, int numberOfExpectedChanges)
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
                    case "Description":
                        Assert.AreEqual(toolModel.Description, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolModelChanged.Description, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Tool model type":
                        Assert.AreEqual(toolModel.ToolModelType, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolModelChanged.ToolModelType, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Manufacturer":
                        Assert.AreEqual(toolModel.Manufacturer, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolModelChanged.Manufacturer, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Class":
                        Assert.AreEqual(toolModel.ToolModelClass, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolModelChanged.ToolModelClass, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Min. power":
                        Assert.AreEqual(toolModel.MinPow.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolModelChanged.MinPow.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Max. power":
                        Assert.AreEqual(toolModel.MaxPow.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolModelChanged.MaxPow.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Air pressure":
                        Assert.AreEqual(toolModel.AirPressure.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolModelChanged.AirPressure.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Tool type":
                        Assert.AreEqual(toolModel.ToolType, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolModelChanged.ToolType, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Weight":
                        Assert.AreEqual(toolModel.Weight.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolModelChanged.Weight.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Max. rotation speed":
                        Assert.AreEqual(toolModel.MaxRotSpeed.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolModelChanged.MaxRotSpeed.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Air consumption":
                        Assert.AreEqual(toolModel.AirConsumption.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolModelChanged.AirConsumption.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Switch off":
                        Assert.AreEqual(toolModel.SwitchOff, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolModelChanged.SwitchOff, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Drive size":
                        Assert.AreEqual(toolModel.DriveSize, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolModelChanged.DriveSize, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Shut off":
                        Assert.AreEqual(toolModel.ShutOff, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolModelChanged.ShutOff, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Drive type":
                        Assert.AreEqual(toolModel.DriveType, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolModelChanged.DriveType, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Construction type":
                        Assert.AreEqual(toolModel.ConstructionType, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(toolModelChanged.ConstructionType, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    default:
                        Assert.IsTrue(false, string.Format("Case '{0}' not implemented", changeTypeText));
                        break;
                }
                i++;
            }
        }
        private static void VerifyAndApplyToolModelChanges(ToolModel toolModel, ToolModel toolModelChanged, int numberOfChanges, string changeComment)
        {
            var viewVerifyChanges = DesktSession.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.View);
            var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyToolModelChangesInVerifyView(toolModel, toolModelChanged, listViewChanges, numberOfChanges);
            var textBoxComment = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Comment);
            TestHelper.SendKeysConverted(textBoxComment, changeComment);

            var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            btnApply.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
        }
        private static string GetToolModelComboboxString(WindowsElement toolModelView, AppiumWebElement comboBox, WindowsDriver<WindowsElement> driver)
        {
            var toolModelParamTab = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.ParamTab);
            var scrollViewer = toolModelParamTab.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.ParamTabScrollViewer);

            return TestHelper.GetSelectedComboboxStringWithScrolling(driver, comboBox, scrollViewer);
        }
        public static void DeleteToolModel(WindowsDriver<WindowsElement> QstSession, ToolModel model, AppiumWebElement btnDeleteModel)
        {
            var toolModelTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.ToolModelTreeViewRoot, QstSession, 5, 10);
            var node = TestHelper.GetNode(QstSession, toolModelTreeviewRootNode, model.GetParentListWithToolModel());
            if (node != null)
            {
                node.Click();
                TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, btnDeleteModel);
                btnDeleteModel.Click();

                var confirmDeleteBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
                confirmDeleteBtn.Click();
                TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            }
        }
        /// <summary>
        /// Updated Toolmodel Seite muss vorher ausgewählt und Element ausgewählt sein
        /// </summary>
        private static void UpdateToolModel(
            WindowsDriver<WindowsElement> QstSession,
            ToolModel toolModel,
            bool save = true)
        {
            var toolModelView = QstSession.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.View);
            var saveToolModelBtn = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SaveToolModel);
            var toolModelParamTab = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.ParamTab);
            var scrollViewer = toolModelParamTab.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.ParamTabScrollViewer);
            scrollViewer.SendKeys(Keys.PageUp);

            var fieldDescription = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMDescription);
            fieldDescription.Clear();
            TestHelper.SendKeysConverted(fieldDescription, toolModel.Description);

            //var test = GetWindowSession(AiStringHelper.MainWindow.Window);
            //var testview = test.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.View);
            //var manu = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMManu);
            var comboBoxManufacturer = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMManu);
            ClickToolModelComboBoxEntryWithScrolling(QstSession, toolModel.Manufacturer, comboBoxManufacturer, toolModelView);

            var comboBoxToolModelType = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMToolModelType);
            ClickToolModelComboBoxEntryWithScrolling(QstSession, toolModel.ToolModelType, comboBoxToolModelType, toolModelView);

            var comboBoxClass = TestHelper.TryFindElementBy(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMClass, toolModelView);
            if (toolModel.ToolModelType != ToolModel.ToolModelTypeStrings.ClickWrench &
                toolModel.ToolModelType != ToolModel.ToolModelTypeStrings.MdWrench &
                toolModel.ToolModelType != ToolModel.ToolModelTypeStrings.ProductionWrench)
            {
                Assert.IsNull(comboBoxClass);
            }
            else
            {
                ClickToolModelComboBoxEntryWithScrolling(QstSession, toolModel.ToolModelClass, comboBoxClass, toolModelView);
            }

            var inputAirPressure = TestHelper.TryFindElementBy(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMAirPressure, toolModelView);
            var inputAirConsumpt = TestHelper.TryFindElementBy(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMAirConsumpt, toolModelView);

            if (toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General || toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriver || toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriverShutOff)
            {
                inputAirPressure.Clear();
                inputAirPressure.SendKeys(Keys.ArrowRight);
                inputAirPressure.SendKeys(toolModel.AirPressure.ToString(numberFormatThreeDecimals, currentCulture));
                inputAirConsumpt.Clear();
                inputAirConsumpt.SendKeys(Keys.ArrowRight);
                inputAirConsumpt.SendKeys(toolModel.AirConsumption.ToString(numberFormatThreeDecimals, currentCulture));
            }
            else
            {
                Assert.IsNull(inputAirPressure);
                Assert.IsNull(inputAirConsumpt);
            }

            var inputBattVolt = TestHelper.TryFindElementBy(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMBattVolt, toolModelView);

            if (toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General || toolModel.ToolModelType == ToolModel.ToolModelTypeStrings.EcDriver)
            {
                inputBattVolt.Clear();
                inputBattVolt.SendKeys(Keys.ArrowRight);
                inputBattVolt.SendKeys(toolModel.BattVoltage.ToString(numberFormatThreeDecimals, currentCulture));
            }
            else
            {
                Assert.IsNull(inputBattVolt);
            }

            var inputMaxRotSpeed = TestHelper.TryFindElementBy(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMMaxRot, toolModelView);
            if (toolModel.ToolModelType != ToolModel.ToolModelTypeStrings.ClickWrench && toolModel.ToolModelType != ToolModel.ToolModelTypeStrings.ProductionWrench && toolModel.ToolModelType != ToolModel.ToolModelTypeStrings.MdWrench)
            {
                inputMaxRotSpeed.Clear();
                inputMaxRotSpeed.SendKeys(Keys.ArrowRight);
                inputMaxRotSpeed.SendKeys(toolModel.MaxRotSpeed.ToString());
            }
            else
            {
                Assert.IsNull(inputMaxRotSpeed);
            }

            var inputMinPow = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMLowLim);
            inputMinPow.Clear();
            inputMinPow.SendKeys(Keys.ArrowRight);
            inputMinPow.SendKeys(toolModel.MinPow.ToString(numberFormatThreeDecimals, currentCulture));

            var inputMaxPow = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMUpLim);
            inputMaxPow.Clear();
            inputMaxPow.SendKeys(Keys.ArrowRight);
            inputMaxPow.SendKeys(toolModel.MaxPow.ToString(numberFormatThreeDecimals, currentCulture));

            var comboBoxToolType = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMToolType);
            ClickToolModelComboBoxEntryWithScrolling(QstSession, toolModel.ToolType, comboBoxToolType, toolModelView);

            var inputWeight = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMWeight);
            inputWeight.Clear();
            inputWeight.SendKeys(Keys.ArrowRight);
            inputWeight.SendKeys(toolModel.Weight.ToString(numberFormatThreeDecimals, currentCulture));

            var comboBoxSwitchOff = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMSwitchOff);
            ClickToolModelComboBoxEntryWithScrolling(QstSession, toolModel.SwitchOff, comboBoxSwitchOff, toolModelView);

            var comboBoxDriveSize = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMDriveSize);
            ClickToolModelComboBoxEntryWithScrolling(QstSession, toolModel.DriveSize, comboBoxDriveSize, toolModelView);

            var comboBoxShutOff = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMShutOff);
            ClickToolModelComboBoxEntryWithScrolling(QstSession, toolModel.ShutOff, comboBoxShutOff, toolModelView);

            var comboBoxDriveType = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMDriveType);
            ClickToolModelComboBoxEntryWithScrolling(QstSession, toolModel.DriveType, comboBoxDriveType, toolModelView);

            var comboBoxConstructionType = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.SMConstrType);
            ClickToolModelComboBoxEntryWithScrolling(QstSession, toolModel.ConstructionType, comboBoxConstructionType, toolModelView);

            if (save)
            {
                saveToolModelBtn.Click();
            }
        }
        private static void ClickToolModelComboBoxEntryWithScrolling(WindowsDriver<WindowsElement> driver, string entryString, AppiumWebElement comboBox, WindowsElement toolModelView)
        {
            var toolModelParamTab = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.ParamTab);
            var scrollViewer = toolModelParamTab.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.SingleModel.ParamTabScrollViewer);
            TestHelper.ClickComboBoxEntryWithScrolling(driver, entryString, comboBox, scrollViewer);
        }
        private static void SelectToolModelNodesforGrid(WindowsDriver<WindowsElement> QstSession, List<AppiumWebElement> listNodes)
        {
            Assert.IsFalse(listNodes.Count < 2, "Zu wenig Einträge zum vergleichen; Testfall anpassen!!!");

            //Implementierung über Offset weil bei Actions.Click([Element]) in die Mitte des Elements geklickt wird und nicht am Anfang wie bei Element.Click()
            //Wenn das ToolModel jetzt über die Hälfte bereits außerhalb des sichtbaren Bereichs liegt landet der Click nicht auf dem Toolmodel
            //TODO Evtl. Scrolling einbauen ähnlich ClickMpFolder
            var actions = new Actions(QstSession);

            foreach (var item in listNodes)
            {
                actions.MoveToElement(item);
                actions.MoveByOffset(20 - item.Size.Width / 2, 0);
                actions.Click();
                if (item == listNodes.First())
                {
                    actions.KeyDown(Keys.Control);
                }
            }
            actions.KeyUp(Keys.Control);
            actions.Build().Perform();
        }
    }
}