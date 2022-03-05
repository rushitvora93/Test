namespace Core.Entities
{
    public class TestResult
    {
        public TestResult(long result)
        {
            _result = result;
        }
        private readonly long _result;

        public bool IsIo => _result == 0;

        public long LongValue => _result;
    }
}
