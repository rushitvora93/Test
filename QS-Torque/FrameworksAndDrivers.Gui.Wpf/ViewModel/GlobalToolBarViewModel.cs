using Client.UseCases.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class GlobalToolBarViewModel: BindableBase, INotifyPropertyChanged, ILanguageErrorHandler
    {
        private ILanguageUseCase _languageUseCase;
        private ILanguageInterface _languageInterface;
        private ILocalizationWrapper _localization;

        public string Language
        {
            get => _languageInterface.Language;
            set
            {
                _languageUseCase.SetDefaultLanguage(value, this);
            }
        }

        private void LoadedExecute(object obj)
        {
            _languageUseCase.GetLastLanguage(this);
        }

        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;

        public RelayCommand LoadedCommand { get; private set; }

        private bool LoadedCanExecute(object arg) { return true; }

        public void ShowLanguageError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("LoginView", "Error occured while loading language"),
                _localization.Strings.GetParticularString("LoginView", "Error"),
                MessageBoxButton.OK, MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        private void WireViewModelToTestLevelSetInterface()
        {
            PropertyChangedEventManager.AddHandler(
              _languageInterface,
              (s, e) => RaisePropertyChanged(nameof(Language)),
              nameof(LanguageInterfaceAdapter.Language));
        }

        public GlobalToolBarViewModel(ILocalizationWrapper localization,ILanguageUseCase languageUseCase, ILanguageInterface languageInterface)
        {
            _languageUseCase = languageUseCase;
            _languageInterface = languageInterface;
            _localization = localization;

            WireViewModelToTestLevelSetInterface();

            LoadedCommand = new RelayCommand(LoadedExecute, LoadedCanExecute);
        }
    }
}
