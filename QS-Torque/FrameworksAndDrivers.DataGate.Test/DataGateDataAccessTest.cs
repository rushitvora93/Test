using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Core.UseCases.Communication;
using Core.UseCases.Communication.DataGate;
using NUnit.Framework;
using TestHelper.Factories;

namespace FrameworksAndDrivers.DataGate.Test
{
    public class DataGateDataAccessTest
    {
        [TestCase("C:\\TEMP\\DataGate.xml")]
        [TestCase("C:\\Other\\InformationDoor.xml")]
        public void TransferringOpensDataGateXml(string path)
        {
            var equipment = CreateTestEquipment.WithCommunicationFilePath(path);
            var environment = new Environment();
            environment.dataGate.TransferToTestEquipment(null, equipment, status => { });
            Assert.AreEqual(path, environment.mocks.fileSystem.lastOpenFileParameterPath);
        }

        [Test]
        public void TransferringPutsSemanticModelIntoFileGenerator()
        {
            var semanticModel = new SemanticModel(null);
            var environment = new Environment();
            environment.dataGate.TransferToTestEquipment(
                semanticModel, 
                CreateTestEquipment.Anonymous(),
                status => { });
            Assert.AreSame(semanticModel, environment.mocks.generator.lastAcceptParameterSemanticModel);
        }

        [TestCase("content")]
        [TestCase("oasd fkja ejaidjfaks jfweio")]
        public void TransferringWritesGeneratorResultsIntoFile(string content)
        {
            var environment = new Environment();
            environment.mocks.generator.dataGateFileContentReturn = content;
            environment.dataGate.TransferToTestEquipment(
                new SemanticModel(null),
                CreateTestEquipment.Anonymous(),
                status => { });
            Assert.AreEqual(
                content,
                Encoding.ASCII.GetString(environment.mocks.fileSystem.pseudoFileStream.ToArray()));
        }

        [Test]
        public void TransferringSetsLastTransferFinishedToFalse()
        {
            var environment = new Environment();
            environment.dataGate.TransferToTestEquipment(
                new SemanticModel(null),
                CreateTestEquipment.Anonymous(),
                status => { });
            Assert.IsFalse(environment.dataGate.LastTransferFinished());
        }

        [TestCase(@"C:\TOLLERPFAD\file.xml")]
        [TestCase(@"D:\Pizza\yay\status.othertype")]
        public void TransferringStartsWatchingStatusPath(string path)
        {
            var environment = new Environment();
            environment.dataGate.TransferToTestEquipment(
                new SemanticModel(null),
                CreateTestEquipment.WithModelStatusPath(path),
                status => { });
            Assert.AreEqual(path, environment.mocks.fileSystem.lastWatchForFileParameterPath);
        }

        [Test]
        public void TransferringParsesStatusFileWhenFound()
        {
            var statusFile = new XElement("name");
            var environment = new Environment();
            environment.mocks.fileSystem.triggerWatchForFileEvaluateAction = true;
            environment.mocks.fileSystem.nextWatchForFileEvaluateActionParameter = statusFile;
            environment.dataGate.TransferToTestEquipment(
                new SemanticModel(null),
                CreateTestEquipment.Anonymous(),
                status => { });
            Assert.AreSame(statusFile, environment.mocks.statusParser.lastParseParameterStatusFile);
        }

        [Test]
        public void TransferringForwardsParsedStatusToStatusHandler()
        {
            var expectedStatus = new TransmissionStatus();
            TransmissionStatus resultStatus = null;
            var environment = new Environment();
            environment.mocks.fileSystem.triggerWatchForFileEvaluateAction = true;
            environment.mocks.statusParser.nextParseResult = expectedStatus;
            environment.dataGate.TransferToTestEquipment(
                new SemanticModel(null),
                CreateTestEquipment.Anonymous(),
                status => resultStatus = status);
            Assert.AreSame(expectedStatus, resultStatus);
        }

        [Test]
        public void TransferringAndReceivingStatusFileSetsTransmissionFinishedToTrue()
        {
            var environment = new Environment();
            environment.mocks.fileSystem.triggerWatchForFileEvaluateAction = true;
            environment.dataGate.TransferToTestEquipment(
                new SemanticModel(null),
                CreateTestEquipment.Anonymous(),
                status => { });
            Assert.IsTrue(environment.dataGate.LastTransferFinished());
        }

