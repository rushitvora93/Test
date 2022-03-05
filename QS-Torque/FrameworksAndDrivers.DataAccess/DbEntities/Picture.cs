using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("QST_PICT")]
    public class Picture
    {
        [Key]
        public long SEQID { get; set; }
        public long NODEID { get; set; }
        public long NODESEQID { get; set; }
        public String PICT { get; set; }
        public byte[] PICTURE { get; set; }
        public DateTime TSA { get; set; }
        public long FILETYPE { get; set; }
        public long? ALIVE { get; set; }
        public long? GID_01 { get; set; }
        public long? GID_02 { get; set; }
        public long? GLOBAL_ADDRESS_ID { get; set; }
    }
}
