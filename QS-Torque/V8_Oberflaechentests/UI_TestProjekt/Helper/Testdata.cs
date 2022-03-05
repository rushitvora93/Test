using System;
using System.Collections.Generic;
using UI_TestProjekt.TestModel;

namespace UI_TestProjekt.Helper
{
    public static class Testdata
    {
        public const string MpRootNode = "Root";

        public const string ConfigFieldTool1 = "TestConfigurableFieldTool1";
        public const string ConfigFieldTool2 = "TestConfigurableFieldTool2";
        public const string ConfigFieldToolAddHelper1 = "2345bk";
        public const string ConfigFieldToolChangedHelper2 = "TestConfigurableFieldTool2(Neu)";

        public const string ConstructionType1 = "TestConstructionType1";
        public const string ConstructionType2 = "TestConstructionType2";
        public const string ConstructionTypeAddHelper1 = "2345bk";
        public const string ConstructionTypeChangedHelper2 = "TestConstructionType2(Neu)";

        public const string CostCenter1 = "TestCostCenter1";
        public const string CostCenter2 = "TestCostCenter2";
        public const string CostCenterAddHelper1 = "2345bk";
        public const string CostCenterChangedHelper2 = "TestCostCenter2(Neu)";

        public const string DriveSize1 = "TestDriveSize1";
        public const string DriveSize2 = "TestDriveSize2";
        public const string DriveSizeAddHelper1 = "2345bk";
        public const string DriveSizeChangedHelper2 = "TestDriveSize2(Neu)";

        public const string DriveType1 = "TestDriveType1";
        public const string DriveType2 = "TestDriveType2";
        public const string DriveTypeAddHelper1 = "2345bk";
        public const string DriveTypeChangedHelper2 = "TestDriveType2(Neu)";

        public const string ReasonToolChange1 = "TestReasonToolChange1";
        public const string ReasonToolChange2 = "TestReasonToolChange2";
        public const string ReasonToolChangeAddHelper1 = "2345bk";
        public const string ReasonToolChangeChangedHelper2 = "TestReasonToolChange2(Neu)";

        public const string ShutOff1 = "TestShutOff1";
        public const string ShutOff2 = "TestShutOff2";
        public const string ShutOffAddHelper1 = "2345bk";
        public const string ShutOffChangedHelper2 = "TestShutOff2(Neu)";

        public const string Status1 = "TestStatus1";
        public const string Status2 = "TestStatus2";
        public const string StatusAddHelper1 = "2345bk";
        public const string StatusChangedHelper2 = "TestStatus2(Neu)";

        public const string SwitchOff1 = "TestSwitchOff1";
        public const string SwitchOff2 = "TestSwitchOff2";
        public const string SwitchOffAddHelper1 = "2345bk";
        public const string SwitchOffChangedHelper2 = "TestSwitchOff2(Neu)";

        public const string ToolType1 = "TestToolType1";
        public const string ToolType2 = "TestToolType2";
        public const string ToolTypeAddHelper1 = "2345bk";
        public const string ToolTypeChangedHelper2 = "TestToolType2(Neu)";

        public const string ToolUsage1 = "TestToolUsage1";
        public const string ToolUsage2 = "TestToolUsage2";
        public const string ToolUsageAddHelper1 = "2345bk";
        public const string ToolUsageChangedHelper2 = "TestToolUsage2(Neu)";

        public static Manufacturer GetManufacturer1()
        {
            return new Manufacturer("TestManufacturer1", "Tester1", "1234 - 56789", "1234 - 56780", "TestingStreet", "5", "98765", "TestingCity", "Germany", "Testkommentar");
        }
        public static Manufacturer GetManufacturer2()
        {
            return new Manufacturer("TestManufacturer2", "Tester2", "987 - 65432", "987 - 65430", "TestingAvenue", "7", "12345", "TestingTown", "USA", "TestComment");
        }
        public static Manufacturer GetManufacturerChanged1()
        {
            return new Manufacturer("TestManufacturer1a", "Tester1b", "1234 - 56789c", "1234 - 56780d", "TestingStreete", "50", "98761", "TestingCityh", "Germanyi", "Testkommentarj");
        }
        public static Manufacturer GetManufacturerChanged2()
        {
            return new Manufacturer("TestManufacturer2Blub", "Tester2", "987 - 65432", "987 - 65430", "TestingAvenue", "7", "12345", "TestingTown", "USA", "TestComment");
        }

        public static ToleranceClass GetToleranceClass1()
        {
            return new ToleranceClass("TolClass1", true, true, 5, 5);
        }
        public static ToleranceClass GetToleranceClass2()
        {
            return new ToleranceClass("TolClass2", true, false, 6, 7);
        }
        public static ToleranceClass GetToleranceClass3()
        {
            return new ToleranceClass("TolClass3", false, true, 10, 10);
        }
        public static ToleranceClass GetToleranceClass4()
        {
            return new ToleranceClass("TolClass4", false, false, 10, 15);
        }
        public static ToleranceClass GetToleranceClassChanged1()
        {
            return new ToleranceClass("TolClass1_Neu", true, false, 5, 8);
        }
        public static ToleranceClass GetToleranceClassChanged2()
        {
            return new ToleranceClass("TolClassBlub", false, false, 5, 6);
        }
        public static ToleranceClass GetMp2ToleranceClass()
        {
            return new ToleranceClass("TestTCFuerzweitenMP", true, false, 10, 20);
        }
        public static ToleranceClass GetMp2ToleranceClassAngle()
        {
            return new ToleranceClass("TestTCFuerzweitenMPAngle", true, true, 10, 10);
        }
        public static ToleranceClass GetMp2ToleranceClassChanged()
        {
            return new ToleranceClass("TestTCFuerzweitenMPAngleChanged", false, true, 7, 5);
        }
        public static ToleranceClass GetMpTreeToleranceClass()
        {
            return new ToleranceClass("TestTCFuerzweitenTreeMP", true, false, 10, 20);
        }
        public static ToleranceClass GetMpTreeToleranceClassAngle()
        {
            return new ToleranceClass("TestTCFuerzweitenTreeMPAngle", true, true, 10, 10);
        }
        public static ToleranceClass GetMpToolAllocationToleranceClassAngle()
        {
            return new ToleranceClass("10 abs", false, true, 10, 10);
        }
        public static ToleranceClass GetMpTemplateToleranceClassAngle()
        {
            return new ToleranceClass("5 abs", false, true, 5, 5);
        }
        public static ToleranceClass GetMpTemplateToleranceClassTorque()
        {
            return new ToleranceClass("+5% rel -4%", true, false, 4, 5);
        }
        public static ToleranceClass GetToleranceclassTorqueForMpUndoChanges()
        {
            return new ToleranceClass("rel -5%, +3%", true, false, 5, 3);
        }
        public static ToleranceClass GetToleranceclassAngleForMpUndoChanges()
        {
            return new ToleranceClass("abs -8, +7", false, false, 8, 7);
        }

