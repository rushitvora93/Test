using System.Collections.Generic;
using System.Security;
using Core.Entities;
using Core.UseCases;

namespace StartUp
{
    public class LoginGuiProxy : ILoginGui
	{
		public ILoginGui real;

		public void CloseLogin()
		{
			real.CloseLogin();
		}

		public void EnableLogin(bool status)
		{
			real.EnableLogin(status);
		}

		public void EnableServerConnectionSelection(bool enable)
		{
			real.EnableServerConnectionSelection(enable);
		}

        public void ShowLicenseErrorMessage(string msg)
        {
            real.ShowLicenseErrorMessage(msg);
        }

        public Group GetGroup()
		{
			return real.GetGroup();
		}

		public SecureString GetPassword()
		{
			return real.GetPassword();
		}

		public ServerConnection GetServerConnection()
		{
			return real.GetServerConnection();
		}

		public string GetUserName()
		{
			return real.GetUserName();
		}

		public void ShowConnectServerConnectionError()
		{
			real.ShowConnectServerConnectionError();
		}

		public void ShowGenericErrorMessage()
		{
			real.ShowGenericErrorMessage();
		}

		public void ShowGroups(List<Group> groups)
		{
			real.ShowGroups(groups);
		}

		public void ShowLoginNotSuccessfullMessage()
		{
			real.ShowLoginNotSuccessfullMessage();
		}

		public void ShowReadServerConnectionsError()
		{
			real.ShowReadServerConnectionsError();
		}

		public void ShowServerConnections(List<ServerConnection> serverConnections, string lastServerConnectionName)
		{
			real.ShowServerConnections(serverConnections, lastServerConnectionName);
		}

		public void ShowSuggestionUserNames(List<string> suggestionUserNames)
		{
			real.ShowSuggestionUserNames(suggestionUserNames);
		}

		public void ShowTimeoutMessage()
		{
			real.ShowTimeoutMessage();
		}
	}
}
