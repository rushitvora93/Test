using System;
using System.Collections.Generic;
using System.Linq;
using Client.Core.Entities;
using Client.TestHelper.Mock;
using Core.Entities;
using Core.Enums;
using Core.UseCases.Communication;
using Core.UseCases.Communication.DataGate;
using NUnit.Framework;
using TestHelper.Factories;
using TestHelper.Mock;

namespace Core.Test.UseCases.Communication
{
    class TransferToTestEquipmentUseCaseTest
    {
        [TestCase(TestType.Mfu)]
        [TestCase(TestType.Chk)]
        public void ShowingLocationToolAssignmentsForwardsResultsAndTestTypeToGui(TestType testType)
        {
            var list = new List<LocationToolAssignmentForTransfer>();
            var environment = new Environment();
            environment.mocks.dataAccess.nextLoadLocationToolAssignments = list;
            environment.useCase.ShowLocationToolAssignments(testType);
            Assert.IsTrue(ReferenceEquals(list, environment.mocks.gui.ShowLocationToolAssignmentForTransferListParameter));
            Assert.AreEqual(testType, environment.mocks.gui.ShowLocationToolAssignmentForTransferTestTypeParameter);
        }

        [TestCase(TestType.Chk)]
        [TestCase(TestType.Mfu)]
        public void ShowingLocationToolAssignmentsCallsDataAccessWithCorrectParameter(TestType testType)
        {
            var list = new List<LocationToolAssignmentForTransfer>();
            var environment = new Environment();
            environment.mocks.dataAccess.nextLoadLocationToolAssignments = list;
            environment.useCase.ShowLocationToolAssignments(testType);
            Assert.AreEqual(testType, environment.mocks.dataAccess.LoadLocationToolAssignmentsForTransferParameterTestType);
        }


        [TestCase(false)]
        [TestCase(true)]
        public void SubmittingRotatingDataRequestsTestConditionsByIdFromDataAccess(bool transferInProgressButCancelled)
        {
            var route =
                new List<LocationToolAssignmentForTransfer>
                {
                    new LocationToolAssignmentForTransfer {LocationToolAssignmentId = new LocationToolAssignmentId(5)},
                    new LocationToolAssignmentForTransfer {LocationToolAssignmentId = new LocationToolAssignmentId(7)},
                    new LocationToolAssignmentForTransfer {LocationToolAssignmentId = new LocationToolAssignmentId(12)}
                };
            var environment = new Environment();
            environment.SetTransferBusyButCancelledByUser(transferInProgressButCancelled);
            environment.useCase.SubmitToTestEquipment(CreateTestEquipment.Anonymous(), route, TestType.Chk);
            CollectionAssert.AreEqual(
                new List<long>{ 5, 7, 12 },
                environment.mocks.locationToolAssignmentData.GetLocationToolAssignmentsByIdsParameter
                    .Select(id => id.ToLong()).ToList());
        }

        [TestCase(false)]
        [TestCase(true)]
        public void SubmittingRotatingDataRequestsOtherTestConditionsByIdFromDataAccess(bool transferInProgressButCancelled)
        {
            var route =
                new List<LocationToolAssignmentForTransfer>
                {
                    new LocationToolAssignmentForTransfer {LocationToolAssignmentId = new LocationToolAssignmentId(7825)},
                    new LocationToolAssignmentForTransfer {LocationToolAssignmentId = new LocationToolAssignmentId(565)},
                    new LocationToolAssignmentForTransfer {LocationToolAssignmentId = new LocationToolAssignmentId(3789)},
                    new LocationToolAssignmentForTransfer {LocationToolAssignmentId = new LocationToolAssignmentId(3)},
                    new LocationToolAssignmentForTransfer {LocationToolAssignmentId = new LocationToolAssignmentId(789454)}
                };
            var environment = new Environment();
            environment.SetTransferBusyButCancelledByUser(transferInProgressButCancelled);
            environment.useCase.SubmitToTestEquipment(CreateTestEquipment.Anonymous(), route, TestType.Chk);
            CollectionAssert.AreEqual(
                new List<long> { 7825, 565, 3789, 3, 789454 },
                environment.mocks.locationToolAssignmentData.GetLocationToolAssignmentsByIdsParameter
                    .Select(id => id.ToLong()).ToList());
        }

        [TestCase(false, 73485)]
        [TestCase(true, 73485)]
        [TestCase(false, 84305)]
        [TestCase(true, 84305)]
        public void SubmittingDataRequestsLocationsByIdFromDataAccess(bool transferInProgressButCancelled, int seed)
        {
            var random = new Random(seed);
            var route = new List<ProcessControlForTransfer>();
            var expectedIds = new List<LocationId>();
            var routeEntries = random.Next(10);
            for(int i = 0; i < routeEntries; i++)
            {
                var id = new LocationId(random.Next());
                expectedIds.Add(id);
                route.Add(new ProcessControlForTransfer { LocationId = id });
            }
            var environment = new Environment();
            environment.SetTransferBusyButCancelledByUser(transferInProgressButCancelled);
            environment.useCase.SubmitToTestEquipment(CreateTestEquipment.Anonymous(), route);
            CollectionAssert.AreEqual(
                expectedIds,
                environment.mocks.LocationData.LastLoadLocationsByIdsParameterIds);
        }

        [TestCase(false, 79695)]
        [TestCase(true, 79695)]
        [TestCase(false, 31597)]
        [TestCase(true, 31597)]
        public void SubmittingDataRequestsLocationProcessDataByLocationFromDataAccess(bool transferInProgressButCancelled, int seed)
        {
            var random = new Random(seed);
            var expected = new List<Location>();
            var routeEntries = random.Next(10);
            for (int i = 0; i < routeEntries; i++)
            {
                expected.Add(CreateLocation.Randomized(random.Next()));
            }
            var environment = new Environment();
            environment.SetTransferBusyButCancelledByUser(transferInProgressButCancelled);
            environment.mocks.LocationData.NextLoadLocationsByIdsReturn = expected;
            environment.SubmitToTestEquipmentWithoutRoute(CreateTestEquipment.Anonymous(), SubmissionType.Process);
            CollectionAssert.AreEqual(expected, environment.mocks.ProcessControlData.LoadProcessControlConditionForLocationParameterList);
        }

