using Core.Entities;
using Core.Entities.ToolTypes;
using InterfaceAdapters.Models;
using NUnit.Framework;

namespace InterfaceAdapters.Test.Models
{
    public class ToolModelTest
    {
        [Test]
        public void MapModelToEntityTest()
        {
            var toolModel = new InterfaceAdapters.Models.ToolModel(new Tool(), new NullLocalizationWrapper())
            {
                Id = 1,
                InventoryNumber = "cooles tool ",
                SerialNumber = "hat noch",
                ToolModelModel = new ToolModelModel(new Core.Entities.ToolModel(), new NullLocalizationWrapper())
                {
                    Id = 12,
                    Description = "cooles model",
                    Manufacturer = new ManufacturerModel(new Manufacturer())
                    {
                        Id = 1,
                        Name = "cooler hersteller"
                    },
                    ModelType = new ECDriverModel(new NullLocalizationWrapper()),
                    MinPower = 0,
                    MaxPower = 100
                },
                AdditionalConfigurableField1 = "sfdjklaöfkgjl",
                AdditionalConfigurableField2 = "gsauilfhsdajk",
                AdditionalConfigurableField3 = "8ugzhrn4mk5oz9u"
            };

            var entity = toolModel.Entity;
            
            Assert.AreEqual(toolModel.Id, entity.Id.ToLong());
            Assert.AreEqual(toolModel.InventoryNumber , entity.InventoryNumber.ToDefaultString());
            Assert.AreEqual(toolModel.SerialNumber, entity.SerialNumber.ToDefaultString());
            Assert.AreEqual(toolModel.Accessory, entity.Accessory);
            Assert.IsNotNull(entity.ToolModel);
            Assert.AreEqual(toolModel.ToolModelModel.Id, entity.ToolModel.Id.ToLong());
            Assert.AreEqual(toolModel.ToolModelModel.Description, entity.ToolModel.Description.ToDefaultString());
            Assert.AreEqual(toolModel.AdditionalConfigurableField1, entity.AdditionalConfigurableField1.ToDefaultString());
            Assert.AreEqual(toolModel.AdditionalConfigurableField2, entity.AdditionalConfigurableField2.ToDefaultString());
            Assert.AreEqual(toolModel.AdditionalConfigurableField3, entity.AdditionalConfigurableField3.ToDefaultString());
        }

        [Test]
        public void MapEntityToModelTest()
        {
            var entity = new Tool()
            {
                Id = new ToolId(123),
                InventoryNumber = new ToolInventoryNumber("coole invnr"),
                SerialNumber = new ToolSerialNumber("cool serno"),
                Accessory = "cooles zubehör",
                ToolModel = new Core.Entities.ToolModel()
                {
                    Id = new ToolModelId(12),
                    Description = new ToolModelDescription("cooles toolmodel"),
                    Manufacturer = new Manufacturer() { Id = new ManufacturerId(5), Name = new ManufacturerName("cooler hersteller") },
                    MinPower = 1.5,
                    MaxPower = 100,
                    ModelType = new ClickWrench()
                },
                AdditionalConfigurableField1 = new ConfigurableFieldString40("jb430plmvnjbhgz7t89ro"),
                AdditionalConfigurableField2 = new ConfigurableFieldString80("sdhnk cmlk4usvdöjfn"),
                AdditionalConfigurableField3 = new ConfigurableFieldString250("giovjhcynklmskeotz8hi")
            };

            var model = new InterfaceAdapters.Models.ToolModel(entity, new NullLocalizationWrapper());

            Assert.AreEqual(entity.Id.ToLong(), model.Id);
            Assert.AreEqual(entity.InventoryNumber.ToDefaultString(), model.InventoryNumber);
            Assert.AreEqual(entity.SerialNumber.ToDefaultString(), model.SerialNumber);
            Assert.AreEqual(entity.Accessory, model.Accessory);
            Assert.IsNotNull(model.ToolModelModel);
            Assert.AreEqual(entity.ToolModel.Id.ToLong(), model.ToolModelModel.Id);
            Assert.AreEqual(entity.ToolModel.Description.ToDefaultString(), model.ToolModelModel.Description);
            Assert.AreEqual(entity.AdditionalConfigurableField1.ToDefaultString(), model.AdditionalConfigurableField1);
            Assert.AreEqual(entity.AdditionalConfigurableField2.ToDefaultString(), model.AdditionalConfigurableField2);
            Assert.AreEqual(entity.AdditionalConfigurableField3.ToDefaultString(), model.AdditionalConfigurableField3);
        }
    }
}
