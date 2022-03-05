using System.Collections.Generic;
using BasicTypes;
using Client.Core.Diffs;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using TestLevelSetService;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface ITestLevelSetClient
    {
        ListOfTestLevelSets LoadTestLevelSets();
        DtoTypes.TestLevelSet InsertTestLevelSet(DtoTypes.TestLevelSetDiff newItem);
        void DeleteTestLevelSet(DtoTypes.TestLevelSetDiff oldItem);
        void UpdateTestLevelSet(DtoTypes.TestLevelSetDiff updatedItem);
        BasicTypes.Bool IsTestLevelSetNameUnique(StringResponse name);
        BasicTypes.Bool DoesTestLevelSetHaveReferences(DtoTypes.TestLevelSet set);
    }


    public class TestLevelSetDataAccess : ITestLevelSetData
    {
        private readonly IClientFactory _clientFactory;

        public TestLevelSetDataAccess(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private ITestLevelSetClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetTestLevelSetClient();
        }


        public List<TestLevelSet> LoadTestLevelSets()
        {
            var mapper = new Mapper();
            var list = new List<TestLevelSet>();

            foreach (var testLevelSet in GetClient().LoadTestLevelSets().TestLevelSets)
            {
                list.Add(mapper.DirectPropertyMapping(testLevelSet));
            }

            return list;
        }

        public TestLevelSet AddTestLevelSet(TestLevelSetDiff diff)
        {
            var mapper = new Mapper();
            var result = GetClient().InsertTestLevelSet(new DtoTypes.TestLevelSetDiff()
            {
                New = mapper.DirectPropertyMapping(diff.New),
                UserId = diff.User.UserId.ToLong()
            });
            return result != null ? mapper.DirectPropertyMapping(result) : null;
        }

        public void RemoveTestLevelSet(TestLevelSetDiff diff)
        {
            var mapper = new Mapper();
            GetClient().DeleteTestLevelSet(new DtoTypes.TestLevelSetDiff()
            {
                Old = mapper.DirectPropertyMapping(diff.Old),
                UserId = diff.User.UserId.ToLong()
            });
        }

        public void UpdateTestLevelSet(TestLevelSetDiff diff)
        {
            var mapper = new Mapper();
            GetClient().UpdateTestLevelSet(new DtoTypes.TestLevelSetDiff()
            {
                Old = mapper.DirectPropertyMapping(diff.Old),
                New = mapper.DirectPropertyMapping(diff.New),
                Comment = diff.Comment.ToDefaultString(),
                UserId = diff.User.UserId.ToLong()
            });
        }

        public bool IsTestLevelSetNameUnique(string name)
        {
            return GetClient().IsTestLevelSetNameUnique(new StringResponse() { Value = name })?.Value ?? false;
        }

        public bool DoesTestLevelSetHaveReferences(TestLevelSet set)
        {
            var mapper = new Mapper();
            return GetClient().DoesTestLevelSetHaveReferences(mapper.DirectPropertyMapping(set))?.Value ?? false;
        }
    }
}
