using Client.Core.ChangesGenerators;
using Client.Core.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapters.Models
{
    public class ValueChangesContainerModel : BindableBase
    {
        public ValueChangesContainer Entity { get; private set; }

        public DateTime TimeStamp
        {
            get => Entity.Timestamp;
            set
            {
                Entity.Timestamp = value;
                RaisePropertyChanged();
            }
        }

        public DiffType Type
        {
            get => Entity.Type;
            set
            {
                Entity.Type = value;
                RaisePropertyChanged();
            }
        }

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
                Entity.Comment = new Core.Entities.HistoryComment(value);
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<SingleValueChangeModel> Changes { get; private set; }


        public ValueChangesContainerModel(ValueChangesContainer entity)
        {
            Entity = entity ?? new ValueChangesContainer();
            Changes = new ObservableCollection<SingleValueChangeModel>(entity.Changes.Select(x => SingleValueChangeModel.GetModelFor(x)));
            RaisePropertyChanged();
        }

        public static ValueChangesContainerModel GetModelFor(ValueChangesContainer entity)
        {
            return entity != null ? new ValueChangesContainerModel(entity) : null;
        }
    }
}
