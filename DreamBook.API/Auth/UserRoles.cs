using DreamBook.Domain.Enums;

namespace DreamBook.API.Auth
{
    public static class UserRoles
    {
        public static readonly string Admin = nameof(UserType.Admin);
        public static readonly string User = nameof(UserType.User);
    }
}
