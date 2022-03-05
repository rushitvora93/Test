using System;
using System.Collections.Generic;
using BasicTypes;
using Core.Entities;
using ManufacturerService;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using Server.TestHelper.Factories;
using Server.UseCases.UseCases;
using TestHelper.Checker;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    public class ManufacturerUseCaseMock : IManufacturerUseCase
    {
        public List<Manufacturer> LoadManufacturers()
        {
            LoadManufacturersCalled = true;
            return LoadManufacturersReturnValue;
        }

        public string GetManufacturerComment(ManufacturerId manufacturerId)
        {
            GetManufacturerCommentParameter = manufacturerId;
            return GetManufacturerCommentReturnValue;
        }

        public List<ToolModelReferenceLink> GetManufacturerModelLinks(ManufacturerId manufacturerId)
        {
            GetManufacturerModelLinksParameter = manufacturerId;
            return GetManufacturerModelLinksReturnValue;
        }

        public List<Manufacturer> InsertManufacturersWithHistory(List<ManufacturerDiff> manufacturerDiffs, bool returnList)
        {
            InsertManufacturersWithHistoryParameterDiff = manufacturerDiffs;
            InsertManufacturersWithHistoryReturnList = returnList;
            return InsertManufacturersWithHistoryReturnValue;
        }

        public List<Manufacturer> UpdateManufacturersWithHistory(List<ManufacturerDiff> manufacturerDiffs)
        {
            UpdateManufacturersWithHistoryParameter = manufacturerDiffs;
            return UpdateManufacturersWithHistoryReturnValue;
        }

        public List<Manufacturer> UpdateManufacturersWithHistoryReturnValue { get; set; } = new List<Manufacturer>();
        public List<ManufacturerDiff> UpdateManufacturersWithHistoryParameter { get; set; }
        public List<Manufacturer> InsertManufacturersWithHistoryReturnValue { get; set; } = new List<Manufacturer>();
        public bool InsertManufacturersWithHistoryReturnList { get; set; }
        public List<ManufacturerDiff> InsertManufacturersWithHistoryParameterDiff { get; set; }
        public List<ToolModelReferenceLink> GetManufacturerModelLinksReturnValue { get; set; } = new List<ToolModelReferenceLink>();
        public ManufacturerId GetManufacturerModelLinksParameter { get; set; }
        public string GetManufacturerCommentReturnValue { get; set; }
        public ManufacturerId GetManufacturerCommentParameter { get; set; }
        public List<Manufacturer> LoadManufacturersReturnValue { get; set; } = new List<Manufacturer>();
        public bool LoadManufacturersCalled { get; set; }
    }

    public class ManufacturerServiceTest
    {
        [Test]
        public void LoadManufacturersCallsUseCaseLoadManufacturers()
        {
            var useCase = new ManufacturerUseCaseMock();
            var service = new NetworkView.Services.ManufacturerService(null, useCase);

            service.LoadManufacturers(new NoParams(), null);

            Assert.IsTrue(useCase.LoadManufacturersCalled);
        }

        private static IEnumerable<List<Manufacturer>> manufacturerData = new List<List<Manufacturer>>()
        {
            new List<Manufacturer>()
            {
                CreateManufacturer.Parametrized(99, "BLM", "Mailand", "Italien", "0123456", "12", "Sepp","0923 21345", "95468", "Weg 1"),
                CreateManufacturer.Parametrized(1, "SCS", "München", "Deutschland", "6567", "88", "Hans","546345", "11111", "Straße 11")

            },
            new List<Manufacturer>()
            {
                CreateManufacturer.Parametrized(12, "Atlas Copco", "Berlin", "Deutschland", "32234", "87238", "Müller","1234", "0034", "Straße 11")
            }
        };
        
        [TestCaseSource(nameof(manufacturerData))]
        public void LoadManufacturersReturnsCorrectValue(List<Manufacturer> manufacturers)
        {
            var useCase = new ManufacturerUseCaseMock();
            useCase.LoadManufacturersReturnValue = manufacturers;
            var service = new NetworkView.Services.ManufacturerService(null, useCase);

            var result = service.LoadManufacturers(new NoParams(), null);

            var comparer = new Func<Manufacturer, DtoTypes.Manufacturer, bool>((manufacturer, dtoManufacturer) =>
                EqualityChecker.CompareManufacturerDtoWithManufacturer(dtoManufacturer, manufacturer)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(manufacturers, result.Result.Manufacturers, comparer);
        }


        [TestCase(10)]
        [TestCase(99)]
        public void GetManufacturerCommentCallsUseCaseGetManufacturerComment(long manufacturerId)
        {
            var useCase = new ManufacturerUseCaseMock();
            useCase.GetManufacturerCommentReturnValue = "";
            var service = new NetworkView.Services.ManufacturerService(null, useCase);

            service.GetManufacturerComment(new LongRequest() {Value = manufacturerId}, null);

            Assert.AreEqual(manufacturerId, useCase.GetManufacturerCommentParameter.ToLong());
        }

        [TestCase("blub2020")]
        [TestCase("Testkommentar")]
        public void GetManufacturerCommentReturnsCorrectValue(string comment)
        {
            var useCase = new ManufacturerUseCaseMock();
            useCase.GetManufacturerCommentReturnValue = comment;
            var service = new NetworkView.Services.ManufacturerService(null, useCase);

            var result = service.GetManufacturerComment(new LongRequest() { Value = 1 }, null);

            Assert.AreEqual(comment, result.Result.Value);
        }

        [TestCase(10)]
        [TestCase(99)]
        public void GetManufacturerModelLinksCallsUseCaseGetManufacturerModelLinks(long manufacturerId)
        {
            var useCase = new ManufacturerUseCaseMock();
            var service = new NetworkView.Services.ManufacturerService(null, useCase);

            service.GetManufacturerModelLinks(new LongRequest() { Value = manufacturerId }, null);

            Assert.AreEqual(manufacturerId, useCase.GetManufacturerModelLinksParameter.ToLong());
        }


        static IEnumerable<List<ToolModelReferenceLink>> GetManufacturerModelLinksData = new List<List<ToolModelReferenceLink>>()
        {
            new List<ToolModelReferenceLink>()
            {
                new ToolModelReferenceLink()
                {
                    Id = new QstIdentifier(99),
                    DisplayName = "Wheelmaster"
                },
                new ToolModelReferenceLink()
                {
                    Id = new QstIdentifier(1),
                    DisplayName = "blub"
                }
            },
            new List<ToolModelReferenceLink>()
            {
                new ToolModelReferenceLink()
                {
                    Id = new QstIdentifier(12),
                    DisplayName = "Gerät 9999"
                }
            }
        };

        [TestCaseSource(nameof(GetManufacturerModelLinksData))]
        public void GetManufacturerModelLinksReturnsCorrectValue(List<ToolModelReferenceLink> toolModelReferenceLink)
        {
            var useCase = new ManufacturerUseCaseMock();
            useCase.GetManufacturerModelLinksReturnValue = toolModelReferenceLink;
            var service = new NetworkView.Services.ManufacturerService(null, useCase);

            var result = service.GetManufacturerModelLinks(new LongRequest() {Value = 1}, null);

            var comparer = new Func<ToolModelReferenceLink, DtoTypes.ModelLink, bool>((modelLink, dtoModelLink) =>
                modelLink.Id.ToLong() == dtoModelLink.Id &&
                modelLink.DisplayName == dtoModelLink.Model
            );

            CheckerFunctions.CollectionAssertAreEquivalent(toolModelReferenceLink, result.Result.ModelLinks, comparer);
        }

        static IEnumerable<(ListOfManufacturerDiffs, bool)> InsertUpdateManufacturerWithHistoryData = new List<(ListOfManufacturerDiffs, bool)>
        {
            (
                new ListOfManufacturerDiffs()
                {
                    ManufacturerDiff =
                    {
                        new DtoTypes.ManufacturerDiff()
                        {
                            UserId = 1,
                            Comment = "",
                            OldManufacturer = DtoFactory.CreateManufacturerDto(1, "BLM", "Italien", "Weg 1", "Giovanni", 
                                "12", "Mailand", "234" , "05494 545654", "45678", "Kommentar", true),
                            NewManufacturer =  DtoFactory.CreateManufacturerDto(1, "BLM X", "Italien Y", "Weg 1 Z", "Giovanni A",
                                "12 B", "Mailand C", "234 D" , "05494 545654 E", "45678 F","Kommentar 1", false)
                        },
                        new DtoTypes.ManufacturerDiff()
                        {
                            UserId = 2,
                            Comment = "ABCDEFG",
                            OldManufacturer =  DtoFactory.CreateManufacturerDto(3, "SCS", "Deutschland", "Straße 1", "Hans",
                                "99", "Berlin", "54 567789" , "06764 556786", "43335", "Kommentar", true),
                            NewManufacturer = DtoFactory.CreateManufacturerDto(3, "SCS A", "Deutschland B", "Straße 1 C", "Hans D",
                                "99 E", "Berlin F", "54 567789 G" , "06764 556786 H", "43335 I", "Kommentar1", false)
                        }
                    }
                },
                true
             ),
            (
                new ListOfManufacturerDiffs()
                {
                    ManufacturerDiff =
                    {
                        new DtoTypes.ManufacturerDiff()
                        {
                            UserId = 9,
                            Comment = "04359 435646",
                            OldManufacturer = DtoFactory.CreateManufacturerDto(6, "ATLAS COPCO", "Deutschland", "Weg 99", "Frank",
                                "4", "Dingolfing", "987 65443", "1234 678", "22222", "Kommentar A", true),
                            NewManufacturer = DtoFactory.CreateManufacturerDto(6, "ATLAS", "Deutschland", "Weg 100", "Müller",
                                "1", "Dingolfing", "987 1111", "1234 2223", "77777", "Kommentar B", true)
                        }
                    }
                },
                false
            )
        };

        [TestCaseSource(nameof(InsertUpdateManufacturerWithHistoryData))]
        public void InsertManufacturersWithHistoryCallsUseCase((ListOfManufacturerDiffs manufacturerDiffs, bool returnList) data)
        {
            var useCase = new ManufacturerUseCaseMock();
            var service = new NetworkView.Services.ManufacturerService(null, useCase);

            var request = new InsertManufacturerWithHistoryRequest()
            {
                ManufacturerDiffs = data.manufacturerDiffs,
                ReturnList = data.returnList
            };

            service.InsertManufacturerWithHistory(request, null);

            Assert.AreEqual(data.returnList, useCase.InsertManufacturersWithHistoryReturnList);

            var comparer = new Func<DtoTypes.ManufacturerDiff, ManufacturerDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareManufacturerDtoWithManufacturer(dtoDiff.OldManufacturer, diff.GetOldManufacturer()) &&
                EqualityChecker.CompareManufacturerDtoWithManufacturer(dtoDiff.NewManufacturer, diff.GetNewManufacturer())
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.manufacturerDiffs.ManufacturerDiff, useCase.InsertManufacturersWithHistoryParameterDiff, comparer);
        }

        [TestCaseSource(nameof(manufacturerData))]
        public void InsertManufacturerWithHistoryReturnsCorrectValue(List<Manufacturer> manufacturers)
        {
            var useCase = new ManufacturerUseCaseMock();
            var service = new NetworkView.Services.ManufacturerService(null, useCase);

            useCase.InsertManufacturersWithHistoryReturnValue = manufacturers;

            var request = new InsertManufacturerWithHistoryRequest()
            {
                ManufacturerDiffs = new ListOfManufacturerDiffs()
            };

            var result = service.InsertManufacturerWithHistory(request, null).Result;

            var comparer = new Func<Manufacturer, DtoTypes.Manufacturer, bool>((manufacturer, dtoManufacturer) =>
                EqualityChecker.CompareManufacturerDtoWithManufacturer(dtoManufacturer, manufacturer)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(manufacturers, result.Manufacturers, comparer);
        }

        [TestCaseSource(nameof(InsertUpdateManufacturerWithHistoryData))]
        public void UpdateManufacturerWithHistoryCallsUseCase((ListOfManufacturerDiffs manufacturerDiffs, bool returnList) data)
        {
            var useCase = new ManufacturerUseCaseMock();
            var service = new NetworkView.Services.ManufacturerService(null, useCase);

            var request = new UpdateManufacturerWithHistoryRequest()
            {
                ManufacturerDiffs = data.manufacturerDiffs
            };

            service.UpdateManufacturerWithHistory(request, null);

            var comparer = new Func<DtoTypes.ManufacturerDiff, ManufacturerDiff, bool>((dtoDiff, diff) =>
                dtoDiff.UserId == diff.GetUser().UserId.ToLong() &&
                dtoDiff.Comment == diff.GetComment().ToDefaultString() &&
                EqualityChecker.CompareManufacturerDtoWithManufacturer(dtoDiff.OldManufacturer, diff.GetOldManufacturer()) &&
                EqualityChecker.CompareManufacturerDtoWithManufacturer(dtoDiff.NewManufacturer, diff.GetNewManufacturer())
            );

            CheckerFunctions.CollectionAssertAreEquivalent(data.manufacturerDiffs.ManufacturerDiff, useCase.UpdateManufacturersWithHistoryParameter, comparer);
        }

        [TestCaseSource(nameof(manufacturerData))]
        public void UpdateManufacturerWithHistoryReturnsCorrectValue(List<Manufacturer> manufacturers)
        {
            var useCase = new ManufacturerUseCaseMock();
            var service = new NetworkView.Services.ManufacturerService(null, useCase);

            useCase.UpdateManufacturersWithHistoryReturnValue = manufacturers;

            var request = new UpdateManufacturerWithHistoryRequest()
            {
                ManufacturerDiffs = new ListOfManufacturerDiffs()
            };

            var result = service.UpdateManufacturerWithHistory(request, null).Result;

            var comparer = new Func<Manufacturer, DtoTypes.Manufacturer, bool>((manufacturer, dtoManufacturer) =>
                EqualityChecker.CompareManufacturerDtoWithManufacturer(dtoManufacturer, manufacturer)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(manufacturers, result.Manufacturers, comparer);
        }
    }
}
