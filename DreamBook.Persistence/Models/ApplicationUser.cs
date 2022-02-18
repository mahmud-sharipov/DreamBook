namespace DreamBook.Persistence.Models;

public class ApplicationUser : IdentityUser<Guid>, IUser
{
    public ApplicationUser()
    {
        RefreshTokens = new Collection<RefreshToken>();
    }

    public Guid Guid => Id;

    public virtual Collection<RefreshToken> RefreshTokens { get; set; }
    public string FullName { get; set; }
    public string AvatarImage { get; set; }
}
