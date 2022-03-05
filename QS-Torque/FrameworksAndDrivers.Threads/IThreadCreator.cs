using System;

namespace FrameworksAndDrivers.Threads
{
    public interface IThreadCreator
    {
        void Run(Action action);    
    }
}