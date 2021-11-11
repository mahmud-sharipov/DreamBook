using DreamBook.Application.Abstraction.Request;
using System;
using System.Text.Json.Serialization;

namespace DreamBook.Application.Interpretations
{
    public class InterpretationTranslationRequestModel : ITranslationRequestModel
    {
        public string Description { get; set; }
        public Guid LanguageGuid { get; set; }

        [JsonIgnore]
        public Guid WordGuid { get; set; }
        [JsonIgnore]
        public Guid BookGuid { get; set; }
    }
}
