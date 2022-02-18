namespace DreamBook.Domain.Entities;

public class AdTranslation : EntityBase, ITranslation
{
    public string Title { get; set; }
    public string Description { get; set; }

    public Guid LanguageGuid { get; set; }
    public virtual Language Language { get; set; }

    public Guid AdGuid { get; set; }
    public virtual Ad Ad { get; set; }
}
