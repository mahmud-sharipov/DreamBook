namespace DreamBook.API.Controllers.Attributes
{
    public class RequireAdminAttribute : AuthorizeAttribute
    {
        public RequireAdminAttribute()
        {
            Roles = UserRoles.Admin;
        }
    }
}
