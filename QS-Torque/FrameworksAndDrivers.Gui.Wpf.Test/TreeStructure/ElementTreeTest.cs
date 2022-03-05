using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using FrameworksAndDrivers.Gui.Wpf.TreeStructure;
using InterfaceAdapters.Models;
using NUnit.Framework;
using Syncfusion.Windows.Tools.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.Test.TreeStructure
{
    class ElementTreeTest
    {
        private class ElementTreeSourceMock : IElementTreeSource<string>
        {
            public ObservableCollection<string> TreeElements { get; }
            public Dictionary<string, string> GetTreeEdgesFor(IEnumerable<string> elements)
            {
                var dict = new Dictionary<string, string>();

                if (elements.Contains(TreeElements[0]))
                {
                    dict.Add(TreeElements[0], null);
                }

                if (elements.Contains(TreeElements[1]))
                {
                    dict.Add(TreeElements[1], TreeElements[0]);
                }

                if (elements.Contains(TreeElements[2]))
                {
                    dict.Add(TreeElements[2], TreeElements[1]);
                }

                if (elements.Contains("Element 4"))
                {
                    dict.Add("Element 4", TreeElements[0]);
                }

                return dict;
            }

            public IEnumerable<string> GetElementsTopologicalSorted(IEnumerable<string> elements, Dictionary<string, string> edges)
            {
                return TreeElements.ToList();
            }

            public ElementTreeSourceMock()
            {
                TreeElements = new ObservableCollection<string>()
                {
                    "Element 1",
                    "Element 2",
                    "Element 3"
                };
            }
        }


        [Test, RequiresThread(ApartmentState.STA)]
        public void ElementTreeInitailizesTree()
        {
            var source = new ElementTreeSourceMock();
            var tree = new ElementTree<string>(source, x => x, null);

            Assert.AreEqual(1, tree.Source.Count);
            Assert.AreEqual(source.TreeElements[0], tree.Source[0].Header);
            Assert.AreEqual(1, tree.Source[0].Items.Count);
            Assert.AreEqual(source.TreeElements[1], (tree.Source[0].Items[0] as TreeViewItemAdv).Header);
            Assert.AreEqual(1, (tree.Source[0].Items[0] as TreeViewItemAdv).Items.Count);
            Assert.AreEqual(source.TreeElements[2], ((tree.Source[0].Items[0] as TreeViewItemAdv).Items[0] as TreeViewItemAdv).Header);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void AddThrowsArgumentExceptionIfNoParentIsFount()
        {
            var source = new ElementTreeSourceMock();
            var tree = new ElementTree<string>(source, x => x, null);

            Assert.Catch<ArgumentException>(() => tree.Add("Element 5"));
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ElementTreeAddsElementCorrect()
        {
            var source = new ElementTreeSourceMock();
            var tree = new ElementTree<string>(source, x => x, null);
            tree.Add("Element 4");
            
            Assert.AreEqual(2, tree.Source[0].Items.Count);
            Assert.AreEqual("Element 4", (tree.Source[0].Items[1] as TreeViewItemAdv).Header);
        }
    }
}
