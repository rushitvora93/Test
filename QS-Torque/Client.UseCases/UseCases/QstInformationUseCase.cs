namespace Core.UseCases
{
    public interface IQstInformationDataLocal
    {
        string LoadQstVersion();
        string LoadComputerName();
        void CreateLogPackage(string filePath);
    }

    public interface IQstInformationData
    {
        string LoadServerVersion();
    }

    public interface IQstInformationGui
    {
        void ShowQstVersion(string qstVersion);
        void ShowServerVersion(string serverVersion);
        void ShowComputerName(string computerName);
        void ShowLogPackageSuccessMessage();
    }


    public class QstInformationUseCase
    {
        IQstInformationDataLocal _dataInterfaceLocal;
        IQstInformationData _dataInterface;
        IQstInformationGui _guiInterface;


        /// <summary>
        /// Loads the qst version from the data interface and shows it in the gui
        /// </summary>
        public void LoadQstVersion()
        {
            _guiInterface.ShowQstVersion(_dataInterfaceLocal.LoadQstVersion());
        }

        /// <summary>
        /// Loads the server version from the data interface and shows it in the gui
        /// </summary>
        public void LoadServerVersion()
        {
            _guiInterface.ShowServerVersion(_dataInterface.LoadServerVersion());
        }

        /// <summary>
        /// Loads the computer name from the data interface and shows it in the gui
        /// </summary>
        public void LoadComputerName()
        {
            _guiInterface.ShowComputerName(_dataInterfaceLocal.LoadComputerName());
        }

        /// <summary>
        /// Asks for FileName in the gui and saves a log package
        /// </summary>
        public void CreateLogPackage(string fileName)
        {
            _dataInterfaceLocal.CreateLogPackage(fileName);
            _guiInterface.ShowLogPackageSuccessMessage();
        }


        public QstInformationUseCase(IQstInformationDataLocal dataInterfaceLocal, IQstInformationData dataInterface, IQstInformationGui guiInterface)
        {
            _dataInterfaceLocal = dataInterfaceLocal;
            _dataInterface = dataInterface;
            _guiInterface = guiInterface;
        }
    }
}
