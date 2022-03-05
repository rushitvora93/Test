using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    class ShiftManagementUseCaseMock : IShiftManagementUseCase
    {
        public ShiftManagement GetShiftManagementReturnValue { get; set; }
        public ShiftManagementDiff SaveShiftManagementDiffParameter { get; set; }

        public ShiftManagement GetShiftManagement()
        {
            return GetShiftManagementReturnValue;
        }

        public void SaveShiftManagement(ShiftManagementDiff diff)
        {
            SaveShiftManagementDiffParameter = diff;
        }
    }

    public class ShiftManagementServiceTest
    {
        [TestCaseSource(nameof(CreateAnonymousShiftManagements))]
        public void GetShiftManagementReturnsDataOfUseCase(ShiftManagement entity)
        {
            var tuple = CreateServiceTuple();
            tuple.useCase.GetShiftManagementReturnValue = entity;
            var result = tuple.service.GetShiftManagement(new NoParams(), null).Result;
            Assert.IsTrue(AreShiftManagementEntityAndDtoEqual(entity, result));
        }

        [TestCaseSource(nameof(CreateAnonymousShiftManagementDiffs))]
        public void SaveShiftManagementWithHistoryPassesDataToUseCase(DtoTypes.ShiftManagementDiff dto)
        {
            var tuple = CreateServiceTuple();
            tuple.service.SaveShiftManagement(dto, null);
            Assert.IsTrue(AreShiftManagementDiffAndDtoEqual(tuple.useCase.SaveShiftManagementDiffParameter, dto));
        }


        static bool AreShiftManagementEntityAndDtoEqual(ShiftManagement entity, DtoTypes.ShiftManagement dto)
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

        static bool AreShiftManagementDiffAndDtoEqual(ShiftManagementDiff diff, DtoTypes.ShiftManagementDiff dto)
        {
            return AreShiftManagementEntityAndDtoEqual(diff.Old, dto.Old) &&
                AreShiftManagementEntityAndDtoEqual(diff.New, dto.New) &&
                diff.Comment.ToDefaultString() == dto.Comment &&
                diff.User.UserId.ToLong() == dto.UserId;
        }

        static IEnumerable<DtoTypes.ShiftManagementDiff> CreateAnonymousShiftManagementDiffs()
        {
            var dtos = CreateAnonymousShiftManagementDtos().ToList();

            yield return new DtoTypes.ShiftManagementDiff() { Old = dtos[0], New = dtos[1], UserId = 5, Comment = "gzfuidospguh" };
            yield return new DtoTypes.ShiftManagementDiff() { Old = dtos[2], New = dtos[3], UserId = 8, Comment = "dfgdrtbhuzmki.ö" };
        }

        static IEnumerable<ShiftManagement> CreateAnonymousShiftManagements()
        {
            yield return new ShiftManagement()
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
            yield return new ShiftManagement()
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
            yield return new DtoTypes.ShiftManagement()
            {
                FirstShiftStart = new BasicTypes.TimeSpan() { ValueInTicks = 9807652 },
                FirstShiftEnd = new BasicTypes.TimeSpan() { ValueInTicks = 5607 },
                SecondShiftStart = new BasicTypes.TimeSpan() { ValueInTicks = 8076 },
                SecondShiftEnd = new BasicTypes.TimeSpan() { ValueInTicks = 8502 },
                ThirdShiftStart = new BasicTypes.TimeSpan() { ValueInTicks = 74013 },
                ThirdShiftEnd = new BasicTypes.TimeSpan() { ValueInTicks = 2087 },
                IsSecondShiftActive = true,
                IsThirdShiftActive = true,
                ChangeOfDay = new BasicTypes.TimeSpan() { ValueInTicks = 806 },
                FirstDayOfWeek = 2
            };
            yield return new DtoTypes.ShiftManagement()
            {
                FirstShiftStart = new BasicTypes.TimeSpan() { ValueInTicks = 87832 },
                FirstShiftEnd = new BasicTypes.TimeSpan() { ValueInTicks = 1283 },
                SecondShiftStart = new BasicTypes.TimeSpan() { ValueInTicks = 4889 },
                SecondShiftEnd = new BasicTypes.TimeSpan() { ValueInTicks = 75824 },
                ThirdShiftStart = new BasicTypes.TimeSpan() { ValueInTicks = 238456 },
                ThirdShiftEnd = new BasicTypes.TimeSpan() { ValueInTicks = 809 },
                IsSecondShiftActive = false,
                IsThirdShiftActive = false,
                ChangeOfDay = new BasicTypes.TimeSpan() { ValueInTicks = 28345 },
                FirstDayOfWeek = 6
            };
        }

        static (NetworkView.Services.ShiftManagementService service, ShiftManagementUseCaseMock useCase) CreateServiceTuple()
        {
            var useCase = new ShiftManagementUseCaseMock();
            var service = new NetworkView.Services.ShiftManagementService(null, useCase);
            return (service, useCase);
        }
    }
}
