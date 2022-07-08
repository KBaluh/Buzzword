using System;

namespace Buzzword.Common.Exceptions
{
    public class BuzzwordException : Exception
    {
        public BuzzwordException()
        { }

        public BuzzwordException(string message)
            : base(message)
        { }

        public BuzzwordException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
