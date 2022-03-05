using Core.Diffs;
using Core.Entities;
using Core.Enums;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Core.PhysicalValueTypes;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Threads;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;
using FrameworksAndDrivers.Gui.Wpf.Test.ViewModels.Mock;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    class LocationViewModelTest
    {
        private class LocationValidatorMock : LocationValidator
        {
            public bool WasValidateLocationCalled = false;
            public LocationModel ValidateLocationParameter;
            public bool ValidateLocationReturnValue;


            public override bool ValidateLocation(LocationModel location)
            {
                WasValidateLocationCalled = true;
                ValidateLocationParameter = location;
                return ValidateLocationReturnValue;
            }
        }


        [Test]
        public void InvokeLoadingCommandExecutesUseCaseLoadTree()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.LoadTreeCommand.Invoke(null);

            AsyncCallCheckerNoAssuredTimeout.OnCallCheck(useCase.LoadTreeCalled.Task, 0, () => Assert.Pass());
        }

        [Test]
        public void ShowLocationTreeSetsLocationTree()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            var directorys = new List<LocationDirectory>();
            directorys.Add(CreateLocationDirectoryByIdOnly(15));
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.ShowLocationTree(directorys);
            Assert.AreEqual(1, viewModel.LocationTree.LocationDirectoryModels.Count);
            Assert.AreEqual(15, viewModel.LocationTree.LocationDirectoryModels[0].Id);
        }

        [Test]
        [Parallelizable]
        [TestCase(89)]
        [TestCase(46)]
        public void RemoveLocationRemovesLocationFromLocationTree(long id)
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            var locationToRemove = CreateLocation.IdOnly(id);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.ShowLocationTree(new List<LocationDirectory>());
            viewModel.LocationTree.LocationModels.Add(LocationModel.GetModelFor(locationToRemove, new NullLocalizationWrapper(), null));
            viewModel.LocationTree.LocationModels.Add(LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null));
            Assert.AreEqual(2, viewModel.LocationTree.LocationModels.Count);
            viewModel.RemoveLocation(locationToRemove);
            Assert.AreEqual(1, viewModel.LocationTree.LocationModels.Count);
            var remainingLocation = viewModel.LocationTree.LocationModels[0];
            Assert.AreNotEqual(locationToRemove.Id.ToLong(), remainingLocation.Id);
        }

        private static IEnumerable<(Location, Location, bool)> RemoveLocationSetsSelectedLocationToNullCorrectData =
            new List<(Location, Location, bool)>()
            {
                (CreateLocation.IdOnly(1), CreateLocation.IdOnly(1), true),
                (CreateLocation.IdOnly(1), CreateLocation.IdOnly(7), false),
                (CreateLocation.IdOnly(99), CreateLocation.IdOnly(99), true),
            };

        [TestCaseSource(nameof(RemoveLocationSetsSelectedLocationToNullCorrectData))]
        public void RemoveLocationSetsSelectedLocationToNullCorrect((Location selectedLocation, Location location2Remove, bool isNull) data)
        {
            var viewModel = CreateLocationViewModel(CreateLocationUseCaseMock());
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            viewModel.SelectedLocation = LocationModel.GetModelFor(data.selectedLocation, new NullLocalizationWrapper(), null);
            viewModel.RemoveLocation(data.location2Remove);

            Assert.AreEqual(data.isNull, viewModel.SelectedLocation == null);
        }

        [Test]
        public void ShowRemoveLocationErrorCallsMessageBoxRequest()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.MessageBoxRequest += (sender, args) => Assert.Pass();
            viewModel.ShowRemoveLocationError();
            Assert.Fail();
        }

        [Test]
        public void ShowLocationTreeErrorInvokesMessageBoxRequest()
        {
            var viewModel = new LocationViewModel(null, null, new LocationValidator(), new NullLocalizationWrapper(), null, null);
            viewModel.MessageBoxRequest += (s, e) => Assert.Pass();

            viewModel.ShowLocationTreeError();
        }

        [Test]
        public void AddLocationErrorInvokesMessageBoxRequest()
        {
            var viewModel = new LocationViewModel(null, null, new LocationValidator(), new NullLocalizationWrapper(), null, null);
            viewModel.MessageBoxRequest += (s, e) => Assert.Pass();

            viewModel.AddLocationError();
        }

        [Test]
        [TestCase(15)]
        [TestCase(26)]
        public void AddLocationAddsLocationToAllLocationModels(long locationId)
        {
            var viewModel = new LocationViewModel(null, new StartUpMock(), new LocationValidator(),
                new NullLocalizationWrapper(), null, null);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            var location = CreateLocation.IdOnly(locationId);

            viewModel.AddLocation(location);

            Assert.AreEqual(1, viewModel.LocationTree.LocationModels.Count);
            Assert.AreEqual(location.Id.ToLong(), viewModel.LocationTree.LocationModels[0].Id);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void AddLocationCommandExecuteCallsOpenAddLocationAssistentOnStartUp()
        {
            var startUp = new StartUpMock();
            var viewModel = new LocationViewModel(null, startUp, new LocationValidator(), null, null, null);
            startUp.OpenAddLocationAssistentReturnValue = new View.AssistentView("");

            viewModel.AddLocationCommand.Invoke(null);

            Assert.IsTrue(startUp.WasOpenAddLocationAssistent);
            Assert.IsNull(startUp.OpenAddLocationAssistentParameter);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void AddLocationCommandExecuteCallsOpenAddLocationAssistentOnStartUpWithCorrectParameter()
        {
            var startUp = new StartUpMock();
            var useCase = CreateLocationUseCaseMock();
            var viewModel =
                new LocationViewModel(useCase, startUp, new LocationValidator(), new NullLocalizationWrapper(), null, null);
            viewModel.SelectedLocation = new LocationModel(new Location(), new NullLocalizationWrapper(), null) {Id = 987645312};
            startUp.OpenAddLocationAssistentReturnValue = new View.AssistentView("");

            viewModel.AddLocationCommand.Invoke(null);

            Assert.IsTrue(startUp.WasOpenAddLocationAssistent);
            Assert.IsNotNull(startUp.OpenAddLocationAssistentParameter);
            Assert.AreEqual(viewModel.SelectedLocation.Id, startUp.OpenAddLocationAssistentParameter.Id.ToLong());
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void AddLocationCommandExecuteInvokesShowDialogRequestWithCorrectParameter()
        {
            var startUp = new StartUpMock();
            var viewModel = new LocationViewModel(null, startUp, new LocationValidator(), null, null, null);
            startUp.OpenAddLocationAssistentReturnValue = new View.AssistentView("");

            ICanShowDialog arg = null;
            viewModel.ShowDialogRequest += (s, e) => arg = e;

            viewModel.AddLocationCommand.Invoke(null);

            Assert.AreEqual(startUp.OpenAddLocationAssistentReturnValue, arg);
        }

        [Test]
        public void ChangingSelectedLocationCallsLoadPictureForLocation()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SelectedLocation = CreateAnonymousLocationModel();
            Assert.AreEqual(1, useCase.LoadPictureForLocationCallCount);
        }

        [Test]
        [TestCase(15)]
        [TestCase(26)]
        public void ChangingSelectedLocationHasCorrectLocationIdInLoadPictureForLocation(long id)
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            var location = CreateLocationWithOnlyId(id);
            viewModel.SelectedLocation = location;
            Assert.AreEqual(id, useCase.LoadPictureForLocationParameter.ToLong());
        }

        [Test]
        public void ChangingSelectedLocationCallsLoadCommentForLocation()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            var location = CreateAnonymousLocationModel();
            viewModel.SelectedLocation = location;
            Assert.AreEqual(1, useCase.LoadCommentForLocationCallCount);
        }

        [Test]
        [TestCase(15)]
        [TestCase(26)]
        public void ChangingSelectedLocationHasCorrectLocationIdInLoadCommentForLocation(long id)
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            var location = CreateLocationWithOnlyId(id);
            viewModel.SelectedLocation = location;
            Assert.AreEqual(id, useCase.LoadCommentForLocationParameter.ToLong());
        }

        [Test]
        public void ChangingSelectedLocationToNullLoadsNoPicture()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SelectedLocation = null;
            Assert.AreEqual(0, useCase.LoadPictureForLocationCallCount);
        }

        [Test]
        [TestCase(15)]
        [TestCase(26)]
        public void ShowPictureForLocationAddsImageToLocationModel(long pictureId)
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            var picture = new Picture {SeqId = pictureId};
            var locationModel = CreateLocationWithOnlyId(15);
            viewModel.LocationTree.LocationModels.Add(locationModel);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.ShowPictureForLocation(picture, new LocationId(15));
            Assert.AreEqual(1, viewModel.LocationTree.LocationModels.Count);
            Assert.AreEqual(pictureId, viewModel.LocationTree.LocationModels[0].Picture.SeqId);
        }

        [Test]
        [TestCase(15)]
        [TestCase(26)]
        public void ShowPictureForLocationAddsImageToCorrectLocation(long locationId)
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);

            var picture = new Picture {SeqId = 15};
            var locationModel = CreateLocationWithOnlyId(locationId);
            viewModel.LocationTree.LocationModels.Add(locationModel);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.ShowPictureForLocation(picture, new LocationId(locationId));
            Assert.AreEqual(1, viewModel.LocationTree.LocationModels.Count);
            Assert.IsNotNull(viewModel.LocationTree.LocationModels[0].Picture);
            Assert.AreEqual(locationId, viewModel.LocationTree.LocationModels[0].Id);
        }

        [Test]
        public void ShowPictureForLocationWithNullPictureDoesNothing()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);

            var locationModel = CreateLocationWithOnlyId(15);
            viewModel.LocationTree.LocationModels.Add(locationModel);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.ShowPictureForLocation(null, new LocationId(15));
            Assert.AreEqual(1, viewModel.LocationTree.LocationModels.Count);
            Assert.IsNull(viewModel.LocationTree.LocationModels[0].Picture);
        }


        [Test]
        public void ShowPictureForLocationWithNullLocationIdDoesNothing()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);

            var picture = new Picture {SeqId = 15};
            var locationModel = CreateLocationWithOnlyId(15);
            viewModel.LocationTree.LocationModels.Add(locationModel);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.ShowPictureForLocation(picture, null);
            Assert.AreEqual(1, viewModel.LocationTree.LocationModels.Count);
            Assert.IsNull(viewModel.LocationTree.LocationModels[0].Picture);
        }

        [Test]
        [TestCase("TestDing1")]
        [TestCase("TestBlub1")]
        public void ShowCommentForLocationAddsCommentToLocationModel(string comment)
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            var locationModel = CreateLocationWithOnlyId(15);
            viewModel.LocationTree.LocationModels.Add(locationModel);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.ShowCommentForLocation(comment, new LocationId(15));
            Assert.AreEqual(1, viewModel.LocationTree.LocationModels.Count);
            Assert.AreEqual(comment, viewModel.LocationTree.LocationModels[0].Comment);
        }

        [Test]
        public void ShowCommentForLocationWithNullCommentDoesNothing()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);

            var locationModel = CreateLocationWithOnlyId(15);
            viewModel.LocationTree.LocationModels.Add(locationModel);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.ShowCommentForLocation(null, new LocationId(15));
            Assert.AreEqual(1, viewModel.LocationTree.LocationModels.Count);
            Assert.IsNull(viewModel.LocationTree.LocationModels[0].Comment);
        }

        [Test]
        public void ShowCommentForLocationWithNullLocationIdDoesNothing()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);

            var locationModel = CreateLocationWithOnlyId(15);
            viewModel.LocationTree.LocationModels.Add(locationModel);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.ShowCommentForLocation("", null);
            Assert.AreEqual(1, viewModel.LocationTree.LocationModels.Count);
            Assert.IsNull(viewModel.LocationTree.LocationModels[0].Picture);
        }

        [Test]
        [Parallelizable]
        public void InvokeRemoveLocationOrDirectoryExecuteWithSelectedLocationCallsUseCaseLoadLocationToolAssignmentsForLocation()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            var location = CreateAnonymousLocationModel();
            viewModel.SelectedLocation = location;
            viewModel.MessageBoxRequest += (sender, args) => args.ResultAction?.Invoke(MessageBoxResult.Yes);
            viewModel.RemoveLocationOrDirectoryCommand.Invoke(null);
            Assert.AreEqual(1, useCase.RemoveLocationCallCount);
            Assert.AreEqual(location.Id, useCase.RemovedLocation.Id.ToLong());
        }


        [Test]
        [Parallelizable]
        public void InvokeRemoveLocationOrDirectoryExecuteCallsRemoveLocationWithResetedParameters()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            var location = CreateLocation.Anonymous();
            var locationModel = LocationModel.GetModelFor(location, new NullLocalizationWrapper(), null);

            viewModel.SelectedLocation = locationModel.CopyDeep();
            viewModel.SelectedLocation.Number = "345345";
            viewModel.SelectedLocation.Description = "78435743857835";
            viewModel.SelectedLocation.ConfigurableField1 = "J";

            viewModel.MessageBoxRequest += (sender, args) => args.ResultAction?.Invoke(MessageBoxResult.Yes);
            viewModel.RemoveLocationOrDirectoryCommand.Invoke(null);

            Assert.IsTrue(useCase.RemovedLocation.EqualsByContent(locationModel.Entity));
        }

        [Test]
        [Parallelizable]
        [TestCase(15)]
        [TestCase(26)]
        public void InvokeAddLocationFolderCallsUseCaseAddLocationWithCorrectLocation(long id)
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            var locationDirectory = LocationDirectoryHumbleModel.GetModelFor(CreateLocationDirectoryByIdOnly(id), null);
            viewModel.SelectedDirectoryHumble = locationDirectory;
            viewModel.LocationDirectoryNameRequest += (sender, tuple) =>
            {
                tuple.Item1.Invoke(MessageBoxResult.OK, String.Empty);
            };
            viewModel.AddLocationDirectoryCommand.Invoke(null);
            Assert.AreEqual(1, useCase.AddLocationFolderCallCount);
            Assert.AreEqual(locationDirectory.Id, useCase.AddLocationFolderParameter.ToLong());
        }

        [Test]
        [Parallelizable]
        [TestCase(15)]
        [TestCase(26)]
        public void InvokeAddLocationFolderCallsUseCaseAddLocationWithLocationParentId(long id)
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SelectedDirectoryHumble = null;
            viewModel.SelectedLocation = new LocationModel(CreateLocation.IdOnly(id), new NullLocalizationWrapper(), null) {ParentId = id};
            viewModel.LocationDirectoryNameRequest += (sender, tuple) =>
            {
                tuple.Item1.Invoke(MessageBoxResult.OK, String.Empty);
            };
            viewModel.AddLocationDirectoryCommand?.Invoke(this);
            Assert.AreEqual(1, useCase.AddLocationFolderCallCount);
            Assert.AreEqual(id, useCase.AddLocationFolderParameter.ToLong());
        }

        [Test]
        [Parallelizable]
        public void InvokeAddLocationFolderCallsLocationFolderNameRequest()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.LocationDirectoryNameRequest += (sender, s) => { Assert.Pass(); };
            viewModel.AddLocationDirectoryCommand.Invoke(this);
            Assert.Fail();
        }

        [Test]
        public void UpdateLocationErrorInvokesMessageBoxRequest()
        {
            var viewModel = new LocationViewModel(null, null, new LocationValidator(), new NullLocalizationWrapper(), null, null);
            viewModel.MessageBoxRequest += (s, e) => Assert.Pass();

            viewModel.UpdateLocationError();
        }

        [Test]
        public void LocationAlreadyExistsInvokesMessageBoxRequest()
        {
            var viewModel = new LocationViewModel(null, null, new LocationValidator(), new NullLocalizationWrapper(), null, null);
            viewModel.MessageBoxRequest += (s, e) => Assert.Pass();

            viewModel.LocationAlreadyExists();
        }

        [Test]
        public void UpdateLocationUpdatesLocationInTree()
        {
            var viewModel = CreateLocationViewModel(null);
            var location = new Location()
            {
                Id = new LocationId(7498615),
                Description = new LocationDescription("ß03e498r5utfgjkd"),
                Number = new LocationNumber("0r9tiuiroi")
            };
            var model = new LocationModelMock(new Location())
            {
                Id = location.Id.ToLong(),
                Description = "509rt87uzhfdemk,wl",
                Number = "p409r5t8jfdm,.edrfcvx"
            };

            viewModel.LocationTree.LocationModels.Add(new LocationModel(CreateLocation.IdOnly(852), new NullLocalizationWrapper(), null));
            viewModel.LocationTree.LocationModels.Add(model);
            viewModel.LocationTree.LocationModels.Add(new LocationModel(CreateLocation.IdOnly(96845), new NullLocalizationWrapper(), null));
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.UpdateLocation(location);

            Assert.IsTrue(model.WasUpdateByEntityCalled);
            Assert.AreEqual(location, model.UpdateByEntityParameter);
        }

        [Test]
        public void SaveLocationCanExecuteReturnsTrueAfterSelectedLocationChanged()
        {
            var entity = CreateLocation.Anonymous();
            var model = LocationModel.GetModelFor(entity, new NullLocalizationWrapper(), null);
            var validator = new LocationValidatorMock();
            var viewModel = CreateLocationViewModel(CreateLocationUseCaseMock(), validator);

            validator.ValidateLocationReturnValue = true;
            viewModel.SelectedLocation = model;
            model.Number = "w3eo04ritufghtireoriguj";

            Assert.IsTrue(viewModel.SaveLocationCommand.CanExecute(null));
        }

        [Test]
        public void SaveLocationCanExecuteReturnsFalseWidthSelectedLocationNull()
        {
            var viewModel = CreateLocationViewModel(CreateLocationUseCaseMock());

            Assert.IsFalse(viewModel.SaveLocationCommand.CanExecute(null));
        }

        [Test]
        public void SaveLocationExecuteCallsUpdateUpdateOnUseCaseWithCorrectParameter()
        {
            var entity = CreateLocation.Anonymous();
            var model = LocationModel.GetModelFor(entity, new NullLocalizationWrapper(), null);
            var useCase = CreateLocationUseCaseMock();
            var validator = new LocationValidatorMock();
            var viewModel = CreateLocationViewModel(useCase, validator);
            var oldNumber = entity.Number.ToDefaultString();

            validator.ValidateLocationReturnValue = true;
            viewModel.RequestChangesVerification += (s, e) => e.Result = MessageBoxResult.Yes;
            viewModel.SelectedLocation = model;
            model.Number = "w3eo0reoriguj";
            viewModel.SaveLocationCommand.Invoke(null);

            Assert.IsTrue(useCase.WasUpdateLocationCalled);
            Assert.AreEqual(entity.Id.ToLong(), useCase.UpdateLocationParameter.OldLocation.Id.ToLong());
            Assert.AreEqual(entity.Id.ToLong(), useCase.UpdateLocationParameter.NewLocation.Id.ToLong());
            Assert.AreEqual(oldNumber, useCase.UpdateLocationParameter.OldLocation.Number.ToDefaultString());
            Assert.AreEqual(model.Number, useCase.UpdateLocationParameter.NewLocation.Number.ToDefaultString());
        }

        [Test]
        public void VerifyChangesDiffInvokesRequestChangesVerfication()
        {
            var viewModel = CreateLocationViewModel(null);
            viewModel.RequestChangesVerification += (s, e) => Assert.Pass();

            viewModel.VerifyLocationDiff(null);
        }

        [Test, TestCaseSource(nameof(GetLocationModelsForEqualityCheck))]
        public void VerifyChangesDiffPassesCorrectParametersToRequestChangesVerfication(
            Tuple<Location, Location, Func<Location, object>, string> tuple)
        {
            var viewModel = CreateLocationViewModel(null);

            viewModel.RequestChangesVerification += (s, e) =>
            {
                Assert.AreEqual(1, e.ChangedValues.Count);

                if (tuple.Item4 != nameof(LocationModel.ControlledBy) &&
                    tuple.Item4 != nameof(LocationModel.ConfigurableField3))
                {
                    Assert.AreEqual(tuple.Item3(tuple.Item1), e.ChangedValues[0].OldValue);
                    Assert.AreEqual(tuple.Item3(tuple.Item2), e.ChangedValues[0].NewValue);
                }
            };

            viewModel.VerifyLocationDiff(new LocationDiff()
            {
                OldLocation = tuple.Item1,
                NewLocation = tuple.Item2
            });
        }

        [TestCase("ptu8rieo0rjzuh")]
        [TestCase("ortighwpeoghjwpghrß0tue4654")]
        public void VerifyChangesDiffSetsCommentOfDiff(string comment)
        {
            var viewModel = CreateLocationViewModel(null);
            viewModel.RequestChangesVerification += (s, e) => e.Comment = comment;
            var diff = new LocationDiff()
            {
                OldLocation = CreateLocation.Anonymous(),
                NewLocation = CreateLocation.Anonymous()
            };
            diff.NewLocation.MaximumAngle = Angle.FromDegree(140);
            viewModel.VerifyLocationDiff(diff);

            Assert.AreEqual(comment, diff.Comment.ToDefaultString());
        }

        [TestCase(MessageBoxResult.Yes)]
        [TestCase(MessageBoxResult.No)]
        [TestCase(MessageBoxResult.Cancel)]
        public void VerifyChangesDiffReturnsCorrectValue(MessageBoxResult result)
        {
            var viewModel = CreateLocationViewModel(CreateLocationUseCaseMock());
            viewModel.RequestChangesVerification += (s, e) => e.Result = result;
            viewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);

            var diff = new LocationDiff()
            {
                OldLocation = CreateLocation.Anonymous(),
                NewLocation = CreateLocation.Anonymous()
            };
            diff.NewLocation.MaximumAngle = Angle.FromDegree(125);
            var verifyResult = viewModel.VerifyLocationDiff(diff);

            Assert.AreEqual(result, verifyResult);
        }

        [Test]
        public void VerifyLocationDiffWithNoChangesReturnsMessageBoxResultOk()
        {
            var viewModel = CreateLocationViewModel(CreateLocationUseCaseMock());
            viewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);

            var diff = new LocationDiff()
            {
                OldLocation = CreateLocation.Anonymous(),
                NewLocation = CreateLocation.Anonymous()
            };
            var verifyResult = viewModel.VerifyLocationDiff(diff);
            Assert.AreEqual(MessageBoxResult.OK, verifyResult);
        }

        [Test]
        [TestCase(15)]
        [TestCase(26)]
        public void AddLocationDirectoryAddsDirectoryToLocationTree(long id)
        {
            var viewModel = CreateLocationViewModel(null);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            var locationDirectory = new LocationDirectory {Id = new LocationDirectoryId(id)};
            viewModel.AddLocationDirectory(locationDirectory);
            Assert.AreEqual(1, viewModel.LocationTree.LocationDirectoryModels.Count);
            Assert.AreEqual(id, viewModel.LocationTree.LocationDirectoryModels.FirstOrDefault().Id);
        }

        [Test]
        public void AddLocationDirectoryCallsFolderSelectionRequest()
        {
            var viewModel = CreateLocationViewModel(null);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.FolderSelectionRequest += (sender, directory) => Assert.Pass();
            viewModel.AddLocationDirectory(CreateLocationDirectoryByIdOnly(15));
            Assert.Fail();
        }

        [Test]
        public void ShowAddLocationDirectoryErrorCallsMessageBoxRequest()
        {
            var viewModel = CreateLocationViewModel(null);
            viewModel.MessageBoxRequest += (sender, args) => Assert.Pass();
            viewModel.ShowAddLocationDirectoryError("Test");
            Assert.Fail();
        }

        [Test]
        public void SelectSameLocationDoesNotInvokePropertyChanged()
        {
            var viewModel = CreateLocationViewModel(CreateLocationUseCaseMock());
            var location = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);
            bool wasPropertyChangedInvoked = false;

            viewModel.SelectedLocation = location;
            viewModel.PropertyChanged += (s, e) => wasPropertyChangedInvoked = true;

            Assert.IsFalse(wasPropertyChangedInvoked);
        }

        [Test]
        public void SelectSameLocationDoesNotInvokeRequestChangesVerification()
        {
            var viewModel = CreateLocationViewModel(CreateLocationUseCaseMock());
            var location = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);
            bool wasRequestChangesVerificationInvoked = false;

            viewModel.SelectedLocation = location;
            viewModel.RequestChangesVerification += (s, e) => wasRequestChangesVerificationInvoked = true;

            Assert.IsFalse(wasRequestChangesVerificationInvoked);
        }

        [Test]
        public void ChangeSelectedLocationWithUnequalLocationInvokesRequestChangesVerification()
        {
            var validator = new LocationValidatorMock();
            var viewModel = CreateLocationViewModel(CreateLocationUseCaseMock(), validator);
            var location = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);
            var otherLocation = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);
            bool wasRequestChangesVerificationInvoked = false;

            validator.ValidateLocationReturnValue = true;
            viewModel.RequestChangesVerification += (s, e) => wasRequestChangesVerificationInvoked = true;

            viewModel.SelectedLocation = location;
            location.Description = "40tzugjflpegj";

            Assert.IsFalse(wasRequestChangesVerificationInvoked);

            viewModel.SelectedLocation = otherLocation;

            Assert.IsTrue(wasRequestChangesVerificationInvoked);
        }

        [Test]
        public void ChangingSelectedLocationWithPassedVerificationSavesLocation()
        {
            var useCase = CreateLocationUseCaseMock();
            var validator = new LocationValidatorMock();
            var viewModel = CreateLocationViewModel(useCase, validator);
            var location = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);
            var otherLocation = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);

            validator.ValidateLocationReturnValue = true;
            viewModel.RequestChangesVerification += (s, e) => e.Result = MessageBoxResult.Yes;

            viewModel.SelectedLocation = location;
            location.Description = "40tzugjfwptgjuvfkdlpegj";
            viewModel.SelectedLocation = otherLocation;

            Assert.IsTrue(useCase.WasUpdateLocationCalled);
            Assert.AreEqual(CreateLocation.Anonymous().Description.ToDefaultString(),
                useCase.UpdateLocationParameter.OldLocation.Description.ToDefaultString());
            Assert.AreEqual(location.Description,
                useCase.UpdateLocationParameter.NewLocation.Description.ToDefaultString());
            Assert.AreEqual(otherLocation, viewModel.SelectedLocation);
        }

        [Test]
        public void ChangingSelectedLocationWithFailedVerificationResetsLocation()
        {
            var useCase = CreateLocationUseCaseMock();
            var validator = new LocationValidatorMock();
            var viewModel = CreateLocationViewModel(useCase, validator);
            var location = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);
            var otherLocation = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);

            validator.ValidateLocationReturnValue = true;
            viewModel.RequestChangesVerification += (s, e) => e.Result = MessageBoxResult.No;

            viewModel.SelectedLocation = location;
            location.Description = "40tzuglpegj";
            viewModel.SelectedLocation = otherLocation;

            Assert.IsFalse(useCase.WasUpdateLocationCalled);
            Assert.AreEqual(CreateLocation.Anonymous().Description.ToDefaultString(), location.Description);
            Assert.AreEqual(otherLocation, viewModel.SelectedLocation);
        }

        [Test]
        public void ChangingSelectedLocationWithCanceledVerificationDoesNotSelectNewLocation()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            var location = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);
            var otherLocation = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);

            viewModel.RequestChangesVerification += (s, e) => e.Result = MessageBoxResult.Cancel;

            viewModel.SelectedLocation = location;
            location.Description = "40tzudlpegj";
            viewModel.SelectedLocation = otherLocation;

            Assert.IsFalse(useCase.WasUpdateLocationCalled);
            Assert.AreEqual("40tzudlpegj", location.Description);
            Assert.AreEqual(location, viewModel.SelectedLocation);
        }

        [Test]
        public void CanCloseInvokesRequestChangesVerificationIfLocationHasChanged()
        {
            var validator = new LocationValidatorMock();
            var viewModel = CreateLocationViewModel(CreateLocationUseCaseMock(), validator);
            var location = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);
            bool wasRequestChangesVerificationInvoked = false;

            validator.ValidateLocationReturnValue = true;
            viewModel.SelectedLocation = location;
            location.Description = "woerufghpwl";
            viewModel.RequestChangesVerification += (s, e) => wasRequestChangesVerificationInvoked = true;

            viewModel.CanClose();

            Assert.IsTrue(wasRequestChangesVerificationInvoked);
        }

        [Test]
        public void CanCloseReturnsTrueIfLocationHasNotChanged()
        {
            var validator = new LocationValidatorMock();
            var viewModel = CreateLocationViewModel(CreateLocationUseCaseMock(), validator);
            var location = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);

            validator.ValidateLocationReturnValue = true;
            viewModel.SelectedLocation = location;
            var result = viewModel.CanClose();

            Assert.IsTrue(result);
        }

        [Test]
        public void CanCloseWithPassedVerificationSavesLocation()
        {
            var useCase = CreateLocationUseCaseMock();
            var validator = new LocationValidatorMock();
            var viewModel = CreateLocationViewModel(useCase, validator);
            var location = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);

            validator.ValidateLocationReturnValue = true;
            viewModel.SelectedLocation = location;
            location.Description = "4oepbjhgepü";
            viewModel.RequestChangesVerification += (s, e) => e.Result = MessageBoxResult.Yes;

            var result = viewModel.CanClose();

            Assert.IsTrue(useCase.WasUpdateLocationCalled);
            Assert.AreEqual(CreateLocation.Anonymous().Description.ToDefaultString(),
                useCase.UpdateLocationParameter.OldLocation.Description.ToDefaultString());
            Assert.AreEqual(location.Description,
                useCase.UpdateLocationParameter.NewLocation.Description.ToDefaultString());
            Assert.IsTrue(result);
        }

        [Test]
        public void CanCloseWithFailedVerificationResetsLocation()
        {
            var useCase = CreateLocationUseCaseMock();
            var validator = new LocationValidatorMock();
            var viewModel = CreateLocationViewModel(useCase, validator);
            var location = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);

            validator.ValidateLocationReturnValue = false;
            viewModel.RequestChangesVerification += (s, e) => e.Result = MessageBoxResult.No;

            var result = viewModel.CanClose();

            Assert.IsFalse(useCase.WasUpdateLocationCalled);
            Assert.AreEqual(CreateLocation.Anonymous().Description.ToDefaultString(), location.Description);
            Assert.IsTrue(result);
        }

        [Test]
        public void CanCloseWithCanceledVerificationDoesNotSelectNewLocation()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            var location = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);

            viewModel.RequestChangesVerification += (s, e) => e.Result = MessageBoxResult.Cancel;

            viewModel.SelectedLocation = location;
            location.Description = "40tzugjfwptgjuvfkdlpegj";
            var result = viewModel.CanClose();

            Assert.IsFalse(useCase.WasUpdateLocationCalled);
            Assert.AreEqual("40tzugjfwptgjuvfkdlpegj", location.Description);
            Assert.IsFalse(result);
        }

        [Test]
        public void InvokeRemoveLocationOrDirectoryCommandCallsUseCaseLoadLocationToolAssignments()
        {
            var useCase = CreateLocationUseCaseMock();

            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SelectedDirectoryHumble = LocationDirectoryHumbleModel.GetModelFor(CreateLocationDirectoryByIdOnly(15), null);
            viewModel.MessageBoxRequest += (sender, args) => args.ResultAction.Invoke(MessageBoxResult.Yes);
            viewModel.RemoveLocationOrDirectoryCommand?.Invoke(null);
            Assert.AreEqual(1, useCase.RemoveDirectoryCallCount);
        }

        [Test]
        public void InvokeRemoveLocationOrDirectoryCommandNotCallingUseCaseRemoveDirectoryIfMessageBoxRequestResultIsNo()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SelectedDirectoryHumble = LocationDirectoryHumbleModel.GetModelFor(CreateLocationDirectoryByIdOnly(15), null);
            viewModel.MessageBoxRequest += (sender, args) => args.ResultAction.Invoke(MessageBoxResult.No);
            viewModel.RemoveLocationOrDirectoryCommand?.Invoke(null);
            Assert.AreEqual(0, useCase.RemoveDirectoryCallCount);
        }

        [TestCase("")]
        [TestCase(null)]
        public void SaveLocationCanExecuteReturnsFalseIfNumberOfSelectedIsEmpty(string number)
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            var location = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);

            location.Number = number;
            viewModel.SelectedLocation = location;
            location.MaximumAngle = 64738902;

            Assert.IsFalse(viewModel.SaveLocationCommand.CanExecute(null));
        }

        [TestCase("")]
        [TestCase(null)]
        public void SaveLocationCanExecuteReturnsFalseIfDescriptionOfSelectedIsEmpty(string description)
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            var location = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);

            location.Description = description;
            viewModel.SelectedLocation = location;
            location.MaximumAngle = 64738902;

            Assert.IsFalse(viewModel.SaveLocationCommand.CanExecute(null));
        }

        [TestCase("")]
        [TestCase(null)]
        public void ChangeSelectedLocationSelectsPreviousLocationIfNumberIsEmpty(string number)
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            var location = LocationModel.GetModelFor(CreateLocation.IdOnly(78), new NullLocalizationWrapper(), null);
            location.Description = "ortgijuftre";
            var secondLocation = LocationModel.GetModelFor(CreateLocation.IdOnly(45), new NullLocalizationWrapper(), null);
            bool wasMessageBoxRequestCalled = false;

            viewModel.SelectedLocation = location;
            location.Number = number;
            viewModel.SelectionRequest += (s, e) => Assert.AreEqual(78, e.Id);
            viewModel.MessageBoxRequest += (s, e) => wasMessageBoxRequestCalled = true;

            viewModel.SelectedLocation = secondLocation;

            Assert.IsTrue(wasMessageBoxRequestCalled);
            Assert.AreEqual(location, viewModel.SelectedLocation);
        }

        [TestCase("")]
        [TestCase(null)]
        public void ChangeSelectedLocationSelectsPreviousLocationIfDescriptionIsEmpty(string description)
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            var location = LocationModel.GetModelFor(CreateLocation.IdOnly(78), new NullLocalizationWrapper(), null);
            location.Number = "oreifghfjreorfgij";
            var secondLocation = LocationModel.GetModelFor(CreateLocation.IdOnly(45), new NullLocalizationWrapper(), null);
            bool wasMessageBoxRequestCalled = false;

            viewModel.SelectedLocation = location;
            location.Description = description;
            viewModel.SelectionRequest += (s, e) => Assert.AreEqual(78, e.Id);
            viewModel.MessageBoxRequest += (s, e) => wasMessageBoxRequestCalled = true;

            viewModel.SelectedLocation = secondLocation;

            Assert.IsTrue(wasMessageBoxRequestCalled);
            Assert.AreEqual(location, viewModel.SelectedLocation);
        }

        [TestCase("")]
        [TestCase(null)]
        public void CanCloseReturnsFalseAndShowsMessageBoxIfNumberIsEmpty(string number)
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            var location = LocationModel.GetModelFor(CreateLocation.IdOnly(78), new NullLocalizationWrapper(), null);
            location.Description = "ortgijuftre";
            bool wasMessageBoxRequestCalled = false;

            viewModel.SelectedLocation = location;
            location.Number = number;
            viewModel.MessageBoxRequest += (s, e) => wasMessageBoxRequestCalled = true;

            var result = viewModel.CanClose();

            Assert.IsTrue(wasMessageBoxRequestCalled);
            Assert.IsFalse(result);
        }

        [Test]
        public void CanCloseInvokesValidateLocationWidthCorrectParameter()
        {
            var useCase = CreateLocationUseCaseMock();
            var validator = new LocationValidatorMock();
            var viewModel = CreateLocationViewModel(useCase, validator);
            var location = LocationModel.GetModelFor(CreateLocation.IdOnly(78), new NullLocalizationWrapper(), null);

            viewModel.SelectedLocation = location;
            viewModel.CanClose();

            Assert.IsTrue(validator.WasValidateLocationCalled);
            Assert.AreEqual(location, validator.ValidateLocationParameter);
        }

        [Test]
        public void SaveLocationCanExecuteInvokesValidateLocationWidthCorrectParameter()
        {
            var useCase = CreateLocationUseCaseMock();
            var validator = new LocationValidatorMock();
            var viewModel = CreateLocationViewModel(useCase, validator);
            var location = LocationModel.GetModelFor(CreateLocation.IdOnly(78), new NullLocalizationWrapper(), null);

            viewModel.SelectedLocation = location;
            viewModel.SaveLocationCommand.CanExecute(null);

            Assert.IsTrue(validator.WasValidateLocationCalled);
            Assert.AreEqual(location, validator.ValidateLocationParameter);
        }

        [Test]
        public void ChangeLocationInvokesValidateLocationWidthCorrectParameter()
        {
            var useCase = CreateLocationUseCaseMock();
            var validator = new LocationValidatorMock();
            var viewModel = CreateLocationViewModel(useCase, validator);
            var location = LocationModel.GetModelFor(CreateLocation.IdOnly(78), new NullLocalizationWrapper(), null);

            viewModel.SelectedLocation = location;
            viewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);

            Assert.IsTrue(validator.WasValidateLocationCalled);
            Assert.AreEqual(location, validator.ValidateLocationParameter);
        }

        [Test]
        public void ClosingWithInvalidLocationCannotExecuteAndShowsMessage()
        {
            var useCase = CreateLocationUseCaseMock();
            var validator = new LocationValidatorMock();
            var viewModel = CreateLocationViewModel(useCase, validator);
            var location = LocationModel.GetModelFor(CreateLocation.IdOnly(78), new NullLocalizationWrapper(), null);

            bool wasMessageBoxRequestCalled = false;
            viewModel.MessageBoxRequest += (s, e) => wasMessageBoxRequestCalled = true;
            validator.ValidateLocationReturnValue = false;

            viewModel.SelectedLocation = location;
            var result = viewModel.CanClose();

            Assert.IsTrue(wasMessageBoxRequestCalled);
            Assert.IsFalse(result);
        }

        [Test]
        public void CanCloseReturnsTrueIfSelectedLocationIsNull()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);

            viewModel.SelectedLocation = null;

            Assert.IsTrue(viewModel.CanClose());
        }

        [Test]
        [Parallelizable]
        public void RemoveDirectoryRemovesDirectoryOfLocationTree()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            var directorys = new ObservableCollection<LocationDirectoryHumbleModel>();
            directorys.Add(new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 15});
            var locDir = new LocationDirectoryHumbleModel(new LocationDirectory()) {Id = 26};
            directorys.Add(locDir);
            viewModel.LocationTree = new LocationTreeModel(null, directorys);
            viewModel.RemoveDirectory(new LocationDirectoryId(15));
            Assert.AreEqual(1, viewModel.LocationTree.LocationDirectoryModels.Count);
            Assert.AreEqual(locDir.Id, viewModel.LocationTree.LocationDirectoryModels[0].Id);
        }

        [Test]
        public void ShowRemoveDirectoryErrorCallsMessageRequest()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.MessageBoxRequest += (sender, args) => Assert.Pass();
            viewModel.ShowRemoveDirectoryError();
            Assert.Fail();
        }

        [Test]
        public void CutCanExecuteReturnsTrueIfSelectedLocationIsNotNull()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.IdOnly(15), new NullLocalizationWrapper(), useCase);
            Assert.IsTrue(viewModel.CutCommand.CanExecute(this));
        }

        [Test]
        public void CutCanExecuteReturnsFalseIfSelectedLocationIsNull()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SelectedLocation = null;
            Assert.IsFalse(viewModel.CutCommand.CanExecute(this));
        }

        [Test]
        public void CutCanExecuteReturnsTrueIfSelectedDirectoryIsNotNull()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SelectedDirectoryHumble = LocationDirectoryHumbleModel.GetModelFor(CreateLocationDirectoryByIdOnly(15), null);
            Assert.IsTrue(viewModel.CutCommand.CanExecute(this));
        }

        [Test]
        public void CutCanExecuteReturnsFalseIfSelectedDirectoryIsNull()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SelectedDirectoryHumble = null;
            Assert.IsFalse(viewModel.CutCommand.CanExecute(this));
        }

        [Test]
        public void PasteCanExecuteReturnsTrueIfLocationWasCut()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.IdOnly(15), new NullLocalizationWrapper(), useCase);
            viewModel.CutCommand.Execute(null);
            Assert.True(viewModel.PasteCommand.CanExecute(null));
        }

        [Test]
        public void PasteCanExecuteReturnsFalseIfLocationWasCut()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            Assert.IsFalse(viewModel.PasteCommand.CanExecute(null));
        }

        [Test]
        public void PasteCanExecuteReturnsTrueIfDirectoryWasCut()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SelectedDirectoryHumble = LocationDirectoryHumbleModel.GetModelFor(CreateLocationDirectoryByIdOnly(36), null);
            viewModel.SelectedDirectoryHumble.ParentId = 35;
            viewModel.CutCommand.Execute(null);
            viewModel.SelectedDirectoryHumble =
                LocationDirectoryHumbleModel.GetModelFor(CreateLocationDirectoryByIdOnly(26), null);
            Assert.True(viewModel.PasteCommand.CanExecute(null));
        }

        [Test]
        public void PasteCanExecuteReturnsFalseIfDirectoryWasCut()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            Assert.IsFalse(viewModel.PasteCommand.CanExecute(null));
        }

        [Test]
        public void PasteExecuteCallsUseCaseChangeLocationParentWithCorrectParameter()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.IdOnly(15), new NullLocalizationWrapper(), useCase);
            viewModel.CutCommand.Execute(null);
            viewModel.PasteCommand.Execute(null);
            Assert.AreEqual(1, useCase.ChangeLocationParentCallCount);
            Assert.AreEqual(15, useCase.ChangeLocationParentLocationParameter.Id.ToLong());
        }

        [Test]
        public void PasteExecuteCallsUseCaseChangeLocationDirectoryParentWithCorrectParameter()
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SelectedDirectoryHumble =
                LocationDirectoryHumbleModel.GetModelFor(CreateLocationDirectoryByIdOnly(15), useCase);
            viewModel.CutCommand.Execute(null);
            viewModel.SelectedDirectoryHumble =
                LocationDirectoryHumbleModel.GetModelFor(CreateLocationDirectoryByIdOnly(23), useCase);
            viewModel.PasteCommand.Execute(null);
            Assert.AreEqual(1, useCase.ChangeLocationDirectoryParentCallCount);
            Assert.AreEqual(15, useCase.ChangeLocationDirectoryParentLocationDirectoryParameter.Id.ToLong());
        }

        [Test]
        [TestCase(26)]
        [TestCase(95)]
        public void PasteExecuteCallsUseCaseWithParentIdOfSelectedLocation(long parentId)
        {
            var useCase = CreateLocationUseCaseMock();
            var validation = new LocationValidatorMock();
            var viewModel = CreateLocationViewModelWithValidatorMock(useCase, validation);
            viewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.IdOnly(15), new NullLocalizationWrapper(), useCase);
            viewModel.SelectedLocation.Number = "weorijfhv";
            viewModel.SelectedLocation.Description = "ofjvasdc";
            viewModel.CutCommand.Execute(null);
            viewModel.MessageBoxRequest += (sender, args) => args.ResultAction.Invoke(MessageBoxResult.No);
            validation.ValidateLocationReturnValue = true;
            var loc = CreateLocation.ParentIdOnly(parentId);
            loc.Number = new LocationNumber("pofgiujvc");
            loc.Description = new LocationDescription("orifjvc,");
            viewModel.SelectedLocation = LocationModel.GetModelFor(loc, new NullLocalizationWrapper(), null);
            viewModel.PasteCommand.Execute(null);
            Assert.AreEqual(parentId, useCase.ChangeLocationParentNewParentId);
        }

        [Test]
        [TestCase(26)]
        [TestCase(95)]
        public void PasteExecuteCallsUseCaseWIthIdOfSelectedDirectory(long directoryId)
        {
            var useCase = CreateLocationUseCaseMock();
            var validation = new LocationValidatorMock();
            var viewModel = CreateLocationViewModelWithValidatorMock(useCase, validation);
            viewModel.SelectedLocation = LocationModel.GetModelFor(CreateLocation.IdOnly(15), new NullLocalizationWrapper(), useCase);
            viewModel.CutCommand.Execute(null);
            viewModel.MessageBoxRequest += (sender, args) => args.ResultAction.Invoke(MessageBoxResult.No);
            validation.ValidateLocationReturnValue = true;
            viewModel.SelectedDirectoryHumble =
                LocationDirectoryHumbleModel.GetModelFor(CreateLocationDirectoryByIdOnly(directoryId), null);
            viewModel.PasteCommand.Execute(null);
            Assert.AreEqual(directoryId, useCase.ChangeLocationParentNewParentId);
        }

        [Test]
        [TestCase(15)]
        [TestCase(25)]
        public void ChangeLocationParentChangesParentOfLocationInAllLocationModels(long newParentId)
        {
            var useCase = CreateLocationUseCaseMock();
            var viewModel = CreateLocationViewModel(useCase);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            var location = CreateLocation.ParentIdOnly(5);
            viewModel.LocationTree.LocationModels.Add(LocationModel.GetModelFor(location, new NullLocalizationWrapper(), null));
            viewModel.ChangeLocationParent(location, new LocationDirectoryId(newParentId));
            Assert.AreEqual(1, viewModel.LocationTree.LocationModels.Count);
            Assert.AreEqual(newParentId, viewModel.LocationTree.LocationModels[0].ParentId);
        }

        [Test]
        public void ChangeLocationParentErrorShowsMessageBox()
        {
            var viewModel = CreateLocationViewModel(null);
            viewModel.MessageBoxRequest += (sender, args) => Assert.Pass();
            viewModel.ChangeLocationParentError();
        }

        [Test]
        [TestCase(15)]
        [TestCase(26)]
        public void ShowLocationAddsCorrectLocationToLocationTree(long locationId)
        {
            var viewModel = CreateLocationViewModel(null);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.ShowLocation(CreateLocation.IdOnly(locationId));
            Assert.AreEqual(1, viewModel.LocationTree.LocationModels.Count);
            Assert.AreEqual(locationId, viewModel.LocationTree.LocationModels[0].Id);
        }

        [Test]
        public void ShowChangeLocationToolAssignmentNoticeCallsMessageBoxRequest()
        {
            var viewModel = CreateLocationViewModel(null);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.MessageBoxRequest += (sender, args) => Assert.Pass();
            viewModel.ShowChangeLocationToolAssignmentNotice();
            Assert.Fail();
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void ShowChangeToolStatusDialogCallsOpenChangeToolStatusDialog()
        {
            var startupMock = new StartUpMock();
            
            var threadCreator = new MockThreadCreator();
            var viewModel = CreateLocationViewModel(startupMock, threadCreator);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            startupMock.OpenChangeToolStatusAssistantReturn = new ChangeToolStateView("Test", startupMock, new NullLocalizationWrapper(), null);
            viewModel.ShowChangeToolStatusDialog(null, new List<LocationToolAssignment>());
            Assert.AreEqual(1, startupMock.OpenChangeToolStatusAssistantCallCount);
        }


        private static IEnumerable<List<ToleranceClass>> AddToleranceClassAddsToleranceClassData =
            new List<List<ToleranceClass>>()
            {
                new List<ToleranceClass>()
                {
                    CreateToleranceClass.Anonymous(),
                    CreateToleranceClass.Parametrized(5, "345", true, 0 , 10)
                },
                new List<ToleranceClass>()
                {
                    CreateToleranceClass.Parametrized(7, "56765775", false, 0 , 10),
                    CreateToleranceClass.Parametrized(9, "345", true, 0 , 99),
                    CreateToleranceClass.Parametrized(0, "356745", true, 1 , 10)
                },
            };

        [TestCaseSource(nameof(AddToleranceClassAddsToleranceClassData))]
        public void AddToleranceClassAddsToleranceClass(List<ToleranceClass> toleranceClasses)
        {
            var viewModel = CreateLocationViewModel(null);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            foreach (var toleranceClass in toleranceClasses)
            {
                viewModel.AddToleranceClass(toleranceClass);
            }

            var comparer = new Func<ToleranceClass,ToleranceClassModel,bool>((a, b) => a.EqualsByContent(b.Entity));

            Assert.AreEqual(toleranceClasses.Count, viewModel.ToleranceClasses.Count);
            CheckerFunctions.CollectionAssertAreEquivalent(toleranceClasses, viewModel.ToleranceClasses, comparer);
        }

        private class LocationModelMock : LocationModel
        {
            public bool WasUpdateByEntityCalled = false;
            public Location UpdateByEntityParameter;

            public override void UpdateWith(Location entity)
            {
                UpdateByEntityParameter = entity;
                WasUpdateByEntityCalled = true;
            }

            public LocationModelMock(Location entity) : base(entity, new NullLocalizationWrapper(), null)
            {
            }
        }

        private static IEnumerable<Tuple<Location, Location, Func<Location, object>, string>>
            GetLocationModelsForEqualityCheck()
        {
            var properties = new List<string>()
            {
                nameof(Location.Number),
                nameof(Location.Description),
                nameof(Location.ControlledBy),
                nameof(Location.SetPointTorque),
                nameof(Location.ToleranceClassTorque),
                nameof(Location.MinimumTorque),
                nameof(Location.MaximumTorque),
                nameof(Location.ToleranceClassAngle),
                nameof(Location.ThresholdTorque),
                nameof(Location.SetPointAngle),
                nameof(Location.MinimumAngle),
                nameof(Location.MaximumAngle),
                nameof(Location.ConfigurableField1),
                nameof(Location.ConfigurableField2),
                nameof(Location.ConfigurableField3),
                nameof(Location.Comment)
            };

            foreach (var property in properties)
            {
                yield return GetEqualLocationModelsExceptOneProperty(property);
            }
        }

        /// <returns>Item1/2: unequal pair, Item3: getter of changed property, Item4: setter of changed property, Item5: porpertyname</returns>
        private static Tuple<Location, Location, Func<Location, object>, string>
            GetEqualLocationModelsExceptOneProperty(string propertyName)
        {
            var loc1 = CreateLocation.Anonymous();
            loc1.Comment = "Test";
            var loc2 = LocationModel.GetModelFor(loc1, new NullLocalizationWrapper(), null).CopyDeep().Entity;
            loc2.Comment = "Test";
            Func<Location, object> getter = null;

            switch (propertyName)
            {
                case nameof(Location.Id):
                    loc2.Id = new LocationId(7412369);
                    getter = x => x.Id;
                    break;
                case nameof(Location.Number):
                    loc2.Number = new LocationNumber("rtgeoriguio");
                    getter = x => x.Number.ToDefaultString();
                    break;
                case nameof(Location.Description):
                    loc2.Description = new LocationDescription("ugtferdkfjd");
                    getter = x => x.Description.ToDefaultString();
                    break;
                case nameof(Location.ParentDirectoryId):
                    loc2.ParentDirectoryId = new LocationDirectoryId(467389);
                    getter = x => x.ParentDirectoryId;
                    break;
                case nameof(Location.ControlledBy):
                    loc2.ControlledBy = loc2.ControlledBy == LocationControlledBy.Torque
                        ? LocationControlledBy.Angle
                        : LocationControlledBy.Torque;
                    getter = x => x.ControlledBy;
                    break;
                case nameof(Location.SetPointTorque):
                    loc2.SetPointTorque = Torque.FromNm(789465);
                    getter = x => x.SetPointTorque.Nm.ToString(CultureInfo.CurrentCulture);
                    break;
                case nameof(Location.ToleranceClassTorque):
                    loc2.ToleranceClassTorque = new ToleranceClass()
                        {Id = new ToleranceClassId(54542512), Name = "405djwkrt0o"};
                    getter = x => x.ToleranceClassTorque.Name;
                    break;
                case nameof(Location.MinimumTorque):
                    loc2.MinimumTorque = Torque.FromNm(2304987);
                    getter = x => x.MinimumTorque.Nm.ToString(CultureInfo.CurrentCulture);
                    break;
                case nameof(Location.MaximumTorque):
                    loc2.MaximumTorque = Torque.FromNm(3645575);
                    getter = x => x.MaximumTorque.Nm.ToString(CultureInfo.CurrentCulture);
                    break;
                case nameof(Location.ToleranceClassAngle):
                    loc2.ToleranceClassAngle = new ToleranceClass()
                        {Id = new ToleranceClassId(76545), Name = "ritugrotugj"};
                    getter = x => x.ToleranceClassAngle.Name;
                    break;
                case nameof(Location.ThresholdTorque):
                    loc2.ThresholdTorque = Torque.FromNm(75932);
                    getter = x => x.ThresholdTorque.Nm.ToString(CultureInfo.CurrentCulture);
                    break;
                case nameof(Location.SetPointAngle):
                    loc2.SetPointAngle = Angle.FromDegree(3123456);
                    getter = x => x.SetPointAngle.Degree.ToString(CultureInfo.CurrentCulture);
                    break;
                case nameof(Location.MinimumAngle):
                    loc2.MinimumAngle = Angle.FromDegree(9524178563);
                    getter = x => x.MinimumAngle.Degree.ToString(CultureInfo.CurrentCulture);
                    break;
                case nameof(Location.MaximumAngle):
                    loc2.MaximumAngle = Angle.FromDegree(309485);
                    getter = x => x.MaximumAngle.Degree.ToString(CultureInfo.CurrentCulture);
                    break;
                case nameof(Location.ConfigurableField1):
                    loc2.ConfigurableField1 = new LocationConfigurableField1("eortugfhf");
                    getter = x => x.ConfigurableField1.ToDefaultString();
                    break;
                case nameof(Location.ConfigurableField2):
                    loc2.ConfigurableField2 = new LocationConfigurableField2("8");
                    getter = x => x.ConfigurableField2.ToDefaultString();
                    break;
                case nameof(Location.ConfigurableField3):
                    loc2.ConfigurableField3 = !loc2.ConfigurableField3;
                    getter = x => x.ConfigurableField3;
                    break;
                case nameof(Location.Comment):
                    loc2.Comment = "409r58tu9034e9rtgfvdefgb65ef";
                    getter = x => x.Comment;
                    break;
            }

            return new Tuple<Location, Location, Func<Location, object>, string>(loc1, loc2, getter, propertyName);
        }

        private LocationModel CreateLocationWithOnlyId(long id)
        {
            return new LocationModel(new Location(), new NullLocalizationWrapper(), null)
            {
                Id = id
            };
        }

        private LocationUseCaseMock CreateLocationUseCaseMock()
        {
            return new LocationUseCaseMock(new ToleranceClassUseCaseMock(null, null, null, null));
        }

        private LocationViewModel CreateLocationViewModelWithValidatorMock(LocationUseCaseMock useCase,
            LocationValidatorMock locationValidatorMock)
        {
            return new LocationViewModel(useCase, new StartUpMock(), locationValidatorMock,
                new NullLocalizationWrapper(), new MockThreadCreator(), new MockLocationDisplayFormatter());
        }

        private LocationViewModel CreateLocationViewModel(LocationUseCaseMock useCase)
        {
            return new LocationViewModel(useCase, new StartUpMock(), new LocationValidatorMock(),
                new NullLocalizationWrapper(), new MockThreadCreator(), new MockLocationDisplayFormatter());
        }

        private LocationViewModel CreateLocationViewModel(LocationUseCaseMock useCase, LocationValidator validator)
        {
            return new LocationViewModel(useCase, new StartUpMock(), validator, new NullLocalizationWrapper(), new MockThreadCreator(), new MockLocationDisplayFormatter());
        }

        private LocationViewModel CreateLocationViewModel(StartUpMock startUpMock, IThreadCreator threadCreator)
        {
            return new LocationViewModel(null, startUpMock, null, null, threadCreator, null);
        }

        private LocationViewModel CreateLocationViewModel(ILocationUseCase locationUseCase,StartUpMock startUpMock, IThreadCreator threadCreator)
        {
            return new LocationViewModel(locationUseCase, startUpMock, null, null, threadCreator, new MockLocationDisplayFormatter());
        }

        private LocationModel CreateAnonymousLocationModel()
        {
            return new LocationModel(new Location(), new NullLocalizationWrapper(), null)
            {
                Id = 5
            };
        }

        private LocationDirectory CreateLocationDirectoryByIdOnly(long id)
        {
            return new LocationDirectory
            {
                Id = new LocationDirectoryId(id),
                ParentId = new LocationDirectoryId(15)
            };
        }
    }

    internal class MockChangeToolStatusAssistent : IChangeToolStateView, ICanShowDialog
    {
        public object DataContext { get => new ChangeToolStateViewModel(null, new ChangeToolStateForLocationUseCaseMock(null, null, null), null); set => throw new NotImplementedException(); }

        public event EventHandler EndOfAssistent;

        public void SetAssignedTools(List<LocationToolAssignmentChangeStateModel> list)
        {
            throw new NotImplementedException();
        }

        public void InvokeEndOfAssiststent()
        {
            EndOfAssistent.Invoke(null, System.EventArgs.Empty);
        }

        public bool? ShowDialog()
        {
            return null;
        }
    }
}