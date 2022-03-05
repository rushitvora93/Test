using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.UseCases.UseCases;

namespace Server.UseCases.Test.UseCases
{
    class ShiftManagementDataMock : IShiftManagementData
    {
        public ShiftManagement GetShiftManagementReturnValue { get; set; }
        public ShiftManagementDiff SaveShiftManagementDiffParameter { get; set; }
        public bool GetShiftManagementCalled { get; set; }
        public bool CommitCalled { get; set; }

        public void Commit()
        {
            CommitCalled = true;
        }

        public ShiftManagement GetShiftManagement()
        {
            GetShiftManagementCalled = true;
            return GetShiftManagementReturnValue;
        }

        public void SaveShiftManagement(ShiftManagementDiff shiftManagement)
        {
            SaveShiftManagementDiffParameter = shiftManagement;
        }
    }

    public class ShiftManagementUseCaseTest
    {
        [Test]
        public void GetShiftManagementReturnsEntityFromDataAccess()
        {
            var tuple = CreateUseCaseTuple();
            var entity = new ShiftManagement();
            tuple.data.GetShiftManagementReturnValue = entity;
            var result = tuple.useCase.GetShiftManagement();
            Assert.AreSame(entity, result);
        }

        [Test]
        public void SaveShiftManagementWithHistoryPassesEntityToDataAccess()
        {
            var tuple = CreateUseCaseTuple();
            var diff = new ShiftManagementDiff();
            tuple.useCase.SaveShiftManagement(diff);
            Assert.AreSame(diff, tuple.data.SaveShiftManagementDiffParameter);
            Assert.IsTrue(tuple.data.CommitCalled);
        }


        static (ShiftManagementUseCase useCase, ShiftManagementDataMock data) CreateUseCaseTuple()
        {
            var data = new ShiftManagementDataMock();
            var useCase = new ShiftManagementUseCase(data);
            return (useCase, data);
        }
    }
}
