using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using ClassicTestService;
using Common.Types.Enums;
using Core.Enums;
using NUnit.Framework;
using Server.Core.Entities;
using Server.TestHelper.Factories;
using Server.UseCases.UseCases;
using TestHelper.Checker;
using ClassicChkTest = Server.Core.Entities.ClassicChkTest;
using ClassicMfuTest = Server.Core.Entities.ClassicMfuTest;
using DateTime = System.DateTime;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    public class ClassicTestUseCaseMock : IClassicTestUseCase
    {
        public List<ClassicChkTest> GetClassicChkHeaderFromToolReturnValue { get; set; } = new List<ClassicChkTest>();
        public long? GetClassicChkHeaderFromToolParameterLocationId { get; set; }
        public long GetClassicChkHeaderFromToolParameterPowToolId { get; set; }
        public List<ClassicMfuTest> GetClassicMfuHeaderFromToolReturnValue { get; set; } = new List<ClassicMfuTest>();
        public long? GetClassicMfuHeaderFromToolParameterLocationId { get; set; }
        public long GetClassicMfuHeaderFromToolParameterPowTool { get; set; }
        public List<ClassicChkTestValue> GetValuesFromClassicChkHeaderReturnValue { get; set; } = new List<ClassicChkTestValue>();
        public List<GlobalHistoryId> GetValuesFromClassicChkHeaderParameter { get; set; }
        public List<ClassicMfuTestValue> GetValuesFromClassicMfuHeaderReturnValue { get; set; } = new List<ClassicMfuTestValue>();
        public List<GlobalHistoryId> GetValuesFromClassicMfuHeaderParameter { get; set; }
        public List<ClassicProcessTestValue> GetValuesFromClassicProcessHeaderReturnValue { get; set; } = new List<ClassicProcessTestValue>();
        public List<GlobalHistoryId> GetValuesFromClassicProcessHeaderGlobalHistoryIds { get; set; }

        public Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)>
            GetToolsFromLocationTestsReturnValue { get; set; } =
            new Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)>();
        public LocationId GetToolsFromLocationTestsParameter { get; set; }

        public List<ClassicChkTest> GetClassicChkHeaderFromTool(long powToolId, long? locationId)
        {
            GetClassicChkHeaderFromToolParameterPowToolId = powToolId;
            GetClassicChkHeaderFromToolParameterLocationId = locationId;
            return GetClassicChkHeaderFromToolReturnValue;
        }

        public List<ClassicMfuTest> GetClassicMfuHeaderFromTool(long powToolId, long? locationId)
        {
            GetClassicMfuHeaderFromToolParameterPowTool = powToolId;
            GetClassicMfuHeaderFromToolParameterLocationId = locationId;
            return GetClassicMfuHeaderFromToolReturnValue;
        }

        public List<ClassicChkTestValue> GetValuesFromClassicChkHeader(List<GlobalHistoryId> ids)
        {
            GetValuesFromClassicChkHeaderParameter = ids;
            return GetValuesFromClassicChkHeaderReturnValue;
        }

        public List<ClassicMfuTestValue> GetValuesFromClassicMfuHeader(List<GlobalHistoryId> ids)
        {
            GetValuesFromClassicMfuHeaderParameter = ids;
            return GetValuesFromClassicMfuHeaderReturnValue;
        }

        public Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)> GetToolsFromLocationTests(LocationId locationId)
        {
            GetToolsFromLocationTestsParameter = locationId;
            return GetToolsFromLocationTestsReturnValue;
        }

        public List<ClassicProcessTest> GetClassicProcessHeaderFromLocationReturnValue { get; set; }
        public LocationId GetClassicProcessHeaderFromLocationParameter { get; set; }
        public List<ClassicProcessTest> GetClassicProcessHeaderFromLocation(LocationId locationId)
        {
            GetClassicProcessHeaderFromLocationParameter = locationId;
            return GetClassicProcessHeaderFromLocationReturnValue;
        }

        public List<ClassicProcessTestValue> GetValuesFromClassicProcessHeader(List<GlobalHistoryId> ids)
        {
            GetValuesFromClassicProcessHeaderGlobalHistoryIds = ids;
            return GetValuesFromClassicProcessHeaderReturnValue;
        }
    }

    class ClassicTestServiceTest
    {
        [TestCase(1, 34)]
        [TestCase(13, 4)]
        public void GetClassicChkHeaderFromToolCallsUseCase(long powToolId, long locationId)
        {
            var useCase = new ClassicTestUseCaseMock();
            var service = new NetworkView.Services.ClassicTestService(null, useCase);

            service.GetClassicChkHeaderFromTool(new GetClassicHeaderFromToolRequest()
            {
                PowToolId = powToolId,
                LocationId = new NullableLong() { IsNull = false, Value = locationId}
            }, null); 

            Assert.AreEqual(powToolId,useCase.GetClassicChkHeaderFromToolParameterPowToolId);
            Assert.AreEqual(locationId, useCase.GetClassicChkHeaderFromToolParameterLocationId.Value);
        }

        [Test]
        public void GetClassicChkHeaderFromToolWithNoLocationIdCallsUseCase()
        {
            var useCase = new ClassicTestUseCaseMock();
            var service = new NetworkView.Services.ClassicTestService(null, useCase);

            service.GetClassicChkHeaderFromTool(new GetClassicHeaderFromToolRequest()
            {
                PowToolId = 4,
                LocationId = new NullableLong() { IsNull = true }
            }, null);

            Assert.IsNull(useCase.GetClassicChkHeaderFromToolParameterLocationId);
        }

        private static IEnumerable<List<ClassicChkTest>> GetClassicChkHeaderFromToolsReturnsCorrectValueData =
            new List<List<ClassicChkTest>>()
            {
                new List<ClassicChkTest>()
                {
                    
                    CreateClassicTest.CreateClassicChkTest(1, 1, 2.0, MeaUnit.Deg, 1, MeaUnit.KN, 3.3,
                        4.4, 3, 1, "312313", 21, 1, 3,
                        4, 7, MeaUnit.Nm, 99, 1, new System.DateTime(2021,4,5), 
                        CreateTestEquipment.Randomized(3243647),0,
                        CreateClassicTest.CreateClassicTestLocation(1,2),5),
                    CreateClassicTest.CreateClassicChkTest(13, 14, 2.50, MeaUnit.N, 15, MeaUnit.KN, 3.35,
                        44.4, 34, 12, "4353", 3, 1, 3,
                        4, 5, MeaUnit.cm, 5, 6, new System.DateTime(2019,4,5),
                        CreateTestEquipment.Randomized(33257),0,
                        CreateClassicTest.CreateClassicTestLocation(15,24),6)

                },
                new List<ClassicChkTest>()
                {

                    CreateClassicTest.CreateClassicChkTest(135, 146, 2.450, MeaUnit.cm, 15, MeaUnit.KN, 3.35,
                        434.4, 134, 142, "43646", 3, 1, 3,
                        4, 5, MeaUnit.cm, 5, 6, new System.DateTime(2019,4,5),
                        CreateTestEquipment.Randomized(12546),0,
                        CreateClassicTest.CreateClassicTestLocation(145,246),6)

                }
            };

        [TestCaseSource(nameof(GetClassicChkHeaderFromToolsReturnsCorrectValueData))]
        public void GetClassicChkHeaderFromToolsReturnsCorrectValue(List<ClassicChkTest> classicTests)
        {
            var useCase = new ClassicTestUseCaseMock();
            var service = new NetworkView.Services.ClassicTestService(null, useCase);
            useCase.GetClassicChkHeaderFromToolReturnValue = classicTests;

            var result = service.GetClassicChkHeaderFromTool(new GetClassicHeaderFromToolRequest(), null);

            var comparer = new Func<ClassicChkTest, DtoTypes.ClassicChkTest, bool>(EqualityChecker.CompareClassicChkTestDtoWithClassicChkTest);

            CheckerFunctions.CollectionAssertAreEquivalent(classicTests, result.Result.ClassicChkTests, comparer);
        }

        [TestCase(1, 34)]
        [TestCase(13, 4)]
        public void GetClassicMfuHeaderFromToolCallsUseCase(long powToolId, long locationId)
        {
            var useCase = new ClassicTestUseCaseMock();
            var service = new NetworkView.Services.ClassicTestService(null, useCase);

            service.GetClassicMfuHeaderFromTool(new GetClassicHeaderFromToolRequest()
            {
                PowToolId = powToolId,
                LocationId = new NullableLong() { IsNull = false, Value = locationId }
            }, null);

            Assert.AreEqual(powToolId, useCase.GetClassicMfuHeaderFromToolParameterPowTool);
            Assert.AreEqual(locationId, useCase.GetClassicMfuHeaderFromToolParameterLocationId.Value);
        }

        [Test]
        public void GetClassicMfuHeaderFromToolWithNoLocationIdCallsUseCase()
        {
            var useCase = new ClassicTestUseCaseMock();
            var service = new NetworkView.Services.ClassicTestService(null, useCase);

            service.GetClassicMfuHeaderFromTool(new GetClassicHeaderFromToolRequest()
            {
                PowToolId = 4,
                LocationId = new NullableLong() { IsNull = true }
            }, null);

            Assert.IsNull(useCase.GetClassicMfuHeaderFromToolParameterLocationId);
        }

        private static IEnumerable<List<ClassicMfuTest>> GetClassicMfuHeaderFromToolsReturnsCorrectValueData =
            new List<List<ClassicMfuTest>>()
            {
                new List<ClassicMfuTest>()
                {

                    CreateClassicTest.CreateClassicMfuTest(1, 1, 2.0, MeaUnit.Deg, 1, MeaUnit.KN, 3.3,
                        4.4, 3, 1, "312313", 21, 1, 3,
                        4, 7, MeaUnit.Nm, 99, 1, new System.DateTime(2021,4,5), 4, 3,
                        CreateTestEquipment.Randomized(456890),0,
                        CreateClassicTest.CreateClassicTestLocation(1,2),8, 1.66, 1.55),
                    CreateClassicTest.CreateClassicMfuTest(13, 14, 2.50, MeaUnit.N, 15, MeaUnit.KN, 3.35,
                        44.4, 34, 12, "4353", 3, 1, 3,
                        4, 5, MeaUnit.cm, 5, 6, new System.DateTime(2019,4,5), 2,3,
                        CreateTestEquipment.Randomized(1345478),0,
                        CreateClassicTest.CreateClassicTestLocation(15,24),9, 1.55, 1.98)

                },
                new List<ClassicMfuTest>()
                {

                    CreateClassicTest.CreateClassicMfuTest(135, 146, 2.450, MeaUnit.cm, 15, MeaUnit.KN, 3.35,
                        434.4, 134, 142, "43646", 3, 1, 3,
                        4, 5, MeaUnit.cm, 5, 6, new System.DateTime(2019,4,5), 3, 3.2,
                        CreateTestEquipment.Randomized(888889),0,
                        CreateClassicTest.CreateClassicTestLocation(145,246),11, 1.76, 1.93)

                }
            };

        [TestCaseSource(nameof(GetClassicMfuHeaderFromToolsReturnsCorrectValueData))]
        public void GetClassicMfuHeaderFromToolsReturnsCorrectValue(List<ClassicMfuTest> classicTests)
        {
            var useCase = new ClassicTestUseCaseMock();
            var service = new NetworkView.Services.ClassicTestService(null, useCase);
            useCase.GetClassicMfuHeaderFromToolReturnValue = classicTests;

            var result = service.GetClassicMfuHeaderFromTool(new GetClassicHeaderFromToolRequest(), null);

            var comparer = new Func<ClassicMfuTest, DtoTypes.ClassicMfuTest, bool>(EqualityChecker.CompareClassicMfuTestDtoWithClassicChkTest);

            CheckerFunctions.CollectionAssertAreEquivalent(classicTests, result.Result.ClassicMfuTests, comparer);
        }

        private static IEnumerable<ListOfLongs> ListOfLongData = new List<ListOfLongs>()
        {
            new ListOfLongs()
            {
                Values =
                {
                    new Long() {Value = 1},
                    new Long() {Value = 18},
                    new Long() {Value = 15}
                }
            },
            new ListOfLongs()
            {
                Values =
                {
                    new Long() {Value = 99},
                }
            }
        };

        [TestCaseSource(nameof(ListOfLongData))]
        public void GetValuesFromClassicChkHeaderCallsUseCase(ListOfLongs ids)
        {
            var useCase = new ClassicTestUseCaseMock();
            var service = new NetworkView.Services.ClassicTestService(null, useCase);

            service.GetValuesFromClassicChkHeader(ids, null);

            Assert.AreEqual(ids.Values.Select(x => x.Value).ToList(), useCase.GetValuesFromClassicChkHeaderParameter.Select(x => x.ToLong()).ToList());
        }

        private static IEnumerable<List<ClassicChkTestValue>> GetValuesFromClassicChkHeaderReturnsCorrectValueData =
            new List<List<ClassicChkTestValue>>()
            {
                new List<ClassicChkTestValue>()
                {
                    new ClassicChkTestValue()
                    {
                        Id = new GlobalHistoryId(1),
                        Position = 1,
                        ValueUnit2 = 2,
                        ValueUnit1 = 4
                    },
                    new ClassicChkTestValue()
                    {
                        Id = new GlobalHistoryId(17),
                        Position = 19,
                        ValueUnit2 = 24,
                        ValueUnit1 = 41
                    },
                },
                new List<ClassicChkTestValue>()
                {
                    new ClassicChkTestValue()
                    {
                        Id = new GlobalHistoryId(41),
                        Position = 41,
                        ValueUnit2 = 52,
                        ValueUnit1 = 84
                    }
                }
            };

        [TestCaseSource(nameof(GetValuesFromClassicChkHeaderReturnsCorrectValueData))]
        public void GetValuesFromClassicChkHeaderReturnsCorrectValue(List<ClassicChkTestValue> values)
        {
            var useCase = new ClassicTestUseCaseMock();
            var service = new NetworkView.Services.ClassicTestService(null, useCase);
            useCase.GetValuesFromClassicChkHeaderReturnValue = values;

            var result = service.GetValuesFromClassicChkHeader(new ListOfLongs(), null);

            var comparer = new Func<ClassicChkTestValue, DtoTypes.ClassicChkTestValue, bool>(EqualityChecker.CompareClassicChkTestValueDtoWithClassicChkTestValue);

            CheckerFunctions.CollectionAssertAreEquivalent(values, result.Result.ClassicChkTestValues, comparer);
        }

        [TestCaseSource(nameof(ListOfLongData))]
        public void GetValuesFromClassicMfuHeaderCallsUseCase(ListOfLongs ids)
        {
            var useCase = new ClassicTestUseCaseMock();
            var service = new NetworkView.Services.ClassicTestService(null, useCase);

            service.GetValuesFromClassicMfuHeader(ids, null);

            Assert.AreEqual(ids.Values.Select(x => x.Value).ToList(), useCase.GetValuesFromClassicMfuHeaderParameter.Select(x => x.ToLong()).ToList());
        }

        private static IEnumerable<List<ClassicMfuTestValue>> GetValuesFromClassicMfuHeaderReturnsCorrectValueData =
            new List<List<ClassicMfuTestValue>>()
            {
                new List<ClassicMfuTestValue>()
                {
                    new ClassicMfuTestValue()
                    {
                        Id = new GlobalHistoryId(1),
                        Position = 1,
                        ValueUnit2 = 2,
                        ValueUnit1 = 4
                    },
                    new ClassicMfuTestValue()
                    {
                        Id = new GlobalHistoryId(17),
                        Position = 19,
                        ValueUnit2 = 24,
                        ValueUnit1 = 41
                    },
                },
                new List<ClassicMfuTestValue>()
                {
                    new ClassicMfuTestValue()
                    {
                        Id = new GlobalHistoryId(41),
                        Position = 41,
                        ValueUnit2 = 52,
                        ValueUnit1 = 84
                    }
                }
            };

        [TestCaseSource(nameof(GetValuesFromClassicMfuHeaderReturnsCorrectValueData))]
        public void GetValuesFromClassicMfuHeaderReturnsCorrectValue(List<ClassicMfuTestValue> values)
        {
            var useCase = new ClassicTestUseCaseMock();
            var service = new NetworkView.Services.ClassicTestService(null, useCase);
            useCase.GetValuesFromClassicMfuHeaderReturnValue = values;

            var result = service.GetValuesFromClassicMfuHeader(new ListOfLongs(), null);

            var comparer = new Func<ClassicMfuTestValue, DtoTypes.ClassicMfuTestValue, bool>(EqualityChecker.CompareClassicMfuTestValueDtoWithClassicMfuTestValue);

            CheckerFunctions.CollectionAssertAreEquivalent(values, result.Result.ClassicMfuTestValues, comparer);
        }

        [TestCase(1)]
        [TestCase(134)]
        public void GetToolsFromLocationTestsCallsUseCase(long locationId)
        {
            var useCase = new ClassicTestUseCaseMock();
            var service = new NetworkView.Services.ClassicTestService(null, useCase);

            service.GetToolsFromLocationTests(new Long() {Value = locationId}, null);

            Assert.AreEqual(locationId, useCase.GetToolsFromLocationTestsParameter.ToLong());
        }

        private static
            IEnumerable<Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)>> GetToolsFromLocationTestsReturnsCorrectValueData =
                new List<Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)>>()
                {
                    new Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)>()
                    {
                        { 
                            CreateTool.Parameterized(1, "34546", "34547", true,
                                CreateToolModel.Randomized(84966), "a", "b", "c", "d",
                                new ConfigurableField() {ListId = new HelperTableEntityId(1), Value = new HelperTableEntityValue("f")},
                                new CostCenter(){ListId = new HelperTableEntityId(1), Value = new HelperTableEntityValue("XXX")},
                                new Status(){Id = new StatusId(11), Value = new StatusDescription("V")}), 
                            (new DateTime(2021, 5, 3),new DateTime(2019, 4, 3), true)
                        },
                        {
                            CreateTool.Parameterized(13, "33233", "ewwe", false,
                                CreateToolModel.Randomized(7843), "X", "D", "c", "I",
                                new ConfigurableField() {ListId = new HelperTableEntityId(45), Value = new HelperTableEntityValue("f")},
                                new CostCenter(){ListId = new HelperTableEntityId(1), Value = new HelperTableEntityValue("XXX")},
                                new Status(){Id = new StatusId(12), Value = new StatusDescription("V")}),
                            (new DateTime(2021, 1, 4),new DateTime(2021, 5, 3), false)
                        }
                    },
                    new Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)>()
                    {
                        {
                            CreateTool.Parameterized(15, "bbb", "aaa", true,
                                CreateToolModel.Randomized(286680982), "G", "U", "c", "d",
                                new ConfigurableField() {ListId = new HelperTableEntityId(1), Value = new HelperTableEntityValue("f")},
                                new CostCenter(){ListId = new HelperTableEntityId(13), Value = new HelperTableEntityValue("XXX")},
                                new Status(){Id = new StatusId(11), Value = new StatusDescription("V")}),
                            (new DateTime(2010, 9, 3),new DateTime(2021, 5, 9), false)
                        },
                    },
                };

        [TestCaseSource(nameof(GetToolsFromLocationTestsReturnsCorrectValueData))]
        public void GetToolsFromLocationTestsReturnsCorrectValue(Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)> values)
        {
            var useCase = new ClassicTestUseCaseMock();
            var service = new NetworkView.Services.ClassicTestService(null, useCase);
            useCase.GetToolsFromLocationTestsReturnValue = values;

            var result = service.GetToolsFromLocationTests(new Long(), null);
            var i = 0;

            Assert.AreEqual(values.Count, result.Result.ClassicTestPowToolDatas.Count);
            foreach (var val in values)
            {
                
                Assert.IsTrue(EqualityChecker.CompareToolDtoWithTool(result.Result.ClassicTestPowToolDatas[i].Tool, val.Key));
                Assert.AreEqual(val.Value.isToolAssignmentActive,result.Result.ClassicTestPowToolDatas[i].IsToolAssignmentActive);
                Assert.IsTrue(EqualityChecker.ArePrimitiveDateTimeAndDtoEqual(val.Value.firsttest.Value, result.Result.ClassicTestPowToolDatas[i].FirstTest.Value));
                Assert.IsTrue(EqualityChecker.ArePrimitiveDateTimeAndDtoEqual(val.Value.lasttest.Value, result.Result.ClassicTestPowToolDatas[i].LastTest.Value));
                i++;
            }
        }

        [TestCase(1)]
        [TestCase(14)]
        public void GetClassicProcessHeaderFromLocationCallsUseCase(long locationId)
        {
            var useCase = new ClassicTestUseCaseMock();
            var service = new NetworkView.Services.ClassicTestService(null, useCase);
            useCase.GetClassicProcessHeaderFromLocationReturnValue = new List<ClassicProcessTest>();
            service.GetClassicProcessHeaderFromLocation(new Long(){Value = locationId}, null);

            Assert.AreEqual(locationId, useCase.GetClassicProcessHeaderFromLocationParameter.ToLong());
        }

        private static IEnumerable<List<ClassicProcessTest>> LoadManufacturersReturnsCorrectValueData = new List<List<ClassicProcessTest>>()
        {
            new List<ClassicProcessTest>()
            {
                CreateClassicProcessTest.Randomized(345),
                CreateClassicProcessTest.Randomized(3434),

            },
            new List<ClassicProcessTest>()
            {
                CreateClassicProcessTest.Randomized(345333)
            }
        };

        [TestCaseSource(nameof(LoadManufacturersReturnsCorrectValueData))]
        public void LoadManufacturersReturnsCorrectValue(List<ClassicProcessTest> tests)
        {
            var useCase = new ClassicTestUseCaseMock();
            var service = new NetworkView.Services.ClassicTestService(null, useCase);
            useCase.GetClassicProcessHeaderFromLocationReturnValue = tests;
            var result = service.GetClassicProcessHeaderFromLocation(new Long(), null);

            var comparer = new Func<ClassicProcessTest, DtoTypes.ClassicProcessTest, bool>(EqualityChecker.CompareClassicProcessTestWithDto);

            CheckerFunctions.CollectionAssertAreEquivalent(tests, result.Result.ClassicProcessTests, comparer);
        }


        [TestCaseSource(nameof(ListOfLongData))]
        public void GetValuesFromClassicProcessHeaderCallsUseCase(ListOfLongs ids)
        {
            var useCase = new ClassicTestUseCaseMock();
            var service = new NetworkView.Services.ClassicTestService(null, useCase);

            service.GetValuesFromClassicProcessHeader(ids, null);

            Assert.AreEqual(ids.Values.Select(x => x.Value).ToList(), useCase.GetValuesFromClassicProcessHeaderGlobalHistoryIds.Select(x => x.ToLong()).ToList());
        }

        private static IEnumerable<List<ClassicProcessTestValue>> GetValuesFromClassicProcessHeaderReturnsCorrectValueData =
            new List<List<ClassicProcessTestValue>>()
            {
                new List<ClassicProcessTestValue>()
                {
                    new ClassicProcessTestValue()
                    {
                        Id = new GlobalHistoryId(1),
                        Position = 1,
                        ValueUnit2 = 2,
                        ValueUnit1 = 4
                    },
                    new ClassicProcessTestValue()
                    {
                        Id = new GlobalHistoryId(17),
                        Position = 19,
                        ValueUnit2 = 24,
                        ValueUnit1 = 41
                    },
                },
                new List<ClassicProcessTestValue>()
                {
                    new ClassicProcessTestValue()
                    {
                        Id = new GlobalHistoryId(41),
                        Position = 41,
                        ValueUnit2 = 52,
                        ValueUnit1 = 84
                    }
                }
            };

        [TestCaseSource(nameof(GetValuesFromClassicProcessHeaderReturnsCorrectValueData))]
        public void GetValuesFromClassicProcessHeaderReturnsCorrectValue(List<ClassicProcessTestValue> values)
        {
            var useCase = new ClassicTestUseCaseMock();
            var service = new NetworkView.Services.ClassicTestService(null, useCase);
            useCase.GetValuesFromClassicProcessHeaderReturnValue = values;

            var result = service.GetValuesFromClassicProcessHeader(new ListOfLongs(), null);

            var comparer = new Func<ClassicProcessTestValue, DtoTypes.ClassicProcessTestValue, bool>(EqualityChecker.CompareClassicProcessTestValueDtoWithClassicProcessTestValue);

            CheckerFunctions.CollectionAssertAreEquivalent(values, result.Result.ClassicProcessTestValues, comparer);
        }
    }
}
