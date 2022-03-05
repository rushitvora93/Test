using System.Collections.Generic;
using Core.Entities;
using NUnit.Framework;
using TestHelper.Checker;

namespace Core.Test.Entities
{
    class ManufacturerTest
    {
        [Test]
        public void EqualsByIdWithEqualIdMeansEquality()
        {
            var helper = GetEqualsByIdTestHelper();
            helper.CheckEqualityForParameterList();
        }

        [Test]
        public void EqualsByIdWithRightIsNullMeansInequality()
        {
            var helper = GetEqualsByIdTestHelper();
            helper.CheckInequalityWithRightIsNull();
        }

        [TestCaseSource(nameof(EqualsByIdTestSource))]
        public void EqualsByIdWithDifferentParameterMeansInequality((EqualityParameter<Manufacturer> parameter, EqualityTestHelper<Manufacturer> helper) helperTuple)
        {
            helperTuple.helper.CheckInequalityForParameter(helperTuple.parameter);
        }


        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<Manufacturer> parameter, EqualityTestHelper<Manufacturer> helper) helperTuple)
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



        private static IEnumerable<(EqualityParameter<Manufacturer>, EqualityTestHelper<Manufacturer>)> EqualsByIdTestSource()
        {
            var helper = GetEqualsByIdTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<Manufacturer> GetEqualsByIdTestHelper()
        {
            return new EqualityTestHelper<Manufacturer>(
                (left, right) => left.EqualsById(right),
                () => new Manufacturer(), 
                new List<EqualityParameter<Manufacturer>>()
                {
                    new EqualityParameter<Manufacturer>()
                    {
                        SetParameter = (entity, value) => entity.Id = value as ManufacturerId,
                        CreateParameterValue = () => new ManufacturerId(8520),
                        CreateOtherParameterValue = () => new ManufacturerId(7895453)
                    }
                });
        }

        private static IEnumerable<(EqualityParameter<Manufacturer>, EqualityTestHelper<Manufacturer>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<Manufacturer> GetEqualsByContentTestHelper()
        {
            return  new EqualityTestHelper<Manufacturer>(
                (left, right) => left.EqualsByContent(right),
                () => new Manufacturer(), 
                new List<EqualityParameter<Manufacturer>>()
                {
                    new EqualityParameter<Manufacturer>()
                    {
                        SetParameter = (entity, value) => entity.Id = value as ManufacturerId,
                        CreateParameterValue = () => new ManufacturerId(8520),
                        CreateOtherParameterValue = () => new ManufacturerId(7895453),
                        ParameterName = nameof(Manufacturer.Id)
                    },
                    new EqualityParameter<Manufacturer>()
                    {
                        SetParameter = (entity, value) => entity.Name = value as ManufacturerName,
                        CreateParameterValue = () => new ManufacturerName("ortigujm"),
                        CreateOtherParameterValue = () => new ManufacturerName("pgoiujhpö9ij"),
                        ParameterName = nameof(Manufacturer.Name)
                    },
                    new EqualityParameter<Manufacturer>()
                    {
                        SetParameter = (entity, value) => entity.Person = value as string,
                        CreateParameterValue = () => "fghdysfhsfdth",
                        CreateOtherParameterValue = () => "pgoiujhpgdxhö9ij",
                        ParameterName = nameof(Manufacturer.Person)
                    },
                    new EqualityParameter<Manufacturer>()
                    {
                        SetParameter = (entity, value) => entity.PhoneNumber = value as string,
                        CreateParameterValue = () => "swrzw",
                        CreateOtherParameterValue = () => " tdnrt",
                        ParameterName = nameof(Manufacturer.PhoneNumber)
                    },
                    new EqualityParameter<Manufacturer>()
                    {
                        SetParameter = (entity, value) => entity.Fax = value as string,
                        CreateParameterValue = () => "eargsrfg",
                        CreateOtherParameterValue = () => "sdfgr",
                        ParameterName = nameof(Manufacturer.Fax)
                    },
                    new EqualityParameter<Manufacturer>()
                    {
                        SetParameter = (entity, value) => entity.Street = value as string,
                        CreateParameterValue = () => "sdgrfsfgrsdfsd",
                        CreateOtherParameterValue = () => "47524753875",
                        ParameterName = nameof(Manufacturer.Street)
                    },
                    new EqualityParameter<Manufacturer>()
                    {
                        SetParameter = (entity, value) => entity.HouseNumber = value as string,
                        CreateParameterValue = () => "ayrdsgshst",
                        CreateOtherParameterValue = () => "huiouziktu",
                        ParameterName = nameof(Manufacturer.HouseNumber)
                    },
                    new EqualityParameter<Manufacturer>()
                    {
                        SetParameter = (entity, value) => entity.Plz = value as string,
                        CreateParameterValue = () => "gw45zergfb",
                        CreateOtherParameterValue = () => "oiuz76trdfjzt",
                        ParameterName = nameof(Manufacturer.Plz)
                    },
                    new EqualityParameter<Manufacturer>()
                    {
                        SetParameter = (entity, value) => entity.City = value as string,
                        CreateParameterValue = () => "6yzxecrfgb",
                        CreateOtherParameterValue = () => "7.z,utmzrntzfdbtf",
                        ParameterName = nameof(Manufacturer.City)
                    },
                    new EqualityParameter<Manufacturer>()
                    {
                        SetParameter = (entity, value) => entity.Country = value as string,
                        CreateParameterValue = () => ",zkumnrdbtf",
                        CreateOtherParameterValue = () => "<q6wz7urdtifz",
                        ParameterName = nameof(Manufacturer.Country)
                    },
                    new EqualityParameter<Manufacturer>()
                    {
                        SetParameter = (entity, value) => entity.Comment = value as string,
                        CreateParameterValue = () => "<wysz6e7dutfizo",
                        CreateOtherParameterValue = () => "ui,hzugmkngfzbtf",
                        ParameterName = nameof(Manufacturer.Comment)
                    }
                });
        }
    }
}
