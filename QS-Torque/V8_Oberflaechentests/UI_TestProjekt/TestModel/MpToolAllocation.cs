using System;

namespace UI_TestProjekt.TestModel
{
    public class MpToolAllocation
    {
        private MeasurementPoint mp = new MeasurementPoint();
        private Tool tool = new Tool();
        private string toolUsage = "";

        private DateTime nextTestDateMfu = new DateTime();
        private string nextTestShiftMfu = "";
        private DateTime nextTestDateChk = new DateTime();
        private string nextTestShiftChk = "";
        private bool areConditionsCreated = false;
        private ToolControlConditions toolControlConditions;

        public MeasurementPoint Mp { get => mp; set => mp = value; }
        public Tool Tool { get => tool; set => tool = value; }
        public string ToolUsage { get => toolUsage; set => toolUsage = value; }
        public DateTime NextTestDateMfu { get => nextTestDateMfu; set => nextTestDateMfu = value; }
        public string NextTestShiftMfu { get => nextTestShiftMfu; set => nextTestShiftMfu = value; }
        public DateTime NextTestDateChk { get => nextTestDateChk; set => nextTestDateChk = value; }
        public string NextTestShiftChk { get => nextTestShiftChk; set => nextTestShiftChk = value; }
        public bool AreConditionsCreated { get => areConditionsCreated; set => areConditionsCreated = value; }

        public ToolControlConditions ToolControlConditions { get => toolControlConditions; set => toolControlConditions = value; }

        public MpToolAllocation(MeasurementPoint mp, Tool tool)
        {
            Mp = mp;
            Tool = tool;
        }

        public MpToolAllocation(MeasurementPoint mp, Tool tool, string toolUsage)
        {
            Mp = mp;
            Tool = tool;
            ToolUsage = toolUsage;
        }

        public MpToolAllocation(MeasurementPoint mp, Tool tool, string toolUsage, DateTime nextTestDateMfu, string nextTestShiftMfu, DateTime nextTestDateChk, string nextTestShiftChk, ToolControlConditions toolControlConditions) : this(mp, tool, toolUsage)
        {
            NextTestDateMfu = nextTestDateMfu;
            NextTestShiftMfu = nextTestShiftMfu;
            NextTestDateChk = nextTestDateChk;
            NextTestShiftChk = nextTestShiftChk;
            ToolControlConditions = toolControlConditions;
        }

        public string GetToolIdentString()
        {
            return string.Format("{0},{1}", Tool.SerialNumber, Tool.InventoryNumber);
        }

        public static class AllocatedToolsListHeaderStrings
        {
            public const string SerialNumber = "Serial number";
            public const string InventoryNumber = "Inventory number";
            public const string ToolUsage = "Tool usage";
            public const string Id = "Id";
            public const string TestLevelNumberMfu = "TestLevelNumberMfu";
            public const string TestLevelNumberChk = "TestLevelNumberChk";
            public const string NextTestDateMfu = "NextTestDateMfu";
            public const string NextTestShiftMfu = "NextTestShiftMfu";
            public const string NextTestDateChk = "NextTestDateChk";
            public const string NextTestShiftChk = "NextTestShiftChk";
            public const string StartDateMfu = "StartDateMfu";
            public const string StartDateChk = "StartDateChk";
            public const string TestOperationActiveMfu = "TestOperationActiveMfu";
            public const string TestOperationActiveChk = "TestOperationActiveChk";
        }
    }
}
