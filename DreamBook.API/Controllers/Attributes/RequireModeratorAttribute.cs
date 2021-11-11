using DreamBook.API.Auth;
using Microsoft.AspNetCore.Authorization;

namespace DreamBook.API.Controllers
{
    public class RequireModeratorAttribute : AuthorizeAttribute
    {
        public RequireModeratorAttribute()
        {
            Roles = $"{UserRoles.Admin},{UserRoles.Moderator}";
        }
    }
}
