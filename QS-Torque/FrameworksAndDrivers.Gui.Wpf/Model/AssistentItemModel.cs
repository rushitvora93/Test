using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using InterfaceAdapters;

namespace FrameworksAndDrivers.Gui.Wpf.Model
{
    public enum AssistentItemType
    {
        ChapterHeading,
        Numeric,
        FloatingPoint,
        Text,
        Boolean,
        Date,
        List,
        Interval
    }

    public abstract class AssistentItemModel : BindableBase
    {
        #region Properties
        private bool _isSet;
        /// <summary>
        /// True if the Value of the AssistentModel is set by default or by the user
        /// </summary>
        public bool IsSet
        {
            get => _isSet;
            set => Set(ref _isSet, value);
        }

        private bool _alreadySelected;
        /// <summary>
        /// True if the user selected this Item in the Assistent (every unselected item is ghosted)
        /// </summary>
        public bool AlreadySelected
        {
            get => _alreadySelected;
            set => Set(ref _alreadySelected, value);
        }

        private AssistentItemType _type;
        /// <summary>
        /// Type of the required Attribute, needed to determine the right InputControl
        /// </summary>
        public AssistentItemType Type
        {
            get => _type;
            set => Set(ref _type, value);
        }

        private string _description;
        /// <summary>
        /// Description of the required AttributeValue, is placed above the InputControl
        /// </summary>
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        private string _attributName;
        /// <summary>
        /// Name of the required attribute, is shown in the ListView on the left of the Assistent (Column "Field")
        /// </summary>
        public string AttributName
        {
            get => _attributName;
            set => Set(ref _attributName, value);
        }

        private string _unit;
        /// <summary>
        /// Unit of the required attributeValue, is placed on the right hand side of the InputControl
        /// </summary>
        public string Unit
        {
            get => _unit;
            set => Set(ref _unit, value);
        }

        private Action<object, AssistentItemModel> _setAttributeValueToResultObject;
        /// <summary>
        /// Funtion that sets the entered value of the AssistentItemModel to the right attribute of the result object
        /// </summary>
        public Action<object, AssistentItemModel> SetAttributeValueToResultObject
        {
            get => _setAttributeValueToResultObject;
            set => Set(ref _setAttributeValueToResultObject, value);
        }

        private Func<AssistentItemModel, string> _warningText;
        /// <summary>
        /// Text that is shown in a MessageBox if the WarningPredicate returns true
        /// </summary>
        public Func<AssistentItemModel, string> WarningText
        {
            get => _warningText;
            set => Set(ref _warningText, value);
        }

        /// <summary>
        /// Predicate that determines if a warning is shown for the AssisetentItemModel-Parameter (warning is shown if Predicate returns true)
        /// </summary>
        public Predicate<AssistentItemModel> WarningCheck { get; set; }

        private string _errorText;
        /// <summary>
        /// Text that is shown in a MessageBox if the ErrorPredicate returns true
        /// </summary>
        public string ErrorText
        {
            get => _errorText;
            set => Set(ref _errorText, value);
        }

        /// <summary>
        /// Predicate that determines if a error is shown for the AssisetentItemModel-Parameter (error is shown if Predicate returns true)
        /// </summary>
        public virtual Predicate<AssistentItemModel> ErrorCheck { get; set; }

        private uint _maxLengthText;
        /// <summary>
        /// Max number of digits/letters that can be put into the TextBox (0 means no limit)
        /// </summary>
        public uint MaxLengthText
        {
            get => _maxLengthText;
            set => Set(ref _maxLengthText, value);
        }

        private DateTime _minimumDate;
        /// <summary>
        /// Minimum date for DatePicker
        /// </summary>
        public DateTime MinimumDate
        {
            get => _minimumDate;
            set => Set(ref _minimumDate, value);
        }

        public List<Behavior> Behaviors { get; private set; }

        private string _hint;
        /// <summary>
        /// Information for the user - Is placed under the input control
        /// </summary>
        public string Hint
        {
            get => _hint;
            set => Set(ref _hint, value);
        }
        #endregion


