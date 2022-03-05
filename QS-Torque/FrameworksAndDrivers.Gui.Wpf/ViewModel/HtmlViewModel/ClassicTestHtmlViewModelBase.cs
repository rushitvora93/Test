using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Localization;
using InterfaceAdapters;
using InterfaceAdapters.Localization;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel.HtmlViewModel
{
    public abstract class ClassicTestHtmlViewModelBase : BindableBase, IGetUpdatedByLanguageChanges, ICanClose
    {
        protected LocalizationWrapper _localization;
        protected readonly string doubleformat = "0.000";
        protected readonly int doubleDecimalDigits = 3;
        protected readonly CultureInfo _culture = CultureInfo.InvariantCulture;
        protected readonly string _fontFamily = "Segoe UI";
        protected readonly string _backGroundColorHeader = "#b4cce4";
        protected readonly string _backGroundColorContent = "#c7d9eb";
        protected readonly string _unitNm = "Nm";
        protected readonly string _unitDeg = "Deg";
        protected readonly string _treeSeperator = " / ";
        public event EventHandler ShowPrintDialog;
        public event EventHandler<string> ReloadHtmlRequest;
        public abstract string HtmlDocument { get; }
        public RelayCommand PrintTestHtmlCommand { get; }

        protected ClassicTestHtmlViewModelBase(LocalizationWrapper localization)
        {
            _localization = localization;
            _localization.Subscribe(this);
            PrintTestHtmlCommand = new RelayCommand(PrintTestHtmlExecute);
        }

        /// <summary>
        /// Workaround for BoxPlot because there were problems with the automatic resize
        /// </summary>
        protected string GetResizeBoxPlotHtml(List<(string name, string legend)> pPlotContainers)
        {
            var resizePlot =
                "		<script>\r\n" +
                "			const resizeChartWidth = () => {\r\n";
            foreach (var plotContainer in pPlotContainers)
            {
                resizePlot +=
                    "               Plotly.relayout(" + plotContainer.name + ", { width: null, height: null, autosize: true})\r\n" +
                    "				Plotly.Plots.resize(" + plotContainer.name + ");\r\n";
                resizePlot +=
                    "               Plotly.relayout(" + plotContainer.name + ", { " + plotContainer.legend + "})\r\n" +
                    "				Plotly.Plots.resize(" + plotContainer.name + ");\r\n";
            }

            resizePlot +=
                "			};\r\n" +

                "			if (window.matchMedia) { \r\n" +
                "				window.matchMedia('print').addListener((print) => {\r\n" +
                "					resizeChartWidth();\r\n" +
                "				});\r\n" +
                "			}\r\n" +
                "			window.onbeforeprint = resizeChartWidth;\r\n" +
                "			window.onafterprint = resizeChartWidth;\r\n" +
                "		</script>\r\n";
            return resizePlot;
        }

        protected string GetMonitoringScrollFunction()
        {
            var scrollFunction =
                "		<script>\r\n" +
                "			(function() {\r\n" +
                "				var statValues = $('#statValues');\r\n" +
                "				var testValues = $('#testValues');\r\n" +
                "				var boxPlotGraph = $('#boxPlotGraph');\r\n" +

                "				$('#footerScrollBox').scroll(function() {\r\n" +
                "					boxPlotGraph.prop('scrollTop', this.scrollTop)\r\n" +
                "						.prop('scrollLeft', this.scrollLeft);\r\n" +
                "					statValues.prop('scrollTop', this.scrollTop)\r\n" +
                "						.prop('scrollLeft', this.scrollLeft);\r\n" +
                "					testValues.prop('scrollTop', this.scrollTop)\r\n" +
                "						.prop('scrollLeft', this.scrollLeft);\r\n" +
                "				});\r\n" +
                "			})();\r\n" +
                "		</script>\r\n";
            return scrollFunction;
        }

        protected string GetMonitoringScrollFooter()
        {
            var scrollFooter =
                "		<div class='footerScrollContainer'>	\r\n" +
                "			<div class='footerScrollContent'>\r\n" +
                "				<div class='footerScrollBox' id='footerScrollBox'>\r\n" +
                "					<div class='footerScroll'/>\r\n" +
                "				</div>\r\n" +
                "			</div>\r\n" +
                "		</div>\r\n";
            return scrollFooter;
        }


        /// <summary>
        /// Creates the script for the collapsible ContentHeader with 
        /// </summary>
        protected string GetShowContainer()
        {
            var showContainer =
                "		<script>\r\n" +
                "			var coll = document.getElementsByClassName('contentHeader');\r\n" +
                "			var i;\r\n" +

                "			for (i = 0; i < coll.length; i++) {\r\n" +
                "				coll[i].addEventListener('click', function() {\r\n" +
                "					resizeChartWidth();\r\n" +

                "					this.classList.toggle('inactive');\r\n" +
                "					var content = this.nextElementSibling;\r\n" +
                "					if (!content.style.display || content.style.display == 'block') {\r\n" +
                "						content.style.display = 'none';\r\n" +
                "					} else {\r\n" +
                "						content.style.display = 'block';\r\n" +
                "					}\r\n" +
                "				});\r\n" +
                "			}\r\n" +
                "		</script>\r\n";
            return showContainer;
        }

        /// <summary>
        /// Creates the script for the collapsible ContentHeader with boxPlot
        /// </summary>
        protected string GetShowContainerWithBoxPlot(string screenBoxPlotName, List<string> printBoxPlotNames, List<string> boxPlotContentHeaderNames)
        {
            var showContainer =
                "		<script>\r\n" +
                "			var coll = document.getElementsByClassName('contentHeader');\r\n" +
                "			var i;\r\n" +

                "			for (i = 0; i < coll.length; i++) {\r\n" +
                "				coll[i].addEventListener('click', function() {\r\n" +
                "					this.classList.toggle('inactive');\r\n" +
                "					var content = this.nextElementSibling;\r\n" +
                "					if (!content.style.display || content.style.display == 'block') {\r\n" +
                "						content.style.display = 'none';\r\n" +
                "					} else {\r\n" +
                "						content.style.display = 'block';\r\n" +
                "					}\r\n";

            foreach (var boxPlotContentHeader in boxPlotContentHeaderNames)
            {
                showContainer +=
                    GetShowContainerPrinted(boxPlotContentHeader, screenBoxPlotName, printBoxPlotNames);
            }

            showContainer +=
                "					resizeChartWidth();\r\n" +
                "				});\r\n" +
                "			}\r\n" +
                "		</script>\r\n";
            return showContainer;
        }


        private string GetShowContainerPrinted(string boxPlotContentHeaderName, string screenBoxPlotName, List<string> printBoxPlots)
        {
            var showContainerPrinted =
                "                   if(this.id == '" + boxPlotContentHeaderName + screenBoxPlotName + "') {\r\n";
            foreach (var boxPlot in printBoxPlots)
            {
                showContainerPrinted +=
                    "                       var element" + boxPlot + " = document.getElementById('" + boxPlotContentHeaderName + boxPlot + "');\r\n" +
                    "                       element" + boxPlot + ".classList.toggle('inactive');\r\n" +
                    "                       var elementContent" + boxPlot + " = element" + boxPlot + ".nextElementSibling;\r\n" +
                    "                       elementContent" + boxPlot + ".style.display = content.style.display;\r\n";
            }

            showContainerPrinted +=
                "                   }\r\n";

            return showContainerPrinted;
        }

        protected string GetCopyRight(string pstyleclassContainer, string styleclassText)
        {
            var copyRightText = "QS-Torque CSP GmbH & Co.KG 1996-" + DateTime.Now.Year;
            var copyright =
                "       <div class='" + pstyleclassContainer + "'>\r\n" +
                "           <div><span class='" + styleclassText + "'>" + WebUtility.HtmlEncode(copyRightText) + "</span></div>\r\n" +
                "       </div>\r\n";
            return copyright;
        }

        public abstract void Initialize();
        public virtual void FillTranslation()
        {
            Translation4Date = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Date");
            Translation4Time = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Time");
            Translation4From = _localization.Strings.GetParticularString("ClassicTestHtmlView", "from");
            Translation4To = _localization.Strings.GetParticularString("ClassicTestHtmlView", "to");
            Translation4InfosOfTest = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Information of the test");
            Translation4Location = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Measurement Point");
            Translation4Number = _localization.Strings.GetParticularString("LocationAttribute", "Number");
            Translation4Description = _localization.Strings.GetParticularString("LocationAttribute", "Description");
            Translation4Path = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Tree path");
            CurrentTranslation4Path = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Current tree path");
            Translation4BoxPlot = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Box plot");
            Translation4StatisticalData = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Statistical data");
            Translation4NominalValue = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Nominal value");
            Translation4NumberOfValues = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Number of values");
            Translation4Average = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Average");
            Translation4Minimum = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Minimum");
            Translation4Maximum = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Maximum");
            Translation4StandardDeviation = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Standard deviation");
            Translation4Range = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Range");
            Translation4TestEquipment = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Test device");
            Translation4TestEquipmentType = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Test device type");
            Translation4SingleValues = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Single values");
            Translation4Value = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Value");
            Translation4DataOfTest = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Data of the test");
            Translation4NotOk = _localization.Strings.GetParticularString("ClassicTestHtmlView", "not Ok");
            Translation4Ok = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Ok");
            Translation4Unit = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Unit");
            Translation4Result = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Result");
            Translation4Cm = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Cm");
            Translation4Cmk = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Cmk");
            Translation4Note = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Note");
            Translation4Variance = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Variance");
            Translation4SingleValueCard = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Single value card");
            Translation4NormalDistribution = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Normal distribution");
            Translation4Histogram = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Histogram");
            Translation4Tool = _localization.Strings.GetParticularString("HelperTableViewModel", "Tool");
            Translation4SerialNumber = _localization.Strings.GetParticularString("ToolAttribute", "Serial number");
            Translation4InventoryNumber = _localization.Strings.GetParticularString("ToolAttribute", "Inventory number");
            Translation4ToolModel = _localization.Strings.GetParticularString("ToolAttribute", "Tool model");
            Translation4Manufacturer = _localization.Strings.GetParticularString("ToolModelAttribute", "Manufacturer");
            Translation4ToolType = _localization.Strings.GetParticularString("ToolModelView", "Type of tool model");
            Translation4ToleranceClass = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Tolerance class");
            Translation4LowerLimit = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Lower tolerance limit");
            Translation4UpperLimit = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Upper tolerance limit");
            Translation4TolLimits = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Tolerance limits");
            Translation4NotEnoughValues = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Not enough values");
        }

        public void LanguageUpdate()
        {
            ReloadHtmlRequest?.Invoke(this, HtmlDocument);
        }

        protected void PrintTestHtmlExecute(object obj)
        {
            ShowPrintDialog?.Invoke(this, System.EventArgs.Empty);
        }

        public virtual bool CanClose()
        {
            return true;
        }

        #region Translatio Properties
        public string Translation4Date { get; set; }
        public string Translation4Time { get; set; }
        public string Translation4From { get; set; }
        public string Translation4To { get; set; }
        public string Translation4InfosOfTest { get; set; }
        public string Translation4Location { get; set; }
        public string Translation4Number { get; set; }
        public string Translation4Description { get; set; }
        public string Translation4Path { get; set; }
        public string CurrentTranslation4Path { get; set; }
        public string Translation4BoxPlot { get; set; }
        public string Translation4StatisticalData { get; set; }
        public string Translation4NumberOfValues { get; set; }
        public string Translation4Average { get; set; }
        public string Translation4Minimum { get; set; }
        public string Translation4Maximum { get; set; }
        public string Translation4StandardDeviation { get; set; }
        public string Translation4Range { get; set; }
        public string Translation4TestEquipment { get; set; }
        public string Translation4TestEquipmentType { get; set; }
        public string Translation4SingleValues { get; set; }
        public string Translation4Value { get; set; }
        public string Translation4NominalValue { get; set; }
        public string Translation4DataOfTest { get; set; }
        public string Translation4Ok { get; set; }
        public string Translation4NotOk { get; set; }
        public string Translation4Unit { get; set; }
        public string Translation4Result { get; set; }
        public string Translation4Cm { get; set; }
        public string Translation4Cmk { get; set; }
        public string Translation4Note { get; set; }
        public string Translation4Variance { get; set; }
        public string Translation4SingleValueCard { get; set; }
        public string Translation4NormalDistribution { get; set; }
        public string Translation4Histogram { get; set; }
        public string Translation4Tool { get; set; }
        public string Translation4SerialNumber { get; set; }
        public string Translation4InventoryNumber { get; set; }
        public string Translation4ToolModel { get; set; }
        public string Translation4Manufacturer { get; set; }
        public string Translation4ToolType { get; set; }
        public string Translation4LowerLimit { get; set; }
        public string Translation4UpperLimit { get; set; }
        public string Translation4TolLimits { get; set; }
        public string Translation4ToleranceClass { get; set; }
        public string Translation4NotEnoughValues { get; set; }
        #endregion
    }
}
