using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UdpLibrary
{
    public class UdpListener : UdpBase
    {
        private IPEndPoint _listenOn;

        // default constructor (default port = 50025)
        public UdpListener() : this(new IPEndPoint(IPAddress.Any, 50025))
        {
        }

        // given specific endpoint
        public UdpListener(IPEndPoint endpoint)
        {
            _listenOn = endpoint;
            Client = new UdpClient(_listenOn);
        }

        // wrapper for SendAsync, but semantically correct for server side
        public async Task ReplyAsync(string message, IPEndPoint endpoint)
        {
            await SendAsync(message, endpoint);
        }

    }
}