        [TestCase(false, 54986)]
        [TestCase(true, 54986)]
        public void SubmittingDataPassesProcessDataIntoSemanticModelFactory(bool transferInProgressButCancelled, int seed)
        {
            var environment = new Environment();
            var expectedProcessControlConditions = new List<ProcessControlCondition> { new ProcessControlCondition() };
            var expectedLocations = new List<Location> { CreateLocation.Anonymous() };
            environment.mocks.ProcessControlData.LoadProcessControlConditionForLocationReturnValues = expectedProcessControlConditions;
            environment.mocks.LocationData.NextLoadLocationsByIdsReturn = expectedLocations;
            environment.SetTransferBusyButCancelledByUser(transferInProgressButCancelled);
            environment.SubmitToTestEquipmentWithoutRoute(
                CreateTestEquipment.Anonymous(),
                SubmissionType.Process);

            CollectionAssert.AreEquivalent(
                expectedProcessControlConditions,
                environment.mocks.semanticModelFactory.LastConvertProcessControlCondition);
            Assert.AreSame(
                expectedLocations,
                environment.mocks.semanticModelFactory.LastConvertLocations);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void SubmittingDataPutsRouteIntoDataGateSemanticModelFactory(bool transferInProgressButCancelled)
        {
            var expected = new List<LocationToolAssignment>();
            var environment = new Environment();
            environment.SetTransferBusyButCancelledByUser(transferInProgressButCancelled);
            environment.mocks.locationToolAssignmentData.GetLocationToolAssignmentsByIdsReturnValue = expected;
            environment.useCase.SubmitToTestEquipment(
                CreateTestEquipment.Anonymous(),
                new List<LocationToolAssignmentForTransfer>() { new LocationToolAssignmentForTransfer() },
				TestType.Chk);
            Assert.AreSame(expected, environment.mocks.semanticModelFactory.lastConvertParameterRoute);
        }

        [TestCase(false, SubmissionType.ToolAnonymous)]
        [TestCase(true, SubmissionType.ToolAnonymous)]
        [TestCase(false, SubmissionType.Process)]
        [TestCase(true, SubmissionType.Process)]
        public void SubmittingDataPutsTestEquipmentIntoDataGateSemanticModelFactory(
            bool transferInProgressButCancelled,
            SubmissionType submissionType)
        {
            var expectedTestEquipment = CreateTestEquipment.Anonymous();
            var environment = new Environment();
            environment.SetTransferBusyButCancelledByUser(transferInProgressButCancelled);
            environment.SubmitToTestEquipmentWithoutRoute(
                expectedTestEquipment,
                submissionType);

            Assert.AreSame(
                expectedTestEquipment,
                environment.mocks.semanticModelFactory.lastConvertParameterTestEquipment);
        }

        [TestCase(false, 3.6, 4.8)]
        [TestCase(false, 7.8, 0.2)]
        [TestCase(true, 3.6, 4.8)]
        [TestCase(true, 7.8, 0.2)]
        public void SubmittingDataPutsCmCmkLimitsFromDataAccessIntoDataGateSemanticModelFactory(
            bool transferInProgressButCancelled,
            double cm,
            double cmk)
        {
            var environment = new Environment();
            environment.SetTransferBusyButCancelledByUser(transferInProgressButCancelled);
            environment.mocks.rewriteBuilder.nextBuildReturn = new DataGateRewriterMock();
            environment.mocks.cmCmkDataAccess.CmCmkToLoad = (cm, cmk);
            environment.useCase.SubmitToTestEquipment(
                CreateTestEquipment.Anonymous(),
                new List<LocationToolAssignmentForTransfer>() { new LocationToolAssignmentForTransfer() }, TestType.Chk);
            Assert.AreEqual((cm, cmk), environment.mocks.semanticModelFactory.lastConvertParameterCmCmk);
        }

        [TestCase(false, 1991, 9, 24, 12, 31, 25, SubmissionType.ToolAnonymous)]
        [TestCase(false, 1994, 3, 22, 5, 7, 58, SubmissionType.ToolAnonymous)]
        [TestCase(true, 1991, 9, 24, 12, 31, 25, SubmissionType.ToolAnonymous)]
        [TestCase(true, 1994, 3, 22, 5, 7, 58, SubmissionType.ToolAnonymous)]
        [TestCase(false, 1991, 9, 24, 12, 31, 25, SubmissionType.Process)]
        [TestCase(false, 1994, 3, 22, 5, 7, 58, SubmissionType.Process)]
        [TestCase(true, 1991, 9, 24, 12, 31, 25, SubmissionType.Process)]
        [TestCase(true, 1994, 3, 22, 5, 7, 58, SubmissionType.Process)]
        public void SubmittingDataPutsLocalDateIntoDataGateSemanticModelFactory(
            bool transferInProgressButCancelled,
            int year,
            int month,
            int day,
            int hour,
            int minute,
            int second,
            SubmissionType submissionType)
        {
            var expectedDateTime = new DateTime(year, month, day, hour, minute, second);
            var environment = new Environment();
            environment.SetTransferBusyButCancelledByUser(transferInProgressButCancelled);
            environment.mocks.timeDataAccess.NextLocalNow = expectedDateTime;
            environment.SubmitToTestEquipmentWithoutRoute(
                CreateTestEquipment.Anonymous(),
                submissionType);
            Assert.AreEqual(
                expectedDateTime,
                environment.mocks.semanticModelFactory.lastConvertparameterLocalNow);
        }

        [TestCase(false, TestType.Mfu)]
        [TestCase(false, TestType.Chk)]
        [TestCase(true, TestType.Mfu)]
        [TestCase(true, TestType.Chk)]
        public void SubmittingDataPutsTestTypeIntoDataGateSemanticModelFactory(
            bool transferInProgressButCancelled,
            TestType testType)
        {
            var environment = new Environment();
            environment.SetTransferBusyButCancelledByUser(transferInProgressButCancelled);
            environment.useCase.SubmitToTestEquipment(
                CreateTestEquipment.Anonymous(),
                new List<LocationToolAssignmentForTransfer>() { new LocationToolAssignmentForTransfer() }, testType);
            Assert.AreEqual(
                testType,
                environment.mocks.semanticModelFactory.lastConvertparameterTestType);
        }

        [TestCase(false, SubmissionType.ToolAnonymous)]
        [TestCase(true, SubmissionType.ToolAnonymous)]
        [TestCase(false, SubmissionType.Process)]
        [TestCase(true, SubmissionType.Process)]
        public void SubmittingDataBuildsDataGateRewriterDependingOnTestEquipment(
            bool transferInProgressButCancelled,
            SubmissionType submissionType)
        {
            var testEquipment = CreateTestEquipment.Anonymous();
            var environment = new Environment();
            environment.SetTransferBusyButCancelledByUser(transferInProgressButCancelled);
            environment.SubmitToTestEquipmentWithoutRoute(testEquipment, submissionType);
            Assert.AreSame(testEquipment, environment.mocks.rewriteBuilder.lastBuildParameterTestEquipment);
        }

        [TestCase(false, SubmissionType.ToolAnonymous)]
        [TestCase(true, SubmissionType.ToolAnonymous)]
        [TestCase(false, SubmissionType.Process)]
        [TestCase(true, SubmissionType.Process)]
        public void SubmittingDataRewritesDataGateSemanticModelWithNewlyBuiltRewriter(
            bool transferInProgressButCancelled,
            SubmissionType submissionType)
        {
            var dataGateSemanticModel = new SemanticModel(null);
            var rewriter = new DataGateRewriterMock();
            var environment = new Environment();
            environment.SetTransferBusyButCancelledByUser(transferInProgressButCancelled);
            environment.mocks.rewriteBuilder.nextBuildReturn = rewriter;
            environment.mocks.semanticModelFactory.nextConvertReturn = dataGateSemanticModel;
            environment.SubmitToTestEquipmentWithoutRoute(
                CreateTestEquipment.Anonymous(),
                submissionType);
            Assert.AreSame(
                dataGateSemanticModel,
                rewriter._lastApplyParameterDataGateSemanticModel);
        }

        [TestCase(false, SubmissionType.ToolAnonymous)]
        [TestCase(true, SubmissionType.ToolAnonymous)]
        [TestCase(false, SubmissionType.Process)]
        [TestCase(true, SubmissionType.Process)]
        public void SubmittingDataWritesRewrittenSemanticModelToDataGate(
            bool transferInProgressButCancelled,
            SubmissionType submissionType)
        {
            var expected = new SemanticModel(null);
            var rewriter = new DataGateRewriterMock();
            rewriter._overWriteOnApply = true;
            rewriter._nextOverWriteApplyDataGateSemanticModelWith = expected;
            var environment = new Environment();
            environment.SetTransferBusyButCancelledByUser(transferInProgressButCancelled);
            environment.mocks.rewriteBuilder.nextBuildReturn = rewriter;
            environment.SubmitToTestEquipmentWithoutRoute(
                CreateTestEquipment.Anonymous(),
                submissionType);
            Assert.AreSame(
                expected,
                environment.mocks.dataGateDataAccess.lastTransferToTestEquipmentParameterDataGateSemanticModel);
        }

        [TestCase(false, SubmissionType.ToolAnonymous)]
        [TestCase(true, SubmissionType.ToolAnonymous)]
        [TestCase(false, SubmissionType.Process)]
        [TestCase(true, SubmissionType.Process)]
        public void SubmittingDataWritesRewrittenSemanticModelToTestEquipment(
            bool transferInProgressButCancelled,
            SubmissionType submissionType)
        {
            var expected = CreateTestEquipment.Anonymous();
            var environment = new Environment();
            environment.SetTransferBusyButCancelledByUser(transferInProgressButCancelled);
            environment.SubmitToTestEquipmentWithoutRoute(expected, submissionType);
            Assert.AreSame(
                expected,
                environment.mocks.dataGateDataAccess.lastTransferToTestEquipmentParameterTestEquipment);
        }

        [TestCase(false, "message1", SubmissionType.ToolAnonymous)]
        [TestCase(false, "jkalsdfjls", SubmissionType.ToolAnonymous)]
        [TestCase(true, "message1", SubmissionType.ToolAnonymous)]
        [TestCase(true, "jkalsdfjls", SubmissionType.ToolAnonymous)]
        [TestCase(false, "message1", SubmissionType.Process)]
        [TestCase(false, "jkalsdfjls", SubmissionType.Process)]
        [TestCase(true, "message1", SubmissionType.Process)]
        [TestCase(true, "jkalsdfjls", SubmissionType.Process)]
        public void SubmittingAndFailingTransmissionShowsMessageInGui(
            bool transferInProgressButCancelled,
            string errormessage,
            SubmissionType submissionType)
        {
            var serialNumber = "";
            var environment = new Environment();
            environment.SetTransferBusyButCancelledByUser(transferInProgressButCancelled);
            environment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = new TestEquipmentSerialNumber(serialNumber),
                transmissionFailed = true,
                message = errormessage
            };
            environment.SubmitToTestEquipmentWithoutRoute(CreateTestEquipment.WithSerialNumber(serialNumber), submissionType);
            Assert.AreEqual(errormessage, environment.mocks.gui.lastShowTransmissionErrorParameterMessage);
        }

        [TestCase(SubmissionType.ToolAnonymous)]
        [TestCase(SubmissionType.Process)]
        public void SubmittingAndGettingValidStatusCallsSendSuccessNotification(SubmissionType submissionType)
        {
            var serialNumber = "";
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = new TestEquipmentSerialNumber(serialNumber),
                transmissionFailed = false
            };
            environment.SubmitToTestEquipmentWithoutRoute(CreateTestEquipment.WithSerialNumber(serialNumber), submissionType);
            Assert.IsTrue(environment.mocks.notificationManagerMock.SendSuccessNotificationCalled);
        }

