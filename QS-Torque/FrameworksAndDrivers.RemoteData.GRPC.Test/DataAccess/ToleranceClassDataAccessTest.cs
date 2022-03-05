using BasicTypes;
using Client.TestHelper.Mock;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.ReferenceLink;
using DtoTypes;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;
using ToleranceClassService;
using DateTime = System.DateTime;
using ToleranceClass = DtoTypes.ToleranceClass;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    public class ToleranceClassClientMock : IToleranceClassClient
    {
        public bool LoadToleranceClassesCalled { get; set; }
        public ListOfToleranceClasses LoadToleranceClassesReturnValue { get; set; } = new ListOfToleranceClasses();
        public ListOfLocationLink GetToleranceClassLocationLinksReturnValue { get; set; } = new ListOfLocationLink();
        public LongRequest GetToleranceClassLocationLinksParameter { get; set; }
        public ListOfToleranceClasses InsertToleranceClassesWithHistoryReturnValue { get; set; } = new ListOfToleranceClasses();
        public InsertToleranceClassesWithHistoryRequest InsertToleranceClassesWithHistoryParameter { get; set; }
        public ListOfToleranceClasses UpdateToleranceClassesWithHistoryReturnValue { get; set; } = new ListOfToleranceClasses();
        public UpdateToleranceClassesWithHistoryRequest UpdateToleranceClassesWithHistoryParameter { get; set; }
        public ListOfLongs GetToleranceClassLocationToolAssignmentLinksReturnValue { get; set; } = new ListOfLongs();
        public LongRequest GetToleranceClassLocationToolAssignmentLinksParameter { get; set; }
        public GetToleranceClassesFromHistoryForIdsResponse GetToleranceClassFromHistoryForIdsReturnValue { get; set; } = new GetToleranceClassesFromHistoryForIdsResponse();
        public ListOfToleranceClassFromHistoryParameter GetToleranceClassFromHistoryForIdsParameter { get; set; }

        public ListOfLocationLink GetToleranceClassLocationLinks(LongRequest toleranceClassId)
        {
            GetToleranceClassLocationLinksParameter = toleranceClassId;
            return GetToleranceClassLocationLinksReturnValue;
        }

        public ListOfLongs GetToleranceClassLocationToolAssignmentLinks(LongRequest request)
        {
            GetToleranceClassLocationToolAssignmentLinksParameter = request;
            return GetToleranceClassLocationToolAssignmentLinksReturnValue;
        }

        public GetToleranceClassesFromHistoryForIdsResponse GetToleranceClassFromHistoryForIds(
            ListOfToleranceClassFromHistoryParameter datas)
        {
            GetToleranceClassFromHistoryForIdsParameter = datas;
            return GetToleranceClassFromHistoryForIdsReturnValue;
        }

        public ListOfToleranceClasses InsertToleranceClassesWithHistory(InsertToleranceClassesWithHistoryRequest request)
        {
            InsertToleranceClassesWithHistoryParameter = request;
            return InsertToleranceClassesWithHistoryReturnValue;
        }

        public ListOfToleranceClasses LoadToleranceClasses()
        {
            LoadToleranceClassesCalled = true;
            return LoadToleranceClassesReturnValue;
        }

        public ListOfToleranceClasses UpdateToleranceClassesWithHistory(UpdateToleranceClassesWithHistoryRequest request)
        {
            UpdateToleranceClassesWithHistoryParameter = request;
            return UpdateToleranceClassesWithHistoryReturnValue;
        }
    }

    public class ToleranceClassDataAccessTest
    {
        [Test]
        public void LoadToleranceClassesCallsClient()
        {
            var environment = new Environment();
            environment.dataAccess.LoadToleranceClasses();
            Assert.IsTrue(environment.mocks.toleranceClassClient.LoadToleranceClassesCalled);
        }

        static IEnumerable<ListOfToleranceClasses> LoadToleranceClassesData = new List<ListOfToleranceClasses>()
        {
            new ListOfToleranceClasses()
            {
                ToleranceClasses =
                {
                    DtoFactory.CreateToleranceClass(1, "status1", true, 1.0, 2.0, true),
                    DtoFactory.CreateToleranceClass(2, "statusB", true, 19.0, 22.0, false)
                },
             },
             new ListOfToleranceClasses()
             {
                ToleranceClasses =
                {
                    DtoFactory.CreateToleranceClass(19, "freie Eingabe", false, 0, 0, true)
                }
             }
        };
        

        [TestCaseSource(nameof(LoadToleranceClassesData))]
        public void LoadToleranceClassesReturnsCorrectValue(ListOfToleranceClasses listOfToleranceClass)
        {
            var environment = new Environment();
            environment.mocks.toleranceClassClient.LoadToleranceClassesReturnValue = listOfToleranceClass;
            var result = environment.dataAccess.LoadToleranceClasses();

            var comparer = new Func<DtoTypes.ToleranceClass, Core.Entities.ToleranceClass, bool>((tolDto, tol) =>
                EqualityChecker.CompareToleranceClassWithToleranceClassDto(tol, tolDto)
             );

            CheckerFunctions.CollectionAssertAreEquivalent(listOfToleranceClass.ToleranceClasses, result, comparer);
        }

        [TestCase(1)]
        [TestCase(100)]
        public void LoadReferencedLocationsCallsClient(long classId)
        {
            var environment = new Environment();
            environment.dataAccess.LoadReferencedLocations(new ToleranceClassId(classId));
            Assert.AreEqual(classId, environment.mocks.toleranceClassClient.GetToleranceClassLocationLinksParameter.Value);
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
        public void LoadReferencedLocationsReturnsCorrectValue(ListOfLocationLink locationLinks)
        {
            var environment = new Environment();
            environment.mocks.toleranceClassClient.GetToleranceClassLocationLinksReturnValue = locationLinks;
            var result = environment.dataAccess.LoadReferencedLocations(new ToleranceClassId(1));

            var comparer = new Func<LocationLink, LocationReferenceLink, bool>((locReferenceLinkDto, locReferenceLink) =>
                locReferenceLinkDto.Id == locReferenceLink.Id.ToLong() &&
                locReferenceLinkDto.Number == locReferenceLink.Number.ToDefaultString() &&
                locReferenceLinkDto.Description == locReferenceLink.Description.ToDefaultString()
            );

            CheckerFunctions.CollectionAssertAreEquivalent(locationLinks.LocationLinks, result, comparer);
        }

        [Test]
        public void AddToleranceClassWithoutUserThrowsArgumentException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.AddToleranceClass(null, null); });
        }

        static IEnumerable<(Core.Entities.ToleranceClass, Core.Entities.User)> AddToleranceClassData = new List<(Core.Entities.ToleranceClass, Core.Entities.User)>()
        {
            (
                TestHelper.Factories.CreateToleranceClass.Parametrized(1,"test",true, 0.1, 9.2),
                CreateUser.IdOnly(2)
            ),
            (
                TestHelper.Factories.CreateToleranceClass.Parametrized(9,"freie Eingabe",true, 0, 0),
                CreateUser.IdOnly(2)
            )
        };

        [TestCaseSource(nameof(AddToleranceClassData))]
        public void AddToleranceClassCallsClient((Core.Entities.ToleranceClass toleranceClass, Core.Entities.User user) data)
        {
            var environment = new Environment();
            environment.mocks.toleranceClassClient.InsertToleranceClassesWithHistoryReturnValue =
                new ListOfToleranceClasses() {ToleranceClasses = {new ToleranceClass()}};

            environment.dataAccess.AddToleranceClass(data.toleranceClass, data.user);

            var clientParam = environment.mocks.toleranceClassClient.InsertToleranceClassesWithHistoryParameter;
            var classDiff = clientParam.ToleranceClassesDiffs.ToleranceClassesDiff.First();

            Assert.AreEqual(1, clientParam.ToleranceClassesDiffs.ToleranceClassesDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), classDiff.UserId);
            Assert.AreEqual("", classDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareToleranceClassWithToleranceClassDto(data.toleranceClass, classDiff.NewToleranceClass));
            Assert.IsNull(classDiff.OldToleranceClass);
            Assert.IsTrue(clientParam.ReturnList);
        }

        static IEnumerable<ToleranceClass> ToleranceClassDtoData = new List<ToleranceClass>()
        {
            DtoFactory.CreateToleranceClass(1, "name 1", true, 0.1, 10.2, true),
            DtoFactory.CreateToleranceClass(19, "freie Eingabe 1", false, 0, 0, false),
        };

        [TestCaseSource(nameof(ToleranceClassDtoData))]
        public void AddToleranceClassReturnsCorrectValue(ToleranceClass tolDto)
        {
            var environment = new Environment();
            environment.mocks.toleranceClassClient.InsertToleranceClassesWithHistoryReturnValue =
                new ListOfToleranceClasses() { ToleranceClasses = { tolDto }};
            var result = environment.dataAccess.AddToleranceClass(TestHelper.Factories.CreateToleranceClass.Anonymous(), CreateUser.Anonymous());

            Assert.IsTrue(EqualityChecker.CompareToleranceClassWithToleranceClassDto(result, tolDto));
        }

        [Test]
        public void AddToleranceClassReturnsNullThrowsException()
        {
            var environment = new Environment();
            Assert.Throws<NullReferenceException>(() =>
            {
                environment.dataAccess.AddToleranceClass(TestHelper.Factories.CreateToleranceClass.Anonymous(), CreateUser.Anonymous());
            });
        }

        [Test]
        public void SaveToleranceClassWithMismatchingIdsThrowsArgumentException()
        {
            var environment = new Environment();

            var toleranceClass = new Core.UseCases.ToleranceClassDiff(CreateUser.Anonymous(), null,
                TestHelper.Factories.CreateToleranceClass.WithId(1),
                TestHelper.Factories.CreateToleranceClass.WithId(2));
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.SaveToleranceClass(toleranceClass); });
        }

        static IEnumerable<(Core.Entities.ToleranceClass, Core.Entities.ToleranceClass, Core.Entities.User, string)> SaveAndRemoveToleranceClassData = new List<(Core.Entities.ToleranceClass, Core.Entities.ToleranceClass, Core.Entities.User, string)>()
        {
            (
                TestHelper.Factories.CreateToleranceClass.Parametrized(1, "t1", true, 1.1, 9.9),
                TestHelper.Factories.CreateToleranceClass.Parametrized(1, "t1 X", false, 3.1, 9.6),
                CreateUser.IdOnly(2),
                "Komentar A"
            ),
            (
                TestHelper.Factories.CreateToleranceClass.Parametrized(99, "Klasse A", true, 1.1, 92.9),
                TestHelper.Factories.CreateToleranceClass.Parametrized(99, "Klasse B", false, 1.1, 94.6),
                CreateUser.IdOnly(2),
                "Komentar B"
            )
        };

        [TestCaseSource(nameof(SaveAndRemoveToleranceClassData))]
        public void SaveToleranceClassCallsClient((Core.Entities.ToleranceClass oldClass, Core.Entities.ToleranceClass newClass, Core.Entities.User user, string comment) data)
        {
            var environment = new Environment();

            var toleranceClassDiff = new Core.UseCases.ToleranceClassDiff(data.user,
                new HistoryComment(data.comment), data.oldClass, data.newClass);
            environment.dataAccess.SaveToleranceClass(toleranceClassDiff);

            var clientParam = environment.mocks.toleranceClassClient.UpdateToleranceClassesWithHistoryParameter;
            var classDiff = clientParam.ToleranceClassesDiffs.ToleranceClassesDiff.First();

            Assert.AreEqual(1, clientParam.ToleranceClassesDiffs.ToleranceClassesDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), classDiff.UserId);
            Assert.AreEqual(data.comment, classDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareToleranceClassWithToleranceClassDto(data.newClass, classDiff.NewToleranceClass));
            Assert.IsTrue(EqualityChecker.CompareToleranceClassWithToleranceClassDto(data.oldClass, classDiff.OldToleranceClass));
            Assert.AreEqual(true, classDiff.OldToleranceClass.Alive);
            Assert.AreEqual(true, classDiff.NewToleranceClass.Alive);
        }

        [Test]
        public void SaveToleranceClassReturnsCorrectValue()
        {
            var environment = new Environment();

            var newTol = TestHelper.Factories.CreateToleranceClass.WithId(1);
            var toleranceClass = new Core.UseCases.ToleranceClassDiff(CreateUser.Anonymous(), new HistoryComment(""), 
                TestHelper.Factories.CreateToleranceClass.WithId(1),
                newTol);

            Assert.AreSame(newTol, environment.dataAccess.SaveToleranceClass(toleranceClass));
        }

        [Test]
        public void RemoveToleranceClassWithoutUserThrowsArgumentException()
        {
            var environment = new Environment();
            var toleranceClassDiff = new Core.UseCases.ToleranceClassDiff(null, new HistoryComment(""),
                TestHelper.Factories.CreateToleranceClass.Anonymous(), TestHelper.Factories.CreateToleranceClass.Anonymous());
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.RemoveToleranceClass(toleranceClassDiff); });
        }

        [TestCaseSource(nameof(SaveAndRemoveToleranceClassData))]
        public void RemoveToleranceClassCallsClient((Core.Entities.ToleranceClass oldClass, Core.Entities.ToleranceClass newClass, Core.Entities.User user, string comment) data)
        {
            var environment = new Environment();
            environment.mocks.toleranceClassClient.UpdateToleranceClassesWithHistoryReturnValue = new ListOfToleranceClasses() { ToleranceClasses = { new ToleranceClass() } };
            var toleranceClassDiff = new Core.UseCases.ToleranceClassDiff(data.user, new HistoryComment(data.comment),
                data.oldClass, data.newClass);

            environment.dataAccess.RemoveToleranceClass(toleranceClassDiff);

            var clientParam = environment.mocks.toleranceClassClient.UpdateToleranceClassesWithHistoryParameter;
            var diff = clientParam.ToleranceClassesDiffs.ToleranceClassesDiff.First();

            Assert.AreEqual(1, clientParam.ToleranceClassesDiffs.ToleranceClassesDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), diff.UserId);
            Assert.AreEqual(data.comment, diff.Comment);
            Assert.IsTrue(EqualityChecker.CompareToleranceClassWithToleranceClassDto(data.newClass, diff.NewToleranceClass));
            Assert.IsTrue(EqualityChecker.CompareToleranceClassWithToleranceClassDto(data.oldClass, diff.OldToleranceClass));
            Assert.AreEqual(true, diff.OldToleranceClass.Alive);
            Assert.AreEqual(false, diff.NewToleranceClass.Alive);
        }

        [TestCase(1)]
        [TestCase(100)]
        public void LoadReferencedLocationToolAssignmentsCallsClient(long classId)
        {
            var environment = new Environment();
            environment.dataAccess.LoadReferencedLocationToolAssignments(new ToleranceClassId(classId));
            Assert.AreEqual(classId, environment.mocks.toleranceClassClient.GetToleranceClassLocationToolAssignmentLinksParameter.Value);
        }

        static IEnumerable<ListOfLongs> LoadReferencedLocationToolAssignmentsData = new List<ListOfLongs>()
        {
            new ListOfLongs()
            {
                Values =
                {
                    new Long{Value = 1},
                    new Long{Value = 2},
                    new Long{Value = 3},
                }
            },
            new ListOfLongs()
            {
                Values =
                {
                    new Long{Value = 5},
                    new Long{Value = 7},
                    new Long{Value = 9},
                }
            }
        };

        [TestCaseSource(nameof(LoadReferencedLocationToolAssignmentsData))]
        public void LoadReferencedLocationToolAssignmentsReturnsCorrectValue(ListOfLongs longs)
        {
            var environment = new Environment();
            environment.mocks.toleranceClassClient.GetToleranceClassLocationToolAssignmentLinksReturnValue = longs;
            var result = environment.dataAccess.LoadReferencedLocationToolAssignments(new ToleranceClassId(1));

            var comparer = new Func<long, Long, bool>((val, valDto) =>
                val == valDto.Value
            );

            CheckerFunctions.CollectionAssertAreEquivalent(result.Select(x => x.ToLong()).ToList(), longs.Values, comparer);
        }

        private static IEnumerable<List<Tuple<long, long, DateTime>>> GetToleranceClassData =
            new List<List<Tuple<long, long, DateTime>>>()
            {
                new List<Tuple<long, long, DateTime>>()
                {
                    new Tuple<long, long, DateTime>(1, 2, new DateTime(2020, 10, 1, 10,11, 12)),
                    new Tuple<long, long, DateTime>(4, 5, new DateTime(2019, 11, 3, 5,7, 8))
                },
                new List<Tuple<long, long, DateTime>>()
                {
                    new Tuple<long, long, DateTime>(41, 62, new DateTime(2020, 1, 13, 7,8, 12)),
                }
            };

        [TestCaseSource(nameof(GetToleranceClassData))]
        public void GetToleranceClassFromHistoryForIdsCallsClient(List<Tuple<long, long, DateTime>> datas)
        {
            var environment = new Environment();

            environment.dataAccess.GetToleranceClassFromHistoryForIds(datas);

            var clientParameter =
                environment.mocks.toleranceClassClient.GetToleranceClassFromHistoryForIdsParameter.Parameters.Select(
                    x => new Tuple<long, long, DateTime>(
                        x.GlobalHistoryId, 
                        x.ToleranceClassId,
                        new DateTime(x.Timestamp.Ticks))
                    );

            Assert.AreEqual(datas, clientParameter);           
        }

        static IEnumerable<Dictionary<long, Core.Entities.ToleranceClass>> GetToleranceClassFromHistoryReturnData = new List<Dictionary<long, Core.Entities.ToleranceClass>>()
        {
            new Dictionary<long, Core.Entities.ToleranceClass>()
            {
                {1, TestHelper.Factories.CreateToleranceClass.Parametrized(1,"test",true, 0.1, 9.2)},
                {3, TestHelper.Factories.CreateToleranceClass.Parametrized(7,"tes67t",false, 1.1, 91.2)}
            },
            new Dictionary<long, Core.Entities.ToleranceClass>()
            {
                {99, TestHelper.Factories.CreateToleranceClass.Parametrized(19,"tesjtzu ",true, 6.1, 9.2)},
            }
        };

        [TestCaseSource(nameof(GetToleranceClassFromHistoryReturnData))]
        public void GetToleranceClassFromHistoryForIdsReturnsCorrectValue(Dictionary<long, Core.Entities.ToleranceClass> datas)
        {
            var environment = new Environment();
            var returnValue = new GetToleranceClassesFromHistoryForIdsResponse();
            foreach (var data in datas)
            {
                returnValue.Datas.Add(new GlobalHistoryIdToleranceClassPair()
                {
                    GlobalHistoryId = data.Key,
                    ToleranceClass = GetToleranceClassDtoFromToleranceClass(data.Value)
                });
            }
            environment.mocks.toleranceClassClient.GetToleranceClassFromHistoryForIdsReturnValue = returnValue;
            var result = environment.dataAccess.GetToleranceClassFromHistoryForIds(new List<Tuple<long, long, DateTime>>());

            Assert.AreEqual(datas.Count, result.Count);
            foreach (var data in datas)
            {
                Assert.IsTrue(data.Value.EqualsByContent(result[data.Key]));
            }
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

        private ToleranceClass GetToleranceClassDtoFromToleranceClass(Core.Entities.ToleranceClass tol)
        {
            return new ToleranceClass()
            {
                Id = tol.Id.ToLong(),
                Name = tol.Name,
                Relative = tol.Relative,
                UpperLimit = tol.UpperLimit,
                LowerLimit = tol.LowerLimit
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
                    toleranceClassClient = new ToleranceClassClientMock();
                    channelWrapper.GetToleranceClassClientReturnValue = toleranceClassClient;
                    clientFactory.AuthenticationChannel = channelWrapper;
                    timeDataAccess = new TimeDataAccessMock();
                }
                public ClientFactoryMock clientFactory;
                public ChannelWrapperMock channelWrapper;
                public ToleranceClassClientMock toleranceClassClient;
                public TimeDataAccessMock timeDataAccess;
            }

            public Environment()
            {
                mocks = new Mocks();
                dataAccess = new ToleranceClassDataAccess(mocks.clientFactory, null, mocks.timeDataAccess);
            }

            public readonly Mocks mocks;
            public readonly ToleranceClassDataAccess dataAccess;
        }
    }

}
