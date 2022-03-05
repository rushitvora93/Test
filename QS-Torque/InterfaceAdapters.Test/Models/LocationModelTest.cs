using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using InterfaceAdapters.Models;
using NUnit.Framework;
using TestHelper.Factories;

namespace InterfaceAdapters.Test.Models
{
    public class LocationModelTest
    {
        private class LocationMock : Location
        {
            public bool WasUpdateToleranceLimitsCalled = false;

            public override void UpdateToleranceLimits()
            {
                WasUpdateToleranceLimitsCalled = true;
            }
        }


        [Test]
        public void MapEntityToModelTest()
        {
            var location = new Location
            {
                Id = new LocationId(123),
                Number = new LocationNumber("Test"),
                Description = new LocationDescription("TestDescription"),
                ParentDirectoryId = new LocationDirectoryId(15),
                ControlledBy = LocationControlledBy.Angle,
                SetPointTorque = Torque.FromNm(15.15),
                ToleranceClassTorque = CreateParameterizedToleranceClass(15),
                MinimumTorque = Torque.FromNm(15.15),
                MaximumTorque = Torque.FromNm(15.15),
                ThresholdTorque = Torque.FromNm(15.15),
                ToleranceClassAngle = CreateParameterizedToleranceClass(15),
                SetPointAngle = Angle.FromDegree(15.15),
                MinimumAngle = Angle.FromDegree(15.15),
                MaximumAngle = Angle.FromDegree(15.15),
                ConfigurableField1 = new LocationConfigurableField1("TestConfField1"),
                ConfigurableField2 = new LocationConfigurableField2("T"),
                ConfigurableField3 = false,
                Comment = "34ß50t69z8uhgfiodew09tizuhjgk"
            };

            var model = LocationModel.GetModelFor(location, new NullLocalizationWrapper(), null);
            Assert.AreEqual(location.Id.ToLong(), model.Id);
            Assert.AreEqual(location.Number.ToDefaultString(), model.Number);
            Assert.AreEqual(location.Description.ToDefaultString(), model.Description);
            Assert.AreEqual(location.ParentDirectoryId.ToLong(), model.ParentId);
            Assert.AreEqual(location.ControlledBy, model.ControlledBy);
            Assert.AreEqual(location.SetPointTorque.Nm, model.SetPointTorque);
            Assert.AreEqual(location.ToleranceClassTorque.Id.ToLong(), model.ToleranceClassTorque.Id);
            Assert.AreEqual(location.MinimumTorque.Nm, model.MinimumTorque);
            Assert.AreEqual(location.MaximumTorque.Nm, model.MaximumTorque);
            Assert.AreEqual(location.ThresholdTorque.Nm, model.ThresholdTorque);
            Assert.AreEqual(location.ToleranceClassAngle.Id.ToLong(), model.ToleranceClassAngle.Id);
            Assert.AreEqual(location.SetPointAngle.Degree, model.SetPointAngle);
            Assert.AreEqual(location.MinimumAngle.Degree, model.MinimumAngle);
            Assert.AreEqual(location.MaximumAngle.Degree, model.MaximumAngle);
            Assert.AreEqual(location.ConfigurableField1.ToDefaultString(), model.ConfigurableField1);
            Assert.AreEqual(location.ConfigurableField2.ToDefaultString(), model.ConfigurableField2);
            Assert.AreEqual(location.ConfigurableField3, model.ConfigurableField3);
            Assert.AreEqual(location.Comment, model.Comment);
        }

