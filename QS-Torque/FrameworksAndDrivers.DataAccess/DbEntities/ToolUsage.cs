using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("POWPOS")]
    public class ToolUsage
    {
        [Key]
        public long POSID { get; set; }
        public String POSNAME { get; set; }
        public bool? ALIVE { get; set; }
        public DateTime? TSN { get; set; }
        public DateTime? TSA { get; set; }
        public long SERVID { get; set; }
        public ICollection<LocPow> LocPows { get; set; }
    }

    [Table("TOOLUSAGECHANGES")]
    public class ToolUsageChanges
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
        public long TOOLUSAGEID { get; set; }
        public long USERID { get; set; }
        public string ACTION { get; set; }
        public string USERCOMMENT { get; set; }
        public string POSNAMEOLD { get; set; }
        public string POSNAMENEW { get; set; }
        public bool? ALIVEOLD { get; set; }
        public bool? ALIVENEW { get; set; }
    }
}
