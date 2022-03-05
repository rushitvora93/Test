using Core.Entities;
using NUnit.Framework;

namespace Core.Test.Entities
{
    class TestResultTest
    {
        [TestCase(0, true)]
        [TestCase(1, false)]
        [TestCase(2, false)]
        public void TestResultGetsCorrectIsIo(long longValue, bool isIoResultValue)
        {
            var testResult = new TestResult(longValue);
            Assert.AreEqual(isIoResultValue, testResult.IsIo);
        }
    }
}
