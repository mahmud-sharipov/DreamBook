using DreamBook.Application.Abstraction.PagedList;
using DreamBook.Domain.Entities;
using System;
using System.Linq;

namespace DreamBook.Application.Dreams
{
    public class DreamPagedListRequestModel : PagedListRequestModel<Dream>
    {
        public Guid? TypeGuid { get; set; }
        public bool OrderByDescending { get; set; }

        public override IQueryable<Dream> Filter(IQueryable<Dream> source, string searchFiled, string defaultPropertyToOrderBy)
        {
            var result = base.Filter(source, searchFiled, OrderByDescending ? "" : defaultPropertyToOrderBy);

            if (TypeGuid != null)
                result = result.Where(i => i.TypeGuid == TypeGuid);

            if (OrderByDescending)
                result = result.OrderByDescending(e => e.CreatedAt);

            return result;
        }
    }
}
