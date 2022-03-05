using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using TestHelper.Mock;
using NUnit.Framework;
using TestHelper.Factories;

namespace Core.Test.UseCases
{
    public class ManufacturerUseCaseTest
    {
        [Test]
        public void LoadManufacturerTest()
        {
            var environment = new Environment();
            var manufacturers = new List<Manufacturer>();
            environment.Mock.Data.LoadManufacturerData = manufacturers;
            environment.UseCase.LoadManufacturer();
            Assert.AreSame(manufacturers, environment.Mock.Gui.ShownManufacturers);
        }

        [Test]
        public void LoadManufacturerErrorTest()
        {
            var environment = new Environment();
            environment.Mock.Data.LoadManufacturersException = new Exception();
            environment.UseCase.LoadManufacturer();
            Assert.IsTrue(environment.Mock.Gui.ShowErrorMessageWasCalled);
        }

        public static List<Manufacturer> AddManufacturerTestList = new List<Manufacturer>
        {
            CreateManufacturer.Parametrized(
                5,
                "Experienced Procedure",
                "Secure Link",
                "Strategic Cafe",
                "Revolutionary Coach",
                "Arbitrary Creature",
                "Very Display",
                "Cruel Jet",
                "Accurate Cereal",
                "Relative Decision-Making"),
            CreateManufacturer.Parametrized(
                65798,
                "Casual Grammar",
                "Odd Grin",
                "Sound Warrior",
                "Imminent Gown",
                "Invaluable Writing",
                "Municipal Photograph",
                "Giant Predator",
                "Vocational Output",
                "Private Judgment")
        };

        [TestCaseSource(nameof(AddManufacturerTestList))]
        public void AddManufacturerTest(Manufacturer manufacturerToAdd)
        {
            var environment = new Environment();
            var expectedManufacturer = manufacturerToAdd.CopyDeep();
            environment.Mock.Data.AddManufacturerId = manufacturerToAdd.Id.ToLong();
            manufacturerToAdd.Id = new ManufacturerId(0); // reset Id, because it needs to be loaded from DataInterface
            environment.UseCase.AddManufacturer(manufacturerToAdd);
            Assert.IsTrue(expectedManufacturer.EqualsByContent(environment.Mock.Gui.AddedManufacturer));
        }

        [Test]
        public void AddManufacturerErrorTest()
        {
            var environment = new Environment();
            environment.Mock.Data.AddManufacturerException = new Exception();
            environment.UseCase.AddManufacturer(CreateManufacturer.Anonymous());
            Assert.IsTrue(environment.Mock.Gui.ShowErrorMessageWasCalled);
        }

		[TestCase(5)]
		[TestCase(33)]
		public void AddingManufacturerPassesCurrentUser(int userAddedByUserId)
		{
            var environment = new Environment();
            environment.Mock.UserGetter.NextReturnedUser = CreateUser.IdOnly(userAddedByUserId);
            environment.UseCase.AddManufacturer(CreateManufacturer.Anonymous());
            Assert.AreEqual(userAddedByUserId, environment.Mock.Data.AddManufacturerUser.UserId.ToLong());
        }

		[Test]
        public void RemoveManufacturerTest()
        {
            var environment = new Environment();
            var manufacturerToRemove = CreateManufacturer.Anonymous();
            environment.UseCase.RemoveManufacturer(manufacturerToRemove, environment.Mock.Gui);
            Assert.AreSame(manufacturerToRemove, environment.Mock.Gui.RemovedManufacturer);
            Assert.AreSame(manufacturerToRemove, environment.Mock.Data.RemovedManufacturer);
        }

        [TestCase(10)]
		[TestCase(23578392)]
		public void RemovingManufacturerSuppliesCorrectUser(long userId)
		{
            var environment = new Environment();
            environment.Mock.UserGetter.NextReturnedUser = CreateUser.IdOnly(userId);
            environment.UseCase.RemoveManufacturer(CreateManufacturer.Anonymous(), environment.Mock.Gui);
            Assert.AreEqual(userId, environment.Mock.Data.RemoveManufacturerUser.UserId.ToLong());
		}

