using System;
using System.Windows.Input;

namespace JWOrbitalSimulatorPortable.Commands
{
    /// <summary>
    /// Impliments the ICommand Interface used to relay an event 
    /// to a delegate with a single paramater to execute
    /// </summary>
    public class ParamRelayCommand<T> : ICommand
    {
        // An Action type delegate with 1 param and no return.
        // To be executed on the command's execute
        protected Action<T> _Action;

        /// <summary>
        /// Construct instance with a Action Delegate to execute
        /// </summary>
        public ParamRelayCommand(Action<T> action)
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
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Execute The Action Delegate given to this commadn
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _Action((T)parameter);
        }
    }
}