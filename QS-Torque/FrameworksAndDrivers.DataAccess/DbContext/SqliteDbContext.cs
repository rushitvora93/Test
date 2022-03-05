using FrameworksAndDrivers.DataAccess.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace FrameworksAndDrivers.DataAccess.DbContext
{
    public class SqliteDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly string _dataSource;

        public SqliteDbContext(string dataSource)
        {
            _dataSource = dataSource;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=" + _dataSource);
        }

        public DbSet<QstUser> QstUsers { get; set; }
        public DbSet<QstGroup> QstGroups { get; set; }
        public DbSet<QstUserGroup> QstUserGroups { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ManufacturerHistory> ManufacturerHistories { get; set; }
        public DbSet<ManufacturerChanges> ManufacturerChanges { get; set; }
        public DbSet<QstComment> QstComments { get; set; }
        public DbSet<ToolModel> ToolModels { get; set; }
        public DbSet<ToolModelHistory> ToolModelHistories { get; set; }
        public DbSet<ToolModelChanges> ToolModelChanges { get; set; }
        public DbSet<GlobalHistory> GlobalHistories { get; set; }
        public DbSet<UsageStatistic> UsageStatistics { get; set; }
        public DbSet<WorkingCalendarEntry> WorkingCalendarEntries { get; set; }
        public DbSet<WorkingCalendar> WorkingCalendars { get; set; }
        public DbSet<Shift_Worktime> Shift_Worktimes { get; set; }
        public DbSet<ShiftHistory> ShiftHistory { get; set; }
        public DbSet<ShiftChanges> ShiftChanges { get; set; }
        public DbSet<QstSetup> QstSetups { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<StatusHistory> StatusHistories { get; set; }
        public DbSet<StatusChanges> StatusChanges { get; set; }
        public DbSet<ToleranceClass> ToleranceClasses { get; set; }
        public DbSet<ToleranceClassHistory> ToleranceClassHistories { get; set; }
        public DbSet<ToleranceClassChanges> ToleranceClassChanges { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<ToolChanges> ToolChanges { get; set; }
        public DbSet<ToolHistory> ToolHistories { get; set; }
        public DbSet<TestLevelSet> TestLevelSets { get; set; }
        public DbSet<TestLevel> TestLevels { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationChanges> LocationChanges { get; set; }
        public DbSet<LocationHistory> LocationHistories { get; set; }
        public DbSet<LocationDirectory> LocationDirectories { get; set; }
        public DbSet<LocationDirectoryChanges> LocationDirectoryChanges { get; set; }
        public DbSet<LocationDirectoryHistory> LocationDirectoryHistories { get; set; }
        public DbSet<CondRot> CondRots { get; set; }
        public DbSet<CondRotChanges> CondRotChanges { get; set; }
        public DbSet<CondRotHistory> CondRotHistories { get; set; }
        public DbSet<LocPow> LocPows { get; set; }
        public DbSet<LocPowChanges> LocPowChanges { get; set; }
        public DbSet<LocPowHistory> LocPowHistories { get; set; }
        public DbSet<ClassicMfuTest> ClassicMfuTests { get; set; }
        public DbSet<ClassicMfuTestValue> ClassicMfuTestValues { get; set; }
        public DbSet<ClassicMfuTestLocation> ClassicMfuTestLocations { get; set; }
        public DbSet<ClassicChkTest> ClassicChkTests { get; set; }
        public DbSet<ClassicChkTestValue> ClassicChkTestValues { get; set; }
        public DbSet<ClassicChkTestLocation> ClassicChkTestLocations { get; set; }
        public DbSet<ClassicProcessTest> ClassicProcessTest { get; set; }
        public DbSet<ClassicProcessTestLocation> ClassicProcessTestLocations { get; set; }
        public DbSet<ClassicProcessTestValue> ClassicProcessTestValues { get; set; }
        public DbSet<QstList> QstLists { get; set; }
        public DbSet<QstListChanges> QstListChanges { get; set; }
        public DbSet<ConstructionTypeHistory> ConstructionTypeHistories { get; set; }
        public DbSet<DriveSizeHistory> DriveSizeHistories { get; set; }
        public DbSet<DriveTypeHistory> DriveTypeHistories { get; set; }
        public DbSet<ReasonForToolChangeHistory> ReasonForToolChangeHistories { get; set; }
        public DbSet<ShutoffHistory> ShutoffHistories { get; set; }
        public DbSet<SwitchoffHistory> SwitchoffHistories { get; set; }
        public DbSet<ToolTypeHistory> ToolTypeHistories { get; set; }
        public DbSet<ConfigurableFieldHistory> ConfigurableFieldHistories { get; set; }
        public DbSet<CostCenterHistory> CostCenterHistories { get; set; }
        public DbSet<ToolUsage> ToolUsages { get; set; }
        public DbSet<ToolUsageChanges> ToolUsageChanges { get; set; }
        public DbSet<ToolUsageHistory> ToolUsageHistories { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<TestEquipment> TestEquipments { get; set; }
        public DbSet<TestEquipmentChanges> TestEquipmentChanges { get; set; }
        public DbSet<TestEquipmentHistory> TestEquipmentHistories { get; set; }
        public DbSet<TestEquipmentModel> TestEquipmentModels { get; set; }
        public DbSet<TestEquipmentModelChanges> TestEquipmentModelChanges { get; set; }
        public DbSet<TestEquipmentModelHistory> TestEquipmentModelHistories { get; set; }
        public DbSet<CondLoc> CondLocs { get; set; }
        public DbSet<CondLocChanges> CondLocChanges { get; set; }
        public DbSet<CondLocHistory> CondLocHistories { get; set; }
        public DbSet<CondLocTech> CondLocTechs { get; set; }
        public DbSet<CondLocTechChanges> CondLocTechChanges { get; set; }
        public DbSet<CondLocTechHistory> CondLocTechHistories { get; set; }
        public DbSet<Extension> Extensions { get; set; }
        public DbSet<ExtensionChanges> ExtensionChanges { get; set; }
        public DbSet<ExtensionHistory> ExtensionHistories { get; set; }
        public DbSet<WorkingCalendarEntryHistory> WorkingCalendarEntryHistories { get; set; }
        public DbSet<WorkingCalendarEntryChanges> WorkingCalendarEntryChanges { get; set; }
        public DbSet<WorkingCalendarHistory> WorkingCalendarHistories { get; set; }
        public DbSet<WorkingCalendarChanges> WorkingCalendarChanges { get; set; }
        public DbSet<TestLevelSetHistory> TestLevelSetHistory { get; set; }
        public DbSet<TestLevelSetChanges> TestLevelSetChanges { get; set; }
        public DbSet<TestLevelHistory> TestLevelHistory { get; set; }
        public DbSet<TestLevelChanges> TestLevelChanges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsageStatistic>().HasKey(u => new { u.ACTION, u.TIMESTAMP });
            modelBuilder.Entity<WorkingCalendarEntry>().HasKey(u => new { u.FDDate, u.Name, u.Area });

            modelBuilder.Entity<ClassicMfuTest>()
                .HasOne(c => c.GlobalHistory)
                .WithOne(l => l.ClassicMfuTest)
                .HasForeignKey<ClassicMfuTest>(c => c.GlobalHistoryId);

            modelBuilder.Entity<ClassicMfuTest>()
                .HasOne(c => c.TestEquipment)
                .WithMany(t => t.ClassicMfuTests)
                .HasForeignKey(c => c.TEST_DEVICE_ID);

            modelBuilder.Entity<ClassicMfuTest>()
                .HasOne(c => c.ClassicMfuTestLocation)
                .WithOne(l => l.ClassicMfuTest)
                .HasForeignKey<ClassicMfuTestLocation>(c => c.GLOBALHISTORYID);

            modelBuilder.Entity<ClassicChkTest>()
                .HasOne(c => c.GlobalHistory)
                .WithOne(l => l.ClassicChkTest)
                .HasForeignKey<ClassicChkTest>(c => c.GlobalHistoryId);

            modelBuilder.Entity<ClassicChkTest>()
                .HasOne(c => c.ClassicChkTestLocation)
                .WithOne(l => l.ClassicChkTest)
                .HasForeignKey<ClassicChkTestLocation>(c => c.GLOBALHISTORYID);

            modelBuilder.Entity<ClassicChkTest>()
                .HasOne(c => c.TestEquipment)
                .WithMany(t => t.ClassicChkTests)
                .HasForeignKey(c => c.TEST_DEVICE_ID);

            modelBuilder.Entity<ClassicProcessTest>()
                .HasOne(c => c.GlobalHistory)
                .WithOne(l => l.ClassicProcessTest)
                .HasForeignKey<ClassicProcessTest>(c => c.GlobalHistoryId);

            modelBuilder.Entity<ClassicProcessTest>()
                .HasOne(c => c.ClassicProcessTestLocation)
                .WithOne(l => l.ClassicProcessTest)
                .HasForeignKey<ClassicProcessTestLocation>(c => c.GLOBALHISTORYID);

            modelBuilder.Entity<ClassicProcessTest>()
                .HasOne(c => c.TestEquipment)
                .WithMany(t => t.ClassicProcessTests)
                .HasForeignKey(c => c.TEST_DEVICE_ID);

            modelBuilder.Entity<TestEquipment>()
                .HasOne(t => t.TestEquipmentModel)
                .WithMany(m => m.TestEquipments)
                .HasForeignKey(t => t.MODELID);

            modelBuilder.Entity<TestEquipmentModel>()
                .HasOne(m => m.Manufacturer)
                .WithMany(m => m.TestEquipmentModels)
                .HasForeignKey(m => m.MANUID);

            modelBuilder.Entity<LocPow>()
                .HasOne(c => c.Location)
                .WithMany(t => t.LocPows)
                .HasForeignKey(c => c.LocId);

            modelBuilder.Entity<LocPow>()
                .HasOne(c => c.CondRot)
                .WithOne(l => l.LocPow)
                .HasForeignKey<CondRot>(c => c.LOCPOWID);

            modelBuilder.Entity<LocPow>()
                .HasOne(c => c.ToolUsage)
                .WithMany(l => l.LocPows)
                .HasForeignKey(c => c.PowPosId);

            modelBuilder.Entity<LocPow>()
                .HasOne(c => c.Tool)
                .WithMany(t => t.LocPows)
                .HasForeignKey(c => c.PowId);

            modelBuilder.Entity<Tool>()
                .HasOne(m => m.ToolModel)
                .WithMany(m => m.Tools)
                .HasForeignKey(m => m.MODELID);

            modelBuilder.Entity<Tool>()
                .HasOne(m => m.Status)
                .WithMany(m => m.Tools)
                .HasForeignKey(m => m.STATEID);

            modelBuilder.Entity<ToolModel>()
                .HasOne(m => m.Manufacturer)
                .WithMany(m => m.ToolModels)
                .HasForeignKey(m => m.MANUID);

            modelBuilder.Entity<Location>()
                .HasOne(m => m.ToleranceClass1)
                .WithMany(m => m.ToleranceClass1Locations)
                .HasForeignKey(m => m.CLASSID);

            modelBuilder.Entity<Location>()
                .HasOne(m => m.ToleranceClass2)
                .WithMany(m => m.ToleranceClass2Locations)
                .HasForeignKey(m => m.CLASSID2);

            modelBuilder.Entity<CondLoc>()
                .HasOne(t => t.Location)
                .WithMany(m => m.CondLocs)
                .HasForeignKey(t => t.LOCID);

            modelBuilder.Entity<CondLocTech>()
                .HasOne(t => t.CondLoc)
                .WithMany(m => m.CondLocTechs)
                .HasForeignKey(t => t.CONDLOCID);

            modelBuilder.Entity<CondLocTech>()
                .HasOne(t => t.Extension)
                .WithMany(m => m.CondLocTechs)
                .HasForeignKey(t => t.EXTENSIONID);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
