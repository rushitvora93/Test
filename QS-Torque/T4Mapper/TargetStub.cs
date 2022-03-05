namespace T4Mapper
{
    public class TargetStub
    {
        public TargetStub()
        {
        }

        public TargetStub(int param1, string pname, int pcount, TargetStub subitem)
        {
            count = pcount;
            name = pname;
        }

        public int count { get; set; }
        public string name { get; set; }
    }
}
