using Core.UseCases;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using log4net;
using ProcessControlService;
using System;
using System.Linq;
using BasicTypes;
using Client.UseCases.UseCases;
using Location = Core.Entities.Location;
using ProcessControlCondition = Client.Core.Entities.ProcessControlCondition;
using User = Core.Entities.User;
using System.Collections.Generic;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface IProcessControlClient
    {
        void UpdateProcessControlConditionsWithHistory(UpdateProcessControlConditionsWithHistoryRequest updateRequest);
        LoadProcessControlConditionForLocationResponse LoadProcessControlConditionForLocation(BasicTypes.Long locationId);
        ListOfProcessControlCondition InsertProcessControlConditionsWithHistory(InsertProcessControlConditionsWithHistoryRequest request);
        ListOfProcessControlConditions LoadProcessControlConditions(NoParams request);
    }

    public class ProcessControlDataAccess : IProcessControlData
    {
        private readonly IClientFactory _clientFactory;
        private static readonly ILog Log = LogManager.GetLogger(typeof(ProcessControlUseCase));
        private readonly Mapper _mapper = new Mapper();

        public ProcessControlDataAccess(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private IProcessControlClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetProcessControlClient();
        }
        
        public void RemoveProcessControlCondition(ProcessControlCondition processControlCondition, Core.Entities.User byUser)
        {
            if (byUser == null)
            {
                throw new ArgumentException("User should not be null");
            }

            var oldCondition = _mapper.DirectPropertyMapping(processControlCondition);
            var newCondition = _mapper.DirectPropertyMapping(processControlCondition);

            oldCondition.Alive = true;
            newCondition.Alive = false;

            if (oldCondition.ProcessControlTech.QstProcessControlTech != null)
            {
                oldCondition.ProcessControlTech.QstProcessControlTech.Alive = true;
            }

            if (newCondition.ProcessControlTech.QstProcessControlTech != null)
            {
                newCondition.ProcessControlTech.QstProcessControlTech.Alive = false;
            }

            var request = new UpdateProcessControlConditionsWithHistoryRequest
            {
                ConditionDiffs = new ListOfProcessControlConditionDiffs()
            };

            var diff = new ProcessControlConditionDiff
            {
                UserId = byUser.UserId.ToLong(),
                Comment = new NullableString() { IsNull = false, Value = "" },
                NewCondition = newCondition,
                OldCondition = oldCondition
            };

            request.ConditionDiffs.ConditionDiff.Add(diff);
            GetClient().UpdateProcessControlConditionsWithHistory(request);
        }

        public void RestoreProcessControlCondition(ProcessControlCondition processControlCondition, Core.Entities.User byUser)
        {
            if (byUser == null)
            {
                throw new ArgumentException("User should not be null");
            }

            var oldCondition = _mapper.DirectPropertyMapping(processControlCondition);
            var newCondition = _mapper.DirectPropertyMapping(processControlCondition);

            oldCondition.Alive = false;
            newCondition.Alive = true;

            if (oldCondition.ProcessControlTech.QstProcessControlTech != null)
            {
                oldCondition.ProcessControlTech.QstProcessControlTech.Alive = false;
            }

            if (newCondition.ProcessControlTech.QstProcessControlTech != null)
            {
                newCondition.ProcessControlTech.QstProcessControlTech.Alive = true;
            }

            var request = new UpdateProcessControlConditionsWithHistoryRequest
            {
                ConditionDiffs = new ListOfProcessControlConditionDiffs()
            };

            var diff = new ProcessControlConditionDiff
            {
                UserId = byUser.UserId.ToLong(),
                Comment = new NullableString() { IsNull = false, Value = "" },
                NewCondition = newCondition,
                OldCondition = oldCondition
            };

            request.ConditionDiffs.ConditionDiff.Add(diff);
            GetClient().UpdateProcessControlConditionsWithHistory(request);
        }

        public ProcessControlCondition LoadProcessControlConditionForLocation(Location location)
        {
            if (location?.Id == null)
            {
                throw new ArgumentException("Location should not be null");
            }

            var response =
                GetClient().LoadProcessControlConditionForLocation(new Long() {Value = location.Id.ToLong()});

            if (response?.Value == null)
            {
                return null;
            }

            return _mapper.DirectPropertyMapping(response.Value);
        }

        public ProcessControlCondition AddProcessControlCondition(ProcessControlCondition processControlCondition, User byUser)
        {
            if (byUser == null)
            {
                throw new ArgumentException("User should not be null");
            }

            var request = new InsertProcessControlConditionsWithHistoryRequest()
            {
                Diffs = new ListOfProcessControlConditionDiffs()
                {
                    ConditionDiff =
                    {
                        new DtoTypes.ProcessControlConditionDiff()
                        {
                            UserId = byUser.UserId.ToLong(),
                            Comment = new NullableString(){IsNull = true, Value = ""},
                            NewCondition = _mapper.DirectPropertyMapping(processControlCondition)
                        }
                    }
                },
                ReturnList = true
            };

            var result = GetClient().InsertProcessControlConditionsWithHistory(request);

            if (result?.ProcessControlConditions.FirstOrDefault() == null)
            {
                throw new NullReferenceException("Server returned null when adding a ProcessControlCondition");
            }
            return _mapper.DirectPropertyMapping(result.ProcessControlConditions.FirstOrDefault());
        }

        public void SaveProcessControlCondition(List<Client.Core.Diffs.ProcessControlConditionDiff> diffs)
        {
            var request = new UpdateProcessControlConditionsWithHistoryRequest()
            {
                ConditionDiffs = new ListOfProcessControlConditionDiffs()
            };


            foreach (var diff in diffs)
            {
                if (diff.GetOldProcessControlCondition().Id.ToLong() != diff.GetNewProcessControlCondition().Id.ToLong())
                {
                    throw new ArgumentException("Mismatching TestEquipmentModelIds");
                }

                if (diff.GetOldProcessControlCondition().EqualsByContent(diff.GetNewProcessControlCondition()))
                {
                    continue;
                }

                var oldTestEquipmentModel = _mapper.DirectPropertyMapping(diff.GetOldProcessControlCondition());
                var newTestEquipmentModel = _mapper.DirectPropertyMapping(diff.GetNewProcessControlCondition());

                oldTestEquipmentModel.Alive = true;
                newTestEquipmentModel.Alive = true;

                if (oldTestEquipmentModel.ProcessControlTech?.QstProcessControlTech != null)
                {
                    oldTestEquipmentModel.ProcessControlTech.QstProcessControlTech.Alive = true;
                }

                if (newTestEquipmentModel.ProcessControlTech?.QstProcessControlTech != null)
                {
                    newTestEquipmentModel.ProcessControlTech.QstProcessControlTech.Alive = true;
                }

                request.ConditionDiffs.ConditionDiff.Add(new DtoTypes.ProcessControlConditionDiff()
                {
                    UserId = diff.User.UserId.ToLong(),
                    Comment = new NullableString() { IsNull = false, Value = diff.Comment == null ? "" : diff.Comment.ToDefaultString() },
                    OldCondition = oldTestEquipmentModel,
                    NewCondition = newTestEquipmentModel
                });
            }

            if (request.ConditionDiffs.ConditionDiff.Count > 0)
            {
                GetClient().UpdateProcessControlConditionsWithHistory(request); 
            }
        }

        public List<ProcessControlCondition> LoadProcessControlConditions()
        {
            var dtos = GetClient().LoadProcessControlConditions(new NoParams()).Conditions.ToList();
            return dtos.Select(x => _mapper.DirectPropertyMapping(x)).ToList();
        }
    }
}
