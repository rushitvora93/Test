using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("CLASSIC_CHK_TEST_VALUE")]
    public class ClassicChkTestValue
    { 
        [Key]
        public long SEQID { get; set; }
        public long GLOBALHISTORYID { get; set; }
        public long POSITION { get; set; }
        public double VALUE_UNIT1 { get; set; }
        public double VALUE_UNIT2 { get; set; }
        public string IDENT { get; set; }
        public string SENSOR_SERIAL_NUMBER { get; set; }
        public double SENSOR_MIN_CAPACITY { get; set; }
        public double SENSOR_MAX_CAPACITY { get; set; }
    }
}
