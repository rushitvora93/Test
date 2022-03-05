using Client.Core.ChangesGenerators;
using InterfaceAdapters;

namespace InterfaceAdapters.Models
{
    public class SingleValueChangeModel : BindableBase
    {
        public SingleValueChange Entity { get; private set; }

        private int _number;
        /// <summary>
        /// Is set automatically
        /// </summary>
        public int Number
        {
            get => _number;
            set => Set(ref _number, value);
        }

        public string ChangedAttribute
        {
            get => Entity.ChangedAttribute;
            set
            {
                Entity.ChangedAttribute = value;
                RaisePropertyChanged();
            }
        }
        
        public string OldValue
        {
            get => Entity.OldValue;
            set
            {
                Entity.OldValue = value;
                RaisePropertyChanged();
            }
        }
        
        public string NewValue
        {
            get => Entity.NewValue;
            set
            {
                Entity.NewValue = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Just the name of the entity - 
        /// Necessary to group the changes by the entity they belong to
        /// </summary>
        public string AffectedEntity
        {
            get => Entity.AffectedEntity;
            set
            {
                Entity.AffectedEntity = value;
                RaisePropertyChanged();
            }
        }


        public SingleValueChangeModel(SingleValueChange entity)
        {
            Entity = entity ?? new SingleValueChange();
            RaisePropertyChanged();
        }

        public static SingleValueChangeModel GetModelFor(SingleValueChange entity)
        {
            return entity != null ? new SingleValueChangeModel(entity) : null;
        }
    }
}
