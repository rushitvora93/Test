using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Core;
using Core.Entities;
using Core.UseCases;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using ToolService;
using LocationToolAssignmentReferenceLink = Core.Entities.ReferenceLink.LocationToolAssignmentReferenceLink;
using Picture = Core.Entities.Picture;
using String = BasicTypes.String;
using Tool = DtoTypes.Tool;
using ToolDiff = Core.UseCases.ToolDiff;
using ToolModel = Core.Entities.ToolModel;
using User = Core.Entities.User;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface IToolClient
    {
        ListOfTools LoadTools(LoadToolsRequest request);
        Tool GetToolById(Long toolId);
        ListOfTools InsertToolsWithHistory(InsertToolsWithHistoryRequest request);
        ListOfTools UpdateToolsWithHistory(UpdateToolsWithHistoryRequest request);
        ListOfLocationToolAssignmentReferenceLink GetLocationToolAssignmentLinkForTool(Long locationToolAssignmentId);
        Bool IsSerialNumberUnique(String serialNumber);
        Bool IsInventoryNumberUnique(String inventoryNumber);
        String GetToolComment(Long toolId);
        LoadPictureForToolResponse LoadPictureForTool(LoadPictureForToolRequest request);
        ListOfTools LoadToolsForModel(Long modelId);
        ListOfToolModel LoadModelsWithAtLeasOneTool();
    }

    public class ToolDataAccess: IToolData
    {
        private readonly IClientFactory _clientFactory;
        private readonly ILocationToolAssignmentDisplayFormatter _locationToolAssignmentDisplayFormatter;
        private readonly IPictureFromZipLoader _pictureFromZipLoader;
        private readonly Mapper _mapper = new Mapper();

        public ToolDataAccess(IClientFactory clientFactory, 
            ILocationToolAssignmentDisplayFormatter locationToolAssignmentDisplayFormatter,
            IPictureFromZipLoader pictureFromZipLoader)
        {
            _clientFactory = clientFactory;
            _locationToolAssignmentDisplayFormatter = locationToolAssignmentDisplayFormatter;
            _pictureFromZipLoader = pictureFromZipLoader;
        }

        private IToolClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetToolClient();
        }

        public Core.Entities.Tool AddTool(Core.Entities.Tool newTool, User byUser)
        {
            if (byUser == null)
            {
                throw new ArgumentException("User should not be null");
            }

            var request = new InsertToolsWithHistoryRequest()
            {
                ToolDiffs = new ListOfToolDiffs()
                {
                    ToolDiffs =
                    {
                        new DtoTypes.ToolDiff()
                        {
                            UserId = byUser.UserId.ToLong(),
                            Comment = "",
                            NewTool = _mapper.DirectPropertyMapping(newTool)
                        }
                    }
                },
                ReturnList = true
            };

            var result = GetClient().InsertToolsWithHistory(request);

            if (result?.Tools.FirstOrDefault() == null)
            {
                throw new NullReferenceException("Server returned null when Adding a Tool");
            }
            newTool.Id = new ToolId(result.Tools.First().Id);
            return newTool;
        }

        public IEnumerable<List<Core.Entities.Tool>> LoadTools()
        {
            throw new System.NotImplementedException();
        }

        public string LoadComment(Core.Entities.Tool tool)
        {
            var stringResponse = GetClient().GetToolComment(new Long()
            {
                Value = tool.Id.ToLong()
            });

            return stringResponse.Value;
        }

        public Picture LoadPictureForTool(Core.Entities.Tool tool)
        {
            var toolId = tool?.Id;
            if (toolId is null)
            {
                throw new ArgumentNullException(nameof(toolId), "ToolId should not be null");
            }

            const int pictureFileType = 0;

            var response = GetClient().LoadPictureForTool(new LoadPictureForToolRequest()
            {
                ToolId = toolId.ToLong(),
                FileType = pictureFileType
            });

            if (response.Picture == null)
            {
                return null;
            }

            var picture = _pictureFromZipLoader.LoadPictureFromZipBytes(response.Picture.Image.ToByteArray());
            if (picture != null)
            {
                var mapper = new Mapper();
                mapper.DirectPropertyMapping(response.Picture, picture);
            }

            return picture;
        }

        public List<ToolModel> LoadModelsWithAtLeasOneTool()
        {
            var dtos = GetClient().LoadModelsWithAtLeasOneTool();

            var tools = new List<ToolModel>();

            foreach (var model in dtos.ToolModels)
            {
                tools.Add(_mapper.DirectPropertyMapping(model));
            }

            return tools;
        }

        public List<Core.Entities.Tool> LoadToolsForModel(ToolModel toolModel)
        {
            var tools = new List<Core.Entities.Tool>();
            var toolDtos = GetClient().LoadToolsForModel(new Long() {Value = toolModel.Id.ToLong()});
            foreach (var tool in toolDtos.Tools)
            {
                tools.Add(_mapper.DirectPropertyMapping(tool));
            }
            return tools;
        }

        public bool IsSerialNumberUnique(string serialNumber)
        {
            return GetClient().IsSerialNumberUnique(new String() { Value = serialNumber }).Value;
        }

        public bool IsInventoryNumberUnique(string inventoryNumber)
        {
            return GetClient().IsInventoryNumberUnique(new String() { Value = inventoryNumber }).Value;
        }

        public void RemoveTool(Core.Entities.Tool tool, User byUser)
        {
            if (byUser == null)
            {
                throw new ArgumentException("User should not be null");
            }

            var oldTool = _mapper.DirectPropertyMapping(tool);
            var newTool = _mapper.DirectPropertyMapping(tool);
            oldTool.Alive = true;
            newTool.Alive = false;

            var request = new UpdateToolsWithHistoryRequest()
            {
                ToolDiffs = new ListOfToolDiffs()
                {
                    ToolDiffs =
                    {
                        new List<DtoTypes.ToolDiff>()
                        {
                            new DtoTypes.ToolDiff()
                            {
                                UserId = byUser.UserId.ToLong(),
                                Comment = "",
                                OldTool = oldTool,
                                NewTool = newTool
                            }
                        }
                    }
                }
            };

            GetClient().UpdateToolsWithHistory(request);
        }

        public Core.Entities.Tool UpdateTool(ToolDiff diff)
        {
            if (!diff.OldTool?.EqualsById(diff.NewTool) ?? false)
            {
                throw new ArgumentException("Mismatching ToolIds");
            }

            var oldTool = _mapper.DirectPropertyMapping(diff.OldTool);
            var newTool = _mapper.DirectPropertyMapping(diff.NewTool);
            oldTool.Alive = true;
            newTool.Alive = true;

            var request = new UpdateToolsWithHistoryRequest()
            {
                ToolDiffs = new ListOfToolDiffs()
                {
                    ToolDiffs =
                    {
                        new List<DtoTypes.ToolDiff>()
                        {
                            new DtoTypes.ToolDiff()
                            {
                                UserId = diff.User.UserId.ToLong(),
                                Comment = diff.Comment?.ToDefaultString() == null ? "" : diff.Comment.ToDefaultString(),
                                OldTool = oldTool,
                                NewTool = newTool
                            }
                        }
                    }
                }
            };

            GetClient().UpdateToolsWithHistory(request);
            return diff.NewTool;
        }

        public List<LocationToolAssignmentReferenceLink> LoadLocationToolAssignmentLinksForToolId(ToolId toolId)
        {
            var locationToolLinks = GetClient().GetLocationToolAssignmentLinkForTool(new Long()
            {
                Value = toolId.ToLong()
            });

            var modelLinks = new List<LocationToolAssignmentReferenceLink>();

            foreach (var locToolLink in locationToolLinks.Links)
            {
                modelLinks.Add(new LocationToolAssignmentReferenceLink(
                    new QstIdentifier(locToolLink.Id),
                    new LocationDescription(locToolLink.LocationName),
                    new LocationNumber(locToolLink.LocationNumber),
                    locToolLink.ToolSerialNumber,
                    locToolLink.ToolInventoryNumber,
                    _locationToolAssignmentDisplayFormatter,
                    new LocationId(locToolLink.LocationId),
                    new ToolId(locToolLink.ToolId)));
            }
            return modelLinks;
        }
    }
}

