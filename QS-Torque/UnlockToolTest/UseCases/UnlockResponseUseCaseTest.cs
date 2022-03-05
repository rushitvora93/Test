using System.IO;
using NUnit.Framework;
using UnlockTool.UseCases;
using UnlockToolShared.Entities;
using UnlockToolShared.UseCases;

namespace UnlockToolTest.UseCases
{
    public class UnlockResponseWriteDataAccessMock : IUnlockResponseWriteDataAccess
    {
        public bool WriteUnlockResponseCalled = false;
        public void WriteUnlockResponse(UnlockResponse ur, Stream stream)
        {
            WriteUnlockResponseCalled = true;
        }
    }

    public class UnlockResponseReadDataAccessMock : IUnlockResponseReadDataAccess
    {
        public bool ReadUnlockResponseCalled = false;
        public UnlockResponse ReadUnlockResponse(Stream stream)
        {
            ReadUnlockResponseCalled = true;
            CreatedUnlockResponse = new UnlockResponse();
            callingStream = stream;
            return CreatedUnlockResponse;
        }

        public Stream callingStream = null;
        public UnlockResponse CreatedUnlockResponse = null;
    }

    public class UnlockResponseGuiMock : IUnlockResponseGui
    {
        public bool UnlockResponseSavedCalled = false;
        public bool ShowUnlockResponseCalled = false;
        public void UnlockResponseSaved()
        {
            UnlockResponseSavedCalled = true;
        }

        public void ShowUnlockResponse(UnlockResponse ur)
        {
            ShowUnlockResponseCalled = true;
            UrFromDA = ur;
        }

        public UnlockResponse UrFromDA = null;
    }

    public class UnlockResponseUseCaseTest
    {
        [Test]
        public void SaveUnlockResponse()
        {
            UnlockResponse ur = new UnlockResponse();
            using (var stream = new MemoryStream())
            {
                var daw = new UnlockResponseWriteDataAccessMock();
                var dar = new UnlockResponseReadDataAccessMock();
                var gui = new UnlockResponseGuiMock();
                var useCase = new UnlockResponseUseCase(daw,dar, gui);
                useCase.SaveUnlockResponse(ur,stream);
                Assert.IsTrue(daw.WriteUnlockResponseCalled);
                Assert.IsTrue(gui.UnlockResponseSavedCalled);
            }
        }

        [Test]
        public void LoadUnlockResponse()
        {
            UnlockResponse ur = new UnlockResponse();
            using (var stream = new MemoryStream())
            {
                var daw = new UnlockResponseWriteDataAccessMock();
                var dar = new UnlockResponseReadDataAccessMock();
                var gui = new UnlockResponseGuiMock();
                var useCase = new UnlockResponseUseCase(daw, dar, gui);
                useCase.ReadUnlockRespose(stream);
                Assert.IsTrue(dar.ReadUnlockResponseCalled);
                Assert.IsTrue(gui.ShowUnlockResponseCalled);
                Assert.AreEqual(dar.CreatedUnlockResponse,gui.UrFromDA);
                Assert.AreEqual(stream,dar.callingStream);
            }
        }
    }
}