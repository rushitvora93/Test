using System;
using System.Collections.Generic;
using System.Linq;
using Client.Core.Entities;
using Client.TestHelper.Factories;
using Core.Entities;
using Core.Entities.ToolTypes;
using Core.Enums;
using Core.PhysicalValueTypes;
using Core.UseCases.Communication;
using Core.UseCases.Communication.DataGate;
using NUnit.Framework;
using TestHelper.Factories;
using TestHelper.Mock;

namespace Core.Test.UseCases.Communication
{
    class DataGateSemanticModelFactoryTest
    {
        [TestCase(ConversionType.Rotating)]
        [TestCase(ConversionType.Process)]
        public void ConvertingToSemanticModelHasDataGateContainer(ConversionType conversionType)
        {
            AssertHasDataGateRootNode(
                CallConvert(
                    CreateSemanticModelFactory(),
                    null,
                    _noCmCmk,
                    _noTime,
                    new EmptyConvertDataLoader(),
                    conversionType));
        }

        [TestCase(ConversionType.Rotating)]
        [TestCase(ConversionType.Process)]
        public void ConvertingToSemanticModelHasAllTagsInDataGateContainer(ConversionType conversionType)
        {
            ;
            AssertItemsInDataGateRootContainer(
                CallConvert(
                    CreateSemanticModelFactory(),
                    null,
                    _noCmCmk,
                    _noTime,
                    new EmptyConvertDataLoader(),
                    conversionType),
                new List<string>
                {
                    "Header",
                    "Communication",
                    "RouteList"
                });
        }

        [TestCase(ConversionType.Rotating)]
        [TestCase(ConversionType.Process)]
        public void ConvertingToSemanticModelHasAllTheTagsInHeader(ConversionType conversionType)
        {
            AssertItemsInHeaderContainer(
                CallConvert(
                    CreateSemanticModelFactory(),
                    null,
                    _noCmCmk,
                    _noTime,
                    new EmptyConvertDataLoader(),
                    conversionType),
                new List<string>
                {
                    "dgVersion",
                    "qstVersion",
                    "qstLevel",
                    "source",
                    "destination",
                    "fileDate",
                    "fileTime",
                    "DevSerNo",
                    "DevInvNo",
                    // "Handle",
                    "Instruction",
                    "ResultFile",
                    "StatusFile",
                    "UseLogOn",
                    "UseErrCode",
                    "PCRule1",
                    "PCRule2",
                    "PCRule3",
                    "PCRule4",
                    "PCRule5",
                    "PCRule6",
                    "PCRule7",
                    "CustomerName",
                    "MinCp",
                    "MinCpk",
                    "AskForIdent",
                    "QuitTest",
                    "AllowDeleteTest",
                    "LooseCheck",
                    "UseFileId"
                });
        }

        [TestCase("serno", ConversionType.Rotating)]
        [TestCase("um348275892", ConversionType.Rotating)]
        [TestCase("serno", ConversionType.Process)]
        [TestCase("um348275892", ConversionType.Process)]
        public void ConvertingToSemanticModelSetsDestinationAndDevSerNoToTestEquipmentSerialNumber(
            string serialNumber,
            ConversionType conversionType)
        {
            AssertTagContentAreEqual(
                serialNumber,
                new List<string>
                {
                    "destination",
                    "DevSerNo"
                },
                CallConvert(
                    CreateSemanticModelFactory(),
                    CreateTestEquipment.WithSerialNumber(serialNumber),
                    _noCmCmk,
                    _noTime,
                    new EmptyConvertDataLoader(),
                    conversionType),
                result => (result as Content).GetValue());
        }

        [TestCase("gjakklvmwerap", ConversionType.Rotating)]
        [TestCase("Schnitzel mit Pommes", ConversionType.Rotating)]
        [TestCase("gjakklvmwerap", ConversionType.Process)]
        [TestCase("Schnitzel mit Pommes", ConversionType.Process)]
        public void ConvertingToSemanticModelSetsStatusFilePathFromTestEquipmentFilePath(
            string statusFilePath,
            ConversionType conversionType)
        {
            AssertTagContentAreEqual(
                statusFilePath,
                new List<string> { "StatusFile" },
                CallConvert(
                    CreateSemanticModelFactory(),
                    CreateTestEquipment.WithModelStatusPath(statusFilePath),
                    _noCmCmk,
                    _noTime,
                    new EmptyConvertDataLoader(),
                    conversionType),
                result => (result as Content).GetValue());
        }

        [TestCase("jakoiwemowwg", ConversionType.Rotating)]
        [TestCase("Pizza Pizza Pizza Pizza", ConversionType.Rotating)]
        [TestCase("jakoiwemowwg", ConversionType.Process)]
        [TestCase("Pizza Pizza Pizza Pizza", ConversionType.Process)]
        public void ConvertingToSemanticModelSetsResultFilePathFromTestEquipmentFilePath(
            string resultFilePath,
            ConversionType conversionType)
        {
            AssertTagContentAreEqual(
                resultFilePath,
                new List<string> { "ResultFile" },
                CallConvert(
                    CreateSemanticModelFactory(),
                    CreateTestEquipment.WithModelResultPath(resultFilePath),
                    _noCmCmk,
                    _noTime,
                    new EmptyConvertDataLoader(),
                    conversionType),
                result => (result as Content).GetValue());
        }

        [TestCase(5.0, "5.00", ConversionType.Rotating)]
        [TestCase(2.87, "2.87", ConversionType.Rotating)]
        [TestCase(5.0, "0.00", ConversionType.Process)] // process has no min cp
        [TestCase(2.87, "0.00", ConversionType.Process)] // process has no min cp
        public void ConvertingToSemanticModelSetsMinCpByParameter(
            double cp,
            string expectedContent,
            ConversionType conversionType)
        {
            AssertTagContentAreEqual(
                expectedContent,
                new List<string> { "MinCp" },
                CallConvert(
                    CreateSemanticModelFactory(),
                    null,
                    (cp, 0.0),
                    _noTime,
                    new EmptyConvertDataLoader(),
                    conversionType),
                result => (result as Content).GetValue());
        }

        [TestCase(5.0, "5.00", ConversionType.Rotating)]
        [TestCase(2.87, "2.87", ConversionType.Rotating)]
        [TestCase(5.0, "0.00", ConversionType.Process)] // process has no min cpk
        [TestCase(2.87, "0.00", ConversionType.Process)] // process has no min cpk
        public void ConvertingToSemanticModelSetsMinCpkByParameter(
            double cpk,
            string expectedContent,
            ConversionType conversionType)
        {
            AssertTagContentAreEqual(
                expectedContent,
                new List<string> { "MinCpk" },
                CallConvert(
                    CreateSemanticModelFactory(),
                    null,
                    (0.0, cpk),
                    _noTime,
                    new EmptyConvertDataLoader(),
                    conversionType),
                result => (result as Content).GetValue());
        }

        [TestCase(2007, 9, 18, 0, 43, 58, "2007-09-18", "00:43:58", ConversionType.Rotating)]
        [TestCase(2009, 6, 30, 2, 37, 45, "2009-06-30", "02:37:45", ConversionType.Rotating)]
        [TestCase(2007, 9, 18, 0, 43, 58, "2007-09-18", "00:43:58", ConversionType.Process)]
        [TestCase(2009, 6, 30, 2, 37, 45, "2009-06-30", "02:37:45", ConversionType.Process)]
        public void ConvertingToSemanticModelSetsFileDateAndTime(
            int year,
            int month,
            int day,
            int hour,
            int minute,
            int second,
            string expectedDate,
            string expectedTime,
            ConversionType conversionType)
        {
            var semanticModel =
                CallConvert(
                    CreateSemanticModelFactory(),
                    null,
                    _noCmCmk,
                    new DateTime(year, month, day, hour, minute, second),
                    new EmptyConvertDataLoader(),
                    conversionType);
            Func<IElement, string> getResultValue = result => (result as Content).GetValue();
            AssertTagContentAreEqual(expectedDate, new List<string> { "fileDate" }, semanticModel, getResultValue);
            AssertTagContentAreEqual(expectedTime, new List<string> { "fileTime" }, semanticModel, getResultValue);
        }

        [TestCase(ConversionType.Rotating)]
        [TestCase(ConversionType.Process)]
        public void ConvertingToSemanticModelHasOneRoute(ConversionType conversionType)
        {
            var semanticModel =
                CallConvert(
                    CreateSemanticModelFactory(),
                    null,
                    _noCmCmk,
                    _noTime,
                    new EmptyConvertDataLoader(),
                    conversionType);
            var routeListFinder = new FindFirstByName(new ElementName("RouteList"));
            semanticModel.Accept(routeListFinder);
            var routeList = routeListFinder.Result as Container;
            var routeElementCounter = routeList.Count(element => element.GetName().ToDefaultString() == "Route");
            var routeCountElement = (routeList.First(element => element.GetName().ToDefaultString() == "RouteCount") as Content);
            Assert.AreEqual(1, routeElementCounter);
            Assert.AreEqual("1", routeCountElement.GetValue());
        }

