using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("LOCTREE")]
    public class LocationDirectory
    {
        [Key]
        public long LOCTREEID { get; set; }
        [Column("PARENT")]
        public long? PARENTID { get; set; }
        public long? POS { get; set; }
        public long? TYPE { get; set; }
        public bool? STATUS { get; set; }
        public long? STATION { get; set; }
        public DateTime? TSA { get; set; }
        public DateTime? TSN { get; set; }
        public string NAME { get; set; }
        public long SERVID { get; set; }
        public long? ID_PDL2_TREE { get; set; }
    }

    [Table("LOCATIONDIRECTORYCHANGES")]
    public class LocationDirectoryChanges
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
        public long LOCATIONDIRECTORYID { get; set; }
        public long USERID { get; set; }
        public String ACTION { get; set; }
        public String USERCOMMENT { get; set; }
        public string NAMEOLD { get; set; }
        public string NAMENEW { get; set; }
        public long? PARENTOLD { get; set; }
        public long? PARENTNEW { get; set; }
        public bool? STATUSOLD { get; set; }
        public bool? STATUSNEW { get; set; }
    }
}
