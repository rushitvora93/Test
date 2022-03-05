using System.Collections.Generic;

namespace UI_TestProjekt.TestModel
{
    public class TestequipmentModel
    {
        private string name = "";
        private string dataGateVersion = DataGateVersionOption.leer;
        private string type = TestequipmentType.TestWrench;

        private bool useProcess = false;
        private bool useRotating = false;

        private bool transferUser = false;
        private bool transferAdapter = false;
        private bool transferTransducer = false;
        private bool transferAttributes = false;
        private bool transferPictures = false;
        private bool transferNewLimits = false;
        private bool transferCurves = false;

        private bool askForIdent = false;
        private bool askForSign = false;
        private bool useErrorCodes = false;
        private bool performLooseCheck = false;
        private bool mpCanBeDeleted = false;
        private bool confirmMp = false;
        private bool standardMethodsCanBeUsed = false;

        private string testEquipmentDriver = "";
        private string statusFile = "";
        private string qstToTestequipment = "";
        private string testequipmentToQst = "";

        public string Name { get => name; set => name = value; }
        public string DataGateVersion { get => dataGateVersion; set => dataGateVersion = value; }
        public string Type { get => type; set => type = value; }
        public bool UseProcess { get => useProcess; set => useProcess = value; }
        public bool UseRotating { get => useRotating; set => useRotating = value; }
        public bool TransferUser { get => transferUser; set => transferUser = value; }
        public bool TransferAdapter { get => transferAdapter; set => transferAdapter = value; }
        public bool TransferTransducer { get => transferTransducer; set => transferTransducer = value; }
        public bool TransferAttributes { get => transferAttributes; set => transferAttributes = value; }
        public bool TransferPictures { get => transferPictures; set => transferPictures = value; }
        public bool TransferNewLimits { get => transferNewLimits; set => transferNewLimits = value; }
        public bool TransferCurves { get => transferCurves; set => transferCurves = value; }
        public bool AskForIdent { get => askForIdent; set => askForIdent = value; }
        public bool AskForSign { get => askForSign; set => askForSign = value; }
        public bool UseErrorCodes { get => useErrorCodes; set => useErrorCodes = value; }
        public bool PerformLooseCheck { get => performLooseCheck; set => performLooseCheck = value; }
        public bool MpCanBeDeleted { get => mpCanBeDeleted; set => mpCanBeDeleted = value; }
        public bool ConfirmMp { get => confirmMp; set => confirmMp = value; }
        public bool StandardMethodsCanBeUsed { get => standardMethodsCanBeUsed; set => standardMethodsCanBeUsed = value; }
        public string TestequipmentDriver { get => testEquipmentDriver; set => testEquipmentDriver = value; }
        public string StatusFile { get => statusFile; set => statusFile = value; }
        public string QstToTestequipment { get => qstToTestequipment; set => qstToTestequipment = value; }
        public string TestequipmentToQst { get => testequipmentToQst; set => testequipmentToQst = value; }

        public TestequipmentModel(string name, string dataGateVersion, string type, bool useProcess, bool useRotating, bool transferUser, bool transferAdapter, bool transferTransducer, bool transferAttributes, bool transferPictures, bool transferNewLimits, bool transferCurves, bool askForIdent, bool askForSign, bool useErrorCodes, bool performLooseCheck, bool mpCanBeDeleted, bool confirmMp, bool standardMethodsCanBeUsed, string testEquipmentDriver, string statusFile, string qstToTestequipment, string testequipmentToQst)
        {
            Name = name;
            DataGateVersion = dataGateVersion;
            Type = type;
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
            TestequipmentDriver = testEquipmentDriver;
            StatusFile = statusFile;
            QstToTestequipment = qstToTestequipment;
            TestequipmentToQst = testequipmentToQst;
        }

        public TestequipmentModel() { }

        public static class DataGateVersionOption
        {
            public const string leer = "";
            public const string eins = "1.0";
            public const string zwei = "2.0";
            public const string drei = "3.0";
            public const string vier = "4.0";
            public const string fuenf = "5.0";
            public const string sechs = "6.0";
            public const string sieben = "7.0";
        }

        public static class DeviceName
        {
            public const string Quantec = "QuanTec";
            public const string STA6000 = "STA6000";
        }
        public static class TestequipmentType
        {
            public const string TestWrench = "Test wrench";
            public const string AnalysisTool = "Analysis tool";
        }

        public List<string> GetParentListWithTestequipmentModel()
        {
            List<string> testEquipmentModelStrings = new List<string>();
            testEquipmentModelStrings.Add("Test equipments");
            testEquipmentModelStrings.Add(Type);
            testEquipmentModelStrings.Add(Name);
            return testEquipmentModelStrings;
        }

        public string GetManufacturer()
        {
            return GetManufacturerFromDeviceName(Name);
        }

        //TODO andere Hersteller implementieren sobald in V8 implementiert
        public static string GetManufacturerFromDeviceName(string deviceName)
        {
            switch (deviceName)
            {
                case DeviceName.Quantec: return "GWK";
                case DeviceName.STA6000: return "Atlas Copco";
            }
            return "";
        }
    }
}
