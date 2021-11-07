using DreamBook.Application.Abstraction.Request;
using System;

namespace DreamBook.Application.Interpretations
{
    public class InterpretationTranslationRequestModel : ITranslationRequestModel
    {
        public string Description { get; set; }
        public Guid LanguageGuid { get; set; }
    }
}
