using System.Collections.Generic;

namespace DreamBook.Application.Abstraction
{
    public interface ITranslatableEntityService<TEntity, TTranslaionEntity, TResponse, TResponseWithTranslation>
         where TEntity : class, ITranslatable<TTranslaionEntity>
         where TTranslaionEntity : class, ITranslation
         where TResponse : class, IResponseModel
         where TResponseWithTranslation : class, IResponseModel
    {
        Task<IEnumerable<TResponse>> GetAll();
        Task<IPagedList<TResponse>> GetPagedList(IPagedListRequestModel<TTranslaionEntity> requestModel);
        Task<TResponse> GetById(Guid id);
        Task<IEnumerable<TResponseWithTranslation>> GetAllWithTranslations();
        Task<TResponseWithTranslation> GetByIdWithTranslations(Guid id);
        Task Delete(Guid id);
        Task Delete(params Guid[] ids);
    }
}
