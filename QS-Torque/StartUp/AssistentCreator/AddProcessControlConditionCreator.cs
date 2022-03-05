using System;
using System.Collections.Generic;
using System.Globalization;
using Client.Core.Entities;
using Common.Types.Enums;
using Core.Entities;
using Core.PhysicalValueTypes;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;

namespace StartUp.AssistentCreator
{
    public class AddProcessControlConditionCreator
    {
        private readonly Location _location;
        private readonly ILocalizationWrapper _localization;
        private readonly StartUpImpl _startUp;
        private readonly UseCaseCollection _useCases;

        public AddProcessControlConditionCreator(Location location, ILocalizationWrapper localization, StartUpImpl startUp, UseCaseCollection useCases)
        {
            _location = location;
            _localization = localization;
            _startUp = startUp;
            _useCases = useCases;
        }

        public AssistentView CreateAddProcessControlAssistant()
        {
            var assistantView = new AssistentView(_localization.Strings.GetParticularString("Add process control condition assistant", "Add new process control condition"));
            var qstProcessControlTechCreator = new QstProcessControlTechCreator(_localization);

            var lowerMeasuringLimitPlan = CreateLowerMeasuringLimit();
            var upperMeasuringLimitPlan = CreateUpperMeasuringLimit();
            var lowerInterventionLimitPlan = CreateLowerInterventionLimit();
            var upperInterventionLimitPlan = CreateUpperInterventionLimit();

            WireMinMaxMeasuringLimitItems(lowerMeasuringLimitPlan.AssistentItem, upperMeasuringLimitPlan.AssistentItem);
            WireMinMaxInterventionLimitItems(lowerInterventionLimitPlan.AssistentItem, upperInterventionLimitPlan.AssistentItem);
            var testLevelSetPlan = CreateTestLevelSetAssistantPlan(assistantView);

            var values1Plan = CreateTestLevelNumber(assistantView, new List<int>() { 1 });
            var values12Plan = CreateTestLevelNumber(assistantView, new List<int>() { 1, 2 });
            var values13Plan = CreateTestLevelNumber(assistantView, new List<int>() { 1, 3 });
            var values123Plan = CreateTestLevelNumber(assistantView, new List<int>() {1, 2, 3});


            var parentPlan = new ParentAssistentPlan(new List<ParentAssistentPlan>()
            {
                lowerMeasuringLimitPlan,
                upperMeasuringLimitPlan,
                lowerInterventionLimitPlan,
                upperInterventionLimitPlan,

                new AssistentPlan(
                    new ChapterHeadingAssistentItemModel(_localization.Strings.GetParticularString("Add process control condition assistant", "Test rules"))),
                testLevelSetPlan,
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>()
                    {
                        values1Plan,
                    },
                    () => 
                          (testLevelSetPlan?.AssistentItem?.EnteredValue?.TestLevel1?.IsActive ?? false) &&
                          (!testLevelSetPlan.AssistentItem?.EnteredValue?.TestLevel2?.IsActive ?? false) &&
                          (!testLevelSetPlan.AssistentItem?.EnteredValue?.TestLevel3?.IsActive ?? false)),
                                                                                                                                                                                                      
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>()
                    {
                        values12Plan,
                    },
                    () =>
                        (testLevelSetPlan?.AssistentItem?.EnteredValue?.TestLevel1?.IsActive ?? false) &&
                        (testLevelSetPlan.AssistentItem?.EnteredValue?.TestLevel2?.IsActive ?? false) &&
                        (!testLevelSetPlan.AssistentItem?.EnteredValue?.TestLevel3?.IsActive ?? false)),
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>()
                    {
                        values13Plan,
                    },
                    () =>
                        (testLevelSetPlan?.AssistentItem?.EnteredValue?.TestLevel1?.IsActive ?? false) &&
                        (!testLevelSetPlan.AssistentItem?.EnteredValue?.TestLevel2?.IsActive ?? false) &&
                        (testLevelSetPlan.AssistentItem?.EnteredValue?.TestLevel3?.IsActive ?? false)),
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>()
                    {
                        values123Plan,
                    },
                    () =>
                        (testLevelSetPlan?.AssistentItem?.EnteredValue?.TestLevel1?.IsActive ?? false) &&
                        (testLevelSetPlan.AssistentItem?.EnteredValue?.TestLevel2?.IsActive ?? false) &&
                        (testLevelSetPlan.AssistentItem?.EnteredValue?.TestLevel3?.IsActive ?? false)),
                CreateStartDateAssistantPlan(),
                CreateTestOperationActiveAssistantPlan(),
                
