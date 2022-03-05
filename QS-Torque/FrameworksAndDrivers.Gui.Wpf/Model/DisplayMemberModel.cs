using System;
using System.ComponentModel;
using InterfaceAdapters;
using InterfaceAdapters.Localization;

namespace FrameworksAndDrivers.Gui.Wpf.Model
{
    public abstract class DisplayMemberModel : BindableBase
    {
        public abstract object GetItem();
    }

    public class DisplayMemberModel<T> : DisplayMemberModel
    {
        private string _displayMember;
        public string DisplayMember
        {
            get => _displayMember;
            private set => Set(ref _displayMember, value);
        }

        private T _item;
        public T Item
        {
            get => _item;
            set
            {
                Set(ref _item, value);
                UpdateDisplayMember();

                if (typeof(T).IsSubclassOf(typeof(BindableBase)))
                {
                    PropertyChangedEventManager.AddHandler((_item as BindableBase),Item_PropertyChanged, string.Empty);
                }
            }
        }

        private Func<T, string> _getDisplayMember;


        public override bool Equals(object obj)
        {
            if (obj is DisplayMemberModel<T> other)
            {
                return this.DisplayMember == other.DisplayMember;
            }
            return base.Equals(obj);
        }

        public void UpdateDisplayMember()
        {
            DisplayMember = _getDisplayMember(_item);
        }

        public override object GetItem()
        {
            return Item;
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateDisplayMember();
        }


        public DisplayMemberModel(Func<T, string> getDisplayMember)
        {
            _getDisplayMember = getDisplayMember;
        }

        public DisplayMemberModel(T item, Func<T, string> getDisplayMember) : this(getDisplayMember)
        {
            Item = item;
        }
    }

    public class LocalizedDisplayMemberModel<T> : DisplayMemberModel<T>, IGetUpdatedByLanguageChanges
    {
        private ILocalizationWrapper _localization;

        public void LanguageUpdate()
        {
            UpdateDisplayMember();
        }

        public LocalizedDisplayMemberModel(Func<T, string> getDisplayMember, ILocalizationWrapper localization) : base(getDisplayMember)
        {
            _localization = localization;
            _localization.Subscribe(this);
        }

        public LocalizedDisplayMemberModel(T item, Func<T, string> getDisplayMember, ILocalizationWrapper localization) : base(item, getDisplayMember)
        {
            _localization = localization;
            _localization.Subscribe(this);
        }
    }
}
