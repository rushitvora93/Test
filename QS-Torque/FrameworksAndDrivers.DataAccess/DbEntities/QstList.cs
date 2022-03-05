using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("QST_LIST")]
    public class QstList
    {
        [Key]
        public long LISTID { get; set; }
        public long NODEID { get; set; }
        public String INFO { get; set; }
        public DateTime? TSN { get; set; }
        public DateTime? TSA { get; set; }
        public long SERVID { get; set; }
        public bool ALIVE { get; set; }
    }

    [Table("QST_LISTCHANGES")]
    public class QstListChanges
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
        public long LISTID { get; set; }
        public long NODEID { get; set; }
        public long USERID { get; set; }
        public String ACTION { get; set; }
        public String USERCOMMENT { get; set; }
        public String INFOOLD { get; set; }
        public String INFONEW { get; set; }
        public bool? ALIVEOLD { get; set; }
        public bool? ALIVENEW { get; set; }
    }
}
