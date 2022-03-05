using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Grpc.Core;
using TestLevelSetService;
using TestLevelSet = DtoTypes.TestLevelSet;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class TestLevelSetClient : ITestLevelSetClient
    {
        private readonly TestLevelSets.TestLevelSetsClient _testLevelSetsClient;

        public TestLevelSetClient(TestLevelSets.TestLevelSetsClient testLevelSetsClient)
        {
            _testLevelSetsClient = testLevelSetsClient;
        }

        public ListOfTestLevelSets LoadTestLevelSets()
        {
            return _testLevelSetsClient.LoadTestLevelSets(new NoParams(), new CallOptions());
        }

        public TestLevelSet InsertTestLevelSet(TestLevelSetDiff diff)
        {
            return _testLevelSetsClient.InsertTestLevelSet(diff);
        }

        public void DeleteTestLevelSet(TestLevelSetDiff diff)
        {
            _testLevelSetsClient.DeleteTestLevelSet(diff, new CallOptions());
        }

        public void UpdateTestLevelSet(TestLevelSetDiff diff)
        {
            _testLevelSetsClient.UpdateTestLevelSet(diff, new CallOptions());
        }

        public Bool IsTestLevelSetNameUnique(StringResponse name)
        {
            return _testLevelSetsClient.IsTestLevelSetNameUnique(name, new CallOptions());
        }

        public Bool DoesTestLevelSetHaveReferences(TestLevelSet set)
        {
            return _testLevelSetsClient.DoesTestLevelSetHaveReferences(set, new CallOptions());
        }
    }
}
