using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using Client.Core.Entities;
using Common.Types.Enums;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Localization;
using InterfaceAdapters.Communication;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel.HtmlViewModel
{
    public class ClassicProcessMonitoringTestHtmlViewModel : ClassicTestHtmlViewModelBase
    {
        public ClassicProcessMonitoringTestHtmlViewModel(Location location, List<ClassicProcessTest> tests,
            LocalizationWrapper localization, ITreePathBuilder treePathBuilder) : base(localization)
        {
            _location = location;
            _tests = tests;
            _locationTreePath = treePathBuilder.GetTreePath(_location, _treeSeperator);
        }

        public override string HtmlDocument { get => GenerateCtlHtml(); }
        public override void Initialize()
        {
            FillTranslation();
        }

        #region CTL Properties
        private readonly Location _location;
        private readonly List<ClassicProcessTest> _tests;
        public int CtlSplitCount = 6;
        private readonly string _locationTreePath;
        private readonly string _contentHeaderBoxplot = "contentHeaderBoxPlot";
        private readonly string _contentHeaderStatistical = "contentHeaderStatistical";
        private readonly string _contentHeaderTestValues = "contentHeaderTestValues";
        #endregion

        #region CTL Translation
        public string Translation4ProcessMonitoring { get; set; }
        public string Translation4LowerTestLimit { get; set; }
        public string Translation4UpperTestLimit { get; set; }
        public string Translation4TestLimits { get; set; }
        public string Translation4InterventionLimits { get; set; }

        public override void FillTranslation()
        {
            Translation4ProcessMonitoring = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Process monitoring");
            Translation4LowerTestLimit = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Lower test limit");
            Translation4UpperTestLimit = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Upper test limit");
            Translation4TestLimits = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Test limits");
            Translation4InterventionLimits = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Intervention limits");
            base.FillTranslation();
        }
        #endregion

        #region CTL HTML

        private string GenerateCtlHtml()
        {
            var boxPlotWithTests = new Dictionary<HtmlBoxPlotContainer, List<ClassicProcessTest>>();

            var boxPlotForScreen = GetBoxPlotContainer("BoxPlot4Screen", _tests);
            boxPlotWithTests[boxPlotForScreen] = _tests;

            var count = 1;
            var splitTestsForPrint = ListUtils.SplitList(_tests, CtlSplitCount);
            var printBoxPlots = new List<HtmlBoxPlotContainer>();
            foreach (var processTest in splitTestsForPrint)
            {
                var boxPlot = GetBoxPlotContainer("BoxPlot4Print" + count, processTest);
                printBoxPlots.Add(boxPlot);
                boxPlotWithTests[boxPlot] = processTest;
                count++;
            }

            var allBoxPlotContainers = new List<HtmlBoxPlotContainer> { boxPlotForScreen };
            allBoxPlotContainers.AddRange(printBoxPlots);

            var boxPlotHtml =
                "<!DOCTYPE html>\r\n" +
                "<html>\r\n" +
                GetCtlHead() +
                "   <body>\r\n" +
                "   <table class='contentTable'>\r\n" +
                "   <thead><tr><td>\r\n" +
                GetCtlTitle() +
                GetCtlInformation() +
                "   </tr></td></thead>\r\n" +
                "   <tbody><tr><td>\r\n" +
                GetCtlBoxPlotTestHtml(false, boxPlotForScreen, printBoxPlots, boxPlotWithTests) +
                GetCtlBoxPlotTestHtml(true, boxPlotForScreen, printBoxPlots, boxPlotWithTests) +
                GetResizeBoxPlotHtml(allBoxPlotContainers.Select(x => (Name: x.BoxPlotName, x.GetBoxPlotLegend())).ToList()) +
                GetShowContainerWithBoxPlot(boxPlotForScreen.BoxPlotName, printBoxPlots.Select(x => x.BoxPlotName).ToList(),
                    new List<string>()
                    {
                        _contentHeaderBoxplot ,
                        _contentHeaderStatistical,
                        _contentHeaderTestValues
                    }) +
                GetMonitoringScrollFooter() +
                GetMonitoringScrollFunction() +
                "   <tr></td></tbody>" +
                "   <tfoot><tr><td>\r\n" +
                GetCopyRight("container", "copyRight") +
                "   </tr></td></tfoot>\r\n" +
                "   </table>\n" +
                "   </body>\r\n" +
                "  </html>";

            return boxPlotHtml;
        }

        private HtmlBoxPlotContainer GetBoxPlotContainer(string name, List<ClassicProcessTest> processTestsForContainer)
        {
            var boxPlotBoxes = new List<BoxPlotBox>();
            var position = 0;
            foreach (var processTest in processTestsForContainer)
            {
                boxPlotBoxes.Add(GetBoxPlotBoxForTest(name, position, processTest));
                position++;
            }

            var boxPlotContainerYAxis = processTestsForContainer.Last().ControlledByUnitId == MeaUnit.Nm ? _unitNm : _unitDeg;
            return new HtmlBoxPlotContainer(name, boxPlotBoxes, CtlSplitCount, boxPlotContainerYAxis, doubleformat, _fontFamily);
        }

        private BoxPlotBox GetBoxPlotBoxForTest(string name, int position, ClassicProcessTest processTest)
        {
            var sigmaBoxPlotFactor = 3.0;
            var chStandardDeviation = processTest.StandardDeviation.GetValueOrDefault(0);
            var lowerSigma = Statistic.GetLowerSigma(processTest.Average, chStandardDeviation, sigmaBoxPlotFactor);
            var upperSigma = Statistic.GetUpperSigma(processTest.Average, chStandardDeviation, sigmaBoxPlotFactor);
            var chk3SigmaKey = "x +- " + sigmaBoxPlotFactor.ToString("0.00", CultureInfo.InvariantCulture) + "s";

            var boxPlotTraces = new List<BoxPlotBoxLine>();
            var boxName = name + position;
            boxPlotTraces.Add(new BoxPlotBoxLine(processTest.LowerLimit, boxName + "_LowerLimit", Translation4TolLimits, "groupToleranceLimit", position == 0, "red", "dash"));
            boxPlotTraces.Add(new BoxPlotBoxLine(processTest.UpperLimit, boxName + "_UpperLimit", Translation4TolLimits, "groupToleranceLimit", false, "red", "dash"));
            boxPlotTraces.Add(new BoxPlotBoxLine(lowerSigma, boxName + "_LowerSigma", chk3SigmaKey, "group3Sigma", position == 0, "black", "dashdot"));
            boxPlotTraces.Add(new BoxPlotBoxLine(upperSigma, boxName + "_UpperSigma", chk3SigmaKey, "group3Sigma", false, "black", "dashdot"));
            boxPlotTraces.Add(new BoxPlotBoxLine(processTest.NominalValue, boxName + "_NominalValue", Translation4NominalValue, "groupNominal", position == 0, "green", "dash"));
            var values = new List<BoxPlotBoxDataValue>();
            processTest.TestValues.ForEach(x => values.Add(new BoxPlotBoxDataValue() { Value = x.ControlledByValue, Position = x.Position }));
            return new BoxPlotBox(processTest.Timestamp, boxName, boxPlotTraces, values, position, doubleformat);
        }

        private string GetCtlHead()
        {
            var minBoxPlotPlace = _tests.Count * 110;

            var head =
                "	<head>\r\n" +
                "		<script src=\"embedded:plotly.js\" type=\"text/javascript\"></script><style id=\"plotly.js-style-global\"></style>\r\n" +
                "		<script src=\"embedded:jquery-1.9.1.min.js\" type=\"text/javascript\"></script>\r\n" +
                "		<style>\r\n" +
                "			html {\r\n" +
                "				font-family: " + _fontFamily + ";\r\n" +
                "			}\r\n" +

                "           div.plotly-notifier {\r\n" +
                "               visibility: hidden;\r\n" +
                "           }\r\n" +

                "			@media screen\r\n" +
                "			{\r\n" +
                "				html {\r\n" +
                "					font-size: 100%;\r\n" +
                "				}\r\n" +
                "				.container {\r\n" +
                "					width:90%;\r\n" +
                "					margin: 0 auto;\r\n" +
                "					margin-bottom: 6px;\r\n" +
                "				}\r\n" +

                "				.contentHeader {\r\n" +
                "					padding-top: 4px;\r\n" +
                "				}\r\n" +

                "				.contentHeader:after {\r\n" +
                "					content: '\\002B';\r\n" +
                "					color: white;\r\n" +
                "					font-weight: bold;\r\n" +
                "					float: right;\r\n" +
                "					margin-right: 1%;\r\n" +
                "					font-size:1.2em;\r\n" +
                "				}\r\n" +

                "				.inactive:after {\r\n" +
                "					content: '\\2212';\r\n" +
                "				}\r\n" +

                "				.contentHeader:hover {\r\n" +
                "					background-color: " + _backGroundColorContent + ";\r\n" +
                "				}\r\n" +

                "				.content {\r\n" +
                "					border: 1.5px solid " + _backGroundColorContent + ";\r\n" +
                "					padding-left:7px;\r\n" +
                "					padding-right:7px;\r\n" +
                "					padding-bottom:6px;	\r\n" +
                "				}\r\n" +

                "				.headercontainer {\r\n" +
                "					background-color: " + _backGroundColorHeader + ";\r\n" +
                "					padding-top:3px;\r\n" +
                "					padding-bottom:3px;\r\n" +
                "				}\r\n" +

                "				.tableHeader {\r\n" +
                "					font-size: 0.9em;\r\n" +
                "					text-align:left;\r\n" +
                "					margin-bottom:3px;\r\n" +
                "					font-weight: bold;\r\n" +
                "				}\r\n" +

                "				.tableValueDescription {\r\n" +
                "					font-size: 0.75em;\r\n" +
                "					font-weight: bold;\r\n" +
                "					padding-right:30px;\r\n" +
                "					padding-left:4px;\r\n" +
                "					min-width:135px;\r\n" +
                "				}\r\n" +

                "				.tableValue {\r\n" +
                "					font-size: 0.75em;\r\n" +
                "					padding-right:15px;\r\n" +
                "				}	\r\n" +

                "				.flexBoxInformation {\r\n" +
                "					display: flex;\r\n" +
                "					flex-wrap: wrap;\r\n" +
                "					margin-top: -7px;\r\n" +
                "				}\r\n" +

                "				.flexBoxInformation > * {\r\n" +
                "					margin-top: 7px;\r\n" +
                "				}	\r\n" +

                "				.flexboxItemLocation {\r\n" +
                "					min-width: 30%;\r\n" +
                "					margin-right: 30px;\r\n" +
                "					padding-left:5px;\r\n" +
                "				}				\r\n" +

                "				.plotlyContainer {\r\n" +
                "					height:400px;\r\n" +
                "					overflow:hidden;\r\n" +
                "					padding-left:200px;\r\n" +
                "				}\r\n" +

                "			    .dataDescription {\r\n" +
                "				    font-size:0.70em;\r\n" +
                "				    text-align: center;\r\n" +
                "				    width:200px;\r\n" +
                "			    }\r\n" +

                "				.scrollContainer {\r\n" +
                "					min-width:" + minBoxPlotPlace + "px;\r\n" +
                "				}\r\n" +

                "				.footerScrollContainer {\r\n" +
                "					width:90%;\r\n" +
                "					position: fixed;\r\n" +
                "					left: 5%;\r\n" +
                "					right: 5%;\r\n" +
                "					bottom: 0;\r\n" +
                "					margin: 0 auto;\r\n" +
                "				}\r\n" +

                "				.footerScrollContent {\r\n" +
                "					padding-left:7px;\r\n" +
                "					padding-right:7px;\r\n" +
                "					\r\n" +
                "				}\r\n" +

                "				.footerScrollBox{\r\n" +
                "					overflow: auto;\r\n" +
                "				}\r\n" +

                "				.footerScroll {\r\n" +
                "					min-width:" + minBoxPlotPlace + "px;\r\n" +
                "					min-height:1px;\r\n" +
                "				}\r\n" +

                "               .copyRight {\r\n" +
                "                   text-align:left;\r\n" +
                "                   float: right;\r\n" +
                "                   font-size:0.6em;\r\n" +
                "                   padding-bottom:3px;\r\n" +
                "               }\r\n" +

                "               .showPrint {\r\n" +
                "                   display: none;\r\n" +
                "               }\r\n" +
                "			}\r\n" +

                "			@media print\r\n" +
                "			{\r\n" +
                "				@page {\r\n" +
                "					size: 210mm 297mm;  \r\n" +
                "					margin: 10mm; \r\n" +
                "				}\r\n" +

                "               .showScreen {\r\n" +
                "                   display: none;\r\n" +
                "               }\r\n" +

                "				html {\r\n" +
                "					font-size: 9.7pt;\r\n" +
                "					color-adjust: exact;\r\n" +
                "					-webkit-print-color-adjust: exact;\r\n" +
                "					print-color-adjust: exact;\r\n" +
                "				}\r\n" +

                "               .boxPlotContent { \r\n" +
                "                   page-break-inside: avoid;\r\n" +
                "               }\r\n" +

                "				.container {\r\n" +
                "					width:100%;\r\n" +
                "					margin: 0 auto;\r\n" +
                "					page-break-inside: avoid;\r\n" +
                "					margin-bottom: 0.5pt;\r\n" +
                "				}\r\n" +

                "				.contentHeader {\r\n" +
                "					font-size: 0.7em;\r\n" +
                "				}\r\n" +

                "				.inactive.contentHeader {\r\n" +
                "					display: none;\r\n" +
                "				}\r\n" +

                "				.content {\r\n" +
                "					border: 1.5pt solid " + _backGroundColorContent + ";\r\n" +
                "					padding-left:5pt;\r\n" +
                "					padding-right:5pt;\r\n" +
                "					padding-bottom:4pt;	\r\n" +
                "				}\r\n" +

                "				.headercontainer {\r\n" +
                "					background-color: " + _backGroundColorHeader + ";\r\n" +
                "					padding-top:2pt;\r\n" +
                "					padding-bottom:2pt;\r\n" +
                "				}\r\n" +

                "				.tableHeader {\r\n" +
                "					font-size: 0.9em;\r\n" +
                "					text-align:left;\r\n" +
                "					margin-bottom:2pt;\r\n" +
                "					font-weight: bold;\r\n" +
                "				}\r\n" +

                "				.tableValueDescription {\r\n" +
                "					font-size: 0.75em;\r\n" +
                "					font-weight: bold;\r\n" +
                "					//padding-right:15pt;\r\n" +
                "					padding-left:3pt;\r\n" +
                "					width:95pt;\r\n" +
                "				}\r\n" +

                "				.tableValue {\r\n" +
                "					font-size: 0.75em;\r\n" +
                "					padding-right:10pt;\r\n" +
                "				}	\r\n" +

                "				.flexBoxInformation {\r\n" +
                "					display: flex; \r\n" +
                "					flex-wrap: wrap; \r\n" +
                "					margin-top: -5pt;\r\n" +
                "				}\r\n" +

                "				.flexBoxInformation > * {\r\n" +
                "					margin-top: 5pt;\r\n" +
                "				}\r\n" +

                "				.flexboxItemLocation\r\n" +
                "				{\r\n" +
                "					min-width: 30%;\r\n" +
                "					margin-right: 20pt;\r\n" +
                "					padding-left:4pt;\r\n" +
                "				}\r\n" +

                "				.plotlyContainer\r\n" +
                "				{\r\n" +
                "					height:220pt;\r\n" +
                "					overflow:hidden;\r\n" +
                "					padding-left:100pt;\r\n" +
                "				}\r\n" +

                "			    .dataDescription {\r\n" +
                "				    font-size:0.70em;\r\n" +
                "				    text-align: center;\r\n" +
                "				    width:100pt;\r\n" +
                "			    }\r\n" +

                "				.footerScrollContainer {\r\n" +
                "					display: none;\r\n" +
                "				}\r\n" +

                "               .copyRight {\r\n" +
                "                   text-align:left;\r\n" +
                "                   float: right;\r\n" +
                "                   font-size:0.6em;\r\n" +
                "                   padding-bottom:2pt;\r\n" +
                "               }\r\n" +
                "   		}\r\n" +

                "			table, th, td {\r\n" +
                "				border-collapse: collapse;\r\n" +
                "			}\r\n" +

                "           .contentTable {\r\n" +
                "               width: 100%;\r\n" +
                "               margin: 0 auto;\r\n" +
                "               table-layout: fixed;\r\n" +
                "           }\r\n" +

                "			table {\r\n" +
                "				word-wrap: break-word;\r\n" +
                "			}\r\n" +

                "			.contentHeader {\r\n" +
                "				background-color: " + _backGroundColorContent + ";\r\n" +
                "				font-weight: bold; \r\n" +
                "				cursor: pointer;\r\n" +
                "				width: 100%;\r\n" +
                "				border: none;\r\n" +
                "				border-collapse: collapse;\r\n" +
                "				outline: none; \r\n" +
                "			}\r\n" +

                "			.headerTitle {\r\n" +
                "				text-align: center;\r\n" +
                "				text-align: center;\r\n" +
                "				font-size: 1.2em;\r\n" +
                "			}\r\n" +

                "			.flexboxItemTestDevice{\r\n" +
                "				min-width: 45%;\r\n" +
                "				display: flex;\r\n" +
                "			}\r\n" +

                "			.dataTable {\r\n" +
                "				width:100%;\r\n" +
                "               table-layout: fixed;\r\n" +
                "			}\r\n" +

                "			.scrollContent {\r\n" +
                "				border: 1.5pt solid " + _backGroundColorContent + ";\r\n" +
                "				overflow: hidden;\r\n" +
                "			}\r\n" +

                "			.dataTableGap {\r\n" +
                "				width: 45px;\r\n" +
                "			}\r\n" +

                "			.dataValue {\r\n" +
                "				font-size:0.70em;\r\n" +
                "				text-align: center;\r\n" +
                "				width: 100%;\r\n" +
                "			}\r\n" +
                "			.dataValueOk {\r\n" +
                "               color: green;\r\n" +
                "				font-size:0.70em;\r\n" +
                "				text-align: center;\r\n" +
                "				width: 100%;\r\n" +
                "			}\r\n" +
                "			.dataValueNotOk {\r\n" +
                "               color: red;\r\n" +
                "				font-size:0.70em;\r\n" +
                "				text-align: center;\r\n" +
                "				width: 100%;\r\n" +
                "			}\r\n" +

                "		</style>\r\n" +
                "	</head>\r\n";
            return head;
        }

        private string GetCtlTitle()
        {
            var minDate = _tests.Min(x => x.Timestamp);
            var maxDate = _tests.Max(x => x.Timestamp);
            var ctlDateStr = Translation4From + " " + minDate.ToString("d", CultureInfo.CurrentCulture);

            if (minDate != maxDate)
            {
                ctlDateStr += " " + Translation4To + " " + maxDate.ToString("d", CultureInfo.CurrentCulture);
            }

            var title = Translation4ProcessMonitoring + " " + ctlDateStr;

            var titleHtml =
                "		<div class='container'>\r\n" +
                "			<div class='headercontainer'>\r\n" +
                "				<h1 class='headerTitle'>" + WebUtility.HtmlEncode(title) + "</h1>\r\n" +
                "			</div>\r\n" +
                "		</div>\r\n";
            return titleHtml;
        }

        private string GetCtlInformation()
        {
            var informations =
                "       <div class='container'>\r\n" +
                "			<button class='contentHeader'>" + WebUtility.HtmlEncode(Translation4InfosOfTest) + "</button>\r\n" +
                "			<div class='content'>\r\n" +

                "				<div class='flexBoxInformation'>\r\n" +
                "					<div class='flexboxItemLocation'>\r\n" +
                "						<table>\r\n" +
                "							<caption class='tableHeader'>" + WebUtility.HtmlEncode(Translation4Location) + "</caption>\r\n" +
                "							<tr>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Number) + ":</td>\r\n" +
                "								<td class='tableValue'>" + WebUtility.HtmlEncode(_location.Number.ToDefaultString()) + "</td>\r\n" +
                "							</tr>\r\n" +
                "							<tr>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Description) + ":</td>\r\n" +
                "								<td class='tableValue'>" + WebUtility.HtmlEncode(_location.Description.ToDefaultString()) + "</td>\r\n" +
                "							</tr>\r\n" +
                "							<tr>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(CurrentTranslation4Path) + ":</td>\r\n" +
                "								<td class='tableValue'>" + _locationTreePath + "</td>\r\n" +
                "							</tr>\r\n" +
                "						</table>\r\n" +
                "					</div>\r\n" +
                "				</div>\r\n" +
                "			</div>\r\n" +
                "		</div>\r\n";

            return informations;
        }

        private string GetCtlBoxPlotTestHtml(bool isPrint, HtmlBoxPlotContainer screenBoxPlot, List<HtmlBoxPlotContainer> printBoxPlot,
            Dictionary<HtmlBoxPlotContainer, List<ClassicProcessTest>> boxPlotWithTests)
        {
            if (!isPrint)
            {
                var screenHtml =
                    "   <div class='showScreen'>\r\n" +
                    "       <div class='container'>\r\n" +
                    "           <button class='contentHeader' id='" + _contentHeaderBoxplot + screenBoxPlot.BoxPlotName + "'>" + WebUtility.HtmlEncode(Translation4BoxPlot) + "</button>\r\n" +
                    "           <div class='scrollContent' id='boxPlotGraph'>\r\n" +
                    "               <div class='scrollContainer'>\r\n" +
                    "                   <div class='plotlyContainer' id='boxplot" + screenBoxPlot.BoxPlotName + "'></div>\r\n" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "       </div>\r\n" +
                    screenBoxPlot.GetBoxPlotHtml() +
                    GetCtlStatisticalTable(screenBoxPlot, boxPlotWithTests[screenBoxPlot]) +
                    GetCtlTestValues(screenBoxPlot, boxPlotWithTests[screenBoxPlot]) +
                    "   </div>\r\n";
                return screenHtml;
            }

            var printHtml =
                "   <div class='showPrint'>\r\n";

            foreach (var boxPlot in printBoxPlot)
            {
                printHtml +=
                    "   <div class='boxPlotContent'>\r\n" +
                    "       <div class='container'>\r\n" +
                    "           <button class='contentHeader' id='" + _contentHeaderBoxplot + boxPlot.BoxPlotName + "'>" + WebUtility.HtmlEncode(Translation4BoxPlot) + "</button>\r\n" +
                    "           <div class='scrollContent' id='boxPlotGraph'>\r\n" +
                    "               <div class='scrollContainer'>\r\n" +
                    "                   <div class='plotlyContainer' id='boxplot" + boxPlot.BoxPlotName + "'></div>\r\n" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "       </div>\r\n" +
                    boxPlot.GetBoxPlotHtml() +
                    GetCtlStatisticalTable(boxPlot, boxPlotWithTests[boxPlot]) +
                    GetCtlTestValues(boxPlot, boxPlotWithTests[boxPlot]) +
                    "   </div>\r\n";
            }

            printHtml += "   </div>\r\n";
            return printHtml;
        }

        private string GetCtlStatisticalTableValue(List<(string, string)> values)
        {
            var dateHtml = "";
            foreach (var val in values)
            {
                dateHtml +=
                    "							<td class='"+ val.Item2 + "'>" + WebUtility.HtmlEncode(val.Item1) + "</td>\r\n";
            }

            if (values.Count >= CtlSplitCount)
            {
                return dateHtml;
            }

            var fillUpCount = CtlSplitCount - values.Count;
            for (var i = 0; i < fillUpCount; i++)
            {
                dateHtml +=
                    "							<td class='dataValue'></td>\r\n";
            }

            return dateHtml;
        }

        public string GetCtlStatisticalTable(HtmlBoxPlotContainer boxPlotContainer, List<ClassicProcessTest> tests)
        {
            var chkStatisticalDataTableValues = GetCtlStatisticalDataTableValues(tests);
            var statData =
                "		<div class='container'>\r\n" +
                "			<button class='contentHeader' id='" + _contentHeaderStatistical + boxPlotContainer.BoxPlotName + "'>" + WebUtility.HtmlEncode(Translation4StatisticalData) + "</button>\r\n" +
                "			<div class='scrollContent' id='statValues'>\r\n" +
                "				<div class='scrollContainer'>\r\n" +
                "					<table class='dataTable'>\r\n";

            foreach (var tableValues in chkStatisticalDataTableValues)
            {
                statData +=
                    "						<tr>\r\n" +
                    "							<th class='dataDescription'>" + WebUtility.HtmlEncode(tableValues.Item1) + ":</th>\r\n" +
                    "							<td class='dataTableGap'/>\r\n" +
                                                GetCtlStatisticalTableValue(tableValues.Item2) +
                    "                           <td/>\r\n" +
                    "						</tr>\r\n";
            }

            statData +=
                "					</table>\r\n" +
                "				</div>\r\n" +
                "			</div>\r\n" +
                "	    </div>\r\n";

            return statData;
        }

        private List<Tuple<string, List<(string, string)>>> GetCtlStatisticalDataTableValues(List<ClassicProcessTest> chkTestsForContainer)
        {
            var statisticalDataTableValues = new List<Tuple<string, List<(string, string)>>>();
            var dataForDate = new Tuple<string, List<(string, string)>>(Translation4Date, new List<(string, string)>());
            var dataForTime = new Tuple<string, List<(string, string)>>(Translation4Time, new List<(string, string)>());
            var numberOfValues = new Tuple<string, List<(string, string)>>(Translation4NumberOfValues, new List<(string, string)>());
            var nominalValue = new Tuple<string, List<(string, string)>>(Translation4NominalValue, new List<(string, string)>());
            var lowerLimit = new Tuple<string, List<(string, string)>>(Translation4LowerTestLimit, new List<(string, string)>());
            var upperLimit = new Tuple<string, List<(string, string)>>(Translation4UpperTestLimit, new List<(string, string)>());
            var toleranceClass = new Tuple<string, List<(string, string)>>(Translation4ToleranceClass, new List<(string, string)>());
            var average = new Tuple<string, List<(string, string)>>(Translation4Average, new List<(string, string)>());
            var standardDeviation = new Tuple<string, List<(string, string)>>(Translation4StandardDeviation, new List<(string, string)>());
            var range = new Tuple<string, List<(string, string)>>(Translation4Range, new List<(string, string)>());
            var minimum = new Tuple<string, List<(string, string)>>(Translation4Minimum, new List<(string, string)>());
            var maximum = new Tuple<string, List<(string, string)>>(Translation4Maximum, new List<(string, string)>());
            var testDevice = new Tuple<string, List<(string, string)>>(Translation4TestEquipment, new List<(string, string)>());
            var testDeviceType = new Tuple<string, List<(string, string)>>(Translation4TestEquipmentType, new List<(string, string)>());
            var result = new Tuple<string, List<(string, string)>>(Translation4Result, new List<(string, string)>());

            foreach (var test in chkTestsForContainer)
            {
                dataForDate.Item2.Add((test.Timestamp.ToString("d", CultureInfo.CurrentCulture), "dataValue"));
                dataForTime.Item2.Add((test.Timestamp.ToString("HH:mm:ss", CultureInfo.CurrentCulture), "dataValue"));
                numberOfValues.Item2.Add((test.NumberOfTests.ToString(), "dataValue"));
                nominalValue.Item2.Add((test.NominalValue.ToString(doubleformat, _culture), "dataValue"));
                lowerLimit.Item2.Add((test.LowerLimit.ToString(doubleformat, _culture), "dataValue"));
                upperLimit.Item2.Add((test.UpperLimit.ToString(doubleformat, _culture), "dataValue"));
                toleranceClass.Item2.Add((test.ToleranceClass?.Name, "dataValue"));
                average.Item2.Add((test.Average.ToString(doubleformat, _culture), "dataValue"));
                standardDeviation.Item2.Add((
                    test.StandardDeviation != null
                        ? test.StandardDeviation.Value.ToString(doubleformat, _culture)
                        : "-", "dataValueOk"));
                var rangeStr = "";
                if (test.TestValues.Count <= 1)
                {
                    rangeStr = "-";
                }
                else
                {
                    rangeStr = Statistic.GetRange(new[]
                    {
                        Math.Round(test.TestValueMinimum, doubleDecimalDigits),
                        Math.Round(test.TestValueMaximum, doubleDecimalDigits)
                    }).ToString(doubleformat, _culture);
                }

                range.Item2.Add((rangeStr, "dataValue"));
                minimum.Item2.Add((test.TestValueMinimum.ToString(doubleformat, _culture), "dataValue"));
                maximum.Item2.Add((test.TestValueMaximum.ToString(doubleformat, _culture), "dataValue"));
                testDevice.Item2.Add((test.TestEquipment?.SerialNumber.ToDefaultString(), "dataValue"));

                var testDeviceTypeStr = "";
                if (test.TestEquipment?.TestEquipmentModel != null)
                {
                    testDeviceTypeStr = TestEquipmentTypeModel.GetTranslationForTestEquipmentType(test.TestEquipment.TestEquipmentModel.Type, _localization);
                }
                testDeviceType.Item2.Add((testDeviceTypeStr, "dataValue"));
                result.Item2.Add((test.Result.IsIo ? Translation4Ok : Translation4NotOk, test.Result.IsIo ? "dataValueOk" : "dataValueNotOk"));
            }

            statisticalDataTableValues.Add(dataForDate);
            statisticalDataTableValues.Add(dataForTime);
            statisticalDataTableValues.Add(numberOfValues);
            statisticalDataTableValues.Add(nominalValue);
            statisticalDataTableValues.Add(lowerLimit);
            statisticalDataTableValues.Add(upperLimit);
            statisticalDataTableValues.Add(toleranceClass);
            statisticalDataTableValues.Add(average);
            statisticalDataTableValues.Add(standardDeviation);
            statisticalDataTableValues.Add(range);
            statisticalDataTableValues.Add(minimum);
            statisticalDataTableValues.Add(maximum);
            statisticalDataTableValues.Add(testDevice);
            statisticalDataTableValues.Add(testDeviceType);
            statisticalDataTableValues.Add(result);
            return statisticalDataTableValues;
        }

        public string GetCtlTestValues(HtmlBoxPlotContainer boxPlotContainer, List<ClassicProcessTest> tests)
        {
            var maxPos = tests.Max(x => x.TestValues.Max(y => y.Position));

            var showValues = new Dictionary<int, List<string>>();

            for (var i = 0; i <= maxPos; i++)
            {
                showValues[i] = new List<string>();

                foreach (var test in tests)
                {
                    var testValue = test.GetTestValueByPosition(i);

                    if (testValue != null)
                    {
                        showValues[i]
                            .Add((test.ControlledByUnitId == test.Unit1Id
                                ? testValue.ValueUnit1
                                : testValue.ValueUnit2).ToString(doubleformat, _culture));
                    }
                    else
                    {
                        showValues[i].Add("");
                    }
                }


                if (tests.Count >= CtlSplitCount)
                {
                    continue;
                }

                var fillUpCount = CtlSplitCount - tests.Count;
                for (var k = 0; k < fillUpCount; k++)
                {
                    showValues[i].Add("");
                }
            }

            var testvalues =
                "		<div class='container'>\r\n" +
                "			<button class='contentHeader' id='" + _contentHeaderTestValues + boxPlotContainer.BoxPlotName + "'>" + WebUtility.HtmlEncode(Translation4SingleValues) + "</button>\r\n" +
                "			<div class='scrollContent' id='testValues'>\r\n" +
                "				<div class='scrollContainer'>\r\n" +
                "						<table class='dataTable'>\r\n";

            foreach (var val in showValues)
            {
                testvalues +=
                    "					  <tr>\r\n" +
                    "						<th class='dataDescription'>" + WebUtility.HtmlEncode(Translation4Value) + " " + (val.Key + 1) + "</th>\r\n" +
                    "						<td class='dataTableGap'/>\r\n";

                foreach (var data in val.Value)
                {
                    testvalues +=
                        "						<td class='dataValue'>" + data + "</td>\r\n";
                }

                testvalues +=
                    "                       <td/>\r\n" +
                    "					  </tr>\r\n";
            }

            testvalues +=
                "					</table>\r\n" +
                "				</div>\r\n" +
                "			</div>\r\n" +
                "		</div>\r\n";

            return testvalues;
        }

        #endregion
    }
}
