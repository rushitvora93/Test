using Core.Entities;
using Core.UseCases;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Core.Entities.ReferenceLink;
using TestHelper.Factories;
using TestHelper.Mock;

namespace Core.Test.UseCases
{
    class ToolUseCaseTest
    {
        private class ToolComparer : IEqualityComparer<Tool>
        {
            public bool Equals(Tool x, Tool y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(Tool obj)
            {
                return obj.Id.GetHashCode();
            }
        }

        private class ToolData : IToolData
        {
            public bool RaiseLoadToolsException { get; set; } = false;
            public List<List<Tool>> Tools { get; set; }
            public string CommentToLoad { get; set; }
            public Picture PictureToLoad { get; set; }

            public Tool AddedTool { get; set; }
            public ToolId AddId { get; set; }
            public bool AddToolShowError { get; set; }
            public User LastAddToolUser;

            public bool IsSerialNumberUniqueReturnValue { get; set; } = true;
            public bool WasIsSerialNumberUniqueInvoked { get; private set; }
            public string IsSerialNumberUniqueParameter { get; private set; }

            public bool IsInventoryNumberUniqueReturnValue { get; set; } = true;
            public bool WasIsInventoryNumberUniqueInvoked { get; private set; }
            public string IsInventoryNumberUniqueParameter { get; private set; }
            public ToolDiff UpdateToolParameter { get; set; }
            public bool UpdateToolThrowsException { get; set; }
            

            public List<LocationToolAssignmentReferenceLink> LoadReferencedLocationToolAssignmentsReturn { get; set; } = new List<LocationToolAssignmentReferenceLink>();
            public bool LoadLocationToolAssignmentLinksForToolIdCalled = false;

            public bool RemoveToolThrowsException;

            public bool LoadToolsForModelThrowsException;

            public bool LoadModelsWithAtLeasOneToolThrowsExcpetion;

            public List<Tool> LoadToolsForModelReturnValue;

            public List<ToolModel> LoadModelsWithAtLeasOneToolReturnValue = null;

            public int LoadModelsWithAtLeasOneToolCallCount;
            
            public Tool RemovedTool;
            public User RemoveToolUserParameter { get; set; }
            public IEnumerable<List<Tool>> LoadTools()
            {
                if (RaiseLoadToolsException)
                {
                    throw new Exception();
                }
                else
                {
                    return Tools;
                }
            }

            public string LoadComment(Tool tool)
            {
                return CommentToLoad;
            }

            public Picture LoadPictureForTool(Tool tool)
            {
                return PictureToLoad;
            }

            public Tool AddTool(Tool newTool, User byUser)
            {
                LastAddToolUser = byUser;
                if (AddToolShowError)
                {
                    throw new Exception("test");
                }

                newTool.Id = AddId; // Id is a positive number and not null
                AddedTool = newTool;

                return newTool;
            }

            public List<ToolModel> LoadModelsWithAtLeasOneTool()
            {
                if (LoadModelsWithAtLeasOneToolThrowsExcpetion)
                {
                    throw new Exception("");
                }

                LoadModelsWithAtLeasOneToolCallCount++;
                return LoadModelsWithAtLeasOneToolReturnValue;
            }

            public List<Tool> LoadToolsForModel(ToolModel toolModel)
            {
                if (LoadToolsForModelThrowsException)
                {
                    throw new Exception("");
                }
                return LoadToolsForModelReturnValue;
            }

            public bool IsSerialNumberUnique(string serialNumber)
            {
                WasIsSerialNumberUniqueInvoked = true;
                IsSerialNumberUniqueParameter = serialNumber;
                return IsSerialNumberUniqueReturnValue;
            }

            public bool IsInventoryNumberUnique(string inventoryNumber)
            {
                WasIsInventoryNumberUniqueInvoked = true;
                IsInventoryNumberUniqueParameter = inventoryNumber;
                return IsInventoryNumberUniqueReturnValue;
            }

            public void RemoveTool(Tool tool, User byUser)
            {
                if (RemoveToolThrowsException)
                {
                    throw new Exception("");
                }

                RemovedTool = tool;
                RemoveToolUserParameter = byUser;
            }

