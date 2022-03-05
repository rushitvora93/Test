using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Common.Types.Enums;
using Core.Entities;
using Core.PhysicalValueTypes;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using FrameworksAndDrivers.Localization;
using InterfaceAdapters.Communication;
using Microsoft.Xaml.Behaviors;

namespace StartUp.AssistentCreator
{
    public class AddTestEquipmentAssistantCreator
    {
        private readonly List<TestEquipmentType> _availableTestEquipmentTypes;
        private readonly TestEquipment _defaultTestEquipmentDefaultValue;
        private readonly TestEquipmentModel _testEquipmentModelDefault;
        private readonly TestEquipmentType? _testEquipmentTypeDefault;
        private readonly LocalizationWrapper _localization;
        private readonly UseCaseCollection _useCases;

        public AddTestEquipmentAssistantCreator(List<TestEquipmentType> availableTestEquipmentTypes, TestEquipment testEquipmentDefaultValue, 
            TestEquipmentModel testEquipmentModelDefault, TestEquipmentType? testEquipmentTypeDefault, LocalizationWrapper localization, UseCaseCollection useCases)
        {
            _availableTestEquipmentTypes = availableTestEquipmentTypes;
            _defaultTestEquipmentDefaultValue = testEquipmentDefaultValue;
            _testEquipmentModelDefault = testEquipmentModelDefault;
            _testEquipmentTypeDefault = testEquipmentTypeDefault;
            _localization = localization;
            _useCases = useCases;
        }

