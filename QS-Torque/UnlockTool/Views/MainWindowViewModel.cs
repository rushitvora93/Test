using System;
using System.IO;
using FrameworksAndDrivers.Gui.Wpf;
using UnlockTool.Entities;
using UnlockTool.Model;
using UnlockTool.UseCases;
using UnlockToolShared.Entities;

namespace UnlockTool.Views
{
    public class MainWindowViewModel : BindableBase, IUnlockRequestGui, IUnlockResponseGui
    {
        private string message_;
        public string Message
        {
            get { return message_; }
            set
            {
                message_ = value;
                RaisePropertyChanged();
            }
        }

        private HumbleUnlockRequestModel unlockRequest_;
        public HumbleUnlockRequestModel unlockRequest
        {
            get { return unlockRequest_;}
            set
            {
                unlockRequest_ = value; 
                RaisePropertyChanged();
            }
        }

        private HumbleUnlockResponseModel unlockResponse_ = new HumbleUnlockResponseModel(new UnlockResponse());
        public HumbleUnlockResponseModel unlockResponse
        {
            get { return unlockResponse_; }
            set
            {
                unlockResponse_ = value;
                RaisePropertyChanged();
            }
        }
        public void ShowUnlockRequest(UnlockRequest ur)
        {
            unlockRequest = new HumbleUnlockRequestModel(ur);
            Message = "File Loaded";
        }

        public void LoadFile(Stream stream)
        {
            try
            {
                UnlockRequestUseCase.LoadUnlockRequest(stream);
            }
            catch (Exception e)
            {
                Message = "Error: " + e.Message;
            }
        }

        public void SaveFile(Stream stream)
        {
            Message = "";
            try
            {
                UnlockResponseUseCase.SaveUnlockResponse(unlockResponse_.getEntity(), stream);
            }
            catch (Exception e)
            {
                Message = "Error: " + e.Message;
            }
        }

        public void LoadResponseFile(Stream stream)
        {
            Message = "";
            try
            {
                UnlockResponseUseCase.ReadUnlockRespose(stream);
            }
            catch (Exception e)
            {
                Message = "Error: " + e.Message;
            }
        }

        public IUnlockRequestUseCase UnlockRequestUseCase { get; set; }
        public IUnlockResponseUseCase UnlockResponseUseCase { get; set; }
        public void UnlockResponseSaved()
        {
            Message = "File Saved";
        }

        public void ShowUnlockResponse(UnlockResponse ur)
        {
            this.unlockResponse.UpdateFromEntity(ur);
            Message = "File Loaded";
        }
    }
}
