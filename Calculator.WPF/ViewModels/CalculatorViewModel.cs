using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Calculator.WPF.Commands;
using UDPCalculatorClient.Models;

namespace Calculator.WPF.ViewModels
{
    public class CalculatorViewModel : ViewModelBase
    {
        // Prop change for output
        private string _currentOutput;
        public string CurrentOutput
        {
            get
            {
                return _currentOutput;
            }
            set
            {
                _currentOutput = value;
                OnPropertyChanged(nameof(CurrentOutput));
            }
        }

        public ICalculator CalculatorModel;

        // Commands for bindings
        public ICommand CalculateCommand { get; set; }
        public ICommand ModifyInputCommand { get; set; }
        public ICommand ClearCommand { get; set; }


        public CalculatorViewModel()
        {
            CurrentOutput = "";

            CalculateCommand = new CalculateCommand(this);
            ModifyInputCommand = new ModifyInputCommand(this);
            ClearCommand = new ClearCommand(this);
            CalculatorModel = new UDPCalculatorClient.Models.Calculator();
        }
    }
}
