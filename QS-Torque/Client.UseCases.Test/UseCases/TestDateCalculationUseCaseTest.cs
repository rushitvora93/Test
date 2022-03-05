using System;
using System.Collections.Generic;
using Client.Core.Entities;
using Client.UseCases.UseCases;
using Core.Entities;
using NUnit.Framework;
using TestHelper.Mock;

namespace Client.UseCases.Test.UseCases
{
    class TestDataCalculationDataMock : ITestDateCalculationData
    {
        public List<LocationToolAssignmentId> CalculateToolTestDateForParameter { get; set; }
        public bool CalculateToolTestDateForThrowsError { get; set; }
        public bool CalculateToolTestDateForAllLocationToolAssignmentsThrowsError { get; set; }
        public bool CalculateToolTestDateForAllLocationToolAssignmentsCalled { get; set; }
        public TestLevelSetId CalculateToolTestDateForTestLevelSetParameter { get; set; }
        public bool CalculateToolTestDateForTestLevelSetThrowsError { get; set; }
        public List<ProcessControlConditionId> CalculateProcessControlDateForParameter { get; set; }
        public bool CalculateProcessControlDateForThrowsError { get; set; }
        public bool CalculateProcessControlDateForAllProcessControlConditionsThrowsError { get; set; }
        public bool CalculateProcessControlDateForAllProcessControlConditionsCalled { get; set; }
        public TestLevelSetId CalculateProcessControlDateForTestLevelSetParameter { get; set; }
        public bool CalculateProcessControlDateForTestLevelSetThrowsError { get; set; }

        public void CalculateToolTestDateFor(List<LocationToolAssignmentId> ids)
        {
            if(CalculateToolTestDateForThrowsError)
            {
                throw new Exception();
            }
            
            CalculateToolTestDateForParameter = ids;
        }

        public void CalculateToolTestDateForTestLevelSet(TestLevelSetId id)
        {
            if (CalculateToolTestDateForTestLevelSetThrowsError)
            {
                throw new Exception();
            }

            CalculateToolTestDateForTestLevelSetParameter = id;
        }

        public void CalculateToolTestDateForAllLocationToolAssignments()
        {
            if (CalculateToolTestDateForAllLocationToolAssignmentsThrowsError)
            {
                throw new Exception();
            }
            
            CalculateToolTestDateForAllLocationToolAssignmentsCalled = true;
        }

        public void CalculateProcessControlDateFor(List<ProcessControlConditionId> ids)
        {
            if (CalculateProcessControlDateForThrowsError)
            {
                throw new Exception();
            }

            CalculateProcessControlDateForParameter = ids;
        }

        public void CalculateProcessControlDateForTestLevelSet(TestLevelSetId id)
        {
            if (CalculateProcessControlDateForTestLevelSetThrowsError)
            {
                throw new Exception();
            }

            CalculateProcessControlDateForTestLevelSetParameter = id;
        }

        public void CalculateProcessControlDateForAllProcessControlConditions()
        {
            if (CalculateProcessControlDateForAllProcessControlConditionsThrowsError)
            {
                throw new Exception();
            }

            CalculateProcessControlDateForAllProcessControlConditionsCalled = true;
        }
    }
    

    public class TestDateCalculationUseCaseTest
    {
        [Test]
        public void CalculateToolTestDateForPassesParameterToData()
        {
            var tuple = CreateUseCaseTuple();
            var ids = new List<LocationToolAssignmentId>();
            tuple.useCase.CalculateToolTestDateFor(ids);
            Assert.AreSame(ids, tuple.data.CalculateToolTestDateForParameter);
        }

        [Test]
        public void CalculateToolTestDateForCallsNotification()
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.CalculateToolTestDateFor(null);
            Assert.IsTrue(tuple.notification.SendSuccessfulTestDateCalculationNotificationCalled);
        }

        [Test]
        public void CalculateToolTestDateForHandlesError()
        {
            var tuple = CreateUseCaseTuple();
            tuple.data.CalculateToolTestDateForThrowsError = true;
            tuple.useCase.CalculateToolTestDateFor(null);
            Assert.IsTrue(tuple.notification.SendFailedTestDateCalculationNotificationCalled);
        }