        [Test]
        public void MapModelToEntityTest()
        {
            var model = new LocationModel(new Location(), new NullLocalizationWrapper(), null)
            {
                Id = 77564,
                Number = "jhuzitorpeü+",
                Description = "ghui3üpjgn-ser",
                ParentId = 879645,
                ControlledBy = LocationControlledBy.Angle,
                SetPointTorque = 15.1534345,
                ToleranceClassTorque = CreateToleranceClassModelWithId(15),
                MinimumTorque = 16545.15,
                MaximumTorque = 198745.5,
                ThresholdTorque = 0.159,
                ToleranceClassAngle = CreateToleranceClassModelWithId(15),
                SetPointAngle = 15.15,
                MinimumAngle = 986546.3215685,
                MaximumAngle = 876,
                ConfigurableField1 = "459tzganklmöt",
                ConfigurableField2 = "L",
                ConfigurableField3 = false,
                Comment = "34ß50t69z8uhgfiodew09tizuhjgk"
            };

            var location = model?.Entity;

            Assert.AreEqual(model.Id, location.Id.ToLong());
            Assert.AreEqual(model.Number, location.Number.ToDefaultString());
            Assert.AreEqual(model.Description, location.Description.ToDefaultString());
            Assert.AreEqual(model.ParentId, location.ParentDirectoryId.ToLong());
            Assert.AreEqual(model.ControlledBy, location.ControlledBy);
            Assert.AreEqual(model.SetPointTorque, location.SetPointTorque.Nm);
            Assert.AreEqual(model.ToleranceClassTorque.Id, location.ToleranceClassTorque.Id.ToLong());
            Assert.AreEqual(model.MinimumTorque, location.MinimumTorque.Nm);
            Assert.AreEqual(model.MaximumTorque, location.MaximumTorque.Nm);
            Assert.AreEqual(model.ThresholdTorque, location.ThresholdTorque.Nm);
            Assert.AreEqual(model.ToleranceClassAngle.Id, location.ToleranceClassAngle.Id.ToLong());
            Assert.AreEqual(model.SetPointAngle, location.SetPointAngle.Degree);
            Assert.AreEqual(model.MinimumAngle, location.MinimumAngle.Degree);
            Assert.AreEqual(model.MaximumAngle, location.MaximumAngle.Degree);
            Assert.AreEqual(model.ConfigurableField1, location.ConfigurableField1.ToDefaultString());
            Assert.AreEqual(model.ConfigurableField2, location.ConfigurableField2.ToDefaultString());
            Assert.AreEqual(model.ConfigurableField3, location.ConfigurableField3);
            Assert.AreEqual(model.Comment, location.Comment);
        }

        [Test]
        public void ChangeToleranceClassTorqueUpdatesLimits()
        {
            var entity = new LocationMock();
            var model = new LocationModel(entity, new NullLocalizationWrapper(), null);

            model.ToleranceClassTorque = new ToleranceClassModel(new ToleranceClass());

            Assert.IsTrue(entity.WasUpdateToleranceLimitsCalled);
        }

        [Test]
        public void ChangeToleranceClassAngleUpdatesLimits()
        {
            var entity = new LocationMock();
            var model = new LocationModel(entity, new NullLocalizationWrapper(), null);

            model.ToleranceClassAngle = new ToleranceClassModel(new ToleranceClass());

            Assert.IsTrue(entity.WasUpdateToleranceLimitsCalled);
        }

        [Test]
        public void UpdateByEntityMapsAllAttributesTest()
        {
            var location = new Location
            {
                Id = new LocationId(123),
                Number = new LocationNumber("Test"),
                Description = new LocationDescription("TestDescription"),
                ParentDirectoryId = new LocationDirectoryId(15),
                ControlledBy = LocationControlledBy.Angle,
                SetPointTorque = Torque.FromNm(15.15),
                ToleranceClassTorque = CreateParameterizedToleranceClass(15),
                MinimumTorque = Torque.FromNm(15.15),
                MaximumTorque = Torque.FromNm(15.15),
                ThresholdTorque = Torque.FromNm(15.15),
                ToleranceClassAngle = CreateParameterizedToleranceClass(15),
                SetPointAngle = Angle.FromDegree(15.15),
                MinimumAngle = Angle.FromDegree(15.15),
                MaximumAngle = Angle.FromDegree(15.15),
                ConfigurableField1 = new LocationConfigurableField1("TestConfField1"),
                ConfigurableField2 = new LocationConfigurableField2("T"),
                ConfigurableField3 = false,
                Comment = "p34e9tiughfudrieopigjhj"
            };

            var model = new LocationModel(location, new NullLocalizationWrapper(), null);
            model.UpdateWith(location);

            Assert.AreEqual(location.Id.ToLong(), model.Id);
            Assert.AreEqual(location.Number.ToDefaultString(), model.Number);
            Assert.AreEqual(location.Description.ToDefaultString(), model.Description);
            Assert.AreEqual(location.ParentDirectoryId.ToLong(), model.ParentId);
            Assert.AreEqual(location.ControlledBy, model.ControlledBy);
            Assert.AreEqual(location.SetPointTorque.Nm, model.SetPointTorque);
            Assert.AreEqual(location.ToleranceClassTorque.Id.ToLong(), model.ToleranceClassTorque.Id);
            Assert.AreEqual(location.MinimumTorque.Nm, model.MinimumTorque);
            Assert.AreEqual(location.MaximumTorque.Nm, model.MaximumTorque);
            Assert.AreEqual(location.ThresholdTorque.Nm, model.ThresholdTorque);
            Assert.AreEqual(location.ToleranceClassAngle.Id.ToLong(), model.ToleranceClassAngle.Id);
            Assert.AreEqual(location.SetPointAngle.Degree, model.SetPointAngle);
            Assert.AreEqual(location.MinimumAngle.Degree, model.MinimumAngle);
            Assert.AreEqual(location.MaximumAngle.Degree, model.MaximumAngle);
            Assert.AreEqual(location.ConfigurableField1.ToDefaultString(), model.ConfigurableField1);
            Assert.AreEqual(location.ConfigurableField2.ToDefaultString(), model.ConfigurableField2);
            Assert.AreEqual(location.ConfigurableField3, model.ConfigurableField3);
            Assert.AreEqual(location.Comment, model.Comment);
        }

