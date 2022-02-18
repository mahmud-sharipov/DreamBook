namespace DreamBook.Application.Exceptions
{
    public class EntityCanNotBeDeletedExxeption : Exception, IValidaionException
    {
        public EntityCanNotBeDeletedExxeption(string entityName, Guid entityId, string reason) : this(entityName, entityId.ToString(), reason) { }

        public EntityCanNotBeDeletedExxeption(string entityName, string entityId, string reason) : base(ExceptionMessages.EntityCanNotBeDeleted.Format(entityName, entityId, reason)) { }
    }

}