using DreamBook.Application.Abstraction.PagedList;
using DreamBook.Domain.Interfaces;

namespace DreamBook.Persistence.Paging
{
    public static class PagedList
    {
        public static IPagedList<T> Empty<T>() where T : IEntity
        {
            return new PagedList<T>();
        }
    }
}
