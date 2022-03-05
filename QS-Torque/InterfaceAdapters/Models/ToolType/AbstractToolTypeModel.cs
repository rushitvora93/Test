using System;
using Core.Entities.ToolTypes;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public abstract class AbstractToolTypeModel : BindableBase, IGetUpdatedByLanguageChanges, IEquatable<AbstractToolTypeModel>
    {
        protected ILocalizationWrapper _localizationWrapper;
        public string Name => _toolType?.Name ?? "";

        public abstract string TranslatedName { get; }
        private AbstractToolType _toolType;

        protected AbstractToolTypeModel(ILocalizationWrapper wrapper, AbstractToolType toolType  = null)
        {
            _localizationWrapper = wrapper;
            _localizationWrapper.Subscribe(this);
            _toolType = toolType;
        }

        private static ClickWrenchModel _clickWrenchModel;
        private static ECDriverModel _ecDriverModel;
        private static GeneralModel _generalModel;
        private static MDWrenchModel _mdWrenchModel;
        private static ProductionWrenchModel _productionWrenchModel;
        private static PulseDriverModel _pulseDriverModel;
        private static PulseDriverShutOffModel _pulseDriverShutOffModel;
        public static AbstractToolTypeModel MapToolTypeToToolTypeModel(AbstractToolType toolType, ILocalizationWrapper wrapper)
        {
            if (_clickWrenchModel == null)
            {
                _clickWrenchModel = new ClickWrenchModel(wrapper, new ClickWrench());
            }
            if (_ecDriverModel == null)
            {
                _ecDriverModel = new ECDriverModel(wrapper, new ECDriver());
            }
            if (_generalModel == null)
            {
                _generalModel = new GeneralModel(wrapper, new General());
            }
            if (_mdWrenchModel == null)
            {
                _mdWrenchModel = new MDWrenchModel(wrapper, new MDWrench());
            }
            if (_productionWrenchModel == null)
            {
                _productionWrenchModel = new ProductionWrenchModel(wrapper, new ProductionWrench());
            }
            if (_pulseDriverModel == null)
            {
                _pulseDriverModel = new PulseDriverModel(wrapper, new PulseDriver());
            }
            if (_pulseDriverShutOffModel == null)
            {
                _pulseDriverShutOffModel = new PulseDriverShutOffModel(wrapper, new PulseDriverShutOff());
            }

            switch (toolType)
            {
                case ClickWrench clickWrenchModel:
                    return _clickWrenchModel;
                case ECDriver ecDriverModel:
                    return _ecDriverModel;
                case General generalModel:
                    return _generalModel;
                case MDWrench mdWrenchModel:
                    return _mdWrenchModel;
                case ProductionWrench productionWrenchModel:
                    return _productionWrenchModel;
                case PulseDriver pulseDriverModel:
                    return _pulseDriverModel;
                case PulseDriverShutOff pulseDriverShutOffModel:
                    return _pulseDriverShutOffModel;
                default:
                    return null;
            }
        }

        public static AbstractToolType MapToolTypeModelToToolType(AbstractToolTypeModel model)
        {
            switch (model)
            {
                case ClickWrenchModel clickWrenchModel:
                    return new ClickWrench();
                case ECDriverModel ecDriverModel:
                    return new ECDriver();
                case GeneralModel generalModel:
                    return new General();
                case MDWrenchModel mdWrenchModel:
                    return new MDWrench();
                case ProductionWrenchModel productionWrenchModel:
                    return new ProductionWrench();
                case PulseDriverModel pulseDriverModel:
                    return new PulseDriver();
                case PulseDriverShutOffModel pulseDriverShutOffModel:
                    return new PulseDriverShutOff();
                default:
                    return null;
            }
        }

        public bool ShouldPropertyBeVisible(string propertyName)
        {
            return _toolType.DoesToolTypeHasProperty(propertyName);
        }

        public void LanguageUpdate()
        {
            RaisePropertyChanged(nameof(TranslatedName));
        }
        public bool Equals(AbstractToolTypeModel other)
        {
            return other != null && this.GetType() == other.GetType();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AbstractToolTypeModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_localizationWrapper != null ? _localizationWrapper.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_toolType != null ? _toolType.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}