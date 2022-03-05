using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using Core.Entities;
using FrameworksAndDrivers.Localization;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel.HtmlViewModel
{
    public class HistogramLine
    {
        public HistogramLine(double value, string name, string color, string dash)
        {
            Value = value;
            Name = name;
            Color = color;
            Dash = dash;
        }
        public double Value;
        public string Name;
        public string Color;
        public string Dash;
    }

    public class HistogramValue
    {
        public long Position;
        public double value;
    }

    public class HtmlHistogram
    {
        private readonly string _histogramName;
        private readonly string _histogramElementName;
        private readonly List<HistogramValue> _histogramValues;
        private readonly List<HistogramLine> _histogramLines;
        private readonly string _doubleFormat;
        private readonly string _fontFamily;
        private readonly List<double> _values;
        private readonly double _maximumValue;
        private readonly double _minimumValue;
        private double _minimumGaussLinePosition;
        private double _maximumGaussLinePosition;
        private double _centerGaussLinePosition;
        public readonly CultureInfo _culture = CultureInfo.InvariantCulture;
        private readonly string _translation4NormalDistribution;
        private readonly string _translation4NumberOfValues;

        public HtmlHistogram(string histogramName, string histogramElementName, List<HistogramValue> histogramValues, 
            List<HistogramLine> histogramLines, LocalizationWrapper localization, string doubleFormat, string fontFamily)
        {
            _histogramName = histogramName;
            _histogramElementName = histogramElementName;
            _histogramValues = histogramValues;
            _histogramLines = histogramLines;
            _doubleFormat = doubleFormat;
            _fontFamily = fontFamily;
            _values = histogramValues.Select(x => x.value).ToList();
            _maximumValue = _values.Max();
            _minimumValue = _values.Min();
            _translation4NormalDistribution = localization.Strings.GetParticularString("ClassicTestHtmlView", "Normal distribution");
            _translation4NumberOfValues = localization.Strings.GetParticularString("ClassicTestHtmlView", "Number of values");
        }

        public void SetGaussLinePositions(double minimumGaussLinePosition, double maximumGaussLinePosition, double centerGaussLinePosition)
        {
            _minimumGaussLinePosition = minimumGaussLinePosition;
            _maximumGaussLinePosition = maximumGaussLinePosition;
            _centerGaussLinePosition = centerGaussLinePosition;
        }

        public string GetHtml()
        {
            var numberOfClasses = 7;
            var columnWidthInXunit = (_maximumValue - _minimumValue) / numberOfClasses;

            var qstFrequencyBarCount = 9;
            var histoFrequencies = Statistic.GetHistogramFrequency(columnWidthInXunit, qstFrequencyBarCount, _values.ToArray());

            var gaussvalues = Statistic.GetStandardGaussCurve(0.001);
            var gausYMax = gaussvalues.Values.Max();

            var qstHightFactor = histoFrequencies.Max() / gausYMax * 1.4 / 1.3;
            var stretchedGausValue = Statistic.GetStretchedGaussCurve(gaussvalues, _minimumGaussLinePosition, _maximumGaussLinePosition, _centerGaussLinePosition, qstHightFactor);

            var xAxisValues = _histogramLines.Select(x => x.Value).ToList();
            var xAxisInterval = (xAxisValues.Max() - xAxisValues.Min()) / 10;

            var valueXGaus = new List<string>();
            var valueYGaus = new List<string>();
            foreach (var value in stretchedGausValue)
            {
                valueXGaus.Add(value.Key.ToString(_culture));
                valueYGaus.Add(value.Value.ToString(_culture));
            }

            var singleValues = "";

            foreach (var val in _histogramValues)
            {
                if (singleValues.Length > 0)
                {
                    singleValues += ",";
                }
                singleValues += "{x: " + (val.Position + 1) + ", y: " + val.value + "}";
            }

            var histogramTestValues = new List<string>();
            foreach (var val in _values)
            {
                histogramTestValues.Add(val.ToString(_doubleFormat, _culture));
            }

            var klass0 = _minimumValue - (columnWidthInXunit / 2.0);
            var yAxisHight = histoFrequencies.Max() * 1.1;

            var start = klass0.ToString(_doubleFormat, _culture);
            var strStepsize = columnWidthInXunit.ToString(_doubleFormat, _culture);
   
            string histogram =
                "           <script>\r\n" +
                "               " + _histogramName + " = document.getElementById('" + _histogramElementName + "');\r\n" +

                "               var histogram = {\r\n" +
                "                   x: [" + string.Join(",", histogramTestValues) + "],\r\n" +
                "                   type: 'histogram',\r\n" +
                "                   showlegend: false,\r\n" +
                "                   textposition: 'inside',\r\n" +
                "                   marker: {\r\n" +
                "                       color: '#009D66',\r\n" +
                "                       line: {\r\n" +
                "                           color: 'black',\r\n" +
                "                           width: 2\r\n" +
                "                       },\r\n" +
                "                   },\r\n" +
                "                   xbins: {\r\n" +
                "                       size: " + strStepsize + ",\r\n" +
                "                       start: " + start + "\r\n" +
                "                   }\r\n" +
                "               };\r\n" +

                "               var gausscurve = {\r\n" +
                "                   type: 'scatter',\r\n" +
                "                   x: [" + string.Join(",", valueXGaus) + "],\r\n" +
                "                   y: [" + string.Join(",", valueYGaus) + "],\r\n" +
                "                   mode: 'lines',\r\n" +
                "                   name: '" + WebUtility.HtmlEncode(_translation4NormalDistribution) + "',\r\n" +
                "                   showlegend: true,\r\n" +
                "                   line: {\r\n" +
                "                       color: 'blue',\r\n" +
                "                       width: 1,\r\n" +
                "                   }\r\n" +
                "               };\r\n";

            var lineVarDescriptions = new List<string>();
            var count = 1;
            foreach (var line in _histogramLines)
            {
                var lineVarDescription = "line" + count;
                lineVarDescriptions.Add(lineVarDescription);
                histogram +=
                    "               var " + lineVarDescription + " = {\r\n" +
                    "                   type: 'scatter',\r\n" +
                    "                   y: [0, " + yAxisHight.ToString(_doubleFormat, _culture) + "],\r\n" +
                    "                   x: [" + line.Value.ToString(_doubleFormat, _culture) + ", " + line.Value.ToString(_doubleFormat, _culture) + "],\r\n" +
                    "                   mode: 'lines',\r\n" +
                    "                   name: '" + WebUtility.HtmlEncode(line.Name) + "',\r\n" +
                    "                   showlegend: true,\r\n" +
                    "                   line: {\r\n" +
                    "                       color: '" + line.Color + "',\r\n" +
                    "                       width: 1,\r\n" +
                    "                       dash: '" + line.Dash + "'\r\n" +
                    "                   }\r\n" +
                    "               }\r\n";
                count++;
            }

            string lineNames = string.Join(",", lineVarDescriptions.ToList());

            histogram +=
                "               var layout = {\r\n" +
                "                   height: null,\r\n" +
                "                   width: null,\r\n" +
                "                   autosize: true,\r\n" +
                "                   margin: {\r\n" +
                "                       b: 25,\r\n" +
                "                       t: 1,\r\n" +
                "                       r: 18,\r\n" +
                "                       l: 45\r\n" +
                "                   },\r\n" +
                "                   legend: { \r\n" +
                "                       orientation: 'v',\r\n" +
                "                       font: {\r\n" +
                "                           family: '" + _fontFamily + "',\r\n" +
                "                           size: 11,\r\n" +
                "                       }\r\n" +
                "                   },\r\n" +
                "                   hovermode: 'closest',\r\n" +
                "                   hoverdistance: 1,\r\n" +
                "                   xaxis: {\r\n" +
                "                       showgrid: false,\r\n" +
                "                       showline: true,\r\n" +
                "                       dtick: " + xAxisInterval.ToString(_doubleFormat, CultureInfo.InvariantCulture) + ",\r\n" +
                "                       zeroline: false\r\n" +
                "                   },\r\n" +
                "                   yaxis: {\r\n" +
                "                       title: {" +
                "                           text: '" + WebUtility.HtmlEncode(_translation4NumberOfValues) + "',\r\n" +
                "                           standoff: 5,\r\n" +
                "                           font: {\r\n" +
                "                               size: 11.5,\r\n" +
                "                           },\r\n" +
                "                       },\r\n" +
                "                       rangemode: 'nonnegative',\r\n" +
                "                       showgrid: true,\r\n" +
                "                       showline: false,\r\n" +
                "                       zeroline: false,\r\n" +
                "                   }\r\n" +
                "               }\r\n" +

                "               var data = [histogram, " + lineNames + ", gausscurve];\r\n" +
                "               var config = { responsive: true, displayModeBar: false, doubleClick: 'reset'};\r\n" +
                "               Plotly.newPlot(" + _histogramName + ", data, layout, config);\r\n" +
                "               Plotly.Plots.resize(" + _histogramName + ");\r\n" +
                "           </script>\r\n";

            return histogram;
        }
    }
}
