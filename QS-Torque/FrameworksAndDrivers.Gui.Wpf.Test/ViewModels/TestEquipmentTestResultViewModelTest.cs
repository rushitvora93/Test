using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.UseCases.Communication;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using NUnit.Framework;
using TestHelper.Factories;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    class TestEquipmentTestResultViewModelTest
    {
        public static List<List<TestEquipmentTestResult>> TestEquipmentTestData =
            new List<List<TestEquipmentTestResult>>
            {
                new List<TestEquipmentTestResult>
                {
                    new TestEquipmentTestResult
                    {
                        LocationToolAssignment = CreateLocationToolAssignment.Anonymous(),
                        LocationNumber = new LocationNumber("123"),
                        LocationDescription = new LocationDescription("12345"),
                        ToolInventoryNumber = "213234",
                        ToolSerialNumber = "1234",
                        TestResult = new TestResult(0),
                        ResultFromDataGate = new DataGateResult
                        {
                            Min1 = 1.0,
                            Max1 = 10.0,
                            Nom1 = 5.5,
                            Values = new List<DataGateResultValue>
                            {
                                new DataGateResultValue {Timestamp = new DateTime(2020, 7, 7, 6, 6, 6)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 7, 7, 6, 6, 6)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 7, 7, 6, 6, 6)}
                            }
                        }
                    },
                    new TestEquipmentTestResult
                    {
                        LocationToolAssignment = CreateLocationToolAssignment.Anonymous(),
                        LocationNumber = new LocationNumber("abc"),
                        LocationDescription = new LocationDescription("12defg345"),
                        ToolInventoryNumber = "213234",
                        ToolSerialNumber = "1234",
                        TestResult = new TestResult(1),
                        ResultFromDataGate = new DataGateResult
                        {
                            Min1 = 33.0,
                            Max1 = 66.00,
                            Nom1 = 55.5,
                            Values = new List<DataGateResultValue>
                            {
                                new DataGateResultValue {Timestamp = new DateTime(2019, 1, 1, 6, 6, 6)},
                                new DataGateResultValue {Timestamp = new DateTime(2019, 1, 1, 6, 6, 6)},
                                new DataGateResultValue {Timestamp = new DateTime(2019, 1, 1, 6, 6, 6)}
                            }
                        }
                    }
                },
                new List<TestEquipmentTestResult>
                {
                    new TestEquipmentTestResult
                    {
                        LocationToolAssignment = CreateLocationToolAssignment.Anonymous(),
                        LocationNumber = new LocationNumber("123"),
                        LocationDescription = new LocationDescription("12345"),
                        ToolInventoryNumber = "678",
                        ToolSerialNumber = "756",
                        TestResult = new TestResult(1),
                        ResultFromDataGate = new DataGateResult
                        {
                            Min1 = 10.0,
                            Max1 = 100.00,
                            Nom1 = 50.5,
                            Values = new List<DataGateResultValue>
                            {
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)},
                                new DataGateResultValue {Timestamp = new DateTime(2020, 8, 8, 2, 2, 2)}
                            }
                        }
                    }
                }
            };

        [TestCaseSource(nameof(TestEquipmentTestData))]
        public void CreateViewModelSetsTestEquipmentTestResultModelsCorrect(List<TestEquipmentTestResult> testEquipmentTestResults)
        {
            var viewModel = new TestEquipmentTestResultViewModel(testEquipmentTestResults, new NullLocalizationWrapper());

            CollectionAssert.AreEqual(testEquipmentTestResults, viewModel.TestEquipmentTestResultModels.Select(x => x.TestEquipmentTestResult).ToList());
        }


        [TestCase(0, "")]
        [TestCase(1, "!")]
        public void TestEquipmentResultViewModelShowsNioSignCorrect(long result, String expectedNioSign)
        {
            var testEquipmentTestResults = new List<TestEquipmentTestResult>
            {
                new TestEquipmentTestResult {TestResult = new TestResult(result)}
            };
            var viewModel = new TestEquipmentTestResultViewModel(testEquipmentTestResults, new NullLocalizationWrapper());

            Assert.AreEqual(expectedNioSign, viewModel.TestEquipmentTestResultModels.First().NioSign);
        }
    }
}
