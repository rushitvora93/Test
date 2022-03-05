using NUnit.Framework;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Behaviors
{
    [SetUpFixture]
    public class BehaviorSetUpFixture
    {
        public static Application TestApplication;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            TestApplication = new Application();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // Do nothing
        }
    }
}
