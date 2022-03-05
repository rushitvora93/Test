using Core.Entities;

namespace Core.UseCases
{
    public class ToolDiff
    {
        public User User { get; set; }
        public HistoryComment Comment { get; set; }
        public Tool OldTool { get; set; }
        public Tool NewTool { get; set; }
        public ToolDiff(User user, HistoryComment comment, Tool oldTool, Tool newTool)
        {
            User = user;
            Comment = comment;
            OldTool = oldTool;
            NewTool = newTool;
        }

        public ToolDiff(User user, Tool oldTool, Tool newTool)
        {
            User = user;
            OldTool = oldTool;
            NewTool = newTool;
        }
    }
}
