using System;
using System.Windows;

namespace ServerConnections.Model
{
    class ServerConnectionModel : DependencyObject
    {
        // ServerName
        public static readonly DependencyProperty ServerNameProperty =
            DependencyProperty.Register("ServerName",  typeof(string), typeof(ServerConnectionModel), new PropertyMetadata(new PropertyChangedCallback(ServerConnectionChangedCallback)));
        public string ServerName
        {
            get { return (string)GetValue(ServerNameProperty); }
            set { SetValue(ServerNameProperty, value); }
        }

        // HostName
        private static readonly DependencyProperty HostNameProperty =
            DependencyProperty.Register("HostName", typeof(string), typeof(ServerConnectionModel), new PropertyMetadata(new PropertyChangedCallback(ServerConnectionChangedCallback)));
        public string HostName
        {
            get { return (string)GetValue(HostNameProperty); }
            set { SetValue(HostNameProperty, value); }
        }

        // Port
        private static readonly DependencyProperty PortProperty =
            DependencyProperty.Register("Port", typeof(UInt16), typeof(ServerConnectionModel), new PropertyMetadata(new PropertyChangedCallback(ServerConnectionChangedCallback)));
        public UInt16 Port
        {
            get { return (UInt16)GetValue(PortProperty); }
            set { SetValue(PortProperty, value); }
        }

        // PrincipalName
        private static readonly DependencyProperty PrincipalNameProperty =
            DependencyProperty.Register("PrincipalName", typeof(string), typeof(ServerConnectionModel), new PropertyMetadata(new PropertyChangedCallback(ServerConnectionChangedCallback)));
        public string PrincipalName
        {
            get { return (string)GetValue(PrincipalNameProperty); }
            set { SetValue(PrincipalNameProperty, value); }
        }



        #region Events
        public static event EventHandler ServerConnectionChanged;

        // Callback to invoke the Event
        private static void ServerConnectionChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ServerConnectionModel.ServerConnectionChanged?.Invoke(d, null);
        }
        #endregion


        #region Mapping
        public static ServerConnectionModel FromEntity(Core.UseCases.ServerConnection entity)
        {
            return new ServerConnectionModel()
            {
                ServerName = entity.ServerName,
                HostName = entity.HostName,
                Port = entity.Port,
                PrincipalName = entity.PrincipalName
            };
        }

        public void MapFromEntity(Core.UseCases.ServerConnection entity)
        {
            ServerName = entity.ServerName;
            HostName = entity.HostName;
            Port = entity.Port;
            PrincipalName = entity.PrincipalName;
        }

        public Core.UseCases.ServerConnection MapToEntity()
        {
            return new Core.UseCases.ServerConnection()
            {
                ServerName = this.ServerName,
                HostName = this.HostName,
                Port = this.Port,
                PrincipalName = this.PrincipalName
            };
        }
        #endregion
    }
}
