using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("LOCATION")]
    public class Location
    {
        [Key]
        public long SEQID { get; set; }
        public long? TREEID { get; set; }
        public double? TNOM { get; set; }
        public double? TMIN { get; set; }
        public double? TMAX { get; set; }
        public long? ANOM { get; set; }
        public long? AMIN { get; set; }
        public long? AMAX { get; set; }
        public double? TSTART { get; set; }
        public long? CLASSID { get; set; }
        public String USERID { get; set; }
        public String KOST { get; set; }
        public bool? DOKU { get; set; }
        public long? PARTNER { get; set; }
        public long? HAND { get; set; }
        public String KATEG { get; set; }
        public DateTime? TSN { get; set; }
        public DateTime? TSA { get; set; }
        public long CONTROL { get; set; }
        public long SERVID { get; set; }
        public String NAME { get; set; }
        public bool? ALIVE { get; set; }
        public long? UNITID { get; set; }
        public double? FQHOEG { get; set; }
        public double? FQHUEG { get; set; }
        public long? DIRECTION { get; set; }
        public long? FPOINTID { get; set; }
        public long? MAINID { get; set; }
        public long? PARENTID { get; set; }
        public DateTime? LASTMFU { get; set; }
        public long? PERIODMFU { get; set; }
        public long? UNIT2ID { get; set; }
        public long? MEASUREID { get; set; }
        public double? NOM2 { get; set; }
        public double? MIN2 { get; set; }
        public double? MAX2 { get; set; }
        public long? QDAEXPORT { get; set; }
        public String USERID2 { get; set; }
        public long? CLASSID2 { get; set; }
        public long? ISNEXTDIFFERENT { get; set; }
        public String USERID2_KENNER { get; set; }
        public DateTime? LASTCNGDNOM { get; set; }
        public double? LASTDNOM { get; set; }
        public long? GID_01 { get; set; }
        public long? GID_02 { get; set; }
        public long? GLOBAL_ADDRESS_ID { get; set; }
        public ICollection<LocPow> LocPows { get; set; }
        public ToleranceClass ToleranceClass1 { get; set; }
        public ToleranceClass ToleranceClass2 { get; set; }
        public ICollection<CondLoc> CondLocs { get; set; }
    }

    [Table("LOCATIONCHANGES")]
    public class LocationChanges
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
        public long LOCATIONID { get; set; }
        public long USERID { get; set; }
        public String ACTION { get; set; }
        public String USERCOMMENT { get; set; }
        public long? TREEIDOLD { get; set; }
        public long? TREEIDNEW { get; set; }
        public String USERIDOLD { get; set; }
        public String USERIDNEW { get; set; }
        public String NAMEOLD { get; set; }
        public String NAMENEW { get; set; }
        public double? TNOMOLD { get; set; }
        public double? TNOMNEW { get; set; }
        public double? TMINOLD { get; set; }
        public double? TMINNEW { get; set; }
        public double? TMAXOLD { get; set; }
        public double? TMAXNEW { get; set; }
        public double? TSTARTOLD { get; set; }
        public double? TSTARTNEW { get; set; }
        public long? CLASSIDOLD { get; set; }
        public long? CLASSIDNEW { get; set; }
        public String KOSTOLD { get; set; }
        public String KOSTNEW { get; set; }
        public bool? DOKUOLD { get; set; }
        public bool? DOKUNEW { get; set; }
        public String KATEGOLD { get; set; }
        public String KATEGNEW { get; set; }
        public long? CONTROLOLD { get; set; }
        public long? CONTROLNEW { get; set; }
        public bool? ALIVEOLD { get; set; }
        public bool? ALIVENEW { get; set; }
        public double? NOM2OLD { get; set; }
        public double? NOM2NEW { get; set; }
        public double? MIN2OLD { get; set; }
        public double? MIN2NEW { get; set; }
        public double? MAX2OLD { get; set; }
        public double? MAX2NEW { get; set; }
        public long? CLASSID2OLD { get; set; }
        public long? CLASSID2NEW { get; set; }
    }
}
