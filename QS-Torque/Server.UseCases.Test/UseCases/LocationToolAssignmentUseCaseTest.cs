using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using Server.Core.Enums;
using Server.TestHelper.Factories;
using Server.UseCases.UseCases;

namespace Server.UseCases.Test.UseCases
{
    public class LocationToolAssignmentDataMock : ILocationToolAssignmentData
    {
        public enum LocationToolAssignmentDataFunction
        {
            InsertLocPowsWithHistory,
            InsertCondRotsWithHistory,
            Commit
        }

        public List<LocationToolAssignment> LoadLocationToolAssignmentsReturnValue { get; set; }
        public LocationToolAssignmentId GetLocationToolAssignmentByIdParameter { get; set; }
        public LocationToolAssignment GetLocationToolAssignmentByIdReturnValue { get; set; }
        public List<LocationToolAssignmentId> GetLocationToolAssignmentIdsForTestLevelSetReturnValue { get; set; }
        public TestLevelSetId GetLocationToolAssignmentIdsForTestLevelSetParameter { get; set; }
        public LocationToolAssignmentId SaveNextTestDatesForParamId { get; set; }
        public DateTime? SaveNextTestDatesForParamNextTestDateMfu { get; set; }
        public Shift? SaveNextTestDatesForParamNextTestShiftMfu { get; set; }
        public DateTime? SaveNextTestDatesForParamEndOfLastTestPeriodMfu { get; set; }
        public Shift? SaveNextTestDatesForParamEndOfLastTestPeriodShiftMfu { get; set; }
        public DateTime? SaveNextTestDatesForParamNextTestDateChk { get; set; }
        public Shift? SaveNextTestDatesForParamNextTestShiftChk { get; set; }
        public DateTime? SaveNextTestDatesForParamEndOfLastTestPeriodChk { get; set; }
        public Shift? SaveNextTestDatesForParamEndOfLastTestPeriodShiftChk { get; set; }
        public List<LocationReferenceLink> LoadLocationReferenceLinksForToolReturnValue { get; set; } = new List<LocationReferenceLink>();
        public ToolId LoadLocationReferenceLinksForToolParameter { get; set; }
        public List<ToolUsage> LoadUnusedToolUsagesForLocationReturnValue { get; set; }
        public LocationId LoadUnusedToolUsagesForLocationParameter { get; set; }
        public List<LocationToolAssignment> GetLocationToolAssignmentsByLocationIdReturnValue { get; set; }
        public LocationId GetLocationToolAssignmentsByLocationIdParameter { get; set; }
        public List<LocationToolAssignment> GetLocationToolAssignmentsByIdsReturnValue { get; set; }
        public List<LocationToolAssignmentId> GetLocationToolAssignmentsByIdsParameter { get; set; }
        public DateTime UpdateNextMfuTestDateParameterDateTime { get; set; }
        public long UpdateNextMfuTestDateParameterCondRotId { get; set; }
        public DateTime UpdateNextChkTestDateParameterDateTime { get; set; }
        public long UpdateNextChkTestDateParameterCondRotId { get; set; }
        public bool CommitCalled { get; set; }
        public List<LocationToolAssignmentDiff> InsertLocPowsWithHistoryParameterDiffs { get; set; }
        public bool InsertLocPowsWithHistoryReturnList { get; set; }
        public List<LocationToolAssignment> InsertLocPowsWithHistoryReturnValue { get; set; } = new List<LocationToolAssignment>();
        public List<LocationToolAssignmentDiff> InsertCondRotsWithHistoryParameterDiffs { get; set; }
        public bool InsertCondRotsWithHistoryParameterReturnList { get; set; }
        public List<LocationToolAssignment> InsertCondRotsWithHistoryReturnValue { get; set; }
        public List<LocationToolAssignmentDiff> UpdateLocPowsWithHistoryParameterDiffs { get; set; }
        public List<LocationToolAssignment> UpdateLocPowsWithHistoryReturnValue { get; set; }
        public List<LocationToolAssignment> UpdateCondRotsWithHistoryReturnValue { get; set; }
        public List<LocationToolAssignmentDiff> UpdateCondRotsWithHistoryParameterDiffs { get; set; }
        public List<LocationToolAssignmentDataFunction> CalledFunctions { get; set; } = new List<LocationToolAssignmentDataFunction>();

