using System.Threading.Tasks;
using Core;
using Core.Entities;
using Core.UseCases;

namespace TestHelper.Mock
{
    public class ToolUseCaseMock : ToolUseCase
    {
        public ToolUseCaseMock(IToolData dataInterface, IToolGui guiInterface, IToolModelPictureData pictureData, ToolModelUseCase toolModelUseCase, IHelperTableUseCase<ToolType> toolTypeUseCase, IHelperTableUseCase<Status> statusUseCase, IHelperTableUseCase<CostCenter> costCenterUseCase, IHelperTableUseCase<ConfigurableField> configurableFieldUseCase, ISessionInformationUserGetter userGetter, INotificationManager notificationManager) : base(dataInterface, guiInterface, pictureData, toolModelUseCase, toolTypeUseCase, statusUseCase, costCenterUseCase, configurableFieldUseCase, userGetter, notificationManager)
        {
        }

        public TaskCompletionSource<bool> LoadToolsForModelCalled = new TaskCompletionSource<bool>();
        public ToolModel LoadToolsForModelParameterToolModel { get; set; }
        public bool WasUpdateToolCalled { get; set; }
        public ToolDiff UpdateToolParameter { get; set; }
        public bool WasLoadModelsWithAtLeastOneToolCalled { get; set; } = false;
        public Tool RemoveToolParameter { get; set; }
        public Tool LoadCommentForToolParameter { get; set; }
        public bool LoadCommentForToolExecuted { get; set; }

        public override void LoadToolsForModel(ToolModel toolModel)
        {
            LoadToolsForModelParameterToolModel = toolModel;
            LoadToolsForModelCalled.SetResult(true);
        }

        public override void UpdateTool(ToolDiff diff, bool withSuccessNotification)
        {
            UpdateToolParameter = diff;
            WasUpdateToolCalled = true;
        }

        public override void LoadModelsWithAtLeastOneTool()
        {
            WasLoadModelsWithAtLeastOneToolCalled = true;
        }

        public override void LoadCommentForTool(Tool tool)
        {
            LoadCommentForToolParameter = tool;
            LoadCommentForToolExecuted = true;
        }

        public override void RemoveTool(Tool tool, IToolGui active)
        {
            RemoveToolParameter = tool;
        }
    }
}
