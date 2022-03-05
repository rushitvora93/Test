using Core.UseCases;

namespace StartUp
{
    public class QstInformationGuiProxy : IQstInformationGui
	{
		public IQstInformationGui real;

		public void ShowComputerName(string computerName)
		{
			real.ShowComputerName(computerName);
		}

		public void ShowLogPackageSuccessMessage()
		{
			real.ShowLogPackageSuccessMessage();
		}

		public void ShowQstVersion(string qstVersion)
		{
			real.ShowQstVersion(qstVersion);
		}

		public void ShowServerVersion(string serverVersion)
		{
			real.ShowServerVersion(serverVersion);
		}
	}
}
