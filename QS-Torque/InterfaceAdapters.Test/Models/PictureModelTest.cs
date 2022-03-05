using Core.Entities;
using NUnit.Framework;
using System.IO;
using InterfaceAdapters.Models;

namespace InterfaceAdapters.Test.Models
{
    public class PictureModelTest
    {
        [Test]
        public void MapModelToEntity()
        {
            var model = new PictureModel(new Picture())
            {
                SeqId = 654987,
                NodeId = 147,
                NodeSeqId = 369,
                FileName = "saöldhkfösaklfd",
                FileType = 6230
            };

            var entity = model.Entity;

            Assert.AreEqual(model.SeqId, entity.SeqId);
            Assert.AreEqual(model.NodeId, entity.NodeId);
            Assert.AreEqual(model.NodeSeqId, entity.NodeSeqId);
            Assert.AreEqual(model.FileName, entity.FileName);
            Assert.AreEqual(model.FileType, entity.FileType);
        }
        
        [Test]
        public void MapEntityToModel()
        {
            var entity = new Picture()
            {
                SeqId = 654987,
                NodeId = 147,
                NodeSeqId = 369,
                FileName = "saöldhkfösaklfd",
                ImageStream = new MemoryStream(),
                FileType = 6230
            };

            var model = new PictureModel(entity);

            Assert.AreEqual(entity.SeqId, model.SeqId);
            Assert.AreEqual(entity.NodeId, model.NodeId);
            Assert.AreEqual(entity.NodeSeqId, model.NodeSeqId);
            Assert.AreEqual(entity.FileName, model.FileName);
            Assert.AreEqual(entity.FileType, model.FileType);
        }

        [TestCase(12, 75849, 234587, "gfhdsjkopiurijoklm", 478632586)]
        [TestCase(67589430, 8965123, 65457849, "iusrjpfoldskfklnvlkd", 142365856)]
        public void CopyPictureModelTest(long seqId, int nodeId, long nodeSeqId, string fileName, long fileType)
        {
            var model = new PictureModel(new Picture())
            {
                SeqId = seqId,
                NodeId = nodeId,
                NodeSeqId = nodeSeqId,
                FileName = fileName,
                FileType = fileType
            };

            var copy = model.CopyDeep();

            Assert.IsFalse(model == copy);
            Assert.IsTrue(model.SeqId.Equals(copy.SeqId));
            Assert.IsTrue(model.NodeId.Equals(copy.NodeId));
            Assert.IsTrue(model.NodeSeqId.Equals(copy.NodeSeqId));
            Assert.IsTrue(model.FileName.Equals(copy.FileName));
            Assert.IsTrue(model.FileType.Equals(copy.FileType));
        }
    }
}
