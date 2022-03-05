using System.IO;
using UnlockToolShared.Entities;
using UnlockToolShared.UseCases;

namespace UnlockTool.UseCases
{
    public interface IUnlockResponseWriteDataAccess
    {
        void WriteUnlockResponse(UnlockResponse ur, Stream stream);
    }

    public interface IUnlockResponseGui
    {
        void UnlockResponseSaved();
        void ShowUnlockResponse(UnlockResponse ur);
    }

    public interface IUnlockResponseUseCase
    {
        void SaveUnlockResponse(UnlockResponse ur,Stream stream);
        void ReadUnlockRespose(Stream stream);
    }

    public class UnlockResponseUseCase : IUnlockResponseUseCase
    {
        private IUnlockResponseGui gui_;
        private IUnlockResponseWriteDataAccess daw_;
        private IUnlockResponseReadDataAccess dar_;

        public UnlockResponseUseCase(IUnlockResponseWriteDataAccess daw, IUnlockResponseReadDataAccess dar, IUnlockResponseGui gui)
        {
            gui_ = gui;
            daw_ = daw;
            dar_ = dar;
        }
        public void SaveUnlockResponse(UnlockResponse ur, Stream stream)
        {
            daw_.WriteUnlockResponse(ur,stream);
            gui_.UnlockResponseSaved();
        }

        public void ReadUnlockRespose(Stream stream)
        {
            gui_.ShowUnlockResponse(dar_.ReadUnlockResponse(stream));
        }
    }
}
