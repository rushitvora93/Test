using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using FrameworksAndDrivers.Localization;
using Microsoft.Xaml.Behaviors;

namespace StartUp.AssistentCreator
{
    class AddTestPlanAssistantCreator
    {
        private LocalizationWrapper _localization;
        private UseCaseCollection _useCases;
        private TestPlan _defaultTestPlan;

        public AddTestPlanAssistantCreator(LocalizationWrapper localization, UseCaseCollection useCases, TestPlan defaultTestPlan = null)
        {
            _localization = localization;
            _useCases = useCases;
            _defaultTestPlan = defaultTestPlan;
        }

        public AssistentView CreateAddTestPlanAssistant()
        {
            AssistentView assistantView = new AssistentView(_localization.Strings.GetParticularString("AddTestPlanAssistant", "Add new test plan"));

            var startDateTestPlan = CreateStartDateAssistantPlan();
            var isEndDateActivePlan = CreateIsEndDateActiveAssistantPlan();
            var endDatePlan = CreateEndDateDateAssistantPlan();

            endDatePlan.AssistentItem.ErrorText = _localization.Strings.GetParticularString("AddTestPlanAssistant", "The end date has to be greater than the start date");
            endDatePlan.AssistentItem.ErrorCheck = assistantItem => startDateTestPlan.AssistentItem.EnteredValue.Date >= (assistantItem as AssistentItemModel<DateTime>).EnteredValue.Date;
            endDatePlan.AssistentItem.Behaviors.Add(new ConditionValidationBehavior()
            {
                WarningText = _localization.Strings.GetParticularString("AddTestPlanAssistant", "The end date has to be greater than the start date"),
                Condition = value => startDateTestPlan.AssistentItem.EnteredValue.Date >= DateTime.Parse(value).Date
            });

            var parentPlan = new ParentAssistentPlan(new List<ParentAssistentPlan>()
            {
                CreateNameAssistantPlan(),
                CreateIntervalAssistantPlan(),
                CreateSampleNumberAssistantPlan(),
                CreateBehaviorAssistantPlan(assistantView),
                CreateConsiderHolidaysAssistantPlan(),
                startDateTestPlan,
                isEndDateActivePlan,
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>()
                    {
                        endDatePlan
                    },
                    () => isEndDateActivePlan.AssistentItem.EnteredValue)
            });

            PropertyChangedEventManager.AddHandler(
                isEndDateActivePlan.AssistentItem,
                (s, e) =>
                {
                    var selectedItem = assistantView.ViewModel.SelectedAssistentItem;
                    assistantView.ViewModel.RefillAssistentItems();
                    assistantView.ViewModel.SelectedAssistentItem = selectedItem;
                },
                nameof(AssistentItemModel<bool>.EnteredValue));

            assistantView.SetParentPlan(parentPlan);
            return assistantView;
        }


        #region Special AssistentPlanCreator
        private AssistentPlan<string> CreateNameAssistantPlan()
        {
            return new AssistentPlan<string>(
                new UniqueAssistentItemModel<string>(x => _useCases.TestLevelSet.IsTestLevelSetNameUnique(x),
                    AssistentItemType.Text,
                    _localization.Strings.GetParticularString("AddTestPlanAssistant", "Enter a name for the test plan"),
                    _localization.Strings.GetParticularString("AddTestPlanAssistant", "Name"),
                    _defaultTestPlan?.Name?.ToDefaultString() ?? "",
                    (resultObject, assistantItem) => (resultObject as TestPlan).Name = new TestPlanName((assistantItem as AssistentItemModel<string>).EnteredValue),
                    maxLengthText: 200,
                    errorText: _localization.Strings.GetParticularString("AddTestPlanAssistant", "The name is a required field"),
                    errorCheck: assistantItem => string.IsNullOrWhiteSpace((assistantItem as AssistentItemModel<string>).EnteredValue),
                    behaviors: new List<Behavior>()
                    {
                        new ConditionValidationBehavior()
                        {
                            Condition = x => string.IsNullOrWhiteSpace(x),
                            WarningText = _localization.Strings.GetParticularString("AddTestPlanAssistant", "The name is a required field")
                        }
                    }));
        }

        private AssistentPlan<Interval> CreateIntervalAssistantPlan()
        {
            return new AssistentPlan<Interval>(
                new AssistentItemModel<Interval>(AssistentItemType.Interval,
                    _localization.Strings.GetParticularString("AddTestPlanAssistant", "Enter a test interval for the test plan"),
                    _localization.Strings.GetParticularString("AddTestPlanAssistant", "Interval"),
                    _defaultTestPlan?.TestInterval?.CopyDeep() ?? new Interval() { IntervalValue = 1, Type = IntervalType.EveryXDays },
                    (resultObject, assistantItem) => (resultObject as TestPlan).TestInterval = (assistantItem as AssistentItemModel<Interval>).EnteredValue,
                    errorText: _localization.Strings.GetParticularString("AddTestPlanAssistant", "The interval has to be greater than one shift"),
                    errorCheck: assistantItem => (assistantItem as AssistentItemModel<Interval>).EnteredValue.IntervalValue <= 0,
                    behaviors: new List<Behavior>()
                    {
                        new ConditionValidationBehavior()
                        {
                            Condition = x => string.IsNullOrWhiteSpace(x) ? true : int.Parse(x) <= 0,
                            WarningText = _localization.Strings.GetParticularString("AddTestPlanAssistant", "The interval has to be greater than one shift")
                        }
                    }));
        }

