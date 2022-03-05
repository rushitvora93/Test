using Core.Entities;

namespace Core.UseCases
{
    public class ToolModelDiff
    {
        public ToolModelDiff(User user, HistoryComment comment, ToolModel oldToolModel, ToolModel newToolModel)
        {
            User = user;
            Comment = comment;
            OldToolModel = oldToolModel;
            NewToolModel = newToolModel;
        }

        public ToolModelDiff()
        {

        }
        public User User { get; set; }
        public HistoryComment Comment { get; set; }
        
        public ToolModel OldToolModel { get; set; }
        public ToolModel NewToolModel { get; set; }
    }
}