        public void Commit()
        {
            CalledFunctions.Add(LocationToolAssignmentDataFunction.Commit);
            CommitCalled = true;
        }

        public List<LocationToolAssignment> LoadLocationToolAssignments()
        {
            return LoadLocationToolAssignmentsReturnValue;
        }

        public LocationToolAssignment GetLocationToolAssignmentById(LocationToolAssignmentId id)
        {
            GetLocationToolAssignmentByIdParameter = id;
            return GetLocationToolAssignmentByIdReturnValue;
        }

        public List<LocationToolAssignmentId> GetLocationToolAssignmentIdsForTestLevelSet(TestLevelSetId id)
        {
            GetLocationToolAssignmentIdsForTestLevelSetParameter = id;
            return GetLocationToolAssignmentIdsForTestLevelSetReturnValue;
        }

        public void SaveNextTestDatesFor(LocationToolAssignmentId id, DateTime? nextTestDateMfu, Shift? nextTestShiftMfu,
            DateTime? nextTestDateChk, Shift? nextTestShiftChk,
            DateTime? endOfLastTestPeriodMfu, Shift? endOfLastTestPeriodShiftMfu, DateTime? endOfLastTestPeriodChk, Shift? endOfLastTestPeriodShiftChk)
        {
            SaveNextTestDatesForParamId = id;
            SaveNextTestDatesForParamNextTestDateMfu = nextTestDateMfu;
            SaveNextTestDatesForParamNextTestShiftMfu = nextTestShiftMfu;
            SaveNextTestDatesForParamEndOfLastTestPeriodMfu = endOfLastTestPeriodMfu;
            SaveNextTestDatesForParamEndOfLastTestPeriodShiftMfu = endOfLastTestPeriodShiftMfu;
            SaveNextTestDatesForParamNextTestDateChk = nextTestDateChk;
            SaveNextTestDatesForParamNextTestShiftChk = nextTestShiftChk;
            SaveNextTestDatesForParamEndOfLastTestPeriodChk = endOfLastTestPeriodChk;
            SaveNextTestDatesForParamEndOfLastTestPeriodShiftChk = endOfLastTestPeriodShiftChk;
        }

        public List<LocationReferenceLink> LoadLocationReferenceLinksForTool(ToolId toolId)
        {
            LoadLocationReferenceLinksForToolParameter = toolId;
            return LoadLocationReferenceLinksForToolReturnValue;
        }

        public List<ToolUsage> LoadUnusedToolUsagesForLocation(LocationId locationId)
        {
            LoadUnusedToolUsagesForLocationParameter = locationId;
            return LoadUnusedToolUsagesForLocationReturnValue;
        }

        public List<LocationToolAssignment> GetLocationToolAssignmentsByLocationId(LocationId locationId)
        {
            GetLocationToolAssignmentsByLocationIdParameter = locationId;
            return GetLocationToolAssignmentsByLocationIdReturnValue;
        }

        public List<LocationToolAssignment> GetLocationToolAssignmentsByIds(List<LocationToolAssignmentId> ids)
        {
            GetLocationToolAssignmentsByIdsParameter = ids;
            return GetLocationToolAssignmentsByIdsReturnValue;
        }

        public void UpdateNextChkTestDate(long condRotId, DateTime dateTime)
        {
            UpdateNextChkTestDateParameterCondRotId = condRotId;
            UpdateNextChkTestDateParameterDateTime = dateTime;
        }

        public void UpdateNextMfuTestDate(long condRotId, DateTime dateTime)
        {
            UpdateNextMfuTestDateParameterCondRotId = condRotId;
            UpdateNextMfuTestDateParameterDateTime = dateTime;
        }

        public List<LocationToolAssignment> InsertLocPowsWithHistory(List<LocationToolAssignmentDiff> locationToolAssignmentDiffs, bool returnList)
        {
            CalledFunctions.Add(LocationToolAssignmentDataFunction.InsertLocPowsWithHistory);
            InsertLocPowsWithHistoryParameterDiffs = locationToolAssignmentDiffs;
            InsertLocPowsWithHistoryReturnList = returnList;
            return InsertLocPowsWithHistoryReturnValue;
        }

