using System.Collections.Generic;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;

namespace InterfaceAdapters
{
    public class ToolInterfaceAdapter : InterfaceAdapter<IToolGui>, IToolGui
    {
        public void AddTool(Tool newTool)
        {
            InvokeActionOnGuiInterfaces(gui => gui.AddTool(newTool));
        }

        public void ShowLoadingErrorMessage()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowLoadingErrorMessage());
        }

        public void ShowTools(List<Tool> loadTools)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowTools(loadTools));
        }

        public void ShowCommentForTool(Tool tool, string comment)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowCommentForTool(tool, comment));
        }

        public void ShowCommentForToolError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowCommentForToolError());
        }

        public void ShowPictureForTool(long toolId, Picture picture)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowPictureForTool(toolId, picture));
        }

        public void ShowToolErrorMessage()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowToolErrorMessage());
        }

        public void ShowModelsWithAtLeastOneTool(List<ToolModel> models)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowModelsWithAtLeastOneTool(models));
        }

        public void ShowRemoveToolErrorMessage()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowRemoveToolErrorMessage());
        }

        public void RemoveTool(Tool tool)
        {
            InvokeActionOnGuiInterfaces(gui => gui.RemoveTool(tool));
        }

        public void UpdateTool(Tool updateTool)
        {
            InvokeActionOnGuiInterfaces(gui => gui.UpdateTool(updateTool));
        }

        public void ShowEntryAlreadyExistsMessage(Tool diffNewTool)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowEntryAlreadyExistsMessage(diffNewTool));
        }

        public void ToolAlreadyExists()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ToolAlreadyExists());
        }

        public void ShowRemoveToolPreventingReferences(List<LocationToolAssignmentReferenceLink> references)
        {
            //Intentionally empty gets called by active parameter in UseCase
        }

    }
}
