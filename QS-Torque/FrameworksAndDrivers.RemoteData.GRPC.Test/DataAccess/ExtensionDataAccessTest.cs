using BasicTypes;
using Client.TestHelper.Mock;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Client.Core.Diffs;
using Client.TestHelper.Factories;
using Core.Entities;
using Core.Entities.ReferenceLink;
using DtoTypes;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;
using ExtensionService;
using String = BasicTypes.String;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    public class ExtensionClientMock : IExtensionClient
    {
        public bool LoadExtensionsCalled { get; set; }
        public ListOfExtensions LoadExtensionsReturnValue { get; set; } = new ListOfExtensions();
        public ListOfLocationLink GetExtensionLocationLinksReturnValue { get; set; } = new ListOfLocationLink();
        public LongRequest GetExtensionLocationLinksParameter { get; set; }
        public ListOfExtensions InsertExtensionsReturnValue { get; set; } = new ListOfExtensions();
        public InsertExtensionsRequest InsertExtensionsParameterRequest { get; set; }
        public Bool IsExtensionInventoryNumberUniqueReturnValue { get; set; } = new Bool();
        public String IsExtensionInventoryNumberUniqueParameter { get; set; }
        public UpdateExtensionsRequest UpdateExtensionsParameterRequest { get; set; }

        public ListOfLocationLink GetExtensionLocationLinks(LongRequest extensionId)
        {
            GetExtensionLocationLinksParameter = extensionId;
            return GetExtensionLocationLinksReturnValue;
        }

        public ListOfExtensions InsertExtensions(InsertExtensionsRequest request)
        {
            InsertExtensionsParameterRequest = request;
            return InsertExtensionsReturnValue;
        }

        public void UpdateExtensions(UpdateExtensionsRequest request)
        {
            UpdateExtensionsParameterRequest = request;
        }

        public Bool IsExtensionInventoryNumberUnique(String inventoryNumber)
        {
            IsExtensionInventoryNumberUniqueParameter = inventoryNumber;
            return IsExtensionInventoryNumberUniqueReturnValue;
        }

        public ListOfExtensions LoadExtensions()
        {
            LoadExtensionsCalled = true;
            return LoadExtensionsReturnValue;
        }

        public ListOfExtensions LoadDeletedExtensions()
        {
            throw new NotImplementedException();
        }
    }

    public class ExtensionDataAccessTest
    {
       [Test]
       public void LoadExtensionsCallsClient()
        {
            var enviroment = new Environment();
            enviroment.dataAccess.LoadExtensions();
            Assert.IsTrue(enviroment.mocks.extensionClient.LoadExtensionsCalled);
        }

        static IEnumerable<ListOfExtensions> LoadExtensionsData = new List<ListOfExtensions>
        {
            new ListOfExtensions()
            {
                Extensions =
                {
                    DtoFactory.CreateExtension(1, "ext1", "0001", 1.0, 2.0, 3.0, true),
                    DtoFactory.CreateExtension(2, "ext2", "0002", 11.0, 22.0, 33.0, true)
                }
            },
            new ListOfExtensions()
            {
                Extensions =
                {
                    DtoFactory.CreateExtension(10, "extension_10", "00xxx01", 0.0, 0.0, 0.0, true),
                }
            }
        };

        [TestCaseSource(nameof(LoadExtensionsData))]
        public void LoadExtensionsReturnsCorrectValue(ListOfExtensions listOfExtensions)
        {
            var enviroment = new Environment();
            enviroment.mocks.extensionClient.LoadExtensionsReturnValue = listOfExtensions;
            var result = enviroment.dataAccess.LoadExtensions();

            var comparer = new Func<DtoTypes.Extension, Core.Entities.Extension, bool>((extDto, ext) =>
                    EqualityChecker.CompareExtensionWithExtensionDto(ext, extDto)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(listOfExtensions.Extensions, result, comparer);
        }

        [TestCase(1)]
        [TestCase(100)]
        public void LoadReferencedLocationsCallsClient(long extensionId)
        {
            var enviroment = new Environment();
            enviroment.dataAccess.LoadReferencedLocations(new ExtensionId(extensionId));
            Assert.AreEqual(extensionId, enviroment.mocks.extensionClient.GetExtensionLocationLinksParameter.Value);
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
            var enviroment = new Environment();
            enviroment.mocks.extensionClient.GetExtensionLocationLinksReturnValue = locationLinks;
            var result = enviroment.dataAccess.LoadReferencedLocations(new ExtensionId(1));

            var comparer = new Func<LocationLink, LocationReferenceLink, bool>((locReferenceLinkDto, locReferenceLink) =>
                locReferenceLinkDto.Id == locReferenceLink.Id.ToLong() &&
                locReferenceLinkDto.Number == locReferenceLink.Number.ToDefaultString() &&
                locReferenceLinkDto.Description == locReferenceLink.Description.ToDefaultString()
            );

            CheckerFunctions.CollectionAssertAreEquivalent(locationLinks.LocationLinks, result, comparer);
        }

        [Test]
        public void AddingExtensionWithoutUserThrowsArgumentException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.AddExtension(null, null); });
        }

        static IEnumerable<(Core.Entities.Extension, Core.Entities.User)> AddAndRemoveExtensionData = new List<(Core.Entities.Extension, Core.Entities.User)>()
        {
            (
                CreateExtension.Randomized(12343),
                CreateUser.IdOnly(2)
            ),
            (
                CreateExtension.Randomized(678678),
                CreateUser.IdOnly(32)
            )
        };

        [TestCaseSource(nameof(AddAndRemoveExtensionData))]
        public void AddExtensionCallsClient((Core.Entities.Extension extension, Core.Entities.User user) data)
        {
            var environment = new Environment();
            environment.mocks.extensionClient.InsertExtensionsReturnValue = new ListOfExtensions() { Extensions = { new DtoTypes.Extension() } };

            environment.dataAccess.AddExtension(data.extension, data.user);

            var clientParam = environment.mocks.extensionClient.InsertExtensionsParameterRequest;
            var clientDiff = clientParam.ExtensionsDiffs.ExtensionsDiff.First();

            Assert.AreEqual(1, clientParam.ExtensionsDiffs.ExtensionsDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.AreEqual("", clientDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareExtensionWithExtensionDto(data.extension, clientDiff.NewExtension));
            Assert.IsNull(clientDiff.OldExtension);
            Assert.IsTrue(clientParam.ReturnList);
        }

        static IEnumerable<DtoTypes.Extension> ExtensionDtoData = new List<DtoTypes.Extension>()
        {
            DtoFactory.CreateExtensionRandomized(435345),
            DtoFactory.CreateExtensionRandomized(324)
        };

        [TestCaseSource(nameof(ExtensionDtoData))]
        public void AddExtensionReturnsCorrectValue(DtoTypes.Extension extensionDto)
        {
            var environment = new Environment();
            environment.mocks.extensionClient.InsertExtensionsReturnValue = new ListOfExtensions() { Extensions = { extensionDto } };
            var result = environment.dataAccess.AddExtension(CreateExtension.Randomized(213), CreateUser.Anonymous());

            Assert.IsTrue(EqualityChecker.CompareExtensionWithExtensionDto(result, extensionDto));
        }

        [Test]
        public void AddExtensionReturnsNullThrowsException()
        {
            var environment = new Environment();

            Assert.Throws<NullReferenceException>(() =>
            {
                environment.dataAccess.AddExtension(CreateExtension.Randomized(12), CreateUser.Anonymous());
            });
        }

        [TestCase("inv 1")]
        [TestCase("abcd")]
        public void IsInventoryNumberUniqueCallsClient(string inventoryNumber)
        {
            var environment = new Environment();

            environment.dataAccess.IsInventoryNumberUnique(new ExtensionInventoryNumber(inventoryNumber));

            Assert.AreEqual(inventoryNumber, environment.mocks.extensionClient.IsExtensionInventoryNumberUniqueParameter.Value);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsInventoryNumberUniqueReturnsCorrectValue(bool isUnique)
        {
            var environment = new Environment();
            environment.mocks.extensionClient.IsExtensionInventoryNumberUniqueReturnValue = new Bool() { Value = isUnique };

            Assert.AreEqual(isUnique, environment.dataAccess.IsInventoryNumberUnique(new ExtensionInventoryNumber("")));
        }

        [Test]
        public void SaveExtensionWithMismatchingIdsThrowsArgumentException()
        {
            var environment = new Environment();
            var extensionDiff = new Client.Core.Diffs.ExtensionDiff(CreateUser.IdOnly(4), new HistoryComment(""), CreateExtension.WithId(1), CreateExtension.WithId(2));
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.SaveExtension(extensionDiff); });
        }

        [Test]
        public void SaveUnchangedExtensionDontCallClient()
        {
            var environment = new Environment();
            var extensionDiff = new Client.Core.Diffs.ExtensionDiff(CreateUser.IdOnly(4), new HistoryComment(""),  CreateExtension.WithId(1), CreateExtension.WithId(1));
            environment.dataAccess.SaveExtension(extensionDiff);
            Assert.IsNull(environment.mocks.extensionClient.UpdateExtensionsParameterRequest);
        }

        static IEnumerable<(Core.Entities.Extension, Core.Entities.Extension, Core.Entities.User, string comment)> SaveExtensionCallsClientData = new List<(Core.Entities.Extension, Core.Entities.Extension, Core.Entities.User, string)>()
        {
            (
                CreateExtension.RandomizedWithId(4335456, 1),
                CreateExtension.RandomizedWithId(5758, 1),
                CreateUser.IdOnly(1),
                "ertet"
            ),
            (
                CreateExtension.RandomizedWithId(33345, 2),
                CreateExtension.RandomizedWithId(778,2),
                CreateUser.IdOnly(2),
                "3242345435"
            ),
        };

        [TestCaseSource(nameof(SaveExtensionCallsClientData))]
        public void SaveExtensionCallsClient((Core.Entities.Extension oldExtension, Core.Entities.Extension newExtension, Core.Entities.User user, string comment) data)
        {
            var environment = new Environment();
            var extensionDiff = new Client.Core.Diffs.ExtensionDiff(data.user, new HistoryComment(data.comment), data.oldExtension, data.newExtension);
            environment.dataAccess.SaveExtension(extensionDiff);

            var clientParam = environment.mocks.extensionClient.UpdateExtensionsParameterRequest;
            var clientDiff = clientParam.ExtensionsDiffs.ExtensionsDiff.First();

            Assert.AreEqual(1, clientParam.ExtensionsDiffs.ExtensionsDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.AreEqual(data.comment, clientDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareExtensionWithExtensionDto(data.oldExtension, clientDiff.OldExtension));
            Assert.IsTrue(EqualityChecker.CompareExtensionWithExtensionDto(data.newExtension, clientDiff.NewExtension));
            Assert.IsTrue(clientDiff.OldExtension.Alive);
            Assert.IsTrue(clientDiff.NewExtension.Alive);
        }

        [Test]
        public void RemoveExtensionWithoutUserThrowsArgumentException()
        {
            var environment = new Environment();
            Assert.Throws<ArgumentException>(() => { environment.dataAccess.RemoveExtension(CreateExtension.WithId(1), null); });
        }

        [TestCaseSource(nameof(AddAndRemoveExtensionData))]
        public void RemoveExtensionCallsClient((Core.Entities.Extension extension, Core.Entities.User user) data)
        {
            var environment = new Environment();

            environment.dataAccess.RemoveExtension(data.extension, data.user);

            var clientParam = environment.mocks.extensionClient.UpdateExtensionsParameterRequest;
            var clientDiff = clientParam.ExtensionsDiffs.ExtensionsDiff.First();

            Assert.AreEqual(1, clientParam.ExtensionsDiffs.ExtensionsDiff.Count);
            Assert.AreEqual(data.user.UserId.ToLong(), clientDiff.UserId);
            Assert.AreEqual("", clientDiff.Comment);
            Assert.IsTrue(EqualityChecker.CompareExtensionWithExtensionDto(data.extension, clientDiff.NewExtension));
            Assert.IsTrue(EqualityChecker.CompareExtensionWithExtensionDto(data.extension, clientDiff.OldExtension));
            Assert.AreEqual(true, clientDiff.OldExtension.Alive);
            Assert.AreEqual(false, clientDiff.NewExtension.Alive);
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

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    clientFactory = new ClientFactoryMock();
                    channelWrapper = new ChannelWrapperMock();
                    extensionClient = new ExtensionClientMock();
                    channelWrapper.GetExtensionClientReturnValue = extensionClient;
                    clientFactory.AuthenticationChannel = channelWrapper;
                    timeDataAccess = new TimeDataAccessMock();
                }
                public ClientFactoryMock clientFactory;
                public ChannelWrapperMock channelWrapper;
                public ExtensionClientMock extensionClient;
                public TimeDataAccessMock timeDataAccess;
            }

            public Environment()
            {
                mocks = new Mocks();
                dataAccess = new ExtensionDataAccess(mocks.clientFactory, null, mocks.timeDataAccess);
            }

            public readonly Mocks mocks;
            public readonly ExtensionDataAccess dataAccess;
        }
    }
}