        public List<LocationToolAssignment> InsertCondRotsWithHistory(List<LocationToolAssignmentDiff> locationToolAssignmentDiffs, bool returnList)
        {
            CalledFunctions.Add(LocationToolAssignmentDataFunction.InsertCondRotsWithHistory);
            InsertCondRotsWithHistoryParameterDiffs = locationToolAssignmentDiffs;
            InsertCondRotsWithHistoryParameterReturnList = returnList;
            return InsertCondRotsWithHistoryReturnValue;
        }

        public List<LocationToolAssignment> UpdateLocPowsWithHistory(List<LocationToolAssignmentDiff> locationToolAssignmentDiffs)
        {
            UpdateLocPowsWithHistoryParameterDiffs = locationToolAssignmentDiffs;
            return UpdateLocPowsWithHistoryReturnValue;
        }

        public List<LocationToolAssignment> UpdateCondRotsWithHistory(List<LocationToolAssignmentDiff> locationToolAssignmentDiffs)
        {
            UpdateCondRotsWithHistoryParameterDiffs = locationToolAssignmentDiffs;
            return UpdateCondRotsWithHistoryReturnValue;
        }
    }

    public class LocationToolAssignmentUseCaseTest
    {
        [Test]
        public void LoadLocationToolAssignmentsReturnsValuesFromData()
        {
            var tuple = CreateUseCaseTuple();
            var list = new List<LocationToolAssignment>();
            tuple.data.LoadLocationToolAssignmentsReturnValue = list;
            var result = tuple.useCase.LoadLocationToolAssignments();
            Assert.AreSame(list, result);
        }

        [TestCase(10)]
        [TestCase(20)]
        public void LoadLocationReferenceLinksForToolCallsDataAccess(long toolId)
        {
            var tuple = CreateUseCaseTuple();

            tuple.useCase.LoadLocationReferenceLinksForTool(new ToolId(toolId));

            Assert.AreEqual(toolId, tuple.data.LoadLocationReferenceLinksForToolParameter.ToLong());
        }

        [Test]
        public void LoadLocationReferenceLinksForToolReturnsCorrectValue()
        {
            var tuple = CreateUseCaseTuple();

            var locationReferenceLinks = new List<LocationReferenceLink>();
            tuple.data.LoadLocationReferenceLinksForToolReturnValue = locationReferenceLinks;

            var result = tuple.useCase.LoadLocationReferenceLinksForTool(new ToolId(1));

            Assert.AreSame(locationReferenceLinks, result);
        }

        [TestCase(10)]
        [TestCase(20)]
        public void LoadUnusedToolUsagesForLocationCallsDataAccess(long locationId)
        {
            var tuple = CreateUseCaseTuple();

            tuple.useCase.LoadUnusedToolUsagesForLocation(new LocationId(locationId));

            Assert.AreEqual(locationId, tuple.data.LoadUnusedToolUsagesForLocationParameter.ToLong());
        }

        [Test]
        public void LoadUnusedToolUsagesForLocationReturnsCorrectValue()
        {
            var tuple = CreateUseCaseTuple();

            var toolUsages = new List<ToolUsage>();
            tuple.data.LoadUnusedToolUsagesForLocationReturnValue = toolUsages;

            var result = tuple.useCase.LoadUnusedToolUsagesForLocation(new LocationId(1));

            Assert.AreSame(toolUsages, result);
        }

        [TestCase(10)]
        [TestCase(20)]
        public void GetLocationToolAssignmentsByLocationIdCallsDataAccess(long locationId)
        {
            var tuple = CreateUseCaseTuple();

            tuple.useCase.GetLocationToolAssignmentsByLocationId(new LocationId(locationId));

            Assert.AreEqual(locationId, tuple.data.GetLocationToolAssignmentsByLocationIdParameter.ToLong());
        }

        [Test]
        public void GetLocationToolAssignmentsByLocationIdReturnsCorrectValue()
        {
            var tuple = CreateUseCaseTuple();

            var links = new List<LocationToolAssignment>();
            tuple.data.GetLocationToolAssignmentsByLocationIdReturnValue = links;

            var result = tuple.useCase.GetLocationToolAssignmentsByLocationId(new LocationId(1));

            Assert.AreSame(links, result);
        }

