using System.Collections.Generic;
using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using FrameworksAndDrivers.Localization;
using Microsoft.Xaml.Behaviors;

namespace StartUp.AssistentCreator
{
    class TestTechniqueAssistentCreator
    {
        private TestTechnique _defaultTestTechnique;
        private LocalizationWrapper _localization;


        public TestTechniqueAssistentCreator(LocalizationWrapper locaization, TestTechnique defaultTestTechnique = null)
        {
            _localization = locaization;
            _defaultTestTechnique = defaultTestTechnique;
        }


        public ParentAssistentPlan CreateClickWrenchAssistent()
        {
            var cycleStartPlan = CreateCycleStartAssistentPlan();
            var cycleCompletePlan = CreateCycleCompleteAssistentPlan();

            WireCycleStartAndCompleteAssistentItems(cycleStartPlan.AssistentItem, cycleCompletePlan.AssistentItem);

            return new ParentAssistentPlan(new List<ParentAssistentPlan>()
            {
                CreateEndCycleTimeAssistentPlan(),
                CreateFilterFrequencyAssistentPlan(),
                cycleCompletePlan,
                CreateMeasureDelayTimeAssistentPlan(),
                CreateResetTimeAssistentPlan(),
                cycleStartPlan,
                CreateSlipTorqueAssistentPlan()
            });
        }

        public ParentAssistentPlan CreatePulseDriverAssistent()
        {
            return new ParentAssistentPlan(new List<ParentAssistentPlan>()
            {
                CreateEndCycleTimeAssistentPlan(),
                CreateFilterFrequencyAssistentPlan(),
                CreateTorqueCoefficientAssistentPlan(),
                CreateMinimumPulseAssistentPlan(),
                CreateMaximumPulseAssistentPlan(),
                CreateThresholdPulseAssistentPlan()
            });
        }

        public (ParentAssistentPlan completeAssistentPlan, AssistentPlan<double> startFinalAnglePlan) CreatePowerToolAssistent()
        {
            var cycleStartPlan = CreateCycleStartAssistentPlan();
            var cycleCompletePlan = CreateCycleCompleteAssistentPlan();
            var startFinalAnglePlan = CreateStartFinalAngleAssistentPlan();

            WireCycleStartAndCompleteAssistentItems(cycleStartPlan.AssistentItem, cycleCompletePlan.AssistentItem);
            WireStartFinalAngleAndCycleStartAndCycleCompleteAssistentItem(startFinalAnglePlan.AssistentItem, cycleStartPlan.AssistentItem, cycleCompletePlan.AssistentItem);

            return (new ParentAssistentPlan(new List<ParentAssistentPlan>()
                    {
                        CreateEndCycleTimeAssistentPlan(),
                        CreateFilterFrequencyAssistentPlan(),
                        cycleCompletePlan,
                        CreateMeasureDelayTimeAssistentPlan(),
                        CreateResetTimeAssistentPlan(),
                        CreateMustTorqueAndAngleBeInLimitsAssistentPlan(),
                        cycleStartPlan,
                        startFinalAnglePlan
                    }),
                    startFinalAnglePlan);
        }

        public (ParentAssistentPlan completeAssistentPlan, AssistentPlan<double> startFinalAnglePlan) CreatePeakValueAssistent()
        {
            var cycleStartPlan = CreateCycleStartAssistentPlan();
            var startFinalAnglePlan = CreateStartFinalAngleAssistentPlan();

            WireStartFinalAngleAndCycleStartAndCycleCompleteAssistentItem(startFinalAnglePlan.AssistentItem, cycleStartPlan.AssistentItem, null);

            return (new ParentAssistentPlan(new List<ParentAssistentPlan>()
                    {
                        CreateEndCycleTimeAssistentPlan(),
                        CreateFilterFrequencyAssistentPlan(),
                        CreateMustTorqueAndAngleBeInLimitsAssistentPlan(),
                        cycleStartPlan,
                        startFinalAnglePlan
                    }),
                    startFinalAnglePlan);
        }


        #region Special AssistentItemCreator

