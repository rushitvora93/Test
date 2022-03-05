using Core.Entities;
using NUnit.Framework;

namespace Core.Test.Entities
{
    class ToolTypeTest
    {
        [Test]
        public void EqualsByIdWithDifferentIdsMeansInequality()
        {
            var left = new ToolType() { ListId = new HelperTableEntityId(97486153) };
            var right = new ToolType() { ListId = new HelperTableEntityId(147896325) };

            Assert.IsFalse(left.EqualsById(right));
        }

        [Test]
        public void EqualsByIdWithNullMeansInequality()
        {
            var left = new ToolType() { ListId = new HelperTableEntityId(97486153) };

            Assert.IsFalse(left.EqualsById(null));
        }

        [Test]
        public void EqualsByIdWithEqualIdsMeansEquality()
        {
            var left = new ToolType() { ListId = new HelperTableEntityId(97486153) };
            var right = new ToolType() { ListId = new HelperTableEntityId(97486153) };

            Assert.IsTrue(left.EqualsById(right));
        }

        [Test]
        public void EqualsByContentWithDifferentIdsMeansInequality()
        {
            var left = new ToolType() { ListId = new HelperTableEntityId(97486153), Value = new HelperTableDescription("ofiugvjcmkspif") };
            var right = new ToolType() { ListId = new HelperTableEntityId(147896325), Value = new HelperTableDescription("ofiugvjcmkspif") };

            Assert.IsFalse(left.EqualsByContent(right));
        }

        [Test]
        public void EqualsByContentWithDifferentValuesMeansInequality()
        {
            var left = new ToolType() { ListId = new HelperTableEntityId(97486153), Value = new HelperTableDescription("ofiugvjcmkspif") };
            var right = new ToolType() { ListId = new HelperTableEntityId(97486153), Value = new HelperTableDescription("e34r56tz7uikjhgfre") };

            Assert.IsFalse(left.EqualsByContent(right));
        }

        [Test]
        public void EqualsByContentWithNullMeansInequality()
        {
            var left = new ToolType() { ListId = new HelperTableEntityId(97486153), Value = new HelperTableDescription("ofiugvjcmkspif") };

            Assert.IsFalse(left.EqualsByContent(null));
        }

        [Test]
        public void EqualsByContentWithEqualContentMeansEquality()
        {
            var left = new ToolType() { ListId = new HelperTableEntityId(97486153), Value = new HelperTableDescription("ofiugvjcmkspif") };
            var right = new ToolType() { ListId = new HelperTableEntityId(97486153), Value = new HelperTableDescription("ofiugvjcmkspif") };

            Assert.IsTrue(left.EqualsByContent(right));
        }

        [Test]
        public void UpdateWithMeansContentEquality()
        {
            var left = new ToolType() { ListId = new HelperTableEntityId(97486153), Value = new HelperTableDescription("ofiugvjcmkspif") };
            var right = new ToolType() { ListId = new HelperTableEntityId(123654789), Value = new HelperTableDescription("asdfghjklöä") };

            left.UpdateWith(right);

            Assert.IsTrue(left.EqualsByContent(right));
        }

        [Test]
        public void UpdateWithNullThrowsNoException()
        {
            var left = new ToolType() { ListId = new HelperTableEntityId(97486153), Value = new HelperTableDescription("ofiugvjcmkspif") };

            Assert.DoesNotThrow(() => left.UpdateWith(null));
        }

        [Test]
        public void CopyMeansContentEqualityButNotReferenceEquality()
        {
            var entity = new ToolType() { ListId = new HelperTableEntityId(97486153), Value = new HelperTableDescription("ofiugvjcmkspif") };
            var copy = entity.CopyDeep();

            Assert.IsFalse(entity == copy);
            Assert.IsTrue(entity.EqualsByContent(copy));
        }
    }
}
