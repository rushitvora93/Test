using System;
using System.Collections.Generic;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using State;

namespace Server.UseCases.UseCases
{
    public interface IManufacturerUseCase
    {
        List<Manufacturer> LoadManufacturers();
        string GetManufacturerComment(ManufacturerId manufacturerId);
        List<ToolModelReferenceLink> GetManufacturerModelLinks(ManufacturerId manufacturerId);
        List<Manufacturer> InsertManufacturersWithHistory(List<ManufacturerDiff> manufacturerDiffs, bool returnList);
        List<Manufacturer> UpdateManufacturersWithHistory(List<ManufacturerDiff> manufacturerDiffs);
    }

    public interface IManufacturerDataAccess
    {
        void Commit();
        List<Manufacturer> LoadManufacturers();
        List<ToolModelReferenceLink> GetManufacturerModelLinks(ManufacturerId manufacturerId);
        List<Manufacturer> InsertManufacturersWithHistory(List<ManufacturerDiff> manufacturerDiffs, bool returnList);
        List<Manufacturer> UpdateManufacturersWithHistory(List<ManufacturerDiff> manufacturerDiffs);
    }

    public interface IGlobalHistoryDataAccess
    {
        long CreateGlobalHistory(string type, DateTime currentTimestamp);
    }

    public interface IQstCommentDataAccess
    {
        HistoryComment GetQstCommentByNodeIdAndNodeSeqId(NodeId nodeId, long nodeSeqId);
        void InsertQstComments(List<QstComment> comments);
    }

    public class QstComment
    {
        public UserId UserId { get; set; }
        public HistoryComment Comment { get; set; }
        public long NodeId { get; set; }
        public long NodeSeqid { get; set; }
    }

    public class ManufacturerUseCase : IManufacturerUseCase
    {
        private readonly IManufacturerDataAccess _manufacturerDataAccess;
        private readonly IQstCommentDataAccess _qstCommentDataAccess;

        public ManufacturerUseCase(IManufacturerDataAccess manufacturerDataAccess, IQstCommentDataAccess qstCommentDataAccess)
        {
            _manufacturerDataAccess = manufacturerDataAccess;
            _qstCommentDataAccess = qstCommentDataAccess;
        }

        public List<Manufacturer> LoadManufacturers()
        {
            return _manufacturerDataAccess.LoadManufacturers();
        }

        public string GetManufacturerComment(ManufacturerId manufacturerId)
        {
            var comment = _qstCommentDataAccess.GetQstCommentByNodeIdAndNodeSeqId(NodeId.Manufacturer,
                manufacturerId.ToLong());

            return comment == null ? "" : comment.ToDefaultString();
        }

        public List<ToolModelReferenceLink> GetManufacturerModelLinks(ManufacturerId manufacturerId)
        {
            return _manufacturerDataAccess.GetManufacturerModelLinks(manufacturerId);
        }

        public List<Manufacturer> InsertManufacturersWithHistory(List<ManufacturerDiff> manufacturerDiffs, bool returnList)
        {
            var manufacturers = _manufacturerDataAccess.InsertManufacturersWithHistory(manufacturerDiffs, returnList);
            _manufacturerDataAccess.Commit();
            return manufacturers;
        }

        public List<Manufacturer> UpdateManufacturersWithHistory(List<ManufacturerDiff> manufacturerDiffs)
        {
            var manufacturers = _manufacturerDataAccess.UpdateManufacturersWithHistory(manufacturerDiffs);
            _qstCommentDataAccess.InsertQstComments(GetQstComments(manufacturerDiffs));
            _manufacturerDataAccess.Commit();
            return manufacturers;
        }

        private List<QstComment> GetQstComments(List<ManufacturerDiff> diffs)
        {
            var comments = new List<QstComment>();
            if (diffs == null)
            {
                return comments;
            }

            foreach (var diff in diffs)
            {
                if (diff.GetOldManufacturer().Comment == diff.GetNewManufacturer().Comment)
                {
                    continue;
                }

                var comment = new QstComment()
                {
                    Comment = new HistoryComment(diff.GetNewManufacturer().Comment),
                    UserId = diff.GetUser().UserId,
                    NodeId = (long)NodeId.Manufacturer,
                    NodeSeqid = diff.GetNewManufacturer().Id.ToLong()
                };

                comments.Add(comment);
            }

            return comments;
        }
    }
}
