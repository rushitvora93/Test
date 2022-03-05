using System.Collections.Generic;
using NUnit.Framework;
using ServerConnections.UseCases;
using Core.UseCases;
using IServerConnectionStorage = ServerConnections.UseCases.IServerConnectionStorage;
using ServerConnections.Gui;
using FrameworksAndDrivers.Localization;
using ServerConnections.CommandLine;
using InterfaceAdapters.Localization;
using Client.Core;

namespace ServerConnections.Test.CommandLine
{
    public class ServerConnectonConsoleViewTest
    {
        class ConsoleWriterMock : IConsoleWriter
        {
            public void WriteLine(string message)
            {
                
            }
        }

        class ServerConnectionStorageMock : IServerConnectionStorage
        {
            List<ServerConnection> list_ = new List<ServerConnection>();
            public List<ServerConnection> GetServerConnections()
            {
                var erg = new List<ServerConnection>();
                erg.AddRange(list_);
                return erg;
            }

            public void SaveServerConnections(List<ServerConnection> connections)
            {
                list_ = new List<ServerConnection>();
                list_.AddRange(connections);
            }
        }

        class NullCatalogProxy : ICatalogProxy
        {
            public string GetParticularString(string context, string text)
            {
                return "";
            }

            public string GetString(string text)
            {
                return "";
            }
        }

        class NullLocalizationWrapper : LocalizationWrapper
        {
            public NullLocalizationWrapper() : base("")
            {
                Strings = new NullCatalogProxy();
            }
        }

        class ServerConnectionCheckerMock : IServerConnectionChecker
        {
            public bool ServerConnectionReachable(Core.UseCases.ServerConnection connection)
            {
                return true;
            }
        }

        [Test]
        public void AddingConnectionWithAllParametersResultsInNewEntryInConnectionList()
        {
            List<ServerConnection> referencelist = new List<ServerConnection>() { new ServerConnection() { ServerName = "qstserver01", HostName = "blubhost", Port = 43993, PrincipalName = "pname" } };
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            List<string> inputlist = new List<string> { "add", "servername=qstserver01", "hostname=blubhost", "port=43993", "principalname=pname" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(referencelist, resultlist);
            Assert.AreEqual(0, retval);
        }

        [Test]
        public void AddingConnectionWithOutPrincipalNameResultsInNewEntryInConnectionList()
        {
            List<ServerConnection> referencelist = new List<ServerConnection>() { new ServerConnection() { ServerName = "qstserver01", HostName = "blubhost", Port = 43993 } };
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "servername=qstserver01", "hostname=blubhost", "port=43993" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(referencelist, resultlist);
            Assert.AreEqual(0, retval);
        }

        [Test]
        public void TryAddingConnectionWithInvalidPortResultsInUnchangedConnectionList()
        {
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "servername=qstserver01", "hostname=blubhost", "port=70000", "principalname=pname" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(0, resultlist.Count);
            Assert.AreEqual((int)CommandLineParseReturnValues.INVALID_PORT, retval);
        }

        [Test]
        public void TryAddingConnectionWithEmptyPortResultsInUnchangedConnectionList()
        {
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "servername=qstserver01", "hostname=blubhost", "port=" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(0, resultlist.Count);
            Assert.AreEqual((int)CommandLineParseReturnValues.INVALID_PORT, retval);
        }

        [Test]
        public void TryAddingConnectionWithEmptyHostNameResultsInUnchangedConnectionList()
        {
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "servername=qstserver01", "hostname=", "port=43993" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(0, resultlist.Count);
            Assert.AreEqual((int)CommandLineParseReturnValues.INVALID_HOSTNAME, retval);
        }

        [Test]
        public void TryAddingConnectionWithEmptyServerNameResultsInUnchangedConnectionList()
        {
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "servername=", "hostname=blubhost", "port=43993" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(0, resultlist.Count);
            Assert.AreEqual((int)CommandLineParseReturnValues.INVALID_SERVERNAME, retval);
        }

        [Test]
        public void TryAddingConnectionWithNoPortResultsInUnchangedConnectionList()
        {
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "servername=qstserver01", "hostname=blubhost"};
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(0, resultlist.Count);
            Assert.AreEqual((int)CommandLineParseReturnValues.INVALID_PORT, retval);
        }

        [Test]
        public void TryAddingConnectionWithNoHostNameResultsInUnchangedConnectionList()
        {
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "servername=qstserver01", "port=43993" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(0, resultlist.Count);
            Assert.AreEqual((int)CommandLineParseReturnValues.INVALID_HOSTNAME, retval);
        }

        [Test]
        public void TryAddingConnectionWithNoServerNameResultsInUnchangedConnectionList()
        {
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "hostname=blubhost", "port=43993" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(0, resultlist.Count);
            Assert.AreEqual((int)CommandLineParseReturnValues.INVALID_SERVERNAME, retval);
        }

        [Test]
        public void TryAddingConnectionWithNoParamtersResultsInUnchangedConnectionList()
        {
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(0, resultlist.Count);
            Assert.AreEqual((int)CommandLineParseReturnValues.INVALID_HOSTNAME | (int)CommandLineParseReturnValues.INVALID_SERVERNAME | (int)CommandLineParseReturnValues.INVALID_PORT, retval);
        }

        [Test]
        public void TryCallingInvalidCommandResultsInUnchangedConnectionListt()
        {
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "komischescommand", "servername=qstserver01", "hostname=blubhost", "port=43993" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(0, resultlist.Count);
            Assert.AreEqual((int)CommandLineParseReturnValues.INVALID_COMMAND, retval);
        }