        [TestCase(SubmissionType.ToolAnonymous)]
        [TestCase(SubmissionType.Process)]
        public void SubmittingDataStartsCommunicationProgramWithTestEquipment(SubmissionType submissionType)
        {
            var testEquipment = CreateTestEquipment.Anonymous();
            var environment = new Environment();
            environment.SubmitToTestEquipmentWithoutRoute(testEquipment, submissionType);
            Assert.AreSame(
                testEquipment,
                environment.mocks.communicationProgramController.lastStartIfNotRunningParameterTestEquipment);
        }

        [TestCase(SubmissionType.ToolAnonymous)]
        [TestCase(SubmissionType.Process)]
        public void SubmittingDataWithUnfindableCommunicationProgramShowsErrorMessage(SubmissionType submissionType)
        {
            var environment = new Environment();
            environment.mocks.communicationProgramController.StartException =
                new CommunicationProgramNotFoundException(new Exception());
            environment.SubmitToTestEquipmentWithoutRoute(CreateTestEquipment.Anonymous(), submissionType);
            Assert.IsTrue(environment.mocks.gui.ShowCommunicationProgramNotFoundErrorCalled);
        }

        [TestCase(SubmissionType.ToolAnonymous)]
        [TestCase(SubmissionType.Process)]
        public void SubmittingDataWithUncaughtExceptionThrowsException(SubmissionType submissionType)
        {
            var environment = new Environment();
            environment.mocks.communicationProgramController.StartException = new Exception();
            Assert.Throws<Exception>(() => environment.SubmitToTestEquipmentWithoutRoute(CreateTestEquipment.Anonymous(), submissionType));
        }

        [TestCase(SubmissionType.ToolAnonymous)]
        [TestCase(SubmissionType.Process)]
        public void SubmittingDataWhenBusyAskUserToCancelOldTransfer(SubmissionType submissionType)
        {
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextLastTransactionFinishedReturn = false;
            environment.SubmitToTestEquipmentWithoutRoute(CreateTestEquipment.Anonymous(), submissionType);
            Assert.IsTrue(environment.mocks.gui.wasCalledAskToCancelLastTransfer);
        }

        [TestCase(SubmissionType.ToolAnonymous)]
        [TestCase(SubmissionType.Process)]
        public void SubmittingAndNotCancelingOldTransferDoesNotCancelTransfer(SubmissionType submissionType)
        {
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextLastTransactionFinishedReturn = false;
            environment.SubmitToTestEquipmentWithoutRoute(CreateTestEquipment.Anonymous(), submissionType);
            Assert.IsFalse(environment.mocks.dataGateDataAccess.wasCancelTransferCalled);
        }

        [TestCase(SubmissionType.ToolAnonymous)]
        [TestCase(SubmissionType.Process)]
        public void SubmittingAndCancelingOldTransferCancelsTransfer(SubmissionType submissionType)
        {
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextLastTransactionFinishedReturn = false;
            environment.mocks.gui.callAskToCancelLastTransferOnCancelLastTransfer = true;
            environment.SubmitToTestEquipmentWithoutRoute(CreateTestEquipment.Anonymous(), submissionType);
            Assert.IsTrue(environment.mocks.dataGateDataAccess.wasCancelTransferCalled);
        }

        [Test]
        public void ReadingDataStartsCommunicationProgramWithTestEquipment()
        {
            var testEquipment = CreateTestEquipment.Anonymous();
            var environment = new Environment();
            environment.useCase.ReadFromTestEquipment(testEquipment);
            Assert.AreSame(
                testEquipment,
                environment.mocks.communicationProgramController.lastStartIfNotRunningParameterTestEquipment);
        }

        [Test]
        public void ReadingDataWithUnfindableCommunicationProgramShowsErrorMessage()
        {
            var environment = new Environment();
            environment.mocks.communicationProgramController.StartException =
                new CommunicationProgramNotFoundException(new Exception());
            environment.useCase.ReadFromTestEquipment(CreateTestEquipment.Anonymous());
            Assert.IsTrue(environment.mocks.gui.ShowCommunicationProgramNotFoundErrorCalled);
        }

        private static IEnumerable<List<LocationToolAssignment>>
            SubmittingDataCallsLoadTreePathForLocationsWithCorrectParameterData = new List<List<LocationToolAssignment>>
            {
                new List<LocationToolAssignment>
                {
                    CreateLocationToolAssignment.IdOnly(1),
                    CreateLocationToolAssignment.IdOnly(2),
                },
                new List<LocationToolAssignment>
                {
                    CreateLocationToolAssignment.IdOnly(11),
                    CreateLocationToolAssignment.IdOnly(22),
                    CreateLocationToolAssignment.IdOnly(33),
                    CreateLocationToolAssignment.IdOnly(44)
                }
            };

        [TestCaseSource(nameof(SubmittingDataCallsLoadTreePathForLocationsWithCorrectParameterData))]
        public void SubmittingDataCallsLoadTreePathForLocationsWithCorrectParameter(List<LocationToolAssignment> locationToolAssignments)
        {
            var environment = new Environment();
            environment.mocks.locationToolAssignmentData.GetLocationToolAssignmentsByIdsReturnValue = locationToolAssignments;
            environment.useCase.SubmitToTestEquipment(
                CreateTestEquipment.Anonymous(),
                new List<LocationToolAssignmentForTransfer>() { new LocationToolAssignmentForTransfer() }, TestType.Chk);

            CollectionAssert.AreEqual(locationToolAssignments.Select(x => x.AssignedLocation).ToList(), environment.mocks.LocationUseCase.LoadTreePathForLocationsParameter.ToList());
        }

        [Test]
        public void ReadingDataWithUncaughtExceptionThrowsException()
        {
            var environment = new Environment();
            environment.mocks.communicationProgramController.StartException = new Exception();
            Assert.Throws<Exception>(() => environment.useCase.ReadFromTestEquipment(CreateTestEquipment.Anonymous()));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void ReadingDataGeneratesReadCommandWithTestEquipment(bool transferInProgressButCancelled)
        {
            var testEquipment = CreateTestEquipment.Anonymous();
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextLastTransactionFinishedReturn = !transferInProgressButCancelled;
            environment.mocks.gui.callAskToCancelLastTransferOnCancelLastTransfer = transferInProgressButCancelled;
            environment.useCase.ReadFromTestEquipment(testEquipment);
            Assert.AreSame(
                testEquipment,
                environment.mocks.semanticModelFactory.LastReadCommandParameterTestEquipment);
        }

        [TestCase(false, 15646515963143)]
        [TestCase(false, 5611951654556)]
        [TestCase(true, 156467893143)]
        [TestCase(true, 5613331654556)]
        public void ReadingDataGeneratesReadCommandWithTimestamp(bool transferInProgressButCancelled, long ticks)
        {
            var timestamp = new DateTime(ticks);
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextLastTransactionFinishedReturn = !transferInProgressButCancelled;
            environment.mocks.gui.callAskToCancelLastTransferOnCancelLastTransfer = transferInProgressButCancelled;
            environment.mocks.timeDataAccess.NextLocalNow = timestamp;
            environment.useCase.ReadFromTestEquipment(CreateTestEquipment.Anonymous());
            Assert.AreEqual(
                timestamp,
                environment.mocks.semanticModelFactory.LastReadCommandParameterTimestamp);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void ReadingDataWritesReadCommandToTestEquipment(bool transferInProgressButCancelled)
        {
            var semanticModel = new SemanticModel(null);
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextLastTransactionFinishedReturn = !transferInProgressButCancelled;
            environment.mocks.gui.callAskToCancelLastTransferOnCancelLastTransfer = transferInProgressButCancelled;
            environment.mocks.semanticModelFactory.nextReadCommandReturn = semanticModel;
            environment.useCase.ReadFromTestEquipment(CreateTestEquipment.Anonymous());
            Assert.AreSame(
                semanticModel,
                environment.mocks.dataGateDataAccess.lastTransferToTestEquipmentParameterDataGateSemanticModel);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void ReadingDataWritesToCorrectTestEquipment(bool transferInProgressButCancelled)
        {
            var testEquipment = CreateTestEquipment.Anonymous();
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextLastTransactionFinishedReturn = !transferInProgressButCancelled;
            environment.mocks.gui.callAskToCancelLastTransferOnCancelLastTransfer = transferInProgressButCancelled;
            environment.useCase.ReadFromTestEquipment(testEquipment);
            Assert.AreSame(
                testEquipment, 
                environment.mocks.dataGateDataAccess.lastTransferToTestEquipmentParameterTestEquipment);
        }

        [Test]
        public void ReadingDataWhenBusyAsksUserToCancelOldTransfer()
        {
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextLastTransactionFinishedReturn = false;
            environment.useCase.ReadFromTestEquipment(CreateTestEquipment.Anonymous());
            Assert.IsTrue(environment.mocks.gui.wasCalledAskToCancelLastTransfer);
        }

        [Test]
        public void ReadingAndNotCancelingOldTransferDoesNotCancelTransfer()
        {
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextLastTransactionFinishedReturn = false;
            environment.useCase.ReadFromTestEquipment(CreateTestEquipment.Anonymous());
            Assert.IsFalse(environment.mocks.dataGateDataAccess.wasCancelTransferCalled);
        }

        [Test]
        public void ReadingAndCancellingCancelsOldTransfer()
        {
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextLastTransactionFinishedReturn = false;
            environment.mocks.gui.callAskToCancelLastTransferOnCancelLastTransfer = true;
            environment.useCase.ReadFromTestEquipment(CreateTestEquipment.Anonymous());
            Assert.IsTrue(environment.mocks.dataGateDataAccess.wasCancelTransferCalled);
        }

        [TestCase("serial1")]
        [TestCase("döner")]
        public void ReadingMatchingSerialNumberDoesNotShowMismatchingSerialNumberError(string serialNumber)
        {
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = new TestEquipmentSerialNumber(serialNumber),
                transmissionFailed = false
            };
            environment.useCase.ReadFromTestEquipment(
                new TestEquipment{SerialNumber = new TestEquipmentSerialNumber(serialNumber)});
            Assert.IsFalse(environment.mocks.gui.wasCalledShowMismatchingSerialNumber);
        }

        [Test]
        public void ReadingMismatchingSerialNumberShowsMismatchingSerialNumberError()
        {
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = new TestEquipmentSerialNumber("NotBrrrr"),
                transmissionFailed = false
            };
            environment.useCase.ReadFromTestEquipment(
                new TestEquipment { SerialNumber = new TestEquipmentSerialNumber("Brrrrr") });
            Assert.IsTrue(environment.mocks.gui.wasCalledShowMismatchingSerialNumber);
        }

        [TestCase("lolfail")]
        [TestCase("dieser keks ist zu hart")]
        public void ReadingAndTransmissionFailedShowsMessageInGui(string errormessage)
        {
            var serialNumber = "";
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = new TestEquipmentSerialNumber(serialNumber),
                transmissionFailed = true,
                message = errormessage
            };
            environment.useCase.ReadFromTestEquipment(CreateTestEquipment.WithSerialNumber(serialNumber));
            Assert.AreEqual(errormessage, environment.mocks.gui.lastShowTransmissionErrorParameterMessage);
        }

