using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("USAGESTATISTICS")]
    public class UsageStatistic
    {
        [Key, Column(Order = 1)]
        public virtual string ACTION { get; set; }

        [Key, Column(Order = 2)]
        public virtual DateTime TIMESTAMP { get; set; }
    }
}
