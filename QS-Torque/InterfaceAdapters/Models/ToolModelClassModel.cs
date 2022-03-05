using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Core.Enums;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class ToolModelClassModel : IGetUpdatedByLanguageChanges, INotifyPropertyChanged
    {
        public ToolModelClass ToolModelClass { get; set; }
        public string TranslatedName => _getTranslatedName();
        private Func<string> _getTranslatedName;

        public ToolModelClassModel(ToolModelClass toolModelClass, Func<string> getTranslatedName)
        {
            _getTranslatedName = getTranslatedName;
            ToolModelClass = toolModelClass;

        }

        public void LanguageUpdate()
        {
            RaisePropertyChanged(nameof(TranslatedName));
        }

        public override bool Equals(object obj)
        {
            if (obj is ToolModelClassModel toolModelClassModel && this.ToolModelClass == toolModelClassModel.ToolModelClass)
            {
                return true;
            }
            return base.Equals(obj);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static ToolModelClassModel CreateToolModelClassModelFromClass(ToolModelClass entityClass,
            ILocalizationWrapper localization)
        {
            switch (entityClass)
            {
                case (ToolModelClass)(-1):
                    return null;
                case ToolModelClass.WrenchScale:
                    var wrenchScaleClassModel = new ToolModelClassModel(entityClass, () => localization.Strings.GetParticularString("ToolModelClass", "Wrench configurable/scale"));
                    localization.Subscribe(wrenchScaleClassModel);
                    return wrenchScaleClassModel;
                case ToolModelClass.WrenchFixSet:
                    var wrenchFixSetClassModel = new ToolModelClassModel(entityClass, () => localization.Strings.GetParticularString("ToolModelClass", "Wrench fix set"));
                    localization.Subscribe(wrenchFixSetClassModel);
                    return wrenchFixSetClassModel;
                case ToolModelClass.WrenchWithoutScale:
                    var wrenchWithoutScaleClassModel = new ToolModelClassModel(entityClass, () => localization.Strings.GetParticularString("ToolModelClass", "Wrench without scale"));
                    localization.Subscribe(wrenchWithoutScaleClassModel);
                    return wrenchWithoutScaleClassModel;
                case ToolModelClass.DriverScale:
                    var driverScaleClassModel = new ToolModelClassModel(entityClass, () => localization.Strings.GetParticularString("ToolModelClass", "Driver scale"));
                    localization.Subscribe(driverScaleClassModel);
                    return driverScaleClassModel;
                case ToolModelClass.DriverFixSet:
                    var driverFixSetClassModel = new ToolModelClassModel(entityClass, () => localization.Strings.GetParticularString("ToolModelClass", "Driver fix set"));
                    localization.Subscribe(driverFixSetClassModel);
                    return driverFixSetClassModel;
                case ToolModelClass.DriverWithoutScale:
                    var driverWithoutScaleClassModel = new ToolModelClassModel(entityClass, () => localization.Strings.GetParticularString("ToolModelClass", "Driver without scale"));
                    localization.Subscribe(driverWithoutScaleClassModel);
                    return driverWithoutScaleClassModel;
                case ToolModelClass.WrenchWithBendingSteelLever:
                    var wrenchWithBendingSteelLever = new ToolModelClassModel(entityClass, () => localization.Strings.GetParticularString("ToolModelClass", "Beam-type torque wrench, adjustable, with scale"));
                    localization.Subscribe(wrenchWithBendingSteelLever);
                    return wrenchWithBendingSteelLever;
                case ToolModelClass.WrenchBendingSteelLever:
                    var wrenchBendingSteelLever = new ToolModelClassModel(entityClass, () => localization.Strings.GetParticularString("ToolModelClass", "Beam-type torque wrench"));
                    localization.Subscribe(wrenchBendingSteelLever);
                    return wrenchBendingSteelLever;
                case ToolModelClass.WrenchWithDialIndicator:
                    var wrenchDialIndicator = new ToolModelClassModel(entityClass, () => localization.Strings.GetParticularString("ToolModelClass", "Wrench with dial indicator"));
                    localization.Subscribe(wrenchDialIndicator);
                    return wrenchDialIndicator;
                case ToolModelClass.WrenchElectronic:
                    var wrenchElectronic = new ToolModelClassModel(entityClass, () => localization.Strings.GetParticularString("ToolModelClass", "Wrench electronic"));
                    localization.Subscribe(wrenchElectronic);
                    return wrenchElectronic;
                case ToolModelClass.DriverWithDialIndicator:
                    var driverDialIndicator = new ToolModelClassModel(entityClass, () => localization.Strings.GetParticularString("ToolModelClass", "Driver with dial indicator"));
                    localization.Subscribe(driverDialIndicator);
                    return driverDialIndicator;
                case ToolModelClass.DriverElectronic:
                    var driverElectronic = new ToolModelClassModel(entityClass, () => localization.Strings.GetParticularString("ToolModelClass", "Driver electronic"));
                    localization.Subscribe(driverElectronic);
                    return driverElectronic;
                default:
                    throw new ArgumentOutOfRangeException(nameof(entityClass), entityClass, null);
            }
        }
    }
}
