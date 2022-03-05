using System;
using System.IO;
using Core.UseCases.Communication;
using FrameworksAndDrivers.Process;
using NUnit.Framework;
using TestHelper.Factories;

namespace FrameworksAndDrivers.DataGate.Test
{
    class CommunicationProgramControllerTest
    {
        [TestCase(@"C:\funPath\thing.exe")]
        [TestCase(@"C:\so_fun\joy\happytime.exe")]
        public void StartingProgramCallsStartProcessWithTestEquipmentPath(string programPath)
        {
            var processController = new ProcessControllerMock();
            var communicationProgramController = new CommunicationProgramController(processController);
            var testEquipment = CreateTestEquipment.WithDriverProgramPath(programPath);
            communicationProgramController.Start(testEquipment);
            Assert.AreEqual(programPath, processController.lastStartProcessParameterPathToBinary);
        }

        [Test]
        public void StartingProgramWithProgramNotFoundThrowsCommunicationProgramNotFoundException()
        {
            var processController = new ProcessControllerMock {StartProcessException = new FileNotFoundException()};
            var communicationProgramController = new CommunicationProgramController(processController);
            Assert.Throws<CommunicationProgramNotFoundException>(
                () => communicationProgramController.Start(CreateTestEquipment.Anonymous()));
        }

        [Test]
        public void StartingProgramWithOtherExceptionThrowsSameException()
        {
            var processController = new ProcessControllerMock {StartProcessException = new Exception()};
            var communicationProgramController = new CommunicationProgramController(processController);
            Assert.Throws<Exception>(() => communicationProgramController.Start(CreateTestEquipment.Anonymous()));
        }

        private class ProcessControllerMock: IProcessController
        {
            public void StartProcess(string pathToBinary)
            {
                if (StartProcessException != null)
                {
                    throw StartProcessException;
                }

                lastStartProcessParameterPathToBinary = pathToBinary;
            }

            public Exception StartProcessException = null;
            public string lastStartProcessParameterPathToBinary = null;
        }
    }
}
