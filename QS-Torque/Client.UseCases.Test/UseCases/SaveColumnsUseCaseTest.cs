using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.UseCases;
using NUnit.Framework;

namespace Core.Test.UseCases
{
    public class SaveColumnsUseCaseTest
    {
        private SaveColumnsUseCase _useCase;
        private DataMock _dataMock;
        private GuiMock _guiMock;

        private class DataMock : ISaveColumnsData
        {
            public List<(string,double)> SavedColumnWidths;
            public bool SaveColumnThrowsError { get; set; }
            public List<(string, double)> LoadColumnWidthsData { get; set; }
            public bool LoadColumnWidthsThrowsError { get; set; }

            public void SaveColumnWidths(string gridName, List<(string, double)> columns)
            {
                if (SaveColumnThrowsError)
                {
                    throw new Exception();
                }

                SavedColumnWidths = columns;
            }

            public List<(string, double)> LoadColumnWidths(string gridName, List<string> columns)
            {
                if (LoadColumnWidthsThrowsError)
                {
                    throw new Exception();
                }

                return LoadColumnWidthsData;
            }
        }

        private class GuiMock : ISaveColumnsGui
        {
            public List<(string,double)> SavedColumnWidthsGui;
            public List<(string, double)> ShownColumnWidths;
            public int SaveColumnErrorCallCount = 0;
            public int ShowColumnWidthsErrorCallCount = 0;
            
            public TaskCompletionSource<bool> ShowColumnWidthsCalled;
            public TaskCompletionSource<bool> ShowColumnWidthsErrorCalled;

            public GuiMock() : base()
            {
                ShowColumnWidthsCalled = new TaskCompletionSource<bool>();
                ShowColumnWidthsErrorCalled = new TaskCompletionSource<bool>();
            }

            public void UpdateColumnWidths(string gridName, List<(string, double)> columns)
            {
                SavedColumnWidthsGui = columns;
            }

            public void ShowSaveColumnError(string gridName)
            {
                SaveColumnErrorCallCount++;
            }

            public void ShowColumnWidths(string gridName, List<(string, double)> columns)
            {
                ShownColumnWidths = columns;
                ShowColumnWidthsCalled.SetResult(true);
            }

            public void ShowLoadColumnWidthsError(string gridName)
            {
                ShowColumnWidthsErrorCallCount++;
                ShowColumnWidthsErrorCalled.SetResult(true);
            }
        }

        [SetUp]
        public void Setup()
        {
            _dataMock = new DataMock();
            _guiMock = new GuiMock();
            _useCase = new SaveColumnsUseCase(_guiMock, _dataMock);
        }

        static readonly List<List<(string, double)>> saveColumnWidthsTestData = new List<List<(string,double)>>
        {
            new List<(string, double)>{ ("test1",10.0),("test2", 11.0),("test", 2.0)},
            new List<(string, double)>{("neuTest1", 5.6),("neuTest2",6.9),("neuTest3", 11.12)}
        };

        [Test, TestCaseSource(nameof(saveColumnWidthsTestData))]
        public void SaveColumnWidthsTest(List<(string, double)> widths)
        {
            _useCase.SaveColumnWidths("Test", widths);
            
            Assert.AreEqual(_dataMock.SavedColumnWidths, widths);
            Assert.AreEqual(_guiMock.SavedColumnWidthsGui, widths);
        }

        [Test, TestCaseSource(nameof(saveColumnWidthsTestData))]
        public void SaveColumnWidthsErrorTest(List<(string, double)> widths)
        {
            _dataMock.SaveColumnThrowsError = true;
            _useCase.SaveColumnWidths("Test", widths);
            Assert.AreEqual(_guiMock.SaveColumnErrorCallCount, 1);
        }

        [Test, TestCaseSource(nameof(saveColumnWidthsTestData))]
        public void LoadColumnWidthsTest(List<(string, double)> widths)
        {
            _dataMock.LoadColumnWidthsData = widths;
            _useCase.LoadColumnWidths("Test", widths.Select(x => x.Item1).ToList());
            Assert.AreEqual(_guiMock.ShownColumnWidths, widths);
        }

        [Test, TestCaseSource(nameof(saveColumnWidthsTestData))]
        public void LoadColumnWidthsErrorTest(List<(string, double)> widths)
        {
            _dataMock.LoadColumnWidthsThrowsError = true;
            _useCase.LoadColumnWidths("Test",widths.Select(x => x.Item1).ToList());
            Assert.AreEqual(_guiMock.ShowColumnWidthsErrorCallCount,1);
        }
    }
}