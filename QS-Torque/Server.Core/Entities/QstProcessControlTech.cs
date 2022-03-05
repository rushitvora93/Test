using Common.Types.Enums;
using Server.Core.Entities;

namespace Server.Core.Entities
{
    public class QstProcessControlTech : ProcessControlTech
    {
        public QstProcessControlTech()
        {
            ManufacturerId = ManufacturerIds.ID_QST;
        }

        public long? AngleLimitMt { get; set; }
        public double? StartMeasurementPeak { get; set; }
        public double? StartAngleCountingPa { get; set; }
        public double? AngleForFurtherTurningPa { get; set; }
        public double? TargetAnglePa { get; set; }
        public double? StartMeasurementPa { get; set; }
        public double? AlarmTorquePa { get; set; }
        public double? AlarmAnglePa { get; set; }
        public double? MinimumTorqueMt { get; set; }
        public double? StartAngleMt { get; set; }
        public double? StartMeasurementMt { get; set; }
        public double? AlarmTorqueMt { get; set; }
        public double? AlarmAngleMt { get; set; }

        public override bool EqualsByContent(ProcessControlTech other)
        {
            var qstProcessControlTech = (QstProcessControlTech)other;
            if (qstProcessControlTech == null)
            {
                return false;
            }

            return this.Id.Equals(other.Id) &&
                    this.ProcessControlConditionId.Equals(other.ProcessControlConditionId) &&
                    this.ManufacturerId == other.ManufacturerId &&
                    this.TestMethod == other.TestMethod &&
                    (this.Extension?.EqualsByContent(other.Extension) ?? other.Extension == null) &&
                    this.AngleLimitMt == qstProcessControlTech.AngleLimitMt &&
                    this.StartMeasurementPeak == qstProcessControlTech.StartMeasurementPeak &&
                    this.StartAngleCountingPa == qstProcessControlTech.StartAngleCountingPa &&
                    this.AngleForFurtherTurningPa == qstProcessControlTech.AngleForFurtherTurningPa &&
                    this.TargetAnglePa == qstProcessControlTech.TargetAnglePa &&
                    this.StartMeasurementPa == qstProcessControlTech.StartMeasurementPa &&
                    this.AlarmTorquePa == qstProcessControlTech.AlarmTorquePa &&
                    this.AlarmAnglePa == qstProcessControlTech.AlarmAnglePa &&
                    this.MinimumTorqueMt == qstProcessControlTech.MinimumTorqueMt &&
                    this.StartAngleMt == qstProcessControlTech.StartAngleMt &&
                    this.StartMeasurementMt == qstProcessControlTech.StartMeasurementMt &&
                    this.AlarmTorqueMt == qstProcessControlTech.AlarmTorqueMt &&
                    this.AlarmAngleMt == qstProcessControlTech.AlarmAngleMt &&
                    this.Alive == other.Alive;
        }

        public override bool EqualsById(ProcessControlTech other)
        {
            return this.Id.Equals(other?.Id);
        }

        public override void UpdateWith(ProcessControlTech other)
        {
            var qstProcessControlTech = (QstProcessControlTech)other;
            if (qstProcessControlTech == null)
            {
                return;
            }

            this.Id = other.Id;
            this.ProcessControlConditionId = other.ProcessControlConditionId;
            this.ManufacturerId = other.ManufacturerId;
            this.TestMethod = other.TestMethod;
            this.Extension = other.Extension;
            this.AngleLimitMt = qstProcessControlTech.AngleLimitMt;
            this.StartMeasurementPeak = qstProcessControlTech.StartMeasurementPeak;
            this.StartAngleCountingPa = qstProcessControlTech.StartAngleCountingPa;
            this.AngleForFurtherTurningPa = qstProcessControlTech.AngleForFurtherTurningPa;
            this.TargetAnglePa = qstProcessControlTech.TargetAnglePa;
            this.StartMeasurementPa = qstProcessControlTech.StartMeasurementPa;
            this.AlarmTorquePa = qstProcessControlTech.AlarmTorquePa;
            this.AlarmAnglePa = qstProcessControlTech.AlarmAnglePa;
            this.MinimumTorqueMt = qstProcessControlTech.MinimumTorqueMt;
            this.StartAngleMt = qstProcessControlTech.StartAngleMt;
            this.StartMeasurementMt = qstProcessControlTech.StartMeasurementMt;
            this.AlarmTorqueMt = qstProcessControlTech.AlarmTorqueMt;
            this.AlarmAngleMt = qstProcessControlTech.AlarmAngleMt;
            this.Alive = other.Alive;
        }

        public override ProcessControlTech CopyDeep()
        {
            return new QstProcessControlTech()
            {
                Id = this.Id != null ? new ProcessControlTechId(this.Id.ToLong()) : null,
                ProcessControlConditionId = this.ProcessControlConditionId != null ? new ProcessControlConditionId(this.ProcessControlConditionId.ToLong()) : null,
                TestMethod = this.TestMethod,
                ManufacturerId = this.ManufacturerId,
                Extension = this.Extension?.CopyDeep(),
                AngleLimitMt = this.AngleLimitMt,
                StartMeasurementPeak = this.StartMeasurementPeak,
                StartAngleCountingPa = this.StartAngleCountingPa,
                AngleForFurtherTurningPa = this.AngleForFurtherTurningPa,
                TargetAnglePa = this.TargetAnglePa,
                StartMeasurementPa = this.StartMeasurementPa,
                AlarmTorquePa = this.AlarmTorquePa,
                AlarmAnglePa = this.AlarmAnglePa,
                MinimumTorqueMt = this.MinimumTorqueMt,
                StartAngleMt = this.StartAngleMt,
                StartMeasurementMt = this.StartMeasurementMt,
                AlarmTorqueMt = this.AlarmTorqueMt,
                AlarmAngleMt = this.AlarmAngleMt,
                Alive = this.Alive,
            };
        }
    }
}
