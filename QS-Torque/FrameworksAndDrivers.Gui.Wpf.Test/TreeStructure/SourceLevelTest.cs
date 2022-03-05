using FrameworksAndDrivers.Gui.Wpf.TreeStructure;
using NUnit.Framework;
using TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.TreeStructure
{
    class SourceLevelTest
    {
        RootLevel<string> _rootLevel;
        ITreeLevelMock<string> _subLevel;

        const string RootNodeHeader = "ghfjeoghjroe";


        [SetUp]
        public void SourceLevelSetUp()
        {
            _subLevel = new ITreeLevelMock<string>();
            _rootLevel = new RootLevel<string>(() => RootNodeHeader, new NullLocalizationWrapper())
            {
                SubLevel = _subLevel
            };
        }


        [Test, RequiresThread(System.Threading.ApartmentState.STA)]
        public void AddTest()
        {
            var item = "ghturdklvmbnghrieolgjhgj";

            _rootLevel.Add(item);

            Assert.IsTrue(_subLevel.WasAddInvoked);
            Assert.AreEqual(item, _subLevel.AddParameterItem);
            Assert.IsNotNull(_subLevel.AddParameterParent);
            Assert.AreEqual(RootNodeHeader, _subLevel.AddParameterParent.Header);
        }
    }
}
