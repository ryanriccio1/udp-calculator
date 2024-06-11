using Calculator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.WPF.Commands
{
    public class ModifyInputCommand : AsyncCommandBase
    {
        private readonly CalculatorViewModel _calculatorViewModel;
        public static int NumberOpenParen { get; set; } // pseudo "Stack" type number to make sure all open parens are closed

        public ModifyInputCommand(CalculatorViewModel calculatorViewModel)
        {
            NumberOpenParen = 0;
            _calculatorViewModel = calculatorViewModel;
        }

        public override Task ExecuteAsync(object parameter)
        {
            string input = parameter.ToString();

            // any input after error will be only input and validated below
            if (_calculatorViewModel.CurrentOutput == "Error")
            {
                _calculatorViewModel.CurrentOutput = "";
            }

            // do not allow non-digits on first character
            if (_calculatorViewModel.CurrentOutput == "")
            {
                // only non-digit allowed is open paren
                if (input.All(char.IsDigit) || input == "(")
                {
                    if (input == "(") NumberOpenParen++;
                    _calculatorViewModel.CurrentOutput += input;
                }
            }
            else
            {
                // backspace handler
                if (input == "⌫")
                {
                    if (_calculatorViewModel.CurrentOutput.Last() == '(')
                    {
                        NumberOpenParen--;
                    }
                    else if (_calculatorViewModel.CurrentOutput.Last() == ')')
                    {
                        NumberOpenParen++;
                    }
                    _calculatorViewModel.CurrentOutput =
                        _calculatorViewModel.CurrentOutput.Remove(_calculatorViewModel.CurrentOutput.Length - 1, 1);
                    return Task.CompletedTask;
                }

                
                // if we have a decimal, and our next input is not a digit, add a 0
                if (_calculatorViewModel.CurrentOutput.Last() == '.' && !input.All(char.IsDigit))
                {
                    _calculatorViewModel.CurrentOutput += "0";
                }
                // make sure close paren only goes after a digit, if its not, return and do nothing
                if (!Char.IsDigit(_calculatorViewModel.CurrentOutput.Last()) && input == ")" &&
                    _calculatorViewModel.CurrentOutput.Last() != ')')
                {
                    return Task.CompletedTask;
                }
                // if we are starting a new paren group without an operator beforehand, assume multiply
                if (Char.IsDigit(_calculatorViewModel.CurrentOutput.Last()) && input == "(" )
                {
                    _calculatorViewModel.CurrentOutput += "×";
                }
                // if we have open and close paren next to each other with no operator, assume multiply
                if (_calculatorViewModel.CurrentOutput.Last() == ')' && input == "(")
                {
                    _calculatorViewModel.CurrentOutput += "×";
                }
                // if we have a digit, add it, if our last character was a digit, we can add any symbol
                // if our last character was a close paren, we can add any symbol, we can add parens whenever
                // (but we are constrained by NumberOpenParen check above)
                if (input.All(char.IsDigit) || Char.IsDigit(_calculatorViewModel.CurrentOutput.Last()) ||
                    _calculatorViewModel.CurrentOutput.Last() == ')' ||
                    input == "(" || input == ")")
                {
                    // make sure all parens are closed and cannot have closed without open
                    if (input == "(") NumberOpenParen++;
                    else if (input == ")")
                    {
                        if (NumberOpenParen > 0)
                        {
                            NumberOpenParen--;
                        }
                        else
                        {
                            return Task.CompletedTask;
                        }
                    }
                    // if we are closing the paren in a proper place, and we are inputting a digit, assume multiply
                    if (_calculatorViewModel.CurrentOutput.Last() == ')' && input.All(char.IsDigit))
                        _calculatorViewModel.CurrentOutput += "×";
                    _calculatorViewModel.CurrentOutput += input;
                }
            }

            // allow async task, return "empty" task
            return Task.CompletedTask;

        }
    }
}
