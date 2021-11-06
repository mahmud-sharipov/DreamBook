using System;

namespace DreamBook.Application.Exceptions
{
    public class ValidationException : BusinessLogicException
    {
        public ValidationException() { }

        public ValidationException(string message) : base(message) { }

        public ValidationException(string message, Exception inner) : base(message, inner) { }
    }
}
