using System.Xml.Linq;
using System.Xml.XPath;
using Core.Entities;
using Core.UseCases.Communication;

namespace FrameworksAndDrivers.DataGate
{
    public class StatusFileParserHumbleObject: IDataGateStatusParser
    {
        public TransmissionStatus Parse(XElement statusFile)
        {
            var codeElement = statusFile.XPathSelectElement("./TxStatus/TxCode");
            var transmissionFailed = true;
            if (codeElement != null)
            {
                int code;
                if(int.TryParse(codeElement.Value, out code))
                {
                    transmissionFailed = code != 0;
                }
            }

            return new TransmissionStatus
            {
                serialNumber = new TestEquipmentSerialNumber(statusFile.XPathSelectElement("./Header/DevSerNo")?.Value),
                transmissionFailed = transmissionFailed,
                message = statusFile.XPathSelectElement("./TxStatus/TxMessage")?.Value
            };
        }
    }
}
