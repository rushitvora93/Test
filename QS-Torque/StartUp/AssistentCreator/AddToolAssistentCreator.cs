using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Localization;
using System.Collections.Generic;
using FrameworksAndDrivers.Gui.Wpf;
using ToolModel = Core.Entities.ToolModel;

namespace StartUp.AssistentCreator
{
    class AddToolAssistentCreator
    {
        Tool _defaultTool;
        IStartUp _startUp;
        LocalizationWrapper _localization;
        UseCaseCollection _useCases;


        public AssistentView CreateAddToolAssistent()
        {
            var assistentView = new AssistentView(_localization.Strings.GetParticularString("Add tool assistent", "Add new tool"));

            var toolModelPlan = CreateToolModelAssistentPlan(assistentView);
            var statusPlan = CreateStatusAssistentPlan(assistentView);
            var configurableFieldPlan = CreateConfigurableFieldAssistentPlan(assistentView);
            var costCenterPlan = CreateCostCenterAssistentPlan(assistentView);

            var parentPlan = new ParentAssistentPlan(new List<ParentAssistentPlan>()
            {
                CreateSerialNumberAssistentPlan(),
                CreateInventoryNumberAssistentPlan(),
                toolModelPlan,
                statusPlan,
                CreateAccessoryAssistentPlan(),
                configurableFieldPlan,
                costCenterPlan,
                CreateAdditionalConfigurableField1AssistentPlan(),
                CreateAdditionalConfigurableField2AssistentPlan(),
                CreateAdditionalConfigurableField3AssistentPlan()
            });

            _useCases.toolModelGuiAdapter.RegisterGuiInterface(toolModelPlan);
            if(!FeatureToggles.FeatureToggles.HelperTablesWithInterfaceAdapter)
			{
				_useCases.statusGuiAdapter.RegisterGuiInterface(statusPlan);
			}
            _useCases.configurableFieldGuiAdapter.RegisterGuiInterface(configurableFieldPlan);
            _useCases.costCenterGuiAdapter.RegisterGuiInterface(costCenterPlan);

            assistentView.SetParentPlan(parentPlan);

            return assistentView;
        }


        #region Special AssistentPlan Creator
        private AssistentPlan<string> CreateSerialNumberAssistentPlan()
        {
            return new AssistentPlan<string>(
                new UniqueAssistentItemModel<string>(_useCases.tool.IsSerialNumberUnique,
                                                     AssistentItemType.Text,
                                                     _localization.Strings.GetParticularString("Add tool assistent", "Enter a serial number"),
                                                     _localization.Strings.GetParticularString("Add tool assistent", "Serial number"),
                                                     _defaultTool?.SerialNumber?.ToDefaultString() ?? "",
                                                     (o, i) => (o as Tool).SerialNumber = new ToolSerialNumber((i as AssistentItemModel<string>).EnteredValue),
                                                     errorCheck: x => string.IsNullOrWhiteSpace((x as AssistentItemModel<string>).EnteredValue),
                                                     errorText: _localization.Strings.GetParticularString("Add tool assistent", "The serial number is a required field and has to be unique"),
                                                     maxLengthText: 20));
        }

        private AssistentPlan<string> CreateInventoryNumberAssistentPlan()
        {
            return new AssistentPlan<string>(
                new UniqueAssistentItemModel<string>(_useCases.tool.IsInventoryNumberUnique,
                                                     AssistentItemType.Text,
                                                     _localization.Strings.GetParticularString("Add tool assistent", "Enter a inventory number"),
                                                     _localization.Strings.GetParticularString("Add tool assistent", "Inventory number"),
                                                     _defaultTool?.InventoryNumber?.ToDefaultString() ?? "",
                                                     (o, i) => (o as Tool).InventoryNumber = new ToolInventoryNumber((i as AssistentItemModel<string>).EnteredValue),
                                                     errorCheck: x => string.IsNullOrWhiteSpace((x as AssistentItemModel<string>).EnteredValue),
                                                     errorText: _localization.Strings.GetParticularString("Add tool assistent", "The inventory number is a required field and has to be unique"),
                                                     maxLengthText: 50));
        }

        private ToolModelAssistentPlan CreateToolModelAssistentPlan(AssistentView assistentView)
        {
            return new ToolModelAssistentPlan(_useCases.toolModel,
                new ListAssistentItemModel<ToolModel>(assistentView.Dispatcher,
                                                      _localization.Strings.GetParticularString("Add tool assistent", "Choose a tool model"),
                                                      _localization.Strings.GetParticularString("Add tool assistent", "Tool model"),
                                                      null,
                                                      (o, i) => (o as Tool).ToolModel = (i as ListAssistentItemModel<ToolModel>).EnteredValue,
                                                      _localization.Strings.GetParticularString("Add tool assistent", "Jump to tool model"),
                                                      x => x.Description?.ToDefaultString(),
                                                      () =>
                                                      {
                                                          _startUp.OpenToolModelDialog(assistentView);
                                                      }),
                _defaultTool?.ToolModel?.Id ?? null);
        }

