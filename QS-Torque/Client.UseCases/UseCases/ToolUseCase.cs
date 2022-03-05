using Core.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.ReferenceLink;

namespace Core.UseCases
{
    public interface IToolData
    {
        Tool AddTool(Tool newTool, User byUser);
        IEnumerable<List<Tool>> LoadTools();
        string LoadComment(Tool tool);
        Picture LoadPictureForTool(Tool tool);
        List<ToolModel> LoadModelsWithAtLeasOneTool();
        List<Tool> LoadToolsForModel(ToolModel toolModel);
        bool IsSerialNumberUnique(string serialNumber);
        bool IsInventoryNumberUnique(string inventoryNumber);
        void RemoveTool(Tool tool, User byUser);
        Tool UpdateTool(ToolDiff diff);
        List<LocationToolAssignmentReferenceLink> LoadLocationToolAssignmentLinksForToolId(ToolId toolId);
    }

    public interface IToolGui
    {
        void AddTool(Tool newTool);
        void ShowLoadingErrorMessage();
        void ShowTools(List<Tool> loadTools);
        void ShowCommentForTool(Tool tool, string comment);
        void ShowCommentForToolError();
        void ShowPictureForTool(long toolId, Picture picture);
        void ShowToolErrorMessage();
        void ShowModelsWithAtLeastOneTool(List<ToolModel> models);
        void ShowRemoveToolErrorMessage();
        void RemoveTool(Tool tool);
        void UpdateTool(Tool updateTool);
        void ShowEntryAlreadyExistsMessage(Tool diffNewTool);
        void ToolAlreadyExists();
        void ShowRemoveToolPreventingReferences(List<LocationToolAssignmentReferenceLink> references);
    }

    public interface IToolModelPictureData
    {
        Picture LoadPictureForToolModel(long toolModelId);
    }

    public interface IToolUseCase
    {
        IHelperTableUseCase<ToolType> ToolTypeUseCase { get; }
        IToolModelUseCase ToolModelUseCase { get; }
        IHelperTableUseCase<Status> StatusUseCase { get; }
        IHelperTableUseCase<CostCenter> CostCenterUseCase { get; }
        IHelperTableUseCase<ConfigurableField> ConfigurableFieldUseCase { get; }
        ISessionInformationUserGetter UserGetter { get; }
        
        void LoadModelsWithAtLeastOneTool();
        void LoadCommentForTool(Tool tool);
        void LoadPictureForTool(Tool tool);
        void AddTool(Tool newTool);
        void LoadToolsForModel(ToolModel toolModel);
        void RemoveTool(Tool tool, IToolGui active);
        bool IsSerialNumberUnique(string serialNumber);
        bool IsInventoryNumberUnique(string inventoryNumber);
        void UpdateTool(ToolDiff diff, bool withSuccessNotification = true);
    }


