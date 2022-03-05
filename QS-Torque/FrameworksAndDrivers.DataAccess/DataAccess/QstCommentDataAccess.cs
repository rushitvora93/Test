using System;
using System.Collections.Generic;
using System.Linq;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using State;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class QstCommentDataAccess : DataAccessBase, IQstCommentDataAccess
    {
        public QstCommentDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext)
            : base(transactionContext, dbContext)
        { }

        public HistoryComment GetQstCommentByNodeIdAndNodeSeqId(NodeId nodeId, long nodeSeqId)
        {
            var dbComment = _dbContext.QstComments.Where(x => x.NODEID == (long) nodeId && x.NODESEQID == nodeSeqId)
                .OrderByDescending(x => x.TSA).FirstOrDefault();

            return dbComment == null ? null : new HistoryComment(dbComment.INFO);
        }

        public void InsertQstComments(List<QstComment> comments)
        {
            var mapper = new Mapper();
            foreach (var comment in comments)
            {
                var dbComment = mapper.DirectPropertyMapping(comment);
                dbComment.TSA = DateTime.Now;
                dbComment.TSN = dbComment.TSA;
                _dbContext.QstComments.Add(dbComment);
            }

            _dbContext.SaveChanges();
        }
    }
}
