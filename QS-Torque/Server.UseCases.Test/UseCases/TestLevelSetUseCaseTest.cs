using System.Collections.Generic;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.UseCases.UseCases;

namespace Server.UseCases.Test.UseCases
{
    class TestLevelSetDataMock : ITestLevelSetData
    {
        public List<TestLevelSet> LoadTestLevelSetsReturnValue { get; set; }
        public TestLevelSetDiff InsertTestLevelSetParameter { get; set; }
        public TestLevelSet InsertTestLevelSetReturnValue { get; set; }
        public TestLevelSetDiff DeleteTestLevelSetParameter { get; set; }
        public TestLevelSetDiff UpdateTestLevelSetParameter { get; set; }
        public TestLevelSet DoesTestLevelSetHaveReferencesParameter { get; set; }
        public string IsTestLevelSetNameUniqueParameter { get; set; }
        public bool IsTestLevelSetNameUniqueReturnValue { get; set; }
        public bool DoesTestLevelSetHaveReferencesReturnValue { get; set; }
        public bool CommitCalled { get; set; }

        public void Commit()
        {
            CommitCalled = true;
        }

        public List<TestLevelSet> LoadTestLevelSets()
        {
            return LoadTestLevelSetsReturnValue;
        }

        public TestLevelSet InsertTestLevelSet(TestLevelSetDiff diff)
        {
            InsertTestLevelSetParameter = diff;
            return InsertTestLevelSetReturnValue;
        }

        public void DeleteTestLevelSet(TestLevelSetDiff diff)
        {
            DeleteTestLevelSetParameter = diff;
        }

        public void UpdateTestLevelSet(TestLevelSetDiff diff)
        {
            UpdateTestLevelSetParameter = diff;
        }

        public bool IsTestLevelSetNameUnique(string name)
        {
            IsTestLevelSetNameUniqueParameter = name;
            return IsTestLevelSetNameUniqueReturnValue;
        }

        public bool DoesTestLevelSetHaveReferences(TestLevelSet set)
        {
            DoesTestLevelSetHaveReferencesParameter = set;
            return DoesTestLevelSetHaveReferencesReturnValue;
        }
    }


    public class TestLevelSetUseCaseTest
    {
        [Test]
        public void LoadTestLevelSetsReturnsEntitiesFromData()
        {
            var tuple = CreateUseCaseTuple();
            var list = new List<TestLevelSet>();
            tuple.data.LoadTestLevelSetsReturnValue = list;
            var result = tuple.useCase.LoadTestLevelSets();
            Assert.AreSame(list, result);
        }

        [Test]
        public void InsertTestLevelSetReturnsEntityFromData()
        {
            var tuple = CreateUseCaseTuple();
            var set = new TestLevelSet();
            tuple.data.InsertTestLevelSetReturnValue = set;
            var result = tuple.useCase.InsertTestLevelSet(null);
            Assert.AreSame(set, result);
        }

        [Test]
        public void InsertTestLevelSetPassesEntityToData()
        {
            var tuple = CreateUseCaseTuple();
            var diff = new TestLevelSetDiff();
            tuple.useCase.InsertTestLevelSet(diff);
            Assert.AreSame(diff, tuple.data.InsertTestLevelSetParameter);
        }
        
        [Test]
        public void InsertTestLevelSetCallsCommit()
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.InsertTestLevelSet(null);
            Assert.IsTrue(tuple.data.CommitCalled);
        }

        [Test]
        public void DeleteTestLevelSetPassesEntityToData()
        {
            var tuple = CreateUseCaseTuple();
            var diff = new TestLevelSetDiff();
            tuple.useCase.DeleteTestLevelSet(diff);
            Assert.AreSame(diff, tuple.data.DeleteTestLevelSetParameter);
        }

        [Test]
        public void DeleteTestLevelSetCallsCommit()
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.DeleteTestLevelSet(null);
            Assert.IsTrue(tuple.data.CommitCalled);
        }

        [Test]
        public void UpdateTestLevelSetPassesEntityToData()
        {
            var tuple = CreateUseCaseTuple();
            var diff = new TestLevelSetDiff();
            tuple.useCase.UpdateTestLevelSet(diff);
            Assert.AreSame(diff, tuple.data.UpdateTestLevelSetParameter);
        }

        [Test]
        public void UpdateTestLevelSetCommit()
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.UpdateTestLevelSet(null);
            Assert.IsTrue(tuple.data.CommitCalled);
        }

        [TestCase("z89crf0mßxi,um")]
        [TestCase("hdcniztz")]
        public void IsTestLevelSetNameUniquePassesValueToData(string name)
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.IsTestLevelSetNameUnique(name);
            Assert.AreEqual(name, tuple.data.IsTestLevelSetNameUniqueParameter);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsTestLevelSetNameUniqueReturnsValueToData(bool isUnique)
        {
            var tuple = CreateUseCaseTuple();
            tuple.data.IsTestLevelSetNameUniqueReturnValue = isUnique;
            var result = tuple.useCase.IsTestLevelSetNameUnique("");
            Assert.AreEqual(isUnique, result);
        }

        [Test]
        public void DoesTestLevelSetHaveReferencesPassesValueToData()
        {
            var tuple = CreateUseCaseTuple();
            var parameter = new TestLevelSet();
            tuple.useCase.DoesTestLevelSetHaveReferences(parameter);
            Assert.AreSame(parameter, tuple.data.DoesTestLevelSetHaveReferencesParameter);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void DoesTestLevelSetHaveReferencesReturnsValueToData(bool isUnique)
        {
            var tuple = CreateUseCaseTuple();
            tuple.data.DoesTestLevelSetHaveReferencesReturnValue = isUnique;
            var result = tuple.useCase.DoesTestLevelSetHaveReferences(new TestLevelSet());
            Assert.AreEqual(isUnique, result);
        }


        private static (TestLevelSetUseCase useCase, TestLevelSetDataMock data) CreateUseCaseTuple()
        {
            var data = new TestLevelSetDataMock();
            var useCase = new TestLevelSetUseCase(data);
            return (useCase, data);
        }
    }
}
