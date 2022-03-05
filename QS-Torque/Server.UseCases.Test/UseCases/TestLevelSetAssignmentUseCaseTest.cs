using System.Collections.Generic;
using NUnit.Framework;
using Server.Core.Entities;
using Server.Core.Enums;
using Server.UseCases.UseCases;

namespace Server.UseCases.Test.UseCases
{
    class TestLevelSetAssignmentDataMock : ITestLevelSetAssignmentData
    {
        public List<(LocationToolAssignmentId, TestType)> RemoveTestLevelSetAssignmentForParameter { get; set; }
        public TestLevelSetId AssignTestLevelSetToLocationToolAssignmentsParameterTestLevelSetId { get; set; }
        public List<(LocationToolAssignmentId, TestType)> AssignTestLevelSetToLocationToolAssignmentsParameterLocToolIds { get; set; }
        public User RemoveTestLevelSetAssignmentForUserParameter { get; set; }
        public User AssignTestLevelSetToLocationToolAssignmentsUserParameter { get; set; }
        public TestLevelSetId AssignTestLevelSetToProcessControlConditionsTestLevelSetId { get; set; }
        public List<ProcessControlConditionId> AssignTestLevelSetToProcessControlConditionsProcessControlConditionIds { get; set; }
        public User AssignTestLevelSetToProcessControlConditionsUser { get; set; }
        public List<ProcessControlConditionId> RemoveTestLevelSetAssignmentForProcessControlParameter { get; set; }
        public bool CommitCalled { get; set; }
        
        public void Commit()
        {
            CommitCalled = true;
        }

        public void RemoveTestLevelSetAssignmentFor(List<(LocationToolAssignmentId, TestType)> ids, User user)
        {
            RemoveTestLevelSetAssignmentForParameter = ids;
            RemoveTestLevelSetAssignmentForUserParameter = user;
        }

        public void AssignTestLevelSetToLocationToolAssignments(TestLevelSetId testLevelSetId, List<(LocationToolAssignmentId, TestType)> locationToolAssignmentIds, User user)
        {
            AssignTestLevelSetToLocationToolAssignmentsParameterTestLevelSetId = testLevelSetId;
            AssignTestLevelSetToLocationToolAssignmentsParameterLocToolIds = locationToolAssignmentIds;
            AssignTestLevelSetToLocationToolAssignmentsUserParameter = user;
        }

        public void AssignTestLevelSetToProcessControlConditions(TestLevelSetId testLevelSetId, List<ProcessControlConditionId> processControlConditionIds, User user)
        {
            AssignTestLevelSetToProcessControlConditionsTestLevelSetId = testLevelSetId;
            AssignTestLevelSetToProcessControlConditionsProcessControlConditionIds = processControlConditionIds;
            AssignTestLevelSetToProcessControlConditionsUser = user;
        }

        public void RemoveTestLevelSetAssignmentFor(List<ProcessControlConditionId> ids, User user)
        {
            RemoveTestLevelSetAssignmentForProcessControlParameter = ids;
            RemoveTestLevelSetAssignmentForUserParameter = user;
        }
    }

    public class TestLevelSetAssignmentUseCaseTest
    {
        [Test]
        public void RemoveTestLevelSetAssignmentForPassesParameterToData()
        {
            var tuple = CreateUseCaseTuple();
            var list = new List<(LocationToolAssignmentId, TestType)>();
            var user = new User();
            tuple.useCase.RemoveTestLevelSetAssignmentFor(list, user);
            Assert.AreSame(list, tuple.data.RemoveTestLevelSetAssignmentForParameter);
            Assert.AreSame(user, tuple.data.RemoveTestLevelSetAssignmentForUserParameter);
        }

        [Test]
        public void RemoveTestLevelSetAssignmentForCallsCommit()
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.RemoveTestLevelSetAssignmentFor(new List<(LocationToolAssignmentId, TestType)>(), null);
            Assert.IsTrue(tuple.data.CommitCalled);
        }

        [Test]
        public void AssignTestLevelSetToLocationToolAssignmentsPassesParameterToData()
        {
            var tuple = CreateUseCaseTuple();
            var list = new List<(LocationToolAssignmentId, TestType)>();
            var testLevelSetId = new TestLevelSetId(32);
            var user = new User();
            tuple.useCase.AssignTestLevelSetToLocationToolAssignments(testLevelSetId, list, user);
            Assert.AreSame(testLevelSetId, tuple.data.AssignTestLevelSetToLocationToolAssignmentsParameterTestLevelSetId);
            Assert.AreSame(list, tuple.data.AssignTestLevelSetToLocationToolAssignmentsParameterLocToolIds);
            Assert.AreSame(user, tuple.data.AssignTestLevelSetToLocationToolAssignmentsUserParameter);
        }

        [Test]
        public void AssignTestLevelSetToLocationToolAssignmentsCallsCommit()
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.AssignTestLevelSetToLocationToolAssignments(null, null, null);
            Assert.IsTrue(tuple.data.CommitCalled);
        }

        [Test]
        public void AssignTestLevelSetToProcessControlConditionsTest()
        {
            var tuple = CreateUseCaseTuple();
            var testLevelSetId = new TestLevelSetId(0);
            var processControlConditionIds = new List<ProcessControlConditionId>();
            var user = new User();
            tuple.useCase.AssignTestLevelSetToProcessControlConditions(testLevelSetId, processControlConditionIds, user);

            Assert.AreSame(testLevelSetId, tuple.data.AssignTestLevelSetToProcessControlConditionsTestLevelSetId);
            Assert.AreSame(processControlConditionIds, tuple.data.AssignTestLevelSetToProcessControlConditionsProcessControlConditionIds);
            Assert.AreSame(user, tuple.data.AssignTestLevelSetToProcessControlConditionsUser);
            Assert.IsTrue(tuple.data.CommitCalled);
        }

        [Test]
        public void RemoveTestLevelSetAssignmentForProcessControlTest()
        {
            var tuple = CreateUseCaseTuple();
            var list = new List<ProcessControlConditionId>();
            var user = new User();
            tuple.useCase.RemoveTestLevelSetAssignmentFor(list, user);
            Assert.AreSame(list, tuple.data.RemoveTestLevelSetAssignmentForProcessControlParameter);
            Assert.AreSame(user, tuple.data.RemoveTestLevelSetAssignmentForUserParameter);
            Assert.IsTrue(tuple.data.CommitCalled);
        }


        private static (TestLevelSetAssignmentUseCase useCase, TestLevelSetAssignmentDataMock data) CreateUseCaseTuple()
        {
            var data = new TestLevelSetAssignmentDataMock();
            var useCase = new TestLevelSetAssignmentUseCase(data);
            return (useCase, data);
        }
    }
}
