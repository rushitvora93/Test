using Core.UseCases.Communication.DataGate;
using NUnit.Framework;

namespace FrameworksAndDrivers.DataGate.Test
{
    class DataGateFileGeneratorTest
    {
        [Test]
        public void GeneratingContentCreatesTag()
        {
            var tag = new Content(new ElementName("testtag"), "testvalue");
            var model = new SemanticModel(tag);
            var generator = new DataGateFileGenerator();
            generator.Accept(model);
            Assert.AreEqual("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n<testtag>testvalue</testtag>\r\n", generator.DataGateFileContent());
        }

        [Test]
        public void GeneratingOtherContentCreatesTag()
        {
            var tag = new Content(new ElementName("ajdoeav"), "veioawmodf");
            var model = new SemanticModel(tag);
            var generator = new DataGateFileGenerator();
            generator.Accept(model);
            Assert.AreEqual("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n<ajdoeav>veioawmodf</ajdoeav>\r\n", generator.DataGateFileContent());
        }

        [Test]
        public void GeneratingHiddenContentCreatesNothing()
        {
            var tag = new HiddenContent(new ElementName("testtag"), "testvalue");
            var model = new SemanticModel(tag);
            var generator = new DataGateFileGenerator();
            generator.Accept(model);
            Assert.AreEqual("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n", generator.DataGateFileContent());
        }

        [Test]
        public void GeneratingContainerCreatesContainerTag()
        {
            var tag = new Container(new ElementName("testtag"));
            var model = new SemanticModel(tag);
            var generator = new DataGateFileGenerator();
            generator.Accept(model);
            Assert.AreEqual("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n<testtag>\r\n</testtag>\r\n", generator.DataGateFileContent());
        }

        [Test]
        public void GeneratingOtherContainerCreatesContainerTag()
        {
            var tag = new Container(new ElementName("aieovmakd"));
            var model = new SemanticModel(tag);
            var generator = new DataGateFileGenerator();
            generator.Accept(model);
            Assert.AreEqual("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n<aieovmakd>\r\n</aieovmakd>\r\n", generator.DataGateFileContent());
        }

        [Test]
        public void GeneratingContainerWithContentCreatesStructure()
        {
            var tag = new Container(new ElementName("testtag")) {new Content(new ElementName("subcontent"), "wow")};
            var subcontainer = new Container(new ElementName("subcontainer"))
            {
                new Content(new ElementName("bla"), "ajdfoa"),
                new Content(new ElementName("ajkoma"), "rekovssd")
            };
            tag.Add(subcontainer);
            var model = new SemanticModel(tag);
            var generator = new DataGateFileGenerator();
            generator.Accept(model);
            var expected =
                "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n" +
                "<testtag>\r\n" +
                "<subcontent>wow</subcontent>\r\n" +
                "<subcontainer>\r\n" +
                "<bla>ajdfoa</bla>\r\n" +
                "<ajkoma>rekovssd</ajkoma>\r\n" +
                "</subcontainer>\r\n" +
                "</testtag>\r\n";
            Assert.AreEqual(expected, generator.DataGateFileContent());
        }
    }
}
