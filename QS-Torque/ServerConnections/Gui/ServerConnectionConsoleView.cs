using Core.UseCases;
using FrameworksAndDrivers.Localization;
using ServerConnections.UseCases;
using System;
using System.Collections.Generic;
using ServerConnections.CommandLine;

namespace ServerConnections.Gui
{
    public interface IConsoleWriter
    {
        void WriteLine(string message);
    }

    public class HumbleConsoleWriter : IConsoleWriter
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class ServerConnectionConsoleView : IServerConnectionGui
    {        
        private ServerConnectionUseCaseFactory useCaseFactory_;
        private LocalizationWrapper localization_;
        private string[] args_;
        private List<ServerConnection> connections_;
        private IConsoleWriter consoleWriter_;

        public ServerConnectionConsoleView(ServerConnectionUseCaseFactory useCaseFactory, LocalizationWrapper localization, string[] args, IConsoleWriter consoleWriter)
        {
            this.useCaseFactory_ = useCaseFactory;
            this.localization_ = localization;
            this.args_ = args;
            this.consoleWriter_ = consoleWriter;
        }

        List<string> getResponseText(CommandLineParseReturnValues enumval)
        {
            List<string> erg = new List<string>();
            if (((int)enumval & (int)CommandLineParseReturnValues.INVALID_COMMAND) > 0)
            {
                erg.Add(localization_.Strings.GetString("invalid command"));
            }

            if (((int)enumval & (int)CommandLineParseReturnValues.INVALID_HOSTNAME) > 0)
            {
                erg.Add(localization_.Strings.GetString("invalid hostname"));
            }

            if (((int)enumval & (int)CommandLineParseReturnValues.INVALID_HOSTNAME) > 0)
            {
                erg.Add(localization_.Strings.GetString("invalid servername"));
            }

            if (((int)enumval & (int)CommandLineParseReturnValues.INVALID_PORT) > 0)
            {
                erg.Add(localization_.Strings.GetString("invalid port"));
            }

            if (enumval == (int)CommandLineParseReturnValues.OK)
            {
                erg.Add(localization_.Strings.GetString("success"));
            }

            return erg;
        }

        public int Execute()
        {
            var retval = CommandLineParser.parseCommandLine(args_);            
            if (retval.Item1 == (int)CommandLineParseReturnValues.OK)
            {
                var usecase = useCaseFactory_.GetUseCase(this);
                usecase.GetConnectionList();
                connections_.Add(retval.Item2);
                usecase.SaveServerConnections();
            }
            var consoleinfo = getResponseText((CommandLineParseReturnValues)retval.Item1);
            foreach (var output in consoleinfo)
            {
                consoleWriter_.WriteLine(output);
            }
            return retval.Item1;
        }

        public List<ServerConnection> GetUpdatedServerConnections()
        {
            return connections_;
        }

        public void ShowServerConnections(List<ServerConnection> connections)
        {
            this.connections_ = connections;
        }

        public void ShowServiceConnectionReachableResult(bool serverConnectionReachable)
        {
            if (serverConnectionReachable)
            {
                Console.WriteLine(localization_.Strings.GetString("The connection could be established"));
            }
            else
            {
                Console.WriteLine(localization_.Strings.GetString("The connection could not be established"));
            }
        }
    }
}
