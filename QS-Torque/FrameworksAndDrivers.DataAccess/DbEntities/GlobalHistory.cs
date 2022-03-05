using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("GLOBALHISTORY")]
    public class GlobalHistory
    {
        [Key]
        public long ID { get; set; }
        public string TYPE { get; set; }
        public DateTime? TIMESTAMP { get; set; }
        public virtual ClassicChkTest ClassicChkTest { get; set; }
        public virtual ClassicMfuTest ClassicMfuTest { get; set; }
        public virtual ClassicProcessTest ClassicProcessTest { get; set; }
    }

    [Table("MANUFACTURERHISTORY")]
    public class ManufacturerHistory
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
    }

    [Table("STATUSHISTORY")]
    public class StatusHistory
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
    }

    [Table("TOLERANCECLASSHISTORY")]
    public class ToleranceClassHistory
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
    }

    [Table("CONSTRUCTIONTYPEHISTORY")]
	public class ConstructionTypeHistory 
	{
        [Key]
		public long GLOBALHISTORYID { get; set; }
	}

    [Table("DRIVESIZEHISTORY")]
	public class DriveSizeHistory
	{
        [Key]
        public long GLOBALHISTORYID { get; set; }
	}

    [Table("DRIVETYPEHISTORY")]
	public class DriveTypeHistory 
	{
        [Key]
        public long GLOBALHISTORYID { get; set; }
	}

    [Table("REASONFORTOOLCHANGEHISTORY")]
	public class ReasonForToolChangeHistory 
	{
        [Key]
        public long GLOBALHISTORYID { get; set; }
	}

    [Table("SHUTOFFHISTORY")]
	public class ShutoffHistory 
	{
        [Key]
        public long GLOBALHISTORYID { get; set; }
	}

    [Table("SWITCHOFFHISTORY")]
	public class SwitchoffHistory
	{
        [Key]
        public long GLOBALHISTORYID { get; set; }
	}

    [Table("TOOLTYPEHISTORY")]
	public class ToolTypeHistory
	{
        [Key]
        public long GLOBALHISTORYID { get; set; }
	}

    [Table("CONFIGURABLEFIELDHISTORY")]
	public class ConfigurableFieldHistory
	{
        [Key]
        public long GLOBALHISTORYID { get; set; }
	}

    [Table("COSTCENTERHISTORY")]
	public class CostCenterHistory
	{
        [Key]
        public long GLOBALHISTORYID { get; set; }
	}

    [Table("TOOLUSAGEHISTORY")]
    public class ToolUsageHistory
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
    }

    [Table("LOCATIONHISTORY")]
    public class LocationHistory
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
    }

    [Table("LOCATIONDIRECTORYHISTORY")]
    public class LocationDirectoryHistory
    {
        [Key]
        public virtual long GLOBALHISTORYID { get; set; }
    }

    [Table("TOOLHISTORY")]
    public class ToolHistory
    {
        [Key]
        public virtual long GLOBALHISTORYID { get; set; }
    }

    [Table("LOCPOWHISTORY")]
    public class LocPowHistory
    {
        [Key]
        public virtual long GLOBALHISTORYID { get; set; }
    }

    [Table("CONDROTHISTORY")]
    public class CondRotHistory
    {
        [Key]
        public virtual long GLOBALHISTORYID { get; set; }
    }

    [Table("TOOLMODELHISTORY")]
    public class ToolModelHistory
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
    }

    [Table("TESTEQUIPMENTHISTORY")]
    public class TestEquipmentHistory
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
    }

    [Table("TESTEQUIPMENTMODELHISTORY")]
    public class TestEquipmentModelHistory
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
    }

    [Table("ShiftHistory")]
    public class ShiftHistory
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
    }

    [Table("WorkingCalendarHistory")]
    public class WorkingCalendarHistory
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
    }

    [Table("WorkingCalendarEntryHistory")]
    public class WorkingCalendarEntryHistory
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
    }

    [Table("TESTLEVELHISTORY")]
    public class TestLevelHistory
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
    }

    [Table("TESTLEVELSETHISTORY")]
    public class TestLevelSetHistory
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
    }

    [Table("CONDLOCHISTORY")]
    public class CondLocHistory
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
    }

    [Table("CONDLOCTECHHISTORY")]
    public class CondLocTechHistory
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
    }

    [Table("EXTENSIONHISTORY")]
    public class ExtensionHistory
    {
        [Key]
        public long GLOBALHISTORYID { get; set; }
    }
}
