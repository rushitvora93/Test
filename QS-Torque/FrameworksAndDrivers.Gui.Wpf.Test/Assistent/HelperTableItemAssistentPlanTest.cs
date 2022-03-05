using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Threading;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Assistent
{
    class HelperTableItemAssistentPlanTest
    {
        private ListAssistentItemModel<ToolType> _item;
        private HelperTableItemAssistentPlan<ToolType> _plan;

        private ToolType _toolType0;
        private ToolType _toolType1;
        private ToolType _toolType2;

        [SetUp]
        public void HelperTableAssistenPlanSetUp()
		{
			_toolType0 = CreateParametrizedToolType(0, "Item");
			_toolType1 = CreateParametrizedToolType(1, "ItemItem");
			_toolType2 = CreateParametrizedToolType(2, "ItemItemItem");

			var defaultModels = new List<ToolType>() { _toolType0, _toolType1, _toolType2 };

			_item = new ListAssistentItemModel<ToolType>(Dispatcher.CurrentDispatcher, defaultModels, "", "", null, (o, i) => { }, "", x => x.Value.ToDefaultString(), () => { });
			_item.ItemsCollectionView.SortDescriptions.Clear();
			_plan = new HelperTableItemAssistentPlan<ToolType>(null, _item);
		}

		[Test]
        public void ShowToolTypeTest()
		{
			var toolType0 = CreateParametrizedToolType(0, "Item 1");
			var toolType1 = CreateParametrizedToolType(1, "ItemItem 1");
			var toolType2 = CreateParametrizedToolType(2, "ItemItemItem 1");

			_plan.ShowItems(new List<ToolType>() { toolType0, toolType1, toolType2 });

			Assert.AreEqual(GetItemCountInCollection(), 3);
			Assert.AreEqual(GetItemInCollectionViewAt(0).Value.ToDefaultString(), "Item 1");
			Assert.AreEqual(GetItemInCollectionViewAt(1).Value.ToDefaultString(), "ItemItem 1");
			Assert.AreEqual(GetItemInCollectionViewAt(2).Value.ToDefaultString(), "ItemItemItem 1");
		}

		[Test]
        public void AddToolTypeTest()
        {
			var toolType = CreateParametrizedToolType(0, "New Item");

            _plan.Add(toolType);

            Assert.AreEqual(GetItemCountInCollection(), 4);
            Assert.AreEqual(GetItemInCollectionViewAt(3).Value.ToDefaultString(), "New Item");
        }

        [Test]
        public void RemoveToolTypeTest()
		{
			var removeItem = CopyToolType(_toolType1);

			_plan.Remove(removeItem);

			Assert.AreEqual(GetItemCountInCollection(), 2);
			Assert.AreEqual(GetItemInCollectionViewAt(0).Value.ToDefaultString(), "Item");
			Assert.AreEqual(GetItemInCollectionViewAt(1).Value.ToDefaultString(), "ItemItemItem");
		}

		[Test]
        public void SaveToolTypeTest()
        {
			var item = CopyToolType(_toolType2);
			item.Value = new HelperTableDescription("Changed Value");

            _plan.Save(item);

            Assert.AreEqual(GetItemCountInCollection(), 3);
            Assert.AreEqual(GetItemInCollectionViewAt(2).ListId, _toolType2.ListId);
            Assert.AreEqual(GetItemInCollectionViewAt(2).Value.ToDefaultString(), "Changed Value");
        }

		private static ToolType CreateParametrizedToolType(int id, string description)
		{
			return new ToolType() { ListId = new HelperTableEntityId(id), Value = new HelperTableDescription(description) };
		}

		private int GetItemCountInCollection()
		{
			return _item.ItemsCollectionView.Count;
		}

		private ToolType GetItemInCollectionViewAt(int Index)
		{
			return ((_item.ItemsCollectionView.GetItemAt(Index) as DisplayMemberModel<ToolType>).Item as ToolType);
		}

		private static ToolType CopyToolType(ToolType other)
		{
			return new ToolType() { ListId = other.ListId, Value = other.Value };
		}
	}
}
