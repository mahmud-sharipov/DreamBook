namespace DreamBook.Domain.Entities;

public class Ad : EntityBase, ITranslatable<AdTranslation>
{
    public Ad()
    {
        Translations = new Collection<AdTranslation>();
    }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Image { get; set; }
    public string Source { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<AdTranslation> Translations { get; set; }
}
