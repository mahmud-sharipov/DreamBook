using DreamBook.Application.Abstraction.PagedList;
using DreamBook.Domain.Entities;
using System;
using System.Linq;

namespace DreamBook.Application.Interpretations
{
    public class InterpretationPagedListRequestModel : SimplePagedListRequestModel<InterpretationTranslation>
    {
        public Guid? BookGuid { get; set; }
        public Guid? WordGuid { get; set; }

        public override IQueryable<InterpretationTranslation> Filter(IQueryable<InterpretationTranslation> source, string searchFiled, string defaultPropertyToOrderBy)
        {
            var result = base.Filter(source, searchFiled, defaultPropertyToOrderBy);

            if (BookGuid != null)
                result = result.Where(i => i.Interpretation.BookGuid == BookGuid);

            if (WordGuid != null)
                result = result.Where(i => i.Interpretation.WordGuid == WordGuid);

            return result;
        }
    }
}
