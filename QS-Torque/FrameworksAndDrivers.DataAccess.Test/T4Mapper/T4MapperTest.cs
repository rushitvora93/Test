using NUnit.Framework;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using T4Mapper;

namespace FrameworksAndDrivers.DataAccess.Test.T4Mapper
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