        [TestCase(ConversionType.Rotating)]
        [TestCase(ConversionType.Process)]
        public void ConvertingToSemanticModelHasAllTheTagsInRoute(ConversionType conversionType)
        {
            var semanticModel =
                CallConvert(
                    CreateSemanticModelFactory(),
                    null,
                    _noCmCmk,
                    _noTime,
                    new EmptyConvertDataLoader(),
                    conversionType);
            var route = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(route);
            var expectedTags =
                new List<string>
                {
                    "RoutePos",
                    "RouteId",
                    "RouteName",
                    "AskForIdent",
                    "AskForCurve",
                    "TestCount"
                };
            CollectionAssert.AreEqual(
                expectedTags,
                (route.Result as Container)
                    .Take(6)
                    .Select(element => element.GetName().ToDefaultString())
                    .ToList());
        }

        [TestCase(1, "1", ConversionType.Rotating)]
        [TestCase(2, "2", ConversionType.Rotating)]
        [TestCase(5, "5", ConversionType.Rotating)]
        [TestCase(1, "1", ConversionType.Process)]
        [TestCase(2, "2", ConversionType.Process)]
        [TestCase(5, "5", ConversionType.Process)]
        public void ConvertingToSemanticModelCreatesCorrectAmountOfTestItems(
            int testItemCount,
            string expectedTestCountContent,
            ConversionType conversionType)
        {
            var semanticModel =
                CallConvert(
                    CreateSemanticModelFactory(),
                    null,
                    _noCmCmk,
                    _noTime,
                    new NAnonymousConvertDataLoader(testItemCount),
                    conversionType);
            var testItemCountFinder = new FindFirstByName(new ElementName("TestCount"));
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(testItemCountFinder);
            semanticModel.Accept(routeContainerFinder);
            Assert.AreEqual(expectedTestCountContent, (testItemCountFinder.Result as Content).GetValue());
            Assert.AreEqual(testItemCount, (routeContainerFinder.Result as Container).Count(item => item.GetName().ToDefaultString() == "TestItem"));
        }

        [TestCase(ConversionType.Rotating)]
        [TestCase(ConversionType.Process)]
        public void ConvertingToSemanticModelIncreasesTestPos(ConversionType conversionType)
        {
            var semanticModel =
                CallConvert(
                    CreateSemanticModelFactory(),
                    null,
                    _noCmCmk,
                    _noTime,
                    new NAnonymousConvertDataLoader(5),
                    conversionType);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            var expectedTestPosList =
                new List<string>
                {
                    "1",
                    "2",
                    "3",
                    "4",
                    "5"
                };
            CollectionAssert.AreEqual(
                expectedTestPosList,
                TestItemContentListFromRoute(routeContainerFinder, "TestPos"));
        }

