using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("LOCPOW")]
    public class LocPow
    {
        [Key] 
        public long LocPowId { get; set; }
        public long? LocId { get; set; }
        public long? PowId { get; set; }
        public bool? Alive { get; set; }
        [Column("ORD")] 
        public long? PowPosId { get; set; }
        public DateTime? TSN { get; set; }
        public DateTime? TSA { get; set; }
        public long? STATION { get; set; }
        public long? SERVID { get; set; }
        public long? SAMEMOD { get; set; }
        public long? IPMOK { get; set; }
        public String SAFO_NR { get; set; }
        public long? QH_FORCE { get; set; }
        public DateTime? TS_FORCE { get; set; }
        public DateTime? TS_ASSIGN { get; set; }
        public long? LPARSETNR { get; set; }
        public String SPARSETDESC { get; set; }
        public long? LNUMBOLT { get; set; }
        public String SFREESTRING1 { get; set; }
        public String SFREESTRING2 { get; set; }
        public String SFREESTRING3 { get; set; }
        public long? LFREEQSTLIST11 { get; set; }
        public long? LFREEQSTLIST12 { get; set; }
        public long? LFREEQSTLIST21 { get; set; }
        public long? LFREEINT1 { get; set; }
        public long? LFREEINT2 { get; set; }
        public double? FFREEFLOAT1 { get; set; }
        public double? FFREEFLOAT2 { get; set; }
        public long? I_PFUINFOID { get; set; }
        public DateTime? START_EXT_PERIOD { get; set; }
        public long? ABTRIEBID { get; set; }
        public long? INTENDED_MODELID { get; set; }

        public long? GID_01 { get; set; }
        public long? GID_02 { get; set; }
        public long? GLOBAL_ADDRESS_ID { get; set; }

        public CondRot CondRot { get; set; }
        public Location Location { get; set; }
        public Tool Tool { get; set; }
        public ToolUsage ToolUsage { get; set; }
    }

    [Table("LOCPOWCHANGES")]
    public class LocPowChanges
    {
        [Key]
        public long GlobalHistoryId { get; set; }
        public long LocPowId { get; set; }
        public long UserId { get; set; }
        public string Action { get; set; }
        public string USERCOMMENT { get; set; }
        public long? LocIdOld { get; set; }
        public long? LocIdNew { get; set; }
        public long? PowIdOld { get; set; }
        public long? PowIdNew { get; set; }
        public long? ORDOLD { get; set; }
        public long? ORDNEW { get; set; }
        public bool? ALIVEOLD { get; set; }
        public bool? ALIVENEW { get; set; }
    }
}