    public class ToolUseCase : IToolUseCase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ToolUseCase));

        private IToolData _dataInterface;
        private IToolGui _guiInterface;
        private IToolModelPictureData _toolModelPictureInterface;

        // Foreign UseCases
        public IHelperTableUseCase<ToolType> ToolTypeUseCase { get; private set; }
        public IToolModelUseCase ToolModelUseCase { get; private set; }
        public IHelperTableUseCase<Status> StatusUseCase { get; private set; }
        public IHelperTableUseCase<CostCenter> CostCenterUseCase { get; private set; }
        public IHelperTableUseCase<ConfigurableField> ConfigurableFieldUseCase { get; private set; }
        public ISessionInformationUserGetter UserGetter { get; private set; }

        private readonly INotificationManager _notificationManager;


        public virtual void LoadModelsWithAtLeastOneTool()
        {
            Log.Info("LoadModelsWithAtLeastOneTool started");
            Log.Debug($"LoadModelsWithAtLeastOneTool called");
            try
            {

                var models = _dataInterface.LoadModelsWithAtLeasOneTool();
                _guiInterface.ShowModelsWithAtLeastOneTool(models);
            }
            catch (Exception exception)
            {
                Log.Error($"Error while LoadModelsWithAtLeastOneTool", exception);
                _guiInterface.ShowLoadingErrorMessage();
            }
            Log.Info("LoadModelsWithAtLeastOneTool ended");
        }

        public virtual void LoadCommentForTool(Tool tool)
        {
            Log.Info("LoadCommentForTool started");
            Log.Debug($"LoadCommentForTool called with Tool Serialno:{tool?.SerialNumber} and Id:{tool?.Id}");
            try
            {
                var comment = _dataInterface.LoadComment(tool);
                Log.Debug($"ShowComment call with Tool Serialno:{tool?.SerialNumber} and Id:{tool?.Id} and Comment {comment}");
                _guiInterface.ShowCommentForTool(tool, comment);
            }
            catch (Exception exception)
            {
                Log.Error($"Error while Loading Comment for Tool Serialno:{tool?.SerialNumber} and Id:{tool?.Id}", exception);
                _guiInterface.ShowCommentForToolError();
            }
            Log.Info("LoadCommentForTool ended");
        }

        public void LoadPictureForTool(Tool tool)
        {
            try
            {
                Picture toolPicture = null;
                if (tool?.Id?.ToLong() != null)
                {
                    toolPicture = _dataInterface.LoadPictureForTool(tool);
                }

                if (toolPicture == null && tool?.ToolModel?.Id?.ToLong() != null)
                {
                    toolPicture = _toolModelPictureInterface.LoadPictureForToolModel(tool.ToolModel.Id.ToLong());
                }

                _guiInterface.ShowPictureForTool(tool.Id.ToLong(), toolPicture);
            }
            catch (Exception exception)
            {
                Log.Error($"Error while Loading Picture for Tool Serialno:{tool?.SerialNumber} and Id:{tool?.Id}", exception);
                _guiInterface.ShowToolErrorMessage();
            }
        }

        public void AddTool(Tool newTool)
        {
            try
            {
                _guiInterface.AddTool(_dataInterface.AddTool(newTool, UserGetter.GetCurrentUser()));
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception exception)
            {
                Log.Error("Error in AddToolModel", exception);
                _guiInterface.ShowToolErrorMessage();
            }
            Log.Info("AddManufacturer ended");
        }

        public virtual void LoadToolsForModel(ToolModel toolModel)
        {
            try
            {
                Log.Info("LoadToolsForModel started");
                Log.Debug(
                    $"LoadToolsForModel called with ToolModel description {toolModel?.Description} and Id:{toolModel?.Id}");
                var tools = _dataInterface.LoadToolsForModel(toolModel);
                _guiInterface.ShowTools(tools);
            }
            catch (Exception e)
            {
                Log.Error("LoadToolsForModel error", e);
                _guiInterface.ShowLoadingErrorMessage();
            }
        }

        public virtual void RemoveTool(Tool tool, IToolGui active)
        {
            try
            {
                Log.Info($"Remove Tool started");
                var references = _dataInterface.LoadLocationToolAssignmentLinksForToolId(tool.Id);
                if (references != null && references.Count > 0)
                {
                    active.ShowRemoveToolPreventingReferences(references);
                    return;
                }
                Log.Debug($"RemoveTool called with tool id{tool?.Id?.ToLong()}");
                _dataInterface.RemoveTool(tool, UserGetter.GetCurrentUser());
                _guiInterface.RemoveTool(tool);
                _notificationManager.SendSuccessNotification();
            }
            catch (Exception exception)
            {
                Log.Error("RemoveTool error", exception);
                _guiInterface.ShowRemoveToolErrorMessage();
            }
        }


        public bool IsSerialNumberUnique(string serialNumber)
        {
            return _dataInterface.IsSerialNumberUnique(serialNumber);
        }

        public bool IsInventoryNumberUnique(string inventoryNumber)
        {
            return _dataInterface.IsInventoryNumberUnique(inventoryNumber);
        }


        public ToolUseCase(IToolData dataInterface,
                            IToolGui guiInterface,
                            IToolModelPictureData toolModelPictureInterface,
                            IToolModelUseCase toolModelUseCase,
                            IHelperTableUseCase<ToolType> toolTypeUseCase,
                            IHelperTableUseCase<Status> statusUseCase,
                            IHelperTableUseCase<CostCenter> costCenterUseCase,
                            IHelperTableUseCase<ConfigurableField> configurableFieldUseCase,
                            ISessionInformationUserGetter userGetter,
                            INotificationManager notificationManager)
        {
            _dataInterface = dataInterface;
            _guiInterface = guiInterface;
            _toolModelPictureInterface = toolModelPictureInterface;
            ToolTypeUseCase = toolTypeUseCase;
            ToolModelUseCase = toolModelUseCase;
            StatusUseCase = statusUseCase;
            CostCenterUseCase = costCenterUseCase;
            ConfigurableFieldUseCase = configurableFieldUseCase;
            UserGetter = userGetter;
            _notificationManager = notificationManager;
        }

        public virtual void UpdateTool(ToolDiff diff, bool withSuccessNotification = true)
        {
            try
            {
                Log.Info("UpdateToolModel started");

                if ((!IsInventoryNumberUnique(diff.NewTool.InventoryNumber?.ToDefaultString()) && diff.OldTool.InventoryNumber?.ToDefaultString() != diff.NewTool.InventoryNumber?.ToDefaultString()) 
                    || (!IsSerialNumberUnique(diff.NewTool.SerialNumber?.ToDefaultString()) && diff.OldTool.SerialNumber?.ToDefaultString() != diff.NewTool.SerialNumber?.ToDefaultString()))
                {
                    _guiInterface.ToolAlreadyExists();
                    return;
                }

                diff.User = UserGetter.GetCurrentUser();
                _guiInterface.UpdateTool(_dataInterface.UpdateTool(diff));

                if (withSuccessNotification)
                {
                    _notificationManager.SendSuccessNotification();
                }
            }
            catch (EntryAlreadyExists error)
            {
                Log.Error($"Tool with Id: {diff?.NewTool?.Id} alerady exists", error);
                _guiInterface.ShowEntryAlreadyExistsMessage(diff.NewTool);
            }
            catch (Exception exception)
            {
                Log.Error("Error while updating a Tool failed with Exception", exception);
                _guiInterface.ShowToolErrorMessage();
            }
            Log.Info("UpdateTool ended");
        }
    }


    public class ToolHumbleAsyncRunner : IToolUseCase
    {
        private IToolUseCase _real;

        public IHelperTableUseCase<ToolType> ToolTypeUseCase => _real.ToolTypeUseCase;
        public IToolModelUseCase ToolModelUseCase => _real.ToolModelUseCase;
        public IHelperTableUseCase<Status> StatusUseCase => _real.StatusUseCase;
        public IHelperTableUseCase<CostCenter> CostCenterUseCase => _real.CostCenterUseCase;
        public IHelperTableUseCase<ConfigurableField> ConfigurableFieldUseCase => _real.ConfigurableFieldUseCase;
        public ISessionInformationUserGetter UserGetter => _real.UserGetter;

        public ToolHumbleAsyncRunner(IToolUseCase real)
        {
            _real = real;
        }

        public void AddTool(Tool newTool)
        {
            Task.Run(() => _real.AddTool(newTool));
        }

        public bool IsInventoryNumberUnique(string inventoryNumber)
        {
            return _real.IsInventoryNumberUnique(inventoryNumber);
        }

        public bool IsSerialNumberUnique(string serialNumber)
        {
            return _real.IsSerialNumberUnique(serialNumber);
        }

        public void LoadCommentForTool(Tool tool)
        {
            Task.Run(() => _real.LoadCommentForTool(tool));
        }

        public void LoadModelsWithAtLeastOneTool()
        {
            Task.Run(() => _real.LoadModelsWithAtLeastOneTool());
        }

        public void LoadPictureForTool(Tool tool)
        {
            Task.Run(() => _real.LoadPictureForTool(tool));
        }

        public void LoadToolsForModel(ToolModel toolModel)
        {
            Task.Run(() => _real.LoadToolsForModel(toolModel));
        }

        public void RemoveTool(Tool tool, IToolGui active)
        {
            Task.Run(() => _real.RemoveTool(tool, active));
        }

        public void UpdateTool(ToolDiff diff, bool withSuccessNotification = true)
        {
            Task.Run(() => _real.UpdateTool(diff, withSuccessNotification));
        }
    }
}
