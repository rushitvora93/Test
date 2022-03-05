using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using SetupService;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class SetupService : global::SetupService.SetupService.SetupServiceBase
    {
        private readonly ILogger<SetupService> _logger;
        private readonly ISetupUseCase _setupUseCase;
        private readonly Mapper _mapper = new Mapper();

        public SetupService(ILogger<SetupService> logger, ISetupUseCase setupUseCase)
        {
            _logger = logger;
            _setupUseCase = setupUseCase;
        }

        [Authorize(Policy = nameof(GetColumnWidthsForGrid))]
        public override Task<ListOfQstSetup> GetColumnWidthsForGrid(GetColumnWidthsForGridRequest request, ServerCallContext context)
        {
            var setups = _setupUseCase.GetColumnWidthsForGrid(request.UserId, request.GridName, request.Columns.ToList());
            var listOfSetups = new ListOfQstSetup();
            setups.ForEach(s => listOfSetups.SetupList.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfSetups);
        }

        [Authorize(Policy = nameof(GetQstSetupsByUserIdAndName))]
        public override Task<ListOfQstSetup> GetQstSetupsByUserIdAndName(GetQstSetupsByUserIdAndNameRequest request, ServerCallContext context)
        {
            var setups = _setupUseCase.GetQstSetupsByUserIdAndName(request.UserId, request.Name);
            var listOfSetups = new ListOfQstSetup();
            setups.ForEach(s => listOfSetups.SetupList.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfSetups);
        }

        [Authorize(Policy = nameof(InsertOrUpdateQstSetups))]
        public override Task<ListOfQstSetup> InsertOrUpdateQstSetups(InsertOrUpdateQstSetupsRequest request, ServerCallContext context)
        {
            var setup2Insert = new List<QstSetup>();
            foreach (var setup in request.SetupList)
            {
                setup2Insert.Add(_mapper.DirectPropertyMapping(setup));
            }
            var setups = _setupUseCase.InsertOrUpdateQstSetups(setup2Insert, request.ReturnList);
            var listOfSetups = new ListOfQstSetup();
            setups?.ForEach(s => listOfSetups.SetupList.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfSetups);
        }
    }
}
