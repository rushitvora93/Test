using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Core.Entities;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using LocationToolAssignmentService;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using Server.Core.Enums;
using Server.TestHelper.Factories;
using Server.UseCases.UseCases;
using TestHelper.Checker;
using DateTime = System.DateTime;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    class LocationToolAssignmentUseCaseMock : ILocationToolAssignmentUseCase
    {
        public List<LocationToolAssignment> LoadLocationToolAssignmentsReturnValue { get; set; }
        public List<LocationReferenceLink> LoadLocationReferenceLinksForToolReturnValue { get; set; } = new List<LocationReferenceLink>();
        public ToolId LoadLocationReferenceLinksForToolParameter { get; set; }
        public List<ToolUsage> LoadUnusedToolUsagesForLocationReturnValue { get; set; } = new List<ToolUsage>();
        public LocationId LoadUnusedToolUsagesForLocationParameter { get; set; }
        public List<LocationToolAssignment> GetLocationToolAssignmentsByLocationIdReturnValue { get; set; } = new List<LocationToolAssignment>();
        public LocationId GetLocationToolAssignmentsByLocationIdParameter { get; set; }
        public List<LocationToolAssignment> GetLocationToolAssignmentsByIdsReturnValue { get; set; } = new List<LocationToolAssignment>();
        public List<LocationToolAssignmentId> GetLocationToolAssignmentsByIdsParameter { get; set; }
        public DateTime UpdateNextChkTestDateParameterDateTime { get; set; }
        public long UpdateNextChkTestDateParameterCondRot { get; set; }
        public DateTime UpdateNextMfuTestDateParameterDateTime { get; set; }
        public long UpdateNextMfuTestDateParameterCondRot { get; set; }
        public List<LocationToolAssignmentId> InsertLocationToolAssignmentReturnValue { get; set; }
        public List<LocationToolAssignmentDiff> InsertLocationToolAssignmentParameterDiffs { get; set; }
        public User AddTestConditionsParameterUser { get; set; }
        public LocationToolAssignment AddTestConditionsParameterAssignment { get; set; }
        public List<LocationToolAssignmentDiff> UpdateLocationToolAssignmentParameterDiff { get; set; }

        public List<LocationToolAssignment> LoadLocationToolAssignments()
        {
            return LoadLocationToolAssignmentsReturnValue;
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
            UpdateNextChkTestDateParameterCondRot = condRotId;
            UpdateNextChkTestDateParameterDateTime = dateTime;
        }

        public void UpdateNextMfuTestDate(long condRotId, DateTime dateTime)
        {
            UpdateNextMfuTestDateParameterCondRot = condRotId;
            UpdateNextMfuTestDateParameterDateTime = dateTime;
        }

        public void AddTestConditions(LocationToolAssignment assignment, User user)
        {
            AddTestConditionsParameterAssignment = assignment;
            AddTestConditionsParameterUser = user;
        }

        public void UpdateLocationToolAssignment(List<LocationToolAssignmentDiff> diffs)
        {
            UpdateLocationToolAssignmentParameterDiff = diffs;
        }

        public List<LocationToolAssignmentId> InsertLocationToolAssignment(List<LocationToolAssignmentDiff> diffs)
        {
            InsertLocationToolAssignmentParameterDiffs = diffs;
            return InsertLocationToolAssignmentReturnValue;
        }
    }
    

    public class LocationToolAssignmentServiceTest
    {
        [TestCaseSource(nameof(LocationToolAssignmentDatas))]
        public void LoadLocationToolAssignmentsReturnsDataOfUseCase(List<LocationToolAssignment> entityList)
        {
            var tuple = CreateServiceTuple();
            tuple.useCase.LoadLocationToolAssignmentsReturnValue = entityList;
            var result = tuple.service.LoadLocationToolAssignments(new NoParams(), null).Result;
            CheckerFunctions.CollectionAssertAreEquivalent(entityList, result.Values, EqualityChecker.AreLocationToolAssignmentEntityAndDtoEqual);
        }

        [TestCase(1)]
        [TestCase(99)]
        public void LoadLocationReferenceLinksForToolCallsUseCase(long toolId)
        {
            var tuple = CreateServiceTuple();

            tuple.service.LoadLocationReferenceLinksForTool(new Long() { Value = toolId }, null);

            Assert.AreEqual(toolId, tuple.useCase.LoadLocationReferenceLinksForToolParameter.ToLong());
        }

        private static IEnumerable<List<LocationReferenceLink>> locationLinkData = new List<List<LocationReferenceLink>>()
        {
            new List<LocationReferenceLink>()
            {
                new LocationReferenceLink(new QstIdentifier(1), "21435", "435456" ),
                new LocationReferenceLink(new QstIdentifier(99), "99765", "11111" )
            },
            new List<LocationReferenceLink>()
            {
                new LocationReferenceLink(new QstIdentifier(66), "666", "44444" ),
            }
        };

        [TestCaseSource(nameof(locationLinkData))]
        public void LoadLocationReferenceLinksForToolReturnsCorrectValue(List<LocationReferenceLink> locationReferenceLink)
        {
            var tuple = CreateServiceTuple();
            tuple.useCase.LoadLocationReferenceLinksForToolReturnValue = locationReferenceLink;

            var result = tuple.service.LoadLocationReferenceLinksForTool(new Long(), null);

            var comparer = new Func<LocationReferenceLink, DtoTypes.LocationLink, bool>((locationLink, dtoLocationLink) =>
                locationLink.Id.ToLong() == dtoLocationLink.Id &&
                locationLink.Description == dtoLocationLink.Description &&
                locationLink.Number == dtoLocationLink.Number
            );

            CheckerFunctions.CollectionAssertAreEquivalent(locationReferenceLink, result.Result.LocationLinks, comparer);
        }

        [TestCase(1)]
        [TestCase(13)]
        public void LoadUnusedToolUsagesForLocationCallsUseCase(long locationId)
        {
            var tuple = CreateServiceTuple();

            tuple.service.LoadUnusedToolUsagesForLocation(new Long() {Value = locationId}, null);

            Assert.AreEqual(locationId, tuple.useCase.LoadUnusedToolUsagesForLocationParameter.ToLong());
        }

        private static IEnumerable<List<ToolUsage>> toolUsageData = new List<List<ToolUsage>>()
        {
            new List<ToolUsage>()
            {
                new ToolUsage() {Id = new ToolUsageId(1), Description = new ToolUsageDescription("test"), Alive = true},
                new ToolUsage() {Id = new ToolUsageId(12), Description = new ToolUsageDescription("Hand 1"), Alive = false}
            },
            new List<ToolUsage>()
            {
                new ToolUsage() {Id = new ToolUsageId(145), Description = new ToolUsageDescription("1. Hand"), Alive = true}
            }
        };

        [TestCaseSource(nameof(toolUsageData))]
        public void LoadUnusedToolUsagesForLocationReturnsCorrectValue(List<ToolUsage> toolUsages)
        {
            var tuple = CreateServiceTuple();
            tuple.useCase.LoadUnusedToolUsagesForLocationReturnValue = toolUsages;

            var result = tuple.service.LoadUnusedToolUsagesForLocation(new Long(), null);

            var comparer = new Func<ToolUsage, DtoTypes.ToolUsage, bool>((toolUsage, dtoToolUsage) =>
                EqualityChecker.CompareToolUsageDtoWithToolUsage(dtoToolUsage, toolUsage)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(toolUsages, result.Result.ToolUsageList, comparer);
        }

        [TestCase(1)]
        [TestCase(99)]
        public void GetLocationToolAssignmentsByLocationIdCallsUseCase(long locationId)
        {
            var tuple = CreateServiceTuple();

            tuple.service.GetLocationToolAssignmentsByLocationId(new Long() { Value = locationId }, null);

            Assert.AreEqual(locationId, tuple.useCase.GetLocationToolAssignmentsByLocationIdParameter.ToLong());
        }

        [TestCaseSource(nameof(LocationToolAssignmentDatas))]
        public void GetLocationToolAssignmentsByLocationIdReturnsData(List<LocationToolAssignment> entityList)
        {
            var tuple = CreateServiceTuple();
            tuple.useCase.GetLocationToolAssignmentsByLocationIdReturnValue = entityList;
            var result = tuple.service.GetLocationToolAssignmentsByLocationId(new Long(), null).Result;
            CheckerFunctions.CollectionAssertAreEquivalent(entityList, result.Values, EqualityChecker.AreLocationToolAssignmentEntityAndDtoEqual);
        }

        private static IEnumerable<ListOfLongs> GetLocationToolAssignmentsByIdsCallsUseCaseData =
            new List<ListOfLongs>()
            {
                new ListOfLongs()
                {
                    Values =
                    {
                        new Long() {Value = 1},
                        new Long() {Value = 1113},
                        new Long() {Value = 14},
                        new Long() {Value = 13},
                    }
                },
                new ListOfLongs()
                {
                    Values =
                    {
                        new Long() {Value = 41},
                        new Long() {Value = 24},
                    }
                }
            };

        [TestCaseSource(nameof(GetLocationToolAssignmentsByIdsCallsUseCaseData))]
        public void GetLocationToolAssignmentsByIdsCallsUseCase(ListOfLongs ids)
        {
            var tuple = CreateServiceTuple();

            tuple.service.GetLocationToolAssignmentsByIds(ids, null);

            Assert.AreEqual(ids.Values.Select(x => x.Value).ToList(), tuple.useCase.GetLocationToolAssignmentsByIdsParameter.Select(x => x.ToLong()));
        }

        [TestCaseSource(nameof(LocationToolAssignmentDatas))]
        public void GetLocationToolAssignmentsByIdsReturnsData(List<LocationToolAssignment> entityList)
        {
            var tuple = CreateServiceTuple();
            tuple.useCase.GetLocationToolAssignmentsByIdsReturnValue = entityList;
            var result = tuple.service.GetLocationToolAssignmentsByIds(new ListOfLongs(), null).Result;
            CheckerFunctions.CollectionAssertAreEquivalent(entityList, result.Values, EqualityChecker.AreLocationToolAssignmentEntityAndDtoEqual);
        }

        private static IEnumerable<List<LocationToolAssignment>> LocationToolAssignmentDatas()
        {
            yield return new List<LocationToolAssignment>()
            {
                new LocationToolAssignment()
                {
                    Id = new LocationToolAssignmentId(654),
                    AssignedLocation = CreateLocation.Parameterized(
                        1,
                        "12345",
                        "765433", 
                        "Kommentar X", 
                        "C1",
                        "C",
                        true,
                        1.1,
                        12.1,
                        4,
                        LocationControlledBy.Angle,
                        12.3,
                        12,
                        1,
                        3,
                        CreateToleranceClass.WithId(1),
                        2,
                        CreateToleranceClass.WithId(9),
                        true),
                    AssignedTool = Server.TestHelper.Factories.CreateTool.Parameterized(
                        1, 
                        "test 1", 
                        "435634", 
                        true,
                        CreateToolModel.Randomized(67898),
                        "a", 
                        "b", 
                        "c", 
                        "d",
                        new ConfigurableField() {ListId = new HelperTableEntityId(1), Value = new HelperTableEntityValue("f")},
                        new CostCenter(){ListId = new HelperTableEntityId(1), Value = new HelperTableEntityValue("XXX")},
                        new Status(){Id = new StatusId(11), Value = new StatusDescription("V")}),
                    ToolUsage = new ToolUsage()
                    {
                         Id = new ToolUsageId(12),
                         Description = new ToolUsageDescription("Pos 1"),
                         Alive = true
                    },
                    TestTechnique = new TestTechnique()
                    {
                        TorqueCoefficient = 12.7,
                        Threshold = 12,
                        MeasureDelayTime = 1.1,
                        CycleComplete = 1234.4,
                        FilterFrequency = 17,
                        EndCycleTime = 8.7,
                        StartFinalAngle = 23,
                        SlipTorque = 3.5,
                        MaximumPulse = 14,
                        MustTorqueAndAngleBeInLimits = true,
                        MinimumPulse = 1,
                        ResetTime = 1,
                        CycleStart = 16
                    },
                    TestParameters = new Server.Core.Entities.TestParameters()
                    {
                        Minimum1 = 1,
                        Maximum2 = 2,
                        SetPoint2 = 3,
                        Threshold1 = 4,
                        ControlledBy = LocationControlledBy.Angle,
                        Maximum1 = 4,
                        ToleranceClass1 = new ToleranceClass()
                        {
                            Id = new ToleranceClassId(2),
                            Name = "Free Class",
                            Alive = true,
                            UpperLimit = 10,
                            LowerLimit = 4,
                            Relative = false
                        },
                        Minimum2 = 33,
                        SetPoint1 = 1,
                        ToleranceClass2 = new ToleranceClass()
                        {
                            Id = new ToleranceClassId(22),
                            Name = "Free Class 1",
                            Alive = false,
                            UpperLimit = 110,
                            LowerLimit = 42,
                            Relative = true
                        },
                    },
                    TestLevelSetMfu = new TestLevelSet()
                    {
                        Id = new TestLevelSetId(67890),
                        Name = new TestLevelSetName("wi4uüq3kcu")
                    },
                    TestLevelNumberMfu = 8,
                    TestLevelSetChk = new TestLevelSet()
                    {
                        Id = new TestLevelSetId(67890),
                        Name = new TestLevelSetName("wi4uüq3kcu")
                    },
                    TestLevelNumberChk = 8,
                    NextTestDateMfu = new System.DateTime(2020, 3, 7),
                    NextTestDateChk = new System.DateTime(2020, 7, 3),
                    NextTestShiftMfu = Shift.FirstShiftOfDay,
                    NextTestShiftChk = Shift.SecondShiftOfDay
                },
                new LocationToolAssignment()
                {
                    Id = new LocationToolAssignmentId(654),
                    AssignedLocation =CreateLocation.Parameterized(13, "avcd","4667",
                        "A D", "CL","A",true,1.1,
                        12.1,4, LocationControlledBy.Angle, 122.3,1.2,1,
                        3,CreateToleranceClass.WithId(1),2,CreateToleranceClass.WithId(9),false),
                    AssignedTool = Server.TestHelper.Factories.CreateTool.Parameterized(134, "Wkz.1", "XXX", true,
                        CreateToolModel.Randomized(758393), "Z", "1", "2", "3",
                        new ConfigurableField() {ListId = new HelperTableEntityId(1), Value = new HelperTableEntityValue("C")},
                        new CostCenter(){ListId = new HelperTableEntityId(14), Value = new HelperTableEntityValue("123")},
                        new Status(){Id = new StatusId(115), Value = new StatusDescription("VD")}),
                    ToolUsage = new ToolUsage()
                    {
                         Id = new ToolUsageId(12),
                         Description = new ToolUsageDescription("Pos 1"),
                         Alive = true
                    },
                    TestTechnique = new TestTechnique()
                    {
                        TorqueCoefficient = 12.7,
                        Threshold = 12,
                        MeasureDelayTime = 1.1,
                        CycleComplete = 1234.4,
                        FilterFrequency = 17,
                        EndCycleTime = 8.7,
                        StartFinalAngle = 23,
                        SlipTorque = 3.5,
                        MaximumPulse = 14,
                        MustTorqueAndAngleBeInLimits = true,
                        MinimumPulse = 1,
                        ResetTime = 1,
                        CycleStart = 16
                    },
                    TestParameters = new Server.Core.Entities.TestParameters()
                    {
                        Minimum1 = 1,
                        Maximum2 = 2,
                        SetPoint2 = 3,
                        Threshold1 = 4,
                        ControlledBy = LocationControlledBy.Angle,
                        Maximum1 = 4,
                        ToleranceClass1 = new ToleranceClass()
                        {
                            Id = new ToleranceClassId(2),
                            Name = "Free Class",
                            Alive = true,
                            UpperLimit = 10,
                            LowerLimit = 4,
                            Relative = false
                        },
                        Minimum2 = 33,
                        SetPoint1 = 1,
                        ToleranceClass2 = new ToleranceClass()
                        {
                            Id = new ToleranceClassId(22),
                            Name = "Free Class 1",
                            Alive = false,
                            UpperLimit = 110,
                            LowerLimit = 42,
                            Relative = true
                        },
                    },
                    TestLevelSetMfu = new TestLevelSet()
                    {
                        Id = new TestLevelSetId(63),
                        Name = new TestLevelSetName("ftguzh")
                    },
                    TestLevelNumberMfu = 495,
                    TestLevelSetChk = new TestLevelSet()
                    {
                        Id = new TestLevelSetId(67890),
                        Name = new TestLevelSetName("wi4uüq3kcu")
                    },
                    TestLevelNumberChk = 8,
                    NextTestDateMfu = new System.DateTime(2021, 3, 7),
                    NextTestDateChk = new System.DateTime(2021, 7, 3),
                    NextTestShiftMfu = Shift.ThirdShiftOfDay,
                    NextTestShiftChk = Shift.FirstShiftOfDay
                }
            };
            yield return new List<LocationToolAssignment>()
            {
                new LocationToolAssignment()
                {
                    Id = new LocationToolAssignmentId(2345),
                    AssignedLocation = CreateLocation.Parameterized(1, "abcd","efgh",
                        "Kommentar 12X", "V1","C",false,13.1,
                        12.15,41,LocationControlledBy.Torque, 132.3,142,10,
                        32, CreateToleranceClass.WithId(13),2,CreateToleranceClass.WithId(91),false),
                    AssignedTool = Server.TestHelper.Factories.CreateTool.Parameterized(4, "2233 1", "abc", false,
                        CreateToolModel.Randomized(4397), "Accesory", "A1", "A2", "A3",
                        new ConfigurableField() {ListId = new HelperTableEntityId(14), Value = new HelperTableEntityValue("Conf")},
                        new CostCenter(){ListId = new HelperTableEntityId(14), Value = new HelperTableEntityValue("Cost")},
                        new Status(){Id = new StatusId(87), Value = new StatusDescription("Status")}),
                    ToolUsage = new ToolUsage()
                    {
                         Id = new ToolUsageId(12),
                         Description = new ToolUsageDescription("Pos 1"),
                         Alive = true
                    },
                    TestTechnique = new TestTechnique()
                    {
                        TorqueCoefficient = 12.7,
                        Threshold = 12,
                        MeasureDelayTime = 1.1,
                        CycleComplete = 1234.4,
                        FilterFrequency = 17,
                        EndCycleTime = 8.7,
                        StartFinalAngle = 23,
                        SlipTorque = 3.5,
                        MaximumPulse = 14,
                        MustTorqueAndAngleBeInLimits = true,
                        MinimumPulse = 1,
                        ResetTime = 1,
                        CycleStart = 16
                    },
                    TestParameters = new Server.Core.Entities.TestParameters()
                    {
                        Minimum1 = 1,
                        Maximum2 = 2,
                        SetPoint2 = 3,
                        Threshold1 = 4,
                        ControlledBy = LocationControlledBy.Angle,
                        Maximum1 = 4,
                        ToleranceClass1 = new ToleranceClass()
                        {
                            Id = new ToleranceClassId(2),
                            Name = "Free Class",
                            Alive = true,
                            UpperLimit = 10,
                            LowerLimit = 4,
                            Relative = false
                        },
                        Minimum2 = 33,
                        SetPoint1 = 1,
                        ToleranceClass2 = new ToleranceClass()
                        {
                            Id = new ToleranceClassId(22),
                            Name = "Free Class 1",
                            Alive = false,
                            UpperLimit = 110,
                            LowerLimit = 42,
                            Relative = true
                        },
                    },
                    TestLevelSetMfu = new TestLevelSet()
                    {
                        Id = new TestLevelSetId(67890),
                        Name = new TestLevelSetName("acw")
                    },
                    TestLevelNumberMfu = 85,
                    TestLevelSetChk = new TestLevelSet()
                    {
                        Id = new TestLevelSetId(67890),
                        Name = new TestLevelSetName("acw")
                    },
                    TestLevelNumberChk = 85,
                    NextTestDateMfu = new System.DateTime(2020, 3, 9),
                    NextTestDateChk = new System.DateTime(2020, 7, 30),
                    NextTestShiftMfu = Shift.SecondShiftOfDay,
                    NextTestShiftChk = Shift.ThirdShiftOfDay
                }
            };
        }


        private static (NetworkView.Services.LocationToolAssignmentService service, LocationToolAssignmentUseCaseMock useCase) CreateServiceTuple()
        {
            var useCase = new LocationToolAssignmentUseCaseMock();
            var service = new NetworkView.Services.LocationToolAssignmentService(useCase, null);
            return (service, useCase);
        }
    }
}
