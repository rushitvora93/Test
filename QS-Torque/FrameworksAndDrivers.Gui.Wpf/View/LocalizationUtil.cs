using FrameworksAndDrivers.Localization;
using InterfaceAdapters.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    public class LocalizationUtil : INotifyPropertyChanged, IGetUpdatedByLanguageChanges
    {
        public static LocalizationUtil Localization { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private LocalizationWrapper _wrapper;

        private DateTimeFormatInfo _dateTimeFormatInfo;
        public DateTimeFormatInfo DateTimeFormatInfo
        {
            get => _dateTimeFormatInfo;
            set
            {
                _dateTimeFormatInfo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateTimeFormatInfo)));
            }
        }

        private CultureInfo _currentCulture;

        public CultureInfo CurrentCulture
        {
            get => _currentCulture;
            set 
            {
                _currentCulture = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentCulture)));
            }
        }


        public void LanguageUpdate()
        {
            DateTimeFormatInfo = CultureInfo.CurrentCulture.DateTimeFormat;
            CurrentCulture = CultureInfo.CurrentCulture;
        }

        public LocalizationUtil(LocalizationWrapper wrapper)
        {
            _wrapper = wrapper;
            _wrapper.Subscribe(this);
            LanguageUpdate();
        }
    }
}