        /// <param name="type">Type of the required Attribute, needed to determine the right InputControl</param>
        /// <param name="description">Description of the required AttributeValue, is placed above the InputControl</param>
        /// <param name="attributName">Name of the required attribute, is shown in the ListView on the left of the Assistent (Column "Field")</param>
        /// <param name="setAttributeValueOfResultAction">Funtion that sets the entered value of the AssistentItemModel to the right attribute of the result object</param>
        /// <param name="unit">Unit of the required attributeValue, is placed on the right hand side of the InputControl</param>
        /// <param name="warningText">Text that is shown in a MessageBox if the WarningPredicate returns true</param>
        /// <param name="warningPredicate">Predicate that determines if a warning is shown for the AssisetentItemModel-Parameter</param>
        /// <param name="errorText">Text that is shown in a MessageBox if the ErrorPredicate returns true</param>
        /// <param name="errorPredicate">Predicate that determines if a error is shown for the AssisetentItemModel-Parameter</param>
        /// <param name="maxLengthText">Max number of digits/letters that can be put into the TextBox (0 means no limit)</param>
        /// <param name="behaviors">List of behaviours that are attached to the input control in the assistent</param>
        /// <param name="hint">Information for the user - Is placed under the input control</param>
        public AssistentItemModel(AssistentItemType type,
                                  string description,
                                  string attributName,
                                  Action<object, AssistentItemModel> setAttributeValueOfResultAction,
                                  string unit = null,
                                  Func<AssistentItemModel, string> warningText = null,
                                  Predicate<AssistentItemModel> warningPredicate = null,
                                  string errorText = "",
                                  Predicate<AssistentItemModel> errorPredicate = null,
                                  uint maxLengthText = 0,
                                  DateTime? minDate = null,
                                  List<Behavior> behaviors = null,
                                  string hint = "")
        {
            Type = type;
            Description = description;
            AttributName = attributName;
            SetAttributeValueToResultObject = setAttributeValueOfResultAction;
            Unit = unit;
            WarningText = warningText == null ? x => "" : warningText;
            WarningCheck = warningPredicate == null ? x => false : warningPredicate;
            ErrorText = errorText;
            ErrorCheck = errorPredicate == null ? x => false : errorPredicate;
            MaxLengthText = maxLengthText;
            MinimumDate = minDate ?? new DateTime();
            Behaviors = behaviors ?? new List<Behavior>();
            Hint = hint;
        }
    }


    public class AssistentItemModel<T> : AssistentItemModel
    {
        #region Properties
        private T _defaultValue;
        /// <summary>
        /// Default value for the required attribute
        /// </summary>
        public T DefaultValue
        {
            get => _defaultValue;
            set
            {
                Set(ref _defaultValue, value);
                EnteredValue = value;
            }
        }

        private T _enteredValue;
        /// <summary>
        /// Value that is entered by the user (contains the DefaultValue at the beginning)
        /// </summary>
        public T EnteredValue
        {
            get => _enteredValue;
            set
            {
                Set(ref _enteredValue, value);
                IsSet = value != null;
            }
        }
        #endregion

        /// <param name="type">Type of the required Attribute, needed to determine the right InputControl</param>
        /// <param name="description">Description of the required AttributeValue, is placed above the InputControl</param>
        /// <param name="attributName">Name of the required attribute, is shown in the ListView on the left of the Assistent (Column "Field")</param>
        /// <param name="defaultValue">Default value for the required attribute</param>
        /// <param name="setAttributeValueOfResultAction">Funtion that sets the entered value of the AssistentItemModel to the right attribute of the result object</param>
        /// <param name="unit">Unit of the required attributeValue, is placed on the right hand side of the InputControl</param>
        /// <param name="warningText">Text that is shown in a MessageBox if the WarningPredicate returns true</param>
        /// <param name="warningCheck">Predicate that determines if a warning is shown for the AssisetentItemModel-Parameter</param>
        /// <param name="errorText">Text that is shown in a MessageBox if the ErrorPredicate returns true</param>
        /// <param name="errorCheck">Predicate that determines if a error is shown for the AssisetentItemModel-Parameter</param>
        /// <param name="maxLengthText">Max number of digits/letters that can be put into the TextBox (0 means no limit)</param>
        /// <param name="behaviors">List of behaviours that are attached to the input control in the assistent</param>
        /// <param name="hint">Information for the user - Is placed under the input control</param>
        public AssistentItemModel(AssistentItemType type,
                                  string description,
                                  string attributName,
                                  T defaultValue,
                                  Action<object, AssistentItemModel> setAttributeValueOfResultAction,
                                  string unit = null,
                                  Func<AssistentItemModel, string> warningText = null,
                                  Predicate<AssistentItemModel> warningCheck = null,
                                  string errorText = "",
                                  Predicate<AssistentItemModel> errorCheck = null,
                                  uint maxLengthText = 0,
                                  DateTime? minDate = null,
                                  List<Behavior> behaviors = null,
                                  string hint = "")
            : base(type,
                   description,
                   attributName,
                   setAttributeValueOfResultAction,
                   unit,
                   warningText,
                   warningCheck,
                   errorText,
                   errorCheck,
                   maxLengthText,
                   minDate,
                   behaviors,
                   hint)
        {
            DefaultValue = defaultValue;
        }
    }


