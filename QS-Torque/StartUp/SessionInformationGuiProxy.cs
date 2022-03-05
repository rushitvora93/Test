using Core.UseCases;

namespace StartUp
{
    public class SessionInformationGuiProxy : ISessionInformationGui
	{
		public ISessionInformationGui real;

        public void LoadMegaMainMenuIsPinned(bool isPinned)
        {
			real.LoadMegaMainMenuIsPinned(isPinned);
        }

        public void ShowAreaName(string areaName)
		{
			real.ShowAreaName(areaName);
		}

		public void ShowGroupName(string groupName)
		{
			real.ShowGroupName(groupName);
		}

		public void ShowQstVersion(string qstVersion)
		{
			real.ShowQstVersion(qstVersion);
		}

		public void ShowServerName(string serverName)
		{
			real.ShowServerName(serverName);
		}

		public void ShowUserName(string userName)
		{
			real.ShowUserName(userName);
		}
	}
}
