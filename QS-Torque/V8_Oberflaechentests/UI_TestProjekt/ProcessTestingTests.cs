using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System.Globalization;

using UI_TestProjekt.Helper;
using UI_TestProjekt.TestModel;
using static UI_TestProjekt.MeasurementpointTests;
using static UI_TestProjekt.TestPlanningMasterDataTests;

namespace UI_TestProjekt
{
    [TestClass]
    public class ProcessTestingTests : TestBase
    {
        [TestMethod]
        [TestCategory("MasterData")]
        [Ignore] //TODO Fertig implementieren wenn Verlängerungen (Extensions)fertig sind
        public void TestProcessControl()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            ProcessControlConditions pc1 = Testdata.GetProcessControlMinTorque();
            ProcessControlConditions pc2 = Testdata.GetProcessControlPeak();
            ProcessControlConditions pc3 = Testdata.GetProcessControlPrevail();
            ProcessControlConditions pc1Changed = Testdata.GetProcessControlMinTorqueChanged();
            ProcessControlConditions pc2Changed = Testdata.GetProcessControlPeakChanged();
            ProcessControlConditions pc3Changed = Testdata.GetProcessControlPrevailChanged();

            var testLevelSetXTimes = Testdata.GetTestLevelSetXTimesProcessControl();
            var testLevelSetEveryX = Testdata.GetTestLevelSetEveryXProcessControl();

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ProcessControl);
            DeleteProcessControl(QstSession, pc1);
            DeleteProcessControl(QstSession, pc2);
            DeleteProcessControl(QstSession, pc3);

            //Auf Mpseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Mps löschen
            var mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            var btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMp(QstSession, pc1.Mp, btnDelete);
            DeleteMp(QstSession, pc2.Mp, btnDelete);
            DeleteMp(QstSession, pc3.Mp, btnDelete);

