using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{

    public class GlobalToolBarViewModelTest
    {
        [Test]
        public void LoadedCommandCallsLanguageUseCase()
        {
            var env = CreateUseCaseEnviroment();
            env.viewModel.LoadedCommand.Invoke(null);
            Assert.AreEqual(env.useCase.Language, "en");
            Assert.AreSame(env.viewModel, env.useCase.GetLastLanguageErrorHandler);
        }

        [TestCase("en")]
        [TestCase("DE-de")]
        public void UseCaseLanguageGetsSetThroughViewModel(string languageString)
        {
            var env = CreateUseCaseEnviroment();           
            env.viewModel.Language = languageString;
            Assert.AreEqual(env.useCase.Language, languageString);
            Assert.AreSame(env.viewModel, env.useCase.SetDefaultLanguageErrorHandler);
        }

        [Test]
        public void LanguageErrorInvokesMessageRequest()
        {
            var env = CreateUseCaseEnviroment();
            bool requestInvoked = false;
            env.viewModel.MessageBoxRequest += (s, e) => requestInvoked = true;
            env.viewModel.ShowLanguageError();
            Assert.IsTrue(requestInvoked);
        }

        private static Enviroment CreateUseCaseEnviroment()
        {
            var env = new Enviroment();
            env.useCase = new LanguageUseCaseMock();
            env.languageInterface = new LanguageInterfaceMock();
            env.viewModel = new GlobalToolBarViewModel(new NullLocalizationWrapper(),env.useCase,env.languageInterface);
            return env;
        }

        class Enviroment
        {
            public GlobalToolBarViewModel viewModel;
            public LanguageUseCaseMock useCase;
            public LanguageInterfaceMock languageInterface;

        }
    }
}
