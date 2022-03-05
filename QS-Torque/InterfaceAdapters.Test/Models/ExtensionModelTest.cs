using Core.Entities;
using InterfaceAdapters.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Types.Enums;

namespace InterfaceAdapters.Test.Models
{
    public class ExtensionModelTest
    {
        [Test]
        public void MapEntityToModelHasCorrectDataInProperties()
        {
            var extension = new Extension
            {
                Id = new ExtensionId(15),
                Description = "TestName",
                InventoryNumber = new ExtensionInventoryNumber("1234"),
                Length = 1.11,
                Bending = 5.5,
                FactorTorque = 4.4
            };

            var model = new ExtensionModel(extension, new NullLocalizationWrapper());

            Assert.AreEqual(extension.Id.ToLong(), model.Id);
            Assert.AreEqual(extension.Description, model.Description);
            Assert.AreEqual(extension.InventoryNumber?.ToDefaultString(), model.InventoryNumber);
            Assert.AreEqual(extension.Length, model.Length);
            Assert.AreEqual(extension.Bending, model.Bending);
            Assert.AreEqual(extension.FactorTorque, model.FactorTorque);
        }

        [Test]
        public void MapModelToEntityFillsEntityWithCorrectDataInProperties()
        {
            var extensionModel = new ExtensionModel(new Extension(), new NullLocalizationWrapper())
            {
                Id = 15,
                Description = "TestName",
                InventoryNumber = "1234",
                Length = 11.1,
                Bending = 5.5,
                FactorTorque = 4.4
            };

            var model = extensionModel.Entity;

            Assert.AreEqual(extensionModel.Id, model.Id.ToLong());
            Assert.AreEqual(extensionModel.Description, model.Description);
            Assert.AreEqual(extensionModel.InventoryNumber, model.InventoryNumber?.ToDefaultString());
            Assert.AreEqual(extensionModel.Length, model.Length);
            Assert.AreEqual(extensionModel.Bending, model.Bending);
            Assert.AreEqual(extensionModel.FactorTorque, model.FactorTorque);
        }

        [Test]
        public void IsFactorVisibleReturnsCorrectValue()
        {
            var extensionModel = new ExtensionModel(new Extension(), new NullLocalizationWrapper())
            {
                ExtensionCorrection = ExtensionCorrection.UseFactor
            };

            Assert.IsTrue(extensionModel.IsFactorVisible);
            Assert.IsFalse(extensionModel.IsGaugeVisible);

            extensionModel.ExtensionCorrection = ExtensionCorrection.UseGauge;

            Assert.IsFalse(extensionModel.IsFactorVisible);
            Assert.IsTrue(extensionModel.IsGaugeVisible);
        }
    }
}
