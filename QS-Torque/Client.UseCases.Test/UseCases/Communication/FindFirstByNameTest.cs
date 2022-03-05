using Core.UseCases.Communication.DataGate;
using NUnit.Framework;

namespace Core.Test.UseCases.Communication.DataGate
{
    class FindFirstByNameTest
    {
        [Test]
        public void SearchingFindsElementItself()
        {
            var finder = new FindFirstByName(new ElementName("findMe"));
            var element = new Content(new ElementName("findMe"), null);
            element.Accept(finder);
            Assert.AreSame(element, finder.Result);
        }

        [TestCase("findme")]
        [TestCase("jfalkeiamod")]
        public void SearchingFindsElementItselfWhenItHasSubElements(string findElementName)
        {
            var finder = new FindFirstByName(new ElementName(findElementName));
            var root = new Container(new ElementName(findElementName));
            var element = new Content(new ElementName(""), null);
            root.Add(element);
            root.Accept(finder);
            Assert.AreSame(root, finder.Result);
        }

        [Test]
        public void SearchingFindsElementInContainer()
        {
            var finder = new FindFirstByName(new ElementName("findme"));
            var root = new Container(new ElementName("dontFindMe"));
            var elementToFind = new Content(new ElementName("findme"), null);
            root.Add(elementToFind);
            root.Accept(finder);
            Assert.AreSame(elementToFind, finder.Result);
        }

        [Test]
        public void SearchingFindsElementOutOfMultipleInContainer()
        {
            var finder = new FindFirstByName(new ElementName("findme"));
            var root = new Container(new ElementName("dontFindMe"));
            var elementToFind = new Content(new ElementName("findme"), null);
            root.Add(new Content(new ElementName("asdfa"), null));
            root.Add(elementToFind);
            root.Add(new Content(new ElementName("jaogjdsal"), null));
            root.Accept(finder);
            Assert.AreSame(elementToFind, finder.Result);
        }

        [Test]
        public void SearchingFindsOnlyFirstElementOutOfMultipleMatchingInContainer()
        {
            var finder = new FindFirstByName(new ElementName("findme"));
            var root = new Container(new ElementName("dontFindMe"));
            var elementToFind = new Content(new ElementName("findme"), null);
            root.Add(new Content(new ElementName("asdfa"), null));
            root.Add(elementToFind);
            root.Add(new Content(new ElementName("jaogjdsal"), null));
            root.Add(new Content(new ElementName("findme"), null)); // important! same as searched name!
            root.Accept(finder);
            Assert.AreSame(elementToFind, finder.Result);
        }

        [Test]
        public void SearchingFindsHiddenContent()
        {
            var finder = new FindFirstByName(new ElementName("findMe"));
            var element = new HiddenContent(new ElementName("findMe"), null);
            element.Accept(finder);
            Assert.AreSame(element, finder.Result);
        }

        [Test]
        public void SearchingFindsHiddenContentOutOfMultipleInContainer()
        {
            var finder = new FindFirstByName(new ElementName("findme"));
            var root = new Container(new ElementName("dontFindMe"));
            var elementToFind = new HiddenContent(new ElementName("findme"), null);
            root.Add(new HiddenContent(new ElementName("asdfa"), null));
            root.Add(elementToFind);
            root.Add(new HiddenContent(new ElementName("jaogjdsal"), null));
            root.Accept(finder);
            Assert.AreSame(elementToFind, finder.Result);
        }
    }
}