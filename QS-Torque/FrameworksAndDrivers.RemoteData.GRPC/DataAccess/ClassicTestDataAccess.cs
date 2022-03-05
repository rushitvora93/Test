using System.Collections.Generic;
using BasicTypes;
using ClassicTestService;
using Client.Core.Entities;
using Core.Entities;
using Core.UseCases;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using ClassicChkTest = Core.Entities.ClassicChkTest;
using ClassicMfuTest = Core.Entities.ClassicMfuTest;
using ClassicProcessTest = Client.Core.Entities.ClassicProcessTest;
using DateTime = System.DateTime;
using Tool = Core.Entities.Tool;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface IClassicTestClient
    {
        ListOfClassicTestPowToolData GetToolsFromLocationTests(Long locationId);
        ListOfClassicMfuTest GetClassicMfuHeaderFromTool(GetClassicHeaderFromToolRequest request);
        ListOfClassicChkTest GetClassicChkHeaderFromTool(GetClassicHeaderFromToolRequest request);
        ListOfClassicChkTestValue GetValuesFromClassicChkHeader(ListOfLongs globalHistoryIds);
        ListOfClassicMfuTestValue GetValuesFromClassicMfuHeader(ListOfLongs globalHistoryIds);
        ListOfClassicProcessTest GetClassicProcessHeaderFromLocation(Long locationId);
        ListOfClassicProcessTestValue GetValuesFromClassicProcessHeader(ListOfLongs globalHistoryIds);
    }

    public class ClassicTestDataAccess : IClassicTestData
    {
        private readonly IClientFactory _clientFactory;
        private readonly ITimeDataAccess _timeDataAccess;
        private readonly Mapper _mapper = new Mapper();

        public ClassicTestDataAccess(IClientFactory clientFactory, ITimeDataAccess timeDataAccess)
        {
            _clientFactory = clientFactory;
            _timeDataAccess = timeDataAccess;
        }

        private IClassicTestClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetClassicTestClient();
        }

        public Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)> LoadToolsFromLocationTests(LocationId locationId)
        {
            var powToolDatas = GetClient().GetToolsFromLocationTests(new Long() { Value = locationId.ToLong() });

            var list = new Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)>();
            foreach (var data in powToolDatas.ClassicTestPowToolDatas)
            {
                _timeDataAccess.ConvertToLocalDate(data);

                var tool = _mapper.DirectPropertyMapping(data.Tool);

                var assigner = new Assigner();
                DateTime? firstTest = null, lastTest = null;
                assigner.Assign((value) => { firstTest = value;}, data.FirstTest);
                assigner.Assign((value) => { lastTest = value; }, data.LastTest);

                list.Add(tool, (firsttest: firstTest, lasttest: lastTest, data.IsToolAssignmentActive));
            }

            return list;
        }

        public List<ClassicMfuTest> GetClassicMfuHeaderFromTool(ToolId toolId, LocationId locationId)
        {
            var request = new GetClassicHeaderFromToolRequest()
            {
                PowToolId = toolId.ToLong(),
                LocationId = new NullableLong() { IsNull = true}
            };

            if (locationId != null)
            {
                request.LocationId.IsNull = false;
                request.LocationId.Value = locationId.ToLong();
            }

            var headerDtos = GetClient().GetClassicMfuHeaderFromTool(request);


            var list = new List<ClassicMfuTest>();
            foreach (var dto in headerDtos.ClassicMfuTests)
            {
                _timeDataAccess.ConvertToLocalDate(dto);               
                list.Add(_mapper.DirectPropertyMapping(dto));
            }

            return list;
        }

        public List<ClassicChkTest> GetClassicChkHeaderFromTool(ToolId toolId, LocationId locationId)
        {
            var request = new GetClassicHeaderFromToolRequest()
            {
                PowToolId = toolId.ToLong(),
                LocationId = new NullableLong() { IsNull = true }
            };

            if (locationId != null)
            {
                request.LocationId.IsNull = false;
                request.LocationId.Value = locationId.ToLong();
            }

            var headerDtos = GetClient().GetClassicChkHeaderFromTool(request);


            var list = new List<ClassicChkTest>();
            foreach (var dto in headerDtos.ClassicChkTests)
            {
                _timeDataAccess.ConvertToLocalDate(dto);
                list.Add(_mapper.DirectPropertyMapping(dto));
            }

            return list;
        }

        public List<Core.Entities.ClassicChkTestValue> GetValuesFromClassicChkHeader(List<ClassicChkTest> header)
        {
            var globalHistoryIds = new ListOfLongs();
            foreach (var item in header)
            {
                globalHistoryIds.Values.Add(new Long {Value = item.Id.ToLong()});
            }

            var dtoValues = GetClient().GetValuesFromClassicChkHeader(globalHistoryIds);

            var list = new List<Core.Entities.ClassicChkTestValue>();
            foreach (var dto in dtoValues.ClassicChkTestValues)
            { 
                list.Add(_mapper.DirectPropertyMapping(dto));
            }

            return list;
        }

        public List<Core.Entities.ClassicMfuTestValue> GetValuesFromClassicMfuHeader(List<ClassicMfuTest> header)
        {
            var globalHistoryIds = new ListOfLongs();
            foreach (var item in header)
            {
                globalHistoryIds.Values.Add(new Long { Value = item.Id.ToLong() });
            }

            var dtoValues = GetClient().GetValuesFromClassicMfuHeader(globalHistoryIds);

            var list = new List<Core.Entities.ClassicMfuTestValue>();
            foreach (var dto in dtoValues.ClassicMfuTestValues)
            {
                list.Add(_mapper.DirectPropertyMapping(dto));
            }

            return list;
        }

        public List<Client.Core.Entities.ClassicProcessTest> GetClassicProcessHeaderFromLocation(LocationId locationId)
        {
            var headerDtos = GetClient().GetClassicProcessHeaderFromLocation(new Long(){Value = locationId.ToLong()});

            var list = new List<Client.Core.Entities.ClassicProcessTest>();
            foreach (var dto in headerDtos.ClassicProcessTests)
            {
                var entity = _mapper.DirectPropertyMapping(dto);
                entity.Timestamp = _timeDataAccess.ConvertToLocalDate(entity.Timestamp);
                list.Add(entity);
            }
            return list;
        }

        public List<Client.Core.Entities.ClassicProcessTestValue> GetValuesFromClassicProcessHeader(List<ClassicProcessTest> tests)
        {
            var globalHistoryIds = new ListOfLongs();
            foreach (var item in tests)
            {
                globalHistoryIds.Values.Add(new Long { Value = item.Id.ToLong() });
            }

            var dtoValues = GetClient().GetValuesFromClassicProcessHeader(globalHistoryIds);

            var list = new List<Client.Core.Entities.ClassicProcessTestValue>();
            foreach (var dto in dtoValues.ClassicProcessTestValues)
            {
                list.Add(_mapper.DirectPropertyMapping(dto));
            }

            return list;
        }
    }
}