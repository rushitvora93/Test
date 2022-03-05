using System;
using System.Collections.Generic;
using System.Linq;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using Microsoft.EntityFrameworkCore;
using Server.Core.Entities;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{

    public class ClassicTestDataAccess : DataAccessBase, IClassicTestDataAccess
    {
        private readonly Mapper _mapper = new Mapper();

        public ClassicTestDataAccess(ITransactionDbContext transactionContext, SqliteDbContext dbContext) 
            : base(transactionContext, dbContext) { }


        private class TestToolData
        {
            public long PowId { get; set; }
            public DateTime Timestamp { get; set; }

        }

        public Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)> GetToolsFromLocationTests(LocationId locationId)
        {
            var mfuTests = _dbContext.ClassicMfuTestLocations.Where(x => x.LOCATION_ID == locationId.ToLong())
                .Include(x => x.ClassicMfuTest.GlobalHistory)
                .Select(x => new TestToolData()
                {
                    PowId = x.ClassicMfuTest.POW_TOOL_ID,
                    Timestamp = x.ClassicMfuTest.GlobalHistory.TIMESTAMP.Value
                });

            var chkTests = _dbContext.ClassicChkTestLocations.Where(x => x.LOCATION_ID == locationId.ToLong())
                .Include(x => x.ClassicChkTest.GlobalHistory)
                .Select(x => new TestToolData()
                {
                    PowId = x.ClassicChkTest.POW_TOOL_ID,
                    Timestamp = x.ClassicChkTest.GlobalHistory.TIMESTAMP.Value
                });

            var testData = mfuTests.Union(chkTests).ToList();

            var allPowToolIdsFromTests = testData.GroupBy(x => x.PowId).ToList().Select(x => x.Key).ToList();

            var allToolsFromTests = _dbContext.Tools.Where(x => allPowToolIdsFromTests.Contains(x.SEQID)).Include(x => x.ToolModel).ThenInclude(x => x.Manufacturer).ToList();
            var tools = new List<Tool>();
            allToolsFromTests.ForEach(x => tools.Add(_mapper.DirectPropertyMapping(x)));

            var powToolGroups = testData.GroupBy(x => x.PowId).ToList();

            var assignedPowTools = _dbContext.LocPows.Where(x => allPowToolIdsFromTests.Contains(x.PowId.GetValueOrDefault(-1)) && x.Alive == true)
                .Select(x => x.PowId)
                .ToList();

            var result = new Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)>();

            foreach (var powToolGroup in powToolGroups)
            {
                var tool = tools.SingleOrDefault(x => x.Id.ToLong() == powToolGroup.Key);
                if (tool == null)
                {
                    continue;
                }

                var firstTest = powToolGroup.Min(x => x.Timestamp);
                var lastTest = powToolGroup.Max(x => x.Timestamp);
                var isToolAssignmentActive = assignedPowTools.Contains(tool.Id.ToLong());
                result[tool] = (firstTest, lastTest, isToolAssignmentActive);
            }

            return result;
        }
    }
}
