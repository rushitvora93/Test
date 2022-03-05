using System.Collections.Generic;
using System.Threading.Tasks;
using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.Core.Enums;
using Server.UseCases.UseCases;
using TransferToTestEquipmentService;
using ClassicChkTest = Server.Core.Entities.ClassicChkTest;
using ClassicMfuTest = Server.Core.Entities.ClassicMfuTest;
using DateTime = System.DateTime;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class TransferToTestEquipmentService : global::TransferToTestEquipmentService.TransferToTestEquipmentService.TransferToTestEquipmentServiceBase
    {
        private readonly ILogger<TransferToTestEquipmentService> _logger;
        private readonly ITransferToTestEquipmentUseCase _transferToTestEquipmentUseCase;
        private readonly Mapper _mapper = new Mapper();

        public TransferToTestEquipmentService(ILogger<TransferToTestEquipmentService> logger, ITransferToTestEquipmentUseCase transferToTestEquipmentUseCase)
        {
            _logger = logger;
            _transferToTestEquipmentUseCase = transferToTestEquipmentUseCase;
        }

        [Authorize(Policy = nameof(LoadLocationToolAssignmentsForTransfer))]
        public override Task<ListOfLocationToolAssignmentForTransfer> LoadLocationToolAssignmentsForTransfer(Long request, ServerCallContext context)
        {
            var entities = _transferToTestEquipmentUseCase.LoadLocationToolAssignmentsForTransfer((TestType)request.Value);
            var listOfEntities = new ListOfLocationToolAssignmentForTransfer();
            entities.ForEach(s => listOfEntities.Values.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }

        [Authorize(Policy = nameof(InsertClassicChkTests))]
        public override Task<NoParams> InsertClassicChkTests(ListOfClassicChkTestWithLocalTimestamp request, ServerCallContext context)
        {
            var assigner = new Assigner();
            var tests = new Dictionary<Server.Core.Entities.ClassicChkTest, DateTime>();
            foreach (var classicTest in request.ClassicChkTests)
            {
                DateTime localDateTime = new DateTime();
                assigner.Assign(v => localDateTime = v, classicTest.LocalTimestamp);
                tests.Add(_mapper.DirectPropertyMapping(classicTest.ClassicChkTest), localDateTime);
            }

            _transferToTestEquipmentUseCase.InsertClassicChkTests(tests);

            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(InsertClassicMfuTests))]
        public override Task<NoParams> InsertClassicMfuTests(ListOfClassicMfuTestWithLocalTimestamp request, ServerCallContext context)
        {
            var assigner = new Assigner();
            var tests = new Dictionary<Server.Core.Entities.ClassicMfuTest, DateTime>();
            foreach (var classicTest in request.ClassicMfuTests)
            {
                DateTime localDateTime = new DateTime();
                assigner.Assign(v => localDateTime = v, classicTest.LocalTimestamp);
                tests.Add(_mapper.DirectPropertyMapping(classicTest.ClassicMfuTest), localDateTime);
            }

            _transferToTestEquipmentUseCase.InsertClassicMfuTests(tests);

            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(LoadProcessControlDataForTransfer))]
        public override Task<ListOfProcessControlDataForTransfer> LoadProcessControlDataForTransfer(NoParams request, ServerCallContext context)
        {
            var entities = _transferToTestEquipmentUseCase.LoadProcessControlDataForTransfer();
            var listOfEntities = new ListOfProcessControlDataForTransfer();
            entities.ForEach(s => listOfEntities.Values.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfEntities);
        }
    }
}
