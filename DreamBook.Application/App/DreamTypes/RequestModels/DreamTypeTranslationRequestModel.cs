using DreamBook.Application.Abstraction.Request;
using System;

namespace DreamBook.Application.DreamTypes
{
    public class DreamTypeTranslationRequestModel : ITranslationRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid LanguageGuid { get; set; }
    }
}