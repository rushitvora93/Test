using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("QSTUSER")]
    public class QstUser
    {
        [Key]
        public long USERID { get; set; }
        public long UNUM { get; set; }
        public String UNAME { get; set; }
        public String UPWD { get; set; }
        public String DEPT { get; set; }
        public String FUNC { get; set; }
        public String UTEL { get; set; }
        public long? TEAMID { get; set; }
        public DateTime? TSA { get; set; }
        public DateTime? TSN { get; set; }
        public long SERVID { get; set; }
        public String PWD { get; set; }
        public long STATE { get; set; }
        public long? QHMSTR { get; set; }
        public String SMAIL { get; set; }
        public long? IKAT { get; set; }
        public String FGBUSER { get; set; }
        public String DOMAINUSER { get; set; }
        public long? LASTDOMAIN { get; set; }
        public bool SUPERUSER { get; set; }
    }
}
