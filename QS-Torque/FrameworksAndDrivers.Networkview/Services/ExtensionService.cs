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
using ExtensionService;
using DateTime = System.DateTime;
using Extension = Server.Core.Entities.Extension;
using ExtensionDiff = Server.Core.Diffs.ExtensionDiff;
using String = BasicTypes.String;
using User = Server.Core.Entities.User;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class ExtensionService : global::ExtensionService.Extensions.ExtensionsBase
    {
        private readonly ILogger<ExtensionService> _logger;
        private readonly IExtensionUseCase _extensionUseCase;
        private readonly Mapper _mapper = new Mapper();

        public ExtensionService(ILogger<ExtensionService> logger, IExtensionUseCase extensionUseCase)
        {
            _logger = logger;
            _extensionUseCase = extensionUseCase;
        }

        [Authorize(Policy = nameof(LoadExtensions))]
        public override Task<ListOfExtensions> LoadExtensions(NoParams request, ServerCallContext context)
        {
            var extensions = _extensionUseCase.LoadExtensions();
            var listOfExtensions = new ListOfExtensions();
            extensions.ForEach(s => listOfExtensions.Extensions.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfExtensions);
        }

        [Authorize(Policy = nameof(LoadDeletedExtensions))]
        public override Task<ListOfExtensions> LoadDeletedExtensions(NoParams request, ServerCallContext context)
        {
            var extensions = _extensionUseCase.LoadDeletedExtensions();
            var listOfExtensions = new ListOfExtensions();
            extensions.ForEach(s => listOfExtensions.Extensions.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfExtensions);
        }

        [Authorize(Policy = nameof(GetExtensionLocationLinks))]
        public override Task<ListOfLocationLink> GetExtensionLocationLinks(LongRequest request, ServerCallContext context)
        {
            var locationLinks = _extensionUseCase.GetExtensionLocationLinks(new ExtensionId(request.Value));
            var listOfLocationLink = new ListOfLocationLink();
            locationLinks.ForEach(s => listOfLocationLink.LocationLinks.Add(_mapper.DirectPropertyMapping(s)));
            return Task.FromResult(listOfLocationLink);
        }

        [Authorize(Policy = nameof(InsertExtensions))]
        public override Task<ListOfExtensions> InsertExtensions(InsertExtensionsRequest request, ServerCallContext context)
        {
            var extensions = _extensionUseCase.InsertExtensions(GetExtensionDiffs(request.ExtensionsDiffs), request.ReturnList);
            return Task.FromResult(GetListOfExtensionFromExtension(extensions));
        }

        [Authorize(Policy = nameof(UpdateExtensions))]
        public override Task<NoParams> UpdateExtensions(UpdateExtensionsRequest request, ServerCallContext context)
        {
            _extensionUseCase.UpdateExtensions(GetExtensionDiffs(request.ExtensionsDiffs));
            return Task.FromResult(new NoParams());
        }

        [Authorize(Policy = nameof(IsExtensionInventoryNumberUnique))]
        public override Task<Bool> IsExtensionInventoryNumberUnique(String request, ServerCallContext context)
        {
            var result = _extensionUseCase.IsInventoryNumberUnique(new ExtensionInventoryNumber(request.Value));
            return Task.FromResult(new Bool() { Value = result });
        }

        private ListOfExtensions GetListOfExtensionFromExtension(List<Extension> extensiones)
        {
            var listOfExtensions = new ListOfExtensions();
            foreach (var extension in extensiones)
            {
                listOfExtensions.Extensions.Add(_mapper.DirectPropertyMapping(extension));
            }

            return listOfExtensions;
        }

        private List<ExtensionDiff> GetExtensionDiffs(ListOfExtensionsDiffs extensionesDiffs)
        {
            var classDiffs = new List<ExtensionDiff>();

            foreach (var diff in extensionesDiffs.ExtensionsDiff)
            {
                var user = new User() { UserId = new UserId(diff.UserId) };
                classDiffs.Add(new Server.Core.Diffs.ExtensionDiff(user, new HistoryComment(diff.Comment),
                    GetExtensionFromExtensionDto(diff.OldExtension),
                    GetExtensionFromExtensionDto(diff.NewExtension)));
            }

            return classDiffs;
        }

        private Extension GetExtensionFromExtensionDto(DtoTypes.Extension classDto)
        {
            if (classDto == null)
            {
                return null;
            }

            return _mapper.DirectPropertyMapping(classDto);
        }
    }
}
