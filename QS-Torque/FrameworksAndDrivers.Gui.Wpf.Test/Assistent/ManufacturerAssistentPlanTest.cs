using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Threading;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Assistent
{
    class ManufacturerAssistentPlanTest
    {
        private ListAssistentItemModel<Manufacturer> _item;
        private ManufacturerAssistentPlan _plan;

        private Manufacturer manufacturer0;
        private Manufacturer manufacturer1;
        private Manufacturer manufacturer2;

        [SetUp]
        public void ManufacturerAssistenPlanSetUp()
		{
			manufacturer0 = CreateParametrizedManufacturer(0, "Hersteller");
			manufacturer1 = CreateParametrizedManufacturer(1, "Manufacturer");
			manufacturer2 = CreateParametrizedManufacturer(2, "Fabricant");

			var defaultModels = new List<Manufacturer>() { manufacturer0, manufacturer1, manufacturer2 };

			_item = new ListAssistentItemModel<Manufacturer>(Dispatcher.CurrentDispatcher, defaultModels, "", "", null, (o, i) => { }, "", x => x.Name.ToDefaultString(), () => { });
			_item.ItemsCollectionView.SortDescriptions.Clear();
			_plan = new ManufacturerAssistentPlan(null, _item);
		}

		[Test]
        public void ShowManufacturerTest()
        {
			var manufacturer0 = CreateNameOnlyManufacturer("Hersteller 1");
			var manufacturer1 = CreateNameOnlyManufacturer("Manufacturer 1");
			var manufacturer2 = CreateNameOnlyManufacturer("Fabricant 1");

            _plan.ShowManufacturer(new List<Manufacturer>() { manufacturer0, manufacturer1, manufacturer2 });

            Assert.AreEqual(_item.ItemsCollectionView.Count, 3);
            Assert.AreEqual(((_item.ItemsCollectionView.GetItemAt(0) as DisplayMemberModel<Manufacturer>).Item as Manufacturer).Name.ToDefaultString(), "Hersteller 1");
            Assert.AreEqual(((_item.ItemsCollectionView.GetItemAt(1) as DisplayMemberModel<Manufacturer>).Item as Manufacturer).Name.ToDefaultString(), "Manufacturer 1");
            Assert.AreEqual(((_item.ItemsCollectionView.GetItemAt(2) as DisplayMemberModel<Manufacturer>).Item as Manufacturer).Name.ToDefaultString(), "Fabricant 1");
        }

        [Test]
        public void AddManufacturerTest()
        {
			var newManufacturer = CreateNameOnlyManufacturer("New Manufacturer");

            _plan.AddManufacturer(newManufacturer);

            Assert.AreEqual(_item.ItemsCollectionView.Count, 4);
            Assert.AreEqual(((_item.ItemsCollectionView.GetItemAt(3) as DisplayMemberModel<Manufacturer>).Item as Manufacturer).Name.ToDefaultString(), "New Manufacturer");
        }

        [Test]
        public void RemoveManufacturerTest()
        {
            var removeManufacturer = new Manufacturer() { Id = manufacturer1.Id, Name = manufacturer1.Name };

            _plan.RemoveManufacturer(removeManufacturer);

            Assert.AreEqual(_item.ItemsCollectionView.Count, 2);
            Assert.AreEqual(((_item.ItemsCollectionView.GetItemAt(0) as DisplayMemberModel<Manufacturer>).Item as Manufacturer).Name.ToDefaultString(), "Hersteller");
            Assert.AreEqual(((_item.ItemsCollectionView.GetItemAt(1) as DisplayMemberModel<Manufacturer>).Item as Manufacturer).Name.ToDefaultString(), "Fabricant");
        }

        [Test]
        public void SaveManufacturerTest()
        {
			var manufacturer = CreateParametrizedManufacturer(manufacturer2.Id.ToLong(), "Changed Name");

            _plan.SaveManufacturer(manufacturer);

            Assert.AreEqual(_item.ItemsCollectionView.Count, 3);
            Assert.AreEqual(((_item.ItemsCollectionView.GetItemAt(2) as DisplayMemberModel<Manufacturer>).Item as Manufacturer).Id, manufacturer2.Id);
            Assert.AreEqual(((_item.ItemsCollectionView.GetItemAt(2) as DisplayMemberModel<Manufacturer>).Item as Manufacturer).Name.ToDefaultString(), "Changed Name");
        }

		private static Manufacturer CreateParametrizedManufacturer(long id, string name)
		{
			return new Manufacturer() { Id = new ManufacturerId(id), Name = new ManufacturerName(name) };
		}

		private static Manufacturer CreateNameOnlyManufacturer(string name)
		{
			return new Manufacturer() { Name = new ManufacturerName(name) };
		}
	}
}
