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
using StatusService;
using User = Server.Core.Entities.User;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class StatusService : global::StatusService.StatusService.StatusServiceBase
    {
        private readonly ILogger<StatusService> _logger;
        private readonly IStatusUseCase _statusUseCase;
        private readonly Mapper _mapper = new Mapper();

        public StatusService(ILogger<StatusService> logger, IStatusUseCase statusUseCase)
        {
            _logger = logger;
            _statusUseCase = statusUseCase;
        }

        [Authorize(Policy = nameof(LoadStatus))]
        public override Task<ListOfStatus> LoadStatus(NoParams request, ServerCallContext context)
        {
            var status = _statusUseCase.LoadStatus();
            var listOfStatus = new ListOfStatus();
            status.ForEach(s => listOfStatus.Status.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfStatus);
        }

        [Authorize(Policy = nameof(GetStatusToolLinks))]
        public override Task<ListOfToolReferenceLink> GetStatusToolLinks(LongRequest request, ServerCallContext context)
        {
            var toolLinks = _statusUseCase.GetStatusToolLinks(new StatusId(request.Value));
            var listOfToolLinks = new ListOfToolReferenceLink();
            toolLinks.ForEach(s => listOfToolLinks.ToolReferenceLinks.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfToolLinks);
        }

        [Authorize(Policy = nameof(InsertStatusWithHistory))]
        public override Task<ListOfStatus> InsertStatusWithHistory(InsertStatusWithHistoryRequest request, ServerCallContext context)
        {
            var status = _statusUseCase.InsertStatusWithHistory(GetStatusDiffs(request.StatusDiffs), request.ReturnList);
            return Task.FromResult(GetListOfStatusFromStatus(status));
        }

        [Authorize(Policy = nameof(UpdateStatusWithHistory))]
        public override Task<ListOfStatus> UpdateStatusWithHistory(UpdateStatusWithHistoryRequest request, ServerCallContext context)
        {
            var status = _statusUseCase.UpdateStatusWithHistory(GetStatusDiffs(request.StatusDiffs));
            return Task.FromResult(GetListOfStatusFromStatus(status));
        }

        private ListOfStatus GetListOfStatusFromStatus(List<Server.Core.Entities.Status> statusList)
        {
            var listOfStatus = new ListOfStatus();
            foreach (var status in statusList)
            {
                listOfStatus.Status.Add(_mapper.DirectPropertyMapping(status));
            }

            return listOfStatus;
        }

        private List<Server.Core.Diffs.StatusDiff> GetStatusDiffs(ListOfStatusDiffs listOfStatusDiffs)
        {
            var statusDiffs = new List<Server.Core.Diffs.StatusDiff>();

            foreach (var statusDiff in listOfStatusDiffs.StatusDiff)
            {
                var user = new User() {UserId = new UserId(statusDiff.UserId)};
                statusDiffs.Add(new Server.Core.Diffs.StatusDiff(user, new HistoryComment(statusDiff.Comment),
                    GetStatusFromStatusDto(statusDiff.OldStatus),
                    GetStatusFromStatusDto(statusDiff.NewStatus)));
            }

            return statusDiffs;
        }

        private Server.Core.Entities.Status GetStatusFromStatusDto(DtoTypes.Status statusDto)
        {
            if (statusDto == null)
            {
                return null;
            }

            return _mapper.DirectPropertyMapping(statusDto);
        }
    }
}
