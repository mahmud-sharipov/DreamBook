using System.Collections.Generic;

namespace DreamBook.Application.Abstraction.PagedList
{
    public interface IPagedList<T>
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        IList<T> Items { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}
