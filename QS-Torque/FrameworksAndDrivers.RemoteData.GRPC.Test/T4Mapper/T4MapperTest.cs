using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using NUnit.Framework;
using T4Mapper;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.T4Mapper
{
    public class T4MapperTests
    {

        [Test]
        public void CreateInstance()
        {
            var mapper = new Mapper();
        }

        [Test]
        public void DirectMapping()
        {
            var mapper = new Mapper();

            var source = new SourceStub();
            source.ITEM = 5;
            source.UNAME = "test";

            var result = mapper.DirectPropertyMapping(source);

            Assert.AreEqual(result.count, 5);
            Assert.AreEqual(result.name, "test");
        }
    }
}
