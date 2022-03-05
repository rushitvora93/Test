using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("POW_TOOL")]
    public class Tool
    {
        [Key]
        public long SEQID { get; set; }
        public long? MODELID { get; set; }
        public String SERIALNO { get; set; }
        public String USERNO { get; set; }
        public long? STATEID { get; set; }
        public long? VERSIONID { get; set; }
        public String PTACCESS { get; set; }
        public long? POWSYSID { get; set; }
        public long? POWSYSPOS { get; set; }
        public DateTime? TSN { get; set; }
        public DateTime? TSA { get; set; }
        public long? STATION { get; set; }
        public long SERVID { get; set; }
        public long? ORDERID { get; set; }
        public long? KOSTID { get; set; }
        public long? IPLANOK { get; set; }
        public DateTime? LAST_POWCHK { get; set; }
        public DateTime? NEXT_POWCHK { get; set; }
        public DateTime? LAST_POWMFU { get; set; }
        public DateTime? NEXT_POWMFU { get; set; }
        public DateTime? LAST_ISO6789 { get; set; }
        public DateTime? NEXT_ISO6789 { get; set; }
        public long? CHK_RES { get; set; }
        public long? MFU_RES { get; set; }
        public long? ISO_RES { get; set; }
        public bool? ALIVE { get; set; }
        public long? CTRLID { get; set; }
        public String SSPUENO { get; set; }
        public long? TOOLID { get; set; }
        public long? USEAREA { get; set; }
        public long? USEROLE { get; set; }
        public long? OWNAREA { get; set; }
        public long? OWNROLE { get; set; }
        public double? CALVALUE { get; set; }
        public long? CYCLES { get; set; }
        public DateTime? SELDATE { get; set; }
        public double? PRICE { get; set; }
        public long? SCRAP { get; set; }
        public long? LASTCTMODID { get; set; }
        public long? PLANPOWCHK { get; set; }
        public long? PLANPOWMFU { get; set; }
        public long? FREE_INT { get; set; }
        public double? FREE_FLOAT { get; set; }
        public String FREE_STR1 { get; set; }
        public String FREE_STR2 { get; set; }
        public String FREE_STR3 { get; set; }
        public DateTime? WORKTIME { get; set; }
        public long? IHTREEID { get; set; }
        public long? CARMODELID { get; set; }
        public long? ACTUALCYCLES { get; set; }
        public long? IIH_USEMAN { get; set; }
        public long? IIH_MANNUM { get; set; }
        public long? IIH_FDAREA { get; set; }
        public long? IWORKDAYS { get; set; }
        public String FREE_STR4 { get; set; }
        public String FREE_STR5 { get; set; }
        public DateTime? LASTEFFECT { get; set; }
        public long? ABTRIEBID { get; set; }
        public long? WINKELID { get; set; }
        public long? LLIMID { get; set; }
        public double? RIVET_CV_FORCE_AKT { get; set; }
        public double? RIVET_CV_FORCE_NULL_AKT { get; set; }
        public double? RIVET_CV_DIST_AKT { get; set; }
        public double? RIVET_CV_DIST_NULL_AKT { get; set; }
        public double? RIVET_SV_FORCE_AKT { get; set; }
        public double? RIVET_SV_FORCE_NULL_AKT { get; set; }
        public double? RIVET_SV_DIST_AKT { get; set; }
        public double? RIVET_SV_DIST_NULL_AKT { get; set; }
        public double? RIVET_CV_FORCE_OLD { get; set; }
        public double? RIVET_CV_FORCE_NULL_OLD { get; set; }
        public double? RIVET_SV_FORCE_OLD { get; set; }
        public double? RIVET_SV_FORCE_NULL_OLD { get; set; }
        public double? RIVET_CV_DIST_OLD { get; set; }
        public double? RIVET_CV_DIST_NULL_OLD { get; set; }
        public double? RIVET_SV_DIST_OLD { get; set; }
        public double? RIVET_SV_DIST_NULL_OLD { get; set; }
        public long? LASTRESGRAD { get; set; }
        public double? LASTCMK { get; set; }
        public double? LASTCM { get; set; }
        public long? POWTREEID { get; set; }
        public DateTime? TESTSTART { get; set; }
        public long? IFLAGS { get; set; }
        public long? I_STRG_KANALNR { get; set; }
        public long? I_STRG_PRGNR { get; set; }
        public long? PLANGRAD { get; set; }
        public DateTime? NEXTGRAD { get; set; }
        public long? LAST_CTMOD_6789 { get; set; }
        public DateTime? LAST_LOCMFU { get; set; }
        public long? LAST_LOCMFU_ISOID { get; set; }
        public DateTime? LAST_LOCCHK { get; set; }
        public long? LAST_LOCCHK_ISOID { get; set; }
        public long? LAST_POWMFU_ISOID { get; set; }
        public long? LAST_POWCHK_ISOID { get; set; }
        public DateTime? LAST_ROTMFU { get; set; }
        public long? LAST_ROTMFU_ISOID { get; set; }
        public DateTime? LAST_ROTCHK { get; set; }
        public long? LAST_ROTCHK_ISOID { get; set; }
        public DateTime? LAST_LOCPOW { get; set; }
        public long? LAST_LOCPOWID { get; set; }
        public long? I_PT_EXT_PERIOD { get; set; }
        public DateTime? DT_PT_EXT_LASTTEST { get; set; }
        public DateTime? DT_PT_EXT_NEXTTEST { get; set; }
        public long? I_PT_EXT_PRUEFBETRIEB { get; set; }
        public long? I_PT_EXT_LASTRESULT { get; set; }
        public ICollection<LocPow> LocPows { get; set; }
        public ToolModel ToolModel { get; set; }
        public Status Status { get; set; }
    }

    [Table("TOOLCHANGES")]
    public class ToolChanges
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
        public long ToolId { get; set; }
        public long USERID { get; set; }
        public String ACTION { get; set; }
        public String USERCOMMENT { get; set; }
        public long? MODELIDOLD { get; set; }
        public long? MODELIDNEW { get; set; }
        public string SERIALNOOLD { get; set; }
        public string SERIALNONEW { get; set; }
        public string USERNOOLD { get; set; }
        public string USERNONEW { get; set; }
        public long? STATEIDOLD { get; set; }
        public long? STATEIDNEW { get; set; }
        public string PTACCESSOLD { get; set; }
        public string PTACCESSNEW { get; set; }
        public long? ORDERIDOLD { get; set; }
        public long? ORDERIDNEW { get; set; }
        public long? KOSTIDOLD { get; set; }
        public long? KOSTIDNEW { get; set; }
        public bool? ALIVEOLD { get; set; }
        public bool? ALIVENEW { get; set; }
        public string FREE_STR1OLD { get; set; }
        public string FREE_STR1NEW { get; set; }
        public string FREE_STR2OLD { get; set; }
        public string FREE_STR2NEW { get; set; }
        public string FREE_STR3OLD { get; set; }
        public string FREE_STR3NEW { get; set; }
    }
}
