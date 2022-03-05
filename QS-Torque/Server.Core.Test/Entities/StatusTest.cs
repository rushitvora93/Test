using NUnit.Framework;
using Server.Core.Entities;

namespace Server.Core.Test.Entities
{
    class StatusTest
    {
        [Test]
        public void EqualsByIdWithDifferentIdsMeansInequality()
        {
            var left = new Status() { Id = new StatusId(97486153) };
            var right = new Status() { Id = new StatusId(147896325) };

            Assert.IsFalse(left.EqualsById(right));
        }

        [Test]
        public void EqualsByIdWithNullMeansInequality()
        {
            var left = new Status() { Id = new StatusId(97486153) };

            Assert.IsFalse(left.EqualsById(null));
        }

        [Test]
        public void EqualsByIdWithEqualIdsMeansEquality()
        {
            var left = new Status() { Id = new StatusId(97486153) };
            var right = new Status() { Id = new StatusId(97486153) };

            Assert.IsTrue(left.EqualsById(right));
        }

        [Test]
        public void EqualsByContentWithDifferentIdsMeansInequality()
        {
            var left = new Status() { Id = new StatusId(97486153), Value = new StatusDescription("ofiugvjcmkspif") };
            var right = new Status() { Id = new StatusId(147896325), Value = new StatusDescription("ofiugvjcmkspif") };

            Assert.IsFalse(left.EqualsByContent(right));
        }

        [Test]
        public void EqualsByContentWithDifferentValuesMeansInequality()
        {
            var left = new Status() { Id = new StatusId(97486153), Value = new StatusDescription("ofiugvjcmkspif") };
            var right = new Status() { Id = new StatusId(97486153), Value = new StatusDescription("e34r56tz7uikjhgfre") };

            Assert.IsFalse(left.EqualsByContent(right));
        }

        [Test]
        public void EqualsByContentWithNullMeansInequality()
        {
            var left = new Status() { Id = new StatusId(97486153), Value = new StatusDescription("ofiugvjcmkspif") };

            Assert.IsFalse(left.EqualsByContent(null));
        }

        [Test]
        public void EqualsByContentWithEqualContentMeansEquality()
        {
            var left = new Status() { Id = new StatusId(97486153), Value = new StatusDescription("ofiugvjcmkspif") };
            var right = new Status() { Id = new StatusId(97486153), Value = new StatusDescription("ofiugvjcmkspif") };

            Assert.IsTrue(left.EqualsByContent(right));
        }

        [Test]
        public void UpdateWithMeansContentEquality()
        {
            var left = new Status() { Id = new StatusId(97486153), Value = new StatusDescription("ofiugvjcmkspif") };
            var right = new Status() { Id = new StatusId(123654789), Value = new StatusDescription("asdfghjklöä") };

            left.UpdateWith(right);

            Assert.IsTrue(left.EqualsByContent(right));
        }

        [Test]
        public void UpdateWithNullThrowsNoException()
        {
            var left = new Status() { Id = new StatusId(97486153), Value = new StatusDescription("ofiugvjcmkspif") };

            Assert.DoesNotThrow(() => left.UpdateWith(null));
        }

        [Test]
        public void CopyMeansContentEqualityButNotReferenceEquality()
        {
            var entity = new Status() { Id = new StatusId(97486153), Value = new StatusDescription("ofiugvjcmkspif") };
            var copy = entity.CopyDeep();

            Assert.IsFalse(entity == copy);
            Assert.IsTrue(entity.EqualsByContent(copy));
        }
    }
}
