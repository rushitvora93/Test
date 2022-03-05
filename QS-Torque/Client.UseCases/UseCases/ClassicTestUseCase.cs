using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Core.Entities;
using Core.Entities;
using log4net;

namespace Core.UseCases
{
    public interface IClassicTestUseCase
    {
        void LoadToolsFromLocationTests(Location location, IClassicTestDataErrorShower errorShower);
        void LoadMfuHeaderFromTool(Tool tool, IClassicTestDataErrorShower errorShower, Location location = null);
        void LoadChkHeaderFromTool(Tool tool, IClassicTestDataErrorShower errorShower, Location location = null);
        void LoadValuesForClassicChkHeader(Location location, Tool tool, List<ClassicChkTest> chktests, IClassicTestDataErrorShower errorShower, IShowEvaluation showEval);
        void LoadValuesForClassicMfuHeader(Location location, Tool tool, List<ClassicMfuTest> mfutest, IClassicTestDataErrorShower errorShower, IShowEvaluation showEval);
        void LoadProcessHeaderFromLocation(Location location, IClassicTestDataErrorShower errorShower);
        void LoadValuesForClassicProcessHeader(Location location, List<ClassicProcessTest> tests, bool isPfu, IClassicTestDataErrorShower errorShower, IShowEvaluation showEval);
    }