            public Tool UpdateTool(ToolDiff diff)
            {
                if (UpdateToolThrowsException)
                {
                    throw new Exception();
                }

                UpdateToolParameter = diff;
                return diff.NewTool;
            }

            public List<LocationToolAssignmentReferenceLink> LoadLocationToolAssignmentLinksForToolId(ToolId toolId)
            {
                LoadLocationToolAssignmentLinksForToolIdCalled = true;
                return LoadReferencedLocationToolAssignmentsReturn;
            }
        }

        private class ToolGui : IToolGui
        {
            public int ShowToolsCallCount;
            public int ShowToolsErrorCallCount;
            public int ShowCommentForToolCallCount;
            public int ShowCommentForToolErrorCallCount;
            public int ShowPictureForToolCallCount;
            public int ToolErrorCalledCount;
            public int RemoveToolCallCount;
            public List<Tool> Tools { get; set; }
            public string ShownComment { get; set; }
            public Picture ShownPicture { get; set; }
            public long ShownToolId { get; set; }
            public Tool AddedTool { get; set; }

            public List<ToolModel> ShowModelsWithAtLeastOneToolResult;

            public int ShowModelsWithAtLeastOneToolCallCount;

            public int UpdateToolCallCount { get; set; }

            public bool DiffdialogResult { get; set; }
            public string ShowDiffDialogComment { get; set; }
            public Tool UpdateToolParameter { get; set; }
            public List<LocationToolAssignmentReferenceLink> ShowRemoveToolPreventingReferencesParameter { get; set; }

            public bool ShowEntryAlreadyExistsMessageCalled = false;
            public bool ToolAlreadyExistsCalled = false;

            public void ShowLoadingErrorMessage()
            {
                ShowToolsErrorCallCount++;
            }

            public void ShowTools(List<Tool> tools)
            {
                Tools = tools;
                ShowToolsCallCount++;
            }

            public void AddTool(Tool newTool)
            {
                AddedTool = newTool;
            }

            public void ShowCommentForTool(Tool tool, string comment)
            {
                ShowCommentForToolCallCount++;
                ShownComment = comment;
            }

            public void ShowCommentForToolError()
            {
                ShowCommentForToolErrorCallCount++;
            }

            public void ShowPictureForTool(long toolId, Picture picture)
            {
                ShowPictureForToolCallCount++;
                ShownPicture = picture;
                ShownToolId = toolId;
            }

            public void ShowToolErrorMessage()
            {
                ToolErrorCalledCount++;
            }

            public void ShowModelsWithAtLeastOneTool(List<ToolModel> models)
            {
                ShowModelsWithAtLeastOneToolCallCount++;
                ShowModelsWithAtLeastOneToolResult = models;
            }

            public void ShowRemoveToolErrorMessage()
            {
                ShowToolsErrorCallCount++;
            }

            public void RemoveTool(Tool tool)
            {
                RemoveToolCallCount++;
            }

            public void UpdateTool(Tool updateTool)
            {
                UpdateToolParameter = updateTool;
                UpdateToolCallCount++;
            }

            public void ShowEntryAlreadyExistsMessage(Tool diffNewTool)
            {
                ShowEntryAlreadyExistsMessageCalled = true;
            }

            public void ToolAlreadyExists()
            {
                ToolAlreadyExistsCalled = true;
            }

            public void ShowRemoveToolPreventingReferences(List<LocationToolAssignmentReferenceLink> references)
            {
                ShowRemoveToolPreventingReferencesParameter = references;
            }
        }

        private class ToolModelPictureData : IToolModelPictureData
        {
            public int LoadPictureForToolModelCallCount;
            public Picture PictureToLoad { get; set; }

            public Picture LoadPictureForToolModel(long toolModelId)
            {
                LoadPictureForToolModelCallCount++;
                return PictureToLoad;
            }
        }

        ToolData _data;
        ToolGui _gui;
        ToolModelPictureData _toolModelPictureData;
        ToolUseCase _useCase;
        UserGetterMock _userGetter;
        Tool _testTool;

