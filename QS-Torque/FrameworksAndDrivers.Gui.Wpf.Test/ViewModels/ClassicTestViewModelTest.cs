using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using NUnit.Framework;
using System.Collections.Generic;
using TestHelper.Mock;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Client.Core.Entities;
using Client.TestHelper.Factories;
using Common.Types.Enums;
using Core.Enums;
using InterfaceAdapters;
using InterfaceAdapters.Models;
using TestHelper.Factories;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    public class ClassicTestViewModelTest
    {
        private class ClassicTestUseCaseMock : IClassicTestUseCase
        {
            public Tool LoadChkHeaderFromToolToolParameter { get; set; }
            public Location LoadChkHeaderFromToolLocationParameter { get; set; }
            public IClassicTestDataErrorShower LoadChkHeaderFromToolParameterError { get; set; }
            public void LoadChkHeaderFromTool(Tool tool, IClassicTestDataErrorShower errorShower, Location location = null)
            {
                LoadChkHeaderFromToolParameterError = errorShower;
                LoadChkHeaderFromToolToolParameter = tool;
                LoadChkHeaderFromToolLocationParameter = location;
            }

            public Tool LoadMfuHeaderFromToolToolParameter { get; set; }
            public Location LoadMfuHeaderFromToolLocationParameter { get; set; }
            public IClassicTestDataErrorShower LoadMfuHeaderFromToolParameterError { get; set; }
            public void LoadMfuHeaderFromTool(Tool tool, IClassicTestDataErrorShower errorShower, Location location = null)
            {
                LoadMfuHeaderFromToolToolParameter = tool;
                LoadMfuHeaderFromToolLocationParameter = location;
                LoadMfuHeaderFromToolParameterError = errorShower;
            }

            public Location LoadToolsFromLocationTestsParameterLocation { get; set; }
            public IClassicTestDataErrorShower LoadToolsFromLocationTestsParameterError { get; set; }
            public void LoadToolsFromLocationTests(Location location, IClassicTestDataErrorShower errorShower)
            {
                LoadToolsFromLocationTestsParameterLocation = location;
                LoadToolsFromLocationTestsParameterError = errorShower;
            }


            public IShowEvaluation LoadValuesForClassicChkHeaderParameterEvaluate { get; set; }
            public IClassicTestDataErrorShower LoadValuesForClassicChkHeaderParameterError { get; set; }
            public List<ClassicChkTest> LoadValuesForClassicChkHeaderParameterTests { get; set; }
            public Tool LoadValuesForClassicChkHeaderParameterTool { get; set; }
            public Location LoadValuesForClassicChkHeaderParameterLocation { get; set; }
            public void LoadValuesForClassicChkHeader(Location location, Tool tool, List<ClassicChkTest> chktests, IClassicTestDataErrorShower errorShower, IShowEvaluation showEval)
            {
                LoadValuesForClassicChkHeaderParameterLocation = location;
                LoadValuesForClassicChkHeaderParameterTool = tool;
                LoadValuesForClassicChkHeaderParameterTests = chktests;
                LoadValuesForClassicChkHeaderParameterError = errorShower;
                LoadValuesForClassicChkHeaderParameterEvaluate = showEval;
            }

            public IShowEvaluation LoadValuesForClassicMfuHeaderParameterEval { get; set; }
            public IClassicTestDataErrorShower LoadValuesForClassicMfuHeaderParameterError { get; set; }
            public List<ClassicMfuTest> LoadValuesForClassicMfuHeaderParameterTests { get; set; }
            public Tool LoadValuesForClassicMfuHeaderParameterTool { get; set; }
            public Location LoadValuesForClassicMfuHeaderParameterLocation { get; set; }
            public void LoadValuesForClassicMfuHeader(Location location, Tool tool, List<ClassicMfuTest> mfutest, IClassicTestDataErrorShower errorShower, IShowEvaluation showEval)
            {
                LoadValuesForClassicMfuHeaderParameterLocation = location;
                LoadValuesForClassicMfuHeaderParameterTool = tool;
                LoadValuesForClassicMfuHeaderParameterTests = mfutest;
                LoadValuesForClassicMfuHeaderParameterError = errorShower;
                LoadValuesForClassicMfuHeaderParameterEval = showEval;
            }

            public IClassicTestDataErrorShower LoadProcessHeaderFromLocationError { get; set; }
            public Location LoadProcessHeaderFromLocationLocation { get; set; }
            public void LoadProcessHeaderFromLocation(Location location, IClassicTestDataErrorShower errorShower)
            {
                LoadProcessHeaderFromLocationLocation = location;
                LoadProcessHeaderFromLocationError = errorShower;
            }

            public IShowEvaluation LoadValuesForClassicProcessHeaderEval { get; set; }
            public IClassicTestDataErrorShower LoadValuesForClassicProcessHeaderError { get; set; }
            public List<ClassicProcessTest> LoadValuesForClassicProcessHeaderTests { get; set; }
            public Location LoadValuesForClassicProcessHeaderLocation { get; set; }
            public bool LoadValuesForClassicProcessHeaderIsPfu { get; set; }
            public void LoadValuesForClassicProcessHeader(Location location, List<ClassicProcessTest> tests, bool isPfu, IClassicTestDataErrorShower errorShower,
                IShowEvaluation showEval)
            {
                LoadValuesForClassicProcessHeaderLocation = location;
                LoadValuesForClassicProcessHeaderTests = tests;
                LoadValuesForClassicProcessHeaderError = errorShower;
                LoadValuesForClassicProcessHeaderEval = showEval;
                LoadValuesForClassicProcessHeaderIsPfu = isPfu;
            }
        }

        [Test]
        public void SelectLocationCallsLoadToolsFromLocationTestsWithCorrectParameter()
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = true;
            var location = new Location();
            environment.viewModel.SelectedLocation = new LocationModel(location, new NullLocalizationWrapper(), null);
            Assert.AreSame(location, environment.mocks.ClassicTestUseCase.LoadToolsFromLocationTestsParameterLocation);
            Assert.AreSame(environment.viewModel, environment.mocks.ClassicTestUseCase.LoadToolsFromLocationTestsParameterError);
            Assert.IsNull(environment.mocks.ClassicTestUseCase.LoadProcessHeaderFromLocationLocation);
            Assert.IsNull(environment.mocks.ClassicTestUseCase.LoadProcessHeaderFromLocationError);
        }

        [Test]
        public void SelectLocationCallLoadProcessHeaderFromLocationWithCorrectParameter()
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = false;
            var location = new Location();
            environment.viewModel.SelectedLocation = new LocationModel(location, new NullLocalizationWrapper(), null);
            Assert.AreSame(location, environment.mocks.ClassicTestUseCase.LoadProcessHeaderFromLocationLocation);
            Assert.AreSame(environment.viewModel, environment.mocks.ClassicTestUseCase.LoadProcessHeaderFromLocationError);
            Assert.IsNull(environment.mocks.ClassicTestUseCase.LoadToolsFromLocationTestsParameterLocation);
            Assert.IsNull(environment.mocks.ClassicTestUseCase.LoadToolsFromLocationTestsParameterError);
        }

        [Test]
        public void SelectedLocationSetsIsMfuControlledByTorqueCheckedCorrect()
        {
            var environment = new Environment();
            environment.viewModel.SelectedLocation = null;
            Assert.IsTrue(environment.viewModel.IsMfuControlledByTorqueChecked);
            environment.viewModel.SelectedLocation = new LocationModel(new Location() { ControlledBy = LocationControlledBy.Angle }, new NullLocalizationWrapper(), null);
            Assert.IsFalse(environment.viewModel.IsMfuControlledByTorqueChecked);
            environment.viewModel.SelectedLocation = new LocationModel(new Location() { ControlledBy = LocationControlledBy.Torque }, new NullLocalizationWrapper(), null);
            Assert.IsTrue(environment.viewModel.IsMfuControlledByTorqueChecked);
        }

        [Test]
        public void SelectedLocationSetsIsChkControlledByTorqueCheckedCorrect()
        {
            var environment = new Environment();
            environment.viewModel.SelectedLocation = null;
            Assert.IsTrue(environment.viewModel.IsChkControlledByTorqueChecked);
            environment.viewModel.SelectedLocation = new LocationModel(new Location() { ControlledBy = LocationControlledBy.Angle }, new NullLocalizationWrapper(), null);
            Assert.IsFalse(environment.viewModel.IsChkControlledByTorqueChecked);
            environment.viewModel.SelectedLocation = new LocationModel(new Location() { ControlledBy = LocationControlledBy.Torque }, new NullLocalizationWrapper(), null);
            Assert.IsTrue(environment.viewModel.IsChkControlledByTorqueChecked);
        }

        [Test]
        public void SelectedToolCallsLoadChkHeaderFromTool()
        {
            var environment = new Environment();
            var location = new Location();
            var tool = new Tool();
            environment.viewModel.SelectedLocation = new LocationModel(location, new NullLocalizationWrapper(), null);
            environment.viewModel.SelectedTool = new PowToolClassicTestHumbleModel(tool, (DateTime.Now, DateTime.Now, true));
            Assert.AreSame(location, environment.mocks.ClassicTestUseCase.LoadChkHeaderFromToolLocationParameter);
            Assert.AreSame(tool, environment.mocks.ClassicTestUseCase.LoadChkHeaderFromToolToolParameter);
            Assert.AreSame(environment.viewModel, environment.mocks.ClassicTestUseCase.LoadChkHeaderFromToolParameterError);
        }

        [Test]
        public void SelectedToolCallsLoadMfuHeaderFromTool()
        {
            var environment = new Environment();
            var location = new Location();
            var tool = new Tool();
            environment.viewModel.SelectedLocation = new LocationModel(location, new NullLocalizationWrapper(), null);
            environment.viewModel.SelectedTool = new PowToolClassicTestHumbleModel(tool, (DateTime.Now, DateTime.Now, true));
            Assert.AreSame(location, environment.mocks.ClassicTestUseCase.LoadMfuHeaderFromToolLocationParameter);
            Assert.AreSame(tool, environment.mocks.ClassicTestUseCase.LoadMfuHeaderFromToolToolParameter);
            Assert.AreSame(environment.viewModel, environment.mocks.ClassicTestUseCase.LoadMfuHeaderFromToolParameterError);
        }

        [Test]
        public void CanEvaluateWithToolTestingCheckedTest()
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = true;

            environment.viewModel.SelectedLocation = new LocationModel(new Location(), new NullLocalizationWrapper(), null);
            environment.viewModel.SelectedTool = new PowToolClassicTestHumbleModel(new Tool(), (DateTime.Now, DateTime.Now, true));
            environment.viewModel.selectedChks.Add(new ChkHeaderClassicTestHumbleModel(CreateClassicChkTest()));
            Assert.IsTrue(environment.viewModel.CanEvaluateData);

            environment.viewModel.selectedChks.Clear();
            environment.viewModel.selectedMfus.Add(new MfuHeaderClassicTestHumbleModel(CreateClassicMfuTest()));
            Assert.IsTrue(environment.viewModel.CanEvaluateData);

            environment.viewModel.selectedChks.Clear();
            environment.viewModel.selectedMfus.Clear();
            Assert.IsFalse(environment.viewModel.CanEvaluateData);

            environment.viewModel.SelectedLocation = null;
            environment.viewModel.selectedMfus.Add(new MfuHeaderClassicTestHumbleModel(CreateClassicMfuTest()));
            Assert.IsFalse(environment.viewModel.CanEvaluateData);

            environment.viewModel.SelectedLocation = new LocationModel(new Location(), new NullLocalizationWrapper(), null);
            environment.viewModel.SelectedTool = null;
            Assert.IsFalse(environment.viewModel.CanEvaluateData);
        }

        [Test]
        public void CanEvaluateWithProcessTestingCheckedTest()
        {
            var environment = new Environment();
            environment.viewModel.ProcessTestingChecked = true;

            environment.viewModel.SelectedLocation = new LocationModel(new Location(), new NullLocalizationWrapper(), null);
            environment.viewModel.selectedCtls.Add(new ProcessHeaderClassicTestHumbleModel(CreateClassicProcessTest.Randomized(324)));
            Assert.IsTrue(environment.viewModel.CanEvaluateData);

            environment.viewModel.selectedCtls.Clear();
            environment.viewModel.selectedPfus.Add(new ProcessHeaderClassicTestHumbleModel(CreateClassicProcessTest.Randomized(324)));
            Assert.IsTrue(environment.viewModel.CanEvaluateData);

            environment.viewModel.selectedCtls.Clear();
            environment.viewModel.selectedPfus.Clear();
            Assert.IsFalse(environment.viewModel.CanEvaluateData);

            environment.viewModel.SelectedLocation = null;
            environment.viewModel.selectedPfus.Add(new ProcessHeaderClassicTestHumbleModel(CreateClassicProcessTest.Randomized(324)));
            Assert.IsFalse(environment.viewModel.CanEvaluateData);
        }


        [Test]
        public void IsChkByControlVisibleReturnsCorrectValue()
        {
            var environment = new Environment();
            environment.mocks.ClassicTestInterfaceAdapter.ChkHeaderClassicTests = null;
            Assert.IsFalse(environment.viewModel.IsChkByControlVisible);
            environment.mocks.ClassicTestInterfaceAdapter.ChkHeaderClassicTests =
                new ObservableCollection<ChkHeaderClassicTestHumbleModel>();
            environment.mocks.ClassicTestInterfaceAdapter.ChkHeaderClassicTests.Add(new ChkHeaderClassicTestHumbleModel(new ClassicChkTest(){ControlledByUnitId = MeaUnit.Deg}));
            Assert.IsFalse(environment.viewModel.IsChkByControlVisible);
            environment.mocks.ClassicTestInterfaceAdapter.ChkHeaderClassicTests.Add(new ChkHeaderClassicTestHumbleModel(new ClassicChkTest() { ControlledByUnitId = MeaUnit.Nm }));
            Assert.IsTrue(environment.viewModel.IsChkByControlVisible);
        }

        [Test]
        public void IsMfuByControlVisibleReturnsCorrectValue()
        {
            var environment = new Environment();
            environment.mocks.ClassicTestInterfaceAdapter.MfuHeaderClassicTests = null;
            Assert.IsFalse(environment.viewModel.IsMfuByControlVisible);
            environment.mocks.ClassicTestInterfaceAdapter.MfuHeaderClassicTests =
                new ObservableCollection<MfuHeaderClassicTestHumbleModel>();
            environment.mocks.ClassicTestInterfaceAdapter.MfuHeaderClassicTests.Add(new MfuHeaderClassicTestHumbleModel(new ClassicMfuTest() { ControlledByUnitId = MeaUnit.Deg }));
            Assert.IsFalse(environment.viewModel.IsMfuByControlVisible);
            environment.mocks.ClassicTestInterfaceAdapter.MfuHeaderClassicTests.Add(new MfuHeaderClassicTestHumbleModel(new ClassicMfuTest() { ControlledByUnitId = MeaUnit.Nm }));
            Assert.IsTrue(environment.viewModel.IsMfuByControlVisible);
        }

        [Test]
        public void ShowToolAssignmentLegendReturnsCorrectValue()
        {
            var environment = new Environment();
            Assert.IsFalse(environment.viewModel.ShowToolAssignmentLegend);
            environment.mocks.ClassicTestInterfaceAdapter.PowToolClassicTests.Add(new PowToolClassicTestHumbleModel(CreateTool.Anonymous(), (DateTime.Now, DateTime.Now, false)));
            environment.mocks.ClassicTestInterfaceAdapter.PowToolClassicTests.Add(new PowToolClassicTestHumbleModel(CreateTool.Anonymous(), (DateTime.Now, DateTime.Now, false)));
            Assert.IsFalse(environment.viewModel.ShowToolAssignmentLegend);
            environment.mocks.ClassicTestInterfaceAdapter.PowToolClassicTests.Add(new PowToolClassicTestHumbleModel(CreateTool.Anonymous(), (DateTime.Now, DateTime.Now, true)));
            Assert.IsTrue(environment.viewModel.ShowToolAssignmentLegend);
        }

        [Test]
        public void MfuHeaderClassicTestsReturnsInterfaceMfuHeaderClassicTestsIfIsMfuByControlVisibleIsFalse()
        {
            var environment = new Environment();
            var list = new ObservableCollection<MfuHeaderClassicTestHumbleModel>();
            environment.mocks.ClassicTestInterfaceAdapter.MfuHeaderClassicTests = list;
            Assert.AreSame(list, environment.viewModel.MfuHeaderClassicTests);
        }

        [Test]
        public void MfuHeaderClassicTestsReturnsTorqueDataIfIsMfuControlledByTorqueChecked()
        {
            var environment = new Environment();
            var list = new ObservableCollection<MfuHeaderClassicTestHumbleModel>()
            {
                new MfuHeaderClassicTestHumbleModel(new ClassicMfuTest() {ControlledByUnitId = MeaUnit.Deg}),
                new MfuHeaderClassicTestHumbleModel(new ClassicMfuTest() {ControlledByUnitId = MeaUnit.Nm}),
                new MfuHeaderClassicTestHumbleModel(new ClassicMfuTest() {ControlledByUnitId = MeaUnit.Deg}),
                new MfuHeaderClassicTestHumbleModel(new ClassicMfuTest() {ControlledByUnitId = MeaUnit.Deg}),
                new MfuHeaderClassicTestHumbleModel(new ClassicMfuTest() {ControlledByUnitId = MeaUnit.Nm})
            };
            environment.mocks.ClassicTestInterfaceAdapter.MfuHeaderClassicTests = list;
            environment.viewModel.IsMfuControlledByTorqueChecked = true;
            Assert.AreEqual(list.Where(x => x.Entity.ControlledByUnitId == MeaUnit.Nm).ToList(), environment.viewModel.MfuHeaderClassicTests.ToList());
        }

        [Test]
        public void MfuHeaderClassicTestsReturnsAngleDataIfIsMfuControlledByTorqueNotChecked()
        {
            var environment = new Environment();
            var list = new ObservableCollection<MfuHeaderClassicTestHumbleModel>()
            {
                new MfuHeaderClassicTestHumbleModel(new ClassicMfuTest() {ControlledByUnitId = MeaUnit.Deg}),
                new MfuHeaderClassicTestHumbleModel(new ClassicMfuTest() {ControlledByUnitId = MeaUnit.Nm}),
                new MfuHeaderClassicTestHumbleModel(new ClassicMfuTest() {ControlledByUnitId = MeaUnit.Deg}),
                new MfuHeaderClassicTestHumbleModel(new ClassicMfuTest() {ControlledByUnitId = MeaUnit.Deg}),
                new MfuHeaderClassicTestHumbleModel(new ClassicMfuTest() {ControlledByUnitId = MeaUnit.Nm})
            };
            environment.mocks.ClassicTestInterfaceAdapter.MfuHeaderClassicTests = list;
            environment.viewModel.IsMfuControlledByTorqueChecked = false;
            Assert.AreEqual(list.Where(x => x.Entity.ControlledByUnitId == MeaUnit.Deg).ToList(), environment.viewModel.MfuHeaderClassicTests.ToList());
        }

        [Test]
        public void ChkHeaderClassicTestsReturnsInterfaceChkHeaderClassicTestsIfIsChkByControlVisibleIsFalse()
        {
            var environment = new Environment();
            var list = new ObservableCollection<ChkHeaderClassicTestHumbleModel>();
            environment.mocks.ClassicTestInterfaceAdapter.ChkHeaderClassicTests = list;
            Assert.AreSame(list, environment.viewModel.ChkHeaderClassicTests);
        }

        [Test]
        public void ChkHeaderClassicTestsReturnsTorqueDataIfIsChkControlledByTorqueChecked()
        {
            var environment = new Environment();
            var list = new ObservableCollection<ChkHeaderClassicTestHumbleModel>()
            {
                new ChkHeaderClassicTestHumbleModel(new ClassicChkTest() {ControlledByUnitId = MeaUnit.Deg}),
                new ChkHeaderClassicTestHumbleModel(new ClassicChkTest() {ControlledByUnitId = MeaUnit.Nm}),
                new ChkHeaderClassicTestHumbleModel(new ClassicChkTest() {ControlledByUnitId = MeaUnit.Deg}),
                new ChkHeaderClassicTestHumbleModel(new ClassicChkTest() {ControlledByUnitId = MeaUnit.Deg}),
                new ChkHeaderClassicTestHumbleModel(new ClassicChkTest() {ControlledByUnitId = MeaUnit.Nm})
            };
            environment.mocks.ClassicTestInterfaceAdapter.ChkHeaderClassicTests = list;
            environment.viewModel.IsChkControlledByTorqueChecked = true;
            Assert.AreEqual(list.Where(x => x.Entity.ControlledByUnitId == MeaUnit.Nm).ToList(), environment.viewModel.ChkHeaderClassicTests.ToList());
        }

        [Test]
        public void ChkHeaderClassicTestsReturnsAngleDataIfIsChkControlledByTorqueNotChecked()
        {
            var environment = new Environment();
            var list = new ObservableCollection<ChkHeaderClassicTestHumbleModel>()
            {
                new ChkHeaderClassicTestHumbleModel(new ClassicChkTest() {ControlledByUnitId = MeaUnit.Deg}),
                new ChkHeaderClassicTestHumbleModel(new ClassicChkTest() {ControlledByUnitId = MeaUnit.Nm}),
                new ChkHeaderClassicTestHumbleModel(new ClassicChkTest() {ControlledByUnitId = MeaUnit.Deg}),
                new ChkHeaderClassicTestHumbleModel(new ClassicChkTest() {ControlledByUnitId = MeaUnit.Deg}),
                new ChkHeaderClassicTestHumbleModel(new ClassicChkTest() {ControlledByUnitId = MeaUnit.Nm})
            };
            environment.mocks.ClassicTestInterfaceAdapter.ChkHeaderClassicTests = list;
            environment.viewModel.IsChkControlledByTorqueChecked = false;
            Assert.AreEqual(list.Where(x => x.Entity.ControlledByUnitId == MeaUnit.Deg).ToList(), environment.viewModel.ChkHeaderClassicTests.ToList());
        }


        [Test]
        public void EvaluateDataMfuDataWithMfuDataSelected()
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = true;
            var location = new Location();
            var tool = new Tool();
            var tests = new List<MfuHeaderClassicTestHumbleModel>()
            {
                new MfuHeaderClassicTestHumbleModel(CreateClassicMfuTest()),
                new MfuHeaderClassicTestHumbleModel(CreateClassicMfuTest()),
                new MfuHeaderClassicTestHumbleModel(CreateClassicMfuTest())
            };

            environment.viewModel.SelectedLocation = new LocationModel(location, new NullLocalizationWrapper(), null);
            environment.viewModel.SelectedTool = new PowToolClassicTestHumbleModel(tool, (DateTime.Now, DateTime.Now, true));

            environment.viewModel.selectedMfus = tests;
            environment.viewModel.EvaluateDataComand.Execute(null);
            Assert.AreSame(location, environment.mocks.ClassicTestUseCase.LoadValuesForClassicMfuHeaderParameterLocation);
            Assert.AreSame(tool, environment.mocks.ClassicTestUseCase.LoadValuesForClassicMfuHeaderParameterTool);
            Assert.AreEqual(tests.Select(x => x.Entity).ToList(), environment.mocks.ClassicTestUseCase.LoadValuesForClassicMfuHeaderParameterTests.ToList());
            Assert.AreSame(environment.viewModel, environment.mocks.ClassicTestUseCase.LoadValuesForClassicMfuHeaderParameterError);
            Assert.AreSame(environment.viewModel, environment.mocks.ClassicTestUseCase.LoadValuesForClassicMfuHeaderParameterEval);
        }

        [Test]
        public void EvaluateDataChkDataWithChkDataSelected()
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = true;
            var location = new Location();
            var tool = new Tool();
            var tests = new List<ChkHeaderClassicTestHumbleModel>()
            {
                new ChkHeaderClassicTestHumbleModel(CreateClassicChkTest()),
                new ChkHeaderClassicTestHumbleModel(CreateClassicChkTest()),
                new ChkHeaderClassicTestHumbleModel(CreateClassicChkTest())
            };

            environment.viewModel.SelectedLocation = new LocationModel(location, new NullLocalizationWrapper(), null);
            environment.viewModel.SelectedTool = new PowToolClassicTestHumbleModel(tool, (DateTime.Now, DateTime.Now, true));

            environment.viewModel.selectedChks = tests;
            environment.viewModel.EvaluateDataComand.Execute(null);
            Assert.AreSame(location, environment.mocks.ClassicTestUseCase.LoadValuesForClassicChkHeaderParameterLocation);
            Assert.AreSame(tool, environment.mocks.ClassicTestUseCase.LoadValuesForClassicChkHeaderParameterTool);
            Assert.AreEqual(tests.Select(x => x.Entity).ToList(), environment.mocks.ClassicTestUseCase.LoadValuesForClassicChkHeaderParameterTests.ToList());
            Assert.AreSame(environment.viewModel, environment.mocks.ClassicTestUseCase.LoadValuesForClassicChkHeaderParameterError);
            Assert.AreSame(environment.viewModel, environment.mocks.ClassicTestUseCase.LoadValuesForClassicChkHeaderParameterEvaluate);
        }

        [Test]
        public void EvaluateDataCtlDataWithCtlDataSelected()
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = false;
            var location = new Location();
             var tests = new List<ProcessHeaderClassicTestHumbleModel>()
            {
                new ProcessHeaderClassicTestHumbleModel(CreateClassicProcessTest.Randomized(325)),
                new ProcessHeaderClassicTestHumbleModel(CreateClassicProcessTest.Randomized(56)),
                new ProcessHeaderClassicTestHumbleModel(CreateClassicProcessTest.Randomized(3))
            };

            environment.viewModel.SelectedLocation = new LocationModel(location, new NullLocalizationWrapper(), null);

            environment.viewModel.selectedCtls = tests;
            environment.viewModel.EvaluateDataComand.Invoke(null);
            Assert.AreSame(location, environment.mocks.ClassicTestUseCase.LoadValuesForClassicProcessHeaderLocation);
            Assert.AreEqual(tests.Select(x => x.Entity).ToList(), environment.mocks.ClassicTestUseCase.LoadValuesForClassicProcessHeaderTests.ToList());
            Assert.AreSame(environment.viewModel, environment.mocks.ClassicTestUseCase.LoadValuesForClassicProcessHeaderError);
            Assert.AreSame(environment.viewModel, environment.mocks.ClassicTestUseCase.LoadValuesForClassicProcessHeaderEval);
            Assert.IsFalse(environment.mocks.ClassicTestUseCase.LoadValuesForClassicProcessHeaderIsPfu);
        }

        [Test]
        public void EvaluateDataPfuDataWithCtlDataSelected()
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = false;
            var location = new Location();
            var tests = new List<ProcessHeaderClassicTestHumbleModel>()
            {
                new ProcessHeaderClassicTestHumbleModel(CreateClassicProcessTest.Randomized(325)),
                new ProcessHeaderClassicTestHumbleModel(CreateClassicProcessTest.Randomized(56)),
                new ProcessHeaderClassicTestHumbleModel(CreateClassicProcessTest.Randomized(3))
            };

            environment.viewModel.SelectedLocation = new LocationModel(location, new NullLocalizationWrapper(), null);

            environment.viewModel.selectedPfus = tests;
            environment.viewModel.EvaluateDataComand.Invoke(null);
            Assert.AreSame(location, environment.mocks.ClassicTestUseCase.LoadValuesForClassicProcessHeaderLocation);
            Assert.AreEqual(tests.Select(x => x.Entity).ToList(), environment.mocks.ClassicTestUseCase.LoadValuesForClassicProcessHeaderTests.ToList());
            Assert.AreSame(environment.viewModel, environment.mocks.ClassicTestUseCase.LoadValuesForClassicProcessHeaderError);
            Assert.AreSame(environment.viewModel, environment.mocks.ClassicTestUseCase.LoadValuesForClassicProcessHeaderEval);
            Assert.IsTrue(environment.mocks.ClassicTestUseCase.LoadValuesForClassicProcessHeaderIsPfu);
        }

        [Test]
        public void ShowValuesFromClassicChkHeaderStartUpCalled()
        {
            var environment = new Environment();
            var location = new Location();
            var tool = new Tool();
            var tests = new List<ClassicChkTest>()
            {
                CreateClassicChkTest(),
                CreateClassicChkTest(),
                CreateClassicChkTest()
            };
            environment.viewModel.ShowValuesForClassicChkHeader(location,tool, tests);

            Assert.AreSame(location, environment.mocks.StartUp.OpenClassicChkTestHtmlViewLocation);
            Assert.AreSame(tool, environment.mocks.StartUp.OpenClassicChkTestHtmlViewTool);
            Assert.AreEqual(tests.ToList(), environment.mocks.StartUp.OpenClassicChkTestHtmlViewTests.ToList());
        }

        [Test]
        public void ShowValuesFromClassicMfuHeaderStartUpCalled()
        {
            var environment = new Environment();
            var location = new Location();
            var tool = new Tool();
            var tests = new List<ClassicMfuTest>()
            {
                CreateClassicMfuTest(),
                CreateClassicMfuTest(),
                CreateClassicMfuTest()
            };
            environment.viewModel.ShowValuesForClassicMfuHeader(location, tool, tests);

            Assert.AreSame(location, environment.mocks.StartUp.OpenClassicMfuTestHtmlViewLocation);
            Assert.AreSame(tool, environment.mocks.StartUp.OpenClassicMfuTestHtmlViewTool);
            Assert.AreEqual(tests.ToList(), environment.mocks.StartUp.OpenClassicMfuTestHtmlViewTests.ToList());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ProcessAndToolTestingCheckedReturnsCorrectValue(bool toolTestingChecked)
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = toolTestingChecked;
            Assert.AreEqual(toolTestingChecked, environment.viewModel.ToolTestingChecked);
            Assert.AreEqual(!toolTestingChecked, environment.viewModel.ProcessTestingChecked);
        }

        [Test]
        public void ToolTestingCommandCallsLoadProcessHeaderFromLocationWithCorrectParameter()
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = true;
            var location = new Location();
            environment.viewModel.SelectedLocation = new LocationModel(location, new NullLocalizationWrapper(), null);
            environment.viewModel.ToolTestingCommand.Invoke(null);
            Assert.AreSame(location, environment.mocks.ClassicTestUseCase.LoadToolsFromLocationTestsParameterLocation);
            Assert.AreSame(environment.viewModel, environment.mocks.ClassicTestUseCase.LoadToolsFromLocationTestsParameterError);
        }

        [Test]
        public void ToolTestingCommandSetsIsMfuControlledByTorqueCheckedCorrect()
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = true;
            environment.viewModel.SelectedLocation = null;
            Assert.IsTrue(environment.viewModel.IsMfuControlledByTorqueChecked);
            environment.viewModel.SelectedLocation = new LocationModel(new Location() { ControlledBy = LocationControlledBy.Angle }, new NullLocalizationWrapper(), null);
            environment.viewModel.ToolTestingCommand.Invoke(null);
            Assert.IsFalse(environment.viewModel.IsMfuControlledByTorqueChecked);
            environment.viewModel.SelectedLocation = new LocationModel(new Location() { ControlledBy = LocationControlledBy.Torque }, new NullLocalizationWrapper(), null);
            environment.viewModel.ToolTestingCommand.Invoke(null);
            Assert.IsTrue(environment.viewModel.IsMfuControlledByTorqueChecked);
        }

        [Test]
        public void ToolTestingCommandSetsIsChkControlledByTorqueCheckedCorrect()
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = true;
            environment.viewModel.SelectedLocation = null;
            environment.viewModel.ToolTestingCommand.Invoke(null);
            Assert.IsTrue(environment.viewModel.IsChkControlledByTorqueChecked);
            environment.viewModel.ToolTestingCommand.Invoke(null);
            environment.viewModel.SelectedLocation = new LocationModel(new Location() { ControlledBy = LocationControlledBy.Angle }, new NullLocalizationWrapper(), null);
            environment.viewModel.ToolTestingCommand.Invoke(null);
            Assert.IsFalse(environment.viewModel.IsChkControlledByTorqueChecked);
            environment.viewModel.SelectedLocation = new LocationModel(new Location() { ControlledBy = LocationControlledBy.Torque }, new NullLocalizationWrapper(), null);
            environment.viewModel.ToolTestingCommand.Invoke(null);
            Assert.IsTrue(environment.viewModel.IsChkControlledByTorqueChecked);
        }

        [Test]
        public void ProcessTestingCommandCallsLoadProcessHeaderFromLocationWithCorrectParameter()
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = true;
            var location = new Location();
            environment.viewModel.SelectedLocation = new LocationModel(location, new NullLocalizationWrapper(), null);
            environment.viewModel.ProcessTestingCommand.Invoke(null);
            Assert.AreSame(location, environment.mocks.ClassicTestUseCase.LoadProcessHeaderFromLocationLocation);
            Assert.AreSame(environment.viewModel, environment.mocks.ClassicTestUseCase.LoadProcessHeaderFromLocationError);
        }


        [TestCase(true)]
        [TestCase(false)]
        public void ShowValuesFromClassicProcessHeaderStartUpCalled(bool isPfu)
        {
            var environment = new Environment();
            var location = new Location();
            var tool = new Tool();
            var tests = new List<ClassicMfuTest>()
            {
                CreateClassicMfuTest(),
                CreateClassicMfuTest(),
                CreateClassicMfuTest()
            };
            environment.viewModel.ShowValuesForClassicMfuHeader(location, tool, tests);

            Assert.AreSame(location, environment.mocks.StartUp.OpenClassicMfuTestHtmlViewLocation);
            Assert.AreSame(tool, environment.mocks.StartUp.OpenClassicMfuTestHtmlViewTool);
            Assert.AreEqual(tests.ToList(), environment.mocks.StartUp.OpenClassicMfuTestHtmlViewTests.ToList());
        }

        [Test]
        public void ShowValuesForClassicProcessHeaderCallsProcessMonitoringStartUp()
        {
            var environment = new Environment();
            var location = new Location();
            var tests = new List<ClassicProcessTest>()
            {
                CreateClassicProcessTest.Randomized(435),
                CreateClassicProcessTest.Randomized(234),
                CreateClassicProcessTest.Randomized(111)
            };
            environment.viewModel.ShowValuesForClassicProcessHeader(location, tests, false);

            Assert.AreSame(location, environment.mocks.StartUp.OpenClassicProcessMonitoringTestHtmlViewLocation);
            Assert.AreEqual(tests.ToList(), environment.mocks.StartUp.OpenClassicProcessMonitoringTestHtmlViewTests.ToList());
        }

        [Test]
        public void ShowValuesForClassicProcessHeaderCallsProcessPfuStartUp()
        {
            var environment = new Environment();
            var location = new Location();
            var tests = new List<ClassicProcessTest>()
            {
                CreateClassicProcessTest.Randomized(435),
                CreateClassicProcessTest.Randomized(234),
                CreateClassicProcessTest.Randomized(111)
            };
            environment.viewModel.ShowValuesForClassicProcessHeader(location, tests, true);

            Assert.AreSame(location, environment.mocks.StartUp.OpenClassicProcessPfuTestHtmlViewLocation);
            Assert.AreEqual(tests.ToList(), environment.mocks.StartUp.OpenClassicProcessPfuTestHtmlViewTests.ToList());
        }

        public class ClassicTestInterfaceAdapterMock : IClassicTestInterface
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public ObservableCollection<PowToolClassicTestHumbleModel> PowToolClassicTests { get; } =
                new ObservableCollection<PowToolClassicTestHumbleModel>();
            public ObservableCollection<MfuHeaderClassicTestHumbleModel> MfuHeaderClassicTests { get; set; }
            public ObservableCollection<ChkHeaderClassicTestHumbleModel> ChkHeaderClassicTests { get; set; }
            public ObservableCollection<ProcessHeaderClassicTestHumbleModel> ProcessHeaderClassicTests { get; set; }
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    localization = new NullLocalizationWrapper();
                    ClassicTestUseCase = new ClassicTestUseCaseMock();
                    LocationUseCase = new LocationUseCaseMock(null);
                    StartUp = new StartUpMock();
                    ClassicTestInterfaceAdapter = new ClassicTestInterfaceAdapterMock();
                }

                public NullLocalizationWrapper localization;
                public ClassicTestUseCaseMock ClassicTestUseCase;
                public LocationUseCaseMock LocationUseCase;
                public StartUpMock StartUp;
                public ClassicTestInterfaceAdapterMock ClassicTestInterfaceAdapter;
            }

            public Environment()
            {
                mocks = new Mocks();
                viewModel = new ClassicTestViewModel(mocks.ClassicTestUseCase, mocks.LocationUseCase, mocks.localization, mocks.StartUp, mocks.ClassicTestInterfaceAdapter);
            }

            public Mocks mocks;
            public ClassicTestViewModel viewModel;
        }


        internal ClassicMfuTest CreateClassicMfuTest(long globalhistory = 0, MeaUnit controlledByMeaUnit = MeaUnit.Nm)
        {
            var test = new ClassicMfuTest() { ControlledByUnitId = controlledByMeaUnit };
            test.Id = new GlobalHistoryId(globalhistory);
            return test;
        }

        internal ClassicChkTest CreateClassicChkTest(long globalhistory = 0, MeaUnit controlledByMeaUnit = MeaUnit.Nm)
        {
            var test = new ClassicChkTest() { ControlledByUnitId = controlledByMeaUnit };
            test.Id = new GlobalHistoryId(globalhistory);
            return test;
        }
    }
}