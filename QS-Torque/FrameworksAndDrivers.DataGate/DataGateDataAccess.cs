using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Core.Entities;
using Core.UseCases.Communication;
using Core.UseCases.Communication.DataGate;

namespace FrameworksAndDrivers.DataGate
{
    public interface IDataGateFileSystem
    {
        Stream OpenFile(string path);
        void WatchForFile(string path, Action<XElement> evaluateAction);
        void StopWatchingForFile();
        XElement ReadFile(string path);
    }

    public interface IDataGateFileGenerator
    {
        void Accept(SemanticModel semanticModel);
        string DataGateFileContent();
    }

    public interface IDataGateStatusParser
    {
        TransmissionStatus Parse(XElement statusFile);
    }

    public interface IDataGateResultParser
    {
        DataGateResults Parse(XElement resultFile);
    }

    public class DataGateDataAccess : IDataGateDataAccess
    {
        public DataGateDataAccess(
            IDataGateFileSystem fileSystem,
            IDataGateFileGenerator generator,
            IDataGateStatusParser statusParser,
            IDataGateResultParser resultParser)
        {
            _fileSystem = fileSystem;
            _generator = generator;
            _statusParser = statusParser;
            _resultParser = resultParser;
        }

        public void TransferToTestEquipment(
            SemanticModel dataGateSemanticModel,
            TestEquipment testEquipment,
            Action<TransmissionStatus> withReceivedStatus)
        {
            _lastTransferFinished = false;
            using (var stream = _fileSystem.OpenFile(testEquipment.TestEquipmentModel?.CommunicationFilePath?.ToDefaultString()))
            {
                _generator.Accept(dataGateSemanticModel);
                var bytes = Encoding.ASCII.GetBytes(_generator.DataGateFileContent() ?? "");
                stream.Write(bytes, 0, bytes.Length);
            }

            _fileSystem.WatchForFile(
                testEquipment.StatusFilePath(),
                statusFile =>
                {
                    var status = _statusParser.Parse(statusFile);
                    _lastTransferFinished = true;
                    withReceivedStatus(status);
                });
        }

        public bool LastTransferFinished()
        {
            return _lastTransferFinished;
        }

        public void CancelTransfer()
        {
            _fileSystem.StopWatchingForFile();
            _lastTransferFinished = true;
        }

        public DataGateResults GetResults(TestEquipment testEquipment)
        {
            return _resultParser.Parse(_fileSystem.ReadFile(testEquipment.ResultFilePath()));
        }

        private readonly IDataGateFileSystem _fileSystem;
        private readonly IDataGateFileGenerator _generator;
        private readonly IDataGateStatusParser _statusParser;
        private readonly IDataGateResultParser _resultParser;
        private bool _lastTransferFinished = true;
    }
}
