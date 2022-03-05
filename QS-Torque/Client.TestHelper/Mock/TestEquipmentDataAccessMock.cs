using System;
using System.Collections.Generic;
using Client.Core.Diffs;
using Common.Types.Enums;
using Core.Diffs;
using Core.Entities;
using Core.UseCases.Communication;

namespace TestHelper.Mock
{
    public class TestEquipmentDataAccessMock : ITestEquipmentDataAccess
    {
        public List<TestEquipmentModel> LoadTestEquipmentModels()
        {
            if (LoadTestEquipmentModelsThrowsError)
            {
                throw new Exception("");
            }
            LoadTestEquipmentModelsCalled = true;
            return LoadTestEquipmentModelsReturnValue;
        }

        public void SaveTestEquipment(TestEquipmentDiff testEquipmentDiff)
        {
            if (SaveTestEquipmentThrowsError)
            {
                throw new Exception("");
            }

            SaveTestEquipmentParameterTestEquipmentDiff = testEquipmentDiff;
            if (SaveTestEquipmentException != null)
            {
                throw SaveTestEquipmentException;
            }
        }

        public void SaveTestEquipmentModel(TestEquipmentModelDiff testEquipmentModelDiff)
        {
            if (SaveTestEquipmentThrowsError)
            {
                throw new Exception("");
            }
            SaveTestEquipmentModelParameterDiff = testEquipmentModelDiff;
        }

        public void RemoveTestEquipment(TestEquipment testEquipment, User user)
        {
            if (RemoveTestEquipmentThrowsError)
            {
                throw new Exception("");
            }
            RemoveTestEquipmentParameterTestEquipment = testEquipment;
            RemoveTestEquipmentParameterUser = user;
        }

        public TestEquipment AddTestEquipment(TestEquipment testEquipment, User byUser)
        {
            if (AddTestEquipmentThrowsError)
            {
                throw new Exception("");
            }
            AddTestEquipmentTestEquipmentParameter = testEquipment;
            AddTestEquipmentUserParameter = byUser;
            return AddTestEquipmentReturnValue;
        }

        public bool IsInventoryNumberUnique(TestEquipmentInventoryNumber inventoryNumber)
        {
            IsInventoryNumberUniqueParameter = inventoryNumber;
            return IsInventoryNumberUniqueReturnValue;
        }

        public bool IsSerialNumberUnique(TestEquipmentSerialNumber serialNumber)
        {
            IsSerialNumberUniqueParameter = serialNumber;
            return IsSerialNumberUniqueReturnValue;
        }

        public bool IsTestEquipmentModelNameUnique(TestEquipmentModelName name)
        {
            IsTestEquipmentModelNameUniqueParameter = name;
            return IsTestEquipmentModelNameUniqueReturnValue;
        }

        public List<TestEquipmentType> LoadAvailableTestEquipmentTypes()
        {
            LoadAvailableTestEquipmentTypesCalled = true;
            return LoadAvailableTestEquipmentTypesReturnValue;
        }

        public List<TestEquipment> GetTestEquipmentsByIds(List<TestEquipmentId> ids)
        {
            if (GetTestEquipmentByIdReturnValue == null || GetTestEquipmentByIdReturnValue.Count == 0)
            {
                return null;
            }
            return GetTestEquipmentByIdReturnValue;
        }

        public TestEquipmentDiff SaveTestEquipmentParameterTestEquipmentDiff;
        public Exception SaveTestEquipmentException = null;
        public List<TestEquipment> GetTestEquipmentByIdReturnValue;
        public List<TestEquipmentModel> LoadTestEquipmentModelsReturnValue { get; set; }
        public bool LoadTestEquipmentModelsCalled { get; set; }
        public bool LoadTestEquipmentModelsThrowsError { get; set; }
        public TestEquipmentModelDiff SaveTestEquipmentModelParameterDiff { get; set; }
        public bool SaveTestEquipmentThrowsError { get; set; }
        public User RemoveTestEquipmentParameterUser { get; set; }
        public TestEquipment RemoveTestEquipmentParameterTestEquipment { get; set; }
        public bool RemoveTestEquipmentThrowsError { get; set; }
        public bool IsTestEquipmentModelNameUniqueReturnValue { get; set; }
        public TestEquipmentModelName IsTestEquipmentModelNameUniqueParameter { get; set; }
        public bool IsSerialNumberUniqueReturnValue { get; set; }
        public TestEquipmentSerialNumber IsSerialNumberUniqueParameter { get; set; }
        public bool IsInventoryNumberUniqueReturnValue { get; set; }
        public TestEquipmentInventoryNumber IsInventoryNumberUniqueParameter { get; set; }
        public TestEquipment AddTestEquipmentReturnValue { get; set; }
        public User AddTestEquipmentUserParameter { get; set; }
        public TestEquipment AddTestEquipmentTestEquipmentParameter { get; set; }
        public bool AddTestEquipmentThrowsError { get; set; }
        public List<TestEquipmentType> LoadAvailableTestEquipmentTypesReturnValue { get; set; }
        public bool LoadAvailableTestEquipmentTypesCalled { get; set; }
    }
}
