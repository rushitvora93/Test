using Core.Entities;
using Core.Enums;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.Translation;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using FrameworksAndDrivers.Localization;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.ToolTypes;
using FrameworksAndDrivers.Gui.Wpf;

namespace StartUp.AssistentCreator
{
    class AddToolModelAssistentCreator
    {
        Core.Entities.ToolModel _defaultToolModel;
        IStartUp _startUp;
        LocalizationWrapper _localization;
        UseCaseCollection _useCases;


        public AssistentView CreateAddToolModelAssistent()
        {
            var assistentView = new AssistentView(_localization.Strings.GetParticularString("Add tool model assistent", "Add new tool model"));

            var manufacturerPlan = CreateManufacturerAssistentPlan(assistentView);
            var toolModelTypePlan = CreateToolModelTypeAssistentPlan(assistentView);
            var toolModelClassPlanClickWrench = CreateToolModelClassAssistentPlan(assistentView, new List<ToolModelClass>()
            {
                ToolModelClass.WrenchScale,
                ToolModelClass.WrenchFixSet,
                ToolModelClass.WrenchWithoutScale,
                ToolModelClass.DriverScale,
                ToolModelClass.DriverFixSet,
                ToolModelClass.DriverWithoutScale,
                ToolModelClass.WrenchWithBendingSteelLever
            });

            var toolModelClassPlanMdWrenchProductionWrench = CreateToolModelClassAssistentPlan(assistentView, new List<ToolModelClass>()
            {
                ToolModelClass.WrenchBendingSteelLever,
                ToolModelClass.WrenchWithDialIndicator,
                ToolModelClass.WrenchElectronic,
                ToolModelClass.DriverWithDialIndicator,
                ToolModelClass.DriverElectronic
            });
            var airPressurePlan = CreateAirPressurePlan();
            var airConsumptionPlan = CreateAirConsumptionPlan();
            var batteryVoltagePlan = CreateBatteryVoltagePlan();
            var maxRotationSpeedPlan = CreateMaxRotationSpeedPlan();
            var toolTypePlan = CreateToolTypePlan(assistentView);
            var switchOffPlan = CreateSwitchOffPlan(assistentView);
            var shutOffPlan = CreateShutOffPlan(assistentView);
            var driveSizePlan = CreateDriveSizePlan(assistentView);
            var driveTypePlan = CreateDriveTypePlan(assistentView);
            var constructionTypePlan = CreateConstructionTypePlan(assistentView);
            var minPowerPlan = CreateMinPowerPlan();

            // MinValueValidationBehaviour for MaxPowerPlan
            var maxPowerValidationBehaviour = new MinValueValidationBehavior()
            {
                MinValue = "0",
                ValueType = ValidationBehavior.ValidationType.FloatingPoint,
                WarningText = _localization.Strings.GetParticularString("Add tool model assistent", "The value has to be grather than or equal to 0 and grather than the min power")
            };

            var maxPowerPlan = CreateMaxPowerPlan(maxPowerValidationBehaviour);

            // Wire min power to the max power validation
            minPowerPlan.AssistentItem.PropertyChanged += (s, e) =>
            {
                // Update the min value for max power if the entered value has changed
                if (e.PropertyName == nameof(AssistentItemModel<double>.EnteredValue))
                {
                    maxPowerValidationBehaviour.MinValue = (s as AssistentItemModel<double>).EnteredValue.ToString();
                }
            };

            var parentPlan = new ParentAssistentPlan(new List<ParentAssistentPlan>()
            {
                new AssistentPlan<string>(new UniqueAssistentItemModel<string>(x => _useCases.toolModel.IsToolModelDesciptionUnique(x),
                                                                               AssistentItemType.Text,
                                                                               _localization.Strings.GetParticularString("Add tool model assistent", "Enter the description"),
                                                                               _localization.Strings.GetParticularString("Add tool model assistent", "Description"),
                                                                               _defaultToolModel?.Description?.ToDefaultString() ?? "",
                                                                               (o, i) => { (o as Core.Entities.ToolModel).Description = new ToolModelDescription((i as AssistentItemModel<string>).EnteredValue); },
                                                                               errorText: _localization.Strings.GetParticularString("Add tool model assistent", "This field is required and has to be unique"),
                                                                               errorCheck: x => string.IsNullOrWhiteSpace((x as AssistentItemModel<string>).EnteredValue),
                                                                               maxLengthText: 40)),
                manufacturerPlan,
                toolModelTypePlan,

                new ConditionalAssistentPlan(new List<ParentAssistentPlan>(){ toolModelClassPlanClickWrench },
                                             () => toolModelTypePlan.AssistentItem.EnteredValue is ClickWrench && toolModelTypePlan.AssistentItem.EnteredValue.DoesToolTypeHasProperty(nameof(Core.Entities.ToolModel.Class))),
                new ConditionalAssistentPlan(new List<ParentAssistentPlan>(){ toolModelClassPlanMdWrenchProductionWrench },
                                    () => (toolModelTypePlan.AssistentItem.EnteredValue is MDWrench || toolModelTypePlan.AssistentItem.EnteredValue is ProductionWrench )&& toolModelTypePlan.AssistentItem.EnteredValue.DoesToolTypeHasProperty(nameof(Core.Entities.ToolModel.Class))),
                new ConditionalAssistentPlan(new List<ParentAssistentPlan>(){ airPressurePlan },
                                             () => toolModelTypePlan.AssistentItem.EnteredValue != null && toolModelTypePlan.AssistentItem.EnteredValue.DoesToolTypeHasProperty(nameof(Core.Entities.ToolModel.AirPressure))),
                new ConditionalAssistentPlan(new List<ParentAssistentPlan>(){ airConsumptionPlan },
                                             () => toolModelTypePlan.AssistentItem.EnteredValue != null && toolModelTypePlan.AssistentItem.EnteredValue.DoesToolTypeHasProperty(nameof(Core.Entities.ToolModel.AirConsumption))),
                new ConditionalAssistentPlan(new List<ParentAssistentPlan>(){ batteryVoltagePlan },
                                             () => toolModelTypePlan.AssistentItem.EnteredValue != null && toolModelTypePlan.AssistentItem.EnteredValue.DoesToolTypeHasProperty(nameof(Core.Entities.ToolModel.BatteryVoltage))),
                new ConditionalAssistentPlan(new List<ParentAssistentPlan>(){ maxRotationSpeedPlan },
                                             () => toolModelTypePlan.AssistentItem.EnteredValue != null && toolModelTypePlan.AssistentItem.EnteredValue.DoesToolTypeHasProperty(nameof(Core.Entities.ToolModel.MaxRotationSpeed))),
                minPowerPlan,
                maxPowerPlan,
                toolTypePlan,
                new AssistentPlan<double>(new AssistentItemModel<double>(AssistentItemType.FloatingPoint,
                                                                         _localization.Strings.GetParticularString("Add tool model assistent", "Enter the weight"),
                                                                         _localization.Strings.GetParticularString("Add tool model assistent", "Weight"),
                                                                         _defaultToolModel?.Weight ?? 0.0,
                                                                         (o, i) => { (o as Core.Entities.ToolModel).Weight = (i as AssistentItemModel<double>).EnteredValue; },
                                                                         _localization.Strings.GetParticularString("Unit", "kg"))),
                switchOffPlan,
                shutOffPlan,
                driveSizePlan,
                driveTypePlan,
                constructionTypePlan
            });

            _useCases.manufacturerGuiAdapter.RegisterGuiInterface(manufacturerPlan);
            _useCases.toolTypeGuiAdapter.RegisterGuiInterface(toolTypePlan);
            _useCases.switchOffGuiAdapter.RegisterGuiInterface(switchOffPlan);
            _useCases.shutOffGuiAdapter.RegisterGuiInterface(shutOffPlan);
            _useCases.driveSizeGuiAdapter.RegisterGuiInterface(driveSizePlan);
            _useCases.driveTypeGuiAdapter.RegisterGuiInterface(driveTypePlan);
            _useCases.constructionTypeGuiAdapter.RegisterGuiInterface(constructionTypePlan);

            assistentView.SetParentPlan(parentPlan);

            return assistentView;
        }


