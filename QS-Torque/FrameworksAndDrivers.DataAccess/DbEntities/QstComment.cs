using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("QSTCOMMENTS")]
    public class QstComment
    {
        [Key]
        public long LID { get; set; }
        public long NODEID { get; set; }
        public long NODESEQID { get; set; }
        public string INFO { get; set; }
        public DateTime? TSN { get; set; }
        public DateTime? TSA { get; set; }
        public long? USERID { get; set; }
        public long? STATION { get; set; }
        public long? GLOBAL_ADDRESS_ID { get; set; }
    }
}
