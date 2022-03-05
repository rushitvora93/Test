using Core.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using Core;

namespace InterfaceAdapters.Models
{
    public class HelperTableItemModelComparer<T, U> : IEqualityComparer<HelperTableItemModel<T, U>> 
        where T : HelperTableEntity, IQstEquality<T>, IUpdate<T>, ICopy<T>
    {
        public bool Equals(HelperTableItemModel<T, U> x, HelperTableItemModel<T, U> y)
        {
            return x.ListId.Equals(y.ListId);
        }

        public int GetHashCode(HelperTableItemModel<T, U> obj)
        {
            return obj.ListId.GetHashCode();
        }
    }


    public abstract class HelperTableItemModel : BindableBase
    {
        public static HelperTableItemModel<ToolType, string> GetModelForToolType(ToolType toolType)
        {
            return toolType != null ?
                new HelperTableItemModel<ToolType, string>(toolType,
                entity => entity.Value.ToDefaultString(),
                (entity, value) => entity.Value = new HelperTableDescription(value),
                () => new ToolType())
                : null;
        }

        public static HelperTableItemModel<SwitchOff, string> GetModelForSwitchOff(SwitchOff switchOff)
        {
            return switchOff != null ? new HelperTableItemModel<SwitchOff, string>(switchOff,
                entity => entity.Value.ToDefaultString(),
                (entity, value) => entity.Value = new HelperTableDescription(value),
                () => new SwitchOff())
                : null;
        }

        public static HelperTableItemModel<DriveSize, string> GetModelForDriveSize(DriveSize driveSize)
        {
            return driveSize != null ?
                new HelperTableItemModel<DriveSize, string>(driveSize,
                entity => entity.Value.ToDefaultString(),
                (entity, value) => entity.Value = new HelperTableDescription(value),
                () => new DriveSize())
                : null;
        }

        public static HelperTableItemModel<ShutOff, string> GetModelForShutOff(ShutOff shutOff)
        {
            return shutOff != null ?
                new HelperTableItemModel<ShutOff, string>(shutOff,
                entity => entity.Value.ToDefaultString(),
                (entity, value) => entity.Value = new HelperTableDescription(value),
                () => new ShutOff())
                : null;
        }

        public static HelperTableItemModel<DriveType, string> GetModelForDriveType(DriveType driveType)
        {
            return driveType != null ?
                new HelperTableItemModel<DriveType, string>(driveType,
                entity => entity.Value.ToDefaultString(),
                (entity, value) => entity.Value = new HelperTableDescription(value),
                () => new DriveType())
                : null;
        }

        public static HelperTableItemModel<ConstructionType, string> GetModelForConstructionType(ConstructionType constructionType)
        {
            return constructionType != null ?
                new HelperTableItemModel<ConstructionType, string>(constructionType,
                entity => entity.Value.ToDefaultString(),
                (entity, value) => entity.Value = new HelperTableDescription(value),
                () => new ConstructionType())
                : null;
        }

        public static HelperTableItemModel<Status, string> GetModelForStatus(Status status)
        {
            return status != null ?
                new HelperTableItemModel<Status, string>(status,
                entity => entity.Value.ToDefaultString(),
                (entity, value) => entity.Value = new StatusDescription(value),
                () => new Status())
                : null;
        }

        public static HelperTableItemModel<CostCenter, string> GetModelForCostCenter(CostCenter costCenter)
        {
            return costCenter != null ?
                new HelperTableItemModel<CostCenter, string>(costCenter,
                entity => entity.Value.ToDefaultString(),
                (entity, value) => entity.Value = new HelperTableDescription(value),
                () => new CostCenter())
                : null;
        }

        public static HelperTableItemModel<ConfigurableField, string> GetModelForConfigurableField(ConfigurableField configurableField)
        {
            return configurableField != null ?
                new HelperTableItemModel<ConfigurableField, string>(configurableField,
                entity => entity.Value.ToDefaultString(),
                (entity, value) => entity.Value = new HelperTableDescription(value),
                () => new ConfigurableField())
                : null;
        }

