using System;
using System.Collections.Generic;
using BasicTypes;
using Common.Types.Enums;
using Core.Entities;
using DtoTypes;
using FrameworksAndDrivers.NetworkView.Common;
using Server.Core.Entities;
using Server.Core.Enums;
using ClassicChkTestValue = Server.Core.Entities.ClassicChkTestValue;
using ClassicTestLocation = Server.Core.Entities.ClassicTestLocation;
using DateTime = System.DateTime;
using Location = Server.Core.Entities.Location;
using Manufacturer = Server.Core.Entities.Manufacturer;
using Status = Server.Core.Entities.Status;
using TestEquipment = Server.Core.Entities.TestEquipment;
using TestEquipmentModel = Server.Core.Entities.TestEquipmentModel;
using TestLevelSet = Server.Core.Entities.TestLevelSet;
using TestParameters = Server.Core.Entities.TestParameters;
using TestTechnique = Server.Core.Entities.TestTechnique;
using TimeSpan = System.TimeSpan;
using ToleranceClass = Server.Core.Entities.ToleranceClass;
using Tool = Server.Core.Entities.Tool;
using ToolModel = Server.Core.Entities.ToolModel;
using ToolUsage = Server.Core.Entities.ToolUsage;
using User = Server.Core.Entities.User;
using Extension = Server.Core.Entities.Extension;

namespace FrameworksAndDrivers.NetworkView.T4Mapper
{
    public class Assigner : global::Server.Core.Assigner
    {
        public void Assign(Action<BasicTypes.DateTime> setter, DateTime source)
        {
            setter(new BasicTypes.DateTime()
            {
                Ticks = source.Ticks
            });
        }

        public void Assign(Action<BasicTypes.DateTime> setter, DateTime? source)
        {
            Assign(setter, source ?? DateTime.Now);
        }

        public void Assign(Action<DateTime> setter, BasicTypes.DateTime source)
        {
            setter(source == null ? new DateTime() : new DateTime(source.Ticks));
        }

        public void Assign(Action<long> setter, WorkingCalendarEntryType source)
        {
            setter(source == WorkingCalendarEntryType.Holiday ? 1 : 0);
        }

        public void Assign(Action<long> setter, WorkingCalendarEntryRepetition source)
        {
            setter(source == WorkingCalendarEntryRepetition.Yearly ? 1 : 0);
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

        public void Assign(Action<DtoTypes.TestLevel> setter, Server.Core.Entities.TestLevel source)
        {
            if (source == null)
            {
                setter(null);
                return;
            }

            var mapper = new Mapper();
            setter(mapper.DirectPropertyMapping(source));
        }

        public void Assign(Action<Server.Core.Entities.TestLevel> setter, DtoTypes.TestLevel source)
        {
            if(source == null)
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

        public void Assign(Action<DtoTypes.Location> setter, Location source)
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
                Value = new HelperTableEntityValue(source.Value)
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
                NodeId = State.NodeId.ToolType,
                Value = new HelperTableEntityValue(source.Value),
                Alive = source.Alive
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
                Alive = source.Alive,
                Value = value,
                NodeId = (long)source.NodeId
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
                NodeId = State.NodeId.ShutOff,
                Value = new HelperTableEntityValue(source.Value),
                Alive = source.Alive
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
                Alive = source.Alive,
                Value = value,
                NodeId = (long)source.NodeId
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
                NodeId = State.NodeId.SwitchOff,
                Value = new HelperTableEntityValue(source.Value),
                Alive = source.Alive
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
                Alive = source.Alive,
                Value = value,
                NodeId = (long)source.NodeId
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
                NodeId = State.NodeId.DriveType,
                Value = new HelperTableEntityValue(source.Value),
                Alive = source.Alive
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
                Alive = source.Alive,
                Value = value,
                NodeId = (long)source.NodeId
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
                NodeId = State.NodeId.DriveSize,
                Value = new HelperTableEntityValue(source.Value),
                Alive = source.Alive
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
                Alive = source.Alive,
                Value = value,
                NodeId = (long)source.NodeId
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
                NodeId = State.NodeId.ConstructionType,
                Value = new HelperTableEntityValue(source.Value),
                Alive = source.Alive
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
                Alive = source.Alive,
                Value = value,
                NodeId = (long)source.NodeId
            });
        }

        public void Assign(Action<Server.Core.Entities.User> setter, DtoTypes.User source)
        {
            if (source == null)
            {
                return;
            }
            
            setter(new User()
            {
                UserId = new UserId(source.UserId),
                UserName = source.UserName
            });
        }

        public void Assign(Action<DtoTypes.User> setter, Server.Core.Entities.User source)
        {
            if (source == null)
            {
                return;
            }

            setter(new DtoTypes.User()
            {
                UserId = source.UserId.ToLong(),
                UserName = source.UserName
            });
        }

        public void Assign(Action<List<Server.Core.Entities.ClassicChkTestValue>> setter, ListOfClassicChkTestValue source)
        {
            if (source == null)
            {
                return;
            }

            var mapper = new Mapper();
            var values = new List<ClassicChkTestValue>();
            foreach (var val in source.ClassicChkTestValues)
            {
                values.Add(mapper.DirectPropertyMapping(val));
            }
            setter(values);
        }

        public void Assign(Action<ListOfClassicChkTestValue> setter, List<Server.Core.Entities.ClassicChkTestValue> source)
        {
            if (source == null)
            {
                return;
            }

            var mapper = new Mapper();
            var values = new ListOfClassicChkTestValue();
            foreach (var val in source)
            {
                values.ClassicChkTestValues.Add(mapper.DirectPropertyMapping(val));
            }
            setter(values);
        }

