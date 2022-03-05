using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("MANU_ID")]
    public class Manufacturer
    {
        [Key]
        public long MANUID { get; set; }
        public String NAME { get; set; }
        public String STREET { get; set; }
        public long? NUM { get; set; }
        public String CITY { get; set; }
        public long? CODE { get; set; }
        public String TEL { get; set; }
        public String FAX { get; set; }
        public String PERSON { get; set; }
        public String COUNTRY { get; set; }
        public DateTime? TSA { get; set; }
        public DateTime? TSN { get; set; }
        public long? SERVID { get; set; }
        public bool? ALIVE { get; set; }
        public String SIGN { get; set; }
        public long? GID_01 { get; set; }
        public long? GID_02 { get; set; }
        public long? GLOBAL_ADDRESS_ID { get; set; }
        public string ZIPCODESTR { get; set; }
        public string HOUSENUMBERSTR { get; set; }
        public long? FIXED { get; set; }

        public ICollection<TestEquipmentModel> TestEquipmentModels { get; set; }
        public ICollection<ToolModel> ToolModels { get; set; }
    }

    [Table("MANUFACTURERCHANGES")]
    public class ManufacturerChanges
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
        public long MANUFACTURERID { get; set; }
        public long USERID { get; set; }
        public string ACTION { get; set; }
        public string USERCOMMENT { get; set; }
        public string NAMEOLD { get; set; }
        public string NAMENEW { get; set; }
        public string STREETOLD { get; set; }
        public string STREETNEW { get; set; }
        public long? NUMOLD { get; set; }
        public long? NUMNEW { get; set; }
        public string CITYOLD { get; set; }
        public string CITYNEW { get; set; }
        public long? CODEOLD { get; set; }
        public long? CODENEW { get; set; }
        public string TELOLD { get; set; }
        public string TELNEW { get; set; }
        public string FAXOLD { get; set; }
        public string FAXNEW { get; set; }
        public string PERSONOLD { get; set; }
        public string PERSONNEW { get; set; }
        public string COUNTRYOLD { get; set; }
        public string COUNTRYNEW { get; set; }
        public bool? ALIVEOLD { get; set; }
        public bool? ALIVENEW { get; set; }
        public string SIGNOLD { get; set; }
        public string SIGNNEW { get; set; }
        public string HOUSENUMBERSTROLD { get; set; }
        public string HOUSENUMBERSTRNEW { get; set; }
        public string ZIPCODESTROLD { get; set; }
        public string ZIPCODESTRNEW { get; set; }
	}
}