    public interface IClassicTestGui
    {
        void ShowToolsForSelectedLocation(Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)> tools);
        void ShowProcessHeaderForSelectedLocation(List<ClassicProcessTest> header);
        void ShowMfuHeaderForSelectedTool(List<ClassicMfuTest> header);
        void ShowChkHeaderForSelectedTool(List<ClassicChkTest> header);
    }

    public interface IShowEvaluation
    {
        void ShowValuesForClassicChkHeader(Location location, Tool tool, List<ClassicChkTest> chktest);
        void ShowValuesForClassicMfuHeader(Location location, Tool tool, List<ClassicMfuTest> mfutest);
        void ShowValuesForClassicProcessHeader(Location location, List<ClassicProcessTest> tests, bool isPfu);
    }

    public interface IClassicTestDataErrorShower
    {
        void ShowErrorMessage();
    }

    public interface IClassicTestData
    {
        Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)> LoadToolsFromLocationTests(LocationId locationId);

        List<ClassicMfuTest> GetClassicMfuHeaderFromTool(ToolId toolId, LocationId locationId);

        List<ClassicChkTest> GetClassicChkHeaderFromTool(ToolId toolId, LocationId locationId);

        List<ClassicChkTestValue> GetValuesFromClassicChkHeader(List<ClassicChkTest> header);

        List<ClassicMfuTestValue> GetValuesFromClassicMfuHeader(List<ClassicMfuTest> header);
        List<ClassicProcessTest> GetClassicProcessHeaderFromLocation(LocationId locationId);
        List<ClassicProcessTestValue> GetValuesFromClassicProcessHeader(List<ClassicProcessTest> tests);
    }

    public class ClassicTestUseCase : IClassicTestUseCase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ClassicTestUseCase));
        private IClassicTestGui _guiInterface;
        private IClassicTestData _dataInterface;
        private ILocationUseCase _locationUseCase;
        private readonly IToleranceClassData _toleranceClassData;

        public ClassicTestUseCase(IClassicTestGui guiInterface, IClassicTestData dataInterface, ILocationUseCase locationUseCase, IToleranceClassData toleranceClassData)
        {
            _guiInterface = guiInterface;
            _dataInterface = dataInterface;
            _locationUseCase = locationUseCase;
            _toleranceClassData = toleranceClassData;
        }

        public void LoadChkHeaderFromTool(Tool tool, IClassicTestDataErrorShower errorShower, Location location = null)
        {
            Log.Info("LoadChkHeaderFromTool started");
            try
            {
                Log.Debug($"LoadChkHeaderFromTool call with tool {tool?.SerialNumber}");
                var header = new List<ClassicChkTest>();
                if (tool != null)
                {
                    header = _dataInterface.GetClassicChkHeaderFromTool(tool.Id, location?.Id);
                }
                
                _guiInterface.ShowChkHeaderForSelectedTool(header);
            }
            catch (Exception exception)
            {
                Log.Error("Error while loading MfuHeader for tool failed with Exception", exception);
                errorShower.ShowErrorMessage();
            }
            Log.Info("LoadChkHeaderFromTool ended");
        }

        public void LoadMfuHeaderFromTool(Tool tool, IClassicTestDataErrorShower errorShower,  Location location = null)
        {
            Log.Info("LoadMfuHeaderFromTool started");
            try
            {
                Log.Debug($"LoadMfuHeaderFromTool call with tool {tool?.SerialNumber}");
                var header = new List<ClassicMfuTest>();
                if (tool != null)
                {
                    header = _dataInterface.GetClassicMfuHeaderFromTool(tool.Id, location?.Id);
                }
                _guiInterface.ShowMfuHeaderForSelectedTool(header);
            }
            catch (Exception exception)
            {
                Log.Error("Error while loading MfuHeader for tool failed with Exception", exception);
                errorShower.ShowErrorMessage();
            }
            Log.Info("LoadMfuHeaderFromTool ended");
        }

        public virtual void LoadToolsFromLocationTests(Location location, IClassicTestDataErrorShower errorShower)
        {
            Log.Info("ShowToolsForSelectedLocation started");
            try
            {
                Log.Debug($"getToolsFromLocationTests call with location {location?.Number}");
                if (location != null)
                {
                    var tools = _dataInterface.LoadToolsFromLocationTests(location.Id);
                    _guiInterface.ShowToolsForSelectedLocation(tools);
                }
                else
                {
                    _guiInterface.ShowToolsForSelectedLocation(null);
                }
            }
            catch (Exception exception)
            {
                Log.Error("Error while loading Tools for Location failed with Exception", exception);
                errorShower.ShowErrorMessage();
            }
            Log.Info("ShowToolsForSelectedLocation ended");
        }

        public void LoadValuesForClassicChkHeader(Location location, Tool tool, List<ClassicChkTest> chktests, IClassicTestDataErrorShower errorShower, IShowEvaluation showEval)
        {
            Log.Info("LoadValuesForClassicChkHeader started");
            try
            {
                var values = _dataInterface.GetValuesFromClassicChkHeader(chktests);

                foreach (var chkTest in chktests)
                {
                    chkTest.TestValues = values.FindAll(x => x.Id.Equals(chkTest.Id)).OrderBy(x => x.Position).ToList();
                }

                foreach (var val in values)
                {
                    val.ChkTest = chktests.SingleOrDefault(x => x.Id.Equals(val.Id));
                }

                if (location.LocationDirectoryPath == null || location.LocationDirectoryPath.Count == 0)
                {
                    _locationUseCase.LoadTreePathForLocations(new List<Location>() { location });
                }

                LoadToleranceClassForChkTests(chktests);

                Log.Debug($"LoadValuesForClassicChkHeader call");
                showEval.ShowValuesForClassicChkHeader(location, tool, chktests);
            }
            catch (Exception exception)
            {
                Log.Error("Error while loading ClassicChkTestValues", exception);
                errorShower.ShowErrorMessage();
            }
            Log.Info("LoadValuesForClassicChkHeader ended");
        }

        private void LoadToleranceClassForChkTests(List<ClassicChkTest> chkTests)
        {
            var idsWithClass1Data = new List<Tuple<long, long, DateTime>>();
            var idsWithClass2Data = new List<Tuple<long, long, DateTime>>();

            foreach (var chkTest in chkTests)
            {
                if (chkTest.ToleranceClassUnit1 != null)
                {
                    idsWithClass1Data.Add(new Tuple<long, long, DateTime>(chkTest.Id.ToLong(), chkTest.ToleranceClassUnit1.Id.ToLong(), chkTest.Timestamp));
                }

                if (chkTest.ToleranceClassUnit2 != null)
                {
                    idsWithClass2Data.Add(new Tuple<long, long, DateTime>(chkTest.Id.ToLong(), chkTest.ToleranceClassUnit2.Id.ToLong(), chkTest.Timestamp));
                }
            }

            var tolerance1HistoryForTestIds = _toleranceClassData.GetToleranceClassFromHistoryForIds(idsWithClass1Data);
            foreach (var chkTest in chkTests)
            {
                var toleranceClass = tolerance1HistoryForTestIds.SingleOrDefault(x => x.Key == chkTest.Id.ToLong()).Value;
                if (toleranceClass != null)
                {
                    chkTest.ToleranceClassUnit1 = toleranceClass;
                }
            }

            var tolerance2HistoryForTestIds = _toleranceClassData.GetToleranceClassFromHistoryForIds(idsWithClass2Data);
            foreach (var chkTest in chkTests)
            {
                var toleranceClass = tolerance2HistoryForTestIds.SingleOrDefault(x => x.Key == chkTest.Id.ToLong()).Value;
                if (toleranceClass != null)
                {
                    chkTest.ToleranceClassUnit2 = toleranceClass;
                }
            }
        }

        public void LoadValuesForClassicMfuHeader(Location location, Tool tool, List<ClassicMfuTest> mfutests, IClassicTestDataErrorShower errorShower, IShowEvaluation showEval)
        {
            Log.Info("LoadValuesForClassicMfuHeader started");
            try
            {
                var values = _dataInterface.GetValuesFromClassicMfuHeader(mfutests);

                foreach (var mfuTest in mfutests)
                {
                    mfuTest.TestValues = values.FindAll(x => x.Id.Equals(mfuTest.Id)).OrderBy(x => x.Position).ToList();
                }

                foreach (var val in values)
                {
                    val.MfuTest = mfutests.SingleOrDefault(x => x.Id.Equals(val.Id));
                }

                LoadToleranceClassForMfuTests(mfutests);
                Log.Debug($"LoadValuesForClassicMfuHeader call");
                showEval.ShowValuesForClassicMfuHeader(location, tool, mfutests);
            }
            catch (Exception exception)
            {
                Log.Error("Error while loading ClassicMfuTestValues", exception);
                errorShower.ShowErrorMessage();
            }
            Log.Info("LoadValuesForClassicMfuHeader ended");
        }

        public void LoadProcessHeaderFromLocation(Location location, IClassicTestDataErrorShower errorShower)
        {
            Log.Info("LoadProcessHeaderFromLocation started");
            try
            {
                Log.Debug($"LoadProcessHeaderFromLocation call with location {location?.Number.ToDefaultString()}");
                var header = new List<ClassicProcessTest>();
                if (location != null)
                {
                    header = _dataInterface.GetClassicProcessHeaderFromLocation(location.Id);
                }
                _guiInterface.ShowProcessHeaderForSelectedLocation(header);
            }
            catch (Exception exception)
            {
                Log.Error("Error while loading ProcessHeader for location failed with Exception", exception);
                errorShower.ShowErrorMessage();
            }
            Log.Info("LoadProcessHeaderFromLocation ended");
        }

        public void LoadValuesForClassicProcessHeader(Location location, List<ClassicProcessTest> tests, bool isPfu, IClassicTestDataErrorShower errorShower,
            IShowEvaluation showEval)
        {
            Log.Info("LoadValuesForClassicProcessHeader started");
            try
            {
                var values = _dataInterface.GetValuesFromClassicProcessHeader(tests);

                foreach (var test in tests)
                {
                    test.SetTestValues(values);
                }

                if (location.LocationDirectoryPath == null || location.LocationDirectoryPath.Count == 0)
                {
                    _locationUseCase.LoadTreePathForLocations(new List<Location>() { location });
                }

                LoadToleranceClassForProcessTests(tests);

                Log.Debug($"LoadValuesForClassicProcessHeader call");
                showEval.ShowValuesForClassicProcessHeader(location, tests, isPfu);
            }
            catch (Exception exception)
            {
                Log.Error("Error while loading LoadValuesForClassicProcessHeader", exception);
                errorShower.ShowErrorMessage();
            }
            Log.Info("LoadValuesForClassicProcessHeader ended");
        }

        private void LoadToleranceClassForProcessTests(List<ClassicProcessTest> processTests)
        {
            var idsWithClass1Data = new List<Tuple<long, long, DateTime>>();
            var idsWithClass2Data = new List<Tuple<long, long, DateTime>>();

            foreach (var processTest in processTests)
            {
                if (processTest.ToleranceClassUnit1 != null)
                {
                    idsWithClass1Data.Add(new Tuple<long, long, DateTime>(processTest.Id.ToLong(), processTest.ToleranceClassUnit1.Id.ToLong(), processTest.Timestamp));
                }

                if (processTest.ToleranceClassUnit2 != null)
                {
                    idsWithClass2Data.Add(new Tuple<long, long, DateTime>(processTest.Id.ToLong(), processTest.ToleranceClassUnit2.Id.ToLong(), processTest.Timestamp));
                }
            }

            var tolerance1HistoryForTestIds = _toleranceClassData.GetToleranceClassFromHistoryForIds(idsWithClass1Data);
            foreach (var processTest in processTests)
            {
                processTest.ToleranceClassUnit1 = GetToleranceClass(tolerance1HistoryForTestIds, processTest.Id, processTest.ToleranceClassUnit1);    
            }

            var tolerance2HistoryForTestIds = _toleranceClassData.GetToleranceClassFromHistoryForIds(idsWithClass2Data);
            foreach (var processTest in processTests)
            {
                processTest.ToleranceClassUnit2 = GetToleranceClass(tolerance2HistoryForTestIds, processTest.Id, processTest.ToleranceClassUnit2);
            }
        }

        private ToleranceClass GetToleranceClass(Dictionary<long, ToleranceClass> tolerance2HistoryForTestIds, GlobalHistoryId id, ToleranceClass currentClass)
        {
            var toleranceClass = tolerance2HistoryForTestIds.SingleOrDefault(x => x.Key == id.ToLong()).Value;
            if (toleranceClass != null)
            {
                return toleranceClass;
            }

            return currentClass;
        }

        private void LoadToleranceClassForMfuTests(List<ClassicMfuTest> mfuTests)
        {
            var idsWithClass1Data = new List<Tuple<long, long, DateTime>>();
            var idsWithClass2Data = new List<Tuple<long, long, DateTime>>();

            foreach (var mfuTest in mfuTests)
            {
                if (mfuTest.ToleranceClassUnit1 != null)
                {
                    idsWithClass1Data.Add(new Tuple<long, long, DateTime>(mfuTest.Id.ToLong(), mfuTest.ToleranceClassUnit1.Id.ToLong(), mfuTest.Timestamp));
                }

                if (mfuTest.ToleranceClassUnit2 != null)
                {
                    idsWithClass2Data.Add(new Tuple<long, long, DateTime>(mfuTest.Id.ToLong(), mfuTest.ToleranceClassUnit2.Id.ToLong(), mfuTest.Timestamp));
                }
            }

            var tolerance1HistoryForTestIds = _toleranceClassData.GetToleranceClassFromHistoryForIds(idsWithClass1Data);
            foreach (var mfuTest in mfuTests)
            {
                var toleranceClass = tolerance1HistoryForTestIds.SingleOrDefault(x => x.Key == mfuTest.Id.ToLong()).Value;
                if (toleranceClass != null)
                {
                    mfuTest.ToleranceClassUnit1 = toleranceClass;
                }
            }

            var tolerance2HistoryForTestIds = _toleranceClassData.GetToleranceClassFromHistoryForIds(idsWithClass2Data);
            foreach (var mfuTest in mfuTests)
            {
                var toleranceClass = tolerance2HistoryForTestIds.SingleOrDefault(x => x.Key == mfuTest.Id.ToLong()).Value;
                if (toleranceClass != null)
                {
                    mfuTest.ToleranceClassUnit2 = toleranceClass;
                }
            }
        }
    }

    public class SelectTestDataUseCaseHumbleAsyncRunner : IClassicTestUseCase
    {
        IClassicTestUseCase _real;
        public SelectTestDataUseCaseHumbleAsyncRunner(IClassicTestUseCase real)
        {
            _real = real;
        }

        public void LoadChkHeaderFromTool(Tool tool, IClassicTestDataErrorShower errorShower, Location location = null)
        {
            Task.Run(() => _real.LoadChkHeaderFromTool(tool, errorShower, location));
        }

        public void LoadMfuHeaderFromTool(Tool tool, IClassicTestDataErrorShower errorShower,  Location location = null)
        {
            Task.Run(() => _real.LoadMfuHeaderFromTool(tool, errorShower, location));
        }

        public void LoadToolsFromLocationTests(Location location, IClassicTestDataErrorShower errorShower)
        {
            Task.Run(() => _real.LoadToolsFromLocationTests(location, errorShower));
        }

        public void LoadValuesForClassicChkHeader(Location location, Tool tool, List<ClassicChkTest> chktests, IClassicTestDataErrorShower errorShower, IShowEvaluation showEval)
        {
            Task.Run(() => _real.LoadValuesForClassicChkHeader(location,tool,chktests, errorShower, showEval));
        }

        public void LoadValuesForClassicMfuHeader(Location location, Tool tool, List<ClassicMfuTest> mfutest, IClassicTestDataErrorShower errorShower, IShowEvaluation showEval)
        {
            Task.Run(() => _real.LoadValuesForClassicMfuHeader(location, tool, mfutest, errorShower, showEval));
        }

        public void LoadProcessHeaderFromLocation(Location location, IClassicTestDataErrorShower errorShower)
        {
            Task.Run(() => _real.LoadProcessHeaderFromLocation(location, errorShower));
        }

        public void LoadValuesForClassicProcessHeader(Location location, List<ClassicProcessTest> tests, bool isPfu, IClassicTestDataErrorShower errorShower,
            IShowEvaluation showEval)
        {
            Task.Run(() => _real.LoadValuesForClassicProcessHeader(location, tests, isPfu, errorShower, showEval));
        }
    }
}