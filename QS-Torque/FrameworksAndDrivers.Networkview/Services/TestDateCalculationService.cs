using System.Linq;
using System.Threading.Tasks;
using BasicTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using TestDateCalculationService;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class TestDateCalculationService : global::TestDateCalculationService.TestDateCalculations.TestDateCalculationsBase
    {
        private readonly ILogger<NetworkView.Services.AuthenticationService> _logger;
        private readonly ITestDateCalculationUseCase _useCase;

        public TestDateCalculationService(ITestDateCalculationUseCase useCase, ILogger<NetworkView.Services.AuthenticationService> logger)
        {
            _useCase = useCase;
            _logger = logger;
        }

        [Authorize(Policy = nameof(CalculateToolTestDateFor))]
        public override Task<NoParams> CalculateToolTestDateFor(ListOfLocationToolAssignmentIds request, ServerCallContext context)
        {
            _useCase.CalculateToolTestDateFor(request.Values.Select(x => new LocationToolAssignmentId(x)).ToList());
            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(CalculateToolTestDateForAllLocationToolAssignments))]
        public override Task<NoParams> CalculateToolTestDateForAllLocationToolAssignments(NoParams request, ServerCallContext context)
        {
            _useCase.CalculateToolTestDateForAllLocationToolAssignments();
            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(CalculateToolTestDateForTestLevelSet))]
        public override Task<NoParams> CalculateToolTestDateForTestLevelSet(Long request, ServerCallContext context)
        {
            _useCase.CalculateToolTestDateForTestLevelSet(new TestLevelSetId(request.Value));
            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(CalculateProcessControlDateFor))]
        public override Task<NoParams> CalculateProcessControlDateFor(ListOfProcessControlConditionIds request, ServerCallContext context)
        {
            _useCase.CalculateProcessControlDateFor(request.Values.Select(x => new ProcessControlConditionId(x)).ToList());
            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(CalculateProcessControlDateForAllProcessControlConditions))]
        public override Task<NoParams> CalculateProcessControlDateForAllProcessControlConditions(NoParams request, ServerCallContext context)
        {
            _useCase.CalculateProcessControlDateForAllProcessControlConditions();
            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(CalculateProcessControlDateForTestLevelSet))]
        public override Task<NoParams> CalculateProcessControlDateForTestLevelSet(Long request, ServerCallContext context)
        {
            _useCase.CalculateProcessControlDateForTestLevelSet(new TestLevelSetId(request.Value));
            return Task.FromResult(new NoParams());
        }
    }
}
