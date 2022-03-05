using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Core;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.Entities.ToolTypes;
using Core.UseCases;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using LocationToolAssignmentService;
using DateTime = System.DateTime;
using LocationToolAssignment = Core.Entities.LocationToolAssignment;
using LocationToolAssignmentDiff = Core.Diffs.LocationToolAssignmentDiff;
using ToolUsage = Core.Entities.ToolUsage;
using User = Core.Entities.User;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface ILocationToolAssignmentClient
    {
        ListOfLocationToolAssignments LoadLocationToolAssignments();
        ListOfLocationLink LoadLocationReferenceLinksForTool(Long toolId);
        ListOfToolUsage LoadUnusedToolUsagesForLocation(Long locationId);
        ListOfLocationToolAssignments GetLocationToolAssignmentsByLocationId(Long locationId);
        ListOfLocationToolAssignments GetLocationToolAssignmentsByIds(ListOfLongs ids);
        ListOfLongs InsertLocationToolAssignmentsWithHistory(ListOfLocationToolAssignmentDiffs diffs);
        void UpdateLocationToolAssignmentsWithHistory(ListOfLocationToolAssignmentDiffs diffs);
        void AddTestConditions(AddTestConditionsRequest request);
    }


    public class LocationToolAssignmentDataAccess : ILocationToolAssignmentData
    {
        private readonly IClientFactory _clientFactory;
        private readonly ILocationDisplayFormatter _locationDisplayFormatter;
        private readonly ITimeDataAccess _timeDataAccess;
        private readonly Mapper _mapper = new Mapper();

        public LocationToolAssignmentDataAccess(IClientFactory clientFactory, ILocationDisplayFormatter locationDisplayFormatter,
            ITimeDataAccess timeDataAccess)
        {
            _clientFactory = clientFactory;
            _locationDisplayFormatter = locationDisplayFormatter;
            _timeDataAccess = timeDataAccess;
        }

        private ILocationToolAssignmentClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetLocationToolAssignmentClient();
        }

        public List<LocationToolAssignment> LoadLocationToolAssignments()
        {
            var dtos = GetClient().LoadLocationToolAssignments();
            var entities = new List<LocationToolAssignment>();
            foreach (var dto in dtos.Values)
            {
                entities.Add(_mapper.DirectPropertyMapping(dto));
            }
            return entities;
        }

        public List<LocationToolAssignment> GetLocationToolAssignmentsByIds(List<LocationToolAssignmentId> ids)
        {
            var dtoIds = new ListOfLongs();
            ids.ForEach(x => dtoIds.Values.Add(new Long() {Value = x.ToLong()}));
            var locationToolAssignmentDtos = GetClient().GetLocationToolAssignmentsByIds(dtoIds);

            var result = new List<LocationToolAssignment>();
            foreach (var locationToolAssignmentDto in locationToolAssignmentDtos.Values)
            {
                result.Add(_mapper.DirectPropertyMapping(locationToolAssignmentDto));
            }
            return result;
        }

        public void AssignToolToLocation(LocationToolAssignment assignment, User user)
        {
            if (assignment is null)
            {
                throw new ArgumentNullException(nameof(assignment));
            }

            var diffs = new ListOfLocationToolAssignmentDiffs()
            {
                Diffs =
                {
                    new DtoTypes.LocationToolAssignmentDiff()
                    {
                        UserId = user.UserId.ToLong(),
                        NewLocationToolAssignment = _mapper.DirectPropertyMapping(assignment)
                    }
                }
            };

            var result = GetClient().InsertLocationToolAssignmentsWithHistory(diffs);

            if (result?.Values.FirstOrDefault() == null)
            {
                throw new NullReferenceException("Server returned null when assigning tool to location");
            }

            assignment.Id = new LocationToolAssignmentId(result.Values.First().Value);
        }

        public void AddTestConditions(LocationToolAssignment assignment, User user)
        {
            if (assignment is null)
            {
                throw new ArgumentNullException(nameof(assignment));
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User should not be null");
            }

            var request = new AddTestConditionsRequest()
            {
                LocationToolAssignment = _mapper.DirectPropertyMapping(assignment),
                UserId = user.UserId.ToLong()
            };

           GetClient().AddTestConditions(request);
        }

        public List<LocationToolAssignment> LoadAssignedToolsForLocation(LocationId locationId)
        {
            var locationToolAssignmentDtos = GetClient().GetLocationToolAssignmentsByLocationId(new Long() {Value = locationId.ToLong()});

            var result = new List<LocationToolAssignment>();
            foreach (var locationToolAssignmentDto in locationToolAssignmentDtos.Values)
            {
                var assignment = _mapper.DirectPropertyMapping(locationToolAssignmentDto);

                //TODO: Remove Line if ToolModel Grpc is finished
                if (assignment.AssignedTool.ToolModel != null && assignment.AssignedTool.ToolModel.ModelType == null)
                {
                    assignment.AssignedTool.ToolModel.ModelType = new ClickWrench();
                }
                

                result.Add(assignment);
            }
            return result;
        }

        public List<ToolUsage> LoadUnusedToolUsagesForLocation(LocationId locationId)
        {
            var toolUsageDtoList = GetClient().LoadUnusedToolUsagesForLocation(new Long() {Value = locationId.ToLong()});
            var mapper = new Mapper();
            var toolUsages = new List<ToolUsage>();
            foreach (var toolUsage in toolUsageDtoList.ToolUsageList)
            {
                toolUsages.Add(mapper.DirectPropertyMapping(toolUsage));
            }

            return toolUsages;
        }

        public void RemoveLocationToolAssignment(LocationToolAssignment assignment, User user)
        {
            if (assignment is null)
            {
                throw new ArgumentNullException(nameof(assignment));
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User should not be null");
            }

            var oldLocationToolAssignment = _mapper.DirectPropertyMapping(assignment);
            var newLocationToolAssignment = _mapper.DirectPropertyMapping(assignment);
            oldLocationToolAssignment.Alive = true;
            newLocationToolAssignment.Alive = false;

            if (oldLocationToolAssignment.TestParameters != null)
            {
                oldLocationToolAssignment.TestParameters.Alive = true;
            }

            if (newLocationToolAssignment.TestParameters != null)
            {
                newLocationToolAssignment.TestParameters.Alive = false;
            }

            var diffs = new ListOfLocationToolAssignmentDiffs()
            {
                Diffs =
                {
                    new DtoTypes.LocationToolAssignmentDiff()
                    {
                        UserId = user.UserId.ToLong(),
                        OldLocationToolAssignment = oldLocationToolAssignment,
                        NewLocationToolAssignment = newLocationToolAssignment
                    }
                }
            };

            GetClient().UpdateLocationToolAssignmentsWithHistory(diffs);
        }

        public void RestoreLocationToolAssignment(LocationToolAssignment assignment, User user)
        {
            if (assignment is null)
            {
                throw new ArgumentNullException(nameof(assignment));
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User should not be null");
            }

            var oldLocationToolAssignment = _mapper.DirectPropertyMapping(assignment);
            var newLocationToolAssignment = _mapper.DirectPropertyMapping(assignment);
            oldLocationToolAssignment.Alive = false;
            newLocationToolAssignment.Alive = true;

            if (oldLocationToolAssignment.TestParameters != null)
            {
                oldLocationToolAssignment.TestParameters.Alive = false;
            }

            if (newLocationToolAssignment.TestParameters != null)
            {
                newLocationToolAssignment.TestParameters.Alive = true;
            }

            var diffs = new ListOfLocationToolAssignmentDiffs()
            {
                Diffs =
                {
                    new DtoTypes.LocationToolAssignmentDiff()
                    {
                        UserId = user.UserId.ToLong(),
                        OldLocationToolAssignment = oldLocationToolAssignment,
                        NewLocationToolAssignment = newLocationToolAssignment
                    }
                }
            };

            GetClient().UpdateLocationToolAssignmentsWithHistory(diffs);
        }

        public List<LocationReferenceLink> LoadLocationReferenceLinksForTool(ToolId toolId)
        {
            var locationLinks = GetClient().LoadLocationReferenceLinksForTool(new Long(){Value = toolId.ToLong()});
            var locationReferenceLinks = new List<LocationReferenceLink>();

            foreach (var locationLinkDto in locationLinks.LocationLinks)
            {
                var locationReferenceLink = new LocationReferenceLink(new QstIdentifier(locationLinkDto.Id), new LocationNumber(locationLinkDto.Number), new LocationDescription(locationLinkDto.Description), _locationDisplayFormatter);
                locationReferenceLinks.Add(locationReferenceLink);
            }

            return locationReferenceLinks;
        }

        public void UpdateLocationToolAssignment(List<LocationToolAssignmentDiff> diffs)
        {
            var dto = new ListOfLocationToolAssignmentDiffs();

            if(diffs == null)
            {
                throw new ArgumentNullException(nameof(diffs));

            }

            foreach (var diff in diffs)
            {
                if (!diff.OldLocationToolAssignment.EqualsById(diff.NewLocationToolAssignment))
                {
                    throw new ArgumentException("Missmatching LocationToolAssignmentIds");
                }

                var oldLocationToolAssignment = _mapper.DirectPropertyMapping(diff.OldLocationToolAssignment);
                var newLocationToolAssignment = _mapper.DirectPropertyMapping(diff.NewLocationToolAssignment);
                oldLocationToolAssignment.Alive = true;
                newLocationToolAssignment.Alive = true;

                if (oldLocationToolAssignment.TestParameters != null)
                {
                    oldLocationToolAssignment.TestParameters.Alive = true;
                }

                if (newLocationToolAssignment.TestParameters != null)
                {
                    newLocationToolAssignment.TestParameters.Alive = true;
                }


                dto.Diffs.Add(
                    new DtoTypes.LocationToolAssignmentDiff()
                    {
                        UserId = diff.User.UserId.ToLong(),
                        OldLocationToolAssignment = oldLocationToolAssignment,
                        NewLocationToolAssignment = newLocationToolAssignment,
                        Comment = new NullableString { IsNull = false, Value = diff.Comment?.ToDefaultString() == null ? "" : diff.Comment.ToDefaultString() }
                    }); 
            }

            GetClient().UpdateLocationToolAssignmentsWithHistory(dto);
        }
    }
}
