using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UdpLibrary
{
    // https://stackoverflow.com/a/19787486
    
    // make it easier to parse responses
    public struct Received
    {
        public IPEndPoint Sender;
        public string Message;
    }

    public abstract class UdpBase
    {
        protected UdpClient Client;

        protected UdpBase()
        {
            Client = new UdpClient();
        }

        // client and server receive the same way
        public async Task<Received> ReceiveAsync()
        {
            var result = await Client.ReceiveAsync();
            return new Received()
            {
                Message = Encoding.ASCII.GetString(result.Buffer, 0, result.Buffer.Length),
                Sender = result.RemoteEndPoint
            };
        }

        // SendAsync to specific user
        public async Task SendAsync(string message, IPEndPoint endpoint)
        {
            var datagram = Encoding.ASCII.GetBytes(message);
            await Client.SendAsync(datagram, datagram.Length, endpoint);
        }

        // SendAsync to connected listener
        public async Task SendAsync(string message)
        {
            var datagram = Encoding.ASCII.GetBytes(message);
            await Client.SendAsync(datagram, datagram.Length);
        }
    }
}
