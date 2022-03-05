using System;
using Common.Types.Enums;
using Core.Entities;
using Core.Enums;
using Server.Core.Entities;
using Server.Core.Enums;
using State;

namespace Server.Core
{
    public class Assigner
    {
        public void Assign(Action<TestEquipmentType> setter, long source)
        {
            setter((TestEquipmentType)source);
        }
        public void Assign(Action<TestEquipmentType> setter, TestEquipmentType source)
        {
            setter((TestEquipmentType)source);
        }

        public void Assign(Action<long> setter, TestEquipmentType source)
        {
            setter((long)source);
        }

        public void Assign(Action<DateTime> setter, DateTime source)
        {
            setter(source);
        }

        public void Assign(Action<int> setter, int source)
        {
            setter(source);
        }

        public void Assign(Action<string> setter, string source)
        {
            setter(source);
        }

        public void Assign(Action<double> setter, double? source)
        {
            setter(source.GetValueOrDefault());
        }

        public void Assign(Action<double?> setter, double source)
        {
            setter(source);
        }

        public void Assign(Action<bool> setter, bool? source)
        {
            setter(source.GetValueOrDefault());
        }

        public void Assign(Action<bool?> setter, bool source)
        {
            setter(source);
        }

        public void Assign(Action<long> setter, long? source)
        {
            setter(source.GetValueOrDefault());
        }

        public void Assign(Action<bool> setter, long? source)
        {
            setter(source.GetValueOrDefault() != 0);
        }

        public void Assign(Action<GroupId> setter, long source)
        {
            setter(new GroupId(source));
        }

        public void Assign(Action<long> setter, GroupId source)
        {
            setter(source.ToLong());
        }

        public void Assign(Action<ManufacturerId> setter, long source)
        {
            setter(new ManufacturerId(source));
        }

        public void Assign(Action<long> setter, ManufacturerId source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToLong());
        }

        public void Assign(Action<ManufacturerName> setter, string source)
        {
            setter(new ManufacturerName(source));
        }

        public void Assign(Action<string> setter, ManufacturerName source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }

        public void Assign(Action<long> setter, bool source)
        {
            setter(source ? 1 : 0);
        }

        public void Assign(Action<long?> setter, WorkingCalendarEntryType source)
        {
            setter((long)source);
        }

        public void Assign(Action<WorkingCalendarEntryType> setter, long? source)
        {
            setter((WorkingCalendarEntryType?)source ?? WorkingCalendarEntryType.Holiday);
        }

        public void Assign(Action<long?> setter, WorkingCalendarEntryRepetition source)
        {
            setter((long)source);
        }

        public void Assign(Action<WorkingCalendarEntryRepetition> setter, long? source)
        {
            setter((WorkingCalendarEntryRepetition?)source ?? WorkingCalendarEntryRepetition.Once);
        }

        public void Assign(Action<string> setter, WorkingCalendarEntryDescription source)
        {
            setter(source.ToDefaultString());
        }

        public void Assign(Action<WorkingCalendarEntryDescription> setter, string source)
        {
            setter(new WorkingCalendarEntryDescription(source));
        }

        public void Assign(Action<string> setter, TimeSpan source)
        {
            setter(source.ToString());
        }

        public void Assign(Action<TimeSpan> setter, string source)
        {
            TimeSpan value;
            var successful = TimeSpan.TryParse(source, out value);
            setter(successful ? value : TimeSpan.Zero);
        }

        public void Assign(Action<IntervalType> setter, IntervalType source)
        {
            setter(source);
        }

        public void Assign(Action<long> setter, User source)
        {
            setter(source?.UserId?.ToLong() ?? 0);
        }

        public void Assign(Action<string> setter, HistoryComment source)
        {
            setter(source?.ToDefaultString() ?? "");
        }

        public void Assign(Action<long> setter, WorkingCalendarId source)
        {
            setter(source?.ToLong() ?? 0);
        }

        public void Assign(Action<string> setter, WorkingCalendarName source)
        {
            setter(source?.ToDefaultString() ?? "");
        }

        public void Assign(Action<WorkingCalendarId> setter, long source)
        {
            setter(new WorkingCalendarId(source));
        }

        public void Assign(Action<WorkingCalendarName> setter, string source)
        {
            setter(new WorkingCalendarName(source));
        }

