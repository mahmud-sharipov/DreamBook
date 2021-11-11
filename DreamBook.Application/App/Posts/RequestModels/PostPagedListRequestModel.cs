using DreamBook.Application.Abstraction.PagedList;
using DreamBook.Domain.Entities;
using System;
using System.Linq;

namespace DreamBook.Application.App.Posts.RequestModels
{
    public class PostPagedListRequestModel : PagedListRequestModel<Post>
    {
        public Guid? CategoryGuid { get; set; }
        public DateTime? CreatedDateFrom { get; set; }
        public DateTime? CreatedDateTo { get; set; }
        public bool IncludeInactive { get; set; }

        public override IQueryable<Post> Filter(IQueryable<Post> source, string searchFiled, string defaultPropertyToOrderBy)
        {
            var result = base.Filter(source, searchFiled, defaultPropertyToOrderBy);

            if (CategoryGuid != null)
                result = result.Where(i => i.CategoryGuid == CategoryGuid);

            if (CreatedDateFrom != null)
                result = result.Where(i => i.CreatedAt >= CreatedDateFrom.Value);

            if (CreatedDateTo != null)
                result = result.Where(i => i.CreatedAt <= CreatedDateTo.Value);

            if (!IncludeInactive)
                result = result.Where(i => i.IsActive);

            return result;
        }
    }
}
