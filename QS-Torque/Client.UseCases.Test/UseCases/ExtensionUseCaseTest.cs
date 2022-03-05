using System;
using System.Collections.Generic;
using Client.Core.Diffs;
using Client.TestHelper.Factories;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using NUnit.Framework;
using TestHelper.Factories;
using TestHelper.Mock;

namespace Core.Test.UseCases
{
    public class ExtensionUseCaseTest
    {
        public class ExtensionGuiMock : IExtensionGui
        {
            public List<Extension> ShowExtensionsParameter { get; set; }
            public bool ShowExtensionsError { get; set; }
            public List<LocationReferenceLink> ShowReferencedLocationsParameter { get; set; }
            public bool ShowReferencedLocationsError { get; set; }
            public Extension AddExtensionParameter { get; set; }
            public Extension UpdateExtensionParameter { get; set; }
            public Extension RemoveExtensionParameter { get; set; }

            public void ShowExtensions(List<Extension> extensions)
            {
                if (ShowExtensionsError)
                {
                    throw new Exception();
                }
                ShowExtensionsParameter = extensions;
            }

            public void ShowReferencedLocations(List<LocationReferenceLink> locationReferenceLinks)
            {
                if (ShowReferencedLocationsError)
                {
                    throw new Exception();
                }
                ShowReferencedLocationsParameter = locationReferenceLinks;
            }

            public void AddExtension(Extension addedExtension)
            {
                AddExtensionParameter = addedExtension;
            }

            public void UpdateExtension(Extension updatedExtension)
            {
                UpdateExtensionParameter = updatedExtension;
            }

            public void RemoveExtension(Extension extension)
            {
                RemoveExtensionParameter = extension;
            }
        }

        private class ExtensionDependencyGuiMock : IExtensionDependencyGui
        {
            public void ShowRemoveExtensionPreventingReferences(List<LocationReferenceLink> references)
            {
                ShowRemoveExtensionPreventingReferencesParameter = references;
            }

            public List<LocationReferenceLink> ShowRemoveExtensionPreventingReferencesParameter { get; set; }
        }

        private class ExtensionErrorMock : IExtensionErrorGui
        {
            public void ShowErrorMessageLoadingExentsions()
            {
                ShowErrorMessageLoadingExtensionsCalled = true;
            }

            public void ShowErrorMessageLoadingReferencedLocations()
            {
                ShowErrorMessageLoadingReferencedLocationsCalled = true;
            }

            public void ShowProblemSavingExtension()
            {
                ShowProblemSavingExtensionCalled = true;
            }

            public void ExtensionAlreadyExists()
            {
                ExtensionAlreadyExistsCalled = true;
            }

            public void ShowProblemRemoveExtension()
            {
                ShowProblemRemoveExtensionCalled = true;
            }

            public bool ShowProblemRemoveExtensionCalled { get; set; }
            public bool ExtensionAlreadyExistsCalled { get; set; }
            public bool ShowProblemSavingExtensionCalled { get; set; }
            public bool ShowErrorMessageLoadingExtensionsCalled { get; set; }
            public bool ShowErrorMessageLoadingReferencedLocationsCalled { get; set; }

        }

        private class ExtensionSaveGuiShowerMock : IExtensionSaveGuiShower
        {
            public void SaveExtension(ExtensionDiff diff, Action saveAction)
            {
                SaveExtensionParameterDiff = diff;
                SaveExtensionParameterSaveAction = saveAction;
                saveAction();
            }

            public Action SaveExtensionParameterSaveAction { get; set; }

            public ExtensionDiff SaveExtensionParameterDiff { get; set; }
        }

        [Test]
        public void ShowExtensionsCallsDataaccess()
        {
            var environment = new Environment();
            environment.UseCase.ShowExtensions(null);
            Assert.IsTrue(environment.Mock.Data.LoadExtensionsCalled);
        }

        [Test]
        public void ShowExtensionsCallsGui()
        {
            var environment = new Environment();

            var extensions = new List<Extension>();

            environment.Mock.Data.LoadExtensionsData = extensions;
            environment.UseCase.ShowExtensions(null);
            Assert.AreSame(extensions, environment.Mock.Gui.ShowExtensionsParameter);
        }

