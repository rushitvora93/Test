using Client.Core.Entities;

namespace Client.Core.Validator
{
    public interface IProcessControlConditionValidator
    {
        bool Validate(ProcessControlCondition processControlCondition);
    }
    public class ProcessControlConditionValidator : IProcessControlConditionValidator
    {
        public bool Validate(ProcessControlCondition processControlCondition)
        {
            if (processControlCondition is null)
            {
                return true;
            }

            if (processControlCondition.Validate(nameof(ProcessControlCondition.LowerMeasuringLimit)) != null ||
                processControlCondition.Validate(nameof(ProcessControlCondition.UpperMeasuringLimit)) != null)
            {
                return false;
            }

            if (processControlCondition.Validate(nameof(ProcessControlCondition.LowerInterventionLimit)) != null ||
                processControlCondition.Validate(nameof(ProcessControlCondition.UpperInterventionLimit)) != null)
            {
                return false;
            }

            if (processControlCondition.ProcessControlTech == null)
            {
                return false;
            }

            if (processControlCondition.ProcessControlTech is QstProcessControlTech qstTech)
            {
                return ValidateQstProcessControlTech(qstTech);
            }
            return true;
        }

        private bool ValidateQstProcessControlTech(QstProcessControlTech qstTech)
        {
            if (qstTech.Validate(nameof(QstProcessControlTech.MinimumTorqueMt)) != null)
            {
                return false;
            }

            if (qstTech.Validate(nameof(QstProcessControlTech.StartAngleMt)) != null)
            {
                return false;
            }

            if (qstTech.Validate(nameof(QstProcessControlTech.StartMeasurementMt)) != null)
            {
                return false;
            }

            if (qstTech.Validate(nameof(QstProcessControlTech.AlarmTorqueMt)) != null)
            {
                return false;
            }

            if (qstTech.Validate(nameof(QstProcessControlTech.StartAngleCountingPa)) != null)
            {
                return false;
            }

            if (qstTech.Validate(nameof(QstProcessControlTech.StartMeasurementPa)) != null)
            {
                return false;
            }

            if (qstTech.Validate(nameof(QstProcessControlTech.AlarmTorquePa)) != null)
            {
                return false;
            }

            if (qstTech.Validate(nameof(QstProcessControlTech.AngleForFurtherTurningPa)) != null)
            {
                return false;
            }

            if (qstTech.Validate(nameof(QstProcessControlTech.TargetAnglePa)) != null)
            {
                return false;
            }

            if (qstTech.Validate(nameof(QstProcessControlTech.AlarmAnglePa)) != null)
            {
                return false;
            }

            if (qstTech.Validate(nameof(QstProcessControlTech.AngleLimitMt)) != null)
            {
                return false;
            }

            if (qstTech.Validate(nameof(QstProcessControlTech.AlarmAngleMt)) != null)
            {
                return false;
            }

            if (qstTech.Validate(nameof(QstProcessControlTech.StartMeasurementPeak)) != null)
            {
                return false;
            }

            return true;
        }
    }
}
