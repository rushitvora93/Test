﻿using System;
using Common.Types.Enums;
using Core.Entities;
using Core.Enums;
using NUnit.Framework;
using TestHelper.Factories;

namespace Core.Test.Entities
{
    class ClassicChkTestTest
    {
        [TestCase(MeaUnit.Nm, MeaUnit.Nm, MeaUnit.Deg, 5.0, 6.0, 5.0, 6.0)]
        [TestCase(MeaUnit.Deg, MeaUnit.Nm, MeaUnit.Deg, 7.0, 8.0, 8.0, 7.0)]
        public void GetNominalTorqueAndNominalAngleGetsCorrectValue(MeaUnit controledByUnitId, MeaUnit unit1Id, MeaUnit unit2Id,
            double nominalValueUnit1, double nominalValueUnit2, double resultNominalTorque, double resultNominalAngle)
        {
            var classicChkTest = new ClassicChkTest
            {
                ControlledByUnitId = controledByUnitId, Unit1Id = unit1Id,Unit2Id = unit2Id,
                NominalValueUnit1 = nominalValueUnit1, NominalValueUnit2 = nominalValueUnit2,
            };

            Assert.AreEqual(resultNominalTorque, classicChkTest.NominalTorque);
            Assert.AreEqual(resultNominalAngle, classicChkTest.NominalAngle);
        }

        [TestCase(MeaUnit.Nm, MeaUnit.Nm, MeaUnit.Deg, 5.0, 6.0, 5.0)]
        [TestCase(MeaUnit.Deg, MeaUnit.Nm, MeaUnit.Deg, 15.0, 16.0, 16.0)]
        public void GetLowerLimitGetsCorrectValue(MeaUnit controledByUnitId, MeaUnit unit1Id, MeaUnit unit2Id, double lowerlimit1, double lowerLimit2, double resultLowerLimit)
        {
            var classicChkTest = new ClassicChkTest
            {
                ControlledByUnitId = controledByUnitId,
                Unit1Id = unit1Id,
                Unit2Id = unit2Id,
                LowerLimitUnit1 = lowerlimit1,
                LowerLimitUnit2 = lowerLimit2,
            };

            Assert.AreEqual(resultLowerLimit, classicChkTest.LowerLimit);
        }


        [TestCase(MeaUnit.Nm, MeaUnit.Nm, MeaUnit.Deg, 5.0, 6.0, 5.0)]
        [TestCase(MeaUnit.Deg, MeaUnit.Nm, MeaUnit.Deg, 15.0, 16.0, 16.0)]
        public void GetUpperLimitGetsCorrectValue(MeaUnit controledByUnitId, MeaUnit unit1Id, MeaUnit unit2Id, double upperlimit1, double upperLimit2, double resultUpperLimit)
        {
            var classicChkTest = new ClassicChkTest
            {
                ControlledByUnitId = controledByUnitId,
                Unit1Id = unit1Id,
                Unit2Id = unit2Id,
                UpperLimitUnit1 = upperlimit1,
                UpperLimitUnit2 = upperLimit2,
            };

            Assert.AreEqual(resultUpperLimit, classicChkTest.UpperLimit);
        }


        [Test]
        public void GetTestValueByPositionReturnsNullWhenPositionNotFound()
        {
            var classicChkTest = new ClassicChkTest();
            classicChkTest.TestValues.Add(new ClassicChkTestValue() { Position = 1 });
            classicChkTest.TestValues.Add(new ClassicChkTestValue() { Position = 2 });
            classicChkTest.TestValues.Add(new ClassicChkTestValue() { Position = 3 });

            var classicChkTestValue = classicChkTest.GetTestValueByPosition(10);
            Assert.IsNull(classicChkTestValue);
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetTestValueByPositionReturnsCorrectChkTestValue(long position)
        {
            var classicChkTest = new ClassicChkTest();
            classicChkTest.TestValues.Add(new ClassicChkTestValue() { Position = 1 });
            classicChkTest.TestValues.Add(new ClassicChkTestValue() { Position = 2 });
            classicChkTest.TestValues.Add(new ClassicChkTestValue() { Position = 3 });

            var classicChkTestValue = classicChkTest.GetTestValueByPosition(position);
            Assert.AreEqual(position, classicChkTestValue.Position);
        }

        [TestCase(MeaUnit.Nm, MeaUnit.Nm, MeaUnit.Deg, 99, 66, 99)]
        [TestCase(MeaUnit.Deg, MeaUnit.Nm, MeaUnit.Deg, 1, 12, 12)]
        public void GetToleranceClassGetsCorrectValue(MeaUnit controledByUnitId, MeaUnit unit1Id, MeaUnit unit2Id, long toleranceUnit1Id, long toleranceUnit2Id, long toleranceClassIdResult)
        {
            var classicMfuTest = new ClassicMfuTest
            {
                ControlledByUnitId = controledByUnitId,
                Unit1Id = unit1Id,
                Unit2Id = unit2Id,
                ToleranceClassUnit1 = CreateToleranceClass.WithId(toleranceUnit1Id),
                ToleranceClassUnit2 = CreateToleranceClass.WithId(toleranceUnit2Id)
            };

            Assert.AreEqual(toleranceClassIdResult, classicMfuTest.ToleranceClass.Id.ToLong());
        }

        [TestCase(MeaUnit.Nm, MeaUnit.Nm, MeaUnit.Deg, 10, 20, 10)]
        [TestCase(MeaUnit.Nm, MeaUnit.Deg, MeaUnit.Nm, 23, 55, 55)]
        [TestCase(MeaUnit.Deg, MeaUnit.Nm, MeaUnit.Deg, 11, 40, 40)]
        [TestCase(MeaUnit.Deg, MeaUnit.Deg, MeaUnit.Nm, 74, 33, 74)]
        public void GetNominalValueByMeaUnitGetsCorrectValue(MeaUnit unit, MeaUnit unit1, MeaUnit unit2, double nominalValue1, double nominalValue2, double expectedNominalValue)
        {
            var classicMfuTest = new ClassicChkTest
            {
                Unit1Id = unit1,
                Unit2Id = unit2,
                NominalValueUnit1 = nominalValue1,
                NominalValueUnit2 = nominalValue2
            };
            Assert.AreEqual(expectedNominalValue, classicMfuTest.GetNominalValueByMeaUnit(unit));
        }

        [Test]
        public void GetNominalValueByMeaUnitWithWrongMeaUnitThrowsArgumentException()
        {
            var classicMfuTest = new ClassicChkTest
            {
                Unit1Id = MeaUnit.Nm,
                Unit2Id = MeaUnit.Deg
            };
            Assert.Throws<ArgumentException>(() => classicMfuTest.GetNominalValueByMeaUnit(MeaUnit.lbf_ft));
        }
    }
}
