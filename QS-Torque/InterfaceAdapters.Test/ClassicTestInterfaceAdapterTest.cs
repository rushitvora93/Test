using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Core.Entities;
using Client.TestHelper.Factories;
using Common.Types.Enums;
using Core.Entities;
using NUnit.Framework;
using TestHelper.Checker;
using CreateTool = TestHelper.Factories.CreateTool;

namespace InterfaceAdapters.Test
{
    public class ClassicTestInterfaceAdapterTest
    {
        [Test]
        public void ShowToolsForSelectedLocationWithNullToolSetsEmptyPowToolClassicTests()
        {
            var adapter = new ClassicTestInterfaceAdapter();
            adapter.ShowToolsForSelectedLocation(null);
            Assert.AreEqual(0, adapter.PowToolClassicTests.Count);
        }

        private static
            IEnumerable<Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)>>
            ShowToolsForSelectedLocationData =
                new List<Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)>>()
                {
                    new Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)>()
                    {
                        {
                            CreateTool.WithId(1),
                            (
                                new DateTime(2021, 10,10),
                                new DateTime(2020,1,1), 
                                true 
                             )
                        },
                        {
                            CreateTool.WithId(31),
                            (
                                new DateTime(2011, 5,7),
                                new DateTime(2019,4,11),
                                true
                            )
                        }
                    },
                    new Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)>()
                    {
                        {
                            CreateTool.WithId(15),
                            (
                                new DateTime(2001, 10,10),
                                new DateTime(2000,1,1),
                                true
                            )
                        }
                    }
                };

        [TestCaseSource(nameof(ShowToolsForSelectedLocationData))]
        public void ShowToolsForSelectedLocationSetsPowToolClassicTestsFromTools(Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)> tools)
        {
            var adapter = new ClassicTestInterfaceAdapter();
            adapter.ShowToolsForSelectedLocation(tools);

            Assert.AreEqual(tools.Count, adapter.PowToolClassicTests.Count);
            var i = 0;
            foreach (var tool in tools)
            {
                Assert.AreSame(adapter.PowToolClassicTests[i].Entity, tool.Key);
                Assert.AreEqual(adapter.PowToolClassicTests[i].FirstTest, tool.Value.firsttest);
                Assert.AreEqual(adapter.PowToolClassicTests[i].LastTest, tool.Value.lasttest);
                Assert.AreEqual(adapter.PowToolClassicTests[i].IsToolAssignmentActive, tool.Value.isToolAssignmentActive);

                i++;
            }
        }

        private static IEnumerable<List<ClassicMfuTest>> ShowMfuHeaderForSelectedToolData =
            new List<List<ClassicMfuTest>>()
            {
                new List<ClassicMfuTest>()
                {
                    new ClassicMfuTest()
                    {
                        Id = new GlobalHistoryId(1),
                        Average = 1.1,
                        Cm = 5.5,
                        Cmk = 4.4,
                        ControlledByUnitId = MeaUnit.Deg,
                        LimitCm = 5,
                        LimitCmk = 8.8,
                        LowerLimitUnit1 = 4,
                        LowerLimitUnit2 = 4,
                        NominalValueUnit1 = 1,
                        NominalValueUnit2 = 7,
                        NumberOfTests = 1
                    },
                    new ClassicMfuTest()
                    {
                        Id = new GlobalHistoryId(13),
                        Average = 41.1,
                        Cm = 55.5,
                        Cmk = 64.4,
                        ControlledByUnitId = MeaUnit.Deg,
                        LimitCm = 52,
                        LimitCmk = 83.8,
                        LowerLimitUnit1 = 44,
                        LowerLimitUnit2 = 45,
                        NominalValueUnit1 = 11,
                        NominalValueUnit2 = 73,
                        NumberOfTests = 41
                    }
                },
                new List<ClassicMfuTest>()
                {
                    new ClassicMfuTest()
                    {
                        Id = new GlobalHistoryId(1),
                        Average = 15.1,
                        Cm = 52.5,
                        Cmk = 14.4,
                        ControlledByUnitId = MeaUnit.DegPerS,
                        LimitCm = 54,
                        LimitCmk = 85.8,
                        LowerLimitUnit1 = 64,
                        LowerLimitUnit2 = 47,
                        NominalValueUnit1 = 12,
                        NominalValueUnit2 = 74,
                        NumberOfTests = 21
                    }
                }
            };

        [TestCaseSource(nameof(ShowMfuHeaderForSelectedToolData))]
        public void ShowMfuHeaderForSelectedToolSetsMfuHeaderClassicTests(List<ClassicMfuTest> tests)
        {
            var adapter = new ClassicTestInterfaceAdapter();
            adapter.ShowMfuHeaderForSelectedTool(tests);
            Assert.AreEqual(adapter.MfuHeaderClassicTests.Select(x => x.Entity).ToList(), tests);
        }

        private static IEnumerable<List<ClassicChkTest>> ShowChkHeaderForSelectedToolData =
            new List<List<ClassicChkTest>>()
            {
                new List<ClassicChkTest>()
                {
                    new ClassicChkTest()
                    {
                        Id = new GlobalHistoryId(1),
                        Average = 1.1,
                        ControlledByUnitId = MeaUnit.Deg,
                        LowerLimitUnit1 = 4,
                        LowerLimitUnit2 = 4,
                        NominalValueUnit1 = 1,
                        NominalValueUnit2 = 7,
                        NumberOfTests = 1
                    },
                    new ClassicChkTest()
                    {
                        Id = new GlobalHistoryId(13),
                        Average = 41.1,
                        ControlledByUnitId = MeaUnit.Deg,
                        LowerLimitUnit1 = 44,
                        LowerLimitUnit2 = 45,
                        NominalValueUnit1 = 11,
                        NominalValueUnit2 = 73,
                        NumberOfTests = 41
                    }
                },
                new List<ClassicChkTest>()
                {
                    new ClassicChkTest()
                    {
                        Id = new GlobalHistoryId(1),
                        Average = 15.1,
                        ControlledByUnitId = MeaUnit.DegPerS,
                        LowerLimitUnit1 = 64,
                        LowerLimitUnit2 = 47,
                        NominalValueUnit1 = 12,
                        NominalValueUnit2 = 74,
                        NumberOfTests = 21
                    }
                }
            };

        [TestCaseSource(nameof(ShowChkHeaderForSelectedToolData))]
        public void ShowChkHeaderForSelectedToolSetsChkHeaderClassicTests(List<ClassicChkTest> tests)
        {
            var adapter = new ClassicTestInterfaceAdapter();
            adapter.ShowChkHeaderForSelectedTool(tests);
            Assert.AreEqual(adapter.ChkHeaderClassicTests.Select(x => x.Entity).ToList(), tests);
        }

        private static IEnumerable<List<ClassicProcessTest>> ShowProcessHeaderForSelectedLocationData =
            new List<List<ClassicProcessTest>>()
            {
                new List<ClassicProcessTest>()
                {
                    CreateClassicProcessTest.Randomized(234),
                    CreateClassicProcessTest.Randomized(23234)
                },
                new List<ClassicProcessTest>()
                {
                    CreateClassicProcessTest.Randomized(234)
                }

            };

        [TestCaseSource(nameof(ShowProcessHeaderForSelectedLocationData))]
        public void ShowProcessHeaderForSelectedLocationSetsProcessHeaderClassicTests(List<ClassicProcessTest> tests)
        {
            var adapter = new ClassicTestInterfaceAdapter();
            adapter.ShowProcessHeaderForSelectedLocation(tests);
            Assert.AreEqual(adapter.ProcessHeaderClassicTests.Select(x => x.Entity).ToList(), tests);
        }
    }
}
