using System;
using System.Diagnostics;
using System.Windows.Input;

namespace TemplateSelectorDemo.ViewModel
{   
    /// <summary>
    /// https://msdn.microsoft.com/en-us/magazine/dd419663.aspx#id0090030
    /// </summary>
    public class RelayCommand : ICommand
    {
        readonly Action<object> _Execute;
        readonly Predicate<object> _CanExecute;

        public RelayCommand(Action<object> execute) : this(execute, null) { }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _Execute = execute ?? throw new ArgumentNullException("execute");
            _CanExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _CanExecute == null ? true : _CanExecute(parameter);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public void Execute(object parameter) { _Execute(parameter); }
    }
}
