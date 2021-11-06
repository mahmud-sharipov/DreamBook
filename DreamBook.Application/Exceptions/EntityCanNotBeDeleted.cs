using DreamBook.Application.LanguageResources;
using DreamBook.Domain.Interfaces;
using System;

namespace DreamBook.Application.Exceptions
{
    public class EntityCanNotBeDeleted : BusinessLogicException
    {
        public EntityCanNotBeDeleted(IEntity entity, string reason) : this(entity.GetType().Name, entity?.Guid.ToString() ?? "", reason) { }

        public EntityCanNotBeDeleted(string entityName, Guid entityId, string reason) : this(entityName, entityId.ToString(), reason) { }

        public EntityCanNotBeDeleted(string entityName, string entityId, string reason) : base(Messages.EntityCanNotBeDeleted.Format(entityName, entityId, reason)) { }
    }
}
