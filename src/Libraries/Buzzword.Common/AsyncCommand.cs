using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Buzzword.Common
{
    public class AsyncCommand : IAsyncCommand
    {
        public event EventHandler CanExecuteChanged;

        private Guid _id;
        private bool _isExecuting;
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;
        private readonly IErrorHandler _errorHandler;

        /// <summary>
        /// <b>ВНИМАНИЕ</b> - без использования <see cref="IErrorHandler"/> будет потеря <see cref="Exception"/>, поэтом на каждый вызов команды <b>необходимо использовать блок try catch</b>
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="errorHandler"></param>
        public AsyncCommand(Func<Task> execute, IErrorHandler errorHandler)
            : this(execute, null, errorHandler)
        {
        }

        /// <summary>
        /// <b>ВНИМАНИЕ</b> - без использования <see cref="IErrorHandler"/> будет потеря <see cref="Exception"/>, поэтом на каждый вызов команды <b>необходимо использовать блок try catch</b>
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        /// <param name="errorHandler"></param>
        public AsyncCommand(Func<Task> execute, Func<bool> canExecute, IErrorHandler errorHandler)
        {
            _id = Guid.NewGuid();
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }

        public bool CanExecute()
        {
            //System.Diagnostics.Trace.WriteLine($"{GetType().Name}::{nameof(CanExecute)}::{_id} {nameof(_isExecuting)}: {_isExecuting}");
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    _isExecuting = true;
                    //System.Diagnostics.Trace.WriteLine($"{GetType().Name}::{nameof(ExecuteAsync)}::{_id} {nameof(_isExecuting)}: {_isExecuting}");
                    await _execute();
                }
                finally
                {
                    _isExecuting = false;
                    //System.Diagnostics.Trace.WriteLine($"{GetType().Name}::{nameof(ExecuteAsync)}::{_id} {nameof(_isExecuting)}: {_isExecuting}");
                }
            }

            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit implementations
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            if (_errorHandler == null)
            {
                ExecuteAsync().FireAndForgetAsync();
            }
            else
            {
                ExecuteAsync().FireAndForgetSafeAsync(_errorHandler);
            }
        }
        #endregion
    }

    public class AsyncCommand<T> : IAsyncCommand<T>
    {
        public event EventHandler CanExecuteChanged;

        private Guid _id;
        private bool _isExecuting;
        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool> _canExecute;
        private readonly IErrorHandler _errorHandler;

        /// <summary>
        /// <b>ВНИМАНИЕ</b> - без использования <see cref="IErrorHandler"/> будет потеря <see cref="Exception"/>, поэтом на каждый вызов команды <b>необходимо использовать блок try catch</b>
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="errorHandler"></param>
        public AsyncCommand(Func<T, Task> execute, IErrorHandler errorHandler)
            : this(execute, null, errorHandler)
        {
        }

        /// <summary>
        /// <b>ВНИМАНИЕ</b> - без использования <see cref="IErrorHandler"/> будет потеря <see cref="Exception"/>, поэтом на каждый вызов команды <b>необходимо использовать блок try catch</b>
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        /// <param name="errorHandler"></param>
        public AsyncCommand(Func<T, Task> execute, Func<T, bool> canExecute, IErrorHandler errorHandler)
        {
            _id = Guid.NewGuid();
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }

        public bool CanExecute(T parameter)
        {
            //System.Diagnostics.Trace.WriteLine($"{GetType().Name}::{nameof(CanExecute)}::{_id} {nameof(_isExecuting)}: {_isExecuting}");
            return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
        }

        public async Task ExecuteAsync(T parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    _isExecuting = true;
                    //System.Diagnostics.Trace.WriteLine($"{GetType().Name}::{nameof(ExecuteAsync)}::{_id} {nameof(_isExecuting)}: {_isExecuting}");
                    await _execute(parameter);
                }
                finally
                {
                    _isExecuting = false;
                    //System.Diagnostics.Trace.WriteLine($"{GetType().Name}::{nameof(ExecuteAsync)}::{_id} {nameof(_isExecuting)}: {_isExecuting}");
                }
            }

            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit implementations
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            if (_errorHandler == null)
            {
                ExecuteAsync((T)parameter).FireAndForgetAsync();
            }
            else
            {
                ExecuteAsync((T)parameter).FireAndForgetSafeAsync(_errorHandler);
            }
        }
        #endregion
    }
}