    public class ChapterHeadingAssistentItemModel : AssistentItemModel
    {
        /// <param name="headerText">Heading of a assistent chapter</param>
        /// <param name="hint">Information for the user - Is placed under the input control</param>
        public ChapterHeadingAssistentItemModel(string headerText, string hint = "") :
            base(AssistentItemType.ChapterHeading,
                "",
                headerText,
                (o, i) => { },
                hint:hint)
        {
            IsSet = true;
        }
    }


    public class UniqueAssistentItemModel<T> : AssistentItemModel<T>
    {
        #region Properties
        private Predicate<T> _isValueUnique;

        /// <summary>
        /// Predicate that determines if a error is shown for the AssisetentItemModel-Parameter (it is automatically checked wether the Value is unique in the contextList)
        /// </summary>
        public override Predicate<AssistentItemModel> ErrorCheck
        {
            get => (x) => base.ErrorCheck(x) || !_isValueUnique((x as UniqueAssistentItemModel<T>).EnteredValue);
            set => base.ErrorCheck = value;
        }
        #endregion


        /// <param name="isValueUnique">Function that returns a list in which the enterd value of the UniqueAssistentItemModel has to be unique</param>
        /// <param name="type">Type of the required Attribute, needed to determine the right InputControl</param>
        /// <param name="description">Description of the required AttributeValue, is placed above the InputControl</param>
        /// <param name="attributName">Name of the required attribute, is shown in the ListView on the left of the Assistent (Column "Field")</param>
        /// <param name="defaultValue">Default value for the required attribute</param>
        /// <param name="setAttributeValueOfResultAction">Funtion that sets the entered value of the AssistentItemModel to the right attribute of the result object</param>
        /// <param name="unit">Unit of the required attributeValue, is placed on the right hand side of the InputControl</param>
        /// <param name="warningText">Text that is shown in a MessageBox if the WarningPredicate returns true</param>
        /// <param name="warningCheck">Predicate that determines if a warning is shown for the AssisetentItemModel-Parameter</param>
        /// <param name="errorText">Text that is shown in a MessageBox if the ErrorPredicate returns true (it is automatically checked wether the Value is unique in the contextList)</param>
        /// <param name="errorCheck">Predicate that determines if a error is shown for the AssisetentItemModel-Parameter</param>
        /// <param name="maxLengthText">Max number of digits/letters that can be put into the TextBox (0 means no limit)</param>
        /// <param name="behaviors">List of behaviours that are attached to the input control in the assistent</param>
        /// <param name="hint">Information for the user - Is placed under the input control</param>
        public UniqueAssistentItemModel(Predicate<T> isValueUnique,
                                        AssistentItemType type,
                                        string description,
                                        string attributName,
                                        T defaultValue,
                                        Action<object, AssistentItemModel> setAttributeValueOfResultAction,
                                        string errorText,
                                        string unit = null,
                                        Func<AssistentItemModel, string> warningText = null,
                                        Predicate<AssistentItemModel> warningCheck = null,
                                        Predicate<AssistentItemModel> errorCheck = null,
                                        uint maxLengthText = 0,
                                        DateTime? minDate = null,
                                        List<Behavior> behaviors = null,
                                        string hint = "")
            : base(type,
                   description,
                   attributName,
                   defaultValue,
                   setAttributeValueOfResultAction,
                   unit,
                   warningText,
                   warningCheck,
                   errorText,
                   errorCheck,
                   maxLengthText,
                   minDate,
                   behaviors,
                   hint)
        {
            _isValueUnique = isValueUnique;
        }
    }


    public class ListAssistentItemModel<T> : AssistentItemModel<T>
    {
        #region Properties
        private Dispatcher _guiDiaptcher;

        private ObservableCollection<DisplayMemberModel<T>> _listItems;
        /// <summary>
        /// ListCollectionView with all Items that are shown in the table, where the user can choose the value for this AssistentItem
        /// </summary>
        public ListCollectionView ItemsCollectionView { get; private set; }

