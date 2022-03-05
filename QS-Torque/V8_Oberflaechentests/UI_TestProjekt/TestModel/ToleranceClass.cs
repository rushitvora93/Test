using System.Collections.Generic;

namespace UI_TestProjekt.TestModel
{
    public class ToleranceClass
    {
        private string name = "freie Eingabe";
        private bool isRelative = true;
        private bool isSymmetrical = false;
        private double lower = 0;
        private double upper = 0;

        private List<string> referencedMps = new List<string>();
        private List<string> referencedMpToolAssignements = new List<string>();

        public string Name { get => name; set => name = value; }
        public bool IsRelative { get => isRelative; set => isRelative = value; }
        public bool IsSymmetrical { get => isSymmetrical; set => isSymmetrical = value; }
        public double Lower { get => lower; set => lower = value; }
        public double Upper { get => upper; set => upper = value; }
        public List<string> ReferencedMps { get => referencedMps; set => referencedMps = value; }
        public List<string> ReferencedMpToolAssignements { get => referencedMpToolAssignements; set => referencedMpToolAssignements = value; }

        public ToleranceClass(string name, bool isRelative, bool isSymmetrical, double lower, double upper, List<string> referencedMps, List<string> referencedMpToolAssignements)
        {
            Name = name;
            IsRelative = isRelative;
            IsSymmetrical = isSymmetrical;
            Lower = lower;
            Upper = upper;
            ReferencedMps = referencedMps;
            ReferencedMpToolAssignements = referencedMpToolAssignements;
        }

        public ToleranceClass(string name, bool isRelative, bool isSymmetrical, double lower, double upper) : this(name, isRelative, isSymmetrical, lower, upper, new List<string>(), new List<string>()) { }

        public ToleranceClass() { }
    }
}
