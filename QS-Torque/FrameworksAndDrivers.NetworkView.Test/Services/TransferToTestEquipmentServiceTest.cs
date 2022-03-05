using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Common.Types.Enums;
using Core.Entities;
using DtoTypes;
using NUnit.Framework;
using Server.Core;
using Server.Core.Entities;
using Server.Core.Enums;
using Server.UseCases.UseCases;
using TestHelper.Checker;
using ClassicChkTest = Server.Core.Entities.ClassicChkTest;
using ClassicMfuTest = Server.Core.Entities.ClassicMfuTest;
using DateTime = BasicTypes.DateTime;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    public class TransferToTestEquipmentUseCaseMock : ITransferToTestEquipmentUseCase
    {
        public List<LocationToolAssignmentForTransfer> LoadLocationToolAssignmentsForTransferReturnValue { get; set; } = new List<LocationToolAssignmentForTransfer>(); 
        public TestType LoadLocationToolAssignmentsForTransferParameter { get; set; }
        public Dictionary<ClassicChkTest, System.DateTime> InsertClassicChkTestsParameter { get; set; }
        public Dictionary<ClassicMfuTest, System.DateTime> InsertClassicMfuTestsParameter { get; set; }
        public List<ProcessControlForTransfer> LoadProcessControlDataForTransferReturnValue { get; set; } = new List<ProcessControlForTransfer>();
        public bool LoadProcessControlDataForTransferCalled { get; set; }

        public List<ProcessControlForTransfer> LoadProcessControlDataForTransfer()
        {
            LoadProcessControlDataForTransferCalled = true;
            return LoadProcessControlDataForTransferReturnValue;
        }

        public void InsertClassicChkTests(Dictionary<ClassicChkTest, System.DateTime> tests)
        {
            InsertClassicChkTestsParameter = tests;
        }

        public void InsertClassicMfuTests(Dictionary<ClassicMfuTest, System.DateTime> tests)
        {
            InsertClassicMfuTestsParameter = tests;
        }

        public List<LocationToolAssignmentForTransfer> LoadLocationToolAssignmentsForTransfer(TestType testType)
        {
            LoadLocationToolAssignmentsForTransferParameter = testType;
            return LoadLocationToolAssignmentsForTransferReturnValue;
        }
    }

    class TransferToTestEquipmentServiceTest
    {

        [Test]
        public void LoadLocationToolAssignmentsForTransferCallsUseCase()
        {
            var useCase = new TransferToTestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TransferToTestEquipmentService(null, useCase);

            service.LoadProcessControlDataForTransfer(new NoParams(), null);

            Assert.IsTrue(useCase.LoadProcessControlDataForTransferCalled);
        }

        private static IEnumerable<List<ProcessControlForTransfer>> ProcessControlForTransferData =
            new List<List<ProcessControlForTransfer>>()
            {
                new List<ProcessControlForTransfer>()
                {
                    new Server.Core.Entities.ProcessControlForTransfer()
                    {
                        LocationId = new LocationId(1),
                        LocationNumber = new LocationNumber("12345"),
                        LocationDescription = new LocationDescription("7890"),
                        ProcessControlConditionId = new ProcessControlConditionId(1),
                        ProcessControlTechId = new ProcessControlTechId(8),
                        TestMethod = TestMethod.QST_MT,
                        SampleNumber = 99,
                        LastTestDate = new System.DateTime(2020,10,1,20,20,21),
                        NextTestDate = new System.DateTime(2021,1,2,3,4,5),
                        MaximumTorque = 15.1,
                        MinimumTorque = 5.1,
                        SetPointTorque = 10.4,
                        TestInterval = new Core.Entities.Interval()
                        {
                            Type = IntervalType.EveryXDays,
                            IntervalValue = 4
                        },
                        NextTestDateShift = Shift.FirstShiftOfDay
                    },
                    new Server.Core.Entities.ProcessControlForTransfer()
                    {
                        LocationId = new LocationId(13),
                        LocationNumber = new LocationNumber("abc"),
                        LocationDescription = new LocationDescription("def"),
                        ProcessControlConditionId = new ProcessControlConditionId(11),
                        ProcessControlTechId = new ProcessControlTechId(81),
                        TestMethod = TestMethod.QST_PA,
                        SampleNumber = 101,
                        LastTestDate = new System.DateTime(2019,2,4,5,20,21),
                        NextTestDate = new System.DateTime(2022,3,6,4,4,5),
                        MaximumTorque = 115.1,
                        MinimumTorque = 52.1,
                        SetPointTorque = 110.4,
                        TestInterval = new Core.Entities.Interval()
                        {
                            Type = IntervalType.EveryXShifts,
                            IntervalValue = 4
                        },
                        NextTestDateShift = Shift.SecondShiftOfDay
                    }
                },
                new List<ProcessControlForTransfer>()
                {
                    new Server.Core.Entities.ProcessControlForTransfer()
                    {
                        LocationId = new LocationId(134),
                        LocationNumber = new LocationNumber("hhh"),
                        LocationDescription = new LocationDescription("4444"),
                        ProcessControlConditionId = new ProcessControlConditionId(15),
                        ProcessControlTechId = new ProcessControlTechId(86),
                        TestMethod = TestMethod.QST_MT,
                        SampleNumber = 1,
                        LastTestDate = new System.DateTime(2000,10,1,20,20,21),
                        NextTestDate = new System.DateTime(2001,1,2,3,4,5),
                        MaximumTorque = 135.1,
                        MinimumTorque = 5.15,
                        SetPointTorque = 106.4,
                        TestInterval = new Core.Entities.Interval()
                        {
                            Type = IntervalType.XTimesADay,
                            IntervalValue = 5
                        },
                        NextTestDateShift = null
                    }
                }
            };

        [TestCaseSource(nameof(ProcessControlForTransferData))]
        public void LoadProcessControlDataForTransferReturnsCorrectValue(List<ProcessControlForTransfer> data)
        {
            var useCase = new TransferToTestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TransferToTestEquipmentService(null, useCase);
            useCase.LoadProcessControlDataForTransferReturnValue = data;

            var result = service.LoadProcessControlDataForTransfer(new NoParams(), null);

            var comparer = new Func<ProcessControlForTransfer, TransferToTestEquipmentService.ProcessControlDataForTransfer, bool>((dto, entity) =>
                EqualityChecker.CompareProcessControlForTransferWithDto(entity, dto)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data, result.Result.Values, comparer);
        }


        [TestCase(TestType.Chk)]
        [TestCase(TestType.Mfu)]
        public void LoadLocationToolAssignmentsForTransferCallsUseCase(TestType testType)
        {
            var useCase = new TransferToTestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TransferToTestEquipmentService(null, useCase);

            service.LoadLocationToolAssignmentsForTransfer(new Long() {Value = (long)testType}, null);

            Assert.AreEqual(testType, useCase.LoadLocationToolAssignmentsForTransferParameter);
        }

        private static IEnumerable<List<LocationToolAssignmentForTransfer>> locationToolAssignmentDataForTransfer = new List<List<LocationToolAssignmentForTransfer>>()
        {
            new List<LocationToolAssignmentForTransfer>()
            {
                new LocationToolAssignmentForTransfer()
                {
                    LocationId = new LocationId(1),
                    LocationToolAssignmentId = new LocationToolAssignmentId(2),
                    ToolId = new ToolId(3),
                    ToolUsageId = new HelperTableEntityId(5),
                    LocationNumber = new LocationNumber("345456578"),
                    ToolSerialNumber = "435456587",
                    ToolInventoryNumber = "Abcdefgh",
                    ToolUsageDescription = new ToolUsageDescription("X01"),
                    LocationDescription = new LocationDescription("SST`1"),
                    LocationFreeFieldCategory = "L",
                    LocationFreeFieldDocumentation = true,
                    LastTestDate =  new System.DateTime(2020,1,1,1,2,4),
                    NextTestDate =  new System.DateTime(2016,6,1,1,2,4),
                    NextTestDateShift = (Shift?)1,
                    SampleNumber = 1
                },
                new LocationToolAssignmentForTransfer()
                {
                    LocationId = new LocationId(13),
                    LocationToolAssignmentId = new LocationToolAssignmentId(25),
                    ToolId = new ToolId(33),
                    ToolUsageId = new HelperTableEntityId(5),
                    LocationNumber = new LocationNumber("00000001"),
                    ToolSerialNumber = "11111110",
                    ToolInventoryNumber = "xyz",
                    ToolUsageDescription = new ToolUsageDescription("L01"),
                    LocationDescription = new LocationDescription("SST`1A"),
                    LocationFreeFieldCategory = "D",
                    LocationFreeFieldDocumentation = false,
                    LastTestDate =  new System.DateTime(2021,11,4,1,2,4),
                    NextTestDate =  new System.DateTime(2016,6,1,1,2,4),
                    NextTestDateShift = (Shift?)1,
                    SampleNumber = 1
                }
            },
            new List<LocationToolAssignmentForTransfer>()
            {
                new LocationToolAssignmentForTransfer()
                {
                    LocationId = new LocationId(31),
                    LocationToolAssignmentId = new LocationToolAssignmentId(25),
                    ToolId = new ToolId(83),
                    ToolUsageId = new HelperTableEntityId(55),
                    LocationNumber = new LocationNumber("777776"),
                    ToolSerialNumber = "000001",
                    ToolInventoryNumber = "Abcdefgh",
                    ToolUsageDescription = new ToolUsageDescription("X01"),
                    LocationDescription = new LocationDescription("SST`1"),
                    LocationFreeFieldCategory = "L",
                    LocationFreeFieldDocumentation = false,
                    LastTestDate =  new System.DateTime(2010,1,1,1,2,4),
                    NextTestDate =  new System.DateTime(2006,6,1,1,2,4),
                    NextTestDateShift = (Shift?)1,
                    SampleNumber = 1
                }
            }
        };

        [TestCaseSource(nameof(locationToolAssignmentDataForTransfer))]
        public void LoadLocationToolAssignmentsForTransferReturnsCorrectValue(List<LocationToolAssignmentForTransfer> data)
        {
            var useCase = new TransferToTestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TransferToTestEquipmentService(null, useCase);
            useCase.LoadLocationToolAssignmentsForTransferReturnValue = data;

            var result = service.LoadLocationToolAssignmentsForTransfer(new Long(), null);

            var comparer = new Func<LocationToolAssignmentForTransfer, TransferToTestEquipmentService.LocationToolAssignmentForTransfer, bool>((dto, entity) =>
                EqualityChecker.CompareLocationToolAssignmentsForTransferDtoWithLocationToolAssignmentForTransfer(entity, dto)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data, result.Result.Values, comparer);
        }

        static IEnumerable<ListOfClassicChkTestWithLocalTimestamp> InsertClassicChkTestsCallsUseCaseData =
            new List<ListOfClassicChkTestWithLocalTimestamp>()
            {
                new ListOfClassicChkTestWithLocalTimestamp()
                {
                    ClassicChkTests =
                    {
                        new ClassicChkTestWithLocalTimestamp()
                        {
                            ClassicChkTest = new DtoTypes.ClassicChkTest()
                            {
                                Id = 1,
                                Result = 2,
                                Average = 3,
                                ControlledByUnitId = 4,
                                LowerLimitUnit1 = 5,
                                LowerLimitUnit2 = 6,
                                NominalValueUnit1 = 7,
                                NominalValueUnit2 = 8,
                                NumberOfTests = 9,
                                TestValueMaximum = 10,
                                TestValueMinimum = 11,
                                ThresholdTorque = 12,
                                ToleranceClassUnit1 = 13,
                                ToleranceClassUnit2 = 14,
                                ToolId = 15,
                                Unit1Id = 16,
                                Unit2Id = 17,
                                UpperLimitUnit1 = 18,
                                UpperLimitUnit2 = 19,
                                TestEquipment = new DtoTypes.TestEquipment(){Id = 21},
                                User = new DtoTypes.User(){UserId = 21},
                                SensorSerialNumber = new NullableString() {IsNull = false, Value = "22"},
                                StandardDeviation = new NullableDouble() {IsNull = false, Value = 23},
                                Timestamp = new BasicTypes.DateTime() {Ticks = new System.DateTime(2021, 2, 1).Ticks},
                                TestLocation = new DtoTypes.ClassicTestLocation()
                                {
                                    LocationId = 24,
                                    LocationDirectoryId = 25,
                                    TreePath =  new NullableString(){IsNull = false, Value = "26"}
                                },
                                TestValues = new ListOfClassicChkTestValue()
                                {
                                    ClassicChkTestValues =
                                    {
                                        new DtoTypes.ClassicChkTestValue()
                                        {
                                            Id = 27,
                                            Position = 28,
                                            ValueUnit1 = 29,
                                            ValueUnit2 = 30
                                        },
                                        new DtoTypes.ClassicChkTestValue()
                                        {
                                            Id = 31,
                                            Position = 32,
                                            ValueUnit1 = 33,
                                            ValueUnit2 = 34
                                        }
                                    }
                                }
                            },
                            LocalTimestamp = new DateTime() { Ticks = 983216954 }
                        },
                        new ClassicChkTestWithLocalTimestamp()
                        {
                            ClassicChkTest = new DtoTypes.ClassicChkTest()
                            {
                                Id = 11,
                                Result = 12,
                                Average = 13,
                                ControlledByUnitId = 14,
                                LowerLimitUnit1 = 15,
                                LowerLimitUnit2 = 16,
                                NominalValueUnit1 = 17,
                                NominalValueUnit2 = 18,
                                NumberOfTests = 19,
                                TestValueMaximum = 110,
                                TestValueMinimum = 111,
                                ThresholdTorque = 112,
                                ToleranceClassUnit1 = 113,
                                ToleranceClassUnit2 = 114,
                                ToolId = 115,
                                Unit1Id = 116,
                                Unit2Id = 117,
                                UpperLimitUnit1 = 118,
                                UpperLimitUnit2 = 119,
                                TestEquipment = new DtoTypes.TestEquipment(){Id = 121},
                                User = new DtoTypes.User(){UserId = 121},
                                SensorSerialNumber = new NullableString() {IsNull = false, Value = "122"},
                                StandardDeviation = new NullableDouble() {IsNull = false, Value = 123},
                                Timestamp = new BasicTypes.DateTime() {Ticks = new System.DateTime(2021, 12, 11).Ticks},
                                TestLocation = new DtoTypes.ClassicTestLocation()
                                {
                                    LocationId = 124,
                                    LocationDirectoryId = 125,
                                    TreePath = new NullableString(){IsNull = false, Value = "126"}
                                },
                                TestValues = new ListOfClassicChkTestValue()
                                {
                                    ClassicChkTestValues =
                                    {
                                        new DtoTypes.ClassicChkTestValue()
                                        {
                                            Id = 127,
                                            Position = 128,
                                            ValueUnit1 = 129,
                                            ValueUnit2 = 130
                                        },
                                        new DtoTypes.ClassicChkTestValue()
                                        {
                                            Id = 131,
                                            Position = 132,
                                            ValueUnit1 = 133,
                                            ValueUnit2 = 134
                                        }
                                    }
                                }
                            },
                            LocalTimestamp = new DateTime() { Ticks = 3216549874 }
                        }
                    }
                },
                new ListOfClassicChkTestWithLocalTimestamp()
                {
                    ClassicChkTests =
                    {
                        new ClassicChkTestWithLocalTimestamp()
                        {
                            ClassicChkTest = new DtoTypes.ClassicChkTest()
                            {
                                Id = 41,
                                Result = 42,
                                Average = 43,
                                ControlledByUnitId = 44,
                                LowerLimitUnit1 = 45,
                                LowerLimitUnit2 = 46,
                                NominalValueUnit1 = 47,
                                NominalValueUnit2 = 48,
                                NumberOfTests = 49,
                                TestValueMaximum = 410,
                                TestValueMinimum = 411,
                                ThresholdTorque = 412,
                                ToleranceClassUnit1 = 413,
                                ToleranceClassUnit2 = 414,
                                ToolId = 415,
                                Unit1Id = 416,
                                Unit2Id = 417,
                                UpperLimitUnit1 = 418,
                                UpperLimitUnit2 = 419,
                                TestEquipment = new DtoTypes.TestEquipment(){Id = 421},
                                User = new DtoTypes.User(){UserId = 421},
                                SensorSerialNumber = new NullableString() {IsNull = false, Value = "422"},
                                StandardDeviation = new NullableDouble() {IsNull = false, Value = 423},
                                Timestamp = new BasicTypes.DateTime() {Ticks = new System.DateTime(2021, 4, 1).Ticks},
                                TestLocation = new DtoTypes.ClassicTestLocation()
                                {
                                    LocationId = 424,
                                    LocationDirectoryId = 425,
                                    TreePath =  new NullableString(){IsNull = false, Value = "426"}
                                },
                                TestValues = new ListOfClassicChkTestValue()
                                {
                                    ClassicChkTestValues =
                                    {
                                        new DtoTypes.ClassicChkTestValue()
                                        {
                                            Id = 427,
                                            Position = 428,
                                            ValueUnit1 = 429,
                                            ValueUnit2 = 430
                                        },
                                        new DtoTypes.ClassicChkTestValue()
                                        {
                                            Id = 431,
                                            Position = 432,
                                            ValueUnit1 = 433,
                                            ValueUnit2 = 434
                                        }
                                    }
                                }
                            },
                            LocalTimestamp = new DateTime() { Ticks = 95135765 }
                        }
                    }
                }
            };

        [TestCaseSource(nameof(InsertClassicChkTestsCallsUseCaseData))]
        public void InsertClassicChkTestsCallsUseCase(ListOfClassicChkTestWithLocalTimestamp data)
        {
            var useCase = new TransferToTestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TransferToTestEquipmentService(null, useCase);

            service.InsertClassicChkTests(data, null);


            var comparer = new Func<DtoTypes.ClassicChkTestWithLocalTimestamp, KeyValuePair<ClassicChkTest, System.DateTime>, bool>((dto, entity) =>
                EqualityChecker.CompareClassicChkTestWithDto(entity.Key, dto.ClassicChkTest) && EqualityChecker.ArePrimitiveDateTimeAndDtoEqual(entity.Value, dto.LocalTimestamp)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.ClassicChkTests, useCase.InsertClassicChkTestsParameter.ToList(), comparer);
        }

        static IEnumerable<ListOfClassicMfuTestWithLocalTimestamp> InsertClassicMfuTestsCallsUseCaseData =
            new List<ListOfClassicMfuTestWithLocalTimestamp>()
            {
                new ListOfClassicMfuTestWithLocalTimestamp()
                {
                    ClassicMfuTests =
                    {
                        new ClassicMfuTestWithLocalTimestamp()
                        {
                            ClassicMfuTest = new DtoTypes.ClassicMfuTest()
                            {
                                Id = 1,
                                Result = 2,
                                Average = 3,
                                ControlledByUnitId = 4,
                                LowerLimitUnit1 = 5,
                                LowerLimitUnit2 = 6,
                                NominalValueUnit1 = 7,
                                NominalValueUnit2 = 8,
                                NumberOfTests = 9,
                                TestValueMaximum = 10,
                                TestValueMinimum = 11,
                                ThresholdTorque = 12,
                                ToleranceClassUnit1 = 13,
                                ToleranceClassUnit2 = 14,
                                ToolId = 15,
                                Unit1Id = 16,
                                Unit2Id = 17,
                                UpperLimitUnit1 = 18,
                                UpperLimitUnit2 = 19,
                                Cm = 1.88,
                                Cmk = 1.99,
                                TestEquipment = new DtoTypes.TestEquipment(){Id = 21},
                                User = new DtoTypes.User(){UserId = 21},
                                SensorSerialNumber = new NullableString() {IsNull = false, Value = "22"},
                                StandardDeviation = new NullableDouble() {IsNull = false, Value = 23},
                                Timestamp = new BasicTypes.DateTime() {Ticks = new System.DateTime(2021, 2, 1).Ticks},
                                TestLocation = new DtoTypes.ClassicTestLocation()
                                {
                                    LocationId = 24,
                                    LocationDirectoryId = 25,
                                    TreePath = new NullableString(){IsNull = false, Value = "26"}
                                },
                                TestValues = new ListOfClassicMfuTestValue()
                                {
                                    ClassicMfuTestValues =
                                    {
                                        new DtoTypes.ClassicMfuTestValue()
                                        {
                                            Id = 27,
                                            Position = 28,
                                            ValueUnit1 = 29,
                                            ValueUnit2 = 30
                                        },
                                        new DtoTypes.ClassicMfuTestValue()
                                        {
                                            Id = 31,
                                            Position = 32,
                                            ValueUnit1 = 33,
                                            ValueUnit2 = 34
                                        }
                                    }
                                }
                            },
                            LocalTimestamp = new DateTime() { Ticks = 654987321 }
                        },
                        new ClassicMfuTestWithLocalTimestamp()
                        {
                            ClassicMfuTest = new DtoTypes.ClassicMfuTest()
                            {
                                Id = 11,
                                Result = 12,
                                Average = 13,
                                ControlledByUnitId = 14,
                                LowerLimitUnit1 = 15,
                                LowerLimitUnit2 = 16,
                                NominalValueUnit1 = 17,
                                NominalValueUnit2 = 18,
                                NumberOfTests = 19,
                                TestValueMaximum = 110,
                                TestValueMinimum = 111,
                                ThresholdTorque = 112,
                                ToleranceClassUnit1 = 113,
                                ToleranceClassUnit2 = 114,
                                ToolId = 115,
                                Unit1Id = 116,
                                Unit2Id = 117,
                                UpperLimitUnit1 = 118,
                                UpperLimitUnit2 = 119,
                                Cm = 1.67,
                                Cmk = 1.77,
                                TestEquipment = new DtoTypes.TestEquipment(){Id = 121},
                                User = new DtoTypes.User(){UserId = 121},
                                SensorSerialNumber = new NullableString() {IsNull = false, Value = "122"},
                                StandardDeviation = new NullableDouble() {IsNull = false, Value = 123},
                                Timestamp = new BasicTypes.DateTime() {Ticks = new System.DateTime(2021, 12, 11).Ticks},
                                TestLocation = new DtoTypes.ClassicTestLocation()
                                {
                                    LocationId = 124,
                                    LocationDirectoryId = 125,
                                    TreePath =  new NullableString(){IsNull = false, Value = "126"}
                                },
                                TestValues = new ListOfClassicMfuTestValue()
                                {
                                    ClassicMfuTestValues =
                                    {
                                        new DtoTypes.ClassicMfuTestValue()
                                        {
                                            Id = 127,
                                            Position = 128,
                                            ValueUnit1 = 129,
                                            ValueUnit2 = 130
                                        },
                                        new DtoTypes.ClassicMfuTestValue()
                                        {
                                            Id = 131,
                                            Position = 132,
                                            ValueUnit1 = 133,
                                            ValueUnit2 = 134
                                        }
                                    }
                                }
                            },
                            LocalTimestamp = new DateTime() { Ticks = 98654213 }
                        }
                    }
                },
                new ListOfClassicMfuTestWithLocalTimestamp()
                {
                    ClassicMfuTests =
                    {
                        new ClassicMfuTestWithLocalTimestamp()
                        {
                            ClassicMfuTest = new DtoTypes.ClassicMfuTest()
                            {
                                Id = 41,
                                Result = 42,
                                Average = 43,
                                ControlledByUnitId = 44,
                                LowerLimitUnit1 = 45,
                                LowerLimitUnit2 = 46,
                                NominalValueUnit1 = 47,
                                NominalValueUnit2 = 48,
                                NumberOfTests = 49,
                                TestValueMaximum = 410,
                                TestValueMinimum = 411,
                                ThresholdTorque = 412,
                                ToleranceClassUnit1 = 413,
                                ToleranceClassUnit2 = 414,
                                ToolId = 415,
                                Unit1Id = 416,
                                Unit2Id = 417,
                                UpperLimitUnit1 = 418,
                                UpperLimitUnit2 = 419,
                                Cm = 1.22,
                                Cmk = 1.22,
                                TestEquipment = new DtoTypes.TestEquipment(){Id = 421},
                                User = new DtoTypes.User(){UserId = 421},
                                SensorSerialNumber = new NullableString() {IsNull = false, Value = "422"},
                                StandardDeviation = new NullableDouble() {IsNull = false, Value = 423},
                                Timestamp = new BasicTypes.DateTime() {Ticks = new System.DateTime(2021, 4, 1).Ticks},
                                TestLocation = new DtoTypes.ClassicTestLocation()
                                {
                                    LocationId = 424,
                                    LocationDirectoryId = 425,
                                    TreePath =  new NullableString(){IsNull = false, Value = "426"}
                                },
                                TestValues = new ListOfClassicMfuTestValue()
                                {
                                    ClassicMfuTestValues =
                                    {
                                        new DtoTypes.ClassicMfuTestValue()
                                        {
                                            Id = 427,
                                            Position = 428,
                                            ValueUnit1 = 429,
                                            ValueUnit2 = 430
                                        },
                                        new DtoTypes.ClassicMfuTestValue()
                                        {
                                            Id = 431,
                                            Position = 432,
                                            ValueUnit1 = 433,
                                            ValueUnit2 = 434
                                        }
                                    }
                                }
                            },
                            LocalTimestamp = new DateTime() { Ticks = 365789421 }
                        }
                    }
                }
            };

        [TestCaseSource(nameof(InsertClassicMfuTestsCallsUseCaseData))]
        public void InsertClassicMfuTestsCallsUseCase(ListOfClassicMfuTestWithLocalTimestamp data)
        {
            var useCase = new TransferToTestEquipmentUseCaseMock();
            var service = new NetworkView.Services.TransferToTestEquipmentService(null, useCase);

            service.InsertClassicMfuTests(data, null);

            var comparer = new Func<DtoTypes.ClassicMfuTestWithLocalTimestamp, KeyValuePair<ClassicMfuTest, System.DateTime>, bool>((dto, entity) =>
                EqualityChecker.CompareClassicMfuTestWithDto(entity.Key, dto.ClassicMfuTest) && EqualityChecker.ArePrimitiveDateTimeAndDtoEqual(entity.Value, dto.LocalTimestamp)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.ClassicMfuTests, useCase.InsertClassicMfuTestsParameter.ToList(), comparer);
        }
    }
}
