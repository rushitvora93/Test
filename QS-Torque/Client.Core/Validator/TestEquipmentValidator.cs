using Core.Entities;

namespace Client.Core.Validator
{
    public interface ITestEquipmentValidator
    {
        bool Validate(TestEquipment testEquipment);
        bool Validate(TestEquipmentModel testEquipmentModel);
    }

    public class TestEquipmentValidator : ITestEquipmentValidator
    {
        public bool Validate(TestEquipment testEquipment)
        {
            if (testEquipment is null)
            {
                return true;
            }

            if (testEquipment.Validate(nameof(TestEquipment.SerialNumber)) != null)
            {
                return false;
            }

            if(testEquipment.Validate(nameof(TestEquipment.InventoryNumber)) != null)
            {
                return false;
            }

            if (testEquipment.Validate(nameof(TestEquipment.CapacityMax)) != null || 
                testEquipment.Validate(nameof(TestEquipment.CapacityMin)) != null)
            {
                return false;
            }

            return true;
        }

        public bool Validate(TestEquipmentModel testEquipmentModel)
        {
            if (testEquipmentModel?.Validate(nameof(TestEquipmentModel.TestEquipmentModelName)) != null)
            {
                return false;
            }

            return true;
        }
    }
}
