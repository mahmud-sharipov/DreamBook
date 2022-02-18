namespace DreamBook.Application.Abstraction.PagedList
{
    public interface IPagedListRequestModel<TEntity> : IRequestModel where TEntity : class, IEntity
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }

        IQueryable<TEntity> Filter(IQueryable<TEntity> source, string searchFiled, string orderBy);
    }
}
