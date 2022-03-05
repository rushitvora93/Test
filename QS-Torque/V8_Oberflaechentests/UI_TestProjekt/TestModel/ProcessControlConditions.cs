using System;

namespace UI_TestProjekt.TestModel
{
    public class ProcessControlConditions
    {
        private MeasurementPoint mp = new MeasurementPoint();
        private double lowerInterventionLimit;
        private double upperInterventionLimit;
        private double lowerMeasuringLimit;
        private double upperMeasuringLimit;
        private string testLevelSet;
        private int testLevelNumber;
        private DateTime startDate;
        private bool isAuditOperationActive;
        private string method = Methods.QSTPeak;

        private double qstMinMinimumTorque = 0.5;
        private double qstMinStartAngleCount = 0.5;
        private double qstMinAngleLimit = 1;
        private double qstMinStartMeasurement = 0.5;
        private double qstMinAlarmLimitTorque;
        private double qstMinAlarmLimitAngle;

        private double qstPeakStartMeasurement = 0.5;

        private double qstPrevailStartAngleCount = 0.5;
        private double qstPrevailAngleForPrevail;
        private double qstPrevailTargetAngle;
        private double qstPrevailStartMeasurement = 0.5;
        private double qstPrevailAlarmLimitTorque;
        private double qstPrevailAlarmLimitAngle;

        private string extension;
        private double extensionFactorAngle;
        private double extensionLengthGauge;

        public MeasurementPoint Mp { get => mp; set => mp = value; }
        public double LowerInterventionLimit { get => lowerInterventionLimit; set => lowerInterventionLimit = value; }
        public double UpperInterventionLimit { get => upperInterventionLimit; set => upperInterventionLimit = value; }
        public double LowerMeasuringLimit { get => lowerMeasuringLimit; set => lowerMeasuringLimit = value; }
        public double UpperMeasuringLimit { get => upperMeasuringLimit; set => upperMeasuringLimit = value; }
        public string TestLevelSet { get => testLevelSet; set => testLevelSet = value; }
        public int TestLevelNumber { get => testLevelNumber; set => testLevelNumber = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public bool IsAuditOperationActive { get => isAuditOperationActive; set => isAuditOperationActive = value; }
        public string Method { get => method; set => method = value; }
        public double QstMinMinimumTorque { get => qstMinMinimumTorque; set => qstMinMinimumTorque = value; }
        public double QstMinStartAngleCount { get => qstMinStartAngleCount; set => qstMinStartAngleCount = value; }
        public double QstMinAngleLimit { get => qstMinAngleLimit; set => qstMinAngleLimit = value; }
        public double QstMinStartMeasurement { get => qstMinStartMeasurement; set => qstMinStartMeasurement = value; }
        public double QstMinAlarmLimitTorque { get => qstMinAlarmLimitTorque; set => qstMinAlarmLimitTorque = value; }
        public double QstMinAlarmLimitAngle { get => qstMinAlarmLimitAngle; set => qstMinAlarmLimitAngle = value; }
        public double QstPeakStartMeasurement { get => qstPeakStartMeasurement; set => qstPeakStartMeasurement = value; }
        public double QstPrevailStartAngleCount { get => qstPrevailStartAngleCount; set => qstPrevailStartAngleCount = value; }
        public double QstPrevailAngleForPrevail { get => qstPrevailAngleForPrevail; set => qstPrevailAngleForPrevail = value; }
        public double QstPrevailTargetAngle { get => qstPrevailTargetAngle; set => qstPrevailTargetAngle = value; }
        public double QstPrevailStartMeasurement { get => qstPrevailStartMeasurement; set => qstPrevailStartMeasurement = value; }
        public double QstPrevailAlarmLimitTorque { get => qstPrevailAlarmLimitTorque; set => qstPrevailAlarmLimitTorque = value; }
        public double QstPrevailAlarmLimitAngle { get => qstPrevailAlarmLimitAngle; set => qstPrevailAlarmLimitAngle = value; }
        public string Extension { get => extension; set => extension = value; }
        public double ExtensionFactorAngle { get => extensionFactorAngle; set => extensionFactorAngle = value; }
        public double ExtensionLengthGauge { get => extensionLengthGauge; set => extensionLengthGauge = value; }

        public ProcessControlConditions() { }

        public static class Methods
        {
            public const string QSTMinTorque = "Minimum torque";
            public const string QSTPeak = "Peak";
            public const string QSTPrevail = "Prevail torque/angle";
        }

        public string GetInternalMethodString()
        {
            switch (method)
            {
                case Methods.QSTMinTorque:
                    return "QST_MT";
                case Methods.QSTPeak:
                    return "QST_PEAK";
                case Methods.QSTPrevail:
                    return "QST_PA";
                default:
                    return "Methode nicht implementiert";
            }
        }
    }
}