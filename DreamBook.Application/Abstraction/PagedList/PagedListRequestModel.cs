using DreamBook.Domain.Interfaces;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace DreamBook.Application.Abstraction.PagedList
{
    public class SimplePagedListRequestModel<TEntity> : IPagedListRequestModel<TEntity> where TEntity : class, IEntity
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public virtual IQueryable<TEntity> Filter(IQueryable<TEntity> source, string searchFiled, string defaultPropertyToOrderBy)
        {
            return source;
        }
    }

    public class PagedListRequestModel<TEntity> : SimplePagedListRequestModel<TEntity> where TEntity : class, IEntity
    {
        public string SearchText { get; set; }

        public override IQueryable<TEntity> Filter(IQueryable<TEntity> source, string searchFiled, string defaultPropertyToOrderBy)
        {
            var result = base.Filter(source, searchFiled, defaultPropertyToOrderBy);
            if (!string.IsNullOrEmpty(SearchText))
                result = result.Where($"{searchFiled}.StartsWith(@0)", SearchText);

           if (string.IsNullOrEmpty(defaultPropertyToOrderBy))
                result = result.OrderBy(defaultPropertyToOrderBy);

            return result;
        }
    }
}