        private AssistentPlan<double> CreateEndCycleTimeAssistentPlan()
        {
            return new AssistentPlan<double>(new AssistentItemModel<double>(AssistentItemType.FloatingPoint,
                _localization.Strings.GetParticularString("Test technique assistent", "Enter a value for the end cycle time"),
                _localization.Strings.GetParticularString("Test technique assistent", "End cycle time"),
                _defaultTestTechnique?.EndCycleTime ?? 0.4,
                (resultObject, assistentItem) => (resultObject as LocationToolAssignment).TestTechnique.EndCycleTime = (assistentItem as AssistentItemModel<double>).EnteredValue,
                _localization.Strings.GetParticularString("Unit", "s"),
                errorText:_localization.Strings.GetParticularString("Test technique assistent", "The end cycle time has to be between 0.1s and 5s"),
                errorCheck:assistentItem => (assistentItem as AssistentItemModel<double>).EnteredValue < 0.1 || (assistentItem as AssistentItemModel<double>).EnteredValue > 5,
                behaviors:new List<Behavior>()
                {
                    new MinValueValidationBehavior()
                    {
                        ValueType = ValidationBehavior.ValidationType.FloatingPoint,
                        MinValue = "0.1",
                        WarningText = _localization.Strings.GetParticularString("Test technique assistent", "The end cycle time has to be greater than or equal to 0.1s")
                    },
                    new MaxValueValidationBehavior()
                    {
                        ValueType = ValidationBehavior.ValidationType.FloatingPoint,
                        MaxValue = "5",
                        WarningText = _localization.Strings.GetParticularString("Test technique assistent", "The end cycle time has to be less than or equal to 5s")
                    }
                },
                hint:_localization.Strings.GetParticularString("Test technique assistent", "The STa 6000 / STa 6000 Plus ends the test when the torque drops beneath the Cycle Start value for a longer time than this timeout.")));
        }

        private AssistentPlan<double> CreateFilterFrequencyAssistentPlan()
        {
            return new AssistentPlan<double>(new AssistentItemModel<double>(AssistentItemType.FloatingPoint,
                _localization.Strings.GetParticularString("Test technique assistent", "Enter a value for the filter frequency"),
                _localization.Strings.GetParticularString("Test technique assistent", "Filter frequency"),
                _defaultTestTechnique?.FilterFrequency ?? 100,
                (resultObject, assistentItem) => (resultObject as LocationToolAssignment).TestTechnique.FilterFrequency = (assistentItem as AssistentItemModel<double>).EnteredValue,
                unit:_localization.Strings.GetParticularString("Unit", "Hz"),
                errorText:_localization.Strings.GetParticularString("Test technique assistent", "The filter frequency has to be between 100Hz and 1000Hz"),
                errorCheck:assistentItem => (assistentItem as AssistentItemModel<double>).EnteredValue < 100 || (assistentItem as AssistentItemModel<double>).EnteredValue > 1000,
                behaviors:new List<Behavior>()
                {
                    new MinValueValidationBehavior()
                    {
                        ValueType = ValidationBehavior.ValidationType.FloatingPoint,
                        MinValue = "100",
                        WarningText = _localization.Strings.GetParticularString("Test technique assistent", "The filter frequency has to be greater than or equal to 100Hz")
                    },
                    new MaxValueValidationBehavior()
                    {
                        ValueType = ValidationBehavior.ValidationType.FloatingPoint,
                        MaxValue = "1000",
                        WarningText = _localization.Strings.GetParticularString("Test technique assistent", "The filter frequency has to be less than or equal to 1000Hz")
                    }
                },
                hint: _localization.Strings.GetParticularString("Test technique assistent", "The filter frequency can be set from 100 to 1000 Hz. This is applied to the samples measured by the torque transducer to filter the noise.")));
        }

