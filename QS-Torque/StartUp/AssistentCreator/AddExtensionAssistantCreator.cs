using System.Collections.Generic;
using System.Globalization;
using Common.Types.Enums;
using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Gui.Wpf.View.Behaviors;
using FrameworksAndDrivers.Localization;
using Microsoft.Xaml.Behaviors;

namespace StartUp.AssistentCreator
{
    public class AddExtensionAssistantCreator
    {
        private readonly Extension _defaultExtension;
        private readonly StartUpImpl _startUp;
        private readonly LocalizationWrapper _localization;
        private readonly UseCaseCollection _useCases;

        public AddExtensionAssistantCreator(Core.Entities.Extension defaultExtension, StartUpImpl startUp, LocalizationWrapper localization, UseCaseCollection useCases)
        {
            _defaultExtension = defaultExtension;
            _startUp = startUp;
            _localization = localization;
            _useCases = useCases;
        }

        public AssistentView CreateAddExtensionAssistant()
        {
            var view =
                new AssistentView(
                    _localization.Strings.GetParticularString("Add extension assistant", "Add new extension"));

            var extensionCorrection = CreateExtensionCorrectionPlan(view);
            var gauge = CreateGaugeAssistantPlan();
            var factor = CreateFactorAssistantPlan();

            WireFactorGauge(gauge.AssistentItem, factor.AssistentItem);

            var parentPlan = new ParentAssistentPlan(new List<ParentAssistentPlan>()
            {
                CreateInventoryNumberAssistantPlan(),
                CreateDescriptionAssistantPlan(),
                extensionCorrection,
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>() { gauge },
                    () => extensionCorrection.AssistentItem.EnteredValue == ExtensionCorrection.UseGauge),
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>() { factor },
                    () => extensionCorrection.AssistentItem.EnteredValue == ExtensionCorrection.UseFactor),
                CreateBendingAssistantPlan()
            });

