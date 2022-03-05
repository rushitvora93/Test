using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("WORKTIME")]
    public class Shift_Worktime
    {
        [Key]
        public long SeqId { get; set; }

        public string Name { get; set; }
        public string Von { get; set; }
        public string Bis { get; set; }
        public long? Aktiv { get; set; }
        public DateTime? TSA { get; set; }
        public DateTime? TSN { get; set; }
        public string Time1 { get; set; }
        public string Time2 { get; set; }
        public long? Area { get; set; }
        public long? Role { get; set; }
    }

    [Table("ShiftChanges")]
    public class ShiftChanges
    {
        [Key]
        public long GlobalHistoryId { get; set; }

        public long SeqId { get; set; }
        public long UserId { get; set; }
        public string Action { get; set; }
        public string UserComment { get; set; }
        public string NameOld { get; set; }
        public string NameNew { get; set; }
        public long? ActiveOld { get; set; }
        public long? ActiveNew { get; set; }
        public string Time1Old { get; set; }
        public string Time1New { get; set; }
        public string Time2Old { get; set; }
        public string Time2New { get; set; }
        public long? AreaOld { get; set; }
        public long? AreaNew { get; set; }
    }
}
