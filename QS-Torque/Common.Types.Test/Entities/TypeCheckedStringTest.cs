using Core.Entities;
using NUnit.Framework;
using System;

namespace Core.Test.Entities
{
    class TypeCheckedStringTest
    {
        [Test]
        public void Stringlength50Valid()
        {
            var stringToCheck = CreateStringWithLength(50);
            var subjectUnderTest = CreateStringLengthCheckerWithLimit50();
            ExpectAcceptedCheck(stringToCheck, subjectUnderTest);
        }

        [Test]
        public void Stringlength50Invalid()
        {
            var stringToCheck = CreateStringWithLength(51);
            var subjectUnderTest = CreateStringLengthCheckerWithLimit50();
            ExpectFailedCheck(stringToCheck, subjectUnderTest);
        }

        [Test]
        public void Stringlength20Valid()
        {
            var stringToCheck = CreateStringWithLength(20);
            var subjectUnderTest = CreateStringLengthCheckerWithLimit20();
            ExpectAcceptedCheck(stringToCheck, subjectUnderTest);
        }

        [Test]
        public void Stringlength20Invalid()
        {
            var stringToCheck = CreateStringWithLength(21);
            var subjectUnderTest = CreateStringLengthCheckerWithLimit20();
            ExpectFailedCheck(stringToCheck, subjectUnderTest);
        }

        [Test]
        public void StringMaxLengthWidthNullValue()
        {
            var maxLengthCheck = CreateStringLengthCheckerWithLimit20();
            ExpectAcceptedCheck(null, maxLengthCheck);
        }

        [Test]
        public void UnblacklistedStringValid()
        {
            var stringToCheck = CreateStringWithOnlyLatinCharacters();
            var subjectUnderTest = CreateNewLineBlacklistChecker();
            ExpectAcceptedCheck(stringToCheck, subjectUnderTest);
        }

        [TestCase('\n')]
        [TestCase('\r')]
        [TestCase('\f')]
        [TestCase('\v')]
        [TestCase('\x85')]
        [TestCase('\x2028')]
        [TestCase('\x2029')]
        public void BlacklistedStringInvalid(char blacklistedCharacter)
        {
            var stringToCheck = CreateStringWithOnlyLatinCharacters() + blacklistedCharacter;
            var subjectUnderTest = CreateNewLineBlacklistChecker();
            ExpectFailedCheck(stringToCheck, subjectUnderTest);
        }

        [Test]
        public void PassThroughCheckedStringIsValid()
        {
            var subjectUnderTest = new TypeCheckedString<NoCheck, NoCheck, NoCheck>("");
            // only pass, because failure would have thrown an exception and failed the test
            Assert.Pass();
        }

        [Test]
        public void FailWhenAtLeastOneCheckFails()
        {
            Assert.Catch<ArgumentException>(() => new TypeCheckedString<AlwaysFailCheck, NoCheck, NoCheck>(""));
            Assert.Catch<ArgumentException>(() => new TypeCheckedString<NoCheck, AlwaysFailCheck, NoCheck>(""));
            Assert.Catch<ArgumentException>(() => new TypeCheckedString<NoCheck, NoCheck, AlwaysFailCheck>(""));
            Assert.Catch<ArgumentException>(() =>
                new TypeCheckedString<AlwaysFailCheck, AlwaysFailCheck, AlwaysFailCheck>(""));
        }

        [Test]
        public void StringBlackListWidthNullValue()
        {
            var blackListCheck = CreateNewLineBlacklistChecker();
            ExpectAcceptedCheck(null, blackListCheck);
        }

        [TestCase("akbjalsdfj2893ruäaewipo2", true)]
        [TestCase(" akbjalsdfj2893ruäaewipo2", false)]
        [TestCase("akbjalsdfj28 93ruäaewipo2", false)]
        [TestCase("akbjalsdfj28 93ruäaewipo2 ", false)]
        [TestCase("", true)]
        [TestCase(" ", false)]
        [TestCase("\u1680", false)]
        [TestCase("\u2005", false)]
        [TestCase("\u2008", false)]
        [TestCase("\u202f", false)]
        [TestCase("\u3000", false)]
        [TestCase("\u2028", false)]
        [TestCase("\u2029", false)]
        [TestCase("\u0009", false)]
        [TestCase("\u000a", false)]
        [TestCase("\u000b", false)]
        [TestCase("\u000c", false)]
        [TestCase("\u000d", false)]
        [TestCase("\u0085", false)]
        [TestCase("\u0085akbjalsdfj28\u0009\u000993ruäa ewipo2\u000a", false)]
        public void CheckingForNoWhiteSpaceDetectsWhiteSpace(string stringToCheck, bool expectedCheckResult)
        {
            Assert.AreEqual(expectedCheckResult, new NoWhiteSpace().Check(stringToCheck));
        }

        [Test]
        public void EqualityIsDeterminedWithValue()
        {
            var left = new TypeCheckedString<NoCheck, NoCheck, NoCheck>("roiguhjnsodfj");
            var right = new TypeCheckedString<NoCheck, NoCheck, NoCheck>(left.ToDefaultString());

            Assert.IsTrue(left.Equals(right));
        }

        [Test]
        public void EqualsWithNullMeansInequality()
        {
            var left = new TypeCheckedString<NoCheck, NoCheck, NoCheck>("roiguhjnsodfj");

            Assert.IsFalse(left.Equals(null));
        }

        class AlwaysFailCheck : StringCheck
        {
            public bool Check(string value)
            {
                return false;
            }
        }

        private static void ExpectAcceptedCheck(string stringToCheck, StringCheck subjectUnderTest)
        {
            Assert.IsTrue(subjectUnderTest.Check(stringToCheck));
        }

        private static void ExpectFailedCheck(string stringToCheck, StringCheck subjectUnderTest)
        {
            Assert.IsFalse(subjectUnderTest.Check(stringToCheck));
        }

        private string CreateStringWithLength(int length)
        {
            var result = "";
            for (var i = 0; i < length; ++i)
            {
                result += "a";
            }

            return result;
        }

        private static string CreateStringWithOnlyLatinCharacters()
        {
            return "rtzuio";
        }

        private static MaxLength<CtInt50> CreateStringLengthCheckerWithLimit50()
        {
            return new MaxLength<CtInt50>();
        }

        private static MaxLength<CtInt20> CreateStringLengthCheckerWithLimit20()
        {
            return new MaxLength<CtInt20>();
        }

        private static Blacklist<NewLines> CreateNewLineBlacklistChecker()
        {
            return new Blacklist<NewLines>();
        }
    }
}