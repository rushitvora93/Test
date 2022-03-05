using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Core.UseCases.Communication;

namespace FrameworksAndDrivers.DataGate
{
    public class ResultFileParser: IDataGateResultParser
    {
        public DataGateResults Parse(XElement resultFile)
        {
            var dataGateResults = new DataGateResults {Results = new List<DataGateResult>()};
            var fileItems = resultFile.XPathSelectElements("./ResultList/FileItem");
            foreach (var fileItem in fileItems)
            {
                long.TryParse(
                    fileItem.XPathSelectElement("./TestId1")?.Value, out var locationToolAssignmentId);
                long.TryParse(
                    fileItem.XPathSelectElement("./Unit1Id")?.Value, out var unit1Id);
                double.TryParse(
                    fileItem.XPathSelectElement("./Nom1")?.Value,
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out var nom1);
                double.TryParse(
                    fileItem.XPathSelectElement("./Min1")?.Value,
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out var min1);
                double.TryParse(
                    fileItem.XPathSelectElement("./Max1")?.Value,
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out var max1);
                long.TryParse(
                    fileItem.XPathSelectElement("./Unit2Id")?.Value, out var unit2Id);
                double.TryParse(
                    fileItem.XPathSelectElement("./Nom2")?.Value,
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out var nom2);
                double.TryParse(
                    fileItem.XPathSelectElement("./Min2")?.Value,
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out var min2);
                double.TryParse(
                    fileItem.XPathSelectElement("./Max2")?.Value,
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out var max2);
                var sampleElements = fileItem.XPathSelectElements("./Test/Sample");
                var maxSample = sampleElements.Select(element => long.Parse(element.Value)).Max();
                for (var currentSample = 1; currentSample <= maxSample; ++currentSample)
                {
                    var dataGateResultValues = new List<DataGateResultValue>();
                    var valueElements = fileItem.XPathSelectElements(
                        $"./Test[Sample='{currentSample}']");
                    foreach (var valueElement in valueElements)
                    {
                        DateTime.TryParseExact(valueElement.XPathSelectElement("./TimeStamp")?.Value,
                            "yyyy-MM-dd HH:mm:ss",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out var timestamp);
                        double.TryParse(
                            valueElement.XPathSelectElement("./Value1")?.Value,
                            NumberStyles.Any,
                            CultureInfo.InvariantCulture,
                            out var value1);
                        double.TryParse(
                            valueElement.XPathSelectElement("./Value2")?.Value,
                            NumberStyles.Any,
                            CultureInfo.InvariantCulture,
                            out var value2);
                        dataGateResultValues.Add(new DataGateResultValue
                        {
                            Timestamp = timestamp,
                            Value1 = value1,
                            Value2 = value2
                        });
                    }
                    dataGateResults.Results.Add(new DataGateResult
                    {
                        LocationToolAssignmentId = locationToolAssignmentId,
                        Unit1Id = unit1Id,
                        Nom1 = nom1,
                        Min1 = min1,
                        Max1 = max1,
                        Unit2Id = unit2Id,
                        Nom2 = nom2,
                        Min2 = min2,
                        Max2 = max2,
                        Values = dataGateResultValues
                    });
                }
            }
            return dataGateResults;
        }
    }
}
