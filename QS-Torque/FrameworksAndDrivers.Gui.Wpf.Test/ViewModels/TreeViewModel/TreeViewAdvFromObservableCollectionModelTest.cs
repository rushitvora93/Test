using System;
using System.Collections.Generic;
using NUnit.Framework;
using FrameworksAndDrivers.Gui.Wpf.ViewModel.TreeViewModel;
using System.Collections.ObjectModel;
using InterfaceAdapters;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels.TreeViewModel
{
    public class HeaderDataTest
    {
        [Test]
        public void ConstructorTest()
        {
            string testdata = "testdata";
            var headerData = new HeaderData(testdata);
            Assert.AreEqual(testdata, headerData.Header);
        }

        [Test]
        public void GetHastCodeTest()
        {
            string testdata = "testdata";
            var headerData = new HeaderData(testdata);
            Assert.AreEqual(testdata.GetHashCode(), headerData.GetHashCode());
        }

        [Test]
        public void EqualsTest()
        {
            string testdata1 = "testdata1";
            string testdata2 = "testdata2";
            var headerData1a = new HeaderData(testdata1);
            var headerData1b = new HeaderData(testdata1);
            var headerData2 = new HeaderData(testdata2);
            Assert.IsTrue(headerData1a.Equals(headerData1b));
            Assert.IsFalse(headerData1a.Equals(headerData2));
        }
    }

    public class TranslateableHeaderDataTest
    {
        [Test]
        public void ConstructorTest()
        {
            string testdata = "testdata";
            var headerData = new TranslateableHeaderData("", testdata);
            Assert.AreEqual(testdata, headerData.Header);
        }

        [Test]
        public void GetHastCodeTest()
        {
            string testdata = "testdata";
            var headerData = new TranslateableHeaderData("", testdata);
            Assert.AreEqual(testdata.GetHashCode(), headerData.GetHashCode());
        }

        [Test]
        public void EqualsTest()
        {
            string testdata1 = "testdata1";
            string testdata2 = "testdata2";
            var headerData1a = new TranslateableHeaderData("", testdata1);
            var headerData1b = new TranslateableHeaderData("", testdata1);
            var headerData2 = new TranslateableHeaderData("", testdata2);
            Assert.IsTrue(headerData1a.Equals(headerData1b));
            Assert.IsFalse(headerData1a.Equals(headerData2));
        }
    }

    public class TreeObservableCollectionTest
    {
        [Test]
        public void AddTest()
        {
            var parent = new TreeViewItemAdvModelHeader(new HeaderData("Selected"));
            var collection = new TreeObservableCollection(parent);
            var item1 = new TreeViewItemAdvModelHeader(new HeaderData("blub1"));

            Assert.AreEqual(0, collection.Count);
            collection.Add(item1);
            Assert.IsTrue(collection[0] == item1);
            Assert.IsTrue(collection[0].Parent == parent);

            var collection2 = new TreeObservableCollection(null);
            Assert.AreEqual(0, collection2.Count);
            collection2.Add(item1);
            Assert.IsTrue(collection2[0] == item1);
            Assert.IsTrue(collection2[0].Parent == null);
        }

        [Test]
        public void SetTest()
        {
            var parent = new TreeViewItemAdvModelHeader(new HeaderData("Selected"));
            var collection = new TreeObservableCollection(parent);
            var item1 = new TreeViewItemAdvModelHeader(new HeaderData("blub1"));
            var item2 = new TreeViewItemAdvModelHeader(new HeaderData("blub2"));

            Assert.AreEqual(0, collection.Count);
            collection.Add(item1);
            collection[0] = item2;
            Assert.IsTrue(collection[0] == item2);
            Assert.IsTrue(collection[0].Parent == parent);

            var collection2 = new TreeObservableCollection(null);
            Assert.AreEqual(0, collection2.Count);
            collection2.Add(item1);
            collection2[0] = item2;
            Assert.IsTrue(collection2[0] == item2);
            Assert.IsTrue(collection2[0].Parent == null);
        }
    }

    public class TreeViewItemAdvModelTest
    {
        [Test]
        public void GetHeaderListTest()
        {
            string headerdatastring1 = "blub1";
            string headerdatastring2 = "blub2";
            var item1 = new TreeViewItemAdvModelHeader(new HeaderData(headerdatastring1));
            var item2= new TreeViewItemAdvModelHeader(new HeaderData(headerdatastring2));

            Assert.AreEqual(0,item1.GetHeaderList(false).Count);
            Assert.AreEqual(1, item1.GetHeaderList(true).Count);

            item1.SubItems.Add(item2);

            Assert.AreEqual(0, item1.GetHeaderList(false).Count);
            Assert.AreEqual(1, item1.GetHeaderList(true).Count);

            Assert.AreEqual(1, item2.GetHeaderList(false).Count);
            Assert.AreEqual(2, item2.GetHeaderList(true).Count);

            Assert.IsTrue(item2.GetHeaderList(true)[0].Header == headerdatastring1);
            Assert.IsTrue(item2.GetHeaderList(true)[1].Header == headerdatastring2);
        }
    }

    public class TreeViewItemAdvModelHeaderTest
    {
        [Test]
        public void ConstructorTest()
        {
            string headerdatastring = "Selected";
            var header = new HeaderData(headerdatastring);
            var tvheader = new TreeViewItemAdvModelHeader(header);
            Assert.AreEqual(headerdatastring, tvheader.Header);
        }
    }

    public class TreeViewItemAdvModelTranslateableHeaderTest
    {
        [Test]
        public void ConstructorTest()
        {
            string headerdatastring = "Selected";
            var header = new TranslateableHeaderData("", headerdatastring);
            var tvheader = new TreeViewItemAdvModelTranslateableHeader(header);
            Assert.AreEqual(headerdatastring, tvheader.Header);
        }
    }

    public class TreeViewItemAdvModelEntryTest
    {        
        [Test]
        public void ConstructorTest()
        {
            String testval = "Selected";
            var val = new TreeViewItemAdvModelEntry<String>(testval);
            Assert.AreEqual(testval, val.Entry);   
        }
    }

    public class ListComparerTest
    {
        [Test]
        public void EqualsTest()
        {
            var comp = new ListComparer();
            string headerdatastring1 = "blub1";
            string headerdatastring2 = "blub2";
            var item1 = new TreeViewItemAdvModelHeader(new HeaderData(headerdatastring1));
            var item2 = new TreeViewItemAdvModelHeader(new HeaderData(headerdatastring2));

            item1.SubItems.Add(item2);

            var list1 = item1.GetHeaderList(true);
            var list2 = item2.GetHeaderList(true);
            var list3 = item2.GetHeaderList(true);

            Assert.IsFalse(list2 == list3);            
            Assert.IsTrue(comp.Equals(list2, list3));
            Assert.IsFalse(comp.Equals(list1, list2));
        }

        [Test]
        public void GetHashCodeTest()
        {
            var comp = new ListComparer();
            string headerdatastring1 = "blub1";
            string headerdatastring2 = "blub2";
            var item1 = new TreeViewItemAdvModelHeader(new HeaderData(headerdatastring1));
            var item2 = new TreeViewItemAdvModelHeader(new HeaderData(headerdatastring2));

            item1.SubItems.Add(item2);

            var list1 = item1.GetHeaderList(true);
            var list2 = item2.GetHeaderList(true);
            var list3 = item2.GetHeaderList(true);

            Assert.IsFalse(list2 == list3);
            Assert.IsTrue(comp.GetHashCode(list2) == comp.GetHashCode(list3));
            Assert.IsFalse(comp.GetHashCode(list1) == comp.GetHashCode(list2));
        }
    }

    public class TreeModelAdvFromObserverablecolleciontModelCacheTest
    {
        [Test]
        public void ConstructorTest()
        {
            ITreeModelAdvFromObserverablecolleciontModelCache<String> cache = new TreeModelAdvFromObserverablecolleciontModelCache<String>();
            Assert.IsNotNull(cache.ListToLowestPathModel);
            Assert.IsNotNull(cache.TToLowestPathmodel);
            Assert.IsNotNull(cache.TranslateableTreeItems);
        }
    }

    internal class ModelMock : BindableBase
    {
        private String _caption;
        public String Caption { get => _caption; set => Set<String>(ref _caption, value); }
    } 

    public class TreeViewAdvFromObservableCollectionModelTest
    {
        [Test]
        public void AddTest()
        {
            String header1 = "Werkzeugmodelle";
            String header2 = "andere feste ebene";
            String model1 = "modelmock1";
            String model2 = "modelmock2";
            ModelMock modelMock1a = new ModelMock() { Caption = model1 };
            ModelMock modelMock1b = new ModelMock() { Caption = model1 };
            ModelMock modelMock2 = new ModelMock() { Caption = model2 };
            
            ITreeModelAdvFromObserverablecolleciontModelCache<ModelMock> cache = new TreeModelAdvFromObserverablecolleciontModelCache< ModelMock>();
            ObservableCollection<ModelMock> obsCollection = new ObservableCollection<ModelMock>();
            TreeViewAdvFromObservableCollectionModel<ModelMock> model =
                new TreeViewAdvFromObservableCollectionModel<ModelMock>(
                    obsCollection,
                    (item) => (new List<HeaderData>() {
                    new TranslateableHeaderData("",header1),
                    new TranslateableHeaderData("",header2),
                    new HeaderData(item.Caption + item.Caption) }),
                    (item) => (item.Caption), null, cache);

            Assert.AreEqual(0, cache.ListToLowestPathModel.Count);
            Assert.AreEqual(0, cache.TToLowestPathmodel.Count);
            Assert.AreEqual(0, cache.TranslateableTreeItems.Count);
            Assert.AreEqual(0, model.Source.Count);

            obsCollection.Add(modelMock1a);

            Assert.AreEqual(3, cache.ListToLowestPathModel.Count);
            Assert.AreEqual(1, cache.TToLowestPathmodel.Count);
            Assert.AreEqual(2, cache.TranslateableTreeItems.Count);
            Assert.AreEqual(1, model.Source.Count);
            Assert.AreEqual(header1, model.Source[0].Header);
            Assert.AreEqual(1, model.Source[0].SubItems.Count);
            Assert.AreEqual(header2, model.Source[0].SubItems[0].Header);
            Assert.AreEqual(1, model.Source[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(model1 + model1, model.Source[0].SubItems[0].SubItems[0].Header);
            Assert.AreEqual(1, model.Source[0].SubItems[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(model1, model.Source[0].SubItems[0].SubItems[0].SubItems[0].Header);

            obsCollection.Add(modelMock1b);
            Assert.AreEqual(3, cache.ListToLowestPathModel.Count);
            Assert.AreEqual(2, cache.TToLowestPathmodel.Count);
            Assert.AreEqual(2, cache.TranslateableTreeItems.Count);
            Assert.AreEqual(1, model.Source.Count);
            Assert.AreEqual(header1, model.Source[0].Header);
            Assert.AreEqual(1, model.Source[0].SubItems.Count);
            Assert.AreEqual(header2, model.Source[0].SubItems[0].Header);
            Assert.AreEqual(1, model.Source[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(model1 + model1, model.Source[0].SubItems[0].SubItems[0].Header);
            Assert.AreEqual(2, model.Source[0].SubItems[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(model1, model.Source[0].SubItems[0].SubItems[0].SubItems[0].Header);
            Assert.AreEqual(model1, model.Source[0].SubItems[0].SubItems[0].SubItems[1].Header);

            obsCollection.Add(modelMock2);
            Assert.AreEqual(4, cache.ListToLowestPathModel.Count);
            Assert.AreEqual(3, cache.TToLowestPathmodel.Count);
            Assert.AreEqual(2, cache.TranslateableTreeItems.Count);
            Assert.AreEqual(1, model.Source.Count);
            Assert.AreEqual(header1, model.Source[0].Header);
            Assert.AreEqual(1, model.Source[0].SubItems.Count);
            Assert.AreEqual(header2, model.Source[0].SubItems[0].Header);
            Assert.AreEqual(2, model.Source[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(model1 + model1, model.Source[0].SubItems[0].SubItems[0].Header);
            Assert.AreEqual(model2 + model2, model.Source[0].SubItems[0].SubItems[1].Header);
            Assert.AreEqual(2, model.Source[0].SubItems[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(1, model.Source[0].SubItems[0].SubItems[1].SubItems.Count);
            Assert.AreEqual(model1, model.Source[0].SubItems[0].SubItems[0].SubItems[0].Header);
            Assert.AreEqual(model1, model.Source[0].SubItems[0].SubItems[0].SubItems[1].Header);
            Assert.AreEqual(model2, model.Source[0].SubItems[0].SubItems[1].SubItems[0].Header);
        }

        [Test]
        public void RemoveTest()
        {
            String header1 = "Werkzeugmodelle";
            String header2 = "andere feste ebene";
            String model1 = "modelmock1";
            String model2 = "modelmock2";
            ModelMock modelMock1a = new ModelMock() { Caption = model1 };
            ModelMock modelMock1b = new ModelMock() { Caption = model1 };
            ModelMock modelMock2 = new ModelMock() { Caption = model2 };

            ITreeModelAdvFromObserverablecolleciontModelCache<ModelMock> cache = new TreeModelAdvFromObserverablecolleciontModelCache<ModelMock>();
            ObservableCollection<ModelMock> obsCollection = new ObservableCollection<ModelMock>();
            TreeViewAdvFromObservableCollectionModel<ModelMock> model =
                new TreeViewAdvFromObservableCollectionModel<ModelMock>(
                    obsCollection,
                    (item) => (new List<HeaderData>() {
                    new TranslateableHeaderData("",header1),
                    new TranslateableHeaderData("",header2),
                    new HeaderData(item.Caption + item.Caption) }),
                    (item) => (item.Caption), null, cache);


            obsCollection.Add(modelMock1a);
            obsCollection.Add(modelMock1b);
            obsCollection.Add(modelMock2);

            obsCollection.Remove(modelMock2);
            Assert.AreEqual(3, cache.ListToLowestPathModel.Count);
            Assert.AreEqual(2, cache.TToLowestPathmodel.Count);
            Assert.AreEqual(2, cache.TranslateableTreeItems.Count);
            Assert.AreEqual(1, model.Source.Count);
            Assert.AreEqual(header1, model.Source[0].Header);
            Assert.AreEqual(1, model.Source[0].SubItems.Count);
            Assert.AreEqual(header2, model.Source[0].SubItems[0].Header);
            Assert.AreEqual(1, model.Source[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(model1 + model1, model.Source[0].SubItems[0].SubItems[0].Header);
            Assert.AreEqual(2, model.Source[0].SubItems[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(model1, model.Source[0].SubItems[0].SubItems[0].SubItems[0].Header);
            Assert.AreEqual(model1, model.Source[0].SubItems[0].SubItems[0].SubItems[1].Header);

            obsCollection.Remove(modelMock1b);
            Assert.AreEqual(3, cache.ListToLowestPathModel.Count);
            Assert.AreEqual(1, cache.TToLowestPathmodel.Count);
            Assert.AreEqual(2, cache.TranslateableTreeItems.Count);
            Assert.AreEqual(1, model.Source.Count);
            Assert.AreEqual(header1, model.Source[0].Header);
            Assert.AreEqual(1, model.Source[0].SubItems.Count);
            Assert.AreEqual(header2, model.Source[0].SubItems[0].Header);
            Assert.AreEqual(1, model.Source[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(model1 + model1, model.Source[0].SubItems[0].SubItems[0].Header);
            Assert.AreEqual(1, model.Source[0].SubItems[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(model1, model.Source[0].SubItems[0].SubItems[0].SubItems[0].Header);

            obsCollection.Remove(modelMock1a);
            Assert.AreEqual(0, cache.ListToLowestPathModel.Count);
            Assert.AreEqual(0, cache.TToLowestPathmodel.Count);
            Assert.AreEqual(0, cache.TranslateableTreeItems.Count);
            Assert.AreEqual(0, model.Source.Count);           
        }

        [Test]
        public void ClearTest()
        {
            String header1 = "Werkzeugmodelle";
            String header2 = "andere feste ebene";
            String model1 = "modelmock1";
            String model2 = "modelmock2";
            ModelMock modelMock1a = new ModelMock() { Caption = model1 };
            ModelMock modelMock1b = new ModelMock() { Caption = model1 };
            ModelMock modelMock2 = new ModelMock() { Caption = model2 };

            ITreeModelAdvFromObserverablecolleciontModelCache<ModelMock> cache = new TreeModelAdvFromObserverablecolleciontModelCache<ModelMock>();
            ObservableCollection<ModelMock> obsCollection = new ObservableCollection<ModelMock>();
            TreeViewAdvFromObservableCollectionModel<ModelMock> model =
                new TreeViewAdvFromObservableCollectionModel<ModelMock>(
                    obsCollection,
                    (item) => (new List<HeaderData>() {
                    new TranslateableHeaderData("",header1),
                    new TranslateableHeaderData("",header2),
                    new HeaderData(item.Caption + item.Caption) }),
                    (item) => (item.Caption), null, cache);


            obsCollection.Add(modelMock1a);
            obsCollection.Add(modelMock1b);
            obsCollection.Add(modelMock2);

            obsCollection.Clear();
            Assert.AreEqual(0, cache.ListToLowestPathModel.Count);
            Assert.AreEqual(0, cache.TToLowestPathmodel.Count);
            Assert.AreEqual(0, cache.TranslateableTreeItems.Count);
            Assert.AreEqual(0, model.Source.Count);
        }

        [Test]
        public void ItemChangedTest()
        {
            String header1 = "Werkzeugmodelle";
            String header2 = "andere feste ebene";
            String model1 = "modelmock1";
            String model2 = "modelmock2";
            ModelMock modelMock1a = new ModelMock() { Caption = model1 };
            ModelMock modelMock1b = new ModelMock() { Caption = model1 };
            ModelMock modelMock2 = new ModelMock() { Caption = model2 };

            ITreeModelAdvFromObserverablecolleciontModelCache<ModelMock> cache = new TreeModelAdvFromObserverablecolleciontModelCache<ModelMock>();
            ObservableCollection<ModelMock> obsCollection = new ObservableCollection<ModelMock>();
            TreeViewAdvFromObservableCollectionModel<ModelMock> model =
                new TreeViewAdvFromObservableCollectionModel<ModelMock>(
                    obsCollection,
                    (item) => (new List<HeaderData>() {
                    new TranslateableHeaderData("",header1),
                    new TranslateableHeaderData("",header2),
                    new HeaderData(item.Caption + item.Caption) }),
                    (item) => (item.Caption), null, cache);


            obsCollection.Add(modelMock1a);
            obsCollection.Add(modelMock1b);
            obsCollection.Add(modelMock2);

            Assert.AreEqual(4, cache.ListToLowestPathModel.Count);
            Assert.AreEqual(3, cache.TToLowestPathmodel.Count);
            Assert.AreEqual(2, cache.TranslateableTreeItems.Count);
            Assert.AreEqual(1, model.Source.Count);
            Assert.AreEqual(header1, model.Source[0].Header);
            Assert.AreEqual(1, model.Source[0].SubItems.Count);
            Assert.AreEqual(header2, model.Source[0].SubItems[0].Header);
            Assert.AreEqual(2, model.Source[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(model1 + model1, model.Source[0].SubItems[0].SubItems[0].Header);
            Assert.AreEqual(model2 + model2, model.Source[0].SubItems[0].SubItems[1].Header);
            Assert.AreEqual(2, model.Source[0].SubItems[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(1, model.Source[0].SubItems[0].SubItems[1].SubItems.Count);
            Assert.AreEqual(model1, model.Source[0].SubItems[0].SubItems[0].SubItems[0].Header);
            Assert.AreEqual(model1, model.Source[0].SubItems[0].SubItems[0].SubItems[1].Header);
            Assert.AreEqual(model2, model.Source[0].SubItems[0].SubItems[1].SubItems[0].Header);

            model1 = "modelmock1a";
            String model1b = "modelmock1b";
            model2 = "modelmock2X";

            modelMock1a.Caption = model1;
            modelMock1b.Caption = model1b;
            modelMock2.Caption = model2;

            Assert.AreEqual(5, cache.ListToLowestPathModel.Count);
            Assert.AreEqual(3, cache.TToLowestPathmodel.Count);
            Assert.AreEqual(2, cache.TranslateableTreeItems.Count);
            Assert.AreEqual(1, model.Source.Count);
            Assert.AreEqual(header1, model.Source[0].Header);
            Assert.AreEqual(1, model.Source[0].SubItems.Count);
            Assert.AreEqual(header2, model.Source[0].SubItems[0].Header);

            Assert.AreEqual(3, model.Source[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(model1 + model1, model.Source[0].SubItems[0].SubItems[0].Header);
            Assert.AreEqual(model1b + model1b, model.Source[0].SubItems[0].SubItems[1].Header);
            Assert.AreEqual(model2 + model2, model.Source[0].SubItems[0].SubItems[2].Header);
            Assert.AreEqual(1, model.Source[0].SubItems[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(1, model.Source[0].SubItems[0].SubItems[1].SubItems.Count);
            Assert.AreEqual(1, model.Source[0].SubItems[0].SubItems[2].SubItems.Count);
            Assert.AreEqual(model1, model.Source[0].SubItems[0].SubItems[0].SubItems[0].Header);
            Assert.AreEqual(model1b, model.Source[0].SubItems[0].SubItems[1].SubItems[0].Header);
            Assert.AreEqual(model2, model.Source[0].SubItems[0].SubItems[2].SubItems[0].Header);
        }

        [Test]
        public void ReplaceTest()
        {
            String header1 = "Werkzeugmodelle";
            String header2 = "andere feste ebene";
            String model1 = "modelmock1";
            String model2 = "modelmock2";
            ModelMock modelMock1a = new ModelMock() { Caption = model1 };
            ModelMock modelMock1b = new ModelMock() { Caption = model1 };
            ModelMock modelMock2 = new ModelMock() { Caption = model2 };

            String model1a = "modelmock1a";
            String model1b = "modelmock1b";
            String model2x = "modelmock2X";

            ModelMock modelMock1ax = new ModelMock() { Caption = model1a };
            ModelMock modelMock1bx = new ModelMock() { Caption = model1b };
            ModelMock modelMock2x = new ModelMock() { Caption = model2x };

            ITreeModelAdvFromObserverablecolleciontModelCache<ModelMock> cache = new TreeModelAdvFromObserverablecolleciontModelCache<ModelMock>();
            ObservableCollection<ModelMock> obsCollection = new ObservableCollection<ModelMock>();
            TreeViewAdvFromObservableCollectionModel<ModelMock> model =
                new TreeViewAdvFromObservableCollectionModel<ModelMock>(
                    obsCollection,
                    (item) => (new List<HeaderData>() {
                    new TranslateableHeaderData("",header1),
                    new TranslateableHeaderData("",header2),
                    new HeaderData(item.Caption + item.Caption) }),
                    (item) => (item.Caption), null, cache);


            obsCollection.Add(modelMock1a);
            obsCollection.Add(modelMock1b);
            obsCollection.Add(modelMock2);

            Assert.AreEqual(4, cache.ListToLowestPathModel.Count);
            Assert.AreEqual(3, cache.TToLowestPathmodel.Count);
            Assert.AreEqual(2, cache.TranslateableTreeItems.Count);
            Assert.AreEqual(1, model.Source.Count);
            Assert.AreEqual(header1, model.Source[0].Header);
            Assert.AreEqual(1, model.Source[0].SubItems.Count);
            Assert.AreEqual(header2, model.Source[0].SubItems[0].Header);
            Assert.AreEqual(2, model.Source[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(model1 + model1, model.Source[0].SubItems[0].SubItems[0].Header);
            Assert.AreEqual(model2 + model2, model.Source[0].SubItems[0].SubItems[1].Header);
            Assert.AreEqual(2, model.Source[0].SubItems[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(1, model.Source[0].SubItems[0].SubItems[1].SubItems.Count);
            Assert.AreEqual(model1, model.Source[0].SubItems[0].SubItems[0].SubItems[0].Header);
            Assert.AreEqual(model1, model.Source[0].SubItems[0].SubItems[0].SubItems[1].Header);
            Assert.AreEqual(model2, model.Source[0].SubItems[0].SubItems[1].SubItems[0].Header);

            obsCollection[0] = modelMock1ax;
            obsCollection[1] = modelMock1bx;
            obsCollection[2] = modelMock2x;

            Assert.AreEqual(5, cache.ListToLowestPathModel.Count);
            Assert.AreEqual(3, cache.TToLowestPathmodel.Count);
            Assert.AreEqual(2, cache.TranslateableTreeItems.Count);
            Assert.AreEqual(1, model.Source.Count);
            Assert.AreEqual(header1, model.Source[0].Header);
            Assert.AreEqual(1, model.Source[0].SubItems.Count);
            Assert.AreEqual(header2, model.Source[0].SubItems[0].Header);

            Assert.AreEqual(3, model.Source[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(model1a + model1a, model.Source[0].SubItems[0].SubItems[0].Header);
            Assert.AreEqual(model1b + model1b, model.Source[0].SubItems[0].SubItems[1].Header);
            Assert.AreEqual(model2x + model2x, model.Source[0].SubItems[0].SubItems[2].Header);
            Assert.AreEqual(1, model.Source[0].SubItems[0].SubItems[0].SubItems.Count);
            Assert.AreEqual(1, model.Source[0].SubItems[0].SubItems[1].SubItems.Count);
            Assert.AreEqual(1, model.Source[0].SubItems[0].SubItems[2].SubItems.Count);
            Assert.AreEqual(model1a, model.Source[0].SubItems[0].SubItems[0].SubItems[0].Header);
            Assert.AreEqual(model1b, model.Source[0].SubItems[0].SubItems[1].SubItems[0].Header);
            Assert.AreEqual(model2x, model.Source[0].SubItems[0].SubItems[2].SubItems[0].Header);
        }
    }
}
