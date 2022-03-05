using Core;
using Core.Entities;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class ToolModel : BindableBase, IQstEquality<ToolModel>, IUpdate<Tool>, ICopy<ToolModel>
    {
        public Tool Entity { get; private set; }
        private ILocalizationWrapper _localization;


        #region Properties
        public long Id
        {
            get => Entity.Id.ToLong();
            set
            {
                Entity.Id = new ToolId(value);
                RaisePropertyChanged();
            }
        }
        
        public string InventoryNumber
        {
            get => Entity.InventoryNumber?.ToDefaultString();
            set
            {
                Entity.InventoryNumber = new ToolInventoryNumber(value);
                RaisePropertyChanged();
            }
        }
        
        public string SerialNumber
        {
            get => Entity.SerialNumber?.ToDefaultString();
            set
            {
                Entity.SerialNumber = new ToolSerialNumber(value);
                RaisePropertyChanged();
            }
        }
        
        public ToolModelModel ToolModelModel
        {
            get => Entity.ToolModel != null ? ToolModelModel.GetModelFor(Entity.ToolModel, _localization) : null;
            set
            {
                Entity.ToolModel = value?.Entity;
                RaisePropertyChanged();
            }
        }
        
        public HelperTableItemModel<Status, string> Status
        {
            get => Entity.Status != null ? HelperTableItemModel.GetModelForStatus(Entity.Status) : null;
            set
            {
                Entity.Status = value?.Entity;
                RaisePropertyChanged();
            }
        }
        
        public HelperTableItemModel<CostCenter, string> CostCenter
        {
            get => Entity.CostCenter != null ? HelperTableItemModel.GetModelForCostCenter(Entity.CostCenter) : null;
            set
            {
                Entity.CostCenter = value?.Entity;
                RaisePropertyChanged();
            }
        }
        
        public HelperTableItemModel<ConfigurableField, string> ConfigurableField
        {
            get => Entity.ConfigurableField != null ? HelperTableItemModel.GetModelForConfigurableField(Entity.ConfigurableField) : null;
            set
            {
                Entity.ConfigurableField = value?.Entity;
                RaisePropertyChanged();
            }
        }
        
        public string Accessory
        {
            get => Entity.Accessory;
            set
            {
                Entity.Accessory = value;
                RaisePropertyChanged();
            }
        }
        
        public string Comment
        {
            get => Entity.Comment;
            set
            {
                Entity.Comment = value;
                RaisePropertyChanged();
            }
        }
        
        public PictureModel Picture
        {
            get => PictureModel.GetModelFor(Entity.Picture);
            set
            {
                Entity.Picture = value?.Entity;
                RaisePropertyChanged();
            }
        }
        
        public string AdditionalConfigurableField1
        {
            get => Entity.AdditionalConfigurableField1?.ToDefaultString() ?? null;
            set
            {
                Entity.AdditionalConfigurableField1 = value != null ? new ConfigurableFieldString40(value) : null;
                RaisePropertyChanged();
            }
        }
        
        public string AdditionalConfigurableField2
        {
            get => Entity.AdditionalConfigurableField2?.ToDefaultString() ?? null;
            set
            {
                Entity.AdditionalConfigurableField2 = value != null ? new ConfigurableFieldString80(value) : null;
                RaisePropertyChanged();
            }
        }
        
        public string AdditionalConfigurableField3
        {
            get => Entity.AdditionalConfigurableField3?.ToDefaultString() ?? null;
            set
            {
                Entity.AdditionalConfigurableField3 = value != null ? new ConfigurableFieldString250(value) : null;
                RaisePropertyChanged();
            }
        }
        #endregion


        public ToolModel(Tool entity, ILocalizationWrapper localization)
        {
            Entity = entity ?? new Tool();
            _localization = localization;
            RaisePropertyChanged();
        }

        public static ToolModel GetModelFor(Tool entity, ILocalizationWrapper localization)
        {
            return entity != null ? new ToolModel(entity, localization) : null;
        }
        

        public void UpdateWith(Tool other)
        {
            this.Entity.UpdateWith(other);
            RaisePropertyChanged();
        }

        public bool EqualsByContent(ToolModel other)
        {
            return this.Entity.EqualsByContent(other?.Entity);
        }

        public bool EqualsById(ToolModel other)
        {
            return this.Entity.EqualsById(other?.Entity);
        }

        public ToolModel CopyDeep()
        {
            return new ToolModel(Entity.CopyDeep(), _localization);
        }
    }
}
