using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("STATE_ID")]
    public class Status
    {
        [Key] 
        public long STATEID { get; set; }
        public string STATE { get; set; }
        public DateTime? TSN { get; set; }
        public DateTime? TSA { get; set; }
        public long SERVID { get; set; }
        public bool? ALIVE { get; set; }
        public ICollection<Tool> Tools { get; set; }
    }

    [Table("STATUSCHANGES")]
    public class StatusChanges
    {
        [Key] 
        public long GLOBALHISTORYID { get; set; }
        [Column("StatusId")] 
        public long STATEID { get; set; }
        public long USERID { get; set; }
        public string ACTION { get; set; }
        public string USERCOMMENT { get; set; }
        public string STATEOLD { get; set; }
        public string STATENEW { get; set; }
        public bool? ALIVEOLD { get; set; }
        public bool? ALIVENEW { get; set; }
    }
}
