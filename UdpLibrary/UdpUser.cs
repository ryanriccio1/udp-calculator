using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpLibrary
{
    public class UdpUser : UdpBase
    {
        private UdpUser() { }

        // create connection to Listener
        public static UdpUser ConnectTo(string hostname, int port)
        {
            var connection = new UdpUser();
            connection.Client.Connect(hostname, port);
            return connection;
        }
    }
}
