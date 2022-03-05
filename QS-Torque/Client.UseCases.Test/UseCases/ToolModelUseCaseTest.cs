using Core.Entities;
using Core.UseCases;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.ReferenceLink;
using Core.Entities.ToolTypes;
using TestHelper.Factories;
using TestHelper.Mock;

namespace Core.Test.UseCases
{
    class ToolModelUseCaseTest
    {
        #region Interface implementations
        private class ToolModelComparer : IEqualityComparer<ToolModel>
        {
            public bool Equals(ToolModel x, ToolModel y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(ToolModel obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        private class ToolModelData : IToolModelData
        {
            public bool RaiseLoadToolModelsException { get; set; } = false;
            public bool RaiseLoadPictureForToolModelException { get; set; } = false;
            public bool RaiseAddToolModelException { get; set; } = false;
            public bool RaiseAddToolModelEntryAlreadyExistsException { get; set; } = false;
            public bool RaiseUpdateToolModelException { get; set; } = false;
            public bool RaiseUpdateToolModelAlreadyExistsException { get; set; } = false;
            public List<ToolModel> ToolModels { get; set; } = new List<ToolModel>();
            public Picture Picture { get; set; }
            public User AddToolModelUserParameter;
            public User RemoveToolModelUserParameter;
            public bool RaiseRemoveToolModelsException { get; set; } = false;
            public User UpdateToolModelUser { get; set; }
            public long LoadReferencedToolsParameter { get; set; }
            public List<ToolReferenceLink> LoadReferencedToolsReturnValue { get; set; }

            public ToolModelDiff UpdateToolModelDiff;

            public List<ToolModel> LoadToolModels()
            {
                if(RaiseLoadToolModelsException)
                {
                    throw new Exception();
                }
                else
                {
                    return ToolModels; 
                }
            }
            
            public Picture LoadPictureForToolModel(long toolModelId)
            {
                if (RaiseLoadPictureForToolModelException)
                {
                    throw new Exception();
                }
                else
                {
                    return Picture;
                }
            }
            
            public void RemoveToolModels(List<ToolModel> toolModels, User user)
            {
                if (RaiseRemoveToolModelsException)
                {
                    throw new Exception("error on removetoolmodel, BDE Table toolmodel is now lost");
                }

                RemoveToolModelUserParameter = user;
                ToolModels = ToolModels.Except(toolModels,new ToolModelComparer()).ToList();
                
            }

            public ToolModel AddToolModel(ToolModel toolModel, User user)
            {
                if (RaiseAddToolModelException)
                {
                    throw new Exception();
                }

                AddToolModelUserParameter = user;

                if (RaiseAddToolModelEntryAlreadyExistsException)
                {
                    throw new EntryAlreadyExists("", null);
                }

                toolModel.Id = new ToolModelId(123);
                ToolModels.Add(toolModel);
                return toolModel;
            }

            public ToolModel UpdateToolModel(ToolModelDiff changedToolModel)
            {
                if (RaiseUpdateToolModelException)
                {
                    throw new Exception();
                }

                if (RaiseUpdateToolModelAlreadyExistsException)
                {
                    throw new EntryAlreadyExists("", null);
                }

                UpdateToolModelDiff = changedToolModel;
                UpdateToolModelUser = changedToolModel?.User;
                var toRemove = ToolModels.First(x => x.Id.Equals(changedToolModel.NewToolModel.Id));
                var index = ToolModels.IndexOf(toRemove);
                ToolModels.Remove(toRemove);
                ToolModels.Insert(index, changedToolModel.NewToolModel);
                return changedToolModel.NewToolModel;
            }

            public List<ToolReferenceLink> LoadReferencedTools(long modelId)
            {
                LoadReferencedToolsParameter = modelId;
                return LoadReferencedToolsReturnValue;
            }
        }

        private class ToolModelGui : IToolModelGui
        {
            public int ShowErrorMessageCallCount;
            public int ShowEntryAlreadyExistsMessageCallCount;
            public bool RemoveToolModelsErrorCalled = false;
            public bool ShowCmCmkErrorCalled = false;

            public List<ToolModel> ToolModels { get; set; } = new List<ToolModel>();
            public Picture Picture { get; set; }
            public (double cm, double cmk) ShownCmCmk { get; set; }
            public ToolModelDiff ToolModelDiff { get; set; }
            public bool DiffDialogResult { get; set; }
            public List<ToolReferenceLink> ShowRemoveToolModelPreventingReferencesParameter { get; set; }
            public bool ShowLoadingErrorMessageCalled = false;

            public string ShowDiffDialogComment;

            public void ShowLoadingErrorMessage()
            {
                ShowLoadingErrorMessageCalled = true;
            }

            public void ShowToolModels(List<ToolModel> toolModels)
            {
                ToolModels = toolModels;
            }

            public void SetPictureForToolModel(long toolModelId, Picture picture)
            {
                Picture = picture;
            }
            
            public void RemoveToolModels(List<ToolModel> toolModels)
            {
                ToolModels = ToolModels.Except(toolModels, new ToolModelComparer()).ToList();
            }

            public void ShowCmCmk(double cm, double cmk)
            {
                ShownCmCmk = (cm, cmk);
            }

            public void ShowCmCmkError()
            {
                ShowCmCmkErrorCalled = true;
            }

            public void ShowRemoveToolModelsErrorMessage()
            {
                RemoveToolModelsErrorCalled = true;
            }

            public void AddToolModel(ToolModel toolModel)
            {
                ToolModels.Add(toolModel);
            }

            public void UpdateToolModel(ToolModel updatedToolModel)
            {
                ToolModels.Where(x => x.Id.Equals(updatedToolModel.Id)).ToList().ForEach(x => x.Description = updatedToolModel.Description);
            }

            public void ShowEntryAlreadyExistsMessage(ToolModel toolModel)
            {
                ShowEntryAlreadyExistsMessageCallCount++;
            }

            public void ShowErrorMessage()
            {
                ShowErrorMessageCallCount++;
            }

            public void ShowRemoveToolModelPreventingReferences(List<ToolReferenceLink> references)
            {
                ShowRemoveToolModelPreventingReferencesParameter = references;
            }

            public bool ShowDiffDialog(ToolModelDiff diff)
            {
                ToolModelDiff = diff;
                diff.Comment = new HistoryComment(ShowDiffDialogComment);
                return DiffDialogResult;
            }
        }
        #endregion


        #region Methods
        private ToolModel GetToolModel()
        {
            return new ToolModel()
            {
                Id = new ToolModelId(56),
                Description = new ToolModelDescription("wrepiofbjhvnmk"),
                ModelType = new General(),
                Class = Enums.ToolModelClass.DriverWithoutScale,
                Manufacturer = new Manufacturer() { Id = new ManufacturerId(437890) },
                MinPower = 743890.647389,
                MaxPower = 438294930.40,
                AirPressure = 784309.6789,
                ToolType = new ToolType() { ListId = new HelperTableEntityId(7869514) },
                Weight = 7869541.56,
                BatteryVoltage = 76578.52,
                MaxRotationSpeed = 785453,
                AirConsumption = 8987.43,
                SwitchOff = new SwitchOff() { ListId = new HelperTableEntityId(675490) },
                DriveSize = new DriveSize() { ListId = new HelperTableEntityId(546211) },
                ShutOff = new ShutOff() { ListId = new HelperTableEntityId(20398457) },
                DriveType = new DriveType() { ListId = new HelperTableEntityId(4309857) },
                ConstructionType = new ConstructionType() { ListId = new HelperTableEntityId(321654) },
                Picture = new Picture() { SeqId = 45621 }
            };
        }

        private ToolModel GetUpdatedToolModel()
        {
            return new ToolModel()
            {
                Id = new ToolModelId(56),
                Description = new ToolModelDescription("c,gsdökjgh"),
                ModelType = new ECDriver(),
                Class = Enums.ToolModelClass.WrenchFixSet,
                Manufacturer = new Manufacturer() { Id = new ManufacturerId(453) },
                MinPower = 5645,
                MaxPower = 46564,
                AirPressure = 456,
                ToolType = new ToolType() { ListId = new HelperTableEntityId(53838) },
                Weight = 65686,
                BatteryVoltage = 75,
                MaxRotationSpeed = 7567,
                AirConsumption = 6756,
                SwitchOff = new SwitchOff() { ListId = new HelperTableEntityId(3863) },
                DriveSize = new DriveSize() { ListId = new HelperTableEntityId(3) },
                ShutOff = new ShutOff() { ListId = new HelperTableEntityId(3836) },
                DriveType = new DriveType() { ListId = new HelperTableEntityId(83) },
                ConstructionType = new ConstructionType() { ListId = new HelperTableEntityId(83683) },
                Picture = new Picture() { SeqId = 83653 }
            };
        }
        #endregion

        ToolModelData _data;
        CmCmkDataAccessMock _cmCmkDataAccess;
        ToolModelGui _gui;
        ToolModelUseCase _useCase;
        UserGetterMock _userGetter;

        [SetUp]
        public void ToolModelUseCaseSetUp()
        {
            _data = new ToolModelData();
            _cmCmkDataAccess = new CmCmkDataAccessMock();
            _gui = new ToolModelGui();
            _userGetter = new UserGetterMock();
            _useCase = new ToolModelUseCase(_data, _cmCmkDataAccess, _gui, null, null, null, null, null, null, null, _userGetter, new NotificationManagerMock());
        }

        [Test]
        public void ShowToolModelsTest()
        {
            _data.ToolModels = new List<ToolModel>() { new ToolModel() { Id = new ToolModelId(45), Description = new ToolModelDescription("Tool model 9856") } };

            _useCase.ShowToolModels();
            Assert.AreEqual(_data.ToolModels, _gui.ToolModels);
        }

        [Test]
        public void ShowToolModelsErrorTest()
        {
            _data.RaiseLoadToolModelsException = true;

            _useCase.ShowToolModels();
            Assert.IsTrue(_gui.ShowLoadingErrorMessageCalled);
        }


        [Test]
        public void LoadPictureForToolModelTest()
        {
            var pict = new Picture();
            _data.Picture = pict;

            _useCase.LoadPictureForToolModel(0);
            Assert.AreEqual(_data.Picture, _gui.Picture);
        }

        [Test]
        public void LoadPictureForToolModelErrorTest()
        {
            var pict = new Picture();
            _data.Picture = pict;
            _data.RaiseLoadPictureForToolModelException = true;

            _useCase.LoadPictureForToolModel(0);
            Assert.IsTrue(_gui.ShowLoadingErrorMessageCalled);
        }

        [Test]
        public void AddToolModelTest()
        {
            var toolModel = new ToolModel() { Description = new ToolModelDescription("Test ToolModel Description"), Id = new ToolModelId(0) };

            _useCase.AddToolModel(toolModel);
            Assert.IsTrue(_data.ToolModels.Contains(toolModel));
            Assert.IsTrue(_gui.ToolModels.Contains(toolModel));
            Assert.AreEqual(0, _gui.ShowEntryAlreadyExistsMessageCallCount);
        }

        [Test]
        public void AddToolModelEntryAlreadyExistsTest()
        {
            var toolModel = new ToolModel() { Description = new ToolModelDescription("Test ToolModel Description"), Id = new ToolModelId(0) };
            _data.RaiseAddToolModelEntryAlreadyExistsException = true;

            _useCase.AddToolModel(toolModel);
            
            Assert.IsTrue(_gui.ShowEntryAlreadyExistsMessageCallCount > 0);
        }

        [Test]
        public void AddToolModelErrorTest()
        {
            var toolModel = new ToolModel() { Description = new ToolModelDescription("Test ToolModel Description"), Id = new ToolModelId(0) };
            _data.RaiseAddToolModelException = true;

            _useCase.AddToolModel(toolModel);
            Assert.IsTrue(_gui.ShowErrorMessageCallCount > 0);
        }

        [TestCase(5)]
        [TestCase(165)]
        public void AddToolModelCallsDataAccessWithCorrectUserId(long userId)
        {
            _userGetter.NextReturnedUser = CreateUser.IdOnly(userId);
            _useCase.AddToolModel(new ToolModel{Id = new ToolModelId(5)});
            Assert.AreEqual(userId, _data.AddToolModelUserParameter.UserId.ToLong());
        }

        [Test]
        public void RequestToolModelUpdateWithDifferentIdTest()
        {
            var toolModel = new ToolModel() { Id = new ToolModelId(12) };
            var differentToolModel = new ToolModel() { Id = new ToolModelId(78) };

            _useCase.RequestToolModelUpdate(toolModel, differentToolModel, _gui);

            Assert.IsTrue(_gui.ShowErrorMessageCallCount > 0);
            Assert.IsNull(_gui.ToolModelDiff);
        }

        [Test]
        public void RequestToolModelUpdateFalseTest()
        {
            var oldToolModel = new ToolModel() { Id = new ToolModelId(21), Description = new ToolModelDescription("Old Description") };
            var newToolModel = new ToolModel() { Id = new ToolModelId(21), Description = new ToolModelDescription("New Description") };

            _userGetter.NextReturnedUser = CreateUser.IdOnly(57489);
            _gui.DiffDialogResult = false;
            _useCase.RequestToolModelUpdate(oldToolModel, newToolModel, _gui);
            
            Assert.AreEqual(_gui.ToolModelDiff.NewToolModel.Id, newToolModel.Id);
            Assert.AreEqual(_gui.ToolModelDiff.User.UserId, _userGetter.NextReturnedUser.UserId);
            Assert.AreEqual(_gui.ToolModelDiff.OldToolModel, oldToolModel);
            Assert.AreEqual(_gui.ToolModelDiff.NewToolModel, newToolModel);
        }

        [Test]
        public void RequestToolModelUpdateTrueTest()
        {
            var oldToolModel = new ToolModel() { Id = new ToolModelId(21), Description = new ToolModelDescription("Old Description") };
            var newToolModel = new ToolModel() { Id = new ToolModelId(21), Description = new ToolModelDescription("New Description") };

            _data.ToolModels.Add(oldToolModel);
            _gui.ToolModels.Add(oldToolModel);

            _userGetter.NextReturnedUser = CreateUser.IdOnly(98765414);
            _gui.DiffDialogResult = true;
            _useCase.RequestToolModelUpdate(oldToolModel, newToolModel, _gui);
            
            Assert.AreEqual(_gui.ToolModelDiff.NewToolModel.Id, newToolModel.Id);
            Assert.AreEqual(_gui.ToolModelDiff.User.UserId, _userGetter.NextReturnedUser.UserId);
            Assert.AreEqual(_gui.ToolModelDiff.OldToolModel, oldToolModel);
            Assert.AreEqual(_gui.ToolModelDiff.NewToolModel, newToolModel);
        }

        [Test]
        public void UpdateToolModelTest()
        {
            var toolModel0 = new ToolModel() { Id = new ToolModelId(0), Description = new ToolModelDescription("Test ToolModel Description") };
            var toolModel1 = new ToolModel() { Id = new ToolModelId(1), Description = new ToolModelDescription("Test ToolModel") };
            var toolModel2 = new ToolModel() { Id = new ToolModelId(2), Description = new ToolModelDescription("Commander") };
            var updatedToolModel = new ToolModel() { Id = new ToolModelId(1), Description = new ToolModelDescription("Changed description") };
            
            _data.ToolModels.Add(toolModel0);
            _data.ToolModels.Add(toolModel1);
            _data.ToolModels.Add(toolModel2);
            _gui.ToolModels.Add(toolModel0);
            _gui.ToolModels.Add(toolModel1);
            _gui.ToolModels.Add(toolModel2);

            _useCase.UpdateToolModel(new ToolModelDiff { OldToolModel = toolModel1, NewToolModel = updatedToolModel });
            
            Assert.AreEqual(_data.ToolModels[1].Description, toolModel1.Description);
            Assert.AreEqual(_gui.ToolModels[1].Description, toolModel1.Description);
        }

        [Test]
        public void UpdateToolModelEntryAlreadyExistsTest()
        {
            var toolModel = new ToolModel() { Description = new ToolModelDescription("Test ToolModel Description"), Id = new ToolModelId(0) };
            _data.RaiseUpdateToolModelAlreadyExistsException = true;

            _useCase.UpdateToolModel(new ToolModelDiff() { OldToolModel = toolModel, NewToolModel = toolModel });

            Assert.IsTrue(_gui.ShowEntryAlreadyExistsMessageCallCount > 0);
        }

        [Test]
        public void UpdateToolModelErrorTest()
        {
            var toolModel = new ToolModel() { Description = new ToolModelDescription("Test ToolModel Description"), Id = new ToolModelId(0) };
            _data.RaiseUpdateToolModelException = true;

            _useCase.UpdateToolModel(new ToolModelDiff() { OldToolModel = toolModel, NewToolModel = toolModel });

            Assert.IsTrue(_gui.ShowErrorMessageCallCount > 0);
        }

        [TestCase("TestCommentarEins")]
        [TestCase("TestKommentarZwei")]
        public void RequestToolModelCallsUpdateToolModelWithCorrectHistoryComment(string comment)
        {
            var toolModel = CreateParameterizedToolModel(15);
            _gui.ToolModels.Add(toolModel);
            _data.ToolModels.Add(toolModel);
            _gui.DiffDialogResult = true;
            _gui.ShowDiffDialogComment = comment;
            _useCase.RequestToolModelUpdate(toolModel, toolModel, _gui);
            Assert.AreEqual(comment, _gui.ToolModelDiff.Comment.ToDefaultString());
        }

        private ToolModel CreateParameterizedToolModel(long id, string description = "", Manufacturer manufacturer=null)
        {
            return new ToolModel
            {
                Id = new ToolModelId(id),
                Description = new ToolModelDescription(description),
                Manufacturer = manufacturer,
            };
        }

        [Test]
        public void SaveToolModelClassDiffHasCorrectUser()
        {
            var user = CreateUser.Parametrized(5, "Test", CreateGroup.Anonymous());
            _userGetter.NextReturnedUser = user;
            var toolModel = new ToolModel {Id = new ToolModelId(5), Description = new ToolModelDescription("Baum")};
            _data.ToolModels.Add(toolModel);
            _useCase.UpdateToolModel(new ToolModelDiff(user, null, new ToolModel { Id = new ToolModelId(1) }, toolModel));
            Assert.AreEqual(user, _data.UpdateToolModelUser);
        }

        [Test]
        public void RemoveToolModelTest()
		{
            var data = new ToolModelData() { ToolModels = new List<ToolModel>() };
			var gui = new ToolModelGui() { ToolModels = new List<ToolModel>() };
			var useCase = new ToolModelUseCase(data, null, gui, null, null, null, null, null, null, null, _userGetter, new NotificationManagerMock());

			var manu = CreateAnonymousManufacturer();

			var modeltoremove = new ToolModel()
			{
				Id = new ToolModelId(1),
				Description = new ToolModelDescription("tolles model1"),
				ModelType = new ECDriver(),
				MinPower = 0,
				MaxPower = 20,
				Manufacturer = manu
			};

			data.ToolModels.Add(modeltoremove);

			for (int i = 0; i < 100; ++i)
			{
				data.ToolModels.Add(new ToolModel()
				{
					Id = new ToolModelId(i + 2),
					Description = new ToolModelDescription("tolles model" + (i + 2)),
					ModelType = new ECDriver(),
					MinPower = 0,
					MaxPower = 20,
					Manufacturer = manu
				});
			}

            data.RaiseRemoveToolModelsException = false;
            useCase.RemoveToolModels(new List<ToolModel> { modeltoremove }, _gui);
            Assert.IsFalse(data.ToolModels.Contains(modeltoremove, new ToolModelComparer()));
		}

		[Test]
        public void RemoveToolModelTestError()
        {
            var data = new ToolModelData() { ToolModels = new List<ToolModel>() };
            var gui = new ToolModelGui() { ToolModels = new List<ToolModel>() };
            var useCase = new ToolModelUseCase(data, null, gui, null, null, null, null, null, null, null, _userGetter, new NotificationManagerMock());

            var manu = CreateAnonymousManufacturer();

            var modeltoremove = new ToolModel()
            {
                Id = new ToolModelId(1),
                Description = new ToolModelDescription("tolles model1"),
                ModelType = new ECDriver(),
                MinPower = 0,
                MaxPower = 20,
                Manufacturer = manu
            };

            data.ToolModels.Add(modeltoremove);

            for (int i = 0; i < 100; ++i)
            {
                data.ToolModels.Add(new ToolModel()
                {
                    Id = new ToolModelId(i + 2),
                    Description = new ToolModelDescription("tolles model" + (i + 2)),
                    ModelType = new ECDriver(),
                    MinPower = 0,
                    MaxPower = 20,
                    Manufacturer = manu
                });
            }
            
            data.RaiseRemoveToolModelsException = true;
            useCase.RemoveToolModels(new List<ToolModel> { modeltoremove }, _gui);
            Assert.IsTrue(gui.RemoveToolModelsErrorCalled);
        }

        [TestCase(5)]
        [TestCase(165)]
        public void RemoveToolModelCallsDataAccessWithCorrectUserId(long userId)
        {
            _userGetter.NextReturnedUser = CreateUser.IdOnly(userId);
            _useCase.RemoveToolModels(new List<ToolModel> { new ToolModel{ Id = new ToolModelId(5) }}, _gui);
            Assert.AreEqual(userId, _data.RemoveToolModelUserParameter.UserId.ToLong());
        }

        [Test]
        public void LoadCmCmkTest()
        {
            var cmCmk = (1.67, 1.67);
            _cmCmkDataAccess.CmCmkToLoad = cmCmk;
            _useCase.LoadCmCmk();
            Assert.AreEqual(cmCmk, _gui.ShownCmCmk);
        }

        [Test]
        public void LoadCmCmkErrorTest()
        {
            var cmCmk = (1.67, 1.67);
            _cmCmkDataAccess.CmCmkToLoad = cmCmk;
            _cmCmkDataAccess.LoadCmCmkThrowsException = true;
            _useCase.LoadCmCmk();
            Assert.IsTrue(_gui.ShowCmCmkErrorCalled);
        }

        [Test]
        public void IsDescriptionUnipueWithPreLoadedCacheTest()
        {
            _data.ToolModels.Add(new ToolModel() { Description = new ToolModelDescription("a") });
            _data.ToolModels.Add(new ToolModel() { Description = new ToolModelDescription("b") });
            _data.ToolModels.Add(new ToolModel() { Description = new ToolModelDescription("c") });

            // Preload cache for tool model descriptions
            _useCase.ShowToolModels();
            Assert.IsTrue(_useCase.IsToolModelDesciptionUnique("d"));
        }

        [Test]
        public void IsDescriptionNotUnipueWithPreLoadedCacheTest()
        {
            _data.ToolModels.Add(new ToolModel() { Description = new ToolModelDescription("a") });
            _data.ToolModels.Add(new ToolModel() { Description = new ToolModelDescription("b") });
            _data.ToolModels.Add(new ToolModel() { Description = new ToolModelDescription("c") });

            // Preload cache for tool model descriptions
            _useCase.ShowToolModels();
            Assert.IsFalse(_useCase.IsToolModelDesciptionUnique("b"));
        }

        [Test]
        public void IsDescriptionUnipueWithoutPreLoadedCacheTest()
        {
            _data.ToolModels.Add(new ToolModel() { Description = new ToolModelDescription("a") });
            _data.ToolModels.Add(new ToolModel() { Description = new ToolModelDescription("b") });
            _data.ToolModels.Add(new ToolModel() { Description = new ToolModelDescription("c") });
            
            Assert.IsTrue(_useCase.IsToolModelDesciptionUnique("d"));
        }

        [Test]
        public void IsDescriptionUnipueNotWithoutPreLoadedCacheTest()
        {
            _data.ToolModels.Add(new ToolModel() { Description = new ToolModelDescription("a") });
            _data.ToolModels.Add(new ToolModel() { Description = new ToolModelDescription("b") });
            _data.ToolModels.Add(new ToolModel() { Description = new ToolModelDescription("c") });

            Assert.IsFalse(_useCase.IsToolModelDesciptionUnique("b"));
        }

        [Test]
        public void IsDescriptionNotUniqueAfterAdd()
        {
            // Preload DescriptionCache
            _useCase.ShowToolModels();
            _useCase.AddToolModel(new ToolModel() { Description = new ToolModelDescription("a") });
            Assert.IsFalse(_useCase.IsToolModelDesciptionUnique("a"));
        }

        [Test]
        public void IsDescriptionUniqueAfterRemove()
        {
            _data.ToolModels.Add(new ToolModel() { Id = new ToolModelId(0), Description = new ToolModelDescription("a") });
            _data.ToolModels.Add(new ToolModel() { Id = new ToolModelId(1), Description = new ToolModelDescription("b") });
            _data.ToolModels.Add(new ToolModel() { Id = new ToolModelId(2), Description = new ToolModelDescription("c") });

            // Preload DescriptionCache
            _useCase.ShowToolModels();
            _useCase.RemoveToolModels(new List<ToolModel>() { new ToolModel() { Id = new ToolModelId(0), Description = new ToolModelDescription("a") } }, _gui);
            Assert.IsTrue(_useCase.IsToolModelDesciptionUnique("a"));
        }

        [Test]
        [TestCase(15)]
        [TestCase(26)]
        public void RemoveToolModelWithReferencesCallsDataLoadReferencedTools(long modelId)
        {
            var toolModel = CreateParameterizedToolModel(modelId, "blub");
            _useCase.RemoveToolModels(new List<ToolModel>{toolModel}, _gui);
            Assert.AreEqual(_data.LoadReferencedToolsParameter, toolModel.Id.ToLong());
        }

        [Test]
        [TestCase(15)]
        [TestCase(26)]
        public void RemoveToolModelWithReferencesCallsGuiShowRemoveToolModelPreventingReferences(long toolId)
        {
            var tool = CreateToolByIdOnly(toolId);
            var toolModel = CreateParameterizedToolModel(15);
            _data.LoadReferencedToolsReturnValue = new List<ToolReferenceLink>{tool};
            _useCase.RemoveToolModels(new List<ToolModel>{toolModel}, _gui);
            Assert.AreEqual(1, _gui.ShowRemoveToolModelPreventingReferencesParameter.Count); 
            Assert.AreEqual(toolId, _gui.ShowRemoveToolModelPreventingReferencesParameter[0].Id.ToLong());
        }

        [Test]
        public void AddToolModelWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var useCase = CreateToolModelUseCaseParametrized(null, null, null, null, notificationManager);
            useCase.AddToolModel(CreateParameterizedToolModel(1));
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void RemoveToolModelWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var gui = new ToolModelGui();
            var useCase = CreateToolModelUseCaseParametrized(null, null, gui, null, notificationManager);
            useCase.RemoveToolModels(new List<ToolModel>(){CreateToolModel.Anonymous()}, gui);
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        [Test]
        public void UpdateToolModelWithoutErrorCallsSendSuccessNotification()
        {
            var notificationManager = new NotificationManagerMock();
            var gui = new ToolModelGui();
            var data = new ToolModelData();

            var toolModel = CreateToolModel.WithId(1);
            data.ToolModels.Add(toolModel);

            var useCase = CreateToolModelUseCaseParametrized(data, null, gui, null, notificationManager);
            useCase.UpdateToolModel(new ToolModelDiff() { OldToolModel = toolModel, NewToolModel = toolModel });
            Assert.IsTrue(notificationManager.SendSuccessNotificationCalled);
        }

        private ToolModelUseCase CreateToolModelUseCaseParametrized(ToolModelData toolModelData, CmCmkDataAccessMock cmCmkData, ToolModelGui toolModelGui, UserGetterMock userGetterMock, NotificationManagerMock notificationManager)
        {          
            return new ToolModelUseCase(toolModelData ?? new ToolModelData(), cmCmkData ?? new CmCmkDataAccessMock(), toolModelGui ?? new ToolModelGui(), null, null, null, null, null, null, null, userGetterMock ?? new UserGetterMock(), notificationManager ?? new NotificationManagerMock());
        }

        private ToolReferenceLink CreateToolByIdOnly(long id)
        {
            return new ToolReferenceLink(new QstIdentifier(id), "test","test",new MockToolDisplayFormatter());
        }

        private static Manufacturer CreateAnonymousManufacturer()
		{
			return new Manufacturer() { Id = new ManufacturerId(1), Name = new ManufacturerName("manu2000") };
		}
	}
}
