using System;
using System.Collections.Generic;
using System.Globalization;
using Client.Core.Entities;
using Core.Entities;
using Core.Entities.ToolTypes;
using Core.Enums;
using Core.UseCases.Communication.DataGate;

namespace Core.UseCases.Communication
{
    public class SemanticModelFactory : ISemanticModelFactory
    {
        private readonly ITreePathBuilder _treePathBuilder;

        public SemanticModelFactory(ITreePathBuilder treePathBuilder)
        {
            _treePathBuilder = treePathBuilder;
        }

        public SemanticModel Convert(TestEquipment testEquipment, List<LocationToolAssignment> route, (double cm, double cmk) cmCmk, DateTime localNow, TestType testType)
        {
            var dataGate = new Container(new ElementName("sDataGate"));

            var header = new Container(new ElementName("Header"));
            GenerateDefaultHeaderFields(testEquipment, localNow, header, "CMD_PROG");
            GenerateAdditionalHeaderFields(cmCmk, header);
            dataGate.Add(header);

            var communication = new Container(new ElementName("Communication"));
            GenerateCommunicationFields(communication);
            dataGate.Add(communication);

            var routeList = new Container(new ElementName("RouteList"));
            routeList.Add(new Content(new ElementName("RouteCount"), "1"));

            var routeElement = new Container(new ElementName("Route"));
            routeElement.Add(new Content(new ElementName("RoutePos"), "1"));
            routeElement.Add(new Content(new ElementName("RouteId"), "0"));
            routeElement.Add(new Content(new ElementName("RouteName"), "QS-Torque"));
            routeElement.Add(new Content(new ElementName("AskForIdent"), "0"));
            routeElement.Add(new Content(new ElementName("AskForCurve"), "0"));
            routeElement.Add(new Content(new ElementName("TestCount"), route.Count.ToString()));
            var testItemCounter = 0;
            foreach (var item in route)
            {
                var testItem = new Container(new ElementName("TestItem"));
                testItem.Add(new Content(new ElementName("TestPos"), $"{(++testItemCounter).ToString()}"));
                testItem.Add(new Content(new ElementName("TestId1"), item.Id.ToLong().ToString()));
                testItem.Add(new Content(new ElementName("TestId2"), "0"));
                testItem.Add(new Content(new ElementName("TestId3"), item.AssignedLocation.Id.ToLong().ToString()));
                testItem.Add(new Content(new ElementName("UserId"), item.AssignedLocation.Number.ToDefaultString()));
                testItem.Add(new Content(new ElementName("UserName"), item.AssignedLocation.Description.ToDefaultString()));
                testItem.Add(new Content(new ElementName("Path"), _treePathBuilder.GetTreePath(item.AssignedLocation, " - ")));
                testItem.Add(new HiddenContent(new ElementName("ControlDimension"), ((int)item.TestParameters.ControlledBy).ToString()));
                testItem.Add(new HiddenContent(new ElementName("DimensionTorqueNominal"), item.TestParameters.SetPointTorque.Nm.ToString("F3", CultureInfo.InvariantCulture)));
                // TODO: DimensionTorqueMin und Max via tolerance class
                testItem.Add(new HiddenContent(new ElementName("DimensionTorqueMin"), item.TestParameters.MinimumTorque.Nm.ToString("F3", CultureInfo.InvariantCulture)));
                testItem.Add(new HiddenContent(new ElementName("DimensionTorqueMax"), item.TestParameters.MaximumTorque.Nm.ToString("F3", CultureInfo.InvariantCulture)));
                testItem.Add(new HiddenContent(new ElementName("DimensionAngleNominal"), item.TestParameters.SetPointAngle.Degree.ToString("F3", CultureInfo.InvariantCulture)));
                // TODO: DimensionAngleMin und Max via tolerance class
                testItem.Add(new HiddenContent(new ElementName("DimensionAngleMin"), item.TestParameters.MinimumAngle.Degree.ToString("F3", CultureInfo.InvariantCulture)));
                testItem.Add(new HiddenContent(new ElementName("DimensionAngleMax"), item.TestParameters.MaximumAngle.Degree.ToString("F3", CultureInfo.InvariantCulture)));
                testItem.Add(new Content(new ElementName("Unit1Id"), ""));
                testItem.Add(new Content(new ElementName("Unit1"), ""));
                testItem.Add(new Content(new ElementName("Nom1"), ""));
                testItem.Add(new Content(new ElementName("Min1"), ""));
                testItem.Add(new Content(new ElementName("Max1"), ""));
                testItem.Add(new Content(new ElementName("Unit2Id"), ""));
                testItem.Add(new Content(new ElementName("Unit2"), ""));
                testItem.Add(new Content(new ElementName("Nom2"), ""));
                testItem.Add(new Content(new ElementName("Min2"), ""));
                testItem.Add(new Content(new ElementName("Max2"), ""));
                testItem.Add(new Content(new ElementName("TestCycles"), item.GetTestLevel(testType).SampleNumber.ToString()));
                testItem.Add(new Content(new ElementName("TestCyclesCm"), item.GetTestLevel(TestType.Mfu).SampleNumber.ToString())); // TODO: in v7 this has logic if no mfu count is set (this is 0)... do we need that?
                // TODO: TestMethod as rewriter
                var testMethodConverter = new TestMethodConverter();
                item.AssignedTool.ToolModel.ModelType.Accept(testMethodConverter);
                testItem.Add(new Content(new ElementName("TestMethod"), testMethodConverter.Result));
                testItem.Add(new Content(new ElementName("TestTypeId"), "0")); // TODO: check if this is needed
                testItem.Add(new Content(new ElementName("TestTypeName"), "")); // TODO: check if this is needed
                testItem.Add(new Content(new ElementName("AskForIdent"), "0"));
                testItem.Add(new Content(new ElementName("AskForCurve"), "0"));
                testItem.Add(new Content(new ElementName("ToolId"), item.AssignedTool.Id.ToLong().ToString()));
                testItem.Add(new Content(new ElementName("ToolType"), "1")); // für atlas immer 1?
                testItem.Add(new Content(new ElementName("ToolName"), item.AssignedTool.InventoryNumber?.ToDefaultString()));
                testItem.Add(new Content(new ElementName("ModelName"), item.AssignedTool.ToolModel.Description?.ToDefaultString()));
                testItem.Add(new Content(new ElementName("SupplierName"), item.AssignedTool.ToolModel.Manufacturer.Name.ToDefaultString()));
                testItem.Add(new Content(new ElementName("AdapterId"), "0"));
                testItem.Add(new Content(new ElementName("TransPos"), "1"));
                testItem.Add(new Content(new ElementName("TransId"), "0"));
                testItem.Add(new Content(new ElementName("WiMin"), "0.0"));
                testItem.Add(new Content(new ElementName("WiMax"), "0.0"));
                testItem.Add(new Content(new ElementName("StartMea"), item.TestTechnique.CycleStart.ToString("F2", CultureInfo.InvariantCulture)));
                testItem.Add(new Content(new ElementName("StartDeg"), item.TestTechnique.StartFinalAngle.ToString("F2", CultureInfo.InvariantCulture)));
                testItem.Add(new Content(new ElementName("TurnCW"), "1")); //TODO: TurnCW can not be configured right now
                testItem.Add(new Content(new ElementName("TreeParentId"), "0"));
                testItem.Add(new Content(new ElementName("Info1"), ""));
                // TODO: do i need DeadTime??? looks like schatz field.
                // TODO: sta6000 fields
                testItem.Add(new Content(new ElementName("AC_CycleComplete"), item.TestTechnique.CycleComplete.ToString("F2", CultureInfo.InvariantCulture)));
                testItem.Add(new Content(new ElementName("AC_SlipTorque"), item.TestTechnique.SlipTorque.ToString("F2", CultureInfo.InvariantCulture)));
                testItem.Add(new Content(new ElementName("AC_TorqueCoefficient"), item.TestTechnique.TorqueCoefficient.ToString("F2", CultureInfo.InvariantCulture)));
                // AC_Threshold
                testItem.Add(new Content(new ElementName("AC_EndTime"), item.TestTechnique.EndCycleTime.ToString("F2", CultureInfo.InvariantCulture)));
                testItem.Add(new Content(new ElementName("AC_MeasureDelayTime"), item.TestTechnique.MeasureDelayTime.ToString("F2", CultureInfo.InvariantCulture)));
                testItem.Add(new Content(new ElementName("AC_ResetTime"), item.TestTechnique.ResetTime.ToString("F2", CultureInfo.InvariantCulture)));
                testItem.Add(new Content(new ElementName("AC_FilterFreq"), item.TestTechnique.FilterFrequency.ToString("F2", CultureInfo.InvariantCulture)));
                testItem.Add(item.TestParameters.ControlledBy == LocationControlledBy.Torque // TODO: convert to rewriter
                    ? new Content(new ElementName("AC_MeasureTorqueAt"), "0")
                    : new Content(new ElementName("AC_MeasureTorqueAt"), "1"));
                if (item.TestParameters.ControlledBy == LocationControlledBy.Torque) // TODO: convert to rewriter
                {
                    testItem.Add(new Content(new ElementName("AC_CmCmkSpcTestType"), item.TestTechnique.MustTorqueAndAngleBeInLimits ? "24" : "8"));
                }
                else
                {
                    testItem.Add(new Content(new ElementName("AC_CmCmkSpcTestType"), item.TestTechnique.MustTorqueAndAngleBeInLimits ? "32" : "16"));
                }
                testItem.Add(new Content(new ElementName("AC_MinimumCm"), cmCmk.cm.ToString("F3", CultureInfo.InvariantCulture)));
                testItem.Add(new Content(new ElementName("AC_MinimumCmk"), cmCmk.cmk.ToString("F3", CultureInfo.InvariantCulture)));
                testItem.Add(new Content(new ElementName("AC_MinimumCmAngle"), cmCmk.cm.ToString("F3", CultureInfo.InvariantCulture)));
                testItem.Add(new Content(new ElementName("AC_MinimumCmkAngle"), cmCmk.cmk.ToString("F3", CultureInfo.InvariantCulture)));
                testItem.Add(new Content(new ElementName("AC_TestType"), "3")); // TODO: spcStandalone == 3; cmcmk == 0; make choosable
                testItem.Add(new Content(new ElementName("AC_SubgroupSize"), testType == TestType.Chk ? item.GetTestLevel(TestType.Chk).SampleNumber.ToString() : "1"));
                testItem.Add(new Content(new ElementName("AC_SubgroupFrequency"), "1"));
                testItem.Add(new Content(new ElementName("AC_MinimumPulse"), item.TestTechnique.MinimumPulse.ToString()));
                testItem.Add(new Content(new ElementName("AC_MaximumPulse"), item.TestTechnique.MaximumPulse.ToString()));
                // TODO: TestLimits?
                // TODO: QstLimits?
                // TODO: ChangeScrewAt?
                routeElement.Add(testItem);
            }
            routeList.Add(routeElement);

            dataGate.Add(routeList);

            return new SemanticModel(dataGate);
        }

