using Core.Entities;
using NUnit.Framework;
using InterfaceAdapters.Models;
using TestHelper.Factories;
using TestHelper.Mock;

namespace InterfaceAdapters.Test.Models
{
    public class HelperTableItemModelTest
    {
        [Test]
        public void MapModelToToolType()
        {
            var model = HelperTableItemModel.GetModelForToolType(new ToolType()
            {
                ListId = new HelperTableEntityId(951753),
                Value = new HelperTableDescription("fdkljhsaklfhsajklf")
            });

            var entity = model.Entity;

            Assert.AreEqual(model.ListId, entity.ListId.ToLong());
            Assert.AreEqual(model.Value, entity.Value.ToDefaultString());
        }

        [Test]
        public void MapToolTypeToModel()
        {
            var entity = new ToolType()
            {
                ListId = new HelperTableEntityId(951753),
                Value = new HelperTableDescription("fdkljhsaklfhsajklf")
            };

            var model = HelperTableItemModel.GetModelForToolType(entity);

            Assert.AreEqual(entity.ListId.ToLong(), model.ListId);
            Assert.AreEqual(entity.Value.ToDefaultString(), model.Value);
        }


        [Test]
        public void MapModelToSwitchOff()
        {
            var model = HelperTableItemModel.GetModelForSwitchOff(new SwitchOff()
            {
                ListId = new HelperTableEntityId(951753),
                Value = new HelperTableDescription("fdkljhsaklfhsajklf")
            });

            var entity = model.Entity;

            Assert.AreEqual(model.ListId, entity.ListId.ToLong());
            Assert.AreEqual(model.Value, entity.Value.ToDefaultString());
        }

        [Test]
        public void MapSwitchOffToModel()
        {
            var entity = new SwitchOff()
            {
                ListId = new HelperTableEntityId(951753),
                Value = new HelperTableDescription("fdkljhsaklfhsajklf")
            };

            var model = HelperTableItemModel.GetModelForSwitchOff(entity);

            Assert.AreEqual(entity.ListId.ToLong(), model.ListId);
            Assert.AreEqual(entity.Value.ToDefaultString(), model.Value);
        }


        [Test]
        public void MapModelToDriveSize()
        {
            var model = HelperTableItemModel.GetModelForDriveSize(new DriveSize()
            {
                ListId = new HelperTableEntityId(951753),
                Value = new HelperTableDescription("fdkljhsaklfhsajklf")
            });

            var entity = model.Entity;

            Assert.AreEqual(model.ListId, entity.ListId.ToLong());
            Assert.AreEqual(model.Value, entity.Value.ToDefaultString());
        }

        [Test]
        public void MapDriveSizeToModel()
        {
            var entity = new DriveSize()
            {
                ListId = new HelperTableEntityId(951753),
                Value = new HelperTableDescription("fdkljhsaklfhsajklf")
            };

            var model = HelperTableItemModel.GetModelForDriveSize(entity);

            Assert.AreEqual(entity.ListId.ToLong(), model.ListId);
            Assert.AreEqual(entity.Value.ToDefaultString(), model.Value);
        }


        [Test]
        public void MapModelToShutOff()
        {
            var model = HelperTableItemModel.GetModelForShutOff(new ShutOff()
            {
                ListId = new HelperTableEntityId(951753),
                Value = new HelperTableDescription("fdkljhsaklfhsajklf")
            });

            var entity = model.Entity;

            Assert.AreEqual(model.ListId, entity.ListId.ToLong());
            Assert.AreEqual(model.Value, entity.Value.ToDefaultString());
        }

        [Test]
        public void MapShutOffToModel()
        {
            var entity = new ShutOff()
            {
                ListId = new HelperTableEntityId(951753),
                Value = new HelperTableDescription("fdkljhsaklfhsajklf")
            };

            var model = HelperTableItemModel.GetModelForShutOff(entity);

            Assert.AreEqual(entity.ListId.ToLong(), model.ListId);
            Assert.AreEqual(entity.Value.ToDefaultString(), model.Value);
        }


        [Test]
        public void MapModelToDriveType()
        {
            var model = HelperTableItemModel.GetModelForDriveType(new DriveType()
            {
                ListId = new HelperTableEntityId(951753),
                Value = new HelperTableDescription("fdkljhsaklfhsajklf")
            });

            var entity = model.Entity;

            Assert.AreEqual(model.ListId, entity.ListId.ToLong());
            Assert.AreEqual(model.Value, entity.Value.ToDefaultString());
        }

