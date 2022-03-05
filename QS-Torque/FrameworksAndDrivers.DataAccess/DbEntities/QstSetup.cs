using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("QSTSETUP")]
    public class QstSetup
    {
        [Key] 
        public long LID { get; set; }

        public string SName { get; set; }
        public string SText { get; set; }
        public long LUserId { get; set; }
        public long? Area { get; set; }
        public long? Role { get; set; }
        public long? GId_01 { get; set; }
        public long? GId_02 { get; set; }
        public long? Global_Address_Id { get; set; }
    }
}