        [SetUp]
        public void ToolUseCaseSetUp()
        {
            _data = new ToolData();
            _gui = new ToolGui();
            _toolModelPictureData = new ToolModelPictureData();
            _userGetter = new UserGetterMock();
            _useCase = new ToolUseCase(_data, _gui, _toolModelPictureData, null, null, null, null, null, _userGetter, new NotificationManagerMock());

            _testTool = CreateParametrizedTool(6531, "jbug524", "gfhjk5865");
        }

        [TestCase("Test Kommentar")]
        [TestCase("Zweites Test Kommentar")]
        public void LoadCommentForToolTest(string testComment)
        {
            _data.CommentToLoad = testComment;
            Tool tool = CreateParametrizedTool(4, "123", "456");
            _useCase.LoadCommentForTool(tool);
            Assert.AreEqual(testComment, _gui.ShownComment);
        }

        [Test]
        public void LoadPictureForToolTest()
        {
            var tool = CreateParametrizedTool(1, "123", "456");
            Picture picture = new Picture();
            _data.PictureToLoad = picture;
            _toolModelPictureData.PictureToLoad = null;
            _useCase.LoadPictureForTool(tool);
            Assert.AreEqual(0, _toolModelPictureData.LoadPictureForToolModelCallCount);
            Assert.AreEqual(picture, _gui.ShownPicture);
            Assert.AreEqual(tool.Id.ToLong(), _gui.ShownToolId);
        }

        [Test]
        public void LoadPictureForToolReverseTest()
        {
            var tool = CreateParametrizedTool(1, "123", "456", CreateParameterizedToolModel(1, "Test"));
            Picture picture = new Picture();
            _data.PictureToLoad = null;
            _toolModelPictureData.PictureToLoad = picture;
            _useCase.LoadPictureForTool(tool);
            Assert.AreEqual(1, _toolModelPictureData.LoadPictureForToolModelCallCount);
            Assert.AreEqual(picture, _gui.ShownPicture);
            Assert.AreEqual(tool.Id.ToLong(), _gui.ShownToolId);
        }

        [Test]
        public void LoadPictureForToolNullImage()
        {
            var tool = CreateParametrizedTool(1, "123", "456", CreateParameterizedToolModel(1, "Test"));
            _data.PictureToLoad = null;
            _toolModelPictureData.PictureToLoad = null;
            _useCase.LoadPictureForTool(tool);
            Assert.AreEqual(1, _toolModelPictureData.LoadPictureForToolModelCallCount);
            Assert.IsNull(_gui.ShownPicture);
            Assert.AreEqual(tool.Id.ToLong(), _gui.ShownToolId);
        }

        [TestCase(2)]
        [TestCase(604)]
        public void AddToolTest(int addId)
        {
            _data.AddId = new ToolId(addId);
            _testTool.Id = new ToolId(0);

            _useCase.AddTool(_testTool);
            
            Assert.AreEqual(_testTool, _data.AddedTool);
            Assert.AreEqual(_gui.AddedTool.Id, _data.AddId);
        }

        [Test]
        public void AddToolErrorTest()
        {
            _data.AddToolShowError = true;
            _useCase.AddTool(_testTool);
            Assert.AreEqual(1, _gui.ToolErrorCalledCount);
        }

        [TestCase(5)]
        [TestCase(33)]
        public void AddingToolPassesCurrentUser(int userAddedByUserId)
        {
            _userGetter.NextReturnedUser = CreateUser.IdOnly(userAddedByUserId);
            _useCase.AddTool(_testTool);
             Assert.AreEqual(userAddedByUserId, _data.LastAddToolUser.UserId.ToLong());
        }

        [TestCase("568zughfnmcd.eörtij")]
        [TestCase(" 8zgjvnckt9zuhjf")]
        public void IsSerialNumberUnique(string serialNumber)
        {
            _data.IsSerialNumberUniqueReturnValue = true;

            Assert.IsTrue(_useCase.IsSerialNumberUnique(serialNumber));
            Assert.IsTrue(_data.WasIsSerialNumberUniqueInvoked);
            Assert.AreEqual(serialNumber, _data.IsSerialNumberUniqueParameter);
        }

