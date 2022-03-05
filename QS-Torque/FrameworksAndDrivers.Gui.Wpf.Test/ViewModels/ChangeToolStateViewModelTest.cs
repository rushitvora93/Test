using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.Test.ViewModels.Mock;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using InterfaceAdapters.Models;
using TestHelper.Factories;
using TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    public class ChangeToolStateViewModelTest
    {

        private static List<List<LocationToolAssignmentChangeStateModel>> SetAssignedToolsTestData =
            new List<List<LocationToolAssignmentChangeStateModel>>
            {
                new List<LocationToolAssignmentChangeStateModel>
                {
                    new LocationToolAssignmentChangeStateModel(Dispatcher.CurrentDispatcher,
                        CreateLocationToolAssignment.IdOnly(15), "Blub", "Blub",
                        new Status {ListId = new HelperTableEntityId(15)}, null),
                    new LocationToolAssignmentChangeStateModel(Dispatcher.CurrentDispatcher,
                        CreateLocationToolAssignment.IdOnly(36), "Test", "Test",
                        new Status {ListId = new HelperTableEntityId(36)}, null),
                    new LocationToolAssignmentChangeStateModel(Dispatcher.CurrentDispatcher,
                        CreateLocationToolAssignment.IdOnly(82), "BTest", "BTest",
                        new Status {ListId = new HelperTableEntityId(82)}, null),
                },
                new List<LocationToolAssignmentChangeStateModel>
                {
                    new LocationToolAssignmentChangeStateModel(Dispatcher.CurrentDispatcher,
                        CreateLocationToolAssignment.IdOnly(15), "Blub", "Blub",
                        new Status {ListId = new HelperTableEntityId(15)}, null),
                    new LocationToolAssignmentChangeStateModel(Dispatcher.CurrentDispatcher,
                        CreateLocationToolAssignment.IdOnly(36), "Test", "Test",
                        new Status {ListId = new HelperTableEntityId(36)}, null),
                    new LocationToolAssignmentChangeStateModel(Dispatcher.CurrentDispatcher,
                        CreateLocationToolAssignment.IdOnly(82), "BTest", "BTest",
                        new Status {ListId = new HelperTableEntityId(82)}, null),
                    new LocationToolAssignmentChangeStateModel(Dispatcher.CurrentDispatcher,
                        CreateLocationToolAssignment.IdOnly(74), "Blub", "Blub",
                        new Status {ListId = new HelperTableEntityId(74)}, null),
                    new LocationToolAssignmentChangeStateModel(Dispatcher.CurrentDispatcher,
                        CreateLocationToolAssignment.IdOnly(91), "Test", "Test",
                        new Status {ListId = new HelperTableEntityId(91)}, null),
                    new LocationToolAssignmentChangeStateModel(Dispatcher.CurrentDispatcher,
                        CreateLocationToolAssignment.IdOnly(3), "BTest", "BTest",
                        new Status {ListId = new HelperTableEntityId(3)}, null),
                },
            };

        [Test]
        [TestCaseSource(nameof(SetAssignedToolsTestData))]
        public void SetAssignedToolsTest(List<LocationToolAssignmentChangeStateModel> testData)
        {
            var viewModel = CreateViewModel();
            viewModel.SetAssignedTools(testData);
            CollectionAssert.AreEquivalent(testData, viewModel.AssignedToolsCollectionView.SourceCollection);
        }

        [Test]
        public void SaveNewToolStatesTest()
        {
            ChangeToolStateForLocationUseCaseMock changeToolStateForLocationUseCaseMock = new ChangeToolStateForLocationUseCaseMock(null, null, null);
            var changeToolStateViewModel =
                CreateViewModel(changeToolStateForLocationUseCase: changeToolStateForLocationUseCaseMock);
            changeToolStateViewModel.SaveNewToolStates();
            Assert.AreEqual(1, changeToolStateForLocationUseCaseMock.SetNewToolStatesCall);
        }

        [Test]
        public void FillResultObjectReturnsNullOnNullParameter()
        {
            var viewModel = CreateViewModel();
            Assert.IsNull(viewModel.FillResultObject(null));
        }

        private static List<LocationToolAssignmentChangeStateModel> FillResultObjectChangesStateIdsOfLocationToolAssignmentData = new List<LocationToolAssignmentChangeStateModel>()
        {
            new LocationToolAssignmentChangeStateModel(
                Dispatcher.CurrentDispatcher,
                CreateLocationToolAssignment.Anonymous(), "Blub", "Blub",
                new Status {ListId = new HelperTableEntityId(15), Value = new StatusDescription("Test")}, null),
            new LocationToolAssignmentChangeStateModel(
                Dispatcher.CurrentDispatcher,
                CreateLocationToolAssignment.Anonymous(), "Blub", "Blub",
                new Status {ListId = new HelperTableEntityId(36), Value = new StatusDescription("Test")}, null)
        };

        [Test]
        [TestCaseSource(nameof(FillResultObjectChangesStateIdsOfLocationToolAssignmentData))]
        public void FillResultObjectChangesStateIdsOfLocationToolAssignment(LocationToolAssignmentChangeStateModel data)
        {
            var viewModel = CreateViewModel();
            var locationToolAssignment = data.LocationToolAssignment;
            var status = data.Status.Entity;
            viewModel.AssignedToolsCollectionView.AddNewItem(data);
            var result = viewModel.FillResultObject(new List<LocationToolAssignment> { locationToolAssignment });
            Assert.AreEqual(status, result.FirstOrDefault().AssignedTool.Status);
        }

        [Test]
        public void ShowItemsWithNullListDoesNothing()
        {
            var viewModel = CreateViewModel();
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.ShowItems(null);
            Assert.AreEqual(0, viewModel.ToolStatusCollectionView.Count);
        }

        private static List<List<Status>> ShowItemsAddsStatusToToolStatusCollectionViewData = new List<List<Status>>
        {
            {
                new List<Status>
                {
                    new Status {ListId = new HelperTableEntityId(15), Value = new StatusDescription("Test")},
                    new Status {ListId = new HelperTableEntityId(35), Value = new StatusDescription("Blub")},
                    new Status {ListId = new HelperTableEntityId(98), Value = new StatusDescription("hans")}
                }
            },
            {
                new List<Status>
                {
                    new Status {ListId = new HelperTableEntityId(15), Value = new StatusDescription("Test")}
                }
            }
        };

        [Test]
        [TestCaseSource(nameof(ShowItemsAddsStatusToToolStatusCollectionViewData))]
        public void ShowItemsAddsStatusToToolStatusCollectionView(List<Status> statuses)
        {
            var viewModel = CreateViewModel();
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.ShowItems(statuses);
            foreach (var status in statuses)
            {
                Assert.IsNotNull(
                    viewModel.ToolStatusCollectionView.FirstOrDefault(x => x.ListId == status.ListId.ToLong()));
            }
        }

        [Test]
        [TestCase(36)]
        [TestCase(98)]
        public void AddAddsStatusToToolStatusCollectionView(long id)
        {
            var viewModel = CreateViewModel();
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.Add(new Status { ListId = new HelperTableEntityId(id) });
            Assert.IsNotNull(viewModel.ToolStatusCollectionView.FirstOrDefault(x => x.ListId == id));
        }

        [Test]
        [TestCase(36, 3)]
        [TestCase(98, 6)]
        public void RemoveRemovesStatusToToolStatusCollectionView(long id, int count)
        {
            var viewModel = CreateViewModel();
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            var status = new Status { ListId = new HelperTableEntityId(id) };
            for (int i = 0; i < count; i++)
            {
                viewModel.ToolStatusCollectionView.Add(
                    HelperTableItemModel.GetModelForStatus(new Status { ListId = new HelperTableEntityId(i) }));
            }
            viewModel.ToolStatusCollectionView.Add(HelperTableItemModel.GetModelForStatus(status));
            Assert.AreEqual(count + 1, viewModel.ToolStatusCollectionView.Count);
            viewModel.Remove(status);
            Assert.AreEqual(count, viewModel.ToolStatusCollectionView.Count);
        }

        [Test]
        [TestCase("blub")]
        [TestCase("Hanse")]
        public void SaveStatusUpdatesValueOfItem(string updatedValue)
        {
            var viewModel = CreateViewModel();
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            var status = new Status { ListId = new HelperTableEntityId(15), Value = new StatusDescription("aaaa") };
            viewModel.ToolStatusCollectionView.Add(HelperTableItemModel.GetModelForStatus(status));
            status.Value = new StatusDescription(updatedValue);
            viewModel.Save(status);
            Assert.AreEqual(updatedValue, viewModel.ToolStatusCollectionView.FirstOrDefault().Value);
        }

        [Test]
        [TestCase(3, "blub")]
        [TestCase(6, "hans")]
        public void ShowLocationsShowLocationsForTools(int referenceCount, string name)
        {
            //Aragnge
            var viewModel = CreateViewModel();
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            var testSetupData = SetupShowLocationShowLocationsForToolsData(viewModel, referenceCount, name);
            
            //Act
            viewModel.ShowLocationsForTools(testSetupData.dictionary);
            string result = "";
            for (int i = 0; i < referenceCount; i++)
            {
                result += name + "\r\n";
            }

            //Assert
            Assert.AreEqual(result, testSetupData.locationToolAssignmentChangeStateModel.OtherConnectedLocations);
        }

        private (Tool tool, Location location, LocationToolAssignment locationToolAssignment, 
            LocationToolAssignmentChangeStateModel locationToolAssignmentChangeStateModel, Dictionary<Tool, List<LocationReferenceLink>> dictionary, 
            MockLocationDisplayFormatter mockFormatter) SetupShowLocationShowLocationsForToolsData(ChangeToolStateViewModel viewModel, int referenceCount, string name)
        {
            var tool = CreateTool.Anonymous();
            var location = CreateLocation.Anonymous();
            var locationToolAssignment = CreateLocationToolAssignment.Anonymous();
            locationToolAssignment.AssignedTool = tool;
            locationToolAssignment.AssignedLocation = location;
            var locationToolAssignmentChangeStateModel = new LocationToolAssignmentChangeStateModel(
                Dispatcher.CurrentDispatcher, locationToolAssignment, "", "",
                new Status { ListId = new HelperTableEntityId(15) }, null);
            viewModel.AssignedToolsCollectionView.AddNewItem(locationToolAssignmentChangeStateModel);
            var dictionary = new Dictionary<Tool, List<LocationReferenceLink>>();
            var mockFormatter = new MockLocationDisplayFormatter();
            mockFormatter.DisplayString = name;
            List<LocationReferenceLink> locationReferenceLinks = new List<LocationReferenceLink>();
            for (int i = 0; i < referenceCount; i++)
            {
                locationReferenceLinks.Add(new LocationReferenceLink(new QstIdentifier(i), new LocationNumber("Test"),
                    new LocationDescription("Test"), mockFormatter));
            }
            dictionary.Add(tool, locationReferenceLinks);
            return (tool, location, locationToolAssignment, locationToolAssignmentChangeStateModel, dictionary,
                mockFormatter);
        }

        [Test]
        public void ShowErrorForSaveToolStatesInvokesMessageBoxRequest()
        {
            var viewModel = CreateViewModel();
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.MessageBoxRequest += (sender, args) => Assert.Pass();
            viewModel.ShowErrorForSaveToolStates();
            Assert.Fail();
        }

        private ChangeToolStateViewModel CreateViewModel(IStartUp startUp = null, IChangeToolStateForLocationUseCase changeToolStateForLocationUseCase = null, NullLocalizationWrapper nullLocalizationWrapper = null)
        {
            return new ChangeToolStateViewModel(startUp ?? new StartUpMock(), changeToolStateForLocationUseCase ?? new ChangeToolStateForLocationUseCaseMock(null, null, null), nullLocalizationWrapper ?? new NullLocalizationWrapper());
        }


    }
}
