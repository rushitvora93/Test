using BasicTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Grpc.Core;
using TestDateCalculationService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class TestDateCalculationClient : ITestDateCalculationClient
    {
        private readonly TestDateCalculations.TestDateCalculationsClient _testDateCalculationClient;

        public TestDateCalculationClient(TestDateCalculations.TestDateCalculationsClient testDateCalculationClient)
        {
            _testDateCalculationClient = testDateCalculationClient;
        }
        
        public void CalculateToolTestDateFor(ListOfLocationToolAssignmentIds param)
        {
            _testDateCalculationClient.CalculateToolTestDateFor(param, new CallOptions());
        }

        public void CalculateToolTestDateForTestLevelSet(Long id)
        {
            _testDateCalculationClient.CalculateToolTestDateForTestLevelSet(id, new CallOptions());
        }

        public void CalculateToolTestDateForAllLocationToolAssignments()
        {
            _testDateCalculationClient.CalculateToolTestDateForAllLocationToolAssignments(new NoParams(), new CallOptions());
        }

        public void CalculateProcessControlDateFor(ListOfProcessControlConditionIds param)
        {
            _testDateCalculationClient.CalculateProcessControlDateFor(param, new CallOptions());
        }

        public void CalculateProcessControlDateForTestLevelSet(Long id)
        {
            _testDateCalculationClient.CalculateProcessControlDateForTestLevelSet(id, new CallOptions());
        }

        public void CalculateProcessControlDateForAllProcessControlConditions()
        {
            _testDateCalculationClient.CalculateProcessControlDateForAllProcessControlConditions(new NoParams(), new CallOptions());
        }
    }
}
