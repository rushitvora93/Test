using System;
using System.Collections.Generic;
using BasicTypes;
using Core.Entities;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using Server.TestHelper.Factories;
using Server.UseCases.UseCases;
using TestHelper.Checker;
using ToleranceClassService;
using DateTime = System.DateTime;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    public class ToleranceClassUseCaseMock : IToleranceClassUseCase
    {
        public List<ToleranceClass> LoadToleranceClasses()
        {
            LoadToleranceClassesCalled = true;
            return LoadToleranceClassesReturnValue;
        }

        public List<LocationReferenceLink> GetToleranceClassLocationLinks(ToleranceClassId classId)
        {
            GetToleranceClassLocationLinksParameter = classId;
            return GetToleranceClassLocationLinksReturnValue;
        }

        public List<ToleranceClass> InsertToleranceClassesWithHistory(List<ToleranceClassDiff> toleranceClassDiffs, bool returnList)
        {
            InsertToleranceClassesWithHistoryParameterDiffs = toleranceClassDiffs;
            InsertToleranceClassesWithHistoryParameterReturnList = returnList;
            return InsertToleranceClassesWithHistoryReturnValue;
        }

        public List<ToleranceClass> UpdateToleranceClassesWithHistory(List<ToleranceClassDiff> toleranceClassDiffs)
        {
            UpdateToleranceClassesWithHistoryParameter = toleranceClassDiffs;
            return UpdateToleranceClassesWithHistoryReturnValue;
        }

        public List<LocationToolAssignmentId> GetToleranceClassLocationToolAssignmentLinks(ToleranceClassId toleranceClassId)
        {
            GetToleranceClassLocationToolAssignmentLinksParameter = toleranceClassId;
            return GetToleranceClassLocationToolAssignmentLinksReturnValue;
        }

        public Dictionary<long, ToleranceClass> GetToleranceClassesFromHistoryForIds(List<Tuple<long, long, DateTime>> idsWithClassData)
        {
            GetToleranceClassesFromHistoryForIdsParameter = idsWithClassData;
            return GetToleranceClassesFromHistoryForIdsReturnValue;
        }

        public List<ToleranceClass> LoadToleranceClassesReturnValue { get; set; } = new List<ToleranceClass>();
        public bool LoadToleranceClassesCalled { get; set; }
        public List<LocationReferenceLink> GetToleranceClassLocationLinksReturnValue { get; set; } = new List<LocationReferenceLink>();
        public ToleranceClassId GetToleranceClassLocationLinksParameter { get; set; }
        public List<ToleranceClass> InsertToleranceClassesWithHistoryReturnValue { get; set; } = new List<ToleranceClass>();
        public bool InsertToleranceClassesWithHistoryParameterReturnList { get; set; }
        public List<ToleranceClassDiff> InsertToleranceClassesWithHistoryParameterDiffs { get; set; } = new List<ToleranceClassDiff>();
        public List<ToleranceClass> UpdateToleranceClassesWithHistoryReturnValue { get; set; } = new List<ToleranceClass>();
        public List<ToleranceClassDiff> UpdateToleranceClassesWithHistoryParameter { get; set; } = new List<ToleranceClassDiff>();
        public List<LocationToolAssignmentId> GetToleranceClassLocationToolAssignmentLinksReturnValue { get; set; } = new List<LocationToolAssignmentId>();
        public ToleranceClassId GetToleranceClassLocationToolAssignmentLinksParameter { get; set; }
        public Dictionary<long, ToleranceClass> GetToleranceClassesFromHistoryForIdsReturnValue { get; set; } = new Dictionary<long, ToleranceClass>();
        public List<Tuple<long, long, DateTime>> GetToleranceClassesFromHistoryForIdsParameter { get; set; }
    }

    public class ToleranceClassServiceTest
    {
        [Test]
        public void LoadToleranceClassesCallsUseCase()
        {
            var useCase = new ToleranceClassUseCaseMock();
            var service = new NetworkView.Services.ToleranceClassService(null, useCase);

            service.LoadToleranceClasses(new NoParams(), null);

            Assert.IsTrue(useCase.LoadToleranceClassesCalled);
        }

        private static IEnumerable<List<ToleranceClass>> toleranceClassData = new List<List<ToleranceClass>>()
        {
            new List<ToleranceClass>()
            {
                CreateToleranceClass.Parametrized(1, "name", true, 0.0, 1.1),
                CreateToleranceClass.Parametrized(2, "freie eingabe", false, 0.0, 0.0)
            },
            new List<ToleranceClass>()
            {
                CreateToleranceClass.Parametrized(99, "Klasse 1", true, 9.9, 12.1)
            }
        };

        [TestCaseSource(nameof(toleranceClassData))]
        public void LoadToleranceClassesReturnsCorrectValue(List<ToleranceClass> toleranceClasses)
        {
            var useCase = new ToleranceClassUseCaseMock();
            useCase.LoadToleranceClassesReturnValue = toleranceClasses;
            var service = new NetworkView.Services.ToleranceClassService(null, useCase);

            var result = service.LoadToleranceClasses(new NoParams(), null);

            var comparer = new Func<ToleranceClass, DtoTypes.ToleranceClass, bool>((toleranceClass, dtotoleranceClass) =>
                EqualityChecker.CompareToleranceClassDtoWithToleranceClass(dtotoleranceClass, toleranceClass)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(toleranceClasses, result.Result.ToleranceClasses, comparer);
        }

        [TestCase(1)]
        [TestCase(99)]
        public void GetToleranceClassLocationLinksCallsUseCase(long classId)
        {
            var useCase = new ToleranceClassUseCaseMock();
            var service = new NetworkView.Services.ToleranceClassService(null, useCase);

            service.GetToleranceClassLocationLinks(new LongRequest() { Value = classId }, null);

            Assert.AreEqual(classId, useCase.GetToleranceClassLocationLinksParameter.ToLong());
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
        public void GetToleranceClassLocationLinksReturnsCorrectValue(List<LocationReferenceLink> locationReferenceLink)
        {
            var useCase = new ToleranceClassUseCaseMock();
            var service = new NetworkView.Services.ToleranceClassService(null, useCase);
            useCase.GetToleranceClassLocationLinksReturnValue = locationReferenceLink;

            var result = service.GetToleranceClassLocationLinks(new LongRequest(), null);

            var comparer = new Func<LocationReferenceLink, DtoTypes.LocationLink, bool>((locationLink, dtoLocationLink) =>
                locationLink.Id.ToLong() == dtoLocationLink.Id &&
                locationLink.Description == dtoLocationLink.Description &&
                locationLink.Number == dtoLocationLink.Number
            );

            CheckerFunctions.CollectionAssertAreEquivalent(locationReferenceLink, result.Result.LocationLinks, comparer);
        }

        static IEnumerable<(ListOfToleranceClassesDiffs, bool)> InsertUpdateToleranceClassWithHistoryData = new List<(ListOfToleranceClassesDiffs, bool)>
        {
            (
                new ListOfToleranceClassesDiffs()
                {
                    ToleranceClassesDiff =
                    {
                        new DtoTypes.ToleranceClassDiff()
                        {
                            UserId = 1,
                            Comment = "",
                            OldToleranceClass = DtoFactory.CreateToleranceClassDto(1, "freie Eingabe", false, 0.0, 0.0, true),
                            NewToleranceClass = DtoFactory. CreateToleranceClassDto(1, "freie Eingabe", true, 0.0, 0.0, false),
                        },
                        new DtoTypes.ToleranceClassDiff()
                        {
                            UserId = 2,
                            Comment = "ABCDEFG",
                            OldToleranceClass = DtoFactory.CreateToleranceClassDto(1, "Klasse 1", true, 0.1, 1.0, true),
                            NewToleranceClass = DtoFactory.CreateToleranceClassDto(1, "Klasse 100", false, 10.0, 110.10, true),
                        }
                    }
                },
                true
             ),
            (
                new ListOfToleranceClassesDiffs()
                {
                    ToleranceClassesDiff =
                    {
                        new DtoTypes.ToleranceClassDiff()
                        {
                            UserId = 9,
                            Comment = "04359 435646",
                            OldToleranceClass = DtoFactory.CreateToleranceClassDto(12, "freie Eingabe X", false, 1.0, 2.0, true),
                            NewToleranceClass = DtoFactory.CreateToleranceClassDto(12, "freie Eingabe", true, 0.0, 0.0, true),
                        }
                    }
                },
                false
            )
        };

        [TestCaseSource(nameof(InsertUpdateToleranceClassWithHistoryData))]
        public void InsertToleranceClassesWithHistoryCallsUseCase((ListOfToleranceClassesDiffs diffs, bool returnList) data)
        {
            var useCase = new ToleranceClassUseCaseMock();
            var service = new NetworkView.Services.ToleranceClassService(null, useCase);

            var request = new InsertToleranceClassesWithHistoryRequest()
            {
                ToleranceClassesDiffs = data.diffs,
                ReturnList = data.returnList
            };

            service.InsertToleranceClassesWithHistory(request, null);

            Assert.AreEqual(data.returnList, useCase.InsertToleranceClassesWithHistoryParameterReturnList);

            var comparer = new Func<DtoTypes.ToleranceClassDiff, ToleranceClassDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.User.UserId.ToLong() &&
                dtoDiff.Comment == diff.Comment.ToDefaultString() &&
                EqualityChecker.CompareToleranceClassDtoWithToleranceClass(dtoDiff.OldToleranceClass, diff.OldToleranceClass) &&
                EqualityChecker.CompareToleranceClassDtoWithToleranceClass(dtoDiff.NewToleranceClass, diff.NewToleranceClass)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.diffs.ToleranceClassesDiff, useCase.InsertToleranceClassesWithHistoryParameterDiffs, comparer);
        }

        [TestCaseSource(nameof(toleranceClassData))]
        public void InsertToleranceClassesWithHistoryReturnsCorrectValue(List<ToleranceClass> toleranceClasses)
        {
            var useCase = new ToleranceClassUseCaseMock();
            var service = new NetworkView.Services.ToleranceClassService(null, useCase);

            useCase.InsertToleranceClassesWithHistoryReturnValue = toleranceClasses;

            var request = new InsertToleranceClassesWithHistoryRequest()
            {
                ToleranceClassesDiffs = new ListOfToleranceClassesDiffs()
            };

            var result = service.InsertToleranceClassesWithHistory(request, null).Result;

            var comparer = new Func<ToleranceClass, DtoTypes.ToleranceClass, bool>((tol, tolDto) =>
                EqualityChecker.CompareToleranceClassDtoWithToleranceClass(tolDto, tol)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(toleranceClasses, result.ToleranceClasses, comparer);
        }

        [TestCaseSource(nameof(InsertUpdateToleranceClassWithHistoryData))]
        public void UpdateToleranceClassesWithHistoryCallsUseCase((ListOfToleranceClassesDiffs toleranceClassDiff, bool returnList) data)
        {
            var useCase = new ToleranceClassUseCaseMock();
            var service = new NetworkView.Services.ToleranceClassService(null, useCase);

            var request = new UpdateToleranceClassesWithHistoryRequest()
            {
                ToleranceClassesDiffs = data.toleranceClassDiff
            };

            service.UpdateToleranceClassesWithHistory(request, null);

            var comparer = new Func<DtoTypes.ToleranceClassDiff, ToleranceClassDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.User.UserId.ToLong() &&
                dtoDiff.Comment == diff.Comment.ToDefaultString() &&
                EqualityChecker.CompareToleranceClassDtoWithToleranceClass(dtoDiff.OldToleranceClass, diff.OldToleranceClass) &&
                EqualityChecker.CompareToleranceClassDtoWithToleranceClass(dtoDiff.NewToleranceClass, diff.NewToleranceClass)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.toleranceClassDiff.ToleranceClassesDiff, useCase.UpdateToleranceClassesWithHistoryParameter, comparer);
        }

        [TestCaseSource(nameof(toleranceClassData))]
        public void UpdateToleranceClassesWithHistoryReturnsCorrectValue(List<ToleranceClass> toleranceClasses)
        {
            var useCase = new ToleranceClassUseCaseMock();
            var service = new NetworkView.Services.ToleranceClassService(null, useCase);

            useCase.UpdateToleranceClassesWithHistoryReturnValue = toleranceClasses;

            var request = new UpdateToleranceClassesWithHistoryRequest()
            {
                ToleranceClassesDiffs = new ListOfToleranceClassesDiffs()
            };

            var result = service.UpdateToleranceClassesWithHistory(request, null).Result;

            var comparer = new Func<ToleranceClass, DtoTypes.ToleranceClass, bool>((tol, tolDto) =>
                EqualityChecker.CompareToleranceClassDtoWithToleranceClass(tolDto, tol)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(toleranceClasses, result.ToleranceClasses, comparer);
        }

        [TestCase(1)]
        [TestCase(99)]
        public void GetToleranceClassToolLinksCallsUseCase(long classId)
        {
            var useCase = new ToleranceClassUseCaseMock();
            var service = new NetworkView.Services.ToleranceClassService(null, useCase);

            service.GetToleranceClassLocationToolAssignmentLinks(new LongRequest() { Value = classId }, null);

            Assert.AreEqual(classId, useCase.GetToleranceClassLocationToolAssignmentLinksParameter.ToLong());
        }

        static IEnumerable<List<LocationToolAssignmentId>> GetToleranceClassToolLinksData = new List<List<LocationToolAssignmentId>>()
        {
            new List<LocationToolAssignmentId>{new LocationToolAssignmentId(1),new LocationToolAssignmentId(2),new LocationToolAssignmentId(3)},
            new List<LocationToolAssignmentId>{ new LocationToolAssignmentId(4), new LocationToolAssignmentId(5), new LocationToolAssignmentId(6)}
        };

        [TestCaseSource(nameof(GetToleranceClassToolLinksData))]
        public void GetToleranceClassToolLinksReturnsCorrectValue(List<LocationToolAssignmentId> toolLinksIds)
        {
            var useCase = new ToleranceClassUseCaseMock();
            useCase.GetToleranceClassLocationToolAssignmentLinksReturnValue = toolLinksIds;
            var service = new NetworkView.Services.ToleranceClassService(null, useCase);

            var result = service.GetToleranceClassLocationToolAssignmentLinks(new LongRequest(), null);

            var comparer = new Func<LocationToolAssignmentId, Long, bool>((val, dtoVal) =>
                val.ToLong() == dtoVal.Value
            );

            CheckerFunctions.CollectionAssertAreEquivalent(toolLinksIds, result.Result.Values, comparer);
        }

        static IEnumerable<List<Tuple<long, long, DateTime>>> GetToleranceClassesFromHistoryForIdsCallsUseCaseData = new List<List<Tuple<long, long, DateTime>>>()
        {
            new List<Tuple<long, long, DateTime>>()
            {
                new Tuple<long, long, DateTime>(1,2, new DateTime(2020,10,1,1,10,1)),
                new Tuple<long, long, DateTime>(2,6, new DateTime(2020,7,16,15,17,10))
            },
            new List<Tuple<long, long, DateTime>>()
            {
                new Tuple<long, long, DateTime>(4,62, new DateTime(2019,1,9,9,10,15))
            }
        };

        [TestCaseSource(nameof(GetToleranceClassesFromHistoryForIdsCallsUseCaseData))]
        public void GetToleranceClassesFromHistoryForIdsCallsUseCase(List<Tuple<long, long, DateTime>> datas)
        {
            var useCase = new ToleranceClassUseCaseMock();
            var service = new NetworkView.Services.ToleranceClassService(null, useCase);
            var toleranceClassDatas = new ListOfToleranceClassFromHistoryParameter();

            foreach (var data in datas)
            {
                toleranceClassDatas.Parameters.Add(new ToleranceClassFromHistoryParameter()
                {
                    GlobalHistoryId = data.Item1,
                    ToleranceClassId = data.Item2,
                    Timestamp = new BasicTypes.DateTime(){Ticks = data.Item3.Ticks}
                });
            }
            service.GetToleranceClassesFromHistoryForIds(toleranceClassDatas, null);

            Assert.AreEqual(datas, useCase.GetToleranceClassesFromHistoryForIdsParameter);
        }

        static IEnumerable<Dictionary<long, ToleranceClass>>
            GetToleranceClassesFromHistoryForIdsReturnsCorrectValueData = new List<Dictionary<long, ToleranceClass>>()
            {
                new Dictionary<long, ToleranceClass>()
                {
                    { 1, CreateToleranceClass.Parametrized(1, "test", true, 1, 10)},
                    { 19, CreateToleranceClass.Parametrized(167, "class 1", false, 1.5, 15.0)},
                },
                new Dictionary<long, ToleranceClass>()
                {
                    { 199, CreateToleranceClass.Parametrized(1637, "t 1", false, 9.5, 155.0)},
                }
            };

        [TestCaseSource(nameof(GetToleranceClassesFromHistoryForIdsReturnsCorrectValueData))]
        public void GetToleranceClassesFromHistoryForIdsReturnsCorrectValue(Dictionary<long, ToleranceClass> datas)
        {
            var useCase = new ToleranceClassUseCaseMock();
            useCase.GetToleranceClassesFromHistoryForIdsReturnValue = datas;
            var service = new NetworkView.Services.ToleranceClassService(null, useCase);

            var result = service.GetToleranceClassesFromHistoryForIds(new ListOfToleranceClassFromHistoryParameter(), null);

            Assert.AreEqual(datas.Count, result.Result.Datas.Count);
            foreach (var data in result.Result.Datas)
            {
                Assert.IsTrue(EqualityChecker.CompareToleranceClassDtoWithToleranceClass(data.ToleranceClass, datas[data.GlobalHistoryId]));
            }
        }
    }
}
