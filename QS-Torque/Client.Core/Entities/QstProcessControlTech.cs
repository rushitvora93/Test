using Common.Types.Enums;
using Core.PhysicalValueTypes;

namespace Client.Core.Entities
{
    public class QstProcessControlTech : ProcessControlTech
    {
        public QstProcessControlTech()
        {
            ManufacturerId = ManufacturerIds.ID_QST;
        }

        public Angle AngleLimitMt { get; set; }
        public Torque StartMeasurementPeak { get; set; }
        public Torque StartAngleCountingPa { get; set; }
        public Angle AngleForFurtherTurningPa { get; set; }
        public Angle TargetAnglePa { get; set; }
        public Torque StartMeasurementPa { get; set; }
        public Torque AlarmTorquePa { get; set; }
        public Angle AlarmAnglePa { get; set; }
        public Torque MinimumTorqueMt { get; set; }
        public Torque StartAngleMt { get; set; }
        public Torque StartMeasurementMt { get; set; }
        public Torque AlarmTorqueMt { get; set; }
        public Angle AlarmAngleMt { get; set; }

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
                    (this.Extension?.EqualsByContent(qstProcessControlTech.Extension) ?? qstProcessControlTech.Extension == null) &&
                    (this.AngleLimitMt?.Equals(qstProcessControlTech.AngleLimitMt) ?? qstProcessControlTech.AngleLimitMt == null) &&
                    (this.StartMeasurementPeak?.Equals(qstProcessControlTech.StartMeasurementPeak) ?? qstProcessControlTech.StartMeasurementPeak == null) &&
                    (this.StartAngleCountingPa?.Equals(qstProcessControlTech.StartAngleCountingPa) ?? qstProcessControlTech.StartAngleCountingPa == null) &&
                    (this.AngleForFurtherTurningPa?.Equals(qstProcessControlTech.AngleForFurtherTurningPa) ?? qstProcessControlTech.AngleForFurtherTurningPa == null) &&
                    (this.TargetAnglePa?.Equals(qstProcessControlTech.TargetAnglePa) ?? qstProcessControlTech.TargetAnglePa == null) &&
                    (this.StartMeasurementPa?.Equals(qstProcessControlTech.StartMeasurementPa) ?? qstProcessControlTech.StartMeasurementPa == null) &&
                    (this.AlarmTorquePa?.Equals(qstProcessControlTech.AlarmTorquePa) ?? qstProcessControlTech.AlarmTorquePa == null) &&
                    (this.AlarmAnglePa?.Equals(qstProcessControlTech.AlarmAnglePa) ?? qstProcessControlTech.AlarmAnglePa == null) &&
                    (this.MinimumTorqueMt?.Equals(qstProcessControlTech.MinimumTorqueMt) ?? qstProcessControlTech.MinimumTorqueMt == null) &&
                    (this.StartAngleMt?.Equals(qstProcessControlTech.StartAngleMt) ?? qstProcessControlTech.StartAngleMt == null) &&
                    (this.StartMeasurementMt?.Equals(qstProcessControlTech.StartMeasurementMt) ?? qstProcessControlTech.StartMeasurementMt == null) &&
                    (this.AlarmTorqueMt?.Equals(qstProcessControlTech.AlarmTorqueMt) ?? qstProcessControlTech.AlarmTorqueMt == null) &&
                    (this.AlarmAngleMt?.Equals(qstProcessControlTech.AlarmAngleMt) ?? qstProcessControlTech.AlarmAngleMt == null);
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
                AngleLimitMt = this.AngleLimitMt != null ? Angle.FromDegree(this.AngleLimitMt.Degree) : null,
                StartMeasurementPeak = this.StartMeasurementPeak != null ? Torque.FromNm(this.StartMeasurementPeak.Nm) : null,
                StartAngleCountingPa = this.StartAngleCountingPa != null ? Torque.FromNm(this.StartAngleCountingPa.Nm) : null,
                AngleForFurtherTurningPa = this.AngleForFurtherTurningPa != null ? Angle.FromDegree(this.AngleForFurtherTurningPa.Degree) : null,
                TargetAnglePa = this.TargetAnglePa != null ? Angle.FromDegree(this.TargetAnglePa.Degree) : null,
                StartMeasurementPa = this.StartMeasurementPa != null ? Torque.FromNm(this.StartMeasurementPa.Nm) : null,
                AlarmTorquePa = this.AlarmTorquePa != null ? Torque.FromNm(this.AlarmTorquePa.Nm) : null,
                AlarmAnglePa = this.AlarmAnglePa != null ? Angle.FromDegree(this.AlarmAnglePa.Degree) : null,
                MinimumTorqueMt = this.MinimumTorqueMt != null ? Torque.FromNm(this.MinimumTorqueMt.Nm) : null,
                StartAngleMt = this.StartAngleMt != null ? Torque.FromNm(this.StartAngleMt.Nm) : null,
                StartMeasurementMt = this.StartMeasurementMt != null ? Torque.FromNm(this.StartMeasurementMt.Nm) : null,
                AlarmTorqueMt = this.AlarmTorqueMt != null ? Torque.FromNm(this.AlarmTorqueMt.Nm) : null,
                AlarmAngleMt = this.AlarmAngleMt != null ? Angle.FromDegree(this.AlarmAngleMt.Degree) : null
            };
        }

        public ProcessControlTechValidationError? Validate(string property)
        {
            switch (property)
            {
                case nameof(MinimumTorqueMt):
                    if (MinimumTorqueMt.Nm < 0.5 || MinimumTorqueMt.Nm > 999)
                    {
                        return ProcessControlTechValidationError.MinimumTorqueMtNotBetween0Point5And999;
                    }
                    break;

                case nameof(StartAngleMt):
                    if (StartAngleMt.Nm < 0.5 || StartAngleMt.Nm > 999)
                    {
                        return ProcessControlTechValidationError.StartAngleMtNotBetween0Point5And999;
                    }
                    break;

                case nameof(StartMeasurementMt):
                    if (StartMeasurementMt.Nm < 0.5 || StartMeasurementMt.Nm > 999 || StartMeasurementMt.Nm > StartAngleMt.Nm)
                    {
                        return ProcessControlTechValidationError.StartMeasurementMtNotBetween0Point5And999AndGreaterThanStartAngleMt;
                    }
                    break;

                case nameof(AlarmTorqueMt):
                    if (AlarmTorqueMt.Nm < 0 || AlarmTorqueMt.Nm > 9999 || AlarmTorqueMt.Nm != 0 &&
                        (AlarmTorqueMt.Nm < MinimumTorqueMt.Nm || AlarmTorqueMt.Nm < StartAngleMt.Nm || AlarmTorqueMt.Nm < StartMeasurementMt.Nm))
                    {
                        return ProcessControlTechValidationError.AlarmTorqueMtNotBetween0And9999AndLessThanMinimumTorqueMtOrStartAngleMtOrStartMeasurementMt;
                    }
                    break;

                case nameof(StartAngleCountingPa):
                    if (StartAngleCountingPa.Nm < 0.5 || StartAngleCountingPa.Nm > 999)
                    {
                        return ProcessControlTechValidationError.StartAngleCountingPaNotBetween0Point5And999;
                    }
                    break;

                case nameof(AlarmTorquePa):
                    if (AlarmTorquePa.Nm < 0 || AlarmTorquePa.Nm > 9999 || AlarmTorquePa.Nm != 0 && 
                        (AlarmTorquePa.Nm < StartAngleCountingPa.Nm || AlarmTorquePa.Nm < StartMeasurementPa.Nm))
                    {
                        return ProcessControlTechValidationError.AlarmTorquePaNotBetween0And9999AndLessThanStartAngleCountingPaOrStartMeasurementPa;
                    }
                    break;

                case nameof(StartMeasurementPa):
                    if (StartMeasurementPa.Nm < 0.5 || StartMeasurementPa.Nm > 999 || StartMeasurementPa.Nm > StartAngleCountingPa.Nm)
                    {
                        return ProcessControlTechValidationError.StartMeasurementPaNotBetween0Point5And999OrGreaterThanStartAngleCountingPa;
                    }
                    break;

                case nameof(AngleForFurtherTurningPa):
                    if (AngleForFurtherTurningPa.Degree < 0 || AngleForFurtherTurningPa.Degree > 99)
                    {
                        return ProcessControlTechValidationError.AngleForFurtherTurningPaNotBetween0And99;
                    }
                    break;

                case nameof(TargetAnglePa):
                    if (TargetAnglePa.Degree < 0 || TargetAnglePa.Degree > 20)
                    {
                        return ProcessControlTechValidationError.TargetAnglePaNotBetween0And20;
                    }
                    break;

                case nameof(AlarmAnglePa):
                    if (AlarmAnglePa.Degree < 0 || AlarmAnglePa.Degree > 9999 || AlarmAnglePa.Degree != 0 && 
                        (AlarmAnglePa.Degree < AngleForFurtherTurningPa.Degree || AlarmAnglePa.Degree < TargetAnglePa.Degree))
                    {
                        return ProcessControlTechValidationError.AlarmAnglePaNotBetween0And9999OrLessThanAngleForFurtherTurningPaOrTargetAnglePa;
                    }
                    break;
                case nameof(AngleLimitMt):
                    if (AngleLimitMt.Degree < 1 || AngleLimitMt.Degree > 99)
                    {
                        return ProcessControlTechValidationError.AngleLimitMtNotBetween1And99;
                    }
                    break;
                case nameof(AlarmAngleMt):
                    if (AlarmAngleMt.Degree < 0 || AlarmAngleMt.Degree > 9999)
                    {
                        return ProcessControlTechValidationError.AlarmAngleMtNotBetween0And9999;
                    }
                    break;
                case nameof(StartMeasurementPeak):
                    if (StartMeasurementPeak.Nm < 0.5 || StartMeasurementPeak.Nm > 999)
                    {
                        return ProcessControlTechValidationError.StartMeasurementPeakNotBetween0Point5And999;
                    }
                    break;
            }

            return null;
        }
    }

    public enum ProcessControlTechValidationError
    {
        MinimumTorqueMtNotBetween0Point5And999,
        StartAngleMtNotBetween0Point5And999,
        StartMeasurementMtNotBetween0Point5And999AndGreaterThanStartAngleMt,
        AlarmTorqueMtNotBetween0And9999AndLessThanMinimumTorqueMtOrStartAngleMtOrStartMeasurementMt,
        StartAngleCountingPaNotBetween0Point5And999,
        AlarmTorquePaNotBetween0And9999AndLessThanStartAngleCountingPaOrStartMeasurementPa,
        StartMeasurementPaNotBetween0Point5And999OrGreaterThanStartAngleCountingPa,
        AngleForFurtherTurningPaNotBetween0And99,
        TargetAnglePaNotBetween0And20,
        AlarmAnglePaNotBetween0And9999OrLessThanAngleForFurtherTurningPaOrTargetAnglePa,
        AngleLimitMtNotBetween1And99,
        AlarmAngleMtNotBetween0And9999,
        StartMeasurementPeakNotBetween0Point5And999
    }
}
