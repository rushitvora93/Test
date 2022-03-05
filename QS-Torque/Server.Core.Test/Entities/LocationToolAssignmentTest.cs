using NUnit.Framework;
using Server.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Test.Entities
{
    public class LocationToolAssignmentTest
    {
        [Test]
        public void GetTestLevelMfuReturnsTestLevel2()
        {
            var testLevel = new TestLevel() { IsActive = true };
            var locTool = new LocationToolAssignment()
            {
                TestLevelNumberMfu = 2,
                TestLevelSetMfu = new TestLevelSet() { TestLevel2 = testLevel }
            };
            Assert.AreSame(testLevel, locTool.GetTestLevel(Enums.TestType.Mfu));
        }

        [Test]
        public void GetTestLevelMfuReturnsTestLevel3()
        {
            var testLevel = new TestLevel() { IsActive = true };
            var locTool = new LocationToolAssignment()
            {
                TestLevelNumberMfu = 3,
                TestLevelSetMfu = new TestLevelSet() { TestLevel3 = testLevel }
            };
            Assert.AreSame(testLevel, locTool.GetTestLevel(Enums.TestType.Mfu));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetTestLevelMfuReturnsTestLevel1InAllOtherCases(int testLevelNumber)
        {
            var testLevel = new TestLevel() { IsActive = true };
            var locTool = new LocationToolAssignment()
            {
                TestLevelNumberMfu = testLevelNumber,
                TestLevelSetMfu = new TestLevelSet()
                {
                    TestLevel1 = testLevel,
                    TestLevel2 = new TestLevel(),
                    TestLevel3 = new TestLevel()
                }
            };
            Assert.AreSame(testLevel, locTool.GetTestLevel(Enums.TestType.Mfu));
        }

        [Test]
        public void GetTestLevelChkReturnsTestLevel2()
        {
            var testLevel = new TestLevel() { IsActive = true };
            var locTool = new LocationToolAssignment()
            {
                TestLevelNumberChk = 2,
                TestLevelSetChk = new TestLevelSet() { TestLevel2 = testLevel }
            };
            Assert.AreSame(testLevel, locTool.GetTestLevel(Enums.TestType.Chk));
        }

        [Test]
        public void GetTestLevelChkReturnsTestLevel3()
        {
            var testLevel = new TestLevel() { IsActive = true };
            var locTool = new LocationToolAssignment()
            {
                TestLevelNumberChk = 3,
                TestLevelSetChk = new TestLevelSet() { TestLevel3 = testLevel }
            };
            Assert.AreSame(testLevel, locTool.GetTestLevel(Enums.TestType.Chk));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetTestLevelChkReturnsTestLevel1InAllOtherCases(int testLevelNumber)
        {
            var testLevel = new TestLevel() { IsActive = true };
            var locTool = new LocationToolAssignment()
            {
                TestLevelNumberChk = testLevelNumber,
                TestLevelSetChk = new TestLevelSet()
                {
                    TestLevel1 = testLevel,
                    TestLevel2 = new TestLevel(),
                    TestLevel3 = new TestLevel()
                }
            };
            Assert.AreSame(testLevel, locTool.GetTestLevel(Enums.TestType.Chk));
        }
    }
}
