using System.IO;
using NUnit.Framework;
using UnlockTool.Entities;
using UnlockTool.UseCases;

namespace UnlockToolTest.UseCases
{
    public class UnlockRequestDataAccessMock : IUnlockRequestDataAccess
    {
        public bool GetUnlockRequestFromFileCalled = false;
        public UnlockRequest GetUnlockRequestFromStream(Stream stream)
        {
            GetUnlockRequestFromFileCalled = true;
            createdUr = new UnlockRequest();
            callingStream = stream;
            return createdUr;
        }

        public UnlockRequest createdUr = null;
        public Stream callingStream = null;
    }

    public class UnlockRequestGuiMock : IUnlockRequestGui
    {
        public bool ShowUnlockRequestCalled = false;
        public void ShowUnlockRequest(UnlockRequest ur)
        {
            UrFromDA = ur;
            ShowUnlockRequestCalled = true;
        }

        public UnlockRequest UrFromDA = null;
    }

    public class UnlockRequestUseCaseTest
    {
        [Test]
        public void LoadUnlockRequest()
        {
            using (var stream = new MemoryStream())
            {
                var da = new UnlockRequestDataAccessMock();
                var gui = new UnlockRequestGuiMock();
                var useCase = new UnlockRequestUseCase(da, gui);
                useCase.LoadUnlockRequest(stream);
                Assert.IsTrue(da.GetUnlockRequestFromFileCalled);
                Assert.IsTrue(gui.ShowUnlockRequestCalled);
                Assert.AreEqual(da.createdUr,gui.UrFromDA);
                Assert.AreEqual(stream,da.callingStream);
            }
        }
    }
}