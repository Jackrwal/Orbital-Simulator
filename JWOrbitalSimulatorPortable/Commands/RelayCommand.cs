using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JWOrbitalSimulatorPortable.Commands
{
    public class RelayCommand : ICommand
    {
        protected Action _Action;

        /// <summary>
        /// Construct instance with a Action Delegate to execute
        /// </summary>
        /// <param name="action"></param>
        public RelayCommand(Action action)
        {
            _Action = action;
        }

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// RelayCommands can always be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Execute The Action Delegate given to this commadn
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _Action();
        }
    }
}