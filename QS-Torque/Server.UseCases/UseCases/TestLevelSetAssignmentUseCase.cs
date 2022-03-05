using System.Collections.Generic;
using Server.Core.Entities;
using Server.Core.Enums;

namespace Server.UseCases.UseCases
{
    public interface ITestLevelSetAssignmentData
    {
        void Commit();
        void RemoveTestLevelSetAssignmentFor(List<(LocationToolAssignmentId, TestType)> ids, User user);
        void RemoveTestLevelSetAssignmentFor(List<ProcessControlConditionId> ids, User user);
        void AssignTestLevelSetToLocationToolAssignments(TestLevelSetId testLevelSetId, List<(LocationToolAssignmentId, TestType)> locationToolAssignmentIds, User user);
        void AssignTestLevelSetToProcessControlConditions(TestLevelSetId testLevelSetId, List<ProcessControlConditionId> processControlConditionIds, User user);
    }

    public interface ITestLevelSetAssignmentUseCase
    {
        void RemoveTestLevelSetAssignmentFor(List<(LocationToolAssignmentId, TestType)> ids, User user);
        void RemoveTestLevelSetAssignmentFor(List<ProcessControlConditionId> ids, User user);
        void AssignTestLevelSetToLocationToolAssignments(TestLevelSetId testLevelSetId, List<(LocationToolAssignmentId, TestType)> locationToolAssignmentIds, User user);
        void AssignTestLevelSetToProcessControlConditions(TestLevelSetId testLevelSetId, List<ProcessControlConditionId> processControlConditionIds, User user);
    }

    public class TestLevelSetAssignmentUseCase : ITestLevelSetAssignmentUseCase
    {
        private ITestLevelSetAssignmentData _data;

        public TestLevelSetAssignmentUseCase(ITestLevelSetAssignmentData data)
        {
            _data = data;
        }
        
        public void RemoveTestLevelSetAssignmentFor(List<(LocationToolAssignmentId, TestType)> ids, User user)
        {
            _data.RemoveTestLevelSetAssignmentFor(ids, user);
            _data.Commit();
        }

        public void AssignTestLevelSetToLocationToolAssignments(TestLevelSetId testLevelSetId, List<(LocationToolAssignmentId, TestType)> locationToolAssignmentIds, User user)
        {
            _data.AssignTestLevelSetToLocationToolAssignments(testLevelSetId, locationToolAssignmentIds, user);
            _data.Commit();
        }

        public void AssignTestLevelSetToProcessControlConditions(TestLevelSetId testLevelSetId, List<ProcessControlConditionId> processControlConditionIds, User user)
        {
            _data.AssignTestLevelSetToProcessControlConditions(testLevelSetId, processControlConditionIds, user);
            _data.Commit();
        }

        public void RemoveTestLevelSetAssignmentFor(List<ProcessControlConditionId> ids, User user)
        {
            _data.RemoveTestLevelSetAssignmentFor(ids, user);
            _data.Commit();
        }
    }
}
