using Client.TestHelper.Factories;
using Core.Entities;
using Core.Enums;

namespace TestHelper.Factories
{
    public class CreateLocationToolAssignment
    {
        public static LocationToolAssignment Anonymous()
        {
            return
                new LocationToolAssignment
                {
                    Id = new LocationToolAssignmentId(7),
                    AssignedLocation = CreateLocation.Anonymous(),
                    TestParameters = CreateTestParameters.Anonymous(),
                    AssignedTool = CreateTool.Anonymous(),
                    TestTechnique = new TestTechnique(),
                    ToolUsage = new ToolUsage{ListId = new HelperTableEntityId(15), Value=new ToolUsageDescription("Test")},
                    TestLevelSetChk = new TestLevelSet()
                    {
                        Id = new TestLevelSetId(1),
                        Name = new TestLevelSetName("gzfuirdoepü"),
                        TestLevel1 = new TestLevel()
                        {
                            Id = new TestLevelId(2),
                            SampleNumber = 6,
                            TestInterval = new Interval(),
                            IsActive = true
                        },
                        TestLevel2 = new TestLevel()
                        {
                            Id = new TestLevelId(2),
                            SampleNumber = 6,
                            TestInterval = new Interval(),
                            IsActive = true
                        },
                        TestLevel3 = new TestLevel()
                        {
                            Id = new TestLevelId(2),
                            SampleNumber = 6,
                            TestInterval = new Interval(),
                            IsActive = true
                        }
                    },
                    TestLevelSetMfu = new TestLevelSet()
                    {
                        Id = new TestLevelSetId(1),
                        Name = new TestLevelSetName("gzfuirdoepü"),
                        TestLevel1 = new TestLevel()
                        {
                            Id = new TestLevelId(2),
                            SampleNumber = 6,
                            TestInterval = new Interval(),
                            IsActive = true
                        },
                        TestLevel2 = new TestLevel()
                        {
                            Id = new TestLevelId(2),
                            SampleNumber = 6,
                            TestInterval = new Interval(),
                            IsActive = true
                        },
                        TestLevel3 = new TestLevel()
                        {
                            Id = new TestLevelId(2),
                            SampleNumber = 6,
                            TestInterval = new Interval(),
                            IsActive = true
                        }
                    },
                    TestLevelNumberChk = 1,
                    TestLevelNumberMfu = 1
            };
        }

        public static LocationToolAssignment WithLocationWithToolAndTestParam(Location location, Tool tool, TestParameters testParameter)
        {
            var locationToolAssignment = Anonymous();
            locationToolAssignment.AssignedLocation = location;
            locationToolAssignment.AssignedTool = tool;
            locationToolAssignment.TestParameters = testParameter;
            return locationToolAssignment;
        }


        public static LocationToolAssignment IdOnly(long id)
        {
            var assignment = Anonymous();
            assignment.Id = new LocationToolAssignmentId(id);
            return assignment;
        }

        public static LocationToolAssignment WithLocation(Location location)
        {
            var locationToolAssignment = Anonymous();
            locationToolAssignment.AssignedLocation = location;
            return locationToolAssignment;
        }

        public static LocationToolAssignment WithTestParameters(Core.Entities.TestParameters testParameters)
        {
            var locationToolAssignment = Anonymous();
            locationToolAssignment.TestParameters = testParameters;
            return locationToolAssignment;
        }

        public static LocationToolAssignment WithTestParametersControlledBy(LocationControlledBy controlledBy)
        {
            var locationToolAssignment = Anonymous();
            locationToolAssignment.TestParameters = new TestParameters()
            {
                ToleranceClassAngle = new ToleranceClass() { Id = new ToleranceClassId(1) },
                ToleranceClassTorque = new ToleranceClass() { Id = new ToleranceClassId(1)},
                ControlledBy = controlledBy
            };
            return locationToolAssignment;
        }

        public static LocationToolAssignment WithTool(Tool tool)
        {
            var locationToolAssignment = Anonymous();
            locationToolAssignment.AssignedTool = tool;
            return locationToolAssignment;
        }

        public static LocationToolAssignment WithTestTechnique(TestTechnique technique)
        {
            var locationToolAssignment = Anonymous();
            locationToolAssignment.TestTechnique = technique;
            return locationToolAssignment;
        }

        public static LocationToolAssignment WithMcaTestLevelSets(TestLevelSet mcaTestLevelSet, int mcaTestLevelNumber = 1)
        {
            var locationToolAssignment = Anonymous();
            locationToolAssignment.TestLevelSetMfu = mcaTestLevelSet;
            locationToolAssignment.TestLevelNumberMfu = mcaTestLevelNumber;
            return locationToolAssignment;
        }

        public static LocationToolAssignment WithTestLevelSets(TestLevelSet mcaTestLevelSet, TestLevelSet monitoringTestLevelSet, int mcaTestLevelNumber = 1, int monitoringTestLevelNumber = 1)
        {
            var locationToolAssignment = WithMcaTestLevelSets(mcaTestLevelSet, mcaTestLevelNumber);
            locationToolAssignment.TestLevelSetChk = monitoringTestLevelSet;
            locationToolAssignment.TestLevelNumberChk = monitoringTestLevelNumber;
            return locationToolAssignment;
        }
    }
}