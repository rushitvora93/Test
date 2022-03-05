using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("QSTGROUP")]
    public class QstGroup
    {
        [Key]
        public long GROUPID { get; set; }
        public String NAME { get; set; }
        public long? PERM1 { get; set; }
        public long? PERM2 { get; set; }
        public long? PERM3 { get; set; }
        public DateTime? TSN { get; set; }
        public DateTime? TSA { get; set; }
        public long SERVID { get; set; }
        public long? QHINCLIST { get; set; }
        public long? AREA { get; set; }
        public bool ADMIN { get; set; }
        public long? STATE { get; set; }
        public bool CAN_RECEIVE_SYNC { get; set; }
        public long? GID_01 { get; set; }
        public long? GID_02 { get; set; }
        public long? GLOBAL_ADDRESS_ID { get; set; }
    }
}
