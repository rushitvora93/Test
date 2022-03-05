using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enums;

namespace Core.UseCases.Communication
{
    public class TransferToTestEquipmentUseCaseHumbleAsyncRunner : ITransferToTestEquipmentUseCase
    {
        public TransferToTestEquipmentUseCaseHumbleAsyncRunner(ITransferToTestEquipmentUseCase real)
        {
            _real = real;
        }

        public void ShowLocationToolAssignments(TestType testType)
        {
            Task.Run(() =>_real.ShowLocationToolAssignments(testType));
        }

        public void SubmitToTestEquipment(TestEquipment testEquipment, List<LocationToolAssignmentForTransfer> locationToolAssignments, TestType testType)
        {
            Task.Run(() => _real.SubmitToTestEquipment(testEquipment, locationToolAssignments, testType));
        }

        public void SubmitToTestEquipment(TestEquipment testEquipment, List<ProcessControlForTransfer> processControlForTransfers)
        {
            Task.Run(() => _real.SubmitToTestEquipment(testEquipment, processControlForTransfers));
        }

        public void ReadFromTestEquipment(TestEquipment testEquipment)
        {
            Task.Run(() => _real.ReadFromTestEquipment(testEquipment));
        }

        public void ShowProcessControlData()
        {
            Task.Run(() => _real.ShowProcessControlData());
        }

        private readonly ITransferToTestEquipmentUseCase _real;
    }
}