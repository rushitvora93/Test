using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("FREEDAY_LIST")]
    public class WorkingCalendar
    {
        [Key]
        public long SeqId { get; set; }

        public String Name { get; set; }
        public long? Role { get; set; }
        public long? FdSat { get; set; }
        public long? FdSun { get; set; }
        public DateTime? TSN { get; set; }
        public DateTime? TSA { get; set; }
        public long? ServId { get; set; }
    }

    [Table("WorkingCalendarChanges")]
    public class WorkingCalendarChanges
    {
        [Key]
        public long GlobalHistoryId { get; set; }

        public long SeqId { get; set; }
        public long? UserId { get; set; }
        public string Action { get; set; }
        public string UserComment { get; set; }
        public long? FdSatOld { get; set; }
        public long? FdSatNew { get; set; }
        public long? FdSunOld { get; set; }
        public long? FdSunNew { get; set; }
    }
}
