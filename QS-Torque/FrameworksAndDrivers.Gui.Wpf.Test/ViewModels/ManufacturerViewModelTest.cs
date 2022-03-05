using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Core.Entities.ReferenceLink;
using InterfaceAdapters.Models;
using TestHelper.Factories;
using TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    public class ManufacturerViewModelTest
    {
        private class ManufacturerUseCaseMock : IManufacturerUseCase
        {
            public int LoadManufacturerCallCount;
			public Manufacturer lastSaveManufacturerOldManufacturer;
			public Manufacturer lastSaveManufacturerNewManufacturer;
            public Manufacturer RemoveManufacturerParameter;

            public void LoadManufacturer()
            {
                LoadManufacturerCallCount++;
            }

            public void RemoveManufacturer(Manufacturer manufacturer, IManufacturerGui active)
            {
                RemoveManufacturerParameter = manufacturer;
            }

            public void SaveManufacturer(Manufacturer oldManufacturer, Manufacturer newManufacturer)
			{
				lastSaveManufacturerOldManufacturer = oldManufacturer;
				lastSaveManufacturerNewManufacturer = newManufacturer;
			}

            public void LoadReferencedToolModels(long manufacturerId)
            {
                throw new System.NotImplementedException();
            }

            public void LoadCommentForManufacturer(Manufacturer manufacturer)
			{
			}

            public void AddManufacturer(Manufacturer manufacturer)
            {
                throw new System.NotImplementedException();
            }
        }

        private class SaveColumnsUseCaseMock : SaveColumnsUseCase
        {
            public SaveColumnsUseCaseMock(ISaveColumnsGui guiInterface, ISaveColumnsData dataInterface) : base(guiInterface, dataInterface)
            {
            }

        }

        private ManufacturerViewModel _manufacturerViewModel;
        private ManufacturerUseCaseMock _manufacturerUseCaseMock;

        [SetUp]
        public void SetUp()
        {
            _manufacturerUseCaseMock = new ManufacturerUseCaseMock();
            _manufacturerViewModel = new ManufacturerViewModel(new StartUpMock(), _manufacturerUseCaseMock, new SaveColumnsUseCaseMock(null, null), new NullLocalizationWrapper());
			_manufacturerViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
        }

		[Test]
        public void ShowManufacturerTest()
        {
			var manufacturerList = fullManufacturers;
            _manufacturerViewModel.ShowManufacturer(manufacturerList);
            var guiManufacturers = _manufacturerViewModel.Manufacturers.SourceCollection as IList<ManufacturerModel>;
            if (guiManufacturers.Count <= 0)
            {
                Assert.Fail("gui List was empty");
            }
            foreach (var manufacturerModel in _manufacturerViewModel.Manufacturers.SourceCollection as IList<ManufacturerModel>)
            {
                var singleOrDefault = manufacturerList.SingleOrDefault(x => AreEqual(x,manufacturerModel));
                if (singleOrDefault == null)
                {
                    Assert.Fail("Lists are not equal");
                }
            }
            Assert.Pass();
        }

        private bool AreEqual(Manufacturer manufacturer, ManufacturerModel manufacturerModel)
        {
            return manufacturer.Id.ToLong() == manufacturerModel.Id &&
                   manufacturer.Name.ToDefaultString() == manufacturerModel.Name &&
                   manufacturer.Person == manufacturerModel.Person &&
                   manufacturer.PhoneNumber == manufacturerModel.PhoneNumber &&
                   manufacturer.Fax == manufacturerModel.Fax &&
                   manufacturer.Street == manufacturerModel.Street &&
                   manufacturer.HouseNumber == manufacturerModel.HouseNumber &&
                   manufacturer.Plz == manufacturerModel.Plz &&
                   manufacturer.City == manufacturerModel.City &&
                   manufacturer.Country == manufacturerModel.Country &&
                   manufacturer.Comment == manufacturerModel.Comment;
        }

        [Test]
        public void LoadManufacturersTest()
        {
            _manufacturerViewModel.LoadManufacturers();
            Assert.AreEqual(_manufacturerUseCaseMock.LoadManufacturerCallCount,1);
        }

		[Test, TestCaseSource(nameof(fullManufacturers))]
		public void SavingManufacturerDeliversCorrectOldManufacturer(Manufacturer manufacturer)
		{
			_manufacturerViewModel.SelectedManufacturer = ManufacturerModelFromManufacturer(manufacturer);
			_manufacturerViewModel.SelectedManufacturer.Name = "NewName";
			_manufacturerViewModel.SaveCommand.Execute(null);
			Assert.IsTrue(ContentEqual(manufacturer, _manufacturerUseCaseMock.lastSaveManufacturerOldManufacturer));
		}

		[Test, TestCaseSource(nameof(fullManufacturers))]
		public void SavingManufacturerDeliversCorrectNewManufacturer(Manufacturer manufacturer)
		{
			_manufacturerViewModel.SelectedManufacturer = ManufacturerModelFromManufacturer(manufacturer);
			_manufacturerViewModel.SelectedManufacturer.Name = "NewName";
			manufacturer.Name = new ManufacturerName("NewName");
			_manufacturerViewModel.SaveCommand.Execute(null);
			Assert.IsTrue(ContentEqual(manufacturer, _manufacturerUseCaseMock.lastSaveManufacturerNewManufacturer));
		}

		[Test, TestCaseSource(nameof(fullManufacturers))]
		public void SavingManufacturerDeliversAnotherCorrectNewManufacturer(Manufacturer manufacturer)
		{
			_manufacturerViewModel.SelectedManufacturer = ManufacturerModelFromManufacturer(manufacturer);
			_manufacturerViewModel.SelectedManufacturer.Name = "AnotherName";
			manufacturer.Name = new ManufacturerName("AnotherName");
			_manufacturerViewModel.SaveCommand.Execute(null);
			Assert.IsTrue(ContentEqual(manufacturer, _manufacturerUseCaseMock.lastSaveManufacturerNewManufacturer));
		}

        [Test]
        public void ShowRemoveManufacturerPreventingReferencesCallsRequestWithCorrectParameters()
        {
            var manufacturerUseCaseMock = new ManufacturerUseCaseMock();
            var manufacturerViewModel = new ManufacturerViewModel(new StartUpMock(), manufacturerUseCaseMock, new SaveColumnsUseCaseMock(null, null), new NullLocalizationWrapper());
            manufacturerViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            var toolModel1 = CreateToolModelByValueAndIdOnly(15, "Test");
            var toolModel2 = CreateToolModelByValueAndIdOnly(26, "Blub");
            manufacturerViewModel.ReferencesDialogRequest += (sender, list) =>
            {
                Assert.AreEqual(2, list.References.Count);
                Assert.AreEqual(toolModel1.DisplayName, list.References[0]);
                Assert.AreEqual(toolModel2.DisplayName, list.References[1]);
                Assert.Pass();
            };
            manufacturerViewModel.ShowRemoveManufacturerPreventingReferences(new List<ToolModelReferenceLink>{toolModel1, toolModel2});
            Assert.Fail();
        }


        [Test]
        public void InvokeRemoveManufacturerExecuteCallsRemoveManufacturerWithResetedParameters()
        {
            var useCase = new ManufacturerUseCaseMock();
            var viewModel = new ManufacturerViewModel(new StartUpMock(), useCase, new SaveColumnsUseCaseMock(null, null), new NullLocalizationWrapper());
            viewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            var manufacturer = CreateManufacturer.Anonymous();
            var manufacturerModel = ManufacturerModelFromManufacturer(manufacturer.CopyDeep());

            viewModel.AddManufacturer(manufacturerModel.Entity);

            manufacturerModel.Name = "dfgdf436765";
            manufacturerModel.City = "ftgh54756";
            manufacturerModel.Country = "45687678 45456";

            viewModel.SelectionChanged.Invoke(new SelectionChangedEventArgs(
                EventManager.RegisterRoutedEvent("selectionchanged", RoutingStrategy.Bubble, this.GetType(), this.GetType()),
                removedItems: new List<ManufacturerModel>(),
                addedItems: new List<ManufacturerModel>(){ manufacturerModel }));

            viewModel.MessageBoxRequest += (sender, args) => args.ResultAction?.Invoke(MessageBoxResult.Yes);
            viewModel.RemoveManufacturer.Invoke(null);

            Assert.IsTrue(useCase.RemoveManufacturerParameter.EqualsByContent(manufacturer));
        }

        private ToolModelReferenceLink CreateToolModelByValueAndIdOnly(int id, string value)
        {
            return new ToolModelReferenceLink() {Id = new ToolModelId(id), DisplayName = value};
        }

        private ManufacturerModel ManufacturerModelFromManufacturer(Manufacturer manufacturer)
        {
            return new ManufacturerModel(manufacturer);
        }

		private bool ContentEqual(Manufacturer lhs, Manufacturer rhs)
		{
			return
				lhs.City.Equals(rhs.City)
				&& lhs.Comment.Equals(rhs.Comment)
				&& lhs.Country.Equals(rhs.Country)
				&& lhs.Fax.Equals(rhs.Fax)
				&& lhs.HouseNumber.Equals(rhs.HouseNumber)
				&& lhs.Id.Equals(rhs.Id)
				&& lhs.Name.ToDefaultString().Equals(rhs.Name.ToDefaultString())
				&& lhs.Person.Equals(rhs.Person)
				&& lhs.PhoneNumber.Equals(rhs.PhoneNumber)
				&& lhs.Plz.Equals(rhs.Plz)
				&& lhs.Street.Equals(rhs.Street);
		}

		static readonly List<Manufacturer> fullManufacturers = new List<Manufacturer>
		{
			new Manufacturer
			{
				Id = new ManufacturerId(0),
				City = "Deggendorf",
				Comment = "Test",
				Country = "Country",
				Fax = "Fax",
				HouseNumber = "5",
				Name = new ManufacturerName("Hans"),
				PhoneNumber = "12345",
				Person = "Person",
				Plz =" 94469",
				Street = "Hansstraße"
			},
			new Manufacturer
			{
				Id = new ManufacturerId(1),
				City = "Passau",
				Comment = "Test2",
				Country = "Deutschland",
				Fax = "Fax2",
				HouseNumber = "10",
				Name = new ManufacturerName("Peter"),
				PhoneNumber = "54321",
				Person = "Person2",
				Plz = "94034",
				Street = "Peterstraße"
			}
		};
	}
}