namespace DreamBook.Application.Exceptions
{
    public class EntityNotFoundException : Exception, IValidaionException
    {
        public EntityNotFoundException(string entityLabel, Guid entityId) : this(entityLabel, entityId.ToString()) { }

        public EntityNotFoundException(string entityLabel, string entityId) : base(ExceptionMessages.EntityNotFound.Format(entityLabel, entityId)) { }
    }
}
