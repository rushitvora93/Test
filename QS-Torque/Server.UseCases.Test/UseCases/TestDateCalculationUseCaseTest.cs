using System;
using System.Collections.Generic;
using NUnit.Framework;
using Server.Core.Entities;
using Server.Core.Enums;
using Server.UseCases.UseCases;

namespace Server.UseCases.Test.UseCases
{
    class TestDateCalculatorMock : ITestDateCalculator
    {
        public int CalculateTestDateCallCount { get; set; } = 0;
        public TestLevel CalculateTestDateParameterIntervalInfo1 { get; set; }
        public DateTime? CalculateTestDateParameterEndOfLastTestPeriod1 { get; set; }
        public Shift? CalculateTestDateParameterEndOfLastTestPeriodShift1 { get; set; }
        public DateTime CalculateTestDateParameterStartDate1 { get; set; }
        public Func<DateTime, Shift?, DateTime, Shift?, List<(DateTime, Shift?)>> CalculateTestDateGetClassicTestDatesForTimePeriod1 { get; set; }
        public (DateTime resultDate, Shift? resultShift, DateTime newEndOfLastTestPeriod, Shift? newEndOfLastTestPeriodShift) CalculateTestDateReturnValue1 { get; set; }
        
        public TestLevel CalculateTestDateParameterIntervalInfo2 { get; set; }
        public DateTime? CalculateTestDateParameterEndOfLastTestPeriod2 { get; set; }
        public Shift? CalculateTestDateParameterEndOfLastTestPeriodShift2 { get; set; }
        public DateTime CalculateTestDateParameterStartDate2 { get; set; }
        public Func<DateTime, Shift?, DateTime, Shift?, List<(DateTime, Shift?)>> CalculateTestDateGetClassicTestDatesForTimePeriod2 { get; set; }
        public (DateTime resultDate, Shift? resultShift, DateTime newEndOfLastTestPeriod, Shift? newEndOfLastTestPeriodShift) CalculateTestDateReturnValue2 { get; set; }

        public (DateTime resultDate, Shift? resultShift, DateTime? newEndOfLastTestPeriod, Shift? newEndOfLastTestPeriodShift)
            CalculateTestDate(TestLevel intervalInfo, DateTime? endOfLastTestPeriod, Shift? endOfLastTestPeriodShift,
                DateTime startDate, Func<DateTime, Shift?, DateTime, Shift?, List<(DateTime, Shift?)>> getClassicTestDatesForTimePeriod)
        {
            if(CalculateTestDateCallCount == 0)
            {
                CalculateTestDateParameterIntervalInfo1 = intervalInfo;
                CalculateTestDateParameterEndOfLastTestPeriod1 = endOfLastTestPeriod;
                CalculateTestDateParameterEndOfLastTestPeriodShift1 = endOfLastTestPeriodShift;
                CalculateTestDateParameterStartDate1 = startDate;
                CalculateTestDateGetClassicTestDatesForTimePeriod1 = getClassicTestDatesForTimePeriod;
                CalculateTestDateCallCount++;
                return CalculateTestDateReturnValue1;
            }
            else
            {
                CalculateTestDateParameterIntervalInfo2 = intervalInfo;
                CalculateTestDateParameterEndOfLastTestPeriod2 = endOfLastTestPeriod;
                CalculateTestDateParameterEndOfLastTestPeriodShift2 = endOfLastTestPeriodShift;
                CalculateTestDateParameterStartDate2 = startDate;
                CalculateTestDateGetClassicTestDatesForTimePeriod2 = getClassicTestDatesForTimePeriod;
                CalculateTestDateCallCount++;
                return CalculateTestDateReturnValue2;
            }
        }
    }


    public class TestDateCalculationUseCaseTest
    {
        [Test]
        public void CreateTestDateCalculatorCallsData()
        {
            var tuple = CreateUseCaseTuple();
            var id = new WorkingCalendarId(64);
            tuple.workingCalendarData.GetWorkingCalendarReturnValue = new WorkingCalendar() { Id = id };
            tuple.workingCalendarData.GetWorkingCalendarEntriesReturnValue = new List<WorkingCalendarEntry>();
            tuple.useCase.CreateTestDateCalculator(new ShiftManagement()
            {
                IsSecondShiftActive = true,
                IsThirdShiftActive = true
            });
            
            Assert.IsTrue(tuple.workingCalendarData.GetWorkingCalendarCalled);
            Assert.IsTrue(tuple.workingCalendarData.GetWorkingCalendarEntriesForWorkingCalendarIdCalled);
            Assert.AreSame(id, tuple.workingCalendarData.GetWorkingCalendarEntriesForWorkingCalendarIdParameter);
        }
        
