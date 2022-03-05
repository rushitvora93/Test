using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("MODEL")]
    public class ToolModel
    {
        [Key]
        public long MODELID { get; set; }
        public string MODEL { get; set; }
        public long TYPEID { get; set; }
        public long? MANUID { get; set; }
        public double? LIMITHI { get; set; }
        public double? LIMITLO { get; set; }
        public double? PRESSURE { get; set; }
        public long? ID { get; set; }
        public long? DRIVEID { get; set; }
        public long? FORMID { get; set; }
        public long? SWITCH { get; set; }
        public long? SWITCHID { get; set; }
        public long? TURN { get; set; }
        public double WEIGHT { get; set; }
        public long? KINDID { get; set; }
        public long? MEAID { get; set; }
        public double? AIR { get; set; }
        public double? VOLT { get; set; }
        public DateTime? TSN { get; set; }
        public DateTime? TSA { get; set; }
        public long SERVID { get; set; }
        public long CLASSID { get; set; }
        public long? ANGLE { get; set; }
        public bool? ALIVE { get; set; }
        public double? RAMPMD { get; set; }
        public double? RAMPWI { get; set; }
        public long? VERSIONID { get; set; }
        public long? MAINTENANCEINT { get; set; }
        public string FREES1 { get; set; }
        public string FREES2 { get; set; }
        public long? FREEI1 { get; set; }
        public long? FREEI2 { get; set; }
        public double? FREEF1 { get; set; }
        public double? FREEF2 { get; set; }
        public long? LTECHNOID { get; set; }
        public long? ITOOLMODEL { get; set; }
        public long? IRIVETSDRIVER { get; set; }
        public long? IRIVETSKORREKTUR { get; set; }
        public long? LLIMID { get; set; }
        public long? GID_01 { get; set; }
        public long? GID_02 { get; set; }
        public long? GLOBAL_ADDRESS_ID { get; set; }
        public ICollection<Tool> Tools { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }

    [Table("TOOLMODELCHANGES")]
    public class ToolModelChanges
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
        public long TOOLMODELID { get; set; }
        public long USERID { get; set; }
        public string ACTION { get; set; }
        public string USERCOMMENT { get; set; }

        public bool? ALIVEOLD { get; set; }
        public bool? ALIVENEW { get; set; }
        public string MODELOLD { get; set; }
        public string MODELNEW { get; set; }
        public long? TYPEIDOLD { get; set; }
        public long? TYPEIDNEW { get; set; }
        public long? MANUIDOLD { get; set; }
        public long? MANUIDNEW { get; set; }
        public double? LIMITHIOLD { get; set; }
        public double? LIMITHINEW { get; set; }
        public double? LIMITLOOLD { get; set; }
        public double? LIMITLONEW { get; set; }
        public double? PRESSUREOLD { get; set; }
        public double? PRESSURENEW { get; set; }
        public long? IDOLD { get; set; }
        public long? IDNEW { get; set; }
        public long? DRIVEIDOLD { get; set; }
        public long? DRIVEIDNEW { get; set; }
        public long? FORMIDOLD { get; set; }
        public long? FORMIDNEW { get; set; }
        public long? SWITCHOLD { get; set; }
        public long? SWITCHNEW { get; set; }
        public long? SWITCHIDOLD { get; set; }
        public long? SWITCHIDNEW { get; set; }
        public long? TURNOLD { get; set; }
        public long? TURNNEW { get; set; }
        public double? WEIGHTOLD { get; set; }
        public double? WEIGHTNEW { get; set; }
        public long? KINDIDOLD { get; set; }
        public long? KINDIDNEW { get; set; }
        public long? MEAIDOLD { get; set; }
        public long? MEAIDNEW { get; set; }
        public double? AIROLD { get; set; }
        public double? AIRNEW { get; set; }
        public double? VOLTOLD { get; set; }
        public double? VOLTNEW { get; set; }
        public long? CLASSIDOLD { get; set; }
        public long? CLASSIDNEW { get; set; }
        public long? ANGLEOLD { get; set; }
        public long? ANGLENEW { get; set; }
        public long? VERSIONIDOLD { get; set; }
        public long? VERSIONIDNEW { get; set; }
        public long? MAINTENANCEINTOLD { get; set; }
        public long? MAINTENANCEINTNEW { get; set; }
        public double? RAMPMDOLD { get; set; }
        public double? RAMPMDNEW { get; set; }
        public double? RAMPWIOLD { get; set; }
        public double? RAMPWINEW { get; set; }
        public string FREES1OLD { get; set; }
        public string FREES1NEW { get; set; }
        public string FREES2OLD { get; set; }
        public string FREES2NEW { get; set; }
        public long? FREEI1OLD { get; set; }
        public long? FREEI1NEW { get; set; }
        public long? FREEI2OLD { get; set; }
        public long? FREEI2NEW { get; set; }
        public double? FREEF1OLD { get; set; }
        public double? FREEF1NEW { get; set; }
        public double? FREEF2OLD { get; set; }
        public double? FREEF2NEW { get; set; }
        public long? LTECHNOIDOLD { get; set; }
        public long? LTECHNOIDNEW { get; set; }
        public long? ITOOLMODELOLD { get; set; }
        public long? ITOOLMODELNEW { get; set; }
        public long? IRIVETSDRIVEROLD { get; set; }
        public long? IRIVETSDRIVERNEW { get; set; }
        public long? IRIVETSKORREKTUROLD { get; set; }
        public long? IRIVETSKORREKTURNEW { get; set; }
        public long? LLIMIDOLD { get; set; }
        public long? LLIMIDNEW { get; set; }
        public long? JOINTHEADOLD { get; set; }
        public long? JOINTHEADNEW { get; set; }
        public double? INTERVALOLD { get; set; }
        public double? INTERVALNEW { get; set; }
        public long? TESTOPERATIONOLD { get; set; }
        public long? TESTOPERATIONNEW { get; set; }
    }
}
