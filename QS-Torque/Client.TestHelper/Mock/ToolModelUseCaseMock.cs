using System;
using System.Collections.Generic;
using Core.Entities;
using Core.UseCases;

namespace TestHelper.Mock
{
    public class ToolModelUseCaseMock : IToolModelUseCase
    {
        public ToolModelUseCaseMock(IManufacturerUseCase manufacturerUseCase, IHelperTableUseCase<ToolType> toolTypeUseCase, IHelperTableUseCase<DriveSize> driveSizeUseCase, IHelperTableUseCase<DriveType> driveTypeUseCase, IHelperTableUseCase<SwitchOff> switchOffUseCase, IHelperTableUseCase<ShutOff> shutOffUseCase, IHelperTableUseCase<ConstructionType> constructionTypeUseCase)
        {
            ManufacturerUseCase = manufacturerUseCase;
            ToolTypeUseCase = toolTypeUseCase;
            DriveSizeUseCase = driveSizeUseCase;
            DriveTypeUseCase = driveTypeUseCase;
            SwitchOffUseCase = switchOffUseCase;
            ShutOffUseCase = shutOffUseCase;
            ConstructionTypeUseCase = constructionTypeUseCase;
        }

        public IManufacturerUseCase ManufacturerUseCase { get; }
        public IHelperTableUseCase<ToolType> ToolTypeUseCase { get; }
        public IHelperTableUseCase<DriveSize> DriveSizeUseCase { get; }
        public IHelperTableUseCase<DriveType> DriveTypeUseCase { get; }
        public IHelperTableUseCase<SwitchOff> SwitchOffUseCase { get; }
        public IHelperTableUseCase<ShutOff> ShutOffUseCase { get; }
        public IHelperTableUseCase<ConstructionType> ConstructionTypeUseCase { get; }
        public void ShowToolModels()
        {
            throw new NotImplementedException();
        }

        public void LoadPictureForToolModel(long id)
        {
            
        }

        public void AddToolModel(ToolModel toolModel)
        {
            throw new NotImplementedException();
        }

        public List<ToolModel> RemoveToolModelsParameter { get; set; }
        public void RemoveToolModels(List<ToolModel> toolModels, IToolModelGui active)
        {
            RemoveToolModelsParameter = toolModels;
        }
        
        public void LoadCmCmk()
        {
            
        }

        public void RequestToolModelUpdate(ToolModel oldToolModel, ToolModel newToolModel, IToolModelGui active)
        {
            throw new NotImplementedException();
        }

        public void UpdateToolModel(ToolModelDiff toolModelDiff)
        {
            throw new NotImplementedException();
        }

        public bool IsToolModelDesciptionUnique(string description)
        {
            throw new NotImplementedException();
        }
    }
}
