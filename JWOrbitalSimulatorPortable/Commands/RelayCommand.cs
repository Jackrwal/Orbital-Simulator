using System;
using System.Windows.Input;

namespace JWOrbitalSimulatorPortable.Commands
{
    /// <summary>
    /// Impliments the ICommand Interface used to relay an event 
    /// to a delegate to execute
    /// </summary>
    public class RelayCommand : ICommand
    {
        // An Action type delegate with no params and no return.
        // To be executed on the command's execute
        protected Action _Action;

        /// <summary>
        /// Construct instance with a Action Delegate to execute
        /// </summary>
        /// <param name="action"></param>
        public RelayCommand(Action action)
        {
            _Action = action;
        }

        /// <summary>
        /// Rasied when can execute changed. Which it will not in this implimentation.
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// RelayCommands can always be executed
        /// </summary>
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