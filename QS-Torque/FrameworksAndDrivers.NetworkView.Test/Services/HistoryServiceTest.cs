using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    public class HistoryUseCaseMock : IHistoryUseCase
    {
        public LocationId LoadLocationDiffsParameter { get; set; }
        public List<LocationDiff> LoadLocationDiffsReturnValue { get; set; }

        public List<LocationDiff> LoadLocationDiffsFor(LocationId id)
        {
            LoadLocationDiffsParameter = id;
            return LoadLocationDiffsReturnValue;
        }
    }


    public class HistoryServiceTest
    {
        [TestCaseSource(nameof(CreateListOfLocationDiffs))]
        public void LoadLocationDiffTest(List<LocationDiff> diffs)
        {
            var environment = CreateServiceTuple();
            environment.useCase.LoadLocationDiffsReturnValue = diffs;
            var now = DateTime.Now.Ticks;
            var result = environment.service.LoadLocationDiffsFor(new BasicTypes.Long() { Value = now }, null).Result;

            Assert.AreEqual(now, environment.useCase.LoadLocationDiffsParameter.ToLong());
            for (int i = 0; i < diffs.Count; i++)
            {
                Assert.AreEqual(diffs[i].GetUser().UserId.ToLong(), result.LocationDiffs[i].User.UserId);
                Assert.AreEqual(diffs[i].GetComment().ToDefaultString(), result.LocationDiffs[i].Comment);
                Assert.IsTrue(EqualityChecker.CompareLocationDtoWithLocation(result.LocationDiffs[i].OldLocation, diffs[i].GetOldLocation()));
                Assert.IsTrue(EqualityChecker.CompareLocationDtoWithLocation(result.LocationDiffs[i].NewLocation, diffs[i].GetNewLocation()));
                Assert.AreEqual((long)diffs[i].Type, result.LocationDiffs[i].Type);
                Assert.AreEqual(diffs[i].TimeStamp.Ticks, result.LocationDiffs[i].TimeStamp.Ticks);
            }
        }


        private static IEnumerable<List<LocationDiff>> CreateListOfLocationDiffs()
        {
            yield return new List<LocationDiff>()
            {
                new LocationDiff(new User() { UserId = new UserId(5), UserName = "" },
                    new HistoryComment("ösdfaökdj"),
                    new Location()
                    {
                        Id = new LocationId(6),
                        Number = new LocationNumber("öasidjf"),
                        Description = new LocationDescription("k"),
                        ParentDirectoryId = new LocationDirectoryId(4),
                        ControlledBy = Server.Core.Enums.LocationControlledBy.Angle,
                        SetPoint1 = 6,
                        ToleranceClass1 = new ToleranceClass() { Id = new ToleranceClassId(12), Name = "öasdjfk" },
                        Minimum1 = 5,
                        Maximum1 = 45,
                        Threshold1 = 65,
                        SetPoint2 = 32,
                        ToleranceClass2 = new ToleranceClass() { Id = new ToleranceClassId(10), Name = "awöeit" },
                        Minimum2 = 20,
                        Maximum2 = 30,
                        ConfigurableField1 = new LocationConfigurableField1("söadfjö"),
                        ConfigurableField2 = new LocationConfigurableField2("j"),
                        ConfigurableField3 = true,
                        Alive = true,
                        Comment = "09divajof"
                    },
                    new Location()
                    {
                        Id = new LocationId(45),
                        Number = new LocationNumber("asdfl"),
                        Description = new LocationDescription("awloef"),
                        ParentDirectoryId = new LocationDirectoryId(324879),
                        ControlledBy = Server.Core.Enums.LocationControlledBy.Torque,
                        SetPoint1 = 3342,
                        ToleranceClass1 = new ToleranceClass() { Id = new ToleranceClassId(5951), Name = "psepr89t" },
                        Minimum1 = 32,
                        Maximum1 = 409,
                        Threshold1 = 655,
                        SetPoint2 = 323642,
                        ToleranceClass2 = new ToleranceClass() { Id = new ToleranceClassId(1230), Name = "qp34u" },
                        Minimum2 = 2023,
                        Maximum2 = 305,
                        ConfigurableField1 = new LocationConfigurableField1("w3ölkj"),
                        ConfigurableField2 = new LocationConfigurableField2("d"),
                        ConfigurableField3 = false,
                        Alive = false,
                        Comment = "ql334"
                    }) 
                {
                    Type = Server.Core.Enums.DiffType.Update,
                    TimeStamp = new DateTime(65434321)
                },
                new LocationDiff(new User() { UserId = new UserId(5), UserName = "" },
                    new HistoryComment("ösdfaökdj"),
                    new Location()
                    {
                        Id =  new LocationId(26),
                        Number = new LocationNumber("aapf98"),
                        Description = new LocationDescription("aw6e4"),
                        ParentDirectoryId = new LocationDirectoryId(34),
                        ControlledBy = Server.Core.Enums.LocationControlledBy.Torque,
                        SetPoint1 = 36,
                        ToleranceClass1 = new ToleranceClass() { Id = new ToleranceClassId(312), Name = "pq3vu4" },
                        Minimum1 = 35,
                        Maximum1 = 345,
                        Threshold1 = 365,
                        SetPoint2 = 332,
                        ToleranceClass2 = new ToleranceClass() { Id = new ToleranceClassId(310), Name = "ezh8f4t" },
                        Minimum2 = 320,
                        Maximum2 = 330,
                        ConfigurableField1 = new LocationConfigurableField1("sapodfsöadfjö"),
                        ConfigurableField2 = new LocationConfigurableField2("3"),
                        ConfigurableField3 = true,
                        Alive = false,
                        Comment = "xcvvydf"
                    },
                    new Location()
                    {
                        Id = new LocationId(96),
                        Number = new LocationNumber("sldöfjk"),
                        Description = new LocationDescription("käapreoi"),
                        ParentDirectoryId = new LocationDirectoryId(94),
                        ControlledBy = Server.Core.Enums.LocationControlledBy.Angle,
                        SetPoint1 = 96,
                        ToleranceClass1 = new ToleranceClass() { Id = new ToleranceClassId(912), Name = "qrvj8tu" },
                        Minimum1 = 95,
                        Maximum1 = 945,
                        Threshold1 = 965,
                        SetPoint2 = 932,
                        ToleranceClass2 = new ToleranceClass() { Id = new ToleranceClassId(910), Name = "54456" },
                        Minimum2 = 920,
                        Maximum2 = 930,
                        ConfigurableField1 = new LocationConfigurableField1("q6w554r0"),
                        ConfigurableField2 = new LocationConfigurableField2("a"),
                        ConfigurableField3 = true,
                        Alive = false,
                        Comment = "serf654"
                    })
                {
                    Type = Server.Core.Enums.DiffType.Delete,
                    TimeStamp = new DateTime(5266)
                }
            };
            yield return new List<LocationDiff>()
            {
                new LocationDiff(new User() { UserId = new UserId(5), UserName = "" },
                    new HistoryComment("ösdfaökdj"),
                    new Location()
                    {
                        Id = new LocationId(16),
                        Number = new LocationNumber("ölaskföasidjf"),
                        Description = new LocationDescription("öeirui"),
                        ParentDirectoryId = new LocationDirectoryId(12),
                        ControlledBy = Server.Core.Enums.LocationControlledBy.Angle,
                        SetPoint1 = 16,
                        ToleranceClass1 = new ToleranceClass() { Id = new ToleranceClassId(112), Name = "23fp8" },
                        Minimum1 = 15,
                        Maximum1 = 145,
                        Threshold1 = 165,
                        SetPoint2 = 132,
                        ToleranceClass2 = new ToleranceClass() { Id = new ToleranceClassId(110), Name = "öaosiv" },
                        Minimum2 = 120,
                        Maximum2 = 130,
                        ConfigurableField1 = new LocationConfigurableField1("dpaowiiuef"),
                        ConfigurableField2 = new LocationConfigurableField2("s"),
                        ConfigurableField3 = true,
                        Alive = true,
                        Comment = "öalsksjdf"
                    },
                    new Location()
                    {
                        Id = new LocationId(26),
                        Number = new LocationNumber("2öasidjf"),
                        Description = new LocationDescription("2k"),
                        ParentDirectoryId = new LocationDirectoryId(24),
                        ControlledBy = Server.Core.Enums.LocationControlledBy.Angle,
                        SetPoint1 = 26,
                        ToleranceClass1 = new ToleranceClass() { Id = new ToleranceClassId(212), Name = "qpvn" },
                        Minimum1 = 25,
                        Maximum1 = 245,
                        Threshold1 = 265,
                        SetPoint2 = 232,
                        ToleranceClass2 = new ToleranceClass() { Id = new ToleranceClassId(210), Name = "9pwznrt" },
                        Minimum2 = 220,
                        Maximum2 = 230,
                        ConfigurableField1 = new LocationConfigurableField1("2söadfjö"),
                        ConfigurableField2 = new LocationConfigurableField2("l"),
                        ConfigurableField3 = false,
                        Alive = false,
                        Comment = "209divajof"
                    })
                {
                    Type = Server.Core.Enums.DiffType.Insert,
                    TimeStamp = new DateTime(89879)
                }
            };
        }


        private static (FrameworksAndDrivers.NetworkView.Services.HistoryService service, HistoryUseCaseMock useCase) CreateServiceTuple()
        {
            var useCase = new HistoryUseCaseMock();
            var service = new FrameworksAndDrivers.NetworkView.Services.HistoryService(useCase, null);
            return (service, useCase);
        }
    }
}