        [Test]
        public void GetLocationToolAssignmentsByIdsCallsDataAccess()
        {
            var tuple = CreateUseCaseTuple();

            var ids = new List<LocationToolAssignmentId>();

            tuple.useCase.GetLocationToolAssignmentsByIds(ids);

            Assert.AreSame(ids, tuple.data.GetLocationToolAssignmentsByIdsParameter);
        }

        [Test]
        public void GetLocationToolAssignmentsByIdsReturnsCorrectValue()
        {
            var tuple = CreateUseCaseTuple();

            var locationToolAssignments = new List<LocationToolAssignment>();
            tuple.data.GetLocationToolAssignmentsByIdsReturnValue = locationToolAssignments;

            var result = tuple.useCase.GetLocationToolAssignmentsByIds(new List<LocationToolAssignmentId>());

            Assert.AreSame(locationToolAssignments, result);
        }

        private static IEnumerable<List<LocationToolAssignmentDiff>> InsertLocationToolAssignmentCallsDataAccessData =
            new List<List<LocationToolAssignmentDiff>>()
            {
                new List<LocationToolAssignmentDiff>()
                {
                    new LocationToolAssignmentDiff(CreateUser.Anonymous(), null, new LocationToolAssignment()
                    {
                        Id = new LocationToolAssignmentId(1),
                        TestParameters = new Core.Entities.TestParameters()
                    }),
                    new LocationToolAssignmentDiff(CreateUser.Anonymous(), null, new LocationToolAssignment()
                    {
                        Id = new LocationToolAssignmentId(15),
                        TestParameters = new Core.Entities.TestParameters()
                    })
                },
                new List<LocationToolAssignmentDiff>()
                {
                    new LocationToolAssignmentDiff(CreateUser.Anonymous(), null, new LocationToolAssignment()
                    {
                        Id = new LocationToolAssignmentId(14),
                        TestParameters = new Core.Entities.TestParameters()
                    }),
                }
            };

        [TestCaseSource(nameof(InsertLocationToolAssignmentCallsDataAccessData))]
        public void InsertLocationToolAssignmentCallsDataAccess(List<LocationToolAssignmentDiff> diffs)
        {
            var tuple = CreateUseCaseTuple();
            
            tuple.useCase.InsertLocationToolAssignment(diffs);

            Assert.AreSame(diffs, tuple.data.InsertLocPowsWithHistoryParameterDiffs);
            Assert.AreEqual(diffs, tuple.data.InsertCondRotsWithHistoryParameterDiffs);
            Assert.IsTrue(tuple.data.InsertLocPowsWithHistoryReturnList);
            Assert.IsFalse(tuple.data.InsertCondRotsWithHistoryParameterReturnList);
        }

        [Test]
        public void InsertLocationToolAssignmentDontCallsDataAccessInsertCondRotWhenTestParametersIsEmpty()
        {
            var tuple = CreateUseCaseTuple();
            var diffs = new List<LocationToolAssignmentDiff>()
            {
                new LocationToolAssignmentDiff(null, null, new LocationToolAssignment()),
            };
            tuple.useCase.InsertLocationToolAssignment(diffs);

            Assert.IsEmpty(tuple.data.InsertCondRotsWithHistoryParameterDiffs);
        }


        private static IEnumerable<List<long>> InsertLocationToolAssignmentReturnsCorrectValueData =
            new List<List<long>>()
            {
                new List<long>() {1, 2, 6, 1},
                new List<long>() {99, 5, 22, 4}
            };

        [TestCaseSource(nameof(InsertLocationToolAssignmentReturnsCorrectValueData))]
        public void InsertLocationToolAssignmentReturnsCorrectValue(List<long> datas)
        {
            var tuple = CreateUseCaseTuple();

            var locationToolAssignments = new List<LocationToolAssignment>();
            foreach (var data in datas)
            {
                locationToolAssignments.Add(new LocationToolAssignment() {Id = new LocationToolAssignmentId(data)});   
            }

            tuple.data.InsertLocPowsWithHistoryReturnValue = locationToolAssignments;

            var result = tuple.useCase.InsertLocationToolAssignment(new List<LocationToolAssignmentDiff>());

            Assert.AreEqual(locationToolAssignments.Select(x => x.Id.ToLong()).ToList(), result.Select(x => x.ToLong()).ToList());
        }