        public static ToolModel GetToolModel1()
        {
            return new ToolModel(
                "TestToolModel (CSP)",
                Manufacturer.DefaultManufacturer.CSP,
                ToolModel.ToolModelTypeStrings.General,
                "",
                2.1,
                3.4,
                6.1,
                60,
                4,
                20.5,
                "Tool Type 1",
                5,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolModel2()
        {
            return new ToolModel(
                "TestToolModel (SCS)",
                Manufacturer.DefaultManufacturer.SCS,
                ToolModel.ToolModelTypeStrings.EcDriver,
                "",
                0,
                0,
                6,
                80,
                10,
                50,
                "Tool Type 2",
                4.7,
                "Switch Off 2",
                "Shut Off 2",
                "Drive Size 2",
                "Drive Type 2",
                "Construction Type 2",
                0,
                0);
        }
        public static ToolModel GetToolModel3()
        {
            return new ToolModel(
                "TestToolModel (Atlas Copco)",
                Manufacturer.DefaultManufacturer.AtlasCopco,
                ToolModel.ToolModelTypeStrings.ClickWrench,
                ToolModel.ToolModelClassStrings.ClickWrenchClass.BeamTypeTorqueWrenchWithScale,
                0,
                0,
                0,
                0,
                0.5,
                50,
                "Tool Type 2",
                4.7,
                "Switch Off 2",
                "Shut Off 2",
                "Drive Size 2",
                "Drive Type 2",
                "Construction Type 2",
                0,
                0);
        }
        public static ToolModel GetToolModel4()
        {
            return new ToolModel(
                "TestToolModel (Gedore)",
                Manufacturer.DefaultManufacturer.Gedore,
                ToolModel.ToolModelTypeStrings.ProductionWrench,
                ToolModel.ToolModelClassStrings.Md_ProductionWrenchClass.DriverWithDialIndicator,
                0,
                0,
                0,
                0,
                1,
                100,
                "Tool Type 2",
                4.7,
                "Switch Off 2",
                "Shut Off 2",
                "Drive Size 2",
                "Drive Type 2",
                "Construction Type 2",
                0,
                0);
        }
        public static ToolModel GetToolModel5()
        {
            return new ToolModel(
                "TestToolModel (Schatz)",
                Manufacturer.DefaultManufacturer.Schatz,
                ToolModel.ToolModelTypeStrings.MdWrench,
                ToolModel.ToolModelClassStrings.Md_ProductionWrenchClass.WrenchElectronic,
                0,
                0,
                0,
                0,
                30,
                100,
                "Tool Type 2",
                4.7,
                "Switch Off 2",
                "Shut Off 2",
                "Drive Size 2",
                "Drive Type 2",
                "Construction Type 2",
                0,
                0);
        }
        public static ToolModel GetToolModelChanged1()
        {
            ToolModel cspToolModelChanged = new ToolModel();

            cspToolModelChanged.Description = "TestToolModel (Nicht CSP) geändert";
            cspToolModelChanged.Manufacturer = Manufacturer.DefaultManufacturer.AtlasCopco;
            cspToolModelChanged.ToolModelType = ToolModel.ToolModelTypeStrings.MdWrench;
            cspToolModelChanged.ToolModelClass = ToolModel.ToolModelClassStrings.Md_ProductionWrenchClass.WrenchElectronic;
            cspToolModelChanged.MinPow = 22;
            cspToolModelChanged.MaxPow = 33;
            cspToolModelChanged.ToolType = "Tool Type 2";
            cspToolModelChanged.Weight = 1.2;
            cspToolModelChanged.SwitchOff = "Switch Off 2";
            cspToolModelChanged.ShutOff = "Shut Off 2";
            cspToolModelChanged.DriveSize = "Drive Size 2";
            cspToolModelChanged.DriveType = "Drive Type 2";
            cspToolModelChanged.ConstructionType = "Construction Type 3";

            //Werte aus altem Modell übernehmen weil alter Wert in der Liste immer noch vorhanden ist
            cspToolModelChanged.AirConsumption = GetToolModel1().AirConsumption;
            cspToolModelChanged.AirPressure = GetToolModel1().AirPressure;
            cspToolModelChanged.BattVoltage = GetToolModel1().BattVoltage;
            cspToolModelChanged.MaxRotSpeed = GetToolModel1().MaxRotSpeed;
            return cspToolModelChanged;
        }
        public static ToolModel GetToolModelChanged2()
        {
            ToolModel scsToolModelChanged = new ToolModel();
            scsToolModelChanged.Description = "TestToolModel (SCS) geändert";
            scsToolModelChanged.Manufacturer = Manufacturer.DefaultManufacturer.SCS;
            scsToolModelChanged.ToolModelType = ToolModel.ToolModelTypeStrings.PulseDriver;
            scsToolModelChanged.AirPressure = 4.7;
            scsToolModelChanged.AirConsumption = 2.3;
            scsToolModelChanged.MaxRotSpeed = 90;
            scsToolModelChanged.MinPow = 15;
            scsToolModelChanged.MaxPow = 35;
            scsToolModelChanged.ToolType = "Tool Type 3";
            scsToolModelChanged.Weight = 5;
            scsToolModelChanged.SwitchOff = "Standard";
            scsToolModelChanged.ShutOff = "Standard";
            scsToolModelChanged.DriveSize = "Drive Size 3";
            scsToolModelChanged.DriveType = "Drive Type 4";
            scsToolModelChanged.ConstructionType = "Construction Type 2";

            //Werte aus altem Modell übernehmen weil alter Wert in der Liste immer noch vorhanden ist
            scsToolModelChanged.BattVoltage = GetToolModel2().BattVoltage;
            return scsToolModelChanged;
        }
        public static ToolModel GetToolModelChanged3()
        {
            ToolModel acToolModelChanged = new ToolModel();
            acToolModelChanged.Description = "TestToolModel (AC) geändert";
            acToolModelChanged.Manufacturer = Manufacturer.DefaultManufacturer.AtlasCopco;
            acToolModelChanged.ToolModelType = ToolModel.ToolModelTypeStrings.ProductionWrench;
            acToolModelChanged.ToolModelClass = ToolModel.ToolModelClassStrings.Md_ProductionWrenchClass.DriverWithDialIndicator;
            acToolModelChanged.MinPow = 0.5;
            acToolModelChanged.MaxPow = 60;
            acToolModelChanged.ToolType = "Tool Type 3";
            acToolModelChanged.Weight = 5;
            acToolModelChanged.SwitchOff = "Standard";
            acToolModelChanged.ShutOff = "Standard";
            acToolModelChanged.DriveSize = "Drive Size 3";
            acToolModelChanged.DriveType = "Drive Type 4";
            acToolModelChanged.ConstructionType = "Construction Type 2";

            return acToolModelChanged;
        }
        public static ToolModel GetToolModelChanged4()
        {
            ToolModel gedoreToolModelChanged = new ToolModel();
            gedoreToolModelChanged.Description = "TestToolModel (Gedore) geändert";
            gedoreToolModelChanged.Manufacturer = Manufacturer.DefaultManufacturer.Gedore;
            gedoreToolModelChanged.ToolModelType = ToolModel.ToolModelTypeStrings.ClickWrench;
            gedoreToolModelChanged.ToolModelClass = ToolModel.ToolModelClassStrings.ClickWrenchClass.WrenchConfScale;
            gedoreToolModelChanged.MinPow = 2;
            gedoreToolModelChanged.MaxPow = 200;
            gedoreToolModelChanged.ToolType = "Tool Type 3";
            gedoreToolModelChanged.Weight = 5;
            gedoreToolModelChanged.SwitchOff = "Standard";
            gedoreToolModelChanged.ShutOff = "Standard";
            gedoreToolModelChanged.DriveSize = "Drive Size 3";
            gedoreToolModelChanged.DriveType = "Drive Type 4";
            gedoreToolModelChanged.ConstructionType = "Construction Type 2";

            return gedoreToolModelChanged;
        }
        public static ToolModel GetToolModelChanged5()
        {
            ToolModel schatzToolModelChanged = new ToolModel();
            schatzToolModelChanged.Description = "TestToolModel (Schatz) geändert";
            schatzToolModelChanged.Manufacturer = Manufacturer.DefaultManufacturer.Schatz;
            schatzToolModelChanged.ToolModelType = ToolModel.ToolModelTypeStrings.ProductionWrench;
            schatzToolModelChanged.ToolModelClass = ToolModel.ToolModelClassStrings.Md_ProductionWrenchClass.WrenchWithDialIndicator;
            schatzToolModelChanged.MinPow = 30;
            schatzToolModelChanged.MaxPow = 100;
            schatzToolModelChanged.ToolType = "Tool Type 3";
            schatzToolModelChanged.Weight = 5;
            schatzToolModelChanged.SwitchOff = "Standard";
            schatzToolModelChanged.ShutOff = "Standard";
            schatzToolModelChanged.DriveSize = "Drive Size 3";
            schatzToolModelChanged.DriveType = "Drive Type 4";
            schatzToolModelChanged.ConstructionType = "Construction Type 2";

            return schatzToolModelChanged;
        }
        
        public static ToolModel GetToolToolModel1()
        {
            return new ToolModel(
                "TestToolModel ToolTest(CSP)",
                Manufacturer.DefaultManufacturer.CSP,
                ToolModel.ToolModelTypeStrings.General,
                "",
                2.1,
                3.4,
                6.1,
                60,
                4,
                20.5,
                "Tool Type 1",
                5,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolToolModel2()
        {
            return new ToolModel(
                "TestToolModel ToolTest(SCS)",
                Manufacturer.DefaultManufacturer.SCS,
                ToolModel.ToolModelTypeStrings.EcDriver,
                "",
                0,
                0,
                6,
                80,
                10,
                50,
                "Tool Type 2",
                4.7,
                "Switch Off 2",
                "Shut Off 2",
                "Drive Size 2",
                "Drive Type 2",
                "Construction Type 2",
                0,
                0);
        }
        public static ToolModel GetToolToolModelChanged1()
        {
            ToolModel cspToolModelChanged = new ToolModel();
            cspToolModelChanged.Description = "TestToolModel TT (Nicht CSP) geändert";
            cspToolModelChanged.Manufacturer = Manufacturer.DefaultManufacturer.AtlasCopco;
            cspToolModelChanged.ToolModelType = ToolModel.ToolModelTypeStrings.MdWrench;
            cspToolModelChanged.ToolModelClass = ToolModel.ToolModelClassStrings.Md_ProductionWrenchClass.WrenchElectronic;
            cspToolModelChanged.MinPow = 22;
            cspToolModelChanged.MaxPow = 33;
            cspToolModelChanged.ToolType = "Tool Type 2";
            cspToolModelChanged.Weight = 1.2;
            cspToolModelChanged.SwitchOff = "Switch Off 2";
            cspToolModelChanged.ShutOff = "Shut Off 2";
            cspToolModelChanged.DriveSize = "Drive Size 2";
            cspToolModelChanged.DriveType = "Drive Type 2";
            cspToolModelChanged.ConstructionType = "Construction Type 3";

            //Werte aus altem Modell übernehmen weil alter Wert in der Liste immer noch vorhanden ist
            cspToolModelChanged.AirConsumption = GetToolToolModel1().AirConsumption;
            cspToolModelChanged.AirPressure = GetToolToolModel1().AirPressure;
            cspToolModelChanged.BattVoltage = GetToolToolModel1().BattVoltage;
            cspToolModelChanged.MaxRotSpeed = GetToolToolModel1().MaxRotSpeed;
            return cspToolModelChanged;
        }
        public static ToolModel GetMpToolAllocationToolModel1()
        {
            return new ToolModel(
                "TestToolModel MPTA (CSP) Neu",
                Manufacturer.DefaultManufacturer.CSP,
                ToolModel.ToolModelTypeStrings.General,
                "",
                2.1,
                3.4,
                6.1,
                60,
                4,
                20.5,
                "Tool Type 1",
                5,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }        
        public static ToolModel GetMpToolAllocationToolModel2()
        {
            return new ToolModel(
                "TestToolModel MPTA (Atlas Copco) Neu",
                Manufacturer.DefaultManufacturer.AtlasCopco,
                ToolModel.ToolModelTypeStrings.ClickWrench,
                ToolModel.ToolModelClassStrings.ClickWrenchClass.DriverScale,
                0,
                0,
                6,
                80,
                10,
                50,
                "Tool Type 2",
                4.7,
                "Switch Off 2",
                "Shut Off 2",
                "Drive Size 2",
                "Drive Type 2",
                "Construction Type 2",
                0,
                0);
        }
        public static ToolModel GetMpToolAllocationToolModel3()
        {
            return new ToolModel(
                "TestToolModel MPTA (SCS) Neu",
                Manufacturer.DefaultManufacturer.SCS,
                ToolModel.ToolModelTypeStrings.PulseDriver,
                "",
                0,
                0,
                6,
                80,
                10,
                50,
                "Tool Type 2",
                4.7,
                "Switch Off 2",
                "Shut Off 2",
                "Drive Size 2",
                "Drive Type 2",
                "Construction Type 2",
                0,
                0);
        }
        public static ToolModel GetMpToolAllocationToolModel4()
        {
            return new ToolModel(
                "TestToolModel MPTA (GWK) Neu",
                Manufacturer.DefaultManufacturer.GWK,
                ToolModel.ToolModelTypeStrings.ProductionWrench,
                ToolModel.ToolModelClassStrings.Md_ProductionWrenchClass.DriverElectronic,
                0,
                0,
                6,
                80,
                10,
                50,
                "Tool Type 2",
                4.7,
                "Switch Off 2",
                "Shut Off 2",
                "Drive Size 2",
                "Drive Type 2",
                "Construction Type 2",
                0,
                0);
        }
        public static ToolModel GetToolModelForTestToolLongData()
        {
            return new ToolModel(
                "TestToolModel (BMW) (TTLD)",
                Manufacturer.DefaultManufacturer.BMW,
                ToolModel.ToolModelTypeStrings.EcDriver,
                "",
                0,
                0,
                0,
                0,
                1,
                10,
                "Tool Type 1 (TTLD)",
                0,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolModelForToolTemplateTest()
        {
            return new ToolModel(
                "TestToolModel (Crane) (TTWT)",
                Manufacturer.DefaultManufacturer.Crane,
                ToolModel.ToolModelTypeStrings.EcDriver,
                "",
                0,
                0,
                0,
                0,
                1,
                10,
                "Tool Type 1 (TTLD)",
                0,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolModelForToolDuplicateIdsTest()
        {
            return new ToolModel(
                "TestToolModel (Schatz) (TTDI)",
                Manufacturer.DefaultManufacturer.Schatz,
                ToolModel.ToolModelTypeStrings.MdWrench,
                ToolModel.ToolModelClassStrings.Md_ProductionWrenchClass.WrenchElectronic,
                0,
                0,
                0,
                0,
                1,
                10,
                "Tool Type 1 (TTLD)",
                0,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
                public static ToolModel GetToolModelLongInvalidData()
        {
            return new ToolModel(
                "TestToolModel very looooooooooooooooooooooooooooooooooooooong",
                Manufacturer.DefaultManufacturer.CSP,
                ToolModel.ToolModelTypeStrings.EcDriver,
                1,
                10,
                "Tool Type 1",
                5,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolModelLongInvalidDataValid()
        {
            return new ToolModel(
                "TestToolModel very loooooooooooooooooooo",
                Manufacturer.DefaultManufacturer.CSP,
                ToolModel.ToolModelTypeStrings.EcDriver,
                1,
                10,
                "Tool Type 1",
                5,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolModelLongInvalidDataForChange()
        {
            return new ToolModel(
                "TestToolModel even looooooooooooooooooooooooooooooooooooooooooooooonger",
                Manufacturer.DefaultManufacturer.CSP,
                ToolModel.ToolModelTypeStrings.EcDriver,
                1,
                10,
                "Tool Type 1",
                5,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolModelLongInvalidDataForChangeValid()
        {
            return new ToolModel(
                "TestToolModel even loooooooooooooooooooo",
                Manufacturer.DefaultManufacturer.CSP,
                ToolModel.ToolModelTypeStrings.EcDriver,
                1,
                10,
                "Tool Type 1",
                5,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolModelTemplateForTemplateTest()
        {
            return new ToolModel(
                "TestToolModel Template (TTMWT)",
                Manufacturer.DefaultManufacturer.BMW,
                ToolModel.ToolModelTypeStrings.General,
                "",
                3,
                5,
                6.1,
                60,
                22,
                90.5,
                "Template Tool Type 1",
                5,
                "Template SwitchOff",
                "Template ShutOff",
                "Template DriveSize",
                "Template DriveType",
                "Template ConstructionType",
                0,
                0);
        }
        public static ToolModel GetToolModelForTemplateTest()
        {
            return new ToolModel(
                "TestToolModel (TTMWT)",
                Manufacturer.DefaultManufacturer.Crane,
                ToolModel.ToolModelTypeStrings.General,
                "",
                2,
                7,
                3.8,
                80,
                21,
                91,
                "Tool Type 1",
                3,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolModelDuplicateDescription()
        {
            return new ToolModel(
                "TestToolModel TTMDD",
                Manufacturer.DefaultManufacturer.CSP,
                ToolModel.ToolModelTypeStrings.General,
                "",
                2.1,
                3.4,
                6.1,
                60,
                4,
                20.5,
                "Tool Type 1",
                5,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolModelChangeSite1()
        {
            return new ToolModel(
                "TestToolModel (TTMOCSTM)",
                Manufacturer.DefaultManufacturer.Schatz,
                ToolModel.ToolModelTypeStrings.General,
                "",
                2,
                7,
                3.8,
                80,
                20,
                90,
                "Tool Type 1",
                3,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolModelChangeSite1Changed()
        {
            return new ToolModel(
                "TestToolModel (Changed) (TTMOCSTM)",
                Manufacturer.DefaultManufacturer.REC,
                ToolModel.ToolModelTypeStrings.General,
                "",
                2,
                7,
                3.8,
                80,
                10,
                70,
                "Tool Type 1",
                3,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolModelChangeSite2()
        {
            return new ToolModel(
                "TestToolModel To Switch(TTMOCSTM)",
                Manufacturer.DefaultManufacturer.Saltus,
                ToolModel.ToolModelTypeStrings.General,
                "",
                2,
                7,
                3.8,
                80,
                20,
                90,
                "Tool Type 1",
                3,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolToolModelChangeSite1()
        {
            return new ToolModel(
                "TestToolModel (TTOCST)",
                Manufacturer.DefaultManufacturer.Schatz,
                ToolModel.ToolModelTypeStrings.General,
                "",
                2,
                7,
                3.8,
                80,
                20,
                90,
                "Tool Type 1",
                3,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolToolModelChangeSite1Changed()
        {
            return new ToolModel(
                "TestToolModel (Changed) (TTOCST)",
                Manufacturer.DefaultManufacturer.REC,
                ToolModel.ToolModelTypeStrings.General,
                "",
                2,
                7,
                3.8,
                80,
                10,
                70,
                "Tool Type 1",
                3,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolToolModelChangeSite2()
        {
            return new ToolModel(
                "TestToolModel To Switch(TTOCST)",
                Manufacturer.DefaultManufacturer.Saltus,
                ToolModel.ToolModelTypeStrings.General,
                "",
                2,
                7,
                3.8,
                80,
                20,
                90,
                "Tool Type 1",
                3,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolModelForMpToolReferenceTest()
        {
            return new ToolModel(
                "TestToolModel (Gedore) (TMMPTRT1)",
                Manufacturer.DefaultManufacturer.Gedore,
                ToolModel.ToolModelTypeStrings.MdWrench,
                ToolModel.ToolModelClassStrings.Md_ProductionWrenchClass.WrenchElectronic,
                0,
                0,
                0,
                0,
                1,
                10,
                "Tool Type 1 (TTLD)",
                0,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolModelForMpToolReferenceTest2()
        {
            return new ToolModel(
                "TestToolModel (GWK) (TMMPTRT2)",
                Manufacturer.DefaultManufacturer.GWK,
                ToolModel.ToolModelTypeStrings.ClickWrench,
                ToolModel.ToolModelClassStrings.ClickWrenchClass.BeamTypeTorqueWrenchWithScale,
                0,
                0,
                0,
                0,
                1,
                10,
                "Tool Type 1 (TTLD)",
                0,
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                "Standard",
                0,
                0);
        }
        public static ToolModel GetToolModelForUndoChanges()
        {
            return new ToolModel(
                "TestToolModel (GWK) (TTMUC)",
                Manufacturer.DefaultManufacturer.GWK,
                ToolModel.ToolModelTypeStrings.ClickWrench,
                ToolModel.ToolModelClassStrings.ClickWrenchClass.BeamTypeTorqueWrenchWithScale,
                1,
                2,
                3,
                4,
                1,
                10,
                "Tool Type 1 (TMMUC)",
                5,
                "SwitchOff 1 (TMMUC)",
                "ShutOff 1 (TMMUC)",
                "DriveSize 1 (TMMUC)",
                "DriveType 1 (TMMUC)",
                "ConstructionType 1 (TMMUC)",
                ToolModel.defaultCm,
                ToolModel.defaultCmk);
        }
        public static ToolModel GetToolModelForUndoChangesUpdate()
        {
            return new ToolModel(
                "TestToolModel (GWK) (TTMUC) Update",
                Manufacturer.DefaultManufacturer.BLM,
                ToolModel.ToolModelTypeStrings.ProductionWrench,
                ToolModel.ToolModelClassStrings.Md_ProductionWrenchClass.WrenchElectronic,
                4,
                3,
                2,
                1,
                5,
                32,
                "Tool Type 2 (TMMUC)",
                2,
                "SwitchOff 2 (TMMUC)",
                "ShutOff 2 (TMMUC)",
                "DriveSize 2 (TMMUC)",
                "DriveType 2 (TMMUC)",
                "ConstructionType 2 (TMMUC)",
                ToolModel.defaultCm,
                ToolModel.defaultCmk);
        }

        public static Tool GetTool1()
        {
            return new Tool(
            "300001",
            "ASP739",
            GetToolToolModel1(),
            "In Betrieb",
            "Akkus inkl.",
            "2000",
            "Config Field",
            "WKZ Kommentar",
            "Qst-Standard",
            "Config1 Field",
            "Config2 Field",
            "Config3 Field");
        }
        public static Tool GetTool2()
        {
            return new Tool(
                "300002",
                "EAP740",
                GetToolToolModel2(),
                "Im Lager",
                "Mit Ersatz-Schraubkopf",
                "3000",
                "Freier Text",
                "WKZ SCS Kommentar",
                "Qst-Standard",
                "Konfig1 Field",
                "Konfig2 Field",
                "Konfig3 Field");
        }
        public static Tool GetToolChanged1()
        {
            Tool cspToolChanged = new Tool();

            cspToolChanged.InventoryNumber = "300003";
            cspToolChanged.SerialNumber = "ASP740";
            cspToolChanged.ToolModel = GetToolToolModelChanged1();
            cspToolChanged.Status = "Zur Reparatur";
            cspToolChanged.Accessory = "2 Akkus inkl.";
            cspToolChanged.CostCenter = "2100";
            cspToolChanged.ConfigurableField = "ConfigField (Parameter)";
            cspToolChanged.Comment = "WKZ Kommentar Neu";
            cspToolChanged.CmCmkLimit = "Qst-Standard";
            cspToolChanged.ConfigurableField1 = "Config1 Field (Additional)";
            cspToolChanged.ConfigurableField2 = "Config2 Field (Additional)";
            cspToolChanged.ConfigurableField3 = "Config3 Field (Additional)";
            return cspToolChanged;
        }
        public static Tool GetToolChanged2()
        {
            Tool scsToolChanged = new Tool();

            scsToolChanged.InventoryNumber = "300012";
            scsToolChanged.SerialNumber = "TAP740";
            scsToolChanged.ToolModel = GetTool2().ToolModel;
            scsToolChanged.Status = "Zur Reparatur";
            scsToolChanged.Accessory = "Ersatz-Schraubkopf (verschwunden)";
            scsToolChanged.CostCenter = "3300";
            scsToolChanged.ConfigurableField = "neuer Freier Text";
            scsToolChanged.Comment = "WKZ SCS Kommentar Neu";
            scsToolChanged.CmCmkLimit = "Qst-Standard";
            scsToolChanged.ConfigurableField1 = "Konfig1 Field (Additional)";
            scsToolChanged.ConfigurableField2 = "Konfig2 Field (Additional)";
            scsToolChanged.ConfigurableField3 = "Konfig3 Field (Additional)";
            return scsToolChanged;
        }
        public static Tool GetToolLongInvalidData()
        {
            Tool invalidTool = new Tool();

            invalidTool.InventoryNumber = "300031012345678901234567890123456789012345678901234567890123456789";
            invalidTool.SerialNumber = "TTLDAQ740012345678901234567890123456789";
            invalidTool.ToolModel = GetToolModelForTestToolLongData();
            invalidTool.Status = "In Betrieb";
            invalidTool.Accessory = ""; //TODO einbauen wenn Längenbeschränkung vorhanden https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-6
            invalidTool.CostCenter = "300031";
            invalidTool.ConfigurableField = "ConfigField Long";
            invalidTool.Comment = "WKZ BMW Kommentar Neu"; //TODO einbauen wenn Längenbeschränkung vorhanden https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-6
            invalidTool.CmCmkLimit = "Qst-Standard";
            invalidTool.ConfigurableField1 = "Laaaaaaaaaaaaaaaaaaaaaaaaanges Konfig1 Feld";
            invalidTool.ConfigurableField2 = "Laaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaanges Konfig2 Feld";
            invalidTool.ConfigurableField3 = "Laaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaäaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaanges Konfig3 Feeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeld";
            return invalidTool;
        }
        public static Tool GetToolLongInvalidDataValid()
        {
            Tool validTool = new Tool();

            validTool.InventoryNumber = "30003101234567890123456789012345678901234567890123";
            validTool.SerialNumber = "TTLDAQ74001234567890";
            validTool.ToolModel = GetToolModelForTestToolLongData();
            validTool.Status = "In Betrieb";
            validTool.Accessory = "";//TODO einbauen wenn Längenbeschränkung vorhanden https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-6
            validTool.CostCenter = "300031";
            validTool.ConfigurableField = "ConfigField Long";
            validTool.Comment = "WKZ BMW Kommentar Neu"; //TODO einbauen wenn Längenbeschränkung vorhanden https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-6
            validTool.CmCmkLimit = "Qst-Standard";
            validTool.ConfigurableField1 = "Laaaaaaaaaaaaaaaaaaaaaaaaanges Konfig1 F";
            validTool.ConfigurableField2 = "Laaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaanges Konfig2 Fe";
            validTool.ConfigurableField3 = "Laaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaäaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaanges Konfig3 Feeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee";
            return validTool;
        }
        public static Tool GetToolLongInvalidDataForChange()
        {
            Tool invalidToolChanged = new Tool();

            invalidToolChanged.InventoryNumber = "300031987654321098765432109876543210987654321098765432109876543210";
            invalidToolChanged.SerialNumber = "TTLDAQ740987654321098765432109876543210";
            invalidToolChanged.ToolModel = GetToolModelForTestToolLongData();
            invalidToolChanged.Status = "In Betrieb";
            invalidToolChanged.Accessory = "";//TODO einbauen wenn Längenbeschränkung vorhanden https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-6
            invalidToolChanged.CostCenter = "300031";
            invalidToolChanged.ConfigurableField = "ConfigField Long";
            invalidToolChanged.Comment = "WKZ BMW Kommentar Neu"; //TODO einbauen wenn Längenbeschränkung vorhanden https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-6
            invalidToolChanged.CmCmkLimit = "Qst-Standard";
            invalidToolChanged.ConfigurableField1 = "Laaaaaaaaaaaaaaaaaaaaaaaaaeeeenges Konfig1 Field1";
            invalidToolChanged.ConfigurableField2 = "Laaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaeeeeeeenges Konfig2 Feld";
            invalidToolChanged.ConfigurableField3 = "Laaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaäeeeaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaanges Konfig3 Feeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeellllld";
            return invalidToolChanged;
        }
        public static Tool GetToolLongInvalidDataForChangeValid()
        {
            Tool validToolChanged = new Tool();

            validToolChanged.InventoryNumber = "30003198765432109876543210987654321098765432109876";
            validToolChanged.SerialNumber = "TTLDAQ74098765432109";
            validToolChanged.ToolModel = GetToolModelForTestToolLongData();
            validToolChanged.Status = "In Betrieb";
            validToolChanged.Accessory = "";//TODO einbauen wenn Längenbeschränkung vorhanden https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-6
            validToolChanged.CostCenter = "300031";
            validToolChanged.ConfigurableField = "ConfigField Long";
            validToolChanged.Comment = "WKZ BMW Kommentar Neu"; //TODO einbauen wenn Längenbeschränkung vorhanden https://atlassian.csp-sw.de:8443/jira/browse/QSTBV8-6
            validToolChanged.CmCmkLimit = "Qst-Standard";
            validToolChanged.ConfigurableField1 = "Laaaaaaaaaaaaaaaaaaaaaaaaaeeeenges Konfi";
            validToolChanged.ConfigurableField2 = "Laaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaeeeeeeenges Konfig2 F";
            validToolChanged.ConfigurableField3 = "Laaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaäeeeaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaanges Konfig3 Feeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeell";
            return validToolChanged;
        }
        public static Tool GetToolChangeSite1()
        {
            return new Tool(
            "300061",
            "TTOCST",
            GetToolToolModelChangeSite1(),
            "In Betrieb",
            "Akkus inkl.",
            "2000",
            "Config Field",
            "",
            "Qst-Standard",
            "Config1 Field",
            "Config2 Field",
            "Config3 Field");
        }
        public static Tool GetToolChangeSite1Changed()
        {
            return new Tool(
            "300061 (Changed)",
            "TTOCCS",
            GetToolToolModelChangeSite1(),
            "Zur Reparatur",
            "Akkus + Ladekabel",
            "3000",
            "Config Field",
            "",
            "Qst-Standard",
            "Config1 Field",
            "Config2 Field",
            "Config3 Field");
        }
        public static Tool GetToolChangeSite2()
        {
            return new Tool(
            "300063",
            "TTOCCS2",
            GetToolToolModelChangeSite2(),
            "In Betrieb",
            "Akkus inkl.",
            "2000",
            "Config Field",
            "",
            "Qst-Standard",
            "Config1 Field",
            "Config2 Field",
            "Config3 Field");
        }
        public static Tool GetMpToolAllocationTool1()
        {
            return new Tool(
            "300021",
            "TMTA1",
            GetMpToolAllocationToolModel1(),
            "In Betrieb",
            "Akkus inkl.",
            "2000",
            "Config Field",
            "WKZ Kommentar",
            "Qst-Standard",
            "Config1 Field",
            "Config2 Field",
            "Config3 Field");
        }
        public static Tool GetMpToolAllocationTool2()
        {
            return new Tool(
            "300022",
            "TMTA2",
            GetMpToolAllocationToolModel2(),
            "Im Lager",
            "Mit Ersatz-Schraubkopf",
            "3000",
            "Freier Text",
            "WKZ SCS Kommentar",
            "Qst-Standard",
            "Konfig1 Field",
            "Konfig2 Field",
            "Konfig3 Field");
        }
        public static Tool GetMpToolAllocationTool3()
        {
            return new Tool(
            "300023",
            "TMTA3",
            GetMpToolAllocationToolModel3(),
            "In Betrieb",
            "Akkus inkl.",
            "2000",
            "Config Field",
            "WKZ Kommentar",
            "Qst-Standard",
            "Config1 Field",
            "Config2 Field",
            "Config3 Field");
        }
        public static Tool GetMpToolAllocationTool4()
        {
            return new Tool(
            "300024",
            "TMTA4",
            GetMpToolAllocationToolModel4(),
            "In Betrieb",
            "Akkus inkl.",
            "2000",
            "Config Field",
            "WKZ Kommentar",
            "Qst-Standard",
            "Config1 Field",
            "Config2 Field",
            "Config3 Field");
        }
        public static Tool GetToolTemplateForTemplateTest()
        {
            Tool template = new Tool();

            template.InventoryNumber = "300041";
            template.SerialNumber = "ToolTemplate (TTWT)";
            template.ToolModel = GetToolModelForToolTemplateTest();
            template.Status = "In Betrieb";
            template.Accessory = "Accessory Template";
            template.CostCenter = "300041";
            template.ConfigurableField = "ConfigField Template";
            template.Comment = "Tool Comment Template";
            template.CmCmkLimit = "Qst-Standard";
            template.ConfigurableField1 = "Konfig 1 Template";
            template.ConfigurableField2 = "Konfig 2 Template";
            template.ConfigurableField3 = "Konfig 3 Template";
            return template;
        }
        public static Tool GetToolForTemplateTest()
        {
            Tool toolForTemplateTest = new Tool();

            toolForTemplateTest.InventoryNumber = "300042";
            toolForTemplateTest.SerialNumber = "Tool FTT (TTWT)";
            toolForTemplateTest.ToolModel = GetToolModelForToolTemplateTest();
            toolForTemplateTest.Status = "Zur Reparatur";
            toolForTemplateTest.Accessory = "Accessory";
            toolForTemplateTest.CostCenter = "300042";
            toolForTemplateTest.ConfigurableField = "ConfigField Tool";
            toolForTemplateTest.Comment = "Tool Comment";
            toolForTemplateTest.CmCmkLimit = "Qst-Standard";
            toolForTemplateTest.ConfigurableField1 = "Konfig 1 Tool";
            toolForTemplateTest.ConfigurableField2 = "Konfig 2 Tool";
            toolForTemplateTest.ConfigurableField3 = "Konfig 3 Tool";
            return toolForTemplateTest;
        }
        public static Tool GetToolForDuplicateIdsTest()
        {
            Tool toolForTemplateTest = new Tool();

            toolForTemplateTest.InventoryNumber = "300051";
            toolForTemplateTest.SerialNumber = "Tool asdf (TTDI)";
            toolForTemplateTest.ToolModel = GetToolModelForToolDuplicateIdsTest();
            toolForTemplateTest.Status = "Zur Reparatur";
            toolForTemplateTest.Accessory = "Accessory";
            toolForTemplateTest.CostCenter = "2000";
            toolForTemplateTest.ConfigurableField = "ConfigField Tool";
            toolForTemplateTest.Comment = "Tool Comment";
            toolForTemplateTest.CmCmkLimit = "Qst-Standard";
            toolForTemplateTest.ConfigurableField1 = "Konfig 1 Tool";
            toolForTemplateTest.ConfigurableField2 = "Konfig 2 Tool";
            toolForTemplateTest.ConfigurableField3 = "Konfig 3 Tool";
            return toolForTemplateTest;
        }        
        public static Tool GetToolForMpToolReferenceTest1()
        {
            Tool template = new Tool();

            template.InventoryNumber = "300061";
            template.SerialNumber = "Tool (TMPTRT1)";
            template.ToolModel = GetToolModelForMpToolReferenceTest();
            template.Status = "In Betrieb";
            template.Accessory = "Accessory Template";
            template.CostCenter = "2000";
            template.ConfigurableField = "ConfigField Template";
            template.Comment = "Tool Comment Template";
            template.CmCmkLimit = "Qst-Standard";
            template.ConfigurableField1 = "Konfig 1 Template";
            template.ConfigurableField2 = "Konfig 2 Template";
            template.ConfigurableField3 = "Konfig 3 Template";
            return template;
        }
        public static Tool GetToolForMpToolReferenceTest2()
        {
            Tool template = new Tool();

            template.InventoryNumber = "300062";
            template.SerialNumber = "Tool (TMPTRT2)";
            template.ToolModel = GetToolModelForMpToolReferenceTest2();
            template.Status = "In Betrieb";
            template.Accessory = "Accessory Template";
            template.CostCenter = "2000";
            template.ConfigurableField = "ConfigField Template";
            template.Comment = "Tool Comment Template";
            template.CmCmkLimit = "Qst-Standard";
            template.ConfigurableField1 = "Konfig 1 Template";
            template.ConfigurableField2 = "Konfig 2 Template";
            template.ConfigurableField3 = "Konfig 3 Template";
            return template;
        }

        public static MeasurementPoint GetMp1()
        {
            return new MeasurementPoint(
                "100001",
                "GUI-Test MP1",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                25,
                35.378,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "Config. Field",
                "B",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "☺wwwwwwwww", "☻", "♥", "♦" }
                );
        }
        public static MeasurementPoint GetMp2()
        {
            return new MeasurementPoint(
                "100002",
                "Noch ein GUI-Test MP2",
                ControlledBy.Angle,
                15,
                GetMp2ToleranceClass(),
                13.5,
                18,
                7.5,
                50,
                GetMp2ToleranceClassAngle(),
                45,
                55,
                "Freie Eingabe",
                "A",
                false,
                "Kommentar:☺☻!!!",
                new List<string>() { MpRootNode });
        }
        public static MeasurementPoint GetMpChanged1()
        {
            MeasurementPoint ersterMPChanged = new MeasurementPoint();

            ersterMPChanged.Number = "100001_1";
            ersterMPChanged.Description = "GUI - Test MP1 (geändert)";
            ersterMPChanged.ControlledBy = ControlledBy.Angle;
            ersterMPChanged.SetPointTorque = 0;
            ersterMPChanged.ToleranceClassTorque = GetMp1().ToleranceClassTorque;
            ersterMPChanged.MinTorque = 0;
            ersterMPChanged.MaxTorque = 0;
            ersterMPChanged.SetPointAngle = 105;
            ersterMPChanged.ThresholdTorque = 17;
            ersterMPChanged.ToleranceClassAngle = GetMp1().ToleranceClassAngle;
            ersterMPChanged.MinAngle = 95;
            ersterMPChanged.MaxAngle = 110;
            ersterMPChanged.ConfigurableField = "Blub";
            ersterMPChanged.ConfigurableField2 = "C";
            ersterMPChanged.ConfigurableField3 = false;
            ersterMPChanged.Comment = "Kommentar Neu";
            ersterMPChanged.ListParentFolder = new List<string>() { MpRootNode, "☺wwwwwwwww", "☻", "♥", "♦"};
            return ersterMPChanged;
        }
        public static MeasurementPoint GetMpChanged2()
        {
            MeasurementPoint zweiterMPChanged = new MeasurementPoint();

            zweiterMPChanged.Number = "100003";
            zweiterMPChanged.Description = "Noch ein weiterer GUI-Test MP2";
            zweiterMPChanged.ControlledBy = ControlledBy.Angle;
            zweiterMPChanged.SetPointTorque = 18;
            zweiterMPChanged.ToleranceClassTorque = GetMp2().ToleranceClassTorque;
            zweiterMPChanged.MinTorque = 16.2;
            zweiterMPChanged.MaxTorque = 21.6;
            zweiterMPChanged.SetPointAngle = 55;
            zweiterMPChanged.ThresholdTorque = 8;
            zweiterMPChanged.ToleranceClassAngle = GetMp2ToleranceClassChanged();
            zweiterMPChanged.MinAngle = 48;
            zweiterMPChanged.MaxAngle = 62;
            zweiterMPChanged.ConfigurableField = "Freie Eingabe neu";
            zweiterMPChanged.ConfigurableField2 = "D";
            zweiterMPChanged.ConfigurableField3 = false;
            zweiterMPChanged.Comment = "Kommentar:☺☻!!!♥♦♣♠•◘○";
            zweiterMPChanged.ListParentFolder = new List<string>() { MpRootNode };
            return zweiterMPChanged;
        }
        public static MeasurementPoint GetMpTreeMp1()
        {
            return new MeasurementPoint(
                "100011",
                "GUI-Test MP_T1",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                25,
                35.378,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "Config. Field",
                "B",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "B-☺", "☻", "♥", "♦" });
        }
        public static MeasurementPoint GetMpTreeMp2()
        {
            return new MeasurementPoint(
                "100012",
                "Noch ein GUI-Test MP_T2",
                ControlledBy.Angle,
                15,
                GetMpTreeToleranceClass(),
                13.5,
                18,
                7.5,
                50,
                GetMpTreeToleranceClassAngle(),
                45,
                55,
                "Freie Eingabe",
                "A",
                false,
                "Kommentar:☺☻!!!",
                new List<string>() { MpRootNode, "B-☺", "C", "D" });
        }
        public static MeasurementPoint GetMpTreeMp3()
        {
            return new MeasurementPoint(
                "100013",
                "Noch ein GUI-Test MP_T3",
                ControlledBy.Angle,
                15,
                GetMpTreeToleranceClass(),
                13.5,
                18,
                7.5,
                50,
                GetMpTreeToleranceClassAngle(),
                45,
                55,
                "Freie Eingabe",
                "A",
                false,
                "Kommentar:☺☻!!!",
                new List<string>() { MpRootNode, "B-☺", "C" });
        }
        public static MeasurementPoint GetQSTBV8_116Mp1()
        {
            return new MeasurementPoint(
                "5",
                "A",
                ControlledBy.Torque,
                4,
                new ToleranceClass(),
                3,
                5,
                2,
                0,
                new ToleranceClass(),
                0,
                0,
                "",
                "",
                false,
                "",
                new List<string>() { MpRootNode, "B" });
        }
        public static MeasurementPoint GetMpToolAllocationMp1()
        {
            return new MeasurementPoint(
                "100021",
                "GUI-Test MP1 (Alloc)",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                25,
                35.378,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "Config. Field",
                "B",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "MpToolAllocations" }
            );
        }
        public static MeasurementPoint GetMpToolAllocationMp2()
        {
            return new MeasurementPoint(
                "100022",
                "GUI-Test MP2 (Alloc)",
                ControlledBy.Angle,
                30,
                new ToleranceClass(),
                25,
                35.378,
                15,
                100,
                GetMpToolAllocationToleranceClassAngle(),
                90,
                110,
                "Config. Field2",
                "A",
                false,
                "Das ist ein Testkommentar Neu!",
                new List<string>() { MpRootNode, "MpToolAllocations" }
            );
        }
        public static MeasurementPoint GetMpToolAllocationMp3()
        {
            return new MeasurementPoint(
                "100023",
                "GUI-Test MP3 (Alloc)",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                25,
                35.378,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "C.Field",
                "C",
                true,
                "Kommentar...",
                new List<string>() { MpRootNode, "MpToolAllocations", "B" }
            );
        }
        public static MeasurementPoint GetMpTemplateForTemplateTest()
        {
            return new MeasurementPoint(
                "100031",
                "GUI-Test MP Template",
                ControlledBy.Torque,
                50,
                GetMpTemplateToleranceClassTorque(),
                48,
                52.5,
                25,
                103,
                new ToleranceClass(),
                97,
                105,
                "Config. Field",
                "B",
                true,
                "Kommentar",
                new List<string>() { MpRootNode, "TemplateTest"}
                );
        }
        public static MeasurementPoint GetMpForTemplateTest()
        {
            return new MeasurementPoint(
                "100032",
                "Noch ein GUI-Test MP for TemplateTest",
                ControlledBy.Angle,
                50,
                new ToleranceClass(),
                47.3,
                56.5,
                12.5,
                103,
                GetMpTemplateToleranceClassAngle(),
                98,
                108,
                "Config. Field",
                "B",
                true,
                "Kommentar",
                new List<string>() { MpRootNode, "TemplateTest"}
                );
        }
        public static MeasurementPoint GetMpProcessControl1()
        {
            return new MeasurementPoint(
                "100041",
                "TPC1",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                25,
                35.378,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "Config. Field",
                "B",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "ProcessControl" }
                );
        }
        public static MeasurementPoint GetMpProcessControl2()
        {
            return new MeasurementPoint(
                "100042",
                "TPC2",
                ControlledBy.Angle,
                15,
                GetMp2ToleranceClass(),
                13.5,
                18,
                7.5,
                50,
                GetMp2ToleranceClassAngle(),
                45,
                55,
                "Freie Eingabe",
                "A",
                false,
                "Kommentar:☺☻!!!",
                new List<string>() { MpRootNode, "ProcessControl" });
        }
        public static MeasurementPoint GetMpProcessControl3()
        {
            return new MeasurementPoint(
                "100043",
                "TPC3",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                25,
                35.378,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "Config. Field",
                "B",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "ProcessControl" }
            );
        }
        public static MeasurementPoint GetMpInvalidData1()
        {
            return new MeasurementPoint(
                "10005199999999999999999999999999",
                "°!\"§$%&/()?*_:><,.-#+´ß^²³{}\\~|€µ@測試測點TMPIDV0123456789öäüÖÄÜ",
                ControlledBy.Torque,
                0,
                new ToleranceClass(),
                30.001,
                29.999,
                30.001, //Threshold torque Größer als minTorque?
                90,
                new ToleranceClass(),
                90.001,
                89.999,
                "***นานเกินไป ***",
                "AB",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "InvalidData"}
                );
        }
        public static MeasurementPoint GetMpInvalidData1Valid()
        {
            return new MeasurementPoint(
                "100051999999999999999999999999",
                "°!\"§$%&/()?*_:><,.-#+´ß^²³{}\\~|€µ@測試測點TMPIDV012345",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                25,
                35.378,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "***นานเกินไป **",
                "A",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "InvalidData" }
                );
        }
        public static MeasurementPoint GetMpInvalidData2()
        {
            return new MeasurementPoint(
                "100052",
                "GUI-Test MP TMPiD (2)",
                ControlledBy.Angle,
                -0,
                new ToleranceClass(),
                30.001,
                29.999,
                -0,
                0,
                new ToleranceClass(),
                90.001,
                89.999,
                "español campo de prueba",
                "B",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "InvalidData"}
                );
        }
        public static MeasurementPoint GetMpInvalidData2Valid()
        {
            return new MeasurementPoint(
                "100052",
                "GUI-Test MP TMPiD (2)",
                ControlledBy.Angle,
                30,
                new ToleranceClass(),
                25,
                35.378,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "español campo d",
                "B",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "InvalidData" }
                );
        }
        public static MeasurementPoint GetMpInvalidDataForChange1()
        {
            return new MeasurementPoint(
                "10005199999999999999999999999999",
                "",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                30.001,
                29.999,
                30.001, //Threshold torque Größer als minTorque?
                90,
                new ToleranceClass(),
                90.001,
                89.999,
                "***นานเกินไป ***",
                "AB",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "InvalidData" }
                );
        }
        public static MeasurementPoint GetMpInvalidDataForChange2()
        {
            return new MeasurementPoint(
                "100052",
                "GUI-Test MP TMPiD (2)",
                ControlledBy.Angle,
                30,
                new ToleranceClass(),
                30.001,
                29.999,
                31,
                90,
                new ToleranceClass(),
                90.001,
                89.999,
                "español campo d",
                "B",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "InvalidData" }
                );
        }
        public static MeasurementPoint GetMpLongInvalidData1()
        {
            return new MeasurementPoint(
                "1000610123456789012345678901234567890123456789",
                "looooooooooooooooooooooong Description 0123456789012345678901234567890123456789",
                ControlledBy.Torque,
                0,
                new ToleranceClass(),
                25,
                35,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "loooooooong Field",
                "AB",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "LongInvalidData" }
                );
        }
        public static MeasurementPoint GetMpLongInvalidData1Valid()
        {
            return new MeasurementPoint(
                "100061012345678901234567890123",
                "looooooooooooooooooooooong Description 01234567890",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                25,
                35,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "loooooooong Fie",
                "A",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "LongInvalidData" }
                );
        }
        public static MeasurementPoint GetMpLongInvalidData2()
        {
            return new MeasurementPoint(
                "100062",
                "GUI-Test MP TMPiD (2)",
                ControlledBy.Angle,
                -0,
                new ToleranceClass(),
                25,
                35,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "español campo de prueba",
                "B",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "LongInvalidData" }
                );
        }
        public static MeasurementPoint GetMpLongInvalidData2Valid()
        {
            return new MeasurementPoint(
                "100062",
                "GUI-Test MP TMPiD (2)",
                ControlledBy.Angle,
                30,
                new ToleranceClass(),
                25,
                35,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "español campo d",
                "B",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "LongInvalidData" }
                );
        }
        public static MeasurementPoint GetMpLongInvalidDataForChange1()
        {
            return new MeasurementPoint(
                "1000619876543210987654321098765432109876543210",
                "98765432109876543 looooooooooooooooooooooong Description ",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                25,
                35,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "long Field very long",
                "CA",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "LongInvalidData" }
                );
        }
        public static MeasurementPoint GetMpLongInvalidDataForChange1Valid()
        {
            return new MeasurementPoint(
                "100061987654321098765432109876",
                "98765432109876543 looooooooooooooooooooooong Descr",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                25,
                35,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "long Field very",
                "C",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "LongInvalidData" }
                );
        }
        public static MeasurementPoint GetMpLongInvalidDataForChange2()
        {
            return new MeasurementPoint(
                "100062",
                "GUI-Test MP TMPiD (2)",
                ControlledBy.Angle,
                30,
                new ToleranceClass(),
                25,
                35,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "español campo d",
                "B",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "LongInvalidData" }
                );
        }
        public static MeasurementPoint GetMpLongInvalidDataForChange2Valid()
        {
            return new MeasurementPoint(
                "100062",
                "GUI-Test MP TMPiD (2)",
                ControlledBy.Angle,
                30,
                new ToleranceClass(),
                25,
                35,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "español campo d",
                "B",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "LongInvalidData" }
                );
        }
        public static MeasurementPoint GetMpDuplicateId()
        {
            return new MeasurementPoint(
                "100061",
                "GUI-Test MP TMPDId",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                25,
                35,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "",
                "",
                false,
                "",
                new List<string>() { MpRootNode, "Duplicate Id" }
                );
        }
        public static MeasurementPoint GetMpChangeSite1()
        {
            return new MeasurementPoint(
                "100071",
                "GUI-Test MP TMPOCCS",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                25,
                35,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "",
                "",
                false,
                "",
                new List<string>() { MpRootNode, "ChangeSite" }
                );
        }
        public static MeasurementPoint GetMpChangeSite1Changed()
        {
            return new MeasurementPoint(
                "100071",
                "GUI-Test MP TMPOCCS Changed",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                25,
                35,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "",
                "",
                false,
                "",
                new List<string>() { MpRootNode, "ChangeSite" }
                );
        }
        public static MeasurementPoint GetMpChangeSite2()
        {
            return new MeasurementPoint(
                "100072",
                "GUI-Test MP TMPOCCS2",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                25,
                35,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "",
                "",
                false,
                "",
                new List<string>() { MpRootNode, "ChangeSite" }
                );
        }
        public static MeasurementPoint GetMpForUndoChanges()
        {
            return new MeasurementPoint(
                "100081",
                "GUI-Test MP81",
                ControlledBy.Torque,
                30,
                GetToleranceclassTorqueForMpUndoChanges(),
                28.5,
                30.9,
                15,
                90,
                GetToleranceclassAngleForMpUndoChanges(),
                82,
                97,
                "Config. Field",
                "B",
                true,
                "",
                new List<string>() { MpRootNode, "UndoChanges" }
                );
        }
        public static MeasurementPoint GetMpForUndoChangesUpdate()
        {
            return new MeasurementPoint(
                "100081 new",
                "GUI-Test MP81 ☺☻",
                ControlledBy.Angle,
                45,
                new ToleranceClass(),
                25.87,
                55.78,
                16.1,
                83,
                new ToleranceClass(),
                76,
                88,
                "Config. Field Neu",
                "A",
                false,
                "",
                new List<string>() { MpRootNode, "UndoChanges" }
                );
        }
        public static MeasurementPoint GetMpForReferenceTest1()
        {
            return new MeasurementPoint(
                "100091",
                "TMPTRT1",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                25,
                35.378,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "Config. Field",
                "B",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "ReferenceTest" }
                );
        }
        public static MeasurementPoint GetMpForReferenceTest2()
        {
            return new MeasurementPoint(
                "100092",
                "TMPTRT2",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                25,
                35.378,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "Config. Field",
                "B",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "ReferenceTest" }
                );
        }
        public static MeasurementPoint GetMpForReferenceTest3()
        {
            return new MeasurementPoint(
                "100093",
                "TMPTRT3",
                ControlledBy.Torque,
                30,
                new ToleranceClass(),
                25,
                35.378,
                15,
                90,
                new ToleranceClass(),
                85,
                95,
                "Config. Field",
                "B",
                true,
                "Das ist ein Testkommentar!",
                new List<string>() { MpRootNode, "ReferenceTest" }
                );
        }

        public static MpToolAllocation GetMpToolAllocation1()
        {
            return new MpToolAllocation(GetMpToolAllocationMp1(), GetMpToolAllocationTool1(), "testUsage", new DateTime(), "", new DateTime(), "", GetToolControlConditionsForMpToolAllocation1());
        }
        public static MpToolAllocation GetMpToolAllocation2()
        {
            return new MpToolAllocation(GetMpToolAllocationMp1(), GetMpToolAllocationTool2(), "testUsage2", new DateTime(), "", new DateTime(), "", GetToolControlConditionsForMpToolAllocation2());
        }
        public static MpToolAllocation GetMpToolAllocation3()
        {
            return new MpToolAllocation(GetMpToolAllocationMp2(), GetMpToolAllocationTool3(), "testUsage2", new DateTime(), "", new DateTime(), "", GetToolControlConditionsForMpToolAllocation3());
        }
        public static MpToolAllocation GetMpToolAllocation4()
        {
            return new MpToolAllocation(GetMpToolAllocationMp2(), GetMpToolAllocationTool4(), "testUsage4", new DateTime(), "", new DateTime(), "", GetToolControlConditionsForMpToolAllocation4());
        }
        public static MpToolAllocation GetMpToolForToolReferenceTest1()
        {
            return new MpToolAllocation(GetMpForReferenceTest1(), GetToolForMpToolReferenceTest1(), "testUsage5", new DateTime(), "", new DateTime(), "", null);
        }
        public static MpToolAllocation GetMpToolForToolReferenceTest2()
        {
            return new MpToolAllocation(GetMpForReferenceTest1(), GetToolForMpToolReferenceTest2(), "testUsage6", new DateTime(), "", new DateTime(), "", null);
        }
        public static MpToolAllocation GetMpToolForToolReferenceTest3()
        {
            return new MpToolAllocation(GetMpForReferenceTest2(), GetToolForMpToolReferenceTest1(), "testUsage5", new DateTime(), "", new DateTime(), "", null);
        }
        public static MpToolAllocation GetMpToolForToolReferenceTest4()
        {
            return new MpToolAllocation(GetMpForReferenceTest3(), GetToolForMpToolReferenceTest1(), "testUsage5", new DateTime(), "", new DateTime(), "", null);
        }

        //TODO evtl Drop AddHours wenn Zeitstempel nicht mehr angezeigt wird
        public static Testequipment GetTestEquipment1()
        {
            return new Testequipment(
                "67891234",
                "67894321", 
                GetTestEquipmentModel1(),
                "",
                DateTime.Today.AddDays(-200).AddHours(-24),
                199,
                "ISO1234",
                "3.79",
                1,
                20,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                Testequipment.TransferCurveOption.Always,
                Testequipment.AskForIdentOption.PerMeasurement,
                true,
                true,
                true,
                true,
                Testequipment.ConfirmMpOption.Always,
                true);
        }
        public static Testequipment GetTestEquipment2()
        {
            return new Testequipment(
                "43219876",
                "43216789",
                GetTestEquipmentModel2(),
                "",
                DateTime.Today.AddDays(-300).AddHours(-24),
                365,
                "ISO1234-2",
                "6.89",
                1,
                30,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                Testequipment.TransferCurveOption.Never,
                Testequipment.AskForIdentOption.Never,
                false,
                false,
                false,
                false,
                Testequipment.ConfirmMpOption.Never,
                false);
        }
        public static Testequipment GetTestEquipmentChanged1()
        {
            return new Testequipment(
                "678912345",
                "678943210",
                GetTestEquipmentModel1(),
                "",
                DateTime.Today.AddDays(-300).AddHours(-24),
                365,
                "ISO12345",
                "3.842",
                1.57,
                30,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                Testequipment.TransferCurveOption.OnlyNio,
                Testequipment.AskForIdentOption.PerTest,
                false,
                false,
                false,
                false,
                Testequipment.ConfirmMpOption.Never,
                false);
        }
        public static Testequipment GetTestEquipmentChanged2()
        {
            return new Testequipment(
                "432198764",
                "433216789",
                GetTestEquipmentModel2(),
                "",
                DateTime.Today.AddDays(-400).AddHours(-24),
                360,
                "ISO1234-2a",
                "6.892",
                2,
                40.4,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                Testequipment.TransferCurveOption.Always,
                Testequipment.AskForIdentOption.PerRoute,
                true,
                true,
                true,
                true,
                Testequipment.ConfirmMpOption.OnlyNio,
                true);
        }

        public static TestequipmentModel GetTestEquipmentModel1()
        {
            return new TestequipmentModel(
                TestequipmentModel.DeviceName.Quantec,
                TestequipmentModel.DataGateVersionOption.sechs,
                TestequipmentModel.TestequipmentType.TestWrench,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                @"C:\temp\abc.exe",
                @"C:\temp\abcd.xml",
                @"C:\temp\for_gwk.xml",
                @"C:\temp\for_csp.xml"
            );
        }

        public static TestequipmentModel GetTestEquipmentModel2()
        {
            return new TestequipmentModel(
                TestequipmentModel.DeviceName.STA6000,
                TestequipmentModel.DataGateVersionOption.fuenf,
                TestequipmentModel.TestequipmentType.AnalysisTool,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                @"C:\temp\cba.exe",
                @"C:\temp\dcba.xml",
                @"C:\temp\for_ac.xml",
                @"C:\temp\from_ac.xml"
            );
        }
        public static TestequipmentModel GetTestEquipmentModel3()
        {
            return new TestequipmentModel(
                TestequipmentModel.DeviceName.Quantec,
                TestequipmentModel.DataGateVersionOption.zwei,
                TestequipmentModel.TestequipmentType.TestWrench,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                @"C:\temp\dcba.exe",
                @"C:\temp\cba.xml",
                @"C:\temp\for_ac2.xml",
                @"C:\temp\from_ac2.xml"
            );
        }
        public static TestequipmentModel GetTestEquipmentModel4()
        {
            return new TestequipmentModel(
                TestequipmentModel.DeviceName.STA6000,
                TestequipmentModel.DataGateVersionOption.vier,
                TestequipmentModel.TestequipmentType.AnalysisTool,
                true,
                false,
                true,
                false,
                false,
                false,
                true,
                true,
                false,
                false,
                true,
                false,
                true,
                false,
                false,
                true,
                @"C:\temp\1.exe",
                @"C:\temp\2.xml",
                @"C:\temp\3.xml",
                @"C:\temp\4.xml"
            );
        }
        public static TestequipmentModel GetTestEquipmentModel5()
        {
            return new TestequipmentModel(
                TestequipmentModel.DeviceName.STA6000,
                TestequipmentModel.DataGateVersionOption.sieben,
                TestequipmentModel.TestequipmentType.AnalysisTool,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                @"C:\temp\edcba.exe",
                @"C:\temp\fedcba.xml",
                @"C:\temp\for_ac99.xml",
                @"C:\temp\from_ac99.xml"
            );
        }
        public static TestequipmentModel GetTestEquipmentModel6()
        {
            return new TestequipmentModel(
                TestequipmentModel.DeviceName.STA6000,
                TestequipmentModel.DataGateVersionOption.fuenf,
                TestequipmentModel.TestequipmentType.AnalysisTool,
                true,
                true,
                true,
                false,
                false,
                false,
                false,
                false,
                false,
                true,
                true,
                false,
                false,
                false,
                true,
                true,
                @"C:\temp\ba.exe",
                @"C:\temp\a.xml",
                @"C:\temp\ac.xml",
                @"C:\temp\csp.xml"
            );
        }

        public static TestLevelSet GetTestLevelSetXTimes()
        {
            return new TestLevelSet(
                "TestLevelSet für X Mal Pro",
                4,
                TestLevelSet.IntervalTypes.XTimesPerDay,
                1,
                true,
                true,
                3,
                TestLevelSet.IntervalTypes.XTimesPerShift,
                2,
                true,
                true,
                2,
                TestLevelSet.IntervalTypes.XTimesPerWeek,
                3,
                false
                );
        }
        public static TestLevelSet GetTestLevelSetEveryX()
        {
            return new TestLevelSet(
                "TestLevelSet für Alle X...",
                3,
                TestLevelSet.IntervalTypes.EveryXShifts,
                2,
                false,
                false,
                2,
                TestLevelSet.IntervalTypes.EveryXDays,
                3,
                true,
                true,
                1,
                TestLevelSet.IntervalTypes.XTimesPerWeek,
                5,
                true
                );
        }
        public static TestLevelSet GetTestLevelSetXTimesProcessControl()
        {
            return new TestLevelSet(
                "TPC TestLevelSet für X Mal Pro",
                4,
                TestLevelSet.IntervalTypes.XTimesPerDay,
                1,
                true,
                true,
                3,
                TestLevelSet.IntervalTypes.XTimesPerShift,
                2,
                true,
                true,
                2,
                TestLevelSet.IntervalTypes.XTimesPerWeek,
                3,
                false
                );
        }
        public static TestLevelSet GetTestLevelSetEveryXProcessControl()
        {
            return new TestLevelSet(
                "TPC TestLevelSet für Alle X...",
                3,
                TestLevelSet.IntervalTypes.EveryXShifts,
                2,
                false,
                false,
                2,
                TestLevelSet.IntervalTypes.EveryXDays,
                3,
                true,
                true,
                1,
                TestLevelSet.IntervalTypes.XTimesPerMonth,
                5,
                true
                );
        }
        public static TestLevelSet GetTestLevelSetXTimesChanged()
        {
            return new TestLevelSet(
                "TestLevelSet für nicht mehr X Mal Pro Geändert",
                2,
                TestLevelSet.IntervalTypes.EveryXDays,
                6,
                false,
                false,
                3,
                TestLevelSet.IntervalTypes.XTimesPerDay,
                2,
                true,
                false,
                2,
                TestLevelSet.IntervalTypes.XTimesPerWeek,
                3,
                false
                );
        }
        public static TestLevelSet GetTestLevelSetEveryXChanged()
        {
            return new TestLevelSet(
                "TestLevelSet für Alle X... (Neu)",
                7,
                TestLevelSet.IntervalTypes.EveryXShifts,
                3,
                false,
                true,
                1,
                TestLevelSet.IntervalTypes.EveryXDays,
                2,
                false,
                false,
                1,
                TestLevelSet.IntervalTypes.XTimesPerMonth,
                5,
                true
                );
        }
        public static TestLevelSet GetTestLevelSetXTimesForMpToolAllocation()
        {
            return new TestLevelSet(
                "TestLevelSet für X Mal Pro (TLSMTA1)",
                4,
                TestLevelSet.IntervalTypes.XTimesPerDay,
                1,
                true,
                true,
                3,
                TestLevelSet.IntervalTypes.XTimesPerShift,
                2,
                true,
                true,
                2,
                TestLevelSet.IntervalTypes.XTimesPerWeek,
                3,
                false
                );
        }
        public static TestLevelSet GetTestLevelSetEveryXForMpToolAllocation()
        {
            return new TestLevelSet(
                "TestLevelSet für Alle X... (TLSMTA2)",
                3,
                TestLevelSet.IntervalTypes.EveryXShifts,
                2,
                false,
                false,
                2,
                TestLevelSet.IntervalTypes.EveryXDays,
                3,
                true,
                true,
                1,
                TestLevelSet.IntervalTypes.XTimesPerMonth,
                5,
                true
                );
        }
        public static TestLevelSet GetTestLevelSetEveryXChangedForMpToolAllocation()
        {
            return new TestLevelSet(
                "TestLevelSet für Alle X... (Neu) (TLSMTA3)",
                7,
                TestLevelSet.IntervalTypes.EveryXShifts,
                3,
                false,
                true,
                1,
                TestLevelSet.IntervalTypes.EveryXDays,
                2,
                false,
                false,
                1,
                TestLevelSet.IntervalTypes.XTimesPerMonth,
                5,
                true
                );
        }

        public static CalendarEntry GetWorkingCalendarEntryChristmasHoliday()
        {
            return new CalendarEntry(new DateTime(2020, 12, 25), "Christmas Day(Fr)", true, false);
        }
        public static CalendarEntry GetWorkingCalendarEntry2ndChristmasHoliday()
        {
            return new CalendarEntry(new DateTime(2021, 12, 26), "2nd Christmas Day(So)", true, false);
        }

        public static CalendarEntry GetWorkingCalendarEntrySingleChristmasHoliday()
        {
            return new CalendarEntry(new DateTime(2020, 12, 25), "Christmas Day einmalig(Fr)", false, false);
        }
        public static CalendarEntry GetWorkingCalendarEntrySingleCompanyVacation()
        {
            return new CalendarEntry(new DateTime(2021, 06, 23), "Einmaliger Betriebsurlaub(Mi)", false, false);
        }

        public static CalendarEntry GetWorkingCalendarEntryExtraShiftHalloween()
        {
            return new CalendarEntry(new DateTime(2021, 10, 31), "Extra Schicht HW 2021(So)", false, true);
        }

        public static CalendarEntry GetWorkingCalendarEntryHolidaySaturday()
        {
            return new CalendarEntry(new DateTime(2021, 06, 26), "Urlaub 26.06.2021(Sa)", false, false);
        }
        public static CalendarEntry GetWorkingCalendarEntryHolidaySunday()
        {
            return new CalendarEntry(new DateTime(2021, 06, 27), "Urlaub 27.06.2021(So)", false, false);
        }
        public static CalendarEntry GetWorkingCalendarEntryHolidaySaturdayAnnually()
        {
            return new CalendarEntry(new DateTime(2021, 06, 26), "Frei jährlich 26.06.2021(Sa)", true, false);
        }
        public static CalendarEntry GetWorkingCalendarEntryHolidaySundayAnnually()
        {
            return new CalendarEntry(new DateTime(2021, 06, 27), "Frei jährlich 27.06.2021(So)", true, false);
        }
        public static CalendarEntry GetWorkingCalendarEntryExtraShiftSaturday()
        {
            return new CalendarEntry(new DateTime(2021, 06, 19), "Extra Schicht 19.06.2021(Sa)", false, true);
        }
        public static CalendarEntry GetWorkingCalendarEntryExtraShiftSunday()
        {
            return new CalendarEntry(new DateTime(2021, 06, 20), "Extra Schicht 20.06.2021(So)", false, true);
        }

        public static ShiftManagement GetShiftManagementDefault()
        {
            return new ShiftManagement(
                DateTime.Today.AddHours(6).AddMinutes(5),
                DateTime.Today.AddHours(13).AddMinutes(55),

                true,
                DateTime.Today.AddHours(14).AddMinutes(00),
                DateTime.Today.AddHours(22).AddMinutes(00),

                true,
                DateTime.Today.AddHours(22).AddMinutes(00),
                DateTime.Today.AddHours(6).AddMinutes(00),

                DateTime.Today.AddHours(22).AddMinutes(00),
                DayOfWeek.Monday);
        }
        public static ShiftManagement GetShiftManagementSingleShift()
        {
            return new ShiftManagement(
                DateTime.Today.AddHours(5).AddMinutes(32),
                DateTime.Today.AddHours(22).AddMinutes(07),

                false,
                new DateTime(),
                new DateTime(),

                false,
                new DateTime(),
                new DateTime(),

                DateTime.Today.AddHours(00).AddMinutes(00),
                DayOfWeek.Sunday);
        }
        public static ShiftManagement GetShiftManagementDualShift()
        {
            return new ShiftManagement(
                DateTime.Today.AddHours(4).AddMinutes(00),
                DateTime.Today.AddHours(12).AddMinutes(00),

                true,
                DateTime.Today.AddHours(12).AddMinutes(00),
                DateTime.Today.AddHours(20).AddMinutes(00),

                false,
                new DateTime(),
                new DateTime(),

                DateTime.Today.AddHours(4).AddMinutes(00),
                DayOfWeek.Wednesday);
        }
        
        public static ProcessControlConditions GetProcessControlMinTorque()
        {
            ProcessControlConditions pc = new ProcessControlConditions();
            pc.Mp = GetMpProcessControl1();

            pc.LowerInterventionLimit = 17.37;
            pc.UpperInterventionLimit = 34.3;
            pc.LowerMeasuringLimit = 15;
            pc.UpperMeasuringLimit = 35;
            pc.TestLevelSet = GetTestLevelSetEveryXProcessControl().Name;
            pc.TestLevelNumber = 3;
            pc.StartDate = DateTime.Now;
            pc.IsAuditOperationActive = true;

            pc.Method = ProcessControlConditions.Methods.QSTMinTorque;

            pc.QstMinMinimumTorque = 19;
            pc.QstMinStartAngleCount = 2;
            pc.QstMinAngleLimit = 30;
            pc.QstMinStartMeasurement = 1.5;
            pc.QstMinAlarmLimitTorque = 35;
            pc.QstMinAlarmLimitAngle = 40;

            pc.Extension = "Einfache Verlängerung";
            pc.ExtensionFactorAngle = 5;
            pc.ExtensionLengthGauge = 1.2;
            return pc;
        }
        public static ProcessControlConditions GetProcessControlPeak()
        {
            ProcessControlConditions pc = new ProcessControlConditions();
            pc.Mp = GetMpProcessControl2();
            
            pc.LowerInterventionLimit = 14;
            pc.UpperInterventionLimit = 17.5;
            pc.LowerMeasuringLimit = 13.5;
            pc.UpperMeasuringLimit = 18;
            pc.TestLevelSet = GetTestLevelSetXTimesProcessControl().Name;
            pc.TestLevelNumber = 2;
            pc.StartDate = DateTime.Now.AddDays(-2);
            pc.IsAuditOperationActive = true;

            pc.Method = ProcessControlConditions.Methods.QSTPeak;

            pc.QstPeakStartMeasurement = 5;

            pc.Extension = "doppelt gebogene Verlängerung";
            pc.ExtensionFactorAngle = 12.3;
            pc.ExtensionLengthGauge = 1.3;

            return pc;
        }
        public static ProcessControlConditions GetProcessControlPrevail()
        {
            ProcessControlConditions pc = new ProcessControlConditions();
            pc.Mp = GetMpProcessControl3();

            pc.LowerInterventionLimit = 20;
            pc.UpperInterventionLimit = 29.87;
            pc.LowerMeasuringLimit = 16;
            pc.UpperMeasuringLimit = 33;
            pc.TestLevelSet = GetTestLevelSetEveryXProcessControl().Name;
            pc.TestLevelNumber = 1;
            pc.StartDate = DateTime.Now.AddDays(5);
            pc.IsAuditOperationActive = false;

            pc.Method = ProcessControlConditions.Methods.QSTPrevail;

            pc.QstPrevailStartAngleCount = 3.1;
            pc.QstPrevailAngleForPrevail = 10;
            pc.QstPrevailTargetAngle = 14;
            pc.QstPrevailStartMeasurement = 2.7;
            pc.QstPrevailAlarmLimitTorque = 37;
            pc.QstPrevailAlarmLimitAngle = 60;

            pc.Extension = "Keine Verlängerung";
            pc.ExtensionFactorAngle = 0;
            pc.ExtensionLengthGauge = 1;
            return pc;
        }
        public static ProcessControlConditions GetProcessControlMinTorqueChanged()
        {
            ProcessControlConditions pc = new ProcessControlConditions();
            pc.Mp = GetMpProcessControl1();

            pc.LowerInterventionLimit = 18.37;
            pc.UpperInterventionLimit = 32.3;
            pc.LowerMeasuringLimit = 16;
            pc.UpperMeasuringLimit = 38;
            pc.TestLevelSet = GetTestLevelSetEveryXProcessControl().Name;
            pc.TestLevelNumber = 1;
            pc.StartDate = DateTime.Now;
            pc.IsAuditOperationActive = true;

            pc.Method = ProcessControlConditions.Methods.QSTMinTorque;

            pc.QstMinMinimumTorque = 18;
            pc.QstMinStartAngleCount = 3;
            pc.QstMinAngleLimit = 32;
            pc.QstMinStartMeasurement = 1.7;
            pc.QstMinAlarmLimitTorque = 32;
            pc.QstMinAlarmLimitAngle = 37;

            pc.Extension = "Einfache Verlängerung2";
            pc.ExtensionFactorAngle = 4.5;
            pc.ExtensionLengthGauge = 1.1;
            return pc;
        }
        public static ProcessControlConditions GetProcessControlPeakChanged()
        {
            ProcessControlConditions pc = new ProcessControlConditions();
            pc.Mp = GetMpProcessControl2();

            pc.LowerInterventionLimit = 14;
            pc.UpperInterventionLimit = 17.5;
            pc.LowerMeasuringLimit = 13.5;
            pc.UpperMeasuringLimit = 18;
            pc.TestLevelSet = GetTestLevelSetEveryXProcessControl().Name;
            pc.TestLevelNumber = 3;
            pc.StartDate = DateTime.Now.AddDays(-4);
            pc.IsAuditOperationActive = false;

            pc.Method = ProcessControlConditions.Methods.QSTPrevail;

            pc.QstPeakStartMeasurement = 5;

            pc.QstPrevailStartAngleCount = 2.73;
            pc.QstPrevailAngleForPrevail = 12;
            pc.QstPrevailTargetAngle = 15;
            pc.QstPrevailStartMeasurement = 2.6;
            pc.QstPrevailAlarmLimitTorque = 38;
            pc.QstPrevailAlarmLimitAngle = 70;

            pc.Extension = "doppelt gebogene Verlängerung Neu";
            pc.ExtensionFactorAngle = 11.3;
            pc.ExtensionLengthGauge = 1.4;

            return pc;
        }
        public static ProcessControlConditions GetProcessControlPrevailChanged()
        {
            ProcessControlConditions pc = new ProcessControlConditions();
            pc.Mp = GetMpProcessControl3();

            pc.LowerInterventionLimit = 21;
            pc.UpperInterventionLimit = 29.89;
            pc.LowerMeasuringLimit = 15.8;
            pc.UpperMeasuringLimit = 32;
            pc.TestLevelSet = GetTestLevelSetEveryXProcessControl().Name;
            pc.TestLevelNumber = 3;
            pc.StartDate = DateTime.Now.AddDays(7);
            pc.IsAuditOperationActive = true;

            pc.Method = ProcessControlConditions.Methods.QSTPrevail;

            pc.QstPrevailStartAngleCount = 3.1;
            pc.QstPrevailAngleForPrevail = 10;
            pc.QstPrevailTargetAngle = 14;
            pc.QstPrevailStartMeasurement = 2.7;
            pc.QstPrevailAlarmLimitTorque = 37;
            pc.QstPrevailAlarmLimitAngle = 60;

            pc.Extension = "Keine Verlängerung";
            pc.ExtensionFactorAngle = 0;
            pc.ExtensionLengthGauge = 1;
            return pc;
        }

        public static ToolControlConditions GetToolControlConditionsForMpToolAllocation1()
        {
            ToolControlConditions toolControlConditions = new ToolControlConditions();

            toolControlConditions.ControlledBy = ControlledBy.Torque;
            toolControlConditions.SetPointTorque = 30;
            toolControlConditions.ToleranceClassTorque = new ToleranceClass();
            toolControlConditions.MinTorque = 25;
            toolControlConditions.MaxTorque = 35.378;

            toolControlConditions.TestLevelSetChk = GetTestLevelSetEveryXForMpToolAllocation();
            toolControlConditions.TestLevelSetChkNumber = 3;
            toolControlConditions.StartDateChk = DateTime.Today.AddDays(-1);
            toolControlConditions.IsActiveChk = true;

            toolControlConditions.TestLevelSetMca = GetTestLevelSetEveryXForMpToolAllocation();
            toolControlConditions.TestLevelSetMcaNumber = 1;
            toolControlConditions.StartDateMca = DateTime.Now;
            toolControlConditions.IsActiveMca = true;

            toolControlConditions.PowEndCycleTime = 0.4;
            toolControlConditions.PowFilterFrequency = 150;
            toolControlConditions.PowCycleComplete = 5;
            toolControlConditions.PowMeasureDelayTime = 0.5;
            toolControlConditions.PowResetTime = 0.2;
            toolControlConditions.PowMustTorqueAndAngleBeBeetweenLimits = true;
            toolControlConditions.PowCycleStart = 4.5;
            toolControlConditions.PowStartFinalAngle = 5.5;

            return toolControlConditions;
        }
        public static ToolControlConditions GetToolControlConditionsForMpToolAllocation2()
        {
            ToolControlConditions toolControlConditions = new ToolControlConditions();
            toolControlConditions.ControlledBy = ControlledBy.Torque;
            toolControlConditions.SetPointTorque = 29.46;
            toolControlConditions.ToleranceClassTorque = new ToleranceClass();
            toolControlConditions.MinTorque = 23.7;
            toolControlConditions.MaxTorque = 38;

            toolControlConditions.TestLevelSetChk = GetTestLevelSetXTimesForMpToolAllocation();
            toolControlConditions.TestLevelSetChkNumber = 2;
            toolControlConditions.StartDateChk = DateTime.Today;
            toolControlConditions.IsActiveChk = false;

            toolControlConditions.TestLevelSetMca = GetTestLevelSetEveryXForMpToolAllocation();
            toolControlConditions.TestLevelSetMcaNumber = 1;
            toolControlConditions.StartDateMca = DateTime.Now.AddDays(-3);
            toolControlConditions.IsActiveMca = false;

            toolControlConditions.ClickWrenchEndCycleTime = 0.6;
            toolControlConditions.ClickWrenchFilterFrequency = 183;
            toolControlConditions.ClickWrenchCycleComplete = 13.2;
            toolControlConditions.ClickWrenchMeasureDelayTime = 0.3;
            toolControlConditions.ClickWrenchResetTime = 0.1;
            toolControlConditions.ClickWrenchCycleStart = 1;
            toolControlConditions.ClickWrenchSlipTorque= 2;

            return toolControlConditions;
        }
        public static ToolControlConditions GetToolControlConditionsForMpToolAllocation3()
        {
            ToolControlConditions toolControlConditions = new ToolControlConditions();
            toolControlConditions.ControlledBy = ControlledBy.Angle;
            toolControlConditions.ThresholdTorque = 15;
            toolControlConditions.SetPointAngle = 100;
            toolControlConditions.ToleranceClassAngle = GetMpToolAllocationToleranceClassAngle();
            toolControlConditions.MinAngle = 90;
            toolControlConditions.MaxAngle = 110;

            toolControlConditions.TestLevelSetChk = GetTestLevelSetEveryXChangedForMpToolAllocation();
            toolControlConditions.TestLevelSetChkNumber = 2;
            toolControlConditions.StartDateChk = DateTime.Today.AddDays(+2);
            toolControlConditions.IsActiveChk = true;

            toolControlConditions.TestLevelSetMca = GetTestLevelSetEveryXForMpToolAllocation();
            toolControlConditions.TestLevelSetMcaNumber = 1;
            toolControlConditions.StartDateMca = DateTime.Now;
            toolControlConditions.IsActiveMca = true;

            toolControlConditions.PulseDriverEndCycleTime = 0.4;
            toolControlConditions.PulseDriverFilterFrequency = 150;
            toolControlConditions.PulseDriverTorqueCoefficient = 0.5;
            toolControlConditions.PulseDriverMinimumPulse = 2;
            toolControlConditions.PulseDriverMaximumPulse = 37;
            toolControlConditions.PulseDriverThreshold = 14;

            return toolControlConditions;
        }
        public static ToolControlConditions GetToolControlConditionsForMpToolAllocation4()
        {
            ToolControlConditions toolControlConditions = new ToolControlConditions();
            toolControlConditions.ControlledBy = ControlledBy.Angle;
            toolControlConditions.ThresholdTorque = 15;
            toolControlConditions.SetPointAngle = 100;
            toolControlConditions.ToleranceClassAngle = GetMpToolAllocationToleranceClassAngle();
            toolControlConditions.MinAngle = 90;
            toolControlConditions.MaxAngle = 110;

            toolControlConditions.TestLevelSetChk = GetTestLevelSetEveryXChangedForMpToolAllocation();
            toolControlConditions.TestLevelSetChkNumber = 1;
            toolControlConditions.StartDateChk = DateTime.Today.AddDays(+2);
            toolControlConditions.IsActiveChk = true;

            toolControlConditions.TestLevelSetMca = GetTestLevelSetEveryXForMpToolAllocation();
            toolControlConditions.TestLevelSetMcaNumber = 1;
            toolControlConditions.StartDateMca = DateTime.Now;
            toolControlConditions.IsActiveMca = true;

            toolControlConditions.PeakEndCycleTime = 0.4;
            toolControlConditions.PeakFilterFrequency = 150;
            toolControlConditions.PeakMustTorqueAndAngleBeBetweenLimits = true;
            toolControlConditions.PeakCycleStart = 4.5;
            toolControlConditions.PeakStartFinalAngle = 5.5;

            return toolControlConditions;
        }
    }
}