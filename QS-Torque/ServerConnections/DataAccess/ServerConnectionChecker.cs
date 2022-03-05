using log4net;
using ServerConnections.UseCases;
using State;
using System;
using System.Collections.Generic;
using Core.UseCases;

namespace ServerConnections.DataAccess
{
    public class ServerConnectionChecker : IServerConnectionChecker
    {
        private ILog _log = LogManager.GetLogger(LoggerName.ServerConnectionConfigurationLogger);
        public bool ServerConnectionReachable(ServerConnection connection)
        {
            try
            {
                throw new NotImplementedException("GRPC needs to be implemented");
                return true;
            }
            catch(NotImplementedException e)
            {
                _log.Error("Not implemented!", e);
                return false;
            }
            catch (Exception e)
            {
                _log.Error("WCF Service not reachable",e);
                return false;
            }
        }
    }
}
