using AutoMapper;
using DreamBook.Application.Abstraction.PagedList;
using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.Abstraction.Response;
using DreamBook.Application.Exceptions;
using DreamBook.Application.LanguageResources;
using DreamBook.Domain.Entities;
using DreamBook.Domain.Interfaces;
using DreamBook.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DreamBook.Application.Abstraction.Service
{
    public abstract class TranslatableEntityService<TEntity, TTranslaionEntity, TResponse, TResponseWithTranslation> :
        ITranslatableEntityService<TEntity, TTranslaionEntity, TResponse, TResponseWithTranslation>
             where TEntity : class, ITranslatable<TTranslaionEntity>
             where TTranslaionEntity : class, ITranslation
             where TResponse : class, IResponseModel
             where TResponseWithTranslation : class, IResponseModel
    {
        public AppLanguageManager AppLanguageManager { get; }
        protected Expression<Func<TTranslaionEntity, bool>> LanguagePredicate { get; }
        protected IContext Context { get; }
        protected IMapper Mapper { get; }

        public TranslatableEntityService(IContext context, IMapper mapper, AppLanguageManager appLanguageManager)
        {
            Context = context;
            Mapper = mapper;
            AppLanguageManager = appLanguageManager;
            LanguagePredicate = dt => dt.LanguageGuid == AppLanguageManager.CurrentLanguage.Guid;
        }

        public virtual async Task<IEnumerable<TResponse>> GetAll()
        {
            var entities = await Context.GetAllAsync(LanguagePredicate);
            return Mapper.Map<IEnumerable<TResponse>>(entities);
        }

        public virtual async Task<TResponse> GetById(Guid id)
        {
            var entity = (await Context.GetByIdAsync<TEntity>(id))?.Translations.SingleOrDefault(LanguagePredicate.Compile());
            if (entity == null)
                throw new EntityNotFoundException(id);

            return Mapper.Map<TResponse>(entity);
        }

        public virtual async Task<IPagedList<TResponse>> GetPagedList(IPagedListRequestModel<TTranslaionEntity> requestModel)
        {
            var entities = requestModel.Filter(Context.GetAll(LanguagePredicate), GetDefaultSearchPropertyName(), GetDefaultPropertyNameToOrderBy());
            var result = new PagedList<TTranslaionEntity, TResponse>(entities, l => Mapper.Map<IEnumerable<TResponse>>(l), requestModel.PageNumber, requestModel.PageSize);
            return await Task.FromResult(result);
        }

        public virtual async Task<IEnumerable<TResponseWithTranslation>> GetAllWithTranslations()
        {
            var entities = await Context.GetAllAsync<TEntity>();
            return Mapper.Map<IEnumerable<TResponseWithTranslation>>(entities);
        }

        public virtual async Task<TResponseWithTranslation> GetByIdWithTranslations(Guid id)
        {
            var entity = await Context.GetByIdAsync<TEntity>(id);
            if (entity == null)
                throw new EntityNotFoundException(id);

            return Mapper.Map<TResponseWithTranslation>(entity);
        }

        protected async Task<TResponseWithTranslation> Create<TTranslationRequest>(ITranslatableRequestModel<TTranslationRequest> requestModel)
            where TTranslationRequest : ITranslationRequestModel
        {
            var entity = Mapper.Map<TEntity>(requestModel);
            foreach (var translationRequest in requestModel.Translations)
                entity.Translations.Add(Mapper.Map<TTranslaionEntity>(translationRequest));

            await Context.AddAsync(entity);
            var result = await Context.SaveChangesAsync();
            if (result == 0)
                throw new Exception(ExceptionMessages.EntityWasNotSavedToDB.Format(typeof(TEntity).Name));

            return Mapper.Map<TEntity, TResponseWithTranslation>(entity);
        }

        protected async Task Update<TTranslationRequest>(ITranslatableRequestModel<TTranslationRequest> requestModel, Guid id)
            where TTranslationRequest : ITranslationRequestModel
        {
            var entity = await Context.GetByIdAsync<TEntity>(id);
            if (entity == null)
                throw new EntityNotFoundException(id);

            Mapper.Map(requestModel, entity);
            foreach (var translationRequest in requestModel.Translations)
            {
                var translationEntity = entity.Translations.SingleOrDefault(e => e.LanguageGuid == translationRequest.LanguageGuid);
                if (translationEntity == null)
                {
                    throw new EntityNotFoundException(translationRequest.LanguageGuid);
                }
                Mapper.Map(translationRequest, translationEntity);
            }

            var result = await Context.SaveChangesAsync();
            if (result == 0)
                throw new Exception(ExceptionMessages.EntityWasNotSavedToDB.Format(typeof(TEntity).Name));
        }

        public virtual async Task Delete(Guid id)
        {
            var entity = await Context.GetByIdAsync<TEntity>(id);

            if (entity == null)
                throw new EntityNotFoundException(id);

            var canDelete = CanEntityBeDeleted(entity);
            if (!canDelete.CanBeDeleted)
                throw new EntityCanNotBeDeleted(entity, canDelete.Reason);

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
    }
}
