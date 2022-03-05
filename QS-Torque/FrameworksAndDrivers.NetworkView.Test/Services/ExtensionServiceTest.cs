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
using ExtensionService;
using DateTime = System.DateTime;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    public class ExtensionUseCaseMock : IExtensionUseCase
    {
        public List<Extension> LoadExtensions()
        {
            LoadExtensionsCalled = true;
            return LoadExtensionsReturnValue;
        }

        public List<LocationReferenceLink> GetExtensionLocationLinks(ExtensionId classId)
        {
            GetExtensionLocationLinksParameter = classId;
            return GetExtensionLocationLinksReturnValue;
        }

        public List<Extension> InsertExtensions(List<ExtensionDiff> diffs, bool returnList)
        {
            InsertExtensionsDiffs = diffs;
            InsertExtensionsReturnList = returnList;
            return InsertExtensionsReturnValue;
        }

        public void UpdateExtensions(List<ExtensionDiff> extensionDiffs)
        {
            UpdateExtensionsDiff = extensionDiffs;
        }        

        public bool IsInventoryNumberUnique(ExtensionInventoryNumber inventoryNumber)
        {
            IsInventoryNumberUniqueParameter = inventoryNumber;
            return IsInventoryNumberUniqueReturnValue;
        }

        public List<Extension> LoadDeletedExtensions()
        {
            throw new NotImplementedException();
        }

        public List<ExtensionDiff> UpdateExtensionsDiff { get; set; }
        public bool IsInventoryNumberUniqueReturnValue { get; set; }
        public ExtensionInventoryNumber IsInventoryNumberUniqueParameter { get; set; }
        public List<Extension> InsertExtensionsReturnValue { get; set; } = new List<Extension>();
        public bool InsertExtensionsReturnList { get; set; }
        public List<ExtensionDiff> InsertExtensionsDiffs { get; set; }
        public List<Extension> LoadExtensionsReturnValue { get; set; } = new List<Extension>();
        public bool LoadExtensionsCalled { get; set; }
        public List<LocationReferenceLink> GetExtensionLocationLinksReturnValue { get; set; } = new List<LocationReferenceLink>();
        public ExtensionId GetExtensionLocationLinksParameter { get; set; }
    }

    public class ExtensionServiceTest
    {
        [Test]
        public void LoadExtensionsCallsUseCase()
        {
            var useCase = new ExtensionUseCaseMock();
            var service = new NetworkView.Services.ExtensionService(null, useCase);

            service.LoadExtensions(new NoParams(), null);

            Assert.IsTrue(useCase.LoadExtensionsCalled);
        }

        private static IEnumerable<List<Extension>> ExtensionData = new List<List<Extension>>()
        {
            new List<Extension>()
            {
                CreateExtension.Parametrized(1, "name", "0001", 0.0, 1.1, 2.2),
                CreateExtension.Parametrized(2, "Extens", "0002", 0.0, 0.0, 0.0)
            },
            new List<Extension>()
            {
                CreateExtension.Parametrized(99, "Ext 1", "0123", 9.9, 12.1, 13.5)
            }
        };

        [TestCaseSource(nameof(ExtensionData))]
        public void LoadExtensionsReturnsCorrectValue(List<Extension> Extensions)
        {
            var useCase = new ExtensionUseCaseMock();
            useCase.LoadExtensionsReturnValue = Extensions;
            var service = new NetworkView.Services.ExtensionService(null, useCase);

            var result = service.LoadExtensions(new NoParams(), null);

            var comparer = new Func<Extension, DtoTypes.Extension, bool>((Extension, dtoExtension) =>
                EqualityChecker.CompareExtensionDtoWithExtension(dtoExtension, Extension)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(Extensions, result.Result.Extensions, comparer);
        }

        [TestCase(1)]
        [TestCase(99)]
        public void GetExtensionLocationLinksCallsUseCase(long classId)
        {
            var useCase = new ExtensionUseCaseMock();
            var service = new NetworkView.Services.ExtensionService(null, useCase);

            service.GetExtensionLocationLinks(new LongRequest() { Value = classId }, null);

            Assert.AreEqual(classId, useCase.GetExtensionLocationLinksParameter.ToLong());
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
        public void GetExtensionLocationLinksReturnsCorrectValue(List<LocationReferenceLink> locationReferenceLink)
        {
            var useCase = new ExtensionUseCaseMock();
            var service = new NetworkView.Services.ExtensionService(null, useCase);
            useCase.GetExtensionLocationLinksReturnValue = locationReferenceLink;

            var result = service.GetExtensionLocationLinks(new LongRequest(), null);

            var comparer = new Func<LocationReferenceLink, DtoTypes.LocationLink, bool>((locationLink, dtoLocationLink) =>
                locationLink.Id.ToLong() == dtoLocationLink.Id &&
                locationLink.Description == dtoLocationLink.Description &&
                locationLink.Number == dtoLocationLink.Number
            );

            CheckerFunctions.CollectionAssertAreEquivalent(locationReferenceLink, result.Result.LocationLinks, comparer);
        }

        static IEnumerable<(ListOfExtensionsDiffs, bool)> InsertUpdateExtensionData = new List<(ListOfExtensionsDiffs, bool)>
        {
            (
                new ListOfExtensionsDiffs()
                {
                    ExtensionsDiff =
                    {
                        new DtoTypes.ExtensionDiff()
                        {
                            UserId = 1,
                            Comment = "4365678",
                            OldExtension = DtoFactory.CreateExtensionRandomized(45646),
                            NewExtension = DtoFactory.CreateExtensionRandomized(32423),
                        },
                        new DtoTypes.ExtensionDiff()
                        {
                            UserId = 14,
                            Comment = "345647",
                            OldExtension = DtoFactory.CreateExtensionRandomized(111),
                            NewExtension = DtoFactory.CreateExtensionRandomized(2222),
                        },
                    }
                },
                true
             ),
            (
                new ListOfExtensionsDiffs()
                {
                    ExtensionsDiff =
                    {
                        new DtoTypes.ExtensionDiff()
                        {
                            UserId = 451,
                            Comment = "43547558",
                            OldExtension = DtoFactory.CreateExtensionRandomized(4567),
                            NewExtension = DtoFactory.CreateExtensionRandomized(12),
                        },
                        new DtoTypes.ExtensionDiff()
                        {
                            UserId = 23,
                            Comment = "43546",
                            OldExtension = DtoFactory.CreateExtensionRandomized(124324),
                            NewExtension = DtoFactory.CreateExtensionRandomized(5674),
                        },
                    }
                },
                false
            )
        };


        [TestCaseSource(nameof(InsertUpdateExtensionData))]
        public void InsertExtensionsCallsUseCase((ListOfExtensionsDiffs extensionDiffs, bool returnList) data)
        {
            var useCase = new ExtensionUseCaseMock();
            var service = new NetworkView.Services.ExtensionService(null, useCase);

            var request = new InsertExtensionsRequest()
            {
                ExtensionsDiffs = data.extensionDiffs,
                ReturnList = data.returnList
            };

            service.InsertExtensions(request, null);

            Assert.AreEqual(data.returnList, useCase.InsertExtensionsReturnList);

            var comparer = new Func<DtoTypes.ExtensionDiff, ExtensionDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.User.UserId.ToLong() &&
                dtoDiff.Comment == diff.Comment.ToDefaultString() &&
                EqualityChecker.CompareExtensionDtoWithExtension(dtoDiff.OldExtension, diff.OldExtension) &&
                EqualityChecker.CompareExtensionDtoWithExtension(dtoDiff.NewExtension, diff.NewExtension)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.extensionDiffs.ExtensionsDiff, useCase.InsertExtensionsDiffs, comparer);
        }

        [TestCaseSource(nameof(ExtensionData))]
        public void InsertExtensionsReturnsCorrectValue(List<Extension> extensions)
        {
            var useCase = new ExtensionUseCaseMock();
            var service = new NetworkView.Services.ExtensionService(null, useCase);

            useCase.InsertExtensionsReturnValue = extensions;

            var request = new InsertExtensionsRequest()
            {
                ExtensionsDiffs = new ListOfExtensionsDiffs()
            };

            var result = service.InsertExtensions(request, null).Result;

            var comparer = new Func<Extension, DtoTypes.Extension, bool>((extension, dtoExtension) =>
                EqualityChecker.CompareExtensionDtoWithExtension(dtoExtension, extension)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(extensions, result.Extensions, comparer);
        }

        [TestCaseSource(nameof(InsertUpdateExtensionData))]
        public void UpdateExtensionsWithHistoryCallsUseCase((ListOfExtensionsDiffs diffs, bool returnList) data)
        {
            var useCase = new ExtensionUseCaseMock();
            var service = new NetworkView.Services.ExtensionService(null, useCase);

            var request = new UpdateExtensionsRequest()
            {
                ExtensionsDiffs = data.diffs
            };

            service.UpdateExtensions(request, null);

            var comparer = new Func<DtoTypes.ExtensionDiff, ExtensionDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.User.UserId.ToLong() &&
                dtoDiff.Comment == diff.Comment.ToDefaultString() &&
                EqualityChecker.CompareExtensionDtoWithExtension(dtoDiff.OldExtension, diff.OldExtension) &&
                EqualityChecker.CompareExtensionDtoWithExtension(dtoDiff.NewExtension, diff.NewExtension)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.diffs.ExtensionsDiff, useCase.UpdateExtensionsDiff, comparer);
        }

        [TestCase("1234")]
        [TestCase("8435345 123")]
        public void IsInventoryNumberUniqueCallsUseCase(string inventoryNumber)
        {
            var useCase = new ExtensionUseCaseMock();
            var service = new NetworkView.Services.ExtensionService(null, useCase);

            service.IsExtensionInventoryNumberUnique(new BasicTypes.String() { Value = inventoryNumber }, null);

            Assert.AreEqual(inventoryNumber, useCase.IsInventoryNumberUniqueParameter.ToDefaultString());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsInventoryNumberUniqueReturnsCorrectValue(bool isUnique)
        {
            var useCase = new ExtensionUseCaseMock();
            var service = new NetworkView.Services.ExtensionService(null, useCase);
            useCase.IsInventoryNumberUniqueReturnValue = isUnique;

            var result = service.IsExtensionInventoryNumberUnique(new BasicTypes.String() { Value = "" }, null);

            Assert.AreEqual(isUnique, result.Result.Value);
        }
    }
}