        private AssistentPlan<double> CreateCycleCompleteAssistentPlan()
        {
            return new AssistentPlan<double>(new AssistentItemModel<double>(AssistentItemType.FloatingPoint,
                _localization.Strings.GetParticularString("Test technique assistent", "Enter a value for the cycle complete"),
                _localization.Strings.GetParticularString("Test technique assistent", "Cycle complete"),
                _defaultTestTechnique?.CycleComplete ?? 0,
                (resultObject, assistentItem) => (resultObject as LocationToolAssignment).TestTechnique.CycleComplete = (assistentItem as AssistentItemModel<double>).EnteredValue,
                _localization.Strings.GetParticularString("Unit", "Nm"),
                hint:_localization.Strings.GetParticularString("Test technique assistent", "When the torque drops and remains under this value for longer than the end time parameter, the test ends.")));
        }

        private AssistentPlan<double> CreateMeasureDelayTimeAssistentPlan()
        {
            return new AssistentPlan<double>(new AssistentItemModel<double>(AssistentItemType.FloatingPoint,
                _localization.Strings.GetParticularString("Test technique assistent", "Enter a value for the measure delay time"),
                _localization.Strings.GetParticularString("Test technique assistent", "Measure delay time"),
                _defaultTestTechnique?.MeasureDelayTime ?? 0,
                (resultObject, assistentItem) => (resultObject as LocationToolAssignment).TestTechnique.MeasureDelayTime = (assistentItem as AssistentItemModel<double>).EnteredValue,
                _localization.Strings.GetParticularString("Unit", "s"),
                hint: _localization.Strings.GetParticularString("Test technique assistent", "This Parameter defines a delay time, that starts when the torque reaches the cycle start value. During this delay time, the torque is not considered.")));
        }

        private AssistentPlan<double> CreateResetTimeAssistentPlan()
        {
            return new AssistentPlan<double>(new AssistentItemModel<double>(AssistentItemType.FloatingPoint,
                _localization.Strings.GetParticularString("Test technique assistent", "Enter a value for the reset time"),
                _localization.Strings.GetParticularString("Test technique assistent", "Reset time"),
                _defaultTestTechnique?.ResetTime ?? 0,
                (resultObject, assistentItem) => (resultObject as LocationToolAssignment).TestTechnique.ResetTime = (assistentItem as AssistentItemModel<double>).EnteredValue,
                _localization.Strings.GetParticularString("Unit", "s"),
                hint: _localization.Strings.GetParticularString("Test technique assistent", "After the torque drops below the Cycle Complete, the STa 6000 / STa 6000 Plus does not consider the torque trace during this Reset time. It can be helpful to filter the bounces occurring in the pulse tool test.")));
        }

        private AssistentPlan<bool> CreateMustTorqueAndAngleBeInLimitsAssistentPlan()
        {
            return new AssistentPlan<bool>(new AssistentItemModel<bool>(AssistentItemType.Boolean,
                _localization.Strings.GetParticularString("Test technique assistent", "Must torque and angle be between the limits?"),
                _localization.Strings.GetParticularString("Test technique assistent", "Torque & angle must be within the limits"),
                _defaultTestTechnique?.MustTorqueAndAngleBeInLimits ?? false,
                (resultObject, assistentItem) => (resultObject as LocationToolAssignment).TestTechnique.MustTorqueAndAngleBeInLimits = (assistentItem as AssistentItemModel<bool>).EnteredValue,
                hint: _localization.Strings.GetParticularString("Test technique assistent", "Torque and angle must be within the limits for an OK result.")));
        }

        private AssistentPlan<double> CreateCycleStartAssistentPlan()
        {
            return new AssistentPlan<double>(new AssistentItemModel<double>(AssistentItemType.FloatingPoint,
                _localization.Strings.GetParticularString("Test technique assistent", "Enter a value for the cycle start"),
                _localization.Strings.GetParticularString("Test technique assistent", "Cycle start"),
                _defaultTestTechnique?.CycleStart ?? 0,
                (resultObject, assistentItem) => (resultObject as LocationToolAssignment).TestTechnique.CycleStart = (assistentItem as AssistentItemModel<double>).EnteredValue,
                _localization.Strings.GetParticularString("Unit", "Nm"),
                hint: _localization.Strings.GetParticularString("Test technique assistent", "Torque value from which the test starts.\n\nThis must be higher than the transducer Minimum Load value; if not, when the Pset is started the Min Load Error message is shown.\n\nIf it is set to zero, the STa 6000 / STa 6000 Plus assigns automatically the Cycle Start equal to the transducer minimum load.")));
        }

