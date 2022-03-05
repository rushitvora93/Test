using Core.Entities;
using Core.UseCases;
using State;
using System.Reflection;

namespace FrameworksAndDrivers.Data.Xml.DataAccess
{
    public class SessionInformationLocalDataAccess : ISessionInformationDataLocal, ISessionInformationUserGetter
    {
        public string LoadAreaName()
        {
            // There are no Areas yet
            return "";
        }
        
        public string LoadGroupName()
        {
            return SessionInformation.CurrentUser.Group?.GroupName ?? null;
        }
        
        public string LoadServerName()
        {
            return SessionInformation.ServerName;
        }

        public string LoadUserName()
        {
            return SessionInformation.CurrentUser?.UserName ?? null;
        }

        public string LoadQstVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

		public User GetCurrentUser()
		{
			return SessionInformation.CurrentUser;
		}
	}
}