        [Test]
        public void CopyLocationMapsAllProperties()
        {
            var location = new Location()
            {
                Id = new LocationId(77564),
                Number = new LocationNumber("jhuzitorpeü+"),
                Description = new LocationDescription("ghui3üpjgn-ser"),
                ParentDirectoryId = new LocationDirectoryId(879645),
                ControlledBy = LocationControlledBy.Angle,
                SetPointTorque = Torque.FromNm(15.1534345),
                ToleranceClassTorque = CreateAnonymousToleranceClass(),
                MinimumTorque = Torque.FromNm(16545.15),
                MaximumTorque = Torque.FromNm(198745.5),
                ThresholdTorque = Torque.FromNm(0.159),
                ToleranceClassAngle = CreateAnonymousToleranceClass(),
                SetPointAngle = Angle.FromDegree(15.15),
                MinimumAngle = Angle.FromDegree(986546.3215685),
                MaximumAngle = Angle.FromDegree(876),
                ConfigurableField1 = new LocationConfigurableField1("459tzganklmöt"),
                ConfigurableField2 = new LocationConfigurableField2("L"),
                ConfigurableField3 = false
            };
            var model = LocationModel.GetModelFor(location, new NullLocalizationWrapper(), null);

            var copy = model.CopyDeep();

            Assert.IsFalse(model == copy);
            Assert.AreEqual(model.Id, copy.Id);
            Assert.AreEqual(model.Number, copy.Number);
            Assert.AreEqual(model.Description, copy.Description);
            Assert.AreEqual(model.ParentId, copy.ParentId);
            Assert.AreEqual(model.ControlledBy, copy.ControlledBy);
            Assert.AreEqual(model.SetPointTorque, copy.SetPointTorque);
            Assert.AreEqual(model.ToleranceClassTorque.Id, copy.ToleranceClassTorque.Id);
            Assert.AreEqual(model.MinimumTorque, copy.MinimumTorque);
            Assert.AreEqual(model.MaximumTorque, copy.MaximumTorque);
            Assert.AreEqual(model.ThresholdTorque, copy.ThresholdTorque);
            Assert.AreEqual(model.ToleranceClassAngle.Id, copy.ToleranceClassAngle.Id);
            Assert.AreEqual(model.SetPointAngle, copy.SetPointAngle);
            Assert.AreEqual(model.MinimumAngle, copy.MinimumAngle);
            Assert.AreEqual(model.MaximumAngle, copy.MaximumAngle);
            Assert.AreEqual(model.ConfigurableField1, copy.ConfigurableField1);
            Assert.AreEqual(model.ConfigurableField2, copy.ConfigurableField2);
            Assert.AreEqual(model.ConfigurableField3, copy.ConfigurableField3);
        }

        [Test]
        public void AreLocationModelsEqualWithEqualModels()
        {
            var location = CreateLocation.Anonymous();
            location.ToleranceClassTorque = new ToleranceClass() {Id = new ToleranceClassId(987654)};
            location.ToleranceClassAngle = new ToleranceClass() {Id = new ToleranceClassId(963852)};
            var model1 = LocationModel.GetModelFor(location, new NullLocalizationWrapper(), null);
            var model2 = model1.CopyDeep();

            Assert.IsTrue(model1.EqualsByContent(model2));
        }

