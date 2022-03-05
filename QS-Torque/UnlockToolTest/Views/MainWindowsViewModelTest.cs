using System.IO;
using NUnit.Framework;
using UnlockTool.Entities;
using UnlockTool.UseCases;
using UnlockTool.Views;
using UnlockToolShared.Entities;

namespace UnlockToolTest
{
    public class UnlockRequestUseCaseMock : IUnlockRequestUseCase
    {
        public bool LoadUnlockRequestCalled = false;
        public void LoadUnlockRequest(Stream stream)
        {
            LoadUnlockRequestCalled = true;
        }
    }

    public class UnlockResponseUseCaseMock : IUnlockResponseUseCase
    {
        public bool SaveUnlockResponseCalled = false;
        public bool ReadUnlockResposeCalled = false;

        public void SaveUnlockResponse(UnlockResponse ur, Stream stream)
        {
            SaveUnlockResponseCalled = true;
        }

        public void ReadUnlockRespose(Stream stream)
        {
            ReadUnlockResposeCalled = true;
        }
    }


    public class MainWindowsViewModelTest
    {
        [Test]
        public void ShowUnlockRequestSetsRightPropertyInViewModel()
        {
            var useCase = new UnlockRequestUseCaseMock();
            var viewmodel = new MainWindowViewModel();
            viewmodel.UnlockRequestUseCase = useCase;
            var ur = new UnlockRequest();
            viewmodel.ShowUnlockRequest(ur);
            Assert.IsTrue(viewmodel.unlockRequest.getEntity() == ur);
            var ur2 = new UnlockRequest();
            viewmodel.ShowUnlockRequest(ur2);
            Assert.IsTrue(viewmodel.unlockRequest.getEntity() == ur2);
        }

        [Test]
        public void LoadFileCallsCorrectUseCaseFunction()
        {
            var useCase = new UnlockRequestUseCaseMock();
            var viewmodel = new MainWindowViewModel();
            viewmodel.UnlockRequestUseCase = useCase;
            using (var stream = new MemoryStream())
            {
                viewmodel.LoadFile(stream);
            }
            Assert.IsTrue(useCase.LoadUnlockRequestCalled);
        }

        [Test]
        public void ShowUnlockResponseSetsCorrectPropertyInViewModel()
        {
            var ReqUseCase = new UnlockRequestUseCaseMock();
            var ResUseCase = new UnlockResponseUseCaseMock();
            var viewmodel = new MainWindowViewModel();
            viewmodel.UnlockRequestUseCase = ReqUseCase;
            viewmodel.UnlockResponseUseCase = ResUseCase;
            var ur = new UnlockResponse();
            viewmodel.ShowUnlockResponse(ur);
            Assert.IsTrue(viewmodel.unlockResponse.getEntity() == ur);
            var ur2 = new UnlockResponse();
            viewmodel.ShowUnlockResponse(ur2);
            Assert.IsTrue(viewmodel.unlockResponse.getEntity() == ur2);
        }

        [Test]
        public void LoadResponseFileCallsCorrectUseCaseFunction()
        {
            var ReqUseCase = new UnlockRequestUseCaseMock();
            var ResUseCase = new UnlockResponseUseCaseMock();
            var viewmodel = new MainWindowViewModel();
            viewmodel.UnlockRequestUseCase = ReqUseCase;
            viewmodel.UnlockResponseUseCase = ResUseCase;
            using (var stream = new MemoryStream())
            {
                viewmodel.LoadResponseFile(stream);
            }
            Assert.IsTrue(ResUseCase.ReadUnlockResposeCalled);
        }

    }
}