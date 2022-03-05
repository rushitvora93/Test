using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.ViewModel.Controls;
using NUnit.Framework;
using System;
using TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels.Controls
{
    public class SessionInformationUseCaseMock :ISessionInformationUseCase
    {
        public bool IsPinnedSet {get; set;}

        public bool LoadMegaMainMenuIsPinnedCalled { get; set; }

        public void LoadMegaMainMenuIsPinned(ISessionInformationErrorHandler errorHandler)
        {
            LoadMegaMainMenuIsPinnedCalled = true;
        }

        public bool IsCspUser()
        {
            throw new NotImplementedException();
        }

        public void LoadUserData()
        {
            throw new NotImplementedException();
        }

        public void SetMegaMainMenuIsPinned(ISessionInformationErrorHandler errorHandler, bool isPinned)
        {
            IsPinnedSet = isPinned;
        }
    }

    public class GlobalTreeViewModelTest
    {
        [Test]
        public void SetIsExpandedWhenPinnedThrowsException()
        {
            var env = CreateViewModelEnviroment();
            env.viewModel.IsPinned = true;
            env.viewModel.IsTreeExpanded = true;
            
            Assert.Catch<InvalidOperationException>(() => env.viewModel.IsTreeExpanded = false);
        }

        [Test]
        public void CollapseCommandDoesNotSetIsExpandedWhenPinned()
        {
            var env = CreateViewModelEnviroment();
            env.viewModel.IsTreeExpanded = true;
            env.viewModel.IsPinned = true;

            Assert.Throws<InvalidOperationException>(() => env.viewModel.CollapseTreeCommand.Execute(null));
            Assert.IsTrue(env.viewModel.IsTreeExpanded);
        }

        [Test]
        public void ExpandCommandDoesNotSetIsExpandedWhenPinned()
        {
            var env = CreateViewModelEnviroment();
            env.viewModel.IsPinned = true;
            Assert.Catch<InvalidOperationException>(() => env.viewModel.IsTreeExpanded = false);
            Assert.Throws<InvalidOperationException>(() => env.viewModel.CollapseTreeCommand.Execute(null));
            Assert.IsFalse(env.viewModel.IsTreeExpanded);
        }

        [Test]
        public void LoadMegaMenuIsPinnedSetsIsPinned()
        {
            var env = CreateViewModelEnviroment();
            env.viewModel.LoadMegaMainMenuIsPinned(true);
            Assert.IsTrue(env.viewModel.IsPinned);
            env.viewModel.LoadMegaMainMenuIsPinned(false);
            Assert.IsFalse(env.viewModel.IsPinned);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void LoadMegaMenuIsPinnedSetsIsTreeExpanded(bool isPinned)
        {
            var env = CreateViewModelEnviroment();
            env.viewModel.LoadMegaMainMenuIsPinned(isPinned);
            Assert.IsTrue(env.viewModel.IsTreeExpanded);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void LoadedCommandSetsIsPinned(bool isPinned)
        {
            var env = CreateViewModelEnviroment();
            env.viewModel.IsPinned = isPinned;
            env.viewModel.LoadedCommand.Invoke(null);
            Assert.IsTrue(env.useCase.LoadMegaMainMenuIsPinnedCalled);
        }

        [Test]
        public void ShowMegaMenuErrorInvokesMessageRequest()
        {
            var env = CreateViewModelEnviroment();
            bool requestInvoked = false;
            env.viewModel.MessageBoxRequest += (s, e) => requestInvoked = true;
            env.viewModel.ShowMegaMenuLockingError();
            Assert.IsTrue(requestInvoked);
        }

        private static Enviroment CreateViewModelEnviroment()
        {
            var env = new Enviroment();
            env.useCase = new SessionInformationUseCaseMock();
            env.languageInterface = new NullLocalizationWrapper();
            env.viewModel = new GlobalTreeViewModel(env.useCase,env.languageInterface,env.startUp);
            return env;
        }

        class Enviroment
        {
            public GlobalTreeViewModel viewModel;
            public SessionInformationUseCaseMock useCase;
            public NullLocalizationWrapper languageInterface;
            public StartUpMock startUp;
        }
    }
}
