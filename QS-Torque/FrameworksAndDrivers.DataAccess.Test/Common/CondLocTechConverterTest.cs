using System;
using System.Collections.Generic;
using Common.Types.Enums;
using FrameworksAndDrivers.DataAccess.Common;
using FrameworksAndDrivers.DataAccess.DbEntities;
using NUnit.Framework;
using Server.Core.Entities;
using Server.TestHelper.Factories;
using Server.TestHelper.Mocks;

namespace FrameworksAndDrivers.DataAccess.Test.Common
{
    public class CondLocTechConverterTest
    {
        [Test]
        public void ConvertEntityToCondLocTechWithNotImplementedTechThrowsNotImplementedException()
        {
            Assert.Throws<NotImplementedException>(() =>
            {
                CondLocTechConverter.ConvertEntityToCondLocTech(new ProcessControlTechMock());
            });
        }

        [Test]
        public void ConvertEntityToCondLocTechWithNullReturnsNull()
        {
            Assert.IsNull(CondLocTechConverter.ConvertEntityToCondLocTech(null));
        }


        static IEnumerable<QstProcessControlTech> QstProcessControlTechData = new List<QstProcessControlTech>()
        {
            CreateQstProcessControlTech.Randomized(123256),
            CreateQstProcessControlTech.Randomized(457678)
        };

        [TestCaseSource(nameof(QstProcessControlTechData))]
        public void ConvertQstEntityToCondLocTechReturnsCorrectValue(QstProcessControlTech processControlTech)
        {
            var result = CondLocTechConverter.ConvertEntityToCondLocTech(processControlTech);
            Assert.AreEqual(processControlTech.Id.ToLong(), result.SEQID);
            Assert.AreEqual(processControlTech.ProcessControlConditionId.ToLong(), result.CONDLOCID);
            Assert.AreEqual(processControlTech.ManufacturerId, result.HERSTELLERID);
            Assert.AreEqual(processControlTech.TestMethod, result.METHODE);
            Assert.AreEqual(processControlTech.Alive, result.ALIVE);
            Assert.AreEqual(processControlTech.Extension.Id.ToLong(), result.EXTENSIONID);
            Assert.AreEqual(processControlTech.AngleLimitMt, result.I0);
            Assert.AreEqual(processControlTech.StartMeasurementPeak, result.F0);
            Assert.AreEqual(processControlTech.StartAngleCountingPa, result.F1);
            Assert.AreEqual(processControlTech.AngleForFurtherTurningPa, result.F2);
            Assert.AreEqual(processControlTech.TargetAnglePa, result.F3);
            Assert.AreEqual(processControlTech.StartMeasurementPa, result.F4);
            Assert.AreEqual(processControlTech.AlarmTorquePa, result.F5);
            Assert.AreEqual(processControlTech.AlarmAnglePa, result.F6);
            Assert.AreEqual(processControlTech.MinimumTorqueMt, result.F7);
            Assert.AreEqual(processControlTech.StartAngleMt, result.F8);
            Assert.AreEqual(processControlTech.StartMeasurementMt, result.F9);
            Assert.AreEqual(processControlTech.AlarmTorqueMt, result.F10);
            Assert.AreEqual(processControlTech.AlarmAngleMt, result.F11);
        }

        [Test]
        public void ConvertCondLocTechToEntityWithNotImplementedTechThrowsNotImplementedException()
        {
            Assert.Throws<NotImplementedException>(() =>
            {
                CondLocTechConverter.ConvertCondLocTechToEntity(new CondLocTech(){HERSTELLERID = ManufacturerIds.ID_BLM});
            });
        }

        [Test]
        public void ConvertCondLocTechToEntityWithNullReturnsNull()
        {
            Assert.IsNull(CondLocTechConverter.ConvertCondLocTechToEntity(null));
        }

        static IEnumerable<CondLocTech> QstCondLocTechData = new List<CondLocTech>()
        {
            new CondLocTech()
            {
                SEQID = 1,
                CONDLOCID = 2,
                HERSTELLERID = ManufacturerIds.ID_QST,
                METHODE = TestMethod.QST_PA,
                ALIVE = true,
                EXTENSIONID = 6,
                I0 = 1,
                F0 = 2.5,
                F1 = 6.5,
                F2 = 8.56,
                F3 = 0.53,
                F4 = 4.55,
                F5 = 5.53,
                F6 = 2.50,
                F7 = 6.58,
                F8 = 7.57,
                F9 = 2.55,
                F10 = 52.52,
                F11 = 24.51
            },
            new CondLocTech()
            {
                SEQID = 61,
                CONDLOCID = 92,
                HERSTELLERID = ManufacturerIds.ID_QST,
                METHODE = TestMethod.QST_PEAK,
                ALIVE = false,
                EXTENSIONID = 5,
                I0 = 51,
                F0 = 72.5,
                F1 = 96.5,
                F2 = 58.56,
                F3 = 70.53,
                F4 = 44.55,
                F5 = 65.53,
                F6 = 24.50,
                F7 = 36.58,
                F8 = 57.57,
                F9 = 26.55,
                F10 = 52.52,
                F11 = 24.51
            }
        };

        [TestCaseSource(nameof(QstCondLocTechData))]
        public void ConvertQstCondLocTechToEntityReturnsCorrectValue(CondLocTech condLocTech)
        {
            var result = (QstProcessControlTech)CondLocTechConverter.ConvertCondLocTechToEntity(condLocTech);
            Assert.AreEqual(condLocTech.SEQID, result.Id.ToLong());
            Assert.AreEqual(condLocTech.CONDLOCID, result.ProcessControlConditionId.ToLong());
            Assert.AreEqual(condLocTech.HERSTELLERID, result.ManufacturerId);
            Assert.AreEqual(condLocTech.METHODE, result.TestMethod);
            Assert.AreEqual(condLocTech.ALIVE, result.Alive);
            Assert.AreEqual(condLocTech.EXTENSIONID, result.Extension.Id.ToLong());
            Assert.AreEqual(condLocTech.I0, result.AngleLimitMt);
            Assert.AreEqual(condLocTech.F0, result.StartMeasurementPeak);
            Assert.AreEqual(condLocTech.F1, result.StartAngleCountingPa);
            Assert.AreEqual(condLocTech.F2, result.AngleForFurtherTurningPa);
            Assert.AreEqual(condLocTech.F3, result.TargetAnglePa);
            Assert.AreEqual(condLocTech.F4, result.StartMeasurementPa);
            Assert.AreEqual(condLocTech.F5, result.AlarmTorquePa);
            Assert.AreEqual(condLocTech.F6, result.AlarmAnglePa);
            Assert.AreEqual(condLocTech.F7, result.MinimumTorqueMt);
            Assert.AreEqual(condLocTech.F8, result.StartAngleMt);
            Assert.AreEqual(condLocTech.F9, result.StartMeasurementMt);
            Assert.AreEqual(condLocTech.F10, result.AlarmTorqueMt);
            Assert.AreEqual(condLocTech.F11, result.AlarmAngleMt);
        }
    }
}