        #region Special AssistentPlan Creator
        private ManufacturerAssistentPlan CreateManufacturerAssistentPlan(AssistentView assistentView)
        {
            return new ManufacturerAssistentPlan(
                _useCases.manufacturer,
                new ListAssistentItemModel<Manufacturer>(assistentView.Dispatcher,
                                                         _localization.Strings.GetParticularString("Add tool model assistent", "Choose the manufacturer"),
                                                         _localization.Strings.GetParticularString("Add tool model assistent", "Manufacturer"),
                                                         null,
                                                         (o, i) => { (o as Core.Entities.ToolModel).Manufacturer = (i as ListAssistentItemModel<Manufacturer>).EnteredValue; },
                                                         _localization.Strings.GetParticularString("Add tool model assistent", "Jump to manufacturer"),
                                                         (manu) => manu.Name.ToDefaultString(),
                                                         () =>
                                                         {
                                                             _startUp.OpenManufacturerDialog(assistentView);
                                                         }),
                _defaultToolModel?.Manufacturer?.Id ?? null);
        }

        private ListAssistentPlan<AbstractToolType> CreateToolModelTypeAssistentPlan(AssistentView assistentView)
        {
            var toolModelTypeList = new List<AbstractToolType>()
            {
                new ClickWrench(),
                new ECDriver(),
                new General(),
                new MDWrench(),
                new ProductionWrench(),
                new PulseDriver(),
                new PulseDriverShutOff(),
            };
            var toolModelTypePlan = new ListAssistentPlan<AbstractToolType>(
                new ListAssistentItemModel<AbstractToolType>(
                    assistentView.Dispatcher,
                    toolModelTypeList,
                    _localization.Strings.GetParticularString("Add tool model assistent", "Choose a tool model type"),
                    _localization.Strings.GetParticularString("Add tool model assistent", "Tool model type"),
                    _defaultToolModel == null ? null : toolModelTypeList.FirstOrDefault(x => x.GetType() == _defaultToolModel.ModelType.GetType()),
                    (o, i) => { (o as Core.Entities.ToolModel).ModelType = (i as ListAssistentItemModel<AbstractToolType>).EnteredValue; },
                    "",
                    (type) =>
                    {
                        switch (type)
                        {
                            case ClickWrench clickWrench: return _localization.Strings.GetParticularString("ToolModelType", "Click wrench");
                            case ECDriver ecDriver: return _localization.Strings.GetParticularString("ToolModelType", "EC driver");
                            case General general: return _localization.Strings.GetParticularString("ToolModelType", "General");
                            case MDWrench mdWrench: return _localization.Strings.GetParticularString("ToolModelType", "MD wrench");
                            case ProductionWrench productionWrench: return _localization.Strings.GetParticularString("ToolModelType", "Production wrench");
                            case PulseDriver pulseDriver: return _localization.Strings.GetParticularString("ToolModelType", "Pulse driver");
                            case PulseDriverShutOff pulseDriverShutOff: return _localization.Strings.GetParticularString("ToolModelType", "Pulse driver shut off");
                            default: return "";
                        }
                    },
                    () => { }));
            // Set default value
            if (_defaultToolModel != null)
            {
                toolModelTypePlan.AssistentItem.SetDefaultValue(toolModelTypeList.FirstOrDefault(x => x.GetType() == _defaultToolModel.ModelType.GetType()));
            }

            return toolModelTypePlan;
        }

