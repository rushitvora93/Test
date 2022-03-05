using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Types.Enums;
using Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table(("TESTEQUIPMENT"))]
    public class TestEquipment
    {
        [Key]
        public long SEQID { get; set; }
        public String SERNO { get; set; }
        public String USRNO { get; set; }
        public long? MODELID { get; set; }
        public long? STATEID { get; set; }
        public DateTime? LASTCERT { get; set; }
        public long? PERIOD { get; set; }
        public long? ITER { get; set; }
        public long? STATION { get; set; }
        public long SERVID { get; set; }
        public DateTime? TSN { get; set; }
        public DateTime? TSA { get; set; }
        public String VERS { get; set; }
        public bool? ALIVE { get; set; }
        public long? USEAREA { get; set; }
        public long? USEROLE { get; set; }
        public long? OWNAREA { get; set; }
        public long? OWNROLE { get; set; }
        public long? LDGVERSION { get; set; }
        public long? CALIBRATION_NORM { get; set; }
        public bool? TRANSFERUSER { get; set; }
        public bool? TRANSFERADAPTER { get; set; }
        public bool? TRANSFERTRANSDUCER { get; set; }
        public TestEquipmentBehaviourAskForIdent? ASKFORIDENT { get; set; }
        public TestEquipmentBehaviourTransferCurves? TRANSFERCURVES { get; set; }
        public bool? USEERRORCODES { get; set; }
        public bool? ASKFORSIGN { get; set; }
        public bool? ASKFORCURVES { get; set; }
        public bool? DOLOSECHECK { get; set; }
        public bool? CANDELETEMEASUREMENTS { get; set; }
        public TestEquipmentBehaviourConfirmMeasurements? CONFIRMMEASUREMENT { get; set; }
        public bool? TRANSFERPICT { get; set; }
        public bool? TRANSFERNEWLIMITS { get; set; }
        public bool? TRANSFERATTRIBUTES { get; set; }
        public bool? CANUSEQSTSTANDARD { get; set; }
        public long? ROUTINSUBDIR { get; set; }
        public long? NOTREANSEXE { get; set; }
        public long? TRANSFERNOSTATE { get; set; }
        public bool? ISCTLTEST { get; set; }
        public bool? ISROUTTEST { get; set; }
        public long? ISTESTTESTEQUIPMENT { get; set; }
        public long? ISSINGLECHECK { get; set; }
        public long? CHECKCAPACITY { get; set; }
        public double? CAPACITYMIN { get; set; }
        public double? CAPACITYMAX { get; set; }
        public double? SENSITIVITY { get; set; }
        public string CALIBRATION_NORM_TEXT { get; set; }
        public long? LANGUAGE { get; set; }
        public MeaUnit? UNIT { get; set; }
        public virtual ICollection<ClassicChkTest> ClassicChkTests { get; set; }
        public virtual ICollection<ClassicMfuTest> ClassicMfuTests { get; set; }
        public virtual ICollection<ClassicProcessTest> ClassicProcessTests { get; set; }
        public virtual TestEquipmentModel TestEquipmentModel { get; set; }
    }

    [Table(("TESTEQUIPMENTChanges"))]
    public class TestEquipmentChanges
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
        public long TESTEQUIPMENTID { get; set; }
        public long USERID { get; set; }
        public String ACTION { get; set; }
        public String USERCOMMENT { get; set; }
        public String SERNOOLD { get; set; }
        public String SERNONEW { get; set; }
        public String USRNOOLD { get; set; }
        public String USRNONEW { get; set; }
        public long? MODELIDOLD { get; set; }
        public long? MODELIDNEW { get; set; }
        public long? STATEIDOLD { get; set; }
        public long? STATEIDNEW { get; set; }
        public DateTime? LASTCERTOLD { get; set; }
        public DateTime? LASTCERTNEW { get; set; }
        public long? PERIODOLD { get; set; }
        public long? PERIODNEW { get; set; }
        public long? ITEROLD { get; set; }
        public long? ITERNEW { get; set; }
        public long? STATIONOLD { get; set; }
        public long? STATIONNEW { get; set; }
        public String VERSOLD { get; set; }
        public String VERSNEW { get; set; }
        public bool? ALIVEOLD { get; set; }
        public bool? ALIVENEW { get; set; }
        public long? LDGVERSIONOLD { get; set; }
        public long? LDGVERSIONNEW { get; set; }
        public long? CALIBRATION_NORMOLD { get; set; }
        public long? CALIBRATION_NORMNEW { get; set; }
        public bool? TRANSFERUSEROLD { get; set; }
        public bool? TRANSFERUSERNEW { get; set; }
        public bool? TRANSFERADAPTEROLD { get; set; }
        public bool? TRANSFERADAPTERNEW { get; set; }
        public bool? TRANSFERTRANSDUCEROLD { get; set; }
        public bool? TRANSFERTRANSDUCERNEW { get; set; }
        public TestEquipmentBehaviourAskForIdent? ASKFORIDENTOLD { get; set; }
        public TestEquipmentBehaviourAskForIdent? ASKFORIDENTNEW { get; set; }
        public TestEquipmentBehaviourTransferCurves? TRANSFERCURVESOLD { get; set; }
        public TestEquipmentBehaviourTransferCurves? TRANSFERCURVESNEW { get; set; }
        public bool? USEERRORCODESOLD { get; set; }
        public bool? USEERRORCODESNEW { get; set; }
        public bool? ASKFORSIGNOLD { get; set; }
        public bool? ASKFORSIGNNEW { get; set; }
        public bool? ASKFORCURVESOLD { get; set; }
        public bool? ASKFORCURVESNEW { get; set; }
        public bool? DOLOSECHECKOLD { get; set; }
        public bool? DOLOSECHECKNEW { get; set; }
        public bool? CANDELETEMEASUREMENTSOLD { get; set; }
        public bool? CANDELETEMEASUREMENTSNEW { get; set; }
        public TestEquipmentBehaviourConfirmMeasurements? CONFIRMMEASUREMENTOLD { get; set; }
        public TestEquipmentBehaviourConfirmMeasurements? CONFIRMMEASUREMENTNEW { get; set; }
        public bool? TRANSFERPICTOLD { get; set; }
        public bool? TRANSFERPICTNEW { get; set; }
        public bool? TRANSFERNEWLIMITSOLD { get; set; }
        public bool? TRANSFERNEWLIMITSNEW { get; set; }
        public bool? TRANSFERATTRIBUTESOLD { get; set; }
        public bool? TRANSFERATTRIBUTESNEW { get; set; }
        public bool? CANUSEQSTSTANDARDOLD { get; set; }
        public bool? CANUSEQSTSTANDARDNEW { get; set; }
        public long? ROUTINSUBDIROLD { get; set; }
        public long? ROUTINSUBDIRNEW { get; set; }
        public long? NOTREANSEXEOLD { get; set; }
        public long? NOTREANSEXENEW { get; set; }
        public long? TRANSFERNOSTATEOLD { get; set; }
        public long? TRANSFERNOSTATENEW { get; set; }
        public bool? ISCTLTESTOLD { get; set; }
        public bool? ISCTLTESTNEW { get; set; }
        public bool? ISROUTTESTOLD { get; set; }
        public bool? ISROUTTESTNEW { get; set; }
        public long? ISTESTTESTEQUIPMENTOLD { get; set; }
        public long? ISTESTTESTEQUIPMENTNEW { get; set; }
        public long? ISSINGLECHECKOLD { get; set; }
        public long? ISSINGLECHECKNEW { get; set; }
        public long? CHECKCAPACITYOLD { get; set; }
        public long? CHECKCAPACITYNEW { get; set; }
        public double? CAPACITYMINOLD { get; set; }
        public double? CAPACITYMINNEW { get; set; }
        public double? CAPACITYMAXOLD { get; set; }
        public double? CAPACITYMAXNEW { get; set; }
        public double? SENSITIVITYOLD { get; set; }
        public double? SENSITIVITYNEW { get; set; }
        public string CALIBRATION_NORM_TEXTOLD { get; set; }
        public string CALIBRATION_NORM_TEXTNEW { get; set; }
        public long? LANGUAGEOLD { get; set; }
        public long? LANGUAGENEW { get; set; }
        public MeaUnit? UNITOLD { get; set; }
        public MeaUnit? UNITNEW { get; set; }
    }
}