        public void Assign(Action<QstIdentifier> setter, long source)
        {
            setter(new ManufacturerId(source));
        }

        public void Assign(Action<long> setter, QstIdentifier source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToLong());
        }

        public void Assign(Action<StatusDescription> setter, string source)
        {
            setter(new StatusDescription(source));
        }

        public void Assign(Action<string> setter, StatusDescription source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }

        public void Assign(Action<TestEquipmentModelName> setter, string source)
        {
            setter(new TestEquipmentModelName(source));
        }

        public void Assign(Action<string> setter, TestEquipmentModelName source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }


        public void Assign(Action<TestEquipmentInventoryNumber> setter, string source)
        {
            setter(new TestEquipmentInventoryNumber(source));
        }

        public void Assign(Action<string> setter, TestEquipmentInventoryNumber source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }

        public void Assign(Action<ExtensionInventoryNumber> setter, string source)
        {
            setter(new ExtensionInventoryNumber(source));
        }

        public void Assign(Action<string> setter, ExtensionInventoryNumber source)
        {
            if (source == null)
            {
                return;
            }

            setter(source.ToDefaultString());
        }

        public void Assign(Action<TestEquipmentSerialNumber> setter, string source)
        {
            setter(new TestEquipmentSerialNumber(source));
        }

        public void Assign(Action<string> setter, TestEquipmentSerialNumber source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }

        public void Assign(Action<TestEquipmentVersion> setter, string source)
        {
            setter(new TestEquipmentVersion(source));
        }

        public void Assign(Action<string> setter, TestEquipmentVersion source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }

        public void Assign(Action<CalibrationNorm> setter, string source)
        {
            setter(new CalibrationNorm(source));
        }

        public void Assign(Action<string> setter, CalibrationNorm source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }

        public void Assign(Action<TestEquipmentSetupPath> setter, string source)
        {
            setter(new TestEquipmentSetupPath(source));
        }

        public void Assign(Action<string> setter, TestEquipmentSetupPath source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }

        public void Assign(Action<StatusId> setter, long source)
        {
            setter(new StatusId(source));
        }

        public void Assign(Action<long> setter, StatusId source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToLong());
        }

        public void Assign(Action<ToleranceClassId> setter, long source)
        {
            setter(new ToleranceClassId(source));
        }

        public void Assign(Action<long> setter, ToleranceClassId source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToLong());
        }

        public void Assign(Action<ToleranceClass> setter, long? source)
        {
            if (source == null)
            {
                return;
            }
            setter(new ToleranceClass() { Id = new ToleranceClassId(source.Value)});
        }

        public void Assign(Action<Location> setter, long? source)
        {
            if (source == null)
            {
                return;
            }
            setter(new Location() { Id = new LocationId(source.Value) });
        }

        public void Assign(Action<long> setter, Location source)
        {
            if (source?.Id == null)
            {
                return;
            }
            setter(source.Id.ToLong());
        }

        public void Assign(Action<Tool> setter, long? source)
        {
            if (source == null)
            {
                return;
            }
            setter(new Tool() { Id = new ToolId(source.Value) });
        }

        public void Assign(Action<ToolUsage> setter, long? source)
        {
            if (source == null)
            {
                return;
            }
            setter(new ToolUsage() { Id = new ToolUsageId(source.Value) });
        }

        public void Assign(Action<long> setter, ToleranceClass source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.Id.ToLong());
        }

        public void Assign(Action<int> setter, long source)
        {
            setter((int)source);
        }

        public void Assign(Action<TestLevelId> setter, long source)
        {
            setter(new TestLevelId(source));
        }

        public void Assign(Action<long> setter, TestLevelId source)
        {
            setter(source.ToLong());
        }

        public void Assign(Action<TestLevelSetId> setter, long source)
        {
            setter(new TestLevelSetId(source));
        }

        public void Assign(Action<long> setter, TestLevelSetId source)
        {
            setter(source.ToLong());
        }


        public void Assign(Action<TestLevelSet> setter, long? source)
        {
            if (source!= null)
            {
                setter(new TestLevelSet() { Id = new TestLevelSetId(source.Value) });
            }
        }

        public void Assign(Action<long?> setter, TestLevelSet source)
        {
            if (source?.Id != null)
            {
                setter(source.Id.ToLong());
            }
        }


        public void Assign(Action<TestLevelSetName> setter, string source)
        {
            setter(new TestLevelSetName(source));
        }

        public void Assign(Action<string> setter, TestLevelSetName source)
        {
            if (source != null)
            {
                setter(source.ToDefaultString());
            }            
        }

        public void Assign(Action<long> setter, LocationId source)
        {
            setter(source?.ToLong() ?? 0);
        }

        public void Assign(Action<LocationId> setter, long source)
        {
            setter(new LocationId(source));
        }

        public void Assign(Action<string> setter, LocationNumber source)
        {
            setter(source?.ToDefaultString() == null ? "" : source.ToDefaultString());
        }

        public void Assign(Action<LocationNumber> setter, string source)
        {
            setter(new LocationNumber(source));
        }

        public void Assign(Action<string> setter, LocationDescription source)
        {
            setter(source?.ToDefaultString() == null ? "" : source.ToDefaultString());
        }

        public void Assign(Action<LocationDescription> setter, string source)
        {
            setter(new LocationDescription(source));
        }

        public void Assign(Action<long> setter, ToolId source)
        {
            setter(source.ToLong());
        }

        public void Assign(Action<ToolId> setter, long source)
        {
            setter(new ToolId(source));
        }

        public void Assign(Action<long> setter, LocationToolAssignmentId source)
        {
            if (source != null)
            {
                setter(source.ToLong());
            }
        }
        
        public void Assign(Action<LocationToolAssignmentId> setter, long? source)
        {
            if (source != null)
            {
                setter(new LocationToolAssignmentId(source.Value));
            }
        }

        public void Assign(Action<long> setter, TestEquipmentModelId source)
        {
            if (source != null)
            {
                setter(source.ToLong());
            }
        }


        public void Assign(Action<TestEquipmentModelId> setter, long? source)
        {
            if (source != null)
            {
                setter(new TestEquipmentModelId(source.Value));
            }
        }

        public void Assign(Action<TestEquipmentModel> setter, long? source)
        {
            if (source == null)
            {
                return;
            }
            
            setter(new TestEquipmentModel(){Id = new TestEquipmentModelId(source.Value)});
        }

        public void Assign(Action<long?> setter, TestEquipmentModel source)
        {
            if (source == null)
            {
                return;
            }

            setter(source.Id.ToLong());
        }

        public void Assign(Action<Manufacturer> setter, long? source)
        {
            if (source == null)
            {
                return;
            }

            setter(new Manufacturer() { Id = new ManufacturerId(source.Value) });
        }

        public void Assign(Action<QstSetupId> setter, long source)
        {
            setter(new QstSetupId(source));
        }

        public void Assign(Action<long> setter, QstSetupId source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToLong());
        }

        public void Assign(Action<QstSetupName> setter, string source)
        {
            setter(new QstSetupName(source));
        }

        public void Assign(Action<string> setter, QstSetupName source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }

        public void Assign(Action<QstSetupValue> setter, string source)
        {
            setter(new QstSetupValue(source));
        }

        public void Assign(Action<string> setter, QstSetupValue source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }

        public void Assign(Action<HelperTableEntityId> setter, long source)
        {
            setter(new HelperTableEntityId(source));
        }

        public void Assign(Action<long> setter, HelperTableEntityId source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToLong());
        }

        public void Assign(Action<HelperTableEntityValue> setter, string source)
        {
            setter(new HelperTableEntityValue(source));
        }

        public void Assign(Action<string> setter, HelperTableEntityValue source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }

        public void Assign(Action<ToolUsageId> setter, long source)
        {
            setter(new ToolUsageId(source));
        }

        public void Assign(Action<long> setter, ToolUsageId source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToLong());
        }

        public void Assign(Action<TestEquipmentId> setter, long source)
        {
            setter(new TestEquipmentId(source));
        }

        public void Assign(Action<long> setter, TestEquipmentId source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToLong());
        }

        public void Assign(Action<ToolUsageDescription> setter, string source)
        {
            setter(new ToolUsageDescription(source));
        }

        public void Assign(Action<string> setter, ToolUsageDescription source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }

        public void Assign(Action<NodeId> setter, long source)
        {
            setter((NodeId)source);
        }

        public void Assign(Action<long> setter, NodeId source)
        {
            setter((long)source);
        }

        public void Assign(Action<Shift?> setter, long? source)
        {
            setter((Shift?)source);
        }


        public void Assign(Action<LocationDirectoryId> setter, long? source)
        {
            if (source == null)
            {
                return;
            }

            setter(new LocationDirectoryId(source.Value));
        }

        public void Assign(Action<long?> setter, LocationDirectoryId source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToLong());
        }

        public void Assign(Action<LocationDirectoryName> setter, string source)
        {
            setter(new LocationDirectoryName(source));
        }

        public void Assign(Action<string> setter, LocationDirectoryName source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }

        public void Assign(Action<GlobalHistoryId> setter, long source)
        {
            setter(new GlobalHistoryId(source));
        }

        public void Assign(Action<long> setter, GlobalHistoryId source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToLong());
        }

        public void Assign(Action<LocationControlledBy> setter, long source)
        {
            setter((LocationControlledBy) source);
        }

        public void Assign(Action<long> setter, LocationControlledBy source)
        {
            setter((long)source);
        }


        public void Assign(Action<MeaUnit> setter, long source)
        {
            setter((MeaUnit)source);
        }

        public void Assign(Action<long> setter, MeaUnit source)
        {
            setter((int)source);
        }

        public void Assign(Action<MeaUnit?> setter, MeaUnit source)
        {
            setter(source);
        }

        public void Assign(Action<MeaUnit> setter, MeaUnit? source)
        {
            setter(source.GetValueOrDefault(MeaUnit.Nm));
        }

        public void Assign(Action<TestResult> setter, long source)
        {
            setter(new TestResult(source));
        }

        public void Assign(Action<long> setter, TestResult source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.LongValue);
        }

        public void Assign(Action<LocationConfigurableField1> setter, string source)
        {
            setter(new LocationConfigurableField1(source));
        }

        public void Assign(Action<string> setter, LocationConfigurableField1 source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }

        public void Assign(Action<LocationConfigurableField2> setter, string source)
        {
            setter(new LocationConfigurableField2(source));
        }

        public void Assign(Action<string> setter, LocationConfigurableField2 source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }

        public void Assign(Action<byte[]> setter, byte[] source)
        {
            setter(source);
        }

        public void Assign(Action<IntervalType> setter, long? source)
        {
            setter(source == null ? IntervalType.EveryXDays : (IntervalType)source);
        }

        public void Assign(Action<long?> setter, IntervalType source)
        {
            setter((long)source);
        }

        public void Assign(Action<ToolModelId> setter, long source)
        {
            setter(new ToolModelId(source));
        }

        public void Assign(Action<ToolModel> setter, long? source)
        {
            if (source != null)
            {
                setter(new ToolModel(){Id = new ToolModelId(source.Value)});
            }
        }

        public void Assign(Action<long?> setter, ToolModel source)
        {
            setter(source?.Id?.ToLong());
        }

        public void Assign(Action<Status> setter, long? source)
        {
            if (source != null)
            {
                setter(new Status() { Id = new StatusId(source.Value) });
            }
        }

        public void Assign(Action<long?> setter, Status source)
        {
            setter(source?.Id?.ToLong());
        }

        public void Assign(Action<ConfigurableField> setter, long? source)
        {
            if (source != null)
            {
                setter(new ConfigurableField() { ListId = new HelperTableEntityId(source.Value) });
            }
        }

        public void Assign(Action<long?> setter, ConfigurableField source)
        {
            setter(source?.ListId?.ToLong());
        }

        public void Assign(Action<CostCenter> setter, long? source)
        {
            if (source != null)
            {
                setter(new CostCenter() { ListId = new HelperTableEntityId(source.Value) });
            }
        }

        public void Assign(Action<long?> setter, CostCenter source)
        {
            setter(source?.ListId?.ToLong());
        }

        public void Assign(Action<ToolType> setter, long? source)
        {
            if (source != null)
            {
                setter(new ToolType() { ListId = new HelperTableEntityId(source.Value) });
            }
        }

        public void Assign(Action<long?> setter, ToolType source)
        {
            setter(source?.ListId?.ToLong());
        }

        public void Assign(Action<SwitchOff> setter, long? source)
        {
            if (source != null)
            {
                setter(new SwitchOff() { ListId = new HelperTableEntityId(source.Value) });
            }
        }

        public void Assign(Action<long?> setter, SwitchOff source)
        {
            setter(source?.ListId?.ToLong());
        }

        public void Assign(Action<DriveType> setter, long? source)
        {
            if (source != null)
            {
                setter(new DriveType() { ListId = new HelperTableEntityId(source.Value) });
            }
        }

        public void Assign(Action<long?> setter, DriveType source)
        {
            setter(source?.ListId?.ToLong());
        }

        public void Assign(Action<DriveSize> setter, long? source)
        {
            if (source != null)
            {
                setter(new DriveSize() { ListId = new HelperTableEntityId(source.Value) });
            }
        }

        public void Assign(Action<long?> setter, DriveSize source)
        {
            setter(source?.ListId?.ToLong());
        }

        public void Assign(Action<ShutOff> setter, long? source)
        {
            if (source != null)
            {
                setter(new ShutOff() { ListId = new HelperTableEntityId(source.Value) });
            }
        }

        public void Assign(Action<long?> setter, ShutOff source)
        {
            setter(source?.ListId?.ToLong());
        }

        public void Assign(Action<ConstructionType> setter, long? source)
        {
            if (source != null)
            {
                setter(new ConstructionType() { ListId = new HelperTableEntityId(source.Value) });
            }
        }

        public void Assign(Action<long?> setter, ConstructionType source)
        {
            setter(source?.ListId?.ToLong());
        }

        public void Assign(Action<ConfigurableFieldString40> setter, string source)
        {
            setter(new ConfigurableFieldString40(source));
        }

        public void Assign(Action<string> setter, ConfigurableFieldString40 source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }

        public void Assign(Action<ConfigurableFieldString80> setter, string source)
        {
            setter(new ConfigurableFieldString80(source));
        }

        public void Assign(Action<string> setter, ConfigurableFieldString80 source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }

        public void Assign(Action<ConfigurableFieldString250> setter, string source)
        {
            setter(new ConfigurableFieldString250(source));
        }

        public void Assign(Action<string> setter, ConfigurableFieldString250 source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToDefaultString());
        }

        public void Assign(Action<long> setter, Server.Core.Entities.TestEquipment source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.Id.ToLong());
        }

        public void Assign(Action<ToolModelType> setter, long source)
        {
            setter((ToolModelType)source);
        }

        public void Assign(Action<long> setter, ToolModelType source)
        {
            setter((long)source);
        }

        public void Assign(Action<ToolModelClass> setter, long source)
        {
            setter((ToolModelClass)source);
        }

        public void Assign(Action<long> setter, ToolModelClass source)
        {
            setter((long)source);
        }

        public void Assign(Action<long> setter, Manufacturer source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.Id.ToLong());
        }

        public void Assign(Action<Interval> setter, long? source)
        {
            setter(new Interval()
            {
                IntervalValue = source.GetValueOrDefault()
            });
        }

        public void Assign(Action<long?> setter, Interval source)
        {
            setter(source?.IntervalValue);
        }

        public void Assign(Action<long> setter, ProcessControlConditionId source)
        {
            setter(source.ToLong());
        }

        public void Assign(Action<ProcessControlConditionId> setter, long source)
        {
            setter(new ProcessControlConditionId(source));
        }

        public void Assign(Action<long> setter, ProcessControlTechId source)
        {
            setter(source.ToLong());
        }

        public void Assign(Action<ProcessControlTechId> setter, long source)
        {
            setter(new ProcessControlTechId(source));
        }

        public void Assign(Action<long> setter, ManufacturerIds source)
        {
            setter((long)source);
        }

        public void Assign(Action<ManufacturerIds> setter, ManufacturerIds source)
        {
            setter(source);
        }

        public void Assign(Action<ManufacturerIds> setter, long source)
        {
            setter((ManufacturerIds)source);
        }

        public void Assign(Action<long> setter, TestMethod source)
        {
            setter((long)source);
        }

        public void Assign(Action<TestMethod> setter, long source)
        {
            setter((TestMethod)source);
        }

        public void Assign(Action<TestMethod> setter, TestMethod source)
        {
            setter(source);
        }


        public void Assign(Action<ExtensionId> setter, long source)
        {
            setter(new ExtensionId(source));
        }

        public void Assign(Action<long> setter, ExtensionId source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToLong());
        }

        public void Assign(Action<Extension> setter, long? source)
        {
            if (source == null)
            {
                return;
            }
            setter(new Extension() { Id = new ExtensionId(source.Value) });
        }

        public void Assign(Action<long> setter, Extension source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.Id.ToLong());
        }
    }
}
