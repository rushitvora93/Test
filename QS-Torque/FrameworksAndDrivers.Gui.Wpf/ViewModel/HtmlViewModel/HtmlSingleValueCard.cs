using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel.HtmlViewModel
{
    public class SingleValueCardValue
    {
        public long Position;
        public double value;
        public string Color;
    }

    public class SingleValueCardLine
    {
        public SingleValueCardLine(double value, string name, string color, string dash)
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

    public class HtmlSingleValueCard
    {
        private readonly string _singleValueCardName;
        private readonly string _singleValueCardElementName;
        private readonly List<SingleValueCardValue> _singleValueCardValues;
        private readonly List<SingleValueCardLine> _singleValueCardLines;
        private readonly string _doubleFormat;
        private readonly string _fontFamily;
        private readonly string _yDescription;
        public readonly CultureInfo _culture = CultureInfo.InvariantCulture;

        public HtmlSingleValueCard(string singleValueCardName, string singleValueCardElementName, List<SingleValueCardValue> singleValueCardValues, 
            List<SingleValueCardLine> singleValueCardLines, string doubleFormat, 
            string fontFamily, string yDescription)
        {
            _singleValueCardName = singleValueCardName;
            _singleValueCardElementName = singleValueCardElementName;
            _singleValueCardValues = singleValueCardValues;
            _singleValueCardLines = singleValueCardLines;
            _doubleFormat = doubleFormat;
            _fontFamily = fontFamily;
            _yDescription = yDescription;
        }


        public string GetHtml()
        {
            var maxPos = _singleValueCardValues.Max(x => x.Position) + 1;

            var minTestValueForDiagram = Math.Min(_singleValueCardLines.Select(x => x.Value).Min(), _singleValueCardValues.Select(x => x.value).Min());
            var maxTestValueForDiagram = Math.Max(_singleValueCardLines.Select(x => x.Value).Max(), _singleValueCardValues.Select(x => x.value).Max());
            var singleValueYInterval = (maxTestValueForDiagram - minTestValueForDiagram) / 10;

            var testValuesDouble = new List<string>();
            var singleValuePositions = new List<long>();
            var singleValueColors = new List<string>();
            foreach (var val in _singleValueCardValues)
            {
                testValuesDouble.Add(val.value.ToString(_doubleFormat, _culture));
                singleValuePositions.Add(val.Position + 1);
                singleValueColors.Add(val.Color);
            }

            string singleValueCard =
                "           <script>\r\n" +
                "               " + _singleValueCardName + " = document.getElementById('" + _singleValueCardElementName + "');\r\n" +
                "               var Data = {\r\n" +
                "                   type: 'scatter',\r\n" +
                "                   x: [" + string.Join(",", singleValuePositions) + "],\r\n" +
                "                   y: [" + string.Join(",", testValuesDouble) + "],\r\n" +
                "                   mode: 'lines+markers',\r\n" +
                "                   name: 'Data',\r\n" +
                "                   showlegend: false,\r\n" +
                "                   hoverinfo: 'all',\r\n" +
                "                   line: {\r\n" +
                "                       color: 'black',\r\n" +
                "                       width: 2\r\n" +
                "                   },\r\n" +
                "                   marker: {\r\n" +
                "                       color: ['" + string.Join("','", singleValueColors) + "'],\r\n" +
                "                       size: 6,\r\n" +
                "                       symbol: 'circle'\r\n" +
                "                   }\r\n" +
                "               }\r\n";

            var lineVarDescriptions = new List<string>();
            var count = 1;
            foreach (var line in _singleValueCardLines)
            {
                var lineVarDescription = "line" + count;
                lineVarDescriptions.Add(lineVarDescription);
                singleValueCard +=
                "               var " + lineVarDescription + " = {\r\n" +
                "                   type: 'scatter',\r\n" +
                "                   x: [0, " + maxPos + "],\r\n" +
                "                   y: [" + line.Value.ToString(_doubleFormat, _culture) + ", " + line.Value.ToString(_doubleFormat, _culture) + "],\r\n" +
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

            singleValueCard +=
                "               var data = [Data, " + lineNames + "]\r\n" +

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
                "                   xaxis: {\r\n" +
                "                       rangemode: 'nonnegative',\r\n" +
                "                       range: [0," + (maxPos + 1) + "],\r\n" +
                "                       showgrid: false,\r\n" +
                "                       showline: true,\r\n" +
                "                       zeroline: false,\r\n" +
                "                       dtick: 3,\r\n" +
                "                       tickwidth: 1,\r\n" +
                "                       ticklen: 3,\r\n" +
                "                       tickfont: {\r\n" +
                "                           size: 10,\r\n" +
                "                       },\r\n" +
                "                   },\r\n" +
                "                   yaxis: {\r\n" +
                "                       title: {" +
                "                           text: '" + WebUtility.HtmlEncode(_yDescription) + "',\r\n" +
                "                           standoff: 5,\r\n" +
                "                           font: {\r\n" +
                "                               size: 11.5,\r\n" +
                "                           },\r\n" +
                "                       },\r\n" +
                "                       autorange: true,\r\n" +
                "                       showgrid: true,\r\n" +
                "                       showline: true,\r\n" +
                "                       zeroline: false,\r\n" +
                "                       dtick: " + singleValueYInterval.ToString(_doubleFormat, _culture) + ",\r\n" +
                "                       tickwidth: 1,\r\n" +
                "                       ticklen: 3,\r\n" +
                "                       tickfont: {\r\n" +
                "                           size: 10,\r\n" +
                "                       },\r\n" +
                "                   }\r\n" +
                "               }\r\n" +

                "               var config = { responsive: true, displayModeBar: false, doubleClick: 'reset'};\r\n" +
                "               Plotly.newPlot(" + _singleValueCardName + ", data, layout, config);\r\n" +
                "               Plotly.Plots.resize(" + _singleValueCardName + ");\r\n" +
                "           </script >\r\n";

            return singleValueCard;
        }
    }
}
