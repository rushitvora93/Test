using BasicTypes;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ProcessControlService;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using System.Collections.Generic;
using System.Threading.Tasks;
using DtoTypes;
using Microsoft.AspNetCore.Authorization;
using ProcessControlCondition = Server.Core.Entities.ProcessControlCondition;
using ProcessControlConditionDiff = Server.Core.Diffs.ProcessControlConditionDiff;
using User = Server.Core.Entities.User;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class ProcessControlService : global::ProcessControlService.ProcessControlService.ProcessControlServiceBase
    {
        private readonly ILogger<ProcessControlService> _logger;
        private readonly IProcessControlUseCase _processControlUseCase;
        private readonly Mapper _mapper = new Mapper();

        public ProcessControlService(ILogger<ProcessControlService> logger, IProcessControlUseCase processControlUseCase)
        {
            _logger = logger;
            _processControlUseCase = processControlUseCase;
        }

        [Authorize(Policy = nameof(InsertProcessControlConditionsWithHistory))]
        public override Task<ListOfProcessControlCondition> InsertProcessControlConditionsWithHistory(InsertProcessControlConditionsWithHistoryRequest request, ServerCallContext context)
        {
            var processControls = _processControlUseCase.InsertProcessControlConditionsWithHistory(GetProcessControlDiffs(request.Diffs), request.ReturnList);
            return Task.FromResult(GetListOfProcessControlConditionFromProcessControlConditions(processControls));
        }

        [Authorize(Policy = nameof(UpdateProcessControlConditionsWithHistory))]
        public override Task<NoParams> UpdateProcessControlConditionsWithHistory(UpdateProcessControlConditionsWithHistoryRequest request, ServerCallContext context)
        {
            _processControlUseCase.UpdateProcessControlConditionsWithHistory(GetProcessControlDiffs(request.ConditionDiffs));
            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(LoadProcessControlConditionForLocation))]
        public override Task<LoadProcessControlConditionForLocationResponse> LoadProcessControlConditionForLocation(Long request, ServerCallContext context)
        {
            var response = new LoadProcessControlConditionForLocationResponse();
            var processControl = _processControlUseCase.LoadProcessControlConditionForLocation(new LocationId(request.Value));
            if (processControl != null)
            {
                response.Value = _mapper.DirectPropertyMapping(processControl);
            }
            return Task.FromResult(response);
        }

        [Authorize(Policy = nameof(LoadProcessControlConditionForLocation))]
        public override Task<ListOfProcessControlConditions> LoadProcessControlConditions(NoParams request, ServerCallContext context)
        {
            var response = new ListOfProcessControlConditions();
            var entities = _processControlUseCase.LoadProcessControlConditions();
            entities.ForEach(x => response.Conditions.Add(_mapper.DirectPropertyMapping(x)));
            return Task.FromResult(response);
        }

        private ProcessControlCondition GetEntityFromDto(DtoTypes.ProcessControlCondition condition)
        {
            if (condition == null)
            {
                return null;
            }
            return _mapper.DirectPropertyMapping(condition);
        }

        private List<Server.Core.Diffs.ProcessControlConditionDiff> GetProcessControlDiffs(ListOfProcessControlConditionDiffs listOfTestEquipmentDiffs)
        {
            var diffList = new List<ProcessControlConditionDiff>();
            foreach (var processControlConditionDiff in listOfTestEquipmentDiffs.ConditionDiff)
            {
                var user = new User { UserId = new UserId(processControlConditionDiff.UserId) };
                var comment = new HistoryComment(processControlConditionDiff.Comment?.Value);
                var oldEntity = GetEntityFromDto(processControlConditionDiff.OldCondition);
                var newEntity = GetEntityFromDto(processControlConditionDiff.NewCondition);
                var diff = new ProcessControlConditionDiff(user, comment, oldEntity, newEntity);

                diffList.Add(diff);
            }

            return diffList;
        }

        private ListOfProcessControlCondition GetListOfProcessControlConditionFromProcessControlConditions(List<Server.Core.Entities.ProcessControlCondition> processControlConditions)
        {
            var listOfTestEquipments = new ListOfProcessControlCondition();
            foreach (var processControlCondition in processControlConditions)
            {
                listOfTestEquipments.ProcessControlConditions.Add(_mapper.DirectPropertyMapping(processControlCondition));
            }

            return listOfTestEquipments;
        }
    }
}
