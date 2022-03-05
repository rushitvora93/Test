using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;

using UI_TestProjekt.Helper;
using UI_TestProjekt.TestModel;
using static UI_TestProjekt.ToolModelTests;
using static UI_TestProjekt.ToolTests;
using static UI_TestProjekt.ToolTestingTests;
using static UI_TestProjekt.AuxiliaryMasterDataTests;

namespace UI_TestProjekt
{
    [TestClass]
    public class MeasurementpointTests : TestBase
    {
        [TestMethod]
        [TestCategory("Dummy")]
        public void DummyMethod()
        {
            LoginAsCSP();
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestMeasurementPoint()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            MeasurementPoint ersterMP = Testdata.GetMp1();
            MeasurementPoint zweiterMP = Testdata.GetMp2();

            //Mp for Changes
            MeasurementPoint ersterMPChanged = Testdata.GetMpChanged1();
            MeasurementPoint zweiterMPChanged = Testdata.GetMpChanged2();

            //TODO wenn Bug https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-115 gefixed ist wieder am Anfang anlegen
            //addToleranceClass(zweiterMPChanged.ToleranceClassAngle, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ToleranceClass, QstSession);

            //Auf MPseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPointContainer, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //evtl. übriggebliebene Mps löschen falls vorhanden
            var mPView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            var btnDelete = mPView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);

            DeleteMpFolder(QstSession, ersterMP.ListParentFolder.GetRange(0, 2), btnDelete);

            DeleteMp(QstSession, ersterMP, btnDelete);
            DeleteMp(QstSession, zweiterMP, btnDelete);
            DeleteMp(QstSession, ersterMPChanged, btnDelete);
            DeleteMp(QstSession, zweiterMPChanged, btnDelete);

            //Auf Mpseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPointContainer, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Create Mp
            CreateMp(QstSession, ersterMP);
            CreateMp(QstSession, zweiterMP);

            //Erneut auf Mpseite wechseln
            //TODO Seitenwechsel evtl entfernen, oder mit und ohne Seitenwechsel testen
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPointContainer, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Check Mps
            AssertMp(QstSession, ersterMP);
            AssertMp(QstSession, zweiterMP);

