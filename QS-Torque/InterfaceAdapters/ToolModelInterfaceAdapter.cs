using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using ToolModel = Core.Entities.ToolModel;

namespace InterfaceAdapters
{
    public class ToolModelInterfaceAdapter : InterfaceAdapter<IToolModelGui>, IToolModelGui
    {
        public void RemoveToolModels(List<ToolModel> toolModels)
        {
            InvokeActionOnGuiInterfaces(gui => gui.RemoveToolModels(toolModels));
        }

        public void ShowCmCmk(double cm, double cmk)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowCmCmk(cm, cmk));
        }

        public void ShowCmCmkError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowCmCmkError());
        }

        public void SetPictureForToolModel(long toolModelId, Picture picture)
		{
            InvokeActionOnGuiInterfaces(gui => gui.SetPictureForToolModel(toolModelId, picture));
        }

		public void ShowLoadingErrorMessage()
		{
            InvokeActionOnGuiInterfaces(gui => gui.ShowLoadingErrorMessage());
        }

        public void ShowRemoveToolModelsErrorMessage()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowRemoveToolModelsErrorMessage());
        }

        public void ShowToolModels(List<ToolModel> toolModels)
		{
            InvokeActionOnGuiInterfaces(gui => gui.ShowToolModels(toolModels));
        }

        public void AddToolModel(ToolModel toolModel)
        {
            InvokeActionOnGuiInterfaces(gui => gui.AddToolModel(toolModel));
        }

        public void UpdateToolModel(ToolModel toolModel)
        {
            InvokeActionOnGuiInterfaces(gui => gui.UpdateToolModel(toolModel));
        }

        public void ShowEntryAlreadyExistsMessage(ToolModel toolModel)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowEntryAlreadyExistsMessage(toolModel));
        }

        public void ShowErrorMessage()
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowErrorMessage());
        }

        public void ShowRemoveToolModelPreventingReferences(List<ToolReferenceLink> references)
        {
            //Intentionally empty gets called by active parameter in usecase
        }

        public bool ShowDiffDialog(ToolModelDiff diff)
        {
            throw new InvalidOperationException("The method ShowDiffDialog has to be invoked direktly on a ViewModel");
        }
    }
}
