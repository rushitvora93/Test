using Core.Entities;

namespace TestHelper.Factories
{
    public class CreateTool
    {
        public static Tool Anonymous()
        {
            return new Tool
            {
                Id = new ToolId(15),
                InventoryNumber = new ToolInventoryNumber("Test"),
                SerialNumber = new ToolSerialNumber("Test"),
                Comment = "",
                Status = new Status { ListId = new HelperTableEntityId(15), Value = new StatusDescription("")},
                ToolModel = CreateToolModel.Anonymous()
            };
        }

        public static Tool WithId(long id)
        {
            var tool = Anonymous();
            tool.Id = new ToolId(id);
            return tool;
        }

        public static Tool WithIdAndState(long id, long stateId)
        {
            var tool = Anonymous();
            tool.Id = new ToolId(id);
            tool.Status = new Status(){ListId = new HelperTableEntityId(stateId)};
            return tool;
        }

        public static Tool WithInventoryNumber(string name)
        {
            var tool = Anonymous();
            tool.InventoryNumber = new ToolInventoryNumber(name);
            return tool;
        }

        public static Tool WithIdInventoryAndSerialNumber(long id, string inventoryNumber, string serialNumber)
        {
            var tool = Anonymous();
            tool.Id = new ToolId(id);
            tool.InventoryNumber = new ToolInventoryNumber(inventoryNumber);
            tool.SerialNumber = new ToolSerialNumber(serialNumber);
            return tool;
        }

        public static Tool WithModel(ToolModel model)
        {
            var tool = Anonymous();
            tool.ToolModel = model;
            return tool;
        }

        public static Tool Parameterized(long id, string inventoryNumber, string serialNumber, ToolModel toolModel,
            string accesory, string additionalConf1, string additionalConf2, string additionalConf3,
            ConfigurableField configurableField, CostCenter costCenter, Status status)
        {
            return new Tool
            {
                Id = new ToolId(id),
                InventoryNumber = new ToolInventoryNumber(inventoryNumber),
                SerialNumber = new ToolSerialNumber(serialNumber),
                ToolModel = toolModel,
                Accessory = accesory,
                AdditionalConfigurableField1 = new ConfigurableFieldString40(additionalConf1),
                AdditionalConfigurableField2 = new ConfigurableFieldString80(additionalConf2),
                AdditionalConfigurableField3 = new ConfigurableFieldString250(additionalConf3),
                ConfigurableField = configurableField,
                CostCenter = costCenter,
                Status = status,
                Comment = ""
            };
        }
    }
}