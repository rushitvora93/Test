using System;
using BasicTypes;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using FrameworksAndDrivers.RemoteData.GRPC.Common;
using ClassicTestLocation = Core.Entities.ClassicTestLocation;
using DateTime = System.DateTime;
using Location = Core.Entities.Location;
using Manufacturer = Core.Entities.Manufacturer;
using Status = Core.Entities.Status;
using TestEquipment = Core.Entities.TestEquipment;
using TestEquipmentModel = Core.Entities.TestEquipmentModel;
using TestLevelSet = Core.Entities.TestLevelSet;
using TestParameters = Core.Entities.TestParameters;
using TestTechnique = Core.Entities.TestTechnique;
using TimeSpan = System.TimeSpan;
using ToleranceClass = Core.Entities.ToleranceClass;
using Tool = Core.Entities.Tool;
using ToolModel = Core.Entities.ToolModel;
using ToolUsage = Core.Entities.ToolUsage;
using Extension = Core.Entities.Extension;
using Client.Core.Enums;

namespace FrameworksAndDrivers.RemoteData.GRPC.T4Mapper
{
    public class Assigner : Client.Core.Assigner
    {
        public void Assign(Action<BasicTypes.DateTime> setter, DateTime source)
        {
            setter(new BasicTypes.DateTime()
            {
                Ticks = source.Ticks
            });
        }

        public void Assign(Action<DateTime> setter, BasicTypes.DateTime source)
        {
            setter(source == null ? new DateTime() : new DateTime(source.Ticks));
        }

        public void Assign(Action<long> setter, DayOfWeek source)
        {
            setter((long)source);
        }

        public void Assign(Action<DayOfWeek> setter, long source)
        {
            setter((DayOfWeek)source);
        }

        public void Assign(Action<BasicTypes.TimeSpan> setter, TimeSpan source)
        {
            setter(new BasicTypes.TimeSpan()
            {
                ValueInTicks = source.Ticks
            });
        }

        public void Assign(Action<TimeSpan> setter, BasicTypes.TimeSpan source)
        {
            setter(new TimeSpan(source.ValueInTicks));
        }

        public void Assign(Action<long> setter, TestLevelId source)
        {
            setter(source?.ToLong() ?? 0);
        }

        public void Assign(Action<TestLevelId> setter, long source)
        {
            setter(new TestLevelId(source));
        }

        public void Assign(Action<Core.Entities.Interval> setter, BasicTypes.Interval source)
        {
            if (source == null)
            {
                setter(null);
                return;
            }

            setter(new Core.Entities.Interval()
            {
                IntervalValue = source.IntervalValue,
                Type = (IntervalType)source.IntervalType
            });
        }

        public void Assign(Action<BasicTypes.Interval> setter, Core.Entities.Interval source)
        {
            if (source == null)
            {
                setter(null);
                return;
            }

            setter(new BasicTypes.Interval()
            {
                IntervalValue = source.IntervalValue,
                IntervalType = (int)source.Type
            });
        }

        public void Assign(Action<long> setter, TestLevelSetId source)
        {
            setter(source?.ToLong() ?? 0);
        }

        public void Assign(Action<TestLevelSetId> setter, long source)
        {
            setter(new TestLevelSetId(source));
        }

        public void Assign(Action<string> setter, TestLevelSetName source)
        {
            setter(source?.ToDefaultString() ?? "");
        }

        public void Assign(Action<TestLevelSetName> setter, string source)
        {
            setter(source == null ? null : new TestLevelSetName(source));
        }

        public void Assign(Action<DtoTypes.TestLevel> setter, Core.Entities.TestLevel source)
        {
            if (source == null)
            {
                setter(null);
                return;
            }

            var mapper = new Mapper();
            setter(mapper.DirectPropertyMapping(source));
        }

        public void Assign(Action<Core.Entities.TestLevel> setter, DtoTypes.TestLevel source)
        {
            if (source == null)
            {
                setter(null);
                return;
            }

            var mapper = new Mapper();
            setter(mapper.DirectPropertyMapping(source));
        }

