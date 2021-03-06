using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("COND_ROT")]
    public class CondRot
    {
        [Key]
        public long LOCPOWID { get; set; }
        public long? TESTID { get; set; }
        public long? TESTS { get; set; }
        public double? PERIOD { get; set; }
        public bool? PLANOK { get; set; }
        public DateTime? TESTSTART { get; set; }
        public DateTime? TSN { get; set; }
        public DateTime? TSA { get; set; }
        public long? STATID { get; set; }
        public long? CHANEL { get; set; }
        public long? TYPEID { get; set; }
        public long? PROGID { get; set; }
        public DateTime? LAST_MFU { get; set; }
        public DateTime? LAST_CHK { get; set; }
        public long? MFU_ID { get; set; }
        public long? CHK_ID { get; set; }
        public DateTime? NEXT_CHK { get; set; }
        public long? CHK_RES { get; set; }
        public long? MFU_RES { get; set; }
        public long? KODEX { get; set; }
        public long? LTIMEOUT { get; set; }
        public long? STIMEOUT { get; set; }
        public long? DURATION { get; set; }
        public long? USE_TRIGGER { get; set; }
        public long? RESULTTYP { get; set; }
        public double? OEGX { get; set; }
        public double? UEGX { get; set; }
        public double? OEGS { get; set; }
        public double? OEGR { get; set; }
        public long? USE_OEGX { get; set; }
        public long? USE_UEGX { get; set; }
        public long? USE_OEGS { get; set; }
        public long? USE_OEGR { get; set; }
        public double? OEGN { get; set; }
        public double? UEGN { get; set; }
        public double? DELTAN { get; set; }
        public double? DELTAX { get; set; }
        public long? USE_OEGN { get; set; }
        public long? USE_UEGN { get; set; }
        public long? USE_DELTAN { get; set; }
        public long? USE_DELTAX { get; set; }
        public long? FLAGS { get; set; }
        public double? F1 { get; set; }
        public double? F2 { get; set; }
        public double? F3 { get; set; }
        public double? F4 { get; set; }
        public double? F5 { get; set; }
        public long? I1 { get; set; }
        public long? I2 { get; set; }
        public long? I3 { get; set; }
        public long? I4 { get; set; }
        public long? PERIOD_MFU { get; set; }
        public double? MS_MIN { get; set; }
        public double? MS_MAX { get; set; }
        public double? NOM1 { get; set; }
        public double? MIN1 { get; set; }
        public double? MAX1 { get; set; }
        public double? NOM2 { get; set; }
        public double? MIN2 { get; set; }
        public double? MAX2 { get; set; }
        public double? MS { get; set; }
        public long? CLASSID { get; set; }
        public long? TESTSMFU { get; set; }
        public long? SCHTESTTYPE { get; set; }
        public long? SCHGETANGLE { get; set; }
        public long? SCHFILTER { get; set; }
        public long? SCHSURPRESS { get; set; }
        public long? SCHTRANS { get; set; }
        public long? SCHMANTERM { get; set; }
        public long? SCHKPIL1 { get; set; }
        public long? SCHKPIL2 { get; set; }
        public double? SCHDEADTIME { get; set; }
        public double? SCHSTARTMEA { get; set; }
        public double? SCHSTARTDEG { get; set; }
        public double? SCHACCEPTMIN1 { get; set; }
        public double? SCHACCEPTMAX1 { get; set; }
        public double? SCHACCEPTMIN2 { get; set; }
        public double? SCHACCEPTMAX2 { get; set; }
        public long? CLASSID2 { get; set; }
        public double? AC_CYCLECOMPLETE { get; set; }
        public double? AC_SLIPTORQUE { get; set; }
        public double? AC_CYCLESTARTROT { get; set; }
        public double? AC_STARTFINALANGLEROT { get; set; }
        public double? AC_TORQUECOEFFICIENT { get; set; }
        public double? AC_ENDTIME { get; set; }
        public double? AC_MEASUREDELAYTIME { get; set; }
        public double? AC_RESETTIME { get; set; }
        public double? AC_FILTERFREQ { get; set; }
        public long? AC_MEASURETORQUEAT { get; set; }
        public long? AC_THRESHOLD { get; set; }
        public bool? AC_CMCMKSPCTESTTYPE { get; set; }
        public double? AC_MINIMUMCM { get; set; }
        public double? AC_MINIMUMCMK { get; set; }
        public double? AC_MINIMUMCMANGLE { get; set; }
        public double? AC_MINIMUMCMKANGLE { get; set; }
        public long? AC_TESTTYPE { get; set; }
        public long? AC_MINIMUMPULSE { get; set; }
        public long? AC_MAXIMUMPULSE { get; set; }
        public long? AC_STRATEGY { get; set; }
        public long? I_CTRL { get; set; }
        public long? SCH_SURPRESS2 { get; set; }
        public long? SCH_USEANWENDSTICHMASS { get; set; }
        public double? SCH_ANWENDSTICHMASS { get; set; }
        public double? SCH_NENNWERTANALYSE { get; set; }
        public double? SCH_WINKELKORREKTUR { get; set; }
        public DateTime? MISS_CHK { get; set; }
        public DateTime? MISS_MFU { get; set; }
        public long? GID_01 { get; set; }
        public long? GID_02 { get; set; }
        public long? GLOBAL_ADDRESS_ID { get; set; }
        public DateTime? TESTSTART_MFU { get; set; }
        public bool? ALIVE { get; set; }
        public DateTime? NEXT_MFU { get; set; }
        public bool? PLANOK_MFU { get; set; }
        public long? NextMfuShift { get; set; }
        public long? NextChkShift { get; set; }
        public DateTime? EndOfLastTestPeriodMfu { get; set; }
        public DateTime? EndOfLastTestPeriodChk { get; set; }
        public long? EndOfLastTestPeriodShiftMfu { get; set; }
        public long? EndOfLastTestPeriodShiftChk { get; set; }
        public long? TestLevelSetIdMfu { get; set; }
        public long? TestLevelSetIdChk { get; set; }
        public long? TestLevelNumberMfu { get; set; }
        public long? TestLevelNumberChk { get; set; }
        public LocPow LocPow { get; set; }
    }

    [Table("CONDROTCHANGES")]
    public class CondRotChanges
    {
        [Key]
        public long GlobalHistoryId { get; set; }
        public long CondRotId { get; set; }
        public long UserId { get; set; }
        public string Action { get; set; }
        public string USERCOMMENT { get; set; }
        public double? NOM1OLD { get; set; }
        public double? NOM1NEW { get; set; }
        public double? MIN1OLD { get; set; }
        public double? MIN1NEW { get; set; }
        public double? MAX1OLD { get; set; }
        public double? MAX1NEW { get; set; }
        public double? MSOLD { get; set; }
        public double? MSNEW { get; set; }
        public double? NOM2OLD { get; set; }
        public double? NOM2NEW { get; set; }
        public double? MIN2OLD { get; set; }
        public double? MIN2NEW { get; set; }
        public double? MAX2OLD { get; set; }
        public double? MAX2NEW { get; set; }
        public long? CLASSIDOLD { get; set; }
        public long? CLASSIDNEW { get; set; }
        public long? CLASSID2OLD { get; set; }
        public long? CLASSID2NEW { get; set; }
        public long? TESTSOLD { get; set; }
        public long? TESTSNEW { get; set; }
        public double? PERIODOLD { get; set; }
        public double? PERIODNEW { get; set; }
        public DateTime? TESTSTARTOLD { get; set; }
        public DateTime? TESTSTARTNEW { get; set; }
        public bool? PLANOKOLD { get; set; }
        public bool? PLANOKNEW { get; set; }
        public long? PERIOD_MFUOLD { get; set; }
        public long? PERIOD_MFUNEW { get; set; }
        public long? TESTSMFUOLD { get; set; }
        public long? TESTSMFUNEW { get; set; }
        public DateTime? TESTSTART_MFUOLD { get; set; }
        public DateTime? TESTSTART_MFUNEW { get; set; }
        public double? AC_ENDTIMEOLD { get; set; }
        public double? AC_ENDTIMENEW { get; set; }
        public double? AC_FILTERFREQOLD { get; set; }
        public double? AC_FILTERFREQNEW { get; set; }
        public double? AC_CYCLECOMPLETEOLD { get; set; }
        public double? AC_CYCLECOMPLETENEW { get; set; }
        public double? AC_MEASUREDELAYTIMEOLD { get; set; }
        public double? AC_MEASUREDELAYTIMENEW { get; set; }
        public double? AC_RESETTIMEOLD { get; set; }
        public double? AC_RESETTIMENEW { get; set; }
        public bool? AC_CMCMKSPCTESTTYPEOLD { get; set; }
        public bool? AC_CMCMKSPCTESTTYPENEW { get; set; }
        public double? AC_CYCLESTARTROTOLD { get; set; }
        public double? AC_CYCLESTARTROTNEW { get; set; }
        public double? AC_STARTFINALANGLEROTOLD { get; set; }
        public double? AC_STARTFINALANGLEROTNEW { get; set; }
        public double? AC_SLIPTORQUEOLD { get; set; }
        public double? AC_SLIPTORQUENEW { get; set; }
        public double? AC_TORQUECOEFFICIENTOLD { get; set; }
        public double? AC_TORQUECOEFFICIENTNEW { get; set; }
        public long? AC_MINIMUMPULSEOLD { get; set; }
        public long? AC_MINIMUMPULSENEW { get; set; }
        public long? AC_MAXIMUMPULSEOLD { get; set; }
        public long? AC_MAXIMUMPULSENEW { get; set; }
        public long? AC_THRESHOLDOLD { get; set; }
        public long? AC_THRESHOLDNEW { get; set; }
        public long? I_CTRLOLD { get; set; }
        public long? I_CTRLNEW { get; set; }
        public bool? ALIVEOLD { get; set; }
        public bool? ALIVENEW { get; set; }
        public bool? PLANOK_MFUOLD { get; set; }
        public bool? PLANOK_MFUNEW { get; set; }
        public DateTime? NEXT_CHKOld { get; set; }
        public DateTime? NEXT_CHKNew { get; set; }
        public DateTime? LAST_CHKOld { get; set; }
        public DateTime? LAST_CHKNew { get; set; }
        public DateTime? NEXT_MFUOld { get; set; }
        public DateTime? NEXT_MFUNew { get; set; }
        public DateTime? LAST_MFUOld { get; set; }
        public DateTime? LAST_MFUNew { get; set; }
        public long? TestLevelSetIdMfuNew { get; set; }
        public long? TestLevelSetIdMfuOld { get; set; }
        public long? TestLevelSetIdChkNew { get; set; }
        public long? TestLevelSetIdChkOld { get; set; }
        public long? TestLevelNumberMfuNew { get; set; }
        public long? TestLevelNumberMfuOld { get; set; }
        public long? TestLevelNumberChkNew { get; set; }
        public long? TestLevelNumberChkOld { get; set; }
    }
}