        [Test]
        public void CalculateToolTestDateForCallsShiftManagementData()
        {
            var tuple = CreateUseCaseTuple();
            tuple.shiftManagementData.GetShiftManagementReturnValue = new ShiftManagement();
            tuple.workingCalendarData.GetWorkingCalendarReturnValue = new WorkingCalendar() { Id = new WorkingCalendarId(0) };
            tuple.useCase.CalculateToolTestDateFor(new List<LocationToolAssignmentId>());
            Assert.IsTrue(tuple.shiftManagementData.GetShiftManagementCalled);
        }

        [Test]
        public void CalculateToolTestDateForCallsCommit()
        {
            var tuple = CreateUseCaseTuple();
            tuple.shiftManagementData.GetShiftManagementReturnValue = new ShiftManagement();
            tuple.useCase.CalculateToolTestDateFor(new List<LocationToolAssignmentId>(), null, null);
            Assert.IsTrue(tuple.locationToolAssignmentData.CommitCalled);
        }
        
        [TestCase(2)]
        [TestCase(5)]
        public void CalculateToolTestDateForCallsLocationToolAssignmentData(long idValue)
        {
            var tuple = CreateUseCaseTuple();
            var id = new LocationToolAssignmentId(idValue);
            tuple.useCase.CalculateToolTestDateFor(id, null, null);
            Assert.AreSame(id, tuple.locationToolAssignmentData.GetLocationToolAssignmentByIdParameter);
        }

        [Test]
        public void NoToolTestDateCalculationIfTestOperationIsNotActive()
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            tuple.locationToolAssignmentData.GetLocationToolAssignmentByIdReturnValue = new LocationToolAssignment()
            {
                TestLevelSetMfu = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveMfu = false,
                TestLevelSetChk = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveChk = false
            };
            tuple.useCase.CalculateToolTestDateFor(new LocationToolAssignmentId(0), calculator, new ShiftManagement());
            Assert.AreEqual(0, calculator.CalculateTestDateCallCount);
        }

