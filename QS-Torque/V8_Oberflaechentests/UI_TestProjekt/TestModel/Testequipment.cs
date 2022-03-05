using System;
using System.Collections.Generic;

namespace UI_TestProjekt.TestModel
{
    public class Testequipment
    {
        private string serialNumber = "";
        private string inventoryNumber = "";
        private TestequipmentModel model = new TestequipmentModel();
        private string status = "";

        private DateTime lastCalibrationDate = DateTime.Now;
        private int interval = 365;
        private string calibrationNorm = "";
        private string firmwareVersion = "";
        private double minCapacity = 0;
        private double maxCapacity = 0;
        private bool useProcess = false;
        private bool useRotating = false;

        private bool transferUser = false;
        private bool transferAdapter = false;
        private bool transferTransducer = false;
        private bool transferAttributes = false;
        private bool transferPictures = false;
        private bool transferNewLimits = false;
        private string transferCurves = TransferCurveOption.Never;

        private string askForIdent = AskForIdentOption.Never;
        private bool askForSign = false;
        private bool useErrorCodes = false;
        private bool performLooseCheck = false;
        private bool mpCanBeDeleted = false;
        private string confirmMp = ConfirmMpOption.Never;

        private bool standardMethodsCanBeUsed = false;

        public string SerialNumber { get => serialNumber; set => serialNumber = value; }
        public string InventoryNumber { get => inventoryNumber; set => inventoryNumber = value; }
        public TestequipmentModel Model { get => model; set => model = value; }
        public string Status { get => status; set => status = value; }
        public DateTime LastCalibrationDate { get => lastCalibrationDate; set => lastCalibrationDate = value; }
        public int Interval { get => interval; set => interval = value; }
        public string CalibrationNorm { get => calibrationNorm; set => calibrationNorm = value; }
        public string FirmwareVersion { get => firmwareVersion; set => firmwareVersion = value; }
        public double MinCapacity { get => minCapacity; set => minCapacity = value; }
        public double MaxCapacity { get => maxCapacity; set => maxCapacity = value; }
        public bool UseProcess { get => useProcess; set => useProcess = value; }
        public bool UseRotating { get => useRotating; set => useRotating = value; }
        public bool TransferUser { get => transferUser; set => transferUser = value; }
        public bool TransferAdapter { get => transferAdapter; set => transferAdapter = value; }
        public bool TransferTransducer { get => transferTransducer; set => transferTransducer = value; }
        public bool TransferAttributes { get => transferAttributes; set => transferAttributes = value; }
        public bool TransferPictures { get => transferPictures; set => transferPictures = value; }
        public bool TransferNewLimits { get => transferNewLimits; set => transferNewLimits = value; }
        public string TransferCurves { get => transferCurves; set => transferCurves = value; }
        public string AskForIdent { get => askForIdent; set => askForIdent = value; }
        public bool AskForSign { get => askForSign; set => askForSign = value; }
        public bool UseErrorCodes { get => useErrorCodes; set => useErrorCodes = value; }
        public bool PerformLooseCheck { get => performLooseCheck; set => performLooseCheck = value; }
        public bool MpCanBeDeleted { get => mpCanBeDeleted; set => mpCanBeDeleted = value; }
        public string ConfirmMp { get => confirmMp; set => confirmMp = value; }
        public bool StandardMethodsCanBeUsed { get => standardMethodsCanBeUsed; set => standardMethodsCanBeUsed = value; }

        public Testequipment(string serialNumber, string inventoryNumber, TestequipmentModel model, string status, DateTime lastCalibrationDate, int intervall, string calibrationNorm, string firmwareVersion, double minCapacity, double maxCapacity, bool useProcess, bool useRotating, bool transferUser, bool transferAdapter, bool transferTransducer, bool transferAttributes, bool transferPictures, bool transferNewLimits, string transferCurves, string askForIdent, bool askForSign, bool useErrorCodes, bool performLooseCheck, bool mpCanBeDeleted, string confirmMp, bool standardMethodsCanBeUsed)
        {
            SerialNumber = serialNumber;
            InventoryNumber = inventoryNumber;
            Model = model;
            Status = status;
            LastCalibrationDate = lastCalibrationDate;
            Interval = intervall;
            CalibrationNorm = calibrationNorm;
            FirmwareVersion = firmwareVersion;
            MinCapacity = minCapacity;
            MaxCapacity = maxCapacity;
            UseProcess = useProcess;
            UseRotating = useRotating;
            TransferUser = transferUser;
            TransferAdapter = transferAdapter;
            TransferTransducer = transferTransducer;
            TransferAttributes = transferAttributes;
            TransferPictures = transferPictures;
            TransferNewLimits = transferNewLimits;
            TransferCurves = transferCurves;
            AskForIdent = askForIdent;
            AskForSign = askForSign;
            UseErrorCodes = useErrorCodes;
            PerformLooseCheck = performLooseCheck;
            MpCanBeDeleted = mpCanBeDeleted;
            ConfirmMp = confirmMp;
            StandardMethodsCanBeUsed = standardMethodsCanBeUsed;
        }
        public Testequipment() { }

        public static class TransferCurveOption
        {
            public const string Never = "Never";
            public const string OnlyNio = "Only Nio";
            public const string Always = "Always";
        }

        public static class AskForIdentOption
        {
            public const string Never = "Never";
            public const string PerTest = "Per test";
            public const string PerMeasurement = "Per measurement";
            public const string OnlyNio= "Only Nio";
            public const string PerRoute = "Per route";
        }

        public string GetAskForIdentComboBoxString()
        {
            switch(AskForIdent)
            {
                case AskForIdentOption.Never: return "Never";
                case AskForIdentOption.PerTest: return "PerTest";
                case AskForIdentOption.PerMeasurement: return "PerVal";
                case AskForIdentOption.OnlyNio: return "OnlyNio";
                case AskForIdentOption.PerRoute: return "PerRoute";
            }
            return "";
        }

        public static class ConfirmMpOption
        {
            public const string Never = "Never";
            public const string OnlyNio = "Only Nio";
            public const string Always = "Always";
        }

        public List<string> GetParentListWithTestequipment()
        {
            List<string> testEquipmentStrings = new List<string>();
            testEquipmentStrings.AddRange(model.GetParentListWithTestequipmentModel());
            testEquipmentStrings.Add(GetTestEquipmentTreeName());
            return testEquipmentStrings;
        }
        public string GetTestEquipmentTreeName()
        {
            return string.Format("{0} - {1}", serialNumber, inventoryNumber);
        }

        public bool ModelHasFeaturesEnabled()
        {
          return Model.TransferUser
          || Model.TransferAdapter
          || Model.TransferTransducer
          || Model.TransferAttributes
          || Model.TransferPictures
          || Model.TransferNewLimits
          || Model.TransferCurves
          || Model.AskForIdent
          || Model.AskForSign
          || Model.UseErrorCodes
          || Model.PerformLooseCheck
          || Model.MpCanBeDeleted
          || Model.ConfirmMp
          || Model.StandardMethodsCanBeUsed;
        }
    }
}
