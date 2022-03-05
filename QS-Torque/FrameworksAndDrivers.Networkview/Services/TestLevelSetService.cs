using System.Threading.Tasks;
using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.UseCases.UseCases;
using TestLevelSetService;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class TestLevelSetService : global::TestLevelSetService.TestLevelSets.TestLevelSetsBase
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly ITestLevelSetUseCase _useCase;

        public TestLevelSetService(ILogger<AuthenticationService> logger, ITestLevelSetUseCase useCase)
        {
            _logger = logger;
            _useCase = useCase;
        }

        [Authorize(Policy = nameof(LoadTestLevelSets))]
        public override Task<ListOfTestLevelSets> LoadTestLevelSets(NoParams request, ServerCallContext context)
        {
            var entities = _useCase.LoadTestLevelSets();
            var dtos = new ListOfTestLevelSets();
            var mapper = new Mapper();

            foreach (var entity in entities)
            {
                dtos.TestLevelSets.Add(mapper.DirectPropertyMapping(entity));
            }

            return Task.FromResult(dtos);
        }

        [Authorize(Policy = nameof(InsertTestLevelSet))]
        public override Task<TestLevelSet> InsertTestLevelSet(TestLevelSetDiff request, ServerCallContext context)
        {
            var mapper = new Mapper();
            var result = _useCase.InsertTestLevelSet(new Server.Core.Diffs.TestLevelSetDiff()
            {
                New = mapper.DirectPropertyMapping(request.New),
                User = new Server.Core.Entities.User() { UserId = new Server.Core.Entities.UserId(request.UserId) }
            });
            return Task.FromResult(mapper.DirectPropertyMapping(result));
        }

        [Authorize(Policy = nameof(DeleteTestLevelSet))]
        public override Task<NoParams> DeleteTestLevelSet(TestLevelSetDiff request, ServerCallContext context)
        {
            var mapper = new Mapper();
            _useCase.DeleteTestLevelSet(new Server.Core.Diffs.TestLevelSetDiff()
            {
                Old = mapper.DirectPropertyMapping(request.Old),
                User = new Server.Core.Entities.User() { UserId = new Server.Core.Entities.UserId(request.UserId) }
            });
            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(UpdateTestLevelSet))]
        public override Task<NoParams> UpdateTestLevelSet(TestLevelSetDiff request, ServerCallContext context)
        {
            var mapper = new Mapper();
            _useCase.UpdateTestLevelSet(new Server.Core.Diffs.TestLevelSetDiff() { 
                New = mapper.DirectPropertyMapping(request.New),
                Old = mapper.DirectPropertyMapping(request.Old),
                Comment = new Server.Core.Entities.HistoryComment(request.Comment),
                User = new Server.Core.Entities.User() { UserId = new Server.Core.Entities.UserId(request.UserId) }
            });
            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(IsTestLevelSetNameUnique))]
        public override Task<Bool> IsTestLevelSetNameUnique(StringResponse request, ServerCallContext context)
        {
            var result = _useCase.IsTestLevelSetNameUnique(request.Value);
            return Task.FromResult(new Bool() { Value = result });
        }

        [Authorize(Policy = nameof(DoesTestLevelSetHaveReferences))]
        public override Task<Bool> DoesTestLevelSetHaveReferences(TestLevelSet request, ServerCallContext context)
        {
            var mapper = new Mapper();
            var result = _useCase.DoesTestLevelSetHaveReferences(mapper.DirectPropertyMapping(request));
            return Task.FromResult(new Bool() { Value = result });
        }
    }
}
