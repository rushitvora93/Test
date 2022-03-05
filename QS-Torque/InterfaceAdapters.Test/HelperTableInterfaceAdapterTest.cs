using Core.Entities;
using InterfaceAdapters.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using TestHelper.Factories;
using TestHelper.Mock;

namespace InterfaceAdapters.Test
{
    class HelperTableInterfaceAdapterTest
    {
        [Test]
        public void ShowingHelperTableAddsNewItems()
        {
            var interfaceAdapter = 
                new HelperTableInterface<HelperTableEntityMock, string>(MapEntityToModel, Dispatcher.CurrentDispatcher);
            var newEntities = new List<HelperTableEntityMock>
            {
                CreateHelperTableEntityMock.Anonymous()
            };
            interfaceAdapter.ShowItems(newEntities);
            CollectionAssert.AreEquivalent(newEntities, interfaceAdapter.HelperTableItems.Select(itemModel => itemModel.Entity));
        }

        [Test]
        public void ShowingHelperTableAddsMoreNewItems()
        {
            var interfaceAdapter =
                new HelperTableInterface<HelperTableEntityMock, string>(MapEntityToModel, Dispatcher.CurrentDispatcher);
            var newEntities = new List<HelperTableEntityMock>
            {
                CreateHelperTableEntityMock.Anonymous(),
                CreateHelperTableEntityMock.Anonymous(),
                CreateHelperTableEntityMock.Anonymous(),
                CreateHelperTableEntityMock.Anonymous()
            };
            interfaceAdapter.ShowItems(newEntities);
            CollectionAssert.AreEquivalent(newEntities, interfaceAdapter.HelperTableItems.Select(itemModel => itemModel.Entity));
        }

        [Test]
        public void ShowingHelperTableClearsPreviousContent()
        {
            var interfaceAdapter =
                new HelperTableInterface<HelperTableEntityMock, string>(MapEntityToModel, Dispatcher.CurrentDispatcher);
            var oldEntities = new List<HelperTableEntityMock>
            {
                CreateHelperTableEntityMock.Anonymous(),
                CreateHelperTableEntityMock.Anonymous(),
                CreateHelperTableEntityMock.Anonymous(),
                CreateHelperTableEntityMock.Anonymous()
            };
            oldEntities.ForEach(item => interfaceAdapter.HelperTableItems.Add(MapEntityToModel(item)));
            var newEmptyEntityList = new List<HelperTableEntityMock> { };
            interfaceAdapter.ShowItems(newEmptyEntityList);
            CollectionAssert.AreEquivalent(
                newEmptyEntityList,
                interfaceAdapter.HelperTableItems.Select(itemModel => itemModel.Entity));
        }

        private HelperTableItemModel<HelperTableEntityMock, string> MapEntityToModel(HelperTableEntityMock entity)
        {
            return new HelperTableItemModel<HelperTableEntityMock, string>(
                entity,
                e => e.Description.ToDefaultString(),
                (e, value) => entity.Description = new HelperTableDescription(value),
                () => new HelperTableEntityMock());
        }
    }
}
