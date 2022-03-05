using System.Collections.Generic;
using System.Globalization;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using FrameworksAndDrivers.Localization;

namespace StartUp.AssistentCreator
{
    class TestParameterAssistentCreator
    {
        private TestParameters _defaultTestParameters;
        private LocalizationWrapper _localization;
        private UseCaseCollection _useCases;
        private StartUpImpl _startUp;


        public TestParameterAssistentCreator(LocalizationWrapper locaization, StartUpImpl startUp, UseCaseCollection useCases, TestParameters defaultTestParameters = null)
        {
            _localization = locaization;
            _defaultTestParameters = defaultTestParameters;
            _useCases = useCases;
            _startUp = startUp;
        }


        public (ParentAssistentPlan completeAssistantPlan, AssistentPlan<double> setpointPlan, ToleranceClassAssistentPlan toleranceClassPlan, AssistentPlan<double> minTorquePlan, AssistentPlan<double> maxTorquePlan)
            CreateControlledByTorqueAssistent(AssistentView assistentView)
        {
            var setpointTorquePlan = CreateSetPointTorqueAssistentPlan();
            var toleranceClassTorquePlan = CreateToleranceClassTorqueAssistentPlan(assistentView);
            var minTorquePlan = CreateMinimumTorqueAssistentPlan();
            var maxTorquePlan = CreateMaximumTorqueAssistentPlan();

            AddLocationAssistentCreator.WireMinMaxItems(minTorquePlan.AssistentItem,
                maxTorquePlan.AssistentItem,
                toleranceClassTorquePlan.AssistentItem,
                setpointTorquePlan.AssistentItem,
                true,
                LocationControlledBy.Torque,
                _localization);

            _useCases.toleranceClassGuiAdapter.RegisterGuiInterface(toleranceClassTorquePlan);

            return (new ParentAssistentPlan(new List<ParentAssistentPlan>()
                    {
                        setpointTorquePlan,
                        toleranceClassTorquePlan,
                        new ConditionalAssistentPlan(
                            new List<ParentAssistentPlan>()
                            {
                                minTorquePlan,
                                maxTorquePlan
                            },
                            () => toleranceClassTorquePlan.AssistentItem.EnteredValue?.LowerLimit == 0 && toleranceClassTorquePlan.AssistentItem.EnteredValue?.UpperLimit == 0)
                    }),
                    setpointTorquePlan,
                    toleranceClassTorquePlan,
                    minTorquePlan,
                    maxTorquePlan);
        }

        public ParentAssistentPlan CreateControlledByAngleAssistent(AssistentView assistentView)
        {
            var targetTorquePlan = CreateThresholdTorqueAssistentPlan();
            var setpointAnglePlan = CreateSetPointAngleAssistentPlan();
            var toleranceClassAnglePlan = CreateToleranceClassAngleAssistentPlan(assistentView);
            var minAnglePlan = CreateMinimumAngleAssistentPlan();
            var maxAnglePlan = CreateMaximumAngleAssistentPlan();

            AddLocationAssistentCreator.WireMinMaxItems(minAnglePlan.AssistentItem,
                maxAnglePlan.AssistentItem,
                toleranceClassAnglePlan.AssistentItem,
                setpointAnglePlan.AssistentItem,
                false,
                LocationControlledBy.Angle,
                _localization);

            targetTorquePlan.AssistentItem.ErrorCheck = assistentItem => (assistentItem as AssistentItemModel<double>).EnteredValue <= 0;
            targetTorquePlan.AssistentItem.ErrorText = _localization.Strings.GetParticularString("Test parameter assistent", "The threshold torque has to be greater than zero");
            targetTorquePlan.AssistentItem.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = s => double.Parse(s, CultureInfo.InvariantCulture) <= 0,
                WarningText = _localization.Strings.GetParticularString("Test parameter assistent", "The threshold torque has to be greater than zero")
            });

            _useCases.toleranceClassGuiAdapter.RegisterGuiInterface(toleranceClassAnglePlan);

