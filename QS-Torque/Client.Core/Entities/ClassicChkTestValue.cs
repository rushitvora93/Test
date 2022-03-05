namespace Core.Entities
{
    public class ClassicChkTestValue
    {
        public GlobalHistoryId Id { get; set; }
        public long Position { get; set; }
        public double ValueUnit1 { get; set; }
        public double ValueUnit2 { get; set; }
        public ClassicChkTest ChkTest { get; set; }
        public double ControlledByValue => ChkTest.ControlledByUnitId == ChkTest.Unit1Id ? ValueUnit1 : ValueUnit2;
    }
}
