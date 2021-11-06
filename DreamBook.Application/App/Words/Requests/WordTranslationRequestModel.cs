using DreamBook.Application.Abstraction.Request;
using System;

namespace DreamBook.Application.Words
{
    public class WordTranslationRequestModel : ITranslationRequestModel
    {
        public Guid LanguageGuid { get; set; }

        public string Name { get; set; }
    }
}