            return new ParentAssistentPlan(new List<ParentAssistentPlan>()
            {
                targetTorquePlan,
                setpointAnglePlan,
                toleranceClassAnglePlan,
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>()
                    {
                        minAnglePlan,
                        maxAnglePlan
                    },
                    () => toleranceClassAnglePlan.AssistentItem.EnteredValue?.LowerLimit == 0 && toleranceClassAnglePlan.AssistentItem.EnteredValue?.UpperLimit == 0)
            });
        }


        #region Special AssistentPlanCreator

        private AssistentPlan<double> CreateSetPointTorqueAssistentPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Test parameter assistent", "Enter the setpoint for the torque"),
                    _localization.Strings.GetParticularString("Test parameter assistent", "Setpoint torque"),
                    _defaultTestParameters?.SetPointTorque.Nm ?? 0.0,
                    (o, i) => (o as LocationToolAssignment).TestParameters.SetPointTorque = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm")));
        }

        private ToleranceClassAssistentPlan CreateToleranceClassTorqueAssistentPlan(AssistentView assistentView)
        {
            return new ToleranceClassAssistentPlan(
                _useCases.toleranceClass,
                new ListAssistentItemModel<ToleranceClass>(
                    assistentView.Dispatcher,
                    _localization.Strings.GetParticularString("Test parameter assistent", "Choose a tolerance class for the torque"),
                    _localization.Strings.GetParticularString("Test parameter assistent", "Tolerance class torque"),
                    null,
                    (o, i) => (o as LocationToolAssignment).TestParameters.ToleranceClassTorque = (i as ListAssistentItemModel<ToleranceClass>).EnteredValue,
                    _localization.Strings.GetParticularString("Test parameter assistent", "Jump to tolerance class"),
                    x => x.Name,
                    () =>
                    {
                        _startUp.OpenToleranceClassDialog(assistentView);
                    }),
                _defaultTestParameters?.ToleranceClassTorque?.Id ?? null);
        }

        private AssistentPlan<double> CreateMinimumTorqueAssistentPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Test parameter assistent", "Enter a value for the minimum torque"),
                    _localization.Strings.GetParticularString("Test parameter assistent", "Minimum torque"),
                    _defaultTestParameters?.MinimumTorque.Nm ?? 0.0,
                    (o, i) => (o as LocationToolAssignment).TestParameters.MinimumTorque = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm")));
        }

        private AssistentPlan<double> CreateMaximumTorqueAssistentPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Test parameter assistent", "Enter a value for the maximum torque"),
                    _localization.Strings.GetParticularString("Test parameter assistent", "Maximum torque"),
                    _defaultTestParameters?.MaximumTorque.Nm ?? 0.0,
                    (o, i) => (o as LocationToolAssignment).TestParameters.MaximumTorque = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm")));
        }

        private AssistentPlan<double> CreateThresholdTorqueAssistentPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Test parameter assistent", "Enter the threshold for the torque"),
                    _localization.Strings.GetParticularString("Test parameter assistent", "Threshold torque"),
                    _defaultTestParameters?.ThresholdTorque.Degree ?? 0.0,
                    (o, i) => (o as LocationToolAssignment).TestParameters.ThresholdTorque = Angle.FromDegree((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm")));
        }

        private AssistentPlan<double> CreateSetPointAngleAssistentPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Test parameter assistent", "Enter the setpoint for the angle"),
                    _localization.Strings.GetParticularString("Test parameter assistent", "Setpoint angle"),
                    _defaultTestParameters?.SetPointAngle.Degree ?? 0.0,
                    (o, i) => (o as LocationToolAssignment).TestParameters.SetPointAngle = Angle.FromDegree((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "°")));
        }

        private ToleranceClassAssistentPlan CreateToleranceClassAngleAssistentPlan(AssistentView assistentView)
        {
            return new ToleranceClassAssistentPlan(
                _useCases.toleranceClass,
                new ListAssistentItemModel<ToleranceClass>(
                    assistentView.Dispatcher,
                    _localization.Strings.GetParticularString("Test parameter assistent", "Choose a tolerance class for the angle"),
                    _localization.Strings.GetParticularString("Test parameter assistent", "Tolerance class angle"),
                    null,
                    (o, i) => (o as LocationToolAssignment).TestParameters.ToleranceClassAngle = (i as ListAssistentItemModel<ToleranceClass>).EnteredValue,
                    _localization.Strings.GetParticularString("Test parameter assistent", "Jump to tolerance class"),
                    x => x.Name,
                    () =>
                    {
                        _startUp.OpenToleranceClassDialog(assistentView);
                    }),
                _defaultTestParameters?.ToleranceClassAngle?.Id ?? null);
        }

        private AssistentPlan<double> CreateMinimumAngleAssistentPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Test parameter assistent", "Enter a value for the minimum angle"),
                    _localization.Strings.GetParticularString("Test parameter assistent", "Minimum angle"),
                    _defaultTestParameters?.MinimumAngle.Degree ?? 0.0,
                    (o, i) => (o as LocationToolAssignment).TestParameters.MinimumAngle = Angle.FromDegree((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "°")));
        }

        private AssistentPlan<double> CreateMaximumAngleAssistentPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Test parameter assistent", "Enter a value for the maximum angle"),
                    _localization.Strings.GetParticularString("Test parameter assistent", "Maximum angle"),
                    _defaultTestParameters?.MaximumAngle.Degree ?? 0.0,
                    (o, i) => (o as LocationToolAssignment).TestParameters.MaximumAngle = Angle.FromDegree((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "°")));
        }

        #endregion
    }
}
