using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Client.TestHelper.Mock;
using Core.Entities;
using Core.Entities.ReferenceLink;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using ManufacturerService;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;
using Manufacturer = Core.Entities.Manufacturer;
using User = Core.Entities.User;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    public class ManufacturerClientMock : IManufacturerClient
    {
        public ListOfManufacturers LoadManufacturers()
        {
            LoadManufacturersCalled = true;
            return LoadManufacturersReturnValue;
        }

        public StringResponse GetManufacturerComment(LongRequest longRequest)
        {
            GetManufacturerCommentParameter = longRequest;
            return GetManufacturerCommentReturnValue;
        }

        public ListOfModelLink GetManufacturerModelLinks(LongRequest longRequest)
        {
            GetManufacturerModelLinksParameter = longRequest;
            return GetManufacturerModelLinksReturnValue;
        }

        public ListOfManufacturers InsertManufacturerWithHistory(InsertManufacturerWithHistoryRequest request)
        {
            InsertManufacturerWithHistoryParameter = request;
            return InsertManufacturerWithHistoryReturnValue;
        }

        public ListOfManufacturers UpdateManufacturerWithHistory(UpdateManufacturerWithHistoryRequest request)
        {
            UpdateManufacturerWithHistoryParameter = request;
            return UpdateManufacturerWithHistoryReturnValue;
        }

        public ListOfManufacturers UpdateManufacturerWithHistoryReturnValue { get; set; }
        public UpdateManufacturerWithHistoryRequest UpdateManufacturerWithHistoryParameter { get; set; }
        public ListOfManufacturers InsertManufacturerWithHistoryReturnValue { get; set; }
        public InsertManufacturerWithHistoryRequest InsertManufacturerWithHistoryParameter { get; set; }
        public ListOfModelLink GetManufacturerModelLinksReturnValue { get; set; } = new ListOfModelLink();
        public LongRequest GetManufacturerModelLinksParameter { get; set; }
        public StringResponse GetManufacturerCommentReturnValue { get; set; }
        public LongRequest GetManufacturerCommentParameter { get; set; }
        public ListOfManufacturers LoadManufacturersReturnValue { get; set; } = new ListOfManufacturers();
        public bool LoadManufacturersCalled { get; set; }
    }

    public class ManufacturerDataAccessTest
    {
        [Test]
        public void LoadManufacturersCallsClientLoadManufacturers()
        {
            var environment = new Environment();

            environment.dataAccess.LoadManufacturer();

            Assert.IsTrue(environment.mocks.manufacturerClient.LoadManufacturersCalled);
        }

        private static IEnumerable<ListOfManufacturers> LoadManufacturersData = new List<ListOfManufacturers>()
        {
            new ListOfManufacturers()
            {
                Manufacturers =
                {
                    DtoFactory.CreateManufacturerDto(1, "Atlas Copco", "Deutschland", "Straße 1", "Hans", "14",
                        "München", "23435", "123345", "84385", "Kommentar 1"),
                    DtoFactory.CreateManufacturerDto(99, "BLM", "Italien", "Straße 99", "Giovanni", "6",
                        "Mailand", "237895", "0086", "55555", "Kommentar 2")
                }
            },
            new ListOfManufacturers()
            {
                Manufacturers =
                {
                    DtoFactory.CreateManufacturerDto(4, "SCS", "Deutschland", "Straße 100", "Christian", "5",
                        "Berlin", "222", "444", "55555", "Kommentar 3")
                }
            }
        };

        [TestCaseSource(nameof(LoadManufacturersData))]
        public void LoadManufacturersReturnsCorrectValue(ListOfManufacturers listOfManufacturers)
        {
            var environment = new Environment();
            environment.mocks.manufacturerClient.LoadManufacturersReturnValue = listOfManufacturers;

            var result = environment.dataAccess.LoadManufacturer();

            var comparer = new Func<DtoTypes.Manufacturer, Manufacturer, bool>((dtoManufacturer, manufacturer) =>
                EqualityChecker.CompareManufacturerWithManufacturerDto(manufacturer, dtoManufacturer)
             );

            CheckerFunctions.CollectionAssertAreEquivalent(listOfManufacturers.Manufacturers, result, comparer);
        }

        [TestCase(145)]
        [TestCase(678)]
        public void LoadManufacturerForCommentCallsGetManufacturerComment(long manufacturerId)
        {
            var environment = new Environment();
            environment.mocks.manufacturerClient.GetManufacturerCommentReturnValue = new StringResponse();
            environment.dataAccess.LoadManufacturerForComment(CreateManufacturer.IdOnly(manufacturerId));

            Assert.AreEqual(manufacturerId, environment.mocks.manufacturerClient.GetManufacturerCommentParameter.Value);
        }

        [TestCase("Kommentar 7364")]
        [TestCase("Hersteller")]
        public void LoadManufacturerForCommentReturnsCorrectValue(string comment)
        {
            var environment = new Environment();
            environment.mocks.manufacturerClient.GetManufacturerCommentReturnValue = new StringResponse() {Value = comment};

            var result = environment.dataAccess.LoadManufacturerForComment(CreateManufacturer.Anonymous());

            Assert.AreEqual(comment, result);
        }

        [TestCase(145)]
        [TestCase(678)]
        public void LoadToolModelReferenceLinksForManufacturerIdCallsGetManufacturerModelLinks(long manufacturerId)
        {
            var environment = new Environment();

            environment.dataAccess.LoadToolModelReferenceLinksForManufacturerId(manufacturerId);

            Assert.AreEqual(manufacturerId, environment.mocks.manufacturerClient.GetManufacturerModelLinksParameter.Value);
        }

        static IEnumerable<ListOfModelLink> LoadToolModelReferenceLinksForManufacturerIdData = new List<ListOfModelLink>()
        {
            new ListOfModelLink()
            {
                ModelLinks =
                {
                    new ModelLink() {Id = 99, Model = "223434546"},
                    new ModelLink() {Id = 12, Model = "blub 2009"},
                }
            },
            new ListOfModelLink()
            {
                ModelLinks =
                {
                    new ModelLink() {Id = 99, Model = "Wheelmaster"}
                }
            }
        };

        [TestCaseSource(nameof(LoadToolModelReferenceLinksForManufacturerIdData))]
        public void LoadToolModelReferenceLinksForManufacturerIdReturnsCorrectValue(ListOfModelLink listOfModelLink)
        {
            var environment = new Environment();
            environment.mocks.manufacturerClient.GetManufacturerModelLinksReturnValue = listOfModelLink;

            var result = environment.dataAccess.LoadToolModelReferenceLinksForManufacturerId(1);

            var comparer = new Func<ModelLink, ToolModelReferenceLink, bool>((dtoModelLink, modelLink) =>
                dtoModelLink.Id == modelLink.Id.ToLong() &&
                dtoModelLink.Model == modelLink.DisplayName
            );

            CheckerFunctions.CollectionAssertAreEquivalent(listOfModelLink.ModelLinks, result, comparer);
        }

        [Test]
        public void AddingManufacturerWithoutUserThrowsArgumentException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.AddManufacturer(null, null); });
        }

        static IEnumerable<(Manufacturer, User)> AddAndRemoveManufacturerData = new List<(Manufacturer, User)>()
        {
            (
                CreateManufacturer.Parametrized(1, "ATLAS COPCO", "Dingolfing", "Deutschland", "244435", "1",
                    "Hans", "12345", "345456", "Weg 1", "Kommentar 1"),
                CreateUser.IdOnly(2)
            ),
            (
                CreateManufacturer.Parametrized(99, "SCS", "Berlin", "Deutschland", "112", "41",
                    "Müller", "222 5678", "11122", "Straße 1", "Kommentar 3"),
                CreateUser.IdOnly(2)
            )
        };

        [TestCaseSource(nameof(AddAndRemoveManufacturerData))]
        public void AddManufacturerCallsClient((Manufacturer manufacturer, User user) data)
        {
            var environment = new Environment();
            environment.mocks.manufacturerClient.InsertManufacturerWithHistoryReturnValue = new ListOfManufacturers() { Manufacturers = { new DtoTypes.Manufacturer() } };

            environment.dataAccess.AddManufacturer(data.manufacturer, data.user);

            var clientParam = environment.mocks.manufacturerClient.InsertManufacturerWithHistoryParameter;
            var clientManuDiff = clientParam.ManufacturerDiffs.ManufacturerDiff.First();

            Assert.AreEqual(1, clientParam.ManufacturerDiffs.ManufacturerDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientManuDiff.UserId);
            Assert.AreEqual("", clientManuDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareManufacturerWithManufacturerDto(data.manufacturer, clientManuDiff.NewManufacturer));
            Assert.IsNull(clientManuDiff.OldManufacturer);
            Assert.IsTrue(clientParam.ReturnList);
        }

        static IEnumerable<DtoTypes.Manufacturer> ManufacturerDtoData = new List<DtoTypes.Manufacturer>()
        {
            DtoFactory.CreateManufacturerDto(1, "BLM", "Deutschland", "Straße 99", "Mario", 
                "12", "Berlin", "23", "243435 546", "45678", "Kommentar 4"),
            DtoFactory.CreateManufacturerDto(99, "SCS", "Belign", "Straße 9", "Kevin",
                "99", "Brüssel", "23454", "67766 7", "78643", "Kommentar 5")
        };

        [TestCaseSource(nameof(ManufacturerDtoData))]
        public void AddManufacturerReturnsCorrectValue(DtoTypes.Manufacturer manufacturerDto)
        {
            var environment = new Environment();
            environment.mocks.manufacturerClient.InsertManufacturerWithHistoryReturnValue = new ListOfManufacturers() { Manufacturers = { manufacturerDto } };
            var result = environment.dataAccess.AddManufacturer(CreateManufacturer.Anonymous(), CreateUser.Anonymous());

            Assert.IsTrue(EqualityChecker.CompareManufacturerWithManufacturerDto(result, manufacturerDto));
        }

        [Test]
        public void AddManufacturerReturnsNullThrowsException()
        {
            var environment = new Environment();
            Assert.Throws<NullReferenceException>(() =>
            {
                environment.dataAccess.AddManufacturer(CreateManufacturer.Anonymous(), CreateUser.Anonymous());
            });
        }

        [Test]
        public void RemoveManufacturerWithoutUserThrowsArgumentException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.RemoveManufacturer(null, null); });
        }

        [TestCaseSource(nameof(AddAndRemoveManufacturerData))]
        public void RemoveManufacturerCallsClient((Manufacturer manufacturer, User user) data)
        {
            var environment = new Environment();
            environment.mocks.manufacturerClient.UpdateManufacturerWithHistoryReturnValue = new ListOfManufacturers() { Manufacturers = { new DtoTypes.Manufacturer() } };

            environment.dataAccess.RemoveManufacturer(data.manufacturer, data.user);

            var clientParam = environment.mocks.manufacturerClient.UpdateManufacturerWithHistoryParameter;
            var clientManuDiff = clientParam.ManufacturerDiffs.ManufacturerDiff.First();

            Assert.AreEqual(1, clientParam.ManufacturerDiffs.ManufacturerDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientManuDiff.UserId);
            Assert.AreEqual("", clientManuDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareManufacturerWithManufacturerDto(data.manufacturer, clientManuDiff.NewManufacturer));
            Assert.IsTrue(EqualityChecker.CompareManufacturerWithManufacturerDto(data.manufacturer, clientManuDiff.OldManufacturer));
            Assert.AreEqual(true, clientManuDiff.OldManufacturer.Alive);
            Assert.AreEqual(false, clientManuDiff.NewManufacturer.Alive);
        }

        [Test]
        public void SavingManufacturerWithMismatchingIdsThrowsArgumentException()
        {
            var environment = new Environment();

            var manufacturerDiff = new Core.UseCases.ManufacturerDiff(CreateUser.Anonymous(), null, CreateManufacturer.IdOnly(1), CreateManufacturer.IdOnly(2));
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.SaveManufacturer(manufacturerDiff); });
        }

        static IEnumerable<(Manufacturer, Manufacturer, User, string)> SaveManufacturerData = new List<(Manufacturer, Manufacturer, User, string)>()
        {
            (
                CreateManufacturer.Parametrized(1, "ATLAS COPCO", "Dingolfing", "Deutschland", "244435", "1",
                    "Hans", "12345", "345456", "Weg 1", "Kommentar 1"),
                CreateManufacturer.Parametrized(1, "ATLAS COPCO X", "Dingolfing Y", "Deutschland Z", "244435 A", "1",
                    "Hans B", "12345 C", "345456 D", "Weg 1 E", "Kommentar 2"),
                CreateUser.IdOnly(2),
                "Komentar A"
            ),
            (
                CreateManufacturer.Parametrized(99, "SCS", "Berlin", "Deutschland", "112", "41",
                    "Müller", "222 5678", "11122", "Straße 1", "Kommentar 3"),
                CreateManufacturer.Parametrized(99, "SCS 1", "Berlin 2", "Deutschland3 ", "112 4", "41 5",
                    "Müller 6", "222 5678 7", "11122 8", "Straße 1 9", "Kommentar 4"),
                CreateUser.IdOnly(2),
                "Komentar B"
            )
        };

        [TestCaseSource(nameof(SaveManufacturerData))]
        public void SaveManufacturerCallsClient((Manufacturer oldManufacturer, Manufacturer newManufacturer, User user, string comment) data)
        {
            var environment = new Environment();
            
            var manufacturerDiff = new Core.UseCases.ManufacturerDiff(data.user,
                new HistoryComment(data.comment), data.oldManufacturer, data.newManufacturer);
            environment.dataAccess.SaveManufacturer(manufacturerDiff);

            var clientParam = environment.mocks.manufacturerClient.UpdateManufacturerWithHistoryParameter;
            var clientManuDiff = clientParam.ManufacturerDiffs.ManufacturerDiff.First();

            Assert.AreEqual(1, clientParam.ManufacturerDiffs.ManufacturerDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientManuDiff.UserId);
            Assert.AreEqual(data.comment, clientManuDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareManufacturerWithManufacturerDto(data.newManufacturer, clientManuDiff.NewManufacturer));
            Assert.IsTrue(EqualityChecker.CompareManufacturerWithManufacturerDto(data.oldManufacturer, clientManuDiff.OldManufacturer));
            Assert.AreEqual(true, clientManuDiff.OldManufacturer.Alive);
            Assert.AreEqual(true, clientManuDiff.NewManufacturer.Alive);
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    clientFactory = new ClientFactoryMock();
                    channelWrapper = new ChannelWrapperMock();
                    manufacturerClient = new ManufacturerClientMock();
                    channelWrapper.GetManufacturerClientReturnValue = manufacturerClient;
                    clientFactory.AuthenticationChannel = channelWrapper;
                }
                public ClientFactoryMock clientFactory;
                public ChannelWrapperMock channelWrapper;
                public ManufacturerClientMock manufacturerClient;
            }

            public Environment()
            {
                mocks = new Mocks();
                dataAccess = new ManufacturerDataAccess(mocks.clientFactory);
            }

            public readonly Mocks mocks;
            public readonly ManufacturerDataAccess dataAccess;
        }
    }
}
