using System.Collections.Generic;

namespace UI_TestProjekt.TestModel
{
    public class ToolModel
    {
        public static readonly double defaultCm = 1.670;
        public static readonly double defaultCmk = 1.670;

        public string Description { get; set; } = "";
        public string Manufacturer { get; set; } = "";
        public string ToolModelType { get; set; } = "";
        public string ToolModelClass { get; set; } = "";
        public double AirPressure { get; set; } = 0;
        public double AirConsumption { get; set; } = 0;
        public double BattVoltage { get; set; } = 0;
        public int MaxRotSpeed { get; set; } = 0;
        public double MinPow { get; set; } = 0;
        public double MaxPow { get; set; } = 0;
        public string ToolType { get; set; } = "";
        public double Weight { get; set; } = 0;
        public string SwitchOff { get; set; } = "";
        public string ShutOff { get; set; } = "";
        public string DriveSize { get; set; } = "";
        public string DriveType { get; set; } = "";
        public string ConstructionType { get; set; } = "";
        public double LimitCm { get; set; } = 0;
        public double LimitCmk { get; set; } = 0;

        public ToolModel(string description, string manufacturer, string toolModelType, string toolModelClass, double airPressure, double airConsumption, double battVoltage, int maxRotSpeed, double minPow, double maxPow, string toolType, double weight, string switchOff, string shutOff, string driveSize, string driveType, string constructionType, double limitCm, double limitCmk)
        {
            Description = description;
            Manufacturer = manufacturer;
            ToolModelType = toolModelType;
            ToolModelClass = toolModelClass;
            AirPressure = airPressure;
            AirConsumption = airConsumption;
            BattVoltage = battVoltage;
            MaxRotSpeed = maxRotSpeed;
            MinPow = minPow;
            MaxPow = maxPow;
            ToolType = toolType;
            Weight = weight;
            SwitchOff = switchOff;
            ShutOff = shutOff;
            DriveSize = driveSize;
            DriveType = driveType;
            ConstructionType = constructionType;
            LimitCm = limitCm;
            LimitCmk = limitCmk;
        }

        public ToolModel(string description, string manufacturer, string toolModelType, double minPow, double maxPow, string toolType, double weight, string switchOff, string shutOff, string driveSize, string driveType, string constructionType, double limitCm, double LimitCmk) 
            : this(description, manufacturer, toolModelType, "", 0, 0, 0, 0, minPow, maxPow, toolType, weight, switchOff, shutOff, driveSize, driveType, constructionType, limitCm, LimitCmk)
        { 
        }

        public ToolModel()
        {
        }

        public static class ToolModelTypeStrings
        {
            public const string ClickWrench = "Click wrench";
            public const string EcDriver = "EC driver";
            public const string General = "General";
            public const string MdWrench = "MD wrench";
            public const string ProductionWrench = "Production wrench";
            public const string PulseDriver = "Pulse driver";
            public const string PulseDriverShutOff = "Pulse driver shut off";
        }

        public static class ToolModelClassStrings
        {
            public static class ClickWrenchClass
            {
                public const string WrenchConfScale = "Wrench configurable/scale";
                public const string WrenchFixSet = "Wrench fix set";
                public const string WrenchWithoutScale = "Wrench without scale";
                public const string DriverScale = "Driver scale";
                public const string DriverFixSet = "Driver fix set";
                public const string DriverWithoutScale = "Driver without scale";
                public const string BeamTypeTorqueWrenchWithScale = "Beam-type torque wrench, adjustable, with scale";
            }

            public static class Md_ProductionWrenchClass
            {
                public const string BeamTypeTorqueWrench = "Beam-type torque wrench";
                public const string WrenchWithDialIndicator = "Wrench with dial indicator";
                public const string WrenchElectronic = "Wrench electronic";
                public const string DriverWithDialIndicator = "Driver with dial indicator";
                public const string DriverElectronic = "Driver electronic";
            }
        }

        public List<string> GetParentListWithToolModel()
        {
            List<string> parentFolderWithToolModel = new List<string>();
            parentFolderWithToolModel.Add("Tool models");
            parentFolderWithToolModel.Add(ToolModelType);
            parentFolderWithToolModel.Add(Manufacturer);
            parentFolderWithToolModel.Add(Description);
            return parentFolderWithToolModel;
        }

        public static class ToolModelListHeaderStrings
        {
            public const string Description = "Description";
            public const string ToolModelType = "Type of tool model";
            public const string Class = "Class";
            public const string Manufacturer = "Manufacturer";
            public const string LowPowLim = "Lower power limit";
            public const string UpperPowLim = "Upper power limit";
            public const string AirPressure = "Air pressure";
            public const string ToolType = "Tool type";
            public const string Weight = "Weight";
            public const string BatteryVolt = "Battery voltage";
            public const string MaxRotSpeed = "Max. rotation speed";
            public const string AirConsumption = "Air consumption";
            public const string SwitchOff = "Switch Off";
            public const string DriveSize = "Drive Size";
            public const string ShutOff = "Shut Off";
            public const string DriveType = "Drive Type";
            public const string ConstructionType = "Construction Type";
            public const string LimitCM = "Limit Cm";
            public const string LimitCMK = "Limit Cmk";
        }
    }
}