        public static HelperTableItemModel<ReasonForToolChange, string> GetModelForReasonForToolChange(ReasonForToolChange reasonForToolChange)
        {
            return reasonForToolChange != null ?
                new HelperTableItemModel<ReasonForToolChange, string>(reasonForToolChange,
                entity => entity.Value.ToDefaultString(),
                (entity, value) => entity.Value = new HelperTableDescription(value),
                () => new ReasonForToolChange())
                : null;
        }

        public static HelperTableItemModel<ToolUsage, string> GetModelForToolUsage(ToolUsage toolUsage)
        {
            return toolUsage != null ?
                new HelperTableItemModel<ToolUsage, string>(toolUsage,
                entity => entity.Value.ToDefaultString(),
                (entity, value) => entity.Value = new ToolUsageDescription(value),
                () => new ToolUsage())
                : null;
        }
    }


    public class HelperTableItemModel<T, U> : HelperTableItemModel, IEquatable<HelperTableItemModel<T, U>>, IQstEquality<HelperTableItemModel<T, U>>, IUpdate<T>, ICopy<HelperTableItemModel<T, U>>
        where T : HelperTableEntity, IQstEquality<T>, IUpdate<T>, ICopy<T>
    {
        public T Entity { get; private set; }

        private Func<T, U> _getHelperTableValue;
        private Action<T, U> _setHelperTableValue;
        private Func<T> _createNewHelperTableItem;


        public HelperTableItemModel(T entity, Func<T, U> getHelperTableValue, Action<T, U> setHelperTableValue, Func<T> createNewHelperTableItem)
        {
            Entity = entity ?? createNewHelperTableItem();
            _getHelperTableValue = getHelperTableValue;
            _setHelperTableValue = setHelperTableValue;
            _createNewHelperTableItem = createNewHelperTableItem;
            RaisePropertyChanged();
        }


        public long ListId
        {
            get => Entity.ListId?.ToLong() ?? 0;
            set
            {
                Entity.ListId = new HelperTableEntityId(value);
                RaisePropertyChanged();
            }
        }
        
        public U Value
        {
            get => _getHelperTableValue(Entity);
            set
            {
                HelperTableItemChanged?.Invoke(this, new RoutedPropertyChangedEventArgs<U>(_getHelperTableValue(Entity), value));
                _setHelperTableValue(Entity, value);
                RaisePropertyChanged();
            }
        }


        public HelperTableItemModel<T, U> Copy()
        {
            return new HelperTableItemModel<T, U>(_createNewHelperTableItem(), _getHelperTableValue, _setHelperTableValue, _createNewHelperTableItem)
            {
                ListId = ListId,
                Value = Value
            };
        }
        
        // Event (Invoked if Value has been changed)
        public event RoutedPropertyChangedEventHandler<U> HelperTableItemChanged;

        public bool Equals(HelperTableItemModel<T, U> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.EqualsById(other);
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((HelperTableItemModel<T, U>) obj);
        }

        public override int GetHashCode()
        {
            return ListId.GetHashCode();
        }

        public bool EqualsById(HelperTableItemModel<T, U> other)
        {
            return this.Entity.EqualsById(other?.Entity);
        }

        public bool EqualsByContent(HelperTableItemModel<T, U> other)
        {
            return this.Entity.EqualsByContent(other?.Entity);
        }

        public void UpdateWith(T other)
        {
            this.Entity.UpdateWith(other);
            RaisePropertyChanged();
        }

        public HelperTableItemModel<T, U> CopyDeep()
        {
            return new HelperTableItemModel<T, U>(this.Entity.CopyDeep(), _getHelperTableValue, _setHelperTableValue, _createNewHelperTableItem);
        }
    }
}