        private AssistentPlan<double> CreateStartFinalAngleAssistentPlan()
        {
            return new AssistentPlan<double>(new AssistentItemModel<double>(AssistentItemType.FloatingPoint,
                _localization.Strings.GetParticularString("Test technique assistent", "Enter a value for the start final angle"),
                _localization.Strings.GetParticularString("Test technique assistent", "Start final angle"),
                _defaultTestTechnique?.StartFinalAngle ?? 0,
                (resultObject, assistentItem) => (resultObject as LocationToolAssignment).TestTechnique.StartFinalAngle = (assistentItem as AssistentItemModel<double>).EnteredValue,
                _localization.Strings.GetParticularString("Unit", "Nm"),
                hint: _localization.Strings.GetParticularString("Test technique assistent", "For control strategies involving angle measurement, it specifies the torque value from which the angle measurement starts.")));
        }

        private AssistentPlan<double> CreateSlipTorqueAssistentPlan()
        {
            return new AssistentPlan<double>(new AssistentItemModel<double>(AssistentItemType.FloatingPoint,
                _localization.Strings.GetParticularString("Test technique assistent", "Enter a value for the slip torque"),
                _localization.Strings.GetParticularString("Test technique assistent", "Slip torque"),
                _defaultTestTechnique?.SlipTorque ?? 0,
                (resultObject, assistentItem) => (resultObject as LocationToolAssignment).TestTechnique.SlipTorque = (assistentItem as AssistentItemModel<double>).EnteredValue,
                unit: _localization.Strings.GetParticularString("Unit", "Nm"),
                errorText: _localization.Strings.GetParticularString("Test technique assistent", "The slip torque has to be between 0Nm and 9999.9Nm"),
                errorCheck: assistentItem => (assistentItem as AssistentItemModel<double>).EnteredValue < 0 || (assistentItem as AssistentItemModel<double>).EnteredValue > 9999.9,
                behaviors: new List<Behavior>()
                {
                    new MinValueValidationBehavior()
                    {
                        ValueType = ValidationBehavior.ValidationType.FloatingPoint,
                        MinValue = "0",
                        WarningText = _localization.Strings.GetParticularString("Test technique assistent", "The slip torque has to be greater than or equal to 0Nm")
                    },
                    new MaxValueValidationBehavior()
                    {
                        ValueType = ValidationBehavior.ValidationType.FloatingPoint,
                        MaxValue = "9999.9",
                        WarningText = _localization.Strings.GetParticularString("Test technique assistent", "The slip torque has to be less than or equal to 9999.9Nm")
                    }
                },
                hint: _localization.Strings.GetParticularString("Test technique assistent", "This parameter is active only for Click-wrench control strategy. It specifies the requested drop in order to consider the relative peak value measured as the click - point.")));
        }

        private AssistentPlan<double> CreateTorqueCoefficientAssistentPlan()
        {
            return new AssistentPlan<double>(new AssistentItemModel<double>(AssistentItemType.FloatingPoint,
                _localization.Strings.GetParticularString("Test technique assistent", "Enter a value for the torque coefficient"),
                _localization.Strings.GetParticularString("Test technique assistent", "Torque coefficient"),
                _defaultTestTechnique?.TorqueCoefficient ?? 0.1,
                (resultObject, assistentItem) => (resultObject as LocationToolAssignment).TestTechnique.TorqueCoefficient = (assistentItem as AssistentItemModel<double>).EnteredValue,
                errorText: _localization.Strings.GetParticularString("Test technique assistent", "The torque coefficient has to be between 0.1 and 10"),
                errorCheck: assistentItem => (assistentItem as AssistentItemModel<double>).EnteredValue < 0.1 || (assistentItem as AssistentItemModel<double>).EnteredValue > 10,
                behaviors: new List<Behavior>()
                {
                    new MinValueValidationBehavior()
                    {
                        ValueType = ValidationBehavior.ValidationType.FloatingPoint,
                        MinValue = "0.1",
                        WarningText = _localization.Strings.GetParticularString("Test technique assistent", "The torque coefficient has to be greater than or equal to 0.1")
                    },
                    new MaxValueValidationBehavior()
                    {
                        ValueType = ValidationBehavior.ValidationType.FloatingPoint,
                        MaxValue = "10",
                        WarningText = _localization.Strings.GetParticularString("Test technique assistent", "The torque coefficient has to be less than or equal to 10")
                    }
                },
                hint: _localization.Strings.GetParticularString("Test technique assistent", "The torque coefficient can be used to align the torque measured by the transducer with the real torque produced on the joint.\n\nThe torque produced on the real joint will be always smaller(ideally equal) than the peak torque measured on the transducer.")));
        }