        private static void GenerateAdditionalHeaderFields((double cm, double cmk) cmCmk, Container header)
        {
            header.Add(new Content(new ElementName("UseLogOn"), "0"));
            header.Add(new Content(new ElementName("UseErrCode"), "0"));
            header.Add(new Content(new ElementName("PCRule1"), "0"));
            header.Add(new Content(new ElementName("PCRule2"), "0"));
            header.Add(new Content(new ElementName("PCRule3"), "0"));
            header.Add(new Content(new ElementName("PCRule4"), "0"));
            header.Add(new Content(new ElementName("PCRule5"), "0"));
            header.Add(new Content(new ElementName("PCRule6"), "0"));
            header.Add(new Content(new ElementName("PCRule7"), "0"));
            header.Add(new Content(new ElementName("CustomerName"), ""));
            header.Add(new Content(new ElementName("MinCp"), cmCmk.cm.ToString("F2", CultureInfo.InvariantCulture)));
            header.Add(new Content(new ElementName("MinCpk"), cmCmk.cmk.ToString("F2", CultureInfo.InvariantCulture)));
            header.Add(new Content(new ElementName("AskForIdent"), "0"));
            header.Add(new Content(new ElementName("QuitTest"), "0"));
            header.Add(new Content(new ElementName("AllowDeleteTest"), "0"));
            header.Add(new Content(new ElementName("LooseCheck"), "0"));
            header.Add(new Content(new ElementName("UseFileId"), "0"));
        }

