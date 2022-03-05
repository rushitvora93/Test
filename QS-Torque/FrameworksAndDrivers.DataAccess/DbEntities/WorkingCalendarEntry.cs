using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("FREEDAY")]
    public class WorkingCalendarEntry
    {
        [Key, Column(Order = 1)] 
        public DateTime FDDate { get; set; }
        [Key, Column(Order = 2)]
        public String Name { get; set; }
        [Key, Column(Order = 3)]
        public long Area { get; set; }
        public long? Repeat { get; set; }
        public long? IsFree { get; set; }
        public long? Role { get; set; }
    }

    [Table("WorkingCalendarEntryChanges")]
    public class WorkingCalendarEntryChanges
    {
        [Key]
        public long GlobalHistoryId { get; set; }
        
        public DateTime FDDateOld { get; set; }
        public DateTime FDDateNew { get; set; }
        public String NameOld { get; set; }
        public String NameNew { get; set; }
        public long AreaOld { get; set; }
        public long AreaNew { get; set; }
        public long? RepeatOld { get; set; }
        public long? RepeatNew { get; set; }
        public long? IsFreeOld { get; set; }
        public long? IsFreeNew { get; set; }
    }
}
