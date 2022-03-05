using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using Client.Core.Entities;
using Common.Types.Enums;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Localization;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel.HtmlViewModel
{
    public class ClassicProcessPfuHtmlViewModel : ClassicTestHtmlViewModelBase
    {
        public ClassicProcessPfuHtmlViewModel(Location location, List<ClassicProcessTest> tests,
            LocalizationWrapper localization, ITreePathBuilder treePathBuilder) : base(localization)
        {
            _location = location;
            _pfuData = new ClassicPfuData(tests, doubleDecimalDigits);
            _treePathBuilder = treePathBuilder;
        }

        private void FillPfuValues()
        {
            _pfuUnit = _pfuData.ControlledByUnitId == MeaUnit.Nm ? _unitNm : _unitDeg;
            _pfuMinimum = _pfuData.TestValueMinimum;
            _pfuMaximum = _pfuData.TestValueMaximum;
            _pfuAverage = _pfuData.Average;
            _pfuRange = _pfuData.Range;
            _pfuLowerLimit = _pfuData.LowerLimit;
            _pfuUpperLimit = _pfuData.UpperLimit;
            _pfuNominalValue = _pfuData.NominalValue;

            _pfuStandardDeviationStr = _pfuData.StandardDeviation == null ? "-" : _pfuData.StandardDeviation.Value.ToString(doubleformat, _culture);
            _pfuVarianceStr = _pfuData.Variance == null ? "-" : _pfuData.Variance.Value.ToString(doubleformat, _culture);
            _pfuCmValueStr = _pfuData.CmValue == null ? "-" : _pfuData.CmValue.Value.ToString(doubleformat, _culture);
            _pfuCmkValueStr = _pfuData.CmkValue == null ? "-" : _pfuData.CmkValue.Value.ToString(doubleformat, _culture);

            _pfuResult = _pfuData.Result;
            _pfuResultStr = Translation4NotEnoughValues;
            _pfuNotEnoughValues = false;
            if (_pfuData.ClassicProcessTestValues.Count >= 10)
            {
                _pfuResultStr = _pfuResult ? Translation4Ok : Translation4NotOk;
                _pfuNotEnoughValues = true;
            }

            const double sigmaFactor = 3;
            const string sigmaDoubleFormat = "0.00";
            _pfuLower3SigmaKey = "x - " + sigmaFactor.ToString(sigmaDoubleFormat, CultureInfo.InvariantCulture) + "s";
            _pfuUpper3SigmaKey = "x + " + sigmaFactor.ToString(sigmaDoubleFormat, CultureInfo.InvariantCulture) + "s";
            _pfuLower3xS = _pfuData.GetLowerSigma(sigmaFactor);
            _pfuUpper3xS = _pfuData.GetUpperSigma(sigmaFactor);
            _locationTreePath = _treePathBuilder.GetTreePath(_location, _treeSeperator);
        }

        public override string HtmlDocument => GeneratePfuHtml();

        public override void Initialize()
        {
            FillTranslation();
            FillPfuValues();
        }

        #region PFU Properties
        private readonly Location _location;
        private readonly ClassicPfuData _pfuData;
        private readonly ITreePathBuilder _treePathBuilder;
        private string _locationTreePath;
        private bool _pfuResult;
        private string _pfuResultStr;
        private string _pfuUnit;
        private string _pfuStandardDeviationStr;
        private string _pfuCmkValueStr;
        private string _pfuCmValueStr;
        private double _pfuAverage;
        private double _pfuRange;
        private string _pfuVarianceStr;
        private double _pfuMinimum;
        private double _pfuMaximum;
        private string _pfuLower3SigmaKey;
        private string _pfuUpper3SigmaKey;
        private double _pfuLower3xS;
        private double _pfuUpper3xS;
        private double _pfuLowerLimit;
        private double _pfuUpperLimit;
        private double _pfuNominalValue;
        private bool _pfuNotEnoughValues;
        #endregion

        #region PFU Translation
        public string Translation4Pfu { get; set; }
        public string Translation4NoOutlierTests { get; set; }
        public string Translation4NoNormalDist { get; set; }
        public string Translation4LowerTestLimit { get; set; }
        public string Translation4UpperTestLimit { get; set; }
        public override void FillTranslation()
        {
            Translation4Pfu = _localization.Strings.GetParticularString("ClassicTestHtmlView", "PFU");
            Translation4LowerTestLimit = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Lower test limit");
            Translation4UpperTestLimit = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Upper test limit");
            Translation4NoOutlierTests = _localization.Strings.GetParticularString("ClassicTestHtmlView", "No outlier tests were carried out");
            Translation4NoNormalDist = _localization.Strings.GetParticularString("ClassicTestHtmlView", "No normal distribution tests were performed");
            base.FillTranslation();
        }
        #endregion

        #region PFU HTML
        private string GeneratePfuHtml()
        {
            string pfuHtml =
                "<!DOCTYPE html>\r\n" +
                "<html>\r\n" +
                GetPfuHead() +
                "   <body>\r\n" +
                GetPfuTitle() +
                GetPfuInformation() +
                GetPfuTestData() +
                GetSingleValueCard() +
                GetHistogram() +
                GetPfuSingleValueTable() +
                GetCopyRight("container", "copyRight") +
                GetResizeBoxPlotHtml(new List<(string, string)>() { ("SINGLEVALUECARD", PlotLegend()), ("HISTOGRAMGRAPH", PlotLegend()) }) +
                GetShowContainer() +
                "  </body>\r\n" +
                "  </html>";

            return pfuHtml;
        }

        private string PlotLegend()
        {
            return "legend:{font:{family:'SegoeUI',size:11}}";
        }

        private string GetPfuHead()
        {
            var pfuhead =
                "	<head>\r\n" +
                "		<script src=\"embedded:plotly.js\" type=\"text/javascript\"></script>\r\n" +
                "		<style>\r\n" +
                "				html {\r\n" +
                "					font-family: '" + _fontFamily + "';\r\n" +
                "				}\r\n" +

                "               div.plotly-notifier {\r\n" +
                "                   visibility: hidden;\r\n" +
                "               }\r\n" +

                "				@media screen\r\n" +
                "				{\r\n" +
                "					html {\r\n" +
                "						font-size: 98%;\r\n" +
                "					}\r\n" +

                "					.container {\r\n" +
                "						width:75%;\r\n" +
                "						min-width:950px;\r\n" +
                "						margin: 0 auto;\r\n" +
                "						margin-bottom: 6px;\r\n" +
                "					} \r\n" +

                "					.contentHeader {\r\n" +
                "						padding-top: 4px;\r\n" +
                "					}\r\n" +

                "					.contentHeader:after {\r\n" +
                "						content: '\\2212';\r\n" +
                "						color: white;\r\n" +
                "						font-weight: bold;\r\n" +
                "						float: right;\r\n" +
                "						margin-right: 1%;\r\n" +
                "						font-size:1.2em;\r\n" +
                "					}\r\n" +

                "					.inactive:after {\r\n" +
                "						content: '\\002B';\r\n" +
                "					}\r\n" +

                "					.contentHeader:hover {\r\n" +
                "						background-color: " + _backGroundColorContent + ";\r\n" +
                "					}\r\n" +

                "					.content {\r\n" +
                "						border: 1.5px solid " + _backGroundColorContent + ";\r\n" +
                "						padding-left:7px;\r\n" +
                "						padding-right:7px;\r\n" +
                "						padding-bottom:6px;\r\n" +
                "					}\r\n" +

                "					.headercontainer {\r\n" +
                "						background-color: " + _backGroundColorHeader + ";\r\n" +
                "						padding-top:3px;\r\n" +
                "						padding-bottom:3px;\r\n" +
                "					}\r\n" +

                "					.tableHeader {\r\n" +
                "						font-size: 0.9em; \r\n" +
                "						text-align:left; \r\n" +
                "						margin-bottom:3px;\r\n" +
                "						font-weight: bold;\r\n" +
                "					}\r\n" +

                "					.tableValueDescription {\r\n" +
                "						font-size: 0.75em;\r\n" +
                "						font-weight: bold;\r\n" +
                "						padding-right:30px;\r\n" +
                "						width:135px;\r\n" +
                "					}\r\n" +

                "					.tableValue {\r\n" +
                "						font-size: 0.75em;\r\n" +
                "						padding-right:15px;\r\n" +
                "					}\r\n" +

                "					.resultDescription {\r\n" +
                "						padding-top: 3px;\r\n" +
                "						padding-bottom: 3px;\r\n" +
                "						font-weight: bold;\r\n" +
                "						font-size: 1em;\r\n" +
                "					}\r\n" +

                "					.resultValue {\r\n" +
                "						font-size:0.8em;\r\n" +
                "						text-align:right;\r\n" +
                "						font-weight: bold;\r\n" +
                "						padding-top: 3px;\r\n" +
                "						padding-bottom: 3px;\r\n" +
                "					}\r\n" +

                "					.tableValueStatistical {\r\n" +
                "						font-size: 0.75em;\r\n" +
                "						text-align: right;\r\n" +
                "						min-width:60px;\r\n" +
                "					}\r\n" +

                "					.tableValueStatisticalGap {\r\n" +
                "						width:60px;\r\n" +
                "					}\r\n" +

                "					.statTableCmCmkGap {\r\n" +
                "						height:5px;\r\n" +
                "					}\r\n" +

                "					.flexBoxInformation {\r\n" +
                "						display: flex;\r\n" +
                "						flex-wrap: wrap;\r\n" +
                "						margin-top: -7px;\r\n" +
                "					}\r\n" +

                "					.flexBoxInformation > * {\r\n" +
                "						margin-top: 7px;\r\n" +
                "					}\r\n" +

                "					.flexboxItemLocation {\r\n" +
                "						min-width: 400px;\r\n" +
                "						margin-right: 30px;\r\n" +
                "					    padding-left:5px;\r\n" +
                "					}\r\n" +

                "					.flexboxItemToolTestDevice {\r\n" +
                "						display: flex;\r\n" +
                "						flex-wrap: nowrap;\r\n" +
                "					}\r\n" +

                "					.flexboxItemTool {\r\n" +
                "						margin-right: 30px;\r\n" +
                "					}\r\n" +

                "					.flexboxItemStat {\r\n" +
                "						margin-right:5%;\r\n" +
                "					}\r\n" +

                "					.plotlyContainer {\r\n" +
                "						height:325px;\r\n" +
                "						overflow:hidden;\r\n" +
                "						width:95%;\r\n" +
                "					}\r\n" +

                "					.tableSingleValues {\r\n" +
                "						font-size: 0.6em;\r\n" +
                "					}\r\n" +

                "					.copyRight {\r\n" +
                "						text-align:left;\r\n" +
                "						float: right;\r\n" +
                "						font-size:0.6em;\r\n" +
                "						padding-bottom:3px;\r\n" +
                "					}\r\n" +
                "				}\r\n" +

                "				@media print\r\n" +
                "				{\r\n" +
                "					@page {\r\n" +
                "						size: 210mm 297mm;\r\n" +
                "						margin: 10mm;\r\n" +
                "					}\r\n" +

                "					html {\r\n" +
                "						font-size: 9.7pt;\r\n" +
                "						color-adjust: exact;\r\n" +
                "						-webkit-print-color-adjust: exact;\r\n" +
                "						print-color-adjust: exact;\r\n" +
                "					}\r\n" +

                "					.container {\r\n" +
                "						width:100%;\r\n" +
                "						margin: 0 auto;\r\n" +
                "						page-break-inside: avoid;\r\n" +
                "						margin-bottom: 0.5pt;\r\n" +
                "					}\r\n" +

                "					.contentHeader {\r\n" +
                "						font-size: 0.7em;\r\n" +
                "					}\r\n" +

                "					.inactive.contentHeader {\r\n" +
                "						display: none;\r\n" +
                "					}\r\n" +

                "					.content {\r\n" +
                "						border: 1.5pt solid " + _backGroundColorContent + ";\r\n" +
                "						padding-left:5pt;\r\n" +
                "						padding-right:5pt;\r\n" +
                "						padding-bottom:4pt;\r\n" +
                "					}\r\n" +

                "					.headercontainer {\r\n" +
                "						background-color: " + _backGroundColorHeader + ";\r\n" +
                "						padding-top:2pt;\r\n" +
                "						padding-bottom:2pt;\r\n" +
                "					}\r\n" +

                "					.tableHeader {\r\n" +
                "						font-size: 0.9em;\r\n" +
                "						text-align:left;\r\n" +
                "						margin-bottom:2pt;\r\n" +
                "						font-weight: bold;\r\n" +
                "					}\r\n" +

                "					.tableValueDescription {\r\n" +
                "						font-size: 0.75em;\r\n" +
                "						font-weight: bold;\r\n" +
                "						width:95pt;\r\n" +
                "					}\r\n" +

                "					.tableValue {\r\n" +
                "						font-size: 0.75em;\r\n" +
                "						padding-right:10pt;\r\n" +
                "					}\r\n" +

                "					.resultDescription {\r\n" +
                "						padding-top: 2pt;\r\n" +
                "						padding-bottom: 2pt;\r\n" +
                "						font-weight: bold;\r\n" +
                "						font-size: 1em;\r\n" +
                "					}\r\n" +

                "					.resultValue {\r\n" +
                "						font-size:0.8em;\r\n" +
                "						text-align:right;\r\n" +
                "						font-weight: bold;\r\n" +
                "						padding-top: 2pt;\r\n" +
                "						padding-bottom: 2pt;\r\n" +
                "					}\r\n" +

                "					.tableValueStatistical {\r\n" +
                "						font-size: 0.75em;\r\n" +
                "						text-align: right;\r\n" +
                "						min-width:30pt;\r\n" +
                "					}\r\n" +

                "					.tableValueStatisticalGap {\r\n" +
                "						width:5%;\r\n" +
                "					}\r\n" +

                "					.statTableCmCmkGap {\r\n" +
                "						height:2pt;\r\n" +
                "					}\r\n" +

                "					.flexBoxInformation {\r\n" +
                "						display: flex;\r\n" +
                "						flex-wrap: wrap;\r\n" +
                "						margin-top: -5pt;\r\n" +
                "						width: 100%;\r\n" +
                "					}\r\n" +

                "					.flexBoxInformation > * {\r\n" +
                "						margin-top: 5pt;\r\n" +
                "					}\r\n" +

                "				    .flexboxItemLocation {\r\n" +
                "					    padding-left:4pt;\r\n" +
                "				    }\r\n" +

                "					.flexboxItemToolTestDevice {\r\n" +
                "						width: 100%;\r\n" +
                "						max-width: 100%;\r\n" +
                "						display: flex;\r\n" +
                "						flex-wrap: nowrap;\r\n" +
                "						overflow:hidden;\r\n" +
                "					}\r\n" +

                "					.flexboxItemTool {\r\n" +
                "						margin-right: 20pt;\r\n" +
                "						max-width:50%;\r\n" +
                "						min-width:45%;\r\n" +
                "					}\r\n" +

                "					.flexboxItemTestDevice {\r\n" +
                "						max-width:50%;\r\n" +
                "						min-width:45%;\r\n" +
                "					}\r\n" +

                "					.flexboxItemStat {\r\n" +
                "						width: 100%;\r\n" +
                "					}\r\n" +

                "					.tableStatistical {\r\n" +
                "						width:100%;\r\n" +
                "					}\r\n" +

                "					.plotlyContainer {\r\n" +
                "						height:160pt;\r\n" +
                "						overflow:auto;\r\n" +
                "						width:100%;\r\n" +
                "					}\r\n" +

                "					.tableSingleValues {\r\n" +
                "						font-size: 0.6em; \r\n" +
                "						line-height: 0.9;\r\n" +
                "					}\r\n" +

                "					.copyRight {\r\n" +
                "						text-align:left;\r\n" +
                "						float: right;\r\n" +
                "						font-size:0.6em;\r\n" +
                "						padding-bottom:2pt;\r\n" +
                "					}\r\n" +
                "				}\r\n" +

                "				table, th, td {\r\n" +
                "					border-collapse: collapse;\r\n" +
                "				}\r\n" +

                "				td {\r\n" +
                "					word-break:break-all;\r\n" +
                "				}\r\n" +

                "				.contentHeader {\r\n" +
                "					background-color: " + _backGroundColorContent + ";\r\n" +
                "					font-weight: bold;\r\n" +
                "					cursor: pointer;\r\n" +
                "					width: 100%;\r\n" +
                "					border: none;\r\n" +
                "					border-collapse: collapse;\r\n" +
                "					outline: none;\r\n" +
                "				}\r\n" +

                "				.headerTitle {\r\n" +
                "					text-align: center;\r\n" +
                "					font-size: 1.2em;\r\n" +
                "				}\r\n" +

                "				.statisticalNote {\r\n" +
                "					font-size: 0.75em;\r\n" +
                "				}\r\n" +

                "				.flexboxTestData {\r\n" +
                "					display: flex;\r\n" +
                "					flex-wrap: wrap;\r\n" +
                "				}\r\n" +

                "				.singleValueNotOk {\r\n" +
                "					color: red;\r\n" +
                "					word-break: keep-all;\r\n" +
                "				}\r\n" +

                "				.singleValueOk {\r\n" +
                "					color: black;\r\n" +
                "					word-break: keep-all;\r\n" +
                "				}\r\n" +

                "				.singleValueCol {\r\n" +
                "					width: 10%;\r\n" +
                "				}\r\n" +

                "			    .resultValueOk {\r\n" +
                "					color: green;\r\n" +
                "				}\r\n" +

                "				.resultValueNotOk {\r\n" +
                "					color: red;\r\n" +
                "				}\r\n" +

                "				.resultValueNotEnoughValues {\r\n" +
                "					color: blue;\r\n" +
                "				}\r\n" +

                "		</style>\r\n" +
                "	</head>\r\n";
            return pfuhead;
        }
        
        private string GetPfuTitle()
        {
            var minDate = _pfuData.ClassicProcessTests.Min(x => x.Timestamp);
            var maxDate = _pfuData.ClassicProcessTests.Max(x => x.Timestamp);
            var ctlDateStr = Translation4From + " " + minDate.ToString("d", CultureInfo.CurrentCulture);

            if (minDate != maxDate)
            {
                ctlDateStr += " " + Translation4To + " " + maxDate.ToString("d", CultureInfo.CurrentCulture);
            }

            var title = Translation4Pfu + " " + ctlDateStr;

            var titleHtml =
                "		<div class='container'>\r\n" +
                "			<div class='headercontainer'>\r\n" +
                "				<h1 class='headerTitle'>" + WebUtility.HtmlEncode(title) + "</h1>\r\n" +
                "			</div>\r\n" +
                "		</div>\r\n";
            return titleHtml;
        }

        private string GetPfuInformation()
        {
            var information =
                "		<div class='container'>\r\n" +
                "			<button class='contentHeader'>" + WebUtility.HtmlEncode(Translation4InfosOfTest) + "</button>\r\n" +
                "			<div class='content'>\r\n" +

                "				<div class='flexBoxInformation'>\r\n" +
                "					<div class='flexboxItemLocation'>\r\n" +
                "						<table>\r\n" +
                "							<caption class='tableHeader'>" + WebUtility.HtmlEncode(Translation4Location) + ":</caption>\r\n" +
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

            return information;
        }

        private string GetPfuTestData()
        {
            var resultColor = "resultValueNotEnoughValues";
            if (_pfuNotEnoughValues)
            {
                resultColor = _pfuResult ? "resultValueOk" : "resultValueNotOk";
            }
            

            var testData =
                "		<div class='container'>\r\n" +
                "			<button class='contentHeader'>" + WebUtility.HtmlEncode(Translation4DataOfTest) + "</button>\r\n" +

                "			<div class='content'>\r\n" +
                "				<div class='flexboxTestData'>\r\n" +
                "					<div class='flexboxItemStat'>\r\n" +
                "						<table class='tableStatistical'>\r\n" +
                "							<caption class='tableHeader'>" + WebUtility.HtmlEncode(Translation4StatisticalData) + "</caption> \r\n" +
                "							<tr>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Unit) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + WebUtility.HtmlEncode(_pfuUnit) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4NumberOfValues) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + WebUtility.HtmlEncode(_pfuData.ClassicProcessTestValues.Count.ToString()) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4StandardDeviation) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _pfuStandardDeviationStr + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "							</tr>\r\n" +
                "							<tr>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4NominalValue) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _pfuNominalValue.ToString(doubleformat, _culture) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Average) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _pfuAverage.ToString(doubleformat, _culture) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Variance) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _pfuVarianceStr + "</td>\r\n" +
                "							</tr>\r\n" +
                "							<tr>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4LowerTestLimit) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _pfuLowerLimit.ToString(doubleformat, _culture) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Minimum) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _pfuMinimum.ToString(doubleformat, _culture) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(_pfuLower3SigmaKey) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _pfuLower3xS.ToString(doubleformat, _culture) + "</td>\r\n" +
                "							</tr>\r\n" +
                "							<tr>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4UpperTestLimit) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _pfuUpperLimit.ToString(doubleformat, _culture) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                 "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Maximum) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _pfuMaximum.ToString(doubleformat, _culture) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(_pfuUpper3SigmaKey) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _pfuUpper3xS.ToString(doubleformat, _culture) + "</td>\r\n" +
                "							</tr>\r\n" +
                "							<tr>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4ToleranceClass) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + WebUtility.HtmlEncode(_pfuData.ToleranceClass?.Name) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Range) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _pfuRange.ToString(doubleformat, _culture) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "							</tr>\r\n" +
                "                           <tr class='statTableCmCmkGap'></tr>\r\n" +
                "							<tr>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Cm) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _pfuCmValueStr + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "							</tr>\r\n" +
                "							<tr>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Cmk) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _pfuCmkValueStr + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "							</tr>\r\n" +
                "							<tr>\r\n" +
                "								<td><div class='resultDescription'>" + WebUtility.HtmlEncode(Translation4Result) + ":</div></td>\r\n" +
                "								<td class='resultValue " + resultColor + "'><div>" + WebUtility.HtmlEncode(_pfuResultStr) + "</div></td>\r\n" +
                "							</tr>\r\n" +
                "						</table>\r\n" +
                "						\r\n" +
                "					</div>\r\n" +

                "					<div class='flexboxItemStatNote'>\r\n" +
                "						<table>\r\n" +
                "							<caption class='tableHeader'>" + WebUtility.HtmlEncode(Translation4Note) + ":</caption>\r\n" +
                "							<tr>\r\n" +
                "								<td class='statisticalNote'>" + WebUtility.HtmlEncode(Translation4NoOutlierTests) + "</td>\r\n" +
                "							</tr>\r\n" +
                "							<tr>\r\n" +
                "								<td class='statisticalNote'>" + WebUtility.HtmlEncode(Translation4NoNormalDist) + "</td>\r\n" +
                "							</tr>\r\n" +
                "						</table>\r\n" +
                "					</div>\r\n" +
                "				</div>\r\n" +
                "			</div>\r\n" +
                "		</div>\r\n";

            return testData;
        }

        private string GetSingleValueCard()
        {
            var singleValueCardLines = new List<SingleValueCardLine>
            {
                new SingleValueCardLine(_pfuAverage, Translation4Average, "blue", "dashdot"),
                new SingleValueCardLine(_pfuNominalValue, Translation4NominalValue, "#009D66", "solid"),
                new SingleValueCardLine(_pfuUpperLimit, Translation4UpperLimit, "red", "solid"),
                new SingleValueCardLine(_pfuLowerLimit, Translation4LowerLimit, "red", "solid"),
                new SingleValueCardLine(_pfuLower3xS, _pfuLower3SigmaKey, "black", "dash"),
                new SingleValueCardLine(_pfuUpper3xS, _pfuUpper3SigmaKey, "black", "dash")
            };

            var singleValueCardValues = new List<SingleValueCardValue>();
            var counter = 0;
            foreach (var testVal in _pfuData.ClassicProcessTestValues)
            {
                singleValueCardValues.Add(new SingleValueCardValue
                {
                    value = testVal.ControlledByValue,
                    Position = counter,
                    Color = testVal.ControlledByValue > _pfuUpperLimit || testVal.ControlledByValue < _pfuLowerLimit ? "red" : "green"
                });
                counter++;
            }

            var singleValueCard = new HtmlSingleValueCard("SINGLEVALUECARD", "singleValueCard", singleValueCardValues, singleValueCardLines,
                doubleformat, _fontFamily, _pfuUnit);

            var html =
                "       <div class='container'>\r\n" +
                "           <button class='contentHeader'>" + WebUtility.HtmlEncode(Translation4SingleValueCard) + "</button>\r\n" +
                "           <div class='content'>\r\n" +
                "               <div id='singleValueCard' class='plotlyContainer'></div>\r\n" +
                "           </div>\r\n" 
                + singleValueCard.GetHtml() +
                "       </div>\r\n";

            return html;
        }

        private string GetHistogram()
        {
            var histogramLines = new List<HistogramLine>
            {
                new HistogramLine(_pfuAverage, Translation4Average, "blue", "dashdot"),
                new HistogramLine(_pfuNominalValue, Translation4NominalValue, "#009D66", "solid"),
                new HistogramLine(_pfuUpperLimit, Translation4UpperLimit, "red", "solid"),
                new HistogramLine(_pfuLowerLimit, Translation4LowerLimit, "red", "solid"),
                new HistogramLine(_pfuLower3xS, _pfuLower3SigmaKey, "black", "dash"),
                new HistogramLine(_pfuUpper3xS, _pfuUpper3SigmaKey, "black", "dash")
            };

            var histogramValues = new List<HistogramValue>();
            var counter = 1;
            foreach (var testVal in _pfuData.ClassicProcessTestValues)
            {
                histogramValues.Add(new HistogramValue
                {
                    value = testVal.ControlledByValue,
                    Position = counter
                });
                counter++;
            }

            var histogram = new HtmlHistogram("HISTOGRAMGRAPH", "histogramGraph", histogramValues, histogramLines,
                _localization, doubleformat, _fontFamily);
            if (_pfuLower3xS != _pfuUpper3xS)
            {
                histogram.SetGaussLinePositions(_pfuLower3xS, _pfuUpper3xS, _pfuAverage);
            }
            else
            {
                histogram.SetGaussLinePositions(_pfuLowerLimit, _pfuUpperLimit, _pfuAverage);
            }
            
            var html =
                "       <div class='container'>\r\n" +
                "           <button class='contentHeader'>" + WebUtility.HtmlEncode(Translation4Histogram) + "</button>\r\n" +
                "           <div class='content'>\r\n" +
                "               <div class='plotlyContainer' id='histogramGraph'></div>\r\n" +
                "           </div>\r\n" 
                            + histogram.GetHtml() +
                "       </div>\r\n";
            return html;
        }

        private string GetPfuSingleValueTable()
        {
            var valueTable =
                "       <div class='container'>\r\n" +
                "           <button class='contentHeader'>" + WebUtility.HtmlEncode(Translation4SingleValues) + "</button>\r\n" +
                "           <div class='content'>\r\n" +
                "               <table class='tableSingleValues'>\r\n" +
                "			        <tr>\r\n";

            var counter = 1;

            foreach (var value in _pfuData.ClassicProcessTestValues)
            {
                var testValue = value.ControlledByValue;

                var positionStrCol = "<td class='singleValueOk'> " + counter + ".</td>";
                var testValueStrCol = "<td class='singleValueOk'>" + testValue.ToString(doubleformat, _culture) + "</td>";
                if (testValue > _pfuUpperLimit || testValue < _pfuLowerLimit)
                {
                    positionStrCol = "<td class='singleValueNotOk'> " + (value.Position + 1) + ".</td>";
                    testValueStrCol = "<td class='singleValueNotOk'>" + testValue.ToString(doubleformat, _culture) + "</td>";
                }

                valueTable +=
                    "			            " + positionStrCol + testValueStrCol + "<td class='singleValueCol'/>\r\n";

                if (counter % 10 == 0)
                {
                    valueTable += "			        </tr>\r\n";
                    valueTable += "			        <tr>\r\n";
                }
                counter++;
            }

            valueTable +=
                "			        </tr>\r\n" +
                "               </table>\r\n" +
                "           </div>\r\n" +
                "       </div>\r\n";

            return valueTable;
        }

        #endregion
    }
}
