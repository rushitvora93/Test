using System;
using Core.Entities;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;

namespace Core.UseCases
{

    using Port = UInt16;

    public class ServerConnection : IEquatable<ServerConnection>
    {
        public string ServerName { get; set; }
        public string HostName { get; set; }
        public Port Port { get; set; }
        public string PrincipalName { get; set; }

        public bool Equals(ServerConnection other)
        {
            return HostName == other?.HostName
                   && ServerName == other?.ServerName
                   && Port == other?.Port
                   && PrincipalName == other?.PrincipalName;
        }
    }

    public interface IServerConnectionStorage
    {
        List<ServerConnection> LoadServerConnections();
        List<string> LoadSuggestionUserNames();
        void AddUserNameToSuggestions(string userName);
        void AddLastServerConnection(ServerConnection serverConnection);
        string LoadLastServerConnectionName();
    }

    public interface ILoginData
    {
        Task<List<Group>> LoadGroupsForUserNameAsync(string username);
        Task SetupServerConnectionAsync(ServerConnection serverConnection);
        Task LoginAsync(string userName, SecureString password, Group group);
    }

    public interface ILoginGui
    {
        ServerConnection GetServerConnection();
        string GetUserName();
        SecureString GetPassword();
        Group GetGroup();

        void ShowServerConnections(List<ServerConnection> serverConnections, string lastServerConnectionName);
        void ShowSuggestionUserNames(List<string> suggestionUserNames);
        void ShowGroups(List<Group> groups);
        void CloseLogin();
        void ShowLoginNotSuccessfullMessage();
        void ShowTimeoutMessage();
        void ShowGenericErrorMessage();
        void ShowReadServerConnectionsError();
        void ShowConnectServerConnectionError();
        void EnableLogin(bool status);
        void EnableServerConnectionSelection(bool enable);

        void ShowLicenseErrorMessage(string msg);
    }

    public class LoginFailedException : Exception
    {
        public LoginFailedException(Exception innerException, string message = "") : base(message, innerException)
        {
        }
    }

    public class LicenseFailedException : Exception
    {
        public LicenseFailedException(Exception innerException, string message = "") : base(message, innerException)
        {
        }
    }


    public class LoginUseCase
    {
        IServerConnectionStorage _xmlDataInterface;
        ILoginData _dataInterface;
        ILoginGui _guiInterface;
        ResponseTracking<List<Group>> loadGroupTracking = new ResponseTracking<List<Group>>();

        #region Methods

        public virtual void LoadServerConnections()
        {
            try
            {
                _guiInterface.ShowServerConnections(_xmlDataInterface.LoadServerConnections(), _xmlDataInterface.LoadLastServerConnectionName());
            }
            catch (Exception)
            {
                //TODO: Logging
                _guiInterface.ShowReadServerConnectionsError();
            }
        }

        public virtual void LoadSuggestionUserNames()
        {
            _guiInterface.ShowSuggestionUserNames(_xmlDataInterface.LoadSuggestionUserNames());
        }

        public async void LoadGroupsForUserName()
        {
            try
            {
                var thisResponse = loadGroupTracking.Add(new List<Group>());
                var userName = _guiInterface.GetUserName();
                loadGroupTracking.Update(thisResponse, await _dataInterface.LoadGroupsForUserNameAsync(userName));
                _guiInterface.ShowGroups(loadGroupTracking.TopResponse());
                loadGroupTracking.Cleanup();
            }
            catch (Exception)
            {
                //TODO: Logging
            }
        }

        public async void Login()
        {
            var userName = _guiInterface.GetUserName();
            var password = _guiInterface.GetPassword();
            var group = _guiInterface.GetGroup();
            try
            {
                _guiInterface.EnableLogin(false);
                _guiInterface.EnableServerConnectionSelection(false);
                await _dataInterface.LoginAsync(userName, password, group);
                _xmlDataInterface.AddUserNameToSuggestions(userName);
                var serverConnection = _guiInterface.GetServerConnection();
                _xmlDataInterface.AddLastServerConnection(serverConnection);
                _guiInterface.CloseLogin();
            }
            catch (LoginFailedException)
            {
                //TODO: Logging
                _guiInterface.ShowLoginNotSuccessfullMessage();
            }
            catch (TimeoutException)
            {
                //TODO: Logging
                _guiInterface.ShowTimeoutMessage();
            }
            catch (LicenseFailedException e)
            {
                _guiInterface.ShowLicenseErrorMessage(e.Message);
            }
            catch (Exception ex)
            {
                //TODO: Logging
                _guiInterface.ShowGenericErrorMessage();
            }
            finally
            {
                _guiInterface.EnableLogin(true);
                _guiInterface.EnableServerConnectionSelection(true);
            }
        }

        public virtual async void SetupServerConnection()
        {
            try
            {
                _guiInterface.EnableServerConnectionSelection(false);
                var serverConnection = _guiInterface.GetServerConnection();
                await _dataInterface.SetupServerConnectionAsync(serverConnection);
                _guiInterface.EnableLogin(true);
            }
            catch (LicenseFailedException e)
            {
                _guiInterface.EnableLogin(false);
                _guiInterface.ShowLicenseErrorMessage(e.Message);
            }
            catch (Exception)
            {
                //TODO: Logging
                _guiInterface.EnableLogin(false);
                _guiInterface.ShowConnectServerConnectionError();
            }
            finally
            {
                _guiInterface.EnableServerConnectionSelection(true);
            }
        }
        #endregion

        public LoginUseCase(IServerConnectionStorage xmlDataInterface, ILoginData dataInterface, ILoginGui guiInterface)
        {
            _xmlDataInterface = xmlDataInterface;
            _dataInterface = dataInterface;
            _guiInterface = guiInterface;
        }
    }
}
