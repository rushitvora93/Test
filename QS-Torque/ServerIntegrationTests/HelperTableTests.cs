using System;
using Core;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.Formatter;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using State;

namespace ServerIntegrationTests
{
    [TestClass]
    public class HelperTableTests
    {
        private readonly TestSetup _testSetup;

        public HelperTableTests()
        {
            _testSetup = new TestSetup();
        }

   
        [TestMethod]
        public void LoadItems()
        {
            var name = "ht_" + DateTime.Now.Ticks;

            CheckLoadItems(GetReasonForToolChangeDataAccess(), new ReasonForToolChange() { Value = new HelperTableDescription(name) });
            CheckLoadItems(GetConfigurableFieldDataAccess(), new ConfigurableField() { Value = new HelperTableDescription(name) });
            CheckLoadItems(GetConstructionTypeDataAccess(), new ConstructionType() { Value = new HelperTableDescription(name) });
            CheckLoadItems(GetCostCenterDataAccess(), new CostCenter() { Value = new HelperTableDescription(name) });
            CheckLoadItems(GetDriveSizeDataAccess(), new DriveSize() { Value = new HelperTableDescription(name) });
            CheckLoadItems(GetDriveTypeDataAccess(), new DriveType() { Value = new HelperTableDescription(name) });
            CheckLoadItems(GetShutOffDataAccess(), new ShutOff() { Value = new HelperTableDescription(name) });
            CheckLoadItems(GetSwitchOffDataAccess(), new SwitchOff() { Value = new HelperTableDescription(name) });
            CheckLoadItems(GetToolTypeDataAccess(), new ToolType() { Value = new HelperTableDescription(name) });
        }

        [TestMethod]
        public void AddItem()
        {
            var name = "ht_" + DateTime.Now.Ticks;

            CheckAddItem(GetReasonForToolChangeDataAccess(), new ReasonForToolChange() { Value = new HelperTableDescription(name) });
            CheckAddItem(GetConfigurableFieldDataAccess(), new ConfigurableField() { Value = new HelperTableDescription(name) });
            CheckAddItem(GetConstructionTypeDataAccess(), new ConstructionType() { Value = new HelperTableDescription(name) });
            CheckAddItem(GetCostCenterDataAccess(), new CostCenter() { Value = new HelperTableDescription(name) });
            CheckAddItem(GetDriveSizeDataAccess(), new DriveSize() { Value = new HelperTableDescription(name) });
            CheckAddItem(GetDriveTypeDataAccess(), new DriveType() { Value = new HelperTableDescription(name) });
            CheckAddItem(GetShutOffDataAccess(), new ShutOff() { Value = new HelperTableDescription(name) });
            CheckAddItem(GetSwitchOffDataAccess(), new SwitchOff() { Value = new HelperTableDescription(name) });
            CheckAddItem(GetToolTypeDataAccess(), new ToolType() { Value = new HelperTableDescription(name) });
        }

        [TestMethod]
        public void SaveItem()
        {
            var name = "ht_" + DateTime.Now.Ticks;

            CheckSaveItem(GetReasonForToolChangeDataAccess(), new ReasonForToolChange() { Value = new HelperTableDescription(name) });
            CheckSaveItem(GetConfigurableFieldDataAccess(), new ConfigurableField() { Value = new HelperTableDescription(name) });
            CheckSaveItem(GetConstructionTypeDataAccess(), new ConstructionType() { Value = new HelperTableDescription(name) });
            CheckSaveItem(GetCostCenterDataAccess(), new CostCenter() { Value = new HelperTableDescription(name) });
            CheckSaveItem(GetDriveSizeDataAccess(), new DriveSize() { Value = new HelperTableDescription(name) });
            CheckSaveItem(GetDriveTypeDataAccess(), new DriveType() { Value = new HelperTableDescription(name) });
            CheckSaveItem(GetShutOffDataAccess(), new ShutOff() { Value = new HelperTableDescription(name) });
            CheckSaveItem(GetSwitchOffDataAccess(), new SwitchOff() { Value = new HelperTableDescription(name) });
            CheckSaveItem(GetToolTypeDataAccess(), new ToolType() { Value = new HelperTableDescription(name) });
        }

        [TestMethod]
        public void RemoveItem()
        {
            var name = "ht_" + DateTime.Now.Ticks;

            CheckRemoveItem(GetReasonForToolChangeDataAccess(), new ReasonForToolChange() { Value = new HelperTableDescription(name) });
            CheckRemoveItem(GetConfigurableFieldDataAccess(), new ConfigurableField() { Value = new HelperTableDescription(name) });
            CheckRemoveItem(GetConstructionTypeDataAccess(), new ConstructionType() { Value = new HelperTableDescription(name) });
            CheckRemoveItem(GetCostCenterDataAccess(), new CostCenter() { Value = new HelperTableDescription(name) });
            CheckRemoveItem(GetDriveSizeDataAccess(), new DriveSize() { Value = new HelperTableDescription(name) });
            CheckRemoveItem(GetDriveTypeDataAccess(), new DriveType() { Value = new HelperTableDescription(name) });
            CheckRemoveItem(GetShutOffDataAccess(), new ShutOff() { Value = new HelperTableDescription(name) });
            CheckRemoveItem(GetSwitchOffDataAccess(), new SwitchOff() { Value = new HelperTableDescription(name) });
            CheckRemoveItem(GetToolTypeDataAccess(), new ToolType() { Value = new HelperTableDescription(name) });
        }

