namespace DreamBook.Application.Users.RequestModels;

public class UserPagedListRequestModel : PagedListRequestModel<User>
{
    //public Guid? CategoryGuid { get; set; }
    //public DateTime? CreatedDateFrom { get; set; }
    //public DateTime? CreatedDateTo { get; set; }
    //public bool IncludeInactive { get; set; }

    public override IQueryable<User> Filter(IQueryable<User> source, string searchFiled, string defaultPropertyToOrderBy)
    {
        var result = base.Filter(source, searchFiled, defaultPropertyToOrderBy);

        return result;
    }
}
