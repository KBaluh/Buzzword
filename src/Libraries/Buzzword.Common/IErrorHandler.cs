using System;
using System.Runtime.CompilerServices;

namespace Buzzword.Common
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex, [CallerMemberName] string callerMemberName = null);
    }
}
