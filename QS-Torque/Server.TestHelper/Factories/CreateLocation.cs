using System;
using System.Collections.Generic;
using Server.Core.Entities;
using Server.Core.Enums;

namespace Server.TestHelper.Factories
{
    public class CreateLocation
    {
        public static Location Parameterized(long id, string description, string number, string comment,
            string configurableField1, string configurableField2,
            bool configurableField3, double minimumAngle, double maximumAngle, double setPointTorque,
            LocationControlledBy controlledBy,
            double threshouldTorque, double setPointAngle, double maximumTorque, double minimumTorque,
            ToleranceClass toleranceClassTorque, long parentDirectoryId, ToleranceClass toleranceClassAngle, bool alive)
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
                Minimum2 = minimumAngle,
                Maximum2 = maximumAngle,
                SetPoint1 = setPointTorque,
                ControlledBy = controlledBy,
                Threshold1 = threshouldTorque,
                SetPoint2 = setPointAngle,
                Maximum1 = maximumTorque,
                Minimum1 = minimumTorque,
                ToleranceClass1 = toleranceClassTorque,
                ParentDirectoryId = new LocationDirectoryId(parentDirectoryId),
                ToleranceClass2= toleranceClassAngle, Alive = alive
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
                CreateToleranceClass.Randomized(randomizer.Next()),
                randomizer.Next(0, 1) == 1);
        }

        public static Location IdOnly(long id)
        {
            var result = Anonymous();
            result.Id = new LocationId(id);
            return result;
        }

        public static Location ParentIdOnly(long parentId)
        {
            return new Location {Id = new LocationId(15), ParentDirectoryId = new LocationDirectoryId(parentId)};
        }

        public static Location Anonymous()
        {
            return Parameterized(15, "Test", "Test", null, "Test", "t", false, 5, 7, 6, LocationControlledBy.Angle, 3,
                6, 7,
                5, new ToleranceClass() {Id = new ToleranceClassId(321)}, 5,
                new ToleranceClass() {Id = new ToleranceClassId(987)}, false);
        }

        public static Location IdAndDescription(long id, string description)
        {
            return new Location {Id = new LocationId(id), Description = new LocationDescription(description)};
        }

        public static Location NumberOnly(string number)
        {
            var result = Anonymous();
            result.Number = new LocationNumber(number);
            return result;
        }

        public static Location DescriptionOnly(string description)
        {
            var result = Anonymous();
            result.Description = new LocationDescription(description);
            return result;
        }

        public static Location WithIdNumberDescription(long id, string number, string description)
        {
            var result = Anonymous();
            result.Id = new LocationId(id);
            result.Number = new LocationNumber(number);
            result.Description = new LocationDescription(description);
            return result;
        }

        public static Location IdAndCommentOnly(long id, string comment)
        {
            var result = IdOnly(id);
            result.Comment = comment;
            return result;
        }
    }
}
