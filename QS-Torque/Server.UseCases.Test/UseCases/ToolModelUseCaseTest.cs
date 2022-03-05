using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using Server.UseCases.UseCases;

namespace Server.UseCases.Test.UseCases
{
    public class ToolModelUseCaseTest
    {
        [Test]
        public void GettingAllToolModelsReturnsDataFromDataAccess()
        {
            var environment = new Environment();
            var expected = new List<ToolModel>();
            environment.Mocks.DataAccess.GetAllToolModelsReturn = expected;
            Assert.AreSame(expected, environment.UseCase.GetAllToolModels());
        }

        [Test]
        public void InsertingToolModelForwardsDiffsToDataAccess()
        {
            var environment = new Environment();
            var expected = new List<ToolModelDiff>();
            environment.UseCase.InsertToolModels(expected);
            Assert.AreSame(expected, environment.Mocks.DataAccess.LastInsertToolModelsParameterToolModelDiffs);
        }

        [Test]
        public void InsertingToolModelReturnsToolModelsFromDataAccess()
        {
            var environment = new Environment();
            var expected = new List<ToolModel>();
            environment.Mocks.DataAccess.InsertToolModelsReturn = expected;
            Assert.AreSame(expected, environment.UseCase.InsertToolModels(null));
        }

        [Test]
        public void InsertingToolModelCallsCommitOnDataAccessLast()
        {
            var environment = new Environment();
            environment.UseCase.InsertToolModels(null);
            Assert.AreEqual(
                ToolModelDataAccessMock.Functions.Commit, 
                environment.Mocks.DataAccess.CallHistory.Last());
        }

        [Test]
        public void UpdatingToolModelForwardsDiffsToDataAccess()
        {
            var environment = new Environment();
            var expected = new List<ToolModelDiff>();
            environment.UseCase.UpdateToolModels(expected);
            Assert.AreSame(expected, environment.Mocks.DataAccess.LastUpdateToolModelsParameterToolModelDiffs);
        }

        [Test]
        public void UpdatingToolModelReturnsToolModelsFromDataAccess()
        {
            var environment = new Environment();
            var expected = new List<ToolModel>();
            environment.Mocks.DataAccess.UpdateToolModelsReturn = expected;
            Assert.AreSame(expected, environment.UseCase.UpdateToolModels(null));
        }

        [Test]
        public void UpdatingToolModelCallsCommitOnDataAccessLast()
        {
            var environment = new Environment();
            environment.UseCase.UpdateToolModels(null);
            Assert.AreEqual(
                ToolModelDataAccessMock.Functions.Commit,
                environment.Mocks.DataAccess.CallHistory.Last());
        }

        [Test]
        public void GettingToolReferencesForwardsToolModelIdToDataAccess()
        {
            var environment = new Environment();
            var expected = new ToolModelId(0);
            environment.UseCase.GetReferencedToolLinks(expected);
            Assert.AreSame(expected, environment.Mocks.DataAccess.LastGetReferencedToolLinksParameterToolModelId);
        }

        [Test]
        public void GettingToolReferencesReturnsResultsFromDataAccess()
        {
            var environment = new Environment();
            var expected = new List<ToolReferenceLink>();
            environment.Mocks.DataAccess.GetReferencedToolLinksReturn = expected;
            Assert.AreSame(expected, environment.Mocks.DataAccess.GetReferencedToolLinksReturn);
        }

        [Test]
        public void GettingAllDeletedToolModelsReturnsDataFromDataAccess()
        {
            var environment = new Environment();
            var expected = new List<ToolModel>();
            environment.Mocks.DataAccess.GetAllDeletedToolModelsReturn = expected;
            Assert.AreSame(expected, environment.UseCase.LoadDeletedToolModels());
        }

        public class ToolModelDataAccessMock: IToolModelDataAccess
        {
            public enum Functions
            {
                GetAllToolModels,
                InsertToolModels,
                UpdateToolModels,
                GetReferencedToolLinks,
                Commit,
                GetAllDeletedToolModelsReturn
            }

            public void Commit()
            {
                CallHistory.Add(Functions.Commit);
            }

            public List<ToolModel> GetAllToolModels()
            {
                CallHistory.Add(Functions.GetAllToolModels);
                return GetAllToolModelsReturn;
            }

            public List<ToolModel> InsertToolModels(List<ToolModelDiff> toolModelDiffs)
            {
                CallHistory.Add(Functions.InsertToolModels);
                LastInsertToolModelsParameterToolModelDiffs = toolModelDiffs;
                return InsertToolModelsReturn;
            }

            public List<ToolModel> UpdateToolModels(List<ToolModelDiff> toolModelDiffs)
            {
                CallHistory.Add(Functions.UpdateToolModels);
                LastUpdateToolModelsParameterToolModelDiffs = toolModelDiffs;
                return UpdateToolModelsReturn;
            }

            public List<ToolReferenceLink> GetReferencedToolLinks(ToolModelId toolModelId)
            {
                CallHistory.Add(Functions.GetReferencedToolLinks);
                LastGetReferencedToolLinksParameterToolModelId = toolModelId;
                return GetReferencedToolLinksReturn;
            }

            public List<ToolModel> LoadDeletedToolModels()
            {
                CallHistory.Add(Functions.GetAllDeletedToolModelsReturn);
                return GetAllDeletedToolModelsReturn;
            }

            public List<ToolModel> GetAllToolModelsReturn;
            public List<ToolModel> GetAllDeletedToolModelsReturn;
            public List<ToolModelDiff> LastInsertToolModelsParameterToolModelDiffs;
            public List<ToolModel> InsertToolModelsReturn;
            public List<ToolModelDiff> LastUpdateToolModelsParameterToolModelDiffs;
            public List<ToolModel> UpdateToolModelsReturn;
            public ToolModelId LastGetReferencedToolLinksParameterToolModelId;
            public List<ToolReferenceLink> GetReferencedToolLinksReturn;
            public List<Functions> CallHistory = new List<Functions>();
        }

        public class Environment
        {
            public class Mock
            {
                public Mock()
                {
                    DataAccess = new ToolModelDataAccessMock();
                }

                public ToolModelDataAccessMock DataAccess;
            }

            public Environment()
            {
                Mocks = new Mock();
                UseCase = new ToolModelUseCase(Mocks.DataAccess);
            }

            public Mock Mocks;
            public ToolModelUseCase UseCase;
        }
    }
}
