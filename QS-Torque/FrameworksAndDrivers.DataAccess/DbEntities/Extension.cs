using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("EXTENSION")]
    public class Extension
    {
        [Key] public long TOOLID { get; set; }
        public string TOOL { get; set; }
        public string INVENTORY_NUMBER { get; set; }
        public double? FAKTOR { get; set; }
        public double? TOOLLEN { get; set; }
        public double? BENDING { get; set; }
        public DateTime? TSN { get; set; }
        public DateTime? TSA { get; set; }
        public long? SERVID { get; set; }
        public bool? ALIVE { get; set; }
        public virtual ICollection<CondLocTech> CondLocTechs { get; set; }
    }

    [Table("EXTENSIONCHANGES")]
    public class ExtensionChanges
    {
        [Key] 
        public long GLOBALHISTORYID { get; set; }
        public long EXTENSIONID { get; set; }
        public long USERID { get; set; }
        public String ACTION { get; set; }
        public String USERCOMMENT { get; set; }
        public string DESCRIPTIONOLD { get; set; }
        public string DESCRIPTIONNEW { get; set; }
        public string INVENTORY_NUMBEROLD { get; set; }
        public string INVENTORY_NUMBERNEW { get; set; }
        public double? FAKTOROLD { get; set; }
        public double? FAKTORNEW { get; set; }
        public double? TOOLLENOLD { get; set; }
        public double? TOOLLENNEW { get; set; }
        public double? BENDINGOLD { get; set; }
        public double? BENDINGNEW { get; set; }
        public bool? ALIVEOLD { get; set; }
        public bool? ALIVENEW { get; set; }
    }

}