            //TestLevelSets löschen
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestPlanningMasterData);
            DeleteTestLevelSet(QstSession, testLevelSetXTimes);
            DeleteTestLevelSet(QstSession, testLevelSetEveryX);

            //TestLevelSets anlegen
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestPlanningMasterData);
            CreateTestLevelSet(QstSession, testLevelSetXTimes);
            CreateTestLevelSet(QstSession, testLevelSetEveryX);

            //Auf Mpseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Create Mp
            CreateMp(QstSession, pc1.Mp);
            CreateMp(QstSession, pc2.Mp);
            CreateMp(QstSession, pc3.Mp);

            //Auf ProcessControlSeite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ProcessControl);

            var processView = TestHelper.FindElementWithWait(AiStringHelper.ProcessControl.View, QstSession);
            var treeViewRootNode = TestHelper.FindElementByAiWithWaitFromParent(processView, AiStringHelper.ProcessControl.TreeViewRootNode, QstSession);

            var pc1MpNode = TestHelper.GetNode(QstSession, treeViewRootNode, pc1.Mp.GetParentListWithMp());
            pc1MpNode.Click();

            CreateProcessControl(QstSession, pc1);
            AssertProcessControl(QstSession, pc1);

            var pc2MpNode = TestHelper.GetNode(QstSession, treeViewRootNode, pc2.Mp.GetParentListWithMp());
            pc2MpNode.Click();
            //TOOD Entfernen wenn gefixed 2x Click bis sich die Buttons richtig aktualisieren
            pc2MpNode.Click();
            CreateProcessControl(QstSession, pc2);
            AssertProcessControl(QstSession, pc2);

            var pc3MpNode = TestHelper.GetNode(QstSession, treeViewRootNode, pc3.Mp.GetParentListWithMp());
            pc3MpNode.Click();
            //TOOD Entfernen wenn gefixed 2x Click bis sich die Buttons richtig aktualisieren
            pc3MpNode.Click();
            CreateProcessControl(QstSession, pc3);
            AssertProcessControl(QstSession, pc3);

            UpdateProcessControl(QstSession, pc1, pc1Changed);
            var save = QstSession.FindElementByAccessibilityId(AiStringHelper.ProcessControl.Save);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, save);
            save.Click();
            var viewVerifyChanges = DesktSession.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.View);
            var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyPcChangesInVerifyView(QstSession, pc1, pc1Changed, listViewChanges, 11);

            var textBoxComment = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Comment);
            TestHelper.SendKeysConverted(textBoxComment, "Changecomment erste Prüfbedingung!");

            var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            btnApply.Click();

            AssertProcessControl(QstSession, pc1Changed);

            UpdateProcessControl(QstSession, pc2, pc2Changed);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, save);
            save.Click();
            viewVerifyChanges = DesktSession.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.View);
            listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyPcChangesInVerifyView(QstSession, pc2, pc2Changed, listViewChanges, 11);

            textBoxComment = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Comment);
            TestHelper.SendKeysConverted(textBoxComment, "Changecomment zweite Prüfbedingung!");

            btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            btnApply.Click();

            AssertProcessControl(QstSession, pc2Changed);

            UpdateProcessControl(QstSession, pc3, pc3Changed);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, save);
            save.Click();
            viewVerifyChanges = DesktSession.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.View);
            listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyPcChangesInVerifyView(QstSession, pc3, pc3Changed, listViewChanges, 7);

            textBoxComment = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Comment);
            TestHelper.SendKeysConverted(textBoxComment, "Changecomment dritte Prüfbedingung!");

            btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            btnApply.Click();

            AssertProcessControl(QstSession, pc3Changed);

            DeleteProcessControl(QstSession, pc1);
            DeleteProcessControl(QstSession, pc2);
            DeleteProcessControl(QstSession, pc3);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ProcessControl);

            processView = TestHelper.FindElementWithWait(AiStringHelper.ProcessControl.View, QstSession);
            treeViewRootNode = TestHelper.FindElementByAiWithWaitFromParent(processView, AiStringHelper.ProcessControl.TreeViewRootNode, QstSession);
            var pc1ChangedNode = TestHelper.GetNode(QstSession, treeViewRootNode, pc1Changed.Mp.GetParentListWithMp());
            pc1ChangedNode.Click();
            //TOOD Entfernen wenn gefixed 2x Click bis sich die Buttons richtig aktualisieren
            pc1ChangedNode.Click();
            var delete = processView.FindElementByAccessibilityId(AiStringHelper.ProcessControl.Delete);
            var createConditions = processView.FindElementByAccessibilityId(AiStringHelper.ProcessControl.Add);
            Assert.IsTrue(createConditions.Enabled);
            Assert.IsFalse(delete.Enabled);

            var pc2ChangedNode = TestHelper.GetNode(QstSession, treeViewRootNode, pc2Changed.Mp.GetParentListWithMp());
            pc2ChangedNode.Click();
            //TOOD Entfernen wenn gefixed 2x Click bis sich die Buttons richtig aktualisieren
            pc2ChangedNode.Click();
            delete = processView.FindElementByAccessibilityId(AiStringHelper.ProcessControl.Delete);
            createConditions = processView.FindElementByAccessibilityId(AiStringHelper.ProcessControl.Add);
            Assert.IsTrue(createConditions.Enabled);
            Assert.IsFalse(delete.Enabled);

            var pc3ChangedNode = TestHelper.GetNode(QstSession, treeViewRootNode, pc3Changed.Mp.GetParentListWithMp());
            pc3ChangedNode.Click();
            //TOOD Entfernen wenn gefixed 2x Click bis sich die Buttons richtig aktualisieren
            pc3ChangedNode.Click();
            delete = processView.FindElementByAccessibilityId(AiStringHelper.ProcessControl.Delete);
            createConditions = processView.FindElementByAccessibilityId(AiStringHelper.ProcessControl.Add);
            Assert.IsTrue(createConditions.Enabled);
            Assert.IsFalse(delete.Enabled);

            //Mps löschen
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);
            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMp(QstSession, pc1.Mp, btnDelete);
            DeleteMp(QstSession, pc2.Mp, btnDelete);
            DeleteMp(QstSession, pc3.Mp, btnDelete);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);
            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMpFolder(QstSession, pc1.Mp.ListParentFolder, btnDelete);

            //TestLevelSets löschen
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.TestPlanningMasterData);
            DeleteTestLevelSet(QstSession, testLevelSetXTimes);
            DeleteTestLevelSet(QstSession, testLevelSetEveryX);
        }

        private static void AssertProcessControl(WindowsDriver<WindowsElement> QstSession, ProcessControlConditions pc)
        {
            AssertProcessControlProcessTab(QstSession, pc);
            AssertProcessControlMeasurementTab(QstSession, pc);
        }
        private static void AssertProcessControlMeasurementTab(WindowsDriver<WindowsElement> QstSession, ProcessControlConditions pc)
        {
            var processView = TestHelper.FindElementWithWait(AiStringHelper.ProcessControl.View, QstSession);
            var locationParameterTab = processView.FindElementByAccessibilityId(AiStringHelper.ProcessControl.LocationParameterTab);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, locationParameterTab);
            locationParameterTab.Click();

            var number = locationParameterTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.LocationParameterTabElements.Number);
            Assert.AreEqual(pc.Mp.Number, number.Text);

            var description = locationParameterTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.LocationParameterTabElements.Description);
            Assert.AreEqual(pc.Mp.Description, description.Text);

            var controlledBy = locationParameterTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.LocationParameterTabElements.ControlledBy);
            Assert.AreEqual(pc.Mp.ControlledBy.ToString(), controlledBy.Text);

            var setPointTorque = locationParameterTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.LocationParameterTabElements.SetPointTorque);
            Assert.AreEqual(pc.Mp.SetPointTorque.ToString(numberFormatThreeDecimals, currentCulture), setPointTorque.Text);

            var toleranceClassTorque = locationParameterTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.LocationParameterTabElements.ToleranceClassTorque);
            Assert.AreEqual(pc.Mp.ToleranceClassTorque.Name, toleranceClassTorque.Text);

            var minimumTorque = locationParameterTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.LocationParameterTabElements.MinimumTorque);
            Assert.AreEqual(pc.Mp.MinTorque.ToString(numberFormatThreeDecimals, currentCulture), minimumTorque.Text);

            var maxiumumTorque = locationParameterTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.LocationParameterTabElements.MaximumTorque);
            Assert.AreEqual(pc.Mp.MaxTorque.ToString(numberFormatThreeDecimals, currentCulture), maxiumumTorque.Text);

            var thresholdTorque = locationParameterTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.LocationParameterTabElements.ThresholdTorque);
            Assert.AreEqual(pc.Mp.ThresholdTorque.ToString(numberFormatThreeDecimals, currentCulture), thresholdTorque.Text);

            var setPointAngle = locationParameterTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.LocationParameterTabElements.SetPointAngle);
            Assert.AreEqual(pc.Mp.SetPointAngle.ToString(numberFormatThreeDecimals, currentCulture), setPointAngle.Text);

            var toleranceClassAngle = locationParameterTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.LocationParameterTabElements.ToleranceClassAngle);
            Assert.AreEqual(pc.Mp.ToleranceClassAngle.Name, toleranceClassAngle.Text);

            var minimumAngle = locationParameterTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.LocationParameterTabElements.MinimumAngle);
            Assert.AreEqual(pc.Mp.MinAngle.ToString(numberFormatThreeDecimals, currentCulture), minimumAngle.Text);

            var maximumAngle = locationParameterTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.LocationParameterTabElements.MaximumAngle);
            Assert.AreEqual(pc.Mp.MaxAngle.ToString(numberFormatThreeDecimals, currentCulture), maximumAngle.Text);

            var configurableField1 = locationParameterTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.LocationParameterTabElements.ConfigurableField1);
            Assert.AreEqual(pc.Mp.ConfigurableField, configurableField1.Text);

            var configurableField2 = locationParameterTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.LocationParameterTabElements.ConfigurableField2);
            Assert.AreEqual(pc.Mp.ConfigurableField2, configurableField2.Text);

            var configurableField3 = locationParameterTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.LocationParameterTabElements.AConfigurableField3);
            Assert.AreEqual(pc.Mp.ConfigurableField3, configurableField3.Selected);

            //Einkommentieren, wenn Kommentare implementiert sind
            //var comment = locationParameterTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.LocationParameterTabElements.Comment);
            //Assert.AreEqual(pc.Mp.Comment, comment.Text);
        }
        private static void AssertProcessControlProcessTab(WindowsDriver<WindowsElement> QstSession, ProcessControlConditions pc1)
        {
            var processView = TestHelper.FindElementWithWait(AiStringHelper.ProcessControl.View, QstSession);
            var processControlConditionsTab = processView.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTab);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, processControlConditionsTab);
            processControlConditionsTab.Click();

            var lowerInterventionLimit = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.LowerInterventionLimit);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, lowerInterventionLimit);
            Assert.AreEqual(pc1.LowerInterventionLimit.ToString(numberFormatThreeDecimals, currentCulture), lowerInterventionLimit.Text);

            var upperInterventionLimit = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.UpperInterventionLimit);
            Assert.AreEqual(pc1.UpperInterventionLimit.ToString(numberFormatThreeDecimals, currentCulture), upperInterventionLimit.Text);

            var lowerMeasuringLimit = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.LowerMeasuringLimit);
            Assert.AreEqual(pc1.LowerMeasuringLimit.ToString(numberFormatThreeDecimals, currentCulture), lowerMeasuringLimit.Text);

            var upperMeasuringLimit = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.UpperMeasuringLimit);
            Assert.AreEqual(pc1.UpperMeasuringLimit.ToString(numberFormatThreeDecimals, currentCulture), upperMeasuringLimit.Text);

            var testLevelSet = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.TestLevelSet);
            Assert.AreEqual(pc1.TestLevelSet, testLevelSet.GetAttribute("Name"));

            var testLevelSetNumber = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.TestLevelSetNumber);
            Assert.AreEqual(pc1.TestLevelNumber.ToString(), testLevelSetNumber.GetAttribute("Name"));

            var startDate = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.StartDate);
            var startDateTextbox = startDate.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.DatePickerTextbox);
            Assert.AreEqual(TestHelper.GetDateString(pc1.StartDate), startDateTextbox.Text);

            var isAuditOperationsActive = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.AuditOperationActive);
            Assert.AreEqual(pc1.IsAuditOperationActive, isAuditOperationsActive.Selected);

            var method = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.Method);
            Assert.AreEqual(pc1.GetInternalMethodString(), method.GetAttribute("Name"));

            switch (pc1.Method)
            {
                case ProcessControlConditions.Methods.QSTMinTorque:
                    var qstMinMinimumTorque = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTMin_MinimumTorque);
                    Assert.AreEqual(pc1.QstMinMinimumTorque.ToString(numberFormatThreeDecimals, currentCulture), qstMinMinimumTorque.Text);
                    var qstMinStartAngleCount = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTMin_StartAngleCount);
                    Assert.AreEqual(pc1.QstMinStartAngleCount.ToString(numberFormatThreeDecimals, currentCulture), qstMinStartAngleCount.Text);
                    var qstMinAngleLimit = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTMin_AngleLimit);
                    Assert.AreEqual(pc1.QstMinAngleLimit.ToString(currentCulture), qstMinAngleLimit.Text);

                    var qstMinStartMeasurement = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTMin_StartMeasurement);
                    Assert.AreEqual(pc1.QstMinStartMeasurement.ToString(numberFormatThreeDecimals, currentCulture), qstMinStartMeasurement.Text);
                    var qstMinAlarmLimitTorque = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTMin_AlarmLimitTorque);
                    Assert.AreEqual(pc1.QstMinAlarmLimitTorque.ToString(numberFormatThreeDecimals, currentCulture), qstMinAlarmLimitTorque.Text);
                    var qstMinAlarmLimitAngle = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTMin_AlarmLimitAngle);
                    Assert.AreEqual(pc1.QstMinAlarmLimitAngle.ToString(numberFormatThreeDecimals, currentCulture), qstMinAlarmLimitAngle.Text);

                    break;
                case ProcessControlConditions.Methods.QSTPeak:
                    var qstPeakStartMeasurement = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTPeak_StartMeasurement);
                    Assert.AreEqual(pc1.QstPeakStartMeasurement.ToString(numberFormatThreeDecimals, currentCulture), qstPeakStartMeasurement.Text);
                    break;
                case ProcessControlConditions.Methods.QSTPrevail:
                    var qstPrevailStartAngleCount = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTPrevail_StartAngleCount);
                    Assert.AreEqual(pc1.QstPrevailStartAngleCount.ToString(numberFormatThreeDecimals, currentCulture), qstPrevailStartAngleCount.Text);
                    var qstPrevailAnglForPrevail = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTPrevail_AnglePrevailTorque);
                    Assert.AreEqual(pc1.QstPrevailAngleForPrevail.ToString(numberFormatThreeDecimals, currentCulture), qstPrevailAnglForPrevail.Text);
                    var qstPrevailTargetAngle = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTPrevail_TargetAngle);
                    Assert.AreEqual(pc1.QstPrevailTargetAngle.ToString(numberFormatThreeDecimals, currentCulture), qstPrevailTargetAngle.Text);

                    var qstPrevailStartMeasurement = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTPrevail_StartMeasurement);
                    Assert.AreEqual(pc1.QstPrevailStartMeasurement.ToString(numberFormatThreeDecimals, currentCulture), qstPrevailStartMeasurement.Text);
                    var qstPrevailAlarmLimitTorque = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTPrevail_AlarmLimitTorque);
                    Assert.AreEqual(pc1.QstPrevailAlarmLimitTorque.ToString(numberFormatThreeDecimals, currentCulture), qstPrevailAlarmLimitTorque.Text);
                    var qstPrevailAlarmLimitAngle = processControlConditionsTab.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTPrevail_AlarmLimitAngle);
                    Assert.AreEqual(pc1.QstPrevailAlarmLimitAngle.ToString(numberFormatThreeDecimals, currentCulture), qstPrevailAlarmLimitAngle.Text);
                    break;
                default:
                    Assert.IsTrue(false, "Methode nicht implementiert");
                    break;
            }
        }
        private static void CreateProcessControl(WindowsDriver<WindowsElement> QstSession, ProcessControlConditions pc)
        {
            var processView = TestHelper.FindElementWithWait(AiStringHelper.ProcessControl.View, QstSession);

            var addProcessControl = processView.FindElementByAccessibilityId(AiStringHelper.ProcessControl.Add);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, addProcessControl);
            addProcessControl.Click();

            var assistantSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.Assistant.View, TestConfiguration.GetWindowsApplicationDriverUrl());

            var floatingInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputFloatingPoint, assistantSession);
            floatingInput.SendKeys(pc.LowerMeasuringLimit.ToString(numberFormatThreeDecimals, currentCulture));
            var assistantNextBtn = TestHelper.FindElementWithWait(AiStringHelper.Assistant.Next, assistantSession);
            AssertAssistantListEntry(assistantSession, pc.LowerMeasuringLimit.ToString(currentCulture), AssistantStringHelper.ProcessControl.LowerMeasuringLimit, AssistantStringHelper.UnitStrings.Nm);
            assistantNextBtn.Click();

            floatingInput.SendKeys(pc.UpperMeasuringLimit.ToString(numberFormatThreeDecimals, currentCulture));
            AssertAssistantListEntry(assistantSession, pc.UpperMeasuringLimit.ToString(currentCulture), AssistantStringHelper.ProcessControl.UpperMeasuringLimit, AssistantStringHelper.UnitStrings.Nm);
            assistantNextBtn.Click();

            floatingInput.SendKeys(pc.LowerInterventionLimit.ToString(numberFormatThreeDecimals, currentCulture));
            AssertAssistantListEntry(assistantSession, pc.LowerInterventionLimit.ToString(currentCulture), AssistantStringHelper.ProcessControl.LowerInterventionLimit, AssistantStringHelper.UnitStrings.Nm);
            assistantNextBtn.Click();

            floatingInput.SendKeys(pc.UpperInterventionLimit.ToString(numberFormatThreeDecimals, currentCulture));
            AssertAssistantListEntry(assistantSession, pc.UpperInterventionLimit.ToString(currentCulture), AssistantStringHelper.ProcessControl.UpperInterventionLimit, AssistantStringHelper.UnitStrings.Nm);
            assistantNextBtn.Click();

            var listInput = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.InputList);
            var listEntry = TestHelper.FindElementInListbox(pc.TestLevelSet, listInput);
            listEntry.Click();
            AssertAssistantListEntry(assistantSession, pc.TestLevelSet, AssistantStringHelper.ProcessControl.TestLevelSet);
            assistantNextBtn.Click();

            listEntry = TestHelper.FindElementInListbox(pc.TestLevelNumber.ToString(), listInput);
            listEntry.Click();
            AssertAssistantListEntry(assistantSession, pc.TestLevelNumber.ToString(), AssistantStringHelper.ProcessControl.TestLevel);
            assistantNextBtn.Click();

            var dateInput = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.InputDate);
            TestHelper.SendDate(pc.StartDate, dateInput);
            AssertAssistantListEntry(assistantSession, TestHelper.GetDateString(pc.StartDate), AssistantStringHelper.ProcessControl.StartDate);
            assistantNextBtn.Click();

            var boolInput = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.InputBoolean);
            TestHelper.SetCheckbox(boolInput, pc.IsAuditOperationActive);
            AssertAssistantListEntry(assistantSession, pc.IsAuditOperationActive.ToString(), AssistantStringHelper.ProcessControl.TestModeActive, "", true);
            assistantNextBtn.Click();

            listEntry = TestHelper.FindElementInListbox(pc.Method, listInput);
            listEntry.Click();
            AssertAssistantListEntry(assistantSession, pc.Method, AssistantStringHelper.ProcessControl.TestMethod);
            assistantNextBtn.Click();

            switch (pc.Method)
            {
                case ProcessControlConditions.Methods.QSTMinTorque:
                    floatingInput.SendKeys(pc.QstMinMinimumTorque.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, pc.QstMinMinimumTorque.ToString(currentCulture), AssistantStringHelper.ProcessControl.MinimumTorque, AssistantStringHelper.UnitStrings.Nm);
                    assistantNextBtn.Click();

                    floatingInput.SendKeys(pc.QstMinStartAngleCount.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, pc.QstMinStartAngleCount.ToString(currentCulture), AssistantStringHelper.ProcessControl.StartAngleCount, AssistantStringHelper.UnitStrings.Nm);
                    assistantNextBtn.Click();

                    var integerInput = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.InputInteger);
                    integerInput.SendKeys(pc.QstMinAngleLimit.ToString());
                    AssertAssistantListEntry(assistantSession, pc.QstMinAngleLimit.ToString(currentCulture), AssistantStringHelper.ProcessControl.AngleLimit, AssistantStringHelper.UnitStrings.Deg);
                    assistantNextBtn.Click();

                    floatingInput.SendKeys(pc.QstMinStartMeasurement.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, pc.QstMinStartMeasurement.ToString(currentCulture), AssistantStringHelper.ProcessControl.StartMeasurement, AssistantStringHelper.UnitStrings.Nm);
                    assistantNextBtn.Click();

                    floatingInput.SendKeys(pc.QstMinAlarmLimitTorque.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, pc.QstMinAlarmLimitTorque.ToString(currentCulture), AssistantStringHelper.ProcessControl.AlarmLimitTorque, AssistantStringHelper.UnitStrings.Nm);
                    assistantNextBtn.Click();

                    floatingInput.SendKeys(pc.QstMinAlarmLimitAngle.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, pc.QstMinAlarmLimitAngle.ToString(currentCulture), AssistantStringHelper.ProcessControl.AlarmLimitAngle, AssistantStringHelper.UnitStrings.Deg);
                    assistantNextBtn.Click();
                    break;

                case ProcessControlConditions.Methods.QSTPeak:
                    floatingInput.SendKeys(pc.QstPeakStartMeasurement.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, pc.QstPeakStartMeasurement.ToString(currentCulture), AssistantStringHelper.ProcessControl.StartMeasurement, AssistantStringHelper.UnitStrings.Nm);
                    assistantNextBtn.Click();
                    break;

                case ProcessControlConditions.Methods.QSTPrevail:
                    floatingInput.SendKeys(pc.QstPrevailStartAngleCount.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, pc.QstPrevailStartAngleCount.ToString(currentCulture), AssistantStringHelper.ProcessControl.StartAngleCount, AssistantStringHelper.UnitStrings.Nm);
                    assistantNextBtn.Click();

                    floatingInput.SendKeys(pc.QstPrevailAngleForPrevail.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, pc.QstPrevailAngleForPrevail.ToString(currentCulture), AssistantStringHelper.ProcessControl.AngleForPrevail, AssistantStringHelper.UnitStrings.Deg);
                    assistantNextBtn.Click();

                    floatingInput.SendKeys(pc.QstPrevailTargetAngle.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, pc.QstPrevailTargetAngle.ToString(currentCulture), AssistantStringHelper.ProcessControl.TargetAngle, AssistantStringHelper.UnitStrings.Deg);
                    assistantNextBtn.Click();

                    floatingInput.SendKeys(pc.QstPrevailStartMeasurement.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, pc.QstPrevailStartMeasurement.ToString(currentCulture), AssistantStringHelper.ProcessControl.StartMeasurement, AssistantStringHelper.UnitStrings.Nm);
                    assistantNextBtn.Click();

                    floatingInput.SendKeys(pc.QstPrevailAlarmLimitTorque.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, pc.QstPrevailAlarmLimitTorque.ToString(currentCulture), AssistantStringHelper.ProcessControl.AlarmLimitTorque, AssistantStringHelper.UnitStrings.Nm);
                    assistantNextBtn.Click();

                    floatingInput.SendKeys(pc.QstPrevailAlarmLimitAngle.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, pc.QstPrevailAlarmLimitAngle.ToString(currentCulture), AssistantStringHelper.ProcessControl.AlarmLimitAngle, AssistantStringHelper.UnitStrings.Deg);
                    assistantNextBtn.Click();
                    break;

                default:
                    Assert.IsTrue(false, "Methode nicht implementiert");
                    break;
            }
        }
        private static void DeleteProcessControl(WindowsDriver<WindowsElement> driver, ProcessControlConditions pc)
        {
            var processView = TestHelper.FindElementWithWait(AiStringHelper.ProcessControl.View, driver);
            var deleteBtn = TestHelper.FindElementByAiWithWaitFromParent(processView, AiStringHelper.ProcessControl.Delete, driver);

            var treeViewRootNode = TestHelper.FindElementByAiWithWaitFromParent(processView, AiStringHelper.ProcessControl.TreeViewRootNode, driver);

            var pcNode = TestHelper.GetNode(driver, treeViewRootNode, pc.Mp.GetParentListWithMp());
            if (pcNode != null)
            {
                pcNode.Click();
                //TOOD Entfernen wenn gefixed 2x Click bis sich die Buttons richtig aktualisieren
                pcNode.Click();

                if (TestHelper.WaitForElementToBeEnabledAndDisplayed(driver, deleteBtn))
                {
                    deleteBtn.Click();

                    var confirmBtn = DesktSession.FindElementByXPath(AiStringHelper.GeneralStrings.XPathConfirmButton);
                    confirmBtn.Click();
                }
            }
        }
        private static void UpdateProcessControl(WindowsDriver<WindowsElement> QstSession, ProcessControlConditions pc, ProcessControlConditions pcChanged)
        {
            var pcView = TestHelper.FindElementWithWait(AiStringHelper.ProcessControl.View, QstSession);
            var pcControlTab = pcView.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTab);
            var treeViewRootNode = TestHelper.FindElementByAiWithWaitFromParent(pcView, AiStringHelper.ProcessControl.TreeViewRootNode, QstSession);
            var pcNode = TestHelper.GetNode(QstSession, treeViewRootNode, pc.Mp.GetParentListWithMp());
            pcNode.Click();
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, pcControlTab);
            pcControlTab.Click();

            var scrollviewer = TestHelper.FindElementByAiWithWaitFromParent(pcControlTab, AiStringHelper.ProcessControl.ProcessConditionTabElements.Scrollviewer, QstSession);

            var lowerInterventionLimit = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.LowerInterventionLimit);
            TestHelper.SetFloatingPointTextBox(QstSession, lowerInterventionLimit, pcChanged.LowerInterventionLimit, numberFormatThreeDecimals, currentCulture);

            var upperInterventionLimit = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.UpperInterventionLimit);
            TestHelper.SetFloatingPointTextBox(QstSession, upperInterventionLimit, pcChanged.UpperInterventionLimit, numberFormatThreeDecimals, currentCulture);

            var lowerMeasuringLimit = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.LowerMeasuringLimit);
            TestHelper.SetFloatingPointTextBox(QstSession, lowerMeasuringLimit, pcChanged.LowerMeasuringLimit, numberFormatThreeDecimals, currentCulture);

            var upperMeasuringLimit = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.UpperMeasuringLimit);
            TestHelper.SetFloatingPointTextBox(QstSession, upperMeasuringLimit, pcChanged.UpperMeasuringLimit, numberFormatThreeDecimals, currentCulture);

            var testLevelSet = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.TestLevelSet);
            TestHelper.ClickComboBoxEntry(QstSession, pcChanged.TestLevelSet, testLevelSet);

            var testLevelSetNr = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.TestLevelSetNumber);
            TestHelper.ClickComboBoxEntry(QstSession, pcChanged.TestLevelNumber.ToString(), testLevelSetNr);

            var startDate = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.StartDate);
            TestHelper.SetDatePicker(QstSession, startDate, pcChanged.StartDate, currentCulture);

            var auditOperationActive = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.AuditOperationActive);
            TestHelper.SetCheckbox(auditOperationActive, pcChanged.IsAuditOperationActive);

            var method = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.Method);
            TestHelper.ClickComboBoxEntry(QstSession, pcChanged.Method, method);

            if (pcChanged.Method == ProcessControlConditions.Methods.QSTMinTorque)
            {
                var qstMinMinTorque = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTMin_MinimumTorque);
                TestHelper.SetFloatingPointTextBox(QstSession, qstMinMinTorque, pcChanged.QstMinMinimumTorque, numberFormatThreeDecimals, currentCulture);

                var qstMinStartAngleCount = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTMin_StartAngleCount);
                TestHelper.SetFloatingPointTextBox(QstSession, qstMinStartAngleCount, pcChanged.QstMinStartAngleCount, numberFormatThreeDecimals, currentCulture);

                var qstMinAngleLimit = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTMin_AngleLimit);
                TestHelper.SetFloatingPointTextBox(QstSession, qstMinAngleLimit, pcChanged.QstMinAngleLimit, numberFormatNoDecimals, currentCulture);

                var qstMinStartMeasurement = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTMin_StartMeasurement);
                TestHelper.SetFloatingPointTextBox(QstSession, qstMinStartMeasurement, pcChanged.QstMinStartMeasurement, numberFormatThreeDecimals, currentCulture);

                var qstMinAlarmLimitTorque = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTMin_AlarmLimitTorque);
                TestHelper.SetFloatingPointTextBox(QstSession, qstMinAlarmLimitTorque, pcChanged.QstMinAlarmLimitTorque, numberFormatThreeDecimals, currentCulture);

                var qstMinAlarmLimitAngle = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTMin_AlarmLimitAngle);
                TestHelper.SetFloatingPointTextBox(QstSession, qstMinAlarmLimitAngle, pcChanged.QstMinAlarmLimitAngle, numberFormatThreeDecimals, currentCulture);
            }
            else if (pcChanged.Method == ProcessControlConditions.Methods.QSTPeak)
            {
                var qstPeakStartMeasurement = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTPeak_StartMeasurement);
                TestHelper.SetFloatingPointTextBox(QstSession, qstPeakStartMeasurement, pcChanged.QstPeakStartMeasurement, numberFormatThreeDecimals, currentCulture);
            }
            else if (pcChanged.Method == ProcessControlConditions.Methods.QSTPrevail)
            {
                var qstPrevailStartAngleCount = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTPrevail_StartAngleCount);
                TestHelper.SetFloatingPointTextBox(QstSession, qstPrevailStartAngleCount, pcChanged.QstPrevailStartAngleCount, numberFormatThreeDecimals, currentCulture);

                var qstPrevailAnglePrevail = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTPrevail_AnglePrevailTorque);
                TestHelper.SetFloatingPointTextBox(QstSession, qstPrevailAnglePrevail, pcChanged.QstPrevailAngleForPrevail, numberFormatThreeDecimals, currentCulture);

                var qstPrevailTargetAngle = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTPrevail_TargetAngle);
                TestHelper.SetFloatingPointTextBox(QstSession, qstPrevailTargetAngle, pcChanged.QstPrevailTargetAngle, numberFormatThreeDecimals, currentCulture);

                var qstPrevailStartMeasurement = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTPrevail_StartMeasurement);
                TestHelper.SetFloatingPointTextBox(QstSession, qstPrevailStartMeasurement, pcChanged.QstPrevailStartMeasurement, numberFormatThreeDecimals, currentCulture);

                var qstPrevailAlarmLimitTorque = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTPrevail_AlarmLimitTorque);
                TestHelper.SetFloatingPointTextBox(QstSession, qstPrevailAlarmLimitTorque, pcChanged.QstPrevailAlarmLimitTorque, numberFormatThreeDecimals, currentCulture);

                var qstPrevailAlarmLimitAngle = scrollviewer.FindElementByAccessibilityId(AiStringHelper.ProcessControl.ProcessConditionTabElements.QSTPrevail_AlarmLimitAngle);
                TestHelper.SetFloatingPointTextBox(QstSession, qstPrevailAlarmLimitAngle, pcChanged.QstPrevailAlarmLimitAngle, numberFormatThreeDecimals, currentCulture);
            }
        }
        private static void VerifyPcChangesInVerifyView(WindowsDriver<WindowsElement> driver, ProcessControlConditions pc, ProcessControlConditions pcChanged, AppiumWebElement listViewChanges, int numberOfExpectedChanges)
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
                    case "Lower intervention limit":
                        Assert.AreEqual(pc.LowerInterventionLimit.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.LowerInterventionLimit.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Upper intervention limit":
                        Assert.AreEqual(pc.UpperInterventionLimit.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.UpperInterventionLimit.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Lower measuring limit":
                        Assert.AreEqual(pc.LowerMeasuringLimit.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.LowerMeasuringLimit.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Upper measuring limit":
                        Assert.AreEqual(pc.UpperMeasuringLimit.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.UpperMeasuringLimit.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Test level set":
                        Assert.AreEqual(pc.TestLevelSet, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.TestLevelSet, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Test level number":
                        Assert.AreEqual(pc.TestLevelNumber.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.TestLevelNumber.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Start date":
                        Assert.AreEqual(pc.StartDate.ToString("d", CultureInfo.CreateSpecificCulture("en-us")), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.StartDate.ToString("d", CultureInfo.CreateSpecificCulture("en-us")), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Test operation active":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(pc.IsAuditOperationActive, true), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(pcChanged.IsAuditOperationActive, true), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Method":
                        Assert.AreEqual(pc.Method, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.Method, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "QST Standard Methods - Minimum torque - Minimum torque (Mmin)":
                        Assert.AreEqual(pc.QstMinMinimumTorque.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.QstMinMinimumTorque.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "QST Standard Methods - Minimum torque - Start angle count (Ms)":
                        Assert.AreEqual(pc.QstMinStartAngleCount.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.QstMinStartAngleCount.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "QST Standard Methods - Minimum torque - Angle limit (Alim)":
                        Assert.AreEqual(pc.QstMinAngleLimit.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.QstMinAngleLimit.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "QST Standard Methods - Minimum torque - Start measurement (Mstart)":
                        Assert.AreEqual(pc.QstMinStartMeasurement.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.QstMinStartMeasurement.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "QST Standard Methods - Minimum torque - Alarm limit - torque":
                        Assert.AreEqual(pc.QstMinAlarmLimitTorque.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.QstMinAlarmLimitTorque.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "QST Standard Methods - Minimum torque - Alarm limit - angle":
                        Assert.AreEqual(pc.QstMinAlarmLimitAngle.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.QstMinAlarmLimitAngle.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "QST Standard Methods - Peak - Start measurement (Mstart)":
                        Assert.AreEqual(pc.QstPeakStartMeasurement.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.QstPeakStartMeasurement.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "QST Standard Methods - Prevail torque/angle - Start angle count (Ms)":
                        Assert.AreEqual(pc.QstPrevailStartAngleCount.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.QstPrevailStartAngleCount.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "QST Standard Methods - Prevail torque/angle - Angle for prevail torque (A1)":
                        Assert.AreEqual(pc.QstPrevailAngleForPrevail.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.QstPrevailAngleForPrevail.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "QST Standard Methods - Prevail torque/angle - Target angle (A2)":
                        Assert.AreEqual(pc.QstPrevailTargetAngle.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.QstPrevailTargetAngle.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "QST Standard Methods - Prevail torque/angle - Start measurement (Mstart)":
                        Assert.AreEqual(pc.QstPrevailStartMeasurement.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.QstPrevailStartMeasurement.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "QST Standard Methods - Prevail torque/angle - Alarm limit - torque":
                        Assert.AreEqual(pc.QstPrevailAlarmLimitTorque.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.QstPrevailAlarmLimitTorque.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "QST Standard Methods - Prevail torque/angle - Alarm limit - angle":
                        Assert.AreEqual(pc.QstPrevailAlarmLimitAngle.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(pcChanged.QstPrevailAlarmLimitAngle.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    default:
                        Assert.IsTrue(false, string.Format("Case '{0}' not implemented", changeTypeText));
                        break;
                }
                i++;
            }
        }
    }
}