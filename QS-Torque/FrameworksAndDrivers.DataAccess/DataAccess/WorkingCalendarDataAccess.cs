using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using FrameworksAndDrivers.DataAccess.Common;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.DbEntities;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class WorkingCalendarDataAccess : DataAccessBase, IWorkingCalendarData
    {
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;
        private readonly ITimeDataAccess _timeDataAccess;

        public WorkingCalendarDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext,
            IGlobalHistoryDataAccess globalHistoryDataAccess, ITimeDataAccess timeDataAccess)
            : base(transactionContext, dbContext)
        {
            _globalHistoryDataAccess = globalHistoryDataAccess;
            _timeDataAccess = timeDataAccess;
        }


        public Server.Core.Entities.WorkingCalendar GetWorkingCalendar()
        {
            if(_dbContext.WorkingCalendars.Any())
            {
                var minId = _dbContext.WorkingCalendars.Min(x => x.SeqId);
                var calendar = _dbContext.WorkingCalendars.First(x => x.SeqId == minId);
                var mapper = new Mapper();
                return mapper.DirectPropertyMapping(calendar);
            }
            else
            {
                return new Server.Core.Entities.WorkingCalendar() { Id = new WorkingCalendarId(1) };
            }
        }

        public List<Server.Core.Entities.WorkingCalendarEntry> GetWorkingCalendarEntriesForWorkingCalendarId(WorkingCalendarId id)
        {
            var dbEntries = _dbContext.WorkingCalendarEntries.Where(x => x.Area == id.ToLong()).ToList();
            var entities = new List<Server.Core.Entities.WorkingCalendarEntry>();
            var mapper = new Mapper();

            foreach (var dbEntry in dbEntries)
            {
                entities.Add(mapper.DirectPropertyMapping(dbEntry));
            }
            return entities;
        }

        public void InsertWorkingCalendarEntry(Server.Core.Entities.WorkingCalendarEntry newEntry, WorkingCalendarId calendarId)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
            var workingCalendarEntryHistory = new WorkingCalendarEntryHistory() { GLOBALHISTORYID = globalHistoryId };
            _dbContext.WorkingCalendarEntryHistories.Add(workingCalendarEntryHistory);

            var mapper = new Mapper();
            var dbEntry = mapper.DirectPropertyMapping(newEntry);
            dbEntry.Area = calendarId.ToLong();
            _dbContext.WorkingCalendarEntries.Add(dbEntry);

            var workingCalendarChanges = new WorkingCalendarEntryChanges()
            {
                GlobalHistoryId = globalHistoryId,
                FDDateNew = newEntry.Date,
                NameNew = newEntry.Description.ToDefaultString(),
                AreaNew = 0,
                RepeatNew = (long)newEntry.Repetition,
                IsFreeNew = (long)newEntry.Type
            };
            _dbContext.WorkingCalendarEntryChanges.Add(workingCalendarChanges);
            _dbContext.SaveChanges();
        }

        public void DeleteWorkingCalendarEntry(Server.Core.Entities.WorkingCalendarEntry oldEntry)
        {
            var dbEntry = _dbContext.WorkingCalendarEntries
                .FirstOrDefault(x => x.FDDate.Date == oldEntry.Date.Date && x.Name == oldEntry.Description.ToDefaultString());

            if (dbEntry == null)
            {
                return;
            }

            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA DELETED", currentTimestamp);
            var workingCalendarEntryHistory = new WorkingCalendarEntryHistory() { GLOBALHISTORYID = globalHistoryId };
            _dbContext.WorkingCalendarEntryHistories.Add(workingCalendarEntryHistory);

            _dbContext.WorkingCalendarEntries.Remove(dbEntry);
            _dbContext.SaveChanges();

            var workingCalendarChanges = new WorkingCalendarEntryChanges()
            {
                GlobalHistoryId = globalHistoryId,
                FDDateOld = oldEntry.Date,
                NameOld = oldEntry.Description.ToDefaultString(),
                AreaOld = 0,
                RepeatOld = (long)oldEntry.Repetition,
                IsFreeOld = (long)oldEntry.Type
            };
            _dbContext.WorkingCalendarEntryChanges.Add(workingCalendarChanges);
            _dbContext.SaveChanges();
        }

        public void SaveWorkingCalendar(WorkingCalendarDiff diff)
        {
            var dbEntry = _dbContext.WorkingCalendars.FirstOrDefault(x => x.SeqId == 1);

            if (dbEntry.FdSat != (diff.Old.AreSaturdaysFree ? 1 : 0) || dbEntry.FdSun != (diff.Old.AreSundaysFree ? 1 : 0))
            {
                var usageStatistic = new UsageStatistic
                {
                    ACTION = UsageStatisticActions.SaveCollision("WorkingCalendar"),
                    TIMESTAMP = _timeDataAccess.UtcNow()
                };

                _dbContext.UsageStatistics.Add(usageStatistic);
                _dbContext.SaveChanges();
            }

            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
            var workingCalendarHistory = new WorkingCalendarHistory() { GLOBALHISTORYID = globalHistoryId };
            _dbContext.WorkingCalendarHistories.Add(workingCalendarHistory);

            dbEntry.FdSat = diff.New.AreSaturdaysFree ? 1 : 0;
            dbEntry.FdSun = diff.New.AreSundaysFree ? 1 : 0;
            dbEntry.TSA = currentTimestamp;

            var changes = new WorkingCalendarChanges()
            {
                GlobalHistoryId = globalHistoryId,
                UserId = diff.User.UserId.ToLong(),
                UserComment = diff.Comment.ToDefaultString(),
                Action = "DATA UPDATED",
                SeqId = diff.New.Id.ToLong(),
                FdSatOld = diff.Old.AreSaturdaysFree ? 1 : 0,
                FdSatNew = diff.New.AreSaturdaysFree ? 1 : 0,
                FdSunOld = diff.Old.AreSundaysFree ? 1 : 0,
                FdSunNew = diff.New.AreSundaysFree ? 1 : 0
            };
            _dbContext.WorkingCalendarChanges.Add(changes);
            _dbContext.SaveChanges();
        }
    }
}
