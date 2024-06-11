using Calculator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.WPF.Commands
{
    public class ClearCommand : AsyncCommandBase
    {
        private readonly CalculatorViewModel _calculatorViewModel;

        public ClearCommand(CalculatorViewModel calculatorViewModel)
        {
            _calculatorViewModel = calculatorViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // reset values and alert the server
            _calculatorViewModel.CurrentOutput = "";
            ModifyInputCommand.NumberOpenParen = 0;
            await _calculatorViewModel.CalculatorModel.Clear();
        }
    }
}