        private static void GenerateCommunicationFields(Container communication)
        {
            communication.Add(new Content(new ElementName("TxMode"), "Serial"));
            communication.Add(new Content(new ElementName("ComPort"), "1"));
            communication.Add(new Content(new ElementName("ComBaud"), "9600"));
            communication.Add(new Content(new ElementName("IP_Host"), ""));
            communication.Add(new Content(new ElementName("IP_Port"), "0"));
            communication.Add(new Content(new ElementName("FTPUser"), ""));
            communication.Add(new Content(new ElementName("FTP_Pwd"), ""));
        }

        public SemanticModel ReadCommand(TestEquipment testEquipment, DateTime timestamp)
        {
            return GenerateSimpleCommand(testEquipment, timestamp, "CMD_READ");
        }

        public SemanticModel ClearCommand(TestEquipment testEquipment, DateTime timestamp)
        {
            return GenerateSimpleCommand(testEquipment, timestamp, "CMD_CLEAR");
        }

        private static SemanticModel GenerateSimpleCommand(
            TestEquipment testEquipment,
            DateTime timestamp,
            string commandString)
        {
            var dataGate = new Container(new ElementName("sDataGate"));

            var header = new Container(new ElementName("Header"));
            GenerateDefaultHeaderFields(testEquipment, timestamp, header, commandString);
            dataGate.Add(header);

            var communication = new Container(new ElementName("Communication"));
            GenerateCommunicationFields(communication);
            dataGate.Add(communication);

            return new SemanticModel(dataGate);
        }

