namespace DreamBook.Domain.Entities;

public class InterpretationTranslation : EntityBase, ITranslation
{
    public Guid InterpretationGuid { get; set; }
    public virtual Interpretation Interpretation { get; set; }

    public Guid LanguageGuid { get; set; }
    public virtual Language Language { get; set; } 

    public string Description { get; set; }

    public Guid WordGuid { get; set; }
    public virtual WordTranslation Word { get; set; }

    public Guid BookGuid { get; set; }
    public virtual BookTranslation Book { get; set; }
}
