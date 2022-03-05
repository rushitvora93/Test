using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TestHelper.Checker
{
    public class CheckerFunctions
    {
        // More flexible implementation of CollectionAssert.AreEquivalent
        public static void CollectionAssertAreEquivalent<TypeA, TypeB>(
            IList<TypeA> expectedList,
            IList<TypeB> realList,
            Func<TypeA, TypeB, bool> comparer)
        {
            Assert.AreEqual(expectedList.Count, realList.Count, "lists have different size");
            var countFound = expectedList.Count(expectedItem =>
                realList.FirstOrDefault(realListItem => comparer(expectedItem, realListItem)) != null);
            Assert.AreEqual(expectedList.Count, countFound, "objects in list don't match");
        }
    }
}
