using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using Server.UseCases.UseCases;

namespace Server.UseCases.Test.UseCases
{
    public class ExtensionDataAccessMock : IExtensionDataAccess
    {
        public enum ExtensionDataAccessFunction
        {
            InsertExtensions,
            UpdateExtensions,
            Commit
        }

        public void Commit()
        {
            CalledFunctions.Add(ExtensionDataAccessFunction.Commit);
        }

        public List<LocationReferenceLink> GetExtensionLocationLinks(ExtensionId extensionId)
        {
            GetExtensionLocationLinksParameter = extensionId;
            return GetExtensionLocationLinksReturnValue;
        }

        public List<Extension> InsertExtensions(List<ExtensionDiff> diffs, bool returnList)
        {
            CalledFunctions.Add(ExtensionDataAccessFunction.InsertExtensions);
            InsertExtensionsDiffs = diffs;
            InsertExtensionsReturnList = returnList;
            return InsertExtensionsReturnValue;
        }

        public void UpdateExtensions(List<ExtensionDiff> extensionDiffs)
        {
            CalledFunctions.Add(ExtensionDataAccessFunction.UpdateExtensions);
            UpdateExtensionsDiffs = extensionDiffs;
        }

        public bool IsInventoryNumberUnique(ExtensionInventoryNumber inventoryNumber)
        {
            IsInventoryNumberUniqueParameter = inventoryNumber;
            return IsInventoryNumberUniqueReturnValue;
        }

        public List<Extension> LoadExtensions()
        {
            LoadExtensionsCalled = true;
            return LoadExtensionsReturnValue;
        }

        public List<Extension> LoadDeletedExtensions()
        {
            throw new NotImplementedException();
        }

        public List<Extension> LoadExtensionsReturnValue { get; set; }
        public bool LoadExtensionsCalled { get; set; }
        public List<LocationReferenceLink> GetExtensionLocationLinksReturnValue { get; set; }
        public ExtensionId GetExtensionLocationLinksParameter { get; set; }
        public List<Extension> InsertExtensionsReturnValue { get; set; }
        public List<ExtensionDiff> InsertExtensionsDiffs { get; set; }
        public bool InsertExtensionsReturnList { get; set; }
        public bool IsInventoryNumberUniqueReturnValue { get; set; }
        public ExtensionInventoryNumber IsInventoryNumberUniqueParameter { get; set; }
        public List<ExtensionDiff> UpdateExtensionsDiffs { get; set; }
        public List<ExtensionDataAccessFunction> CalledFunctions { get; set; } = new List<ExtensionDataAccessFunction>();
    }

    class ExtensionUseCaseTest
    {
        [Test]
        public void LoadExtensionsCallsDataAccess()
        {
            var environment = new Environment();

            environment.useCase.LoadExtensions();

            Assert.IsTrue(environment.dataAccess.LoadExtensionsCalled);
        }

        [Test]
        public void LoadExtensionsReturnsCorrectValue()
        {
            var environment = new Environment();

            var extensions = new List<Extension>();
            environment.dataAccess.LoadExtensionsReturnValue = extensions;

            Assert.AreSame(extensions, environment.useCase.LoadExtensions());
        }

        [TestCase(10)]
        [TestCase(20)]
        public void GetExtensionLocationLinksCallsDataAccess(long extensionId)
        {
            var environment = new Environment();

            environment.useCase.GetExtensionLocationLinks(new ExtensionId(extensionId));

            Assert.AreEqual(extensionId, environment.dataAccess.GetExtensionLocationLinksParameter.ToLong());
        }

        [Test]
        public void GetExtensionLocationLinksReturnsCorrectValue()
        {
            var environment = new Environment();

            var locationReferenceLinks = new List<LocationReferenceLink>();
            environment.dataAccess.GetExtensionLocationLinksReturnValue = locationReferenceLinks;

            var result = environment.useCase.GetExtensionLocationLinks(new ExtensionId(1));

            Assert.AreSame(locationReferenceLinks, result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InsertExtensionsCallsDataAccess(bool returnList)
        {
            var environment = new Environment();
            var diffs = new List<ExtensionDiff>();
            environment.useCase.InsertExtensions(diffs, returnList);

            Assert.AreSame(diffs, environment.dataAccess.InsertExtensionsDiffs);
            Assert.AreEqual(returnList, environment.dataAccess.InsertExtensionsReturnList);
        }

        [Test]
        public void InsertExtensionsCallsCommitAfterWork()
        {
            var environment = new Environment();

            environment.useCase.InsertExtensions(new List<ExtensionDiff>(), true);

            Assert.AreEqual(ExtensionDataAccessMock.ExtensionDataAccessFunction.Commit, environment.dataAccess.CalledFunctions.Last());
        }

        [Test]
        public void UpdateExtensionsCallsDataAccess()
        {
            var environment = new Environment();
            var diffs = new List<ExtensionDiff>();
            environment.useCase.UpdateExtensions(diffs);

            Assert.AreSame(diffs, environment.dataAccess.UpdateExtensionsDiffs);
        }

        [Test]
        public void UpdateExtensionsCallsCommitAfterWork()
        {
            var environment = new Environment();

            environment.useCase.UpdateExtensions(new List<ExtensionDiff>());

            Assert.AreEqual(ExtensionDataAccessMock.ExtensionDataAccessFunction.Commit, environment.dataAccess.CalledFunctions.Last());
        }

        [Test]
        public void InsertExtensionsReturnsCorrectValue()
        {
            var environment = new Environment();

            var extensions = new List<Extension>();
            environment.dataAccess.InsertExtensionsReturnValue = extensions;
            var result = environment.useCase.InsertExtensions(new List<ExtensionDiff>(), true);

            Assert.AreSame(extensions, result);
        }

        [TestCase("234345")]
        [TestCase("abcdef")]
        public void IsInventoryNumberUniqueCallsDataAccess(string inventoryNumber)
        {
            var environment = new Environment();

            environment.useCase.IsInventoryNumberUnique(new ExtensionInventoryNumber(inventoryNumber));

            Assert.AreEqual(inventoryNumber, environment.dataAccess.IsInventoryNumberUniqueParameter.ToDefaultString());
        }

        [TestCase(false)]
        [TestCase(true)]
        public void IsInventoryNumberUniqueReturnsCorrectValue(bool isUnique)
        {
            var environment = new Environment();

            environment.dataAccess.IsInventoryNumberUniqueReturnValue = isUnique;

            Assert.AreEqual(isUnique, environment.useCase.IsInventoryNumberUnique(new ExtensionInventoryNumber("")));
        }

        private class Environment
        {
            public Environment()
            {
                dataAccess = new ExtensionDataAccessMock();
                useCase = new ExtensionUseCase(dataAccess);
            }

            public readonly ExtensionDataAccessMock dataAccess;
            public readonly ExtensionUseCase useCase;
        }
    }
}
