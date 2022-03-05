using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;

namespace TestHelper.Factories
{
    public class CreateLocation
    {
        public static Location Parameterized(long id, string description, string number, string comment, string configurableField1, string configurableField2,
            bool configurableField3, double minimumAngle, double maximumAngle, double setPointTorque, LocationControlledBy controlledBy,
            double threshouldTorque, double setPointAngle, double maximumTorque, double minimumTorque, ToleranceClass toleranceClassTorque, long parentDirectoryId, ToleranceClass toleranceClassAngle)
        {
            return new Location
            {
                Id = new LocationId(id),
                Description = new LocationDescription(description),
                Number = new LocationNumber(number),
                Comment = comment,
                ConfigurableField1 = new LocationConfigurableField1(configurableField1),
                ConfigurableField2 = new LocationConfigurableField2(configurableField2),
                ConfigurableField3 = configurableField3,
                MinimumAngle = Angle.FromDegree(minimumAngle),
                MaximumAngle = Angle.FromDegree(maximumAngle),
                SetPointTorque = Torque.FromNm(setPointTorque),
                ControlledBy = controlledBy,
                ThresholdTorque = Torque.FromNm(threshouldTorque),
                SetPointAngle = Angle.FromDegree(setPointAngle),
                MaximumTorque = Torque.FromNm(maximumTorque),
                MinimumTorque = Torque.FromNm(minimumTorque),
                ToleranceClassTorque = toleranceClassTorque,
                ParentDirectoryId = new LocationDirectoryId(parentDirectoryId),
                ToleranceClassAngle = toleranceClassAngle
            };
        }

        public static Location Randomized(int seed)
        {
            var randomizer = new Random(seed);
            var controlList = new List<LocationControlledBy>()
            {
                LocationControlledBy.Torque,
                LocationControlledBy.Angle
            };

            return Parameterized(
                randomizer.Next(),
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                CreateString.Randomized(randomizer.Next(0, 15), randomizer.Next()),
                CreateString.Randomized(randomizer.Next(0, 1), randomizer.Next()),
                randomizer.Next(0, 1) == 1,
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                controlList[randomizer.Next(0, controlList.Count - 1)],
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                CreateToleranceClass.Randomized(randomizer.Next()),
                randomizer.Next(),
                CreateToleranceClass.Randomized(randomizer.Next()));
        }

        public static Location IdOnly(long id)
        {
            return
                WithDynamicParameters(
                    new List<(LocationParameter, string)>
                    {
                        (LocationParameter.Id, id.ToString())
                    });
        }

        public static Location ParentIdOnly(long parentId)
        {
            return
                WithDynamicParameters(
                    new List<(LocationParameter, string)>
                    {
                        (LocationParameter.ParentDirectoryId, parentId.ToString())
                    });
        }
        public static Location Anonymous()
        {
            return Parameterized(15, "Test", "Test",  null, "Test", "t", false, 5, 7, 6, LocationControlledBy.Angle, 3, 6, 7,
                5, CreateToleranceClass.WithId(21425), 5, CreateToleranceClass.WithId(214235));
        }

        public static Location IdAndDescription(long id, string description)
        {
            return
                WithDynamicParameters(
                    new List<(LocationParameter, string)>
                    {
                        (LocationParameter.Id, id.ToString()),
                        (LocationParameter.Description, description)
                    });
        }

        public static Location IdAndNumber(long id, string number)
        {
            return
                WithDynamicParameters(
                    new List<(LocationParameter, string)>
                    {
                        (LocationParameter.Id, id.ToString()),
                        (LocationParameter.Number, number)
                    });
        }

        public static Location NumberOnly(string number)
        {
            return
                WithDynamicParameters(
                    new List<(LocationParameter, string)>
                    {
                        (LocationParameter.Number, number)
                    });
        }

        public static Location DescriptionOnly(string description)
        {
            return
                WithDynamicParameters(
                    new List<(LocationParameter, string)>
                    {
                        (LocationParameter.Description, description)
                    });
        }

        public static Location WithIdNumberDescription(long id, string number, string description)
        {
            return
                WithDynamicParameters(
                    new List<(LocationParameter, string)>
                    {
                        (LocationParameter.Id, id.ToString()),
                        (LocationParameter.Number, number),
                        (LocationParameter.Description, description)
                    });
        }

        public static Location WithIdAndLocationTreePath(long id, List<LocationDirectory> directories)
        {
            var result = Anonymous();
            result.Id = new LocationId(id);
            result.LocationDirectoryPath = directories;
            return result;
        }

        public enum LocationParameter
        {
            Id,
            Number,
            Description,
            ParentDirectoryId,
        }

        public static Location WithDynamicParameters(List<(LocationParameter, string)> parameters)
        {
            var location = Anonymous();
            foreach((LocationParameter parameter, string value) paramValue in parameters)
            {
                switch (paramValue.parameter)
                {
                    case LocationParameter.Id:
                        location.Id = new LocationId(long.Parse(paramValue.value));
                        break;

                    case LocationParameter.Number:
                        location.Number = new LocationNumber(paramValue.value);
                        break;

                    case LocationParameter.Description:
                        location.Description = new LocationDescription(paramValue.value);
                        break;

                    case LocationParameter.ParentDirectoryId:
                        location.ParentDirectoryId = new LocationDirectoryId(long.Parse(paramValue.value));
                        break;
                }
            }
            return location;
        }
    }
}