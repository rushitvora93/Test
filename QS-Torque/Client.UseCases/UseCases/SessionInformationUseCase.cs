using log4net;
using System;
using System.Threading.Tasks;

namespace Core.UseCases
{
    public interface ISessionInformationDataLocal
    {
        string LoadUserName();
        string LoadServerName();
        string LoadGroupName();
        string LoadAreaName();
        string LoadQstVersion();
    }

    public interface ISessionInformationGui
    {
        void ShowUserName(string userName);
        void ShowServerName(string serverName);
        void ShowGroupName(string groupName);
        void ShowAreaName(string areaName);
        void ShowQstVersion(string qstVersion);
        void LoadMegaMainMenuIsPinned(bool isPinned);
    }

    public interface ISessionInformationRegistryDataAccess
    {
        void SetMegaMainMenuIsPinned(bool isPinned);
        bool LoadMegaMainMenuIsPinned();
    }

    public interface ISessionInformationErrorHandler
    {
        void ShowMegaMenuLockingError();
    }

    public interface ISessionInformationUseCase
    {
        public void LoadUserData();
        public bool IsCspUser();
        public void SetMegaMainMenuIsPinned(ISessionInformationErrorHandler errorHandler, bool isPinned);
        public void LoadMegaMainMenuIsPinned(ISessionInformationErrorHandler errorHandler);
    }

    public class SessionInformationUseCase : ISessionInformationUseCase
    {
        private ISessionInformationGui _guiInterface;
        private ISessionInformationDataLocal _sessionInformationLocalData;
        private readonly ISessionInformationUserGetter _sessionInformationUserGetter;
        private ISessionInformationRegistryDataAccess _registryDataAccess;
        private static readonly ILog _log = LogManager.GetLogger(typeof(SessionInformationUseCase));

        public virtual void LoadUserData()
        {
            _guiInterface.ShowUserName(_sessionInformationLocalData.LoadUserName());
            _guiInterface.ShowServerName(_sessionInformationLocalData.LoadServerName());
            _guiInterface.ShowGroupName(_sessionInformationLocalData.LoadGroupName());
            _guiInterface.ShowAreaName(_sessionInformationLocalData.LoadAreaName());
            _guiInterface.ShowQstVersion(_sessionInformationLocalData.LoadQstVersion());
        }

        public SessionInformationUseCase(ISessionInformationDataLocal sessionInformationLocalData, ISessionInformationUserGetter sessionInformationUserGetter, ISessionInformationGui guiInterface, ISessionInformationRegistryDataAccess registryAccess)
        {
            _sessionInformationLocalData = sessionInformationLocalData;
            _sessionInformationUserGetter = sessionInformationUserGetter;
            _guiInterface = guiInterface;
            _registryDataAccess = registryAccess;
        }

        public bool IsCspUser()
        {
            return _sessionInformationUserGetter?.GetCurrentUser()?.UserId?.ToLong() == -100;
        }

        public void SetMegaMainMenuIsPinned(ISessionInformationErrorHandler errorHandler, bool isPinned)
        {
            try
            {
                _registryDataAccess.SetMegaMainMenuIsPinned(isPinned);
                _guiInterface.LoadMegaMainMenuIsPinned(isPinned);
            }
            catch(Exception e)
            {
                _log.Error("Error in SetMegaMainMenuIsPinned");
                errorHandler.ShowMegaMenuLockingError();
            }
            
        }

        public void LoadMegaMainMenuIsPinned(ISessionInformationErrorHandler errorHandler)
        {
            try
            {
                _guiInterface.LoadMegaMainMenuIsPinned(_registryDataAccess.LoadMegaMainMenuIsPinned());
            }
            catch (Exception e)
            {
                _log.Error("Error in LoadMegaMainMenuIsPinned");
                errorHandler.ShowMegaMenuLockingError();
            }
        }
    }

    public class SessionInformationHumbleAsyncRunner : ISessionInformationUseCase
    {
        private ISessionInformationUseCase _real;

        public SessionInformationHumbleAsyncRunner(ISessionInformationUseCase real)
        {
            _real = real;
        }

        public bool IsCspUser()
        {
            return _real.IsCspUser();
        }

        public void LoadMegaMainMenuIsPinned(ISessionInformationErrorHandler errorHandler)
        {
            Task.Run(() => _real.LoadMegaMainMenuIsPinned(errorHandler));
        }

        public void LoadUserData()
        {
            _real.LoadUserData();
        }

        public void SetMegaMainMenuIsPinned(ISessionInformationErrorHandler errorHandler, bool isPinned)
        {
            Task.Run(() => _real.SetMegaMainMenuIsPinned(errorHandler,isPinned));
        }
    }

    public class SessionInformationUseCaseNull : SessionInformationUseCase
	{
		public SessionInformationUseCaseNull(ISessionInformationDataLocal sessionInformationLocalData, ISessionInformationUserGetter sessionInformationUserGetter, ISessionInformationGui guiInterface, ISessionInformationRegistryDataAccess registryAccess)
			: base(sessionInformationLocalData, sessionInformationUserGetter, guiInterface, registryAccess)
		{

		}

		public override void LoadUserData()
		{

		}
	}
}
