using System.IO;
using UnlockTool.Entities;

namespace UnlockTool.UseCases
{
    public interface IUnlockRequestDataAccess
    {
        UnlockRequest GetUnlockRequestFromStream(Stream stream);
    }

    public interface IUnlockRequestGui
    {
        void ShowUnlockRequest(UnlockRequest ur);
    }

    public interface IUnlockRequestUseCase
    {
        void LoadUnlockRequest(Stream stream);
    }

    public class UnlockRequestUseCase : IUnlockRequestUseCase
    {
        private IUnlockRequestDataAccess da_;
        private IUnlockRequestGui gui_;

        public UnlockRequestUseCase(IUnlockRequestDataAccess da, IUnlockRequestGui gui)
        {
            da_ = da;
            gui_ = gui;
        }

        public void LoadUnlockRequest(Stream stream)
        {
            gui_.ShowUnlockRequest(da_.GetUnlockRequestFromStream(stream));
        }
    }
}
