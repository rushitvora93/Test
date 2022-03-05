using System;
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
using ToleranceClassService;
using DateTime = System.DateTime;
using ToleranceClass = Server.Core.Entities.ToleranceClass;
using ToleranceClassDiff = Server.Core.Diffs.ToleranceClassDiff;
using User = Server.Core.Entities.User;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class ToleranceClassService : global::ToleranceClassService.ToleranceClasses.ToleranceClassesBase
    {
        private readonly ILogger<ToleranceClassService> _logger;
        private readonly IToleranceClassUseCase _toleranceClassUseCase;
        private readonly Mapper _mapper = new Mapper();

        public ToleranceClassService(ILogger<ToleranceClassService> logger, IToleranceClassUseCase toleranceClassUseCase)
        {
            _logger = logger;
            _toleranceClassUseCase = toleranceClassUseCase;
        }

        [Authorize(Policy = nameof(LoadToleranceClasses))]
        public override Task<ListOfToleranceClasses> LoadToleranceClasses(NoParams request, ServerCallContext context)
        {
            var toleranceClasses = _toleranceClassUseCase.LoadToleranceClasses();
            var listOfToleranceClasses = new ListOfToleranceClasses();
            toleranceClasses.ForEach(s => listOfToleranceClasses.ToleranceClasses.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfToleranceClasses);
        }

        [Authorize(Policy = nameof(GetToleranceClassLocationLinks))]
        public override Task<ListOfLocationLink> GetToleranceClassLocationLinks(LongRequest request, ServerCallContext context)
        {
            var locationLinks = _toleranceClassUseCase.GetToleranceClassLocationLinks(new ToleranceClassId(request.Value));
            var listOfLocationLink = new ListOfLocationLink();
            locationLinks.ForEach(s => listOfLocationLink.LocationLinks.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfLocationLink);
        }

        [Authorize(Policy = nameof(InsertToleranceClassesWithHistory))]
        public override Task<ListOfToleranceClasses> InsertToleranceClassesWithHistory(InsertToleranceClassesWithHistoryRequest request, ServerCallContext context)
        {
            var toleranceClasses = _toleranceClassUseCase.InsertToleranceClassesWithHistory(GetToleranceClassDiffs(request.ToleranceClassesDiffs), request.ReturnList);
            return Task.FromResult(GetListOfToleranceClassFromToleranceClass(toleranceClasses));
        }

        [Authorize(Policy = nameof(UpdateToleranceClassesWithHistory))]
        public override Task<ListOfToleranceClasses> UpdateToleranceClassesWithHistory(UpdateToleranceClassesWithHistoryRequest request, ServerCallContext context)
        {
            var toleranceClasses = _toleranceClassUseCase.UpdateToleranceClassesWithHistory(GetToleranceClassDiffs(request.ToleranceClassesDiffs));
            return Task.FromResult(GetListOfToleranceClassFromToleranceClass(toleranceClasses));
        }

        [Authorize(Policy = nameof(GetToleranceClassLocationToolAssignmentLinks))]
        public override Task<ListOfLongs> GetToleranceClassLocationToolAssignmentLinks(LongRequest request, ServerCallContext context)
        {
            var references = _toleranceClassUseCase.GetToleranceClassLocationToolAssignmentLinks(new ToleranceClassId(request.Value));
            var listOfLongs = new ListOfLongs();
            references.ForEach(r => listOfLongs.Values.Add(new Long() {Value = r.ToLong()}));
            return Task.FromResult(listOfLongs);
        }

        [Authorize(Policy = nameof(GetToleranceClassesFromHistoryForIds))]
        public override Task<GetToleranceClassesFromHistoryForIdsResponse> GetToleranceClassesFromHistoryForIds(ListOfToleranceClassFromHistoryParameter request, ServerCallContext context)
        {
            var idsWithClassDatas = new List<Tuple<long, long, DateTime>>();
            foreach (var param in request.Parameters)
            {
                idsWithClassDatas.Add(new Tuple<long, long, DateTime>(
                    param.GlobalHistoryId, 
                    param.ToleranceClassId,
                    new DateTime(param.Timestamp.Ticks)));
            }
            var historyToleranceClasses = _toleranceClassUseCase.GetToleranceClassesFromHistoryForIds(idsWithClassDatas);
            return Task.FromResult(GetToleranceClassesFromHistory(historyToleranceClasses));
        }

        private GetToleranceClassesFromHistoryForIdsResponse GetToleranceClassesFromHistory(Dictionary<long, ToleranceClass> datas)
        {
            var result = new GetToleranceClassesFromHistoryForIdsResponse();
            foreach (var data in datas)
            {
                result.Datas.Add(new GlobalHistoryIdToleranceClassPair()
                {
                    GlobalHistoryId = data.Key,
                    ToleranceClass = _mapper.DirectPropertyMapping(data.Value)
                });
            }

            return result;
        }

        private ListOfToleranceClasses GetListOfToleranceClassFromToleranceClass(List<ToleranceClass> toleranceClasses)
        {
            var listOfToleranceClasses = new ListOfToleranceClasses();
            foreach (var toleranceClass in toleranceClasses)
            {
                listOfToleranceClasses.ToleranceClasses.Add(_mapper.DirectPropertyMapping(toleranceClass));
            }

            return listOfToleranceClasses;
        }

        private List<ToleranceClassDiff> GetToleranceClassDiffs(ListOfToleranceClassesDiffs toleranceClassesDiffs)
        {
            var classDiffs = new List<ToleranceClassDiff>();

            foreach (var diff in toleranceClassesDiffs.ToleranceClassesDiff)
            {
                var user = new User() { UserId = new UserId(diff.UserId) };
                classDiffs.Add(new Server.Core.Diffs.ToleranceClassDiff(user, new HistoryComment(diff.Comment),
                    GetToleranceClassFromToleranceClassDto(diff.OldToleranceClass),
                    GetToleranceClassFromToleranceClassDto(diff.NewToleranceClass)));
            }

            return classDiffs;
        }

        private ToleranceClass GetToleranceClassFromToleranceClassDto(DtoTypes.ToleranceClass classDto)
        {
            if (classDto == null)
            {
                return null;
            }

            return _mapper.DirectPropertyMapping(classDto);
        }
    }
}
