using Core.UseCases;
using NUnit.Framework;

namespace ServerConnections.Test.EntityTests
{
    class ServerConnectionTest
    {
        [Test]
        public void TestEquality()
        {
            var connection1 = new ServerConnection();
            var connection2 = new ServerConnection();

            Assert.AreEqual(connection1.Equals(connection2), true);
        }

        [Test]
        public void TestEqualityWithInitialisation()
        {
            var connection1 = new ServerConnection
            {
                HostName = "Bla",
                Port = 1234,
                PrincipalName = "Peter",
                ServerName = "Hubert"
            };
            var connection2 = new ServerConnection
            {
                HostName = "Bla",
                Port = 1234,
                PrincipalName = "Peter",
                ServerName = "Hubert"
            };

            Assert.AreEqual(connection1.Equals(connection2), true);
        }

        [Test]
        public void TestInequalityWithHostName()
        {
            var connection1 = new ServerConnection
            {
                HostName = "Baum"
            };
            var connection2 = new ServerConnection();

            Assert.AreEqual(connection1.Equals(connection2), false);
        }

        [Test]
        public void TestInequalityWithServerName()
        {
            var connection1 = new ServerConnection
            {
                ServerName = "Baum"
            };
            var connection2 = new ServerConnection();

            Assert.AreEqual(connection1.Equals(connection2), false);
        }

        [Test]
        public void TestInequalityWithPort()
        {
            var connection1 = new ServerConnection();
            var connection2 = new ServerConnection
            {
                Port = 555
            };

            Assert.AreEqual(connection1.Equals(connection2), false);
        }

        [Test]
        public void TestInequalityWithPrincipleName()
        {
            var connection1 = new ServerConnection();
            var connection2 = new ServerConnection
            {
                PrincipalName = "Hubert"
            };

            Assert.AreEqual(connection1.Equals(connection2), false);
        }
    }
}
