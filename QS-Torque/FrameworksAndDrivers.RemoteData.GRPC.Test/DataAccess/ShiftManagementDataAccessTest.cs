using System;
using System.Collections.Generic;
using System.Linq;
using Client.TestHelper.Mock;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using TestHelper.Mock;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    class ShiftManagementClientMock : IShiftManagementClient
    {
        public DtoTypes.ShiftManagement GetShiftManagementReturnValue { get; set; }
        public DtoTypes.ShiftManagementDiff SaveShiftManagementDiffParameter { get; set; }

        public ShiftManagement GetShiftManagement()
        {
            return GetShiftManagementReturnValue;
        }

        public void SaveShiftManagement(ShiftManagementDiff diff)
        {
            SaveShiftManagementDiffParameter = diff;
        }
    }


    public class ShiftManagementDataAccessTest
    {
        [TestCaseSource(nameof(CreateAnonymousShiftManagementDtos))]
        public void LoadShiftManagementReturnsDataFromClient(DtoTypes.ShiftManagement dto)
        {
            var tuple = CreateDataAccessTuple();
            tuple.client.GetShiftManagementReturnValue = dto;
            var result = tuple.dataAccess.LoadShiftManagement();
            Assert.IsTrue(AreShiftManagementEntityAndDtoEqual(result, dto));
        }

        [TestCaseSource(nameof(CreateAnonymousShiftManagementDiffs))]
        public void SaveShiftManagementPassesDataToClient(Client.Core.Diffs.ShiftManagementDiff diff)
        {
            var tuple = CreateDataAccessTuple();
            tuple.dataAccess.SaveShiftManagement(diff);
            Assert.IsTrue(AreShiftManagementDiffAndDtoEqual(diff, tuple.client.SaveShiftManagementDiffParameter));
        }


        static bool AreShiftManagementEntityAndDtoEqual(Core.Entities.ShiftManagement entity, DtoTypes.ShiftManagement dto)
        {
            return entity.FirstShiftStart.Ticks == dto.FirstShiftStart.ValueInTicks &&
                   entity.FirstShiftEnd.Ticks == dto.FirstShiftEnd.ValueInTicks &&
                   entity.SecondShiftStart.Ticks == dto.SecondShiftStart.ValueInTicks &&
                   entity.SecondShiftEnd.Ticks == dto.SecondShiftEnd.ValueInTicks &&
                   entity.ThirdShiftStart.Ticks == dto.ThirdShiftStart.ValueInTicks &&
                   entity.ThirdShiftEnd.Ticks == dto.ThirdShiftEnd.ValueInTicks &&
                   entity.IsSecondShiftActive == dto.IsSecondShiftActive &&
                   entity.IsThirdShiftActive == dto.IsThirdShiftActive &&
                   entity.ChangeOfDay.Ticks == dto.ChangeOfDay.ValueInTicks &&
                   (long)entity.FirstDayOfWeek == dto.FirstDayOfWeek;
        }

        static bool AreShiftManagementDiffAndDtoEqual(Client.Core.Diffs.ShiftManagementDiff diff, DtoTypes.ShiftManagementDiff dto)
        {
            return AreShiftManagementEntityAndDtoEqual(diff.Old, dto.Old) &&
                AreShiftManagementEntityAndDtoEqual(diff.New, dto.New) &&
                diff.Comment.ToDefaultString() == dto.Comment &&
                diff.User.UserId.ToLong() == dto.UserId;
        }

        static IEnumerable<Client.Core.Diffs.ShiftManagementDiff> CreateAnonymousShiftManagementDiffs()
        {
            var entities = CreateAnonymousShiftManagements().ToList();

            yield return new Client.Core.Diffs.ShiftManagementDiff(entities[0], entities[1], new Core.Entities.User() { UserId = new Core.Entities.UserId(2) }, new Core.Entities.HistoryComment("gufh"));
            yield return new Client.Core.Diffs.ShiftManagementDiff(entities[2], entities[3], new Core.Entities.User() { UserId = new Core.Entities.UserId(3) }, new Core.Entities.HistoryComment("sdrgajc,"));
        }

        static IEnumerable<Core.Entities.ShiftManagement> CreateAnonymousShiftManagements()
        {
            yield return new Core.Entities.ShiftManagement()
            {
                FirstShiftStart = new System.TimeSpan(987652),
                FirstShiftEnd = new System.TimeSpan(567),
                SecondShiftStart = new System.TimeSpan(876),
                SecondShiftEnd = new System.TimeSpan(852),
                ThirdShiftStart = new System.TimeSpan(7413),
                ThirdShiftEnd = new System.TimeSpan(287),
                IsSecondShiftActive = false,
                IsThirdShiftActive = true,
                ChangeOfDay = new System.TimeSpan(86),
                FirstDayOfWeek = DayOfWeek.Tuesday
            };
            yield return new Core.Entities.ShiftManagement()
            {
                FirstShiftStart = new System.TimeSpan(8732),
                FirstShiftEnd = new System.TimeSpan(123),
                SecondShiftStart = new System.TimeSpan(489),
                SecondShiftEnd = new System.TimeSpan(7524),
                ThirdShiftStart = new System.TimeSpan(23456),
                ThirdShiftEnd = new System.TimeSpan(09),
                IsSecondShiftActive = true,
                IsThirdShiftActive = false,
                ChangeOfDay = new System.TimeSpan(2345),
                FirstDayOfWeek = DayOfWeek.Friday
            };
            yield return new Core.Entities.ShiftManagement()
            {
                FirstShiftStart = new System.TimeSpan(87732),
                FirstShiftEnd = new System.TimeSpan(1273),
                SecondShiftStart = new System.TimeSpan(4789),
                SecondShiftEnd = new System.TimeSpan(75274),
                ThirdShiftStart = new System.TimeSpan(234756),
                ThirdShiftEnd = new System.TimeSpan(097),
                IsSecondShiftActive = true,
                IsThirdShiftActive = true,
                ChangeOfDay = new System.TimeSpan(27345),
                FirstDayOfWeek = DayOfWeek.Wednesday
            };
            yield return new Core.Entities.ShiftManagement()
            {
                FirstShiftStart = new System.TimeSpan(87932),
                FirstShiftEnd = new System.TimeSpan(1293),
                SecondShiftStart = new System.TimeSpan(4989),
                SecondShiftEnd = new System.TimeSpan(75924),
                ThirdShiftStart = new System.TimeSpan(239456),
                ThirdShiftEnd = new System.TimeSpan(099),
                IsSecondShiftActive = false,
                IsThirdShiftActive = false,
                ChangeOfDay = new System.TimeSpan(23945),
                FirstDayOfWeek = DayOfWeek.Monday
            };
        }

        static IEnumerable<DtoTypes.ShiftManagement> CreateAnonymousShiftManagementDtos()
        {
            yield return new DtoTypes.ShiftManagement()
            {
                FirstShiftStart = new BasicTypes.TimeSpan() { ValueInTicks = 987652 },
                FirstShiftEnd = new BasicTypes.TimeSpan() { ValueInTicks = 567 },
                SecondShiftStart = new BasicTypes.TimeSpan() { ValueInTicks = 876 },
                SecondShiftEnd = new BasicTypes.TimeSpan() { ValueInTicks = 852 },
                ThirdShiftStart = new BasicTypes.TimeSpan() { ValueInTicks = 7413 },
                ThirdShiftEnd = new BasicTypes.TimeSpan() { ValueInTicks = 287 },
                IsSecondShiftActive = false,
                IsThirdShiftActive = true,
                ChangeOfDay = new BasicTypes.TimeSpan() { ValueInTicks = 86 },
                FirstDayOfWeek = 1
            };
            yield return new DtoTypes.ShiftManagement()
            {
                FirstShiftStart = new BasicTypes.TimeSpan() { ValueInTicks = 8732 },
                FirstShiftEnd = new BasicTypes.TimeSpan() { ValueInTicks = 123 },
                SecondShiftStart = new BasicTypes.TimeSpan() { ValueInTicks = 489 },
                SecondShiftEnd = new BasicTypes.TimeSpan() { ValueInTicks = 7524 },
                ThirdShiftStart = new BasicTypes.TimeSpan() { ValueInTicks = 23456 },
                ThirdShiftEnd = new BasicTypes.TimeSpan() { ValueInTicks = 09 },
                IsSecondShiftActive = true,
                IsThirdShiftActive = false,
                ChangeOfDay = new BasicTypes.TimeSpan() { ValueInTicks = 2345 },
                FirstDayOfWeek = 4
            };
        }

        static (ShiftManagementDataAccess dataAccess, ShiftManagementClientMock client) CreateDataAccessTuple()
        {
            var client = new ShiftManagementClientMock();
            var clientFactory = new ClientFactoryMock()
            {
                AuthenticationChannel = new ChannelWrapperMock()
                {
                    GetShiftManagementClientReturnValue = client
                }
            };
            var dataAccess = new ShiftManagementDataAccess(clientFactory);

            return (dataAccess, client);
        }
    }
}
