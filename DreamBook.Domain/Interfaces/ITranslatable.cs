using System.Collections.Generic;

namespace DreamBook.Domain.Interfaces
{
    public interface ITranslatable<TTranslation> : IEntity
        where TTranslation : ITranslation
    {
        ICollection<TTranslation> Translations { get; }
    }
}
