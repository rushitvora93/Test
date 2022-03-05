using System;
using System.Collections.Generic;
using System.Linq;
using Core.UseCases;

namespace ServerConnections.CommandLine
{
    public enum CommandLineParseReturnValues
    {
        OK = 0,
        INVALID_COMMAND = 1,
        INVALID_SERVERNAME = 2,
        INVALID_HOSTNAME = 4,
        INVALID_PORT = 8
    }

    public static class CommandLineParser
    {
        const string keyservername = "ServerName=";
        const string keyhostname = "HostName=";
        const string keyport = "Port=";
        const string keyprincipalname = "PrincipalName=";
        const string addcommand = "add";

        static public (int,ServerConnection) parseCommandLine(string[] args)
        {            
            var servernamelist = args.Where(x => x.ToUpper().StartsWith(keyservername.ToUpper())).ToList();
            var hostnamelist = args.Where(x => x.ToUpper().StartsWith(keyhostname.ToUpper())).ToList();
            var portlist = args.Where(x => x.ToUpper().StartsWith(keyport.ToUpper())).ToList();
            var principalnamelist = args.Where(x => x.ToUpper().StartsWith(keyprincipalname.ToUpper())).ToList();                       

            Func<string, List<string>, string> parseobject = (key, input) => (input.Count > 0?input[0].Substring(key.Length , input[0].Length - key.Length):"");
            string servername = parseobject(keyservername,servernamelist);
            string hostname = parseobject(keyhostname, hostnamelist);
            string portstring = parseobject(keyport, portlist);
            string principalname = parseobject(keyprincipalname, principalnamelist);
            ushort port = 0;

            int errorcode = 0;

            if (args.Length > 0)
            {
                if (args[0].ToUpper() != addcommand.ToUpper())
                {
                    errorcode = (int)CommandLineParseReturnValues.INVALID_COMMAND;
                }
            }
            else
            {
                errorcode = (int)CommandLineParseReturnValues.INVALID_COMMAND;
            }

            if(servername.Length < 1)
            {
                errorcode |= (int)CommandLineParseReturnValues.INVALID_SERVERNAME;
            }
            if (hostname.Length < 1)
            {
                errorcode |= (int)CommandLineParseReturnValues.INVALID_HOSTNAME; 
            }
            if (!ushort.TryParse(portstring, out port))
            {
                errorcode |= (int)CommandLineParseReturnValues.INVALID_PORT; 
            }            
            if (errorcode == (int)CommandLineParseReturnValues.OK)
            {
                var connection = new ServerConnection() { ServerName = servername, HostName = hostname, Port = port };
                connection.PrincipalName = principalname.Length > 0 ? principalname : null;
                return (errorcode, connection);
            }       
            return (errorcode,null);
        }
    }
}
