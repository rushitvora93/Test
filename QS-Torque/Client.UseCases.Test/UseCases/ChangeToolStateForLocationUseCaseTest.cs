using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestHelper.Factories;
using TestHelper.Mock;

namespace Core.Test.UseCases
{
    public class ChangeToolStateForLocationUseCaseTest
    {
        class ChangeToolStateForLocationGuiMock : IChangeToolStateForLocationGui
        {
            public int ShowLocationsForToolsCallCount { get; set; }
            public Dictionary<Tool, List<LocationReferenceLink>> ShowLocationsForToolsParameter { get; set; }
            public int ShowErrorForLoadLocationsForToolsCallCount { get; set; }
            public int ShowErrorForSaveToolStatesCount { get; set; }

            public void ShowLocationsForTools(Dictionary<Tool, List<LocationReferenceLink>> locationsForTools)
            {
                ShowLocationsForToolsCallCount++;
                ShowLocationsForToolsParameter = locationsForTools;
            }

            public void ShowErrorForLoadLocationsForTools()
            {
                ShowErrorForLoadLocationsForToolsCallCount++;
            }

            public void ShowErrorForSaveToolStates()
            {
                ShowErrorForSaveToolStatesCount++;
            }

        }

        static readonly IEnumerable<List<Tool>> loadLocationsForToolsCallsDataLoadLocationReferenceLinksForToolWithCorrectParameterData = new List<List<Tool>>
        {
            new List<Tool>{ CreateTool.WithId(7895) },
            new List<Tool>{ CreateTool.WithId(7895) ,  CreateTool.WithId(78),  CreateTool.WithId(5), CreateTool.WithId(89) }
        };

        [Test]
        [TestCaseSource(nameof(loadLocationsForToolsCallsDataLoadLocationReferenceLinksForToolWithCorrectParameterData))]
        public void LoadLocationsForToolsCallsDataLoadLocationReferenceLinksForToolWithCorrectParameter(List<Tool> tools)
        {
            var guiMock = new ChangeToolStateForLocationGuiMock();
            var locationToolAssignmentData = new LocationToolAssignmentDataMock();
            var usecase = new ChangeToolStateForLocationUseCase(locationToolAssignmentData, null, guiMock, null);

            usecase.LoadLocationsForTools(tools);
            CollectionAssert.AreEqual(tools.Select(x => x.Id).ToList(),
                locationToolAssignmentData.LoadLocationReferenceLinksForToolParameter);
        }


        static readonly IEnumerable<Dictionary<Tool, List<LocationReferenceLink>>> loadLocationsForToolsCallsGuiShowLocationsForToolsWithCorrectParameterData = new List<Dictionary<Tool, List<LocationReferenceLink>>>
        {
            new Dictionary<Tool, List<LocationReferenceLink>>
            {
                {CreateTool.WithId(13), new List<LocationReferenceLink>
                {
                    new LocationReferenceLink(new QstIdentifier(13), new LocationNumber("blub1"), new LocationDescription("blub1"), null),
                    new LocationReferenceLink(new QstIdentifier(20), new LocationNumber("blub2"), new LocationDescription("blub2"), null)
                }
                }
            },
            new Dictionary<Tool, List<LocationReferenceLink>>
            {
                {CreateTool.WithId(13), new List<LocationReferenceLink>
                    {
                        new LocationReferenceLink(new QstIdentifier(13), new LocationNumber("blub1"), new LocationDescription("blub1"), null),
                        new LocationReferenceLink(new QstIdentifier(20), new LocationNumber("blub2"), new LocationDescription("blub2"), null)
                    }
                },
                {CreateTool.WithId(14), new List<LocationReferenceLink>
                    {
                        new LocationReferenceLink(new QstIdentifier(44), new LocationNumber("möp"), new LocationDescription("möp1"), null),
                        new LocationReferenceLink(new QstIdentifier(40), new LocationNumber("möp2"), new LocationDescription("möp2"), null)
                    }
                },
                {CreateTool.WithId(24), new List<LocationReferenceLink>
                    {
                        new LocationReferenceLink(new QstIdentifier(64), new LocationNumber("narf"), new LocationDescription("narf1"), null),
                        new LocationReferenceLink(new QstIdentifier(60), new LocationNumber("narf2"), new LocationDescription("narf2"), null),
                        new LocationReferenceLink(new QstIdentifier(63), new LocationNumber("narf3"), new LocationDescription("narf3"), null),
                        new LocationReferenceLink(new QstIdentifier(66), new LocationNumber("narf4"), new LocationDescription("narf4"), null)
                    }
                }
            }
        };

        [Test]
        [TestCaseSource(nameof(loadLocationsForToolsCallsGuiShowLocationsForToolsWithCorrectParameterData))]
        public void LoadLocationsForToolsCallsGuiShowLocationsForToolsWithCorrectParameter(Dictionary<Tool, List<LocationReferenceLink>> testData)
        {
            var guiMock = new ChangeToolStateForLocationGuiMock();
            var locationToolAssignmentData = new LocationToolAssignmentDataMock();
            var usecase = new ChangeToolStateForLocationUseCase(locationToolAssignmentData, null, guiMock, null);
            

            locationToolAssignmentData.LoadLocationReferenceLinksForToolReturnValue = testData.Select(x => new {x.Key.Id, x.Value}).ToDictionary(y => y.Id, z => z.Value);
            usecase.LoadLocationsForTools(testData.Select(x => x.Key).ToList());


            Assert.AreEqual(1, guiMock.ShowLocationsForToolsCallCount);

            var guiParameter = guiMock.ShowLocationsForToolsParameter;
            Assert.AreEqual(testData.Count,  guiParameter.Count);
            foreach (var toolLinksPair in testData)
            {
                Assert.AreEqual(toolLinksPair.Key.Id, guiParameter.FirstOrDefault(x => x.Key.Id.Equals(toolLinksPair.Key.Id)).Key.Id);
                Assert.AreEqual(toolLinksPair.Value, guiParameter.FirstOrDefault(x => x.Key.Id.Equals(toolLinksPair.Key.Id)).Value);
            }

        }

