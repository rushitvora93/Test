using System.Threading.Tasks;
using Core.UseCases;
using NUnit.Framework;
using TestHelper.Checker;

namespace Core.Test.UseCases
{
    public class LogoutUseCaseTest
    {
        class LogoutDataMock : ILogoutData
        {
            public TaskCompletionSource<bool> LogoutCalled { get; set; } = new TaskCompletionSource<bool>();
            public void Logout()
            {
                LogoutCalled.SetResult(true);
            }
        }

        class LogoutGuiMock : ILogoutGui
        {
            public int LogoutAsyncCallCount { get; set; }

            public void FinishLogout()
            {
                LogoutAsyncCallCount++;
            }
        }

        [Test]
        public void LogoutCallsLogoutOnDataInterface()
        {
            var setup = SetupLogoutUseCase();
            setup.useCase.LogoutAsync();
            AsyncCallCheckerNoAssuredTimeout.OnCallCheck(setup.data.LogoutCalled.Task, 0, () =>
            {
                Assert.Pass();
            });
        }

        [Test]
        public void LogoutCallsLogoutOnGuiInterface()
        {
            var setup = SetupLogoutUseCase();
            setup.useCase.LogoutAsync();
            Assert.AreEqual(1, setup.gui.LogoutAsyncCallCount);
        }

        (LogoutGuiMock gui, LogoutDataMock data, ILogoutUseCase useCase) SetupLogoutUseCase()
        {
            var dataMock = new LogoutDataMock();
            var guiMock = new LogoutGuiMock();
            var useCase = new LogoutUseCase(dataMock, guiMock);
            return (guiMock, dataMock, useCase);
        }
    }
}