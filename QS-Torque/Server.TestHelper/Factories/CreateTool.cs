using Server.Core.Entities;

namespace Server.TestHelper.Factories
{
    public class CreateTool
    {
        public static Tool Parameterized(long id, string inventoryNumber, string serialNumber,bool alive, ToolModel toolModel,
            string accesory, string additionalConf1, string additionalConf2, string additionalConf3,
            ConfigurableField configurableField, CostCenter costCenter, Status status)
        {
            return new Tool
            {
                Id = new ToolId(id),
                InventoryNumber = inventoryNumber,
                SerialNumber = serialNumber,
                ToolModel = toolModel,
                Accessory = accesory,
                AdditionalConfigurableField1 = new ConfigurableFieldString40(additionalConf1),
                AdditionalConfigurableField2 = new ConfigurableFieldString80(additionalConf2),
                AdditionalConfigurableField3 = new ConfigurableFieldString250(additionalConf3),
                ConfigurableField = configurableField,
                CostCenter = costCenter,
                Status = status,
                Comment = "",
                Alive = alive
            };
        }

        public static Tool Anonymous()
        {
            return new Tool
            {
                Id = new ToolId(15),
                InventoryNumber = "Test",
                SerialNumber = "Test",
                Comment = "",
                Status = new Status { Id = new StatusId(15), Value = new StatusDescription("") },
                ToolModel = CreateToolModel.Anonymous()
            };
        }

        public static Tool WithId(long id)
        {
            var tool = Anonymous();
            tool.Id = new ToolId(id);
            return tool;
        }

        public static Tool IdAndCommentOnly(long id, string comment)
        {
            var tool = WithId(id);
            tool.Comment = comment;
            return tool;
        }
    }
}
