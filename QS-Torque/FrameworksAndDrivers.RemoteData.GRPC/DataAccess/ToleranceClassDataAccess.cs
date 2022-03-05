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
using log4net;
using ToleranceClassService;
using DateTime = System.DateTime;
using ToleranceClass = Core.Entities.ToleranceClass;
using ToleranceClassDiff = Core.UseCases.ToleranceClassDiff;
using User = Core.Entities.User;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface IToleranceClassClient
    {
        ListOfToleranceClasses LoadToleranceClasses();
        ListOfToleranceClasses InsertToleranceClassesWithHistory(InsertToleranceClassesWithHistoryRequest request);
        ListOfToleranceClasses UpdateToleranceClassesWithHistory(UpdateToleranceClassesWithHistoryRequest request);
        ListOfLocationLink GetToleranceClassLocationLinks(LongRequest toleranceClassId);
        ListOfLongs GetToleranceClassLocationToolAssignmentLinks(LongRequest request);
        GetToleranceClassesFromHistoryForIdsResponse GetToleranceClassFromHistoryForIds(ListOfToleranceClassFromHistoryParameter datas);
    }

    public class ToleranceClassDataAccess : IToleranceClassData
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ToleranceClassDataAccess));
        private readonly IClientFactory _clientFactory;
        private readonly ILocationDisplayFormatter _locationDisplayFormatter;
        private readonly ITimeDataAccess _timeDataAccess;

        public ToleranceClassDataAccess(IClientFactory clientFactory, ILocationDisplayFormatter locationDisplayFormatter, ITimeDataAccess timeDataAccess)
        {
            _clientFactory = clientFactory;
            _locationDisplayFormatter = locationDisplayFormatter;
            _timeDataAccess = timeDataAccess;
        }

        private IToleranceClassClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetToleranceClassClient();
        }

        public List<ToleranceClass> LoadToleranceClasses()
        {
            var dtoList = GetClient().LoadToleranceClasses();
            var mapper = new Mapper();
            var toleranceClasses = new List<ToleranceClass>();

            foreach (var dto in dtoList.ToleranceClasses)
            {
                toleranceClasses.Add(mapper.DirectPropertyMapping(dto));
            }

            return toleranceClasses;
        }

        public void RemoveToleranceClass(ToleranceClassDiff toleranceClass)
        {
            if (toleranceClass.User == null)
            {
                throw new ArgumentException("User should not be null");
            }
            var mapper = new Mapper();
            var oldTol = mapper.DirectPropertyMapping(toleranceClass.OldToleranceClass);
            var newTol = mapper.DirectPropertyMapping(toleranceClass.NewToleranceClass);
            oldTol.Alive = true;
            newTol.Alive = false;

            var request = new UpdateToleranceClassesWithHistoryRequest()
            {
                ToleranceClassesDiffs = new ListOfToleranceClassesDiffs()
                {
                    ToleranceClassesDiff =
                    {
                        new DtoTypes.ToleranceClassDiff()
                        {
                            UserId = toleranceClass.User.UserId.ToLong(),
                            Comment = toleranceClass.Comment.ToDefaultString(),
                            OldToleranceClass = oldTol,
                            NewToleranceClass = newTol
                        }
                    }
                }
            };

            GetClient().UpdateToleranceClassesWithHistory(request);
        }

        public ToleranceClass AddToleranceClass(ToleranceClass toleranceClass, User byUser)
        {
            if (byUser == null)
            {
                throw new ArgumentException("User should not be null");
            }

            var request = new InsertToleranceClassesWithHistoryRequest()
            {
                ToleranceClassesDiffs = new ListOfToleranceClassesDiffs()
                {
                    ToleranceClassesDiff =
                    {
                        new DtoTypes.ToleranceClassDiff()
                        {
                            UserId = byUser.UserId.ToLong(),
                            Comment = "",
                            NewToleranceClass = GetToleranceClassDtoFromToleranceClass(toleranceClass)
                        }
                    }
                },
                ReturnList = true
            };

            var result = GetClient().InsertToleranceClassesWithHistory(request);

            if (result?.ToleranceClasses.FirstOrDefault() == null)
            {
                throw new NullReferenceException("Server returned null when Adding a ToleranceClass");
            }

            return CreateToleranceClassFromToleranceClassDto(result.ToleranceClasses.First());
        }

        
        public ToleranceClass SaveToleranceClass(ToleranceClassDiff toleranceClass)
        {
            if (toleranceClass.OldToleranceClass != null && !toleranceClass.OldToleranceClass.EqualsById(toleranceClass.NewToleranceClass))
            {
                throw new ArgumentException("Missmatching toleranceClassIds");
            }
            var mapper = new Mapper();
            var oldTol = mapper.DirectPropertyMapping(toleranceClass.OldToleranceClass);
            var newTol = mapper.DirectPropertyMapping(toleranceClass.NewToleranceClass);
            oldTol.Alive = true;
            newTol.Alive = true;

            var request = new UpdateToleranceClassesWithHistoryRequest()
            {
                ToleranceClassesDiffs = new ListOfToleranceClassesDiffs()
                {
                    ToleranceClassesDiff =
                    {
                        new DtoTypes.ToleranceClassDiff()
                        {
                            UserId = toleranceClass.User.UserId.ToLong(),
                            Comment = toleranceClass.Comment.ToDefaultString(),
                            OldToleranceClass = oldTol,
                            NewToleranceClass = newTol
                        }
                    }
                }
            };

            GetClient().UpdateToleranceClassesWithHistory(request);
            return toleranceClass.NewToleranceClass;
        }

        public List<LocationReferenceLink> LoadReferencedLocations(ToleranceClassId id)
        {
            var locationLinkDtos = GetClient().GetToleranceClassLocationLinks(new LongRequest() {Value = id.ToLong()});
            var locationReferenceLinks = new List<LocationReferenceLink>();

            foreach (var locationLinkDto in locationLinkDtos.LocationLinks)
            {
                try
                {
                    var locationReferenceLink = new LocationReferenceLink(new QstIdentifier(locationLinkDto.Id), new LocationNumber(locationLinkDto.Number), new LocationDescription(locationLinkDto.Description), _locationDisplayFormatter);
                    locationReferenceLinks.Add(locationReferenceLink);
                }
                catch (Exception exception)
                {
                    Log.Error($"Error while mapping Location with Id { locationLinkDto?.Id}", exception);
                }
            }

            return locationReferenceLinks;
        }

        public List<LocationToolAssignmentId> LoadReferencedLocationToolAssignments(ToleranceClassId id)
        {
            return GetClient().GetToleranceClassLocationToolAssignmentLinks(new LongRequest() {Value = id.ToLong()}).Values
                .Select(x => new LocationToolAssignmentId(x.Value))
                .ToList();
        }

        public Dictionary<long, ToleranceClass> GetToleranceClassFromHistoryForIds(List<Tuple<long, long, DateTime>> idsWithClassDatas)
        {
            var listOfToleranceClassesFromHistory = new ListOfToleranceClassFromHistoryParameter();
            foreach (var idsWithClassData in idsWithClassDatas)
            {
                var timestamp = _timeDataAccess.ConvertToUtc(idsWithClassData.Item3);
                listOfToleranceClassesFromHistory.Parameters.Add(new ToleranceClassFromHistoryParameter()
                {
                    GlobalHistoryId = idsWithClassData.Item1,
                    ToleranceClassId = idsWithClassData.Item2,
                    Timestamp = new BasicTypes.DateTime() { Ticks = timestamp.Ticks }
                });
            }
            var classFromHistoryForIdDtos = GetClient().GetToleranceClassFromHistoryForIds(listOfToleranceClassesFromHistory);

            var mapper = new Mapper();
            var classFromHistoryForIds = new Dictionary<long, ToleranceClass>();
            foreach (var dto in classFromHistoryForIdDtos.Datas)
            {
                classFromHistoryForIds[dto.GlobalHistoryId] = mapper.DirectPropertyMapping(dto.ToleranceClass);
            }

            return classFromHistoryForIds;
        }

        private static DtoTypes.ToleranceClass GetToleranceClassDtoFromToleranceClass(ToleranceClass toleranceClass)
        {
            return new DtoTypes.ToleranceClass()
            {
                Id = toleranceClass.Id?.ToLong() ?? 0,
                Name = toleranceClass.Name,
                Relative = toleranceClass.Relative,
                LowerLimit = toleranceClass.LowerLimit,
                UpperLimit = toleranceClass.UpperLimit
            };
        }

        private ToleranceClass CreateToleranceClassFromToleranceClassDto(DtoTypes.ToleranceClass toleranceClass)
        {
            return new ToleranceClass()
            {
                Id = new ToleranceClassId(toleranceClass.Id),
                Name = toleranceClass.Name,
                Relative = toleranceClass.Relative,
                LowerLimit = toleranceClass.LowerLimit,
                UpperLimit = toleranceClass.UpperLimit
            };
        }

    }
}