            view.SetParentPlan(parentPlan);
            return view;
        }

        private void WireFactorGauge(AssistentItemModel<double> gaugeAssistentItem, AssistentItemModel<double> factorAssistentItem)
        {
            var gaugeErrorText = _localization.Strings.GetParticularString("Add extension assistant",
                "The gauge has to be greater or equal to 0 and less than 10000");

            gaugeAssistentItem.ErrorCheck = x => (x as AssistentItemModel<double>).EnteredValue < 0 || (x as AssistentItemModel<double>).EnteredValue >= 10000;
            gaugeAssistentItem.ErrorText = gaugeErrorText;
            gaugeAssistentItem.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = x => double.Parse(x, CultureInfo.InvariantCulture) < 0 || double.Parse(x, CultureInfo.InvariantCulture) >= 10000,
                WarningText = gaugeErrorText
            });

            var factorErrorText = _localization.Strings.GetParticularString("Add extension assistant",
                "The factor has to be between 0.9 and 1.5");

            factorAssistentItem.ErrorCheck = x => (x as AssistentItemModel<double>).EnteredValue < 0.9 || (x as AssistentItemModel<double>).EnteredValue > 1.5; 
            factorAssistentItem.ErrorText = factorErrorText;
            factorAssistentItem.Behaviors.Add(new ConditionValidationBehavior()
            {
                Condition = x => double.Parse(x, CultureInfo.InvariantCulture) < 0.9 || double.Parse(x, CultureInfo.InvariantCulture) > 1.5,
                WarningText = factorErrorText
            });
        }

        private AssistentPlan<string> CreateInventoryNumberAssistantPlan()
        {
            return new AssistentPlan<string>(
                new UniqueAssistentItemModel<string>(x => _useCases.extension.IsInventoryNumberUnique(new ExtensionInventoryNumber(x)),
                    AssistentItemType.Text,
                    _localization.Strings.GetParticularString("Add extension assistant", "Enter a inventory number"),
                    _localization.Strings.GetParticularString("Add extension assistant", "Inventory number"),
                    _defaultExtension?.InventoryNumber?.ToDefaultString() ?? "",
                    (o, i) => (o as Extension).InventoryNumber = new ExtensionInventoryNumber((i as AssistentItemModel<string>).EnteredValue),
                    errorCheck: x => string.IsNullOrWhiteSpace((x as AssistentItemModel<string>).EnteredValue),
                    errorText: _localization.Strings.GetParticularString("Add extension assistant", "The inventory number is a required field and has to be unique"),
                    maxLengthText: 50));
        }

        private AssistentPlan<string> CreateDescriptionAssistantPlan()
        {
            return new AssistentPlan<string>(
                new AssistentItemModel<string>(
                    AssistentItemType.Text,
                    _localization.Strings.GetParticularString("Add extension assistant", "Enter a description for the extension"),
                    _localization.Strings.GetParticularString("Add extension assistant", "Description"),
                    _defaultExtension?.Description,
                    (o, i) => (o as Extension).Description = (i as AssistentItemModel<string>).EnteredValue,
                    errorCheck: x => string.IsNullOrWhiteSpace((x as AssistentItemModel<string>).EnteredValue),
                    errorText: _localization.Strings.GetParticularString("Add extension assistant", "The description is required!"),
                    maxLengthText: 40));
        }

        private AssistentPlan<ExtensionCorrection> CreateExtensionCorrectionPlan(AssistentView assistentView)
        {
            return new ListAssistentPlan<ExtensionCorrection>(
                new ListAssistentItemModel<ExtensionCorrection>(
                    assistentView.Dispatcher,
                    new List<ExtensionCorrection> { ExtensionCorrection.UseFactor, ExtensionCorrection.UseGauge },
                    _localization.Strings.GetParticularString("Add extension assistant", "Choose the test extension correction type"),
                    _localization.Strings.GetParticularString("Add extension assistant", "Extension correction"),
                    _defaultExtension?.ExtensionCorrection ?? ExtensionCorrection.UseFactor,
                    (o, i) => (o as Extension).ExtensionCorrection = (i as ListAssistentItemModel<ExtensionCorrection>).EnteredValue,
                    null,
                    x =>
                    {
                        switch (x)
                        {
                            case ExtensionCorrection.UseFactor: return _localization.Strings.GetParticularString("Extension correction", "Factor");
                            case ExtensionCorrection.UseGauge: return _localization.Strings.GetParticularString("Extension correction", "Gauge");
                            default: return "";
                        }
                    },
                    () => { }));
        }


        private AssistentPlan<double> CreateGaugeAssistantPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add extension assistant assistant", "Enter a value for the gauge"),
                    _localization.Strings.GetParticularString("Add extension assistant assistant", "Gauge"),
                    _defaultExtension?.Length ?? 0.0,
                    (o, i) => (o as Extension).Length = (i as AssistentItemModel<double>).EnteredValue,
                    _localization.Strings.GetParticularString("Unit", "mm")));
        }

        private AssistentPlan<double> CreateFactorAssistantPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add extension assistant assistant", "Enter a value for the factor"),
                    _localization.Strings.GetParticularString("Add extension assistant assistant", "Factor"),
                    _defaultExtension == null ? 1.0 : _defaultExtension.Length != 0.0 ? 1.0 : _defaultExtension.FactorTorque,
                    (o, i) => (o as Extension).FactorTorque = (i as AssistentItemModel<double>).EnteredValue));
        }

        private AssistentPlan<double> CreateBendingAssistantPlan()
        {
            return new AssistentPlan<double>(
                new AssistentItemModel<double>(
                    AssistentItemType.FloatingPoint,
                    _localization.Strings.GetParticularString("Add extension assistant assistant", "Enter a value for the bending compensation"),
                    _localization.Strings.GetParticularString("Add extension assistant assistant", "Bending compensation"),
                    _defaultExtension?.Bending ?? 0.0,
                    (o, i) => (o as Extension).Bending = (i as AssistentItemModel<double>).EnteredValue,
                    _localization.Strings.GetParticularString("Unit", "°/100 Nm"), 
                    errorCheck: assistentItem => (assistentItem as AssistentItemModel<double>).EnteredValue < 0 || (assistentItem as AssistentItemModel<double>).EnteredValue >= 100,
                    errorText: _localization.Strings.GetParticularString("Add extension assistant assistant", "The bending compensation has to be greater than or equal to 0 and less than 100!"),
                    behaviors: new List<Behavior>()
                    {
                        new ConditionValidationBehavior()
                        {
                            Condition = x => double.Parse(x, CultureInfo.InvariantCulture) < 0 || double.Parse(x, CultureInfo.InvariantCulture) >= 100,
                            WarningText = _localization.Strings.GetParticularString("Add test equipment assistant", "The bending compensation has to be greater than or equal to 0 and less than 100!")
                        }
                    }));



           
        }
    }
}
