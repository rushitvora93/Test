using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("CLASSIC_PROCESS_TEST_VALUE")]
    public class ClassicProcessTestValue
    {
        [Key]
        public long SEQID { get; set; }
        public long GLOBALHISTORYID { get; set; }
        public long POSITION { get; set; }
        public double VALUE_UNIT1 { get; set; }
        public double VALUE_UNIT2 { get; set; }
        public long? ERROR_CODE { get; set; }
        public string IDENT { get; set; }
        public double? FENDVAL_UNIT1 { get; set; }
        public double? FENDVAL_UNIT2 { get; set; }
        public double? ORG_VAL_UNIT1 { get; set; }
        public double? ORG_VAL_UNIT2 { get; set; }
    }
}
