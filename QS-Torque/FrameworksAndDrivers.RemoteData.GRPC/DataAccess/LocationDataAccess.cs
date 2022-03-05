using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Core.Entities;
using Core.UseCases;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using log4net;
using LocationService;
using Location = Core.Entities.Location;
using LocationDiff = Core.Diffs.LocationDiff;
using LocationDirectory = Core.Entities.LocationDirectory;
using User = Core.Entities.User;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface ILocationClient
    {
        ListOfLocationDirectory LoadLocationDirectories();
        ListOfLocationDirectory LoadAllLocationDirectories();
        ListOfLocationDirectory InsertLocationDirectoriesWithHistory(InsertLocationDirectoriesWithHistoryRequest request);
        ListOfLocationDirectory UpdateLocationDirectoriesWithHistory(UpdateLocationDirectoriesWithHistoryRequest request);

        ListOfLocation LoadLocations(LoadLocationsRequest request);
        ListOfLocation LoadDeletedLocations(LoadLocationsRequest request);
        ListOfLocation LoadLocationsByIds(ListOfLongs ids);
        ListOfLocation InsertLocationsWithHistory(InsertLocationsWithHistoryRequest request);
        ListOfLocation UpdateLocationsWithHistory(UpdateLocationsWithHistoryRequest request);
        BasicTypes.Bool IsUserIdUnique(BasicTypes.String name);
        ListOfLongs GetReferencedLocPowIdsForLocationId(Long locationId);
        BasicTypes.String GetLocationComment(Long locationId);
        LoadPictureForLocationResponse LoadPictureForLocation(LoadPictureForLocationRequest request);
    }

    public class LocationDataAccess : ILocationData
    {
        private readonly IClientFactory _clientFactory;
        private readonly IPictureFromZipLoader _pictureFromZipLoader;
        private static readonly ILog Log = LogManager.GetLogger(typeof(LocationUseCase));
        private readonly Mapper _mapper = new Mapper();

        private const int LocationPackageSize = 25000;

        public LocationDataAccess(IClientFactory clientFactory, IPictureFromZipLoader pictureFromZipLoader)
        {
            _clientFactory = clientFactory;
            _pictureFromZipLoader = pictureFromZipLoader;
        }

        private ILocationClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetLocationClient();
        }

        public IEnumerable<Location> LoadLocations()
        {
            var locations = new List<Location>();
            var index = 0;
            ListOfLocation listOfLocation;

            do
            {
                var loadLocationRequest = new LoadLocationsRequest()
                {
                    Index = index,
                    Size = LocationPackageSize
                };

                listOfLocation = GetClient().LoadLocations(loadLocationRequest);
                index += LocationPackageSize;

                foreach (var dto in listOfLocation.Locations)
                {
                    Location location = null;
                    try
                    {
                        location = _mapper.DirectPropertyMapping(dto);
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Error while mapping Location with Id {dto.Id}", e);
                    }

                    if (location != null)
                    {
                        locations.Add(location);
                    }
                }

            } while (listOfLocation.Locations.Count > 0);

            return locations;
        }

        public List<Location> LoadLocationsByIds(List<LocationId> ids)
        {
            var dtoIds = new ListOfLongs();
            ids.ForEach(x => dtoIds.Values.Add(new Long() { Value = x.ToLong() }));

            var listOfLocation = GetClient().LoadLocationsByIds(dtoIds);
            var mapper = new Mapper();

            var locations = new List<Location>();
            foreach (var location in listOfLocation.Locations)
            {
                locations.Add(mapper.DirectPropertyMapping(location));
            }
            return locations;
        }

        public List<LocationDirectory> LoadDirectories()
        {
            var listOfLocationDirectory = GetClient().LoadAllLocationDirectories();
            var entities = new List<LocationDirectory>();
            var mapper = new Mapper();
            foreach (var locationDirectory in listOfLocationDirectory.LocationDirectories)
            {
                entities.Add(mapper.DirectPropertyMapping(locationDirectory));
            }
            return entities;
        }

        public Location AddLocation(Location location, User byUser)
        {
            if (byUser == null)
            {
                throw new ArgumentNullException(nameof(byUser), "User should not be null");
            }

            var request = new InsertLocationsWithHistoryRequest()
            {
                LocationDiffs = new ListOfLocationDiff()
                {
                    LocationDiffs =
                    {
                        new DtoTypes.LocationDiff()
                        {
                            User = _mapper.DirectPropertyMapping(byUser),
                            Comment = "",
                            NewLocation = _mapper.DirectPropertyMapping(location)
                        }
                    }
                },
                ReturnList = true
            };

            var result = GetClient().InsertLocationsWithHistory(request);

            if (result?.Locations.FirstOrDefault() == null)
            {
                throw new NullReferenceException("Server returned null when Adding a Location");
            }

            var newLocation = _mapper.DirectPropertyMapping(result.Locations.FirstOrDefault());
            newLocation.ToleranceClassAngle = location.ToleranceClassAngle;
            newLocation.ToleranceClassTorque = location.ToleranceClassTorque;
            return newLocation;
        }

        public void RemoveLocation(Location location, User byUser)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location), "Location should not be null");
            }

            if (byUser == null)
            {
                throw new ArgumentNullException(nameof(byUser), "User should not be null");
            }

            var oldLocation = _mapper.DirectPropertyMapping(location);
            var newLocation = _mapper.DirectPropertyMapping(location);
            oldLocation.Alive = true;
            newLocation.Alive = false;

            var request = GetUpdateLocationsWithHistoryRequest(oldLocation, newLocation, byUser, "");

            GetClient().UpdateLocationsWithHistory(request);
        }

        public void RestoreLocation(Location location, User byUser)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location), "Location should not be null");
            }

            if (byUser == null)
            {
                throw new ArgumentNullException(nameof(byUser), "User should not be null");
            }

            var oldLocation = _mapper.DirectPropertyMapping(location);
            var newLocation = _mapper.DirectPropertyMapping(location);
            oldLocation.Alive = false;
            newLocation.Alive = true;

            var request = GetUpdateLocationsWithHistoryRequest(oldLocation, newLocation, byUser, "");

            GetClient().UpdateLocationsWithHistory(request);
        }

        public Core.Entities.Picture LoadPictureForLocation(LocationId locationId)
        {
            if (locationId is null)
            {
                throw new ArgumentNullException(nameof(locationId), "LocationId should not be null");
            }

            const int pictureFileType = 0;

            var response = GetClient().LoadPictureForLocation(new LoadPictureForLocationRequest()
            {
                LocationId = locationId.ToLong(),
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

        public string LoadCommentForLocation(LocationId locationId)
        {
            if (locationId is null)
            {
                throw new ArgumentNullException(nameof(locationId), "LocationId should not be null");
            }

            var stringResponse = GetClient().GetLocationComment(new Long()
            {
                Value = locationId.ToLong()
            });

            return stringResponse.Value;
        }

        public Location UpdateLocation(LocationDiff diff)
        {
            if (!diff.OldLocation?.EqualsById(diff.NewLocation) ?? false)
            {
                throw new ArgumentException("Mismatching LocationIds");
            }

            var oldLocation = _mapper.DirectPropertyMapping(diff.OldLocation);
            var newLocation = _mapper.DirectPropertyMapping(diff.NewLocation);
            oldLocation.Alive = true;
            newLocation.Alive = true;

            var request =
                GetUpdateLocationsWithHistoryRequest(oldLocation, newLocation, diff.User, diff.Comment.ToDefaultString());

            GetClient().UpdateLocationsWithHistory(request);

            return diff.NewLocation;
        }

        public bool IsNumberUnique(string number)
        {
            var isUnique = GetClient().IsUserIdUnique(new BasicTypes.String() { Value = number });
            return isUnique.Value;
        }

        public LocationDirectory AddLocationDirectory(LocationDirectoryId parentId, string name, User byUser)
        {
            if (parentId is null)
            {
                throw new ArgumentNullException(nameof(parentId), "ParentId was null");
            }

            if (name is null || name == "")
            {
                throw new ArgumentNullException(nameof(name), "Name was null");
            }

            if (byUser == null)
            {
                throw new ArgumentNullException(nameof(byUser), "User should not be null");
            }

            var newDictionary = new LocationDirectory()
            {
                Id = new LocationDirectoryId(0),
                Name = new LocationDirectoryName(name),
                ParentId = parentId
            };

            var request = new InsertLocationDirectoriesWithHistoryRequest()
            {
                LocationDirectoryDiffs = new ListOfLocationDirectoryDiff()
                {
                    LocationDirectoyDiffs =
                    {
                        new DtoTypes.LocationDirectoryDiff()
                        {
                            UserId = byUser.UserId.ToLong(),
                            Comment = "",
                            NewLocationDirectory = _mapper.DirectPropertyMapping(newDictionary)
                        }
                    }
                },
                ReturnList = true
            };

            var result = GetClient().InsertLocationDirectoriesWithHistory(request);

            if (result?.LocationDirectories.FirstOrDefault() == null)
            {
                throw new NullReferenceException("Server returned null when Adding a LocationDirectory");
            }

            return _mapper.DirectPropertyMapping(result.LocationDirectories.FirstOrDefault());
        }

        public void RemoveDirectory(LocationDirectory selectedDirectory, User byUser)
        {
            if (selectedDirectory?.Id == null)
            {
                throw new ArgumentNullException(nameof(selectedDirectory),
                    "Directory should not be null in RemoveDirectory");
            }

            if (byUser == null)
            {
                throw new ArgumentNullException(nameof(byUser), "User should not be null");
            }

            var oldLocationDirectory = _mapper.DirectPropertyMapping(selectedDirectory);
            var newLocationDirectory = _mapper.DirectPropertyMapping(selectedDirectory);
            oldLocationDirectory.Alive = true;
            newLocationDirectory.Alive = false;

            var request = GetUpdateLocationDirectoriesWithHistoryRequest(oldLocationDirectory, newLocationDirectory, byUser);

            GetClient().UpdateLocationDirectoriesWithHistory(request);
        }

        public void RestoreDirectory(LocationDirectory selectedDirectory, User byUser)
        {
            if (selectedDirectory?.Id == null)
            {
                throw new ArgumentNullException(nameof(selectedDirectory),
                    "Directory should not be null in RestoreDirectory");
            }

            if (byUser == null)
            {
                throw new ArgumentNullException(nameof(byUser), "User should not be null");
            }

            var oldLocationDirectory = _mapper.DirectPropertyMapping(selectedDirectory);
            var newLocationDirectory = _mapper.DirectPropertyMapping(selectedDirectory);
            oldLocationDirectory.Alive = false;
            newLocationDirectory.Alive = true;

            var request = GetUpdateLocationDirectoriesWithHistoryRequest(oldLocationDirectory, newLocationDirectory, byUser);

            GetClient().UpdateLocationDirectoriesWithHistory(request);
        }

        public void ChangeLocationParent(Location location, LocationDirectoryId newParentId, User byUser)
        {
            if (location.ParentDirectoryId.Equals(newParentId))
            {
                return;
            }

            var oldLocation = _mapper.DirectPropertyMapping(location);
            var newLocation = _mapper.DirectPropertyMapping(location);
            oldLocation.Alive = true;
            newLocation.Alive = true;
            newLocation.ParentDirectoryId = newParentId.ToLong();

            var request = GetUpdateLocationsWithHistoryRequest(oldLocation, newLocation, byUser, "");

            GetClient().UpdateLocationsWithHistory(request);
        }

        public void ChangeLocationDirectoryParent(LocationDirectory directory, LocationDirectoryId newParentId, User byUser)
        {
            if (directory.ParentId.Equals(newParentId))
            {
                return;
            }

            var oldLocationDirectory = _mapper.DirectPropertyMapping(directory);
            var newLocationDirectory = _mapper.DirectPropertyMapping(directory);
            oldLocationDirectory.Alive = true;
            newLocationDirectory.Alive = true;
            newLocationDirectory.ParentId = newParentId.ToLong();

            var request = GetUpdateLocationDirectoriesWithHistoryRequest(oldLocationDirectory, newLocationDirectory, byUser);

            GetClient().UpdateLocationDirectoriesWithHistory(request);
        }

        public List<long> GetLocationToolAssignmentIdsByLocationId(LocationId locationId)
        {
            var dtoReferences = GetClient().GetReferencedLocPowIdsForLocationId(new Long() { Value = locationId.ToLong() });
            var references = new List<long>();
            foreach (var dtoReference in dtoReferences.Values)
            {
                references.Add(dtoReference.Value);
            }
            return references;
        }

        private UpdateLocationsWithHistoryRequest GetUpdateLocationsWithHistoryRequest(DtoTypes.Location oldLocation, DtoTypes.Location newLocation, User byUser, string comment)
        {
            var request = new UpdateLocationsWithHistoryRequest()
            {
                LocationDiffs = new ListOfLocationDiff()
                {
                    LocationDiffs =
                    {
                        new DtoTypes.LocationDiff()
                        {
                            User = _mapper.DirectPropertyMapping(byUser),
                            Comment = comment ?? "",
                            OldLocation = oldLocation,
                            NewLocation = newLocation
                        }
                    }
                }
            };
            return request;
        }

        private UpdateLocationDirectoriesWithHistoryRequest GetUpdateLocationDirectoriesWithHistoryRequest(
            DtoTypes.LocationDirectory oldLocationDirectory, DtoTypes.LocationDirectory newLocationDirectory, User byUser)
        {
            var request = new UpdateLocationDirectoriesWithHistoryRequest()
            {
                LocationDirectoryDiffs = new ListOfLocationDirectoryDiff()
                {
                    LocationDirectoyDiffs =
                    {
                        new LocationDirectoryDiff()
                        {
                            UserId = byUser.UserId.ToLong(),
                            Comment = "",
                            OldLocationDirectory = oldLocationDirectory,
                            NewLocationDirectory = newLocationDirectory
                        }
                    }
                }
            };
            return request;
        }
    }
}
