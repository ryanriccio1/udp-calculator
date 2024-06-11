namespace UDPCalculatorClient.Models
{
    // main calculator model
    public interface ICalculator
    {
        public string Operands { get; set; }

        public Task<string> Calculate();
        public Task Clear();

    }
}