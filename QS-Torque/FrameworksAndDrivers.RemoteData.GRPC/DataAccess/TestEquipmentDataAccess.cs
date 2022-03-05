using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Common.Types.Enums;
using Core.Entities;
using Core.UseCases;
using Core.UseCases.Communication;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using TestEquipmentService;
using TestEquipment = Core.Entities.TestEquipment;
using TestEquipmentModel = Core.Entities.TestEquipmentModel;
using User = Core.Entities.User;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface ITestEquipmentClient
    {
        ListOfTestEquipmentModel LoadTestEquipmentModels();
        ListOfTestEquipment GetTestEquipmentsByIds(ListOfLongs ids);
        ListOfTestEquipment InsertTestEquipmentsWithHistory(InsertTestEquipmentsWithHistoryRequest request);
        void UpdateTestEquipmentsWithHistory(UpdateTestEquipmentsWithHistoryRequest request);
        void UpdateTestEquipmentModelsWithHistory(UpdateTestEquipmentModelsWithHistoryRequest request);
        Bool IsTestEquipmentInventoryNumberUnique(BasicTypes.String inventoryNumber);
        Bool IsTestEquipmentSerialNumberUnique(BasicTypes.String serialNumber);
        Bool IsTestEquipmentModelNameUnique(BasicTypes.String name);
        ListOfLongs LoadAvailableTestEquipmentTypes();
    }

    public class TestEquipmentDataAccess : ITestEquipmentDataAccess
    {
        private readonly IClientFactory _clientFactory;
        private readonly Mapper _mapper = new Mapper();

        public TestEquipmentDataAccess(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private ITestEquipmentClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetTestEquipmentClient();
        }

        public List<TestEquipmentModel> LoadTestEquipmentModels()
        {
            var result = new List<TestEquipmentModel>();
            var testEquipmentModelDtos = GetClient().LoadTestEquipmentModels();

            foreach (var testEquipmentModelDto in testEquipmentModelDtos.TestEquipmentModels)
            {
                var testEquipmentModel = _mapper.DirectPropertyMapping(testEquipmentModelDto);
                testEquipmentModel.TestEquipments = new List<TestEquipment>();
                if (testEquipmentModelDto?.TestEquipments?.TestEquipments != null)
                {
                    foreach (var testEquipmentDto in testEquipmentModelDto.TestEquipments.TestEquipments)
                    {
                        var testEquipment = _mapper.DirectPropertyMapping(testEquipmentDto);
                        testEquipment.TestEquipmentModel = testEquipmentModel;
                        testEquipmentModel.TestEquipments.Add(testEquipment);
                    }
                }
                
                result.Add(testEquipmentModel);
            }
            return result;
        }

        public List<TestEquipment> GetTestEquipmentsByIds(List<TestEquipmentId> ids)
        {
            //TODO: Remove after Soap is removed, because it is not used
            throw new NotImplementedException();
        }

        public void SaveTestEquipment(Core.Diffs.TestEquipmentDiff testEquipmentDiff)
        {
            if (testEquipmentDiff.OldTestEquipment.Id.ToLong() != testEquipmentDiff.NewTestEquipment.Id.ToLong())
            {
                throw new ArgumentException("Mismatching TestEquipmentIds");
            }

            if (testEquipmentDiff.OldTestEquipment.EqualsByContent(testEquipmentDiff.NewTestEquipment))
            {
                return;
            }

            var oldTestEquipment = _mapper.DirectPropertyMapping(testEquipmentDiff.OldTestEquipment);
            var newTestEquipment = _mapper.DirectPropertyMapping(testEquipmentDiff.NewTestEquipment);

            oldTestEquipment.Alive = true;
            newTestEquipment.Alive = true;

            if (oldTestEquipment.TestEquipmentModel != null)
            {
                oldTestEquipment.TestEquipmentModel.Alive = true;
            }
            if (newTestEquipment.TestEquipmentModel != null)
            {
                newTestEquipment.TestEquipmentModel.Alive = true;
            }

            var request = new UpdateTestEquipmentsWithHistoryRequest()
            {
                 TestEquipmentDiffs = new ListOfTestEquipmentDiffs()
                 {
                     Diffs =
                     {
                         new DtoTypes.TestEquipmentDiff()
                         {
                             UserId = testEquipmentDiff.User.UserId.ToLong(),
                             Comment = new NullableString(){IsNull = false, Value = testEquipmentDiff.Comment == null ? "" : testEquipmentDiff.Comment.ToDefaultString()},
                             OldTestEquipment = oldTestEquipment,
                             NewTestEquipment = newTestEquipment
                         }
                     }
                 },
                 WithTestEquipmentModelUpdate = true
            };

            GetClient().UpdateTestEquipmentsWithHistory(request);
        }

        public void SaveTestEquipmentModel(Client.Core.Diffs.TestEquipmentModelDiff testEquipmentModelDiff)
        {
            if (testEquipmentModelDiff.OldTestEquipmentModel.Id.ToLong() != testEquipmentModelDiff.NewTestEquipmentModel.Id.ToLong())
            {
                throw new ArgumentException("Mismatching TestEquipmentModelIds");
            }

            if (testEquipmentModelDiff.OldTestEquipmentModel.EqualsByContent(testEquipmentModelDiff.NewTestEquipmentModel))
            {
                return;
            }

            var oldTestEquipmentModel = _mapper.DirectPropertyMapping(testEquipmentModelDiff.OldTestEquipmentModel);
            var newTestEquipmentModel = _mapper.DirectPropertyMapping(testEquipmentModelDiff.NewTestEquipmentModel);

            oldTestEquipmentModel.Alive = true;
            newTestEquipmentModel.Alive = true;

            var request = new UpdateTestEquipmentModelsWithHistoryRequest()
            {
                TestEquipmentModelDiffs = new ListOfTestEquipmentModelDiffs()
                {
                    Diffs =
                    {
                        new DtoTypes.TestEquipmentModelDiff()
                        {
                            UserId = testEquipmentModelDiff.User.UserId.ToLong(),
                            Comment = new NullableString(){IsNull = false, Value = testEquipmentModelDiff.Comment == null ? "" : testEquipmentModelDiff.Comment.ToDefaultString()},
                            OldTestEquipmentModel = oldTestEquipmentModel,
                            NewTestEquipmentModel = newTestEquipmentModel
                        }
                    }
                }
            };

            GetClient().UpdateTestEquipmentModelsWithHistory(request);
        }

        public void RemoveTestEquipment(TestEquipment testEquipment, User user)
        {
            if (user == null)
            {
                throw new ArgumentException("User should not be null");
            }

            var oldTestEquipment = _mapper.DirectPropertyMapping(testEquipment);
            var newTestEquipment = _mapper.DirectPropertyMapping(testEquipment);
            oldTestEquipment.Alive = true;
            newTestEquipment.Alive = false;

            var request = new UpdateTestEquipmentsWithHistoryRequest()
            {
                TestEquipmentDiffs = new ListOfTestEquipmentDiffs()
                {
                    Diffs =
                    {
                        new DtoTypes.TestEquipmentDiff()
                        {
                            UserId = user.UserId.ToLong(),
                            Comment = new NullableString(){IsNull = false, Value = ""},
                            OldTestEquipment = oldTestEquipment,
                            NewTestEquipment = newTestEquipment
                        }
                        
                    }
                }
            };

            GetClient().UpdateTestEquipmentsWithHistory(request);
        }

        public TestEquipment AddTestEquipment(TestEquipment testEquipment, User byUser)
        {
            if (byUser == null)
            {
                throw new ArgumentException("User should not be null");
            }

            var request = new InsertTestEquipmentsWithHistoryRequest()
            {
                Diffs = new ListOfTestEquipmentDiffs()
                {
                    Diffs =
                    {
                        new DtoTypes.TestEquipmentDiff()
                        {
                            UserId = byUser.UserId.ToLong(),
                            Comment = new NullableString(){IsNull = false, Value = ""},
                            NewTestEquipment = _mapper.DirectPropertyMapping(testEquipment)
                        }
                    }
                },
                ReturnList = true
            };

            var result = GetClient().InsertTestEquipmentsWithHistory(request);

            if (result?.TestEquipments.FirstOrDefault() == null)
            {
                throw new NullReferenceException("Server returned null when adding a TestEquipment");
            }

            return _mapper.DirectPropertyMapping(result.TestEquipments.FirstOrDefault());
        }

        public bool IsInventoryNumberUnique(TestEquipmentInventoryNumber inventoryNumber)
        {
            return GetClient().IsTestEquipmentInventoryNumberUnique(new BasicTypes.String() { Value = inventoryNumber.ToDefaultString() }).Value;
        }

        public bool IsSerialNumberUnique(TestEquipmentSerialNumber serialNumber)
        {
            return GetClient().IsTestEquipmentSerialNumberUnique(new BasicTypes.String() { Value = serialNumber.ToDefaultString() }).Value;
        }

        public bool IsTestEquipmentModelNameUnique(TestEquipmentModelName name)
        {
            return GetClient().IsTestEquipmentModelNameUnique(new BasicTypes.String() { Value = name.ToDefaultString() }).Value;
        }

        public List<TestEquipmentType> LoadAvailableTestEquipmentTypes()
        {
            var testEquipmentTypesDtos = GetClient().LoadAvailableTestEquipmentTypes();
            var result = new List<TestEquipmentType>();
            foreach (var dto in testEquipmentTypesDtos.Values)
            {
                result.Add((TestEquipmentType)dto.Value);
            }
            return result;
        }
    }
}
