using Core.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.ReferenceLink;

namespace Core.UseCases
{
    public interface IToolModelData
    {
        List<ToolModel> LoadToolModels();
        Picture LoadPictureForToolModel(long toolModelId);
        void RemoveToolModels(List<ToolModel> toolModels, User user);
        ToolModel AddToolModel(ToolModel toolModel, User byUser);
        ToolModel UpdateToolModel(ToolModelDiff toolModelDiff);
        List<ToolReferenceLink> LoadReferencedTools(long modelId);
    }

    public interface IToolModelGui
    {
        void ShowToolModels(List<ToolModel> toolModels);
        void ShowLoadingErrorMessage();
        void SetPictureForToolModel(long toolModelId, Picture picture);
        void ShowRemoveToolModelsErrorMessage();
        void RemoveToolModels(List<ToolModel> toolModels);
        void ShowCmCmk(double cm, double cmk);
        void ShowCmCmkError();
        void AddToolModel(ToolModel toolModel);
        void UpdateToolModel(ToolModel toolModel);
        bool ShowDiffDialog(ToolModelDiff diff);
        void ShowEntryAlreadyExistsMessage(ToolModel updatedToolModel);
        void ShowErrorMessage();
        void ShowRemoveToolModelPreventingReferences(List<ToolReferenceLink> references);
    }

    public interface IToolModelUseCase
    {
         IManufacturerUseCase ManufacturerUseCase { get; }
         IHelperTableUseCase<ToolType> ToolTypeUseCase { get; }
         IHelperTableUseCase<DriveSize> DriveSizeUseCase { get; }
         IHelperTableUseCase<DriveType> DriveTypeUseCase { get; }
         IHelperTableUseCase<SwitchOff> SwitchOffUseCase { get; }
         IHelperTableUseCase<ShutOff> ShutOffUseCase { get; }
         IHelperTableUseCase<ConstructionType> ConstructionTypeUseCase { get; }

        void ShowToolModels();
        void LoadPictureForToolModel(long id);
        void AddToolModel(ToolModel toolModel);
        void RemoveToolModels(List<ToolModel> toolModels, IToolModelGui active);
        void LoadCmCmk();
        void RequestToolModelUpdate(ToolModel oldToolModel, ToolModel newToolModel, IToolModelGui active);
        void UpdateToolModel(ToolModelDiff toolModelDiff);
        bool IsToolModelDesciptionUnique(string description);
    }


