using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("CLASSIC_PROCESS_TEST_LOCATION")]
    public class ClassicProcessTestLocation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long? GLOBALHISTORYID { get; set; }
        public long LOCATION_ID { get; set; }
        public string LOCATION_TREE_PATH { get; set; }
        public long LOCTREE_ID { get; set; }
        public virtual ClassicProcessTest ClassicProcessTest { get; set; }
    }
}
