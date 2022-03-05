﻿using Core.Entities;
using NUnit.Framework;

namespace Core.Test.Entities
{
    class ToolUsageTest
    {
        [Test]
        public void EqualsByIdWithDifferentIdsMeansInequality()
        {
            var left = new ToolUsage() { ListId = new HelperTableEntityId(97486153) };
            var right = new ToolUsage() { ListId = new HelperTableEntityId(147896325) };

            Assert.IsFalse(left.EqualsById(right));
        }

        [Test]
        public void EqualsByIdWithNullMeansInequality()
        {
            var left = new ToolUsage() { ListId = new HelperTableEntityId(97486153) };

            Assert.IsFalse(left.EqualsById(null));
        }

        [Test]
        public void EqualsByIdWithEqualIdsMeansEquality()
        {
            var left = new ToolUsage() { ListId = new HelperTableEntityId(97486153) };
            var right = new ToolUsage() { ListId = new HelperTableEntityId(97486153) };

            Assert.IsTrue(left.EqualsById(right));
        }

        [Test]
        public void EqualsByContentWithDifferentIdsMeansInequality()
        {
            var left = new ToolUsage() { ListId = new HelperTableEntityId(97486153), Value = new ToolUsageDescription("ofiugvjcmkspif") };
            var right = new ToolUsage() { ListId = new HelperTableEntityId(147896325), Value = new ToolUsageDescription("ofiugvjcmkspif") };

            Assert.IsFalse(left.EqualsByContent(right));
        }

        [Test]
        public void EqualsByContentWithDifferentValuesMeansInequality()
        {
            var left = new ToolUsage() { ListId = new HelperTableEntityId(97486153), Value = new ToolUsageDescription("ofiugvjcmkspif") };
            var right = new ToolUsage() { ListId = new HelperTableEntityId(97486153), Value = new ToolUsageDescription("e34r56tz7uikjhgfre") };

            Assert.IsFalse(left.EqualsByContent(right));
        }

        [Test]
        public void EqualsByContentWithNullMeansInequality()
        {
            var left = new ToolUsage() { ListId = new HelperTableEntityId(97486153), Value = new ToolUsageDescription("ofiugvjcmkspif") };

            Assert.IsFalse(left.EqualsByContent(null));
        }

        [Test]
        public void EqualsByContentWithEqualContentMeansEquality()
        {
            var left = new ToolUsage() { ListId = new HelperTableEntityId(97486153), Value = new ToolUsageDescription("ofiugvjcmkspif") };
            var right = new ToolUsage() { ListId = new HelperTableEntityId(97486153), Value = new ToolUsageDescription("ofiugvjcmkspif") };

            Assert.IsTrue(left.EqualsByContent(right));
        }

        [Test]
        public void UpdateWithMeansContentEquality()
        {
            var left = new ToolUsage() { ListId = new HelperTableEntityId(97486153), Value = new ToolUsageDescription("ofiugvjcmkspif") };
            var right = new ToolUsage() { ListId = new HelperTableEntityId(123654789), Value = new ToolUsageDescription("asdfghjklöä") };

            left.UpdateWith(right);

            Assert.IsTrue(left.EqualsByContent(right));
        }

        [Test]
        public void UpdateWithNullThrowsNoException()
        {
            var left = new ToolUsage() { ListId = new HelperTableEntityId(97486153), Value = new ToolUsageDescription("ofiugvjcmkspif") };

            Assert.DoesNotThrow(() => left.UpdateWith(null));
        }

        [Test]
        public void CopyMeansContentEqualityButNotReferenceEquality()
        {
            var entity = new ToolUsage() { ListId = new HelperTableEntityId(97486153), Value = new ToolUsageDescription("ofiugvjcmkspif") };
            var copy = entity.CopyDeep();

            Assert.IsFalse(entity == copy);
            Assert.IsTrue(entity.EqualsByContent(copy));
        }
    }
}
