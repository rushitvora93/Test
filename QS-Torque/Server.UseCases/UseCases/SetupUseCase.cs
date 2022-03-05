using System.Collections.Generic;
using Server.Core.Entities;

namespace Server.UseCases.UseCases
{
    public interface ISetupUseCase
    {
        List<QstSetup> GetQstSetupsByUserIdAndName(long userId, string name);
        List<QstSetup> GetColumnWidthsForGrid(long userId, string gridName, List<string> columns);
        List<QstSetup> InsertOrUpdateQstSetups(List<QstSetup> qstSetups, bool returnList);
    }

    public interface ISetupDataAccess
    {
        void Commit();
        List<QstSetup> GetQstSetupsByUserIdAndName(long userId, string name);
        List<QstSetup> GetColumnWidthsForGrid(long userId, string gridName, List<string> columns);
        List<QstSetup> InsertOrUpdateQstSetups(List<QstSetup> qstSetups, bool returnList);
    }

    public class SetupUseCase : ISetupUseCase
    {
        private readonly ISetupDataAccess _setupDataAccess;

        public SetupUseCase(ISetupDataAccess setupDataAccess)
        {
            _setupDataAccess = setupDataAccess;
        }
        public List<QstSetup> GetQstSetupsByUserIdAndName(long userId, string name)
        {
            return _setupDataAccess.GetQstSetupsByUserIdAndName(userId, name);
        }

        public List<QstSetup> GetColumnWidthsForGrid(long userId, string gridName, List<string> columns)
        {
            return _setupDataAccess.GetColumnWidthsForGrid(userId, gridName, columns);
        }

        public List<QstSetup> InsertOrUpdateQstSetups(List<QstSetup> qstSetups, bool returnList)
        {
            var setups = _setupDataAccess.InsertOrUpdateQstSetups(qstSetups, returnList);
            _setupDataAccess.Commit();
            return setups;
        }
    }
}