        private DisplayMemberModel<T> _enteredDisplayMemberModel;
        public DisplayMemberModel<T> EnteredDisplayMemberModel
        {
            get => _enteredDisplayMemberModel;
            set
            {
                Set(ref _enteredDisplayMemberModel, value);
                if (value != null)
                {
                    EnteredValue = value.Item; 
                }
                else
                {
                    EnteredValue = DefaultValue;

                    if (EnteredValue == null)
                    {
                        IsSet = false;
                        CommandManager.InvalidateRequerySuggested();
                    }
                }
            }
        }

        private string _helperTableButtonText;
        /// <summary>
        /// Text for the HelperTableButton, Button is placed under the ListBox with the HelperTableItems, if this string is null the Button is not visible
        /// </summary>
        public string HelperTableButtonText
        {
            get => _helperTableButtonText;
            set
            {
                if(value == "")
                {
                    Set(ref _helperTableButtonText, null);
                }
                else
                {
                    Set(ref _helperTableButtonText, value); 
                }
            }
        }
        
        /// <summary>
        /// Returns the Value of the displayed property for a HelperTableItem
        /// </summary>
        public Func<T, string> GetDisplayMember { get; set; }

        /// <summary>
        /// Action that is invoked if the HelperTableButton is clicked
        /// </summary>
        public Action OpenHelperTable { get; set; }
        #endregion


        #region Commands
        public RelayCommand OpenHelperTableCommand { get; private set; }
        private bool OpenHelperTableCanExecute(object arg) { return true; }
        private void OpenHelperTableExecute(object obj)
        {
            OpenHelperTable();
        }
        #endregion


        #region Methods
        public void SetDefaultValue(T defaultvalue)
        {
            EnteredDisplayMemberModel = _listItems.FirstOrDefault(x => x.Item.Equals(defaultvalue));
        }

        public void RefillListItems(IEnumerable<T> newListItems)
        {
            _guiDiaptcher.Invoke(() =>
            {
                _listItems.Clear();

                foreach (var i in newListItems)
                {
                    _listItems.Add(new DisplayMemberModel<T>(i, GetDisplayMember));
                }
            });
        }

        public void AddListItem(T item)
        {
            _guiDiaptcher.Invoke(() => { _listItems.Add(new DisplayMemberModel<T>(item, GetDisplayMember)); });
        }

        public void InsertListItem(int index, T item)
        {
            _guiDiaptcher.Invoke(() => { _listItems.Insert(index, new DisplayMemberModel<T>(item, GetDisplayMember)); });
        }

        public List<T> GetCurrentListItems()
        {
            return _listItems.Select(x => x.Item).ToList();
        }
        #endregion


