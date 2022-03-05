using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_TestProjekt.TestModel
{
    public class MeasurementPoint
    {
        private string number = "";
        private string description = "";
        private ControlledBy controlledBy = ControlledBy.Torque;
        private double setPointTorque = 4;
        private ToleranceClass toleranceClassTorque = new ToleranceClass();
        private double minTorque = 3;
        private double maxTorque = 5;
        private double thresholdTorque = 2;
        private double setPointAngle = 15;
        private ToleranceClass toleranceClassAngle = new ToleranceClass();
        private double minAngle = 10;
        private double maxAngle = 20;
        private string configurableField = "";
        private string configurableField2 = "";
        private bool configurableField3 = false;
        private string comment = "";
        private List<string> listParentFolder = new List<string>();

        public string Number { get => number; set => number = value; }
        public string Description { get => description; set => description = value; }
        public ControlledBy ControlledBy { get => controlledBy; set => controlledBy = value; }
        public double SetPointTorque { get => setPointTorque; set => setPointTorque = value; }
        public ToleranceClass ToleranceClassTorque { get => toleranceClassTorque; set => toleranceClassTorque = value; }
        public double MinTorque { get => minTorque; set => minTorque = value; }
        public double MaxTorque { get => maxTorque; set => maxTorque = value; }
        public double ThresholdTorque { get => thresholdTorque; set => thresholdTorque = value; }
        public double SetPointAngle { get => setPointAngle; set => setPointAngle = value; }
        public ToleranceClass ToleranceClassAngle { get => toleranceClassAngle; set => toleranceClassAngle = value; }
        public double MinAngle { get => minAngle; set => minAngle = value; }
        public double MaxAngle { get => maxAngle; set => maxAngle = value; }
        public string ConfigurableField { get => configurableField; set => configurableField = value; }
        public string ConfigurableField2 { get => configurableField2; set => configurableField2 = value; }
        public bool ConfigurableField3 { get => configurableField3; set => configurableField3 = value; }
        public string Comment { get => comment; set => comment = value; }
        public List<string> ListParentFolder { get => listParentFolder; set => listParentFolder = value; }

        public MeasurementPoint() { }

        public MeasurementPoint(string number, string description, ControlledBy controlledBy, double setPointTorque, ToleranceClass toleranceClassTorque, double minTorque, double maxTorque, double thresholdTorque, double setPointAngle, ToleranceClass toleranceClassAngle, double minAngle, double maxAngle, string configurableField, string configurableField2, bool configurableField3, string comment, List<string> listParentFolder)
        {
            Number = number;
            Description = description;
            ControlledBy = controlledBy;
            SetPointTorque = setPointTorque;
            ToleranceClassTorque = toleranceClassTorque;
            MinTorque = minTorque;
            MaxTorque = maxTorque;
            ThresholdTorque = thresholdTorque;
            SetPointAngle = setPointAngle;
            ToleranceClassAngle = toleranceClassAngle;
            MinAngle = minAngle;
            MaxAngle = maxAngle;
            ConfigurableField = configurableField;
            ConfigurableField2 = configurableField2;
            ConfigurableField3 = configurableField3;
            Comment = comment;
            ListParentFolder = listParentFolder;
        }
        public List<string> GetParentListWithMp()
        {
            List<string> parentFolderWithMP = new List<string>(listParentFolder);
            parentFolderWithMP.Add(GetMpTreeName());
            return parentFolderWithMP;
        }
        public string GetMpTreeName()
        {
            return string.Format("{0} - {1}", Number, Description);
        }
        public double GetMinTorqueFromClass()
        {
            if(ToleranceClassTorque.Name == "freie Eingabe")
            {
                return MinTorque;
            }
            if (ToleranceClassTorque.IsRelative)
            {
                return (100.0 - ToleranceClassTorque.Lower) * SetPointTorque;
            }
            else
            {
                return SetPointTorque - ToleranceClassTorque.Lower;
            }
        }
        public double GetMaxTorqueFromClass()
        {
            if (ToleranceClassTorque.Name == "freie Eingabe")
            {
                return MaxTorque;
            }
            if (ToleranceClassTorque.IsRelative)
            {
                return (100.0 + ToleranceClassTorque.Upper) * SetPointTorque;
            }
            else
            {
                return SetPointTorque + ToleranceClassTorque.Upper;
            }
        }
        public double GetMinAngleFromClass()
        {
            if (ToleranceClassAngle.Name == "freie Eingabe")
            {
                return MinAngle;
            }
            if (ToleranceClassAngle.IsRelative)
            {
                return (100.0 - ToleranceClassAngle.Lower) * SetPointAngle;
            }
            else
            {
                return SetPointAngle - ToleranceClassAngle.Lower;
            }
        }
        public double GetMaxAngleFromClass()
        {
            if (ToleranceClassAngle.Name == "freie Eingabe")
            {
                return MaxAngle;
            }
            if (ToleranceClassAngle.IsRelative)
            {
                return (100.0 + ToleranceClassAngle.Upper) * SetPointAngle;
            }
            else
            {
                return SetPointAngle + ToleranceClassAngle.Upper;
            }
        }
    }
    public enum ControlledBy
    {
        Torque,
        Angle
    }

}