        [TestCase(5, 6, "5", "6")]
        [TestCase(276, 3697, "276", "3697")]
        public void ConvertingToSemanticModelSetsLocationToolAssignmentIdToTestId1(
            long firstAssignmentId,
            long secondAssignmentId,
            string expectedFirstContent,
            string expectedSecondContent)
        {
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.IdOnly(firstAssignmentId));
            locationToolAssignments.Add(CreateLocationToolAssignment.IdOnly(secondAssignmentId));
            var semanticModel = CreateSemanticModelFactory().Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirstContent, expectedSecondContent },
                TestItemContentListFromRoute(routeContainerFinder, "TestId1"));
        }

        [TestCase(5, 27, "5", "27", "TestId1")]
        [TestCase(87934, 48123, "87934", "48123", "TestId1")]
        [TestCase(5, 27, "5", "27", "TestId3")]
        [TestCase(87934, 48123, "87934", "48123", "TestId3")]
        public void ConvertingToSemanticModelSetsLocationIdToTestIds(
            long firstLocationId,
            long secondLocationId,
            string expectedFirstTestId1,
            string expectedSecondTestId1,
            string targetField)
        {
            var locations = CreateLocationsEmpty();
            locations.Add(CreateLocation.IdOnly(firstLocationId));
            locations.Add(CreateLocation.IdOnly(secondLocationId));
            var processControlConditions = CreateProcessControlConditionsEmpty();
            processControlConditions.Add(CreateProcessControlCondition.WithLocationId(firstLocationId));
            processControlConditions.Add(CreateProcessControlCondition.WithLocationId(secondLocationId));
            var semanticModel =
                CreateSemanticModelFactory()
                    .Convert(null, locations, processControlConditions, _noTime);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirstTestId1, expectedSecondTestId1 },
                TestItemContentListFromRoute(routeContainerFinder, targetField));
        }

        [TestCase(789, 137, "789", "137")]
        [TestCase(23835, 19658, "23835", "19658")]
        public void ConvertingToSemanticModelSetsLocationIdToTestId3(long firstLocationId, long secondLocationId, string expectedFirstContent, string expectedSecondContent)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithLocation(CreateLocation.IdOnly(firstLocationId)));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithLocation(CreateLocation.IdOnly(secondLocationId)));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirstContent, expectedSecondContent },
                TestItemContentListFromRoute(routeContainerFinder, "TestId3"));
        }

        [TestCase("number", "jfkaeovnwio", ConversionType.Rotating)]
        [TestCase("aaethsefgs", "jdgfjsfsbe", ConversionType.Rotating)]
        [TestCase("number", "jfkaeovnwio", ConversionType.Process)]
        [TestCase("aaethsefgs", "jdgfjsfsbe", ConversionType.Process)]
        public void ConvertingToSemanticModelSetsLocationNumberToUserId(
            string firstLocationNumber,
            string secondLocationNumber,
            ConversionType conversionType)
        {
            var semanticModel =
                CallConvert(
                    CreateSemanticModelFactory(),
                    null,
                    _noCmCmk,
                    _noTime,
                    new SingleParameterLocationConvertDataLoader(
                        SingleParameterLocationConvertDataLoader.CreateLocationFactory(CreateLocation.LocationParameter.Number),
                        new List<string>
                        {
                            firstLocationNumber,
                            secondLocationNumber
                        }),
                    conversionType);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { firstLocationNumber, secondLocationNumber },
                TestItemContentListFromRoute(routeContainerFinder, "UserId"));
        }

        [TestCase("number", "jfkaeovnwio", ConversionType.Rotating)]
        [TestCase("aaethsefgs", "jdgfjsfsbe", ConversionType.Rotating)]
        [TestCase("number", "jfkaeovnwio", ConversionType.Process)]
        [TestCase("aaethsefgs", "jdgfjsfsbe", ConversionType.Process)]
        public void ConvertingToSemanticModelSetsLocationDescriptionToUserName(
            string firstLocationDescription,
            string secondLocationDescription,
            ConversionType conversionType)
        {
            var semanticModel =
                CallConvert(
                    CreateSemanticModelFactory(),
                    null,
                    _noCmCmk,
                    _noTime,
                    new SingleParameterLocationConvertDataLoader(
                        SingleParameterLocationConvertDataLoader.CreateLocationFactory(CreateLocation.LocationParameter.Description),
                        new List<string>
                        {
                            firstLocationDescription,
                            secondLocationDescription
                        }),
                    conversionType);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { firstLocationDescription, secondLocationDescription },
                TestItemContentListFromRoute(routeContainerFinder, "UserName"));
        }

        public class TestParameterTestData
        {
            public ConversionType ConversionType;
            public IDataLoader DataLoader;
            public List<string> ExpectedFieldValues;
            public string ExpectedField;
        }

        private static List<TestParameterTestData> TestParameterTestDataSource =
            new List<TestParameterTestData>
            {
                new TestParameterTestData
                {
                    ConversionType = ConversionType.Rotating,
                    DataLoader =
                        new SingleParameterTestConditionConvertDataLoader(
                            SingleParameterTestConditionConvertDataLoader.CreateToolAssignmentFactory(
                                CreateTestParameters.Parameter.ControlledBy),
                            new List<string>
                            {
                                "0", // Angle
                                "1" // Torque
                            }),
                    ExpectedField = "ControlDimension",
                    ExpectedFieldValues = new List<string>
                    {
                        "0",
                        "1"
                    }
                }
            };

        [TestCaseSource(nameof(TestParameterTestDataSource))]
        public void ConvertingTestParametersTest(TestParameterTestData testData)
        {
            var semanticModel =
                CallConvert(
                    CreateSemanticModelFactory(),
                    null,
                    _noCmCmk,
                    _noTime,
                    testData.DataLoader,
                    testData.ConversionType);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                testData.ExpectedFieldValues,
                TestItemHiddenContentListFromRoute(routeContainerFinder, testData.ExpectedField));
        }

        //remove stuff below for test data and merge into test above

        [Test]
        public void ConvertingToSemanticModelSetsHiddenControlDimension()
        {
            var semanticModel =
                CallConvert(
                    CreateSemanticModelFactory(),
                    null,
                    _noCmCmk,
                    _noTime,
                    new SingleParameterTestConditionConvertDataLoader(
                        SingleParameterTestConditionConvertDataLoader.CreateToolAssignmentFactory(
                            CreateTestParameters.Parameter.ControlledBy),
                        new List<string>
                        {
                            "0", // Angle
                            "1", // Torque
                        }),
                    ConversionType.Rotating);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string>{"0", "1"},
                TestItemHiddenContentListFromRoute(routeContainerFinder, "ControlDimension"));
        }

        [TestCase(2.5, 8.67, "2.500", "8.670")]
        [TestCase(3.5, 13.567, "3.500", "13.567")]
        public void ConvertingToSemanticModelSetsHiddenDimensionTorqueNominalToTestParametersSetPointTorque(double firstSetPoint, double secondSetPoint, string expectedFirst, string expectedSecond)
        {
            var semanticModel =
                CallConvert(
                    CreateSemanticModelFactory(),
                    null,
                    _noCmCmk,
                    _noTime,
                    new SingleParameterTestConditionConvertDataLoader(
                        SingleParameterTestConditionConvertDataLoader.CreateToolAssignmentFactory(
                            CreateTestParameters.Parameter.SetPointTorque),
                        new List<string>
                        {
                            firstSetPoint.ToString(),
                            secondSetPoint.ToString()
                        }),
                    ConversionType.Rotating);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemHiddenContentListFromRoute(routeContainerFinder, "DimensionTorqueNominal"));
        }

        [TestCase(2.5, 8.67, "2.500", "8.670")]
        [TestCase(3.5, 13.567, "3.500", "13.567")]
        public void ConvertingToSemanticModelSetsHiddenDimensionTorqueMinToTestParametersMinimumTorque(double firstSetPoint, double secondSetPoint, string expectedFirst, string expectedSecond)
        {
            var semanticModel =
                CallConvert(
                    CreateSemanticModelFactory(),
                    null,
                    _noCmCmk,
                    _noTime,
                    new SingleParameterTestConditionConvertDataLoader(
                        SingleParameterTestConditionConvertDataLoader.CreateToolAssignmentFactory(
                            CreateTestParameters.Parameter.MinimumTorque),
                        new List<string>
                        {
                            firstSetPoint.ToString(),
                            secondSetPoint.ToString()
                        }),
                    ConversionType.Rotating);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemHiddenContentListFromRoute(routeContainerFinder, "DimensionTorqueMin"));
        }

        [TestCase(2.5, 8.67, "2.500", "8.670")]
        [TestCase(3.5, 13.567, "3.500", "13.567")]
        public void ConvertingToSemanticModelSetsHiddenDimensionTorqueMaxToTestParametersSetPointTorque(double firstSetPoint, double secondSetPoint, string expectedFirst, string expectedSecond)
        {
            var semanticModel =
                CallConvert(
                    CreateSemanticModelFactory(),
                    null,
                    _noCmCmk,
                    _noTime,
                    new SingleParameterTestConditionConvertDataLoader(
                        SingleParameterTestConditionConvertDataLoader.CreateToolAssignmentFactory(
                            CreateTestParameters.Parameter.MaximumTorque),
                        new List<string>
                        {
                            firstSetPoint.ToString(),
                            secondSetPoint.ToString()
                        }),
                    ConversionType.Rotating);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemHiddenContentListFromRoute(routeContainerFinder, "DimensionTorqueMax"));
        }

        [TestCase(2.5, 8.67, "2.500", "8.670")]
        [TestCase(3.5, 13.567, "3.500", "13.567")]
        public void ConvertingToSemanticModelSetsHiddenDimensionAngleNominalToTestParametersSetPointAngle(double firstSetPoint, double secondSetPoint, string expectedFirst, string expectedSecond)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestParameters(new Core.Entities.TestParameters { SetPointAngle = Angle.FromDegree(firstSetPoint) }));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestParameters(new Core.Entities.TestParameters { SetPointAngle = Angle.FromDegree(secondSetPoint) }));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemHiddenContentListFromRoute(routeContainerFinder, "DimensionAngleNominal"));
        }

        [TestCase(2.5, 8.67, "2.500", "8.670")]
        [TestCase(3.5, 13.567, "3.500", "13.567")]
        public void ConvertingToSemanticModelSetsHiddenDimensionAngleMinToTestParametersSetPointAngle(double firstSetPoint, double secondSetPoint, string expectedFirst, string expectedSecond)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestParameters(new Core.Entities.TestParameters { MinimumAngle = Angle.FromDegree(firstSetPoint) }));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestParameters(new Core.Entities.TestParameters { MinimumAngle = Angle.FromDegree(secondSetPoint) }));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemHiddenContentListFromRoute(routeContainerFinder, "DimensionAngleMin"));
        }

        [TestCase(2.5, 8.67, "2.500", "8.670")]
        [TestCase(3.5, 13.567, "3.500", "13.567")]
        public void ConvertingToSemanticModelSetsHiddenDimensionAngleMaxToTestParametersSetPointAngle(double firstSetPoint, double secondSetPoint, string expectedFirst, string expectedSecond)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestParameters(new Core.Entities.TestParameters { MaximumAngle = Angle.FromDegree(firstSetPoint) }));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestParameters(new Core.Entities.TestParameters { MaximumAngle = Angle.FromDegree(secondSetPoint) }));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemHiddenContentListFromRoute(routeContainerFinder, "DimensionAngleMax"));
        }

        private static IEnumerable<(List<(TestLevelSet, int)>, List<(TestLevelSet, int)>, TestType, List<string>)> TestCyclesToRulesSampleCountDataSource =
            new List<(List<(TestLevelSet, int)>, List<(TestLevelSet, int)>, TestType, List<string>)>
            {
                (
                    new List<(TestLevelSet, int)>
                    {
                        (new TestLevelSet() { TestLevel1 = new TestLevel() { SampleNumber = 1, IsActive = true}}, 1),
                        (new TestLevelSet() { TestLevel1 = new TestLevel() { SampleNumber = 2, IsActive = true}}, 1)
                    },
                    new List<(TestLevelSet, int)>
                    {
                        (new TestLevelSet() { TestLevel1 = new TestLevel() { SampleNumber = 8, IsActive = true}}, 1),
                        (new TestLevelSet() { TestLevel3 = new TestLevel() { SampleNumber = 9, IsActive = true}}, 3)
                    },
                    TestType.Chk,
                    new List<string>{ "1", "2"}
                ),
                (
                    new List<(TestLevelSet, int)>
                    {
                        (new TestLevelSet() { TestLevel2 = new TestLevel() { SampleNumber = 7, IsActive = true}}, 2),
                        (new TestLevelSet() { TestLevel2 = new TestLevel() { SampleNumber = 5, IsActive = true}}, 2)
                    },
                    new List<(TestLevelSet, int)>
                    {
                        (new TestLevelSet() { TestLevel3 = new TestLevel() { SampleNumber = 3, IsActive = true}}, 3),
                        (new TestLevelSet() { TestLevel3 = new TestLevel() { SampleNumber = 1, IsActive = true}}, 3)
                    },
                    TestType.Chk,
                    new List<string>{ "7", "5"}
                ),
                (
                    new List<(TestLevelSet, int)>
                    {
                        (new TestLevelSet() { TestLevel2 = new TestLevel() { SampleNumber = 12, IsActive = true}}, 2),
                        (new TestLevelSet() { TestLevel3 = new TestLevel() { SampleNumber = 3, IsActive = true}}, 3)
                    },
                    new List<(TestLevelSet, int)>
                    {
                        (new TestLevelSet() { TestLevel3 = new TestLevel() { SampleNumber = 1, IsActive = true}}, 3),
                        (new TestLevelSet() { TestLevel1 = new TestLevel() { SampleNumber = 10, IsActive = true}}, 1)
                    },
                    TestType.Mfu,
                    new List<string>{ "1", "10"}
                ),
                (
                    new List<(TestLevelSet, int)>
                    {
                        (new TestLevelSet() { TestLevel3 = new TestLevel() { SampleNumber = 6, IsActive = true}}, 3),
                        (new TestLevelSet() { TestLevel3 = new TestLevel() { SampleNumber = 7, IsActive = true}}, 3)
                    },
                    new List<(TestLevelSet, int)>
                    {
                        (new TestLevelSet() { TestLevel1 = new TestLevel() { SampleNumber = 3, IsActive = true}}, 1),
                        (new TestLevelSet() { TestLevel2 = new TestLevel() { SampleNumber = 4, IsActive = true}}, 2)
                    },
                    TestType.Mfu,
                    new List<string>{ "3", "4"}
                )
            };

        [TestCaseSource(nameof(TestCyclesToRulesSampleCountDataSource))]
        public void ConvertingToSemanticModelSetsTestCyclesToTestRulesSamplingNumberCorrect((List<(TestLevelSet, int)> monitoringTestLevelSets, List<(TestLevelSet, int)> mcaTestLevelSets, TestType testType, List<string> expectedValue) testData)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestLevelSets(testData.mcaTestLevelSets[0].Item1, testData.monitoringTestLevelSets[0].Item1, testData.mcaTestLevelSets[0].Item2, testData.monitoringTestLevelSets[0].Item2));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestLevelSets(testData.mcaTestLevelSets[1].Item1, testData.monitoringTestLevelSets[1].Item1, testData.mcaTestLevelSets[1].Item2, testData.monitoringTestLevelSets[1].Item2));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), testData.testType);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { testData.expectedValue[0], testData.expectedValue[1] },
                TestItemContentListFromRoute(routeContainerFinder, "TestCycles"));
        }

        [TestCase(5, 8, "5", "8")]
        [TestCase(3, 7, "3", "7")]
        public void ConvertingToSemanticModelSetsTestCyclesCmToMcaTestRulesSamplingNumber(int firstSampleCount, int secondSampleCount, string expectedFirst, string expectedSecond)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithMcaTestLevelSets(new TestLevelSet() { TestLevel1 = new TestLevel() { SampleNumber = firstSampleCount }}));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithMcaTestLevelSets(new TestLevelSet() { TestLevel2 = new TestLevel() { SampleNumber = secondSampleCount, IsActive = true }}, 2));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemContentListFromRoute(routeContainerFinder, "TestCyclesCm"));
        }

        [Test]
        public void ConvertingToSemanticModelSetsCorrectTestMethod()
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTool(CreateTool.WithModel(new ToolModel { ModelType = new ClickWrench(), Manufacturer = CreateManufacturer.Anonymous()} )));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTool(CreateTool.WithModel(new ToolModel { ModelType = new ECDriver(), Manufacturer = CreateManufacturer.Anonymous() })));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTool(CreateTool.WithModel(new ToolModel { ModelType = new General(), Manufacturer = CreateManufacturer.Anonymous() })));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTool(CreateTool.WithModel(new ToolModel { ModelType = new MDWrench(), Manufacturer = CreateManufacturer.Anonymous() })));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTool(CreateTool.WithModel(new ToolModel { ModelType = new ProductionWrench(), Manufacturer = CreateManufacturer.Anonymous() })));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTool(CreateTool.WithModel(new ToolModel { ModelType = new PulseDriverShutOff(), Manufacturer = CreateManufacturer.Anonymous() })));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTool(CreateTool.WithModel(new ToolModel { ModelType = new PulseDriver(), Manufacturer = CreateManufacturer.Anonymous() })));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(new List<string>{ "18", "19", "19", "13", "13", "14", "14" }, TestItemContentListFromRoute(routeContainerFinder, "TestMethod"));
        }

        [TestCase(7, 9, "7", "9")]
        [TestCase(383, 742, "383", "742")]
        public void ConvertingToSemanticModelSetsToolIdToToolId(long firstToolId, long secondToolId, string expectedFirst, string expectedSecond)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTool(CreateTool.WithId(firstToolId)));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTool(CreateTool.WithId(secondToolId)));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemContentListFromRoute(routeContainerFinder, "ToolId"));
        }

        [TestCase("afdsfae", "ferjdhgjd")]
        [TestCase("kvioersklsd", "jrosekfsl")]
        public void ConvertingToSemanticModelSetsToolNameToToolInventoryNumber(string firstToolName, string secondToolName)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTool(CreateTool.WithInventoryNumber(firstToolName)));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTool(CreateTool.WithInventoryNumber(secondToolName)));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { firstToolName, secondToolName },
                TestItemContentListFromRoute(routeContainerFinder, "ToolName"));
        }

        [TestCase("afdsfae", "ferjdhgjd")]
        [TestCase("kvioersklsd", "jrosekfsl")]
        public void ConvertingToSemanticModelSetsModelNameToToolModelDescription(string firstModelName, string secondModelName)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTool(CreateTool.WithModel(new ToolModel { Description = new ToolModelDescription(firstModelName), ModelType = new General(), Manufacturer = CreateManufacturer.Anonymous()} )));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTool(CreateTool.WithModel(new ToolModel { Description = new ToolModelDescription(secondModelName), ModelType = new General(), Manufacturer = CreateManufacturer.Anonymous()})));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { firstModelName, secondModelName },
                TestItemContentListFromRoute(routeContainerFinder, "ModelName"));
        }

        [TestCase("afdsfae", "ferjdhgjd")]
        [TestCase("kvioersklsd", "jrosekfsl")]
        public void ConvertingToSemanticModelSetsSupplierNameToToolModelManufacturername(string firstManufacturerName, string secondManufacturerName)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTool(CreateTool.WithModel(new ToolModel { Manufacturer = CreateManufacturer.WithName(firstManufacturerName), ModelType = new General() })));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTool(CreateTool.WithModel(new ToolModel { Manufacturer = CreateManufacturer.WithName(secondManufacturerName), ModelType = new General() })));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { firstManufacturerName, secondManufacturerName },
                TestItemContentListFromRoute(routeContainerFinder, "SupplierName"));
        }

        [TestCase(7.35, 9.8, "7.35", "9.80")]
        [TestCase(383.79, 742, "383.79", "742.00")]
        public void ConvertingToSemanticModelSetsStartMeaToCycleStart(double firstCycleStart, double secondCycleStart, string expectedFirst, string expectedSecond)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique {CycleStart = firstCycleStart }));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { CycleStart = secondCycleStart }));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemContentListFromRoute(routeContainerFinder, "StartMea"));
        }

        [TestCase(7.35, 9.8, "7.35", "9.80")]
        [TestCase(383.79, 742, "383.79", "742.00")]
        public void ConvertingToSemanticModelSetsStartDegToStartFinalAngle(double firstStartFinalAngle, double secondStartFinalAngle, string expectedFirst, string expectedSecond)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { StartFinalAngle = firstStartFinalAngle }));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { StartFinalAngle = secondStartFinalAngle }));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemContentListFromRoute(routeContainerFinder, "StartDeg"));
        }

        [TestCase(7.35, 9.8, "7.35", "9.80")]
        [TestCase(383.79, 742, "383.79", "742.00")]
        public void ConvertingToSemanticModelSetsAC_CycleCompleteToCycleComplete(double firstCycleComplete, double secondCycleComplete, string expectedFirst, string expectedSecond)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { CycleComplete = firstCycleComplete }));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { CycleComplete = secondCycleComplete }));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemContentListFromRoute(routeContainerFinder, "AC_CycleComplete"));
        }

        [TestCase(7.35, 9.8, "7.35", "9.80")]
        [TestCase(383.79, 742, "383.79", "742.00")]
        public void ConvertingToSemanticModelSetsAC_SlipTorqueToSlipTorque(double firstSlipTorque, double secondSlipTorque, string expectedFirst, string expectedSecond)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { SlipTorque = firstSlipTorque }));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { SlipTorque = secondSlipTorque }));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemContentListFromRoute(routeContainerFinder, "AC_SlipTorque"));
        }

        [TestCase(7.35, 9.8, "7.35", "9.80")]
        [TestCase(383.79, 742, "383.79", "742.00")]
        public void ConvertingToSemanticModelSetsAC_TorqueCoefficientToTorqueCoefficient(double firstTorqueCoefficient, double secondTorqueCoefficient, string expectedFirst, string expectedSecond)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { TorqueCoefficient = firstTorqueCoefficient }));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { TorqueCoefficient = secondTorqueCoefficient }));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemContentListFromRoute(routeContainerFinder, "AC_TorqueCoefficient"));
        }

        [TestCase(7.35, 9.8, "7.35", "9.80")]
        [TestCase(383.79, 742, "383.79", "742.00")]
        public void ConvertingToSemanticModelSetsAC_EndTimeToEndCycleTime(double firstEndCycleTime, double secondEndCycleTime, string expectedFirst, string expectedSecond)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { EndCycleTime = firstEndCycleTime }));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { EndCycleTime = secondEndCycleTime }));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemContentListFromRoute(routeContainerFinder, "AC_EndTime"));
        }

        [TestCase(7.35, 9.8, "7.35", "9.80")]
        [TestCase(383.79, 742, "383.79", "742.00")]
        public void ConvertingToSemanticModelSetsAC_MeasureDelayTimeToMeasureDelayTime(double firstMeasureDelayTime, double secondMeasureDelayTime, string expectedFirst, string expectedSecond)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { MeasureDelayTime = firstMeasureDelayTime }));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { MeasureDelayTime = secondMeasureDelayTime }));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemContentListFromRoute(routeContainerFinder, "AC_MeasureDelayTime"));
        }

        [TestCase(7.35, 9.8, "7.35", "9.80")]
        [TestCase(383.79, 742, "383.79", "742.00")]
        public void ConvertingToSemanticModelSetsAC_ResetTimeToResetTime(double firstResetTime, double secondResetTime, string expectedFirst, string expectedSecond)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { ResetTime = firstResetTime }));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { ResetTime = secondResetTime }));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemContentListFromRoute(routeContainerFinder, "AC_ResetTime"));
        }

        [TestCase(7.35, 9.8, "7.35", "9.80")]
        [TestCase(383.79, 742, "383.79", "742.00")]
        public void ConvertingToSemanticModelSetsAC_FilterFrequencyToFilterFrequency(double firstFrequency, double secondFrequency, string expectedFirst, string expectedSecond)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { FilterFrequency = firstFrequency }));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { FilterFrequency = secondFrequency }));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemContentListFromRoute(routeContainerFinder, "AC_FilterFreq"));
        }

        [Test]
        public void ConvertingToSemanticModelSetsAC_MeasureTorqueAtTo0OnTorqueControl()
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestParameters(new Core.Entities.TestParameters { ControlledBy = LocationControlledBy.Torque }));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var measureTorqueAt = new FindFirstByName(new ElementName("AC_MeasureTorqueAt"));
            semanticModel.Accept(measureTorqueAt);
            Assert.AreEqual("0", (measureTorqueAt.Result as Content).GetValue());
        }

        [Test]
        public void ConvertingToSemanticModelSetsAC_MeasureTorqueAtTo1OnAngleControl()
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestParameters(new Core.Entities.TestParameters { ControlledBy = LocationControlledBy.Angle }));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var measureTorqueAt = new FindFirstByName(new ElementName("AC_MeasureTorqueAt"));
            semanticModel.Accept(measureTorqueAt);
            Assert.AreEqual("1", (measureTorqueAt.Result as Content).GetValue());
        }

        [Test]
        public void ConvertingToSemanticModelSetsAC_CmCmkSpcTestTypeTo8WhenOnlyTorqueAndTorqueControlled()
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            var locationToolAssignment = CreateLocationToolAssignment.WithTestParameters(new Core.Entities.TestParameters {ControlledBy = LocationControlledBy.Torque});
            locationToolAssignment.TestTechnique.MustTorqueAndAngleBeInLimits = false;
            locationToolAssignments.Add(locationToolAssignment);
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var spctesttype = new FindFirstByName(new ElementName("AC_CmCmkSpcTestType"));
            semanticModel.Accept(spctesttype);
            Assert.AreEqual("8", (spctesttype.Result as Content).GetValue());
        }

        [Test]
        public void ConvertingToSemanticModelSetsAC_CmCmkSpcTestTypeTo24WhenWithAngleAndTorqueControlled()
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            var locationToolAssignment = CreateLocationToolAssignment.WithTestParameters(new Core.Entities.TestParameters { ControlledBy = LocationControlledBy.Torque });
            locationToolAssignment.TestTechnique.MustTorqueAndAngleBeInLimits = true;
            locationToolAssignments.Add(locationToolAssignment);
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var spctesttype = new FindFirstByName(new ElementName("AC_CmCmkSpcTestType"));
            semanticModel.Accept(spctesttype);
            Assert.AreEqual("24", (spctesttype.Result as Content).GetValue());
        }

        [Test]
        public void ConvertingToSemanticModelSetsAC_CmCmkSpcTestTypeTo16WhenOnlyTorqueAndAngleControlled()
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            var locationToolAssignment = CreateLocationToolAssignment.WithTestParameters(new Core.Entities.TestParameters { ControlledBy = LocationControlledBy.Angle });
            locationToolAssignment.TestTechnique.MustTorqueAndAngleBeInLimits = false;
            locationToolAssignments.Add(locationToolAssignment);
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var spctesttype = new FindFirstByName(new ElementName("AC_CmCmkSpcTestType"));
            semanticModel.Accept(spctesttype);
            Assert.AreEqual("16", (spctesttype.Result as Content).GetValue());
        }

        [Test]
        public void ConvertingToSemanticModelSetsAC_CmCmkSpcTestTypeTo32WhenWithAngleAndAngleControlled()
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            var locationToolAssignment = CreateLocationToolAssignment.WithTestParameters(new Core.Entities.TestParameters { ControlledBy = LocationControlledBy.Angle });
            locationToolAssignment.TestTechnique.MustTorqueAndAngleBeInLimits = true;
            locationToolAssignments.Add(locationToolAssignment);
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var spctesttype = new FindFirstByName(new ElementName("AC_CmCmkSpcTestType"));
            semanticModel.Accept(spctesttype);
            Assert.AreEqual("32", (spctesttype.Result as Content).GetValue());
        }

        [TestCase(1.0, 1.71, "1.000", "1.710")]
        [TestCase(1.81, 1.91, "1.810", "1.910")]
        public void ConvertingToSemanticModelSetsAC_CmCmkFieldsToCmCmk(double cm, double cmk, string expectedCm, string expectedCmk)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.Anonymous());
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (cm, cmk), new DateTime(), TestType.Chk);
            var minimumCmFinder = new FindFirstByName(new ElementName("AC_MinimumCm"));
            var minimumCmkFinder = new FindFirstByName(new ElementName("AC_MinimumCmk"));
            var minimumCmAngleFinder = new FindFirstByName(new ElementName("AC_MinimumCmAngle"));
            var minimumCmkAngleFinder = new FindFirstByName(new ElementName("AC_MinimumCmkAngle"));
            semanticModel.Accept(minimumCmFinder);
            semanticModel.Accept(minimumCmkFinder);
            semanticModel.Accept(minimumCmAngleFinder);
            semanticModel.Accept(minimumCmkAngleFinder);
            Assert.AreEqual(expectedCm, (minimumCmFinder.Result as Content).GetValue());
            Assert.AreEqual(expectedCm, (minimumCmAngleFinder.Result as Content).GetValue());
            Assert.AreEqual(expectedCmk, (minimumCmkFinder.Result as Content).GetValue());
            Assert.AreEqual(expectedCmk, (minimumCmkAngleFinder.Result as Content).GetValue());
        }

        private static IEnumerable<(List<(TestLevelSet, int)>, List<(TestLevelSet, int)>, TestType, List<string>)> AC_SubgroupSizeToRulesSampleCountDataSource =
            new List<(List<(TestLevelSet, int)>, List<(TestLevelSet, int)>, TestType, List<string>)>
            {
                (
                    new List<(TestLevelSet, int)>
                    {
                        (new TestLevelSet() { TestLevel1 = new TestLevel() { SampleNumber = 1, IsActive = true}}, 1),
                        (new TestLevelSet() { TestLevel1 = new TestLevel() { SampleNumber = 2, IsActive = true}}, 1)
                    },
                    new List<(TestLevelSet, int)>
                    {
                        (new TestLevelSet() { TestLevel1 = new TestLevel() { SampleNumber = 8, IsActive = true}}, 1),
                        (new TestLevelSet() { TestLevel3 = new TestLevel() { SampleNumber = 9, IsActive = true}}, 3)
                    },
                    TestType.Chk,
                    new List<string>{ "1", "2"}
                ),
                (
                    new List<(TestLevelSet, int)>
                    {
                        (new TestLevelSet() { TestLevel2 = new TestLevel() { SampleNumber = 7, IsActive = true}}, 2),
                        (new TestLevelSet() { TestLevel2 = new TestLevel() { SampleNumber = 5, IsActive = true}}, 2)
                    },
                    new List<(TestLevelSet, int)>
                    {
                        (new TestLevelSet() { TestLevel3 = new TestLevel() { SampleNumber = 3, IsActive = true}}, 3),
                        (new TestLevelSet() { TestLevel3 = new TestLevel() { SampleNumber = 1, IsActive = true}}, 3)
                    },
                    TestType.Chk,
                    new List<string>{ "7", "5"}
                ),
                (
                    new List<(TestLevelSet, int)>
                    {
                        (new TestLevelSet() { TestLevel2 = new TestLevel() { SampleNumber = 12, IsActive = true}}, 2),
                        (new TestLevelSet() { TestLevel3 = new TestLevel() { SampleNumber = 3, IsActive = true}}, 3)
                    },
                    new List<(TestLevelSet, int)>
                    {
                        (new TestLevelSet() { TestLevel3 = new TestLevel() { SampleNumber = 1, IsActive = true}}, 3),
                        (new TestLevelSet() { TestLevel1 = new TestLevel() { SampleNumber = 10, IsActive = true}}, 1)
                    },
                    TestType.Mfu,
                    new List<string>{ "1", "1"}
                ),
                (
                    new List<(TestLevelSet, int)>
                    {
                        (new TestLevelSet() { TestLevel3 = new TestLevel() { SampleNumber = 6, IsActive = true}}, 3),
                        (new TestLevelSet() { TestLevel3 = new TestLevel() { SampleNumber = 7, IsActive = true}}, 3)
                    },
                    new List<(TestLevelSet, int)>
                    {
                        (new TestLevelSet() { TestLevel1 = new TestLevel() { SampleNumber = 3, IsActive = true}}, 1),
                        (new TestLevelSet() { TestLevel2 = new TestLevel() { SampleNumber = 4, IsActive = true}}, 2)
                    },
                    TestType.Mfu,
                    new List<string>{ "1", "1"}
                )
            };
        [TestCaseSource(nameof(AC_SubgroupSizeToRulesSampleCountDataSource))]
        public void ConvertingToSemanticModelSetsAC_SubgroupSizeToSamplingNumberCorrect((List<(TestLevelSet, int)> monitoringTestLevelSets, List<(TestLevelSet, int)> mcaTestLevelSets, TestType testType, List<string> expectedValue) testData)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestLevelSets(testData.mcaTestLevelSets[0].Item1, testData.monitoringTestLevelSets[0].Item1, testData.mcaTestLevelSets[0].Item2, testData.monitoringTestLevelSets[0].Item2));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestLevelSets(testData.mcaTestLevelSets[1].Item1, testData.monitoringTestLevelSets[1].Item1, testData.mcaTestLevelSets[1].Item2, testData.monitoringTestLevelSets[1].Item2));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), testData.testType);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);

            CollectionAssert.AreEqual(
                new List<string> { testData.expectedValue[0], testData.expectedValue[1] },
                TestItemContentListFromRoute(routeContainerFinder, "AC_SubgroupSize"));
        }

        [TestCase(7, 9, "7", "9")]
        [TestCase(383, 742, "383", "742")]
        public void ConvertingToSemanticModelSetsAC_MinimumPulseToMinimumPulse(int firstPulse, int secondPulse, string expectedFirst, string expectedSecond)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { MinimumPulse = firstPulse }));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { MinimumPulse = secondPulse }));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemContentListFromRoute(routeContainerFinder, "AC_MinimumPulse"));
        }

        [TestCase(7, 9, "7", "9")]
        [TestCase(383, 742, "383", "742")]
        public void ConvertingToSemanticModelSetsAC_MaximumPulseToMaximumPulse(int firstPulse, int secondPulse, string expectedFirst, string expectedSecond)
        {
            var semanticModelFactory = CreateSemanticModelFactory();
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { MaximumPulse = firstPulse }));
            locationToolAssignments.Add(CreateLocationToolAssignment.WithTestTechnique(new TestTechnique { MaximumPulse = secondPulse }));
            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);
            CollectionAssert.AreEqual(
                new List<string> { expectedFirst, expectedSecond },
                TestItemContentListFromRoute(routeContainerFinder, "AC_MaximumPulse"));
        }

        [Test]
        public void CheckHardcodedTagsInConvertHeader()
        {
            CheckDefaultHeaderHardcodedValues(PrepareHardcodedConvertCheck);
            MultiHardcodedValueChecks(
                new List<(string, string)>
                {
                    ("Instruction", "CMD_PROG"),
                    ("UseLogOn", "0"),
                    ("UseErrCode", "0"),
                    ("PCRule1", "0"),
                    ("PCRule2", "0"),
                    ("PCRule3", "0"),
                    ("PCRule4", "0"),
                    ("PCRule5", "0"),
                    ("PCRule6", "0"),
                    ("PCRule7", "0"),
                    ("AskForIdent", "0"),
                    ("QuitTest", "0"),
                    ("AllowDeleteTest", "0"),
                    ("LooseCheck", "0"),
                    ("UseFileId", "0"),
                    ("CustomerName", ""),
                },
                PrepareHardcodedConvertCheck);
        }

        [Test]
        public void CheckHardcodedTagsInConvertCommunication()
        {
            MultiHardcodedValueChecks(
                new List<(string, string)>
                {
                    ("TxMode", "Serial"),
                    ("ComPort", "1"),
                    ("ComBaud", "9600"),
                    ("IP_Host", ""),
                    ("IP_Port", "0"),
                    ("FTPUser", ""),
                    ("FTP_Pwd", ""),
                },
                PrepareHardcodedConvertCheck);
        }

        [Test]
        public void CheckHardcodedTagsInRoute()
        {
            MultiHardcodedValueChecks(
                new List<(string, string)>
                {
                    ("RoutePos", "1"),
                    ("RouteId", "0"),
                    ("RouteName", "QS-Torque"),
                    ("AskForIdent", "0"),
                    ("AskForCurve", "0"),
                },
                PrepareHardcodedConvertCheck);
        }

        [Test]
        public void CheckHardcodedTagsInTestItem()
        {
            MultiHardcodedValueChecks(
                new List<(string, string)>
                {
                    ("TestId2", "0"),
                    ("Unit1Id", ""),
                    ("Unit1", ""),
                    ("Nom1", ""),
                    ("Min1", ""),
                    ("Max1", ""),
                    ("Unit2Id", ""),
                    ("Unit2", ""),
                    ("Nom2", ""),
                    ("Min2", ""),
                    ("Max2", ""),
                    ("TestTypeId", "0"),
                    ("TestTypeName", ""),
                    ("AskForIdent", "0"),
                    ("AskForCurve", "0"),
                    ("ToolType", "1"),
                    ("AdapterId", "0"),
                    ("TransPos", "1"),
                    ("TransId", "0"),
                    ("TurnCW", "1"),
                    ("TreeParentId", "0"),
                    ("WiMin", "0.0"),
                    ("WiMax", "0.0"),
                    ("Info1", ""),
                    ("AC_TestType", "3"), // 3 = spcStandalone
                    ("AC_SubgroupFrequency", "1"),
                },
                PrepareHardcodedConvertCheck);
        }

        public enum SimpleCommandType
        {
            Read,
            Clear
        }

        [TestCase(SimpleCommandType.Read)]
        [TestCase(SimpleCommandType.Clear)]
        public void GeneratingSimpleCommandHasDataGateContainer(SimpleCommandType commandType)
        {
            AssertHasDataGateRootNode(
                CallGenerateSimpleCommand(
                    commandType,
                    null,
                    new DateTime()));
        }

        [TestCase(SimpleCommandType.Read)]
        [TestCase(SimpleCommandType.Clear)]
        public void GeneratingSimpleCommandHasAllTagsInDataGateContainer(SimpleCommandType commandType)
        {
            AssertItemsInDataGateRootContainer(
                CallGenerateSimpleCommand(
                    commandType,
                    null,
                    new DateTime()),
                new List<string>
                {
                    "Header",
                    "Communication"
                });
        }

        [TestCase(SimpleCommandType.Read)]
        [TestCase(SimpleCommandType.Clear)]
        public void GeneratingSimpleCommandHasAllTheTagsInHeader(SimpleCommandType commandType)
        {
            AssertItemsInHeaderContainer(
                CallGenerateSimpleCommand(
                    commandType,
                    null,
                    new DateTime()),
                new List<string>
                {
                    "dgVersion",
                    "qstVersion",
                    "qstLevel",
                    "source",
                    "destination",
                    "fileDate",
                    "fileTime",
                    "DevSerNo",
                    "DevInvNo",
                    // "Handle",
                    "Instruction",
                    "ResultFile",
                    "StatusFile",
                });
        }

        [TestCase(SimpleCommandType.Read, "serno")]
        [TestCase(SimpleCommandType.Read, "um348275892")]
        [TestCase(SimpleCommandType.Clear, "serno")]
        [TestCase(SimpleCommandType.Clear, "um348275892")]
        public void GeneratingSimpleCommandSetsDestinationAndDevSerNoToTestEquipmentSerialNumber(
            SimpleCommandType commandType,
            string serialNumber)
        {
            AssertTagContentAreEqual(
                serialNumber,
                new List<string>
                {
                    "destination",
                    "DevSerNo"
                },
                CallGenerateSimpleCommand(
                    commandType,
                    CreateTestEquipment.WithSerialNumber(serialNumber),
                    new DateTime()),
                result => (result as Content).GetValue());
        }

        [TestCase(SimpleCommandType.Read, "gjakklvmwerap")]
        [TestCase(SimpleCommandType.Read, "Schnitzel mit Pommes")]
        [TestCase(SimpleCommandType.Clear, "gjakklvmwerap")]
        [TestCase(SimpleCommandType.Clear, "Schnitzel mit Pommes")]
        public void GeneratingSimpleCommandSetsStatusFilePathFromTestEquipmentFilePath(
            SimpleCommandType commandType,
            string statusFilePath)
        {
            AssertTagContentAreEqual(
                statusFilePath,
                new List<string> { "StatusFile" },
                CallGenerateSimpleCommand(
                    commandType,
                    CreateTestEquipment.WithModelStatusPath(statusFilePath),
                    new DateTime()),
                result => (result as Content).GetValue());
        }

        [TestCase(SimpleCommandType.Read, "jakoiwemowwg")]
        [TestCase(SimpleCommandType.Read, "Pizza Pizza Pizza Pizza")]
        [TestCase(SimpleCommandType.Clear, "jakoiwemowwg")]
        [TestCase(SimpleCommandType.Clear, "Pizza Pizza Pizza Pizza")]
        public void GeneratingSimpleCommandSetsResultFilePathFromTestEquipmentFilePath(
            SimpleCommandType commandType,
            string resultFilePath)
        {
            AssertTagContentAreEqual(
                resultFilePath,
                new List<string> { "ResultFile" },
                CallGenerateSimpleCommand(
                    commandType,
                    CreateTestEquipment.WithModelResultPath(resultFilePath),
                    new DateTime()),
                result => (result as Content).GetValue());
        }

        [TestCase(SimpleCommandType.Read, 2007, 9, 18, 0, 43, 58, "2007-09-18", "00:43:58")]
        [TestCase(SimpleCommandType.Read, 2009, 6, 30, 2, 37, 45, "2009-06-30", "02:37:45")]
        [TestCase(SimpleCommandType.Clear, 2007, 9, 18, 0, 43, 58, "2007-09-18", "00:43:58")]
        [TestCase(SimpleCommandType.Clear, 2009, 6, 30, 2, 37, 45, "2009-06-30", "02:37:45")]
        public void GeneratingSimpleCommandSetsFileDateAndTime(
            SimpleCommandType commandType,
            int year,
            int month,
            int day,
            int hour,
            int minute,
            int second,
            string expectedDate,
            string expectedTime)
        {
            var semanticModel = CallGenerateSimpleCommand(
                commandType,
                null,
                new DateTime(year, month, day, hour, minute, second));
            Func<IElement, string> getResultValue = result => (result as Content).GetValue();
            AssertTagContentAreEqual(expectedDate, new List<string> { "fileDate" }, semanticModel, getResultValue);
            AssertTagContentAreEqual(expectedTime, new List<string> { "fileTime" }, semanticModel, getResultValue);
        }

        [TestCase(SimpleCommandType.Read, "CMD_READ")]
        [TestCase(SimpleCommandType.Clear, "CMD_CLEAR")]
        public void CheckHardcodedTagsInSimpleCommandHeader(SimpleCommandType commandType, string commandString)
        {
            CheckDefaultHeaderHardcodedValues(() => PrepareHardcodedSimpleCommandCheck(commandType));
            HardCodedValueCheck(
                "Instruction",
                commandString, 
                ()=>PrepareHardcodedSimpleCommandCheck(commandType));
        }

        [TestCase(SimpleCommandType.Read)]
        [TestCase(SimpleCommandType.Clear)]
        public void CheckHardcodedTagsInSimpleCommandCommunication(SimpleCommandType commandType)
        {
            MultiHardcodedValueChecks(
                new List<(string, string)>
                {
                    ("TxMode", "Serial"),
                    ("ComPort", "1"),
                    ("ComBaud", "9600"),
                    ("IP_Host", ""),
                    ("IP_Port", "0"),
                    ("FTPUser", ""),
                    ("FTP_Pwd", ""),
                },
                () => PrepareHardcodedSimpleCommandCheck(commandType));
        }

        private static IEnumerable<List<LocationToolAssignment>> ConvertingToSemanticModelCallsTreePathBuilderWithAssignedLocationsData =
            new List<List<LocationToolAssignment>>
            {
                new List<LocationToolAssignment>()
                {
                    CreateLocationToolAssignment.WithLocation(CreateLocation.IdOnly(1)),
                    CreateLocationToolAssignment.WithLocation(CreateLocation.IdOnly(2)),
                    CreateLocationToolAssignment.WithLocation(CreateLocation.IdOnly(3))
                },
                new List<LocationToolAssignment>()
                {
                    CreateLocationToolAssignment.WithLocation(CreateLocation.IdOnly(99)),
                    CreateLocationToolAssignment.WithLocation(CreateLocation.IdOnly(100)),
                },
            };

        [TestCaseSource(nameof(ConvertingToSemanticModelCallsTreePathBuilderWithAssignedLocationsData))]
        public void ConvertingToSemanticModelCallsTreePathBuilderWithAssignedLocations(List<LocationToolAssignment> locationToolAssignments)
        {
            var treePatchBuilderMock = new TreePathBuilderMock();
            var semanticModelFactory = new SemanticModelFactory(treePatchBuilderMock);

            semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            CollectionAssert.AreEqual(locationToolAssignments.Select(x => x.AssignedLocation).ToList(), treePatchBuilderMock.GetTreePathLocationParameters);
        }

        [Test]
        public void ConvertingToSemanticModelCallsTreePathBuilderWithCorrectSeperator()
        {
            var treePatchBuilderMock = new TreePathBuilderMock();
            var semanticModelFactory = new SemanticModelFactory(treePatchBuilderMock);

            var locationToolAssignments = new List<LocationToolAssignment>
            {
                CreateLocationToolAssignment.WithLocation(CreateLocation.IdOnly(1))
            };

            semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);
            CollectionAssert.AreEqual(" - ", treePatchBuilderMock.GetTreePathSeperatorParameter);
        }

        private static IEnumerable<List<string>> ConvertingToSemanticModelSetsLocationDirectoryPathToPathData =
            new List<List<string>>
            {
                new List<string>
                {
                    "A - B - C",
                    "D - E - F",
                    "G - H - I",
                },
                new List<string>()
                {
                    "J - K - L - M - N - O - P - Q - R",
                    "S - T - U - V - X -Y - Z",
                },
            };

        [TestCaseSource(nameof(ConvertingToSemanticModelSetsLocationDirectoryPathToPathData))]
        public void ConvertingToSemanticModelSetsLocationDirectoryPathToPath(List<string> pathList)
        {
            var treeBuilder = new TreePathBuilderMock {GetTreePathLocationReturnValues = pathList};
            var semanticModelFactory = new SemanticModelFactory(treeBuilder);
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();

            foreach (var path in pathList)
            {
                locationToolAssignments.Add(CreateLocationToolAssignment.WithLocation(CreateLocation.Anonymous()));
            }

            var semanticModel = semanticModelFactory.Convert(null, locationToolAssignments, (0.0, 0.0), new DateTime(), TestType.Chk);

            var routeContainerFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeContainerFinder);

            var routePathValues = TestItemContentListFromRoute(routeContainerFinder, "Path");
            CollectionAssert.AreEqual(pathList,routePathValues);
        }

        private static void AssertHasDataGateRootNode(SemanticModel semanticModel)
        {
            var rootContainerGetter = new FindFirstByName(new ElementName("sDataGate"));
            semanticModel.Accept(rootContainerGetter);
            Assert.AreEqual("sDataGate", rootContainerGetter.Result.GetName().ToDefaultString());
        }

        private static void AssertItemsInDataGateRootContainer(SemanticModel semanticModel, List<string> expected)
        {
            var dgContainerGetter = new FindFirstByName(new ElementName("sDataGate"));
            semanticModel.Accept(dgContainerGetter);
            CollectionAssert.AreEqual(expected,
                (dgContainerGetter.Result as Container).Select(item => item.GetName().ToDefaultString()).ToList());
        }

        private static void AssertItemsInHeaderContainer(SemanticModel semanticModel, List<string> expected)
        {
            var headerFinder = new FindFirstByName(new ElementName("Header"));
            semanticModel.Accept(headerFinder);
            CollectionAssert.AreEqual(expected,
                (headerFinder.Result as Container).Select(item => item.GetName().ToDefaultString()).ToList());
        }

        private static void AssertTagContentAreEqual(
            string expected,
            List<string> tags,
            SemanticModel semanticModel,
            Func<IElement, string> getResult)
        {
            var tagFinders = tags.Select(tag => new FindFirstByName(new ElementName(tag))).ToList();
            foreach (var tagFinder in tagFinders)
            {
                semanticModel.Accept(tagFinder);
                Assert.AreEqual(expected, getResult(tagFinder.Result));
            }
        }

        private static void HardCodedValueCheck(string tag, string value, Func<SemanticModel> prepareSemanticModel)
        {
            var tagFinder = new FindFirstByName(new ElementName(tag));
            prepareSemanticModel().Accept(tagFinder);
            Assert.AreEqual(value, (tagFinder.Result as Content).GetValue());
        }
        private static void MultiHardcodedValueChecks(List<(string, string)> tags, Func<SemanticModel> prepareHardcodedConvertCheck)
        {
            foreach (var element in tags)
            {
                HardCodedValueCheck(element.Item1, element.Item2, prepareHardcodedConvertCheck);
            }
        }
        private static void CheckDefaultHeaderHardcodedValues(Func<SemanticModel> prepareHardcodedValuesCheck)
        {
            MultiHardcodedValueChecks(
                new List<(string, string)>
                {
                    ("dgVersion", "7.0"),
                    ("qstVersion", "8.0.0.0"),
                    ("qstLevel", "500000"),
                    ("source", "QST"),
                    ("DevInvNo", ""),
                },
                prepareHardcodedValuesCheck);
        }

        private static SemanticModel PrepareHardcodedConvertCheck()
        {
            var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
            locationToolAssignments.Add(CreateLocationToolAssignment.Anonymous());
            var semanticModel = CreateSemanticModelFactory().Convert(null, locationToolAssignments, (0, 0), new DateTime(), TestType.Chk);
            return semanticModel;
        }

        private static SemanticModel PrepareHardcodedSimpleCommandCheck(SimpleCommandType commandType)
        {
            var semanticModel = CallGenerateSimpleCommand(commandType, null, new DateTime());
            return semanticModel;
        }

        private static List<string> TestItemContentListFromRoute(FindFirstByName routeContainerFinder, string testItemFieldname)
        {
            Func<IElement, bool> testItemFilter = item => item.GetName().ToDefaultString() == "TestItem";
            Func<IElement, bool> testPosFinder = testItemChild => testItemChild.GetName().ToDefaultString() == testItemFieldname;

            return (routeContainerFinder.Result as Container)
                .Where(testItemFilter)
                .Select(testitem =>
                {
                    var testPosElement = ((testitem as Container).First(testPosFinder) as Content);
                    return testPosElement.GetValue();
                }).ToList();
        }

        private static List<string> TestItemHiddenContentListFromRoute(FindFirstByName routeContainerFinder, string testItemFieldname)
        {
            Func<IElement, bool> testItemFilter = item => item.GetName().ToDefaultString() == "TestItem";
            Func<IElement, bool> testPosFinder = testItemChild => testItemChild.GetName().ToDefaultString() == testItemFieldname;

            return (routeContainerFinder.Result as Container)
                .Where(testItemFilter)
                .Select(testitem =>
                {
                    var testPosElement = ((testitem as Container).First(testPosFinder) as HiddenContent);
                    return testPosElement.GetValue();
                }).ToList();
        }

        private static List<LocationToolAssignment> CreateLocationToolAssignmentsEmpty()
        {
            return new List<LocationToolAssignment>();
        }

        private static List<Location> CreateLocationsEmpty()
        {
            return new List<Location>();
        }

        private static List<ProcessControlCondition> CreateProcessControlConditionsEmpty()
        {
            return new List<ProcessControlCondition>();
        }

        public enum ConversionType
        {
            Rotating,
            Process
        }

        public interface IRotatingConvertDataLoader
        {
            List<LocationToolAssignment> LocationToolAssignments();
        }

        public interface IProcessConvertDataLoader
        {
            List<Location> Locations();
            List<ProcessControlCondition> ProcessControlConditions();
        }

        public interface IDataLoader: IRotatingConvertDataLoader, IProcessConvertDataLoader
        {
        }

        private class EmptyConvertDataLoader : IDataLoader
        {
            public List<LocationToolAssignment> LocationToolAssignments()
            {
                return CreateLocationToolAssignmentsEmpty();
            }

            public List<Location> Locations()
            {
                return CreateLocationsEmpty();
            }

            public List<ProcessControlCondition> ProcessControlConditions()
            {
                return CreateProcessControlConditionsEmpty();
            }
        }

        private class NAnonymousConvertDataLoader : IDataLoader
        {
            public NAnonymousConvertDataLoader(int routeEntryCount)
            {
                _routeEntryCount = routeEntryCount;
                _processControlConditions = new List<ProcessControlCondition>();
                for (int i = 0; i < _routeEntryCount; i++)
                {
                    _processControlConditions.Add(CreateProcessControlCondition.Anonymous());
                }
                _locations =
                    _processControlConditions
                        .Select(condition => CreateLocation.IdOnly(condition.Location.Id.ToLong()))
                        .ToList();
            }

            public List<Location> Locations()
            {
                return _locations;
            }

            public List<LocationToolAssignment> LocationToolAssignments()
            {
                var locationToolAssignments = CreateLocationToolAssignmentsEmpty();
                for (int i = 0; i < _routeEntryCount; i++)
                {
                    locationToolAssignments.Add(CreateLocationToolAssignment.Anonymous());
                }
                return locationToolAssignments;
            }

            public List<ProcessControlCondition> ProcessControlConditions()
            {
                return _processControlConditions;
            }

            private readonly int _routeEntryCount;
            private List<ProcessControlCondition> _processControlConditions;
            private List<Location> _locations;
        }

        private class SingleParameterTestConditionConvertDataLoader : IDataLoader
        {
            public SingleParameterTestConditionConvertDataLoader(
                Func<string, Entities.TestParameters> testParametersFactory,
                List<string> values)
            {
                var toolAssignmentCounter = 0;
                _toolAssignments =
                    values
                        .Select(
                            value => CreateLocationToolAssignment.WithTestParameters(testParametersFactory(value)))
                        .ToList();
            }

            public static Func<string, Entities.TestParameters> CreateToolAssignmentFactory(CreateTestParameters.Parameter parameter)
            {
                return
                    (string value) =>
                        CreateTestParameters.WithDynamicParameters(
                            new List<(CreateTestParameters.Parameter parameter, string value)>
                            {
                                (parameter, value)
                            });
            }

            public List<Location> Locations()
            {
                throw new NotImplementedException();
            }

            public List<LocationToolAssignment> LocationToolAssignments()
            {
                return _toolAssignments;
            }

            public List<ProcessControlCondition> ProcessControlConditions()
            {
                throw new NotImplementedException();
            }

            private List<LocationToolAssignment> _toolAssignments;
        }

        private class SingleParameterLocationConvertDataLoader : IDataLoader
        {
            public SingleParameterLocationConvertDataLoader(
                Func<long, string, Location> locationFactory,
                List<string> values)
            {
                var locationIdCounter = 0;
                _locations =
                    values
                        .Select(value => locationFactory(++locationIdCounter, value))
                        .ToList();
                _controlConditions =
                    _locations
                        .Select(location => CreateProcessControlCondition.WithLocationId(location.Id.ToLong()))
                        .ToList();
                var toolAssignmentCounter = 0;
                _toolAssignments =
                    values
                        .Select(
                            value =>
                                CreateLocationToolAssignment.WithLocation(
                                    locationFactory(++toolAssignmentCounter, value)))
                        .ToList();
            }

            public static Func<long, string, Location> CreateLocationFactory(
                CreateLocation.LocationParameter parameter)
            {
                return
                    (id, value) =>
                    {
                        return
                            CreateLocation.WithDynamicParameters(
                                new List<(CreateLocation.LocationParameter, string)>
                                {
                                            (CreateLocation.LocationParameter.Id, id.ToString()),
                                            (parameter, value)
                                });
                    };
            }

            public List<Location> Locations()
            {
                return _locations;
            }

            public List<LocationToolAssignment> LocationToolAssignments()
            {
                return _toolAssignments;
            }

            public List<ProcessControlCondition> ProcessControlConditions()
            {
                return _controlConditions;
            }

            private List<Location> _locations;
            private List<ProcessControlCondition> _controlConditions;
            private List<LocationToolAssignment> _toolAssignments;
        }

        private static SemanticModel CallConvert(
            SemanticModelFactory factory,
            TestEquipment testEquipment,
            (double cm, double cmk) cmCmk,
            DateTime localNow,
            IDataLoader dataLoader,
            ConversionType callType)
        {
            switch (callType)
            {
                case ConversionType.Rotating:
                    return
                        factory.Convert(
                            testEquipment,
                            dataLoader.LocationToolAssignments(),
                            cmCmk,
                            localNow,
                            TestType.Chk);

                case ConversionType.Process:
                    return
                        factory.Convert(
                            testEquipment,
                            dataLoader.Locations(),
                            dataLoader.ProcessControlConditions(),
                            localNow);

                default:
                    throw new Exception("Unknown ConversionType");
            }
        }

        private static SemanticModel CallGenerateSimpleCommand(
            SimpleCommandType type,
            TestEquipment equipment,
            DateTime timestamp)
        {
            switch (type)
            {
                case SimpleCommandType.Read:
                    return CreateSemanticModelFactory().ReadCommand(equipment, timestamp);
                case SimpleCommandType.Clear:
                    return CreateSemanticModelFactory().ClearCommand(equipment, timestamp);
            }
            Assert.Fail();
            return null;
        }

        private static SemanticModelFactory CreateSemanticModelFactory()
        {
            return new SemanticModelFactory(new TreePathBuilderMock());
        }

        private static (double, double) _noCmCmk = (0, 0);
        private static DateTime _noTime = new DateTime();
    }
}
