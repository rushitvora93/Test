using System;
using System.Collections.Generic;
using Common.Types.Enums;
using FrameworksAndDrivers.NetworkView.Common;
using NUnit.Framework;
using Server.Core.Entities;
using Server.TestHelper.Factories;
using Server.TestHelper.Mocks;
using ProcessControlTech = DtoTypes.ProcessControlTech;

namespace FrameworksAndDrivers.NetworkView.Test.Common
{
    public class ProcessControlTechConverterTest
    {
        [Test]
        public void ConvertEntityToDtoWithNotImplementedTechThrowsNotImplementedException()
        {
            Assert.Throws<NotImplementedException>(() =>
            {
                ProcessControlTechConverter.ConvertEntityToDto(new ProcessControlTechMock());
            });
        }

        [Test]
        public void ConvertEntityToDtoWithNullReturnsNull()
        {
            Assert.IsNull(ProcessControlTechConverter.ConvertEntityToDto(null));
        }

        static IEnumerable<QstProcessControlTech> QstProcessControlTechData = new List<QstProcessControlTech>()
        {
            CreateQstProcessControlTech.Randomized(123256),
            CreateQstProcessControlTech.Randomized(457678)
        };

        [TestCaseSource(nameof(QstProcessControlTechData))]
        public void ConvertQstEntityToCondLocTechReturnsCorrectValue(QstProcessControlTech processControlTech)
        {
            var result = ProcessControlTechConverter.ConvertEntityToDto(processControlTech);
            Assert.IsTrue(EqualityChecker.CompareQstProcessControlTechToDto(processControlTech, result.QstProcessControlTech));
        }

        [Test]
        public void ConvertDtoToEntityWithNotImplementedTechThrowsNotImplementedException()
        {
            Assert.Throws<NotImplementedException>(() =>
            {
                ProcessControlTechConverter.ConvertDtoToEntity(new DtoTypes.ProcessControlTech());
            });
        }

        [Test]
        public void ConvertDtoToEntityWithNullReturnsNull()
        {
            Assert.IsNull(ProcessControlTechConverter.ConvertDtoToEntity(null));
        }

        static IEnumerable<DtoTypes.ProcessControlTech> ProcessControlTechData = new List<ProcessControlTech>()
        {
            new ProcessControlTech()
            {
                QstProcessControlTech = DtoFactory.CreateQstProcessControlTechRandomized(43546)
            },
            new ProcessControlTech()
            {
                QstProcessControlTech = DtoFactory.CreateQstProcessControlTechRandomized(43564)
            }
        };

        [TestCaseSource(nameof(ProcessControlTechData))]
        public void ConvertQstEntityToCondLocTechReturnsCorrectValue(DtoTypes.ProcessControlTech processControlTech)
        {
            var result = ProcessControlTechConverter.ConvertDtoToEntity(processControlTech);
            Assert.IsTrue(EqualityChecker.CompareProcessControlTechToDto(result, processControlTech));
        }
    }
}