        public void Assign(Action<Location> setter, DtoTypes.Location source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<Status> setter, DtoTypes.Status source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<DtoTypes.Status> setter, Status source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<CostCenter> setter, DtoTypes.HelperTableEntity source)
        {
            if (source == null)
            {
                return;
            }

            setter(new CostCenter()
            {
                ListId = new HelperTableEntityId(source.ListId),
                Value = new HelperTableDescription(source.Value)
            });
        }

        public void Assign(Action<DtoTypes.HelperTableEntity> setter, CostCenter source)
        {
            if (source == null)
            {
                return;
            }

            long listId = 0;
            if (source.ListId != null)
            {
                listId = source.ListId.ToLong();
            }

            var value = "";
            if (source.Value != null)
            {
                value = source.Value.ToDefaultString();
            }

            setter(new DtoTypes.HelperTableEntity()
            {
                ListId = listId,
                Value = value
            });
        }

        public void Assign(Action<ToolType> setter, DtoTypes.HelperTableEntity source)
        {
            if (source == null)
            {
                return;
            }

            setter(new ToolType()
            {
                ListId = new HelperTableEntityId(source.ListId),
                Value = new HelperTableDescription(source.Value)
            });
        }

        public void Assign(Action<DtoTypes.HelperTableEntity> setter, ToolType source)
        {
            if (source == null)
            {
                return;
            }

            long listId = 0;
            if (source.ListId != null)
            {
                listId = source.ListId.ToLong();
            }

            var value = "";
            if (source.Value != null)
            {
                value = source.Value.ToDefaultString();
            }

            setter(new DtoTypes.HelperTableEntity()
            {
                ListId = listId,
                Value = value
            });
        }

        public void Assign(Action<ShutOff> setter, DtoTypes.HelperTableEntity source)
        {
            if (source == null)
            {
                return;
            }

            setter(new ShutOff()
            {
                ListId = new HelperTableEntityId(source.ListId),
                Value = new HelperTableDescription(source.Value)
            });
        }

        public void Assign(Action<DtoTypes.HelperTableEntity> setter, ShutOff source)
        {
            if (source == null)
            {
                return;
            }

            long listId = 0;
            if (source.ListId != null)
            {
                listId = source.ListId.ToLong();
            }

            var value = "";
            if (source.Value != null)
            {
                value = source.Value.ToDefaultString();
            }

            setter(new DtoTypes.HelperTableEntity()
            {
                ListId = listId,
                Value = value
            });
        }

        public void Assign(Action<SwitchOff> setter, DtoTypes.HelperTableEntity source)
        {
            if (source == null)
            {
                return;
            }

            setter(new SwitchOff()
            {
                ListId = new HelperTableEntityId(source.ListId),
                Value = new HelperTableDescription(source.Value)
            });
        }

        public void Assign(Action<DtoTypes.HelperTableEntity> setter, SwitchOff source)
        {
            if (source == null)
            {
                return;
            }

            long listId = 0;
            if (source.ListId != null)
            {
                listId = source.ListId.ToLong();
            }

            var value = "";
            if (source.Value != null)
            {
                value = source.Value.ToDefaultString();
            }

            setter(new DtoTypes.HelperTableEntity()
            {
                ListId = listId,
                Value = value
            });
        }

        public void Assign(Action<DriveSize> setter, DtoTypes.HelperTableEntity source)
        {
            if (source == null)
            {
                return;
            }

            setter(new DriveSize()
            {
                ListId = new HelperTableEntityId(source.ListId),
                Value = new HelperTableDescription(source.Value)
            });
        }

        public void Assign(Action<DtoTypes.HelperTableEntity> setter, DriveSize source)
        {
            if (source == null)
            {
                return;
            }

            long listId = 0;
            if (source.ListId != null)
            {
                listId = source.ListId.ToLong();
            }

            var value = "";
            if (source.Value != null)
            {
                value = source.Value.ToDefaultString();
            }

            setter(new DtoTypes.HelperTableEntity()
            {
                ListId = listId,
                Value = value
            });
        }


