using System;

namespace DreamBook.Application.Exceptions
{
    public class EntityNotFoundException : BusinessLogicException
    {
        public EntityNotFoundException() { }

        public EntityNotFoundException(Guid entityId) : base(entityId.ToString()) { }

        public EntityNotFoundException(string entityId) : base(entityId) { }
    }
}
