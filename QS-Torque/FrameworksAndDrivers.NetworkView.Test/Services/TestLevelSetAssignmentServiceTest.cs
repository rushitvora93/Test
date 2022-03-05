using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using NUnit.Framework;
using Server.Core.Entities;
using Server.Core.Enums;
using Server.UseCases.UseCases;
using TestHelper.Checker;
using TestLevelSetAssignmentService;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    class TestLevelSetAssignmentUseCaseMock : ITestLevelSetAssignmentUseCase
    {
        public List<LocationToolAssignment> LoadLocationToolAssignmentsReturnValue { get; set; }
        public List<(LocationToolAssignmentId, TestType)> RemoveTestLevelSetAssignmentForParameter { get; set; }
        public TestLevelSetId AssignTestLevelSetToLocationToolAssignmentsParameterTestLevelSetId { get; set; }
        public List<(LocationToolAssignmentId, TestType)> AssignTestLevelSetToLocationToolAssignmentsParameterLocToolIds { get; set; }
        public User RemoveTestLevelSetAssignmentForUserParameter { get; set; }
        public User AssignTestLevelSetToLocationToolAssignmentsUserParameter { get; set; }
        public TestLevelSetId AssignTestLevelSetToProcessControlConditionsTestLevelSetId { get; set; }
        public List<ProcessControlConditionId> AssignTestLevelSetToProcessControlConditionsProcessControlConditionIds { get; set; }
        public User AssignTestLevelSetToProcessControlConditionsUser { get; set; }
        public List<ProcessControlConditionId> RemoveTestLevelSetAssignmentForProcessControlIds { get; set; }

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
            RemoveTestLevelSetAssignmentForProcessControlIds = ids;
            RemoveTestLevelSetAssignmentForUserParameter = user;
        }
    }
    
    public class TestLevelSetAssignmentServiceTest
    {
        [TestCaseSource(nameof(CreateAnonymousListOfLocationToolAssignmentIdsAndTestTypes))]
        public void RemoveTestLevelSetAssignmentForPassesDataToUseCase(ListOfLocationToolAssignmentIdsAndTestTypes dtoList)
        {
            var tuple = CreateServiceTuple();
            tuple.service.RemoveTestLevelSetAssignmentFor(dtoList, null);
            CheckerFunctions.CollectionAssertAreEquivalent(tuple.useCase.RemoveTestLevelSetAssignmentForParameter, dtoList.Values, (entity, dto) =>
                {
                    return entity.Item1.ToLong() == dto.LocationToolAssignmentId &&
                           (int)entity.Item2 == dto.TestType;
                });
        }

        [TestCase(5)]
        [TestCase(6)]
        public void RemoveTestLevelSetAssignmentForPassesUserToUseCase(long userId)
        {
            var tuple = CreateServiceTuple();
            tuple.service.RemoveTestLevelSetAssignmentFor(new ListOfLocationToolAssignmentIdsAndTestTypes()
            {
                UserId = userId
            }, null);
            Assert.AreEqual(userId, tuple.useCase.RemoveTestLevelSetAssignmentForUserParameter.UserId.ToLong());
        }

        [TestCaseSource(nameof(CreateAnonymousListOfLocationToolAssignmentIdsAndTestTypes))]
        public void AssignTestLevelSetToLocationToolAssignmentsPassesDataToUseCase(ListOfLocationToolAssignmentIdsAndTestTypes dtoList)
        {
            var tuple = CreateServiceTuple();
            var testLevelSetId = new Random().Next();

            var serviceParameter = new AssignTestLevelSetToLocationToolAssignmentsParameter()
            {
                TestLevelSetId = testLevelSetId,
                LocationToolAssignmentIdsAndTestTypes = dtoList
            };

            tuple.service.AssignTestLevelSetToLocationToolAssignments(serviceParameter, null);
            CheckerFunctions.CollectionAssertAreEquivalent(tuple.useCase.AssignTestLevelSetToLocationToolAssignmentsParameterLocToolIds, dtoList.Values, (entity, dto) =>
            {
                return entity.Item1.ToLong() == dto.LocationToolAssignmentId &&
                       (int)entity.Item2 == dto.TestType;
            });
            Assert.AreEqual(testLevelSetId, tuple.useCase.AssignTestLevelSetToLocationToolAssignmentsParameterTestLevelSetId.ToLong());
        }

        [TestCase(5)]
        [TestCase(6)]
        public void AssignTestLevelSetToLocationToolAssignmentsForPassesUserToUseCase(long userId)
        {
            var tuple = CreateServiceTuple();
            tuple.service.AssignTestLevelSetToLocationToolAssignments(new AssignTestLevelSetToLocationToolAssignmentsParameter()
            {
                LocationToolAssignmentIdsAndTestTypes = new ListOfLocationToolAssignmentIdsAndTestTypes(),
                UserId = userId
            }, null);
            Assert.AreEqual(userId, tuple.useCase.AssignTestLevelSetToLocationToolAssignmentsUserParameter.UserId.ToLong());
        }

        [TestCase(1, 2, 3, 4, 5, 6, 7, 8, 9)]
        [TestCase(8, 56, 95, 32, 4)]
        [TestCase(100, 1000, 1000)]
        public void AssignTestLevelSetToProcessControlConditionsTest(long testLevelSetId, long userId, params long[] processControlIds)
        {
            var tuple = CreateServiceTuple();
            var param = new AssignTestLevelSetToProcessControlConditionsParameter()
            {
                TestLevelSetId = testLevelSetId,
                UserId = userId
            };
            foreach (var id in processControlIds)
            {
                param.ProcessControlConditionIds.Add(id);
            }

            tuple.service.AssignTestLevelSetToProcessControlConditions(param, null);

            Assert.AreEqual(testLevelSetId, tuple.useCase.AssignTestLevelSetToProcessControlConditionsTestLevelSetId.ToLong());
            Assert.AreEqual(userId, tuple.useCase.AssignTestLevelSetToProcessControlConditionsUser.UserId.ToLong());
            Assert.AreEqual(processControlIds.Length, tuple.useCase.AssignTestLevelSetToProcessControlConditionsProcessControlConditionIds.Count);
            foreach (long id in processControlIds)
            {
                Assert.IsTrue(tuple.useCase.AssignTestLevelSetToProcessControlConditionsProcessControlConditionIds.Any(x => x.ToLong() == id));
            }
        }

        [TestCase(12, 5, 96, 45, 85)]
        [TestCase(100, 236, 4)]
        public void RemoveTestLevelSetAssignmentForProcessControlPassesDataToUseCase(params long[] ids)
        {
            var tuple = CreateServiceTuple();
            var param = new RemoveTestLevelSetAssignmentForProcessControlParameter();
            foreach (var id in ids)
            {
                param.ProcessControlConditionIds.Add(id);
            }
            tuple.service.RemoveTestLevelSetAssignmentForProcessControl(param, null);
            foreach (var id in ids)
            {
                Assert.IsTrue(tuple.useCase.RemoveTestLevelSetAssignmentForProcessControlIds.Any(x => x.ToLong() == id));
            }
        }

        [TestCase(5)]
        [TestCase(6)]
        public void RemoveTestLevelSetAssignmentForProcessControlPassesUserToUseCase(long userId)
        {
            var tuple = CreateServiceTuple();
            tuple.service.RemoveTestLevelSetAssignmentForProcessControl(new RemoveTestLevelSetAssignmentForProcessControlParameter()
            {
                UserId = userId
            }, null);
            Assert.AreEqual(userId, tuple.useCase.RemoveTestLevelSetAssignmentForUserParameter.UserId.ToLong());
        }


        private bool AreLocationToolAssignmentEntityAndDtoEqual(LocationToolAssignment entity, DtoTypes.LocationToolAssignment dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   entity.AssignedLocation.Id.ToLong() == dto.AssignedLocation.Id &&
                   entity.AssignedLocation.Number.ToDefaultString() == dto.AssignedLocation.Number &&
                   entity.AssignedLocation.Description.ToDefaultString() == dto.AssignedLocation.Description &&
                   entity.AssignedTool.Id.ToLong() == dto.AssignedTool.Id &&
                   entity.AssignedTool.InventoryNumber == dto.AssignedTool.InventoryNumber &&
                   entity.AssignedTool.SerialNumber == dto.AssignedTool.SerialNumber &&
                   entity.TestLevelSetMfu.Id.ToLong() == dto.TestLevelSetMfu.Id &&
                   entity.TestLevelSetMfu.Name.ToDefaultString() == dto.TestLevelSetMfu.Name &&
                   entity.TestLevelNumberMfu == dto.TestLevelNumberMfu &&
                   entity.TestLevelSetChk.Id.ToLong() == dto.TestLevelSetChk.Id &&
                   entity.TestLevelSetChk.Name.ToDefaultString() == dto.TestLevelSetChk.Name &&
                   entity.TestLevelNumberChk == dto.TestLevelNumberChk;
        }
        
        private static IEnumerable<ListOfLocationToolAssignmentIdsAndTestTypes> CreateAnonymousListOfLocationToolAssignmentIdsAndTestTypes()
        {
            var list1 = new ListOfLocationToolAssignmentIdsAndTestTypes();
            var list2 = new ListOfLocationToolAssignmentIdsAndTestTypes();
            
            list1.Values.Add(new LocationToolAssignmentIdAndTestType()
            {
                LocationToolAssignmentId = 987,
                TestType = 1
            }); 
            list1.Values.Add(new LocationToolAssignmentIdAndTestType()
            {
                LocationToolAssignmentId = 465,
                TestType = 0
            }); 
            list2.Values.Add(new LocationToolAssignmentIdAndTestType()
            {
                LocationToolAssignmentId = 5,
                TestType = 1
            });

            yield return list1;
            yield return list2;
        }


        private static (NetworkView.Services.TestLevelSetAssignmentService service, TestLevelSetAssignmentUseCaseMock useCase) CreateServiceTuple()
        {
            var useCase = new TestLevelSetAssignmentUseCaseMock();
            var service = new NetworkView.Services.TestLevelSetAssignmentService(useCase, null);
            return (service, useCase);
        }
    }
}