        private ListAssistentPlan<ToolModelClass> CreateToolModelClassAssistentPlan(AssistentView assistentView, List<ToolModelClass> classes)
        {
            return new ListAssistentPlan<ToolModelClass>(
                new ListAssistentItemModel<ToolModelClass>(
                    assistentView.Dispatcher,
                    classes,
                    _localization.Strings.GetParticularString("Add tool model assistent", "Choose a tool model class"),
                    _localization.Strings.GetParticularString("Add tool model assistent", "Tool model class"),
                    _defaultToolModel?.Class ?? ToolModelClass.WrenchScale,
                    (o, i) => { (o as Core.Entities.ToolModel).Class = (i as ListAssistentItemModel<ToolModelClass>).EnteredValue; },
                    "",
                    (toolModelClass) => ToolModelClassTranslation.GetTranlationForToolModelClass(toolModelClass, _localization),
                    () => { }));
        }

        private AssistentPlan<double> CreateAirPressurePlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(AssistentItemType.FloatingPoint,
                                               _localization.Strings.GetParticularString("Add tool model assistent", "Enter the air pressure"),
                                               _localization.Strings.GetParticularString("Add tool model assistent", "Air pressure"),
                                               _defaultToolModel?.AirPressure ?? 0.0,
                                               (o, i) => { (o as Core.Entities.ToolModel).AirPressure = (i as AssistentItemModel<double>).EnteredValue; },
                                               _localization.Strings.GetParticularString("Unit", "bar")));
        }

        private AssistentPlan<double> CreateAirConsumptionPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(AssistentItemType.FloatingPoint,
                                               _localization.Strings.GetParticularString("Add tool model assistent", "Enter the air consumption"),
                                               _localization.Strings.GetParticularString("Add tool model assistent", "Air consumption"),
                                               _defaultToolModel?.AirConsumption ?? 0.0,
                                               (o, i) => { (o as Core.Entities.ToolModel).AirConsumption = (i as AssistentItemModel<double>).EnteredValue; }));
        }

        private AssistentPlan<double> CreateBatteryVoltagePlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(AssistentItemType.FloatingPoint,
                                               _localization.Strings.GetParticularString("Add tool model assistent", "Enter the battery voltage"),
                                               _localization.Strings.GetParticularString("Add tool model assistent", "Battery voltage"),
                                               _defaultToolModel?.BatteryVoltage ?? 0.0,
                                               (o, i) => { (o as Core.Entities.ToolModel).BatteryVoltage = (i as AssistentItemModel<double>).EnteredValue; },
                                               _localization.Strings.GetParticularString("Unit", "V")));
        }

        private AssistentPlan<long> CreateMaxRotationSpeedPlan()
        {
            return new AssistentPlan<long>(
                new AssistentItemModel<long>(AssistentItemType.Numeric,
                                             _localization.Strings.GetParticularString("Add tool model assistent", "Enter the max. rotation speed"),
                                             _localization.Strings.GetParticularString("Add tool model assistent", "Max. rotation speed"),
                                             _defaultToolModel?.MaxRotationSpeed ?? 0,
                                             (o, i) => { (o as Core.Entities.ToolModel).MaxRotationSpeed = (i as AssistentItemModel<long>).EnteredValue; },
                                             _localization.Strings.GetParticularString("Unit", "U/min")));
        }

        private HelperTableItemAssistentPlan<ToolType> CreateToolTypePlan(AssistentView assistentView)
        {
            return new HelperTableItemAssistentPlan<ToolType>(_useCases.toolType,
                    new ListAssistentItemModel<ToolType>(assistentView.Dispatcher,
                                                         _localization.Strings.GetParticularString("Add tool model assistent", "Choose the tool type"),
                                                         _localization.Strings.GetParticularString("Add tool model assistent", "Tool type"),
                                                         null,
                                                         (o, i) => { (o as Core.Entities.ToolModel).ToolType = (i as ListAssistentItemModel<ToolType>).EnteredValue; },
                                                         _localization.Strings.GetParticularString("Add tool model assistent", "Jump to tool type"),
                                                         item => item.Value.ToDefaultString(),
                                                         () =>
                                                         {
                                                             _startUp.OpenToolTypeHelperTableDialog(assistentView);
                                                         }),
                    _defaultToolModel?.ToolType?.ListId ?? null);
        }

        private HelperTableItemAssistentPlan<SwitchOff> CreateSwitchOffPlan(AssistentView assistentView)
        {
            return new HelperTableItemAssistentPlan<SwitchOff>(_useCases.switchOff,
                    new ListAssistentItemModel<SwitchOff>(assistentView.Dispatcher,
                                                          _localization.Strings.GetParticularString("Add tool model assistent", "Choose the switch off"),
                                                          _localization.Strings.GetParticularString("Add tool model assistent", "Switch off"),
                                                          null,
                                                          (o, i) => { (o as Core.Entities.ToolModel).SwitchOff = (i as ListAssistentItemModel<SwitchOff>).EnteredValue; },
                                                          _localization.Strings.GetParticularString("Add tool model assistent", "Jump to switch off"),
                                                          item => item.Value.ToDefaultString(),
                                                          () =>
                                                          {
                                                              _startUp.OpenSwitchOffHelperTableDialog(assistentView);
                                                          }),
                    _defaultToolModel?.SwitchOff?.ListId ?? null);
        }

        private HelperTableItemAssistentPlan<ShutOff> CreateShutOffPlan(AssistentView assistentView)
        {
            return new HelperTableItemAssistentPlan<ShutOff>(_useCases.shutOff,
                    new ListAssistentItemModel<ShutOff>(assistentView.Dispatcher,
                                                        _localization.Strings.GetParticularString("Add tool model assistent", "Choose the Shut off"),
                                                        _localization.Strings.GetParticularString("Add tool model assistent", "Shut off"),
                                                        null,
                                                        (o, i) => { (o as Core.Entities.ToolModel).ShutOff = (i as ListAssistentItemModel<ShutOff>).EnteredValue; },
                                                        _localization.Strings.GetParticularString("Add tool model assistent", "Jump to shut off"),
                                                        item => item.Value.ToDefaultString(),
                                                        () =>
                                                        {
                                                            _startUp.OpenShutOffHelperTableDialog(assistentView);
                                                        }),
                    _defaultToolModel?.ShutOff?.ListId ?? null);
        }

        private HelperTableItemAssistentPlan<DriveSize> CreateDriveSizePlan(AssistentView assistentView)
        {
            return new HelperTableItemAssistentPlan<DriveSize>(_useCases.driveSize,
                    new ListAssistentItemModel<DriveSize>(assistentView.Dispatcher,
                                                          _localization.Strings.GetParticularString("Add tool model assistent", "Choose the drive size"),
                                                          _localization.Strings.GetParticularString("Add tool model assistent", "Drive size"),
                                                          null,
                                                          (o, i) => { (o as Core.Entities.ToolModel).DriveSize = (i as ListAssistentItemModel<DriveSize>).EnteredValue; },
                                                          _localization.Strings.GetParticularString("Add tool model assistent", "Jump to drive size"),
                                                          item => item.Value.ToDefaultString(),
                                                          () =>
                                                          {
                                                              _startUp.OpenDriveSizeHelperTableDialog(assistentView);
                                                          }),
                    _defaultToolModel?.DriveSize?.ListId ?? null);
        }

        private HelperTableItemAssistentPlan<DriveType> CreateDriveTypePlan(AssistentView assistentView)
        {
            return new HelperTableItemAssistentPlan<DriveType>(_useCases.driveType,
                    new ListAssistentItemModel<DriveType>(assistentView.Dispatcher,
                                                          _localization.Strings.GetParticularString("Add tool model assistent", "Choose the drive type"),
                                                          _localization.Strings.GetParticularString("Add tool model assistent", "Drive type"),
                                                          null,
                                                          (o, i) => { (o as Core.Entities.ToolModel).DriveType = (i as ListAssistentItemModel<DriveType>).EnteredValue; },
                                                          _localization.Strings.GetParticularString("Add tool model assistent", "Jump to drive type"),
                                                          item => item.Value.ToDefaultString(),
                                                          () =>
                                                          {
                                                              _startUp.OpenDriveTypeHelperTableDialog(assistentView);
                                                          }),
                    _defaultToolModel?.DriveType?.ListId ?? null);
        }

        private HelperTableItemAssistentPlan<ConstructionType> CreateConstructionTypePlan(AssistentView assistentView)
        {
            return new HelperTableItemAssistentPlan<ConstructionType>(_useCases.constructionType,
                    new ListAssistentItemModel<ConstructionType>(assistentView.Dispatcher,
                                                                 _localization.Strings.GetParticularString("Add tool model assistent", "Choose the construction type"),
                                                                 _localization.Strings.GetParticularString("Add tool model assistent", "Construction type"),
                                                                 null,
                                                                 (o, i) => { (o as Core.Entities.ToolModel).ConstructionType = (i as ListAssistentItemModel<ConstructionType>).EnteredValue; },
                                                                 _localization.Strings.GetParticularString("Add tool model assistent", "Jump to construction type"),
                                                                 item => item.Value.ToDefaultString(),
                                                                 () =>
                                                                 {
                                                                     _startUp.OpenConstructionTypeHelperTableDialog(assistentView);
                                                                 }),
                    _defaultToolModel?.ConstructionType?.ListId ?? null);
        }

        private AssistentPlan<double> CreateMinPowerPlan()
        {
            return new AssistentPlan<double>(new AssistentItemModel<double>(AssistentItemType.FloatingPoint,
                                             _localization.Strings.GetParticularString("Add tool model assistent", "Enter the lower power limit"),
                                             _localization.Strings.GetParticularString("Add tool model assistent", "Min. power"),
                                             _defaultToolModel?.MinPower ?? 0.0,
                                             (o, i) => { (o as Core.Entities.ToolModel).MinPower = (i as AssistentItemModel<double>).EnteredValue; },
                                             _localization.Strings.GetParticularString("Unit", "Nm"),
                                             behaviors: new List<Microsoft.Xaml.Behaviors.Behavior>()
                                             {
                                                 new MinValueValidationBehavior()
                                                 {
                                                     MinValue = "0",
                                                     ValueType = ValidationBehavior.ValidationType.FloatingPoint,
                                                     WarningText = _localization.Strings.GetParticularString("Add tool model assistent", "The value has to be grather than or equal to 0")
                                                 }
                                             },
                                             errorText: _localization.Strings.GetParticularString("Add tool model assistent", "The value has to be grather than or equal to 0"),
                                             errorCheck: x => (x as AssistentItemModel<double>).EnteredValue < 0));
        }

        private AssistentPlan<double> CreateMaxPowerPlan(MinValueValidationBehavior maxPowerValidation)
        {
            return  new AssistentPlan<double>(new AssistentItemModel<double>(AssistentItemType.FloatingPoint,
                                              _localization.Strings.GetParticularString("Add tool model assistent", "Enter the upper power limit"),
                                              _localization.Strings.GetParticularString("Add tool model assistent", "Max. power"),
                                              _defaultToolModel?.MaxPower ?? 0,
                                              (o, i) => { (o as Core.Entities.ToolModel).MaxPower = (i as AssistentItemModel<double>).EnteredValue; },
                                              _localization.Strings.GetParticularString("Unit", "Nm"),
                                              behaviors: new List<Microsoft.Xaml.Behaviors.Behavior>() { maxPowerValidation },
                                              errorText: _localization.Strings.GetParticularString("Add tool model assistent", "The value has to be grather than or equal to 0 and grather than the min power"),
                                              errorCheck: x => (x as AssistentItemModel<double>).EnteredValue < 0));
        }
        #endregion


        public AddToolModelAssistentCreator(Core.Entities.ToolModel defaultToolModel, StartUpImpl startUp, LocalizationWrapper localization, UseCaseCollection useCases)
        {
            _defaultToolModel = defaultToolModel;
            _startUp = startUp;
            _localization = localization;
            _useCases = useCases;
        }
    }
}
