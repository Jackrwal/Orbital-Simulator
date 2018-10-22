using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

// ##  This Class should be able to hold a action capable of taking an Application Page at a parameter

namespace OrbitalSimulator_Objects
{
    public class RelayCommand : ICommand
    {
        Action _Action;

        public RelayCommand(Action action)
        {
            _Action = action;
        }

        // Do Nothing on can execute changed as 
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        // Allways allow Command to be executed
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _Action();
        }
    }
}