        /// <param name="items">All items that are shown in the List in the Assistent by default</param>
        /// <param name="type">Type of the required Attribute, needed to determine the right InputControl</param>
        /// <param name="description">Description of the required AttributeValue, is placed above the InputControl</param>
        /// <param name="attributName">Name of the required attribute, is shown in the ListView on the left of the Assistent (Column "Field")</param>
        /// <param name="defaultValue">Default value for the required attribute</param>
        /// <param name="setAttributeValueOfResultAction">Funtion that sets the entered value of the AssistentItemModel to the right attribute of the result object</param>
        /// <param name="helperTableButtonText">Text for the HelperTableButton, Button is placed under the ListBox with the HelperTableItems, if this string is null the Button is not visible</param>
        /// <param name="getDisplayMember">Returns the Value of the displayed property for a HelperTableItem</param>
        /// <param name="openHelperTable">Action that is invoked if the HelperTableButton is clicked</param>
        /// <param name="unit">Unit of the required attributeValue, is placed on the right hand side of the InputControl</param>
        /// <param name="warningText">Text that is shown in a MessageBox if the WarningPredicate returns true</param>
        /// <param name="warningPredicate">Predicate that determines if a warning is shown for the AssisetentItemModel-Parameter</param>
        /// <param name="errorText">Text that is shown in a MessageBox if the ErrorPredicate returns true</param>
        /// <param name="errorPredicate">Predicate that determines if a error is shown for the AssisetentItemModel-Parameter</param>
        /// <param name="maxLengthText">Max number of digits/letters that can be put into the TextBox (0 means no limit)</param>
        public ListAssistentItemModel(Dispatcher guiDispatcher,
                                      IEnumerable<T> items,
                                      string description,
                                      string attributName,
                                      T defaultValue,
                                      Action<object, AssistentItemModel> setAttributeValueOfResultAction,
                                      string helperTableButtonText,
                                      Func<T, string> getDisplayMember,
                                      Action openHelperTable,
                                      string unit = null,
                                      Func<AssistentItemModel, string> warningText = null,
                                      Predicate<AssistentItemModel> warningPredicate = null,
                                      string errorText = "",
                                      Predicate<AssistentItemModel> errorPredicate = null,
                                      uint maxLengthText = 0)
            : base(AssistentItemType.List,
                   description,
                   attributName,
                   defaultValue,
                   setAttributeValueOfResultAction,
                   unit,
                   warningText,
                   warningPredicate,
                   errorText,
                   errorPredicate,
                   maxLengthText)
        {
            _guiDiaptcher = guiDispatcher;

            // Fill _listItems
            _listItems = new ObservableCollection<DisplayMemberModel<T>>();
            ItemsCollectionView = new ListCollectionView(_listItems);
            ItemsCollectionView.SortDescriptions.Add(new System.ComponentModel.SortDescription(nameof(DisplayMemberModel<T>.DisplayMember), System.ComponentModel.ListSortDirection.Ascending));
            ItemsCollectionView.IsLiveSorting = true;
            
            HelperTableButtonText = helperTableButtonText;
            GetDisplayMember = getDisplayMember;
            OpenHelperTable = openHelperTable;
            
            // Set default list
            RefillListItems(items);

            // Set default Value
            EnteredDisplayMemberModel = _listItems.FirstOrDefault(x => x.Item.Equals(defaultValue));

            // Initialize Commands
            OpenHelperTableCommand = new RelayCommand(OpenHelperTableExecute, OpenHelperTableCanExecute);
        }

        /// <param name="type">Type of the required Attribute, needed to determine the right InputControl</param>
        /// <param name="description">Description of the required AttributeValue, is placed above the InputControl</param>
        /// <param name="attributName">Name of the required attribute, is shown in the ListView on the left of the Assistent (Column "Field")</param>
        /// <param name="defaultValue">Default value for the required attribute</param>
        /// <param name="setAttributeValueOfResultAction">Funtion that sets the entered value of the AssistentItemModel to the right attribute of the result object</param>
        /// <param name="helperTableButtonText">Text for the HelperTableButton, Button is placed under the ListBox with the HelperTableItems, if this string is null or empty the Button is not visible</param>
        /// <param name="getDisplayMember">Returns the Value of the displayed property for a HelperTableItem</param>
        /// <param name="openHelperTable">Action that is invoked if the HelperTableButton is clicked</param>
        /// <param name="unit">Unit of the required attributeValue, is placed on the right hand side of the InputControl</param>
        /// <param name="warningText">Text that is shown in a MessageBox if the WarningPredicate returns true</param>
        /// <param name="warningPredicate">Predicate that determines if a warning is shown for the AssisetentItemModel-Parameter</param>
        /// <param name="errorText">Text that is shown in a MessageBox if the ErrorPredicate returns true</param>
        /// <param name="errorPredicate">Predicate that determines if a error is shown for the AssisetentItemModel-Parameter</param>
        /// <param name="maxLengthText">Max number of digits/letters that can be put into the TextBox (0 means no limit)</param>
        public ListAssistentItemModel(Dispatcher guiDispatcher,
                                      string description,
                                      string attributName,
                                      T defaultValue,
                                      Action<object, AssistentItemModel> setAttributeValueOfResultAction,
                                      string helperTableButtonText,
                                      Func<T, string> getDisplayMember,
                                      Action openHelperTable,
                                      string unit = null,
                                      Func<AssistentItemModel, string> warningText = null,
                                      Predicate<AssistentItemModel> warningPredicate = null,
                                      string errorText = "",
                                      Predicate<AssistentItemModel> errorPredicate = null,
                                      uint maxLengthText = 0)
            : this(guiDispatcher,
                   new List<T>(),
                   description,
                   attributName,
                   defaultValue,
                   setAttributeValueOfResultAction,
                   helperTableButtonText,
                   getDisplayMember,
                   openHelperTable,
                   unit,
                   warningText,
                   warningPredicate,
                   errorText,
                   errorPredicate,
                   maxLengthText)
        {

        }
    }
}