        [Test]
        public void ShowTestEquipmentHasErrorCallsShowLoadingError()
        {
            var environment = new Environment();
            var errorMock = new ExtensionErrorMock();
            environment.Mock.Gui.ShowExtensionsError = true;
            environment.UseCase.ShowExtensions(errorMock);
            Assert.IsTrue(errorMock.ShowErrorMessageLoadingExtensionsCalled);
        }

        [Test]
        public void ShowReferencedLocationsCallsDataAcess()
        {
            var environment = new Environment();
            var id = new ExtensionId(123);
            environment.UseCase.ShowReferencedLocations(null, id);
            Assert.IsTrue(environment.Mock.Data.LoadReferencedLocationsCalled);
        }

        [Test]
        public void ShowReferencedLocationsCallsGui()
        {
            var environment = new Environment();
            var referencedLocations = new List<LocationReferenceLink>();
            var id = new ExtensionId(123);
            environment.Mock.Data.LoadReferencedLocationsReturnValue = referencedLocations;
            environment.UseCase.ShowReferencedLocations(null, id);
            Assert.AreSame(referencedLocations, environment.Mock.Gui.ShowReferencedLocationsParameter);
        }

        [Test]
        public void ShowReferenecdLocationsHasErrorCallsShowLoadingError()
        {
            var environment = new Environment();
            var errorMock = new ExtensionErrorMock();
            var id = new ExtensionId(123);
            environment.Mock.Gui.ShowReferencedLocationsError = true;
            environment.UseCase.ShowReferencedLocations(errorMock, id);
            Assert.IsTrue(errorMock.ShowErrorMessageLoadingReferencedLocationsCalled);                
        }

        [Test]
        public void AddExtensionCallsDataAccess()
        {
            var environment = new Environment();
            var user = CreateUser.Anonymous();
            environment.Mock.UserGetter.NextReturnedUser = user;
            var extension = CreateExtension.Randomized(433647);
            environment.UseCase.AddExtension(extension, null);
            Assert.AreSame(extension, environment.Mock.Data.AddExtensionParameterExtension);
            Assert.AreSame(user, environment.Mock.Data.AddExtensionParameterUser);
        }

        [Test]
        public void AddExtensionCallsGui()
        {
            var environment = new Environment();
            var extension = CreateExtension.Randomized(433647);
            environment.Mock.Data.AddExtensionReturnValue = extension;
            environment.UseCase.AddExtension(CreateExtension.Randomized(456456), null);
            Assert.AreSame(extension, environment.Mock.Gui.AddExtensionParameter);
        }

