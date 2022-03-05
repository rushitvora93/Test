using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;
using OpenQA.Selenium.Appium;
using System;

using UI_TestProjekt.Helper;
using UI_TestProjekt.TestModel;
using static UI_TestProjekt.MeasurementpointTests;
using static UI_TestProjekt.ToolModelTests;
using static UI_TestProjekt.ToolTests;
using static UI_TestProjekt.AuxiliaryMasterDataTests;
using static UI_TestProjekt.TestPlanningMasterDataTests;

namespace UI_TestProjekt
{
    [TestClass]
    public class ToolTestingTests : TestBase
    {
        [TestMethod]
        [TestCategory("MasterData")]
        public void TestMpToolAllocation()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, WindowsApplicationDriverUrl);

            MpToolAllocation mpToolAllocation1 = Testdata.GetMpToolAllocation1();
            MpToolAllocation mpToolAllocation2 = Testdata.GetMpToolAllocation2();
            MpToolAllocation mpToolAllocation3 = Testdata.GetMpToolAllocation3();
            MpToolAllocation mpToolAllocation4 = Testdata.GetMpToolAllocation4();

            // Löschen von Überbleibseln aus altem Test
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MpToolAllocation);
            string statusAfterRemoveAllocationString = mpToolAllocation1.Tool.Status;
            RemoveToolAllocation(statusAfterRemoveAllocationString, mpToolAllocation1, QstSession);
            statusAfterRemoveAllocationString = mpToolAllocation2.Tool.Status;
            RemoveToolAllocation(statusAfterRemoveAllocationString, mpToolAllocation2, QstSession);
            statusAfterRemoveAllocationString = mpToolAllocation3.Tool.Status;
            RemoveToolAllocation(statusAfterRemoveAllocationString, mpToolAllocation3, QstSession);
            statusAfterRemoveAllocationString = mpToolAllocation4.Tool.Status;
            RemoveToolAllocation(statusAfterRemoveAllocationString, mpToolAllocation4, QstSession);

            //Auf Toolseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);
            var toolView = QstSession.FindElementByAccessibilityId(AiStringHelper.Tool.View);
            var btnDeleteTool = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.DeleteTool);

            DeleteTool(QstSession, mpToolAllocation1.Tool, btnDeleteTool);
            DeleteTool(QstSession, mpToolAllocation2.Tool, btnDeleteTool);
            DeleteTool(QstSession, mpToolAllocation3.Tool, btnDeleteTool);
            DeleteTool(QstSession, mpToolAllocation4.Tool, btnDeleteTool);

            //Auf Toolmodelseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //übriggebliebene Toolmodel löschen
            var toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            var btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, mpToolAllocation1.Tool.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, mpToolAllocation2.Tool.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, mpToolAllocation3.Tool.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, mpToolAllocation4.Tool.ToolModel, btnDeleteModel);

            //Auf Mpseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Mps löschen
            var mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            var btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMp(QstSession, mpToolAllocation1.Mp, btnDelete);
            DeleteMp(QstSession, mpToolAllocation2.Mp, btnDelete);
            DeleteMp(QstSession, mpToolAllocation3.Mp, btnDelete);
            DeleteMp(QstSession, mpToolAllocation4.Mp, btnDelete);

            //TestLevelSets löschen
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestPlanningMasterData);
            DeleteTestLevelSet(QstSession, mpToolAllocation1.ToolControlConditions.TestLevelSetChk);
            DeleteTestLevelSet(QstSession, mpToolAllocation1.ToolControlConditions.TestLevelSetMca);
            DeleteTestLevelSet(QstSession, mpToolAllocation2.ToolControlConditions.TestLevelSetChk);
            DeleteTestLevelSet(QstSession, mpToolAllocation2.ToolControlConditions.TestLevelSetMca);
            DeleteTestLevelSet(QstSession, mpToolAllocation3.ToolControlConditions.TestLevelSetChk);
            DeleteTestLevelSet(QstSession, mpToolAllocation3.ToolControlConditions.TestLevelSetMca);
            DeleteTestLevelSet(QstSession, mpToolAllocation4.ToolControlConditions.TestLevelSetChk);
            DeleteTestLevelSet(QstSession, mpToolAllocation4.ToolControlConditions.TestLevelSetMca);
            //Ende Löschen von Überbleibseln aus altem Test

            //Create Entities

            // TestLevelSets erstellen
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestPlanningMasterData);
            CreateTestLevelSet(QstSession, mpToolAllocation1.ToolControlConditions.TestLevelSetChk);
            VerifyAndApplyTestLevelChanges(new TestLevelSet(), mpToolAllocation1.ToolControlConditions.TestLevelSetChk, new int[] { 1, 4, 0, 3 }, mpToolAllocation1.ToolControlConditions.TestLevelSetChk.Name + " created");
            CreateTestLevelSet(QstSession, mpToolAllocation2.ToolControlConditions.TestLevelSetChk);
            VerifyAndApplyTestLevelChanges(new TestLevelSet(), mpToolAllocation2.ToolControlConditions.TestLevelSetChk, new int[] { 1, 1, 4, 5 }, mpToolAllocation2.ToolControlConditions.TestLevelSetChk.Name + " created");
            CreateTestLevelSet(QstSession, mpToolAllocation3.ToolControlConditions.TestLevelSetChk);
            VerifyAndApplyTestLevelChanges(new TestLevelSet(), mpToolAllocation3.ToolControlConditions.TestLevelSetChk, new int[] { 1, 4, 4, 0 }, mpToolAllocation3.ToolControlConditions.TestLevelSetChk.Name + " created");

            //Auf MpSeite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Create Mp
            CreateMp(QstSession, mpToolAllocation1.Mp);
            CreateMp(QstSession, mpToolAllocation2.Mp);
            CreateMp(QstSession, mpToolAllocation3.Mp);
            CreateMp(QstSession, mpToolAllocation4.Mp);

            //Auf ToolModelseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            CreateToolModel(QstSession, mpToolAllocation1.Tool.ToolModel);
            CreateToolModel(QstSession, mpToolAllocation2.Tool.ToolModel);
            CreateToolModel(QstSession, mpToolAllocation3.Tool.ToolModel);
            CreateToolModel(QstSession, mpToolAllocation4.Tool.ToolModel);

            AddHelper(mpToolAllocation1.Tool.ConfigurableField, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ConfigurableFieldTool);
            AddHelper(mpToolAllocation2.Tool.ConfigurableField, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ConfigurableFieldTool);
            AddHelper(mpToolAllocation3.Tool.ConfigurableField, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ConfigurableFieldTool);
            AddHelper(mpToolAllocation4.Tool.ConfigurableField, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ConfigurableFieldTool);

            AddHelper(mpToolAllocation1.Tool.CostCenter, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.CostCenter);
            AddHelper(mpToolAllocation2.Tool.CostCenter, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.CostCenter);
            AddHelper(mpToolAllocation3.Tool.CostCenter, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.CostCenter);
            AddHelper(mpToolAllocation4.Tool.CostCenter, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.CostCenter);

            //Auf Toolseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);

            CreateTool(QstSession, mpToolAllocation1.Tool);
            CreateTool(QstSession, mpToolAllocation2.Tool);
            CreateTool(QstSession, mpToolAllocation3.Tool);
            CreateTool(QstSession, mpToolAllocation4.Tool);

            //Eigentlicher Test
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MpToolAllocation);

            var mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.MpTreeViewRoot, QstSession);
            var mpToolAllocation1MpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpToolAllocation1.Mp.GetParentListWithMp());
            mpToolAllocation1MpNode.Click();

            var locTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.MpParamTab, QstSession);
            locTab.Click();
            AssertMpToolAllocationMpTab(mpToolAllocation1.Mp, QstSession);

            var toolTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.ToolTreeViewRoot, QstSession);
            var mpToolAllocation1ToolNode = TestHelper.GetNode(QstSession, toolTreeviewRootNode, mpToolAllocation1.Tool.GetParentListWithTool());
            mpToolAllocation1ToolNode.Click();

            var toolTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.ToolParamTab, QstSession);
            toolTab.Click();
            AssertMpToolAllocationToolTab(mpToolAllocation1.Tool, QstSession);

            string statusAfterAllocationString = "In Betrieb";
            AllocateTool(mpToolAllocation1, statusAfterAllocationString);

            var mpToolAllocationTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.MpToolAllocTab, QstSession);
            mpToolAllocationTab.Click();
            AssertMpToolAllocationMpToolTab(QstSession, mpToolAllocation1, mpToolAllocationTab);

            //zweites WKZ zuordnen
            var mpToolAllocation2MpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpToolAllocation2.Mp.GetParentListWithMp());
            mpToolAllocation2MpNode.Click();

            toolTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.ToolTreeViewRoot, QstSession);
            var mpToolAllocation2ToolNode = TestHelper.GetNode(QstSession, toolTreeviewRootNode, mpToolAllocation2.Tool.GetParentListWithTool());
            mpToolAllocation2ToolNode.Click();

            string status2AfterAllocationString = "In Betrieb";
            //TODO entfernen bzw. beim Zuordnen Status wechseln wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-118 gefixed ist
            status2AfterAllocationString = mpToolAllocation2.Tool.Status;
            AllocateTool(mpToolAllocation2, status2AfterAllocationString, mpToolAllocation1.ToolUsage);

            locTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.MpParamTab, QstSession);
            locTab.Click();
            AssertMpToolAllocationMpTab(mpToolAllocation2.Mp, QstSession);

            toolTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.ToolParamTab, QstSession);
            toolTab.Click();
            AssertMpToolAllocationToolTab(mpToolAllocation2.Tool, QstSession);

            mpToolAllocationTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.MpToolAllocTab, QstSession);
            mpToolAllocationTab.Click();
            AssertMpToolAllocationMpToolTab(QstSession, mpToolAllocation2, mpToolAllocationTab);

            //Test AllocatedToolsList
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MpToolAllocation);

            mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.MpTreeViewRoot, QstSession);
            mpToolAllocation1MpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpToolAllocation1.Mp.GetParentListWithMp());
            mpToolAllocation1MpNode.Click();

            var allocatedToolsTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.AllocatedToolsTab, QstSession);
            allocatedToolsTab.Click();

            //TODO entkommentieren wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-146 gefixed ist
            //AssertListAllocatedTools(QstSession, allocatedToolsTab, mpToolAllocation1);
            //AssertListAllocatedTools(QstSession, allocatedToolsTab, mpToolAllocation2);

            //3. MP_WKZ-Zuordnung 
            SelectMpToolAllocation(mpToolAllocation3, QstSession);
            string status3AfterAllocationString = "In Betrieb";
            AllocateTool(mpToolAllocation3, status3AfterAllocationString);

            locTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.MpParamTab, QstSession);
            locTab.Click();
            AssertMpToolAllocationMpTab(mpToolAllocation3.Mp, QstSession);

            toolTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.ToolParamTab, QstSession);
            toolTab.Click();
            AssertMpToolAllocationToolTab(mpToolAllocation3.Tool, QstSession);

            mpToolAllocationTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.MpToolAllocTab, QstSession);
            mpToolAllocationTab.Click();
            AssertMpToolAllocationMpToolTab(QstSession, mpToolAllocation3, mpToolAllocationTab);

            //Test AllocatedToolsList
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MpToolAllocation);

            mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.MpTreeViewRoot, QstSession);
            var mpToolAllocation3MpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpToolAllocation3.Mp.GetParentListWithMp());
            mpToolAllocation3MpNode.Click();

            allocatedToolsTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.AllocatedToolsTab, QstSession);
            allocatedToolsTab.Click();

            //TODO entkommentieren wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-146 gefixed ist
            //AssertListAllocatedTools(QstSession, allocatedToolsTab, mpToolAllocation3);

            //4. MP_WKZ-Zuordnung 
            SelectMpToolAllocation(mpToolAllocation4, QstSession);
            string status4AfterAllocationString = "In Betrieb";
            AllocateTool(mpToolAllocation4, status4AfterAllocationString);

            locTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.MpParamTab, QstSession);
            locTab.Click();
            AssertMpToolAllocationMpTab(mpToolAllocation4.Mp, QstSession);

            toolTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.ToolParamTab, QstSession);
            toolTab.Click();
            AssertMpToolAllocationToolTab(mpToolAllocation4.Tool, QstSession);

            mpToolAllocationTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.MpToolAllocTab, QstSession);
            mpToolAllocationTab.Click();
            AssertMpToolAllocationMpToolTab(QstSession, mpToolAllocation4, mpToolAllocationTab);

            //Test AllocatedToolsList
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MpToolAllocation);

            mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.MpTreeViewRoot, QstSession);
            var mpToolAllocation4MpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpToolAllocation4.Mp.GetParentListWithMp());
            mpToolAllocation4MpNode.Click();

            allocatedToolsTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.AllocatedToolsTab, QstSession);
            allocatedToolsTab.Click();

            //TODO entkommentieren wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-146 gefixed ist
            //AssertListAllocatedTools(QstSession, allocatedToolsTab, mpToolAllocation4);

            //Create TestConditions
            SelectMpToolAllocation(mpToolAllocation1, QstSession);
            var mpToolAllocView = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.View, QstSession);
            var createTestConditions = mpToolAllocView.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.CreateTestConditions);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, createTestConditions);
            createTestConditions.Click();
            CreateTestConditions(mpToolAllocation1);

            //AssertTestConditions1
            AssertTestConditionsTab(QstSession, mpToolAllocation1);
            allocatedToolsTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.AllocatedToolsTab, QstSession);
            allocatedToolsTab.Click();
            //TODO entkommentieren wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-146 gefixed ist
            //AssertListAllocatedTools(QstSession, allocatedToolsTab, mpToolAllocation1);

            SelectMpToolAllocation(mpToolAllocation2, QstSession);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, createTestConditions);
            createTestConditions.Click();
            createTestConditions = mpToolAllocView.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.CreateTestConditions);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, createTestConditions);
            createTestConditions.Click();
            CreateTestConditions(mpToolAllocation2);

            SelectMpToolAllocation(mpToolAllocation3, QstSession);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, createTestConditions);
            createTestConditions.Click();
            createTestConditions = mpToolAllocView.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.CreateTestConditions);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, createTestConditions);
            createTestConditions.Click();
            CreateTestConditions(mpToolAllocation3);

            SelectMpToolAllocation(mpToolAllocation4, QstSession);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, createTestConditions);
            createTestConditions.Click();
            createTestConditions = mpToolAllocView.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.CreateTestConditions);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, createTestConditions);
            createTestConditions.Click();
            CreateTestConditions(mpToolAllocation4);

            //AssertTestConditions 2-4
            SelectMpToolAllocation(mpToolAllocation2, QstSession);
            AssertTestConditionsTab(QstSession, mpToolAllocation2);
            allocatedToolsTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.AllocatedToolsTab, QstSession);
            allocatedToolsTab.Click();
            //TODO entkommentieren wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-146 gefixed ist
            //AssertListAllocatedTools(QstSession, allocatedToolsTab, mpToolAllocation2);

            SelectMpToolAllocation(mpToolAllocation3, QstSession);
            AssertTestConditionsTab(QstSession, mpToolAllocation3);
            allocatedToolsTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.AllocatedToolsTab, QstSession);
            allocatedToolsTab.Click();
            //TODO entkommentieren wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-146 gefixed ist
            //AssertListAllocatedTools(QstSession, allocatedToolsTab, mpToolAllocation3);

            SelectMpToolAllocation(mpToolAllocation4, QstSession);
            AssertTestConditionsTab(QstSession, mpToolAllocation4);
            allocatedToolsTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.AllocatedToolsTab, QstSession);
            allocatedToolsTab.Click();
            //TODO entkommentieren wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-146 gefixed ist
            //AssertListAllocatedTools(QstSession, allocatedToolsTab, mpToolAllocation4);

            //MpToolAllocation1 aufheben
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MpToolAllocation);
            statusAfterRemoveAllocationString = "Im Lager";
            RemoveMpToolAllocationWithAssert(mpToolAllocation1, statusAfterRemoveAllocationString, QstSession);

            //MpToolAllocation2 Aufheben
            statusAfterRemoveAllocationString = "Zur Reparatur";
            RemoveMpToolAllocationWithAssert(mpToolAllocation2, statusAfterRemoveAllocationString, QstSession);

            //MpToolAllocation3 Aufheben
            statusAfterRemoveAllocationString = "Zur Reparatur";
            RemoveMpToolAllocationWithAssert(mpToolAllocation3, statusAfterRemoveAllocationString, QstSession);

            //MpToolAllocation4 Aufheben
            statusAfterRemoveAllocationString = "In Betrieb";
            RemoveMpToolAllocationWithAssert(mpToolAllocation4, statusAfterRemoveAllocationString, QstSession);

            //Auf Toolseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.Tool);
            toolView = QstSession.FindElementByAccessibilityId(AiStringHelper.Tool.View);
            btnDeleteTool = toolView.FindElementByAccessibilityId(AiStringHelper.Tool.DeleteTool);

            DeleteTool(QstSession, mpToolAllocation1.Tool, btnDeleteTool);
            DeleteTool(QstSession, mpToolAllocation2.Tool, btnDeleteTool);
            DeleteTool(QstSession, mpToolAllocation3.Tool, btnDeleteTool);
            DeleteTool(QstSession, mpToolAllocation4.Tool, btnDeleteTool);

            //Auf Toolmodelseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            //übriggebliebene Toolmodel löschen
            toolModelView = TestHelper.FindElementWithWait(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            btnDeleteModel = toolModelView.FindElementByAccessibilityId(AiStringHelper.ToolModel.ToolModelView.DeleteToolModel);

            DeleteToolModel(QstSession, mpToolAllocation1.Tool.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, mpToolAllocation2.Tool.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, mpToolAllocation3.Tool.ToolModel, btnDeleteModel);
            DeleteToolModel(QstSession, mpToolAllocation4.Tool.ToolModel, btnDeleteModel);

            //Auf MP-Seite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Mps löschen
            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMp(QstSession, mpToolAllocation1.Mp, btnDelete);
            DeleteMp(QstSession, mpToolAllocation2.Mp, btnDelete);
            DeleteMp(QstSession, mpToolAllocation3.Mp, btnDelete);
            DeleteMp(QstSession, mpToolAllocation4.Mp, btnDelete);

            //erneuter Seitenwechsel wegen https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-116
            //Auf MP-Seite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMpFolder(QstSession, new List<string> { Testdata.MpRootNode, "MpToolAllocations" }, btnDelete);

            //TestLevelSets löschen
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestPlanningMasterData);
            DeleteTestLevelSet(QstSession, mpToolAllocation1.ToolControlConditions.TestLevelSetChk);
            DeleteTestLevelSet(QstSession, mpToolAllocation1.ToolControlConditions.TestLevelSetMca);
            DeleteTestLevelSet(QstSession, mpToolAllocation2.ToolControlConditions.TestLevelSetChk);
            DeleteTestLevelSet(QstSession, mpToolAllocation2.ToolControlConditions.TestLevelSetMca);
            DeleteTestLevelSet(QstSession, mpToolAllocation3.ToolControlConditions.TestLevelSetChk);
            DeleteTestLevelSet(QstSession, mpToolAllocation3.ToolControlConditions.TestLevelSetMca);
            DeleteTestLevelSet(QstSession, mpToolAllocation4.ToolControlConditions.TestLevelSetChk);
            DeleteTestLevelSet(QstSession, mpToolAllocation4.ToolControlConditions.TestLevelSetMca);
        }


        //Ordnet Werkzeuge zu und testet ob die angegebene toolUsage NICHT mehr vorhanden ist (weil schon bei anderer Zuordnung verwendet)
        public static void AllocateTool(MpToolAllocation mpToolAllocation, string statusAfterAllocationString, string toolUsage = "")
        {
            var allocateTool = QstSession.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.AllocateTool);
            allocateTool.Click();

            Thread.Sleep(500);
            var assistantSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.Assistant.View, TestConfiguration.GetWindowsApplicationDriverUrl());
            var listInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputList, assistantSession);
            var assistantNextBtn = TestHelper.FindElementWithWait(AiStringHelper.Assistant.Next, assistantSession);

            if (!string.IsNullOrEmpty(toolUsage))
            {
                //Die verwendete WKZ-Verwendung darf nur einmal verwendet werden
                var toolUsageEntry = TestHelper.TryFindElementBy(toolUsage, listInput);
                Assert.IsNull(toolUsageEntry, string.Format("Die bereits ausgewählte WKZVerwendung {0} kann immer noch ausgewählt werden", toolUsage));
            }

            var mpToolAllocationToolUsage = FindOrCreateHelperListEntryInAssistWindow(mpToolAllocation.ToolUsage, assistantSession, listInput);
            mpToolAllocationToolUsage.Click();
            AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolUsage, AssistantStringHelper.MpToolAllocation.ToolUsage);
            assistantNextBtn.Click();

            var statusAfterAllocation = FindOrCreateHelperListEntryInAssistWindow(statusAfterAllocationString, assistantSession, listInput);
            statusAfterAllocation.Click();
            AssertAssistantListEntry(assistantSession, statusAfterAllocationString, AssistantStringHelper.MpToolAllocation.StatusForTool);
            assistantNextBtn.Click();
            mpToolAllocation.Tool.Status = statusAfterAllocationString;

            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            //TODO: Aktion erfolgreich Notification erscheint 2mal evtl. entfernen falls gefixed
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
        }
        private static void AssertMpToolAllocationMpToolTab(WindowsDriver<WindowsElement> QstSession, MpToolAllocation mpToolAllocation, WindowsElement mpToolAllocationTab)
        {
            var mpToolAllocationMp = mpToolAllocationTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpToolAllocTabElements.MeasurementPoint);
            Assert.AreEqual(mpToolAllocation.Mp.GetMpTreeName(), mpToolAllocationMp.Text);
            var mpToolAllocationTool = mpToolAllocationTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpToolAllocTabElements.Tool);
            Assert.AreEqual(mpToolAllocation.Tool.GetTreeString(), mpToolAllocationTool.Text);

            var mpToolAllocationToolUsage = mpToolAllocationTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpToolAllocTabElements.ToolUsage);
            string selectedToolUsage = TestHelper.GetSelectedComboboxString(QstSession, mpToolAllocationToolUsage);
            Assert.AreEqual(mpToolAllocation.ToolUsage, selectedToolUsage);
        }
        private static void AssertMpToolAllocationToolTab(Tool tool, WindowsDriver<WindowsElement> QstSession)
        {
            var toolParamTabScrollViewer = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.ToolParamTabElements.ScrollViewer, QstSession);
            var inventoryNumber = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.InventoryNumber);
            Assert.AreEqual(tool.InventoryNumber, inventoryNumber.Text);
            var serialNumber = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.SerialNumber);
            Assert.AreEqual(tool.SerialNumber, serialNumber.Text);
            var toolModelDescription = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.ModelDescription);
            Assert.AreEqual(tool.ToolModel.Description, toolModelDescription.Text);
            var status = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.Status);
            Assert.AreEqual(tool.Status, status.Text);
            var accessory = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.Accessory);
            Assert.AreEqual(tool.Accessory, accessory.Text);
            var costCenter = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.CostCenter);
            Assert.AreEqual(tool.CostCenter, costCenter.Text);
            var configurableField = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.ConfigurableField);
            Assert.AreEqual(tool.ConfigurableField, configurableField.Text);

            //TODO entkommentieren wenn Kommentare angezeigt werden
            /*var comment = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.Comment);
            Assert.AreEqual(cspTool.Comment, comment.Text);*/

            var manufacturer = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.ModelManufacturer);
            Assert.AreEqual(tool.ToolModel.Manufacturer, manufacturer.Text);
            var toolModelType = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.ModelToolType);
            Assert.AreEqual(tool.ToolModel.ToolModelType, toolModelType.Text);
            var constructionType = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.ModelConstructionType);
            Assert.AreEqual(tool.ToolModel.ConstructionType, constructionType.Text);
            var switchOff = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.ModelSwitchOff);
            Assert.AreEqual(tool.ToolModel.SwitchOff, switchOff.Text);
            var shutOff = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.ModelShutOff);
            Assert.AreEqual(tool.ToolModel.ShutOff, shutOff.Text);
            var driveSize = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.ModelDriveSize);
            Assert.AreEqual(tool.ToolModel.DriveSize, driveSize.Text);
            var driveType = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.ModelDriveType);
            Assert.AreEqual(tool.ToolModel.DriveType, driveType.Text);
            var lowerPowerLimit = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.ModelLowerPowerLimit);
            Assert.AreEqual(tool.ToolModel.MinPow.ToString(numberFormatThreeDecimals, currentCulture), lowerPowerLimit.Text);
            var upperPowerLimit = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.ModelUpperPowerLimit);
            Assert.AreEqual(tool.ToolModel.MaxPow.ToString(numberFormatThreeDecimals, currentCulture), upperPowerLimit.Text);
            //Feld für Luftdruck ist nur bei "Pulse drivern", "Pulse driver Shut Off" und "General" vorhanden
            var airPressure = TestHelper.TryFindElementBy(AiStringHelper.MpToolAllocation.ToolParamTabElements.ModelAirPressure, toolParamTabScrollViewer);
            if (tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriver ||
                tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriverShutOff ||
                tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General)
            {
                Assert.AreEqual(tool.ToolModel.AirPressure.ToString(numberFormatThreeDecimals, currentCulture), airPressure.Text);
            }
            else
            {
                Assert.IsNull(airPressure);
            }
            var weight = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.ModelWeight);
            Assert.AreEqual(tool.ToolModel.Weight.ToString(numberFormatThreeDecimals, currentCulture), weight.Text);
            //Feld für maximale Umdrehungen ist nur bei "Pulse drivern", "Pulse driver Shut Off", "General" und EC Drivern vorhanden
            var maxRotSpeed = TestHelper.TryFindElementBy(AiStringHelper.MpToolAllocation.ToolParamTabElements.ModelMaxRotationSpeed, toolParamTabScrollViewer);
            if (tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriver ||
                tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriverShutOff ||
                tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General ||
                tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.EcDriver)
            {
                Assert.AreEqual(tool.ToolModel.MaxRotSpeed.ToString(numberFormatThreeDecimals, currentCulture), maxRotSpeed.Text);
            }
            else
            {
                Assert.IsNull(maxRotSpeed);
            }
            //Feld für Batteriespannung ist nur bei "General" und "Ec Drivern" vorhanden
            var batteryVoltage = TestHelper.TryFindElementBy(AiStringHelper.MpToolAllocation.ToolParamTabElements.ModelBatteryVoltage, toolParamTabScrollViewer);
            if (tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General ||
                tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.EcDriver)
            {
                Assert.AreEqual(tool.ToolModel.BattVoltage.ToString(numberFormatThreeDecimals, currentCulture), batteryVoltage.Text);
            }
            else
            {
                Assert.IsNull(batteryVoltage);
            }

            //Feld für Luftverbrauch ist nur bei "Pulse drivern", "Pulse driver Shut Off" und "General" vorhanden
            var airConsumption = TestHelper.TryFindElementBy(AiStringHelper.MpToolAllocation.ToolParamTabElements.ModelAirConsumption, toolParamTabScrollViewer);
            if (tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriver ||
                tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriverShutOff ||
                tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General)
            {
                Assert.AreEqual(tool.ToolModel.AirConsumption.ToString(numberFormatThreeDecimals, currentCulture), airConsumption.Text);
            }
            else
            {
                Assert.IsNull(airConsumption);
            }
        }
        private static void AssertMpToolAllocationMpTab(MeasurementPoint mp, WindowsDriver<WindowsElement> QstSession)
        {
            var mpParamTabScrollViewer = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.MpParamTabElements.ScrollViewer, QstSession);
            var number = mpParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpParamTabElements.Number);
            Assert.AreEqual(mp.Number, number.Text);
            var description = mpParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpParamTabElements.Description);
            Assert.AreEqual(mp.Description, description.Text);
            var controlledByTorque = TestHelper.TryFindElementBy(AiStringHelper.MpToolAllocation.MpParamTabElements.ControlledByTorque, mpParamTabScrollViewer);
            var controlledByAngle = TestHelper.TryFindElementBy(AiStringHelper.MpToolAllocation.MpParamTabElements.ControlledByAngle, mpParamTabScrollViewer);
            if (mp.ControlledBy == ControlledBy.Torque)
            {
                Assert.AreEqual(mp.ControlledBy.ToString(), controlledByTorque.Text);
                Assert.IsNull(controlledByAngle);
            }
            else
            {
                Assert.AreEqual(mp.ControlledBy.ToString(), controlledByAngle.Text);
                Assert.IsNull(controlledByTorque);
            }
            var setPointTorque = mpParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpParamTabElements.SetPointTorque);
            Assert.AreEqual(mp.SetPointTorque.ToString(numberFormatThreeDecimals, currentCulture), setPointTorque.Text);
            var tolClassTorque = mpParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpParamTabElements.ToleranceClassTorque);
            Assert.AreEqual(mp.ToleranceClassTorque.Name, tolClassTorque.Text);
            var minTorque = mpParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpParamTabElements.MinTorque);
            Assert.AreEqual(mp.MinTorque.ToString(numberFormatThreeDecimals, currentCulture), minTorque.Text);
            var maxTorque = mpParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpParamTabElements.MaxTorque);
            Assert.AreEqual(mp.MaxTorque.ToString(numberFormatThreeDecimals, currentCulture), maxTorque.Text);
            var thresholdTorque = mpParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpParamTabElements.ThresholdTorque);
            Assert.AreEqual(mp.ThresholdTorque.ToString(numberFormatThreeDecimals, currentCulture), thresholdTorque.Text);
            var setPointAngle = mpParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpParamTabElements.SetPointAngle);
            Assert.AreEqual(mp.SetPointAngle.ToString(numberFormatThreeDecimals, currentCulture), setPointAngle.Text);
            var toleranceClassAngle = mpParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpParamTabElements.ToleranceClassAngle);
            Assert.AreEqual(mp.ToleranceClassAngle.Name, toleranceClassAngle.Text);
            var minAngle = mpParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpParamTabElements.MinAngle);
            Assert.AreEqual(mp.MinAngle.ToString(numberFormatThreeDecimals, currentCulture), minAngle.Text);
            var maxAngle = mpParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpParamTabElements.MaxAngle);
            Assert.AreEqual(mp.MaxAngle.ToString(numberFormatThreeDecimals, currentCulture), maxAngle.Text);
            var configurableField1 = mpParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpParamTabElements.ConfigurableField1);
            Assert.AreEqual(mp.ConfigurableField, configurableField1.Text);
            var configurableField2 = mpParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpParamTabElements.ConfigurableField2);
            Assert.AreEqual(mp.ConfigurableField2, configurableField2.Text);
            var configurableField3 = mpParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpParamTabElements.ConfigurableField3);
            Assert.IsTrue(mp.ConfigurableField3 == configurableField3.Selected);
            //TODO entkommentieren wenn Kommentare angezeigt werden
            /*var comment = mpParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpParamTabElements.Comment);
            Assert.AreEqual(MP1.Comment, comment.Text);*/
        }
        public static void AssertListAllocatedTools(WindowsDriver<WindowsElement> QstSession, AppiumWebElement parent, MpToolAllocation mpToolAllocation)
        {
            TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.SerialNumber, mpToolAllocation.Tool.SerialNumber);
            TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.InventoryNumber, mpToolAllocation.Tool.InventoryNumber);
            TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.ToolUsage, mpToolAllocation.ToolUsage);
            if (mpToolAllocation.AreConditionsCreated)
            {
                TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.TestLevelNumberChk, mpToolAllocation.ToolControlConditions.TestLevelSetChkNumber.ToString());
                TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.StartDateChk, mpToolAllocation.ToolControlConditions.StartDateChk.ToString("M/d/yyyy", currentCulture));
                TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.TestOperationActiveChk, mpToolAllocation.ToolControlConditions.IsActiveChk.ToString(), true);
                TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.TestLevelNumberMfu, mpToolAllocation.ToolControlConditions.TestLevelSetMcaNumber.ToString());
                TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.StartDateMfu, mpToolAllocation.ToolControlConditions.StartDateMca.ToString("M/d/yyyy", currentCulture));
                TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.TestOperationActiveMfu, mpToolAllocation.ToolControlConditions.IsActiveMca.ToString(), true);
                //TODO entkommentieren sobald Prüfdatenberechnung implementiert
                //TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.NextTestDateMfu, mpToolAllocation.NextTestDateMfu.ToString("?d/?M/yyyy"));
                //TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.NextTestShiftMfu, mpToolAllocation.NextTestShiftMfu);
                //TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.NextTestDateChk, mpToolAllocation.NextTestDateChk.ToString("?d/?M/yyyy"));
                //TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.NextTestShiftChk, mpToolAllocation.NextTestShiftChk);
            }
            else
            {
                TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.TestLevelNumberChk, "0");
                TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.StartDateChk, new DateTime().ToString("d/M/yyyy", currentCulture));
                TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.TestOperationActiveChk, "False", true);
                TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.TestLevelNumberMfu, "0");
                TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.StartDateMfu, new DateTime().ToString("d/M/yyyy", currentCulture));
                TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.TestOperationActiveMfu, "False", true);
                TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.NextTestDateMfu, "");
                TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.NextTestShiftMfu, "");
                TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.NextTestDateChk, "");
                TestHelper.AssertGridRow(QstSession, parent, AiStringHelper.MpToolAllocation.AllocatedToolsGrid, AiStringHelper.MpToolAllocation.AllocatedToolsGridHeaderRow, AiStringHelper.MpToolAllocation.AllocatedToolsGridRowPrefix, mpToolAllocation.GetToolIdentString(), MpToolAllocation.AllocatedToolsListHeaderStrings.NextTestShiftChk, "");
            }
        }
        private static void RemoveMpToolAllocationWithAssert(MpToolAllocation mpToolAllocation, string statusAfterRemoveAllocationString, WindowsDriver<WindowsElement> QstSession)
        {
            bool allocationSelected = SelectMpToolAllocation(mpToolAllocation, QstSession);
            if (allocationSelected)
            {
                var removeMpToolAllocation = QstSession.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.RemoveToolAllocation);
                var allocateTool = QstSession.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.AllocateTool);
                Assert.IsTrue(removeMpToolAllocation.Enabled);
                Assert.IsFalse(allocateTool.Enabled);

                removeMpToolAllocation.Click();

                var confirmBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
                confirmBtn.Click();

                Thread.Sleep(500);
                var assistantSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.Assistant.View, TestConfiguration.GetWindowsApplicationDriverUrl());
                var listInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputList, assistantSession);
                var assistantNextBtn = TestHelper.FindElementWithWait(AiStringHelper.Assistant.Next, assistantSession);

                var statusAfterRemoveAllocation = FindOrCreateHelperListEntryInAssistWindow(statusAfterRemoveAllocationString, assistantSession, listInput);
                statusAfterRemoveAllocation.Click();
                assistantNextBtn.Click();
                mpToolAllocation.Tool.Status = statusAfterRemoveAllocationString;
                TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

                var mpToolAllocationTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.MpToolAllocTab, QstSession);
                mpToolAllocationTab.Click();

                var mpToolAllocation1Mp = mpToolAllocationTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpToolAllocTabElements.MeasurementPoint);
                Assert.AreEqual(mpToolAllocation.Mp.GetMpTreeName(), mpToolAllocation1Mp.Text);
                var mpToolAllocation1Tool = mpToolAllocationTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpToolAllocTabElements.Tool);
                Assert.AreEqual(mpToolAllocation.Tool.GetTreeString(), mpToolAllocation1Tool.Text);

                var mpToolAllocation1ToolUsage = mpToolAllocationTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.MpToolAllocTabElements.ToolUsage);
                Assert.IsFalse(mpToolAllocation1ToolUsage.Enabled);



                //TODO Seitenwechsel entfernen nachdem https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-119 gefixed wurde
                //Info Status ändert sich nicht sofort erst bei Wechsel und Zurückwechseln des wkz
                NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MpToolAllocation);
                SelectMpToolAllocation(mpToolAllocation, QstSession);
                var toolTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.ToolParamTab, QstSession);
                toolTab.Click();



                var toolParamTabScrollViewer = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.ToolParamTabElements.ScrollViewer, QstSession);
                var status = toolParamTabScrollViewer.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.ToolParamTabElements.Status);
                Assert.AreEqual(mpToolAllocation.Tool.Status, status.Text);

                removeMpToolAllocation = QstSession.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.RemoveToolAllocation);
                allocateTool = QstSession.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.AllocateTool);
                var createTestConditions = QstSession.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.CreateTestConditions);
                Assert.IsFalse(removeMpToolAllocation.Enabled);
                Assert.IsTrue(allocateTool.Enabled);
                Assert.IsFalse(createTestConditions.Enabled);
            }
        }
        public static void RemoveToolAllocation(string statusAfterRemoveAllocationString, MpToolAllocation mpToolAllocation, WindowsDriver<WindowsElement> QstSession)
        {
            bool allocationSelected = SelectMpToolAllocation(mpToolAllocation, QstSession);
            if (allocationSelected)
            {
                var removeMpToolAllocation = QstSession.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.RemoveToolAllocation);

                //Falls vorhanden mpToolAllocation löschen
                AppiumWebElement confirmBtn;
                WindowsDriver<WindowsElement> assistantSession;
                WindowsElement listInput;
                AppiumWebElement assistantNextBtn;
                AppiumWebElement statusAfterRemoveAllocation;
                if (removeMpToolAllocation.Enabled)
                {
                    removeMpToolAllocation.Click();

                    confirmBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
                    confirmBtn.Click();

                    Thread.Sleep(500);
                    assistantSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.Assistant.View, TestConfiguration.GetWindowsApplicationDriverUrl());
                    listInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputList, assistantSession);
                    assistantNextBtn = TestHelper.FindElementWithWait(AiStringHelper.Assistant.Next, assistantSession);

                    statusAfterRemoveAllocation = FindOrCreateHelperListEntryInAssistWindow(statusAfterRemoveAllocationString, assistantSession, listInput);
                    statusAfterRemoveAllocation.Click();
                    assistantNextBtn.Click();
                    TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
                }
            }
        }
        private static bool SelectMpToolAllocation(MpToolAllocation mpToolAllocation, WindowsDriver<WindowsElement> QstSession)
        {
            var mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.MpTreeViewRoot, QstSession);
            var mpToolAllocationMpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpToolAllocation.Mp.GetParentListWithMp());
            if (mpToolAllocationMpNode == null)
            {
                return false;
            }
            mpToolAllocationMpNode.Click();

            var allToolsTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.AllToolsTab, QstSession);
            allToolsTab.Click();
            var toolTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.ToolTreeViewRoot, QstSession);
            var mpToolAllocationToolNode = TestHelper.GetNode(QstSession, toolTreeviewRootNode, mpToolAllocation.Tool.GetParentListWithTool());
            if (mpToolAllocationToolNode == null)
            {
                return false;
            }
            mpToolAllocationToolNode.Click();
            return true;
        }
        private static void AssertTestConditionsTab(WindowsDriver<WindowsElement> QstSession, MpToolAllocation mpToolAllocation)
        {
            var mpToolAllocView = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.View, QstSession);
            var testConditionsTab = mpToolAllocView.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTab);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, testConditionsTab);
            testConditionsTab.Click();

            var controlledBy = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.ControlledBy);
            Assert.AreEqual(mpToolAllocation.ToolControlConditions.ControlledBy.ToString(), controlledBy.Text);

            if (mpToolAllocation.ToolControlConditions.ControlledBy == ControlledBy.Torque)
            {
                var setPointTorque = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.SetPointTorque);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.SetPointTorque.ToString(numberFormatThreeDecimals, currentCulture), setPointTorque.Text);

                var toleranceClassTorque = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.ToleranceClassTorque);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.ToleranceClassTorque.Name, toleranceClassTorque.GetAttribute("Name"));

                var minTorque = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.MinTorque);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.MinTorque.ToString(numberFormatThreeDecimals, currentCulture), minTorque.Text);

                var maxTorque = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.MaxTorque);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.MaxTorque.ToString(numberFormatThreeDecimals, currentCulture), maxTorque.Text);
            }
            else
            {
                var thresholdTorque = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.ThresholdTorque);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.ThresholdTorque.ToString(numberFormatThreeDecimals, currentCulture), thresholdTorque.Text);

                var setPointAngle = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.SetPointAngle);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.SetPointAngle.ToString(numberFormatThreeDecimals, currentCulture), setPointAngle.Text);

                var toleranceClassAngle = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.ToleranceClassAngle);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.ToleranceClassAngle.Name, toleranceClassAngle.GetAttribute("Name"));

                var minAngle = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.MinAngle);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.MinAngle.ToString(numberFormatThreeDecimals, currentCulture), minAngle.Text);

                var maxAngle = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.MaxAngle);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.MaxAngle.ToString(numberFormatThreeDecimals, currentCulture), maxAngle.Text);
            }

            var testLevelSetChk = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.TestLevelSetChk);
            Assert.AreEqual(mpToolAllocation.ToolControlConditions.TestLevelSetChk.Name, testLevelSetChk.GetAttribute("Name"));

            var testLevelSetNumberChk = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.TestLevelSetNumberChk);
            Assert.AreEqual(mpToolAllocation.ToolControlConditions.TestLevelSetChkNumber.ToString(), testLevelSetNumberChk.GetAttribute("Name"));

            var startDateChk = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.StartDateChk);
            var startDateChkTextbox = startDateChk.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.DatePickerTextbox);
            Assert.AreEqual(TestHelper.GetDateString(mpToolAllocation.ToolControlConditions.StartDateChk), startDateChkTextbox.Text);

            var testOperationActiveChk = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.TestOperationActiveChk);
            Assert.AreEqual(mpToolAllocation.ToolControlConditions.IsActiveChk, testOperationActiveChk.Selected);

            var testLevelSetMca = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.TestLevelSetMca);
            Assert.AreEqual(mpToolAllocation.ToolControlConditions.TestLevelSetMca.Name, testLevelSetMca.GetAttribute("Name"));

            var testLevelSetNumberMca = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.TestLevelSetNumberMca);
            Assert.AreEqual(mpToolAllocation.ToolControlConditions.TestLevelSetMcaNumber.ToString(), testLevelSetNumberMca.GetAttribute("Name"));

            var startDateMca = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.StartDateMca);
            var startDateMcaTextbox = startDateMca.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.DatePickerTextbox);
            Assert.AreEqual(TestHelper.GetDateString(mpToolAllocation.ToolControlConditions.StartDateMca), startDateMcaTextbox.Text);

            var testOperationActiveMca = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.TestOperationActiveMca);
            Assert.AreEqual(mpToolAllocation.ToolControlConditions.IsActiveMca, testOperationActiveMca.Selected);

            if (mpToolAllocation.Tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.ClickWrench)
            {
                var clickWrenchEndCycleTime = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.ClickWrenchEndCycleTime);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.ClickWrenchEndCycleTime.ToString(numberFormatThreeDecimals, currentCulture), clickWrenchEndCycleTime.Text);

                var clickWrenchFilterFrequency = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.ClickWrenchFilterFrequency);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.ClickWrenchFilterFrequency.ToString(numberFormatThreeDecimals, currentCulture), clickWrenchFilterFrequency.Text);

                var clickWrenchCycleComplete = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.ClickWrenchCycleComplete);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.ClickWrenchCycleComplete.ToString(numberFormatThreeDecimals, currentCulture), clickWrenchCycleComplete.Text);

                var clickWrenchMeasureDelayTime = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.ClickWrenchMeasureDelayTime);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.ClickWrenchMeasureDelayTime.ToString(numberFormatThreeDecimals, currentCulture), clickWrenchMeasureDelayTime.Text);

                var clickWrenchResetTime = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.ClickWrenchResetTime);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.ClickWrenchResetTime.ToString(numberFormatThreeDecimals, currentCulture), clickWrenchResetTime.Text);

                var clickWrenchCycleStart = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.ClickWrenchCycleStart);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.ClickWrenchCycleStart.ToString(numberFormatThreeDecimals, currentCulture), clickWrenchCycleStart.Text);

                var clickWrenchSlipTorque = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.ClickWrenchSlipTorque);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.ClickWrenchSlipTorque.ToString(numberFormatThreeDecimals, currentCulture), clickWrenchSlipTorque.Text);
            }
            if (mpToolAllocation.Tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriver
                || mpToolAllocation.Tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriverShutOff)
            {
                var pulseDriverEndCycleTime = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PulseDriverEndCycleTime);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PulseDriverEndCycleTime.ToString(numberFormatThreeDecimals, currentCulture), pulseDriverEndCycleTime.Text);

                var pulseDriverFilterFrequency = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PulseDriverFilterFrequency);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PulseDriverFilterFrequency.ToString(numberFormatThreeDecimals, currentCulture), pulseDriverFilterFrequency.Text);

                var pulseDriverTorqueCoefficient = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PulseDriverTorqueCoefficient);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PulseDriverTorqueCoefficient.ToString(numberFormatThreeDecimals, currentCulture), pulseDriverTorqueCoefficient.Text);

                var pulseDriverMinimumPulse = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PulseDriverMinimumPulse);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PulseDriverMinimumPulse.ToString(), pulseDriverMinimumPulse.Text);

                var pulseDriverMaximumPulse = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PulseDriverMaximumPulse);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PulseDriverMaximumPulse.ToString(), pulseDriverMaximumPulse.Text);

                var pulseDriverThreshold = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PulseDriverThreshold);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PulseDriverThreshold.ToString(), pulseDriverThreshold.Text);
            }
            if (mpToolAllocation.Tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.EcDriver
                || mpToolAllocation.Tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General)
            {
                var powToolEndCycleTime = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PowToolEndCycleTime);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PowEndCycleTime.ToString(numberFormatThreeDecimals, currentCulture), powToolEndCycleTime.Text);

                var powToolFilterFrequency = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PowToolFilterFrequency);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PowFilterFrequency.ToString(numberFormatThreeDecimals, currentCulture), powToolFilterFrequency.Text);

                var powToolCycleComplete = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PowToolCycleComplete);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PowCycleComplete.ToString(numberFormatThreeDecimals, currentCulture), powToolCycleComplete.Text);

                var powToolMeasureDelayTime = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PowToolMeasureDelayTime);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PowMeasureDelayTime.ToString(numberFormatThreeDecimals, currentCulture), powToolMeasureDelayTime.Text);

                var powToolResetTime = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PowToolResetTime);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PowResetTime.ToString(numberFormatThreeDecimals, currentCulture), powToolResetTime.Text);

                var powToolMustTorqueAngleBeInLimits = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PowToolMustTorqueAngleBeInLimits);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PowMustTorqueAndAngleBeBeetweenLimits, powToolMustTorqueAngleBeInLimits.Selected);

                var powToolCycleStart = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PowToolCycleStart);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PowCycleStart.ToString(numberFormatThreeDecimals, currentCulture), powToolCycleStart.Text);

                var powToolStartFinalAngle = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PowToolStartFinalAngle);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PowStartFinalAngle.ToString(numberFormatThreeDecimals, currentCulture), powToolStartFinalAngle.Text);
            }
            if (mpToolAllocation.Tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.MdWrench
                || mpToolAllocation.Tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.ProductionWrench)
            {
                var peakEndCycleTime = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PeakEndCycleTime);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PeakEndCycleTime.ToString(numberFormatThreeDecimals, currentCulture), peakEndCycleTime.Text);

                var peakFilterFrequency = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PeakFilterFrequency);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PeakFilterFrequency.ToString(numberFormatThreeDecimals, currentCulture), peakFilterFrequency.Text);

                var peakMustTorqueAngleBeInLimits = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PeakMustTorqueAngleBeInLimits);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PeakMustTorqueAndAngleBeBetweenLimits, peakMustTorqueAngleBeInLimits.Selected);

                var peakCycleStart = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PeakCycleStart);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PeakCycleStart.ToString(numberFormatThreeDecimals, currentCulture), peakCycleStart.Text);

                var peakStartFinalAngle = testConditionsTab.FindElementByAccessibilityId(AiStringHelper.MpToolAllocation.TestConditionsTabElements.PeakStartFinalAngle);
                Assert.AreEqual(mpToolAllocation.ToolControlConditions.PeakStartFinalAngle.ToString(numberFormatThreeDecimals, currentCulture), peakStartFinalAngle.Text);
            }
        }
        private static void CreateTestConditions(MpToolAllocation mpToolAllocation)
        {
            var assistantSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.Assistant.View, TestConfiguration.GetWindowsApplicationDriverUrl());
            var listInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputList, assistantSession);
            var listEntry = TestHelper.FindElementInListbox(mpToolAllocation.ToolControlConditions.ControlledBy.ToString(), listInput);
            listEntry.Click();
            var assistantNextBtn = TestHelper.FindElementWithWait(AiStringHelper.Assistant.Next, assistantSession);
            AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.ControlledBy.ToString(), AssistantStringHelper.MpToolAllocation.ControlledBy);
            assistantNextBtn.Click();

            var floatingInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputFloatingPoint, assistantSession);
            if (mpToolAllocation.ToolControlConditions.ControlledBy == ControlledBy.Torque)
            {
                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.SetPointTorque.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.SetPointTorque.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.SetpointTorque, AssistantStringHelper.UnitStrings.Nm);
                assistantNextBtn.Click();

                listInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputList, assistantSession);
                var tolClass = FindOrCreateToleranceClass(mpToolAllocation.ToolControlConditions.ToleranceClassTorque, assistantSession, listInput);
                tolClass.Click();
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.ToleranceClassTorque.Name, AssistantStringHelper.MpToolAllocation.ToleranceClassTorque);
                assistantNextBtn.Click();

                if (mpToolAllocation.ToolControlConditions.ToleranceClassTorque.Name == "freie Eingabe")
                {
                    floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.MinTorque.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.MinTorque.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.MininumTorque, AssistantStringHelper.UnitStrings.Nm);
                    assistantNextBtn.Click();

                    floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.MaxTorque.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.MaxTorque.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.MaximumTorque, AssistantStringHelper.UnitStrings.Nm);
                    assistantNextBtn.Click();
                }
            }
            else
            {
                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.ThresholdTorque.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.ThresholdTorque.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.ThresholdTorque, AssistantStringHelper.UnitStrings.Nm);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.SetPointAngle.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.SetPointAngle.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.SetpointAngle, AssistantStringHelper.UnitStrings.Deg);
                assistantNextBtn.Click();

                listInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputList, assistantSession);
                var tolClass = FindOrCreateToleranceClass(mpToolAllocation.ToolControlConditions.ToleranceClassAngle, assistantSession, listInput);
                tolClass.Click();
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.ToleranceClassAngle.Name, AssistantStringHelper.MpToolAllocation.ToleranceClassAngle);
                assistantNextBtn.Click();

                if (mpToolAllocation.ToolControlConditions.ToleranceClassAngle.Name == "freie Eingabe")
                {
                    floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.MinAngle.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.MinAngle.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.MininumAngle, AssistantStringHelper.UnitStrings.Deg);
                    assistantNextBtn.Click();

                    floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.MaxAngle.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.MaxAngle.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.MaximumAngle, AssistantStringHelper.UnitStrings.Deg);
                    assistantNextBtn.Click();
                }
            }

            listInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputList, assistantSession);
            listEntry = TestHelper.FindElementInListbox(mpToolAllocation.ToolControlConditions.TestLevelSetChk.Name, listInput);
            listEntry.Click();
            AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.TestLevelSetChk.Name, AssistantStringHelper.MpToolAllocation.TestLevelSetChk);
            assistantNextBtn.Click();

            listInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputList, assistantSession);
            listEntry = TestHelper.FindElementInListbox(mpToolAllocation.ToolControlConditions.TestLevelSetChkNumber.ToString(), listInput);
            listEntry.Click();
            AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.TestLevelSetChkNumber.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.TestLevelChk);
            assistantNextBtn.Click();

            var dateInput = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.InputDate);
            dateInput.Clear();
            TestHelper.SendDate(mpToolAllocation.ToolControlConditions.StartDateChk, dateInput);
            AssertAssistantListEntry(assistantSession, TestHelper.GetDateString(mpToolAllocation.ToolControlConditions.StartDateChk), AssistantStringHelper.MpToolAllocation.StartDateChk);
            assistantNextBtn.Click();

            var boolInput = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.InputBoolean);
            TestHelper.SetCheckbox(boolInput, mpToolAllocation.ToolControlConditions.IsActiveChk);
            AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.IsActiveChk.ToString(), AssistantStringHelper.MpToolAllocation.TestModeActiveChk, "", true);
            assistantNextBtn.Click();

            listInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputList, assistantSession);
            listEntry = TestHelper.FindElementInListbox(mpToolAllocation.ToolControlConditions.TestLevelSetMca.Name, listInput);
            listEntry.Click();
            AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.TestLevelSetMca.Name, AssistantStringHelper.MpToolAllocation.TestLevelSetMca);
            assistantNextBtn.Click();

            listInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputList, assistantSession);
            listEntry = TestHelper.FindElementInListbox(mpToolAllocation.ToolControlConditions.TestLevelSetMcaNumber.ToString(), listInput);
            listEntry.Click();
            AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.TestLevelSetMcaNumber.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.TestLevelMca);
            assistantNextBtn.Click();

            dateInput = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.InputDate);
            dateInput.Clear();
            TestHelper.SendDate(mpToolAllocation.ToolControlConditions.StartDateMca, dateInput);
            AssertAssistantListEntry(assistantSession, TestHelper.GetDateString(mpToolAllocation.ToolControlConditions.StartDateMca), AssistantStringHelper.MpToolAllocation.StartDateMca);
            assistantNextBtn.Click();

            boolInput = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.InputBoolean);
            TestHelper.SetCheckbox(boolInput, mpToolAllocation.ToolControlConditions.IsActiveMca);
            AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.IsActiveMca.ToString(), AssistantStringHelper.MpToolAllocation.TestModeActiveMCA, "", true);
            assistantNextBtn.Click();

            if (mpToolAllocation.Tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.ClickWrench)
            {
                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.ClickWrenchEndCycleTime.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.ClickWrenchEndCycleTime.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.EndCycleTime, AssistantStringHelper.UnitStrings.Sec);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.ClickWrenchFilterFrequency.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.ClickWrenchFilterFrequency.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.FilterFrequency, AssistantStringHelper.UnitStrings.Hertz);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.ClickWrenchCycleComplete.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.ClickWrenchCycleComplete.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.CycleComplete, AssistantStringHelper.UnitStrings.Nm);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.ClickWrenchMeasureDelayTime.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.ClickWrenchMeasureDelayTime.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.MeasureDelay, AssistantStringHelper.UnitStrings.Sec);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.ClickWrenchResetTime.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.ClickWrenchResetTime.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.ResetTime, AssistantStringHelper.UnitStrings.Sec);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.ClickWrenchCycleStart.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.ClickWrenchCycleStart.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.CycleStart, AssistantStringHelper.UnitStrings.Nm);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.ClickWrenchSlipTorque.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.ClickWrenchSlipTorque.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.SlipTorque, AssistantStringHelper.UnitStrings.Nm);
                assistantNextBtn.Click();
            }
            if (mpToolAllocation.Tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriver
                || mpToolAllocation.Tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.PulseDriverShutOff)
            {
                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.PulseDriverEndCycleTime.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PulseDriverEndCycleTime.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.EndCycleTime, AssistantStringHelper.UnitStrings.Sec);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.PulseDriverFilterFrequency.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PulseDriverFilterFrequency.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.FilterFrequency, AssistantStringHelper.UnitStrings.Hertz);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.PulseDriverTorqueCoefficient.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PulseDriverTorqueCoefficient.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.TorqueCoefficient);
                assistantNextBtn.Click();

                var inputInteger = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.InputInteger);
                inputInteger.SendKeys(mpToolAllocation.ToolControlConditions.PulseDriverMinimumPulse.ToString());
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PulseDriverMinimumPulse.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.MinimumPulse);
                assistantNextBtn.Click();

                inputInteger.SendKeys(mpToolAllocation.ToolControlConditions.PulseDriverMaximumPulse.ToString());
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PulseDriverMaximumPulse.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.MaximumPulse);
                assistantNextBtn.Click();

                inputInteger.SendKeys(mpToolAllocation.ToolControlConditions.PulseDriverThreshold.ToString());
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PulseDriverThreshold.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.Threshold);
                assistantNextBtn.Click();
            }
            if (mpToolAllocation.Tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.EcDriver
                || mpToolAllocation.Tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.General)
            {
                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.PowEndCycleTime.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PowEndCycleTime.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.EndCycleTime, AssistantStringHelper.UnitStrings.Sec);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.PowFilterFrequency.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PowFilterFrequency.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.FilterFrequency, AssistantStringHelper.UnitStrings.Hertz);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.PowCycleComplete.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PowCycleComplete.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.CycleComplete, AssistantStringHelper.UnitStrings.Nm);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.PowMeasureDelayTime.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PowMeasureDelayTime.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.MeasureDelay, AssistantStringHelper.UnitStrings.Sec);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.PowResetTime.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PowResetTime.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.ResetTime, AssistantStringHelper.UnitStrings.Sec);
                assistantNextBtn.Click();

                TestHelper.SetCheckbox(boolInput, mpToolAllocation.ToolControlConditions.PowMustTorqueAndAngleBeBeetweenLimits);
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PowMustTorqueAndAngleBeBeetweenLimits.ToString(), AssistantStringHelper.MpToolAllocation.TorqueAndAngleBetweenLimits, "", true);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.PowCycleStart.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PowCycleStart.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.CycleStart, AssistantStringHelper.UnitStrings.Nm);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.PowStartFinalAngle.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PowStartFinalAngle.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.StartFinalAngle, AssistantStringHelper.UnitStrings.Nm);
                assistantNextBtn.Click();
            }
            if (mpToolAllocation.Tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.MdWrench
                || mpToolAllocation.Tool.ToolModel.ToolModelType == ToolModel.ToolModelTypeStrings.ProductionWrench)
            {
                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.PeakEndCycleTime.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PeakEndCycleTime.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.EndCycleTime, AssistantStringHelper.UnitStrings.Sec);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.PeakFilterFrequency.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PeakFilterFrequency.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.FilterFrequency, AssistantStringHelper.UnitStrings.Hertz);
                assistantNextBtn.Click();

                TestHelper.SetCheckbox(boolInput, mpToolAllocation.ToolControlConditions.PeakMustTorqueAndAngleBeBetweenLimits);
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PeakMustTorqueAndAngleBeBetweenLimits.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.TorqueAndAngleBetweenLimits, "", true);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.PeakCycleStart.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PeakCycleStart.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.CycleStart, AssistantStringHelper.UnitStrings.Nm);
                assistantNextBtn.Click();

                floatingInput.SendKeys(mpToolAllocation.ToolControlConditions.PeakStartFinalAngle.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mpToolAllocation.ToolControlConditions.PeakStartFinalAngle.ToString(currentCulture), AssistantStringHelper.MpToolAllocation.StartFinalAngle, AssistantStringHelper.UnitStrings.Nm);
                assistantNextBtn.Click();
            }
            mpToolAllocation.AreConditionsCreated = true;
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ToolTestsCalcSuccess);
        }

        public static void ClickToolsRoot()
        {
            var allToolsTab = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.AllToolsTab, QstSession);
            allToolsTab.Click();
            var toolTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.ToolTreeViewRoot, QstSession);
            var rootNode = TestHelper.GetNode(QstSession, toolTreeviewRootNode, new List<string> { "Tools" });
            if (rootNode != null)
            {
                var rootTextBlock = rootNode.FindElementByXPath("*/Text[@ClassName=\"TextBlock\"]");
                rootTextBlock.Click();
            }
        }
    }
}