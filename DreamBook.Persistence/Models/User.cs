namespace DreamBook.Persistence.Model;

public class User : IdentityUser<Guid>, IUser
{
    public User()
    {
        RefreshTokens = new Collection<RefreshToken>();
    }

    public virtual Collection<RefreshToken> RefreshTokens { get; set; }
    public virtual Collection<Dream> Dreams { get; set; }

    public string FullName { get; set; }
    public string AvatarImage { get; set; }

    public Guid Guid => Id;
}
