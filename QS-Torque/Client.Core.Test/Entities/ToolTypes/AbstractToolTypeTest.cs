using System;
using Core.Entities.ToolTypes;
using NUnit.Framework;

namespace Core.Test.Entities.ToolTypes
{
    class AbstractToolTypeTest
    {
        private class AbstractToolTypeMock : AbstractToolType {
            public override string Name { get; }
            public override bool DoesToolTypeHasProperty(string propertyName)
            {
                throw new NotImplementedException();
            }

            public override void Accept(IAbstractToolTypeVisitor visitor)
            {
                throw new NotImplementedException();
            }
        }

        private class SecondAbstractToolTypeMock : AbstractToolType
        {
            public override string Name { get; }
            public override bool DoesToolTypeHasProperty(string propertyName)
            {
                throw new NotImplementedException();
            }

            public override void Accept(IAbstractToolTypeVisitor visitor)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void EqualsByTypeWithDifferentTypesMeansInequality()
        {
            var left = new AbstractToolTypeMock();
            var right = new SecondAbstractToolTypeMock();

            Assert.IsFalse(left.EqualsByType(right));
        }

        [Test]
        public void EqualsByTypeWithNullMeansInequality()
        {
            var left = new AbstractToolTypeMock();

            Assert.IsFalse(left.EqualsByType(null));
        }

        [Test]
        public void EqualsByIdWithEqualTypesMeansEquality()
        {
            var left = new AbstractToolTypeMock();
            var right = new AbstractToolTypeMock();

            Assert.IsTrue(left.EqualsByType(right));
        }
    }
}
