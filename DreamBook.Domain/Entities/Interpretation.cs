namespace DreamBook.Domain.Entities;

public class Interpretation : EntityBase, ITranslatable<InterpretationTranslation>
{
    public Interpretation()
    {
        Translations = new Collection<InterpretationTranslation>();
    }

    public Guid WordGuid { get; set; }
    public virtual Word Word { get; set; }

    public Guid BookGuid { get; set; }
    public virtual Book Book { get; set; }

    public virtual ICollection<InterpretationTranslation> Translations { get; set; }
}
