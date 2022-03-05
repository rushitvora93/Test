using System;
using System.Collections.Generic;
using Core.Enums;
using Core.UseCases.Communication;

namespace StartUp
{
    public class TransferToTestEquipmentGuiProxy: ITransferToTestEquipmentGui
    {
        public ITransferToTestEquipmentGui _real;

        public void ShowNoTestEquipmentSelectedError()
        {
            _real.ShowNoTestEquipmentSelectedError();
        }

        public void ShowLocationToolAssignmentForTransferList(List<LocationToolAssignmentForTransfer> locationToolAssignments, TestType testType)
        {
            _real.ShowLocationToolAssignmentForTransferList(locationToolAssignments, testType);
        }

        public void ShowCommunicationProgramNotFoundError()
        {
            _real.ShowCommunicationProgramNotFoundError();
        }

        public void AskToCancelLastTransfer(Action onCancelLastTransfert)
        {
            _real.AskToCancelLastTransfer(onCancelLastTransfert);
        }

        public void ShowMismatchingSerialNumber()
        {
            _real.ShowMismatchingSerialNumber();
        }

        public void ShowTransmissionError(string message)
        {
            _real.ShowTransmissionError(message);
        }

        public void ShowReadResults(List<TestEquipmentTestResult> results)
        {
            _real.ShowReadResults(results);
        }

        public void ShowProcessControlForTransferList(List<ProcessControlForTransfer> processData)
        {
            _real.ShowProcessControlForTransferList(processData);
        }

        public void ShowLoadProcessControlDataError()
        {
            _real.ShowLoadProcessControlDataError();
        }

        public void ShowNoRouteSelectedError()
        {
            _real.ShowNoRouteSelectedError();
        }
    }
}