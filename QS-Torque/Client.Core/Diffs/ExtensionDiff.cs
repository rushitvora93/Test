using Core.Entities;

namespace Client.Core.Diffs
{
    public class ExtensionDiff
    {
        public User User { get; set; }
        public HistoryComment Comment { get; set; }
        public Extension OldExtension { get; private set; }
        public Extension NewExtension { get; private set; }

        public ExtensionDiff(User user, HistoryComment comment, Extension oldExtension, Extension newExtension)
        {
            User = user;
            Comment = comment;
            OldExtension = oldExtension;
            NewExtension = newExtension;
        }

    }
}