        public void Assign(Action<DriveType> setter, DtoTypes.HelperTableEntity source)
        {
            if (source == null)
            {
                return;
            }

            setter(new DriveType()
            {
                ListId = new HelperTableEntityId(source.ListId),
                Value = new HelperTableDescription(source.Value)
            });
        }

        public void Assign(Action<DtoTypes.HelperTableEntity> setter, DriveType source)
        {
            if (source == null)
            {
                return;
            }

            long listId = 0;
            if (source.ListId != null)
            {
                listId = source.ListId.ToLong();
            }

            var value = "";
            if (source.Value != null)
            {
                value = source.Value.ToDefaultString();
            }

            setter(new DtoTypes.HelperTableEntity()
            {
                ListId = listId,
                Value = value
            });
        }

        public void Assign(Action<ConstructionType> setter, DtoTypes.HelperTableEntity source)
        {
            if (source == null)
            {
                return;
            }

            setter(new ConstructionType()
            {
                ListId = new HelperTableEntityId(source.ListId),
                Value = new HelperTableDescription(source.Value)
            });
        }

        public void Assign(Action<DtoTypes.HelperTableEntity> setter, ConstructionType source)
        {
            if (source == null)
            {
                return;
            }

            long listId = 0;
            if (source.ListId != null)
            {
                listId = source.ListId.ToLong();
            }

            var value = "";
            if (source.Value != null)
            {
                value = source.Value.ToDefaultString();
            }

            setter(new DtoTypes.HelperTableEntity()
            {
                ListId = listId,
                Value = value
            });
        }

        public void Assign(Action<ConfigurableField> setter, DtoTypes.HelperTableEntity source)
        {
            if (source == null)
            {
                return;
            }

            setter(new ConfigurableField()
            {
                ListId = new HelperTableEntityId(source.ListId),
                Value = new HelperTableDescription(source.Value)
            });
        }

        public void Assign(Action<DtoTypes.HelperTableEntity> setter, ConfigurableField source)
        {
            if (source == null)
            {
                return;
            }

            long listId = 0;
            if (source.ListId != null)
            {
                listId = source.ListId.ToLong();
            }

            var value = "";
            if (source.Value != null)
            {
                value = source.Value.ToDefaultString();
            }

            setter(new DtoTypes.HelperTableEntity()
            {
                ListId = listId,
                Value = value
            });
        }

        public void Assign(Action<ToolModel> setter, DtoTypes.ToolModel source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<DtoTypes.ToolModel> setter, ToolModel source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<DtoTypes.Manufacturer> setter, Manufacturer source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<ClassicTestLocation> setter, DtoTypes.ClassicTestLocation source)
        {
            if (source == null)
            {
                setter(null);
            }
            else
            {
                var testLocation = new ClassicTestLocation()
                {
                    LocationId = new LocationId(source.LocationId),
                    LocationDirectoryId = new LocationDirectoryId(source.LocationDirectoryId),
                    LocationTreePath = source.TreePath == null ? "" : source.TreePath.IsNull ? "" : source.TreePath.Value
                };
                setter(testLocation);
            }
        }

        public void Assign(Action<TestEquipment> setter, DtoTypes.TestEquipment source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<TestEquipmentModel> setter, DtoTypes.TestEquipmentModel source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<DtoTypes.TestEquipment> setter, TestEquipment source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<DtoTypes.TestEquipmentModel> setter, TestEquipmentModel source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<Manufacturer> setter, DtoTypes.Manufacturer source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<DtoTypes.Location> setter, Location source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<DtoTypes.ToleranceClass> setter, ToleranceClass source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<ToleranceClass> setter, DtoTypes.ToleranceClass source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<Tool> setter, DtoTypes.Tool source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<DtoTypes.ToolUsage> setter, ToolUsage source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<ToolUsage> setter, DtoTypes.ToolUsage source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<TestParameters> setter, DtoTypes.TestParameters source)
        {
            if (source == null)
            {
                setter(null);
            }
            else
            {
                var mapper = new Mapper();
                var testParam = mapper.DirectPropertyMapping(source);

                if (source.ToleranceClass1 != null)
                {
                    testParam.ToleranceClassTorque = mapper.DirectPropertyMapping(source.ToleranceClass1);
                }

                if (source.ToleranceClass2 != null)
                {
                    testParam.ToleranceClassAngle = mapper.DirectPropertyMapping(source.ToleranceClass2);
                }

                setter(testParam);
            }
        }

