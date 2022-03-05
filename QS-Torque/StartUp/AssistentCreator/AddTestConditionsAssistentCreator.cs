using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Core.Entities;
using Core.Entities.ToolTypes;
using Core.Enums;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Localization;

namespace StartUp.AssistentCreator
{
    class AddTestConditionsAssistentCreator
    {
        private LocationToolAssignment _defaultAssignment;
        private LocalizationWrapper _localization;
        private StartUpImpl _startUp;
        private UseCaseCollection _useCases;

        public AddTestConditionsAssistentCreator(LocalizationWrapper localization, StartUpImpl startUp, UseCaseCollection useCases, LocationToolAssignment defafultAssignment)
        {
            _localization = localization;
            _defaultAssignment = defafultAssignment;
            _startUp = startUp;
            _useCases = useCases;
        }


        public AssistentView CreateAddTestConditionsAssistent()
        {
            AssistentView assistentView = new AssistentView(_localization.Strings.GetParticularString("Add location tool assignment assistent", "Add a location tool assignment"));

            var controlledByPlan = CreateControlledByAssistentPlan(assistentView);

            var parameterAssistentCreator = new TestParameterAssistentCreator(_localization, _startUp, _useCases, _defaultAssignment?.TestParameters);
            var techniqueAssistentCreator = new TestTechniqueAssistentCreator(_localization, _defaultAssignment?.TestTechnique);

            var controlledByTorqueParametersTuple = parameterAssistentCreator.CreateControlledByTorqueAssistent(assistentView);
            var powerToolTuple = techniqueAssistentCreator.CreatePowerToolAssistent();
            var peakValueTuple = techniqueAssistentCreator.CreatePeakValueAssistent();

            // Set default value of start final angle in test technique
            PropertyChangedEventHandler parameterForStartFinalAngleChanged = (s, e) =>
            {
                if (e.PropertyName == nameof(AssistentItemModel<double>.EnteredValue))
                {
                    var toleranceClassItem = controlledByTorqueParametersTuple.toleranceClassPlan.AssistentItem;
                    var setPointItem = controlledByTorqueParametersTuple.setpointPlan.AssistentItem;

                    if (toleranceClassItem.EnteredValue == null)
                    {
                        powerToolTuple.startFinalAnglePlan.AssistentItem.DefaultValue = 0;
                        peakValueTuple.startFinalAnglePlan.AssistentItem.DefaultValue = 0;
                    }
                    else if (toleranceClassItem.EnteredValue.LowerLimit == 0 && toleranceClassItem.EnteredValue.UpperLimit == 0)
                    {
                        // ToleranceClass free input
                        var min = controlledByTorqueParametersTuple.minTorquePlan.AssistentItem.EnteredValue;
                        var max = controlledByTorqueParametersTuple.maxTorquePlan.AssistentItem.EnteredValue;
                        var average = (min + max) / 2;

                        powerToolTuple.startFinalAnglePlan.AssistentItem.DefaultValue = average;
                        peakValueTuple.startFinalAnglePlan.AssistentItem.DefaultValue = average;
                    }
                    else
                    {
                        var min = toleranceClassItem.EnteredValue.GetLowerLimitForValue(setPointItem.EnteredValue);
                        var max = toleranceClassItem.EnteredValue.GetUpperLimitForValue(setPointItem.EnteredValue);
                        var average = (min + max) / 2;

                        powerToolTuple.startFinalAnglePlan.AssistentItem.DefaultValue = average;
                        peakValueTuple.startFinalAnglePlan.AssistentItem.DefaultValue = average;
                    }
                }
            };

            controlledByTorqueParametersTuple.setpointPlan.AssistentItem.PropertyChanged += parameterForStartFinalAngleChanged;
            controlledByTorqueParametersTuple.toleranceClassPlan.AssistentItem.PropertyChanged += parameterForStartFinalAngleChanged;
            controlledByTorqueParametersTuple.minTorquePlan.AssistentItem.PropertyChanged += parameterForStartFinalAngleChanged;
            controlledByTorqueParametersTuple.maxTorquePlan.AssistentItem.PropertyChanged += parameterForStartFinalAngleChanged;

            var testLevelSetMfuPlan = CreateTestLevelSetAssistantPlanForMfu(assistentView);
            var testLevelSetChkPlan = CreateTestLevelSetAssistantPlanForChk(assistentView);

            var parentPlan = new ParentAssistentPlan(new List<ParentAssistentPlan>()
            {
                controlledByPlan,

                // Test parameters
                new AssistentPlan(
                    new ChapterHeadingAssistentItemModel(_localization.Strings.GetParticularString("Add location tool assignment assistent", "Test parameters"))),
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>() { controlledByTorqueParametersTuple.completeAssistantPlan },
                    () => controlledByPlan.AssistentItem.EnteredValue == LocationControlledBy.Torque),
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>() { parameterAssistentCreator.CreateControlledByAngleAssistent(assistentView) },
                    () => controlledByPlan.AssistentItem.EnteredValue == LocationControlledBy.Angle),

                // Testing schedule
                new AssistentPlan(new ChapterHeadingAssistentItemModel(_localization.Strings.GetParticularString("Add location tool assignment assistent", "Testing schedule - Monitoring"))),
                testLevelSetChkPlan,
                new ConditionalAssistentPlan(new List<ParentAssistentPlan>()
                    {
                        new ConditionalAssistentPlan(new List<ParentAssistentPlan>() { CreateTestLevelNumberChkByAssistantPlan(assistentView, new List<int>() { 1 }) },
                            () => !testLevelSetChkPlan.AssistentItem.EnteredValue.TestLevel2.IsActive && !testLevelSetChkPlan.AssistentItem.EnteredValue.TestLevel3.IsActive),
                        new ConditionalAssistentPlan(new List<ParentAssistentPlan>() { CreateTestLevelNumberChkByAssistantPlan(assistentView, new List<int>() { 1, 2 }) },
                            () => testLevelSetChkPlan.AssistentItem.EnteredValue.TestLevel2.IsActive && !testLevelSetChkPlan.AssistentItem.EnteredValue.TestLevel3.IsActive),
                        new ConditionalAssistentPlan(new List<ParentAssistentPlan>() { CreateTestLevelNumberChkByAssistantPlan(assistentView, new List<int>() { 1, 3 }) },
                            () => !testLevelSetChkPlan.AssistentItem.EnteredValue.TestLevel2.IsActive && testLevelSetChkPlan.AssistentItem.EnteredValue.TestLevel3.IsActive),
                        new ConditionalAssistentPlan(new List<ParentAssistentPlan>() { CreateTestLevelNumberChkByAssistantPlan(assistentView, new List<int>() { 1, 2, 3 }) },
                            () => testLevelSetChkPlan.AssistentItem.EnteredValue.TestLevel2.IsActive && testLevelSetChkPlan.AssistentItem.EnteredValue.TestLevel3.IsActive)
                    },
                    () => testLevelSetChkPlan?.AssistentItem?.EnteredValue?.TestLevel2 != null && testLevelSetChkPlan?.AssistentItem?.EnteredValue?.TestLevel3 != null),
                CreateStartDateAssistantPlanForChk(),
                CreateTestOperationActiveAssistantPlanForChk(),

                
                new AssistentPlan(new ChapterHeadingAssistentItemModel(_localization.Strings.GetParticularString("Add location tool assignment assistent", "Testing schedule - MCA"))),
                testLevelSetMfuPlan,
                new ConditionalAssistentPlan(new List<ParentAssistentPlan>()
                    {
                        new ConditionalAssistentPlan(new List<ParentAssistentPlan>() { CreateTestLevelNumberMfuByAssistantPlan(assistentView, new List<int>() { 1 }) },
                            () => !testLevelSetMfuPlan.AssistentItem.EnteredValue.TestLevel2.IsActive && !testLevelSetMfuPlan.AssistentItem.EnteredValue.TestLevel3.IsActive),
                        new ConditionalAssistentPlan(new List<ParentAssistentPlan>() { CreateTestLevelNumberMfuByAssistantPlan(assistentView, new List<int>() { 1, 2 }) },
                            () => testLevelSetMfuPlan.AssistentItem.EnteredValue.TestLevel2.IsActive && !testLevelSetMfuPlan.AssistentItem.EnteredValue.TestLevel3.IsActive),
                        new ConditionalAssistentPlan(new List<ParentAssistentPlan>() { CreateTestLevelNumberMfuByAssistantPlan(assistentView, new List<int>() { 1, 3 }) },
                            () => !testLevelSetMfuPlan.AssistentItem.EnteredValue.TestLevel2.IsActive && testLevelSetMfuPlan.AssistentItem.EnteredValue.TestLevel3.IsActive),
                        new ConditionalAssistentPlan(new List<ParentAssistentPlan>() { CreateTestLevelNumberMfuByAssistantPlan(assistentView, new List<int>() { 1, 2, 3 }) },
                            () => testLevelSetMfuPlan.AssistentItem.EnteredValue.TestLevel2.IsActive && testLevelSetMfuPlan.AssistentItem.EnteredValue.TestLevel3.IsActive),
                    },
                    () => testLevelSetMfuPlan?.AssistentItem?.EnteredValue?.TestLevel2 != null && testLevelSetMfuPlan?.AssistentItem?.EnteredValue?.TestLevel3 != null),
                CreateStartDateAssistantPlanForMfu(),
                CreateTestOperationActiveAssistantPlanForMfu(),

                // Test techniques
                new AssistentPlan(
                    new ChapterHeadingAssistentItemModel(_localization.Strings.GetParticularString("Add location tool assignment assistent", "Test technique"))),
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>() { techniqueAssistentCreator.CreateClickWrenchAssistent() },
                    () => _defaultAssignment?.AssignedTool?.ToolModel.ModelType is ClickWrench),
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>() { techniqueAssistentCreator.CreatePulseDriverAssistent() },
                    () => _defaultAssignment?.AssignedTool?.ToolModel.ModelType is PulseDriver ||
                                  _defaultAssignment?.AssignedTool?.ToolModel.ModelType is PulseDriverShutOff),
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>() { powerToolTuple.completeAssistentPlan },
                    () => _defaultAssignment?.AssignedTool?.ToolModel.ModelType is General ||
                                  _defaultAssignment?.AssignedTool?.ToolModel.ModelType is ECDriver),
                new ConditionalAssistentPlan(
                    new List<ParentAssistentPlan>() { peakValueTuple.completeAssistentPlan },
                    () => _defaultAssignment?.AssignedTool?.ToolModel.ModelType is MDWrench ||
                                  _defaultAssignment?.AssignedTool?.ToolModel.ModelType is ProductionWrench)
            });

            assistentView.SetParentPlan(parentPlan);
            return assistentView;
        }


        #region Special AssistentPlanCreator

        private ListAssistentPlan<LocationControlledBy> CreateControlledByAssistentPlan(AssistentView assistentView)
        {
            return new ListAssistentPlan<LocationControlledBy>(
                new ListAssistentItemModel<LocationControlledBy>(
                    assistentView.Dispatcher,
                    new List<LocationControlledBy>() { LocationControlledBy.Torque, LocationControlledBy.Angle },
                    _localization.Strings.GetParticularString("Add location tool assignment assistent", "Choose by which attribute the location tool assignment is controlled"),
                    _localization.Strings.GetParticularString("Add location tool assignment assistent", "Controlled by"),
                     _defaultAssignment?.TestParameters.ControlledBy ?? LocationControlledBy.Torque,
                    (o, i) => (o as LocationToolAssignment).TestParameters.ControlledBy = (i as ListAssistentItemModel<LocationControlledBy>).EnteredValue,
                    null,
                    x =>
                    {
                        switch (x)
                        {
                            case LocationControlledBy.Torque: return _localization.Strings.GetParticularString("Controlled by", "Torque");
                            case LocationControlledBy.Angle: return _localization.Strings.GetParticularString("Controlled by", "Angle");
                            default: return "";
                        }
                    },
                    () => { }));
        }

        private TestLevelSetAssistantPlan CreateTestLevelSetAssistantPlanForMfu(AssistentView assistantView)
        {
            var plan = new TestLevelSetAssistantPlan(_useCases.TestLevelSet,
                _useCases.TestLevelSetInterface,
                _localization,
                new ListAssistentItemModel<TestLevelSet>(assistantView.Dispatcher,
                    _localization.Strings.GetParticularString("Add location tool assignment assistant", "Choose a test level set for mca tests"),
                    _localization.Strings.GetParticularString("Add location tool assignment assistant", "Test level set mca"),
                    null,
                    (resultObject, assistantItem) => (resultObject as LocationToolAssignment).TestLevelSetMfu = (assistantItem as ListAssistentItemModel<TestLevelSet>).EnteredValue,
                    _localization.Strings.GetParticularString("Add location tool assignment assistant", "Jump to test level set"),
                    x => x.Name.ToDefaultString(),
                    () =>
                    {
                        _startUp.OpenTestLevelSetDialog(assistantView);
                    }),
                _defaultAssignment?.TestLevelSetMfu?.Id);

            plan.MessageBoxRequest += (s, e) => assistantView.ShowMessageBox(e);
            return plan;
        }

        private TestLevelSetAssistantPlan CreateTestLevelSetAssistantPlanForChk(AssistentView assistantView)
        {
            var plan = new TestLevelSetAssistantPlan(_useCases.TestLevelSet,
                _useCases.TestLevelSetInterface,
                _localization,
                new ListAssistentItemModel<TestLevelSet>(assistantView.Dispatcher,
                    _localization.Strings.GetParticularString("Add location tool assignment assistant", "Choose a test level set for monitoring tests"),
                    _localization.Strings.GetParticularString("Add location tool assignment assistant", "Test level set monitoring"),
                    null,
                    (resultObject, assistantItem) => (resultObject as LocationToolAssignment).TestLevelSetChk = (assistantItem as ListAssistentItemModel<TestLevelSet>).EnteredValue,
                    _localization.Strings.GetParticularString("Add location tool assignment assistant", "Jump to test level set"),
                    x => x.Name.ToDefaultString(),
                    () =>
                    {
                        _startUp.OpenTestLevelSetDialog(assistantView);
                    }),
                _defaultAssignment?.TestLevelSetChk?.Id);

            plan.MessageBoxRequest += (s, e) => assistantView.ShowMessageBox(e);
            return plan;
        }

        private ListAssistentPlan<int> CreateTestLevelNumberMfuByAssistantPlan(AssistentView assistantView, List<int> numbersToChoose)
        {
            return new ListAssistentPlan<int>(
                new ListAssistentItemModel<int>(
                    assistantView.Dispatcher,
                    numbersToChoose ?? new List<int>() { 1, 2, 3 },
                    _localization.Strings.GetParticularString("Add location tool assignment assistant", "Choose the test level for mfu tests"),
                    _localization.Strings.GetParticularString("Add location tool assignment assistant", "Test level mfu"),
                    numbersToChoose.Contains(_defaultAssignment?.TestLevelNumberMfu ?? 1) ? _defaultAssignment?.TestLevelNumberMfu ?? 1 : numbersToChoose.First(),
                    (o, i) => (o as LocationToolAssignment).TestLevelNumberMfu = (i as ListAssistentItemModel<int>).EnteredValue,
                    null,
                    x => x.ToString(),
                    () => { }));
        }

        private ListAssistentPlan<int> CreateTestLevelNumberChkByAssistantPlan(AssistentView assistantView, List<int> numbersToChoose)
        {
            return new ListAssistentPlan<int>(
                new ListAssistentItemModel<int>(
                    assistantView.Dispatcher,
                    numbersToChoose ?? new List<int>() { 1, 2, 3 },
                    _localization.Strings.GetParticularString("Add location tool assignment assistant", "Choose the test level for monitoring tests"),
                    _localization.Strings.GetParticularString("Add location tool assignment assistant", "Test level monitoring"),
                    numbersToChoose.Contains(_defaultAssignment?.TestLevelNumberMfu ?? 1) ? _defaultAssignment?.TestLevelNumberMfu ?? 1 : numbersToChoose.First(),
                    (o, i) => (o as LocationToolAssignment).TestLevelNumberChk = (i as ListAssistentItemModel<int>).EnteredValue,
                    null,
                    x => x.ToString(),
                    () => { }));
        }

        private AssistentPlan<DateTime> CreateStartDateAssistantPlanForMfu()
        {
            return new AssistentPlan<DateTime>(
                new AssistentItemModel<DateTime>(
                    AssistentItemType.Date,
                    _localization.Strings.GetParticularString("Add location tool assignment assistant", "Enter a start date for mca tests"),
                    _localization.Strings.GetParticularString("Add location tool assignment assistant", "Start date mca"),
                    _defaultAssignment?.StartDateMfu ?? DateTime.Today,
                    (o, i) => (o as LocationToolAssignment).StartDateMfu =
                        (i as AssistentItemModel<DateTime>).EnteredValue));
        }

        private AssistentPlan<DateTime> CreateStartDateAssistantPlanForChk()
        {
            return new AssistentPlan<DateTime>(
                new AssistentItemModel<DateTime>(
                    AssistentItemType.Date,
                    _localization.Strings.GetParticularString("Add location tool assignment assistant", "Enter a start date for monitoring tests"),
                    _localization.Strings.GetParticularString("Add location tool assignment assistant", "Start date monitoring"),
                    _defaultAssignment?.StartDateChk ?? DateTime.Today,
                    (o, i) => (o as LocationToolAssignment).StartDateChk =
                        (i as AssistentItemModel<DateTime>).EnteredValue));
        }

        private AssistentPlan<bool> CreateTestOperationActiveAssistantPlanForMfu()
        {
            return new AssistentPlan<bool>(
                new AssistentItemModel<bool>(
                    AssistentItemType.Boolean,
                    _localization.Strings.GetParticularString("Testing schedule assistant", "Choose if the location too assignment is active for mfu tests"),
                    _localization.Strings.GetParticularString("Testing schedule assistant", "Test mode active mfu"),
                    false,
                    (resultObject, assistantItem) => (resultObject as LocationToolAssignment).TestOperationActiveMfu = (assistantItem as AssistentItemModel<bool>).EnteredValue));
        }

        private AssistentPlan<bool> CreateTestOperationActiveAssistantPlanForChk()
        {
            return new AssistentPlan<bool>(
                new AssistentItemModel<bool>(
                    AssistentItemType.Boolean,
                    _localization.Strings.GetParticularString("Testing schedule assistant", "Choose if the location too assignment is active for monitoring tests"),
                    _localization.Strings.GetParticularString("Testing schedule assistant", "Test mode active monitoring"),
                    false,
                    (resultObject, assistantItem) => (resultObject as LocationToolAssignment).TestOperationActiveChk = (assistantItem as AssistentItemModel<bool>).EnteredValue));
        }

        #endregion
    }
}