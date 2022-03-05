using System.Collections.Generic;
using Core.Entities;
using NUnit.Framework;
using TestHelper.Checker;

namespace Core.Test.Entities
{
    class PictureTest
    {
        [Test]
        public void EqualsByIdWithDifferentIdsMeansInequality()
        {
            var left = new Picture() { SeqId = 97486153 };
            var right = new Picture() { SeqId = 147896325 };

            Assert.IsFalse(left.EqualsById(right));
        }

        [Test]
        public void EqualsByIdWithNullMeansInequality()
        {
            var left = new Picture() { SeqId = 97486153 };

            Assert.IsFalse(left.EqualsById(null));
        }

        [Test]
        public void EqualsByIdWithEqualIdsMeansEquality()
        {
            var left = new Picture() { SeqId = 97486153 };
            var right = new Picture() { SeqId = 97486153 };

            Assert.IsTrue(left.EqualsById(right));
        }

        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<Picture> parameter, EqualityTestHelper<Picture> helper) helperTuple)
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



        private static IEnumerable<(EqualityParameter<Picture>, EqualityTestHelper<Picture>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<Picture> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<Picture>(
                (left, right) => left.EqualsByContent(right),
                () => new Picture(),
                new List<EqualityParameter<Picture>>()
                {
                    new EqualityParameter<Picture>()
                    {
                        SetParameter = (entity, value) => entity.SeqId = (long)value,
                        CreateParameterValue = () => 8520L,
                        CreateOtherParameterValue = () => 7895453L,
                        ParameterName = nameof(Picture.SeqId)
                    },
                    new EqualityParameter<Picture>()
                    {
                        SetParameter = (entity, value) => entity.NodeId = (int)value,
                        CreateParameterValue = () => 8520,
                        CreateOtherParameterValue = () => 7895453,
                        ParameterName = nameof(Picture.NodeId)
                    },
                    new EqualityParameter<Picture>()
                    {
                        SetParameter = (entity, value) => entity.NodeSeqId = (int)value,
                        CreateParameterValue = () => 8520,
                        CreateOtherParameterValue = () => 7895453,
                        ParameterName = nameof(Picture.NodeSeqId)
                    },
                    new EqualityParameter<Picture>()
                    {
                        SetParameter = (entity, value) => entity.FileName = (string)value,
                        CreateParameterValue = () => "pßr0t9iugfzhvjckld",
                        CreateOtherParameterValue = () => "ä-plöitjmhfgdj5ez",
                        ParameterName = nameof(Picture.FileName)
                    },
                    new EqualityParameter<Picture>()
                    {
                        SetParameter = (entity, value) => entity.FileType = (long)value,
                        CreateParameterValue = () => 8520L,
                        CreateOtherParameterValue = () => 7895453L,
                        ParameterName = nameof(Picture.FileType)
                    }
                });
        }
    }
}