        [Test]
        public void InsertLocationsWithHistoryCallsCommitAfterWork()
        {
            var tuple = CreateUseCaseTuple();

            tuple.useCase.InsertLocationToolAssignment(new List<LocationToolAssignmentDiff>());

            Assert.AreEqual(LocationToolAssignmentDataMock.LocationToolAssignmentDataFunction.Commit, tuple.data.CalledFunctions.Last());
        }

        [Test]
        public void UpdateLocationToolAssignmentWithEqualDataDontCallDataAccess()
        {
            var tuple = CreateUseCaseTuple();
            var locationToolAssignment = new LocationToolAssignment()
            {
                Id = new LocationToolAssignmentId(1),
                ToolUsage = new ToolUsage() { Id = new ToolUsageId(1), Description = new ToolUsageDescription("")},
                AssignedLocation = CreateLocation.Anonymous(),
                AssignedTool = CreateTool.Parameterized(
                    1, 
                    "", 
                    "", 
                    true,
                    CreateToolModel.Randomized(5676), 
                    "a", 
                    "b", 
                    "c", 
                    "d",
                    new ConfigurableField() { ListId = new HelperTableEntityId(1), Value = new HelperTableEntityValue("f") },
                    new CostCenter() { ListId = new HelperTableEntityId(1), Value = new HelperTableEntityValue("XXX") },
                    new Status() { Id = new StatusId(11), Value = new StatusDescription("V") }),
                TestParameters = new Server.Core.Entities.TestParameters(),
                TestTechnique = new TestTechnique()
            };
            var data = new List<LocationToolAssignmentDiff>()
            {
                new LocationToolAssignmentDiff(null, locationToolAssignment, locationToolAssignment)
            };

            tuple.useCase.UpdateLocationToolAssignment(data);

            Assert.IsEmpty(tuple.data.UpdateLocPowsWithHistoryParameterDiffs);
            Assert.IsEmpty(tuple.data.UpdateCondRotsWithHistoryParameterDiffs);
        }

        private static IEnumerable<List<LocationToolAssignmentDiff>> UpdateLocationToolAssignmentCallsDataAccessData =
            new List<List<LocationToolAssignmentDiff>>()
            {
                new List<LocationToolAssignmentDiff>()
                {
                    new LocationToolAssignmentDiff(CreateUser.Anonymous(),
                    new LocationToolAssignment()
                    {
                        Id = new LocationToolAssignmentId(2),
                        TestParameters = new Core.Entities.TestParameters() {Maximum2 = 4}
                    },
                    new LocationToolAssignment()
                    {
                        Id = new LocationToolAssignmentId(4),
                        TestParameters = new Core.Entities.TestParameters() {Maximum2 = 6}
                    }),
                    new LocationToolAssignmentDiff(CreateUser.Anonymous(),
                        new LocationToolAssignment()
                        {
                            Id = new LocationToolAssignmentId(27),
                            TestParameters = new Core.Entities.TestParameters() {Maximum2 = 7}
                        },
                        new LocationToolAssignment()
                        {
                            Id = new LocationToolAssignmentId(4),
                            TestParameters = new Core.Entities.TestParameters() {Maximum2 = 6}
                        })
                },
                new List<LocationToolAssignmentDiff>()
                {
                    new LocationToolAssignmentDiff(CreateUser.Anonymous(),
                        new LocationToolAssignment()
                        {
                            Id = new LocationToolAssignmentId(27),
                            TestParameters = new Core.Entities.TestParameters() {Maximum2 = 7}
                        },
                        new LocationToolAssignment()
                        {
                            Id = new LocationToolAssignmentId(4),
                            TestParameters = new Core.Entities.TestParameters() {Maximum2 = 6}
                        })
                }
            };

