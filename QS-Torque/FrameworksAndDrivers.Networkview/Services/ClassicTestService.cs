using System.Collections.Generic;
using System.Threading.Tasks;
using BasicTypes;
using ClassicTestService;
using DtoTypes;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.Core.Entities;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class ClassicTestService : global::ClassicTestService.ClassicTestService.ClassicTestServiceBase
    {
        private readonly ILogger<ClassicTestService> _logger;
        private readonly IClassicTestUseCase _classicTestUseCase;
        private readonly Mapper _mapper = new Mapper();

        public ClassicTestService(ILogger<ClassicTestService> logger, IClassicTestUseCase classicTestUseCase)
        {
            _logger = logger;
            _classicTestUseCase = classicTestUseCase;
        }

        [Authorize(Policy = nameof(GetClassicChkHeaderFromTool))]
        public override Task<ListOfClassicChkTest> GetClassicChkHeaderFromTool(GetClassicHeaderFromToolRequest request, ServerCallContext context)
        {
            long? locationId = null;
            if (request.LocationId != null && !request.LocationId.IsNull)
            {
                locationId = request.LocationId.Value;
            }

            var entities = _classicTestUseCase.GetClassicChkHeaderFromTool(request.PowToolId, locationId);
            var listOfEntities = new ListOfClassicChkTest();
            entities.ForEach(s => listOfEntities.ClassicChkTests.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(GetClassicMfuHeaderFromTool))]
        public override Task<ListOfClassicMfuTest> GetClassicMfuHeaderFromTool(GetClassicHeaderFromToolRequest request, ServerCallContext context)
        {
            long? locationId = null;
            if (request.LocationId != null && !request.LocationId.IsNull)
            {
                locationId = request.LocationId.Value;
            }

            var entities = _classicTestUseCase.GetClassicMfuHeaderFromTool(request.PowToolId, locationId);
            var listOfEntities = new ListOfClassicMfuTest();
            entities.ForEach(s => listOfEntities.ClassicMfuTests.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(GetToolsFromLocationTests))]
        public override Task<ListOfClassicTestPowToolData> GetToolsFromLocationTests(Long request, ServerCallContext context)
        {
            var results = _classicTestUseCase.GetToolsFromLocationTests(new LocationId(request.Value));
            var assigner = new Assigner();

            var listOfEntities = new ListOfClassicTestPowToolData();
            foreach (var result in results)
            {
                var data = new ClassicTestPowToolData()
                {
                    Tool = _mapper.DirectPropertyMapping(result.Key),
                    IsToolAssignmentActive = result.Value.isToolAssignmentActive
                };
                assigner.Assign((value) => { data.FirstTest = value;}, result.Value.firsttest);
                assigner.Assign((value) => { data.LastTest = value; }, result.Value.lasttest);
                listOfEntities.ClassicTestPowToolDatas.Add(data);
            }
            
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(GetValuesFromClassicChkHeader))]
        public override Task<ListOfClassicChkTestValue> GetValuesFromClassicChkHeader(ListOfLongs request, ServerCallContext context)
        {
            var ids = new List<GlobalHistoryId>();
            foreach (var id in request.Values)
            {
                ids.Add(new GlobalHistoryId(id.Value));
            }
            var entities = _classicTestUseCase.GetValuesFromClassicChkHeader(ids);
            var listOfEntities = new ListOfClassicChkTestValue();
            entities.ForEach(s => listOfEntities.ClassicChkTestValues.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(GetValuesFromClassicMfuHeader))]
        public override Task<ListOfClassicMfuTestValue> GetValuesFromClassicMfuHeader(ListOfLongs request, ServerCallContext context)
        {
            var ids = new List<GlobalHistoryId>();
            foreach (var id in request.Values)
            {
                ids.Add(new GlobalHistoryId(id.Value));
            }
            var entities = _classicTestUseCase.GetValuesFromClassicMfuHeader(ids);
            var listOfEntities = new ListOfClassicMfuTestValue();
            entities.ForEach(s => listOfEntities.ClassicMfuTestValues.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(GetClassicProcessHeaderFromLocation))]
        public override Task<ListOfClassicProcessTest> GetClassicProcessHeaderFromLocation(Long request, ServerCallContext context)
        {
            var entities = _classicTestUseCase.GetClassicProcessHeaderFromLocation(new LocationId(request.Value));
            var listOfEntities = new ListOfClassicProcessTest();
            entities.ForEach(s => listOfEntities.ClassicProcessTests.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(GetClassicProcessHeaderFromLocation))]
        public override Task<ListOfClassicProcessTestValue> GetValuesFromClassicProcessHeader(ListOfLongs request, ServerCallContext context)
        {
            var ids = new List<GlobalHistoryId>();
            foreach (var id in request.Values)
            {
                ids.Add(new GlobalHistoryId(id.Value));
            }
            var entities = _classicTestUseCase.GetValuesFromClassicProcessHeader(ids);
            var listOfEntities = new ListOfClassicProcessTestValue();
            entities.ForEach(s => listOfEntities.ClassicProcessTestValues.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }
    }
}
