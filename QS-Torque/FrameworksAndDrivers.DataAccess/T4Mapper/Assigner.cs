using System;
using Common.Types.Enums;
using FrameworksAndDrivers.DataAccess.DbEntities;
using Server.Core.Enums;

namespace FrameworksAndDrivers.DataAccess.T4Mapper
{
    public class Assigner : Server.Core.Assigner
    {
        public void Assign(Action<Server.Core.Entities.TestEquipment> setter, TestEquipment source)
        {
            if (source == null) return;

            var mapper = new Mapper();
            setter(mapper.DirectPropertyMapping(source));
        }

        public void Assign(Action<Server.Core.Entities.ClassicTestLocation> setter, ClassicChkTestLocation source)
        {
            if (source == null) return;

            var mapper = new Mapper();
            setter(mapper.DirectPropertyMapping(source));
        }

        public void Assign(Action<Server.Core.Entities.ClassicTestLocation> setter, ClassicProcessTestLocation source)
        {
            if (source == null) return;

            var mapper = new Mapper();
            setter(mapper.DirectPropertyMapping(source));
        }

        public void Assign(Action<Server.Core.Entities.ClassicTestLocation> setter, ClassicMfuTestLocation source)
        {
            if (source == null) return;

            var mapper = new Mapper();
            setter(mapper.DirectPropertyMapping(source));
        }
        public void Assign(Action<Server.Core.Entities.TestEquipmentModel> setter, TestEquipmentModel source)
        {
            if (source == null) return;

            var mapper = new Mapper();
            setter(mapper.DirectPropertyMapping(source));
        }

        public void Assign(Action<Server.Core.Entities.Manufacturer> setter, Manufacturer source)
        {
            if (source == null) return;

            var mapper = new Mapper();
            setter(mapper.DirectPropertyMapping(source));
        }

        public void Assign(Action<Server.Core.Entities.ToolModel> setter, ToolModel source)
        {
            if (source == null) return;

            var mapper = new Mapper();
            setter(mapper.DirectPropertyMapping(source));
        }

        public void Assign(Action<ToolModel> setter, Server.Core.Entities.ToolModel source)
        {
            if (source == null) return;

            var mapper = new Mapper();
            setter( mapper.DirectPropertyMapping(source));
        }

        public void Assign(Action<Server.Core.Entities.ToleranceClass> setter, ToleranceClass source)
        {
            if (source == null) return;

            var mapper = new Mapper();
            setter(mapper.DirectPropertyMapping(source));
        }

        public void Assign(Action<long?> setter, Shift? source)
        {
            setter((long?)source);
        }

        public void Assign(Action<Shift> setter, long? source)
        {
            setter((Shift?)source ?? Shift.FirstShiftOfDay);
        }

        public void Assign(Action<TestEquipmentBehaviourAskForIdent> setter, TestEquipmentBehaviourAskForIdent? source)
        {
            setter(source.GetValueOrDefault(TestEquipmentBehaviourAskForIdent.Never));
        }

        public void Assign(Action<TestEquipmentBehaviourAskForIdent?> setter, TestEquipmentBehaviourAskForIdent source)
        {
            setter(source);
        }


        public void Assign(Action<TestEquipmentBehaviourConfirmMeasurements> setter, TestEquipmentBehaviourConfirmMeasurements? source)
        {
            setter(source.GetValueOrDefault(TestEquipmentBehaviourConfirmMeasurements.Never));
        }

        public void Assign(Action<TestEquipmentBehaviourConfirmMeasurements?> setter, TestEquipmentBehaviourConfirmMeasurements source)
        {
            setter(source);
        }

        public void Assign(Action<TestEquipmentBehaviourTransferCurves> setter, TestEquipmentBehaviourTransferCurves? source)
        {
            setter(source.GetValueOrDefault(TestEquipmentBehaviourTransferCurves.Never));
        }

        public void Assign(Action<TestEquipmentBehaviourTransferCurves?> setter, TestEquipmentBehaviourTransferCurves source)
        {
            setter(source);
        }

        public void Assign(Action<Server.Core.Entities.Extension> setter, Extension source)
        {
            if (source == null) return;

            var mapper = new Mapper();
            setter(mapper.DirectPropertyMapping(source));
        }

        public void Assign(Action<DateTime?> setter, DateTime? source)
        {
            setter(source);
        }

        public void Assign(Action<long> setter, Server.Core.Entities.LocationId source)
        {
            setter(source.ToLong());
        }

        public void Assign(Action<Server.Core.Entities.UserId> setter, long source)
        {
            setter(new Server.Core.Entities.UserId(source));
        }
    }
}