        private AssistentPlan<int> CreateMinimumPulseAssistentPlan()
        {
            return new AssistentPlan<int>(new AssistentItemModel<int>(AssistentItemType.Numeric,
                _localization.Strings.GetParticularString("Test technique assistent", "Enter a value for the minimum pulse"),
                _localization.Strings.GetParticularString("Test technique assistent", "Minimum pulse"),
                _defaultTestTechnique?.MinimumPulse ?? 1,
                (resultObject, assistentItem) => (resultObject as LocationToolAssignment).TestTechnique.MinimumPulse = (assistentItem as AssistentItemModel<int>).EnteredValue,
                errorText: _localization.Strings.GetParticularString("Test technique assistent", "The minimum pulse has to be between 1 and 255"),
                errorCheck: assistentItem => (assistentItem as AssistentItemModel<int>).EnteredValue < 1 || (assistentItem as AssistentItemModel<int>).EnteredValue > 255,
                behaviors: new List<Behavior>()
                {
                    new MinValueValidationBehavior()
                    {
                        ValueType = ValidationBehavior.ValidationType.Numeric,
                        MinValue = "1",
                        WarningText = _localization.Strings.GetParticularString("Test technique assistent", "The minimum pulse has to be greater than or equal to 1")
                    },
                    new MaxValueValidationBehavior()
                    {
                        ValueType = ValidationBehavior.ValidationType.Numeric,
                        MaxValue = "255",
                        WarningText = _localization.Strings.GetParticularString("Test technique assistent", "The minimum pulse has to be less than or equal to 255")
                    }
                },
                hint: _localization.Strings.GetParticularString("Test technique assistent", "The value is used for pulse tools to calculate the Cm-Cmk related to the number of pulses per second.")));
        }

        private AssistentPlan<int> CreateMaximumPulseAssistentPlan()
        {
            return new AssistentPlan<int>(new AssistentItemModel<int>(AssistentItemType.Numeric,
                _localization.Strings.GetParticularString("Test technique assistent", "Enter a value for the maximum pulse"),
                _localization.Strings.GetParticularString("Test technique assistent", "Maximum pulse"),
                _defaultTestTechnique?.MaximumPulse ?? 1,
                (resultObject, assistentItem) => (resultObject as LocationToolAssignment).TestTechnique.MaximumPulse = (assistentItem as AssistentItemModel<int>).EnteredValue,
                errorText: _localization.Strings.GetParticularString("Test technique assistent", "The maximum pulse has to be between 1 and 255"),
                errorCheck: assistentItem => (assistentItem as AssistentItemModel<int>).EnteredValue < 1 || (assistentItem as AssistentItemModel<int>).EnteredValue > 255,
                behaviors: new List<Behavior>()
                {
                    new MinValueValidationBehavior()
                    {
                        ValueType = ValidationBehavior.ValidationType.Numeric,
                        MinValue = "1",
                        WarningText = _localization.Strings.GetParticularString("Test technique assistent", "The maximum pulse has to be greater than or equal to 1")
                    },
                    new MaxValueValidationBehavior()
                    {
                        ValueType = ValidationBehavior.ValidationType.Numeric,
                        MaxValue = "255",
                        WarningText = _localization.Strings.GetParticularString("Test technique assistent", "The maximum pulse has to be less than or equal to 255")
                    }
                },
                hint: _localization.Strings.GetParticularString("Test technique assistent", "The value is used for pulse tools to calculate the Cm-Cmk related to the number of pulses per second.")));
        }