        [TestMethod]
        public void LoadReferencedToolModels()
        {
            //TODO: Complete the test, as soon AddToolModel is implemented with grpc
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void LoadToolReferenceLinks()
        {
            var name = "ht_" + DateTime.Now.Ticks;

            var costCenterDataAccess = GetCostCenterDataAccess();
            var newCostCenter = AddNewHelperTableToDataAccess(costCenterDataAccess, new CostCenter() { Value = new HelperTableDescription(name) });

            var configurableFieldDataAccess = GetConfigurableFieldDataAccess();
            var newConfigurableField = AddNewHelperTableToDataAccess(configurableFieldDataAccess, new ConfigurableField() { Value = new HelperTableDescription(name) });

            var tool = TestDataCreator.CreateTool(_testSetup, "t_" + DateTime.Now.Ticks, newCostCenter, newConfigurableField);
            var toolReferenceCostCenter = costCenterDataAccess.LoadToolReferenceLinks(newCostCenter.ListId);
            var toolReferenceConfigurableField = configurableFieldDataAccess.LoadToolReferenceLinks(newConfigurableField.ListId);

            ToolTest.CheckToolReferenceLink(toolReferenceCostCenter, tool);
            ToolTest.CheckToolReferenceLink(toolReferenceConfigurableField, tool);
        }

        private void CheckLoadItems<T>(IHelperTableData<T> dataAccess, T data) where T : IStandardHelperTable, IQstEquality<T>
        {
            var newEntity = AddNewHelperTableToDataAccess(dataAccess, data);
            var items = dataAccess.LoadItems();
            var result = items.Find(x => x.ListId.ToLong() == newEntity.ListId.ToLong());

            Assert.IsTrue(newEntity.EqualsByContent(result));
        }

        private void CheckAddItem<T>(IHelperTableData<T> dataAccess, T data) where T : IStandardHelperTable, IQstEquality<T>
        {
            var newEntity = AddNewHelperTableToDataAccess(dataAccess, data);
            var items = dataAccess.LoadItems();
            var result = items.Find(x => x.ListId.ToLong() == newEntity.ListId.ToLong());

            Assert.IsTrue(newEntity.ListId.ToLong() != 0);
            Assert.IsTrue(newEntity.EqualsByContent(result));
        }

        private void CheckSaveItem<T>(IHelperTableData<T> dataAccess, T data) where T : IStandardHelperTable, IQstEquality<T>, ICopy<T>
        {
            var newEntity = AddNewHelperTableToDataAccess(dataAccess, data);
            var changedData = newEntity.CopyDeep();
            changedData.Value = new HelperTableDescription(data.Value.ToDefaultString() + "_X");

            var items = dataAccess.LoadItems();
            var result = items.Find(x => x.ListId.ToLong() == newEntity.ListId.ToLong());
            Assert.IsTrue(newEntity.EqualsByContent(result));

            dataAccess.SaveItem(data, changedData, _testSetup.TestUser);

            items = dataAccess.LoadItems();
            result = items.Find(x => x.ListId.ToLong() == newEntity.ListId.ToLong());
            Assert.IsTrue(changedData.EqualsByContent(result));
        }

        private void CheckRemoveItem<T>(IHelperTableData<T> dataAccess, T data) where T : IStandardHelperTable, IQstEquality<T>
        {
            var newEntity = AddNewHelperTableToDataAccess(dataAccess, data);
            var items = dataAccess.LoadItems();
            var result = items.Find(x => x.ListId.ToLong() == newEntity.ListId.ToLong());

            Assert.IsNotNull(result);

            dataAccess.RemoveItem(data, _testSetup.TestUser);

            items = dataAccess.LoadItems();
            result = items.Find(x => x.ListId.ToLong() == newEntity.ListId.ToLong());
            Assert.IsNull(result);
        }

        private T AddNewHelperTableToDataAccess<T>(IHelperTableData<T> dataAccess, T data) where T : IStandardHelperTable
        {
            var items = dataAccess.LoadItems();
            var result = items.Find(x => x.Value.ToDefaultString() == data.Value.ToDefaultString());
            Assert.IsNull(result);

            var addedItem = dataAccess.AddItem(data, _testSetup.TestUser);
            data.ListId = new HelperTableEntityId(addedItem.ToLong());
            return data;
        }

        private IHelperTableData<ReasonForToolChange> GetReasonForToolChangeDataAccess()
        {
            return new HelperTableDataAccess<ReasonForToolChange>(
                NodeId.ReasonForToolChange,
                new LambdaHelperTableEntitySupport<ReasonForToolChange>(
                    () => new ReasonForToolChange(),
                    (i, m) => m.DirectPropertyMapping(i),
                    (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                false,
                false,
                new HelperTableToToolModelReferenceDoesNotExist(),
                _testSetup.ClientFactory);
        }

        private IHelperTableData<ShutOff> GetShutOffDataAccess()
        {
            return new HelperTableDataAccess<ShutOff>(
                NodeId.ShutOff,
                new LambdaHelperTableEntitySupport<ShutOff>(
                    () => new ShutOff(),
                    (i, m) => m.DirectPropertyMapping(i),
                    (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                true,
                false,
                new HelperTableToToolModelReferenceLoader(_testSetup.ClientFactory, NodeId.ShutOff),
                _testSetup.ClientFactory);
        }

        private IHelperTableData<SwitchOff> GetSwitchOffDataAccess()
        {
            return new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableDataAccess<SwitchOff>(
                NodeId.SwitchOff,
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LambdaHelperTableEntitySupport<Core.Entities.SwitchOff>(
                    () => new Core.Entities.SwitchOff(),
                    (i, m) => m.DirectPropertyMapping(i),
                    (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                true,
                false,
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableToToolModelReferenceLoader(_testSetup.ClientFactory, NodeId.SwitchOff),
                _testSetup.ClientFactory);
        }

        private IHelperTableData<DriveSize> GetDriveSizeDataAccess()
        {
            return new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableDataAccess<DriveSize>(
                NodeId.DriveSize,
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LambdaHelperTableEntitySupport<Core.Entities.DriveSize>(
                    () => new Core.Entities.DriveSize(),
                    (i, m) => m.DirectPropertyMapping(i),
                    (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                true,
                false,
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableToToolModelReferenceLoader(_testSetup.ClientFactory, NodeId.DriveSize),
                _testSetup.ClientFactory);
        }

        private IHelperTableData<DriveType> GetDriveTypeDataAccess()
        {
            return new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableDataAccess<DriveType>(
                NodeId.DriveType,
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LambdaHelperTableEntitySupport<
                    Core.Entities.DriveType>(
                    () => new Core.Entities.DriveType(),
                    (i, m) => m.DirectPropertyMapping(i),
                    (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                true,
                false,
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableToToolModelReferenceLoader(
                    _testSetup.ClientFactory, NodeId.DriveType),
                _testSetup.ClientFactory);
        }

        private IHelperTableData<ToolType> GetToolTypeDataAccess()
        {
            return new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableDataAccess<ToolType>(
                NodeId.ToolType,
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LambdaHelperTableEntitySupport<Core.Entities.ToolType>(
                    () => new Core.Entities.ToolType(),
                    (i, m) => m.DirectPropertyMapping(i),
                    (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                true,
                false,
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableToToolModelReferenceLoader(_testSetup.ClientFactory, NodeId.ToolType),
                _testSetup.ClientFactory);
        }

        private IHelperTableData<ConstructionType> GetConstructionTypeDataAccess()
        {
            return new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableDataAccess<ConstructionType>(
                NodeId.ConstructionType,
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LambdaHelperTableEntitySupport<Core.Entities.ConstructionType>(
                    () => new Core.Entities.ConstructionType(),
                    (i, m) => m.DirectPropertyMapping(i),
                    (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                true,
                false,
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableToToolModelReferenceLoader(_testSetup.ClientFactory, NodeId.ConstructionType),
                _testSetup.ClientFactory);
        }

        private IHelperTableData<ConfigurableField> GetConfigurableFieldDataAccess()
        {
            return new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableDataAccess<ConfigurableField>(
                NodeId.ConfigurableField,
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LambdaHelperTableEntitySupport<Core.Entities.ConfigurableField>(
                    () => new Core.Entities.ConfigurableField(),
                    (i, m) => m.DirectPropertyMapping(i),
                    (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                false,
                true,
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableToToolReferenceLoader(_testSetup.ClientFactory, NodeId.ConfigurableField, new DefaultFormatter()),
                _testSetup.ClientFactory);
        }

        private IHelperTableData<CostCenter> GetCostCenterDataAccess()
        {
            return new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableDataAccess<CostCenter>(
                NodeId.CostCenter,
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LambdaHelperTableEntitySupport<Core.Entities.CostCenter>(
                    () => new Core.Entities.CostCenter(),
                    (i, m) => m.DirectPropertyMapping(i),
                    (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                false,
                true,
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableToToolReferenceLoader(_testSetup.ClientFactory, NodeId.CostCenter, new DefaultFormatter()),
                _testSetup.ClientFactory);
        }
    }
}
