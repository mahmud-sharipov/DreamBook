using System;

namespace DreamBook.Application.Abstraction.Request
{
    public interface ITranslationRequestModel : IRequestModel
    {
        public Guid LanguageGuid { get; set; }
    }
}
