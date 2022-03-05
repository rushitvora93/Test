using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace TestHelper.Checker
{
	public class AsyncCallChecker
	{
		public static void OnCallCheck(Task called, Task error, Action OnCall)
		{
			var checker = new AsyncCallChecker();
			checker.OnCalled = OnCall;
			checker.Check(called, error);
		}

		public static void OnErrorCheck(Task called, Task error, Action OnError)
		{
			var checker = new AsyncCallChecker();
			checker.OnError = OnError;
			checker.Check(called, error);
		}

		public Action OnCalled = () => Assert.Fail("Unexpected Function called");
		public Action OnError = () => Assert.Fail("Unexpected Error encountered");
		public Action OnTimeOut = () => Assert.Fail("Unexpected Timeout encountered");

		public void Check(Task called, Task error)
		{
			const int timeoutIndex = -1;
			const int calledIndex = 0;
			const int errorIndex = 1;
			int result = Task.WaitAny(new Task[] { called, error }, 1000);
			switch(result)
			{
				case timeoutIndex:
					OnTimeOut();
					break;

				case calledIndex:
					OnCalled();
					break;

				case errorIndex:
					OnError();
					break;

				default:
					Assert.Fail("Something went horribly wrong");
					break;
			}
		}
	}
}
