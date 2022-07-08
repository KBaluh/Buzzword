using System.Threading.Tasks;
using System.Windows.Input;

namespace Buzzword.Common.Extensions
{
    public static class CommandExtensions
    {
        public static void TryExecute(this ICommand command, object parameter = null)
        {
            if (command != null)
            {
                if (command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
            }
        }

        public static void TryExecute(this IAsyncCommand command)
        {
            if (command != null)
            {
                if (command.CanExecute())
                {
                    _ = command.ExecuteAsync();
                }
            }
        }

        public static async Task TryExecuteAsync(this IAsyncCommand command)
        {
            if (command != null)
            {
                if (command.CanExecute())
                {
                    await command.ExecuteAsync();
                }
            }
        }

        public static void TryExecute<T>(this IAsyncCommand<T> command, T parameter)
        {
            if (command != null)
            {
                if (command.CanExecute(parameter))
                {
                    _ = command.ExecuteAsync(parameter);
                }
            }
        }

        public static async Task TryExecuteAsync<T>(this IAsyncCommand<T> command, T parameter)
        {
            if (command != null)
            {
                if (command.CanExecute(parameter))
                {
                    await command.ExecuteAsync(parameter);
                }
            }
        }
    }
}
