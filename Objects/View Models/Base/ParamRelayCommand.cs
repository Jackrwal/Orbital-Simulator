using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrbitalSimulator_Objects
{
    class ParamRelayCommand : ICommand
    {
        Action<object> _Action;
        
        public ParamRelayCommand(Action<object> action)
        {
            _Action = action;
        }

        // Do Nothing on can execute changed as 
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        // Allways allow Command to be executed
        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _Action(parameter);
        }
    }
}
