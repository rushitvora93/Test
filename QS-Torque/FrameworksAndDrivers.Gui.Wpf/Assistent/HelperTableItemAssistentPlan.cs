using Core;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.Model;
using System.Collections.Generic;
using System.Linq;

namespace FrameworksAndDrivers.Gui.Wpf.Assistent
{
    public class HelperTableItemAssistentPlan<T>
        : ListAssistentPlan<T>
        , IHelperTableGui<T>
        , IHelperTableReadOnlyErrorGui<T>
        where T : HelperTableEntity, IQstEquality<T>
    {
        private IHelperTableUseCase<T> _useCase;
        private HelperTableEntityId _defaultHelperTableId;
        protected bool DisableInitialization = false;


        #region Interface implementations
        public virtual void ShowItems(List<T> items)
        {
            AssistentItem.RefillListItems(items);
            if (_defaultHelperTableId != null)
            {
                // Select default value
                AssistentItem.EnteredDisplayMemberModel = AssistentItem.ItemsCollectionView.SourceCollection.OfType<DisplayMemberModel<T>>().FirstOrDefault(x => x.Item.ListId.Equals(_defaultHelperTableId));
            }
        }

        public void Add(T newItem)
        {
            AssistentItem.AddListItem(newItem);
        }

        public void Remove(T removeItem)
        {
            var helperTableItems = AssistentItem.GetCurrentListItems();
            var removingItem = helperTableItems.FirstOrDefault(x => x.EqualsById(removeItem));

            if (removingItem == null)
            {
                return;
            }

            // Update ListItems
            helperTableItems.Remove(removingItem);
            AssistentItem.RefillListItems(helperTableItems);
        }

        public void Save(T savedItem)
        {
            var helperTableItemModels = AssistentItem.GetCurrentListItems();
            var oldHelperTableItemModel = helperTableItemModels.FirstOrDefault(x => x.EqualsById(savedItem));

            if (oldHelperTableItemModel == null)
            {
                return;
            }

            // Remove the old HelperTableItem and add the updated one
            var index = helperTableItemModels.IndexOf(oldHelperTableItemModel);
            helperTableItemModels.Remove(oldHelperTableItemModel);
            helperTableItemModels.Insert(index, savedItem);
            AssistentItem.RefillListItems(helperTableItemModels);
        }

        public void ShowErrorMessage()
        {
            // Do nothing
        }

        #endregion


        #region Overrides
        public override void Initialize()
        {
            if (!DisableInitialization)
            {
                _useCase?.LoadItems(this); 
            }

            base.Initialize();
        }
        #endregion


        public HelperTableItemAssistentPlan(IHelperTableUseCase<T> useCase, ListAssistentItemModel<T> item, HelperTableEntityId defaultHelperTableId = null) : base(item)
        {
            _useCase = useCase;
            _defaultHelperTableId = defaultHelperTableId;
        }

        public HelperTableItemAssistentPlan(IHelperTableUseCase<T> useCase, List<AssistentPlan> subPlans, ListAssistentItemModel<T> item, HelperTableEntityId defaultHelperTableId = null) : base(subPlans, item)
        {
            _useCase = useCase;
            _defaultHelperTableId = defaultHelperTableId;
        }
    }
}