        public void Assign(Action<DtoTypes.TestParameters> setter, TestParameters source)
        {
            if (source == null)
            {
                setter(null);
            }
            else
            {
                var mapper = new Mapper();
                var testParam = mapper.DirectPropertyMapping(source);

                if (source.ToleranceClassTorque != null)
                {
                    testParam.ToleranceClass1 = mapper.DirectPropertyMapping(source.ToleranceClassTorque);
                }

                if (source.ToleranceClassAngle != null)
                {
                    testParam.ToleranceClass2 = mapper.DirectPropertyMapping(source.ToleranceClassAngle);
                }

                setter(testParam);
            }
        }

        public void Assign(Action<TestTechnique> setter, DtoTypes.TestTechnique source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<DtoTypes.TestTechnique> setter, TestTechnique source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<DtoTypes.Tool> setter, Tool source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<TestLevelSet> setter, DtoTypes.TestLevelSet source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<DtoTypes.TestLevelSet> setter, TestLevelSet source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<long> setter, LocationToolAssignmentId source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToLong());
        }

        public void Assign(Action<TestEquipmentModelId> setter, long source)
        {
            setter(new TestEquipmentModelId(source));
        }

        public void Assign(Action<long> setter, TestEquipmentModelId source)
        {
            if (source == null)
            {
                return;
            }
            setter(source.ToLong());
        }

        public void Assign(Action<LocationToolAssignmentId> setter, long source)
        {
            setter(new LocationToolAssignmentId(source));
        }

        public void Assign(Action<BasicTypes.NullableInt> setter, Shift? source)
        {
            if (source == null)
            {
                setter(new NullableInt() { IsNull = true });
            }
            else
            {
                setter(new NullableInt()
                {
                    Value = (int)source.Value,
                    IsNull = false
                });
            }
        }

