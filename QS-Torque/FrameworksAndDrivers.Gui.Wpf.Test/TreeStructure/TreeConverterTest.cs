using System.Collections.ObjectModel;
using System.Threading;
using FrameworksAndDrivers.Gui.Wpf.TreeStructure;
using NUnit.Framework;
using Syncfusion.Windows.Tools.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.Test.TreeStructure
{
    class TreeConverterTest
    {
        private class TestClass
        {
            public string Text { get; set; }
        }

        private class TreeMock : TreeBase<TestClass>
        {
            public TestClass AddParameter;
            public TestClass RemoveParameter;
            public TestClass GetContainerForItemParameter;
            public TestClass ExpandToItemParameter;
            public TestClass CollapseToItemParameter;
            public TestClass SelectParameter;

            public TreeViewItemAdv GetContainerForItemReturnValue;

            public override ObservableCollection<TreeViewItemAdv> Source { get; }

            public TreeMock(ObservableCollection<TestClass> list, bool allowEmptyNodes) : base(list, allowEmptyNodes, null, false)
            {
            }

            public override void Add(TestClass item)
            {
                AddParameter = item;
            }

            public override void Remove(TestClass item)
            {
                RemoveParameter = item;
            }

            public override TreeViewItemAdv GetContainerForItem(TestClass item)
            {
                GetContainerForItemParameter = item;
                return GetContainerForItemReturnValue;
            }

            public override void ExpandToItem(TestClass item)
            {
                ExpandToItemParameter = item;
            }

            public override void CollapseToItem(TestClass item)
            {
                CollapseToItemParameter = item;
            }

            public override void Select(TestClass item)
            {
                SelectParameter = item;
            }

            protected override void ProtectedAdd(TestClass item)
            {
                // Do nothing
            }
        }

        private const string GlobalString = "40r5tizuhgjifroepw049utzhgjfkdlsöpaö";

        [Test]
        public void AddOfSubTreeIsCalledWidthCorrectParameter()
        {
            var subTree = CreateTreeMockWithGlobalString(null);
            var converter = CreateTreeConverter(subTree);

            converter.Add("340r5t9i8ughfjdkseiertuzhj");
            
            Assert.AreEqual("340r5t9i8ughfjdkseiertuzhj", subTree.AddParameter.Text);
        }

        [Test]
        public void RemoveOfSubTreeIsCalledWidthCorrectParameter()
        {
            var item = new TestClass {Text = GlobalString};
            var subTree = CreateTreeMockWithGlobalString(item);
            var converter = CreateTreeConverter(subTree);

            converter.Remove(GlobalString);

            Assert.AreEqual(item, subTree.RemoveParameter);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void GetContainerForItemOfSubTreeIsCalledWidthCorrectParameter()
        {
            var item = new TestClass { Text = GlobalString };
            var tvi = new TreeViewItemAdv();
            var subTree = CreateTreeMockWithGlobalString(item);
            var converter = CreateTreeConverter(subTree);

            subTree.GetContainerForItemReturnValue = tvi;
            var result = converter.GetContainerForItem(GlobalString);

            Assert.AreEqual(item, subTree.GetContainerForItemParameter);
            Assert.AreEqual(tvi, result);
        }

        [Test]
        public void ExpandToItemOfSubTreeIsCalledWidthCorrectParameter()
        {
            var item = new TestClass { Text = GlobalString };
            var subTree = CreateTreeMockWithGlobalString(item);
            var converter = CreateTreeConverter(subTree);

            converter.ExpandToItem(GlobalString);

            Assert.AreEqual(item, subTree.ExpandToItemParameter);
        }

        [Test]
        public void CollapseToItemOfSubTreeIsCalledWidthCorrectParameter()
        {
            var item = new TestClass { Text = GlobalString };
            var subTree = CreateTreeMockWithGlobalString(item);
            var converter = CreateTreeConverter(subTree);

            converter.CollapseToItem(GlobalString);

            Assert.AreEqual(item, subTree.CollapseToItemParameter);
        }

        [Test]
        public void SelectOfSubTreeIsCalledWidthCorrectParameter()
        {
            var item = new TestClass { Text = GlobalString };
            var subTree = CreateTreeMockWithGlobalString(item);
            var converter = CreateTreeConverter(subTree);

            converter.Select(GlobalString);

            Assert.AreEqual(item, subTree.SelectParameter);
        }


        private TreeMock CreateTreeMockWithGlobalString(TestClass item)
        {
            return new TreeMock(new ObservableCollection<TestClass>(){ item }, false);
        }

        private TreeConverter<string, TestClass> CreateTreeConverter(TreeMock subTree)
        {
            return new TreeConverter<string, TestClass>(
                subTree,
                (s, tc) => s == tc.Text,
                x => new TestClass() {Text = x});
        }
    }
}