        [Test]
        public void MapDriveTypeToModel()
        {
            var entity = new DriveType()
            {
                ListId = new HelperTableEntityId(951753),
                Value = new HelperTableDescription("fdkljhsaklfhsajklf")
            };

            var model = HelperTableItemModel.GetModelForDriveType(entity);

            Assert.AreEqual(entity.ListId.ToLong(), model.ListId);
            Assert.AreEqual(entity.Value.ToDefaultString(), model.Value);
        }


        [Test]
        public void MapModelToConstructionType()
        {
            var model = HelperTableItemModel.GetModelForConstructionType(new ConstructionType()
            {
                ListId = new HelperTableEntityId(951753),
                Value = new HelperTableDescription("fdkljhsaklfhsajklf")
            });

            var entity = model.Entity;

            Assert.AreEqual(model.ListId, entity.ListId.ToLong());
            Assert.AreEqual(model.Value, entity.Value.ToDefaultString());
        }

        [Test]
        public void MapConstructionTypeToModel()
        {
            var entity = new ConstructionType()
            {
                ListId = new HelperTableEntityId(951753),
                Value = new HelperTableDescription("fdkljhsaklfhsajklf")
            };

            var model = HelperTableItemModel.GetModelForConstructionType(entity);

            Assert.AreEqual(entity.ListId.ToLong(), model.ListId);
            Assert.AreEqual(entity.Value.ToDefaultString(), model.Value);
        }


        [Test]
        public void MapModelToStatus()
        {
            var model = HelperTableItemModel.GetModelForStatus(new Status()
            {
                ListId = new HelperTableEntityId(951753),
                Value = new StatusDescription("fdkljhsaklfhsajklf")
            });

            var entity = model.Entity;

            Assert.AreEqual(model.ListId, entity.ListId.ToLong());
            Assert.AreEqual(model.Value, entity.Value.ToDefaultString());
        }

        [Test]
        public void MapStatusToModel()
        {
            var entity = new Status()
            {
                ListId = new HelperTableEntityId(951753),
                Value = new StatusDescription("fdkljhsaklfhsajklf")
            };

            var model = HelperTableItemModel.GetModelForStatus(entity);

            Assert.AreEqual(entity.ListId.ToLong(), model.ListId);
            Assert.AreEqual(entity.Value.ToDefaultString(), model.Value);
        }

        [Test]
        public void HelperTableItemModelComparerEqualsTest()
        {
            var comparer = new HelperTableItemModelComparer<HelperTableEntityMock, string>();

            var item1 = new HelperTableItemModel<HelperTableEntityMock, string>(CreateHelperTableEntityMock.Anonymous(), null, null, null) { ListId = 1 };
            var item2 = new HelperTableItemModel<HelperTableEntityMock, string>(CreateHelperTableEntityMock.Anonymous(), null, null, null) { ListId = 1 };

            Assert.IsTrue(comparer.Equals(item1, item2));
        }

        [Test]
        public void HelperTableItemModelComparerNotEqualsTest()
        {
            var comparer = new HelperTableItemModelComparer<HelperTableEntityMock, string>();

            var item1 = new HelperTableItemModel<HelperTableEntityMock, string>(CreateHelperTableEntityMock.Anonymous(), null, null, null) { ListId = 1 };
            var item2 = new HelperTableItemModel<HelperTableEntityMock, string>(CreateHelperTableEntityMock.Anonymous(), null, null, null) { ListId = 2 };

            Assert.IsFalse(comparer.Equals(item1, item2));
        }

        [TestCase(12, "gfhdsjkopiurijoklm")]
        [TestCase(67589430, "iusrjpfoldskfklnvlkd")]
        public void CopyHelperTableItemModelTest(long id, string value)
        {
            var model = new HelperTableItemModel<HelperTableEntityMock, string>(CreateHelperTableEntityMock.Anonymous(), 
                entity => entity.Description.ToDefaultString(), 
                (entity, val) => entity.Description = new HelperTableDescription(value), 
                () => new HelperTableEntityMock()) { ListId = id, Value = value };

            var copy = model.Copy();

            Assert.IsFalse(model == copy);
            Assert.IsTrue(model.ListId.Equals(copy.ListId));
            Assert.IsTrue(model.Value.Equals(copy.Value));
        }
    }
}
