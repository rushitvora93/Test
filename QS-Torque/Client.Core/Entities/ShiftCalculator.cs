using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core.Entities
{
    public class ShiftCalculator
    {
        private ShiftManagement _shiftManagement;
        private List<Shift> _activeShifts = new List<Shift>();

        public ShiftCalculator(ShiftManagement shiftManagement)
        {
            _shiftManagement = shiftManagement;

            _activeShifts.Add(Shift.FirstShiftOfDay);       // First shift is always active
            if (_shiftManagement.IsSecondShiftActive)
            {
                _activeShifts.Add(Shift.SecondShiftOfDay);
            }
            if (_shiftManagement.IsThirdShiftActive)
            {
                _activeShifts.Add(Shift.ThirdShiftOfDay);
            }
        }


        public Shift GetShiftForDateTime(DateTime now)
        {
            return GetShiftForTimeSpan(now.TimeOfDay);
        }

        public Shift GetShiftForTimeSpan(TimeSpan now)
        {
            var shiftStartDict = new Dictionary<Shift, TimeSpan>()
            {
                {Shift.FirstShiftOfDay, _shiftManagement.FirstShiftStart},
                {Shift.SecondShiftOfDay, _shiftManagement.SecondShiftStart},
                {Shift.ThirdShiftOfDay, _shiftManagement.ThirdShiftStart}
            };

            foreach (var shift in _activeShifts)
            {
                var followingShift = _activeShifts[(_activeShifts.IndexOf(shift) + 1) % _activeShifts.Count];
                if (IsTimeBetweenBorders(now, shiftStartDict[shift], shiftStartDict[followingShift]))
                {
                    return shift;
                }
            }
            return Shift.FirstShiftOfDay;
        }

        private bool IsTimeBetweenBorders(TimeSpan timeToCheck, TimeSpan leftBorder, TimeSpan rightBorder)
        {
            if (leftBorder > rightBorder)
            {
                // e. g. between 23:00 and 02:00
                return timeToCheck >= leftBorder && timeToCheck < new TimeSpan(24, 0, 0) ||
                       timeToCheck < rightBorder && timeToCheck >= TimeSpan.Zero;
            }
            else
            {
                // e. g. between 02:00 and 04:00
                return timeToCheck >= leftBorder && timeToCheck < rightBorder;
            }
        }
    }
}
