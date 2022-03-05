using Client.Core.Entities;
using Core.Entities;

namespace Client.Core.Diffs
{
    public class ProcessControlConditionDiff
    {
        public ProcessControlConditionDiff(User byUser, HistoryComment comment, ProcessControlCondition oldCondition, ProcessControlCondition newCondition)
        {
            User = byUser;
            Comment = comment;
            _oldCondition = oldCondition;
            _newCondition = newCondition;
        }

        public ProcessControlCondition GetNewProcessControlCondition()
        {
            return _newCondition;
        }

        public ProcessControlCondition GetOldProcessControlCondition()
        {
            return _oldCondition;
        }



        public User User { get; set; }
        public HistoryComment Comment { get; set; }
        private ProcessControlCondition _oldCondition;
        private ProcessControlCondition _newCondition;
    }
}