        public AssistentView CreateAddTestEquipmentAssistant()
        {
            var assistantView = new AssistentView(_localization.Strings.GetParticularString("Add test equipment assistant", "Add new test equipment"));
            var testEquipmentModelPlan = CreateTestEquipmentModelPlan(assistantView);
            var testEquipmentTypePlan = CreateTestEquipmentTypePlan(assistantView);
            var minimumCapacityPlan = CreateCapacityMinimumAssistantPlan();
            var maximumCapacityPlan = CreateCapacityMaximumAssistantPlan();

            WireMinMaxItems(minimumCapacityPlan.AssistentItem,maximumCapacityPlan.AssistentItem);

            var parentPlan = new ParentAssistentPlan(new List<ParentAssistentPlan>()
            {
                CreateSerialNumberAssistantPlan(),
                CreateInventoryNumberAssistantPlan(),
                testEquipmentTypePlan,
                testEquipmentModelPlan,
                CreateLastCalibrationPlan(),
                CreateCalibrationIntervalAssistantPlan(),
                CreateTestEquipmentCalibrationNormAssistantPlan(),
                CreateTestEquipmentVersionAssistantPlan(),
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>()
                    {
                        minimumCapacityPlan,
                    },
                    () => testEquipmentTypePlan?.AssistentItem?.EnteredValue  == TestEquipmentType.Wrench),
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>()
                    {
                        maximumCapacityPlan,
                    },
                    () => testEquipmentTypePlan?.AssistentItem?.EnteredValue  == TestEquipmentType.Wrench),
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>()
                    {
                        CreateUseForCtlPlan(),
                    },
                    () => testEquipmentModelPlan?.AssistentItem?.EnteredValue?.UseForCtl ?? false),
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>()
                    {
                        CreateUseForRotPlan(),
                    },
                    () => testEquipmentModelPlan?.AssistentItem?.EnteredValue?.UseForRot ?? false)
            });

            testEquipmentTypePlan.AssistentItem.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(AssistentItemModel<TestEquipmentType>.EnteredValue))
                {
                    testEquipmentModelPlan.TestEquipmentType = testEquipmentTypePlan.AssistentItem.EnteredValue;
                    testEquipmentModelPlan.RefillListItems();
                }
            };

            assistantView.SetParentPlan(parentPlan);
            return assistantView;
        }

        private AssistentPlan<string> CreateSerialNumberAssistantPlan()
        {
            return new AssistentPlan<string>(
                new UniqueAssistentItemModel<string>(_useCases.testEquipment.IsSerialNumberUnique,
                     AssistentItemType.Text,
                     _localization.Strings.GetParticularString("Add test equipment assistant", "Enter a serial number"),
                     _localization.Strings.GetParticularString("Add test equipment assistant", "Serial number"),
                     _defaultTestEquipmentDefaultValue?.SerialNumber?.ToDefaultString() ?? "",
                     (o, i) => (o as TestEquipment).SerialNumber = new TestEquipmentSerialNumber((i as AssistentItemModel<string>).EnteredValue),
                     errorCheck: x => string.IsNullOrWhiteSpace((x as AssistentItemModel<string>).EnteredValue),
                     errorText: _localization.Strings.GetParticularString("Add test equipment assistant", "The serial number is a required field and has to be unique"),
                     maxLengthText: 20));
        }

        private AssistentPlan<string> CreateInventoryNumberAssistantPlan()
        {
            return new AssistentPlan<string>(
                new UniqueAssistentItemModel<string>(_useCases.testEquipment.IsInventoryNumberUnique,
                    AssistentItemType.Text,
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Enter a inventory number"),
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Inventory number"),
                    _defaultTestEquipmentDefaultValue?.InventoryNumber?.ToDefaultString() ?? "",
                    (o, i) => (o as TestEquipment).InventoryNumber = new TestEquipmentInventoryNumber((i as AssistentItemModel<string>).EnteredValue),
                    errorCheck: x => string.IsNullOrWhiteSpace((x as AssistentItemModel<string>).EnteredValue),
                    errorText: _localization.Strings.GetParticularString("Add test equipment assistant", "The inventory number is a required field and has to be unique"),
                    maxLengthText: 20));
        }

        private TestEquipmentModelAssistantPlan CreateTestEquipmentModelPlan(AssistentView assistantView)
        {
            var plan = new TestEquipmentModelAssistantPlan(_useCases.testEquipment,
                _useCases.testEquipmentInterface,
                _localization,
                new ListAssistentItemModel<TestEquipmentModel>(assistantView.Dispatcher,
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Choose a test equipment model"),
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Test equipment model"),
                    _defaultTestEquipmentDefaultValue?.TestEquipmentModel ?? _testEquipmentModelDefault,
                    (resultObject, assistantItem) => (resultObject as TestEquipment).TestEquipmentModel = (assistantItem as ListAssistentItemModel<TestEquipmentModel>).EnteredValue,
                    "",
                    x => x.TestEquipmentModelName.ToDefaultString(),
                    () => {}),
                    _defaultTestEquipmentDefaultValue?.TestEquipmentModel?.Id ?? _testEquipmentModelDefault?.Id);

            plan.TestEquipmentType = _defaultTestEquipmentDefaultValue?.TestEquipmentModel?.Type ??
                                     _testEquipmentModelDefault?.Type ?? _testEquipmentTypeDefault ??
                                     _availableTestEquipmentTypes.FirstOrDefault();

            plan.MessageBoxRequest += (s, e) => assistantView.ShowMessageBox(e);
            return plan;
        }

        private AssistentPlan<TestEquipmentType> CreateTestEquipmentTypePlan(AssistentView assistentView)
        {
            return new ListAssistentPlan<TestEquipmentType>(
                new ListAssistentItemModel<TestEquipmentType>(
                    assistentView.Dispatcher,
                    _availableTestEquipmentTypes,
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Choose the test equipment type"),
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Test equipment type"),
                    _defaultTestEquipmentDefaultValue?.TestEquipmentModel?.Type ??
                    _testEquipmentModelDefault?.Type ?? _testEquipmentTypeDefault ??
                    _availableTestEquipmentTypes.FirstOrDefault(),
                    (o, i) => { },
                    null,
                    x => TestEquipmentTypeModel.GetTranslationForTestEquipmentType(x, _localization),
                    () => { }));
        }

        private AssistentPlan<DateTime> CreateLastCalibrationPlan()
        {
            return new AssistentPlan<DateTime>(
                new AssistentItemModel<DateTime>(AssistentItemType.Date,
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Choose the last calibration of the test equipment"),
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Last calibration date"),
                    _defaultTestEquipmentDefaultValue?.LastCalibration ?? DateTime.Today,
                    (resultObject, assistantItem) => (resultObject as TestEquipment).LastCalibration = (assistantItem as AssistentItemModel<DateTime>).EnteredValue));
        }

        private AssistentPlan<long> CreateCalibrationIntervalAssistantPlan()
        {
            return new AssistentPlan<long>(
                new AssistentItemModel<long>(
                    AssistentItemType.Numeric,
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Enter a calibration interval for the test equipment"),
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Interval"),
                    _defaultTestEquipmentDefaultValue?.CalibrationInterval?.IntervalValue ?? 365,
                    (o, i) => (o as TestEquipment).CalibrationInterval = new Interval(){ IntervalValue = (i as AssistentItemModel<long>).EnteredValue, Type = IntervalType.EveryXDays},
                    _localization.Strings.GetParticularString("Unit", "Days"),
                    hint: _localization.Strings.GetParticularString("Add test equipment assistant", "Every x days"),
                    errorCheck: assistentItem => (assistentItem as AssistentItemModel<long>).EnteredValue <= 0 || (assistentItem as AssistentItemModel<long>).EnteredValue > 900,
                    errorText: _localization.Strings.GetParticularString("Add test equipment assistant", "The calibration interval has to be greater than 0 and less than or equal to 900"),
                    behaviors: new List<Behavior>()
                    {
                        new ConditionValidationBehavior()
                        {
                            Condition = x => string.IsNullOrWhiteSpace(x) ? true : long.Parse(x, NumberStyles.Number, CultureInfo.InvariantCulture) <= 0 || long.Parse(x, NumberStyles.Number, CultureInfo.InvariantCulture) > 900,
                            WarningText = _localization.Strings.GetParticularString("Add test equipment assistant", "The calibration interval has to be greater than 0 and less than or equal to 900")
                        }
                    }));
        }

        private AssistentPlan<string> CreateTestEquipmentVersionAssistantPlan()
        {
            return new AssistentPlan<string>(
                new AssistentItemModel<string>(AssistentItemType.Text,
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Enter a test equipment firmware version"),
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Firmware version"),
                    _defaultTestEquipmentDefaultValue?.Version?.ToDefaultString() ?? "",
                    (o, i) => (o as TestEquipment).Version = new TestEquipmentVersion((i as AssistentItemModel<string>).EnteredValue),
                    maxLengthText: 10));
        }

        private AssistentPlan<string> CreateTestEquipmentCalibrationNormAssistantPlan()
        {
            return new AssistentPlan<string>(
                new AssistentItemModel<string>(AssistentItemType.Text,
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Enter a test equipment calibration norm"),
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Calibration norm"),
                    _defaultTestEquipmentDefaultValue?.CalibrationNorm.ToDefaultString() ?? "",
                    (o, i) => (o as TestEquipment).CalibrationNorm = new CalibrationNorm((i as AssistentItemModel<string>).EnteredValue),
                    maxLengthText: 30));
        }

        private AssistentPlan<double> CreateCapacityMinimumAssistantPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Enter a value for the capacity minimum"),
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Capacity minimum"),
                    _defaultTestEquipmentDefaultValue?.CapacityMin.Nm ?? 0.0,
                    (o, i) => (o as TestEquipment).CapacityMin = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm")));
        }

        private AssistentPlan<double> CreateCapacityMaximumAssistantPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Enter a value for the capacity maximum"),
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Capacity maximum"),
                    _defaultTestEquipmentDefaultValue?.CapacityMax.Nm ?? 0.0,
                    (o, i) => (o as TestEquipment).CapacityMax = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm")));
        }

        private void WireMinMaxItems(AssistentItemModel<double> capacityMinItem, AssistentItemModel<double> capacityMaxItem)
        {
            var minimumCapacityErrorText = _localization.Strings.GetParticularString("Add test equipment assistant",
                "The minimum capacity has to be between 0 and 1000");

            capacityMinItem.ErrorCheck = x => (x as AssistentItemModel<double>).EnteredValue < 0 || (x as AssistentItemModel<double>).EnteredValue > 1000;
            capacityMinItem.ErrorText = minimumCapacityErrorText;
            capacityMinItem.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = x => double.Parse(x, CultureInfo.InvariantCulture) < 0 || double.Parse(x, CultureInfo.InvariantCulture) > 1000,
                WarningText = minimumCapacityErrorText
            });

            var maximumCapacityErrorText = _localization.Strings.GetParticularString("Add test equipment assistant",
                "The maximum capacity has to be between 0 and 9999 and greater than or equal to minimum capacity");

            capacityMaxItem.ErrorCheck = x => (x as AssistentItemModel<double>).EnteredValue < capacityMinItem.EnteredValue || (x as AssistentItemModel<double>).EnteredValue > 9999 || (x as AssistentItemModel<double>).EnteredValue < 0;
            capacityMaxItem.ErrorText = maximumCapacityErrorText;
            capacityMaxItem.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = x => double.Parse(x, CultureInfo.InvariantCulture) < 0 || double.Parse(x, CultureInfo.InvariantCulture) > 9999 || double.Parse(x, CultureInfo.InvariantCulture) < capacityMinItem.EnteredValue,
                WarningText = maximumCapacityErrorText
            });
        }

        private AssistentPlan<bool> CreateUseForCtlPlan()
        {
            return new AssistentPlan<bool>(
                new AssistentItemModel<bool>(
                    AssistentItemType.Boolean,
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Enter a value if the test equipment can be used for process testing"),
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Use for ctl"),
                    _defaultTestEquipmentDefaultValue?.UseForCtl ?? _testEquipmentModelDefault?.UseForCtl ?? false,
                    (o, i) => (o as TestEquipment).UseForCtl = (i as AssistentItemModel<bool>).EnteredValue));
        }

        private AssistentPlan<bool> CreateUseForRotPlan()
        {
            return new AssistentPlan<bool>(
                new AssistentItemModel<bool>(
                    AssistentItemType.Boolean,
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Enter a value if the test equipment can be used for rotating testing"),
                    _localization.Strings.GetParticularString("Add test equipment assistant", "Use for rot"),
                    _defaultTestEquipmentDefaultValue?.UseForRot ?? _testEquipmentModelDefault?.UseForRot ?? false,
                    (o, i) => (o as TestEquipment).UseForRot = (i as AssistentItemModel<bool>).EnteredValue));
        }
    }
}
