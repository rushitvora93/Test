using System;
using Core.Entities;
using Core.Enums;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Core.Entities.ReferenceLink;
using Core.Entities.ToolTypes;
using InterfaceAdapters.Models;
using TestHelper.Factories;
using TestHelper.Mock;
using DriveType = Core.Entities.DriveType;
using ToolModel = Core.Entities.ToolModel;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    public class ToolModelViewModelTest
    {
        private ToolModelViewModel _viewModel;
        private MockToolDisplayFormatter _toolDisplayFormatter;

        class ToolModelGuiProxy : IToolModelGui
		{
			public IToolModelGui real;

            public void RemoveToolModels(List<Core.Entities.ToolModel> toolModels)
            {
                real.RemoveToolModels(toolModels);
            }

            public void ShowCmCmk(double cm, double cmk)
            {
                real.ShowCmCmk(cm, cmk);
            }

            public void ShowCmCmkError()
            {
                real.ShowCmCmkError();
            }

            public void SetPictureForToolModel(long toolModelId, Picture picture)
			{
				real.SetPictureForToolModel(toolModelId, picture);
			}

			public void ShowLoadingErrorMessage()
			{
				real.ShowLoadingErrorMessage();
			}

            public void ShowRemoveToolModelsErrorMessage()
            {
                real.ShowRemoveToolModelsErrorMessage();
            }

            public void ShowToolModels(List<Core.Entities.ToolModel> toolModels)
			{
				real.ShowToolModels(toolModels);
			}

            public void AddToolModel(ToolModel toolModel)
            {
                real.AddToolModel(toolModel);
            }

            public void UpdateToolModel(ToolModel toolModel)
            {
                real.UpdateToolModel(toolModel);
            }

            public void ShowEntryAlreadyExistsMessage(ToolModel toolModel)
            {
                real.ShowEntryAlreadyExistsMessage(toolModel);
            }

            public void ShowErrorMessage()
            {
                real.ShowErrorMessage();
            }

            public void ShowRemoveToolModelPreventingReferences(List<ToolReferenceLink> references)
            {
                real.ShowRemoveToolModelPreventingReferences(references);
            }

            public bool ShowDiffDialog(ToolModelDiff diff)
            {
                return real.ShowDiffDialog(diff);
            }
        }

		[SetUp]
        public void ToolModelViewModelSetUp()
        {
			var proxy = new ToolModelGuiProxy();
			// Dear Developers of ToolModelViewModel:
			// This TooModelUseCase should be a Mock.
			// When you have fixed that, then you can delete the Proxy.
            _toolDisplayFormatter = new MockToolDisplayFormatter();
			_viewModel = new ToolModelViewModel(new StartUpMock(), new Core.UseCases.ToolModelUseCase(null, null, proxy, null, null, null, null, null, null, null, null, null), new SaveColumnsUseCase(null,null),  new NullLocalizationWrapper(), _toolDisplayFormatter);
			proxy.real = _viewModel;
			_viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
        }

        [Test]
        public void CreateToolModelViewModelTest()
        {
			var proxy = new ToolModelGuiProxy();
			// Dear Developers of ToolModelViewModel:
			// This TooModelUseCase should be a Mock.
			// When you have fixed that, then you can delete the Proxy.
			var viewModel = new ToolModelViewModel(null, new Core.UseCases.ToolModelUseCase(null, null, proxy, null, null, null, null, null, null, null, null, null),new SaveColumnsUseCase(null, null),  new NullLocalizationWrapper(), null);
			proxy.real = viewModel;
			viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            Assert.IsTrue(viewModel.AllToolModelTypesCollectionView.GetItemAt(0) is PulseDriverModel);
            Assert.IsTrue(viewModel.AllToolModelTypesCollectionView.GetItemAt(1) is PulseDriverShutOffModel);
            Assert.IsTrue(viewModel.AllToolModelTypesCollectionView.GetItemAt(2) is GeneralModel);
            Assert.IsTrue(viewModel.AllToolModelTypesCollectionView.GetItemAt(3) is ECDriverModel);
            Assert.IsTrue(viewModel.AllToolModelTypesCollectionView.GetItemAt(4) is ClickWrenchModel);
            Assert.IsTrue(viewModel.AllToolModelTypesCollectionView.GetItemAt(5) is MDWrenchModel);
            Assert.IsTrue(viewModel.AllToolModelTypesCollectionView.GetItemAt(6) is ProductionWrenchModel);
        }
        
        [Test]
        public void ShowToolModelTest()
		{
			var manu1 = CreateParametrizedManufacturer(1, "Manufacturer 1");
			var manu2 = CreateParametrizedManufacturer(2, "Manufacturer 2");
			var toolModel1 = new Core.Entities.ToolModel() { Id = new ToolModelId(1), Description = new ToolModelDescription("First tool model"), ModelType = new ECDriver(), Manufacturer = manu1 };
			var toolModel2 = new Core.Entities.ToolModel() { Id = new ToolModelId(2), Description = new ToolModelDescription("Second tool model"), ModelType = new ECDriver(), Manufacturer = manu2 };
			var list = new List<Core.Entities.ToolModel>() { toolModel1, toolModel2 };

			_viewModel.ShowManufacturer(new List<Manufacturer>() { manu1, manu2 });
			_viewModel.ShowToolModels(list);

			Assert.AreEqual(2, _viewModel.AllToolModelModels.Count);
            Assert.IsTrue(_viewModel.AllToolModelModels[0].Equals(ToolModelModel.GetModelFor(toolModel1, new NullLocalizationWrapper())));
            Assert.IsTrue(_viewModel.AllToolModelModels[1].Equals(ToolModelModel.GetModelFor(toolModel2, new NullLocalizationWrapper())));
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
        public void RemoveToolModelsTest()
        {
            var useCase = new ToolModelUseCaseMock(null, null, null, null, null, null, null);
            var viewModel = new ToolModelViewModel(new StartUpMock(), useCase, new SaveColumnsUseCase(null, null), new NullLocalizationWrapper(), null);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            var toolModel1 = CreateToolModel.Anonymous();
            var toolModel2 = CreateToolModel.Anonymous();
            var toolModel3 = CreateToolModel.Anonymous();
            var allToolModels = new List<Core.Entities.ToolModel>() { toolModel1, toolModel2, toolModel3 };
            var selectedToolModels = new List<Core.Entities.ToolModel>() { toolModel1, toolModel2 };

            viewModel.ShowToolModels(allToolModels);
            selectedToolModels.ForEach(x => viewModel.SelectedToolModels.Add(ToolModelModel.GetModelFor(x, new NullLocalizationWrapper())));

            viewModel.RemoveToolModels(new List<Core.Entities.ToolModel>() { toolModel1, toolModel2 });

            Assert.AreEqual(1, viewModel.AllToolModelModels.Count);
            Assert.AreSame(viewModel.AllToolModelModels.First().Entity, toolModel3);
            Assert.AreEqual(0, viewModel.SelectedToolModels.Count);
            Assert.IsNull(viewModel.SelectedToolModel);
        }

        [Test]
        public void InvokeRemoveToolModelExecuteCallsRemoveToolModelsWithResetedParameters()
        {
            var useCase = new ToolModelUseCaseMock(null, null, null, null, null, null, null);
            var viewModel = new ToolModelViewModel(new StartUpMock(), useCase, new SaveColumnsUseCase(null, null), new NullLocalizationWrapper(), null);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            
            var toolModel = CreateToolModel.Anonymous();

            viewModel.AddToolModel(toolModel.CopyDeep());
            viewModel.SelectedToolModel.Description = "345345";
            viewModel.SelectedToolModel.AirPressure = 9999;

            viewModel.MessageBoxRequest += (sender, args) => args.ResultAction?.Invoke(MessageBoxResult.Yes);
            viewModel.RemoveToolModelsCommand.Invoke(null);

            Assert.IsTrue(useCase.RemoveToolModelsParameter.First().EqualsByContent(toolModel));
        }

        [Test]
        public void RemoveToolModelsCanExecuteReturnsCorrectValue()
        {
            var useCase = new ToolModelUseCaseMock(null, null, null, null, null, null, null);
            var viewModel = new ToolModelViewModel(new StartUpMock(), useCase, new SaveColumnsUseCase(null, null), new NullLocalizationWrapper(), null);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            Assert.IsFalse(viewModel.RemoveToolModelsCommand.CanExecute(null));
            
            viewModel.AddToolModel(CreateToolModel.Anonymous());

            Assert.IsTrue(viewModel.RemoveToolModelsCommand.CanExecute(null));
        }

        [Test]
        public void ShowRemoveToolModelsErrorMessageTest()
        {
            var requestInvoked = false;
            _viewModel.MessageBoxRequest += (s, e) => requestInvoked = true;
            _viewModel.ShowRemoveToolModelsErrorMessage();

            Assert.True(requestInvoked);
        }

        [Test]
        public void AddToolModelTest()
        {
            var useCase = new ToolModelUseCaseMock(null, null, null, null, null, null, null);
            var viewModel = new ToolModelViewModel(new StartUpMock(), useCase, new SaveColumnsUseCase(null, null), new NullLocalizationWrapper(), null);
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            var selectedToolModels = new List<Core.Entities.ToolModel>() { CreateToolModel.Anonymous(), CreateToolModel.Anonymous() };
            selectedToolModels.ForEach(x => viewModel.SelectedToolModels.Add(ToolModelModel.GetModelFor(x, new NullLocalizationWrapper())));
            var toolModelModel = CreateToolModel.Anonymous();

            viewModel.AddToolModel(toolModelModel);

            Assert.AreEqual(1, viewModel.AllToolModelModels.Count);
            Assert.AreSame(toolModelModel, viewModel.AllToolModelModels.First().Entity);
            Assert.AreEqual(1, viewModel.SelectedToolModels.Count);
            Assert.AreSame(toolModelModel, viewModel.SelectedToolModels.First().Entity);

        }

        [Test]
        public void AddTwoToolModelTest()
        {
            _viewModel.AddToolModel(CreateParametricedToolModel(987654, "ToolModel description"));
            _viewModel.AddToolModel(CreateParametricedToolModel(695412, "thzu54eirokdlfcvj"));

            Assert.AreEqual(2, _viewModel.AllToolModelModels.Count);
            Assert.AreEqual(987654, _viewModel.AllToolModelModels[0].Id);
            Assert.AreEqual("ToolModel description", _viewModel.AllToolModelModels[0].Description);
            Assert.AreEqual(695412, _viewModel.AllToolModelModels[1].Id);
            Assert.AreEqual("thzu54eirokdlfcvj", _viewModel.AllToolModelModels[1].Description);
        }

        [Test]
        public void UpdateToolModelTest()
        {
            bool wasClearShownChangesInvoked = false;
            _viewModel.ClearShownChanges += (s, e) => wasClearShownChangesInvoked = true;

            _viewModel.AddToolModel(CreateParametricedToolModel(1254, "ToolModel description"));
            _viewModel.AddToolModel(CreateParametricedToolModel(695412, "thzu54eirokdlfcvj"));

            _viewModel.UpdateToolModel(CreateParametricedToolModel(695412, "04985rtufjdk"));

            Assert.AreEqual(2, _viewModel.AllToolModelModels.Count);
            Assert.AreEqual(1254, _viewModel.AllToolModelModels[0].Id);
            Assert.AreEqual("ToolModel description", _viewModel.AllToolModelModels[0].Description);
            Assert.AreEqual(695412, _viewModel.AllToolModelModels[1].Id);
            Assert.AreEqual("04985rtufjdk", _viewModel.AllToolModelModels[1].Description);
            Assert.IsTrue(wasClearShownChangesInvoked);
        }

        [Test]
        public void ShowManufacturerTest()
		{
			var list = CreateDefaultManufacturerList();

			_viewModel.ShowManufacturer(list);

			Assert.AreEqual(_viewModel.ManufacturerCollectionView.Count, 3);
			Assert.AreEqual((_viewModel.ManufacturerCollectionView.GetItemAt(0) as ManufacturerModel).Name, "Manu1");
			Assert.AreEqual((_viewModel.ManufacturerCollectionView.GetItemAt(1) as ManufacturerModel).Name, "Manu2");
			Assert.AreEqual((_viewModel.ManufacturerCollectionView.GetItemAt(2) as ManufacturerModel).Name, "Manu3");
		}

		[Test]
        public void AddManufacturerTest()
        {
			_viewModel.AddManufacturer(CreateParametrizedManufacturer(0, "New Manufacturer"));
			_viewModel.AddManufacturer(CreateParametrizedManufacturer(1, "New Manufacturer2"));

            Assert.AreEqual(_viewModel.ManufacturerCollectionView.Count, 2);
            Assert.AreEqual((_viewModel.ManufacturerCollectionView.GetItemAt(0) as ManufacturerModel).Name, "New Manufacturer");
            Assert.AreEqual((_viewModel.ManufacturerCollectionView.GetItemAt(1) as ManufacturerModel).Name, "New Manufacturer2");
        }

        [Test]
        public void SaveManufacturer()
        {
			var changeManufacturer = CreateParametrizedManufacturer(4, "Manufcturer");
			var list = CreateDefaultManufacturerList();
			list.Add(changeManufacturer);
            _viewModel.ShowManufacturer(list);

            changeManufacturer.Name = new ManufacturerName("Changed");
            _viewModel.SaveManufacturer(changeManufacturer);

            Assert.AreEqual(_viewModel.ManufacturerCollectionView.Count, 4);
            Assert.AreEqual((_viewModel.ManufacturerCollectionView.GetItemAt(3) as ManufacturerModel).Name, "Changed");
        }

        [Test]
        public void RemoveManufacturerTest()
        {
			var removeManufacturer = CreateParametrizedManufacturer(4, "Manufcturer");
			var list = new List<Manufacturer>()
			{
				CreateParametrizedManufacturer(1, "Manu1"),
				CreateParametrizedManufacturer(2, "Manu2"),
                removeManufacturer,
				CreateParametrizedManufacturer(3, "Manu3")
			};

            _viewModel.ShowManufacturer(list);
            _viewModel.RemoveManufacturer(removeManufacturer);

            Assert.AreEqual(_viewModel.ManufacturerCollectionView.Count, 3);
            Assert.AreEqual((_viewModel.ManufacturerCollectionView.GetItemAt(0) as ManufacturerModel).Name, "Manu1");
            Assert.AreEqual((_viewModel.ManufacturerCollectionView.GetItemAt(1) as ManufacturerModel).Name, "Manu2");
            Assert.AreEqual((_viewModel.ManufacturerCollectionView.GetItemAt(2) as ManufacturerModel).Name, "Manu3");
        }

        
        [Test]
        public void ShowToolTypeTest()
		{
			var list = CreateDefaultToolTypeList();

			_viewModel.ShowItems(list);

			Assert.AreEqual(_viewModel.ToolTypeCollectionView.Count, 3);
			Assert.AreEqual((_viewModel.ToolTypeCollectionView.GetItemAt(0) as HelperTableItemModel<ToolType, string>).Value, "ToolType1");
			Assert.AreEqual((_viewModel.ToolTypeCollectionView.GetItemAt(1) as HelperTableItemModel<ToolType, string>).Value, "ToolType2");
			Assert.AreEqual((_viewModel.ToolTypeCollectionView.GetItemAt(2) as HelperTableItemModel<ToolType, string>).Value, "ToolType3");
		}

		[Test]
        public void AddToolTypeTest()
        {
			_viewModel.Add(CreateParametrizedToolType(0, "New ToolType"));
			_viewModel.Add(CreateParametrizedToolType(1, "New ToolType2"));

            Assert.AreEqual(_viewModel.ToolTypeCollectionView.Count, 2);
            Assert.AreEqual((_viewModel.ToolTypeCollectionView.GetItemAt(0) as HelperTableItemModel<ToolType, string>).Value, "New ToolType");
            Assert.AreEqual((_viewModel.ToolTypeCollectionView.GetItemAt(1) as HelperTableItemModel<ToolType, string>).Value, "New ToolType2");
        }

        [Test]
        public void SaveToolType()
        {
			var changeToolType = CreateParametrizedToolType(4, "ToolType");
			var list = CreateDefaultToolTypeList();
			list.Add(changeToolType);
            _viewModel.ShowItems(list);

            changeToolType.Value = new HelperTableDescription("Changed");
            _viewModel.Save(changeToolType);

            Assert.AreEqual(_viewModel.ToolTypeCollectionView.Count, 4);
            Assert.AreEqual((_viewModel.ToolTypeCollectionView.GetItemAt(3) as HelperTableItemModel<ToolType, string>).Value, "Changed");
        }

        [Test]
        public void RemoveToolTypeTest()
        {
			var removeToolType = CreateParametrizedToolType(4, "ToolType");
            var list = new List<ToolType>()
			{
				CreateParametrizedToolType(1, "ToolType1"),
				CreateParametrizedToolType(2, "ToolType2"),
                removeToolType,
				CreateParametrizedToolType(3, "ToolType3")
			};

            _viewModel.ShowItems(list);
            _viewModel.Remove(removeToolType);

            Assert.AreEqual(_viewModel.ToolTypeCollectionView.Count, 3);
            Assert.AreEqual((_viewModel.ToolTypeCollectionView.GetItemAt(0) as HelperTableItemModel<ToolType, string>).Value, "ToolType1");
            Assert.AreEqual((_viewModel.ToolTypeCollectionView.GetItemAt(1) as HelperTableItemModel<ToolType, string>).Value, "ToolType2");
            Assert.AreEqual((_viewModel.ToolTypeCollectionView.GetItemAt(2) as HelperTableItemModel<ToolType, string>).Value, "ToolType3");
        }



        [Test]
        public void ShowDriveSizeTest()
		{
			var list = CreateDefaultDriveSizeList();

			_viewModel.ShowItems(list);

			Assert.AreEqual(_viewModel.DriveSizeCollectionView.Count, 3);
			Assert.AreEqual((_viewModel.DriveSizeCollectionView.GetItemAt(0) as HelperTableItemModel<DriveSize, string>).Value, "DriveSize1");
			Assert.AreEqual((_viewModel.DriveSizeCollectionView.GetItemAt(1) as HelperTableItemModel<DriveSize, string>).Value, "DriveSize2");
			Assert.AreEqual((_viewModel.DriveSizeCollectionView.GetItemAt(2) as HelperTableItemModel<DriveSize, string>).Value, "DriveSize3");
		}

		[Test]
        public void AddDriveSizeTest()
        {
			_viewModel.Add(CreateParametrizedDriveSize(0, "New DriveSize"));
			_viewModel.Add(CreateParametrizedDriveSize(1, "New DriveSize2"));

            Assert.AreEqual(_viewModel.DriveSizeCollectionView.Count, 2);
            Assert.AreEqual((_viewModel.DriveSizeCollectionView.GetItemAt(0) as HelperTableItemModel<DriveSize, string>).Value, "New DriveSize");
            Assert.AreEqual((_viewModel.DriveSizeCollectionView.GetItemAt(1) as HelperTableItemModel<DriveSize, string>).Value, "New DriveSize2");
        }

        [Test]
        public void SaveDriveSize()
        {
			var changeDriveSize = CreateParametrizedDriveSize(4, "DriveSize");
			var list = CreateDefaultDriveSizeList();
			list.Add(changeDriveSize);

            _viewModel.ShowItems(list);

            changeDriveSize.Value = new HelperTableDescription("Changed");
            _viewModel.Save(changeDriveSize);

            Assert.AreEqual(_viewModel.DriveSizeCollectionView.Count, 4);
            Assert.AreEqual((_viewModel.DriveSizeCollectionView.GetItemAt(3) as HelperTableItemModel<DriveSize, string>).Value, "Changed");
        }

        [Test]
        public void RemoveDriveSizeTest()
        {
            var removeToolType = CreateParametrizedDriveSize(4, "DriveSize");
			var list = new List<DriveSize>()
			{
				CreateParametrizedDriveSize(1, "DriveSize1"),
				CreateParametrizedDriveSize(2, "DriveSize2"),
                removeToolType,
				CreateParametrizedDriveSize(3, "DriveSize3")
			};

            _viewModel.ShowItems(list);
            _viewModel.Remove(removeToolType);

            Assert.AreEqual(_viewModel.DriveSizeCollectionView.Count, 3);
            Assert.AreEqual((_viewModel.DriveSizeCollectionView.GetItemAt(0) as HelperTableItemModel<DriveSize, string>).Value, "DriveSize1");
            Assert.AreEqual((_viewModel.DriveSizeCollectionView.GetItemAt(1) as HelperTableItemModel<DriveSize, string>).Value, "DriveSize2");
            Assert.AreEqual((_viewModel.DriveSizeCollectionView.GetItemAt(2) as HelperTableItemModel<DriveSize, string>).Value, "DriveSize3");
        }



        [Test]
        public void ShowDriveTypeTest()
		{
			var list = CreateDefaultDriveTypeList();

			_viewModel.ShowItems(list);

			Assert.AreEqual(_viewModel.DriveTypeCollectionView.Count, 3);
			Assert.AreEqual((_viewModel.DriveTypeCollectionView.GetItemAt(0) as HelperTableItemModel<DriveType, string>).Value, "DriveType 1");
			Assert.AreEqual((_viewModel.DriveTypeCollectionView.GetItemAt(1) as HelperTableItemModel<DriveType, string>).Value, "DriveType 2");
			Assert.AreEqual((_viewModel.DriveTypeCollectionView.GetItemAt(2) as HelperTableItemModel<DriveType, string>).Value, "DriveType 3");
		}

		[Test]
        public void AddDriveTypeTest()
        {
			_viewModel.Add(CreateParametrizedDriveType(0, "New DriveType"));
			_viewModel.Add(CreateParametrizedDriveType(1, "New DriveType 2"));

            Assert.AreEqual(_viewModel.DriveTypeCollectionView.Count, 2);
            Assert.AreEqual((_viewModel.DriveTypeCollectionView.GetItemAt(0) as HelperTableItemModel<DriveType, string>).Value, "New DriveType");
            Assert.AreEqual((_viewModel.DriveTypeCollectionView.GetItemAt(1) as HelperTableItemModel<DriveType, string>).Value, "New DriveType 2");
        }

        [Test]
        public void SaveDriveType()
        {
			var changeDriveType = CreateParametrizedDriveType(4, "DriveType");
			var list = CreateDefaultDriveTypeList();
			list.Add(changeDriveType);
            _viewModel.ShowItems(list);

            changeDriveType.Value = new HelperTableDescription("Changed");
            _viewModel.Save(changeDriveType);

            Assert.AreEqual(_viewModel.DriveTypeCollectionView.Count, 4);
            Assert.AreEqual((_viewModel.DriveTypeCollectionView.GetItemAt(3) as HelperTableItemModel<DriveType, string>).Value, "Changed");
        }

        [Test]
        public void RemoveDriveTypeTest()
        {
			var removeDriveType = CreateParametrizedDriveType(4, "DriveType");
            var list = new List<DriveType>()
			{
				CreateParametrizedDriveType(1, "DriveType 1"),
				CreateParametrizedDriveType(2, "DriveType 2"),
                removeDriveType,
				CreateParametrizedDriveType(3, "DriveType 3")
			};

            _viewModel.ShowItems(list);
            _viewModel.Remove(removeDriveType);

            Assert.AreEqual(_viewModel.DriveTypeCollectionView.Count, 3);
            Assert.AreEqual((_viewModel.DriveTypeCollectionView.GetItemAt(0) as HelperTableItemModel<DriveType, string>).Value, "DriveType 1");
            Assert.AreEqual((_viewModel.DriveTypeCollectionView.GetItemAt(1) as HelperTableItemModel<DriveType, string>).Value, "DriveType 2");
            Assert.AreEqual((_viewModel.DriveTypeCollectionView.GetItemAt(2) as HelperTableItemModel<DriveType, string>).Value, "DriveType 3");
        }



        [Test]
        public void ShowSwitchOffTest()
		{
			var list = CreateDefaultSwitchOffList();

			_viewModel.ShowItems(list);

			Assert.AreEqual(_viewModel.SwitchOffCollectionView.Count, 3);
			Assert.AreEqual((_viewModel.SwitchOffCollectionView.GetItemAt(0) as HelperTableItemModel<SwitchOff, string>).Value, "SwitchOff 1");
			Assert.AreEqual((_viewModel.SwitchOffCollectionView.GetItemAt(1) as HelperTableItemModel<SwitchOff, string>).Value, "SwitchOff 2");
			Assert.AreEqual((_viewModel.SwitchOffCollectionView.GetItemAt(2) as HelperTableItemModel<SwitchOff, string>).Value, "SwitchOff 3");
		}

		[Test]
        public void AddSwitchOffTest()
        {
			_viewModel.Add(CreateParametrizedSwitchOff(0, "New SwitchOff"));
			_viewModel.Add(CreateParametrizedSwitchOff(1, "New SwitchOff 2"));

            Assert.AreEqual(_viewModel.SwitchOffCollectionView.Count, 2);
            Assert.AreEqual((_viewModel.SwitchOffCollectionView.GetItemAt(0) as HelperTableItemModel<SwitchOff, string>).Value, "New SwitchOff");
            Assert.AreEqual((_viewModel.SwitchOffCollectionView.GetItemAt(1) as HelperTableItemModel<SwitchOff, string>).Value, "New SwitchOff 2");
        }

        [Test]
        public void SaveSwitchOff()
        {
			var changeSwitchOff = CreateParametrizedSwitchOff(4, "SwitchOff");
			var list = CreateDefaultSwitchOffList();
			list.Add(changeSwitchOff);

            _viewModel.ShowItems(list);

            changeSwitchOff.Value = new HelperTableDescription("Changed");
            _viewModel.Save(changeSwitchOff);

            Assert.AreEqual(_viewModel.SwitchOffCollectionView.Count, 4);
            Assert.AreEqual((_viewModel.SwitchOffCollectionView.GetItemAt(3) as HelperTableItemModel<SwitchOff, string>).Value, "Changed");
        }

        [Test]
        public void RemoveSwitchOffTest()
        {
			var removeSwitchOff = CreateParametrizedSwitchOff(4, "SwitchOff");
            var list = new List<SwitchOff>()
			{
				CreateParametrizedSwitchOff(1, "SwitchOff 1"),
				CreateParametrizedSwitchOff(2, "SwitchOff 2"),
                removeSwitchOff,
				CreateParametrizedSwitchOff(3, "SwitchOff 3")
			};

            _viewModel.ShowItems(list);
            _viewModel.Remove(removeSwitchOff);

            Assert.AreEqual(_viewModel.SwitchOffCollectionView.Count, 3);
            Assert.AreEqual((_viewModel.SwitchOffCollectionView.GetItemAt(0) as HelperTableItemModel<SwitchOff, string>).Value, "SwitchOff 1");
            Assert.AreEqual((_viewModel.SwitchOffCollectionView.GetItemAt(1) as HelperTableItemModel<SwitchOff, string>).Value, "SwitchOff 2");
            Assert.AreEqual((_viewModel.SwitchOffCollectionView.GetItemAt(2) as HelperTableItemModel<SwitchOff, string>).Value, "SwitchOff 3");
        }



        [Test]
        public void ShowShutOffTest()
		{
			var list = CreateDefaultShutOffList();

			_viewModel.ShowItems(list);

			Assert.AreEqual(_viewModel.ShutOffCollectionView.Count, 3);
			Assert.AreEqual((_viewModel.ShutOffCollectionView.GetItemAt(0) as HelperTableItemModel<ShutOff, string>).Value, "ShutOff 1");
			Assert.AreEqual((_viewModel.ShutOffCollectionView.GetItemAt(1) as HelperTableItemModel<ShutOff, string>).Value, "ShutOff 2");
			Assert.AreEqual((_viewModel.ShutOffCollectionView.GetItemAt(2) as HelperTableItemModel<ShutOff, string>).Value, "ShutOff 3");
		}

		[Test]
        public void AddShutOffTest()
        {
			_viewModel.Add(CreateParametrizedShutOff(0, "New ShutOff"));
			_viewModel.Add(CreateParametrizedShutOff(1, "New ShutOff 2"));

            Assert.AreEqual(_viewModel.ShutOffCollectionView.Count, 2);
            Assert.AreEqual((_viewModel.ShutOffCollectionView.GetItemAt(0) as HelperTableItemModel<ShutOff, string>).Value, "New ShutOff");
            Assert.AreEqual((_viewModel.ShutOffCollectionView.GetItemAt(1) as HelperTableItemModel<ShutOff, string>).Value, "New ShutOff 2");
        }

        [Test]
        public void SaveShutOff()
        {
			var changeShutOff = CreateParametrizedShutOff(4, "ShutOff");
			var list = CreateDefaultShutOffList();
			list.Add(changeShutOff);

            _viewModel.ShowItems(list);

            changeShutOff.Value = new HelperTableDescription("Changed");
            _viewModel.Save(changeShutOff);

            Assert.AreEqual(_viewModel.ShutOffCollectionView.Count, 4);
            Assert.AreEqual((_viewModel.ShutOffCollectionView.GetItemAt(3) as HelperTableItemModel<ShutOff, string>).Value, "Changed");
        }

        [Test]
        public void RemoveShutOffTest()
        {
			var removeShutOff = CreateParametrizedShutOff(4, "ShutOff");
            var list = new List<ShutOff>()
			{
				CreateParametrizedShutOff(1, "ShutOff 1"),
				CreateParametrizedShutOff(2, "ShutOff 2"),
				removeShutOff,
				CreateParametrizedShutOff(3, "ShutOff 3")
			};

            _viewModel.ShowItems(list);
            _viewModel.Remove(removeShutOff);

            Assert.AreEqual(_viewModel.ShutOffCollectionView.Count, 3);
            Assert.AreEqual((_viewModel.ShutOffCollectionView.GetItemAt(0) as HelperTableItemModel<ShutOff, string>).Value, "ShutOff 1");
            Assert.AreEqual((_viewModel.ShutOffCollectionView.GetItemAt(1) as HelperTableItemModel<ShutOff, string>).Value, "ShutOff 2");
            Assert.AreEqual((_viewModel.ShutOffCollectionView.GetItemAt(2) as HelperTableItemModel<ShutOff, string>).Value, "ShutOff 3");
        }



        [Test]
        public void ShowConstructionTypeTest()
		{
			var list = CreateDefaultConstructionTypeList();

			_viewModel.ShowItems(list);

			Assert.AreEqual(_viewModel.ConstructionTypeCollectionView.Count, 3);
			Assert.AreEqual((_viewModel.ConstructionTypeCollectionView.GetItemAt(0) as HelperTableItemModel<ConstructionType, string>).Value, "ConstructionType 1");
			Assert.AreEqual((_viewModel.ConstructionTypeCollectionView.GetItemAt(1) as HelperTableItemModel<ConstructionType, string>).Value, "ConstructionType 2");
			Assert.AreEqual((_viewModel.ConstructionTypeCollectionView.GetItemAt(2) as HelperTableItemModel<ConstructionType, string>).Value, "ConstructionType 3");
		}

		[Test]
        public void AddConstructionTypeTest()
        {
			_viewModel.Add(CreateParametrizedConstructionType(0, "New ConstructionType"));
			_viewModel.Add(CreateParametrizedConstructionType(1, "New ConstructionType 2"));

            Assert.AreEqual(_viewModel.ConstructionTypeCollectionView.Count, 2);
            Assert.AreEqual((_viewModel.ConstructionTypeCollectionView.GetItemAt(0) as HelperTableItemModel<ConstructionType, string>).Value, "New ConstructionType");
            Assert.AreEqual((_viewModel.ConstructionTypeCollectionView.GetItemAt(1) as HelperTableItemModel<ConstructionType, string>).Value, "New ConstructionType 2");
        }

        [Test]
        public void SaveConstructionType()
        {
			var changeConstructionType = CreateParametrizedConstructionType(4, "ConstructionType");
			var list = CreateDefaultConstructionTypeList();
			list.Add(changeConstructionType);

            _viewModel.ShowItems(list);

            changeConstructionType.Value = new HelperTableDescription("Changed");
            _viewModel.Save(changeConstructionType);

            Assert.AreEqual(_viewModel.ConstructionTypeCollectionView.Count, 4);
            Assert.AreEqual((_viewModel.ConstructionTypeCollectionView.GetItemAt(3) as HelperTableItemModel<ConstructionType, string>).Value, "Changed");
        }

        [Test]
        public void RemoveConstructionTypeTest()
        {
            var removeConstructionType = CreateParametrizedConstructionType(4, "ConstructionType");
			var list = new List<ConstructionType>()
			{
				CreateParametrizedConstructionType(1, "ConstructionType 1"),
				CreateParametrizedConstructionType(2, "ConstructionType 2"),
				removeConstructionType,
				CreateParametrizedConstructionType(3, "ConstructionType 3")
			};

            _viewModel.ShowItems(list);
            _viewModel.Remove(removeConstructionType);

            Assert.AreEqual(_viewModel.ConstructionTypeCollectionView.Count, 3);
            Assert.AreEqual((_viewModel.ConstructionTypeCollectionView.GetItemAt(0) as HelperTableItemModel<ConstructionType, string>).Value, "ConstructionType 1");
            Assert.AreEqual((_viewModel.ConstructionTypeCollectionView.GetItemAt(1) as HelperTableItemModel<ConstructionType, string>).Value, "ConstructionType 2");
            Assert.AreEqual((_viewModel.ConstructionTypeCollectionView.GetItemAt(2) as HelperTableItemModel<ConstructionType, string>).Value, "ConstructionType 3");
        }

        [Test]
        public void UpdateColumnWidthsTest()
        {
            var updatedColumns = new List<(string, double)>()
            {
                ("BatteryVoltage", 500),
                ("AirConsumption", 60)
            };
            _viewModel.SetColumnWidths += (sender, list) =>
            {
                Assert.AreEqual(updatedColumns, list);
            };
            _viewModel.UpdateColumnWidths("ToolModel", updatedColumns);
            
            
        }

        [Test]
        public void ShowSaveColumnErrorTest()
        {
            _viewModel.MessageBoxRequest += (sender, args) => { Assert.Pass(); };
            _viewModel.ShowSaveColumnError("ToolModel");
            Assert.Fail("No Message was shown");
        }

        [Test]
        public void ShowColumnWidthsTest()
        {
            var updatedColumns = new List<(string, double)>()
            {
                ("BatteryVoltage", 500),
                ("AirConsumption", 60)
            };
            _viewModel.SetColumnWidths += (sender, list) =>
            {
                Assert.AreEqual(updatedColumns, list);
            };
            _viewModel.UpdateColumnWidths("ToolModel", updatedColumns);
        }

        [Test]
        public void ShowLoadColumnWidthsError()
        {
            _viewModel.MessageBoxRequest += (sender, args) => { Assert.Pass(); };
            _viewModel.ShowLoadColumnWidthsError("ToolModel");
            Assert.Fail("No Message was shown");
        }

        [TestCase(1.67, 3.65)]
        [TestCase(2.54, 1.36)]
        public void ShowingCmCmkShowsOnlyOneEntry(double cm, double cmk)
        {
            _viewModel.ShowCmCmk(cm, cmk);
            var expected = new List<(double cm, double cmk)> { (cm, cmk) };
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
        public void SetPictureForToolModelTest()
        {
			var manufacturer = CreateParametrizedManufacturer(5, "Test");
            var toolModelToAdd = new ToolModel {Id = new ToolModelId(1), Description = new ToolModelDescription("Test"), Manufacturer = manufacturer};

            _viewModel.AddManufacturer(manufacturer);
            _viewModel.ShowToolModels(new List<ToolModel>{toolModelToAdd});
            var picture = new Picture
            {
                FileName = "Test.jpg", FileType = 0, ImageStream = new MemoryStream(), NodeId = 4, NodeSeqId = 111, SeqId = 111
            };
            _viewModel.SetPictureForToolModel(1, picture);

            Assert.IsTrue(PictureEqualToPictureModel(picture, _viewModel.AllToolModelModels.First().Picture));
            Assert.IsFalse(_viewModel.SaveToolModelCommand.CanExecute(null));
        }

        [Test]
        public void ShowEmptyDiffDialog()
        {
            bool showDialogRequestInvoked = false;
            _viewModel.RequestVerifyChangesView += (s, e) => showDialogRequestInvoked = true;
            
            var result = _viewModel.ShowDiffDialog(GetToolModelDiff(GetToolModel(), GetToolModel()));

            Assert.IsFalse(showDialogRequestInvoked);
            Assert.IsFalse(result);
        }

        [Test]
        public void ShowDiffDialogNoTest()
        {
            _viewModel.SelectedToolModel = ToolModelModel.GetModelFor(GetToolModel(), new NullLocalizationWrapper());

            bool showDialogRequestInvoked = false;
            _viewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };

            var result = _viewModel.ShowDiffDialog(GetToolModelDiff(GetToolModel(), GetUpdatedToolModel()));

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsFalse(result);
        }

        [Test]
        public void ShowDiffDialogCancleTest()
        {
            bool showDialogRequestInvoked = false;
            _viewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Cancel;
            };

            var result = _viewModel.ShowDiffDialog(GetToolModelDiff(GetToolModel(), GetUpdatedToolModel()));

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsFalse(result);
        }

        [Test]
        public void ShowDiffDialogTest()
        {
            var diff = GetToolModelDiff(GetToolModel(), GetUpdatedToolModel());
            bool showDialogRequestInvoked = false;
            _viewModel.RequestVerifyChangesView += (s, e) =>
            {
                var changes = e.ChangedValues;
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;

                Assert.AreEqual(changes.Count, 16);
                Assert.AreEqual(changes[0].AffectedEntity, diff.NewToolModel.Description.ToDefaultString());
                Assert.AreEqual(changes[0].OldValue, diff.OldToolModel.Description.ToDefaultString());
                Assert.AreEqual(changes[0].NewValue, diff.NewToolModel.Description.ToDefaultString());
                Assert.AreEqual(changes[2].AffectedEntity, diff.NewToolModel.Description.ToDefaultString());
                Assert.AreEqual(changes[2].OldValue, diff.OldToolModel.Manufacturer.Name.ToDefaultString());
                Assert.AreEqual(changes[2].NewValue, diff.NewToolModel.Manufacturer.Name.ToDefaultString());
                Assert.AreEqual(changes[3].AffectedEntity, diff.NewToolModel.Description.ToDefaultString());
                Assert.AreEqual(changes[3].OldValue, diff.OldToolModel.MinPower.ToString());
                Assert.AreEqual(changes[3].NewValue, diff.NewToolModel.MinPower.ToString());
                Assert.AreEqual(changes[4].AffectedEntity, diff.NewToolModel.Description.ToDefaultString());
                Assert.AreEqual(changes[4].OldValue, diff.OldToolModel.MaxPower.ToString());
                Assert.AreEqual(changes[4].NewValue, diff.NewToolModel.MaxPower.ToString());
                Assert.AreEqual(changes[5].AffectedEntity, diff.NewToolModel.Description.ToDefaultString());
                Assert.AreEqual(changes[5].OldValue, diff.OldToolModel.AirPressure.ToString());
                Assert.AreEqual(changes[5].NewValue, diff.NewToolModel.AirPressure.ToString());
                Assert.AreEqual(changes[6].AffectedEntity, diff.NewToolModel.Description.ToDefaultString());
                Assert.AreEqual(changes[6].OldValue, diff.OldToolModel.ToolType.Value.ToDefaultString());
                Assert.AreEqual(changes[6].NewValue, diff.NewToolModel.ToolType.Value.ToDefaultString());
                Assert.AreEqual(changes[7].AffectedEntity, diff.NewToolModel.Description.ToDefaultString());
                Assert.AreEqual(changes[7].OldValue, diff.OldToolModel.Weight.ToString());
                Assert.AreEqual(changes[7].NewValue, diff.NewToolModel.Weight.ToString());
                Assert.AreEqual(changes[8].AffectedEntity, diff.NewToolModel.Description.ToDefaultString());
                Assert.AreEqual(changes[8].OldValue, diff.OldToolModel.BatteryVoltage.ToString());
                Assert.AreEqual(changes[8].NewValue, diff.NewToolModel.BatteryVoltage.ToString());
                Assert.AreEqual(changes[9].AffectedEntity, diff.NewToolModel.Description.ToDefaultString());
                Assert.AreEqual(changes[9].OldValue, diff.OldToolModel.MaxRotationSpeed.ToString());
                Assert.AreEqual(changes[9].NewValue, diff.NewToolModel.MaxRotationSpeed.ToString());
                Assert.AreEqual(changes[10].AffectedEntity, diff.NewToolModel.Description.ToDefaultString());
                Assert.AreEqual(changes[10].OldValue, diff.OldToolModel.AirConsumption.ToString());
                Assert.AreEqual(changes[10].NewValue, diff.NewToolModel.AirConsumption.ToString());
                Assert.AreEqual(changes[11].AffectedEntity, diff.NewToolModel.Description.ToDefaultString());
                Assert.AreEqual(changes[11].OldValue, diff.OldToolModel.SwitchOff.Value.ToDefaultString());
                Assert.AreEqual(changes[11].NewValue, diff.NewToolModel.SwitchOff.Value.ToDefaultString());
                Assert.AreEqual(changes[12].AffectedEntity, diff.NewToolModel.Description.ToDefaultString());
                Assert.AreEqual(changes[12].OldValue, diff.OldToolModel.DriveSize.Value.ToDefaultString());
                Assert.AreEqual(changes[12].NewValue, diff.NewToolModel.DriveSize.Value.ToDefaultString());
                Assert.AreEqual(changes[13].AffectedEntity, diff.NewToolModel.Description.ToDefaultString());
                Assert.AreEqual(changes[13].OldValue, diff.OldToolModel.ShutOff.Value.ToDefaultString());
                Assert.AreEqual(changes[13].NewValue, diff.NewToolModel.ShutOff.Value.ToDefaultString());
                Assert.AreEqual(changes[14].AffectedEntity, diff.NewToolModel.Description.ToDefaultString());
                Assert.AreEqual(changes[14].OldValue, diff.OldToolModel.DriveType.Value.ToDefaultString());
                Assert.AreEqual(changes[14].NewValue, diff.NewToolModel.DriveType.Value.ToDefaultString());
                Assert.AreEqual(changes[15].AffectedEntity, diff.NewToolModel.Description.ToDefaultString());
                Assert.AreEqual(changes[15].OldValue, diff.OldToolModel.ConstructionType.Value.ToDefaultString());
                Assert.AreEqual(changes[15].NewValue, diff.NewToolModel.ConstructionType.Value.ToDefaultString());
            };

            var result = _viewModel.ShowDiffDialog(diff);

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(result);
        }

        [Test]
        public void ShowRemoveManufacturerPreventingReferencesCallsRequestWithCorrectParameters()
        {
            var tool1 = CreateToolByIdSerialNumberAndInventoryNumber(15, "Test", "Hans");
            var tool2 = CreateToolByIdSerialNumberAndInventoryNumber(26, "Blub", "DiDup");
            string displayString = "Test";
            _toolDisplayFormatter.DisplayString = displayString;
            _viewModel.ReferencesDialogRequest += (sender, list) =>
            {
                Assert.AreEqual(2, list.References.Count);
                Assert.AreEqual(displayString, list.References[0]);
                Assert.AreEqual(displayString, list.References[1]);
                Assert.Pass();
            };
            _viewModel.ShowRemoveToolModelPreventingReferences(new List<ToolReferenceLink> { tool1, tool2 });
            Assert.Fail();
        }

        [Test]
        public void SetSelectSelectedToolModelSetsToolModelClasses()
        {
            var proxy = new ToolModelGuiProxy();
            var viewModel = new ToolModelViewModel(new StartUpMock(), new Core.UseCases.ToolModelUseCase(new ToolModelDataAccessMock(), new CmCmkDataAccessMock(), proxy, null, null, null, null, null, null, null, null, null), new SaveColumnsUseCase(null, null), new NullLocalizationWrapper(), new MockToolDisplayFormatter());
            proxy.real = viewModel;
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            viewModel.SelectedToolModel = ToolModelModel.GetModelFor(CreateToolModel.RandomizedWithType(32535, new ECDriver()), new NullLocalizationWrapper());
            Assert.IsNull(viewModel.AllToolModelClassesCollectionView);

            viewModel.SelectedToolModel = ToolModelModel.GetModelFor(CreateToolModel.RandomizedWithType(32535, new ClickWrench()), new NullLocalizationWrapper());

            var oldClass = viewModel.SelectedToolModel.Class;

            Assert.AreEqual(7, viewModel.AllToolModelClassesCollectionView.Count);
            Assert.AreEqual((viewModel.AllToolModelClassesCollectionView.GetItemAt(0) as ToolModelClassModel).ToolModelClass, ToolModelClass.WrenchScale);
            Assert.AreEqual((viewModel.AllToolModelClassesCollectionView.GetItemAt(1) as ToolModelClassModel).ToolModelClass, ToolModelClass.WrenchFixSet);
            Assert.AreEqual((viewModel.AllToolModelClassesCollectionView.GetItemAt(2) as ToolModelClassModel).ToolModelClass, ToolModelClass.WrenchWithoutScale);
            Assert.AreEqual((viewModel.AllToolModelClassesCollectionView.GetItemAt(3) as ToolModelClassModel).ToolModelClass, ToolModelClass.DriverScale);
            Assert.AreEqual((viewModel.AllToolModelClassesCollectionView.GetItemAt(4) as ToolModelClassModel).ToolModelClass, ToolModelClass.DriverFixSet);
            Assert.AreEqual((viewModel.AllToolModelClassesCollectionView.GetItemAt(5) as ToolModelClassModel).ToolModelClass, ToolModelClass.DriverWithoutScale);
            Assert.AreEqual((viewModel.AllToolModelClassesCollectionView.GetItemAt(6) as ToolModelClassModel).ToolModelClass, ToolModelClass.WrenchWithBendingSteelLever);

            viewModel.SelectedToolModel = ToolModelModel.GetModelFor(CreateToolModel.RandomizedWithType(32535, new ProductionWrench()), new NullLocalizationWrapper());
            Assert.AreEqual(5, viewModel.AllToolModelClassesCollectionView.Count);
            Assert.AreEqual((viewModel.AllToolModelClassesCollectionView.GetItemAt(0) as ToolModelClassModel).ToolModelClass, ToolModelClass.WrenchBendingSteelLever);
            Assert.AreEqual((viewModel.AllToolModelClassesCollectionView.GetItemAt(1) as ToolModelClassModel).ToolModelClass, ToolModelClass.WrenchWithDialIndicator);
            Assert.AreEqual((viewModel.AllToolModelClassesCollectionView.GetItemAt(2) as ToolModelClassModel).ToolModelClass, ToolModelClass.WrenchElectronic);
            Assert.AreEqual((viewModel.AllToolModelClassesCollectionView.GetItemAt(3) as ToolModelClassModel).ToolModelClass, ToolModelClass.DriverWithDialIndicator);
            Assert.AreEqual((viewModel.AllToolModelClassesCollectionView.GetItemAt(4) as ToolModelClassModel).ToolModelClass, ToolModelClass.DriverElectronic);

            viewModel.SelectedToolModel = ToolModelModel.GetModelFor(CreateToolModel.RandomizedWithType(32535, new MDWrench()), new NullLocalizationWrapper());
            Assert.AreEqual(5, viewModel.AllToolModelClassesCollectionView.Count);
            Assert.AreEqual((viewModel.AllToolModelClassesCollectionView.GetItemAt(0) as ToolModelClassModel).ToolModelClass, ToolModelClass.WrenchBendingSteelLever);
            Assert.AreEqual((viewModel.AllToolModelClassesCollectionView.GetItemAt(1) as ToolModelClassModel).ToolModelClass, ToolModelClass.WrenchWithDialIndicator);
            Assert.AreEqual((viewModel.AllToolModelClassesCollectionView.GetItemAt(2) as ToolModelClassModel).ToolModelClass, ToolModelClass.WrenchElectronic);
            Assert.AreEqual((viewModel.AllToolModelClassesCollectionView.GetItemAt(3) as ToolModelClassModel).ToolModelClass, ToolModelClass.DriverWithDialIndicator);
            Assert.AreEqual((viewModel.AllToolModelClassesCollectionView.GetItemAt(4) as ToolModelClassModel).ToolModelClass, ToolModelClass.DriverElectronic);

            Assert.AreEqual(oldClass, viewModel.SelectedToolModel.Class);
        }


        [Test]
        public void SetSelectedSelectedModelTypeWithNullSelectedModelDoesNothing()
        {
            var proxy = new ToolModelGuiProxy();
            var viewModel = new ToolModelViewModel(new StartUpMock(), new Core.UseCases.ToolModelUseCase(new ToolModelDataAccessMock(), new CmCmkDataAccessMock(), proxy, null, null, null, null, null, null, null, null, null), new SaveColumnsUseCase(null, null), new NullLocalizationWrapper(), new MockToolDisplayFormatter());
            proxy.real = viewModel;
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            viewModel.SelectedToolModel = null;
            try
            {
                viewModel.SelectedModelType = AbstractToolTypeModel.MapToolTypeToToolTypeModel(new ECDriver(), new NullLocalizationWrapper());
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        private static IEnumerable<AbstractToolType> ToolTypeData = new List<AbstractToolType>()
        {
            new ProductionWrench(),
            new ClickWrench(),
            new ECDriver(),
            new General()
        };

        [TestCaseSource(nameof(ToolTypeData))]
        public void SetSelectedModelTypeSetTypeOfSelectedModel(AbstractToolType tooltype)
        {
            var proxy = new ToolModelGuiProxy();
            var viewModel = new ToolModelViewModel(new StartUpMock(), new Core.UseCases.ToolModelUseCase(new ToolModelDataAccessMock(), new CmCmkDataAccessMock(), proxy, null, null, null, null, null, null, null, null, null), new SaveColumnsUseCase(null, null), new NullLocalizationWrapper(), new MockToolDisplayFormatter());
            proxy.real = viewModel;
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            viewModel.SelectedToolModel = ToolModelModel.GetModelFor(CreateToolModel.RandomizedWithTypeAndClass(32535, new ClickWrench(), ToolModelClass.DriverElectronic), new NullLocalizationWrapper());
            var newType = AbstractToolTypeModel.MapToolTypeToToolTypeModel(tooltype, new NullLocalizationWrapper());
            viewModel.SelectedModelType = newType;
            Assert.AreEqual(newType, viewModel.SelectedToolModel.ModelType);
        }

        [Test]
        public void SetSelectedModelTypeWithSameModelTypeSetsClassToClassBeforeChange()
        {
            var proxy = new ToolModelGuiProxy();
            var viewModel = new ToolModelViewModel(new StartUpMock(), new Core.UseCases.ToolModelUseCase(new ToolModelDataAccessMock(), new CmCmkDataAccessMock(), proxy, null, null, null, null, null, null, null, null, null), new SaveColumnsUseCase(null, null), new NullLocalizationWrapper(), new MockToolDisplayFormatter());
            proxy.real = viewModel;
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            var oldClass = ToolModelClass.DriverElectronic;
            viewModel.SelectedToolModel = ToolModelModel.GetModelFor(CreateToolModel.RandomizedWithTypeAndClass(32535, new ECDriver(), oldClass), new NullLocalizationWrapper());
            viewModel.SelectedToolModel.Entity.Class = ToolModelClass.DriverScale;
            var newType = AbstractToolTypeModel.MapToolTypeToToolTypeModel(new ECDriver(), new NullLocalizationWrapper());
            viewModel.SelectedModelType = newType;
            Assert.AreEqual(oldClass, viewModel.SelectedToolModel.Entity.Class);
        }


        [Test]
        public void SetSelectedModelTypeWithOtherModelTypeSetsClassToFirstClassFromAllToolModelClassesCollectionView()
        {
            var proxy = new ToolModelGuiProxy();
            var viewModel = new ToolModelViewModel(new StartUpMock(), new Core.UseCases.ToolModelUseCase(new ToolModelDataAccessMock(), new CmCmkDataAccessMock(), proxy, null, null, null, null, null, null, null, null, null), new SaveColumnsUseCase(null, null), new NullLocalizationWrapper(), new MockToolDisplayFormatter());
            proxy.real = viewModel;
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            viewModel.SelectedToolModel = ToolModelModel.GetModelFor(CreateToolModel.RandomizedWithTypeAndClass(32535, new PulseDriver(), (ToolModelClass)(-1)), new NullLocalizationWrapper());
            viewModel.SelectedModelType = AbstractToolTypeModel.MapToolTypeToToolTypeModel(new MDWrench(), new NullLocalizationWrapper());
            var expected = (viewModel.AllToolModelClassesCollectionView.GetItemAt(0) as ToolModelClassModel).ToolModelClass;
            Assert.AreEqual(expected, viewModel.SelectedToolModel.Class.ToolModelClass);
        }

        private ToolReferenceLink CreateToolByIdSerialNumberAndInventoryNumber(int id, string inventoryNumber, string serialNumber)
        {
            return new ToolReferenceLink(new QstIdentifier(id), inventoryNumber, serialNumber, _toolDisplayFormatter);
        }

        private bool PictureEqualToPictureModel(Picture picture, PictureModel pictureModel)
        {
            return picture.FileName == pictureModel.FileName &&
                   picture.FileType == pictureModel.FileType &&
                   picture.NodeId == pictureModel.NodeId &&
                   picture.NodeSeqId == pictureModel.NodeSeqId &&
                   picture.SeqId == pictureModel.SeqId;
        }

        private static ToolModelDiff GetToolModelDiff(ToolModel oldToolModel, ToolModel newToolModel)
        {
            return new ToolModelDiff()
            {
                OldToolModel = oldToolModel,
                NewToolModel = newToolModel
            };
        }

        private ToolModel GetToolModel()
        {
            return new ToolModel()
            {
                Id = new ToolModelId(56),
                Description = new ToolModelDescription("wrepiofbjhvnmk"),
                ModelType = new General(),
                Class = Core.Enums.ToolModelClass.DriverWithoutScale,
                Manufacturer = new Manufacturer() { Id = new ManufacturerId(437890), Name = new ManufacturerName("bgreuioöklmnb") },
                MinPower = 743890.647389,
                MaxPower = 438294930.40,
                AirPressure = 784309.6789,
                ToolType = new ToolType() { ListId = new HelperTableEntityId(7869514), Value = new HelperTableDescription("sdölfjalfjklsövn") },
                Weight = 7869541.56,
                BatteryVoltage = 76578.52,
                MaxRotationSpeed = 785453,
                AirConsumption = 8987.43,
                SwitchOff = new SwitchOff() { ListId = new HelperTableEntityId(675490), Value = new HelperTableDescription("bwreuiomnh") },
                DriveSize = new DriveSize() { ListId = new HelperTableEntityId(546211), Value = new HelperTableDescription("7o6zvn chlseuigjkdl") },
                ShutOff = new ShutOff() { ListId = new HelperTableEntityId(20398457), Value = new HelperTableDescription("gdstrkfjlvbhper") },
                DriveType = new DriveType() { ListId = new HelperTableEntityId(4309857), Value = new HelperTableDescription("bfgauäojfbhör") },
                ConstructionType = new ConstructionType() { ListId = new HelperTableEntityId(321654), Value = new HelperTableDescription("bn,trhln.bdjfgö") },
                Picture = new Picture() { SeqId = 45621, FileName = "bxhl.byfdhpeuöioj" }
            };
        }

        private ToolModel GetUpdatedToolModel()
        {
            return new ToolModel()
            {
                Id = new ToolModelId(56),
                Description = new ToolModelDescription("c,gsdökjgh"),
                ModelType = new ECDriver(),
                Class = Core.Enums.ToolModelClass.WrenchFixSet,
                Manufacturer = new Manufacturer() { Id = new ManufacturerId(453), Name = new ManufacturerName("blhjkgazlulhbjklndg") },
                MinPower = 5645,
                MaxPower = 46564,
                AirPressure = 456,
                ToolType = new ToolType() { ListId = new HelperTableEntityId(53838), Value = new HelperTableDescription("szujhbyjfhlrg") },
                Weight = 65686,
                BatteryVoltage = 75,
                MaxRotationSpeed = 7567,
                AirConsumption = 6756,
                SwitchOff = new SwitchOff() { ListId = new HelperTableEntityId(3863), Value = new HelperTableDescription("sgzleurighleghö") },
                DriveSize = new DriveSize() { ListId = new HelperTableEntityId(3), Value = new HelperTableDescription("bghleuö-vjksyjäfgk") },
                ShutOff = new ShutOff() { ListId = new HelperTableEntityId(3836), Value = new HelperTableDescription("rdoqzöierghlajdhfbvydb") },
                DriveType = new DriveType() { ListId = new HelperTableEntityId(83), Value = new HelperTableDescription("zghlzo7ghlbkno4pnöi") },
                ConstructionType = new ConstructionType() { ListId = new HelperTableEntityId(83683), Value = new HelperTableDescription("ghlzughjDÖLghzuo") },
                Picture = new Picture() { SeqId = 83653, FileName = "nhutgopnhgöjgbh" }
            };
        }

        private static ToolModel CreateParametricedToolModel(int id, string description)
        {
            return new ToolModel() { Id = new ToolModelId(id), Description = new ToolModelDescription(description) };
        }

        private static Manufacturer CreateParametrizedManufacturer(int manuId, string manuName)
		{
			return new Manufacturer() { Id = new ManufacturerId(manuId), Name = new ManufacturerName(manuName) };
		}

		private static List<Manufacturer> CreateDefaultManufacturerList()
		{
			return new List<Manufacturer>()
			{
				CreateParametrizedManufacturer(0, "Manu1"),
				CreateParametrizedManufacturer(1, "Manu2"),
				CreateParametrizedManufacturer(2, "Manu3")
			};
		}

		private static List<ConstructionType> CreateDefaultConstructionTypeList()
		{
			return new List<ConstructionType>()
			{
				CreateParametrizedConstructionType(0,"ConstructionType 1"),
				CreateParametrizedConstructionType(1, "ConstructionType 2"),
				CreateParametrizedConstructionType(2, "ConstructionType 3")
			};
		}

		private static ConstructionType CreateParametrizedConstructionType(int id, string description)
		{
			return new ConstructionType { ListId = new HelperTableEntityId(id), Value = new HelperTableDescription(description) };
		}

		private static DriveSize CreateParametrizedDriveSize(int id, string description)
		{
			return new DriveSize { ListId = new HelperTableEntityId(id), Value = new HelperTableDescription(description) };
		}

		private static List<DriveSize> CreateDefaultDriveSizeList()
		{
			return new List<DriveSize>()
			{
				CreateParametrizedDriveSize(0,"DriveSize1"),
				CreateParametrizedDriveSize(1, "DriveSize2"),
				CreateParametrizedDriveSize(2, "DriveSize3")
			};
		}

		private static DriveType CreateParametrizedDriveType(int id, string description)
		{
			return new DriveType { ListId = new HelperTableEntityId(id), Value = new HelperTableDescription(description) };
		}

		private static List<DriveType> CreateDefaultDriveTypeList()
		{
			return new List<DriveType>()
			{
				CreateParametrizedDriveType(0, "DriveType 1"),
				CreateParametrizedDriveType(1, "DriveType 2"),
				CreateParametrizedDriveType(2, "DriveType 3")
			};
		}

		private static ShutOff CreateParametrizedShutOff(int id, string description)
		{
			return new ShutOff { ListId = new HelperTableEntityId(id), Value = new HelperTableDescription(description) };
		}

		private static List<ShutOff> CreateDefaultShutOffList()
		{
			return new List<ShutOff>()
			{
				CreateParametrizedShutOff(0,"ShutOff 1"),
				CreateParametrizedShutOff(1, "ShutOff 2"),
				CreateParametrizedShutOff(2, "ShutOff 3")
			};
		}

		private static SwitchOff CreateParametrizedSwitchOff(int id, string description)
		{
			return new SwitchOff { ListId = new HelperTableEntityId(id), Value = new HelperTableDescription(description) };
		}

		private static List<SwitchOff> CreateDefaultSwitchOffList()
		{
			return new List<SwitchOff>()
			{
				CreateParametrizedSwitchOff(0,"SwitchOff 1"),
				CreateParametrizedSwitchOff(1, "SwitchOff 2"),
				CreateParametrizedSwitchOff(2, "SwitchOff 3")
			};
		}

		private static ToolType CreateParametrizedToolType(int id, string description)
		{
			return new ToolType { ListId = new HelperTableEntityId(id), Value = new HelperTableDescription(description) };
		}

		private static List<ToolType> CreateDefaultToolTypeList()
		{
			return new List<ToolType>()
			{
				CreateParametrizedToolType(0, "ToolType1"),
				CreateParametrizedToolType(1, "ToolType2"),
				CreateParametrizedToolType(2, "ToolType3")
			};
		}
	}
}
