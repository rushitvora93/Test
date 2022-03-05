using System;
using Core.UseCases;
using InterfaceAdapters;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class MainViewViewModel : BindableBase, ILogoutGui
    {
        private ILogoutUseCase _logoutUseCase;
        public EventHandler LogoutCompletedEventHandler { get; set; }

        public MainViewViewModel(ILogoutUseCase logoutUseCase)
        {
            _logoutUseCase = logoutUseCase;
        }

        public void Logout()
        {
            _logoutUseCase.LogoutAsync();
        }

        public void FinishLogout()
        {
            LogoutCompletedEventHandler.Invoke(this, System.EventArgs.Empty);
        }
    }
}