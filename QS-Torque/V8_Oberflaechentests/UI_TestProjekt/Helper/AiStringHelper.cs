namespace UI_TestProjekt.Helper
{
    public static class AiStringHelper
    {
        public static class Login
        {
            public const string Window = "AI_LoginWindow";
            public const string Language = "AI_LoginView_Language";
            public const string Language_DE = "AI_LoginView_Language_DE";
            public const string Language_EN = "AI_LoginView_Language_EN";
            public const string TxtServer = "AI_LoginView_TxtServer";
            public const string Server = "AI_LoginView_Server";
            public const string Username = "AI_LoginView_Username";
            public const string Password = "AI_LoginView_Password";
            public const string BtnLogIn = "AI_LoginView_LogIn";
        }

        public static class MainWindow
        {
            public const string Window = "AI_MainWindow";
        }

        public static class GlobalToolbar
        {
            public const string Language = "AI_GlobalToolbar_Language";
            public const string LanguageDE = "AI_GlobalToolbar_Language_DE";
            public const string LanguageEN = "AI_GlobalToolbar_Language_EN";
            public const string Settings = "AI_GlobalToolbar_Settings";
            public const string AboutQST = "AI_GlobalToolbar_AboutQST";
            public const string LogOut = "AI_GlobalToolbar_Logout";
        }

        public static class MegaMainSubmodule
        {
            public const string MegaMainSubmoduleSelector = "AI_MainView_MegaMainSubmoduleSelector";
            public const string ExpandButton = "AI_GlobalTree_ExpandButton";
            public const string ExpandedButton = "AI_GlobalTree_ExpandedButton";
            public const string PinButton = "AI_GlobalTree_PinButton";
            public const string MasterData = "AI_GlobalTree_MasterDataButton";
            public const string TreeViewItemMasterData = "AI_GlobalTree_TreeViewItemMasterData";

            public const string MeasurementPoint = "AI_GlobalTree_MeasurementPoint";
            public const string ToolModel = "AI_GlobalTree_ToolModel";
            public const string Tool = "AI_GlobalTree_Tool";
            public const string MpToolAllocation = "AI_GlobalTree_MpToolAllocation";
            public const string TestEquipment = "AI_GlobalTree_TestEquipment";
            public const string TestPlanningMasterData = "AI_GlobalTree_TestPlanningMasterData";
            public const string ProcessControl = "AI_GlobalTree_ProcessControl";
            public const string AuxiliaryMasterData = "AI_GlobalTree_AuxiliaryMasterData";

            public const string MeasurementPointContainer = "AI_GlobalTree_MeasurementPointContainer";
            public const string ToolModelContainer = "AI_GlobalTree_ToolModelContainer";
            public const string ToolContainer = "AI_GlobalTree_ToolContainer";
            public const string MpToolAllocationContainer = "AI_GlobalTree_MpToolAllocationContainer";
            public const string TestEquipmentContainer = "AI_GlobalTree_TestEquipmentContainer";
            public const string TestPlanningMasterDataContainer = "AI_GlobalTree_TestPlanningMasterDataContainer";
            public const string ProcessControlContainer = "AI_GlobalTree_ProcessControlContainer";

            public enum MainSelectorTreenames
            {
                MasterData
            }
            public static class HelperTableTreeNames
            {
                public const string ShutOff = "Shut Off";
                public const string SwitchOff = "Switch Off";
                public const string DriveSize = "Drive Size";
                public const string DriveType = "Drive Type";
                public const string ToolType = "Tool Type";
                public const string ConstructionType = "Construction Type";
                public const string Status = "Status";
                public const string ReasonToolChange = "Reason for tool change";
                public const string ConfigurableFieldTool = "Configurable field tool";
                public const string Manufacturer = "Manufacturer";
                public const string ToleranceClass = "Tolerance class";
                public const string CostCenter = "Cost center";
                public const string ToolUsage = "Tool usage";
            }
        }

        public static class Assistant
        {
            public const string View = "AI_AssistentView";
            public const string Cancel = "AI_AssistentView_Cancel";
            public const string Previous = "AI_AssistentView_Previous";
            public const string Next = "AI_AssistentView_Next";
            public const string JumpHelperTable = "AI_AssistentView_JumpHelperTable";
            public const string NavigationList = "AI_AssistentView_NavigationList";

            public const string InputInteger = "AI_AssistentView_InputInteger";
            public const string InputFloatingPoint = "AI_AssistentView_InputFloatingPoint";
            public const string InputText = "AI_AssistentView_InputText";
            public const string InputBoolean = "AI_AssistentView_InputBoolean";
            public const string InputDate = "AI_AssistentView_InputDate";
            public const string InputList = "AI_AssistentView_InputList";
        }

        public static class Mp
        {
            public const string View = "AI_MpView";
            public const string AddMp = "AI_MpView_AddMp";
            public const string Delete = "AI_MpView_Delete";
            public const string SaveMp = "AI_MpView_SaveMp";
            public const string AddFolder = "AI_MpView_AddFolder";
            public const string Paste = "AI_MpView_Paste";
            public const string Cut = "AI_MpView_Cut";
            public const string MpTreeView = "AI_MpView_MPTreeView";
            public const string MpTreeViewRoot = "AI_MpView_MpTreeView_RootNode";


            public static class AddMpFolderDialog
            {
                public const string Dialog = "AI_AddMpFolderDialog";
                public const string Folder = "AI_AddMpFolderDialog_Folder";
                public const string Ok = "AI_AddMpFolderDialog_Ok";
                public const string Cancel = "AI_AddMpFolderDialog_Cancel";
            }

            public static class SingleMp
            {
                public const string ParamTab = "AI_MpView_SingleMp_ParamTab";
                public const string ParamTabScrollViewer = "AI_MpView_SingleMp_ParamTabScrollViewer";

                public const string Number = "AI_MpView_SingleMp_Number";
                public const string Description = "AI_MpView_SingleMp_Description";
                public const string ControlledBy = "AI_MpView_SingleMp_ControlledBy";

                public const string SetpointTorque = "AI_MpView_SingleMp_SetpointTorque";
                public const string ToleranceClassTorque = "AI_MpView_SingleMp_ToleranceClassTorque";
                public const string MinTorque = "AI_MpView_SingleMp_MinTorque";
                public const string MaxTorque = "AI_MpView_SingleMp_MaxTorque";

                public const string ThresholdTorque = "AI_MpView_SingleMp_ThresholdTorque";
                public const string SetpointAngle = "AI_MpView_SingleMp_SetpointAngle";
                public const string ToleranceClassAngle = "AI_MpView_SingleMp_ToleranceClassAngle";
                public const string MinAngle = "AI_MpView_SingleMp_MinAngle";
                public const string MaxAngle = "AI_MpView_SingleMp_MaxAngle";

                public const string ConfigurableField = "AI_MpView_SingleMp_ConfigurableField";
                public const string ConfigurableField2 = "AI_MpView_SingleMp_ConfigurableField2";
                public const string ConfigurableField3 = "AI_MpView_SingleMp_ConfigurableField3";
                public const string Comment = "AI_MpView_SingleMp_Comment";
            }
        }

        public static class Tool
        {
            public const string View = "AI_ToolView";
            public const string AddTool = "AI_ToolView_AddTool";
            public const string DeleteTool = "AI_ToolView_DeleteTool";
            public const string SaveTool = "AI_ToolView_SaveTool";

            public const string ToolTreeView = "AI_ToolView_ToolTreeView";
            public const string ToolTreeViewRoot = "AI_ToolView_ToolTreeView_RootNode";

            public static class SingleTool
            {
                //Parameter-Tab
                public const string ParamTab = "AI_ToolView_TabParameter";
                public const string ParamTabScrollViewer = "AI_ToolView_SingleTool_ParamTabScrollViewer";
                public const string TabCmCmk = "AI_ToolView_TabCmCmk";
                public const string TabAdditionalInformation = "AI_ToolView_TabAdditionalInformation";

                public const string InventoryNumber = "AI_ToolView_SingleTool_InventoryNumber";
                public const string SerialNumber = "AI_ToolView_SingleTool_SerialNumber";
                public const string ToolModel = "AI_ToolView_SingleTool_ToolModel";
                public const string ToolStatus = "AI_ToolView_SingleTool_ToolStatus";
                public const string Accessory = "AI_ToolView_SingleTool_Accessory";
                public const string ToolCostCenter = "AI_ToolView_SingleTool_ToolCostCenter";
                public const string ConfigurableField = "AI_ToolView_SingleTool_ConfigurableField";
                public const string Comment = "AI_ToolView_SingleTool_Comment";

                public const string ModelManufacturer = "AI_ToolView_SingleTool_ModelManufacturer";
                public const string ModelType = "AI_ToolView_SingleTool_ModelType";
                public const string ModelConstructionType = "AI_ToolView_SingleTool_ModelConstructionType";
                public const string ModelSwitchOff = "AI_ToolView_SingleTool_ModelSwitchOff";
                public const string ModelShutOff = "AI_ToolView_SingleTool_ModelShutOff";
                public const string ModelDriveSize = "AI_ToolView_SingleTool_ModelDriveSize";
                public const string ModelDriveType = "AI_ToolView_SingleTool_ModelModelDriveType";
                public const string ModelLowerPowLimit = "AI_ToolView_SingleTool_ModelLowerPowerLimit";
                public const string ModelUpperPowLimit = "AI_ToolView_SingleTool_ModelUpperPowerLimit";
                public const string ModelAirPressure = "AI_ToolView_SingleTool_ModelAirPressure";
                public const string ModelWeight = "AI_ToolView_SingleTool_ModelWeight";
                public const string ModelMaxRotSpeed = "AI_ToolView_SingleTool_ModelMaxRotSpeed";
                public const string ModelBatteryVoltage = "AI_ToolView_SingleTool_ModelBatteryVoltage";
                public const string ModelAirConsumption = "AI_ToolView_SingleTool_ModelAirConsumption";

                //CM-/CMK-Limit-Tab
                public const string CmCmkLimit = "AI_ToolView_SingleTool_CmCmkLimit";
                public const string CmCmkLimitGrid = "AI_ToolView_SingleTool_CmCmkGrid";

                //Additional Information-Tab
                public const string ConfigurableField1 = "AI_ToolView_SingleTool_ConfigurableField1";
                public const string ConfigurableField2 = "AI_ToolView_SingleTool_ConfigurableField2";
                public const string ConfigurableField3 = "AI_ToolView_SingleTool_ConfigurableField3";
            }
        }
        public static class ToolType
        {
        }
        public static class ToolModel
        {
            public const string clickWrench = "Click Wrench";
            public static class ToolModelView
            {
                public const string View = "AI_ToolModelView";
                public const string AddToolModel = "AI_ToolModelView_AddToolModel";
                public const string DeleteToolModel = "AI_ToolModelView_DeleteToolModel";
                public const string SaveToolModel = "AI_ToolModelView_SaveToolModel";
                public const string ToolModelTreeView = "AI_ToolModelView_ToolModelTreeView";
                public const string ToolModelTreeViewRoot = "AI_ToolModelView_ToolModelTreeView_RootNode";
                public const string ToolModelGrid = "AI_ToolModelView_MultiToolModelGrid";
                public const string ToolModelGridHeader = "AI_ToolModelView_DG_HeaderRow";
                public const string ToolModelGridRowPrefix = "AI_ToolModelView_DG_Row:";

                public static class SingleModel
                {
                    public const string ParamTab = "AI_ToolModelView_SingleModel_ParamTab";
                    public const string ParamTabScrollViewer = "AI_ToolModelView_SingleModel_ParamTabScrollViewer";
                    public const string SMDescription = "AI_ToolModelView_SingleModel_Description";
                    public const string SMToolModelType = "AI_ToolModelView_SingleModel_ToolModelType";
                    public const string SMClass = "AI_ToolModelView_SingleModel_Class";
                    public const string SMManu = "AI_ToolModelView_SingleModel_Manufacturer";
                    public const string SMLowLim = "AI_ToolModelView_SingleModel_LowerLimit";
                    public const string SMUpLim = "AI_ToolModelView_SingleModel_UpperLimit";
                    public const string SMAirPressure = "AI_ToolModelView_SingleModel_AirPressure";
                    public const string SMToolType = "AI_ToolModelView_SingleModel_ToolType";
                    public const string SMWeight = "AI_ToolModelView_SingleModel_Weight";
                    public const string SMMaxRot = "AI_ToolModelView_SingleModel_MaxRotSpeed";
                    public const string SMBattVolt = "AI_ToolModelView_SingleModel_BattVoltage";
                    public const string SMAirConsumpt = "AI_ToolModelView_SingleModel_AirConsumption";
                    public const string SMSwitchOff = "AI_ToolModelView_SingleModel_SwitchOff";
                    public const string SMDriveSize = "AI_ToolModelView_SingleModel_DriveSize";
                    public const string SMShutOff = "AI_ToolModelView_SingleModel_ShutOff";
                    public const string SMDriveType = "AI_ToolModelView_SingleModel_DriveType";
                    public const string SMConstrType = "AI_ToolModelView_SingleModel_ConstructionType";
                }
            }
        }

        public static class MpToolAllocation
        {
            public const string View = "AI_MpToolAllocation";
            public const string MpTreeView = "AI_MpToolAllocation_MpTreeView";
            public const string MpTreeViewRoot = "AI_MpToolAllocation_MpTreeView_RootNode";
            public const string ToolTreeView = "AI_MpToolAllocation_ToolTreeView";
            public const string ToolTreeViewRoot = "AI_MpToolAllocation_ToolTreeView_RootNode";

            public const string AllocatedToolsGrid = "AI_MpToolAllocation_AllocatedToolsGrid";
            public const string AllocatedToolsGridHeaderRow = "AI_MpToolAllocation_AllocatedToolsGrid_HeaderRow";
            public const string AllocatedToolsGridRowPrefix = "AI_MpToolAllocation_AllocatedToolsGrid_Row:";

            public const string AllocateTool = "AI_MpToolAllocation_AllocateTool";
            public const string CreateTestConditions = "AI_MpToolAllocation_CreateTestConditions";
            public const string RemoveToolAllocation = "AI_MpToolAllocation_RemoveToolAllocation";
            public const string SaveToolAllocation = "AI_MpToolAllocation_SaveLocationToolAssignement";
            public const string ShowToolDetails = "AI_MpToolAllocation_ShowToolDetails";

            public const string MpParamTab = "AI_MpToolAllocation_MpParamTab";
            public const string MpToolAllocTab = "AI_MpToolAllocation_MpToolAllocTab";
            public const string TestConditionsTab = "AI_MpToolAllocation_TestConditionsTab";
            public const string ToolParamTab = "AI_MpToolAllocation_ToolParamTab";

            public const string AllToolsTab = "AI_MpToolAllocation_AllToolsTab";
            public const string AllocatedToolsTab = "AI_MpToolAllocation_AllocatedToolsTab";

            public static class MpToolAllocTabElements
            {
                public const string MeasurementPoint = "AI_MpToolAllocation_MpToolAllocTab_Mp";
                public const string Tool = "AI_MpToolAllocation_MpToolAllocTab_Tool";
                public const string ToolUsage = "AI_MpToolAllocation_MpToolAllocTab_ToolUsage";
            }
            public static class MpParamTabElements
            {
                public const string ScrollViewer = "AI_MpToolAllocation_MpParamTab_ScrollViewer";
                public const string Number = "AI_MpToolAllocation_MpParamTab_Number";
                public const string Description = "AI_MpToolAllocation_MpParamTab_Description";
                public const string ControlledByTorque = "AI_MpToolAllocation_MpParamTab_ControlledByTorque";
                public const string ControlledByAngle = "AI_MpToolAllocation_MpParamTab_ControlledByAngle";
                public const string SetPointTorque = "AI_MpToolAllocation_MpParamTab_SetPointTorque";
                public const string ToleranceClassTorque = "AI_MpToolAllocation_MpParamTab_ToleranceClassTorque";
                public const string MinTorque = "AI_MpToolAllocation_MpParamTab_MinimumTorque";
                public const string MaxTorque = "AI_MpToolAllocation_MpParamTab_MaximumTorque";
                public const string ThresholdTorque = "AI_MpToolAllocation_MpParamTab_ThresholdTorque";
                public const string SetPointAngle = "AI_MpToolAllocation_MpParamTab_SetPointAngle";
                public const string ToleranceClassAngle = "AI_MpToolAllocation_MpParamTab_ToleranceClassAngle";
                public const string MinAngle = "AI_MpToolAllocation_MpParamTab_MinimumAngle";
                public const string MaxAngle = "AI_MpToolAllocation_MpParamTab_MaximumAngle";
                public const string ConfigurableField1 = "AI_MpToolAllocation_MpParamTab_ConfigurableField1";
                public const string ConfigurableField2 = "AI_MpToolAllocation_MpParamTab_ConfigurableField2";
                public const string ConfigurableField3 = "AI_MpToolAllocation_MpParamTab_ConfigurableField3";
                public const string Comment = "AI_MpToolAllocation_MpParamTab_Comment";
            }
            public static class ToolParamTabElements
            {
                public const string ScrollViewer = "AI_MpToolAllocation_ToolParamTab_ScrollViewer";
                public const string InventoryNumber = "AI_MpToolAllocation_ToolParamTab_InventoryNumber";
                public const string SerialNumber = "AI_MpToolAllocation_ToolParamTab_SerialNumber";
                public const string ModelDescription = "AI_MpToolAllocation_ToolParamTab_ModelDescription";
                public const string Status = "AI_MpToolAllocation_ToolParamTab_Status";
                public const string Accessory = "AI_MpToolAllocation_ToolParamTab_Accessory";
                public const string CostCenter = "AI_MpToolAllocation_ToolParamTab_CostCenter";
                public const string ConfigurableField = "AI_MpToolAllocation_ToolParamTab_ConfigurableField";
                public const string Comment = "AI_MpToolAllocation_ToolParamTab_Comment";
                public const string ModelManufacturer = "AI_MpToolAllocation_ToolParamTab_ModelManufacturer";
                public const string ModelToolType = "AI_MpToolAllocation_ToolParamTab_ModelToolType";
                public const string ModelConstructionType = "AI_MpToolAllocation_ToolParamTab_ModelConstructionType";
                public const string ModelSwitchOff = "AI_MpToolAllocation_ToolParamTab_ModelSwitchOff";
                public const string ModelShutOff = "AI_MpToolAllocation_ToolParamTab_ModelShutOff";
                public const string ModelDriveSize = "AI_MpToolAllocation_ToolParamTab_ModelDriveSize";
                public const string ModelDriveType = "AI_MpToolAllocation_ToolParamTab_ModelDriveType";
                public const string ModelLowerPowerLimit = "AI_MpToolAllocation_ToolParamTab_ModelLowerPowerLimit";
                public const string ModelUpperPowerLimit = "AI_MpToolAllocation_ToolParamTab_ModelUpperPowerLimit";
                public const string ModelAirPressure = "AI_MpToolAllocation_ToolParamTab_ModelAirPressure";
                public const string ModelWeight = "AI_MpToolAllocation_ToolParamTab_ModelWeight";
                public const string ModelMaxRotationSpeed = "AI_MpToolAllocation_ToolParamTab_ModelMaxRotationSpeed";
                public const string ModelBatteryVoltage = "AI_MpToolAllocation_ToolParamTab_ModelBatteryVoltage";
                public const string ModelAirConsumption = "AI_MpToolAllocation_ToolParamTab_ModelAirConsumption";
            }
            public static class TestConditionsTabElements
            {
                public const string ScrollViewer = "AI_MpToolAllocation_TestConditionsTab_ScrollViewer";

                public const string ControlledBy = "AI_MpToolAllocation_TestConditionsTab_ControlledBy";
                public const string SetPointTorque = "AI_MpToolAllocation_TestConditionsTab_SetPointTorque";
                public const string ToleranceClassTorque = "AI_MpToolAllocation_TestConditionsTab_ToleranceClassTorque";
                public const string MinTorque = "AI_MpToolAllocation_TestConditionsTab_MinTorque";
                public const string MaxTorque = "AI_MpToolAllocation_TestConditionsTab_MaxTorque";

                public const string ThresholdTorque = "AI_MpToolAllocation_TestConditionsTab_ThresholdTorque";
                public const string SetPointAngle = "AI_MpToolAllocation_TestConditionsTab_SetPointAngle";
                public const string ToleranceClassAngle = "AI_MpToolAllocation_TestConditionsTab_ToleranceClassAngle";
                public const string MinAngle = "AI_MpToolAllocation_TestConditionsTab_MinAngle";
                public const string MaxAngle = "AI_MpToolAllocation_TestConditionsTab_MaxAngle";

                public const string TestLevelSetChk = "AI_MpToolAllocation_TestConditionsTab_TestLevelSetChk";
                public const string TestLevelSetNumberChk = "AI_MpToolAllocation_TestConditionsTab_TestLevelSetNumberChk";
                public const string StartDateChk = "AI_MpToolAllocation_TestConditionsTab_StartDateChk";
                public const string TestOperationActiveChk = "AI_MpToolAllocation_TestConditionsTab_TestOperationActiveChk";

                public const string TestLevelSetMca = "AI_MpToolAllocation_TestConditionsTab_TestLevelSetMca";
                public const string TestLevelSetNumberMca = "AI_MpToolAllocation_TestConditionsTab_TestLevelSetNumberMca";
                public const string StartDateMca = "AI_MpToolAllocation_TestConditionsTab_StartDateMca";
                public const string TestOperationActiveMca = "AI_MpToolAllocation_TestConditionsTab_TestOperationActiveMca";

                //TestTechnique
                public const string ClickWrenchEndCycleTime = "AI_MpToolAllocation_TestConditionsTab_ClickWrenchEndCycleTime";
                public const string ClickWrenchFilterFrequency = "AI_MpToolAllocation_TestConditionsTab_ClickWrenchFilterFrequency";
                public const string ClickWrenchCycleComplete = "AI_MpToolAllocation_TestConditionsTab_ClickWrenchCycleComplete";
                public const string ClickWrenchMeasureDelayTime = "AI_MpToolAllocation_TestConditionsTab_ClickWrenchMeasureDelayTime";
                public const string ClickWrenchResetTime = "AI_MpToolAllocation_TestConditionsTab_ClickWrenchResetTime";
                public const string ClickWrenchCycleStart = "AI_MpToolAllocation_TestConditionsTab_ClickWrenchCycleStart";
                public const string ClickWrenchSlipTorque = "AI_MpToolAllocation_TestConditionsTab_ClickWrenchSlipTorque";

                public const string PowToolEndCycleTime = "AI_MpToolAllocation_TestConditionsTab_PowToolEndCycleTime";
                public const string PowToolFilterFrequency = "AI_MpToolAllocation_TestConditionsTab_PowToolFilterFrequency";
                public const string PowToolCycleComplete = "AI_MpToolAllocation_TestConditionsTab_PowToolCycleComplete";
                public const string PowToolMeasureDelayTime = "AI_MpToolAllocation_TestConditionsTab_PowToolMeasureDelayTime";
                public const string PowToolResetTime = "AI_MpToolAllocation_TestConditionsTab_PowToolResetTime";
                public const string PowToolMustTorqueAngleBeInLimits = "AI_MpToolAllocation_TestConditionsTab_PowToolMustTorqueAngleBeInLimits";
                public const string PowToolCycleStart = "AI_MpToolAllocation_TestConditionsTab_PowToolCycleStart";
                public const string PowToolStartFinalAngle = "AI_MpToolAllocation_TestConditionsTab_PowToolStartFinalAngle";

                public const string PulseDriverEndCycleTime = "AI_MpToolAllocation_TestConditionsTab_PulseDriverEndCycleTime";
                public const string PulseDriverFilterFrequency = "AI_MpToolAllocation_TestConditionsTab_PulseDriverFilterFrequency";
                public const string PulseDriverTorqueCoefficient = "AI_MpToolAllocation_TestConditionsTab_PulseDriverTorqueCoefficient";
                public const string PulseDriverMinimumPulse = "AI_MpToolAllocation_TestConditionsTab_PulseDriverMinimumPulse";
                public const string PulseDriverMaximumPulse = "AI_MpToolAllocation_TestConditionsTab_PulseDriverMaximumPulse";
                public const string PulseDriverThreshold = "AI_MpToolAllocation_TestConditionsTab_PulseDriverThreshold";

                public const string PeakEndCycleTime = "AI_MpToolAllocation_TestConditionsTab_PeakEndCycleTime";
                public const string PeakFilterFrequency = "AI_MpToolAllocation_TestConditionsTab_PeakFilterFrequency";
                public const string PeakMustTorqueAngleBeInLimits = "AI_MpToolAllocation_TestConditionsTab_PeakMustTorqueAngleBeInLimits";
                public const string PeakCycleStart = "AI_MpToolAllocation_TestConditionsTab_PeakCycleStart";
                public const string PeakStartFinalAngle = "AI_MpToolAllocation_TestConditionsTab_PeakStartFinalAngle";
            }
        }

        public static class TestEquipment
        {
            public const string View = "AI_TestEquipmentView";
            public const string TestEquipmentTreeView = "AI_TeView_TeTreeView";
            public const string TestEquipmentTreeViewRoot = "AI_TeView_TeTreeView_RootNode";
            public const string Add = "AI_TeView_AddTestEquipment";
            public const string Delete = "AI_TeView_DeleteTestEquipment";
            public const string Save = "AI_TeView_SaveTestEquipment";

            public const string TeTab = "AI_TeView_TestEquipmentTab";
            public const string TeFeaturesTab = "AI_TeView_TestEquipmentFeaturesTab";
            public const string TeModelTab = "AI_TeView_TestEquipmentModelTab";


            public static class TestEquipmentTabElements
            {
                public const string ScrollViewer = "AI_TeView_TestEquipmentTab_ScrollViewer";
                public const string SerialNumber = "AI_TeView_TestEquipmentTab_SerialNumber";
                public const string InventoryNumber = "AI_TeView_TestEquipmentTab_InventoryNumber";
                public const string TestEquipmentModel = "AI_TeView_TestEquipmentTab_TestEquipmentModel";
                public const string Manufacturer = "AI_TeView_TestEquipmentTab_Manufacturer";
                public const string DataGateVersion = "AI_TeView_TestEquipmentTab_DataGateVersion";
                public const string Status = "AI_TeView_TestEquipmentTab_Status";
                public const string FirmwareVersion = "AI_TeView_TestEquipmentTab_FirmwareVersion";

                public const string LastCalibration = "AI_TeView_TestEquipmentTab_LastCalibration";
                public const string CalibrationInterval = "AI_TeView_TestEquipmentTab_CalibrationInterval";
                public const string NextCalibration = "AI_TeView_TestEquipmentTab_NextCalibration";
                public const string CalibrationNorm = "AI_TeView_TestEquipmentTab_Calibration";

                public const string MinCapacity = "AI_TeView_TestEquipmentTab_MinCapacity";
                public const string MaxCapacity = "AI_TeView_TestEquipmentTab_MaxCapacity";

                public const string UseForProcess = "AI_TeView_TestEquipmentTab_UseForProcess";
                public const string UseForRotating = "AI_TeView_TestEquipmentTab_UseForRotating";
            }
            public static class TestEquipmentFeaturesTabElements
            {
                public const string ScrollViewer = "AI_TeView_TestEquipmentFeaturesTab_ScrollViewer";
                public const string TransferUser = "AI_TeView_TeFeaturesTab_TransferUser";
                public const string TransferUserToolTip = "AI_TeView_TeFeaturesTab_TransferUserTT";
                public const string TransferAdapter = "AI_TeView_TeFeaturesTab_TransferAdapter";
                public const string TransferAdapterToolTip = "AI_TeView_TeFeaturesTab_TransferAdapterTT";
                public const string TransferTransducer = "AI_TeView_TeFeaturesTab_TransferTransducer";
                public const string TransferTransducerToolTip = "AI_TeView_TeFeaturesTab_TransferTransducerTT";
                public const string TransferAttributes = "AI_TeView_TeFeaturesTab_TransferAttributes";
                public const string TransferAttributesToolTip = "AI_TeView_TeFeaturesTab_TransferAttributesTT";
                public const string TransferPictures = "AI_TeView_TeFeaturesTab_TransferPictures";
                public const string TransferPicturesToolTip = "AI_TeView_TeFeaturesTab_TransferPicturesTT";
                public const string TransferNewLimits = "AI_TeView_TeFeaturesTab_TransferNewLimits";
                public const string TransferNewLimitsToolTip = "AI_TeView_TeFeaturesTab_TransferNewLimitsTT";
                public const string TransferCurves = "AI_TeView_TeFeaturesTab_TransferCurves";
                public const string TransferCurvesToolTip = "AI_TeView_TeFeaturesTab_TransferCurvesTT";

                public const string AskForIdent = "AI_TeView_TeFeaturesTab_AskForIdent";
                public const string AskForIdentToolTip = "AI_TeView_TeFeaturesTab_AskForIdentTT";
                public const string AskForSign = "AI_TeView_TeFeaturesTab_AskForSign";
                public const string AskForSignToolTip = "AI_TeView_TeFeaturesTab_AskForSignTT";
                public const string UseErrorCodes = "AI_TeView_TeFeaturesTab_UseErrorCodes";
                public const string UseErrorCodesToolTip = "AI_TeView_TeFeaturesTab_UseErrorCodesTT";
                public const string PerformLooseCheck = "AI_TeView_TeFeaturesTab_PerformLooseCheck";
                public const string PerformLooseCheckToolTip = "AI_TeView_TeFeaturesTab_PerformLooseCheckTT";
                public const string MpCanBeDeleted = "AI_TeView_TeFeaturesTab_MpCanBeDeleted";
                public const string MpCanBeDeletedToolTip = "AI_TeView_TeFeaturesTab_MpCanBeDeletedTT";
                public const string ConfirmMp = "AI_TeView_TeFeaturesTab_ConfirmMp";
                public const string ConfirmMpToolTip = "AI_TeView_TeFeaturesTab_ConfirmMpTT";
                public const string StandardMethodsCanBeUsed = "AI_TeView_TeFeaturesTab_StandardMethodesCanBeUsed";
                public const string StandardMethodesCanBeUsedToolTip = "AI_TeView_TeFeaturesTab_StandardMethodesCanBeUsedTT";

            }
            public static class TestEquipmentModelTabElements
            {
                public const string ScrollViewer = "AI_TeView_TestEquipmentModelTab_ScrollViewer";
                public const string Name = "AI_TeView_TestEquipmentModelTab_Name";
                public const string DataGateVersion = "AI_TeView_TestEquipmentModelTab_DataGateVersion";
                public const string UseForProcess = "AI_TeView_TestEquipmentModelTab_UseForProcess";
                public const string UseForRotating = "AI_TeView_TestEquipmentModelTab_UseForRotating";

                public const string TransferUser = "AI_TeView_TestEquipmentModelTab_TransferUser";
                public const string TransferAdapter = "AI_TeView_TestEquipmentModelTab_TransferAdapter";
                public const string TransferTransducer = "AI_TeView_TestEquipmentModelTab_TransferTransducer";
                public const string TransferAttributes = "AI_TeView_TestEquipmentModelTab_TransferAttributes";
                public const string TransferPictures = "AI_TeView_TestEquipmentModelTab_TransferPictures";
                public const string TransferNewLimits = "AI_TeView_TestEquipmentModelTab_TransferNewLimits";
                public const string TransferCurves = "AI_TeView_TestEquipmentModelTab_TransferCurves";
                public const string AskForIdent = "AI_TeView_TestEquipmentModelTab_AskForIdent";
                public const string AskForSign = "AI_TeView_TestEquipmentModelTab_AskForSign";
                public const string UseErrorCodes = "AI_TeView_TestEquipmentModelTab_UseErrorCodes";
                public const string PerformLooseCheck = "AI_TeView_TestEquipmentModelTab_PerformLooseCheck";
                public const string MpCanBeDeleted = "AI_TeView_TestEquipmentModelTab_MpCanBeDeleted";
                public const string ConfirmMp = "AI_TeView_TestEquipmentModelTab_ConfirmMp";
                public const string StandardMethodsCanBeUsed = "AI_TeView_TestEquipmentModelTab_StandardMethodesCanBeUsed";

                public const string TeDriver = "AI_TeView_TestEquipmentModelTab_TeDriver";
                public const string TeDriverExplorerBtn = "AI_TeView_TestEquipmentModelTab_TeDriverExplorerBtn";
                public const string StatusFile = "AI_TeView_TestEquipmentModelTab_StatusFile";
                public const string StatusFileExplorerBtn = "AI_TeView_TestEquipmentModelTab_StatusFileExplorerBtn";
                public const string QstToTeFile = "AI_TeView_TestEquipmentModelTab_QstToTeFile";
                public const string QstToTeFileExplorerBtn = "AI_TeView_TestEquipmentModelTab_QstToTeFileExplorerBtn";
                public const string TeToQstFile = "AI_TeView_TestEquipmentModelTab_TeToQstFile";
                public const string TeToQstFileExplorerBtn = "AI_TeView_TestEquipmentModelTab_TeToQstFileExplorerBtn";
            }
        }
        public static class TestPlanningMasterData
        {
            public const string View = "AI_TestLevelSetView";
            public const string TestLevelSetsTab = "AI_TLSView_TestLevelSetsTab";
            public const string WorkingCalendarTab = "AI_TLSView_WorkingCalendarTab";
            public const string ShiftManagementTab = "AI_TLSView_ShiftManagementTab";

            public static class TestLevelSetsTabElements
            {
                public const string ListBox = "AI_TLSView_TestLevelSetsTab_ListBox";
                public const string Add = "AI_TLSView_TestLevelSetsTab_Add";
                public const string Delete = "AI_TLSView_TestLevelSetsTab_Delete";
                public const string Save = "AI_TLSView_TestLevelSetsTab_Save";

                public const string Name = "AI_TLSView_TestLevelSetsTab_Name";

                public const string Interval1 = "AI_TLSView_TestLevelSetsTab_Interval1";
                public const string IntervalType1 = "AI_TLSView_TestLevelSetsTab_IntervalType1";
                public const string SampleNumber1 = "AI_TLSView_TestLevelSetsTab_SampleNumber1";
                public const string ConsiderWorkingCalendar1 = "AI_TLSView_TestLevelSetsTab_ConsiderWorkingCalendar1";

                public const string TestLevelSet2Active = "AI_TLSView_TestLevelSetsTab_TestLevelSet2Active";
                public const string Interval2 = "AI_TLSView_TestLevelSetsTab_Interval2";
                public const string IntervalType2 = "AI_TLSView_TestLevelSetsTab_IntervalType2";
                public const string SampleNumber2 = "AI_TLSView_TestLevelSetsTab_SampleNumber2";
                public const string ConsiderWorkingCalendar2 = "AI_TLSView_TestLevelSetsTab_ConsiderWorkingCalendar2";

                public const string TestLevelSet3Active = "AI_TLSView_TestLevelSetsTab_TestLevelSet3Active";
                public const string Interval3 = "AI_TLSView_TestLevelSetsTab_Interval3";
                public const string IntervalType3 = "AI_TLSView_TestLevelSetsTab_IntervalType3";
                public const string SampleNumber3 = "AI_TLSView_TestLevelSetsTab_SampleNumber3";
                public const string ConsiderWorkingCalendar3 = "AI_TLSView_TestLevelSetsTab_ConsiderWorkingCalendar3";

            }
            public static class WorkingCalendarTabElements
            {
                public const string AddCalendarEntry = "AI_TLSView_WorkingCalendarTab_AddCalendarEntry";
                public const string RemoveEntry = "AI_TLSView_WorkingCalendarTab_RemoveEntry";

                public const string SingleEntriesGrid = "AI_TLSView_WorkingCalendarTab_SingleEntriesGrid";
                public const string AnnuallyEntriesGrid = "AI_TLSView_WorkingCalendarTab_AnnuallyEntriesGrid";
                public const string Calendar = "AI_TLSView_WorkingCalendarTab_Calendar";
                public const string SaturdayWorkfree = "AI_TLSView_WorkingCalendarTab_SaturdayWorkfree";
                public const string SundayWorkfree = "AI_TLSView_WorkingCalendarTab_SundayWorkfree";

                public const string AnnuallyGridHeader = "AI_TLSView_WorkingCalendarTab_AnnuallyDG_HeaderRow";
                public const string AnnuallyGridRowPrefix = "AI_TLSView_WorkingCalendarTab_AnnuallyDG_Row:";

                public const string SingleEntriesGridHeader = "AI_TLSView_WorkingCalendarTab_SingleEntryDG_HeaderRow";
                public const string SingleEntriesGridRowPrefix = "AI_TLSView_WorkingCalendarTab_SingleEntryDG_Row:";
            }

            public static class ShiftManagementTabElements
            {
                public const string Save = "AI_TLSView_ShiftManagementTab_Save";

                public const string FirstShiftFrom = "AI_TLSView_ShiftManagementTab_FirstShiftFrom";
                public const string FirstShiftTo = "AI_TLSView_ShiftManagementTab_FirstShiftTo";
                public const string FirstShiftFromText = "AI_TLSView_ShiftManagementTab_FirstShiftFromText";
                public const string FirstShiftToText = "AI_TLSView_ShiftManagementTab_FirstShiftToText";

                public const string SecondShiftActive = "AI_TLSView_ShiftManagementTab_SecondShiftActive";
                public const string SecondShiftFrom = "AI_TLSView_ShiftManagementTab_SecondShiftFrom";
                public const string SecondShiftTo = "AI_TLSView_ShiftManagementTab_SecondShiftTo";
                public const string SecondShiftFromText = "AI_TLSView_ShiftManagementTab_SecondShiftFromText";
                public const string SecondShiftToText = "AI_TLSView_ShiftManagementTab_SecondShiftToText";

                public const string ThirdShiftActive = "AI_TLSView_ShiftManagementTab_ThirdShiftActive";
                public const string ThirdShiftFrom = "AI_TLSView_ShiftManagementTab_ThirdShiftFrom";
                public const string ThirdShiftTo = "AI_TLSView_ShiftManagementTab_ThirdShiftTo";
                public const string ThirdShiftFromText = "AI_TLSView_ShiftManagementTab_ThirdShiftFromText";
                public const string ThirdShiftToText = "AI_TLSView_ShiftManagementTab_ThirdShiftToText";

                public const string ChangeOfDay = "AI_TLSView_ShiftManagementTab_ChangeOfDay";
                public const string ChangeOfDayText = "AI_TLSView_ShiftManagementTab_ChangeOfDayText";
                public const string FirstDayOfWeek = "AI_TLSView_ShiftManagementTab_FirstDayOfWeek";
            }
        }

        public static class ProcessControl
        {
            public const string View = "AI_ProcessControlView";
            public const string Add = "AI_ProcessControlView_Add";
            public const string Delete = "AI_ProcessControlView_Delete";
            public const string Save = "AI_ProcessControlView_Save";
            public const string TreeView = "AI_ProcessControlView_ProcessControlTreeView";
            public const string TreeViewRootNode = "AI_ProcessControlView_ProcessControlTreeViewRootNode";

            public const string ProcessConditionTab = "AI_ProcessControlView_ProcessConditionTab";
            public const string LocationParameterTab = "AI_ProcessControlView_LocationParameterTab";

            public static class ProcessConditionTabElements
            {
                public const string Scrollviewer = "AI_PCV_ProcessConditionTab_Scrollviewer";
                public const string LowerInterventionLimit = "AI_PCV_ProcessConditionTab_LowerInterventionLimit";
                public const string UpperInterventionLimit = "AI_PCV_ProcessConditionTab_UpperInterventionLimit";
                public const string LowerMeasuringLimit = "AI_PCV_ProcessConditionTab_LowerMeaseringLimit";
                public const string UpperMeasuringLimit = "AI_PCV_ProcessConditionTab_UpperMeaseringLimit";
                public const string TestLevelSet = "AI_PCV_ProcessConditionTab_TestLevelSet";
                public const string TestLevelSetNumber = "AI_PCV_ProcessConditionTab_TestLevelSetNumber";
                public const string StartDate = "AI_PCV_ProcessConditionTab_StartDate";
                public const string AuditOperationActive = "AI_PCV_ProcessConditionTab_AuditOperationActive";

                public const string TechniqueExpander = "AI_PCV_ProcessConditionTab_TechniqueExpander";
                public const string Method = "AI_PCV_ProcessConditionTab_Method";

                public const string QSTMin_MinimumTorque = "AI_PCV_ProcessConditionTab_QSTMin_MinimumTorque";
                public const string QSTMin_StartAngleCount = "AI_PCV_ProcessConditionTab_QSTMin_StartAngleCount";
                public const string QSTMin_AngleLimit = "AI_PCV_ProcessConditionTab_QSTMin_AngleLimit";
                public const string QSTMin_StartMeasurement = "AI_PCV_ProcessConditionTab_QSTMin_StartMeasurement";
                public const string QSTMin_AlarmLimitTorque = "AI_PCV_ProcessConditionTab_QSTMin_AlarmLimitTorque";
                public const string QSTMin_AlarmLimitAngle = "AI_PCV_ProcessConditionTab_QSTMin_AlarmLimitAngle";

                public const string QSTPeak_StartMeasurement = "AI_PCV_ProcessConditionTab_QSTPeak_StartMeasurement";

                public const string QSTPrevail_StartAngleCount = "AI_PCV_ProcessConditionTab_QSTPrevail_StartAngleCount";
                public const string QSTPrevail_AnglePrevailTorque = "AI_PCV_ProcessConditionTab_QSTPrevail_AnglePrevailTorque";
                public const string QSTPrevail_TargetAngle = "AI_PCV_ProcessConditionTab_QSTPrevail_TargetAngle";
                public const string QSTPrevail_StartMeasurement = "AI_PCV_ProcessConditionTab_QSTPrevail_StartMeasurement";
                public const string QSTPrevail_AlarmLimitTorque = "AI_PCV_ProcessConditionTab_QSTPrevail_AlarmLimitTorque";
                public const string QSTPrevail_AlarmLimitAngle = "AI_PCV_ProcessConditionTab_QSTPrevail_AlarmLimitAngle";

                public const string Extension = "AI_PCV_ProcessConditionTab_Extension";
                public const string ExtensionFactorAngle = "AI_PCV_ProcessConditionTab_ExtensionFactorAngle";
                public const string LengthGauge = "AI_PCV_ProcessConditionTab_LengthGauge";
            }
            public static class LocationParameterTabElements
            {
                public const string Scrollviewer = "AI_PCV_LocationParameterTab_Scrollviewer";
                public const string Number = "AI_PCV_LocationParameterTab_Number";
                public const string Description = "AI_PCV_LocationParameterTab_Description";
                public const string ControlledBy = "AI_PCV_LocationParameterTab_ControlledBy";
                public const string SetPointTorque = "AI_PCV_LocationParameterTab_SetPointTorque";
                public const string ToleranceClassTorque = "AI_PCV_LocationParameterTab_ToleranceClassTorque";
                public const string MinimumTorque = "AI_PCV_LocationParameterTab_MinimumTorque";
                public const string MaximumTorque = "AI_PCV_LocationParameterTab_MaximumTorque";
                public const string ThresholdTorque = "AI_PCV_LocationParameterTab_ThresholdTorque";
                public const string SetPointAngle = "AI_PCV_LocationParameterTab_SetPointAngle";
                public const string ToleranceClassAngle = "AI_PCV_LocationParameterTab_ToleranceClassAngle";
                public const string MinimumAngle = "AI_PCV_LocationParameterTab_MinimumAngle";
                public const string MaximumAngle = "AI_PCV_LocationParameterTab_MaximumAngle";
                public const string ConfigurableField1 = "AI_PCV_LocationParameterTab_ConfigurableField1";
                public const string ConfigurableField2 = "AI_PCV_LocationParameterTab_ConfigurableField2";
                public const string AConfigurableField3 = "AI_PCV_LocationParameterTab_ConfigurableField3";
                public const string Comment = "AI_PCV_LocationParameterTab_Comment";
            }
        }

        public static class Manufacturer
        {
            public const string View = "AI_ManufacturerView";
            public const string AddManufacturer = "AI_ManufacturerView_AddManufacturer";
            public const string DeleteManufacturer = "AI_ManufacturerView_DeleteManufacturer";
            public const string SaveManufacturer = "AI_ManufacturerView_SaveManufacturer";
            public const string ManufacturerListBox = "AI_ManufacturerView_ManuListBox";
            public const string ManufacturerGrid = "AI_ManufacturerView_MultiManuGrid";
            public const string ManufacturerGridHeader = "AI_ManufacturerView_DG_HeaderRow";
            public const string ManufacturerGridRowPrefix = "AI_ManufacturerView_DG_Row:";

            public static class SingleManufacturer
            {
                public const string SingleManuScrollViewer = "AI_ManufacturerView_SingleManuScrollViewer";
                public const string Name = "AI_ManufacturerView_SingleManuGrid_Name";
                public const string ContactPerson = "AI_ManufacturerView_SingleManuGrid_Person";
                public const string PhoneNumber = "AI_ManufacturerView_SingleManuGrid_PhoneNumber";
                public const string Fax = "AI_ManufacturerView_SingleManuGrid_Fax";
                public const string Street = "AI_ManufacturerView_SingleManuGrid_Street";
                public const string HouseNumber = "AI_ManufacturerView_SingleManuGrid_HouseNumber";
                public const string ZipCode = "AI_ManufacturerView_SingleManuGrid_Plz";
                public const string City = "AI_ManufacturerView_SingleManuGrid_City";
                public const string Country = "AI_ManufacturerView_SingleManuGrid_Country";
                public const string Comment = "AI_ManufacturerView_SingleManuGrid_Comment";
            }
        }

        public static class ToleranceClass
        {
            public const string View = "AI_ToleranceClassView";
            public const string AddToleranceClass = "AI_ToleranceClassView_AddToleranceClass";
            public const string DeleteToleranceClass = "AI_ToleranceClassView_DeleteToleranceClass";
            public const string SaveToleranceClass = "AI_ToleranceClassView_SaveToleranceClass";
            public const string ListBox = "AI_ToleranceClassView_ToleranceClassListBox";

            public static class SingleToleranceClass
            {
                public const string Grid = "AI_ToleranceClassView_SingleToleranceClass_Grid";
                public const string ScrollViewer = "AI_ToleranceClassView_SingleToleranceClass_Grid_ScrollViewer";
                public const string Name = "AI_ToleranceClassView_SingleToleranceClass_Name";
                public const string RdBtnRelative = "AI_ToleranceClassView_SingleToleranceClass_RdBtnToleranceRelative";
                public const string RdBtnAbsolute = "AI_ToleranceClassView_SingleToleranceClass_RdBtnToleranceAbsolute";
                public const string SymmetricalLimits = "AI_ToleranceClassView_SingleToleranceClass_SymmetricalLimits";
                public const string LowerUpperLimit = "AI_ToleranceClassView_SingleToleranceClass_SymmetricLowerUpperLimit";
                public const string LowerLimit = "AI_ToleranceClassView_SingleToleranceClass_LowerLimit";
                public const string UpperLimit = "AI_ToleranceClassView_SingleToleranceClass_UpperLimit";

                public const string ReferencedMpsExpander = "AI_ToleranceClassView_SingleToleranceClass_ReferencedMpsExpander";
                public const string ReferencedMpsList = "AI_ToleranceClassView_SingleToleranceClass_ReferencedMpsList";
                public const string ReferencedMpToolAssignementsExpander = "AI_ToleranceClassView_SingleToleranceClass_ReferencedMpToolAssignementsExpander";
                public const string ReferencedMpToolAssignementsDG = "AI_ToleranceClassView_SingleToleranceClass_ReferencedMpToolAssignementsDG";
            }
        }

        public static class HelperTable
        {
            public const string View = "AI_HelperTableView";
            public const string AddHelper = "AI_HelperView_AddHelper";
            public const string SaveHelper = "AI_HelperView_SaveHelper";
            public const string DeleteHelper = "AI_HelperView_DeleteHelper";

            public const string HelperInput = "AI_HelperTableView_HelperInput";
            public const string HelperListBox = "AI_HelperTableView_HelperListBox";
        }

        public static class ChangeToolState
        {
            public const string View = "AI_ChangeToolStateView";
            public const string MpToolReferenceDG = "AI_ChangeToolStateView_MpToolReferenceDG";
            public const string OtherAssignedLocations = "AI_ChangeToolStateView_OtherAssignedLocations";
            public const string JumpStatus = "AI_ChangeToolStateView_JumpStatus";
            public const string Cancel = "AI_ChangeToolStateView_Cancel";
            public const string Apply = "AI_ChangeToolStateView_Apply";
        }
        

        public static class VerifyChanges
        { 
            public const string View = "AI_VerifyChangesView";
            public const string Apply = "AI_VerifyChangesView_ApplyChanges";
            public const string Reset = "AI_VerifyChangesView_ResetChanges";
            public const string Cancel = "AI_VerifyChangesView_CancelChanges";
            public const string Comment = "AI_VerifyChangesView_Comment";
            public const string ChangesListView = "AI_VerifyChangesView_ChangesListView";
        }
        
        public static class GeneralStrings
        {
            public const string ScrollViewerLeftBtn = "PART_LineLeftButton";
            public const string ScrollViewerRightBtn = "PART_LineRightButton";
            public const string ScrollViewerAreaLeft = "PageLeft";
            public const string ScrollViewerAreaRight = "PageRight";
            public const string ScrollViewerHorizontalScrollbar = "HorizontalScrollBar";

            public const string ScrollViewerUpBtn = "PART_LineUpButton";
            public const string ScrollViewerDownBtn = "PART_LineDownButton";
            public const string ScrollViewerAreaUp = "PageUp";
            public const string ScrollViewerAreaDown = "PageDown";
            public const string ScrollViewerVerticalScrollbar = "VerticalScrollBar";

            public const string Expander = "Expander";
            public const string PartExpander = "PART_Expander";
            public const string SyncFusionUpDownTextbox = "DoubleTextBox";
            public const string DatePickerTextbox = "PART_TextBox";
            public const string PART_ScrollViewer = "PART_ScrollViewer";

            public const string OkBtn = "2";
            //Cancelbutton bei Yes/No/Cancel hat Cancelbutton gleiche AutomationId wie Ok-Button bei einfacher Messagebox
            public const string CancelBtn = "2"; 
            public const string YesBtn = "6";
            public const string NoBtn = "7";
            public const string Image = "20";
            public const string MessageText = "65535";            

            public const string XPathConfirmButton = "/Pane[@ClassName=\"#32769\"][@Name=\"Desktop 1\"]/Window[@Name=\"QS-Torque\"][@AutomationId=\"AI_MainWindow\"]/Window[@ClassName=\"#32770\"]/Button[@ClassName=\"Button\"][@Name=\"Ja\"]";
            public const string XPathOkButton = "/Pane[@ClassName=\"#32769\"][@Name=\"Desktop 1\"]/Window[@Name=\"QS-Torque\"][@AutomationId=\"AI_AssistentView\"]/Window[@ClassName=\"#32770\"]/Button[@ClassName=\"Button\"][@Name=\"OK\"]";

            public const string XPathDenyButton = "/Pane[@ClassName=\"#32769\"][@Name=\"Desktop 1\"]/Window[@Name=\"QS-Torque\"][@AutomationId=\"AI_MainWindow\"]/Window[@ClassName=\"#32770\"]/Button[@ClassName=\"Button\"][@Name=\"Nein\"]";
            public const string XPathCancelButton = "/Pane[@ClassName=\"#32769\"][@Name=\"Desktop 1\"]/Window[@Name=\"QS-Torque\"][@AutomationId=\"AI_MainWindow\"]/Window[@ClassName=\"#32770\"]/Button[@ClassName=\"Button\"][@Name=\"Abbrechen\"]";
            public const string XPathAssistantConfirmButton = "/Pane[@ClassName=\"#32769\"][@Name=\"Desktop 1\"]/Window[@Name=\"QS-Torque\"][@AutomationId=\"AI_AssistentView\"]/Window[@ClassName=\"Window\"]/Window[@ClassName=\"#32770\"]/Button[@ClassName=\"Button\"][@Name=\"Ja\"]";
        }

        public static class ToastNotification
        {
            public const string ToastParentWindowName = "Neue Benachrichtigung";
            public const string Toast = "NormalToastView";
            public const string SenderName = "SenderName";
            //Inspect zeigt "StandardImage" aber im XML vom PageSource ist das Bild nicht vorhanden
            //public const string Image = "StandardImage";
            public const string Text = "TitleText";
            public const string CloseButton = "DismissButton";
        }

        public static class GeneralAttributeStrings
        {
            public const string IsKeyboardFocusable = "IsKeyboardFocusable";
            public const string IsOffscreen = "IsOffscreen";
        }


        public static class UnlockTool
        {
            public const string LoadUnlockRequest = "AI_UnlockToolView_LoadUnlockRequest";
            public const string GenerateUnlockResponse = "AI_UnlockToolView_GenerateUnlockResponse";
            public const string LoadUnlockResponse = "AI_UnlockToolView_LoadUnlockResponse";
        }

        public enum By
        { AI,
          Class,
          Name,
          XPath
        }
    }
}
