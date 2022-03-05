using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Grpc.Core;
using LocationToolAssignmentService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class LocationToolAssignmentClient : ILocationToolAssignmentClient
    {
        private readonly LocationToolAssignments.LocationToolAssignmentsClient _locationToolAssignmentClient;

        public LocationToolAssignmentClient(LocationToolAssignments.LocationToolAssignmentsClient locationToolAssignmentClient)
        {
            _locationToolAssignmentClient = locationToolAssignmentClient;
        }
        
        public ListOfLocationToolAssignments LoadLocationToolAssignments()
        {
            return _locationToolAssignmentClient.LoadLocationToolAssignments(new NoParams(), new CallOptions());
        }

        public ListOfLocationLink LoadLocationReferenceLinksForTool(Long toolId)
        {
            return _locationToolAssignmentClient.LoadLocationReferenceLinksForTool(toolId);
        }

        public ListOfToolUsage LoadUnusedToolUsagesForLocation(Long locationId)
        {
            return _locationToolAssignmentClient.LoadUnusedToolUsagesForLocation(locationId);
        }

        public ListOfLocationToolAssignments GetLocationToolAssignmentsByLocationId(Long locationId)
        {
            return _locationToolAssignmentClient.GetLocationToolAssignmentsByLocationId(locationId);
        }

        public ListOfLocationToolAssignments GetLocationToolAssignmentsByIds(ListOfLongs ids)
        {
            return _locationToolAssignmentClient.GetLocationToolAssignmentsByIds(ids);
        }

        public ListOfLongs InsertLocationToolAssignmentsWithHistory(ListOfLocationToolAssignmentDiffs diffs)
        {
            return _locationToolAssignmentClient.InsertLocationToolAssignmentsWithHistory(diffs);
        }

        public void UpdateLocationToolAssignmentsWithHistory(ListOfLocationToolAssignmentDiffs diffs)
        {
            _locationToolAssignmentClient.UpdateLocationToolAssignmentsWithHistory(diffs);
        }

        public void AddTestConditions(AddTestConditionsRequest request)
        {
            _locationToolAssignmentClient.AddTestConditions(request);
        }
    }
}
