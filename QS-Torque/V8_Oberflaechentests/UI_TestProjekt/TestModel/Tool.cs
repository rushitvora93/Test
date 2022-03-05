using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_TestProjekt.TestModel
{
    public class Tool
    {
        private string inventoryNumber = "";
        private string serialNumber = "";
        private ToolModel toolModel = null;
        private string status = "";
        private string accessory = "";
        private string costCenter = "";
        private string configurableField = "";
        private string comment = "";

        private string cmCmkLimit = "";

        private string configurableField1 = "";
        private string configurableField2 = "";
        private string configurableField3 = "";

        public string InventoryNumber { get => inventoryNumber; set => inventoryNumber = value; }
        public string SerialNumber { get => serialNumber; set => serialNumber = value; }
        public ToolModel ToolModel { get => toolModel; set => toolModel = value; }
        public string Status { get => status; set => status = value; }
        public string Accessory { get => accessory; set => accessory = value; }
        public string CostCenter { get => costCenter; set => costCenter = value; }
        public string ConfigurableField { get => configurableField; set => configurableField = value; }
        public string Comment { get => comment; set => comment = value; }
        public string CmCmkLimit { get => cmCmkLimit; set => cmCmkLimit = value; }
        public string ConfigurableField1 { get => configurableField1; set => configurableField1 = value; }
        public string ConfigurableField2 { get => configurableField2; set => configurableField2 = value; }
        public string ConfigurableField3 { get => configurableField3; set => configurableField3 = value; }

        public Tool()
        {
        }

        public Tool(string inventoryNumber, string serialNumber, ToolModel toolModel, string status, string accessory, string costCenter, string configurableField, string comment, string cmCmkLimit, string configurableField1, string configurableField2, string configurableField3)
        {
            InventoryNumber = inventoryNumber;
            SerialNumber = serialNumber;
            ToolModel = toolModel;
            Status = status;
            Accessory = accessory;
            CostCenter = costCenter;
            ConfigurableField = configurableField;
            Comment = comment;
            CmCmkLimit = cmCmkLimit;
            ConfigurableField1 = configurableField1;
            ConfigurableField2 = configurableField2;
            ConfigurableField3 = configurableField3;
        }

        public string GetTreeString()
        {
            return string.Concat(SerialNumber, " - ", InventoryNumber);
        }

        public List<string> GetParentListWithTool()
        {
            List<string> toolTreeStrings = new List<string>
            {
                "Tools",
                ToolModel.ToolModelType,
                ToolModel.Manufacturer,
                ToolModel.Description,
                GetTreeString()
            };
            return toolTreeStrings;
        }
    }
}
