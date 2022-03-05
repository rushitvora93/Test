using Core;

namespace Server.Core.Entities
{
    public class Picture : IQstEquality<Picture>, ICopy<Picture>
    {
        public long SeqId { get; set; }
        public int NodeId { get; set; }
        public long NodeSeqId { get; set; }
        public string FileName { get; set; }
        public byte[] PictureBytes { get; set; }
        public long FileType { get; set; }
        public bool EqualsById(Picture other)
        {
            return this.SeqId == other?.SeqId;
        }

        public bool EqualsByContent(Picture other)
        {
            if (other == null)
            {
                return false;
            }

            return this.SeqId == other.SeqId &&
                   this.NodeId == other.NodeId &&
                   this.NodeSeqId == other.NodeSeqId &&
                   this.FileName == other.FileName &&
                   this.FileType == other.FileType;
        }

        public Picture CopyDeep()
        {
            return new Picture()
            {
                SeqId = this.SeqId,
                NodeId = this.NodeId,
                NodeSeqId = this.NodeSeqId,
                FileName = this.FileName,
                PictureBytes = this.PictureBytes,
                FileType = this.FileType
            };
        }
    }
}
