using System;
using System.Collections.Generic;
using System.Linq;
using Client.Core.Entities;
using Client.TestHelper.Factories;
using Core.Entities;
using Core.UseCases;
using NUnit.Framework;
using TestHelper.Factories;
using TestHelper.Mock;


namespace Core.Test.UseCases
{
    public class ClassicTestUseCaseTest
    {
        public class ClassicTestErrorMock : IClassicTestDataErrorShower
        {
            public void ShowErrorMessage()
            {
                ShowErrorMessageCalled = true;
            }

            public bool ShowErrorMessageCalled { get; set; }
        }

        public class ShowEvaluationMock : IShowEvaluation
        {
            public void ShowValuesForClassicChkHeader(Location location, Tool tool, List<ClassicChkTest> chktest)
            {
                ShowValuesForClassicChkHeaderParameterLocation = location;
                ShowValuesForClassicChkHeaderParameterTool = tool;
                ShowValuesForClassicChkHeaderParameterTests = chktest;
            }

            public Tool ShowValuesForClassicChkHeaderParameterTool { get; set; }

            public List<ClassicChkTest> ShowValuesForClassicChkHeaderParameterTests { get; set; }

            public Location ShowValuesForClassicChkHeaderParameterLocation { get; set; }

            public void ShowValuesForClassicMfuHeader(Location location, Tool tool, List<ClassicMfuTest> mfutest)
            {
                ShowValuesForClassicMfuHeaderParameterLocation = location;
                ShowValuesForClassicMfuHeaderParameterTool = tool;
                ShowValuesForClassicMfuHeaderParameterTests = mfutest;
            }

            public void ShowValuesForClassicProcessHeader(Location location, List<ClassicProcessTest> tests, bool isPfu)
            {
                ShowValuesForClassicProcessHeaderLocation = location;
                ShowValuesForClassicProcessHeaderTests = tests;
                ShowValuesForClassicProcessHeaderIsPfu = isPfu;
            }

            public bool ShowValuesForClassicProcessHeaderIsPfu { get; set; }
            public List<ClassicProcessTest> ShowValuesForClassicProcessHeaderTests { get; set; }
            public Location ShowValuesForClassicProcessHeaderLocation { get; set; }
            public Tool ShowValuesForClassicMfuHeaderParameterTool { get; set; }
            public List<ClassicMfuTest> ShowValuesForClassicMfuHeaderParameterTests { get; set; }
            public Location ShowValuesForClassicMfuHeaderParameterLocation { get; set; }
        }

        public class ClassicTestGuiMock : IClassicTestGui
        {
            
            public bool ShowToolsForSelectedLocationCalled;
            public Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)>
                ShowToolsForSelectedLocationCalledParameter;

            public void ShowToolsForSelectedLocation(Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)> tools)
            {
                ShowToolsForSelectedLocationCalled = true;
                ShowToolsForSelectedLocationCalledParameter = tools;
            }

            public List<ClassicProcessTest> ShowProcessHeaderForSelectedLocationHeader { get; set; }
            public void ShowProcessHeaderForSelectedLocation(List<ClassicProcessTest> header)
            {
                ShowProcessHeaderForSelectedLocationHeader = header;
            }

            public bool ShowMfuHeaderForSelectedToolCalled;
            public List<ClassicMfuTest> ShowMfuHeaderForSelectedToolCalledParameterHeader;
            public void ShowMfuHeaderForSelectedTool(List<ClassicMfuTest> header)
            {
                ShowMfuHeaderForSelectedToolCalled = true;
                ShowMfuHeaderForSelectedToolCalledParameterHeader = header;
            }

