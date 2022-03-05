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
using Grpc.Core;
using HelperTableService;
using State;
using HelperTableEntity = Core.Entities.HelperTableEntity;
using User = Core.Entities.User;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface IHelperTableClient
    {
        ListOfHelperTableEntity GetHelperTableByNodeId(Long request);
        ListOfHelperTableEntity GetAllHelperTableEntities();
        ListOfModelLink GetHelperTableEntityModelLinks(HelperTableEntityLinkRequest request);
        ListOfToolReferenceLink GetHelperTableEntityToolLinks(HelperTableEntityLinkRequest request);
        ListOfHelperTableEntity InsertHelperTableEntityWithHistory(InsertHelperTableEntityWithHistoryRequest request);
        ListOfHelperTableEntity UpdateHelperTableEntityWithHistory(UpdateHelperTableEntityWithHistoryRequest request);
    }

    public interface IHelperTableReferenceLoader
    {
        List<ToolModelReferenceLink> GetReferencedToolModels(HelperTableEntityId id);
        List<Core.Entities.ReferenceLink.ToolReferenceLink> GetReferencedTools(HelperTableEntityId id);
    }

    public class LambdaHelperTableEntitySupport<T> : IHelperTableEntitySupport<T> where T : HelperTableEntity
    {
        private Func<T> _createEntity;
        private Func<T, Mapper, DtoTypes.HelperTableEntity> _mapEntityToDto;
        private Action<DtoTypes.HelperTableEntity, T, Mapper> _mapDtoToEntity;

        public LambdaHelperTableEntitySupport(
            Func<T> createEntity,
            Func<T, Mapper, DtoTypes.HelperTableEntity> mapEntityToDto,
            Action<DtoTypes.HelperTableEntity, T, Mapper> mapDtoToEntity)
        {
            _createEntity = createEntity;
            _mapEntityToDto = mapEntityToDto;
            _mapDtoToEntity = mapDtoToEntity;
        }

        public T CreateEntity()
        {
            return _createEntity();
        }

        public void MapDtoToEntity(DtoTypes.HelperTableEntity dto, T entity, Mapper mapper)
        {
            _mapDtoToEntity(dto, entity, mapper);
        }

        public DtoTypes.HelperTableEntity MapEntityToDto(T entity, Mapper mapper)
        {
            return _mapEntityToDto(entity, mapper);
        }
    }

    public class HelperTableDataAccess<T> : IHelperTableData<T> where T : HelperTableEntity, IQstEquality<T>
    {
        private readonly IClientFactory _clientFactory;
        protected readonly NodeId _nodeId;
        protected readonly IHelperTableEntitySupport<T> _entitySupport;
        private readonly IHelperTableReferenceLoader _referenceLoader;
        public bool HasToolModelAsReference { get; private set; }
        public bool HasToolAsReference { get; private set; }
        public bool HasLocationToolAssignmentAsReference => false;

        public HelperTableDataAccess(
            NodeId nodeId,
            IHelperTableEntitySupport<T> entitySupport,
            bool hasToolModelAsReference,
            bool hasToolAsReference,
            IHelperTableReferenceLoader toolModelReferenceLoader,
            IClientFactory clientFactory)
        {
            _nodeId = nodeId;
            _entitySupport = entitySupport;
            _referenceLoader = toolModelReferenceLoader;
            HasToolModelAsReference = hasToolModelAsReference;
            HasToolAsReference = hasToolAsReference;
            _clientFactory = clientFactory;
        }

        private IHelperTableClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetHelperTableClient();
        }

        public List<T> LoadItems()
        {
            var dtoList = GetClient().GetHelperTableByNodeId(new Long() { Value = (long)_nodeId });

            var mapper = new Mapper();
            var items = new List<T>();

            foreach (var dto in dtoList.HelperTableEntities)
            {
                var entity = _entitySupport.CreateEntity();
                _entitySupport.MapDtoToEntity(dto, entity, mapper);
                items.Add(entity);
            }

            return items;
        }

        public HelperTableEntityId AddItem(T item, User byUser)
        {
            try
            {
                var newHelperTableEntity = _entitySupport.MapEntityToDto(item, new Mapper());
                newHelperTableEntity.NodeId = (long)_nodeId;

                var request = new InsertHelperTableEntityWithHistoryRequest()
                {
                    HelperTableEntityDiffs = new ListOfHelperTableEntityDiff()
                    {
                        HelperTableEntityDiff =
                        {
                            new HelperTableEntityDiff()
                            {
                                UserId = byUser.UserId.ToLong(),
                                Comment = "",
                                NewHelperTableEntity = newHelperTableEntity
                            }
                        }
                    },
                    ReturnList = true
                };

                var result = GetClient().InsertHelperTableEntityWithHistory(request);

                if (result?.HelperTableEntities.FirstOrDefault() == null)
                {
                    throw new NullReferenceException("Server returned null when Adding a HelperTableEntity");
                }

                return new HelperTableEntityId(result.HelperTableEntities.First().ListId);
            }
            catch (RpcException e) when (e.Status.StatusCode == StatusCode.AlreadyExists)
            {
                throw new EntryAlreadyExists($"{typeof(T).Name} with Id {item.ListId.ToLong()}", e);
            }
        }

        public void RemoveItem(T item, User byUser)
        {
            var oldHelperTableEntity = _entitySupport.MapEntityToDto(item, new Mapper());
            var newHelperTableEntity = _entitySupport.MapEntityToDto(item, new Mapper());
            oldHelperTableEntity.Alive = true;
            oldHelperTableEntity.NodeId = (long)_nodeId;
            newHelperTableEntity.Alive = false;
            newHelperTableEntity.NodeId = (long)_nodeId;

            var request = new UpdateHelperTableEntityWithHistoryRequest()
            {
                HelperTableEntityDiffs = new ListOfHelperTableEntityDiff()
                {
                    HelperTableEntityDiff =
                    {
                        new HelperTableEntityDiff()
                        {
                            UserId = byUser.UserId.ToLong(),
                            Comment = "",
                            OldHelperTableEntity = oldHelperTableEntity,
                            NewHelperTableEntity = newHelperTableEntity
                        }
                    }
                }
            };

            GetClient().UpdateHelperTableEntityWithHistory(request);
        }

        public void SaveItem(T oldItem, T changedItem, User byUser)
        {
            if (!oldItem.EqualsById(changedItem))
            {
                throw new ArgumentException("trying to save items with mismatching ids");
            }

            var oldHelperTableEntity = _entitySupport.MapEntityToDto(oldItem, new Mapper());
            var newHelperTableEntity = _entitySupport.MapEntityToDto(changedItem, new Mapper());
            oldHelperTableEntity.Alive = true;
            oldHelperTableEntity.NodeId = (long)_nodeId;
            newHelperTableEntity.Alive = true;
            newHelperTableEntity.NodeId = (long)_nodeId;

            var request = new UpdateHelperTableEntityWithHistoryRequest()
            {
                HelperTableEntityDiffs = new ListOfHelperTableEntityDiff()
                {
                    HelperTableEntityDiff =
                    {
                        new HelperTableEntityDiff()
                        {
                            UserId = byUser.UserId.ToLong(),
                            Comment = "",
                            OldHelperTableEntity = oldHelperTableEntity,
                            NewHelperTableEntity = newHelperTableEntity
                        }
                    }
                }
            };

            GetClient().UpdateHelperTableEntityWithHistory(request);
        }

        public List<ToolModelReferenceLink> LoadReferencedToolModels(HelperTableEntityId id)
        {
            return _referenceLoader.GetReferencedToolModels(id);
        }

        public List<Core.Entities.ReferenceLink.ToolReferenceLink> LoadToolReferenceLinks(HelperTableEntityId id)
        {
            return _referenceLoader.GetReferencedTools(id);
        }

        public List<LocationToolAssignmentId> LoadReferencedLocationToolAssignmentIds(HelperTableEntityId id)
        {
            throw new InvalidOperationException("This entity has no references to LocationToolAssignment");
        }
    }

    public interface IHelperTableEntitySupport<T> where T : HelperTableEntity
    {
        T CreateEntity();
        DtoTypes.HelperTableEntity MapEntityToDto(T entity, Mapper mapper);
        void MapDtoToEntity(DtoTypes.HelperTableEntity dto, T entity, Mapper mapper);
    }

    public class HelperTableToToolModelReferenceLoader : IHelperTableReferenceLoader
    {
        private readonly IClientFactory _clientFactory;
        private readonly NodeId _nodeId;

        public HelperTableToToolModelReferenceLoader(IClientFactory clientFactory, NodeId nodeId)
        {
            _clientFactory = clientFactory;
            _nodeId = nodeId;
        }

        private IHelperTableClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetHelperTableClient();
        }

        public List<ToolModelReferenceLink> GetReferencedToolModels(HelperTableEntityId id)
        {
            var modelLinkDtos = GetClient().GetHelperTableEntityModelLinks(new HelperTableEntityLinkRequest()
            {
                Id = id.ToLong(),
                NodeId = (long) _nodeId
            });

            var toolModelReferenceLinks = new List<ToolModelReferenceLink>();
            foreach (var modelLinkDto in modelLinkDtos.ModelLinks)
            {
                toolModelReferenceLinks.Add(new ToolModelReferenceLink
                {
                    Id = new QstIdentifier(modelLinkDto.Id),
                    DisplayName = modelLinkDto.Model
                });
            }

            return toolModelReferenceLinks;
        }

        public List<Core.Entities.ReferenceLink.ToolReferenceLink> GetReferencedTools(HelperTableEntityId id)
        {
            throw new InvalidOperationException("This entity has no references to Tool");
        }
    }

    public class HelperTableToToolReferenceLoader : IHelperTableReferenceLoader
    {
        private readonly IClientFactory _clientFactory;
        private readonly NodeId _nodeId;
        private readonly IToolDisplayFormatter _toolDisplayFormatter;

        public HelperTableToToolReferenceLoader(IClientFactory clientFactory, NodeId nodeId, IToolDisplayFormatter toolDisplayFormatter)
        {
            _clientFactory = clientFactory;
            _nodeId = nodeId;
            _toolDisplayFormatter = toolDisplayFormatter;
        }

        private IHelperTableClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetHelperTableClient();
        }

        public List<ToolModelReferenceLink> GetReferencedToolModels(HelperTableEntityId id)
        {
            throw new InvalidOperationException("This entity has no references to ToolModel");
        }

        public List<Core.Entities.ReferenceLink.ToolReferenceLink> GetReferencedTools(HelperTableEntityId id)
        {
            var toolReferenceLinks = new List<Core.Entities.ReferenceLink.ToolReferenceLink>();

            var powToolLinkDtos = GetClient().GetHelperTableEntityToolLinks(new HelperTableEntityLinkRequest()
            {
                Id = id.ToLong(),
                NodeId = (long)_nodeId
            });

            foreach (var powToolLinkDto in powToolLinkDtos.ToolReferenceLinks)
            {
                toolReferenceLinks.Add(new Core.Entities.ReferenceLink.ToolReferenceLink(
                    new QstIdentifier(powToolLinkDto.Id), powToolLinkDto.InventoryNumber, powToolLinkDto.SerialNumber,
                    _toolDisplayFormatter));
            }

            return toolReferenceLinks;
        }
    }

    public class HelperTableToToolModelReferenceDoesNotExist : IHelperTableReferenceLoader
    {
        public List<ToolModelReferenceLink> GetReferencedToolModels(HelperTableEntityId id)
        {
            throw new InvalidOperationException("This entity has no references to ToolModel");
        }

        public List<Core.Entities.ReferenceLink.ToolReferenceLink> GetReferencedTools(HelperTableEntityId id)
        {
            throw new InvalidOperationException("This entity has no references to Tool");
        }
    }
}
