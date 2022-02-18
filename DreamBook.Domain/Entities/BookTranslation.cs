namespace DreamBook.Domain.Entities;

public class BookTranslation : EntityBase, ITranslation
{
    public BookTranslation()
    {
        Interpretations = new Collection<InterpretationTranslation>();
    }

    public Guid BookGuid { get; set; }
    public virtual Book Book { get; set; }

    public Guid LanguageGuid { get; set; }
    public virtual Language Language { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public virtual ICollection<InterpretationTranslation> Interpretations { get; set; }
}
