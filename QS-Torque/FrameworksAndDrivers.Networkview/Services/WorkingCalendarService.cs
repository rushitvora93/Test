using System.Threading.Tasks;
using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using WorkingCalendarService;
using WorkingCalendar = DtoTypes.WorkingCalendar;
using WorkingCalendarEntry = DtoTypes.WorkingCalendarEntry;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class WorkingCalendarService : global::WorkingCalendarService.WorkingCalendars.WorkingCalendarsBase
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IWorkingCalendarUseCase _useCase;

        public WorkingCalendarService(ILogger<AuthenticationService> logger, IWorkingCalendarUseCase useCase)
        {
            _logger = logger;
            _useCase = useCase;
        }

        [Authorize(Policy = nameof(GetWorkingCalendar))]
        public override Task<WorkingCalendar> GetWorkingCalendar(NoParams request, ServerCallContext context)
        {
            var entity = _useCase.GetWorkingCalendar();

            return Task.FromResult(new WorkingCalendar()
            {
                Id = entity.Id.ToLong(),
                AreSaturdaysFree = entity.AreSaturdaysFree,
                AreSundaysFree = entity.AreSundaysFree
            });
        }

        [Authorize(Policy = nameof(GetWorkingCalendarEntriesForWorkingCalendarId))]
        public override Task<ListOfWorkingCalendarEntries> GetWorkingCalendarEntriesForWorkingCalendarId(LongRequest request, ServerCallContext context)
        {
            var mapper = new Mapper();
            var entities = _useCase.GetWorkingCalendarEntriesForWorkingCalendarId(new WorkingCalendarId(request.Value));
            var dtos = new ListOfWorkingCalendarEntries();

            foreach (var entity in entities)
            {
                dtos.WorkingCalendarEntries.Add(mapper.DirectPropertyMapping(entity));
            }

            return Task.FromResult(dtos);
        }

        [Authorize(Policy = nameof(InsertWorkingCalendarEntry))]
        public override Task<NoParams> InsertWorkingCalendarEntry(InsertWorkingCalendarEntryParameter request, ServerCallContext context)
        {
            var mapper = new Mapper();
            _useCase.InsertWorkingCalendarEntry(mapper.DirectPropertyMapping(request.Entry), new WorkingCalendarId(request.CalendarId));
            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(DeleteWorkingCalendarEntry))]
        public override Task<NoParams> DeleteWorkingCalendarEntry(WorkingCalendarEntry request, ServerCallContext context)
        {
            var mapper = new Mapper();
            _useCase.DeleteWorkingCalendarEntry(mapper.DirectPropertyMapping(request));
            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(SaveWorkingCalendar))]
        public override Task<NoParams> SaveWorkingCalendar(WorkingCalendarDiff request, ServerCallContext context)
        {
            _useCase.SaveWorkingCalendar(new Server.Core.Diffs.WorkingCalendarDiff(
                new Server.Core.Entities.WorkingCalendar()
                {
                    Id = new WorkingCalendarId(request.Old.Id),
                    AreSaturdaysFree = request.Old.AreSaturdaysFree,
                    AreSundaysFree = request.Old.AreSundaysFree
                },
                new Server.Core.Entities.WorkingCalendar()
                {
                    Id = new WorkingCalendarId(request.New.Id),
                    AreSaturdaysFree = request.New.AreSaturdaysFree,
                    AreSundaysFree = request.New.AreSundaysFree
                },
                new Server.Core.Entities.User() { UserId = new UserId(request.UserId) },
                new HistoryComment(request.Comment)));
            return Task.FromResult(new NoParams());
        }
    }
}