        [Test, TestCaseSource(nameof(GetLocationModelsForEqualityCheck))]
        public void AreLocationModelsEqualCheckesForAttributeEquality(Tuple<LocationModel, LocationModel> unequalPair)
        {
            Assert.IsFalse(unequalPair.Item1.EqualsByContent(unequalPair.Item2));
        }


        private static IEnumerable<Tuple<LocationModel, LocationModel>> GetLocationModelsForEqualityCheck()
        {
            var properties = new List<string>()
            {
                nameof(LocationModel.Id),
                nameof(LocationModel.Number),
                nameof(LocationModel.Description),
                nameof(LocationModel.ParentId),
                nameof(LocationModel.ControlledBy),
                nameof(LocationModel.SetPointTorque),
                nameof(LocationModel.ToleranceClassTorque),
                nameof(LocationModel.MinimumTorque),
                nameof(LocationModel.MaximumTorque),
                nameof(LocationModel.ThresholdTorque),
                nameof(LocationModel.SetPointAngle),
                nameof(LocationModel.MinimumAngle),
                nameof(LocationModel.MaximumAngle),
                nameof(LocationModel.ConfigurableField1),
                nameof(LocationModel.ConfigurableField2),
                nameof(LocationModel.ConfigurableField3),
                nameof(LocationModel.Comment)
            };

            foreach (var property in properties)
            {
                yield return GetEqualLocationModelsExceptOneProperty(property);
            }
        }

        private static Tuple<LocationModel, LocationModel> GetEqualLocationModelsExceptOneProperty(string propertyName)
        {
            var model1 = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);
            var model2 = model1.CopyDeep();

            switch (propertyName)
            {
                case nameof(LocationModel.Id): model2.Id = 7412369; break;
                case nameof(LocationModel.Number): model2.Number = "rtguuriguio"; break;
                case nameof(LocationModel.Description): model2.Number = "ugtfrerdkfjd"; break;
                case nameof(LocationModel.ParentId): model2.ParentId = 467389; break;
                case nameof(LocationModel.ControlledBy): model2.ControlledBy = model2.ControlledBy == LocationControlledBy.Torque ? LocationControlledBy.Angle : LocationControlledBy.Torque; break;
                case nameof(LocationModel.SetPointTorque): model2.SetPointTorque = 789465; break;
                case nameof(LocationModel.ToleranceClassTorque): model2.ToleranceClassTorque = new ToleranceClassModel(new ToleranceClass()) { Id = 54542512 }; break;
                case nameof(LocationModel.MinimumTorque): model2.MinimumTorque = 2304987; break;
                case nameof(LocationModel.MaximumTorque): model2.MaximumTorque = 3645575; break;
                case nameof(LocationModel.ToleranceClassAngle): model2.ToleranceClassAngle = new ToleranceClassModel(new ToleranceClass()) { Id = 76545 }; break;
                case nameof(LocationModel.ThresholdTorque): model2.ThresholdTorque = 75932; break;
                case nameof(LocationModel.SetPointAngle): model2.SetPointAngle = 3123456; break;
                case nameof(LocationModel.MinimumAngle): model2.MinimumAngle = 9524178563; break;
                case nameof(LocationModel.MaximumAngle): model2.MaximumAngle = 309485; break;
                case nameof(LocationModel.ConfigurableField1): model2.ConfigurableField1 = "eorideoporifj"; break;
                case nameof(LocationModel.ConfigurableField2): model2.ConfigurableField2 = "8"; break;
                case nameof(LocationModel.ConfigurableField3): model2.ConfigurableField3 = !model2.ConfigurableField3; break;
                case nameof(LocationModel.Comment): model2.Comment = "409r58tu9034e9rtgfvdefgb65ef"; break;
            }

            return new Tuple<LocationModel, LocationModel>(model1, model2);
        }
        
        private ToleranceClassModel CreateToleranceClassModelWithId(long id)
        {
            return new ToleranceClassModel(new ToleranceClass()) { Id = id };
        }

        private ToleranceClass CreateAnonymousToleranceClass()
        {
            return CreateParameterizedToleranceClass(15);
        }

        private ToleranceClass CreateParameterizedToleranceClass(long id, string name = null, double lowerLimit = 0, double upperLimit = 0, bool relative = false)
        {
            return new ToleranceClass
            {
                Id = new ToleranceClassId(id),
                Name = name,
                LowerLimit = lowerLimit,
                UpperLimit = upperLimit,
                Relative = relative
            };
        }
    }
}