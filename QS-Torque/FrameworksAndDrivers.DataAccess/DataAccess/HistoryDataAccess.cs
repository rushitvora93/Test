using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Enums;
using Server.UseCases.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class HistoryDataAccess : DataAccessBase, IHistoryData
    {
        public HistoryDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext)
            : base(transactionContext, dbContext)
        { }

        public List<LocationDiff> LoadLocationDiffsFor(LocationId id)
        {
            var changes = _dbContext.LocationChanges
                .Where(x => x.LOCATIONID == id.ToLong())
                .Join(_dbContext.GlobalHistories,
                    x => x.GLOBALHISTORYID,
                    y => y.ID,
                    (x, y) => new { LocationChanges = x, GlobalHistory = y })
                .OrderBy(x => x.GlobalHistory.TIMESTAMP)
                .ToList();
            var diffs = new List<LocationDiff>();
            var mapper = new Mapper();

            for (int i = 0; i < changes.Count(); i++)
            {
                var dbUser = _dbContext.QstUsers.FirstOrDefault(x => x.USERID == changes[i].LocationChanges.USERID);
                diffs.Add(new LocationDiff(
                    dbUser == null ? null : mapper.DirectPropertyMapping(dbUser),
                    new HistoryComment(changes[i].LocationChanges.USERCOMMENT),
                    GetOldLocation(changes[i].LocationChanges),
                    GetNewLocation(changes[i].LocationChanges))
                {
                    Type = DiffTypeUtil.ConvertToDiffType(changes[i].LocationChanges.ACTION),
                    TimeStamp = changes[i].GlobalHistory.TIMESTAMP.GetValueOrDefault()
                });
            }

            return diffs;
        }

        private Location GetOldLocation(DbEntities.LocationChanges diff)
        {
            var mapper = new T4Mapper.Mapper();
            var oldLocation = new Location();
            oldLocation.Number = new LocationNumber(diff.USERIDOLD);
            oldLocation.Description = new LocationDescription(diff.NAMEOLD);
            oldLocation.ParentDirectoryId = diff.TREEIDOLD == null ? null : new LocationDirectoryId(diff.TREEIDOLD.Value);
            oldLocation.ControlledBy = ((LocationControlledBy?)diff.CONTROLOLD) ?? LocationControlledBy.Torque;
            oldLocation.SetPoint1 = diff.TNOMOLD.GetValueOrDefault();
            oldLocation.Minimum1 = diff.TMINOLD.GetValueOrDefault();
            oldLocation.Maximum1 = diff.TMAXOLD.GetValueOrDefault();
            oldLocation.Threshold1 = diff.TSTARTOLD.GetValueOrDefault();
            oldLocation.SetPoint2 = diff.NOM2OLD.GetValueOrDefault();
            oldLocation.Minimum2 = diff.MIN2OLD.GetValueOrDefault();
            oldLocation.Maximum2 = diff.MAX2OLD.GetValueOrDefault();
            oldLocation.ConfigurableField1 = new LocationConfigurableField1(diff.KOSTOLD); ;
            oldLocation.ConfigurableField2 = new LocationConfigurableField2(diff.KATEGOLD);
            oldLocation.ConfigurableField3 = diff.DOKUOLD.GetValueOrDefault(false);
            oldLocation.Alive = diff.ALIVEOLD.GetValueOrDefault(false);
            if (diff.CLASSIDOLD != null)
            {
                oldLocation.ToleranceClass1 = mapper.DirectPropertyMapping(_dbContext.ToleranceClasses.Where(x => x.CLASSID == diff.CLASSIDOLD).First()); 
            }
            if (diff.CLASSIDOLD != null)
            {
                oldLocation.ToleranceClass2 = mapper.DirectPropertyMapping(_dbContext.ToleranceClasses.Where(x => x.CLASSID == diff.CLASSID2OLD).First());
            }
            return oldLocation;
        }
        private Location GetNewLocation(DbEntities.LocationChanges diff)
        {
            var mapper = new T4Mapper.Mapper();
            var newLocation = new Location();
            newLocation.Number = new LocationNumber(diff.USERIDNEW);
            newLocation.Description = new LocationDescription(diff.NAMENEW);
            newLocation.ParentDirectoryId = diff.TREEIDNEW == null ? null : new LocationDirectoryId(diff.TREEIDNEW.Value);
            newLocation.ControlledBy = ((LocationControlledBy?)diff.CONTROLNEW) ?? LocationControlledBy.Torque;
            newLocation.SetPoint1 = diff.TNOMNEW.GetValueOrDefault();
            newLocation.Minimum1 = diff.TMINNEW.GetValueOrDefault();
            newLocation.Maximum1 = diff.TMAXNEW.GetValueOrDefault();
            newLocation.Threshold1 = diff.TSTARTNEW.GetValueOrDefault();
            newLocation.SetPoint2 = diff.NOM2NEW.GetValueOrDefault();
            newLocation.Minimum2 = diff.MIN2NEW.GetValueOrDefault();
            newLocation.Maximum2 = diff.MAX2NEW.GetValueOrDefault();
            newLocation.ConfigurableField1 = new LocationConfigurableField1(diff.KOSTNEW); ;
            newLocation.ConfigurableField2 = new LocationConfigurableField2(diff.KATEGNEW);
            newLocation.ConfigurableField3 = diff.DOKUNEW.GetValueOrDefault(false);
            newLocation.Alive = diff.ALIVENEW.GetValueOrDefault(false);
            if (diff.CLASSIDNEW != null)
            {
                newLocation.ToleranceClass1 = mapper.DirectPropertyMapping(_dbContext.ToleranceClasses.Where(x => x.CLASSID == diff.CLASSIDNEW).First());
            }
            if (diff.CLASSIDNEW != null)
            {
                newLocation.ToleranceClass2 = mapper.DirectPropertyMapping(_dbContext.ToleranceClasses.Where(x => x.CLASSID == diff.CLASSID2NEW).First());
            }
            return newLocation;
        }
    }
}
