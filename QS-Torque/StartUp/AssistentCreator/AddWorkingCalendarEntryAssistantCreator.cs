using System;
using System.Collections.Generic;
using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Localization;

namespace StartUp.AssistentCreator
{
    class AddWorkingCalendarEntryAssistantCreator
    {
        private LocalizationWrapper _localization;
        private DateTime? _defaultDate;
        private WorkingCalendarEntryType? _defaultType;
        private bool _areSaturdaysFree;
        private bool _areSundaysFree;
        private Func<DateTime, WorkingCalendarEntry> _getExistingEntryForDate;

        public AddWorkingCalendarEntryAssistantCreator(LocalizationWrapper localization, bool areSaturdaysFree, bool areSundaysFree, Func<DateTime, WorkingCalendarEntry> getExistingEntryForDate, DateTime? defaultDate = null, WorkingCalendarEntryType? defaultType = null)
        {
            _localization = localization;
            _areSaturdaysFree = areSaturdaysFree;
            _areSundaysFree = areSundaysFree;
            _getExistingEntryForDate = getExistingEntryForDate;
            _defaultDate = defaultDate;
            _defaultType = defaultType;
        }


        public AssistentView CreateAddWorkingCalendarEntryAssistant()
        {
            AssistentView assistantView =
                new AssistentView(
                    _localization.Strings.GetParticularString("AddWorkingCalendarEntryAssistant", "Add new working calendar entry"));

            var datePlan = CreateDateAssistantPlan();
            var typePlan = CreateTypeAssistantPlan(assistantView);
            var repititionPlan = CreateRepetitionAssistantPlan(assistantView);

            var parentPlan = new ParentAssistentPlan(new List<ParentAssistentPlan>()
            {
                datePlan,
                CreateDescriptionAssistantPlan(),
                repititionPlan,
                typePlan
            });

            typePlan.AssistentItem.ErrorText = _localization.Strings.GetParticularString("AddWorkingCalendarEntryAssistant", "Holidays are just allowed during the week and extra shifts are just allowed on general free weekends");
            typePlan.AssistentItem.ErrorCheck = x =>
            {
                if(repititionPlan.AssistentItem.EnteredValue == WorkingCalendarEntryRepetition.Yearly) { return false; }

                // Check just for single calendar entries
                return (_areSaturdaysFree && datePlan.AssistentItem.EnteredValue.DayOfWeek == DayOfWeek.Saturday && (x as AssistentItemModel<WorkingCalendarEntryType>).EnteredValue == WorkingCalendarEntryType.Holiday) ||
                       (_areSundaysFree && datePlan.AssistentItem.EnteredValue.DayOfWeek == DayOfWeek.Sunday && (x as AssistentItemModel<WorkingCalendarEntryType>).EnteredValue == WorkingCalendarEntryType.Holiday) ||
                       (datePlan.AssistentItem.EnteredValue.DayOfWeek != DayOfWeek.Saturday && datePlan.AssistentItem.EnteredValue.DayOfWeek != DayOfWeek.Sunday && (x as AssistentItemModel<WorkingCalendarEntryType>).EnteredValue == WorkingCalendarEntryType.ExtraShift) ||
                       (!_areSaturdaysFree && datePlan.AssistentItem.EnteredValue.DayOfWeek == DayOfWeek.Saturday && (x as AssistentItemModel<WorkingCalendarEntryType>).EnteredValue == WorkingCalendarEntryType.ExtraShift) ||
                       (!_areSundaysFree && datePlan.AssistentItem.EnteredValue.DayOfWeek == DayOfWeek.Sunday && (x as AssistentItemModel<WorkingCalendarEntryType>).EnteredValue == WorkingCalendarEntryType.ExtraShift);
            };

            assistantView.SetParentPlan(parentPlan);

            return assistantView;
        }


        #region Special AssistentPlanCreator
        private AssistentPlan<DateTime> CreateDateAssistantPlan()
        {
            return new AssistentPlan<DateTime>(
                new AssistentItemModel<DateTime>(AssistentItemType.Date,
                    _localization.Strings.GetParticularString("AddWorkingCalendarEntryAssistant", "Enter a date for the calendar entry"),
                    _localization.Strings.GetParticularString("AddWorkingCalendarEntryAssistant", "Date"),
                    _defaultDate ?? DateTime.Today,
                    (resultObject, assistantItem) => (resultObject as WorkingCalendarEntry).Date = (assistantItem as AssistentItemModel<DateTime>).EnteredValue,
                    errorCheck: x => (x as AssistentItemModel<DateTime>).EnteredValue < new DateTime(1900, 1, 1),
                    errorText: _localization.Strings.GetParticularString("AddWorkingCalendarEntryAssistant", "The working calendar entry has to be after 1/1/1900"),
                    warningCheck: x => _getExistingEntryForDate((x as AssistentItemModel<DateTime>).EnteredValue.Date) != null,
                    warningText: GetWarningTextForDateAssistantPlan,
                    minDate: new DateTime(1900, 1, 1)));
        }