            public bool ShowChkHeaderForSelectedToolCalled;
            public List<ClassicChkTest> ShowChkHeaderForSelectedToolCalledParameterHeader;
            public void ShowChkHeaderForSelectedTool(List<ClassicChkTest> header)
            {
                ShowChkHeaderForSelectedToolCalled = true;
                ShowChkHeaderForSelectedToolCalledParameterHeader = header;
            }
        }
        public class ClassicTestDataAccessMock : IClassicTestData
        {
            public bool ThrowError;
            public ClassicTestDataAccessMock(bool throwError = false)
            {
                ThrowError = throwError;
            }

            public bool LoadToolsForSelectedLocationCalled;
            public LocationId LoadToolsForLocationParameter;
            public Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)>
                LoadToolsFromLocationTestsReturnValue;
            public Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)> LoadToolsFromLocationTests(LocationId locationId)
            {
                LoadToolsForSelectedLocationCalled = true;
                LoadToolsForLocationParameter = locationId;
                if (ThrowError)
                {
                    throw new Exception("irgendwos is hi");
                }

                return LoadToolsFromLocationTestsReturnValue;
            }

            public bool GetClassicMfuHeaderFromToolCalled;
            public ToolId GetClassicMfuHeaderFromToolToolParameter;
            public List<ClassicMfuTest> GetClassicMfuHeaderFromToolReturnValue;
            public LocationId GetClassicMfuHeaderFromToolLocationParameter;

            public List<ClassicMfuTest> GetClassicMfuHeaderFromTool(ToolId toolId, LocationId locationId)
            {
                GetClassicMfuHeaderFromToolCalled = true;
                GetClassicMfuHeaderFromToolToolParameter = toolId;
                GetClassicMfuHeaderFromToolLocationParameter = locationId;
                if (ThrowError)
                {
                    throw new Exception("irgendwos is hi");
                }

                return GetClassicMfuHeaderFromToolReturnValue;
            }

            public bool GetClassicChkHeaderFromToolCalled;
            public ToolId GetClassicChkHeaderFromToolToolParameter;
            public LocationId GetClassicChkHeaderFromToolLocationParameter;
            public List<ClassicChkTest> GetClassicChkHeaderFromToolReturnValue;
            public List<ClassicChkTest> GetClassicChkHeaderFromTool(ToolId toolId, LocationId locationId)
            {
                GetClassicChkHeaderFromToolCalled = true;
                GetClassicChkHeaderFromToolToolParameter = toolId;
                GetClassicChkHeaderFromToolLocationParameter = locationId;
                if (ThrowError)
                {
                    throw new Exception("irgendwos is hi");
                }

                return GetClassicChkHeaderFromToolReturnValue;
            }

            public bool GetValuesFromClassicChkHeaderCalled = false;
            public List<ClassicChkTest> GetValuesFromClassicChkHeaderParameter;
            public List<ClassicChkTestValue> GetValuesFromClassicChkHeaderReturnValue;
            public List<ClassicChkTestValue> GetValuesFromClassicChkHeader(List<ClassicChkTest> header)
            {
                GetValuesFromClassicChkHeaderCalled = true;
                GetValuesFromClassicChkHeaderParameter = header;
                if (ThrowError)
                {
                    throw new Exception("irgendwos is hi");
                }

                return GetValuesFromClassicChkHeaderReturnValue;
            }

            public bool GetValuesFromClassicMfuHeaderCalled = false;
            public List<ClassicMfuTest> GetValuesFromClassicMfuHeaderParameter;
            public List<ClassicMfuTestValue> GetValuesFromClassicMfuHeaderReturnValue;
            public List<ClassicMfuTestValue> GetValuesFromClassicMfuHeader(List<ClassicMfuTest> header)
            {
                GetValuesFromClassicMfuHeaderCalled = true;
                GetValuesFromClassicMfuHeaderParameter = header;
                if (ThrowError)
                {
                    throw new Exception("irgendwos is hi");
                }

                return GetValuesFromClassicMfuHeaderReturnValue;
            }

            public LocationId GetClassicProcessHeaderFromLocationLocationId { get; set; }
            public bool GetClassicProcessHeaderFromLocationThrowsError { get; set; }
            public List<ClassicProcessTest> GetClassicProcessHeaderFromLocationReturnValue { get; set; }
            public List<ClassicProcessTest> GetClassicProcessHeaderFromLocation(LocationId locationId)
            {
                if (GetClassicProcessHeaderFromLocationThrowsError)
                {
                    throw new Exception();
                }
                GetClassicProcessHeaderFromLocationLocationId = locationId;
                return GetClassicProcessHeaderFromLocationReturnValue;
            }
            
            public List<ClassicProcessTestValue> GetValuesFromClassicProcessHeaderReturnValue { get; set; }
            public List<ClassicProcessTest> GetValuesFromClassicProcessHeaderParameter { get; set; }
            public bool GetValuesFromClassicProcessHeaderThrowsError { get; set; }
            public List<ClassicProcessTestValue> GetValuesFromClassicProcessHeader(List<ClassicProcessTest> tests)
            {
                if (GetValuesFromClassicProcessHeaderThrowsError)
                {
                    throw new Exception();
                }
                GetValuesFromClassicProcessHeaderParameter = tests;
                return GetValuesFromClassicProcessHeaderReturnValue;
            }
        }

        [TestCase(1, 2)]
        [TestCase(11, 22)]
        public void LoadToolsFromLocationTestsCalledWithCorrectParameter(long toolId, long locationId)
        {
            var environment = new Environment();

            environment.mocks.classicTestDataAccessMock.LoadToolsFromLocationTestsReturnValue = new Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)>();
            environment.mocks.classicTestDataAccessMock.LoadToolsFromLocationTestsReturnValue.Add(CreateTool.WithId(toolId), (DateTime.Now, DateTime.Now, true));

            var location = CreateLocation.IdOnly(locationId);
            environment.useCase.LoadToolsFromLocationTests(location, environment.mocks.ClassicTestError);

            Assert.AreEqual(location.Id, environment.mocks.classicTestDataAccessMock.LoadToolsForLocationParameter);
            Assert.AreEqual(environment.mocks.classicTestDataAccessMock.LoadToolsFromLocationTestsReturnValue, environment.mocks.classicTestGuiMock.ShowToolsForSelectedLocationCalledParameter);
            Assert.IsFalse(environment.mocks.ClassicTestError.ShowErrorMessageCalled);
        }

        [Test]
        public void LoadToolsFromLocationTestsWithErrorCallsShowErrorMessage()
        {
            var environment = new Environment();
            environment.mocks.classicTestDataAccessMock.ThrowError = true;
            environment.useCase.LoadToolsFromLocationTests(TestHelper.Factories.CreateLocation.Anonymous(), environment.mocks.ClassicTestError);
            Assert.IsTrue(environment.mocks.classicTestDataAccessMock.LoadToolsForSelectedLocationCalled);
            Assert.IsFalse(environment.mocks.classicTestGuiMock.ShowToolsForSelectedLocationCalled);
            Assert.IsTrue(environment.mocks.ClassicTestError.ShowErrorMessageCalled);
        }

        [TestCase(1, 3, 6)]
        [TestCase(5, 9, 8)]
        public void LoadMfuHeaderFromToolCalledWithCorrectParameter(long toolId, long locationId, long globaHistoryId)
        {
            var environment = new Environment();
            var classicMfuList = new List<ClassicMfuTest>() { new ClassicMfuTest() { Id = new GlobalHistoryId(globaHistoryId) } };
            environment.mocks.classicTestDataAccessMock.GetClassicMfuHeaderFromToolReturnValue = classicMfuList;

            var tool = CreateTool.WithId(toolId);
            var location = CreateLocation.IdOnly(locationId);
            environment.useCase.LoadMfuHeaderFromTool(tool, environment.mocks.ClassicTestError, location);

            Assert.AreEqual(tool.Id, environment.mocks.classicTestDataAccessMock.GetClassicMfuHeaderFromToolToolParameter);
            Assert.AreEqual(location.Id, environment.mocks.classicTestDataAccessMock.GetClassicMfuHeaderFromToolLocationParameter);

            Assert.AreEqual(classicMfuList, environment.mocks.classicTestGuiMock.ShowMfuHeaderForSelectedToolCalledParameterHeader);

            Assert.IsFalse(environment.mocks.ClassicTestError.ShowErrorMessageCalled);
        }

        [Test]
        public void LoadMfuHeaderFromToolCallsDataAccessWithNullLocationIfLocationIsNull()
        {
            var environment = new Environment();
            environment.useCase.LoadMfuHeaderFromTool(CreateTool.Anonymous(), null);

            Assert.IsNull(environment.mocks.classicTestDataAccessMock.GetClassicMfuHeaderFromToolLocationParameter);
        }

        [Test]
        public void LoadMfuHeaderFromToolWithErrorCallsShowErrorMessage()
        {
            var environment = new Environment();
            environment.mocks.classicTestDataAccessMock.ThrowError = true;
            environment.useCase.LoadMfuHeaderFromTool(new Tool(), environment.mocks.ClassicTestError);
            Assert.IsTrue(environment.mocks.classicTestDataAccessMock.GetClassicMfuHeaderFromToolCalled);
            Assert.IsFalse(environment.mocks.classicTestGuiMock.ShowMfuHeaderForSelectedToolCalled);
            Assert.IsTrue(environment.mocks.ClassicTestError.ShowErrorMessageCalled);
        }

        [TestCase(1, 6, 1)]
        [TestCase(13, 61, 5)]
        public void LoadChkHeaderFromToolCalledWithCorrectParameter(long toolId, long locationId, long globaHistoryId)
        {
            var environment = new Environment();

            var classicChkList = new List<ClassicChkTest>() { new ClassicChkTest() { Id = new GlobalHistoryId(globaHistoryId) } };
            environment.mocks.classicTestDataAccessMock.GetClassicChkHeaderFromToolReturnValue = classicChkList;

            var tool = CreateTool.WithId(toolId);
            var location = CreateLocation.IdOnly(locationId);
            environment.useCase.LoadChkHeaderFromTool(tool, environment.mocks.ClassicTestError, location);

            Assert.AreEqual(tool.Id, environment.mocks.classicTestDataAccessMock.GetClassicChkHeaderFromToolToolParameter);

            Assert.AreEqual(classicChkList, environment.mocks.classicTestGuiMock.ShowChkHeaderForSelectedToolCalledParameterHeader);            

            Assert.IsFalse(environment.mocks.ClassicTestError.ShowErrorMessageCalled);
        }

        [Test]
        public void LoadChkHeaderFromToolCallsDataAccessWithNullLocationIfLocationIsNull()
        {
            var environment = new Environment();
            environment.useCase.LoadChkHeaderFromTool(CreateTool.Anonymous(), null);

            Assert.IsNull(environment.mocks.classicTestDataAccessMock.GetClassicChkHeaderFromToolLocationParameter);
        }

        [Test]
        public void LoadChkHeaderFromToolWithErrorCallsShowErrorMessage()
        {
            var environment = new Environment();
            environment.mocks.classicTestDataAccessMock.ThrowError = true;

            environment.useCase.LoadChkHeaderFromTool(new Tool(), environment.mocks.ClassicTestError);
            Assert.IsTrue(environment.mocks.classicTestDataAccessMock.GetClassicChkHeaderFromToolCalled);
            Assert.IsFalse(environment.mocks.classicTestGuiMock.ShowChkHeaderForSelectedToolCalled);
            Assert.IsTrue(environment.mocks.ClassicTestError.ShowErrorMessageCalled);
        }

        [TestCase(4, 8, 3)]
        [TestCase(41, 3, 8)]
        public void LoadValuesFromClassicChkHeaderCalledWithCorrectParameter(long toolId, long locationId, long globalHistoryId)
        {
            var environment = new Environment();
            var classicChkHeader = new List<ClassicChkTestValue>() { new ClassicChkTestValue() { Id = new GlobalHistoryId(globalHistoryId) } };
            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicChkHeaderReturnValue = classicChkHeader;

            var location = CreateLocation.IdOnly(locationId);
            var tool = CreateTool.WithId(toolId);
            var classicChkTest = new List<ClassicChkTest>() { new ClassicChkTest() { Id = new GlobalHistoryId(globalHistoryId) } };

            environment.useCase.LoadValuesForClassicChkHeader(location, tool, classicChkTest, environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);

            Assert.AreEqual(classicChkTest, environment.mocks.classicTestDataAccessMock.GetValuesFromClassicChkHeaderParameter);

            Assert.AreEqual(location, environment.mocks.ShowEvaluation.ShowValuesForClassicChkHeaderParameterLocation);
            Assert.AreEqual(tool, environment.mocks.ShowEvaluation.ShowValuesForClassicChkHeaderParameterTool);
            Assert.AreEqual(classicChkTest, environment.mocks.ShowEvaluation.ShowValuesForClassicChkHeaderParameterTests);

            Assert.IsFalse(environment.mocks.ClassicTestError.ShowErrorMessageCalled);
        }


        private static IEnumerable<(Location, int)>
            LoadValuesFromClassicChkHeaderCallsLoadTreePathForLocationsCorrectData = new List<(Location, int)>()
            {
                (
                    CreateLocation.IdOnly(1), 1
                ),
                (
                    CreateLocation.WithIdAndLocationTreePath(1, new List<LocationDirectory>(){CreateLocationDirectory.Anonymous()}), 0
                )
            };


        [TestCaseSource(nameof(LoadValuesFromClassicChkHeaderCallsLoadTreePathForLocationsCorrectData))]
        public void LoadValuesFromClassicChkHeaderCallsLoadTreePathForLocationsCorrect((Location location, int count) data)
        {
            var environment = new Environment();

            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicChkHeaderReturnValue = new List<ClassicChkTestValue>();

            environment.useCase.LoadValuesForClassicChkHeader(data.location, CreateTool.Anonymous(), new List<ClassicChkTest>() { new ClassicChkTest() }, environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);

            Assert.AreEqual(data.count, environment.mocks.locationuseCase.LoadTreePathForLocationsParameter?.Count ?? 0);

            if (data.count > 0)
            {
                Assert.AreEqual(data.location.Id.ToLong(), environment.mocks.locationuseCase.LoadTreePathForLocationsParameter?.First().Id.ToLong());
            }
        }

        private static IEnumerable<(List<ClassicChkTest>, List<Tuple<long, long, DateTime>>)>
            LoadValuesFromClassicChkHeaderCallsGetClassIdFromHistoryForIdsWithCorrectParameterData = new List<(List<ClassicChkTest>, List<Tuple<long, long, DateTime>>)>
            {
                (
                    new List<ClassicChkTest>(),
                    new List<Tuple<long, long, DateTime>>()
                ),
                (
                    new List<ClassicChkTest>()
                    {
                        new ClassicChkTest()
                        {
                            Id = new GlobalHistoryId(1)
                        }
                    },
                    new List<Tuple<long, long, DateTime>>()
                ),
                (
                    new List<ClassicChkTest>()
                    {
                        new ClassicChkTest()
                        {
                            Id = new GlobalHistoryId(1),
                            ToleranceClassUnit1 = CreateToleranceClass.WithId(10),
                            Timestamp = new DateTime(2000,1,1)
                        }
                    },
                    new List<Tuple<long, long, DateTime>>()
                    {
                        new Tuple<long, long, DateTime>(1, 10, new DateTime(2000,1,1))
                    }
                ),
                (
                    new List<ClassicChkTest>()
                    {
                        new ClassicChkTest()
                        {
                            Id = new GlobalHistoryId(99),
                            ToleranceClassUnit2 = CreateToleranceClass.WithId(33),
                            Timestamp = new DateTime(2020,9,30)
                        }
                    },
                    new List<Tuple<long, long, DateTime>>()
                    {
                        new Tuple<long, long, DateTime>(99, 33, new DateTime(2020,9,30))
                    }
                ),
                (
                    new List<ClassicChkTest>()
                    {
                        new ClassicChkTest()
                        {
                            Id = new GlobalHistoryId(88),
                            ToleranceClassUnit1 = CreateToleranceClass.WithId(55),
                            ToleranceClassUnit2 = CreateToleranceClass.WithId(66),
                            Timestamp = new DateTime(2020,12,31)
                        }
                    },
                    new List<Tuple<long, long, DateTime>>()
                    {
                        new Tuple<long, long, DateTime>(88, 55, new DateTime(2020,12,31)),
                        new Tuple<long, long, DateTime>(88, 66, new DateTime(2020,12,31))
                    }
                ),

            };

        [TestCaseSource(nameof(LoadValuesFromClassicChkHeaderCallsGetClassIdFromHistoryForIdsWithCorrectParameterData))]
        public void LoadValuesFromClassicChkHeaderCallsGetClassIdFromHistoryForIdsWithCorrectParameter((List<ClassicChkTest> chkTests, List<Tuple<long, long, DateTime>> expectedList) data)
        {
            var environment = new Environment();

            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicChkHeaderReturnValue = new List<ClassicChkTestValue>();

            environment.useCase.LoadValuesForClassicChkHeader(CreateLocation.Anonymous(), CreateTool.Anonymous(), data.chkTests, environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);

            Assert.AreEqual(data.expectedList, environment.mocks.toleranceClassDataAccessMock.GetToleranceClassFromHistoryForIdsParameter);
        }

        [TestCase(1)]
        [TestCase(5)]
        public void LoadValuesFromClassicChkHeaderReturnsCorrectTestReferences(long globalHistoryId)
        {
            var environment = new Environment();

            var classicChkHeader = new List<ClassicChkTestValue>() { new ClassicChkTestValue() { Id = new GlobalHistoryId(globalHistoryId) } };
            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicChkHeaderReturnValue = classicChkHeader;

            var location = CreateLocation.IdOnly(1);
            var tool = CreateTool.WithId(1);
            var classicChkTest = new List<ClassicChkTest>() { new ClassicChkTest() { Id = new GlobalHistoryId(globalHistoryId) } };

            environment.useCase.LoadValuesForClassicChkHeader(location, tool, classicChkTest, environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);

            Assert.AreEqual(classicChkTest.First().Id.ToLong(), classicChkTest.First().TestValues.First().Id.ToLong());
            Assert.AreEqual(classicChkTest.First().TestValues.First().ChkTest.Id.ToLong(), classicChkTest.First().Id.ToLong());

            Assert.IsFalse(environment.mocks.ClassicTestError.ShowErrorMessageCalled);
        }

        [TestCaseSource(nameof(LoadValuesForClassicHeaderSetsToleranceClassCorrectData))]
        public void LoadValuesForClassicChkHeaderSetsToleranceClassCorrect(List<(GlobalHistoryId gid, ToleranceClass toleranceClass)> datas)
        {
            var environment = new Environment();
            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicChkHeaderReturnValue = new List<ClassicChkTestValue>();
            environment.mocks.toleranceClassDataAccessMock.GetToleranceClassFromHistoryForIdsReturnValue = new Dictionary<long, ToleranceClass>();
            var classicChkTest = new List<ClassicChkTest>();

            foreach (var data in datas)
            {
                environment.mocks.toleranceClassDataAccessMock.GetToleranceClassFromHistoryForIdsReturnValue.Add(data.gid.ToLong(), data.toleranceClass);
                classicChkTest.Add(new ClassicChkTest()
                {
                    Id = data.gid,
                    ToleranceClassUnit1 = CreateToleranceClass.WithId(data.toleranceClass.Id.ToLong()),
                    ToleranceClassUnit2 = CreateToleranceClass.WithId(data.toleranceClass.Id.ToLong())
                });
            }

            environment.useCase.LoadValuesForClassicChkHeader(CreateLocation.Anonymous(), CreateTool.Anonymous(), classicChkTest, environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);

            CollectionAssert.AreEqual(datas.Select(x => x.toleranceClass).ToList(), classicChkTest.Select(x => x.ToleranceClassUnit1).ToList());
            CollectionAssert.AreEqual(datas.Select(x => x.toleranceClass).ToList(), classicChkTest.Select(x => x.ToleranceClassUnit2).ToList());
        }

        [Test]
        public void LoadValuesForClassicChkHeaderNotSetsToleranceClassIfNoHistoryFound()
        {
            var environment = new Environment();
            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicChkHeaderReturnValue = new List<ClassicChkTestValue>();
            environment.mocks.toleranceClassDataAccessMock.GetToleranceClassFromHistoryForIdsReturnValue = new Dictionary<long, ToleranceClass>();

            var toleranceClassUnit1 = CreateToleranceClass.WithId(1);
            var toleranceClassUnit2 = CreateToleranceClass.WithId(2);
            var classicChkTest = new List<ClassicChkTest>()
            {
                new ClassicChkTest()
                {
                    Id = new GlobalHistoryId(1),
                    ToleranceClassUnit1 = toleranceClassUnit1,
                    ToleranceClassUnit2 = toleranceClassUnit2
                }
            };

            environment.useCase.LoadValuesForClassicChkHeader(CreateLocation.Anonymous(), CreateTool.Anonymous(), classicChkTest, environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);

            Assert.AreSame(toleranceClassUnit1, classicChkTest.First().ToleranceClassUnit1);
            Assert.AreSame(toleranceClassUnit2, classicChkTest.First().ToleranceClassUnit2);
        }

        [Test]
        public void LoadValuesFromClassicChkHeaderWithErrorCallsShowErrorMessage()
        {
            var environment = new Environment();
            environment.mocks.classicTestDataAccessMock.ThrowError = true;
            environment.useCase.LoadValuesForClassicChkHeader(CreateLocation.Anonymous(), CreateTool.Anonymous(), new List<ClassicChkTest>(), environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);
            Assert.IsTrue(environment.mocks.classicTestDataAccessMock.GetValuesFromClassicChkHeaderCalled);
            Assert.IsTrue(environment.mocks.ClassicTestError.ShowErrorMessageCalled);
        }

        [TestCase(1, 5, 6)]
        [TestCase(8, 7, 9)]
        public void LoadValuesFromClassicMfuHeaderCalledWithCorrectParameter(long toolId, long locationId, long globalHistoryId)
        {
            var environment = new Environment();

            var classicMfuHeader = new List<ClassicMfuTestValue>() { new ClassicMfuTestValue() { Id = new GlobalHistoryId(globalHistoryId) } };
            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicMfuHeaderReturnValue = classicMfuHeader;

            var location = CreateLocation.IdOnly(locationId);
            var tool = TestHelper.Factories.CreateTool.WithId(toolId);
            var classicMfuTest = new List<ClassicMfuTest>() { new ClassicMfuTest() { Id = new GlobalHistoryId(globalHistoryId) } };

            environment.useCase.LoadValuesForClassicMfuHeader(location, tool, classicMfuTest, environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);

            Assert.AreEqual(classicMfuTest, environment.mocks.classicTestDataAccessMock.GetValuesFromClassicMfuHeaderParameter);

            Assert.AreEqual(location, environment.mocks.ShowEvaluation.ShowValuesForClassicMfuHeaderParameterLocation);
            Assert.AreEqual(tool, environment.mocks.ShowEvaluation.ShowValuesForClassicMfuHeaderParameterTool);
            Assert.AreEqual(classicMfuTest, environment.mocks.ShowEvaluation.ShowValuesForClassicMfuHeaderParameterTests);

            Assert.IsFalse(environment.mocks.ClassicTestError.ShowErrorMessageCalled);
        }

        private static IEnumerable<(List<ClassicMfuTest>, List<Tuple<long, long, DateTime>>)>
            LoadValuesFromClassicMfuHeaderCallsGetClassIdFromHistoryForIdsWithCorrectParameterData = new List<(List<ClassicMfuTest>, List<Tuple<long, long, DateTime>>)>
            {
                (
                    new List<ClassicMfuTest>(),
                    new List<Tuple<long, long, DateTime>>()
                ),
                (
                    new List<ClassicMfuTest>()
                    {
                        new ClassicMfuTest()
                        {
                            Id = new GlobalHistoryId(1)
                        }
                    },
                    new List<Tuple<long, long, DateTime>>()
                ),
                (
                    new List<ClassicMfuTest>()
                    {
                        new ClassicMfuTest()
                        {
                            Id = new GlobalHistoryId(1),
                            ToleranceClassUnit1 = CreateToleranceClass.WithId(10),
                            Timestamp = new DateTime(2000,1,1)
                        }
                    },
                    new List<Tuple<long, long, DateTime>>()
                    {
                        new Tuple<long, long, DateTime>(1, 10, new DateTime(2000,1,1))
                    }
                ),
                (
                    new List<ClassicMfuTest>()
                    {
                        new ClassicMfuTest()
                        {
                            Id = new GlobalHistoryId(99),
                            ToleranceClassUnit2 = CreateToleranceClass.WithId(33),
                            Timestamp = new DateTime(2020,9,30)
                        }
                    },
                    new List<Tuple<long, long, DateTime>>()
                    {
                        new Tuple<long, long, DateTime>(99, 33, new DateTime(2020,9,30))
                    }
                ),
                (
                    new List<ClassicMfuTest>()
                    {
                        new ClassicMfuTest()
                        {
                            Id = new GlobalHistoryId(88),
                            ToleranceClassUnit1 = CreateToleranceClass.WithId(55),
                            ToleranceClassUnit2 = CreateToleranceClass.WithId(66),
                            Timestamp = new DateTime(2020,12,31)
                        }
                    },
                    new List<Tuple<long, long, DateTime>>()
                    {
                        new Tuple<long, long, DateTime>(88, 55, new DateTime(2020,12,31)),
                        new Tuple<long, long, DateTime>(88, 66, new DateTime(2020,12,31))
                    }
                )
            };

        [TestCaseSource(nameof(LoadValuesFromClassicMfuHeaderCallsGetClassIdFromHistoryForIdsWithCorrectParameterData))]
        public void LoadValuesFromClassicMfuHeaderCallsGetClassIdFromHistoryForIdsWithCorrectParameter((List<ClassicMfuTest> mfuTests, List<Tuple<long, long, DateTime>> expectedList) data)
        {
            var environment = new Environment();

            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicMfuHeaderReturnValue = new List<ClassicMfuTestValue>();

            environment.useCase.LoadValuesForClassicMfuHeader(CreateLocation.Anonymous(), CreateTool.Anonymous(), data.mfuTests, environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);

            Assert.AreEqual(data.expectedList, environment.mocks.toleranceClassDataAccessMock.GetToleranceClassFromHistoryForIdsParameter);
        }

        [TestCase(1)]
        [TestCase(5)]
        public void LoadValuesFromClassicMfuHeaderReturnsCorrectTestReferences(long globalHistoryId)
        {
            var environment = new Environment();

            var classicMfuHeader = new List<ClassicMfuTestValue>() { new ClassicMfuTestValue() { Id = new GlobalHistoryId(globalHistoryId) } };
            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicMfuHeaderReturnValue = classicMfuHeader;

            var location = CreateLocation.IdOnly(1);
            var tool = CreateTool.WithId(1);
            var classicMfuTest = new List<ClassicMfuTest>() { new ClassicMfuTest() { Id = new GlobalHistoryId(globalHistoryId) } };

            environment.useCase.LoadValuesForClassicMfuHeader(location, tool, classicMfuTest, environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);

            Assert.AreEqual(classicMfuTest.First().Id.ToLong(), classicMfuTest.First().TestValues.First().Id.ToLong());
            Assert.AreEqual(classicMfuTest.First().TestValues.First().MfuTest.Id.ToLong(), classicMfuTest.First().Id.ToLong());

            Assert.IsFalse(environment.mocks.ClassicTestError.ShowErrorMessageCalled);
        }

        [TestCaseSource(nameof(LoadValuesForClassicHeaderSetsToleranceClassCorrectData))]
        public void LoadValuesForClassicMfuHeaderSetsToleranceClassCorrect(List<(GlobalHistoryId gid, ToleranceClass toleranceClass)> datas)
        {
            var environment = new Environment();
            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicMfuHeaderReturnValue = new List<ClassicMfuTestValue>();
            environment.mocks.toleranceClassDataAccessMock.GetToleranceClassFromHistoryForIdsReturnValue = new Dictionary<long, ToleranceClass>();
            var classicMfuTest = new List<ClassicMfuTest>();

            foreach (var data in datas)
            {
                environment.mocks.toleranceClassDataAccessMock.GetToleranceClassFromHistoryForIdsReturnValue.Add(data.gid.ToLong(), data.toleranceClass);
                classicMfuTest.Add(new ClassicMfuTest()
                {
                    Id = data.gid,
                    ToleranceClassUnit1 = CreateToleranceClass.WithId(data.toleranceClass.Id.ToLong()),
                    ToleranceClassUnit2 = CreateToleranceClass.WithId(data.toleranceClass.Id.ToLong())
                });
            }

            environment.useCase.LoadValuesForClassicMfuHeader(CreateLocation.Anonymous(), CreateTool.Anonymous(), classicMfuTest, environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);

            CollectionAssert.AreEqual(datas.Select(x => x.toleranceClass).ToList(), classicMfuTest.Select(x => x.ToleranceClassUnit1).ToList());
            CollectionAssert.AreEqual(datas.Select(x => x.toleranceClass).ToList(), classicMfuTest.Select(x => x.ToleranceClassUnit2).ToList());
        }

        [Test]
        public void LoadValuesForClassicMfuHeaderNotSetsToleranceClassIfNoHistoryFound()
        {
            var environment = new Environment();
            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicMfuHeaderReturnValue = new List<ClassicMfuTestValue>();
            environment.mocks.toleranceClassDataAccessMock.GetToleranceClassFromHistoryForIdsReturnValue = new Dictionary<long, ToleranceClass>();

            var toleranceClassUnit1 = CreateToleranceClass.WithId(1);
            var toleranceClassUnit2 = CreateToleranceClass.WithId(2);
            var classicMfuTest = new List<ClassicMfuTest>()
            {
                new ClassicMfuTest()
                {
                    Id = new GlobalHistoryId(1),
                    ToleranceClassUnit1 = toleranceClassUnit1,
                    ToleranceClassUnit2 = toleranceClassUnit2
                }
            };

            environment.useCase.LoadValuesForClassicMfuHeader(CreateLocation.Anonymous(), CreateTool.Anonymous(), classicMfuTest, environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);

            Assert.AreSame(toleranceClassUnit1, classicMfuTest.First().ToleranceClassUnit1);
            Assert.AreSame(toleranceClassUnit2, classicMfuTest.First().ToleranceClassUnit2);
        }

        [Test]
        public void LoadValuesFromClassicMfuHeaderWithErrorCallsShowErrorMessage()
        {
            var environment = new Environment();
            environment.mocks.classicTestDataAccessMock.ThrowError = true;

            environment.useCase.LoadValuesForClassicMfuHeader(CreateLocation.Anonymous(), CreateTool.Anonymous(), new List<ClassicMfuTest>(), environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);
            Assert.IsTrue(environment.mocks.classicTestDataAccessMock.GetValuesFromClassicMfuHeaderCalled);
            Assert.IsTrue(environment.mocks.ClassicTestError.ShowErrorMessageCalled);
        }

        [Test]
        public void LoadChkHeaderFromToolWithNoToolCallsShowChkHeaderForSelectedToolWithEmptyTests()
        {
            var environment = new Environment();
            environment.mocks.classicTestDataAccessMock.GetClassicChkHeaderFromToolReturnValue = new List<ClassicChkTest>(){new ClassicChkTest()};
            environment.useCase.LoadChkHeaderFromTool(null, null);
            Assert.AreEqual(0, environment.mocks.classicTestGuiMock.ShowChkHeaderForSelectedToolCalledParameterHeader.Count);
        }

        [Test] 
        public void LoadMfuHeaderFromToolWithNoToolCallsShowMfuHeaderForSelectedToolWithEmptyTests()
        {
            var environment = new Environment();
            environment.mocks.classicTestDataAccessMock.GetClassicMfuHeaderFromToolReturnValue = new List<ClassicMfuTest>() { new ClassicMfuTest() };
            environment.useCase.LoadMfuHeaderFromTool(null, null);
            Assert.AreEqual(0, environment.mocks.classicTestGuiMock.ShowMfuHeaderForSelectedToolCalledParameterHeader.Count);
        }

        [Test]
        public void LoadToolsFromLocationTestsWithNoLocationCallsShowToolsForSelectedLocationWithNull()
        {
            var environment = new Environment();
            environment.useCase.LoadToolsFromLocationTests(null, null);
            Assert.IsNull(environment.mocks.classicTestGuiMock.ShowToolsForSelectedLocationCalledParameter);
        }

        [TestCase(1)]
        [TestCase(13)]
        public void LoadProcessHeaderFromLocationCallsDataAccessGetClassicProcessHeaderFromLocation(long locationId)
        {
            var environment = new Environment();
            environment.useCase.LoadProcessHeaderFromLocation(CreateLocation.IdOnly(locationId),null);
            Assert.AreEqual(locationId, environment.mocks.classicTestDataAccessMock.GetClassicProcessHeaderFromLocationLocationId.ToLong());
        }

        [Test]
        public void LoadProcessHeaderFromLocationCallsGuiWithCorrectData()
        {
            var environment = new Environment();
            var tests = new List<ClassicProcessTest>();
            environment.mocks.classicTestDataAccessMock.GetClassicProcessHeaderFromLocationReturnValue = tests;
            environment.useCase.LoadProcessHeaderFromLocation(CreateLocation.Anonymous(), null);
            Assert.AreSame(tests, environment.mocks.classicTestGuiMock.ShowProcessHeaderForSelectedLocationHeader);
        }

        [Test]
        public void LoadProcessHeaderFromLocationWithNoLocationCallsGuiWithNoTests()
        {
            var environment = new Environment();
            environment.mocks.classicTestDataAccessMock.GetClassicProcessHeaderFromLocationReturnValue = new List<ClassicProcessTest>()
            {
                CreateClassicProcessTest.Randomized(345)
            };

            environment.useCase.LoadProcessHeaderFromLocation(null, null);
            Assert.AreEqual(0, environment.mocks.classicTestGuiMock.ShowProcessHeaderForSelectedLocationHeader.Count);
        }

        [Test]
        public void LoadProcessHeaderFromLocationWithErrorCallsShowErrorMessage()
        {
            var environment = new Environment();
            environment.mocks.classicTestDataAccessMock.GetClassicProcessHeaderFromLocationThrowsError = true;

            environment.useCase.LoadProcessHeaderFromLocation(CreateLocation.Anonymous(), environment.mocks.ClassicTestError);
            Assert.IsTrue(environment.mocks.ClassicTestError.ShowErrorMessageCalled);
        }

        [Test]
        public void LoadValuesForClassicProcessHeaderCallsDataAccessGetValuesFromClassicProcessHeader()
        {
            var environment = new Environment();
            var tests = new List<ClassicProcessTest>();
            environment.useCase.LoadValuesForClassicProcessHeader(CreateLocation.Anonymous(), tests, true,
                environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);
            Assert.AreSame(tests, environment.mocks.classicTestDataAccessMock.GetValuesFromClassicProcessHeaderParameter);
        }

        private static IEnumerable<(Location, int)>
            LoadValuesFromClassicProcessHeaderCallsLoadTreePathForLocationsCorrectData = new List<(Location, int)>()
            {
                (
                    CreateLocation.IdOnly(1), 1
                ),
                (
                    CreateLocation.WithIdAndLocationTreePath(1, new List<LocationDirectory>(){CreateLocationDirectory.Anonymous()}), 0
                )
            };


        [TestCaseSource(nameof(LoadValuesFromClassicProcessHeaderCallsLoadTreePathForLocationsCorrectData))]
        public void LoadValuesFromClassicProcessHeaderCallsLoadTreePathForLocationsCorrect((Location location, int count) data)
        {
            var environment = new Environment();

            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicProcessHeaderReturnValue = new List<ClassicProcessTestValue>();

            environment.useCase.LoadValuesForClassicProcessHeader(data.location, new List<ClassicProcessTest>() { new ClassicProcessTest() },true, environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);

            Assert.AreEqual(data.count, environment.mocks.locationuseCase.LoadTreePathForLocationsParameter?.Count ?? 0);

            if (data.count > 0)
            {
                Assert.AreEqual(data.location.Id.ToLong(), environment.mocks.locationuseCase.LoadTreePathForLocationsParameter?.First().Id.ToLong());
            }
        }

        static IEnumerable<List<long>> LoadValuesFromClassicProcessHeaderReturnsCorrectTestReferencesData = new List<List<long>>()
        {
            new List<long>()
            {
                1,2,3
            },
            new List<long>()
            {
                55,7
            }
        };

        [TestCaseSource(nameof(LoadValuesFromClassicProcessHeaderReturnsCorrectTestReferencesData))]
        public void LoadValuesFromClassicProcessHeaderReturnsCorrectTestReferences(List<long> globalHistoryIds)
        {
            var environment = new Environment();

            var classicProcessHeader = new List<ClassicProcessTestValue>();
            var classicProcessTests = new List<ClassicProcessTest>();
            foreach (var id in globalHistoryIds)
            {
                classicProcessHeader.Add(new ClassicProcessTestValue()
                {
                    Id = new GlobalHistoryId(id)
                });

                classicProcessTests.Add(new ClassicProcessTest() { Id = new GlobalHistoryId(id)});
            }
            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicProcessHeaderReturnValue = classicProcessHeader;
            
            environment.useCase.LoadValuesForClassicProcessHeader(CreateLocation.IdOnly(1), classicProcessTests, false, environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);

            var i = 0;
            foreach (var test in classicProcessTests)
            {
                Assert.AreSame(test, classicProcessHeader[i].ProcessTest);
                Assert.AreSame(test.TestValues.First(), classicProcessHeader[i]);
                i++;
            }
        }

        [TestCase(true)]
        [TestCase(false)]
        public void LoadValuesForClassicProcessHeaderCallsShowValuesForClassicProcessHeader(bool isPfu)
        {
            var environment = new Environment();
            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicProcessHeaderReturnValue = new List<ClassicProcessTestValue>();
            var tests = new List<ClassicProcessTest>();
            var location = CreateLocation.Anonymous();
            environment.useCase.LoadValuesForClassicProcessHeader(location, tests, isPfu,
                environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);
            Assert.AreSame(location, environment.mocks.ShowEvaluation.ShowValuesForClassicProcessHeaderLocation);
            Assert.AreSame(tests, environment.mocks.ShowEvaluation.ShowValuesForClassicProcessHeaderTests);
            Assert.AreEqual(isPfu, environment.mocks.ShowEvaluation.ShowValuesForClassicProcessHeaderIsPfu);
        }

        [Test]
        public void LoadValuesFromClassicProcessHeaderWithErrorCallsShowErrorMessage()
        {
            var environment = new Environment();
            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicProcessHeaderThrowsError = true;
            environment.useCase.LoadValuesForClassicProcessHeader(CreateLocation.Anonymous(), new List<ClassicProcessTest>(), false, environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);            
            Assert.IsTrue(environment.mocks.ClassicTestError.ShowErrorMessageCalled);
        }

        private static IEnumerable<(List<ClassicProcessTest>, List<Tuple<long, long, DateTime>>)>
           LoadValuesFromClassicProcessHeaderCallsGetClassIdFromHistoryForIdsWithCorrectParameterData = new List<(List<ClassicProcessTest>, List<Tuple<long, long, DateTime>>)>
           {
                (
                    new List<ClassicProcessTest>(),
                    new List<Tuple<long, long, DateTime>>()
                ),
                (
                    new List<ClassicProcessTest>()
                    {
                        new ClassicProcessTest()
                        {
                            Id = new GlobalHistoryId(1)
                        }
                    },
                    new List<Tuple<long, long, DateTime>>()
                ),
                (
                    new List<ClassicProcessTest>()
                    {
                        new ClassicProcessTest()
                        {
                            Id = new GlobalHistoryId(1),
                            ToleranceClassUnit1 = CreateToleranceClass.WithId(10),
                            Timestamp = new DateTime(2000,1,1)
                        }
                    },
                    new List<Tuple<long, long, DateTime>>()
                    {
                        new Tuple<long, long, DateTime>(1, 10, new DateTime(2000,1,1))
                    }
                ),
                (
                    new List<ClassicProcessTest>()
                    {
                        new ClassicProcessTest()
                        {
                            Id = new GlobalHistoryId(99),
                            ToleranceClassUnit2 = CreateToleranceClass.WithId(33),
                            Timestamp = new DateTime(2020,9,30)
                        }
                    },
                    new List<Tuple<long, long, DateTime>>()
                    {
                        new Tuple<long, long, DateTime>(99, 33, new DateTime(2020,9,30))
                    }
                ),
                (
                    new List<ClassicProcessTest>()
                    {
                        new ClassicProcessTest()
                        {
                            Id = new GlobalHistoryId(88),
                            ToleranceClassUnit1 = CreateToleranceClass.WithId(55),
                            ToleranceClassUnit2 = CreateToleranceClass.WithId(66),
                            Timestamp = new DateTime(2020,12,31)
                        }
                    },
                    new List<Tuple<long, long, DateTime>>()
                    {
                        new Tuple<long, long, DateTime>(88, 55, new DateTime(2020,12,31)),
                        new Tuple<long, long, DateTime>(88, 66, new DateTime(2020,12,31))
                    }
                ),

           };

        [TestCaseSource(nameof(LoadValuesFromClassicProcessHeaderCallsGetClassIdFromHistoryForIdsWithCorrectParameterData))]
        public void LoadValuesFromClassicProcessHeaderCallsGetClassIdFromHistoryForIdsWithCorrectParameter((List<ClassicProcessTest> chkTests, List<Tuple<long, long, DateTime>> expectedList) data)
        {
            var environment = new Environment();

            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicProcessHeaderReturnValue = new List<ClassicProcessTestValue>();

            environment.useCase.LoadValuesForClassicProcessHeader(CreateLocation.Anonymous(), data.chkTests, true, environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);

            Assert.AreEqual(data.expectedList, environment.mocks.toleranceClassDataAccessMock.GetToleranceClassFromHistoryForIdsParameter);
        }

        [TestCaseSource(nameof(LoadValuesForClassicHeaderSetsToleranceClassCorrectData))]
        public void LoadValuesForClassicProcessHeaderSetsToleranceClassCorrect(List<(GlobalHistoryId gid, ToleranceClass toleranceClass)> datas)
        {
            var environment = new Environment();
            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicProcessHeaderReturnValue = new List<ClassicProcessTestValue>();
            environment.mocks.toleranceClassDataAccessMock.GetToleranceClassFromHistoryForIdsReturnValue = new Dictionary<long, ToleranceClass>();
            var classicProcessTest = new List<ClassicProcessTest>();

            foreach (var data in datas)
            {
                environment.mocks.toleranceClassDataAccessMock.GetToleranceClassFromHistoryForIdsReturnValue.Add(data.gid.ToLong(), data.toleranceClass);
                classicProcessTest.Add(new ClassicProcessTest()
                {
                    Id = data.gid,
                    ToleranceClassUnit1 = CreateToleranceClass.WithId(data.toleranceClass.Id.ToLong()),
                    ToleranceClassUnit2 = CreateToleranceClass.WithId(data.toleranceClass.Id.ToLong())
                });
            }

            environment.useCase.LoadValuesForClassicProcessHeader(CreateLocation.Anonymous(),classicProcessTest, true, environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);

            CollectionAssert.AreEqual(datas.Select(x => x.toleranceClass).ToList(), classicProcessTest.Select(x => x.ToleranceClassUnit1).ToList());
            CollectionAssert.AreEqual(datas.Select(x => x.toleranceClass).ToList(), classicProcessTest.Select(x => x.ToleranceClassUnit2).ToList());
        }

        [Test]
        public void LoadValuesForClassicProcessHeaderNotSetsToleranceClassIfNoHistoryFound()
        {
            var environment = new Environment();
            environment.mocks.classicTestDataAccessMock.GetValuesFromClassicProcessHeaderReturnValue = new List<ClassicProcessTestValue>();
            environment.mocks.toleranceClassDataAccessMock.GetToleranceClassFromHistoryForIdsReturnValue = new Dictionary<long, ToleranceClass>();

            var toleranceClassUnit1 = CreateToleranceClass.WithId(1);
            var toleranceClassUnit2 = CreateToleranceClass.WithId(2);
            var classicProcessTest = new List<ClassicProcessTest>()
            {
                new ClassicProcessTest()
                {
                    Id = new GlobalHistoryId(1),
                    ToleranceClassUnit1 = toleranceClassUnit1,
                    ToleranceClassUnit2 = toleranceClassUnit2
                }
            };

            environment.useCase.LoadValuesForClassicProcessHeader(CreateLocation.Anonymous(), classicProcessTest, true, environment.mocks.ClassicTestError, environment.mocks.ShowEvaluation);

            Assert.AreSame(toleranceClassUnit1, classicProcessTest.First().ToleranceClassUnit1);
            Assert.AreSame(toleranceClassUnit2, classicProcessTest.First().ToleranceClassUnit2);
        }

        private static IEnumerable<List<(GlobalHistoryId, ToleranceClass)>>
            LoadValuesForClassicHeaderSetsToleranceClassCorrectData =
                new List<List<(GlobalHistoryId, ToleranceClass)>>()
                {
                    new List<(GlobalHistoryId, ToleranceClass)>()
                    {
                        (
                            new GlobalHistoryId(99),
                            CreateToleranceClass.Parametrized(1, "abcde", true, 1, 0)
                        ),
                        (
                            new GlobalHistoryId(44),
                            CreateToleranceClass.Parametrized(14, "xxxxx", false, 1, 0)
                        ),
                        (
                            new GlobalHistoryId(88),
                            CreateToleranceClass.Parametrized(67, "567td xxx", false, 99, 1000)
                        )
                    },
                    new List<(GlobalHistoryId, ToleranceClass)>()
                    {
                        (
                            new GlobalHistoryId(4),
                            CreateToleranceClass.Parametrized(65, "Freie Eingabe", false, 1, 0)
                        )
                    }
                };

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    classicTestGuiMock = new ClassicTestGuiMock();
                    classicTestDataAccessMock = new ClassicTestDataAccessMock();
                    locationuseCase = new LocationUseCaseMock(null);
                    toleranceClassDataAccessMock = new ToleranceClassDataAccessMock();
                    ClassicTestError = new ClassicTestErrorMock();
                    ShowEvaluation = new ShowEvaluationMock();
                }

                public ClassicTestGuiMock classicTestGuiMock;
                public ClassicTestDataAccessMock classicTestDataAccessMock;
                public LocationUseCaseMock locationuseCase;
                public ToleranceClassDataAccessMock toleranceClassDataAccessMock;
                public ClassicTestErrorMock ClassicTestError;
                public ShowEvaluationMock ShowEvaluation;
            }

            public Environment()
            {
                mocks = new Mocks();
                useCase = new ClassicTestUseCase(mocks.classicTestGuiMock, mocks.classicTestDataAccessMock, mocks.locationuseCase, mocks.toleranceClassDataAccessMock);
            }

            public ClassicTestUseCase useCase;
            public Mocks mocks;
        }
    }
}

