using System.IO;
using Core.Entities;
using Core.UseCases.Communication;
using FrameworksAndDrivers.Process;

namespace FrameworksAndDrivers.DataGate
{
    public class CommunicationProgramController: ICommunicationProgramController
    {
        public CommunicationProgramController(IProcessController processController)
        {
            _processController = processController;
        }

        public void Start(TestEquipment testEquipment)
        {
            try
            {
                _processController.StartProcess(testEquipment.TestEquipmentModel?.DriverProgramPath?.ToDefaultString());
            }
            catch(FileNotFoundException e)
            {
                throw new CommunicationProgramNotFoundException(e);
            }
        }

        private readonly IProcessController _processController;
    }
}
