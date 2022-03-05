using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("TestLevel")]
    public class TestLevel
    {
        [Key] 
        public long Id { get; set; }

        public long? IntervalValue { get; set; }
        public long? IntervalType { get; set; }
        public long? SampleNumber { get; set; }
        public long? ConsiderWorkingCalendar { get; set; }
        public long? TestLevelSetId { get; set; }
        public long? TestLevelNumber { get; set; }
        public long? IsActive { get; set; }
    }

    [Table("TestLevelChanges")]
    public class TestLevelChanges
    {
        [Key]
        public long GlobalHistoryId { get; set; }

        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserComment { get; set; }
        public string Action { get; set; }
        public long? TestLevelSetId { get; set; }
        public long? TestLevelNumber { get; set; }
        public long? IntervalValueOld { get; set; }
        public long? IntervalValueNew { get; set; }
        public long? IntervalTypeOld { get; set; }
        public long? IntervalTypeNew { get; set; }
        public long? SampleNumberOld { get; set; }
        public long? SampleNumberNew { get; set; }
        public long? ConsiderWorkingCalendarOld { get; set; }
        public long? ConsiderWorkingCalendarNew { get; set; }
        public long? IsActiveOld { get; set; }
        public long? IsActiveNew { get; set; }
    }
}
