using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class HtmlBoxPlotContainer
    {
        public string BoxPlotName { get; set; }
        private readonly string _doubleFormat;
        private readonly List<BoxPlotBox> _boxPlotBoxes;
        private readonly long _splitCount;
        private readonly string _boxplotYAxisName;
        private readonly CultureInfo _culture = CultureInfo.InvariantCulture;
        private readonly string _fontFamily;
        
        public HtmlBoxPlotContainer(string name, List<BoxPlotBox> boxPlotBoxes, long splitCount, string boxplotYAxisName, string doubleformat, string fontFamily)
        {
            BoxPlotName = name;
            _doubleFormat = doubleformat;
            _fontFamily = fontFamily;
            _boxPlotBoxes = boxPlotBoxes;
            _splitCount = splitCount;
            _boxplotYAxisName = boxplotYAxisName;
        }

        public string GetBoxPlotHtml()
        {
            var boxPlotBoxesHtml = "";
            _boxPlotBoxes.ForEach(x => boxPlotBoxesHtml += x.GetBoxPlotBoxHtml() + "\r\n");

            var boxPlotBoxesData = new List<string>();
            _boxPlotBoxes.ForEach(x => boxPlotBoxesData.AddRange(x.GetDataNames()));
            var boxPlotBoxesDataString = "var data" + BoxPlotName + " = [" + string.Join(",", boxPlotBoxesData.ToList()) + "];\r\n";
           
            var values4YInterval = new List<double>();
            foreach (var boxPlotBox in _boxPlotBoxes)
            {
                values4YInterval.AddRange(boxPlotBox.BoxPlotBoxLines.Select(x => x.Value).ToList());
                values4YInterval.AddRange(boxPlotBox.BoxPlotBoxValues.Select(x => x.Value));
            }
           
            var boxPlotYInterval = (values4YInterval.Max() - values4YInterval.Min()) / 8;
            var xRange = Math.Max(_splitCount, _boxPlotBoxes.Count);
            var boxPlotXAxisRange = (xRange - 0.5).ToString(_doubleFormat, CultureInfo.InvariantCulture);

            var boxPlotHtml =
                "       <script>\r\n" +
                "           " + BoxPlotName + " = document.getElementById('boxplot" + BoxPlotName + "');\r\n" +
                            boxPlotBoxesHtml + "\r\n" +
                            boxPlotBoxesDataString + "\r\n" +
                "           var layout" + BoxPlotName + " = {\r\n" +
                "               height: null,\r\n" +
                "               width: null,\r\n" +
                "               autosize: true,\r\n" +
                "               margin: {\r\n" +
                "                   b: 0,\r\n" +
                "                   t: 5,\r\n" +
                "                   r: 0,\r\n" +
                "                   l: 45\r\n" +
                "               },\r\n" +
                "               title: '',\r\n" +
                "               showlegend: true,\r\n" +
                                GetBoxPlotLegend() +
                "               hovermode: 'closest',\r\n" +
                "               hoverdistance: 1,\r\n" +
                "               yaxis:\r\n" +
                "               {\r\n" +
                "                   title: {" +
                "                       text: '" + WebUtility.HtmlEncode(_boxplotYAxisName) + "',\r\n" +
                "                       standoff: 12,\r\n" +
                "                       font: {\r\n" +
                "                           size: 11,\r\n" +
                "                       },\r\n" +
                "                   },\r\n" +
                "                   autorange: true,\r\n" +
                "                   showgrid: false,\r\n" +
                "                   zeroline: false,\r\n" +
                "                   zerolinewidth: 1,\r\n" +
                "                   dtick: " + boxPlotYInterval.ToString(_doubleFormat, _culture) + ",\r\n" +
                "                   tickfont: {\r\n" +
                "                       size: 10,\r\n" +
                "                   },\r\n" +
                "               },\r\n" +
                "               xaxis:\r\n" +
                "               {\r\n" +
                "                   visible: false,\r\n" +
                "                   fixedrange : true,\r\n" +
                "                   range: [-0.5, " + boxPlotXAxisRange + "],\r\n" +
                "               },\r\n" +
                "            };\r\n" +

                "           var config" + BoxPlotName + " = { responsive: true, displayModeBar: false, doubleClick: 'reset'};\r\n" +

                "           Plotly.newPlot(" + BoxPlotName + ", data" + BoxPlotName + ", layout" + BoxPlotName + ", config" + BoxPlotName + ");\r\n" +
                "           Plotly.Plots.resize(" + BoxPlotName + ");\r\n" +

                "       </script >\r\n";

            return boxPlotHtml;
        }

        /// <summary>
        /// Legende in Funktion wegen Workaround für Druck-Bug
        /// </summary>
        public string GetBoxPlotLegend()
        {
            var legend =
                "               legend: { \r\n" +
                "                       orientation: 'h',\r\n" +
                "                       y: 0,\r\n" +
                "                       font: {\r\n" +
                "                           family: '" + _fontFamily + "',\r\n" +
                "                           size: 11,\r\n" +
                "                       }\r\n" +
                "                   },\r\n";
            return legend;
        }
    }


    public class BoxPlotBoxLine
    {
        public BoxPlotBoxLine(double value, string name, string description, 
            string legendGroup, bool showLegend, string color, string dash)
        {
            Value = value;
            Name = name;
            Description = description;
            LegendGroup = legendGroup;
            ShowLegend = showLegend;
            Color = color;
            Dash = dash;
        }
        public double Value;
        public string Name;
        public string Description;
        public string LegendGroup;
        public bool ShowLegend;
        public string Color;
        public string Dash;
    }

    public class BoxPlotBoxDataValue
    {
        public long Position;
        public double Value;
    }

    public class BoxPlotBox
    {
        public readonly List<BoxPlotBoxLine> BoxPlotBoxLines;
        public readonly List<BoxPlotBoxDataValue> BoxPlotBoxValues;
        private readonly string _boxPlotName;
        private readonly DateTime _timestamp;
        private readonly int _position;
        private readonly string _doubleFormat;
        private readonly CultureInfo _culture = CultureInfo.InvariantCulture;
        private const string BoxPlotNameCode = "BP";

        public BoxPlotBox(DateTime timestamp, string boxPlotName, List<BoxPlotBoxLine> boxPlotBoxLines, List<BoxPlotBoxDataValue> boxPlotBoxValues, int position, string doubleFormat)
        {
            BoxPlotBoxLines = boxPlotBoxLines;
            _boxPlotName = boxPlotName;
            _timestamp = timestamp;
            BoxPlotBoxValues = boxPlotBoxValues;
            _position = position;
            _doubleFormat = doubleFormat;
        }

        public string GetBoxPlotBoxHtml()
        {
            var valueList = new List<string>();
            foreach (var testValue in BoxPlotBoxValues)
            {
                valueList.Add(testValue.Value.ToString(_doubleFormat, _culture));
            }

            var minPosLine = _position - 0.5;
            var maxPosLine = _position + 0.5;

            var boxPlotHtml = "";
            foreach (var line in BoxPlotBoxLines)
            {
                var showLegend = line.ShowLegend ? "true" : "false";
                boxPlotHtml +=
                    "           var " + line.Name + " = {\r\n" +
                    "               type: 'scatter',\r\n" +
                    "               x: [" + minPosLine.ToString(_doubleFormat, _culture) + ", " + maxPosLine.ToString(_doubleFormat, _culture) + "],\r\n" +
                    "               y: [" + line.Value.ToString(_doubleFormat, _culture) + ", " + line.Value.ToString(_doubleFormat, _culture) + "],\r\n" +
                    "               mode: 'lines',\r\n" +
                    "               name: '" + WebUtility.HtmlEncode(line.Description) + "',\r\n" +
                    "               hoverinfo: 'none',\r\n" +
                    "               legendgroup: '" + line.LegendGroup +"',\r\n" +
                    "               showlegend: " + showLegend + ",\r\n" +
                    "               line:\r\n" +
                    "               {\r\n" +
                    "                   color: '" + line.Color + "',\r\n" +
                    "                   width: 1,\r\n" +
                    "                   dash: '" + line.Dash + "'\r\n" +
                    "               }\r\n" +
                    "           }\r\n";
            }

            boxPlotHtml +=
                "           var " + _boxPlotName + BoxPlotNameCode + " = {\r\n" +
                "               y: [" + string.Join(",", valueList) + "],\r\n" +
                "               type: 'box',\r\n" +
                "               name: '" + _timestamp + "',\r\n" +
                "               hoverinfo: 'y',\r\n" +
                "               showlegend: false,\r\n" +
                "               marker: {color: '#009D66', size:5},\r\n" +
                "               pointpos: 0,\r\n" +
                "               boxpoints: 'all',\r\n" +
                "               fillcolor:'#C0D9AF',\r\n" +
                "               boxmean: true\r\n" +
                "           };\r\n";

            return boxPlotHtml;
        }

        public List<string> GetDataNames()
        {
            var names = BoxPlotBoxLines.Select(x => x.Name).ToList();
            names.Add(_boxPlotName + BoxPlotNameCode);
            return names;
        }
    }
}
