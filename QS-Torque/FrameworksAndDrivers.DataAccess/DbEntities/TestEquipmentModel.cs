using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Types.Enums;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("TESTEQUIPMENTMODEL")]
    public class TestEquipmentModel
    {
        [Key] public long MODELID { get; set; }
        public String NAME { get; set; }
        public TestEquipmentType TYPE { get; set; }
        public long? MANUID { get; set; }
        public String PEAK { get; set; }
        public String PEAKTIME { get; set; }
        public String PEAKANGLE { get; set; }
        public String LA { get; set; }
        public String ROTATE { get; set; }
        public String ONE { get; set; }
        public long? TECHNOID { get; set; }
        public DateTime? TSA { get; set; }
        public DateTime? TSN { get; set; }
        public long SERVID { get; set; }
        public long? QSTID { get; set; }
        public String TXSCHEMA { get; set; }
        public String SETUP { get; set; }
        public long? DGVERSION { get; set; }
        public bool? TRANSFERUSER { get; set; }
        public bool? TRANSFERADAPTER { get; set; }
        public bool? TRANSFERTRANSDUCER { get; set; }
        public bool? ASKFORIDENT { get; set; }
        public bool? TRANSFERCURVES { get; set; }
        public bool? USEERRORCODES { get; set; }
        public bool? ASKFORSIGN { get; set; }
        public bool? DOLOSECHECK { get; set; }
        public bool? CANDELETEMEASUREMENTS { get; set; }
        public bool? CONFIRMMEASUREMENT { get; set; }
        public bool? TRANSFERPICTS { get; set; }
        public bool? TRANSFERNEWLIMITS { get; set; }
        public bool? ASKFORCURVES { get; set; }
        public bool? TRANSFERATTRIBUTES { get; set; }
        public bool? USEFORROT { get; set; }
        public bool? USEFORCTL { get; set; }
        public bool? CANUSEQSTSTANDARD { get; set; }
        public long? DATEFORMAT { get; set; }
        public long? WITHTOOLDATA { get; set; }
        public long? HASTESTTESTEQUIPMENT { get; set; }
        public String DRIVERPROGRAM_FILE_PATH { get; set; }
        public String TOTESTEQUIPMENT_FILE_PATH { get; set; }
        public String FROMTESTEQUIPMENT_FILE_PATH { get; set; }
        public String STATUS_FILE_PATH { get; set; }
        public bool? ALIVE { get; set; }

        public ICollection<TestEquipment> TestEquipments { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }

    [Table("TESTEQUIPMENTMODELCHANGES")]
    public class TestEquipmentModelChanges
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
        public long TESTEQUIPMENTMODELID { get; set; }
        public long USERID { get; set; }
        public String ACTION { get; set; }
        public String USERCOMMENT { get; set; }
        public String NAMEOLD { get; set; }
        public String NAMENEW { get; set; }
        public TestEquipmentType? TYPEOLD { get; set; }
        public TestEquipmentType? TYPENEW { get; set; }
        public long? MANUIDOLD { get; set; }
        public long? MANUIDNEW { get; set; }
        public String PEAKOLD { get; set; }
        public String PEAKNEW { get; set; }
        public String PEAKTIMEOLD { get; set; }
        public String PEAKTIMENEW { get; set; }
        public String PEAKANGLEOLD { get; set; }
        public String PEAKANGLENEW { get; set; }
        public String LAOLD { get; set; }
        public String LANEW { get; set; }
        public String ROTATEOLD { get; set; }
        public String ROTATENEW { get; set; }
        public String ONEOLD { get; set; }
        public String ONENEW { get; set; }
        public long? TECHNOIDOLD { get; set; }
        public long? TECHNOIDNEW { get; set; }
        public String TXSCHEMAOLD { get; set; }
        public String TXSCHEMANEW { get; set; }
        public String SETUPOLD { get; set; }
        public String SETUPNEW { get; set; }
        public long? DGVERSIONOLD { get; set; }
        public long? DGVERSIONNEW { get; set; }
        public bool? TRANSFERUSEROLD { get; set; }
        public bool? TRANSFERUSERNEW { get; set; }
        public bool? TRANSFERADAPTEROLD { get; set; }
        public bool? TRANSFERADAPTERNEW { get; set; }
        public bool? TRANSFERTRANSDUCEROLD { get; set; }
        public bool? TRANSFERTRANSDUCERNEW { get; set; }
        public bool? ASKFORIDENTOLD { get; set; }
        public bool? ASKFORIDENTNEW { get; set; }
        public bool? TRANSFERCURVESOLD { get; set; }
        public bool? TRANSFERCURVESNEW { get; set; }
        public bool? USEERRORCODESOLD { get; set; }
        public bool? USEERRORCODESNEW { get; set; }
        public bool? ASKFORSIGNOLD { get; set; }
        public bool? ASKFORSIGNNEW { get; set; }
        public bool? DOLOSECHECKOLD { get; set; }
        public bool? DOLOSECHECKNEW { get; set; }
        public bool? CANDELETEMEASUREMENTSOLD { get; set; }
        public bool? CANDELETEMEASUREMENTSNEW { get; set; }
        public bool? CONFIRMMEASUREMENTOLD { get; set; }
        public bool? CONFIRMMEASUREMENTNEW { get; set; }
        public bool? TRANSFERPICTSOLD { get; set; }
        public bool? TRANSFERPICTSNEW { get; set; }
        public bool? TRANSFERNEWLIMITSOLD { get; set; }
        public bool? TRANSFERNEWLIMITSNEW { get; set; }
        public bool? ASKFORCURVESOLD { get; set; }
        public bool? ASKFORCURVESNEW { get; set; }
        public bool? TRANSFERATTRIBUTESOLD { get; set; }
        public bool? TRANSFERATTRIBUTESNEW { get; set; }
        public bool? USEFORROTOLD { get; set; }
        public bool? USEFORROTNEW { get; set; }
        public bool? USEFORCTLOLD { get; set; }
        public bool? USEFORCTLNEW { get; set; }
        public bool? CANUSEQSTSTANDARDOLD { get; set; }
        public bool? CANUSEQSTSTANDARDNEW { get; set; }
        public long? DATEFORMATOLD { get; set; }
        public long? DATEFORMATNEW { get; set; }
        public long? WITHTOOLDATAOLD { get; set; }
        public long? WITHTOOLDATANEW { get; set; }
        public long? HASTESTTESTEQUIPMENTOLD { get; set; }
        public long? HASTESTTESTEQUIPMENTNEW { get; set; }
        public String DRIVERPROGRAM_FILE_PATHOLD { get; set; }
        public String DRIVERPROGRAM_FILE_PATHNEW { get; set; }
        public String TOTESTEQUIPMENT_FILE_PATHOLD { get; set; }
        public String TOTESTEQUIPMENT_FILE_PATHNEW { get; set; }
        public String FROMTESTEQUIPMENT_FILE_PATHOLD { get; set; }
        public String FROMTESTEQUIPMENT_FILE_PATHNEW { get; set; }
        public String STATUS_FILE_PATHOLD { get; set; }
        public String STATUS_FILE_PATHNEW { get; set; }
        public bool? ALIVEOLD { get; set; }
        public bool? ALIVENEW { get; set; }
    }
}