        [TestCaseSource(nameof(UpdateLocationToolAssignmentCallsDataAccessData))]
        public void UpdateLocationToolAssignmentCallsDataAccess(List<LocationToolAssignmentDiff> diffs)
        {
            var tuple = CreateUseCaseTuple();

            tuple.useCase.UpdateLocationToolAssignment(diffs);

            Assert.AreEqual(diffs, tuple.data.UpdateLocPowsWithHistoryParameterDiffs);
            Assert.AreEqual(diffs, tuple.data.UpdateCondRotsWithHistoryParameterDiffs);
        }

        [Test]
        public void AddTestConditionsCallsCommitAfterWork()
        {
            var tuple = CreateUseCaseTuple();

            tuple.useCase.AddTestConditions(new LocationToolAssignment(), new User());

            Assert.AreEqual(LocationToolAssignmentDataMock.LocationToolAssignmentDataFunction.Commit, tuple.data.CalledFunctions.Last());
        }

        [Test]
        public void AddTestConditionsCallsDataAccess()
        {
            var tuple = CreateUseCaseTuple();
            var assignment = new LocationToolAssignment();
            var user = new User();
            tuple.useCase.AddTestConditions(assignment, user);

            Assert.AreSame(assignment,
                tuple.data.InsertCondRotsWithHistoryParameterDiffs.First().GetNewLocationToolAssignment());
            Assert.IsFalse(tuple.data.InsertCondRotsWithHistoryParameterReturnList);
        }

        [TestCase("2021-08-30", IntervalType.XTimesAWeek)]
        [TestCase("2021-09-30", IntervalType.XTimesAShift)]
        [TestCase("2021-10-30", IntervalType.EveryXDays)]
        [TestCase("2021-11-30", IntervalType.EveryXShifts)]
        public void UpdateLocationToolAssignmentAdjustsEndOfLastTestDateOnStartDateMfuChange(DateTime startDateMfu, IntervalType intervalType)
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.UpdateLocationToolAssignment(new List<LocationToolAssignmentDiff>()
            {
                new LocationToolAssignmentDiff(new User(),
                    new LocationToolAssignment() 
                    { 
                        Id = new LocationToolAssignmentId(0),
                        ToolUsage = new ToolUsage(),
                        AssignedLocation = new Location(),
                        AssignedTool = new Tool(),
                        TestLevelSetMfu = new TestLevelSet() { TestLevel1 = new TestLevel() }
                    },
                    new LocationToolAssignment() 
                    {
                        Id = new LocationToolAssignmentId(1),
                        ToolUsage = new ToolUsage(),
                        AssignedLocation = new Location(),
                        AssignedTool = new Tool(),
                        TestLevelSetMfu = new TestLevelSet() { TestLevel1 = new TestLevel() { TestInterval = new Interval() { Type = intervalType } } },
                        StartDateMfu = startDateMfu,
                        TestParameters = new Core.Entities.TestParameters()
                    })
            });

            Assert.AreEqual(startDateMfu, tuple.data.SaveNextTestDatesForParamEndOfLastTestPeriodMfu);
            if(intervalType == IntervalType.EveryXShifts || intervalType == IntervalType.XTimesAShift)
            {
                Assert.AreEqual(Shift.FirstShiftOfDay, tuple.data.SaveNextTestDatesForParamEndOfLastTestPeriodShiftMfu);
            }
            else
            {
                Assert.IsNull(tuple.data.SaveNextTestDatesForParamEndOfLastTestPeriodShiftMfu);
            }
        }

        [TestCase("2021-08-30", IntervalType.XTimesAWeek)]
        [TestCase("2021-09-30", IntervalType.XTimesAShift)]
        [TestCase("2021-10-30", IntervalType.EveryXDays)]
        [TestCase("2021-11-30", IntervalType.EveryXShifts)]
        public void UpdateLocationToolAssignmentAdjustsEndOfLastTestDateOnStartDateChkChange(DateTime startDateChk, IntervalType intervalType)
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.UpdateLocationToolAssignment(new List<LocationToolAssignmentDiff>()
            {
                new LocationToolAssignmentDiff(new User(),
                    new LocationToolAssignment()
                    {
                        Id = new LocationToolAssignmentId(0),
                        ToolUsage = new ToolUsage(),
                        AssignedLocation = new Location(),
                        AssignedTool = new Tool(),
                        TestLevelSetChk = new TestLevelSet() { TestLevel1 = new TestLevel() }
                    },
                    new LocationToolAssignment()
                    {
                        Id = new LocationToolAssignmentId(1),
                        ToolUsage = new ToolUsage(),
                        AssignedLocation = new Location(),
                        AssignedTool = new Tool(),
                        TestLevelSetChk = new TestLevelSet() { TestLevel1 = new TestLevel() { TestInterval = new Interval() { Type = intervalType } } },
                        StartDateChk = startDateChk,
                        TestParameters = new Core.Entities.TestParameters()
                    })
            });