        private string GetWarningTextForDateAssistantPlan(AssistentItemModel item)
        {
            var existingItem = _getExistingEntryForDate((item as AssistentItemModel<DateTime>).EnteredValue);
            return _localization.Strings.GetParticularString("AddWorkingCalendarEntryAssistant", "An entry exists already at this date. If you proceed, this entry will be overwritten.") + "\n\n" +
                   existingItem.Description.ToDefaultString() + " (" +
                   (existingItem.Type == WorkingCalendarEntryType.Holiday ? _localization.Strings.GetParticularString("Working calendar", "Workfree day") : _localization.Strings.GetParticularString("Working calendar", "Extra shift")) + ", " +
                   (existingItem.Repetition == WorkingCalendarEntryRepetition.Once ? _localization.Strings.GetParticularString("Working calendar", "Once") : _localization.Strings.GetParticularString("Working calendar", "Yearly")) + ")";
        }

        private AssistentPlan<string> CreateDescriptionAssistantPlan()
        {
            return new AssistentPlan<string>(
                new AssistentItemModel<string>(AssistentItemType.Text,
                    _localization.Strings.GetParticularString("AddWorkingCalendarEntryAssistant", "Enter a description for the calendar entry"),
                    _localization.Strings.GetParticularString("AddWorkingCalendarEntryAssistant", "Description"),
                    "",
                    (resultObject, assistantItem) => (resultObject as WorkingCalendarEntry).Description = new WorkingCalendarEntryDescription((assistantItem as AssistentItemModel<string>).EnteredValue),
                    errorCheck: assistantItem => string.IsNullOrWhiteSpace((assistantItem as AssistentItemModel<string>).EnteredValue),
                    errorText: _localization.Strings.GetParticularString("AddWorkingCalendarEntryAssistant", "You have to enter a description for the calendar entry"),
                    maxLengthText:30));
        }

        private ListAssistentPlan<WorkingCalendarEntryType> CreateTypeAssistantPlan(AssistentView assistantView)
        {
            return new ListAssistentPlan<WorkingCalendarEntryType>(
                new ListAssistentItemModel<WorkingCalendarEntryType>(
                    assistantView.Dispatcher,
                    new List<WorkingCalendarEntryType>() { WorkingCalendarEntryType.Holiday, WorkingCalendarEntryType.ExtraShift },
                    _localization.Strings.GetParticularString("AddWorkingCalendarEntryAssistant", "Choose the type of the calendar entry"),
                    _localization.Strings.GetParticularString("AddWorkingCalendarEntryAssistant", "Type"),
                    _defaultType ?? WorkingCalendarEntryType.Holiday,
                    (resultObject, assistantItem) => (resultObject as WorkingCalendarEntry).Type = (assistantItem as ListAssistentItemModel<WorkingCalendarEntryType>).EnteredValue,
                    null,
                    type =>
                    {
                        switch (type)
                        {
                            case WorkingCalendarEntryType.Holiday: return _localization.Strings.GetParticularString("AddWorkingCalendarEntryAssistant", "Holiday");
                            case WorkingCalendarEntryType.ExtraShift: return _localization.Strings.GetParticularString("AddWorkingCalendarEntryAssistant", "Extra shift");
                            default: return "";
                        }
                    },
                    () => { }));
        }

        private ListAssistentPlan<WorkingCalendarEntryRepetition> CreateRepetitionAssistantPlan(AssistentView assistantView)
        {
            return new ListAssistentPlan<WorkingCalendarEntryRepetition>(
                new ListAssistentItemModel<WorkingCalendarEntryRepetition>(
                    assistantView.Dispatcher,
                    new List<WorkingCalendarEntryRepetition>() { WorkingCalendarEntryRepetition.Once, WorkingCalendarEntryRepetition.Yearly },
                    _localization.Strings.GetParticularString("AddWorkingCalendarEntryAssistant", "Choose a repetition for the calendar entry"),
                    _localization.Strings.GetParticularString("AddWorkingCalendarEntryAssistant", "Repetition"),
                    WorkingCalendarEntryRepetition.Once,
                    (resultObject, assistantItem) => (resultObject as WorkingCalendarEntry).Repetition = (assistantItem as ListAssistentItemModel<WorkingCalendarEntryRepetition>).EnteredValue,
                    null,
                    type =>
                    {
                        switch (type)
                        {
                            case WorkingCalendarEntryRepetition.Once: return _localization.Strings.GetParticularString("AddWorkingCalendarEntryAssistant", "Once");
                            case WorkingCalendarEntryRepetition.Yearly: return _localization.Strings.GetParticularString("AddWorkingCalendarEntryAssistant", "Yearly");
                            default: return "";
                        }
                    },
                    () => { }));
        }
        #endregion
    }
}
