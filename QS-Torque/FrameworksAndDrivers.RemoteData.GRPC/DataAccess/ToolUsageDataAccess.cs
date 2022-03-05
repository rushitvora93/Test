using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using ToolUsageService;
using ToolReferenceLink = Core.Entities.ReferenceLink.ToolReferenceLink;
using ToolUsage = Core.Entities.ToolUsage;
using User = Core.Entities.User;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface IToolUsageClient
    {
        ListOfToolUsage GetAllToolUsages();
        ListOfLongs GetToolUsageLocationToolAssignmentReferences(Long id);
        ListOfToolUsage InsertToolUsagesWithHistory(InsertToolUsagesWithHistoryRequest request);
        ListOfToolUsage UpdateToolUsagesWithHistory(UpdateToolUsagesWithHistoryRequest request);
    }

    public class ToolUsageDataAccess : IHelperTableData<ToolUsage>
    {
        private readonly IClientFactory _clientFactory;
        public bool HasToolModelAsReference => false;
        public bool HasToolAsReference => false;
        public bool HasLocationToolAssignmentAsReference => true;
        private readonly Mapper _mapper = new Mapper();


        public ToolUsageDataAccess(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private IToolUsageClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetToolUsageClient();
        }

        public List<ToolUsage> LoadItems()
        {
            var allToolUsages = GetClient().GetAllToolUsages();

            var toolUsages = new List<ToolUsage>();
            foreach (var toolUsage in allToolUsages.ToolUsageList)
            {
                toolUsages.Add(_mapper.DirectPropertyMapping(toolUsage));
            }

            return toolUsages;
        }

        public HelperTableEntityId AddItem(ToolUsage item, User byUser)
        {
            if (byUser == null)
            {
                throw new ArgumentException("User should not be null");
            }

            var request = new InsertToolUsagesWithHistoryRequest()
            {
                ToolUsageDiffs = new ListOfToolUsageDiffs()
                {
                    ToolUsageDiffs =
                    {
                        new ToolUsageDiff()
                        {
                            UserId = byUser.UserId.ToLong(),
                            Comment = "",
                            NewToolUsage = _mapper.DirectPropertyMapping(item)
                        }
                    }
                },
                ReturnList = true
            };

            var result = GetClient().InsertToolUsagesWithHistory(request);

            if (result?.ToolUsageList.FirstOrDefault() == null)
            {
                throw new NullReferenceException("Server returned null when Adding a ToolUsage");
            }

            return _mapper.DirectPropertyMapping(result.ToolUsageList.FirstOrDefault()).ListId;
        }

        public void RemoveItem(ToolUsage item, User byUser)
        {
            if (byUser == null)
            {
                throw new ArgumentException("User should not be null");
            }

            var oldToolUsage = _mapper.DirectPropertyMapping(item);
            var newToolUsage = _mapper.DirectPropertyMapping(item);
            oldToolUsage.Alive = true;
            newToolUsage.Alive = false;

            var request = new UpdateToolUsagesWithHistoryRequest()
            {
                ToolUsageDiffs = new ListOfToolUsageDiffs()
                {
                    ToolUsageDiffs =
                    {
                        new List<ToolUsageDiff>()
                        {
                            new ToolUsageDiff()
                            {
                                UserId = byUser.UserId.ToLong(),
                                Comment = "",
                                OldToolUsage = oldToolUsage,
                                NewToolUsage = newToolUsage
                            }
                        }
                    }
                }
            };

            GetClient().UpdateToolUsagesWithHistory(request);
        }

        public void SaveItem(ToolUsage oldItem, ToolUsage changedItem, User byUser)
        {
            if (!oldItem.EqualsById(changedItem))
            {
                throw new ArgumentException("Mismatching ToolUsageIds");
            }

            var oldToolUsage = _mapper.DirectPropertyMapping(oldItem);
            var newToolUsage = _mapper.DirectPropertyMapping(changedItem);
            oldToolUsage.Alive = true;
            newToolUsage.Alive = true;

            var request = new UpdateToolUsagesWithHistoryRequest()
            {
                ToolUsageDiffs = new ListOfToolUsageDiffs()
                {
                    ToolUsageDiffs =
                    {
                        new List<ToolUsageDiff>()
                        {
                            new ToolUsageDiff()
                            {
                                UserId = byUser.UserId.ToLong(),
                                Comment = "",
                                OldToolUsage = oldToolUsage,
                                NewToolUsage = newToolUsage
                            }
                        }
                    }
                }
            };

            GetClient().UpdateToolUsagesWithHistory(request);
        }

        public List<ToolModelReferenceLink> LoadReferencedToolModels(HelperTableEntityId id)
        {
            throw new InvalidOperationException();
        }

        public List<ToolReferenceLink> LoadToolReferenceLinks(HelperTableEntityId id)
        {
            throw new InvalidOperationException();
        }

        public List<LocationToolAssignmentId> LoadReferencedLocationToolAssignmentIds(HelperTableEntityId id)
        {
            var result = GetClient().GetToolUsageLocationToolAssignmentReferences(new Long() { Value = id.ToLong() });
            var references = new List<LocationToolAssignmentId>();
            foreach (var refId in result.Values)
            {
                references.Add(new LocationToolAssignmentId(refId.Value));
            }
            return references;
        }
    }
}
