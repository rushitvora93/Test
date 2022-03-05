using System.Collections.Generic;
using System.Linq;
using Common.Types.Enums;
using Core.Entities;
using FrameworksAndDrivers.DataAccess.DbContext;
using Microsoft.EntityFrameworkCore;
using Server.Core;
using Server.Core.Entities;
using Server.Core.Enums;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class TransferToTestEquipmentDataAccess : DataAccessBase, ITransferToTestEquipmentDataAccess
    {
        public TransferToTestEquipmentDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext)
            : base(transactionContext, dbContext)
        { }

        public List<LocationToolAssignmentForTransfer> LoadLocationToolAssignmentsForTransfer(TestType testType)
        {
            var dataQuery = _dbContext.CondRots
                .Include(x => x.LocPow)
                .Include(x => x.LocPow.Location)
                .Include(x => x.LocPow.Tool)
                .Include(x => x.LocPow.ToolUsage)
                .Where(x => x.LocPow.Location.ALIVE == true && x.LocPow.Tool.ALIVE == true && x.LocPow.Alive == true);

            if (testType == TestType.Chk)
            {
                dataQuery = dataQuery.Where(x => x.PLANOK == true);
            }
            else if (testType == TestType.Mfu)
            {
                dataQuery = dataQuery.Where(x => x.PLANOK_MFU == true);
            }

            var list = new List<LocationToolAssignmentForTransfer>();

            foreach (var condRot in dataQuery)
            {
                var testLevelSetId = (testType == TestType.Chk ? condRot.TestLevelSetIdChk : condRot.TestLevelSetIdMfu) ?? 0;
                var testLevelNumber = (testType == TestType.Chk ? condRot.TestLevelNumberChk : condRot.TestLevelNumberMfu) ?? 0;
                var testLevel = _dbContext.TestLevels.FirstOrDefault(x => x.TestLevelSetId == testLevelSetId && x.TestLevelNumber == testLevelNumber) ??
                                new DbEntities.TestLevel();

                list.Add(new LocationToolAssignmentForTransfer()
                {
                    LocationId = new LocationId(condRot.LocPow.LocId.GetValueOrDefault(0)),
                    LocationNumber = new LocationNumber(condRot.LocPow.Location.USERID),
                    LocationDescription = new LocationDescription(condRot.LocPow.Location.NAME),
                    LocationFreeFieldCategory = condRot.LocPow.Location.KATEG,
                    LocationFreeFieldDocumentation = condRot.LocPow.Location.DOKU.GetValueOrDefault(false),
                    LocationToolAssignmentId = new LocationToolAssignmentId(condRot.LOCPOWID),
                    ToolId = new ToolId(condRot.LocPow.Tool.SEQID),
                    ToolInventoryNumber = condRot.LocPow.Tool.USERNO,
                    ToolSerialNumber = condRot.LocPow.Tool.SERIALNO,
                    ToolUsageId = new HelperTableEntityId(condRot.LocPow.ToolUsage.POSID),
                    ToolUsageDescription = new ToolUsageDescription(condRot.LocPow.ToolUsage.POSNAME),
                    SampleNumber = (int)testLevel.SampleNumber.GetValueOrDefault(0),
                    TestInterval = new Interval()
                    {
                        IntervalValue = testLevel.IntervalValue.GetValueOrDefault(0),
                        Type = (IntervalType?)testLevel.IntervalType ?? IntervalType.EveryXDays
                    },
                    NextTestDate = testType == TestType.Chk ? condRot.NEXT_CHK : condRot.NEXT_MFU,
                    LastTestDate = testType == TestType.Chk ? condRot.LAST_CHK : condRot.LAST_MFU,
                    NextTestDateShift = testType == TestType.Chk ? (Shift?)condRot.NextChkShift : (Shift?)condRot.NextMfuShift
                });
            }

            return list;
        }

        public List<ProcessControlForTransfer> LoadProcessControlDataForTransfer()
        {
            var processControlForTransfer = new List<ProcessControlForTransfer>();
            var condLocData = _dbContext.CondLocs
                .Include(x => x.Location)
                .Include(x => x.CondLocTechs.Where(y => y.HERSTELLERID == ManufacturerIds.ID_QST && y.ALIVE))
                .Where(x => x.ALIVE && x.Location.ALIVE == true && x.CondLocTechs.Count > 0 && x.PLANOK == true).ToList();

            foreach (var data in condLocData)
            {
                var condLocTech = data.CondLocTechs.Single();

                //Todo: TestLevelSet aus foreach nehmen!
                var testLevelSetId = data.TESTLEVELSETID.GetValueOrDefault(0);
                var testLevelNumber = data.TESTLEVELNUMBER;
                var testLevel = _dbContext.TestLevels.FirstOrDefault(x =>
                        x.TestLevelSetId == testLevelSetId && x.TestLevelNumber == testLevelNumber) ??
                        new DbEntities.TestLevel();

                var controlForTransfer = new ProcessControlForTransfer()
                {
                    LocationId = new LocationId(data.Location.SEQID),
                    LocationNumber = new LocationNumber(data.Location.USERID),
                    LocationDescription = new LocationDescription(data.Location.NAME),
                    ProcessControlConditionId = new ProcessControlConditionId(data.SEQID),
                    TestMethod = condLocTech.METHODE,
                    MinimumTorque = data.MDMIN,
                    MaximumTorque = data.MDMAX,
                    SetPointTorque = (data.MDMIN + data.MDMAX) / 2,
                    ProcessControlTechId = new ProcessControlTechId(condLocTech.SEQID),
                    LastTestDate = data.LAST_CTL,
                    NextTestDate = data.NEXT_CTL,
                    TestInterval = new Interval()
                    {
                        IntervalValue = testLevel.IntervalValue.GetValueOrDefault(0),
                        Type = (IntervalType?)testLevel.IntervalType ?? IntervalType.EveryXDays
                    },
                    SampleNumber = (int)testLevel.SampleNumber.GetValueOrDefault(0),
                    NextTestDateShift = (Shift?)data.NEXTSHIFT
                };

                processControlForTransfer.Add(controlForTransfer);
            }
            return processControlForTransfer;
        }
    }
}
