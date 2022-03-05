using System.Collections.Generic;
using Core.Entities;
using NUnit.Framework;
using TestHelper.Checker;

namespace Core.Test.Entities
{
    class LocationDirectoryTest
    {
        [Test]
        public void EqualsByIdWithDifferentIdsMeansInequality()
        {
            var left = new LocationDirectory() {Id = new LocationDirectoryId(97486153) };
            var right = new LocationDirectory() { Id = new LocationDirectoryId(147896325) };

            Assert.IsFalse(left.EqualsById(right));
        }

        [Test]
        public void EqualsByIdWithNullMeansInequality()
        {
            var left = new LocationDirectory() { Id = new LocationDirectoryId(97486153) };

            Assert.IsFalse(left.EqualsById(null));
        }

        [Test]
        public void EqualsByIdWithEqualIdsMeansEquality()
        {
            var left = new LocationDirectory() { Id = new LocationDirectoryId(97486153) };
            var right = new LocationDirectory() { Id = new LocationDirectoryId(97486153) };

            Assert.IsTrue(left.EqualsById(right));
        }

        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<LocationDirectory> parameter, EqualityTestHelper<LocationDirectory> helper) helperTuple)
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

        [Test]
        public void CopyMeansContentEqualityButNotReferenceEquality()
        {
            var helper = GetEqualsByContentTestHelper();
            helper.CheckEqualityAfterCopy(entity => entity.CopyDeep());
        }




        private static IEnumerable<(EqualityParameter<LocationDirectory>, EqualityTestHelper<LocationDirectory>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<LocationDirectory> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<LocationDirectory>(
                (left, right) => left.EqualsByContent(right),
                () => new LocationDirectory(),
                new List<EqualityParameter<LocationDirectory>>()
                {
                    new EqualityParameter<LocationDirectory>()
                    {
                        SetParameter = (entity, value) => entity.Id = (LocationDirectoryId)value,
                        CreateParameterValue = () => new LocationDirectoryId(8520),
                        CreateOtherParameterValue = () => new LocationDirectoryId(7895453),
                        ParameterName = nameof(LocationDirectory.Id)
                    },
                    new EqualityParameter<LocationDirectory>()
                    {
                        SetParameter = (entity, value) => entity.Name = (LocationDirectoryName)value,
                        CreateParameterValue = () => new LocationDirectoryName("üoäipöuozltfd"),
                        CreateOtherParameterValue = () => new LocationDirectoryName("xrctvzuhbhijo"),
                        ParameterName = nameof(LocationDirectory.Name)
                    },
                    new EqualityParameter<LocationDirectory>()
                    {
                        SetParameter = (entity, value) => entity.Id = (LocationDirectoryId)value,
                        CreateParameterValue = () => new LocationDirectoryId(8520),
                        CreateOtherParameterValue = () => new LocationDirectoryId(7895453),
                        ParameterName = nameof(LocationDirectory.ParentId)
                    }
                });
        }
    }
}
