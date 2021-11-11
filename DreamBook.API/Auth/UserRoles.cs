using DreamBook.Domain.Enums;

namespace DreamBook.API.Auth
{
    public static class UserRoles
    {
        public const string Admin = nameof(UserType.Admin);
        public const string Moderator = nameof(UserType.Admin);
        public const string User = nameof(UserType.User);
    }
}
