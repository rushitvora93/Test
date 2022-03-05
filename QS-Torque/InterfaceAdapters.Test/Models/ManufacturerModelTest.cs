using NUnit.Framework;
using Core.Entities;
using InterfaceAdapters.Models;

namespace InterfaceAdapters.Test.Models
{
    class ManufacturerModelTest
    {
        [Test]
        public void HelperTableItemModelComparerEqualsTest()
        {
            var comparer = new ManufacturerModelComparer();

            var item1 = new ManufacturerModel(new Manufacturer()) { Id = 1 };
            var item2 = new ManufacturerModel(new Manufacturer()) { Id = 1 };

            Assert.IsTrue(comparer.Equals(item1, item2));
        }

        [Test]
        public void HelperTableItemModelComparerNotEqualsTest()
        {
            var comparer = new ManufacturerModelComparer();

            var item1 = new ManufacturerModel(new Manufacturer()) { Id = 1 };
            var item2 = new ManufacturerModel(new Manufacturer()) { Id = 2 };

            Assert.IsFalse(comparer.Equals(item1, item2));
        }
    }
}
