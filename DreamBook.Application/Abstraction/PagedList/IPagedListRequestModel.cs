using DreamBook.Application.Abstraction.Request;
using DreamBook.Domain.Interfaces;
using System.Linq;

namespace DreamBook.Application.Abstraction.PagedList
{
    public interface IPagedListRequestModel<TEntity> : IRequestModel where TEntity : class, IEntity
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        //string[] OrderBy { get; set; }
        string SearchText { get; set; }

        IQueryable<TEntity> Filter(IQueryable<TEntity> source, string searchFiled, string orderBy);
    }
}
