namespace DreamBook.Domain.Entities;

public class DreamType : EntityBase, ITranslatable<DreamTypeTranslation>
{
    public DreamType()
    {
        Dreams = new Collection<Dream>();
        Translations = new Collection<DreamTypeTranslation>();
    }

    public string Color { get; set; }

    public virtual ICollection<Dream> Dreams { get; set; }
    public virtual ICollection<DreamTypeTranslation> Translations { get; set; }
}
