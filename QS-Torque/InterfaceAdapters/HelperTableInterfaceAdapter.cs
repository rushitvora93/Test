using Core;
using Core.Entities;
using Core.UseCases;
using InterfaceAdapters.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace InterfaceAdapters
{
    public class HelperTableInterfaceAdapter<T>
        : IHelperTableGui<T>
        , IInterfaceAdapter<IHelperTableGui<T>>
    {
        public void Add(T newItem)
        {
            _fullInterfaces.InvokeActionOnGuiInterfaces(gui => gui.Add(newItem));
        }

        public void Remove(T removeItem)
        {
            _fullInterfaces.InvokeActionOnGuiInterfaces(gui => gui.Remove(removeItem));
        }

        public void Save(T savedItem)
        {
            _fullInterfaces.InvokeActionOnGuiInterfaces(gui => gui.Save(savedItem));
        }

        public void ShowItems(List<T> items)
        {
            _fullInterfaces.InvokeActionOnGuiInterfaces(gui => gui.ShowItems(items));
        }

        public void RegisterGuiInterface(IHelperTableGui<T> helperTableGui)
        {
            _fullInterfaces.RegisterGuiInterface(helperTableGui);
        }

        public void RemoveGuiInterface(IHelperTableGui<T> guiInterface)
        {
            _fullInterfaces.RemoveGuiInterface(guiInterface);
        }

        private readonly InterfaceAdapter<IHelperTableGui<T>>  _fullInterfaces = new InterfaceAdapter<IHelperTableGui<T>>();
    }

    public interface IHelperTableInterface<HelperTableEntityType, HelperTableModelType>
        where
            HelperTableEntityType
                : HelperTableEntity
                , IQstEquality<HelperTableEntityType>
                , IUpdate<HelperTableEntityType>
                , ICopy<HelperTableEntityType>
    {
        ObservableCollection<HelperTableItemModel<HelperTableEntityType, HelperTableModelType>> HelperTableItems { get; }
    }

    public class HelperTableInterface<HelperTableEntityType, HelperTableValueType>
        : BindableBase
        , IHelperTableGui<HelperTableEntityType>
        , IHelperTableInterface<HelperTableEntityType, HelperTableValueType>
        where
            HelperTableEntityType
                : HelperTableEntity
                , IQstEquality<HelperTableEntityType>
                , IUpdate<HelperTableEntityType>
                , ICopy<HelperTableEntityType>
    {
        public HelperTableInterface(
            Func<HelperTableEntityType, HelperTableItemModel<HelperTableEntityType, HelperTableValueType>> mapModelToEntity,
            Dispatcher guiDispatcher)
        {
            HelperTableItems = new ObservableCollection<HelperTableItemModel<HelperTableEntityType, HelperTableValueType>>();
            _mapModelToEntity = mapModelToEntity;
            _guiDispatcher = guiDispatcher;
        }

        public void Add(HelperTableEntityType newItem)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(HelperTableEntityType removeItem)
        {
            throw new System.NotImplementedException();
        }

        public void Save(HelperTableEntityType savedItem)
        {
            throw new System.NotImplementedException();
        }

        public void ShowItems(List<HelperTableEntityType> items)
        {
            _guiDispatcher.Invoke(() =>
            {
                HelperTableItems.Clear();
                items.ForEach(item => HelperTableItems.Add(_mapModelToEntity(item)));
            });
        }

        public ObservableCollection<HelperTableItemModel<HelperTableEntityType, HelperTableValueType>> HelperTableItems { get; }
        public HelperTableItemModel<HelperTableEntityType, HelperTableValueType> SelectedItem;

        private readonly Func<HelperTableEntityType, HelperTableItemModel<HelperTableEntityType, HelperTableValueType>> _mapModelToEntity;
        private readonly Dispatcher _guiDispatcher;
    }
}
