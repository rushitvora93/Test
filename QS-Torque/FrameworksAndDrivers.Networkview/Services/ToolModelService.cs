using System.Collections.Generic;
using System.Threading.Tasks;
using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using ToolModelService;
using ToolModelDiff = Server.Core.Diffs.ToolModelDiff;
using User = Server.Core.Entities.User;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class ToolModelService : global::ToolModelService.ToolModels.ToolModelsBase
    {
        private readonly Mapper _mapper = new Mapper();
        public ToolModelService(IToolModelUseCase useCase)
        {
            _useCase = useCase;
        }

        [Authorize(Policy = nameof(GetAllToolModels))]
        public override Task<ListOfToolModel> GetAllToolModels(NoParams request, ServerCallContext context)
        {
            var mapper = new Mapper();
            var toolModels = _useCase.GetAllToolModels();
            var toolModelDtos = new ListOfToolModel();
            foreach (var toolModel in toolModels)
            {
                toolModelDtos.ToolModels.Add(mapper.DirectPropertyMapping(toolModel));
            }
            return Task.FromResult(toolModelDtos);
        }

        [Authorize(Policy = nameof(GetAllDeletedToolModels))]
        public override Task<ListOfToolModel> GetAllDeletedToolModels(NoParams request, ServerCallContext context)
        {
            var mapper = new Mapper();
            var toolModels = _useCase.GetAllDeletedToolModels();
            var toolModelDtos = new ListOfToolModel();
            foreach (var toolModel in toolModels)
            {
                toolModelDtos.ToolModels.Add(mapper.DirectPropertyMapping(toolModel));
            }
            return Task.FromResult(toolModelDtos);
        }

        [Authorize(Policy = nameof(InsertToolModel))]
        public override Task<ListOfToolModel> InsertToolModel(ListOfToolModelDiff request, ServerCallContext context)
        {
            var mapper = new Mapper();
            var convertedToolModels = new List<ToolModelDiff>();
            foreach (var diff in request.ToolModelDiffs)
            {
                var convertedToolModel =
                    new ToolModelDiff(
                        new User { UserId = new UserId(diff.UserId) },
                        new HistoryComment(diff.Comment),
                        diff.OldToolModel == null ? null : mapper.DirectPropertyMapping(diff.OldToolModel),
                        mapper.DirectPropertyMapping(diff.NewToolModel));
                convertedToolModels.Add(convertedToolModel);
            }
            var useCaseResult = _useCase.InsertToolModels(convertedToolModels);
            var resultList = new ListOfToolModel();
            useCaseResult.ForEach(toolModel => resultList.ToolModels.Add(mapper.DirectPropertyMapping(toolModel)));
            return Task.FromResult(resultList);
        }

        [Authorize(Policy = nameof(UpdateToolModel))]
        public override Task<ListOfToolModel> UpdateToolModel(ListOfToolModelDiff request, ServerCallContext context)
        {
            var mapper = new Mapper();
            var convertedToolModels = new List<ToolModelDiff>();
            foreach (var diff in request.ToolModelDiffs)
            {
                convertedToolModels.Add(
                    new ToolModelDiff(
                    new User { UserId = new UserId(diff.UserId) },
                    new HistoryComment(diff.Comment),
                    mapper.DirectPropertyMapping(diff.OldToolModel),
                    mapper.DirectPropertyMapping(diff.NewToolModel)));
            }
            var useCaseResult = _useCase.UpdateToolModels(convertedToolModels);
            var resultList = new ListOfToolModel();
            useCaseResult.ForEach(toolModel => resultList.ToolModels.Add(mapper.DirectPropertyMapping(toolModel)));
            return Task.FromResult(resultList);
        }

        [Authorize(Policy = nameof(GetReferencedToolLinks))]
        public override Task<ListOfToolReferenceLink> GetReferencedToolLinks(Long request, ServerCallContext context)
        {
            var useCaseResult = _useCase.GetReferencedToolLinks(new ToolModelId(request.Value));

            var result = new ListOfToolReferenceLink();
            useCaseResult.ForEach(toolReferenceLink =>
            {
                result.ToolReferenceLinks.Add(
                    new ToolReferenceLink
                    {
                        Id = toolReferenceLink.Id.ToLong(),
                        InventoryNumber = toolReferenceLink.InventoryNumber,
                        SerialNumber = toolReferenceLink.SerialNumber
                    });
            });
            return Task.FromResult(result);
        }

        [Authorize(Policy = nameof(LoadDeletedToolModels))]
        public override Task<ListOfToolModel> LoadDeletedToolModels(NoParams request, ServerCallContext context)
        {
            var toolModels = _useCase.LoadDeletedToolModels();
            var listOfToolModels = new ListOfToolModel();
            toolModels.ForEach(s => listOfToolModels.ToolModels.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfToolModels);
        }
        private readonly IToolModelUseCase _useCase;
    }
}
