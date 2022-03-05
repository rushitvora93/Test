using System.Collections.Generic;
using System.Threading.Tasks;
using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using TestEquipmentService;
using TestEquipment = DtoTypes.TestEquipment;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class TestEquipmentService : global::TestEquipmentService.TestEquipmentService.TestEquipmentServiceBase
    {
        private readonly ILogger<TestEquipmentService> _logger;
        private readonly ITestEquipmentUseCase _testEquipmentUseCase;
        private readonly Mapper _mapper = new Mapper();

        public TestEquipmentService(ILogger<TestEquipmentService> logger, ITestEquipmentUseCase testEquipmentUseCase)
        {
            _logger = logger;
            _testEquipmentUseCase = testEquipmentUseCase;
        }

        [Authorize(Policy = nameof(GetTestEquipmentsByIds))]
        public override Task<ListOfTestEquipment> GetTestEquipmentsByIds(ListOfLongs request, ServerCallContext context)
        {
            var ids = new List<TestEquipmentId>();
            foreach (var id in request.Values)
            {
                ids.Add(new TestEquipmentId(id.Value));
            }

            var entities = _testEquipmentUseCase.GetTestEquipmentsByIds(ids);
            var listOfEntities = new ListOfTestEquipment();
            entities.ForEach(s => listOfEntities.TestEquipments.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(InsertTestEquipmentsWithHistory))]
        public override Task<ListOfTestEquipment> InsertTestEquipmentsWithHistory(InsertTestEquipmentsWithHistoryRequest request, ServerCallContext context)
        {
            var testEquipments = _testEquipmentUseCase.InsertTestEquipmentsWithHistory(GetTestEquipmentDiffs(request.Diffs), request.ReturnList);
            return Task.FromResult(GetListOfTestEquipmentsFromTestEquipment(testEquipments));
        }

        [Authorize(Policy = nameof(UpdateTestEquipmentsWithHistory))]
        public override Task<NoParams> UpdateTestEquipmentsWithHistory(UpdateTestEquipmentsWithHistoryRequest request, ServerCallContext context)
        {
            _testEquipmentUseCase.UpdateTestEquipmentsWithHistory(GetTestEquipmentDiffs(request.TestEquipmentDiffs), request.WithTestEquipmentModelUpdate);
            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(UpdateTestEquipmentModelsWithHistory))]
        public override Task<NoParams> UpdateTestEquipmentModelsWithHistory(UpdateTestEquipmentModelsWithHistoryRequest request,
            ServerCallContext context)
        {
            _testEquipmentUseCase.UpdateTestEquipmentModelsWithHistory(GetTestEquipmentModelDiffs(request.TestEquipmentModelDiffs));
            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(LoadTestEquipmentModels))]
        public override Task<ListOfTestEquipmentModel> LoadTestEquipmentModels(NoParams request, ServerCallContext context)
        {
            var testEquipmentModels = _testEquipmentUseCase.LoadTestEquipmentModels();
            var listOfEntities = new ListOfTestEquipmentModel();
            foreach (var testEquipmentModel in testEquipmentModels)
            {
                var model = _mapper.DirectPropertyMapping(testEquipmentModel);
                model.TestEquipments = new ListOfTestEquipment();

                foreach (var testEquipment in testEquipmentModel.TestEquipments)
                {
                    model.TestEquipments.TestEquipments.Add(_mapper.DirectPropertyMapping(testEquipment));
                }
                listOfEntities.TestEquipmentModels.Add(model);
            }
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(IsTestEquipmentInventoryNumberUnique))]
        public override Task<Bool> IsTestEquipmentInventoryNumberUnique(String request, ServerCallContext context)
        {
            var result = _testEquipmentUseCase.IsInventoryNumberUnique(new TestEquipmentInventoryNumber(request.Value));
            return Task.FromResult(new Bool() { Value = result });
        }

        [Authorize(Policy = nameof(IsTestEquipmentSerialNumberUnique))]
        public override Task<Bool> IsTestEquipmentSerialNumberUnique(String request, ServerCallContext context)
        {
            var result = _testEquipmentUseCase.IsSerialNumberUnique(new TestEquipmentSerialNumber(request.Value));
            return Task.FromResult(new Bool() { Value = result });
        }

        [Authorize(Policy = nameof(IsTestEquipmentModelNameUnique))]
        public override Task<Bool> IsTestEquipmentModelNameUnique(String request, ServerCallContext context)
        {
            var result = _testEquipmentUseCase.IsTestEquipmentModelNameUnique(new TestEquipmentModelName(request.Value));
            return Task.FromResult(new Bool() { Value = result });
        }

        [Authorize(Policy = nameof(LoadAvailableTestEquipmentTypes))]
        public override Task<ListOfLongs> LoadAvailableTestEquipmentTypes(NoParams request, ServerCallContext context)
        {
            var availableTestEquipmentTypes = _testEquipmentUseCase.LoadAvailableTestEquipmentTypes();
            var result = new ListOfLongs();
            availableTestEquipmentTypes.ForEach(x => result.Values.Add(new Long(){Value = (long)x}));
            return Task.FromResult(result);
        }

        private List<Server.Core.Diffs.TestEquipmentDiff> GetTestEquipmentDiffs(ListOfTestEquipmentDiffs listOfTestEquipmentDiffs)
        {
            var diffs = new List<Server.Core.Diffs.TestEquipmentDiff>();

            foreach (var diff in listOfTestEquipmentDiffs.Diffs)
            {
                var user = new Server.Core.Entities.User() { UserId = new UserId(diff.UserId) };
                diffs.Add(new Server.Core.Diffs.TestEquipmentDiff(user, new HistoryComment(diff.Comment?.Value),
                    GetTestEquipmentFromTestEquipmentDto(diff.OldTestEquipment),
                    GetTestEquipmentFromTestEquipmentDto(diff.NewTestEquipment)));
            }

            return diffs;
        }

        private Server.Core.Entities.TestEquipment GetTestEquipmentFromTestEquipmentDto(TestEquipment dto)
        {
            if (dto == null)
            {
                return null;
            }

            return _mapper.DirectPropertyMapping(dto);
        }

        private List<Server.Core.Diffs.TestEquipmentModelDiff> GetTestEquipmentModelDiffs(ListOfTestEquipmentModelDiffs listOfTestEquipmentModelDiffs)
        {
            var diffs = new List<Server.Core.Diffs.TestEquipmentModelDiff>();

            foreach (var diff in listOfTestEquipmentModelDiffs.Diffs)
            {
                var user = new Server.Core.Entities.User() { UserId = new UserId(diff.UserId) };
                diffs.Add(new Server.Core.Diffs.TestEquipmentModelDiff(user, new HistoryComment(diff.Comment?.Value),
                    GetTestEquipmentModelFromTestEquipmentModelDto(diff.OldTestEquipmentModel),
                    GetTestEquipmentModelFromTestEquipmentModelDto(diff.NewTestEquipmentModel)));
            }

            return diffs;
        }

        private Server.Core.Entities.TestEquipmentModel GetTestEquipmentModelFromTestEquipmentModelDto(DtoTypes.TestEquipmentModel dto)
        {
            if (dto == null)
            {
                return null;
            }

            return _mapper.DirectPropertyMapping(dto);
        }

        private ListOfTestEquipment GetListOfTestEquipmentsFromTestEquipment(List<Server.Core.Entities.TestEquipment> testEquipments)
        {
            var listOfTestEquipments = new ListOfTestEquipment();
            foreach (var testEquipment in testEquipments)
            {
                listOfTestEquipments.TestEquipments.Add(_mapper.DirectPropertyMapping(testEquipment));
            }

            return listOfTestEquipments;
        }
    }
}
