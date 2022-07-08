using System;

namespace Buzzword.Common.Exceptions
{
    public class BuzzwordNetworkException : BuzzwordException
    {
        public BuzzwordNetworkException()
        { }

        public BuzzwordNetworkException(string message)
            : base(message)
        { }

        public BuzzwordNetworkException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
