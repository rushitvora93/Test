using System.Collections.Generic;
using Client.Core.Entities;
using Client.UseCases.UseCases;
using Core.Entities;
using Core.Enums;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using TestLevelSetAssignmentService;
using LocationToolAssignment = Core.Entities.LocationToolAssignment;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface ITestLevelSetAssignmentClient
    {
        void RemoveTestLevelSetAssignmentFor(ListOfLocationToolAssignmentIdsAndTestTypes param);
        void RemoveTestLevelSetAssignmentForProcessControl(RemoveTestLevelSetAssignmentForProcessControlParameter param);
        void AssignTestLevelSetToLocationToolAssignments(AssignTestLevelSetToLocationToolAssignmentsParameter param);
        void AssignTestLevelSetToProcessControlConditions(AssignTestLevelSetToProcessControlConditionsParameter param);
    }

    public class TestLevelSetAssignmentDataAccess : ITestLevelSetAssignmentData
    {
        private readonly IClientFactory _clientFactory;

        public TestLevelSetAssignmentDataAccess(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private ITestLevelSetAssignmentClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetTestLevelSetAssignmentClient();
        }
        
        
        public List<LocationToolAssignment> LoadLocationToolAssignments()
        {
            var mapper = new Mapper();
            var dtos = _clientFactory.AuthenticationChannel.GetLocationToolAssignmentClient().LoadLocationToolAssignments();
            var entities = new List<LocationToolAssignment>();
            foreach (var dto in dtos.Values)
            {
                entities.Add(mapper.DirectPropertyMapping(dto));
            }
            return entities;
        }

        public void RemoveTestLevelSetAssignmentFor(List<(LocationToolAssignmentId, TestType)> ids, Core.Entities.User user)
        {
            var serverParam = new ListOfLocationToolAssignmentIdsAndTestTypes()
            {
                UserId = user.UserId.ToLong()
            };

            foreach (var item in ids)
            {
                serverParam.Values.Add(new LocationToolAssignmentIdAndTestType()
                    {
                        LocationToolAssignmentId = item.Item1.ToLong(),
                        TestType = (int)item.Item2
                    });
            }
            GetClient().RemoveTestLevelSetAssignmentFor(serverParam);
        }

        public void AssignTestLevelSetToLocationToolAssignments(TestLevelSetId testLevelSetId, List<(LocationToolAssignmentId, TestType)> locationToolAssignmentIds, Core.Entities.User user)
        {
            var pramList = new ListOfLocationToolAssignmentIdsAndTestTypes();

            foreach (var item in locationToolAssignmentIds)
            {
                pramList.Values.Add(new LocationToolAssignmentIdAndTestType()
                {
                    LocationToolAssignmentId = item.Item1.ToLong(),
                    TestType = (int)item.Item2
                });
            }
            GetClient().AssignTestLevelSetToLocationToolAssignments(new AssignTestLevelSetToLocationToolAssignmentsParameter()
                {
                    LocationToolAssignmentIdsAndTestTypes = pramList,
                    TestLevelSetId = testLevelSetId.ToLong(),
                UserId = user.UserId.ToLong()
            });
        }

        public void AssignTestLevelSetToProcessControlConditions(Core.Entities.TestLevelSetId testLevelSetId, List<ProcessControlConditionId> processControlConditionIds, Core.Entities.User user)
        {
            var param = new AssignTestLevelSetToProcessControlConditionsParameter();
            param.TestLevelSetId = testLevelSetId.ToLong();
            processControlConditionIds.ForEach(x => param.ProcessControlConditionIds.Add(x.ToLong()));
            param.UserId = user.UserId.ToLong();
            GetClient().AssignTestLevelSetToProcessControlConditions(param);
        }

        public void RemoveTestLevelSetAssignmentFor(List<ProcessControlConditionId> ids, Core.Entities.User user)
        {
            var param = new RemoveTestLevelSetAssignmentForProcessControlParameter()
            {
                UserId = user.UserId.ToLong()
            };
            ids.ForEach(x => param.ProcessControlConditionIds.Add(x.ToLong()));
            GetClient().RemoveTestLevelSetAssignmentForProcessControl(param);
        }
    }
}
