using System;

namespace Domain.Exceptions
{
    public class ValidateDomainException : Exception
    {
        public ValidateDomainException() { }

        public ValidateDomainException(string message)
            : base(message) { }

        public ValidateDomainException(string message, Exception inner)
            : base(message, inner) { }
    }
}