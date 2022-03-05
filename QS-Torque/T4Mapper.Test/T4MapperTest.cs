using NUnit.Framework;

namespace T4Mapper.Test
{
    public class T4MapperTests
    {
        MappigDefinitionParser parser;

        [SetUp]
        public void Setup()
        {
            parser = new MappigDefinitionParser();
        }

        [TestCase("Map", "Map")]
        [TestCase("Bla", "Bla")]
        [TestCase("\r\n  \tBla \t   ", "Bla")]
        public void ParseAstRootSimplifiedAction(string mapDefinition, string expectedResult)
        {
            var result = parser.Parse(mapDefinition);
            Assert.AreEqual(result.action, expectedResult);
        }

        [TestCase("(Map)", "Map")]
        [TestCase("(Test)", "Test")]
        [TestCase("\t  (   Test     \t)", "Test")]
        public void ParseAstRootAction(string mapDefinition, string expectedResult)
        {
            var result = parser.Parse(mapDefinition);
            Assert.AreEqual(result.action, expectedResult);
        }

        [TestCase("(Map Test)", "Test")]
        [TestCase("(Map Bla)", "Bla")]
        [TestCase("(Map (Bla))", "Bla")]
        public void ParseAstWithSubnodes(string mapDefinition, string expectedResult)
        {
            var result = parser.Parse(mapDefinition);
            Assert.AreEqual(result.subnodes[0].action, expectedResult);
        }

        [TestCase("(Map Bla Test)", "Bla", "Test")]
        public void ParseAstWithMoreSubnodes(string mapDefinition, string expectedResult1, string expectedResult2)
        {
            var result = parser.Parse(mapDefinition);
            Assert.AreEqual(result.subnodes[0].action, expectedResult1);
            Assert.AreEqual(result.subnodes[1].action, expectedResult2);
        }

        [Test]
        public void ParseSubnodesInSubnodes()
        {
            var mapDefinition = "(Map (Options Test)";
            var result = parser.Parse(mapDefinition);
            Assert.AreEqual(result.subnodes[0].subnodes[0].action, "Test");
        }

        [Test]
        public void AstWithSubnodesBehindSubnodesWithSubnodes()
        {
            var mapDefinition = "(Map (Options Test) (Parameter COUNT ITEM))";
            var result = parser.Parse(mapDefinition);
            Assert.AreEqual(result.subnodes[1].subnodes[1].action, "ITEM");
        }

        [Test]
        public void ParseAstRootWithoutSubnodesHasNoSubnodes()
        {
            var mapDefinition = "(Map)";
            var result = parser.Parse(mapDefinition);
            Assert.AreEqual(result.subnodes.Count, 0);
        }
    }
}