        [Test]
        public void RemoveManufacturerErrorTest()
        {
            var environment = new Environment();
            environment.Mock.Data.RemoveManufacturerException = new Exception();
            environment.UseCase.RemoveManufacturer(CreateManufacturer.Anonymous(), environment.Mock.Gui);
            Assert.IsTrue(environment.Mock.Gui.ShowErrorMessageWasCalled);
        }

        [Test]
        public void RemoveManufacturerWithReferencesCallsGuiShowRemoveManufacturerPreventingReferences()
        {
            var environment = new Environment();
            var toolModel = new ToolModelReferenceLink { Id = new ToolModelId(15) };
            environment.Mock.Data.ReferencedToolModels = new List<ToolModelReferenceLink> {toolModel};
            environment.UseCase.RemoveManufacturer(CreateManufacturer.Anonymous(), environment.Mock.Gui);
            Assert.AreEqual(
                1,
                environment.Mock.Gui.ShowRemoveManufacturerPreventingReferencesParameter.Count);
            Assert.AreEqual(
                toolModel.Id.ToLong(),
                environment.Mock.Gui.ShowRemoveManufacturerPreventingReferencesParameter[0].Id.ToLong());
        }

        [Test]
        public void SaveManufacturerTest()
        {
            var environment = new Environment();
            var updatedManufacturer = CreateManufacturer.Anonymous();
            environment.UseCase.SaveManufacturer(null, updatedManufacturer);
            Assert.AreSame(updatedManufacturer, environment.Mock.Data.SavedManufacturer);
            Assert.AreSame(updatedManufacturer, environment.Mock.Gui.SavedManufacturer);
        }

        [TestCase(5)]
        [TestCase(68)]
        public void SavingManufacturerPassesOldManufacturer(long manuId)
        {
            var environment = new Environment();
            environment.UseCase.SaveManufacturer(
                CreateManufacturer.IdOnly(manuId),
                CreateManufacturer.IdOnly(manuId));
            Assert.AreEqual(
                manuId,
                environment.Mock.Data.LastSaveManufacturerDiff.GetOldManufacturer().Id.ToLong());
        }

		[TestCase(5)]
		[TestCase(33)]
		public void SavingManufacturerPassesCurrentUser(int userAddedByUserId)
		{
            var environment = new Environment();
            environment.Mock.UserGetter.NextReturnedUser = CreateUser.IdOnly(userAddedByUserId);
            environment.UseCase.SaveManufacturer(
                CreateManufacturer.Anonymous(),
                CreateManufacturer.Anonymous());
            Assert.AreEqual(userAddedByUserId, environment.Mock.Data.SaveManufacturerUser.UserId.ToLong());
        }

		[Test]
        public void SaveManufacturerErrorTest()
        {
            var environment = new Environment();
            environment.Mock.Data.SaveManufacturerException = new Exception();
            environment.UseCase.SaveManufacturer(
                CreateManufacturer.Anonymous(),
                CreateManufacturer.Anonymous());
            Assert.IsTrue(environment.Mock.Gui.ShowErrorMessageWasCalled);
        }

        [TestCase("Test1")]
        [TestCase("Hans")]
        public void LoadCommentForManufacturerTest(string comment)
        {
            var environment = new Environment();
            environment.Mock.Data.LoadCommentForManufacturerReturn = comment;
            var manufacturer = CreateManufacturer.Anonymous();
            environment.UseCase.LoadCommentForManufacturer(manufacturer);
            Assert.AreEqual(comment, environment.Mock.Gui.ShowCommentParameterComment);
            Assert.AreSame(manufacturer, environment.Mock.Gui.ShowCommentParameterManufacturer);
        }

        [Test]
        public void LoadCommentForManufacturerErrorTest()
        {
            var environment = new Environment();
            environment.Mock.Data.LoadCommentForManufacturerException = new Exception();
            environment.UseCase.LoadCommentForManufacturer(CreateManufacturer.Anonymous());
            Assert.IsTrue(environment.Mock.Gui.ShowCommentErrorMessageWasCalled);
        }

