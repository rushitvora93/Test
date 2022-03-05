using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.Model;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.ReferenceLink;

namespace FrameworksAndDrivers.Gui.Wpf.Assistent
{
    public class ManufacturerAssistentPlan : ListAssistentPlan<Manufacturer>, IManufacturerGui
    {
        private IManufacturerUseCase _useCase;
        private ManufacturerId _defaultManufacturerId;
        private bool _isInitializing = false;           // True if ShowManufacturers is called at the initializing


        #region Interface Implementations
        public void AddManufacturer(Manufacturer manufacturer)
        {
            // Add the new Manufacturer to the ListItems of the AsstentItemModel
            AssistentItem.AddListItem(manufacturer);
        }

        public void RemoveManufacturer(Manufacturer manufacturer)
        {
            // Get the Manufacturer of the ListItems, that has to be removed
            var manufacturers = AssistentItem.GetCurrentListItems();
            var removingManufacturer = manufacturers.FirstOrDefault(x => x.EqualsById(manufacturer));

            if (removingManufacturer == null)
            {
                return;
            }

            // Update ListItems
            manufacturers.Remove(removingManufacturer);
            AssistentItem.RefillListItems(manufacturers);
        }

        public void SaveManufacturer(Manufacturer manufacturer)
        {
            // Get the Manufacturer to the ListItems, that has to be updated
            var manufacturers = AssistentItem.GetCurrentListItems();
            var oldManufacturerModel = manufacturers.FirstOrDefault(x => x.EqualsById(manufacturer));

            if (oldManufacturerModel == null)
            {
                return;
            }

            // Remove the old Manufacturer and add the updated one
            var index = manufacturers.IndexOf(oldManufacturerModel);
            manufacturers.Remove(oldManufacturerModel);
            manufacturers.Insert(index, manufacturer);
            AssistentItem.RefillListItems(manufacturers);
        }

        public void ShowManufacturer(List<Manufacturer> manufacturer)
        {
            // Add all Manufacturers to the ListItems of the AsstentItemModel
            AssistentItem.RefillListItems(manufacturer);

            if(_isInitializing && _defaultManufacturerId != null)
            {
                // Select default value
                AssistentItem.EnteredDisplayMemberModel = AssistentItem.ItemsCollectionView.SourceCollection.OfType<DisplayMemberModel<Manufacturer>>().FirstOrDefault(x => x.Item.Id.Equals(_defaultManufacturerId));
                _isInitializing = false;
            }
        }

        public void ShowComment(Manufacturer manufacturer, string comment)
        {
            // Do nothing
        }

        public void ShowCommentError()
        {
            // Do nothing
        }

        public void ShowErrorMessage()
        {
            // Do nothing
        }

        public void ShowReferencedToolModels(List<ToolModelReferenceLink> toolModelReferenceLinks)
        {
            // Do nothing
        }

        public void ShowReferencesError()
        {
            // Do nothing
        }

        public void ShowRemoveManufacturerPreventingReferences(List<ToolModelReferenceLink> references)
        {
            //Do nothing
        }

        #endregion


        #region Overrides
        public override void Initialize()
        {
            _isInitializing = true;
            _useCase?.LoadManufacturer();
            
            base.Initialize();
        }
        #endregion


        public ManufacturerAssistentPlan(IManufacturerUseCase useCase, ListAssistentItemModel<Manufacturer> item, ManufacturerId defaultManufacturerId = null) : base(item)
        {
            _useCase = useCase;
            _defaultManufacturerId = defaultManufacturerId;
        }

        public ManufacturerAssistentPlan(IManufacturerUseCase useCase, List<AssistentPlan> subPlans, ListAssistentItemModel<Manufacturer> item, ManufacturerId defaultManufacturerId = null) : base(subPlans, item)
        {
            _useCase = useCase;
            _defaultManufacturerId = defaultManufacturerId;
        }
    }
}
