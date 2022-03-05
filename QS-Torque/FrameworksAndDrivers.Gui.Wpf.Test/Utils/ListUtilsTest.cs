using System;
using System.Collections.Generic;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using FrameworksAndDrivers.Gui.Wpf.ViewModel.HtmlViewModel;
using NUnit.Framework;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Utils
{
    class ListUtilsTest
    {

        private static readonly object[] SplitListIntTestSource =
        {
            new object[] {
                5,
                new List<int>  {1, 2, 3, 4, 5, 6, 7, 8, 9},
                new List<List<int>>
                {
                    new List<int>{ 1, 2, 3, 4, 5},
                    new List<int>{ 6, 7, 8, 9 }
                }},

            new object[] {
                3,
                new List<int>  {10, 20, 30, 40, 50, 60, 70, 80, 90},
                new List<List<int>>
                {
                    new List<int>{ 10, 20, 30 },
                    new List<int>{ 40, 50, 60 },
                    new List<int>{ 70, 80, 90 },
                }},
        };

 
        [TestCaseSource("SplitListIntTestSource")]
        public void SplitListIntTest(int splitSize, List<int> list2Split, List<List<int>> resultList)
        {
            var erg = ListUtils.SplitList(list2Split, splitSize);
            Assert.AreEqual(resultList, erg);
        }

        private static readonly object[] SplitListStringTestSource =
        {
            new object[] {
                5,
                new List<string>  {"1", "2", "3", "4", "5", "6", "7", "8", "9"},
                new List<List<string>>
                {
                    new List<string>{ "1", "2", "3", "4", "5"},
                    new List<string>{ "6", "7", "8", "9" }
                }},

            new object[] {
                3,
                new List<string>  {"10", "20", "30", "40", "50", "60", "70", "80", "90"},
                new List<List<string>>
                {
                    new List<string>{ "10", "20","30" },
                    new List<string>{ "40", "50", "60" },
                    new List<string>{ "70", "80", "90" },
                }},
        };


        [TestCaseSource("SplitListStringTestSource")]
        public void SplitListStringTest(int splitSize, List<string> list2Split, List<List<string>> resultList)
        {
            var erg = ListUtils.SplitList(list2Split, splitSize);
            Assert.AreEqual(resultList, erg);
        }

        [Test]
        public void TrySplitListwithNull()
        {
            Assert.Throws<ArgumentException>(() => ListUtils.SplitList((List<int>) null, 1));
        }
    }
}
