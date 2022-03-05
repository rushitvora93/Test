using System.Collections.Generic;
using BasicTypes;
using Client.Core.Entities;
using Client.UseCases.UseCases;
using Core.Entities;
using TestDateCalculationService;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface ITestDateCalculationClient
    {
        void CalculateToolTestDateFor(ListOfLocationToolAssignmentIds param);
        void CalculateToolTestDateForTestLevelSet(BasicTypes.Long id);
        void CalculateToolTestDateForAllLocationToolAssignments();
        void CalculateProcessControlDateFor(ListOfProcessControlConditionIds param);
        void CalculateProcessControlDateForTestLevelSet(BasicTypes.Long id);
        void CalculateProcessControlDateForAllProcessControlConditions();
    }
    
    public class TestDateCalculationDataAccess : ITestDateCalculationData
    {
        private readonly IClientFactory _clientFactory;

        public TestDateCalculationDataAccess(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private ITestDateCalculationClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetTestDateCalculationClient();
        }

        
        public void CalculateToolTestDateFor(List<LocationToolAssignmentId> ids)
        {
            var serverParam = new ListOfLocationToolAssignmentIds();
            foreach (var id in ids)
            {
                serverParam.Values.Add(id.ToLong());
            }
            GetClient().CalculateToolTestDateFor(serverParam);
        }

        public void CalculateToolTestDateForTestLevelSet(TestLevelSetId id)
        {
            GetClient().CalculateToolTestDateForTestLevelSet(new Long() { Value = id.ToLong() });
        }

        public void CalculateToolTestDateForAllLocationToolAssignments()
        {
            GetClient().CalculateToolTestDateForAllLocationToolAssignments();
        }

        public void CalculateProcessControlDateFor(List<ProcessControlConditionId> ids)
        {
            var serverParam = new ListOfProcessControlConditionIds();
            foreach (var id in ids)
            {
                serverParam.Values.Add(id.ToLong());
            }
            GetClient().CalculateProcessControlDateFor(serverParam);
        }

        public void CalculateProcessControlDateForTestLevelSet(TestLevelSetId id)
        {
            GetClient().CalculateProcessControlDateForTestLevelSet(new Long() { Value = id.ToLong() });
        }

        public void CalculateProcessControlDateForAllProcessControlConditions()
        {
            GetClient().CalculateProcessControlDateForAllProcessControlConditions();
        }
    }
}
