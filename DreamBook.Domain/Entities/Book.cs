namespace DreamBook.Domain.Entities;

public class Book : EntityBase, ITranslatable<BookTranslation>
{
    public Book()
    {
        Translations = new Collection<BookTranslation>();
        Interpretations = new Collection<Interpretation>();
    }

    public virtual ICollection<BookTranslation> Translations { get; set; }

    public virtual ICollection<Interpretation> Interpretations { get; set; }
}
