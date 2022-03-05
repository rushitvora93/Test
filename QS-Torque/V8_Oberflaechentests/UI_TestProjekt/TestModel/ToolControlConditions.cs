using System;

namespace UI_TestProjekt.TestModel
{
    public class ToolControlConditions
    {
        private ControlledBy controlledBy = ControlledBy.Torque;
        private double setPointTorque = 0;
        private ToleranceClass toleranceClassTorque;
        private double minTorque = 3;
        private double maxTorque = 5;

        private double thresholdTorque = 2.5;
        private double setPointAngle = 15;
        private ToleranceClass toleranceClassAngle = new ToleranceClass();
        private double minAngle = 10;
        private double maxAngle = 20;

        private TestLevelSet testLevelSetChk;
        private int testLevelSetChkNumber = 1;
        private DateTime startDateChk = DateTime.Now;
        private bool isActiveChk;

        private TestLevelSet testLevelSetMca;
        private int testLevelSetMcaNumber = 1;
        private DateTime startDateMca = DateTime.Now;
        private bool isActiveMca;

        private double powEndCycleTime = 0.4;
        private double powFilterFrequency = 100;
        private double powCycleComplete = 0;
        private double powMeasureDelayTime = 0;
        private double powResetTime = 0;
        private bool powMustTorqueAndAngleBeBeetweenLimits = false;
        private double powCycleStart = 0;
        private double powStartFinalAngle = 0;

        private double clickWrenchEndCycleTime;
        private double clickWrenchFilterFrequency;
        private double clickWrenchCycleComplete;
        private double clickWrenchMeasureDelayTime;
        private double clickWrenchResetTime;
        private double clickWrenchCycleStart;
        private double clickWrenchSlipTorque;

        private double peakEndCycleTime;
        private double peakFilterFrequency; 
        private bool peakMustTorqueAndAngleBeBetweenLimits; 
        private double peakCycleStart;
        private double peakStartFinalAngle;

        private double pulseDriverEndCycleTime;
        private double pulseDriverFilterFrequency;
        private double pulseDriverTorqueCoefficient;
        private int pulseDriverMinimumPulse;
        private int pulseDriverMaximumPulse;
        private int pulseDriverThreshold;

        public ControlledBy ControlledBy { get => controlledBy; set => controlledBy = value; }
        public double SetPointTorque { get => setPointTorque; set => setPointTorque = value; }
        public ToleranceClass ToleranceClassTorque { get => toleranceClassTorque; set => toleranceClassTorque = value; }
        public double MinTorque { get => minTorque; set => minTorque = value; }
        public double MaxTorque { get => maxTorque; set => maxTorque = value; }
        public double ThresholdTorque { get => thresholdTorque; set => thresholdTorque = value; }
        public double SetPointAngle { get => setPointAngle; set => setPointAngle = value; }
        public ToleranceClass ToleranceClassAngle { get => toleranceClassAngle; set => toleranceClassAngle = value; }
        public double MinAngle { get => minAngle; set => minAngle = value; }
        public double MaxAngle { get => maxAngle; set => maxAngle = value; }
        public TestLevelSet TestLevelSetChk { get => testLevelSetChk; set => testLevelSetChk = value; }
        public int TestLevelSetChkNumber { get => testLevelSetChkNumber; set => testLevelSetChkNumber = value; }
        public DateTime StartDateChk { get => startDateChk; set => startDateChk = value; }
        public bool IsActiveChk { get => isActiveChk; set => isActiveChk = value; }
        public TestLevelSet TestLevelSetMca { get => testLevelSetMca; set => testLevelSetMca = value; }
        public int TestLevelSetMcaNumber { get => testLevelSetMcaNumber; set => testLevelSetMcaNumber = value; }
        public DateTime StartDateMca { get => startDateMca; set => startDateMca = value; }
        public bool IsActiveMca { get => isActiveMca; set => isActiveMca = value; }
        public double PowEndCycleTime { get => powEndCycleTime; set => powEndCycleTime = value; }
        public double PowFilterFrequency { get => powFilterFrequency; set => powFilterFrequency = value; }
        public double PowCycleComplete { get => powCycleComplete; set => powCycleComplete = value; }
        public double PowMeasureDelayTime { get => powMeasureDelayTime; set => powMeasureDelayTime = value; }
        public double PowResetTime { get => powResetTime; set => powResetTime = value; }
        public bool PowMustTorqueAndAngleBeBeetweenLimits { get => powMustTorqueAndAngleBeBeetweenLimits; set => powMustTorqueAndAngleBeBeetweenLimits = value; }
        public double PowCycleStart { get => powCycleStart; set => powCycleStart = value; }
        public double PowStartFinalAngle { get => powStartFinalAngle; set => powStartFinalAngle = value; }
        public double ClickWrenchEndCycleTime { get => clickWrenchEndCycleTime; set => clickWrenchEndCycleTime = value; }
        public double ClickWrenchFilterFrequency { get => clickWrenchFilterFrequency; set => clickWrenchFilterFrequency = value; }
        public double ClickWrenchCycleComplete { get => clickWrenchCycleComplete; set => clickWrenchCycleComplete = value; }
        public double ClickWrenchMeasureDelayTime { get => clickWrenchMeasureDelayTime; set => clickWrenchMeasureDelayTime = value; }
        public double ClickWrenchResetTime { get => clickWrenchResetTime; set => clickWrenchResetTime = value; }
        public double ClickWrenchCycleStart { get => clickWrenchCycleStart; set => clickWrenchCycleStart = value; }
        public double ClickWrenchSlipTorque { get => clickWrenchSlipTorque; set => clickWrenchSlipTorque = value; }
        public double PeakEndCycleTime { get => peakEndCycleTime; set => peakEndCycleTime = value; }
        public double PeakFilterFrequency { get => peakFilterFrequency; set => peakFilterFrequency = value; }
        public bool PeakMustTorqueAndAngleBeBetweenLimits { get => peakMustTorqueAndAngleBeBetweenLimits; set => peakMustTorqueAndAngleBeBetweenLimits = value; }
        public double PeakCycleStart { get => peakCycleStart; set => peakCycleStart = value; }
        public double PeakStartFinalAngle { get => peakStartFinalAngle; set => peakStartFinalAngle = value; }
        public double PulseDriverEndCycleTime { get => pulseDriverEndCycleTime; set => pulseDriverEndCycleTime = value; }
        public double PulseDriverFilterFrequency { get => pulseDriverFilterFrequency; set => pulseDriverFilterFrequency = value; }
        public double PulseDriverTorqueCoefficient { get => pulseDriverTorqueCoefficient; set => pulseDriverTorqueCoefficient = value; }
        public int PulseDriverMinimumPulse { get => pulseDriverMinimumPulse; set => pulseDriverMinimumPulse = value; }
        public int PulseDriverMaximumPulse { get => pulseDriverMaximumPulse; set => pulseDriverMaximumPulse = value; }
        public int PulseDriverThreshold { get => pulseDriverThreshold; set => pulseDriverThreshold = value; }

        public ToolControlConditions() { }
    }
}
