using System;

namespace Domain.Exceptions
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException() { }

        public ElementNotFoundException(string message)
            : base(message) { }

        public ElementNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}