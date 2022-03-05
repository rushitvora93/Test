using BasicTypes;
using ClassicTestService;
using Client.TestHelper.Mock;
using Core.Entities;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using TestHelper.Mock;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using DtoTypes;
using TestHelper.Checker;
using ClassicChkTest = Core.Entities.ClassicChkTest;
using ClassicMfuTest = Core.Entities.ClassicMfuTest;
using DateTime = BasicTypes.DateTime;
using Tool = Core.Entities.Tool;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    public class ClassicTestClientMock : IClassicTestClient
    {
        public Long GetToolsFromLocationTestsParameter { get; set; }
        public ListOfClassicTestPowToolData GetToolsFromLocationTestsReturnValue { get; set; } = new ListOfClassicTestPowToolData();
        public ListOfClassicMfuTest GetClassicMfuHeaderFromToolReturnValue { get; set; } = new ListOfClassicMfuTest();
        public GetClassicHeaderFromToolRequest GetClassicMfuHeaderFromToolParameter { get; set; }
        public GetClassicHeaderFromToolRequest GetClassicChkHeaderFromToolParameter { get; set; }
        public ListOfClassicChkTest GetClassicChkHeaderFromToolReturnValue { get; set; } = new ListOfClassicChkTest();
        public ListOfClassicChkTestValue GetValuesFromClassicChkHeaderReturnValue { get; set; } = new ListOfClassicChkTestValue();
        public ListOfLongs GetValuesFromClassicChkHeaderParameter { get; set; }
        public ListOfClassicMfuTestValue GetValuesFromClassicMfuHeaderReturnValue { get; set; } = new ListOfClassicMfuTestValue();
        public ListOfLongs GetValuesFromClassicMfuHeaderParameter { get; set; }
        public ListOfClassicProcessTest GetClassicProcessHeaderFromLocationReturnValue { get; set; } = new ListOfClassicProcessTest();
        public Long GetClassicProcessHeaderFromLocationLocationId { get; set; }
        public ListOfClassicProcessTestValue GetValuesFromClassicProcessHeaderReturnValue { get; set; } = new ListOfClassicProcessTestValue();
        public ListOfLongs GetValuesFromClassicProcessHeaderGlobalHistoryIds { get; set; }

        public ListOfClassicTestPowToolData GetToolsFromLocationTests(Long locationId)
        {
            GetToolsFromLocationTestsParameter = locationId;
            return GetToolsFromLocationTestsReturnValue;
        }

        public ListOfClassicMfuTest GetClassicMfuHeaderFromTool(GetClassicHeaderFromToolRequest request)
        {
            GetClassicMfuHeaderFromToolParameter = request;
            return GetClassicMfuHeaderFromToolReturnValue;
        }

        public ListOfClassicChkTest GetClassicChkHeaderFromTool(GetClassicHeaderFromToolRequest request)
        {
            GetClassicChkHeaderFromToolParameter = request;
            return GetClassicChkHeaderFromToolReturnValue;
        }

        public ListOfClassicChkTestValue GetValuesFromClassicChkHeader(ListOfLongs globalHistoryIds)
        {
            GetValuesFromClassicChkHeaderParameter = globalHistoryIds;
            return GetValuesFromClassicChkHeaderReturnValue;
        }

        public ListOfClassicMfuTestValue GetValuesFromClassicMfuHeader(ListOfLongs globalHistoryIds)
        {
            GetValuesFromClassicMfuHeaderParameter = globalHistoryIds;
            return GetValuesFromClassicMfuHeaderReturnValue;
        }

        public ListOfClassicProcessTest GetClassicProcessHeaderFromLocation(Long locationId)
        {
            GetClassicProcessHeaderFromLocationLocationId = locationId;
            return GetClassicProcessHeaderFromLocationReturnValue;
        }

        public ListOfClassicProcessTestValue GetValuesFromClassicProcessHeader(ListOfLongs globalHistoryIds)
        {
            GetValuesFromClassicProcessHeaderGlobalHistoryIds = globalHistoryIds;
            return GetValuesFromClassicProcessHeaderReturnValue;
        }
    }

    class ClassicTestDataAccessTest
    {
        [TestCase(1)]
        [TestCase(19)]
        public void LoadToolsFromLocationTestsCallsClient(long locationId)
        {
            var environment = new Environment();

            environment.dataAccess.LoadToolsFromLocationTests(new LocationId(locationId));

            Assert.AreEqual(locationId, environment.mocks.classicTestClient.GetToolsFromLocationTestsParameter.Value);
        }


        private static IEnumerable<ListOfClassicTestPowToolData> ClassicTestPowToolData =
            new List<ListOfClassicTestPowToolData>()
            {
                new ListOfClassicTestPowToolData()
                {
                    ClassicTestPowToolDatas =
                    {
                        new ClassicTestPowToolData()
                        {
                            Tool = DtoFactory.CreateToolDto(1, "1234", "0931", false, 
                                "abc", "F1", "F2", "F3", "a",
                                new DtoTypes.Status(){Id = 1, Description="S", Alive = true},
                                new DtoTypes.HelperTableEntity(){ListId = 1,Value="C",Alive = true,NodeId = 5},
                                new DtoTypes.HelperTableEntity(){ListId = 11, Value="K",Alive = false, NodeId = 15},
                                DtoFactory.CreateToolModelDtoRandomized(46651)),
                            IsToolAssignmentActive = true,
                            FirstTest = new NullableDateTime(){ Value = new DateTime(){Ticks = new System.DateTime(2012,1,1).Ticks}},
                            LastTest = new NullableDateTime(){ Value = new DateTime(){Ticks = new System.DateTime(2012,11,1).Ticks}},
                        },
                        new ClassicTestPowToolData()
                        {
                            Tool = DtoFactory.CreateToolDto(12, "33333", "456", true,
                                    "abc", "F1", "F2", "F3", "a",
                                new DtoTypes.Status(){Id = 1, Description="S", Alive = true},
                                new DtoTypes.HelperTableEntity(){ListId = 1,Value="C",Alive = true,NodeId = 5},
                                new DtoTypes.HelperTableEntity(){ListId = 11, Value="K",Alive = false, NodeId = 15},
                                DtoFactory.CreateToolModelDtoRandomized(8976)),
                            IsToolAssignmentActive = false,
                            FirstTest = new NullableDateTime(){ Value = new DateTime(){Ticks = new System.DateTime(2018,1,1).Ticks}},
                            LastTest = new NullableDateTime(){ Value = new DateTime(){Ticks = new System.DateTime(2017,6,17).Ticks}}
                        }
                    }
                },
                new ListOfClassicTestPowToolData()
                {
                    ClassicTestPowToolDatas =
                    {
                        new ClassicTestPowToolData()
                        {
                            Tool = DtoFactory.CreateToolDto(18, "1234", "0931", false,
                                    "abc", "F1", "F2", "F3", "a",
                                new DtoTypes.Status(){Id = 1, Description="S", Alive = true},
                                new DtoTypes.HelperTableEntity(){ListId = 1,Value="C",Alive = true,NodeId = 5},
                                new DtoTypes.HelperTableEntity(){ListId = 11, Value="K",Alive = false, NodeId = 15},
                                DtoFactory.CreateToolModelDtoRandomized(7689)),
                            IsToolAssignmentActive = true,
                            FirstTest = new NullableDateTime(){ Value = new DateTime(){Ticks = new System.DateTime(2021,11,14).Ticks}},
                            LastTest = new NullableDateTime(){ Value = new DateTime(){Ticks = new System.DateTime(2019,1,1).Ticks}},
                        }
                    }
                }
            };

        [TestCaseSource(nameof(ClassicTestPowToolData))]
        public void LoadToolsFromLocationTestReturnsCorrectValues(ListOfClassicTestPowToolData datas)
        {
            var environment = new Environment();

            environment.mocks.classicTestClient.GetToolsFromLocationTestsReturnValue = datas;

            var dict = environment.dataAccess.LoadToolsFromLocationTests(new LocationId(12));
            var returnedTools = new List<Tool>();
            foreach (var item in dict)
            {
                returnedTools.Add(item.Key);
            }

            var comparer = new Func<DtoTypes.Tool, Tool, bool>((dto, entity) => EqualityChecker.CompareToolWithToolDto(entity, dto));
            CheckerFunctions.CollectionAssertAreEquivalent(datas.ClassicTestPowToolDatas.Select(x => x.Tool).ToList(), returnedTools, comparer);

            var comparerDate = new Func<NullableDateTime, System.DateTime?, bool>((dto, entity) => EqualityChecker.ArePrimitiveDateTimeAndDtoEqual(entity.Value, dto.Value));
            CheckerFunctions.CollectionAssertAreEquivalent(datas.ClassicTestPowToolDatas.Select(x => x.FirstTest).ToList(), dict.Select(x => x.Value.firsttest).ToList(), comparerDate);
            CheckerFunctions.CollectionAssertAreEquivalent(datas.ClassicTestPowToolDatas.Select(x => x.LastTest).ToList(), dict.Select(x => x.Value.lasttest).ToList(), comparerDate);

            Assert.AreEqual(datas.ClassicTestPowToolDatas.Select(x => x.IsToolAssignmentActive), dict.Values.Select(x => x.isToolAssignmentActive).ToList());
        }


        [TestCaseSource(nameof(ClassicTestPowToolData))]
        public void LoadToolsFromLocationTestsCallsConvertToLocalDateForClassicTestPowTools(ListOfClassicTestPowToolData datas)
        {
            var environment = new Environment();
            environment.mocks.classicTestClient.GetToolsFromLocationTestsReturnValue = datas;

            environment.dataAccess.LoadToolsFromLocationTests(new LocationId(1));

            Assert.AreEqual(datas.ClassicTestPowToolDatas, environment.mocks.timeDataAccess.ConvertToLocalDateParameters);
        }

        [TestCase(1, 6)]
        [TestCase(19, 9)]
        public void GetClassicMfuHeaderFromToolCallsClient(long toolId, long locationId)
        {
            var environment = new Environment();

            environment.dataAccess.GetClassicMfuHeaderFromTool(new ToolId(toolId), new LocationId(locationId));

            Assert.AreEqual(toolId, environment.mocks.classicTestClient.GetClassicMfuHeaderFromToolParameter.PowToolId);
            Assert.AreEqual(locationId, environment.mocks.classicTestClient.GetClassicMfuHeaderFromToolParameter.LocationId.Value);
        }

        [Test]
        public void GetClassicMfuHeaderFromToolCallsClientWithoutLocationId()
        {
            var environment = new Environment();

            environment.dataAccess.GetClassicMfuHeaderFromTool(new ToolId(1), null);

            Assert.IsTrue(environment.mocks.classicTestClient.GetClassicMfuHeaderFromToolParameter.LocationId.IsNull);
        }

        private static IEnumerable<ListOfClassicMfuTest> GetClassicMfuHeaderFromToolReturnsCorrectValueData =
            new List<ListOfClassicMfuTest>()
            {
                new ListOfClassicMfuTest()
                {
                    ClassicMfuTests =
                    {
                        DtoFactory.CreateClassicMfuTest(1, 1, 2.0, 3.0, 4, 2, 1, 3, 3.3, 
                            4.4, 3, 1, "312313", 21, 1, 3,
                            1, 2, 2, 99, 1,  new System.DateTime(2020,12,1), 
                            DtoFactory.CreateTestEquipmentRandomized(324536),0,
                            DtoFactory.CreateClassicTestLocation(1,2), 1.6, 1.7),
                        DtoFactory.CreateClassicMfuTest(3, 0, 2.1, 3.2, 4, 1, 2, 5, 1.3,
                            4.4, 3, 1, "3132313", 21, 1, 3,
                            1, 2, 2, 99, 1, new System.DateTime(2020,12,1),
                            DtoFactory.CreateTestEquipmentRandomized(4565857),0,
                            DtoFactory.CreateClassicTestLocation(1,2), 1.9, 2.1)
                    }
                },
                new ListOfClassicMfuTest()
                {
                    ClassicMfuTests =
                    {
                        DtoFactory.CreateClassicMfuTest(3, 0, 2.1, 3.2, 4, 1, 2, 5, 1.3,
                            4.4, 3, 1, "312313", 21, 1, 3,
                            2, 1, 1, 9, 3,  new System.DateTime(2021,12,1), 
                            DtoFactory.CreateTestEquipmentRandomized(5678698),0,
                            DtoFactory.CreateClassicTestLocation(1,2),2.2, 2.3)
                    }
                }
            };

        [TestCaseSource(nameof(GetClassicMfuHeaderFromToolReturnsCorrectValueData))]
        public void GetClassicMfuHeaderFromToolReturnsCorrectValue(ListOfClassicMfuTest datas)
        {
            var environment = new Environment();
            environment.mocks.classicTestClient.GetClassicMfuHeaderFromToolReturnValue = datas;

            var result = environment.dataAccess.GetClassicMfuHeaderFromTool(new ToolId(1), new LocationId(1));

            var comparer = new Func<DtoTypes.ClassicMfuTest, ClassicMfuTest, bool>((dto, entity) => EqualityChecker.CompareClassicMfuTestDtoWithClassicMfuTest(entity, dto));
            CheckerFunctions.CollectionAssertAreEquivalent(datas.ClassicMfuTests, result, comparer);
        }

        [TestCaseSource(nameof(GetClassicMfuHeaderFromToolReturnsCorrectValueData))]
        public void GetClassicMfuHeaderFromToolCallsConvertToLocalDateForClassicMfuTests(ListOfClassicMfuTest datas)
        {
            var environment = new Environment();
            environment.mocks.classicTestClient.GetClassicMfuHeaderFromToolReturnValue = datas;

            environment.dataAccess.GetClassicMfuHeaderFromTool(new ToolId(1), new LocationId(1));

            Assert.AreEqual(datas.ClassicMfuTests, environment.mocks.timeDataAccess.ConvertToLocalDateParameters);
        }

        [TestCase(1, 6)]
        [TestCase(19, 9)]
        public void GetClassicChkHeaderFromToolCallsClient(long toolId, long locationId)
        {
            var environment = new Environment();

            environment.dataAccess.GetClassicChkHeaderFromTool(new ToolId(toolId), new LocationId(locationId));

            Assert.AreEqual(toolId, environment.mocks.classicTestClient.GetClassicChkHeaderFromToolParameter.PowToolId);
            Assert.AreEqual(locationId, environment.mocks.classicTestClient.GetClassicChkHeaderFromToolParameter.LocationId.Value);
        }

        [Test]
        public void GetClassicChkHeaderFromToolCallsClientWithoutLocationId()
        {
            var environment = new Environment();

            environment.dataAccess.GetClassicChkHeaderFromTool(new ToolId(1), null);

            Assert.IsTrue(environment.mocks.classicTestClient.GetClassicChkHeaderFromToolParameter.LocationId.IsNull);
        }

        private static IEnumerable<ListOfClassicChkTest> GetClassicChkHeaderFromToolReturnsCorrectValueData =
            new List<ListOfClassicChkTest>()
            {
                new ListOfClassicChkTest()
                {
                    ClassicChkTests =
                    {
                        DtoFactory.CreateClassicChkTest(1, 1, 2.0, 2, 1, 3, 3.3,
                            4.4, 3, 1, "312313", 21, 1, 3,
                            1, 2, 2, 99, 1,  new System.DateTime(2020,12,1),
                            DtoFactory.CreateTestEquipmentRandomized(869),0,
                            DtoFactory.CreateClassicTestLocation(1,2)),
                        DtoFactory.CreateClassicChkTest(3, 0, 2.1, 1, 2, 5, 1.3,
                            4.4, 3, 1, "3132313", 21, 1, 3,
                            1, 2, 2, 99, 1,  new System.DateTime(2020,12,1),
                            DtoFactory.CreateTestEquipmentRandomized(56786),0,
                            DtoFactory.CreateClassicTestLocation(1,2))
                    }
                },
                new ListOfClassicChkTest()
                {
                    ClassicChkTests =
                    {
                        DtoFactory.CreateClassicChkTest(3, 0, 2.1, 1, 2, 5, 1.3,
                            4.4, 3, 1, "312313", 21, 1, 3,
                            2, 1, 1, 9, 3,  new System.DateTime(2021,12,1),
                            DtoFactory.CreateTestEquipmentRandomized(75568),0,
                            DtoFactory.CreateClassicTestLocation(1,2))
                    }
                }
            };

        [TestCaseSource(nameof(GetClassicChkHeaderFromToolReturnsCorrectValueData))]
        public void GetClassicChkHeaderFromToolReturnsCorrectValue(ListOfClassicChkTest datas)
        {
            var environment = new Environment();
            environment.mocks.classicTestClient.GetClassicChkHeaderFromToolReturnValue = datas;

            var result = environment.dataAccess.GetClassicChkHeaderFromTool(new ToolId(1), new LocationId(1));

            var comparer = new Func<DtoTypes.ClassicChkTest, Core.Entities.ClassicChkTest, bool>((dto, entity) => EqualityChecker.CompareClassicChkTestDtoWithClassicChkTest(entity, dto));
            CheckerFunctions.CollectionAssertAreEquivalent(datas.ClassicChkTests, result, comparer);
        }

        [TestCaseSource(nameof(GetClassicChkHeaderFromToolReturnsCorrectValueData))]
        public void GetClassicChkHeaderFromToolCallsConvertToLocalDateForClassicChkTests(ListOfClassicChkTest datas)
        {
            var environment = new Environment();
            environment.mocks.classicTestClient.GetClassicChkHeaderFromToolReturnValue = datas;

            environment.dataAccess.GetClassicChkHeaderFromTool(new ToolId(1), new LocationId(1));

            Assert.AreEqual(datas.ClassicChkTests, environment.mocks.timeDataAccess.ConvertToLocalDateParameters);
        }


        static IEnumerable<List<long>> GetValuesFromClassicChkHeaderData = new List<List<long>>()
        {
            new List<long>() {1,7,4,7,2},
            new List<long>() {17,545,3,12}
        };

        [TestCaseSource(nameof(GetValuesFromClassicChkHeaderData))]
        public void GetValuesFromClassicChkHeaderCallsClient(List<long> ids)
        {
            var environment = new Environment();
            var classicTests = new List<ClassicChkTest>();
            foreach (var id in ids)
            {
                classicTests.Add(new ClassicChkTest() {Id = new GlobalHistoryId(id)});
            }

            environment.dataAccess.GetValuesFromClassicChkHeader(classicTests);

            Assert.AreEqual(ids.ToList(), environment.mocks.classicTestClient.GetValuesFromClassicChkHeaderParameter.Values.Select(x => x.Value).ToList());
        }

        private static IEnumerable<ListOfClassicChkTestValue> GetValuesFromClassicChkHeaderReturnsCorrectValueData =
            new List<ListOfClassicChkTestValue>()
            {
                new ListOfClassicChkTestValue()
                {
                    ClassicChkTestValues =
                    {
                        new DtoTypes.ClassicChkTestValue() {Id = 1, Position = 1, ValueUnit1 = 2.0, ValueUnit2 = 4.9},
                        new DtoTypes.ClassicChkTestValue() {Id = 31, Position = 41, ValueUnit1 = 1.0, ValueUnit2 = 34.9},
                    }
                },
                new ListOfClassicChkTestValue()
                {
                    ClassicChkTestValues =
                    {
                        new DtoTypes.ClassicChkTestValue() {Id = 14, Position = 61, ValueUnit1 = 24.0, ValueUnit2 = 45.9}
                    }
                }
            };

        [TestCaseSource(nameof(GetValuesFromClassicChkHeaderReturnsCorrectValueData))]
        public void GetValuesFromClassicChkHeaderReturnsCorrectValue(ListOfClassicChkTestValue datas)
        {
            var environment = new Environment();
            environment.mocks.classicTestClient.GetValuesFromClassicChkHeaderReturnValue = datas;

            var result = environment.dataAccess.GetValuesFromClassicChkHeader(new List<ClassicChkTest>());

            var comparer = new Func<DtoTypes.ClassicChkTestValue, Core.Entities.ClassicChkTestValue, bool>(
                (dto, entity) => 
                    dto.Id == entity.Id.ToLong() && 
                    dto.Position == entity.Position &&
                    dto.ValueUnit1 == entity.ValueUnit1 && 
                    dto.ValueUnit2 == entity.ValueUnit2);
            CheckerFunctions.CollectionAssertAreEquivalent(datas.ClassicChkTestValues, result, comparer);
        }

        static IEnumerable<List<long>> GetValuesFromClassicMfuHeaderData = new List<List<long>>()
        {
            new List<long>() {1,7,4,7,2},
            new List<long>() {17,545,3,12}
        };

        [TestCaseSource(nameof(GetValuesFromClassicMfuHeaderData))]
        public void GetValuesFromClassicMfuHeaderCallsClient(List<long> ids)
        {
            var environment = new Environment();
            var classicTests = new List<ClassicMfuTest>();
            foreach (var id in ids)
            {
                classicTests.Add(new ClassicMfuTest() { Id = new GlobalHistoryId(id) });
            }

            environment.dataAccess.GetValuesFromClassicMfuHeader(classicTests);

            Assert.AreEqual(ids.ToList(), environment.mocks.classicTestClient.GetValuesFromClassicMfuHeaderParameter.Values.Select(x => x.Value).ToList());
        }

        private static IEnumerable<ListOfClassicMfuTestValue> GetValuesFromClassicMfuHeaderReturnsCorrectValueData =
            new List<ListOfClassicMfuTestValue>()
            {
                new ListOfClassicMfuTestValue()
                {
                    ClassicMfuTestValues =
                    {
                        new DtoTypes.ClassicMfuTestValue() {Id = 1, Position = 1, ValueUnit1 = 2.0, ValueUnit2 = 4.9},
                        new DtoTypes.ClassicMfuTestValue() {Id = 31, Position = 41, ValueUnit1 = 1.0, ValueUnit2 = 34.9},
                    }
                },
                new ListOfClassicMfuTestValue()
                {
                    ClassicMfuTestValues =
                    {
                        new DtoTypes.ClassicMfuTestValue() {Id = 14, Position = 61, ValueUnit1 = 24.0, ValueUnit2 = 45.9}
                    }
                }
            };

        [TestCaseSource(nameof(GetValuesFromClassicMfuHeaderReturnsCorrectValueData))]
        public void GetValuesFromClassicMfuHeaderReturnsCorrectValue(ListOfClassicMfuTestValue datas)
        {
            var environment = new Environment();
            environment.mocks.classicTestClient.GetValuesFromClassicMfuHeaderReturnValue = datas;

            var result = environment.dataAccess.GetValuesFromClassicMfuHeader(new List<ClassicMfuTest>());

            var comparer = new Func<DtoTypes.ClassicMfuTestValue, Core.Entities.ClassicMfuTestValue, bool>(
                (dto, entity) =>
                    dto.Id == entity.Id.ToLong() &&
                    dto.Position == entity.Position &&
                    dto.ValueUnit1 == entity.ValueUnit1 &&
                    dto.ValueUnit2 == entity.ValueUnit2);
            CheckerFunctions.CollectionAssertAreEquivalent(datas.ClassicMfuTestValues, result, comparer);
        }

        [TestCase(1)]
        [TestCase(15)]
        public void GetClassicProcessHeaderFromLocationCallsClient(long locationId)
        {
            var environment = new Environment();
            environment.dataAccess.GetClassicProcessHeaderFromLocation(new LocationId(locationId));
            Assert.AreEqual(locationId,
                environment.mocks.classicTestClient.GetClassicProcessHeaderFromLocationLocationId.Value);
        }

        private static IEnumerable<ListOfClassicProcessTest> GetClassicProcessHeaderFromLocationReturnsCorrectValueData =
            new List<ListOfClassicProcessTest>()
            {
                new ListOfClassicProcessTest()
                {
                    ClassicProcessTests =
                    {
                        DtoFactory.CreateProcessTestRandomized(4355345),
                        DtoFactory.CreateProcessTestRandomized(34534521)
                    }
                },
                new ListOfClassicProcessTest()
                {
                    ClassicProcessTests =
                    {
                        DtoFactory.CreateProcessTestRandomized(5),
                    }
                }
            };

        [TestCaseSource(nameof(GetClassicProcessHeaderFromLocationReturnsCorrectValueData))]
        public void GetClassicProcessHeaderFromLocationReturnsCorrectValue(ListOfClassicProcessTest datas)
        {
            var environment = new Environment();
            environment.mocks.classicTestClient.GetClassicProcessHeaderFromLocationReturnValue = datas;

            var result = environment.dataAccess.GetClassicProcessHeaderFromLocation(new LocationId(4));

            var comparer = new Func<DtoTypes.ClassicProcessTest, Client.Core.Entities.ClassicProcessTest, bool>(
                (dto, entity) => EqualityChecker.CompareClassicProcessTestWithDto(entity, dto));
            CheckerFunctions.CollectionAssertAreEquivalent(datas.ClassicProcessTests, result, comparer);
        }

        [TestCaseSource(nameof(GetClassicProcessHeaderFromLocationReturnsCorrectValueData))]
        public void GetClassicProcessHeaderFromLocationReturnsCorrectValueCallsConvertToLocalDate(ListOfClassicProcessTest datas)
        {
            var environment = new Environment();
            environment.mocks.classicTestClient.GetClassicProcessHeaderFromLocationReturnValue = datas;

            environment.dataAccess.GetClassicProcessHeaderFromLocation(new LocationId(4));

            Assert.AreEqual(datas.ClassicProcessTests.Select(x => new System.DateTime(x.Timestamp.Ticks)), environment.mocks.timeDataAccess.ConvertToLocalParameters.ToList());
        }

        
        static IEnumerable<List<long>> GetValuesFromClassicProcessHeaderData = new List<List<long>>()
        {
            new List<long>() {1,7,4,7,2},
            new List<long>() {17,545,3,12}
        };

        [TestCaseSource(nameof(GetValuesFromClassicProcessHeaderData))]
        public void GetValuesFromClassicProcessHeaderCallsClient(List<long> ids)
        {
            var environment = new Environment();
            var classicTests = new List<Client.Core.Entities.ClassicProcessTest>();
            foreach (var id in ids)
            {
                classicTests.Add(new Client.Core.Entities.ClassicProcessTest() { Id = new GlobalHistoryId(id) });
            }

            environment.dataAccess.GetValuesFromClassicProcessHeader(classicTests);

            Assert.AreEqual(ids.ToList(), environment.mocks.classicTestClient.GetValuesFromClassicProcessHeaderGlobalHistoryIds.Values.Select(x => x.Value).ToList());
        }

        private static IEnumerable<ListOfClassicProcessTestValue> GetValuesFromClassicProcessHeaderReturnsCorrectValueData =
            new List<ListOfClassicProcessTestValue>()
            {
                new ListOfClassicProcessTestValue()
                {
                    ClassicProcessTestValues =
                    {
                        new DtoTypes.ClassicProcessTestValue() {Id = 1, Position = 1, ValueUnit1 = 2.0, ValueUnit2 = 4.9},
                        new DtoTypes.ClassicProcessTestValue() {Id = 31, Position = 41, ValueUnit1 = 1.0, ValueUnit2 = 34.9},
                    }
                },
                new ListOfClassicProcessTestValue()
                {
                    ClassicProcessTestValues =
                    {
                        new DtoTypes.ClassicProcessTestValue() {Id = 14, Position = 61, ValueUnit1 = 24.0, ValueUnit2 = 45.9}
                    }
                }
            };

        [TestCaseSource(nameof(GetValuesFromClassicProcessHeaderReturnsCorrectValueData))]
        public void GetValuesFromClassicProcessHeaderReturnsCorrectValue(ListOfClassicProcessTestValue datas)
        {
            var environment = new Environment();
            environment.mocks.classicTestClient.GetValuesFromClassicProcessHeaderReturnValue = datas;

            var result = environment.dataAccess.GetValuesFromClassicProcessHeader(new List<Client.Core.Entities.ClassicProcessTest>());

            var comparer = new Func<DtoTypes.ClassicProcessTestValue, Client.Core.Entities.ClassicProcessTestValue, bool>(
                (dto, entity) =>
                    dto.Id == entity.Id.ToLong() &&
                    dto.Position == entity.Position &&
                    dto.ValueUnit1 == entity.ValueUnit1 &&
                    dto.ValueUnit2 == entity.ValueUnit2);
            CheckerFunctions.CollectionAssertAreEquivalent(datas.ClassicProcessTestValues, result, comparer);
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    clientFactory = new ClientFactoryMock();
                    channelWrapper = new ChannelWrapperMock();
                    classicTestClient = new ClassicTestClientMock();
                    channelWrapper.GetClassicTestClientReturnValue = classicTestClient;
                    clientFactory.AuthenticationChannel = channelWrapper;
                    timeDataAccess = new TimeDataAccessMock();
                }
                public ClientFactoryMock clientFactory;
                public ChannelWrapperMock channelWrapper;
                public ClassicTestClientMock classicTestClient;
                public TimeDataAccessMock timeDataAccess;
            }

            public Environment()
            {
                mocks = new Mocks();
                dataAccess = new ClassicTestDataAccess(mocks.clientFactory, mocks.timeDataAccess);
            }

            public readonly Mocks mocks;
            public readonly ClassicTestDataAccess dataAccess;
        }
    }
}