        [Test]
        public void LastTransferFinishedStartsAsTrue()
        {
            Assert.IsTrue(new Environment().dataGate.LastTransferFinished());
        }

        [Test]
        public void CancellingTransferStopsFileWatching()
        {
            var environment = new Environment();
            environment.dataGate.CancelTransfer();
            Assert.IsTrue(environment.mocks.fileSystem.wasCalledStopWatchingForFile);
        }

        [TestCase("testpath")]
        [TestCase("another path so fun wow")]
        public void GettingResultsCallsOpenFileForResultFile(string path)
        {
            var environment = new Environment();
            environment.dataGate.GetResults(CreateTestEquipment.WithModelResultPath(path));
            Assert.AreEqual(path, environment.mocks.fileSystem.lastReadFileParameterPath);
        }

        [Test]
        public void GettingResultsPassesResultFromOpenFileToResultParser()
        {
            var expectedResult = new XElement("asdf");
            var environment = new Environment();
            environment.mocks.fileSystem.nextReadFileReturn = expectedResult;
            environment.dataGate.GetResults(CreateTestEquipment.Anonymous());
            Assert.AreSame(expectedResult, environment.mocks.resultParser.lastParserParameterResultFile);
        }

        [Test]
        public void GettingResultsReturnsResultFromResultParser()
        {
            var expectedResult = new DataGateResults();
            var environment = new Environment();
            environment.mocks.resultParser.nextParseReturn = expectedResult;
            var actual = environment.dataGate.GetResults(CreateTestEquipment.Anonymous());
            Assert.AreSame(expectedResult, actual);
        }

        private class DataGateFileSystemMock: IDataGateFileSystem
        {
            public Stream OpenFile(string path)
            {
                lastOpenFileParameterPath = path;
                pseudoFileStream = new MemoryStream();
                return pseudoFileStream;
            }

            public void WatchForFile(string path, Action<XElement> evaluateAction)
            {
                lastWatchForFileParameterPath = path;
                if (triggerWatchForFileEvaluateAction)
                {
                    evaluateAction(nextWatchForFileEvaluateActionParameter);
                }
            }

            public void StopWatchingForFile()
            {
                wasCalledStopWatchingForFile = true;
            }

            public XElement ReadFile(string path)
            {
                lastReadFileParameterPath = path;
                return nextReadFileReturn;
            }

            public string lastOpenFileParameterPath;
            public MemoryStream pseudoFileStream;
            public string lastWatchForFileParameterPath;
            public bool triggerWatchForFileEvaluateAction = false;
            public XElement nextWatchForFileEvaluateActionParameter;
            public bool wasCalledStopWatchingForFile = false;
            public string lastReadFileParameterPath;
            public XElement nextReadFileReturn;
        }

        public class DataGateFileGeneratorMock: IDataGateFileGenerator
        {
            public void Accept(SemanticModel semanticModel)
            {
                lastAcceptParameterSemanticModel = semanticModel;
            }

            public string DataGateFileContent()
            {
                return dataGateFileContentReturn;
            }

            public SemanticModel lastAcceptParameterSemanticModel;
            public string dataGateFileContentReturn;
        }

        public class DataGateStatusParserMock : IDataGateStatusParser
        {
            public TransmissionStatus Parse(XElement statusFile)
            {
                lastParseParameterStatusFile = statusFile;
                return nextParseResult;
            }

            public XElement lastParseParameterStatusFile;
            public TransmissionStatus nextParseResult;
        }

        public class DataGateResultParserMock : IDataGateResultParser
        {
            public DataGateResults Parse(XElement resultFile)
            {
                lastParserParameterResultFile = resultFile;
                return nextParseReturn;
            }

            public XElement lastParserParameterResultFile;
            public DataGateResults nextParseReturn;
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    fileSystem = new DataGateFileSystemMock();
                    generator = new DataGateFileGeneratorMock();
                    statusParser = new DataGateStatusParserMock();
                    resultParser = new DataGateResultParserMock();
                }

                public DataGateFileSystemMock fileSystem;
                public DataGateFileGeneratorMock generator;
                public DataGateStatusParserMock statusParser;
                public DataGateResultParserMock resultParser;
            }

            public Environment()
            {
                mocks = new Mocks();
                dataGate = new DataGateDataAccess(
                    mocks.fileSystem,
                    mocks.generator,
                    mocks.statusParser,
                    mocks.resultParser);
            }

            public DataGateDataAccess dataGate;
            public Mocks mocks;
        }
    }
}