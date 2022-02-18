using System.Collections.Generic;

namespace DreamBook.Application.Abstraction
{
    public interface IEntityService<TEntity, TResponse>
         where TEntity : class, IEntity
         where TResponse : class, IResponseModel
    {
        Task<IEnumerable<TResponse>> GetAll();
        Task<IPagedList<TResponse>> GetPagedList(IPagedListRequestModel<TEntity> requestModel);
        Task<TResponse> GetById(Guid id);
        Task Delete(Guid id);
        Task Delete(params Guid[] ids);
    }
}
