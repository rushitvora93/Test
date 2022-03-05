using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("CLASSIC_PROCESS_TEST")]
    public class ClassicProcessTest
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long GlobalHistoryId { get; set; }
        public long NUMBER_OF_TESTS { get; set; }
        public long NOMINAL_UNIT1 { get; set; }
        public long LOWER_LIMIT_UNIT1 { get; set; }
        public long UPPER_LIMIT_UNIT1 { get; set; }
        public long NOMINAL_UNIT2 { get; set; }
        public long LOWER_LIMIT_UNIT2 { get; set; }
        public long UPPER_LIMIT_UNIT2 { get; set; }
        public long EXTENSION_ID { get; set; }
        public long TEST_DEVICE_ID { get; set; }
        public long AVERAGE { get; set; }
        public long STANDARDDEVIATION { get; set; }
        public long MINIMUM { get; set; }
        public long MAXIMUM { get; set; }
        public long TESTER_ID { get; set; }
        public long STATUS { get; set; }
        public long TOLERANCE_CLASS_UNIT1_ID { get; set; }
        public long TOLERANCE_CLASS_UNIT2_ID { get; set; }
        public long CONTROLED_BY_UNIT_ID { get; set; }
        public long IDENT { get; set; }
        public long UNIT1_ID { get; set; }
        public long UNIT2_ID { get; set; }
        public long LOWER_INTERVENTION_LIMIT_UNIT1 { get; set; }
        public long UPPER_INTERVENTION_LIMIT_UNIT1 { get; set; }
        public long LOWER_INTERVENTION_LIMIT_UNIT2 { get; set; }
        public long UPPER_INTERVENTION_LIMIT_UNIT2 { get; set; }
        public long RESULT { get; set; }
        public long TEST_MODE { get; set; }
        public long TEST_METHODID { get; set; }
        public long? SHIFT { get; set; }
        public long PROCESS_TYPE { get; set; }
        public long CONDLOCID { get; set; }
        public long CONDLOCTECHID { get; set; }
        public virtual ClassicProcessTestLocation ClassicProcessTestLocation { get; set; }
        public virtual GlobalHistory GlobalHistory { get; set; }
        public virtual TestEquipment TestEquipment { get; set; }
    }
}
