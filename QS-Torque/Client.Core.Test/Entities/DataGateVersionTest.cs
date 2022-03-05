using System;
using Client.Core.Entities;
using NUnit.Framework;

namespace Client.Core.Test.Entities
{
    class DataGateVersionTest
    {
        [Test]
        public void CreateDataGateVersionThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var dg = new DataGateVersion(-5464);
            });
        }

        [Test]
        public void EqualsWithDifferentVersionsMeansInequality()
        {
            var left = new DataGateVersion(1);
            var right = new DataGateVersion(2);

            Assert.IsFalse(left.Equals(right));
        }

        [Test]
        public void EqualsWithSameVersionsMeansInequality()
        {
            var left = new DataGateVersion(2);
            var right = new DataGateVersion(2);

            Assert.IsTrue(left.Equals(right));
        }
    }
}
