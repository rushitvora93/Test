using System.Linq;
using BasicTypes;
using Client.Core.Entities;
using Client.TestHelper.Mock;
using Core.Entities;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using TestDateCalculationService;
using TestHelper.Checker;
using TestHelper.Mock;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    class TestDateCalculationClientMock : ITestDateCalculationClient
    {
        public ListOfLocationToolAssignmentIds CalculateToolTestDateForParameter { get; set; }
        public bool CalculateToolTestDateForAllLocationToolAssignmentsCalled { get; set; }
        public Long CalculateToolTestDateForTestLevelSetParameter { get; set; }
        public ListOfProcessControlConditionIds CalculateProcessControlDateForParameter { get; set; }
        public bool CalculateProcessControlDateForAllProcessControlConditionsCalled { get; set; }
        public Long CalculateProcessControlDateForTestLevelSetParameter { get; set; }

        public void CalculateToolTestDateFor(ListOfLocationToolAssignmentIds param)
        {
            CalculateToolTestDateForParameter = param;
        }

        public void CalculateToolTestDateForTestLevelSet(Long id)
        {
            CalculateToolTestDateForTestLevelSetParameter = id;
        }

        public void CalculateToolTestDateForAllLocationToolAssignments()
        {
            CalculateToolTestDateForAllLocationToolAssignmentsCalled = true;
        }

        public void CalculateProcessControlDateFor(ListOfProcessControlConditionIds param)
        {
            CalculateProcessControlDateForParameter = param;
        }

        public void CalculateProcessControlDateForTestLevelSet(Long id)
        {
            CalculateProcessControlDateForTestLevelSetParameter = id;
        }

        public void CalculateProcessControlDateForAllProcessControlConditions()
        {
            CalculateProcessControlDateForAllProcessControlConditionsCalled = true;
        }
    }


    public class TestDateCalculationDataAccessTest
    {
        [TestCase(5, 9, 12)]
        [TestCase(56)]
        [TestCase(5, 65, 98, 132, 465465)]
        public void CalculateToolTestDateForPassesParameterToClient(params long[] ids)
        {
            var tuple = CreateDataAccessTuple();
            tuple.dataAccess.CalculateToolTestDateFor(ids.Select(x => new LocationToolAssignmentId(x)).ToList());
            CheckerFunctions.CollectionAssertAreEquivalent(ids, tuple.client.CalculateToolTestDateForParameter.Values, (x, y) => x == y);
        }

        [Test]
        public void CalculateToolTestDateForPassesParameterToClient()
        {
            var tuple = CreateDataAccessTuple();
            tuple.dataAccess.CalculateToolTestDateForAllLocationToolAssignments();
            Assert.IsTrue(tuple.client.CalculateToolTestDateForAllLocationToolAssignmentsCalled);
        }

        [TestCase(5)]
        [TestCase(56)]
        [TestCase(132)]
        public void CalculateToolTestDateForTestLevelSetPassesParameterToClient(long id)
        {
            var tuple = CreateDataAccessTuple();
            tuple.dataAccess.CalculateToolTestDateForTestLevelSet(new TestLevelSetId(id));
            Assert.AreEqual(id, tuple.client.CalculateToolTestDateForTestLevelSetParameter.Value);
        }

        [TestCase(5, 9, 12)]
        [TestCase(56)]
        [TestCase(5, 65, 98, 132, 465465)]
        public void CalculateProcessControlDateForPassesParameterToClient(params long[] ids)
        {
            var tuple = CreateDataAccessTuple();
            tuple.dataAccess.CalculateProcessControlDateFor(ids.Select(x => new ProcessControlConditionId(x)).ToList());
            CheckerFunctions.CollectionAssertAreEquivalent(ids, tuple.client.CalculateProcessControlDateForParameter.Values, (x, y) => x == y);
        }

        [Test]
        public void CalculateProcessControlDateForPassesParameterToClient()
        {
            var tuple = CreateDataAccessTuple();
            tuple.dataAccess.CalculateProcessControlDateForAllProcessControlConditions();
            Assert.IsTrue(tuple.client.CalculateProcessControlDateForAllProcessControlConditionsCalled);
        }

        [TestCase(5)]
        [TestCase(56)]
        [TestCase(132)]
        public void CalculateProcessControlDateForTestLevelSetPassesParameterToClient(long id)
        {
            var tuple = CreateDataAccessTuple();
            tuple.dataAccess.CalculateProcessControlDateForTestLevelSet(new TestLevelSetId(id));
            Assert.AreEqual(id, tuple.client.CalculateProcessControlDateForTestLevelSetParameter.Value);
        }


        private static (TestDateCalculationDataAccess dataAccess, TestDateCalculationClientMock client) CreateDataAccessTuple()
        {
            var client = new TestDateCalculationClientMock();
            var clientFactory = new ClientFactoryMock()
            {
                AuthenticationChannel = new ChannelWrapperMock()
                {
                    GetTestDateCalculationClientReturnValue = client
                }
            };
            var dataAccess = new TestDateCalculationDataAccess(clientFactory);

            return (dataAccess, client);
        }
    }
}
