using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Types.Enums;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("COND_LOCTECH")]
    public class CondLocTech
    {
        [Key] public long SEQID { get; set; }
        public long CONDLOCID { get; set; }
        public ManufacturerIds HERSTELLERID { get; set; }
        public long? I0 { get; set; }
        public long? I1 { get; set; }
        public long? I2 { get; set; }
        public long? I3 { get; set; }
        public long? I4 { get; set; }
        public long? I5 { get; set; }
        public long? I6 { get; set; }
        public long? I7 { get; set; }
        public long? I8 { get; set; }
        public long? I9 { get; set; }
        public long? I10 { get; set; }
        public long? I11 { get; set; }
        public long? I12 { get; set; }
        public long? I13 { get; set; }
        public long? I14 { get; set; }
        public double? F0 { get; set; }
        public double? F1 { get; set; }
        public double? F2 { get; set; }
        public double? F3 { get; set; }
        public double? F4 { get; set; }
        public double? F5 { get; set; }
        public double? F6 { get; set; }
        public double? F7 { get; set; }
        public double? F8 { get; set; }
        public double? F9 { get; set; }
        public double? F10 { get; set; }
        public double? F11 { get; set; }
        public double? F12 { get; set; }
        public double? F13 { get; set; }
        public double? F14 { get; set; }
        public String S0 { get; set; }
        public String S1 { get; set; }
        public String S2 { get; set; }
        public String S3 { get; set; }
        public String S4 { get; set; }
        public TestMethod METHODE { get; set; }
        public double? F15 { get; set; }
        public double? F16 { get; set; }
        public double? F17 { get; set; }
        public double? F18 { get; set; }
        public double? F19 { get; set; }
        public double? F20 { get; set; }
        public double? F21 { get; set; }
        public double? F22 { get; set; }
        public double? F23 { get; set; }
        public double? F24 { get; set; }
        public double? F25 { get; set; }
        public double? F26 { get; set; }
        public double? F27 { get; set; }
        public double? F28 { get; set; }
        public double? F29 { get; set; }
        public long? EXTENSIONID { get; set; }
        public bool ALIVE { get; set; }
        public virtual CondLoc CondLoc { get; set; }
        public virtual Extension Extension { get; set; }
    }

    [Table("CONDLOCTECHCHANGES")]
    public class CondLocTechChanges
    {
        [Key] 
        public long GLOBALHISTORYID { get; set; }
        public long CONDLOCTECHID { get; set; }
        public long USERID { get; set; }
        public String ACTION { get; set; }
        public String USERCOMMENT { get; set; }
        public ManufacturerIds? HERSTELLERIDNEW { get; set; }
        public ManufacturerIds? HERSTELLERIDOLD { get; set; }
        public long? I0NEW { get; set; }
        public long? I0OLD { get; set; }
        public long? I1NEW { get; set; }
        public long? I1OLD { get; set; }
        public long? I2NEW { get; set; }
        public long? I2OLD { get; set; }
        public long? I3NEW { get; set; }
        public long? I3OLD { get; set; }
        public long? I4NEW { get; set; }
        public long? I4OLD { get; set; }
        public long? I5NEW { get; set; }
        public long? I5OLD { get; set; }
        public long? I6NEW { get; set; }
        public long? I6OLD { get; set; }
        public long? I7NEW { get; set; }
        public long? I7OLD { get; set; }
        public long? I8NEW { get; set; }
        public long? I8OLD { get; set; }
        public long? I9NEW { get; set; }
        public long? I9OLD { get; set; }
        public long? I10NEW { get; set; }
        public long? I10OLD { get; set; }
        public long? I11NEW { get; set; }
        public long? I11OLD { get; set; }
        public long? I12NEW { get; set; }
        public long? I12OLD { get; set; }
        public long? I13NEW { get; set; }
        public long? I13OLD { get; set; }
        public long? I14NEW { get; set; }
        public long? I14OLD { get; set; }
        public double? F0NEW { get; set; }
        public double? F0OLD { get; set; }
        public double? F1NEW { get; set; }
        public double? F1OLD { get; set; }
        public double? F2NEW { get; set; }
        public double? F2OLD { get; set; }
        public double? F3NEW { get; set; }
        public double? F3OLD { get; set; }
        public double? F4NEW { get; set; }
        public double? F4OLD { get; set; }
        public double? F5NEW { get; set; }
        public double? F5OLD { get; set; }
        public double? F6NEW { get; set; }
        public double? F6OLD { get; set; }
        public double? F7NEW { get; set; }
        public double? F7OLD { get; set; }
        public double? F8NEW { get; set; }
        public double? F8OLD { get; set; }
        public double? F9NEW { get; set; }
        public double? F9OLD { get; set; }
        public double? F10NEW { get; set; }
        public double? F10OLD { get; set; }
        public double? F11NEW { get; set; }
        public double? F11OLD { get; set; }
        public double? F12NEW { get; set; }
        public double? F12OLD { get; set; }
        public double? F13NEW { get; set; }
        public double? F13OLD { get; set; }
        public double? F14NEW { get; set; }
        public double? F14OLD { get; set; }
        public String S0NEW { get; set; }
        public String S0OLD { get; set; }
        public String S1NEW { get; set; }
        public String S1OLD { get; set; }
        public String S2NEW { get; set; }
        public String S2OLD { get; set; }
        public String S3NEW { get; set; }
        public String S3OLD { get; set; }
        public String S4NEW { get; set; }
        public String S4OLD { get; set; }
        public TestMethod? METHODENEW { get; set; }
        public TestMethod? METHODEOLD { get; set; }
        public double? F15NEW { get; set; }
        public double? F15OLD { get; set; }
        public double? F16NEW { get; set; }
        public double? F16OLD { get; set; }
        public double? F17NEW { get; set; }
        public double? F17OLD { get; set; }
        public double? F18NEW { get; set; }
        public double? F18OLD { get; set; }
        public double? F19NEW { get; set; }
        public double? F19OLD { get; set; }
        public double? F20NEW { get; set; }
        public double? F20OLD { get; set; }
        public double? F21NEW { get; set; }
        public double? F21OLD { get; set; }
        public double? F22NEW { get; set; }
        public double? F22OLD { get; set; }
        public double? F23NEW { get; set; }
        public double? F23OLD { get; set; }
        public double? F24NEW { get; set; }
        public double? F24OLD { get; set; }
        public double? F25NEW { get; set; }
        public double? F25OLD { get; set; }
        public double? F26NEW { get; set; }
        public double? F26OLD { get; set; }
        public double? F27NEW { get; set; }
        public double? F27OLD { get; set; }
        public double? F28NEW { get; set; }
        public double? F28OLD { get; set; }
        public double? F29NEW { get; set; }
        public double? F29OLD { get; set; }
        public long? EXTENSIONIDNEW { get; set; }
        public long? EXTENSIONIDOLD { get; set; }
        public bool? ALIVENEW { get; set; }
        public bool? ALIVEOLD { get; set; }
    }
}