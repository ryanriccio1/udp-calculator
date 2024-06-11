using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.WPF.ViewModels;

namespace Calculator.WPF.Commands
{
    public class CalculateCommand : AsyncCommandBase
    {
        private readonly CalculatorViewModel _calculatorViewModel;

        public CalculateCommand(CalculatorViewModel calculatorViewModel)
        {
            _calculatorViewModel = calculatorViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // close off any open parens if needed
            if (ModifyInputCommand.NumberOpenParen > 0)
            {
                _calculatorViewModel.CurrentOutput += new string(')', ModifyInputCommand.NumberOpenParen);
            }

            // set the operands
            _calculatorViewModel.CalculatorModel.Operands = _calculatorViewModel.CurrentOutput;

            // replace special characters
            _calculatorViewModel.CalculatorModel.Operands = _calculatorViewModel.CalculatorModel.Operands.Replace("÷", "/").Replace("×", "*");
            _calculatorViewModel.CurrentOutput = await _calculatorViewModel.CalculatorModel.Calculate();

            // handle div by 0
            if (_calculatorViewModel.CurrentOutput == "?")
            {
                _calculatorViewModel.CurrentOutput = "Error";
            }
            try
            {
                // try to set the output to whatever response we get
                _calculatorViewModel.CurrentOutput =
                    Convert.ToString(Math.Round(Convert.ToDouble(_calculatorViewModel.CurrentOutput), 5));
            }
            catch
            {
            }
            ModifyInputCommand.NumberOpenParen = 0;


        }
    }
}
