using System.IO;
using System.Text;
using NUnit.Framework;
using UnlockTool.DataAccess;

namespace UnlockToolTest.DataAccess
{
    public class UnlockRequestDataAccessTest
    {
        private byte[] goodFile = Encoding.UTF8.GetBytes(
            "qstversion=8.0.0.0" + "\r\n" +
            "unlockrequestversion=1" + "\r\n" +
            "name=" + "\r\n" +
            "phonenumber=" + "\r\n" +
            "company=" + "\r\n" +
            "address=" + "\r\n" +
            "email=" + "\r\n" +
            "windows=10.0.19041 SP0" + "\r\n" +
            "pcname=PROGNOSTIX" + "\r\n" +
            "fqdn=Prognostix.CSP.local" + "\r\n" +
            "logedinusername=franzw" + "\r\n" +
            "currentlocaldatetime=20200918152440" + "\r\n" +
            "currentutcdatetime=20200918132440" + "\r\n" +
            "0996953BBF61D4BFD52E240314A5B3BA6101DE1FBB283CF158A5E87D6C62632716BBB55F91D82626CC8DD3F67AEB34F56D99349D062A2237D29413796FE04EF8"
        );

        private byte[] noHashCodeFile = Encoding.UTF8.GetBytes(
            "qstversion=8.0.0.0" + "\r\n" +
            "unlockrequestversion=1" + "\r\n" +
            "name=" + "\r\n" +
            "phonenumber=" + "\r\n" +
            "company=" + "\r\n" +
            "address=" + "\r\n" +
            "email=" + "\r\n" +
            "windows=10.0.19041 SP0" + "\r\n" +
            "pcname=PROGNOSTIX" + "\r\n" +
            "fqdn=Prognostix.CSP.local" + "\r\n" +
            "logedinusername=franzw" + "\r\n" +
            "currentlocaldatetime=20200918152440" + "\r\n" +
            "currentutcdatetime=20200918132440" + "\r\n" 
        );

        private byte[] wrongHashCodeFile = Encoding.UTF8.GetBytes(
            "qstversion=8.0.0.0" + "\r\n" +
            "unlockrequestversion=1" + "\r\n" +
            "name=wrongname" + "\r\n" +
            "phonenumber=wrongphone" + "\r\n" +
            "company=wrongcompany" + "\r\n" +
            "address=wrongaddress" + "\r\n" +
            "email=wrongemail" + "\r\n" +
            "windows=10.0.19041 SP0" + "\r\n" +
            "pcname=PROGNOSTIX" + "\r\n" +
            "fqdn=Prognostix.CSP.local" + "\r\n" +
            "logedinusername=franzw" + "\r\n" +
            "currentlocaldatetime=20200918152440" + "\r\n" +
            "currentutcdatetime=20200918132440" + "\r\n" +
            "0996953BBF61D4BFD52E240314A5B3BA6101DE1FBB283CF158A5E87D6C62632716BBB55F91D82626CC8DD3F67AEB34F56D99349D062A2237D29413796FE04EF8"
        );
        [Test]
        public void GetUnlockRequestFromWrongHashCodeFile()
        {
            using (var stream = new MemoryStream())
            {
                stream.Write(wrongHashCodeFile);
                stream.Position = 0;
                var da = new UnlockRequestDataAccess();
                var unlockRequest = da.GetUnlockRequestFromStream(stream);
                Assert.AreEqual("8.0.0.0", unlockRequest.QSTVersion);
                Assert.AreEqual(1, unlockRequest.UnlockRequestVersion);
                Assert.AreEqual("wrongname", unlockRequest.Name);
                Assert.AreEqual("wrongphone", unlockRequest.Phonenumber);
                Assert.AreEqual("wrongcompany", unlockRequest.Company);
                Assert.AreEqual("wrongaddress", unlockRequest.Address);
                Assert.AreEqual("wrongemail", unlockRequest.Email);
                Assert.AreEqual("10.0.19041 SP0", unlockRequest.Windows);
                Assert.AreEqual("PROGNOSTIX", unlockRequest.PCName);
                Assert.AreEqual("Prognostix.CSP.local", unlockRequest.FQDN);
                Assert.AreEqual("franzw", unlockRequest.LogedinUserName);
                Assert.AreEqual("20200918152440", unlockRequest.CurrentLocalDateTime);
                Assert.AreEqual("20200918132440", unlockRequest.CurrentUtcDateTime);
                Assert.IsFalse(unlockRequest.HashOk);
            }
        }

        [Test]
        public void GetUnlockRequestFromGoodFile()
        {
            using (var stream = new MemoryStream())
            {
                stream.Write(goodFile);
                stream.Position = 0;
                var da = new UnlockRequestDataAccess();
                var unlockRequest = da.GetUnlockRequestFromStream(stream);
                Assert.AreEqual("8.0.0.0", unlockRequest.QSTVersion);
                Assert.AreEqual(1, unlockRequest.UnlockRequestVersion);
                Assert.AreEqual("", unlockRequest.Name);
                Assert.AreEqual("", unlockRequest.Phonenumber);
                Assert.AreEqual("", unlockRequest.Company);
                Assert.AreEqual("", unlockRequest.Address);
                Assert.AreEqual("", unlockRequest.Email);
                Assert.AreEqual("10.0.19041 SP0", unlockRequest.Windows);
                Assert.AreEqual("PROGNOSTIX", unlockRequest.PCName);
                Assert.AreEqual("Prognostix.CSP.local", unlockRequest.FQDN);
                Assert.AreEqual("franzw", unlockRequest.LogedinUserName);
                Assert.AreEqual("20200918152440", unlockRequest.CurrentLocalDateTime);
                Assert.AreEqual("20200918132440", unlockRequest.CurrentUtcDateTime);
                Assert.IsTrue(unlockRequest.HashOk);
            }
        }

        [Test]
        public void GetUnlockRequestFromNoHashCodeFile()
        {
            using (var stream = new MemoryStream())
            {
                stream.Write(noHashCodeFile);
                stream.Position = 0;
                var da = new UnlockRequestDataAccess();
                var unlockRequest = da.GetUnlockRequestFromStream(stream);
                Assert.IsFalse(unlockRequest.HashOk);
            }
        }
    }
}


