using System;

namespace DreamBook.Domain.Interfaces
{
    public interface ITranslation : IEntity
    {
        Guid LanguageGuid { get; }
    }
}
