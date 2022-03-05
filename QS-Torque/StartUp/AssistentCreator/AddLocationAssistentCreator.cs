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
using Microsoft.Xaml.Behaviors;

namespace StartUp.AssistentCreator
{
    class AddLocationAssistentCreator
    {
        Location _defaultLocation;
        StartUpImpl _startUp;
        LocalizationWrapper _localization;
        UseCaseCollection _useCases;


        public AssistentView CreateAddLocationAssistent()
        {
            AssistentView assistentView = new AssistentView(_localization.Strings.GetParticularString("AddLocationAssistent", "Add new location"));

            var controlledByPlan = CreateControlledByAssistentPlan(assistentView);
            var setpointTorqueControlledByTorquePlan = CreateSetPointTorqueAssistentPlan();
            var setpointAngleControlledByTorquePlan = CreateSetPointAngleAssistentPlan();
            var setpointAngleControlledByAnglePlan = CreateSetPointAngleAssistentPlan();
            var setpointTorqueControlledByAnglePlan = CreateSetPointTorqueAssistentPlan();
            var thresholdTorqueControlledByTorquePlan = CreateThresholdTorqueControlledByTorqueAssistentPlan();
            var toleranceClassTorqueControlledByTorquePlan = CreateToleranceClassTorqueAssistentPlan(assistentView);
            var toleranceClassAngleControlledByTorquePlan = CreateToleranceClassAngleAssistentPlan(assistentView);
            var toleranceClassTorqueControlledByAnglePlan = CreateToleranceClassTorqueAssistentPlan(assistentView);
            var toleranceClassAngleControlledByAnglePlan = CreateToleranceClassAngleAssistentPlan(assistentView);
            var minTorqueControlledByTorquePlan = CreateMinimumTorqueAssistentPlan();
            var maxTorqueControlledByTorquePlan = CreateMaximumTorqueAssistentPlan();
            var minAngleControlledByTorquePlan = CreateMinimumAngleAssistentPlan();
            var maxAngleControlledByTorquePlan = CreateMaximumAngleAssistentPlan();
            var minTorqueControlledByAnglePlan = CreateMinimumTorqueAssistentPlan();
            var maxTorqueControlledByAnglePlan = CreateMaximumTorqueAssistentPlan();
            var minAngleControlledByAnglePlan = CreateMinimumAngleAssistentPlan();
            var maxAngleControlledByAnglePlan = CreateMaximumAngleAssistentPlan();

            WireThresholdTorqueItemToSetPoint(thresholdTorqueControlledByTorquePlan.AssistentItem, setpointTorqueControlledByTorquePlan.AssistentItem);
            WireMinMaxItems(minTorqueControlledByTorquePlan.AssistentItem,
                maxTorqueControlledByTorquePlan.AssistentItem,
                toleranceClassTorqueControlledByTorquePlan.AssistentItem,
                setpointTorqueControlledByTorquePlan.AssistentItem,
                true,
                LocationControlledBy.Torque,
                _localization);
            WireMinMaxItems(minAngleControlledByTorquePlan.AssistentItem,
                maxAngleControlledByTorquePlan.AssistentItem,
                toleranceClassAngleControlledByTorquePlan.AssistentItem,
                setpointAngleControlledByTorquePlan.AssistentItem,
                false,
                LocationControlledBy.Torque,
                _localization);
            WireMinMaxItems(minTorqueControlledByAnglePlan.AssistentItem,
                maxTorqueControlledByAnglePlan.AssistentItem,
                toleranceClassTorqueControlledByAnglePlan.AssistentItem,
                setpointTorqueControlledByAnglePlan.AssistentItem,
                true,
                LocationControlledBy.Angle,
                _localization);
            WireMinMaxItems(minAngleControlledByAnglePlan.AssistentItem,
                maxAngleControlledByAnglePlan.AssistentItem,
                toleranceClassAngleControlledByAnglePlan.AssistentItem,
                setpointAngleControlledByAnglePlan.AssistentItem,
                false,
                LocationControlledBy.Angle,
                _localization);

            var parentPlan = new ParentAssistentPlan(new List<ParentAssistentPlan>()
            {
                CreateNumberAssistentPlan(),
                CreateDescriptionAssistentPlan(),
                controlledByPlan,

                // Controlled by torque branch
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>()
                    {
                        setpointTorqueControlledByTorquePlan,
                        toleranceClassTorqueControlledByTorquePlan,
                        new ConditionalAssistentPlan(
                            new List<ParentAssistentPlan>()
                            {
                                minTorqueControlledByTorquePlan,
                                maxTorqueControlledByTorquePlan
                            },
                            () => toleranceClassTorqueControlledByTorquePlan.AssistentItem.EnteredValue?.LowerLimit == 0 && toleranceClassTorqueControlledByTorquePlan.AssistentItem.EnteredValue?.UpperLimit == 0),
                        thresholdTorqueControlledByTorquePlan,
                        setpointAngleControlledByTorquePlan,
                        toleranceClassAngleControlledByTorquePlan,
                        new ConditionalAssistentPlan(
                            new List<ParentAssistentPlan>()
                            {
                                minAngleControlledByTorquePlan,
                                maxAngleControlledByTorquePlan
                            },
                            () => toleranceClassAngleControlledByTorquePlan.AssistentItem.EnteredValue?.LowerLimit == 0 && toleranceClassAngleControlledByTorquePlan.AssistentItem.EnteredValue?.UpperLimit == 0)
                    },
                    () => controlledByPlan.AssistentItem.EnteredValue == LocationControlledBy.Torque),
                
                // Controlled by angle branch
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>()
                    {
                        CreateThresholdTorqueControlledByAngleAssistentPlan(),
                        setpointAngleControlledByAnglePlan,
                        toleranceClassAngleControlledByAnglePlan,
                        new ConditionalAssistentPlan(
                            new List<ParentAssistentPlan>()
                            {
                                minAngleControlledByAnglePlan,
                                maxAngleControlledByAnglePlan
                            },
                            () => toleranceClassAngleControlledByAnglePlan.AssistentItem.EnteredValue?.LowerLimit == 0 && toleranceClassAngleControlledByAnglePlan.AssistentItem.EnteredValue?.UpperLimit == 0),
                        setpointTorqueControlledByAnglePlan,
                        toleranceClassTorqueControlledByAnglePlan,
                        new ConditionalAssistentPlan(
                            new List<ParentAssistentPlan>()
                            {
                                minTorqueControlledByAnglePlan,
                                maxTorqueControlledByAnglePlan
                            },
                            () => toleranceClassTorqueControlledByAnglePlan.AssistentItem.EnteredValue?.LowerLimit == 0 && toleranceClassTorqueControlledByAnglePlan.AssistentItem.EnteredValue?.UpperLimit == 0)
                    },
                    () => controlledByPlan.AssistentItem.EnteredValue == LocationControlledBy.Angle),

                CreateConfigurableField1AssistentPlan(),
                CreateConfigurableField2AssistentPlan(),
                CreateConfigurableField3AssistentPlan()
            });

