namespace DreamBook.Domain.Interfaces;

public interface IAppLanguage : IEntity
{
    string Name { get; }

    string Code { get; }

    bool IsDefault { get; }
}
