using System;
using Client.Core.Diffs;
using Core.Entities;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServerIntegrationTests
{
    [TestClass]
    public class ShiftManagementTest
    {
        private readonly TestSetup _testSetup;

        public ShiftManagementTest()
        {
            _testSetup = new TestSetup();
        }

        [TestMethod]
        public void SaveShiftManagement()
        {
            var dataAccess = new ShiftManagementDataAccess(_testSetup.ClientFactory);
            SaveShiftManagementWithDataAccess(dataAccess);

            var updatedShift = new ShiftManagement()
            {
                FirstShiftStart = TimeSpan.FromTicks(6540),
                FirstShiftEnd = TimeSpan.FromTicks(7890),
                SecondShiftStart = TimeSpan.FromTicks(8900),
                SecondShiftEnd = TimeSpan.FromTicks(9000),
                ThirdShiftStart = TimeSpan.FromTicks(9100),
                ThirdShiftEnd = TimeSpan.FromTicks(9200),
                IsSecondShiftActive = false,
                IsThirdShiftActive = true,
                ChangeOfDay = TimeSpan.FromTicks(9300),
                FirstDayOfWeek = DayOfWeek.Friday
            };
            dataAccess.SaveShiftManagement(new ShiftManagementDiff(new ShiftManagement(), updatedShift, _testSetup.TestUser, new HistoryComment("")));

            var loadedShift = dataAccess.LoadShiftManagement();

            Assert.IsTrue(updatedShift.EqualsByContent(loadedShift));
        }

        [TestMethod]
        public void LoadShiftManagement()
        {
            var dataAccess = new ShiftManagementDataAccess(_testSetup.ClientFactory);
            var shift = SaveShiftManagementWithDataAccess(dataAccess);

            var result = dataAccess.LoadShiftManagement();

            Assert.IsTrue(shift.EqualsByContent(result));
        }

        private ShiftManagement SaveShiftManagementWithDataAccess(ShiftManagementDataAccess dataAccess)
        {
            var shift = new ShiftManagement()
            {
                FirstShiftStart = TimeSpan.FromTicks(654),
                FirstShiftEnd = TimeSpan.FromTicks(789),
                SecondShiftStart = TimeSpan.FromTicks(890),
                SecondShiftEnd = TimeSpan.FromTicks(900),
                ThirdShiftStart = TimeSpan.FromTicks(910),
                ThirdShiftEnd = TimeSpan.FromTicks(920),
                IsSecondShiftActive = true,
                IsThirdShiftActive = false,
                ChangeOfDay = TimeSpan.FromTicks(930),
                FirstDayOfWeek = DayOfWeek.Wednesday
            };

            dataAccess.SaveShiftManagement(new ShiftManagementDiff(new ShiftManagement(), shift, _testSetup.TestUser, new HistoryComment("")));
            return shift;
        }
    }
}
