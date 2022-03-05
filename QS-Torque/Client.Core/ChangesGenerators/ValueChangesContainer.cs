using Client.Core.Enums;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core.ChangesGenerators
{
    public class ValueChangesContainer
    {
        public DateTime Timestamp { get; set; }
        public DiffType Type { get; set; }
        public User User { get; set; }
        public HistoryComment Comment { get; set; }
        public List<SingleValueChange> Changes { get; set; }
    }
}