        public static List<List<ToolModelReferenceLink>> ReferencedToolModelsTestList =
            new List<List<ToolModelReferenceLink>>
            {
                new List<ToolModelReferenceLink>
                {
                    new ToolModelReferenceLink {Id = new ToolModelId(1), DisplayName = "ToolModel 1"},
                    new ToolModelReferenceLink {Id = new ToolModelId(2), DisplayName = "ToolModel 2"}
                },
                new List<ToolModelReferenceLink>
                {
                    new ToolModelReferenceLink {Id = new ToolModelId(7), DisplayName = "Skilled Daughter"},
                    new ToolModelReferenceLink {Id = new ToolModelId(9), DisplayName = "Long-Term Warning"},
                    new ToolModelReferenceLink {Id = new ToolModelId(5), DisplayName = "Mutual Outlook"},
                    new ToolModelReferenceLink {Id = new ToolModelId(6), DisplayName = "Temporary Pillow"}
                }
            };

        [TestCaseSource(nameof(ReferencedToolModelsTestList))]
        public void LoadReferencedToolModelsTest(List<ToolModelReferenceLink> toolModels)
        {
            var environment = new Environment();
            environment.Mock.Data.ReferencedToolModels = toolModels;
            environment.UseCase.LoadReferencedToolModels(0);
            CollectionAssert.AreEqual(toolModels, environment.Mock.Gui.ReferencedToolModels);
        }

        [Test]
        public void LoadReferencedToolModelsErrorTest()
        {
            var environment = new Environment();
            environment.Mock.Data.ReferencedToolModelsException = new Exception();
            environment.UseCase.LoadReferencedToolModels(0);
            Assert.IsTrue(environment.Mock.Gui.ShowReferencesErrorCalled);
        }

