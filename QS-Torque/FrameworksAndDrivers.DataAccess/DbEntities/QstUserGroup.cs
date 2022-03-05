using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("QSTUSERGROUP")]
    public class QstUserGroup
    {
        [Key]
        public long LID { get; set; }
        public long USERID { get; set; }
        public long GROUPID { get; set; }
    }
}
