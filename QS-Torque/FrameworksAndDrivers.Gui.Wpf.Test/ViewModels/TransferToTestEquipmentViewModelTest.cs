using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using Client.Core.Diffs;
using Client.Core.Entities;
using Client.TestHelper.Factories;
using Common.Types.Enums;
using Core.Diffs;
using Core.Entities;
using Core.Enums;
using Core.UseCases.Communication;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using InterfaceAdapters.Communication;
using InterfaceAdapters.Models;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    class TransferToTestEquipmentViewModelTest
    {
        [Test]
        public void GettingSelectedTestEquipmentForwardsToInterfaceAdapter()
        {
            var environment = new Environment();
            var equipmentModel = TestEquipmentHumbleModel.GetModelFor(CreateTestEquipment.Anonymous(), new NullLocalizationWrapper());
            environment.mocks.InterfaceAdapter.SelectedTestEquipment = equipmentModel;
            Assert.AreSame(equipmentModel, environment.viewModel.SelectedTestEquipment);
        }

        [Test]
        public void LoadCommandCallsShowLocationToolAssignmentWithChkTestType()
        {
            var environment = new Environment();
            environment.viewModel.Load.Invoke(null);
            Assert.AreEqual(TestType.Chk, environment.mocks.transferUseCase.ShowLocationToolAssignmentsTestTypeParameter);
        }

        [Test]
        public void LoadCommandCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.viewModel.Load.Invoke(null);
            Assert.IsTrue(environment.mocks.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.AreEqual(2, environment.mocks.StartUp.RaiseShowLoadingControlCount);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void LoadCommandCallsUseCaseShowTestEquipmentsForProcessControlAndRotatingTests(bool toolChecked)
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = toolChecked;
            environment.viewModel.Load.Invoke(null);
            Assert.AreEqual(environment.viewModel, environment.mocks.testEquipmentUseCase.ShowTestEquipmentsForProcessControlAndRotatingTestsParameterError);
            Assert.AreEqual(environment.viewModel.ToolTestingChecked, environment.mocks.testEquipmentUseCase.ShowTestEquipmentsForProcessControlAndRotatingTestsParameterUseRot);
            Assert.AreEqual(environment.viewModel.ProcessTestingChecked, environment.mocks.testEquipmentUseCase.ShowTestEquipmentsForProcessControlAndRotatingTestsParameterUseCtl);
        }

        [Test]
        public void ShowRotatingTestCommandCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = true;
            environment.viewModel.ShowRotatingTestCommand.Invoke(null);
            Assert.IsTrue(environment.mocks.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.AreEqual(2, environment.mocks.StartUp.RaiseShowLoadingControlCount);
        }

        [TestCase(true)]
        [TestCase(true)]
        public void ShowRotatingTestCommandCallsUseCase(bool mcaChecked)
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = true;
            environment.viewModel.IsMcaTestTypeChecked = mcaChecked;
            environment.viewModel.ShowRotatingTestCommand.Invoke(null);
            Assert.AreEqual(environment.viewModel, environment.mocks.testEquipmentUseCase.ShowTestEquipmentsForProcessControlAndRotatingTestsParameterError);
            Assert.IsTrue( environment.mocks.testEquipmentUseCase.ShowTestEquipmentsForProcessControlAndRotatingTestsParameterUseRot);
            Assert.IsFalse(environment.mocks.testEquipmentUseCase.ShowTestEquipmentsForProcessControlAndRotatingTestsParameterUseCtl);
            Assert.AreEqual(mcaChecked ? TestType.Mfu : TestType.Chk, environment.mocks.transferUseCase.ShowLocationToolAssignmentsTestTypeParameter);
        }

        [TestCase]
        public void ShowRotatingTestCommandSetsTestEquipmentToNull()
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = true;
            environment.viewModel.SelectedTestEquipment = TestEquipmentHumbleModel.GetModelFor(CreateTestEquipment.Randomized(234), null);
            environment.viewModel.ShowRotatingTestCommand.Invoke(null);
            Assert.IsNull(environment.viewModel.SelectedTestEquipment);
        }

        [Test]
        public void ChkTestType4TransferCommandCallsShowLocationToolAssignmentsWithChkTestType()
        {
            var environment = new Environment();
            environment.viewModel.ChkTestType4Transfer.Invoke(null);
            Assert.AreEqual(TestType.Chk, environment.mocks.transferUseCase.ShowLocationToolAssignmentsTestTypeParameter);
        }

        [Test]
        public void ChkTestType4TransferCommandCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.viewModel.ChkTestType4Transfer.Invoke(null);
            Assert.IsTrue(environment.mocks.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [Test]
        public void MfuTestType4TransferCommandCallsShowLocationToolAssignmentsWithChkTestType()
        {
            var environment = new Environment();
            environment.viewModel.MfuTestType4Transfer.Invoke(null);
            Assert.AreEqual(TestType.Mfu, environment.mocks.transferUseCase.ShowLocationToolAssignmentsTestTypeParameter);
        }

        [Test]
        public void MfuTestType4TransferCommandCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.viewModel.MfuTestType4Transfer.Invoke(null);
            Assert.IsTrue(environment.mocks.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        public static List<LocationToolAssignmentForTransferParams>
            ShowingLocationToolAssignmentShowsTableEntryWithContentsTestData =
                new List<LocationToolAssignmentForTransferParams>
                {
                    new LocationToolAssignmentForTransferParams
                    {
                        locationId = 5,
                        locationNumber = "testlocationnumber",
                        locationDescription = "locdesc",
                        locationFreeFieldCategory = "categ",
                        locationFreeFieldDocumentation = false,
                        toolUsageId = 6,
                        toolUsageDescription = "posdesc1",
                        toolId = 7,
                        toolSerialNumber = "toolserno",
                        toolInventoryNumber = "toolinvno",
                        locationToolAssignmentId = 3452,
                        monitoringSamples = 1,
                        monitoringPeriod = 5,
                        monitoringNextCheck = new DateTime(1995, 5, 15),
                        monitoringLastCheck = new DateTime(1994, 5, 15)
                    },
                    new LocationToolAssignmentForTransferParams
                    {
                        locationId = 666,
                        locationNumber = "othernumber",
                        locationDescription = "otherlocdesc",
                        locationFreeFieldCategory = "cat2",
                        locationFreeFieldDocumentation = true,
                        toolUsageId = 7,
                        toolUsageDescription = "posdesc2",
                        toolId = 29,
                        toolSerialNumber = "sertoolno",
                        toolInventoryNumber = "invnotool",
                        locationToolAssignmentId = 7777,
                        monitoringSamples = 2,
                        monitoringPeriod = 6,
                        monitoringNextCheck = new DateTime(1992, 4, 18),
                        monitoringLastCheck = new DateTime(1991, 4, 18)
                    },
                };

        [TestCaseSource(nameof(ShowingLocationToolAssignmentShowsTableEntryWithContentsTestData))]
        public void ShowingLocationToolAssignmentShowsTableEntryWithContents(
            LocationToolAssignmentForTransferParams toolAssignmentParameters)
        {
            var environment = new Environment();

            environment.viewModel.ShowLocationToolAssignmentForTransferList(
                new List<LocationToolAssignmentForTransfer>
                {
                    CreateLocationToolAssignmentForTransferParametrized(toolAssignmentParameters)
                }, TestType.Chk);

            var expectedList =
                new List<LocationToolAssignmentForTransfer>
                {
                    CreateLocationToolAssignmentForTransferParametrized(toolAssignmentParameters)
                };

            CheckerFunctions.CollectionAssertAreEquivalent(
                expectedList,
                environment.viewModel.LocationToolAssignments,
                CompareLocationToolAssignmentEntityToModel);
        }

        [Test]
        public void ShowingLocationToolAssignmentsShowsMoreEntries()
        {
            var environment = new Environment();
            var itemCount = 5;
            var list = new List<LocationToolAssignmentForTransfer>();
            for (int i = 0; i < itemCount; i++)
            {
                list.Add(CreateLocationToolAssignmentForTransferAnonymous());
            }
            environment.viewModel.ShowLocationToolAssignmentForTransferList(list, TestType.Chk);

            Assert.AreEqual(itemCount, environment.viewModel.LocationToolAssignments.Count);
        }

        public static List<LocationToolAssignmentForTransferParams>
            ShowingDifferentLocationToolAssignmentsShowsDifferentValuesTestData =
                new List<LocationToolAssignmentForTransferParams>
                {
                    new LocationToolAssignmentForTransferParams
                    {
                        locationId = 5,
                        locationNumber = "testlocationnumber",
                        locationDescription = "locdesc",
                        locationFreeFieldCategory = "categ",
                        locationFreeFieldDocumentation = false,
                        toolUsageId = 6,
                        toolUsageDescription = "posdesc1",
                        toolId = 7,
                        toolSerialNumber = "toolserno",
                        toolInventoryNumber = "toolinvno",
                        locationToolAssignmentId = 3452,
                        monitoringSamples = 1,
                        monitoringPeriod = 5,
                        monitoringNextCheck = new DateTime(1995, 5, 15),
                        monitoringLastCheck = new DateTime(1994, 5, 15)
                    },
                    new LocationToolAssignmentForTransferParams
                    {
                        locationId = 666,
                        locationNumber = "othernumber",
                        locationDescription = "otherlocdesc",
                        locationFreeFieldCategory = "cat2",
                        locationFreeFieldDocumentation = true,
                        toolUsageId = 7,
                        toolUsageDescription = "posdesc2",
                        toolId = 29,
                        toolSerialNumber = "sertoolno",
                        toolInventoryNumber = "invnotool",
                        locationToolAssignmentId = 7777,
                        monitoringSamples = 2,
                        monitoringPeriod = 6,
                        monitoringNextCheck = new DateTime(1992, 4, 18),
                        monitoringLastCheck = new DateTime(1991, 4, 18)
                    },
                };

        [TestCaseSource(nameof(ShowingDifferentLocationToolAssignmentsShowsDifferentValuesTestData))]
        public void ShowingDifferentLocationToolAssignmentsShowsDifferentValues(
            LocationToolAssignmentForTransferParams locationToolAssignmentTransferParameters)
        {
            var environment = new Environment();
            var list = new List<LocationToolAssignmentForTransfer>
            {
                CreateLocationToolAssignmentForTransferAnonymous(),
                CreateLocationToolAssignmentForTransferParametrized(locationToolAssignmentTransferParameters)
            };

            environment.viewModel.ShowLocationToolAssignmentForTransferList(list, TestType.Chk);

            var expectedLocationToolAssignment = 
                CreateLocationToolAssignmentForTransferParametrized(locationToolAssignmentTransferParameters);
            Assert.IsTrue(
                CompareLocationToolAssignmentEntityToModel(
                    expectedLocationToolAssignment,
                    environment.viewModel.LocationToolAssignments[1]));
        }

        [Test]
        public void ShowingLocationToolAssignmentsClearsPreviousList()
        {
            var environment = new Environment();
            environment.viewModel.ShowLocationToolAssignmentForTransferList(
                new List<LocationToolAssignmentForTransfer>
                {
                    CreateLocationToolAssignmentForTransferAnonymous(),
                    CreateLocationToolAssignmentForTransferAnonymous(),
                    CreateLocationToolAssignmentForTransferAnonymous(),
                    CreateLocationToolAssignmentForTransferAnonymous(),
                }, TestType.Chk);
            environment.viewModel.ShowLocationToolAssignmentForTransferList(
                new List<LocationToolAssignmentForTransfer>
                {
                    CreateLocationToolAssignmentForTransferAnonymous()
                }, TestType.Chk);
            Assert.AreEqual(1, environment.viewModel.LocationToolAssignments.Count);
        }

        [TestCase("abc1234", "drvpath", "compath")]
        [TestCase("serno", "lol", "wat")]
        [TestCase("abc1234", "drvpath", "compath")]
        [TestCase("serno", "lol", "wat")]
        public void SubmittingDataCallsTransferToTestEquipmentWithSelectedTestEquipment(
            string serialNumber,
            string driverPath,
            string communicationPath)
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = true;
            environment.mocks.InterfaceAdapter.SelectedTestEquipment =
                TestEquipmentHumbleModel.GetModelFor(
                    CreateTestEquipment.ParametrizedForTransfer(
                    serialNumber,
                    driverPath,
                    communicationPath), new NullLocalizationWrapper());
            environment.viewModel.SubmitDataToSelectedTestEquipment.Invoke(null);
            var expectedTestEquipment =
                CreateTestEquipment.ParametrizedForTransfer(
                    serialNumber,
                    driverPath,
                    communicationPath);
            Assert.IsTrue(
                expectedTestEquipment.EqualsByContent(environment.mocks.transferUseCase
                    .lastSubmitToTestEquipmentTestEquipmentParameter));
        }

        [TestCase(true, TestType.Chk)]
        [TestCase(false, TestType.Mfu)]
        [TestCase( true, TestType.Chk)]
        [TestCase(false, TestType.Mfu)]
        public void SubmittingDataCallsTransferToTestEquipmentWithCorrectTestType(
            bool chkTestTypeChecked,
            TestType expectedTestType)
        {
            var environment = new Environment();
            environment.viewModel.IsChkTestTypeChecked = chkTestTypeChecked;

            environment.viewModel.SubmitDataToSelectedTestEquipment.Invoke(null);
            Assert.AreEqual(
                expectedTestType,
                environment.mocks.transferUseCase.lastSubmitToTestEquipmentTestTypeParameter);
        }

        public static List<LocationToolAssignmentForTransferParams>
            SubmittingDataWithASelectedItemCallsTransferToTestEquipmentWithCurrentSelectionTestData =
                new List<LocationToolAssignmentForTransferParams>
                {
                    new LocationToolAssignmentForTransferParams
                    {
                        locationId = 5,
                        locationNumber = "testlocationnumber",
                        locationDescription = "locdesc",
                        locationFreeFieldCategory = "categ",
                        locationFreeFieldDocumentation = false,
                        toolUsageId = 6,
                        toolUsageDescription = "posdesc1",
                        toolId = 7,
                        toolSerialNumber = "toolserno",
                        toolInventoryNumber = "toolinvno",
                        locationToolAssignmentId = 3452,
                        monitoringSamples = 1,
                        monitoringPeriod = 5,
                        monitoringNextCheck = new DateTime(1995, 5, 15),
                        monitoringLastCheck = new DateTime(1994, 5, 15)
                    },
                    new LocationToolAssignmentForTransferParams
                    {
                        locationId = 666,
                        locationNumber = "othernumber",
                        locationDescription = "otherlocdesc",
                        locationFreeFieldCategory = "cat2",
                        locationFreeFieldDocumentation = true,
                        toolUsageId = 7,
                        toolUsageDescription = "posdesc2",
                        toolId = 29,
                        toolSerialNumber = "sertoolno",
                        toolInventoryNumber = "invnotool",
                        locationToolAssignmentId = 7777,
                        monitoringSamples = 2,
                        monitoringPeriod = 6,
                        monitoringNextCheck = new DateTime(1992, 4, 18),
                        monitoringLastCheck = new DateTime(1991, 4, 18)
                    },
                };

        [TestCaseSource(nameof(SubmittingDataWithASelectedItemCallsTransferToTestEquipmentWithCurrentSelectionTestData))]
        public void SubmittingDataWithASelectedItemCallsTransferToTestEquipmentWithCurrentSelection(
            LocationToolAssignmentForTransferParams locationToolAssignmentParameters)
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = true;
            var itemList = new List<LocationToolAssignmentForTransfer>
            {
                CreateLocationToolAssignmentForTransferParametrized(locationToolAssignmentParameters)
            };
            environment.viewModel.ShowLocationToolAssignmentForTransferList(itemList, TestType.Chk);
            environment.viewModel.LocationToolAssignments[0].Selected = true;
            environment.viewModel.SubmitDataToSelectedTestEquipment.Invoke(null);
            CheckerFunctions.CollectionAssertAreEquivalent(
                itemList,
                environment.mocks.transferUseCase.lastSubmitToTestEquipmentRouteParameter, 
                CompareLocationToolAssignmentEntityToEntity);
        }
        

        [TestCase(true, true, 2)]
        [TestCase(false, true, 1)]
        [TestCase(false, false, 0)]
        [TestCase(true, true, 2)]
        [TestCase(false, true, 1)]
        [TestCase(false, false, 0)]
        public void SubmittingDataWithMoreSelectedItemsSendsMoreSelectedItems(
            bool firstItemSelection,
            bool secondItemSelection,
            int resultingItemCount)
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = true;
            var itemList = new List<LocationToolAssignmentForTransfer>
            {
                CreateLocationToolAssignmentForTransferAnonymous(),
                CreateLocationToolAssignmentForTransferAnonymous(),
            };
            environment.viewModel.ShowLocationToolAssignmentForTransferList(itemList, TestType.Chk);
            environment.viewModel.LocationToolAssignments[0].Selected = firstItemSelection;
            environment.viewModel.LocationToolAssignments[1].Selected = secondItemSelection;
            environment.viewModel.SubmitDataToSelectedTestEquipment.Invoke(null);
            Assert.AreEqual(
                resultingItemCount,
                environment.mocks.transferUseCase.lastSubmitToTestEquipmentRouteParameter.Count);
        }

        [Test]
        public void ShowLocationToolAssignmentForTransferListCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.viewModel.ShowLocationToolAssignmentForTransferList(
                new List<LocationToolAssignmentForTransfer>(), TestType.Chk);
            Assert.IsFalse(environment.mocks.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [Test]
        public void ReadingTestEquipmentPassesTestEquipmentToUseCase()
        {
            var testEquipment = CreateTestEquipment.Anonymous();
            var environment = new Environment();
            environment.mocks.InterfaceAdapter.SelectedTestEquipment = TestEquipmentHumbleModel.GetModelFor(testEquipment, new NullLocalizationWrapper());
            environment.viewModel.ReadDataToSelectedTestEquipment.Invoke(null);
            Assert.AreSame(
                testEquipment,
                environment.mocks.transferUseCase.lastReadFromTestEquipmentParameterTestEquipment);
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

        [TestCase(true)]
        [TestCase(false)]
        public void IsChkAndMcaTestTypeCheckedReturnsCorrectValue(bool chkTestTypeChecked)
        {
            var environment = new Environment();
            environment.viewModel.IsChkTestTypeChecked = chkTestTypeChecked;
            Assert.AreEqual(chkTestTypeChecked, environment.viewModel.IsChkTestTypeChecked);
            Assert.AreEqual(!chkTestTypeChecked, environment.viewModel.IsMcaTestTypeChecked);
        }

        [Test]
        public void LocationToolAssignmentWithAndWithoutTestLevelSetVisibleReturnsCorrectValue()
        {
            var environment = new Environment();
            environment.viewModel.ProcessTestingChecked = true;
            Assert.IsFalse(environment.viewModel.LocationToolAssignmentWithTestLevelSetVisible);

            environment.viewModel.ProcessTestingChecked = false;

            Assert.IsTrue(environment.viewModel.LocationToolAssignmentWithTestLevelSetVisible);
        }

        private static IEnumerable<List<ProcessControlForTransfer>> ProcessControlForTransferData =
            new List<List<ProcessControlForTransfer>>()
            {
                new List<ProcessControlForTransfer>()
                {
                    CreateProcessControlForTransfer.Randomized(345),
                    CreateProcessControlForTransfer.Randomized(345435)
                }, 
                new List<ProcessControlForTransfer>()
                {
                    CreateProcessControlForTransfer.Randomized(1111)
                }
            };

        [TestCaseSource(nameof(ProcessControlForTransferData))]
        public void ShowProcessControlForTransferListFillsProcessControlForTransferData(List<ProcessControlForTransfer> data)
        {
            var environment = new Environment();
            environment.viewModel.ShowProcessControlForTransferList(data);
            Assert.AreEqual(data.ToList(), environment.viewModel.ProcessControlForTransferData.Select(x => x.GetEntity()).ToList());
        }

        [Test]
        public void ShowProcessControlForTransferListCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.viewModel.ShowProcessControlForTransferList(new List<ProcessControlForTransfer>());
            Assert.IsFalse(environment.mocks.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [Test]
        public void LoadProcessControlData4TransferCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.viewModel.ShowProcessControlCommand.Invoke(null);
            Assert.IsTrue(environment.mocks.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.AreEqual(2, environment.mocks.StartUp.RaiseShowLoadingControlCount);
        }

        [Test]
        public void LoadProcessControlData4TransferCallsUseCae()
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = false;
            environment.viewModel.ShowProcessControlCommand.Invoke(null);
            Assert.AreEqual(environment.viewModel, environment.mocks.testEquipmentUseCase.ShowTestEquipmentsForProcessControlAndRotatingTestsParameterError);
            Assert.IsFalse(environment.mocks.testEquipmentUseCase.ShowTestEquipmentsForProcessControlAndRotatingTestsParameterUseRot);
            Assert.IsTrue(environment.mocks.testEquipmentUseCase.ShowTestEquipmentsForProcessControlAndRotatingTestsParameterUseCtl);
            Assert.IsTrue(environment.mocks.transferUseCase.ShowProcessControlDataCalled);
        }

        [TestCase]
        public void ShowProcessControlCommandSetsTestEquipmentToNull()
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = false;
            environment.viewModel.SelectedTestEquipment = TestEquipmentHumbleModel.GetModelFor(CreateTestEquipment.Randomized(234), null);
            environment.viewModel.ShowProcessControlCommand.Invoke(null);
            Assert.IsNull(environment.viewModel.SelectedTestEquipment);
        }

        [Test]
        public void SubmitDataToSelectedTestEquipmentCallsCorrectUseCaseMethod()
        {
            var environment = new Environment();
            environment.mocks.transferUseCase.lastSubmitToTestEquipmentTestEquipmentParameter = null;
            environment.mocks.transferUseCase.SubmitToTestEquipmentProcessControlTestEquipmentEntity = null;
            environment.viewModel.SelectedTestEquipment = TestEquipmentHumbleModel.GetModelFor(CreateTestEquipment.Randomized(4353), null);

            environment.viewModel.ToolTestingChecked = true;
            environment.viewModel.SubmitDataToSelectedTestEquipment.Invoke(null);
            Assert.IsNotNull(environment.mocks.transferUseCase.lastSubmitToTestEquipmentTestEquipmentParameter);
            Assert.IsNull(environment.mocks.transferUseCase.SubmitToTestEquipmentProcessControlTestEquipmentEntity);

            environment.mocks.transferUseCase.lastSubmitToTestEquipmentTestEquipmentParameter = null;
            environment.mocks.transferUseCase.SubmitToTestEquipmentProcessControlTestEquipmentEntity = null;

            environment.viewModel.ToolTestingChecked = false;
            environment.viewModel.SubmitDataToSelectedTestEquipment.Invoke(null);
            Assert.IsNull(environment.mocks.transferUseCase.lastSubmitToTestEquipmentTestEquipmentParameter);
            Assert.IsNotNull(environment.mocks.transferUseCase.SubmitToTestEquipmentProcessControlTestEquipmentEntity);
        }

        static IEnumerable<(TestEquipment, List<(ProcessControlForTransfer, bool)>)> SubmitProcessControlData = new List<(TestEquipment, List<(ProcessControlForTransfer, bool)>)>()
        {
            (
                CreateTestEquipment.Randomized(123425),
                new List<(ProcessControlForTransfer, bool)>()
                {
                    (
                        CreateProcessControlForTransfer.Randomized(12335),
                        true
                    ),
                    (
                        CreateProcessControlForTransfer.Randomized(76868),
                        false
                    ),
                    (
                        CreateProcessControlForTransfer.Randomized(7667656),
                        true
                    )
                }
            ),
            (
                CreateTestEquipment.Randomized(120097),
                new List<(ProcessControlForTransfer, bool)>()
                {
                    (
                        CreateProcessControlForTransfer.Randomized(9999),
                        true
                    )
                }
            )
        };

        [TestCaseSource(nameof(SubmitProcessControlData))]
        public void SubmitDataToTestEquipmentCallsUseCaseForProcessControlSelectionTest((TestEquipment testEquipment, List<(ProcessControlForTransfer item, bool selected)> processControlForTransferData) data)
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = false;
            environment.viewModel.SelectedTestEquipment = TestEquipmentHumbleModel.GetModelFor(data.testEquipment, null);

            foreach (var item in data.processControlForTransferData)
            {
                var model = new ProcessControlForTransferHumbleModel(item.item, null) {Selected = item.selected};
                environment.viewModel.ProcessControlForTransferData.Add(model);
            }

            environment.viewModel.SubmitDataToSelectedTestEquipment.Invoke(null);

            Assert.AreSame(data.testEquipment, environment.mocks.transferUseCase.SubmitToTestEquipmentProcessControlTestEquipmentEntity);
            Assert.AreEqual(data.processControlForTransferData.Where(x => x.selected).Select(x => x.item).ToList(),
                environment.mocks.transferUseCase.SubmitToTestEquipmentProcessControlRoute);
        }

        [Test]
        public void SubmitDataToTestEquipmentCallsUseCaseForProcessControlCapacityErrorTest()
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = false;
            environment.viewModel.SelectedTestEquipment = TestEquipmentHumbleModel.GetModelFor(CreateTestEquipment.Randomized(23424), null);
            var pc = new ProcessControlForTransferHumbleModel(CreateProcessControlForTransfer.Randomized(345), null);
            pc.HasCapacityError = true;
            pc.Selected = true;
            environment.viewModel.ProcessControlForTransferData.Add(pc);
            environment.viewModel.SubmitDataToSelectedTestEquipment.Invoke(null);
            Assert.AreEqual(0, environment.mocks.transferUseCase.SubmitToTestEquipmentProcessControlRoute.Count);
        }

        private static IEnumerable<(TestEquipment, List<(ProcessControlForTransfer, bool)>)>
            SetProcessControlCapacityData = new List<(TestEquipment, List<(ProcessControlForTransfer, bool)>)>()
            {
                (
                    CreateTestEquipment.RandomizedWithCapacityAndType(23536, 10, 1000, TestEquipmentType.AcqTool),
                    new List<(ProcessControlForTransfer, bool)>()
                    {
                        (
                            CreateProcessControlForTransfer.RandomizedWithMinimumMaximumAndSetPoint(435476,10,30,20),
                            false
                         ),
                        (
                            CreateProcessControlForTransfer.RandomizedWithMinimumMaximumAndSetPoint(435345,0,3000000,2000),
                            false
                        )

                    }
                ),
                (
                    CreateTestEquipment.RandomizedWithCapacityAndType(23536, 1, 100, TestEquipmentType.Wrench),
                    new List<(ProcessControlForTransfer, bool)>()
                    {
                        (
                            CreateProcessControlForTransfer.RandomizedWithMinimumMaximumAndSetPoint(1231,1,100,20),
                            false
                         ),
                        (
                            CreateProcessControlForTransfer.RandomizedWithMinimumMaximumAndSetPoint(45646,0.9,100.1,2000),
                            true
                        )

                    }
                )
            };

        [TestCaseSource(nameof(SetProcessControlCapacityData))]
        public void SelectTestEquipmentSetsProcessControlCapacityErrorCorrect((TestEquipment testEquipment,
            List<(ProcessControlForTransfer processControl, bool hasCapacityError)> processTuples) data)
        {
            var environment = new Environment
            {
                viewModel = { ToolTestingChecked = false }
            };

            foreach (var item in data.processTuples)
            {
                environment.viewModel.ProcessControlForTransferData.Add(new ProcessControlForTransferHumbleModel(item.processControl, null));
            }

            environment.viewModel.SelectedTestEquipment = TestEquipmentHumbleModel.GetModelFor(data.testEquipment, null);

            foreach (var item in data.processTuples)
            {
                var processControlModel =
                    environment.viewModel.ProcessControlForTransferData.SingleOrDefault(x =>
                        ReferenceEquals(x.GetEntity(), item.processControl));

                Assert.IsNotNull(processControlModel);
                Assert.AreEqual(item.hasCapacityError, processControlModel.HasCapacityError);
            }
        }

        [TestCaseSource(nameof(SetProcessControlCapacityData))]
        public void ShowProcessControlForTransferListSetsProcessControlCapacityErrorCorrect((TestEquipment testEquipment,
            List<(ProcessControlForTransfer processControl, bool hasCapacityError)> processTuples) data)
        {
            var environment = new Environment
            {
                viewModel = { ToolTestingChecked = false }
            };
            environment.viewModel.SelectedTestEquipment = TestEquipmentHumbleModel.GetModelFor(data.testEquipment, null);

            foreach (var item in data.processTuples)
            {
                environment.viewModel.ProcessControlForTransferData.Add(new ProcessControlForTransferHumbleModel(item.processControl, null));
            }
            environment.viewModel.ShowProcessControlForTransferList(data.processTuples.Select(x => x.processControl).ToList());


            foreach (var item in data.processTuples)
            {
                var processControlModel =
                    environment.viewModel.ProcessControlForTransferData.SingleOrDefault(x =>
                        ReferenceEquals(x.GetEntity(), item.processControl));

                Assert.IsNotNull(processControlModel);
                Assert.AreEqual(item.hasCapacityError, processControlModel.HasCapacityError);
            }
        }

        [Test]
        public void SelectTestEquipmentWithNullSetsProcessControlHasCapacityErrorToFalse()
        {
            var environment = new Environment
            {
                viewModel = {ToolTestingChecked = false}
            };

            var model = new ProcessControlForTransferHumbleModel(CreateProcessControlForTransfer.Randomized(345),null)
            {
                HasCapacityError = true
            };
            environment.viewModel.ProcessControlForTransferData.Add(model);

            environment.viewModel.SelectedTestEquipment = null;

            Assert.IsFalse(model.HasCapacityError);
        }

        [Test]
        public void ShowProcessControlForTransferListWithNoSelectedTestEquipmentSetsProcessControlHasCapacityErrorToFalse()
        {
            var environment = new Environment
            {
                viewModel = { ToolTestingChecked = false }
            };

            var data = CreateProcessControlForTransfer.Randomized(345);
            environment.viewModel.ShowProcessControlForTransferList(new List<ProcessControlForTransfer>(){ data });

            Assert.IsFalse(environment.viewModel.ProcessControlForTransferData.First().HasCapacityError);
        }

        [Test]
        public void SelectTestEquipmentWithToolTestingCheckedSetsProcessControlHasCapacityErrorToFalse()
        {
            var environment = new Environment
            {
                viewModel = {ToolTestingChecked = true}
            };

            var model = new ProcessControlForTransferHumbleModel(CreateProcessControlForTransfer.RandomizedWithMinimumMaximumAndSetPoint(345, 0,100,30), null)
            {
                HasCapacityError = true
            };

            environment.viewModel.ProcessControlForTransferData.Add(model);

            environment.viewModel.SelectedTestEquipment = TestEquipmentHumbleModel.GetModelFor(CreateTestEquipment.RandomizedWithCapacity(64576, 0,100), null);

            Assert.IsFalse(model.HasCapacityError);
        }

        [Test]
        public void ShowProcessControlForTransferListWithToolTestingCheckedSetsProcessControlHasCapacityErrorToFalse()
        {
            var environment = new Environment
            {
                viewModel = { ToolTestingChecked = true }
            };
            environment.viewModel.SelectedTestEquipment = TestEquipmentHumbleModel.GetModelFor(CreateTestEquipment.RandomizedWithCapacity(64576, 0, 100), null);

            var data = CreateProcessControlForTransfer.RandomizedWithMinimumMaximumAndSetPoint(345, 0, 100, 30);
            environment.viewModel.ShowProcessControlForTransferList(new List<ProcessControlForTransfer>() { data });


            Assert.IsFalse(environment.viewModel.ProcessControlForTransferData.First().HasCapacityError);
        }

        private static
            IEnumerable<(List<LocationToolAssignmentForTransfer>, List<ProcessControlForTransfer>, bool, bool, bool)>
            SelectDeselectCommandData =
                new List<(List<LocationToolAssignmentForTransfer>, List<ProcessControlForTransfer>, bool, bool, bool)>()
                {
                    (
                        new List<LocationToolAssignmentForTransfer>()
                        {
                            CreateLocationToolAssignmentForTransferAnonymous(),
                            CreateLocationToolAssignmentForTransferAnonymous(),
                            CreateLocationToolAssignmentForTransferAnonymous()
                        },
                        new List<ProcessControlForTransfer>()
                        {
                            CreateProcessControlForTransfer.Randomized(1123),
                            CreateProcessControlForTransfer.Randomized(112343)
                        }, 
                        true, true, false   
                    ),
                    (
                        new List<LocationToolAssignmentForTransfer>()
                        {
                            CreateLocationToolAssignmentForTransferAnonymous(),
                            CreateLocationToolAssignmentForTransferAnonymous()
                        },
                        new List<ProcessControlForTransfer>()
                        {
                            CreateProcessControlForTransfer.Randomized(1123),
                            CreateProcessControlForTransfer.Randomized(112343),
                            CreateProcessControlForTransfer.Randomized(234)
                        },
                        false, false, true
                    ),
                };

        [TestCaseSource(nameof(SelectDeselectCommandData))]
        public void SelectCommandSelectsItems((List<LocationToolAssignmentForTransfer> locToolData, List<ProcessControlForTransfer> processData, 
            bool toolTestingChecked, bool locToolSelected, bool processSelected) data)
        {
            var environment = new Environment();
            foreach (var item in data.locToolData)
            {
                environment.viewModel.LocationToolAssignments.Add(new LocationToolAssignmentForTransferHumbleModel(item) { Selected = false });
            }

            foreach (var item in data.processData)
            {
                environment.viewModel.ProcessControlForTransferData.Add(new ProcessControlForTransferHumbleModel(item, null) { Selected = false });
            }

            environment.viewModel.ToolTestingChecked = data.toolTestingChecked;
            environment.viewModel.SelectCommand.Invoke(null);

            foreach (var item in environment.viewModel.LocationToolAssignments)
            {
                Assert.AreEqual(data.locToolSelected, item.Selected);
            }

            foreach (var item in environment.viewModel.ProcessControlForTransferData)
            {
                Assert.AreEqual(data.processSelected,item.Selected);
            }
        }

        [TestCaseSource(nameof(SelectDeselectCommandData))]
        public void DeselectCommandSelectsItems((List<LocationToolAssignmentForTransfer> locToolData, List<ProcessControlForTransfer> processData,
            bool toolTestingChecked, bool locToolSelected, bool processSelected) data)
        {
            var environment = new Environment();
            foreach (var item in data.locToolData)
            {
                environment.viewModel.LocationToolAssignments.Add(new LocationToolAssignmentForTransferHumbleModel(item) { Selected = true });
            }

            foreach (var item in data.processData)
            {
                environment.viewModel.ProcessControlForTransferData.Add(new ProcessControlForTransferHumbleModel(item, null) { Selected = true });
            }

            environment.viewModel.ToolTestingChecked = data.toolTestingChecked;
            environment.viewModel.DeselectCommand.Invoke(null);

            foreach (var item in environment.viewModel.LocationToolAssignments)
            {
                Assert.AreEqual(!data.locToolSelected, item.Selected);
            }

            foreach (var item in environment.viewModel.ProcessControlForTransferData)
            {
                Assert.AreEqual(!data.processSelected, item.Selected);
            }
        }

        [Test]
        public void HasCapacityErrorLegendVisibleReturnsCorrectValue()
        {
            var environment = new Environment();
            environment.viewModel.ToolTestingChecked = false;
            environment.viewModel.ProcessControlForTransferData.Add(new ProcessControlForTransferHumbleModel(CreateProcessControlForTransfer.RandomizedWithMinimumMaximumAndSetPoint(1234,20,40,30), null));
            environment.viewModel.ProcessControlForTransferData.Add(new ProcessControlForTransferHumbleModel(CreateProcessControlForTransfer.RandomizedWithMinimumMaximumAndSetPoint(234, 20, 400, 300), null));

            Assert.IsFalse(environment.viewModel.HasCapacityErrorLegendVisible);

            environment.viewModel.SelectedTestEquipment = TestEquipmentHumbleModel.GetModelFor(CreateTestEquipment.RandomizedWithCapacityAndType(2134,0,70, TestEquipmentType.Wrench),null);

            Assert.IsTrue(environment.viewModel.HasCapacityErrorLegendVisible);

            environment.viewModel.SelectedTestEquipment = TestEquipmentHumbleModel.GetModelFor(CreateTestEquipment.RandomizedWithCapacityAndType(345, 0, 70000, TestEquipmentType.Wrench), null);

            Assert.IsFalse(environment.viewModel.HasCapacityErrorLegendVisible);
        }

        public class TestEquipmentUseCaseMock : ITestEquipmentUseCase
        {
            public void ShowTestEquipments(ITestEquipmentErrorGui loadingError)
            {
                ShowTestEquipmentsCalled = true;
                ShowTestEquipmentsParameter = loadingError;
            }

            public void ShowTestEquipmentsForProcessControlAndRotatingTests(ITestEquipmentErrorGui loadingError, bool forProcessControl,
                bool forRotatingTests)
            {
                ShowTestEquipmentsForProcessControlAndRotatingTestsParameterError = loadingError;
                ShowTestEquipmentsForProcessControlAndRotatingTestsParameterUseCtl = forProcessControl;
                ShowTestEquipmentsForProcessControlAndRotatingTestsParameterUseRot = forRotatingTests;
            }

            public void SaveTestEquipmentModel(TestEquipmentModelDiff diff, ITestEquipmentErrorGui errorHandler, ITestEquipmentSaveGuiShower saveGuiShower)
            {
                SaveTestEquipmentModelParameterDiff = diff;
                SaveTestEquipmentModelParameterGuiError = errorHandler;
                SaveTestEquipmentModelSaveGuiShower = saveGuiShower;
            }

            public void SaveTestEquipment(TestEquipmentDiff diff, ITestEquipmentErrorGui errorHandler,
                ITestEquipmentSaveGuiShower saveGuiShower)
            {
                SaveTestEquipmentParameterDiff = diff;
                SaveTestEquipmentParameterGuiError = errorHandler;
                SaveTestEquipmentSaveGuiShower = saveGuiShower;
            }

            public void UpdateTestEquipment(TestEquipmentDiff diff, ITestEquipmentErrorGui errorHandler)
            {
                UpdateTestEquipmentDiffParameter = diff;
                UpdateTestEquipmentErrorParameter = errorHandler;
            }

            public void UpdateTestEquipmentModel(TestEquipmentModelDiff diff, ITestEquipmentErrorGui errorHandler)
            {
                UpdateTestEquipmentModelDiffParameter = diff;
                UpdateTestEquipmentModelErrorParameter = errorHandler;
            }

            public void RemoveTestEquipment(TestEquipment testEquipment, ITestEquipmentErrorGui errorHandler)
            {
                RemoveTestEquipmentParameterTestEquipment = testEquipment;
                RemoveTestEquipmentParameterErrorHandler = errorHandler;
            }
           
            public void AddTestEquipment(TestEquipment testEquipment, ITestEquipmentErrorGui errorHandler)
            {
                AddTestEquipmentParameterTestEquipment = testEquipment;
                AddTestEquipmentParameterErrorHandler = errorHandler;
            }

            public bool IsInventoryNumberUnique(string inventoryNumber)
            {
                IsInventoryNumberUniqueParameter = inventoryNumber;
                return IsInventoryNumberUniqueReturnValue;
            }

            public bool IsSerialNumberUnique(string serialNumber)
            {
                IsSerialNumberUniqueParameter = serialNumber;
                return IsSerialNumberUniqueReturnValue;
            }

            public void LoadTestEquipmentModels(ITestEquipmentErrorGui errorHandler)
            {
                LoadTestEquipmentModelsParameter = errorHandler;
            }

            public List<TestEquipmentType> LoadAvailableTestEquipmentTypes()
            {
                return LoadAvailableTestEquipmentTypesReturnValue;
            }

            public List<TestEquipmentType> LoadAvailableTestEquipmentTypesReturnValue { get; set; }
            public ITestEquipmentErrorGui LoadTestEquipmentModelsParameter { get; set; }
            public ITestEquipmentErrorGui ShowTestEquipmentsParameter { get; set; }
            public bool ShowTestEquipmentsCalled { get; set; }
            public ITestEquipmentErrorGui SaveTestEquipmentModelParameterGuiError { get; set; }
            public TestEquipmentModelDiff SaveTestEquipmentModelParameterDiff { get; set; }
            public ITestEquipmentSaveGuiShower SaveTestEquipmentModelSaveGuiShower { get; set; }
            public ITestEquipmentErrorGui SaveTestEquipmentParameterGuiError { get; set; }
            public TestEquipmentDiff SaveTestEquipmentParameterDiff { get; set; }
            public ITestEquipmentSaveGuiShower SaveTestEquipmentSaveGuiShower { get; set; }
            public ITestEquipmentErrorGui RemoveTestEquipmentParameterErrorHandler { get; set; }
            public TestEquipment RemoveTestEquipmentParameterTestEquipment { get; set; }
            public ITestEquipmentErrorGui AddTestEquipmentParameterErrorHandler { get; set; }
            public TestEquipment AddTestEquipmentParameterTestEquipment { get; set; }
            public bool IsSerialNumberUniqueReturnValue { get; set; }
            public string IsSerialNumberUniqueParameter { get; set; }
            public bool IsInventoryNumberUniqueReturnValue { get; set; }
            public string IsInventoryNumberUniqueParameter { get; set; }
            public ITestEquipmentErrorGui UpdateTestEquipmentErrorParameter { get; set; }
            public TestEquipmentDiff UpdateTestEquipmentDiffParameter { get; set; }
            public ITestEquipmentErrorGui UpdateTestEquipmentModelErrorParameter { get; set; }
            public TestEquipmentModelDiff UpdateTestEquipmentModelDiffParameter { get; set; }
            public bool ShowTestEquipmentsForProcessControlAndRotatingTestsParameterUseRot { get; set; }
            public bool ShowTestEquipmentsForProcessControlAndRotatingTestsParameterUseCtl { get; set; }
            public ITestEquipmentErrorGui ShowTestEquipmentsForProcessControlAndRotatingTestsParameterError { get; set; }
        }

        private class TransferToTestEquipmentUseCaseMock : ITransferToTestEquipmentUseCase
        {
            public void ShowLocationToolAssignments(TestType testType)
            {
                ShowLocationToolAssignmentsTestTypeParameter = testType;
            }

            public void SubmitToTestEquipment(TestEquipment testEquipment,
                List<LocationToolAssignmentForTransfer> route, TestType testType)
            {
                lastSubmitToTestEquipmentTestEquipmentParameter = testEquipment;
                lastSubmitToTestEquipmentRouteParameter = route;
                lastSubmitToTestEquipmentTestTypeParameter = testType;
            }

            public void SubmitToTestEquipment(TestEquipment entity, List<ProcessControlForTransfer> processControlForTransfers)
            {
                SubmitToTestEquipmentProcessControlTestEquipmentEntity = entity;
                SubmitToTestEquipmentProcessControlRoute = processControlForTransfers;
            }

            public void ReadFromTestEquipment(TestEquipment testEquipment)
            {
                lastReadFromTestEquipmentParameterTestEquipment = testEquipment;
            }

            public void ShowProcessControlData()
            {
                ShowProcessControlDataCalled = true;
            }

            public bool ShowProcessControlDataCalled;

            public TestType ShowLocationToolAssignmentsTestTypeParameter;
            public TestEquipment lastSubmitToTestEquipmentTestEquipmentParameter = null;
            public List<LocationToolAssignmentForTransfer> lastSubmitToTestEquipmentRouteParameter;
            public TestType lastSubmitToTestEquipmentTestTypeParameter;
            public TestEquipment lastReadFromTestEquipmentParameterTestEquipment = null;
            public TestEquipment SubmitToTestEquipmentProcessControlTestEquipmentEntity;
            public List<ProcessControlForTransfer> SubmitToTestEquipmentProcessControlRoute;
        }

        public class LocationToolAssignmentForTransferParams
        {
            public long locationId;
            public string locationNumber;
            public string locationDescription;
            public string locationFreeFieldCategory;
            public bool locationFreeFieldDocumentation;
            public long toolUsageId;
            public string toolUsageDescription;
            public long toolId;
            public string toolSerialNumber;
            public string toolInventoryNumber;
            public long locationToolAssignmentId;
            public long monitoringSamples;
            public long monitoringPeriod;
            public DateTime monitoringNextCheck;
            public DateTime monitoringLastCheck;
        }

        private static LocationToolAssignmentForTransfer CreateLocationToolAssignmentForTransferAnonymous()
        {
            return CreateLocationToolAssignmentForTransferParametrized(
                0,
                "",
                "",
                "",
                false,
                0,
                "",
                0,
                "",
                "",
                0,
                0,
                0,
                new DateTime(),
                new DateTime());
        }

        private static LocationToolAssignmentForTransfer CreateLocationToolAssignmentForTransferParametrized(
            LocationToolAssignmentForTransferParams parameters)
        {
            return CreateLocationToolAssignmentForTransferParametrized(
                parameters.locationId,
                parameters.locationNumber,
                parameters.locationDescription,
                parameters.locationFreeFieldCategory,
                parameters.locationFreeFieldDocumentation,
                parameters.toolUsageId,
                parameters.toolUsageDescription,
                parameters.toolId,
                parameters.toolSerialNumber,
                parameters.toolInventoryNumber,
                parameters.locationToolAssignmentId,
                parameters.monitoringSamples,
                parameters.monitoringPeriod,
                parameters.monitoringNextCheck,
                parameters.monitoringLastCheck
            );
        }

        private static LocationToolAssignmentForTransfer CreateLocationToolAssignmentForTransferParametrized(
            long locationId,
            string locationNumber,
            string locationDescription, 
            string locationFreeFieldCategory, 
            bool locationFreeFieldDocumentation, 
            long toolUsageId, 
            string toolUsageDescription,
            long toolId,
            string toolSerialNumber,
            string toolInventoryNumber,
            long locationToolAssignmentId,
            long monitoringSamples,
            long monitoringPeriod,
            DateTime monitoringNextCheck,
            DateTime monitoringLastCheck)
        {
            return new LocationToolAssignmentForTransfer
            {
                LocationId = new LocationId(locationId),
                LocationNumber = new LocationNumber(locationNumber),
                LocationDescription = new LocationDescription(locationDescription),
                LocationFreeFieldCategory = locationFreeFieldCategory,
                LocationFreeFieldDocumentation = locationFreeFieldDocumentation,
                ToolUsageId = new HelperTableEntityId(toolUsageId),
                ToolUsageDescription = new ToolUsageDescription(toolUsageDescription),
                ToolId = new ToolId(toolId),
                ToolSerialNumber = toolSerialNumber,
                ToolInventoryNumber = toolInventoryNumber,
                LocationToolAssignmentId = new LocationToolAssignmentId(locationToolAssignmentId),
                SampleNumber = (int)monitoringSamples,
                TestInterval = new Interval() { IntervalValue = monitoringPeriod },
                NextTestDate = monitoringNextCheck,
                LastTestDate = monitoringLastCheck
            };
        }

        public static bool CompareLocationToolAssignmentEntityToModel(
            LocationToolAssignmentForTransfer entity,
            LocationToolAssignmentForTransferHumbleModel viewModelItem)
        {
            return
                entity.LocationId.ToLong().Equals(viewModelItem.LocationId)
                && entity.ToolUsageId.ToLong().Equals(viewModelItem.ToolUsageId)
                && entity.ToolUsageDescription.ToDefaultString().Equals(viewModelItem.ToolUsageDescription)
                && entity.LocationNumber.ToDefaultString().Equals(viewModelItem.LocationNumber)
                && entity.LocationDescription.ToDefaultString().Equals(viewModelItem.LocationDescription)
                && entity.LocationFreeFieldCategory.Equals(viewModelItem.LocationFreeFieldCategory)
                && entity.LocationFreeFieldDocumentation.Equals(viewModelItem.LocationFreeFieldDocumentation)
                && entity.ToolId.ToLong().Equals(viewModelItem.ToolId)
                && entity.ToolSerialNumber.Equals(viewModelItem.ToolSerialNumber)
                && entity.ToolInventoryNumber.Equals(viewModelItem.ToolInventoryNumber)
                && entity.LocationToolAssignmentId.ToLong().Equals(viewModelItem.LocationToolAssignmentId)
                && entity.SampleNumber.Equals(viewModelItem.SampleNumber)
                && entity.TestInterval.EqualsByContent(viewModelItem.TestInterval.Entity)
                && entity.NextTestDate.Equals(viewModelItem.NextTestDate)
                && entity.LastTestDate.Equals(viewModelItem.LastTestDate);
        }

        private static bool CompareLocationToolAssignmentEntityToEntity(
            LocationToolAssignmentForTransfer lhs,
            LocationToolAssignmentForTransfer rhs)
        {
            return
                lhs.LocationId.ToLong().Equals(rhs.LocationId.ToLong())
                && lhs.ToolUsageId.Equals(rhs.ToolUsageId)
                && lhs.ToolUsageDescription.Equals(rhs.ToolUsageDescription)
                && lhs.LocationNumber.ToDefaultString().Equals(rhs.LocationNumber.ToDefaultString())
                && lhs.LocationDescription.ToDefaultString().Equals(rhs.LocationDescription.ToDefaultString())
                && lhs.LocationFreeFieldCategory.Equals(rhs.LocationFreeFieldCategory)
                && lhs.LocationFreeFieldDocumentation.Equals(rhs.LocationFreeFieldDocumentation)
                && lhs.ToolId.ToLong().Equals(rhs.ToolId.ToLong())
                && lhs.ToolSerialNumber.Equals(rhs.ToolSerialNumber)
                && lhs.ToolInventoryNumber.Equals(rhs.ToolInventoryNumber)
                && lhs.LocationToolAssignmentId.Equals(rhs.LocationToolAssignmentId)
                && lhs.SampleNumber.Equals(rhs.SampleNumber)
                && lhs.TestInterval.EqualsByContent(rhs.TestInterval)
                && lhs.NextTestDate.Equals(rhs.NextTestDate)
                && lhs.LastTestDate.Equals(rhs.LastTestDate);
        }

        public class TestEquipmentInterfaceAdapterMock: ITestEquipmentInterface
        {
            public event PropertyChangedEventHandler PropertyChanged;
            public TestEquipmentHumbleModel SelectedTestEquipment { get; set; }
            public TestEquipmentHumbleModel SelectedTestEquipmentWithoutChanges { get; set; }
            public TestEquipmentModelHumbleModel SelectedTestEquipmentModel { get; set; }
            public TestEquipmentModelHumbleModel SelectedTestEquipmentModelWithoutChanges { get; set; }
            private ObservableCollection<TestEquipmentModelHumbleModel> _testEquipmentModels;
            public ObservableCollection<TestEquipmentModelHumbleModel> TestEquipmentModels
            {
                get => _testEquipmentModels;
                set
                {
                    _testEquipmentModels = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TestEquipmentModels)));
                }
            }

            public ObservableCollection<DataGateVersion> DataGateVersions { get; set; }
            public ObservableCollection<TestEquipmentHumbleModel> TestEquipments { get; }
            public void SetDispatcher(Dispatcher dispatcher)
            {
                SetDispatcherParameter = dispatcher;
            }

            public Dispatcher SetDispatcherParameter { get; set; }

            public event EventHandler<bool> ShowLoadingControlRequest;
            public event EventHandler<TestEquipmentHumbleModel> SelectionRequestTestEquipment;
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    localization = new NullLocalizationWrapper();
                    testEquipmentUseCase = new TestEquipmentUseCaseMock();
                    transferUseCase = new TransferToTestEquipmentUseCaseMock();
                    InterfaceAdapter = new TestEquipmentInterfaceAdapterMock();
                    StartUp = new StartUpMock();
                }

                public TestEquipmentUseCaseMock testEquipmentUseCase;
                public TransferToTestEquipmentUseCaseMock transferUseCase;
                public NullLocalizationWrapper localization;
                public TestEquipmentInterfaceAdapterMock InterfaceAdapter;
                public StartUpMock StartUp;
            }

            public Environment()
            {
                mocks = new Mocks();
                viewModel = new TransferToTestEquipmentViewModel(
                    mocks.testEquipmentUseCase,
                    mocks.transferUseCase,
                    mocks.localization,
                    mocks.InterfaceAdapter, 
                    mocks.StartUp);
            }

            public Mocks mocks;
            public TransferToTestEquipmentViewModel viewModel;
        }
    }
}
