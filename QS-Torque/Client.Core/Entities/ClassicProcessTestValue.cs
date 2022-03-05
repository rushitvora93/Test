using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Client.Core.Entities
{
    public class ClassicProcessTestValue
    {
        public GlobalHistoryId Id { get; set; }
        public long Position { get; set; }
        public double ValueUnit1 { get; set; }
        public double ValueUnit2 { get; set; }
        public ClassicProcessTest ProcessTest { get; set; }
        public double ControlledByValue => ProcessTest.ControlledByUnitId == ProcessTest.Unit1Id ? ValueUnit1 : ValueUnit2;
    }
}
