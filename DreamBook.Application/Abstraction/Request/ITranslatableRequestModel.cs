using System.Collections.Generic;

namespace DreamBook.Application.Abstraction.Request
{
    public interface ITranslatableRequestModel<TTranslation> : IRequestModel where TTranslation : ITranslationRequestModel
    {
        public List<TTranslation> Translations { get; set; }
    }
}
