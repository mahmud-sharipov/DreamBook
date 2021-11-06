using DreamBook.Domain.Interfaces;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace DreamBook.Application.Abstraction.PagedList
{
    public class PagedListRequestModel<TEntity> : IPagedListRequestModel<TEntity> where TEntity : class, IEntity
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        //public string[] OrderBy { get; set; }
        public string SearchText { get; set; }

        public IQueryable<TEntity> Filter(IQueryable<TEntity> source, string searchFiled, string defaultPropertyToOrderBy)
        {
            if (!string.IsNullOrEmpty(SearchText))
                source = source.Where($"{searchFiled}.StartsWith(@0)", SearchText);

            //if (OrderBy != null && OrderBy.Any())
            //{
            //    var orderedList = source.OrderBy(OrderBy.First());
            //    foreach (var property in OrderBy.Skip(1))
            //        orderedList = orderedList.ThenBy(property);

            //    source = orderedList;
            //}
            else if (string.IsNullOrEmpty(defaultPropertyToOrderBy))
                source = source.OrderBy(defaultPropertyToOrderBy);

            return source;
        }
    }
}
