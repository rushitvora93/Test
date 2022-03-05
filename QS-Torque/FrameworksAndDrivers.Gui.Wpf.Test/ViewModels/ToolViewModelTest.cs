using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
using ToolModel = Core.Entities.ToolModel;
using TestHelper.Mock;
using System.Windows;
using Core.Entities.ReferenceLink;
using Core.Entities.ToolTypes;
using InterfaceAdapters.Models;
using TestHelper.Checker;
using TestHelper.Factories;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    public class ToolViewModelTest
    {
        private ToolViewModel _viewModel;
        private ToolUseCaseMock _useCase;
        private ToolGuiMock _toolGui;
        private ToolDataMock _toolData;
        private ToolModelPictureDataMock _toolModelPictureData;

        class ToolGuiMock : IToolGui
		{
            public void ShowLoadingErrorMessage()
			{            
            }

            public void ShowTools(List<Tool> toolModels)
			{
            }

            public void ShowCommentForTool(Tool tool, string comment)
            {
            }

            

            public void ShowCommentForToolError()
            {
            }

            public void ShowPictureForTool(long toolId, Picture picture)
            {
            }

            public void AddTool(Tool newTool)
            {
                throw new System.NotImplementedException();
            }
            public void ShowModelsWithAtLeastOneTool(List<ToolModel> models)
            {
                throw new System.NotImplementedException();
            }

            public void ShowRemoveToolErrorMessage()
            {
                throw new System.NotImplementedException();
            }

            public void RemoveTool(Tool tool)
            {
                throw new System.NotImplementedException();
            }

            public void UpdateTool(Tool updateTool)
            {
                throw new System.NotImplementedException();
            }

            public void ShowEntryAlreadyExistsMessage(Tool diffNewTool)
            {
                throw new System.NotImplementedException();
            }

            public void ToolAlreadyExists()
            {
                throw new System.NotImplementedException();
            }

            public void ShowRemoveToolPreventingReferences(List<LocationToolAssignmentReferenceLink> references)
            {
                throw new System.NotImplementedException();
            }

            public void ShowToolErrorMessage()
            {
                throw new System.NotImplementedException();
            }
        }

        class ToolDataMock : IToolData
        {
            public IEnumerable<List<Tool>> LoadTools()
            {
                List<Tool> erg = new List<Tool>()
                {
                    CreateParametrizedTool(1, "123","456"),
                    CreateParametrizedTool(2, "123aßasfdasf","456213123saödf"),
                    CreateParametrizedTool(3, "23ßAFDÖKL","456alskdf"),
                };
                yield return erg;
            }

            public string LoadComment(Tool tool)
            {
                return "Test Kommentar";
            }

            public Picture LoadPictureForTool(Tool tool)
            {
                return null;
            }
            
            public Tool AddTool(Tool newTool, User byUser)
            {
                throw new System.NotImplementedException();
            }

            public List<ToolModel> LoadModelsWithAtLeasOneTool()
            {
                throw new System.NotImplementedException();
            }

            public List<Tool> LoadToolsForModel(ToolModel toolModel)
            {
                return null;
            }

            public bool IsSerialNumberUnique(string serialNumber)
            {
                return true;
            }

            public bool IsInventoryNumberUnique(string inventoryNumber)
            {
                return true;
            }

            public void RemoveTool(Tool tool, User byUser)
            {
                throw new System.NotImplementedException();
            }

            public Tool UpdateTool(ToolDiff diff)
            {
                throw new System.NotImplementedException();
            }

            public List<LocationToolAssignmentReferenceLink> LoadLocationToolAssignmentLinksForToolId(ToolId toolId)
            {
                throw new System.NotImplementedException();
            }
        }


        class ToolModelPictureDataMock : IToolModelPictureData
        {
            public Picture LoadPictureForToolModel(long toolModelId)
            {
                return null;
            }
        }

        [SetUp]
        public void ToolModelViewModelSetUp()
        {
            _toolGui = new ToolGuiMock();
            _toolData = new ToolDataMock();
            _toolModelPictureData = new ToolModelPictureDataMock();
            _useCase = new ToolUseCaseMock(_toolData, _toolGui, _toolModelPictureData, null, null, null, null, null, null, null);
            _viewModel = new ToolViewModel(_useCase, new NullLocalizationWrapper(), null, new StartUpMock());
            _viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
        }
        
        [Test]
        public void ShowToolTest()
        {
			var manu1 = CreateParametrizedManufacturer(1, "Manufacturer1");
			var manu2 = CreateParametrizedManufacturer(2, "Manufacturer2");
            var toolModel1 = CreateParameterizedToolModel(1, null, manu1, new ECDriver());
            var toolModel2 = CreateParameterizedToolModel(2, null, manu2, new ECDriver());
            var tool1 = CreateParametrizedTool(1, "serno1", "invno1", toolModel1);
            var tool2 = CreateParametrizedTool(2, "serno2", "invno2", toolModel2);

            var list = new List<Tool>() {tool1, tool2};

            _viewModel.ShowTools(list);

            Assert.AreEqual(2, _viewModel.AllToolModels.Count);
            Assert.AreEqual(tool1.Id.ToLong(), _viewModel.AllToolModels[0].Id);
            Assert.AreEqual(tool2.Id.ToLong(), _viewModel.AllToolModels[1].Id);
        }

        [Test]
        public void AddToolTest()
        {
            var toolModel = new ToolModel() { Id = new ToolModelId(87412) };
            var tool = CreateParametrizedTool(987, model:toolModel);

            _viewModel.AddTool(tool);

            AsyncCallCheckerNoAssuredTimeout.OnCallCheck(_useCase.LoadToolsForModelCalled.Task, 0, 
                () => Assert.AreEqual(toolModel, _useCase.LoadToolsForModelParameterToolModel));
        }

        [Test]
        public void AddToolNullTest()
        {
            _viewModel.AddTool(null);

            Assert.AreEqual(0, _viewModel.AllToolModels.Count);
        }

        [Test]
        public void RemoveToolRemovesToolFromCollection()
        {
            var tool = CreateParametrizedTool(15);
            var toolModel = new InterfaceAdapters.Models.ToolModel(tool, new NullLocalizationWrapper());
            _viewModel.AllToolModels.Add(toolModel);
            Assert.AreEqual(1, _viewModel.AllToolModels.Count);
            _viewModel.RemoveTool(tool);
            Assert.AreEqual(0, _viewModel.AllToolModels.Count);
        }

        [Test]
        public void InvokeRemoveToolExecuteCallsRemoveToolWithResetedParameters()
        {
            var useCase = new ToolUseCaseMock(new ToolDataMock(), new ToolGuiMock(), new ToolModelPictureDataMock(), null, null, null, null, null, null, null);
            var viewModel = new ToolViewModel(useCase, new NullLocalizationWrapper(), null, new StartUpMock());
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            var tool = CreateTool.Anonymous();
            var toolModel = InterfaceAdapters.Models.ToolModel.GetModelFor(tool, new NullLocalizationWrapper());

            viewModel.SelectedTool = toolModel.CopyDeep();
            viewModel.SelectedTool.InventoryNumber = "345345";
            viewModel.SelectedTool.SerialNumber = "78435743857835";
            viewModel.SelectedTool.Accessory = "J";

            viewModel.MessageBoxRequest += (sender, args) => args.ResultAction?.Invoke(MessageBoxResult.Yes);
            viewModel.RemoveToolsCommand.Invoke(null);

            Assert.IsTrue(useCase.RemoveToolParameter.EqualsByContent(toolModel.Entity));
        }

        [Test]
        public void SelectionAfterAddTest()
        {
            bool wasSelectionRequestInvoked = false;
            long selectionRequestParameterId = -1;
            _viewModel.SelectionRequest += (s, e) =>
            {
                wasSelectionRequestInvoked = true;
                selectionRequestParameterId = e.Id;
            };

            var toolModel = new ToolModel() { Id = new ToolModelId(87412) };
            var tool = CreateParametrizedTool(987, model: toolModel);

            _viewModel.AddTool(tool);

            AsyncCallCheckerNoAssuredTimeout.OnCallCheck(_useCase.LoadToolsForModelCalled.Task, 0, 
                () => Assert.AreEqual(toolModel, _useCase.LoadToolsForModelParameterToolModel));

            _viewModel.ShowTools(new List<Tool>() { tool });

            Assert.IsTrue(wasSelectionRequestInvoked);
            Assert.AreEqual(tool.Id.ToLong(), selectionRequestParameterId);
        }
        
        [Test]
        public void ShowErrorMessageBoxTest()
        {
            var requestInvoked = false;
            _viewModel.MessageBoxRequest += (s, e) => requestInvoked = true;
            _viewModel.ShowLoadingErrorMessage();

            Assert.True(requestInvoked);
        }

        [Test]
        public void AddToolModelTest()
        {
            var toolModelToAdd = CreateParameterizedToolModel(5, "Test");
            _viewModel.AddToolModel(toolModelToAdd);
            Assert.AreEqual(toolModelToAdd.Id.ToLong(),(_viewModel.ToolModelModelsCollectionView[0] as ToolModelModel).Id);
        }

        [Test]
        public void RemoveToolModelTest()
        {
            var toolModelToRemove = CreateParameterizedToolModel(4, "ToolModelToRemove");
            var toolModelList = new List<ToolModel>
            {
                CreateParameterizedToolModel(1, "First"),
                CreateParameterizedToolModel(2, "Second"),
                CreateParameterizedToolModel(3, "Third"),
                toolModelToRemove
            };
            _viewModel.ShowToolModels(toolModelList);
            _viewModel.RemoveToolModels(new List<ToolModel>{toolModelToRemove});
            Assert.AreEqual(3, _viewModel.ToolModelModelsCollectionView.Count);
            Assert.AreEqual(1,(_viewModel.ToolModelModelsCollectionView[0] as ToolModelModel).Id);
            Assert.AreEqual(2,(_viewModel.ToolModelModelsCollectionView[1] as ToolModelModel).Id);
            Assert.AreEqual(3,(_viewModel.ToolModelModelsCollectionView[2] as ToolModelModel).Id);
        }

        [TestCase("TextNachUpdate")]
        [TestCase("AndererTextNachUpdate")]
        public void UpdateToolModelTest(string updatedDescription)
        {
            var toolModelList = new List<ToolModel>
            {
                CreateParameterizedToolModel(1, "First"),
                CreateParameterizedToolModel(2, "Second"),
                CreateParameterizedToolModel(3, "Third"),
                CreateParameterizedToolModel(4, "Text vor update"),
            };
            _viewModel.ShowToolModels(toolModelList);
            _viewModel.UpdateToolModel(
            CreateParameterizedToolModel(4, updatedDescription));
            Assert.AreEqual(toolModelList[0].Description.ToDefaultString(),(_viewModel.ToolModelModelsCollectionView[0] as ToolModelModel).Description);
            Assert.AreEqual(toolModelList[1].Description.ToDefaultString(), (_viewModel.ToolModelModelsCollectionView[1] as ToolModelModel).Description);
            Assert.AreEqual(toolModelList[2].Description.ToDefaultString(), (_viewModel.ToolModelModelsCollectionView[2] as ToolModelModel).Description);
            Assert.AreEqual(updatedDescription, (_viewModel.ToolModelModelsCollectionView[3] as ToolModelModel).Description);
        }

        #region ConfigurableField

        private static ConfigurableField[] configurebleFields = new ConfigurableField[]
        {
			CreateParametrizedConfigurableField(15, "test"),
			CreateParametrizedConfigurableField(98, "Hans")
        };
        [Test, TestCaseSource(nameof(configurebleFields))]
        public void AddConfigurableFieldTest(ConfigurableField field)
        {
            _viewModel.Add(field);
            Assert.AreEqual(field.ListId.ToLong(), (_viewModel.ConfigurableFieldsCollectionView[0] as HelperTableItemModel<ConfigurableField, string>).ListId);
        }

        private static List<List<ConfigurableField>> showConfigurableFieldsTestSourceList = new List<List<ConfigurableField>>
        {
            new List<ConfigurableField>
            {
				CreateParametrizedConfigurableField(1, "Test"),
				CreateParametrizedConfigurableField(2, "Baum"),
				CreateParametrizedConfigurableField(3, "Buch")
            },
            new List<ConfigurableField>
            {
				CreateParametrizedConfigurableField(25, "Hans"),
				CreateParametrizedConfigurableField(48, "Peter"),
				CreateParametrizedConfigurableField(98, "jorg")
            }
        };
        [Test, TestCaseSource(nameof(showConfigurableFieldsTestSourceList))]
        public void ShowCostCentersTest(List<ConfigurableField> configurableFields)
        {
            _viewModel.ShowItems(configurableFields);
            Assert.AreEqual((_viewModel.ConfigurableFieldsCollectionView[0] as HelperTableItemModel<ConfigurableField, string>).ListId,
                configurableFields[0].ListId.ToLong());
            Assert.AreEqual((_viewModel.ConfigurableFieldsCollectionView[1] as HelperTableItemModel<ConfigurableField, string>).ListId,
                configurableFields[1].ListId.ToLong());
            Assert.AreEqual((_viewModel.ConfigurableFieldsCollectionView[2] as HelperTableItemModel<ConfigurableField, string>).ListId,
                configurableFields[2].ListId.ToLong());
        }

        [Test, TestCaseSource(nameof(configurebleFields))]
        public void SaveConfigurableField(ConfigurableField configurableField)
        {
            _viewModel.ShowItems(new List<ConfigurableField> { configurableField });
            configurableField.Value = new HelperTableDescription("NewValue");
            _viewModel.Save(configurableField);
			Assert.AreEqual(configurableField.ListId.ToLong(), (_viewModel.ConfigurableFieldsCollectionView[0] as HelperTableItemModel<ConfigurableField, string>).ListId);
			Assert.AreEqual(configurableField.Value.ToDefaultString(), (_viewModel.ConfigurableFieldsCollectionView[0] as HelperTableItemModel<ConfigurableField, string>).Value);
        }

        [Test, TestCaseSource(nameof(configurebleFields))]
        public void SaveConfigurableFieldNotFound(ConfigurableField configurableField)
        {
            _viewModel.ShowItems(new List<ConfigurableField> { configurableField });
            configurableField.ListId = new HelperTableEntityId(-199);
            configurableField.Value = new HelperTableDescription("Test");
            _viewModel.Save(configurableField);
            Assert.AreEqual(configurableField.ListId.ToLong(), (_viewModel.ConfigurableFieldsCollectionView[0] as HelperTableItemModel<ConfigurableField, string>).ListId);
            Assert.AreEqual(configurableField.Value.ToDefaultString(), (_viewModel.ConfigurableFieldsCollectionView[0] as HelperTableItemModel<ConfigurableField, string>).Value);
        }

        [Test, TestCaseSource(nameof(configurebleFields))]
        public void RemoveConfigurableField(ConfigurableField configurableField)
        {
			_viewModel.ShowItems(new List<ConfigurableField> { configurableField, CreateParametrizedConfigurableField(-99, "test") });
            _viewModel.Remove(configurableField);
            Assert.AreEqual(1,_viewModel.ConfigurableFieldsCollectionView.Count);
            Assert.AreNotEqual(configurableField.ListId.ToLong(), (_viewModel.ConfigurableFieldsCollectionView[0] as HelperTableItemModel<ConfigurableField, string>).ListId);
        }

        #endregion

        #region Status

        private static Status[] statuses = new Status[]
        {
			CreateParametrizedStatus(15, "test"),
			CreateParametrizedStatus(98, "Hans")
        };
        [Test, TestCaseSource(nameof(statuses))]
        public void AddStatusTest(Status status)
        {
            _viewModel.Add(status);
            Assert.AreEqual(status.ListId.ToLong(), (_viewModel.ToolStatusCollectionView[0] as HelperTableItemModel<Status, string>).ListId);
        }

        private static List<List<Status>> showStatusesTestSourceList = new List<List<Status>>
        {
            new List<Status>
            {
				CreateParametrizedStatus(1, "Test"),
				CreateParametrizedStatus(2, "Baum"),
				CreateParametrizedStatus(3, "Buch")
            },
            new List<Status>
            {
				CreateParametrizedStatus(25, "Hans"),
				CreateParametrizedStatus(48, "Peter"),
				CreateParametrizedStatus(98, "jorg")
            }
        };
        [Test, TestCaseSource(nameof(showStatusesTestSourceList))]
        public void ShowStatusesTest(List<Status> statuses)
        {
            _viewModel.ShowItems(statuses);
            Assert.AreEqual(statuses[0].ListId.ToLong(),(_viewModel.ToolStatusCollectionView[0] as HelperTableItemModel<Status, string>).ListId);
            Assert.AreEqual(statuses[1].ListId.ToLong(),(_viewModel.ToolStatusCollectionView[1] as HelperTableItemModel<Status, string>).ListId);
            Assert.AreEqual(statuses[2].ListId.ToLong(),(_viewModel.ToolStatusCollectionView[2] as HelperTableItemModel<Status, string>).ListId);
        }

        [Test, TestCaseSource(nameof(statuses))]
        public void SaveStatus(Status status)
        {
            _viewModel.ShowItems(new List<Status> { status });
            status.Value = new StatusDescription("NewValue");
            _viewModel.Save(status);
            Assert.AreEqual(status.ListId.ToLong(),(_viewModel.ToolStatusCollectionView[0] as HelperTableItemModel<Status, string>).ListId);
            Assert.AreEqual(status.Value.ToDefaultString(),(_viewModel.ToolStatusCollectionView[0] as HelperTableItemModel<Status, string>).Value);
        }

        [Test, TestCaseSource(nameof(statuses))]
        public void SaveStatusNotFound(Status status)
        {
            _viewModel.ShowItems(new List<Status> { status });
            status.ListId = new HelperTableEntityId(-199);
            status.Value = new StatusDescription("Test");
            _viewModel.Save(status);
            Assert.AreEqual(status.ListId.ToLong(),(_viewModel.ToolStatusCollectionView[0] as HelperTableItemModel<Status, string>).ListId);
            Assert.AreEqual(status.Value.ToDefaultString(),(_viewModel.ToolStatusCollectionView[0] as HelperTableItemModel<Status, string>).Value);
        }

        [Test, TestCaseSource(nameof(statuses))]
        public void RemoveStatus(Status status)
		{
			_viewModel.ShowItems(new List<Status> { status, CreateParametrizedStatus(-99, "test") });
			_viewModel.Remove(status);
			Assert.AreEqual(1, _viewModel.ToolStatusCollectionView.Count);
			Assert.AreNotEqual(status.ListId.ToLong(), (_viewModel.ToolStatusCollectionView[0] as HelperTableItemModel<Status, string>).ListId);
		}

		#endregion

		#region CostCenter

		private static CostCenter[] costCenters = new CostCenter[]
        {
			CreateParametrizedCostCenter(15, "test"),
			CreateParametrizedCostCenter(98, "Hans")
        };
        [Test, TestCaseSource(nameof(costCenters))]
        public void AddCostCenterTest(CostCenter costCenter)
        {
            _viewModel.Add(costCenter);
            Assert.AreEqual(costCenter.ListId.ToLong(), (_viewModel.CostCentersCollectionView[0] as HelperTableItemModel<CostCenter, string>).ListId);
        }

        [Test, TestCaseSource(nameof(costCenters))]
        public void SaveCostCenter(CostCenter costCenter)
        {
            _viewModel.ShowItems(new List<CostCenter> { costCenter });
            costCenter.Value = new HelperTableDescription("NewValue");
            _viewModel.Save(costCenter);
			Assert.AreEqual(costCenter.ListId.ToLong(), (_viewModel.CostCentersCollectionView[0] as HelperTableItemModel<CostCenter, string>).ListId);
			Assert.AreEqual(costCenter.Value.ToDefaultString(), (_viewModel.CostCentersCollectionView[0] as HelperTableItemModel<CostCenter, string>).Value);
        }

        [Test, TestCaseSource(nameof(costCenters))]
        public void SaveCostCenterNotFound(CostCenter costCenter)
        {
            _viewModel.ShowItems(new List<CostCenter> { costCenter });
            costCenter.ListId = new HelperTableEntityId(-199);
            costCenter.Value = new HelperTableDescription("Test");
            _viewModel.Save(costCenter);
            Assert.AreEqual(costCenter.ListId.ToLong(),(_viewModel.CostCentersCollectionView[0] as HelperTableItemModel<CostCenter, string>).ListId);
            Assert.AreEqual(costCenter.Value.ToDefaultString(),(_viewModel.CostCentersCollectionView[0] as HelperTableItemModel<CostCenter, string>).Value);
        }

        private static List<List<CostCenter>> showCostCentersTestSourceList = new List<List<CostCenter>>
        {
            new List<CostCenter>
            {
				CreateParametrizedCostCenter(1, "Test"),
				CreateParametrizedCostCenter(2, "Baum"),
				CreateParametrizedCostCenter(3, "Buch")
            },
            new List<CostCenter>
            {
				CreateParametrizedCostCenter(25, "Hans"),
				CreateParametrizedCostCenter(48, "Peter"),
				CreateParametrizedCostCenter(98, "jorg")
            }
        };
        [Test, TestCaseSource(nameof(showCostCentersTestSourceList))]
        public void ShowCostCentersTest(List<CostCenter> costCenters)
        {
            _viewModel.ShowItems(costCenters);
            Assert.AreEqual(costCenters[0].ListId.ToLong(),(_viewModel.CostCentersCollectionView[0] as HelperTableItemModel<CostCenter, string>).ListId);
            Assert.AreEqual(costCenters[1].ListId.ToLong(),(_viewModel.CostCentersCollectionView[1] as HelperTableItemModel<CostCenter, string>).ListId);
            Assert.AreEqual(costCenters[2].ListId.ToLong(),(_viewModel.CostCentersCollectionView[2] as HelperTableItemModel<CostCenter, string>).ListId);
        }

        [Test, TestCaseSource(nameof(costCenters))]
        public void RemoveCostCenter(CostCenter costCenter)
		{
			const int id = -99;
			const string description = "test";
			_viewModel.ShowItems(new List<CostCenter> { costCenter, CreateParametrizedCostCenter(id, description) });
			_viewModel.Remove(costCenter);
			Assert.AreEqual(1, _viewModel.CostCentersCollectionView.Count);
			Assert.AreNotEqual(costCenter.ListId.ToLong(), (_viewModel.CostCentersCollectionView[0] as HelperTableItemModel<CostCenter, string>).ListId);
		}
		#endregion

		#region ToolType

		private static ToolType[] toolTypes = 
        {
            CreateParameterizedToolType(15, "Test"),
            CreateParameterizedToolType(98, "Hans"),
        };
        [Test, TestCaseSource(nameof(toolTypes))]
        public void AddToolTypeTest(ToolType toolType)
        {
            _viewModel.Add(toolType);
            Assert.AreEqual(toolType.ListId.ToLong(), (_viewModel.ToolTypesCollectionView[0] as HelperTableItemModel<ToolType, string>).ListId);
        }

        [Test, TestCaseSource(nameof(toolTypes))]
        public void SaveToolType(ToolType toolType)
		{
			_viewModel.ShowItems(new List<ToolType> { toolType });
			UpdateToolTypeValue(toolType, "NewValue");
			_viewModel.Save(toolType);
			Assert.AreEqual(toolType.ListId.ToLong(), (_viewModel.ToolTypesCollectionView[0] as HelperTableItemModel<ToolType, string>).ListId);
			Assert.AreEqual(toolType.Value.ToDefaultString(), (_viewModel.ToolTypesCollectionView[0] as HelperTableItemModel<ToolType, string>).Value);
		}

		[Test, TestCaseSource(nameof(toolTypes))]
        public void SaveToolTypeNotFound(ToolType toolType)
        {
            _viewModel.ShowItems(new List<ToolType> { toolType });
            toolType.ListId = new HelperTableEntityId(-199);
			UpdateToolTypeValue(toolType, "Test");
            _viewModel.Save(toolType);
            Assert.AreEqual(toolType.ListId.ToLong(), (_viewModel.ToolTypesCollectionView[0] as HelperTableItemModel<ToolType, string>).ListId);
            Assert.AreEqual(toolType.Value.ToDefaultString(), (_viewModel.ToolTypesCollectionView[0] as HelperTableItemModel<ToolType, string>).Value);
        }

        private static List<List<ToolType>> showToolTypesTestSourceList = new List<List<ToolType>>
        {
            new List<ToolType>
            {
                CreateParameterizedToolType(1, "Test"),
                CreateParameterizedToolType(2, "Baum"),
                CreateParameterizedToolType(3, "Buch")
            },
            new List<ToolType>
            {
                CreateParameterizedToolType(25, "Hans"),
                CreateParameterizedToolType(48, "Peter"),
                CreateParameterizedToolType(98, "jorg")
            }
        };
        [Test, TestCaseSource(nameof(showToolTypesTestSourceList))]
        public void ShowToolTypesTest(List<ToolType> toolTypes)
        {
            _viewModel.ShowItems(toolTypes);
            Assert.AreEqual(toolTypes[0].ListId.ToLong(), (_viewModel.ToolTypesCollectionView[0] as HelperTableItemModel<ToolType, string>).ListId);
            Assert.AreEqual(toolTypes[1].ListId.ToLong(), (_viewModel.ToolTypesCollectionView[1] as HelperTableItemModel<ToolType, string>).ListId);
            Assert.AreEqual(toolTypes[2].ListId.ToLong(), (_viewModel.ToolTypesCollectionView[2] as HelperTableItemModel<ToolType, string>).ListId);
        }

        [Test, TestCaseSource(nameof(toolTypes))]
        public void RemoveToolType(ToolType toolType)
        {
            _viewModel.ShowItems(new List<ToolType> { toolType, CreateParameterizedToolType(-99, "test")});
            _viewModel.Remove(toolType);
            Assert.AreEqual(1, _viewModel.ToolTypesCollectionView.Count);
            Assert.AreNotEqual(toolType.ListId.ToLong(), (_viewModel.ToolTypesCollectionView[0] as HelperTableItemModel<ToolType, string>).ListId);
        }

		#endregion

        [TestCase("Test Kommentar")]
        [TestCase("Zweites Test Kommentar")]
        public void ShowCommentForToolTest(string comment)
        {
            var tools = new List<Tool>
            {
                CreateParametrizedTool(1, "789", "987", CreateParameterizedToolModel(1, null, CreateParametrizedManufacturer(1, "Test Hersteller"))),
                CreateParametrizedTool(2, "123", "321", CreateParameterizedToolModel(1, null, CreateParametrizedManufacturer(1, "Test Hersteller")))
            };
            _viewModel.ShowTools(tools);
            _viewModel.ShowCommentForTool(tools[0],comment);
            
            Assert.AreEqual(comment, _viewModel.AllToolModels.First().Comment);
            Assert.AreNotEqual(comment, _viewModel.AllToolModels.Last().Comment);
        }

        [Test]
        public void ShowCommentForToolErrorTest()
        {
            var requestInvoked = false;
            _viewModel.MessageBoxRequest += (s, e) => requestInvoked = true;
            _viewModel.ShowCommentForToolError();
            Assert.True(requestInvoked);
        }

        [TestCase(1.67, 3.65)]
        [TestCase(2.54, 1.36)]
        public void ShowingCmCmkShowsOnlyOneEntry(double cm, double cmk)
        {
            _viewModel.ShowCmCmk(cm, cmk);
            var expected = new List<(double cm, double cmk)> {(cm, cmk)};
            CollectionAssert.AreEqual(
                expected,
                _viewModel.CmCmkTuples.Select(item => (item.Cm, item.Cmk)).ToList());
        }

        [Test]
        public void ShowCmCmkErrorTest()
        {
            _viewModel.MessageBoxRequest += (sender, args) => Assert.Pass();
            _viewModel.ShowCmCmkError();
            Assert.Fail("No Message was shown");
        }

        [Test]
        public void SetPictureForToolTest()
        {
            var picture = new Picture
            {
                FileName = "TestPicture",
                FileType = 0,
                ImageStream = new MemoryStream(),
                NodeId = 15,
                NodeSeqId = 150,
                SeqId = 36
            };
            var tool = CreateParametrizedTool(1, "SerialNo", "InvNo",
                CreateParameterizedToolModel(15, "ModelFlieger", CreateParametrizedManufacturer(15, "Manu")));
            _viewModel.ShowTools(new List<Tool>{tool});
            _viewModel.ShowPictureForTool(1, picture);
            var toolModel = _viewModel.AllToolModels.First();
            Assert.IsTrue(PictureEqualToPictureModel(picture, toolModel.Picture));
        }

        [TestCase("Baum")]
        [TestCase("Buch")]
        public void UpdateToolUpdatesToolInList(string updatedText)
        {
            _viewModel.AllToolModels.Add(new InterfaceAdapters.Models.ToolModel(new Tool(), new NullLocalizationWrapper()) {Id = 15, SerialNumber = "Test", InventoryNumber = "Test"});
            var tool = CreateParametrizedTool(15, updatedText, updatedText);
            _viewModel.UpdateTool(tool);
            Assert.AreEqual(1, _viewModel.AllToolModels.Count);
            Assert.AreEqual(_viewModel.AllToolModels[0].Id, tool.Id.ToLong());
            Assert.AreEqual(_viewModel.AllToolModels[0].SerialNumber, updatedText);
            Assert.AreEqual(_viewModel.AllToolModels[0].InventoryNumber, updatedText);
        }


        [Test]
        [Parallelizable]
        public void SelectSameToolDoesNotInvokePropertyChanged()
        {
            var tuple = CreateToolViewModel();
            var viewModel = tuple.Item1;
            var tool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(15, "Test", "Test"), new NullLocalizationWrapper());
            bool wasPropertyChangedInvoked = false;

            viewModel.SelectedTool = tool;
            viewModel.PropertyChanged += (s, e) => wasPropertyChangedInvoked = true;

            Assert.IsFalse(wasPropertyChangedInvoked);
        }

        [Test]
        [Parallelizable]
        public void SelectSameToolDoesNotInvokeRequestChangesVerification()
        {
            var tuple = CreateToolViewModel();
            var viewModel = tuple.Item1;
            var tool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(15, "Test", "Test"), new NullLocalizationWrapper());
            bool wasRequestChangesVerificationInvoked = false;

            viewModel.SelectedTool = tool;
            viewModel.RequestVerifyChangesView += (s, e) => wasRequestChangesVerificationInvoked = true;

            Assert.IsFalse(wasRequestChangesVerificationInvoked);
        }

        [Test]
        [Parallelizable]
        public void ChangeSelectedToolWithUnequalToolInvokesRequestChangesVerification()
        {
            var tuple = CreateToolViewModel();
            var viewModel = tuple.Item1;
            var tool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(15, "Test", "Test"), new NullLocalizationWrapper());
            var otherTool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(16, "Selected", "Selected"), new NullLocalizationWrapper());
            bool wasRequestChangesVerificationInvoked = false;

            viewModel.RequestVerifyChangesView += (s, e) => wasRequestChangesVerificationInvoked = true;

            viewModel.SelectedTool = tool;
            tool.SerialNumber = "40tzugjfwptgjuvfkd";

            Assert.IsFalse(wasRequestChangesVerificationInvoked);

            viewModel.SelectedTool = otherTool;

            Assert.IsTrue(wasRequestChangesVerificationInvoked);
        }

        [Test]
        [TestCase("Selected")]
        [TestCase("Didup")]
        [Parallelizable]
        public void ChangeSelectedToolCallsMethodOnUseCaseIfVerifyReturnsYes(string serialNo)
        {
            var tuple = CreateToolViewModel();
            var viewModel = tuple.Item1;
            var useCase = tuple.Item2;
            var tool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(15, "Test", "Test"), new NullLocalizationWrapper());
            var otherTool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(16, "Selected", "Selected"), new NullLocalizationWrapper());

            viewModel.RequestVerifyChangesView += (s, e) => e.Result = MessageBoxResult.Yes;

            viewModel.SelectedTool = tool;
            tool.SerialNumber = serialNo;
            viewModel.SelectedTool = otherTool;

            Assert.IsTrue(useCase.WasUpdateToolCalled);
            Assert.AreEqual("Test", useCase.UpdateToolParameter.OldTool.SerialNumber.ToDefaultString());
            Assert.AreEqual(tool.SerialNumber, useCase.UpdateToolParameter.NewTool.SerialNumber.ToDefaultString());
            Assert.AreEqual(otherTool, viewModel.SelectedTool);
        }

        [Test]
        [TestCase("Selected")]
        [TestCase("Didup")]
        [Parallelizable]
        public void ChangeSelectedToolResetsToolIfVerifyReturnsNo(string serialNumber)
        {
            var tuple = CreateToolViewModel();
            var viewModel = tuple.Item1;
            var useCase = tuple.Item2;
            var tool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(15, "Test", "Test"), new NullLocalizationWrapper());
            var otherTool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(16, "Selected", "Selected"), new NullLocalizationWrapper());

            viewModel.RequestVerifyChangesView += (s, e) => e.Result = MessageBoxResult.No;

            viewModel.SelectedTool = tool;
            tool.SerialNumber = serialNumber;
            viewModel.SelectedTool = otherTool;

            Assert.IsFalse(useCase.WasUpdateToolCalled);
            Assert.AreEqual("Test", tool.SerialNumber);
            Assert.AreEqual(otherTool, viewModel.SelectedTool);
        }

        [Test]
        [Parallelizable]
        public void ChangeSelectedToolDoesNotSelectNewToolIfVerifyReturnsCancel()
        {
            var tuple = CreateToolViewModel();
            var viewModel = tuple.Item1;
            var useCase = tuple.Item2;
            var tool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(15, "Test", "Test"), new NullLocalizationWrapper());
            var otherTool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(16, "Selected", "Selected"), new NullLocalizationWrapper());

            viewModel.RequestVerifyChangesView += (s, e) => e.Result = MessageBoxResult.Cancel;

            viewModel.SelectedTool = tool;
            tool.SerialNumber = "40tzugjfwptgjuvfkd";
            viewModel.SelectedTool = otherTool;

            Assert.IsFalse(useCase.WasUpdateToolCalled);
            Assert.AreEqual("40tzugjfwptgjuvfkd", tool.SerialNumber);
            Assert.AreEqual(tool, viewModel.SelectedTool);
        }


        [Test]
        [Parallelizable]
        public void CanCloseInvokesRequestChangesVerificationIfToolHasChanged()
        {
            var tuple = CreateToolViewModel();
            var viewModel = tuple.Item1;
            var tool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(15,"Test","Test"), new NullLocalizationWrapper());
            bool wasRequestChangesVerificationInvoked = false;

            viewModel.SelectedTool = tool;
            tool.SerialNumber = "woeruftheiwopeoghpwl";
            viewModel.RequestVerifyChangesView += (s, e) => wasRequestChangesVerificationInvoked = true;

            viewModel.CanClose();

            Assert.IsTrue(wasRequestChangesVerificationInvoked);
        }

        [Test]
        [Parallelizable]
        public void CanCloseReturnsTrueIfToolHasNotChanged()
        {
            var tuple = CreateToolViewModel();
            var viewModel = tuple.Item1;
            var tool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(15, "Test", "Test"), new NullLocalizationWrapper());

            viewModel.SelectedTool = tool;
            var result = viewModel.CanClose();

            Assert.IsTrue(result);
        }

        [Test]
        [TestCase("Blub")]
        [TestCase("bla")]
        [Parallelizable]
        public void CanCloseCallsMethodOnUseCaseIfVerifyReturnsYes(string serialNo)
        {
            var tuple = CreateToolViewModel();
            var viewModel = tuple.Item1;
            var useCase = tuple.Item2;
            var tool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(15, "Test", "Test"), new NullLocalizationWrapper());

            viewModel.RequestVerifyChangesView += (s, e) => e.Result = MessageBoxResult.Yes;

            viewModel.SelectedTool = tool;
            tool.SerialNumber = serialNo;
            var result = viewModel.CanClose();

            Assert.IsTrue(useCase.WasUpdateToolCalled);
            Assert.AreEqual("Test", useCase.UpdateToolParameter.OldTool.SerialNumber.ToDefaultString());
            Assert.AreEqual(serialNo, useCase.UpdateToolParameter.NewTool.SerialNumber.ToDefaultString());
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase("Blub")]
        [TestCase("bla")]
        [Parallelizable]
        public void CanCloseResetsToolIfVerifyReturnsNo(string serialNo)
        {
            var tuple = CreateToolViewModel();
            var viewModel = tuple.Item1;
            var useCase = tuple.Item2;
            var tool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(15, "Test", "Test"), new NullLocalizationWrapper());

            viewModel.RequestVerifyChangesView += (s, e) => e.Result = MessageBoxResult.No;

            viewModel.SelectedTool = tool;
            tool.SerialNumber = serialNo;
            var result = viewModel.CanClose();

            Assert.IsFalse(useCase.WasUpdateToolCalled);
            Assert.AreEqual("Test", tool.SerialNumber);
            Assert.IsTrue(result);
        }


        [Test]
        public void AddToolCommandWithChangedToolIfCanceledDontChangeSelectedToolOrOpenAssistent()
        {
            var tuple = CreateToolViewModel();
            var viewModel = tuple.Item1;
            var usecase = tuple.Item2;
            var startup = tuple.Item3;
            var tool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(15, "Old Serial Number", "Old Invetory Number"), new NullLocalizationWrapper());

            viewModel.RequestVerifyChangesView += (s, e) => e.Result = MessageBoxResult.Cancel;

            viewModel.SelectedTool = tool;
            tool.SerialNumber = "New Serial Number";
            viewModel.AddToolCommand.Invoke(null);

            Assert.AreEqual(viewModel.SelectedTool, tool);
            Assert.IsFalse(usecase.WasUpdateToolCalled);
            Assert.IsFalse(startup.WasOpenAddToolAssistent);
        }


        [TestCase("Old Serial Number" , "New SerialNumber"), RequiresThread(ApartmentState.STA)]
        [TestCase("546456456", "9999769999")]
        public void AddToolCommandWithChangedToolIfResetedResetToolParameterAndOpenAssistent(string serialNumber, string newSerialNumber)
        {
            var tuple = CreateToolViewModel();
            var viewModel = tuple.Item1;
            var usecase = tuple.Item2;
            var startup = tuple.Item3;
            startup.OpenAddToolAssistentAssistentReturnValue = new View.AssistentView("");
            var tool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(15, serialNumber, "Old Invetory Number"), new NullLocalizationWrapper());

            viewModel.RequestVerifyChangesView += (s, e) => e.Result = MessageBoxResult.No;

            viewModel.SelectedTool = tool;
            tool.SerialNumber = newSerialNumber;
            viewModel.AddToolCommand.Invoke(null);

            Assert.AreEqual(tool.SerialNumber, serialNumber);
            Assert.IsFalse(usecase.WasUpdateToolCalled);
            Assert.IsTrue(startup.WasOpenAddToolAssistent);
        }

        [TestCase("Old Serial Number", "New SerialNumber"), RequiresThread(ApartmentState.STA)]
        [TestCase("546456456", "9999769999")]
        public void AddToolCommandWithChangedToolIfAppliedUpdateToolParameterAndOpenAssistent(string serialNumber, string newSerialNumber)
        {
            var tuple = CreateToolViewModel();
            var viewModel = tuple.Item1;
            var usecase = tuple.Item2;
            var startup = tuple.Item3;
            startup.OpenAddToolAssistentAssistentReturnValue = new View.AssistentView("");
            var tool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(15, serialNumber, "Old Invetory Number"), new NullLocalizationWrapper());

            viewModel.RequestVerifyChangesView += (s, e) => e.Result = MessageBoxResult.Yes;

            viewModel.SelectedTool = tool;
            tool.SerialNumber = newSerialNumber;
            viewModel.AddToolCommand.Invoke(null);

            Assert.AreEqual(tool.SerialNumber, newSerialNumber);
            Assert.IsTrue(usecase.WasUpdateToolCalled);
            Assert.IsTrue(startup.WasOpenAddToolAssistent);
        }



        [Test]
        [TestCase("Blub")]
        [TestCase("bla")]
        public void CanCloseDoesNotSelectNewToolIfVerifyReturnsCancel(string serialNo)
        {
            var tuple = CreateToolViewModel();
            var viewModel = tuple.Item1;
            var useCase = tuple.Item2;
            var tool = new InterfaceAdapters.Models.ToolModel(CreateParametrizedTool(15, "Test", "Test"), new NullLocalizationWrapper());

            viewModel.RequestVerifyChangesView += (s, e) => e.Result = MessageBoxResult.Cancel;

            viewModel.SelectedTool = tool;
            tool.SerialNumber = serialNo;
            var result = viewModel.CanClose();

            Assert.IsFalse(useCase.WasUpdateToolCalled);
            Assert.AreEqual(serialNo, tool.SerialNumber);
            Assert.IsFalse(result);
        }

        [Test]
        public void ShowModelsWithAtLeastOneToolWithPreviousLoadedToolModelsResetsCollection()
        {
            var viewModel = CreateToolViewModel().Item1;

            viewModel.AllToolModelModels.Add(new ToolModelModel(new Core.Entities.ToolModel(), new NullLocalizationWrapper()));
            viewModel.AllToolModelModels.Add(new ToolModelModel(new Core.Entities.ToolModel(), new NullLocalizationWrapper()));
            viewModel.AllToolModelModels.Add(new ToolModelModel(new Core.Entities.ToolModel(), new NullLocalizationWrapper()));
            viewModel.AllToolModelModels.Add(new ToolModelModel(new Core.Entities.ToolModel(), new NullLocalizationWrapper()));

            const long id = 987654324789;

            var toolModel = new Core.Entities.ToolModel() { Id = new ToolModelId(id) };
            viewModel.ShowModelsWithAtLeastOneTool(new List<ToolModel>() { toolModel });

            Assert.AreEqual(1, viewModel.AllToolModelModels.Count);
            Assert.AreEqual(id, viewModel.AllToolModelModels[0].Id);
        }

        [Test]
        public void ShowRemoveToolPreventingReferencesCallsReferencesDialogRequest()
        {
            var viewModel = CreateToolViewModel().Item1;
            viewModel.ReferencesDialogRequest += (sender, list) => { Assert.Pass(); };
            viewModel.ShowRemoveToolPreventingReferences(
                new List<LocationToolAssignmentReferenceLink>{
                    new LocationToolAssignmentReferenceLink(new QstIdentifier(15), new LocationDescription("blub"), new LocationNumber("blub"), "blub", "blub", new MockLocationToolAssignmentDisplayFormatter(), null, null)
                });
            Assert.Fail();
        }

        [Test]
        public void ShowCmCmkWithSelectedToolSaveCanExecuteReturnsFalse()
        {
            (ToolViewModel viewModel, ToolUseCaseMock useCase, StartUpMock startUp) setupTuple = CreateToolViewModel();
            var tool = InterfaceAdapters.Models.ToolModel.GetModelFor(CreateTool.Anonymous(), new NullLocalizationWrapper());
            setupTuple.viewModel.AllToolModels.Add(tool);
            setupTuple.viewModel.SelectedTool = tool;
            setupTuple.viewModel.ShowCmCmk(15, 15);
            Assert.IsFalse(setupTuple.viewModel.SaveToolCommand.CanExecute(null));
        }

        private bool PictureEqualToPictureModel(Picture picture, PictureModel pictureModel)
        {
            return picture.FileName == pictureModel.FileName &&
                   picture.FileType == pictureModel.FileType &&
                   picture.NodeId == pictureModel.NodeId &&
                   picture.NodeSeqId == pictureModel.NodeSeqId &&
                   picture.SeqId == pictureModel.SeqId;
        }

        private static ToolModel CreateParameterizedToolModel(int modelId, string description, Manufacturer manufacturer = null, AbstractToolType toolType = null)
        {
            return new ToolModel {Id = new ToolModelId(modelId), Description = new ToolModelDescription(description), Manufacturer = manufacturer, ModelType = toolType};
        }

        private static Tool CreateParametrizedTool(int toolId, string serialNo = null, string invNo = null, ToolModel model = null)
        {
            return new Tool {Id = new ToolId(toolId), SerialNumber = new ToolSerialNumber(serialNo), InventoryNumber = new ToolInventoryNumber(invNo), ToolModel = model};
        }

        private static Manufacturer CreateParametrizedManufacturer(int manuId, string manuName)
		{
			return new Manufacturer() { Id = new ManufacturerId(manuId), Name = new ManufacturerName(manuName) };
		}

        private static ToolType CreateParameterizedToolType(int id, string description)
        {
            return new ToolType {ListId = new HelperTableEntityId(id), Value = new HelperTableDescription(description)};
        }

		private static Status CreateParametrizedStatus(int id, string description)
		{
			return new Status { ListId = new HelperTableEntityId(id), Value = new StatusDescription(description) };
		}

		private static ConfigurableField CreateParametrizedConfigurableField(int id, string description)
		{
			return new ConfigurableField { ListId = new HelperTableEntityId(id), Value = new HelperTableDescription(description) };
		}

		private static CostCenter CreateParametrizedCostCenter(int id, string description)
		{
			return new CostCenter { ListId = new HelperTableEntityId(id), Value = new HelperTableDescription(description) };
		}

		private static ToolType CreateParametrizedToolType(int id, string description)
		{
			return new ToolType { ListId = new HelperTableEntityId(id), Value = new HelperTableDescription(description) };
		}

		private static void UpdateToolTypeValue(ToolType toolType, string description)
		{
			toolType.Value = new HelperTableDescription(description);
		}

        private static (ToolViewModel, ToolUseCaseMock, StartUpMock) CreateToolViewModel()
        {
            var toolGui = new ToolGuiMock();
            var toolData = new ToolDataMock();
            var startUp = new StartUpMock();
            var toolModelPictureData = new ToolModelPictureDataMock();
            var useCase = new ToolUseCaseMock(toolData, toolGui, toolModelPictureData, null, null, null, null, null, null, null);
            var viewModel = new ToolViewModel(useCase, new NullLocalizationWrapper(), new MockToolDisplayFormatter(), startUp);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            return (viewModel,useCase, startUp);
        }
	}
}
