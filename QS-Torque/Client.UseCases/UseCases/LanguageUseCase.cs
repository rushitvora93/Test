using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.UseCases.UseCases
{
    public interface ILanguageData
    {
        string GetLastLanguage();
        void SetDefaultLanguage(string language);
    }

    public interface ILanguageUseCase
    {
        void GetLastLanguage(ILanguageErrorHandler errorHandler);
        void SetDefaultLanguage(string language, ILanguageErrorHandler errorHandler);
    }

    public interface ILanguageGui
    {
        void GetLastLanguage(string language);
        void SetDefaultLanguage(string language);
    }

    public interface ILanguageErrorHandler
    {
        void ShowLanguageError();
    }
    
    public class LanguageUseCase : ILanguageUseCase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(LanguageUseCase));
        ILanguageData _registryDataAccess;
        ILanguageGui _gui;

        public void GetLastLanguage(ILanguageErrorHandler errorHandler)
        {
            try
            {
                _gui.GetLastLanguage(_registryDataAccess.GetLastLanguage());
            }
            catch(Exception e)
            {
                _log.Error("Error in GetLastLanguage");
                errorHandler.ShowLanguageError();      
            }         
        }

        public void SetDefaultLanguage(string language, ILanguageErrorHandler errorHandler)
        {
            try
            {
                _registryDataAccess.SetDefaultLanguage(language);
                _gui.SetDefaultLanguage(language);
            }
            catch(Exception e)
            {
                _log.Error("Error in SetDefaultLanguage");
                errorHandler.ShowLanguageError();               
            }
            
        }

        public LanguageUseCase(ILanguageGui gui,ILanguageData registryInterface)
        {
            _gui = gui;
            _registryDataAccess = registryInterface;
        }
    }

    public class LanguageHumbleAsyncRunner: ILanguageUseCase
    {
        private ILanguageUseCase _real;

        public LanguageHumbleAsyncRunner(ILanguageUseCase real)
        {
            _real = real;
        }

        public void GetLastLanguage(ILanguageErrorHandler errorHandler)
        {
            Task.Run(() => _real.GetLastLanguage(errorHandler));
        }

        public void SetDefaultLanguage(string language, ILanguageErrorHandler errorHandler)
        {
            Task.Run(() => _real.SetDefaultLanguage(language, errorHandler));
        }
    }
}
