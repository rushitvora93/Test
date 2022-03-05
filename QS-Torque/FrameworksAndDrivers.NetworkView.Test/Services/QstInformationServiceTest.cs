using BasicTypes;
using NUnit.Framework;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    class QstInformationServiceTest
    {
        [Test]
        public void LoadServerVersionReturnsVersion()
        {
            var service = new NetworkView.Services.QstInformationService(null);
            var result = service.LoadServerVersion(new NoParams(), null);
            Assert.IsNotNull(result?.Result);
            Assert.IsTrue(result.Result.Value.Length > 0);
        }
    }
}