        private AssistentPlan<int> CreateThresholdPulseAssistentPlan()
        {
            return new AssistentPlan<int>(new AssistentItemModel<int>(AssistentItemType.Numeric,
                _localization.Strings.GetParticularString("Test technique assistent", "Enter a value for the threshold"),
                _localization.Strings.GetParticularString("Test technique assistent", "Threshold"),
                _defaultTestTechnique?.Threshold ?? 80,
                (resultObject, assistentItem) => (resultObject as LocationToolAssignment).TestTechnique.Threshold = (assistentItem as AssistentItemModel<int>).EnteredValue,
                errorText: _localization.Strings.GetParticularString("Test technique assistent", "The threshold has to be between 1 and 99"),
                errorCheck: assistentItem => (assistentItem as AssistentItemModel<int>).EnteredValue < 1 || (assistentItem as AssistentItemModel<int>).EnteredValue > 99,
                behaviors: new List<Behavior>()
                {
                    new MinValueValidationBehavior()
                    {
                        ValueType = ValidationBehavior.ValidationType.Numeric,
                        MinValue = "1",
                        WarningText = _localization.Strings.GetParticularString("Test technique assistent", "The threshold has to be greater than or equal to 1")
                    },
                    new MaxValueValidationBehavior()
                    {
                        ValueType = ValidationBehavior.ValidationType.Numeric,
                        MaxValue = "99",
                        WarningText = _localization.Strings.GetParticularString("Test technique assistent", "The threshold has to be less than or equal to 99")
                    }
                },
                hint: _localization.Strings.GetParticularString("Test technique assistent", "For pulse tools to filter the 100% peak.")));
        }

        #endregion


        #region WireAssistentItems

        private void WireCycleStartAndCompleteAssistentItems(AssistentItemModel<double> cycleStartItem, AssistentItemModel<double> cycleCompleteItem)
        {
            cycleStartItem.PropertyChanged += (s, e) =>
            {
                if (!cycleCompleteItem.AlreadySelected && e.PropertyName == nameof(AssistentItemModel<double>.EnteredValue))
                {
                    cycleCompleteItem.DefaultValue = cycleStartItem.EnteredValue / 2;
                }
            };
        }

        private void WireStartFinalAngleAndCycleStartAndCycleCompleteAssistentItem(AssistentItemModel<double> startFinalAngleItem, AssistentItemModel<double> cycleStartItem, AssistentItemModel<double> cycleCompleteItem)
        {
            startFinalAngleItem.ErrorCheck = assistentItem =>
            {
                if (cycleCompleteItem != null)
                {
                    return startFinalAngleItem.EnteredValue < 0 || (cycleStartItem.EnteredValue == 0 && cycleCompleteItem.EnteredValue == 0 && startFinalAngleItem.EnteredValue == 0);
                }
                else
                {
                    return startFinalAngleItem.EnteredValue < 0 || (cycleStartItem.EnteredValue == 0 && startFinalAngleItem.EnteredValue <= 0);
                }
            };
            startFinalAngleItem.ErrorText = _localization.Strings.GetParticularString("Test technique assistent", "Start final angle has to be greater tha zero if cycle start amd cycle complete are zero, otherwise start final angle has to be greater than or equal to zero");

            startFinalAngleItem.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = inputString =>
                {
                    if (cycleCompleteItem != null)
                    {
                        return startFinalAngleItem.EnteredValue < 0 || (cycleStartItem.EnteredValue == 0 && cycleCompleteItem.EnteredValue == 0 && startFinalAngleItem.EnteredValue == 0);
                    }
                    else
                    {
                        return startFinalAngleItem.EnteredValue < 0 || (cycleStartItem.EnteredValue == 0 && startFinalAngleItem.EnteredValue <= 0);
                    }
                },
                WarningText = _localization.Strings.GetParticularString("Test technique assistent", "Start final angle has to be greater tha zero if cycle start amd cycle complete are zero, otherwise start final angle has to be greater than or equal to zero")
            });
        }

        #endregion
    }
}
