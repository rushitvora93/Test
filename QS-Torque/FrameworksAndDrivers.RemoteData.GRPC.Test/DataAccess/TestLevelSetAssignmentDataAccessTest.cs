using System;
using System.Collections.Generic;
using System.Linq;
using Client.TestHelper.Mock;
using Core.Entities;
using Core.Enums;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Mock;
using TestLevelSetAssignmentService;
using LocationToolAssignment = Core.Entities.LocationToolAssignment;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    class TestLevelSetAssignmentClientMock : ITestLevelSetAssignmentClient
    {
        public ListOfLocationToolAssignments LoadLocationToolAssignmentsReturnValue { get; set; }
        public ListOfLocationToolAssignmentIdsAndTestTypes RemoveTestLevelSetAssignmentForParameter { get; set; }
        public AssignTestLevelSetToLocationToolAssignmentsParameter AssignTestLevelSetToLocationToolAssignmentsParameter { get; set; }
        public AssignTestLevelSetToProcessControlConditionsParameter AssignTestLevelSetToProcessControlConditionsParameter { get; set; }
        public RemoveTestLevelSetAssignmentForProcessControlParameter RemoveTestLevelSetAssignmentForProcessControlParameter { get; set; }

        public ListOfLocationToolAssignments LoadLocationToolAssignments()
        {
            return LoadLocationToolAssignmentsReturnValue;
        }

        public void RemoveTestLevelSetAssignmentFor(ListOfLocationToolAssignmentIdsAndTestTypes param)
        {
            RemoveTestLevelSetAssignmentForParameter = param;
        }

        public void AssignTestLevelSetToLocationToolAssignments(AssignTestLevelSetToLocationToolAssignmentsParameter param)
        {
            AssignTestLevelSetToLocationToolAssignmentsParameter = param;
        }

        public void AssignTestLevelSetToProcessControlConditions(AssignTestLevelSetToProcessControlConditionsParameter param)
        {
            AssignTestLevelSetToProcessControlConditionsParameter = param;
        }

        public void RemoveTestLevelSetAssignmentForProcessControl(RemoveTestLevelSetAssignmentForProcessControlParameter param)
        {
            RemoveTestLevelSetAssignmentForProcessControlParameter = param;
        }
    }

    public class TestLevelSetAssignmentDataAccessTest
    {
        [TestCaseSource(nameof(CreateAnonymousLocationToolAssignmentDtos))]
        public void LoadLocationToolAssignmentsReturnsDataFromClient(List<DtoTypes.LocationToolAssignment> dtos)
        {
            var tuple = CreateDataAccessTuple();
            tuple.locationToolAssignmentClient.LoadLocationToolAssignmentsReturnValue = new ListOfLocationToolAssignments();

            foreach (var dto in dtos)
            {
                tuple.locationToolAssignmentClient.LoadLocationToolAssignmentsReturnValue.Values.Add(dto);
            }

            var result = tuple.dataAccess.LoadLocationToolAssignments();
            CheckerFunctions.CollectionAssertAreEquivalent(result, dtos, AreLocationToolAssignmentEntityAndDtoEqual);
        }

        [TestCaseSource(nameof(CreateAnonymousListOfLocationToolAssignmentIdsAndTestTypes))]
        public void RemoveTestLevelSetAssignmentForPassesDataToClient(List<(LocationToolAssignmentId, TestType)> list)
        {
            var tuple = CreateDataAccessTuple();
            tuple.dataAccess.RemoveTestLevelSetAssignmentFor(list, new Core.Entities.User() { UserId = new UserId(0) });
            CheckerFunctions.CollectionAssertAreEquivalent(list, tuple.client.RemoveTestLevelSetAssignmentForParameter.Values, (entity, dto) =>
            {
                return entity.Item1.ToLong() == dto.LocationToolAssignmentId &&
                       (int)entity.Item2 == dto.TestType;
            });
        }

        [TestCase(5)]
        [TestCase(6)]
        public void RemoveTestLevelSetAssignmentForPassesUserToClient(long userId)
        {
            var tuple = CreateDataAccessTuple();
            tuple.dataAccess.RemoveTestLevelSetAssignmentFor(new List<(LocationToolAssignmentId, TestType)>(),
                new Core.Entities.User
                {
                    UserId = new UserId(userId)
                });
            Assert.AreEqual(userId, tuple.client.RemoveTestLevelSetAssignmentForParameter.UserId);
        }

        [TestCaseSource(nameof(CreateAnonymousListOfLocationToolAssignmentIdsAndTestTypes))]
        public void AssignTestLevelSetToLocationToolAssignmentsPassesDataToClient(List<(LocationToolAssignmentId, TestType)> list)
        {
            var tuple = CreateDataAccessTuple();
            var testLevelSetId = new TestLevelSetId(new Random().Next());
            tuple.dataAccess.AssignTestLevelSetToLocationToolAssignments(testLevelSetId, list, new Core.Entities.User() { UserId = new UserId(0) });
            CheckerFunctions.CollectionAssertAreEquivalent(list, tuple.client.AssignTestLevelSetToLocationToolAssignmentsParameter.LocationToolAssignmentIdsAndTestTypes.Values, (entity, dto) =>
            {
                return entity.Item1.ToLong() == dto.LocationToolAssignmentId &&
                       (int)entity.Item2 == dto.TestType;
            });
            Assert.AreEqual(testLevelSetId.ToLong(), tuple.client.AssignTestLevelSetToLocationToolAssignmentsParameter.TestLevelSetId);
        }

        [TestCase(5)]
        [TestCase(6)]
        public void AssignTestLevelSetToLocationToolAssignmentsPassesUserToClient(long userId)
        {
            var tuple = CreateDataAccessTuple();
            tuple.dataAccess.AssignTestLevelSetToLocationToolAssignments(new TestLevelSetId(0), new List<(LocationToolAssignmentId, TestType)>(),
                new Core.Entities.User
                {
                    UserId = new UserId(userId)
                });
            Assert.AreEqual(userId, tuple.client.AssignTestLevelSetToLocationToolAssignmentsParameter.UserId);
        }

        [TestCase(1, 2, 3, 4, 5, 6, 7, 8, 9)]
        [TestCase(8, 56, 95, 32, 4)]
        [TestCase(100, 1000, 1000)]
        public void AssignTestLevelSetToProcessControlConditionsTest(long testLevelSetId, long userId, params long[] processControlIds)
        {
            var tuple = CreateDataAccessTuple();
            tuple.dataAccess.AssignTestLevelSetToProcessControlConditions(
                new TestLevelSetId(testLevelSetId), 
                new List<long>(processControlIds).Select(x => new Client.Core.Entities.ProcessControlConditionId(x)).ToList(), 
                new Core.Entities.User() { UserId = new UserId(userId) });
            Assert.AreEqual(testLevelSetId, tuple.client.AssignTestLevelSetToProcessControlConditionsParameter.TestLevelSetId);
            Assert.AreEqual(userId, tuple.client.AssignTestLevelSetToProcessControlConditionsParameter.UserId);
            Assert.AreEqual(processControlIds.Length, tuple.client.AssignTestLevelSetToProcessControlConditionsParameter.ProcessControlConditionIds.Count);
            foreach(long id in processControlIds)
            {
                Assert.IsTrue(tuple.client.AssignTestLevelSetToProcessControlConditionsParameter.ProcessControlConditionIds.Contains(id));
            }
        }

        [TestCase(1, 3, 5, 9, 5)]
        [TestCase(100, 20, 69,88)]
        public void RemoveTestLevelSetAssignmentForProcessControlPassesDataToClient(params long[] ids)
        {
            var tuple = CreateDataAccessTuple();
            tuple.dataAccess.RemoveTestLevelSetAssignmentFor(new List<long>(ids).Select(x => new Client.Core.Entities.ProcessControlConditionId(x)).ToList(), new Core.Entities.User() { UserId = new UserId(0) });
            foreach (var id in ids)
            {
                Assert.IsTrue(tuple.client.RemoveTestLevelSetAssignmentForProcessControlParameter.ProcessControlConditionIds.Contains(id));
            }
        }

        [TestCase(5)]
        [TestCase(6)]
        public void RemoveTestLevelSetAssignmentForProcessControlPassesUserToClient(long userId)
        {
            var tuple = CreateDataAccessTuple();
            tuple.dataAccess.RemoveTestLevelSetAssignmentFor(new List<Client.Core.Entities.ProcessControlConditionId>(),
                new Core.Entities.User
                {
                    UserId = new UserId(userId)
                });
            Assert.AreEqual(userId, tuple.client.RemoveTestLevelSetAssignmentForProcessControlParameter.UserId);
        }



        private bool AreLocationToolAssignmentEntityAndDtoEqual(LocationToolAssignment entity, DtoTypes.LocationToolAssignment dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   entity.AssignedLocation.Id.ToLong() == dto.AssignedLocation.Id &&
                   entity.AssignedLocation.Number.ToDefaultString() == dto.AssignedLocation.Number &&
                   entity.AssignedLocation.Description.ToDefaultString() == dto.AssignedLocation.Description &&
                   entity.AssignedTool.Id.ToLong() == dto.AssignedTool.Id &&
                   entity.AssignedTool.InventoryNumber?.ToDefaultString() == dto.AssignedTool.InventoryNumber &&
                   entity.AssignedTool.SerialNumber?.ToDefaultString() == dto.AssignedTool.SerialNumber &&
                   entity.TestLevelSetMfu.Id.ToLong() == dto.TestLevelSetMfu.Id &&
                   entity.TestLevelSetMfu.Name.ToDefaultString() == dto.TestLevelSetMfu.Name &&
                   entity.TestLevelNumberMfu == dto.TestLevelNumberMfu &&
                   entity.TestLevelSetChk.Id.ToLong() == dto.TestLevelSetChk.Id &&
                   entity.TestLevelSetChk.Name.ToDefaultString() == dto.TestLevelSetChk.Name &&
                   entity.TestLevelNumberChk == dto.TestLevelNumberChk;
        }

        private static IEnumerable<List<DtoTypes.LocationToolAssignment>> CreateAnonymousLocationToolAssignmentDtos()
        {
            yield return new List<DtoTypes.LocationToolAssignment>()
            {
                new DtoTypes.LocationToolAssignment()
                {
                    Id = 654,
                    AssignedLocation = new DtoTypes.Location()
                    {
                        Id = 98,
                        Number = "gvbrhu3io0",
                        Description = "vbgzhu89ijn"
                    },
                    AssignedTool = new DtoTypes.Tool()
                    {
                        Id = 852,
                        SerialNumber = "whn9tf5uzwpi9tf",
                        InventoryNumber = "aeertujqü0tjcuq"
                    },
                    TestLevelSetMfu = new DtoTypes.TestLevelSet()
                    {
                        Id = 67890,
                        Name = "wi4uüq3kcu"
                    },
                    TestLevelNumberMfu = 8,
                    TestLevelSetChk = new DtoTypes.TestLevelSet()
                    {
                        Id = 67890,
                        Name = "wi4uüq3kcu"
                    },
                    TestLevelNumberChk = 8
                },
                new DtoTypes.LocationToolAssignment()
                {
                    Id = 654,
                    AssignedLocation = new DtoTypes.Location()
                    {
                        Id = 679,
                        Number = "w4f5",
                        Description = "z8k9p"
                    },
                    AssignedTool = new DtoTypes.Tool()
                    {
                        Id = 3568,
                        SerialNumber = "r6tk7mir6",
                        InventoryNumber = "rtj67"
                    },
                    TestLevelSetMfu = new DtoTypes.TestLevelSet()
                    {
                        Id = 63,
                        Name = "ftguzh"
                    },
                    TestLevelNumberMfu = 495,
                    TestLevelSetChk = new DtoTypes.TestLevelSet()
                    {
                        Id = 67890,
                        Name = "wi4uüq3kcu"
                    },
                    TestLevelNumberChk = 8
                }
            };
            yield return new List<DtoTypes.LocationToolAssignment>()
            {
                new DtoTypes.LocationToolAssignment()
                {
                    Id = 2345,
                    AssignedLocation = new DtoTypes.Location()
                    {
                        Id = 453,
                        Number = "dfgh",
                        Description = "w3z"
                    },
                    AssignedTool = new DtoTypes.Tool()
                    {
                        Id = 86,
                        SerialNumber = "we4h5",
                        InventoryNumber = "w4j"
                    },
                    TestLevelSetMfu = new DtoTypes.TestLevelSet()
                    {
                        Id = 67890,
                        Name = "acw"
                    },
                    TestLevelNumberMfu = 85,
                    TestLevelSetChk = new DtoTypes.TestLevelSet()
                    {
                        Id = 67890,
                        Name = "acw"
                    },
                    TestLevelNumberChk = 85
                }
            };
        }

        private static IEnumerable<List<(LocationToolAssignmentId, TestType)>> CreateAnonymousListOfLocationToolAssignmentIdsAndTestTypes()
        {
            yield return new List<(LocationToolAssignmentId, TestType)>()
            {
                (new LocationToolAssignmentId(65), TestType.Chk),
                (new LocationToolAssignmentId(987), TestType.Mfu)
            };
            yield return new List<(LocationToolAssignmentId, TestType)>()
            {
                (new LocationToolAssignmentId(38), TestType.Chk)
            };
        }

        private static (TestLevelSetAssignmentDataAccess dataAccess, TestLevelSetAssignmentClientMock client, LocationToolAssignmentClientMock locationToolAssignmentClient) CreateDataAccessTuple()
        {
            var client = new TestLevelSetAssignmentClientMock();
            var locationToolAssignmentClient = new LocationToolAssignmentClientMock();
            var clientFactory = new ClientFactoryMock()
            {
                AuthenticationChannel = new ChannelWrapperMock()
                {
                    GetTestLevelSetAssignmentClientReturnValue = client,
                    GetLocationToolAssignmentClientReturnValue = locationToolAssignmentClient
                }
            };
            var dataAccess = new TestLevelSetAssignmentDataAccess(clientFactory);

            return (dataAccess, client, locationToolAssignmentClient);
        }
    }
}