        private HelperTableItemAssistentPlan<Status> CreateStatusAssistentPlan(AssistentView assistentView)
        {
            return new HelperTableItemAssistentPlan<Status>(_useCases.status,
                new ListAssistentItemModel<Status>(assistentView.Dispatcher,
                                                   _localization.Strings.GetParticularString("Add tool assistent", "Choose a status"),
                                                   _localization.Strings.GetParticularString("Add tool assistent", "Status"),
                                                   null,
                                                   (o, i) => (o as Tool).Status = (i as ListAssistentItemModel<Status>).EnteredValue,
                                                   _localization.Strings.GetParticularString("Add tool assistent", "Jump to status"),
                                                   x => x.Value.ToDefaultString(),
                                                   () =>
                                                   {
                                                       _startUp.OpenStatusHelperTableDialog(assistentView);
                                                   }),
                _defaultTool?.Status?.ListId ?? null);
        }

        private AssistentPlan<string> CreateAccessoryAssistentPlan()
        {
            return new AssistentPlan<string>(
                new AssistentItemModel<string>(AssistentItemType.Text,
                                               _localization.Strings.GetParticularString("Add tool assistent", "Enter a accessory"),
                                               _localization.Strings.GetParticularString("Add tool assistent", "Accessory"),
                                               _defaultTool?.Accessory ?? "",
                                               (o, i) => (o as Tool).Accessory = (i as AssistentItemModel<string>).EnteredValue));
        }

        private HelperTableItemAssistentPlan<ConfigurableField> CreateConfigurableFieldAssistentPlan(AssistentView assistentView)
        {
            return new HelperTableItemAssistentPlan<ConfigurableField>(_useCases.configurableField,
                new ListAssistentItemModel<ConfigurableField>(assistentView.Dispatcher,
                                                              _localization.Strings.GetParticularString("Add tool assistent", "Choose a customer"),
                                                              _localization.Strings.GetParticularString("Add tool assistent", "Customer"),
                                                              null,
                                                              (o, i) => (o as Tool).ConfigurableField = (i as ListAssistentItemModel<ConfigurableField>).EnteredValue,
                                                              _localization.Strings.GetParticularString("Add tool assistent", "Jump to customer"),
                                                              x => x.Value.ToDefaultString(),
                                                              () =>
                                                              {
                                                                  _startUp.OpenConfigurableFieldHelperTableDialog(assistentView);
                                                              }),
                _defaultTool?.ConfigurableField?.ListId ?? null);
        }

        private HelperTableItemAssistentPlan<CostCenter> CreateCostCenterAssistentPlan(AssistentView assistentView)
        {
            return new HelperTableItemAssistentPlan<CostCenter>(_useCases.costCenter,
                new ListAssistentItemModel<CostCenter>(assistentView.Dispatcher,
                                                       _localization.Strings.GetParticularString("Add tool assistent", "Choose a cost center"),
                                                       _localization.Strings.GetParticularString("Add tool assistent", "Cost center"),
                                                       null,
                                                       (o, i) => (o as Tool).CostCenter = (i as ListAssistentItemModel<CostCenter>).EnteredValue,
                                                       _localization.Strings.GetParticularString("Add tool assistent", "Jump to cost center"),
                                                       x => x.Value.ToDefaultString(),
                                                       () =>
                                                       {
                                                           _startUp.OpenCostCenterHelperTableDialog(assistentView);
                                                       }),
                _defaultTool?.CostCenter?.ListId ?? null);
        }

        private AssistentPlan<string> CreateAdditionalConfigurableField1AssistentPlan()
        {
            return new AssistentPlan<string>(
                new AssistentItemModel<string>(AssistentItemType.Text,
                                               _localization.Strings.GetParticularString("Add tool assistent", "Enter a value for configurale field 1"),
                                               _localization.Strings.GetParticularString("Add tool assistent", "Configurable field 1"),
                                               _defaultTool?.AdditionalConfigurableField1.ToDefaultString() ?? "",
                                               (o, i) => (o as Tool).AdditionalConfigurableField1 = new ConfigurableFieldString40((i as AssistentItemModel<string>).EnteredValue),
                                               maxLengthText: 40));
        }

        private AssistentPlan<string> CreateAdditionalConfigurableField2AssistentPlan()
        {
            return new AssistentPlan<string>(
                new AssistentItemModel<string>(AssistentItemType.Text,
                                               _localization.Strings.GetParticularString("Add tool assistent", "Enter a value for configurale field 2"),
                                               _localization.Strings.GetParticularString("Add tool assistent", "Configurable field 2"),
                                               _defaultTool?.AdditionalConfigurableField2.ToDefaultString() ?? "",
                                               (o, i) => (o as Tool).AdditionalConfigurableField2 = new ConfigurableFieldString80((i as AssistentItemModel<string>).EnteredValue),
                                               maxLengthText: 80));
        }

        private AssistentPlan<string> CreateAdditionalConfigurableField3AssistentPlan()
        {
            return new AssistentPlan<string>(
                new AssistentItemModel<string>(AssistentItemType.Text,
                                               _localization.Strings.GetParticularString("Add tool assistent", "Enter a value for configurale field 3"),
                                               _localization.Strings.GetParticularString("Add tool assistent", "Configurable field 3"),
                                               _defaultTool?.AdditionalConfigurableField3.ToDefaultString() ?? "",
                                               (o, i) => (o as Tool).AdditionalConfigurableField3 = new ConfigurableFieldString250((i as AssistentItemModel<string>).EnteredValue),
                                               maxLengthText: 250));
        }
        #endregion


        public AddToolAssistentCreator(Tool defaultTool, StartUpImpl startUp, LocalizationWrapper localization, UseCaseCollection useCases)
        {
            _defaultTool = defaultTool;
            _startUp = startUp;
            _localization = localization;
            _useCases = useCases;
        }
    }
}
