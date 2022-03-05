namespace Core.Entities
{
    public class ClassicMfuTestValue 
    {
        public GlobalHistoryId Id { get; set; }
        public long Position { get; set; }
        public double ValueUnit1 { get; set; }
        public double ValueUnit2 { get; set; }
        public ClassicMfuTest MfuTest { get; set; }
        public double ControlledByValue => MfuTest.ControlledByUnitId == MfuTest.Unit1Id ? ValueUnit1 : ValueUnit2; 
    }
}
