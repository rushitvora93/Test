using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("CLASS_ID")]
    public class ToleranceClass
    {
        [Key]
        public long CLASSID { get; set; }
        public long? CLASS { get; set; }
        public string DESCRIPTION { get; set; }
        public double? CMINUS { get; set; }
        public double? CPLUS { get; set; }
        public DateTime? TSN { get; set; }
        public DateTime? TSA { get; set; }
        public long? SERVID { get; set; }
        public bool? CL_REL { get; set; }
        public bool? ALIVE { get; set; }
        public string RANK { get; set; }
        public ICollection<Location> ToleranceClass1Locations { get; set; }
        public ICollection<Location> ToleranceClass2Locations { get; set; }
    }

    [Table("TOLERANCECLASSCHANGES")]
    public class ToleranceClassChanges
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
        public long TOLERANCECLASSID { get; set; }
        public long USERID { get; set; }
        public String ACTION { get; set; }
        public String USERCOMMENT { get; set; }
        public String DESCRIPTIONOLD { get; set; }
        public String DESCRIPTIONNEW { get; set; }
        public double? CMINUSOLD { get; set; }
        public double? CMINUSNEW { get; set; }
        public double? CPLUSOLD { get; set; }
        public double? CPLUSNEW { get; set; }
        public bool? CL_RELOLD { get; set; }
        public bool? CL_RELNEW { get; set; }
        public bool? ALIVENEW { get; set; }
        public bool? ALIVEOLD { get; set; }
        public string RANKOLD { get; set; }
        public string RANKNEW { get; set; }
    }
}
