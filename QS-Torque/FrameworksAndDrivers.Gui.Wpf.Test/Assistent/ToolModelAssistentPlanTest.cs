using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Threading;
using ToolModel = Core.Entities.ToolModel;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Assistent
{
    class ToolModelAssistentPlanTest
    {
        private ListAssistentItemModel<ToolModel> _item;
        private ToolModelAssistentPlan _plan;

        private ToolModel _toolModel0;
        private ToolModel _toolModel1;
        private ToolModel _toolModel2;

        [SetUp]
        public void ToolModelAssistenPlanSetUp()
        {
            _toolModel0 = CreateParametrizedToolModel(0, "Item");
            _toolModel1 = CreateParametrizedToolModel(1, "ItemItem");
            _toolModel2 = CreateParametrizedToolModel(2, "ItemItemItem");

            var defaultModels = new List<ToolModel>() { _toolModel0, _toolModel1, _toolModel2 };

            _item = new ListAssistentItemModel<ToolModel>(Dispatcher.CurrentDispatcher, defaultModels, "", "", null, (o, i) => { }, "", x => x.Description.ToDefaultString(), () => { });
            _item.ItemsCollectionView.SortDescriptions.Clear();
            _plan = new ToolModelAssistentPlan(null, _item);
        }

        [Test]
        public void ShowToolModelTest()
        {
            var toolModel0 = CreateParametrizedToolModel(0, "Item 1");
            var toolModel1 = CreateParametrizedToolModel(1, "ItemItem 1");
            var toolModel2 = CreateParametrizedToolModel(2, "ItemItemItem 1");

            _plan.ShowToolModels(new List<ToolModel>() { toolModel0, toolModel1, toolModel2 });

            Assert.AreEqual(GetItemCountInCollection(), 3);
            Assert.AreEqual(GetItemInCollectionViewAt(0).Description.ToDefaultString(), "Item 1");
            Assert.AreEqual(GetItemInCollectionViewAt(1).Description.ToDefaultString(), "ItemItem 1");
            Assert.AreEqual(GetItemInCollectionViewAt(2).Description.ToDefaultString(), "ItemItemItem 1");
        }

        [Test]
        public void AddToolModelTest()
        {
            var toolModel = CreateParametrizedToolModel(0, "New Item");

            _plan.AddToolModel(toolModel);

            Assert.AreEqual(GetItemCountInCollection(), 4);
            Assert.AreEqual(GetItemInCollectionViewAt(3).Description.ToDefaultString(), "New Item");
        }

        [Test]
        public void RemoveToolModelTest()
        {
            var removeItems = new List<ToolModel> { CopyToolModel(_toolModel1) };

            _plan.RemoveToolModels(removeItems);

            Assert.AreEqual(GetItemCountInCollection(), 2);
            Assert.AreEqual(GetItemInCollectionViewAt(0).Description.ToDefaultString(), "Item");
            Assert.AreEqual(GetItemInCollectionViewAt(1).Description.ToDefaultString(), "ItemItemItem");
        }

        [Test]
        public void SaveToolModelTest()
        {
            var item = CopyToolModel(_toolModel2);
            item.Description = new ToolModelDescription("Changed Value");

            _plan.UpdateToolModel(item);

            Assert.AreEqual(GetItemCountInCollection(), 3);
            Assert.AreEqual(GetItemInCollectionViewAt(2).Id, _toolModel2.Id);
            Assert.AreEqual(GetItemInCollectionViewAt(2).Description.ToDefaultString(), "Changed Value");
        }

        [Test]
        public void SetPictureForToolModelTest()
        {
            var pict = new Picture();
            _plan.SetPictureForToolModel(_toolModel1.Id.ToLong(), pict);

            Assert.AreEqual(GetItemInCollectionViewAt(1).Picture, pict);
        }

        private static ToolModel CreateParametrizedToolModel(int id, string description)
        {
            return new ToolModel() { Id = new ToolModelId(id), Description = new ToolModelDescription(description) };
        }

        private int GetItemCountInCollection()
        {
            return _item.ItemsCollectionView.Count;
        }

        private ToolModel GetItemInCollectionViewAt(int Index)
        {
            return ((_item.ItemsCollectionView.GetItemAt(Index) as DisplayMemberModel<ToolModel>).Item as ToolModel);
        }

        private static ToolModel CopyToolModel(ToolModel other)
        {
            return new ToolModel() { Id = other.Id, Description = other.Description };
        }
    }
}
