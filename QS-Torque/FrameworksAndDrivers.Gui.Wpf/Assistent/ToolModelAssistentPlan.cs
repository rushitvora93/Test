using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.Model;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.ReferenceLink;
using ToolModel = Core.Entities.ToolModel;

namespace FrameworksAndDrivers.Gui.Wpf.Assistent
{
    public class ToolModelAssistentPlan : ListAssistentPlan<ToolModel>, IToolModelGui
    {
        private IToolModelUseCase _useCase;
        private ToolModelId _defaultToolModelId;
        private bool _isInitializing;


        #region Interface implementations
        public void ShowToolModels(List<ToolModel> toolModels)
        {
            AssistentItem.RefillListItems(toolModels);

            if (_isInitializing && _defaultToolModelId != null)
            {
                // Select default value
                AssistentItem.EnteredDisplayMemberModel = AssistentItem.ItemsCollectionView.SourceCollection.OfType<DisplayMemberModel<ToolModel>>().FirstOrDefault(x => x.Item.Id.Equals(_defaultToolModelId));
                _isInitializing = false;
            }
        }

        public void ShowLoadingErrorMessage()
        {
            // Do nothing
        }

        public void SetPictureForToolModel(long toolModelId, Picture picture)
        {
            var toolModel = AssistentItem.ItemsCollectionView.SourceCollection.OfType<DisplayMemberModel<ToolModel>>().FirstOrDefault(x => x.Item.Id.ToLong() == toolModelId).Item;
            toolModel.Picture = picture;
        }

        public void ShowRemoveToolModelsErrorMessage()
        {
            // Do nothing
        }

        public void RemoveToolModels(List<ToolModel> toolModelsToRemove)
        {
            var toolModels = AssistentItem.GetCurrentListItems();
            var idsToRemove = toolModelsToRemove.Select(x => x.Id);
            var removingItems = toolModels.Where(x => idsToRemove.FirstOrDefault(y => y.Equals(x.Id)) != null).ToList();
            
            // Update ListItems
            foreach (var removingItem in removingItems)
            {
                toolModels.Remove(removingItem); 
            }
            AssistentItem.RefillListItems(toolModels);
        }

        public void ShowCmCmk(double cm, double cmk)
        {
            // Do nothing
        }

        public void ShowCmCmkError()
        {
            // Do nothing
        }

        public void AddToolModel(ToolModel toolModel)
        {
            AssistentItem.AddListItem(toolModel);
        }

        public void UpdateToolModel(ToolModel toolModel)
        {
            var toolModels = AssistentItem.GetCurrentListItems();
            var oldToolModel = toolModels.FirstOrDefault(x => x.EqualsById(toolModel));

            if (oldToolModel == null)
            {
                return;
            }

            // Remove the old HelperTableItem and add the updated one
            var index = toolModels.IndexOf(oldToolModel);
            toolModels.Remove(oldToolModel);
            toolModels.Insert(index, toolModel);
            AssistentItem.RefillListItems(toolModels);
        }

        public bool ShowDiffDialog(ToolModelDiff diff)
        {
            return false;
        }

        public void ShowEntryAlreadyExistsMessage(ToolModel updatedToolModel)
        {
            // Do nothing
        }

        public void ShowErrorMessage()
        {
            // Do nothing
        }

        public void ShowRemoveToolModelPreventingReferences(List<ToolReferenceLink> references)
        {
            // Do nothing
        }

        #endregion


        #region Overrides
        public override void Initialize()
        {
            _isInitializing = true;
            _useCase?.ShowToolModels();

            base.Initialize();
        }
        #endregion


        public ToolModelAssistentPlan(IToolModelUseCase useCase, ListAssistentItemModel<ToolModel> item, ToolModelId defaultToolModelId = null) : base(item)
        {
            _useCase = useCase;
            _defaultToolModelId = defaultToolModelId;
        }

        public ToolModelAssistentPlan(ToolModelUseCase useCase, List<AssistentPlan> subPlans, ListAssistentItemModel<ToolModel> item, ToolModelId defaultToolModelId = null) : base(subPlans, item)
        {
            _useCase = useCase;
            _defaultToolModelId = defaultToolModelId;
        }
    }
}
