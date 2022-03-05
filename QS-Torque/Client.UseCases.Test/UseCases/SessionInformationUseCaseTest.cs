using Core.UseCases;
using NUnit.Framework;
using System;
using TestHelper.Factories;
using TestHelper.Mock;

namespace Core.Test.UseCases
{
    class SessionInformationUseCaseTest
    {
        private class SessionInformationDataLocal : ISessionInformationDataLocal
        {
            public string UserName { get; set; }
            public string ServerName { get; set; }
            public string GroupName { get; set; }
            public string AreaName { get; set; }
            public string QstVersion { get; set; }

            public string LoadUserName()
            {
                return UserName;
            }

            public string LoadServerName()
            {
                return ServerName;
            }

            public string LoadGroupName()
            {
                return GroupName;
            }

            public string LoadAreaName()
            {
                return AreaName;
            }

            public string LoadQstVersion()
            {
                return QstVersion;
            }
        }

        private class SessionInformationRegistryDataMock : ISessionInformationRegistryDataAccess
        {
            public bool LoadMegaMenuPinningReturnValue { get; set; }

            public bool IsPinned { get; set; }

            public bool LoadMegaMenuPinningThrowsError { get; set; }

            public bool SetMegaMenuPinningThrowsError { get; set; }


            public bool LoadMegaMainMenuIsPinned()
            {
                if (LoadMegaMenuPinningThrowsError)
                {
                    throw new Exception();
                }

                return LoadMegaMenuPinningReturnValue;
            }

            public void SetMegaMainMenuIsPinned(bool isPinned)
            {
                if (SetMegaMenuPinningThrowsError)
                {
                    throw new Exception();
                }
                IsPinned = isPinned;
            }
        }

        public class SessionInformationErrorHandlerMock : ISessionInformationErrorHandler
        {
            public bool ShowMegaMenuLockingErrorCalled { get; set; }

            public void ShowMegaMenuLockingError()
            {
                ShowMegaMenuLockingErrorCalled = true;
            }
        }

        private class SessionInformationGui : ISessionInformationGui
        {
            public string UserName { get; set; }
            public string ServerName { get; set; }
            public string GroupName { get; set; }
            public string AreaName { get; set; }
            public string QstVersion { get; set; }
            public double GlobalGridWidth { get; set; }
            public bool IsPinned { get; set; }

            public void ShowUserName(string userName)
            {
                UserName = userName;
            }

            public void ShowServerName(string serverName)
            {
                ServerName = serverName;
            }

            public void ShowGroupName(string groupName)
            {
                GroupName = groupName;
            }

            public void ShowAreaName(string areaName)
            {
                AreaName = areaName;
            }

            public void SetGlobalTreeWidth(double width)
            {
                GlobalGridWidth = width;
            }

            public void ShowQstVersion(string qstVersion)
            {
                QstVersion = qstVersion;
            }

            public void LoadMegaMainMenuIsPinned(bool isPinned)
            {
                IsPinned = isPinned;
            }
        }


        [Test]
        public void LoadSessionInformationTest()
        {
            var dataLocal = new SessionInformationDataLocal();
            var gui = new SessionInformationGui();
            var useCase = new SessionInformationUseCase(dataLocal, new UserGetterMock(), gui,null);
            // Set values
            dataLocal.UserName = "TestUserName";
            dataLocal.ServerName = "TestServerName";
            dataLocal.GroupName = "TestGroupName";
            dataLocal.AreaName = "TestAreaName";
            dataLocal.QstVersion = "QstVersion";

            // Load data to gui
            useCase.LoadUserData();

            // Check if the gui data is correct
            Assert.AreEqual(dataLocal.UserName, gui.UserName);
            Assert.AreEqual(dataLocal.ServerName, gui.ServerName);
            Assert.AreEqual(dataLocal.GroupName, gui.GroupName);
            Assert.AreEqual(dataLocal.AreaName, gui.AreaName);
            Assert.AreEqual(dataLocal.QstVersion, gui.QstVersion);
        }

        [TestCase(-100, true)]
        [TestCase(1, false)]
        [TestCase(14, false)]
        public void IsCspUserReturnsCorrectValue(long userId, bool result)
        {
            var userGetter = new UserGetterMock();
            var useCase = new SessionInformationUseCase(new SessionInformationDataLocal(), userGetter, new SessionInformationGui(),null);
            userGetter.NextReturnedUser = CreateUser.IdOnly(userId);
            Assert.AreEqual(result, useCase.IsCspUser());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void LoadMegaMenuPinningForwardsValueFromDataToGui(bool isPinned)
        {
            var env = CreateUseCaseEnviroment();
            env.registryData.LoadMegaMenuPinningReturnValue = isPinned;
            env.useCase.LoadMegaMainMenuIsPinned(null);
            Assert.AreEqual(isPinned, env.gui.IsPinned);
        }

        [Test]
        public void LoadMegaMenuPinningHandlesError()
        {
            var env = CreateUseCaseEnviroment();
            env.registryData.LoadMegaMenuPinningThrowsError = true;
            var errorHandler = new SessionInformationErrorHandlerMock();
            env.useCase.LoadMegaMainMenuIsPinned(errorHandler);
            Assert.IsTrue(errorHandler.ShowMegaMenuLockingErrorCalled);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void SetMegaMenuPinningForwardsValueToGuiAndData(bool isPinned)
        {
            var env = CreateUseCaseEnviroment();
            env.gui.IsPinned = !isPinned;
            env.registryData.IsPinned = !isPinned;
            env.useCase.SetMegaMainMenuIsPinned(null, isPinned);
            Assert.AreEqual(isPinned, env.registryData.IsPinned);
            Assert.AreEqual(isPinned, env.gui.IsPinned);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void SetMegaMenuPinningHandlesError(bool isPinned)
        {
            var env = CreateUseCaseEnviroment();
            env.registryData.SetMegaMenuPinningThrowsError = true;
            var errorHandler = new SessionInformationErrorHandlerMock();
            env.useCase.SetMegaMainMenuIsPinned(errorHandler, isPinned);
            Assert.IsTrue(errorHandler.ShowMegaMenuLockingErrorCalled);
        }

        private static Enviroment CreateUseCaseEnviroment()
        {
            var env = new Enviroment();
            env.gui = new SessionInformationGui();
            env.registryData = new SessionInformationRegistryDataMock();
            env.useCase = new SessionInformationUseCase(null,null,env.gui,env.registryData);
            return env;
        }

        class Enviroment
        {
            public SessionInformationUseCase useCase;
            public SessionInformationGui gui;
            public SessionInformationRegistryDataMock registryData;
        }
    }
}
