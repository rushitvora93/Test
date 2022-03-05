using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Client.TestHelper.Mock;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using Core.UseCases.Communication;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;
using TransferToTestEquipmentService;
using DateTime = System.DateTime;
using LocationToolAssignment = Core.Entities.LocationToolAssignment;
using TestEquipment = Core.Entities.TestEquipment;
using User = Core.Entities.User;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    class TransferToTestEquipmentDataAccessTest
    {
        public class TransferToTestEquipmentClientMock : ITransferToTestEquipmentClient
        {
            public ListOfLocationToolAssignmentForTransfer LoadLocationToolAssignmentsForTransferReturnValue { get; set; } = new ListOfLocationToolAssignmentForTransfer();
            public Long LoadLocationToolAssignmentsForTransferParameter { get; set; }
            public ListOfClassicMfuTestWithLocalTimestamp InsertClassicMfuTestsParameter { get; set; }
            public ListOfClassicChkTestWithLocalTimestamp InsertClassicChkTestsParameter { get; set; }
            public ListOfProcessControlDataForTransfer LoadProcessControlDataForTransferReturnValue { get; set; }
            public bool LoadProcessControlDataForTransferCalled { get; set; }

            public ListOfLocationToolAssignmentForTransfer LoadLocationToolAssignmentsForTransfer(Long testType)
            {
                LoadLocationToolAssignmentsForTransferParameter = testType;
                return LoadLocationToolAssignmentsForTransferReturnValue;
            }

            public ListOfProcessControlDataForTransfer LoadProcessControlDataForTransfer()
            {
                LoadProcessControlDataForTransferCalled = true;
                return LoadProcessControlDataForTransferReturnValue;
            }

            public void InsertClassicChkTests(ListOfClassicChkTestWithLocalTimestamp tests)
            {
                InsertClassicChkTestsParameter = tests;
            }

            public void InsertClassicMfuTests(ListOfClassicMfuTestWithLocalTimestamp tests)
            {
                InsertClassicMfuTestsParameter = tests;
            }
        }

        [Test]
        public void LoadProcessControlDataForTransferCallsClient()
        {
            var environment = new Environment();
            environment.dataAccess.LoadProcessControlDataForTransfer();
            Assert.IsTrue(environment.mocks.transferToTestEquipmentClient.LoadProcessControlDataForTransferCalled);
        }

        private static IEnumerable<ListOfProcessControlDataForTransfer>
            ListOfProcessControlDataForTransferData =
                new List<ListOfProcessControlDataForTransfer>()
                {
                    new ListOfProcessControlDataForTransfer()
                    {
                        Values =
                        {
                            new ProcessControlDataForTransfer()
                            {
                                LocationId = 1,
                                TestMethod = 2,
                                ProcessControlConditionId = 3,
                                LocationDescription = "123",
                                LocationNumber = "765",
                                TestInterval = new BasicTypes.Interval()
                                {
                                    IntervalValue = 1,
                                    IntervalType = 3
                                },
                                ProcessControlTechId = 45,
                                SampleNumber = 5,
                                MaximumTorque = new NullableDouble(){Value = 5.6, IsNull = false},
                                MinimumTorque = new NullableDouble(){Value = 15.6, IsNull = false},
                                SetPointTorque = new NullableDouble(){Value = 5.46, IsNull = false},
                                LastTestDate = new NullableDateTime()
                                {
                                    IsNull = false,
                                    Value = new BasicTypes.DateTime()
                                    {
                                        Ticks = new System.DateTime(2020,12,1,2,3,4).Ticks
                                    }
                                },
                                NextTestDate = new NullableDateTime()
                                {
                                    IsNull = false,
                                    Value = new BasicTypes.DateTime()
                                    {
                                        Ticks = new System.DateTime(2021,1,14,21,3,4).Ticks
                                    }
                                },
                                NextTestDateShift = new NullableInt(){IsNull = false, Value = 1}
                            },
                            new ProcessControlDataForTransfer()
                            {
                                LocationId = 13,
                                TestMethod = 25,
                                ProcessControlConditionId = 63,
                                LocationDescription = "aaa",
                                LocationNumber = "bbb",
                                TestInterval = new BasicTypes.Interval()
                                {
                                    IntervalValue = 5,
                                    IntervalType = 1
                                },
                                ProcessControlTechId = 11,
                                SampleNumber = 25,
                                MaximumTorque = new NullableDouble(){Value = 53.6, IsNull = false},
                                MinimumTorque = new NullableDouble(){Value = 145.6, IsNull = false},
                                SetPointTorque = new NullableDouble(){Value = 15.46, IsNull = false},
                                LastTestDate = new NullableDateTime()
                                {
                                    IsNull = false,
                                    Value = new BasicTypes.DateTime()
                                    {
                                        Ticks = new System.DateTime(2019,5,16,2,3,4).Ticks
                                    }
                                },
                                NextTestDate = new NullableDateTime()
                                {
                                    IsNull = false,
                                    Value = new BasicTypes.DateTime()
                                    {
                                        Ticks = new System.DateTime(2021,11,4,1,33,4).Ticks
                                    }
                                },
                                NextTestDateShift = new NullableInt(){IsNull = false, Value = 3}
                            }
                        }
                    },
                    new ListOfProcessControlDataForTransfer()
                    {
                        Values =
                        {
                            new ProcessControlDataForTransfer()
                            {
                                LocationId = 16,
                                TestMethod = 27,
                                ProcessControlConditionId = 39,
                                LocationDescription = "oooo",
                                LocationNumber = "iiii",
                                TestInterval = new BasicTypes.Interval()
                                {
                                    IntervalValue = 1,
                                    IntervalType = 2
                                },
                                ProcessControlTechId = 1,
                                SampleNumber = 3,
                                MaximumTorque = new NullableDouble(){Value = 5.65, IsNull = false},
                                MinimumTorque = new NullableDouble(){Value = 15.76, IsNull = false},
                                SetPointTorque = new NullableDouble(){Value = 5.86, IsNull = false},
                                LastTestDate = new NullableDateTime()
                                {
                                    IsNull = false,
                                    Value = new BasicTypes.DateTime()
                                    {
                                        Ticks = new System.DateTime(2010,2,6,2,3,4).Ticks
                                    }
                                },
                                NextTestDate = new NullableDateTime()
                                {
                                    IsNull = false,
                                    Value = new BasicTypes.DateTime()
                                    {
                                        Ticks = new System.DateTime(2011,1,24,11,3,45).Ticks
                                    }
                                },
                                NextTestDateShift = new NullableInt(){IsNull = true}
                            }
                        }
                    }
                };

        [TestCaseSource(nameof(ListOfProcessControlDataForTransferData))]
        public void LoadProcessControlDataForTransferReturnsCorrectValue(ListOfProcessControlDataForTransfer datas)
        {
            var environment = new Environment();
            environment.mocks.transferToTestEquipmentClient.LoadProcessControlDataForTransferReturnValue = datas;

            var result = environment.dataAccess.LoadProcessControlDataForTransfer();

            var comparer =
                new Func<TransferToTestEquipmentService.ProcessControlDataForTransfer,
                    Core.UseCases.Communication.ProcessControlForTransfer, bool>(
                    EqualityChecker.CompareProcessControlForTransferWithDto);

            CheckerFunctions.CollectionAssertAreEquivalent(datas.Values, result, comparer);
        }

        [TestCase(TestType.Mfu)]
        [TestCase(TestType.Chk)]
        public void LoadLocationToolAssignmentsForTransferCallsClient(TestType testType)
        {
            var environment = new Environment();
            environment.dataAccess.LoadLocationToolAssignmentsForTransfer(testType);
            Assert.AreEqual((long)testType, environment.mocks.transferToTestEquipmentClient.LoadLocationToolAssignmentsForTransferParameter.Value);
        }

        private static IEnumerable<ListOfLocationToolAssignmentForTransfer>
            LoadLocationToolAssignmentsForTransferReturnsCorrectValueData =
                new List<ListOfLocationToolAssignmentForTransfer>()
                {
                      new ListOfLocationToolAssignmentForTransfer()
                      {
                          Values =
                          {
                              new TransferToTestEquipmentService.LocationToolAssignmentForTransfer()
                              {
                                  LocationId = 1,
                                  LocationToolAssignmentId = 2,
                                  ToolId = 3,
                                  ToolUsageId = 4,
                                  ToolInventoryNumber = "124323543",
                                  ToolSerialNumber = "234536457",
                                  LocationNumber = "sfbsdhf",
                                  LocationDescription = "235mgd9654",
                                  LocationFreeFieldCategory = "C",
                                  LocationFreeFieldDocumentation = true,
                                  TestRulePeriod = 3,
                                  TestRuleSamples = 4,
                                  ToolUsageDescription = "ABC",
                                  TestRuleNextCheck = new NullableDateTime()
                                  {
                                      IsNull = false, Value = new BasicTypes.DateTime()
                                      {
                                          Ticks = new System.DateTime(2021,1,1).Ticks
                                      }
                                  },
                                  TestRuleLastCheck = new NullableDateTime()
                                  {
                                      IsNull = false, Value = new BasicTypes.DateTime()
                                      {
                                          Ticks = new System.DateTime(2020,1,1).Ticks
                                      }
                                  },
                                  SampleNumber = 4,
                                  TestInterval = new BasicTypes.Interval()
                                  {
                                      IntervalType = 2,
                                      IntervalValue = 4
                                  },
                                  NextTestDateShift = new NullableInt()
                                  {
                                      Value = 1,
                                      IsNull = false
                                  },
                                  NextTestDate = new NullableDateTime()
                                  {
                                      IsNull = false, Value = new BasicTypes.DateTime()
                                      {
                                          Ticks = new System.DateTime(2021,1,1).Ticks
                                      }
                                  },
                                  LastTestDate = new NullableDateTime()
                                  {
                                      IsNull = false, Value = new BasicTypes.DateTime()
                                      {
                                          Ticks = new System.DateTime(2020,1,1).Ticks
                                      }
                                  }
                              },
                              new TransferToTestEquipmentService.LocationToolAssignmentForTransfer()
                              {
                                  LocationId = 14,
                                  LocationToolAssignmentId = 52,
                                  ToolId = 32,
                                  ToolUsageId = 45,
                                  ToolInventoryNumber = "AVC",
                                  ToolSerialNumber = "DEF",
                                  LocationNumber = "FH",
                                  LocationDescription = "ER",
                                  LocationFreeFieldCategory = "D",
                                  LocationFreeFieldDocumentation = false,
                                  TestRulePeriod = 1,
                                  TestRuleSamples = 44,
                                  ToolUsageDescription = "ABCXCW",
                                  TestRuleNextCheck = new NullableDateTime()
                                  {
                                      IsNull = false, Value = new BasicTypes.DateTime()
                                      {
                                          Ticks = new System.DateTime(2011,1,1).Ticks
                                      }
                                  },
                                  TestRuleLastCheck = new NullableDateTime()
                                  {
                                      IsNull = false, Value = new BasicTypes.DateTime()
                                      {
                                          Ticks = new System.DateTime(2010,1,1).Ticks
                                      }
                                  },
                                  SampleNumber = 44,
                                  TestInterval = new BasicTypes.Interval()
                                  {
                                      IntervalType = 0,
                                      IntervalValue = 45
                                  },
                                  NextTestDateShift = new NullableInt()
                                  {
                                      Value = 0,
                                      IsNull = true
                                  },
                                  NextTestDate = new NullableDateTime()
                                  {
                                      IsNull = false, Value = new BasicTypes.DateTime()
                                      {
                                          Ticks = new System.DateTime(2011,1,1).Ticks
                                      }
                                  },
                                  LastTestDate = new NullableDateTime()
                                  {
                                      IsNull = false, Value = new BasicTypes.DateTime()
                                      {
                                          Ticks = new System.DateTime(2010,1,1).Ticks
                                      }
                                  }
                              }
                          }
                      },
                      new ListOfLocationToolAssignmentForTransfer()
                      {
                          Values =
                          {
                              new TransferToTestEquipmentService.LocationToolAssignmentForTransfer()
                              {
                                  LocationId = 14,
                                  LocationToolAssignmentId = 52,
                                  ToolId = 32,
                                  ToolUsageId = 45,
                                  ToolInventoryNumber = "AVC",
                                  ToolSerialNumber = "DEF",
                                  LocationNumber = "FH",
                                  LocationDescription = "ER",
                                  LocationFreeFieldCategory = "D",
                                  LocationFreeFieldDocumentation = false,
                                  TestRulePeriod = 5,
                                  TestRuleSamples = 44,
                                  ToolUsageDescription = "ABCXCW",
                                  TestRuleNextCheck = new NullableDateTime()
                                  {
                                      IsNull = false, Value = new BasicTypes.DateTime()
                                      {
                                          Ticks = new System.DateTime(2012,6,1).Ticks
                                      }
                                  },
                                  TestRuleLastCheck = new NullableDateTime()
                                  {
                                      IsNull = false, Value = new BasicTypes.DateTime()
                                      {
                                          Ticks = new System.DateTime(2011,6,1).Ticks
                                      }
                                  },
                                  SampleNumber = 445,
                                  TestInterval = new BasicTypes.Interval()
                                  {
                                      IntervalType = 4,
                                      IntervalValue = 450
                                  },
                                  NextTestDateShift = new NullableInt()
                                  {
                                      Value = 2,
                                      IsNull = false
                                  },
                                  NextTestDate = new NullableDateTime()
                                  {
                                      IsNull = false, Value = new BasicTypes.DateTime()
                                      {
                                          Ticks = new System.DateTime(2012,6,1).Ticks
                                      }
                                  },
                                  LastTestDate = new NullableDateTime()
                                  {
                                      IsNull = false, Value = new BasicTypes.DateTime()
                                      {
                                          Ticks = new System.DateTime(2011,6,1).Ticks
                                      }
                                  }
                              }
                          }
                      }
                };

        [TestCaseSource(nameof(LoadLocationToolAssignmentsForTransferReturnsCorrectValueData))]
        public void LoadLocationToolAssignmentsForTransferReturnsCorrectValue(ListOfLocationToolAssignmentForTransfer datas)
        {
            var environment = new Environment();
            environment.mocks.transferToTestEquipmentClient.LoadLocationToolAssignmentsForTransferReturnValue = datas;

            var result = environment.dataAccess.LoadLocationToolAssignmentsForTransfer(TestType.Chk);

            var comparer =
                new Func<TransferToTestEquipmentService.LocationToolAssignmentForTransfer,
                    Core.UseCases.Communication.LocationToolAssignmentForTransfer, bool>(
                    EqualityChecker.CompareLocationToolAssignmentsForTransferDtoWithLocationToolAssignmentForTransfer);

            CheckerFunctions.CollectionAssertAreEquivalent(datas.Values, result, comparer);
        }

        [Test]
        public void LoadLocationToolAssignmentsForTransferDontSetTestRuleNextCheckIfNull()
        {

            var environment = new Environment();

            environment.mocks.transferToTestEquipmentClient.LoadLocationToolAssignmentsForTransferReturnValue =
                new ListOfLocationToolAssignmentForTransfer()
                    { 
                        Values = 
                        { 
                            new TransferToTestEquipmentService.LocationToolAssignmentForTransfer()
                            {
                                LastTestDate = new NullableDateTime() { IsNull = true },
                                NextTestDate = new NullableDateTime() { IsNull = true },
                                NextTestDateShift = new NullableInt() { Value = 1 }
                            }
                        }
                    };

            var result = environment.dataAccess.LoadLocationToolAssignmentsForTransfer(TestType.Chk);

            Assert.IsNull(result.First().NextTestDate);
        }

        [Test]
        public void LoadLocationToolAssignmentsForTransferDontSetTestRuleLastCheckIfNull()
        {
            var environment = new Environment();

            environment.mocks.transferToTestEquipmentClient.LoadLocationToolAssignmentsForTransferReturnValue =
                new ListOfLocationToolAssignmentForTransfer()
                {
                    Values =
                    {
                        new TransferToTestEquipmentService.LocationToolAssignmentForTransfer()
                        {
                            LastTestDate = new NullableDateTime() { IsNull = true },
                            NextTestDate = new NullableDateTime() { IsNull = true },
                            NextTestDateShift = new NullableInt() { Value = 1 }
                        }
                    }
                };

            var result = environment.dataAccess.LoadLocationToolAssignmentsForTransfer(TestType.Chk);
            Assert.IsNull(result.First().LastTestDate);
        }

        [TestCaseSource(nameof(LoadLocationToolAssignmentsForTransferReturnsCorrectValueData))]
        public void LoadLocationToolAssignmentsForTransferCallsTimeDataAccess(ListOfLocationToolAssignmentForTransfer datas)
        {
            var environment = new Environment();
            environment.mocks.transferToTestEquipmentClient.LoadLocationToolAssignmentsForTransferReturnValue = datas;

            environment.dataAccess.LoadLocationToolAssignmentsForTransfer(TestType.Chk);

            var assigner = new FrameworksAndDrivers.RemoteData.GRPC.T4Mapper.Assigner();
            var dataDates = new List<DateTime?>();
            foreach (var data in datas.Values)
            {
                DateTime? date = null;
                assigner.Assign((value) => { date = value; }, data.TestRuleNextCheck);
                dataDates.Add(date);

                DateTime? lastDate = null;
                assigner.Assign((value) => { lastDate = value; }, data.TestRuleLastCheck);
                dataDates.Add(lastDate);
            }

            Assert.AreEqual(dataDates.ToList(), environment.mocks.timeDataAccess.ConvertToLocalNullAbleParameter);
        }

        public class LocationToolAssignmentLocationTestData
        {
            public long AssignmentId;
            public string LocationNumber;
            public string LocationDescription;
        }

        public static List<List<LocationToolAssignmentLocationTestData>>
            FillingWithLocationToolAssignmentsDataAddsLocationDataData =
                new List<List<LocationToolAssignmentLocationTestData>>
                {
                    new List<LocationToolAssignmentLocationTestData>
                    {
                        new LocationToolAssignmentLocationTestData
                        {
                            AssignmentId = 5,
                            LocationNumber = "NumberFive",
                            LocationDescription = "DescriptionFive"
                        }
                    },
                    new List<LocationToolAssignmentLocationTestData>
                    {
                        new LocationToolAssignmentLocationTestData
                        {
                            AssignmentId = 7,
                            LocationNumber = "UltimateLounge",
                            LocationDescription = "ModerateNuisance"
                        }
                    },
                    new List<LocationToolAssignmentLocationTestData>
                    {
                        new LocationToolAssignmentLocationTestData
                        {
                            AssignmentId = 3,
                            LocationNumber = "PhilosophicalGrave",
                            LocationDescription = "BindingJail"
                        },
                        new LocationToolAssignmentLocationTestData
                        {
                            AssignmentId = 2,
                            LocationNumber = "FederalBearing",
                            LocationDescription = "NarrowDuke"
                        }
                    }
                };

        [TestCaseSource(nameof(FillingWithLocationToolAssignmentsDataAddsLocationDataData))]
        public void FillingWithLocationToolAssignmentsDataAddsLocationData(
            List<LocationToolAssignmentLocationTestData> testData)
        {
            var environment = new Environment();
            environment.mocks.locationToolAssignmentData.GetLocationToolAssignmentsByIdsReturnValue =
                TestDataToLocationToolAssignments(testData);
            var input = TestDataToDataGateResults(testData);
            var result = environment.dataAccess.FillWithLocationToolAssignmentsData(input);
            CollectionAssert.AreEquivalent(TestDataToExpectedLocationData(testData), ResultToLocationData(result));
        }

        public class LocationToolAssignmentToolTestData
        {
            public long AssignmentId;
            public string ToolInventoryNumber;
            public string ToolSerialNumber;
        }

        public static List<List<LocationToolAssignmentToolTestData>>
            FillingWithLocationToolAssignmentsDataAddsToolDataData =
                new List<List<LocationToolAssignmentToolTestData>>
                {
                    new List<LocationToolAssignmentToolTestData>
                    {
                        new LocationToolAssignmentToolTestData
                        {
                            AssignmentId = 5,
                            ToolInventoryNumber = "PaleSet",
                            ToolSerialNumber = "GentleAuction"
                        }
                    },
                    new List<LocationToolAssignmentToolTestData>
                    {
                        new LocationToolAssignmentToolTestData
                        {
                            AssignmentId = 7,
                            ToolInventoryNumber = "HappyText",
                            ToolSerialNumber = "FormidableGrave"
                        }
                    },
                    new List<LocationToolAssignmentToolTestData>
                    {
                        new LocationToolAssignmentToolTestData
                        {
                            AssignmentId = 2,
                            ToolInventoryNumber = "FlatPresentation",
                            ToolSerialNumber = "WholeAge"
                        },
                        new LocationToolAssignmentToolTestData
                        {
                            AssignmentId = 8,
                            ToolInventoryNumber = "ExactHandful",
                            ToolSerialNumber = "WellRow"
                        },
                        new LocationToolAssignmentToolTestData
                        {
                            AssignmentId = 3,
                            ToolInventoryNumber = "ElegantWarranty",
                            ToolSerialNumber = "OriginalSalt"
                        }
                    }
                };

        [TestCaseSource(nameof(FillingWithLocationToolAssignmentsDataAddsToolDataData))]
        public void FillingWithLocationToolAssignmentsDataAddsToolData(
            List<LocationToolAssignmentToolTestData> testData)
        {
            var environment = new Environment();
            environment.mocks.locationToolAssignmentData.GetLocationToolAssignmentsByIdsReturnValue =
                TestDataToLocationToolAssignments(testData);
            var input = TestDataToDataGateResults(testData);
            var result = environment.dataAccess.FillWithLocationToolAssignmentsData(input);
            CollectionAssert.AreEquivalent(TestDataToExpectedToolData(testData), ResultToToolData(result));
        }

        public static List<List<long>> LocationToolAssignmentIdsToLoad = new List<List<long>>
        {
            new List<long> { 5 },
            new List<long> { 8 },
            new List<long> { 17, 38, 999, 523, 420 }
        };

        [TestCaseSource(nameof(LocationToolAssignmentIdsToLoad))]
        public void FillingWithLocationToolAssignmentsAsksDataAccessForCorrectIds(List<long> assignmentIds)
        {
            var environment = new Environment();
            var input = new DataGateResults
            {
                Results = assignmentIds
                    .Select(assignment => new DataGateResult { LocationToolAssignmentId = assignment })
                    .ToList()
            };
            environment.dataAccess.FillWithLocationToolAssignmentsData(input);
            CollectionAssert.AreEquivalent(
                assignmentIds,
                environment.mocks.locationToolAssignmentData.GetLocationToolAssignmentsByIdsParameter
                    .Select(assignment => assignment.ToLong()));
        }

        [Test]
        public void FillingWithLocationToolAssignmentsSetsResultFromDataGate()
        {
            var environment = new Environment();
            var input = new DataGateResults
            {
                Results = new List<DataGateResult>
                {
                    new DataGateResult{LocationToolAssignmentId = 5}
                }
            };
            environment.mocks.locationToolAssignmentData.GetLocationToolAssignmentsByIdsReturnValue =
                new List<LocationToolAssignment> { CreateLocationToolAssignment.IdOnly(5) };
            var result = environment.dataAccess.FillWithLocationToolAssignmentsData(input);
            Assert.AreSame(input.Results[0], result[0].ResultFromDataGate);
        }

        [Test]
        public void FillingWithLocationToolAsisgnmentsSetsLocationToolAssignment()
        {
            var enviroment = new Environment();
            var input = new DataGateResults
            {
                Results = new List<DataGateResult>
                {
                    new DataGateResult{LocationToolAssignmentId = 5}
                }
            };
            var locationToolAssignment = CreateLocationToolAssignment.IdOnly(5);
            enviroment.mocks.locationToolAssignmentData.GetLocationToolAssignmentsByIdsReturnValue = new List<LocationToolAssignment> { locationToolAssignment };
            var result = enviroment.dataAccess.FillWithLocationToolAssignmentsData(input);
            Assert.AreSame(locationToolAssignment, result[0].LocationToolAssignment);
        }

        [TestCaseSource(nameof(TestEquipmentTestDataChk))]
        public void SaveTestEquipmentTestResultCallsClientInsertClassicChkTests((Core.Entities.TestEquipment testequipment, List<TestEquipmentTestResult> results, double cm, double cmk, Core.Entities.User user) data)
        {
            var environment = new Environment();
            environment.dataAccess.SaveTestEquipmentTestResult(data.testequipment, data.results, (data.cm, data.cmk), data.user);
            var param = environment.mocks.transferToTestEquipmentClient.InsertClassicChkTestsParameter;

            Assert.AreEqual(data.results.Count, param.ClassicChkTests.Count);

            var i = 0;
            foreach (var test in param.ClassicChkTests)
            {
                var testResult = data.results[i];
                Assert.IsTrue(EqualityChecker.ArePrimitiveDateTimeAndDtoEqual(testResult.TestTimestamp, test.LocalTimestamp));
                Assert.AreEqual(testResult.SampleCount, test.ClassicChkTest.NumberOfTests);
                Assert.AreEqual(testResult.LocationToolAssignment.AssignedTool.Id.ToLong(), test.ClassicChkTest.ToolId);
                Assert.AreEqual(testResult.Values.Min(), test.ClassicChkTest.TestValueMinimum);
                Assert.AreEqual(testResult.Values.Max(), test.ClassicChkTest.TestValueMaximum);
                Assert.AreEqual(testResult.Average, test.ClassicChkTest.Average);
                Assert.AreEqual(testResult.StandardDeviation.Value, test.ClassicChkTest.StandardDeviation.Value);
                Assert.AreEqual(testResult.TestResult.LongValue, test.ClassicChkTest.Result);
                Assert.AreEqual(testResult.ResultFromDataGate.Min1, test.ClassicChkTest.LowerLimitUnit1);
                Assert.AreEqual(testResult.ResultFromDataGate.Nom1, test.ClassicChkTest.NominalValueUnit1);
                Assert.AreEqual(testResult.ResultFromDataGate.Max1, test.ClassicChkTest.UpperLimitUnit1);
                Assert.AreEqual(testResult.ResultFromDataGate.Unit1Id, test.ClassicChkTest.Unit1Id);
                Assert.AreEqual(testResult.ResultFromDataGate.Min2, test.ClassicChkTest.LowerLimitUnit2);
                Assert.AreEqual(testResult.ResultFromDataGate.Nom2, test.ClassicChkTest.NominalValueUnit2);
                Assert.AreEqual(testResult.ResultFromDataGate.Max2, test.ClassicChkTest.UpperLimitUnit2);
                Assert.AreEqual(testResult.ResultFromDataGate.Unit2Id, test.ClassicChkTest.Unit2Id);
                Assert.AreEqual(testResult.LocationToolAssignment.TestParameters.ToleranceClassTorque.Id.ToLong(), test.ClassicChkTest.ToleranceClassUnit1);
                Assert.AreEqual(testResult.LocationToolAssignment.TestParameters.ToleranceClassAngle.Id.ToLong(), test.ClassicChkTest.ToleranceClassUnit2);
                Assert.AreEqual(testResult.LocationToolAssignment.TestParameters.ControlledBy, (LocationControlledBy)test.ClassicChkTest.ControlledByUnitId);
                Assert.AreEqual(data.user.UserId.ToLong(), test.ClassicChkTest.User.UserId);
                Assert.AreEqual(data.testequipment.Id.ToLong(), test.ClassicChkTest.TestEquipment.Id);
                Assert.AreEqual(testResult.LocationToolAssignment.TestParameters.ThresholdTorque.Degree, test.ClassicChkTest.ThresholdTorque);
                Assert.IsTrue(test.ClassicChkTest.SensorSerialNumber.IsNull);
                Assert.AreEqual(testResult.LocationToolAssignment.AssignedLocation.Id.ToLong(), test.ClassicChkTest.TestLocation.LocationId);
                Assert.AreEqual(testResult.LocationToolAssignment.AssignedLocation.ParentDirectoryId.ToLong(), test.ClassicChkTest.TestLocation.LocationDirectoryId);
                Assert.AreEqual(testResult.LocationTreePath, test.ClassicChkTest.TestLocation.TreePath.Value);
                Assert.IsTrue(EqualityChecker.ArePrimitiveDateTimeAndDtoEqual(testResult.TestTimestamp, test.ClassicChkTest.Timestamp));
                Assert.AreEqual(testResult.Values.Count, test.ClassicChkTest.TestValues.ClassicChkTestValues.Count);
                Assert.AreEqual(testResult.LocationToolAssignment.Id.ToLong(), test.ClassicChkTest.LocationToolAssignmentId);
                var j = 0;
                foreach (var val in testResult.ResultFromDataGate.Values)
                {
                    Assert.AreEqual(j, test.ClassicChkTest.TestValues.ClassicChkTestValues[j].Position);
                    Assert.AreEqual(val.Value1, test.ClassicChkTest.TestValues.ClassicChkTestValues[j].ValueUnit1);
                    Assert.AreEqual(val.Value2, test.ClassicChkTest.TestValues.ClassicChkTestValues[j].ValueUnit2);
                    j++;
                }
                i++;
            }
        }

        [TestCaseSource(nameof(TestEquipmentTestDataMfu))]
        public void SaveTestEquipmentTestResultCallsClientInsertClassicMfuTests((Core.Entities.TestEquipment testequipment, List<TestEquipmentTestResult> results, double cm, double cmk, Core.Entities.User user) data)
        {
            var environment = new Environment();
            environment.dataAccess.SaveTestEquipmentTestResult(data.testequipment, data.results, (data.cm, data.cmk), data.user);
            var param = environment.mocks.transferToTestEquipmentClient.InsertClassicMfuTestsParameter;

            Assert.AreEqual(data.results.Count, param.ClassicMfuTests.Count);

            var i = 0;
            foreach (var test in param.ClassicMfuTests)
            {
                var testResult = data.results[i];
                Assert.IsTrue(EqualityChecker.ArePrimitiveDateTimeAndDtoEqual(testResult.TestTimestamp, test.LocalTimestamp));
                Assert.AreEqual(testResult.SampleCount, test.ClassicMfuTest.NumberOfTests);
                Assert.AreEqual(testResult.LocationToolAssignment.AssignedTool.Id.ToLong(), test.ClassicMfuTest.ToolId);
                Assert.AreEqual(testResult.Values.Min(), test.ClassicMfuTest.TestValueMinimum);
                Assert.AreEqual(testResult.Values.Max(), test.ClassicMfuTest.TestValueMaximum);
                Assert.AreEqual(testResult.Average, test.ClassicMfuTest.Average);
                Assert.AreEqual(testResult.StandardDeviation.Value, test.ClassicMfuTest.StandardDeviation.Value);
                Assert.AreEqual(testResult.TestResult.LongValue, test.ClassicMfuTest.Result);
                Assert.AreEqual(testResult.ResultFromDataGate.Min1, test.ClassicMfuTest.LowerLimitUnit1);
                Assert.AreEqual(testResult.ResultFromDataGate.Nom1, test.ClassicMfuTest.NominalValueUnit1);
                Assert.AreEqual(testResult.ResultFromDataGate.Max1, test.ClassicMfuTest.UpperLimitUnit1);
                Assert.AreEqual(testResult.ResultFromDataGate.Unit1Id, test.ClassicMfuTest.Unit1Id);
                Assert.AreEqual(testResult.ResultFromDataGate.Min2, test.ClassicMfuTest.LowerLimitUnit2);
                Assert.AreEqual(testResult.ResultFromDataGate.Nom2, test.ClassicMfuTest.NominalValueUnit2);
                Assert.AreEqual(testResult.ResultFromDataGate.Max2, test.ClassicMfuTest.UpperLimitUnit2);
                Assert.AreEqual(testResult.ResultFromDataGate.Unit2Id, test.ClassicMfuTest.Unit2Id);
                Assert.AreEqual(testResult.LocationToolAssignment.TestParameters.ToleranceClassTorque.Id.ToLong(), test.ClassicMfuTest.ToleranceClassUnit1);
                Assert.AreEqual(testResult.LocationToolAssignment.TestParameters.ToleranceClassAngle.Id.ToLong(), test.ClassicMfuTest.ToleranceClassUnit2);
                Assert.AreEqual(testResult.LocationToolAssignment.TestParameters.ControlledBy, (LocationControlledBy)test.ClassicMfuTest.ControlledByUnitId);
                Assert.AreEqual(data.user.UserId.ToLong(), test.ClassicMfuTest.User.UserId);
                Assert.AreEqual(data.testequipment.Id.ToLong(), test.ClassicMfuTest.TestEquipment.Id);
                Assert.AreEqual(testResult.LocationToolAssignment.TestParameters.ThresholdTorque.Degree, test.ClassicMfuTest.ThresholdTorque);
                Assert.IsTrue(test.ClassicMfuTest.SensorSerialNumber.IsNull);
                Assert.AreEqual(testResult.LocationToolAssignment.AssignedLocation.Id.ToLong(), test.ClassicMfuTest.TestLocation.LocationId);
                Assert.AreEqual(testResult.LocationToolAssignment.AssignedLocation.ParentDirectoryId.ToLong(), test.ClassicMfuTest.TestLocation.LocationDirectoryId);
                Assert.AreEqual(testResult.LocationTreePath, test.ClassicMfuTest.TestLocation.TreePath.Value);
                Assert.IsTrue(EqualityChecker.ArePrimitiveDateTimeAndDtoEqual(testResult.TestTimestamp, test.ClassicMfuTest.Timestamp));
                Assert.AreEqual(testResult.Values.Count, test.ClassicMfuTest.TestValues.ClassicMfuTestValues.Count);
                Assert.AreEqual(testResult.LocationToolAssignment.Id.ToLong(), test.ClassicMfuTest.LocationToolAssignmentId);
                var j = 0;
                foreach (var val in testResult.ResultFromDataGate.Values)
                {
                    Assert.AreEqual(j, test.ClassicMfuTest.TestValues.ClassicMfuTestValues[j].Position);
                    Assert.AreEqual(val.Value1, test.ClassicMfuTest.TestValues.ClassicMfuTestValues[j].ValueUnit1);
                    Assert.AreEqual(val.Value2, test.ClassicMfuTest.TestValues.ClassicMfuTestValues[j].ValueUnit2);
                    j++;
                }
                i++;
            }
        }

        static IEnumerable<(Core.Entities.TestEquipment, List<TestEquipmentTestResult>, double, double, Core.Entities.User)> TestEquipmentTestDataChk =
            new List<(TestEquipment, List<TestEquipmentTestResult>, double, double, User)>()
        {
            (
                CreateTestEquipment.WithId(4),
                new List<TestEquipmentTestResult>()
                {
                    new TestEquipmentTestResult()
                    {
                        ResultFromDataGate = new DataGateResult()
                        {
                            LocationToolAssignmentId = 1,
                            Max1 = 7,
                            Max2 = 6,
                            Min1 = 1,
                            Min2 = 2,
                            Nom1 = 3,
                            Nom2 = 4,
                            Unit1Id = (long)LocationControlledBy.Torque,
                            Unit2Id = (long)LocationControlledBy.Angle,
                            Values = new List<DataGateResultValue>()
                            {
                                new DataGateResultValue()
                                {
                                    Timestamp = new DateTime(2021,10,1,3,4,5),
                                    Value1 = 4,
                                    Value2 = 6
                                },
                                new DataGateResultValue()
                                {
                                    Timestamp = new DateTime(2020,5,12,12,44,5),
                                    Value1 = 4.6,
                                    Value2 = 6.1
                                },
                                new DataGateResultValue()
                                {
                                    Timestamp = new DateTime(2020,5,12,12,44,5),
                                    Value1 = 4.4,
                                    Value2 = 6.2
                                }
                            }
                        },
                        LocationToolAssignment = CreateLocationToolAssignment.WithLocationWithToolAndTestParam(
                            CreateLocation.IdOnly(1), CreateTool.WithId(2), new Core.Entities.TestParameters(){ToleranceClassAngle = CreateToleranceClass.WithId(7),
                            ToleranceClassTorque = CreateToleranceClass.WithId(7), ThresholdTorque = Angle.FromDegree(12), ControlledBy = LocationControlledBy.Angle}),
                        LocationNumber = new LocationNumber("2123245"),
                        LocationDescription = new LocationDescription("Schraubstelle 1"),
                        LocationTreePath = "Ordner",
                        TestResult = new TestResult(1),
                        ToolInventoryNumber = "2345345647",
                        ToolSerialNumber = "345468",
                        Values = { 1,2,3,4,5}
                    }
                },
                1.56,
                1.77,
                CreateUser.IdOnly(1)
            ),
            (
                CreateTestEquipment.WithId(4),
                new List<TestEquipmentTestResult>()
                {
                    new TestEquipmentTestResult()
                    {
                        ResultFromDataGate = new DataGateResult()
                        {
                            LocationToolAssignmentId = 21,
                            Max1 = 72,
                            Max2 = 62,
                            Min1 = 12,
                            Min2 = 22,
                            Nom1 = 32,
                            Nom2 = 42,
                            Unit1Id = (long)LocationControlledBy.Torque,
                            Unit2Id = (long)LocationControlledBy.Angle,
                            Values = new List<DataGateResultValue>()
                            {
                                new DataGateResultValue()
                                {
                                    Timestamp = new DateTime(2019,1,12,3,4,5),
                                    Value1 = 342,
                                    Value2 = 624
                                },
                                new DataGateResultValue()
                                {
                                    Timestamp = new DateTime(2000,5,22,12,44,5),
                                    Value1 = 462.6,
                                    Value2 = 672.14
                                },
                                new DataGateResultValue()
                                {
                                    Timestamp = new DateTime(2020,5,12,12,44,5),
                                    Value1 = 42.44,
                                    Value2 = 642.2
                                }
                            }
                        },
                        LocationToolAssignment = CreateLocationToolAssignment.WithLocationWithToolAndTestParam(
                            CreateLocation.IdOnly(21), CreateTool.WithId(22), new Core.Entities.TestParameters(){ToleranceClassAngle = CreateToleranceClass.WithId(27),
                            ToleranceClassTorque = CreateToleranceClass.WithId(27), ThresholdTorque = Angle.FromDegree(212), ControlledBy = LocationControlledBy.Torque}),
                        LocationNumber = new LocationNumber("2222"),
                        LocationDescription = new LocationDescription("Schraubstelle 12"),
                        LocationTreePath = "Ordner",
                        TestResult = new TestResult(12),
                        ToolInventoryNumber = "87679708",
                        ToolSerialNumber = "ewrewr",
                        Values = { 1,2,3,4,5,5,6,7}
                    }
                },
                2.56,
                2.77,
                CreateUser.IdOnly(21)
            )
        };
        static IEnumerable<(Core.Entities.TestEquipment, List<TestEquipmentTestResult>, double, double, Core.Entities.User)> TestEquipmentTestDataMfu =
            new List<(TestEquipment, List<TestEquipmentTestResult>, double, double, User)>()
        {
            (
                CreateTestEquipment.WithId(4),
                new List<TestEquipmentTestResult>()
                {
                    new TestEquipmentTestResult()
                    {
                        ResultFromDataGate = new DataGateResult()
                        {
                            LocationToolAssignmentId = 21,
                            Max1 = 72,
                            Max2 = 62,
                            Min1 = 12,
                            Min2 = 22,
                            Nom1 = 32,
                            Nom2 = 42,
                            Unit1Id = (long)LocationControlledBy.Torque,
                            Unit2Id = (long)LocationControlledBy.Angle,
                            Values = new List<DataGateResultValue>()
                            {
                                new DataGateResultValue()
                                {
                                    Timestamp = new DateTime(2010,1,12,3,4,5),
                                    Value1 = 1,
                                    Value2 = 2
                                },
                                new DataGateResultValue()
                                {
                                    Timestamp = new DateTime(2011,5,22,12,44,5),
                                    Value1 = 3,
                                    Value2 = 4
                                },
                                new DataGateResultValue()
                                {
                                    Timestamp = new DateTime(2012,5,12,12,44,5),
                                    Value1 = 5,
                                    Value2 = 6
                                },
                                new DataGateResultValue()
                                {
                                    Timestamp = new DateTime(2013,1,12,3,4,5),
                                    Value1 = 7,
                                    Value2 = 8
                                },
                                new DataGateResultValue()
                                {
                                    Timestamp = new DateTime(2014,5,22,12,44,5),
                                    Value1 = 9,
                                    Value2 = 10
                                },
                                new DataGateResultValue()
                                {
                                    Timestamp = new DateTime(2015,5,12,12,44,5),
                                    Value1 = 11,
                                    Value2 = 12
                                },
                                new DataGateResultValue()
                                {
                                    Timestamp = new DateTime(2016,1,12,3,4,5),
                                    Value1 = 13,
                                    Value2 = 14
                                },
                                new DataGateResultValue()
                                {
                                    Timestamp = new DateTime(2017,5,22,12,44,5),
                                    Value1 = 15,
                                    Value2 = 16
                                },
                                new DataGateResultValue()
                                {
                                    Timestamp = new DateTime(2018,5,12,12,44,5),
                                    Value1 = 17,
                                    Value2 = 18
                                },
                                new DataGateResultValue()
                                {
                                    Timestamp = new DateTime(2019,5,12,12,44,5),
                                    Value1 = 19,
                                    Value2 = 20
                                },
                                new DataGateResultValue()
                                {
                                    Timestamp = new DateTime(2020,5,12,12,44,5),
                                    Value1 = 21,
                                    Value2 = 22
                                }
                            }
                        },
                        LocationToolAssignment = CreateLocationToolAssignment.WithLocationWithToolAndTestParam(
                            CreateLocation.IdOnly(21), CreateTool.WithId(22), new Core.Entities.TestParameters(){ToleranceClassAngle = CreateToleranceClass.WithId(27),
                            ToleranceClassTorque = CreateToleranceClass.WithId(27), ThresholdTorque = Angle.FromDegree(212), ControlledBy = LocationControlledBy.Torque}),
                        LocationNumber = new LocationNumber("2222"),
                        LocationDescription = new LocationDescription("Schraubstelle 12"),
                        LocationTreePath = "Ordner",
                        TestResult = new TestResult(12),
                        ToolInventoryNumber = "87679708",
                        ToolSerialNumber = "ewrewr",
                        Values = { 1,2,3,4,5,5,6,7,8,9,10,11,12,13,14}
                    }
                },
                2.56,
                2.77,
                CreateUser.IdOnly(21)
            )
        };



        private static List<LocationToolAssignment> TestDataToLocationToolAssignments(List<LocationToolAssignmentLocationTestData> testData)
        {
            var assignments = testData.Select(element =>
            {
                var assignment2 = CreateLocationToolAssignment.IdOnly(element.AssignmentId);
                assignment2.AssignedLocation =
                    CreateLocation.WithIdNumberDescription(0, element.LocationNumber, element.LocationDescription);
                return assignment2;
            }).ToList();
            return assignments;
        }

        private static List<LocationToolAssignment> TestDataToLocationToolAssignments(List<LocationToolAssignmentToolTestData> testData)
        {
            var assignments = testData.Select(element =>
            {
                var assignment2 = CreateLocationToolAssignment.IdOnly(element.AssignmentId);
                assignment2.AssignedTool =
                    CreateTool.WithIdInventoryAndSerialNumber(0, element.ToolInventoryNumber, element.ToolSerialNumber);
                return assignment2;
            }).ToList();
            return assignments;
        }

        private static DataGateResults TestDataToDataGateResults(List<LocationToolAssignmentLocationTestData> testData)
        {
            var input = new DataGateResults
            {
                Results = testData
                    .Select(
                        element
                            => new DataGateResult { LocationToolAssignmentId = element.AssignmentId })
                    .ToList()
            };
            return input;
        }

        private static DataGateResults TestDataToDataGateResults(List<LocationToolAssignmentToolTestData> testData)
        {
            var input = new DataGateResults
            {
                Results = testData
                    .Select(
                        element
                            => new DataGateResult { LocationToolAssignmentId = element.AssignmentId })
                    .ToList()
            };
            return input;
        }

        private static List<(string, string)> ResultToLocationData(List<TestEquipmentTestResult> result)
        {
            return result.Select(element => (element.LocationNumber.ToDefaultString(), element.LocationDescription.ToDefaultString())).ToList();
        }

        private static List<(string, string)> ResultToToolData(List<TestEquipmentTestResult> result)
        {
            return result.Select(element => (element.ToolInventoryNumber, element.ToolSerialNumber)).ToList();
        }

        private static List<(string LocationNumber, string LocationDescription)> TestDataToExpectedLocationData(List<LocationToolAssignmentLocationTestData> testData)
        {
            return testData.Select(element => (element.LocationNumber, element.LocationDescription)).ToList();
        }

        private static List<(string LocationNumber, string LocationDescription)> TestDataToExpectedToolData(List<LocationToolAssignmentToolTestData> testData)
        {
            return testData.Select(element => (element.ToolInventoryNumber, element.ToolSerialNumber)).ToList();
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    locationToolAssignmentData = new LocationToolAssignmentDataMock();
                    clientFactory = new ClientFactoryMock();
                    channelWrapper = new ChannelWrapperMock();
                    transferToTestEquipmentClient = new TransferToTestEquipmentClientMock();
                    channelWrapper.GetTransferToTestEquipmentClientReturnValue = transferToTestEquipmentClient;
                    clientFactory.AuthenticationChannel = channelWrapper;
                    timeDataAccess = new TimeDataAccessMock();
                }
                public ClientFactoryMock clientFactory;
                public ChannelWrapperMock channelWrapper;
                public TransferToTestEquipmentClientMock transferToTestEquipmentClient;
                public TimeDataAccessMock timeDataAccess;
                public LocationToolAssignmentDataMock locationToolAssignmentData;
            }

            public Environment()
            {
                mocks = new Mocks();
                dataAccess = new TransferToTestEquipmentDataAccess(mocks.clientFactory, mocks.timeDataAccess,
                    mocks.locationToolAssignmentData);
            }

            public readonly Mocks mocks;
            public readonly TransferToTestEquipmentDataAccess dataAccess;
        }
    }
}