        [TestCase("4hbfvcn.eörtij")]
        [TestCase(" /98754z1gdsyuikr")]
        public void IsSerialNumberNotUnique(string serialNumber)
        {
            _data.IsSerialNumberUniqueReturnValue = false;

            Assert.IsFalse(_useCase.IsSerialNumberUnique(serialNumber));
            Assert.IsTrue(_data.WasIsSerialNumberUniqueInvoked);
            Assert.AreEqual(serialNumber, _data.IsSerialNumberUniqueParameter);
        }

        [TestCase("568zughfnmcd.eörtij")]
        [TestCase(" 8zgjvnckt9zuhjf")]
        public void IsInventoryNumberUnique(string serialNumber)
        {
            _data.IsInventoryNumberUniqueReturnValue = true;

            Assert.IsTrue(_useCase.IsInventoryNumberUnique(serialNumber));
            Assert.IsTrue(_data.WasIsInventoryNumberUniqueInvoked);
            Assert.AreEqual(serialNumber, _data.IsInventoryNumberUniqueParameter);
        }

        [TestCase("4hbfvcn.eörtij")]
        [TestCase(" /98754z1gdsyuikr")]
        public void IsInventoryNumberNotUnique(string serialNumber)
        {
            _data.IsInventoryNumberUniqueReturnValue = false;

            Assert.IsFalse(_useCase.IsInventoryNumberUnique(serialNumber));
            Assert.IsTrue(_data.WasIsInventoryNumberUniqueInvoked);
            Assert.AreEqual(serialNumber, _data.IsInventoryNumberUniqueParameter);
        }

        [Test]
        public void LoadModelsWithAtLeastOneTool()
        {
            _useCase.LoadModelsWithAtLeastOneTool();
            Assert.AreEqual(1, _data.LoadModelsWithAtLeasOneToolCallCount);
            Assert.AreEqual(1, _gui.ShowModelsWithAtLeastOneToolCallCount);
        }

        [TestCase(15, "Test")]
        public void LoadModelsWithAtLeastOneToolCallsGuiWithCorrectParameters(int id, string description)
        {
            var toolModel = CreateParameterizedToolModel(id, description);
            _data.LoadModelsWithAtLeasOneToolReturnValue = new List<ToolModel> {toolModel};
            _useCase.LoadModelsWithAtLeastOneTool();
            Assert.AreEqual(toolModel.Id, _gui.ShowModelsWithAtLeastOneToolResult[0].Id);
            Assert.AreEqual(toolModel.Description, _gui.ShowModelsWithAtLeastOneToolResult[0].Description);
        }

        [Test]
        public void LoadModelsWithAtLeastOneToolErrorTest()
        {
            _data.LoadModelsWithAtLeasOneToolThrowsExcpetion = true;
            _useCase.LoadModelsWithAtLeastOneTool();
            Assert.IsTrue(_gui.ShowToolsErrorCallCount > 0);
        }

        [Test]
        public void LoadToolsForModelTest()
        {
            _useCase.LoadToolsForModel(CreateParameterizedToolModel(15));
            Assert.AreEqual(1, _gui.ShowToolsCallCount);
        }

        [TestCase(15, "Test", "Test")]
        [TestCase(76, "Test2", "Test2")]
        public void LoadToolsForModelCallsGuiWithCorrectParameters(int id, string serialno, string invno)
        {
            var toolModel = CreateParameterizedToolModel(75);
            var tool = CreateParametrizedTool(id, serialno, invno, toolModel);
            _data.LoadToolsForModelReturnValue = new List<Tool> {tool};
            _useCase.LoadToolsForModel(toolModel);
            Assert.AreEqual(id, _gui.Tools[0].Id.ToLong());
            Assert.AreEqual(serialno, _gui.Tools[0].SerialNumber.ToDefaultString());
            Assert.AreEqual(invno, _gui.Tools[0].InventoryNumber.ToDefaultString());
        }

        [Test]
        public void LoadToolsForModelErrorTest()
        {
            _data.LoadToolsForModelThrowsException = true;
            _useCase.LoadToolsForModel(CreateParameterizedToolModel(15));
            Assert.IsTrue(_gui.ShowToolsErrorCallCount > 0);
        }

