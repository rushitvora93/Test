using log4net;

namespace Core.UseCases
{
    public interface ILogoutGui
    {
        void FinishLogout();
    }

    public interface ILogoutData
    {
        void Logout();
    }

    public interface ILogoutUseCase
    {
        void LogoutAsync();
    }

    public class LogoutUseCase : ILogoutUseCase
    {
        private ILogoutData _data;
        private ILogoutGui _gui;
        private static readonly ILog Log = LogManager.GetLogger(typeof(ClassicTestUseCase));

        public LogoutUseCase(ILogoutData data, ILogoutGui gui)
        {
            _data = data;
            _gui = gui;
        }

        public void LogoutAsync()
        {
            _data.Logout();
            _gui.FinishLogout();
        }
    }
}
