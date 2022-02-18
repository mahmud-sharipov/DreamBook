namespace DreamBook.Domain.Entities;

public class Language : EntityBase, IAppLanguage
{
    public string Name { get; set; }

    public string Code { get; set; }

    public bool IsDefault { get; set; }
}