        private static void GenerateDefaultHeaderFields(TestEquipment testEquipment, DateTime localNow, Container header, string instruction)
        {
            header.Add(new Content(new ElementName("dgVersion"), "7.0"));
            header.Add(new Content(new ElementName("qstVersion"), "8.0.0.0"));
            header.Add(new Content(new ElementName("qstLevel"), "500000")); // TODO: check if test equipment can handle those numbers
            header.Add(new Content(new ElementName("source"), "QST"));
            header.Add(new Content(new ElementName("destination"), testEquipment?.SerialNumber.ToDefaultString()));
            header.Add(new Content(new ElementName("fileDate"), localNow.ToString("yyyy-MM-dd")));
            header.Add(new Content(new ElementName("fileTime"), localNow.ToString("HH:mm:ss")));
            header.Add(new Content(new ElementName("DevSerNo"), testEquipment?.SerialNumber.ToDefaultString()));
            header.Add(new Content(new ElementName("DevInvNo"), ""));
            header.Add(new Content(new ElementName("Instruction"), instruction));
            header.Add(new Content(new ElementName("ResultFile"), testEquipment?.ResultFilePath()));
            header.Add(new Content(new ElementName("StatusFile"), testEquipment?.StatusFilePath()));
        }

        public SemanticModel Convert(TestEquipment testEquipment, List<Location> locations, List<ProcessControlCondition> processControls, DateTime localNow)
        {
            var datagate = new Container(new ElementName("sDataGate"));

            var header = new Container(new ElementName("Header"));
            GenerateDefaultHeaderFields(testEquipment, localNow, header, "");
            GenerateAdditionalHeaderFields((0, 0), header);
            datagate.Add(header);

            var communication = new Container(new ElementName("Communication"));
            datagate.Add(communication);

            var routeList = new Container(new ElementName("RouteList"));
            routeList.Add(new Content(new ElementName("RouteCount"), "1"));

            var routeElement = new Container(new ElementName("Route"));
            routeElement.Add(new Content(new ElementName("RoutePos"), "1"));
            routeElement.Add(new Content(new ElementName("RouteId"), "0"));
            routeElement.Add(new Content(new ElementName("RouteName"), "QS-Torque"));
            routeElement.Add(new Content(new ElementName("AskForIdent"), "0"));
            routeElement.Add(new Content(new ElementName("AskForCurve"), "0"));
            routeElement.Add(new Content(new ElementName("TestCount"), processControls.Count.ToString()));

            var locationLookupTable = new Dictionary<long, Location>();
            foreach(var locationEntry in locations)
            {
                locationLookupTable.TryAdd(locationEntry.Id.ToLong(), locationEntry);
            }

            var testItemCounter = 0;
            foreach(var processControlCondition in processControls)
            {
                var testItem = new Container(new ElementName("TestItem"));
                testItem.Add(new Content(new ElementName("TestPos"), $"{(++testItemCounter).ToString()}"));
                testItem.Add(new Content(new ElementName("TestId1"), processControlCondition.Location.Id.ToLong().ToString()));
                // testid2?
                testItem.Add(new Content(new ElementName("TestId3"), processControlCondition.Location.Id.ToLong().ToString()));
                testItem.Add(new Content(new ElementName("UserId"), locationLookupTable[processControlCondition.Location.Id.ToLong()].Number.ToDefaultString()));
                testItem.Add(new Content(new ElementName("UserName"), locationLookupTable[processControlCondition.Location.Id.ToLong()].Description.ToDefaultString()));

                routeElement.Add(testItem);
            }

            routeList.Add(routeElement);

            datagate.Add(routeList);

            return new SemanticModel(datagate);
        }

        private class TestMethodConverter: IAbstractToolTypeVisitor
        {
            public void Visit(ClickWrench toolType)
            {
                Result = "18";
            }

            public void Visit(ECDriver toolType)
            {
                Result = "19";
            }

            public void Visit(General toolType)
            {
                Result = "19";
            }

            public void Visit(MDWrench toolType)
            {
                Result = "13";
            }

            public void Visit(ProductionWrench toolType)
            {
                Result = "13";
            }

            public void Visit(PulseDriver toolType)
            {
                Result = "14";
            }

            public void Visit(PulseDriverShutOff toolType)
            {
                Result = "14";
            }

            public string Result = "";
        }
    }
}