        public void Assign(Action<List<Server.Core.Entities.ClassicMfuTestValue>> setter, ListOfClassicMfuTestValue source)
        {
            if (source == null)
            {
                return;
            }

            var mapper = new Mapper();
            var values = new List<Server.Core.Entities.ClassicMfuTestValue>();
            foreach (var val in source.ClassicMfuTestValues)
            {
                values.Add(mapper.DirectPropertyMapping(val));
            }
            setter(values);
        }

        public void Assign(Action<ListOfClassicMfuTestValue> setter, List<Server.Core.Entities.ClassicMfuTestValue> source)
        {
            if (source == null)
            {
                return;
            }

            var mapper = new Mapper();
            var values = new ListOfClassicMfuTestValue();
            foreach (var val in source)
            {
                values.ClassicMfuTestValues.Add(mapper.DirectPropertyMapping(val));
            }
            setter(values);
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
                Value = new HelperTableEntityValue(source.Value)
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

        public void Assign(Action<Manufacturer> setter, DtoTypes.Manufacturer source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<DtoTypes.Manufacturer> setter, Manufacturer source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<DtoTypes.ClassicTestLocation> setter, ClassicTestLocation source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<ClassicTestLocation> setter, DtoTypes.ClassicTestLocation source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<DtoTypes.TestEquipment> setter, TestEquipment source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<TestEquipment> setter, DtoTypes.TestEquipment source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<DtoTypes.TestEquipmentModel> setter, TestEquipmentModel source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<TestEquipmentModel> setter, DtoTypes.TestEquipmentModel source)
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

                if (source.ToleranceClass1 != null)
                {
                    testParam.ToleranceClass1 = mapper.DirectPropertyMapping(source.ToleranceClass1);
                }

                if (source.ToleranceClass2 != null)
                {
                    testParam.ToleranceClass2 = mapper.DirectPropertyMapping(source.ToleranceClass2);
                }

                setter(testParam);
            }
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
                    testParam.ToleranceClass1 = mapper.DirectPropertyMapping(source.ToleranceClass1);
                }

                if (source.ToleranceClass2 != null)
                {
                    testParam.ToleranceClass2 = mapper.DirectPropertyMapping(source.ToleranceClass2);
                }

                setter(testParam);
            }
        }

        public void Assign(Action<DtoTypes.TestTechnique> setter, TestTechnique source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<TestTechnique> setter, DtoTypes.TestTechnique source)
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

        public void Assign(Action<BasicTypes.NullableInt> setter, Shift? source)
        {
            if(source == null)
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
            if(source == null || source.IsNull)
            {
                setter(null);
            }
            else
            {
                setter((Shift)source.Value);
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

        public void Assign(Action<BasicTypes.NullableLong> setter, long? source)
        {
            if (source == null)
            {
                setter(new NullableLong{IsNull = true});
                return;
            }
            setter(new NullableLong{IsNull = false, Value = source.Value});
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
                setter(new NullableString() { IsNull = true, Value = ""});
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
                setter(null);
            }
            else
            {
                setter(new TestEquipmentSetupPath(source.Value));
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

        public void Assign(Action<BasicTypes.NullableString> setter, ConfigurableFieldString40 source)
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

        public void Assign(Action<BasicTypes.NullableString> setter, ConfigurableFieldString80 source)
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

        public void Assign(Action<BasicTypes.NullableString> setter, ConfigurableFieldString250 source)
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

        public void Assign(Action<TestEquipmentBehaviourAskForIdent> setter, long source)
        {
            setter((TestEquipmentBehaviourAskForIdent)source);
        }

        public void Assign(Action<long> setter, TestEquipmentBehaviourAskForIdent source)
        {
            setter((long)source);
        }

        public void Assign(Action<TestEquipmentBehaviourConfirmMeasurements> setter, long source)
        {
            setter((TestEquipmentBehaviourConfirmMeasurements)source);
        }

        public void Assign(Action<long> setter, TestEquipmentBehaviourConfirmMeasurements source)
        {
            setter((long)source);
        }

        public void Assign(Action<TestEquipmentBehaviourTransferCurves> setter, long source)
        {
            setter((TestEquipmentBehaviourTransferCurves)source);
        }

        public void Assign(Action<long> setter, TestEquipmentBehaviourTransferCurves source)
        {
            setter((long)source);
        }

        public void Assign(Action<DtoTypes.ProcessControlTech> setter, Server.Core.Entities.ProcessControlTech source)
        {
            setter(ProcessControlTechConverter.ConvertEntityToDto(source));
        }

        public void Assign(Action<Server.Core.Entities.ProcessControlTech> setter, DtoTypes.ProcessControlTech source)
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

        public void Assign(Action<Server.Core.Entities.ShiftManagement> setter, DtoTypes.ShiftManagement source)
        {
            var mapper = new Mapper();
            setter(source != null ? mapper.DirectPropertyMapping(source) : null);
        }

        public void Assign(Action<User> setter, long source)
        {
            setter(new User() { UserId = new UserId(source) });
        }

        public void Assign(Action<HistoryComment> setter, string source)
        {
            setter(new HistoryComment(source));
        }

        public void Assign(Action<long> setter, UserId source)
        {
            setter(source?.ToLong() ?? 0);
        }

        public void Assign(Action<UserId> setter, long source)
        {
            setter(new UserId(source));
        }

        public void Assign(Action<int> setter, DiffType source)
        {
            setter((int)source);
        }
    }
}
