using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Core.Entities;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using TestHelper.Checker;
using Interval = Core.Entities.Interval;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    class TestLevelSetUseCaseMock : ITestLevelSetUseCase
    {
        public List<TestLevelSet> LoadTestLevelSetsReturnValue { get; set; }
        public TestLevelSetDiff InsertTestLevelSetParameter { get; set; }
        public TestLevelSet InsertTestLevelSetReturnValue { get; set; }
        public TestLevelSetDiff DeleteTestLevelSetParameter { get; set; }
        public TestLevelSetDiff UpdateTestLevelSetParameter { get; set; }
        public string IsTestLevelSetNameUniqueParameter { get; set; }
        public bool IsTestLevelSetNameUniqueReturnValue { get; set; }
        public TestLevelSet DoesTestLevelSetHaveReferencesParameter { get; set; }
        public bool DoesTestLevelSetHaveReferencesReturnValue { get; set; }

        public List<TestLevelSet> LoadTestLevelSets()
        {
            return LoadTestLevelSetsReturnValue;
        }

        public TestLevelSet InsertTestLevelSet(TestLevelSetDiff diff)
        {
            InsertTestLevelSetParameter = diff;
            return InsertTestLevelSetReturnValue;
        }

        public void DeleteTestLevelSet(TestLevelSetDiff diff)
        {
            DeleteTestLevelSetParameter = diff;
        }

        public void UpdateTestLevelSet(TestLevelSetDiff diff)
        {
            UpdateTestLevelSetParameter = diff;
        }

        public bool IsTestLevelSetNameUnique(string name)
        {
            IsTestLevelSetNameUniqueParameter = name;
            return IsTestLevelSetNameUniqueReturnValue;
        }

        public bool DoesTestLevelSetHaveReferences(TestLevelSet set)
        {
            DoesTestLevelSetHaveReferencesParameter = set;
            return DoesTestLevelSetHaveReferencesReturnValue;
        }
    }


    public class TestLevelSetServiceTest
    {
        [TestCaseSource(nameof(CreateAnonymousTestLevelSetLists))]
        public void LoadTestLevelSetsReturnsDataFromUseCase(List<TestLevelSet> testLevelSetList)
        {
            var tuple = CreateServiceTuple();
            tuple.useCase.LoadTestLevelSetsReturnValue = testLevelSetList;
            var result = tuple.service.LoadTestLevelSets(new NoParams(), null).Result.TestLevelSets;
            CheckerFunctions.CollectionAssertAreEquivalent(testLevelSetList, result, AreTestLevelSetEntityAndDtoEqual);
        }

        [TestCaseSource(nameof(CreateAnonymousTestLevelSetDtos))]
        public void InsertTestLevelSetPassesDataToUseCase(DtoTypes.TestLevelSet dto)
        {
            var tuple = CreateServiceTuple();
            var userId = new System.Random().Next(200);
            tuple.useCase.InsertTestLevelSetReturnValue = CreateAnonymousTestLevelSets().ToList()[0];
            tuple.service.InsertTestLevelSet(new DtoTypes.TestLevelSetDiff() { New = dto, UserId = userId }, null);
            Assert.IsTrue(AreTestLevelSetEntityAndDtoEqual(tuple.useCase.InsertTestLevelSetParameter.New, dto));
            Assert.AreEqual(userId, tuple.useCase.InsertTestLevelSetParameter.User.UserId.ToLong());
        }

        [TestCaseSource(nameof(CreateAnonymousTestLevelSets))]
        public void InsertTestLevelSetReturnsDataFromUseCase(TestLevelSet entity)
        {
            var tuple = CreateServiceTuple();
            tuple.useCase.InsertTestLevelSetReturnValue = entity;
            var result = tuple.service.InsertTestLevelSet(new DtoTypes.TestLevelSetDiff() { New = CreateAnonymousTestLevelSetDtos().ToList()[0] }, null).Result;
            Assert.IsTrue(AreTestLevelSetEntityAndDtoEqual(entity, result));
        }

        [TestCaseSource(nameof(CreateAnonymousTestLevelSetDtos))]
        public void DeleteTestLevelSetPassesDataToUseCase(DtoTypes.TestLevelSet dto)
        {
            var tuple = CreateServiceTuple();
            var userId = new System.Random().Next(200);
            tuple.service.DeleteTestLevelSet(new DtoTypes.TestLevelSetDiff() { Old = dto, UserId = userId }, null);
            Assert.IsTrue(AreTestLevelSetEntityAndDtoEqual(tuple.useCase.DeleteTestLevelSetParameter.Old, dto));
            Assert.AreEqual(userId, tuple.useCase.DeleteTestLevelSetParameter.User.UserId.ToLong());
        }

        [TestCaseSource(nameof(CreateAnonymousTestLevelSetDtoLists))]
        public void UpdateTestLevelSetPassesDataToUseCase(List<DtoTypes.TestLevelSet> dto)
        {
            var tuple = CreateServiceTuple();
            tuple.service.UpdateTestLevelSet(new DtoTypes.TestLevelSetDiff() { New = dto[0], Old = dto[1], Comment = dto[0].Name, UserId = dto[1].Id }, null);
            Assert.IsTrue(AreTestLevelSetEntityAndDtoEqual(tuple.useCase.UpdateTestLevelSetParameter.New, dto[0]));
            Assert.IsTrue(AreTestLevelSetEntityAndDtoEqual(tuple.useCase.UpdateTestLevelSetParameter.Old, dto[1]));
            Assert.AreEqual(dto[0].Name, tuple.useCase.UpdateTestLevelSetParameter.Comment.ToDefaultString());
            Assert.AreEqual(dto[1].Id, tuple.useCase.UpdateTestLevelSetParameter.User.UserId.ToLong());
        }

        [TestCase("zgzermkopü,ldi")]
        [TestCase("ghvfnodpfkpojgi")]
        public void IsTestLevelSetNameUniquePassesDataToUseCase(string name)
        {
            var tuple = CreateServiceTuple();
            tuple.service.IsTestLevelSetNameUnique(new StringResponse() { Value = name }, null);
            Assert.AreEqual(name, tuple.useCase.IsTestLevelSetNameUniqueParameter);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsTestLevelSetNameUniqueReturnsDataFromUseCase(bool isNameUnique)
        {
            var tuple = CreateServiceTuple();
            tuple.useCase.IsTestLevelSetNameUniqueReturnValue = isNameUnique;
            var result = tuple.service.IsTestLevelSetNameUnique(new StringResponse(), null).Result;
            Assert.AreEqual(isNameUnique, result.Value);
        }

        [TestCaseSource(nameof(CreateAnonymousTestLevelSetDtos))]
        public void DoesTestLevelSetHaveReferencesPassesDataToUseCase(DtoTypes.TestLevelSet dto)
        {
            var tuple = CreateServiceTuple();
            tuple.service.DoesTestLevelSetHaveReferences(dto, null);
            Assert.IsTrue(AreTestLevelSetEntityAndDtoEqual(tuple.useCase.DoesTestLevelSetHaveReferencesParameter, dto));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void DoesTestLevelSetHaveReferencesReturnsDataFromUseCase(bool hasReferences)
        {
            var tuple = CreateServiceTuple();
            tuple.useCase.DoesTestLevelSetHaveReferencesReturnValue = hasReferences;
            var result = tuple.service.DoesTestLevelSetHaveReferences(CreateAnonymousTestLevelSetDtos().ToList()[0], null).Result;
            Assert.AreEqual(hasReferences, result.Value);
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

        private static IEnumerable<TestLevelSet> CreateAnonymousTestLevelSets()
        {
            foreach (var list in CreateAnonymousTestLevelSetLists())
            {
                foreach (var testLevelSet in list)
                {
                    yield return testLevelSet;
                }
            }
        }

        private static IEnumerable<List<TestLevelSet>> CreateAnonymousTestLevelSetLists()
        {
            yield return new List<TestLevelSet>()
            {
                new TestLevelSet()
                {
                    Id = new TestLevelSetId(987),
                    Name = new TestLevelSetName("gzsdga bnh"),
                    TestLevel1 = new TestLevel()
                    {
                        Id = new TestLevelId(321),
                        ConsiderWorkingCalendar = true,
                        IsActive = false,
                        SampleNumber = 3456,
                        TestInterval = new Interval()
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
                        TestInterval = new Interval()
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
                        TestInterval = new Interval()
                        {
                            IntervalValue = 357,
                            Type = IntervalType.XTimesAWeek
                        }
                    }
                },
                new TestLevelSet()
                {
                    Id = new TestLevelSetId(852),
                    Name = new TestLevelSetName("glkdbhnjkl"),
                    TestLevel1 = new TestLevel()
                    {
                        Id = new TestLevelId(852),
                        ConsiderWorkingCalendar = false,
                        IsActive = true,
                        SampleNumber = 74120,
                        TestInterval = new Interval()
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
                        TestInterval = new Interval()
                        {
                            IntervalValue = 37,
                            Type = IntervalType.XTimesAWeek
                        }
                    },
                    TestLevel3 = new TestLevel()
                    {
                        Id = new TestLevelId(327),
                        ConsiderWorkingCalendar = true,
                        IsActive = true,
                        SampleNumber = 3456,
                        TestInterval = new Interval()
                        {
                            IntervalValue = 59,
                            Type = IntervalType.EveryXDays
                        }
                    }
                }
            };
            yield return new List<TestLevelSet>()
            {
                new TestLevelSet()
                {
                    Id = new TestLevelSetId(098),
                    Name = new TestLevelSetName(".,tm 0e9w8abn"),
                    TestLevel1 = new TestLevel()
                    {
                        Id = new TestLevelId(42),
                        ConsiderWorkingCalendar = false,
                        IsActive = false,
                        SampleNumber = 456,
                        TestInterval = new Interval()
                        {
                            IntervalValue = 67,
                            Type = IntervalType.EveryXShifts
                        }
                    },
                    TestLevel2 = new TestLevel()
                    {
                        Id = new TestLevelId(45),
                        ConsiderWorkingCalendar = true,
                        IsActive = false,
                        SampleNumber = 379845,
                        TestInterval = new Interval()
                        {
                            IntervalValue = 786,
                            Type = IntervalType.XTimesADay
                        }
                    },
                    TestLevel3 = new TestLevel()
                    {
                        Id = new TestLevelId(12145),
                        ConsiderWorkingCalendar = true,
                        IsActive = false,
                        SampleNumber = 3546,
                        TestInterval = new Interval()
                        {
                            IntervalValue = 35,
                            Type = IntervalType.XTimesAWeek
                        }
                    }
                }
            };
        }

        private static IEnumerable<List<DtoTypes.TestLevelSet>> CreateAnonymousTestLevelSetDtoLists()
        {
            var dtos = CreateAnonymousTestLevelSetDtos().ToList();
            yield return new List<DtoTypes.TestLevelSet>() { dtos[0], dtos[1] };
            yield return new List<DtoTypes.TestLevelSet>() { dtos[1], dtos[0] };
        }

        private static IEnumerable<DtoTypes.TestLevelSet> CreateAnonymousTestLevelSetDtos()
        {
            yield return new DtoTypes.TestLevelSet()
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
            };
            yield return new DtoTypes.TestLevelSet()
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
            };
        }

        private static (NetworkView.Services.TestLevelSetService service, TestLevelSetUseCaseMock useCase) CreateServiceTuple()
        {
            var useCase = new TestLevelSetUseCaseMock();
            var service = new NetworkView.Services.TestLevelSetService(null, useCase);
            return (service, useCase);
        }
    }
}
