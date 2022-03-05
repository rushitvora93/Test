using System.Collections.Generic;
using System.Linq;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using Server.Core.Entities;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class SetupDataAccess : DataAccessBase, ISetupDataAccess
    {
        private readonly Mapper _mapper = new Mapper();
        public SetupDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext)
            : base(transactionContext, dbContext)
        { }

        public List<QstSetup> GetQstSetupsByUserIdAndName(long userId, string name)
        {
            var dbSetups = _dbContext.QstSetups.Where(s => s.LUserId == userId && s.SName == name).OrderBy(s => s.SName).ToList();
            var setups = new List<QstSetup>();
            dbSetups.ForEach(s => setups.Add(_mapper.DirectPropertyMapping(s)));
            return setups;
        }

        public List<QstSetup> GetColumnWidthsForGrid(long userId, string gridName, List<string> columns)
        {
            var namesToSearch = columns.Select(x => $"{gridName}{x}").ToList();
            var dbSetups = _dbContext.QstSetups.Where(s => s.LUserId == userId && namesToSearch.Contains(s.SName)).OrderBy(s => s.SName).ToList();
            var setups = new List<QstSetup>();
            dbSetups.ForEach(s => setups.Add(_mapper.DirectPropertyMapping(s)));
            return setups;
        }

        public List<QstSetup> InsertOrUpdateQstSetups(List<QstSetup> qstSetups, bool returnList)
        {
            var returnSetups = new List<QstSetup>();
            foreach (var setup in qstSetups)
            {
                var existingSetups = _dbContext.QstSetups
                    .Where(s => s.LUserId == setup.UserId && s.SName == setup.Name.ToDefaultString())
                    .OrderBy(s => s.SName).ToList();

                if (existingSetups.Count <= 0)
                {
                    var newSetup = _mapper.DirectPropertyMapping(setup);
                    _dbContext.QstSetups.Add(newSetup);
                    _dbContext.SaveChanges();

                    setup.Id = new QstSetupId(newSetup.LID);
                    returnSetups.Add(setup);
                }
                else
                {
                    foreach (var existingSetup in existingSetups)
                    {
                        existingSetup.SText = setup.Value.ToDefaultString();
                        returnSetups.Add(_mapper.DirectPropertyMapping(existingSetup));
                    }
                }
            }

            _dbContext.SaveChanges();

            return returnList ? returnSetups : null;
        }
    }
}