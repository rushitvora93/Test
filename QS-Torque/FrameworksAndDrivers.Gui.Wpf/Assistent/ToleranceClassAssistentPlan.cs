using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.Model;

namespace FrameworksAndDrivers.Gui.Wpf.Assistent
{
    public class ToleranceClassAssistentPlan : ListAssistentPlan<ToleranceClass>, IToleranceClassGui
    {
        private IToleranceClassUseCase _useCase;
        private ToleranceClassId _defaultToleranceClassId;
        private bool _isInitializing = false;           // True if ShowToleranceClasses is called at the initializing


        #region Interface implementation

        public void ShowToleranceClasses(List<ToleranceClass> toleranceClasses)
        {
            AssistentItem.RefillListItems(toleranceClasses);

            if (_isInitializing && _defaultToleranceClassId != null)
            {
                // Select default value
                AssistentItem.EnteredDisplayMemberModel = AssistentItem.ItemsCollectionView.SourceCollection
                    .OfType<DisplayMemberModel<ToleranceClass>>()
                    .FirstOrDefault(x => x.Item.Id.Equals(_defaultToleranceClassId));
                _isInitializing = false;
            }
        }

        public void ShowToleranceClassesError()
        {
            // Do nothing
        }

        public void RemoveToleranceClass(ToleranceClass toleranceClass)
        {
            var toleranceClasses = AssistentItem.GetCurrentListItems();
            var removingToleranceClass = toleranceClasses.FirstOrDefault(x => x.EqualsById(toleranceClass));

            if (removingToleranceClass == null)
            {
                return;
            }

            // Update ListItems
            toleranceClasses.Remove(removingToleranceClass);
            AssistentItem.RefillListItems(toleranceClasses);
        }

        public void RemoveToleranceClassError()
        {
            // Do nothing
        }

        public void AddToleranceClass(ToleranceClass toleranceClass)
        {
            AssistentItem.AddListItem(toleranceClass);
        }

        public void AddToleranceClassError()
        {
            // Do nothing
        }

        public void UpdateToleranceClass(ToleranceClass toleranceClass)
        {
            var toleranceClasses = AssistentItem.GetCurrentListItems();
            var oldToleranceClass = toleranceClasses.FirstOrDefault(x => x.EqualsById(toleranceClass));

            if (oldToleranceClass == null)
            {
                return;
            }
            
            var index = toleranceClasses.IndexOf(oldToleranceClass);
            toleranceClasses.Remove(oldToleranceClass);
            toleranceClasses.Insert(index, toleranceClass);
            AssistentItem.RefillListItems(toleranceClasses);
        }

        public void SaveToleranceClassError()
        {
            // Do nothing
        }

        public void ShowReferencedLocations(List<LocationReferenceLink> locationReferenceLinks)
        {
            // Do nothing
        }

        public void ShowReferencesError()
        {
            // Do nothing
        }

        public void ShowReferencedLocationToolAssignments(List<LocationToolAssignment> assignments)
        {
            // Do nothing
        }

        public void ShowRemoveToleranceClassPreventingReferences(List<LocationReferenceLink> referencedLocations, List<LocationToolAssignment> referencedLocationToolAssignments)
        {
            // Do nothing
        }

        #endregion


        #region Overrides
        public override void Initialize()
        {
            _isInitializing = true;
            _useCase?.LoadToleranceClasses();
            
            base.Initialize();
        }
        #endregion


        public ToleranceClassAssistentPlan(IToleranceClassUseCase useCase, ListAssistentItemModel<ToleranceClass> item, ToleranceClassId defaultToleranceClassId = null) : base(item)
        {
            _useCase = useCase;
            _defaultToleranceClassId = defaultToleranceClassId;
        }

        public ToleranceClassAssistentPlan(IToleranceClassUseCase useCase, List<AssistentPlan> subPlans, ListAssistentItemModel<ToleranceClass> item, ToleranceClassId defaultToleranceClassId = null) : base(subPlans, item)
        {
            _useCase = useCase;
            _defaultToleranceClassId = defaultToleranceClassId;
        }
    }
}
