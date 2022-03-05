using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.Core.Entities;
using Server.Core.Enums;
using Server.UseCases.UseCases;
using TestLevelSetAssignmentService;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class TestLevelSetAssignmentService : global::TestLevelSetAssignmentService.TestLevelSetAssignments.TestLevelSetAssignmentsBase
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly ITestLevelSetAssignmentUseCase _useCase;

        public TestLevelSetAssignmentService(ITestLevelSetAssignmentUseCase useCase, ILogger<AuthenticationService> logger)
        {
            _useCase = useCase;
            _logger = logger;
        }
        
        [Authorize(Policy = nameof(RemoveTestLevelSetAssignmentFor))]
        public override Task<NoParams> RemoveTestLevelSetAssignmentFor(ListOfLocationToolAssignmentIdsAndTestTypes request, ServerCallContext context)
        {
            var list = new List<(LocationToolAssignmentId, TestType)>();

            foreach (var dto in request.Values)
            {
                list.Add((new LocationToolAssignmentId(dto.LocationToolAssignmentId), (TestType)dto.TestType));
            }
            _useCase.RemoveTestLevelSetAssignmentFor(list, new Server.Core.Entities.User() { UserId = new UserId(request.UserId) });

            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(AssignTestLevelSetToLocationToolAssignments))]
        public override Task<NoParams> AssignTestLevelSetToLocationToolAssignments(AssignTestLevelSetToLocationToolAssignmentsParameter request,
            ServerCallContext context)
        {
            var list = new List<(LocationToolAssignmentId, TestType)>();

            foreach (var dto in request.LocationToolAssignmentIdsAndTestTypes.Values)
            {
                list.Add((new LocationToolAssignmentId(dto.LocationToolAssignmentId), (TestType)dto.TestType));
            }
            _useCase.AssignTestLevelSetToLocationToolAssignments(new TestLevelSetId(request.TestLevelSetId), list, new Server.Core.Entities.User() { UserId = new UserId(request.UserId) });

            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(AssignTestLevelSetToProcessControlConditions))]
        public override Task<NoParams> AssignTestLevelSetToProcessControlConditions(AssignTestLevelSetToProcessControlConditionsParameter request, ServerCallContext context)
        {
            _useCase.AssignTestLevelSetToProcessControlConditions(
                new TestLevelSetId(request.TestLevelSetId),
                request.ProcessControlConditionIds.Select(x => new ProcessControlConditionId(x)).ToList(),
                new Server.Core.Entities.User() { UserId = new UserId(request.UserId) });
            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(RemoveTestLevelSetAssignmentForProcessControl))]
        public override Task<NoParams> RemoveTestLevelSetAssignmentForProcessControl(RemoveTestLevelSetAssignmentForProcessControlParameter request, ServerCallContext context)
        {
            _useCase.RemoveTestLevelSetAssignmentFor(
                request.ProcessControlConditionIds.Select(x => new ProcessControlConditionId(x)).ToList(),
                new Server.Core.Entities.User() { UserId = new UserId(request.UserId) });
            return Task.FromResult(new NoParams());
        }
    }
}