        [Test]
        public void LoadLocationsForToolsDataReturnsDirectoryWithEmptyLocationReferenceLinkList()
        {
            var guiMock = new ChangeToolStateForLocationGuiMock();
            var locationToolAssignmentData = new LocationToolAssignmentDataMock();
            var usecase = new ChangeToolStateForLocationUseCase(locationToolAssignmentData, null, guiMock, null);

            usecase.LoadLocationsForTools(new List<Tool>{CreateTool.Anonymous()});

            var guiParameter = guiMock.ShowLocationsForToolsParameter;

            Assert.AreEqual(1, guiParameter.Count);
            Assert.AreEqual(0, guiParameter.FirstOrDefault().Value.Count);
        }

        [Test]
        public void LoadLocationsForToolsThrowsArgumentNullExceptionOnNullToolList()
        {
            var usecase = new ChangeToolStateForLocationUseCase(null, null, null, null);
            Assert.Throws<ArgumentNullException>(() => usecase.LoadLocationsForTools(null));
        }

        [Test]
        public void LoadLocationsForToolsCallsGuiShowErrorForLoadLocationsForToolsOnDataThrowException()
        {
            var guiMock = new ChangeToolStateForLocationGuiMock();
            var locationToolAssignmentData = new LocationToolAssignmentDataMock();
            var usecase = new ChangeToolStateForLocationUseCase(locationToolAssignmentData, null, guiMock, null);

            locationToolAssignmentData.LoadLocationReferenceLinksForToolThrowsException = true;
            usecase.LoadLocationsForTools(new List<Tool>{CreateTool.Anonymous()});

            Assert.AreEqual(1, guiMock.ShowErrorForLoadLocationsForToolsCallCount);

        }

        [Test]
        public void LoadLocationsForToolsWithEmptyToolListReturnsEmptyDirectory()
        {
            var guiMock = new ChangeToolStateForLocationGuiMock();
            var locationToolAssignmentData = new LocationToolAssignmentDataMock();
            var usecase = new ChangeToolStateForLocationUseCase(locationToolAssignmentData, null, guiMock, null);

            usecase.LoadLocationsForTools(new List<Tool>());

            Assert.AreEqual(0, guiMock.ShowLocationsForToolsParameter.Count);

        }

        static Tool CreateParametrizedTool(int toolId, long stateId)
        {
            Tool tool = CreateTool.WithId(toolId);
            tool.Status = new Status{ListId = new HelperTableEntityId(stateId)};
            return tool;
        }
        static readonly IEnumerable<List<ToolDiff>> setNewToolStatesForToolsWithCorrectParameterData = new List<List<ToolDiff>>
        {
            new List<ToolDiff>
            {
                new ToolDiff(CreateUser.Anonymous(), null,
                    CreateParametrizedTool(13, 20), CreateParametrizedTool(13, 25))
            },
            new List<ToolDiff>
            {
                new ToolDiff(CreateUser.Anonymous(), null,
                    CreateParametrizedTool(13, 20), CreateParametrizedTool(13, 25)),
                new ToolDiff(CreateUser.Anonymous(), null,
                    CreateParametrizedTool(23, 40), CreateParametrizedTool(23, 45)),
                new ToolDiff(CreateUser.Anonymous(), null,
                    CreateParametrizedTool(33, 50), CreateParametrizedTool(33, 25))
            }
        };

        [Test]
        [TestCaseSource(nameof(setNewToolStatesForToolsWithCorrectParameterData))]
        public void SetNewToolStatesForToolsWithCorrectParameter(List<ToolDiff> testData)
        {
            var toolDataMock = new ToolDataAccessMock();
            var useCase = new ChangeToolStateForLocationUseCase(null, toolDataMock, null, new UserGetterMock());

            useCase.SetNewToolStates(testData);

            CollectionAssert.AreEquivalent(testData, toolDataMock.UpdateToolParameters);
        }

        [Test]
        public void SetNewToolStatesForToolsCallsGuiShowErrorForSaveToolStatesThrowException()
        {
            var guiMock = new ChangeToolStateForLocationGuiMock();
            var toolDataMock = new ToolDataAccessMock();
            var useCase = new ChangeToolStateForLocationUseCase(null, toolDataMock, guiMock, null);

            toolDataMock.SetNewToolStatesThrowsException = true;
            useCase.SetNewToolStates(new List<ToolDiff>{new ToolDiff(CreateUser.Anonymous(), null,
                CreateParametrizedTool(33, 50), CreateParametrizedTool(33, 25))});

            Assert.AreEqual(1, guiMock.ShowErrorForSaveToolStatesCount);

        }

        [TestCaseSource(nameof(setNewToolStatesForToolsWithCorrectParameterData))]
        public void SetNewToolStatesCallsUpdateWithDiffUser(List<ToolDiff> testData)
        {
            var toolDataMock = new ToolDataAccessMock();
            var user = new User();
            var userMock = new UserGetterMock();
            userMock.NextReturnedUser = user;
            var useCase = new ChangeToolStateForLocationUseCase(null, toolDataMock, null, userMock);

            useCase.SetNewToolStates(testData);

            foreach (var data in toolDataMock.UpdateToolParameters)
            {
                Assert.AreSame(user, data.User);
            }
        }
    }
}
