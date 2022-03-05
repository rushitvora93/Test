using System;
using FrameworksAndDrivers.Threads;

namespace TestHelper.Mock
{
    public class MockThreadCreator : IThreadCreator
    {
        public void Run(Action action)
        {
            action.Invoke();
        }
    }
}