        [TestCase(2, 2)]
        [TestCase(1, 1)]
        [TestCase(3, 3)]
        [TestCase(10, 1)]
        [TestCase(4, 1)]
        public void CalculateToolTestDateForCallsCalculatorWithCorrectTestLevelMfu(int testLevelNumber, long expectedId)
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            tuple.locationToolAssignmentData.GetLocationToolAssignmentByIdReturnValue = new LocationToolAssignment()
            {
                TestLevelNumberMfu = testLevelNumber,
                TestLevelSetMfu = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveMfu = true,
                TestLevelSetChk = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveChk = true
            };
            tuple.useCase.CalculateToolTestDateFor(new LocationToolAssignmentId(0), calculator, new ShiftManagement());
            Assert.AreEqual(expectedId, calculator.CalculateTestDateParameterIntervalInfo1.Id.ToLong());
        }

        [Test]
        public void CalculateToolTestDateForDoesNotCallCalculatorIfLocationToolAssignmentDataReturnsNull()
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            tuple.useCase.CalculateToolTestDateFor(new LocationToolAssignmentId(0), calculator, null);
            Assert.AreEqual(0, calculator.CalculateTestDateCallCount);
        }

        [TestCase("2020-12-21", Shift.FirstShiftOfDay, "2019-12-21")]
        [TestCase("2020-12-22", Shift.SecondShiftOfDay, "2019-12-22")]
        [TestCase("2020-12-23", Shift.ThirdShiftOfDay, "2019-12-23")]
        [TestCase(null, null, "2019-12-24")]
        public void CalculateToolTestDateForCallsCalculatorWithCorrectParameterMfu(DateTime? endOfLastTestPeriod, Shift? endOfLastTestPeriodShift, DateTime startDate)
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            tuple.locationToolAssignmentData.GetLocationToolAssignmentByIdReturnValue = new LocationToolAssignment()
            {
                EndOfLastTestPeriodMfu = endOfLastTestPeriod,
                EndOfLastTestPeriodShiftMfu = endOfLastTestPeriodShift,
                StartDateMfu = startDate,
                StartDateChk = startDate,
                TestLevelSetMfu = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveMfu = true,
                TestLevelSetChk = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveChk = true
            };
            tuple.useCase.CalculateToolTestDateFor(new LocationToolAssignmentId(0), calculator, new ShiftManagement());
            Assert.AreEqual(endOfLastTestPeriod, calculator.CalculateTestDateParameterEndOfLastTestPeriod1);
            Assert.AreEqual(endOfLastTestPeriodShift, calculator.CalculateTestDateParameterEndOfLastTestPeriodShift1);
            Assert.AreEqual(startDate, calculator.CalculateTestDateParameterStartDate1);
        }

        [TestCase(1, "2020-12-21", Shift.FirstShiftOfDay, "2019-12-21", Shift.SecondShiftOfDay, 15)]
        [TestCase(2, "2020-12-22", Shift.SecondShiftOfDay, "2019-12-22", null, 23)]
        [TestCase(3, "2020-12-23", Shift.ThirdShiftOfDay, "2019-12-23", Shift.ThirdShiftOfDay, 3)]
        [TestCase(56, "2020-12-24", null, "2019-12-24", Shift.FirstShiftOfDay, 5)]
        public void GetClassicTestDatesForTimePeriodCallsClassicMfuTestDataWithCorrectParameter(long idValue, DateTime startPeriodDate, Shift? startPeriodShift, DateTime endPeriodDate, Shift? endPeriodShift, int changeOfDayHours)
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            var id = new LocationToolAssignmentId(idValue);
            tuple.locationToolAssignmentData.GetLocationToolAssignmentByIdReturnValue = new LocationToolAssignment()
            {
                TestLevelSetMfu = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveMfu = true,
                TestLevelSetChk = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveChk = true
            };
            tuple.useCase.CalculateToolTestDateFor(id, calculator, new ShiftManagement()
            {
                ChangeOfDay = TimeSpan.FromHours(changeOfDayHours)
            });
            calculator.CalculateTestDateGetClassicTestDatesForTimePeriod1(startPeriodDate, startPeriodShift, endPeriodDate, endPeriodShift);
            
            Assert.AreSame(id, tuple.classicMfuTestData.GetTestsForTimePeriodParameterLocToolId);
            Assert.AreEqual(startPeriodDate, tuple.classicMfuTestData.GetTestsForTimePeriodParameterStartPeriodDate);
            Assert.AreEqual(startPeriodShift, tuple.classicMfuTestData.GetTestsForTimePeriodParameterStartPeriodShift);
            Assert.AreEqual(endPeriodDate, tuple.classicMfuTestData.GetTestsForTimePeriodParameterEndPeriodDate);
            Assert.AreEqual(endPeriodShift, tuple.classicMfuTestData.GetTestsForTimePeriodParameterEndPeriodShift);
            Assert.AreEqual(TimeSpan.FromHours(changeOfDayHours), tuple.classicMfuTestData.GetTestsForTimePeriodParameterChangeOfDay);
        }

        [TestCase(2, 2)]
        [TestCase(1, 1)]
        [TestCase(3, 3)]
        [TestCase(10, 1)]
        [TestCase(4, 1)]
        public void CalculateToolTestDateForCallsCalculatorWithCorrectTestLevelChk(int testLevelNumber, long expectedId)
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            tuple.locationToolAssignmentData.GetLocationToolAssignmentByIdReturnValue = new LocationToolAssignment()
            {
                TestLevelNumberMfu = -1,
                TestLevelNumberChk = testLevelNumber,
                TestLevelSetMfu = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveMfu = true,
                TestLevelSetChk = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveChk = true
            };
            tuple.useCase.CalculateToolTestDateFor(new LocationToolAssignmentId(0), calculator, new ShiftManagement());
            Assert.AreEqual(expectedId, calculator.CalculateTestDateParameterIntervalInfo2.Id.ToLong());
        }

        [TestCase("2020-12-21", Shift.FirstShiftOfDay, "2019-12-21")]
        [TestCase("2020-12-22", Shift.SecondShiftOfDay, "2019-12-22")]
        [TestCase("2020-12-23", Shift.ThirdShiftOfDay, "2019-12-23")]
        [TestCase(null, null, "2019-12-24")]
        public void CalculateToolTestDateForCallsCalculatorWithCorrectParameterChk(DateTime? endOfLastTestPeriod, Shift? endOfLastTestPeriodShift, DateTime startDate)
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            tuple.locationToolAssignmentData.GetLocationToolAssignmentByIdReturnValue = new LocationToolAssignment()
            {
                EndOfLastTestPeriodChk = endOfLastTestPeriod,
                EndOfLastTestPeriodShiftChk = endOfLastTestPeriodShift,
                StartDateMfu = startDate,
                StartDateChk = startDate,
                TestLevelSetMfu = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveMfu = true,
                TestLevelSetChk = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveChk = true
            };
            tuple.useCase.CalculateToolTestDateFor(new LocationToolAssignmentId(0), calculator, new ShiftManagement());
            Assert.AreEqual(endOfLastTestPeriod, calculator.CalculateTestDateParameterEndOfLastTestPeriod2);
            Assert.AreEqual(endOfLastTestPeriodShift, calculator.CalculateTestDateParameterEndOfLastTestPeriodShift2);
            Assert.AreEqual(startDate, calculator.CalculateTestDateParameterStartDate2);
        }

        [TestCase(1, "2020-12-21", Shift.FirstShiftOfDay, "2019-12-21", Shift.SecondShiftOfDay, 15)]
        [TestCase(2, "2020-12-22", Shift.SecondShiftOfDay, "2019-12-22", null, 23)]
        [TestCase(3, "2020-12-23", Shift.ThirdShiftOfDay, "2019-12-23", Shift.ThirdShiftOfDay, 3)]
        [TestCase(56, "2020-12-24", null, "2019-12-24", Shift.FirstShiftOfDay, 5)]
        public void GetClassicTestDatesForTimePeriodCallsClassicChkTestDataWithCorrectParameter(long idValue, DateTime startPeriodDate, Shift? startPeriodShift, DateTime endPeriodDate, Shift? endPeriodShift, int changeOfDayHours)
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            var id = new LocationToolAssignmentId(idValue);
            tuple.locationToolAssignmentData.GetLocationToolAssignmentByIdReturnValue = new LocationToolAssignment()
            {
                TestLevelSetMfu = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveMfu = true,
                TestLevelSetChk = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveChk = true
            };
            tuple.useCase.CalculateToolTestDateFor(id, calculator, new ShiftManagement()
            {
                ChangeOfDay = TimeSpan.FromHours(changeOfDayHours)
            });
            calculator.CalculateTestDateGetClassicTestDatesForTimePeriod2(startPeriodDate, startPeriodShift, endPeriodDate, endPeriodShift);

            Assert.AreSame(id, tuple.classicChkTestData.GetTestsForTimePeriodParameterLocToolId);
            Assert.AreEqual(startPeriodDate, tuple.classicChkTestData.GetTestsForTimePeriodParameterStartPeriodDate);
            Assert.AreEqual(startPeriodShift, tuple.classicChkTestData.GetTestsForTimePeriodParameterStartPeriodShift);
            Assert.AreEqual(endPeriodDate, tuple.classicChkTestData.GetTestsForTimePeriodParameterEndPeriodDate);
            Assert.AreEqual(endPeriodShift, tuple.classicChkTestData.GetTestsForTimePeriodParameterEndPeriodShift);
            Assert.AreEqual(TimeSpan.FromHours(changeOfDayHours), tuple.classicChkTestData.GetTestsForTimePeriodParameterChangeOfDay);
        }

        [TestCase(1, "2020-12-21", Shift.FirstShiftOfDay, "2019-12-21", Shift.SecondShiftOfDay, "2020-12-23", Shift.ThirdShiftOfDay, "2019-12-23", Shift.ThirdShiftOfDay)]
        [TestCase(10, "2020-12-23", null, "2019-12-21", null, "2020-11-23", null, "2018-12-23", null)]
        [TestCase(111, "2020-12-22", Shift.SecondShiftOfDay, "2019-12-21", Shift.SecondShiftOfDay, "2020-10-23", Shift.FirstShiftOfDay, "2017-12-23", Shift.SecondShiftOfDay)]
        public void CalculateToolTestDateForCallsLocationToolAssignmentDataSaveNextTestDatesForWithCorrectParameter(long idValue, DateTime nextTestDateMfu, Shift? nextTestShiftMfu, DateTime nextTestDateChk, Shift? nextTestShiftChk,
            DateTime endOfLastTestPeriodMfu, Shift? endOfLastTestPeriodShiftMfu, DateTime endOfLastTestPeriodChk, Shift? endOfLastTestPeriodShiftChk)
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            var id = new LocationToolAssignmentId(idValue);
            tuple.locationToolAssignmentData.GetLocationToolAssignmentByIdReturnValue = new LocationToolAssignment()
            {
                TestLevelSetMfu = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveMfu = true,
                TestLevelSetChk = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveChk = true
            };

            calculator.CalculateTestDateReturnValue1 = (nextTestDateMfu, nextTestShiftMfu, endOfLastTestPeriodMfu, endOfLastTestPeriodShiftMfu);
            calculator.CalculateTestDateReturnValue2 = (nextTestDateChk, nextTestShiftChk, endOfLastTestPeriodChk, endOfLastTestPeriodShiftChk);
            tuple.useCase.CalculateToolTestDateFor(id, calculator, new ShiftManagement());
            
            Assert.AreSame(id, tuple.locationToolAssignmentData.SaveNextTestDatesForParamId);
            Assert.AreEqual(nextTestDateMfu, tuple.locationToolAssignmentData.SaveNextTestDatesForParamNextTestDateMfu);
            Assert.AreEqual(nextTestShiftMfu, tuple.locationToolAssignmentData.SaveNextTestDatesForParamNextTestShiftMfu);
            Assert.AreEqual(endOfLastTestPeriodMfu, tuple.locationToolAssignmentData.SaveNextTestDatesForParamEndOfLastTestPeriodMfu);
            Assert.AreEqual(endOfLastTestPeriodShiftMfu, tuple.locationToolAssignmentData.SaveNextTestDatesForParamEndOfLastTestPeriodShiftMfu);
            Assert.AreEqual(nextTestDateChk, tuple.locationToolAssignmentData.SaveNextTestDatesForParamNextTestDateChk);
            Assert.AreEqual(nextTestShiftChk, tuple.locationToolAssignmentData.SaveNextTestDatesForParamNextTestShiftChk);
            Assert.AreEqual(endOfLastTestPeriodChk, tuple.locationToolAssignmentData.SaveNextTestDatesForParamEndOfLastTestPeriodChk);
            Assert.AreEqual(endOfLastTestPeriodShiftChk, tuple.locationToolAssignmentData.SaveNextTestDatesForParamEndOfLastTestPeriodShiftChk);
        }

        [TestCase(5)]
        [TestCase(7)]
        public void CalculateToolTestDateForTestLevelSetCallsLocationToolAssignmentData(long idValue)
        {
            var tuple = CreateUseCaseTuple();
            var id = new TestLevelSetId(idValue);
            tuple.locationToolAssignmentData.GetLocationToolAssignmentIdsForTestLevelSetReturnValue = new List<LocationToolAssignmentId>();
            tuple.workingCalendarData.GetWorkingCalendarReturnValue = new WorkingCalendar() { Id = new WorkingCalendarId(0) };
            tuple.shiftManagementData.GetShiftManagementReturnValue = new ShiftManagement();
            
            tuple.useCase.CalculateToolTestDateForTestLevelSet(id);
            Assert.AreSame(id, tuple.locationToolAssignmentData.GetLocationToolAssignmentIdsForTestLevelSetParameter);
        }
        
        [Test]
        public void CalculateToolTestDateForCallsCalculatorNotForMfuIfThereIsNoTestLevelSetAssigned()
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            tuple.locationToolAssignmentData.GetLocationToolAssignmentByIdReturnValue = new LocationToolAssignment()
            {
                TestLevelSetChk = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveChk = true
            };
            tuple.useCase.CalculateToolTestDateFor(new LocationToolAssignmentId(0), calculator, new ShiftManagement());
            Assert.AreEqual(1, calculator.CalculateTestDateCallCount);
            Assert.IsNull(tuple.locationToolAssignmentData.SaveNextTestDatesForParamNextTestDateMfu);
            Assert.IsNull(tuple.locationToolAssignmentData.SaveNextTestDatesForParamNextTestShiftMfu);
            Assert.IsNull(tuple.locationToolAssignmentData.SaveNextTestDatesForParamEndOfLastTestPeriodMfu);
            Assert.IsNull(tuple.locationToolAssignmentData.SaveNextTestDatesForParamEndOfLastTestPeriodShiftMfu);
        }

        [Test]
        public void CalculateToolTestDateForCallsCalculatorNotForChkIfThereIsNoTestLevelSetAssigned()
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            tuple.locationToolAssignmentData.GetLocationToolAssignmentByIdReturnValue = new LocationToolAssignment()
            {
                TestLevelSetMfu = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActiveMfu = true
            };
            tuple.useCase.CalculateToolTestDateFor(new LocationToolAssignmentId(0), calculator, new ShiftManagement());
            Assert.AreEqual(1, calculator.CalculateTestDateCallCount);
            Assert.IsNull(tuple.locationToolAssignmentData.SaveNextTestDatesForParamNextTestDateChk);
            Assert.IsNull(tuple.locationToolAssignmentData.SaveNextTestDatesForParamNextTestShiftChk);
            Assert.IsNull(tuple.locationToolAssignmentData.SaveNextTestDatesForParamEndOfLastTestPeriodChk);
            Assert.IsNull(tuple.locationToolAssignmentData.SaveNextTestDatesForParamEndOfLastTestPeriodShiftChk);
        }

        [Test]
        public void CalculateToolTestDateForCallsCalculatorNotForMfuAndChkIfThereAreNoTestLevelSetsAssigned()
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            tuple.locationToolAssignmentData.GetLocationToolAssignmentByIdReturnValue = new LocationToolAssignment();
            tuple.useCase.CalculateToolTestDateFor(new LocationToolAssignmentId(0), calculator, new ShiftManagement());
            
            Assert.AreEqual(0, calculator.CalculateTestDateCallCount);
            Assert.IsNull(tuple.locationToolAssignmentData.SaveNextTestDatesForParamNextTestDateMfu);
            Assert.IsNull(tuple.locationToolAssignmentData.SaveNextTestDatesForParamNextTestShiftMfu);
            Assert.IsNull(tuple.locationToolAssignmentData.SaveNextTestDatesForParamEndOfLastTestPeriodMfu);
            Assert.IsNull(tuple.locationToolAssignmentData.SaveNextTestDatesForParamEndOfLastTestPeriodShiftMfu);
            Assert.IsNull(tuple.locationToolAssignmentData.SaveNextTestDatesForParamNextTestDateChk);
            Assert.IsNull(tuple.locationToolAssignmentData.SaveNextTestDatesForParamNextTestShiftChk);
            Assert.IsNull(tuple.locationToolAssignmentData.SaveNextTestDatesForParamEndOfLastTestPeriodChk);
            Assert.IsNull(tuple.locationToolAssignmentData.SaveNextTestDatesForParamEndOfLastTestPeriodShiftChk);
        }

        [Test]
        public void CalculateProcessControlDateForCallsShiftManagementData()
        {
            var tuple = CreateUseCaseTuple();
            tuple.shiftManagementData.GetShiftManagementReturnValue = new ShiftManagement();
            tuple.workingCalendarData.GetWorkingCalendarReturnValue = new WorkingCalendar() { Id = new WorkingCalendarId(0) };
            tuple.useCase.CalculateProcessControlDateFor(new List<ProcessControlConditionId>());
            Assert.IsTrue(tuple.shiftManagementData.GetShiftManagementCalled);
        }

        [Test]
        public void CalculateProcessControlDateForCallsCommit()
        {
            var tuple = CreateUseCaseTuple();
            tuple.shiftManagementData.GetShiftManagementReturnValue = new ShiftManagement();
            tuple.useCase.CalculateProcessControlDateFor(new List<ProcessControlConditionId>(), null, null);
            Assert.IsTrue(tuple.processControlData.CommitExecuted);
        }

        [TestCase(2)]
        [TestCase(5)]
        public void CalculateProcessControlDateForCallsLocationToolAssignmentData(long idValue)
        {
            var tuple = CreateUseCaseTuple();
            var id = new ProcessControlConditionId(idValue);
            tuple.useCase.CalculateProcessControlDateFor(id, null, null);
            Assert.AreSame(id, tuple.processControlData.GetProcessControlConditionByIdParameter);
        }

        [Test]
        public void NoProcessControlDateCalculationIfTestOperationIsNotActive()
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            tuple.processControlData.GetProcessControlConditionByIdReturnValue = new ProcessControlCondition()
            {
                TestLevelSet = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActive = false
            };
            tuple.useCase.CalculateProcessControlDateFor(new ProcessControlConditionId(0), calculator, new ShiftManagement());
            Assert.AreEqual(0, calculator.CalculateTestDateCallCount);
        }

        [TestCase(2, 2)]
        [TestCase(1, 1)]
        [TestCase(3, 3)]
        [TestCase(10, 1)]
        [TestCase(4, 1)]
        public void CalculateProcessControlDateForCallsCalculatorWithCorrectTestLevel(int testLevelNumber, long expectedId)
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            tuple.processControlData.GetProcessControlConditionByIdReturnValue = new ProcessControlCondition()
            {
                TestLevelNumber = testLevelNumber,
                TestLevelSet = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActive = true
            };
            tuple.useCase.CalculateProcessControlDateFor(new ProcessControlConditionId(0), calculator, new ShiftManagement());
            Assert.AreEqual(expectedId, calculator.CalculateTestDateParameterIntervalInfo1.Id.ToLong());
        }

        [Test]
        public void CalculateProcessControlDateForDoesNotCallCalculatorIfLocationToolAssignmentDataReturnsNull()
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            tuple.useCase.CalculateToolTestDateFor(new LocationToolAssignmentId(0), calculator, null);
            Assert.AreEqual(0, calculator.CalculateTestDateCallCount);
        }

        [TestCase("2020-12-21", Shift.FirstShiftOfDay, "2019-12-21")]
        [TestCase("2020-12-22", Shift.SecondShiftOfDay, "2019-12-22")]
        [TestCase("2020-12-23", Shift.ThirdShiftOfDay, "2019-12-23")]
        [TestCase(null, null, "2019-12-24")]
        public void CalculateProcessControlDateForCallsCalculatorWithCorrectParameter(DateTime? endOfLastTestPeriod, Shift? endOfLastTestPeriodShift, DateTime startDate)
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            tuple.processControlData.GetProcessControlConditionByIdReturnValue = new ProcessControlCondition()
            {
                EndOfLastTestPeriod = endOfLastTestPeriod,
                EndOfLastTestPeriodShift = endOfLastTestPeriodShift,
                StartDate = startDate,
                TestLevelSet = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActive = true
            };
            tuple.useCase.CalculateProcessControlDateFor(new ProcessControlConditionId(0), calculator, new ShiftManagement());
            Assert.AreEqual(endOfLastTestPeriod, calculator.CalculateTestDateParameterEndOfLastTestPeriod1);
            Assert.AreEqual(endOfLastTestPeriodShift, calculator.CalculateTestDateParameterEndOfLastTestPeriodShift1);
            Assert.AreEqual(startDate, calculator.CalculateTestDateParameterStartDate1);
        }

        [TestCase(1, "2020-12-21", Shift.FirstShiftOfDay, "2019-12-21", Shift.SecondShiftOfDay, 15)]
        [TestCase(2, "2020-12-22", Shift.SecondShiftOfDay, "2019-12-22", null, 23)]
        [TestCase(3, "2020-12-23", Shift.ThirdShiftOfDay, "2019-12-23", Shift.ThirdShiftOfDay, 3)]
        [TestCase(56, "2020-12-24", null, "2019-12-24", Shift.FirstShiftOfDay, 5)]
        public void CalculateProcessControlDateForTimePeriodCallsClassicProcessTestDataWithCorrectParameter(long idValue, DateTime startPeriodDate, Shift? startPeriodShift, DateTime endPeriodDate, Shift? endPeriodShift, int changeOfDayHours)
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            var id = new ProcessControlConditionId(idValue);
            tuple.processControlData.GetProcessControlConditionByIdReturnValue = new ProcessControlCondition()
            {
                TestLevelSet = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActive = true
            };
            tuple.useCase.CalculateProcessControlDateFor(id, calculator, new ShiftManagement()
            {
                ChangeOfDay = TimeSpan.FromHours(changeOfDayHours)
            });
            calculator.CalculateTestDateGetClassicTestDatesForTimePeriod1(startPeriodDate, startPeriodShift, endPeriodDate, endPeriodShift);

            Assert.AreSame(id, tuple.classicProcessTestData.GetTestsForTimePeriodParameterLocToolId);
            Assert.AreEqual(startPeriodDate, tuple.classicProcessTestData.GetTestsForTimePeriodParameterStartPeriodDate);
            Assert.AreEqual(startPeriodShift, tuple.classicProcessTestData.GetTestsForTimePeriodParameterStartPeriodShift);
            Assert.AreEqual(endPeriodDate, tuple.classicProcessTestData.GetTestsForTimePeriodParameterEndPeriodDate);
            Assert.AreEqual(endPeriodShift, tuple.classicProcessTestData.GetTestsForTimePeriodParameterEndPeriodShift);
            Assert.AreEqual(TimeSpan.FromHours(changeOfDayHours), tuple.classicProcessTestData.GetTestsForTimePeriodParameterChangeOfDay);
        }

        [TestCase(1, "2020-12-21", Shift.FirstShiftOfDay, "2020-12-23", Shift.ThirdShiftOfDay)]
        [TestCase(10, "2020-12-23", null, "2020-11-23", null)]
        [TestCase(111, "2020-12-22", Shift.SecondShiftOfDay, "2020-10-23", Shift.FirstShiftOfDay)]
        public void CalculateProcessControlDateForCallsProcessControlDataSaveNextTestDatesForWithCorrectParameter(long idValue, DateTime nextTestDate, Shift? nextTestShift,
            DateTime endOfLastTestPeriod, Shift? endOfLastTestPeriodShift)
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            var id = new ProcessControlConditionId(idValue);
            tuple.processControlData.GetProcessControlConditionByIdReturnValue = new ProcessControlCondition()
            {
                TestLevelSet = new TestLevelSet()
                {
                    TestLevel1 = new TestLevel() { Id = new TestLevelId(1) },
                    TestLevel2 = new TestLevel() { Id = new TestLevelId(2) },
                    TestLevel3 = new TestLevel() { Id = new TestLevelId(3) }
                },
                TestOperationActive = true
            };

            calculator.CalculateTestDateReturnValue1 = (nextTestDate, nextTestShift, endOfLastTestPeriod, endOfLastTestPeriodShift);
            tuple.useCase.CalculateProcessControlDateFor(id, calculator, new ShiftManagement());

            Assert.AreSame(id, tuple.processControlData.SaveNextTestDatesForParamId);
            Assert.AreEqual(nextTestDate, tuple.processControlData.SaveNextTestDatesForParamNextTestDate);
            Assert.AreEqual(nextTestShift, tuple.processControlData.SaveNextTestDatesForParamNextTestShift);
            Assert.AreEqual(endOfLastTestPeriod, tuple.processControlData.SaveNextTestDatesForParamEndOfLastTestPeriod);
            Assert.AreEqual(endOfLastTestPeriodShift, tuple.processControlData.SaveNextTestDatesForParamEndOfLastTestPeriodShift);
        }

        [TestCase(5)]
        [TestCase(7)]
        public void CalculateProcessControlDateForTestLevelSetCallsProcessControlData(long idValue)
        {
            var tuple = CreateUseCaseTuple();
            var id = new TestLevelSetId(idValue);
            tuple.processControlData.GetProcessControlConditionIdsForTestLevelSetReturnValue = new List<ProcessControlConditionId>();
            tuple.workingCalendarData.GetWorkingCalendarReturnValue = new WorkingCalendar() { Id = new WorkingCalendarId(0) };
            tuple.shiftManagementData.GetShiftManagementReturnValue = new ShiftManagement();

            tuple.useCase.CalculateProcessControlDateForTestLevelSet(id);
            Assert.AreSame(id, tuple.processControlData.GetProcessControlConditionIdsForTestLevelSetParameter);
        }

        [Test]
        public void CalculateToolTestDateForCallsCalculatorNotIfThereAreNoTestLevelSetsAssigned()
        {
            var tuple = CreateUseCaseTuple();
            var calculator = new TestDateCalculatorMock();
            tuple.processControlData.GetProcessControlConditionByIdReturnValue = new ProcessControlCondition() { TestOperationActive = true };
            tuple.useCase.CalculateToolTestDateFor(new LocationToolAssignmentId(0), calculator, new ShiftManagement());

            Assert.AreEqual(0, calculator.CalculateTestDateCallCount);
            Assert.IsNull(tuple.processControlData.SaveNextTestDatesForParamNextTestDate);
            Assert.IsNull(tuple.processControlData.SaveNextTestDatesForParamNextTestShift);
            Assert.IsNull(tuple.processControlData.SaveNextTestDatesForParamEndOfLastTestPeriod);
            Assert.IsNull(tuple.processControlData.SaveNextTestDatesForParamEndOfLastTestPeriodShift);
            Assert.IsNull(tuple.processControlData.SaveNextTestDatesForParamNextTestDate);
            Assert.IsNull(tuple.processControlData.SaveNextTestDatesForParamNextTestShift);
            Assert.IsNull(tuple.processControlData.SaveNextTestDatesForParamEndOfLastTestPeriod);
            Assert.IsNull(tuple.processControlData.SaveNextTestDatesForParamEndOfLastTestPeriodShift);
        }



        private static (TestDateCalculationUseCase useCase, 
            ShiftManagementDataMock shiftManagementData, 
            WorkingCalendarDataMock workingCalendarData,
            LocationToolAssignmentDataMock locationToolAssignmentData,
            ProcessControlDataAccessMock processControlData,
            ClassicMfuTestDataMock classicMfuTestData,
            ClassicChkTestDataMock classicChkTestData,
            ClassicProcessTestDataMock classicProcessTestData) CreateUseCaseTuple()
        {
            var shiftManagementData = new ShiftManagementDataMock();
            var workingCalendarData = new WorkingCalendarDataMock();
            var locationToolAssignmentData = new LocationToolAssignmentDataMock();
            var processControlData = new ProcessControlDataAccessMock();
            var classicMfuTestData = new ClassicMfuTestDataMock();
            var classicChkTestData = new ClassicChkTestDataMock();
            var classicProcessTestData = new ClassicProcessTestDataMock();
            var useCase = new TestDateCalculationUseCase(shiftManagementData, workingCalendarData, locationToolAssignmentData, processControlData, classicMfuTestData, classicChkTestData, classicProcessTestData);
            return (useCase, shiftManagementData, workingCalendarData, locationToolAssignmentData, processControlData, classicMfuTestData, classicChkTestData, classicProcessTestData);
        }
    }
}