        [Test]
        public void AddManufacturerWithoutErrorCallsSendSuccessNotification()
        {
            var environment = new Environment();
            environment.UseCase.AddManufacturer(CreateManufacturer.Anonymous());
            Assert.IsTrue(environment.Mock.NotificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void RemoveManufacturertWithoutErrorCallsSendSuccessNotification()
        {
            var environment = new Environment();
            environment.UseCase.RemoveManufacturer(CreateManufacturer.Anonymous(), environment.Mock.Gui);
            Assert.IsTrue(environment.Mock.NotificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void SaveManufacturerWithoutErrorCallsSendSuccessNotification()
        {
            var environment = new Environment();
            environment.UseCase.SaveManufacturer(CreateManufacturer.Anonymous(), CreateManufacturer.Anonymous());
            Assert.IsTrue(environment.Mock.NotificationManager.SendSuccessNotificationCalled);
        }

        private class GuiInterfaceMock : IManufacturerGui
        {
            public void ShowManufacturer(List<Manufacturer> manufacturer)
            {
                ShownManufacturers = manufacturer;
            }

            public void AddManufacturer(Manufacturer manufacturer)
            {
                AddedManufacturer = manufacturer;
            }

            public void ShowErrorMessage()
            {
                ShowErrorMessageWasCalled = true;
            }

            public void ShowComment(Manufacturer manufacturer, string comment)
            {
                ShowCommentParameterManufacturer = manufacturer;
                ShowCommentParameterComment = comment;
            }

            public void ShowCommentError()
            {
                ShowCommentErrorMessageWasCalled = true;
            }

            public void SaveManufacturer(Manufacturer manufacturer)
            {
                SavedManufacturer = manufacturer;
            }

            public void RemoveManufacturer(Manufacturer manufacturer)
            {
                RemovedManufacturer = manufacturer;
            }

            public void ShowReferencedToolModels(List<ToolModelReferenceLink> toolModelReferenceLinks)
            {
                ReferencedToolModels = toolModelReferenceLinks;
            }

            public void ShowReferencesError()
            {
                ShowReferencesErrorCalled = true;
            }

            public void ShowRemoveManufacturerPreventingReferences(List<ToolModelReferenceLink> references)
            {
                ShowRemoveManufacturerPreventingReferencesParameter = references;
            }

            public List<Manufacturer> ShownManufacturers;
            public bool ShowErrorMessageWasCalled = false;
            public Manufacturer AddedManufacturer;
            public Manufacturer RemovedManufacturer;
            public List<ToolModelReferenceLink> ShowRemoveManufacturerPreventingReferencesParameter;
            public Manufacturer SavedManufacturer;
            public string ShowCommentParameterComment;
            public Manufacturer ShowCommentParameterManufacturer;
            public bool ShowCommentErrorMessageWasCalled = false;
            public List<ToolModelReferenceLink> ReferencedToolModels;
            public bool ShowReferencesErrorCalled;
        }

        private class DataInterfaceMock : IManufacturerData
        {
            public List<Manufacturer> LoadManufacturer()
            {
                if (LoadManufacturersException != null)
                {
                    throw LoadManufacturersException;
                }
                return LoadManufacturerData;
            }

            public Manufacturer AddManufacturer(Manufacturer manufacturer, User byUser)
            {
                AddManufacturerUser = byUser;
                if (AddManufacturerException != null)
                {
                    throw AddManufacturerException;
                }
                manufacturer.Id = new ManufacturerId(AddManufacturerId);
                return manufacturer;
            }

            public void RemoveManufacturer(Manufacturer manufacturer, User byUser)
            {
                RemovedManufacturer = manufacturer;
                RemoveManufacturerUser = byUser;
                if (RemoveManufacturerException != null)
                {
                    throw RemoveManufacturerException;
                }
            }

            public Manufacturer SaveManufacturer(ManufacturerDiff manufacturer)
            {
                SavedManufacturer = manufacturer.GetNewManufacturer();
                SaveManufacturerUser = manufacturer.GetUser();
                LastSaveManufacturerDiff = manufacturer;
                if (SaveManufacturerException != null)
                {
                    throw SaveManufacturerException;
                }
                return SavedManufacturer;
            }

            public string LoadManufacturerForComment(Manufacturer manufacturer)
            {
                if (LoadCommentForManufacturerException != null)
                {
                    throw LoadCommentForManufacturerException;
                }
                return LoadCommentForManufacturerReturn;
            }

            public List<ToolModelReferenceLink> LoadToolModelReferenceLinksForManufacturerId(long manufacturerId)
            {
                if (ReferencedToolModelsException != null)
                {
                    throw ReferencedToolModelsException;
                }
                return ReferencedToolModels;
            }

            public List<Manufacturer> LoadManufacturerData;
            public Exception LoadManufacturersException;
            public long AddManufacturerId;
            public Exception AddManufacturerException;
            public User AddManufacturerUser;
            public Manufacturer RemovedManufacturer;
            public User RemoveManufacturerUser;
            public Exception RemoveManufacturerException;
            public List<ToolModelReferenceLink> ReferencedToolModels;
            public Manufacturer SavedManufacturer;
            public ManufacturerDiff LastSaveManufacturerDiff;
            public User SaveManufacturerUser;
            public Exception SaveManufacturerException;
            public string LoadCommentForManufacturerReturn;
            public Exception LoadCommentForManufacturerException;
            public Exception ReferencedToolModelsException;
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    Gui = new GuiInterfaceMock();
                    Data = new DataInterfaceMock();
                    UserGetter = new UserGetterMock();
                    NotificationManager = new NotificationManagerMock();
                }

                public readonly GuiInterfaceMock Gui;
                public readonly DataInterfaceMock Data;
                public readonly UserGetterMock UserGetter;
                public readonly NotificationManagerMock NotificationManager;
            }

            public Environment()
            {
                Mock = new Mocks();
                UseCase = new ManufacturerUseCase(
                    Mock.Gui,
                    Mock.Data,
                    Mock.UserGetter,
                    Mock.NotificationManager);
            }

            public readonly Mocks Mock;
            public readonly ManufacturerUseCase UseCase;
        }
    }
}