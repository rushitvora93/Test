using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Grpc.Core;
using LocationToolAssignmentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.Core.Entities;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class LocationToolAssignmentService : LocationToolAssignments.LocationToolAssignmentsBase
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly ILocationToolAssignmentUseCase _useCase;
        private readonly Mapper _mapper = new Mapper();

        public LocationToolAssignmentService(ILocationToolAssignmentUseCase useCase, ILogger<AuthenticationService> logger)
        {
            _useCase = useCase;
            _logger = logger;
        }

        [Authorize(Policy = nameof(LoadLocationToolAssignments))]
        public override Task<ListOfLocationToolAssignments> LoadLocationToolAssignments(NoParams request, ServerCallContext context)
        {
            var mapper = new Mapper();
            var entities = _useCase.LoadLocationToolAssignments();
            var dtos = new ListOfLocationToolAssignments();

            foreach (var entity in entities)
            {
                dtos.Values.Add(mapper.DirectPropertyMapping(entity));
            }

            return Task.FromResult(dtos);
        }

        [Authorize(Policy = nameof(LoadLocationReferenceLinksForTool))]
        public override Task<ListOfLocationLink> LoadLocationReferenceLinksForTool(Long request, ServerCallContext context)
        {
            var locationLinks = _useCase.LoadLocationReferenceLinksForTool(new ToolId(request.Value));
            var listOfLocationLink = new ListOfLocationLink();
            locationLinks.ForEach(s => listOfLocationLink.LocationLinks.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfLocationLink);
        }

        [Authorize(Policy = nameof(LoadUnusedToolUsagesForLocation))]
        public override Task<ListOfToolUsage> LoadUnusedToolUsagesForLocation(Long request, ServerCallContext context)
        {
            var entities = _useCase.LoadUnusedToolUsagesForLocation(new LocationId(request.Value));
            var listOfEntities = new ListOfToolUsage();
            entities.ForEach(s => listOfEntities.ToolUsageList.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(GetLocationToolAssignmentsByLocationId))]
        public override Task<ListOfLocationToolAssignments> GetLocationToolAssignmentsByLocationId(Long request, ServerCallContext context)
        {
            var entities = _useCase.GetLocationToolAssignmentsByLocationId(new LocationId(request.Value));
            var listOfEntities = new ListOfLocationToolAssignments();
            entities.ForEach(s => listOfEntities.Values.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities); 
        }

        [Authorize(Policy = nameof(GetLocationToolAssignmentsByIds))]
        public override Task<ListOfLocationToolAssignments> GetLocationToolAssignmentsByIds(ListOfLongs request, ServerCallContext context)
        {
            var entities = _useCase.GetLocationToolAssignmentsByIds(request.Values.Select(x => new LocationToolAssignmentId(x.Value)).ToList());
            var listOfEntities = new ListOfLocationToolAssignments();
            entities.ForEach(s => listOfEntities.Values.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(InsertLocationToolAssignmentsWithHistory))]
        public override Task<ListOfLongs> InsertLocationToolAssignmentsWithHistory(ListOfLocationToolAssignmentDiffs request, ServerCallContext context)
        {
            var results =_useCase.InsertLocationToolAssignment(GetLocationToolAssignmentDiffs(request));
            var ids = new ListOfLongs();
            results.ForEach(x => ids.Values.Add(new Long() { Value = x.ToLong() }));
            return Task.FromResult(ids);
        }

        [Authorize(Policy = nameof(UpdateLocationToolAssignmentsWithHistory))]
        public override Task<NoParams> UpdateLocationToolAssignmentsWithHistory(ListOfLocationToolAssignmentDiffs request, ServerCallContext context)
        {
            _useCase.UpdateLocationToolAssignment(GetLocationToolAssignmentDiffs(request));
            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(AddTestConditions))]
        public override Task<NoParams> AddTestConditions(AddTestConditionsRequest request, ServerCallContext context)
        {
            var assignment = _mapper.DirectPropertyMapping(request.LocationToolAssignment);

            _useCase.AddTestConditions(assignment, new Server.Core.Entities.User() { UserId = new UserId(request.UserId) });
            return Task.FromResult(new NoParams());
        }

        private List<Server.Core.Diffs.LocationToolAssignmentDiff> GetLocationToolAssignmentDiffs(ListOfLocationToolAssignmentDiffs diffs)
        {
            var result = new List<Server.Core.Diffs.LocationToolAssignmentDiff>();
            foreach (var diff in diffs.Diffs)
            {
                var user = new Server.Core.Entities.User() { UserId = new UserId(diff.UserId) };
                result.Add(new Server.Core.Diffs.LocationToolAssignmentDiff(user,
                    GetLocationToolAssignmentFromLocationToolAssignmentDto(diff.OldLocationToolAssignment),
                    GetLocationToolAssignmentFromLocationToolAssignmentDto(diff.NewLocationToolAssignment), new HistoryComment(diff.Comment?.Value)));
            }

            return result;
        }

        private Server.Core.Entities.LocationToolAssignment GetLocationToolAssignmentFromLocationToolAssignmentDto(DtoTypes.LocationToolAssignment entityDto)
        {
            if (entityDto == null)
            {
                return null;
            }

            return _mapper.DirectPropertyMapping(entityDto);
        }
    }
}