        [Test]
        public void RemoveToolTest()
        {
            var tool = CreateParametrizedTool(15);
            _useCase.RemoveTool(tool, _gui);
            Assert.AreEqual(tool, _data.RemovedTool);
        }

        [Test]
        public void RemovingToolThrowsErrorTest()
        {
            _data.RemoveToolThrowsException = true;
            _useCase.RemoveTool(CreateParametrizedTool(21), _gui);
            Assert.IsTrue(_gui.ShowToolsErrorCallCount > 0);
        }

        [Test]
        public void RemovingToolCallsLoadReferencedLocationToolAssignments()
        {
            var tool = CreateTool.Anonymous();
            _useCase.RemoveTool(tool, _gui);
            Assert.IsTrue(_data.LoadLocationToolAssignmentLinksForToolIdCalled);
        }

        [Test]
        public void RemovingToolWithReferencesCallsShowRemoveToolPreventingReferences()
        {
            _data.LoadReferencedLocationToolAssignmentsReturn = new List<LocationToolAssignmentReferenceLink>
            {
                new LocationToolAssignmentReferenceLink(new QstIdentifier(15), new LocationDescription("blub"), new LocationNumber("blub"), "blub", "blub", null, null, null)
            };
            _useCase.RemoveTool(CreateTool.Anonymous(), _gui);
            Assert.IsNotNull(_gui.ShowRemoveToolPreventingReferencesParameter);
        }

        [Test]
        public void RemovingToolWithReferencesCallsShowRemoveToolPreventingReferencesWithCorrectTool()
        {
            var inventoryNumber = "Blub";
            _data.LoadReferencedLocationToolAssignmentsReturn = new List<LocationToolAssignmentReferenceLink>
            {
                new LocationToolAssignmentReferenceLink(new QstIdentifier(15), new LocationDescription("blub"), new LocationNumber("blub"), "blub", "blub",null, null, null)
                {
                    Id = new QstIdentifier(15),
                    ToolInventoryNumber = inventoryNumber
                }
            };
            var tool = CreateTool.WithInventoryNumber(inventoryNumber);
            _useCase.RemoveTool(tool, _gui);
            Assert.AreEqual(1, _gui.ShowRemoveToolPreventingReferencesParameter.Count);
            Assert.AreEqual(tool.InventoryNumber.ToDefaultString(), _gui.ShowRemoveToolPreventingReferencesParameter[0].ToolInventoryNumber);
        }

        [Test]
        [TestCase(15,"Test", 36)]
        [TestCase(99, "Blub", 1)]
        public void RemovingToolCallsDataRemoveToolWithCorrectParamters(long userId, string userName, long toolId)
        {
            var data = new ToolData();
            var userGetter = new UserGetterMock();
            var gui = new ToolGui();
            var useCase = new ToolUseCase(data, gui, null, null, null, null, null, null, userGetter, new NotificationManagerMock());

            var user = CreateUser.Parametrized(userId, userName, null);
            userGetter.NextReturnedUser = user;
            var tool = CreateTool.WithId(toolId);
            useCase.RemoveTool(tool, gui);

            Assert.AreEqual(tool, data.RemovedTool);
            Assert.AreEqual(user, data.RemoveToolUserParameter);
        }

        [TestCase(15, 16)]
        [TestCase(26, 27)]
        [Parallelizable]
        public void UpdateToolCallsDataAccessUpdateToolWithCorrectParameter(long oldToolId, long newToolId)
        {
            var data = new ToolData();
            var gui = new ToolGui();
            var userGetter = new UserGetterMock();
            var useCase = CreateToolUseCase(data, gui, userGetter);
            useCase.UpdateTool(new ToolDiff(CreateUser.Anonymous(), null, CreateParametrizedTool(oldToolId, "Test"),
                CreateParametrizedTool(newToolId, "blub")));

            Assert.AreEqual(oldToolId, data.UpdateToolParameter.OldTool.Id.ToLong());
            Assert.AreEqual(newToolId, data.UpdateToolParameter.NewTool.Id.ToLong());
        }

