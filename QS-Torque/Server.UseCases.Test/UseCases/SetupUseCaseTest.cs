using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Server.Core.Entities;
using Server.UseCases.UseCases;

namespace Server.UseCases.Test.UseCases
{
    public class SetupDataAccessMock : ISetupDataAccess
    {
        public enum SetupDataAccessFunction
        {
            InsertOrUpdateQstSetups,
            Commit
        }
        public void Commit()
        {
            CalledFunctions.Add(SetupDataAccessFunction.Commit);
        }

        public List<QstSetup> GetQstSetupsByUserIdAndName(long userId, string name)
        {
            GetQstSetupsByUserIdAndNameParameterUserId = userId;
            GetQstSetupsByUserIdAndNameParameterName = name;
            return GetQstSetupsByUserIdAndNameReturnValue;
        }

        public List<QstSetup> GetColumnWidthsForGrid(long userId, string gridName, List<string> columns)
        {
            GetColumnWidthsForGridParameterUserId = userId;
            GetColumnWidthsForGridParameterGridName = gridName;
            GetColumnWidthsForGridParameterColumns = columns;
            return GetColumnWidthsForGridReturnValue;
        }

        public List<QstSetup> InsertOrUpdateQstSetups(List<QstSetup> qstSetups, bool returnList)
        {
            CalledFunctions.Add(SetupDataAccessFunction.InsertOrUpdateQstSetups);
            InsertOrUpdateQstSetupsParameterSetups = qstSetups;
            InsertOrUpdateQstSetupsParameterReturnList = returnList;
            return InsertOrUpdateQstSetupsReturnValue;
        }

        public List<QstSetup> GetQstSetupsByUserIdAndNameReturnValue { get; set; }
        public List<QstSetup> InsertOrUpdateQstSetupsReturnValue { get; set; }
        public bool InsertOrUpdateQstSetupsParameterReturnList { get; set; }
        public List<QstSetup> InsertOrUpdateQstSetupsParameterSetups { get; set; }
        public string GetQstSetupsByUserIdAndNameParameterName { get; set; }
        public long GetQstSetupsByUserIdAndNameParameterUserId { get; set; }
        public List<QstSetup> GetColumnWidthsForGridReturnValue { get; set; }
        public List<string> GetColumnWidthsForGridParameterColumns { get; set; }
        public string GetColumnWidthsForGridParameterGridName { get; set; }
        public long GetColumnWidthsForGridParameterUserId { get; set; }
        public List<SetupDataAccessFunction> CalledFunctions { get; set; } = new List<SetupDataAccessFunction>();
    }

    public class SetupUseCaseTest
    {
        [TestCase(12, "hans")]
        [TestCase(1, "wurst")]
        public void GetQstSetupsByUserIdAndNameCallsDataAccess(long userId, string name)
        {
            var dataAccess = new SetupDataAccessMock();
            var useCase = new SetupUseCase(dataAccess);

            useCase.GetQstSetupsByUserIdAndName(userId, name);

            Assert.AreEqual(userId, dataAccess.GetQstSetupsByUserIdAndNameParameterUserId);
            Assert.AreEqual(name, dataAccess.GetQstSetupsByUserIdAndNameParameterName);
        }

        [Test]
        public void GetQstSetupsByUserIdAndNameReturnsCorrectValue()
        {
            var dataAccess = new SetupDataAccessMock();
            var useCase = new SetupUseCase(dataAccess);

            var setups = new List<QstSetup>();
            dataAccess.GetQstSetupsByUserIdAndNameReturnValue = setups;
            var results = useCase.GetQstSetupsByUserIdAndName(1, "");

            Assert.AreSame(setups, results);
        }


        [TestCase(12, "hans")]
        [TestCase(1, "wurst")]
        public void GetColumnWidthsForGridCallsDataAccess(long userId, string name)
        {
            var dataAccess = new SetupDataAccessMock();
            var useCase = new SetupUseCase(dataAccess);

            var columns = new List<string>();
            useCase.GetColumnWidthsForGrid(userId, name, columns);

            Assert.AreEqual(userId, dataAccess.GetColumnWidthsForGridParameterUserId);
            Assert.AreEqual(name, dataAccess.GetColumnWidthsForGridParameterGridName);
            Assert.AreSame(columns, dataAccess.GetColumnWidthsForGridParameterColumns);
        }

        [Test]
        public void GetColumnWidthsForGridReturnsCorrectValue()
        {
            var dataAccess = new SetupDataAccessMock();
            var useCase = new SetupUseCase(dataAccess);

            var setups = new List<QstSetup>();
            dataAccess.GetColumnWidthsForGridReturnValue = setups;
            var results = useCase.GetColumnWidthsForGrid(1, "", new List<string>());

            Assert.AreSame(setups, results);
        }


        [TestCase(true)]
        [TestCase(false)]
        public void InsertOrUpdateQstSetupsCallsDataAccess(bool returnList)
        {
            var dataAccess = new SetupDataAccessMock();
            var useCase = new SetupUseCase(dataAccess);

            var setups = new List<QstSetup>();
            useCase.InsertOrUpdateQstSetups(setups,returnList);

            Assert.AreEqual(returnList, dataAccess.InsertOrUpdateQstSetupsParameterReturnList);
            Assert.AreSame(setups, dataAccess.InsertOrUpdateQstSetupsParameterSetups);
            Assert.AreEqual(SetupDataAccessMock.SetupDataAccessFunction.Commit, dataAccess.CalledFunctions.Last());
        }

        [Test]
        public void InsertOrUpdateQstSetupsReturnsCorrectValue()
        {
            var dataAccess = new SetupDataAccessMock();
            var useCase = new SetupUseCase(dataAccess);

            var setups = new List<QstSetup>();
            dataAccess.InsertOrUpdateQstSetupsReturnValue = setups;
            var results = useCase.InsertOrUpdateQstSetups(new List<QstSetup>(), true);

            Assert.AreSame(setups, results);
        }
    }
}
