using System;
using Core;
using Core.Entities;
using Core.Enums;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class LocationToolAssignmentModel : BindableBase, IQstEquality<LocationToolAssignmentModel>, IUpdate<LocationToolAssignment>, ICopy<LocationToolAssignmentModel>
    {
        public LocationToolAssignment Entity { get; protected set; }
        protected ILocalizationWrapper _localization;

        public long Id
        {
            get => Entity.Id.ToLong();
            set
            {
                Entity.Id = new LocationToolAssignmentId(value);
                RaisePropertyChanged();
            }
        }

        public LocationModel AssignedLocation
        {
            get => LocationModel.GetModelFor(Entity.AssignedLocation, _localization, null);
            set
            {
                Entity.AssignedLocation = value?.Entity;
                RaisePropertyChanged();
            }
        }

        public ToolModel AssignedTool
        {
            // TODO: Clarify if we can find an alternative for LocalizationWrapper in Models (maybe in the future)
            get => ToolModel.GetModelFor(Entity.AssignedTool, _localization);
            set
            {
                Entity.AssignedTool = value.Entity;
                RaisePropertyChanged();
            }
        }

        private HelperTableItemModel<ToolUsage, string> _toolUsageModel;
        public HelperTableItemModel<ToolUsage, string> ToolUsage
        {
            get
            {
                if (_toolUsageModel is null)
                {
                    _toolUsageModel = HelperTableItemModel.GetModelForToolUsage(Entity.ToolUsage);
                }

                return _toolUsageModel;
            }
            set
            {
                Entity.ToolUsage = value?.Entity;
                _toolUsageModel = HelperTableItemModel.GetModelForToolUsage(Entity.ToolUsage);
                RaisePropertyChanged();
            }
        }

        private TestParametersModel _testParametersModel;

        public TestParametersModel TestParameters
        {
            get
            {
                if (_testParametersModel is null)
                {
                    _testParametersModel = TestParametersModel.GetModelFor(Entity.TestParameters, _localization);
                    if (_testParametersModel != null)
                    {
                        _testParametersModel.PropertyChanged += PropertyChangedChaining;
                    }
                }
                return _testParametersModel;
            }
            set
            {
                Entity.TestParameters = value?.Entity;
                _testParametersModel = TestParametersModel.GetModelFor(Entity.TestParameters, _localization);
                _testParametersModel.PropertyChanged += PropertyChangedChaining;
                RaisePropertyChanged();
            }
        }

        private TestTechniqueModel _testTechniqueModel;
        public TestTechniqueModel TestTechnique
        {
            get
            {
                if (_testTechniqueModel is null)
                {
                    _testTechniqueModel = TestTechniqueModel.GetModelFor(Entity.TestTechnique, _localization);
                    if(_testTechniqueModel != null)
                    {
                        _testTechniqueModel.PropertyChanged += PropertyChangedChaining;
                    }
                }
                return _testTechniqueModel;
            }
            set
            {
                Entity.TestTechnique = value?.Entity;
                _testTechniqueModel = TestTechniqueModel.GetModelFor(Entity.TestTechnique, _localization);
                _testTechniqueModel.PropertyChanged += PropertyChangedChaining;
                RaisePropertyChanged();
            }
        }

        public virtual TestLevelSetModel TestLevelSetMfu
        {
            get => TestLevelSetModel.GetModelFor(Entity.TestLevelSetMfu);
            set
            {
                Entity.TestLevelSetMfu = value?.Entity;
                TestLevelNumberMfu = 1;
                RaisePropertyChanged();
            }
        }

        public virtual int TestLevelNumberMfu
        {
            get => Entity.TestLevelNumberMfu;
            set
            {
                Entity.TestLevelNumberMfu = value;
                RaisePropertyChanged();
            }
        }

        public virtual TestLevelSetModel TestLevelSetChk
        {
            get => TestLevelSetModel.GetModelFor(Entity.TestLevelSetChk);
            set
            {
                Entity.TestLevelSetChk = value?.Entity;
                TestLevelNumberChk = 1;
                RaisePropertyChanged();
            }
        }

        public virtual int TestLevelNumberChk
        {
            get => Entity.TestLevelNumberChk;
            set
            {
                Entity.TestLevelNumberChk = value;
                RaisePropertyChanged();
            }
        }

        public DateTime? NextTestDateMfu
        {
            get => Entity.NextTestDateMfu;
            set
            {
                Entity.NextTestDateMfu = value;
                RaisePropertyChanged();
            }
        }

        public Shift? NextTestShiftMfu
        {
            get => Entity.NextTestShiftMfu;
            set
            {
                Entity.NextTestShiftMfu = value;
                RaisePropertyChanged();
            }
        }

        public DateTime? NextTestDateChk
        {
            get => Entity.NextTestDateChk;
            set
            {
                Entity.NextTestDateChk = value;
                RaisePropertyChanged();
            }
        }

        public Shift? NextTestShiftChk
        {
            get => Entity.NextTestShiftChk;
            set
            {
                Entity.NextTestShiftChk = value;
                RaisePropertyChanged();
            }
        }

        public DateTime StartDateMfu
        {
            get => Entity.StartDateMfu;
            set
            {
                Entity.StartDateMfu = value;
                RaisePropertyChanged();
            }
        }

        public DateTime StartDateChk
        {
            get => Entity.StartDateChk;
            set
            {
                Entity.StartDateChk = value;
                RaisePropertyChanged();
            }
        }

        public bool TestOperationActiveMfu
        {
            get => Entity.TestOperationActiveMfu;
            set
            {
                Entity.TestOperationActiveMfu = value;
                RaisePropertyChanged();
            }
        }

        public bool TestOperationActiveChk
        {
            get => Entity.TestOperationActiveChk;
            set
            {
                Entity.TestOperationActiveChk = value;
                RaisePropertyChanged();
            }
        }

        public LocationToolAssignmentModel(LocationToolAssignment entity, ILocalizationWrapper localization)
        {
            Entity = entity ?? new LocationToolAssignment();
            _localization = localization;
            RaisePropertyChanged();
        }


        public static LocationToolAssignmentModel GetModelFor(LocationToolAssignment entity, ILocalizationWrapper localization)
        {
            return entity != null ? new LocationToolAssignmentModel(entity, localization) : null;
        }

        public bool EqualsByContent(LocationToolAssignmentModel other)
        {
            return this.Entity.EqualsByContent(other?.Entity);
        }

        public bool EqualsById(LocationToolAssignmentModel other)
        {
            return this.Entity.EqualsById(other?.Entity);
        }

        public void UpdateWith(LocationToolAssignment other)
        {
            this.Entity.UpdateWith(other);
            RaisePropertyChanged();
        }

        public LocationToolAssignmentModel CopyDeep()
        {
            return new LocationToolAssignmentModel(Entity.CopyDeep(), _localization);
        }
    }
    
    public class LocationToolAssignmentModelWithTestType : LocationToolAssignmentModel
    {
        public LocationToolAssignmentModelWithTestType(LocationToolAssignment entity, ILocalizationWrapper localization)
            : base(entity, localization) { }


        public new static LocationToolAssignmentModelWithTestType GetModelFor(LocationToolAssignment entity, TestType testType, ILocalizationWrapper localization)
        {
            return entity != null 
                ? new LocationToolAssignmentModelWithTestType(entity, localization)
                {
                    TestType = testType
                }
                : null;
        }

        private TestType _testType = TestType.Mfu;
        public TestType TestType
        {
            get => _testType;
            set
            {
                _testType = value;
                RaisePropertyChanged();
            }
        }

        public TestLevelSetModel TestLevelSetForTestType
        {
            get => TestType == TestType.Chk ? TestLevelSetChk : TestLevelSetMfu;
        }

        public override TestLevelSetModel TestLevelSetMfu 
        {
            get => base.TestLevelSetMfu;
            set
            {
                base.TestLevelSetMfu = value;
                if (TestType == TestType.Mfu)
                {
                    RaisePropertyChanged(nameof(TestLevelSetForTestType));
                }
                RaisePropertyChanged(nameof(TestLevelNumberForTestType));
            }
        }

        public override TestLevelSetModel TestLevelSetChk
        {
            get => base.TestLevelSetChk;
            set
            {
                base.TestLevelSetChk = value;
                if (TestType == TestType.Chk)
                {
                    RaisePropertyChanged(nameof(TestLevelSetForTestType)); 
                }
                RaisePropertyChanged(nameof(TestLevelNumberForTestType));
            }
        }
        
        public int? TestLevelNumberForTestType
        {
            get => TestLevelSetForTestType == null ? null : (TestType == TestType.Chk ? TestLevelNumberChk : TestLevelNumberMfu);
            set
            {
                if(value == null) { return; }

                if(TestType == TestType.Mfu)
                {
                    TestLevelNumberMfu = value ?? 1;
                }
                else if(TestType == TestType.Chk)
                {
                    TestLevelNumberChk = value ?? 1;
                }
                RaisePropertyChanged();
            }
        }

        public override int TestLevelNumberMfu
        {
            get => base.TestLevelNumberMfu;
            set
            {
                base.TestLevelNumberMfu = value;
                if (TestType == TestType.Mfu)
                {
                    RaisePropertyChanged(nameof(TestLevelNumberForTestType)); 
                }
            }
        }

        public override int TestLevelNumberChk
        {
            get => base.TestLevelNumberChk;
            set
            {
                base.TestLevelNumberChk = value;
                if (TestType == TestType.Chk)
                {
                    RaisePropertyChanged(nameof(TestLevelNumberForTestType)); 
                }
            }
        }

        public DateTime? NextTestDateForTestType
        {
            get => TestType == TestType.Chk ? NextTestDateChk : NextTestDateMfu;
        }

        public Shift? NextTestShiftForTestType
        {
            get => TestType == TestType.Chk ? NextTestShiftChk : NextTestShiftMfu;
        }

        public void RaiseTestLevelNumberForTestTypeChanged()
        {
            RaisePropertyChanged(nameof(TestLevelNumberForTestType));
        }
    }
}
