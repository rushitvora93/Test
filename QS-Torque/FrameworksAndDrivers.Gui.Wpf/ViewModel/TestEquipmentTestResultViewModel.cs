
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.UseCases.Communication;
using FrameworksAndDrivers.Gui.Wpf.Model;
using InterfaceAdapters;
using InterfaceAdapters.Localization;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class TestEquipmentTestResultViewModel : BindableBase
    {
        #region Properties

        private List<TestEquipmentTestResult> _testEquipmentTestResults;
        #endregion

        public ObservableCollection<TestEquipmentTestResultHumbleModel> TestEquipmentTestResultModels { get; set; }

        public TestEquipmentTestResultViewModel(List<TestEquipmentTestResult> testEquipmentTestResults, ILocalizationWrapper localization) 
        {
            _testEquipmentTestResults = testEquipmentTestResults;

            TestEquipmentTestResultModels = new ObservableCollection<TestEquipmentTestResultHumbleModel>();
            foreach (var testEquipmentTestResult in testEquipmentTestResults)
            {
                TestEquipmentTestResultModels.Add(new TestEquipmentTestResultHumbleModel(testEquipmentTestResult, localization));
            }
        }
    }
}
