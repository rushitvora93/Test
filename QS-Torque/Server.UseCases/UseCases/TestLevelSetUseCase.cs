using System.Collections.Generic;
using Server.Core.Diffs;
using Server.Core.Entities;

namespace Server.UseCases.UseCases
{
    public interface ITestLevelSetData
    {
        void Commit();
        List<TestLevelSet> LoadTestLevelSets();
        TestLevelSet InsertTestLevelSet(TestLevelSetDiff diff);
        void DeleteTestLevelSet(TestLevelSetDiff diff);
        void UpdateTestLevelSet(TestLevelSetDiff diff);
        bool IsTestLevelSetNameUnique(string name);
        bool DoesTestLevelSetHaveReferences(TestLevelSet set);
    }

    public interface ITestLevelSetUseCase
    {
        List<TestLevelSet> LoadTestLevelSets();
        TestLevelSet InsertTestLevelSet(TestLevelSetDiff diff);
        void DeleteTestLevelSet(TestLevelSetDiff diff);
        void UpdateTestLevelSet(TestLevelSetDiff diff);
        bool IsTestLevelSetNameUnique(string name);
        bool DoesTestLevelSetHaveReferences(TestLevelSet set);
    }


    public class TestLevelSetUseCase : ITestLevelSetUseCase
    {
        private ITestLevelSetData _data;

        public TestLevelSetUseCase(ITestLevelSetData data)
        {
            _data = data;
        }


        public List<TestLevelSet> LoadTestLevelSets()
        {
            return _data.LoadTestLevelSets();
        }

        public TestLevelSet InsertTestLevelSet(TestLevelSetDiff diff)
        {
            var result = _data.InsertTestLevelSet(diff);
            _data.Commit();
            return result;
        }

        public void DeleteTestLevelSet(TestLevelSetDiff diff)
        {
            _data.DeleteTestLevelSet(diff);
            _data.Commit();
        }

        public void UpdateTestLevelSet(TestLevelSetDiff diff)
        {
            _data.UpdateTestLevelSet(diff);
            _data.Commit();
        }

        public bool IsTestLevelSetNameUnique(string name)
        {
            return _data.IsTestLevelSetNameUnique(name);
        }

        public bool DoesTestLevelSetHaveReferences(TestLevelSet set)
        {
            return _data.DoesTestLevelSetHaveReferences(set);
        }
    }
}
