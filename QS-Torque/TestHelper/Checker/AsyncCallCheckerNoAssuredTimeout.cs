using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TestHelper.Checker
{
    public class AsyncCallCheckerNoAssuredTimeout
    {
        public static void OnCallCheck(Task<bool> called, int wrongMethodCallCount, Action onCall)
        {
            var checker = new AsyncCallCheckerNoAssuredTimeout();
            checker.OnCalled = onCall;
            checker.Check(called, wrongMethodCallCount);
        }

        public Action OnCalled = () => Assert.Fail("Unexpected Function called");
        public Action OnError = () => Assert.Fail("Unexpected Error encountered");
        public Action OnTimeOut = () => Assert.Fail("Unexpected Timeout encountered");

        public void Check(Task called, int wrongMethodCallCount)
        {
            const int timeoutIndex = -1;
            int result = Task.WaitAny(new Task[] { called}, 1000);
            if(result == timeoutIndex)
            {
                OnTimeOut();
            }

            if (wrongMethodCallCount > 0)
            {
                OnError();
            }
            OnCalled();
        }
    }
}
