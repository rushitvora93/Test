using Client.UseCases.UseCases;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.UseCases.Test.UseCases
{
   
    class LanguageUseCaseTest
    {
        private class LanguageUseCaseRegistryMock : ILanguageData
        {
            public string Language { get; set; }

            public string LanguageReturnValue { get; set; }

            public bool GetLastLanguageThrowsError { get; set; }

            public bool SetLastLanguageThrowsError { get; set; }

            public string GetLastLanguage()
            {
                if (GetLastLanguageThrowsError)
                {
                    throw new Exception();
                }               
                return LanguageReturnValue;
            }

            public void SetDefaultLanguage(string language)
            {
                if (SetLastLanguageThrowsError)
                {
                    throw new Exception();
                }
                Language = language;
            }
        }

        private class LanguageUseCaseErrorHandlerMock : ILanguageErrorHandler
        {

            public bool LanguageErrorCalled { get; set; }

            public void ShowLanguageError()
            {
                LanguageErrorCalled = true;
            }
        }

        private class LanguageGui : ILanguageGui
        {

            public string Language { get; set; }

            public void GetLastLanguage(string language)
            {
                Language = language;
            }

            public void SetDefaultLanguage(string language)
            {
                Language = language;
            }
        }

       [Test]
       public void GetLastLanguageForwardsToGui()
       {
            var env = CreateUseCaseEnviroment();
            env.registryData.LanguageReturnValue = "en";
            env.useCase.GetLastLanguage(null);
            Assert.AreEqual(env.gui.Language, env.registryData.LanguageReturnValue);
       }
       
        [Test]
        public void GetLastLanguageHandlesError()
        {
            var env = CreateUseCaseEnviroment();
            env.registryData.GetLastLanguageThrowsError = true;
            var errorHandler = new LanguageUseCaseErrorHandlerMock();
            env.useCase.GetLastLanguage(errorHandler);

            Assert.IsTrue(errorHandler.LanguageErrorCalled);
        }

        [Test]
        public void SetDefaultLanguageForwardsToGuiAndDataAccess()
        {
            var env = CreateUseCaseEnviroment();
            string languageString = "en";
            env.useCase.SetDefaultLanguage(languageString, null);

            Assert.AreEqual(env.registryData.Language, languageString);
            Assert.AreEqual(env.gui.Language, languageString);
        }

        [Test]
        public void SetDefaultLanguageHandlesError()
        {
            var env = CreateUseCaseEnviroment();
            env.registryData.SetLastLanguageThrowsError = true;
            var errorHandler = new LanguageUseCaseErrorHandlerMock();
            var languageString = "en";
            env.useCase.SetDefaultLanguage(languageString, errorHandler);
            Assert.IsTrue(errorHandler.LanguageErrorCalled);
        }

        private static Enviroment CreateUseCaseEnviroment()
        {
            var env = new Enviroment();
            env.gui = new LanguageGui();
            env.registryData = new LanguageUseCaseRegistryMock();
            env.useCase = new LanguageUseCase(env.gui,env.registryData);
            return env;
        }

        class Enviroment
        {
            public LanguageUseCase useCase;
            public LanguageGui gui;
            public LanguageUseCaseRegistryMock registryData;
        }
    }
}
