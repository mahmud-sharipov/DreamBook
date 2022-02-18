namespace DreamBook.Persistence.Model;

public class RefreshToken : EntityBase
{
    public string Token { get; set; }
    public DateTime ExpiryOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedByIp { get; set; }
    public string RevokedByIp { get; set; }
    public DateTime RevokedOn { get; set; }

    public virtual User User { get; set; }
    public Guid UserGuid { get; set; }
}
