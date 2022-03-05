using Core.UseCases;
using NUnit.Framework;

namespace Core.Test.UseCases
{
    class QstInformationUseCaseTest
    {
        #region Interface implementations
        private class QstInformationDataLocal : IQstInformationDataLocal
        {
            public string QstVersion { get; set; }
            public string ComputerName { get; set; }
            public string LogPackageFilePath { get; set; }
            public bool LogPackageCreated { get; set; } = false;


            public string LoadQstVersion()
            {
                return QstVersion;
            }

            public string LoadComputerName()
            {
                return ComputerName;
            }

            public void CreateLogPackage(string filePath)
            {
                LogPackageFilePath = filePath;
                LogPackageCreated = true;
            }
        }

        private class QstInformationData : IQstInformationData
        {
            public string ServerVersion { get; set; }

            public string LoadServerVersion()
            {
                return ServerVersion;
            }
        }

        private class QstInformationGui : IQstInformationGui
        {
            public string QstVersion { get; set; }
            public string ServerVersion { get; set; }
            public string ComputerName { get; set; }
            public string LogPackageFilePath { get; set; }


            public void ShowQstVersion(string qstVersion)
            {
                QstVersion = qstVersion;
            }

            public void ShowServerVersion(string serverVersion)
            {
                ServerVersion = serverVersion;
            }

            public void ShowComputerName(string computerName)
            {
                ComputerName = computerName;
            }

            public void ShowLogPackageSuccessMessage()
            {
            }
        }
        #endregion


        QstInformationDataLocal _dataLocal;
        QstInformationData _dataSoap;
        QstInformationGui _gui;
        QstInformationUseCase _useCase;


        [SetUp]
        public void QstInformationSetup()
        {
            _dataLocal = new QstInformationDataLocal();
            _dataSoap = new QstInformationData();
            _gui = new QstInformationGui();
            _useCase = new QstInformationUseCase(_dataLocal, _dataSoap, _gui);
        }


        [TestCase("QstVersion")]
        [TestCase("8.0.0.1")]
        public void LoadQstVersionTest(string qstVersion)
        {
            // Set default data
            _dataLocal.QstVersion = qstVersion;

            // Load data from to gui
            _useCase.LoadQstVersion();

            // Check if data is in gui
            Assert.AreEqual(_dataLocal.QstVersion, _gui.QstVersion);
        }

        [TestCase("Server Version")]
        [TestCase("0.0.0.1")]
        public void LoadServerVersionTest(string serverVersion)
        {
            // Set default data
            _dataSoap.ServerVersion = serverVersion;

            // Load data from to gui
            _useCase.LoadServerVersion();

            // Check if data is in gui
            Assert.AreEqual(_dataSoap.ServerVersion, _gui.ServerVersion);
        }

        [TestCase("Computer")]
        [TestCase("Cat")]
        public void LoadComputerNameTest(string computerName)
        {
            // Set default data
            _dataLocal.ComputerName = computerName;

            // Load data from to gui
            _useCase.LoadComputerName();

            // Check if data is in gui
            Assert.AreEqual(_dataLocal.ComputerName, _gui.ComputerName);
        }

        [TestCase("FilePath")]
        [TestCase("OtherFilePath")]
        public void CreateLogPackageTest(string filePath)
        {
            // Set default data
            _gui.LogPackageFilePath = filePath;

            // Create og package
            _useCase.CreateLogPackage(_gui.LogPackageFilePath);

            // Check if package was created
            Assert.AreEqual(_dataLocal.LogPackageCreated, true);
            Assert.AreEqual(_gui.LogPackageFilePath, _dataLocal.LogPackageFilePath);
        }
    }
}
