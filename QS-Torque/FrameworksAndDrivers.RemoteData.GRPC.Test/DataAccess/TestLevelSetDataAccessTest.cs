using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Client.TestHelper.Mock;
using Core.Entities;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Mock;
using TestLevelSetService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    class TestLevelClientMock : ITestLevelSetClient
    {
        public ListOfTestLevelSets LoadTestLevelSetsReturnValue { get; set; }
        public DtoTypes.TestLevelSetDiff InsertTestLevelSetParameter { get; set; }
        public DtoTypes.TestLevelSet InsertTestLevelSetReturnValue { get; set; }
        public DtoTypes.TestLevelSetDiff DeleteTestLevelSetParameter { get; set; }
        public DtoTypes.TestLevelSetDiff UpdateTestLevelSetParameter { get; set; }
        public StringResponse IsTestLevelSetNameUniqueParameter { get; set; }
        public Bool IsTestLevelSetNameUniqueReturnValue { get; set; }
        public DtoTypes.TestLevelSet DoesTestLevelSetHaveReferencesPartameter { get; set; }
        public Bool DoesTestLevelSetHaveReferencesReturnValue { get; set; }

        public ListOfTestLevelSets LoadTestLevelSets()
        {
            return LoadTestLevelSetsReturnValue;
        }

        public DtoTypes.TestLevelSet InsertTestLevelSet(DtoTypes.TestLevelSetDiff diff)
        {
            InsertTestLevelSetParameter = diff;
            return InsertTestLevelSetReturnValue;
        }

        public void DeleteTestLevelSet(DtoTypes.TestLevelSetDiff diff)
        {
            DeleteTestLevelSetParameter = diff;
        }

        public void UpdateTestLevelSet(DtoTypes.TestLevelSetDiff diff)
        {
            UpdateTestLevelSetParameter = diff;
        }

        public Bool IsTestLevelSetNameUnique(StringResponse name)
        {
            IsTestLevelSetNameUniqueParameter = name;
            return IsTestLevelSetNameUniqueReturnValue;
        }

        public Bool DoesTestLevelSetHaveReferences(DtoTypes.TestLevelSet set)
        {
            DoesTestLevelSetHaveReferencesPartameter = set;
            return DoesTestLevelSetHaveReferencesReturnValue;
        }
    }


    public class TestLevelSetDataAccessTest
    {
        [TestCaseSource(nameof(CreateAnonymousListOfTestLevelSets))]
        public void LoadTestLevelSetsReturnsDataFromClient(ListOfTestLevelSets listOfTestLevelSets)
        {
            var tuple = CreateDataAccessTuple();
            tuple.client.LoadTestLevelSetsReturnValue = listOfTestLevelSets;
            var result = tuple.dataAccess.LoadTestLevelSets();
            CheckerFunctions.CollectionAssertAreEquivalent(result, listOfTestLevelSets.TestLevelSets, AreTestLevelSetEntityAndDtoEqual);
        }

        [TestCaseSource(nameof(CreateAnonymousTestLevelSets))]
        public void AddTestLevelSetPassesDataToClient(TestLevelSet entity)
        {
            var tuple = CreateDataAccessTuple();
            var userId = new System.Random().Next(200);
            tuple.dataAccess.AddTestLevelSet(new Client.Core.Diffs.TestLevelSetDiff() { New = entity, User = new User() { UserId = new UserId(userId) } });
            Assert.IsTrue(AreTestLevelSetEntityAndDtoEqual(entity, tuple.client.InsertTestLevelSetParameter.New));
            Assert.AreEqual(userId, tuple.client.InsertTestLevelSetParameter.UserId);
        }

        [TestCaseSource(nameof(CreateAnonymousTestLevelSetDtos))]
        public void AddTestLevelSetReturnsDataFromClient(DtoTypes.TestLevelSet dto)
        {
            var tuple = CreateDataAccessTuple();
            tuple.client.InsertTestLevelSetReturnValue = dto;
            var result = tuple.dataAccess.AddTestLevelSet(new Client.Core.Diffs.TestLevelSetDiff() { New = CreateAnonymousTestLevelSets().ToList()[0], User = new User() { UserId = new UserId(0) } });
            Assert.IsTrue(AreTestLevelSetEntityAndDtoEqual(result, dto));
        }

        [Test]
        public void AddTestLevelSetReturnsNullIfClientReturnsNull()
        {
            var tuple = CreateDataAccessTuple();
            tuple.client.InsertTestLevelSetReturnValue = null;
            var result = tuple.dataAccess.AddTestLevelSet(new Client.Core.Diffs.TestLevelSetDiff() { New = CreateAnonymousTestLevelSets().ToList()[0], User = new User() { UserId = new UserId(0) } });
            Assert.IsNull(result);
        }

        [TestCaseSource(nameof(CreateAnonymousTestLevelSets))]
        public void RemoveTestLevelSetPassesDataToClient(TestLevelSet entity)
        {
            var tuple = CreateDataAccessTuple();
            var userId = new System.Random().Next(200);
            tuple.dataAccess.RemoveTestLevelSet(new Client.Core.Diffs.TestLevelSetDiff() { Old = entity, User = new User() { UserId = new UserId(userId) } });
            Assert.IsTrue(AreTestLevelSetEntityAndDtoEqual(entity, tuple.client.DeleteTestLevelSetParameter.Old));
            Assert.AreEqual(userId, tuple.client.DeleteTestLevelSetParameter.UserId);
        }

        [TestCaseSource(nameof(CreateAnonymousTestLevelSetLists))]
        public void UpdateTestLevelSetPassesDataToClient(List<TestLevelSet> entities)
        {
            var tuple = CreateDataAccessTuple();
            tuple.dataAccess.UpdateTestLevelSet(new Client.Core.Diffs.TestLevelSetDiff() 
            { 
                New = entities[0], 
                Old = entities[1], 
                Comment = new HistoryComment(entities[0].Name.ToDefaultString()), 
                User = new User() { UserId = new UserId(entities[1].Id.ToLong())}
            });
            Assert.IsTrue(AreTestLevelSetEntityAndDtoEqual(entities[0], tuple.client.UpdateTestLevelSetParameter.New));
            Assert.IsTrue(AreTestLevelSetEntityAndDtoEqual(entities[1], tuple.client.UpdateTestLevelSetParameter.Old));
            Assert.AreEqual(entities[0].Name.ToDefaultString(), tuple.client.UpdateTestLevelSetParameter.Comment);
            Assert.AreEqual(entities[1].Id.ToLong(), tuple.client.UpdateTestLevelSetParameter.UserId);
        }

        [TestCase("sdfzhmküvtjzh")]
        [TestCase("fhdjskldfgh8uj4")]
        public void IsTestLevelSetNameUniquePassesDataToClient(string name)
        {
            var tuple = CreateDataAccessTuple();
            tuple.dataAccess.IsTestLevelSetNameUnique(name);
            Assert.AreEqual(name, tuple.client.IsTestLevelSetNameUniqueParameter.Value);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsTestLevelSetNameUniqueReturnsDataFromClient(bool isUnique)
        {
            var tuple = CreateDataAccessTuple();
            tuple.client.IsTestLevelSetNameUniqueReturnValue = new Bool() { Value = isUnique };
            var result = tuple.dataAccess.IsTestLevelSetNameUnique("");
            Assert.AreEqual(isUnique, result);
        }

        [TestCaseSource(nameof(CreateAnonymousTestLevelSets))]
        public void DoesTestLevelSetHaveReferencesPassesDataToClient(TestLevelSet entity)
        {
            var tuple = CreateDataAccessTuple();
            tuple.dataAccess.DoesTestLevelSetHaveReferences(entity);
            Assert.IsTrue(AreTestLevelSetEntityAndDtoEqual(entity, tuple.client.DoesTestLevelSetHaveReferencesPartameter));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void DoesTestLevelSetHaveReferencesReturnsDataFromClient(bool hasReferences)
        {
            var tuple = CreateDataAccessTuple();
            tuple.client.DoesTestLevelSetHaveReferencesReturnValue = new Bool() { Value = hasReferences };
            var result = tuple.dataAccess.DoesTestLevelSetHaveReferences(CreateAnonymousTestLevelSets().ToList()[0]);
            Assert.AreEqual(hasReferences, result);
        }


        private bool AreTestLevelSetEntityAndDtoEqual(TestLevelSet entity, DtoTypes.TestLevelSet dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   entity.Name.ToDefaultString() == dto.Name &&
                   entity.TestLevel1.Id.ToLong() == dto.TestLevel1.Id &&
                   entity.TestLevel1.TestInterval.IntervalValue == dto.TestLevel1.TestInterval.IntervalValue &&
                   (int)entity.TestLevel1.TestInterval.Type == dto.TestLevel1.TestInterval.IntervalType &&
                   entity.TestLevel1.SampleNumber == dto.TestLevel1.SampleNumber &&
                   entity.TestLevel1.ConsiderWorkingCalendar == dto.TestLevel1.ConsiderWorkingCalendar &&
                   entity.TestLevel1.IsActive == dto.TestLevel1.IsActive &&
                   entity.TestLevel2.Id.ToLong() == dto.TestLevel2.Id &&
                   entity.TestLevel2.TestInterval.IntervalValue == dto.TestLevel2.TestInterval.IntervalValue &&
                   (int)entity.TestLevel2.TestInterval.Type == dto.TestLevel2.TestInterval.IntervalType &&
                   entity.TestLevel2.SampleNumber == dto.TestLevel2.SampleNumber &&
                   entity.TestLevel2.ConsiderWorkingCalendar == dto.TestLevel2.ConsiderWorkingCalendar &&
                   entity.TestLevel2.IsActive == dto.TestLevel2.IsActive &&
                   entity.TestLevel3.Id.ToLong() == dto.TestLevel3.Id &&
                   entity.TestLevel3.TestInterval.IntervalValue == dto.TestLevel3.TestInterval.IntervalValue &&
                   (int)entity.TestLevel3.TestInterval.Type == dto.TestLevel3.TestInterval.IntervalType &&
                   entity.TestLevel3.SampleNumber == dto.TestLevel3.SampleNumber &&
                   entity.TestLevel3.ConsiderWorkingCalendar == dto.TestLevel3.ConsiderWorkingCalendar &&
                   entity.TestLevel3.IsActive == dto.TestLevel3.IsActive;
        }

        private static IEnumerable<DtoTypes.TestLevelSet> CreateAnonymousTestLevelSetDtos()
        {
            foreach (var list in CreateAnonymousListOfTestLevelSets())
            {
                foreach (var testLevelSet in list.TestLevelSets)
                {
                    yield return testLevelSet;
                }
            }
        }

        private static IEnumerable<ListOfTestLevelSets> CreateAnonymousListOfTestLevelSets()
        {
            yield return new ListOfTestLevelSets()
            {
                TestLevelSets =
                {
                    new DtoTypes.TestLevelSet()
                    {
                        Id = 987,
                        Name = "gzsdga bnh",
                        TestLevel1 = new DtoTypes.TestLevel()
                        {
                            Id = 321,
                            ConsiderWorkingCalendar = true,
                            IsActive = false,
                            SampleNumber = 3456,
                            TestInterval = new BasicTypes.Interval()
                            {
                                IntervalValue = 357,
                                IntervalType = 3
                            }
                        },
                        TestLevel2 = new DtoTypes.TestLevel()
                        {
                            Id = 95,
                            ConsiderWorkingCalendar = false,
                            IsActive = false,
                            SampleNumber = 345,
                            TestInterval = new BasicTypes.Interval()
                            {
                                IntervalValue = 65,
                                IntervalType = 5
                            }
                        },
                        TestLevel3 = new DtoTypes.TestLevel()
                        {
                            Id = 12145,
                            ConsiderWorkingCalendar = true,
                            IsActive = false,
                            SampleNumber = 3456,
                            TestInterval = new BasicTypes.Interval()
                            {
                                IntervalValue = 357,
                                IntervalType = 1
                            }
                        }
                    },
                    new DtoTypes.TestLevelSet()
                    {
                        Id = 852,
                        Name = "glkdbhnjkl",
                        TestLevel1 = new DtoTypes.TestLevel()
                        {
                            Id = 852,
                            ConsiderWorkingCalendar = false,
                            IsActive = true,
                            SampleNumber = 74120,
                            TestInterval = new BasicTypes.Interval()
                            {
                                IntervalValue = 290,
                                IntervalType = 0
                            }
                        },
                        TestLevel2 = new DtoTypes.TestLevel()
                        {
                            Id = 5678,
                            ConsiderWorkingCalendar = false,
                            IsActive = false,
                            SampleNumber = 345,
                            TestInterval = new BasicTypes.Interval()
                            {
                                IntervalValue = 37,
                                IntervalType = 2
                            }
                        },
                        TestLevel3 = new DtoTypes.TestLevel()
                        {
                            Id = 327,
                            ConsiderWorkingCalendar = true,
                            IsActive = true,
                            SampleNumber = 3456,
                            TestInterval = new BasicTypes.Interval()
                            {
                                IntervalValue = 59,
                                IntervalType = 4
                            }
                        }
                    }
                }
            };
            yield return new ListOfTestLevelSets()
            {
                TestLevelSets =
                {
                    new DtoTypes.TestLevelSet()
                    {
                        Id = 852,
                        Name = "hetdfg",
                        TestLevel1 = new DtoTypes.TestLevel()
                        {
                            Id = 852,
                            ConsiderWorkingCalendar = false,
                            IsActive = true,
                            SampleNumber = 74120,
                            TestInterval = new BasicTypes.Interval()
                            {
                                IntervalValue = 290,
                                IntervalType = 0
                            }
                        },
                        TestLevel2 = new DtoTypes.TestLevel()
                        {
                            Id = 234,
                            ConsiderWorkingCalendar = true,
                            IsActive = false,
                            SampleNumber = 345,
                            TestInterval = new BasicTypes.Interval()
                            {
                                IntervalValue = 37,
                                IntervalType = 2
                            }
                        },
                        TestLevel3 = new DtoTypes.TestLevel()
                        {
                            Id = 1,
                            ConsiderWorkingCalendar = true,
                            IsActive = true,
                            SampleNumber = 5,
                            TestInterval = new BasicTypes.Interval()
                            {
                                IntervalValue = 59,
                                IntervalType = 4
                            }
                        }
                    }
                }
            };
        }

        private static IEnumerable<List<TestLevelSet>> CreateAnonymousTestLevelSetLists()
        {
            var entities = CreateAnonymousTestLevelSets().ToList();
            yield return new List<TestLevelSet>() { entities[0], entities[1] };
            yield return new List<TestLevelSet>() { entities[1], entities[0] };
        }

        private static IEnumerable<TestLevelSet> CreateAnonymousTestLevelSets()
        {
            yield return new TestLevelSet()
            {
                Id = new TestLevelSetId(987),
                Name = new TestLevelSetName("gzsdga bnh"),
                TestLevel1 = new TestLevel()
                {
                    Id = new TestLevelId(321),
                    ConsiderWorkingCalendar = true,
                    IsActive = false,
                    SampleNumber = 3456,
                    TestInterval = new Core.Entities.Interval()
                    {
                        IntervalValue = 357,
                        Type = IntervalType.XTimesADay
                    }
                },
                TestLevel2 = new TestLevel()
                {
                    Id = new TestLevelId(95),
                    ConsiderWorkingCalendar = false,
                    IsActive = false,
                    SampleNumber = 345,
                    TestInterval = new Core.Entities.Interval()
                    {
                        IntervalValue = 65,
                        Type = IntervalType.XTimesAShift
                    }
                },
                TestLevel3 = new TestLevel()
                {
                    Id = new TestLevelId(12145),
                    ConsiderWorkingCalendar = true,
                    IsActive = false,
                    SampleNumber = 3456,
                    TestInterval = new Core.Entities.Interval()
                    {
                        IntervalValue = 357,
                        Type = IntervalType.XTimesAWeek
                    }
                }
            };
            yield return new TestLevelSet()
            {
                Id = new TestLevelSetId(852),
                Name = new TestLevelSetName("glkdbhnjkl"),
                TestLevel1 = new TestLevel()
                {
                    Id = new TestLevelId(852),
                    ConsiderWorkingCalendar = false,
                    IsActive = true,
                    SampleNumber = 74120,
                    TestInterval = new Core.Entities.Interval()
                    {
                        IntervalValue = 290,
                        Type = IntervalType.EveryXShifts
                    }
                },
                TestLevel2 = new TestLevel()
                {
                    Id = new TestLevelId(5678),
                    ConsiderWorkingCalendar = false,
                    IsActive = false,
                    SampleNumber = 345,
                    TestInterval = new Core.Entities.Interval()
                    {
                        IntervalValue = 37,
                        Type = IntervalType.XTimesAShift
                    }
                },
                TestLevel3 = new TestLevel()
                {
                    Id = new TestLevelId(327),
                    ConsiderWorkingCalendar = true,
                    IsActive = true,
                    SampleNumber = 3456,
                    TestInterval = new Core.Entities.Interval()
                    {
                        IntervalValue = 59,
                        Type = IntervalType.EveryXDays
                    }
                }
            };
        }

        private static (TestLevelSetDataAccess dataAccess, TestLevelClientMock client) CreateDataAccessTuple()
        {
            var client = new TestLevelClientMock();
            var clientFactory = new ClientFactoryMock()
            {
                AuthenticationChannel = new ChannelWrapperMock()
                {
                    GetTestLevelSetClientReturnValue = client
                }
            };
            var dataAccess = new TestLevelSetDataAccess(clientFactory);

            return (dataAccess, client);
        }
    }
}
