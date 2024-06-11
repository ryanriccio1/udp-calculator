using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPCalculatorClient.Models
{
    // implement calculator with a Udp handler for operands
    public class Calculator : ICalculator
    {
        public string Operands { get; set; }
        private readonly UdpCalculation UdpCalculation;
        
        public Calculator()
        {
            Operands = "";
            UdpCalculation = new UdpCalculation();
        }
        public async Task<string> Calculate()
        {
            return await UdpCalculation.GetResult(Operands);
        }

        public async Task Clear()
        {
            await UdpCalculation.Clear();
        }

    }
}
