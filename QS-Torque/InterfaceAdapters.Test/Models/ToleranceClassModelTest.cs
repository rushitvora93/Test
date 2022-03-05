using Core.Entities;
using InterfaceAdapters.Models;
using NUnit.Framework;

namespace InterfaceAdapters.Test.Models
{
    public class ToleranceClassModelTest
    {
        [Test]
        public void MapEntityToModelHasCorrectDataInProperties()
        {
            var toleranceClass = new ToleranceClass
            {
                Id = new ToleranceClassId(15),
                Name = "TestName",
                LowerLimit = 15.15,
                UpperLimit = 15.15,
                Relative = false
            };

            var model = new ToleranceClassModel(toleranceClass);

            Assert.AreEqual(toleranceClass.Id.ToLong(), model.Id);
            Assert.AreEqual(toleranceClass.Name, model.Name);
            Assert.AreEqual(toleranceClass.LowerLimit, model.LowerLimit);
            Assert.AreEqual(toleranceClass.UpperLimit, model.UpperLimit);
            Assert.AreEqual(toleranceClass.Relative, model.Relative);
        }

        [Test]
        public void MapModelToEntityFillsEntityWithCorrectDataInProperties()
        {
            var toleranceClassModel = new ToleranceClassModel(new ToleranceClass())
            {
                Id = 15,
                Name = "TestName",
                LowerLimit = 15.15,
                UpperLimit = 15.15,
                Relative = false
            };

            var model = toleranceClassModel.Entity;

            Assert.AreEqual(toleranceClassModel.Id, model.Id.ToLong());
            Assert.AreEqual(toleranceClassModel.Name, model.Name);
            Assert.AreEqual(toleranceClassModel.LowerLimit, model.LowerLimit);
            Assert.AreEqual(toleranceClassModel.UpperLimit, model.UpperLimit);
            Assert.AreEqual(toleranceClassModel.Relative, model.Relative);
        }
   
    }
}