using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Interpretations
{
    public class InterpretationTranslationRequestModel : ITranslationRequestModel
    {
        public string Description { get; set; }
        public Guid LanguageGuid { get; set; }
    }
}
