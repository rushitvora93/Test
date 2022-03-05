using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using NUnit.Framework;
using TestHelper.Checker;

namespace Core.Test.Entities
{
    public class ShiftManagementTest
    {
        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<ShiftManagement> parameter, EqualityTestHelper<ShiftManagement> helper) helperTuple)
        {
            helperTuple.helper.CheckInequalityForParameter(helperTuple.parameter);
        }

        [Test]
        public void EqualsByContentWithRightIsNullMeansInequality()
        {
            var helper = GetEqualsByContentTestHelper();
            helper.CheckInequalityWithRightIsNull();
        }

        [Test]
        public void EqualsByContentWithEqualContentMeansEquality()
        {
            var helper = GetEqualsByContentTestHelper();
            helper.CheckEqualityForParameterList();
        }

        [Test]
        public void CopyMeansContentEqualityButNotReferenceEquality()
        {
            var helper = GetEqualsByContentTestHelper();
            helper.CheckEqualityAfterCopy(entity => entity.CopyDeep());
        }

        [Test]
        public void UpdateWithMeansContentEquality()
        {
            var helper = GetEqualsByContentTestHelper();
            helper.CheckEqualityAfterUpdate((left, right) => left.UpdateWith(right));
        }

        [Test]
        public void UpdateWithNullThrowsNoException()
        {
            var helper = GetEqualsByContentTestHelper();
            var left = helper.GetFilledEntity(helper.EqualityParameterList);

            Assert.DoesNotThrow(() => left.UpdateWith(null));
        }

        [TestCase("06:00:00", "14:00:00", "14:00:00", "22:00:00", "22:00:00", "06:00:00")]
        [TestCase("23:00:00", "14:00:00", "14:00:00", "22:00:00", "22:00:00", "23:00:00")]
        [TestCase("06:00:00", "14:00:00", "14:00:00", "04:00:00", "04:00:00", "06:00:00")]
        public void ValidateChecksAllShiftsForCorrectness(TimeSpan firstStart, TimeSpan firstEnd, TimeSpan secondStart, TimeSpan secondEnd, TimeSpan thirdStart, TimeSpan thirdEnd)
        {
            var entity = new ShiftManagement()
            {
                FirstShiftStart = firstStart,
                FirstShiftEnd = firstEnd,
                SecondShiftStart = secondStart,
                SecondShiftEnd = secondEnd,
                ThirdShiftStart = thirdStart,
                ThirdShiftEnd = thirdEnd,
                IsSecondShiftActive = true,
                IsThirdShiftActive = true
            };

            Assert.IsFalse(entity.Validate().Any());
        }

        [TestCase("06:00:00", "15:00:00", "14:00:00", "22:00:00", "22:00:00", "06:00:00", nameof(ShiftManagement.FirstShiftEnd), nameof(ShiftManagement.SecondShiftStart))]
        [TestCase("23:00:00", "14:00:00", "21:00:00", "20:00:00", "22:00:00", "23:00:00", nameof(ShiftManagement.SecondShiftStart), nameof(ShiftManagement.SecondShiftEnd))]
        [TestCase("05:00:00", "14:00:00", "14:00:00", "22:00:00", "22:00:00", "06:00:00", nameof(ShiftManagement.ThirdShiftEnd), nameof(ShiftManagement.ThirdShiftEnd))]
        public void ValidateChecksAllShiftsForIncorrectness(TimeSpan firstStart, TimeSpan firstEnd, TimeSpan secondStart, TimeSpan secondEnd, TimeSpan thirdStart, TimeSpan thirdEnd, params string[] invalidShiftNames)
        {
            var entity = new ShiftManagement()
            {
                FirstShiftStart = firstStart,
                FirstShiftEnd = firstEnd,
                SecondShiftStart = secondStart,
                SecondShiftEnd = secondEnd,
                ThirdShiftStart = thirdStart,
                ThirdShiftEnd = thirdEnd,
                IsSecondShiftActive = true,
                IsThirdShiftActive = true
            };

            Assert.IsTrue(entity.Validate().SequenceEqual(invalidShiftNames));
        }

        [TestCase("06:00:00", "14:00:00", "14:00:00", "22:00:00")]
        [TestCase("23:00:00", "14:00:00", "14:00:00", "22:00:00")]
        [TestCase("06:00:00", "14:00:00", "14:00:00", "04:00:00")]
        public void ValidateChecksFirstAndSecondShiftForCorrectness(TimeSpan firstStart, TimeSpan firstEnd, TimeSpan secondStart, TimeSpan secondEnd)
        {
            var entity = new ShiftManagement()
            {
                FirstShiftStart = firstStart,
                FirstShiftEnd = firstEnd,
                SecondShiftStart = secondStart,
                SecondShiftEnd = secondEnd,
                IsSecondShiftActive = true,
                IsThirdShiftActive = false
            };

            Assert.IsFalse(entity.Validate().Any());
        }

        [TestCase("06:00:00", "15:00:00", "14:00:00", "22:00:00", nameof(ShiftManagement.FirstShiftEnd), nameof(ShiftManagement.SecondShiftStart))]
        [TestCase("23:00:00", "14:00:00", "21:00:00", "20:00:00", nameof(ShiftManagement.SecondShiftStart), nameof(ShiftManagement.SecondShiftEnd))]
        [TestCase("05:00:00", "14:00:00", "14:00:00", "06:00:00", nameof(ShiftManagement.SecondShiftEnd), nameof(ShiftManagement.SecondShiftEnd))]
        public void ValidateChecksFirstAndSecondShiftForIncorrectness(TimeSpan firstStart, TimeSpan firstEnd, TimeSpan secondStart, TimeSpan secondEnd, params string[] invalidShiftNames)
        {
            var entity = new ShiftManagement()
            {
                FirstShiftStart = firstStart,
                FirstShiftEnd = firstEnd,
                SecondShiftStart = secondStart,
                SecondShiftEnd = secondEnd,
                IsSecondShiftActive = true,
                IsThirdShiftActive = false
            };

            Assert.IsTrue(entity.Validate().SequenceEqual(invalidShiftNames));
        }

        [TestCase("06:00:00", "14:00:00", "14:00:00", "22:00:00")]
        [TestCase("23:00:00", "14:00:00", "14:00:00", "22:00:00")]
        [TestCase("06:00:00", "14:00:00", "14:00:00", "04:00:00")]
        public void ValidateChecksFirstAndThirdShiftForCorrectness(TimeSpan firstStart, TimeSpan firstEnd, TimeSpan thirdStart, TimeSpan thirdEnd)
        {
            var entity = new ShiftManagement()
            {
                FirstShiftStart = firstStart,
                FirstShiftEnd = firstEnd,
                ThirdShiftStart = thirdStart,
                ThirdShiftEnd = thirdEnd,
                IsSecondShiftActive = false,
                IsThirdShiftActive = true
            };

            Assert.IsFalse(entity.Validate().Any());
        }

        [TestCase("06:00:00", "15:00:00", "14:00:00", "22:00:00", nameof(ShiftManagement.FirstShiftEnd), nameof(ShiftManagement.ThirdShiftStart))]
        [TestCase("23:00:00", "14:00:00", "21:00:00", "20:00:00", nameof(ShiftManagement.ThirdShiftStart), nameof(ShiftManagement.ThirdShiftEnd))]
        [TestCase("05:00:00", "14:00:00", "14:00:00", "06:00:00", nameof(ShiftManagement.ThirdShiftEnd), nameof(ShiftManagement.ThirdShiftEnd))]
        public void ValidateChecksFirstAndThirdShiftForIncorrectness(TimeSpan firstStart, TimeSpan firstEnd, TimeSpan thirdStart, TimeSpan thirdEnd, params string[] invalidShiftNames)
        {
            var entity = new ShiftManagement()
            {
                FirstShiftStart = firstStart,
                FirstShiftEnd = firstEnd,
                ThirdShiftStart = thirdStart,
                ThirdShiftEnd = thirdEnd,
                IsSecondShiftActive = false,
                IsThirdShiftActive = true
            };
            
            Assert.IsTrue(entity.Validate().SequenceEqual(invalidShiftNames));
        }



        private static IEnumerable<(EqualityParameter<ShiftManagement>, EqualityTestHelper<ShiftManagement>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<ShiftManagement> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<ShiftManagement>(
                (left, right) => left.EqualsByContent(right),
                () => new ShiftManagement(),
                new List<EqualityParameter<ShiftManagement>>()
                {
                    new EqualityParameter<ShiftManagement>()
                    {
                        SetParameter = (entity, value) => entity.FirstShiftStart = (TimeSpan)value,
                        CreateParameterValue = () => TimeSpan.FromTicks(9687),
                        CreateOtherParameterValue = () => TimeSpan.FromTicks(7895453),
                        ParameterName = nameof(ShiftManagement.FirstShiftStart)
                    },
                    new EqualityParameter<ShiftManagement>()
                    {
                        SetParameter = (entity, value) => entity.FirstShiftEnd = (TimeSpan)value,
                        CreateParameterValue = () => TimeSpan.FromTicks(9687),
                        CreateOtherParameterValue = () => TimeSpan.FromTicks(7895453),
                        ParameterName = nameof(ShiftManagement.FirstShiftEnd)
                    },
                    new EqualityParameter<ShiftManagement>()
                    {
                        SetParameter = (entity, value) => entity.SecondShiftStart = (TimeSpan)value,
                        CreateParameterValue = () => TimeSpan.FromTicks(9687),
                        CreateOtherParameterValue = () => TimeSpan.FromTicks(7895453),
                        ParameterName = nameof(ShiftManagement.SecondShiftStart)
                    },
                    new EqualityParameter<ShiftManagement>()
                    {
                        SetParameter = (entity, value) => entity.SecondShiftEnd = (TimeSpan)value,
                        CreateParameterValue = () => TimeSpan.FromTicks(9687),
                        CreateOtherParameterValue = () => TimeSpan.FromTicks(7895453),
                        ParameterName = nameof(ShiftManagement.SecondShiftEnd)
                    },
                    new EqualityParameter<ShiftManagement>()
                    {
                        SetParameter = (entity, value) => entity.ThirdShiftStart = (TimeSpan)value,
                        CreateParameterValue = () => TimeSpan.FromTicks(9687),
                        CreateOtherParameterValue = () => TimeSpan.FromTicks(7895453),
                        ParameterName = nameof(ShiftManagement.ThirdShiftStart)
                    },
                    new EqualityParameter<ShiftManagement>()
                    {
                        SetParameter = (entity, value) => entity.ThirdShiftEnd = (TimeSpan)value,
                        CreateParameterValue = () => TimeSpan.FromTicks(9687),
                        CreateOtherParameterValue = () => TimeSpan.FromTicks(7895453),
                        ParameterName = nameof(ShiftManagement.ThirdShiftEnd)
                    },
                    new EqualityParameter<ShiftManagement>()
                    {
                        SetParameter = (entity, value) => entity.IsSecondShiftActive = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(ShiftManagement.IsSecondShiftActive)
                    },
                    new EqualityParameter<ShiftManagement>()
                    {
                        SetParameter = (entity, value) => entity.IsThirdShiftActive = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(ShiftManagement.IsThirdShiftActive)
                    },
                    new EqualityParameter<ShiftManagement>()
                    {
                        SetParameter = (entity, value) => entity.ChangeOfDay = (TimeSpan)value,
                        CreateParameterValue = () => TimeSpan.FromTicks(9687),
                        CreateOtherParameterValue = () => TimeSpan.FromTicks(7895453),
                        ParameterName = nameof(ShiftManagement.ChangeOfDay)
                    },
                    new EqualityParameter<ShiftManagement>()
                    {
                        SetParameter = (entity, value) => entity.FirstDayOfWeek = (DayOfWeek)value,
                        CreateParameterValue = () => DayOfWeek.Monday,
                        CreateOtherParameterValue = () => DayOfWeek.Sunday,
                        ParameterName = nameof(ShiftManagement.FirstDayOfWeek)
                    }
                });
        }
    }
}
