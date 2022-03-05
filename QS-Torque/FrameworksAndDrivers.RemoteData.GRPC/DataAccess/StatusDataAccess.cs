using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Core;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using StatusService;
using ToolReferenceLink = Core.Entities.ReferenceLink.ToolReferenceLink;
using User = Core.Entities.User;
using Status = Core.Entities.Status;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface IStatusClient
    {
        ListOfStatus LoadStatus();
        ListOfToolReferenceLink GetStatusToolLinks(LongRequest status);
        ListOfStatus InsertStatusWithHistory(InsertStatusWithHistoryRequest request);
        ListOfStatus UpdateStatusWithHistory(UpdateStatusWithHistoryRequest request);
    }

    public class StatusDataAccess : IHelperTableData<Status>
    {
        private readonly IClientFactory _clientFactory;
        private readonly IToolDisplayFormatter _toolDisplayFormatter;

        public bool HasToolModelAsReference => false;
        public bool HasToolAsReference => true;
        public bool HasLocationToolAssignmentAsReference => false;

        public StatusDataAccess(IClientFactory clientFactory, IToolDisplayFormatter toolDisplayFormatter)
        {
            _clientFactory = clientFactory;
            _toolDisplayFormatter = toolDisplayFormatter;
        }

        private IStatusClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetStatusClient();
        }

        public List<Status> LoadItems()
        {
            var dtoList = GetClient().LoadStatus();

            var mapper = new Mapper();
            var statusList = new List<Status>();

            foreach (var dto in dtoList.Status)
            {
                statusList.Add(mapper.DirectPropertyMapping(dto));
            }

            return statusList;
        }

        public HelperTableEntityId AddItem(Status item, User byUser)
        {
            if (byUser == null)
            {
                throw new ArgumentException("User should not be null");
            }

            var request = new InsertStatusWithHistoryRequest()
            {
                StatusDiffs = new ListOfStatusDiffs()
                {
                    StatusDiff =
                    {
                        new StatusDiff()
                        {
                            UserId = byUser.UserId.ToLong(),
                            Comment = "",
                            NewStatus = new DtoTypes.Status() {Id = item.ListId.ToLong(), Description = item.Value.ToDefaultString()}
                        }
                    }
                },
                ReturnList = true
            };

            var result = GetClient().InsertStatusWithHistory(request);

            if (result?.Status.FirstOrDefault() == null)
            {
                throw new NullReferenceException("Server returned null when Adding a Status");
            }

            return new HelperTableEntityId(result.Status.FirstOrDefault().Id);
        }

        public void RemoveItem(Status item, User byUser)
        {
            if (byUser == null)
            {
                throw new ArgumentException("User should not be null");
            }
            var mapper = new Mapper();
            var oldStatus = mapper.DirectPropertyMapping(item);
            var newStatus = mapper.DirectPropertyMapping(item);
            oldStatus.Alive = true;
            newStatus.Alive = false;

            var request = new UpdateStatusWithHistoryRequest()
            {
                StatusDiffs = new ListOfStatusDiffs()
                {
                    StatusDiff =
                    {
                        new StatusDiff()
                        {
                            UserId = byUser.UserId.ToLong(),
                            Comment = "",
                            OldStatus = oldStatus,
                            NewStatus = newStatus
                        }
                    }
                }
            };

            GetClient().UpdateStatusWithHistory(request);
        }

        public void SaveItem(Status oldItem, Status newItem, User byUser)
        {
            if (oldItem != null && !oldItem.EqualsById(newItem))
            {
                throw new ArgumentException("Missmatching StatusIds");
            }
            var mapper = new Mapper();
            var oldStatus = mapper.DirectPropertyMapping(oldItem);
            var newStatus = mapper.DirectPropertyMapping(newItem);
            oldStatus.Alive = true;
            newStatus.Alive = true;

            var request = new UpdateStatusWithHistoryRequest()
            {
                StatusDiffs = new ListOfStatusDiffs()
                {
                    StatusDiff =
                    {
                        new StatusDiff()
                        {
                            UserId = byUser.UserId.ToLong(),
                            Comment = "",
                            OldStatus = oldStatus,
                            NewStatus = newStatus
                        }
                    }
                }
            };

            GetClient().UpdateStatusWithHistory(request);
        }

        public List<ToolModelReferenceLink> LoadReferencedToolModels(HelperTableEntityId id)
        {
            throw new InvalidOperationException("Status has no references to ToolModel");
        }

        public List<ToolReferenceLink> LoadToolReferenceLinks(HelperTableEntityId id)
        {
            var tools = new List<ToolReferenceLink>();

            var statusToolLinks = GetClient().GetStatusToolLinks(new LongRequest() { Value = id.ToLong() });

            foreach (var statusToolLink in statusToolLinks.ToolReferenceLinks)
            {
                tools.Add(new ToolReferenceLink(new QstIdentifier(statusToolLink.Id), statusToolLink.InventoryNumber, statusToolLink.SerialNumber, _toolDisplayFormatter));
            }

            return tools;
        }

        public List<LocationToolAssignmentId> LoadReferencedLocationToolAssignmentIds(HelperTableEntityId id)
        {
            throw new InvalidOperationException("This entity has no references to LocationToolAssignment");
        }
    }
}
