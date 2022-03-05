using Server.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Diffs
{
    public class ExtensionDiff
    {
        public User User { get; private set; }
        public HistoryComment Comment { get; private set; }
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
