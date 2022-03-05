using System;
using System.Threading.Tasks;

namespace FrameworksAndDrivers.Threads
{
    public class HumbleTaskThreadCreator : IThreadCreator
    {
        public void Run(Action action)
        {
            Task.Run(action);
        }
    }
}