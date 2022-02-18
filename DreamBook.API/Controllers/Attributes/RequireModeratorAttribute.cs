namespace DreamBook.API.Controllers.Attributes
{
    public class RequireModeratorAttribute : AuthorizeAttribute
    {
        public RequireModeratorAttribute()
        {
            Roles = $"{UserRoles.Admin},{UserRoles.Moderator}";
        }
    }
}