            Assert.AreEqual(startDateChk, tuple.data.SaveNextTestDatesForParamEndOfLastTestPeriodChk);
            if (intervalType == IntervalType.EveryXShifts || intervalType == IntervalType.XTimesAShift)
            {
                Assert.AreEqual(Shift.FirstShiftOfDay, tuple.data.SaveNextTestDatesForParamEndOfLastTestPeriodShiftChk);
            }
            else
            {
                Assert.IsNull(tuple.data.SaveNextTestDatesForParamEndOfLastTestPeriodShiftChk);
            }
        }

        [TestCase("2021-08-30", "2020-08-30", IntervalType.XTimesAWeek)]
        [TestCase("2021-09-30", "2020-09-30", IntervalType.XTimesAShift)]
        [TestCase("2021-10-30", "2020-10-30", IntervalType.EveryXDays)]
        [TestCase("2021-11-30", "2020-11-30", IntervalType.EveryXShifts)]
        public void UpdateLocationToolAssignmentAdjustsEndOfLastTestDateOnStartDateMfuAndChkChange(DateTime startDateMfu, DateTime startDateChk, IntervalType intervalType)
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.UpdateLocationToolAssignment(new List<LocationToolAssignmentDiff>()
            {
                new LocationToolAssignmentDiff(new User(),
                    new LocationToolAssignment()
                    {
                        Id = new LocationToolAssignmentId(0),
                        ToolUsage = new ToolUsage(),
                        AssignedLocation = new Location(),
                        AssignedTool = new Tool(),
                        TestLevelSetMfu = new TestLevelSet() { TestLevel1 = new TestLevel() },
                        TestLevelSetChk = new TestLevelSet() { TestLevel1 = new TestLevel() }
                    },
                    new LocationToolAssignment()
                    {
                        Id = new LocationToolAssignmentId(1),
                        ToolUsage = new ToolUsage(),
                        AssignedLocation = new Location(),
                        AssignedTool = new Tool(),
                        TestLevelSetMfu = new TestLevelSet() { TestLevel1 = new TestLevel() { TestInterval = new Interval() { Type = intervalType } } },
                        StartDateMfu = startDateMfu,
                        TestLevelSetChk = new TestLevelSet() { TestLevel1 = new TestLevel() { TestInterval = new Interval() { Type = intervalType } } },
                        StartDateChk = startDateChk,
                        TestParameters = new Core.Entities.TestParameters()
                    })
            });

            Assert.AreEqual(startDateMfu, tuple.data.SaveNextTestDatesForParamEndOfLastTestPeriodMfu);
            Assert.AreEqual(startDateChk, tuple.data.SaveNextTestDatesForParamEndOfLastTestPeriodChk);
            if (intervalType == IntervalType.EveryXShifts || intervalType == IntervalType.XTimesAShift)
            {
                Assert.AreEqual(Shift.FirstShiftOfDay, tuple.data.SaveNextTestDatesForParamEndOfLastTestPeriodShiftMfu);
                Assert.AreEqual(Shift.FirstShiftOfDay, tuple.data.SaveNextTestDatesForParamEndOfLastTestPeriodShiftChk);
            }
            else
            {
                Assert.IsNull(tuple.data.SaveNextTestDatesForParamEndOfLastTestPeriodShiftMfu);
                Assert.IsNull(tuple.data.SaveNextTestDatesForParamEndOfLastTestPeriodShiftChk);
            }
        }


        private static (LocationToolAssignmentUseCase useCase, LocationToolAssignmentDataMock data) CreateUseCaseTuple()
        {
            var data = new LocationToolAssignmentDataMock();
            var useCase = new LocationToolAssignmentUseCase(data);
            return (useCase, data);
        }
    }
}
