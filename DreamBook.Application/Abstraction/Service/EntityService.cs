using System.Collections.Generic;

namespace DreamBook.Application.Abstraction.Service
{
    public abstract class EntityService<TEntity, TResponse> : IEntityService<TEntity, TResponse>
        where TEntity : class, IEntity
        where TResponse : class, IResponseModel
    {
        protected IContext Context { get; }
        protected IMapper Mapper { get; }

        public EntityService(IContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public virtual async Task<IEnumerable<TResponse>> GetAll()
        {
            var entities = await Context.GetAllAsync<TEntity>();
            return Mapper.Map<IEnumerable<TResponse>>(entities);
        }

        public virtual async Task<IPagedList<TResponse>> GetPagedList(IPagedListRequestModel<TEntity> requestModel)
        {
            var entities = requestModel.Filter(Context.GetAll<TEntity>(), GetDefaultSearchPropertyName(), GetDefaultPropertyNameToOrderBy());
            var result = new PagedList<TEntity, TResponse>(entities, l => Mapper.Map<IEnumerable<TResponse>>(l), requestModel.PageNumber, requestModel.PageSize);
            return await Task.FromResult(result);
        }

        public virtual async Task<TResponse> GetById(Guid id)
        {
            var entity = await Context.GetByIdAsync<TEntity>(id);
            if (entity == null)
                throw new EntityNotFoundException(GetEntityLabel(), id);

            return Mapper.Map<TResponse>(entity);
        }

        protected virtual async Task<TResponse> Create<TRequest>(TRequest requestModel)
            where TRequest : IRequestModel
        {
            var entity = Mapper.Map<TEntity>(requestModel);
            await Context.AddAsync(entity);
            var result = await Context.SaveChangesAsync();
            if (result == 0)
                throw new Exception(ExceptionMessages.EntityWasNotSavedToDB.Format(typeof(TEntity).Name));

            return Mapper.Map<TEntity, TResponse>(entity);
        }

        protected virtual async Task Update<TRequest>(TRequest requestModel, Guid id) where TRequest : IRequestModel
        {
            var entity = await Context.GetByIdAsync<TEntity>(id);
            if (entity == null)
                throw new EntityNotFoundException(GetEntityLabel(), id);

            Mapper.Map(requestModel, entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task Delete(Guid id)
        {
            var entity = await Context.GetByIdAsync<TEntity>(id);

            if (entity == null)
                throw new EntityNotFoundException(GetEntityLabel(), id);

            var canDelete = CanEntityBeDeleted(entity);
            if (!canDelete.CanBeDeleted)
                throw new EntityCanNotBeDeletedExxeption(GetEntityLabel(), entity.Guid, canDelete.Reason);

            Context.Delete(entity);
            await Context.SaveChangesAsync();
        }

        public virtual Task Delete(params Guid[] ids)
        {
            throw new NotImplementedException();
        }

        protected virtual (bool CanBeDeleted, string Reason) CanEntityBeDeleted(TEntity entity) => (true, "");

        protected virtual string GetDefaultSearchPropertyName() => nameof(IEntity.Guid);

        protected virtual string GetDefaultPropertyNameToOrderBy() => nameof(IEntity.Guid);

        protected virtual string GetEntityLabel() => typeof(TEntity).Name;
    }
}
