using System;

namespace Buzzword.Common.Exceptions
{
    public class BuzzwordAccessException : BuzzwordException
    {
        public BuzzwordAccessException()
        { }

        public BuzzwordAccessException(string message)
            : base(message)
        { }

        public BuzzwordAccessException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
