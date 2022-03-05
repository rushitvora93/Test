using System.IO;
using UnlockToolShared.Entities;

namespace UnlockToolShared.UseCases
{
    public interface IUnlockResponseReadDataAccess
    {
        UnlockResponse ReadUnlockResponse(Stream stream);
    }
}