    public class ToolModelUseCase : IToolModelUseCase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ToolModelUseCase));

        private IToolModelData _dataInterface;
        private ICmCmkDataAccess _cmCmkDataAccess;
        private IToolModelGui _guiInterface;
        private ISessionInformationUserGetter _userGetter;
        private readonly INotificationManager _notificationManager;

        // Foreign UseCases
        public IManufacturerUseCase ManufacturerUseCase { get; private set; }
        public IHelperTableUseCase<ToolType> ToolTypeUseCase { get; private set; }
        public IHelperTableUseCase<DriveSize> DriveSizeUseCase { get; private set; }
        public IHelperTableUseCase<DriveType> DriveTypeUseCase { get; private set; }
        public IHelperTableUseCase<SwitchOff> SwitchOffUseCase { get; private set; }
        public IHelperTableUseCase<ShutOff> ShutOffUseCase { get; private set; }
        public IHelperTableUseCase<ConstructionType> ConstructionTypeUseCase { get; private set; }

        // Caches
        private List<string> _toolModelDescriptionChache;


        public void ShowToolModels()
        {
            try
            {
                Log.Info("LoadToolModels started");
                var toolModels = _dataInterface.LoadToolModels();
                FillToolModelDescriptionCache(toolModels);

                Log.Debug($"ShowToolModels call with List of ToolModels Size of {toolModels?.Count}");
                _guiInterface.ShowToolModels(toolModels);
            }
            catch (Exception exception)
            {
                Log.Error("Error while loading ToolModels failed with Exception", exception);
                _guiInterface.ShowLoadingErrorMessage();
            }
            Log.Info("LoadToolModels ended");
        }

        public void LoadPictureForToolModel(long id)
        {
            try
            {
                Log.Info("LoadToolModels started");
                var picture = _dataInterface.LoadPictureForToolModel(id);
                Log.Debug($"LoadPictorForToolModel: Picture for Id {id} loaded");
                _guiInterface.SetPictureForToolModel(id, picture);
            }
            catch (Exception exception)
            {
                Log.Error("Error while loading the picture for a ToolModel failed with Exception", exception);
                _guiInterface.ShowLoadingErrorMessage();
            }
            Log.Info("LoadToolModels ended");
        }

        public void AddToolModel(ToolModel toolModel)
        {
            try
            {
                Log.Info("AddToolModel started");
                // Update DescriptionCache
                _toolModelDescriptionChache?.Add(toolModel.Description?.ToDefaultString());

                _guiInterface.AddToolModel(_dataInterface.AddToolModel(toolModel, _userGetter.GetCurrentUser()));
                _notificationManager.SendSuccessNotification();
            }
            catch (EntryAlreadyExists)
            {
                Log.Error($"ToolModel with the Id {toolModel.Id} already exists");
                _guiInterface.ShowEntryAlreadyExistsMessage(toolModel);
            }
            catch (Exception exception)
            {
                Log.Error("Error while adding a ToolModel failed with Exception", exception);
                _guiInterface.ShowErrorMessage();
            }
            Log.Info("AddToolModel ended");
        }

        public void RemoveToolModels(List<ToolModel> toolModels, IToolModelGui active)
        {
            var methodname = nameof(RemoveToolModels);
            if (toolModels == null)
            {
                Log.Debug($"{nameof(methodname)} called with toolModels is null, call canceled");
                return;
            }

            if (toolModels.Count == 0)
            {
                Log.Debug($"{nameof(methodname)} called with {toolModels.Count} elements, , call canceled");
                return;
            }
            
            try
            {
                Log.Info($"{methodname} started");

                // Update DescriptionCache
                if (_toolModelDescriptionChache != null)
                {
                    foreach (var s in toolModels.Select(x => x.Description))
                    {
                        if (_toolModelDescriptionChache.Contains(s?.ToDefaultString()))
                        {
                            _toolModelDescriptionChache.Remove(s?.ToDefaultString());
                        }
                    }
                }
                foreach (var toolModel in toolModels)
                {
                    var references = _dataInterface.LoadReferencedTools(toolModel.Id.ToLong());
                    if (references != null && references.Count > 0)
                    {
                        active.ShowRemoveToolModelPreventingReferences(references);
                        return;
                    }
                }
                Log.Debug($"{nameof(_dataInterface.RemoveToolModels)} called with {toolModels.Count} elements");
                _dataInterface.RemoveToolModels(toolModels, _userGetter.GetCurrentUser());
                _guiInterface.RemoveToolModels(toolModels);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception exception)
            {
                Log.Error($"{methodname} failed with Exception", exception);
                _guiInterface.ShowRemoveToolModelsErrorMessage();
            }
            Log.Info($"{methodname} ended");
        }

        public void LoadCmCmk()
        {
            try
            {
                var cmCmk = _cmCmkDataAccess.LoadCmCmk();
                _guiInterface.ShowCmCmk(cmCmk.cm, cmCmk.cmk);
            }
            catch (Exception exception)
            {
                Log.Error("LoadCmCmk failed with Exception", exception);
                _guiInterface.ShowCmCmkError();
            }
        }

        public void RequestToolModelUpdate(ToolModel oldToolModel, ToolModel newToolModel, IToolModelGui active)
        {
            if (!oldToolModel.EqualsById(newToolModel))
            {
                _guiInterface.ShowErrorMessage();
                return;
            }

            var diff = new ToolModelDiff()
            {
                User = _userGetter.GetCurrentUser(),
                OldToolModel = oldToolModel,
                NewToolModel = newToolModel
            };

            var result = active.ShowDiffDialog(diff);

            if(result)
            {
                UpdateToolModel(diff);
            }
        }

        public void UpdateToolModel(ToolModelDiff toolModelDiff)
        {
            try
            {
                Log.Info("UpdateToolModel started");
                _guiInterface.UpdateToolModel(_dataInterface.UpdateToolModel(toolModelDiff));
                _notificationManager.SendSuccessNotification();
            }
            catch (EntryAlreadyExists)
            {
                Log.Error($"ToolModel with the Id {toolModelDiff.NewToolModel.Id} already exists");
                _guiInterface.ShowEntryAlreadyExistsMessage(toolModelDiff.NewToolModel);
            }
            catch (Exception exception)
            {
                Log.Error("Error while updating a ToolModel failed with Exception", exception);
                _guiInterface.ShowErrorMessage();
            }
            Log.Info("UpdateToolModel ended");
        }

        public bool IsToolModelDesciptionUnique(string description)
        {
            if(_toolModelDescriptionChache == null)
            {
                FillToolModelDescriptionCache(_dataInterface.LoadToolModels());
            }

            return !_toolModelDescriptionChache.Contains(description);
        }

        
        private void FillToolModelDescriptionCache(List<ToolModel> toolModels)
        {
            _toolModelDescriptionChache = toolModels.Select(x => x.Description?.ToDefaultString()).ToList();
        }



        public ToolModelUseCase(
            IToolModelData dataInterface, 
            ICmCmkDataAccess cmCmkDataAccess,
            IToolModelGui guiInterface, 
            IManufacturerUseCase manufacturerUseCase,
            IHelperTableUseCase<ToolType> toolTypeUseCase,
            IHelperTableUseCase<DriveSize> driveSizeUseCase,
            IHelperTableUseCase<DriveType> driveTypeUseCase,
            IHelperTableUseCase<SwitchOff> switchOffUseCase,
            IHelperTableUseCase<ShutOff> shutOffUseCase,
            IHelperTableUseCase<ConstructionType> constructionTypeUseCase,
            ISessionInformationUserGetter userGetter,
            INotificationManager notificationManager)
        {
            _dataInterface = dataInterface;
            _cmCmkDataAccess = cmCmkDataAccess;
            _guiInterface = guiInterface;
            _userGetter = userGetter;
            _notificationManager = notificationManager;

            ManufacturerUseCase = manufacturerUseCase;
            ToolTypeUseCase = toolTypeUseCase;
            DriveSizeUseCase = driveSizeUseCase;
            DriveTypeUseCase = driveTypeUseCase;
            SwitchOffUseCase = switchOffUseCase;
            ShutOffUseCase = shutOffUseCase;
            ConstructionTypeUseCase = constructionTypeUseCase;

            _toolModelDescriptionChache = null;
        }
    }


    public class ToolModelHumbleAsyncRunner : IToolModelUseCase
    {
        private IToolModelUseCase _real;

        public IManufacturerUseCase ManufacturerUseCase => _real.ManufacturerUseCase;
        public IHelperTableUseCase<ToolType> ToolTypeUseCase => _real.ToolTypeUseCase;
        public IHelperTableUseCase<DriveSize> DriveSizeUseCase => _real.DriveSizeUseCase;
        public IHelperTableUseCase<DriveType> DriveTypeUseCase => _real.DriveTypeUseCase;
        public IHelperTableUseCase<SwitchOff> SwitchOffUseCase => _real.SwitchOffUseCase;
        public IHelperTableUseCase<ShutOff> ShutOffUseCase => _real.ShutOffUseCase;
        public IHelperTableUseCase<ConstructionType> ConstructionTypeUseCase => _real.ConstructionTypeUseCase;

        public ToolModelHumbleAsyncRunner(IToolModelUseCase real)
        {
            _real = real;
        }

        public void AddToolModel(ToolModel toolModel)
        {
            Task.Run(() => _real.AddToolModel(toolModel));
        }

        public bool IsToolModelDesciptionUnique(string description)
        {
            return _real.IsToolModelDesciptionUnique(description);
        }

        public void LoadCmCmk()
        {
            Task.Run(() => _real.LoadCmCmk());
        }

        public void LoadPictureForToolModel(long id)
        {
            Task.Run(() => _real.LoadPictureForToolModel(id));
        }

        public void RemoveToolModels(List<ToolModel> toolModels, IToolModelGui active)
        {
            Task.Run(() => _real.RemoveToolModels(toolModels, active));
        }

        public void RequestToolModelUpdate(ToolModel oldToolModel, ToolModel newToolModel, IToolModelGui active)
        {
            Task.Run(() => _real.RequestToolModelUpdate(oldToolModel, newToolModel, active));
        }

        public void ShowToolModels()
        {
            Task.Run(() => _real.ShowToolModels());
        }

        public void UpdateToolModel(ToolModelDiff toolModelDiff)
        {
            Task.Run(() => _real.UpdateToolModel(toolModelDiff));
        }
    }
}
