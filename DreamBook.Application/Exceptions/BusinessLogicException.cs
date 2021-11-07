using System;

namespace DreamBook.Application.Exceptions
{
    public class BusinessLogicException : Exception, IValidaionException
    {
        public BusinessLogicException(string message) : base(message) { }

        public BusinessLogicException(string message, Exception inner) : base(message, inner) { }
    }
}