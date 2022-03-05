using Core.Diffs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapters.Models.Diffs
{
    public class LocationDiffModel : BindableBase
    {
        public LocationDiff Entity { get; private set; }

        public UserModel User
        {
            get => UserModel.GetModelFor(Entity.User);
            set
            {
                Entity.User = value.Entity;
                RaisePropertyChanged();
            }
        }

        public string Comment
        {
            get => Entity.Comment.ToDefaultString();
            set
            {
                Entity.Comment = new HistoryComment(value);
                RaisePropertyChanged();
            }
        }

        public LocationModel OldLocation
        {
            get => LocationModel.GetModelFor(Entity.OldLocation, null, null);
            set
            {
                Entity.OldLocation = value.Entity;
                RaisePropertyChanged();
            }
        }

        public LocationModel NewLocation
        {
            get => LocationModel.GetModelFor(Entity.NewLocation, null, null);
            set
            {
                Entity.NewLocation = value.Entity;
                RaisePropertyChanged();
            }
        }

        public LocationDiffModel(LocationDiff entity)
        {
            Entity = entity ?? new LocationDiff();
        }

        public static LocationDiffModel GetModelFor(LocationDiff entity)
        {
            return entity != null ? new LocationDiffModel(entity) : null;
        }
    }
}
