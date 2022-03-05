using System.Threading.Tasks;
using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class ShiftManagementService : global::ShiftManagementService.ShiftManagements.ShiftManagementsBase
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IShiftManagementUseCase _useCase;

        public ShiftManagementService(ILogger<AuthenticationService> logger, IShiftManagementUseCase useCase)
        {
            _logger = logger;
            _useCase = useCase;
        }

        [Authorize(Policy = nameof(GetShiftManagement))]
        public override Task<ShiftManagement> GetShiftManagement(NoParams request, ServerCallContext context)
        {
            var entity = _useCase.GetShiftManagement();
            var mapper = new Mapper();
            return Task.FromResult(mapper.DirectPropertyMapping(entity));
        }

        public override Task<NoParams> SaveShiftManagement(ShiftManagementDiff request, ServerCallContext context)
        {
            var mapper = new Mapper();
            _useCase.SaveShiftManagement(mapper.DirectPropertyMapping(request));
            return Task.FromResult(new NoParams());
        }
    }
}
