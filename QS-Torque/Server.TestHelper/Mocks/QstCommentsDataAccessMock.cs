using System.Collections.Generic;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using State;

namespace Server.TestHelper.Mocks
{
    public class QstCommentDataAccessMock : IQstCommentDataAccess
    {
        public HistoryComment GetQstCommentByNodeIdAndNodeSeqId(NodeId nodeId, long nodeSeqId)
        {
            GetQstCommentByNodeIdAndNodeSeqIdParameterNodeId = nodeId;
            GetQstCommentByNodeIdAndNodeSeqIdParameterNodeSeqId = nodeSeqId;
            return GetQstCommentByNodeIdAndNodeSeqIdReturnValue;
        }

        public void InsertQstComments(List<QstComment> comments)
        {
            InsertQstCommentsCalled = true;
            InsertQstCommentsParameter = comments;
        }

        public List<QstComment> InsertQstCommentsParameter { get; set; }
        public bool InsertQstCommentsCalled { get; set; }
        public HistoryComment GetQstCommentByNodeIdAndNodeSeqIdReturnValue { get; set; }
        public long GetQstCommentByNodeIdAndNodeSeqIdParameterNodeSeqId { get; set; }
        public NodeId GetQstCommentByNodeIdAndNodeSeqIdParameterNodeId { get; set; }
    }
}