        [Test]
        public void CalculateToolTestDateForAllLocationToolAssignmentsPassesParameterToData()
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.CalculateToolTestDateForAllLocationToolAssignments();
            Assert.IsTrue(tuple.data.CalculateToolTestDateForAllLocationToolAssignmentsCalled);
        }

        [Test]
        public void CalculateToolTestDateForAllLocationToolAssignmentsCallsNotification()
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.CalculateToolTestDateForAllLocationToolAssignments();
            Assert.IsTrue(tuple.notification.SendSuccessfulTestDateCalculationNotificationCalled);
        }

        [Test]
        public void CalculateToolTestDateForAllLocationToolAssignmentsHandlesError()
        {
            var tuple = CreateUseCaseTuple();
            tuple.data.CalculateToolTestDateForAllLocationToolAssignmentsThrowsError = true;
            tuple.useCase.CalculateToolTestDateForAllLocationToolAssignments();
            Assert.IsTrue(tuple.notification.SendFailedTestDateCalculationNotificationCalled);
        }

        [Test]
        public void CalculateToolTestDateForTestLevelSetPassesParameterToData()
        {
            var tuple = CreateUseCaseTuple();
            var id = new TestLevelSetId(0);
            tuple.useCase.CalculateToolTestDateForTestLevelSet(id);
            Assert.AreSame(id, tuple.data.CalculateToolTestDateForTestLevelSetParameter);
        }

        [Test]
        public void CalculateToolTestDateForTestLevelSetCallsNotification()
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.CalculateToolTestDateForTestLevelSet(new TestLevelSetId(0));
            Assert.IsTrue(tuple.notification.SendSuccessfulTestDateCalculationNotificationCalled);
        }

        [Test]
        public void CalculateToolTestDateForTestLevelSetHandlesError()
        {
            var tuple = CreateUseCaseTuple();
            tuple.data.CalculateToolTestDateForTestLevelSetThrowsError = true;
            tuple.useCase.CalculateToolTestDateForTestLevelSet(null);
            Assert.IsTrue(tuple.notification.SendFailedTestDateCalculationNotificationCalled);
        }

        [Test]
        public void CalculateProcessControlDateForPassesParameterToData()
        {
            var tuple = CreateUseCaseTuple();
            var ids = new List<ProcessControlConditionId>();
            tuple.useCase.CalculateProcessControlDateFor(ids);
            Assert.AreSame(ids, tuple.data.CalculateProcessControlDateForParameter);
        }

        [Test]
        public void CalculateProcessControlDateForCallsNotification()
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.CalculateProcessControlDateFor(null);
            Assert.IsTrue(tuple.notification.SendSuccessfulProcessControlDateCalculationNotificationCalled);
        }

        [Test]
        public void CalculateProcessControlDateForHandlesError()
        {
            var tuple = CreateUseCaseTuple();
            tuple.data.CalculateProcessControlDateForThrowsError = true;
            tuple.useCase.CalculateProcessControlDateFor(null);
            Assert.IsTrue(tuple.notification.SendFailedTestDateCalculationNotificationCalled);
        }

        [Test]
        public void CalculateProcessControlDateForAllLocationToolAssignmentsPassesParameterToData()
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.CalculateProcessControlDateForAllProcessControlConditions();
            Assert.IsTrue(tuple.data.CalculateProcessControlDateForAllProcessControlConditionsCalled);
        }

        [Test]
        public void CalculateProcessControlDateForAllLocationToolAssignmentsCallsNotification()
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.CalculateProcessControlDateForAllProcessControlConditions();
            Assert.IsTrue(tuple.notification.SendSuccessfulProcessControlDateCalculationNotificationCalled);
        }

        [Test]
        public void CalculateProcessControlDateForAllLocationToolAssignmentsHandlesError()
        {
            var tuple = CreateUseCaseTuple();
            tuple.data.CalculateProcessControlDateForAllProcessControlConditionsThrowsError = true;
            tuple.useCase.CalculateProcessControlDateForAllProcessControlConditions();
            Assert.IsTrue(tuple.notification.SendFailedTestDateCalculationNotificationCalled);
        }

        [Test]
        public void CalculateProcessControlDateForTestLevelSetPassesParameterToData()
        {
            var tuple = CreateUseCaseTuple();
            var id = new TestLevelSetId(0);
            tuple.useCase.CalculateProcessControlDateForTestLevelSet(id);
            Assert.AreSame(id, tuple.data.CalculateProcessControlDateForTestLevelSetParameter);
        }

        [Test]
        public void CalculateProcessControlDateForTestLevelSetCallsNotification()
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.CalculateProcessControlDateForTestLevelSet(new TestLevelSetId(0));
            Assert.IsTrue(tuple.notification.SendSuccessfulProcessControlDateCalculationNotificationCalled);
        }

        [Test]
        public void CalculateProcessControlDateForTestLevelSetHandlesError()
        {
            var tuple = CreateUseCaseTuple();
            tuple.data.CalculateProcessControlDateForTestLevelSetThrowsError = true;
            tuple.useCase.CalculateProcessControlDateForTestLevelSet(null);
            Assert.IsTrue(tuple.notification.SendFailedTestDateCalculationNotificationCalled);
        }


        private static (TestDateCalculationUseCase useCase, TestDataCalculationDataMock data, NotificationManagerMock notification) CreateUseCaseTuple()
        {
            var data = new TestDataCalculationDataMock();
            var notification = new NotificationManagerMock();
            var useCase = new TestDateCalculationUseCase(data, notification);
            return (useCase, data, notification);
        }
    }
}