        [TestCase("blub")]
        [TestCase("bla")]
        [Parallelizable]
        public void UpdateToolCallsGuiUpdateToolWithCorrectParameter(string serialNo)
        {
            var data = new ToolData();
            var gui = new ToolGui();
            var userGetter = new UserGetterMock();
            var useCase = CreateToolUseCase(data, gui, userGetter);
            useCase.UpdateTool(new ToolDiff(CreateUser.Anonymous(), null, CreateParametrizedTool(15, "Test"),
                CreateParametrizedTool(15, serialNo)));
            Assert.AreEqual(serialNo, gui.UpdateToolParameter.SerialNumber.ToDefaultString());
        }

        [Test]
        [Parallelizable]
        public void UpdateToolCallsGuiShowToolErrorMessageOnDataAccessException()
        {
            var data = new ToolData();
            var gui = new ToolGui();
            var userGetter = new UserGetterMock();
            var useCase = CreateToolUseCase(data, gui, userGetter);
            data.UpdateToolThrowsException = true;
            useCase.UpdateTool(new ToolDiff(CreateUser.Anonymous(), null, CreateParametrizedTool(15, "Test"),
                CreateParametrizedTool(15, "blub")));

            Assert.IsTrue(gui.ToolErrorCalledCount > 0);
        }

        [Test]
        [Parallelizable]
        public void UpdateToolCallsGuiToolAlreadyExistsIfInventoryNumberIsNotUnique()
        {
            var data = new ToolData();
            var gui = new ToolGui();
            var userGetter = new UserGetterMock();
            var useCase = CreateToolUseCase(data, gui, userGetter);
            data.IsInventoryNumberUniqueReturnValue = false;

            useCase.UpdateTool(new ToolDiff(CreateUser.Anonymous(), null, CreateParametrizedTool(15, "Test", "Test"),
                CreateParametrizedTool(15, "blub", "blub")));

            Assert.IsTrue(gui.ToolAlreadyExistsCalled);
        }

        [Test]
        public void AddToolWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var useCase = CreateToolUseCase(new ToolData(), new ToolGui(), new UserGetterMock(), notificationManager);
            useCase.AddTool(CreateTool.Anonymous());
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void RemoveToolParentWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var gui = new ToolGui();
            var useCase = CreateToolUseCase(new ToolData(),gui , new UserGetterMock(), notificationManager);
            useCase.RemoveTool(CreateTool.Anonymous(), gui);
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void UpdateToolWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var useCase = CreateToolUseCase(new ToolData(), new ToolGui(), new UserGetterMock(), notificationManager);
            useCase.UpdateTool(new ToolDiff(CreateUser.Anonymous(),CreateTool.WithId(1), CreateTool.WithId(1)));
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void UpdateToolWithoutNotificationDontCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var useCaseGui = new ToolGui();
            var useCase = CreateToolUseCase(new ToolData(), useCaseGui, new UserGetterMock(), notificationManager);
            useCase.UpdateTool(new ToolDiff(CreateUser.Anonymous(), CreateTool.WithId(1), CreateTool.WithId(1)), false);
            Assert.IsFalse(notificationManager.SendSuccessNotificationCalled);
        }

        private ToolUseCase CreateToolUseCase(ToolData toolData, ToolGui toolGui, UserGetterMock userGetterMock, INotificationManager notificationManager = null)
        {
            return new ToolUseCase(toolData, toolGui, null, null, null, null, null, null, userGetterMock, notificationManager ?? new NotificationManagerMock());
        }

        private static Tool CreateParametrizedTool(long toolId, string serialNo = null, string invNo = null,
            ToolModel model = null)
        {
            return new Tool
            {
                Id = new ToolId(toolId), SerialNumber = new ToolSerialNumber(serialNo), InventoryNumber = new ToolInventoryNumber(invNo), ToolModel = model
            };
        }

        private static ToolModel CreateParameterizedToolModel(int modelid, string description = "")
        {
            return new ToolModel {Id = new ToolModelId(modelid), Description = new ToolModelDescription(description)};
        }
    }
}