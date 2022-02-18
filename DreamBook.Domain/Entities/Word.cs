namespace DreamBook.Domain.Entities;

public class Word : EntityBase, ITranslatable<WordTranslation>
{
    public Word()
    {
        Translations = new Collection<WordTranslation>();
        Dreams = new Collection<DreamWord>();
        Interpretations = new Collection<Interpretation>();
    }

    public virtual ICollection<WordTranslation> Translations { get; set; }

    public virtual ICollection<Interpretation> Interpretations { get; set; }

    public virtual ICollection<DreamWord> Dreams { get; set; }
}
