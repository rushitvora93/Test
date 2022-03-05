using System;
using Core.Entities;
using Core.Entities.ReferenceLink;
using FrameworksAndDrivers.Gui.Wpf.Formatter;
using NUnit.Framework;
using TestHelper.Factories;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Formatter
{
    public class DefaultFormatterTest
    {
        [TestCase("Number", "Description", "Number - Description")]
        [TestCase("asdbsabd", "rejrets", "asdbsabd - rejrets")]
        public void FormatWithLocationReturnsFormattedString(string number, string description, string expectedResult)
        {
            var location = CreateLocation.Anonymous();
            location.Number = new LocationNumber(number);
            location.Description = new LocationDescription(description);

            var defaultFormatter = new DefaultFormatter();
            var formattedString = defaultFormatter.Format(location);
            Assert.AreEqual(expectedResult, formattedString);
        }

        [Test]
        public void FormatWithNullLocationThrowsArgumentNullException()
        {
            Location location = null;
            var defaultFormatter = new DefaultFormatter();
            Assert.Throws<ArgumentNullException>(() => defaultFormatter.Format(location));
        }

        [TestCase("userid", "name", "name - userid")]
        [TestCase("kasdfkl", "vejalfjea", "vejalfjea - kasdfkl")]
        public void FormatWithLocationReferenceLinkReturnsFormattedString(string number, string description, string expectedResult)
        {
            var defaultFormatter = new DefaultFormatter();
            var locationReferenceLink = new LocationReferenceLink(new QstIdentifier(15), new LocationNumber(number), new LocationDescription(description), new DefaultFormatter());
            Assert.AreEqual(expectedResult, defaultFormatter.Format(locationReferenceLink));
        }

        [Test]
        public void FormatWithNullLocationReferenceLinkThrowsArgumentNullException()
        {
            var defaultFormatter = new DefaultFormatter();
            LocationReferenceLink locationReferenceLink = null;
            Assert.Throws<ArgumentNullException>(() => defaultFormatter.Format(locationReferenceLink));
        }

        [TestCase("SerialNumber", "InventoryNumber", "SerialNumber - InventoryNumber")]
        [TestCase("456", "98765", "456 - 98765")]
        public void FormatWithToolReturnsFormattedString(string serialNumber, string inventoryNumber, string expectedResult)
        {
            var defaultFormatter = new DefaultFormatter();
            var tool = CreateTool.Anonymous();
            tool.InventoryNumber = new ToolInventoryNumber(inventoryNumber);
            tool.SerialNumber = new ToolSerialNumber(serialNumber);
            Assert.AreEqual(expectedResult, defaultFormatter.Format(tool));
        }

        [Test]
        public void FormatWithNullToolThrowsArgumentNullException()
        {
            var defaultFormatter = new DefaultFormatter();
            Tool tool = null;
            Assert.Throws<ArgumentNullException>(() => defaultFormatter.Format(tool));
        }

        [TestCase("SerialNumber", "InventoryNumber", "SerialNumber - InventoryNumber")]
        [TestCase("456", "98765", "456 - 98765")]
        public void FormatWithToolReferenceLinkReturnsFormattedString(string serialNumber, string inventoryNumber, string expectedResult)
        {
            var defaultFormatter = new DefaultFormatter();
            var toolReferenceLink = new ToolReferenceLink(new QstIdentifier(15), inventoryNumber, serialNumber, defaultFormatter );
            Assert.AreEqual(expectedResult, defaultFormatter.Format(toolReferenceLink));
        }

        [Test]
        public void FormatWithNullToolReferenceLinkThrowsArgumentNullException()
        {
            var defaultFormatter = new DefaultFormatter();
            ToolReferenceLink toolReferenceLink = null;
            Assert.Throws<ArgumentNullException>(() => defaultFormatter.Format(toolReferenceLink));
        }

        [TestCase("LocationName", "LocationNumber", "ToolSerialNumber", "ToolInventoryNumber", "LocationNumber - LocationName / ToolSerialNumber - ToolInventoryNumber")]
        [TestCase("adfsd", "rtjtzj", "asdfdsf", "jztkuk", "rtjtzj - adfsd / asdfdsf - jztkuk")]
        public void FormatWithLocationToolAssignmentReturnsFormattedString(string locationName, string locationNumber, string toolSerialfNumber, string toolInventoryNumber, string expectedResult)
        {
            var defaultFormatter = new DefaultFormatter();
            var locationToolAssignment = new LocationToolAssignment();
            var location = CreateLocation.Anonymous();
            location.Number = new LocationNumber(locationNumber); 
            location.Description = new LocationDescription(locationName);
            locationToolAssignment.AssignedLocation = location;
            var tool = CreateTool.Anonymous();
            tool.InventoryNumber = new ToolInventoryNumber(toolInventoryNumber);
            tool.SerialNumber = new ToolSerialNumber(toolSerialfNumber);
            locationToolAssignment.AssignedTool = tool;
            Assert.AreEqual(expectedResult, defaultFormatter.Format(locationToolAssignment));
        }

        [Test]
        public void FormatWithNullLocationToolAssignmentThrowsArgumentNullException()
        {
            var defaultFormatter = new DefaultFormatter();
            LocationToolAssignment locationToolAssignment = null;
            Assert.Throws<ArgumentNullException>(() => defaultFormatter.Format(locationToolAssignment));
        }

        [TestCase("LocationName", "LocationNumber", "ToolSerialNumber", "ToolInventoryNumber", "LocationNumber - LocationName / ToolSerialNumber - ToolInventoryNumber")]
        [TestCase("adfsd", "rtjtzj", "asdfdsf", "jztkuk", "rtjtzj - adfsd / asdfdsf - jztkuk")]
        public void FormatWithLocationToolAssignmentReferenceLinkReturnsFormattedString(string locationName, string locationNumber, string toolSerialfNumber, string toolInventoryNumber, string expectedResult)
        {
            var defaultFormatter = new DefaultFormatter();
            var irrelevantId = 15;
            var locationToolAssignmentReferenceLink =  new LocationToolAssignmentReferenceLink(
                new QstIdentifier(irrelevantId),
                new LocationDescription(locationName),
                new LocationNumber(locationNumber),
                toolSerialfNumber,
                toolInventoryNumber,
                defaultFormatter,
                new LocationId(irrelevantId),
                new ToolId(irrelevantId));
            Assert.AreEqual(expectedResult, defaultFormatter.Format(locationToolAssignmentReferenceLink));
        }

        [Test]
        public void FormatWithNullLocationToolAssignmentReferenceLinkThrowsArgumentNullException()
        {
            var defaultFormatter = new DefaultFormatter();
            LocationToolAssignmentReferenceLink referenceLink = null;
            Assert.Throws<ArgumentNullException>(() => defaultFormatter.Format(referenceLink));
        }
    }
}