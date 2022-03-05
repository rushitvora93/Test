using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("CLASSIC_CHK_TEST")]
    public class ClassicChkTest
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long GlobalHistoryId { get; set; }
        public long? LocPowId { get; set; }
        public long? Shift { get; set; }
        public long NUMBER_OF_TESTS { get; set; }
        public double LOWER_LIMIT_UNIT1 { get; set; }
        public double NOMINAL_UNIT1 { get; set; }
        public double UPPER_LIMIT_UNIT1 { get; set; }
        public long TOLERANCE_CLASS_UNIT1_ID { get; set; }
        public long UNIT1_ID { get; set; }
        public double LOWER_LIMIT_UNIT2 { get; set; }
        public double NOMINAL_UNIT2 { get; set; }
        public double UPPER_LIMIT_UNIT2 { get; set; }
        public long TOLERANCE_CLASS_UNIT2_ID { get; set; }
        public long UNIT2_ID { get; set; }
        public long POW_TOOL_ID { get; set; }
        public double BRAKING_ANGLE { get; set; }
        public long CELL_NUMBER { get; set; }
        public long TEST_DEVICE_ID { get; set; }
        public double MINIMUM { get; set; }
        public double MAXIMUM { get; set; }
        public double X { get; set; }
        public double? S { get; set; }
        public long TESTER_ID { get; set; }
        public long STATUS { get; set; }
        public double AVERAGE_SPEED_STAGE1 { get; set; }
        public double AVERAGE_SPEED_STAGE2 { get; set; }
        public long CONTROLED_BY_UNIT_ID { get; set; }
        public double START_COUNT { get; set; }
        public double TARGET_UNIT1 { get; set; }
        public double TARGET_UNIT2 { get; set; }
        public double TEMPERATURE { get; set; }
        public double HUMIDITY { get; set; }
        public string IDENT { get; set; }
        public long SPEED_STAGE1 { get; set; }
        public long SPEED_STAGE2 { get; set; }
        public string SENSOR_SERIAL_NUMBER { get; set; }
        public double SENSOR_MIN_CAPACITY { get; set; }
        public double SENSOR_MAX_CAPACITY { get; set; }
        public long RESULT { get; set; }

        public virtual TestEquipment TestEquipment { get; set; }

        public virtual ClassicChkTestLocation ClassicChkTestLocation { get; set; }
        public virtual GlobalHistory GlobalHistory { get; set; }
    }
}
