using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Grpc.Core;
using TestLevelSetAssignmentService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class TestLevelSetAssignmentClient : ITestLevelSetAssignmentClient
    {
        private readonly TestLevelSetAssignments.TestLevelSetAssignmentsClient _testLevelSetAssignmentsClient;

        public TestLevelSetAssignmentClient(TestLevelSetAssignments.TestLevelSetAssignmentsClient testLevelSetAssignmentsClient)
        {
            _testLevelSetAssignmentsClient = testLevelSetAssignmentsClient;
        }

        public void RemoveTestLevelSetAssignmentFor(ListOfLocationToolAssignmentIdsAndTestTypes param)
        {
            _testLevelSetAssignmentsClient.RemoveTestLevelSetAssignmentFor(param, new CallOptions());
        }

        public void AssignTestLevelSetToLocationToolAssignments(AssignTestLevelSetToLocationToolAssignmentsParameter param)
        {
            _testLevelSetAssignmentsClient.AssignTestLevelSetToLocationToolAssignments(param, new CallOptions());
        }

        public void AssignTestLevelSetToProcessControlConditions(AssignTestLevelSetToProcessControlConditionsParameter param)
        {
            _testLevelSetAssignmentsClient.AssignTestLevelSetToProcessControlConditions(param);
        }

        public void RemoveTestLevelSetAssignmentForProcessControl(RemoveTestLevelSetAssignmentForProcessControlParameter param)
        {
            _testLevelSetAssignmentsClient.RemoveTestLevelSetAssignmentForProcessControl(param);
        }
    }
}
