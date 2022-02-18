using System.Collections.Generic;

namespace DreamBook.Application.Abstraction.PagedList
{
    public class PagedList<T> : IPagedList<T> where T : IEntity
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public IList<T> Items { get; set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        internal PagedList(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                throw new ArgumentException($"pageIndex: {pageIndex}, must pageIndex >= 1");

            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = source.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            Items = source.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        internal PagedList() => Items = new T[0];
    }

    public class PagedList<TSource, TResult> : IPagedList<TResult>
        where TResult : IResponseModel
        where TSource : IEntity
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        public IList<TResult> Items { get; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PagedList(IEnumerable<TSource> source,
                         Func<IEnumerable<TSource>, IEnumerable<TResult>> converter,
                         int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                throw new ArgumentException($"{pageIndex}, must pageIndex >= 1");

            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = source.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            var items = source.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToArray();
            Items = new List<TResult>(converter(items));
        }
    }
}