        [Test]
        public void AddExtensionCallsNotificationManager()
        {
            var environment = new Environment();
            environment.UseCase.AddExtension(CreateExtension.Randomized(456456), null);
            Assert.IsTrue(environment.Mock.notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void AddExtensionCallsShowProblemSavingExtension()
        {
            var environment = new Environment();
            environment.Mock.Data.AddExtensionThrowsException = true;
            environment.UseCase.AddExtension(CreateExtension.Randomized(456456), environment.Mock.Error);
            Assert.IsTrue(environment.Mock.Error.ShowProblemSavingExtensionCalled);
        }

        [TestCase("324356")]
        [TestCase("abcdefg")]
        public void IsInventoryNumberUniqueCallsDataAccess(string inventoryNumber)
        {
            var environment = new Environment();
            environment.UseCase.IsInventoryNumberUnique(new ExtensionInventoryNumber(inventoryNumber));
            Assert.AreEqual(inventoryNumber, environment.Mock.Data.IsInventoryNumberUniqueParameter.ToDefaultString());
        }

        [TestCase(false)]
        [TestCase(true)]
        public void IsInventoryNumberUniqueReturnsCorrectValue(bool isInventoryNumberUnique)
        {
            var environment = new Environment();
            environment.Mock.Data.IsInventoryNumberUniqueReturnValue = isInventoryNumberUnique;
            Assert.AreEqual(isInventoryNumberUnique, environment.UseCase.IsInventoryNumberUnique(new ExtensionInventoryNumber("")));
        }


        [Test]
        public void SaveExtensionCallsDataAccess()
        {
            var environment = new Environment();
            var diff = new ExtensionDiff(CreateUser.Anonymous(), new HistoryComment(""), CreateExtension.Randomized(3245), CreateExtension.Randomized(3245));
            environment.UseCase.SaveExtension(diff, null, environment.Mock.SaveGuiShower);

            Assert.AreSame(diff, environment.Mock.Data.SaveExtensionParameterDiff);
        }

        [Test]
        public void SaveExtensionSetsUser()
        {
            var environment = new Environment();
            var user = CreateUser.Anonymous();
            environment.Mock.UserGetter.NextReturnedUser = user;
            var diff = new ExtensionDiff(user, new HistoryComment(""), CreateExtension.Randomized(3245), CreateExtension.Randomized(3245));
            environment.UseCase.SaveExtension(diff, null, environment.Mock.SaveGuiShower);

            Assert.AreSame(user, environment.Mock.Data.SaveExtensionParameterDiff.User);
        }

        [Test]
        public void SaveExtensionCallsSuccessNotification()
        {
            var environment = new Environment();
            var diff = new ExtensionDiff(new User(), new HistoryComment(""), CreateExtension.Randomized(3245), CreateExtension.Randomized(3245));
            environment.UseCase.SaveExtension(diff, null, environment.Mock.SaveGuiShower);

            Assert.IsTrue(environment.Mock.notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void SaveExtensionCallsGui()
        {
            var environment = new Environment();
            var diff = new ExtensionDiff(new User(), new HistoryComment(""), CreateExtension.Randomized(3245), CreateExtension.Randomized(3245));
            environment.UseCase.SaveExtension(diff, null, environment.Mock.SaveGuiShower);

            Assert.AreSame(diff.NewExtension, environment.Mock.Gui.UpdateExtensionParameter);
        }


        [Test]
        public void SaveExtensionWithErrorCallsShowSavingError()
        {
            var environment = new Environment();
            environment.Mock.Data.SaveExtensionThrowsError = true;

            var diff = new ExtensionDiff(new User(), new HistoryComment(""), new Extension(), new Extension());
            environment.UseCase.SaveExtension(diff, environment.Mock.Error, environment.Mock.SaveGuiShower);

            Assert.IsTrue(environment.Mock.Error.ShowProblemSavingExtensionCalled);
        }

        [Test]
        public void SaveExtensionCallsGuiShowerSaveExtension()
        {
            var environment = new Environment();

            var diff = new ExtensionDiff(new User(), new HistoryComment(""), new Extension(), new Extension());
            environment.UseCase.SaveExtension(diff, environment.Mock.Error, environment.Mock.SaveGuiShower);

            Assert.AreSame(diff, environment.Mock.SaveGuiShower.SaveExtensionParameterDiff);
        }

        [TestCase("231425")]
        [TestCase("abcde")]
        public void SaveExtensionCallsDataAccessIsInventoryNumberUnique(string inventoryNumber)
        {
            var environment = new Environment();

            environment.UseCase.SaveExtension(
                new ExtensionDiff(new User(), new HistoryComment(""), CreateExtension.RandomizedWithInventoryNumber(32324, "s1"),
                    CreateExtension.RandomizedWithInventoryNumber(32324, inventoryNumber)), environment.Mock.Error,
                environment.Mock.SaveGuiShower);

            Assert.AreEqual(inventoryNumber, environment.Mock.Data.IsInventoryNumberUniqueParameter.ToDefaultString());
        }

        [Test]
        public void SaveExtensionCallsExtensionAlreadyExistsIfInventoryNumberIsNotUnique()
        {
            var environment = new Environment();
            environment.Mock.Data.IsInventoryNumberUniqueReturnValue = false;

            environment.UseCase.SaveExtension(
                new ExtensionDiff(new User(), new HistoryComment(""), CreateExtension.RandomizedWithInventoryNumber(32324, "s1"),
                    CreateExtension.RandomizedWithInventoryNumber(32324, "s2")), environment.Mock.Error,
                environment.Mock.SaveGuiShower);

            Assert.IsTrue(environment.Mock.Error.ExtensionAlreadyExistsCalled);
        }

        [Test]
        public void RemoveExtensionCallsDataAccess()
        {
            var environment = new Environment();
            var user = CreateUser.Anonymous();
            environment.Mock.UserGetter.NextReturnedUser = user;
            var extension = CreateExtension.Randomized(32424);
            environment.UseCase.RemoveExtension(extension, null, environment.Mock.ExtensionDependencyGui);

            Assert.AreSame(extension, environment.Mock.Data.RemoveExtensionExtension);
            Assert.AreSame(user, environment.Mock.Data.RemoveExtensionUser);
        }

        [Test]
        public void RemoveExtensionCallsSuccessNotification()
        {
            var environment = new Environment();
            environment.UseCase.RemoveExtension(CreateExtension.Randomized(45646), null, environment.Mock.ExtensionDependencyGui);

            Assert.IsTrue(environment.Mock.notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void RemoveExtensionCallsGui()
        {
            var environment = new Environment();
            var extension = CreateExtension.Randomized(34535);
            environment.UseCase.RemoveExtension(extension, null, environment.Mock.ExtensionDependencyGui);

            Assert.AreSame(extension, environment.Mock.Gui.RemoveExtensionParameter);
        }

        [Test]
        public void RemoveExtensionWithErrorCallsShowProblemRemoveExtension()
        {
            var environment = new Environment();
            environment.Mock.Data.RemoveExtensionThrowsError = true;

            environment.UseCase.RemoveExtension(CreateExtension.Randomized(34535), environment.Mock.Error, environment.Mock.ExtensionDependencyGui);

            Assert.IsTrue(environment.Mock.Error.ShowProblemRemoveExtensionCalled);
        }

        [Test]
        public void RemoveExtensionCallsLoadReferencedLocations()
        {
            var environment = new Environment();
            var extensionId = new ExtensionId(5);
            var extension = CreateExtension.Randomized(54647);
            extension.Id = extensionId;
            environment.UseCase.RemoveExtension(extension,null, environment.Mock.ExtensionDependencyGui);

            Assert.AreSame(extensionId, environment.Mock.Data.LoadReferencedLocationsParameter);
        }

        [Test]
        public void RemoveExtensionWithReferencesTest()
        {
            var environment = new Environment();
            
            var reference = new List<LocationReferenceLink>()
            {
                new LocationReferenceLink(new QstIdentifier(1), new LocationNumber("235"),new LocationDescription("436"),  new MockLocationDisplayFormatter() ),
            };
            environment.Mock.Data.LoadReferencedLocationsReturnValue = reference;
            environment.UseCase.RemoveExtension(CreateExtension.Randomized(54647), null, environment.Mock.ExtensionDependencyGui);
            Assert.AreSame(reference, environment.Mock.ExtensionDependencyGui.ShowRemoveExtensionPreventingReferencesParameter);
            Assert.IsNull(environment.Mock.Data.RemoveExtensionExtension);
        }

        [Test]
        public void RemoveExtensionWithNoReferencesTest()
        {
            var environment = new Environment();

            var reference = new List<LocationReferenceLink>(){};
            environment.Mock.Data.LoadReferencedLocationsReturnValue = reference;
            environment.UseCase.RemoveExtension(CreateExtension.Randomized(54647), null, environment.Mock.ExtensionDependencyGui);
            Assert.IsNull(environment.Mock.ExtensionDependencyGui.ShowRemoveExtensionPreventingReferencesParameter);
            Assert.IsNotNull(environment.Mock.Data.RemoveExtensionExtension);
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    Data = new ExtensionDataAccessMock();
                    Gui = new ExtensionGuiMock();
                    Error = new ExtensionErrorMock();
                    UserGetter = new UserGetterMock();
                    notificationManager = new NotificationManagerMock();
                    SaveGuiShower = new ExtensionSaveGuiShowerMock();
                    ExtensionDependencyGui = new ExtensionDependencyGuiMock();
                }

                public readonly ExtensionDataAccessMock Data;
                public readonly ExtensionGuiMock Gui;
                public readonly ExtensionErrorMock Error;
                public readonly UserGetterMock UserGetter;
                public readonly NotificationManagerMock notificationManager;
                public readonly ExtensionSaveGuiShowerMock SaveGuiShower;
                public readonly ExtensionDependencyGuiMock ExtensionDependencyGui;
            }

            public Environment()
            {
                Mock = new Mocks();
                UseCase = new ExtensionUseCase(Mock.Gui, Mock.Data, Mock.UserGetter, Mock.notificationManager);
            }

            public readonly Mocks Mock;
            public readonly ExtensionUseCase UseCase;
        }
    }
}
