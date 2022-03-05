using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core.Diffs
{
    public class TestLevelSetDiff
    {
        public TestLevelSet Old { get; set; }
        public TestLevelSet New { get; set; }
        public HistoryComment Comment { get; set; }
        public User User { get; set; }
    }
}
