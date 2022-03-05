using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using Common.Types.Enums;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Localization;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel.HtmlViewModel
{
    public class ClassicToolMfuTestHtmlViewModel : ClassicTestHtmlViewModelBase
    {      
        public ClassicToolMfuTestHtmlViewModel(Location location, Tool tool, List<ClassicMfuTest> tests, LocalizationWrapper localization, ITreePathBuilder treePathBuilder) : base(localization)
        {
            _treePathBuilder = treePathBuilder;
            _location = location;
            _tool = tool;
            _mfuTest = tests.FirstOrDefault();
            _mfuTest = tests.First();
        }

        private void FillMfuValues()
        {
            if (_mfuTest.ControlledByUnitId == MeaUnit.Nm)
            {
                _mfuNominalValueString = _mfuTest.NominalValueUnit1.ToString(doubleformat, _culture);
            }
            else
            {
                _mfuNominalValueString = _mfuTest.ThresholdTorque.ToString(doubleformat, _culture) + " " + _unitNm + " + " +
                                         _mfuTest.NominalValueUnit2.ToString(doubleformat, _culture) + " " + _unitDeg;
            }

            _mfuUnit = _mfuTest.ControlledByUnitId == MeaUnit.Nm ? _unitNm : _unitDeg;
            _mfuLowerTolerance = _mfuTest.LowerLimit;
            _mfuUpperTolerance = _mfuTest.UpperLimit;
            _mfuMinimum = _mfuTest.TestValueMinimum;
            _mfuMaximum = _mfuTest.TestValueMaximum;
            _mfuNominalValue = _mfuTest.NominalValueUnit1;

            _mfuResult = _mfuTest.Result.IsIo;
            _mfuResultStr = _mfuResult ? Translation4Ok : Translation4NotOk;

            _mfuTestValues = _mfuTest.TestValues.Select(x => x.ControlledByValue).ToList();

            _mfuStandardDeviationStr = _mfuTest.StandardDeviation != null
                ? _mfuTest.StandardDeviation.Value.ToString(doubleformat, _culture)
                : "-";

            _mfuAverage = _mfuTest.Average;

            _mfuCmValue = _mfuTest.Cm;
            _mfuCmkValue = _mfuTest.Cmk;
            _mfuRange = Statistic.GetRange(new[] { Math.Round(_mfuMinimum, doubleDecimalDigits), Math.Round(_mfuMaximum, doubleDecimalDigits) });
            _mfuVariance = Statistic.GetVariance(_mfuTestValues.ToArray());

            const double sigmaFactor = 3;
            const string sigmaDoubleFormat = "0.00";
            _mfuLower3SigmaKey = "x - " + sigmaFactor.ToString(sigmaDoubleFormat, CultureInfo.InvariantCulture) + "s";
            _mfuUpper3SigmaKey = "x + " + sigmaFactor.ToString(sigmaDoubleFormat, CultureInfo.InvariantCulture) + "s";
            _mfuLower3xS = Statistic.GetLowerSigma(_mfuAverage, _mfuTest.StandardDeviation.GetValueOrDefault(0), sigmaFactor);
            _mfuUpper3xS = Statistic.GetUpperSigma(_mfuAverage, _mfuTest.StandardDeviation.GetValueOrDefault(0), sigmaFactor);
            _locationTreePath = _treePathBuilder.GetDeMaskedTreePathFromBase64(_mfuTest.TestLocation?.LocationTreePath, _treeSeperator);
        }
        public override void Initialize()
        {
            FillTranslation();
            FillMfuValues();
        }

        public override string HtmlDocument => GenerateMfuHtml();

        #region Properties
        private readonly ITreePathBuilder _treePathBuilder;
        private readonly Location _location;
        private readonly Tool _tool;
        private ClassicMfuTest _mfuTest;
        private List<double> _mfuTestValues;
        private string _mfuUnit;
        private double _mfuNominalValue;
        private string _mfuNominalValueString;
        private double _mfuLowerTolerance;
        private double _mfuUpperTolerance;
        private double _mfuMinimum;
        private double _mfuMaximum;
        private string _mfuStandardDeviationStr;
        private double _mfuCmkValue;
        private double _mfuCmValue;
        private double _mfuAverage;
        private string _mfuLower3SigmaKey;
        private string _mfuUpper3SigmaKey;
        private double _mfuLower3xS;
        private double _mfuUpper3xS;
        private bool _mfuResult;
        private string _mfuResultStr;
        private double _mfuRange;
        private double _mfuVariance;
        private string _locationTreePath;
        #endregion

        #region MFU Translation
        public string Translation4SensorSerialNumber { get; set; }
        public string Translation4TestEquipmentModel { get; set; }
        public string Translation4Mca { get; set; }
        public string Translation4NoOutlierTests { get; set; }
        public string Translation4NoNormalDist { get; set; }
        

        public override void FillTranslation()
        {            
            Translation4SensorSerialNumber = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Sensor serial number");
            Translation4TestEquipmentModel = _localization.Strings.GetParticularString("ClassicTestHtmlView", "Test device model");
            Translation4Mca = _localization.Strings.GetParticularString("ClassicTestHtmlView", "MCA (rotating)");
            Translation4NoOutlierTests = _localization.Strings.GetParticularString("ClassicTestHtmlView", "No outlier tests were carried out");
            Translation4NoNormalDist = _localization.Strings.GetParticularString("ClassicTestHtmlView", "No normal distribution tests were performed");

            base.FillTranslation();
        }
        #endregion

        #region generiere MFU

        private string GenerateMfuHtml()
        {
            string mfuHtml =
                "<!DOCTYPE html>\r\n" +
                "<html>\r\n" +
                    GetMfuHead() +
                "   <body>\r\n" +
                        GetMfuTitle() +
                        GetMfuInformations() +
                        GetMfuTestDatas() +
                        GetSingleValueCard() +
                        GetHistogram() +
                        GetMfuSingleValueTable() +
                        GetCopyRight("container", "copyRight") +
                        GetResizeBoxPlotHtml(new List<(string, string)>() { ("SINGLEVALUECARD", PlotLegend()), ("HISTOGRAMGRAPH", PlotLegend()) }) +
                        GetShowContainer() +
                "  </body>\r\n" +
                "  </html>";

            return mfuHtml;
        }

        private string PlotLegend()
        {
            return "legend:{font:{family:'SegoeUI',size:11}}";
        }

        private string GetMfuHead()
        {
            var mfuhead =
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
                "		</style>\r\n" +
                "	</head>\r\n";
            return mfuhead;
        }

        private string GetMfuTitle()
        {
            var title = Translation4Mca + " " + Translation4From + " " + _mfuTest.Timestamp.ToString(CultureInfo.CurrentCulture);
            var titleHtml =
                "		<div class='container'>\r\n" +
                "			<div class='headercontainer'>\r\n" +
                "				<h1 class='headerTitle'>" + WebUtility.HtmlEncode(title) + "</h1>\r\n" +
                "			</div>\r\n" +
                "		</div>\r\n";
            return titleHtml;
        }

        private string GetMfuInformations()
        {
            var informations =
                "		<div class='container'>\r\n" +
                "			<button class='contentHeader'>" + WebUtility.HtmlEncode(Translation4InfosOfTest) + "</button>\r\n" +
                "			<div class='content'>\r\n" +

                "				<div class='flexBoxInformation'>\r\n" +
                "				  \r\n" +
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
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Path) + ":</td>\r\n" +
                "								<td class='tableValue'>" + _locationTreePath + "</td>\r\n" +
                "							</tr>\r\n" +
                "						</table>\r\n" +
                "					</div>\r\n" +

                "					<div class='flexboxItemToolTestDevice'>\r\n" +
                "						<div class='flexboxItemTool'>\r\n" +
                "							<table>\r\n" +
                "								<caption class='tableHeader'>" + WebUtility.HtmlEncode(Translation4Tool) + "</caption>\r\n" +
                "								<tr>\r\n" +
                "									<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4SerialNumber) + ":</td>\r\n" +
                "									<td class='tableValue'>" + WebUtility.HtmlEncode(_tool?.SerialNumber?.ToDefaultString()) + "</td>\r\n" +
                "								</tr>\r\n" +
                "								<tr>\r\n" +
                "									<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4InventoryNumber) + ":</td>\r\n" +
                "									<td class='tableValue'>" + WebUtility.HtmlEncode(_tool?.InventoryNumber?.ToDefaultString()) + "</td>\r\n" +
                "								</tr>\r\n" +
                "								<tr>\r\n" +
                "									<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4ToolModel) + ":</td>\r\n" +
                "									<td class='tableValue'>" + WebUtility.HtmlEncode(_tool?.ToolModel?.Description?.ToDefaultString()) + "</td>\r\n" +
                "								</tr>\r\n" +
                "								<tr>\r\n" +
                "									<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Manufacturer) + ":</td>\r\n" +
                "									<td class='tableValue'>" + WebUtility.HtmlEncode(_tool?.ToolModel?.Manufacturer?.Name.ToDefaultString()) + "</td>\r\n" +
                "								</tr>\r\n" +
                "								<tr>\r\n" +
                "									<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4ToolType) + ":</td>\r\n" +
                "									<td class='tableValue'>" + WebUtility.HtmlEncode(GetModelTypeString()) + "</td>\r\n" +
                "								</tr>\r\n" +
                "							</table>\r\n" +
                "						</div>\r\n" +

                "						<div class='flexboxItemTestDevice'>\r\n" +
                "							<table>\r\n" +
                "								<caption class='tableHeader'>" + WebUtility.HtmlEncode(Translation4TestEquipment) + "</caption>\r\n" +
                "								<tr>\r\n" +
                "									<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4SerialNumber) + ":</td>\r\n" +
                "									<td class='tableValue'>" + WebUtility.HtmlEncode(_mfuTest.TestEquipment?.SerialNumber.ToDefaultString()) + "</td>\r\n" +
                "								</tr>\r\n" +
                "								<tr>\r\n" +
                "									<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4SensorSerialNumber) + ":</td>\r\n" +
                "									<td class='tableValue'>" + WebUtility.HtmlEncode(_mfuTest.SensorSerialNumber) + "</td>\r\n" +
                "								</tr>\r\n" +
                "								<tr>\r\n" +
                "									<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4TestEquipmentModel) + ":</td>\r\n" +
                "									<td class='tableValue'>" + WebUtility.HtmlEncode(_mfuTest.TestEquipment?.TestEquipmentModel?.TestEquipmentModelName.ToDefaultString()) + "</td>\r\n" +
                "								</tr>\r\n" +
                "							</table>\r\n" +
                "						</div>\r\n" +
                "					</div>\r\n" +
                "				</div>\r\n" +
                "			</div>\r\n" +
                "		</div>\r\n";

            return informations;
        }

        private string GetModelTypeString()
        {
            if (_tool?.ToolModel?.ModelType == null)
                return "";

            var abstractToolTypeModel = AbstractToolTypeModel.MapToolTypeToToolTypeModel(_tool.ToolModel.ModelType, _localization);
            return abstractToolTypeModel != null ? abstractToolTypeModel.TranslatedName : "";
        }

        private string GetMfuTestDatas()
        {
            var resultColor = _mfuResult ? "resultValueOk" : "resultValueNotOk";

            var mfuTestDatas =
                "		<div class='container'>\r\n" +
                "			<button class='contentHeader'>" + WebUtility.HtmlEncode(Translation4DataOfTest) + "</button>\r\n" +

                "			<div class='content'>\r\n" +
                "				<div class='flexboxTestData'>\r\n" +
                "					<div class='flexboxItemStat'>\r\n" +
                "						<table class='tableStatistical'>\r\n" +
                "							<caption class='tableHeader'>" + WebUtility.HtmlEncode(Translation4StatisticalData) + "</caption> \r\n" +
                "							<tr>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Unit) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + WebUtility.HtmlEncode(_mfuUnit) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4NumberOfValues) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + WebUtility.HtmlEncode(_mfuTest.TestValues.Count.ToString()) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4StandardDeviation) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _mfuStandardDeviationStr + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "							</tr>\r\n" +
                "							<tr>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4NominalValue) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _mfuNominalValueString + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Average) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _mfuAverage.ToString(doubleformat, _culture) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Variance) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _mfuVariance.ToString(doubleformat, _culture) + "</td>\r\n" +
                "							</tr>\r\n" +
                "							<tr>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4LowerLimit) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _mfuLowerTolerance.ToString(doubleformat, _culture) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Minimum) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _mfuMinimum.ToString(doubleformat, _culture) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(_mfuLower3SigmaKey) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _mfuLower3xS.ToString(doubleformat, _culture) + "</td>\r\n" +
                "							</tr>\r\n" +
                "							<tr>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4UpperLimit) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _mfuUpperTolerance.ToString(doubleformat, _culture) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Maximum) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _mfuMaximum.ToString(doubleformat, _culture) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(_mfuUpper3SigmaKey) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _mfuUpper3xS.ToString(doubleformat, _culture) + "</td>\r\n" +
                "							</tr>\r\n" +
                "							<tr>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4ToleranceClass) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + WebUtility.HtmlEncode(_mfuTest.ToleranceClass?.Name) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Range) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _mfuRange.ToString(doubleformat, _culture) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "							</tr>\r\n" +
                "                           <tr class='statTableCmCmkGap'></tr>\r\n" +
                "							<tr>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Cm) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _mfuCmValue.ToString(doubleformat, _culture) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "							</tr>\r\n" +
                "							<tr>\r\n" +
                "								<td class='tableValueDescription'>" + WebUtility.HtmlEncode(Translation4Cmk) + ":</td>\r\n" +
                "								<td class='tableValueStatistical'>" + _mfuCmkValue.ToString(doubleformat, _culture) + "</td><td class='tableValueStatisticalGap'/>\r\n" +
                "							</tr>\r\n" +
                "							<tr>\r\n" +
                "								<td><div class='resultDescription'>" + WebUtility.HtmlEncode(Translation4Result) + ":</div></td>\r\n" +
                "								<td class='resultValue " + resultColor + "'><div>" + WebUtility.HtmlEncode(_mfuResultStr) + "</div></td>\r\n" +
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

            return mfuTestDatas;
        }

        private string GetSingleValueCard()
        {
            var singleValueCardLines = new List<SingleValueCardLine>
            {
                new SingleValueCardLine(_mfuAverage, Translation4Average, "blue", "dashdot"),
                new SingleValueCardLine(_mfuNominalValue, Translation4NominalValue, "#009D66", "solid"),
                new SingleValueCardLine(_mfuUpperTolerance, Translation4UpperLimit, "red", "solid"),
                new SingleValueCardLine(_mfuLowerTolerance, Translation4LowerLimit, "red", "solid"),
                new SingleValueCardLine(_mfuLower3xS, _mfuLower3SigmaKey, "black", "dash"),
                new SingleValueCardLine(_mfuUpper3xS, _mfuUpper3SigmaKey, "black", "dash")
            };

            var singleValueCardValues = new List<SingleValueCardValue>();
            foreach (var testVal in _mfuTest.TestValues)
            {
                singleValueCardValues.Add(new SingleValueCardValue
                {
                    value = testVal.ControlledByValue,
                    Position = testVal.Position,
                    Color = testVal.ControlledByValue > _mfuUpperTolerance || testVal.ControlledByValue < _mfuLowerTolerance ? "red" : "green"
                });
            }

            var singleValueCard = new HtmlSingleValueCard("SINGLEVALUECARD", "singleValueCard", singleValueCardValues, singleValueCardLines,
                doubleformat, _fontFamily, _mfuUnit);

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
                new HistogramLine(_mfuAverage, Translation4Average, "blue", "dashdot"),
                new HistogramLine(_mfuNominalValue, Translation4NominalValue, "#009D66", "solid"),
                new HistogramLine(_mfuUpperTolerance, Translation4UpperLimit, "red", "solid"),
                new HistogramLine(_mfuLowerTolerance, Translation4LowerLimit, "red", "solid"),
                new HistogramLine(_mfuLower3xS, _mfuLower3SigmaKey, "black", "dash"),
                new HistogramLine(_mfuUpper3xS, _mfuUpper3SigmaKey, "black", "dash")
            };

            var histogramValues = new List<HistogramValue>();
            foreach (var testVal in _mfuTest.TestValues)
            {
                histogramValues.Add(new HistogramValue
                {
                    value = testVal.ControlledByValue,
                    Position = testVal.Position
                });
            }

            var histogram = new HtmlHistogram("HISTOGRAMGRAPH", "histogramGraph", histogramValues, histogramLines,
                _localization, doubleformat, _fontFamily);
            histogram.SetGaussLinePositions(_mfuLower3xS, _mfuUpper3xS, _mfuAverage);

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

        private string GetMfuSingleValueTable()
        {
            var valueTable =
                "       <div class='container'>\r\n" +
                "           <button class='contentHeader'>" + WebUtility.HtmlEncode(Translation4SingleValues) + "</button>\r\n" +
                "           <div class='content'>\r\n" +
                "               <table class='tableSingleValues'>\r\n" +
                "			        <tr>\r\n";

            var counter = 1;

            foreach (var value in _mfuTest.TestValues)
            {
                var testValue = value.ControlledByValue;

                var positionStrCol = "<td class='singleValueOk'> " + (value.Position + 1) + ".</td>";
                var testValueStrCol = "<td class='singleValueOk'>" + testValue.ToString(doubleformat, _culture) + "</td>";
                if (testValue > _mfuUpperTolerance || testValue < _mfuLowerTolerance)
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

    public static class ListUtils
    {
        public static List<List<T>> SplitList<T>(List<T> list, int splitSize)
        {
            if (list == null)
            {
                throw new ArgumentException("");
            }
            var returnList = new List<List<T>>();
            var index = 0;
            while (index < list.Count)
            {
                var count = list.Count - index > splitSize ? splitSize : list.Count - index;
                returnList.Add(list.GetRange(index, count));
                index += splitSize;
            }
            return returnList;
        }
    }
}