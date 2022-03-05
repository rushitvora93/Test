using Client.Core.Enums;
using Client.TestHelper.Mock;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHelper.Mock;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    class HistoryClientMock : IHistoryClient
    {
        public BasicTypes.Long LoadLocationDiffsParam { get; set; }
        public ListOfLocationDiff LoadLocationDiffsReturnValue { get; set; }

        public ListOfLocationDiff LoadLocationDiffsFor(BasicTypes.Long param)
        {
            LoadLocationDiffsParam = param;
            return LoadLocationDiffsReturnValue;
        }
    }

    public class HistoryDataAccessTest
    {
        [TestCaseSource(nameof(CreateListOfLocationDiff))]
        public void LoadLocationDiffsTest(ListOfLocationDiff diffs)
        {
            var environment = CreateDataAccessTuple();
            environment.client.LoadLocationDiffsReturnValue = diffs;
            var now = DateTime.Now.Ticks;
            var result = environment.dataAccess.LoadLocationDiffsFor(new Core.Entities.LocationId(now));

            Assert.AreEqual(now, environment.client.LoadLocationDiffsParam.Value);
            for (int i = 0; i < diffs.LocationDiffs.Count; i++)
            {
                Assert.IsTrue(EqualityChecker.CompareLocationDtoWithLocation(diffs.LocationDiffs[i].OldLocation, result[i].OldLocation));
                Assert.IsTrue(EqualityChecker.CompareLocationDtoWithLocation(diffs.LocationDiffs[i].NewLocation, result[i].NewLocation));
                Assert.AreEqual(diffs.LocationDiffs[i].User.UserId, result[i].User.UserId.ToLong());
                Assert.AreEqual(diffs.LocationDiffs[i].Comment, result[i].Comment.ToDefaultString());
                Assert.AreEqual((DiffType)diffs.LocationDiffs[i].Type, result[i].Type);
                Assert.AreEqual(diffs.LocationDiffs[i].TimeStamp.Ticks, result[i].TimeStamp.Ticks);
            }
        }

        private static IEnumerable<ListOfLocationDiff> CreateListOfLocationDiff()
        {
            yield return new ListOfLocationDiff()
            {
                LocationDiffs =
                {
                    new DtoTypes.LocationDiff()
                    {
                        OldLocation = new Location()
                        {
                            Id = 6,
                            Number = "öasidjf",
                            Description = "k",
                            ParentDirectoryId = 4,
                            ControlledBy = 1,
                            SetPoint1 = 6,
                            ToleranceClass1 = new ToleranceClass() { Id = 12 },
                            Minimum1 = 5,
                            Maximum1 = 45,
                            Threshold1 = 65,
                            SetPoint2 = 32,
                            ToleranceClass2 = new ToleranceClass() { Id = 10 },
                            Minimum2 = 20,
                            Maximum2 = 30,
                            ConfigurableField1 = new BasicTypes.NullableString() { Value = "söadfjö" },
                            ConfigurableField2 = new BasicTypes.NullableString() { Value = "j" },
                            ConfigurableField3 = true,
                            Alive = true,
                            Comment = new BasicTypes.NullableString() { Value = "09divajof" }
                        },
                        NewLocation = new Location()
                        {
                            Id = 45,
                            Number = "asdfl",
                            Description = "awloef",
                            ParentDirectoryId = 324879,
                            ControlledBy = 2,
                            SetPoint1 = 3342,
                            ToleranceClass1 = new ToleranceClass() { Id = 5951 },
                            Minimum1 = 32,
                            Maximum1 = 409,
                            Threshold1 = 655,
                            SetPoint2 = 323642,
                            ToleranceClass2 = new ToleranceClass() { Id = 1230 },
                            Minimum2 = 2023,
                            Maximum2 = 305,
                            ConfigurableField1 = new BasicTypes.NullableString() { Value = "w3ölkj" },
                            ConfigurableField2 = new BasicTypes.NullableString() { Value = "d" },
                            ConfigurableField3 = false,
                            Alive = false,
                            Comment = new BasicTypes.NullableString() { Value = "ql334" }
                        },
                        User = new User() { UserId = 5 },
                        Comment = "ösdfaökdj",
                        Type = 3,
                        TimeStamp = new BasicTypes.DateTime() { Ticks = 456321789 }
                    },
                    new DtoTypes.LocationDiff()
                    {
                        OldLocation = new Location()
                        {
                            Id = 26,
                            Number = "aapf98",
                            Description = "aw6e4",
                            ParentDirectoryId = 34,
                            ControlledBy = 31,
                            SetPoint1 = 36,
                            ToleranceClass1 = new ToleranceClass() { Id = 312 },
                            Minimum1 = 35,
                            Maximum1 = 345,
                            Threshold1 = 365,
                            SetPoint2 = 332,
                            ToleranceClass2 = new ToleranceClass() { Id = 310 },
                            Minimum2 = 320,
                            Maximum2 = 330,
                            ConfigurableField1 = new BasicTypes.NullableString() { Value = "sapodfsöadfjö" },
                            ConfigurableField2 = new BasicTypes.NullableString() { Value = "3" },
                            ConfigurableField3 = true,
                            Alive = false,
                            Comment = new BasicTypes.NullableString() { Value = "xcvvydf" }
                        },
                        NewLocation = new Location()
                        {
                            Id = 96,
                            Number = "sldöfjk",
                            Description = "käapreoi",
                            ParentDirectoryId = 94,
                            ControlledBy = 91,
                            SetPoint1 = 96,
                            ToleranceClass1 = new ToleranceClass() { Id = 912 },
                            Minimum1 = 95,
                            Maximum1 = 945,
                            Threshold1 = 965,
                            SetPoint2 = 932,
                            ToleranceClass2 = new ToleranceClass() { Id = 910 },
                            Minimum2 = 920,
                            Maximum2 = 930,
                            ConfigurableField1 = new BasicTypes.NullableString() { Value = "q6w554r0" },
                            ConfigurableField2 = new BasicTypes.NullableString() { Value = "a" },
                            ConfigurableField3 = true,
                            Alive = false,
                            Comment = new BasicTypes.NullableString() { Value = "serf654" }
                        },
                        User = new User() { UserId = 5, UserName = "" },
                        Comment = "ösdfaökdj",
                        Type = 1,
                        TimeStamp = new BasicTypes.DateTime() { Ticks = 9879654 }
                    }
                }
            };
            yield return new ListOfLocationDiff()
            {
                LocationDiffs =
                {
                    new DtoTypes.LocationDiff()
                    {
                        OldLocation = new Location()
                        {
                            Id = 16,
                            Number = "ölaskföasidjf",
                            Description = "öeirui",
                            ParentDirectoryId = 12,
                            ControlledBy = 11,
                            SetPoint1 = 16,
                            ToleranceClass1 = new ToleranceClass() { Id = 112 },
                            Minimum1 = 15,
                            Maximum1 = 145,
                            Threshold1 = 165,
                            SetPoint2 = 132,
                            ToleranceClass2 = new ToleranceClass() { Id = 110 },
                            Minimum2 = 120,
                            Maximum2 = 130,
                            ConfigurableField1 = new BasicTypes.NullableString() { Value = "dpaowiiuef" },
                            ConfigurableField2 = new BasicTypes.NullableString() { Value = "s" },
                            ConfigurableField3 = true,
                            Alive = true,
                            Comment = new BasicTypes.NullableString() { Value = "öalsksjdf" }
                        },
                        NewLocation = new Location()
                        {
                            Id = 26,
                            Number = "2öasidjf",
                            Description = "2k",
                            ParentDirectoryId = 24,
                            ControlledBy = 21,
                            SetPoint1 = 26,
                            ToleranceClass1 = new ToleranceClass() { Id = 212 },
                            Minimum1 = 25,
                            Maximum1 = 245,
                            Threshold1 = 265,
                            SetPoint2 = 232,
                            ToleranceClass2 = new ToleranceClass() { Id = 210 },
                            Minimum2 = 220,
                            Maximum2 = 230,
                            ConfigurableField1 = new BasicTypes.NullableString() { Value = "2söadfjö" },
                            ConfigurableField2 = new BasicTypes.NullableString() { Value = "l" },
                            ConfigurableField3 = false,
                            Alive = false,
                            Comment = new BasicTypes.NullableString() { Value = "209divajof" }
                        },
                        User = new User() { UserId = 5, UserName = "" },
                        Comment = "ösdfaökdj",
                        Type = 2,
                        TimeStamp = new BasicTypes.DateTime() { Ticks = 159357 }
                    }
                }
            };
        }

        private static (HistoryDataAccess dataAccess, HistoryClientMock client) CreateDataAccessTuple()
        {
            var client = new HistoryClientMock();
            var clientFactory = new ClientFactoryMock()
            {
                AuthenticationChannel = new ChannelWrapperMock()
                {
                    GetHistoryClientReturnValue = client
                }
            };
            var dataAccess = new HistoryDataAccess(clientFactory);

            return (dataAccess, client);
        }
    }
}
