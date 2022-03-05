using Core.Enums;
using InterfaceAdapters.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapters.Test.Models
{
    public class LocationToolAssignmentModelTest
    {
        [TestCase("2020-02-23")]
        [TestCase("2021-12-06 23:15:00")]
        public void NextTestDateForTestTypeForChk(DateTime date)
        {
            var model = new LocationToolAssignmentModelWithTestType(new Core.Entities.LocationToolAssignment(), new NullLocalizationWrapper())
            {
                TestType = Core.Enums.TestType.Chk,
                NextTestDateChk = date
            };
            Assert.AreEqual(date, model.NextTestDateForTestType);
        }

        [TestCase("2020-02-23")]
        [TestCase("2021-12-06 23:15:00")]
        public void NextTestDateForTestTypeForMfu(DateTime date)
        {
            var model = new LocationToolAssignmentModelWithTestType(new Core.Entities.LocationToolAssignment(), new NullLocalizationWrapper())
            {
                TestType = Core.Enums.TestType.Mfu,
                NextTestDateMfu = date
            };
            Assert.AreEqual(date, model.NextTestDateForTestType);
        }

        [TestCase(Shift.SecondShiftOfDay)]
        [TestCase(Shift.ThirdShiftOfDay)]
        public void NextTestShiftForTestTypeForChk(Shift shift)
        {
            var model = new LocationToolAssignmentModelWithTestType(new Core.Entities.LocationToolAssignment(), new NullLocalizationWrapper())
            {
                TestType = Core.Enums.TestType.Chk,
                NextTestShiftChk = shift
            };
            Assert.AreEqual(shift, model.NextTestShiftForTestType);
        }

        [TestCase(Shift.SecondShiftOfDay)]
        [TestCase(Shift.ThirdShiftOfDay)]
        public void NextTestShiftForTestTypeForMfu(Shift shift)
        {
            var model = new LocationToolAssignmentModelWithTestType(new Core.Entities.LocationToolAssignment(), new NullLocalizationWrapper())
            {
                TestType = Core.Enums.TestType.Mfu,
                NextTestShiftMfu = shift
            };
            Assert.AreEqual(shift, model.NextTestShiftForTestType);
        }
    }
}
