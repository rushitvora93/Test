using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Entities
{
    public class ClassicProcessTestValue
    {
        public GlobalHistoryId Id { get; set; }
        public long Position { get; set; }
        public double ValueUnit1 { get; set; }
        public double ValueUnit2 { get; set; }
    }
}