                new AssistentPlan(new ChapterHeadingAssistentItemModel(_localization.Strings.GetParticularString("Add process control condition assistant", "Audit technique"))),
                qstProcessControlTechCreator.CreateQstProcessControlTechPlan(assistantView),
                CreateExtensionPlan(assistantView)
            });

            assistantView.SetParentPlan(parentPlan);
            return assistantView;
        }

        private void WireMinMaxMeasuringLimitItems(AssistentItemModel<double> lowerLimit, AssistentItemModel<double> upperLimit)
        {
            var minimumErrorText = _localization.Strings.GetParticularString("Add process control condition assistant",
                "The lower measuring limit has to be between 0 and 9999");

            lowerLimit.ErrorCheck = x => (x as AssistentItemModel<double>).EnteredValue < 0 || (x as AssistentItemModel<double>).EnteredValue > 9999;
            lowerLimit.ErrorText = minimumErrorText;
            lowerLimit.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = x => double.Parse(x, CultureInfo.InvariantCulture) < 0 || double.Parse(x, CultureInfo.InvariantCulture) > 9999,
                WarningText = minimumErrorText
            });

            var maximumErrorText = _localization.Strings.GetParticularString("Add process control condition assistant",
                "The upper measuring limit has to be between 0 and 9999 and greater than the lower measuring limit");

            upperLimit.ErrorCheck = x => (x as AssistentItemModel<double>).EnteredValue <= lowerLimit.EnteredValue || (x as AssistentItemModel<double>).EnteredValue > 9999 || (x as AssistentItemModel<double>).EnteredValue < 0;
            upperLimit.ErrorText = maximumErrorText;
            upperLimit.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = x => double.Parse(x, CultureInfo.InvariantCulture) < 0 || double.Parse(x, CultureInfo.InvariantCulture) > 9999 || double.Parse(x, CultureInfo.InvariantCulture) <= lowerLimit.EnteredValue,
                WarningText = maximumErrorText
            });
        }

        private void WireMinMaxInterventionLimitItems(AssistentItemModel<double> lowerLimit, AssistentItemModel<double> upperLimit)
        {
            var minimumErrorText = _localization.Strings.GetParticularString("Add process control condition assistant",
                "The lower intervention limit has to be between 0 and 9999");

            lowerLimit.ErrorCheck = x => (x as AssistentItemModel<double>).EnteredValue < 0 || (x as AssistentItemModel<double>).EnteredValue > 9999;
            lowerLimit.ErrorText = minimumErrorText;
            lowerLimit.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = x => double.Parse(x, CultureInfo.InvariantCulture) < 0 || double.Parse(x, CultureInfo.InvariantCulture) > 9999,
                WarningText = minimumErrorText
            });

            var maximumErrorText = _localization.Strings.GetParticularString("Add process control condition assistant",
                "The upper intervention limit has to be between 0 and 9999 and greater than or equal to lower intervention limit");

            upperLimit.ErrorCheck = x => (x as AssistentItemModel<double>).EnteredValue < lowerLimit.EnteredValue || (x as AssistentItemModel<double>).EnteredValue > 9999 || (x as AssistentItemModel<double>).EnteredValue < 0;
            upperLimit.ErrorText = maximumErrorText;
            upperLimit.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = x => double.Parse(x, CultureInfo.InvariantCulture) < 0 || double.Parse(x, CultureInfo.InvariantCulture) > 9999 || double.Parse(x, CultureInfo.InvariantCulture) < lowerLimit.EnteredValue,
                WarningText = maximumErrorText
            });
        }

        private AssistentPlan<double> CreateLowerInterventionLimit()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Enter a value for the lower intervention limit"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Lower intervention limit"),
                    0.0,
                    (o, i) => (o as ProcessControlCondition).LowerInterventionLimit = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm")));
        }

        private AssistentPlan<double> CreateUpperInterventionLimit()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Enter a value for the upper intervention limit"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Upper intervention limit"),
                    0.0,
                    (o, i) => (o as ProcessControlCondition).UpperInterventionLimit = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm")));
        }

        private AssistentPlan<double> CreateLowerMeasuringLimit()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Enter a value for the lower measuring limit"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Lower measuring limit"),
                    _location.SetPointTorque.Nm - _location.SetPointTorque.Nm * 0.2,
                    (o, i) => (o as ProcessControlCondition).LowerMeasuringLimit = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm")));
        }

        private AssistentPlan<double> CreateUpperMeasuringLimit()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Enter a value for the upper measuring limit"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Upper measuring limit"),
                    _location.SetPointTorque.Nm + _location.SetPointTorque.Nm * 0.2,
                    (o, i) => (o as ProcessControlCondition).UpperMeasuringLimit = Torque.FromNm((i as AssistentItemModel<double>).EnteredValue),
                    _localization.Strings.GetParticularString("Unit", "Nm")));
        }


        private TestLevelSetAssistantPlan CreateTestLevelSetAssistantPlan(AssistentView assistantView)
        {
            var plan = new TestLevelSetAssistantPlan(_useCases.TestLevelSet,
                _useCases.TestLevelSetInterface,
                _localization,
                new ListAssistentItemModel<TestLevelSet>(assistantView.Dispatcher,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Choose a test level set for process control condition"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Test level set"),
                    null,
                    (resultObject, assistantItem) => (resultObject as ProcessControlCondition).TestLevelSet = (assistantItem as ListAssistentItemModel<TestLevelSet>).EnteredValue,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Jump to test level set"),
                    x => x.Name.ToDefaultString(),
                    () =>
                    {
                        _startUp.OpenTestLevelSetDialog(assistantView);
                    }));

            plan.MessageBoxRequest += (s, e) => assistantView.ShowMessageBox(e);
            return plan;
        }

        private ListAssistentPlan<int> CreateTestLevelNumber(AssistentView assistantView, List<int> values)
        {
            return new ListAssistentPlan<int>(
                new ListAssistentItemModel<int>(
                    assistantView.Dispatcher,
                    values,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Choose the test level for process control condition"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Test level"),
                    1,
                    (o, i) => (o as ProcessControlCondition).TestLevelNumber = (i as ListAssistentItemModel<int>).EnteredValue,
                    null,
                    x => x.ToString(),
                    () => { }));
        }

        private AssistentPlan<DateTime> CreateStartDateAssistantPlan()
        {
            return new AssistentPlan<DateTime>(
                new AssistentItemModel<DateTime>(
                    AssistentItemType.Date,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Enter a start date for process tests"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Start date"),
                    DateTime.Today,
                    (o, i) => (o as ProcessControlCondition).StartDate = (i as AssistentItemModel<DateTime>).EnteredValue));
        }

        private AssistentPlan<bool> CreateTestOperationActiveAssistantPlan()
        {
            return new AssistentPlan<bool>(
                new AssistentItemModel<bool>(
                    AssistentItemType.Boolean,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Choose if the process control condition is active for process tests"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Test mode active"),
                    false,
                    (resultObject, assistantItem) => (resultObject as ProcessControlCondition).TestOperationActive = (assistantItem as AssistentItemModel<bool>).EnteredValue));
        }

        private ExtensionAssistantPlan CreateExtensionPlan(AssistentView assistantView)
        {
            var plan = new ExtensionAssistantPlan(_useCases.extension,
                _useCases.extensionInterface,
                _localization,
                new ListAssistentItemModel<Extension>(assistantView.Dispatcher,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Choose a extension"),
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Extension"),
                    null,
                    (resultObject, assistantItem) => (resultObject as ProcessControlCondition).ProcessControlTech.Extension = (assistantItem as ListAssistentItemModel<Extension>).EnteredValue,
                    _localization.Strings.GetParticularString("Add process control condition assistant", "Jump to extensions"),
                    x => x.InventoryNumber.ToDefaultString(),
                    () =>
                    {
                        _startUp.OpenExtensionDialog(assistantView);
                    }));

            plan.MessageBoxRequest += (s, e) => assistantView.ShowMessageBox(e);
            return plan;
        }
    }
}
