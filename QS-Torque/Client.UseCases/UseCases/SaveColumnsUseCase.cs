using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using log4net;

namespace Core.UseCases
{
    public interface ISaveColumnsData
    {
        void SaveColumnWidths(string gridName, List<(string, double)> columns);
        List<(string, double)> LoadColumnWidths(string gridName, List<string> columns);
    }

    public interface ISaveColumnsGui
    {
        void UpdateColumnWidths(string gridName, List<(string, double)> columns);
        void ShowSaveColumnError(string gridName);
        void ShowColumnWidths(string gridName, List<(string, double)> columns);
        void ShowLoadColumnWidthsError(string gridName);
    }

    public interface ISaveColumnsUseCase
    {
        void SaveColumnWidths(string gridName, List<(string, double)> columns);
        void LoadColumnWidths(string gridName, List<string> columns);
    }


    public class SaveColumnsUseCase : ISaveColumnsUseCase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SaveColumnsUseCase));

        private readonly ISaveColumnsData _dataInterface;
        private readonly ISaveColumnsGui _guiInterface;
        public SaveColumnsUseCase(ISaveColumnsGui guiInterface, ISaveColumnsData dataInterface)
        {
            _guiInterface = guiInterface;
            _dataInterface = dataInterface;
        }

        public virtual void SaveColumnWidths(string gridName, List<(string, double)> columns)
        {
            Log.Info("UpdateColumnWidths started");
            try
            {
                _dataInterface.SaveColumnWidths(gridName, columns);
                _guiInterface.UpdateColumnWidths(gridName, columns);
            }
            catch (Exception exception)
            {
                Log.Error($"Error in UpdateColumnWidths for {gridName} with columns count {columns?.Count}", exception);
                _guiInterface.ShowSaveColumnError(gridName);
            }
            Log.Info("UpdateColumnWidths ended");
        }

        public virtual void LoadColumnWidths(string gridName, List<string> columns)
        {
            Log.Info("LoadColumnWidths started");
            try
            {
                var widths = _dataInterface.LoadColumnWidths(gridName, columns);
                _guiInterface.ShowColumnWidths(gridName, widths);
            }
            catch (Exception exception)
            {
                Log.Error($"Error in LoadColumnWidths for {gridName} with columns count {columns?.Count}", exception);
                _guiInterface.ShowLoadColumnWidthsError(gridName);
            }
            Log.Info("LoadColumnWidths ended");
        }
    }


    public class SaveColumnsHumbleAsyncRunner : ISaveColumnsUseCase
    {
        private ISaveColumnsUseCase _real;

        public SaveColumnsHumbleAsyncRunner(ISaveColumnsUseCase real)
        {
            _real = real;
        }

        public void LoadColumnWidths(string gridName, List<string> columns)
        {
            Task.Run(() => _real.LoadColumnWidths(gridName, columns));
        }

        public void SaveColumnWidths(string gridName, List<(string, double)> columns)
        {
            Task.Run(() => _real.SaveColumnWidths(gridName, columns));
        }
    }
}