        [Test]
        public void ReadingAndGettingValidStatusPassesTestEquipmentToGetResults()
        {
            var testEquipment = new TestEquipment {SerialNumber = new TestEquipmentSerialNumber("")};
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };
            environment.useCase.ReadFromTestEquipment(testEquipment);
            Assert.AreSame(
                testEquipment,
                environment.mocks.dataGateDataAccess.lastGetResultsParameterTestEquipment);
        }

        [Test]
        public void ReadingAndGettingInvalidStatusDoesNotCallGetResults()
        {
            var testEquipment = new TestEquipment { SerialNumber = new TestEquipmentSerialNumber("") };
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = true
            };
            environment.useCase.ReadFromTestEquipment(testEquipment);
            Assert.IsFalse(environment.mocks.dataGateDataAccess.wasCalledGetResults);
        }

        [Test]
        public void ReadingAndGettingValidStatusPassesDataGateResultsToFillWithAssignmentsData()
        {
            var results = new DataGateResults();
            var testEquipment = new TestEquipment { SerialNumber = new TestEquipmentSerialNumber("") };
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };
            environment.mocks.dataGateDataAccess.nextGetResultsResult = results;
            environment.useCase.ReadFromTestEquipment(testEquipment);
            Assert.AreSame(
                results,
                environment.mocks.dataAccess.lastFillWithLocationToolAssignmentsDataParameterResults);
        }

        [Test]
        public void ReadingAndGettingValidStatusPassesFinalResultsToGui()
        {
            var results = new List<TestEquipmentTestResult>();
            var testEquipment = new TestEquipment { SerialNumber = new TestEquipmentSerialNumber("") };
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };
            environment.mocks.dataAccess.nextFillWithLocationToolAssignmentsDataResult = results;
            environment.useCase.ReadFromTestEquipment(testEquipment);
            Assert.AreSame(
                results,
                environment.mocks.gui.lastShowReadResultsParameterResults);
        }

        [Test]
        public void ReadingAndGettingValidStatusCallsSendSuccessNotification()
        {
            var results = new List<TestEquipmentTestResult>();
            var testEquipment = CreateTestEquipment.Anonymous();
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };
            environment.mocks.dataAccess.nextFillWithLocationToolAssignmentsDataResult = results;
            environment.useCase.ReadFromTestEquipment(testEquipment);
            Assert.IsTrue(environment.mocks.notificationManagerMock.SendSuccessNotificationCalled);
        }

        [Test]
        public void ReadingAndGettingValidStatusForChkCheckCalculatesIoIfValuesAreBetweenToleranceLimits()
        {
            var results = new List<TestEquipmentTestResult>();
            results.Add(new TestEquipmentTestResult
            {
                LocationToolAssignment = CreateLocationToolAssignment.WithTestParametersControlledBy(LocationControlledBy.Torque),
                ResultFromDataGate = new DataGateResult
                {
                    Min1 = 2.5,
                    Max1 = 3.5,
                    Values = new List<DataGateResultValue>
                    {
                        new DataGateResultValue{Value1 = 2.6},
                        new DataGateResultValue{Value1 = 2.7},
                        new DataGateResultValue{Value1 = 2.9},
                        new DataGateResultValue{Value1 = 3.0},
                        new DataGateResultValue{Value1 = 3.2},
                        new DataGateResultValue{Value1 = 3.4},
                    }
                }
            });
            var testEquipment = new TestEquipment {SerialNumber = new TestEquipmentSerialNumber("")};
            var enviroment = new Environment();
            enviroment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };
            enviroment.mocks.dataAccess.nextFillWithLocationToolAssignmentsDataResult = results;
            enviroment.useCase.ReadFromTestEquipment(testEquipment);
            Assert.IsTrue(enviroment.mocks.gui.lastShowReadResultsParameterResults[0].TestResult.IsIo);
        }

        [Test]
        public void ReadingAndGettingValidStatusForChkCheckCalculatesNioIfValuesAreNotBetweenToleranceLimits()
        {
            var results = new List<TestEquipmentTestResult>();
            results.Add(new TestEquipmentTestResult
            {
                LocationToolAssignment = CreateLocationToolAssignment.Anonymous(),
                ResultFromDataGate = new DataGateResult
                {
                    Min1 = 2.5,
                    Max1 = 3.5,
                    Values = new List<DataGateResultValue>
                    {
                        new DataGateResultValue{Value1 = 1.2},
                        new DataGateResultValue{Value1 = 1.5},
                        new DataGateResultValue{Value1 = 2.9},
                        new DataGateResultValue{Value1 = 3.0},
                        new DataGateResultValue{Value1 = 3.2},
                        new DataGateResultValue{Value1 = 3.8},
                    }
                }
            });
            var testEquipment = new TestEquipment { SerialNumber = new TestEquipmentSerialNumber("") };
            var enviroment = new Environment();
            enviroment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };
            enviroment.mocks.dataAccess.nextFillWithLocationToolAssignmentsDataResult = results;
            enviroment.useCase.ReadFromTestEquipment(testEquipment);
            Assert.IsFalse(enviroment.mocks.gui.lastShowReadResultsParameterResults[0].TestResult.IsIo);
        }

        private static List<(TestType,TestEquipmentTestResult)> testEquipmentTestResultsTestData = new List<(TestType, TestEquipmentTestResult)>
        {
            (TestType.Chk,new TestEquipmentTestResult
            {
                LocationToolAssignment = CreateLocationToolAssignment.Anonymous(),
                ResultFromDataGate = new DataGateResult
                {
                    Min1 = 2.5,
                    Max1 = 3.5,
                    Values = new List<DataGateResultValue>
                    {
                        new DataGateResultValue{Value1 = 1.2},
                        new DataGateResultValue{Value1 = 1.5},
                        new DataGateResultValue{Value1 = 2.9},
                        new DataGateResultValue{Value1 = 3.0},
                        new DataGateResultValue{Value1 = 3.2},
                        new DataGateResultValue{Value1 = 3.8},
                    }
                }
            }),
            (TestType.Mfu,new TestEquipmentTestResult
            {
                LocationToolAssignment = CreateLocationToolAssignment.Anonymous(),
                ResultFromDataGate = new DataGateResult
                {
                    Min1 = 2.5,
                    Max1 = 3.5,
                    Values = new List<DataGateResultValue>
                    {
                        new DataGateResultValue{Value1 = 1.2},
                        new DataGateResultValue{Value1 = 1.5},
                        new DataGateResultValue{Value1 = 2.9},
                        new DataGateResultValue{Value1 = 3.0},
                        new DataGateResultValue{Value1 = 3.2},
                        new DataGateResultValue{Value1 = 3.8},
                        new DataGateResultValue{Value1 = 3.8},
                        new DataGateResultValue{Value1 = 3.8},
                        new DataGateResultValue{Value1 = 3.8},
                        new DataGateResultValue{Value1 = 3.8},
                        new DataGateResultValue{Value1 = 3.8},
                    }
                }
            })
        };

        [Test]
        [TestCaseSource(nameof(testEquipmentTestResultsTestData))]
        public void ReadingAndGettingValidResultCallsSaveTestEquipmentTestResults(
            (TestType testType, TestEquipmentTestResult testEquipmentTestResult) testData)
        {
            var testEquipment = CreateTestEquipment.Anonymous();
            var enviroment = new Environment();
            enviroment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };
            var testList = new List<TestEquipmentTestResult> { testData.testEquipmentTestResult };
            enviroment.mocks.dataAccess.nextFillWithLocationToolAssignmentsDataResult = testList;
            var user = CreateUser.Anonymous();
            enviroment.mocks.userGetter.NextReturnedUser = user;
            enviroment.mocks.cmCmkDataAccess.CmCmkToLoad = (1.56, 1.67);
            enviroment.useCase.ReadFromTestEquipment(testEquipment);

            Assert.AreSame(
                testEquipment,
                enviroment.mocks.dataAccess.SaveTestEquipmentTestResultParameterTestEquipment);

            Assert.AreEqual(
                testList,
                enviroment.mocks.dataAccess.SaveTestEquipmentTestResultParameterResults);

            Assert.AreEqual(user, enviroment.mocks.dataAccess.SaveTestEquipmentTestResultParameterUser);

            Assert.AreEqual(enviroment.mocks.cmCmkDataAccess.CmCmkToLoad.cm, enviroment.mocks.dataAccess.SaveTestEquipmentTestResultParameterCmCmk.cm);
            Assert.AreEqual(enviroment.mocks.cmCmkDataAccess.CmCmkToLoad.cmk, enviroment.mocks.dataAccess.SaveTestEquipmentTestResultParameterCmCmk.cmk);
        }

        [Test]
        public void ReadingAndGettingValidResultWithMoreThanTenSamplesLoadsCmCmk()
        {
            var results = new List<TestEquipmentTestResult>();
            results.Add(new TestEquipmentTestResult
            {
                LocationToolAssignment = CreateLocationToolAssignment.Anonymous(),
                ResultFromDataGate = new DataGateResult
                {
                    Min1 = 2.5,
                    Max1 = 3.5,
                    Values = new List<DataGateResultValue>
                    {
                        new DataGateResultValue{Value1 = 1.2},
                        new DataGateResultValue{Value1 = 1.5},
                        new DataGateResultValue{Value1 = 2.9},
                        new DataGateResultValue{Value1 = 3.0},
                        new DataGateResultValue{Value1 = 3.2},
                        new DataGateResultValue{Value1 = 3.8},
                        new DataGateResultValue{Value1 = 3.8},
                        new DataGateResultValue{Value1 = 3.8},
                        new DataGateResultValue{Value1 = 3.8},
                        new DataGateResultValue{Value1 = 3.8},
                        new DataGateResultValue{Value1 = 3.8},
                    }
                }
            });
            var testEquipment = new TestEquipment { SerialNumber = new TestEquipmentSerialNumber("") };
            var enviroment = new Environment();
            enviroment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };
            enviroment.mocks.dataAccess.nextFillWithLocationToolAssignmentsDataResult = results;
            enviroment.useCase.ReadFromTestEquipment(testEquipment);
            Assert.AreEqual(1, enviroment.mocks.cmCmkDataAccess.LoadCmCmkCallCount);
        }


        [Test]
        public void ReadingAndGettingValidStatusForMfuCheckCalculatesIoifCkCmkAreBiggerOrEqualToLoadedCmCmks()
        {
            var results = new List<TestEquipmentTestResult>();
            results.Add(new TestEquipmentTestResult
            {
                LocationToolAssignment = CreateLocationToolAssignment.WithTestParametersControlledBy(LocationControlledBy.Torque),
                ResultFromDataGate = new DataGateResult
                {
                    Min1 = 2.5,
                    Max1 = 3.5,
                    Values = new List<DataGateResultValue>
                    {
                        new DataGateResultValue{Value1 = 1.2},
                        new DataGateResultValue{Value1 = 1.5},
                        new DataGateResultValue{Value1 = 2.9},
                        new DataGateResultValue{Value1 = 3.0},
                        new DataGateResultValue{Value1 = 3.2},
                        new DataGateResultValue{Value1 = 3.4},
                        new DataGateResultValue{Value1 = 3.2},
                        new DataGateResultValue{Value1 = 2.9},
                        new DataGateResultValue{Value1 = 3.2},
                        new DataGateResultValue{Value1 = 1.8},
                        new DataGateResultValue{Value1 = 2.1},
                    }
                }
            });
            var testEquipment = new TestEquipment { SerialNumber = new TestEquipmentSerialNumber("") };
            var enviroment = new Environment();
            enviroment.mocks.cmCmkDataAccess.CmCmkToLoad = (0.20, 0.02);
            enviroment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };
            enviroment.mocks.dataAccess.nextFillWithLocationToolAssignmentsDataResult = results;
            enviroment.useCase.ReadFromTestEquipment(testEquipment);
            Assert.IsTrue(enviroment.mocks.gui.lastShowReadResultsParameterResults[0].TestResult.IsIo);
        }

        [Test]
        public void ReadingAndGettingValidStatusForMfuCheckCalculatesNioifCkCmkAreSmallerThanLoadedCmCmks()
        {
            var results = new List<TestEquipmentTestResult>();
            results.Add(new TestEquipmentTestResult
            {
                LocationToolAssignment = CreateLocationToolAssignment.Anonymous(),
                ResultFromDataGate = new DataGateResult
                {
                    Min1 = 2.5,
                    Max1 = 3.5,
                    Values = new List<DataGateResultValue>
                    {
                        new DataGateResultValue{Value1 = 1.2},
                        new DataGateResultValue{Value1 = 1.5},
                        new DataGateResultValue{Value1 = 2.9},
                        new DataGateResultValue{Value1 = 3.0},
                        new DataGateResultValue{Value1 = 3.2},
                        new DataGateResultValue{Value1 = 3.4},
                        new DataGateResultValue{Value1 = 3.2},
                        new DataGateResultValue{Value1 = 2.9},
                        new DataGateResultValue{Value1 = 3.2},
                        new DataGateResultValue{Value1 = 1.8},
                        new DataGateResultValue{Value1 = 2.1},
                    }
                }
            });
            var testEquipment = new TestEquipment { SerialNumber = new TestEquipmentSerialNumber("") };
            var enviroment = new Environment();
            enviroment.mocks.cmCmkDataAccess.CmCmkToLoad = (0.50, 0.09);
            enviroment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };
            enviroment.mocks.dataAccess.nextFillWithLocationToolAssignmentsDataResult = results;
            enviroment.useCase.ReadFromTestEquipment(testEquipment);
            Assert.IsFalse(enviroment.mocks.gui.lastShowReadResultsParameterResults[0].TestResult.IsIo);
        }
        
        [TestCase(5, 8)]
        [TestCase(8, 659)]
        public void ReadFromTestEquipmentCallsMultipleTestDateCalculation(long id1, long id2)
        {
            var results = new List<TestEquipmentTestResult>();
            results.Add(new TestEquipmentTestResult
            {
                ResultFromDataGate = new DataGateResult
                {
                    Min1 = 2.5,
                    Max1 = 3.5,
                    Values = new List<DataGateResultValue>
                    {
                        new DataGateResultValue{ Value1 = 1.2, Timestamp = new DateTime() }
                    }
                },
                LocationToolAssignment = CreateLocationToolAssignment.IdOnly(id1),
            });
            results.Add(new TestEquipmentTestResult
            {
                ResultFromDataGate = new DataGateResult
                {
                    Min1 = 2.5,
                    Max1 = 3.5,
                    Values = new List<DataGateResultValue>
                    {
                        new DataGateResultValue{ Value1 = 1.2, Timestamp = new DateTime() }
                    }
                },
                LocationToolAssignment = CreateLocationToolAssignment.IdOnly(id2),
            });
            var testEquipment = new TestEquipment { SerialNumber = new TestEquipmentSerialNumber("") };
            var enviroment = new Environment();
            enviroment.mocks.cmCmkDataAccess.CmCmkToLoad = (0.50, 0.09);
            enviroment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };
            enviroment.mocks.dataAccess.nextFillWithLocationToolAssignmentsDataResult = results;
            enviroment.useCase.ReadFromTestEquipment(testEquipment);
            if (FeatureToggles.FeatureToggles.TestDateCalculation)
            {
                Assert.AreEqual(2, enviroment.mocks.testDateCalculationUseCase.CalculateToolTestDateForParamter.Count);
                Assert.AreEqual(id1, enviroment.mocks.testDateCalculationUseCase.CalculateToolTestDateForParamter[0].ToLong());
                Assert.AreEqual(id2, enviroment.mocks.testDateCalculationUseCase.CalculateToolTestDateForParamter[1].ToLong()); 
            }
        }

        [TestCase(5)]
        [TestCase(8)]
        public void ReadFromTestEquipmentCallsTestDateCalculation(long id)
        {
            var results = new List<TestEquipmentTestResult>();
            var locationToolAssignment = CreateLocationToolAssignment.IdOnly(id);
            results.Add(new TestEquipmentTestResult
            {
                ResultFromDataGate = new DataGateResult
                {
                    Min1 = 2.5,
                    Max1 = 3.5,
                    Values = new List<DataGateResultValue>
                    {
                        new DataGateResultValue{Value1 = 1.2, Timestamp = new DateTime()}
                    }
                },
                LocationToolAssignment = locationToolAssignment,
            });
            var testEquipment = new TestEquipment { SerialNumber = new TestEquipmentSerialNumber("") };
            var enviroment = new Environment();
            enviroment.mocks.cmCmkDataAccess.CmCmkToLoad = (0.50, 0.09);
            enviroment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };
            enviroment.mocks.dataAccess.nextFillWithLocationToolAssignmentsDataResult = results;
            enviroment.useCase.ReadFromTestEquipment(testEquipment);
            if (FeatureToggles.FeatureToggles.TestDateCalculation)
            {
                Assert.AreEqual(1, enviroment.mocks.testDateCalculationUseCase.CalculateToolTestDateForParamter.Count);
                Assert.AreEqual(id, enviroment.mocks.testDateCalculationUseCase.CalculateToolTestDateForParamter[0].ToLong()); 
            }
        }

        [Test]
        public void ReadingAndGettingValidStatusWritesClearCommandToDevice()
        {
            var results = new List<TestEquipmentTestResult>();
            var testEquipment = new TestEquipment { SerialNumber = new TestEquipmentSerialNumber("") };
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };
            environment.mocks.dataAccess.nextFillWithLocationToolAssignmentsDataResult = results;
            var clearCommand = new SemanticModel(new Content(new ElementName("test"), ""));
            environment.mocks.semanticModelFactory.NextClearCommandReturn = clearCommand;
            environment.useCase.ReadFromTestEquipment(testEquipment);
            Assert.AreSame(
                clearCommand,
                environment.mocks.dataGateDataAccess.lastTransferToTestEquipmentParameterDataGateSemanticModel);
        }

        [Test]
        public void ReadingAndGettingValidStatusWritesClearCommandToCorrectDevice()
        {
            var results = new List<TestEquipmentTestResult>();
            var testEquipment = new TestEquipment { SerialNumber = new TestEquipmentSerialNumber("") };
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };
            environment.mocks.dataAccess.nextFillWithLocationToolAssignmentsDataResult = results;
            environment.useCase.ReadFromTestEquipment(testEquipment);
            Assert.AreSame(
                testEquipment,
                environment.mocks.dataGateDataAccess.lastTransferToTestEquipmentParameterTestEquipment);
        }

        [Test]
        public void ReadingAndGettingValidStatusGeneratesClearCommandWithCorrectDevice()
        {
            var results = new List<TestEquipmentTestResult>();
            var testEquipment = new TestEquipment { SerialNumber = new TestEquipmentSerialNumber("") };
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };
            environment.mocks.dataAccess.nextFillWithLocationToolAssignmentsDataResult = results;
            environment.useCase.ReadFromTestEquipment(testEquipment);
            Assert.AreSame(
                testEquipment,
                environment.mocks.semanticModelFactory.LastClearCommandParameterTestEquipment);
        }

        [TestCase(123456)]
        [TestCase(654321)]
        public void ReadingAndGettingValidStatusGeneratesClearCommandWithCorrectTimestamp(long ticks)
        {
            var results = new List<TestEquipmentTestResult>();
            var testEquipment = new TestEquipment { SerialNumber = new TestEquipmentSerialNumber("") };
            var environment = new Environment();
            environment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };
            environment.mocks.dataAccess.nextFillWithLocationToolAssignmentsDataResult = results;
            environment.mocks.timeDataAccess.NextLocalNow = new DateTime(ticks);
            environment.useCase.ReadFromTestEquipment(testEquipment);
            Assert.AreEqual(
                new DateTime(ticks), 
                environment.mocks.semanticModelFactory.LastClearCommandParameterTimeStamp);
        }


        private static IEnumerable<List<Location>> ReadingFromTestEquipmentLocationsData =
            new List<List<Location>>()
            {
                new List<Location>()
                {
                    CreateLocation.IdOnly(1),
                    CreateLocation.IdOnly(2),
                    CreateLocation.IdOnly(3)
                },
                new List<Location>()
                {
                    CreateLocation.IdOnly(4),
                    CreateLocation.IdOnly(5),
                    CreateLocation.IdOnly(6),
                    CreateLocation.IdOnly(7)
                },
                null
            };

        [TestCaseSource(nameof(ReadingFromTestEquipmentLocationsData))]
        public void ReadingFromTestEquipmentCallsLoadTreePathForLocationsWithCorrectParameters(List<Location> results)
        {
            var environment = new Environment();
            var testEquipment = new TestEquipment { SerialNumber = new TestEquipmentSerialNumber("") };
            environment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };

            List<TestEquipmentTestResult> testResults = null;
            if (results != null)
            {
                testResults = new List<TestEquipmentTestResult>();
                foreach (var result in results)
                {
                    testResults.Add(
                            new TestEquipmentTestResult()
                            {
                                ResultFromDataGate = new DataGateResult() { Values = new List<DataGateResultValue>() { new DataGateResultValue { Value1 = 1.2 } } },
                                LocationToolAssignment = CreateLocationToolAssignment.WithLocation(result)
                            });
                }
            }

            environment.mocks.dataAccess.nextFillWithLocationToolAssignmentsDataResult = testResults;

            environment.useCase.ReadFromTestEquipment(testEquipment);

            CollectionAssert.AreEqual(testResults?.Select(x => x.LocationToolAssignment.AssignedLocation).ToList(),environment.mocks.LocationUseCase.LoadTreePathForLocationsParameter?.ToList());
        }


        [TestCaseSource(nameof(ReadingFromTestEquipmentLocationsData))]
        public void ReadingFromTestEquipmentCallsGetMaskedTreePathWithBase64WithCorrectParameter(List<Location> results)
        {
            var environment = new Environment();
            var testEquipment = new TestEquipment { SerialNumber = new TestEquipmentSerialNumber("") };
            environment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };

            var testResults = new List<TestEquipmentTestResult>();
            if (results != null)
            {
                foreach (var result in results)
                {
                    testResults.Add(
                        new TestEquipmentTestResult()
                        {
                            ResultFromDataGate = new DataGateResult() { Values = new List<DataGateResultValue>() { new DataGateResultValue { Value1 = 1.2 } } },
                            LocationToolAssignment = CreateLocationToolAssignment.WithLocation(result)
                        });
                }
            }

            environment.mocks.dataAccess.nextFillWithLocationToolAssignmentsDataResult = testResults;

            environment.useCase.ReadFromTestEquipment(testEquipment);

            CollectionAssert.AreEqual(testResults?.Select(x => x.LocationToolAssignment.AssignedLocation).ToList(), environment.mocks.TreePathBuilder.GetMaskedTreePathWithBase64Parameters?.ToList());
        }


        private static IEnumerable<List<string>> ReadingFromTestEquipmentSetsTreePathForTestResultsCorrectData =
            new List<List<string>>
            {
                new List<string>
                {
                    "A - B - C",
                    "D - E - F",
                    "G - H - I",
                },
                new List<string>()
                {
                    "J - K - L - M - N - O - P - Q - R",
                    "S - T - U - V - X -Y - Z",
                },
                null
            };

        [TestCaseSource(nameof(ReadingFromTestEquipmentSetsTreePathForTestResultsCorrectData))]
        public void ReadingFromTestEquipmentSetsTreePathForTestResultsCorrect(List<string> pathList)
        {
            var environment = new Environment();
            environment.mocks.TreePathBuilder.GetMaskedTreePathWithBase64ReturnValue = pathList;

            var testEquipment = new TestEquipment { SerialNumber = new TestEquipmentSerialNumber("") };
            environment.mocks.dataGateDataAccess.nextTransmissionStatus = new TransmissionStatus
            {
                serialNumber = testEquipment.SerialNumber,
                transmissionFailed = false
            };

            List<TestEquipmentTestResult> testResults = null;
            if (pathList != null)
            {
                testResults = new List<TestEquipmentTestResult>();
                foreach (var path in pathList)
                {
                    testResults.Add(
                        new TestEquipmentTestResult()
                        {
                            ResultFromDataGate = new DataGateResult() { Values = new List<DataGateResultValue>() { new DataGateResultValue { Value1 = 1.2 } } },
                            LocationToolAssignment = CreateLocationToolAssignment.WithLocation(CreateLocation.Anonymous())
                        });
                }
            }

            environment.mocks.dataAccess.nextFillWithLocationToolAssignmentsDataResult = testResults;

            environment.useCase.ReadFromTestEquipment(testEquipment);

            CollectionAssert.AreEqual(pathList?.ToList(), testResults?.Select(x => x.LocationTreePath).ToList());
        }

        [Test]
        public void SubmitRotatingToTestEquipmentWithNoTestEquipmentShowNoTestEquipmentSelectedError()
        {
            var environment = new Environment();
            environment.useCase.SubmitToTestEquipment(
                null,
                new List<LocationToolAssignmentForTransfer>(), TestType.Chk);

            Assert.IsTrue(environment.mocks.gui.ShowNoTestEquipmentSelectedErrorCalled);
            Assert.IsNull(environment.mocks.communicationProgramController.lastStartIfNotRunningParameterTestEquipment);
        }

        [Test]
        public void SubmitRotatingToTestEquipmentWithNoRouteShowNoRouteSelectedError()
        {
            var environment = new Environment();
            environment.useCase.SubmitToTestEquipment(
                CreateTestEquipment.Randomized(2345356),
                new List<LocationToolAssignmentForTransfer>(), TestType.Chk);

            Assert.IsTrue(environment.mocks.gui.ShowNoRouteSelectedErrorCalled);
            Assert.IsNull(environment.mocks.communicationProgramController.lastStartIfNotRunningParameterTestEquipment);
        }

        [Test]
        public void SubmitProcessToTestEquipmentWithNoTestEquipmentShowNoTestEquipmentSelectedError()
        {
            var environment = new Environment();
            environment.useCase.SubmitToTestEquipment(
                null,
                new List<ProcessControlForTransfer>());

            Assert.IsTrue(environment.mocks.gui.ShowNoTestEquipmentSelectedErrorCalled);
            Assert.IsNull(environment.mocks.communicationProgramController.lastStartIfNotRunningParameterTestEquipment);
        }

        [Test]
        public void SubmitProcessToTestEquipmentWithNoTestEquipmentShowNoRouteSelectedError()
        {
            var environment = new Environment();
            environment.useCase.SubmitToTestEquipment(
                CreateTestEquipment.Randomized(2345356),
                new List<ProcessControlForTransfer>());

            Assert.IsTrue(environment.mocks.gui.ShowNoRouteSelectedErrorCalled);
            Assert.IsNull(environment.mocks.communicationProgramController.lastStartIfNotRunningParameterTestEquipment);
        }

        [Test]
        public void ReadFromTestEquipmentWithNoTestEquipmentShowNoTestEquipmentSelectedError()
        {
            var environment = new Environment();
            environment.useCase.ReadFromTestEquipment(null);

            Assert.IsTrue(environment.mocks.gui.ShowNoTestEquipmentSelectedErrorCalled);
            Assert.IsNull(environment.mocks.communicationProgramController.lastStartIfNotRunningParameterTestEquipment);
        }

        [Test]
        public void ShowProcessControlDataCallsDataAccess()
        {
            var environment = new Environment();
            environment.useCase.ShowProcessControlData();
            Assert.IsTrue(environment.mocks.dataAccess.LoadProcessControlDataForTransferCalled);
        }

        [Test]
        public void ShowProcessControlDataCallsGui()
        {
            var environment = new Environment();
            var data = new List<ProcessControlForTransfer>();
            environment.mocks.dataAccess.LoadProcessControlDataForTransferReturnValue = data;
            environment.useCase.ShowProcessControlData();
            Assert.AreSame(data, environment.mocks.gui.ShowProcessControlForTransferListParameter);
        }

        [Test]
        public void ShowProcessControlDataCallsShowLoadProcessControlDataError()
        {
            var environment = new Environment();
            environment.mocks.dataAccess.LoadProcessControlDataForTransferThrowsError = true;
            environment.useCase.ShowProcessControlData();
            Assert.IsTrue(environment.mocks.gui.ShowLoadProcessControlDataErrorCalled);
        }

        private class TransferToTestEquipmentDataAccessMock : ITransferToTestEquipmentDataAccess
        {
            public List<LocationToolAssignmentForTransfer> LoadLocationToolAssignmentsForTransfer(TestType testType)
            {
                LoadLocationToolAssignmentsForTransferParameterTestType = testType;
                return nextLoadLocationToolAssignments;
            }

            public List<TestEquipmentTestResult> FillWithLocationToolAssignmentsData(DataGateResults results)
            {
                lastFillWithLocationToolAssignmentsDataParameterResults = results;
                return nextFillWithLocationToolAssignmentsDataResult;
            }

            public void SaveTestEquipmentTestResult(TestEquipment testEquipment, List<TestEquipmentTestResult> testEquipmentTestResults, (double cm, double cmk) cmCmk, User user)
            {
                SaveTestEquipmentTestResultParameterTestEquipment = testEquipment;
                SaveTestEquipmentTestResultParameterResults = testEquipmentTestResults;
                SaveTestEquipmentTestResultParameterCmCmk = cmCmk;
                SaveTestEquipmentTestResultParameterUser = user;
            }

            public List<ProcessControlForTransfer> LoadProcessControlDataForTransfer()
            {
                if (LoadProcessControlDataForTransferThrowsError)
                {
                    throw new Exception();
                }
                LoadProcessControlDataForTransferCalled = true;
                return LoadProcessControlDataForTransferReturnValue;
            }

            public bool LoadProcessControlDataForTransferThrowsError { get; set; } = false;
            public List<ProcessControlForTransfer> LoadProcessControlDataForTransferReturnValue { get; set; }
            public bool LoadProcessControlDataForTransferCalled { get; set; }
            public TestEquipment SaveTestEquipmentTestResultParameterTestEquipment { get; set; }
            public List<TestEquipmentTestResult> SaveTestEquipmentTestResultParameterResults { get; set; }
            public (double cm, double cmk) SaveTestEquipmentTestResultParameterCmCmk { get; set; }
            public User SaveTestEquipmentTestResultParameterUser { get; set; }
            public List<LocationToolAssignmentForTransfer> nextLoadLocationToolAssignments;
            public DataGateResults lastFillWithLocationToolAssignmentsDataParameterResults = null;
            public List<TestEquipmentTestResult> nextFillWithLocationToolAssignmentsDataResult = null;
            public TestType LoadLocationToolAssignmentsForTransferParameterTestType;
        }

        private class DataGateDataAccessMock : IDataGateDataAccess
        {
            public void TransferToTestEquipment(
                SemanticModel dataGateSemanticModel,
                TestEquipment testEquipment,
                Action<TransmissionStatus> withReceivedStatus)
            {
                lastTransferToTestEquipmentParameterDataGateSemanticModel = dataGateSemanticModel;
                lastTransferToTestEquipmentParameterTestEquipment = testEquipment;
                if (nextTransmissionStatus != null)
                {
                    withReceivedStatus(nextTransmissionStatus);
                }
            }

            public bool LastTransferFinished()
            {
                return nextLastTransactionFinishedReturn;
            }

            public void CancelTransfer()
            {
                wasCancelTransferCalled = true;
            }

            public DataGateResults GetResults(TestEquipment testEquipment)
            {
                lastGetResultsParameterTestEquipment = testEquipment;
                wasCalledGetResults = true;
                return nextGetResultsResult;
            }

            public SemanticModel lastTransferToTestEquipmentParameterDataGateSemanticModel = null;
            public TestEquipment lastTransferToTestEquipmentParameterTestEquipment = null;
            public TestEquipment lastReadFromTestEquipmentParameterTestEquipment = null;
            public bool nextLastTransactionFinishedReturn = true;
            public bool wasCancelTransferCalled = false;
            public TransmissionStatus nextTransmissionStatus = null;
            public TestEquipment lastGetResultsParameterTestEquipment = null;
            public bool wasCalledGetResults = false;
            public DataGateResults nextGetResultsResult = null;
        }

        private class TransferToTestEquipmentGuiMock: ITransferToTestEquipmentGui
        {
            public void ShowNoTestEquipmentSelectedError()
            {
                ShowNoTestEquipmentSelectedErrorCalled = true;
            }

            public void ShowLocationToolAssignmentForTransferList(List<LocationToolAssignmentForTransfer> locationToolAssignments, TestType testType)
            {
                ShowLocationToolAssignmentForTransferListParameter = locationToolAssignments;
                ShowLocationToolAssignmentForTransferTestTypeParameter = testType;
            }

            public void ShowCommunicationProgramNotFoundError()
            {
                ShowCommunicationProgramNotFoundErrorCalled = true;
            }

            public void AskToCancelLastTransfer(Action onCancelLastTransfer)
            {
                if (callAskToCancelLastTransferOnCancelLastTransfer)
                {
                    onCancelLastTransfer();
                }
                wasCalledAskToCancelLastTransfer = true;
            }

            public void ShowMismatchingSerialNumber()
            {
                wasCalledShowMismatchingSerialNumber = true;
            }

            public void ShowTransmissionError(string message)
            {
                lastShowTransmissionErrorParameterMessage = message;
            }

            public void ShowReadResults(List<TestEquipmentTestResult> results)
            {
                lastShowReadResultsParameterResults = results;
            }

            public void ShowProcessControlForTransferList(List<ProcessControlForTransfer> processData)
            {
                ShowProcessControlForTransferListParameter = processData;
            }
            
            public void ShowLoadProcessControlDataError()
            {
                ShowLoadProcessControlDataErrorCalled = true;
            }

            public void ShowNoRouteSelectedError()
            {
                ShowNoRouteSelectedErrorCalled = true;
            }

            public bool ShowLoadProcessControlDataErrorCalled { get; set; }
            public bool ShowNoRouteSelectedErrorCalled { get; set; }
            public List<ProcessControlForTransfer> ShowProcessControlForTransferListParameter { get; set; }
            public bool ShowCommunicationProgramNotFoundErrorCalled = false;
            public List<LocationToolAssignmentForTransfer> ShowLocationToolAssignmentForTransferListParameter;
            public TestType ShowLocationToolAssignmentForTransferTestTypeParameter;
            public bool wasCalledAskToCancelLastTransfer = false;
            public bool callAskToCancelLastTransferOnCancelLastTransfer = false;
            public bool? wasCalledShowMismatchingSerialNumber = false;
            public string lastShowTransmissionErrorParameterMessage;
            public List<TestEquipmentTestResult> lastShowReadResultsParameterResults = null;
            public bool ShowNoTestEquipmentSelectedErrorCalled;
        }

        private class SemanticModelFactoryMock : ISemanticModelFactory
        {
            public SemanticModel Convert(TestEquipment testEquipment, List<LocationToolAssignment> route, (double cm, double cmk) cmCmk, DateTime localNow, TestType testType)
            {
                lastConvertParameterRoute = route;
                lastConvertParameterTestEquipment = testEquipment;
                lastConvertParameterCmCmk = cmCmk;
                lastConvertparameterLocalNow = localNow;
                lastConvertparameterTestType = testType;
                return nextConvertReturn;
            }

            public SemanticModel ReadCommand(TestEquipment testEquipment, DateTime timestamp)
            {
                LastReadCommandParameterTestEquipment = testEquipment;
                LastReadCommandParameterTimestamp = timestamp;
                return nextReadCommandReturn;
            }

            public SemanticModel ClearCommand(TestEquipment testEquipment, DateTime timestamp)
            {
                LastClearCommandParameterTestEquipment = testEquipment;
                LastClearCommandParameterTimeStamp = timestamp;
                return NextClearCommandReturn;
            }

            public SemanticModel Convert(TestEquipment testEquipment, List<Location> locations, List<ProcessControlCondition> processControls, DateTime localNow)
            {
                lastConvertParameterTestEquipment = testEquipment;
                LastConvertLocations = locations;
                LastConvertProcessControlCondition = processControls;
                lastConvertparameterLocalNow = localNow;
                return nextConvertReturn;
            }

            public List<LocationToolAssignment> lastConvertParameterRoute = null;
            public TestEquipment lastConvertParameterTestEquipment = null;
            public (double cm, double cmk) lastConvertParameterCmCmk;
            public DateTime lastConvertparameterLocalNow;
            public TestType lastConvertparameterTestType;
            public SemanticModel nextConvertReturn = null;
            public TestEquipment LastReadCommandParameterTestEquipment = null;
            public DateTime LastReadCommandParameterTimestamp;
            public SemanticModel nextReadCommandReturn = null;
            public SemanticModel NextClearCommandReturn = null;
            public TestEquipment LastClearCommandParameterTestEquipment = null;
            public DateTime LastClearCommandParameterTimeStamp;
            public List<Location> LastConvertLocations = null;
            public List<ProcessControlCondition> LastConvertProcessControlCondition = null;
        }

        private class DataGateRewriterBuilderMock: ISemanticModelRewriterBuilder
        {
            public ISemanticModelRewriter Build(TestEquipment testEquipment)
            {
                lastBuildParameterTestEquipment = testEquipment;
                return nextBuildReturn;
            }

            public TestEquipment lastBuildParameterTestEquipment = null;
            public ISemanticModelRewriter nextBuildReturn = null;
        }

        private class DataGateRewriterMock : ISemanticModelRewriter
        {
            public void Apply(ref SemanticModel dataGateSemanticModel)
            {
                _lastApplyParameterDataGateSemanticModel = dataGateSemanticModel;
                if (_overWriteOnApply)
                {
                    dataGateSemanticModel = _nextOverWriteApplyDataGateSemanticModelWith;
                }
            }

            public SemanticModel _lastApplyParameterDataGateSemanticModel = null;
            public bool _overWriteOnApply = false;
            public SemanticModel _nextOverWriteApplyDataGateSemanticModelWith = null;
        }
        private class CommunicationProgramControllerMock : ICommunicationProgramController
        {
            public void Start(TestEquipment testEquipment)
            {
                if (StartException != null)
                {
                    throw StartException;
                }
                lastStartIfNotRunningParameterTestEquipment = testEquipment;
            }

            public Exception StartException = null;
            public TestEquipment lastStartIfNotRunningParameterTestEquipment = null;
        }


        public enum SubmissionType
        {
            ToolMfu,
            ToolChk,
            ToolAnonymous,
            Process
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    dataAccess = new TransferToTestEquipmentDataAccessMock();
                    locationToolAssignmentData = new LocationToolAssignmentDataMock();
                    dataGateDataAccess = new DataGateDataAccessMock();
                    cmCmkDataAccess = new CmCmkDataAccessMock();
                    timeDataAccess = new TimeDataAccessMock();
                    gui = new TransferToTestEquipmentGuiMock();
                    semanticModelFactory = new SemanticModelFactoryMock();
                    rewriteBuilder = new DataGateRewriterBuilderMock {nextBuildReturn = new DataGateRewriterMock()};
                    communicationProgramController = new CommunicationProgramControllerMock();
                    userGetter = new UserGetterMock();
                    testDateCalculationUseCase = new TestDateCalculationUseCaseMock();
                    notificationManagerMock = new NotificationManagerMock();
                    LocationUseCase = new LocationUseCaseMock(null,new LocationDataAccessMock());
                    TreePathBuilder = new TreePathBuilderMock();
                    LocationData = new LocationDataAccessMock();
                    ProcessControlData = new ProcessControlDataMock();
                }

                public TransferToTestEquipmentDataAccessMock dataAccess;
                public LocationToolAssignmentDataMock locationToolAssignmentData;
                public DataGateDataAccessMock dataGateDataAccess;
                public CmCmkDataAccessMock cmCmkDataAccess;
                public TimeDataAccessMock timeDataAccess;
                public TransferToTestEquipmentGuiMock gui;
                public SemanticModelFactoryMock semanticModelFactory;
                public DataGateRewriterBuilderMock rewriteBuilder;
                public CommunicationProgramControllerMock communicationProgramController;
                public UserGetterMock userGetter;
                public TestDateCalculationUseCaseMock testDateCalculationUseCase;
                public NotificationManagerMock notificationManagerMock;
                public LocationUseCaseMock LocationUseCase;
                public TreePathBuilderMock TreePathBuilder;
                public LocationDataAccessMock LocationData;
                public ProcessControlDataMock ProcessControlData;
            }

            public Environment()
            {
                mocks = new Mocks();
                useCase = new TransferToTestEquipmentUseCase(
                    mocks.dataAccess,
                    mocks.locationToolAssignmentData,
                    mocks.dataGateDataAccess,
                    mocks.cmCmkDataAccess,
                    mocks.timeDataAccess,
                    mocks.gui,
                    mocks.semanticModelFactory,
                    mocks.rewriteBuilder, 
                    mocks.communicationProgramController,
                    mocks.userGetter,
                    mocks.testDateCalculationUseCase,
                    mocks.notificationManagerMock,
                    mocks.LocationUseCase,
                    mocks.TreePathBuilder,
                    mocks.LocationData,
                    mocks.ProcessControlData);
            }

            public void SetTransferBusyButCancelledByUser(bool transferInProgressButCancelled)
            {
                mocks.dataGateDataAccess.nextLastTransactionFinishedReturn = !transferInProgressButCancelled;
                mocks.gui.callAskToCancelLastTransferOnCancelLastTransfer = transferInProgressButCancelled;
            }

            public void SubmitToTestEquipmentWithoutRoute(TestEquipment testEquipment, SubmissionType submissionType)
            {
                var emptyLocationToolAssignments = new List<LocationToolAssignmentForTransfer>{ new LocationToolAssignmentForTransfer() };
				var emptyProcess = new List<ProcessControlForTransfer>{ new ProcessControlForTransfer() };
                switch (submissionType)
                {
                    case SubmissionType.ToolMfu:
                        useCase.SubmitToTestEquipment(testEquipment, emptyLocationToolAssignments, TestType.Mfu);
                        break;
                    case SubmissionType.ToolChk:
                        useCase.SubmitToTestEquipment(testEquipment, emptyLocationToolAssignments, TestType.Chk);
                        break;
                    case SubmissionType.ToolAnonymous:
                        useCase.SubmitToTestEquipment(testEquipment, emptyLocationToolAssignments, TestType.Chk);
                        break;
                    case SubmissionType.Process:
                        useCase.SubmitToTestEquipment(testEquipment, emptyProcess);
                        break;
                }
            }

            public TransferToTestEquipmentUseCase useCase;
            public Mocks mocks;
        }
    }
}
