using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FrameworksAndDrivers.Gui.Wpf.Test
{
    class FilteredObservableCollectionExtensionTest
    {
        [Test]
        public void AddValidEntry()
        {
            var real = new ObservableCollection<object>();
            var filteredColl = new FilteredObservableCollectionExtension<object>(real);
            filteredColl.Filter = x => true;
            var obj = new object();
            real.Add(obj);
            Assert.IsTrue(filteredColl.Contains(obj));
        }

        [Test]
        public void AddInvalidEntry()
        {
            var real = new ObservableCollection<object>();
            var filteredColl = new FilteredObservableCollectionExtension<object>(real);
            filteredColl.Filter = x => false;
            var obj = new object();
            real.Add(obj);
            Assert.IsFalse(filteredColl.Contains(obj));
        }

        [Test]
        public void RefilterCollection()
        {
            var real = new ObservableCollection<object>();
            var filteredColl = new FilteredObservableCollectionExtension<object>(real);
            bool filter = true;
            filteredColl.Filter = x => filter;
            var obj = new object();
            real.Add(obj);
            Assert.IsTrue(filteredColl.Contains(obj));
            filter = false;
            filteredColl.RefilterCollection();
            Assert.IsFalse(filteredColl.Contains(obj));
        }

        [Test]
        public void RefilterCollectionAfterNewFilterSet()
        {
            var real = new ObservableCollection<object>();
            var filteredColl = new FilteredObservableCollectionExtension<object>(real);
            filteredColl.Filter = x => true;
            var obj = new object();
            real.Add(obj);
            Assert.IsTrue(filteredColl.Contains(obj));
            filteredColl.Filter = x => false;
            Assert.IsFalse(filteredColl.Contains(obj));
        }

        [Test]
        public void RefilteredInvoked()
        {
            var real = new ObservableCollection<object>();
            var filteredColl = new FilteredObservableCollectionExtension<object>(real);
            bool invoked = false;
            filteredColl.Refiltered += (s, e) => invoked = true;
            filteredColl.Filter = x => true;
            filteredColl.RefilterCollection();
            Assert.IsTrue(invoked);
        }

        [Test]
        public void RemoveEntry()
        {
            var real = new ObservableCollection<object>();
            var filteredColl = new FilteredObservableCollectionExtension<object>(real);
            filteredColl.Filter = x => true;
            var entry = new object();
            real.Add(entry);
            Assert.IsTrue(filteredColl.Contains(entry));
            real.Remove(entry);
            Assert.IsFalse(filteredColl.Contains(entry));
        }

        [Test]
        public void ResetCollection()
        {
            var real = new ObservableCollection<object>();
            var filteredColl = new FilteredObservableCollectionExtension<object>(real);
            filteredColl.Filter = x => true;
            real.Add(new object());
            real.Add(new object());
            real.Add(new object());
            Assert.AreEqual(3, filteredColl.Count);
            real.Clear();
            Assert.AreEqual(0, filteredColl.Count);
        }

        [Test]
        public void ResetCollectionAfterDisposeDoesNotThrowException()
        {
            var real = new ObservableCollection<object>();
            var filteredColl = new FilteredObservableCollectionExtension<object>(real);
            filteredColl.Filter = x => true;
            filteredColl.Dispose();
            Assert.DoesNotThrow(() => filteredColl.SetNewSourceCollection(new ObservableCollection<object>()));
        }

        [Test]
        public void DisposeResetsFilter()
        {
            var real = new ObservableCollection<object>();
            var filteredColl = new FilteredObservableCollectionExtension<object>(real);
            filteredColl.Filter = o => true;
            Assert.IsNotNull(filteredColl.Filter);
            filteredColl.Dispose();
            Assert.IsNull(filteredColl.Filter);
        }

        [Test]
        public void DisposeClearsCollection()
        {
            var real = new ObservableCollection<object>();
            var filteredColl = new FilteredObservableCollectionExtension<object>(real);
            filteredColl.Filter = o => true;
            filteredColl.Add(new object());
            filteredColl.Add(new object());
            filteredColl.Add(new object());
            filteredColl.Dispose();
            Assert.AreEqual(0, filteredColl.Count);
        }
    }
}
