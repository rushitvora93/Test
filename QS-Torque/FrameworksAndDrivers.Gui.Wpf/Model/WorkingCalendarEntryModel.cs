using System;
using Core.Entities;
using InterfaceAdapters;

namespace FrameworksAndDrivers.Gui.Wpf.Model
{
    public class WorkingCalendarEntryModel : BindableBase
    {
        public WorkingCalendarEntry Entity { get; set; }

        public DateTime Date
        {
            get => Entity.Date;
            set => Entity.Date = value;
        }

        public string Description
        {
            get => Entity.Description.ToDefaultString();
            set => Entity.Description = new WorkingCalendarEntryDescription(value);
        }

        public WorkingCalendarEntryType Type
        {
            get => Entity.Type;
            set => Entity.Type = value;
        }

        public WorkingCalendarEntryRepetition Repetition
        {
            get => Entity.Repetition;
            set => Entity.Repetition = value;
        }


        public WorkingCalendarEntryModel(WorkingCalendarEntry entity)
        {
            Entity = entity ?? new WorkingCalendarEntry();
            RaisePropertyChanged(null);
        }

        public static WorkingCalendarEntryModel GetModelFor(WorkingCalendarEntry entity)
        {
            return entity != null ? new WorkingCalendarEntryModel(entity) : null;
        }
    }
}
