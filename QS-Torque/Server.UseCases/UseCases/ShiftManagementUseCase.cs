using Server.Core.Diffs;
using Server.Core.Entities;

namespace Server.UseCases.UseCases
{
    public interface IShiftManagementData
    {
        void Commit();
        ShiftManagement GetShiftManagement();
        void SaveShiftManagement(ShiftManagementDiff shiftManagement);
    }

    public interface IShiftManagementUseCase
    {
        ShiftManagement GetShiftManagement();
        void SaveShiftManagement(ShiftManagementDiff diff);
    }


    public class ShiftManagementUseCase : IShiftManagementUseCase
    {
        private IShiftManagementData _data;

        public ShiftManagementUseCase(IShiftManagementData data)
        {
            _data = data;
        }

        public ShiftManagement GetShiftManagement()
        {
            return _data.GetShiftManagement();
        }

        public void SaveShiftManagement(ShiftManagementDiff diff)
        {
            _data.SaveShiftManagement(diff);
            _data.Commit();
        }
    }
}