            _useCases.toleranceClassGuiAdapter.RegisterGuiInterface(toleranceClassTorqueControlledByTorquePlan);
            _useCases.toleranceClassGuiAdapter.RegisterGuiInterface(toleranceClassAngleControlledByTorquePlan);
            _useCases.toleranceClassGuiAdapter.RegisterGuiInterface(toleranceClassTorqueControlledByAnglePlan);
            _useCases.toleranceClassGuiAdapter.RegisterGuiInterface(toleranceClassAngleControlledByAnglePlan);

            assistentView.SetParentPlan(parentPlan);

            return assistentView;
        }


        #region AssistentPlan Creators

        private AssistentPlan<string> CreateNumberAssistentPlan()
        {
            return new AssistentPlan<string>(
                new UniqueAssistentItemModel<string>(_useCases.location.IsNumberUnique,
                    AssistentItemType.Text,
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Enter a location number"),
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Number"),
                    _defaultLocation?.Number.ToDefaultString() ?? "",
                    (o, i) => (o as Location).Number = new LocationNumber((i as AssistentItemModel<string>).EnteredValue),
                    errorCheck: x => string.IsNullOrWhiteSpace((x as AssistentItemModel<string>).EnteredValue),
                    errorText: _localization.Strings.GetParticularString("AddLocationAssistent", "The location number is required and has to be unique"),
                    maxLengthText: 30));
        }

        private AssistentPlan<string> CreateDescriptionAssistentPlan()
        {
            return new AssistentPlan<string>(
                new AssistentItemModel<string>(AssistentItemType.Text,
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Enter a location description"),
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Description"),
                    _defaultLocation?.Description.ToDefaultString() ?? "",
                    (o, i) => (o as Location).Description = new LocationDescription((i as AssistentItemModel<string>).EnteredValue),
                    errorCheck: x => string.IsNullOrWhiteSpace((x as AssistentItemModel<string>).EnteredValue),
                    errorText: _localization.Strings.GetParticularString("AddLocationAssistent", "The location description is required"),
                    maxLengthText: 50));
        }

        private ListAssistentPlan<LocationControlledBy> CreateControlledByAssistentPlan(AssistentView assistentView)
        {
            return new ListAssistentPlan<LocationControlledBy>(
                new ListAssistentItemModel<LocationControlledBy>(
                    assistentView.Dispatcher,
                    new List<LocationControlledBy>() { LocationControlledBy.Torque, LocationControlledBy.Angle },
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Choose by which attribute the location is controlled"),
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Controlled by"),
                    _defaultLocation?.ControlledBy ?? LocationControlledBy.Torque,
                    (o, i) => (o as Location).ControlledBy = (i as ListAssistentItemModel<LocationControlledBy>).EnteredValue,
                    null,
                    x =>
                    {
                        switch (x)
                        {
                            case LocationControlledBy.Torque: return _localization.Strings.GetParticularString("Controlled by", "Torque");
                            case LocationControlledBy.Angle: return _localization.Strings.GetParticularString("Controlled by", "Angle");
                            default: return "";
                        }
                    },
                    () => { }));
        }

        private AssistentPlan<double> CreateSetPointTorqueAssistentPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Enter the setpoint for the torque"),
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Setpoint torque"),
                    _defaultLocation?.SetPointTorque.Nm ?? 0.0,
                    (o, i) => (o as Location).SetPointTorque = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm")));
        }

        private ToleranceClassAssistentPlan CreateToleranceClassTorqueAssistentPlan(AssistentView assistentView)
        {
            return new ToleranceClassAssistentPlan(
                _useCases.toleranceClass,
                new ListAssistentItemModel<ToleranceClass>(
                    assistentView.Dispatcher,
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Choose a tolerance class for the torque"),
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Tolerance class torque"),
                    null,
                    (o, i) => (o as Location).ToleranceClassTorque = (i as ListAssistentItemModel<ToleranceClass>).EnteredValue,
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Jump to tolerance class"),
                    x => x.Name,
                    () =>
                    {
                        _startUp.OpenToleranceClassDialog(assistentView);
                    }),
                _defaultLocation?.ToleranceClassTorque?.Id ?? null);
        }

        private AssistentPlan<double> CreateMinimumTorqueAssistentPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Enter a value for the minimum torque"),
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Minimum torque"),
                    _defaultLocation?.MinimumTorque.Nm ?? 0.0,
                    (o, i) => (o as Location).MinimumTorque = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm")));
        }

        private AssistentPlan<double> CreateMaximumTorqueAssistentPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Enter a value for the maximum torque"),
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Maximum torque"),
                    _defaultLocation?.MaximumTorque.Nm ?? 0.0,
                    (o, i) => (o as Location).MaximumTorque = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm")));
        }

        private AssistentPlan<double> CreateThresholdTorqueControlledByTorqueAssistentPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Enter the threshold for the torque"),
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Threshold torque"),
                    _defaultLocation?.ThresholdTorque.Nm ?? 0.0,
                    (o, i) => (o as Location).ThresholdTorque = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm"),
                    errorCheck: x => (x as AssistentItemModel<double>).EnteredValue <= 0,
                    errorText: _localization.Strings.GetParticularString("AddLocationAssistent", "The threshold torque has to be greater than 0"),
                    behaviors: new List<Behavior>()
                    {
                        new ConditionValidationBehavior()
                        {
                            Condition = x => double.Parse(x,  CultureInfo.InvariantCulture) <= 0,
                            WarningText = _localization.Strings.GetParticularString("AddLocationAssistent", "The threshold torque has to be greater than 0")
                        }
                    }));
        }

        private AssistentPlan<double> CreateThresholdTorqueControlledByAngleAssistentPlan()
        {
            var plan = CreateThresholdTorqueControlledByTorqueAssistentPlan();
            plan.AssistentItem.ErrorCheck = x => (x as AssistentItemModel<double>).EnteredValue <= 0;
            plan.AssistentItem.ErrorText = _localization.Strings.GetParticularString("AddLocationAssistent", "The threshold torque has to be greater than 0");
            plan.AssistentItem.Behaviors.Clear();
            plan.AssistentItem.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = x => double.Parse(x, CultureInfo.InvariantCulture) <= 0,
                WarningText = _localization.Strings.GetParticularString("AddLocationAssistent", "The threshold torque has to be greater than 0")
            });
            return plan;
        }

        private AssistentPlan<double> CreateSetPointAngleAssistentPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Enter the setpoint for the angle"),
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Setpoint angle"),
                    _defaultLocation?.SetPointAngle.Degree ?? 0.0,
                    (o, i) => (o as Location).SetPointAngle = Angle.FromDegree((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "°")));
        }

        private ToleranceClassAssistentPlan CreateToleranceClassAngleAssistentPlan(AssistentView assistentView)
        {
            return new ToleranceClassAssistentPlan(
                _useCases.toleranceClass,
                new ListAssistentItemModel<ToleranceClass>(
                    assistentView.Dispatcher,
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Choose a tolerance class for the angle"),
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Tolerance class angle"),
                    null,
                    (o, i) => (o as Location).ToleranceClassAngle = (i as ListAssistentItemModel<ToleranceClass>).EnteredValue,
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Jump to tolerance class"),
                    x => x.Name,
                    () =>
                    {
                        _startUp.OpenToleranceClassDialog(assistentView);
                    }),
                _defaultLocation?.ToleranceClassAngle?.Id ?? null);
        }

        private AssistentPlan<double> CreateMinimumAngleAssistentPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Enter a value for the minimum angle"),
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Minimum angle"),
                    _defaultLocation?.MinimumAngle.Degree ?? 0.0,
                    (o, i) => (o as Location).MinimumAngle = Angle.FromDegree((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "°")));
        }

        private AssistentPlan<double> CreateMaximumAngleAssistentPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Enter a value for the maximum angle"),
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Maximum angle"),
                    _defaultLocation?.MaximumAngle.Degree ?? 0.0,
                    (o, i) => (o as Location).MaximumAngle = Angle.FromDegree((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "°")));
        }

        private AssistentPlan<string> CreateConfigurableField1AssistentPlan()
        {
            return new AssistentPlan<string>(
                new AssistentItemModel<string>(
                    AssistentItemType.Text,
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Enter a value for the first configurable field"),
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Configurable field 1"),
                    _defaultLocation?.ConfigurableField1.ToDefaultString() ?? "",
                    (o, i) => (o as Location).ConfigurableField1 = new LocationConfigurableField1((i as AssistentItemModel<string>).EnteredValue),
                    maxLengthText:15));
        }

        private AssistentPlan<string> CreateConfigurableField2AssistentPlan()
        {
            return new AssistentPlan<string>(
                new AssistentItemModel<string>(
                    AssistentItemType.Text,
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Enter a value for the second configurable field"),
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Configurable field 2"),
                    _defaultLocation?.ConfigurableField2.ToDefaultString() ?? "",
                    (o, i) => (o as Location).ConfigurableField2 = new LocationConfigurableField2((i as AssistentItemModel<string>).EnteredValue),
                    maxLengthText: 1));
        }

        private AssistentPlan<bool> CreateConfigurableField3AssistentPlan()
        {
            return new AssistentPlan<bool>(
                new AssistentItemModel<bool>(
                    AssistentItemType.Boolean,
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Enter a value for the third configurable field"),
                    _localization.Strings.GetParticularString("AddLocationAssistent", "Configurable field 3"),
                    _defaultLocation?.ConfigurableField3 ?? false,
                    (o, i) => (o as Location).ConfigurableField3 = (i as AssistentItemModel<bool>).EnteredValue));
        }

        #endregion


        #region Wire AssistentPlans

        private static bool IsSetPointNotInFreeToleranceClass(AssistentItemModel<double> minItem, AssistentItemModel<double> maxItem, ListAssistentItemModel<ToleranceClass> toleranceClassItem, double setPointValue)
        {
            if (setPointValue == 0)
            {
                return false;
            }

            if (toleranceClassItem.EnteredValue?.LowerLimit == 0 && toleranceClassItem.EnteredValue?.UpperLimit == 0
                && minItem.EnteredValue != 0 && maxItem.EnteredValue != 0)
            {
                // Free input tolerance class selected

                if (minItem.AlreadySelected && !maxItem.AlreadySelected && setPointValue < minItem.EnteredValue ||
                    minItem.AlreadySelected && maxItem.AlreadySelected && (setPointValue < minItem.EnteredValue ||
                                                                           setPointValue > maxItem.EnteredValue))
                {
                    return true;
                }
            }

            return false;
        }

        public static void WireMinMaxItems(AssistentItemModel<double> minItem, AssistentItemModel<double> maxItem, ListAssistentItemModel<ToleranceClass> toleranceClassItem, AssistentItemModel<double> setPointItem, bool isTorqueNotAngle, LocationControlledBy controlledBy, LocalizationWrapper localization)
        {
            string minErrorText, maxErrorText;

            if (isTorqueNotAngle)
            {
                minErrorText = localization.Strings.GetParticularString("AddLocationAssistent",
                    "The minimum torque has to be less than or equal to the setpoint");
                maxErrorText = localization.Strings.GetParticularString("AddLocationAssistent",
                    "The maximum torque has to be greater than or equal the setpoint");
            }
            else
            {
                minErrorText = localization.Strings.GetParticularString("AddLocationAssistent",
                    "The minimum angle has to be less than or equal to the setpoint");
                maxErrorText = localization.Strings.GetParticularString("AddLocationAssistent",
                    "The maximum angle has to be greater than or equal to the setpoint");
            }

            minItem.ErrorCheck = x => (x as AssistentItemModel<double>).EnteredValue > setPointItem.EnteredValue;
            minItem.ErrorText = minErrorText;
            minItem.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = x => double.Parse(x, CultureInfo.InvariantCulture) > setPointItem.EnteredValue,
                WarningText = minErrorText
            });

            maxItem.ErrorCheck = x => (x as AssistentItemModel<double>).EnteredValue < setPointItem.EnteredValue;
            maxItem.ErrorText = maxErrorText;
            maxItem.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = x => double.Parse(x, CultureInfo.InvariantCulture) < setPointItem.EnteredValue,
                WarningText = maxErrorText
            });
 
            setPointItem.ErrorCheck = x =>
            {
                var setPointErrorText = "";
                var hasError = false;

                if (isTorqueNotAngle)
                {
                    if (controlledBy == LocationControlledBy.Torque)
                    {
                        if (setPointItem.EnteredValue <= 0)
                        {
                            setPointErrorText = localization.Strings.GetParticularString("AddLocationAssistent",
                                "The setpoint torque has to be greater than 0");
                            hasError = true;
                        }
                        else if (IsSetPointNotInFreeToleranceClass(minItem, maxItem, toleranceClassItem, setPointItem.EnteredValue))
                        {
                            setPointErrorText = localization.Strings.GetParticularString("AddLocationAssistent",
                            "The setpoint torque has to be between the tolerance class minimum and maximum");
                            hasError = true;
                        }
                    }
                    else if (controlledBy == LocationControlledBy.Angle)
                    {
                        if (setPointItem.EnteredValue < 0)
                        {
                            setPointErrorText = localization.Strings.GetParticularString("AddLocationAssistent",
                                "The setpoint torque has to be greater than or equal to 0");
                            hasError = true;
                        }
                        else if(setPointItem.EnteredValue != 0 && IsSetPointNotInFreeToleranceClass(minItem, maxItem, toleranceClassItem, setPointItem.EnteredValue))
                        {
                            setPointErrorText = localization.Strings.GetParticularString("AddLocationAssistent",
                                "The setpoint torque has to be between the tolerance class minimum and maximum");
                            hasError = true;
                        }
                    }
                }
                else
                {
                    if (controlledBy == LocationControlledBy.Torque)
                    {
                        if (setPointItem.EnteredValue < 0)
                        {
                            setPointErrorText = localization.Strings.GetParticularString("AddLocationAssistent",
                                "The setpoint angle has to be greater than or equal to 0");
                            hasError = true;
                        }
                        else if (setPointItem.EnteredValue != 0 && IsSetPointNotInFreeToleranceClass(minItem, maxItem, toleranceClassItem, setPointItem.EnteredValue))
                        {
                            setPointErrorText = localization.Strings.GetParticularString("AddLocationAssistent",
                            "The setpoint angle has to be between the tolerance class minimum and maximum");
                            hasError = true;
                        }

                    }
                    else if (controlledBy == LocationControlledBy.Angle)
                    {
                        if (setPointItem.EnteredValue <= 0)
                        {
                            setPointErrorText = localization.Strings.GetParticularString("AddLocationAssistent",
                                "The setpoint angle has to be greater than 0");
                            hasError = true;
                        }
                        else if (IsSetPointNotInFreeToleranceClass(minItem, maxItem, toleranceClassItem, setPointItem.EnteredValue))
                        {
                            setPointErrorText = localization.Strings.GetParticularString("AddLocationAssistent",
                            "The setpoint angle has to be between the tolerance class minimum and maximum");
                            hasError = true;
                        }
                    }
                }

                x.ErrorText = setPointErrorText;
                return hasError;
            };

            setPointItem.Behaviors.Add(new ConditionsValidationBehavior() {
                Conditions = new List<ConditionValidationBehavior>() { 
                new ConditionValidationBehavior()
                {
                    Condition = x => isTorqueNotAngle && controlledBy == LocationControlledBy.Torque && double.Parse(x, CultureInfo.InvariantCulture) <= 0,
                    WarningText = localization.Strings.GetParticularString("AddLocationAssistent", 
                        "The setpoint torque has to be greater than zero")
                },
                new ConditionValidationBehavior()
                {
                    Condition = x => isTorqueNotAngle && controlledBy == LocationControlledBy.Torque 
                                                      && IsSetPointNotInFreeToleranceClass(minItem, maxItem, toleranceClassItem, double.Parse(x, CultureInfo.InvariantCulture)),
                    WarningText = localization.Strings.GetParticularString("AddLocationAssistent",
                        "The setpoint torque has to be between the tolerance class minimum and maximum")
                },
                new ConditionValidationBehavior()
                {
                    Condition = x => isTorqueNotAngle && controlledBy == LocationControlledBy.Angle && double.Parse(x, CultureInfo.InvariantCulture) < 0,
                    WarningText = localization.Strings.GetParticularString("AddLocationAssistent",
                        "The setpoint torque has to be greater than or equal to 0")
                },
                new ConditionValidationBehavior()
                {
                    Condition = x => isTorqueNotAngle && controlledBy == LocationControlledBy.Angle 
                                                      && double.Parse(x, CultureInfo.InvariantCulture) != 0 
                                                      && IsSetPointNotInFreeToleranceClass(minItem, maxItem, toleranceClassItem, double.Parse(x, CultureInfo.InvariantCulture)),
                    WarningText = localization.Strings.GetParticularString("AddLocationAssistent",
                        "The setpoint torque has to be between the tolerance class minimum and maximum")
                },
                new ConditionValidationBehavior()
                {
                    Condition = x => !isTorqueNotAngle && controlledBy == LocationControlledBy.Angle && double.Parse(x, CultureInfo.InvariantCulture) <= 0,
                    WarningText = localization.Strings.GetParticularString("AddLocationAssistent",
                        "The setpoint angle has to be greater than 0")
                },
                new ConditionValidationBehavior()
                {
                    Condition = x => !isTorqueNotAngle && controlledBy == LocationControlledBy.Angle 
                                                       && IsSetPointNotInFreeToleranceClass(minItem, maxItem, toleranceClassItem, double.Parse(x, CultureInfo.InvariantCulture)),
                    WarningText = localization.Strings.GetParticularString("AddLocationAssistent",
                        "The setpoint angle has to be between the tolerance class minimum and maximum")
                },
                new ConditionValidationBehavior()
                {
                    Condition = x => !isTorqueNotAngle && controlledBy == LocationControlledBy.Torque && double.Parse(x, CultureInfo.InvariantCulture) < 0,
                    WarningText = localization.Strings.GetParticularString("AddLocationAssistent",
                        "The setpoint angle has to be greater than or equal to 0")
                },
                new ConditionValidationBehavior()
                {
                    Condition = x => !isTorqueNotAngle && controlledBy == LocationControlledBy.Torque && double.Parse(x, CultureInfo.InvariantCulture) != 0 
                                     && IsSetPointNotInFreeToleranceClass(minItem, maxItem, toleranceClassItem, double.Parse(x, CultureInfo.InvariantCulture)),
                    WarningText = localization.Strings.GetParticularString("AddLocationAssistent",
                        "The setpoint angle has to be between the tolerance class minimum and maximum")
                }
            }});
        }

        private void WireThresholdTorqueItemToSetPoint(AssistentItemModel<double> thresholdTorqueItem, AssistentItemModel<double> setpointItem)
        {
            thresholdTorqueItem.ErrorCheck = x => 
                (x as AssistentItemModel<double>).EnteredValue > setpointItem.EnteredValue ||
                (x as AssistentItemModel<double>).EnteredValue <= 0;

            thresholdTorqueItem.ErrorText = _localization.Strings.GetParticularString("AddLocationAssistent",
                "The threshold torque has to be greater than 0 and less than or equal to the setpoint");

            thresholdTorqueItem.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = x =>
                    double.Parse(x, CultureInfo.InvariantCulture) > setpointItem.EnteredValue ||
                    double.Parse(x, CultureInfo.InvariantCulture) <= 0,
                WarningText = _localization.Strings.GetParticularString("AddLocationAssistent",
                    "The threshold torque has to be greater than 0 and less than or equal to the setpoint")
            });
        }

        #endregion


        public AddLocationAssistentCreator(Location defaultLocation, StartUpImpl startUp, LocalizationWrapper localization, UseCaseCollection useCases)
        {
            _defaultLocation = defaultLocation;
            _startUp = startUp;
            _localization = localization;
            _useCases = useCases;
        }
    }
}