            AddToleranceClass(zweiterMPChanged.ToleranceClassAngle, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ToleranceClass, QstSession);
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPointContainer, AiStringHelper.MegaMainSubmodule.MeasurementPoint);
            mPView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);


            //Change Mps
            var mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, QstSession, 5, 10);
            var cspMpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, ersterMP.GetParentListWithMp());
            cspMpNode.Click();
            UpdateMp(QstSession, ersterMPChanged);

            var viewVerifyChanges = DesktSession.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.View);
            var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyMpChangesInVerifyView(ersterMP, ersterMPChanged, listViewChanges, 14);

            var textBoxComment = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Comment);
            TestHelper.SendKeysConverted(textBoxComment, "Changecomment erster MeasurementPoint!");

            var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            btnApply.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

            mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, QstSession, 5, 10);
            var zweiterMpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, zweiterMP.GetParentListWithMp());
            zweiterMpNode.Click();
            UpdateMp(QstSession, zweiterMPChanged);

            viewVerifyChanges = DesktSession.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.View);

            listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyMpChangesInVerifyView(zweiterMP, zweiterMPChanged, listViewChanges, 13);
            textBoxComment = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Comment);
            TestHelper.SendKeysConverted(textBoxComment, "Changecomment zweiter MeasurementPoint!");

            btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            btnApply.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

            //TODO Auch mit Seitenwechsel testen
            //NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPointContainer, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Check Mp Changes
            AssertMp(QstSession, ersterMPChanged);
            AssertMp(QstSession, zweiterMPChanged);

            //Delete Mp
            btnDelete = mPView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMp(QstSession, ersterMPChanged, btnDelete);

            DeleteMp(QstSession, zweiterMPChanged, btnDelete);

            //Check deletion
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPointContainer, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, QstSession, 5, 10);
            var cspMpChangedNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, ersterMPChanged.GetParentListWithMp());
            Assert.IsNull(cspMpChangedNode);

            var zweiterMpChangedNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, zweiterMPChanged.GetParentListWithMp());
            Assert.IsNull(zweiterMpChangedNode);

            //Delete Folder
            mPView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mPView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMpFolder(QstSession, ersterMP.ListParentFolder.GetRange(0, 2), btnDelete);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestMeasurementPointWithTemplate()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            MeasurementPoint templateMp = Testdata.GetMpTemplateForTemplateTest();
            //TODO wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-141 gefixed
            //auch mit templateMp.ControlledBy = ControlledBy.Angle testen
            MeasurementPoint mpFromtemplate = Testdata.GetMpForTemplateTest();

            //TODO wenn Bug https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-115 gefixed ist wieder am Anfang anlegen
            //addToleranceClass(zweiterMPChanged.ToleranceClassAngle, AiStringHelper.MegaMainSubmodule.HelperTableTreeNames.ToleranceClass, QstSession);

            //Auf MPseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //evtl. übriggebliebene Mps löschen falls vorhanden
            var mPView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            var btnDelete = mPView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);

            DeleteMpFolder(QstSession, templateMp.ListParentFolder.GetRange(0, 2), btnDelete);

            DeleteMp(QstSession, templateMp, btnDelete);
            DeleteMp(QstSession, mpFromtemplate, btnDelete);

            //Auf Mpseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Create Mp
            CreateMp(QstSession, templateMp);
            CreateMp(QstSession, mpFromtemplate, true, templateMp);

            //Erneut auf Mpseite wechseln
            //TODO Seitenwechsel evtl entfernen, oder mit und ohne Seitenwechsel testen
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Check Mps
            AssertMp(QstSession, templateMp);
            AssertMp(QstSession, mpFromtemplate);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);
            mPView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);

            //Delete Mp
            btnDelete = mPView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMp(QstSession, templateMp, btnDelete);

            DeleteMp(QstSession, mpFromtemplate, btnDelete);

            //Check deletion
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Delete Folder
            mPView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mPView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMpFolder(QstSession, templateMp.ListParentFolder.GetRange(0, 2), btnDelete);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestMeasurementPointInvalidData()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            MeasurementPoint invalidMpTorque = Testdata.GetMpInvalidData1();
            MeasurementPoint invalidMpAngle = Testdata.GetMpInvalidData2();
            MeasurementPoint validMpTorque = Testdata.GetMpInvalidData1Valid();
            MeasurementPoint validMPAngle = Testdata.GetMpInvalidData2Valid();
            //Mp for Changes
            MeasurementPoint invalidMpTorqueForChange = Testdata.GetMpInvalidDataForChange1();
            MeasurementPoint invalidMpAngleForChange = Testdata.GetMpInvalidDataForChange2();

            //Auf MPseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //evtl. übriggebliebene Mps löschen falls vorhanden
            var mPView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            var btnDelete = mPView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);

            DeleteMpFolder(QstSession, invalidMpTorque.ListParentFolder.GetRange(0, 2), btnDelete);

            DeleteMp(QstSession, invalidMpTorque, btnDelete);
            DeleteMp(QstSession, invalidMpAngle, btnDelete);
            DeleteMp(QstSession, validMpTorque, btnDelete);
            DeleteMp(QstSession, validMPAngle, btnDelete);
            DeleteMp(QstSession, invalidMpTorqueForChange, btnDelete);
            DeleteMp(QstSession, invalidMpAngleForChange, btnDelete);

            //Auf Mpseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Create Mp
            CreateMp(QstSession, validMpTorque,false,null, true, false, invalidMpTorque);
            AssertMp(QstSession, validMpTorque);

            CreateMp(QstSession, validMPAngle, false, null, true, false, invalidMpAngle);
            AssertMp(QstSession, validMPAngle);

            //Mp Updaten
            var mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, QstSession, 5, 10);
            var validMpTorqueNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, validMpTorque.GetParentListWithMp());
            validMpTorqueNode.Click();
            UpdateMp(QstSession, invalidMpTorqueForChange, false);


            var mpView = QstSession.FindElementByAccessibilityId(AiStringHelper.Mp.View);
            var saveMpBtn = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SaveMp);
            var mpParamTab = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ParamTab);
            var scrollViewer = mpParamTab.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ParamTabScrollViewer);
            scrollViewer.SendKeys(Keys.PageUp);

            var description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            TestHelper.ScrollToElementWithOffset(QstSession, description);
            //TODO Entkommentieren, wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-6 (Tooltip) implementiert wurde
            //TestHelper.AssertToolTipText(QstSession, description, ValidationStringHelper.MpValidationStrings.MpValidation.DescriptionRequired);

            var minTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MinTorque);
            TestHelper.ScrollToElementWithOffset(QstSession, minTorque);
            TestHelper.AssertToolTipText(QstSession, minTorque, ValidationStringHelper.MpValidationStrings.MpValidation.MinTorqueLessEqualSetpointTorque);

            var maxTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MaxTorque);
            TestHelper.ScrollToElementWithOffset(QstSession, maxTorque);
            TestHelper.AssertToolTipText(QstSession, maxTorque, ValidationStringHelper.MpValidationStrings.MpValidation.MaxTorqueGreaterEqualSetpointTorque);

            var thresholdTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ThresholdTorque);
            TestHelper.ScrollToElementWithOffset(QstSession, thresholdTorque);
            TestHelper.AssertToolTipText(QstSession, thresholdTorque, ValidationStringHelper.MpValidationStrings.MpValidation.ThresholdLessEqualSetpointTorque);

            var minAngle = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MinAngle);
            TestHelper.ScrollToElementWithOffset(QstSession, minAngle);
            TestHelper.AssertToolTipText(QstSession, minAngle, ValidationStringHelper.MpValidationStrings.MpValidation.MinAngleLessEqualSetpointAngle);

            var maxAngle = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MaxAngle);
            TestHelper.ScrollToElementWithOffset(QstSession, maxAngle);
            TestHelper.AssertToolTipText(QstSession, maxAngle, ValidationStringHelper.MpValidationStrings.MpValidation.MaxAngleGreaterEqualSetpointAngle);

            Assert.IsFalse(saveMpBtn.Enabled);


            var validMpAngleNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, validMPAngle.GetParentListWithMp());
            validMpAngleNode.Click();

            CheckAndCloseValidationWindow(QstSession, ValidationStringHelper.MpValidationStrings.MpValidation.MpNotValidOnChange, AiStringHelper.GeneralStrings.NoBtn);
            UpdateMp(QstSession, invalidMpAngleForChange, false);

            minTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MinTorque);
            TestHelper.ScrollToElementWithOffset(QstSession, minTorque);
            TestHelper.AssertToolTipText(QstSession, minTorque, ValidationStringHelper.MpValidationStrings.MpValidation.MinTorqueLessEqualSetpointTorque);

            maxTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MaxTorque);
            TestHelper.ScrollToElementWithOffset(QstSession, maxTorque);
            TestHelper.AssertToolTipText(QstSession, maxTorque, ValidationStringHelper.MpValidationStrings.MpValidation.MaxTorqueGreaterEqualSetpointTorque);

            thresholdTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ThresholdTorque);
            TestHelper.ScrollToElementWithOffset(QstSession, thresholdTorque);
            TestHelper.AssertToolTipText(QstSession, thresholdTorque, ValidationStringHelper.MpValidationStrings.MpValidation.ThresholdLessEqualSetpointTorque);

            minAngle = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MinAngle);
            TestHelper.ScrollToElementWithOffset(QstSession, minAngle);
            TestHelper.AssertToolTipText(QstSession, minAngle, ValidationStringHelper.MpValidationStrings.MpValidation.MinAngleLessEqualSetpointAngle);

            maxAngle = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MaxAngle);
            TestHelper.ScrollToElementWithOffset(QstSession, minAngle);
            TestHelper.AssertToolTipText(QstSession, maxAngle, ValidationStringHelper.MpValidationStrings.MpValidation.MaxAngleGreaterEqualSetpointAngle);

            Assert.IsFalse(saveMpBtn.Enabled);
            //Ein Feld ändern damit Save-Button enabled ist
            validMPAngle.Description += "Changed";
            UpdateMp(QstSession, validMPAngle, false);

            Assert.IsTrue(saveMpBtn.Enabled);
            saveMpBtn.Click();

            var viewVerifyChanges = DesktSession.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.View);
            var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            btnApply.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

            validMpTorqueNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, validMpTorque.GetParentListWithMp());
            validMpTorqueNode.Click();

            var messageWindow = TestHelper.TryFindElementBy("#32770", DesktSession, AiStringHelper.By.Class);
            Assert.IsNull(messageWindow, "MessageWindow sollte nicht geöffnet sein");

            //Delete Folder
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            mPView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mPView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMpFolder(QstSession, validMpTorque.ListParentFolder.GetRange(0, 2), btnDelete);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        //TODO wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-6 gefixed ist NrnFelder auf Beschränkung testen
        public void TestMeasurementPointLongData()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            MeasurementPoint invalidMpTorque = Testdata.GetMpLongInvalidData1();
            MeasurementPoint invalidMpAngle = Testdata.GetMpLongInvalidData2();
            MeasurementPoint validMpTorque = Testdata.GetMpLongInvalidData1Valid();
            MeasurementPoint validMPAngle = Testdata.GetMpLongInvalidData2Valid();
            //Mp for Changes
            MeasurementPoint invalidMpTorqueForChange = Testdata.GetMpLongInvalidDataForChange1();
            MeasurementPoint validMpTorqueForChange = Testdata.GetMpLongInvalidDataForChange1Valid();
            MeasurementPoint invalidMpAngleForChange = Testdata.GetMpLongInvalidDataForChange2();
            MeasurementPoint validMpAngleForChange = Testdata.GetMpLongInvalidDataForChange2Valid();

            //Auf MPseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //evtl. übriggebliebene Mps löschen falls vorhanden
            var mPView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            var btnDelete = mPView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);

            DeleteMpFolder(QstSession, invalidMpTorque.ListParentFolder.GetRange(0, 2), btnDelete);

            DeleteMp(QstSession, invalidMpTorque, btnDelete);
            //DeleteMp(QstSession, invalidMpAngle, btnDelete);
            DeleteMp(QstSession, validMpTorque, btnDelete);
            //DeleteMp(QstSession, validMPAngle, btnDelete);
            DeleteMp(QstSession, invalidMpTorqueForChange, btnDelete);
            //DeleteMp(QstSession, invalidMpAngleForChange, btnDelete);
            DeleteMp(QstSession, validMpTorqueForChange, btnDelete);
            //DeleteMp(QstSession, validMpAngleForChange, btnDelete);

            //Auf Mpseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Create Mp
            CreateMp(QstSession, validMpTorque, false, null, false, true, invalidMpTorque);
            AssertMp(QstSession, validMpTorque);

            //CreateMp(QstSession, validMPAngle, false, null, false, true, invalidMpAngle);
            //AssertMp(QstSession, validMPAngle);

            //Mp Updaten
            var mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, QstSession, 5, 10);
            var validMpTorqueNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, validMpTorque.GetParentListWithMp());
            validMpTorqueNode.Click();
            UpdateMp(QstSession, invalidMpTorqueForChange, false);

            var mpView = QstSession.FindElementByAccessibilityId(AiStringHelper.Mp.View);
            var saveMpBtn = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SaveMp);
            var mpParamTab = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ParamTab);
            var scrollViewer = mpParamTab.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ParamTabScrollViewer);
            scrollViewer.SendKeys(Keys.PageUp);

            var number = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Number);
            Assert.AreEqual(validMpTorqueForChange.Number, number.Text);

            var description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            Assert.AreEqual(validMpTorqueForChange.Description, description.Text);

            var configurableField = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ConfigurableField);
            Assert.AreEqual(validMpTorqueForChange.ConfigurableField, configurableField.Text);

            var configurableField2 = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ConfigurableField2);
            Assert.AreEqual(validMpTorqueForChange.ConfigurableField2, configurableField2.Text);

            Assert.IsTrue(saveMpBtn.Enabled);
            saveMpBtn.Click();

            var viewVerifyChanges = TestHelper.FindElementWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");

            var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyMpChangesInVerifyView(validMpTorque, validMpTorqueForChange, listViewChanges, 5);

            var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            btnApply.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            var messageWindow = TestHelper.TryFindElementBy("#32770", DesktSession, AiStringHelper.By.Class);
            Assert.IsNull(messageWindow, "MessageWindow sollte nicht geöffnet sein");

            mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, QstSession, 5, 10);
            var validMpTorqueChangedNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, validMpTorqueForChange.GetParentListWithMp());
            validMpTorqueChangedNode.Click();

            AssertMp(QstSession, validMpTorqueForChange);

            //TODO Implementieren https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-6 wenn Nrbeschränkungen auch eingebaut wurden

            //var minTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MinTorque);
            //Assert.AreEqual(validMpTorque.MinTorque, minTorque.Text);

            //var maxTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MaxTorque);
            //Assert.AreEqual(validMpTorque.MaxTorque, maxTorque.Text);

            //var thresholdTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ThresholdTorque);
            //Assert.AreEqual(validMpTorque.ThresholdTorque, thresholdTorque.Text);

            //var minAngle = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MinAngle);
            //Assert.AreEqual(validMpTorque.MinAngle, minAngle.Text);

            //var maxAngle = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MaxAngle);
            //Assert.AreEqual(validMpTorque.MaxAngle, maxAngle.Text);

            //var validMpAngleNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, validMPAngle.GetParentListWithMp());
            //validMpAngleNode.Click();

            //UpdateMp(QstSession, invalidMpAngleForChange, false);

            //var minTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MinTorque);
            //Assert.AreEqual(validMpTorque.MinTorque, minTorque.Text);

            //var maxTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MaxTorque);
            //Assert.AreEqual(validMpTorque.MaxTorque, maxTorque.Text);

            //var thresholdTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ThresholdTorque);
            //Assert.AreEqual(validMpTorque.ThresholdTorque, thresholdTorque.Text);

            //var minAngle = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MinAngle);
            //Assert.AreEqual(validMpTorque.MinAngle, minAngle.Text);

            //var maxAngle = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MaxAngle);
            //Assert.AreEqual(validMpTorque.MaxAngle, maxAngle.Text);

            //Delete Folder
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            mPView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mPView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMpFolder(QstSession, validMpTorque.ListParentFolder.GetRange(0, 2), btnDelete);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestMeasurementPointOnChangeSwitchMp()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            MeasurementPoint mpChangeSite1 = Testdata.GetMpChangeSite1();
            MeasurementPoint mpChangeSite1Changed = Testdata.GetMpChangeSite1Changed();
            MeasurementPoint mpChangeSite2 = Testdata.GetMpChangeSite2();

            //Auf MPseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //evtl. übriggebliebene Mps löschen falls vorhanden
            var mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            var btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);

            DeleteMpFolder(QstSession, mpChangeSite1.ListParentFolder.GetRange(0, 2), btnDelete);
            DeleteMp(QstSession, mpChangeSite1, btnDelete);
            DeleteMp(QstSession, mpChangeSite2, btnDelete);

            //Auf Mpseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Create Mp
            CreateMp(QstSession, mpChangeSite1);
            CreateMp(QstSession, mpChangeSite2);

            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            var mpTreeView = TestHelper.FindElementByAiWithWaitFromParent(mpView, AiStringHelper.Mp.MpTreeView, QstSession);
            var mpTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(mpTreeView, AiStringHelper.Mp.MpTreeViewRoot, QstSession);

            var mpChangeSite1Node = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpChangeSite1.GetParentListWithMp());
            mpChangeSite1Node.Click();

            //Cancel
            var description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            description.Clear();
            TestHelper.SendKeysWithBackslash(QstSession, description, mpChangeSite1Changed.Description);

            var mpChangeSite2Node = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpChangeSite2.GetParentListWithMp());
            mpChangeSite2Node.Click();

            var viewVerifyChanges = TestHelper.FindElementWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");

            var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyMpChangesInVerifyView(mpChangeSite1, mpChangeSite1Changed, listViewChanges, 1);

            var btnCancel = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Cancel);
            btnCancel.Click();

            var number = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Number);
            Assert.AreEqual(mpChangeSite1.Number, number.Text);
            description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            Assert.AreEqual(mpChangeSite1Changed.Description, description.Text);

            //Reset
            description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            description.Clear();
            TestHelper.SendKeysWithBackslash(QstSession, description, mpChangeSite1Changed.Description);

            mpChangeSite2Node = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpChangeSite2.GetParentListWithMp());
            mpChangeSite2Node.Click();

            viewVerifyChanges = TestHelper.FindElementWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");

            listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyMpChangesInVerifyView(mpChangeSite1, mpChangeSite1Changed, listViewChanges, 1);

            var btnReset = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Reset);
            btnReset.Click();

            mpChangeSite1Node = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpChangeSite1.GetParentListWithMp());
            mpChangeSite1Node.Click();

            number = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Number);
            Assert.AreEqual(mpChangeSite1.Number, number.Text);
            description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            Assert.AreEqual(mpChangeSite1.Description, description.Text);

            //Apply
            description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            description.Clear();
            TestHelper.SendKeysWithBackslash(QstSession, description, mpChangeSite1Changed.Description);

            mpChangeSite2Node = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpChangeSite2.GetParentListWithMp());
            mpChangeSite2Node.Click();

            viewVerifyChanges = TestHelper.FindElementWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");

            listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyMpChangesInVerifyView(mpChangeSite1, mpChangeSite1Changed, listViewChanges, 1);

            var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            btnApply.Click();

            var mpChangeSite1ChangedNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpChangeSite1Changed.GetParentListWithMp());
            mpChangeSite1ChangedNode.Click();

            number = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Number);
            Assert.AreEqual(mpChangeSite1Changed.Number, number.Text);
            description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            Assert.AreEqual(mpChangeSite1Changed.Description, description.Text);

            //Delete Folder
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMpFolder(QstSession, mpChangeSite1.ListParentFolder.GetRange(0, 2), btnDelete);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestMeasurementPointOnChangeChangeSite()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            MeasurementPoint mpChangeSite1 = Testdata.GetMpChangeSite1();
            MeasurementPoint mpChangeSite1Changed = Testdata.GetMpChangeSite1Changed();

            //Auf MPseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //evtl. übriggebliebene Mps löschen falls vorhanden
            var mPView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            var btnDelete = mPView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);

            DeleteMpFolder(QstSession, mpChangeSite1.ListParentFolder.GetRange(0, 2), btnDelete);
            DeleteMp(QstSession, mpChangeSite1, btnDelete);

            //Auf Mpseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Create Mp
            CreateMp(QstSession, mpChangeSite1);

            var mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            var mpTreeView = TestHelper.FindElementByAiWithWaitFromParent(mpView, AiStringHelper.Mp.MpTreeView, QstSession);
            var mpTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(mpTreeView, AiStringHelper.Mp.MpTreeViewRoot, QstSession);

            var mpChangeSite1Node = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpChangeSite1.GetParentListWithMp());
            mpChangeSite1Node.Click();

            //Cancel
            var description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            description.Clear();
            TestHelper.SendKeysWithBackslash(QstSession, description, mpChangeSite1Changed.Description);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            var viewVerifyChanges = TestHelper.FindElementWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");

            var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyMpChangesInVerifyView(mpChangeSite1, mpChangeSite1Changed, listViewChanges, 1);

            var btnCancel = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Cancel);
            btnCancel.Click();

            var number = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Number);
            Assert.AreEqual(mpChangeSite1.Number, number.Text);
            description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            Assert.AreEqual(mpChangeSite1Changed.Description, description.Text);

            //Reset
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            viewVerifyChanges = TestHelper.FindElementWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");

            listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyMpChangesInVerifyView(mpChangeSite1, mpChangeSite1Changed, listViewChanges, 1);

            var btnReset = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Reset);
            btnReset.Click();

            var toolModelView = TestHelper.TryFindElementBy(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            Assert.IsNotNull(toolModelView);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            mpTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(mpView, AiStringHelper.Mp.MpTreeViewRoot, QstSession);
            mpChangeSite1Node = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpChangeSite1.GetParentListWithMp());
            mpChangeSite1Node.Click();

            number = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Number);
            Assert.AreEqual(mpChangeSite1.Number, number.Text);
            description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            Assert.AreEqual(mpChangeSite1.Description, description.Text);

            //Apply
            description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            description.Clear();
            TestHelper.SendKeysWithBackslash(QstSession, description, mpChangeSite1Changed.Description);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);

            viewVerifyChanges = TestHelper.FindElementWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");

            listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyMpChangesInVerifyView(mpChangeSite1, mpChangeSite1Changed, listViewChanges, 1);

            var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            btnApply.Click();

            toolModelView = TestHelper.TryFindElementBy(AiStringHelper.ToolModel.ToolModelView.View, QstSession);
            Assert.IsNotNull(toolModelView);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            mpTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(mpView, AiStringHelper.Mp.MpTreeViewRoot, QstSession);
            var mpChangeSite1ChangedNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpChangeSite1Changed.GetParentListWithMp());
            mpChangeSite1ChangedNode.Click();

            number = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Number);
            Assert.AreEqual(mpChangeSite1Changed.Number, number.Text);
            description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            Assert.AreEqual(mpChangeSite1Changed.Description, description.Text);

            //Delete Folder
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            mPView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mPView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMpFolder(QstSession, mpChangeSite1.ListParentFolder.GetRange(0, 2), btnDelete);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestMeasurementPointOnChangeLogout()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            MeasurementPoint mpChangeSite1 = Testdata.GetMpChangeSite1();
            MeasurementPoint mpChangeSite1Changed = Testdata.GetMpChangeSite1Changed();

            //Auf MPseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //evtl. übriggebliebene Mps löschen falls vorhanden
            var mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            var btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);

            DeleteMpFolder(QstSession, mpChangeSite1.ListParentFolder.GetRange(0, 2), btnDelete);
            DeleteMp(QstSession, mpChangeSite1, btnDelete);

            //Auf Mpseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Create Mp
            CreateMp(QstSession, mpChangeSite1);

            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);

            //Cancel
            var description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            description.Clear();
            TestHelper.SendKeysWithBackslash(QstSession, description, mpChangeSite1Changed.Description);

            var logout = QstSession.FindElementByAccessibilityId(AiStringHelper.GlobalToolbar.LogOut);
            logout.Click();

            var viewVerifyChanges = TestHelper.FindElementWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");

            var listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyMpChangesInVerifyView(mpChangeSite1, mpChangeSite1Changed, listViewChanges, 1);

            var btnCancel = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Cancel);
            btnCancel.Click();

            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            Assert.IsNotNull(mpView);

            var number = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Number);
            Assert.AreEqual(mpChangeSite1.Number, number.Text);
            description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            Assert.AreEqual(mpChangeSite1Changed.Description, description.Text);

            //Reset
            logout = QstSession.FindElementByAccessibilityId(AiStringHelper.GlobalToolbar.LogOut);
            logout.Click();

            viewVerifyChanges = TestHelper.FindElementWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");

            listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            VerifyMpChangesInVerifyView(mpChangeSite1, mpChangeSite1Changed, listViewChanges, 1);

            var btnReset = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Reset);
            btnReset.Click();

            LoginAsCSP(true);
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            var mpTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(mpView, AiStringHelper.Mp.MpTreeViewRoot, QstSession);
            var mpChangeSite1Node = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpChangeSite1.GetParentListWithMp());
            mpChangeSite1Node.Click();

            number = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Number);
            Assert.AreEqual(mpChangeSite1.Number, number.Text);
            description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            Assert.AreEqual(mpChangeSite1.Description, description.Text);

            //TODO Entkommentieren sobald https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-96 gefixed ist
            //Apply
            //description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            //description.Clear();
            //TestHelper.SendKeysWithBackslash(QstSession, description, mpChangeSite1Changed.Description);

            //logout = QstSession.FindElementByAccessibilityId(AiStringHelper.GlobalToolbar.LogOut);
            //logout.Click();

            //viewVerifyChanges = TestHelper.FindElementByAiWithWait(AiStringHelper.VerifyChanges.View, DesktSession);
            //Assert.IsNotNull(viewVerifyChanges, "VerifyChanges-Fenster wurde nicht geöffnet");

            //listViewChanges = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.ChangesListView);
            //VerifyMpChangesInVerifyView(mpChangeSite1, mpChangeSite1Changed, listViewChanges, 1);

            //var btnApply = viewVerifyChanges.FindElementByAccessibilityId(AiStringHelper.VerifyChanges.Apply);
            //btnApply.Click();

            //LoginAsCSP(true);
            //QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            //NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //mpView = TestHelper.FindElementByAiWithWait(AiStringHelper.Mp.View, QstSession);
            //mpTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(mpView, AiStringHelper.Mp.MpTreeViewRoot, QstSession);
            //var mpChangeSite1ChangedNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpChangeSite1Changed.GetParentListWithMp());
            //mpChangeSite1ChangedNode.Click();

            //number = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Number);
            //Assert.AreEqual(mpChangeSite1Changed.Number, number.Text);
            //description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            //Assert.AreEqual(mpChangeSite1Changed.Description, description.Text);

            //Delete Folder
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMpFolder(QstSession, mpChangeSite1.ListParentFolder.GetRange(0, 2), btnDelete);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestMeasurementPointDuplicateId()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            MeasurementPoint mpDuplicateId = Testdata.GetMpDuplicateId();

            //Auf MPseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //evtl. übriggebliebene Mps löschen falls vorhanden
            var mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            var btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);

            DeleteMpFolder(QstSession, mpDuplicateId.ListParentFolder.GetRange(0, 2), btnDelete);
            DeleteMp(QstSession, mpDuplicateId, btnDelete);

            //Auf Mpseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Create Mp
            CreateMp(QstSession, mpDuplicateId);

            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            var mpTreeView = TestHelper.FindElementByAiWithWaitFromParent(mpView, AiStringHelper.Mp.MpTreeView, QstSession);
            var mpTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(mpView, AiStringHelper.Mp.MpTreeViewRoot, QstSession);
            
            var addMpBtn = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.AddMp);

            var parentFolder = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpDuplicateId.ListParentFolder);
            ClickMpFolder(parentFolder, mpTreeView, QstSession);

            addMpBtn.Click();
            Thread.Sleep(500);
            var assistantSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.Assistant.View, TestConfiguration.GetWindowsApplicationDriverUrl());
            var assistantNextBtn = TestHelper.FindElementWithWait(AiStringHelper.Assistant.Next, assistantSession);
            var textInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputText, assistantSession);

            TestHelper.SendKeysWithBackslash(assistantSession, textInput, mpDuplicateId.Number);
            assistantNextBtn.Click();
            CheckAndCloseValidationWindow(assistantSession, ValidationStringHelper.MpValidationStrings.MpAssist.NrUniqueAndRequired);
            var cancel = assistantSession.FindElementByAccessibilityId(AiStringHelper.Assistant.Cancel);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(assistantSession, cancel);
            cancel.Click();

            //Delete Folder
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMpFolder(QstSession, mpDuplicateId.ListParentFolder.GetRange(0, 2), btnDelete);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        public void TestMeasurementPointTree()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);


            MeasurementPoint ersterMP = Testdata.GetMpTreeMp1();
            MeasurementPoint zweiterMP = Testdata.GetMpTreeMp2();
            MeasurementPoint dritterMP = Testdata.GetMpTreeMp3();

            var delete = TestHelper.FindElementWithWait(AiStringHelper.Mp.Delete, QstSession);
            DeleteMpFolder(QstSession, new List<string>() { Testdata.MpRootNode, "B-☺" }, delete);
            DeleteMpFolder(QstSession, new List<string>() { Testdata.MpRootNode, "B" }, delete);
            DeleteMpFolder(QstSession, new List<string>() { Testdata.MpRootNode, "C" }, delete);
            DeleteMpFolder(QstSession, new List<string>() { Testdata.MpRootNode, "Wurzel" }, delete);

            //DeleteMp(QstSession, ersterMP, btnDelete);
            //DeleteMp(QstSession, zweiterMP, btnDelete);
            //DeleteMp(QstSession, dritterMP, btnDelete);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            CreateMp(QstSession, ersterMP);
            CreateMp(QstSession, zweiterMP);
            CreateMp(QstSession, dritterMP);

            var listABDC = new List<string>() { Testdata.MpRootNode, "B", "D", "C" };
            var listAWurzelBaumAst = new List<string>() { Testdata.MpRootNode, "Wurzel", "Baum", "Ast" };
            var listABNeu = new List<string>() { Testdata.MpRootNode, "B", "Neu" };
            var listACDC = new List<string>() { Testdata.MpRootNode, "C", "D" };

            var mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            var mpTreeView = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.MpTreeView);
            CreateFolder(QstSession, listABDC, mpTreeView);
            CreateFolder(QstSession, listAWurzelBaumAst, mpTreeView);
            CreateFolder(QstSession, listABNeu, mpTreeView);
            CreateFolder(QstSession, listACDC, mpTreeView);

            var mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, QstSession);
            var zweiterMPNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, zweiterMP.GetParentListWithMp());
            zweiterMPNode.Click();
            var cut = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Cut);
            var paste = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Paste);
            TestHelper.ClickWithWait(cut, QstSession);
            var astNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, listAWurzelBaumAst);
            ClickMpFolder(astNode, mpTreeView, QstSession);
            TestHelper.ClickWithWait(paste, QstSession);
            zweiterMP.ListParentFolder = new List<string>(listAWurzelBaumAst);
            zweiterMPNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, zweiterMP.GetParentListWithMp());
            Assert.IsNotNull(zweiterMPNode, "Verschieben auf Ast hat nicht funktioniert");

            var ersterMpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, ersterMP.GetParentListWithMp());
            ersterMpNode.Click();
            TestHelper.ClickWithWait(cut, QstSession);
            var listAB = new List<string>() { Testdata.MpRootNode, "B" };
            var abNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, listAB);
            ClickMpFolder(abNode, mpTreeView, QstSession);
            TestHelper.ClickWithWait(paste, QstSession);
            ersterMP.ListParentFolder = new List<string>(listAB);
            ersterMpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, ersterMP.GetParentListWithMp());
            Assert.IsNotNull(ersterMpNode, "Verschieben auf AB hat nicht funktioniert");

            var aBCNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, dritterMP.ListParentFolder);
            ClickMpFolder(aBCNode, mpTreeView, QstSession);
            TestHelper.ClickWithWait(cut, QstSession);
            var abNeuNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, listABNeu);
            ClickMpFolder(abNeuNode, mpTreeView, QstSession);
            TestHelper.ClickWithWait(paste, QstSession);
            dritterMP.ListParentFolder = new List<string>(listABNeu);
            dritterMP.ListParentFolder.Add("C");
            var dritterMPNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, dritterMP.GetParentListWithMp());
            Assert.IsNotNull(dritterMPNode, "Verschieben auf ABNeu hat nicht funktioniert");

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);
            delete = TestHelper.FindElementWithWait(AiStringHelper.Mp.Delete, QstSession);
            cut = TestHelper.FindElementWithWait(AiStringHelper.Mp.Cut, QstSession);
            paste = TestHelper.FindElementWithWait(AiStringHelper.Mp.Paste, QstSession);

            mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, QstSession);
            zweiterMPNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, zweiterMP.GetParentListWithMp());
            Assert.IsNotNull(zweiterMPNode, "Verschieben auf Ast hat nicht funktioniert");
            ersterMpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, ersterMP.GetParentListWithMp());
            Assert.IsNotNull(ersterMpNode, "Verschieben auf AB hat nicht funktioniert");
            dritterMPNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, dritterMP.GetParentListWithMp());
            Assert.IsNotNull(dritterMPNode, "Verschieben auf ABNeu hat nicht funktioniert");

            DeleteMpFolder(QstSession, listABNeu, delete);
            dritterMPNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, dritterMP.GetParentListWithMp());
            abNeuNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, listABNeu);
            Assert.IsNull(dritterMPNode, "Löschen von dritterMP mit Pfad hat nicht funktioniert");
            Assert.IsNull(abNeuNode, "Löschen von dritterMP mit Pfad hat nicht funktioniert");

            //Einkommentieren nachdem https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-97 gefixed ist

            /*astNode = GetMpNode(QstSession, listAWurzelBaumAst);
            ClickMpFolder(astNode, mpTreeView, QstSession);
            ClickWithWait(cut, QstSession);
            ClickWithWait(delete, QstSession);
            var confirmDeleteBtn = FindElementByAbsoluteXPathWithWait(AiStringHelper.GeneralStrings.XPathConfirmButton);
            confirmDeleteBtn.Click();
            var aBDCNode = GetMpNode(QstSession, listABDC);
            ClickMpFolder(aBDCNode, mpTreeView, QstSession);
            ClickWithWait(paste, QstSession);

            var listABDCAst = new List<string>(listABDC);
            listABDCAst.Add(listAWurzelBaumAst[3]);
            var aBDCWurzelNode = GetMpNode(QstSession, listABDCAst);
            Assert.IsNull(aBDCWurzelNode,"AstNode wurde nach Löschen wieder eingefügt");

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);
            aBDCWurzelNode = GetMpNode(QstSession, listABDCAst);
            Assert.IsNull(aBDCWurzelNode, "AstNode wurde nach Löschen wieder eingefügt nach Seitenwechsel");
            */

            //Seitenwechsel nötig wegen QSTBV8116
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            delete = TestHelper.FindElementWithWait(AiStringHelper.Mp.Delete, QstSession);
            DeleteMpFolder(QstSession, new List<string>() { Testdata.MpRootNode, "B-☺" }, delete);
            DeleteMpFolder(QstSession, new List<string>() { Testdata.MpRootNode, "B" }, delete);
            DeleteMpFolder(QstSession, new List<string>() { Testdata.MpRootNode, "C" }, delete);
            DeleteMpFolder(QstSession, new List<string>() { Testdata.MpRootNode, "Wurzel" }, delete);
        }

        [TestMethod]
        [TestCategory("MasterData")]
        [Ignore]    // Ignore bis QSTBB-116 gefixed ist
        public void TestQSTBV8_116()
        {
            LoginAsCSP();
            //Session für QST-Fenster erstellen (Einloggen ist ausgenommen)
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            MeasurementPoint TestMp = Testdata.GetQSTBV8_116Mp1();

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            CreateMp(QstSession, TestMp);

            var delete = TestHelper.FindElementWithWait(AiStringHelper.Mp.Delete, QstSession);
            DeleteMp(QstSession, TestMp, delete);
            DeleteMpFolder(QstSession, TestMp.ListParentFolder, delete);
        }
        [TestMethod]
        [TestCategory("MasterData")]
        public void TestMeasurementPointUndoChanges()
        {
            LoginAsCSP();
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            MeasurementPoint mpForUpdate = Testdata.GetMpForUndoChanges();
            MeasurementPoint mpUpdate = Testdata.GetMpForUndoChangesUpdate();

            var btnDelete = TestHelper.FindElementWithWait(AiStringHelper.Mp.Delete, QstSession);
            DeleteMpFolder(QstSession, mpForUpdate.ListParentFolder, btnDelete);

            CreateMp(QstSession, mpForUpdate);

            var mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, QstSession, 5, 10);
            AppiumWebElement mpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpForUpdate.GetParentListWithMp());
            mpNode.Click();

            UpdateMp(QstSession, mpUpdate, false);
            var save = QstSession.FindElementByAccessibilityId(AiStringHelper.Mp.SaveMp);
            Assert.AreEqual(true, save.Enabled, "Speichern-Button sollte aktiv sein");

            UpdateMp(QstSession, mpForUpdate, false);
            save = QstSession.FindElementByAccessibilityId(AiStringHelper.Mp.SaveMp);
            Assert.AreEqual(false, save.Enabled, "Speichern-Button sollte NICHT aktiv sein");
            
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.ToolModel);
            var viewVerifyChanges = TestHelper.TryFindElementByAccessabilityId(AiStringHelper.VerifyChanges.View, DesktSession);
            Assert.IsNull(viewVerifyChanges);

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);
            AssertMp(QstSession, mpForUpdate);

            btnDelete = TestHelper.FindElementWithWait(AiStringHelper.Mp.Delete, QstSession);
            DeleteMpFolder(QstSession, mpForUpdate.ListParentFolder, btnDelete);
        }
        [TestMethod]
        [TestCategory("MasterData")]
        public void TestMeasurementPointToolReferenceTest()
        {
            LoginAsCSP();
            QstSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.MainWindow.Window, TestConfiguration.GetWindowsApplicationDriverUrl());

            MpToolAllocation mpToolAllocation1 = Testdata.GetMpToolForToolReferenceTest1();
            MpToolAllocation mpToolAllocation2 = Testdata.GetMpToolForToolReferenceTest2();
            MpToolAllocation mpToolAllocation3 = Testdata.GetMpToolForToolReferenceTest3();
            MpToolAllocation mpToolAllocation4 = Testdata.GetMpToolForToolReferenceTest4();

            // Löschen von Überbleibseln aus altem Test
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MpToolAllocation);
            
            //TODO ClickToolsRoot() kann entfernt werden wenn https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-161 gefixed ist
            string statusAfterRemoveAllocationString = mpToolAllocation1.Tool.Status;
            RemoveToolAllocation(statusAfterRemoveAllocationString, mpToolAllocation1, QstSession);
            ClickToolsRoot();
            statusAfterRemoveAllocationString = mpToolAllocation2.Tool.Status;
            RemoveToolAllocation(statusAfterRemoveAllocationString, mpToolAllocation2, QstSession);
            ClickToolsRoot();
            statusAfterRemoveAllocationString = mpToolAllocation3.Tool.Status;
            RemoveToolAllocation(statusAfterRemoveAllocationString, mpToolAllocation3, QstSession);
            ClickToolsRoot();
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

            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);
            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMpFolder(QstSession, mpToolAllocation1.Mp.ListParentFolder, btnDelete);

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

            //Auf MpToolAllocationseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MpToolAllocation);

            var mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.MpTreeViewRoot, QstSession);
            var mpToolAllocation1MpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpToolAllocation1.Mp.GetParentListWithMp());
            mpToolAllocation1MpNode.Click();

            var toolTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.MpToolAllocation.ToolTreeViewRoot, QstSession);
            var mpToolAllocation1ToolNode = TestHelper.GetNode(QstSession, toolTreeviewRootNode, mpToolAllocation1.Tool.GetParentListWithTool());
            mpToolAllocation1ToolNode.Click();

            string statusAfterAllocationString = "In Betrieb";
            AllocateTool(mpToolAllocation1, statusAfterAllocationString);

            var mpToolAllocation2MpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpToolAllocation2.Mp.GetParentListWithMp());
            mpToolAllocation2MpNode.Click();
            var mpToolAllocation2ToolNode = TestHelper.GetNode(QstSession, toolTreeviewRootNode, mpToolAllocation2.Tool.GetParentListWithTool());
            mpToolAllocation2ToolNode.Click();
            AllocateTool(mpToolAllocation2, statusAfterAllocationString);

            var mpToolAllocation3MpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpToolAllocation3.Mp.GetParentListWithMp());
            mpToolAllocation3MpNode.Click();
            var mpToolAllocation3ToolNode = TestHelper.GetNode(QstSession, toolTreeviewRootNode, mpToolAllocation3.Tool.GetParentListWithTool());
            mpToolAllocation3ToolNode.Click();
            AllocateTool(mpToolAllocation3, statusAfterAllocationString);

            var mpToolAllocation4MpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpToolAllocation4.Mp.GetParentListWithMp());
            mpToolAllocation4MpNode.Click();
            var mpToolAllocation4ToolNode = TestHelper.GetNode(QstSession, toolTreeviewRootNode, mpToolAllocation4.Tool.GetParentListWithTool());
            mpToolAllocation4ToolNode.Click();
            AllocateTool(mpToolAllocation4, statusAfterAllocationString);


            //Eigentlicher Test
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, QstSession);

            var node = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpToolAllocation1.Mp.GetParentListWithMp());
                
            node.Click();
            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            //Warten bis Mp geladen wurde und DeleteButton Klickbar ist
            Assert.IsTrue(TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, 2, 300, btnDelete), "MpDeleteButton ist nicht sichtbar");
            btnDelete.Click();

            var confirmDeleteBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
            confirmDeleteBtn.Click();

            Thread.Sleep(1500);
            var changeToolStateWindow = TestHelper.FindElementBy(DesktSession, AiStringHelper.ChangeToolState.View, 20, 300);
            Assert.IsNotNull(changeToolStateWindow, "ChangeToolState Window wurde nicht gefunden");
            //Check First ChangeToolStateChange with Cancel
            var changeToolStateSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.ChangeToolState.View, TestConfiguration.GetWindowsApplicationDriverUrl());
            var changeToolStateDG = changeToolStateSession.FindElementByAccessibilityId(AiStringHelper.ChangeToolState.MpToolReferenceDG);
            //var partScrollviewer = changeToolStateDG.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.PART_ScrollViewer);

            var headerBlock = changeToolStateDG.FindElementByXPath("*/Custom[@ClassName=\"CaptionSummaryRowControl\"]/Custom[@ClassName=\"GridCaptionSummaryCell\"]/Text[@ClassName=\"TextBlock\"]");
            var expectedHeader = string.Format("Measurement point : {0} - {1} - 2 Items", mpToolAllocation1.Mp.Number, mpToolAllocation1.Mp.Description);
            Assert.AreEqual(expectedHeader, headerBlock.Text);

            var mpToolAllocationBlocks = changeToolStateDG.FindElementsByXPath("*/Custom[@ClassName=\"VirtualizingCellsControl\"]");

            int count = 0;
            foreach (var mpToolAllocationBlock in mpToolAllocationBlocks)
            {
                var expander = mpToolAllocationBlock.FindElementByXPath("*/Custom[@ClassName=\"GridDetailsViewExpanderCell\"]");

                var gridCells = mpToolAllocationBlock.FindElementsByXPath("*/*[@ClassName=\"GridCell\"]");
                var mp = gridCells[0].FindElementByXPath("*/Text[@ClassName=\"TextBlock\"]");
                var tool = gridCells[1].FindElementByXPath("*/Text[@ClassName=\"TextBlock\"]");
                var status = gridCells[2];
                var statusCombo = status.FindElementByXPath("*/Text[@ClassName=\"TextBlock\"]");
                //var firstStatusCombo = gridCells[2].FindElementByXPath("*/ComboBox[@ClassName=\"ComboBox\"]");
                var expectedMpString = "";
                var expectedToolString = "";
                var expectedStatusString = "";

                expander.Click();

                var otherAllocatedMeasurementPointsContainers = changeToolStateDG.FindElementsByXPath("*/Custom[@ClassName=\"DetailsViewRowControl\"]");
                AppiumWebElement otherAllocatedMeasurementPoints = null;

                var expectedOtherMpToolAllocationString = "";

                if (count == 0)
                {
                    otherAllocatedMeasurementPoints = TestHelper.TryFindElementBy(AiStringHelper.ChangeToolState.OtherAssignedLocations, otherAllocatedMeasurementPointsContainers[count]);

                    expectedMpString = string.Format("{0} - {1}", mpToolAllocation1.Mp.Number, mpToolAllocation1.Mp.Description);
                    expectedToolString = string.Format("Status for{0} - {1}", mpToolAllocation1.Tool.SerialNumber, mpToolAllocation1.Tool.InventoryNumber);
                    expectedStatusString = mpToolAllocation1.Tool.Status;

                    var measurementPointText = otherAllocatedMeasurementPoints.Text;
                    expectedOtherMpToolAllocationString = string.Format("{0} - {1}\r\n{2} - {3}\r\n", mpToolAllocation3.Mp.Description, mpToolAllocation3.Mp.Number, mpToolAllocation4.Mp.Description, mpToolAllocation4.Mp.Number);
                    Assert.AreEqual(expectedOtherMpToolAllocationString, measurementPointText);
                }
                if (count == 1)
                {
                    expectedMpString = string.Format("{0} - {1}", mpToolAllocation2.Mp.Number, mpToolAllocation2.Mp.Description);
                    expectedToolString = string.Format("Status for{0} - {1}", mpToolAllocation2.Tool.SerialNumber, mpToolAllocation2.Tool.InventoryNumber);
                    expectedStatusString = mpToolAllocation2.Tool.Status;

                    Assert.AreEqual(1, otherAllocatedMeasurementPointsContainers.Count, "Bei zweiter Zuordnung gibt es keine weitere Wkz-Zuordnung");
                }

                Assert.AreEqual(expectedMpString, mp.Text);
                Assert.AreEqual(expectedToolString, tool.Text);
                Assert.AreEqual(expectedStatusString, statusCombo.Text);
                count++;
            }
            Assert.AreEqual(2, count);

            var cancel = changeToolStateSession.FindElementByAccessibilityId(AiStringHelper.ChangeToolState.Cancel);
            cancel.Click();

            //Check First ChangeToolStateChange with Apply
            mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, QstSession);

            node = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpToolAllocation1.Mp.GetParentListWithMp());

            node.Click();
            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            //Warten bis Mp geladen wurde und DeleteButton Klickbar ist
            Assert.IsTrue(TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, 2, 300, btnDelete), "MpDeleteButton ist nicht sichtbar");
            btnDelete.Click();

            confirmDeleteBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
            confirmDeleteBtn.Click();
            Thread.Sleep(1500);
            changeToolStateWindow = TestHelper.FindElementBy(DesktSession, AiStringHelper.ChangeToolState.View, 20, 300);
            Assert.IsNotNull(changeToolStateWindow, "ChangeToolState Window wurde nicht gefunden");

            //Check First ChangeToolStateChange
            changeToolStateSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.ChangeToolState.View, TestConfiguration.GetWindowsApplicationDriverUrl());
            changeToolStateDG = changeToolStateSession.FindElementByAccessibilityId(AiStringHelper.ChangeToolState.MpToolReferenceDG);
            //var partScrollviewer = changeToolStateDG.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.PART_ScrollViewer);

            headerBlock = changeToolStateDG.FindElementByXPath("*/Custom[@ClassName=\"CaptionSummaryRowControl\"]/Custom[@ClassName=\"GridCaptionSummaryCell\"]/Text[@ClassName=\"TextBlock\"]");
            expectedHeader = string.Format("Measurement point : {0} - {1} - 2 Items", mpToolAllocation1.Mp.Number, mpToolAllocation1.Mp.Description);
            Assert.AreEqual(expectedHeader, headerBlock.Text);

            mpToolAllocationBlocks = changeToolStateDG.FindElementsByXPath("*/Custom[@ClassName=\"VirtualizingCellsControl\"]");

            count = 0;
            foreach (var mpToolAllocationBlock in mpToolAllocationBlocks)
            {
                var expander = mpToolAllocationBlock.FindElementByXPath("*/Custom[@ClassName=\"GridDetailsViewExpanderCell\"]");

                var gridCells = mpToolAllocationBlock.FindElementsByXPath("*/*[@ClassName=\"GridCell\"]");
                var mp = gridCells[0].FindElementByXPath("*/Text[@ClassName=\"TextBlock\"]");
                var tool = gridCells[1].FindElementByXPath("*/Text[@ClassName=\"TextBlock\"]");
                var status = gridCells[2];
                var statusCombo = status.FindElementByXPath("*/Text[@ClassName=\"TextBlock\"]");
                //var firstStatusCombo = gridCells[2].FindElementByXPath("*/ComboBox[@ClassName=\"ComboBox\"]");
                var expectedMpString = "";
                var expectedToolString = "";
                var expectedStatusString = "";

                expander.Click();

                var otherAllocatedMeasurementPointsContainers = changeToolStateDG.FindElementsByXPath("*/Custom[@ClassName=\"DetailsViewRowControl\"]");
                AppiumWebElement otherAllocatedMeasurementPoints = null;

                var expectedOtherMpToolAllocationString = "";

                if (count == 0)
                {
                    otherAllocatedMeasurementPoints = TestHelper.TryFindElementBy(AiStringHelper.ChangeToolState.OtherAssignedLocations, otherAllocatedMeasurementPointsContainers[count]);

                    expectedMpString = string.Format("{0} - {1}", mpToolAllocation1.Mp.Number, mpToolAllocation1.Mp.Description);
                    expectedToolString = string.Format("Status for{0} - {1}", mpToolAllocation1.Tool.SerialNumber, mpToolAllocation1.Tool.InventoryNumber);
                    expectedStatusString = mpToolAllocation1.Tool.Status;

                    var measurementPointText = otherAllocatedMeasurementPoints.Text;
                    expectedOtherMpToolAllocationString = string.Format("{0} - {1}\r\n{2} - {3}\r\n", mpToolAllocation3.Mp.Description, mpToolAllocation3.Mp.Number, mpToolAllocation4.Mp.Description, mpToolAllocation4.Mp.Number);
                    Assert.AreEqual(expectedOtherMpToolAllocationString, measurementPointText);
                }
                if (count == 1)
                {
                    expectedMpString = string.Format("{0} - {1}", mpToolAllocation2.Mp.Number, mpToolAllocation2.Mp.Description);
                    expectedToolString = string.Format("Status for{0} - {1}", mpToolAllocation2.Tool.SerialNumber, mpToolAllocation2.Tool.InventoryNumber);
                    expectedStatusString = mpToolAllocation2.Tool.Status;

                    Assert.AreEqual(1, otherAllocatedMeasurementPointsContainers.Count, "Bei zweiter Zuordnung gibt es keine weitere Wkz-Zuordnung");
                }

                Assert.AreEqual(expectedMpString, mp.Text);
                Assert.AreEqual(expectedToolString, tool.Text);
                Assert.AreEqual(expectedStatusString, statusCombo.Text);
                count++;
            }
            Assert.AreEqual(2, count);

            var apply = changeToolStateSession.FindElementByAccessibilityId(AiStringHelper.ChangeToolState.Apply);
            apply.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);


            //Check Second ChangeToolStateChange with Apply
            mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, QstSession);

            node = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpToolAllocation3.Mp.GetParentListWithMp());

            node.Click();
            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            //Warten bis Mp geladen wurde und DeleteButton Klickbar ist
            Assert.IsTrue(TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, 2, 300, btnDelete), "MpDeleteButton ist nicht sichtbar");
            btnDelete.Click();

            confirmDeleteBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
            confirmDeleteBtn.Click();
            Thread.Sleep(1500);
            changeToolStateWindow = TestHelper.FindElementBy(DesktSession, AiStringHelper.ChangeToolState.View, 20, 300);
            Assert.IsNotNull(changeToolStateWindow, "ChangeToolState Window wurde nicht gefunden");
            changeToolStateSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.ChangeToolState.View, TestConfiguration.GetWindowsApplicationDriverUrl());
            changeToolStateDG = changeToolStateSession.FindElementByAccessibilityId(AiStringHelper.ChangeToolState.MpToolReferenceDG);
            //var partScrollviewer = changeToolStateDG.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.PART_ScrollViewer);

            headerBlock = changeToolStateDG.FindElementByXPath("*/Custom[@ClassName=\"CaptionSummaryRowControl\"]/Custom[@ClassName=\"GridCaptionSummaryCell\"]/Text[@ClassName=\"TextBlock\"]");
            expectedHeader = string.Format("Measurement point : {0} - {1} - 1 Items", mpToolAllocation3.Mp.Number, mpToolAllocation3.Mp.Description);
            Assert.AreEqual(expectedHeader, headerBlock.Text);

            mpToolAllocationBlocks = changeToolStateDG.FindElementsByXPath("*/Custom[@ClassName=\"VirtualizingCellsControl\"]");

            count = 0;
            foreach (var mpToolAllocationBlock in mpToolAllocationBlocks)
            {
                var expander = mpToolAllocationBlock.FindElementByXPath("*/Custom[@ClassName=\"GridDetailsViewExpanderCell\"]");

                var gridCells = mpToolAllocationBlock.FindElementsByXPath("*/*[@ClassName=\"GridCell\"]");
                var mp = gridCells[0].FindElementByXPath("*/Text[@ClassName=\"TextBlock\"]");
                var tool = gridCells[1].FindElementByXPath("*/Text[@ClassName=\"TextBlock\"]");
                var status = gridCells[2];
                var statusCombo = status.FindElementByXPath("*/Text[@ClassName=\"TextBlock\"]");
                //var firstStatusCombo = gridCells[2].FindElementByXPath("*/ComboBox[@ClassName=\"ComboBox\"]");
                var expectedMpString = "";
                var expectedToolString = "";
                var expectedStatusString = "";

                expander.Click();

                var otherAllocatedMeasurementPointsContainers = changeToolStateDG.FindElementsByXPath("*/Custom[@ClassName=\"DetailsViewRowControl\"]");

                if (count == 0)
                {
                    expectedMpString = string.Format("{0} - {1}", mpToolAllocation3.Mp.Number, mpToolAllocation3.Mp.Description);
                    expectedToolString = string.Format("Status for{0} - {1}", mpToolAllocation3.Tool.SerialNumber, mpToolAllocation3.Tool.InventoryNumber);
                    expectedStatusString = mpToolAllocation1.Tool.Status;

                    Assert.AreEqual(1, otherAllocatedMeasurementPointsContainers.Count, "Bei dritter Zuordnung gibt es keine weitere Wkz-Zuordnung");
                }

                Assert.AreEqual(expectedMpString, mp.Text);
                Assert.AreEqual(expectedToolString, tool.Text);
                Assert.AreEqual(expectedStatusString, statusCombo.Text);
                count++;
            }
            Assert.AreEqual(1, count);

            apply = changeToolStateSession.FindElementByAccessibilityId(AiStringHelper.ChangeToolState.Apply);
            apply.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

            //Check Third ChangeToolStateChange with Apply
            mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, QstSession);

            node = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpToolAllocation4.Mp.GetParentListWithMp());

            node.Click();
            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            //Warten bis Mp geladen wurde und DeleteButton Klickbar ist
            Assert.IsTrue(TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, 2, 300, btnDelete), "MpDeleteButton ist nicht sichtbar");
            btnDelete.Click();

            confirmDeleteBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
            confirmDeleteBtn.Click();
            Thread.Sleep(1500);
            changeToolStateWindow = TestHelper.FindElementBy(DesktSession, AiStringHelper.ChangeToolState.View, 20, 300);
            Assert.IsNotNull(changeToolStateWindow, "ChangeToolState Window wurde nicht gefunden");
            changeToolStateSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.ChangeToolState.View, TestConfiguration.GetWindowsApplicationDriverUrl());
            changeToolStateDG = changeToolStateSession.FindElementByAccessibilityId(AiStringHelper.ChangeToolState.MpToolReferenceDG);
            //var partScrollviewer = changeToolStateDG.FindElementByAccessibilityId(AiStringHelper.GeneralStrings.PART_ScrollViewer);

            headerBlock = changeToolStateDG.FindElementByXPath("*/Custom[@ClassName=\"CaptionSummaryRowControl\"]/Custom[@ClassName=\"GridCaptionSummaryCell\"]/Text[@ClassName=\"TextBlock\"]");
            expectedHeader = string.Format("Measurement point : {0} - {1} - 1 Items", mpToolAllocation4.Mp.Number, mpToolAllocation4.Mp.Description);
            Assert.AreEqual(expectedHeader, headerBlock.Text);

            mpToolAllocationBlocks = changeToolStateDG.FindElementsByXPath("*/Custom[@ClassName=\"VirtualizingCellsControl\"]");

            count = 0;
            foreach (var mpToolAllocationBlock in mpToolAllocationBlocks)
            {
                var expander = mpToolAllocationBlock.FindElementByXPath("*/Custom[@ClassName=\"GridDetailsViewExpanderCell\"]");

                var gridCells = mpToolAllocationBlock.FindElementsByXPath("*/*[@ClassName=\"GridCell\"]");
                var mp = gridCells[0].FindElementByXPath("*/Text[@ClassName=\"TextBlock\"]");
                var tool = gridCells[1].FindElementByXPath("*/Text[@ClassName=\"TextBlock\"]");
                var status = gridCells[2];
                var statusCombo = status.FindElementByXPath("*/Text[@ClassName=\"TextBlock\"]");
                //var firstStatusCombo = gridCells[2].FindElementByXPath("*/ComboBox[@ClassName=\"ComboBox\"]");
                var expectedMpString = "";
                var expectedToolString = "";
                var expectedStatusString = "";

                expander.Click();

                var otherAllocatedMeasurementPointsContainers = changeToolStateDG.FindElementsByXPath("*/Custom[@ClassName=\"DetailsViewRowControl\"]");

                if (count == 0)
                {
                    expectedMpString = string.Format("{0} - {1}", mpToolAllocation4.Mp.Number, mpToolAllocation4.Mp.Description);
                    expectedToolString = string.Format("Status for{0} - {1}", mpToolAllocation4.Tool.SerialNumber, mpToolAllocation4.Tool.InventoryNumber);
                    expectedStatusString = mpToolAllocation4.Tool.Status;

                    Assert.AreEqual(0, otherAllocatedMeasurementPointsContainers.Count, "Bei vierter Zuordnung gibt es keine weitere Wkz-Zuordnung");
                }

                Assert.AreEqual(expectedMpString, mp.Text);
                Assert.AreEqual(expectedToolString, tool.Text);
                Assert.AreEqual(expectedStatusString, statusCombo.Text);
                count++;
            }
            Assert.AreEqual(1, count);

            apply = changeToolStateSession.FindElementByAccessibilityId(AiStringHelper.ChangeToolState.Apply);
            apply.Click();
            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);

            //Check deletion
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPointContainer, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, QstSession, 5, 10);
            var cspMpChangedNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpToolAllocation1.Mp.GetParentListWithMp());
            Assert.IsNull(cspMpChangedNode);

            var zweiterMpChangedNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mpToolAllocation3.Mp.GetParentListWithMp());
            Assert.IsNull(zweiterMpChangedNode);


            //TODO Status von Tool ändern und prüfen wenn man den Status im ChangeToolStatus-Fenster richtig ändern und prüfen kann


            //Reste wieder löschen
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            // Löschen von Überbleibseln aus altem Test

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

            //Auf Mpseite wechseln
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);

            //Mp-Ordner löschen
            NavigateToMainMenu(QstSession, AiStringHelper.MegaMainSubmodule.MeasurementPoint);
            mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            btnDelete = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.Delete);
            DeleteMpFolder(QstSession, mpToolAllocation1.Mp.ListParentFolder, btnDelete);
        }


        public static void DeleteMp(WindowsDriver<WindowsElement> QstSession, MeasurementPoint mp, AppiumWebElement btnDeleteMp)
        {
            var mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, QstSession);

            //Kein Ordner angelegt also muss auch nichts gelöscht werden
            if(mpTreeviewRootNode == null)
            {
                return;
            }
            var node = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mp.GetParentListWithMp());
            if (node != null)
            {
                node.Click();
                //Warten bis Mp geladen wurde und DeleteButton Klickbar ist
                Assert.IsTrue(TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, 2, 300, btnDeleteMp), "MpDeleteButton ist nicht sichtbar");
                btnDeleteMp.Click();

                var confirmDeleteBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
                confirmDeleteBtn.Click();
                TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            }
        }
        public static void DeleteMpFolder(WindowsDriver<WindowsElement> QstSession, List<string> folder, AppiumWebElement btnDelete)
        {
            var treeView = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeView, QstSession);
            var mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, QstSession);
            var node = TestHelper.GetNode(QstSession, mpTreeviewRootNode, folder);
            if (node != null)
            {
                ClickMpFolder(node, treeView, QstSession);
                //Warten bis Mp geladen wurde und DeleteButton Klickbar ist
                Assert.IsTrue(TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, 2, 300, btnDelete), "MpDeleteButton ist nicht enabled/sichtbar");
                btnDelete.Click();

                var confirmDeleteBtn = TestHelper.FindElementByAbsoluteXPathWithWait(DesktSession, AiStringHelper.GeneralStrings.XPathConfirmButton);
                confirmDeleteBtn.Click();
                TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
            }
        }
        public static void CreateMp(
            WindowsDriver<WindowsElement> QstSession,
            MeasurementPoint mp, bool withTemplateCheck = false, MeasurementPoint template = null,
            bool withCheckValidationErrors = false, bool withLongValues = false, MeasurementPoint invalidMp = null)
        {
            Assert.IsNotNull(QstSession, "QstSession in CreateMp ist null");
            Assert.AreEqual(withTemplateCheck, template!=null, "TemplateCheck but Template is null");

            var mpView = TestHelper.FindElementWithWait(AiStringHelper.Mp.View, QstSession);
            var mpTreeView = TestHelper.FindElementByAiWithWaitFromParent(mpView, AiStringHelper.Mp.MpTreeView, QstSession);
            var mpTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(mpView, AiStringHelper.Mp.MpTreeViewRoot, QstSession);
            var mpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mp.GetParentListWithMp());
            if(mpNode != null)
            {
                return;
            }

            CreateFolder(QstSession, mp.ListParentFolder, mpTreeView);
            var addMpBtn = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.AddMp);

            //Falls Rootordner noch nicht angelegt war neuen RootNode auslesen
            if (mpTreeviewRootNode == null)
            {
                mpTreeviewRootNode = TestHelper.FindElementByAiWithWaitFromParent(mpView, AiStringHelper.Mp.MpTreeViewRoot, QstSession);
            }
            var parentFolder = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mp.ListParentFolder);
            if (!withTemplateCheck)
            {
                ClickMpFolder(parentFolder, mpTreeView, QstSession);
            }
            else
            {
                var templateNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, template.GetParentListWithMp());
                templateNode.Click();
            }

            addMpBtn.Click();
            Thread.Sleep(500);
            var assistantSession = TestHelper.GetWindowSession(DesktSession, AiStringHelper.Assistant.View, TestConfiguration.GetWindowsApplicationDriverUrl());
            var textInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputText, assistantSession);
            if (withTemplateCheck)
            {
                Assert.AreEqual(template.Number, textInput.Text);
            }
            textInput.Clear();
            if(withCheckValidationErrors || withLongValues)
            {
                TestHelper.SendKeysWithBackslash(assistantSession, textInput, invalidMp.Number);
            }
            else
            {
                TestHelper.SendKeysWithBackslash(assistantSession, textInput, mp.Number);
            }
            AssertAssistantListEntry(assistantSession, mp.Number, AssistantStringHelper.MpStrings.Number);
            var assistantNextBtn = TestHelper.FindElementWithWait(AiStringHelper.Assistant.Next, assistantSession);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                Assert.AreEqual(template.Description, textInput.Text);
            }
            textInput.Clear();
            if (withCheckValidationErrors || withLongValues)
            {
                TestHelper.SendKeysWithBackslash(assistantSession, textInput, invalidMp.Description);
            }
            else
            {
                TestHelper.SendKeysWithBackslash(assistantSession, textInput, mp.Description);
            }
            AssertAssistantListEntry(assistantSession, mp.Description, AssistantStringHelper.MpStrings.Description);
            assistantNextBtn.Click();

            //Timeout damit das listInput gefunden wird, unklar warum beim Buildserver withWait nicht greift
            Thread.Sleep(500);
            var listInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputList, assistantSession);
            if (withTemplateCheck)
            {
                //FindElementsByClassName("ListBoxItem")[1].Selected
                var templateElement = listInput.FindElementByXPath(String.Format("*/*[@ClassName=\"ListBoxItem\"][@AutomationId=\"{0}\"]", template.ControlledBy.ToString()));
                Assert.IsTrue(templateElement.Selected, "'Controlled By' from Template is not selected");
            }
            var manuControlledBy = listInput.FindElementByAccessibilityId(mp.ControlledBy.ToString());
            manuControlledBy.Click();
            AssertAssistantListEntry(assistantSession, mp.ControlledBy.ToString(), AssistantStringHelper.MpStrings.ControlledBy);
            assistantNextBtn.Click();

            var floatingPointInput = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputFloatingPoint, assistantSession);

            if (mp.ControlledBy == ControlledBy.Torque)
            {
                if (withTemplateCheck)
                {
                    Assert.AreEqual(template.SetPointTorque.ToString(numberFormatThreeDecimals, currentCulture), floatingPointInput.Text);
                }
                if (withCheckValidationErrors)
                {
                    floatingPointInput.SendKeys(invalidMp.SetPointTorque.ToString(numberFormatThreeDecimals, currentCulture));
                    assistantNextBtn.Click();
                    CheckAndCloseValidationWindow(assistantSession, ValidationStringHelper.MpValidationStrings.MpAssist.SetpointTorqueGreaterZero);
                }
                floatingPointInput.SendKeys(mp.SetPointTorque.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mp.SetPointTorque.ToString(currentCulture), AssistantStringHelper.MpStrings.SetPointTorque, AssistantStringHelper.UnitStrings.Nm);
                assistantNextBtn.Click();

                //TODO ausgewählte Toleranzklasse prüfen implementieren
                //Aktuelles Problem Item.Selected ist bei allen Elementen initial False obwohl Klasse von Vorlage ausgewählt angezeigt wird
                //Wird Selected nicht richtig gesetzt?
                //Bei anderen Listboxen im Assistent, welche vorbefüllt sind ist Selected auch richtig gesetzt
                //if (withTemplateCheck)
                //{
                //    var templateElement = listInput.FindElementByXPath(String.Format("*/*[@ClassName=\"ListBoxItem\"][@AutomationId=\"{0}\"]", template.ToleranceClassTorque.Name));
                //    Assert.IsTrue(templateElement.Selected, "'ToleranceClassTorque' from Template is not selected");
                //}
                var tolClassTorque = FindOrCreateToleranceClass(mp.ToleranceClassTorque, assistantSession, listInput);
                tolClassTorque.Click();
                AssertAssistantListEntry(assistantSession, mp.ToleranceClassTorque.Name, AssistantStringHelper.MpStrings.ToleranceClassTorque);
                assistantNextBtn.Click();

                if (mp.ToleranceClassTorque.Name == "freie Eingabe")
                {
                    if (withTemplateCheck)
                    {
                        Assert.AreEqual(template.MinTorque.ToString(numberFormatThreeDecimals, currentCulture), floatingPointInput.Text);
                    }
                    if (withCheckValidationErrors)
                    {
                        floatingPointInput.SendKeys(invalidMp.MinTorque.ToString(numberFormatThreeDecimals, currentCulture));
                        assistantNextBtn.Click();
                        CheckAndCloseValidationWindow(assistantSession, ValidationStringHelper.MpValidationStrings.MpAssist.MinTorqueLessEqualSetpointTorque);
                    }
                    floatingPointInput.SendKeys(mp.MinTorque.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, mp.MinTorque.ToString(currentCulture), AssistantStringHelper.MpStrings.MinimumTorque, AssistantStringHelper.UnitStrings.Nm);
                    assistantNextBtn.Click();

                    if (withTemplateCheck)
                    {
                        Assert.AreEqual(template.MaxTorque.ToString(numberFormatThreeDecimals, currentCulture), floatingPointInput.Text);
                    }
                    if (withCheckValidationErrors)
                    {
                        floatingPointInput.SendKeys(invalidMp.MaxTorque.ToString(numberFormatThreeDecimals, currentCulture));
                        assistantNextBtn.Click();
                        CheckAndCloseValidationWindow(assistantSession, ValidationStringHelper.MpValidationStrings.MpAssist.MaxTorqueGreaterEqualSetpointTorque);
                    }
                    floatingPointInput.SendKeys(mp.MaxTorque.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, mp.MaxTorque.ToString(currentCulture), AssistantStringHelper.MpStrings.MaximumTorque, AssistantStringHelper.UnitStrings.Nm);
                    assistantNextBtn.Click();
                }
                if (withTemplateCheck)
                {
                    Assert.AreEqual(template.ThresholdTorque.ToString(numberFormatThreeDecimals, currentCulture), floatingPointInput.Text);
                }
                if (withCheckValidationErrors)
                {
                    floatingPointInput.SendKeys(invalidMp.ThresholdTorque.ToString(numberFormatThreeDecimals, currentCulture));
                    assistantNextBtn.Click();
                    CheckAndCloseValidationWindow(assistantSession, ValidationStringHelper.MpValidationStrings.MpAssist.ThresholdGreaterZeroLessEqualSetpointTorque);
                }
                floatingPointInput.SendKeys(mp.ThresholdTorque.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mp.ThresholdTorque.ToString(currentCulture), AssistantStringHelper.MpStrings.ThresholdTorque, AssistantStringHelper.UnitStrings.Nm);
                assistantNextBtn.Click();

                if (withTemplateCheck)
                {
                    Assert.AreEqual(template.SetPointAngle.ToString(numberFormatThreeDecimals, currentCulture), floatingPointInput.Text);
                }
                floatingPointInput.SendKeys(mp.SetPointAngle.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mp.SetPointAngle.ToString(currentCulture), AssistantStringHelper.MpStrings.SetPointAngle, AssistantStringHelper.UnitStrings.Deg);
                assistantNextBtn.Click();

                //TODO implementieren
                //Aktuelles Problem Item.Selected ist bei allen Elementen initial False obwohl Klasse von Vorlage ausgewählt angezeigt wird
                //Wird Selected nicht richtig gesetzt?
                //Bei anderen Listboxen im Assistent, welche vorbefüllt sind ist Selected auch richtig gesetzt
                //if (withTemplateCheck)
                //{
                //    var templateElement = listInput.FindElementByXPath(String.Format("*/*[@ClassName=\"ListBoxItem\"][@AutomationId=\"{0}\"]", template.ToleranceClassAngle.Name));
                //    Assert.IsTrue(templateElement.Selected, "'ToleranceClassAngle' from Template is not selected");
                //}
                var tolClassAngle = FindOrCreateToleranceClass(mp.ToleranceClassAngle, assistantSession, listInput);
                tolClassAngle.Click();
                AssertAssistantListEntry(assistantSession, mp.ToleranceClassAngle.Name, AssistantStringHelper.MpStrings.ToleranceClassAngle);
                assistantNextBtn.Click();

                if (mp.ToleranceClassAngle.Name == "freie Eingabe")
                {
                    if (withTemplateCheck)
                    {
                        Assert.AreEqual(template.MinAngle.ToString(numberFormatThreeDecimals, currentCulture), floatingPointInput.Text);
                    }
                    if (withCheckValidationErrors)
                    {
                        floatingPointInput.SendKeys(invalidMp.MinAngle.ToString(numberFormatThreeDecimals, currentCulture));
                        assistantNextBtn.Click();
                        CheckAndCloseValidationWindow(assistantSession, ValidationStringHelper.MpValidationStrings.MpAssist.MinAngleLessEqualSetpointAngle);
                    }
                    floatingPointInput.SendKeys(mp.MinAngle.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, mp.MinAngle.ToString(currentCulture), AssistantStringHelper.MpStrings.MinimumAngle, AssistantStringHelper.UnitStrings.Deg);
                    assistantNextBtn.Click();

                    if (withTemplateCheck)
                    {
                        Assert.AreEqual(template.MinAngle.ToString(numberFormatThreeDecimals, currentCulture), floatingPointInput.Text);
                    }
                    if (withCheckValidationErrors)
                    {
                        floatingPointInput.SendKeys(invalidMp.MaxAngle.ToString(numberFormatThreeDecimals, currentCulture));
                        assistantNextBtn.Click();
                        CheckAndCloseValidationWindow(assistantSession, ValidationStringHelper.MpValidationStrings.MpAssist.MaxAngleGreaterEqualSetpointAngle);
                    }
                    floatingPointInput.SendKeys(mp.MaxAngle.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, mp.MaxAngle.ToString(currentCulture), AssistantStringHelper.MpStrings.MaximumAngle, AssistantStringHelper.UnitStrings.Deg);
                    assistantNextBtn.Click();
                }
            }
            else
            {
                if (withTemplateCheck)
                {
                    Assert.AreEqual(template.ThresholdTorque.ToString(numberFormatThreeDecimals, currentCulture), floatingPointInput.Text);
                }
                if (withCheckValidationErrors)
                {
                    floatingPointInput.SendKeys(invalidMp.ThresholdTorque.ToString(numberFormatThreeDecimals, currentCulture));
                    assistantNextBtn.Click();
                    CheckAndCloseValidationWindow(assistantSession, ValidationStringHelper.MpValidationStrings.MpAssist.ThresholdTorqueGreaterZero);
                }
                floatingPointInput.SendKeys(mp.ThresholdTorque.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mp.ThresholdTorque.ToString(currentCulture), AssistantStringHelper.MpStrings.ThresholdTorque, AssistantStringHelper.UnitStrings.Nm);
                assistantNextBtn.Click();

                if (withTemplateCheck)
                {
                    Assert.AreEqual(template.SetPointAngle.ToString(numberFormatThreeDecimals, currentCulture), floatingPointInput.Text);
                }
                if (withCheckValidationErrors)
                {
                    floatingPointInput.SendKeys(invalidMp.SetPointAngle.ToString(numberFormatThreeDecimals, currentCulture));
                    assistantNextBtn.Click();
                    CheckAndCloseValidationWindow(assistantSession, ValidationStringHelper.MpValidationStrings.MpAssist.SetpointAngleGreaterZero);
                }
                floatingPointInput.SendKeys(mp.SetPointAngle.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mp.SetPointAngle.ToString(currentCulture), AssistantStringHelper.MpStrings.SetPointAngle, AssistantStringHelper.UnitStrings.Deg);
                assistantNextBtn.Click();

                //TODO implementieren
                //Aktuelles Problem Item.Selected ist bei allen Elementen initial False obwohl Klasse von Vorlage ausgewählt angezeigt wird
                //Wird Selected nicht richtig gesetzt?
                //Bei anderen Listboxen im Assistent, welche vorbefüllt sind ist Selected auch richtig gesetzt
                //if (withTemplateCheck)
                //{
                //    var templateElement = listInput.FindElementByXPath(String.Format("*/*[@ClassName=\"ListBoxItem\"][@AutomationId=\"{0}\"]", template.ToleranceClassAngle.Name));
                //    Assert.IsTrue(templateElement.Selected, "'ToleranceClassAngle' from Template is not selected");
                //}
                var tolClassAngle = FindOrCreateToleranceClass(mp.ToleranceClassAngle, assistantSession, listInput);
                tolClassAngle.Click();
                AssertAssistantListEntry(assistantSession, mp.ToleranceClassAngle.Name, AssistantStringHelper.MpStrings.ToleranceClassAngle);
                assistantNextBtn.Click();

                if (mp.ToleranceClassAngle.Name == "freie Eingabe")
                {
                    if (withTemplateCheck)
                    {
                        Assert.AreEqual(template.MinAngle.ToString(numberFormatThreeDecimals, currentCulture), floatingPointInput.Text);
                    }
                    if (withCheckValidationErrors)
                    {
                        floatingPointInput.SendKeys(invalidMp.MinAngle.ToString(numberFormatThreeDecimals, currentCulture));
                        assistantNextBtn.Click();
                        CheckAndCloseValidationWindow(assistantSession, ValidationStringHelper.MpValidationStrings.MpAssist.MinAngleLessEqualSetpointAngle);
                    }
                    floatingPointInput.SendKeys(mp.MinAngle.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, mp.MinAngle.ToString(currentCulture), AssistantStringHelper.MpStrings.MinimumAngle, AssistantStringHelper.UnitStrings.Deg);
                    assistantNextBtn.Click();

                    if (withTemplateCheck)
                    {
                        Assert.AreEqual(template.MaxAngle.ToString(numberFormatThreeDecimals, currentCulture), floatingPointInput.Text);
                    }
                    if (withCheckValidationErrors)
                    {
                        floatingPointInput.SendKeys(invalidMp.MaxAngle.ToString(numberFormatThreeDecimals, currentCulture));
                        assistantNextBtn.Click();
                        CheckAndCloseValidationWindow(assistantSession, ValidationStringHelper.MpValidationStrings.MpAssist.MaxAngleGreaterEqualSetpointAngle);
                    }
                    floatingPointInput.SendKeys(mp.MaxAngle.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, mp.MaxAngle.ToString(currentCulture), AssistantStringHelper.MpStrings.MaximumAngle, AssistantStringHelper.UnitStrings.Deg);
                    assistantNextBtn.Click();
                }
                if (withTemplateCheck)
                {
                    Assert.AreEqual(template.SetPointTorque.ToString(numberFormatThreeDecimals, currentCulture), floatingPointInput.Text);
                }
                floatingPointInput.SendKeys(mp.SetPointTorque.ToString(numberFormatThreeDecimals, currentCulture));
                AssertAssistantListEntry(assistantSession, mp.SetPointTorque.ToString(currentCulture), AssistantStringHelper.MpStrings.SetPointTorque, AssistantStringHelper.UnitStrings.Nm);
                assistantNextBtn.Click();

                //TODO implementieren
                //Aktuelles Problem Item.Selected ist bei allen Elementen initial False obwohl Klasse von Vorlage ausgewählt angezeigt wird
                //Wird Selected nicht richtig gesetzt?
                //Bei anderen Listboxen im Assistent, welche vorbefüllt sind ist Selected auch richtig gesetzt
                //if (withTemplateCheck)
                //{
                //    var templateElement = listInput.FindElementByXPath(String.Format("*/*[@ClassName=\"ListBoxItem\"][@AutomationId=\"{0}\"]", template.ToleranceClassTorque.Name));
                //    Assert.IsTrue(templateElement.Selected, "'ToleranceClassTorque' from Template is not selected");
                //}
                var tolClassTorque = FindOrCreateToleranceClass(mp.ToleranceClassTorque, assistantSession, listInput);
                tolClassTorque.Click();
                AssertAssistantListEntry(assistantSession, mp.ToleranceClassTorque.Name, AssistantStringHelper.MpStrings.ToleranceClassTorque);
                assistantNextBtn.Click();

                if (mp.ToleranceClassTorque.Name == "freie Eingabe")
                {
                    if (withTemplateCheck)
                    {
                        Assert.AreEqual(template.MinTorque.ToString(numberFormatThreeDecimals, currentCulture), floatingPointInput.Text);
                    }
                    if (withCheckValidationErrors)
                    {
                        floatingPointInput.SendKeys(invalidMp.MinTorque.ToString(numberFormatThreeDecimals, currentCulture));
                        assistantNextBtn.Click();
                        CheckAndCloseValidationWindow(assistantSession, ValidationStringHelper.MpValidationStrings.MpAssist.MinTorqueLessEqualSetpointTorque);
                    }
                    floatingPointInput.SendKeys(mp.MinTorque.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, mp.MinTorque.ToString(currentCulture), AssistantStringHelper.MpStrings.MinimumTorque, AssistantStringHelper.UnitStrings.Nm);
                    assistantNextBtn.Click();

                    if (withTemplateCheck)
                    {
                        Assert.AreEqual(template.MaxTorque.ToString(numberFormatThreeDecimals, currentCulture), floatingPointInput.Text);
                    }
                    if (withCheckValidationErrors)
                    {
                        floatingPointInput.SendKeys(invalidMp.MaxTorque.ToString(numberFormatThreeDecimals, currentCulture));
                        assistantNextBtn.Click();
                        CheckAndCloseValidationWindow(assistantSession, ValidationStringHelper.MpValidationStrings.MpAssist.MaxTorqueGreaterEqualSetpointTorque);
                    }
                    floatingPointInput.SendKeys(mp.MaxTorque.ToString(numberFormatThreeDecimals, currentCulture));
                    AssertAssistantListEntry(assistantSession, mp.MaxTorque.ToString(currentCulture), AssistantStringHelper.MpStrings.MaximumTorque, AssistantStringHelper.UnitStrings.Nm);
                    assistantNextBtn.Click();
                }

            }

            if (withTemplateCheck)
            {
                Assert.AreEqual(template.ConfigurableField, textInput.Text);
            }
            textInput.Clear();
            if (withCheckValidationErrors || withLongValues)
            {
                TestHelper.SendKeysWithBackslash(assistantSession, textInput, invalidMp.ConfigurableField);
            }
            else
            {
                TestHelper.SendKeysWithBackslash(assistantSession, textInput, mp.ConfigurableField);
            }
            AssertAssistantListEntry(assistantSession, mp.ConfigurableField, AssistantStringHelper.MpStrings.ConfigurableField1);
            assistantNextBtn.Click();

            if (withTemplateCheck)
            {
                Assert.AreEqual(template.ConfigurableField2, textInput.Text);
            }
            textInput.Clear();
            if (withCheckValidationErrors || withLongValues)
            {
                TestHelper.SendKeysWithBackslash(assistantSession, textInput, invalidMp.ConfigurableField2);
            }
            else
            {
                TestHelper.SendKeysWithBackslash(assistantSession, textInput, mp.ConfigurableField2);
            }
            AssertAssistantListEntry(assistantSession, mp.ConfigurableField2, AssistantStringHelper.MpStrings.ConfigurableField2);
            assistantNextBtn.Click();

            var inputBool = TestHelper.FindElementWithWait(AiStringHelper.Assistant.InputBoolean, assistantSession);
            if (withTemplateCheck)
            {
                Assert.AreEqual(template.ConfigurableField3, inputBool.Selected);
            }
            TestHelper.SetCheckbox(inputBool, mp.ConfigurableField3);
            AssertAssistantListEntry(assistantSession, mp.ConfigurableField3.ToString(), AssistantStringHelper.MpStrings.ConfigurableField3, "", true);
            assistantNextBtn.Click();

            TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
        }
        private static void ClickMpFolder(AppiumWebElement parentFolder, AppiumWebElement treeView, WindowsDriver<WindowsElement> driver)
        {
            var folderImage = parentFolder.FindElementByAccessibilityId("LeftImage");
            Assert.IsNotNull(folderImage, "OrdnerIcon nicht gefunden");

            var scrollViewerRightBtn = TestHelper.TryFindElementBy(AiStringHelper.GeneralStrings.ScrollViewerRightBtn, treeView);
            var scrollViewerLeftBtn = TestHelper.TryFindElementBy(AiStringHelper.GeneralStrings.ScrollViewerLeftBtn, treeView);
            var scrollViewerAreaLeft = TestHelper.TryFindElementBy(AiStringHelper.GeneralStrings.ScrollViewerAreaLeft, treeView);
            var scrollViewerAreaRight = TestHelper.TryFindElementBy(AiStringHelper.GeneralStrings.ScrollViewerAreaRight, treeView);
            if (scrollViewerAreaLeft != null && scrollViewerLeftBtn != null && scrollViewerRightBtn != null)
            {
                if (scrollViewerAreaLeft.Size.Width > 0)
                {
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    Actions actions = new Actions(driver);
                    actions.MoveToElement(scrollViewerLeftBtn);
                    actions.ClickAndHold();
                    actions.Build().Perform();
                    while (scrollViewerAreaLeft.Size.Width > 0 && watch.ElapsedMilliseconds < 5000)
                    {
                        Thread.Sleep(100);
                    }
                    watch.Stop();
                    actions = new Actions(driver);
                    actions.Release(scrollViewerLeftBtn);
                    actions.Build().Perform();
                }

                while (scrollViewerAreaRight.Size.Width > 0
                    && treeView.Location.X + treeView.Rect.Width < folderImage.Location.X)
                //&& treeView.Location.X + treeView.Rect.Width < QstSession.Manage().Window.Position.X + folderImage.Location.X)
                {
                    scrollViewerRightBtn.Click();
                }
            }
            folderImage.Click();
        }
        private static void AssertMp(WindowsDriver<WindowsElement> QstSession, MeasurementPoint mp)
        {
            var mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, QstSession, 5, 10);
            //var mpTreeviewRootNode = TryFindElementByAccessabilityId(AiStringHelper.Mp.MpTreeViewRoot, QstSession);
            AppiumWebElement mpNode = TestHelper.GetNode(QstSession, mpTreeviewRootNode, mp.GetParentListWithMp());

            //TODO Scrolling einbauen wenn Tree zu lang ist
            mpNode.Click();

            var mpView = QstSession.FindElementByAccessibilityId(AiStringHelper.Mp.View);
            var mpParamTab = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ParamTab);
            var scrollViewer = mpParamTab.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ParamTabScrollViewer);
            var scrollViewerUpArea = TestHelper.TryFindElementBy(AiStringHelper.GeneralStrings.ScrollViewerAreaUp, scrollViewer);

            TestHelper.ScrollUp(scrollViewer);

            var number = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Number);
            Assert.AreEqual(mp.Number, number.Text);

            var description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            Assert.AreEqual(mp.Description, description.Text);

            var controlledBy = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ControlledBy);
            string controlledByString = TestHelper.GetSelectedComboboxStringWithScrolling(QstSession, controlledBy, scrollViewer);
            Assert.AreEqual(mp.ControlledBy.ToString(), controlledByString);

            var setPointTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.SetpointTorque);
            Assert.AreEqual(mp.SetPointTorque.ToString(numberFormatThreeDecimals, currentCulture), setPointTorque.Text);

            var tolClassTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ToleranceClassTorque);
            string tolClassTorqueString = TestHelper.GetSelectedComboboxStringWithScrolling(QstSession, tolClassTorque, scrollViewer);
            Assert.AreEqual(mp.ToleranceClassTorque.Name, tolClassTorqueString);

            var minTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MinTorque);
            Assert.AreEqual(mp.MinTorque.ToString(numberFormatThreeDecimals, currentCulture), minTorque.Text);

            var maxTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MaxTorque);
            Assert.AreEqual(mp.MaxTorque.ToString(numberFormatThreeDecimals, currentCulture), maxTorque.Text);

            var thresholdTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ThresholdTorque);
            Assert.AreEqual(mp.ThresholdTorque.ToString(numberFormatThreeDecimals, currentCulture), thresholdTorque.Text);

            var setPointAngle = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.SetpointAngle);
            Assert.AreEqual(mp.SetPointAngle.ToString(numberFormatThreeDecimals, currentCulture), setPointAngle.Text);

            var tolClassAngle = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ToleranceClassAngle);
            string tolClassAngleString = TestHelper.GetSelectedComboboxStringWithScrolling(QstSession, tolClassAngle, scrollViewer);
            Assert.AreEqual(mp.ToleranceClassAngle.Name, tolClassAngleString);

            var minAngle = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MinAngle);
            Assert.AreEqual(mp.MinAngle.ToString(numberFormatThreeDecimals, currentCulture), minAngle.Text);

            var maxAngle = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MaxAngle);
            Assert.AreEqual(mp.MaxAngle.ToString(numberFormatThreeDecimals, currentCulture), maxAngle.Text);

            //Länge ist auf 15 Zeichen beschränkt
            var configurableField = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ConfigurableField);
            if (mp.ConfigurableField.Length <= 15)
            {
                Assert.AreEqual(mp.ConfigurableField, configurableField.Text);
            }
            else
            {
                Assert.AreEqual(mp.ConfigurableField.Substring(0, 15), configurableField.Text);
            }

            var configurableField2 = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ConfigurableField2);
            Assert.AreEqual(mp.ConfigurableField2, configurableField2.Text);

            var configurableField3 = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ConfigurableField3);
            Assert.AreEqual(mp.ConfigurableField3, configurableField3.Selected);

            //TODO Einkommentieren wenn Kommentare angezeigt werden
            //var comment = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Comment);
            //Assert.AreEqual(mp.Comment, comment.Text);
        }
        /// <summary>
        /// Updated Mp: Seite muss vorher ausgewählt und Element ausgewählt sein
        /// </summary>
        private static void UpdateMp(
            WindowsDriver<WindowsElement> QstSession,
            MeasurementPoint mp, bool save = true)
        {
            var mpView = QstSession.FindElementByAccessibilityId(AiStringHelper.Mp.View);
            var saveMpBtn = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SaveMp);
            var mpParamTab = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ParamTab);
            var scrollViewer = mpParamTab.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ParamTabScrollViewer);
            scrollViewer.SendKeys(Keys.PageUp);

            var number = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Number);
            TestHelper.WaitForElementToBeEnabledAndDisplayed(QstSession, number);
            number.Clear();
            TestHelper.SendKeysConverted(number, mp.Number);

            var description = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Description);
            description.Clear();
            TestHelper.SendKeysConverted(description, mp.Description);

            var comboBoxControlledBy = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ControlledBy);
            TestHelper.ClickComboBoxEntryWithScrolling(QstSession, mp.ControlledBy.ToString(), comboBoxControlledBy, mpView, AiStringHelper.Mp.SingleMp.ParamTab, AiStringHelper.Mp.SingleMp.ParamTabScrollViewer);

            var setPointTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.SetpointTorque);
            setPointTorque.Clear();
            setPointTorque.SendKeys(Keys.ArrowRight);
            TestHelper.SendKeysConverted(setPointTorque, mp.SetPointTorque.ToString(numberFormatThreeDecimals, currentCulture));

            var comboBoxTolClassTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ToleranceClassTorque);
            TestHelper.ClickComboBoxEntryWithScrolling(QstSession, mp.ToleranceClassTorque.Name.ToString(), comboBoxTolClassTorque, mpView, AiStringHelper.Mp.SingleMp.ParamTab, AiStringHelper.Mp.SingleMp.ParamTabScrollViewer);

            if (mp.ToleranceClassTorque.Name == "freie Eingabe")
            {
                var minTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MinTorque);
                minTorque.Clear();
                minTorque.SendKeys(Keys.ArrowRight);
                TestHelper.SendKeysConverted(minTorque, mp.MinTorque.ToString(numberFormatThreeDecimals, currentCulture));

                var maxTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MaxTorque);
                maxTorque.Clear();
                maxTorque.SendKeys(Keys.ArrowRight);
                TestHelper.SendKeysConverted(maxTorque, mp.MaxTorque.ToString(numberFormatThreeDecimals, currentCulture));
            }

            var thresholdTorque = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ThresholdTorque);
            thresholdTorque.Clear();
            thresholdTorque.SendKeys(Keys.ArrowRight);
            TestHelper.SendKeysConverted(thresholdTorque, mp.ThresholdTorque.ToString(numberFormatThreeDecimals, currentCulture));

            var setPointAngle = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.SetpointAngle);
            setPointAngle.Clear();
            setPointAngle.SendKeys(Keys.ArrowRight);
            TestHelper.SendKeysConverted(setPointAngle, mp.SetPointAngle.ToString(numberFormatThreeDecimals, currentCulture));

            var comboBoxTolClassAngle = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ToleranceClassAngle);
            TestHelper.ClickComboBoxEntryWithScrolling(QstSession, mp.ToleranceClassAngle.Name.ToString(), comboBoxTolClassAngle, mpView, AiStringHelper.Mp.SingleMp.ParamTab, AiStringHelper.Mp.SingleMp.ParamTabScrollViewer);

            if (mp.ToleranceClassAngle.Name == "freie Eingabe")
            {
                var minAngle = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MinAngle);
                minAngle.Clear();
                minAngle.SendKeys(Keys.ArrowRight);
                TestHelper.SendKeysConverted(minAngle, mp.MinAngle.ToString(numberFormatThreeDecimals, currentCulture));

                var maxAngle = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.MaxAngle);
                maxAngle.Clear();
                maxAngle.SendKeys(Keys.ArrowRight);
                TestHelper.SendKeysConverted(maxAngle, mp.MaxAngle.ToString(numberFormatThreeDecimals, currentCulture));
            }

            var configurableField = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ConfigurableField);
            configurableField.Clear();
            TestHelper.SendKeysConverted(configurableField, mp.ConfigurableField);

            var configurableField2 = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ConfigurableField2);
            configurableField2.Clear();
            TestHelper.SendKeysConverted(configurableField2, mp.ConfigurableField2);


            var checkBoxConfigurableField3 = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.ConfigurableField3);
            if ((checkBoxConfigurableField3.Selected && !mp.ConfigurableField3)
                || (!checkBoxConfigurableField3.Selected && mp.ConfigurableField3))
            {
                checkBoxConfigurableField3.Click();
            }

            var comment = mpView.FindElementByAccessibilityId(AiStringHelper.Mp.SingleMp.Comment);
            comment.Clear();
            TestHelper.SendKeysConverted(comment, mp.Comment);

            if (save) 
            { 
                saveMpBtn.Click();
            }
        }
        private static void VerifyMpChangesInVerifyView(MeasurementPoint mp, MeasurementPoint mpChanged, AppiumWebElement listViewChanges, int numberOfExpectedChanges)
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
                    case "Number":
                        Assert.AreEqual(mp.Number, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(mpChanged.Number, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Description":
                        Assert.AreEqual(mp.Description, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(mpChanged.Description, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Controlled by":
                        Assert.AreEqual(mp.ControlledBy.ToString(), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(mpChanged.ControlledBy.ToString(), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Setpoint torque":
                        Assert.AreEqual(mp.SetPointTorque.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(mpChanged.SetPointTorque.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Tolerance class torque":
                        Assert.AreEqual(mp.ToleranceClassTorque.Name, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(mpChanged.ToleranceClassTorque.Name, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Minimum torque":
                        Assert.AreEqual(mp.MinTorque.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(mpChanged.MinTorque.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Maximum torque":
                        Assert.AreEqual(mp.MaxTorque.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(mpChanged.MaxTorque.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Threshold":
                        Assert.AreEqual(mp.ThresholdTorque.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(mpChanged.ThresholdTorque.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Setpoint angle":
                        Assert.AreEqual(mp.SetPointAngle.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(mpChanged.SetPointAngle.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Tolerance class angle":
                        Assert.AreEqual(mp.ToleranceClassAngle.Name, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(mpChanged.ToleranceClassAngle.Name, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Minimum angle":
                        Assert.AreEqual(mp.MinAngle.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(mpChanged.MinAngle.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Maximum angle":
                        Assert.AreEqual(mp.MaxAngle.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(mpChanged.MaxAngle.ToString(currentCulture), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Cost center":
                        //Eingabe ist auf 15 Zeichen beschränkt
                        if(mp.ConfigurableField.Length <= 15)
                        {
                            Assert.AreEqual(mp.ConfigurableField, item.FindElementsByClassName("TextBlock")[2].Text);
                        }
                        else
                        {
                            Assert.AreEqual(mp.ConfigurableField.Substring(0, 15), item.FindElementsByClassName("TextBlock")[2].Text);
                        }
                        if(mpChanged.ConfigurableField.Length <= 15)
                        {
                            Assert.AreEqual(mpChanged.ConfigurableField, item.FindElementsByClassName("TextBlock")[3].Text);
                        }
                        else
                        {
                            Assert.AreEqual(mpChanged.ConfigurableField.Substring(0, 15), item.FindElementsByClassName("TextBlock")[3].Text);
                        }
                        break;
                    case "Category":
                        Assert.AreEqual(mp.ConfigurableField2, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(mpChanged.ConfigurableField2, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Documentation":
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(mp.ConfigurableField3), item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(TestHelper.GetBoolValueAsVerifyString(mpChanged.ConfigurableField3), item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    case "Comment":
                        //TODO einkommentieren wenn Kommentare umgesetzt sind
                        //Assert.AreEqual(mp.Comment, item.FindElementsByClassName("TextBlock")[2].Text);
                        Assert.AreEqual(mpChanged.Comment, item.FindElementsByClassName("TextBlock")[3].Text);
                        break;
                    default:
                        Assert.IsTrue(false, string.Format("Case '{0}' not implemented", changeTypeText));
                        break;
                }
                i++;
            }
        }
        public static void CreateFolder(WindowsDriver<WindowsElement> driver, List<string> folderList, AppiumWebElement treeView)
        {
            var addFolderBtn = TestHelper.FindElementWithWait(AiStringHelper.Mp.AddFolder, driver);
            List<string> partList;
            for (int i = 1; i <= folderList.Count; i++)
            {
                partList = folderList.GetRange(0, i);
                var mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, driver);
                if (TestHelper.GetNode(driver, mpTreeviewRootNode, partList) == null)
                {
                    partList = folderList.GetRange(0, i - 1);

                    //Beim RootFolder gibts keinen ParentFolder
                    if (i > 1)
                    {
                        mpTreeviewRootNode = TestHelper.FindElementWithWait(AiStringHelper.Mp.MpTreeViewRoot, driver);
                        Assert.IsNotNull(mpTreeviewRootNode, "mpTreeviewRootNode ist null in CreateFolder");
                        var parentFolder = TestHelper.GetNode(driver, mpTreeviewRootNode, partList);
                        Assert.IsNotNull(parentFolder, "parentFolder ist null in CreateFolder");
                        ClickMpFolder(parentFolder, treeView, driver);
                    }
                    addFolderBtn.Click();

                    var addFolderDialog = TestHelper.FindElementWithWait(AiStringHelper.Mp.AddMpFolderDialog.Dialog, driver);
                    var addFolderDialogFolder = addFolderDialog.FindElementByAccessibilityId(AiStringHelper.Mp.AddMpFolderDialog.Folder);
                    var addFolderDialogOkBtn = addFolderDialog.FindElementByAccessibilityId(AiStringHelper.Mp.AddMpFolderDialog.Ok);

                    TestHelper.SendKeysConverted(addFolderDialogFolder, folderList[i - 1]);
                    addFolderDialogOkBtn.Click();
                    TestHelper.AssertAndCloseToastNotification(DesktSession, ValidationStringHelper.ToastNotificationStrings.ActionSuccess);
                }
            }
        }
    }
}