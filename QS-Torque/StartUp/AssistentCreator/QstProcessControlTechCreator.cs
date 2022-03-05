using System.Collections.Generic;
using System.Globalization;
using Client.Core.Entities;
using Common.Types.Enums;
using Core.PhysicalValueTypes;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using InterfaceAdapters.Localization;
using Microsoft.Xaml.Behaviors;

namespace StartUp.AssistentCreator
{
    public class QstProcessControlTechCreator
    {
        private ILocalizationWrapper _localization;


        public QstProcessControlTechCreator(ILocalizationWrapper locaization)
        {
            _localization = locaization;
        }

        public ParentAssistentPlan CreateQstProcessControlTechPlan(AssistentView assistentView)
        {
            var testMethodPlan = CreateTestMethodAssistantPlan(assistentView);

            var minimumTorqueMtPlan = CreateMinimumTorqueMt();
            var startAngleMtPlan = CreateStartAngleMt();
            var startMeasurementMtPlan = CreateStartMeasurementMt();
            var createAlarmTorqueMtPlan = CreateAlarmTorqueMt();
            WireAlarmTorqueMtItems(createAlarmTorqueMtPlan.AssistentItem, minimumTorqueMtPlan.AssistentItem, startAngleMtPlan.AssistentItem, startMeasurementMtPlan.AssistentItem);
            WireStartMeasurementMtItems(startMeasurementMtPlan.AssistentItem, startAngleMtPlan.AssistentItem);


            var startAngleCountingPaPlan = CreateStartAngleCountingPa();
            var alarmTorquePaPlan = CreateAlarmTorquePa();
            var startMeasurementPa = CreateStartMeasurementPa();

            WireAlarmTorquePaItems(alarmTorquePaPlan.AssistentItem, startAngleCountingPaPlan.AssistentItem, startMeasurementPa.AssistentItem);
            WireStartMeasurementMtItems(startMeasurementPa.AssistentItem, startAngleCountingPaPlan.AssistentItem);

            var angleForFurtherTurningPa = CreateAngleForFurtherTurningPa();
            var targetAnglePa = CreateTargetAnglePa();
            var alarmAnglePa = CreateAlarmAnglePa();

            WireAlarmAnglePaItems(alarmAnglePa.AssistentItem, angleForFurtherTurningPa.AssistentItem, targetAnglePa.AssistentItem);


            return new ParentAssistentPlan(new List<ParentAssistentPlan>()
            {
                testMethodPlan,
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>()
                    {
                        minimumTorqueMtPlan,
                        startAngleMtPlan,
                        CreateAngleLimitMt(),
                        startMeasurementMtPlan,
                        createAlarmTorqueMtPlan,
                        CreateAlarmAngleMt()
                    },
                    () => testMethodPlan?.AssistentItem?.EnteredValue  == TestMethod.QST_MT),

                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>()
                    {
                        CreateStartMeasurementPeak()
                    },
                    () => testMethodPlan?.AssistentItem?.EnteredValue  == TestMethod.QST_PEAK),

                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>()
                    {
                        startAngleCountingPaPlan,
                        angleForFurtherTurningPa,
                        targetAnglePa,
                        startMeasurementPa,
                        alarmTorquePaPlan,
                        alarmAnglePa
                    },
                    () => testMethodPlan?.AssistentItem?.EnteredValue  == TestMethod.QST_PA)
            });
        }

        private void WireStartMeasurementMtItems(AssistentItemModel<double> startMeasurementMtPlan, AssistentItemModel<double> startAngleMtPlan)
        {
            var minimumErrorText = _localization.Strings.GetParticularString("Add process control condition assistant",
                "The start measurement has to be between 0.5 and 999 and less than or equal to start angle count");

            startMeasurementMtPlan.ErrorCheck = x => (x as AssistentItemModel<double>).EnteredValue < 0.5 ||
                                                     (x as AssistentItemModel<double>).EnteredValue > 999 ||
                                                     (x as AssistentItemModel<double>).EnteredValue > startAngleMtPlan.EnteredValue;

            startMeasurementMtPlan.ErrorText = minimumErrorText;
            startMeasurementMtPlan.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = x => double.Parse(x, CultureInfo.InvariantCulture) < 0.5 ||
                                 double.Parse(x, CultureInfo.InvariantCulture) > 999 ||
                                 double.Parse(x, CultureInfo.InvariantCulture) > startAngleMtPlan.EnteredValue,
                                 
                WarningText = minimumErrorText
            });
        }



        private void WireAlarmTorqueMtItems(AssistentItemModel<double> alarmTorqueMt, AssistentItemModel<double> minimumTorqueMt, AssistentItemModel<double> startAngleMtPlan, AssistentItemModel<double> startMeasurementMtPlan)
        {
            var minimumErrorText = _localization.Strings.GetParticularString("Add process control condition assistant",
                "The alarm torque has to be between 0 and 9999 and greater than or equal to minimum torque, start angle count and start measurement");

            alarmTorqueMt.ErrorCheck = x => (x as AssistentItemModel<double>).EnteredValue < 0 || 
                                            (x as AssistentItemModel<double>).EnteredValue > 9999 ||
                                            (x as AssistentItemModel<double>).EnteredValue != 0 && 
                                            (
                                                (x as AssistentItemModel<double>).EnteredValue < minimumTorqueMt.EnteredValue || 
                                                (x as AssistentItemModel<double>).EnteredValue < startAngleMtPlan.EnteredValue || 
                                                (x as AssistentItemModel<double>).EnteredValue < startMeasurementMtPlan.EnteredValue
                                            );

            alarmTorqueMt.ErrorText = minimumErrorText;
            alarmTorqueMt.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = x => double.Parse(x, CultureInfo.InvariantCulture) < 0 ||
                                 double.Parse(x, CultureInfo.InvariantCulture) > 9999 ||
                                 double.Parse(x, CultureInfo.InvariantCulture) != 0.0 &&
                                 (
                                     double.Parse(x, CultureInfo.InvariantCulture) < minimumTorqueMt.EnteredValue ||
                                     double.Parse(x, CultureInfo.InvariantCulture) < startAngleMtPlan.EnteredValue ||
                                     double.Parse(x, CultureInfo.InvariantCulture) < startMeasurementMtPlan.EnteredValue
                                 ),
                WarningText = minimumErrorText
            });
        }

        private void WireAlarmTorquePaItems(AssistentItemModel<double> alarmTorquePa, AssistentItemModel<double> startAngleCountingPaPlan, AssistentItemModel<double> startMeasurementPaPlan)
        {
            var minimumErrorText = _localization.Strings.GetParticularString("Add process control condition assistant",
                "The alarm torque has to be between 0 and 9999 and greater than or equal to start angle count and start measurement");

            alarmTorquePa.ErrorCheck = x => (x as AssistentItemModel<double>).EnteredValue < 0 ||
                                            (x as AssistentItemModel<double>).EnteredValue > 9999 ||
                                            (x as AssistentItemModel<double>).EnteredValue != 0 &&
                                            (
                                                (x as AssistentItemModel<double>).EnteredValue < startAngleCountingPaPlan.EnteredValue ||
                                                (x as AssistentItemModel<double>).EnteredValue < startMeasurementPaPlan.EnteredValue 
                                            );

            alarmTorquePa.ErrorText = minimumErrorText;
            alarmTorquePa.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = x => double.Parse(x, CultureInfo.InvariantCulture) < 0 ||
                                 double.Parse(x, CultureInfo.InvariantCulture) > 9999 ||
                                 double.Parse(x, CultureInfo.InvariantCulture) != 0.0 &&
                                 (
                                     double.Parse(x, CultureInfo.InvariantCulture) < startAngleCountingPaPlan.EnteredValue ||
                                     double.Parse(x, CultureInfo.InvariantCulture) < startMeasurementPaPlan.EnteredValue
                                 ),
                WarningText = minimumErrorText
            });
        }

        private void WireAlarmAnglePaItems(AssistentItemModel<double> alarmAnglePa, AssistentItemModel<double> angleForFurtherTurningPa, AssistentItemModel<double> targetAnglePa)
        {
            var minimumErrorText = _localization.Strings.GetParticularString("Add process control condition assistant",
                "The alarm angle has to be between 0 and 9999 and greater than or equal to angle for further turning and target angle");

            alarmAnglePa.ErrorCheck = x => (x as AssistentItemModel<double>).EnteredValue < 0 ||
                                            (x as AssistentItemModel<double>).EnteredValue > 9999 ||
                                            (x as AssistentItemModel<double>).EnteredValue != 0 &&
                                            (
                                                (x as AssistentItemModel<double>).EnteredValue < angleForFurtherTurningPa.EnteredValue ||
                                                (x as AssistentItemModel<double>).EnteredValue < targetAnglePa.EnteredValue
                                            );

            alarmAnglePa.ErrorText = minimumErrorText;
            alarmAnglePa.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = x => double.Parse(x, CultureInfo.InvariantCulture) < 0 ||
                                 double.Parse(x, CultureInfo.InvariantCulture) > 9999 ||
                                 double.Parse(x, CultureInfo.InvariantCulture) != 0.0 &&
                                 (
                                     double.Parse(x, CultureInfo.InvariantCulture) < angleForFurtherTurningPa.EnteredValue ||
                                     double.Parse(x, CultureInfo.InvariantCulture) < targetAnglePa.EnteredValue
                                 ),
                WarningText = minimumErrorText
            });
        }


        private ListAssistentPlan<TestMethod> CreateTestMethodAssistantPlan(AssistentView assistentView)
        {
            return new ListAssistentPlan<TestMethod>(
                new ListAssistentItemModel<TestMethod>(
                    assistentView.Dispatcher,
                    new List<TestMethod>() { TestMethod.QST_MT, TestMethod.QST_PEAK, TestMethod.QST_PA },
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Choose test method for audit technique"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Test method"),
                    TestMethod.QST_MT,
                    (o, i) => (o as ProcessControlCondition).ProcessControlTech.TestMethod = (i as ListAssistentItemModel<TestMethod>).EnteredValue,
                    null,
                    x =>
                    {
                        switch (x)
                        {
                            case TestMethod.QST_MT: return _localization.Strings.GetParticularString("ProcessControl test method", "Minimum torque");
                            case TestMethod.QST_PA: return _localization.Strings.GetParticularString("ProcessControl test method", "Prevail torque/angle");
                            case TestMethod.QST_PEAK: return _localization.Strings.GetParticularString("ProcessControl test method", "Peak");
                            default: return "";
                        }
                    },
                    () => { }));
        }

        private AssistentPlan<double> CreateMinimumTorqueMt()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Enter a value for minimum torque (Mmin)"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Minimum torque"),
                    0.5,
                    (o, i) => ((o as ProcessControlCondition).ProcessControlTech as QstProcessControlTech).MinimumTorqueMt = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm"),
                    errorCheck: x => (x as AssistentItemModel<double>).EnteredValue < 0.5 || (x as AssistentItemModel<double>).EnteredValue > 999,
                    errorText: _localization.Strings.GetParticularString("Add process control condition assistant", "The minimum torque has to be between 0.5 and 999"),
                    behaviors: new List<Behavior>()
                    {
                        new ConditionValidationBehavior()
                        {
                            Condition = x => double.Parse(x,  CultureInfo.InvariantCulture) < 0.5 || double.Parse(x,  CultureInfo.InvariantCulture) > 999,
                            WarningText = _localization.Strings.GetParticularString("Add process control condition assistant", "The minimum torque has to be between 0.5 and 999")
                        }
                    }));
        }

        private AssistentPlan<double> CreateStartAngleMt()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Enter a value for start angle count (Ms)"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Start angle count"),
                    0.5,
                    (o, i) => ((o as ProcessControlCondition).ProcessControlTech as QstProcessControlTech).StartAngleMt = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm"),
                    errorCheck: x => (x as AssistentItemModel<double>).EnteredValue < 0.5 || (x as AssistentItemModel<double>).EnteredValue > 999,
                    errorText: _localization.Strings.GetParticularString("Add process control condition assistant", "The start angle count has to be between 0.5 and 999"),
                    behaviors: new List<Behavior>()
                    {
                        new ConditionValidationBehavior()
                        {
                            Condition = x => double.Parse(x,  CultureInfo.InvariantCulture) < 0.5 || double.Parse(x,  CultureInfo.InvariantCulture) > 999,
                            WarningText = _localization.Strings.GetParticularString("Add process control condition assistant", "The start angle count has to be between 0.5 and 999")
                        }
                    }));
        }

        private AssistentPlan<long> CreateAngleLimitMt()
        {
            return new AssistentPlan<long>(
                new AssistentItemModel<long>(
                    AssistentItemType.Numeric,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Enter a value for angle limit (Alim)"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Angle limit"),
                    1,
                    (o, i) => ((o as ProcessControlCondition).ProcessControlTech as QstProcessControlTech).AngleLimitMt = Angle.FromDegree((i as AssistentItemModel<long>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "°"),
                    errorCheck: x => (x as AssistentItemModel<long>).EnteredValue < 1 || (x as AssistentItemModel<long>).EnteredValue > 99,
                    errorText: _localization.Strings.GetParticularString("Add process control condition assistant", "The alarm limit angle has to be between 1 and 99"),
                    behaviors: new List<Behavior>()
                    {
                        new ConditionValidationBehavior()
                        {
                            Condition = x => long.Parse(x, NumberStyles.Number, CultureInfo.InvariantCulture) < 1 || long.Parse(x, NumberStyles.Number, CultureInfo.InvariantCulture) > 99,
                            WarningText = _localization.Strings.GetParticularString("Add process control condition assistant", "The alarm limit angle has to be between 1 and 99")
                        }
                    }));
        }

        private AssistentPlan<double> CreateStartMeasurementMt()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Enter a value for start measurement (Mstart)"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Start measurement"),
                    0.5,
                    (o, i) => ((o as ProcessControlCondition).ProcessControlTech as QstProcessControlTech).StartMeasurementMt = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm")));
        }

        private AssistentPlan<double> CreateAlarmTorqueMt()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Enter a value for alarm limit torque"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Alarm limit - torque"),
                    0.0,
                    (o, i) => ((o as ProcessControlCondition).ProcessControlTech as QstProcessControlTech).AlarmTorqueMt = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm")));
        }

        private AssistentPlan<double> CreateAlarmAngleMt()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Enter a value for alarm limit angle"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Alarm limit - angle"),
                    0.0,
                    (o, i) => ((o as ProcessControlCondition).ProcessControlTech as QstProcessControlTech).AlarmAngleMt = Angle.FromDegree((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "°"),
                    errorCheck: x => (x as AssistentItemModel<double>).EnteredValue < 0 || (x as AssistentItemModel<double>).EnteredValue > 9999,
                    errorText: _localization.Strings.GetParticularString("Add process control condition assistant", "The alarm limit angle has to be between 0 and 9999"),
                    behaviors: new List<Behavior>()
                    {
                        new ConditionValidationBehavior()
                        {
                            Condition = x => double.Parse(x,  CultureInfo.InvariantCulture) < 0 || double.Parse(x,  CultureInfo.InvariantCulture) > 9999,
                            WarningText = _localization.Strings.GetParticularString("Add process control condition assistant", "The alarm limit angle has to be between 0 and 9999")
                        }
                    }));
        }

        private AssistentPlan<double> CreateStartMeasurementPeak()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Enter a value for start measurement (Mstart)"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Start measurement"),
                    0.5,
                    (o, i) => ((o as ProcessControlCondition).ProcessControlTech as QstProcessControlTech).StartMeasurementPeak = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm"),
                    errorCheck: x => (x as AssistentItemModel<double>).EnteredValue < 0.5 || (x as AssistentItemModel<double>).EnteredValue > 999,
                    errorText: _localization.Strings.GetParticularString("Add process control condition assistant", "The start measurement has to be greater than 0.5 and less or equal to 999"),
                    behaviors: new List<Behavior>()
                    {
                        new ConditionValidationBehavior()
                        {
                            Condition = x => double.Parse(x,  CultureInfo.InvariantCulture) < 0.5 || double.Parse(x,  CultureInfo.InvariantCulture) > 999,
                            WarningText = _localization.Strings.GetParticularString("Add process control condition assistant", "The start measurement has to be greater than 0.5 and less or equal to 999")
                        }
                    }));
        }

        private AssistentPlan<double> CreateStartAngleCountingPa()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Enter a value for start angle count (Ms)"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Start angle count"),
                    0.5,
                    (o, i) => ((o as ProcessControlCondition).ProcessControlTech as QstProcessControlTech).StartAngleCountingPa = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm"),
                    errorCheck: x => (x as AssistentItemModel<double>).EnteredValue < 0.5 || (x as AssistentItemModel<double>).EnteredValue > 999,
                    errorText: _localization.Strings.GetParticularString("Add process control condition assistant", "The start angle count has to be between 0.5 and 999"),
                    behaviors: new List<Behavior>()
                    {
                        new ConditionValidationBehavior()
                        {
                            Condition = x => double.Parse(x,  CultureInfo.InvariantCulture) < 0.5 || double.Parse(x,  CultureInfo.InvariantCulture) > 999,
                            WarningText = _localization.Strings.GetParticularString("Add process control condition assistant", "The start angle count has to be between 0.5 and 999")
                        }
                    }));
            ;
        }

        private AssistentPlan<double> CreateAngleForFurtherTurningPa()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Enter a value for angle for further turning (A1)"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Angle for further turning"),
                    0.0,
                    (o, i) => ((o as ProcessControlCondition).ProcessControlTech as QstProcessControlTech).AngleForFurtherTurningPa = Angle.FromDegree((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "°"),
                    errorCheck: x => (x as AssistentItemModel<double>).EnteredValue < 0 || (x as AssistentItemModel<double>).EnteredValue > 99,
                    errorText: _localization.Strings.GetParticularString("Add process control condition assistant", "The angle for further turning has to be between 0 and 99"),
                    behaviors: new List<Behavior>()
                    {
                        new ConditionValidationBehavior()
                        {
                            Condition = x => double.Parse(x,  CultureInfo.InvariantCulture) < 0 || double.Parse(x,  CultureInfo.InvariantCulture) > 99,
                            WarningText = _localization.Strings.GetParticularString("Add process control condition assistant", "The angle for further turning has to be between 0 and 99")
                        }
                    }));
        }

        private AssistentPlan<double> CreateTargetAnglePa()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Enter a value for target value (A2)"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Target angle"),
                    0.0,
                    (o, i) => ((o as ProcessControlCondition).ProcessControlTech as QstProcessControlTech).TargetAnglePa = Angle.FromDegree((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "°"),
                    errorCheck: x => (x as AssistentItemModel<double>).EnteredValue < 0 || (x as AssistentItemModel<double>).EnteredValue > 20,
                    errorText: _localization.Strings.GetParticularString("Add process control condition assistant", "The target angle has to be between 0 and 20"),
                    behaviors: new List<Behavior>()
                    {
                        new ConditionValidationBehavior()
                        {
                            Condition = x => double.Parse(x,  CultureInfo.InvariantCulture) < 0 || double.Parse(x,  CultureInfo.InvariantCulture) > 20,
                            WarningText = _localization.Strings.GetParticularString("Add process control condition assistant", "The target angle has to be between 0 and 20")
                        }
                    }));
        }

        private AssistentPlan<double> CreateStartMeasurementPa()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add process control condition assistant",
                        "Enter a value for start measurement (Mstart)"),
                    _localization.Strings.GetParticularString("Add process control condition assistant",
                        "Start measurement"),
                    0.5,
                    (o, i) => ((o as ProcessControlCondition).ProcessControlTech as QstProcessControlTech)
                        .StartMeasurementPa = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm")));
        }

        private AssistentPlan<double> CreateAlarmTorquePa()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Enter a value for alarm limit torque"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Alarm limit - torque"),
                    0.0,
                    (o, i) => ((o as ProcessControlCondition).ProcessControlTech as QstProcessControlTech).AlarmTorquePa = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm")));
        }

        private AssistentPlan<double> CreateAlarmAnglePa()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Enter a value for alarm limit angle"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Alarm limit - angle"),
                    0.0,
                    (o, i) => ((o as ProcessControlCondition).ProcessControlTech as QstProcessControlTech).AlarmAnglePa = Angle.FromDegree((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "°")));
        }
    }
}
