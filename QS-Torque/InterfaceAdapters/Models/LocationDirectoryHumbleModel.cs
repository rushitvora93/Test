using Core;
using Core.Entities;
using Core.UseCases;
using System;
using InterfaceAdapters;
using InterfaceAdapters.Models;

namespace InterfaceAdapters.Models
{
    public class LocationDirectoryHumbleModel : BindableBase, IMovableTreeItem, IQstEquality<LocationDirectoryHumbleModel>, IUpdate<LocationDirectory>, ICopy<LocationDirectoryHumbleModel>
    {
        public LocationDirectory Entity { get; private set; }
        

        public long Id
        {
            get => Entity.Id.ToLong();
            set
            {
                Entity.Id = new LocationDirectoryId(value);
                RaisePropertyChanged();
            }
        }

        public string Name
        {
            get => Entity.Name.ToDefaultString();
            set
            {
                Entity.Name = new LocationDirectoryName(value);
                RaisePropertyChanged();
            }
        }

        public long ParentId
        {
            get => Entity.ParentId?.ToLong() ?? -1;
            set
            {
                Entity.ParentId = new LocationDirectoryId(value);
                RaisePropertyChanged();
            }
        }

        private Action<LocationDirectoryHumbleModel, LocationDirectoryId> _changeParentAction;

        public LocationDirectoryHumbleModel(LocationDirectory entity, Action<LocationDirectoryHumbleModel, LocationDirectoryId> changeParentAction)
        {
            Entity = entity ?? new LocationDirectory();
            _changeParentAction = changeParentAction;
            RaisePropertyChanged();
        }

        public LocationDirectoryHumbleModel(LocationDirectory entity)
        {
            Entity = entity;
            RaisePropertyChanged();
        }

        public static LocationDirectoryHumbleModel GetModelFor(LocationDirectory entity, ILocationUseCase useCase)
        {
            return new LocationDirectoryHumbleModel(entity, (model, id) => useCase.ChangeLocationDirectoryParent(model?.Entity, id));
        }

        public void ChangeParent(LocationDirectoryId newParentId)
        {
            _changeParentAction(this, newParentId);
        }

        public bool EqualsById(LocationDirectoryHumbleModel other)
        {
            return this.Entity.EqualsById(other?.Entity);
        }

        public bool EqualsByContent(LocationDirectoryHumbleModel other)
        {
            return this.Entity.EqualsByContent(other?.Entity);
        }

        public void UpdateWith(LocationDirectory other)
        {
            this.Entity.UpdateWith(other);
            RaisePropertyChanged();
        }

        public LocationDirectoryHumbleModel CopyDeep()
        {
            return new LocationDirectoryHumbleModel(Entity.CopyDeep());
        }
    }
}