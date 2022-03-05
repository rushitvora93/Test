using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Client.TestHelper.Mock;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.Enums;
using Core.PhysicalValueTypes;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using LocationToolAssignmentService;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;
using DateTime = System.DateTime;
using LocationToolAssignment = Core.Entities.LocationToolAssignment;
using LocationToolAssignmentDiff = Core.Diffs.LocationToolAssignmentDiff;
using User = Core.Entities.User;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    class LocationToolAssignmentClientMock : ILocationToolAssignmentClient
    {
        public ListOfLocationToolAssignments LoadLocationToolAssignmentsReturnValue { get; set; }
        public ListOfLocationLink LoadLocationReferenceLinksForToolReturnValue { get; set; } = new ListOfLocationLink();
        public Long LoadLocationReferenceLinksForToolParameter { get; set; }
        public ListOfToolUsage LoadUnusedToolUsagesForLocationReturnValue { get; set; } = new ListOfToolUsage();
        public Long LoadUnusedToolUsagesForLocationParameter { get; set; }
        public ListOfLocationToolAssignments GetLocationToolAssignmentsByLocationIdReturnValue { get; set; } = new ListOfLocationToolAssignments();
        public Long GetLocationToolAssignmentsByLocationIdParameter { get; set; }
        public ListOfLocationToolAssignments GetLocationToolAssignmentsByIdsReturnValue { get; set; } = new ListOfLocationToolAssignments();
        public ListOfLongs GetLocationToolAssignmentsByIdsParameter { get; set; }
        public ListOfLongs InsertLocationToolAssignmentsWithHistoryReturnValue { get; set; } = new ListOfLongs();
        public ListOfLocationToolAssignmentDiffs InsertLocationToolAssignmentsWithHistoryParameter { get; set; }
        public ListOfLocationToolAssignmentDiffs UpdateLocationToolAssignmentsWithHistoryParameter { get; set; }
        public AddTestConditionsRequest AddTestConditionsParameter { get; set; }

        public ListOfLocationToolAssignments LoadLocationToolAssignments()
        {
            return LoadLocationToolAssignmentsReturnValue;
        }

        public ListOfLocationLink LoadLocationReferenceLinksForTool(Long toolId)
        {
            LoadLocationReferenceLinksForToolParameter = toolId;
            return LoadLocationReferenceLinksForToolReturnValue;
        }

        public ListOfToolUsage LoadUnusedToolUsagesForLocation(Long locationId)
        {
            LoadUnusedToolUsagesForLocationParameter = locationId;
            return LoadUnusedToolUsagesForLocationReturnValue;
        }

        public ListOfLocationToolAssignments GetLocationToolAssignmentsByLocationId(Long locationId)
        {
            GetLocationToolAssignmentsByLocationIdParameter = locationId;
            return GetLocationToolAssignmentsByLocationIdReturnValue;
        }

        public ListOfLocationToolAssignments GetLocationToolAssignmentsByIds(ListOfLongs ids)
        {
            GetLocationToolAssignmentsByIdsParameter = ids;
            return GetLocationToolAssignmentsByIdsReturnValue;
        }

        public ListOfLongs InsertLocationToolAssignmentsWithHistory(ListOfLocationToolAssignmentDiffs diffs)
        {
            InsertLocationToolAssignmentsWithHistoryParameter = diffs;
            return InsertLocationToolAssignmentsWithHistoryReturnValue;
        }

        public void UpdateLocationToolAssignmentsWithHistory(ListOfLocationToolAssignmentDiffs diffs)
        {
            UpdateLocationToolAssignmentsWithHistoryParameter = diffs;
        }

        public void AddTestConditions(AddTestConditionsRequest request)
        {
            AddTestConditionsParameter = request;
        }
    }


    public class LocationToolAssignmentDataAccessTest
    {
        [TestCaseSource(nameof(CreateAnonymousLocationToolAssignmentDtos))]
        public void LoadLocationToolAssignmentsReturnsDataFromClient(ListOfLocationToolAssignments dtos)
        {
            var environment = new Environment();
            environment.mocks.locationToolAssignmentClient.LoadLocationToolAssignmentsReturnValue = dtos;

            var result = environment.dataAccess.LoadLocationToolAssignments();
            CheckerFunctions.CollectionAssertAreEquivalent(result, dtos.Values, AreLocationToolAssignmentEntityAndDtoEqual);
        }

        [TestCase(1)]
        [TestCase(100)]
        public void LoadLocationReferenceLinksForToolCallsClient(long toolId)
        {
            var environment = new Environment();
            environment.dataAccess.LoadLocationReferenceLinksForTool(new ToolId(toolId));
            Assert.AreEqual(toolId, environment.mocks.locationToolAssignmentClient.LoadLocationReferenceLinksForToolParameter.Value);
        }

        static IEnumerable<ListOfLocationLink> LocationReferenceLinkData = new List<ListOfLocationLink>()
        {
            new ListOfLocationLink()
            {
                LocationLinks =
                {
                    CreateLocationReferenceLink(1, "234435", "546547679"),
                    CreateLocationReferenceLink(5, "444", "576u"),

                }
            },
            new ListOfLocationLink()
            {
                LocationLinks =
                {
                    CreateLocationReferenceLink(55, "4444 54", "32446")
                }
            }
        };

        [TestCaseSource(nameof(LocationReferenceLinkData))]
        public void LoadLocationReferenceLinksForToolReturnsCorrectValue(ListOfLocationLink locationLinks)
        {
            var environment = new Environment();
            environment.mocks.locationToolAssignmentClient.LoadLocationReferenceLinksForToolReturnValue = locationLinks;
            var result = environment.dataAccess.LoadLocationReferenceLinksForTool(new ToolId(1));

            var comparer = new Func<LocationLink, LocationReferenceLink, bool>((locReferenceLinkDto, locReferenceLink) =>
                locReferenceLinkDto.Id == locReferenceLink.Id.ToLong() &&
                locReferenceLinkDto.Number == locReferenceLink.Number.ToDefaultString() &&
                locReferenceLinkDto.Description == locReferenceLink.Description.ToDefaultString()
            );

            CheckerFunctions.CollectionAssertAreEquivalent(locationLinks.LocationLinks, result, comparer);
        }

        [TestCase(1)]
        [TestCase(100)]
        public void LoadUnusedToolUsagesForLocationCallsClient(long locationId)
        {
            var environment = new Environment();
            environment.dataAccess.LoadUnusedToolUsagesForLocation(new LocationId(locationId));
            Assert.AreEqual(locationId, environment.mocks.locationToolAssignmentClient.LoadUnusedToolUsagesForLocationParameter.Value);
        }

        private static IEnumerable<ListOfToolUsage> LoadToolUsagesData = new List<ListOfToolUsage>()
        {
            new ListOfToolUsage()
            {
                ToolUsageList =
                {
                    new DtoTypes.ToolUsage() { Id = 1, Description = "1. Hand", Alive = true},
                    new DtoTypes.ToolUsage() { Id = 133, Description = "Test 99", Alive = false}
                }
            },
            new ListOfToolUsage()
            {
                ToolUsageList =
                {
                    new DtoTypes.ToolUsage() { Id = 31, Description = "1. Hand A", Alive = true},
                }
            }
        };

        [TestCaseSource(nameof(LoadToolUsagesData))]
        public void LoadUnusedToolUsagesForLocationReturnsCorrectValue(ListOfToolUsage listOfToolUsages)
        {
            var environment = new Environment();
            environment.mocks.locationToolAssignmentClient.LoadUnusedToolUsagesForLocationReturnValue = listOfToolUsages;

            var result = environment.dataAccess.LoadUnusedToolUsagesForLocation(new LocationId(1));

            var comparer = new Func<DtoTypes.ToolUsage, Core.Entities.ToolUsage, bool>((dtoToolUsage, toolUsage) =>
                CompareToolUsageWithToolUsageDto(toolUsage, dtoToolUsage)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(listOfToolUsages.ToolUsageList, result, comparer);
        }

        [TestCase(1)]
        [TestCase(100)]
        public void LoadAssignedToolsForLocationCallsClient(long locationId)
        {
            var environment = new Environment();
            environment.dataAccess.LoadAssignedToolsForLocation(new LocationId(locationId));
            Assert.AreEqual(locationId, environment.mocks.locationToolAssignmentClient.GetLocationToolAssignmentsByLocationIdParameter.Value);
        }

        [TestCaseSource(nameof(CreateAnonymousLocationToolAssignmentDtos))]
        public void LoadAssignedToolsForLocationReturnsDataFromClient(ListOfLocationToolAssignments dtos)
        {
            var environment = new Environment();
            environment.mocks.locationToolAssignmentClient.GetLocationToolAssignmentsByLocationIdReturnValue = dtos;
            var result = environment.dataAccess.LoadAssignedToolsForLocation(new LocationId(1));
            CheckerFunctions.CollectionAssertAreEquivalent(result, dtos.Values, AreLocationToolAssignmentEntityAndDtoEqual);
        }

        static IEnumerable<List<LocationToolAssignmentId>> GetLocationToolAssignmentsByIdsCallsClientData = new List<List<LocationToolAssignmentId>>()
        {
            new List<LocationToolAssignmentId>()
            {
                new LocationToolAssignmentId(1),
                new LocationToolAssignmentId(4),
                new LocationToolAssignmentId(8),
            },
            new List<LocationToolAssignmentId>()
            {
                new LocationToolAssignmentId(123),
                new LocationToolAssignmentId(45),
            },
        };

        [TestCaseSource(nameof(GetLocationToolAssignmentsByIdsCallsClientData))]
        public void GetLocationToolAssignmentsByIdsCallsClient(List<LocationToolAssignmentId> datas)
        {
            var environment = new Environment();
            environment.dataAccess.GetLocationToolAssignmentsByIds(datas);
            Assert.AreEqual(datas.Select(x => x.ToLong()).ToList(), environment.mocks.locationToolAssignmentClient.GetLocationToolAssignmentsByIdsParameter.Values.Select(x => x.Value).ToList());
        }

        [TestCaseSource(nameof(CreateAnonymousLocationToolAssignmentDtos))]
        public void GetLocationToolAssignmentsByIdsReturnsDataFromClient(ListOfLocationToolAssignments dtos)
        {
            var environment = new Environment();
            environment.mocks.locationToolAssignmentClient.GetLocationToolAssignmentsByIdsReturnValue = dtos;
            var result = environment.dataAccess.GetLocationToolAssignmentsByIds(new List<LocationToolAssignmentId>());
            CheckerFunctions.CollectionAssertAreEquivalent(result, dtos.Values, AreLocationToolAssignmentEntityAndDtoEqual);
        }

        [Test]
        public void AssignToolToLocationWithNullThrowsArgumentNullException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentNullException>(() => { environment.dataAccess.AssignToolToLocation(null, null); });
        }

        private static IEnumerable<(LocationToolAssignment, User, string)> LocationToolAssignmentData =
            new List<(LocationToolAssignment, User, string)>()
            {
                (
                    new LocationToolAssignment()
                    {
                        Id = new LocationToolAssignmentId(12),
                        ToolUsage = new Core.Entities.ToolUsage()
                        {
                            ListId = new HelperTableEntityId(14),
                            Value = new ToolUsageDescription("usage12")
                        },
                        AssignedLocation = CreateLocation.Parameterized(13,"aaaaa", "bbbbbb","comm", "C1","C",
                            false,10,100,50,LocationControlledBy.Angle,10,50,100,
                            10,CreateToleranceClass.WithId(10),5,CreateToleranceClass.Anonymous()),
                        AssignedTool = CreateTool.Parameterized(11, "AAAA", "BBBB", CreateToolModel.Anonymous(),
                            "X", "Y1", "Z2", "L3",
                            new ConfigurableField() {ListId = new HelperTableEntityId(11), Value = new HelperTableDescription("Test")},
                            new CostCenter(){ListId = new HelperTableEntityId(11), Value = new HelperTableDescription("AAA")},
                            new Core.Entities.Status(){ListId = new HelperTableEntityId(1), Value = new StatusDescription("BB")}),
                        TestParameters = new Core.Entities.TestParameters()
                        {
                            ControlledBy = LocationControlledBy.Torque,
                            ToleranceClassAngle = CreateToleranceClass.WithId(712),
                            ToleranceClassTorque = CreateToleranceClass.WithId(734),
                            MinimumAngle = Angle.FromDegree(71),
                            MaximumTorque = Torque.FromNm(710),
                            ThresholdTorque = Angle.FromDegree(71),
                            MaximumAngle = Angle.FromDegree(710),
                            MinimumTorque = Torque.FromNm(71),
                            SetPointAngle = Angle.FromDegree(75),
                            SetPointTorque = Torque.FromNm(75)
                        },
                        TestTechnique = new Core.Entities.TestTechnique()
                        {
                            TorqueCoefficient = 18,
                            Threshold = 27,
                            MeasureDelayTime = 36,
                            FilterFrequency = 45,
                            EndCycleTime = 54,
                            StartFinalAngle = 63,
                            SlipTorque = 73,
                            MaximumPulse = 82,
                            MustTorqueAndAngleBeInLimits = false,
                            MinimumPulse = 101,
                            ResetTime = 112,
                            CycleStart = 132,
                            CycleComplete = 143
                        },
                        NextTestDateChk = new DateTime(2022,1,2,3,4,5),
                        NextTestDateMfu = new DateTime(2019,6,5,4,3,2),
                        NextTestShiftChk = new Shift(),
                        NextTestShiftMfu = new Shift(),
                        TestLevelNumberChk = 12,
                        TestLevelNumberMfu = 33,
                        TestLevelSetChk = new Core.Entities.TestLevelSet()
                        {
                            Id = new TestLevelSetId(14),
                            Name = new TestLevelSetName("QQWE"),
                            TestLevel1 = new Core.Entities.TestLevel()
                            {
                                Id = new TestLevelId(1),
                                ConsiderWorkingCalendar = true,
                                IsActive = true,
                                SampleNumber = 2,
                                TestInterval = new Core.Entities.Interval()
                                {
                                    Type = IntervalType.EveryXDays,
                                    IntervalValue = 2
                                }
                            },
                            TestLevel2 = new Core.Entities.TestLevel()
                            {
                                Id = new TestLevelId(1),
                                ConsiderWorkingCalendar = true,
                                IsActive = true,
                                SampleNumber = 2,
                                TestInterval = new Core.Entities.Interval()
                                {
                                    Type = IntervalType.EveryXDays,
                                    IntervalValue = 2
                                }
                            },
                            TestLevel3 = new Core.Entities.TestLevel()
                            {
                                Id = new TestLevelId(1),
                                ConsiderWorkingCalendar = false,
                                IsActive = true,
                                SampleNumber = 23,
                                TestInterval = new Core.Entities.Interval()
                                {
                                    Type = IntervalType.EveryXShifts,
                                    IntervalValue = 23
                                }
                            },
                        },
                        TestLevelSetMfu = new Core.Entities.TestLevelSet()
                        {
                            Id = new TestLevelSetId(41),
                            Name = new TestLevelSetName("2342354"),
                            TestLevel1 = new Core.Entities.TestLevel()
                            {
                                Id = new TestLevelId(1),
                                ConsiderWorkingCalendar = true,
                                IsActive = true,
                                SampleNumber = 2,
                                TestInterval = new Core.Entities.Interval()
                                {
                                    Type = IntervalType.EveryXDays,
                                    IntervalValue = 24
                                }
                            },
                            TestLevel2 = new Core.Entities.TestLevel()
                            {
                                Id = new TestLevelId(1),
                                ConsiderWorkingCalendar = true,
                                IsActive = true,
                                SampleNumber = 2,
                                TestInterval = new Core.Entities.Interval()
                                {
                                    Type = IntervalType.EveryXDays,
                                    IntervalValue = 2
                                }
                            },
                            TestLevel3 = new Core.Entities.TestLevel()
                            {
                                Id = new TestLevelId(1),
                                ConsiderWorkingCalendar = true,
                                IsActive = true,
                                SampleNumber = 2,
                                TestInterval = new Core.Entities.Interval()
                                {
                                    Type = IntervalType.EveryXDays,
                                    IntervalValue = 2
                                }
                            },
                        }
                    },
                    CreateUser.IdOnly(13), "2343567"
                ),
                (
                    new LocationToolAssignment()
                    {
                        Id = new LocationToolAssignmentId(1),
                        ToolUsage = new Core.Entities.ToolUsage()
                        {
                            ListId = new HelperTableEntityId(1),
                            Value = new ToolUsageDescription("usage")
                        },
                        AssignedLocation = CreateLocation.Parameterized(1,"23425", "43545657","comm", "C1","C",
                            true,1,10,5,LocationControlledBy.Angle,1,5,10,
                            1,CreateToleranceClass.WithId(1),5,CreateToleranceClass.Anonymous()),
                        AssignedTool = CreateTool.Parameterized(1, "214324536", "32534647", CreateToolModel.Anonymous(),
                            "a", "a1", "a2", "a3",
                            new ConfigurableField() {ListId = new HelperTableEntityId(1), Value = new HelperTableDescription("32424")},
                            new CostCenter(){ListId = new HelperTableEntityId(1), Value = new HelperTableDescription("21425")},
                            new Core.Entities.Status(){ListId = new HelperTableEntityId(1), Value = new StatusDescription("24364")}),
                        TestParameters = new Core.Entities.TestParameters()
                        {
                            ControlledBy = LocationControlledBy.Angle,
                            ToleranceClassAngle = CreateToleranceClass.WithId(12),
                            ToleranceClassTorque = CreateToleranceClass.WithId(34),
                            MinimumAngle = Angle.FromDegree(1),
                            MaximumTorque = Torque.FromNm(10),
                            ThresholdTorque = Angle.FromDegree(1),
                            MaximumAngle = Angle.FromDegree(10),
                            MinimumTorque = Torque.FromNm(1),
                            SetPointAngle = Angle.FromDegree(5),
                            SetPointTorque = Torque.FromNm(5)
                        },
                        TestTechnique = new Core.Entities.TestTechnique()
                        {
                            TorqueCoefficient = 1,
                            Threshold = 2,
                            MeasureDelayTime = 3,
                            FilterFrequency = 4,
                            EndCycleTime = 5,
                            StartFinalAngle = 6,
                            SlipTorque = 7,
                            MaximumPulse = 8,
                            MustTorqueAndAngleBeInLimits = true,
                            MinimumPulse = 10,
                            ResetTime = 11,
                            CycleStart = 12,
                            CycleComplete = 13
                        },
                        NextTestDateChk = new DateTime(2012,1,2,3,4,5),
                        NextTestDateMfu = new DateTime(2013,6,5,4,3,2),
                        NextTestShiftChk = new Shift(),
                        NextTestShiftMfu = new Shift(),
                        TestLevelNumberChk = 1,
                        TestLevelNumberMfu = 3,
                        TestLevelSetChk = new Core.Entities.TestLevelSet()
                        {
                            Id = new TestLevelSetId(1),
                            Name = new TestLevelSetName("2342354"),
                            TestLevel1 = new Core.Entities.TestLevel()
                            {
                                Id = new TestLevelId(1),
                                ConsiderWorkingCalendar = true,
                                IsActive = true,
                                SampleNumber = 2,
                                TestInterval = new Core.Entities.Interval()
                                {
                                    Type = IntervalType.EveryXDays,
                                    IntervalValue = 2
                                }
                            },
                            TestLevel2 = new Core.Entities.TestLevel()
                            {
                                Id = new TestLevelId(1),
                                ConsiderWorkingCalendar = true,
                                IsActive = true,
                                SampleNumber = 2,
                                TestInterval = new Core.Entities.Interval()
                                {
                                    Type = IntervalType.EveryXDays,
                                    IntervalValue = 2
                                }
                            },
                            TestLevel3 = new Core.Entities.TestLevel()
                            {
                                Id = new TestLevelId(1),
                                ConsiderWorkingCalendar = true,
                                IsActive = true,
                                SampleNumber = 2,
                                TestInterval = new Core.Entities.Interval()
                                {
                                    Type = IntervalType.EveryXDays,
                                    IntervalValue = 2
                                }
                            },
                        },
                        TestLevelSetMfu = new Core.Entities.TestLevelSet()
                        {
                            Id = new TestLevelSetId(1),
                            Name = new TestLevelSetName("2342354"),
                            TestLevel1 = new Core.Entities.TestLevel(){
                                Id = new TestLevelId(1),
                                ConsiderWorkingCalendar = true,
                                IsActive = true,
                                SampleNumber = 2,
                                TestInterval = new Core.Entities.Interval()
                                {
                                    Type = IntervalType.EveryXDays,
                                    IntervalValue = 2
                                }
                            },
                            TestLevel2 = new Core.Entities.TestLevel()
                            {
                                Id = new TestLevelId(1),
                                ConsiderWorkingCalendar = true,
                                IsActive = true,
                                SampleNumber = 2,
                                TestInterval = new Core.Entities.Interval()
                                {
                                    Type = IntervalType.EveryXDays,
                                    IntervalValue = 2
                                }
                            },
                            TestLevel3 = new Core.Entities.TestLevel()
                            {
                                Id = new TestLevelId(1),
                                ConsiderWorkingCalendar = true,
                                IsActive = true,
                                SampleNumber = 2,
                                TestInterval = new Core.Entities.Interval()
                                {
                                    Type = IntervalType.EveryXDays,
                                    IntervalValue = 2
                                }
                            },
                        }
                    },
                    CreateUser.IdOnly(1), "645658"
                 )
            };

 
        [TestCaseSource(nameof(LocationToolAssignmentData))]
        public void AssignToolToLocationCallsClient((LocationToolAssignment assignemnt, User user, string comment) data)
        {
            var environment = new Environment();
            environment.mocks.locationToolAssignmentClient.InsertLocationToolAssignmentsWithHistoryReturnValue = new ListOfLongs() {Values = { new Long(){Value = data.assignemnt.Id.ToLong()}}};
            environment.dataAccess.AssignToolToLocation(data.assignemnt, data.user);
            var result = environment.mocks.locationToolAssignmentClient.InsertLocationToolAssignmentsWithHistoryParameter.Diffs.First();
            Assert.IsTrue(EqualityChecker.AreLocationToolAssignmentEntityAndDtoEqual(data.assignemnt, result.NewLocationToolAssignment));
            Assert.AreEqual(data.user.UserId.ToLong(),result.UserId);
        }


        [Test]
        public void AssignToolToLocationReturnNullThrowsNullReferenceException()
        {
            var environment = new Environment();
            Assert.Throws<NullReferenceException>(() => { environment.dataAccess.AssignToolToLocation(LocationToolAssignmentData.First().Item1, LocationToolAssignmentData.First().Item2);  });
        }

        [Test]
        public void AddTestConditionsWithNullAssignmentThrowsArgumentNullException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentNullException>(() => { environment.dataAccess.AddTestConditions(null, LocationToolAssignmentData.First().Item2); });
        }

        [Test]
        public void AddTestConditionsWithNullUserThrowsArgumentNullException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentNullException>(() => { environment.dataAccess.AddTestConditions(LocationToolAssignmentData.First().Item1, null); });
        }

        [TestCaseSource(nameof(LocationToolAssignmentData))]
        public void AddTestConditionsCallsClient((LocationToolAssignment assignemnt, User user, string comment) data)
        {
            var environment = new Environment();
            environment.dataAccess.AddTestConditions(data.assignemnt, data.user);
            var result = environment.mocks.locationToolAssignmentClient.AddTestConditionsParameter;
            Assert.IsTrue(EqualityChecker.AreLocationToolAssignmentEntityAndDtoEqual(data.assignemnt, result.LocationToolAssignment));
            Assert.AreEqual(data.user.UserId.ToLong(), result.UserId);
        }

        [Test]
        public void RemoveLocationToolAssignmentWithNullAssignmentThrowsArgumentNullException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentNullException>(() => { environment.dataAccess.RemoveLocationToolAssignment(null, LocationToolAssignmentData.First().Item2); });
        }

        [Test]
        public void RemoveLocationToolAssignmentWithNullUserThrowsArgumentNullException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentNullException>(() => { environment.dataAccess.RemoveLocationToolAssignment(LocationToolAssignmentData.First().Item1, null); });
        }

        [TestCaseSource(nameof(LocationToolAssignmentData))]
        public void RemoveLocationToolAssignmentCallsClient((LocationToolAssignment assignemnt, User user, string comment) data)
        {
            var environment = new Environment();
            environment.dataAccess.RemoveLocationToolAssignment(data.assignemnt, data.user);

            var clientParam = environment.mocks.locationToolAssignmentClient.UpdateLocationToolAssignmentsWithHistoryParameter;
            var clientDiff = clientParam.Diffs.First();

            Assert.AreEqual(1, clientParam.Diffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.IsTrue(EqualityChecker.AreLocationToolAssignmentEntityAndDtoEqual(data.assignemnt, clientDiff.NewLocationToolAssignment));
            Assert.IsTrue(EqualityChecker.AreLocationToolAssignmentEntityAndDtoEqual(data.assignemnt, clientDiff.OldLocationToolAssignment));
            Assert.AreEqual(true, clientDiff.OldLocationToolAssignment.Alive);
            Assert.AreEqual(false, clientDiff.NewLocationToolAssignment.Alive);
            Assert.AreEqual(true, clientDiff.OldLocationToolAssignment.TestParameters.Alive);
            Assert.AreEqual(false, clientDiff.NewLocationToolAssignment.TestParameters.Alive);
        }

        [Test]
        public void UpdateLocationToolAssignmentWithNullAssignmentThrowsArgumentNullException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentNullException>(() => { environment.dataAccess.UpdateLocationToolAssignment(null); });
        }

        [Test]
        public void UpdateLocationToolAssignmentWithMissmatchingIdsThrowsArgumentException()
        {
            var environment = new Environment();
            var diff = new Core.Diffs.LocationToolAssignmentDiff()
            {
                NewLocationToolAssignment = CreateLocationToolAssignment.IdOnly(1),
                OldLocationToolAssignment = CreateLocationToolAssignment.IdOnly(2)
            };
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.UpdateLocationToolAssignment(new List<LocationToolAssignmentDiff>() { diff }); });
        }

        [TestCaseSource(nameof(LocationToolAssignmentData))]
        public void UpdateLocationToolAssignmentCallsClient((LocationToolAssignment assignemnt, User user, string comment) data)
        {
            var environment = new Environment();
            var diff = new Core.Diffs.LocationToolAssignmentDiff
            {
                User = data.user,
                OldLocationToolAssignment = data.assignemnt,
                NewLocationToolAssignment = data.assignemnt,
                Comment = new HistoryComment(data.comment)
            };
            environment.dataAccess.UpdateLocationToolAssignment(new List<LocationToolAssignmentDiff>() { diff });

            var clientParam = environment.mocks.locationToolAssignmentClient.UpdateLocationToolAssignmentsWithHistoryParameter;
            var clientDiff = clientParam.Diffs.First();


            Assert.AreEqual(1, clientParam.Diffs.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.AreEqual(data.comment, clientDiff.Comment.Value);
            Assert.IsTrue(EqualityChecker.AreLocationToolAssignmentEntityAndDtoEqual(data.assignemnt, clientDiff.NewLocationToolAssignment));
            Assert.IsTrue(EqualityChecker.AreLocationToolAssignmentEntityAndDtoEqual(data.assignemnt, clientDiff.OldLocationToolAssignment));
            Assert.AreEqual(true, clientDiff.OldLocationToolAssignment.Alive);
            Assert.AreEqual(true, clientDiff.NewLocationToolAssignment.Alive);
            Assert.AreEqual(true, clientDiff.OldLocationToolAssignment.TestParameters.Alive);
            Assert.AreEqual(true, clientDiff.NewLocationToolAssignment.TestParameters.Alive);
        }
        

        private bool AreLocationToolAssignmentEntityAndDtoEqual(LocationToolAssignment entity, DtoTypes.LocationToolAssignment dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   entity.AssignedLocation.Id.ToLong() == dto.AssignedLocation.Id &&
                   entity.AssignedLocation.Number.ToDefaultString() == dto.AssignedLocation.Number &&
                   entity.AssignedLocation.Description.ToDefaultString() == dto.AssignedLocation.Description &&
                   entity.AssignedTool.Id.ToLong() == dto.AssignedTool.Id &&
                   entity.AssignedTool.InventoryNumber?.ToDefaultString() == dto.AssignedTool.InventoryNumber &&
                   entity.AssignedTool.SerialNumber?.ToDefaultString() == dto.AssignedTool.SerialNumber &&
                   entity.TestLevelSetMfu.Id.ToLong() == dto.TestLevelSetMfu.Id &&
                   entity.TestLevelSetMfu.Name.ToDefaultString() == dto.TestLevelSetMfu.Name &&
                   entity.TestLevelNumberMfu == dto.TestLevelNumberMfu &&
                   entity.TestLevelSetChk.Id.ToLong() == dto.TestLevelSetChk.Id &&
                   entity.TestLevelSetChk.Name.ToDefaultString() == dto.TestLevelSetChk.Name &&
                   entity.TestLevelNumberChk == dto.TestLevelNumberChk &&
                   EqualityChecker.ArePrimitiveDateTimeAndDtoEqual(entity.NextTestDateMfu.Value, dto.NextTestDateMfu.Value) &&
                   EqualityChecker.ArePrimitiveDateTimeAndDtoEqual(entity.NextTestDateChk.Value, dto.NextTestDateChk.Value) &&
                   (int)entity.NextTestShiftMfu == dto.NextTestShiftMfu.Value &&
                   (int)entity.NextTestShiftChk == dto.NextTestShiftChk.Value;
        }

        private bool CompareToolUsageWithToolUsageDto(Core.Entities.ToolUsage toolUsage, DtoTypes.ToolUsage dtoToolUsage)
        {
            return toolUsage.ListId.ToLong() == dtoToolUsage.Id &&
                   toolUsage.Value.ToDefaultString() == dtoToolUsage.Description;
        }

        private static LocationLink CreateLocationReferenceLink(int id, string number, string descritpion)
        {
            return new LocationLink()
            {
                Id = id,
                Number = number,
                Description = descritpion
            };
        }

        private static IEnumerable<ListOfLocationToolAssignments> CreateAnonymousLocationToolAssignmentDtos()
        {
            yield return new ListOfLocationToolAssignments
            {
                Values =
                {
                    new DtoTypes.LocationToolAssignment()
                    {
                        Id = 654,
                        AssignedLocation = DtoFactory.CreateLocation(1, "test", "sst", 1,"A", "B", false,
                            0,DtoFactory.CreateToleranceClass(1, "freie Eingabe", true, 0, 0, false),
                            DtoFactory.CreateToleranceClass(14, "Klasse 1", true, 10, 20, false),10,100,20,200,
                            5,50,3,"Kommentar", true),
                        AssignedTool = DtoFactory.CreateToolDto(13, "serial XX", "inventory YY", true,
                            "abc", "F1", "F2", "F3", "a",
                            new DtoTypes.Status(){Id = 1, Description="S", Alive = true},
                            new DtoTypes.HelperTableEntity(){ListId = 1,Value="C",Alive = true,NodeId = 5},
                            new DtoTypes.HelperTableEntity(){ListId = 11, Value="K",Alive = false, NodeId = 15},
                            DtoFactory.CreateToolModelDtoRandomized(378951)),
                        ToolUsage = DtoFactory.CreateToolUsageDto(1, "1. Hand", true),
                        TestTechnique = DtoFactory.CreaTestTechnique(1,2,3,4,5,true,
                            6,7,8,9,10,11,12),
                        TestParameters = DtoFactory.CreateTestParameters(1,2,3,4,5,6,7,8,9,1),
                        TestLevelSetMfu = new DtoTypes.TestLevelSet()
                        {
                            Id = 67890,
                            Name = "wi4uüq3kcu"
                        },
                        TestLevelNumberMfu = 8,
                        TestLevelSetChk = new DtoTypes.TestLevelSet()
                        {
                            Id = 67890,
                            Name = "wi4uüq3kcu"
                        },
                        TestLevelNumberChk = 8,
                        NextTestDateMfu = new NullableDateTime() { Value = new BasicTypes.DateTime() { Ticks = new System.DateTime(2012,1,1).Ticks} },
                        NextTestDateChk = new NullableDateTime() { Value = new BasicTypes.DateTime() { Ticks = new System.DateTime(2021,9,5).Ticks} },
                        NextTestShiftMfu = new NullableInt() { Value = 0 },
                        NextTestShiftChk = new NullableInt() { Value = 1 }
                    },
                    new DtoTypes.LocationToolAssignment()
                    {
                        Id = 654,
                        AssignedLocation = DtoFactory.CreateLocation(13, "ABCS", "1234", 11,"S", "X", true,
                            0,DtoFactory.CreateToleranceClass(1, "freie Eingabe", true, 0, 0, false),
                            DtoFactory.CreateToleranceClass(1, "freie Eingabe", true, 0, 0, false),
                            110,100,20,200,
                            5,50,3,"K !", false),
                        AssignedTool = DtoFactory.CreateToolDto(1, "ss 123", "11 123", true,
                            "abc", "F1", "F2", "F3", "a",
                            new DtoTypes.Status(){Id = 1, Description="S", Alive = true},
                            new DtoTypes.HelperTableEntity(){ListId = 1,Value="C",Alive = true,NodeId = 5},
                            new DtoTypes.HelperTableEntity(){ListId = 11, Value="K",Alive = false, NodeId = 15},
                            DtoFactory.CreateToolModelDtoRandomized(87968961)),
                        ToolUsage = DtoFactory.CreateToolUsageDto(13, "13. Hand", false),
                        TestTechnique = DtoFactory.CreaTestTechnique(11,22,31,42,53,false,
                            6,7,8,9,10,11,12),
                        TestParameters = DtoFactory.CreateTestParameters(13,24,35,41,55,61,73,81,91,1),
                        TestLevelSetMfu = new DtoTypes.TestLevelSet()
                        {
                            Id = 63,
                            Name = "ftguzh"
                        },
                        TestLevelNumberMfu = 495,
                        TestLevelSetChk = new DtoTypes.TestLevelSet()
                        {
                            Id = 67890,
                            Name = "wi4uüq3kcu"
                        },
                        TestLevelNumberChk = 8,
                        NextTestDateMfu = new NullableDateTime() { Value = new BasicTypes.DateTime() { Ticks = new System.DateTime(2017,4,18).Ticks} },
                        NextTestDateChk = new NullableDateTime() { Value = new BasicTypes.DateTime() { Ticks = new System.DateTime(2014,8,16).Ticks} },
                        NextTestShiftMfu = new NullableInt() { Value = 2 },
                        NextTestShiftChk = new NullableInt() { Value = 0 }
                    }
                }
            };
            yield return new ListOfLocationToolAssignments
            {
                Values =
                {
                    new DtoTypes.LocationToolAssignment()
                    {
                        Id = 2345,
                        AssignedLocation = DtoFactory.CreateLocation(13, "A", "sst", 1,"A", "B", false,
                            0,DtoFactory.CreateToleranceClass(14, "freie Eingabe", true, 0, 0, false),
                            DtoFactory.CreateToleranceClass(1, "freie Klasse", true, 0, 0, false),
                            106,1100,20,200,
                            52,1,31,"Kommentar", true),
                        AssignedTool = DtoFactory.CreateToolDto(1, "serial 123", "inventory 123", true,
                            "abc", "F1", "F2", "F3", "a",
                            new DtoTypes.Status(){Id = 1, Description="S", Alive = true},
                            new DtoTypes.HelperTableEntity(){ListId = 1,Value="C",Alive = true,NodeId = 5},
                            new DtoTypes.HelperTableEntity(){ListId = 11, Value="K",Alive = false, NodeId = 15},
                            DtoFactory.CreateToolModelDtoRandomized(1387469)),
                        ToolUsage = DtoFactory.CreateToolUsageDto(13, "1. Hand", true),
                        TestTechnique = DtoFactory.CreaTestTechnique(1,2,3,4,5,false,
                            61,73,18,19,110,111,132),
                        TestParameters = DtoFactory.CreateTestParameters(1,2,3,4,5,6,7,8,9,1),
                        TestLevelSetMfu = new DtoTypes.TestLevelSet()
                        {
                            Id = 67890,
                            Name = "acw"
                        },
                        TestLevelNumberMfu = 85,
                        TestLevelSetChk = new DtoTypes.TestLevelSet()
                        {
                            Id = 67890,
                            Name = "acw"
                        },
                        TestLevelNumberChk = 85,
                        NextTestDateMfu = new NullableDateTime() { Value = new BasicTypes.DateTime() { Ticks = new System.DateTime(2020,1,7).Ticks} },
                        NextTestDateChk = new NullableDateTime() { Value = new BasicTypes.DateTime() { Ticks = new System.DateTime(2021,7,8).Ticks} },
                        NextTestShiftMfu = new NullableInt() { Value = 0 },
                        NextTestShiftChk = new NullableInt() { Value = 2 }
                    }
                }
            };
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    clientFactory = new ClientFactoryMock();
                    channelWrapper = new ChannelWrapperMock();
                    locationToolAssignmentClient = new LocationToolAssignmentClientMock();
                    channelWrapper.GetLocationToolAssignmentClientReturnValue = locationToolAssignmentClient;
                    clientFactory.AuthenticationChannel = channelWrapper;
                    timeDataAccess = new TimeDataAccessMock();
                }
                public ClientFactoryMock clientFactory;
                public ChannelWrapperMock channelWrapper;
                public LocationToolAssignmentClientMock locationToolAssignmentClient;
                public TimeDataAccessMock timeDataAccess;
            }

            public Environment()
            {
                mocks = new Mocks();
                dataAccess = new LocationToolAssignmentDataAccess(mocks.clientFactory, new MockLocationDisplayFormatter(), mocks.timeDataAccess);
            }

            public readonly Mocks mocks;
            public readonly LocationToolAssignmentDataAccess dataAccess;
        }
    }
}