        private AssistentPlan<long> CreateSampleNumberAssistantPlan()
        {
            return new AssistentPlan<long>(
                new AssistentItemModel<long>(AssistentItemType.Numeric,
                    _localization.Strings.GetParticularString("AddTestPlanAssistant", "Enter the sample number for the test plan"),
                    _localization.Strings.GetParticularString("AddTestPlanAssistant", "Sample number"),
                    _defaultTestPlan?.SampleNumber ?? 1,
                    (resultObject, assistantItem) => (resultObject as TestPlan).SampleNumber = (assistantItem as AssistentItemModel<long>).EnteredValue,
                    errorText: _localization.Strings.GetParticularString("AddTestPlanAssistant", "The sample number has to greater than 0"),
                    errorCheck: assistantItem => (assistantItem as AssistentItemModel<long>).EnteredValue <= 0,
                    behaviors: new List<Behavior>()
                    {
                        new ConditionValidationBehavior()
                        {
                            Condition = x => long.Parse(x,  CultureInfo.InvariantCulture) <= 0,
                            WarningText = _localization.Strings.GetParticularString("AddTestPlanAssistant", "The sample number has to greater than 0")
                        }
                    }));
        }

        private ListAssistentPlan<TestPlanBehavior> CreateBehaviorAssistantPlan(AssistentView assistantView)
        {
            return new ListAssistentPlan<TestPlanBehavior>(
                new ListAssistentItemModel<TestPlanBehavior>(
                    assistantView.Dispatcher,
                    new List<TestPlanBehavior>() { TestPlanBehavior.Dynamic, TestPlanBehavior.Static },
                    _localization.Strings.GetParticularString("AddTestPlanAssistant", "Choose the behavior of the test plan"),
                    _localization.Strings.GetParticularString("AddTestPlanAssistant", "Behavior"),
                    _defaultTestPlan?.Behavior ?? TestPlanBehavior.Dynamic,
                    (o, i) => (o as TestPlan).Behavior = (i as ListAssistentItemModel<TestPlanBehavior>).EnteredValue,
                    null,
                    x =>
                    {
                        switch (x)
                        {
                            case TestPlanBehavior.Dynamic: return _localization.Strings.GetParticularString("TestPlanBehavior", "Dynamic");
                            case TestPlanBehavior.Static: return _localization.Strings.GetParticularString("TestPlanBehavior", "Static");
                            default: return "";
                        }
                    },
                    () => { }));
        }

        private AssistentPlan<bool> CreateConsiderHolidaysAssistantPlan()
        {
            return new AssistentPlan<bool>(
                new AssistentItemModel<bool>(AssistentItemType.Boolean,
                    _localization.Strings.GetParticularString("AddTestPlanAssistant", "Select if the holidays should be considered"),
                    _localization.Strings.GetParticularString("AddTestPlanAssistant", "Consider holidays"),
                    _defaultTestPlan?.ConsiderHolidays ?? true,
                    (resultObject, assistantItem) => (resultObject as TestPlan).ConsiderHolidays = (assistantItem as AssistentItemModel<bool>).EnteredValue));
        }

        private AssistentPlan<DateTime> CreateStartDateAssistantPlan()
        {
            return new AssistentPlan<DateTime>(
                new AssistentItemModel<DateTime>(AssistentItemType.Date,
                    _localization.Strings.GetParticularString("AddTestPlanAssistant", "Choose the start date of the test plan"),
                    _localization.Strings.GetParticularString("AddTestPlanAssistant", "Start date"),
                    _defaultTestPlan?.StartDate ?? DateTime.Today,
                    (resultObject, assistantItem) => (resultObject as TestPlan).StartDate = (assistantItem as AssistentItemModel<DateTime>).EnteredValue));
        }

        private AssistentPlan<bool> CreateIsEndDateActiveAssistantPlan()
        {
            return new AssistentPlan<bool>(
                new AssistentItemModel<bool>(AssistentItemType.Boolean,
                    _localization.Strings.GetParticularString("AddTestPlanAssistant", "Select if the test plan should have a end date"),
                    _localization.Strings.GetParticularString("AddTestPlanAssistant", "End date active"),
                    _defaultTestPlan?.IsEndDateEnabled ?? false,
                    (resultObject, assistantItem) => (resultObject as TestPlan).IsEndDateEnabled = (assistantItem as AssistentItemModel<bool>).EnteredValue));
        }

        private AssistentPlan<DateTime> CreateEndDateDateAssistantPlan()
        {
            return new AssistentPlan<DateTime>(
                new AssistentItemModel<DateTime>(AssistentItemType.Date,
                    _localization.Strings.GetParticularString("AddTestPlanAssistant", "Choose the end date of the test plan"),
                    _localization.Strings.GetParticularString("AddTestPlanAssistant", "End date"),
                    _defaultTestPlan?.StartDate ?? DateTime.Today.AddDays(1),
                    (resultObject, assistantItem) => (resultObject as TestPlan).EndDate = (assistantItem as AssistentItemModel<DateTime>).EnteredValue));
        }
        #endregion
    }
}
