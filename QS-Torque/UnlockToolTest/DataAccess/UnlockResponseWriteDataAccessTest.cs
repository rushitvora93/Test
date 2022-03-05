using System.IO;
using NUnit.Framework;
using UnlockTool.DataAccess;
using UnlockToolShared.DataAccess;
using UnlockToolShared.Entities;

namespace UnlockToolTest.DataAccess
{
    public class UnlockResponseWriteDataAccessTest
    {
        [Test]
        public void WriteAndReadResultsInSameObject()
        {
            var dar = new UnlockResponseReadDataAccess();
            var daw = new UnlockResponseWriteDataAccess();
            var origUr = new UnlockResponse();
            origUr.PackageComment = "PackageComment";
            origUr.QstVersion = "QstVersion";
            origUr.UnlockResponseVersion = 1;
            var license = new SpecificLicense();
            license.PcFqdns.Add("PcFqdns1");
            license.PcFqdns.Add("PcFqdns2");
            license.LicenseComment = "LicenseComment1";
            origUr.Licenses.Add(license);
            using (var stream = new MemoryStream())
            {
                daw.WriteUnlockResponse(origUr,stream);
                stream.Position = 0;
                var ur = dar.ReadUnlockResponse(stream);
                Assert.AreEqual(origUr.PackageComment, ur.PackageComment);
                Assert.AreEqual(origUr.QstVersion, ur.QstVersion);
                Assert.AreEqual(origUr.UnlockResponseVersion, ur.UnlockResponseVersion);
                Assert.AreEqual(origUr.Licenses[0].LicenseComment, ur.Licenses[0].LicenseComment);
                Assert.AreEqual(origUr.Licenses[0].PcFqdns[0], ur.Licenses[0].PcFqdns[0]);
                Assert.AreEqual(origUr.Licenses[0].PcFqdns[1], ur.Licenses[0].PcFqdns[1]);
            }
        }
    }
}


