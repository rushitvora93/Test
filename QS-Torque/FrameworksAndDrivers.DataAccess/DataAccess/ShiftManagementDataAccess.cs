using System;
using System.Linq;
using Core.UseCases;
using FrameworksAndDrivers.DataAccess.Common;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.DbEntities;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class ShiftManagementDataAccess : DataAccessBase, IShiftManagementData
    {
        private const string ChangeOfDayKey = "NewDay";
        private const string FirstDayOfWeekKey = "FirstDayOfWeek";

        private readonly ITimeDataAccess _timeDataAccess;
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;

        public ShiftManagementDataAccess(ITransactionDbContext transactionContext, SqliteDbContext dbContext, ITimeDataAccess timeDataAccess, IGlobalHistoryDataAccess globalHistoryDataAccess) : base(transactionContext, dbContext)
        {
            _timeDataAccess = timeDataAccess;
            _globalHistoryDataAccess = globalHistoryDataAccess;
        }

        public ShiftManagement GetShiftManagement()
        {
            var dbShifts = _dbContext.Shift_Worktimes
                        .Where(x => x.SeqId == 1 || x.SeqId == 2 || x.SeqId == 3)
                        .OrderBy(x => x.SeqId)
                        .ToList();
                    
            var firstShift = dbShifts.FirstOrDefault(x => x.SeqId == 1);
            var secondShift = dbShifts.FirstOrDefault(x => x.SeqId == 2);
            var thirdShift = dbShifts.FirstOrDefault(x => x.SeqId == 3);

            if (firstShift == null)
            {
                firstShift = new Shift_Worktime()
                {
                    SeqId = 1,
                    Time1 = TimeSpan.FromHours(6).ToString(),
                    Time2 = TimeSpan.FromHours(14).ToString(),
                    Aktiv = 1,
                    Area = 0
                };
                _dbContext.Shift_Worktimes.Add(firstShift);
            }

            if (secondShift == null)
            {
                secondShift = new Shift_Worktime()
                {
                    SeqId = 2,
                    Time1 = TimeSpan.FromHours(14).ToString(),
                    Time2 = TimeSpan.FromHours(22).ToString(),
                    Aktiv = 1,
                    Area = 0
                };
                _dbContext.Shift_Worktimes.Add(secondShift);
            }

            if (thirdShift == null)
            {
                thirdShift = new Shift_Worktime()
                {
                    SeqId = 3,
                    Time1 = TimeSpan.FromHours(22).ToString(),
                    Time2 = TimeSpan.FromHours(6).ToString(),
                    Aktiv = 1,
                    Area = 0
                };
                _dbContext.Shift_Worktimes.Add(thirdShift);
            }
            var changeOfDaySetup = _dbContext.QstSetups
                .Where(x => x.LUserId == 0 && x.SName == ChangeOfDayKey)
                .OrderBy(x => x.Area)
                .FirstOrDefault();
            var firstDayOfWeekSetup = GetFirstDayOfWeek();

            var shiftManagementEntity = new ShiftManagement()
            {
                FirstShiftStart = TimeSpan.Parse(firstShift.Time1),
                FirstShiftEnd = TimeSpan.Parse(firstShift.Time2),
                SecondShiftStart = TimeSpan.Parse(secondShift.Time1),
                SecondShiftEnd = TimeSpan.Parse(secondShift.Time2),
                ThirdShiftStart = TimeSpan.Parse(thirdShift.Time1),
                ThirdShiftEnd = TimeSpan.Parse(thirdShift.Time2),
                IsSecondShiftActive = secondShift.Aktiv.HasValue && secondShift.Aktiv != 0 ? true : false,
                IsThirdShiftActive = thirdShift.Aktiv.HasValue && thirdShift.Aktiv != 0 ? true : false,
                ChangeOfDay = changeOfDaySetup == null ? TimeSpan.Zero : TimeSpan.Parse(changeOfDaySetup.SText),
                FirstDayOfWeek = (DayOfWeek)int.Parse(firstDayOfWeekSetup.SText)
            };

            return shiftManagementEntity;
        }

        private DbEntities.QstSetup GetFirstDayOfWeek()
        {
            var firstDayOfWeekSetup = _dbContext.QstSetups
                .Where(x => x.LUserId == 0 && x.SName == FirstDayOfWeekKey)
                .OrderBy(x => x.Area)
                .FirstOrDefault();

            if (firstDayOfWeekSetup == null)
            {
                firstDayOfWeekSetup = new DbEntities.QstSetup()
                {
                    LID = -1,
                    SName = FirstDayOfWeekKey,
                    Area = 0,
                    LUserId = 0,
                    SText = "1"
                };
                _dbContext.QstSetups.Add(firstDayOfWeekSetup);
                _dbContext.SaveChanges();
            }

            return firstDayOfWeekSetup;
        }

        public void SaveShiftManagement(ShiftManagementDiff diff)
        {
            if (!ShiftManagementUpdateWithHistoryPreconditions(diff.Old))
            {
                AddUseStatistics();
            }
            UpdateShiftManagementDbWithHistory(diff);
        }

        private bool ShiftManagementUpdateWithHistoryPreconditions(ShiftManagement newShiftManagement)
        {
            var dbShifts = _dbContext.Shift_Worktimes
                           .Where(x => x.SeqId == 1 || x.SeqId == 2 || x.SeqId == 3)
                           .OrderBy(x => x.SeqId)
                           .ToList();

            var firstShift = dbShifts.FirstOrDefault(x => x.SeqId == 1);
            var secondShift = dbShifts.FirstOrDefault(x => x.SeqId == 2);
            var thirdShift = dbShifts.FirstOrDefault(x => x.SeqId == 3);

            var changeOfDaySetup = _dbContext.QstSetups
                .Where(x => x.LUserId == 0 && x.SName == ChangeOfDayKey)
                .OrderBy(x => x.Area)
                .FirstOrDefault();
            var firstDayOfWeekSetup = GetFirstDayOfWeek();

            var changeOfDay = changeOfDaySetup == null ? TimeSpan.Zero : TimeSpan.Parse(changeOfDaySetup.SText);
            var firstDayOfWeek = firstDayOfWeekSetup == null ? DayOfWeek.Monday : (DayOfWeek)int.Parse(firstDayOfWeekSetup.SText);

            return newShiftManagement.FirstShiftStart == TimeSpan.Parse(firstShift.Time1) &&
                   newShiftManagement.FirstShiftEnd == TimeSpan.Parse(firstShift.Time2) &&
                   newShiftManagement.SecondShiftStart == TimeSpan.Parse(secondShift.Time1) &&
                   newShiftManagement.SecondShiftEnd == TimeSpan.Parse(secondShift.Time2) &&
                   newShiftManagement.ThirdShiftStart == TimeSpan.Parse(thirdShift.Time1) &&
                   newShiftManagement.ThirdShiftEnd == TimeSpan.Parse(thirdShift.Time2) &&
                   newShiftManagement.IsSecondShiftActive == (secondShift.Aktiv != 0 && secondShift.Aktiv != null) &&
                   newShiftManagement.IsThirdShiftActive == (thirdShift.Aktiv != 0 && thirdShift.Aktiv != null) &&
                   newShiftManagement.ChangeOfDay == changeOfDay &&
                   newShiftManagement.FirstDayOfWeek == firstDayOfWeek;
        }

        private void AddUseStatistics()
        {
            var usageStatistic = new UsageStatistic
            {
                ACTION = UsageStatisticActions.SaveCollision("ShiftManagement"),
                TIMESTAMP = _timeDataAccess.UtcNow()
            };

            _dbContext.UsageStatistics.Add(usageStatistic);
            _dbContext.SaveChanges();
        }

        private void UpdateShiftManagementDbWithHistory(ShiftManagementDiff diff)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryIds = new long[] 
            {
                _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp),
                _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp), 
                _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp) 
            };
            AddSpecificShiftManagementHistoryEntry(globalHistoryIds);

            UpdateShiftManagement(diff, currentTimestamp);

            var shiftManagementChanges = CreateShiftManagementChanges(globalHistoryIds, diff);
            AddShiftManagementChangesForUpdate(diff, shiftManagementChanges);
            _dbContext.SaveChanges();
        }

        private void UpdateShiftManagement(ShiftManagementDiff diff, DateTime currentTimestamp)
        {
            var dbShifts = _dbContext.Shift_Worktimes
                           .Where(x => x.SeqId == 1 || x.SeqId == 2 || x.SeqId == 3)
                           .OrderBy(x => x.SeqId)
                           .ToList();

            var firstShift = dbShifts.FirstOrDefault(x => x.SeqId == 1);
            var secondShift = dbShifts.FirstOrDefault(x => x.SeqId == 2);
            var thirdShift = dbShifts.FirstOrDefault(x => x.SeqId == 3);

            var changeOfDaySetup = _dbContext.QstSetups
                .Where(x => x.LUserId == 0 && x.SName == ChangeOfDayKey)
                .OrderBy(x => x.Area)
                .FirstOrDefault();
            var firstDayOfWeekSetup = GetFirstDayOfWeek();

            UpdateDbFromShiftManagementEntity(firstShift, secondShift, thirdShift, changeOfDaySetup, firstDayOfWeekSetup, diff.New);
        }

        private void UpdateDbFromShiftManagementEntity(Shift_Worktime firstShift, Shift_Worktime secondShift, Shift_Worktime thirdShift, DbEntities.QstSetup changeOfDaySetup, DbEntities.QstSetup firstDayOfWeekSetup, ShiftManagement newShiftManagement)
        {
            firstShift.Time1 = newShiftManagement.FirstShiftStart.ToString();
            firstShift.Time2 = newShiftManagement.FirstShiftEnd.ToString();
            firstShift.Aktiv = 1;
            firstShift.Area = 0;
            secondShift.Time1 = newShiftManagement.SecondShiftStart.ToString();
            secondShift.Time2 = newShiftManagement.SecondShiftEnd.ToString();
            secondShift.Aktiv = newShiftManagement.IsSecondShiftActive ? 1 : 0;
            secondShift.Area = 0;
            thirdShift.Time1 = newShiftManagement.ThirdShiftStart.ToString();
            thirdShift.Time2 = newShiftManagement.ThirdShiftEnd.ToString();
            thirdShift.Aktiv = newShiftManagement.IsThirdShiftActive ? 1 : 0;
            thirdShift.Area = 0;
            changeOfDaySetup.LUserId = 0;
            changeOfDaySetup.SText = newShiftManagement.ChangeOfDay.ToString();
            firstDayOfWeekSetup.LUserId = 0;
            firstDayOfWeekSetup.SText = ((int)newShiftManagement.FirstDayOfWeek).ToString();
        }

        private void AddShiftManagementChangesForUpdate(ShiftManagementDiff diff, ShiftChanges[] changes)
        {
            changes[0].Action = "UPDATE";
            changes[0].Time1Old = diff.Old.FirstShiftStart.ToString();
            changes[0].Time1New = diff.New.FirstShiftStart.ToString();
            changes[0].Time2Old = diff.Old.FirstShiftEnd.ToString();
            changes[0].Time2New = diff.New.FirstShiftEnd.ToString();
            changes[0].ActiveOld = 1;
            changes[0].ActiveNew = 1;
            changes[0].AreaOld = 0;
            changes[0].AreaNew = 0;
            changes[1].Action = "UPDATE";
            changes[1].Time1Old = diff.Old.SecondShiftStart.ToString();
            changes[1].Time1New = diff.New.SecondShiftStart.ToString();
            changes[1].Time2Old = diff.Old.SecondShiftEnd.ToString();
            changes[1].Time2New = diff.New.SecondShiftEnd.ToString();
            changes[1].ActiveOld = diff.Old.IsSecondShiftActive ? 1 : 0;
            changes[1].ActiveNew = diff.New.IsSecondShiftActive ? 1 : 0;
            changes[1].AreaOld = 0;
            changes[1].AreaNew = 0;
            changes[2].Action = "UPDATE";
            changes[2].Time1Old = diff.Old.ThirdShiftStart.ToString();
            changes[2].Time1New = diff.New.ThirdShiftStart.ToString();
            changes[2].Time2Old = diff.Old.ThirdShiftEnd.ToString();
            changes[2].Time2New = diff.New.ThirdShiftEnd.ToString();
            changes[2].ActiveOld = diff.Old.IsThirdShiftActive ? 1 : 0;
            changes[2].ActiveNew = diff.New.IsThirdShiftActive ? 1 : 0;
            changes[2].AreaOld = 0;
            changes[2].AreaNew = 0;

            _dbContext.ShiftChanges.Add(changes[0]);
            _dbContext.ShiftChanges.Add(changes[1]);
            _dbContext.ShiftChanges.Add(changes[2]);
        }

        private ShiftChanges[] CreateShiftManagementChanges(long[] globalHistoryIds, ShiftManagementDiff diff)
        {
            return new ShiftChanges[] { 
                new ShiftChanges()
                {
                    GlobalHistoryId = globalHistoryIds[0],
                    SeqId = 1,
                    UserId = diff.User.UserId.ToLong(),
                    UserComment = diff.Comment.ToDefaultString()
                },
                new ShiftChanges()
                {
                    GlobalHistoryId = globalHistoryIds[1],
                    SeqId = 2,
                    UserId = diff.User.UserId.ToLong(),
                    UserComment = diff.Comment.ToDefaultString()
                },
                new ShiftChanges()
                {
                    GlobalHistoryId = globalHistoryIds[2],
                    SeqId = 3,
                    UserId = diff.User.UserId.ToLong(),
                    UserComment = diff.Comment.ToDefaultString()
                }
            };
        }

        private void AddSpecificShiftManagementHistoryEntry(long[] globalHistoryIds)
        {
            var shiftHistory1 = new ShiftHistory() { GLOBALHISTORYID = globalHistoryIds[0] };
            var shiftHistory2 = new ShiftHistory() { GLOBALHISTORYID = globalHistoryIds[1] };
            var shiftHistory3 = new ShiftHistory() { GLOBALHISTORYID = globalHistoryIds[2] };
            _dbContext.ShiftHistory.Add(shiftHistory1);
            _dbContext.ShiftHistory.Add(shiftHistory2);
            _dbContext.ShiftHistory.Add(shiftHistory3);
        }
    }
}
