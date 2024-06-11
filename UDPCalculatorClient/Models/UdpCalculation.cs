using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UdpLibrary;

namespace UDPCalculatorClient.Models
{
    // provide Udp client wrapper functions for Calculator
    public class UdpCalculation
    {
        private readonly UdpUser _udpUser;

        public UdpCalculation()
        {
            _udpUser = UdpUser.ConnectTo("127.0.0.1",50025);
        }

        public async Task<string> GetResult(string operands)
        {
            await _udpUser.SendAsync(operands);
            var reply = await _udpUser.ReceiveAsync();

            return reply.Message;
        }

        public async Task Clear()
        {
            await _udpUser.SendAsync("CLEAR");
        }
    }
}