        public void Assign(Action<Shift?> setter, BasicTypes.NullableInt source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter((Shift)source.Value);
            }
        }

        public void Assign(Action<BasicTypes.NullableDateTime> setter, DateTime? source)
        {
            if (source == null)
            {
                setter(new NullableDateTime() { IsNull = true });
            }
            else
            {
                BasicTypes.DateTime dtoDateTime = null;
                Assign(x => dtoDateTime = x, source.Value);
                setter(new NullableDateTime()
                {
                    Value = dtoDateTime,
                    IsNull = false
                });
            }
        }

        public void Assign(Action<DateTime?> setter, BasicTypes.NullableDateTime source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                DateTime dateTime = new DateTime();
                Assign(x => dateTime = x, source.Value);
                setter(dateTime);
            }
        }

        public void Assign(Action<BasicTypes.NullableString> setter, string source)
        {
            if (source == null)
            {
                setter(new NullableString() { IsNull = true, Value = "" });
            }
            else
            {
                setter(new NullableString()
                {
                    Value = source,
                    IsNull = false
                });
            }
        }

        public void Assign(Action<string> setter, BasicTypes.NullableString source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter(source.Value);
            }
        }

        public void Assign(Action<ConfigurableFieldString40> setter, BasicTypes.NullableString source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter(new ConfigurableFieldString40(source.Value));
            }
        }

        public void Assign(Action<BasicTypes.NullableString> setter, ConfigurableFieldString40 source)
        {
            setter(source == null
                ? new NullableString() {IsNull = true}
                : new NullableString() {IsNull = false, Value = source.ToDefaultString()});
        }

        public void Assign(Action<ConfigurableFieldString80> setter, BasicTypes.NullableString source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter(new ConfigurableFieldString80(source.Value));
            }
        }

        public void Assign(Action<BasicTypes.NullableString> setter, ConfigurableFieldString80 source)
        {
            setter(source == null
                ? new NullableString() { IsNull = true }
                : new NullableString() { IsNull = false, Value = source.ToDefaultString() });
        }

        public void Assign(Action<ConfigurableFieldString250> setter, BasicTypes.NullableString source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter(new ConfigurableFieldString250(source.Value));
            }
        }
        public void Assign(Action<BasicTypes.NullableString> setter, ConfigurableFieldString250 source)
        {
            setter(source == null
                ? new NullableString() { IsNull = true }
                : new NullableString() { IsNull = false, Value = source.ToDefaultString() });
        }

        public void Assign(Action<double?> setter, BasicTypes.NullableDouble source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter(source.Value);
            }
        }

        public void Assign(Action<BasicTypes.NullableDouble> setter, double? source)
        {
            if (source == null)
            {
                setter(new NullableDouble() { IsNull = true, Value = 0 });
            }
            else
            {
                setter(new NullableDouble()
                {
                    Value = source.Value,
                    IsNull = false
                });
            }
        }

        public void Assign(Action<long?> setter, BasicTypes.NullableLong source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter(source.Value);
            }
        }

        public void Assign(Action<Angle> setter, BasicTypes.NullableLong source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter(Angle.FromDegree(source.Value));
            }
        }

        public void Assign(Action<Angle> setter, BasicTypes.NullableDouble source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter(Angle.FromDegree(source.Value));
            }
        }

        public void Assign(Action<Torque> setter, BasicTypes.NullableDouble source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter(Torque.FromNm(source.Value));
            }
        }

        public void Assign(Action<BasicTypes.NullableLong> setter, long? source)
        {
            if (source == null)
            {
                setter(new NullableLong { IsNull = true });
                return;
            }
            setter(new NullableLong { IsNull = false, Value = source.Value });
        }

        public void Assign(Action<BasicTypes.NullableLong> setter, Angle source)
        {
            if (source == null)
            {
                setter(new NullableLong { IsNull = true });
                return;
            }
            setter(new NullableLong { IsNull = false, Value = (long)source.Degree });
        }

        public void Assign(Action<BasicTypes.NullableDouble> setter, Angle source)
        {
            if (source == null)
            {
                setter(new NullableDouble { IsNull = true });
                return;
            }
            setter(new NullableDouble { IsNull = false, Value = source.Degree });
        }

        public void Assign(Action<BasicTypes.NullableDouble> setter, Torque source)
        {
            if (source == null)
            {
                setter(new NullableDouble { IsNull = true });
                return;
            }
            setter(new NullableDouble { IsNull = false, Value = source.Nm });
        }

        public void Assign(Action<BasicTypes.NullableString> setter, LocationConfigurableField1 source)
        {
            if (source?.ToDefaultString() == null)
            {
                setter(new NullableString() { IsNull = true, Value = "" });
            }
            else
            {
                setter(new NullableString()
                {
                    Value = source.ToDefaultString(),
                    IsNull = false
                });
            }
        }

        public void Assign(Action<LocationConfigurableField1> setter, BasicTypes.NullableString source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter(new LocationConfigurableField1(source.Value));
            }
        }

        public void Assign(Action<BasicTypes.NullableString> setter, LocationConfigurableField2 source)
        {
            if (source?.ToDefaultString() == null)
            {
                setter(new NullableString() { IsNull = true, Value = "" });
            }
            else
            {
                setter(new NullableString()
                {
                    Value = source.ToDefaultString(),
                    IsNull = false
                });
            }
        }

        public void Assign(Action<LocationConfigurableField2> setter, BasicTypes.NullableString source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter(new LocationConfigurableField2(source.Value));
            }
        }

        public void Assign(Action<BasicTypes.NullableString> setter, TestEquipmentInventoryNumber source)
        {
            if (source?.ToDefaultString() == null)
            {
                setter(new NullableString() { IsNull = true, Value = "" });
            }
            else
            {
                setter(new NullableString()
                {
                    Value = source.ToDefaultString(),
                    IsNull = false
                });
            }
        }

        public void Assign(Action<TestEquipmentInventoryNumber> setter, BasicTypes.NullableString source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter(new TestEquipmentInventoryNumber(source.Value));
            }
        }

        public void Assign(Action<BasicTypes.NullableString> setter, TestEquipmentVersion source)
        {
            if (source?.ToDefaultString() == null)
            {
                setter(new NullableString() { IsNull = true, Value = "" });
            }
            else
            {
                setter(new NullableString()
                {
                    Value = source.ToDefaultString(),
                    IsNull = false
                });
            }
        }

        public void Assign(Action<TestEquipmentVersion> setter, BasicTypes.NullableString source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter(new TestEquipmentVersion(source.Value));
            }
        }

        public void Assign(Action<BasicTypes.NullableString> setter, CalibrationNorm source)
        {
            if (source?.ToDefaultString() == null)
            {
                setter(new NullableString() { IsNull = true, Value = "" });
            }
            else
            {
                setter(new NullableString()
                {
                    Value = source.ToDefaultString(),
                    IsNull = false
                });
            }
        }

        public void Assign(Action<CalibrationNorm> setter, BasicTypes.NullableString source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter(new CalibrationNorm(source.Value));
            }
        }

        public void Assign(Action<BasicTypes.NullableString> setter, ManufacturerName source)
        {
            if (source?.ToDefaultString() == null)
            {
                setter(new NullableString() { IsNull = true, Value = "" });
            }
            else
            {
                setter(new NullableString()
                {
                    Value = source.ToDefaultString(),
                    IsNull = false
                });
            }
        }

        public void Assign(Action<ManufacturerName> setter, BasicTypes.NullableString source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter(new ManufacturerName(source.Value));
            }
        }

        public void Assign(Action<BasicTypes.NullableString> setter, TestEquipmentSerialNumber source)
        {
            if (source?.ToDefaultString() == null)
            {
                setter(new NullableString() { IsNull = true, Value = "" });
            }
            else
            {
                setter(new NullableString()
                {
                    Value = source.ToDefaultString(),
                    IsNull = false
                });
            }
        }

        public void Assign(Action<TestEquipmentSerialNumber> setter, BasicTypes.NullableString source)
        {
            if (source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter(new TestEquipmentSerialNumber(source.Value));
            }
        }

        public void Assign(Action<BasicTypes.NullableString> setter, TestEquipmentSetupPath source)
        {
            if (source?.ToDefaultString() == null)
            {
                setter(new NullableString() { IsNull = true, Value = "" });
            }
            else
            {
                setter(new NullableString()
                {
                    Value = source.ToDefaultString(),
                    IsNull = false
                });
            }
        }

        public void Assign(Action<TestEquipmentSetupPath> setter, BasicTypes.NullableString source)
        {
            if (source == null || source.IsNull)
            {
                setter(new TestEquipmentSetupPath(""));
            }
            else
            {
                setter(new TestEquipmentSetupPath(source.Value));
            }
        }

        public void Assign(Action<DtoTypes.ProcessControlTech> setter, Client.Core.Entities.ProcessControlTech source)
        {
            setter(ProcessControlTechConverter.ConvertEntityToDto(source));
        }

        public void Assign(Action<Client.Core.Entities.ProcessControlTech> setter, DtoTypes.ProcessControlTech source)
        {
            setter(ProcessControlTechConverter.ConvertDtoToEntity(source));
        }
        
        public void Assign(Action<DtoTypes.Extension> setter, Extension source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<Extension> setter, DtoTypes.Extension source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<DtoTypes.ShiftManagement> setter, ShiftManagement source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<long> setter, UserId source)
        {
            setter(source?.ToLong() ?? 0);
        }

        public void Assign(Action<UserId> setter, long source)
        {
            setter(new UserId(source));
        }

        public void Assign(Action<DiffType> setter, long source)
        {
            setter((DiffType)source);
        }

        public void Assign(Action<HistoryComment> setter, string source)
        {
            setter(new HistoryComment(source));
        }

        public void Assign(Action<User> setter, DtoTypes.User source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }
    }
}
