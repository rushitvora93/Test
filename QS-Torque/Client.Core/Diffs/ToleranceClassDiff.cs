using Core.Entities;

namespace Core.UseCases
{
    public class ToleranceClassDiff
    {

        public User User { get; private set; }
        public HistoryComment Comment { get; private set; }
        public ToleranceClass OldToleranceClass { get; private set; }
        public ToleranceClass NewToleranceClass { get; private set; }

        public ToleranceClassDiff(User user, HistoryComment comment, ToleranceClass oldToleranceClass,
            ToleranceClass newToleranceClass)
        {
            User = user;
            Comment = comment;
            OldToleranceClass = oldToleranceClass;
            NewToleranceClass = newToleranceClass;
        }

    }
}