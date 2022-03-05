using System;
using Client.Core.Entities;
using Common.Types.Enums;
using Core.Entities;
using Core.Entities.ToolTypes;
using Core.Enums;
using Core.PhysicalValueTypes;

namespace Client.Core
{
    public class Assigner
    {
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

        public void Assign(Action<long> setter, long? source)
        {
            setter(source.GetValueOrDefault());
        }

        public void Assign(Action<bool> setter, long? source)
        {
            setter(source.GetValueOrDefault() != 0);
        }

        public void Assign(Action<ToolModelId> setter, long source)
        {
            setter(new ToolModelId(source));
        }

        public void Assign(Action<long> setter, ToolModelId source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToLong());
        }

        public void Assign(Action<GroupId> setter, long source)
        {
            setter(new GroupId(source));
        }

        public void Assign(Action<long> setter, GroupId source)
        {
            setter(source.ToLong());
        }

        public void Assign(Action<HelperTableEntityId> setter, long source)
        {
            setter(new HelperTableEntityId(source));
        }

        public void Assign(Action<long> setter, HelperTableEntityId source)
        {
            setter(source?.ToLong() ?? 0);
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

        public void Assign(Action<ToolId> setter, long source)
        {
            setter(new ToolId(source));
        }

        public void Assign(Action<long> setter, ToolId source)
        {
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

        public void Assign(Action<HelperTableDescription> setter, string source)
        {
            setter(new HelperTableDescription(source));
        }

        public void Assign(Action<string> setter, HelperTableDescription source)
        {
            if (source == null)
            {
                return;
            }

            setter(source.ToDefaultString());
        }

        public void Assign(Action<long> setter, LocationId source)
        {
            if (source == null)
            {
                return;
            }

            setter(source.ToLong());
        }

        public void Assign(Action<string> setter, LocationNumber source)
        {
            if (source == null)
            {
                return;
            }

            setter(source.ToDefaultString());
        }

        public void Assign(Action<string> setter, LocationDescription source)
        {
            if (source == null)
            {
                return;
            }

            setter(source.ToDefaultString());
        }

        public void Assign(Action<long> setter, LocationDirectoryId source)
        {
            if (source == null)
            {
                setter(-1);
                return;
            }

            setter(source.ToLong());
        }

        public void Assign(Action<string> setter, LocationDirectoryName source)
        {
            if (source == null)
            {
                return;
            }

            setter(source.ToDefaultString());
        }

        public void Assign(Action<LocationDirectoryName> setter, string source)
        {
            setter(new LocationDirectoryName(source));
        }

        public void Assign(Action<string> setter, TestEquipmentSetupPath source)
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

        public void Assign(Action<long> setter, LocationControlledBy source)
        {
            setter((long) source);
        }

        public void Assign(Action<long> setter, TestEquipmentBehaviourAskForIdent source)
        {
            setter((long)source);
        }

        public void Assign(Action<TestEquipmentBehaviourAskForIdent> setter, long source)
        {
            setter((TestEquipmentBehaviourAskForIdent)source);
        }

        public void Assign(Action<long> setter, TestEquipmentBehaviourConfirmMeasurements source)
        {
            setter((long)source);
        }

        public void Assign(Action<TestEquipmentBehaviourConfirmMeasurements> setter, long source)
        {
            setter((TestEquipmentBehaviourConfirmMeasurements)source);
        }

        public void Assign(Action<TestEquipmentBehaviourTransferCurves> setter, long source)
        {
            setter((TestEquipmentBehaviourTransferCurves)source);
        }

        public void Assign(Action<long> setter, TestEquipmentBehaviourTransferCurves source)
        {
            setter((long)source);
        }

        public void Assign(Action<string> setter, LocationConfigurableField1 source)
        {
            if (source == null)
            {
                return;
            }

            setter(source.ToDefaultString());
        }

        public void Assign(Action<string> setter, LocationConfigurableField2 source)
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

        public void Assign(Action<long> setter, ToleranceClass source)
        {
            if (source == null)
            {
                return;
            }

            setter(source.Id.ToLong());
        }

        public void Assign(Action<ToleranceClass> setter, long source)
        {
            setter(new ToleranceClass() {Id = new ToleranceClassId(source)});
        }

        public void Assign(Action<LocationId> setter, long source)
        {
            setter(new LocationId(source));
        }

        public void Assign(Action<LocationDirectoryId> setter, long? source)
        {
            setter(new LocationDirectoryId(source.GetValueOrDefault(-1)));
        }

        public void Assign(Action<LocationNumber> setter, string source)
        {
            setter(new LocationNumber(source));
        }

        public void Assign(Action<LocationDescription> setter, string source)
        {
            setter(new LocationDescription(source));
        }

        public void Assign(Action<LocationConfigurableField1> setter, string source)
        {
            setter(new LocationConfigurableField1(source));
        }

        public void Assign(Action<LocationConfigurableField2> setter, string source)
        {
            setter(new LocationConfigurableField2(source));
        }

        public void Assign(Action<LocationControlledBy> setter, long? source)
        {
            setter((LocationControlledBy) source.GetValueOrDefault());
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

        public void Assign(Action<Torque> setter, double source)
        {
            setter(Torque.FromNm(source));
        }

        public void Assign(Action<double> setter, Torque source)
        {
            setter(source?.Nm ?? 0);
        }

        public void Assign(Action<Angle> setter, double source)
        {
            setter(Angle.FromDegree(source));
        }

        public void Assign(Action<double> setter, Angle source)
        {
            setter(source?.Degree ?? 0);
        }

        public void Assign(Action<Angle> setter, long source)
        {
            setter(Angle.FromDegree(source));
        }

        public void Assign(Action<long?> setter, Angle source)
        {
            if (source?.Degree != null)
            {
                setter((long)source.Degree);
            }
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


        public void Assign(Action<string> setter, TestEquipmentSerialNumber source)
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

        public void Assign(Action<TestEquipmentInventoryNumber> setter, string source)
        {
            setter(new TestEquipmentInventoryNumber(source));
        }

        public void Assign(Action<string> setter, TestEquipmentModelName source)
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


        public void Assign(Action<long> setter, TestResult source)
        {
            if (source == null)
            {
                return;
            }

            setter(source.LongValue);
        }

        public void Assign(Action<TestResult> setter, long source)
        {
            setter(new TestResult(source));
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

        public void Assign(Action<MeaUnit> setter, long source)
        {
            setter((MeaUnit) source);
        }

        public void Assign(Action<long> setter, MeaUnit source)
        {
            setter((long) source);
        }

        public void Assign(Action<long?> setter, WorkingCalendarEntryType source)
        {
            setter((long) source);
        }

        public void Assign(Action<WorkingCalendarEntryType> setter, long? source)
        {
            setter((WorkingCalendarEntryType?) source ?? WorkingCalendarEntryType.Holiday);
        }

        public void Assign(Action<long?> setter, WorkingCalendarEntryRepetition source)
        {
            setter((long) source);
        }

        public void Assign(Action<WorkingCalendarEntryRepetition> setter, long? source)
        {
            setter((WorkingCalendarEntryRepetition?) source ?? WorkingCalendarEntryRepetition.Once);
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

        public void Assign(Action<long> setter, TestPlanId source)
        {
            setter(source?.ToLong() ?? 0);
        }

        public void Assign(Action<string> setter, TestPlanName source)
        {
            setter(source?.ToDefaultString() ?? "");
        }

        public void Assign(Action<TestPlanId> setter, long source)
        {
            setter(new TestPlanId(source));
        }

        public void Assign(Action<TestPlanName> setter, string source)
        {
            setter(new TestPlanName(source));
        }

        public void Assign(Action<IntervalType> setter, IntervalType source)
        {
            setter(source);
        }

        public void Assign(Action<TestPlanBehavior> setter, TestPlanBehavior source)
        {
            setter(source);
        }

        public void Assign(Action<bool> setter, bool source)
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

        public void Assign(Action<long> setter, WorkingCalendarEntryRepetition source)
        {
            setter(source == WorkingCalendarEntryRepetition.Yearly ? 1 : 0);
        }

        public void Assign(Action<long> setter, WorkingCalendarEntryType source)
        {
            setter(source == WorkingCalendarEntryType.Holiday ? 1 : 0);
        }

        public void Assign(Action<WorkingCalendarId> setter, long source)
        {
            setter(new WorkingCalendarId(source));
        }

        public void Assign(Action<TestEquipmentType> setter, long source)
        {
            setter((TestEquipmentType) source);
        }

        public void Assign(Action<long> setter, TestEquipmentType source)
        {
            setter((long) source);
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


        public void Assign(Action<AbstractToolType> setter, long source)
        {
            AbstractToolType modelType = null;
            var enumType = (ToolModelType) source;
            switch (enumType)
            {
                case ToolModelType.PulseDriver:
                    modelType = new PulseDriver();
                    break;
                case ToolModelType.PulseDriverShutOff:
                    modelType = new PulseDriverShutOff();
                    break;
                case ToolModelType.General:
                    modelType = new General();
                    break;
                case ToolModelType.ECDriver:
                    modelType = new ECDriver();
                    break;
                case ToolModelType.ClickWrench:
                    modelType = new ClickWrench();
                    break;
                case ToolModelType.MDWrench:
                    modelType = new MDWrench();
                    break;
                case ToolModelType.ProductionWrench:
                    modelType = new ProductionWrench();
                    break;
                default: break;
            }

            setter(modelType);
        }

        public void Assign(Action<long> setter, AbstractToolType source)
        {
            long value;
            switch (source)
            {
                case ClickWrench clickWrench:
                    value = (long) ToolModelType.ClickWrench;
                    break;
                case ECDriver ecDriver:
                    value = (long) ToolModelType.ECDriver;
                    break;
                case General general:
                    value = (long) ToolModelType.General;
                    break;
                case MDWrench mdWrench:
                    value = (long) ToolModelType.MDWrench;
                    break;
                case ProductionWrench productionWrench:
                    value = (long) ToolModelType.ProductionWrench;
                    break;
                case PulseDriver pulseDriver:
                    value = (long) ToolModelType.PulseDriver;
                    break;
                case PulseDriverShutOff pulseDriverShutOff:
                    value = (long) ToolModelType.PulseDriverShutOff;
                    break;
                default:
                    value = -1;
                    break;
            }

            setter(value);
        }

        public void Assign(Action<ToolModelClass> setter, long source)
        {
            setter((ToolModelClass)source);
        }

        public void Assign(Action<long> setter, ToolModelClass source)
        {
            setter((long)source);
        }

        public void Assign(Action<int> setter, DataGateVersion source)
        {
            if (source != null)
            {
                setter(source.DataGateVersionsId);
            }
        }

        public void Assign(Action<DataGateVersion> setter, int source)
        {
            setter(new DataGateVersion(source));
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

        public void Assign(Action<Extension> setter, long source)
        {
            setter(new Extension() { Id = new ExtensionId(source) });
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

        public void Assign(Action<string> setter, ToolModelDescription source)
        {
            if (source == null)
            {
                return;
            }

            setter(source.ToDefaultString());
        }

        public void Assign(Action<ToolModelDescription> setter, string source)
        {
            setter(new ToolModelDescription(source));
        }

        public void Assign(Action<string> setter, ToolInventoryNumber source)
        {
            if (source == null)
            {
                return;
            }

            setter(source.ToDefaultString());
        }

        public void Assign(Action<ToolInventoryNumber> setter, string source)
        {
            setter(new ToolInventoryNumber(source));
        }

        public void Assign(Action<string> setter, ToolSerialNumber source)
        {
            if (source == null)
            {
                return;
            }

            setter(source.ToDefaultString());
        }

        public void Assign(Action<ToolSerialNumber> setter, string source)
        {
            setter(new ToolSerialNumber(source));
        }
    }
}