        [Test]
        public void AddingConnectionWithAdditonalUndknownParamtersResultsInNewEntryInConnectionList()
        {
           List<ServerConnection> referencelist = new List<ServerConnection>() { new ServerConnection() { ServerName = "qstserver01", HostName = "blubhost", Port = 43993 } };
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "servername=qstserver01", "hostname=blubhost", "port=43993", "ich schreib mal irgendwas dazu " };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(referencelist, resultlist);
            Assert.AreEqual(0, retval);
        }

        [Test]
        public void TryAddingConnectionWithInvalidServerNameKeyResultsInUnchangedConnectionList()
        {
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "servename=qstserver01", "hostname=blubhost", "port=43993" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(0, resultlist.Count);
            Assert.AreEqual((int)CommandLineParseReturnValues.INVALID_SERVERNAME, retval);
        }

        [Test]
        public void TryAddingConnectionWithInvalidHostNameKeyResultsInUnchangedConnectionList()
        {
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "servername=qstserver01", "hostame=blubhost", "port=43993" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(0, resultlist.Count);
            Assert.AreEqual((int)CommandLineParseReturnValues.INVALID_HOSTNAME, retval);
        }

        [Test]
        public void TryAddingConnectionWithNothingAtAllResultsInUnchangedConnectionList()
        {
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(0, resultlist.Count);
            Assert.AreEqual((int)CommandLineParseReturnValues.INVALID_COMMAND | (int)CommandLineParseReturnValues.INVALID_HOSTNAME | (int)CommandLineParseReturnValues.INVALID_SERVERNAME | (int)CommandLineParseReturnValues.INVALID_PORT, retval);
        }

        [Test]
        public void TryAddingConnectionWithInvalidPortKeyResultsInUnchangedConnectionList()
        {
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "servername=qstserver01", "hostname=blubhost", "prt=43993" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(0, resultlist.Count);
            Assert.AreEqual((int)CommandLineParseReturnValues.INVALID_PORT, retval);
        }

        [Test]
        public void AddingConnectionWithMixedCaseLettersInCommandResultsInNewEntryInConnectionList()
        {
            List<ServerConnection> referencelist = new List<ServerConnection>() { new ServerConnection() { ServerName = "qstserver01", HostName = "blubhost", Port = 43993 } };
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "aDd", "servername=qstserver01", "hostname=blubhost", "port=43993" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(referencelist, resultlist);
            Assert.AreEqual(0, retval);
        }

        [Test]
        public void AddingConnectionWithMixedCaseLettersInServerNameKeyResultsInNewEntryInConnectionList()
        {
            List<ServerConnection> referencelist = new List<ServerConnection>() { new ServerConnection() { ServerName = "qstserver01", HostName = "blubhost", Port = 43993 } };
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "serverName=qstserver01", "hostname=blubhost", "port=43993" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(referencelist, resultlist);
            Assert.AreEqual(0, retval);
        }

        [Test]
        public void AddingConnectionWithMixedCaseLettersInHostNameKeyResultsInNewEntryInConnectionList()
        {
            List<ServerConnection> referencelist = new List<ServerConnection>() { new ServerConnection() { ServerName = "qstserver01", HostName = "blubhost", Port = 43993 } };
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "servername=qstserver01", "hostnAme=blubhost", "port=43993" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(referencelist, resultlist);
            Assert.AreEqual(0, retval);
        }

        [Test]
        public void AddingConnectionWithMixedCaseLettersInPortKeyResultsInNewEntryInConnectionList()
        {           
            List<ServerConnection> referencelist = new List<ServerConnection>() { new ServerConnection() { ServerName = "qstserver01", HostName = "blubhost", Port = 43993 } };
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "servername=qstserver01", "hostname=blubhost", "pOrt=43993" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(referencelist, resultlist);
            Assert.AreEqual(0, retval);
        }

        [Test]
        public void AddingConnectionWithMixedCaseLettersInPrincipalNameKeyResultsInNewEntryInConnectionList()
        {
            List<ServerConnection> referencelist = new List<ServerConnection>() { new ServerConnection() { ServerName = "qstserver01", HostName = "blubhost", Port = 43993, PrincipalName = "pname" } };            
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "servername=qstserver01", "hostname=blubhost", "port=43993", "principaLname=pname" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(referencelist, resultlist);
            Assert.AreEqual(0, retval);
        }

        [Test]
        public void AddingConnectionWithMixedCaseLettersInParametersResultsInNewEntryInConnectionList()
        {
            List<ServerConnection> referencelist = new List<ServerConnection>() { new ServerConnection() { ServerName = "qsTserver01", HostName = "bluBhost", Port = 43993, PrincipalName = "pnaMe" } };
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "servername=qsTserver01", "hostname=bluBhost", "port=43993", "principaLname=pnaMe" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(referencelist, resultlist);
            Assert.AreEqual(0, retval);
        }

        [Test]
        public void AddingConnectionWithSpacesInParametersResultsInNewEntryInConnectionList()
        {
            List<ServerConnection> referencelist = new List<ServerConnection>() { new ServerConnection() { ServerName = "qst server01", HostName = "blub host", Port = 43993, PrincipalName = "p name" } };
            ServerConnectionStorageMock storagemock = new ServerConnectionStorageMock();
            var inputlist = new List<string> { "add", "servername=qst server01", "hostname=blub host", "port=43993", "principaLname=p name" };
            ServerConnectionUseCaseFactory useCaseFactory = new ServerConnectionUseCaseFactory(storagemock, new ServerConnectionCheckerMock());
            ServerConnectionConsoleView consoleview = new ServerConnectionConsoleView(useCaseFactory, new NullLocalizationWrapper(), inputlist.ToArray(), new ConsoleWriterMock());
            var retval = consoleview.Execute();
            var resultlist = storagemock.GetServerConnections();
            Assert.AreEqual(referencelist, resultlist);
            Assert.AreEqual(0, retval);
        }
    }
}
