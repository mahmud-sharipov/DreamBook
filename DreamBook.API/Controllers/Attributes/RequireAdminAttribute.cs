using DreamBook.API.Auth;
using Microsoft.AspNetCore.Authorization;

namespace DreamBook.API.Controllers
{
    public class RequireAdminAttribute : AuthorizeAttribute
    {
        public RequireAdminAttribute() 
        {
            Roles = UserRoles.Admin;
        }
    }
}
